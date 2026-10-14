"""Offline, conservative OpenScope route and public-source inventory (Python 3.10+)."""
import argparse
import collections
import hashlib
import json
from pathlib import Path
import re

ROOT = Path(__file__).resolve().parents[1]
CONTRACT = Path("contracts/openscope/openapi-2026-10-11.json")
REPORT = Path("contracts/openscope/coverage.json")
VERBS = {"get", "put", "post", "delete", "patch", "head", "options", "trace"}
CLASS = re.compile(r"\b(public|internal)\s+(?:partial\s+)?class\s+(\w+)")
METHOD = re.compile(r"^\s*(public|internal|protected|private)\s+(?:(?:async|static|virtual|override|sealed)\s+)*([^\n=;{}]+?)\s+(\w+)\s*\(([^;{}]*?)\)\s*\{", re.M)
CALL = re.compile(r'\bCallApi(?:Async)?\(\s*(localVarPath|"[^"]+"|BridgeWritePath\(\s*"[^"]+"\s*,\s*request\s*\))\s*,\s*Method\.(\w+)')


def normalized(method, path):
    return method.upper(), re.sub(r"\{[^}]+\}", "{}", path)


def text_bytes(value):
    return ("\ufeff" + value.replace("\r\n", "\n").replace("\n", "\r\n")).encode("utf-8")


def json_bytes(value):
    return text_bytes(json.dumps(value, indent=2, ensure_ascii=False) + "\n")


def mask_source(source):
    # Preserve offsets, newlines and actual syntax braces; ignore comments/string braces.
    pattern = r'//[^\n]*|/\*.*?\*/|@"(?:""|[^"])*"|"(?:\\.|[^"\\])*"|\'(?:\\.|[^\'\\])*\''
    return re.sub(pattern, lambda m: re.sub(r"[^\n\r]", " ", m.group()), source, flags=re.S)


def extract(source, file):
    masked = mask_source(source)
    classes = CLASS.findall(masked)
    if len(classes) != 1:
        raise ValueError(f"Expected one API class in {file}, found {len(classes)}.")
    class_access, class_name = classes[0]
    methods, counted = [], 0
    for match in METHOD.finditer(masked):
        start = match.end()
        depth, end = 1, start
        while depth and end < len(masked):
            depth += (masked[end] == "{") - (masked[end] == "}")
            end += 1
        if depth:
            raise ValueError(f"Unbalanced method body in {file}.")
        body = source[start:end - 1]
        body_mask = masked[start:end - 1]
        visibility, return_type, name, parameters = match.groups()
        calls = []
        for call in CALL.finditer(body):
            # Reject matches appearing inside comments or strings.
            if not body_mask[call.start():call.start() + 7].strip():
                continue
            counted += 1
            path_expression, verb = call.groups()
            if path_expression == "localVarPath":
                paths = re.findall(r'\bvar localVarPath\s*=\s*"([^"]+)";', body)
                if len(paths) != 1:
                    raise ValueError(f"Ambiguous route in {file}:{name}.")
                path = paths[0]
            elif path_expression.startswith("BridgeWritePath"):
                # Only the fixed, segment-escaping bridge helper and literal routes are supported.
                path = re.search(r'"([^"]+)"', path_expression).group(1)
            else:
                path = path_expression[1:-1]
            calls.append((verb.upper(), path))
        methods.append({
            "file": file, "class": class_name, "class_access": class_access,
            "method": name, "access": visibility, "return_type": " ".join(return_type.split()),
            "parameters": " ".join(source[match.start(4):match.end(4)].split()),
            "line": source[:match.start(1)].count("\n") + 1,
            "native_cancellation_parameter": "CancellationToken" in parameters,
            "deserializes_response": bool(re.search(r"\b(?:Deserialize|DeserializeObject)\s*(?:<[^;{}()]+>)?\s*\(", body_mask)),
            "routes": calls,
            "delegates": sorted(set(re.findall(r"\b(\w+(?:WithHttpInfo))\s*\(", body_mask))) if not calls else [],
        })
    direct_calls = len(re.findall(r"\bCallApi(?:Async)?\s*\(", masked))
    if direct_calls != counted:
        raise ValueError(f"Unparsed HTTP calls in {file}: {direct_calls} calls, {counted} extracted.")
    return methods


def phase(tags):
    if set(tags) & {"Authorization", "Additional", "Teamwork"}:
        return "1-contract-and-transport"
    if set(tags) & {"Contact", "Project", "Task"}:
        return "2-contacts-projects-tasks"
    if set(tags) & {"Accounting", "IncomingInvoice", "OutgoingInvoice", "RecurringInvoice", "Cashbook", "Credit"}:
        return "3-accounting-invoices"
    return "4-remaining-groups"


def build_report(spec, sources, facade, contract_hash):
    methods, unstable = [], set()
    for file, source in sorted(sources.items()):
        extracted = extract(source, file)
        methods.extend(extracted)
        if '[Obsolete("Unstable or incomplete implementation", false)]' in source:
            unstable.update(m["class"] for m in extracted)
    routes = collections.defaultdict(list)
    for method in methods:
        for verb, path in method["routes"]:
            routes[normalized(verb, path)].append(method)
    operations, ids = {}, collections.defaultdict(list)
    for path, item in sorted(spec["paths"].items()):
        for verb, op in sorted(item.items()):
            if verb not in VERBS:
                continue
            key = normalized(verb, path)
            if key in operations:
                raise ValueError(f"Ambiguous normalized contract route: {key}.")
            operations[key] = (path, verb, op, item.get("parameters", []))
            if op.get("operationId"):
                ids[op["operationId"]].append(f"{verb.upper()} {path}")
    entries = []
    for key, (path, verb, op, inherited) in sorted(operations.items()):
        matches = routes.get(key, [])
        exposed = [m for m in matches if m["class_access"] == m["access"] == "public"]
        reason = ("No public source route found; implementation deferred to its ordered batch." if not exposed else
                  "Public route exists; parameter, serialization, response and live behavior verification is deferred.")
        entries.append({"method": verb.upper(), "path": path, "operation_id": op.get("operationId"),
            "tags": op.get("tags", []), "status": "deferred", "reason": reason, "phase": phase(op.get("tags", [])),
            "public_source_route": bool(exposed),
            "sdk_methods": sorted({m["class"] + "." + m["method"] for m in matches}),
            "parameters": inherited + op.get("parameters", []), "request_body": op.get("requestBody"),
            "responses": op.get("responses", {}), "security": op.get("security", spec.get("security", []))})
    source_methods = []
    for method in methods:
        if not method["routes"] and not method["delegates"]:
            continue
        source_methods.append(dict(method, unstable_class=method["class"] in unstable,
            facade_property=bool(re.search(r"\b" + re.escape(method["class"]) + r"\s+\w+\s*\{\s*get;", facade))))
    summary = {
        "contract_sha256": contract_hash, "openapi": spec.get("openapi"), "api_version": spec["info"]["version"],
        "paths": len(spec["paths"]), "operations": len(operations), "schemas": len(spec.get("components", {}).get("schemas", {})),
        "source_routes": len(routes), "matching_routes": len(routes.keys() & operations.keys()),
        "missing_routes": len(operations.keys() - routes.keys()), "legacy_routes": len(routes.keys() - operations.keys()),
        "source_routes_on_unstable_classes": sum(any(m["class"] in unstable for m in v) for v in routes.values()),
        "source_http_methods": sum(bool(m["routes"]) for m in methods),
        "status_counts": dict(collections.Counter(e["status"] for e in entries)),
        "missing_by_tag": dict(sorted(collections.Counter(t for e in entries if not e["public_source_route"] for t in e["tags"]).items())),
        "duplicate_operation_ids": {k: v for k, v in sorted(ids.items()) if len(v) > 1},
    }
    return {"summary": summary, "operations": entries, "sdk_methods": source_methods,
        "legacy_routes": [{"method": k[0], "normalized_path": k[1], "sdk_methods": sorted({m["class"] + "." + m["method"] for m in routes[k]}),
            "decision": "Retain existing APIs; absent documentation does not prove endpoint removal."} for k in sorted(routes.keys() - operations.keys())],
        "limitations": ["Static C# source inventory, not compiled accessibility or behavior verification.",
            "Method parser supports the checked SDK style, not arbitrary C#; unparsed direct HTTP calls fail the audit.",
            "Handwritten routes, cancellation partials and WithHttpInfo wrapper delegates are included; delegate lists are not a runtime call graph.",
            "No operation is promoted to supported by a route match. All pending contract/live checks remain deferred.",
            "Schema references resolve against the pinned contract; generated files can contain handwritten edits."]}


def run(root):
    contract = root / CONTRACT
    metadata = json.loads((root / "contracts/openscope/provenance.json").read_text(encoding="utf-8-sig"))
    contract_hash = hashlib.sha256(contract.read_bytes()).hexdigest()
    if contract_hash != metadata["repository_sha256"]:
        raise ValueError("Pinned contract hash differs from provenance.")
    sdk = root / "src/CompuMaster.Scopevisio.OpenApi"
    sources = {p.relative_to(root).as_posix(): p.read_text(encoding="utf-8-sig") for p in sorted((sdk / "Api").glob("*.cs"))}
    for path in sdk.rglob("*.cs"):
        if path.parent == sdk / "Api" or path == sdk / "Client/ApiClient.cs":
            continue
        if re.search(r"\bCallApi(?:Async)?\s*\(", mask_source(path.read_text(encoding="utf-8-sig"))):
            raise ValueError(f"HTTP call outside the audited API boundary: {path.relative_to(root)}")
    facade = (sdk / "OpenScopeApiClient.cs").read_text(encoding="utf-8-sig")
    report = build_report(json.loads(contract.read_text(encoding="utf-8-sig")), sources, facade, contract_hash)
    if report["summary"]["source_routes"] < 100:
        raise ValueError("Incomplete SDK route extraction.")
    return report


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--check", action="store_true", help="Fail if the committed offline report is stale.")
    args = parser.parse_args()
    report = run(ROOT)
    content = json_bytes(report)
    path = ROOT / REPORT
    if args.check:
        if path.read_bytes() != content:
            raise SystemExit("Coverage report is stale. Run python tools/contract_coverage.py and review the diff.")
    else:
        path.write_bytes(content)
    print(json.dumps(report["summary"], indent=2))


if __name__ == "__main__":
    main()

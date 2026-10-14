"""Isolated extraction fixtures. Never access a server or account."""
import copy
import unittest
from contract_coverage import ROOT, REPORT, build_report, extract, json_bytes, normalized, run


def source(access="public", class_access="public", extra=""):
    return '''namespace Fixture {
    [Obsolete("Unstable or incomplete implementation", false)]
    %s partial class ContactApi {
        %s ApiResponse<Records<Contact>> Read(string id, CancellationToken cancellationToken) {
            // CallApi(localVarPath, Method.Delete) and a misleading brace }
            var localVarPath = "/contact/{legacyId}";
            string message = "a } brace";
            var response = client.CallApiAsync(localVarPath, Method.Get, cancellationToken);
            return client.Deserialize(response);
        }
        public Records<Contact> ReadConvenience(string id) {
            return ReadWithHttpInfo(id).Data;
        }
        %s
    }
}''' % (class_access, access, extra)


def spec():
    return {"openapi": "3.0.1", "info": {"version": "fixture"}, "paths": {
        "/contact/{id}": {"parameters": [{"name": "id", "in": "path", "required": True}], "get": {
            "operationId": "read", "tags": ["Contact"], "responses": {"200": {"description": "fixture"}}}},
        "/projects": {"post": {"operationId": "read", "tags": ["Project"], "responses": {}}}}, "components": {"schemas": {}}}


def native_result_gaps(report):
    # Preserve overloads; source mappings alone do not prove active I/O or server support.
    candidates = {method["class"] + "." + method["method"]
                  for method in report["sdk_methods"]
                  if method["class_access"] == "public" and method["access"] == "public"
                  and method["native_cancellation_parameter"]
                  and "Task<" in method["return_type"]
                  and "ApiResponse<" in method["return_type"]
                  and "ApiResponse<Object>" not in method["return_type"]
                  and "ApiResponse<object>" not in method["return_type"]}
    return [operation["method"] + " " + operation["path"]
            for operation in report["operations"]
            if not candidates.intersection(operation["sdk_methods"])]


class InventoryTests(unittest.TestCase):
    def test_verb_case_and_placeholder_names_do_not_change_route_identity(self):
        self.assertEqual(normalized("get", "/contact/{id}"), normalized("GET", "/contact/{legacyId}"))
        self.assertNotEqual(normalized("post", "/contact/{id}"), normalized("get", "/contact/{id}"))

    def test_comments_strings_nested_scopes_and_public_typed_results(self):
        methods = extract(source(), "fixture.cs")
        self.assertEqual(methods[0]["routes"], [("GET", "/contact/{legacyId}")])
        self.assertEqual(methods[0]["return_type"], "ApiResponse<Records<Contact>>")
        self.assertTrue(methods[0]["native_cancellation_parameter"])
        self.assertTrue(methods[0]["deserializes_response"])
        self.assertEqual(methods[1]["delegates"], ["ReadWithHttpInfo"])

    def test_jsonconvert_deserialization_including_nested_generics_is_recognized(self):
        for expression in ("JsonConvert.DeserializeObject(response)",
                           "JsonConvert.DeserializeObject < Records<JObject> > (response)",
                           "client.Deserialize < Dictionary<string, List<Contact>> > (response)"):
            with self.subTest(expression=expression):
                methods = extract(source().replace("client.Deserialize(response)", expression), "fixture.cs")
                self.assertTrue(methods[0]["deserializes_response"])

    def test_comments_and_strings_do_not_claim_response_deserialization(self):
        for expression in ('"Deserialize("; // JsonConvert.DeserializeObject<Records<JObject>>(response)',
                           'null; /* client.Deserialize(response) */'):
            with self.subTest(expression=expression):
                methods = extract(source().replace("client.Deserialize(response)", expression), "fixture.cs")
                self.assertFalse(methods[0]["deserializes_response"])

    def test_internal_methods_and_internal_classes_are_not_public_coverage(self):
        for api in (source(access="internal"), source(class_access="internal")):
            report = build_report(spec(), {"fixture.cs": api}, "", "hash")
            self.assertFalse(report["operations"][0]["public_source_route"])

    def test_matching_route_is_deferred_until_contract_and_live_verification(self):
        report = build_report(spec(), {"fixture.cs": source()}, "ContactApi ContactApi { get; set; }", "hash")
        self.assertEqual(report["summary"]["matching_routes"], 1)
        self.assertEqual(report["summary"]["missing_routes"], 1)
        self.assertTrue(report["operations"][0]["public_source_route"])
        self.assertEqual(report["operations"][0]["status"], "deferred")
        self.assertTrue(report["sdk_methods"][0]["unstable_class"])
        self.assertTrue(report["sdk_methods"][0]["facade_property"])
        self.assertEqual(len(report["operations"][0]["parameters"]), 1)

    def test_duplicate_operation_ids_do_not_merge_distinct_routes(self):
        report = build_report(spec(), {"fixture.cs": source()}, "", "hash")
        self.assertEqual(report["summary"]["operations"], 2)
        self.assertEqual(report["summary"]["duplicate_operation_ids"]["read"], ["GET /contact/{id}", "POST /projects"])

    def test_legacy_route_is_retained(self):
        contract = spec()
        del contract["paths"]["/contact/{id}"]
        report = build_report(contract, {"fixture.cs": source()}, "", "hash")
        self.assertEqual(report["summary"]["legacy_routes"], 1)
        self.assertIn("Retain", report["legacy_routes"][0]["decision"])

    def test_fixed_bridge_path_helper_maps_each_literal_route(self):
        api = source(extra='public void Bridge(CancellationToken token) { client.CallApi(BridgeWritePath("/teamworkbridge/{arg1}", request), Method.Put); client.CallApiAsync(BridgeWritePath("/teamworkbridge/{arg1}/{arg2}", request), Method.Delete, token); }')
        self.assertEqual(extract(api, "fixture.cs")[-1]["routes"], [("PUT", "/teamworkbridge/{arg1}"), ("DELETE", "/teamworkbridge/{arg1}/{arg2}")])

    def test_fixed_read_bridge_helper_maps_literal_get_routes(self):
        api = source(extra='public void Bridge(CancellationToken token) { client.CallApi(BridgeReadPath("/teamworkbridge/{arg1}", request), Method.Get); client.CallApiAsync(BridgeReadPath("/teamworkbridge/{arg1}/{arg2}/{arg3}", request), Method.Get, token); }')
        self.assertEqual(extract(api, "fixture.cs")[-1]["routes"], [("GET", "/teamworkbridge/{arg1}"), ("GET", "/teamworkbridge/{arg1}/{arg2}/{arg3}")])

    def test_bridge_helper_unknown_shapes_still_fail_closed(self):
        for expression in ('OtherPath("/teamworkbridge/{arg1}", request)', 'BridgeWritePath(dynamicPath, request)', 'BridgeWritePath("/teamworkbridge/{arg1}", otherRequest)'):
            with self.subTest(expression=expression), self.assertRaisesRegex(ValueError, "Unparsed HTTP calls"):
                extract(source(extra='public void Bridge() { client.CallApi(' + expression + ', Method.Put); }'), "fixture.cs")

    def test_unknown_call_shape_fails_instead_of_underreporting(self):
        with self.assertRaisesRegex(ValueError, "Unparsed HTTP calls"):
            extract(source(extra="public void Dynamic() { client.CallApi(GetPath(), GetVerb()); }"), "fixture.cs")

    def test_normalized_contract_collision_fails(self):
        contract = copy.deepcopy(spec())
        contract["paths"]["/contact/{other}"] = {"get": {"responses": {}}}
        with self.assertRaisesRegex(ValueError, "Ambiguous normalized"):
            build_report(contract, {"fixture.cs": source()}, "", "hash")

    def test_all_current_operations_have_public_native_result_source(self):
        report = run(ROOT)
        self.assertEqual(len(report["operations"]), 323)
        self.assertEqual(native_result_gaps(report), [])

    def test_missing_native_result_is_detected_without_losing_overloads(self):
        report = run(ROOT)
        selected = next(operation for operation in report["operations"]
                        if operation["method"] == "POST" and operation["path"] == "/token")
        selected_names = set(selected["sdk_methods"])
        seeded = copy.deepcopy(report)
        for method in seeded["sdk_methods"]:
            if method["class"] + "." + method["method"] in selected_names:
                method["native_cancellation_parameter"] = False
        self.assertEqual(native_result_gaps(seeded), ["POST /token"])
        self.assertEqual(native_result_gaps(report), [])

    def test_pinned_report_is_complete_and_reproducible_including_handwritten_partials(self):
        report = run(ROOT)
        self.assertEqual(json_bytes(report), (ROOT / REPORT).read_bytes())
        self.assertEqual(len(report["operations"]), 323)
        self.assertTrue(all(row["status"] and row["reason"] and row["phase"] for row in report["operations"]))
        methods = report["sdk_methods"]
        contact = next(m for m in methods if m["method"] == "GetContactsWithHttpInfo")
        self.assertEqual(contact["return_type"], "ApiResponse<Model.RecordCollection<Contact>>")
        partials = {m["file"].split("/")[-1] for m in methods if m["native_cancellation_parameter"]}
        self.assertIn("AuthorizationApi.Cancellation.cs", partials)
        self.assertIn("AdditionalApi.Cancellation.cs", partials)

    def test_report_encoding_has_bom_and_crlf(self):
        content = json_bytes({"fixture": "UTF-8 ä"})
        self.assertTrue(content.startswith(b"\xef\xbb\xbf"))
        self.assertNotIn(b"\n", content.replace(b"\r\n", b""))


if __name__ == "__main__":
    unittest.main()

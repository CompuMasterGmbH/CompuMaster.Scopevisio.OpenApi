"""Durable offline fixtures for the bounded generator and its import boundary."""
import copy
import json
from pathlib import Path
import tempfile
import unittest
import generate_read_partials as generator
from contract_coverage import json_bytes, text_bytes


class BoundedGenerationTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(prefix="scopevisio-read-generator-")
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name) / "repo"
        self.root.mkdir()
        self.recipe = json.loads((generator.ROOT / generator.RECIPE).read_text(encoding="utf-8-sig"))
        full = json.loads((generator.ROOT / self.recipe["contract"]).read_text(encoding="utf-8-sig"))
        self.spec = {"openapi": "3.0.1", "paths": {self.recipe["path"]: copy.deepcopy(full["paths"][self.recipe["path"]])}}
        for relative in (generator.TOOL, Path("tools/contract_coverage.py"), Path(self.recipe["template"])):
            self.write(relative, (generator.ROOT / relative).read_bytes())
        self.manual = generator.SDK / "Api/TaskApi.cs"
        self.write(self.manual, text_bytes("public partial class TaskApi {\n public void Legacy() {}\n}\n"))
        self.save_contract()

    def write(self, path, data):
        selected = self.root / path
        selected.parent.mkdir(parents=True, exist_ok=True)
        selected.write_bytes(data)

    def save_contract(self):
        data = json_bytes(self.spec)
        self.write(self.recipe["contract"], data)
        self.recipe["contract_sha256"] = generator.sha(data)
        self.recipe["operation_sha256"] = generator.operation_hash(self.spec["paths"][self.recipe["path"]])
        self.save_recipe()

    def save_recipe(self):
        self.write(generator.RECIPE, json_bytes(self.recipe))

    def test_two_fresh_outputs_have_identical_source_manifest_and_no_source_mutation(self):
        before = (self.root / self.manual).read_bytes()
        outputs = [Path(self.temp.name) / name for name in ("a", "b")]
        for output in outputs:
            name = generator.generate(output, self.root)
        for filename in (name, "manifest.json"):
            self.assertEqual((outputs[0] / filename).read_bytes(), (outputs[1] / filename).read_bytes())
            data = (outputs[0] / filename).read_bytes()
            self.assertTrue(data.startswith(b"\xef\xbb\xbf"))
            self.assertNotIn(b"\n", data.replace(b"\r\n", b""))
        self.assertEqual(before, (self.root / self.manual).read_bytes())
        manifest = json.loads((outputs[0] / "manifest.json").read_text(encoding="utf-8-sig"))
        self.assertEqual(generator.sha((outputs[0] / name).read_bytes()), manifest["generated_sha256"])
        self.assertNotIn(str(self.temp.name), (outputs[0] / "manifest.json").read_text(encoding="utf-8-sig"))

    def test_reviewed_import_and_manifest_are_checked_without_writing(self):
        recipe, source, manifest = generator.plan(self.root)
        self.write(generator.SDK / recipe["import"], source)
        self.write(generator.MANIFEST, manifest)
        generator.check_import(self.root)
        self.write(generator.SDK / recipe["import"], source + b"// changed")
        with self.assertRaisesRegex(ValueError, "owned import differs"):
            generator.check_import(self.root)

    def test_missing_import_or_changed_manifest_fails_check(self):
        with self.assertRaises(ValueError):
            generator.check_import(self.root)
        recipe, source, manifest = generator.plan(self.root)
        self.write(generator.SDK / recipe["import"], source)
        self.write(generator.MANIFEST, manifest + b" ")
        with self.assertRaisesRegex(ValueError, "manifest is not reproducible"):
            generator.check_import(self.root)

    def test_existing_output_is_never_reused_or_overwritten(self):
        output = Path(self.temp.name) / "out"
        generator.generate(output, self.root)
        before = {p.name: p.read_bytes() for p in output.iterdir()}
        with self.assertRaisesRegex(ValueError, "absent or empty"):
            generator.generate(output, self.root)
        self.assertEqual(before, {p.name: p.read_bytes() for p in output.iterdir()})

    def test_repo_source_test_root_parent_and_relative_outputs_are_rejected(self):
        selected = [self.root, self.root.parent, self.root / generator.SDK / "Api",
                    self.root / "src/CompuMaster.Scopevisio.OpenApi.AsyncTests/new",
                    self.root / "tools/new", Path("relative"), Path(self.root.anchor)]
        for output in selected:
            with self.subTest(output=output), self.assertRaises(ValueError):
                generator.validate_output(output, self.root)

    def test_linked_output_is_rejected_when_links_are_available(self):
        real = Path(self.temp.name) / "real-output"
        real.mkdir()
        link = Path(self.temp.name) / "linked-output"
        try:
            link.symlink_to(real, target_is_directory=True)
        except OSError as error:
            self.skipTest("Directory symlink creation is unavailable: " + str(error))
        with self.assertRaisesRegex(ValueError, "link/junction"):
            generator.validate_output(link / "fresh", self.root)
        self.assertEqual(list(real.iterdir()), [])

    def test_owned_disposable_repository_output_is_accepted(self):
        output = self.root / ".task-work-generator/fresh"
        generator.generate(output, self.root)
        self.assertEqual(len(list(output.iterdir())), 2)

    def test_contract_drift_and_template_drift_fail_before_output_creation(self):
        for key in ("contract", "template"):
            path = self.root / self.recipe[key]
            before = path.read_bytes()
            path.write_bytes(before + b" ")
            output = Path(self.temp.name) / ("drift-" + key)
            with self.assertRaisesRegex(ValueError, "differ from the reviewed recipe"):
                generator.generate(output, self.root)
            self.assertFalse(output.exists())
            path.write_bytes(before)

    def test_newly_pinned_unsupported_contract_shapes_require_new_review(self):
        original = copy.deepcopy(self.spec)
        changes = [
            lambda op: op.update(requestBody={}),
            lambda op: op["parameters"].append({"name": "fields", "in": "query"}),
            lambda op: op["parameters"][0]["schema"].update(minimum=1),
            lambda op: op["parameters"][0]["schema"].update(format="int32"),
            lambda op: op["responses"]["200"].update(content={"application/json": {}}),
            lambda op: op.update(security=[]),
            lambda op: op.update(callbacks={}),
        ]
        for change in changes:
            with self.subTest(change=change):
                self.spec = copy.deepcopy(original)
                change(self.spec["paths"][self.recipe["path"]]["get"])
                self.save_contract()
                with self.assertRaises(ValueError):
                    generator.plan(self.root)
        self.spec = copy.deepcopy(original)
        self.spec["paths"][self.recipe["path"]]["post"] = {}
        self.save_contract()
        with self.assertRaises(ValueError):
            generator.plan(self.root)

    def test_operation_fingerprint_drift_is_rejected_independently(self):
        self.recipe["operation_sha256"] = "0" * 64
        self.save_recipe()
        with self.assertRaisesRegex(ValueError, "fingerprint"):
            generator.plan(self.root)

    def test_existing_sdk_name_collision_is_rejected(self):
        self.write(generator.SDK / "Api/TaskApi.Other.cs", text_bytes(
            "public partial class TaskApi {\n public void GetSubtaskDetailsAsync() {}\n}\n"))
        with self.assertRaisesRegex(ValueError, "collides with existing source"):
            generator.plan(self.root)

    def test_existing_class_is_required(self):
        self.write(self.manual, text_bytes("public partial class OtherApi {\n public void Legacy() {}\n}\n"))
        with self.assertRaisesRegex(ValueError, "class does not exist"):
            generator.plan(self.root)

    def test_unsafe_recipe_paths_and_documentation_are_rejected(self):
        original = copy.deepcopy(self.recipe)
        for key, value in (("contract", "../outside.json"), ("template", "../template"),
                           ("import", "../unowned.cs"), ("method", "bad-name"),
                           ("parameter", "class"), ("remarks", "Unsafe <xml>"),
                           ("summary", "Unsafe & entity"), ("version", 2)):
            with self.subTest(key=key):
                self.recipe = copy.deepcopy(original)
                self.recipe[key] = value
                self.save_recipe()
                with self.assertRaises(ValueError):
                    generator.plan(self.root)


if __name__ == "__main__":
    unittest.main()

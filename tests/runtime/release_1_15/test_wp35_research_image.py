"""Offline packaging tests; no Docker daemon, Azure, registry or real keys."""
from contextlib import redirect_stdout
import importlib.util
import io
import json
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch
import zipfile

ROOT = Path(__file__).resolve().parents[3]
SPEC = importlib.util.spec_from_file_location("wp35", ROOT / "eng/azure-cli/r1.15-qualification/wp35_research_image.py")
wp35 = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(wp35)
COMMIT = "a" * 40


class PackagingTests(unittest.TestCase):
    def test_plan_never_claims_execution_or_health(self):
        record = wp35.plan(COMMIT)
        for key in ("registryPublished", "azureDeployed", "liveAttemptReady", "PUBLIC_RUNTIME_VERIFIED"):
            self.assertIs(record[key], False)
        self.assertTrue(record["AUTHENTIC_DATA_NOT_IMPORTED"])
        self.assertEqual(record["health"], "UNVERIFIED")
        self.assertEqual(record["servingCommand"][-1], "--research-snapshot-api")

    def test_invalid_commit_rejected(self):
        for commit in ("", "a" * 39, "A" * 40, "x" * 40):
            with self.assertRaises(ValueError):
                wp35.plan(commit)

    def test_recipe_offline_linux_publish(self):
        restore, publish = wp35.recipe(Path("source"), Path("context"), Path("empty"))
        self.assertEqual(restore[0:2], ["dotnet", "restore"])
        self.assertIn("--source", restore)
        self.assertIn("--configfile", restore)
        self.assertIn("-p:NuGetAudit=false", restore)
        self.assertIn("-p:SelfContained=false", restore)
        self.assertIn("-p:UseAppHost=false", restore)
        self.assertIn("-p:UseAppHost=false", publish)
        self.assertIn("--no-restore", publish)
        self.assertNotIn("--runtime", restore)
        self.assertNotIn("--runtime", publish)
        self.assertEqual(publish[publish.index("--self-contained") + 1], "false")

    def test_default_only_reads_git(self):
        with patch.object(wp35, "run", return_value=COMMIT.encode()) as run, patch.object(wp35, "build") as build:
            with redirect_stdout(io.StringIO()):
                self.assertEqual(wp35.main(["--repository", str(ROOT)]), 0)
            self.assertEqual(run.call_count, 1)
            self.assertEqual(run.call_args.args[0], ["git", "rev-parse", "HEAD"])
            build.assert_not_called()

    def test_build_without_feasibility_cannot_reach_docker(self):
        with patch.object(wp35, "run", return_value=COMMIT.encode()) as run, patch.object(wp35, "build") as build:
            with redirect_stdout(io.StringIO()) as output:
                self.assertEqual(wp35.main(["--repository", str(ROOT), "--build"]), 2)
            self.assertEqual(json.loads(output.getvalue())["result"], "BUILD_BLOCKED_F1_FEASIBILITY_REQUIRED")
            self.assertEqual(run.call_count, 1)
            build.assert_not_called()

    def test_approved_attempt_does_not_claim_feasibility(self):
        with patch.object(wp35, "run", return_value=COMMIT.encode()), patch.object(wp35, "build", return_value=0) as build:
            with redirect_stdout(io.StringIO()):
                self.assertEqual(wp35.main(["--repository", str(ROOT), "--build", "--prepare-approved-compatibility-attempt"]), 0)
            self.assertEqual(build.call_args.args[1]["feasibilityBasis"], "OWNER_APPROVED_REVERSIBLE_ATTEMPT_COMPATIBILITY_UNVERIFIED")
            self.assertFalse(build.call_args.args[1]["azureDeployed"])

    def test_failed_tool_output_is_not_disclosed(self):
        with patch.object(wp35.subprocess, "run") as execute:
            execute.return_value.returncode = 1
            execute.return_value.stderr = b"FAKE_SENSITIVE_TOOL_OUTPUT"
            with self.assertRaisesRegex(ValueError, "^TOOL_FAILED$"):
                wp35.run(["docker", "info"], ROOT)

    def test_build_failure_records_fixed_stage(self):
        with tempfile.TemporaryDirectory() as owned, patch.object(wp35.tempfile, "mkdtemp", return_value=owned), patch.object(wp35, "run", side_effect=OSError("FAKE_SENSITIVE_TOOL_OUTPUT")):
            record = wp35.plan(COMMIT)
            self.assertEqual(wp35.build(ROOT, record), 2)
            saved = Path(owned, "result.json").read_text()
            self.assertNotIn("FAKE_SENSITIVE_TOOL_OUTPUT", saved)
            self.assertEqual(json.loads(saved)["failedStage"], "DOCKER_BASE")

    def test_mocked_build_uses_exact_context_and_no_push(self):
        commands = []
        archive = io.BytesIO()
        with zipfile.ZipFile(archive, "w") as z:
            for name in (wp35.BOOTSTRAP, wp35.DOCKERFILE):
                z.writestr(name, (ROOT / name).read_bytes())
        with tempfile.TemporaryDirectory() as owned:
            def fake_run(args, cwd, timeout=30):
                commands.append(args)
                if args[:3] == ["docker", "image", "inspect"]:
                    if args[3] == wp35.BASE:
                        return json.dumps([dict(Os="linux", Architecture="amd64")]).encode()
                    return json.dumps([dict(Os="linux", Architecture="amd64", Id="sha256:" + "b" * 64,
                        Config=dict(User="1654:1654", Entrypoint=["/bin/sh", "/app/research-bootstrap.sh"],
                        ExposedPorts={"52731/tcp": {}}, Env=["AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0"]))]).encode()
                if args[:2] == ["git", "archive"]:
                    return archive.getvalue()
                if args[:2] == ["dotnet", "publish"]:
                    folder = Path(args[-1])
                    (folder / "AIQuantTradingResearch.Worker.dll").write_bytes(b"fake-assembly")
                    (folder / "AIQuantTradingResearch.Worker.runtimeconfig.json").write_text('{"runtimeOptions":{"tfm":"net10.0"}}', encoding="utf-8", newline="\n")
                    (folder / "libe_sqlite3.so").write_bytes(b"fake-native")
                return b""
            with patch.object(wp35.tempfile, "mkdtemp", return_value=owned), patch.object(wp35, "run", side_effect=fake_run):
                record = wp35.plan(COMMIT)
                self.assertEqual(wp35.build(ROOT, record), 0)
                self.assertEqual(record["result"], "LOCAL_IMAGE_BUILT_RUNTIME_UNQUALIFIED")
                self.assertIsNone(record["registryDigest"])
                self.assertEqual(record["health"], "UNVERIFIED")
            docker_build = next(c for c in commands if c[:2] == ["docker", "build"])
            self.assertIn("--pull=false", docker_build)
            self.assertIn("--network=none", docker_build)
            self.assertFalse(any("push" in c or "az" in c for c in commands))
            self.assertTrue(Path(owned, "context-sha256.json").is_file())
            self.assertEqual(set(p.name for p in Path(owned, "context").iterdir()), {"research", "container"})

    def test_powershell_entry_is_51_and_has_no_live_action(self):
        script = (ROOT / "eng/docker/release-1.15/Invoke-Wp35ResearchImage.ps1").read_text()
        self.assertIn("#Requires -Version 5.1", script)
        self.assertIn("[ValidateSet('Plan','Build')]", script)
        self.assertNotIn("ExecutionPolicy", script)
        self.assertNotIn("AIQ_RESEARCH_API_KEY", script)


if __name__ == "__main__":
    unittest.main()

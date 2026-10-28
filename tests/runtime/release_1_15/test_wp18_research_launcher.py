"""Offline gate tests; never change policy or invoke Azure/deployment/probes."""
import contextlib
import importlib.util
import io
import json
import os
from pathlib import Path
import subprocess
import unittest
from unittest import mock

ROOT = Path(__file__).resolve().parents[3]
SOURCE = ROOT / "eng/azure-cli/r1.15-qualification/wp18_research_launcher.py"
SPEC = importlib.util.spec_from_file_location("wp18_launcher", SOURCE)
M = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(M)


def policy(value="RemoteSigned"):
    scopes = dict.fromkeys(M.SCOPES, "Undefined")
    scopes["LocalMachine"] = value
    return {"version": "5.1.26100.9444", "effective": value, "scopes": scopes}


DRY = {"result": "LOCAL_PREFLIGHT_PASS", "PUBLIC_RUNTIME_VERIFIED": False}


class GateTests(unittest.TestCase):
    def gate(self, observations):
        with mock.patch.object(M.os, "name", "nt"), mock.patch.object(M.shutil, "which", side_effect=lambda x: x), mock.patch.object(M, "call", side_effect=observations) as calls:
            return M.preflight(), calls

    def test_precedence_each_scope_overrides_all_lower_scopes(self):
        for index, scope in enumerate(M.SCOPES):
            scopes = {k: ("Undefined" if i < index else "Unrestricted") for i, k in enumerate(M.SCOPES)}
            scopes[scope] = "Restricted"
            self.assertEqual(M.effective_policy(scopes), "Restricted")

    def test_undefined_windows_default_is_restricted(self):
        self.assertEqual(M.effective_policy(dict.fromkeys(M.SCOPES, "Undefined")), "Restricted")

    def test_unknown_or_missing_scope_rejected(self):
        for scopes in ({}, dict.fromkeys(M.SCOPES, "invented")):
            with self.assertRaises(M.PreflightBlocked): M.effective_policy(scopes)

    def test_real_route_and_offline_switch_required(self):
        result, calls = self.gate([policy(), DRY])
        self.assertEqual(result["result"], "LOCAL_PREFLIGHT_PASS")
        args = calls.call_args_list[1].args[0]
        self.assertEqual(args, ["powershell.exe", "-NoProfile", "-NonInteractive", "-File", str(M.SCRIPT), "-LocalPreflight"])
        self.assertNotIn("-ProbeResearch", args)

    def test_permissive_report_cannot_override_blocked_child(self):
        with self.assertRaises(M.PreflightBlocked):
            self.gate([policy(), M.PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")])

    def test_restricted_and_bypass_cannot_pass_even_with_claimed_dry_success(self):
        for value in ("Restricted", "Bypass"):
            with self.subTest(value=value), self.assertRaises(M.PreflightBlocked): self.gate([policy(value), DRY])

    def test_all_signed_requires_real_launch_success(self):
        result, _ = self.gate([policy("AllSigned"), DRY])
        self.assertEqual(result["effectivePolicy"], "AllSigned")
        with self.assertRaises(M.PreflightBlocked): self.gate([policy("AllSigned"), M.PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")])

    def test_inconsistent_policy_blocks_before_script(self):
        p = policy(); p["effective"] = "Unrestricted"
        with self.assertRaises(M.PreflightBlocked): self.gate([p])

    def test_wrong_powershell_version_rejected(self):
        p = policy(); p["version"] = "7.5.0"
        with self.assertRaises(M.PreflightBlocked): self.gate([p])

    def test_missing_python_or_powershell_blocks(self):
        for missing in ("python", "powershell.exe"):
            with mock.patch.object(M.os, "name", "nt"), mock.patch.object(M.shutil, "which", side_effect=lambda x: None if x == missing else x), mock.patch.object(M, "call") as call:
                with self.assertRaises(M.PreflightBlocked): M.preflight()
                call.assert_not_called()

    def test_invalid_offline_response_rejected(self):
        for dry in ({}, {**DRY, "PUBLIC_RUNTIME_VERIFIED": True}, {**DRY, "secret": "PRIVATE_CANARY"}):
            with self.assertRaises(M.PreflightBlocked): self.gate([policy(), dry])

    def test_every_preflight_failure_prevents_deploy(self):
        for reason in ("POLICY_UNAVAILABLE", "LOCAL_SCRIPT_LAUNCH_BLOCKED", "INTERPRETER_UNAVAILABLE", "INVALID_DRY_RUN"):
            deploy = mock.Mock()
            with mock.patch.object(M, "preflight", side_effect=M.PreflightBlocked(reason)):
                with self.assertRaises(M.PreflightBlocked): M.guarded_deployment(deploy)
            deploy.assert_not_called()

    def test_cli_cannot_launch_controller_when_gate_fails(self):
        with mock.patch.object(M, "preflight", side_effect=M.PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")), mock.patch.object(M.subprocess, "run") as run, contextlib.redirect_stdout(io.StringIO()):
            self.assertEqual(M.main(["--execute-controller", str(SOURCE)]), 1)
        run.assert_not_called()

    def test_success_gates_deployment_order_without_cache(self):
        order = []
        with mock.patch.object(M, "preflight", side_effect=lambda: order.append("preflight") or DRY):
            for _ in range(2): M.guarded_deployment(lambda: order.append("deploy"))
        self.assertEqual(order, ["preflight", "deploy", "preflight", "deploy"])

    def test_child_stderr_and_exceptions_are_redacted(self):
        for output in (subprocess.CompletedProcess([], 1, b"PRIVATE_CANARY", b"PRIVATE_CANARY"), subprocess.TimeoutExpired("PRIVATE_CANARY", 1), FileNotFoundError("PRIVATE_CANARY")):
            kwargs = {"side_effect": output} if isinstance(output, Exception) else {"return_value": output}
            with mock.patch.object(M.subprocess, "run", **kwargs):
                with self.assertRaises(M.PreflightBlocked) as caught: M.call(["fixed"], 15)
                self.assertNotIn("PRIVATE_CANARY", str(caught.exception))

    def test_default_is_preflight_only(self):
        with mock.patch.object(M, "preflight", return_value=DRY), mock.patch.object(M.subprocess, "run") as run, contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(M.main([]), 0)
        run.assert_not_called();self.assertEqual(json.loads(output.getvalue()), DRY)

    def test_cli_failure_has_actionable_sanitized_guidance(self):
        with mock.patch.object(M, "preflight", side_effect=M.PreflightBlocked("LOCAL_SCRIPT_LAUNCH_BLOCKED")), contextlib.redirect_stdout(io.StringIO()) as output:
            self.assertEqual(M.main([]), 1)
        value = json.loads(output.getvalue())
        self.assertIn("administrator", value["guidance"])
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])

    def test_no_policy_override_or_network_commands_in_gate(self):
        source = SOURCE.read_text()
        for forbidden in ("Set-ExecutionPolicy", '"-ExecutionPolicy"', "az webapp", "urllib", "http.client", "socket"):
            self.assertNotIn(forbidden, source)

    def test_real_supported_host_route_and_parser(self):
        # A restrictive policy is an expected safe refusal, not a skipped test.
        self.assertEqual(os.name, "nt", "This qualification requires the approved Windows host")
        command = "$t=$null;$e=$null;[Management.Automation.Language.Parser]::ParseFile('" + str(M.SCRIPT).replace("'", "''") + "',[ref]$t,[ref]$e)|Out-Null; @{errors=$e.Count;version=$PSVersionTable.PSVersion.ToString()}|ConvertTo-Json"
        p = subprocess.run(["powershell.exe", "-NoProfile", "-NonInteractive", "-Command", command], capture_output=True, text=True, timeout=15)
        self.assertEqual(p.returncode, 0)
        parsed = json.loads(p.stdout); self.assertEqual(parsed["errors"], 0);self.assertTrue(parsed["version"].startswith("5.1."))
        p = subprocess.run(["python", "-B", str(SOURCE)], capture_output=True, text=True, timeout=50)
        self.assertIn(p.returncode, (0, 1)); value = json.loads(p.stdout)
        self.assertIn(value["result"], ("LOCAL_PREFLIGHT_PASS", "PREFLIGHT_BLOCKED"))
        self.assertFalse(value["PUBLIC_RUNTIME_VERIFIED"])
        if p.returncode == 1:
            self.assertEqual(value["result"], "PREFLIGHT_BLOCKED")
            self.assertIn(value["reason"], ("LOCAL_SCRIPT_LAUNCH_BLOCKED", "POLICY_NOT_APPROVED", "INTERPRETER_UNAVAILABLE", "LOCAL_EXECUTION_UNAVAILABLE"))


if __name__ == "__main__":
    unittest.main()

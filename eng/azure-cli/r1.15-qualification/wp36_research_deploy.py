"""Separate research image and approved ten-setting actions; no secret output."""
import argparse
import datetime as dt
import hashlib
import json
import os
from pathlib import Path
import re
import shutil
import sys
import subprocess
import tempfile
import time
import urllib.error
import urllib.request

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
APP = "aiqr115researchstagingwus27f5eabb5"
BASE = f"/subscriptions/{SUB}/resourceGroups/{GROUP}/providers/Microsoft.Web/"
SITE = BASE + "sites/" + APP
PLAN = BASE + "serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5"
IMAGE = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f"
HEALTH = "https://" + APP + ".azurewebsites.net/health"
API = "2023-12-01"
SETTINGS = {
    "WEBSITES_PORT": "52731", "AIQ_RESEARCH_BOOTSTRAP_MODE": "serve",
    "AIQ_RESEARCH_BIND_ADDRESS": "0.0.0.0", "AIQ_RESEARCH_TRANSPORT": "azure-app-service-http",
    "AIQ_RESEARCH_PLATFORM_HTTPS_ONLY": "1", "AIQ_RESEARCH_PRIVATE_DIRECTORY": "/tmp/aiq-research",
    "AIQ_RESEARCH_STORAGE_SCOPE": "azure-f1-staging-ephemeral", "AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING": "1",
    "AIQ_RESEARCH_EXPECTED_MOUNT_PATH": "/", "WEBSITES_ENABLE_APP_SERVICE_STORAGE": "false",
}


class Blocked(Exception):
    def __init__(self, reason, code=None, http_status=None):
        super().__init__(reason)
        self.reason, self.code, self.http_status = reason, code, http_status


def require(condition, reason):
    if not condition:
        raise Blocked(reason)


def plan():
    return {
        "result": "PLAN_ONLY", "target": APP, "plan": PLAN.rsplit("/", 1)[-1], "image": IMAGE,
        "proposedAzureChanges": ["PATCH_RESEARCH_WEB_IMAGE_EMPTY_STARTUP", "PATCH_RESEARCH_HTTPS_ONLY_TRUE", "POST_RESEARCH_RESTART"],
        "ownerPortalSettings": dict(SETTINGS), "settingsApiWrites": False,
        "requiredOwnerConfirmations": ["EXACT_APPLY_APPROVAL", "KEY_NAME_PRESENT", "RUNTIME_SETTINGS_CORRECT_AND_UNRELATED_SETTINGS_PRESERVED", "ANONYMOUS_IMAGE_PULL_VERIFIED_BY_PREFLIGHT"],
        "registryPublished": True, "azureDeployed": False, "azureHealth": "UNVERIFIED",
        "liveAttemptReady": False, "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False,
    }


def cli_entrypoint():
    az = shutil.which("az.cmd")
    require(bool(az), "AZURE_CLI_UNAVAILABLE")
    interpreter = Path(az).resolve().parent.parent / "python.exe"
    require(interpreter.is_file(), "AZURE_CLI_UNAVAILABLE")
    # az.cmd delegates to this exact interpreter. Direct launch makes timeout kill
    # the CLI process itself, without leaving a cmd.exe child issuing a late write.
    return [str(interpreter), "-IBm", "azure.cli"]


class Azure:
    def request(self, method, resource=SITE, suffix="", body=None, query=None):
        allowed = {("get", SITE, ""), ("get", SITE, "/config/web"), ("get", PLAN, ""),
                   ("patch", SITE, "/config/web"), ("patch", SITE, ""), ("post", SITE, "/restart")}
        require((method, resource, suffix) in allowed, "OPERATION_NOT_ALLOWED")
        if method == "get":
            require(body is None and isinstance(query, str) and bool(query), "PROJECTED_METADATA_REQUIRED")
        elif suffix == "/config/web":
            require(body == {"properties": {"linuxFxVersion": "DOCKER|" + IMAGE, "appCommandLine": ""}}, "DELTA_NOT_ALLOWED")
        elif method == "patch":
            require(body == {"properties": {"httpsOnly": True}}, "DELTA_NOT_ALLOWED")
        else:
            require(body is None, "DELTA_NOT_ALLOWED")
        args = [*cli_entrypoint(), "rest", "--subscription", SUB, "--method", method, "--url", "https://management.azure.com" + resource + suffix + "?api-version=" + API, "--only-show-errors", "--output", "json" if method == "get" else "none"]
        if query:
            args += ["--query", query]
        if body is not None:
            args += ["--body", json.dumps(body)]  # Fixed non-secret properties only.
        try:
            result = subprocess.run(args, capture_output=True, timeout=25)
        except subprocess.TimeoutExpired:
            raise Blocked("AZURE_TIMEOUT") from None
        except OSError:
            raise Blocked("AZURE_CLI_UNAVAILABLE") from None
        if result.returncode:
            # Fixed status classification only; never raw CLI output/response/exception.
            match = re.search(rb"\b(?:Code: |\()([A-Za-z][A-Za-z0-9]{0,63})\)?", result.stderr)
            code = match.group(1).decode("ascii") if match else None
            if code not in {"BadRequest", "Forbidden", "Unauthorized", "NotFound", "Conflict", "AuthorizationFailed", "ResourceNotFound", "OperationNotAllowed", "RequestDisallowedByPolicy", "InternalServerError", "ServiceUnavailable"}:
                code = None
            raise Blocked("AZURE_REQUEST_REJECTED", code)
        if method != "get":
            return None
        try:
            return json.loads(result.stdout)
        except (ValueError, UnicodeError):
            raise Blocked("AZURE_METADATA_INVALID") from None

    def inspect(self, require_running=True):
        site = self.request("get", query="{id:id,kind:kind,location:location,plan:properties.serverFarmId,state:properties.state,httpsOnly:properties.httpsOnly}")
        plan_meta = self.request("get", resource=PLAN, query="{id:id,location:location,linux:properties.reserved,sku:sku.name}")
        web = self.request("get", suffix="/config/web", query="{empty:properties.linuxFxVersion == '',classic:starts_with(not_null(properties.linuxFxVersion, ''), 'DOCKER|'),sidecar:properties.linuxFxVersion == 'sitecontainers',candidate:properties.linuxFxVersion == 'DOCKER|" + IMAGE + "',startupEmpty:properties.appCommandLine == ''}")
        return validate_metadata(site, plan_meta, web, require_running=require_running)


def validate_metadata(site, plan_meta, web, require_running=True):
    require(isinstance(site, dict) and isinstance(plan_meta, dict) and isinstance(web, dict), "METADATA_INVALID")
    require(str(site.get("id", "")).lower() == SITE.lower() and str(site.get("plan", "")).lower() == PLAN.lower() and str(plan_meta.get("id", "")).lower() == PLAN.lower(), "EXACT_TARGET_PLAN_REQUIRED")
    require(site.get("kind") in ("app,linux", "app,linux,container") and plan_meta.get("linux") is True and plan_meta.get("sku") == "F1", "LINUX_F1_REQUIRED")
    require(all(re.sub("[^a-z0-9]", "", str(x).lower()) == "westus2" for x in (site.get("location"), plan_meta.get("location"))), "REGION_MISMATCH")
    require(type(site.get("httpsOnly")) is bool and site.get("state") in
            ("Running", "Stopped", "QuotaExceeded", "Starting", "Stopping", "Unknown"), "STATE_UNVERIFIED")
    if require_running:
        require(site["state"] != "QuotaExceeded", "AZURE_QUOTA_EXCEEDED")
        require(site["state"] == "Running", "STATE_UNVERIFIED")
    require(all(type(web.get(k)) is bool for k in ("empty", "classic", "sidecar", "candidate", "startupEmpty")), "HOSTING_UNVERIFIED")
    require(not web["sidecar"] and web["startupEmpty"] and (web["empty"] and not web["classic"] and not web["candidate"] or web["candidate"] and web["classic"] and not web["empty"]), "MODE_OR_STARTUP_INCOMPATIBLE")
    return {"target": APP, "resourceId": SITE, "plan": PLAN.rsplit("/", 1)[-1], "linux": True, "sku": "F1", "region": "West US 2",
            "state": site["state"], "mode": "EXPECTED_CLASSIC_IMAGE" if web["candidate"] else "EMPTY_CODE_MODE",
            "httpsOnly": site["httpsOnly"], "startupEmpty": True, "nonsecretSettingNames": sorted(SETTINGS)}


class NoRedirect(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None


def verify_registry():
    # Anonymous registry token, never an owner credential; no Docker login/config reads.
    opener = urllib.request.build_opener(NoRedirect())
    def fetch(url, token=None):
        headers = {"Accept": "application/vnd.oci.image.index.v1+json, application/vnd.oci.image.manifest.v1+json"}
        if token:
            headers["Authorization"] = "Bearer " + token
        with opener.open(urllib.request.Request(url, headers=headers), timeout=10) as response:
            require(response.status == 200, "REGISTRY_UNAVAILABLE")
            data = response.read(1048577)
            require(len(data) <= 1048576, "REGISTRY_RESPONSE_INVALID")
            return data
    try:
        token = json.loads(fetch("https://ghcr.io/token?scope=repository:samuel-santos-engineer/aiquanttradingresearch:pull&service=ghcr.io")).get("token")
        require(isinstance(token, str) and bool(token), "ANONYMOUS_PULL_UNAVAILABLE")
        prefix = "https://ghcr.io/v2/samuel-santos-engineer/aiquanttradingresearch/manifests/"
        digest = IMAGE.rsplit("@", 1)[1]
        raw = fetch(prefix + digest, token)
        require("sha256:" + hashlib.sha256(raw).hexdigest() == digest, "REGISTRY_DIGEST_MISMATCH")
        index = json.loads(raw)
        manifests = [x for x in index.get("manifests", []) if x.get("platform") == {"architecture": "amd64", "os": "linux"}]
        runtime = "sha256:fb9aab67753be84ca3300876fa4ed4d669ab0d897beb49f3fb8f2eae58b9b012"
        require(len(manifests) == 1 and manifests[0].get("digest") == runtime, "REGISTRY_PLATFORM_MISMATCH")
        manifest = fetch(prefix + runtime, token)
        require("sha256:" + hashlib.sha256(manifest).hexdigest() == runtime, "REGISTRY_DIGEST_MISMATCH")
        return {"anonymousPullMetadataVerified": True, "indexDigest": digest, "runtimeManifest": runtime, "platform": "linux/amd64"}
    except Blocked:
        raise
    except Exception:
        raise Blocked("ANONYMOUS_PULL_UNAVAILABLE") from None


def valid_health(status, body):
    return type(status) is int and status == 200 and body == b"Alive"



HEALTH_PROBE_CODE = r"""
import json, urllib.request, urllib.error
class NoRedirect(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, req, fp, code, msg, headers, newurl):
        return None
result = {"status": None, "bodyMatches": False}
try:
    request = urllib.request.Request("https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health", headers={"Accept": "text/plain"}, method="GET")
    with urllib.request.build_opener(NoRedirect()).open(request, timeout=8) as response:
        result = {"status": response.status, "bodyMatches": response.read(6) == b"Alive"}
except urllib.error.HTTPError as error:
    result["status"] = error.code
    error.close()
except (OSError, urllib.error.URLError):
    pass
print(json.dumps(result))
"""


def bounded_health_probe(timeout):
    require(0 < timeout <= 8, "HEALTH_REQUEST_BUDGET_INVALID")
    try:
        # Whole process includes DNS/TLS/header/body work; no shell or children.
        completed = subprocess.run([sys.executable, "-IB", "-c", HEALTH_PROBE_CODE],
                                   capture_output=True, timeout=timeout)
    except subprocess.TimeoutExpired:
        return None, b""
    except OSError:
        raise Blocked("HEALTH_PROBE_UNAVAILABLE") from None
    try:
        require(completed.returncode == 0, "HEALTH_PROBE_UNAVAILABLE")
        value = json.loads(completed.stdout)
        require(set(value) == {"status", "bodyMatches"} and type(value["bodyMatches"]) is bool and
                (value["status"] is None or type(value["status"]) is int), "HEALTH_PROBE_INVALID")
        return value["status"], b"Alive" if value["bodyMatches"] else b""
    except (ValueError, TypeError):
        raise Blocked("HEALTH_PROBE_INVALID") from None


def observe_health(budget=180, clock=time.monotonic, sleep=time.sleep, probe=bounded_health_probe):
    require(0 < budget <= 180, "HEALTH_BUDGET_INVALID")
    deadline = clock() + budget
    last_status = None
    while clock() < deadline:
        remaining = deadline - clock()
        if remaining <= 0:
            break
        status, body = probe(min(8, remaining))
        if status is not None:
            last_status = status
        if clock() <= deadline and valid_health(status, body):
            return {"status": 200, "body": "Alive"}
        if status in (301, 302, 303, 307, 308, 401, 403):
            raise Blocked("HEALTH_REJECTED", http_status=status)
        remaining = deadline - clock()
        if remaining > 0:
            sleep(min(2, remaining))
    raise Blocked("HEALTH_NOT_VERIFIED", http_status=last_status)


def execute(action, approved=False, key_present=False, settings_confirmed=False, azure=None, health=observe_health, registry=verify_registry):
    result = plan()
    if action == "Plan":
        return result, 0
    stage = "APPROVAL"
    mutated = False
    try:
        require(action in ("Inspect", "Apply"), "ACTION_INVALID")
        if action == "Apply":
            require(approved, "EXPLICIT_APPLY_APPROVAL_REQUIRED")
            require(key_present, "OWNER_KEY_NAME_CONFIRMATION_REQUIRED")
            require(settings_confirmed, "OWNER_RUNTIME_SETTINGS_CONFIRMATION_REQUIRED")

        stage = "ANONYMOUS_REGISTRY_PREFLIGHT"
        result["registryEvidence"] = registry()
        azure = azure or Azure()
        stage = "READ_ONLY_METADATA"
        snapshot = azure.inspect()
        result["snapshot"] = snapshot
        if action == "Inspect":
            result["result"] = "METADATA_VERIFIED_NO_MUTATION"
            return result, 0
        stage = "PRE_APPLY_DRIFT_CHECK"
        require(snapshot["mode"] == "EMPTY_CODE_MODE" and snapshot["httpsOnly"] is False, "PRE_APPLY_METADATA_DRIFT")
        stage = "SANITIZED_SNAPSHOT"
        directory = Path(tempfile.mkdtemp(prefix="aiq-wp36-snapshot-"))
        (directory / "before.json").write_text(json.dumps({"observedUtc": dt.datetime.now(dt.timezone.utc).isoformat(), **snapshot}, indent=2) + "\n", encoding="utf-8", newline="\n")
        result["sanitizedSnapshotSaved"] = True
        stage = "IMAGE_CONFIGURATION"
        mutated = True  # Timeout/rejection can leave uncertain state; never claim no mutation.
        azure.request("patch", suffix="/config/web", body={"properties": {"linuxFxVersion": "DOCKER|" + IMAGE, "appCommandLine": ""}})
        stage = "HTTPS_ONLY"
        azure.request("patch", body={"properties": {"httpsOnly": True}})
        stage = "RESTART"
        azure.request("post", suffix="/restart")
        stage = "CONFIGURATION_READBACK"
        current = azure.inspect()
        require(current["mode"] == "EXPECTED_CLASSIC_IMAGE" and current["httpsOnly"] is True, "CONFIGURATION_NOT_CONFIRMED")
        result["azureDeployed"] = True
        stage = "ANONYMOUS_HEALTH"
        result["healthEvidence"] = health()
        result.update(result="RESEARCH_HEALTH_VERIFIED", azureHealth="PASS")
        return result, 0
    except (Blocked, OSError, ValueError, KeyError, TypeError) as error:
        result.update(result="WP36_BLOCKED", failedStage=stage,
                      failureReason=error.reason if isinstance(error, Blocked) else "LOCAL_OR_METADATA_FAILURE",
                      azureErrorCode=error.code if isinstance(error, Blocked) else None,
                      healthHttpStatus=error.http_status if isinstance(error, Blocked) else None,
                      azureMayHaveChanged=mutated, manualRecoveryRequired=mutated)
        if mutated:
            try:
                result["currentState"] = azure.inspect(require_running=False)
            except Exception:
                result["currentState"] = "UNVERIFIED"
        return result, 2



def settings_plan():
    return {"result": "SETTINGS_PLAN_ONLY", "target": APP, "subscription": SUB,
            "settingNames": sorted(SETTINGS), "saveInvocations": 0, "settingsSaved": False,
            "imageDeployInvoked": False, "explicitRestartInvoked": False, "healthProbed": False,
            "liveAttemptReady": False, "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False}


def verify_installed_merge():
    # Installed CLI code only, fake data, no authentication/network or real settings.
    import azure.cli.core
    from azure.cli.command_modules.appservice import custom
    from types import SimpleNamespace
    from unittest.mock import patch
    require(azure.cli.core.__version__ == "2.89.1", "SETTINGS_PRESERVATION_UNVERIFIED")
    baseline = {"AIQ_RESEARCH_API_KEY": "FAKE_KEY", "unrelated": "FAKE_UNRELATED", "WEBSITES_PORT": "old"}
    state = SimpleNamespace(properties=dict(baseline))
    writes = []
    def read(*args):
        require(args[3] == "list_application_settings", "SETTINGS_PRESERVATION_UNVERIFIED")
        return state
    def write(*args):
        require(args[3] == "update_application_settings", "SETTINGS_PRESERVATION_UNVERIFIED")
        writes.append(dict(args[4].properties))
        return args[4]
    # No slot API is permitted by this fake client. --settings must leave slot_result empty.
    client = SimpleNamespace(web_apps=object())
    with patch.object(custom, "_generic_site_operation", read), patch.object(custom, "_generic_settings_operation", write), patch.object(custom, "web_client_factory", return_value=client), patch.object(custom, "is_centauri_functionapp", return_value=False):
        custom.update_app_settings(SimpleNamespace(cli_ctx=object()), GROUP, APP,
                                   settings=[k + "=" + v for k, v in SETTINGS.items()])
    expected = dict(baseline); expected.update(SETTINGS)
    require(writes == [expected], "SETTINGS_PRESERVATION_UNVERIFIED")
    return azure.cli.core.__version__


class SettingsAdapter:
    def __init__(self):
        # Only instantiated in the installed CLI interpreter, after explicit settings approval.
        import logging
        logging.disable(logging.CRITICAL)  # Worker-local only: no SDK body/error logging.
        from azure.cli.core import get_default_cli
        from azure.cli.core.commands.client_factory import get_mgmt_service_client
        from azure.cli.core.profiles import ResourceType
        self.client = get_mgmt_service_client(get_default_cli(), ResourceType.MGMT_APPSERVICE,
            subscription_id=SUB, retry_total=0, connection_timeout=10, read_timeout=10)

    def metadata(self):
        return Azure().inspect()  # Exact identity/plan/mode; no registry/health calls.

    def private_settings(self):
        # Real values stay in this worker RAM, never on stdout, files or interprocess JSON.
        response = self.client.web_apps.list_application_settings(GROUP, APP, logging_enable=False)
        values = response.properties
        require(isinstance(values, dict) and all(isinstance(k, str) and (v is None or isinstance(v, str)) for k, v in values.items()), "SETTINGS_PRESERVATION_UNVERIFIED")
        return dict(values)

    def flags(self):
        value = self.client.web_apps.list_slot_configuration_names(GROUP, APP, logging_enable=False)
        result = {name: getattr(value, name) for name in ("app_setting_names", "connection_string_names", "azure_storage_config_names")}
        require(all(v is None or isinstance(v, list) and all(isinstance(x, str) for x in v) for v in result.values()), "SETTINGS_PRESERVATION_UNVERIFIED")
        return result  # Preserve null/list and order; no normalization.

    def save(self):
        args = [sys.executable, "-IBm", "azure.cli", "webapp", "config", "appsettings", "set",
                "--subscription", SUB, "--resource-group", GROUP, "--name", APP,
                "--settings", *[k + "=" + v for k, v in SETTINGS.items()],
                "--only-show-errors", "--output", "none"]
        # Direct installed CLI Python equals az.cmd's entrypoint, without a cmd child
        # surviving a timeout. Only nonsecret arguments; no --slot-settings or JSON flags.
        try:
            completed = subprocess.run(args, capture_output=True, timeout=90, env={**os.environ,
                "AZURE_CORE_COLLECT_TELEMETRY": "false", "AZURE_LOGGING_ENABLE_LOG_FILE": "false",
                "AZURE_CORE_ONLY_SHOW_ERRORS": "true", "AZURE_LOG_LEVEL": "critical"})
        except subprocess.TimeoutExpired:
            raise Blocked("SETTINGS_SAVE_TIMEOUT_UNCERTAIN") from None
        except OSError:
            raise Blocked("SETTINGS_SAVE_UNAVAILABLE") from None
        require(completed.returncode == 0, "SETTINGS_SAVE_FAILED_OR_UNCERTAIN")
        # Never forward or persist stdout/stderr, even on failure.


def execute_settings(approved=False, adapter=None, cli_probe=verify_installed_merge):
    result = settings_plan()
    before = after = None
    stage = "SETTINGS_APPROVAL"
    attempted = False
    try:
        require(approved, "EXPLICIT_SETTINGS_APPROVAL_REQUIRED")
        stage = "INSTALLED_CLI_MERGE_PROOF"
        result["cliVersion"] = cli_probe()
        adapter = adapter or SettingsAdapter()
        stage = "TARGET_PREFLIGHT"
        metadata = adapter.metadata()
        stage = "PRIVATE_PRESERVATION_PREFLIGHT"
        before = adapter.private_settings()
        flags = adapter.flags()
        require("AIQ_RESEARCH_API_KEY" in before and isinstance(before["AIQ_RESEARCH_API_KEY"], str) and bool(before["AIQ_RESEARCH_API_KEY"]), "KEY_NAME_ABSENT_OR_UNVERIFIED")
        # An orphan sticky name would make a newly created --settings entry sticky.
        require(not (set(SETTINGS) - set(before)) & set(flags["app_setting_names"] or []), "SETTINGS_PRESERVATION_UNVERIFIED")
        stage = "SETTINGS_SAVE"
        attempted = True
        result["saveInvocations"] = 1
        adapter.save()  # Exactly once, no automatic retry or rollback.
        result["saveConfirmed"] = True
        stage = "PRIVATE_POST_SAVE_VERIFICATION"
        after = adapter.private_settings()
        checks = {
            "tenValuesVerified": all(after.get(k) == v for k, v in SETTINGS.items()),
            "keyNamePresent": "AIQ_RESEARCH_API_KEY" in after,
            "keyValueUnchanged": before["AIQ_RESEARCH_API_KEY"] == after.get("AIQ_RESEARCH_API_KEY"),
            "unrelatedEntriesUnchanged": {k: v for k, v in before.items() if k not in SETTINGS} == {k: v for k, v in after.items() if k not in SETTINGS},
            "exactEntryNamesPreserved": set(after) == set(before) | set(SETTINGS),
            "slotFlagsUnchanged": flags == adapter.flags(),
            "hostingHttpsPlanUnchanged": metadata == adapter.metadata(),
        }
        result["checks"] = checks
        require(all(checks.values()), "SETTINGS_PRESERVATION_UNVERIFIED")
        result.update(result="settings saved", settingsSaved=True)
        return result, 0
    except Exception as error:
        result.update(result="SETTINGS_BLOCKED", failedStage=stage,
            failureReason=error.reason if isinstance(error, Blocked) else "SETTINGS_PRESERVATION_UNVERIFIED",
            azureMayHaveChanged=attempted, automaticRetry=False)
        return result, 2
    finally:
        # Best-effort reference clearing, not a guarantee of cryptographic memory erasure.
        if isinstance(before, dict):
            before.clear()
        if isinstance(after, dict):
            after.clear()


def launch_settings_worker():
    # No sensitive values pass between processes. The worker prints only sanitized results.
    az = shutil.which("az.cmd")
    if not az:
        return {**settings_plan(), "result": "SETTINGS_BLOCKED", "failureReason": "INSTALLED_CLI_UNAVAILABLE"}, 2
    interpreter = Path(az).resolve().parent.parent / "python.exe"
    try:
        completed = subprocess.run([str(interpreter), "-IB", str(Path(__file__).resolve()),
            "--action", "SettingsApply", "--approve-research-settings-save", "--settings-worker"],
            capture_output=True, timeout=480)
        result = json.loads(completed.stdout)
        # Child is the same reviewed implementation; never echo unexpected raw output.
        require(isinstance(result, dict) and result.get("result") in ("settings saved", "SETTINGS_BLOCKED"), "SETTINGS_WORKER_UNVERIFIED")
        return result, completed.returncode
    except Exception:
        return {**settings_plan(), "result": "SETTINGS_BLOCKED", "failureReason": "SETTINGS_WORKER_UNVERIFIED_NO_RETRY", "azureMayHaveChanged": True}, 2


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--action", choices=("Plan", "Inspect", "Apply", "SettingsPlan", "SettingsApply"), default="Plan")
    parser.add_argument("--approve-exact-research-deploy", action="store_true")
    parser.add_argument("--owner-key-present", action="store_true")
    parser.add_argument("--owner-runtime-settings-confirmed", action="store_true")
    parser.add_argument("--approve-research-settings-save", action="store_true")
    parser.add_argument("--settings-worker", action="store_true", help=argparse.SUPPRESS)
    args = parser.parse_args(argv)
    if args.action == "SettingsPlan":
        result, code = settings_plan(), 0
    elif args.action == "SettingsApply":
        if not args.approve_research_settings_save:
            result, code = execute_settings(False)
        elif args.settings_worker:
            result, code = execute_settings(True)
        else:
            result, code = launch_settings_worker()
    else:
        result, code = execute(args.action, args.approve_exact_research_deploy, args.owner_key_present,
                               args.owner_runtime_settings_confirmed)
    print(json.dumps(result))
    return code


if __name__ == "__main__":
    raise SystemExit(main())

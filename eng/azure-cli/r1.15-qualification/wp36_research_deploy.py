"""Small research-only deploy path. No settings/credential reads or settings writes."""
import argparse
import datetime as dt
import hashlib
import json
from pathlib import Path
import re
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
    def __init__(self, reason, code=None):
        super().__init__(reason)
        self.reason, self.code = reason, code


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
        args = ["az.cmd", "rest", "--method", method, "--url", "https://management.azure.com" + resource + suffix + "?api-version=" + API, "--only-show-errors", "--output", "json" if method == "get" else "none"]
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

    def inspect(self):
        site = self.request("get", query="{id:id,kind:kind,location:location,plan:properties.serverFarmId,state:properties.state,httpsOnly:properties.httpsOnly}")
        plan_meta = self.request("get", resource=PLAN, query="{id:id,location:location,linux:properties.reserved,sku:sku.name}")
        web = self.request("get", suffix="/config/web", query="{empty:properties.linuxFxVersion == '',classic:starts_with(not_null(properties.linuxFxVersion, ''), 'DOCKER|'),sidecar:properties.linuxFxVersion == 'sitecontainers',candidate:properties.linuxFxVersion == 'DOCKER|" + IMAGE + "',startupEmpty:properties.appCommandLine == ''}")
        return validate_metadata(site, plan_meta, web)


def validate_metadata(site, plan_meta, web):
    require(isinstance(site, dict) and isinstance(plan_meta, dict) and isinstance(web, dict), "METADATA_INVALID")
    require(str(site.get("id", "")).lower() == SITE.lower() and str(site.get("plan", "")).lower() == PLAN.lower() and str(plan_meta.get("id", "")).lower() == PLAN.lower(), "EXACT_TARGET_PLAN_REQUIRED")
    require(site.get("kind") in ("app,linux", "app,linux,container") and plan_meta.get("linux") is True and plan_meta.get("sku") == "F1", "LINUX_F1_REQUIRED")
    require(all(re.sub("[^a-z0-9]", "", str(x).lower()) == "westus2" for x in (site.get("location"), plan_meta.get("location"))), "REGION_MISMATCH")
    require(type(site.get("httpsOnly")) is bool and site.get("state") == "Running", "STATE_UNVERIFIED")
    require(all(type(web.get(k)) is bool for k in ("empty", "classic", "sidecar", "candidate", "startupEmpty")), "HOSTING_UNVERIFIED")
    require(not web["sidecar"] and web["startupEmpty"] and (web["empty"] and not web["classic"] and not web["candidate"] or web["candidate"] and web["classic"] and not web["empty"]), "MODE_OR_STARTUP_INCOMPATIBLE")
    return {"target": APP, "resourceId": SITE, "plan": PLAN.rsplit("/", 1)[-1], "linux": True, "sku": "F1", "region": "West US 2",
            "state": "Running", "mode": "EXPECTED_CLASSIC_IMAGE" if web["candidate"] else "EMPTY_CODE_MODE",
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


def observe_health(budget=180, clock=time.monotonic, sleep=time.sleep, opener=None):
    require(0 < budget <= 180, "HEALTH_BUDGET_INVALID")
    opener = opener or urllib.request.build_opener(NoRedirect())  # Normal TLS validation.
    deadline = clock() + budget
    last_status = None
    while clock() < deadline:
        remaining = deadline - clock()
        if remaining <= 0:
            break
        try:
            request = urllib.request.Request(HEALTH, headers={"Accept": "text/plain"}, method="GET")
            with opener.open(request, timeout=min(8, remaining)) as response:
                last_status = response.status
                body = response.read(6)
                if clock() <= deadline and valid_health(last_status, body):
                    return {"status": 200, "body": "Alive"}
        except urllib.error.HTTPError as error:
            last_status = error.code
            error.close()
            if last_status in (301, 302, 303, 307, 308, 401, 403):
                raise Blocked("HEALTH_REJECTED") from None
        except (OSError, urllib.error.URLError):
            pass
        remaining = deadline - clock()
        if remaining > 0:
            sleep(min(2, remaining))
    raise Blocked("HEALTH_NOT_VERIFIED")


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
                      azureMayHaveChanged=mutated, manualRecoveryRequired=mutated)
        if mutated:
            try:
                result["currentState"] = azure.inspect()
            except Exception:
                result["currentState"] = "UNVERIFIED"
        return result, 2


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--action", choices=("Plan", "Inspect", "Apply"), default="Plan")
    parser.add_argument("--approve-exact-research-deploy", action="store_true")
    parser.add_argument("--owner-key-present", action="store_true")
    parser.add_argument("--owner-runtime-settings-confirmed", action="store_true")
    args = parser.parse_args(argv)
    result, code = execute(args.action, args.approve_exact_research_deploy, args.owner_key_present,
                           args.owner_runtime_settings_confirmed)
    print(json.dumps(result))
    return code


if __name__ == "__main__":
    raise SystemExit(main())

"""Offline staging command planner. Never executes Docker, Azure or HTTP."""
import argparse
import json
import re

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
PLAN = "asp-aiq-r112-wp05-wus2-7f5eabb5"
REPO = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch"
UI_ID = "sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69"
API_DIGEST = "sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3"
API_APP = "aiqr115researchstagingwus27f5eabb5"
UI_APP = "aiqr112wp05wus27f5eabb5"
TAG = REPO + ":wp75-wp61-3e323b0d3008"
API_URL = "https://" + API_APP + ".azurewebsites.net"
SETTINGS = {
    "API": {"WEBSITES_PORT": "52731", "AIQ_RESEARCH_BOOTSTRAP_MODE": "serve",
            "AIQ_RESEARCH_BIND_ADDRESS": "0.0.0.0", "AIQ_RESEARCH_TRANSPORT": "azure-app-service-http",
            "AIQ_RESEARCH_PLATFORM_HTTPS_ONLY": "1", "AIQ_RESEARCH_PRIVATE_DIRECTORY": "/tmp/aiq-research",
            "AIQ_RESEARCH_STORAGE_SCOPE": "azure-f1-staging-ephemeral",
            "AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING": "1", "AIQ_RESEARCH_EXPECTED_MOUNT_PATH": "/",
            "WEBSITES_ENABLE_APP_SERVICE_STORAGE": "false"},
    "UI": {"WEBSITES_PORT": "8501", "AIQ_RESEARCH_CHART": "1", "AIQ_RESEARCH_BASE_URL": API_URL},
}


def digest(value):
    if not isinstance(value, str) or not re.fullmatch(r"sha256:[a-f0-9]{64}", value):
        raise ValueError("IMMUTABLE_SHA256_REQUIRED")
    return value


def azure(method, app, suffix="", properties=None):
    resource = ("/subscriptions/" + SUB + "/resourceGroups/" + GROUP +
                "/providers/Microsoft.Web/sites/" + app)
    argv = ["az", "rest", "--subscription", SUB, "--method", method,
            "--url", "https://management.azure.com" + resource + suffix + "?api-version=2023-12-01",
            "--only-show-errors", "--output", "none"]
    if properties is not None:
        argv += ["--body", json.dumps({"properties": properties}, separators=(",", ":"))]
    return argv


def image_commands(role, reference):
    app = API_APP if role == "API" else UI_APP
    return [azure("patch", app, "/config/web", {"linuxFxVersion": "DOCKER|" + reference, "appCommandLine": ""}),
            azure("patch", app, properties={"httpsOnly": True})]


def commands(role, ui_digest=None):
    if role not in ("API", "UI"): raise ValueError("EXACT_ROLE_REQUIRED")
    candidate = API_DIGEST if role == "API" else digest(ui_digest)
    if role == "UI" and candidate == UI_ID:
        raise ValueError("LOCAL_ID_IS_NOT_VERIFIED_REGISTRY_MAPPING")
    app = API_APP if role == "API" else UI_APP
    settings = ["az", "webapp", "config", "appsettings", "set", "--subscription", SUB,
                "--resource-group", GROUP, "--name", app, "--settings"]
    settings += [key + "=" + value for key, value in SETTINGS[role].items()]
    settings += ["--only-show-errors", "--output", "none"]
    # Merge selected nonsecret settings; never replace the full collection or touch slot flags.
    return [settings, *image_commands(role, REPO + "@" + candidate)]


def plan(phase="Plan", ui_digest=None, role=None, rollback_reference=None):
    result = {"result": "PLAN_ONLY", "phase": phase, "executionImplemented": False,
              "requiresSeparateLiveApproval": True, "azureOperations": 0, "dockerOperations": 0,
              "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False,
              "sequence": ["VERIFY_OR_PUBLISH_UI_MAPPING", "API", "API_AUTHENTIC_DATA_ACCEPTANCE", "UI", "BTC_ETH_BROWSER_ACCEPTANCE"],
              "blockers": ["UI_IMMUTABLE_MAPPING_UNVERIFIED", "FRESH_QUOTA_AND_ROLLBACK_BASELINE_REQUIRED",
                           "SAME_RETAINED_SERVICE_KEY_ON_BOTH_APPS_REQUIRED", "AZURE_PRIVATE_DATA_STATE_UNVERIFIED"],
              "commandArgv": []}
    if phase == "Publish":
        result["commandArgv"] = [["docker", "tag", UI_ID, TAG], ["docker", "push", TAG]]
        result["next"] = "VERIFY_REGISTRY_INDEX_CHILD_CONFIG_AND_ACCEPTED_LOCAL_MAPPING_BEFORE_UI"
    elif phase in ("API", "UI"):
        result["commandArgv"] = commands(phase, ui_digest)
        result["next"] = "BOUNDED_HEALTH_THEN_AUTHENTICATED_ACCEPTANCE_NO_AUTOMATIC_RESTART"
    elif phase == "Rollback":
        if role not in ("API", "UI"): raise ValueError("EXACT_ROLE_REQUIRED")
        if not isinstance(rollback_reference, str) or not rollback_reference.startswith(REPO + "@"):
            raise ValueError("VERIFIED_BASELINE_REFERENCE_REQUIRED")
        digest(rollback_reference[len(REPO) + 1:])
        result["commandArgv"] = image_commands(role, rollback_reference)
        # HTTPS restoration belongs to the actual captured baseline, not a hardcoded assumption.
        result["commandArgv"] = result["commandArgv"][:1]
        result["next"] = "RESTORE_ONLY_CAPTURED_SETTING_AND_HTTPS_DELTAS_USING_PRIVATE_BASELINE"
    elif phase != "Plan": raise ValueError("UNKNOWN_PHASE")
    return result


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--phase", choices=("Plan", "Publish", "API", "UI", "Rollback"), default="Plan")
    parser.add_argument("--ui-digest")
    parser.add_argument("--role", choices=("API", "UI"))
    parser.add_argument("--rollback-reference")
    args = parser.parse_args(argv)
    try:
        value = plan(args.phase, args.ui_digest, args.role, args.rollback_reference)
    except ValueError as error:
        # Only fixed internally authored reasons; never echo inputs.
        value = {"result": "BLOCKED", "reason": str(error), "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
        print(json.dumps(value)); return 2
    print(json.dumps(value, sort_keys=True)); return 0


if __name__ == "__main__": raise SystemExit(main())

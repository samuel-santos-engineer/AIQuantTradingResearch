"""Offline setting-name/provenance parser. No CLI, authentication or network adapter."""
import argparse
import hashlib
import json
import re

SUB = "7f5eabb5-80f5-4369-88be-b14a6de763f1"
GROUP = "rg-aiq-r112-wp05-wus2-7f5eabb5"
BASE = f"/subscriptions/{SUB}/resourceGroups/{GROUP}/providers/Microsoft.Web/sites/"
APPS = {"api": "aiqr115researchstagingwus27f5eabb5", "ui": "aiqr112wp05wus27f5eabb5"}
NAMES = {
    "api": ("WEBSITES_PORT", "AIQ_RESEARCH_PRIVATE_DIRECTORY", "AIQ_RESEARCH_API_KEY", "AIQ_RESEARCH_TRANSPORT",
            "AIQ_RESEARCH_PLATFORM_HTTPS_ONLY", "AIQ_RESEARCH_BIND_ADDRESS", "AIQ_RESEARCH_STORAGE_SCOPE",
            "AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING", "AIQ_RESEARCH_EXPECTED_MOUNT_PATH", "WEBSITES_ENABLE_APP_SERVICE_STORAGE",
            "AIQ_RESEARCH_BOOTSTRAP_MODE", "AIQ_RESEARCH_TLS_PFX", "AIQ_RESEARCH_TLS_PASSWORD"),
    "ui": ("WEBSITES_PORT", "AIQ_RESEARCH_BASE_URL", "AIQ_RESEARCH_API_KEY", "AIQ_RESEARCH_CHART", "AIQ_RESEARCH_CA_FILE")}
REPO = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch"
UI_CONFIGURED = "sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07"
UI_LOCAL = "sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69"
LIMIT = 1048576


def keys(role):
    if role not in APPS:
        raise ValueError("ROLE_NOT_ALLOWED")
    return NAMES[role]


def unknown(role):
    return {name: "UNKNOWN" for name in keys(role)}


def parse_presence(raw, role):
    """Only dictionary keys inspected, never setting values; no raw parser errors escape."""
    names = keys(role)
    def unique(pairs):
        result = {}
        for key, value in pairs:
            if key in result:
                raise ValueError("DUPLICATE_FIELD")
            result[key] = value
        return result
    try:
        if not isinstance(raw, bytes) or len(raw) > LIMIT:
            return unknown(role)
        doc = json.loads(raw, object_pairs_hook=unique)
        expected = BASE + APPS[role] + "/config/appsettings"
        if not isinstance(doc, dict) or not isinstance(doc.get("id"), str) or doc["id"].lower() != expected.lower():
            return unknown(role)
        properties = doc.get("properties")
        if not isinstance(properties, dict):
            return unknown(role)
        return {name: "PRESENT" if name in properties else "ABSENT" for name in names}
    except (ValueError, UnicodeError, TypeError):
        return unknown(role)


def compare_digest(configured, expected_child=None):
    """Compare platform digests only; a local ID/index is not a platform digest mapping."""
    valid = lambda d: isinstance(d, str) and re.fullmatch(r"sha256:[a-f0-9]{64}", d) is not None
    if not valid(configured) or not valid(expected_child):
        return "UNVERIFIED"
    return "MATCH" if configured == expected_child else "MISMATCH"


def map_index_child(index_raw, index_digest, child_raw):
    valid = lambda d: isinstance(d, str) and re.fullmatch(r"sha256:[a-f0-9]{64}", d) is not None
    try:
        if not all(isinstance(b, bytes) and len(b) <= LIMIT for b in (index_raw, child_raw)) or not valid(index_digest):
            return "UNVERIFIED"
        if "sha256:" + hashlib.sha256(index_raw).hexdigest() != index_digest:
            return "UNVERIFIED"
        index = json.loads(index_raw)
        children = [x for x in index.get("manifests", []) if x.get("platform", {}).get("os") == "linux" and x.get("platform", {}).get("architecture") == "amd64"]
        if len(children) != 1 or not valid(children[0].get("digest")):
            return "UNVERIFIED"
        digest = children[0]["digest"]
        if "sha256:" + hashlib.sha256(child_raw).hexdigest() != digest:
            return "UNVERIFIED"
        child = json.loads(child_raw)
        if child.get("schemaVersion") != 2 or not isinstance(child.get("layers"), list):
            return "UNVERIFIED"
        return digest
    except (ValueError, UnicodeError, AttributeError, TypeError):
        return "UNVERIFIED"


def procedure():
    return {"result": "PREPARATION_ONLY", "azureCalls": 0, "liveAdapterImplemented": False,
            "proposedReads": [{"role": role, "method": "POST_READ_ONLY_LIST", "resource": BASE + app + "/config/appsettings/list",
                               "apiVersion": "2023-12-01", "allowlistedNames": list(NAMES[role]), "timeoutSeconds": 15,
                               "responseCapBytes": LIMIT, "approval": "SEPARATE_EXPLICIT_APPROVAL_REQUIRED"} for role, app in APPS.items()],
            "uiProvenance": compare_digest(UI_CONFIGURED), "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.parse_args(argv)
    print(json.dumps(procedure(), sort_keys=True))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

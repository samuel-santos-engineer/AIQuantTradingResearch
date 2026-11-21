"""Offline projection and exact command construction; no default daemon operation."""
import argparse
import json
import re
from wp64_oci_provenance import WP61

CONTEXT = "desktop-linux"
ENDPOINT = "npipe:////./pipe/dockerDesktopLinuxEngine"
# Minimal first-stage identity projection: no nested Config traversal or toolchain requirement.
# Docker formatter semantics remain unverified until a separately authorized real inspection.
FORMAT = '{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}}}'

LIMIT = 1048576


def command(context):
    if context != CONTEXT: raise ValueError("CONTEXT_UNVERIFIED")
    return ["docker", "--context", context, "image", "inspect", WP61, "--format", FORMAT]


def project(raw):
    safe = {"result": "UNVERIFIED", "localIdentityKind": "LOCAL_DOCKER_IMAGE_ID",
            "configDigest": "UNKNOWN", "runtimeManifestDigest": "UNKNOWN", "ociIndexDigest": "UNKNOWN",
            "publication": "UNVERIFIED", "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    try:
        if not isinstance(raw, bytes) or len(raw) > LIMIT: return safe
        def unique(pairs):
            out = {}
            for key, value in pairs:
                if key in out: raise ValueError("DUPLICATE")
                out[key] = value
            return out
        d = json.loads(raw, object_pairs_hook=unique)
        if not isinstance(d, dict): return safe
        if d.get("Id") != WP61:
            safe["reason"] = "WRONG_IMAGE_ID"; return safe
        safe["localId"] = WP61
        checks = {name: "UNVERIFIED" for name in ("linuxAmd64", "user12002", "exposes8501", "streamlitEntrypoint", "listens8501", "bindsAllInterfaces")}
        for name in ("Os", "Architecture", "User"):
            if d.get(name) is not None and not isinstance(d[name], str): return safe
        if d.get("Os") is not None and d.get("Architecture") is not None:
            checks["linuxAmd64"] = d["Os"] == "linux" and d["Architecture"] == "amd64"
            safe["os"] = "linux" if d["Os"] == "linux" else "OTHER"
            safe["architecture"] = "amd64" if d["Architecture"] == "amd64" else "OTHER"
        if d.get("User") is not None:
            checks["user12002"] = d["User"] in {"12002", "12002:12002"}
            safe["user"] = d["User"] if checks["user12002"] else "OTHER"
        ports = d.get("ExposedPorts")
        if ports is not None:
            if not isinstance(ports, dict) or any(not isinstance(name, str) or not re.fullmatch(r"[0-9]{1,5}/(?:tcp|udp)", name) or not 1 <= int(name.split("/")[0]) <= 65535 for name in ports): return safe
            checks["exposes8501"] = "8501/tcp" in ports
            safe["portNames"] = sorted(ports)
        entry, cmd = d.get("Entrypoint"), d.get("Cmd")
        if cmd is None: cmd = []
        if not isinstance(cmd, list) or not all(isinstance(x, str) for x in cmd): return safe
        if entry is not None:
            if not isinstance(entry, list) or not all(isinstance(x, str) for x in entry): return safe
            prefix = ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py"]
            checks["streamlitEntrypoint"] = entry[:6] == prefix
            args = entry[6:] + cmd
            def flag(name, expected):
                values = [x.partition("=")[2] for x in args if x.startswith(name + "=")]
                if name in args or len(values) != 1: return "UNVERIFIED"
                return values[0] == expected
            checks["listens8501"] = flag("--server.port", "8501")
            checks["bindsAllInterfaces"] = flag("--server.address", "0.0.0.0")
        refs = d.get("RepoDigests")
        if refs is not None:
            if not isinstance(refs, list) or not all(isinstance(x, str) and re.fullmatch(r"ghcr[.]io/samuel-santos-engineer/aiquanttradingresearch@sha256:[a-f0-9]{64}", x) for x in refs): return safe
            safe["safeRepoDigests"] = refs
        safe["contract"] = checks
        if any(value is False for value in checks.values()): safe["result"] = "MISMATCH"
        elif all(value is True for value in checks.values()) and refs is not None: safe["result"] = "MATCH"
    except (ValueError, UnicodeError, TypeError, AttributeError, RecursionError):
        pass
    return safe


REPODIGEST_FORMAT = '{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}'
MAX_REPODIGESTS = 16
# Conservative subset: dotted lowercase DNS registry, no port/userinfo/tag,
# lowercase alphanumeric repository components with single internal separators.
# Syntactic acceptance is not a registry lookup or publication attestation.
IMMUTABLE_REFERENCE = re.compile(
    r"(?=.{1,1024}\Z)(?:[a-z0-9]+(?:-[a-z0-9]+)*[.])+"
    r"[a-z][a-z0-9]*(?:-[a-z0-9]+)*/"
    r"[a-z0-9]+(?:[._-][a-z0-9]+)*"
    r"(?:/[a-z0-9]+(?:[._-][a-z0-9]+)*)*@sha256:[a-f0-9]{64}"
)
EXPECTED_REFERENCE = re.compile(
    r"ghcr[.]io/samuel-santos-engineer/aiquanttradingresearch@sha256:[a-f0-9]{64}"
)

# Offline syntax recognition only, not an authoritative Docker/OCI validator.
# Keep explicit ports outside the supported/emittable grammar. Require the same
# dotted lowercase DNS/repository subset and a canonical decimal TCP port.
PORT_REFERENCE = re.compile(
    r"(?=.{1,1024}\Z)((?:[a-z0-9]+(?:-[a-z0-9]+)*[.])+"
    r"[a-z][a-z0-9]*(?:-[a-z0-9]+)*):([1-9][0-9]{0,4})/"
    r"[a-z0-9]+(?:[._-][a-z0-9]+)*"
    r"(?:/[a-z0-9]+(?:[._-][a-z0-9]+)*)*@sha256:[a-f0-9]{64}"
)


def reference_category(ref):
    if not isinstance(ref, str): return "UNEXPECTED_VALUE_TYPE"
    port_shape = PORT_REFERENCE.fullmatch(ref)
    if port_shape and int(port_shape.group(2)) <= 65535:
        return "UNSUPPORTED_REFERENCE_SHAPE"
    if not IMMUTABLE_REFERENCE.fullmatch(ref): return "MALFORMED_REFERENCE"
    if EXPECTED_REFERENCE.fullmatch(ref): return "EXPECTED_REPOSITORY_DIGEST"
    return "OTHER_REPOSITORY_DIGEST"


def repodigest_command(context):
    if context != CONTEXT: raise ValueError("CONTEXT_UNVERIFIED")
    return ["docker", "--context", context, "image", "inspect", WP61, "--format", REPODIGEST_FORMAT]


def project_repodigests(raw):
    safe = {"result": "UNVERIFIED", "repoDigestState": "UNKNOWN", "ociProvenance": "UNVERIFIED",
            "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    try:
        if not isinstance(raw, bytes) or len(raw) > LIMIT: return safe
        def unique(pairs):
            out = {}
            for key, value in pairs:
                if key in out: raise ValueError("DUPLICATE")
                out[key] = value
            return out
        data = json.loads(raw, object_pairs_hook=unique)
        if not isinstance(data, dict) or data.get("Id") != WP61 or "RepoDigests" not in data: return safe
        safe["exactIdMatch"] = True
        refs = data["RepoDigests"]
        if refs is None:
            safe.update(result="OBSERVED", repoDigestState="NULL")
            return safe
        if not isinstance(refs, list): return safe
        if len(refs) > MAX_REPODIGESTS:
            safe["repoDigestState"] = "COUNT_LIMIT_STOP"
            return safe
        counts = {category: 0 for category in (
            "EXPECTED_REPOSITORY_DIGEST", "OTHER_REPOSITORY_DIGEST",
            "MALFORMED_REFERENCE", "UNEXPECTED_VALUE_TYPE", "UNSUPPORTED_REFERENCE_SHAPE")}
        for ref in refs:
            counts[reference_category(ref)] += 1
        safe["referenceCategories"] = counts
        if counts["EXPECTED_REPOSITORY_DIGEST"] != len(refs):
            safe["repoDigestState"] = "UNSAFE_REFERENCE_WITHHELD"
            return safe
        safe.update(result="OBSERVED", repoDigestState="SAFE_REFS" if refs else "EMPTY",
                    safeRepoDigestCount=len(refs), safeRepoDigests=refs)
    except (ValueError, UnicodeError, TypeError, AttributeError, RecursionError):
        pass
    return safe


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__); parser.parse_args(argv)
    print(json.dumps({"result": "PLAN_ONLY", "imageId": WP61, "context": CONTEXT,
                      "dockerOperations": 0, "formatterValidation": "UNVERIFIED_NO_DOCKER_RUN", "exportAuthorized": False, "PUBLIC_RUNTIME_VERIFIED": False,
                      "liveAttemptReady": False}, sort_keys=True))
    return 0


if __name__ == "__main__": raise SystemExit(main())

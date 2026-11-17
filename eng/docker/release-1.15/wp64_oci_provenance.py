"""Pure offline OCI metadata verifier; no Docker, registry, HTTP or credential adapter."""
import argparse
import hashlib
import json
import re

LIMIT = 1048576
WP61 = "sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69"
SOURCE = "cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b"
REPO = "ghcr.io/samuel-santos-engineer/aiquanttradingresearch"
INDEX = {"application/vnd.oci.image.index.v1+json", "application/vnd.docker.distribution.manifest.list.v2+json"}
MANIFEST = {"application/vnd.oci.image.manifest.v1+json", "application/vnd.docker.distribution.manifest.v2+json"}
CONFIG = {"application/vnd.oci.image.config.v1+json", "application/vnd.docker.container.image.v1+json"}
LAYERS = {"application/vnd.oci.image.layer.v1.tar", "application/vnd.oci.image.layer.v1.tar+gzip",
          "application/vnd.oci.image.layer.v1.tar+zstd", "application/vnd.docker.image.rootfs.diff.tar.gzip"}


class Invalid(Exception):
    def __init__(self, reason, status="UNVERIFIED"):
        self.reason, self.status = reason, status


def require(ok, reason, status="UNVERIFIED"):
    if not ok: raise Invalid(reason, status)


def digest(value):
    return isinstance(value, str) and re.fullmatch(r"sha256:[a-f0-9]{64}", value) is not None


def hashed(raw):
    return "sha256:" + hashlib.sha256(raw).hexdigest()


def document(raw, expected):
    require(isinstance(raw, bytes) and len(raw) <= LIMIT and digest(expected), "INPUT_SCHEMA")
    require(hashed(raw) == expected, "RAW_HASH_MISMATCH")  # BEFORE JSON/descriptor interpretation.
    def unique(pairs):
        out = {}
        for key, value in pairs:
            require(key not in out, "DUPLICATE_JSON_FIELD")
            out[key] = value
        return out
    try: value = json.loads(raw, object_pairs_hook=unique)
    except (ValueError, UnicodeError, RecursionError): raise Invalid("JSON_SCHEMA") from None
    require(isinstance(value, dict), "JSON_SCHEMA")
    return value


def descriptor(value, kinds):
    require(isinstance(value, dict) and value.get("mediaType") in kinds and digest(value.get("digest")) and
            type(value.get("size")) is int and value["size"] >= 0 and not value.get("urls"), "DESCRIPTOR_SCHEMA")
    return value["digest"]


def blob(blobs, desc, kinds):
    key = descriptor(desc, kinds)
    require(key in blobs, "BLOB_MISSING")
    require(isinstance(blobs[key], bytes) and len(blobs[key]) == desc["size"], "DESCRIPTOR_SIZE")
    return key, document(blobs[key], key)


def verify(root_digest, blobs, expected_local_id=WP61, publication_receipt=None):
    result = {"status": "UNVERIFIED", "chainStatus": "UNVERIFIED", "acceptedLocalMapping": "UNVERIFIED",
              "publication": "UNVERIFIED", "deployableReference": None,
              "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    try:
        require(digest(expected_local_id) and isinstance(blobs, dict) and root_digest in blobs, "ROOT_MISSING")
        root = document(blobs[root_digest], root_digest)
        require(type(root.get("schemaVersion")) is int and root["schemaVersion"] == 2, "SCHEMA_VERSION")
        kind = root.get("mediaType")
        require(kind in INDEX | MANIFEST, "MEDIA_TYPE")
        runtime_digest = root_digest
        manifest = root
        if kind in INDEX:
            require(isinstance(root.get("manifests"), list) and len(root["manifests"]) <= 128, "INDEX_SCHEMA")
            selected = [x for x in root["manifests"] if isinstance(x, dict) and isinstance(x.get("platform"), dict) and
                        x["platform"].get("os") == "linux" and x["platform"].get("architecture") == "amd64"]
            require(len(selected) == 1, "PLATFORM_AMBIGUOUS")
            runtime_digest, manifest = blob(blobs, selected[0], MANIFEST)
            require(manifest.get("mediaType") == selected[0]["mediaType"], "MEDIA_TYPE_CONFLICT")
        require(type(manifest.get("schemaVersion")) is int and manifest["schemaVersion"] == 2 and
                manifest.get("mediaType") in MANIFEST, "MANIFEST_SCHEMA")
        layers = manifest.get("layers")
        require(isinstance(layers, list) and len(layers) <= 128, "LAYER_SCHEMA")
        for layer in layers: descriptor(layer, LAYERS)  # Descriptors only; no layer content access.
        config_digest, config = blob(blobs, manifest.get("config"), CONFIG)
        require(config.get("os") == "linux" and config.get("architecture") == "amd64", "CONFIG_PLATFORM", "MISMATCH")
        runtime = config.get("config")
        require(isinstance(runtime, dict), "CONFIG_SCHEMA")
        require(runtime.get("User") in {"12002", "12002:12002"}, "CONFIG_USER", "MISMATCH")
        ports = runtime.get("ExposedPorts")
        require(isinstance(ports, dict) and "8501/tcp" in ports, "CONFIG_EXPOSED_PORT", "MISMATCH")
        entry, command = runtime.get("Entrypoint"), runtime.get("Cmd") or []
        require(isinstance(entry, list) and isinstance(command, list) and all(isinstance(x, str) for x in entry + command), "STARTUP_SCHEMA")
        require(entry[:6] == ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py"], "ENTRYPOINT", "MISMATCH")
        args = entry[6:] + command
        def flag(name):
            hits = [x.split("=", 1)[1] for x in args if x.startswith(name + "=")]
            require(name not in args and len(hits) == 1, "LISTENING_EVIDENCE_AMBIGUOUS")
            return hits[0]
        require(flag("--server.port") == "8501" and flag("--server.address") == "0.0.0.0", "CONFIG_LISTENING_PORT", "MISMATCH")
        environment = runtime.get("Env")
        require(isinstance(environment, list) and all(isinstance(x, str) for x in environment), "RESEARCH_MODE_EVIDENCE_MISSING")
        research = [x.partition("=")[2] for x in environment if x.startswith("AIQ_RESEARCH_CHART=")]
        require(len(research) == 1, "RESEARCH_MODE_EVIDENCE_AMBIGUOUS")
        require(research[0] == "1", "RESEARCH_MODE", "MISMATCH")
        labels = runtime.get("Labels") or {}
        require(isinstance(labels, dict), "LABEL_SCHEMA")
        revision = labels.get("org.opencontainers.image.revision")
        if revision is not None: require(revision == SOURCE, "SOURCE_REVISION", "MISMATCH")
        result.update(chainStatus="MATCH", rootKind="INDEX" if kind in INDEX else "RUNTIME_MANIFEST",
                      rootDigest=root_digest, runtimeDigest=runtime_digest, configDigest=config_digest,
                      sourceRevision="MATCH" if revision == SOURCE else "UNKNOWN", layerContentVerified=False,
                      uiContract="MATCH")
        kinds = {"CONFIG": config_digest, "RUNTIME_MANIFEST": runtime_digest}
        if kind in INDEX: kinds["INDEX"] = root_digest
        mapped = [k for k, v in kinds.items() if v == expected_local_id]
        require(len(mapped) == 1, "LOCAL_ID_NOT_MAPPED")
        result.update(acceptedLocalMapping="MATCH", localIdKind=mapped[0])
        require(isinstance(publication_receipt, dict), "PUBLICATION_RECEIPT_MISSING")
        required = {"repository", "rootDigest", "runtimeDigest", "configDigest", "evidenceKind"}
        require(required <= publication_receipt.keys(), "PUBLICATION_RECEIPT_INCOMPLETE")
        require(publication_receipt["evidenceKind"] == "RECORDED_REGISTRY_READBACK", "PUBLICATION_RECEIPT_KIND")
        require(publication_receipt["repository"] == REPO and publication_receipt["rootDigest"] == root_digest and
                publication_receipt["runtimeDigest"] == runtime_digest and publication_receipt["configDigest"] == config_digest,
                "PUBLICATION_RECEIPT_CONFLICT", "MISMATCH")
        result.update(status="MATCH", publication="RECORDED_MATCH_NOT_FRESH_ATTESTATION",
                      deployableReference=REPO + "@" + runtime_digest)
    except Invalid as error:
        result.update(status=error.status, reason=error.reason)
    except Exception:
        result.update(status="UNVERIFIED", reason="INPUT_SCHEMA")
    return result


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.parse_args(argv)
    print(json.dumps({"result": "OFFLINE_VERIFIER_READY", "acceptedWp61Ui": "UNVERIFIED",
                      "deployableReference": None, "liveAdapterImplemented": False,
                      "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}, sort_keys=True))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

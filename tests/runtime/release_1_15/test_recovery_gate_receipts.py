"""Validate the versioned, sanitized Release 1.15 recovery receipt format."""
from copy import deepcopy
from datetime import datetime
import json
from pathlib import Path
import re
import unittest


ROOT = Path(__file__).resolve().parents[3]
RECEIPTS = ROOT / "docs/roadmap/release-1.15/RELEASE_1.15_SIMPLIFIED_API_RECOVERY_GATES.json"
GATES = {"G0", "G1", "G2", "G3", "G4"}
STATUSES = {"PASS", "FAIL", "BLOCKED", "SKIPPED"}
ATTEMPT_KEYS = {
    "gate_id", "attempt", "started_at_utc", "ended_at_utc", "status", "source_commit",
    "environment", "objective", "commands_or_test_ids", "expected", "observed",
    "exit_codes", "test_counts", "evidence_paths", "changed_files", "mutations",
    "rollback_state", "next_action", "rest",
}
REST_KEYS = {"method", "path", "http_status", "response_schema", "data_class"}
PATH_RE = re.compile(r"[A-Za-z0-9_.-]+(?:/[A-Za-z0-9_.-]+)*")


def require(condition, name):
    if not condition:
        raise ValueError(f"Invalid receipt field {name}")


def utc_time(value):
    require(type(value) is str and re.fullmatch(
        r"[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}(?:\.[0-9]{1,6})?Z", value), "UTC")
    return datetime.fromisoformat(value)


def is_text(value):
    return type(value) is str and bool(value.strip()) and not any(ord(char) < 32 for char in value)



def sha256(value):
    return type(value) is str and re.fullmatch(r"sha256:[a-f0-9]{64}", value)


def validate_artifacts(artifacts):
    require(type(artifacts) is dict and set(artifacts) == {
        "source_tree", "build_context_manifest", "publish_manifest_sha256", "inputs", "images",
    }, "artifact keys")
    tree = artifacts["source_tree"]
    require(type(tree) is str and re.fullmatch(r"[0-9a-f]{40}", tree), "source_tree")
    require(sha256(artifacts["build_context_manifest"]) and sha256(artifacts["publish_manifest_sha256"]), "build hashes")
    inputs = artifacts["inputs"]
    require(type(inputs) is dict and set(inputs) == {
        "container/release_1_15/research-api.Dockerfile",
        "container/release_1_15/research-chart.Dockerfile",
        "container/release_1_15/research-bootstrap.sh",
    } and all(sha256(value) for value in inputs.values()), "build inputs")
    images = artifacts["images"]
    require(type(images) is dict and set(images) == {"research", "ui"}, "image roles")
    digest_fields = {"local_id", "root_digest", "runtime_child_digest", "config_digest"}
    for role, image in images.items():
        require(type(image) is dict and set(image) == digest_fields | {"tag", "root_kind"}, "image keys")
        require(all(sha256(image[key]) for key in digest_fields), "image digests")
        require(is_text(image["tag"]) and re.fullmatch(r"aiq-wp07-[0-9a-f]{32}:" + role, image["tag"]), "local tag")
        require(image["root_kind"] in ("INDEX", "MANIFEST"), "root kind")
        require(image["local_id"] in (image["root_digest"], image["runtime_child_digest"],
                                      image["config_digest"]), "local identity")
        require((image["root_kind"] == "MANIFEST") ==
                (image["root_digest"] == image["runtime_child_digest"]), "runtime mapping")

def validate_receipts(document):
    require(type(document) is dict and set(document) == {"schema_version", "attempts"}, "document")
    require(type(document["schema_version"]) is int and document["schema_version"] == 1, "schema_version")
    require(type(document["attempts"]) is list and document["attempts"], "attempts")
    sequence = {}
    for item in document["attempts"]:
        require(type(item) is dict, "attempt")
        keys = set(item)
        require(keys == ATTEMPT_KEYS or
                keys == ATTEMPT_KEYS | {"artifacts"} and item["gate_id"] == "G3", "attempt keys")
        if "artifacts" in item:
            validate_artifacts(item["artifacts"])
        gate = item["gate_id"]
        require(type(gate) is str and gate in GATES, "gate_id")
        require(type(item["attempt"]) is int and item["attempt"] == sequence.get(gate, 0) + 1, "attempt")
        sequence[gate] = item["attempt"]
        require(type(item["status"]) is str and item["status"] in STATUSES, "status")
        require(item["environment"] in ("local", "staging"), "environment")
        require(utc_time(item["started_at_utc"]) <= utc_time(item["ended_at_utc"]), "time order")
        require(type(item["source_commit"]) is str and re.fullmatch(r"[0-9a-f]{40}", item["source_commit"]), "source_commit")
        for name in ("objective", "expected", "observed", "rollback_state", "next_action"):
            require(is_text(item[name]), name)
        names = item["commands_or_test_ids"]
        require(type(names) is list and names and all(is_text(value) for value in names), "commands_or_test_ids")
        for name in ("evidence_paths", "changed_files"):
            values = item[name]
            require(type(values) is list, name)
            require(all(is_text(path) and PATH_RE.fullmatch(path) and
                        not set(path.split("/")) & {".", ".."} for path in values), name)
            require(len(values) == len(set(values)), name)
        require(bool(item["evidence_paths"]), "evidence_paths")
        counts = item["test_counts"]
        require(type(counts) is dict and set(counts) == {"passed", "failed", "skipped"}, "test_counts")
        mutations = item["mutations"]
        require(type(mutations) is dict and (
            set(mutations) == {"azure"} or
            gate == "G3" and set(mutations) == {"azure", "registry_image_tags"}
        ), "mutations")
        require(all(type(value) is int and value >= 0 for value in (*counts.values(), *mutations.values())), "counts")
        exits = item["exit_codes"]
        require(type(exits) is dict and all(is_text(key) and type(code) is int for key, code in exits.items()), "exit_codes")
        require(type(item["rest"]) is list, "rest")
        for entry in item["rest"]:
            require(type(entry) is dict and set(entry) == REST_KEYS, "REST keys")
            require((entry["method"], entry["path"]) in (
                ("GET", "/health"), ("POST", "/v1/research/snapshot"), ("GET", "/_stcore/health")), "REST target")
            require(type(entry["http_status"]) is int and 100 <= entry["http_status"] <= 599, "REST status")
            require(entry["data_class"] in ("fixture", "local cached", "verified staging", "no data"), "REST data class")
            require(is_text(entry["response_schema"]), "REST schema")


def unique_keys(pairs):
    result = {}
    for key, value in pairs:
        require(key not in result, "duplicate key")
        result[key] = value
    return result


class RecoveryReceiptTests(unittest.TestCase):
    def fixture(self, status):
        item = {
            "gate_id": "G1", "attempt": 1, "started_at_utc": "2026-01-01T00:00:00Z",
            "ended_at_utc": "2026-01-01T00:00:01Z", "status": status, "source_commit": "b" * 40,
            "environment": "local", "objective": "Local health", "commands_or_test_ids": ["fixture"],
            "expected": "Alive", "observed": "Synthetic outcome", "exit_codes": {"test": 0},
            "test_counts": {"passed": 1, "failed": 0, "skipped": 0},
            "evidence_paths": ["docs/fixture.md"], "changed_files": [], "mutations": {"azure": 0},
            "rollback_state": "Staging unchanged", "next_action": "Review evidence", "rest": [],
        }
        if status == "FAIL":
            item.update(exit_codes={"test": 1}, test_counts={"passed": 0, "failed": 1, "skipped": 0})
        elif status == "BLOCKED":
            item.update(exit_codes={}, test_counts={"passed": 0, "failed": 0, "skipped": 1})
        return {"schema_version": 1, "attempts": [item]}

    def test_pass_fixture(self):
        validate_receipts(self.fixture("PASS"))

    def test_fail_fixture(self):
        validate_receipts(self.fixture("FAIL"))

    def test_blocked_fixture(self):
        validate_receipts(self.fixture("BLOCKED"))

    def test_rejects_unknown_fields_wrong_types_and_nonportable_paths(self):
        original = self.fixture("PASS")
        for change in ({"undocumented": "fake"}, {"status": "READY"}, {"attempt": True},
                       {"ended_at_utc": "2026-01-01T00:00:01+00:00"},
                       {"evidence_paths": ["../../secret"]}, {"test_counts": {"passed": True}},
                       {"rest": [{"method": "GET", "path": "/health?private=1", "http_status": 200,
                                  "response_schema": "fixture", "data_class": "fixture"}]}):
            with self.subTest(change=change):
                changed = deepcopy(original)
                changed["attempts"][0].update(change)
                with self.assertRaises(ValueError):
                    validate_receipts(changed)

    def test_rejects_duplicate_json_keys(self):
        with self.assertRaises(ValueError):
            json.loads('{"attempt": 1, "attempt": 2}', object_pairs_hook=unique_keys)

    def test_rejects_invalid_candidate_artifact_mapping(self):
        data = json.loads(RECEIPTS.read_text(encoding="utf-8"), object_pairs_hook=unique_keys)
        candidate = next(item["artifacts"] for item in data["attempts"] if "artifacts" in item)
        for change in ({"root_kind": "MANIFEST"}, {"local_id": "sha256:" + "0" * 64},
                       {"tag": "other:research"}):
            with self.subTest(change=change):
                altered = deepcopy(candidate)
                altered["images"]["research"].update(change)
                with self.assertRaises(ValueError):
                    validate_artifacts(altered)

    def test_execution_receipts_match_schema(self):
        data = json.loads(RECEIPTS.read_text(encoding="utf-8"), object_pairs_hook=unique_keys)
        validate_receipts(data)
        for entry in data["attempts"]:
            self.assertTrue(all((ROOT / path).is_file() for path in entry["evidence_paths"]))


if __name__ == "__main__":
    unittest.main()

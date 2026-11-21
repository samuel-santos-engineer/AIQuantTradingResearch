# Codex work package — Release 1.15 WP70: offline RepoDigest grammar review

## Authority and objective

Prepare a small, reviewable **offline-only** improvement to the existing Python RepoDigest structural classifier. Investigate which syntactic forms of Docker `RepoDigests` may be legitimate but are rejected by the deliberately conservative WP68 grammar. Distinguish `UNSUPPORTED_VALID_SHAPE` (or an equally clear fixed category) from genuinely malformed references only where supported by documented, offline reasoning and synthetic fixtures. Preserve strict redaction, fail-closed behavior, and all publication/provenance boundaries.

**No Docker commands of any kind** (including context/version), registry HTTP, Azure commands or reads, credential access, Go/toolchain, image export/build/pull/push/tag, containers/volumes, deployment, or public probes. Do not access WP67 or WP69 raw values; they were discarded. No new dependencies or governance gates. Do not infer a rebuild or publication requirement.

## Repository and branch boundary

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

PR #464 was squash-merged at `3ea0804e9819c8d073131bd79d699cb417e7b7ac`. **PR #465 is currently open/unmerged** at head `f40c5ce24ac5fc73a5a1bfb17064411fcb155ca2`, based on that main commit. Before editing, verify live GitHub/main and PR #465 state. **Do not merge #465**. If #465 is still open, branch from current main and keep WP70 code/tests independent of #465's evidence-only appendix; avoid rewriting or cherry-picking its evidence. If #465 has subsequently merged, branch from the updated main. Open one focused WP70 PR against current main, **leave it open/unmerged**. Preserve any owner worktree changes; use a clean isolated branch/worktree.

Accepted WP61 local image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`. WP66 confirmed its local Linux/amd64 identity. WP67 observed `UNSAFE_REFERENCE_WITHHELD`; WP69 observed one `MALFORMED_REFERENCE` under the existing conservative grammar. **Neither historical observation can be retroactively reclassified**. They do not prove corruption, credential presence, nonpublication, or registry reachability.

## Allowed files

- `eng/docker/release-1.15/wp65_local_image_metadata.py`
- `eng/docker/release-1.15/test_wp65_local_image_metadata.py`
- Append-only WP70 evidence in `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`

No unrelated code, governance, Dockerfiles, or deployment changes. If #465 is still open, preserve a clean conflict-free append-only strategy and document any anticipated evidence-file overlap; do not silently overwrite its evidence.

## Implementation

1. Review the existing `IMMUTABLE_REFERENCE`, `EXPECTED_REFERENCE`, `reference_category`, and `project_repodigests` behavior. Identify syntactic exclusions of the conservative grammar, using only local repository material and offline reasoning. Distinguish supported Docker/OCI reference syntax from assumptions; do not claim authoritative validation of a format without a source.
2. Add the smallest deterministic, bounded **synthetic-only** classifier refinement that can differentiate known valid-but-unsupported shapes from malformed inputs, without making any unapproved reference value printable. If reliable differentiation is not possible for an ambiguous form, keep it malformed/unknown and document the limitation rather than guessing.
3. Preserve the **exact** allowed application repository for emitted values: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:` followed by 64 lowercase hex characters. `safeRepoDigests` may be emitted **only if every value** matches that exact allowlist. Any other repository, unsupported shape, malformed string, unexpected type, or mixed list must withhold **all** reference values, with `result=UNVERIFIED`.
4. Retain exact image-ID guard, duplicate JSON-key rejection, 1 MiB input cap, 16-reference cap, null/empty/unknown/count-limit semantics, fixed bounded categories/counts, sanitized error behavior, `ociProvenance=UNVERIFIED`, `PUBLIC_RUNTIME_VERIFIED=False`, and `liveAttemptReady=false`.
5. Keep `repodigest_command` and the default `PLAN_ONLY` behavior unchanged. Do not add a live switch, execute the inspect command, or add dependencies.
6. Include synthetic regression fixtures for expected-repository values, structurally valid other repositories, optional registry-port syntax where clearly supported, mixed references, userinfo, URL schemes, tags without immutable digests, incomplete or uppercase digest hex, whitespace, ambiguous separators, nonstring values, duplicate keys, wrong/missing ID, null/empty collection, over-limit inputs, count cap, and redaction. Assert category-count bounds and no raw reference leakage. Assert import/default execution launches no process or network operation.

## Validation

Run the existing Python-only suites:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp65_local_image_metadata.py
```

Record actual pass/fail/skip counts (prior WP68 baseline: 72 tests), PLAN_ONLY, syntax checks, UTF-8/LF/no BOM, `git diff --check`, exact changed-file scope, and preservation of all earlier evidence. Run no Docker, registry, Azure or credential operation.

## Evidence and next decision

Append a WP70 section describing the grammar rationale, synthetic test outcomes, strict redaction, and limitations. Keep WP69's actual observation as **one `MALFORMED_REFERENCE` under the then-current WP68 grammar**; do not claim what its discarded value really was. A future real observation requires **separate explicit owner authorization** and must remain one bounded exact-image operation with host/context qualification, 10-second timeout, 1 MiB cap, numeric exit, sanitized errors, owned-process cleanup, no retries/fallback. No such inspection is authorized in WP70.

No local RepoDigest classification establishes a published/reachable OCI index, child manifest, config digest, deployable reference, Azure equivalence, or working public UI. Maintain API-first/UI-second deployment sequencing and existing auth/TLS/storage/ports.

## Deliverables

Open **one** scoped PR, leave it **open/unmerged**, and report its URL, base/head SHAs, changed paths, actual test counts, classification examples (synthetic only), and any evidence-file conflict with #465. Explicitly state:

**Offline grammar review only; no Docker inspection; OCI provenance UNVERIFIED; publication necessity NOT PROVEN; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.**

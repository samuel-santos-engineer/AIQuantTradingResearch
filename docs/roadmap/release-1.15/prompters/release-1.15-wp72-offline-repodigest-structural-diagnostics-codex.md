# Release 1.15 — WP72 Codex work package: offline RepoDigest structural diagnostics

## Authority and starting point

This document authorizes **only** the offline source/test/documentation work described below, plus ordinary Git branch/PR publication. It does **not** authorize Docker commands, image inspection, registry/GHCR HTTP requests, Azure reads/writes, credential access, public probes, runtime operations, image rebuild/export/pull/push/tag, or deployment. Leave the resulting PR **OPEN and UNMERGED**. Never merge another PR under this work package.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

Confirm current `main` includes merged PR #467 at squash commit `94029c6036435efb6a85af5c5dcade14f38d59db` (WP71), PR #466 at `c04fad45c59d5988e79d3c56dc501dc8ea594725` (WP70), and PR #465 at `a88e8a4c880a38e969b6017dcd24037bd649b339` (WP69). Fetch and inspect the current branch state; stop and report if unexpected. Use an isolated worktree/branch from fresh `main`; preserve all owner changes. Do not modify PR #457.

## Background and question

The accepted WP61 UI local image ID is `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` (Linux/amd64 verified locally by earlier separately authorized work). WP69 observed one `MALFORMED_REFERENCE` under WP68 grammar; WP70 added a conservative `UNSUPPORTED_REFERENCE_SHAPE` category for selected explicit-port references; WP71 observed one `MALFORMED_REFERENCE` and zero unsupported-port references under WP70. **All actual RepoDigest reference strings were withheld and discarded.** Their syntax is unknown. Neither observation proves corruption, nonpublication, a credential, or a rebuild requirement.

Goal: make useful **offline-only** progress on conservative structural diagnosis without requesting or inspecting another real RepoDigest value. Examine additional realistic syntactic shapes and decide whether a small, bounded fixed-category classifier refinement is justified by reliable local evidence. Do not guess the withheld value, claim that any proposed category matches WP69/WP71, or relax the exact expected-repository emission allowlist.

## Authorized implementation

1. Inspect `eng/docker/release-1.15/wp65_local_image_metadata.py`, `eng/docker/release-1.15/test_wp65_local_image_metadata.py`, and the append-only evidence document `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`. Review any **locally available** authoritative Docker/OCI reference grammar/parser material, if present, and clearly distinguish source-backed rules from conservative heuristics. Do not fetch online documentation or install dependencies.
2. Develop a compact synthetic-only shape matrix for candidates such as localhost/single-label authorities, bracketed IPv6, tagged-plus-digest, repeated path separators, uppercase components, port variants, and scheme/userinfo. Record which cases are ambiguous or outside the supported grammar. Do not assert universal Docker/OCI validity without reliable local evidence.
3. Prefer **no production change** if evidence is insufficient. If a narrowly justified new category or predicate materially improves fixed-count diagnostics, implement the smallest Python-only change with explicit documented semantics. Never return raw input, substrings, derived hashes, length leaks, character samples, or per-position data. Never classify unknown values as safe merely because they look plausible.
4. Preserve the existing exact expected-repository `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:<64 lowercase hex>` value-emission allowlist, and the all-or-nothing collection rule. Other/unsupported/malformed/mixed collections must continue to be entirely withheld. Keep fixed bounded category counts, 16-entry cap, 1 MiB raw cap, duplicate-key rejection, exact-ID matching, null/empty/unknown states, sanitized errors, 10-second deadline, owned-process cleanup semantics, and default `PLAN_ONLY`.
5. Do not change the inspect command, formatter, Windows host/context qualification, Docker process launcher, external-operation authorization, or any image/runtime/deployment code. No Go or new dependencies. If production code remains unchanged, a focused test/documentation-only PR is acceptable.
6. Add synthetic tests for the new matrix and invariants, including mixed collections, redaction and count limits. Do not place actual, discarded, or sensitive reference values in fixtures or documentation. Do not retrospectively reclassify WP67, WP69 or WP71 observations.
7. Append a clearly dated **WP72** evidence section after WP71, preserving all earlier evidence byte-for-byte. State what was reviewed, exactly which synthetic cases were tested, what was and was not changed, why classification is conservative, and what remains unknown.

## Required offline validation

Run, and record exact results for:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp65_local_image_metadata.py
```

Check `PLAN_ONLY`, `dockerOperations=0`, Python syntax, UTF-8/LF/no BOM, Markdown links/tables, `git diff --check`, exact changed-path scope, unchanged command construction, and prior-evidence prefix preservation. Record failures truthfully; do not claim tests passed if not executed. No Docker CLI calls are authorized, including `docker context show`.

## Deliverables and acceptance

- A narrow, reviewable PR titled `WP72: offline RepoDigest structural diagnostics` from current `main`, **OPEN/UNMERGED**.
- Synthetic-only shape matrix and rationale; small safe classifier/test changes only if warranted.
- Appended WP72 evidence, with earlier WP65–WP71 evidence unchanged.
- Exact offline test outcomes and changed-file list.
- No Docker, registry, Azure, credential, provider, network, runtime or deployment activity (except ordinary GitHub PR publication).
- State clearly: `OCI provenance UNVERIFIED`, `publication necessity NOT PROVEN`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

## Next-step decision boundary

A subsequent real inspection is **not authorized** here. If diagnostics still cannot identify a safe immutable mapping, explain the limitation and propose the smallest separately approvable action with explicit benefit and bounded exposure. Do not loop inspections, infer publication necessity, rebuild/publish by assumption, or declare public readiness. Preserve API-first/UI-second sequencing and existing auth/TLS/storage/port contracts.

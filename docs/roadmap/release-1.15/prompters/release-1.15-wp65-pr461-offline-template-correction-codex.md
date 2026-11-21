# Codex prompt — Release 1.15 WP65 / PR #461 offline Docker inspect template correction

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Existing PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/461
Existing PR head at review: `509371be1c195b4e0b5456f46d43ca99816bd6c9`
Merged main baseline: `ed39e0a0ca18e6316a5738651fce88aa28f2990f` (PR #460).

## Objective

**Update the existing PR #461, not a new PR.** Correct and independently validate the Docker `image inspect --format` projection offline, so a later separately approved single-image inspection will not repeat the observed template failure. Preserve all WP65 observations, including the original `CLI_FAILED` and separately approved retry (`exit 1`, `0.395 s`, classified `templateError=true`, owned-process cleanup passed). The exact failing expression is unknown; do not guess that the image is missing, corrupt, or unpublished.

## Hard authorization boundary

**Offline repository work and synthetic tests only.** Do not run Docker commands of any kind (including `docker version`, `docker context`, or `docker image inspect`), do not access Docker daemon, local images, registry, Azure, credentials, retained volumes, private files, or runtime. Do not export, pull, build, push, retag, restart, deploy, or modify Azure settings. Do not rerun the image inspection, even if the template appears fixed. Do not merge the PR. Do not alter PR #457. Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

## Work plan

1. Fetch current `main` and PR #461 head; check current mergeability and conflicts. Work on the existing PR branch and preserve all prior evidence. Do not overwrite newer work.
2. Inspect `eng/docker/release-1.15/wp65_local_image_metadata.py`, `test_wp65_local_image_metadata.py`, and `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`.
3. Determine the exact syntactic/semantic reason the current `FORMAT` could fail, using **offline evaluation** of the Go `text/template` expression against representative synthetic Docker inspect objects. In particular examine whether fields are accessed through `.Config` versus Docker's inspect projection, nil/missing fields, JSON serialization and how Go templates handle each. A string assertion in Python alone is insufficient.
4. If a Go toolchain is available locally, build a tiny **offline-only** `text/template` evaluation harness using synthetic fixtures and Docker-like structures; no dependency download, network access, daemon, or Docker CLI. If Go is unavailable, do not claim Go-template evaluation was performed: provide a self-contained harness and tests to run later, and report that limitation explicitly. Avoid adding a runtime dependency or changing deployment images.
5. Correct the fixed allowlisted template and its Python parser together as needed. Retain only `Id`, `Os`, `Architecture`, `Config.User`, `Config.ExposedPorts`, `Config.Entrypoint`, `Config.Cmd`, and safe `RepoDigests` in the projected result. Handle optional/nil fields fail-closed without printing or persisting raw values. Never project `Env`, labels, history, layers, filesystem, credentials, or arbitrary paths. Do not weaken WP64 provenance semantics.
6. Test successful template execution with representative synthetic Docker inspect structures, absent/nil fields, wrong types, malformed/duplicate JSON, unsafe RepoDigests, wrong image ID, and contract mismatches. Ensure no secret-bearing strings appear in result, test logs, error messages or committed evidence. Distinguish `UNVERIFIED` from `MISMATCH` appropriately. Keep the exact accepted image ID fixed: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
7. Run the new WP65 tests and existing WP64 and setting-name parser/collector suites offline; record actual counts and outcomes. Check Python syntax, UTF-8/LF/no BOM, Markdown links, and `git diff --check`.
8. Append a clearly dated **offline correction** section to the existing WP65 evidence. Preserve original attempts unchanged; state the corrected template's validation evidence and residual uncertainty. No claim of successful real Docker inspect, image presence, RepoDigest, OCI index/child/config, registry publication or Azure readiness.
9. Push the update to **PR #461** and report the new head SHA, exact files changed, tests, review/mergeability state, and next separately approvable action. If the PR is non-mergeable, identify why and reconcile only this PR against current `main` without discarding WP65 evidence; do not merge.

## Acceptance criteria

- Actual offline Go-template evaluation against synthetic inspect fixtures succeeds, or the missing Go-toolchain limitation is explicitly documented with runnable test harness left for qualification.
- Fixed projection remains strictly allowlisted, bounded, and fail-closed; no Docker or other external operation occurred.
- Original WP65 inspect/retry evidence remains intact; the only actual observation still indicates a template error, not image absence.
- Existing PR #461 updated and left open/unmerged.
- Final report includes `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`, and states **WP61 UI OCI provenance UNVERIFIED**.

## Future step — NOT AUTHORIZED HERE

After owner review of offline validation, request **separate explicit approval** for one bounded, read-only `docker --context desktop-linux image inspect` of the exact WP61 ID, with fixed safe projection, 10-second request deadline, 1 MiB combined capture cap, sanitized numeric exit/error category, no retry/fallback, and owned-process cleanup. Image export and all registry/Azure operations remain out of scope.

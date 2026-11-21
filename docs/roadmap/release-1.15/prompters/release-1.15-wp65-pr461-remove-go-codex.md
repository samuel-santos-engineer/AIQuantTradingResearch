# Codex prompt — Release 1.15 WP65: simplify PR #461, remove Go

## Authority and objective

Work ONLY in `samuel-santos-engineer/AIQuantTradingResearch`, on existing **open/unmerged PR #461** (`feat/release-1.15-wp65-local-image-metadata`). Begin by fetching current `origin/main` and the PR head; expected reviewed head was `fcd59834e2cdce4eda9e6b6c002d9ddb986a9c8b`, but verify before editing. Do not force-push or rewrite history. Keep the PR open for owner review; **do not merge**. Do not modify unrelated PR #457.

Owner decision: **Go is OUT OF SCOPE. Do not install, execute, require, download, or add Go as a toolchain, dependency, CI gate, or acceptance requirement.** The embedded Go `text/template` harness in PR #461 was unnecessary scope expansion. Remove it and its test infrastructure. This is an offline repository-only correction.

## Changes requested

1. Restrict edits to the three existing WP65 PR paths unless an unavoidable reason is documented:
   - `eng/docker/release-1.15/wp65_local_image_metadata.py`
   - `eng/docker/release-1.15/test_wp65_local_image_metadata.py`
   - `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`
2. Remove `GO_HARNESS`, Go-specific fixtures, `go_toolchain`, `go run`, temporary Go cache, environment variables for Go, skip markers, and Go-specific qualification flags. No new language/toolchain, no CI dependency. Preserve the original two real inspect attempts and their exact sanitized observations (including the second `exit 1`, `0.395s`, `templateError=true`). Do not retroactively imply the failed command was valid or the image absent.
3. Simplify the Docker inspect projection to avoid fragile nested Go template traversal. Prefer a minimal, fixed Docker-supported `--format` expression that returns a JSON object using only allowlisted safe fields and no raw full-image dump. Where uncertain about the Docker CLI's actual template root or null behavior, fail closed and state that real Docker evaluation remains **UNVERIFIED**; do not claim a Python test proves Docker's formatter semantics. Consider whether the safest approach is to request a minimal identifier/platform projection first, then separately authorize any richer metadata, instead of requiring an all-fields template to succeed in one command. Do not add a Docker call in this work package.
4. Keep `project(raw)` strictly bounded and redacted: maximum 1 MiB, duplicate-field rejection, exact WP61 image ID, validated Linux/amd64, UID 12002, exposed/listening 8501, Streamlit entrypoint, and safe exact-repository RepoDigests when those fields are present. Distinguish **UNVERIFIED** for missing/unsupported evidence from **MISMATCH** for a proven contradictory contract. Do not emit raw Env, labels, history, private paths, arbitrary command arguments, raw stderr, or secret-bearing fields. Never promote a local image ID or a RepoDigest alone to a fully verified OCI chain or a deployable WP61 reference.
5. Add/adjust **Python-only synthetic tests** for command construction, schema/field absence, null and wrong types, duplicate JSON keys, wrong image ID, port/entrypoint contract, safe RepoDigest handling, redaction, and no Docker/network execution by default. Do not leave a skipped Go test masquerading as qualification. Do not create or run a substitute Go evaluator through another language or executable.
6. Update WP65 evidence to record the owner scope decision and the remaining limitation: no live Docker template evaluation in this correction; next real inspection would require **new, separate, explicit owner authorization**. Keep the accepted WP61 UI provenance **UNVERIFIED**, publication necessity **NOT PROVEN**, and `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

## Hard execution boundaries

**ZERO Docker commands** (including `docker version`, `docker context`, `docker image inspect`, and `docker image save`); ZERO daemon/image operations, exports, builds, pulls, pushes, retags, containers or volumes. ZERO registry HTTP, Azure operations (including reads), credentials, private runtime files, public probes, or deployment. No Go installation or execution. No workaround that bypasses these boundaries. Repository/GitHub operations and Python-only offline synthetic validation are permitted.

Preserve the accepted API-first, UI-second Release 1.15 deployment plan and existing authentication/TLS/storage/port contracts. Do not introduce licensing or governance approval gates unrelated to implementation.

## Validation and deliverable

Run Python-only WP65, WP64 and existing setting-name parser/collector test suites. Report exact pass/fail/skip counts, including any legitimate skips; check Python syntax, UTF-8/LF/no BOM, Markdown links and `git diff --check`. Inspect final diff to ensure the Go machinery is gone and the changes are limited to WP65 scope. Update **existing PR #461** with code, tests, and appended evidence; report final head/base, files changed, exact tests, and remaining uncertainty. **Leave PR OPEN/UNMERGED.**

If the corrected template cannot be shown reliable without a live Docker call, document that limitation rather than introducing another toolchain or performing an unauthorized inspect. Next owner decision should concern a single tightly bounded Docker image inspection only after review, not Go installation.

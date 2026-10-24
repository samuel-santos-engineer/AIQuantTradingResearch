# Codex continuation — Release 1.15 WP02 simplified lab security hardening

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Owner decision: **Keep the project simple.** Two separate single-process Docker images; no custom DNS cancellation worker/process and **no API-key rotation in this phase**. API-key authentication remains required.

## Verified baseline

- PR #384 merged at `d237e268fc0cd61cad9c8a32053f57095351f78c` (offline dual-Docker fixture foundation).
- PR #385 merged at **`4578f8645c9626f4daa7ec76923953e8892c93d6`** (planning-only deadline/rotation preflight); its complex proposals are **superseded by the owner's simpler lab decision in this prompt**.
- WP02 issue #368 remains open; `PUBLIC_RUNTIME_VERIFIED=False`.
- Existing local fixture evidence: 12 client + 9 paired-container tests; do not claim these have been rerun.

## Step 1 — Preflight and preserve local work

Inspect `AGENTS.md`, `main`, merged PRs #382–#385, WP02 #368, and the existing WP02 fixture files and evidence. Confirm clean GitHub baseline; do not reset, stash, clean, overwrite, or delete the owner's pending local files. Prefer a separate worktree. Record the exact baseline SHA. Do not assume earlier planning text is authorization for its more complex designs.

## Step 2 — Scope: simple lab-grade client hardening

Implement a **small, bounded WP02 fixture-only change** that retains the existing two non-root, single-principal-process Linux Docker images. Maintain HTTPS-only server-side `X-API-Key`, TLS chain/hostname verification, environment-specific keys, generic error messages, response size bounds, no browser secret exposure, and no raw/admin research endpoints.

- Use standard Python HTTP client/socket timeouts with one modest configurable timeout (retain the existing fixture default of 4 seconds unless testing reveals a reason to adjust). Track monotonic elapsed time to prevent intentionally resetting the budget between phases. Do not promise hard cancellation of OS DNS resolution; document that Python/native DNS may exceed the deadline on a pathological resolver stall.
- Keep one synchronous request path. **No DNS worker threads, helper subprocesses, watchdog supervisor, killable resolver service, custom socket transport, thread-per-request workaround, or new dependencies.** If a small existing watchdog is useful for body/header stalls, keep it simple; avoid claiming it interrupts DNS. Do not introduce elaborate fault injection requiring a new runtime model.
- Use **one active shared API key** per environment from Azure App Service application settings when deployment is separately authorized. Keep staging/prod keys distinct and separate from Twelve Data and HMAC material. Reject missing/invalid keys with HTTP 401. No previous-key acceptance, grace windows, key-revision witness, trusted-clock mechanism, persistence, or automatic rotation in this phase. Operational replacement can be a coordinated settings update/restart with a short planned interruption; document this as a future operation, not a deployment task.
- Maintain the fixture's intentionally unavailable D4 endpoint; **do not implement production snapshot data**, research algorithms, SQLite writer, ingestion, chart, or Azure integration.

## Step 3 — Proposed exact edit scope

Only these **existing** files are candidates for this bounded WP02 fixture-hardening PR, subject to verifying they are needed:

1. `tests/runtime/release_1_15/dual_docker/api_key_client_fixture.py`
2. `tests/runtime/release_1_15/dual_docker/test_api_key_client_fixture.py`
3. `tests/runtime/release_1_15/dual_docker/Program.cs`
4. `tests/runtime/release_1_15/dual_docker/test_pair_boundary.py`
5. `tests/runtime/release_1_15/dual_docker/fixture_contract.md`
6. `eng/docker/release-1.15/Invoke-Wp02DualDockerQualification.ps1`

Documentation updates, if necessary, must be limited to these **existing** files:

7. `docs/roadmap/release-1.15/RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md`
8. `docs/roadmap/release-1.15/RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT_EVIDENCE.md`
9. `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md`

Do not modify `tls_fixture.py`, Dockerfiles, production sources, CI, dependencies, or other paths without returning an exact-path justification and asking the owner first. The owner's request for a Codex prompt authorizes preparing this handoff, **not automatically changing source**; before source edits, Codex must check whether the owner has explicitly approved the six source paths above. If not, prepare a concise scope/decision report or docs-only PR and request that approval. Do not treat merged PR #385's seven-path *proposal* as implementation approval.

## Step 4 — Minimum test plan after source authorization

- Client rejects HTTP URLs, redirects, invalid/missing key, malformed JSON, excess response bytes, wrong TLS certificate/hostname and generic network errors.
- Research endpoint denies absent/wrong key; authenticated raw/admin paths remain inaccessible; one active key only. Add a test showing a formerly configured key is rejected after a service restart with the new active key (no overlap).
- Cover normal timeout and failure recovery with deterministic local tests. If possible, add a bounded local delayed-header/body scenario without new tooling. **Do not** require proving native DNS cancellation, zero resolver-worker count, trusted wall-clock expiry, or rollback-proof key revocation.
- Preserve existing local Docker qualification, image provenance, cleanup ownership, PowerShell 5.1 compatibility, non-root identity, and all currently passing test cases (adjust obsolete rotation assertions transparently). Run the complete focused fixture suite; record exact commands, results, source hashes and any limits. Distinguish local results from CI and Azure.
- UTF-8 without BOM, LF line endings, terminal newline, valid links, and `git diff --check`.

## Step 5 — PR and handoff

After explicit exact-path source authorization, open a **separate implementation PR** for review, leave it OPEN/UNMERGED, and report changed paths, test results, and any limitations. Do not merge it, close WP02 #368, deploy to Azure, touch real secrets, push images, call providers, or assert public runtime readiness.

WP05 owns production D4 authenticated HTTPS integration and operational key replacement semantics; WP07 owns actual Azure TLS/ingress/App Service settings, deployment and shared F1 qualification; WP03 owns SQLite writer/storage recovery. No paid services, new approval/licensing gates, or production rollouts. `PUBLIC_RUNTIME_VERIFIED=False`.

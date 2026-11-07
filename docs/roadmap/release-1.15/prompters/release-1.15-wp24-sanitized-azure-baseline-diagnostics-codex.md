# Codex implementation prompt — Release 1.15 WP24
## Sanitized Azure F1 Read-Only Baseline Failure Diagnostics

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `main` after merged WP22 PR #412, squash commit `5cc708a9a2035e95988edf1a2ad4104460ba202b`. Confirm the actual checkout, status and history before editing. Preserve any existing dirty checkout and untracked files; use a clean isolated worktree/branch if necessary.

### Operator evidence and objective

WP23 operator execution succeeded for offline tests and real PowerShell preflight. A fresh **disabled** private attempt was prepared: `a6a6a450-6479-473b-8aa7-8d828b671058`. Its Azure read-only baseline validation failed with a generic report, unable to distinguish authentication, authorization/RBAC, response schema, incomplete capture, target mismatch or configuration drift. `serviceKeyChannelAvailable=false` on this failed run means **unverified**, not absent. No live controller ran. The attempt's private records must be preserved and never reset, rewritten, disclosed or consumed.

Implement a **minimal diagnostic correction** to the merged WP22 read-only inspector that returns the *failed capture stage* and a *fixed, sanitized failure category*. Keep the existing strict validation, operation allowlist, bounded transport, exact research target, immutable image and operator boundaries unchanged. This is implementation, not another planning/governance exercise.

### Scope and constraints

- Primary file: `eng/azure-cli/r1.15-qualification/wp22_preparation.py`.
- Tests: `tests/runtime/release_1_15/test_wp22_preparation.py`, with narrowly necessary additional test coverage.
- Operator runbook: `docs/roadmap/release-1.15/prompters/release-1.15-wp22-attempt-preparation-runbook.md`, or a concise WP24 addendum. Only modify the PowerShell entry if a demonstrated defect requires it.
- Inspect the actual WP21 runtime (`wp21_runtime.py`), controller, WP19/WP18 preflight and Azure transport before changing exception handling. Avoid broad refactors and do not modify live deployment/recovery behavior.
- Preserve WP22's eight exact allowed ARM operations per capture: site GET, plan GET, web GET, appsettings/list POST, connectionstrings/list POST, logs GET, slotConfigNames GET and slots GET. POST list is read-only. No other ARM operations, no Azure writes, no Start/Stop, no HTTP health probes, no registry or provider calls, no imports.
- **No live Azure requests by Codex**, no automatic login, no policy/identity workaround, no private credentials, no attempts to access the operator's private files.
- The existing attempt `a6a6a450-6479-473b-8aa7-8d828b671058` is historical evidence. Do not delete, overwrite, re-arm, extend its approval window or repeat inspection against it. Do not expose its private directory, approval or observations.
- Approval stays disabled; `liveAttemptReady=false`, `liveControllerInvoked=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`.

### Required diagnostic design

1. Expose a stable `failedStage` (fixed enum string) indicating where the inspection stopped. Suggested stages: `PRIVATE_ATTEMPT_TEMPLATE`, `OPERATOR_PREFLIGHT`, `FIRST_CAPTURE_SITE`, `FIRST_CAPTURE_PLAN`, `FIRST_CAPTURE_WEB`, `FIRST_CAPTURE_APP_SETTINGS`, `FIRST_CAPTURE_CONNECTION_STRINGS`, `FIRST_CAPTURE_LOGS`, `FIRST_CAPTURE_SLOT_FLAGS`, `FIRST_CAPTURE_SLOTS`, corresponding `SECOND_CAPTURE_*` stages, `FIRST_CAPTURE_VALIDATION`, `SECOND_CAPTURE_VALIDATION`, `BASELINE_COMPARISON`, `POST_CAPTURE_TEMPLATE_RECHECK`, `SERVICE_KEY_CHANNEL_CHECK`, `OBSERVATION_WRITE`. Choose exact enum names based on real code paths. The stage must reflect the actual failing operation, not a guess.
2. Expose `failureCategory` from a **closed allowlist**, e.g. `LOCAL_PREREQUISITE_UNVERIFIED`, `AZURE_CLI_UNAVAILABLE`, `AZURE_AUTHENTICATION_UNVERIFIED`, `AZURE_AUTHORIZATION_UNVERIFIED`, `AZURE_TRANSPORT_FAILURE`, `AZURE_RESOURCE_RESPONSE_INVALID`, `AZURE_RESPONSE_SCHEMA_MISMATCH`, `AZURE_TARGET_OR_PLAN_MISMATCH`, `AZURE_BASELINE_DRIFT`, `AZURE_READ_TIMEOUT`, `AZURE_BASELINE_INCOMPLETE`, `OBSERVATION_PERSISTENCE_FAILED`, `UNCLASSIFIED_FAILURE`. Only assign authentication vs authorization if structured, trustworthy transport evidence actually distinguishes them. Otherwise report a combined or unknown category, never infer from arbitrary exception text.
3. Make stage tracking explicit at each request boundary, including both capture passes. The inherited `Azure.capture()` currently bundles multiple operations: consider a minimal read-only adapter override or a callback/instrumentation mechanism that does not change the transport allowlist or bypass WP21 `validate_baseline`. Avoid falsely attributing validation errors to a network stage.
4. Error classification must be deterministic and based on exception types / fixed internal error codes / safe HTTP status metadata where genuinely available. **Do not inspect, parse or return raw Azure error text** to classify failures; error messages may contain secrets or identifiers. If Azure CLI currently discards HTTP status and all error detail, keep the classification conservative or add a strictly sanitized typed status boundary with tests. Do not leak subprocess stderr, request bodies, raw paths, environment variables, settings names/values, resource IDs, key material or connection strings.
5. Keep a fixed, compact JSON schema, e.g. `result`, `failedStage`, `failureCategory`, booleans for `azureBaselineVerified`, `serviceKeyChannelAvailable`, `serviceKeyChannelChecked`, `approvalEnabled`, `liveControllerInvoked`, `liveAttemptReady`, `PUBLIC_RUNTIME_VERIFIED`. On failure before the key check, `serviceKeyChannelChecked=false` and availability must not be interpreted as missing. A successful full read-only validation must retain the existing `READ_ONLY_BASELINE_VERIFIED_NOT_AUTHORIZED` semantics.
6. Preserve strict two-capture equality, validation of all categories, fixed target/plan/F1/region/Running/slots, freshness and code hashes before and after captures, and no `baseline.json`/`consumed.json` creation. Keep new-only sanitized observation records and original private ACL guarantees.
7. Preserve bounded worker and total inspection behavior. No retries or automatic second inspection beyond the two already-required capture passes. Avoid any new Azure requests from diagnostic logic.
8. Fail closed for unexpected exceptions, including failures while writing observation evidence. Never print a misleading success if evidence persistence failed. Reports should stay within the existing bounded size and UTF-8/LF requirements.

### Tests and acceptance criteria

- Add focused mocked tests for every failed stage, both capture passes, schema missing/malformed, resource/plan/region mismatch, 401 vs 403 *only when structurally supported*, Azure CLI failure without status, timeout, first/second capture drift, template expiry before/after, missing key-channel and not-checked key-channel distinction, observation write failure, and unknown exceptions.
- Assert no secret can appear in stdout, serialized JSON or observation files even when a mock exception contains a unique secret canary, malicious stderr, a path or an Azure response body.
- Assert exact ARM operation allowlist and no additional calls, no mutation, no live controller, no approval enablement, no write to WP21 baseline/consumed, and no change to recovery implementation.
- Run WP24/WP22 tests and WP21/WP19/WP18 relevant regression tests; report exact counts and limitations. Run `git diff --check`, UTF-8/LF checks, and PowerShell parser checks if a `.ps1` file changes. Do not claim a real operator PowerShell `-File` execution in a restricted Codex host.
- Provide a short, safe operator handoff **after PR review/merge**, with commands for a **new** disabled attempt and **one** read-only inspection in the owner's already-authorized PowerShell/Azure session. Do not ask the operator to repeat the failed inspection on the old UUID; preserve old evidence. Do not print private paths or secret-bearing JSON.

### Deliverables and GitHub workflow

Create one narrowly scoped branch and PR against `main`, leave it **open and unmerged** for owner review. Include a concise explanation of exact diagnostic schema, test evidence and why no Azure operation was performed. If current code cannot safely distinguish auth vs RBAC without altering the Azure transport contract, state that limitation and return a conservative category rather than inventing precision. Do not create licensing gates or new architectural decisions.

Stop after the PR and sanitized offline results. No Azure live controller, deployment, Start/Stop, application HTTPS probe, authenticated snapshot, dataset import, UI change or public activation. The goal is a useful failure category and failed stage so the owner can decide the next implementation fix from one subsequent authorized read-only observation.

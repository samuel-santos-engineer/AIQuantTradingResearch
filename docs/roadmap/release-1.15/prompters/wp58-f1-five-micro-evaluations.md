# WP58 — Five minimal Azure F1 staging evaluations (Codex prompt)

## Objective
Determine whether the two accepted application images are *actually running* in existing Azure F1 staging and whether an Azure-side I/O exception exists. Do not conflate the local Linux SDK test-container `System.IO.IOException` with an Azure exception. Keep PR #445 and #446 open/unmerged. No production actions.

## Boundaries
Planning and offline preparation only until the owner separately approves each named live evaluation. No deployments, restarts, configuration changes, SSH installation, CLI upgrades, Docker builds/pulls, data transfer, initialization, credentials, private data, provider requests, or new resources. Do not assume an ARM-configured image digest proves the running container identity. Avoid broad logs, secret-bearing environment/settings dumps, and raw response bodies. Use exact allowlisted staging apps only:
- UI: `aiqr112wp05wus27f5eabb5`
- API: `aiqr115researchstagingwus27f5eabb5`
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Production `aiqr112wp035ec325382770` is excluded.

## E1 — Configuration identity (first approval candidate)
Read-only, projected ARM configuration metadata for the two staging apps, no secrets. Record image reference (registry host/repository and digest, redact auth), startup command present/empty, HTTPS-only, app running/enabled, and plan identity. At most 4 GETs, 10 seconds each, no retries. Record whether API still points to old `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`. Report `CONFIGURED_ONLY`, not `RUNNING_PROVEN`.

## E2 — Running container/startup evidence
First offline identify one existing Azure-supported read-only diagnostic that can attest container startup and exact runtime image identity (if available), without exposing environment, secrets or full logs. Seek separate owner approval for one bounded request per app. If Azure cannot attest actual running digest, report `UNKNOWN`; do not infer from configuration or startup status. Capture only timestamps, startup state, role, exception type and sanitized reason category.

## E3 — Minimal serving checks
After E1/E2, separately approve one headers-only HTTPS GET per app: API `/health`, UI `/`, max 10 seconds/request, no redirects/retries, no body or authentication tokens in output. Record HTTP status, TLS success, content type, elapsed time, and whether a response was captured. Prior API 403 and timeout remain unexplained; do not attribute cause without evidence. Health 200 does not prove SQLite data.

## E4 — Azure exception classification
Only if Azure startup diagnostics show a relevant failure, prepare one narrowly filtered, read-only, time-bounded diagnostic lookup for `System.IO.IOException` and neighboring exception type/operation stage; no raw log dump, stack paths, payloads or secrets. Separate approval required. Distinguish `AZURE_IO_EXCEPTION_CONFIRMED`, `NO_EXCEPTION_IN_BOUNDED_WINDOW`, and `UNKNOWN`. Local Linux test failure must remain a separate finding.

## E5 — SQLite writable-root probe (conditional)
Only after runtime identity and process access are established, assess whether existing nonsecret storage-probe/diagnose code can run safely *inside the actual API container* without restart, mutation, private-data reads or lifecycle changes. Offline code review first; no execution unless owner separately approves exact method. Return UID, overlay/writable flags, and classified error code only. Do not create a public diagnostic endpoint, write test data into the database, or modify startup mode.

## Reporting contract
For each evaluation: `NOT_RUN / PASS / FAIL / UNKNOWN`, exact bounded evidence, interpretation, and single next action. Keep `LOCAL_CHART_VERIFIED=True` historical, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until genuine browser acceptance. Stop on first unexpected result; do not automatically proceed to the next live evaluation. Initial response should propose **E1 only** for owner approval, without executing it.

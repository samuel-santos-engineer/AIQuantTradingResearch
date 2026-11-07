# Release 1.15 — WP26 Codex implementation prompt

## Mission

Implement a **narrow, fail-closed sanitized Azure ARM transport diagnostic correction** so the operator can distinguish trustworthy HTTP status metadata (when available) from fixed CLI/worker failure categories. Open a small PR for owner review; **do not merge it**.

## Authority and starting point

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

Start from current `origin/main`, which includes merged WP24 PR #413 (squash commit `0ac12a38b034ab4bf64fdcb0d8ca3d566773d1b9`). Inspect the current source before changing anything, especially:

- `eng/azure-cli/r1.15-qualification/wp22_preparation.py`
- WP21 Azure transport, bounded worker, parent timeout, and controller code
- `tests/runtime/release_1_15/test_wp22_preparation.py` and relevant WP21 transport tests
- `docs/roadmap/release-1.15/prompters/release-1.15-wp22-attempt-preparation-runbook.md`

Operator WP25 evidence: offline tests and preflight passed, a fresh disabled attempt was prepared, and the **first research-site ARM GET** failed with `failedStage=FIRST_CAPTURE_SITE`, `failureCategory=AZURE_TRANSPORT_FAILURE`. Current transport cannot reliably distinguish authentication, RBAC, HTTP rejection or CLI-worker failure. Service-key check was not reached; its availability is **unverified**.

**Historical private attempt `098374d7-2bd7-46ae-af33-d9a125b2b418` must remain untouched.** Do not reopen, re-inspect, extend, reuse, consume, arm, or modify it or its evidence. Do not access any other private attempt records.

## Scope and requirements

1. Trace the existing WP21 ARM request/worker boundary and WP24 inspector. Implement the **minimum compatible change** needed to carry *trusted structured* transport failure metadata into the WP24 sanitized report. Avoid changing WP21 controller/recovery behavior; if a shared transport modification is unavoidable, demonstrate backwards compatibility with tests.
2. Add a stable, bounded, closed-enum worker/transport failure field (e.g. `transportFailureCategory`) and a nullable numeric HTTP status field (e.g. `httpStatus`) **only if genuinely observable from a structured transport response or reliable process API**. Do not parse CLI stderr, raw Azure error text, exception strings, response bodies, or headers to infer HTTP status or failure class. If HTTP status cannot be established, use `null`; if a cause cannot be established, use a conservative `UNKNOWN` category. Distinguish only cases proven by typed evidence: CLI executable unavailable, launch failure, parent timeout, watchdog timeout if typed, worker nonzero exit, malformed/invalid worker output, and HTTP rejection if reliably structured. Do not claim authentication versus RBAC without reliable evidence.
3. Retain WP24 `failedStage`, `failureCategory`, `serviceKeyChannelChecked`, `serviceKeyChannelAvailable` semantics. For failures before the key check, `serviceKeyChannelChecked=false` and availability remains unverified. For successful captures, diagnostic fields should be null and readiness/approval remain false.
4. Preserve the **exact eight authorized ARM operations per capture**, two captures with strict equality, existing timeout/watchdog limits, existing disabled-attempt TTL, no retries, no pagination, no extra requests, and no mutations. Read-only POST list operations remain the existing fixed read operations. Do not relax baseline schema, F1/region/target, key checks, or other guards.
5. Keep output privacy-preserving and bounded. Never emit raw HTTP response bodies, stderr/stdout from subprocesses, Azure headers, secrets, tokens, connection strings, config values, private paths, environment dumps, arbitrary error messages, or raw exception repr. Avoid persisting secret-bearing baseline data. Observation write failure must still override any earlier success and fail closed without retry. Historical evidence must not be overwritten.
6. Add mocked regression tests for every genuinely supported category, structured status propagation and null fallback, worker exit and timeout paths, malformed responses, status spoofing via error text (must be ignored), secret canaries in stdout and observation, no retry, unchanged operation inventory/order, strict two-capture comparison, and backward compatibility with WP21. Include negative tests for unsupported/ambiguous statuses. No real Azure access in tests.
7. Update the existing runbook with the new exact diagnostic fields, what they can and cannot prove, the offline tests and a **single fresh disabled-attempt operator handoff** to be performed only after PR review/merge in the owner's authorized Windows PowerShell/Azure session. Do not include any instruction to bypass execution policy, elevate permissions, rotate credentials or change identity.

## Explicit exclusions

- **No Azure requests or live probes in the Codex environment.** No deployment, Start/Stop, image/tag/push, credentials, paid resources, production changes, app settings writes, RBAC changes or UI modifications.
- No WP21 controller/recovery redesign; no licensing/governance gates; no synthetic market data.
- Do not create an authorization, approval token, or ready-to-deploy status. `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `PUBLIC_RUNTIME_VERIFIED=False`.
- No repeated operator attempts before this diagnostic PR is reviewed and merged.

## Implementation process

1. Verify clean base and inspect actual files. Use an isolated branch/worktree; preserve the owner's working tree.
2. Make the smallest patch, keeping failure classification at the right transport boundary and documenting where metadata is trustworthy or unavailable.
3. Run focused WP26/WP24/WP22 and WP21 tests plus relevant WP19/WP18 regressions. Run `git diff --check`, UTF-8/LF checks, and applicable syntax checks. If environment blocks PowerShell `-File`, report that limitation; do not bypass policy or claim operator execution.
4. Create a PR targeting `main`, leave **open and unmerged**. Include changed-file inventory, exact test counts, safety invariants, any inability to observe HTTP status, and precise operator instructions for **after** merge.
5. If no reliable HTTP status can be extracted without violating scope, explicitly leave it `null`, implement only the provable fixed worker categories, and explain the limitation. Do not fabricate or infer status.

## Acceptance criteria

- A failed `FIRST_CAPTURE_SITE` yields the existing sanitized stage plus an evidence-grounded, fixed transport category and nullable HTTP status.
- Sensitive data cannot enter stdout or persisted sanitized observation, including on failures.
- All previous fail-closed controls and exact read-only Azure request behavior remain intact.
- All tests pass or blockers are documented accurately.
- PR is open for owner review; no live controller, Azure inspection, deployment, or approval is executed.

## Report back

Provide PR URL and number, base/head commits, files changed, test results, new field names and enum values, limitations, and whether any existing transport interface was changed. Explicitly confirm historical attempt untouched, no Azure requests, and `PUBLIC_RUNTIME_VERIFIED=False`.

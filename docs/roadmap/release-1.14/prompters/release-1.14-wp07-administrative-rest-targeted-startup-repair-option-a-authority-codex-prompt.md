# Release 1.14 WP07 — Administrative REST Targeted Startup Repair and Option A Contract Amendment

## Authority decision

The owner **approves Option A** for disabled-mode routing: preserve the existing direct Streamlit listener on port 8501 when administrative diagnostics are disabled. An `/admin/...` URL may receive Streamlit's generic UI fallback (including HTTP 200). **Do not claim or require uniform HTTP 404 in disabled mode.** The non-negotiable disabled-mode security guarantee is that **no administrative router, handler, credential validation, or diagnostic collection is running**. This is an explicit, narrow amendment to the previous disabled-route HTTP-status requirement, not permission to weaken enabled-mode administrative route reservation.

The owner authorizes a **targeted offline repair** of the previously implemented Release 1.14 WP07 administrative REST candidate, with focused synthetic regression verification and **one bounded local Linux descriptor-only subprocess experiment**. This does **not** authorize a Docker build/run, full local-container requalification, Azure operations, deployment, production secrets, Worker execution, SQLite access, GitHub mutation, or public activation.

## Baseline and preservation

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Reported baseline HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- The existing administrative implementation and earlier unrelated tracked/untracked work are **uncommitted**. Inspect `git status --short`, branch and HEAD before and after; preserve all pre-existing changes. Do not reset, clean, stash, commit, push, or open PRs.
- If baseline differs materially or an authorized file contains unexpected concurrent edits, STOP and report rather than overwriting.

## Evidence and hypotheses to address

The previous read-only diagnosis established:

1. **Proven descriptor-remapping edge-case defect:** `container/admin_diagnostics_bootstrap.py` can duplicate an endpoint to the same descriptor number as the target, while leaving close-on-exec set. A presentation lifecycle channel can therefore disappear across `exec`. This is the **leading hypothesis**, not yet the demonstrated root cause of the earlier enabled-container exit 64 (4.410 seconds).
2. **Proven keep-caps constant defect:** Linux `PR_GET_KEEPCAPS=7`, `PR_SET_KEEPCAPS=8`; the bootstrap used the former where it intended the latter. Correct operation and error handling; verify relevant post-drop security expectations without expanding privileges.
3. **Proven disabled-route conflict:** direct Streamlit owns port 8501 in disabled mode, so the inactive ASP.NET handler cannot enforce 404. Option A explicitly accepts non-collecting Streamlit fallback.
4. Generic `AIQ_UI_OUTCOME=Unavailable` / `AIQ_ADMIN_OUTCOME=Unavailable` logs do not distinguish bootstrap, agent and .NET startup failures. Add **fixed, sanitized stage codes** that permit failure localization without sensitive data.

Do not present the runtime root cause as confirmed unless newly authorized evidence establishes it.

## Exact authorized mutation allowlist

Modify only the following existing paths, and only as needed for the narrowly specified repair:

```text
container/admin_diagnostics_bootstrap.py
container/presentation_lifecycle_agent.py
src/AIQuantTradingResearch.AdminDiagnostics/Program.cs
python/presentation/test_admin_diagnostics_bootstrap.py
python/presentation/test_presentation_lifecycle_agent.py
tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsRoutingTests.cs
```

**Documentation amendment:** The owner has approved Option A as the governing contract in this prompt. Do not create or edit a documentation file without a separate exact literal-path authorization. If a persisted repository contract/document must change before implementation can proceed safely, STOP and request that path. Likewise STOP if repair requires changes to any other path, including entrypoint, Dockerfile, dependency manifests, Worker, provider or SQLite code.

## Repair requirements

### A. Descriptor inheritance and confinement

- Correct the mapping of the **final** fixed descriptors across all cases: source equals target, swapped/overlapping descriptor numbers, close-on-exec, and missing or invalid channel.
- Explicitly set and verify final descriptor inheritability as appropriate; ensure only approved descriptors survive `exec` and unrelated descriptors are closed.
- Preserve the credential pipe's creation **after** the presentation fork, fixed-length 32-byte handoff and EOF validation. Never expose secret bytes through test output, command lines, environment, files or logs.
- Do not introduce generic FD/PID/path parameters, arbitrary execution, or a resident privileged helper.

### B. Keep-caps correctness

- Use the correct `PR_SET_KEEPCAPS` operation and check failure returns; verify relevant privilege-drop assumptions.
- Preserve dedicated admin candidate `1100:1100`, presentation identity, no supplementary groups, no retained capabilities, `NoNewPrivs` expectations and trusted-code immutability checks.
- No relaxation of any fail-closed security condition to make startup succeed.

### C. Sanitized startup diagnostics

- Add a small fixed enumeration of startup-stage failure codes sufficient to distinguish bootstrap descriptor setup, presentation-agent initialization/channel, .NET credential/channel initialization, and readiness/lifecycle failure.
- No exception messages, stack traces, credential values/digests, environment contents, raw descriptor lists, arbitrary paths, or sensitive metadata in logs or HTTP responses.
- Keep output bounded and deterministic; no debug mode or detailed logging switch in public runtime.

### D. Option A disabled-mode contract

- Preserve **direct Streamlit** on `0.0.0.0:8501` when disabled, without starting router/admin or lifecycle agent.
- Accept generic Streamlit fallback, including possible HTTP 200, for `/admin/...` in disabled mode. Do **not** fabricate a disabled 404 via unsupported Streamlit internals.
- Ensure tests distinguish an isolated ASP.NET handler's disabled behavior from the actual disabled startup path. The handler's own disabled 404 behavior may remain as defense-in-depth, but is **not** an end-to-end disabled-mode guarantee.
- In **enabled mode**, preserve `/admin` route reservation, no fallback to Streamlit, authentication-before-collection and header stripping.

## Authorized verification

- Run only targeted Python unit tests for bootstrap and presentation lifecycle agent, and targeted .NET administrative routing tests (or the already approved finite administrative synthetic suite if needed for regression).
- Authorize **at most one** Linux descriptor-only subprocess scenario using harmless `socketpair`/pipe fixtures, to observe descriptor type, inheritability and survival across `exec`. No secret values, Docker, app startup, Worker, database, network listener or external service.
- Keep tests finite (suggested total ceiling: 15 minutes), no retries/polling loops or expanded suites without owner approval. Preserve the repository's existing build/signing conventions.
- `git diff --check`; inspect changed-path list against the literal allowlist; record pass/fail/NOT_RUN without inflating claims.
- STOP on any credential exposure, privilege weakening, unexpected dependency/permission change, new path requirement, unrelated regression, or inability to demonstrate bounded behavior.

## Unchanged security and scope boundaries

- `AIQ_ADMIN_DIAGNOSTICS_ENABLED` must be exactly lowercase `true` to enable; missing/malformed configuration remains fail-closed to the disabled launch path as previously designed.
- Azure App Service application settings remain the preferred secret store; no real keys may be read or provisioned under this authority.
- Retain the TLS-denial policy. **Do not** trust arbitrary forwarded TLS headers, bypass HTTPS checks or make the administrative endpoint reachable through an unqualified ingress.
- No arbitrary shell, SQL, Worker invocation, private provider access, database or sidecar reads, arbitrary PID/path inspection, or new persistent store.
- No Docker/container execution or image rebuild; actual UID/FD isolation, PID 1 behavior, WebSockets, TLS and Azure F1 overhead remain **NOT QUALIFIED**.
- Multiple-instance/revision-overlap analysis remains deferred for this diagnostic API phase; no public Worker gate change.

## Required Codex report and STOP

Return a concise, source-indexed report containing:

1. Baseline/working-tree preservation evidence and exact changed files.
2. Root-cause classification: **source defect proven** vs **runtime cause still unconfirmed** unless directly demonstrated.
3. Descriptor mapping and keep-caps repair explanation, including explicit regression coverage.
4. Option A contract reconciliation and any documentation-path approval still needed.
5. Fixed diagnostic-stage codes and why they cannot leak sensitive data.
6. Exact test commands/results, subprocess scenario count, elapsed time, `git diff --check`, and failures/NOT_RUN.
7. Remaining security gaps and **GO/NO-GO for requesting a separate bounded local-container requalification**, not Azure deployment.

Then **STOP for owner review**. Do not commit or deploy.

**Release gates remain:** `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability **NO-GO**; Azure passive qualification **NO-GO**; public Worker activation **NO-GO**.

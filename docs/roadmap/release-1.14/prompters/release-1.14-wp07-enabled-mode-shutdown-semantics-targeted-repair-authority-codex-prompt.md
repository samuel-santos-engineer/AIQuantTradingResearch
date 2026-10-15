# Release 1.14 WP07 — Administrative REST Enabled-Mode Shutdown Semantics: Targeted Repair Authority

## Owner decision and objective

Authorize a **narrow offline source-and-test repair** to make enabled-mode shutdown outcomes truthful and bounded. The owner-executed disabled-mode qualification passed; **do not modify disabled-mode behavior**. The earlier enabled container ran successfully but exited 64 on shutdown; source inspection established an unconditional `return 64` in `Program.cs`, while lifecycle acknowledgment and child reaping remain unqualified. A change to the return code alone is not an acceptable repair.

This is **not** authorization to deploy, operate Docker, access Azure, activate public Workers, or declare administrative API availability. `PUBLIC_RUNTIME_VERIFIED=False` remains mandatory.

## Baseline and preflight STOP gates

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected baseline HEAD: `67893f51931ee183f71050a83b81127e6e648b87` (HEAD may still be this commit despite existing dirty worktree).
- Before edits, record `git branch --show-current`, `git rev-parse HEAD`, `git status --short`, and relevant diffs. **STOP** if branch/HEAD differ; do not reset, clean, stash, commit, checkout, or overwrite existing changes.
- Review existing implementation and tests before editing. Preserve all unrelated modifications and untracked files. Distinguish preexisting modifications from this authority's edits.
- Do not inspect, print, store, or generate production secrets. No DB, Worker, private provider, or external service access.

## Exact permitted modification envelope

Only the following literal paths may be edited, and only as necessary for shutdown semantics and focused synthetic tests:

1. `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`
2. `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`
3. `container/presentation_lifecycle_agent.py`
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs`
5. `python/presentation/test_presentation_lifecycle_agent.py`

Read-only inspection of directly relevant project files is permitted. Do not edit bootstrap, entrypoint, Dockerfile, configuration, dependency manifests, API authentication, routing, notices, documentation, other tests, or release governance. If a necessary repair requires another path, **STOP and request separate owner authority**.

## Required implementation properties

1. **Truthful exit status:** An orderly enabled shutdown may return `0` only after required router-host shutdown and lifecycle-agent stop protocol complete successfully. Preserve nonzero failure status for startup failures, failed host shutdown/disposal, missing or malformed stop acknowledgment, deadline exhaustion, or unexpected agent termination. Avoid treating cancellation due to a legitimate stop request as an automatic failure; distinguish it from cancellation that interrupts required shutdown steps.
2. **Bounded lifecycle handshake:** Explicitly track `STOP_UI` request, `STOPPED` acknowledgment, channel EOF, malformed frame, timeout, and agent failure as distinct sanitized outcomes. Do not swallow exceptions in a way that converts an unverified stop into success. Never emit exception text, credential material, descriptor contents, arbitrary environment values, or raw paths.
3. **Verified agent termination:** Do not equate a single nonblocking reap with a completed child exit. Use a bounded wait/reap strategy appropriate to the existing Model A architecture. Report missing child termination as failure; avoid zombies and unbounded waits. Do not introduce cross-UID signaling or a resident root helper.
4. **Finite time budget:** Explicitly account for host stop, agent stop and disposal under a single bounded shutdown plan; avoid consuming a shared deadline before sending `STOP_UI`. Preserve compatibility with the existing five-second Docker stop grace without asserting a guarantee on F1. If the design cannot safely fit the budget, **STOP** and explain the required owner decision instead of silently lengthening the Docker deadline.
5. **Security and compatibility:** Preserve distinct UID/GID roles, fixed protocol messages, secret confinement, no extra descriptor inheritance, no restart loop, no new network endpoints, HTTP/WebSocket behavior, existing TLS denial policy, and disabled-mode **Option A** (direct Streamlit on 8501, no admin router/collector; generic UI fallback including 200 permitted).
6. **Failure reporting:** Use fixed, bounded, non-sensitive outcome/stage identifiers; document in the final report which statuses prove success and which represent incomplete evidence. Do not introduce verbose diagnostics or credential-dependent tests.

## Focused offline tests

Add/update synthetic regression tests in the two authorized test files for, where feasible:

- Successful orderly host stop + `STOP_UI`/`STOPPED` + confirmed child termination yields exit `0`.
- Missing acknowledgment, EOF, malformed response, canceled operation, deadline expiration, and agent exit failure yield nonzero status and fixed sanitized outcomes.
- Host shutdown/disposal failure does not become success.
- Termination race and bounded reaping; no infinite waits or uncontrolled retry.
- Preservation of disabled Option A and no sensitive output (inspect existing tests; do not edit unrelated test files).

Use existing installed tools and dependencies; no package restore from network and no new dependencies. Run the narrow existing Python and .NET synthetic test suites if feasible offline, plus `git diff --check`. If a test cannot run without network, Docker, privilege escalation, or modifying unauthorized paths, mark **NOT_RUN** and STOP as appropriate. Do not claim runtime/container qualification from synthetic passes.

## Explicit prohibitions

- **No Docker commands, image builds, container starts, or host-side privileged scripts.**
- No Azure access/deployment, GitHub pushes/PRs/commits, database access, Worker activity, network package installation, secrets, elevated permissions, or broad process inspection.
- No source modifications outside the five named paths.
- No changing disabled-mode implementation or Option A contract.
- No altering ingress TLS trust rules, auth, key handling, credential collection, or public Worker gates.
- Any PowerShell script authored incidentally must be compatible with **Windows PowerShell 5.1.26100.9444**; avoid PowerShell 7 syntax. No PowerShell script is requested by this authority.

## Final evidence report and STOP

Report:

1. Baseline branch/HEAD and before/after Git status; precise authorized files changed.
2. Source-indexed explanation of the original unconditional exit 64 and the repaired shutdown state transitions.
3. Test commands, counts and outcomes; `git diff --check`; any untested scenarios.
4. How normal exit `0` is established, how failures remain nonzero, and the finite shutdown budget.
5. Explicit residual uncertainties: actual local-container graceful shutdown, full descriptor/credential confinement, complete trusted-dependency immutability, and Azure ingress TLS trust remain **NOT QUALIFIED** unless separately evidenced.
6. Recommendation for a separate bounded enabled-mode container requalification; do not execute it.

**STOP for owner review.** Do not commit or deploy. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**.

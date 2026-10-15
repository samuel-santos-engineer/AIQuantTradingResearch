# Codex Authority — Release 1.14 WP07: Agent-Termination Diagnostic Instrumentation Only

## Authority and objective

Owner authorizes a **narrow, offline diagnostic-instrumentation and synthetic-test change** to explain the observed enabled-mode shutdown failure. This is **not** authority to repair process ownership, change shutdown success criteria, execute Docker, repeat host qualification, deploy, or activate any public functionality.

Observed owner-run facts: readiness, router/presentation identities, and unauthenticated `/admin/c1a` denial PASS; UI `Terminated`/`StopAcknowledged`, router `StopAcknowledged` and `ChannelClosed`; then twice `AIQ_ADMIN_SHUTDOWN=AgentTerminationUnverified`, `AIQ_ADMIN_FAILURE=Shutdown`, container exit **64**, OOM false, no forced termination indicated. Owner report run ID `a0bf4ef706c6443f9cdbe217e3136b4e`; approximately 3 ms from channel EOF to first unverified event. A competing .NET PID 1 child reaper is **supported but not proven**; native wait result/errno was not recorded. Preserve this uncertainty.

## Mandatory preflight / STOP gates

1. Confirm repo `C:\projects\github\AIQuantTradingResearch`, branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. If mismatch, **STOP**; do not checkout, reset, merge, rebase or stash.
2. Capture full pre-existing `git status --porcelain=v1` and a SHA-256 manifest of all pre-existing files. The worktree is intentionally dirty; preserve every unrelated byte. If the three allowed files have unexpected changes or ownership ambiguity, **STOP** and report, rather than overwriting.
3. Inspect existing implementations and tests before editing. Confirm exact diagnostic paths and .NET interop error retrieval behavior. Do not assume the 404 from a prior remote branch lookup proves anything about local source.
4. Do not execute any Docker, Azure, HTTP runtime probe, container workload, Git remote operation, dependency restore/update, or qualification script. Do not modify or create any files outside the three allowlisted paths. If an offline test would write generated files outside the allowlist, use an already-existing safe mechanism that does not persist such changes, or **STOP**.

## Exactly three allowed files

- `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`
- `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`
- `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs`

No edits to `container/presentation_lifecycle_agent.py`, bootstrap, Dockerfiles, entrypoints, protocols, build configuration, scripts, runbooks, Python tests, README, GitHub metadata or release gates. No new files.

## Authorized implementation

**A. Native wait evidence:** At the actual agent-specific verification site, retain the observed `waitpid` return value, target/returned PID classification, and immediately captured native error (using the correct interop/last-error contract) before any further native call can overwrite it. Distinguish at minimum: no exited child yet; observed child exited 0; observed child exited nonzero/signaled; `ECHILD`; `EINTR` retry; other errno; interop/managed exception; deadline/cancellation. Do not publish raw PID values, paths, environment, FDs, credentials, exceptions or stack traces. Use a bounded, fixed allowlist of sanitized diagnostic codes. If native errno cannot be reliably captured with the existing P/Invoke contract, **STOP** rather than inventing a classification.

**B. Failure semantics:** Preserve the current fail-closed exit-0 proof requirement. `ECHILD`, process disappearance, EOF, `STOPPED`, acknowledgment, or timeout are **never** success evidence. Do not alter which process owns reaping, replace native waiting with a lookup, extend the deadline, relax the shutdown grace period, change signals, or suppress errors. Do not create a resident root helper or introduce privileged inspection.

**C. Logging:** Ensure one precise sanitized diagnostic event for the underlying verification failure and a distinct final failure summary, without emitting misleading duplicate identical step events. Preserve compatibility with the existing bounded allowlisted logging/evidence collection. If a new code would be dropped by the current host qualification script, report that limitation; do **not** modify the script under this authority. Never emit secrets, raw process details or variable exception text.

**D. Runtime identification:** Capture the exact managed runtime version via safe in-process API if it can be emitted as a bounded, non-sensitive, fixed-format diagnostic without expanding existing log exposure. Otherwise document why it was not added. Do not inspect environment variables, credentials, process memory, descriptor contents or `/proc` broadly.

**E. Synthetic tests:** Add or adjust deterministic offline unit tests in the allowlisted C# test file for all diagnostic classifications, correct native-error preservation, EINTR behavior, exception fallback, nonzero/signaled child, cancellation/deadline, no success on ECHILD, and duplicate-reporting behavior. Tests must not claim to reproduce .NET PID 1 reaping or prove the cause of the owner's run. Prefer injectable/queued synthetic observations already used in the suite; no Docker or real child process required.

## Validation and evidence

- Run only already-available offline checks that do not restore packages, alter dependencies, create unapproved files or run containers. If a validation tool cannot satisfy this, mark **NOT_RUN** with reason; never broaden authority to make it pass.
- Verify targeted diffs, whitespace and changed-path allowlist; compare the pre-existing SHA-256 manifest and Git status, distinguishing the three authorized edits from any unrelated change.
- Report test counts and exact commands; distinguish PASS, FAIL and NOT_RUN. Identify whether the host evidence collector would capture the new sanitized codes, and explicitly state whether native error codes will be observable in a future run.
- Produce a concise owner-facing summary in the Codex response (no new report file): source locations, diagnostic mapping, unchanged exit semantics, validation evidence, any blockers, and a recommendation for a separately authorized next phase if warranted.

## Absolute exclusions

No Docker builds/starts/stops; no host-run retries; no live process probing; no Azure resource changes; no deployment; no Git commit/push; no dependency or schema modifications; no Worker/SQLite/private provider access; no authentication or TLS relaxation; no WebSocket qualification; no administrative API activation; no production process-reaping fix. Do not change the credential-delivery design. All PowerShell used must be compatible with **Windows PowerShell 5.1.26100.9444**.

## Final disposition

**STOP for owner review** after offline diagnostics and tests. No authority for further execution is implied. Keep `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**. The root cause of the specific owner run remains **UNDETERMINED** until new admissible evidence establishes it.

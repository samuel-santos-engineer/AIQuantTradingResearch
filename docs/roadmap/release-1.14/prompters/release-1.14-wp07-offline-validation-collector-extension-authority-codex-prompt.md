# Codex Authority — Release 1.14 WP07: Offline Validation and Sanitized Collector Extension

## Authority type and objective
Narrow, owner-approved **offline validation plus qualification collector hardening** only. Do not perform a Docker qualification run. Validate the previously implemented three-file agent-termination diagnostics and make their fixed, sanitized event categories observable in the existing host-executed qualification report. The observed shutdown failure remains unqualified and its cause **UNDETERMINED**.

## Mandatory preflight and STOP gates
1. Verify repository `C:\projects\github\AIQuantTradingResearch`, branch `draft/release-1.14-wp07-partial-public-disabled`, and HEAD `67893f51931ee183f71050a83b81127e6e648b87`. If different, **STOP**; do not checkout/reset.
2. Capture current `git status --porcelain=v1`, all pre-existing file hashes, and existing artifact contents. The working tree is intentionally dirty. Preserve every existing change; do not stash, clean, reset, revert, commit, push, or delete existing generated files.
3. Inspect the diagnostic event strings and test implementation in the current three source/test files before touching the collector. If diagnostic categories cannot be safely matched by exact grammar, **STOP** and propose a bounded design; never fall back to arbitrary log capture.
4. Verify no concurrent qualification activity and that no path to be edited has unrecognized changes conflicting with this authority. Stop rather than overwrite or broaden scope.

## Exact allowed modifications
Only these two existing files may be edited:
- `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification.ps1`
- `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification-RUNBOOK.md`

The previously instrumented files below are **read-only inputs**, not editable under this authority:
- `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`
- `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`
- `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs`

No other tracked source, project, package, configuration, lockfile, or repository artifact may be modified. Temporary compiler/test outputs are allowed **only** in a new unique disposable directory outside the repository, with explicit output/intermediate locations; do not overwrite existing directories. If the available build/test commands cannot enforce this confinement, **STOP without executing them** and report the blocker. A test harness outside the repository may be transient, but no new permanent script is authorized.

## Task A — Offline .NET validation
- Before invoking dotnet, determine and document exact commands, resolved SDK/runtime, all output and intermediate paths, and whether NuGet restore would be attempted. **No network restore or dependency changes**; use existing cached dependencies only. If an offline build cannot be guaranteed, STOP and report `NOT_RUN` rather than contacting package sources.
- Compile the instrumented .NET production/test code and run the relevant existing test suite, including the 22 new synthetic cases, with all generated build artifacts confined to the unique temporary root. Do not run Docker, container workloads, production processes, Azure, HTTP against the deployed app, or Worker components.
- Check native return/status/errno snapshot ordering, `ECHILD` failure semantics, `EINTR` handling, exception categories, and unchanged exit-0 requirements. If tests fail, report the exact sanitized failure and STOP; do not repair production code within this authority.
- Preserve relevant counts, test names/categories, compiler warnings, and bounded nonsecret results. Never copy credentials, environment dumps, raw process command lines, or private descriptor content into evidence.

## Task B — Extend the host qualification collector safely
- Modify only the existing PowerShell 5.1 script to recognize **exact fixed-format, bounded, sanitized** events emitted by the existing instrumentation under the prefixes `AIQ_ADMIN_WAIT`, `AIQ_ADMIN_RUNTIME`, and `AIQ_ADMIN_SHUTDOWN_FINAL`. Validate the **full event grammar** and any fixed numeric values against the actual code; do not merely allow any line starting with these prefixes.
- Retain existing shutdown-stage event collection and its security restrictions. Preserve the 64 requested log lines, 16 KiB accepted-text limit, 24 report-event cap, and sanitized no-secret reporting. If the 24-event cap could suppress the new failure evidence, prioritize critical shutdown diagnostics deterministically within that cap, documenting the selection rule; do not silently raise caps.
- Reject malformed, oversized, multiline, unknown-category, unbounded numeric, or nonallowlisted events. No raw logs in the report, console, test output, or fallback. No environment values, secrets, process arguments, FD contents, memory, paths from raw logs, or hashes of credentials.
- Preserve current shutdown PASS semantics: `ECHILD`, missing wait status, agent disappearance, EOF, and acknowledgment **never** count as confirmed agent exit 0. No changes to container build/start/stop counts, resource limits, credential generation/delivery, Docker invocation, privilege model, observation probes, deadlines, or cleanup permissions.
- Add Docker-free positive/negative collector checks, including each recognized event category, malformed values, truncation, event-cap prioritization, and proof that no diagnostic failure can be promoted to `ShutdownProtocol=PASS` or overall PASS. Run under **Windows PowerShell 5.1.26100.9444** when available; otherwise mark host-specific checks `NOT_RUN` and STOP for owner review. Avoid PowerShell 7-only syntax; keep `[CmdletBinding()]`/`param()` valid and `$LASTEXITCODE` explicit.
- Update the runbook with exact recognized diagnostic event shapes, their interpretation and limitations, test evidence, and instructions to review sanitized report artifacts. Make clear that the collector exposes a future failure mechanism but does not prove which reaper consumed status.

## Validation and preservation
1. Parse the edited PowerShell file with the Windows PowerShell 5.1 parser, run permitted Docker-free collector tests, and verify the original runtime qualification entrypoint was **not invoked**.
2. Validate whitespace, changed-path allowlist, original Git branch/HEAD, exact before/after Git status for pre-existing changes, and hashes of every protected file. Only the two explicitly editable files may differ; preserve any existing dirty content within them except narrowly authorized changes.
3. Remove only uniquely owned temporary test outputs after recording bounded results. Do not delete user files, NuGet caches, Docker resources, or other temp directories. If cleanup is uncertain, report exact paths and STOP.
4. Report compilation/test counts, test limitations, event-grammar cases, modified lines, and outstanding unqualified obligations. Never claim runtime proof from synthetic tests.

## Hard prohibitions
No Docker calls or container runs; no second owner qualification; no Azure CLI or deployments; no paid resources; no Worker activation; no real API credentials; no Git commits/pushes; no production source/test edits; no new packages, dependency updates, schema changes, or privilege/ACL/elevation changes. Do not relax TLS, authentication, agent exit-0 proof, or any existing NO-GO gate.

## Required final response and STOP
Provide: (1) baseline and preservation results; (2) precise two-file diff summary; (3) .NET compile/test results or a clear STOP reason; (4) PowerShell 5.1 parser and Docker-free collector test results; (5) accepted/rejected diagnostic grammars and cap-priority behavior; (6) known limitations and the next **separate owner review** gate. **STOP after reporting.** No authorization to execute qualification or repair process ownership.

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, Azure passive qualification, and public Worker activation remain **NO-GO**.

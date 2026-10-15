# Codex Authority — Release 1.14 WP07: Read-Only Agent-Termination Diagnosis

## Authority and objective

**READ-ONLY DIAGNOSIS ONLY.** Investigate why the first owner-executed enabled-mode host qualification reported `AIQ_ADMIN_SHUTDOWN=AgentTerminationUnverified`, followed by `AIQ_ADMIN_FAILURE=Shutdown` and container exit **64**, despite successful UI stop acknowledgment and shutdown channel closure. Produce a bounded, evidence-backed diagnosis and proposed *separate* repair scope, if justified. **Do not implement a fix.** STOP for owner review.

## Fixed baseline and non-negotiable constraints

- Repository: `C:\projects\github\AIQuantTradingResearch`.
- Required branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Baseline HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty. Never clean, stash, reset, checkout, stage, commit, push, or rewrite any tracked or untracked file. If branch/HEAD differ, STOP and report. Existing dirty changes are evidence, not an invitation to repair.
- **No edits or new repository artifacts whatsoever**, including generated files, caches, build outputs, test results, or formatting. Do not run build/test commands that may write to the repository. Any optional diagnostic report is returned in the Codex response only.
- **No Docker commands**, container runs, builds, image/container cleanup, HTTP probes, Azure calls, deployments, Worker activation, credential experiments, or package installs. No privilege/ACL/group/execution-policy/context changes. No second qualification run.
- Do not inspect or reveal credential values, environment contents, private process memory, descriptor contents, or non-allowlisted logs. No authenticated administrative requests.
- Preserve Option A disabled-mode direct Streamlit PID 1 semantics and enabled-mode Model A trust/UID separation. Do not relax confirmed agent exit 0, shutdown protocol, five-second stop grace, or any existing NO-GO gate.
- If using PowerShell for read-only inspection, target **Windows PowerShell 5.1.26100.9444**; avoid PowerShell 7-only syntax.

## First-run evidence (owner-provided; treat as observed, not as proof of cause)

The owner ran exactly one bounded host qualification on 2026-10-12. Sanitized report directory:
`C:\Users\sabsf\AppData\Local\Temp\aiq-wp07-enabled-host-a0bf4ef706c6443f9cdbe217e3136b4e`.

Relevant observations from `report.json`:

- `Result=FAIL`, `Readiness=PASS` at approximately **9.056 seconds** after container start; root Streamlit markup and health HTTP 200; router and presentation identities PASS; unauthenticated `/admin/c1a` returned HTTP 503, empty body, `no-store`.
- Stop requested approximately **10.704 seconds** after start; Docker stop grace **5 seconds**; stop completed in approximately **1.694 seconds** with **exit 64**, **OOM false**, `ForcedTerminationConsistent=false`.
- Sanitized shutdown event order (UTC):
  - `22:44:48.920432` `AIQ_UI_STOP=StopRequested`
  - `22:44:48.924093` `AIQ_ADMIN_SHUTDOWN=StopRequested`
  - `22:44:49.286303` `AIQ_UI_STOP=Terminated`
  - `22:44:49.286347` `AIQ_UI_STOP=StopAcknowledged`
  - `22:44:49.301982` `AIQ_ADMIN_SHUTDOWN=StopAcknowledged`
  - `22:44:49.302009` `AIQ_ADMIN_SHUTDOWN=ChannelClosed`
  - `22:44:49.305421` `AIQ_ADMIN_SHUTDOWN=AgentTerminationUnverified`
  - `22:44:49.348605` `AIQ_ADMIN_SHUTDOWN=AgentTerminationUnverified` (repeated)
  - `22:44:49.349946` `AIQ_ADMIN_FAILURE=Shutdown`.
- Expected `AIQ_ADMIN_SHUTDOWN=AgentReaped` and `AIQ_ADMIN_SHUTDOWN=CleanStop` **not observed**; `Stop=FAIL`, `ShutdownProtocol=FAIL`.
- `Cleanup` context/container/image PASS; `Preservation=PASS`; `WebSocket=NOT_RUN`; `PUBLIC_RUNTIME_VERIFIED=False`.

The owner has shared the report in conversation. If the local report is accessible, inspect only `report.json` and only sanitized fields needed for the diagnosis. If inaccessible, use the above evidence and clearly state that limitation. Do not read full raw container logs.

## Authorized read-only inspection

1. **Baseline preflight:** Record branch, HEAD, `git status --porcelain` and relevant working-tree paths without modifying anything. Do not assume clean status.
2. **Trace exact termination logic** in these existing files, if present:
   - `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`
   - `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`
   - `container/presentation_lifecycle_agent.py`
   - `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs`
   - `python/presentation/test_presentation_lifecycle_agent.py`
   Read the qualification script and runbook **only as necessary** to establish stop invocation, log-event classification and reporting semantics:
   - `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification.ps1`
   - adjacent `release-1.14-wp07-enabled-shutdown-qualification-RUNBOOK.md`.
3. Identify the **precise code path** emitting `AgentTerminationUnverified` and `AIQ_ADMIN_FAILURE=Shutdown`; include path and line numbers and the predicates, timeout budgets, process handles, wait/reap APIs and cancellation interactions governing that path.
4. Explain the relationship between `STOP_UI` / `STOPPED` acknowledgment, channel EOF, agent process termination, successful exit status, and router PID 1 exit status. Distinguish observed events from inferred internal state.
5. Examine Linux process ownership and parent/child relationships: which process is the actual parent, who may call `waitpid`/equivalent, whether .NET process APIs can legitimately reap or inspect the agent, whether PID reuse or `/proc` access restrictions affect evidence, and whether the expected handle/FD survives bootstrap exec. Ground each finding in actual source, not assumptions.
6. Examine **races and deadline accounting** around channel closure, agent exit, process-reaping observation, host shutdown and duplicate `AgentTerminationUnverified` events. Assess whether ~3 ms between channel close and first failure is meaningful in the implementation; do not assert causation based on timing alone.
7. Check tests for this exact success/failure path and any mocks that might hide actual OS parentage, exit-code observation, or post-EOF races. **Read tests; do not run them.**
8. Evaluate at least these hypotheses, classifying each as **SUPPORTED**, **PLAUSIBLE / UNPROVEN**, or **CONTRADICTED** with cited code/evidence: (a) agent exited 0 but verification raced, (b) agent exited nonzero, (c) router cannot reliably reap/observe the agent due to parentage/handle semantics, (d) agent remained alive after channel EOF, (e) stop deadline/cancellation caused premature unverified classification, (f) evidence/logging duplicated one failure without two distinct shutdown attempts.

## Required response format

Return a concise structured diagnosis with:

1. **Scope and baseline:** branch/HEAD, working-tree preserved, exact files inspected, any inaccessible evidence.
2. **Observed facts:** report-derived timeline and qualified outcomes, clearly separate from hypotheses.
3. **Code-path map:** source path:line references for every relevant transition and failure predicate.
4. **Hypothesis matrix:** supported/plausible/contradicted and strongest confirming/refuting evidence for each.
5. **Root-cause confidence:** either a specifically evidenced cause or `UNDETERMINED`, with the minimal missing proof. Do not label speculation as confirmed.
6. **Proposed next authority (not executed):** smallest possible source/test change or further *read-only* evidence collection, precise file allowlist, intended invariants, regression tests and STOP conditions. Do not weaken verification to make exit 0 easier.
7. **Final gate statement:** no changes made, no Docker or workload run, no retry authorized, `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**.

If inspection reveals a need for unapproved access, a changed baseline, or a necessary write, **STOP**, describe the blocker, and request explicit new authority. Do not improvise around it.

# Codex Authority — Release 1.14 WP07: Read-Only Process-Ownership Design Investigation

## Authority class and decision boundary

**READ-ONLY DESIGN / NO IMPLEMENTATION.** Investigate reliable agent exit-status ownership after the second owner-run enabled shutdown qualification. Produce a source-grounded design assessment and a proposed smallest subsequent implementation authority, **not code**. STOP for owner review. This authority does not permit a third qualification run.

## Fixed baseline and preservation

- Repository: `C:\projects\github\AIQuantTradingResearch` (`samuel-santos-engineer/AIQuantTradingResearch`).
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty. Record initial branch, HEAD and `git status --porcelain=v1`; preserve all tracked/untracked files and their contents. Never reset, clean, stash, checkout, stage, commit or push. STOP on a baseline mismatch or unexpected mutation.
- Read-only inspection of source, tests, configuration, documentation, local available runtime source and already-produced sanitized reports is permitted. No writes to repository or build outputs; no compilation, test execution, Docker, network dependency restoration, HTTP probing or Azure calls. If an operation may write caches or generated files, do not perform it.

## Evidence to explain — do not relabel as successful

Second owner-run report (`aiq-wp07-enabled-host-1f613f2082774bde811fd5539408a839`) recorded:

```
AIQ_ADMIN_RUNTIME=10.0.12
AIQ_UI_STOP=Terminated
AIQ_UI_STOP=StopAcknowledged
AIQ_ADMIN_SHUTDOWN=StopAcknowledged
AIQ_ADMIN_SHUTDOWN=ChannelClosed
AIQ_ADMIN_WAIT=TARGET_ANY_RETURN_ERROR_ECHILD
AIQ_ADMIN_SHUTDOWN=AgentTerminationUnverified
AIQ_ADMIN_SHUTDOWN_FINAL=AgentTerminationUnverified
AIQ_ADMIN_FAILURE=Shutdown
```

Container exit 64, OOM false, forced termination not indicated, stop approximately 1.119 seconds. The preceding qualification also exited 64 after `AgentTerminationUnverified`, without native errno instrumentation. Readiness, router/presentation identities, local unauthenticated admin denial and preservation had passed. **Confirmed:** the application's `waitpid` returned `ECHILD` and did not establish the agent's exit status. **Hypothesis, not yet proven for this run:** the .NET runtime's PID 1 reaper consumed the agent status first. Do not infer agent exit 0 from `ECHILD`, EOF, acknowledgment, disappearance or a successful UI stop.

## Read-only investigation tasks

1. **Reconstruct ownership:** Trace the complete process tree and handoff from `container/` entrypoint/bootstrap through `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`, `PresentationLifecycle.cs` and `container/presentation_lifecycle_agent.py`. Identify who forks, who becomes PID 1, who is a direct child, who invokes `waitpid`, and the actual mechanism by which each exit status can be obtained. Trace existing `SIGCHLD` handling and the finite four-second shutdown deadline. Cite exact repository file paths and line numbers.
2. **Runtime competition:** Analyze the actual .NET runtime version reported in-container (`10.0.12`) and the relevant native Unix signal/reaping implementation. Distinguish evidence verified in available source from assumptions derived from another runtime patch version. If current runtime source is not locally available without network or cache mutation, state that limitation; do not claim independent verification. Examine whether managed `Process` APIs can track an agent created outside managed process spawning and whether they retain its exit code.
3. **Design alternatives:** Compare at least three plausible ownership models, including (a) a single reaping authority compatible with .NET PID 1, (b) an agent-specific exit-status mechanism that does not compete with a global reaper, and (c) a process supervision/parentage adjustment. Evaluate Linux process semantics, `waitpid`/`waitid`/pidfd limits where relevant, PID reuse, race conditions, the .NET runtime's reaper, status provenance, direct-child requirements, and failure behavior. Do not propose a mechanism as proven merely because it can detect disappearance. Identify any model that requires privileged code, persistent root, cross-UID signaling, bootstrap/protocol changes, new FDs or changes to PID 1.
4. **Threat and operational boundaries:** Preserve enabled-mode Model A: router PID 1 UID/GID 1100:1100; presentation lifecycle agent and Streamlit UID/GID 1000:1000; root only in finite bootstrap, no resident root helper; no broadened capabilities, mounts, Docker socket or process visibility; credential/FD confinement; no secret in inspect-visible Docker configuration or logs. Preserve four-second coordinated shutdown and five-second Docker stop grace. Preserve disabled-mode Option A: direct Streamlit PID 1 on 8501, UID/GID 1000:1000, with generic `/admin` UI fallback permitted.
5. **Verification plan:** Specify minimal offline deterministic tests for success, nonzero exit, reaper race, `ECHILD`, `EINTR`, stale/reused PID, agent hang and protocol EOF-before-exit. Separate mock tests from any real Linux PID 1 integration evidence; explicitly identify what remains unqualified until separately approved owner-executed Docker testing. Never relax exit-0 proof or reinterpret an unknown status as PASS.
6. **Smallest proposed repair boundary:** Recommend one preferred design only if supported by source and runtime semantics. List exact files that would need modification, compatibility consequences, migration/rollback plan, test additions, and explicit STOP conditions. If no design is sufficiently justified, recommend a further bounded read-only evidence step instead of speculative implementation.

## Prohibitions

- No source, test, script, runbook, configuration or documentation modifications.
- No generated files, new branches, Git operations changing state, commits, pushes or PRs.
- No Docker build/start/stop, qualification script invocation, live process experiments, test execution or network activity.
- No Azure operations, public endpoint testing, administrative collections, Worker activation, provider/SQLite access or paid resources.
- No bypass of `ECHILD`, no weakened shutdown PASS conditions, no credential or environment dumping, and no unapproved process architecture change.

## Required final report

Provide: (1) baseline/preservation proof; (2) a process-tree and wait-ownership map with exact source citations; (3) confirmed versus hypothesized runtime behavior; (4) alternatives matrix with security and race tradeoffs; (5) a recommended smallest design or explicit unresolved blocker; (6) exact proposed next-authority file scope and offline/integration test plan; (7) unqualified items and STOP conditions. Clearly state **NO IMPLEMENTATION PERFORMED** and **NO RUNTIME QUALIFICATION PERFORMED**.

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**. **STOP — owner review required.**

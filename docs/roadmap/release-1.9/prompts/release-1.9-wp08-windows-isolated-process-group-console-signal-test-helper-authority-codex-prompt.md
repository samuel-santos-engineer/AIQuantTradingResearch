# Release 1.9 — WP08 Windows Isolated Process-Group Launcher + Console-Signal Test-Helper Authority

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to define and authorize the minimum **Windows test/demo process-launch and signal-delivery helper** needed to safely prove the already-defined Worker graceful-cancellation contract at process level.

It must preserve the already-implemented partial WP08 Worker changes exactly unless repository evidence proves they conflict with binding definitions:

- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

No implementation is authorized here.
No GitHub mutation.
No WP09.

# Proven blocker

The current governed PowerShell launcher cannot safely prove `CTRL_BREAK` delivery to the real Worker because the Worker is not launched in an isolated Windows console process group.

Evidence from the blocked implementation:
- Worker with `--wp08-test-liveness` stays alive.
- A real canonical handoff is published.
- Handoff contract validates.
- Owned-process cleanup works.
- Only forced termination was safely available.
- Forced termination does not satisfy graceful-cancellation acceptance.

Therefore WP08 needs one narrowly governed Windows process-launch/signal helper.

# Entry state

Expected:
- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor 309/309.
- build 0 warnings / 0 errors.
- WP05 Python 3/3.
- WP06 Python 6/6.
- WP07 semantic 2/2.
- WP07 presentation 2/2.
- partial authorized Worker cancellation/liveness implementation exists.
- no WP08 focused test file yet.
- no GitHub mutation from the blocked pass.

Verify read-only.

# Binding authorities

Read and preserve:
1. WP08 lifecycle/bounded-demonstration/process-residue contract.
2. WP08 Worker lifecycle/liveness test-seam + cancellation-adapter contract.
3. current partial Worker implementation.
4. Release 1.9 manifest/path ownership.
5. existing test helper conventions.
6. #233 acceptance criteria.

Do not redefine Worker cancellation semantics.
This authority only defines safe Windows process creation + signal delivery for the test/demo harness.

# Objective

Define one exact Windows-only helper contract that lets the WP08 harness:
1. launch the real Worker executable in a **new isolated process group**;
2. retain an owned process handle/PID/group identity;
3. safely deliver the already-selected standard console control event to that group only;
4. observe graceful Worker exit;
5. fall back to owned-process termination only for cleanup;
6. release all native handles/resources;
7. avoid signaling the test runner or unrelated processes.

Also define exact path/symbol/test authority.

# Phase 0 — Read-only platform inspection

Inspect:
- current Windows target/environment used for local acceptance;
- current process launch helper(s);
- target framework `net10.0`;
- whether P/Invoke is already used in tests;
- repository conventions for Windows-specific helpers;
- architecture tests that might constrain native interop;
- whether `System.Diagnostics.Process` alone can supply required creation flags in this runtime.

No mutation.

# Phase 1 — Windows mechanism selection

Evaluate standard Windows mechanisms.

Preferred design is conceptually:
- `CreateProcessW`
- `CREATE_NEW_PROCESS_GROUP`
- optional `CREATE_NEW_CONSOLE` if required for safe control-event delivery
- process handle + thread handle
- `GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, processGroupId)`

Do not blindly select flags.

Determine exact compatible combination for console-control delivery.

Important constraints:
- target only the owned process group;
- do not broadcast to group 0;
- do not attach the test runner to the Worker console in a way that risks self-signal unless strictly necessary and safely handled;
- no shell-mediated broad signal.

# Phase 2 — Console ownership model

Define exactly one model:

## Model A — inherited console + new process group
Worker shares the current console but has a distinct process group.
Harness calls `GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, workerProcessGroupId)`.
Prove targeted group delivery cannot affect the test runner.

## Model B — new console + new process group
Worker gets a separate console using `CREATE_NEW_CONSOLE`.
Harness attaches/detaches only if required for signal delivery.

## Model C — another standard Windows console model
Only if repository/runtime evidence proves it safer/minimal.

Choose one exact model with the smallest safe surface.

# Phase 3 — Exact native API contract

Define exact native calls/constants/types needed.

Likely candidates:
- `CreateProcessW`
- `GenerateConsoleCtrlEvent`
- `CloseHandle`
- optional:
  - `AttachConsole`
  - `FreeConsole`
  - `SetConsoleCtrlHandler`
  - `WaitForSingleObject`

Authorize only APIs actually required.

No broad Windows interop wrapper library.
No new package.
Use safe handle wrappers where practical with built-in BCL.

# Phase 4 — Process creation semantics

Define exact helper inputs:
- executable path;
- argument list;
- working directory;
- environment variables;
- stdout/stderr strategy;
- creation flags.

Define exact output:
- owned process abstraction;
- PID;
- process-group ID;
- bounded output capture if applicable;
- disposal semantics.

Do not expose raw native handles broadly.

# Phase 5 — Command-line quoting

If `CreateProcessW` requires a constructed command line, define exact Windows quoting/escaping rules.

Prefer:
- existing repository-safe quoting;
- a small tested helper;
- or another standard runtime mechanism.

Do not leave quoting implementation-defined.

Tests must cover paths/arguments with spaces and quotes if quoting is manual.

# Phase 6 — Environment propagation

The helper must support existing harness-owned configuration, including:
- isolated handoff path;
- isolated temporary database;
- existing Worker configuration environment values.

Do not introduce any new production environment variable for liveness or signal semantics.

`--wp08-test-liveness` remains the already-defined test-only argument.

# Phase 7 — Standard streams

Define whether stdout/stderr are redirected/captured or inherited.

Prefer deterministic bounded capture only if acceptance needs process output.

If native redirection is used, define exact handle inheritance and cleanup.

Avoid deadlock-prone unbounded buffering.

# Phase 8 — Signal delivery contract

Define exact helper operation:

`RequestCtrlBreak()`

Semantics:
- sends `CTRL_BREAK_EVENT`;
- target group is the owned Worker's process-group ID;
- never target group 0;
- reports API success/failure;
- no retry unless explicitly justified;
- repeated request is safe from harness perspective.

API success alone does not prove graceful cancellation; Worker exit/read-back does.

# Phase 9 — Test-runner protection

Explicitly define why this cannot signal or terminate the test runner.

Required proof:
- dedicated non-zero process-group ID;
- no broadcast;
- no broad process-name kill;
- no Ctrl+C to the current process group;
- any temporary console attach/detach is strictly scoped and restored.

If the model requires temporarily ignoring console events in the harness, define exact handler scope and disposal.

# Phase 10 — Worker liveness integration

The helper launches the Worker with:
`--wp08-test-liveness`

plus all isolated configuration.

Process-level acceptance flow:
1. launch isolated Worker group;
2. wait for real publication/liveness readiness;
3. verify process alive;
4. call `RequestCtrlBreak`;
5. observe Worker exit within the binding timeout;
6. assert exit code 0;
7. assert no pipeline-failure translation;
8. assert no process residue.

No forced kill in the passing path.

# Phase 11 — Cleanup fallback

If graceful exit fails:
- graceful-cancellation test fails;
- helper may terminate only its owned Worker process/group for cleanup;
- cleanup fallback does not count as successful cancellation;
- wait boundedly;
- dispose all handles.

No global `taskkill` by image name.

# Phase 12 — Child-process ownership

Determine whether Worker launches child processes in this acceptance path.

If no:
- state no child-tree mechanism is needed.

If yes:
- define ownership/cleanup.

Do not add a Job Object unless necessary.

# Phase 13 — Job Object decision

Explicitly decide yes/no.

If yes:
- one job per launched Worker;
- kill-on-job-close only for cleanup ownership;
- never use the Job Object as graceful-cancellation mechanism;
- define exact APIs/constants/disposal.

If no:
- state why owned process cleanup is sufficient.

# Phase 14 — Platform scope

Define exact Windows-only acceptance behavior.

State exact non-Windows behavior:
- platform-conditioned test exclusion only if repository governance permits it; or
- WP08 lifecycle acceptance is explicitly Windows-only.

Do not silently skip required acceptance.

No custom cross-platform IPC fallback.

# Phase 15 — Helper path / visibility

Select one exact test-only helper path using repository conventions.

Preferred conceptual path:
`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Use actual naming if repository conventions differ.

The helper must be:
- internal;
- WP08-owned;
- test-only;
- native interop isolated from production code.

# Phase 16 — WP08 test path authority

Authorize exact:
`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

to use the helper.

If helper path is new, authorize exactly one helper file.

No other test paths.
No WP09 paths.

# Phase 17 — No additional production mutation

This authority authorizes **zero additional production changes** beyond preserving the already-implemented Worker cancellation/liveness surface.

If read-only inspection proves a production adjustment is required, STOP and request a separate narrow amendment.

# Phase 18 — Required future tests

Later Terra implementation must prove:

## Helper
- distinct process group;
- safe launch;
- correct argument/environment behavior;
- `CTRL_BREAK` targeting;
- test runner unaffected;
- deterministic disposal;
- cleanup fallback only touches owned process;
- quoting tests if manual.

## Worker
- real Worker with `--wp08-test-liveness`;
- real publication;
- real CTRL_BREAK;
- exit code 0;
- no forced kill on passing path;
- no residue.

## Restart
- session A canceled gracefully through helper;
- session B restarts under accepted WP08 rules.

# Phase 19 — Manifest/path matrix

Produce an exact table:
- path;
- owner;
- WP08 exception;
- symbols;
- allowed concern;
- forbidden concern.

Expected minimum:
1. `WP08LifecycleDemonstrationTests.cs`
2. one Windows process-group helper file

No wildcard grants.

# Phase 20 — Future validation

Later implementation must run:
- focused WP08 lifecycle tests;
- full finite WP08 demonstration;
- predecessor Python suites;
- Application/Infrastructure/Domain/Architecture;
- build;
- full .NET regression from 309 baseline + authorized WP08 tests.

No new package.

# Non-goals

Do not authorize:
- production IPC;
- socket/named pipe/control file;
- production listener;
- production process supervisor;
- Worker/Streamlit mutual control;
- PowerShell wrapper as canonical signal mechanism;
- broad `taskkill`;
- global console handler changes;
- Replay changes;
- schema/persistence changes;
- package additions;
- WP09.

# Documentation artifact

If governed, create only:
`docs/roadmap/release-1.9/RELEASE_1.9_WP08_WINDOWS_ISOLATED_PROCESS_GROUP_CONSOLE_SIGNAL_TEST_HELPER_AUTHORITY.md`

No production/test/GitHub mutation.

Otherwise return the normative definition in chat.

# Required completion report

## Selected Windows model
Exact console/process-group model and flags.

## Native API surface
Exact calls/constants.

## Helper contract
Inputs/outputs/visibility/disposal.

## Signal contract
Exact CTRL_BREAK delivery and test-runner safety.

## Cleanup fallback
Exact owned-process termination behavior.

## Job Object decision
Yes/no and rationale.

## Platform scope
Exact Windows/non-Windows behavior.

## Path authority
Exact helper/test paths.

## Required future tests
Exact acceptance matrix.

## Mutation statement
If doc created:
`WP08 WINDOWS PROCESS-GROUP/SIGNAL TEST-HELPER DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:
`WP08 WINDOWS PROCESS-GROUP/SIGNAL TEST-HELPER DEFINITION MUTATIONS: ZERO`

## Next step
On success:
`WP08 WINDOWS PROCESS-GROUP/SIGNAL TEST-HELPER AUTHORITY DEFINED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

# Stop conditions

Stop if:
- safe group-targeted signal cannot be delivered without signaling the test runner;
- helper requires custom production IPC;
- production changes beyond the accepted Worker cancellation adapter are required;
- native API surface cannot be made deterministic/safe;
- platform governance forbids Windows-only acceptance.

# Terminal markers

Success:
`RELEASE 1.9 WP08 WINDOWS ISOLATED PROCESS-GROUP LAUNCHER AND CONSOLE-SIGNAL TEST-HELPER AUTHORITY COMPLETE`

Blocked:
`RELEASE 1.9 WP08 WINDOWS ISOLATED PROCESS-GROUP LAUNCHER AND CONSOLE-SIGNAL TEST-HELPER AUTHORITY BLOCKED`

Do not emit COMPLETE unless one exact Windows-owned process-group launch/signal/cleanup model is fully defined and cannot signal unrelated processes.

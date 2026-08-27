# Release 1.9 — WP08 Windows Diagnostic-Capture Mechanics Implementation-Only Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **very narrow implementation-only diagnostic authority** for Release 1.9 WP08, canonical issue **#233**.

This authority removes design discretion.

The executor **must implement** the already-fixed Windows native diagnostic-capture mechanics and the A/B/C/D diagnostic matrix described below.

The executor may stop only if:
- a specific Windows API call cannot be used safely in the governed test environment;
- a required safety invariant cannot be satisfied;
- compilation/runtime proves one of the fixed mechanics impossible;
- the exact two authorized paths are insufficient to implement the mechanics.

The executor must **not** stop merely because the current helper lacks the mechanics. Adding them is the authorized work.

This authority remains **diagnostic-only**.

No behavioral fix is authorized.
No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package addition.
No GitHub lifecycle mutation.
No WP09.

---

# Accepted predecessor state

Preserve all current valid WP08 work.

Production, read-only:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Tests/helpers, authorized for diagnostic instrumentation:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Python, read-only:
- `python/presentation/wp08_presentation_chain_probe.py`

Accepted evidence:
- standalone targeted CTRL_BREAK exits Worker `0`;
- combined Streamlit + Worker + governed probe reproducibly exits Worker `0xC0000142`;
- Streamlit readiness succeeds;
- P2 is real and within the fixed 8-second bound;
- governed probe succeeds;
- standalone cancellation still passes after combined failure;
- root cause remains `Class U`.

Regression reference:
- focused WP08 predecessor: 4/4;
- .NET predecessor: 313/313;
- Python WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check` clean.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Exact authorized mutation paths

Only:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

No other repository path may be modified.

If implementation truly requires another path, STOP and name the exact missing path authority.

---

# Hard implementation mandate

The executor must implement all of the following unless a concrete API/safety failure proves one item impossible.

## In `WindowsIsolatedProcessGroup.cs`

Implement:

1. two anonymous pipe pairs:
   - stdout parent-read / child-write;
   - stderr parent-read / child-write.

2. parent read handles made non-inheritable using:
   - `SetHandleInformation`.

3. child write handles remain inheritable.

4. `STARTF_USESTDHANDLES`.

5. child:
   - `hStdOutput = stdout child-write`;
   - `hStdError = stderr child-write`.

6. exact stdin handling selected from current safe repository behavior:
   - preserve current stdin if safely inheritable; otherwise use the exact safe null/invalid handle behavior supported by current helper/runtime.
   - do not leave `hStdInput` undefined.

7. `CreateProcessW(..., bInheritHandles: TRUE, ...)`.

8. preserve:
   - `CREATE_NEW_PROCESS_GROUP`;
   - existing Worker command line;
   - existing environment;
   - existing working directory;
   - existing runner-safety semantics.

9. immediately after successful launch, parent closes:
   - stdout child-write copy;
   - stderr child-write copy;
   - primary thread handle if not already closed by current helper.

10. parent retains:
   - process handle;
   - stdout parent-read;
   - stderr parent-read.

11. bounded concurrent drain of stdout and stderr.

12. retained diagnostic cap:
   - **64 KiB per stream**.

13. after 64 KiB:
   - continue draining;
   - discard excess;
   - set truncation flag.

14. drain both streams concurrently from process launch onward.

15. on process exit:
   - wait up to **1 second** for stream drains to complete;
   - if not complete, set drain-timeout flag;
   - dispose read handles.

16. expose factual bounded diagnostics:
   - `CapturedStdout`;
   - `CapturedStderr`;
   - stdout/stderr truncated;
   - stdout/stderr drain timeout.

17. expose factual launch/signal diagnostics:
   - Worker PID;
   - process-group ID;
   - creation flags;
   - `bInheritHandles`;
   - stdout redirected;
   - stderr redirected;
   - signal event type;
   - signal target group ID;
   - `GenerateConsoleCtrlEvent` result;
   - Win32 error if false;
   - process exit code when available.

18. preserve current targeted CTRL_BREAK behavior unchanged.

19. preserve existing cleanup fallback unchanged except for handle disposal needed by the new pipes.

20. no persistent files/logs.

## In `WP08LifecycleDemonstrationTests.cs`

Implement the A/B/C/D diagnostic matrix exactly:

A. Worker only.
B. Worker + Streamlit.
C. Worker + governed Python probe.
D. Worker + Streamlit + governed Python probe.

All four must:
- use the same Worker helper;
- use `--wp08-test-liveness`;
- reach P2 hold;
- send the same targeted CTRL_BREAK;
- record diagnostics;
- clean owned processes/resources.

---

# Phase 0 — Read-only preflight

Read both authorized files completely.

Map current symbols to the fixed mechanics.

Do not redesign.

If the current P/Invoke signatures require extension, extend them only inside the existing helper file.

---

# Phase 1 — Native declarations

Add only the declarations needed for:

- `CreatePipe`;
- `SetHandleInformation`;
- existing `CreateProcessW`;
- existing `GenerateConsoleCtrlEvent`;
- existing `CloseHandle` or safe-handle closure;
- `Marshal.GetLastWin32Error()`.

Constants authorized:
- `STARTF_USESTDHANDLES`;
- `HANDLE_FLAG_INHERIT`;
- existing `CREATE_NEW_PROCESS_GROUP`;
- existing `CTRL_BREAK_EVENT`.

Do not add:
- `AttachConsole`;
- `FreeConsole`;
- `SetConsoleCtrlHandler`;
- Job Object APIs;
- Toolhelp APIs;
- ETW/debugger APIs.

---

# Phase 2 — Pipe creation

Implement a helper-local routine that:

- creates pipe pair with inheritable handles;
- calls `SetHandleInformation` on parent read handle to clear inheritance;
- throws with factual Win32 error on failure;
- disposes partial handles on failure.

No placeholder metadata.

No guessed handle state.

---

# Phase 3 — STARTUPINFO wiring

Set:

- `dwFlags |= STARTF_USESTDHANDLES`;
- `hStdOutput = stdout child-write`;
- `hStdError = stderr child-write`;
- exact safe `hStdInput`.

Use the actual `STARTUPINFO` struct currently in the helper.

Do not create an alternate launch path.

---

# Phase 4 — CreateProcessW launch

Launch with:

- `bInheritHandles = TRUE`;
- creation flags preserving `CREATE_NEW_PROCESS_GROUP`;
- all current command/environment/working-directory behavior unchanged.

If `CreateProcessW` fails:
- capture Win32 error;
- dispose all pipe/process/thread handles;
- fail test/helper construction.

No silent fallback.

---

# Phase 5 — Post-launch handle closure

Immediately after success:

- close parent copies of child stdout/stderr write handles;
- close primary thread handle if current helper ownership says parent no longer needs it;
- retain process + parent read handles.

This exact close order is required.

---

# Phase 6 — Concurrent drains

Create two asynchronous drain operations.

Requirements:

- one stdout;
- one stderr;
- begin immediately after launch;
- run concurrently;
- read until EOF;
- retain only first 64 KiB;
- discard remainder;
- set truncation flag;
- no unbounded buffer.

Use BCL streams over safe handles if available.

Do not block the process by waiting synchronously for one stream before the other.

---

# Phase 7 — Drain completion

When Worker exits:

- await both drain tasks for up to 1 second;
- if timeout:
  - set factual timeout flag;
  - dispose read handles;
- do not hang test.

No thread/task leak.

---

# Phase 8 — Signal diagnostics

Inside existing CTRL_BREAK request method:

Before API:
- store target process-group ID;
- store event = CTRL_BREAK_EVENT.

After API:
- store boolean result;
- if false, store `Marshal.GetLastWin32Error()`.

Do not retry.
Do not change signal target.
Do not change group semantics.

---

# Phase 9 — Monotonic scenario timing

In `WP08LifecycleDemonstrationTests.cs`, use one `Stopwatch` per scenario.

Record elapsed values for:
- Worker launch;
- P1;
- P2;
- Streamlit launch/ready;
- probe launch/exit;
- CTRL_BREAK request;
- signal API return;
- Worker exit;
- cleanup complete.

No wall-clock causality.

---

# Phase 10 — Scenario A

Implement:

**A — Worker only**

- launch Worker with same helper;
- no Streamlit;
- no probe;
- wait P2 hold;
- send CTRL_BREAK;
- record:
  - exit code;
  - helper diagnostics;
  - timing;
- cleanup.

Do not alter expected behavior to force pass/fail.

---

# Phase 11 — Scenario B

Implement:

**B — Worker + Streamlit**

- launch Streamlit using existing governed combined-harness logic;
- prove readiness;
- no probe;
- launch Worker;
- wait P2;
- send CTRL_BREAK;
- capture diagnostics;
- cleanup Worker + Streamlit.

Do not change Streamlit launch semantics.

---

# Phase 12 — Scenario C

Implement:

**C — Worker + governed probe**

- no Streamlit;
- launch Worker;
- wait P2;
- invoke existing governed Python probe against P2 handoff;
- require probe completion before signal;
- send CTRL_BREAK;
- capture diagnostics;
- cleanup.

Do not change probe semantics.

---

# Phase 13 — Scenario D

Implement:

**D — Worker + Streamlit + governed probe**

- launch Streamlit;
- prove readiness;
- launch Worker;
- wait P2;
- invoke probe;
- require probe exit;
- send CTRL_BREAK;
- capture diagnostics;
- cleanup.

This must reproduce the known failing topology faithfully.

---

# Phase 14 — Scenario diagnostic record

For each A/B/C/D, record factual:

- scenario name;
- Worker PID;
- Worker group ID;
- Streamlit PID/state if present;
- probe PID/state if present;
- listener owner PID if available from existing test logic;
- creation flags;
- `bInheritHandles`;
- stdout/stderr redirection;
- signal target/result/error;
- Worker exit code;
- stdout truncation;
- stderr truncation;
- drain-timeout flags;
- bounded stdout;
- bounded stderr;
- timing deltas.

Do not fabricate console-association metadata.

If exact console association is not observable under these APIs, state:
`console association: not instrumented`.

---

# Phase 15 — Test output only

Emit bounded scenario diagnostics to test output.

No persistent log file.
No evidence directory.
No temp diagnostic file.

---

# Phase 16 — Root-cause classification

After A/B/C/D:

Classify exactly one:

- **H** — harness sequencing/cleanup;
- **P** — process-group/console helper;
- **S** — stdio/inherited-handle;
- **T** — timing/race;
- **W** — Worker cancellation/exit path;
- **U** — unresolved.

Use factual differential evidence only.

Do not fix.

---

# Phase 17 — One-factor E scenario

Only if A/B/C/D identifies a single remaining stdio/handle ambiguity:

- add one E scenario;
- vary one factor only;
- remain diagnostic-only.

If A/B/C/D is sufficient, no E.

---

# Phase 18 — No-fix gate

Forbidden under this authority:

- changing Worker code;
- changing CTRL_BREAK flags/target;
- changing `CREATE_NEW_PROCESS_GROUP`;
- changing Streamlit/probe semantics;
- changing test sequencing to make combined path pass;
- adding arbitrary sleeps;
- changing Replay;
- changing WP05/WP06/WP07;
- package additions.

Even if the root cause is obvious, stop at classification.

---

# Phase 19 — Validation

Run:

1. helper/test project compile;
2. scenario A/B/C/D matrix;
3. original standalone CTRL_BREAK focused test;
4. focused WP08 tests affected by helper/test instrumentation;
5. build.

Because helper launch mechanics are materially changed for diagnostic capture, also run:

6. Infrastructure suite;
7. full .NET regression from the accepted 313/313 baseline plus only authorized diagnostic test delta.

If C/D invoke probe, verify existing Python predecessor smoke as needed:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- no Python mutation.

---

# Phase 20 — Residue

After every scenario:

- Worker absent;
- Streamlit absent where launched;
- probe absent where launched;
- listener residue zero;
- drain tasks completed/timed out factually;
- pipe handles disposed;
- process/thread handles disposed.

Forced cleanup permitted only as diagnostic cleanup fallback and must be reported.

No global kill by process name.

---

# Phase 21 — Scope audit

Changed paths must be exactly:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else unchanged.

Prove zero:
- production mutation;
- Python mutation;
- Replay mutation;
- WP05/WP06/WP07 mutation;
- package mutation;
- GitHub mutation;
- WP09.

---

# Lifecycle boundary

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open.

GitHub mutations:
`ZERO`

---

# Required completion report

## Mechanics implemented
- CreatePipe;
- SetHandleInformation;
- STARTF_USESTDHANDLES;
- bInheritHandles=TRUE;
- handle-close order;
- bounded drains;
- signal diagnostics.

## A/B/C/D matrix
For each:
- topology;
- exit code;
- signal result;
- stdio diagnostics;
- timing;
- stdout/stderr.

## Differential
Exact factual difference correlated with failure.

## Root-cause classification
H/P/S/T/W/U.

## Validation
Standalone/focused/Infrastructure/full counts.

## Residue
No owned process/listener/task/pipe/handle residue.

## Scope
Only two files changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 WINDOWS DIAGNOSTIC-CAPTURE MECHANICS IMPLEMENTATION GITHUB MUTATIONS: ZERO`

## Next step

If classified H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If still U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

The executor may stop only if a concrete technical/safety blocker is proven, such as:

- `CreatePipe`/`SetHandleInformation`/`CreateProcessW` cannot be used safely in the current test runtime;
- bounded concurrent drain cannot be implemented without deadlock/leak;
- runner safety would be weakened;
- exact two-path scope is insufficient;
- matrix execution requires production semantic changes.

The executor must **not** stop because the mechanics are absent before implementation.

---

# Terminal markers

Successful classification:

`RELEASE 1.9 WP08 WINDOWS DIAGNOSTIC-CAPTURE MECHANICS IMPLEMENTATION COMPLETE`

Still unresolved or technically unsafe:

`RELEASE 1.9 WP08 WINDOWS DIAGNOSTIC-CAPTURE MECHANICS IMPLEMENTATION BLOCKED`

Do not emit COMPLETE unless the native capture mechanics are actually implemented, the A/B/C/D matrix actually executes with factual diagnostics, and the root cause is classified out of Class U.

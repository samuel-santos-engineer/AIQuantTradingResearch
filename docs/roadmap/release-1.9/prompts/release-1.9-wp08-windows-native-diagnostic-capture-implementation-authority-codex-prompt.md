# Release 1.9 — WP08 Windows Native Diagnostic-Capture Implementation Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **very narrow diagnostic-instrumentation implementation authority** for Release 1.9 WP08, canonical issue **#233**.

Its sole purpose is to implement factual Windows-native diagnostic capture sufficient to execute and interpret the previously required A/B/C/D combined-lifecycle topology matrix.

This authority is **diagnostic-only**.

It does **not** authorize a behavioral fix.

No production semantic change.
No Worker cancellation change.
No signal-mechanism redesign.
No Replay change.
No WP05/WP06/WP07 change.
No Python probe semantic change.
No package addition.
No GitHub lifecycle mutation.
No WP09.

---

# Accepted predecessor state

Preserve all current valid WP08 work:

Production:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Tests/helpers:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Python:
- `python/presentation/wp08_presentation_chain_probe.py`

Accepted evidence:
- standalone Worker + targeted CTRL_BREAK exits `0`;
- combined Streamlit + Worker + probe reproduces Worker exit `0xC0000142`;
- Streamlit readiness passes;
- real P2 passes;
- governed probe passes;
- focused predecessor cancellation test still passes after combined failure;
- P2 remains within fixed 8-second bound.

Accepted regression reference:
- focused WP08 predecessor: 4/4;
- .NET predecessor: 313/313;
- Python WP05 3/3, WP06 6/6, WP07 semantic 2/2, WP07 presentation 2/2;
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check` clean.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Proven diagnostic gap

The current helper does not factually expose enough data to interpret the required topology matrix.

Missing evidence includes:

- bounded Worker stdout;
- bounded Worker stderr;
- child stdio inheritance state;
- helper-owned pipe state;
- exact process creation flags;
- process-group identity;
- exact CTRL_BREAK target/result;
- signal-time process topology;
- monotonic timing;
- A/B/C/D scenario structure.

A prior attempt correctly removed placeholder/non-factual metadata.

This authority explicitly permits implementing the real native capture mechanics required to obtain those facts.

---

# Objective

Implement factual bounded diagnostics in the existing Windows helper and lifecycle test so that the matrix can be executed:

A. Worker only.
B. Worker + Streamlit.
C. Worker + governed Python probe.
D. Worker + Streamlit + governed Python probe.

At the end of this authority:

- the matrix must be runnable;
- diagnostics must be factual;
- root cause must be classified H/P/S/T/W or remain U based on evidence;
- no fix may be applied.

---

# Binding prior diagnostic contract

Read the prior bounded process-topology diagnostic instrumentation authority and preserve:

- root-cause classes H/P/S/T/W/U;
- A/B/C/D definitions;
- no-fix requirement;
- no GitHub mutation;
- no production mutation.

This authority narrows and concretizes only the implementation mechanics for factual Windows diagnostics.

---

# Authorized mutation paths

Only:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else is read-only.

No new helper file.
No new test file.
No production mutation.
No Python mutation.

---

# Phase 0 — Read-only helper inspection

Inspect current `WindowsIsolatedProcessGroup.cs` completely.

Record:

- current `CreateProcessW` signature;
- current `STARTUPINFO`/`PROCESS_INFORMATION`;
- current creation flags;
- current `bInheritHandles`;
- current stdout/stderr behavior;
- current handle wrappers;
- current signal method;
- current disposal;
- current cleanup fallback.

Do not mutate until the exact native delta is mapped.

---

# Phase 1 — Native API surface

This authority permits only the minimum Windows APIs required for factual diagnostic capture.

Expected allowed APIs:

- `CreatePipe`
- `SetHandleInformation`
- `CreateProcessW`
- `GenerateConsoleCtrlEvent`
- `GetLastError` via `Marshal.GetLastWin32Error()`
- `CloseHandle` through safe-handle disposal or existing wrapper
- optionally `PeekNamedPipe` only if strictly necessary for nonblocking drain
- optionally `WaitForSingleObject` only if current helper already needs it

Do not add:
- `AttachConsole`
- `FreeConsole`
- `SetConsoleCtrlHandler`
- Job Objects
- Toolhelp process enumeration
- ETW
- debugger APIs
unless a later authority explicitly permits them.

Prefer no extra API beyond pipes + current launch/signal surface.

---

# Phase 2 — Pipe creation contract

Implement two anonymous pipe pairs:

## stdout
- child write handle
- parent read handle

## stderr
- child write handle
- parent read handle

Creation rules:

- child write handles are inheritable;
- parent read handles must be made **non-inheritable** using `SetHandleInformation`;
- no child read handle exists;
- no parent write handle retained after successful launch.

Use exact Windows handle inheritance flags.

If `CreatePipe` fails:
- throw/fail helper construction with factual Win32 error.

No silent fallback to inherited console streams in diagnostic mode.

---

# Phase 3 — STARTUPINFO stdio wiring

For diagnostic launch mode only:

- set `STARTF_USESTDHANDLES`;
- set `hStdOutput` = child stdout write handle;
- set `hStdError` = child stderr write handle;
- define `hStdInput` exactly:
  - either inherited current stdin if current helper already governs it safely; or
  - `INVALID_HANDLE_VALUE`/null-equivalent only if Windows semantics and repository behavior are explicitly safe.

Do not leave stdin undefined.

Select one exact behavior based on current helper/repository convention.

---

# Phase 4 — CreateProcessW inheritance contract

For diagnostic launch mode:

- `bInheritHandles = TRUE`;
- only intended inheritable child stdio handles may be inheritable from helper-controlled resources;
- parent read handles must be non-inheritable;
- creation flags must still include the accepted:
  `CREATE_NEW_PROCESS_GROUP`.

Preserve all existing Worker launch semantics.

Do not add `CREATE_NEW_CONSOLE`.
Do not change process group behavior.

---

# Phase 5 — Handle-close ordering after launch

Immediately after successful `CreateProcessW`:

Parent must close/dispose its copies of:

- child stdout write handle;
- child stderr write handle;
- primary thread handle if current helper already does so.

Parent retains:

- process handle;
- stdout read handle;
- stderr read handle.

This ordering is mandatory so EOF can be observed after child exit.

On launch failure:
- dispose all created pipe handles;
- surface Win32 error.

No leaked handles.

---

# Phase 6 — Safe handle model

Use existing repository safe-handle conventions if present.

If none:

- define the narrowest internal safe-handle wrapper in the existing `WindowsIsolatedProcessGroup.cs`;
- wrapper may only own/close native handles used by this helper;
- no general native abstraction.

Do not expose raw `IntPtr` handles outside the helper except immutable factual numeric diagnostics if truly needed.

---

# Phase 7 — Bounded stdout/stderr capture

Implement asynchronous draining of both parent read handles.

Requirements:

- both streams drained concurrently;
- decoding = UTF-8 unless actual Worker console encoding proves another governed encoding;
- retained text cap = **64 KiB per stream** unless repository convention requires a smaller fixed cap;
- after cap is reached, continue draining and discard additional bytes so child cannot block;
- no unbounded `StringBuilder`;
- no synchronous read pattern that can deadlock process exit;
- capture starts immediately after launch.

Expose:

- bounded `CapturedStdout`;
- bounded `CapturedStderr`;
- truncation flags for each stream.

Do not write captured output to files.

---

# Phase 8 — Capture task lifecycle

The helper must own stdout/stderr drain tasks.

On child exit/disposal:

- wait boundedly for pipe EOF/drain completion;
- do not block indefinitely;
- if drain completion exceeds a small fixed diagnostic timeout, record a factual drain-timeout flag and dispose owned read handles.

Choose exact drain timeout, preferably 1 second unless repository timing conventions require another bounded value.

No thread leak.

---

# Phase 9 — Diagnostic metadata contract

Add a narrow immutable diagnostic snapshot exposed by the helper.

It may contain only factual fields such as:

- Worker PID;
- process-group ID;
- creation flags;
- `bInheritHandles`;
- stdout redirected = true/false;
- stderr redirected = true/false;
- stdout parent read handle open = true/false;
- stderr parent read handle open = true/false;
- stdout child write handle closed in parent = true/false;
- stderr child write handle closed in parent = true/false;
- signal event type;
- signal target process-group ID;
- `GenerateConsoleCtrlEvent` boolean result;
- signal Win32 error when false;
- process exit code when available;
- stdout truncation flag;
- stderr truncation flag.

Do not invent “console association” fields unless directly observable from an authorized API already in use.

If console association cannot be factually observed under this authority, report it as **not instrumented**, not guessed.

---

# Phase 10 — Signal-time snapshot

Immediately before `GenerateConsoleCtrlEvent`, capture factual harness state in the test:

- Worker PID;
- process-group ID;
- Worker alive;
- Streamlit PID/alive if present;
- probe PID/alive or exited if present;
- loopback listener owner PID if present;
- monotonic timestamp.

Immediately after signal API returns capture:

- return value;
- Win32 last error if false;
- monotonic timestamp.

Do not modify signal behavior.

---

# Phase 11 — Monotonic clock

Use exactly one monotonic source in the test, preferably `Stopwatch.GetTimestamp()` / elapsed `Stopwatch`.

Record scenario-relative times for:

- Worker launch;
- P1 observed;
- P2 observed;
- Streamlit launch;
- Streamlit ready;
- probe launch;
- probe exit;
- CTRL_BREAK request;
- CTRL_BREAK API return;
- Worker exit;
- cleanup begin/end.

Do not use wall clock for causality conclusions.

---

# Phase 12 — Streamlit/probe process metadata

In `WP08LifecycleDemonstrationTests.cs`, record factual launch metadata for Streamlit and probe:

- PID;
- executable path;
- working directory;
- argument list or normalized summary;
- shell execute true/false;
- stdout redirected true/false;
- stderr redirected true/false;
- environment override keys only;
- exit code if exited.

Do not log environment values that may contain sensitive data.

No helper refactor required unless current test already centralizes launch.

---

# Phase 13 — A scenario

Implement diagnostic scenario A:

**Worker only**

Requirements:
- same Worker launch helper;
- `--wp08-test-liveness`;
- reach P2 hold;
- no Streamlit;
- no probe;
- send targeted CTRL_BREAK;
- capture helper diagnostics;
- record Worker exit code;
- cleanup.

Expected predecessor result may remain 0, but do not hardcode classification from expectation.

---

# Phase 14 — B scenario

Implement diagnostic scenario B:

**Worker + Streamlit**

Requirements:
- launch Streamlit using existing combined-harness logic;
- prove readiness;
- no probe;
- reach Worker P2 hold;
- capture listener owner;
- send targeted CTRL_BREAK;
- record Worker exit + bounded diagnostics;
- cleanup both.

Do not change Streamlit launch semantics for success.

---

# Phase 15 — C scenario

Implement diagnostic scenario C:

**Worker + governed probe**

Requirements:
- no Streamlit;
- reach Worker P2 hold;
- invoke existing governed probe against real P2 handoff;
- require probe process completion before CTRL_BREAK;
- record probe metadata;
- send targeted CTRL_BREAK;
- capture Worker exit/diagnostics;
- cleanup.

Do not alter probe semantics.

---

# Phase 16 — D scenario

Implement diagnostic scenario D:

**Worker + Streamlit + governed probe**

Requirements:
- same combined topology that reproduces `0xC0000142`;
- Streamlit ready;
- Worker P2 hold;
- governed probe completes;
- signal Worker;
- capture all diagnostics;
- cleanup.

This must reproduce the real failing path as faithfully as possible.

---

# Phase 17 — One-factor E scenario rule

Do not add E initially.

After A/B/C/D:

- if exactly one stdio/handle difference is suspected but not proven;
- E may change **one** launch property only.

Examples:
- Worker diagnostic stdio capture enabled/disabled;
- Streamlit redirection mode unchanged/altered only if existing code already supports both modes.

E remains diagnostic-only.

If A/B/C/D classify the issue, do not add E.

---

# Phase 18 — Structured test output

Emit one bounded diagnostic summary per scenario to test output.

Format should include:

- scenario name;
- Worker PID/group;
- Streamlit PID/state;
- probe PID/state;
- listener PID/state;
- creation flags;
- inherit-handles;
- stdio redirection;
- signal target/result/error;
- timing deltas;
- Worker exit code;
- stdout truncation + bounded text;
- stderr truncation + bounded text.

No persistent log file.

No raw secrets.

---

# Phase 19 — Root-cause classification

After running matrix, classify exactly one:

## H — Harness sequencing/cleanup
Failure changes with ordering/cleanup while topology/stdio remain equivalent.

## P — Process-group/console helper
Failure correlates with process-group/signal topology or helper behavior.

## S — Stdio/inherited-handle
Failure correlates with redirected pipes/inheritance/handle lifecycle.

## T — Timing/race
Same topology and handles; outcome correlates reproducibly with timing/order.

## W — Worker cancellation/exit path
Worker diagnostics show cancellation reaches production path but exit becomes invalid independent of harness topology.

## U — Unresolved
Evidence still insufficient.

Do not fix.

---

# Phase 20 — No-fix hard gate

Even if root cause is obvious:

- do not change signal flags;
- do not change cancellation adapter;
- do not change test sequencing to make it pass;
- do not change Replay;
- do not change Streamlit/probe behavior;
- do not add sleeps;
- do not add custom IPC.

This authority ends at factual classification.

---

# Phase 21 — Validation

Required:

- helper compiles;
- A/B/C/D matrix runs;
- standalone CTRL_BREAK predecessor test still runs;
- focused WP08 tests relevant to changed helper/test paths run;
- build passes.

If helper instrumentation materially changes Infrastructure test behavior, also run:
- Infrastructure suite;
- full .NET regression from 313/313 baseline plus authorized diagnostic tests.

Do not require Python regression unless Python behavior is exercised by the matrix; if D/C invoke the governed probe, verify existing Python predecessor smoke as needed without modifying Python.

---

# Phase 22 — Residue cleanup

After each scenario:

- Worker absent;
- Streamlit absent where launched;
- probe absent where launched;
- no owned listener residue;
- no pipe/drain task residue;
- helper native handles disposed.

Forced cleanup may be used after a failing diagnostic scenario, but record it as cleanup only.

No global kill by image name.

---

# Phase 23 — Scope audit

Changed paths must be exactly:

- `WindowsIsolatedProcessGroup.cs`
- `WP08LifecycleDemonstrationTests.cs`

Everything else unchanged.

Prove zero:
- production mutation;
- Python mutation;
- Replay mutation;
- WP05/WP06/WP07 mutation;
- package change;
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

## Native capture implementation
Exact `CreatePipe` / `SetHandleInformation` / `STARTF_USESTDHANDLES` / handle lifecycle.

## Bounded capture
Cap, truncation, drain behavior, timeout.

## Scenario matrix
A/B/C/D:
- topology;
- signal result;
- exit code;
- stdout/stderr;
- timings.

## Differential
Exact factual difference associated with failure.

## Root-cause classification
H/P/S/T/W/U.

## Confidence
Why evidence supports classification.

## Validation
Build/focused/Infrastructure/full results actually run.

## Residue
No owned process/listener/pipe/handle residue.

## Scope
Only two files changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 WINDOWS NATIVE DIAGNOSTIC-CAPTURE IMPLEMENTATION GITHUB MUTATIONS: ZERO`

## Next step

If H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

Stop if:

- factual capture requires production mutation;
- factual capture requires `AttachConsole`/console-handler manipulation;
- pipe capture cannot be made bounded/deadlock-safe;
- helper runner safety would be weakened;
- diagnostics require package addition;
- A/B/C/D cannot be executed without changing production semantics.

Do not apply a fix.

---

# Terminal markers

Successful factual classification:

`RELEASE 1.9 WP08 WINDOWS NATIVE DIAGNOSTIC-CAPTURE IMPLEMENTATION COMPLETE`

Still unresolved/unsafe:

`RELEASE 1.9 WP08 WINDOWS NATIVE DIAGNOSTIC-CAPTURE IMPLEMENTATION BLOCKED`

Do not emit COMPLETE unless the native bounded capture is implemented, A/B/C/D execute with factual diagnostics, and the root cause is classified out of Class U.

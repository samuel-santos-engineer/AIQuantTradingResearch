# Release 1.9 — WP08 Synchronous Anonymous-Pipe Drain Correction Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow diagnostic-capture correction authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to correct the proven runtime-safety defect in the native diagnostic pipe-capture patch:

> `CreatePipe` produced synchronous anonymous-pipe read handles, but the helper wrapped them in `FileStream(..., isAsync: true)`, causing:
>
> `System.ArgumentException: Handle does not support asynchronous operations.`

This authority may correct only the anonymous-pipe drain implementation and directly related compile warnings/diagnostic plumbing necessary to restore the prescribed diagnostic matrix.

This authority remains **diagnostic-only**.

No behavioral fix to the combined CTRL_BREAK failure is authorized.

No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package addition.
No GitHub mutation.
No WP09.

---

# Accepted partial diagnostic patch

Preserve the already-applied native mechanics in:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted implemented pieces include:

- `CreatePipe`;
- `SetHandleInformation`;
- `STARTF_USESTDHANDLES`;
- `bInheritHandles = TRUE`;
- post-launch child-write-handle disposal;
- bounded capture data structures;
- concurrent-drain intent;
- signal result/error metadata;
- preserved `CREATE_NEW_PROCESS_GROUP`;
- preserved targeted CTRL_BREAK.

The failed piece is specifically:
- wrapping synchronous anonymous-pipe parent read handles with an asynchronous `FileStream`.

Do not discard the valid pipe/handle patch.

---

# Accepted predecessor evidence

Preserve:

- standalone pre-diagnostic Worker CTRL_BREAK path historically exits `0`;
- combined Streamlit + Worker + probe historically reproduces `0xC0000142`;
- P2 within fixed 8-second bound;
- governed probe succeeds;
- root cause remains `Class U`;
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

Build after the attempted patch:
- 0 errors;
- 2 warnings:
  1. nullable `signal` initialization;
  2. CA1838 on mutable `StringBuilder` P/Invoke usage.

Runtime:
- helper fails before matrix due to async `FileStream` construction on synchronous pipe handle.

---

# Exact mutation scope

Modify only:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs` only if required to resume the matrix after helper correction.

Do not modify any other path.

Primary expected mutation is helper-only.

---

# Objective

Correct the helper so that:

1. synchronous `CreatePipe` parent-read handles are consumed safely;
2. stdout and stderr are still drained concurrently;
3. each stream retains at most 64 KiB;
4. excess bytes continue to be drained/discarded;
5. child process cannot block on full pipes;
6. post-exit drain completion remains bounded to 1 second;
7. no task/thread/handle leak occurs;
8. standalone CTRL_BREAK path runs again;
9. then A/B/C/D diagnostic matrix may resume;
10. no behavioral fix is applied.

---

# Phase 0 — Read-only verification

Inspect the current applied helper patch.

Identify exactly:
- where `FileStream(..., isAsync: true)` is constructed;
- current drain task fields;
- current handle ownership;
- current bounded-capture method;
- current cleanup/disposal path;
- current warning locations.

No redesign.

---

# Phase 1 — Synchronous FileStream correction

For each parent-read pipe handle, wrap with a **synchronous** stream:

```csharp
var stdoutStream = new FileStream(
    stdoutReadHandle,
    FileAccess.Read,
    bufferSize: 4096,
    isAsync: false);

var stderrStream = new FileStream(
    stderrReadHandle,
    FileAccess.Read,
    bufferSize: 4096,
    isAsync: false);
```

or the exact equivalent supported by the current target framework.

Do not use `isAsync: true`.

Do not replace `CreatePipe`.

Do not introduce named pipes or overlapped handles.

---

# Phase 2 — Concurrent drain model

Drain stdout and stderr concurrently using two dedicated thread-pool tasks.

Preferred exact pattern:

```csharp
_stdoutDrainTask = Task.Run(
    () => DrainBoundedSynchronously(stdoutStream, DiagnosticCaptureLimitBytes));

_stderrDrainTask = Task.Run(
    () => DrainBoundedSynchronously(stderrStream, DiagnosticCaptureLimitBytes));
```

Each task owns one stream only.

Do not read both streams sequentially.

Do not use `ReadAsync` on these synchronous handles.

---

# Phase 3 — Exact bounded synchronous drain routine

Implement or correct a routine equivalent to:

```csharp
private static BoundedCapturedStream DrainBoundedSynchronously(
    Stream stream,
    int limitBytes)
```

Required behavior:

1. allocate a fixed read buffer, e.g. 4096 bytes;
2. maintain a bounded retained byte buffer up to exactly 64 KiB;
3. blocking-read until EOF;
4. for each read:
   - retain only bytes that still fit under the cap;
   - if more bytes arrive after the cap, set `Truncated = true`;
   - continue reading/discarding all excess bytes;
5. after EOF:
   - decode retained bytes as UTF-8;
   - return text + truncation state;
6. no unbounded `StringBuilder`;
7. no cancellation token that can interrupt drain before EOF during the normal passing path.

The drain is intentionally blocking on its own Task.Run worker.

---

# Phase 4 — One-second post-exit completion bound

After Worker exit:

- wait for both drain tasks to complete;
- each stream gets at most **1 second** of additional wait after process exit.

If a drain task does not finish within the bound:

1. mark `DrainTimedOut = true`;
2. dispose/close its read stream/handle to unblock the blocking read;
3. wait only a small bounded cleanup interval if needed;
4. do not hang the test.

Do not leave blocked thread-pool work indefinitely.

If disposal does not unblock safely in the current runtime, STOP with concrete runtime evidence.

---

# Phase 5 — Ownership and disposal

Ensure each native read handle is owned exactly once.

Preferred:
- `FileStream` owns the SafeFileHandle;
- helper does not separately dispose the same handle after ownership transfer.

Ensure:
- child-write copies are already disposed immediately after launch;
- stream disposal occurs after drain completion or timeout cleanup;
- process/thread handles follow existing ownership;
- no double-close.

Add assertions or careful structure if needed, but no new abstraction path.

---

# Phase 6 — Capture result shape

Preserve current bounded capture records.

If the prior type assumed async drain exceptions, adapt minimally.

Result must still expose:

- captured text;
- truncated;
- drain timed out;
- optional factual drain exception only if already authorized and bounded.

Do not add speculative metadata.

---

# Phase 7 — Nullable signal warning

Fix the nullable `signal` initialization warning factually.

Preferred options:

- initialize signal diagnostic state to `null` where the type is nullable;
- use an explicit immutable “not yet signaled” state if the current record already supports it.

Do not invent a fake signal value.

Do not use null-forgiving merely to hide an actual uninitialized state unless the control flow proves it.

Build must return to 0 warnings.

---

# Phase 8 — CA1838 P/Invoke warning

The existing `CreateProcessW` call requires a mutable command-line buffer.

Resolve CA1838 narrowly.

Allowed approaches, in preference order:

1. use the repository/runtime-supported source-generated or span-based P/Invoke form **only if it can preserve `CreateProcessW` mutable-command-line semantics without broad refactor**;
2. if not practical, apply a narrow, local suppression with an explanatory comment around the specific P/Invoke declaration/call because Windows `CreateProcessW` requires writable command-line memory;
3. do not globally suppress CA1838.

The final build must be 0 warnings.

Do not redesign the process launcher solely for analyzer preference.

---

# Phase 9 — Standalone predecessor gate first

Before adding/resuming A/B/C/D matrix execution, run the original standalone CTRL_BREAK test using the corrected capture helper.

Required:

- helper launches;
- no `ArgumentException`;
- P2 reached;
- targeted CTRL_BREAK sent;
- Worker exit observed;
- captured stdout/stderr drain completes or bounded timeout is factually reported;
- no pipe/task/handle residue.

The standalone path should still exit `0`.

If standalone exit changes due solely to diagnostic capture, STOP and report exact evidence.

Do not continue to matrix until standalone is safe.

---

# Phase 10 — Capture self-test

Verify the capture mechanics themselves.

At minimum prove:

- stdout drain task starts;
- stderr drain task starts;
- both complete after child exit or hit bounded timeout;
- 64-KiB cap logic is exercised by unit-level helper logic if an existing test seam in the same file can do so without new path;
- no deadlock.

Do not create a new test file.

If no large-output fixture exists, do not fabricate production Worker output merely to hit truncation; static/unit invocation of the drain helper inside the existing test file is acceptable only if already feasible.

---

# Phase 11 — Resume A/B/C/D only after helper passes

Once standalone passes, use the already-prescribed matrix:

A. Worker only.
B. Worker + Streamlit.
C. Worker + governed probe.
D. Worker + Streamlit + governed probe.

Do not redesign scenarios.

Do not fix behavior.

Record factual stdout/stderr and signal metadata.

---

# Phase 12 — Matrix classification

Classify H/P/S/T/W/U using the same prior class definitions.

This correction authority does not authorize changing behavior after classification.

If root cause becomes obvious, stop at classification.

---

# Phase 13 — No-fix hard gate

Forbidden:

- Worker code changes;
- signal behavior changes;
- process-group changes;
- Streamlit/probe semantic changes;
- Replay changes;
- arbitrary sleeps;
- WP05/WP06/WP07 changes;
- package additions;
- GitHub mutations.

This remains diagnostic capture correction only.

---

# Phase 14 — Validation

Required order:

1. build after helper correction;
   - require **0 errors / 0 warnings**;
2. standalone CTRL_BREAK predecessor test;
3. A/B/C/D matrix if standalone passes;
4. focused WP08 tests affected by helper;
5. Infrastructure suite;
6. full .NET regression from 313/313 baseline plus exact diagnostic-test delta;
7. Python predecessor suites if C/D invoke the probe:
   - WP05 3/3;
   - WP06 6/6;
   - WP07 semantic 2/2;
   - WP07 presentation 2/2.

No Python mutation.

---

# Phase 15 — Residue

After every test/scenario:

- no Worker;
- no Streamlit where launched;
- no probe;
- no listener residue;
- no drain task left blocked;
- no pipe handle leak;
- no process/thread handle leak.

Forced cleanup is diagnostic-only fallback and must be reported.

---

# Phase 16 — Scope audit

Changed paths must remain only:

- `WindowsIsolatedProcessGroup.cs`;
- optionally `WP08LifecycleDemonstrationTests.cs` if matrix resumption requires previously prescribed instrumentation.

Everything else unchanged.

Prove zero:
- production;
- Python;
- Replay;
- WP05/WP06/WP07;
- package;
- GitHub;
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

## Runtime defect confirmation
Show the original async-handle exception and corrected synchronous model.

## Exact correction
- synchronous FileStream;
- Task.Run per stream;
- bounded synchronous drain;
- 64-KiB cap;
- one-second post-exit completion;
- timeout disposal behavior.

## Warning cleanup
- nullable signal warning resolution;
- CA1838 resolution;
- final build warnings.

## Standalone result
Exit code and capture health.

## A/B/C/D results
If reached:
- exit codes;
- signal metadata;
- stdout/stderr;
- timings.

## Classification
H/P/S/T/W/U.

## Validation
Focused/Infrastructure/full/Python counts.

## Residue
No owned task/pipe/process/listener/handle residue.

## Scope
Only authorized files changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 SYNCHRONOUS ANONYMOUS-PIPE DRAIN CORRECTION GITHUB MUTATIONS: ZERO`

## Next step

If matrix classifies H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If still U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

Stop only on a concrete attempted-correction failure:

- synchronous FileStream still cannot read the pipe handle;
- Task.Run concurrent drains deadlock or leak;
- disposing read stream does not safely unblock timed-out drain;
- standalone predecessor behavior regresses due to capture;
- build cannot return to zero warnings without broad redesign;
- the two authorized paths prove insufficient.

Do not stop because synchronous-drain mechanics are not already present.

---

# Terminal markers

Successful correction and classification:

`RELEASE 1.9 WP08 SYNCHRONOUS ANONYMOUS-PIPE DRAIN CORRECTION COMPLETE`

Blocked after attempted correction:

`RELEASE 1.9 WP08 SYNCHRONOUS ANONYMOUS-PIPE DRAIN CORRECTION BLOCKED`

Do not emit COMPLETE unless the synchronous capture model is actually implemented, build is clean, standalone capture is runtime-safe, and the A/B/C/D matrix executes far enough to classify the root cause out of Class U.

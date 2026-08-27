# Release 1.9 — WP08 Explicit Native Diagnostic Patch Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is an **explicit patching authority** for Release 1.9 WP08, canonical issue **#233**.

The previous implementation-only diagnostic authorities were blocked because the executor treated the absence of the required mechanics as a blocker.

This authority removes that ambiguity.

The executor **must patch the two authorized files** to add the exact native diagnostic mechanics and scenario matrix described below.

The executor may stop only if, after actually attempting the patch:

- the code does not compile and the compile error proves the prescribed structure incompatible;
- a Windows API call fails in a way that proves the prescribed mechanism unusable;
- a handle-safety invariant cannot be satisfied;
- the two authorized files are technically insufficient;
- a runtime deadlock/leak/safety issue is observed despite the prescribed bounded design.

The executor must **not** stop because the mechanics are currently absent.

This authority remains **diagnostic-only**.

No behavioral fix is authorized.
No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package addition.
No GitHub mutation.
No WP09.

---

# Accepted predecessor state

Read-only/preserve:

Production:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Authorized diagnostic paths:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Python read-only:
- `python/presentation/wp08_presentation_chain_probe.py`

Accepted evidence:
- standalone Worker + targeted CTRL_BREAK exits 0;
- combined Streamlit + Worker + governed probe reproduces `0xC0000142`;
- P2 real and within 8 seconds;
- Streamlit readiness succeeds;
- governed probe succeeds;
- focused predecessor WP08 4/4;
- .NET predecessor 313/313;
- Python predecessors green.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Exact mutation scope

Modify exactly:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

No other path.

If another path is truly required, STOP and identify it.

---

# Part A — Exact patch for WindowsIsolatedProcessGroup.cs

## A1 — Required constants

Add/ensure exact constants:

```csharp
private const uint CreateNewProcessGroup = 0x00000200;
private const uint StartfUseStdHandles = 0x00000100;
private const uint HandleFlagInherit = 0x00000001;
private const uint CtrlBreakEvent = 1;
private const int DiagnosticCaptureLimitBytes = 64 * 1024;
private static readonly TimeSpan DrainCompletionTimeout = TimeSpan.FromSeconds(1);
```

Use repository naming conventions if constants already exist.

Do not change the numeric values.

---

## A2 — Required native declarations

Add exactly the needed native declarations using `SetLastError = true` where appropriate:

```csharp
[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool CreatePipe(
    out SafeFileHandle hReadPipe,
    out SafeFileHandle hWritePipe,
    IntPtr lpPipeAttributes,
    uint nSize);

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool SetHandleInformation(
    SafeHandle hObject,
    uint dwMask,
    uint dwFlags);

[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool CreateProcessW(
    string? lpApplicationName,
    StringBuilder lpCommandLine,
    IntPtr lpProcessAttributes,
    IntPtr lpThreadAttributes,
    [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles,
    uint dwCreationFlags,
    IntPtr lpEnvironment,
    string? lpCurrentDirectory,
    ref STARTUPINFO lpStartupInfo,
    out PROCESS_INFORMATION lpProcessInformation);

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool GenerateConsoleCtrlEvent(
    uint dwCtrlEvent,
    uint dwProcessGroupId);
```

If equivalent declarations already exist, normalize them only as needed.

Do not add AttachConsole/FreeConsole/SetConsoleCtrlHandler/Job Objects.

---

## A3 — SECURITY_ATTRIBUTES

Because `CreatePipe` must create inheritable child write handles, define:

```csharp
[StructLayout(LayoutKind.Sequential)]
private struct SECURITY_ATTRIBUTES
{
    public int nLength;
    public IntPtr lpSecurityDescriptor;
    [MarshalAs(UnmanagedType.Bool)]
    public bool bInheritHandle;
}
```

Use this exact structure to create inheritable pipe handles.

If the current helper uses raw `IntPtr` instead of `SafeFileHandle`, adapt minimally but preserve semantics.

---

## A4 — Pipe-pair helper

Add an internal helper method equivalent to:

```csharp
private static (SafeFileHandle ParentRead, SafeFileHandle ChildWrite) CreateChildOutputPipe()
```

Behavior:

1. initialize SECURITY_ATTRIBUTES with:
   - `nLength = Marshal.SizeOf<SECURITY_ATTRIBUTES>()`
   - `lpSecurityDescriptor = IntPtr.Zero`
   - `bInheritHandle = true`

2. call `CreatePipe`.

3. call:
   `SetHandleInformation(parentRead, HandleFlagInherit, 0)`

4. if either call fails:
   - capture `Marshal.GetLastWin32Error()`;
   - dispose both handles;
   - throw `Win32Exception`.

5. return:
   - non-inheritable parent read;
   - inheritable child write.

No placeholder state.

---

## A5 — STARTUPINFO wiring

In launch path, before `CreateProcessW`:

Create:

```csharp
var stdoutPipe = CreateChildOutputPipe();
var stderrPipe = CreateChildOutputPipe();
```

Then set:

```csharp
startupInfo.dwFlags |= StartfUseStdHandles;
startupInfo.hStdOutput = stdoutPipe.ChildWrite.DangerousGetHandle();
startupInfo.hStdError = stderrPipe.ChildWrite.DangerousGetHandle();
```

For stdin:
- if current helper already has a valid inherited stdin handle, preserve it;
- otherwise use `GetStdHandle(STD_INPUT_HANDLE)` if already available in current code or add the minimal declaration;
- if no valid stdin is required by Worker, use the exact safe existing stdin behavior from repository conventions.

Do not leave `hStdInput` unset when `STARTF_USESTDHANDLES` is set.

If current `STARTUPINFO` uses `IntPtr`, assign exact handles.

---

## A6 — bInheritHandles

Call:

```csharp
CreateProcessW(..., bInheritHandles: true, ...)
```

This is mandatory for the child stdout/stderr write handles.

Preserve:
- `CREATE_NEW_PROCESS_GROUP`;
- command line;
- working directory;
- environment.

Do not add `CREATE_NEW_CONSOLE`.

---

## A7 — Exact post-launch handle closure

Immediately after successful `CreateProcessW`:

1. dispose parent copies of:
   - stdout child write handle;
   - stderr child write handle.

2. close/dispose primary thread handle according to current helper ownership.

3. retain:
   - process handle;
   - stdout parent read;
   - stderr parent read.

This exact ordering is required to allow EOF after child exit.

On launch failure:
- dispose all pipe handles;
- dispose any process/thread handles returned;
- throw factual Win32Exception.

---

## A8 — Stream wrappers

Wrap parent read handles in:

```csharp
var stdoutStream = new FileStream(
    stdoutReadHandle,
    FileAccess.Read,
    bufferSize: 4096,
    isAsync: true);

var stderrStream = new FileStream(
    stderrReadHandle,
    FileAccess.Read,
    bufferSize: 4096,
    isAsync: true);
```

Transfer ownership correctly so each native handle is closed exactly once.

Do not double-dispose.

---

## A9 — Bounded capture type

Add an internal result type:

```csharp
internal sealed record BoundedCapturedStream(
    string Text,
    bool Truncated,
    bool DrainTimedOut);
```

or equivalent immutable structure.

No unbounded data.

---

## A10 — Exact drain routine

Add a helper equivalent to:

```csharp
private static async Task<BoundedCapturedStream> DrainBoundedAsync(
    Stream stream,
    int limitBytes,
    CancellationToken cancellationToken)
```

Behavior:

- read bytes concurrently;
- retain only first 64 KiB;
- continue draining/discarding all further bytes;
- decode retained bytes as UTF-8 after completion;
- set `Truncated = true` if total exceeds 64 KiB;
- no unbounded StringBuilder;
- no blocking read loop on test thread.

Use a fixed buffer, e.g. 4096 bytes.

Do not stop reading at 64 KiB; continue draining.

---

## A11 — Start drain tasks immediately

Immediately after successful launch and post-launch write-handle closure:

```csharp
_stdoutDrainTask = DrainBoundedAsync(stdoutStream, DiagnosticCaptureLimitBytes, CancellationToken.None);
_stderrDrainTask = DrainBoundedAsync(stderrStream, DiagnosticCaptureLimitBytes, CancellationToken.None);
```

Do not wait for one before starting the other.

---

## A12 — Drain completion after process exit

After Worker process exit:

- await both drain tasks;
- each gets at most 1 second additional completion time;
- if timeout occurs:
  - mark `DrainTimedOut = true`;
  - dispose stream/handle;
  - do not hang.

If needed, use `Task.WhenAny(drainTask, Task.Delay(DrainCompletionTimeout))`.

No unbounded wait.

---

## A13 — Diagnostic snapshot

Add an immutable helper diagnostic record equivalent to:

```csharp
internal sealed record WindowsProcessDiagnosticSnapshot(
    int ProcessId,
    uint ProcessGroupId,
    uint CreationFlags,
    bool InheritHandles,
    bool StdoutRedirected,
    bool StderrRedirected,
    uint SignalEvent,
    uint SignalTargetGroupId,
    bool? SignalResult,
    int? SignalWin32Error,
    int? ExitCode,
    BoundedCapturedStream? Stdout,
    BoundedCapturedStream? Stderr);
```

Only factual fields.

Do not add guessed console-association fields.

---

## A14 — Signal metadata

Inside existing `RequestCtrlBreak()`:

Before API:
- set signal event = `CTRL_BREAK_EVENT`;
- set target group = Worker process-group ID.

Call existing:
`GenerateConsoleCtrlEvent`.

After call:
- store boolean result;
- if false, store `Marshal.GetLastWin32Error()`;
- if true, error = null.

Do not change signal behavior.
Do not retry.
Do not target group 0.

---

## A15 — Public/internal diagnostic access

Expose enough internal read-only members for the lifecycle test:

- `ProcessId`;
- `ProcessGroupId`;
- `DiagnosticSnapshot`;
- captured stdout/stderr after exit;
- signal result/error.

Do not expose raw native handles.

---

# Part B — Exact patch for WP08LifecycleDemonstrationTests.cs

## B1 — Scenario result record

Add an internal/private record inside the test file:

```csharp
private sealed record CtrlBreakTopologyScenarioResult(
    string Name,
    int WorkerPid,
    uint WorkerGroupId,
    int? StreamlitPid,
    int? ProbePid,
    int? ListenerOwnerPid,
    int WorkerExitCode,
    bool SignalResult,
    int? SignalWin32Error,
    TimeSpan WorkerLaunchElapsed,
    TimeSpan P1Elapsed,
    TimeSpan P2Elapsed,
    TimeSpan? StreamlitReadyElapsed,
    TimeSpan? ProbeExitElapsed,
    TimeSpan SignalElapsed,
    TimeSpan WorkerExitElapsed,
    WindowsProcessDiagnosticSnapshot Diagnostics);
```

Adapt namespace/type access minimally.

---

## B2 — Shared scenario runner

Add one helper method inside the test file equivalent to:

```csharp
private async Task<CtrlBreakTopologyScenarioResult> RunCtrlBreakTopologyScenarioAsync(
    string name,
    bool launchStreamlit,
    bool runProbe)
```

It must:

1. allocate isolated runtime resources using existing harness logic;
2. start one Stopwatch;
3. optionally launch Streamlit and wait readiness;
4. launch Worker using existing Windows helper with `--wp08-test-liveness`;
5. observe P1;
6. observe P2 within fixed 8-second rule;
7. optionally run governed probe against P2 and wait for exit;
8. capture listener owner PID if Streamlit exists;
9. send targeted CTRL_BREAK;
10. wait Worker exit;
11. capture helper diagnostics;
12. cleanup owned resources;
13. return result.

No semantic changes.

---

## B3 — Scenario A test

Add:

```csharp
[Fact]
public async Task Diagnostic_A_WorkerOnly()
```

Call:
`RunCtrlBreakTopologyScenarioAsync("A", false, false)`

Emit bounded diagnostics.

Do not “fix” based on expected exit.

---

## B4 — Scenario B test

Add:

```csharp
[Fact]
public async Task Diagnostic_B_WorkerAndStreamlit()
```

Call:
`RunCtrlBreakTopologyScenarioAsync("B", true, false)`

Emit bounded diagnostics.

---

## B5 — Scenario C test

Add:

```csharp
[Fact]
public async Task Diagnostic_C_WorkerAndProbe()
```

Call:
`RunCtrlBreakTopologyScenarioAsync("C", false, true)`

Emit bounded diagnostics.

---

## B6 — Scenario D test

Add:

```csharp
[Fact]
public async Task Diagnostic_D_WorkerStreamlitAndProbe()
```

Call:
`RunCtrlBreakTopologyScenarioAsync("D", true, true)`

Emit bounded diagnostics.

This must reproduce the known combined path.

---

## B7 — Diagnostic output

Use xUnit test output if the test class already has `ITestOutputHelper`.

If not, add only constructor injection for `ITestOutputHelper`.

Output per scenario:

- Name
- Worker PID/group
- Streamlit PID
- Probe PID
- Listener owner
- creation flags
- inherit handles
- stdout/stderr redirected
- signal target/result/error
- exit code
- elapsed timings
- stdout truncation/timeout/text
- stderr truncation/timeout/text

No persistent files.

---

## B8 — No pass-forcing assertions

These diagnostic tests may assert only safety/setup invariants:

- process launched;
- P2 reached;
- signal API call returned a boolean;
- cleanup completed.

Do not assert exit must be 0 in B/C/D merely to force test semantics.

The purpose is factual classification.

Preserve the original standalone passing CTRL_BREAK test separately.

---

# Part C — Matrix execution and classification

## C1 — Run exact matrix

Run:
- A
- B
- C
- D

Capture factual outputs.

## C2 — Compare outcomes

Classify:

### Class H
Ordering/cleanup difference correlates with failure while process/stdio topology does not.

### Class P
Process-group/signal topology differs and correlates.

### Class S
Inherited-handle/stdio topology differs and correlates.

### Class T
Topology equivalent; timing/ordering explains failure reproducibly.

### Class W
Worker receives cancellation but production exit path fails independently of harness topology.

### Class U
Still unresolved.

Do not fix.

---

# Part D — Validation

After patch:

1. build test project;
2. run A/B/C/D;
3. run original standalone CTRL_BREAK focused test;
4. run focused WP08 tests;
5. run build.

Because `WindowsIsolatedProcessGroup.cs` launch mechanics are modified:
6. run Infrastructure suite;
7. run full .NET regression from 313/313 baseline plus exact diagnostic-test delta.

If C/D invoke Python probe:
8. verify existing Python predecessors remain green:
   - WP05 3/3
   - WP06 6/6
   - WP07 semantic 2/2
   - WP07 presentation 2/2

No Python file changes.

---

# Part E — Residue

After each scenario:
- Worker absent;
- Streamlit absent if launched;
- probe absent if launched;
- listener residue zero;
- all pipe handles disposed;
- no drain tasks left running;
- process/thread handles disposed.

Forced cleanup may be used after a failing diagnostic path, but report it as cleanup only.

No global kill by image name.

---

# Part F — No-fix hard gate

Even if classification is obvious:

- do not alter Worker code;
- do not alter CTRL_BREAK behavior;
- do not alter process-group flags;
- do not alter Streamlit/probe semantics;
- do not alter test sequencing to make D pass;
- do not add sleeps to mask race;
- do not modify Replay/WP05/WP06/WP07;
- do not add packages;
- do not mutate GitHub.

This authority ends at classification.

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

## Exact patch
List exact members added/changed in both files.

## Native mechanics
Confirm:
- CreatePipe;
- SetHandleInformation;
- STARTF_USESTDHANDLES;
- bInheritHandles=TRUE;
- close ordering;
- 64-KiB bounded drains;
- 1-second drain timeout;
- signal/error capture.

## A/B/C/D results
For each:
- exit code;
- topology;
- signal result/error;
- timings;
- bounded stdout/stderr.

## Differential
Exact factual difference.

## Classification
H/P/S/T/W/U.

## Validation
Focused/Infrastructure/full/Python counts.

## Residue
No owned process/listener/pipe/task/handle residue.

## Scope
Only two files changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 EXPLICIT NATIVE DIAGNOSTIC PATCH GITHUB MUTATIONS: ZERO`

## Next step

If H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

The executor may stop only after attempting the patch and proving one concrete blocker:

- prescribed P/Invoke signature/struct cannot compile;
- pipe handle inheritance cannot be made safe;
- CreateProcessW fails with factual Win32 error under the prescribed mechanics;
- bounded concurrent drain deadlocks/leaks despite the exact design;
- the two authorized files cannot support the patch;
- runner safety is materially weakened.

Do not stop because the current files lack the mechanics.

---

# Terminal markers

Successful classification:

`RELEASE 1.9 WP08 EXPLICIT NATIVE DIAGNOSTIC PATCH COMPLETE`

Blocked after real attempted implementation:

`RELEASE 1.9 WP08 EXPLICIT NATIVE DIAGNOSTIC PATCH BLOCKED`

Do not emit COMPLETE unless the patch is actually implemented, A/B/C/D actually run with factual diagnostics, and the root cause is classified out of Class U.

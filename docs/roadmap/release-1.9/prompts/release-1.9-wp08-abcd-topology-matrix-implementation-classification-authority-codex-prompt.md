# Release 1.9 — WP08 A/B/C/D Topology-Matrix Implementation + Classification Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **very narrow test-only diagnostic authority** for Release 1.9 WP08, canonical issue **#233**.

Its sole purpose is to implement and execute the missing shared A/B/C/D topology matrix in the existing lifecycle test file, using the now-corrected Windows native diagnostic-capture helper as fixed predecessor state.

This authority does **not** authorize any behavioral fix.

No helper/native-capture redesign.
No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package addition.
No GitHub mutation.
No WP09.

---

# Accepted predecessor state

Treat the following as binding predecessor state.

## Windows helper
`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted corrected behavior:
- `CreatePipe`;
- `SetHandleInformation`;
- `STARTF_USESTDHANDLES`;
- `bInheritHandles = TRUE`;
- synchronous `FileStream` reads;
- concurrent `Task.Run` stdout/stderr drains;
- retained 64-KiB cap;
- excess bytes continue draining/discarding;
- nullable signal-state correction;
- narrow CA1838 suppression;
- preserved `CREATE_NEW_PROCESS_GROUP`;
- preserved targeted `CTRL_BREAK_EVENT`.

Validation:
- build: **0 warnings / 0 errors**;
- standalone CTRL_BREAK predecessor: **1/1 passed**, Worker exit `0`.

The helper is frozen under this authority.

## WP08 focused state
Current focused WP08:
- **4 passed / 1 failed**.

Known failing combined scenario:
- expected Worker exit `0`;
- actual Worker exit `-1073741502` / `0xC0000142`.

## Other predecessor evidence
- P1 pass-through preserved;
- genuine P2 within fixed 8-second bound preserved;
- governed Python probe exists;
- Streamlit readiness previously reached;
- no behavioral fix applied.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Exact mutation scope

Modify exactly one file:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else is read-only.

Do not modify:
- `WindowsIsolatedProcessGroup.cs`;
- Worker production files;
- Python probe;
- any WP05/WP06/WP07 file.

If the existing lifecycle test file is technically insufficient to host the matrix, STOP and identify the exact missing path authority.

---

# Objective

Implement one shared diagnostic scenario runner and four factual topology scenarios:

A. Worker only.
B. Worker + Streamlit.
C. Worker + governed Python probe.
D. Worker + Streamlit + governed Python probe.

Then execute all four using the same corrected Windows helper and classify the failure as:

- Class H — harness sequencing/cleanup;
- Class P — process-group/console helper;
- Class S — stdio/inherited-handle;
- Class T — timing/race;
- Class W — Worker cancellation/exit path;
- Class U — unresolved.

No fix may be applied.

---

# Phase 0 — Read-only test-file inspection

Read `WP08LifecycleDemonstrationTests.cs` completely.

Identify and reuse existing local methods for:

- isolated runtime allocation;
- Worker launch;
- P1/P2 observation;
- Streamlit launch/readiness;
- governed Python probe launch;
- listener ownership;
- cleanup.

Do not duplicate working harness logic unnecessarily.

---

# Phase 1 — Shared scenario result

Add a private immutable result type inside the same test file, conceptually:

```csharp
private sealed record CtrlBreakTopologyScenarioResult(
    string Name,
    int WorkerPid,
    uint WorkerGroupId,
    int? StreamlitPid,
    int? ProbePid,
    int? ListenerOwnerPid,
    int WorkerExitCode,
    bool? SignalResult,
    int? SignalWin32Error,
    TimeSpan WorkerLaunchElapsed,
    TimeSpan P1Elapsed,
    TimeSpan P2Elapsed,
    TimeSpan? StreamlitReadyElapsed,
    TimeSpan? ProbeLaunchElapsed,
    TimeSpan? ProbeExitElapsed,
    TimeSpan CtrlBreakElapsed,
    TimeSpan WorkerExitElapsed,
    string CapturedStdout,
    string CapturedStderr,
    bool StdoutTruncated,
    bool StderrTruncated,
    bool StdoutDrainTimedOut,
    bool StderrDrainTimedOut);
```

Adapt field names only to match the actual helper API.

Do not invent non-factual fields.

---

# Phase 2 — Shared scenario runner

Add exactly one shared method inside the test file, conceptually:

```csharp
private async Task<CtrlBreakTopologyScenarioResult> RunCtrlBreakTopologyScenarioAsync(
    string name,
    bool launchStreamlit,
    bool runProbe)
```

It must:

1. allocate isolated harness-owned runtime/database/port as existing logic requires;
2. start one monotonic `Stopwatch`;
3. optionally launch Streamlit;
4. if launched, wait exact governed readiness and listener ownership;
5. launch Worker via frozen `WindowsIsolatedProcessGroup`;
6. use `--wp08-test-liveness`;
7. observe P1;
8. observe genuine P2 within fixed 8-second rule;
9. optionally invoke the existing governed Python probe against real P2 handoff;
10. if probe launched, require probe process completion before CTRL_BREAK;
11. capture current listener owner if Streamlit exists;
12. send targeted CTRL_BREAK through frozen helper;
13. wait Worker exit;
14. capture helper diagnostics;
15. cleanup all owned resources;
16. return the factual result.

Do not change launch/signal semantics.

---

# Phase 3 — Scenario A

Add a focused diagnostic test/method for:

## A — Worker only

Settings:
- `launchStreamlit = false`
- `runProbe = false`

Required:
- same Worker helper;
- same `--wp08-test-liveness`;
- P2 reached;
- targeted CTRL_BREAK sent;
- Worker exit recorded;
- stdout/stderr diagnostics recorded;
- cleanup completed.

Do not hardcode expected exit in the diagnostic runner.

Preserve the separate original standalone predecessor test that asserts exit 0.

---

# Phase 4 — Scenario B

Add:

## B — Worker + Streamlit

Settings:
- `launchStreamlit = true`
- `runProbe = false`

Required:
- Streamlit reaches governed readiness;
- listener owner recorded;
- Worker reaches P2;
- targeted CTRL_BREAK sent;
- Worker exit + diagnostics recorded;
- cleanup Worker + Streamlit.

Do not alter Streamlit launch behavior to make the result pass.

---

# Phase 5 — Scenario C

Add:

## C — Worker + governed probe

Settings:
- `launchStreamlit = false`
- `runProbe = true`

Required:
- Worker reaches P2;
- governed Python probe runs against real P2 handoff;
- probe exits before CTRL_BREAK;
- targeted CTRL_BREAK sent;
- Worker exit + diagnostics recorded;
- cleanup.

Do not alter probe semantics.

---

# Phase 6 — Scenario D

Add:

## D — Worker + Streamlit + governed probe

Settings:
- `launchStreamlit = true`
- `runProbe = true`

Required:
- Streamlit ready;
- Worker reaches P2;
- probe completes;
- targeted CTRL_BREAK sent;
- Worker exit + diagnostics recorded;
- cleanup.

This must reproduce the known failing combined topology faithfully.

---

# Phase 7 — Test output

Use existing xUnit output mechanism if present; otherwise add only `ITestOutputHelper` constructor injection in this same file.

Emit one bounded diagnostic block per scenario including:

- scenario name;
- Worker PID/group ID;
- Streamlit PID/state;
- probe PID/state;
- listener owner PID;
- signal result/error;
- Worker exit code;
- P1/P2 timings;
- Streamlit readiness timing;
- probe timing;
- CTRL_BREAK timing;
- Worker exit timing;
- stdout truncation/timeout/text;
- stderr truncation/timeout/text.

No persistent files.
No new logs.

---

# Phase 8 — Diagnostic assertions

These matrix tests may assert only setup/safety invariants necessary for factual execution, such as:

- Worker launched;
- P2 reached;
- probe succeeded where required;
- Streamlit readiness succeeded where required;
- signal call executed;
- cleanup completed.

Do **not** assert B/C/D Worker exit must be 0.

The matrix is for classification.

Do not rewrite the old combined acceptance test yet.

---

# Phase 9 — Execute exact matrix

Run A, B, C, D.

Record exact outputs.

No E scenario under this authority unless A/B/C/D are technically unable to distinguish one single remaining factor and the existing file can vary that one factor without helper changes.

Prefer no E.

---

# Phase 10 — Classification rules

Classify exactly one root-cause class.

## Class H — Harness sequencing/cleanup
Use only if:
- A/B/C/D process/stdio topology are materially equivalent where failure differs;
- and failure correlates with ordering/cleanup state.

## Class P — Process-group/console helper
Use only if:
- failure correlates with process-group/signal topology or helper signal facts.

Because helper is frozen, classification may identify a helper defect but must not fix it.

## Class S — Stdio/inherited-handle
Use only if:
- failure correlates with stdout/stderr redirection, handle lifecycle, drain state, or related factual differences.

## Class T — Timing/race
Use only if:
- topology and handles are equivalent;
- outcome correlates reproducibly with timing/order.

## Class W — Worker cancellation/exit path
Use only if:
- signal reaches Worker;
- cancellation path is evidenced;
- failure persists independently of external topology differences.

## Class U — unresolved
Use if matrix does not establish a causal class.

Do not overclaim.

---

# Phase 11 — Differential table

Produce a compact matrix:

| Scenario | Streamlit | Probe | P2 | Signal | Exit | Stdout | Stderr | Key timing |
|---|---|---|---|---|---|---|---|---|

Use factual results only.

Then identify the minimal observed differential associated with `0xC0000142`.

---

# Phase 12 — No-fix hard gate

Even if classification is obvious:

- do not modify Worker;
- do not modify helper;
- do not change CTRL_BREAK behavior;
- do not change Streamlit launch;
- do not change probe;
- do not change sequencing for success;
- do not add sleeps;
- do not modify Replay/WP05/WP06/WP07;
- do not add packages;
- do not mutate GitHub.

This authority ends at classification.

---

# Phase 13 — Validation

Run:

1. test project build;
2. A/B/C/D matrix;
3. original standalone CTRL_BREAK predecessor test;
4. all focused WP08 tests in the file;
5. repository build.

If matrix additions increase test count, explain exact diagnostic-test delta.

Because helper is frozen and already validated, full Infrastructure/full .NET regression is optional unless the test-file changes themselves affect broader test execution.

If the matrix executes probe in C/D, run existing Python predecessor smoke only if needed to confirm no environment regression:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2.

No Python changes.

---

# Phase 14 — Residue

After every scenario:

- Worker absent;
- Streamlit absent where launched;
- probe absent where launched;
- listener residue zero;
- no harness-owned temp process residue.

Helper-owned pipe/task/handle residue is observed through the frozen helper diagnostics and must be reported.

Forced cleanup may be used only as diagnostic cleanup fallback and must be stated.

---

# Phase 15 — Scope audit

Changed path must be exactly:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else unchanged.

Prove zero:
- helper mutation;
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

## Shared runner
Exact method and reused harness logic.

## A/B/C/D results
For each:
- topology;
- P2;
- signal result;
- exit code;
- timings;
- stdout/stderr.

## Differential table
Exact factual comparison.

## Root-cause classification
H/P/S/T/W/U.

## Confidence
Why evidence supports the class.

## Standalone preservation
Original predecessor CTRL_BREAK result.

## Validation
Build/focused counts and any Python smoke run.

## Residue
No owned process/listener residue.

## Scope
Only lifecycle test file changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 A/B/C/D TOPOLOGY-MATRIX IMPLEMENTATION GITHUB MUTATIONS: ZERO`

## Next step

If H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

The executor may stop only if, after attempting the test-file implementation:

- existing lifecycle test file lacks access to required helper diagnostics;
- A/B/C/D cannot be expressed without modifying the frozen helper;
- Streamlit/probe orchestration requires a new helper path;
- matrix execution creates an unsafe cleanup condition;
- compilation/runtime proves the single-file scope insufficient.

Do not stop because the matrix is not already present.

---

# Terminal markers

Successful classification:

`RELEASE 1.9 WP08 A/B/C/D TOPOLOGY-MATRIX IMPLEMENTATION AND CLASSIFICATION COMPLETE`

Blocked/unresolved:

`RELEASE 1.9 WP08 A/B/C/D TOPOLOGY-MATRIX IMPLEMENTATION AND CLASSIFICATION BLOCKED`

Do not emit COMPLETE unless the shared matrix runner is implemented, all A/B/C/D scenarios execute, and the root cause is classified out of Class U.

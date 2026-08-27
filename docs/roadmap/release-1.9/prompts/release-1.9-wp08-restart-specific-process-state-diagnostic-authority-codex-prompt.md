# Release 1.9 — WP08 Restart-Specific Process-State Diagnostic Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow restart-specific diagnostic authority** for Release 1.9 WP08, canonical issue **#233**.

Its sole purpose is to determine why **Worker B**, launched after a fully exited/disposed Worker A in the same lifecycle test/runtime, still exits with:

`-1073741502 (0xC0000142)`

while all first-worker topology scenarios A/B/C/D exit `0`.

This authority is diagnostic-only.

No behavioral fix.
No helper redesign.
No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package change.
No signing/SAC change.
No GitHub mutation.
No WP09.

---

# Accepted predecessor state

Treat as binding predecessor evidence:

## Smart App Control environment
- local-development signing remediation is complete and documented;
- test assemblies load successfully;
- Windows App Control is no longer the active blocker.

Do not modify signing setup under this authority.

## Frozen Windows helper
`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted:
- `CreatePipe`;
- `SetHandleInformation`;
- `STARTF_USESTDHANDLES`;
- `bInheritHandles = TRUE`;
- synchronous pipe reads;
- concurrent bounded drains;
- 64-KiB retention;
- targeted `CTRL_BREAK_EVENT`;
- standalone Worker exit `0`.

Helper is read-only.

## Existing topology matrix
In `WP08LifecycleDemonstrationTests.cs`:

- A Worker only → exit `0`
- B Worker + Streamlit → exit `0`
- C Worker + governed probe → exit `0`
- D Worker + Streamlit + governed probe → exit `0`

All:
- reach P2 within governed bound;
- signal result `true`;
- empty/non-truncated stdout/stderr;
- no drain timeout.

## Restart sequencing fix
Accepted test-only change:

```csharp
workerA.RequestCtrlBreak();
Assert.True(workerA.WaitForExit(TimeSpan.FromSeconds(5)));
Assert.Equal(0, workerA.ExitCode);
workerA.Dispose();
workerA = null;
workerB = StartWorker(...);
```

Shared Streamlit/handoff/database/runtime remain active across restart as required.

Result:
- Worker B still reproducibly exits `0xC0000142`.

Therefore original Class-H-only explanation is insufficiently specific.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Exact mutation scope

Modify only:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else is read-only.

Do not modify:
- helper;
- Worker;
- Python probe;
- signing scripts/project files;
- Replay;
- WP05/WP06/WP07;
- docs.

If the restart-specific matrix cannot be implemented in this file with existing helper APIs, STOP and identify the exact missing authority.

---

# Objective

Determine whether `0xC0000142` follows:

1. simply being the **second Worker process** in one harness run;
2. Streamlit remaining alive across restart;
3. governed probe use before/after restart;
4. reuse of the same runtime/handoff/database path;
5. another restart-state combination.

Implement and execute R0/R1/R2/R3/R4 as defined below.

No fix may be applied.

---

# Phase 0 — Read-only baseline comparison

Inspect the current lifecycle test and identify:

- single-worker D passing flow;
- Worker A shutdown/restart boundary;
- Worker B launch;
- shared runtime path;
- handoff path;
- DB path;
- Streamlit lifetime;
- probe timing;
- P1/P2 timing;
- Worker B signal timing;
- final cleanup.

Document exact state inherited/reused by Worker B.

---

# Phase 1 — Shared restart-scenario result

Add a private immutable result record in the same test file containing factual fields such as:

- scenario name;
- Worker A PID/group/exit;
- Worker B PID/group/exit;
- Streamlit PID/state;
- probe invocation count/state;
- runtime path reused yes/no;
- handoff path reused yes/no;
- DB path reused yes/no;
- Worker B P1/P2 elapsed;
- Worker B signal result/error;
- Worker B stdout/stderr;
- cleanup result.

Do not invent inaccessible helper metadata.

---

# Phase 2 — Shared restart runner

Add one private method conceptually:

```csharp
private async Task<RestartProcessStateScenarioResult> RunRestartProcessStateScenarioAsync(
    string name,
    bool keepStreamlitAcrossRestart,
    bool runProbeBeforeRestart,
    bool runProbeForWorkerB,
    bool reuseRuntimeState)
```

The method must use existing harness logic only.

It must:
1. create deterministic isolated resources;
2. launch Worker A;
3. observe governed readiness/P2 as required;
4. optionally run probe before restart;
5. gracefully cancel Worker A;
6. wait exit `0`;
7. dispose Worker A helper;
8. apply scenario-specific restart state;
9. launch Worker B;
10. observe governed Worker B readiness/P2;
11. optionally run probe for B;
12. send targeted CTRL_BREAK;
13. capture Worker B exit and diagnostics;
14. clean owned resources;
15. return factual result.

No new helper path.

---

# Phase 3 — R0 control: fresh single Worker

Implement a control scenario:

## R0 — Fresh single Worker

- no preceding Worker A;
- otherwise match Worker B launch configuration as closely as possible;
- same Worker helper;
- same test-liveness;
- same runtime shape;
- same Streamlit/probe settings chosen to match the failing restart case.

Purpose:
- prove the exact Worker-B-like topology still exits `0` when it is the first Worker.

Do not duplicate existing D if D already provides identical control; if identical, formally reuse D evidence and label it R0-control instead of adding a redundant test.

---

# Phase 4 — R1: second Worker only

## R1 — Worker A → Worker B, no Streamlit, no probe

- Worker A runs and exits `0`;
- dispose A helper;
- launch B;
- no Streamlit;
- no probe;
- reuse same runtime/handoff/database only if restart contract requires it;
- reach B P2;
- CTRL_BREAK;
- record B exit.

Interpretation:
- if R1 fails, “second Worker process / reused runtime state” is sufficient.

---

# Phase 5 — R2: second Worker + Streamlit

## R2 — Worker A → Worker B with Streamlit alive across restart

- Streamlit launched before/with A according to governed lifecycle;
- remains alive through A exit and B launch;
- no probe;
- B reaches P2;
- CTRL_BREAK;
- record B exit.

Interpretation:
- compare R2 vs R1 to isolate Streamlit-across-restart effect.

---

# Phase 6 — R3: second Worker + probe state

## R3 — Worker A → Worker B with governed probe involvement, no Streamlit

Use the probe in the exact restart phase that most closely matches the failing legacy test.

Preferred:
- run probe against A or B only if the legacy test does so;
- require probe exit before B CTRL_BREAK.

No Streamlit.

Interpretation:
- compare R3 vs R1 to isolate probe/restart state.

---

# Phase 7 — R4: full restart topology

## R4 — Worker A → Worker B + Streamlit + governed probe

Reproduce the failing restart scenario faithfully:

- same shared runtime;
- same handoff;
- same database;
- Streamlit lifetime;
- probe timing;
- Worker A exit/dispose;
- Worker B launch/readiness;
- CTRL_BREAK.

This is the canonical failing restart control.

---

# Phase 8 — Optional runtime-reuse split

Only if R1 fails and the cause could be either:
- “second Worker process” itself; or
- reuse of runtime/handoff/database state,

add exactly one one-factor split inside the same test file:

## R1F — second Worker with fresh runtime state

- Worker A exits/disposes;
- Worker B launches using a fresh isolated runtime/handoff/database;
- no Streamlit/probe.

If:
- R1 fails but R1F passes → reused runtime state is implicated.
- both fail → second-process/restart-process state is implicated.

Do not add more permutations unless required by factual ambiguity.

---

# Phase 9 — Timing instrumentation

Use one monotonic `Stopwatch` per scenario.

Record:
- A launch;
- A P2;
- A CTRL_BREAK;
- A exit;
- A dispose complete;
- B launch;
- B P1;
- B P2;
- probe launch/exit where applicable;
- B CTRL_BREAK;
- B exit;
- cleanup.

Do not use arbitrary sleeps.

---

# Phase 10 — Runtime-state snapshot

Immediately before Worker B launch, record factual restart state:

- Worker A exited = true;
- Worker A helper disposed = true;
- Streamlit alive yes/no;
- probe alive yes/no (should normally be no);
- canonical handoff exists yes/no;
- handoff revision/identity if already available;
- temp handoff siblings count;
- DB exists yes/no;
- relevant sidecar existence;
- runtime directory exists yes/no.

No mutation merely for observation.

---

# Phase 11 — Worker B pre-signal snapshot

Immediately before B CTRL_BREAK record:

- B PID/group;
- B P2 reached;
- signal target group;
- Streamlit alive yes/no;
- probe alive/exited;
- listener owner if Streamlit;
- handoff identity;
- stdout/stderr status;
- timing since B launch/P2.

Use existing helper diagnostics.

---

# Phase 12 — Classification model

Classify exactly one restart-specific cause family.

## R-SECOND
Failure follows second Worker process even without Streamlit/probe and with fresh runtime if tested.

## R-RUNTIME
Failure requires reuse of runtime/handoff/database state.

## R-STREAMLIT
R1 passes; R2 fails.

## R-PROBE
R1 passes; R3 fails.

## R-COMBINED
R1/R2/R3 pass; only R4 fails.

## R-TIMING
Topology/state equivalent but restart timing reproducibly determines result.

## R-UNRESOLVED
Evidence insufficient.

This is more specific than prior H/P/S/T/W classification.

Do not fix.

---

# Phase 13 — Differential table

Produce:

| Scenario | Prior Worker | Reuse Runtime | Streamlit | Probe | B Signal | B Exit |
|---|---|---|---|---|---|---|

Include R0/R1/R2/R3/R4 and optional R1F.

Also report:
- handoff state;
- DB state;
- key timings.

---

# Phase 14 — No-fix hard gate

Forbidden:
- Worker changes;
- helper changes;
- signal changes;
- process-group changes;
- signing/SAC changes;
- Streamlit/probe semantic changes;
- Replay changes;
- WP05/WP06/WP07 changes;
- arbitrary sleeps;
- GitHub mutation.

This authority ends at restart-specific classification.

---

# Phase 15 — Validation

Run:
1. build;
2. R0/R1/R2/R3/R4;
3. optional R1F only if needed;
4. original standalone CTRL_BREAK;
5. A/B/C/D matrix;
6. focused WP08 suite.

Report exact count delta.

Run Infrastructure/full .NET only if the test-file changes themselves create broader regression risk or repository governance requires it.

No Python mutation; run predecessor Python smoke only if probe scenarios fail unexpectedly.

---

# Phase 16 — Residue

After every restart scenario:
- Worker A absent;
- Worker B absent;
- Streamlit absent where launched after cleanup;
- probe absent;
- listener residue zero;
- no harness-owned process residue;
- handoff/runtime/database cleanup according to diagnostic scenario cleanup rule.

No global process kill.

---

# Phase 17 — Scope audit

Changed path must be exactly:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else unchanged.

Prove zero:
- helper;
- production;
- Python;
- signing;
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

## Restart matrix implementation
Shared runner and exact scenarios.

## R0/R1/R2/R3/R4 results
For each:
- topology;
- reused state;
- B signal;
- B exit;
- timings;
- handoff/DB facts.

## Optional R1F
Only if needed.

## Differential
Exact state required for `0xC0000142`.

## Restart-specific classification
R-SECOND / R-RUNTIME / R-STREAMLIT / R-PROBE / R-COMBINED / R-TIMING / R-UNRESOLVED.

## Preservation
Standalone CTRL_BREAK and A/B/C/D results.

## Validation
Build/focused counts and any broader suites run.

## Residue
No owned process/listener residue.

## Scope
Only lifecycle test file changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 RESTART-SPECIFIC PROCESS-STATE DIAGNOSTIC GITHUB MUTATIONS: ZERO`

## Next step

If classified:

`WP08 RESTART ROOT CAUSE CLASSIFIED — NARROW RESTART-SPECIFIC FIX AUTHORITY REQUIRED`

If unresolved:

`WP08 RESTART ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Stop conditions

Stop only if, after attempting implementation:

- existing test file cannot express required restart scenarios without helper changes;
- factual runtime/handoff/database state cannot be observed with existing APIs;
- cleanup becomes unsafe;
- compilation/runtime proves single-file scope insufficient.

Do not stop because R1–R4 are not already present.

---

# Terminal markers

Successful classification:

`RELEASE 1.9 WP08 RESTART-SPECIFIC PROCESS-STATE DIAGNOSTIC COMPLETE`

Blocked/unresolved:

`RELEASE 1.9 WP08 RESTART-SPECIFIC PROCESS-STATE DIAGNOSTIC BLOCKED`

Do not emit COMPLETE unless the restart matrix executes and the failure is classified more specifically than the prior generic Class H.

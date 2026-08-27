# Release 1.9 — WP08 Restart/Cleanup Sequencing Fix Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow Class-H fix authority** for Release 1.9 WP08, canonical issue **#233**.

The root cause has been classified authoritatively as:

`Class H — harness sequencing/cleanup`

The classification is based on:
- A Worker-only → exit 0;
- B Worker + Streamlit → exit 0;
- C Worker + governed probe → exit 0;
- D Worker + Streamlit + governed probe → exit 0;
- only the legacy restart-specific Worker A→B lifecycle scenario fails with Worker B exit `0xC0000142`.

Therefore this authority may fix only the restart/cleanup sequencing in the existing WP08 lifecycle test.

No helper redesign.
No production mutation.
No Python mutation.
No Replay mutation.
No WP05/WP06/WP07 mutation.
No package addition.
No GitHub lifecycle mutation.
No WP09.

# Accepted predecessor state

Preserve as binding:

## Frozen helper
`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted:
- native pipe capture;
- synchronous drains;
- `CREATE_NEW_PROCESS_GROUP`;
- targeted CTRL_BREAK;
- standalone exit 0;
- no helper changes under this authority.

## Lifecycle diagnostics
`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Accepted:
- shared A/B/C/D runner;
- A/B/C/D matrix passes **4/4**;
- original standalone CTRL_BREAK predecessor exits 0;
- focused WP08 currently **8 passed / 1 failed**;
- failing test is restart-specific legacy lifecycle path.

## Classified evidence
Matrix:
- A: Worker only → exit 0
- B: Worker + Streamlit → exit 0
- C: Worker + probe → exit 0
- D: Worker + Streamlit + probe → exit 0

All reach P2 within bound.
Captured stdout/stderr are empty, not truncated, with no drain timeout.
No owned process/listener residue observed in matrix.
Build: 0 warnings / 0 errors.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

# Exact mutation scope

Modify exactly one file:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Everything else read-only.

Do not modify:
- `WindowsIsolatedProcessGroup.cs`;
- Worker production;
- Python probe;
- Replay;
- WP05/WP06/WP07;
- docs.

If the fix requires another path, STOP and identify exact missing authority.

# Objective

Fix only the restart/cleanup sequencing so that the legacy Worker A→B lifecycle scenario:

1. preserves Worker A successful lifecycle;
2. preserves Streamlit independence;
3. performs restart cleanup in the governed order;
4. launches Worker B safely;
5. reaches Worker B readiness/P2 as required;
6. sends targeted CTRL_BREAK at the correct time;
7. Worker B exits `0`;
8. all final residue assertions pass.

The fix must be derived by comparing the failing restart path against the passing D scenario.

# Phase 0 — Read-only diff of passing D vs failing restart path

Within `WP08LifecycleDemonstrationTests.cs`, compare:

## Passing D
- Streamlit launch timing;
- Worker launch timing;
- P1/P2 observation;
- probe timing;
- CTRL_BREAK timing;
- Worker wait-for-exit;
- cleanup ordering;
- Streamlit lifetime;
- handoff handling;
- temp DB handling.

## Failing restart path
- Worker A shutdown;
- post-A handoff state;
- restart cleanup;
- Worker B launch;
- Streamlit state during restart;
- probe state;
- Worker B P1/P2;
- CTRL_BREAK timing;
- cleanup-before-exit possibility;
- final cleanup.

Produce exact sequencing delta before mutation.

# Phase 1 — Candidate Class-H causes

Evaluate only harness sequencing/cleanup causes:

- cleanup starts before Worker B fully exits;
- Streamlit shutdown/cleanup overlaps Worker B CTRL_BREAK;
- handoff cleanup occurs at the wrong phase;
- probe/subprocess cleanup overlaps Worker B shutdown;
- Worker B signal is sent before the equivalent D readiness point;
- restart-specific resource disposal order differs from D.

Do not reconsider helper/production classes unless new evidence contradicts Class H. If so, STOP.

# Phase 2 — Exact fix principle

Choose the smallest sequencing change that makes the restart path structurally match the passing D path for Worker B shutdown.

Prefer:
- reuse existing passing sequence;
- move cleanup after `WaitForExit`;
- keep Streamlit alive until Worker B exit if D proves that topology stable;
- delay handoff/runtime cleanup until after Worker B process termination;
- preserve probe completion-before-signal if used.

Do not add arbitrary sleep.

Use only existing readiness/process-exit observables.

# Phase 3 — Worker A sequence

Preserve Worker A behavior exactly unless the restart assertion itself proves an ordering defect.

Worker A must:
- reach governed state;
- exit using accepted lifecycle rule;
- have exit observed before restart transition;
- leave the intermediate handoff state required by authority 1.

Do not alter Worker A artificially.

# Phase 4 — Restart transition boundary

Before launching Worker B, assert:
- Worker A process exited;
- Worker A helper disposed only as allowed;
- no Worker A signal operation outstanding;
- probe process exited if used;
- Streamlit state matches binding authority;
- handoff intermediate state captured;
- no final cleanup has run yet.

Do not delete harness-owned runtime directory before Worker B.

# Phase 5 — Worker B launch

Launch Worker B using the exact same frozen helper semantics as passing D.

Use same:
- process-group model;
- test-liveness argument if required;
- isolated runtime/database;
- handoff path;
- environment.

Do not vary launch mechanics.

# Phase 6 — Worker B readiness

Require the exact governed Worker B readiness point.

If Worker B uses P1/P2 under restart acceptance:
- P1 observed;
- P2 genuinely newer in its own process/session context;
- post-P2 hold stable.

Do not signal merely because process is alive.

# Phase 7 — Align Worker B shutdown with passing D

For Worker B cancellation, use this exact order:

1. all required observations complete;
2. probe complete if present;
3. Streamlit remains in the same stable state as D;
4. send targeted CTRL_BREAK;
5. require API success;
6. wait Worker B exit;
7. capture exit code;
8. assert exit code 0;
9. only then begin final cleanup.

This order is binding.

# Phase 8 — Cleanup-after-exit rule

Final cleanup must not begin until Worker B exit has been observed and helper diagnostics captured.

After Worker B exit:
- dispose Worker B helper/process resources;
- shut down Streamlit per governed order;
- clean handoff/runtime/database artifacts;
- assert residue.

Do not overlap final artifact cleanup with Worker B shutdown.

# Phase 9 — Streamlit ordering

If D proves Streamlit-alive is safe:
- keep Streamlit alive through Worker B exit;
- shut it down afterward.

Do not terminate Streamlit immediately before CTRL_BREAK unless the binding lifecycle contract explicitly requires it.

# Phase 10 — Probe ordering

If the restart path invokes the governed probe for Worker B:
- require probe exit before CTRL_BREAK;
- capture its exit;
- do not keep probe alive during signal.

If restart does not require probe B, do not add it.

# Phase 11 — Handoff ordering

Apply exact governed intermediate/final states.

Do not remove canonical handoff or runtime directory before Worker B has finished using them.
Use Worker B startup to prove prior-session cleanup where required.
Harness cleanup comes after lifecycle proof.

# Phase 12 — Database ordering

Do not delete/cleanup harness DB or sidecars while Worker B is alive.

Final DB cleanup:
- only after Worker B exit;
- only after Streamlit shutdown if Streamlit could hold any relevant read handle;
- use exact residue rule.

No persistence change.

# Phase 13 — No-sleep rule

Do not use `Thread.Sleep` or arbitrary delay.

All sequencing must wait on existing facts:
- process exit;
- P1/P2;
- probe exit;
- Streamlit readiness;
- signal result;
- listener release.

If an observable is missing and sleep seems necessary, STOP and report it.

# Phase 14 — Focused fix validation

After the minimal test-only sequencing fix, run:

1. failing restart lifecycle test alone;
2. require Worker B exit `0`;
3. run it **3 consecutive times** if timing governance allows;
4. original standalone CTRL_BREAK test;
5. A/B/C/D matrix;
6. focused WP08 suite.

Required:
- restart-specific test passes;
- standalone remains exit 0;
- A/B/C/D remain 4/4;
- no new failures;
- no owned process/listener residue.

If 3 repeats are too expensive, run the maximum deterministic count permitted and report it.

# Phase 15 — Build and regression

Run:
- build;
- Infrastructure suite;
- full .NET regression.

Accepted predecessor before fix:
- full 313/313 plus current diagnostic-test delta already present in test file.

Report exact final totals.

Also run Python predecessor smoke if the focused lifecycle suite invokes probe:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2.

No Python changes.

# Phase 16 — Residue

After fixed restart scenario:
- Worker A absent;
- Worker B absent;
- Streamlit absent after governed shutdown;
- probe absent;
- listener absent;
- handoff final state correct;
- temp handoff residue correct;
- DB/sidecars correct;
- no helper task/pipe/handle residue.

No global process kill.

# Phase 17 — Scope audit

Changed path must be exactly:

`WP08LifecycleDemonstrationTests.cs`

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

# GitHub lifecycle boundary

This authority does **not** close #233.

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open.

GitHub mutations:
`ZERO`

After the restart fix is proven, final WP08 lifecycle completion requires a fresh authority.

# Required completion report

## Classified root cause
Confirm Class H and exact failing sequence.

## Passing-D vs failing-restart delta
Exact ordering difference.

## Fix
Exact moved/reordered statements and why minimal.

## Worker B proof
Readiness → CTRL_BREAK → exit 0.

## Stability
Repeat count/results.

## Preservation
Standalone CTRL_BREAK and A/B/C/D results.

## Regression
Focused/Infrastructure/full/Python counts.

## Residue
Final matrix.

## Scope
Only lifecycle test file changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 RESTART/CLEANUP SEQUENCING FIX GITHUB MUTATIONS: ZERO`

## Next step

On success:

`WP08 RESTART/CLEANUP SEQUENCING FIXED — FINAL WP08 LIFECYCLE COMPLETION REQUIRES FRESH AUTHORITY`

# Stop conditions

Stop if:
- fixing restart requires helper modification;
- fixing restart requires Worker/production modification;
- no existing observable can sequence safely without sleeps;
- the failure persists after making Worker B shutdown match passing D;
- classification evidence shifts away from Class H;
- residue cannot be cleaned safely within test-only scope.

Do not broaden scope.

# Terminal markers

Success:

`RELEASE 1.9 WP08 RESTART/CLEANUP SEQUENCING FIX COMPLETE`

Blocked:

`RELEASE 1.9 WP08 RESTART/CLEANUP SEQUENCING FIX BLOCKED`

Do not emit COMPLETE unless Worker B exits 0 under the fixed restart path, standalone CTRL_BREAK remains valid, A/B/C/D remain passing, and no helper/production/Python semantic change occurs.

# Release 1.9 — WP08 Reused-Runtime Restart Fix Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow restart-specific fix authority** for Release 1.9 WP08, canonical issue **#233**.

The restart-specific diagnostic has authoritatively classified the remaining failure as:

`R-RUNTIME`

Evidence:

- R0 fresh single Worker → exit `0`
- R1 Worker A → Worker B with reused runtime/handoff/database → Worker B exit `0xC0000142`
- R2 reused runtime + Streamlit → Worker B exit `0xC0000142`
- R3 reused runtime + probe → Worker B exit `0`
- R4 reused runtime + Streamlit + probe → Worker B exit `0`
- R1F Worker A → Worker B with **fresh runtime/handoff/database** → Worker B exit `0`

Therefore:
- merely being the second Worker is **not** sufficient;
- failure requires some aspect of reused runtime/handoff/database state.

This authority may isolate the exact reused artifact and apply only the minimum contract-consistent restart cleanup/reset required to make shared-runtime restart reliable.

WP09 / #234 must remain Open / Backlog and unstarted.

---

# Accepted predecessor state

Preserve as binding:

## Smart App Control/local signing
- resolved and documented;
- read-only under this authority.

## Frozen Windows helper
`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted:
- native diagnostic capture;
- synchronous drains;
- targeted CTRL_BREAK;
- standalone exit 0.

Read-only.

## Worker production
Read-only unless a proven implementation defect is found in an already-authorized restart cleanup path and this authority explicitly reaches the production-fix gate below:

- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

## Existing diagnostics
`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Accepted:
- A/B/C/D matrix;
- R0–R4;
- R1F;
- standalone CTRL_BREAK exit 0;
- build 0 warnings / 0 errors.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Primary objective

Determine exactly which reused runtime artifact/state causes Worker B `0xC0000142`, then fix only that cause while preserving the required **shared-runtime restart** semantics.

Do **not** “fix” WP08 by switching the canonical restart acceptance test to a fresh runtime.

Shared-runtime restart remains required.

---

# Initial mutation scope

Initially modify only:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Use it to isolate the responsible reused artifact.

Do not modify production/helper/Python until the diagnostic split proves a production cleanup defect.

---

# Phase 0 — Read-only contract extraction

Read the binding WP08 lifecycle/residue contracts and extract exact restart requirements for:

- canonical handoff final/intermediate state;
- Worker startup cleanup;
- temp handoff sibling cleanup;
- database reuse;
- DB/WAL/SHM/journal rules;
- runtime directory reuse;
- Streamlit lifetime;
- restart timing;
- revision/session semantics.

The fix must conform exactly.

If the contract already mandates a specific cleanup that current implementation/harness is not performing, note it.

---

# Phase 1 — Compare failing R1 vs passing R1F

List every difference between:

## R1
- Worker A exits/disposes;
- Worker B reuses:
  - runtime directory;
  - canonical handoff path;
  - database path;
  - any temp sidecars/artifacts.

## R1F
- Worker A exits/disposes;
- Worker B uses fresh:
  - runtime directory;
  - handoff path;
  - database path.

Identify the minimal set of state variables that differ.

---

# Phase 2 — One-factor reuse matrix

Add only the minimum one-factor tests needed to isolate the culprit.

Preferred scenarios:

## RF-HANDOFF
Fresh runtime + fresh DB, but reuse Worker A canonical handoff path/state exactly as allowed.

Purpose:
- isolate handoff/canonical/temp-file reuse.

## RF-DB
Fresh runtime + fresh handoff, but reuse Worker A database path/state exactly as allowed.

Purpose:
- isolate DB/sidecars.

## RF-RUNTIME
Reuse Worker A runtime directory container, but use fresh handoff filename/path and fresh DB inside/alongside it if contract allows.

Purpose:
- isolate non-handoff/non-DB directory residue.

If path structure makes exact orthogonalization impossible, use the narrowest equivalent one-factor decomposition.

Do not add broad combinatorial scenarios unless needed.

---

# Phase 3 — Handoff-specific diagnostics

For handoff reuse scenarios, record before Worker B launch:

- canonical handoff exists yes/no;
- file size;
- revision/state/snapshot identity if already governed;
- last-write timestamp only as diagnostic metadata;
- temp sibling count and exact governed pattern;
- any `.tmp`/atomic-publisher residue matching accepted naming rules;
- whether Worker B startup removes/replaces canonical handoff as contract requires.

Do not parse with a duplicate parser.

Use existing handoff/parsing helpers where available.

---

# Phase 4 — Database-specific diagnostics

For DB reuse scenarios, record before Worker B launch:

- DB exists;
- size;
- `-wal` exists;
- `-shm` exists;
- journal/other governed sidecars exist;
- whether any process still owns DB-related handles if existing APIs can prove it;
- whether Worker B startup reuses the same DB.

Do not query/alter schema merely for diagnosis.

Do not delete sidecars unless the binding contract explicitly requires that cleanup.

---

# Phase 5 — Runtime-directory diagnostics

Record exact runtime-directory contents after Worker A exit and before Worker B launch.

Limit to harness-owned runtime tree.

Capture:
- relative paths;
- file types;
- sizes;
- which entries are governed handoff/DB artifacts;
- any unexpected residue.

No unrelated filesystem scan.

---

# Phase 6 — Classification

Classify exact culprit family:

## RR-HANDOFF
Failure follows canonical/temp handoff reuse.

## RR-DB
Failure follows DB/sidecar reuse.

## RR-RUNTIME
Failure follows other runtime-directory residue.

## RR-COMBINED
No single factor fails; only a particular combination does.

## RR-UNRESOLVED
One-factor matrix insufficient.

Do not fix until classification is sufficiently specific.

---

# Phase 7 — Contract check before fix

Once culprit is classified, compare it against the binding restart/residue contract.

Determine whether the problem is:

## Test/Harness cleanup omission
The contract already expects cleanup/reset, but the lifecycle test failed to perform/allow it.

## Existing production startup-cleanup defect
The Worker is contractually responsible for startup cleanup but does not perform it correctly.

## Invalid acceptance assumption
The test is preserving state that the contract says should not survive into Worker B.

Only the first two may be fixed under this authority.

If the contract itself is ambiguous, STOP and request semantic amendment.

---

# Phase 8 — Test-only fix path

If the culprit is a harness-owned cleanup omission:

Authorized mutation remains only:

`WP08LifecycleDemonstrationTests.cs`

Examples:
- wait until exact process exit before observing/cleaning;
- remove a harness-created diagnostic artifact the contract says is test-owned;
- ensure final-vs-restart cleanup boundaries match authority;
- preserve canonical artifacts the Worker, not harness, owns.

Do not delete production-owned handoff/DB state simply to make B pass unless the contract says the harness owns that cleanup.

---

# Phase 9 — Production startup-cleanup fix gate

Only if evidence proves:

1. the binding WP08/WP05 contract assigns cleanup to Worker startup;
2. the current production implementation fails that exact responsibility;
3. the failure is reproduced by the isolated reused artifact;

then this authority permits the **minimum production fix only in the exact already-governed cleanup path**.

Expected possible paths, only if proven relevant:

- `src/AIQuantTradingResearch.Worker/Program.cs`
- exact existing WP05 handoff cleanup/publisher path if already binding-authorized for startup cleanup.

Do not assume these paths are authorized; verify against accepted path amendments.

If a required production path is not already covered by accepted WP08/WP05 path authority, STOP and request a narrow path amendment.

No schema/persistence behavior change.

---

# Phase 10 — Handoff fix constraints

If classified `RR-HANDOFF`:

Permitted fix must preserve:
- atomic publisher ownership;
- prior-session cleanup semantics;
- no Streamlit deletion;
- no fabricated revision;
- no change to WP05 parser/cache semantics.

Potential valid fix shape:
- ensure Worker B startup deletes the prior canonical handoff before new-session publication if the binding contract requires that.

Do not simply choose a new handoff filename for Worker B.

Shared path reuse is the acceptance requirement.

---

# Phase 11 — DB fix constraints

If classified `RR-DB`:

Do not delete/recreate DB unless the lifecycle contract explicitly says DB is temporary and restart-cleaned.

First determine whether the failure is:
- lingering sidecar/handle timing;
- test-owned temporary DB cleanup omission;
- production DB connection lifetime defect.

No persistence semantic/schema change.

If fixing requires persistence implementation changes outside accepted WP08 scope, STOP.

---

# Phase 12 — Runtime residue fix constraints

If classified `RR-RUNTIME`:

Remove/reset only the exact harness-owned residue proven causal and contractually disposable.

Do not wipe the entire runtime directory blindly.

Preserve governed shared artifacts.

---

# Phase 13 — No-fresh-runtime escape hatch

The canonical restart acceptance must still use:

- same governed runtime boundary;
- same handoff path where required;
- same DB path where required by contract.

Fresh-runtime R1F remains diagnostic control only.

Do not change final acceptance to R1F semantics.

---

# Phase 14 — Focused post-fix validation

After minimal fix:

Run the isolated culprit scenario first.

Then require:

1. original failing R1 now exits `0`;
2. R1 repeated 3 times if timing budget permits;
3. R1F still exits `0`;
4. R0–R4 preserved;
5. A/B/C/D preserved;
6. standalone CTRL_BREAK exit 0;
7. no process/listener residue;
8. build 0 warnings / 0 errors.

If the fix changes production cleanup, also run all focused WP05 transport/lifecycle tests affected by that path.

---

# Phase 15 — Full WP08 focused validation

Run the complete focused WP08 lifecycle suite.

Expected:
- prior restart failure gone;
- all diagnostic tests remain factual/passing;
- exact final count reported.

No tests may be deleted merely to reduce count.

---

# Phase 16 — Regression

Run:

## .NET
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Use current accepted baseline plus diagnostic-test delta.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- Streamlit 1.61.1;
- `pip check`.

No Python mutation.

---

# Phase 17 — Residue

After shared-runtime restart:

- Worker A absent;
- Worker B absent;
- Streamlit absent after governed shutdown;
- probe absent;
- listener absent;
- canonical handoff final state correct;
- temp handoff residue correct;
- DB/sidecars correct;
- runtime directory contains only contract-allowed artifacts.

---

# Phase 18 — Scope audit

List every changed path and classify:

- diagnostic;
- test-only fix;
- production startup-cleanup fix, if authorized/proven.

Prove zero:
- helper redesign;
- cancellation/signal redesign;
- Replay;
- WP06/WP07 semantics;
- package;
- signing;
- GitHub;
- WP09.

---

# GitHub lifecycle boundary

This authority does **not** close #233.

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open.

GitHub mutations:
`ZERO`

Final WP08 lifecycle completion requires a fresh authority after this fix is proven.

---

# Required completion report

## Accepted classification
Confirm `R-RUNTIME`.

## One-factor reuse matrix
RF-HANDOFF / RF-DB / RF-RUNTIME results.

## Exact culprit
RR-HANDOFF / RR-DB / RR-RUNTIME / RR-COMBINED / RR-UNRESOLVED.

## Contract ownership
Who owns cleanup/reset and why.

## Fix
Exact path/symbol/ordering change.

## Shared-runtime proof
R1 now exits 0 without switching to fresh runtime.

## Stability
Repeat results.

## Preservation
R1F, R0–R4, A/B/C/D, standalone CTRL_BREAK.

## Regression
Focused/.NET/Python.

## Residue
Exact final matrix.

## Scope
Authorized paths only.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 REUSED-RUNTIME RESTART FIX GITHUB MUTATIONS: ZERO`

## Next step

On success:

`WP08 REUSED-RUNTIME RESTART FIXED — FINAL WP08 LIFECYCLE COMPLETION REQUIRES FRESH AUTHORITY`

---

# Stop conditions

Stop if:
- one-factor reuse cannot be isolated in the existing lifecycle test;
- culprit remains RR-UNRESOLVED;
- contract ownership is ambiguous;
- required production path lacks accepted path authority;
- fix requires schema/persistence semantic change;
- helper/cancellation mechanism must change;
- shared-runtime acceptance cannot pass without switching to fresh state.

Do not broaden scope.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 REUSED-RUNTIME RESTART FIX COMPLETE`

Blocked:

`RELEASE 1.9 WP08 REUSED-RUNTIME RESTART FIX BLOCKED`

Do not emit COMPLETE unless the exact reused-state culprit is proven, the minimal contract-consistent fix is applied, and the shared-runtime R1 restart exits Worker B with code 0.

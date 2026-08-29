# Release 1.10 WP02 — Stage-Boundary Reconciliation Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY for observability truthfulness, architecture/contract reconciliation, stage ownership, path authority, acceptance, and governance.
- **GPT-5.6 Terra** — RESERVED for resumed WP02 implementation/validation after this reconciliation.
- **GPT-5.6 Sol** — supporting analysis, alternatives, synthesis, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Target work package:

**WP02 — Application Pipeline Observability Contract**

Canonical issue:

**#243**

This is a narrow planning/contract reconciliation authority caused by a truthful-observability blocker discovered during the resumed Terra WP02 execution.

It is NOT an implementation authority.

---

# Confirmed blocker

Baseline passed before the block:

- Application build: 0 warnings / 0 errors
- Application tests: 125/125 passed
- issue #243 remained Open
- no WP02 implementation mutation
- Git mutations: zero
- GitHub mutations: zero.

The current frozen WP02 contract requires truthful child activities for five fixed pipeline stages.

However, `PipelineExecutionUseCase.Execute(...)` invokes one opaque:

`IMaterializeDatasetUseCase.Execute(...)`

call that encompasses both accepted conceptual stages:

1. `HistoricalObservationRetrieval`
2. `DatasetMaterialization`

At the currently authorized `PipelineExecutionUseCase` boundary, those two durations are not independently observable.

Creating two activities around the same opaque call would produce duplicated/non-truthful duration evidence and violates WP01 observability semantics.

This authority must resolve that exact contradiction.

---

# Canonical inputs

Read in full:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #243
6. `PipelineExecutionUseCase` implementation
7. `IMaterializeDatasetUseCase`
8. concrete implementation(s) of `IMaterializeDatasetUseCase`
9. related Application interfaces/use cases invoked by that implementation
10. relevant focused tests.

Use repository evidence to determine where the two semantic stages are actually separable.

Do not assume the concrete materialization implementation is the correct owner until inspected.

---

# Allowed decisions

This authority MUST choose one truthful outcome.

## Outcome A — Authorize truthful separate stage boundaries

Choose this only if repository evidence shows exact Application symbols where:

- historical observation retrieval has a real start/end boundary; and
- dataset materialization has a separate real start/end boundary.

If selected, freeze:

- exact additional Application file path(s);
- exact symbol/member(s);
- exact permitted instrumentation shape;
- exact test path implications;
- exact parent/child topology;
- which WP owns each activity/metric.

The new authority must remain minimal.

Do not authorize broad files or unrelated refactoring.

## Outcome B — Reconcile to one combined WP02 boundary

Choose this if the two conceptual stages are not truthfully separable within WP02's proper ownership without architecture distortion.

Freeze one truthful combined Application stage boundary for the opaque materialization operation.

Then explicitly assign finer-grained:

- historical retrieval timing; and/or
- materialization timing

to the later WP that owns the actual observable boundary.

Update the canonical vocabulary/scope mapping only as much as necessary to remove the false five-stage requirement.

Do not weaken observability truthfulness merely to retain the original count.

---

# Decision principle

**Truthful evidence outranks a preselected span count.**

An activity duration must correspond to an actual observable execution interval.

Forbidden:

- two sibling/child spans wrapping the same opaque call and claiming different stage meanings;
- invented duration partitioning;
- inferred timings not measured at their actual boundary;
- renaming one interval into two semantic operations;
- adding architecture bypass solely to satisfy telemetry shape;
- moving business ownership for instrumentation convenience.

Required marker:

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS PRINCIPLE: ENFORCED`

---

# Mutation boundary

## Repository

Planning/contract documentation only.

Authorized candidates:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` only if the canonical scope/vocabulary must be reconciled.

Do not create another source of truth unless explicitly necessary and already permitted.

## Forbidden

No mutation to:

- Application source;
- tests;
- project/package files;
- schema;
- Python;
- Streamlit;
- Worker;
- runtime/config;
- CI/workflows.

## Git

ZERO mutations.

## GitHub

ZERO mutations.

Do not edit #243, milestone #59, or Project #2.

---

# Phase 0 — Entry audit

Record:

- branch;
- local HEAD;
- remote `main` if available without mutation;
- `git status --short`;
- all pre-existing planning/contract artifacts;
- absence of WP02 implementation mutations;
- issue #243 state if available.

Preserve all residue.

Emit:

`RELEASE 1.10 WP02 STAGE RECONCILIATION ENTRY BASELINE: READ-ONLY`

---

# Phase 1 — Reproduce the semantic contradiction

Read the current frozen WP02 stage contract and current code.

Document:

- five required conceptual stages;
- exact method/call currently assigned to each;
- actual observable start/end boundaries;
- why the first two cannot currently be timed separately at `PipelineExecutionUseCase.Execute(...)`.

Prove that wrapping the same `IMaterializeDatasetUseCase.Execute(...)` call twice would be non-truthful.

Emit:

`RELEASE 1.10 WP02 STAGE-BOUNDARY CONTRADICTION: CONFIRMED`

If repository evidence disproves the reported blocker, explain precisely and BLOCK rather than silently changing contract.

---

# Phase 2 — Trace the materialization call graph

Trace:

`PipelineExecutionUseCase.Execute(...)`
→ `IMaterializeDatasetUseCase.Execute(...)`
→ concrete implementation
→ downstream Application/Infrastructure abstractions.

For every relevant call record:

- path;
- symbol;
- architectural layer;
- semantic responsibility;
- whether historical retrieval begins/ends there;
- whether dataset materialization begins/ends there;
- whether instrumentation there would remain Application-owned;
- whether WP03 owns the deeper infrastructure operation instead.

Do not mutate code.

---

# Phase 3 — Determine true ownership

For `HistoricalObservationRetrieval`, identify:

- real observable operation;
- layer that owns the semantic boundary;
- exact symbol;
- whether it is Application orchestration or Infrastructure/provider execution;
- correct WP owner.

For `DatasetMaterialization`, do the same.

Classify each:

- `WP02 — APPLICATION OWNED`
- `WP03 — INFRASTRUCTURE OWNED`
- `COMBINED AT WP02 / DECOMPOSED LATER`
- `NOT TRUTHFULLY OBSERVABLE UNDER ACCEPTED ARCHITECTURE`

No stage may have two primary timing owners.

Emit:

`RELEASE 1.10 WP02 STAGE OWNERSHIP ANALYSIS: COMPLETE`

---

# Phase 4 — Compare Outcome A vs Outcome B

Evaluate both explicitly.

## Outcome A evaluation

Determine the smallest exact additional Application path/symbol authority needed.

Assess:

- truthfulness;
- dependency direction;
- business ownership;
- path expansion size;
- test impact;
- WP03 overlap;
- whether instrumentation is additive;
- whether it causes unnecessary coupling/refactoring.

## Outcome B evaluation

Determine the exact combined boundary.

Assess:

- truthful name;
- start/end;
- metric semantics;
- downstream decomposition;
- WP03 ownership;
- effect on accepted five-stage wording;
- effect on tests.

Choose the outcome that preserves architecture and truthful evidence with the smallest justified authority expansion.

---

# Phase 5A — If Outcome A is selected

Freeze exact additional writable authority.

For every added path:

- exact path;
- exact symbol/member;
- `MODIFY` or `ADD`;
- exact permitted telemetry change;
- exact forbidden changes.

Freeze exact stage topology:

- parent activity;
- `HistoricalObservationRetrieval` activity owner and symbol;
- `DatasetMaterialization` activity owner and symbol;
- start/end semantics;
- success/failure semantics;
- metric recording;
- allowed attributes.

Determine whether existing:

`tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`

is sufficient.

If another test path is genuinely required, name it exactly and justify it.

Emit:

`RELEASE 1.10 WP02 STAGE RECONCILIATION OUTCOME: A — SEPARATE TRUTHFUL BOUNDARIES`

---

# Phase 5B — If Outcome B is selected

Freeze the exact combined WP02 boundary.

Define:

- canonical combined stage name;
- exact owner/symbol;
- start/end;
- success/failure;
- metric semantics;
- allowed attributes.

Explicitly remove/reconcile any requirement that WP02 emit separate timing evidence for both conceptual stages.

Assign each finer-grained observation to the correct later owner, most likely WP03 where provider/infrastructure timing is actually observable, but only if repository evidence supports that.

Specify whether the later owner may emit child activities and their canonical names.

Update test expectations accordingly.

Emit:

`RELEASE 1.10 WP02 STAGE RECONCILIATION OUTCOME: B — COMBINED TRUTHFUL BOUNDARY`

---

# Phase 6 — Preserve WP01 semantics

Verify the selected outcome preserves:

- canonical vocabulary;
- no fabricated duration;
- no duplicate semantic spans;
- bounded attributes/cardinality;
- telemetry security;
- no false live-provider claim;
- no telemetry as business state;
- Worker/Streamlit independence.

If `OPEN_TELEMETRY_SELECTION.md` currently mandates the contradictory five-stage topology, reconcile it narrowly.

Emit:

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS PRINCIPLE: ENFORCED`

---

# Phase 7 — Reconcile file manifest

Update `RELEASE_1.10_FILE_MANIFEST.md` so the resumed Terra authority has a hard deterministic path boundary.

If Outcome A:
- add only exact additional paths/symbols;
- retain prior frozen paths;
- name exact tests.

If Outcome B:
- retain or narrow existing paths;
- remove any implied need for unauthorized materialization internals;
- update test contract to combined semantics.

No ambiguous phrases such as:

- “later authority may name”
- “relevant materialization files”
- “as needed”
- “related tests”

are permitted.

Emit:

`RELEASE 1.10 WP02 STAGE PATH OWNERSHIP: FROZEN`

---

# Phase 8 — Reconcile execution plan

Update `RELEASE_1.10_EXECUTION_PLAN.md` with:

- selected Outcome A/B;
- exact stage topology;
- exact ownership;
- exact metric/activity semantics;
- WP03 handoff;
- acceptance/test expectations.

Preserve BCL-only WP02 API unless repository evidence establishes that this stage reconciliation itself requires a different decision. If it would require a package change:
BLOCK and escalate rather than silently changing it.

---

# Phase 9 — WP03 handoff

Freeze the downstream contract.

For every stage relevant to WP03 specify:

- parent activity source/context;
- whether WP03 creates a child activity;
- exact semantic responsibility;
- whether timing is Infrastructure-owned;
- failure propagation;
- whether WP02 must remain unchanged.

WP03 must not duplicate a WP02 timing interval under a different name.

Emit:

`RELEASE 1.10 WP02 STAGE → WP03 HANDOFF: PASS`

---

# Phase 10 — Terra simulation

Simulate the resumed WP02 Terra authority.

Terra must be able to answer without invention:

1. How many WP02 stage activities are required?
2. What are their exact names?
3. Which exact symbol owns each?
4. What exact start/end interval does each measure?
5. Which paths may Terra modify?
6. Which test path(s) may Terra modify/add?
7. Which stage timings are deferred to WP03?
8. How does WP03 parent/nest its activities?
9. Are any two activities timing the same opaque call under different meanings?
10. Is BCL-only still enforced?

Required answers must be deterministic.

Question 9 must be **NO**.

Emit:

`RELEASE 1.10 WP02 STAGE MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 11 — Acceptance

PASS only if:

- the semantic contradiction is resolved;
- one exact Outcome A/B is selected;
- every stage has one truthful timing owner;
- exact paths/symbols are frozen;
- tests are deterministic;
- WP03 handoff is deterministic;
- no fabricated/duplicated duration remains;
- BCL-only contract remains valid;
- only planning/contract docs changed;
- Git/GitHub mutations are zero.

Emit:

`RELEASE 1.10 WP02 STAGE-BOUNDARY RECONCILIATION: PASS`

---

# Phase 12 — Mutation accounting

Report exact changed planning/contract paths.

Required:

`RELEASE 1.10 WP02 STAGE RECONCILIATION REPOSITORY MUTATIONS: PLANNING/CONTRACT PATHS ONLY`

`RELEASE 1.10 WP02 STAGE RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 STAGE RECONCILIATION GITHUB MUTATIONS: ZERO`

---

# Phase 13 — Next authority

On PASS, regenerate/resume:

**Release 1.10 WP02 — Application Pipeline Observability Contract Authority — GPT-5.6 Terra**

The Terra authority must reread the reconciled artifacts and use the newly frozen stage topology/path allowlist.

Do not execute WP02 here.

---

# Required final report

Report:

1. model assignment;
2. entry baseline;
3. contradiction evidence;
4. materialization call graph;
5. true stage ownership;
6. Outcome A/B comparison;
7. selected outcome;
8. exact stage topology;
9. exact path/symbol authority;
10. exact tests;
11. WP01 truthfulness/security preservation;
12. WP03 handoff;
13. changed planning paths;
14. Terra simulation;
15. acceptance;
16. mutation accounting;
17. exact next authority.

---

# Success markers

`RELEASE 1.10 WP02 STAGE-BOUNDARY CONTRADICTION: CONFIRMED`

`RELEASE 1.10 WP02 STAGE OWNERSHIP ANALYSIS: COMPLETE`

One of:

`RELEASE 1.10 WP02 STAGE RECONCILIATION OUTCOME: A — SEPARATE TRUTHFUL BOUNDARIES`

or

`RELEASE 1.10 WP02 STAGE RECONCILIATION OUTCOME: B — COMBINED TRUTHFUL BOUNDARY`

Then:

`RELEASE 1.10 WP02 STAGE TRUTHFULNESS PRINCIPLE: ENFORCED`

`RELEASE 1.10 WP02 STAGE PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP02 STAGE → WP03 HANDOFF: PASS`

`RELEASE 1.10 WP02 STAGE MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP02 STAGE-BOUNDARY RECONCILIATION: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Terminal:

`RELEASE 1.10 WP02 — STAGE-BOUNDARY RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- repository evidence cannot identify truthful ownership;
- Outcome A requires architecture distortion or broad path expansion;
- Outcome B cannot be reconciled with accepted Release 1.10 scope;
- two stages would still time the same opaque interval;
- WP03 ownership cannot be determined;
- BCL-only contract would need unauthorized revision;
- any production/test/package/Git/GitHub mutation occurs.

Report the exact unresolved semantic/ownership decision.

Terminal:

`RELEASE 1.10 WP02 — STAGE-BOUNDARY RECONCILIATION AUTHORITY BLOCKED`

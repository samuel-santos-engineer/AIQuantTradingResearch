# Release 1.9 — WP03 Consolidated Worker Implementation/Completion — Codex Authority

## Authority

This document grants **fresh consolidated execution authority** to complete:

**Release 1.9 WP03 — canonical GitHub issue #228**

This authority supersedes the prior blocked WP03 Worker implementation/completion authority for execution purposes.

It incorporates two now-fixed normative contracts:

1. the **Worker replay mode/configuration contract**;
2. the **Replay Dataset-boundary contract**.

These contracts are authoritative inputs and must not be redesigned.

This authority also preserves the already-validated WP03 implementation state.

---

# Accepted Current State

Proven current WP03 state:

- WP01 #226: Closed / Done;
- WP02 #227: Closed / Done;
- WP03 #228: Open / Backlog;
- WP04 #229 and later WPs remain unstarted;
- one private canonical five-stage `ExecuteCanonical` executor is implemented;
- historical and explicit-observation materialization converge on that same executor;
- historical observation acquisition remains through `IHistoricalObservationStore`;
- replay does not masquerade as historical storage;
- additive observation-input seam is implemented;
- Application focused tests: **122/122 passed**;
- WP02 replay suite: **142/142 passed**;
- full regression: **288/288 passed**;
- build: **0 errors / 0 warnings**;
- no WP03 GitHub lifecycle mutation has occurred;
- no package/Python/schema/planning mutation has occurred;
- pre-existing Release 1.9 authority/control files remain preserved.

Treat **288/288** as the immediate predecessor full-suite baseline.

Preserve the validated executor/seam work.

Do not roll it back or redesign it for preference.

---

# Fixed Normative Contract A — Worker Replay Configuration

## Configuration section

Canonical section:

`Worker`

Canonical keys:

- `Worker:Mode`
- `Worker:Replay:ReplayIdentity`
- `Worker:Replay:Target`
- `Worker:Replay:StartingTick`
- `Worker:Replay:RequestedObservationCount`

Environment-variable mapping uses standard .NET configuration:

- `Worker__Mode`
- `Worker__Replay__ReplayIdentity`
- `Worker__Replay__Target`
- `Worker__Replay__StartingTick`
- `Worker__Replay__RequestedObservationCount`

## Mode semantics

Key:

`Worker:Mode`

Accepted values:

- `Historical`
- `Replay`

Comparison:

- case-insensitive ordinal

Missing mode:

- defaults to `Historical`

Unknown explicit value:

- fail fast before execution

Historical mode:

- existing historical path;
- acquisition through `IHistoricalObservationStore`.

Replay mode:

- nested replay settings required and validated.

## Replay settings

### `Worker:Replay:ReplayIdentity`

- string;
- required in Replay mode;
- no default;
- non-empty;
- bounded according to existing WP02 conventions;
- maps to `ReplayRequest.ReplayIdentity` and corresponding replay configuration identity.

### `Worker:Replay:Target`

- string;
- required in Replay mode;
- no default;
- non-empty;
- maps to `ReplayRequest.Target` and corresponding replay configuration target.

### `Worker:Replay:StartingTick`

- non-negative integer;
- required in Replay mode;
- no default;
- maps to `ReplayRequest.StartingTick`.

### `Worker:Replay:RequestedObservationCount`

- positive integer;
- required in Replay mode;
- no default;
- maps to `ReplayRequest.RequestedObservationCount`;
- upper/bounds behavior remains WP02-owned.

## Binding/validation

Bind the `Worker` section into narrow Worker-owned configuration types.

Validate during startup/configuration construction before execution dispatch.

Required behavior:

- missing/malformed numeric values fail fast under existing .NET/repository conventions;
- Replay mode with missing replay section fails;
- Replay mode with missing/invalid required fields fails;
- Historical mode does not require replay settings;
- replay-only settings outside Replay mode are ignored for compatibility;
- replay-only settings must not alter historical execution;
- Replay mode must never silently fall back to Historical.

## Lifecycle ownership

Restart/resume:

- configured only through `StartingTick`.

Cancellation:

- no Worker config field;
- runtime cancellation token propagates to WP02.

Finite completion:

- WP02-owned;
- successful terminal replay result is normal finite completion, not pipeline failure.

Not configurable at Worker level:

- logical tick progression;
- duplicate determinism;
- fixture length;
- replay identity determinism;
- end-of-replay semantics beyond bounded input selection.

Do not add additional Worker replay fields.

---

# Fixed Normative Contract B — Replay Dataset Boundary

Replay mode requires the existing `Dataset` section unchanged.

Required existing keys:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`

Do not change `DatasetDefinition`.

## Target semantics

Both are required:

- `Worker:Replay:Target`
- `Dataset:Target`

Ownership:

- `Worker:Replay:Target` is authoritative for WP02 replay acquisition;
- `Dataset:Target` is authoritative for pipeline dataset identity/context.

Consistency:

- they must match exactly;
- comparison is ordinal and case-sensitive;
- no whitespace normalization;
- mismatch fails before replay execution.

## From semantics

`Dataset:From` remains configured.

Meaning:

- inclusive lower timestamp bound of `[From, To)`.

Rules:

- required;
- parse using existing exact `DateTimeOffset` configuration convention;
- must be earlier than `Dataset:To`;
- replay observations must not precede it.

It is not derived from ticks or replay observations.

## To semantics

`Dataset:To` remains configured.

Meaning:

- exclusive upper timestamp bound of `[From, To)`.

Rules:

- required;
- parse using existing exact `DateTimeOffset` configuration convention;
- must be later than `Dataset:From`;
- replay observations must be strictly earlier than it.

It is not derived from ticks or replay observations.

## Replay slice relation

`StartingTick` and `RequestedObservationCount` select observations from the fixed WP02 fixture.

Rules:

- logical ticks do not become timestamps;
- Dataset From/To remain independent pipeline context;
- selected replay observations must all lie within `[Dataset:From, Dataset:To)`;
- Dataset bounds need not equal selected first/last observation timestamps;
- partially or wholly out-of-bounds replay selections fail before pipeline execution;
- end-of-replay remains WP02-owned;
- successful terminal result is not a Dataset-boundary failure.

## Replay validation failures

Replay fails deterministically when:

- Dataset section missing;
- Dataset Target missing;
- Dataset From missing;
- Dataset To missing;
- either timestamp malformed;
- `Dataset:From >= Dataset:To`;
- Dataset Target != Worker Replay Target;
- any selected replay observation is before From;
- any selected replay observation is at/after To;
- WP02 rejects identity/tick/count/bounds under its own semantics.

No replay observation may be persisted through `IHistoricalObservationStore`.

---

# Objective

Complete #228 using the fixed contracts above.

Implement the real production replay composition:

`Worker:Mode=Replay`
→ validated Worker replay settings
→ validated DatasetDefinition context
→ WP02 replay source/configuration
→ selected replay observations
→ Replay Dataset-boundary validation
→ WP03 additive observation-input seam
→ `ExecuteCanonical`
→ canonical stages 1–5
→ finite completion

Historical mode must remain:

`Worker Historical/default`
→ historical materialization
→ `IHistoricalObservationStore`
→ `ExecuteCanonical`
→ stages 1–5

Exactly one canonical five-stage executor must remain.

---

# Permitted Scope

May modify only the minimum WP03 implementation required to satisfy #228 and the fixed contracts.

Potentially permitted:

- Worker configuration/options types;
- Worker configuration binding;
- Worker validation;
- Dataset config reuse/validation for Replay mode;
- target consistency validation;
- replay-observation timestamp-boundary validation;
- Worker Historical/Replay dispatch;
- Worker composition root / DI;
- mapping to existing WP02 replay configuration/request;
- cancellation propagation;
- replay execution loop/composition;
- narrowly required compatibility changes;
- focused tests;
- runtime integration tests;
- explicitly required configuration examples/docs if #228 requires them.

Existing canonical executor/seam code may change only for a proven integration defect and only minimally.

---

# Explicitly Forbidden

Do not:

- alter either fixed normative contract;
- add Worker modes beyond Historical/Replay;
- add Worker replay config keys;
- remove/change Dataset keys;
- derive Dataset From/To from replay observations;
- derive Dataset Target from Worker Replay Target silently;
- normalize target strings;
- redesign `DatasetDefinition`;
- redesign WP02 replay contracts;
- create another executor;
- create a replay-specific pipeline;
- bypass the WP03 observation seam;
- route replay through `IHistoricalObservationStore`;
- persist replay observations as historical data;
- create generalized mode/ingestion frameworks;
- implement WP04+;
- add WP04 scaffolding;
- change packages/Python/Streamlit/schema/protocol;
- alter Release 1.9 planning/dependencies;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #228 before every technical gate passes.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before any mutation:

1. Read #228 completely.
2. Read the accepted WP03 manifest/definition.
3. Read current Worker entry point and composition root.
4. Read Worker configuration conventions.
5. Read Dataset configuration binding/parsing.
6. Read `DatasetDefinition`.
7. Read WP02 replay request/configuration/source/DI.
8. Read WP03 additive seam and `ExecuteCanonical`.
9. Read Worker/Application/WP02 replay tests.
10. Record current Git state and existing WP03 diff.
11. Prove no unrelated or uncertain changes exist.
12. Prove the validated canonical executor/seam state is intact.

Stop if repository reality materially conflicts with either fixed contract.

---

# Phase 1 — Worker Configuration Implementation

Implement narrow Worker-owned configuration types for:

- `Worker:Mode`;
- nested `Worker:Replay`.

Requirements:

- exact keys/shape from Contract A;
- no aliases;
- no extra fields;
- case-insensitive ordinal mode parsing;
- missing mode → Historical;
- unknown mode → fail fast.

Use existing .NET/repository options/binding conventions.

---

# Phase 2 — Replay Configuration Validation

When mode = Replay, validate before execution dispatch:

- Replay section present;
- ReplayIdentity non-empty;
- Target non-empty;
- StartingTick present and >= 0;
- RequestedObservationCount present and > 0.

When mode = Historical:

- replay section optional;
- replay-only values ignored for dispatch;
- replay-only values must not alter historical path.

Do not redefine WP02 upper-bound semantics.

---

# Phase 3 — Dataset Binding and Validation

Replay mode must bind/use the existing Dataset configuration unchanged.

Before replay execution prove:

- Dataset section exists;
- Target exists;
- From exists;
- To exists;
- From/To parse using existing exact `DateTimeOffset` convention;
- From < To;
- `Dataset:Target` equals `Worker:Replay:Target` with ordinal case-sensitive comparison.

Mismatch must fail before replay acquisition/execution proceeds.

Historical behavior remains unchanged.

---

# Phase 4 — Real Worker Dispatch

## Historical/default mode

Preserve existing production flow:

`Worker`
→ Historical/default
→ historical materialization
→ `IHistoricalObservationStore`
→ WP03 materialization
→ `ExecuteCanonical`
→ stages 1–5

No replay dependency/configuration is required for this path.

## Replay mode

Implement production flow:

1. Worker selects Replay;
2. Worker validates fixed Replay config;
3. Worker validates Dataset config and exact target equality;
4. Worker maps replay fields exactly to WP02;
5. Worker resolves/uses the real WP02 replay source;
6. Worker acquires/selects replay increment under WP02 semantics;
7. Worker validates every selected observation timestamp lies in `[Dataset:From, Dataset:To)`;
8. if any selected observation is outside bounds, fail before pipeline execution;
9. pass explicit replay observations and DatasetDefinition context through the WP03 seam;
10. invoke existing `ExecuteCanonical`;
11. execute all five canonical stages;
12. terminate normally on finite WP02 end-of-replay.

Do not persist replay observations via historical storage.

---

# Phase 5 — Cancellation and Replay Semantics

Preserve through real Worker composition:

## Replay identity
Exact Worker value maps to WP02.

## Target
Worker replay target maps to WP02; Dataset target remains pipeline context and must match exactly.

## Logical ticks
Remain WP02-owned; do not convert ticks to timestamps.

## Restart/resume
Only StartingTick selects replay position.

## Requested count
Maps directly to WP02 request count.

## Duplicates
Remain WP02 deterministic behavior.

## Cancellation
Runtime token propagates through replay acquisition/execution; no unauthorized continued work.

## Bounds
WP02 request bounds remain WP02-owned; Dataset timestamp bounds are an additional WP03 validation gate.

## End-of-replay
Successful terminal WP02 result ends Replay mode normally.

---

# Phase 6 — Focused Worker Configuration Tests

Required tests:

- explicit Historical;
- explicit Replay;
- missing mode defaults to Historical;
- unknown mode fails before execution;
- mode matching is case-insensitive;
- Replay missing replay section fails;
- each required replay field missing fails;
- empty replay identity fails;
- empty replay target fails;
- negative StartingTick fails;
- zero requested count fails;
- negative requested count fails if representable through binding;
- valid fields map exactly to WP02;
- Historical succeeds without replay settings;
- replay-only settings cannot switch Historical/default behavior to Replay.

---

# Phase 7 — Focused Dataset-Boundary Tests

Required tests:

- valid Dataset section + valid Replay succeeds;
- Dataset Target exact match succeeds;
- target mismatch fails before replay execution;
- target case difference fails;
- target whitespace difference fails;
- missing Dataset section fails in Replay mode;
- missing Target fails;
- missing From fails;
- missing To fails;
- malformed From fails;
- malformed To fails;
- From == To fails;
- From > To fails;
- replay slice fully inside `[From, To)` succeeds;
- replay observation before From fails before pipeline execution;
- replay observation exactly at To fails before pipeline execution;
- partially out-of-bound slice fails;
- logical ticks are not treated as timestamps;
- finite end-of-replay remains successful WP02 completion.

Do not invent timestamp derivation behavior.

---

# Phase 8 — Focused Production Composition Tests

Prove the real production wiring:

## Historical
- selects historical path;
- uses `IHistoricalObservationStore`;
- reaches shared `ExecuteCanonical`.

## Replay
- selects real WP02 replay source/config;
- maps all fixed Worker replay fields;
- validates Dataset target and interval;
- sends selected observations through WP03 seam;
- reaches shared `ExecuteCanonical`;
- executes all five canonical stages;
- never routes replay observations through historical storage.

Exercise production composition as directly as repository conventions permit.

Mocks may support testing but must not replace proof of actual wiring.

---

# Phase 9 — WP02 Semantic Regression

Rerun/prove:

- replay identity;
- ticks;
- restart/resume;
- duplicates;
- cancellation;
- WP02 bounds;
- finite/end-of-replay.

Required WP02 replay suite predecessor guard:

**142/142 passed**

Higher count acceptable only if explained by owned tests.

Do not weaken predecessor tests.

---

# Phase 10 — Application Regression Guard

Rerun the relevant Application-focused suite.

Predecessor guard:

**122/122 passed**

Higher count acceptable if explained.

---

# Phase 11 — Build and Full Regression

Run established repository build.

Require:

- 0 errors;
- report warning count exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Predecessor full-suite baseline:

**288/288 passed**

Capture exact:

- exit status;
- passed;
- failed;
- skipped;
- material warnings.

A higher count is expected from new tests.

An unexplained lower count is a blocker.

---

# Phase 12 — Production Flow Proof

Before technical acceptance, explicitly document actual code flow.

Historical:

`Worker`
→ Historical/default mode
→ existing Dataset config
→ historical materialization
→ `IHistoricalObservationStore`
→ WP03 materialization
→ `ExecuteCanonical`
→ stages 1–5

Replay:

`Worker`
→ Replay mode
→ validated `Worker:Replay:*`
→ validated existing `Dataset:*`
→ exact target equality
→ WP02 replay source
→ selected replay observations
→ `[From,To)` timestamp validation
→ WP03 explicit-observation seam
→ `ExecuteCanonical`
→ stages 1–5
→ finite completion

If this exact real production composition cannot be proven, stop.

---

# Phase 13 — Diff and Scope Audit

Inspect the full current WP03 diff, including earlier validated changes and this consolidated completion.

Classify every changed file as:

- canonical-stage extraction;
- additive observation seam;
- Worker config/options;
- Worker validation;
- Dataset validation;
- Worker dispatch/composition;
- DI;
- directly required compatibility;
- WP03 test;
- explicitly required documentation/config artifact.

Prove:

- exactly one `ExecuteCanonical`;
- both modes converge on it;
- historical storage remains historical-only;
- replay never persists via historical storage;
- no parallel pipeline;
- no target normalization;
- no derived Dataset From/To;
- no extra Worker config fields;
- no generalized mode framework;
- no unnecessary WP02 redesign;
- no WP04+ work;
- no unauthorized package/Python/schema/protocol/planning changes;
- pre-existing authority/control files preserved.

Anything unexplained blocks acceptance.

---

# Phase 14 — WP03 Technical Acceptance Gate

Before GitHub mutation, enumerate every #228 criterion.

For each report:

- criterion;
- implementation evidence;
- test evidence;
- PASS/FAIL.

Additionally require PASS for:

- validated `ExecuteCanonical`;
- additive observation seam;
- fixed Worker config contract implemented exactly;
- fixed Dataset-boundary contract implemented exactly;
- Historical default/backward compatibility;
- Replay explicit selection;
- fail-fast invalid Worker config;
- fail-fast invalid Dataset config;
- exact target equality;
- timestamp-bound validation;
- real WP02 replay composition;
- replay observations through WP03 seam;
- five canonical stages;
- no replay historical-storage use;
- identity/ticks/restart/duplicates/cancellation/WP02 bounds/end-of-replay;
- Worker tests;
- Dataset-boundary tests;
- Application suite;
- WP02 replay suite;
- build;
- full regression;
- diff/scope audit.

If any item fails, leave #228 Open / Backlog.

---

# Phase 15 — WP03 GitHub Lifecycle Finalization

Only after all technical acceptance gates pass:

1. read #228 current state;
2. confirm established completion convention;
3. add one concise evidence comment if required;
4. transition #228 Project Status from Backlog to authoritative completed state;
5. preserve:
   - Priority = P1;
   - Release = 1.9;
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. read back every mutation.

Do not modify #229.

---

# Expected Post-Completion State

After success:

- #226 Closed / Done;
- #227 Closed / Done;
- #228 Closed / Done or current authoritative completed state;
- #229–#237 remain Open and untouched;
- milestone #58 remains Open;
- canonical milestone counts:
  - **9 open**
  - **3 closed**;
- raw GitHub closed count may additionally include #225;
- dependency chain remains 11/11;
- final successful WP03 regression count becomes WP04 predecessor baseline;
- WP04 #229 becomes next eligible;
- WP04 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- #228/manifest cannot be read;
- validated WP03 executor/seam state is missing;
- either fixed normative contract conflicts with repository reality and would require redesign;
- implementation requires changing Worker keys/modes/defaults;
- implementation requires changing DatasetDefinition or deriving Dataset bounds;
- replay requires historical-storage use;
- second pipeline/executor becomes necessary;
- WP02 semantics regress;
- WP04+ scope becomes necessary;
- unauthorized package/Python/schema/protocol changes become necessary;
- Worker/config/Dataset tests fail;
- Application or WP02 predecessor guards fail;
- build fails;
- full regression fails;
- production flow cannot be proven;
- diff audit reveals unexplained scope;
- GitHub lifecycle mutation fails or cannot be proven.

On stop:

- preserve validated existing WP03 work;
- do not broaden authority;
- report exact blocker and last proven state;
- leave #228 open unless technical acceptance fully passed and lifecycle mutation alone failed.

---

# Success Criteria

WP03 succeeds only when:

- both fixed normative contracts are implemented exactly;
- Historical/default behavior remains compatible;
- Replay mode is explicit and validated;
- Dataset section remains required in Replay;
- exact target equality is enforced;
- Dataset From/To remain configured and unchanged in semantics;
- selected replay observations are validated within `[From,To)`;
- no replay persistence through historical storage;
- real Worker replay composition uses WP02 source;
- replay observations use WP03 seam;
- Historical and Replay converge on `ExecuteCanonical`;
- all five stages execute;
- all WP02 semantics remain intact;
- every #228 criterion passes;
- focused Worker tests pass;
- focused Dataset tests pass;
- Application suite passes;
- WP02 replay suite passes;
- build passes;
- full regression passes;
- final diff remains WP03-scoped;
- #228 is completed and closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- dependency chain remains intact;
- WP04 remains unstarted.

---

# Required Completion Report

Return:

## Fixed Worker contract implementation
- configuration types;
- binding;
- mode parsing;
- defaults;
- validation.

## Fixed Dataset-boundary implementation
- Dataset requirement;
- target consistency enforcement;
- From/To parsing/validation;
- replay observation interval validation.

## Runtime composition
- actual Historical production flow;
- actual Replay production flow;
- exact mapping to WP02;
- proof both converge on `ExecuteCanonical`;
- proof replay bypasses historical storage.

## Validation
Report exact:
- Worker/config tests;
- Dataset-boundary tests;
- Worker/runtime composition tests;
- Application suite count;
- WP02 replay suite count;
- build errors/warnings;
- full regression command and exact counts.

## Acceptance
Report PASS/FAIL for every #228 criterion and explicitly:
- Historical;
- Replay;
- missing mode;
- unknown mode;
- invalid Replay config;
- invalid Dataset config;
- exact target mismatch;
- timestamp bounds;
- identity;
- ticks;
- restart/resume;
- duplicates;
- cancellation;
- WP02 bounds;
- finite completion;
- five-stage Replay production path.

## Scope proof
- full diff classification;
- no contract redesign;
- no parallel pipeline;
- no replay historical-store use;
- no generalized framework;
- no WP04+ work;
- no unauthorized foundation/planning change.

## GitHub lifecycle
- #228 before/after;
- Project Status before/after;
- completion comment;
- milestone #58 canonical counts;
- #229–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP03 CONSOLIDATED WORKER IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP03 CONSOLIDATED WORKER IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical and lifecycle requirement is freshly proven.

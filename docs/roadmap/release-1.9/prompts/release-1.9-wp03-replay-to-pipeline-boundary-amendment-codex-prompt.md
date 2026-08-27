# Release 1.9 — WP03 Replay-to-Pipeline Boundary Amendment — Codex Authority

## Authority

This document grants a **narrow corrective amendment** for Release 1.9 WP03, canonical GitHub issue **#228**.

WP03 is blocked before mutation.

Proven blocker:

- #228 requires replay increments to flow through the existing five-stage pipeline;
- the current pipeline materialization contract accepts only `DatasetDefinition`;
- materialization obtains observations exclusively through `IHistoricalObservationStore`;
- the existing WP03 authority does not define an unambiguous minimum replay-to-pipeline adaptation;
- materially different designs are possible;
- choosing among them without authority could create:
  - a second ingestion path,
  - misuse of the historical-store abstraction,
  - or an unauthorized pipeline/Application contract redesign;
- no WP03 files were changed;
- #228 remains Open / Backlog;
- WP04 has not started.

Terminal blocked state:

`RELEASE 1.9 WP03 BLOCKED`

This amendment authorizes Codex to determine, implement, test, and finalize only the **minimum replay-to-existing-pipeline adaptation required by #228**.

It does not authorize a general ingestion redesign.

It does not authorize WP04.

---

# Objective

Preserve the repository's **single canonical five-stage pipeline** while enabling WP02 replay increments to traverse that pipeline exactly as required by #228.

The amendment must establish an explicit, governed replay-to-pipeline boundary that:

- does not create a parallel replay pipeline;
- does not bypass required pipeline stages;
- preserves historical ingestion behavior;
- preserves completed WP02 replay semantics;
- changes existing contracts only when demonstrably necessary;
- remains strictly inside WP03 acceptance scope.

---

# Architectural Invariants

These invariants are mandatory.

## One pipeline

There must remain one canonical five-stage processing pipeline.

Replay must not receive a separate reduced, duplicated, forked, or shadow pipeline.

## Explicit ingestion boundary

Replay increments must enter through an explicit boundary whose semantics are visible in code and tests.

Do not route replay through hidden global/static state or undocumented side channels.

## Historical behavior preserved

Existing historical materialization through `IHistoricalObservationStore` must continue to behave as governed unless #228 explicitly changes it.

Do not reinterpret "historical store" as "generic replay source" merely because the interface is convenient.

Semantic compatibility must be proven.

## WP02 semantics preserved

The adaptation must preserve, as applicable:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- explicit finite/end-of-replay semantics.

## Minimum change

Prefer the smallest coherent architectural adaptation satisfying #228.

Do not create a generalized ingestion framework for hypothetical future WPs.

---

# Explicitly Forbidden

Do not:

- create a second five-stage pipeline;
- duplicate pipeline stages for replay;
- bypass stages required by #228;
- introduce a broad "universal ingestion" abstraction without demonstrated WP03 necessity;
- force replay into `IHistoricalObservationStore` without proving semantic correctness;
- persist replay data merely to make it look historical;
- change SQLite schema version unless #228 explicitly requires it;
- redesign storage;
- redesign the WP02 replay contract for preference;
- change package pins;
- change Python version;
- change Streamlit version;
- change the one-shot JSON-over-stdio boundary unless #228 explicitly requires a compatible adjustment;
- implement WP04 or later work;
- add future-WP scaffolding;
- alter Release 1.9 planning;
- change dependency edges;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #228 before technical acceptance passes.

---

# Phase 0 — Read and Prove Existing Architecture

Before mutation:

1. Read #228 completely.
2. Read the relevant Release 1.9 WP03 definition/manifest.
3. Read the complete existing five-stage pipeline.
4. Identify and document each of the five stages in order.
5. Identify the pipeline entry/materialization contract.
6. Read `DatasetDefinition`.
7. Read `IHistoricalObservationStore`.
8. Read all materialization implementations and call sites.
9. Read WP02 replay contracts and implementation.
10. Read tests governing:
    - historical ingestion;
    - pipeline stages;
    - replay behavior.
11. Prove repository remains free of WP03 mutations from the blocked attempt.

Build an explicit current data-flow map:

`caller → materialization boundary → observation acquisition → stage 1 → stage 2 → stage 3 → stage 4 → stage 5 → result`

Also map the current replay flow.

Do not mutate code in this phase.

---

# Phase 1 — Requirement-to-Boundary Matrix

For each #228 acceptance criterion, identify:

- required replay input;
- required pipeline stage(s);
- required output;
- required WP02 semantic preservation;
- current architectural gap;
- minimum boundary capability needed.

Explicitly answer:

1. Does replay need to replace observation acquisition only?
2. Does replay require additional context to accompany observations through the pipeline?
3. Does `DatasetDefinition` remain semantically valid for replay processing?
4. Is `IHistoricalObservationStore` semantically valid for replay increments?
5. Can replay enter before stage 1 without altering stage semantics?
6. Does cancellation need propagation beyond acquisition?
7. Does logical replay identity/tick need to survive into downstream pipeline behavior?
8. Can restart/resume remain wholly owned by the replay source while the pipeline processes increments normally?
9. Does finite replay completion require a pipeline contract change, or can it remain outside an individual pipeline invocation?

Do not assume answers.

Prove them from #228 and repository behavior.

---

# Phase 2 — Candidate Boundary Evaluation

Evaluate only minimal plausible adaptations supported by the current architecture.

Examples of candidate classes may include, but are not limited to:

- a narrow observation-input abstraction at the existing materialization boundary;
- an overload/additive pipeline entry that accepts already-acquired observations plus existing dataset context;
- a small source/materialization request contract that selects governed observation acquisition;
- an adapter that is semantically valid against an existing abstraction.

These examples are not pre-authorization of any specific design.

For each viable candidate, assess:

- #228 acceptance coverage;
- number and type of contract changes;
- historical compatibility;
- WP02 semantic preservation;
- testability;
- whether it creates a second ingestion path;
- whether it leaks Infrastructure concerns into Application/domain contracts;
- whether it introduces future-oriented abstraction.

Select the minimum candidate only if one is clearly superior under accepted authority.

### Hard stop

If two or more materially different minimal designs remain equally valid after full inspection, stop.

Report the alternatives and the missing authority needed to choose.

Do not make an architectural preference decision silently.

---

# Phase 3 — Boundary Design Gate

Before implementation, document the selected boundary precisely.

For every proposed changed/new contract element state:

- exact purpose;
- #228 criterion requiring it;
- layer ownership;
- why existing contract is insufficient;
- historical-call compatibility;
- replay-call semantics;
- cancellation semantics;
- lifecycle/end-of-replay semantics;
- test strategy.

Prove explicitly:

`Replay uses the existing five-stage pipeline; it does not create or invoke a second pipeline implementation.`

If that cannot be proven from the design, stop.

---

# Phase 4 — Minimal Boundary Implementation

Implement only the selected replay-to-pipeline boundary.

Rules:

1. preserve all five existing stages;
2. reuse the canonical stage implementations;
3. keep historical acquisition behavior intact;
4. keep replay acquisition semantics explicit;
5. avoid replay persistence unless explicitly required by #228;
6. avoid hidden mutable state;
7. preserve existing `DatasetDefinition` semantics unless a minimum additive change is proven necessary;
8. preserve `IHistoricalObservationStore` semantics unless #228 explicitly and semantically requires a change;
9. update only directly affected callers/implementations;
10. add tests alongside the boundary.

Run focused build/tests after each logical change.

---

# Phase 5 — Complete WP03 Behavior

After the boundary is established, complete the remaining #228 behavior using it.

Replay increments must traverse the canonical pipeline as required.

Prove the actual runtime/control flow reaches:

- canonical stage 1;
- canonical stage 2;
- canonical stage 3;
- canonical stage 4;
- canonical stage 5.

Do not merely test that equivalent calculations occur.

Test/use the real canonical pipeline path.

---

# Phase 6 — Focused Acceptance Tests

Add or update tests proving at minimum:

## Pipeline reuse
- replay enters the canonical pipeline;
- all five canonical stages execute as required;
- no replay-specific parallel stage chain exists.

## Historical compatibility
- existing historical materialization still uses its governed acquisition behavior;
- existing historical tests remain valid.

## Replay identity
- identity semantics from WP02 remain intact across the adaptation where relevant.

## Logical ticks
- tick ordering/determinism remains intact.

## Restart/resume
- resumed replay increments enter the same canonical pipeline correctly.

## Duplicates
- duplicate determinism is preserved through pipeline processing.

## Cancellation
- cancellation remains observable and does not result in unauthorized continued replay processing.

## Bounds/end-of-replay
- finite replay behavior remains correct;
- end-of-replay is not confused with pipeline failure.

## #228-specific outputs
- every explicit WP03 acceptance output/behavior is directly tested.

---

# Phase 7 — Predecessor Regression

Run the relevant WP02 replay/contract test set explicitly.

The adaptation must not regress:

- replay identity;
- ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- end-of-replay.

Do not weaken predecessor tests.

---

# Phase 8 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture exact:

- exit status;
- passed;
- failed;
- skipped;
- material warnings.

Pre-WP03 full regression baseline:

**287/287 passing**

A larger count is acceptable when explained by WP03-owned tests.

A lower count or removed tests requires explicit #228 justification; otherwise stop.

---

# Phase 9 — Architecture and Diff Audit

Before GitHub mutation, inspect the final diff and resulting architecture.

Classify every changed file as:

- replay-to-pipeline boundary;
- directly required compatibility update;
- WP03 production behavior;
- WP03 test;
- WP03 configuration/DI if explicitly required;
- WP03 documentation if explicitly required.

Prove:

- exactly one canonical five-stage pipeline remains;
- replay uses it;
- historical ingestion remains governed;
- no second ingestion pipeline was introduced;
- `IHistoricalObservationStore` was not semantically overloaded without proof;
- WP02 contracts were not unnecessarily redesigned;
- no WP04+ implementation exists;
- no package/Python/schema/planning mutation occurred unless explicitly authorized by #228.

Anything unexplained is a blocker.

---

# Phase 10 — Technical Acceptance Gate

Before lifecycle mutation, enumerate every #228 acceptance criterion.

For each report:

- criterion;
- implementation evidence;
- test evidence;
- PASS/FAIL.

Also require:

- boundary design proven minimal;
- focused WP03 tests pass;
- WP02 predecessor tests pass;
- full regression passes;
- architecture audit passes;
- diff audit passes.

If any item fails, keep #228 Open / Backlog and stop.

---

# Phase 11 — WP03 GitHub Lifecycle Finalization

Only after technical acceptance:

1. read #228 current state;
2. confirm the established completion convention;
3. add one concise completion/evidence comment if required;
4. transition #228 Project Status from Backlog to the authoritative completed value;
5. preserve:
   - Priority = P1;
   - Release = 1.9;
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. immediately read back mutations.

Do not modify #229.

---

# Expected Post-Completion State

After successful WP03 completion:

- #226 closed / Done;
- #227 closed / Done;
- #228 closed / authoritative completed status;
- #229–#237 remain open and untouched;
- milestone #58 remains open;
- canonical milestone state:
  - 9 open
  - 3 closed;
- raw GitHub counts may additionally include historical duplicate #225;
- dependency chain remains 11/11;
- successful WP03 regression count becomes the predecessor baseline for WP04;
- WP04 #229 becomes next eligible;
- WP04 is not started.

---

# Stop Conditions

Stop immediately if:

- #228 cannot be read;
- the five-stage pipeline cannot be unambiguously identified;
- #228 replay-through-pipeline semantics remain ambiguous;
- two materially different minimal boundary designs remain equally valid;
- replay would require a second pipeline;
- safe implementation requires broad ingestion redesign;
- `IHistoricalObservationStore` would need semantic repurposing not supported by authority;
- schema/persistence redesign becomes necessary without explicit #228 authority;
- WP02 semantics cannot be preserved;
- WP04+ scope becomes necessary;
- focused tests fail;
- predecessor regression fails;
- full regression fails;
- architecture/diff audit reveals scope expansion;
- GitHub lifecycle mutation cannot be proven.

On stop:

- do not broaden scope;
- preserve evidence;
- report the candidate designs considered;
- identify the exact unresolved boundary;
- leave #228 open unless technical acceptance had already passed and lifecycle mutation alone failed.

---

# Success Criteria

WP03 succeeds only when:

- a single minimum replay-to-pipeline boundary is proven and implemented;
- replay increments traverse the existing canonical five-stage pipeline;
- no second replay pipeline exists;
- historical ingestion semantics remain correct;
- WP02 replay semantics remain correct;
- every #228 acceptance criterion passes;
- focused WP03 tests pass;
- WP02 predecessor tests pass;
- full regression passes;
- architecture/diff audit confirms narrow scope;
- no unauthorized foundation/planning change occurs;
- #228 receives required completion evidence;
- #228 Project Status reaches the authoritative completed state;
- #228 is closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- dependency chain remains intact;
- WP04 has not started.

---

# Required Completion Report

Return:

## Boundary analysis
- existing five-stage flow;
- existing historical acquisition boundary;
- replay flow before WP03;
- candidate adaptations considered;
- selected minimum boundary and rationale.

## Contract/architecture change
- exact changed/new contract shape;
- why it is required;
- historical compatibility proof;
- proof that one canonical pipeline remains.

## WP03 implementation
- files changed;
- behavior implemented;
- tests added/changed.

## Acceptance evidence
Report PASS/FAIL for every #228 criterion and explicitly for:
- five-stage pipeline reuse;
- historical compatibility;
- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- finite replay/end-of-replay.

## Validation
- focused test results;
- WP02 predecessor regression;
- full regression command and exact counts.

## Scope/architecture proof
- final diff classification;
- no parallel pipeline;
- no unauthorized historical-store repurposing;
- no WP04+ work;
- no unauthorized package/Python/schema/planning change.

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

On success, end with exactly:

`RELEASE 1.9 WP03 REPLAY-TO-PIPELINE AMENDMENT AND EXECUTION COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP03 REPLAY-TO-PIPELINE AMENDMENT BLOCKED`

Do not emit the success marker unless all technical and lifecycle requirements are freshly proven.

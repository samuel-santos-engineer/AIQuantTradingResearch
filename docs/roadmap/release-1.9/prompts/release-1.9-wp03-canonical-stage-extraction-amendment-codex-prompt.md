# Release 1.9 — WP03 Canonical Stage Extraction Amendment — Codex Authority

## Authority

This document grants a **narrow corrective execution amendment** for Release 1.9 WP03, canonical GitHub issue **#228**.

The replay-to-pipeline architectural boundary has already been resolved.

Selected minimum boundary:

> **Additive observation-input seam at materialization, reusing the existing canonical five-stage `PipelineExecutionUseCase` while preserving historical acquisition and WP02 replay semantics.**

The prior implementation attempt established that realizing this seam requires a behavior-preserving extraction of the shared canonical five-stage execution logic from the current pipeline method.

That extraction was not completed.

Current proven state:

- incomplete WP03 edits were removed;
- completed WP02 changes remain intact;
- pre-existing Release 1.9 authority/control files remain intact;
- repository build after restoration passed with:
  - 0 errors
  - 0 warnings;
- #228 remains Open / Backlog;
- no WP03 technical acceptance claim was made;
- no WP03 GitHub lifecycle mutation was made;
- WP04 has not started.

Terminal prior state:

`RELEASE 1.9 WP03 REPLAY-TO-PIPELINE AMENDMENT BLOCKED`

This authority permits only the minimum canonical-stage extraction necessary to realize the already-selected observation-input seam, complete WP03, validate it, and finalize #228 if all acceptance gates pass.

It does not reopen architectural selection.

It does not authorize a generalized pipeline redesign.

It does not authorize WP04.

---

# Objective

Refactor the existing `PipelineExecutionUseCase` so that:

1. the existing historical materialization path retains its governed observation acquisition through `IHistoricalObservationStore`;
2. the selected additive replay observation-input seam can supply replay observations at materialization;
3. **both paths invoke the exact same extracted canonical five-stage execution path**;
4. stage order and stage behavior remain unchanged;
5. no second pipeline implementation or duplicated stage chain exists;
6. completed WP02 replay semantics remain intact;
7. all #228 acceptance criteria can be proven.

The extraction is authorized only as an enabling refactor for WP03.

---

# Fixed Architectural Decision

The following decision is already settled and must not be reconsidered unless repository evidence proves it impossible:

`historical acquisition ─┐`
`                       ├─> canonical materialization boundary ─> shared extracted five-stage executor`
`replay observations ───┘`

Historical ingestion and replay ingestion may differ in **observation acquisition**.

They must converge before canonical stage execution.

There must be exactly one implementation of the governed five-stage processing sequence.

---

# Mandatory Invariants

## One canonical executor

After the refactor, one shared internal execution path must own the canonical five-stage sequence.

Both historical and replay materialization must call it.

## Behavior-preserving extraction

For historical processing:

- inputs remain semantically equivalent;
- stage order remains identical;
- outputs remain equivalent;
- error behavior remains equivalent unless #228 explicitly requires a change;
- cancellation behavior remains equivalent where applicable.

## Additive replay seam

Replay must enter through the already-selected additive observation-input seam.

Do not repurpose `IHistoricalObservationStore` as replay storage.

Do not persist replay observations merely to feed the historical path.

## WP02 preservation

Preserve:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- explicit end-of-replay semantics.

## No future abstraction

Do not turn the extraction into a generic framework for hypothetical ingestion types or future WPs.

---

# Explicitly Forbidden

Do not:

- create a second pipeline;
- duplicate the five stages;
- change stage order;
- rewrite stage algorithms unless #228 explicitly requires behavior change;
- introduce a broad pipeline engine/framework;
- create generalized pluggable ingestion architecture beyond the selected seam;
- semantically repurpose `IHistoricalObservationStore`;
- change SQLite schema version;
- change package pins;
- change Python version;
- change Streamlit version;
- redesign the one-shot JSON-over-stdio boundary;
- implement WP04 or later work;
- add WP04 scaffolding;
- alter Release 1.9 planning;
- change dependency edges;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #228 before all technical acceptance gates pass.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before mutation:

1. Read #228 completely.
2. Read the accepted WP03 definition/manifest.
3. Read the current `PipelineExecutionUseCase`.
4. Identify the exact five canonical stages and their current order.
5. Identify:
   - historical observation acquisition;
   - materialization logic;
   - stage execution logic;
   - result construction.
6. Read `IHistoricalObservationStore`.
7. Read the completed WP02 replay contracts and adapter.
8. Read existing historical pipeline tests.
9. Read WP02 replay tests.
10. Prove the repository contains no residual incomplete WP03 edits.
11. Record branch, HEAD, staged state, tracked state, and relevant untracked state.

Do not mutate until the current pipeline control flow is documented.

---

# Phase 1 — Extraction Plan

Define the smallest behavior-preserving extraction.

The plan must identify:

- exact code currently responsible for stage 1;
- exact code currently responsible for stage 2;
- exact code currently responsible for stage 3;
- exact code currently responsible for stage 4;
- exact code currently responsible for stage 5;
- shared inputs needed by all five stages;
- shared outputs/result construction;
- acquisition-specific logic that must remain outside the executor.

The extracted executor may be:

- a private/internal method;
- a narrowly scoped internal collaborator;

choose whichever requires less architectural surface and better matches repository conventions.

Do not create a public abstraction unless #228 requires public contract exposure.

### Required proof before coding

The plan must demonstrate:

`historical path -> acquire historical observations -> shared executor`

and:

`replay path -> receive replay observations -> shared executor`

with no duplicated stage sequence.

---

# Phase 2 — Canonical Stage Extraction

Perform the extraction first, before adding replay behavior.

Rules:

1. move only the shared stage-execution logic;
2. preserve stage order exactly;
3. preserve existing stage implementations;
4. preserve existing result semantics;
5. keep historical acquisition outside the shared executor;
6. avoid unrelated renaming/cleanup;
7. avoid changing public contracts unless the selected seam requires an additive contract;
8. keep the diff mechanically reviewable.

Immediately after extraction, run the existing historical pipeline tests.

If historical behavior changes before replay is added, stop and correct only the extraction.

Do not proceed by accepting changed historical behavior.

---

# Phase 3 — Historical Equivalence Gate

Before implementing the replay seam, prove the extracted historical path is behaviorally equivalent.

At minimum prove:

- same historical observation acquisition;
- same five stages;
- same stage order;
- same relevant outputs;
- same relevant failure behavior;
- same relevant cancellation behavior;
- existing historical tests pass.

Where practical, add a focused equivalence/interaction test proving the historical entry invokes the shared canonical executor.

Do not continue if the extraction itself regresses historical behavior.

---

# Phase 4 — Additive Observation-Input Seam

Implement the already-selected minimum replay seam.

The seam must:

- accept replay observations without pretending they came from `IHistoricalObservationStore`;
- retain the existing dataset/context required by the canonical stages;
- invoke the shared extracted executor;
- remain additive/minimal;
- avoid creating a generic ingestion framework.

If a small additive Application contract is required, make only the minimum change justified by #228.

Do not redesign the WP02 replay contract.

---

# Phase 5 — Replay-to-Canonical-Pipeline Integration

Connect completed WP02 replay increments to the additive seam.

Prove replay execution reaches the same canonical executor used by historical processing.

Preserve WP02 semantics:

## Replay identity
Identity remains explicit and deterministic where required.

## Logical ticks
Tick ordering remains deterministic.

## Restart/resume
Resumed replay increments traverse the same shared executor.

## Duplicates
Duplicate determinism is preserved.

## Cancellation
Cancellation prevents unauthorized continued replay processing and follows established contract semantics.

## Bounds/end-of-replay
Finite replay and end-of-replay behavior remain explicit and are not confused with pipeline failure.

---

# Phase 6 — Focused WP03 Tests

Add/update tests proving:

## Shared executor
- historical entry uses the shared canonical executor;
- replay entry uses the same shared canonical executor.

## Five-stage execution
- all five canonical stages execute for historical processing as before;
- all five canonical stages execute for replay processing as #228 requires;
- stage order is unchanged.

## No parallel pipeline
- no replay-specific duplicated five-stage chain exists.

Use structural/code evidence plus behavioral tests where direct interaction testing is impractical.

## Historical compatibility
- existing historical pipeline behavior remains correct.

## WP02 replay preservation
- replay identity;
- ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- end-of-replay.

## #228 acceptance
Test every additional #228-specific behavior directly.

---

# Phase 7 — Predecessor-Sensitive Regression

Run the relevant WP02 replay/contract tests explicitly.

All completed WP02 semantics must remain passing.

Do not modify or weaken predecessor tests merely to accommodate WP03.

---

# Phase 8 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

Pre-WP03 regression baseline:

**287/287 passing**

A higher count is acceptable when fully explained by WP03-owned tests.

A lower count or removed tests is a blocker unless #228 explicitly requires and justifies it.

Also run the repository's appropriate build command and report errors/warnings.

---

# Phase 9 — Architecture and Diff Audit

Inspect the complete final diff.

Classify each changed file as:

- canonical-stage extraction;
- additive observation-input seam;
- replay-to-pipeline integration;
- directly required compatibility update;
- WP03 test;
- WP03-required documentation/configuration.

Prove all of the following:

- exactly one canonical five-stage executor exists;
- historical and replay paths both use it;
- historical acquisition still uses `IHistoricalObservationStore`;
- replay does not masquerade as historical storage;
- no duplicated stage chain exists;
- stage order is unchanged;
- no generalized ingestion framework was introduced;
- WP02 replay contracts were not unnecessarily redesigned;
- no WP04+ work exists;
- no unauthorized package/Python/schema/protocol/planning change occurred;
- no pre-existing authority/control file was altered.

Anything unexplained is a blocker.

---

# Phase 10 — Technical Acceptance Gate

Before GitHub mutation, enumerate every #228 acceptance criterion.

For each report:

- criterion;
- implementation evidence;
- test evidence;
- PASS/FAIL.

Additionally require explicit PASS for:

- behavior-preserving canonical-stage extraction;
- one shared executor;
- historical path uses shared executor;
- replay path uses shared executor;
- all five stages preserved in order;
- historical compatibility;
- WP02 replay semantic preservation;
- focused tests;
- predecessor-sensitive regression;
- full regression;
- build;
- architecture/diff audit.

If any item fails, keep #228 Open / Backlog.

---

# Phase 11 — WP03 GitHub Lifecycle Finalization

Only after technical acceptance:

1. read #228 current state;
2. confirm the established completion convention;
3. add one concise completion/evidence comment if required;
4. transition #228 Project Status from Backlog to the authoritative completed state;
5. preserve:
   - Priority = P1;
   - Release = 1.9;
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. immediately read back all mutations.

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
- raw GitHub closed count may additionally include historical duplicate #225;
- dependency chain remains 11/11;
- successful WP03 regression count becomes the predecessor baseline for WP04;
- WP04 #229 becomes next eligible;
- WP04 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- #228 cannot be read;
- repository contains residual uncertain WP03 edits;
- current five-stage sequence cannot be unambiguously identified;
- extraction changes historical behavior;
- selected additive seam cannot be realized without broad redesign;
- more than one canonical stage executor would result;
- replay requires semantic repurposing of `IHistoricalObservationStore`;
- WP02 replay semantics regress;
- WP04+ work becomes necessary;
- package/Python/schema/protocol changes become necessary without explicit #228 authority;
- focused tests fail;
- predecessor-sensitive tests fail;
- full regression fails;
- build fails;
- architecture/diff audit reveals scope expansion;
- GitHub lifecycle mutation fails or cannot be proven.

On stop:

- do not broaden scope;
- preserve evidence;
- report exact last proven state;
- distinguish extraction failure from replay-integration failure;
- leave #228 open unless technical acceptance had fully passed and lifecycle mutation alone failed.

---

# Success Criteria

WP03 succeeds only when:

- the existing five-stage execution is extracted into one shared canonical executor;
- extraction is behavior-preserving for historical processing;
- the additive observation-input seam is implemented;
- replay increments use that seam;
- historical and replay paths converge on the same canonical executor;
- all five stages remain in the same governed order;
- no parallel pipeline exists;
- historical `IHistoricalObservationStore` semantics remain intact;
- all WP02 replay semantics remain intact;
- every #228 acceptance criterion passes;
- focused tests pass;
- predecessor-sensitive tests pass;
- full regression passes;
- build passes;
- final architecture/diff remains narrowly WP03-scoped;
- #228 receives required completion evidence;
- #228 Project Status reaches authoritative completed state;
- #228 is closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- dependency chain remains intact;
- WP04 has not started.

---

# Required Completion Report

Return:

## Extraction
- original five-stage location;
- extracted canonical executor location/shape;
- exact stage order;
- proof extraction is behavior-preserving.

## Replay seam
- exact additive observation-input seam;
- historical acquisition path;
- replay acquisition/input path;
- proof both converge on the same executor.

## WP03 implementation
- files changed;
- behavior implemented;
- tests added/changed.

## Acceptance evidence
Report PASS/FAIL for every #228 criterion and explicitly:
- shared canonical executor;
- historical compatibility;
- replay pipeline reuse;
- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds/end-of-replay.

## Validation
- focused tests;
- WP02 predecessor-sensitive tests;
- build result with errors/warnings;
- full regression command and exact counts.

## Scope/architecture proof
- final diff classification;
- exactly one canonical five-stage executor;
- no parallel replay pipeline;
- no historical-store repurposing;
- no generalized ingestion framework;
- no WP04+ work;
- no unauthorized foundation/planning changes.

## GitHub lifecycle
- #228 before/after;
- Project Status before/after;
- completion comment status;
- milestone #58 canonical counts;
- #229–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP03 CANONICAL STAGE EXTRACTION AND EXECUTION COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP03 CANONICAL STAGE EXTRACTION BLOCKED`

Do not emit the success marker unless all technical acceptance and lifecycle requirements are freshly proven.

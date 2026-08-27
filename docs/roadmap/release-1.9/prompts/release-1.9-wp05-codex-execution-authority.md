# Release 1.9 — WP05 — Codex Execution Authority

## Authority

This document grants execution authority for:

**Release 1.9 WP05 — canonical GitHub issue #230**

Accepted predecessor state:

- WP01 #226: Closed / Done
- WP02 #227: Closed / Done
- WP03 #228: Closed / Done
- WP04 #229: Closed / Done
- SQLite schema baseline: **v4**
- Historical source authority: `0`
- Replay source authority: `1`
- canonical five-stage `ExecuteCanonical` pipeline preserved
- completed WP02 Replay semantics preserved
- completed WP03 Worker Replay composition/persistence preserved
- completed WP04 presentation read-model / atomic-handoff contract implemented
- `HistoricalPresentationRevision` implemented
- Replay logical-tick revisions remain unchanged
- full regression predecessor baseline: **297/297 passed**
- build predecessor baseline: **0 errors / 0 warnings**
- milestone #58 remains open
- canonical milestone state: **8 open / 4 closed**
- raw GitHub milestone count includes historical duplicate #225 separately
- #230–#237 remain open
- WP05 has not started
- dependency chain remains intact
- protected milestones and #225 remain preserved

The current authoritative predecessor regression baseline for WP05 is:

**297/297 passing**

This authority is for **WP05 only**.

It does not authorize WP06 or later work.

---

# Objective

Execute WP05 exactly as defined by:

1. canonical issue **#230**;
2. the accepted Release 1.9 planning/definition authority;
3. completed WP01–WP04 predecessor evidence;
4. the current repository architecture, including the WP04 producer-side presentation contract.

Do not infer WP05 scope from sequence or prior work packages.

Read #230 first and derive the exact work package objective, deliverables, acceptance criteria, authorized paths, and lifecycle expectations.

---

# Fixed Predecessor Architecture

WP05 inherits and must not casually redefine:

## WP04 presentation contract

Model C:

- immutable versioned snapshot
- bounded accumulated window
- capacity = 64
- oldest→newest
- duplicate source-tick replacement
- older out-of-order row ignored
- oldest eviction on overflow
- session-lifetime only

Contract version:

`aiq-visualization-read-model-v1`

States:

- Ready
- Empty
- WarmUp
- Stale
- Failed

Historical revision:

- `RevisionKind = HistoricalPresentation`
- `HistoricalPresentationRevision : ulong`
- first publication = 1
- Ready / Empty / WarmUp / Failed increment
- Stale does not increment
- session-local
- restart resets
- no synthetic source tick

Replay revision:

- `RevisionKind = ReplayLogicalTick`
- actual WP02 logical tick
- no cross-mode numeric ordering

Atomic publication:

- single writer
- immutable complete envelope
- concurrent readers see old complete or new complete only

## WP05 consumer boundary

WP04 already owns:

- read-model construction
- feature-state encoding
- revision assignment
- boundedness
- atomic publication
- stale/failure semantics

WP05 must consume this contract truthfully.

Unless #230 explicitly authorizes otherwise, WP05 must not:

- recompute features
- access SQLite directly
- call providers directly
- reinterpret failure semantics
- mutate producer state
- redefine revision semantics

---

# Phase 0 — Load WP05 Authority

Before mutation:

1. Read #230 completely.
2. Read the Release 1.9 definition/manifest sections governing WP05.
3. Extract:
   - exact title
   - objective
   - deliverables
   - acceptance criteria
   - authorized/expected paths
   - UI/presentation constraints
   - read-model consumption requirements
   - refresh/polling requirements if defined
   - error/state rendering requirements
   - test requirements
   - documentation requirements
   - lifecycle expectations
   - predecessor/dependency requirements
4. Read current Streamlit/UI code and tests relevant to #230.
5. Read WP04 read-model envelope and consumer-facing access path.
6. Build an explicit acceptance checklist.

Do not code until the checklist is unambiguous.

If #230 materially conflicts with the accepted Release 1.9 authority, stop.

---

# Predecessor Gate

Before any WP05 mutation, freshly prove:

- #226–#229 are closed/completed;
- #230 is open and canonical WP05;
- #230 has exactly one Project #2 item;
- #230 Project fields remain:
  - Status = Backlog
  - Priority = P1
  - Release = 1.9
  - authoritative Area;
- milestone #58 remains open;
- dependency edge WP04 → WP05 exists with correct direction semantics;
- #231–#237 remain open;
- repository has no unexpected tracked/staged changes;
- no residual or partial WP05 implementation exists;
- WP04 producer/read-model tests remain passing.

If this gate fails, stop.

---

# Current Technical Baseline

WP05 inherits:

- CPython 3.13.15 x64 in isolated `.venv`;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- governed one-shot JSON-over-stdio boundary;
- SQLite schema v4;
- source authorities `{0,1}`;
- canonical `ExecuteCanonical`;
- completed Worker Replay path;
- completed WP04 read model;
- full regression **297/297**;
- build 0 errors / 0 warnings.

Do not use older 290/293 totals as the expected current baseline.

---

# Scope Discipline

## Permitted

WP05 may:

- inspect code/tests needed to understand #230;
- modify the minimum UI/presentation/consumer files necessary;
- consume the WP04 read-model envelope;
- implement rendering behavior explicitly required by #230;
- implement polling/refresh only if #230 defines or clearly authorizes it;
- add focused UI/consumer tests;
- add narrowly required serialization/reader code if #230 explicitly requires it;
- update Streamlit code within #230 scope;
- update documentation if required;
- run focused validation;
- run predecessor-sensitive regression;
- run full regression;
- finalize #230 lifecycle only after technical acceptance.

## Forbidden

Do not:

- implement WP06 or later work;
- redesign WP04 envelope/revision/state semantics for preference;
- recompute pipeline features in Streamlit/UI;
- access SQLite directly from Streamlit/UI unless #230 explicitly authorizes a new boundary;
- call market/provider services from Streamlit/UI;
- mutate producer/read-model state from the UI;
- reinterpret Failed/Empty/WarmUp/Stale semantics;
- add cross-mode revision ordering;
- change schema/provenance semantics unless #230 explicitly owns such a change;
- change package pins;
- change Python version;
- change Streamlit version;
- alter JSON-over-stdio unless #230 explicitly requires a compatible change;
- alter Release 1.9 planning;
- modify dependency edges unless #230 explicitly owns them;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #230 before technical acceptance;
- add WP06 scaffolding.

If #230 requires a material consumer-contract choice not already defined, stop rather than inventing it.

---

# Phase 1 — Current-State and Design Proof

Record:

- branch
- local HEAD
- origin/main
- ahead/behind
- staged state
- tracked state
- relevant untracked inventory
- files/components likely affected by #230

Map each #230 acceptance criterion to:

- required presentation behavior
- current UI behavior
- WP04 field/state consumed
- gap
- minimum proposed change
- focused validation

If multiple materially different UI/consumer contracts remain equally valid and #230 does not resolve them, stop.

---

# Phase 2 — Minimal Consumer Implementation

Implement only the smallest coherent WP05 change.

Rules:

1. consume WP04 envelope rather than reconstructing backend state;
2. preserve contract-version checking;
3. preserve revision-kind distinction;
4. render explicit presentation states truthfully;
5. avoid business/domain recomputation;
6. avoid SQLite/provider access;
7. use existing Streamlit patterns;
8. add tests alongside behavior;
9. avoid speculative WP06 controls or architecture.

---

# Phase 3 — State Rendering

If #230 requires state-specific rendering, implement exact behavior for:

## Ready
- render current data/features/pipeline evidence required by #230.

## Empty
- render explicit no-data state;
- do not represent as failure.

## WarmUp
- render not-ready/warm-up state;
- show current/required count if #230 requires it;
- do not fabricate feature values.

## Stale
- render last complete payload with explicit stale indication;
- do not invent wall-clock age policy unless #230 defines it.

## Failed
- render safe failure category/message/recoverability only;
- do not expose stack traces/raw exceptions;
- preserve last-good payload rendering only if contract/#230 requires it.

Do not infer state from null fields.

---

# Phase 4 — Revision / Refresh Consumption

Consume WP04 revisions exactly.

Historical:
- `HistoricalPresentation`

Replay:
- `ReplayLogicalTick`

Rules:
- compare only within same kind/context;
- equal equivalent state may render idempotently;
- older state must not replace newer consumer state if WP05 owns a local cache;
- equal conflicting identity must surface as integrity error if #230 requires consumer conflict handling;
- no Historical-vs-Replay numeric compare.

If #230 does not authorize a UI-side cache, do not invent one.

---

# Phase 5 — Bounded Window Rendering

If #230 renders the observation window:

- use the producer-provided bounded 64-row window;
- preserve oldest→newest semantic order unless visualization API requires a display transform that does not change data semantics;
- do not fetch additional history from SQLite/provider;
- do not grow an independent UI-side history.

No unbounded consumer accumulation.

---

# Phase 6 — Feature / Pipeline Evidence Rendering

Use already-computed WP04 fields.

Do not recompute feature values.

Do not reinterpret pipeline status.

Render only fields required by #230.

Avoid exposing raw internal diagnostics.

---

# Phase 7 — Polling / Refresh

Only if #230 explicitly defines polling/refresh behavior:

- implement minimum cadence/trigger;
- read latest complete envelope atomically;
- do not mutate producer;
- do not infer source freshness from wall clock unless #230 defines it;
- preserve revision semantics.

If timing/cadence is not defined and materially affects acceptance, stop and request a narrow definition authority rather than inventing a polling policy.

---

# Phase 8 — Contract-Version Handling

The consumer must recognize:

`aiq-visualization-read-model-v1`

If #230 requires version validation:

- known version accepted;
- unknown version rejected/fails safely;
- no reinterpretation of unknown contracts.

Do not create a generalized version-negotiation framework.

---

# Phase 9 — Focused WP05 Tests

Derive exact tests from #230.

At minimum, where applicable, cover:

- Ready rendering;
- Empty rendering;
- WarmUp rendering;
- Stale rendering;
- Failed rendering;
- safe failure output;
- no fabricated feature values;
- bounded window consumption;
- no extra UI-side history;
- contract version accept/reject;
- Historical revision consumption;
- Replay revision consumption;
- no cross-mode comparison;
- immutable consumer behavior;
- no SQLite/provider access;
- no feature recomputation.

Prefer deterministic tests of presentation logic over brittle pixel/screenshot assertions unless #230 explicitly requires screenshots.

---

# Phase 10 — Predecessor Compatibility

Revalidate WP04 behavior where consumer integration touches it:

- contract version;
- bounded window;
- Historical revision;
- Replay logical ticks;
- Ready/Empty/WarmUp/Stale/Failed;
- immutable publication;
- no consumer mutation.

Also preserve WP02/WP03 semantics where affected.

Do not weaken predecessor tests.

---

# Phase 11 — Build and Full Regression

Run established repository build.

Require:
- 0 errors;
- report warning count exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Pre-WP05 baseline:

**297/297 passing**

Capture:
- exact command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

Higher count is expected with WP05 tests.

An unexplained lower count is a blocker.

---

# Phase 12 — Diff and Scope Audit

Classify every changed file as:

- WP05 Streamlit/UI implementation;
- WP05 read-model consumer/reader;
- WP05 presentation-state rendering;
- WP05 revision/version handling;
- WP05 test;
- WP05-required documentation/config artifact.

Prove:

- no WP06 implementation;
- no producer/read-model redesign;
- no SQLite/provider access from UI unless explicitly authorized;
- no feature recomputation;
- no schema change;
- no pipeline algorithm change;
- no package/Python/Streamlit version change;
- no cross-mode ordering;
- no unbounded UI history;
- no authority/control file mutation.

Anything unexplained blocks acceptance.

---

# Phase 13 — Technical Acceptance Gate

Before GitHub mutation, enumerate every #230 acceptance criterion.

For each:
- implementation evidence
- test evidence
- PASS/FAIL

Additionally require PASS for:
- truthful WP04 envelope consumption;
- state rendering;
- bounded-window consumption;
- revision-kind handling;
- no feature recomputation;
- no SQLite/provider bypass;
- no WP06 leakage;
- focused tests;
- predecessor tests;
- build;
- full regression;
- scope audit.

If any fails, leave #230 Open / Backlog.

---

# Phase 14 — WP05 GitHub Lifecycle Finalization

Only after technical acceptance:

1. read #230 current state;
2. confirm established completion convention;
3. add one concise completion/evidence comment if required;
4. transition Project Status from Backlog to authoritative completed state;
5. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area
6. close #230;
7. keep milestone #58 open;
8. read back all mutations.

Do not modify #231.

---

# Expected Post-Completion State

After successful WP05 closure:

- #226–#230 closed/completed;
- #231–#237 remain open and untouched;
- milestone #58 remains open;
- canonical milestone counts become:
  - **7 open**
  - **5 closed**
- raw GitHub closed count may additionally include #225;
- dependency chain remains intact;
- successful WP05 regression count becomes WP06 predecessor baseline;
- WP06 #231 becomes next eligible;
- WP06 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- #230 cannot be read;
- #230 acceptance criteria are ambiguous;
- predecessor gate fails;
- UI requires a new consumer/polling contract not defined by #230;
- implementation requires producer/read-model redesign;
- implementation requires SQLite/provider bypass;
- feature recomputation becomes necessary;
- WP06+ scope is required;
- focused tests fail;
- predecessor tests regress;
- build fails;
- full regression fails;
- diff audit reveals unexplained scope;
- GitHub mutation fails or cannot be proven.

On stop:

- preserve valid WP05 work;
- do not broaden authority;
- report exact blocker and last proven state;
- leave #230 open unless technical acceptance fully passed and lifecycle mutation alone failed.

---

# Success Criteria

WP05 succeeds only when:

- every #230 acceptance criterion passes;
- WP04 envelope is consumed truthfully;
- Ready/Empty/WarmUp/Stale/Failed semantics are rendered as required;
- bounded window is consumed without unbounded UI history;
- Historical and Replay revisions remain distinct;
- no cross-mode numeric ordering is introduced;
- no feature recomputation occurs;
- no direct SQLite/provider access occurs unless explicitly authorized;
- focused tests pass;
- predecessor behavior remains intact;
- build passes;
- full regression passes;
- final diff remains WP05-scoped;
- #230 is completed and closed;
- milestone #58 remains open;
- #231–#237 remain untouched;
- dependency chain intact;
- WP06 remains unstarted.

---

# Required Completion Report

Return:

## WP05 authority
- #230 exact title;
- objective;
- acceptance criteria.

## Implementation
- files changed;
- UI/consumer behavior summary;
- state rendering;
- revision/version handling;
- refresh/polling behavior if applicable;
- tests added/changed.

## Acceptance
For every #230 criterion:
- evidence;
- PASS/FAIL.

## Validation
- focused WP05 tests;
- predecessor-sensitive tests;
- build errors/warnings;
- full regression command and exact counts.

## Scope proof
- diff classification;
- no WP06 work;
- no producer/read-model redesign;
- no SQLite/provider bypass;
- no feature recomputation;
- no unauthorized foundation/planning changes.

## GitHub lifecycle
- #230 before/after;
- Project Status before/after;
- completion comment;
- milestone #58 canonical counts;
- #231–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP06 — #231`

Do not authorize or execute WP06.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 COMPLETE`

On blocker:

`RELEASE 1.9 WP05 BLOCKED`

Do not emit success unless every technical acceptance and lifecycle requirement is freshly proven.

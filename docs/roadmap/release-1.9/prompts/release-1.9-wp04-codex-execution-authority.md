# Release 1.9 — WP04 — Codex Execution Authority

## Authority

This document grants execution authority for **Release 1.9 WP04 — canonical GitHub issue #229**.

Accepted predecessor state:

- WP01 #226: Closed / Done
- WP02 #227: Closed / Done
- WP03 #228: Closed / Done
- SQLite schema baseline: **v4**
- `dataset_snapshots.source_authority` and `experiment_results.source_authority` allow only `{0,1}`
- `0 = AcceptedRelease11HistoricalObservations`
- `1 = Release19SimulatedLiveReplay`
- v3→v4 migration preserves historical evidence transactionally
- Infrastructure tests: **144/144 passed**
- Application tests: **122/122 passed**
- full regression: **290/290 passed**
- build: **0 errors / 0 warnings**
- milestone #58 remains open
- canonical milestone state: **9 open / 3 closed**
- #229–#237 remain open
- WP04 has not started
- dependency chain remains intact
- #225 and protected milestones remain preserved

The current authoritative predecessor regression baseline for WP04 is **290/290 passing**.

This authority is for **WP04 only**. It does not authorize WP05 or later work.

---

## Objective

Execute WP04 exactly as defined by:

1. canonical issue **#229**
2. the accepted Release 1.9 definition/manifest
3. completed WP01–WP03 predecessor evidence
4. the current repository architecture and schema-v4 baseline

Read #229 first. Do not infer WP04 scope from sequence or prior work packages.

---

## Phase 0 — Load WP04 Authority

Before mutation:

1. Read #229 completely.
2. Read the Release 1.9 definition/manifest sections governing WP04.
3. Extract:
   - exact title
   - objective
   - deliverables
   - acceptance criteria
   - authorized/expected paths
   - architectural constraints
   - schema/persistence constraints, if any
   - test requirements
   - configuration/DI requirements, if any
   - documentation requirements
   - lifecycle expectations
   - predecessor/dependency requirements
4. Read repository code/tests relevant to those criteria.
5. Build an explicit acceptance checklist.

Do not code until the checklist is unambiguous.

If #229 materially conflicts with the accepted Release 1.9 authority, stop.

---

## Predecessor Gate

Before any WP04 mutation, freshly prove:

- #226 closed/completed
- #227 closed/completed
- #228 closed/completed
- #229 open and canonical WP04
- exactly one Project #2 item for #229
- #229 fields remain:
  - Status = Backlog
  - Priority = P1
  - Release = 1.9
  - authoritative Area
- milestone #58 remains open
- dependency edge WP03 → WP04 exists with correct direction semantics
- #230–#237 remain open
- repository has no unexpected tracked/staged changes
- no residual or partial WP04 implementation exists

If this gate fails, stop.

---

## Current Technical Baseline

WP04 inherits:

- CPython 3.13.15 x64 in isolated `.venv`
- NumPy 2.5.1
- pandas 3.0.5
- scikit-learn 1.9.0
- Streamlit 1.61.1
- governed one-shot JSON-over-stdio boundary
- SQLite schema **v4**
- historical source authority `0`
- Replay source authority `1`
- canonical five-stage `ExecuteCanonical` pipeline
- completed WP02 Replay semantics
- completed WP03 Worker Replay composition and persistence
- full regression **290/290**
- build 0 errors / 0 warnings

Do not regress or redesign completed predecessor behavior unless #229 explicitly requires a compatible extension.

Do not use older 281/287/288 totals as the expected current baseline.

---

## Scope Discipline

### Permitted

WP04 may:

- inspect any repository code/tests needed to understand #229
- modify the minimum files necessary to satisfy #229
- extend contracts only where #229 explicitly requires semantics not safely expressible otherwise
- add/update focused tests
- update configuration/DI only if #229 requires it
- update persistence/schema only if #229 explicitly owns such a change and Release 1.9 authority permits it
- add/update WP04-specific documentation when required
- run focused validation
- run predecessor-sensitive regression
- run the full authoritative test suite
- finalize #229 lifecycle state only after technical acceptance

### Forbidden

Do not:

- implement WP05 or later work
- perform unrelated refactors
- redesign WP02 Replay semantics for preference
- redesign WP03 schema-v4/provenance semantics for preference
- broaden source-authority domain beyond governed values unless #229 explicitly authorizes it
- change package pins unless #229 explicitly requires it
- change Python version
- change Streamlit version
- alter JSON-over-stdio unless #229 explicitly requires a compatible change
- alter Release 1.9 planning
- create replacement WP issues
- modify dependency edges unless #229 explicitly owns that change
- modify #225
- modify protected milestones #59/#60/#50/#51/#61
- close #229 before technical acceptance
- add future-WP scaffolding

If satisfying #229 requires broader authority, stop and report the exact boundary.

---

## Phase 1 — Current-State and Design Proof

Record before mutation:

- branch
- local HEAD
- origin/main
- ahead/behind
- staged state
- tracked state
- relevant untracked inventory
- schema `user_version`
- files/components likely affected by #229

Map each #229 acceptance criterion to:

- required behavior
- existing implementation
- gap
- minimum proposed change
- focused validation

If more than one materially different architecture remains equally valid and #229 does not resolve it, stop rather than guessing.

---

## Phase 2 — Minimal Implementation

Implement only the smallest coherent change satisfying #229.

Rules:

1. preserve existing contracts when adequate
2. extend contracts only for explicit WP04 semantics
3. follow repository naming/layering conventions
4. preserve deterministic behavior where required
5. avoid hidden/global mutable state unless already governed
6. preserve cancellation/error conventions
7. preserve schema-v4/provenance behavior unless #229 explicitly changes it
8. add tests alongside behavior changes
9. avoid speculative abstraction and WP05 scaffolding

After each logical change, run the smallest relevant build/test target.

---

## Phase 3 — Focused Acceptance Validation

Prove every #229 acceptance criterion independently.

For each criterion record:

- criterion
- implementation evidence
- test/check command
- observed result
- PASS/FAIL

Compilation alone is not acceptance.

Test edge cases, failure modes, determinism, cancellation, persistence, provenance, serialization, configuration, identity, or boundary semantics whenever #229 requires them.

---

## Phase 4 — Predecessor Compatibility

Where #229 touches predecessor-owned components, explicitly revalidate:

### WP02 Replay semantics
- replay identity
- logical ticks
- restart/resume
- duplicate determinism
- cancellation
- bounds
- finite/end-of-replay

### WP03 semantics
- Worker Historical/Replay dispatch
- Dataset-boundary validation
- canonical `ExecuteCanonical` reuse
- schema-v4 migration/version behavior
- historical authority `0`
- Replay authority `1`
- Replay persistence/catalog distinction
- no Replay use of historical storage

Do not weaken predecessor tests.

---

## Phase 5 — Schema/Persistence Guard

If WP04 does **not** explicitly authorize schema changes:

- schema remains v4
- authority domain remains `{0,1}`
- existing migrations remain unchanged
- historical/Replay provenance remains truthful

If #229 explicitly requires schema/persistence evolution:

1. prove that requirement from #229
2. limit changes to the minimum authorized delta
3. preserve existing v4 migration semantics unless explicitly superseded
4. add focused migration/persistence tests
5. stop if a normative schema decision is missing

Do not invent schema semantics during implementation.

---

## Phase 6 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact command
- exit status
- passed
- failed
- skipped
- material warnings

Pre-WP04 baseline:

**290/290 passing**

A larger total is acceptable when explained by WP04-owned tests.

An unexplained lower count or removed tests is a blocker.

Also run the established repository build and report exact errors/warnings.

---

## Phase 7 — Diff and Scope Audit

Inspect the final diff before GitHub lifecycle mutation.

Classify every changed file as:

- WP04 production implementation
- WP04-required contract change
- directly required compatibility update
- WP04 test
- WP04 configuration/DI
- WP04 schema/persistence change explicitly authorized by #229
- WP04-required documentation

Anything else requires explicit #229 justification.

Prove:

- every changed file is attributable to #229
- no WP05+ implementation is present
- no predecessor architecture was redesigned without authority
- no unauthorized package/Python/Streamlit/protocol change occurred
- no unauthorized schema/provenance change occurred
- no planning authority/control file was altered
- no pre-existing user work was overwritten

Anything unexplained blocks acceptance.

---

## Phase 8 — Technical Acceptance Gate

Before any GitHub mutation, prove all #229 acceptance criteria PASS.

Also prove:

- focused tests pass
- affected predecessor tests pass
- build passes
- full regression passes
- final diff is strictly WP04-scoped
- no unresolved TODO/workaround is used to claim acceptance

If any gate fails, keep #229 Open / Backlog and stop.

---

## Phase 9 — WP04 GitHub Lifecycle Finalization

Only after technical acceptance:

1. read #229 current state
2. confirm established completion convention
3. add one concise completion/evidence comment if required
4. transition #229 Project Status from Backlog to authoritative completed state
5. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area
6. close #229
7. keep milestone #58 open
8. immediately read back all mutations

Do not modify #230.

---

## Expected Post-Completion State

After successful WP04 closure:

- #226 closed / Done
- #227 closed / Done
- #228 closed / Done
- #229 closed / authoritative completed status
- #230–#237 remain open and untouched
- milestone #58 remains open
- canonical milestone counts become:
  - **8 open**
  - **4 closed**
- raw GitHub closed count may additionally include #225
- dependency chain remains intact
- successful WP04 regression count becomes WP05 predecessor baseline
- WP05 #230 becomes next eligible
- WP05 must not start automatically

---

## Stop Conditions

Stop immediately if:

- #229 cannot be read
- #229 identity or acceptance criteria are ambiguous
- predecessor gate fails
- repository contains unsafe unexplained changes
- required work exceeds WP04 scope
- a necessary contract/schema decision is not authorized by #229
- implementation requires WP05+ behavior
- predecessor WP02/WP03 behavior regresses
- focused acceptance tests fail
- build fails
- full regression fails
- final diff contains unexplained changes
- GitHub lifecycle mutation fails or cannot be proven
- milestone #58 is unexpectedly closed
- protected objects appear changed

On stop:

- preserve evidence
- do not broaden scope
- distinguish pre-existing state from WP04-created changes
- report exact blocker and last proven state
- leave #229 open unless technical acceptance fully passed and lifecycle mutation alone failed

---

## Success Criteria

WP04 succeeds only when:

- every #229 acceptance criterion passes
- implementation is strictly WP04-scoped
- focused tests pass
- affected WP02/WP03 predecessor behavior remains correct
- build passes
- full authoritative regression passes
- final diff is fully classified and justified
- no unauthorized foundation/planning changes occur
- #229 receives required completion evidence
- #229 Project item reaches authoritative completed state
- #229 is closed
- milestone #58 remains open
- #230–#237 remain open and untouched
- dependency chain remains intact
- #225 and protected milestones remain unchanged
- WP05 has not started

---

## Required Completion Report

Return:

### WP04 authority
- #229 exact title
- objective
- acceptance criteria

### Implementation
- files changed
- concise behavior summary
- contract/configuration/DI/schema changes if any
- tests added/changed

### Acceptance
For every #229 criterion:
- evidence
- PASS/FAIL

### Validation
- focused test results
- predecessor-sensitive regression results
- build result
- full regression command
- exact passed/failed/skipped counts

### Scope proof
- final diff classification
- confirmation no WP05+ work
- confirmation no unauthorized foundation/planning changes

### GitHub lifecycle
- #229 state before/after
- Project Status before/after
- completion comment status
- milestone #58 canonical counts
- confirmation #230–#237 untouched

### Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP05 — #230`

Do not authorize or execute WP05.

---

## Terminal Markers

On success:

`RELEASE 1.9 WP04 COMPLETE`

On safe stop/blocker:

`RELEASE 1.9 WP04 BLOCKED`

Do not emit success unless all technical acceptance and lifecycle requirements are freshly proven.

# Release 1.9 — WP03 — Codex Execution Authority

## Authority

This document grants execution authority for:

**Release 1.9 WP03 — canonical GitHub issue #228**

Accepted predecessor state:

- WP01 #226: Closed / Done;
- WP02 #227: Closed / Done;
- WP02 minimum additive Application replay contract completed;
- deterministic Infrastructure replay adapter/configuration and DI registration completed;
- replay identity, logical ticks, restart/resume, duplicate determinism, cancellation, bounds, and explicit end-of-replay semantics tested;
- focused WP02 tests: 142/142 passed;
- full regression after WP02: **287/287 passed**;
- milestone #58 remains open;
- canonical milestone state: **10 open / 2 closed**;
- historical duplicate #225 remains separate from the canonical count;
- #228–#237 remain open and untouched;
- no WP03 work has started;
- no package, Python, schema, or planning mutation occurred in WP02;
- pre-existing Release 1.9 authority/control files remain preserved.

The current authoritative regression predecessor baseline for WP03 is **287/287 passing**.

This authority is for **WP03 only**.

It does not authorize WP04 or later work.

---

# Objective

Execute WP03 exactly as defined by:

1. canonical GitHub issue **#228**;
2. the accepted Release 1.9 planning/definition authority;
3. completed WP01 and WP02 predecessor evidence;
4. the current repository architecture and test conventions.

Do not infer WP03's implementation from its sequence number or from prior WPs.

Read #228 first and derive the exact scope.

---

# Phase 0 — Load WP03 Authority

Before mutation:

1. Read #228 completely.
2. Read the Release 1.9 definition/manifest sections governing WP03.
3. Extract:
   - exact title;
   - objective;
   - deliverables;
   - acceptance criteria;
   - authorized/expected paths;
   - architectural constraints;
   - test requirements;
   - configuration/DI requirements if any;
   - documentation requirements;
   - lifecycle expectations;
   - predecessor/dependency requirements.
4. Read repository code/tests relevant to those criteria.
5. Build an explicit acceptance checklist.

If #228 is ambiguous or conflicts materially with accepted Release 1.9 authority, stop.

Do not code until the acceptance checklist is clear.

---

# Predecessor Gate

Before any WP03 mutation, freshly prove:

- #226 is closed / completed;
- #227 is closed / completed;
- #228 is open;
- #228 is canonical WP03;
- #228 has exactly one Project #2 item;
- #228 fields remain:
  - Status = Backlog
  - Priority = P1
  - Release = 1.9
  - authoritative Area;
- milestone #58 remains open;
- dependency edge WP02 → WP03 exists with correct semantics;
- #229–#237 remain open;
- repository has no unexpected tracked/staged changes;
- no residual partial WP03 implementation exists.

If this gate fails, stop.

---

# Current Technical Baseline

WP03 inherits the completed WP02 repository state.

Relevant accepted foundations include:

- CPython 3.13.15 x64 in isolated `.venv`;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- SQLite schema v3;
- governed one-shot JSON-over-stdio boundary;
- completed replay contract and deterministic replay Infrastructure from WP02;
- current full regression baseline: **287/287 passing**.

Do not regress or redesign completed WP02 semantics unless #228 explicitly requires a compatible extension.

Do not use the historical 281-test WP01 count as the expected current baseline.

---

# Scope Discipline

## Permitted

WP03 may:

- inspect any repository code needed to understand #228;
- modify the minimum files necessary to satisfy #228;
- extend existing contracts only when #228 explicitly requires semantics that cannot otherwise be represented safely;
- add/update focused tests;
- update configuration/DI only when required by #228;
- add/update WP03-specific documentation when required;
- run focused validation throughout implementation;
- run the complete authoritative regression before closure;
- finalize #228 in GitHub after technical acceptance is proven.

## Forbidden

Do not:

- implement WP04 or later work;
- perform unrelated refactors;
- redesign the WP02 replay contract merely for preference;
- change package pins unless #228 explicitly requires it;
- change Python version;
- change schema version unless #228 explicitly owns such a migration;
- change Streamlit version;
- alter the one-shot JSON-over-stdio boundary unless #228 explicitly requires and authorizes a compatible change;
- alter Release 1.9 planning;
- create replacement WP issues;
- modify dependency edges unless #228 explicitly owns that change;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #228 before technical acceptance passes;
- silently broaden scope to solve future WP requirements.

---

# Phase 1 — Current-State and Design Proof

Record before mutation:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged state;
- tracked state;
- relevant untracked inventory;
- files/components likely affected by #228.

Then map each #228 acceptance criterion to:

- required behavior;
- existing implementation;
- gap;
- minimum proposed change;
- focused validation.

If a required semantic crosses an architectural boundary not authorized by #228, stop rather than hiding it behind implementation state.

---

# Phase 2 — Minimal Implementation

Implement only the smallest coherent change satisfying #228.

Rules:

1. preserve existing contracts when adequate;
2. extend contracts only when required for explicit acceptance semantics;
3. follow established repository naming and architecture;
4. preserve deterministic behavior where expected;
5. avoid hidden/global mutable state unless existing architecture explicitly governs it;
6. keep error/cancellation behavior consistent with existing conventions;
7. add tests alongside behavioral changes;
8. avoid speculative abstractions and future-WP scaffolding.

After each logical change, run the smallest relevant build/test target.

---

# Phase 3 — Focused Acceptance Validation

Prove every #228 acceptance criterion independently.

For each criterion record:

- criterion;
- implementation evidence;
- test/check command;
- observed result;
- PASS/FAIL.

Do not treat compilation alone as acceptance.

If #228 defines edge cases, failure modes, determinism, cancellation, restart, bounds, identity, persistence, serialization, or configuration semantics, test those explicitly as applicable.

---

# Phase 4 — Compatibility and Predecessor Regression

Verify WP03 has not broken completed WP02 behavior.

Where #228 touches components introduced or amended in WP02, run the relevant replay/contract tests explicitly.

At minimum preserve, where applicable:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- explicit finite/end-of-replay behavior.

Do not weaken predecessor tests to accommodate WP03.

---

# Phase 5 — Full Regression

Run the authoritative suite:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

Pre-WP03 baseline is:

**287/287 passing**

A larger total is acceptable when explained by WP03-owned tests.

Any removed tests or lower total require explicit justification from #228; otherwise stop.

Do not suppress failures.

---

# Phase 6 — Diff and Scope Audit

Inspect the final diff before GitHub lifecycle mutation.

Classify every changed file as:

- WP03 production implementation;
- WP03-required contract change;
- compatibility update directly caused by WP03;
- WP03 test;
- WP03 configuration/DI;
- WP03-required documentation.

Anything else requires explicit #228 justification.

Prove:

- every changed file is attributable to #228;
- no WP04+ implementation is present;
- completed WP02 behavior remains governed;
- no unauthorized package/Python/schema/version change occurred;
- no planning authority was mutated;
- no pre-existing user work was overwritten.

If unauthorized WP03-session changes exist and ownership is certain, remove only those changes.

---

# Phase 7 — Technical Acceptance Gate

Before any GitHub mutation, prove all #228 acceptance criteria PASS.

Also prove:

- focused tests pass;
- predecessor-sensitive tests pass;
- full regression passes;
- final diff is WP03-scoped;
- no unresolved TODO/workaround is being used to claim acceptance.

If any gate fails, keep #228 open / Backlog and stop.

---

# Phase 8 — WP03 GitHub Lifecycle Finalization

Only after technical acceptance:

1. read #228 current state;
2. discover/confirm the established completion convention;
3. add one concise evidence comment if required;
4. transition #228 Project Status from Backlog to the authoritative completed state;
5. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. immediately read back all mutations.

Do not modify #229.

---

# Expected Post-Completion State

After successful WP03 closure:

- #226 = closed / Done;
- #227 = closed / Done;
- #228 = closed / authoritative completed status;
- #229–#237 remain open and untouched;
- milestone #58 remains open;
- canonical milestone counts become:
  - **9 open**
  - **3 closed**;
- raw GitHub counters may additionally include historical duplicate #225;
- canonical 11-edge dependency chain remains intact;
- WP04 #229 becomes next eligible;
- WP04 must not start automatically.

The successful WP03 regression count becomes the predecessor test baseline for WP04.

---

# Stop Conditions

Stop immediately if:

- #228 cannot be read;
- #228 identity or acceptance criteria are ambiguous;
- predecessor gate fails;
- repository contains unsafe unexplained changes;
- required work exceeds WP03 scope;
- a necessary architectural/contract change is not authorized by #228;
- implementation requires WP04+ behavior;
- focused acceptance validation fails;
- predecessor WP02 behavior regresses;
- full regression fails;
- final diff contains unexplained changes;
- GitHub lifecycle mutation fails or cannot be proven;
- milestone #58 is unexpectedly closed;
- protected objects appear changed.

On stop:

- preserve evidence;
- do not broaden scope;
- distinguish pre-existing state from WP03-created changes;
- report the exact blocker and last proven state;
- leave #228 unclosed unless technical acceptance had already been fully proven and lifecycle mutation itself is the only blocker.

---

# Success Criteria

WP03 succeeds only when:

- every #228 acceptance criterion passes;
- implementation is strictly WP03-scoped;
- focused tests pass;
- affected WP02 predecessor behavior remains correct;
- full authoritative regression passes;
- final diff is fully classified and justified;
- no unauthorized foundation/planning changes occur;
- #228 receives required completion evidence;
- #228 Project item reaches authoritative completed state;
- #228 is closed;
- milestone #58 remains open;
- #229–#237 remain open and untouched;
- dependency chain remains intact;
- #225 and protected milestones remain unchanged;
- WP04 has not started.

---

# Required Completion Report

Return:

## WP03 authority
- #228 exact title;
- objective;
- acceptance criteria.

## Implementation
- files changed;
- concise behavior summary;
- contract/configuration/DI changes if any;
- tests added/changed.

## Acceptance
For every #228 criterion:
- evidence;
- PASS/FAIL.

## Validation
- focused test results;
- predecessor-sensitive regression results;
- full regression command;
- exact passed/failed/skipped counts.

## Scope proof
- final diff classification;
- confirmation no WP04+ work;
- confirmation no unauthorized foundation or planning changes.

## GitHub lifecycle
- #228 state before/after;
- Project Status before/after;
- completion comment status;
- milestone #58 canonical counts;
- confirmation #229–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP03 COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP03 BLOCKED`

Do not emit the success marker unless all technical acceptance and lifecycle requirements are freshly proven.

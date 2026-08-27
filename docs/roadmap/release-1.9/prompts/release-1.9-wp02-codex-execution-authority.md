# Release 1.9 — WP02 — Codex Execution Authority

## Authority

This document grants execution authority for the next eligible Release 1.9 work package:

**WP02 — canonical GitHub issue #227**

WP01 is complete in both technical and GitHub lifecycle terms.

Accepted predecessor state:

- WP01 #226: Closed / Done;
- milestone #58: open;
- canonical milestone state: 11 open / 1 closed;
- raw GitHub milestone counters: 11 open / 2 closed because historical duplicate #225 remains closed;
- WP02 #227 through WP12 #237 remain open and untouched;
- canonical dependency chain remains 11/11;
- repository/Git/Python/package/schema state unchanged;
- WP02 has not started.

This authority is for **WP02 only**.

It does not authorize WP03 or later work.

---

# Objective

Execute WP02 exactly as defined by:

1. canonical issue **#227**;
2. the accepted Release 1.9 planning/definition authority;
3. the completed WP01 predecessor evidence;
4. repository-local implementation and test conventions.

Do not infer WP02 scope from this prompt when #227 or the Release 1.9 definition provides a more specific requirement.

Before implementation, read #227 completely and extract:

- exact title;
- objective;
- scope;
- deliverables;
- acceptance criteria;
- permitted/expected files;
- test requirements;
- documentation requirements;
- GitHub completion expectations;
- predecessor/dependency constraints.

If #227 is ambiguous or conflicts materially with the accepted Release 1.9 authority, stop.

---

# Predecessor Gate

Before any WP02 mutation, freshly prove:

- #226 is closed;
- #226 Project Status is `Done`;
- #227 is open;
- #227 is the canonical WP02;
- #227 depends on completed WP01 under the accepted dependency semantics;
- #227 has exactly one Project #2 item;
- #227 Project fields remain:
  - Status = Backlog
  - Priority = P1
  - Release = 1.9
  - authoritative Area;
- milestone #58 remains open;
- repository baseline is not unexpectedly dirty;
- staged paths = 0.

Do not rerun the full WP01 preflight unless #227 explicitly requires some predecessor evidence to be refreshed.

---

# Baseline Foundations

The accepted Release 1.9 execution baseline includes:

- branch: `main`;
- predecessor baseline commit:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- Python: CPython 3.13.15 x64 in isolated `.venv`;
- exact direct pins:
  - NumPy 2.5.1
  - pandas 3.0.5
  - scikit-learn 1.9.0
  - Streamlit 1.61.1;
- SQLite schema v3;
- governed one-shot JSON-over-stdio boundary;
- authoritative test baseline:
  `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`
  with 281/281 passing at WP01 completion.

Treat these as predecessor facts.

Freshly verify only the subset necessary for WP02 execution and final regression proof.

Do not change any foundation unless #227 explicitly requires that change as part of WP02.

---

# Scope Discipline

## Permitted

WP02 may:

- inspect repository code and tests needed to understand #227;
- modify only files necessary to satisfy #227;
- add or update tests required by #227;
- add/update WP02-specific documentation when required;
- run focused tests during development;
- run the authoritative regression suite before completion;
- update #227 lifecycle state only after implementation acceptance is proven.

## Forbidden

Do not:

- implement WP03 or later work;
- opportunistically refactor unrelated code;
- change package versions unless #227 explicitly requires it;
- change Python version;
- change schema version unless #227 explicitly requires a WP02 schema change;
- change the one-shot JSON-over-stdio boundary unless #227 explicitly requires it;
- change Streamlit version unless #227 explicitly requires it;
- alter Release 1.9 planning;
- create or delete work-package issues;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- alter dependency edges unless #227 explicitly owns such a change;
- silently broaden scope because a nearby improvement seems desirable.

If required work exceeds WP02 authority, stop and report the exact boundary.

---

# Execution Protocol

## Phase 0 — Load WP02 authority

Read:

- #227;
- relevant Release 1.9 definition files;
- relevant repository architecture/tests;
- WP01 completion evidence only as needed.

Build an explicit implementation checklist from #227 acceptance criteria.

Do not code until the checklist is clear.

---

## Phase 1 — Current-state proof

Before mutation, record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- tracked/staged/untracked state;
- files likely to be affected;
- focused baseline test result if #227 requires one.

If unexpected unrelated changes exist, stop unless they are clearly outside WP02 and cannot affect execution safety.

---

## Phase 2 — Minimal implementation

Implement only what #227 requires.

Rules:

1. prefer the smallest coherent change;
2. preserve existing architecture unless #227 requires a deliberate extension;
3. preserve established public contracts unless #227 explicitly changes them;
4. avoid speculative abstractions;
5. add tests alongside behavior changes;
6. keep error handling consistent with repository conventions;
7. preserve determinism where the existing system expects it;
8. do not bundle cleanup/refactors unrelated to acceptance criteria.

After each logical change, run the smallest relevant test target.

---

## Phase 3 — Focused acceptance validation

Run the tests and checks directly tied to #227.

Prove each acceptance criterion individually.

For every criterion, report:

- criterion;
- evidence;
- pass/fail.

Do not mark WP02 complete based solely on code inspection.

---

## Phase 4 — Full regression validation

Before lifecycle closure, run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

The historical WP01 baseline was 281/281.

If WP02 legitimately adds tests, the new count may exceed 281.

A different test count is acceptable only when explained by WP02-owned test additions/removals and all required tests pass.

Do not delete or weaken tests merely to make regression pass.

---

## Phase 5 — Diff and scope audit

Before declaring implementation complete, inspect the final diff.

Prove:

- every changed file is attributable to #227;
- no unrelated file was modified;
- no unintended package/Python/schema/protocol/version change occurred;
- no WP03+ implementation slipped into the diff;
- no Release 1.9 planning authority file was modified unless #227 explicitly requires it.

If scope creep is found, revert only the unauthorized WP02-session changes if ownership is certain.

Do not disturb pre-existing user work.

---

## Phase 6 — WP02 GitHub lifecycle finalization

Only after all technical acceptance criteria pass:

1. determine the established completion convention from WP01 and current Project #2 configuration;
2. add one concise completion/evidence comment if required;
3. update #227 Project Status from `Backlog` to the authoritative completed value;
4. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area;
5. close #227;
6. keep milestone #58 open.

Immediately read back every mutation.

Do not assume the completion status name if current Project configuration differs; discover it first.

---

# Post-Completion Expected State

After successful WP02 closure:

- #226 = closed / Done;
- #227 = closed / authoritative completed Project state;
- #228–#237 remain open and untouched;
- milestone #58 remains open;
- canonical milestone state becomes:
  - 10 open
  - 2 closed;
- raw GitHub milestone closed count may include historical duplicate #225 in addition to canonical closures;
- the 11-edge dependency chain remains intact;
- WP03 #228 becomes the next eligible work package;
- WP03 must not start automatically.

---

# Stop Conditions

Stop immediately if:

- #227 cannot be read;
- #227 identity is ambiguous;
- WP01 is not actually closed/completed;
- #227 acceptance criteria are ambiguous;
- #227 conflicts materially with Release 1.9 authority;
- repository contains unsafe unexplained changes;
- implementation would require WP03+ scope;
- required package/Python/schema/protocol changes are not explicitly authorized by #227;
- focused acceptance tests fail and resolving them would exceed WP02 scope;
- full regression fails for reasons not owned by WP02;
- GitHub lifecycle mutation cannot be proven;
- milestone #58 is unexpectedly closed;
- protected objects appear changed.

On stop:

- preserve evidence;
- do not broaden authority;
- report exact blocker;
- identify last proven state;
- distinguish pre-existing state from WP02-created changes.

---

# Success Criteria

WP02 succeeds only when:

- every #227 acceptance criterion passes;
- implementation is limited to WP02;
- focused validation passes;
- full authoritative regression passes;
- final diff contains only justified WP02 changes;
- no unauthorized foundation change occurred;
- #227 receives required completion evidence;
- #227 Project item is in the authoritative completed state;
- #227 is closed;
- milestone #58 remains open;
- #228–#237 remain open and untouched;
- dependency chain remains intact;
- protected milestones and #225 remain unchanged;
- WP03 has not started.

---

# Required Completion Report

Return:

## WP02 authority
- #227 exact title;
- objective;
- acceptance criteria.

## Implementation
- files changed;
- concise behavior summary;
- tests added/changed.

## Validation
- focused test/check results;
- full regression command and exact counts.

## Scope proof
- final diff summary;
- foundation changes, if any and explicitly authorized;
- confirmation that WP03+ work was not performed.

## GitHub lifecycle
- #227 before/after state;
- Project Status before/after;
- completion comment status;
- milestone #58 canonical counts after closure;
- confirmation #228–#237 remain untouched.

## Next eligibility
State:

`NEXT ELIGIBLE WORK PACKAGE: WP03 — #228`

Do not authorize or execute WP03.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP02 COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP02 BLOCKED`

Do not emit the success marker unless all #227 acceptance criteria and lifecycle-finalization requirements are proven.

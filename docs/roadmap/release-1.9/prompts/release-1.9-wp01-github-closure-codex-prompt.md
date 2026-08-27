# Release 1.9 — WP01 GitHub Closure — Codex Authority

## Authority

This document grants **narrow GitHub lifecycle-finalization authority** for:

**Release 1.9 WP01 — Release & Repository Preflight — canonical issue #226**

WP01 technical acceptance has already succeeded.

Accepted completion evidence includes:

- repository on `main`;
- local `HEAD` and `origin/main` at:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- ahead/behind `0/0`;
- tracked/staged changes `0/0`;
- 9 baseline Release 1.9 authority/definition files plus 2 WP01 execution controls preserved;
- CPython 3.13.15 x64 in isolated `.venv`;
- exact pins:
  - NumPy 2.5.1
  - pandas 3.0.5
  - scikit-learn 1.9.0
  - Streamlit 1.61.1
- SQLite schema v3;
- governed one-shot JSON-over-stdio boundary;
- `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo` passed 281/281, with 0 failed and 0 skipped;
- canonical Release 1.9 issues #226–#237 and 12/12 Project #2 items verified;
- all 11 dependency edges verified;
- #225 remains the closed historical duplicate and is excluded from the canonical set;
- protected milestones #59/#60/#50/#51/#61 unchanged;
- WP01 preflight mutations: zero;
- WP02 has not started.

Terminal technical result:

`RELEASE 1.9 WP01 RELEASE & REPOSITORY PREFLIGHT COMPLETE`

The sole purpose of this authority is to finalize WP01's **GitHub lifecycle state** correctly.

This is not authority to rerun WP01.

This is not authority to execute WP02.

This is not authority to close milestone #58.

---

# Objective

Reconcile canonical issue **#226** from technically accepted but still-open/planning state into the repository's authoritative completed GitHub state.

The operation must preserve:

- WP01 completion evidence;
- milestone #58 as an open Release 1.9 milestone;
- WP02–WP12 as untouched future work;
- the canonical dependency chain;
- Project #2 taxonomy and unrelated items;
- repository/Git/Python/package/schema state.

---

# Critical Distinction

**Issue #226** is the WP01 work-package issue.

**Milestone #58** is the Release 1.9 milestone containing the full WP01–WP12 sequence.

Do not confuse them.

This authority may close **issue #226** if governance requires issue closure.

This authority must **not close milestone #58**.

After successful WP01 finalization, milestone #58 must remain open because WP02–WP12 remain outstanding.

---

# Read Before Write

Before any mutation:

1. Read issue #226 completely.
2. Read its current state, milestone, assignees, labels, comments, dependencies, and Project #2 membership.
3. Read the relevant Release 1.9 governance/definition authority establishing how a completed WP is represented.
4. Read Project #2's actual Status field options and determine the exact authoritative completed-state option.
5. Read at least one trustworthy predecessor/completed work-package example if needed to resolve lifecycle convention.
6. Read milestone #58.
7. Read canonical WP02 #227 sufficiently to prove it remains the next dependent work package.
8. Confirm the WP01 technical-completion evidence exists and is not contradicted by current state.

Do not mutate anything until the required completion representation is proven.

---

# Completion-State Discovery

Do **not** assume the Project #2 completed status is named `Done`.

Discover the actual configured Status options and the repository's accepted governance convention.

The authoritative completed transition may involve some or all of:

- a specific Project #2 Status value;
- a completion comment;
- issue closure;
- preservation of milestone #58 association;
- other metadata explicitly required by existing governance.

Use the minimum mutation set required by the proven convention.

If completion semantics are ambiguous, stop.

---

# Permitted Mutations

Only the minimum mutations required to finalize WP01 are authorized.

Potentially permitted, when proven required:

- add one concise completion/evidence comment to #226;
- update #226's existing Project #2 item Status to the authoritative completed value;
- close issue #226;
- preserve #226 in milestone #58.

No other mutation is authorized unless it is strictly necessary to express the already-proven WP01 completion state under established governance.

---

# Explicitly Forbidden

Do not:

- close milestone #58;
- reopen or modify #225;
- execute WP02;
- modify #227–#237;
- change WP02's Project status;
- create issues;
- create Project items;
- recreate WP01;
- change Priority, Release, or Area unless a proven completion convention explicitly requires it;
- change dependency edges;
- alter Project taxonomy/options;
- modify protected milestones #59/#60/#50/#51/#61;
- modify repository files;
- stage or commit anything;
- modify Python;
- modify package pins;
- modify schema;
- rerun implementation;
- repair unrelated GitHub state;
- repeat the full WP01 technical preflight merely because #226 was left open.

This authority is lifecycle finalization only.

---

# Phase 1 — Fresh Pre-Mutation Proof

Immediately before mutation, prove:

## WP01

- #226 is the canonical WP01;
- #226 is currently open;
- #226 belongs to milestone #58;
- #226 has exactly one Project #2 item;
- its current Status is recorded;
- Priority = P1;
- Release = 1.9;
- Area remains the authoritative WP01 Area.

## Remaining canonical WPs

Prove #227–#237 remain open and canonical.

Do not rewrite them.

## Historical duplicate

Prove:

- #225 remains closed;
- #225 has no Project #2 item;
- #225 remains excluded from the canonical set.

## Milestone

Prove milestone #58 is open.

Record both:

- canonical WP count;
- GitHub raw open/closed milestone counters.

Before WP01 closure, the expected canonical state is:

- 12 open / 0 closed canonical WPs.

The raw GitHub counter may be:

- 12 open / 1 closed

because historical duplicate #225 remains attached and closed.

## Dependency chain

Prove the existing 11-edge canonical chain still exists.

This is a read-only guard.

## Repository guard

Confirm:

- no unexpected tracked changes;
- staged paths = 0.

Do not perform a full WP01 validation rerun.

---

# Phase 2 — Completion Evidence Comment

Determine whether established governance requires a completion comment.

If required, add **one concise evidence comment** to #226.

The comment should summarize the already-proven result rather than rerun or invent evidence.

It should include enough information to make closure auditable, such as:

- WP01 acceptance satisfied;
- predecessor/repository alignment;
- Python/package/schema/protocol foundations proven;
- tests 281/281 passed;
- GitHub planning/dependencies verified;
- WP01 mutations zero;
- WP02 not started.

Do not paste excessive logs.

If a completion comment already exists and fully satisfies the convention, do not duplicate it.

Immediately read back the comment after creation.

---

# Phase 3 — Project #2 Completion Transition

Using the already-existing Project #2 item for #226:

1. prove its item identity;
2. prove the authoritative completed Status option and option ID;
3. update **only Status** unless governance explicitly requires another completion-field mutation;
4. immediately read back the item.

Prove after mutation:

- exactly one Project item still exists for #226;
- Status = authoritative completed state;
- Priority = P1;
- Release = 1.9;
- Area = authoritative WP01 Area.

Do not create another Project item.

---

# Phase 4 — Close Canonical Issue #226

Only after the Project completion state is proven:

1. confirm #226 is still the canonical WP01;
2. confirm milestone #58 remains assigned;
3. close issue #226 using the repository's established closure convention;
4. immediately read back issue state.

Prove:

- #226 = closed;
- #226 remains associated with milestone #58;
- #226 remains assigned as governance requires;
- Project #2 item remains present and completed;
- no unintended field changed.

Do not close milestone #58.

---

# Phase 5 — Post-Closure Canonical Proof

Freshly verify the entire relevant state.

## WP01

Prove:

- #226 canonical;
- closed;
- milestone #58;
- exactly one Project #2 item;
- authoritative completed Status;
- P1;
- Release 1.9;
- authoritative Area.

## WP02–WP12

Prove #227–#237:

- remain open;
- remain canonical;
- were not modified by this authority.

Do not transition WP02.

## Historical duplicate

Prove #225 remains:

- closed;
- excluded;
- without Project #2 item.

## Milestone #58

Prove milestone #58 remains **OPEN**.

Canonical Release 1.9 WP counts must now be:

- **11 open**
- **1 closed**

because WP01 is complete.

GitHub's raw milestone counters may now be:

- **11 open**
- **2 closed**

because the raw closed count also includes historical duplicate #225.

This raw/canonical distinction is expected.

Do not attempt to remove #225 from the milestone merely to make raw counters equal canonical counters unless separate authority explicitly requires that.

## Dependency chain

Freshly verify the same 11 edges:

1. WP01 → WP02
2. WP02 → WP03
3. WP03 → WP04
4. WP04 → WP05
5. WP05 → WP06
6. WP06 → WP07
7. WP07 → WP08
8. WP08 → WP09
9. WP09 → WP10
10. WP10 → WP11
11. WP11 → WP12

Closing WP01 must not alter the dependency graph.

## Protected state

Prove no mutation to:

- #225 beyond read-only verification;
- #227–#237;
- #59/#60/#50/#51/#61;
- Project taxonomy/options;
- repository/Git working tree;
- Python/package/schema state.

---

# Mutation Discipline

For every mutation:

1. prove current object identity;
2. prove the mutation is required by completion governance;
3. mutate the minimum field/object;
4. immediately read back;
5. compare with expected state;
6. stop on divergence.

Do not batch mutations in a way that prevents attribution.

Do not interpret a failed API/CLI command as success.

Any uncertain mutation outcome is a blocker requiring fresh read-back before further action.

---

# Stop Conditions

Stop immediately if:

- #226 identity is ambiguous;
- #226 is unexpectedly already closed with inconsistent Project state;
- milestone #58 is unexpectedly closed;
- Project #2 item identity is ambiguous;
- completed Status option cannot be proven;
- governance does not establish whether #226 should be closed;
- Priority/Release/Area unexpectedly differ;
- WP02–WP12 state unexpectedly differs;
- #225 state unexpectedly differs;
- dependency chain differs;
- repository has unexpected tracked/staged mutations;
- a mutation fails or its outcome cannot be proven;
- completing closure would require broader planning or implementation authority.

On stop:

- do not perform speculative cleanup;
- preserve evidence;
- report exact last proven state;
- report exact blocker;
- identify any mutation whose outcome is uncertain.

---

# Success Criteria

WP01 GitHub closure succeeds only when freshly proven:

- #226 is canonical WP01;
- WP01 technical acceptance evidence remains valid;
- required completion evidence/comment exists;
- #226 has exactly one Project #2 item;
- Project Status equals the authoritative completed state;
- Priority = P1;
- Release = 1.9;
- Area = authoritative WP01 Area;
- #226 is closed;
- #226 remains associated with milestone #58;
- milestone #58 remains open;
- canonical milestone state = 11 open / 1 closed;
- raw milestone state is correctly reported, including #225;
- #225 remains closed historical duplicate with no Project item;
- #227–#237 remain open and untouched;
- 11/11 dependency edges remain intact;
- protected milestones #59/#60/#50/#51/#61 remain unchanged;
- Project taxonomy remains unchanged;
- repository/Git/Python/package/schema state remains unchanged;
- WP02 has not started.

---

# Required Completion Report

Return a concise evidence-based report containing:

## WP01 lifecycle transition

- #226 state before;
- Project Status before;
- completion convention discovered;
- mutations performed;
- #226 state after;
- Project Status after.

## Evidence comment

State whether a completion comment was added or an existing sufficient comment was preserved.

## Milestone proof

Report separately:

- milestone #58 state;
- canonical WP counts;
- raw GitHub milestone counts;
- explanation of #225's effect on raw closed count.

## Remaining work packages

Confirm #227–#237 remain open and untouched.

State:

`NEXT ELIGIBLE WORK PACKAGE: WP02 — #227`

Do not authorize or execute it.

## Dependency proof

Confirm 11/11 edges remain unchanged.

## Protected-state proof

Confirm:

- #225 preserved;
- #59/#60/#50/#51/#61 preserved;
- Project taxonomy unchanged;
- repository/Git/Python/package/schema state unchanged.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP01 GITHUB CLOSURE COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP01 GITHUB CLOSURE BLOCKED`

Do not emit the success marker unless all success criteria are freshly proven.

---

# After Success

This authority is exhausted.

Do not rerun WP01.

Do not begin WP02 automatically.

WP02 #227 becomes eligible for a **separate, explicit execution authority**.

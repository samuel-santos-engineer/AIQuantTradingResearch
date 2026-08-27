# Release 1.9 — Partial GitHub Planning Reconciliation — Codex Authority

## Authority

This document grants **narrow corrective authority** to reconcile the proven partial GitHub planning state for Release 1.9.

This is **not** authority to restart Release 1.9 planning from the beginning.

This is **not** authority to implement Release 1.9.

This is **not** authority to modify repository source, tests, packages, schemas, Python assets, documentation authority files, or local Git state.

The sole purpose of this authority is to:

1. resolve the duplicate WP01 identity;
2. reconcile the existing WP01–WP10 GitHub planning objects;
3. prove and reconcile dependency state from scratch;
4. create WP11–WP12 only after WP01–WP10 are proven canonical and unique;
5. prove the final Release 1.9 GitHub planning state.

If any required identity, field, dependency, milestone, Project item, or invariant cannot be proven safely, **stop immediately without guessing**.

---

## Proven Starting State

Treat the following as the accepted starting state. Do not rediscover it by mutation.

### Milestone

- Release 1.9 milestone: **#58**
- Milestone #58 is open.
- Proven count before reconciliation: **11 open / 0 closed issues**.

### Existing work-package issues

| WP | Issue | Proven state |
|---|---:|---|
| WP01 | #225 | One Project item; `Status=Todo`; Priority/Release/Area unset |
| WP01 | #226 | Duplicate WP01 identity; Project fields fully populated |
| WP02 | #227 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP03 | #228 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP04 | #229 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP05 | #230 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP06 | #231 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP07 | #232 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP08 | #233 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP09 | #234 | One Project item; Backlog / P1 / Release 1.9 / intended Area |
| WP10 | #235 | One Project item; `Status=Todo`; Priority/Release/Area unset |
| WP11 | — | Not created |
| WP12 | — | Not created |

### Dependency state

- Dependency state is **unknown**.
- Previous dependency mutation success is not proven.
- Do not assume any dependency edge exists.
- Read and prove dependency state before reconciliation.

### Repository/local state

The failed GitHub planning operation did **not** change repository implementation state.

Preserve all of the following:

- repository files unchanged;
- Git working-tree semantics unchanged except for the already-known untracked Release 1.9 authority/definition files;
- staged paths remain **zero**;
- Python state unchanged;
- package state unchanged;
- schema state unchanged;
- all **nine existing untracked Release 1.9 authority/definition files** remain present and unmodified.

Do not add, stage, commit, delete, rename, rewrite, format, or otherwise mutate those nine files.

---

## Root Cause Being Corrected

The blocker was:

> Invalid GitHub CLI argument construction caused pre-create identity searches to fail silently, producing duplicate WP01 #226.

The corrective workflow must therefore enforce these invariants:

> **A failed identity-search command is a blocking error. It must never be interpreted as “no existing issue found.”**

and:

> **Before any issue create, the identity search itself must complete successfully and explicitly prove zero canonical matches.**

A nonzero process exit, malformed response, parse failure, authentication error, pagination uncertainty, API error, GraphQL error, quoting/escaping failure, or ambiguous result is **not** a zero-match result.

---

## Scope

### Permitted GitHub mutations

Only the minimum GitHub mutations necessary to achieve the final success state are authorized:

- inspect #225 and #226 deeply enough to select the canonical WP01;
- close the confirmed accidental duplicate WP01 issue with an explicit reconciliation/duplicate comment;
- remove the accidental duplicate's Project item if needed to achieve one canonical Project item for WP01;
- reconcile missing/incorrect Project fields on canonical existing WP issues;
- create WP11 and WP12 only when their absence is proven;
- add/reconcile Release 1.9 dependency edges;
- remove incorrect Release 1.9 dependency edges if and only if their incorrectness is proven;
- read back and verify every changed GitHub object.

### Explicitly forbidden

Do **not**:

- rerun the original planning authority from its beginning;
- recreate WP02–WP10;
- delete GitHub issues;
- create a replacement WP01 before reconciling #225/#226;
- create WP11 or WP12 before WP01–WP10 uniqueness is proven;
- mutate milestones other than what is strictly necessary to ensure canonical Release 1.9 issues belong to #58;
- modify milestones #59, #60, #50, #51, or #61;
- modify unrelated GitHub issues;
- change repository files;
- stage or commit anything;
- modify packages, dependencies, Python assets, schemas, migrations, runtime code, tests, or documentation;
- implement any work package;
- infer success from prior command output without fresh read-back proof;
- treat command failure as absence.

---

## Authoritative Release 1.9 Planning Definition

Use the existing nine Release 1.9 authority/definition files as the semantic authority for:

- WP01–WP12 titles;
- WP identities;
- issue bodies;
- intended Areas;
- ordering;
- dependency chain;
- other Release 1.9 planning metadata.

These files are **read-only authority** for this reconciliation.

Do not rewrite them.

If the accepted definition cannot be read or produces an ambiguity that affects mutation safety, stop.

---

## Required Final Planning Shape

The canonical final planning set must contain exactly:

- **12 canonical Release 1.9 WP issues**
- **12 canonical Project items**
- **one issue identity per WP01–WP12**
- no duplicate canonical WP identities
- all canonical issues assigned to milestone **#58**
- all canonical Project items:
  - `Status = Backlog`
  - `Priority = P1`
  - `Release = Release 1.9`
  - `Area =` the intended Area from the accepted Release 1.9 definition
- exactly **11 Release 1.9 dependency edges**
- exact linear chain:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12`

Interpret the arrow consistently with the repository's accepted dependency semantics. Do not reverse edge direction accidentally. Prove the semantic direction from the existing planning convention/API representation before mutating edges.

Milestone #58 must finish:

- open;
- **12 open / 0 closed canonical Release 1.9 issues**.

The accidental duplicate may be closed as part of reconciliation and must not count as a canonical Release 1.9 work-package issue.

---

# Execution Protocol

## Phase 0 — Safety and environment proof

Before any mutation:

1. Confirm the repository and current branch/worktree context.
2. Prove:
   - staged paths = 0;
   - the nine existing untracked Release 1.9 authority/definition files remain present;
   - no repository mutation has occurred since the blocked run that requires cleanup.
3. Record the exact GitHub repository identity.
4. Confirm GitHub CLI/API authentication.
5. Confirm milestone #58 exists and is open.
6. Confirm milestones #59, #60, #50, #51, and #61 exist only as protected untouched objects for this authority.
7. Determine the exact GitHub Project and field identifiers required for read/write operations.
8. Establish a robust command/API invocation method that does not rely on unsafe concatenated shell argument strings.

### Hard stop

Stop immediately if the repository/local proof differs materially from the accepted starting state.

Do not attempt cleanup.

---

## Phase 1 — Load authoritative WP definition

Read the existing Release 1.9 planning/definition authority files.

Build an in-memory reconciliation table for WP01–WP12 containing at minimum:

- WP identity;
- authoritative title;
- authoritative issue body/signature needed to distinguish identity;
- intended Area;
- intended milestone;
- intended Status;
- intended Priority;
- intended Release;
- predecessor/successor dependency expectations.

Do not mutate any GitHub object in this phase.

### Identity rule

WP identity must be based on the accepted semantic planning definition.

Do not identify a WP solely by:

- issue number;
- creation time;
- loose title substring;
- Project position;
- current field population.

---

## Phase 2 — Prove current issue identity state

Perform fresh searches for WP01–WP12.

### Search execution invariant

Every pre-create search must separately prove:

1. command/API execution success;
2. parse success;
3. complete result scope;
4. explicit match count.

A failed or ambiguous search is a blocker.

### Required proof

For each WP:

- enumerate all candidate issues matching the authoritative identity;
- inspect title, body, milestone, state, Project membership, and relevant metadata;
- classify candidate count.

Expected starting shape:

- WP01 → two candidates: #225 and #226;
- WP02–WP10 → exactly one canonical candidate each: #227–#235;
- WP11–WP12 → zero candidates.

If WP02–WP10 show duplicates or WP11/WP12 unexpectedly exist, do not blindly continue. Reconcile only if the canonical identity can be proven safely under this authority; otherwise stop with exact evidence.

---

## Phase 3 — Resolve duplicate WP01

Treat #225 and #226 as candidates for one authoritative WP01 identity.

Read both completely enough to compare:

- title;
- issue body;
- milestone;
- open/closed state;
- assignee(s), if relevant to the accepted planning convention;
- Project item membership;
- Project field values;
- comments;
- dependency relationships;
- creation timestamps;
- any other metadata necessary to establish whether one is accidental or semantically distinct.

### Canonical selection rule

Select the canonical WP01 based on semantic agreement with the accepted Release 1.9 definition.

Do **not** select merely by lower issue number.

Given the proven starting state, #226 is expected to be the likely canonical object because its Project state is fully populated, but this is only a hypothesis until semantic proof is complete.

### If #226 is proven canonical and #225 accidental

Perform the minimum historical-preserving correction:

1. ensure #226 is open and belongs to milestone #58;
2. ensure #226 has exactly one canonical Project item;
3. reconcile #226 Project fields to the authoritative values only if necessary;
4. add a concise comment to #225 explaining it was created accidentally during Release 1.9 GitHub planning reconciliation because the identity pre-check failed;
5. close #225 as duplicate/reconciled;
6. remove #225's Project item if required to leave exactly one canonical WP01 Project item;
7. do not delete #225.

### If #225 is proven canonical instead

Apply the symmetric minimum correction to #226.

### Hard stop

If canonical WP01 cannot be proven unambiguously, stop.

Do not close either candidate.

---

## Phase 4 — Reconcile WP02–WP09 without recreation

For #227–#234:

1. prove each maps uniquely to WP02–WP09 respectively;
2. prove each is open;
3. prove milestone #58;
4. prove exactly one Project item for each;
5. read back:
   - Status
   - Priority
   - Release
   - Area

Expected accepted values:

- Status = Backlog
- Priority = P1
- Release = Release 1.9
- Area = authoritative intended Area

Do not rewrite already-correct values.

Do not recreate any of these issues.

If a field is unexpectedly incorrect, correct only that proven divergence and immediately read it back.

---

## Phase 5 — Reconcile WP10 in place

For #235:

1. prove #235 is the unique canonical WP10;
2. prove it is open;
3. prove milestone #58;
4. prove exactly one Project item;
5. read current fields.

Expected starting divergence:

- Status = Todo
- Priority unset
- Release unset
- Area unset

Reconcile the existing Project item in place to:

- Status = Backlog
- Priority = P1
- Release = Release 1.9
- Area = authoritative WP10 Area

Do not create another WP10.

Immediately read back all four values.

---

## Phase 6 — WP01–WP10 canonical uniqueness gate

Before creating WP11 or WP12, prove all of the following simultaneously:

- exactly one canonical issue for each WP01–WP10;
- each canonical issue is open;
- each canonical issue belongs to milestone #58;
- exactly one canonical Project item per WP;
- every canonical Project item has:
  - Backlog
  - P1
  - Release 1.9
  - correct Area;
- the accidental WP01 duplicate is no longer part of the canonical planning set;
- no other duplicate identities are present.

### Hard gate

If any invariant fails, stop.

WP11–WP12 creation is forbidden until this gate passes.

---

## Phase 7 — Safely create WP11 and WP12

Process WP11 and WP12 one at a time.

For each WP:

### A. Pre-create identity proof

Run the authoritative identity search.

Require:

- successful process/API execution;
- successful parsing;
- complete search scope;
- **exactly zero canonical matches**.

Only an explicit successful zero-match result authorizes creation.

### B. Create exactly one issue

Create the issue using the authoritative Release 1.9 definition:

- exact intended title;
- exact intended issue body;
- milestone #58.

Avoid shell-string argument construction that can silently break quoting.

Prefer structured API input, argument arrays, files/stdin, or another invocation form where title/body/query values are passed as discrete parameters.

### C. Immediate issue read-back

Prove:

- issue exists;
- issue is open;
- exact WP identity;
- intended milestone #58;
- title/body semantics match authority.

### D. Create/add exactly one Project item

Add the issue to the intended Project once.

Immediately obtain and record its Project item identifier.

### E. Set Project fields

Set:

- Status = Backlog
- Priority = P1
- Release = Release 1.9
- Area = authoritative intended Area

### F. Immediate Project read-back

Prove all four fields and exactly one Project item.

### G. Re-run identity search

Re-run the same identity search after creation.

Require exactly one canonical match.

If more than one appears, stop immediately and report the duplicate state. Do not perform speculative cleanup.

Only after WP11 passes all checks may WP12 be processed.

---

## Phase 8 — Prove dependency state from scratch

Do not trust any dependency mutation from the blocked run.

Read dependency relationships for all canonical WP01–WP12 issues.

Construct the observed Release 1.9 edge set.

Normalize it carefully so direction semantics are explicit.

Report the observed edge set before reconciliation.

### Expected authoritative edge set

Exactly 11 edges forming one linear chain:

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

### Reconciliation rules

- Preserve an already-correct edge.
- Add a missing authoritative edge.
- Remove an incorrect Release 1.9 edge only when its incorrectness and ownership are proven.
- Do not touch dependencies involving unrelated releases/issues unless they are proven accidental products of this failed Release 1.9 planning run and correction is necessary for the exact final chain.
- Do not duplicate an existing edge.
- After each mutation, read back the affected relationship.

### Hard stop

If dependency direction cannot be mapped confidently to `WP01 → WP02 → ...` semantics, stop before dependency mutation.

---

## Phase 9 — Full final proof

After all permitted mutations, perform a fresh read-only verification.

Do not rely on cached/local reconciliation tables.

### Issue proof

Prove exactly one canonical open issue for each:

- WP01
- WP02
- WP03
- WP04
- WP05
- WP06
- WP07
- WP08
- WP09
- WP10
- WP11
- WP12

Prove the canonical issue numbers explicitly.

Prove zero duplicate canonical identities.

### Project proof

Prove exactly 12 canonical Project items, one per WP.

For every WP prove:

- Status = Backlog
- Priority = P1
- Release = Release 1.9
- correct Area

### Dependency proof

Freshly enumerate dependency state.

Prove:

- exactly 11 authoritative Release 1.9 edges;
- no missing edge;
- no extra edge within the canonical Release 1.9 planning chain;
- exact WP01 → WP02 → ... → WP12 linear sequence.

### Milestone proof

Prove milestone #58:

- open;
- 12 canonical Release 1.9 issues open;
- 0 canonical Release 1.9 issues closed.

The closed accidental duplicate must be clearly excluded from the canonical work-package count.

### Protected-object proof

Prove no mutation to:

- milestone #59;
- milestone #60;
- milestone #50;
- milestone #51;
- milestone #61.

### Repository/local proof

Prove again:

- staged paths = 0;
- no repository implementation files changed;
- no package changes;
- no Python changes;
- no schema changes;
- all nine existing untracked Release 1.9 authority/definition files remain present and unchanged.

---

# Mutation Discipline

For every GitHub mutation:

1. read current state;
2. prove identity;
3. mutate only the minimum necessary property;
4. immediately read back;
5. compare to authoritative expected state;
6. stop on divergence.

Do not batch unrelated corrective mutations when doing so would weaken attribution or verification.

Prefer idempotent operations where GitHub supports them.

Do not suppress stderr or exit status for identity searches.

Do not use patterns equivalent to:

- `command || true`
- broad exception swallowing
- treating empty parsed output after a failed command as zero matches
- concatenating user/content values into one shell command string when structured argument passing is available.

---

# Stop Conditions

Stop immediately and make no further mutations if any of these occurs:

- identity search exits unsuccessfully;
- identity search output cannot be parsed;
- result completeness cannot be proven;
- canonical WP identity is ambiguous;
- unexpected duplicate state cannot be safely resolved;
- issue creation returns an uncertain result;
- Project item identity is ambiguous;
- Project field mutation cannot be read back;
- dependency direction is ambiguous;
- dependency mutation cannot be verified;
- protected milestone/object appears mutated;
- local repository state changes unexpectedly;
- staged paths become nonzero;
- any of the nine Release 1.9 authority/definition files changes;
- package/Python/schema state changes;
- an operation would require authority broader than this document.

On stop:

- do not attempt broad cleanup;
- preserve evidence;
- report the exact last proven state;
- report the exact blocker;
- identify any mutation whose outcome is uncertain.

---

# Success Criteria

This authority succeeds only when all conditions below are proven:

- exactly 12 canonical Release 1.9 WP issues;
- exactly 12 canonical Project items;
- zero duplicate canonical WP identities;
- WP01–WP12 all open;
- WP01–WP12 all in milestone #58;
- every canonical Project item:
  - Status = Backlog
  - Priority = P1
  - Release = Release 1.9
  - correct authoritative Area;
- exactly 11 dependency edges;
- dependency chain exactly:
  `WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12`;
- milestone #58 open with 12 canonical open / 0 canonical closed;
- accidental duplicate WP01 preserved historically but excluded from the canonical planning set;
- #59/#60/#50/#51/#61 untouched;
- repository implementation state unchanged;
- Git staged paths = 0;
- Python/package/schema state unchanged;
- all nine existing untracked Release 1.9 authority/definition files preserved unchanged;
- no implementation performed.

---

# Required Completion Report

Return a concise but evidence-based report containing:

## Canonical issue map

| WP | Canonical issue | State | Milestone | Project item | Status | Priority | Release | Area |
|---|---:|---|---|---|---|---|---|---|

Include WP01–WP12.

## Duplicate reconciliation

State:

- which WP01 issue was selected canonical;
- which issue was confirmed accidental;
- what historical-preserving action was taken;
- proof that only one canonical WP01 Project item remains.

## Dependency proof

List the final 11 edges explicitly.

## Milestone proof

State milestone #58 final canonical open/closed count.

## Protected-state proof

State that #59/#60/#50/#51/#61 were untouched.

## Repository/local proof

State:

- staged paths count;
- repository mutation status;
- nine untracked Release 1.9 authority/definition files preserved;
- Python/package/schema state unchanged.

## Commands/API reliability note

State how identity searches were invoked safely and how successful zero-match results were distinguished from command failure.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 GITHUB PLANNING RECONCILIATION COMPLETE`

On a safe stop/blocker, end with exactly:

`RELEASE 1.9 GITHUB PLANNING RECONCILIATION BLOCKED`

Do not emit the success marker unless every success criterion has been freshly proven.

---

## After Success

Do **not** rerun the original Release 1.9 GitHub planning authority.

Do **not** begin implementation automatically.

The reconciled live GitHub state becomes the authoritative planning starting point for the next explicitly authorized Release 1.9 action.

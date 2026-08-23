# Release 1.6 Project #2 Release-Field Restoration & Reconciliation Authority

## 1. Purpose

This prompt is the sole corrective authority for the blocked Release 1.6 GitHub planning run in:

`samuel-santos-engineer/AIQuantTradingResearch`

The original Release 1.6 GitHub Planning Authority successfully:

- reconciled milestone #47;
- created issues #182–#195;
- established 14 Project #2 items;
- created exactly one `Release = 1.6` option;

but adding the new single-select option regenerated Project #2 `Release` option IDs and cleared existing predecessor Release selections.

This corrective authority exists only to:

1. reconstruct and restore predecessor Project #2 `Release` values from authoritative GitHub evidence;
2. prove the restoration;
3. finish configuration of #182–#195;
4. complete the Release 1.6 GitHub planning state.

It does not authorize WP01 execution, repository implementation, schema changes, staging, commits, branches, pushes, PRs, releases, tags, or Release 1.7 work.

---

## 2. Authoritative Starting State

Reconcile rather than assume.

Expected repository baseline:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- no tracked/staged repository changes caused by GitHub planning;
- SQLite implementation remains schema v2;
- permanent test baseline remains 238/238.

Expected GitHub planning state:

- milestone #47:
  - OPEN;
  - title `Phase 4 - Release 1.6: Durable Experiment Evidence Foundation`;
  - 14 open / 0 closed;
  - issues #182–#195 only;
- issues #182–#195:
  - OPEN;
  - assigned to `samuel-santos-engineer`;
  - correct existing label;
  - milestone #47;
  - exactly one Project #2 item each;
- Project #2 contains exactly one semantic `Release = 1.6` option;
- predecessor Project items have lost their `Release` selections;
- #182–#195 have not yet received final Status/Priority/Release/Area configuration;
- WP01 has not started.

If this starting state materially differs, stop before mutation.

---

## 3. Preservation Principle

No predecessor Project value may be guessed.

Every restored `Release` assignment must be derived from authoritative GitHub evidence that existed independently of the corrupted Project field state.

Acceptable authoritative sources include, in descending preference:

1. issue milestone association where the milestone uniquely and authoritatively maps to a release;
2. issue title/body explicitly identifying Release X.Y;
3. accepted predecessor release planning issue inventory;
4. closed milestone issue membership;
5. merged release PR / release planning evidence when it uniquely maps the issue to a release;
6. repository-governed release planning artifacts if needed to disambiguate.

Do not use:

- current cleared Project `Release` field;
- issue number ranges alone;
- memory/assumption;
- inferred chronology without corroboration;
- milestone title text when the milestone itself is known legacy/ambiguous and issue membership contradicts it.

If any predecessor item's release cannot be proven uniquely, stop before restoring that item and report the ambiguity.

---

## 4. Restoration Scope

The corrective authority may restore `Release` values only for Project #2 items whose predecessor Release assignment can be proven.

Do not mutate unrelated Project fields on predecessor items.

For predecessor items, the only authorized Project-field mutation is:

`Release`

Do not change predecessor:

- Status;
- Priority;
- Area;
- iteration;
- dates;
- assignee;
- issue lifecycle;
- milestone;
- labels.

---

## 5. Releases Eligible for Reconstruction

Inspect all existing Project #2 items and identify predecessor release-scoped items.

At minimum expect historical release families including:

- Release 1.1;
- Release 1.2;
- Release 1.3;
- Release 1.4;
- Release 1.5.

Also inspect any earlier release values/options that existed before the field-ID regeneration.

Do not invent an option for a historical release unless authoritative evidence proves that semantic release option must exist and restoring it is necessary.

If GitHub single-select option regeneration removed or replaced predecessor semantic options, reconstruct the exact semantic option set required by authoritative predecessor Project evidence before assigning items.

Do not alter option labels unnecessarily.

---

## 6. Milestone-to-Release Evidence Reconciliation

Build a read-only mapping from milestones to releases using authoritative historical state.

Known relevant milestones must be reconciled carefully.

Examples from accepted history include:

- milestone #54 — Release 1.3 accepted issue set #138–#151;
- milestone #45 — Release 1.4 accepted issue set #153–#166;
- milestone #46 — Release 1.5 accepted issue set #168–#180;
- milestone #47 — Release 1.6 issues #182–#195.

Legacy milestone #44 was historical Release 1.3 metadata but remained empty and was separately closed. It must not be used to remap unrelated items.

Use actual issue membership and accepted closure evidence, not milestone numbering alone.

Reconcile Release 1.1 and 1.2 milestone evidence from GitHub/repository truth before mutation.

---

## 7. Issue-Level Proof Table

Before any predecessor restoration mutation, construct an in-memory/read-only proof table containing for every predecessor Project item to be restored:

- Project item ID;
- issue number;
- issue title;
- issue state;
- milestone number/title if any;
- authoritative release;
- evidence source(s);
- confidence:
  - `Exact`
  - `Ambiguous`

Only `Exact` rows may be restored.

Report:

- predecessor items considered;
- Exact rows;
- Ambiguous rows.

Required before mutation:

`Ambiguous rows = 0`

If not zero, stop.

---

## 8. Release Option Set Reconciliation

Read the current Project #2 `Release` single-select field options.

Determine which semantic predecessor options are required by the exact proof table.

Required final option properties:

- each semantic release needed by predecessor items exists exactly once;
- `1.6` exists exactly once;
- duplicate semantic options: 0.

If a required predecessor semantic option is missing because the option-ID regeneration replaced the option set, recreate the missing semantic option only if the Project API supports adding it without deleting other options.

If adding options would again regenerate IDs and clear values, perform all option-set reconciliation before restoring any item values.

Do not repeatedly mutate the option set after restoration has begun.

If the available GitHub API cannot preserve a stable final option set, stop and report the limitation.

---

## 9. Snapshot Before Restoration

Immediately before the first restoration mutation, capture a read-only snapshot of:

- Project #2 Release option labels and IDs;
- every predecessor item in the proof table;
- every #182–#195 item;
- current Status/Priority/Release/Area for those items.

This snapshot is execution evidence.

Do not persist secrets.

Do not modify repository content to store the snapshot.

---

## 10. Predecessor Release Restoration

Restore predecessor `Release` values according to the exact proof table.

Mutation order:

1. finish Release option-set reconciliation;
2. restore predecessor items;
3. verify every restored predecessor value;
4. only then configure Release 1.6 items.

For each restored item:

- set exactly one `Release` value;
- read back immediately or in bounded batches;
- compare to authoritative proof table.

Do not continue to Release 1.6 item configuration if predecessor restoration is incomplete.

---

## 11. Restoration Completeness Gate

Before touching #182–#195 fields, require:

- all predecessor items identified as release-scoped have their exact semantic `Release` value restored;
- restored count equals exact proof-table count;
- wrong predecessor Release values: 0;
- missing predecessor Release values: 0;
- duplicate semantic Release options: 0;
- predecessor non-Release Project fields changed by this corrective run: 0.

If any condition fails, stop.

Do not partially configure Release 1.6 items.

---

## 12. Release 1.6 Project Item Configuration

Only after predecessor restoration passes, configure #182–#195.

Required final fields for all fourteen:

- Status: `Backlog`
- Priority: `P1`
- Release: `1.6`
- Area: authoritative value below

Authoritative Areas:

| WP | Issue | Area |
| --- | --- | --- |
| WP01 | #182 | Engineering |
| WP02 | #183 | Data |
| WP03 | #184 | Architecture |
| WP04 | #185 | Architecture |
| WP05 | #186 | Architecture |
| WP06 | #187 | Data |
| WP07 | #188 | Data |
| WP08 | #189 | Data |
| WP09 | #190 | Architecture |
| WP10 | #191 | Configuration |
| WP11 | #192 | Host |
| WP12 | #193 | Testing |
| WP13 | #194 | Documentation |
| WP14 | #195 | Engineering |

Use exact existing Project field option labels.

If `Backlog` does not currently exist but another status such as `Todo` exists, do not silently substitute. Reconcile against established Release 1.5 Project conventions and stop if the required Backlog option is unavailable.

Do not start WP01.

---

## 13. Release 1.6 Issue / Milestone Preservation

Do not recreate or duplicate issues #182–#195.

Do not change their:

- title;
- body;
- state;
- assignee;
- label;
- milestone;
- dependency metadata;

unless read-back proves the prior planning run did not establish the authoritative value and the correction is necessary to complete the originally authorized planning state.

Default authorized correction under this prompt is Project fields only.

Milestone #47 must remain OPEN with 14 open / 0 closed.

---

## 14. Project Membership Preservation

Require:

- #182–#195 each have exactly one Project #2 item;
- duplicate Project items: 0.

Do not create a new item when automation already created one.

Do not remove unrelated Project items.

---

## 15. Dependency Preservation

Read back the accepted linear Release 1.6 dependency model from issue bodies/metadata:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13 → WP14`

Do not mutate dependencies unless the prior planning run failed to establish them.

Dependency drift must be 0.

---

## 16. Repository Mutation Prohibition

Repository-content mutation budget:

`0`

Do not:

- edit files;
- stage;
- commit;
- create branch;
- push;
- create PR;
- implement schema v3;
- execute WP01;
- begin Release 1.7.

Expected Release 1.6 planning/governance files may remain untracked and unchanged.

---

## 17. GitHub Mutation Budget

Authorized GitHub mutations are limited to:

1. Release single-select option-set reconciliation needed to restore predecessor semantics;
2. predecessor Project item `Release` restoration;
3. #182–#195 Project fields:
   - Status
   - Priority
   - Release
   - Area;
4. narrowly necessary correction of a missing originally-authorized Project field value if read-back proves the previous run stopped before applying it.

Not authorized:

- milestone lifecycle changes;
- issue lifecycle changes;
- issue creation/deletion;
- predecessor Project Status/Priority/Area changes;
- Release 1.7 objects;
- repository mutation.

---

## 18. Full Project Read-Back

After all mutations, read back the complete affected Project state.

### Predecessor preservation

Report:

- predecessor release-scoped items expected;
- predecessor Release values restored;
- missing: 0;
- wrong: 0;
- ambiguous: 0;
- non-Release predecessor field mutations: 0.

### Release 1.6

For #182–#195 require:

- Project membership: 14/14;
- duplicates: 0;
- Status Backlog: 14/14;
- Priority P1: 14/14;
- Release 1.6: 14/14;
- authoritative Area: 14/14.

### Release option set

Require:

- each required semantic predecessor release option exists once;
- `1.6` exists once;
- semantic duplicate options: 0.

---

## 19. GitHub Planning Completion Read-Back

Re-read all Release 1.6 planning objects.

Require:

### Milestone #47
- OPEN;
- correct title;
- 14 open / 0 closed;
- issues #182–#195 only.

### Issues #182–#195
- 14/14 OPEN;
- assignee correct;
- label correct;
- milestone correct;
- dependency correct;
- Project membership exactly once;
- Status Backlog;
- Priority P1;
- Release 1.6;
- Area correct.

### No extras
- duplicate WP identities: 0;
- WP15+: 0;
- Release 1.6 branch/PR: 0;
- WP01 started: NO;
- Release 1.7 work: 0.

---

## 20. Technical Repository Validation

After Project reconciliation, run:

`eng/verify.ps1 -Configuration Release`

Expected:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- warnings/errors: 0/0
- formatting: PASS
- Gitleaks: PASS

Also require:

- `git diff --check`: PASS
- `git diff --cached --check`: PASS
- staged paths: 0
- tracked repository changes: 0
- schema implementation remains v2
- database/WAL/SHM/journal residue: 0
- provider/network execution: 0

---

## 21. Stop Conditions

Stop without widening scope if:

- any predecessor release assignment remains ambiguous;
- a required semantic Release option cannot be reconstructed exactly;
- option-set mutation would repeatedly clear restored values;
- predecessor non-Release Project fields were altered unexpectedly;
- Release 1.6 issues/milestone drifted materially;
- Backlog/P1/Area options are unavailable or ambiguous;
- repository baseline drifted;
- WP01 has started;
- Release 1.7 work exists.

Report the smallest further corrective authority required.

Do not guess.

---

## 22. Required Execution Report

Report:

1. executive summary;
2. authority reviewed;
3. starting repository/GitHub state;
4. Release option-set state;
5. predecessor item inventory;
6. issue-level proof-table summary;
7. ambiguous-row count;
8. milestone-to-release reconciliation;
9. option-set reconciliation mutations;
10. predecessor Release restoration count;
11. predecessor restoration read-back;
12. predecessor non-Release field preservation;
13. #182–#195 Project configuration;
14. Release 1.6 Area mapping verification;
15. Project membership/duplicate state;
16. dependency preservation;
17. milestone #47 final state;
18. issue #182–#195 final state;
19. Release option final state;
20. repository mutation accounting;
21. GitHub mutation accounting;
22. canonical technical validation;
23. findings/blockers;
24. final decision;
25. next authorized work package.

---

## 23. Completion Marker

If every gate passes, end exactly:

`RELEASE 1.6 PROJECT RELEASE-FIELD RESTORATION COMPLETE`

Then:

`RELEASE 1.6 GITHUB PLANNING COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

WP01 must remain OPEN / Backlog at completion.

If blocked, end:

`RELEASE 1.6 PROJECT RELEASE-FIELD RESTORATION BLOCKED`

and identify the smallest corrective authority required.

# Release 1.2 GitHub Planning --- Authoritative Codex Execution Prompt

## 1. Authority

This file is the authoritative GitHub-planning execution contract for:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

Repository:

``` text
samuel-santos-engineer/AIQuantTradingResearch
```

This authority begins only after the accepted Release 1.2 definition and
the accepted governance pair exist:

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
```

This authority may create/reconcile Release 1.2 GitHub planning only.

It does **not** authorize WP01 implementation.

Successful terminal:

``` text
RELEASE 1.2 GITHUB PLANNING COMPLETE
```

Blocked terminal:

``` text
RELEASE 1.2 GITHUB PLANNING BLOCKED
```

------------------------------------------------------------------------

## 2. Governing Authorities

Before mutation, read completely:

1.  `docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md`
2.  `docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md`
3.  the accepted Release 1.2 planning/definition result;
4.  the accepted Release 1.1 post-merge closure result;
5.  current repository/GitHub truth;
6.  current GitHub Project #2 schema and item conventions.

The execution plan is authoritative for:

-   Release identity;
-   work-package titles;
-   exact dependency graph;
-   lifecycle sequence;
-   implementation boundaries.

The file manifest is authoritative for:

-   governance artifact paths;
-   prompt naming;
-   allowed repository-side planning artifacts;
-   ownership boundaries.

GitHub planning must reflect those authorities exactly.

------------------------------------------------------------------------

## 3. Accepted Predecessor State

Verify before any mutation:

``` text
Release 1.1 terminal = RELEASE 1.1 CLOSED
PR #120 = MERGED
Milestone #52 = CLOSED
Issues #103–#118 = Closed / Done
Active Release 1.2 authoritative planning = 0
Release 1.2 implementation = NOT STARTED
```

Legacy milestone:

``` text
#43 — Phase 3 - Release 1.2: Storage
```

must be treated as historical only.

Expected accepted historical state:

``` text
state = CLOSED
issues = 0
```

Do not reuse milestone #43 as the authoritative Release 1.2 milestone.

------------------------------------------------------------------------

## 4. Planning Objective

Create the complete GitHub planning representation for:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

The final planning model must contain:

``` text
1 authoritative Release 1.2 milestone
16 authoritative WP issues
16 Project #2 items
exact dependency graph
exact issue ownership/labels
Status = Backlog
Priority = P1
Release = 1.2
Area = mapped per this authority
WP01 implementation = NOT STARTED
Release 1.3 implementation = NOT STARTED
```

No WP17+ issue is authorized.

No lifecycle-gate issue is authorized.

------------------------------------------------------------------------

## 5. Mutation Scope

This authority may mutate only the following GitHub planning surfaces
when all preflight gates pass:

-   the existing Project #2 `Release` field by adding exactly one `1.2`
    option **if and only if it is absent**;
-   legacy milestone #43 state from OPEN → CLOSED **only if it is still
    empty and all identity checks match**;
-   one new authoritative Release 1.2 milestone;
-   sixteen new/reconciled Release 1.2 WP issues;
-   issue milestone assignments;
-   issue labels;
-   issue assignee;
-   Project #2 item membership;
-   Project #2 field values for those sixteen Release 1.2 items;
-   issue dependency text/links in the issue bodies.

No repository production/test/documentation implementation mutation is
authorized.

------------------------------------------------------------------------

## 6. Explicitly Prohibited

Do not:

-   start WP01;
-   move WP01 to `In Progress`;
-   create WP17+;
-   create a lifecycle-gate issue;
-   create Release 1.3 issues;
-   create Release 1.3 milestone;
-   reopen or repurpose milestone #43;
-   rename milestone #43;
-   change milestone #43 description;
-   assign issues to milestone #43;
-   modify milestone #52;
-   alter Release 1.1 issues;
-   create or edit labels unless a later explicit authority permits it;
-   change Project field names;
-   delete Project fields/options;
-   rename existing Project options;
-   change existing Project option IDs;
-   change Status/Priority/Area schema;
-   create repository branches;
-   stage files;
-   commit;
-   push;
-   create PRs;
-   change source;
-   change tests;
-   begin implementation.

------------------------------------------------------------------------

## 7. Authentication / Repository Identity Gate

Before planning mutation verify:

``` text
GitHub authentication = PASS
authenticated account = samuel-santos-engineer
repository = samuel-santos-engineer/AIQuantTradingResearch
default branch = main
```

Do not expose tokens, credentials, or sensitive authentication
information.

If repository/account identity does not match, stop.

------------------------------------------------------------------------

## 8. Repository Preservation Gate

Inspect local Git before GitHub mutation.

Expected:

``` text
branch = main
main = origin/main
ahead/behind = 0/0
staged tracked changes = 0
tracked modifications = 0
```

The Release 1.2 execution plan, file manifest, planning prompt, and
five-line companion may exist as expected governance artifacts according
to current repository workflow.

Classify every visible repository change.

No Git operation is authorized except read-only inspection.

If unexpected user work or unexplained tracked mutation exists, stop
rather than discarding it.

------------------------------------------------------------------------

## 9. Existing GitHub Planning Inspection

Before creating anything, inventory:

### Milestones

Find:

-   authoritative Release 1.2 milestone candidates;
-   legacy #43;
-   Release 1.1 milestone #52;
-   any Release 1.3 planning objects.

### Issues

Search for:

-   existing Release 1.2 WP01--WP16 identities;
-   duplicates;
-   WP17+;
-   lifecycle-gate issues;
-   issues accidentally assigned to legacy #43;
-   Release 1.3 issues.

### Project #2

Inspect:

-   Project identity/title;
-   `Status` field/options;
-   `Priority` field/options;
-   `Release` field/options;
-   `Area` field/options;
-   existing Release 1.2 items.

Do not mutate until the complete preflight is reconciled.

------------------------------------------------------------------------

## 10. Narrow Project Release-Option Reconciliation

Required Project value:

``` text
Release = 1.2
```

If exactly one `1.2` option already exists:

-   reuse it;
-   do not recreate or rename it.

If no `1.2` option exists, this authority explicitly permits **one
narrow Project-schema mutation**:

``` text
Add exactly one option named `1.2`
to Project #2's existing `Release` single-select field.
```

Preserve:

-   field identity;
-   all existing Release options;
-   existing option IDs;
-   existing names;
-   existing colors/descriptions;
-   all unrelated fields/options.

Do not add any other option.

If more than one `1.2` option already exists, or the `Release` field is
not the expected single-select field, stop.

Record pre/post option counts and IDs.

------------------------------------------------------------------------

## 11. Legacy Milestone #43 Reconciliation

Verify identity exactly:

``` text
#43
Phase 3 - Release 1.2: Storage
```

If #43 is CLOSED and empty:

-   preserve it unchanged.

If #43 is OPEN and empty:

this authority explicitly permits exactly:

``` text
state = CLOSED
```

Preserve title, description, due date, and empty issue state.

If #43 contains any issue, has been repurposed, or materially differs in
identity, stop.

Never use #43 for new planning.

------------------------------------------------------------------------

## 12. Duplicate / Collision Gate

Before new object creation, determine whether an authoritative planning
set already exists.

Authoritative milestone identity:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

Authoritative WP identity is determined by WP number + exact title from
Section 16.

Rules:

-   exactly zero or one authoritative milestone may exist before
    reconciliation;
-   exactly zero or one issue may represent each WP;
-   no duplicate WP identity may remain;
-   no competing active Release 1.2 milestone may remain;
-   do not create duplicate issues merely because labels/body differ.

If a complete authoritative planning set already exists, reconcile it
idempotently rather than duplicating it.

If ambiguous collisions cannot be reconciled safely, stop.

------------------------------------------------------------------------

## 13. Labels

Reuse existing repository labels only.

Required semantic labels:

``` text
research
architecture
feature
infra
tests
```

Do not create or edit labels.

If a required label does not exist, stop and report the missing label.

Each WP receives exactly one governed semantic label according to
Section 16.

------------------------------------------------------------------------

## 14. Assignee

Required assignee for all sixteen issues:

``` text
samuel-santos-engineer
```

Verify assignment permission before creation/reconciliation.

Final:

``` text
assigned = 16/16
additional assignees = 0
```

If assignment is not possible, stop before issue creation where
possible.

------------------------------------------------------------------------

## 15. Authoritative Milestone

Create or reconcile exactly one milestone titled:

``` text
Phase 3 - Release 1.2: Research Dataset Foundation
```

Required state:

``` text
OPEN
```

No due date is required unless an existing accepted authority explicitly
provides one.

Milestone description must concisely establish:

-   objective: deterministic, versioned, reproducible, discoverable
    research datasets;
-   metadata/provenance/lineage/catalog scope;
-   reuse of Release 1.1 durable historical observations;
-   Release 1.3 pipeline orchestration explicitly out of scope;
-   WP01--WP16 governed by the Release 1.2 execution plan/file manifest.

Do not reuse #43.

------------------------------------------------------------------------

## 16. Authoritative Work-Package Mapping

Create/reconcile exactly these sixteen issues.

  ---------------------------------------------------------------------------------
  WP             Exact Title       Depends On     Label            Project Area
  -------------- ----------------- -------------- ---------------- ----------------
  WP01           Release &         Release 1.1    `research`       Engineering
                 Repository        CLOSED
                 Preflight

  WP02           Research Dataset  WP01           `research`       Data
                 Definition &
                 Reproducibility
                 Model

  WP03           Dataset Identity, WP02           `architecture`   Architecture
                 Version &
                 Provenance
                 Semantics

  WP04           Application       WP03           `feature`        Architecture
                 Dataset Contracts

  WP05           Dataset           WP04           `feature`        Data
                 Materialization
                 Use Case

  WP06           Dataset Metadata  WP03, WP04     `architecture`   Data
                 & Catalog Model

  WP07           Dataset Physical  WP05, WP06     `infra`          Infrastructure
                 Storage Model

  WP08           Dataset Snapshot  WP07           `feature`        Infrastructure
                 Persistence

  WP09           Dataset Catalog   WP08           `feature`        Infrastructure
                 Persistence &
                 Lookup

  WP10           Dataset           WP05, WP08,    `feature`        Data
                 Materialization   WP09
                 Integration

  WP11           Dataset           WP10           `feature`        Infrastructure
                 Validation &
                 Failure Mapping

  WP12           Dependency        WP11           `feature`        Host
                 Registration &
                 Bounded Dataset
                 Execution

  WP13           Domain &          WP03, WP04,    `tests`          Testing
                 Application       WP05, WP06
                 Dataset Tests

  WP14           Infrastructure &  WP07, WP08,    `tests`          Testing
                 Dataset Tests     WP09, WP10,
                                   WP11, WP12

  WP15           Architecture &    WP13, WP14     `architecture`   Architecture
                 Documentation
                 Alignment

  WP16           Full Validation,  WP15           `research`       Validation
                 Integration &
                 Acceptance
  ---------------------------------------------------------------------------------

Issue titles should follow the repository's established Release/WP
naming convention while preserving the exact WP identity and title
above.

No artificial dependency edges are allowed.

------------------------------------------------------------------------

## 17. Issue Body Contract

Every WP issue must contain exactly these eight logical sections:

``` text
## Objective
## Scope
## Dependencies
## Deliverables
## Validation Evidence
## Exit Criteria
## Out of Scope
## Authority
```

The content must be derived from the Release 1.2 execution plan and file
manifest.

### Objective

Use the corresponding WP objective.

### Scope

Summarize the WP-owned responsibilities and mutation boundary.

### Dependencies

Use actual GitHub issue links/numbers after issue identity is known.

WP01 dependency must state:

``` text
Release 1.1 CLOSED
```

For multi-parent dependencies, enumerate every required issue.

Do not add transitive dependencies unless explicitly required by the
plan.

### Deliverables

State artifact classes/behaviors owned by the WP without inventing
filenames where the manifest intentionally leaves names evidence-driven.

### Validation Evidence

State evidence the WP must later produce, not fabricated current
results.

### Exit Criteria

Use the plan's WP exit condition.

### Out of Scope

Protect later WPs and Release 1.3.

### Authority

Reference both:

``` text
docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md
docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md
```

Do not paste the full authority text into issue bodies.

------------------------------------------------------------------------

## 18. Issue Creation Order

Create/reconcile issues in dependency-safe WP order:

``` text
WP01
WP02
WP03
WP04
WP05
WP06
WP07
WP08
WP09
WP10
WP11
WP12
WP13
WP14
WP15
WP16
```

This allows concrete predecessor issue numbers to be inserted in
dependent issue bodies.

Before first issue creation, all foreseeable non-issue blockers must
already be cleared:

-   milestone identity;
-   labels;
-   assignee;
-   Project field/schema readiness;
-   required Project options;
-   legacy #43 state;
-   duplicate/collision reconciliation.

Do not knowingly begin a partial issue set while a prerequisite remains
unresolved.

------------------------------------------------------------------------

## 19. Project #2 Integration

Project:

``` text
AIQuantTradingResearch Engineering Roadmap
```

Add each authoritative WP issue exactly once.

Required values for all sixteen items:

``` text
Status   = Backlog
Priority = P1
Release  = 1.2
Area     = mapping from Section 16
```

Do not move WP01 to `In Progress`.

Do not change Project schema except the explicitly authorized narrow
`Release = 1.2` option addition in Section 10.

Do not alter unrelated Project items.

------------------------------------------------------------------------

## 20. Project Automation

After creating/adding/configuring items, inspect whether Project
automation changed any intended values.

Final required state:

``` text
WP01–WP16 Status = Backlog
```

If automation sets a different status and it is safe/authorized to
restore the intended existing field value, set it back to `Backlog`.

Do not change Project workflows/automation definitions.

------------------------------------------------------------------------

## 21. Dependency Validation

After all issues exist, validate the full graph.

Required exact edges:

``` text
WP01  ← Release 1.1 CLOSED
WP02  ← WP01
WP03  ← WP02
WP04  ← WP03
WP05  ← WP04
WP06  ← WP03, WP04
WP07  ← WP05, WP06
WP08  ← WP07
WP09  ← WP08
WP10  ← WP05, WP08, WP09
WP11  ← WP10
WP12  ← WP11
WP13  ← WP03, WP04, WP05, WP06
WP14  ← WP07, WP08, WP09, WP10, WP11, WP12
WP15  ← WP13, WP14
WP16  ← WP15
```

Required:

``` text
missing edges = 0
artificial edges = 0
dependency drift = 0
```

------------------------------------------------------------------------

## 22. Final Issue Validation

For each WP verify:

-   exactly one authoritative issue;
-   state OPEN;
-   correct milestone;
-   correct semantic label;
-   exact one governed label;
-   assignee exactly `samuel-santos-engineer`;
-   all eight body sections;
-   two authority references;
-   exact dependencies;
-   Project membership exactly once;
-   `Status=Backlog`;
-   `Priority=P1`;
-   `Release=1.2`;
-   correct Area.

Final:

``` text
WP01–WP16 = 16/16
open = 16
closed = 0
```

------------------------------------------------------------------------

## 23. Release 1.3 Protection

Inspect current Release 1.3 planning state.

This authority must not create or modify:

-   Release 1.3 milestone;
-   Release 1.3 issues;
-   Release 1.3 Project items;
-   Release 1.3 implementation.

Existing historical/future Release 1.3 planning may be observed, but
must not be mutated unless a later explicit authority governs it.

The key success condition is:

``` text
Release 1.3 objects created by this execution = 0
Release 1.3 implementation started = NO
```

------------------------------------------------------------------------

## 24. Repository Mutation Protection

This GitHub-planning execution must not perform implementation or Git
transport.

Expected repository mutation:

``` text
tracked production/test/docs edits = 0
staged paths = 0
commits = 0
branches = 0
pushes = 0
PRs = 0
```

The governance files already created for Release 1.2 must remain
byte-preserved unless this planning authority is separately integrated
later.

If this planning authority/chat pair appears as untracked execution
input, classify it according to the Release 1.2 manifest and current
workflow; do not stage/commit it during GitHub planning.

Do not remove unrelated user files.

------------------------------------------------------------------------

## 25. Failure / Partial-Mutation Policy

GitHub planning APIs are not assumed transactional.

Therefore:

1.  resolve every foreseeable blocker before the first milestone/issue
    creation;
2.  prefer idempotent reconciliation over duplicate creation;
3.  if an unexpected failure occurs after some authorized planning
    mutations:
    -   stop further mutation;
    -   do not invent destructive rollback;
    -   record exactly what was created/changed;
    -   report the partial state;
    -   request the smallest reconciliation authority required.

Never hide a partial planning run.

------------------------------------------------------------------------

## 26. Acceptance Matrix

Report at least:

  Requirement                           Required
  ------------------------------------- ----------------
  Release 1.1 closure gate              PASS
  Legacy #43                            CLOSED / EMPTY
  Authoritative milestone               exactly 1
  Milestone state                       OPEN
  WP issues                             16
  WP01--WP16 represented                16/16
  WP17+                                 0
  Lifecycle-gate issues                 0
  Open WP issues                        16
  Closed WP issues                      0
  Correct milestone assignment          16/16
  Eight-section bodies                  16/16
  Authority references                  16/16
  Dependency drift                      0
  Assignees                             16/16
  Semantic labels                       16/16
  Project items                         16/16
  Status Backlog                        16/16
  Priority P1                           16/16
  Release 1.2                           16/16
  Area populated                        16/16
  Duplicate WPs                         0
  Repository implementation mutations   0
  Release 1.3 objects created           0
  WP01 started                          NO

------------------------------------------------------------------------

## 27. Required Execution Report

Return a structured:

``` text
Release 1.2 GitHub Planning Execution Report
```

with at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Authentication / Repository Context
4.  Release 1.1 Closure Gate
5.  Existing GitHub Planning-State Inspection
6.  Legacy Milestone #43 Reconciliation
7.  Project Release-Option Reconciliation
8.  Duplicate / Collision Reconciliation
9.  Authoritative Milestone Result
10. Issue Creation / Reconciliation
11. Work-Package Mapping
12. Dependency Validation
13. Labels
14. Assignees
15. GitHub Project Integration
16. Project Automation Observations
17. Release / Priority / Area Validation
18. Scope Protection
19. Repository Mutation Check
20. Release 1.3 Protection
21. Findings / Observations
22. Planning Acceptance Matrix
23. Final GitHub Planning State
24. Final Decision
25. Next Authorized Action

Include actual milestone/issue numbers and links.

------------------------------------------------------------------------

## 28. Success Terminal

Only if every mandatory planning gate passes, end exactly with:

``` text
RELEASE 1.2 GITHUB PLANNING COMPLETE

RELEASE 1.2 PLANNING:
Authoritative milestone: exactly 1 / OPEN
WP01–WP16: 16/16 OPEN / BACKLOG
Dependency drift: 0
Assignees: 16/16
Priority P1: 16/16
Release 1.2: 16/16
Area populated: 16/16
Legacy milestone #43: CLOSED / EMPTY
WP17+: 0
Lifecycle-gate issues: 0
Repository implementation mutations: 0
Release 1.3 implementation started: NO
WP01 started: NO

NEXT AUTHORIZED ACTION:
Human review and acceptance of the Release 1.2 GitHub planning state.
After acceptance, the next separately authorized work package is WP01 — Release & Repository Preflight.
```

------------------------------------------------------------------------

## 29. Blocked Terminal

If any mandatory gate cannot be satisfied, end with:

``` text
RELEASE 1.2 GITHUB PLANNING BLOCKED

BLOCKER:
<exact blocker>

PARTIAL MUTATIONS:
<exact GitHub mutations, or NONE>

SMALLEST CORRECTIVE AUTHORITY:
<required narrow action>

WP01 started: NO
Release 1.3 implementation started: NO
```

Do not emit the COMPLETE terminal.

------------------------------------------------------------------------

## 30. Final Boundary

This authority ends at complete GitHub planning.

After success:

-   milestone exists;
-   WP01--WP16 exist and are Backlog;
-   dependencies and Project values are reconciled;
-   WP01 remains unstarted.

Do not continue into WP01 in the same run.

A separately authored authoritative:

``` text
01-release-repository-preflight-codex-prompt.md
```

plus its standard five-line bootstrap is required before implementation
work may begin.

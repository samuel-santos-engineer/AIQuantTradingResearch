# Release 1.3 GitHub Planning Authority

## Phase 3 --- Release 1.3: Research Pipeline Foundation

**Authority type:** GitHub planning only\
**Implementation authority:** NONE\
**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`\
**Project:** `AIQuantTradingResearch Engineering Roadmap` --- Project
#2\
**Authoritative definition:**
`docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`\
**Execution authority:** `RELEASE_1.3_EXECUTION_PLAN.md`\
**File authority:** `RELEASE_1.3_FILE_MANIFEST.md`

------------------------------------------------------------------------

## 1. Objective

Reconcile GitHub planning so Release 1.3 is represented exactly as the
accepted:

**Phase 3 --- Release 1.3: Research Pipeline Foundation**

Create only the GitHub planning objects required for WP01--WP14. Do not
start WP01 and do not modify repository implementation.

## 2. Starting-state gates

Before any mutation:

1.  Read this authority completely.
2.  Read `RELEASE_1.3_EXECUTION_PLAN.md` completely.
3.  Read `RELEASE_1.3_FILE_MANIFEST.md` completely.
4.  Read `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
    completely.
5.  Verify repository identity and GitHub authentication without
    exposing credentials.
6.  Verify Release 1.2 closure:
    -   PR #137 MERGED.
    -   milestone #53 CLOSED.
    -   issues #121--#136 Closed.
    -   Project #2 Release 1.2 WP items Done.
7.  Inspect current Release 1.3 milestones, issues, Project #2 items,
    fields, field options, labels, and assignees.
8.  Verify no Release 1.3 implementation has started.
9.  Verify no existing authoritative WP01--WP14 planning already
    satisfies this authority.

If Release 1.2 is not closed, Release 1.3 implementation already exists
unexpectedly, or planning objects conflict materially with this
authority, STOP before mutation and report the conflict.

## 3. Legacy milestone protection

Existing milestone #44:

`Phase 3 - Release 1.3: Pipelines`

is legacy planning state.

It must remain unchanged:

-   do not rename it;
-   do not reopen/close it as part of this authority;
-   do not repurpose it;
-   do not rewrite its description;
-   do not assign Release 1.3 WP01--WP14 issues to it.

Expected authoritative treatment: preserve #44 as an empty legacy
milestone.

If it is no longer empty, report the exact state before making an
assumption.

## 4. Authoritative milestone

Create exactly one authoritative milestone if it does not already exist:

`Phase 3 - Release 1.3: Research Pipeline Foundation`

Required state:

-   OPEN;
-   no due date unless already separately authorized;
-   exactly 14 authoritative WP issues assigned;
-   description must concisely record:
    -   deterministic fixed one-shot research pipeline;
    -   reuse of persisted Release 1.1 historical observations;
    -   reuse of Release 1.2 dataset materialization/snapshot/catalog
        capabilities;
    -   Application-owned pipeline semantics/orchestration;
    -   schema remains version 2;
    -   Release 1.4+ scheduling, DAGs, retries, live-acquisition
        orchestration, durable run history, distributed execution,
        feature engineering, and MLOps excluded.

Do not create duplicate authoritative milestones.

## 5. Project Release option

Inspect Project #2 `Release` options.

Required final state:

-   exactly one option with semantic value `1.3`.

If absent, add exactly one `1.3` option.

If exactly one already exists, reuse it.

If duplicates exist, STOP and report rather than deleting or guessing.

Do not alter unrelated Release options.

## 6. Authoritative work-package issues

Create/reconcile exactly these 14 issues:

  -----------------------------------------------------------------------
  WP                Title             Exact             Model
                                      dependencies
  ----------------- ----------------- ----------------- -----------------
  WP01              Release &         Release 1.2       Luna
                    Repository        CLOSED
                    Preflight

  WP02              Research Pipeline WP01              Sol
                    Semantic
                    Discovery

  WP03              Pipeline          WP02              Sol
                    Identity,
                    Provenance &
                    Evidence
                    Semantics

  WP04              Application       WP03              Terra
                    Pipeline
                    Contracts

  WP05              Fixed Pipeline    WP04              Terra
                    Orchestration

  WP06              Pipeline          WP03, WP05        Sol
                    Validation &
                    Failure Semantics

  WP07              Structured        WP03, WP05, WP06  Terra
                    Execution
                    Evidence

  WP08              Dependency        WP04, WP05, WP06, Terra
                    Registration &    WP07
                    Configuration

  WP09              One-Shot Worker   WP08              Terra
                    Pipeline
                    Execution

  WP10              Application       WP03, WP04, WP05, Luna
                    Pipeline Tests    WP06

  WP11              Composition &     WP07, WP08, WP09  Terra
                    Worker Validation

  WP12              Architecture      WP09, WP10, WP11  Terra
                    Evolution

  WP13              Documentation     WP12              Terra
                    Alignment

  WP14              Full Validation,  WP10, WP11, WP12, Sol
                    Integration &     WP13
                    Acceptance
  -----------------------------------------------------------------------

Do not create WP15+, lifecycle-gate issues, merge issues, closure
issues, or Release 1.4 issues.

## 7. Issue body contract

Every WP issue must contain exactly these eight semantic sections,
following existing repository planning conventions:

1.  `## Objective`
2.  `## Scope`
3.  `## Out of Scope`
4.  `## Dependencies`
5.  `## Deliverables`
6.  `## Acceptance Criteria`
7.  `## Validation`
8.  `## Authority`

Each body must reference:

-   `RELEASE_1.3_EXECUTION_PLAN.md`
-   `RELEASE_1.3_FILE_MANIFEST.md`

The Authority section must state that the individual WP Codex prompt,
once separately authorized, is the execution authority for that WP.

Issue bodies must preserve the accepted release boundary. They must not
invent implementation details that belong to later WPs.

## 8. Issue-specific intent

### WP01

Verify Release 1.2 closure, Release 1.3 governance/planning,
repository/toolchain, schema v2, architecture, permanent-test baseline,
security, and scope. No implementation.

### WP02

Freeze research-pipeline vocabulary, deterministic fixed stage model,
source boundary, one-shot semantics, and Release 1.4+ exclusions.

### WP03

Define pipeline definition/run identity, provenance, evidence,
equivalence/distinguishability, and semantic vs operational identifiers.

### WP04

Introduce minimum provider/storage-independent Application pipeline
contracts.

### WP05

Implement deterministic fixed sequential Application orchestration by
composing accepted Release 1.2 capabilities.

### WP06

Validate pipeline semantics and define bounded stage-attributed failure
behavior without retries or broad exception swallowing.

### WP07

Introduce bounded structured local execution evidence without durable
operational run-history persistence.

### WP08

Register accepted pipeline dependencies/configuration without resolution
side effects or background services.

### WP09

Execute exactly one bounded Worker pipeline invocation; no recurring
execution or live-acquisition orchestration.

### WP10

Add permanent pure Application pipeline tests.

### WP11

Add minimum justified composition/configuration/Worker validation.

### WP12

Reconcile executable architecture rules. Explicitly state that zero
architecture-test delta is valid when existing rules already enforce all
new boundaries.

### WP13

Align current-state documentation only; no production/test behavior.

### WP14

Perform exact candidate reconciliation, complete validation, one
integration commit/branch/push, and one review-ready PR. Must not merge
or close the milestone.

## 9. Labels

Reuse existing repository labels. Do not create or edit labels.

Each issue must receive exactly one governed semantic/area label
according to existing Release 1.1/1.2 conventions.

Choose the closest existing authoritative label based on WP
responsibility. Do not attach unrelated extra governed labels.

If the existing label taxonomy cannot represent a WP without inventing a
label, STOP and report the mismatch.

## 10. Assignee

Assign each WP issue to:

`samuel-santos-engineer`

No additional assignees.

## 11. Project #2 integration

Add every WP01--WP14 issue exactly once to Project #2.

Required initial fields:

-   **Status:** Backlog
-   **Priority:** P1
-   **Release:** 1.3
-   **Area:** authoritative mapping below

Area mapping:

  WP     Area intent
  ------ ----------------
  WP01   Engineering
  WP02   Architecture
  WP03   Architecture
  WP04   Architecture
  WP05   Engineering
  WP06   Architecture
  WP07   Engineering
  WP08   Infrastructure
  WP09   Host
  WP10   Testing
  WP11   Testing
  WP12   Architecture
  WP13   Architecture
  WP14   Validation

Use the exact existing Project option whose semantics correspond to each
listed Area intent. Do not create Area options.

If an exact/clearly equivalent option is unavailable, STOP and report
before changing Project schema.

WP01 must remain Backlog. Do not move any WP to In Progress.

## 12. Dependency representation

Represent dependencies using the same GitHub issue-body/dependency
convention accepted for Release 1.2.

The final graph must contain exactly the edges in section 6.

Requirements:

-   missing edges: 0;
-   artificial edges: 0;
-   dependency drift: 0.

Do not infer transitive dependencies.

## 13. Repository protection

This planning authority permits no repository implementation mutation.

Do not:

-   edit production code;
-   edit tests;
-   edit architecture/docs;
-   edit execution plan/manifest/definition;
-   stage files;
-   commit;
-   create a branch;
-   push;
-   create a PR;
-   merge;
-   tag;
-   create a GitHub Release.

The planning authority files themselves are out-of-band inputs and are
not to be staged or committed by this execution.

## 14. Release 1.4 protection

Do not create or modify Release 1.4 planning or implementation.

Do not create pipeline-managed live acquisition, scheduler, retry, DAG,
checkpoint/resume, durable run-history, metrics/tracing backend,
feature-engineering, model-training, or MLOps issues.

Existing unrelated future planning must remain unchanged.

## 15. Validation

Before completion verify:

-   Release 1.2 closure gate: PASS.
-   legacy milestone #44 preserved.
-   exactly one authoritative Release 1.3 milestone.
-   authoritative milestone OPEN.
-   exactly 14 WP issues.
-   WP01--WP14 represented exactly once.
-   all 14 issues OPEN.
-   all 14 assigned to authoritative milestone.
-   all bodies contain exactly the eight required semantic sections.
-   both authority references present in all 14.
-   exact dependency graph.
-   correct assignee 14/14.
-   exactly one governed semantic label per issue.
-   Project membership 14/14.
-   duplicate Project items 0.
-   Status Backlog 14/14.
-   Priority P1 14/14.
-   Release 1.3 14/14.
-   Area populated correctly 14/14.
-   WP15+ 0.
-   lifecycle-gate issues 0.
-   WP01 started: NO.
-   repository implementation mutations: 0.
-   Release 1.4 implementation/planning mutations: 0.

## 16. Allowed GitHub mutations

Only:

1.  add Project `Release = 1.3` option if absent;
2.  create the authoritative Release 1.3 milestone if absent;
3.  create/reconcile the 14 WP issues;
4.  assign the issues to the milestone and sole assignee;
5.  apply existing semantic labels;
6.  add issues to Project #2;
7.  set required Project fields.

No other mutation is authorized.

## 17. Completion report

Produce a detailed execution report including:

-   authentication/repository context;
-   Release 1.2 closure verification;
-   legacy milestone state;
-   Project Release-option reconciliation;
-   authoritative milestone number/state;
-   WP issue number/title table;
-   dependency validation;
-   labels/assignees;
-   Project integration and Area distribution;
-   scope protection;
-   mutation accounting;
-   final acceptance matrix.

End exactly with:

``` text
RELEASE 1.3 GITHUB PLANNING COMPLETE

RELEASE 1.3 PLANNING:
Authoritative milestone: exactly 1 / OPEN
WP01–WP14: 14/14 OPEN / BACKLOG
Dependency drift: 0
Assignees: 14/14
Priority P1: 14/14
Release 1.3: 14/14
Area populated: 14/14
Legacy milestone #44: PRESERVED
WP15+: 0
Lifecycle-gate issues: 0
Repository implementation mutations: 0
Release 1.4 implementation started: NO
WP01 started: NO

NEXT AUTHORIZED ACTION:
Human review and acceptance of the Release 1.3 GitHub planning state.
After acceptance, the next separately authorized work package is WP01 — Release & Repository Preflight.
```

If blocked, do not print the completion marker. Report the smallest
corrective authority required.

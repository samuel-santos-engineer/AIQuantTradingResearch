# Release 1.8 --- GitHub Planning Codex Authority

## Authority

You are authorized to perform **Release 1.8 GitHub planning only** for:

`samuel-santos-engineer/AIQuantTradingResearch`

This authority is subordinate to the three human-accepted Release 1.8
planning artifacts:

-   `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`

Read all three files completely before making any GitHub mutation.

The frozen predecessor is Release 1.7:

-   authoritative commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   authoritative tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   schema: v3
-   permanent test baseline: 268/268
-   Release 1.7 is closed

Do not revise, reinterpret, or retrospectively review completed Release
1.1--1.7 implementation.

## Objective

Materialize the already-defined Release 1.8 planning model in GitHub
without beginning implementation.

Create or reconcile exactly one authoritative Release 1.8 milestone and
exactly 13 Release 1.8 work-package issues corresponding to WP01--WP13
in the accepted execution plan.

Configure the issues in GitHub Project #2 consistently with the
established repository planning model.

The intended milestone is:

`Phase 4 - Release 1.8: Python & AI Engineering Foundation`

## Mandatory Starting-State Gate

Before any mutation, perform a read-only reconciliation of repository
and GitHub state.

Verify at minimum:

1.  repository is `samuel-santos-engineer/AIQuantTradingResearch`;
2.  current branch is `main`;
3.  local `main` equals `origin/main`;
4.  HEAD equals `f8e521af2c5262d6cc173d0731b5e915dbceac0a`;
5.  ahead/behind is `0/0`;
6.  staged paths are zero;
7.  tracked repository-content mutations are zero;
8.  Release 1.7 PR #210 is merged;
9.  Release 1.7 milestone #55 is closed;
10. Release 1.7 issues #197--#209 are Closed/Done;
11. the three Release 1.8 planning artifacts exist and are mutually
    consistent;
12. no Release 1.8 implementation branch, PR, production implementation,
    or WP implementation has begun;
13. no Release 1.9 implementation has begun;
14. Project #2 is accessible and its relevant fields/options can be
    reconciled;
15. there is no existing authoritative Release 1.8 planning state that
    would be duplicated by this execution.

If any mandatory starting-state condition fails, stop before mutation
and report the exact blocker plus the smallest corrective authority
required.

## Historical Milestone Protection

Historical milestone #49 is a known legacy Release 1.8 milestone.

Its historical scope is not the authoritative Release 1.8 scope defined
by the accepted planning artifacts.

Before mutation verify:

-   milestone #49 exists;
-   milestone #49 is CLOSED;
-   milestone #49 is empty;
-   its historical title and metadata remain unchanged.

Milestone #49 is historical evidence.

Do **not**:

-   reopen it;
-   rename it;
-   edit its description;
-   reuse it;
-   delete it;
-   assign new issues to it.

If #49 is not closed and empty exactly as expected, stop before mutation
and request corrective authority.

Also inspect all milestones for any other open or conflicting Release
1.8 assignment. If a conflicting milestone or partially materialized
Release 1.8 planning state exists, stop before creating duplicates and
report the actual state.

## Authoritative Work Packages

Create/reconcile exactly these 13 issues, preserving the accepted
sequence and semantics:

1.  `WP01 — Release & Repository Preflight`
2.  `WP02 — Python Runtime Compatibility & Version Selection`
3.  `WP03 — Windows Machine-Wide Python Foundation`
4.  `WP04 — PowerShell & VS Code Python Validation`
5.  `WP05 — Virtual Environment & Dependency Isolation`
6.  `WP06 — Python Dependency Governance`
7.  `WP07 — Scientific & Machine Learning Library Foundation`
8.  `WP08 — Streamlit Visualization Foundation`
9.  `WP09 — .NET ↔ Python Interoperability Architecture`
10. `WP10 — .NET ↔ Python Integration Proof`
11. `WP11 — Python Foundation & Interoperability Tests`
12. `WP12 — Architecture, Documentation & Developer Environment Alignment`
13. `WP13 — Full Validation, Integration & Acceptance`

Issue bodies must be concise but sufficient to preserve the objective,
reason, owned solution boundary, important exclusions,
predecessor/dependency, and completion intent defined by the
authoritative execution plan.

Do not invent implementation details that the planning artifacts
deliberately defer to the owning WP.

In particular, do not prematurely freeze:

-   the Python version before WP02;
-   the Python dependency-file format before WP06;
-   the Python source tree before the owning WPs inspect repository
    conventions;
-   the `.NET ↔ Python` interoperability implementation before WP09;
-   permanent test-count deltas before WP11.

## Milestone

If and only if the read-only reconciliation proves no authoritative
Release 1.8 milestone already exists, create exactly one milestone:

`Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Assign exactly WP01--WP13 to it.

The milestone must remain OPEN after planning with:

-   13 open issues;
-   0 closed issues.

Do not create a due date unless the accepted repository planning
convention explicitly requires one and a value is already authoritative.
Do not invent dates.

## GitHub Project #2

Add exactly WP01--WP13 to the existing:

`AIQuantTradingResearch Engineering Roadmap`

Project #2.

There must be exactly one Project item per Release 1.8 issue.

Configure every Release 1.8 issue with:

-   Status: `Backlog`
-   Priority: `P1`
-   Release: `1.8`
-   Area: the authoritative repository-native Area appropriate to that
    WP

Use existing Project field options where they already exist.

If the `Release` field lacks a `1.8` option, create only the required
`1.8` option if the GitHub tooling and established Project convention
permit it. Do not modify unrelated Release options.

For `Area`, inspect predecessor issues and existing Project conventions
and choose the smallest truthful existing Area. Do not invent new Area
taxonomy unless no truthful existing option can represent a WP; if a new
Area would be required, stop and request corrective authority rather
than silently extending governance.

## Dependencies

Establish the exact linear dependency chain:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13`

Interpretation:

-   WP02 depends on WP01;
-   WP03 depends on WP02;
-   ...
-   WP13 depends on WP12.

Use the repository's established GitHub dependency mechanism/convention.

Expected dependency edges: 12.

Do not introduce parallel dependencies or dependencies to predecessor
releases unless already required by the accepted planning artifacts.

## Assignment

Assign all 13 issues to:

`samuel-santos-engineer`

Do not assign additional users.

## Predecessor Preservation

Release 1.8 planning must not mutate predecessor release planning state.

Read back and verify the established predecessor Project state after
planning.

At minimum preserve:

-   Release 1.7 issues #197--#209 as Closed/Done;
-   milestone #55 as closed;
-   historical milestone #49 unchanged;
-   predecessor Project Release/Area/Priority/Status values;
-   predecessor dependency relationships.

If repository tooling supports the established broader predecessor
restoration check, perform it and report the exact reconciled count.

Do not "repair" historical state without explicit authority.

## Repository Mutation Prohibition

This GitHub Planning authority does not authorize repository-content
changes.

Do not:

-   edit source code;
-   edit tests;
-   edit documentation;
-   edit the three Release 1.8 planning files;
-   create WP prompt pairs;
-   install Python;
-   create a virtual environment;
-   install Python packages;
-   edit `.gitignore`;
-   create dependency files;
-   stage files;
-   commit;
-   push;
-   create or delete Git branches;
-   create PRs;
-   merge;
-   tag;
-   create GitHub Releases.

Execution-only copies of this authority may be mechanically removed only
if their lifecycle is explicitly established as out-of-band/untracked
execution input and removal is necessary for final cleanliness. Never
remove governed planning artifacts.

## Release 1.9 Prohibition

Do not create or modify:

-   Release 1.9 milestone;
-   Release 1.9 issues;
-   Release 1.9 Project items;
-   Release 1.9 branches;
-   Release 1.9 PRs;
-   Release 1.9 implementation.

Release 1.9 remains separately governed.

## Mutation Strategy

Perform all possible reconciliation before the first mutation.

After the first mutation, continue conservatively.

If a later step reveals that continuing would create duplicate,
contradictory, or materially incorrect planning state:

1.  stop;
2.  do not perform speculative cleanup;
3.  report every mutation already performed;
4.  describe the actual partial state;
5.  request the smallest corrective authority required.

Never hide partial execution.

## Required Final Read-Back

After planning, independently read back and verify:

### Milestone

-   exactly one authoritative Release 1.8 milestone;
-   correct title;
-   OPEN;
-   13 open / 0 closed;
-   all and only WP01--WP13 assigned.

### Issues

-   exactly 13 authoritative Release 1.8 issues;
-   all OPEN;
-   all assigned to `samuel-santos-engineer`;
-   titles correspond exactly to WP01--WP13;
-   no WP14+;
-   no duplicate Release 1.8 issues.

### Project #2

-   membership: 13/13;
-   duplicate items: 0;
-   Status Backlog: 13/13;
-   Priority P1: 13/13;
-   Release 1.8: 13/13;
-   Area: 13/13 correct;
-   dependency edges: 12;
-   dependency drift: 0.

### Historical/predecessor state

-   milestone #49: CLOSED, empty, unchanged;
-   Release 1.7 milestone #55: CLOSED;
-   Release 1.7 issues #197--#209: Closed/Done;
-   predecessor Project state unchanged;
-   Release 1.9 planning/implementation mutations: 0.

### Repository/Git state

-   branch remains `main`;
-   HEAD remains `f8e521af2c5262d6cc173d0731b5e915dbceac0a`;
-   `main == origin/main`;
-   ahead/behind `0/0`;
-   staged paths: 0;
-   tracked repository-content changes: 0;
-   commits/pushes/branches/PRs/tags/releases created by this authority:
    0.

## Required Report

Report:

1.  starting-state reconciliation;
2.  historical milestone #49 verification;
3.  authoritative Release 1.8 milestone number and state;
4.  WP01--WP13 issue numbers and titles;
5.  assignment state;
6.  Project #2 membership and duplicate count;
7.  Status/Priority/Release/Area reconciliation;
8.  dependency-edge reconciliation;
9.  predecessor-state preservation;
10. repository/Git mutation accounting;
11. GitHub mutation accounting;
12. any execution-only authority cleanup;
13. final repository cleanliness;
14. exact next authorized action.

If successful, terminate with exactly:

`RELEASE 1.8 GITHUB PLANNING COMPLETE`

`RELEASE 1.8 READY FOR WP01 AUTHORIZATION`

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

Do not start WP01.

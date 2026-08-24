# Release 1.8 --- Partial GitHub Planning Reconciliation Codex Authority

## Authority

You are authorized to reconcile and complete the known partial GitHub
Planning state for:

`samuel-santos-engineer/AIQuantTradingResearch`

Release:

**Phase 4 - Release 1.8: Python & AI Engineering Foundation**

This authority exists because the original Release 1.8 GitHub Planning
execution created milestone #56 and issues #211--#212, then stopped
after GitHub returned `Content already exists in this project (add_000)`
while adding #212 to Project #2.

The governing objective is to preserve valid objects, reconcile actual
GitHub state idempotently, and complete the already human-accepted
Release 1.8 GitHub Planning state without recreating or deleting valid
objects.

This authority supersedes the original GitHub Planning authority only
for reconciliation and completion of this known partial state. It does
not authorize Release 1.8 implementation.

## Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
-   `release-1.8-github-planning-codex-prompt.md`
-   `release-1.8-github-planning-codex-prompt-chat.md`

The three governed planning artifacts are human-accepted Release 1.8
planning authority. Preserve their WP01--WP13 definitions, titles,
order, scope, Area ownership, priority, Release assignment, and
dependency intent exactly.

## Frozen Repository Baseline

Release 1.7 remains frozen at:

-   branch: `main`
-   commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   schema: v3
-   permanent tests: 268/268
-   Release 1.7: CLOSED

No repository-content or Git mutation is authorized.

## Known Partial GitHub State

The prior execution reported:

-   milestone #56 exists, OPEN:
    `Phase 4 - Release 1.8: Python & AI Engineering Foundation`
-   #211 exists, OPEN: `WP01 — Release & Repository Preflight`
-   #212 exists, OPEN:
    `WP02 — Python Runtime Compatibility & Version Selection`
-   both are assigned to `samuel-santos-engineer`;
-   both are assigned to milestone #56;
-   #211 was added to Project #2;
-   explicit addition of #212 returned:
    `GraphQL: Content already exists in this project (add_000)`;
-   #211 read back with default Project Status `Todo`;
-   required Release/Priority/Area reconciliation had not yet completed;
-   WP03--WP13 were not created by that execution;
-   no dependencies were created by that execution;
-   no repository, Git, Python, PR, tag, Release, or Release 1.9
    mutation occurred.

Treat this as a hypothesis to verify from live GitHub state, not as
permission to assume unseen state.

## Mandatory Read-Only Reconciliation Gate

Before any mutation:

1.  verify authenticated GitHub account is `samuel-santos-engineer`;
2.  verify required Project mutation authorization remains available;
3.  verify repository identity;
4.  verify branch `main`;
5.  verify HEAD equals the frozen Release 1.7 baseline;
6.  verify `main == origin/main`;
7.  verify ahead/behind `0/0`;
8.  verify staged paths are zero;
9.  verify tracked repository state is clean;
10. verify Project #2 is the expected roadmap project;
11. verify Project #2 contains the required fields/options, including:
    -   Status → `Backlog`
    -   Priority → `P1`
    -   Release → `1.8`
    -   all Area values required by the accepted Release 1.8 plan;
12. verify milestone #56 exists exactly once, is OPEN, and has the
    authoritative Release 1.8 title;
13. verify historical milestone #49 remains CLOSED and empty;
14. verify Release 1.7 milestone #55 remains CLOSED;
15. verify #211 exists exactly once with authoritative WP01 title and
    milestone #56;
16. verify #212 exists exactly once with authoritative WP02 title and
    milestone #56;
17. query Project #2 directly for #211 and #212 by stable issue
    identity/URL/node ID, not title alone;
18. determine exact Project membership cardinality for each;
19. determine whether any duplicate Project item exists for either
    issue;
20. verify whether WP03--WP13 or other Release 1.8 planning objects now
    exist from any intervening execution;
21. inventory all existing Release 1.8 issues, Project items, fields,
    and dependency edges before continuing;
22. verify no conflicting Release 1.8 milestone or issue scope exists;
23. verify no Release 1.8 implementation branch/PR exists;
24. verify Release 1.9 planning/implementation has not been mutated by
    the partial execution.

If the live state cannot be reconciled uniquely to the human-accepted
plan, stop before mutation and report the smallest corrective authority
required.

Do not delete, close, recreate, or renumber #56, #211, or #212 merely to
obtain a cleaner sequence.

## Core Idempotency Rule

For every WP issue, including #211 and #212:

**Read Project #2 membership before attempting Project addition.**

-   If exactly one Project item already exists, preserve it and
    configure that item.
-   If no Project item exists, add the issue exactly once, then
    immediately read it back.
-   If GitHub returns `Content already exists in this project`, do not
    classify the issue creation as failed. Immediately reread Project #2
    by stable issue identity.
-   If reread proves exactly one item exists, treat membership as
    reconciled and continue.
-   If more than one item exists or state remains ambiguous, stop before
    further mutation.
-   Never retry a Project-add mutation blindly.

Apply the same read-before-create/read-after-create principle to
milestone, issue, field, and dependency reconciliation wherever the
GitHub API permits.

## Reconcile Existing WP01 and WP02

Preserve milestone #56 and issues #211/#212 if their live identities and
semantics match the authoritative plan.

For each of #211 and #212, reconcile exactly:

-   issue state: OPEN;
-   assignee: `samuel-santos-engineer`;
-   milestone: #56;
-   Project #2 membership: exactly one;
-   Status: `Backlog`;
-   Priority: `P1`;
-   Release: `1.8`;
-   Area: exactly the authoritative Area defined by the accepted Release
    1.8 planning artifacts.

Do not modify issue title/body unless live content materially differs
from the original authoritative issue definition. If a material
discrepancy exists, stop and report it rather than silently rewriting
historical partial state.

After both items are reconciled, create/reconcile only the authoritative
WP01 → WP02 dependency edge if the accepted plan requires it and if it
is not already present.

Do not proceed to WP03 until WP01 and WP02 are fully reconciled and
read-back passes.

## Complete WP03--WP13 Planning

After successful WP01/WP02 reconciliation, complete the remaining
Release 1.8 GitHub Planning state using the accepted artifacts.

For WP03 through WP13, process strictly in authoritative order.

For each WP:

1.  read live GitHub state for an already-existing authoritative issue
    before creating anything;
2.  if exactly one matching issue already exists and is semantically
    authoritative, preserve and reconcile it;
3.  if none exists, create exactly one issue with the authoritative
    title/body/assignee/milestone;
4.  immediately read back the issue;
5.  query Project #2 membership before attempting Project addition;
6.  add to Project #2 only when membership cardinality is zero;
7.  immediately read back membership;
8.  reconcile Status `Backlog`;
9.  reconcile Priority `P1`;
10. reconcile Release `1.8`;
11. reconcile the authoritative Area;
12. read back all required fields;
13. create/reconcile the single predecessor dependency edge from the
    immediately preceding WP according to the accepted WP01→WP13 chain;
14. read back the dependency;
15. only then proceed to the next WP.

If any WP enters ambiguous partial state, stop immediately. Preserve
completed valid work and report exact state. Do not perform broad
cleanup.

## Required Final Release 1.8 Planning State

Successful completion requires:

-   authoritative milestone #56: OPEN;
-   WP01--WP13: exactly 13 issues;
-   expected issue numbering begins with #211/#212 and continues with
    whatever GitHub actually assigns; do not require guessed numbers
    before creation;
-   all 13 issues: OPEN;
-   all 13 assigned to `samuel-santos-engineer`;
-   all 13 assigned to milestone #56;
-   Project #2 membership: 13/13;
-   duplicate Project items: 0;
-   Status `Backlog`: 13/13;
-   Priority `P1`: 13/13;
-   Release `1.8`: 13/13;
-   authoritative Area: 13/13;
-   dependency chain: exactly 12 authoritative edges forming WP01 → WP02
    → ... → WP13;
-   no extra Release 1.8 WP14+ issue;
-   no unrelated Release 1.8 issue;
-   no Release 1.8 implementation branch or PR;
-   no Release 1.9 mutation.

Milestone #56 should therefore read 13 open / 0 closed at planning
completion.

## Predecessor and Taxonomy Preservation

Verify after completion:

-   milestone #49 remains CLOSED and empty;
-   milestone #55 remains CLOSED with Release 1.7 historical state
    preserved;
-   issues #197--#209 remain Closed/Done;
-   predecessor Project state required by the accepted planning
    authority remains preserved;
-   existing Release-field options and assignments remain preserved;
-   `Release = 1.8` exists exactly once;
-   Status/Priority/Area existing taxonomy is unchanged except for field
    values assigned to the new Release 1.8 items;
-   existing Release 1.9 (#50) and Release 2.0 (#51) milestones remain
    unchanged.

Do not "repair" unrelated historical data unless the accepted Release
1.8 GitHub Planning authority explicitly requires a read-back invariant
and the mutation is already authorized there. Otherwise stop and report
unexpected drift.

## Explicitly Prohibited Actions

Do not:

-   delete or recreate milestone #56;
-   delete, close, or recreate valid #211/#212;
-   create a second Release 1.8 milestone;
-   create duplicate WP issues;
-   create duplicate Project items;
-   create WP14+;
-   change Release 1.8 scope;
-   alter accepted planning files;
-   modify repository production/test/documentation content;
-   stage files;
-   commit or push;
-   create branches or PRs;
-   merge;
-   tag;
-   create a GitHub Release;
-   install or upgrade Python;
-   create a venv;
-   install NumPy, pandas, scikit-learn, Streamlit, or other packages;
-   execute any Release 1.8 work package;
-   begin Release 1.9.

## Repository and Git Validation

At the end verify:

-   branch remains `main`;
-   HEAD remains `f8e521af2c5262d6cc173d0731b5e915dbceac0a`;
-   `main == origin/main`;
-   ahead/behind remains `0/0`;
-   staged paths remain zero;
-   tracked repository-content mutations remain zero;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   no Git transport mutation occurred.

Untracked execution/planning authority files must be accounted for
exactly and must not be staged or committed.

## Execution-Only Authority Lifecycle

This reconciliation authority pair is execution-only and must not be
committed.

The previously created Project Release taxonomy corrective authority
pair may remain untracked if not already mechanically removed. The
Release 1.8 GitHub Planning authority pair and the three governed
Release 1.8 planning artifacts remain governed/execution inputs
according to their established lifecycle.

Do not remove any authority or planning file unless its existing
lifecycle explicitly permits mechanical cleanup and the file can be
identified unambiguously. Report every cleanup action.

## Mutation Accounting

Report separately:

-   milestone creates/updates;
-   issue creates/updates;
-   Project item additions;
-   Project field-value updates;
-   dependency additions;
-   duplicate removals, expected to be zero unless separately and
    explicitly justified by live state;
-   repository mutations;
-   Git transport mutations;
-   Release 1.9 mutations.

Distinguish mutations inherited from the prior partial execution from
mutations performed under this corrective authority.

## Required Final Read-Back

Before declaring success, independently reread:

-   milestone #56;
-   all 13 Release 1.8 issues;
-   Project #2 membership for all 13;
-   Status/Priority/Release/Area for all 13;
-   dependency chain;
-   duplicate Project membership;
-   milestones #49, #50, #51, and #55;
-   Release 1.7 issue state;
-   Release 1.8 branch/PR absence;
-   repository/Git cleanliness.

Do not infer success solely from mutation command return codes.

## Required Report

Report:

-   frozen repository baseline;
-   exact partial state discovered for #56/#211/#212;
-   root cause interpretation of the prior `add_000` result based on
    read-back;
-   preserved objects;
-   all corrective mutations;
-   newly created WP issues and their actual numbers;
-   Project membership and duplicate count;
-   Status/Priority/Release/Area reconciliation;
-   dependency count and drift;
-   milestone final state;
-   predecessor/taxonomy preservation;
-   Release 1.8 implementation absence;
-   Release 1.9 mutation count;
-   repository/Git mutation counts;
-   execution-only file state;
-   both Git diff checks;
-   exact next authorized action.

If successful, terminate with exactly:

`RELEASE 1.8 PARTIAL GITHUB PLANNING RECONCILIATION COMPLETE`

`RELEASE 1.8 GITHUB PLANNING COMPLETE`

`RELEASE 1.8 READY FOR WP01 AUTHORIZATION`

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

Do not execute WP01.

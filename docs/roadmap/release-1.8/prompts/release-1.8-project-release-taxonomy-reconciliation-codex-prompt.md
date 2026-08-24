# Release 1.8 --- Project Release Taxonomy Reconciliation Codex Authority

## Authority

You are authorized to perform one narrowly scoped GitHub Project
taxonomy reconciliation for
`samuel-santos-engineer/AIQuantTradingResearch`.

Purpose: add exactly one missing single-select option, `1.8`, to the
existing `Release` field of GitHub Project #2 so the already-authorized
Release 1.8 GitHub Planning workflow can proceed.

This authority does not authorize creation of the Release 1.8 milestone,
WP issues, Project items, dependency edges, repository changes,
implementation, Git integration, or Release 1.9 work.

## Frozen Repository Baseline

Release 1.7 remains frozen at:

-   branch: `main`
-   commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   schema: v3
-   permanent tests: 268/268
-   Release 1.7: CLOSED

Do not modify repository content or retrospectively review Release
1.1--1.7.

## GitHub API Basis

GitHub's supported GraphQL schema provides
`updateProjectV2Field(input: UpdateProjectV2FieldInput!)`.

For a SINGLE_SELECT field, `singleSelectOptions` is a replacement set,
not an append-only delta. Therefore every existing option must first be
read and then supplied unchanged, including each existing option ID,
name, color, and description. Existing IDs must be preserved so existing
Project item field values are not cleared.

Use only the live GitHub GraphQL schema exposed through authenticated
`gh api graphql`. Do not invent field IDs, option IDs, enum values, or
mutation signatures.

## Mandatory Read-Only Starting-State Gate

Before any mutation verify:

1.  repository identity is
    `samuel-santos-engineer/AIQuantTradingResearch`;
2.  branch is `main`;
3.  HEAD is `f8e521af2c5262d6cc173d0731b5e915dbceac0a`;
4.  `main == origin/main`;
5.  ahead/behind is `0/0`;
6.  staged paths are zero;
7.  tracked repository state is clean;
8.  GitHub authentication is valid for `samuel-santos-engineer`;
9.  token authorization is sufficient for Project #2 mutation;
10. Project #2 resolves uniquely and is the expected
    AIQuantTradingResearch roadmap project;
11. exactly one existing field named `Release` resolves;
12. that field is SINGLE_SELECT;
13. all current Release options can be retrieved with `id`, `name`,
    `color`, and `description`;
14. option names are unique;
15. `1.8` is absent;
16. existing Release options through `1.7` are present and no
    unexplained taxonomy state exists;
17. existing item assignments using the Release field can be read
    sufficiently to validate preservation after mutation;
18. the live GraphQL schema confirms `updateProjectV2Field` and the
    required `UpdateProjectV2FieldInput.singleSelectOptions` shape.

If `1.8` already exists exactly once, perform no mutation, validate the
existing taxonomy, and report reconciliation as already satisfied.

If any state is ambiguous, incomplete, duplicated, incompatible, or
differs materially from this authority, stop before mutation and report
the smallest corrective authority required.

## Pre-Mutation Preservation Snapshot

Before mutation capture:

-   Project #2 node ID;
-   Release field node ID;
-   complete ordered option set;
-   for every option: exact `id`, `name`, `color`, and `description`;
-   count of options;
-   existing Project item Release assignments sufficient to prove that
    option identities/assignments survive the update.

Do not rely on display names alone when IDs are available.

## Authorized Mutation

Exactly one GitHub mutation is authorized:

Update the existing Project #2 `Release` SINGLE_SELECT field through the
documented GraphQL `updateProjectV2Field` mutation so that:

-   every pre-existing option is supplied with its existing ID;
-   every pre-existing option preserves its exact name;
-   every pre-existing option preserves its exact color;
-   every pre-existing option preserves its exact description;
-   existing option order is preserved;
-   exactly one new option named `1.8` is appended;
-   no existing option is deleted, renamed, recolored, reordered, or
    recreated.

For the new `1.8` option only, use a color and description consistent
with the existing Release-field convention discovered during read-only
reconciliation. Do not guess if no deterministic convention can be
established; stop instead.

Do not update the Release field name.

Do not execute a second mutation to repair an incorrect first mutation.
If the constructed mutation cannot be proven correct before submission,
stop.

## Mutation Construction Safety

Prefer a variables-based GraphQL invocation so option names/descriptions
are not interpolated unsafely into the mutation document.

Before submission, mechanically compare the proposed complete option
payload against the captured state and prove:

-   old option count is N;
-   proposed option count is N+1;
-   old option IDs are present exactly once;
-   all old option tuples `(id, name, color, description)` are
    unchanged;
-   only the new `1.8` option lacks an existing ID;
-   `1.8` occurs exactly once;
-   no unrelated field input is present.

Only then submit the single mutation.

## Immediate Post-Mutation Read-Back

Immediately reread Project #2 and the Release field and prove:

1.  the Release field has the same field ID and name;
2.  every pre-existing option retains the same option ID;
3.  every pre-existing option retains exact name, color, and
    description;
4.  every pre-existing option remains exactly once;
5.  `1.8` exists exactly once;
6.  total option count increased by exactly one;
7.  existing Project item Release assignments remain semantically
    unchanged;
8.  no unrelated Project field or option changed;
9.  no Project item was added, removed, or modified by this authority;
10. no milestone, issue, dependency, PR, branch, tag, or Release
    changed.

If read-back reveals unexpected drift, stop and report it. Do not
perform compensating mutations without new human authority.

## Explicitly Prohibited Mutations

Do not:

-   create or delete a Project field;
-   create Release 1.8 milestone;
-   create WP01--WP13 issues;
-   add issues to Project #2;
-   modify Status, Priority, Area, or any other field;
-   modify existing Release option semantics;
-   modify Project item field values;
-   create dependency edges;
-   modify milestones #49, #50, #51, or #55;
-   modify issues #197--#209;
-   create or modify Release 1.9/2.0 planning;
-   edit repository files;
-   stage files;
-   commit;
-   push;
-   create branches or PRs;
-   merge;
-   tag;
-   create a GitHub Release;
-   install Python or packages;
-   create a virtual environment;
-   execute Release 1.8 implementation.

## Repository and Git Validation

After GitHub read-back verify:

-   branch remains `main`;
-   HEAD remains the frozen baseline;
-   `main == origin/main`;
-   ahead/behind remains `0/0`;
-   staged paths remain zero;
-   tracked repository content remains unchanged;
-   `git diff --check` passes;
-   `git diff --cached --check` passes.

## Mutation Accounting

Expected successful mutation accounting:

-   Project field updates: exactly 1, unless `1.8` already existed and
    zero mutation was required;
-   new Release options: exactly 1;
-   milestone mutations: 0;
-   issue mutations: 0;
-   Project item mutations: 0;
-   dependency mutations: 0;
-   repository mutations: 0;
-   Git transport mutations: 0.

## Execution-Only Authority Lifecycle

This authority pair is execution-only and must not be staged or
committed.

After successful validation, remove only this corrective authority pair
if that matches the established execution-only authority lifecycle.
Otherwise leave it untracked and report it explicitly.

Do not remove the governed Release 1.8 planning artifacts or the
existing Release 1.8 GitHub Planning authority pair.

## Required Report

Report:

-   frozen repository starting state;
-   authenticated GitHub account and required scope/capability result;
-   Project #2 and Release field IDs;
-   complete pre-mutation Release option names/count;
-   whether `1.8` was initially absent;
-   GraphQL schema validation result;
-   preservation-payload validation;
-   mutation count;
-   complete post-mutation Release option names/count;
-   preservation of all existing option IDs and metadata;
-   preservation of existing Project item Release assignments;
-   all prohibited mutation counts;
-   final repository/Git state;
-   execution-only cleanup;
-   exact next authorized action.

If successful, terminate with exactly:

`RELEASE 1.8 PROJECT RELEASE TAXONOMY RECONCILIATION COMPLETE`

`PROJECT #2 RELEASE OPTION 1.8 READY`

`NEXT AUTHORIZED ACTION: Resume Release 1.8 GitHub Planning using the existing GitHub Planning authority.`

Do not execute Release 1.8 GitHub Planning under this authority.

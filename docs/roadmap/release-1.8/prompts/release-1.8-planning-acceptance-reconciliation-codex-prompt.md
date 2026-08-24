# Release 1.8 --- Planning Acceptance Reconciliation Codex Authority

## Authority

You are authorized to perform one narrowly scoped governance
reconciliation for Release 1.8 in
`samuel-santos-engineer/AIQuantTradingResearch`.

The purpose is solely to reconcile the lifecycle status of the already
prepared Release 1.8 planning artifacts with the explicit human
acceptance that occurred after their creation. This authority does not
authorize Release 1.8 GitHub Planning, implementation, Python
installation, Git integration, or Release 1.9 work.

## Frozen Predecessor

Release 1.7 remains frozen at:

-   Commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   Tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   Schema: v3
-   Permanent tests: 268/268
-   Release 1.7: CLOSED

Do not modify or retrospectively review Release 1.1--1.7 implementation.

## Governed Release 1.8 Planning Artifacts

Read completely before mutation:

-   `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`

Read the existing Release 1.8 GitHub Planning authority for context
only. Do not execute it.

## Human Acceptance

Human acceptance of the existing Release 1.8 plan is explicit and
authoritative.

The accepted release is **Release 1.8 --- Python & AI Engineering
Foundation**. The accepted planning model is the existing three-file
definition, execution plan, and file manifest, including the
already-defined WP01--WP13 sequence.

The stale lifecycle statement in `RELEASE_1.8_DEFINITION.md`:

`Planning authority candidate — awaiting human acceptance.`

no longer reflects the actual governance state.

## Mandatory Starting-State Gate

Before mutation verify:

1.  repository is `samuel-santos-engineer/AIQuantTradingResearch`;
2.  branch is `main`;
3.  HEAD is `f8e521af2c5262d6cc173d0731b5e915dbceac0a`;
4.  local `main == origin/main`;
5.  ahead/behind is `0/0`;
6.  staged paths are zero;
7.  no tracked repository-content mutation exists outside expected
    untracked Release 1.8 planning inputs;
8.  all three governed Release 1.8 planning files exist and can be read
    completely;
9.  their substantive scope, WP01--WP13 sequence, exclusions,
    boundaries, and manifest intent are mutually consistent;
10. the only acceptance-state contradiction requiring correction is
    stale candidate/awaiting-acceptance wording;
11. Release 1.8 implementation has not begun;
12. Release 1.9 implementation has not begun.

If any condition fails, stop before mutation and report the exact
conflict and smallest corrective authority required.

## Authorized Mutation

Make only the minimum textual lifecycle-status correction necessary to
state truthfully that the existing Release 1.8 planning artifacts have
received human acceptance.

At minimum replace:

`Planning authority candidate — awaiting human acceptance.`

with:

`Human-accepted Release 1.8 planning authority.`

If the same stale acceptance state is repeated elsewhere within the
three governed planning artifacts, normalize only those directly
contradictory lifecycle-status statements so the three files agree.

Do not rewrite surrounding prose for style and do not alter substantive
planning decisions.

## Explicit Prohibitions

Do not change Release 1.8 objective/title; WP01--WP13 count, titles,
order, dependencies, scope, ownership, or acceptance intent;
Python-version decision boundaries; machine-wide versus project-local
Python rules; dependency-governance decisions;
NumPy/pandas/scikit-learn/Streamlit scope; .NET↔Python
architecture/proof boundaries; schema v3; production code; tests or test
counts; package/project/reference structure; predecessor content; GitHub
objects; branches; commits; pushes; PRs; merges; tags; GitHub Releases;
Python installations; virtual environments; Python packages; or Release
1.9.

Do not stage any file.

## GitHub Prohibition

Do not use this authority to solve the separately discovered GitHub
Project prerequisites, including GitHub CLI `project` scope, Project #2
`Release = 1.8`, milestone creation, or WP01--WP13 issue creation. Those
remain owned by the separate GitHub Planning workflow.

## Validation

After correction:

1.  reread all three planning artifacts completely;
2.  verify stale `awaiting human acceptance` state is absent from
    governed Release 1.8 planning content;
3.  verify accepted-authority status is explicit;
4.  verify substantive planning semantics are unchanged;
5.  verify WP01--WP13 remain exactly 13 work packages in their existing
    order;
6.  verify the manifest remains consistent with definition and execution
    plan;
7.  verify production/test/schema/package/project/reference deltas are
    zero;
8.  verify GitHub mutations are zero;
9.  verify Git transport mutations are zero;
10. verify staged paths remain zero;
11. run `git diff --check`;
12. run `git diff --cached --check`;
13. verify terminal newlines and trailing whitespace;
14. account exactly for every changed/untracked path.

Canonical build/test execution is not required solely for this text-only
lifecycle correction unless existing repository governance explicitly
requires it.

## Execution-Only Authority Lifecycle

This authority pair is execution-only input and is not part of the
governed Release 1.8 planning deliverable. It must not be staged or
committed.

If repository conventions permit mechanical cleanup after successful
validation, remove only this reconciliation authority pair. Otherwise
leave it untracked and report it explicitly.

Do not remove the three governed Release 1.8 planning artifacts or the
separate GitHub Planning authority pair.

## Required Report

Report starting branch/HEAD/synchronization; exact stale lifecycle
statement; every file changed; final lifecycle status;
substantive-semantic preservation; WP count/order;
production/test/schema/package/project/reference deltas; GitHub and Git
transport mutation counts; execution-only cleanup; final
changed/untracked/staged state; both diff-check results; and the exact
next authorized action.

If successful, terminate with exactly:

`RELEASE 1.8 PLANNING ACCEPTANCE RECONCILIATION COMPLETE`

`RELEASE 1.8 PLANNING AUTHORITY HUMAN-ACCEPTED`

`NEXT AUTHORIZED ACTION: Resume Release 1.8 GitHub Planning using the existing GitHub Planning authority.`

Do not execute GitHub Planning.

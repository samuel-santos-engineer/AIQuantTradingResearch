# Release 1.13 WP05 --- PR #301 Architecture Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

-   **GPT-5.6 Luna** has completed substantive architecture acceptance.
-   **GPT-5.6 Terra** owns this bounded merge and post-merge lifecycle
    operation.
-   **GPT-5.6 Sol** may support analysis only and may not alter accepted
    architecture or lifecycle governance.

## Lifecycle target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Pull request:

`#301`

Expected title/scope: Release 1.13 WP05 historical bridge architecture
documentation.

Expected branch:

`docs/release-1.13-wp05-bridge-architecture`

Expected pre-merge canonical `origin/main` / base lineage:

`cc96c638b3c255499412bd79aca3953cb2973902`

Initial architecture candidate:

`2387b8d466d723dd5213ecb9bb9aa20a871d3ab0`

**Final Luna-accepted head:**

`6668b915e57b6cea82846b75136be9ea54ab5aae`

The final accepted head supersedes the initial candidate for lifecycle
purposes.

## Acceptance evidence

Luna reported:

`RELEASE 1.13 WP05 HISTORICAL BRIDGE ARCHITECTURE FINAL SUBSTANTIVE ACCEPTANCE: PASS`

During acceptance, the architecture's concurrency gap was corrected
within the authorized three-document scope.

The accepted architecture now explicitly includes:

-   additive bounded one-shot local Worker/stdio bridge;
-   fixed runtime invocation:
    `dotnet /app/worker/AIQuantTradingResearch.Worker.dll`;
-   WP04 remains sole cache/freshness/provider authority;
-   bounded schema-free per-cache-key cross-process lock;
-   cache recheck after lock acquisition;
-   lease expiry;
-   abandoned-lock recovery;
-   no Python/browser provider bypass;
-   preservation of legacy Worker/visualization behavior;
-   Azure F1 zero-cost topology.

Do not reinterpret or redesign these decisions during lifecycle
execution.

## Exact accepted path set

Exactly these three paths are authorized to land:

1.  `docs/roadmap/release-1.13/RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`
2.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
3.  `docs/roadmap/release-1.13/prompters/release-1.13-wp05-luna-extended-f1-historical-request-read-model-bridge-architecture-authority.md`

Root `README.md` must remain unchanged.

No implementation file belongs in PR #301.

## Purpose

Perform the established Release 1.13 lifecycle sequence:

1.  verify the live PR still exactly matches the Luna-accepted
    candidate;
2.  rerun bounded pre-merge validation;
3.  merge PR #301 using the repository's normal merge method;
4.  capture the actual merge commit/new canonical `main`;
5.  verify the accepted head is reachable from canonical `main`;
6.  verify exactly the three accepted documentation paths landed;
7.  rerun post-merge validation;
8.  preserve WP05 issue/milestone state because WP05 implementation is
    not complete.

This authority does not authorize WP05 implementation.

## Pre-merge verification

Before merge verify live GitHub/repository truth:

-   PR #301 exists;
-   PR is Open;
-   PR is not merged;
-   PR is not draft;
-   base is `main`;
-   base lineage remains compatible with
    `cc96c638b3c255499412bd79aca3953cb2973902`;
-   branch is `docs/release-1.13-wp05-bridge-architecture`;
-   head is exactly `6668b915e57b6cea82846b75136be9ea54ab5aae`;
-   PR is mergeable under normal repository protections;
-   exactly three changed paths;
-   exact path set equals the accepted path set above;
-   README absent from diff;
-   no production/test/package/Docker/Azure/schema/deployment path;
-   issue #292 remains Open;
-   Project WP05 remains non-Done / expected Backlog;
-   milestone #64 remains Open;
-   no Release 1.13 tag or GitHub Release exists from this work.

## Head-drift rule

If PR #301 head differs from:

`6668b915e57b6cea82846b75136be9ea54ab5aae`

do not merge automatically.

Inspect the drift.

A substantive architecture/documentation change requires renewed GPT-5.6
Luna substantive acceptance.

A purely lifecycle-generated non-substantive change may proceed only if
repository precedent clearly permits it and it does not alter the
accepted architecture or three-path scope.

When uncertain, stop at governance.

## Pre-merge content verification

Confirm the accepted ADR still freezes:

-   one-shot Worker/stdio historical query mechanism;
-   fixed local runtime invocation;
-   canonical 2 × 3 × 4 request matrix;
-   presentation-safe response contract;
-   stdout protocol / stderr diagnostics discipline;
-   bounded timeout/cancellation;
-   safe subprocess invocation;
-   WP04 no-bypass ownership;
-   schema-free per-cache-key cross-process locking;
-   cache recheck under lock;
-   lease expiry and abandoned-lock recovery;
-   Streamlit rerun behavior;
-   no bridge invocation from chart hover/zoom/pan;
-   F1 resource constraints;
-   no new managed service;
-   no new schema;
-   preservation of legacy Worker behavior.

Do not edit architecture content during this lifecycle unless a purely
mechanical correction is required and separately permitted by
governance. A substantive correction requires Luna.

## Pre-merge validation

Run at least:

-   exact changed-path proof;
-   `git diff --check` against canonical base;
-   secret scan appropriate to changed documentation;
-   README byte/diff proof;
-   repository cleanliness/staging check;
-   PR mergeability/protection check.

Because this is documentation-only, do not create unrelated code changes
merely to rerun implementation tests.

If repository lifecycle precedent requires a build/test smoke check
before merge, run it without mutation and report it.

## Merge method

Use the same normal GitHub merge-commit lifecycle pattern established
for Release 1.13 WP01-WP04.

Do not:

-   direct-push to `main`;
-   force push canonical `main`;
-   bypass branch protection;
-   squash/rebase unless that is demonstrably the established required
    method for this PR;
-   alter the accepted candidate to make merging easier.

Capture:

-   merge timestamp if available;
-   actual merge commit SHA;
-   resulting canonical `origin/main` SHA.

The actual merge commit becomes the predecessor for the revised Terra
WP05 implementation authority.

## Post-merge reachability

After merge prove:

`6668b915e57b6cea82846b75136be9ea54ab5aae`

is reachable from the new canonical `main`.

Do not rely solely on the PR API's prospective `merge_commit_sha` field
from before merge.

Use actual post-merge repository/GitHub evidence.

## Post-merge changed-path proof

Prove the merge introduced exactly the accepted three documentation
paths relative to the pre-merge canonical main:

1.  `docs/roadmap/release-1.13/RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`
2.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
3.  `docs/roadmap/release-1.13/prompters/release-1.13-wp05-luna-extended-f1-historical-request-read-model-bridge-architecture-authority.md`

Verify:

-   README unchanged;
-   no C# changed;
-   no Python changed;
-   no tests changed;
-   no dependency manifest changed;
-   no Docker/container changed;
-   no schema changed;
-   no Azure/GHCR/deployment changed.

## Post-merge validation

After canonical main advances:

-   fetch/update canonical `origin/main`;
-   prove merge commit;
-   prove accepted-head ancestry;
-   rerun `git diff --check`/whitespace validation over landed change;
-   rerun secret scan over landed documentation;
-   verify working tree/staging remains clean except unrelated
    pre-existing local authority records, which must not be touched;
-   verify architecture document is readable from canonical main;
-   verify file manifest contains the accepted architecture reservation;
-   verify README remains unchanged.

## WP05 lifecycle state

**Do not close issue #292.**

**Do not mark WP05 Done.**

This PR merges an architecture prerequisite inside WP05; it does not
complete WP05.

After successful merge:

-   issue #292 remains Open;
-   Project WP05 remains Backlog/non-Done unless an existing automation
    changes a non-completion field in a harmless way;
-   Project Release remains `1.13`;
-   milestone #64 remains Open;
-   issues #293-#295 remain Open.

If issue-close automation would only trigger upon closing #292, do not
trigger it.

## Tag/release boundary

Do not create:

-   Release 1.13 tag;
-   GitHub Release;
-   deployment marker;
-   release acceptance artifact.

WP05 and Release 1.13 remain incomplete.

## Implementation boundary

Do not implement:

-   Worker historical-query mode;
-   request/response contracts in code;
-   cross-process lock;
-   Python subprocess consumer;
-   Streamlit Market Research chart;
-   Plotly;
-   UI tests;
-   Docker changes;
-   deployment.

The next implementation authority is separate and must be bound to the
new canonical merge commit.

## Root README protection

`README.md` remains byte-for-byte protected.

Do not modify it for roadmap, architecture, implementation preparation,
or lifecycle notes.

## Retry-until-governance-boundary rule

Do not stop merely because a fetch, checkout, mergeability check,
documentation validation, secret scan, whitespace check, or ordinary
Git/GitHub lifecycle operation initially fails.

Diagnose and correct ordinary lifecycle/tooling problems permitted by
this authority, rerun the relevant checks, and continue.

Do not mutate the accepted architecture to solve a lifecycle problem.

Stop only if the next action requires crossing a governance boundary,
including:

-   accepted head drift requiring substantive review;
-   extra changed path;
-   README mutation;
-   production/test/dependency/Docker/schema/deployment mutation;
-   branch-protection bypass;
-   force/direct push to main;
-   architecture redesign;
-   WP05 implementation;
-   issue #292 closure;
-   Project Done mutation;
-   tag/release creation.

When blocked report:

`GOVERNANCE RESTRICTION — STOPPED`

with exact evidence and required authority.

## Final lifecycle verification

Before PASS verify:

-   PR #301 merged normally;
-   actual merge commit captured;
-   canonical `origin/main` equals/contains that merge;
-   accepted head reachable;
-   exactly three paths landed;
-   README unchanged;
-   whitespace clean;
-   secret scan clean;
-   no implementation/dependency/schema/Docker/Azure/deployment
    mutation;
-   issue #292 still Open;
-   Project WP05 non-Done;
-   milestone #64 Open;
-   #293-#295 Open;
-   no Release 1.13 tag;
-   no GitHub Release;
-   WP05 implementation not started.

## Required final report

Return:

-   pre-merge canonical main;
-   PR #301 state before merge;
-   accepted head;
-   branch;
-   merge method;
-   actual merge commit;
-   resulting canonical `origin/main`;
-   accepted-head reachability result;
-   exact landed paths;
-   pre-merge validation;
-   post-merge validation;
-   whitespace result;
-   secret-safety result;
-   README proof;
-   architecture-content preservation finding;
-   issue #292 state;
-   Project WP05 state;
-   milestone #64 state;
-   #293-#295 states;
-   tag/release state;
-   confirmation no implementation work occurred;
-   technical lifecycle failures corrected;
-   governance restrictions encountered;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP05 ARCHITECTURE PR #301 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP05 ARCHITECTURE PR #301 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS authorizes no implementation by itself. The resulting canonical
`origin/main` SHA must be used as the predecessor for a new GPT-5.6
Terra WP05 bridge/UI implementation authority.

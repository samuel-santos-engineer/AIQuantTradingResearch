# Release 1.13 WP01 --- PR #297 Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

This is a bounded lifecycle operation following successful GPT-5.6 Luna
substantive acceptance.

Model roles remain:

-   **GPT-5.6 Luna** --- owns the accepted WP01 contract, architecture,
    engineering selections, and substantive acceptance.
-   **GPT-5.6 Terra** --- owns this explicitly authorized merge and
    empirical post-merge verification operation.
-   **GPT-5.6 Sol** --- supporting analysis only; no merge or governance
    authority.

Terra must not reinterpret or expand Luna's accepted WP01 contract.

## Repository and target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Target:

`PR #297 — Release 1.13 WP01 contract and engineering selection`

Expected base before merge:

`main @ 2621a3355aa5b28f1541c482d8053358b4ea814e`

Expected accepted head:

`02d9025477a65282aca2f8fbf953488f9ac1d41d`

Expected branch:

`docs/release-1.13-wp01-contract`

The candidate has passed:

`RELEASE 1.13 WP01 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Verify live repository truth before acting.

## Purpose

This authority permits Terra to:

1.  verify that PR #297 still exactly represents the Luna-accepted WP01
    candidate;
2.  rerun bounded lifecycle validation;
3.  correct only ordinary lifecycle/technical defects when correction is
    possible without changing the accepted substantive contract or
    crossing its mutation boundary;
4.  merge PR #297 through the repository's normal merge mechanism when
    all gates pass;
5.  verify the resulting canonical `origin/main`;
6.  reconcile WP01 issue/Project lifecycle state according to
    established repository governance;
7.  establish the predecessor baseline for the separately authorized
    WP02 authority.

This authority does not authorize WP02 implementation.

## Accepted candidate boundary

Expected changed paths are exactly:

-   `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/roadmap/release-1.13/prompters/release-1.13-wp01-luna-contract-architecture-ui-acceptance-engineering-selection.md`

Root `README.md` is protected and must remain unchanged.

No application source, tests, package/dependency manifests,
schema/database, Azure, Docker/GHCR, provider runtime, tags, GitHub
Releases, Release 1.14 implementation, or Release 2.0 implementation may
be introduced.

## Pre-merge verification

Before merge, verify:

-   PR #297 remains open and unmerged;
-   base is `main`;
-   accepted head SHA remains
    `02d9025477a65282aca2f8fbf953488f9ac1d41d`, unless a subsequent
    Luna-authorized correction has explicitly superseded it;
-   no commit has been added after substantive acceptance without
    appropriate review;
-   PR is mergeable;
-   required repository checks/protections are satisfied;
-   changed paths remain exactly within the accepted boundary;
-   root `README.md` is absent from the diff;
-   `git diff --check` passes;
-   repository-standard secret scanning passes;
-   no source/test/package/schema/deployment/runtime mutation exists;
-   no hidden WP02 implementation exists;
-   issue #288 remains the WP01 issue under Release 1.13;
-   milestone #64 remains the Release 1.13 milestone;
-   milestone #65 remains the Release 1.14 reservation.

If the head has changed after Luna acceptance, do not assume the new
head is accepted. Inspect the change. A purely technical correction
already allowed and substantively neutral may be validated; any
substantive contract change requires renewed Luna acceptance.

## Continue-until-governance-boundary rule

Do not stop because of an ordinary technical/lifecycle failure.

Within this authority, continue diagnosing, correcting where permitted,
retrying, and rerunning validation until the merge/post-merge operation
succeeds or the next necessary action crosses a governance boundary.

Correctable examples include:

-   transient Git/GitHub errors;
-   branch synchronization issues that do not alter the accepted
    contract;
-   whitespace/lifecycle defects;
-   an authorized metadata/status update failure;
-   validation command execution errors that can be safely corrected;
-   other mechanical defects that leave the Luna-accepted contract
    unchanged.

After correction, rerun the affected validation.

Stop only when further progress would require governance expansion,
including:

-   changing the Luna-accepted substantive contract;
-   modifying a path outside the accepted three-path candidate boundary;
-   modifying root `README.md`;
-   application/test implementation;
-   package/dependency mutation;
-   schema/database mutation;
-   provider market-data execution;
-   Azure/Docker/GHCR mutation;
-   bypassing required repository protection/review;
-   force-pushing or rewriting history without explicit authority;
-   starting WP02;
-   implementing Release 1.14 or 2.0;
-   exposing secrets;
-   incurring prohibited cost;
-   making an owner-reserved decision.

Report corrected ordinary failures as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Report a true stop as:

`GOVERNANCE RESTRICTION — STOPPED`

## Merge authority

If all gates pass, this document explicitly authorizes normal merge of
PR #297 into `main`.

Use repository-standard merge behavior.

Do not bypass protections.

Do not create a tag.

Do not create a GitHub Release.

Do not close milestone #64.

Do not start WP02.

## WP01 issue and Project lifecycle

After successful merge and post-merge verification, inspect the
established Release 1.12 issue/Project lifecycle pattern.

Because WP01 has already received Luna substantive acceptance and is now
merged, update issue #288 and its Project #2 status only to the state
established by repository governance for a fully accepted and merged
work package.

If established governance clearly requires:

-   WP01 issue closure; and/or
-   Project Status `Done`,

perform those bounded lifecycle updates.

Do not invent a new lifecycle state.

Do not change the Release field away from `1.13`.

Do not modify WP02--WP08 lifecycle state.

If repository precedent is ambiguous about issue closure/status after
accepted merge, leave #288 open/non-Done and report the ambiguity rather
than inventing policy.

## Post-merge verification

After merge:

1.  fetch/update from `origin`;
2.  establish canonical `origin/main` SHA;
3.  prove the merge is reachable from `origin/main`;
4.  verify all three accepted WP01 files are present with the accepted
    content;
5.  verify root `README.md` was not changed by this operation;
6.  verify no unexpected path entered the merge;
7.  rerun appropriate whitespace/secret/scope checks against the merged
    result;
8.  verify Release 1.13 Definition, Execution Plan, File Manifest, and
    WP01 Contract remain internally coherent;
9.  verify Twelve Data preservation remains contractual;
10. verify no WP02 implementation exists;
11. verify milestone #64 remains open;
12. verify milestone #65 remains a Release 1.14 reservation;
13. verify issue #288 / Project #2 state after any authorized lifecycle
    reconciliation;
14. verify issues #289--#295 remain unexecuted;
15. verify no tag, GitHub Release, Azure deployment, Docker/GHCR
    operation, provider call, package mutation, or schema mutation
    occurred.

The resulting `origin/main` SHA becomes the canonical predecessor for
Release 1.13 WP02.

## WP02 boundary

Successful WP01 merge means the architecture contract is canonical.

It does not itself authorize WP02.

WP02 requires a separate Markdown authority with:

-   `release-1.13-` filename prefix;
-   explicit **GPT-5.6 Terra** selection;
-   dependency on accepted/merged WP01;
-   exact literal source/test path allowlist;
-   provider-independent abstraction implementation boundary;
-   tests/validation;
-   preserved Twelve Data behavior;
-   no Vike adapter implementation unless explicitly within the later
    governed WP;
-   no cache/UI implementation;
-   continue-until-governance-boundary rule;
-   separate acceptance/lifecycle handling.

## Required final report

Report:

-   pre-merge `main` SHA;
-   PR #297 state/base/head;
-   exact changed paths;
-   validation results;
-   confirmation that the head still matched the Luna-accepted
    candidate;
-   technical failures encountered/corrected;
-   governance restrictions encountered;
-   merge method;
-   merge commit SHA;
-   canonical post-merge `origin/main` SHA;
-   root README unchanged confirmation;
-   WP01 issue #288 final state;
-   Project #2 WP01 final status and Release field;
-   milestone #64 state;
-   milestone #65 state;
-   issues #289--#295 unchanged confirmation;
-   confirmation that no
    implementation/provider/package/schema/Azure/Docker/GHCR/tag/GitHub
    Release mutation occurred;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP01 PR #297 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP01 PR #297 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS establishes the WP01 contract on `main`. It does not authorize
WP02 implementation.

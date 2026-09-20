# Release 1.13 WP08 --- Terra Final Governance Publication and Release Lifecycle Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the mechanical governance publication and Release
1.13 lifecycle actions authorized here. GPT-5.6 Luna's fresh substantive
acceptance is authoritative and must not be reinterpreted or weakened.
GPT-5.6 Sol may support mechanical reconciliation only.

## Accepted canonical boundary

Canonical `origin/main` before this lifecycle:

`fb946d77d0e8acb34d1bd91865bb62b2aad24160`

Fresh Luna substantive acceptance marker:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: PASS`

Accepted state:

-   PR #304: Merged
-   PR #305: Merged
-   issue #294: Closed / Done
-   issue #295: Open / Backlog
-   milestone #64: Open
-   Release 1.13 tag: absent
-   Release 1.13 GitHub Release: absent
-   root `README.md`: unchanged and protected
-   no implementation commits after the accepted WP07 implementation
    boundary
-   corrected acceptance artifact exists locally, untracked and
    unpublished

## Purpose

Publish the Luna-accepted Release 1.13 acceptance record through the
repository's normal governed workflow, establish its canonical merge
boundary, then complete the Release 1.13 issue/milestone/tag/GitHub
Release lifecycle.

This authority is **mechanical lifecycle authority**, not a new
substantive acceptance review.

Do not change implementation or reinterpret the accepted release
contract.

## Mandatory pre-mutation identity gate

Before changing repository or GitHub state, independently verify:

-   `origin/main` is exactly `fb946d77d0e8acb34d1bd91865bb62b2aad24160`;
-   no commit has appeared after Luna's PASS;
-   PR #304 and PR #305 remain merged;
-   issue #294 remains Closed / Done;
-   issue #295 remains Open / Backlog;
-   milestone #64 remains Open;
-   no Release 1.13 tag exists;
-   no Release 1.13 GitHub Release exists;
-   root README remains unchanged;
-   the acceptance artifact remains untracked;
-   no source/test/dependency/schema/deployment/configuration drift
    exists.

If canonical `main` or any substantive evidence changed after Luna PASS,
stop and require renewed Luna reconciliation.

## Literal publication allowlist

The governance publication change may contain exactly:

`docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`

No other file may enter the publication commit/PR.

Before publication, inspect the complete artifact and prove it is the
same artifact Luna accepted except for the narrowly authorized
final-status update below.

## Final acceptance status update

Because Luna has now issued substantive PASS, update only the
acceptance-result field/text that remained intentionally pending during
Luna review.

Expected machine-readable transition, if present:

`WP08SubstantiveAcceptanceResult=PENDING_LUNA_RECONCILIATION`

to:

`WP08SubstantiveAcceptanceResult=PASS`

Record the Luna marker:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: PASS`

Do not alter substantive evidence, architecture conclusions, regional
reconciliation, historical chronology, limitations, or exclusions while
making this status update.

The acceptance artifact must continue to identify:

-   **Acceptance/governance model: GPT-5.6 Luna**
-   **Documentary/lifecycle execution model: GPT-5.6 Terra**, where the
    artifact records execution roles.

## Publication PR

Create the smallest normal repository branch/commit/PR containing only
the accepted `RELEASE_1.13_ACCEPTANCE.md`.

Record:

-   branch name;
-   commit SHA;
-   PR number;
-   base SHA;
-   head SHA;
-   exact changed path.

Run repository-native documentary checks, including:

-   whitespace/trailing-whitespace check;
-   exact changed-path check;
-   README protection;
-   dependency/schema/source/test protection;
-   secret scan/check appropriate to the repository.

Do not add authority files to the PR.

## Pre-merge publication gate

Before merging the acceptance PR, verify:

-   exactly one changed path;
-   acceptance artifact content matches Luna-accepted evidence plus only
    the authorized PASS-status transition;
-   no unsupported claim was introduced;
-   required checks pass;
-   PR is clean and mergeable;
-   base still descends from the Luna-accepted canonical boundary;
-   no intervening change invalidates substantive acceptance.

If any substantive content changes beyond the authorized status
transition, stop for Luna review.

## Merge authority

If the publication gate passes, merge the acceptance PR using the
repository's normal merge method.

Record:

-   merge method;
-   publication PR final state;
-   merge commit;
-   new full canonical `main` SHA;
-   reachability of the publication head from canonical `main`.

After merge, verify the canonical acceptance document is present and
matches the approved content.

## WP08 lifecycle completion

Only after the acceptance artifact is canonically merged and post-merge
verification passes:

-   close issue #295;
-   allow/verify its existing Project #2 item transitions to `Done`
    according to established repository workflow;
-   if closure automatically moves the Project item, record that and do
    not perform redundant mutation;
-   verify issue #294 remains Closed / Done.

Do not close the milestone until WP08 issue completion is verified.

## Milestone completion

After WP08 is closed/Done and all Release 1.13 milestone work is
reconciled:

-   verify milestone #64 contains no unresolved Release 1.13 work that
    should remain open;
-   close milestone #64.

If another open issue legitimately belongs to milestone #64, stop and
report rather than closing it prematurely.

## Release tag

After canonical acceptance publication, WP08 completion, and milestone
closure:

1.  identify the final canonical `main` SHA;
2.  create the repository's established Release 1.13 version tag at
    exactly that canonical SHA;
3.  use the repository's existing tag naming convention.

Expected version is Release 1.13; determine the exact established tag
spelling from repository precedent rather than inventing a new
convention.

Do not move or overwrite an existing tag.

If a conflicting Release 1.13 tag unexpectedly exists, stop.

## GitHub Release

After the tag is successfully created and verified, create the Release
1.13 GitHub Release using repository precedent.

Release notes must remain factual and concise and should summarize the
accepted Release 1.13 scope:

-   provider-independent historical market-data architecture;
-   BTC/USD and ETH/USD public historical visualization;
-   1h/4h/1d intervals and 1D/7D/30D/90D ranges;
-   candlestick and volume visualization;
-   cache-first historical reads;
-   Vike public historical-data path and provenance;
-   Twelve Data retained for private/internal research;
-   Azure Linux F1/Free constrained deployment qualification;
-   controlled public failure/disclosure behavior;
-   no trade execution.

Do not advertise Release 1.14 technical indicators or 2.0 ML features as
implemented.

Do not make licensing claims broader than the accepted Release 1.13
governance evidence supports.

## Root README protection

`README.md` remains **byte-for-byte protected**.

This lifecycle authority does not authorize the user's separately
reserved front-door README review.

Do not modify it before, during, or after release publication.

## Deployment boundary

Do not rebuild or redeploy the application merely because Release 1.13
is being tagged/published.

The accepted deployed WP07 artifact remains the runtime evidence for
Release 1.13.

No Azure resource, App Service setting, image, secret, tier, or
deployment mutation is authorized.

## Explicitly forbidden

This authority does not permit:

-   source or test changes;
-   provider/cache/UI changes;
-   dependency changes;
-   schema changes;
-   Azure/deployment/configuration changes;
-   secret changes or secret-value retrieval;
-   root README changes;
-   unrelated documentation cleanup;
-   Release 1.14 implementation;
-   Release 2.0 implementation;
-   rewriting Luna's substantive acceptance;
-   adding files other than the canonical acceptance artifact to the
    publication PR.

## Retry-until-governance-boundary rule

Do not stop merely because a formatting check, Git command, PR creation,
merge operation, issue transition, Project synchronization, milestone
transition, tag operation, or GitHub Release operation fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant step, and continue.

Stop only if the next corrective action would exceed this authority or
invalidate the Luna-accepted release boundary.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, corrective
action required, and do not perform that action without new authority.

## Required sequencing

The lifecycle order is mandatory:

1.  pre-mutation identity gate;
2.  final acceptance-status transition;
3.  one-file publication branch/commit/PR;
4.  publication validation;
5.  publication PR merge;
6.  post-merge canonical verification;
7.  issue #295 closure / Project Done verification;
8.  milestone #64 closure;
9.  Release 1.13 tag creation;
10. GitHub Release publication;
11. final reconciliation.

Do not skip ahead after a failed prerequisite.

## Final reconciliation

At completion verify:

-   canonical `main` SHA;
-   canonical `RELEASE_1.13_ACCEPTANCE.md`;
-   acceptance result records Luna PASS;
-   publication PR merged;
-   issue #294 Closed / Done;
-   issue #295 Closed / Done;
-   milestone #64 Closed;
-   Release 1.13 tag exists at exact final canonical SHA;
-   GitHub Release exists for that tag;
-   README unchanged;
-   no unauthorized paths changed;
-   no source/dependency/schema/deployment/configuration drift;
-   no Release 1.14 or 2.0 implementation occurred.

## Required final report

Return:

-   starting canonical SHA;
-   acceptance artifact final status transition;
-   publication branch/commit/PR identity;
-   exact changed paths;
-   publication validation results;
-   merge method and merge commit;
-   final canonical `main` SHA;
-   issue #294 state;
-   issue #295 and Project state;
-   milestone #64 state;
-   tag name and target SHA;
-   GitHub Release identity/state;
-   README protection result;
-   implementation/dependency/schema/deployment protection result;
-   any technical failures corrected;
-   any governance restrictions encountered;
-   whether Release 1.13 lifecycle is fully complete.

End with exactly one of:

`RELEASE 1.13 WP08 FINAL GOVERNANCE PUBLICATION AND RELEASE LIFECYCLE: PASS`

or

`RELEASE 1.13 WP08 FINAL GOVERNANCE PUBLICATION AND RELEASE LIFECYCLE: BLOCKED`

A PASS establishes Release 1.13 as canonically accepted and
lifecycle-complete. It does not authorize root README modification or
Release 1.14 implementation.

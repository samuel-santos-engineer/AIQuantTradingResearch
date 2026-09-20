# Release 1.13 --- PR #296 Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra is selected because this authority is a bounded
lifecycle/execution operation: verify an already-reviewed planning PR,
merge it if all gates remain satisfied, and perform deterministic
post-merge verification.

Model roles remain:

-   **GPT-5.6 Luna** --- release contract, architecture, governance
    definition, and final substantive acceptance.
-   **GPT-5.6 Terra** --- implementation, empirical validation, and
    explicitly authorized lifecycle operations.
-   **GPT-5.6 Sol** --- supporting analysis/reconciliation only; no
    independent governance or lifecycle authority.

This file does not transfer Luna's governance ownership to Terra.

## Repository and target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Target pull request:

`#296 — docs: define Release 1.13 market visualization roadmap`

Expected planning branch:

`docs/release-1.13-planning-governance`

Expected reviewed head commit:

`2f28d2dc833bf7345327a16b127441efa1c397ba`

Expected original base:

`2738d471d21d9e32d021b54dd7ea86a9f4723e33`

## Purpose

This authority permits Codex to:

1.  inspect the current repository and live PR #296 state;
2.  verify that the planning mutation remains within the approved
    Release 1.13 governance boundary;
3.  repair technical or validation defects only when the repair remains
    inside the already-authorized planning mutation boundary;
4.  merge PR #296 into `main` if every gate below passes;
5.  verify the resulting `main` state;
6.  report the new canonical post-merge baseline for the separately
    authorized Release 1.13 WP01 authority.

This authority does **not** authorize WP01 execution or any Release 1.13
product implementation.

## Required pre-merge inspection

Do not rely only on this prompt's expected SHAs or the previous report.

Before mutation or merge, inspect live GitHub/repository truth and
verify:

-   PR #296 still exists and targets `main`;
-   PR #296 is open and unmerged;
-   the head branch is the expected Release 1.13 planning branch;
-   the PR is mergeable under repository rules;
-   no unexpected commits or changed paths have appeared;
-   required checks, if any, are complete and acceptable;
-   the root `README.md` is unchanged by the PR;
-   no implementation source, package, schema, Azure, Docker/GHCR,
    provider-runtime, tag, or release mutation is present;
-   Release 1.13 milestone #64 exists with the governed identity;
-   Release 1.14 milestone #65 exists with the governed reservation
    identity;
-   Release 1.13 WP01--WP08 issues #288--#295 exist and remain
    unexecuted;
-   Project #2 contains Release options `1.13` and `1.14` without
    destructive changes to existing options;
-   Release 1.11 remains abandoned/nonexistent;
-   Initiative-1.11 remains historical feasibility work;
-   the forward roadmap remains coherent:
    `1.10 -> 1.12 -> 1.13 -> 1.14 -> 2.0 -> 2.1 -> 2.2 -> 2.3`.

If the live PR head differs from the expected head SHA, inspect the
additional commits and changed paths. Do not automatically reject a
harmless authorized correction, but do not merge an unexplained or
out-of-scope mutation.

## Expected PR mutation boundary

The planning PR is expected to contain only the bounded governance
mutation established by its authority, including:

-   `docs/project/ROADMAP.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/roadmap/release-1.13/prompters/release-1.13-historical-market-visualization-provider-abstraction-governance-authority-luna.md`

All Release 1.13 prompter/control Markdown names must preserve the
lowercase hyphenated prefix:

`release-1.13-`

Do not broaden this path set merely to make unrelated cleanup
convenient.

## Protected front door

The repository root:

`README.md`

remains explicitly protected.

Do not modify, reformat, regenerate, stage, or otherwise change the root
README under this authority.

The owner will review the front-door README separately after roadmap
milestones/governance are established.

Post-merge verification must explicitly prove that PR #296 did not
change `README.md`.

## Continue-until-governance-boundary rule

Codex must not stop merely because it encounters an ordinary technical
failure.

Within the authority granted by this document, continue iterating until
the lifecycle operation either succeeds or further progress requires
crossing a governance boundary.

For ordinary failures, diagnose the cause, make the smallest authorized
correction when a correction is permitted, rerun the relevant
validation, and continue. Examples include:

-   Markdown/whitespace defects;
-   a failed `git diff --check`;
-   an incorrect internal governance reference;
-   a harmless planning-file formatting problem;
-   a validation command failure caused by an authorized planning-file
    defect;
-   transient Git/GitHub lifecycle errors that can safely be retried;
-   branch synchronization problems that can be resolved without
    introducing unauthorized content.

Do **not** treat an initial test, validation, or merge failure as a
final blocker when it can be corrected within the literal authority.

Stop and report **BLOCKED** only when the next required action would
cross governance, for example:

-   modifying root `README.md`;
-   modifying a path outside the approved planning boundary;
-   changing application/runtime source;
-   adding or changing packages/dependencies;
-   changing a database/schema;
-   performing Azure, Docker/GHCR, or provider-runtime operations;
-   implementing WP01 or later work;
-   changing Release 1.14 beyond its governed reservation;
-   implementing Release 2.0;
-   exposing or requesting secrets;
-   incurring prohibited recurring cost;
-   overriding a failed required repository protection/check without
    authority;
-   rewriting history or force-pushing where not already authorized;
-   making a substantive architecture/product decision reserved to Luna;
-   requiring an owner decision or additional authority.

The final report must distinguish:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

from:

`GOVERNANCE RESTRICTION — STOPPED`

where applicable.

## Validation before merge

Re-run appropriate planning validation against the actual PR head.

At minimum verify:

-   exact changed-file set;
-   `git diff --check` or equivalent whitespace validation;
-   Release 1.13 prompter naming;
-   root `README.md` absent from the diff;
-   no secrets/credentials introduced;
-   no implementation/package/schema/deployment mutation;
-   roadmap sequence and release identities;
-   planning documents agree on WP01--WP08;
-   no Release 1.11 resurrection;
-   Release 2.0/2.1/2.2/2.3 identities remain preserved.

Use the repository's existing secret-scanning mechanism where available.

Do not invent a passing result. If a validation fails, apply the
continue-until-governance-boundary rule.

## Merge authority

If and only if all substantive and repository gates pass, this document
explicitly authorizes merging PR #296 into `main`.

Use the repository's normal merge method and protections.

Do not bypass branch protection, required checks, review requirements,
or repository policy.

Do not create a tag or GitHub Release.

Do not close Release 1.13 milestone #64.

Do not mark WP01--WP08 complete.

Do not start WP01.

If GitHub requires an authorized review/approval that Codex cannot
supply, stop at that governance/repository boundary and report it rather
than bypassing it.

## Post-merge verification

After a successful merge:

1.  fetch/update local repository state from `origin`;
2.  establish the new canonical `origin/main` SHA;
3.  verify the merge is actually reachable from `origin/main`;
4.  verify the planning files are present on `main`;
5.  verify `docs/project/ROADMAP.md` contains the governed forward
    sequence;
6.  verify root `README.md` was not changed by this lifecycle operation;
7.  verify Release 1.13 milestone #64 remains open unless existing
    governance explicitly requires otherwise;
8.  verify Release 1.14 milestone #65 remains a reservation;
9.  verify issues #288--#295 remain in the appropriate pre-execution
    state;
10. verify Project #2 Release identities `1.13` and `1.14` remain
    intact;
11. verify no tag, GitHub Release, deployment, provider request, or
    application mutation occurred.

The resulting `origin/main` SHA becomes the required predecessor
baseline for the next Release 1.13 WP01 authority.

## No WP01 authority

A successful merge means only that Release 1.13 planning governance is
canonical on `main`.

It does not authorize:

-   WP01 mutation;
-   architecture implementation;
-   chart-library selection implementation;
-   Vike integration;
-   Twelve Data changes;
-   cache implementation;
-   Streamlit UI implementation;
-   Azure mutation;
-   package additions;
-   schema changes;
-   technical indicators;
-   ML work;
-   trading functionality.

WP01 requires a new, separately issued Markdown authority using the
established `release-1.13-` prefix and the model assignment defined by
the canonical Release 1.13 execution plan.

## Required final report

Report:

-   pre-merge `main` SHA;
-   inspected PR number/title/state;
-   inspected PR head SHA;
-   exact changed paths;
-   validation/check results;
-   any technical failures encountered and corrected;
-   confirmation that `README.md` remained unchanged;
-   merge method;
-   merge commit SHA;
-   post-merge `origin/main` SHA;
-   milestone #64 state;
-   milestone #65 state;
-   issues #288--#295 state;
-   Project #2 Release `1.13`/`1.14` state;
-   confirmation that no
    implementation/deployment/provider/package/schema/tag/release
    mutation occurred;
-   any governance restriction encountered;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 PR #296 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 PR #296 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS does not authorize WP01 execution.

# Release 1.13 --- Terra Post-Release Dockerfile Fix Publication Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this narrowly bounded source-publication operation.

GPT-5.6 Luna remains the governance/architecture authority if a material
boundary must change. GPT-5.6 Sol may support analysis only and may not
silently assume Terra publication authority or Luna governance
authority.

## Context and frozen release boundary

Release 1.13 remains frozen at:

-   tag: `v1.13.0`
-   accepted release commit: `28aeb4961e505a4f8b89baf8832bb1064c07d033`

Do not move, recreate, retag, or reinterpret `v1.13.0`.

The current post-release canonical `main` before this publication
operation is expected to contain:

`91eb0605ff8be05ca61ba2198afa42100ab16d53`

That state includes the already-merged PR #309 presentation changes,
including the Streamlit page title and favicon assets.

A Dockerfile correction has subsequently been developed and validated
locally but is not yet committed. The prior dual-region Azure deployment
authority is **not valid for this new source state** because it was
explicitly bound to commit `91eb0605ff8be05ca61ba2198afa42100ab16d53`.

This authority publishes the Dockerfile correction only. It does not
authorize Azure deployment.

## Purpose

Publish the already-developed and locally validated Dockerfile
correction through the repository's normal branch → commit → pull
request → validation → merge lifecycle.

The intended outcome is a new canonical `main` merge commit that can
become the immutable source boundary of a **separate fresh dual-region
deployment authority**.

## Mandatory pre-mutation verification

Before creating a branch or commit, verify and record:

-   current local branch;
-   local `main` SHA;
-   `origin/main` SHA;
-   whether local `main` matches `origin/main`;
-   working-tree status;
-   exact uncommitted paths;
-   exact diff of the Dockerfile correction;
-   whether any unrelated staged or unstaged changes exist;
-   whether untracked governance/authority documents exist.

Verify that canonical history contains PR #309 and commit
`91eb0605ff8be05ca61ba2198afa42100ab16d53`.

If `origin/main` has advanced, inspect the advancement before
proceeding. Do not silently discard or overwrite later canonical work.
If the new canonical state materially changes the Dockerfile correction
or its assumptions, stop at the governance boundary.

## Literal mutation allowlist

The only repository file authorized for modification/publication by this
authority is:

`Dockerfile`

No other tracked path may be included in the publication commit or PR.

If the validated local correction actually resides in a differently
named Dockerfile path, do not guess and do not broaden the allowlist.
Stop and report the exact path so new authority can be issued.

## Existing local change rule

The purpose of this authority is to publish the **already-developed and
locally validated** Dockerfile correction.

Before publication:

1.  inspect the current Dockerfile diff;
2.  describe what the correction changes and why it is required;
3.  verify that it addresses the previously observed
    container-build/runtime problem;
4.  verify that it does not introduce unrelated behavior.

Do not opportunistically redesign, refactor, clean up, reformat, or
expand the Dockerfile beyond what is necessary for the validated
correction.

Minor corrections to the Dockerfile itself are permitted only when
required to make the authorized fix build and validate successfully and
remain within the same technical purpose.

## Protected paths and boundaries

Explicitly forbidden:

-   `README.md`;
-   `favicon.ico`;
-   `imgs/icon.png`;
-   Python source;
-   C# source;
-   tests;
-   project/package/dependency files;
-   schemas;
-   provider code/configuration;
-   market-data contracts;
-   cache architecture;
-   UI implementation;
-   Azure configuration;
-   workflow files;
-   roadmap/governance files;
-   Release 1.14 files;
-   any path other than literal `Dockerfile`.

PR #309 presentation behavior must be preserved, not reimplemented.

Existing untracked authority/governance documents must remain untouched
and must not be added to the commit or PR.

## Dependency and architecture boundary

This authority does not permit:

-   adding or changing application dependencies;
-   changing Python/.NET package versions;
-   changing schema versions;
-   changing provider contracts;
-   changing Vike/Twelve Data roles;
-   changing application architecture;
-   changing Azure resources;
-   changing deployment topology;
-   changing the free-tier/zero-cost model.

If the Dockerfile fix cannot succeed without one of those changes, stop
and report the governance restriction.

## Branch and commit

Create the smallest normal publication branch from the verified
canonical base.

Use a descriptive branch name for the Dockerfile correction.

Stage **only**:

`Dockerfile`

Before commit, prove the staged path set is exactly one path.

Create one focused commit for the Dockerfile correction and record:

-   branch name;
-   base SHA;
-   commit SHA;
-   commit message;
-   exact changed path.

Do not include authority documents.

## Required local validation

Re-run the validation that established the Dockerfile correction
locally.

At minimum:

-   validate Dockerfile syntax/buildability;
-   build the application container using the repository's established
    build context and normal build command;
-   prove the build uses the intended repository state;
-   confirm the build succeeds without requiring unauthorized
    source/dependency/schema/configuration changes;
-   inspect the resulting image sufficiently to establish that the
    specific Dockerfile problem is corrected;
-   verify PR #309 title/favicon assets required by the application are
    present in the resulting container where the Dockerfile fix is
    intended to make them available;
-   run any focused existing tests/checks directly relevant to the
    Dockerfile/container packaging change, if available;
-   run repository whitespace/diff checks appropriate to the changed
    text file.

Do not modify tests merely to make validation pass.

Do not deploy this image to Azure under this authority.

## Pull request

Push the focused branch and create a normal pull request against `main`.

The PR must clearly state:

-   this is a post-Release-1.13 Dockerfile packaging/deployment
    correction;
-   Release 1.13 remains frozen;
-   exact problem corrected;
-   validation performed;
-   changed path is only `Dockerfile`;
-   Azure deployment is explicitly deferred to a fresh authority after
    merge;
-   the prior deployment authority bound to `91eb0605...` is not reused.

Record:

-   PR number;
-   PR URL;
-   base branch/SHA;
-   head branch/SHA;
-   changed-file count and exact path.

## Pre-merge gate

Before merge, verify:

-   PR is open and non-draft unless repository convention requires
    otherwise;
-   PR targets `main`;
-   exact changed path is only `Dockerfile`;
-   no README change;
-   no
    source/test/dependency/schema/provider/UI/configuration/governance
    drift;
-   no authority files added;
-   no secrets introduced;
-   Dockerfile diff matches the authorized correction;
-   container build validation passes;
-   relevant checks pass;
-   PR is clean/mergeable under normal repository rules;
-   canonical base has not materially changed in a way that invalidates
    the correction.

If additional files are required, stop. Do not broaden the PR.

## Merge authority

If and only if the pre-merge gate passes, merge the Dockerfile-fix PR
using the repository's normal merge method.

Do not force-push canonical `main`.

Do not bypass required repository checks.

Record:

-   merge method;
-   PR final state;
-   merge commit SHA;
-   final `origin/main` SHA;
-   whether the Dockerfile-fix head is reachable from canonical main.

## Post-merge reconciliation

After merge:

-   update/fetch local canonical state using the repository's normal
    safe workflow;
-   verify final `origin/main`;
-   verify the merge contains exactly the authorized Dockerfile
    publication relative to its canonical base;
-   verify `README.md` is unchanged by this operation;
-   verify PR #309 title/favicon changes remain present;
-   verify no
    dependency/schema/source/test/provider/Azure/configuration/governance
    drift;
-   verify untracked authority documents remain untracked and untouched;
-   rerun the relevant container build from the **final canonical merge
    commit**;
-   record the final canonical merge SHA.

The final canonical merge SHA is the only source commit that may be
proposed for the next fresh dual-region deployment authority.

## No Azure deployment authority

This publication authority explicitly forbids:

-   changing either Azure App Service;
-   pushing a deployment image for production use;
-   changing an App Service container reference;
-   restarting an App Service as part of deployment;
-   changing Azure settings;
-   creating/deleting Azure resources;
-   changing App Service plans/SKUs;
-   performing the West Central US / West US 2 deployment.

A local container build used for validation is allowed.

Publication PASS does **not** revive or extend the previous dual-region
deployment authority.

## No release-lifecycle mutation

Do not:

-   move/change `v1.13.0`;
-   create another Release 1.13 version tag;
-   modify the published Release 1.13 GitHub Release;
-   reopen the Release 1.13 milestone;
-   reopen Release 1.13 issues;
-   start Release 1.14 implementation.

This is a post-release corrective publication only.

## Retry-until-governance-boundary rule

Do not stop merely because a Docker build, lint, validation, Git
operation, PR check, mergeability check, or other ordinary technical
step fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant checks, and continue iterating until the authorized
acceptance conditions pass.

Ordinary technical failures are:

`TECHNICAL FAILURE — CONTINUE FIXING`

Stop only when the next corrective action would require exceeding this
authority or crossing an explicit governance restriction, including:

-   modifying any path other than `Dockerfile`;
-   changing dependencies/schema/architecture/provider contracts;
-   changing source or tests;
-   modifying README or PR #309 assets;
-   changing Azure infrastructure/configuration;
-   deploying to Azure;
-   exposing or changing secrets;
-   modifying the frozen Release 1.13 lifecycle;
-   beginning Release 1.14 work;
-   publishing a materially different fix from the validated Dockerfile
    correction.

A governance stop is:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, corrective
action required, and do not perform that action without new authority.

## Required completion report

Return:

-   starting local/main and `origin/main` SHA;
-   initial working-tree status;
-   exact initial Dockerfile diff purpose;
-   confirmation that unrelated local/untracked files were protected;
-   branch name;
-   publication commit SHA;
-   exact commit path set;
-   local container validation performed and results;
-   confirmation PR #309 title/favicon assets are packaged/preserved as
    intended;
-   PR number and URL;
-   PR base/head SHAs;
-   PR changed-file set;
-   pre-merge checks;
-   merge method;
-   merge commit SHA;
-   final canonical `origin/main` SHA;
-   post-merge Dockerfile diff reconciliation;
-   final canonical container-build result;
-   README protection result;
-   dependency/schema/source/test/provider/Azure/configuration
    protection result;
-   frozen `v1.13.0` protection result;
-   confirmation that no Azure deployment occurred;
-   technical failures encountered and corrections made;
-   governance restrictions encountered, if any.

End with exactly one of:

`POST-1.13 DOCKERFILE FIX PUBLICATION: PASS`

or

`POST-1.13 DOCKERFILE FIX PUBLICATION: BLOCKED`

A PASS authorizes **nothing further by itself**. After PASS, a new
dual-region Azure deployment authority must be created and bound to the
resulting final canonical merge SHA.

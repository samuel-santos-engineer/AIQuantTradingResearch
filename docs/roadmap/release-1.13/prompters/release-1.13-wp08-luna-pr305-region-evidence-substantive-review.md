# Release 1.13 WP08 --- Luna PR #305 Region Evidence Substantive Review Authority

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns this substantive governance-evidence review. GPT-5.6
Terra must not merge PR #305 until Luna has established that the
correction is factually precise and does not erase a separately valid
Azure-region fact. GPT-5.6 Sol may support evidence comparison only.

## Starting boundary

Canonical `main`:

`128bbb4e9462990f6edd1a0e068828db95be1da8`

Correction branch:

`fix/wp08-wp07-region-evidence`

Correction commit:

`2da37d6eb162718ac9682c9ee722954a460ebbd1`

Correction PR:

`#305`

Expected PR state:

-   Open
-   unmerged
-   exactly one changed file

Expected changed path:

`docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`

WP08 remains substantively blocked pending canonical reconciliation of
the Azure-region evidence.

## Critical clarification

**Both `West US 2` and `West Central US` may be valid Azure-region facts
in the wider project evidence.**

Therefore, this review must not assume that one region globally replaces
the other.

The review must establish what resource, deployment, plan, historical
observation, or evidence context each region refers to.

For the WP07 accepted deployed App Service, the evidence previously
reconciled identifies:

`West Central US`

A `West US 2` reference is not automatically erroneous if it truthfully
refers to another Azure resource, an earlier deployment context, a
distinct governed component, or another accurately recorded historical
fact.

## Purpose

Perform an adversarial substantive review of PR #305 before merge.

Determine whether its one-file correction:

1.  accurately records the region of the WP07 accepted deployed App
    Service;
2.  preserves any separately valid `West US 2` evidence;
3.  avoids converting a resource-specific correction into an incorrect
    project-wide assertion;
4.  changes no unrelated WP07 evidence;
5.  is sufficient to remove the WP08 governance-evidence inconsistency.

This authority is primarily observation/reconciliation authority.

## Mandatory PR identity gate

Before substantive review, verify independently:

-   PR #305 exists and is Open;
-   PR #305 is not merged;
-   its head is exactly `2da37d6eb162718ac9682c9ee722954a460ebbd1`;
-   its base is the expected canonical `main` lineage beginning from
    `128bbb4e9462990f6edd1a0e068828db95be1da8`;
-   its branch is `fix/wp08-wp07-region-evidence`;
-   it changes exactly one file;
-   that file is exactly:
    `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`;
-   root `README.md` is untouched;
-   no source, test, dependency, schema, deployment, or configuration
    path is changed.

If PR identity differs, stop and report the mismatch.

## Region evidence reconciliation

Independently inspect the repository evidence and authoritative Azure
metadata available to the review.

For every relevant occurrence of:

-   `West US 2`
-   `West Central US`

determine:

-   exact file/evidence source;
-   resource or context being described;
-   whether it is current, historical, or resource-specific;
-   whether it is factually supported;
-   whether PR #305 affects it.

Do not retrieve or display secret values.

Do not infer that Azure resources must share a region merely because
they belong to the same project, resource group, release, or application
architecture.

## PR #305 semantic review

Inspect the exact one-file diff.

The review must answer:

-   What text did PR #305 remove?
-   What text did it add?
-   Which specific Azure resource/context does the corrected statement
    describe?
-   Does `West Central US` correctly describe that resource/context?
-   Did the removed `West US 2` statement incorrectly describe that same
    resource/context?
-   Is there separately valid `West US 2` evidence elsewhere that
    remains intact?
-   Does the resulting WP07 validation document accidentally imply that
    every Azure resource in the project is in West Central US?
-   Does the correction alter any substantive WP07 acceptance conclusion
    beyond fixing the evidence attribution?
-   Does the correction remain consistent with the accepted WP07
    deployment and runtime evidence?

## No global replacement rule

Do not approve a broad semantic rule such as:

`West US 2 is wrong; West Central US is right.`

The only acceptable conclusion is resource/evidence-specific.

A PASS requires a conclusion equivalent to:

-   the corrected WP07 statement specifically concerns the accepted
    deployed App Service or other precisely identified resource;
-   `West Central US` is the supported region for that context;
-   any independently valid `West US 2` facts remain valid and are not
    contradicted or erased.

If the one-file correction cannot express that distinction accurately,
return BLOCKED and identify the minimum documentary correction required.

## Mutation boundary

This Luna authority does **not** authorize merge.

It also does not authorize ordinary implementation or lifecycle
mutation.

If PR #305 is already correct, make no repository changes.

If a tiny documentary correction inside the same already-authorized file
is necessary to distinguish the two valid regional contexts, Luna may
identify the exact required wording but must not silently expand scope
or merge it. Prefer returning BLOCKED with the minimum correction
required unless an existing explicit authority clearly permits amendment
of the open correction PR.

No additional path may be added.

## Validation

Confirm:

-   exact changed-path count remains 1;
-   correction is documentary only;
-   no trailing whitespace;
-   root README unchanged;
-   dependencies unchanged;
-   schema unchanged;
-   implementation/tests unchanged;
-   Azure deployment/configuration unchanged;
-   secrets unchanged and undisclosed;
-   issue #295 remains Open/Backlog;
-   milestone #64 remains Open;
-   no Release 1.13 tag exists;
-   no Release 1.13 GitHub Release exists.

No application rebuild, redeployment, or runtime retest is required
merely to review this evidence correction.

## Retry-until-governance-boundary rule

Do not stop merely because a repository query, Azure metadata query,
search, diff inspection, or validation step fails.

Diagnose the failure, retry or correct the review method within this
authority, and continue.

Stop only if the next action would cross an explicit governance
boundary.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, missing evidence, and
minimum next authority required.

## PASS standard

PASS requires Luna to independently establish all of the following:

-   PR #305 identity is exact;
-   its one-file diff is the intended documentary correction;
-   `West Central US` is valid for the specific WP07 resource/context
    being corrected;
-   the correction does not establish or imply that `West US 2` is
    globally invalid;
-   any separately valid `West US 2` evidence remains preserved;
-   no unrelated WP07 evidence was altered;
-   no implementation/deployment/lifecycle mutation occurred;
-   the correction is substantively safe to merge.

## Required report

Return:

-   canonical starting SHA;
-   PR #305 state/base/head/branch;
-   exact changed path;
-   exact semantic before/after correction;
-   resource/context associated with `West Central US`;
-   every materially relevant `West US 2` occurrence found and its
    classification;
-   whether both regions remain valid and why;
-   whether PR #305 preserves that distinction;
-   README/dependency/schema/implementation protection result;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   exact next authority.

End with exactly one of:

`RELEASE 1.13 WP08 PR305 REGION EVIDENCE SUBSTANTIVE REVIEW: PASS`

or

`RELEASE 1.13 WP08 PR305 REGION EVIDENCE SUBSTANTIVE REVIEW: BLOCKED`

A PASS authorizes creation/execution of a separate GPT-5.6 Terra PR #305
merge and post-merge reconciliation authority only. It does not
authorize the merge itself, completion of WP08, issue #295 closure,
milestone closure, tagging, GitHub Release publication, root README
modification, or Release 1.14 work.

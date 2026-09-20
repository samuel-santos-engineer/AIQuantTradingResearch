# Release 1.13 WP08 --- Luna WP07 Azure Region Evidence Correction Authority

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns this documentary governance correction because the
defect is in an accepted release evidence record and blocks final
Release 1.13 reconciliation. GPT-5.6 Terra is not authorized to change
implementation or deployment. GPT-5.6 Sol may support evidence
comparison only.

## Starting boundary

Canonical `main`:

`128bbb4e9462990f6edd1a0e068828db95be1da8`

WP08 substantive reconciliation currently ends:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: BLOCKED`

The sole identified blocker is an evidence inconsistency:

-   committed `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`
    records the deployed App Service region as **West US 2**;
-   accepted/verified deployment evidence establishes the App Service
    region as **West Central US**.

WP01--WP07 implementation otherwise reconciled successfully. This
authority exists only to correct and prove that documentary
inconsistency.

## Purpose

Correct the stale Azure-region statement in the committed WP07
validation evidence so the document accurately reflects the accepted
deployed resource.

This is an **evidence-record correction**, not a deployment,
architecture, implementation, or provider change.

Do not use this authority to improve wording unrelated to the region
discrepancy or to reopen settled WP07 conclusions.

## Mandatory pre-mutation verification

Before changing any file:

1.  verify canonical `main` is still exactly
    `128bbb4e9462990f6edd1a0e068828db95be1da8`, or stop and reconcile
    any advancement before mutation;
2.  inspect the committed `WP07_AZURE_F1_VALIDATION.md`;
3.  locate every statement in that file that identifies the Azure
    deployment region/location;
4.  independently verify the authoritative current/accepted resource
    metadata supports **West Central US**;
5.  determine whether any other committed Release 1.13 governance
    artifact contains the same stale **West US 2** claim.

Do not retrieve or display secret values while verifying Azure metadata.

If evidence does not establish West Central US, stop without mutation.

## Literal mutation allowlist

The correction authority is initially limited to:

`docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`

If mandatory search proves another committed Release 1.13 governance
artifact contains the same stale region claim and must be corrected for
consistency, **do not expand the allowlist silently**. Report the exact
additional path and stop for authority expansion.

The untracked/current WP08 acceptance artifact is not to be rewritten
under this correction authority unless a separate authority explicitly
permits it.

## Required correction

Change only the stale deployment-region evidence necessary to make the
WP07 validation record truthful.

Expected semantic correction:

`West US 2` → `West Central US`

Preserve the surrounding accepted WP07 evidence unless a directly
dependent sentence must be minimally adjusted for grammatical or factual
consistency.

Do not alter historical statements where `West US 2` accurately
describes a different resource or a historical observation. Correct only
statements that purport to identify the accepted deployed App
Service/resource addressed by WP07.

## Evidence annotation

If the existing document structure supports it, make clear that **West
Central US** is the verified deployment location associated with the
accepted WP07 runtime evidence.

Do not invent a verification date, command output, Azure resource
identifier, or quotation that was not actually observed.

## Validation

After correction:

-   show the exact diff;
-   prove only authorized path(s) changed;
-   search the Release 1.13 roadmap artifacts again for stale
    `West US 2` references and classify any remaining occurrence;
-   confirm `West Central US` is represented correctly;
-   confirm no trailing whitespace;
-   confirm root `README.md` remains unchanged;
-   confirm no source, test, dependency, schema, configuration, Azure,
    deployment, secret, issue, Project, milestone, tag, or GitHub
    Release mutation occurred.

No application build/test rerun is required solely for this documentary
region correction unless repository-native documentation validation
requires it.

## Commit and PR boundary

Create the smallest repository change needed to carry this documentary
correction through normal review.

A correction commit/PR may contain **only the frozen authorized
documentary path**.

Record:

-   branch name;
-   commit SHA;
-   PR number and URL/state if a PR is created;
-   exact changed paths;
-   base SHA.

Do **not** merge the correction PR under this authority unless an
existing explicit repository governance rule already grants Luna
documentary merge authority. If no such explicit authority exists, leave
it open and report that a separate lifecycle/merge authority is
required.

Do not include the untracked `RELEASE_1.13_ACCEPTANCE.md` in the
correction commit/PR.

## Protected boundaries

This authority does not permit:

-   source or test changes;
-   deployment or image changes;
-   Azure resource/configuration changes;
-   secret changes or secret-value retrieval;
-   dependency changes;
-   schema changes;
-   provider/cache/UI changes;
-   root `README.md` changes;
-   changes to Release 1.14 or 2.0;
-   closing issue #295;
-   moving #295 to Done;
-   closing milestone #64;
-   creating a Release 1.13 tag;
-   creating a GitHub Release;
-   declaring WP08 accepted before the correction is canonically merged
    and reconciliation resumes.

## Retry-until-governance-boundary rule

Do not stop merely because a repository query, Azure metadata query,
search, formatting check, commit, PR creation, or documentation
validation step fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant checks, and continue.

Stop only when the next corrective action would exceed this literal
documentary authority or another explicit governance boundary.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, and minimum
additional authority required.

## Completion standard

PASS requires:

-   authoritative evidence supports **West Central US**;
-   stale accepted-resource `West US 2` statement is corrected;
-   no unrelated evidence is changed;
-   exact diff is documentary only;
-   README/dependencies/schema/implementation/deployment remain
    unchanged;
-   no lifecycle state was advanced;
-   any correction PR is left in the state allowed by this authority;
-   the correction is ready for the smallest next merge/reconciliation
    step.

## Required report

Return:

-   starting canonical SHA;
-   authoritative region evidence used;
-   every `West US 2` occurrence found in Release 1.13 governance and
    its classification;
-   exact changed-path allowlist;
-   exact semantic correction;
-   validation results;
-   branch/commit/PR identity if created;
-   whether correction is merged or awaiting separate merge authority;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   exact next authority required.

End with exactly one of:

`RELEASE 1.13 WP08 WP07 AZURE REGION EVIDENCE CORRECTION: PASS`

or

`RELEASE 1.13 WP08 WP07 AZURE REGION EVIDENCE CORRECTION: BLOCKED`

A PASS does not itself complete WP08 or Release 1.13.

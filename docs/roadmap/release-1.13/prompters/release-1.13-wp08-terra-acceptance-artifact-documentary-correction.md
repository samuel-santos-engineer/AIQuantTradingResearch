# Release 1.13 WP08 --- Terra Acceptance Artifact Correction Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the narrow documentary correction of the untracked
Release 1.13 acceptance artifact. GPT-5.6 Luna retains final substantive
acceptance authority and must perform a fresh reconciliation after this
correction. GPT-5.6 Sol may support analysis only.

## Starting canonical boundary

Canonical `origin/main`:

`fb946d77d0e8acb34d1bd91865bb62b2aad24160`

There are no commits after the PR #305 merge at the start of this
authority.

Relevant lifecycle state:

-   PR #304: Merged
-   PR #305: Merged
-   issue #294: Closed / Done
-   issue #295: Open / Backlog
-   milestone #64: Open
-   Release 1.13 tag: absent
-   Release 1.13 GitHub Release: absent
-   root `README.md`: unchanged

## Prior Luna result

Latest Luna marker:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: BLOCKED`

The sole identified blocker is the stale **untracked** acceptance
artifact:

`docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`

Luna identified these stale statements:

-   canonical `main` records the pre-PR #305 SHA;
-   WP07 still records the obsolete App Service `West US 2` discrepancy;
-   `WP08SubstantiveAcceptanceResult=BLOCKED` reflects the earlier
    pre-correction reconciliation.

The canonical repository implementation and governance evidence
otherwise reconciled successfully.

## Purpose

Correct only the stale Release 1.13 acceptance artifact so it accurately
describes the current canonical evidence boundary and can be subjected
to a **fresh GPT-5.6 Luna final reconciliation**.

This authority does **not** grant Terra substantive acceptance
authority.

Terra must not convert Luna's previous BLOCKED result into PASS by
assertion. Terra may update factual evidence and describe the acceptance
status as **pending fresh Luna reconciliation**.

## Literal mutation allowlist

Exactly one file may be modified:

`docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`

No other tracked or untracked file may be changed.

Before mutation, verify this file is still untracked and inspect its
complete current contents.

If another file must change, stop at the governance boundary.

## Required factual corrections

### Canonical SHA

Replace the obsolete pre-PR #305 canonical SHA with:

`fb946d77d0e8acb34d1bd91865bb62b2aad24160`

Ensure the document clearly distinguishes earlier historical SHAs from
the current canonical boundary. Do not replace a historical SHA where
that SHA remains accurate for the event being described.

### WP07 Azure-region reconciliation

Remove the obsolete assertion that WP07 remains inconsistent because its
accepted App Service is recorded as `West US 2`.

Record the reconciled resource-specific evidence:

-   App Service: `aiqr112wp035ec325382770`
-   accepted/current WP07 App Service region: `West Central US`
-   plan: `asp-aiq-r112-wp03-wcus-5ec325382770`
-   PR #305 corrected the WP07 evidence record and is merged in
    canonical `main`.

State explicitly that this correction is **resource-specific**.

Do not assert that `West US 2` is globally obsolete or invalid.

### Preserve valid West US 2 contexts

The acceptance artifact must state that separately valid `West US 2`
evidence remains preserved for contexts including:

-   Release 1.12 accepted-target documentation;
-   constrained regional recovery order;
-   operations-runbook context;
-   legitimate historical or alternate deployment contexts where
    supported by the repository evidence.

Do not conflate those contexts with the WP07 accepted App Service.

### WP08 acceptance status

The artifact must not claim final WP08 PASS before Luna performs the
fresh reconciliation.

Replace stale wording such as:

`WP08SubstantiveAcceptanceResult=BLOCKED`

when it incorrectly represents the **current pending state** after
correction.

Use wording equivalent to:

`WP08SubstantiveAcceptanceResult=PENDING_LUNA_RECONCILIATION`

if the document uses machine-readable key/value status notation.

In prose, state that:

-   the prior Luna BLOCKED result was valid for the stale artifact;
-   its identified documentary blocker has now been corrected in the
    artifact;
-   final WP08 substantive acceptance remains pending a fresh Luna
    review.

Do not write the final Luna PASS marker.

## Acceptance document model declaration

The artifact must explicitly retain or add:

**Acceptance/governance model: GPT-5.6 Luna**

If the artifact identifies the model used for this documentary
correction, state:

**Documentary correction model: GPT-5.6 Terra**

Do not imply Terra owns final release acceptance.

## Evidence reconciliation

Ensure the corrected artifact accurately reflects:

-   canonical `main` at `fb946d77d0e8acb34d1bd91865bb62b2aad24160`;
-   WP01--WP07 completion evidence;
-   PR #304 merged;
-   PR #305 merged;
-   issue #294 Closed / Done;
-   issue #295 Open / Backlog;
-   milestone #64 Open;
-   no Release 1.13 tag;
-   no Release 1.13 GitHub Release;
-   root README unchanged;
-   post-WP07 implementation changes are documentary only;
-   accepted deployed WP07 image/digest remains the accepted runtime
    artifact;
-   Release 1.13 exclusions/deferred scope remain intact.

Do not invent evidence not previously obtained.

## Historical evidence preservation

Do not rewrite the artifact as though the prior BLOCKED reconciliations
never happened.

Where relevant, preserve the chronology:

1.  initial WP08 reconciliation identified the WP07 region evidence
    discrepancy;
2.  correction PR #305 was substantively reviewed;
3.  PR #305 was merged;
4.  canonical region evidence now distinguishes the WP07 West Central US
    App Service from separately valid West US 2 contexts;
5.  resumed Luna reconciliation then identified the acceptance artifact
    itself as stale;
6.  this Terra authority corrects that artifact;
7.  final acceptance remains pending fresh Luna reconciliation.

## Validation

After editing:

-   inspect the complete resulting artifact;
-   show/reconcile the exact diff against its pre-correction untracked
    contents;
-   confirm only the literal allowed file changed;
-   confirm no trailing whitespace;
-   confirm no obsolete assertion remains that the WP07 App Service is
    in West US 2;
-   confirm `West Central US` is resource-specific;
-   confirm valid West US 2 contexts are preserved;
-   confirm current canonical SHA is correct;
-   confirm final acceptance status is pending Luna rather than PASS;
-   confirm root README remains unchanged;
-   confirm dependencies/schema/source/tests are unchanged;
-   confirm no Azure/deployment/configuration/secret mutation;
-   confirm issue #295 remains Open/Backlog;
-   confirm milestone #64 remains Open;
-   confirm no Release 1.13 tag or GitHub Release exists.

No application build, test rerun, container build, deployment, or Azure
mutation is required for this documentary-only correction.

## Git/commit boundary

Do **not** commit, push, create a PR, merge, or otherwise publish this
acceptance artifact under this authority.

Keep it untracked after correction.

The corrected untracked artifact must first undergo fresh GPT-5.6 Luna
substantive reconciliation.

A later lifecycle authority will decide how it is committed and merged
after Luna PASS.

## Explicitly forbidden

This authority does not permit:

-   changes outside `RELEASE_1.13_ACCEPTANCE.md`;
-   source/test changes;
-   dependency/schema changes;
-   provider/cache/UI changes;
-   Azure or deployment changes;
-   secret changes or secret-value retrieval;
-   root README changes;
-   issue #295 mutation;
-   milestone #64 mutation;
-   tag creation;
-   GitHub Release creation;
-   Release 1.14 work;
-   declaring WP08 PASS;
-   declaring Release 1.13 complete.

## Retry-until-governance-boundary rule

Do not stop merely because a file inspection, editing, formatting,
whitespace, repository-status, or evidence-reconciliation step fails.

Diagnose the failure, make corrections permitted within the one-file
allowlist, rerun the relevant checks, and continue.

Stop only if the next corrective action would exceed this authority.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, and minimum
additional authority required.

## PASS standard

PASS requires:

-   only the acceptance artifact was modified;
-   it remains untracked/unpublished;
-   canonical SHA is current;
-   region evidence is resource-specific and accurate;
-   valid West US 2 contexts remain preserved;
-   prior reconciliation chronology remains truthful;
-   final WP08 status is pending fresh Luna reconciliation, not PASS;
-   all protected repository/lifecycle boundaries remain unchanged;
-   the artifact is ready for fresh Luna review.

## Required completion report

Return:

-   starting/current canonical SHA;
-   artifact path and tracked/untracked state;
-   exact stale fields/sections corrected;
-   final canonical SHA recorded in the artifact;
-   West Central US resource-specific wording;
-   West US 2 preservation wording;
-   WP08 acceptance-status wording;
-   chronology preservation result;
-   whitespace/resulting-diff validation;
-   protected-path/state results;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   exact next authority.

End with exactly one of:

`RELEASE 1.13 WP08 ACCEPTANCE ARTIFACT DOCUMENTARY CORRECTION: PASS`

or

`RELEASE 1.13 WP08 ACCEPTANCE ARTIFACT DOCUMENTARY CORRECTION: BLOCKED`

A PASS authorizes only a fresh GPT-5.6 Luna read-only final Release 1.13
reconciliation against the corrected untracked acceptance artifact.

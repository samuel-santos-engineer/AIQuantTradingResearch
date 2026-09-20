# Release 1.13 WP08 --- Terra PR #305 Merge and Post-Merge Reconciliation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the mechanical merge and post-merge reconciliation
authorized here. GPT-5.6 Luna has already completed the substantive
review of the correction and retains final WP08 acceptance authority.
GPT-5.6 Sol may support reconciliation only.

## Accepted correction

Canonical predecessor:

`128bbb4e9462990f6edd1a0e068828db95be1da8`

PR:

`#305`

Branch:

`fix/wp08-wp07-region-evidence`

Luna-accepted head:

`2da37d6eb162718ac9682c9ee722954a460ebbd1`

Accepted changed path:

`docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`

Accepted semantic correction:

-   before: the existing WP07 App Service was described as `West US 2`;
-   after: that specific App Service is described as `West Central US`.

Verified resource:

`aiqr112wp035ec325382770`

Verified plan:

`asp-aiq-r112-wp03-wcus-5ec325382770`

Luna substantive review marker:

`RELEASE 1.13 WP08 PR305 REGION EVIDENCE SUBSTANTIVE REVIEW: PASS`

The review established that the correction is resource-specific and does
**not** invalidate separately valid `West US 2` evidence for Release
1.12 accepted/recovery documentation, constrained regional recovery
order, operations-runbook context, or historical/alternate deployment
contexts.

## Purpose

Merge exactly the Luna-reviewed PR #305 correction into canonical
`main`, prove that the accepted one-file documentary correction survived
merge without drift, and establish the new canonical boundary from which
Luna may resume WP08 final release reconciliation.

This authority does not complete WP08.

## Mandatory pre-merge identity gate

Before any mutation, independently verify:

-   PR #305 is Open, non-draft, and unmerged;
-   base is `main`;
-   head branch is `fix/wp08-wp07-region-evidence`;
-   head SHA is exactly `2da37d6eb162718ac9682c9ee722954a460ebbd1`;
-   canonical base remains consistent with
    `128bbb4e9462990f6edd1a0e068828db95be1da8`;
-   changed-path count is exactly 1;
-   changed path is exactly:
    `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`;
-   the semantic diff remains exactly the Luna-reviewed
    resource-specific region correction;
-   no new commit has appeared after Luna review;
-   root `README.md` remains unchanged;
-   dependencies, schema, source, tests, Azure configuration, and
    deployment are unchanged;
-   no merge conflict or intervening `main` change alters the reviewed
    meaning.

If any identity or semantic condition differs, **do not merge**. Stop
and require renewed Luna review.

## Merge authority

If and only if the identity gate passes, merge PR #305 through the
repository's normal merge workflow.

Do not amend the correction during merge.

Do not force-push, rewrite history, bypass required protections, or
combine unrelated changes.

Record:

-   merge method;
-   PR #305 final state;
-   merge commit;
-   new full canonical `main` SHA;
-   reachability of accepted head `2da37d6...` from canonical `main`.

## Post-merge reconciliation

After merge:

1.  update/fetch canonical `main`;
2.  prove `origin/main` reflects the merge;
3.  record the new full canonical SHA;
4.  inspect the merged version of `WP07_AZURE_F1_VALIDATION.md`;
5.  prove the accepted App Service context now states `West Central US`;
6.  confirm separately valid `West US 2` evidence was not globally
    erased or reclassified;
7.  confirm only the accepted one-file correction entered through PR
    #305;
8.  run whitespace/documentary checks appropriate to the repository;
9.  confirm root README remains unchanged;
10. confirm dependency/schema/source/test/deployment/configuration state
    remains untouched.

No application rebuild, container rebuild, redeployment, or Azure
mutation is required for this documentary-only merge.

## Untracked WP08 acceptance artifact

Preserve any untracked `RELEASE_1.13_ACCEPTANCE.md` or authority
documents exactly as they are.

Do not include them in PR #305, its merge, or any post-merge commit
under this authority.

The existing WP08 acceptance artifact will be reconciled separately when
Luna resumes final acceptance against the new canonical boundary.

## Lifecycle state

After the correction merge:

-   leave issue #295 **Open / Backlog / not started-to-completion**;
-   leave milestone #64 **Open**;
-   do not create a Release 1.13 tag;
-   do not create a GitHub Release;
-   do not close or reopen completed WP01--WP07 issues;
-   do not declare WP08 or Release 1.13 complete.

PR #305 itself may transition to Merged as the direct action authorized
here.

## Explicitly forbidden

This authority does not permit:

-   implementation changes;
-   test changes;
-   provider/cache/UI changes;
-   Azure resource/configuration changes;
-   secret changes or secret-value retrieval;
-   dependency changes;
-   schema changes;
-   root `README.md` changes;
-   Release 1.14 or 2.0 work;
-   modification/commit of the WP08 acceptance artifact;
-   issue #295 closure or Done transition;
-   milestone #64 closure;
-   Release 1.13 tag creation;
-   GitHub Release publication;
-   any unrelated documentation cleanup.

## Retry-until-governance-boundary rule

Do not stop merely because a merge command, repository synchronization,
diff inspection, whitespace check, or GitHub lifecycle operation
directly required for PR #305 fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant operation, and continue.

Stop only if the next corrective action would exceed this authority or
invalidate the Luna-reviewed correction.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, corrective
action required, and do not perform that action without new authority.

## PASS standard

PASS requires:

-   pre-merge identity gate passed;
-   exactly Luna-reviewed PR #305 merged;
-   accepted head is reachable from canonical `main`;
-   new canonical `main` SHA recorded;
-   merged correction remains resource-specific;
-   WP07 App Service evidence correctly states `West Central US`;
-   separately valid `West US 2` evidence remains preserved;
-   no unrelated path or semantic drift entered the merge;
-   README/dependencies/schema/source/tests/deployment/configuration
    remain protected;
-   issue #295 remains Open/Backlog;
-   milestone #64 remains Open;
-   no Release 1.13 tag or GitHub Release exists;
-   no WP08 acceptance/lifecycle completion was performed.

## Required final report

Return:

-   pre-merge PR #305 state/base/head;
-   changed-path and semantic identity-gate result;
-   merge method;
-   PR #305 post-merge state;
-   merge commit and full canonical `main` SHA;
-   accepted-head reachability result;
-   merged region evidence result;
-   preservation of separately valid `West US 2` contexts;
-   README/dependency/schema/source/test/deployment protection results;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   untracked acceptance/authority artifact preservation;
-   any technical failures corrected;
-   any governance restriction encountered;
-   exact next authority.

End with exactly one of:

`RELEASE 1.13 WP08 PR305 MERGE AND POST-MERGE RECONCILIATION: PASS`

or

`RELEASE 1.13 WP08 PR305 MERGE AND POST-MERGE RECONCILIATION: BLOCKED`

A PASS establishes only the corrected canonical evidence boundary. It
authorizes resumption of GPT-5.6 Luna WP08 final release reconciliation;
it does not itself complete WP08 or Release 1.13.

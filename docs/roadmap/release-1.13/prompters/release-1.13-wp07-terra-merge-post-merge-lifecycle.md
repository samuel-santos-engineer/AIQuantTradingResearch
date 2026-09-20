# Release 1.13 WP07 --- Terra Merge and Post-Merge Lifecycle Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the mechanical merge and post-merge WP07 lifecycle
reconciliation authorized here. GPT-5.6 Luna's substantive acceptance is
final for the accepted candidate and must not be reinterpreted. GPT-5.6
Sol may support reconciliation only.

## Accepted candidate

Canonical predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

PR:

`#304`

Accepted head:

`6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`

Accepted deployed image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`

Accepted deployed digest:

`sha256:18f60a172d51ff3d4c2b1bdf1b8089ba989cb5ef149adc3e5cf9488c9482f829`

Luna final substantive acceptance marker:

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST FINAL SUBSTANTIVE ACCEPTANCE: PASS`

The independent cloud-browser continuation confirmed BTC/USD and ETH/USD
candlesticks and volume, alternate controls, restored defaults, Vike/UTC
provenance, clean System Health, informational ML surface, Twelve Data
boundary, no-trade messaging, and no detected public
secret/internal-path/raw-error leakage.

## Purpose

Merge exactly the Luna-accepted PR #304 candidate and perform only the
WP07 post-merge lifecycle reconciliation required to establish the
canonical WP07 boundary.

This authority does **not** begin WP08 and does not publish Release
1.13.

## Mandatory pre-merge identity gate

Before mutation, independently verify:

-   PR #304 is Open, non-draft, unmerged;
-   PR base is `main`;
-   PR head is exactly `6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`;
-   PR base/predecessor relationship remains consistent with
    `f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`;
-   no new commit has appeared after Luna acceptance;
-   the exact PR changed-path set remains the same nine accepted WP07
    paths;
-   required checks are not newly failing;
-   `README.md` remains unchanged;
-   dependencies and schema remain unchanged relative to the accepted
    candidate;
-   no merge conflict or unexpected mainline divergence changes the
    accepted diff.

If the head differs, changed paths differ, or the accepted diff is no
longer what Luna reviewed, **do not merge**. Stop at the governance
boundary and require renewed Luna acceptance.

## Merge authority

If and only if the identity gate passes, merge PR #304 into `main` using
the repository's established merge method.

Do not modify implementation while merging.

Record:

-   merge method;
-   PR #304 final state;
-   merge commit/full canonical `main` SHA;
-   whether the accepted candidate commit is reachable from canonical
    `main`;
-   final changed-path reconciliation.

Do not force-push, rewrite history, or bypass required repository
protections.

## Post-merge canonical verification

After merge:

1.  fetch/update canonical `main`;
2.  prove local/canonical `main` corresponds to the merged GitHub state;
3.  record the new full `main` SHA;
4.  run the smallest repository-native post-merge smoke/reconciliation
    checks necessary to prove the accepted candidate survived merge
    unchanged;
5.  verify root `README.md` remains unchanged;
6.  verify no dependency/schema drift;
7.  verify no accidental secret was introduced by merge/lifecycle
    handling.

Do not rebuild/redeploy merely because the PR was merged: the currently
deployed immutable image is already the Luna-accepted candidate. If
canonical source no longer corresponds to the accepted candidate due to
unexpected merge behavior, stop and report.

## WP07 lifecycle reconciliation

After a successful merge and canonical verification:

-   close Release 1.13 WP07 issue `#294` using the repository's
    established completion convention;
-   move its Project item to the established completed/Done state if the
    project workflow requires a separate transition;
-   preserve evidence that WP07 completed on the merged canonical SHA;
-   leave WP08 issue `#295` **Open and not started**;
-   leave Release 1.13 milestone `#64` **Open**.

If existing Release 1.13 governance requires a small WP07
completion/evidence update in a path already governed for WP07, perform
only that smallest update. If such a documentation update would require
opening a new PR after #304, stop and report the exact need rather than
silently creating additional lifecycle scope.

## Explicitly forbidden

This authority does not permit:

-   any implementation change;
-   Vike/provider changes;
-   Streamlit/UI changes;
-   cache/Worker changes;
-   Azure configuration/resource/tier changes;
-   image rebuild or redeployment absent an observed merge-integrity
    defect;
-   secret changes;
-   dependency changes;
-   schema changes;
-   root `README.md` changes;
-   WP08 implementation or planning mutation;
-   Release 1.14 work;
-   closing issue #295;
-   closing milestone #64;
-   creating a Release 1.13 tag;
-   creating a GitHub Release;
-   modifying the Release 1.13 front-door README;
-   declaring Release 1.13 complete.

## Retry-until-governance-boundary rule

Do not stop merely because a merge command, synchronization, project
transition, verification, or lifecycle tooling step fails.

Diagnose the failure, correct it when correction is purely mechanical
and within this authority, rerun the relevant operation, and continue.

Stop only if the next corrective action would exceed this authority or
invalidate Luna's accepted candidate.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, and
corrective authority required.

## PASS standard

PASS requires all of the following:

-   pre-merge identity gate passed;
-   exactly Luna-accepted PR #304 merged;
-   canonical `main` SHA recorded and reconciled;
-   accepted WP07 changes survived merge without unauthorized drift;
-   post-merge smoke/reconciliation passed;
-   README/dependencies/schema remain protected;
-   no secret introduced;
-   issue #294 closed/completed according to established repository
    workflow;
-   WP08 issue #295 remains Open/not started;
-   milestone #64 remains Open;
-   no Release 1.13 tag or GitHub Release created;
-   no WP08 implementation begun.

## Required final report

Return:

-   pre-merge PR #304 state/base/head;
-   exact accepted changed-path count/reconciliation;
-   identity-gate result;
-   merge method;
-   PR #304 post-merge state;
-   merge commit and full canonical `main` SHA;
-   reachability/reconciliation evidence;
-   post-merge validation results;
-   README/dependency/schema/secret results;
-   issue #294 final state and Project state;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   deployed accepted image/digest status;
-   any technical failures corrected;
-   any governance restrictions;
-   exact next authority.

End with exactly one of:

`RELEASE 1.13 WP07 MERGE AND POST-MERGE LIFECYCLE: PASS`

or

`RELEASE 1.13 WP07 MERGE AND POST-MERGE LIFECYCLE: BLOCKED`

A PASS establishes the canonical WP07 completion boundary only. It does
not authorize WP08 execution, Release 1.13 milestone closure, tag
creation, GitHub Release publication, or root README modification.

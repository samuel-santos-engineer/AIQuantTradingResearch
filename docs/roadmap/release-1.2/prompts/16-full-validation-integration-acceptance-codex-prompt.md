# Release 1.2 WP16 --- Full Validation, Integration & Acceptance --- Codex Execution Authority

## Execution model

Use **GPT-5.6 Sol** for this work package.

WP16 is the release-wide reconciliation and integration authority. It
requires high-confidence reasoning across the complete Release 1.2
candidate, the authoritative execution plan/file manifest, architecture,
tests, Git state, GitHub lifecycle, and integration boundaries.

------------------------------------------------------------------------

## 1. Authority

You are authorized to execute **Release 1.2 WP16 --- Full Validation,
Integration & Acceptance** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: `#136`
-   Milestone:
    `#53 — Phase 3 - Release 1.2: Research Dataset Foundation`
-   Project: `#2 — AIQuantTradingResearch Engineering Roadmap`

This is the final implementation/integration work package for Release
1.2.

Read completely before mutation:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  this WP16 authority
4.  all accepted Release 1.2 WP01--WP15 authority/evidence available in
    the repository/worktree
5.  the Release 1.2 semantic architecture artifacts
6.  current production source and permanent tests
7.  relevant Release 1.1 persistence authorities/current implementation
8.  GitHub issue #136, milestone #53, and Project #2 state

The execution plan and file manifest remain authoritative. Do not
silently repair an authority contradiction. Stop and report the smallest
corrective authority required if a mandatory requirement conflicts with
repository truth.

------------------------------------------------------------------------

## 2. Required starting state

Before changing Git or GitHub lifecycle state, prove:

-   Release 1.1 is closed and remains intact.
-   WP01--WP15 Release 1.2 issues are Closed/Done.
-   issue #136 is Open/Backlog.
-   milestone #53 is Open.
-   no WP17+ Release 1.2 implementation package exists.
-   Release 1.3 implementation has not started.
-   local repository identity is correct.
-   current branch is `main`.
-   local `main` equals `origin/main`.
-   ahead/behind is `0/0`.
-   no files are staged.
-   every tracked/untracked path is classified.
-   all cumulative Release 1.2 candidate paths are expected under the
    authoritative manifest or explicitly accepted authority.
-   unexpected or ambiguous paths = 0.

Do not discard, normalize, regenerate, or rewrite accepted cumulative
WP01--WP15 work merely because it is uncommitted.

Only after the starting-state gates pass may issue #136 move from
Backlog to In Progress.

------------------------------------------------------------------------

## 3. Accepted technical baseline

The accepted WP15 handoff baseline is:

-   Domain.Tests: **11**
-   Application.Tests: **60**
-   Infrastructure.Tests: **87**
-   Architecture.Tests: **13**
-   Permanent total: **171**

Expected production dependency graph:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Expected schema state:

-   Release 1.1 historical-observation foundation preserved.
-   SQLite schema current version: **2**.
-   v1 → v2 upgrade supported and non-destructive.
-   dataset snapshot/catalog evidence uses the accepted v2 physical
    model.

WP16 must not casually change these counts, architecture rules, or
schema. Any delta must be explicitly required by the authoritative
manifest and reconciled before proceeding.

------------------------------------------------------------------------

## 4. WP16 objective

Validate the complete Release 1.2 candidate as one coherent release,
prove reproducibility from a clean checkout, integrate it into exactly
one release branch/commit candidate, push it without history rewriting,
and open a review-ready pull request against `main`.

WP16 is an **integration and acceptance** package, not a redesign
package.

Do not introduce new product behavior to make validation pass.

------------------------------------------------------------------------

## 5. Candidate reconciliation

Build the candidate inventory from repository truth and
`RELEASE_1.2_FILE_MANIFEST.md`.

Prove and report:

-   exact governed candidate path count;
-   every required path present;
-   missing governed paths = 0;
-   unexpected governed paths = 0;
-   duplicate logical artifacts = 0;
-   temporary/generated residue = 0;
-   no out-of-band authority accidentally included;
-   no Release 1.3 implementation path included.

Validate Release 1.2 prompt/governance conventions required by the
manifest, including standard prompt/chat pairs and five-line bootstrap
companions where applicable.

Do not guess the candidate count in advance. Derive it from the accepted
manifest and actual worktree.

------------------------------------------------------------------------

## 6. Semantic reconciliation

Validate the complete accepted Release 1.2 model.

### Dataset definition and reproducibility

Prove preservation of:

-   exact single target;
-   explicit `[from, to)` selection;
-   deterministic semantic-instant ordering;
-   valid successful empty materialization;
-   exact `DateTimeOffset` offset fidelity;
-   exact decimal fidelity;
-   deterministic equivalent re-materialization.

### Identity/version/provenance

Prove preservation of:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity;
-   Dataset Version = immutable Snapshot Identity semantics;
-   `aiq-dataset-identity-v1`;
-   canonical deterministic representation;
-   SHA-256 / 64 lowercase hexadecimal fingerprints;
-   provenance and narrow lineage;
-   source-state change distinguishability;
-   snapshot immutability;
-   no identity reassignment.

### Application boundaries

Prove:

-   contracts remain provider independent;
-   contracts remain storage independent;
-   materialization remains Application-owned;
-   materialization reads accepted Release 1.1 observations through the
    Application seam;
-   catalog metadata remains immutable semantic evidence;
-   WP10 integration remains bounded and fail-stop.

### Physical persistence

Prove:

-   SQLite ownership remains Infrastructure-only;
-   schema version 2 is authoritative;
-   v1 → v2 upgrade preserves Release 1.1 history;
-   snapshot descriptor/membership evidence is immutable;
-   empty snapshots are represented without sentinel observations;
-   equivalent evidence is non-mutating;
-   contradictory same-identity evidence is an integrity conflict;
-   multiple immutable versions coexist;
-   writes are atomic;
-   exact lookup is by typed Snapshot Identity;
-   catalog miss remains `NotFound`;
-   timestamp offsets and decimals round-trip exactly.

### Failure mapping

Prove:

-   `Unavailable` and `InvalidData` remain distinct storage failures;
-   integrity conflict remains distinct and non-destructive;
-   unknown/unclassified failures are not silently swallowed;
-   SQLite implementation details do not leak into Domain/Application
    public semantics.

### Composition and Worker

Prove:

-   required dataset services resolve through accepted DI;
-   graph resolution itself does not create a database;
-   `Persistence:DatabasePath` remains the storage configuration
    boundary;
-   `Dataset:Target`, `Dataset:From`, and `Dataset:To` remain the
    bounded execution inputs;
-   valid Worker execution performs one bounded
    materialization/integration operation;
-   repeat execution against equivalent source state produces
    equivalent-existing behavior;
-   no scheduling, streaming, refresh loop, DAG, retry orchestration, or
    Release 1.3 pipeline behavior exists.

------------------------------------------------------------------------

## 7. Permanent test acceptance

Run the authoritative permanent suites.

Required accepted baseline unless an authority explicitly says
otherwise:

-   Domain.Tests: 11/11
-   Application.Tests: 60/60
-   Infrastructure.Tests: 87/87
-   Architecture.Tests: 13/13
-   Total: 171/171
-   skipped: 0

Run canonical repository verification in the configuration established
by the accepted repository workflow. Prefer the Release configuration
where prior accepted WP evidence requires it.

Required:

-   restore: PASS;
-   format verification: PASS;
-   build: PASS;
-   warnings/errors: 0/0;
-   permanent tests: PASS;
-   architecture tests: PASS;
-   canonical `eng/verify.ps1`: PASS;
-   Gitleaks/secret scan: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   documentation/link audit: PASS where repository conventions require
    it;
-   temporary SQLite residue: 0.

Do not weaken tests, suppress warnings, bypass architecture rules, or
alter security tooling to obtain a pass.

------------------------------------------------------------------------

## 8. Release 1.1 regression protection

Explicitly prove the Release 1.1 historical-observation foundation
remains valid after the cumulative Release 1.2 candidate.

At minimum reconcile:

-   historical SQLite persistence/retrieval;
-   idempotency;
-   conflict preservation;
-   atomicity;
-   immutable accepted history;
-   target isolation;
-   deterministic ordering;
-   timestamp/offset fidelity;
-   decimal fidelity;
-   successful empty retrieval;
-   failure mapping;
-   existing persistence configuration;
-   operation-owned connection lifecycle.

------------------------------------------------------------------------

## 9. Fresh-checkout reproducibility

Before opening the PR, validate the exact integration candidate from a
clean detached checkout/worktree.

The fresh validation must use the exact candidate commit that will be
pushed.

Prove:

-   restore: PASS;
-   format verification: PASS;
-   build: PASS;
-   warnings/errors: 0/0;
-   permanent tests: 171/171 unless an authorized final count differs;
-   Architecture.Tests: 13/13;
-   canonical verification: PASS;
-   security scan: PASS;
-   temporary database residue: 0;
-   checkout working tree: CLEAN.

Do not use uncommitted files from the original worktree to make the
fresh checkout pass.

------------------------------------------------------------------------

## 10. Integration branch and commit authority

Only after all pre-integration acceptance gates pass:

1.  create/switch to the dedicated Release 1.2 integration branch:
    `release/1.2-research-dataset-foundation`
2.  stage **only** the fully reconciled Release 1.2 candidate.
3.  verify staged path inventory against the authoritative manifest.
4.  verify no secrets/generated residue/out-of-scope files are staged.
5.  create exactly **one integration commit** for the Release 1.2
    candidate.

Preferred commit message:

`feat: establish Release 1.2 research dataset foundation`

Do not:

-   amend historical commits;
-   rebase published history;
-   squash unrelated existing history;
-   force push;
-   create multiple Release 1.2 integration commits unless an
    unavoidable external Git failure makes the first commit
    unusable---in that case stop and report rather than improvising;
-   merge to `main`.

After commit, require a clean working tree and rerun post-commit
acceptance before push.

------------------------------------------------------------------------

## 11. Push authority

After post-commit and fresh-checkout validation pass:

-   push the integration branch normally;
-   set upstream if required;
-   never force push;
-   prove local branch SHA equals remote branch SHA;
-   prove ahead/behind upstream = `0/0`.

Do not push `main`.

------------------------------------------------------------------------

## 12. Pull request authority

Create exactly one review-ready pull request:

-   base: `main`
-   head: `release/1.2-research-dataset-foundation`
-   draft: NO
-   auto-merge: disabled
-   merge: DO NOT PERFORM

Preferred title:

`Release 1.2 — Research Dataset Foundation`

The PR body must summarize:

-   Release 1.2 purpose;
-   deterministic dataset definition/reproducibility;
-   identity/version/provenance model;
-   Application contracts/materialization/catalog model;
-   SQLite schema v2 and v1→v2 evolution;
-   immutable snapshot/catalog persistence;
-   bounded integration/failure mapping;
-   DI/Worker bounded execution;
-   permanent test counts;
-   architecture/security/offline evidence;
-   fresh-checkout reproducibility;
-   explicit Release 1.3 exclusions.

Include issue closure/reference information consistent with repository
conventions, but do not cause premature merge or milestone closure.

If a competing authoritative Release 1.2 integration PR already exists,
stop and report rather than creating a duplicate.

------------------------------------------------------------------------

## 13. GitHub lifecycle

Issue #136 may be closed/Done only after:

-   candidate reconciliation passes;
-   all technical acceptance passes;
-   integration commit exists;
-   post-commit validation passes;
-   fresh-checkout validation passes;
-   branch is pushed successfully;
-   review-ready PR exists and points to the exact accepted head SHA.

At completion:

-   #121--#136 should be Closed/Done;
-   milestone #53 must remain **OPEN** pending post-merge closure;
-   PR must remain **OPEN / UNMERGED**;
-   no Release 1.3 planning/implementation mutation is authorized.

Do not close milestone #53 in WP16.

Do not create a GitHub Release or tag unless separately authorized.

Do not delete the integration branch.

------------------------------------------------------------------------

## 14. Scope prohibitions

WP16 must not:

-   redesign WP02--WP12 semantics;
-   introduce new dataset capabilities;
-   add Release 1.3 pipelines;
-   add scheduling/streaming/automatic refresh;
-   add retry/circuit-breaker orchestration;
-   change identity scheme without corrective authority;
-   redesign schema v2;
-   alter Release 1.1 history;
-   modify permanent tests merely to hide failures;
-   weaken secret scanning;
-   change package/project references unless the manifest explicitly
    requires it;
-   merge the PR;
-   close milestone #53;
-   tag/release;
-   modify unrelated GitHub objects.

If validation exposes a genuine defect requiring out-of-scope production
or test redesign, stop and report it.

------------------------------------------------------------------------

## 15. Expected WP16 repository delta

WP16 should normally add **no new product behavior**.

Expected integration delta is the cumulative accepted Release 1.2
candidate becoming staged/committed on the integration branch.

Any WP16-specific repository content not already authorized by the
manifest must be treated as suspicious and reconciled before inclusion.

------------------------------------------------------------------------

## 16. Required final report

Produce a detailed execution report containing at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Initial Repository/Git State
4.  Release 1.1 Closure/Reconciliation
5.  WP01--WP15 Lifecycle Gate
6.  Candidate Reconciliation
7.  Governance Prompt-Pair Validation
8.  Dataset Semantic Reconciliation
9.  Identity/Version/Provenance Reconciliation
10. Application Boundary Reconciliation
11. Schema v2 / Upgrade Reconciliation
12. Snapshot Persistence Reconciliation
13. Catalog Reconciliation
14. Integration Reconciliation
15. Validation/Failure-Mapping Reconciliation
16. DI/Worker Reconciliation
17. Release 1.3 Exclusion Check
18. Permanent Test Evidence
19. Canonical Verification
20. Architecture Validation
21. Security/Offline Validation
22. Release 1.1 Regression Evidence
23. Documentation Acceptance
24. Candidate Path Accounting
25. Integration Branch
26. Integration Commit SHA / Message / Parent / Commit Count
27. Post-Commit Validation
28. Fresh-Checkout Reproducibility
29. Push Evidence
30. Pull Request Number / URL / Base / Head / SHA / State
31. GitHub Lifecycle State
32. Mutation Accounting
33. Final Repository State
34. Findings / Blockers
35. Acceptance Matrix
36. Final Decision
37. Next Authorized Lifecycle Action

Report exact observed counts and SHAs. Do not fabricate values.

------------------------------------------------------------------------

## 17. Terminal completion marker

Only when every mandatory WP16 gate passes, end with:

``` text
RELEASE 1.2 WP16 COMPLETE

FULL VALIDATION, INTEGRATION & ACCEPTANCE:
Manifest reconciliation: PASS
Unexpected candidate paths: 0
Build warnings/errors: 0/0
Domain.Tests: 11/11
Application.Tests: 60/60
Infrastructure.Tests: 87/87
Architecture.Tests: 13/13
Permanent tests: 171/171
Canonical verification: PASS
Architecture acceptance: PASS
Documentation acceptance: PASS
Dataset semantic acceptance: PASS
Identity/version/provenance acceptance: PASS
Schema v2 / v1→v2 acceptance: PASS
Snapshot persistence acceptance: PASS
Catalog acceptance: PASS
Integration acceptance: PASS
Failure-mapping acceptance: PASS
DI/Worker bounded execution acceptance: PASS
Release 1.1 regression: PASS
Security/offline validation: PASS
Fresh-checkout reproducibility: PASS
Working tree: CLEAN
Integration branch pushed: PASS
Pull request: OPEN / REVIEW-READY
Pull request merged: NO
Issue #136: CLOSED / DONE
Milestone #53: OPEN
Release 1.3 implementation started: NO

NEXT AUTHORIZED LIFECYCLE ACTION:
Human review and explicit merge authorization for the Release 1.2 integration pull request.
```

If any mandatory gate fails, do **not** emit the completion marker.
Emit:

`RELEASE 1.2 WP16 BLOCKED`

and explain the exact blocker and smallest corrective authority
required.

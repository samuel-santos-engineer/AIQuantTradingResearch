# Release 1.7 WP13 --- Full Validation, Integration & Acceptance --- Codex Authority

## 1. Mission

Execute Release 1.7 WP13 --- **Full Validation, Integration &
Acceptance** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#209`

Frozen predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

This is the final Release 1.7 work-package acceptance gate before any
separate human-authorized Git integration workflow.

WP13 validates and reconciles the complete Release 1.7 candidate. It
must not add features, repair accepted design opportunistically, stage,
commit, branch, push, create a PR, tag, release, or merge.

------------------------------------------------------------------------

## 2. Authoritative Starting State

Require:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: `0`;
-   Release 1.6 remains closed at the frozen baseline;
-   #197--#208: 12/12 CLOSED / Done;
-   #209: OPEN / Backlog;
-   milestone #55: OPEN, 1 open / 12 closed;
-   Project #2 Release 1.7 membership: 13/13;
-   duplicate Release 1.7 Project items: 0;
-   schema: v3;
-   current permanent-test baseline:
    -   Domain: 11;
    -   Application: 119;
    -   Infrastructure: 125;
    -   Architecture: 13;
    -   total: 268;
-   skipped tests: 0;
-   Release 1.8 implementation: absent.

Expected cumulative uncommitted Release 1.7 governed content may exist
on `main` according to the established release workflow. Reconcile it
against the Release 1.7 manifest and completed WP authorities rather
than treating expected governed content as unexplained.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 3. Authoritative Inputs

Read completely before acceptance:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   all retained Release 1.7 WP01--WP13 governed prompt pairs that the
    manifest/lifecycle classifies as candidate content;
-   Release 1.7 architecture documents created by WP02, WP03, and WP06;
-   Release 1.7 production changes from WP04, WP05, WP07, WP09, and
    WP10;
-   Release 1.7 permanent tests from WP11;
-   WP12-aligned current-state documentation;
-   WP12 governed arithmetic-correction evidence;
-   relevant Release 1.6 durable Experiment architecture,
    implementation, tests, and closure evidence;
-   current 13 Architecture.Tests;
-   `docs/handbook/ENGINEERING_PLAYBOOK.md`;
-   GitHub issues #197--#209;
-   milestone #55;
-   Project #2 Release 1.7 items and dependency relationships.

Do not infer acceptance solely from prior execution reports. Validate
repository and GitHub state directly.

------------------------------------------------------------------------

## 4. Governed Arithmetic

The Release 1.7 permanent-test arithmetic is frozen for WP13 as:

-   Domain: `11`
-   Application: `119`
-   Infrastructure: `125`
-   Architecture: `13`

Therefore:

`11 + 119 + 125 + 13 = 268`

Authoritative Release 1.7 baseline:

`268/268`

The earlier `266` total was an arithmetic error and is superseded.

For historical reconciliation:

-   pre-WP11: `11 + 111 + 117 + 13 = 252`;
-   WP11 delta: `+8 Application +8 Infrastructure = +16`;
-   post-WP11: `252 + 16 = 268`.

Do not modify tests to fit any stale total.

Any active Release 1.7 current-state claim of 266 referring to this same
baseline is a blocker unless already corrected under the WP12 corrective
authority.

------------------------------------------------------------------------

## 5. Release 1.7 Acceptance Objective

Prove that Release 1.7 delivers exactly:

**Durable Experiment Evidence Discovery**

with the accepted boundaries below.

### Query Contract

-   exact Snapshot Identity;
-   exact Experiment Definition Identity;
-   mandatory positive caller-supplied maximum;
-   no invented maximum ceiling;
-   deterministic Experiment Result Identity ascending binary ordering;
-   successful immutable empty collection when no matches exist;
-   zero matches are not `NotFound`.

### Identity

-   `aiq-experiment-identity-v1` remains the Experiment Result identity
    scheme;
-   no discovery identity;
-   no query identity;
-   discovery does not create, mutate, or reinterpret durable identity.

### Fidelity

Returned evidence preserves:

-   Experiment Result Identity;
-   Snapshot Identity;
-   Snapshot Version;
-   Experiment Definition Identity;
-   Feature Set Identity;
-   provenance/lineage;
-   count;
-   aggregate presence/absence;
-   mean/minimum/maximum when present;
-   canonical decimal/signed-zero semantics;
-   empty Experiment Result semantics.

An empty Experiment Result remains distinct from a successful discovery
query returning zero Experiment Results.

------------------------------------------------------------------------

## 6. Application Acceptance

Verify current Application behavior and boundaries:

-   `DurableExperimentDiscoveryRequest` exists;
-   `DurableExperimentDiscoveryResult` is immutable according to the
    accepted contract;
-   `IDurableExperimentEvidenceDiscoveryStore` is storage-independent;
-   `IDurableExperimentDiscoveryUseCase` exists;
-   valid requests invoke the store exactly once;
-   exact request dimensions/maximum are forwarded;
-   null/non-positive requests produce `InvalidRequest` with zero store
    calls;
-   empty/non-empty successes pass through unchanged;
-   evidence is not recomputed/normalized by Application;
-   bounded classified failures pass through;
-   unknown defects propagate;
-   no retry;
-   no fallback;
-   no write;
-   no provider/network mechanics;
-   no SQLite/Infrastructure type leakage.

------------------------------------------------------------------------

## 7. Infrastructure Acceptance

Verify current SQLite discovery behavior:

-   `SqliteExperimentResultStore` implements the discovery-store
    contract;
-   exact Snapshot + Experiment Definition filtering;
-   explicit binary Experiment Result Identity ascending ordering;
-   identities and maximum are parameterized;
-   maximum returns the deterministic ordered prefix;
-   zero matches return successful empty evidence;
-   existing 19-column mapper reconstructs exact durable evidence;
-   discovery is read-only;
-   durable row identities/count remain unchanged;
-   schema remains v3;
-   table/column/index/migration delta is zero;
-   WP06 accepted the bounded `SCAN experiment_results` query plan;
-   existing binary primary-key ordering avoids a temporary ordering
    B-tree;
-   no structural optimization/index is introduced by WP13;
-   dependency-unavailable behavior is bounded;
-   invalid persisted evidence follows the accepted classification
    boundary;
-   unknown defects are not broadly normalized.

------------------------------------------------------------------------

## 8. Failure Acceptance

Reconcile the Release 1.6 vocabulary without inventing
discovery-specific failures:

-   `InvalidRequest`;
-   `NotFound`;
-   `DependencyUnavailable`;
-   `InvalidEvidence`;
-   `IntegrityConflict`.

For valid discovery:

-   zero matches are successful, not `NotFound`;
-   `DependencyUnavailable` is directly reachable;
-   `InvalidEvidence` is bounded through reconstruction/schema
    validation;
-   `IntegrityConflict` remains a lower-layer durable acceptance
    invariant and is not artificially manufactured through read-only
    corruption;
-   unknown defects propagate.

Require:

-   no retry;
-   no repair;
-   no fallback;
-   no skipped malformed row;
-   no partial-success collection after reconstruction failure;
-   no mutation on failure.

------------------------------------------------------------------------

## 9. Dependency Injection Acceptance

Verify:

-   exactly one effective discovery use-case registration;
-   exactly one effective discovery-store registration;
-   expected implementation resolution;
-   accepted transient lifetime behavior;
-   `SqliteExperimentResultStore` forwarding/shared implementation
    semantics remain correct;
-   DI resolution is side-effect-free;
-   no database/schema is created merely by resolving services;
-   predecessor registrations remain valid;
-   no duplicate registration drift.

------------------------------------------------------------------------

## 10. Worker Acceptance

Verify explicit one-shot Discovery mode.

Required precedence:

`Discovery → Durable Experiment → Experiment → Feature → pipeline`

Require:

-   exact Snapshot Identity parsing;
-   exact Experiment Definition Identity parsing;
-   positive maximum parsing;
-   partial/malformed discovery intent terminates unsuccessfully;
-   malformed/partial intent cannot fall back;
-   valid discovery invokes `IDurableExperimentDiscoveryUseCase` exactly
    once;
-   deterministic evidence presentation;
-   successful empty discovery;
-   bounded maximum;
-   conflicting selectors choose Discovery;
-   Durable Experiment predecessor mode preserved;
-   Experiment predecessor mode preserved;
-   Feature predecessor mode preserved;
-   pipeline predecessor mode preserved;
-   Worker SQL/store implementation: 0;
-   provider fallback: 0;
-   retry/repair: 0.

------------------------------------------------------------------------

## 11. Permanent Regression Acceptance

Inspect WP11 permanent coverage and prove it protects the accepted
Release 1.7 behavior.

Require coverage for:

-   request validation;
-   exact one-call forwarding;
-   empty/non-empty pass-through;
-   evidence/provenance/decimal fidelity;
-   bounded failures;
-   unknown-defect propagation;
-   exact dual-identity filtering;
-   binary identity ordering;
-   caller maximum;
-   empty Experiment Result fidelity;
-   read-only state;
-   safe dependency-unavailable boundary;
-   safe invalid-evidence boundary;
-   DI forwarding/cardinality;
-   repository-native `--no-build` Worker integration.

Confirm `IntegrityConflict` remains covered by the Release 1.6 durable
acceptance-conflict regression rather than unauthorized discovery-state
corruption.

Do not add tests in WP13 merely to increase count.

------------------------------------------------------------------------

## 12. Architecture Acceptance

Review the existing Architecture.Tests and the final production graph.

Require:

-   Architecture.Tests: 13/13;
-   no Release 1.7 architecture-test delta unless already explicitly
    governed;
-   dependency graph unchanged in direction and acyclic;
-   Domain independent;
-   Application free of Infrastructure/Worker/provider/SQLite
    dependencies;
-   Infrastructure depends inward through Application/Domain contracts
    as designed;
-   Worker remains composition root;
-   no reverse dependency;
-   no provider/storage type leakage into Domain/Application;
-   no production project/reference drift.

WP12 zero-delta-first architecture decision must remain defensible.

Do not perform the deferred Release 1.8 Architecture & Design Review
Register.

------------------------------------------------------------------------

## 13. Documentation Acceptance

Verify WP12 current-state documentation is truthful and internally
consistent.

Require current-state alignment for:

-   Durable Experiment Evidence Discovery;
-   query dimensions;
-   positive maximum;
-   ordering;
-   successful empty collection;
-   identity/provenance/fidelity;
-   schema v3;
-   `experiment_results`;
-   read-only persistence;
-   Application/Infrastructure ownership;
-   DI;
-   Worker routing/configuration;
-   failure semantics;
-   predecessor mode preservation;
-   permanent test baseline `268`.

Search active Release 1.7/current-state documentation for stale baseline
claims of:

`266`

Classify each occurrence.

Any occurrence referring to the current 11/119/125/13 baseline must be
corrected already or WP13 blocks. Do not rewrite unrelated historical
evidence mechanically.

Require Markdown links and local anchors to remain valid.

------------------------------------------------------------------------

## 14. Explicit Scope Exclusions

Prove Release 1.7 did not introduce:

-   broad registry/history/search semantics;
-   mutation/edit/delete of Experiment Results;
-   scheduling/background execution;
-   provider acquisition;
-   network product execution;
-   backtesting;
-   portfolio/risk simulation;
-   Machine Learning implementation;
-   Explainable AI implementation;
-   Release 1.8 implementation;
-   schema v4;
-   new table/column/index/migration;
-   cloud deployment;
-   public API/UI;
-   broad pagination;
-   discovery/query identity.

Do not treat future roadmap concepts as current capabilities.

------------------------------------------------------------------------

## 15. Predecessor Regression Acceptance

Validate that Release 1.7 preserves accepted predecessor behavior,
especially:

-   Release 1.6 Durable Experiment evidence foundation;
-   Release 1.5 Experiment execution;
-   Release 1.4 Feature execution;
-   Release 1.3 five-stage pipeline;
-   Release 1.1--1.2 persistence/data foundations as represented by
    existing permanent suites and architecture;
-   schema v3;
-   provider abstraction/isolation;
-   retry/failure semantics outside discovery;
-   feature persistence and durable Experiment acceptance.

Use permanent suites and focused read-back rather than inventing new
probes unless a specific acceptance gap requires safe disposable
validation.

------------------------------------------------------------------------

## 16. Process-Level Fixture Acceptance

Verify the Release 1.7 process-level validation path complied with the
engineering playbook:

-   repository-native fixture identified during planning;
-   `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable acceptance path;
-   existing friend-assembly/test-host boundary;
-   existing `--no-build` Worker runner;
-   deterministic evidence;
-   complete process/database cleanup.

Confirm no hidden permanent test hook, external probe project, or
production visibility change was introduced solely for validation.

------------------------------------------------------------------------

## 17. Candidate Reconciliation

Use `RELEASE_1.7_FILE_MANIFEST.md` plus completed WP evidence to
construct the exact Release 1.7 integration candidate.

Classify every repository path relative to frozen baseline:

1.  governed Release 1.7 candidate;
2.  explicitly excluded execution-only/out-of-band authority;
3.  pre-existing baseline content;
4.  unexpected/unexplained.

Require:

-   missing governed paths: 0;
-   unexpected governed paths: 0;
-   unexplained paths: 0;
-   staged paths: 0.

Do not guess the candidate path count from earlier releases.

Report the exact Release 1.7 candidate count derived from the repository
and manifest.

For prompt pairs that are candidate-governed, validate companion
structure according to repository conventions.

For execution-only corrective authorities, apply only their established
lifecycle classification; do not silently include them in the
integration candidate.

------------------------------------------------------------------------

## 18. Planning Artifact Reconciliation

The three authoritative Release 1.7 planning artifacts are:

-   `RELEASE_1.7_DEFINITION.md`
-   `RELEASE_1.7_EXECUTION_PLAN.md`
-   `RELEASE_1.7_FILE_MANIFEST.md`

Reconcile their intended integration classification exactly as governed
by the manifest/workflow.

Do not accidentally discard them merely because they began as untracked
planning artifacts.

Do not change their substantive scope during WP13.

------------------------------------------------------------------------

## 19. Prompt-Pair Validation

For every governed Release 1.7 prompt pair intended for integration:

-   full prompt exists;
-   companion exists;
-   naming is correct;
-   companion contains exactly five non-empty logical lines where that
    convention applies;
-   terminal newline present;
-   trailing whitespace: 0.

Report:

-   governed prompt-pair count;
-   valid count;
-   malformed count;
-   execution-only excluded prompt pairs separately.

Do not repair substantive prompt history under WP13 authority.

A purely mechanical formatting defect that changes candidate content
requires separate corrective authority unless explicitly allowed by the
manifest.

------------------------------------------------------------------------

## 20. Security and Isolation Acceptance

Require:

-   Gitleaks: PASS;
-   committed/staged real credentials: 0;
-   provider calls during acceptance: 0 unless an authoritative existing
    canonical verifier explicitly requires otherwise;
-   external network product activity: 0;
-   test dependence on real API keys: 0;
-   temporary secrets: 0;
-   generated secret-bearing logs: 0;
-   residue: 0.

Do not contact Twelve Data or any external market-data provider for WP13
acceptance.

------------------------------------------------------------------------

## 21. Repository Hygiene

Require:

-   conflict markers: 0;
-   trailing whitespace: 0;
-   malformed terminal newlines: 0;
-   broken Markdown local links: 0;
-   unexpected generated binaries: 0;
-   temporary databases/WAL/SHM: 0;
-   temporary probe projects/scripts/logs: 0;
-   retained validation processes: 0;
-   staged paths: 0;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS.

Do not clean unrelated user files without authority.

------------------------------------------------------------------------

## 22. Package / Project / Reference / Schema Reconciliation

Compare candidate against frozen baseline.

Require:

-   package delta: 0;
-   project delta: 0;
-   production project-reference delta: 0;
-   schema version: v3;
-   table delta: 0;
-   column delta: 0;
-   index delta: 0;
-   migration delta: 0.

Any unexpected structural delta blocks acceptance.

------------------------------------------------------------------------

## 23. Canonical Verification

Run the repository's canonical verification from the Release 1.7
candidate state.

Require:

-   restore: PASS;
-   formatting: PASS;
-   Gitleaks: PASS;
-   Release build: PASS;
-   build warnings/errors: `0/0`;
-   Domain.Tests: `11/11`;
-   Application.Tests: `119/119`;
-   Infrastructure.Tests: `125/125`;
-   Architecture.Tests: `13/13`;
-   permanent total: `268/268`;
-   skipped: `0`;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   Markdown links: PASS;
-   schema v3: PASS;
-   dependency graph: PASS;
-   package/project/reference preservation: PASS;
-   provider/network/credential isolation: PASS;
-   residue: 0.

If canonical tooling reports a total inconsistent with the four project
counts, stop and reconcile the actual executed counts. Do not repeat the
WP11/WP12 arithmetic mistake.

------------------------------------------------------------------------

## 24. GitHub Governance Read-Back

Before closing #209 verify:

-   milestone #55 contains exactly the authoritative Release 1.7
    WP01--WP13 issues;
-   #197--#208 are Closed / Done;
-   #209 is the only open Release 1.7 WP;
-   Project #2 membership is exactly 13/13;
-   duplicate Project items: 0;
-   Status/Priority/Release/Area fields match authoritative planning;
-   dependency graph matches the planned linear WP graph;
-   predecessor Release project fields remain restored;
-   Release 1.6 issues remain unchanged;
-   no Release 1.8 WP issues were created;
-   no Release 1.7 integration PR/branch was created prematurely.

Report exact read-back counts.

------------------------------------------------------------------------

## 25. WP13 Repository Mutation Rule

WP13 is acceptance-oriented.

Expected repository-content delta introduced by WP13 itself:

`0`

Do not modify production, tests, docs, planning artifacts, or prompts
merely to make acceptance pass.

If a candidate defect is found, stop and request the smallest corrective
authority.

Disposable validation is permitted only when necessary and must leave
residue 0.

------------------------------------------------------------------------

## 26. WP13 GitHub Lifecycle

Only after every acceptance gate passes:

1.  move #209 Backlog → In Progress if necessary;
2.  post the final WP13 acceptance evidence;
3.  close #209;
4.  set #209 Project Status to Done;
5.  close milestone #55 only after confirming all 13 issues are closed.

Final required Release 1.7 planning state:

-   #197--#209: 13/13 CLOSED / Done;
-   Project #2 membership: 13/13;
-   duplicate items: 0;
-   milestone #55: CLOSED, 0 open / 13 closed.

Do not create:

-   integration branch;
-   commit;
-   push;
-   PR;
-   tag;
-   GitHub Release.

Those require separate human authorization.

------------------------------------------------------------------------

## 27. Integration-Readiness Decision

Release 1.7 is integration-ready only if all gates pass.

Report explicitly:

`Release 1.7 integration-ready: YES`

or:

`Release 1.7 integration-ready: NO`

If YES, report:

-   frozen baseline SHA;
-   exact candidate path count;
-   exact excluded/out-of-band path count;
-   unexpected path count;
-   permanent baseline 268/268;
-   schema v3;
-   package/project/reference deltas;
-   GitHub lifecycle final state;
-   confirmation that no Git integration mutation occurred.

Do not create the integration authority automatically.

------------------------------------------------------------------------

## 28. Release 1.8 Boundary

The planned compact **Architecture & Design Review Register** remains
deferred until after Release 1.7 is integrated and closed under separate
governance.

WP13 must not:

-   create Release 1.8 planning;
-   create Release 1.8 issues/milestone;
-   perform the review register;
-   redesign Release 1.7;
-   reintroduce backtesting;
-   begin Machine Learning implementation.

Acceptance of Release 1.7 is not authorization for Release 1.8.

------------------------------------------------------------------------

## 29. Stop Conditions

Stop with:

`RELEASE 1.7 WP13 BLOCKED`

if any of the following occurs:

-   starting state materially differs;
-   authoritative Release 1.7 artifacts conflict;
-   active current-state arithmetic still claims 266 for the
    11/119/125/13 baseline;
-   candidate reconciliation has missing/unexpected/unexplained paths;
-   production/test/documentation behavior conflicts with frozen Release
    1.7 semantics;
-   canonical verification fails;
-   permanent counts differ unexpectedly;
-   architecture boundary fails;
-   schema/package/project/reference drift exists;
-   security/isolation fails;
-   residue cannot be removed safely;
-   prompt-pair governance is malformed;
-   GitHub Project/milestone/dependency state drifts;
-   acceptance requires content mutation;
-   Release 1.8 work is discovered.

Report the exact blocker and smallest corrective authority required.

Do not repair under WP13 unless the authority explicitly permits that
exact action.

------------------------------------------------------------------------

## 30. Required Acceptance Report

Report at minimum:

### Starting State

-   branch;
-   HEAD/origin SHA;
-   ahead/behind;
-   staged paths;
-   issue/milestone state;
-   schema;
-   permanent baseline.

### Candidate

-   governed candidate path count;
-   excluded/out-of-band path count;
-   missing paths;
-   unexpected paths;
-   unexplained paths;
-   prompt-pair count and validity.

### Release Semantics

-   query;
-   ordering;
-   maximum;
-   empty semantics;
-   identity;
-   fidelity;
-   failures;
-   read-only behavior.

### Layer Acceptance

-   Application;
-   Infrastructure;
-   DI;
-   Worker;
-   Architecture;
-   documentation;
-   permanent regression coverage.

### Preservation

-   Release 1.1--1.6 regressions;
-   schema;
-   dependency graph;
-   packages/projects/references;
-   explicit exclusions.

### Verification

-   restore;
-   build;
-   warnings/errors;
-   four project counts;
-   total 268/268;
-   skipped;
-   formatting;
-   Gitleaks;
-   diff/whitespace;
-   links;
-   provider/network/credentials;
-   residue.

### GitHub

-   #197--#209 state;
-   Project membership/duplicates;
-   field/dependency reconciliation;
-   milestone #55 final state;
-   Release 1.8 absence.

### Mutation

-   WP13 repository-content delta;
-   staged/commit/branch/push/PR/tag/release mutations;
-   GitHub mutations limited to #209/milestone lifecycle.

### Decision

-   integration-ready YES/NO;
-   next authorized action.

------------------------------------------------------------------------

## 31. Success Markers

On full success end exactly:

`RELEASE 1.7 WP13 COMPLETE`

`RELEASE 1.7 ACCEPTED FOR INTEGRATION`

`NEXT AUTHORIZED ACTION: Human authorization of the separate Release 1.7 Git integration / commit / push / PR workflow.`

Do not perform Git integration automatically.

If blocked end exactly:

`RELEASE 1.7 WP13 BLOCKED`

and state the smallest corrective authority required.

# Release 1.7 WP08 --- Durable Evidence Discovery Failure Semantics --- Codex Authority

## 1. Mission

Execute Release 1.7 WP08 --- **Durable Evidence Discovery Failure
Semantics** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#204`

Frozen Release 1.6 baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor state:

-   WP01 #197: CLOSED / Done;
-   WP02 #198: CLOSED / Done;
-   WP03 #199: CLOSED / Done;
-   WP04 #200: CLOSED / Done;
-   WP05 #201: CLOSED / Done;
-   WP06 #202: CLOSED / Done;
-   WP07 #203: CLOSED / Done;
-   WP08 #204: OPEN / Backlog;
-   #205--#209: OPEN / Backlog;
-   milestone #55: OPEN, 6 open / 7 closed;
-   canonical permanent baseline: 250/250;
-   schema: v3.

Accepted WP07 implementation:

-   `SqliteExperimentResultStore` implements
    `IDurableExperimentEvidenceDiscoveryStore`;
-   exact Snapshot Identity + Experiment Definition Identity filtering;
-   explicit binary Experiment Result Identity ascending ordering;
-   identities and caller-supplied maximum are parameterized;
-   existing 19-column mapper is reused;
-   zero matches return successful empty evidence;
-   discovery performs no durable mutation;
-   existing unavailable/schema/invalid-evidence mappings are preserved;
-   unknown defects propagate;
-   schema remains v3;
-   permanent-test delta remains 0 because later Release 1.7 work owns
    permanent discovery regression coverage.

WP08 must freeze, validate, and where manifest-authorized minimally
complete the **failure semantics** around this existing discovery path.

WP08 is not authority to redesign discovery, change its successful
behavior, or perform the deferred Release 1.8 Architecture & Design
Review.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_PHYSICAL_ACCESS.md`
-   `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
-   `src/AIQuantTradingResearch.Application/Experiments/DurableExperimentDiscoveryUseCase.cs`
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultStore.cs`
-   existing Release 1.6 durable Experiment failure semantics and tests;
-   current SQLite exception/failure-classification implementation;
-   current schema-v3 opening/validation behavior;
-   GitHub issue #204.

Treat WP02--WP07 successful discovery semantics and physical
implementation as frozen predecessor authority.

------------------------------------------------------------------------

## 3. Flow Preservation

Preserve the prepared Release 1.7 execution flow.

Do not initiate:

-   broad architecture/design review;
-   contract redesign;
-   exception-taxonomy redesign;
-   storage redesign;
-   performance optimization;
-   source-layout refactoring.

Non-blocking architecture/design observations remain deferred to the
separately governed Release 1.8 Architecture & Design Review Register.

WP08 answers only:

> What are the exact bounded failure semantics of Durable Experiment
> Evidence Discovery, and does the current implementation preserve them
> without fallback, repair, retry, partial-success invention, or hidden
> normalization?

------------------------------------------------------------------------

## 4. Execution-Authority Lifecycle

The WP08 prompt pair follows the established Release 1.7
execution-authority lifecycle.

Do not:

-   stage it merely because it exists;
-   count it as production/test/schema mutation;
-   remove prior WP prompt pairs without separate authority;
-   allow expected untracked authority files to create a false blocker.

Report authority-file classification in the final execution report.

------------------------------------------------------------------------

## 5. Mandatory Starting Gate

Before any WP08 mutation or disposable proof verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#203: CLOSED / Done;
-   #204: OPEN / Backlog;
-   #205--#209: OPEN / Backlog;
-   milestone #55: OPEN, 6 open / 7 closed;
-   Project #2 planning state reconciled;
-   schema: v3;
-   canonical permanent baseline: 250 tests;
-   WP07 permanent discovery implementation exists;
-   no premature WP08+ implementation exists.

Reconcile expected untracked planning/documentation/authority files
against the Release 1.7 manifest and completed WPs.

Unexpected tracked/staged state blocks execution.

------------------------------------------------------------------------

## 6. Failure Vocabulary

Reuse exactly the accepted Release 1.6 five-value vocabulary:

-   `InvalidRequest`;
-   `NotFound`;
-   `DependencyUnavailable`;
-   `InvalidEvidence`;
-   `IntegrityConflict`.

Do not add:

-   `DiscoveryFailed`;
-   `QueryFailed`;
-   `EmptyResult`;
-   `PartialResult`;
-   `StorageError`;
-   or any other discovery-specific failure.

WP08 must map discovery conditions into the existing vocabulary only
where the architecture already authorizes a bounded mapping.

Unknown defects must remain unknown defects and propagate.

------------------------------------------------------------------------

## 7. Success Is Not Failure

Freeze these successful conditions:

### S1 --- Zero Matches

A valid discovery request with no matching durable Experiment Result
rows is:

-   successful;
-   an empty collection;
-   never `NotFound`.

### S2 --- One Match

One valid matching row is successful exact evidence.

### S3 --- Multiple Matches

Multiple valid matching rows up to the requested maximum are successful
exact evidence ordered by Experiment Result Identity ascending.

### S4 --- Maximum Truncation

More valid matches than the requested positive maximum is normal bounded
success.

Do not reinterpret bounded truncation as partial failure.

------------------------------------------------------------------------

## 8. InvalidRequest Boundary

Application WP05 already owns request validation:

-   null request;
-   non-positive maximum.

These return `InvalidRequest` without store invocation.

WP08 must verify that Infrastructure does not create a conflicting
second request policy.

Do not:

-   add a different numeric ceiling;
-   clamp values;
-   substitute defaults;
-   turn invalid values into unbounded queries.

If valid Application input reaches Infrastructure, Infrastructure should
execute the accepted contract.

------------------------------------------------------------------------

## 9. NotFound Boundary

Discovery itself does not use `NotFound` for zero query matches.

Determine from accepted predecessor semantics whether any nested
prerequisite lookup in the actual discovery implementation can
legitimately produce `NotFound`.

Because WP07 reads `experiment_results` directly, do not invent a
prerequisite lookup solely to create a `NotFound` case.

If no legitimate discovery-path `NotFound` condition exists, record that
fact explicitly.

Do not manufacture `NotFound` for an empty collection.

------------------------------------------------------------------------

## 10. DependencyUnavailable Boundary

Verify the existing SQLite unavailable-storage classification applies
unchanged to discovery.

Representative safe conditions may include repository-native unavailable
database path/open behavior already used by predecessor tests.

Requirements:

-   bounded classification only where existing infrastructure policy
    already recognizes dependency unavailability;
-   no retry;
-   no fallback;
-   no database creation/repair to recover;
-   no provider access;
-   no result identity/evidence emission on failure.

Do not broaden exception matching merely to make the proof pass.

------------------------------------------------------------------------

## 11. InvalidEvidence Boundary

Discovery reconstructs persisted immutable evidence using the existing
19-column mapper.

If selected persisted state cannot be reconstructed according to
accepted Release 1.6 evidence invariants, preserve the existing
`InvalidEvidence` semantics where applicable.

Do not:

-   silently skip malformed rows;
-   substitute missing fields;
-   normalize contradictory provenance into valid evidence;
-   return a successful partial collection;
-   repair persisted state.

Prefer existing repository-native evidence for this boundary.

Do not corrupt durable state unless the execution plan/file manifest
explicitly authorizes a safe disposable proof mechanism.

If safe construction is not authorized, reuse accepted
lower-layer/permanent predecessor evidence and report why direct
construction is not applicable.

------------------------------------------------------------------------

## 12. IntegrityConflict Boundary

Preserve Release 1.6 contradiction semantics.

Discovery is read-only and must never turn contradiction into:

-   overwrite;
-   delete;
-   repair;
-   reacceptance;
-   `EquivalentExisting`;
-   empty success.

However, do not invent a new discovery-time integrity scan if the
current read path has no legitimate `IntegrityConflict` trigger.

Determine whether `IntegrityConflict` is:

1.  directly reachable during discovery through existing
    validation/classification; or
2.  preserved as a lower-layer invariant whose contradictory state is
    prevented before discovery.

If direct safe construction would require unauthorized corruption, mark
process/store-level construction **NOT APPLICABLE** and reuse accepted
predecessor evidence.

Do not corrupt the database merely to prove the vocabulary member.

------------------------------------------------------------------------

## 13. Unknown Defects

Unknown defects must propagate.

Do not add:

-   catch-all conversion to `DependencyUnavailable`;
-   catch-all conversion to `InvalidEvidence`;
-   generic failure result;
-   retry;
-   fallback;
-   logging-and-success;
-   empty-success substitution.

Where a removable test double/probe can safely prove unknown-defect
propagation at the Application boundary without permanent test ownership
conflict, it may be used and must leave zero residue.

Otherwise rely on existing WP05 proof and current code inspection.

------------------------------------------------------------------------

## 14. First-Failure and Collection Semantics

For discovery over multiple selected rows, establish the accepted
behavior if reconstruction fails.

The implementation must not return evidence known to violate immutable
fidelity.

Unless authoritative predecessor semantics explicitly define another
behavior, preserve the existing bounded failure behavior and do not
invent partial success.

Validate:

-   no successful collection is returned after a classified
    reconstruction failure;
-   no later repair/fallback occurs;
-   no mutation occurs;
-   no invalid row is silently omitted.

If the repository authorities are genuinely ambiguous about
collection-level failure, stop rather than inventing a new policy.

------------------------------------------------------------------------

## 15. No-Fallback Rule

On any classified discovery failure:

-   do not invoke Release 1.6 exact-identity retrieval as fallback;
-   do not invoke Experiment execution;
-   do not invoke Feature execution;
-   do not invoke pipeline execution;
-   do not invoke provider acquisition;
-   do not return cached/synthetic evidence;
-   do not reinterpret failure as empty discovery.

WP08 is primarily Infrastructure/Application semantics; do not modify
Worker routing to prove this unless the execution plan explicitly
assigns process-level validation here.

------------------------------------------------------------------------

## 16. No-Retry Rule

Discovery is a one-shot bounded read.

Do not introduce:

-   retry loops;
-   resilience policies;
-   delayed retry;
-   reopen-and-retry;
-   alternate database paths.

Existing general repository infrastructure must not be expanded for
WP08.

If the current path already has an inherited retry behavior that
conflicts with the Release 1.7 definition, stop and report it.

------------------------------------------------------------------------

## 17. No-Mutation Rule

Failure handling must remain read-only.

Before/after representative failure proofs, verify where practical:

-   Experiment Result row count unchanged;
-   persisted identities unchanged;
-   no tables/indexes created;
-   no rows deleted/updated;
-   schema version unchanged.

Failure must never trigger repair.

------------------------------------------------------------------------

## 18. Process-Level Validation Prerequisite

If WP08 requires process/store-level synthetic durable state, first
identify the repository-native mechanism as required by the engineering
playbook.

Use only:

-   `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable Experiment acceptance;
-   existing test-host/friend-assembly boundary;
-   removable probes where permanent tests are not yet owned by WP08.

Do not invent an external temporary project/seeding mechanism before
checking repository-native fixtures.

If a failure case requires unauthorized corruption, do not construct it.

Reuse accepted predecessor evidence instead.

------------------------------------------------------------------------

## 19. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only create/modify paths explicitly assigned to WP08.

WP08 may be documentation/validation-only if the current implementation
already exactly preserves the frozen semantics.

Do not make production changes merely to create a WP08 delta.

If a genuine semantic defect exists and its correction is
manifest-authorized, make only the smallest correction.

For every permanent changed path report:

-   new or modified;
-   why WP08 owns it;
-   exact semantic defect corrected;
-   why the change does not consume WP09+ authority.

If correction requires a path outside the manifest, stop.

------------------------------------------------------------------------

## 20. Permanent Test Ownership

Respect the Release 1.7 file manifest and execution plan.

WP07 reported that later work retains permanent discovery-test
ownership.

Do not consume permanent tests assigned to WP11 or another later WP.

Expected permanent-test delta for WP08:

`0`

unless the authoritative manifest explicitly assigns failure-semantics
tests to WP08.

Use removable deterministic probes for validation when needed.

Architecture.Tests delta:

`0`

Do not weaken architecture rules.

------------------------------------------------------------------------

## 21. Required Failure Matrix

Prove or reconcile each row without inventing unreachable states:

### F1 --- Null Request

Expected: `InvalidRequest`; store calls 0.

### F2 --- Non-Positive Maximum

Expected: `InvalidRequest`; store calls 0.

### F3 --- Valid Zero Match

Expected: successful empty collection; never `NotFound`.

### F4 --- Dependency Unavailable

Expected: existing bounded `DependencyUnavailable`; no
retry/fallback/mutation.

### F5 --- Invalid Persisted Evidence

Expected: existing `InvalidEvidence` behavior if
safely/repository-natively constructible; otherwise accepted predecessor
evidence reconciliation with direct construction marked not applicable.

### F6 --- Integrity Conflict

Expected: preserve accepted contradiction semantics if legitimately
reachable; otherwise reconcile accepted lower-layer evidence and
explicitly state why direct discovery construction is not applicable.

### F7 --- Unknown Defect

Expected: propagates; not normalized.

### F8 --- Failure During Multi-Row Reconstruction

Expected: no invented partial-success collection, no skipped invalid
row, no repair/fallback; reconcile against existing mapper/control flow.

### F9 --- Read-Only Failure State

Expected: durable state unchanged after classified failures.

### F10 --- No Provider/Network Fallback

Expected: provider calls 0; network product activity 0; real credentials
0.

------------------------------------------------------------------------

## 22. Failure-Reachability Report

For each accepted vocabulary member, classify it as:

-   **directly reachable in discovery**;
-   **Application-prevalidated**;
-   **preserved lower-layer invariant**;
-   **not applicable to valid zero-match discovery**.

This report is important because the five-value vocabulary is shared,
but not every value must be artificially reachable from every operation.

Do not add code solely to make all five values directly reachable.

------------------------------------------------------------------------

## 23. Targeted Validation

Use the smallest safe validation boundary.

Targeted validation must establish:

-   F1--F10 reconciled;
-   no retry;
-   no fallback;
-   no mutation;
-   no partial-success invention;
-   unknown defects propagate;
-   zero-match success remains intact;
-   zero provider/network/credential activity;
-   zero disposable residue.

Remove all temporary probes/scripts/databases after proof.

------------------------------------------------------------------------

## 24. Canonical Validation

After any manifest-authorized mutation and targeted proof, run canonical
verification.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   permanent total: 250/250 unless explicitly authorized otherwise;
-   skipped: 0;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   conflict markers: 0;
-   provider/network product activity: 0;
-   real credentials: 0;
-   temporary database/process/probe residue: 0.

Report exact permanent-test delta from 250.

------------------------------------------------------------------------

## 25. Schema and Dependency Preservation

Successful WP08 completion requires:

-   schema remains v3;
-   table delta: 0;
-   column delta: 0;
-   index delta: 0;
-   migration delta: 0;
-   package delta: 0;
-   project delta: 0;
-   project-reference delta: 0;
-   production dependency graph remains acyclic;
-   Application → Infrastructure dependency remains absent;
-   discovery successful behavior unchanged;
-   Worker behavior unchanged;
-   DI state unchanged unless explicitly assigned to a later WP;
-   Release 1.1--1.6 predecessor behavior preserved.

------------------------------------------------------------------------

## 26. Cleanup

Remove all disposable WP08 artifacts:

-   temporary databases;
-   WAL/SHM files;
-   temporary probes;
-   temporary projects/scripts;
-   logs;
-   retained processes/handles.

Final residue:

`0`

Do not remove governed permanent Release 1.7 artifacts.

------------------------------------------------------------------------

## 27. GitHub Lifecycle

Only after every WP08 acceptance gate passes:

1.  move #204 Backlog → In Progress if necessary;
2.  post concise completion evidence to #204, including the F1--F10
    matrix and reachability classification;
3.  close #204;
4.  set #204 Project Status to Done.

Final required state:

-   #197--#204: CLOSED / Done;
-   #205--#209: OPEN / Backlog;
-   milestone #55: OPEN, 5 open / 8 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #205 automatically.

------------------------------------------------------------------------

## 28. Mutation Budget

### Application production

`0` unless an actual manifest-authorized semantic defect is proven.

### Infrastructure production

`0` unless an actual manifest-authorized semantic defect is proven.

### Worker/DI

`0`

### Schema/table/column/index/migration

`0`

### Packages/projects/references

`0`

### Permanent tests

`0` by default; respect later test ownership.

### Architecture rules

`0`

### Disposable validation

Authorized; final residue 0.

### Git transport

`0`

### GitHub

Only #204 lifecycle mutations.

------------------------------------------------------------------------

## 29. Stop Conditions

Stop with:

`RELEASE 1.7 WP08 BLOCKED`

if:

-   starting state differs materially;
-   frozen WP02--WP07 semantics conflict;
-   a new failure vocabulary value is required;
-   valid zero-match discovery cannot remain successful;
-   existing code silently skips invalid evidence;
-   existing code returns partial success after reconstruction failure
    contrary to frozen semantics;
-   existing code retries, repairs, or falls back contrary to authority;
-   unknown defects are broadly normalized;
-   failure correction requires schema/index/migration changes;
-   Application contract redesign is required;
-   Worker/DI changes are required;
-   package/project/reference changes are required;
-   architecture rules would need weakening;
-   required permanent paths lie outside the manifest;
-   safe validation requires unauthorized durable corruption;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 30. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  existing failure-classification mechanisms inspected;
4.  five-value vocabulary preservation;
5.  F1--F10 matrix;
6.  failure-reachability classification for all five vocabulary values;
7.  zero-match success proof;
8.  `InvalidRequest` boundary;
9.  `NotFound` applicability;
10. `DependencyUnavailable` proof;
11. `InvalidEvidence` proof/reconciliation;
12. `IntegrityConflict` proof/reconciliation;
13. unknown-defect propagation;
14. multi-row failure/partial-success behavior;
15. no-retry proof;
16. no-fallback proof;
17. read-only failure-state proof;
18. process-level fixture mechanism if used;
19. changed permanent paths;
20. permanent-test delta;
21. schema/index/package/project/reference deltas;
22. targeted validation;
23. canonical validation;
24. offline/security/residue evidence;
25. GitHub lifecycle;
26. final milestone counts;
27. next authorized action.

------------------------------------------------------------------------

## 31. Completion Markers

On success end exactly:

`RELEASE 1.7 WP08 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP09 — Durable Evidence Discovery Dependency Injection — GitHub issue #205`

Do not execute WP09 automatically.

If blocked end exactly:

`RELEASE 1.7 WP08 BLOCKED`

and identify the smallest corrective authority required.

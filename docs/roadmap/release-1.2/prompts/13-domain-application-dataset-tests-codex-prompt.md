# Release 1.2 WP13 --- Domain & Application Dataset Tests

## Codex Execution Authority

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`\
**Release:** 1.2 --- Research Dataset Foundation\
**Work package:** WP13 --- Domain & Application Dataset Tests\
**GitHub issue:** #133\
**Recommended model:** GPT-5.6 Luna

------------------------------------------------------------------------

## 1. Mission

Execute WP13 only.

Add the minimum permanent, deterministic, offline Domain/Application
test coverage required by the accepted Release 1.2 dataset semantics and
Application implementation through WP12.

WP13 is a **test-coverage work package**, not a production-design work
package.

Do not modify production code merely to make testing easier. If accepted
production behavior cannot be tested without a production redesign, stop
and report the smallest corrective authority required.

Infrastructure/SQLite-specific proof belongs to WP14.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Before mutation, read completely and reconcile:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  `13-domain-application-dataset-tests-codex-prompt.md`
4.  `13-domain-application-dataset-tests-codex-prompt-chat.md`
5.  Accepted WP02 Research Dataset Definition & Reproducibility Model.
6.  Accepted WP03 Dataset Identity, Version & Provenance Semantics.
7.  Accepted WP04 Application Dataset Contracts.
8.  Accepted WP05 Dataset Materialization Use Case.
9.  Accepted WP06 Dataset Metadata & Catalog Model.
10. Accepted WP07 physical-storage result only as context; do not test
    its SQLite mechanics here.
11. Accepted WP08 snapshot persistence result only as context.
12. Accepted WP09 catalog persistence result only as context.
13. Accepted WP10 Dataset Materialization Integration.
14. Accepted WP11 Dataset Validation & Failure Mapping.
15. Accepted WP12 Dependency Registration & Bounded Dataset Execution.
16. Current Domain and Application production source.
17. Existing Domain.Tests and Application.Tests.
18. GitHub issue #133, milestone #53, Project #2, and predecessor state.

Repository truth plus these authorities define the test target. Do not
invent new semantics.

------------------------------------------------------------------------

## 3. Starting-State Gates

Before editing:

-   verify repository, branch, HEAD, and `origin/main`;
-   report ahead/behind;
-   verify staged paths = 0;
-   classify every tracked/untracked path;
-   unexpected/ambiguous paths = 0;
-   verify Release 1.1 remains closed;
-   verify milestone #53 remains open;
-   verify WP12/#132 = Closed/Done;
-   verify WP13/#133 = Open/Backlog;
-   verify WP14/#134 = Open/Backlog;
-   verify WP13 dependencies exactly match authoritative planning;
-   verify WP14 has not started;
-   verify Release 1.3 has not started.

Run the unchanged baseline:

-   restore;
-   format verification;
-   build;
-   all permanent tests;
-   architecture tests;
-   canonical `eng/verify.ps1` using the accepted configuration;
-   `git diff --check`;
-   `git diff --cached --check`.

Record exact existing suite counts.

Only after baseline success may #133 move Backlog → In Progress.

------------------------------------------------------------------------

## 4. Test Inventory Before Mutation

Inventory existing permanent Domain and Application tests first.

Build a coverage matrix mapping existing tests against Release 1.2
requirements.

Do not add duplicate tests simply to increase counts.

For each required behavior classify:

-   already permanently covered;
-   missing and WP13-owned;
-   Infrastructure/WP14-owned;
-   architecture/documentation-owned;
-   not applicable.

The final report must show why every new permanent test is necessary.

------------------------------------------------------------------------

## 5. Domain Test Policy

Do not assume WP13 requires a Domain-test delta.

The dataset feature is intentionally Application-owned and must not
force dataset concepts into Domain merely to create Domain tests.

Inspect existing Domain tests and determine whether Release 1.2
introduced any new Domain-owned invariant.

If no new Domain-owned production behavior exists:

-   add **zero** Domain tests;
-   explicitly report that Domain coverage is already sufficient;
-   do not create artificial dataset Domain types or tests.

If the manifest explicitly identifies a Domain test file or genuinely
uncovered Domain-owned invariant, add only that authorized coverage.

------------------------------------------------------------------------

## 6. Application Contract Coverage

Add permanent pure Application tests for the accepted dataset contracts
where coverage is missing.

Cover applicable invariants such as:

### Dataset Definition

-   exact target preservation;
-   valid `[from, to)` boundaries;
-   invalid/equal/reversed boundaries rejected;
-   target is not trimmed, case-folded, or normalized;
-   semantic ordering requirement remains explicit.

### Typed identities

Prove the accepted typed identity model remains distinct:

-   Dataset Definition Identity;
-   Research Dataset Identity;
-   Source State Identity;
-   Dataset Snapshot Identity;
-   Dataset Version relationship to Snapshot Identity.

Validate accepted fingerprint rules where Application owns them:

-   `aiq-dataset-identity-v1`;
-   64 lowercase hexadecimal fingerprint;
-   malformed values rejected.

Do not retest SHA-256 internals redundantly if materialization tests
prove deterministic output more appropriately.

### Snapshot candidate / coverage / provenance / lineage

Cover applicable invariants:

-   exact definition/identity consistency;
-   successful empty candidate;
-   non-empty coverage;
-   observation count consistency;
-   first/last actual instant rules;
-   ascending semantic-instant ordering;
-   duplicate semantic instant rejection;
-   exact target consistency;
-   version/snapshot relationship;
-   provenance identity consistency;
-   lineage consistency;
-   original `DateTimeOffset` representation preserved;
-   exact decimal values preserved.

### Catalog entry

Cover:

-   construction from accepted candidate;
-   identity/version preservation;
-   target/boundary/coverage preservation;
-   empty snapshot metadata;
-   provenance/lineage preservation;
-   absence of mutable "latest" semantics.

Use behavior assertions, not implementation-detail assertions.

------------------------------------------------------------------------

## 7. WP05 Materialization Use-Case Coverage

Add deterministic Application tests for `IMaterializeDatasetUseCase` /
its implementation.

Use an Application-owned hand-written fake/stub of
`IHistoricalObservationStore`.

Do not use SQLite.

Required scenarios where not already permanently covered:

1.  exact target forwarded unchanged to source history;
2.  `[from,to)` selection:
    -   include `from`;
    -   exclude `to`;
    -   exclude outside observations;
3.  deterministic ascending ordering;
4.  valid zero-observation materialization;
5.  original timestamp offset preserved;
6.  exact high-precision decimal preserved;
7.  deterministic equivalent re-materialization;
8.  identity output stable across culture/local timezone influences;
9.  relevant selected source changes alter the appropriate
    source/snapshot identity;
10. outside-window source changes do not alter selected materialization
    identity;
11. definition changes alter definition/logical/snapshot identity as
    required by accepted WP03 semantics;
12. source `Unavailable` maps to `SourceHistoryUnavailable`;
13. source `InvalidData` maps to `IntegrityConflict`;
14. duplicate semantic instants fail according to accepted behavior;
15. coverage/provenance/lineage are constructed consistently.

Do not access provider/network/database/filesystem.

------------------------------------------------------------------------

## 8. Identity Determinism Coverage

WP05 implemented `aiq-dataset-identity-v1`. WP13 should permanently
prove the semantic contract without overfitting private encoding
details.

At minimum prove:

-   same definition + same relevant source state → same four
    identities/version;
-   culture changes do not alter identity;
-   original offset representation participates where accepted;
-   exact decimal value participates;
-   selected membership participates;
-   boundary/definition changes participate;
-   unrelated/outside-window observations do not participate in the
    selected source-state identity;
-   empty materialization has stable deterministic identities.

Do not duplicate every byte-level canonicalization rule unless the
public/accessible Application behavior requires it.

------------------------------------------------------------------------

## 9. WP06 Catalog-Model Coverage

Pure Application tests should prove `DatasetCatalogEntry` represents
accepted immutable evidence.

Do not instantiate Infrastructure catalog implementations.

Cover applicable semantics:

-   exact snapshot identity/version;
-   definition/research/source-state identities;
-   exact target and boundaries;
-   selected count;
-   empty/non-empty actual coverage;
-   provenance and lineage;
-   no mutable status or "latest" pointer.

Registration persistence and exact SQLite lookup belong to WP14.

------------------------------------------------------------------------

## 10. WP10 Integration Coverage

Add permanent Application tests for the accepted WP10 orchestration
using hand-written Application-level stubs/fakes for:

-   materialization use case;
-   `IDatasetSnapshotStore`;
-   `IDatasetCatalog`.

Do not use the concrete SQLite implementations.

Cover the accepted matrix:

  -----------------------------------------------------------------------
  Materialization / Snapshot /        Expected
  Catalog
  ----------------------------------- -----------------------------------
  materialization failure             stop; snapshot/catalog not called

  snapshot newly accepted + catalog   integrated newly accepted
  newly registered

  snapshot newly accepted + catalog   integrated newly accepted
  equivalent

  snapshot equivalent + catalog newly equivalent existing
  registered/equivalent

  snapshot integrity conflict         stop; catalog not called; integrity
                                      conflict

  catalog integrity conflict          integrity conflict

  snapshot unavailable                stop; catalog not called;
                                      snapshot-store unavailable

  snapshot invalid data               accepted WP11 integration mapping

  catalog unavailable                 accepted unavailable mapping

  catalog invalid data                accepted WP11 integration mapping
  -----------------------------------------------------------------------

Use the exact current contracts as authority if names differ.

Also prove that the catalog entry passed to the catalog is derived from
the exact materialized candidate without semantic mutation.

------------------------------------------------------------------------

## 11. WP11 Failure-Semantics Coverage

Permanent Application tests must preserve distinctions
introduced/confirmed by WP11.

Where represented at the Application boundary, cover:

-   `Unavailable`;
-   `InvalidData`;
-   `IntegrityConflict`;
-   `NotFound`;
-   `EquivalentExisting`.

Do not collapse these categories.

Do not add SQLite exception tests here.

SQLite error-code mapping and malformed persisted-row/schema behavior
are WP14-owned.

------------------------------------------------------------------------

## 12. WP12 Boundary

WP13 must not test the real Worker process, concrete Microsoft DI
container, configuration binding, or SQLite-backed execution unless the
manifest explicitly assigns such coverage here.

WP12 already supplied execution evidence.

WP14 owns Infrastructure composition/persistence tests.

WP13 should remain fast, deterministic, pure, and offline.

Do not add:

-   provider calls;
-   HTTP;
-   SQLite;
-   temporary databases;
-   filesystem persistence;
-   environment-dependent process execution.

------------------------------------------------------------------------

## 13. Test Doubles

Prefer small hand-written test doubles local to the test project/file.

Do not add mocking packages.

Test doubles must expose only what is necessary to prove:

-   exact arguments;
-   call counts/order where semantically relevant;
-   configured outcomes/failures.

Avoid building reusable testing frameworks.

Package/reference delta should remain `0/0`.

------------------------------------------------------------------------

## 14. Production-Code Protection

Expected production delta for WP13: **0**.

Do not modify:

-   Domain production;
-   Application production;
-   Infrastructure production;
-   Worker production;
-   schema/bootstrap/mappers;
-   DI/configuration;
-   engineering scripts.

If a test exposes a genuine production defect, stop and report it rather
than silently fixing it under WP13.

------------------------------------------------------------------------

## 15. WP14 Protection

Do not pre-empt WP14 --- Infrastructure & Dataset Tests.

Reserved for WP14 include:

-   schema v2 structure and v1→v2 upgrade;
-   bootstrap validation;
-   SQLite mapper round trips;
-   connection lifecycle;
-   snapshot durable persistence;
-   empty snapshot physical representation;
-   equivalent persistence;
-   integrity conflicts;
-   transaction rollback;
-   immutable evidence;
-   catalog registration/exact lookup;
-   multiple versions;
-   SQLite failure-code mapping;
-   malformed persisted evidence;
-   Infrastructure DI/configuration;
-   database cleanup.

WP13 may test the Application abstraction/result semantics for these
behaviors, but not the SQLite mechanics.

------------------------------------------------------------------------

## 16. Release 1.3 Protection

Do not introduce or test speculative:

-   pipelines;
-   scheduling;
-   refresh loops;
-   streaming;
-   polling;
-   retries;
-   DAGs;
-   background materialization;
-   multi-dataset orchestration;
-   monitoring/checkpointing.

Release 1.3 implementation started must remain `NO`.

------------------------------------------------------------------------

## 17. Expected Files

Use `RELEASE_1.2_FILE_MANIFEST.md` as the exact path authority.

Prefer the minimum number of focused Application test files.

Possible logical groupings, only if manifest-authorized, include:

-   dataset contract/model tests;
-   materialization use-case tests;
-   materialization-integration tests.

Do not create paths solely because they are suggested here if the
manifest specifies different names.

Any necessary path outside the WP13 manifest scope is a stop condition.

------------------------------------------------------------------------

## 18. Validation

After tests are added:

1.  run targeted Domain tests;
2.  run targeted Application tests;
3.  restore;
4.  format verification;
5.  build with 0 warnings/errors;
6.  run all permanent test projects;
7.  run Architecture.Tests;
8.  run canonical `eng/verify.ps1`;
9.  confirm Gitleaks passes;
10. run `git diff --check`;
11. run `git diff --cached --check`;
12. directly whitespace-check untracked WP13 files if necessary;
13. verify provider/network calls = 0;
14. verify SQLite/database use by WP13 tests = 0;
15. verify temporary residue = 0;
16. verify production dependency graph unchanged.

Report before/after/delta test counts for every suite.

------------------------------------------------------------------------

## 19. Acceptance Matrix

WP13 completes only when all applicable requirements pass:

  Requirement                             Expected
  --------------------------------------- ----------
  WP12 predecessor                        PASS
  Existing test inventory                 COMPLETE
  Duplicate test additions                0
  Domain production delta                 0
  Application production delta            0
  Infrastructure production delta         0
  Worker production delta                 0
  Dataset Definition coverage             PASS
  Typed identity/version coverage         PASS
  Snapshot candidate invariants           PASS
  Coverage/provenance/lineage             PASS
  Empty materialization                   PASS
  Ordering/duplicate semantics            PASS
  Timestamp/offset fidelity               PASS
  Decimal fidelity                        PASS
  Deterministic identity                  PASS
  Relevant-change distinguishability      PASS
  Catalog model coverage                  PASS
  WP05 materialization coverage           PASS
  WP10 integration matrix                 PASS
  WP11 Application failure distinctions   PASS
  SQLite/database use in WP13 tests       0
  Provider/network calls                  0
  Mocking package delta                   0
  Package/reference delta                 0/0
  WP14 started                            NO
  Release 1.3 started                     NO

------------------------------------------------------------------------

## 20. GitHub Lifecycle

After starting gates pass:

`#133 Backlog → In Progress`

After **all** acceptance gates pass:

-   post concise completion evidence to #133;
-   close #133;
-   set Project #2 status to Done.

Verify:

-   #133 = Closed/Done;
-   #134 = Open/Backlog;
-   milestone #53 = Open.

Do not otherwise mutate #134.

------------------------------------------------------------------------

## 21. Git Protection

Do not:

-   stage;
-   commit;
-   push;
-   create/switch implementation branches;
-   create PR;
-   merge;
-   tag;
-   create release;
-   rewrite history.

The cumulative Release 1.2 working tree remains uncommitted until the
later integration authority.

------------------------------------------------------------------------

## 22. Stop Conditions

Stop and report the smallest corrective authority if:

-   baseline fails for a repository reason;
-   predecessor/planning state is invalid;
-   unexpected working-tree paths exist;
-   manifest conflicts with necessary test paths;
-   required behavior cannot be tested without production changes;
-   accepted WP02--WP12 semantics contradict one another;
-   a genuine production defect is discovered;
-   WP13 would require SQLite/Infrastructure mechanics;
-   package/reference changes become necessary;
-   completing tests would require WP14 or Release 1.3 scope.

Do not repair beyond authority.

------------------------------------------------------------------------

## 23. Required Execution Report

Produce a numbered report covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  Existing Domain Test Inventory
10. Existing Application Test Inventory
11. Existing Coverage Matrix
12. WP02 Reconciliation
13. WP03 Reconciliation
14. WP04 Contract Reconciliation
15. WP05 Materialization Reconciliation
16. WP06 Catalog Reconciliation
17. WP10 Integration Reconciliation
18. WP11 Failure Reconciliation
19. WP12 Boundary Reconciliation
20. Domain Test Design / Delta Decision
21. Application Contract Test Design
22. Dataset Definition Tests
23. Identity/Version Tests
24. Candidate/Coverage/Provenance/Lineage Tests
25. Materialization Use-Case Tests
26. Determinism/Relevant-Change Tests
27. Catalog Model Tests
28. Integration Outcome Matrix Tests
29. Failure Propagation Tests
30. Test-Double Design
31. Exact Files Added/Modified
32. Production Delta
33. Package/Reference Delta
34. Permanent Test Count Delta
35. Targeted Domain Evidence
36. Targeted Application Evidence
37. Full Permanent Test Evidence
38. Canonical Verification
39. Architecture Validation
40. Security/Offline Determinism
41. SQLite/Database Use
42. Whitespace/Diff Evidence
43. Mutation Accounting
44. Git/GitHub Protection
45. Planning Protection
46. Findings/Blockers
47. Acceptance Matrix
48. Final Repository/GitHub State
49. WP14 Handoff
50. Final Decision
51. Next Authorized Work Package

End only after every gate passes with:

`RELEASE 1.2 WP13 COMPLETE`

and:

`NEXT AUTHORIZED WORK PACKAGE: WP14 — Infrastructure & Dataset Tests — GitHub issue #134`

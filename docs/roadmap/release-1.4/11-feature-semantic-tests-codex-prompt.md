# Release 1.4 --- WP11 Feature Semantic Tests --- Codex Authority

## Mission

Execute **Release 1.4 --- WP11: Feature Semantic Tests --- GitHub issue
#163**.

WP11 adds permanent deterministic offline Domain/Application coverage
for the Release 1.4 feature semantics already implemented and accepted
through WP10.

Recommended model: **GPT-5.6 Luna**.

This is a test-expansion work package. Do not change production behavior
to make tests easier.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Before any mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
6.  WP04 --- Feature Domain/Application Model authority/result
7.  WP05 --- Feature Generation Contracts authority/result
8.  WP06 --- Deterministic Feature Computation authority/result
9.  WP06 identity clarification authority/result
10. WP07 --- Feature Validation & Failure Mapping authority/result
11. WP08 --- Feature Generation Integration authority/result
12. WP09 --- Dependency Registration & Configuration authority/result
13. WP10 --- One-Shot Worker Feature Execution authority/result
14. Current Domain/Application feature implementation
15. Existing Domain/Application test conventions
16. Current architecture rules
17. Current GitHub state for #163 and successor #164

Repository truth and accepted semantics govern. Do not invent new
product behavior through tests.

------------------------------------------------------------------------

## 2. Starting-State Gates

Before adding tests verify and report:

-   branch `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   cumulative Release 1.4 paths are expected and classified;
-   #153--#162 are Closed/Done;
-   #163 is OPEN / Backlog;
-   #164 is OPEN / Backlog and untouched;
-   milestone #45 is OPEN;
-   no Release 1.5 implementation exists;
-   SQLite schema remains exactly v2;
-   production dependency graph remains unchanged and acyclic.

Run canonical `eng/verify.ps1 -Configuration Release` before mutation.

Expected pre-WP11 baseline:

-   Domain.Tests: `11`
-   Application.Tests: `77`
-   Infrastructure.Tests: `96`
-   Architecture.Tests: `13`
-   Permanent total: `197`

Repository truth is authoritative if an accepted predecessor delta
exists.

Only after all gates pass may #163 move Backlog → In Progress.

------------------------------------------------------------------------

## 3. WP11 Objective

Add permanent **Domain/Application semantic tests only** for Release
1.4.

The tests must prove the accepted feature semantics and Application
behavior without using:

-   SQLite;
-   Worker processes;
-   DI/service-provider composition;
-   provider/network access;
-   real credentials;
-   filesystem persistence;
-   timing-dependent assertions.

WP12 owns composition and Worker validation.

Production-code delta should be zero.

------------------------------------------------------------------------

## 4. Domain Test Decision

Apply zero-delta-first reasoning.

WP04 introduced no Domain production change, so Domain test delta is
expected to remain `0`.

Do not add Domain tests merely to satisfy the work-package title.

If repository truth shows no new Domain invariant, explicitly report:

`Domain test delta: 0`

Any Domain test addition requires a clearly identified accepted Domain
invariant.

------------------------------------------------------------------------

## 5. Application Coverage Inventory

Before writing tests, inspect current Application tests and classify
which Release 1.4 semantics are already covered versus missing.

Do not duplicate existing tests simply to increase count.

Produce a focused coverage matrix spanning WP02--WP08 behavior.

The permanent suite should emphasize stable semantic contracts rather
than implementation internals.

------------------------------------------------------------------------

## 6. Required Feature Definition / Identity Coverage

Add deterministic coverage for applicable cases including:

-   accepted `simple-return-lag-1-v1` definition;
-   typed/non-interchangeable `FeatureDefinitionIdentity`;
-   typed/non-interchangeable `FeatureSetIdentity`;
-   malformed fingerprint rejection;
-   `aiq-feature-identity-v1`;
-   64 lowercase hexadecimal fingerprint representation;
-   deterministic Feature Definition Identity;
-   deterministic Feature Set Identity;
-   equivalent semantic recomputation → same Feature Set Identity;
-   same numeric output from different snapshots → different Feature Set
    Identity;
-   empty FeatureSet retains deterministic snapshot-bound identity.

Do not test private hashing implementation details that are not semantic
authority.

------------------------------------------------------------------------

## 7. Required Formula Coverage

Prove the exact formula:

`r[i] = (p[i] / p[i-1]) - 1`

using decimal semantics.

Cover representative exact-decimal cases without convenience rounding.

Tests must fail if the implementation changes to:

-   percentage × 100;
-   log returns;
-   configurable lag;
-   floating-point arithmetic;
-   rounded/display values.

Use expected decimal values chosen so the assertions remain
deterministic and exact.

------------------------------------------------------------------------

## 8. Ordering / Cardinality Coverage

Prove:

-   input order is semantic;
-   output follows adjacent accepted snapshot order;
-   no sorting/deduplication occurs in the feature computer;
-   empty input → 0 feature values;
-   single observation → 0 feature values;
-   valid `N >= 2` input → exactly `N-1` ordered values.

Use intentionally distinctive observation values/timestamps so
accidental reordering is observable.

------------------------------------------------------------------------

## 9. Timestamp / Offset Fidelity Coverage

Prove each feature value belongs to the current observation `i`.

Assert:

-   timestamp comes from current observation;
-   original offset is preserved exactly;
-   predecessor timestamp is not used;
-   no local-time or UTC-offset normalization destroys accepted
    evidence.

Use at least one non-zero offset scenario.

Do not rely on the machine timezone.

------------------------------------------------------------------------

## 10. Decimal Canonicalization Coverage

Where accessible through accepted public/Application behavior, prove:

-   equivalent decimal values with redundant trailing zeros preserve
    semantic identity;
-   culture does not alter identity/computation;
-   no binary floating-point conversion changes semantic evidence.

Do not write tests against non-public implementation methods solely for
coverage.

------------------------------------------------------------------------

## 11. Empty / Single-Observation Semantics

Permanent tests must prove:

### Existing empty snapshot

-   successful feature result;
-   count `0`;
-   deterministic snapshot-bound Feature Set Identity.

### Existing single-observation snapshot

-   successful feature result;
-   count `0`;
-   deterministic snapshot-bound Feature Set Identity.

These are not:

-   NotFound;
-   invalid request;
-   invalid evidence;
-   invalid numeric input.

------------------------------------------------------------------------

## 12. Validation / Failure Coverage

Cover stable Application-owned cases introduced by WP07/WP08.

At minimum, where constructible through accepted public seams:

-   invalid request;
-   exact snapshot/version mismatch / invalid snapshot evidence;
-   invalid numeric evidence;
-   snapshot NotFound;
-   dependency unavailable;
-   unknown lookup exception propagation;
-   unknown computation exception propagation;
-   no partial successful FeatureSet after invalid numeric failure;
-   first failure stops downstream computation;
-   no fabricated FeatureSet identity after failure.

Unsupported-definition and integrity-contradiction cases that are
intentionally unconstructable through immutable accepted types should
not be forced through reflection or invalid internal mutation.

Instead, document the invariant that makes them unconstructable and test
the relevant constructor/type boundary if that is public and stable.

------------------------------------------------------------------------

## 13. Integration-Use-Case Coverage

Use hand-written deterministic test doubles for dependencies.

Cover `IFeatureGenerationUseCase` behavior without Infrastructure.

Useful cases include:

-   exact snapshot identity/version is forwarded to lookup;
-   valid snapshot invokes computer exactly once;
-   NotFound prevents computation;
-   unavailable dependency prevents computation;
-   invalid returned snapshot prevents computation;
-   successful result preserves exact snapshot identity/version;
-   equivalent repeated requests preserve Feature Set Identity;
-   unknown dependency/computation exceptions propagate.

Do not use SQLite.

Do not reference Infrastructure implementation types.

------------------------------------------------------------------------

## 14. Determinism / Culture / Timezone Coverage

Use scoped culture changes where appropriate to prove Application
semantic determinism.

At minimum, demonstrate a non-default culture does not alter:

-   formula result;
-   Feature Definition Identity;
-   Feature Set Identity;
-   failure classification.

Where offset behavior is tested, use explicit `DateTimeOffset`; do not
mutate machine timezone or require system configuration.

No test may depend on current wall-clock time.

------------------------------------------------------------------------

## 15. Provenance / Lineage Coverage

Where exposed by the accepted WP04 model, prove:

-   FeatureSet references exact source snapshot identity/version;
-   feature definition evidence is preserved;
-   lineage/provenance is immutable;
-   different snapshots remain lineage-distinct;
-   no operational invocation metadata participates.

Do not recreate a second provenance graph in tests.

------------------------------------------------------------------------

## 16. Immutability Coverage

Cover stable immutable-surface behavior where useful:

-   returned feature-value collection cannot be externally mutated;
-   source references remain coherent;
-   successful result cannot be transformed into failure through mutable
    state;
-   FeatureSet cardinality reflects its immutable values.

Avoid brittle reflection tests against implementation fields.

------------------------------------------------------------------------

## 17. Test Doubles

Use small hand-written local doubles/stubs/fakes consistent with
existing repository conventions.

Allowed examples:

-   in-memory snapshot store stub;
-   counting feature-computer stub;
-   throwing snapshot-store stub;
-   deterministic accepted snapshot builder helpers.

Do not add mocking packages.

Do not add generic shared test frameworks unless already present and
necessary.

Keep doubles scoped to the test file/project unless there is an
established reusable pattern.

------------------------------------------------------------------------

## 18. Infrastructure / Worker / DI Exclusion

WP11 must not:

-   reference SQLite implementation;
-   create database files;
-   launch Worker processes;
-   build a production service provider;
-   inspect `Program.cs`;
-   test environment variables;
-   test feature configuration parsing;
-   call provider/network code.

These belong to WP12 or are already covered by predecessor tests.

------------------------------------------------------------------------

## 19. Production-Code Protection

Expected production delta:

`0`

Do not modify Application production code to make a test pass unless the
test reveals a genuine contradiction with already-accepted Release 1.4
semantics.

If such a contradiction is found, stop with `RELEASE 1.4 WP11 BLOCKED`
and request the smallest corrective authority.

Do not "fix" production behavior inside a test work package.

------------------------------------------------------------------------

## 20. File-Manifest Discipline

Use `RELEASE_1.4_FILE_MANIFEST.md` as hard path authority.

Preferred new test file:

`tests/AIQuantTradingResearch.Application.Tests/FeatureApplicationTests.cs`

Use an alternate existing canonical test-file organization only if the
repository convention clearly requires it and the manifest/prompt
permits bounded reconciliation.

Expected Domain test files changed: `0`.

Do not modify Infrastructure/Architecture tests in WP11.

------------------------------------------------------------------------

## 21. Test Quality

All new tests must be:

-   deterministic;
-   offline;
-   isolated;
-   readable;
-   behavior-focused;
-   independent of execution order;
-   independent of machine locale/timezone;
-   free of sleeps/timeouts;
-   free of network;
-   free of shared mutable global state.

Use descriptive test names aligned with repository conventions.

Do not assert implementation details when semantic behavior is
sufficient.

------------------------------------------------------------------------

## 22. Test Count Accounting

Report exact:

-   Domain test count before/after;
-   Application test count before/after;
-   Infrastructure count unchanged;
-   Architecture count unchanged;
-   total permanent tests before/after.

Do not target an arbitrary count.

The goal is complete semantic coverage with minimal duplication.

------------------------------------------------------------------------

## 23. Required WP11 Coverage Matrix

At completion classify each applicable semantic row as permanently
covered:

1.  sole built-in feature definition;
2.  typed identity invariants;
3.  malformed fingerprint rejection;
4.  deterministic Feature Definition Identity;
5.  deterministic Feature Set Identity;
6.  different snapshot identity distinction;
7.  exact decimal formula;
8.  ordering;
9.  N→N-1 cardinality;
10. timestamp ownership;
11. offset fidelity;
12. empty input success;
13. single-observation success;
14. empty identity behavior;
15. equivalent recomputation identity;
16. culture independence;
17. snapshot/version provenance binding;
18. invalid request;
19. invalid snapshot evidence;
20. invalid numeric evidence;
21. NotFound;
22. dependency unavailable;
23. first-failure behavior;
24. no partial FeatureSet;
25. no fabricated identity after failure;
26. unknown lookup exception propagation;
27. unknown computation exception propagation;
28. Application integration exact lookup forwarding;
29. computation exactly once on success;
30. no Infrastructure/Worker/provider dependency in WP11 tests.

If a row is impossible by construction, state the invariant rather than
manufacturing invalid objects.

------------------------------------------------------------------------

## 24. Validation Requirements

After adding tests:

1.  run targeted Domain.Tests if relevant;
2.  run targeted Application.Tests;
3.  run full permanent suite;
4.  run `git diff --check`;
5.  run `git diff --cached --check`;
6.  directly inspect whitespace for new untracked test files;
7.  run canonical `eng/verify.ps1 -Configuration Release`;
8.  confirm:
    -   build warnings/errors `0/0`;
    -   all tests pass;
    -   Architecture.Tests still pass;
    -   Gitleaks PASS;
    -   production delta `0`;
    -   package/reference/schema delta `0/0/0`;
    -   database/generated residue `0`;
    -   provider/network activity `0`.

No temporary probe should remain.

------------------------------------------------------------------------

## 25. Regression Protection

Confirm existing suites remain intact for:

-   Release 1.1 historical persistence;
-   Release 1.2 dataset/snapshot/catalog;
-   Release 1.3 pipeline;
-   Release 1.4 WP02--WP10 production behavior.

Do not delete or weaken predecessor tests.

------------------------------------------------------------------------

## 26. Git Protection

Do not:

-   stage;
-   commit;
-   branch;
-   push;
-   create/modify PR;
-   merge;
-   tag;
-   release;
-   rewrite history.

Preserve cumulative Release 1.4 work.

------------------------------------------------------------------------

## 27. GitHub Lifecycle

Only issue #163 may change.

After starting gates pass:

1.  move #163 Backlog → In Progress;
2.  add/validate permanent tests;
3.  post completion evidence only after all gates pass;
4.  close #163;
5.  set Project #2 Status = Done;
6.  verify #163 CLOSED / Done;
7.  verify #164 OPEN / Backlog unchanged;
8.  verify milestone #45 OPEN.

If #163 lifecycle writes fail to persist, reconcile only #163 under this
authority.

------------------------------------------------------------------------

## 28. Stop Conditions

Stop with:

`RELEASE 1.4 WP11 BLOCKED`

if:

-   production changes are required;
-   WP02--WP10 semantics materially conflict;
-   required tests need SQLite/Worker/DI/provider behavior;
-   package/project-reference/schema changes are needed;
-   a test requires violating accepted immutable constructors through
    unsafe/reflection hacks;
-   WP12 must be started to make WP11 pass;
-   Release 1.5 behavior is required;
-   canonical verification cannot pass without production redesign.

Report the smallest corrective authority required.

------------------------------------------------------------------------

## 29. Required Execution Report

Report at least:

1.  executive summary;
2.  authorities reviewed;
3.  initial Git/lifecycle state;
4.  working-tree classification;
5.  initial test baseline;
6.  Domain test inventory/decision;
7.  Application test inventory;
8.  coverage-gap matrix;
9.  identity tests;
10. formula tests;
11. ordering/cardinality tests;
12. timestamp/offset tests;
13. decimal/culture tests;
14. empty/single-observation tests;
15. provenance/lineage tests;
16. validation/failure tests;
17. NotFound/unavailable tests;
18. fail-stop/no-partial-result tests;
19. unknown-exception propagation tests;
20. integration-use-case tests;
21. test-double design;
22. Infrastructure/Worker/DI exclusion;
23. exact files added/modified;
24. production delta;
25. package/reference/schema delta;
26. test count deltas by project;
27. targeted test evidence;
28. full permanent test evidence;
29. canonical verification;
30. architecture validation;
31. security/offline evidence;
32. whitespace/diff evidence;
33. database/generated residue;
34. predecessor regressions;
35. WP11 coverage matrix;
36. mutation accounting;
37. Git/GitHub protection;
38. findings/blockers;
39. final GitHub state;
40. WP12 handoff;
41. final decision.

On success end exactly with:

`RELEASE 1.4 WP11 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Composition & Worker Validation — GitHub issue #164`

Do not start WP12.

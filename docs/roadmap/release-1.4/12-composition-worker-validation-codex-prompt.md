# Release 1.4 --- WP12 Composition & Worker Validation --- Codex Authority

## Mission

Execute **Release 1.4 --- WP12: Composition & Worker Validation ---
GitHub issue #164**.

WP12 adds permanent deterministic offline validation of the accepted
WP09 dependency/configuration boundary and WP10 one-shot Worker feature
execution. Production behavior must not change.

Recommended model: **GPT-5.6 Terra**.

## 1. Mandatory Authorities

Before mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
6.  WP04--WP11 authorities and execution results, including WP06
    identity clarification.
7.  Existing Release 1.3 composition/Worker permanent tests.
8.  Current Application/Infrastructure DI registrations.
9.  Current Worker `Program.cs`, `FeatureExecutionConfiguration.cs`, and
    `FeatureExecution.cs`.
10. Current SQLite snapshot store and schema-v2 behavior.
11. Current test-project references, architecture rules, and GitHub
    state for #164/#165.

Repository truth and accepted authorities govern. Do not redesign
production code for testing.

## 2. Starting-State Gates

Verify:

-   `main`; `HEAD == origin/main`; ahead/behind `0/0`; staged paths `0`.
-   Cumulative Release 1.4 paths are expected.
-   #153--#163 Closed/Done.
-   #164 OPEN / Backlog.
-   #165 OPEN / Backlog and untouched.
-   Milestone #45 OPEN.
-   SQLite schema exactly v2.
-   No Release 1.5 implementation.
-   Production graph unchanged: Domain → none; Application → Domain;
    Infrastructure → Application; Worker → Application, Infrastructure.

Run `eng/verify.ps1 -Configuration Release` before mutation.

Expected accepted baseline: Domain 11, Application 86, Infrastructure
96, Architecture 13, total 206. Repository truth wins if an accepted
predecessor delta exists.

Only after gates pass may #164 move Backlog → In Progress.

## 3. Objective

Add permanent offline validation for:

-   WP09 production DI/configuration composition.
-   WP10 real bounded Worker feature execution.

WP12 validates accepted behavior only. Expected production delta: `0`.

## 4. File Surface

Use `RELEASE_1.4_FILE_MANIFEST.md` as hard authority.

Prefer one focused Infrastructure test file consistent with Release 1.3
conventions, e.g.:

`tests/AIQuantTradingResearch.Infrastructure.Tests/FeatureCompositionTests.cs`

Reuse the existing black-box Worker process pattern. Do not add a Worker
project reference, package, or project reference unless already
authorized; otherwise stop.

## 5. Composition Coverage

Permanently prove:

-   exactly one effective `IFeatureGenerationUseCase` registration,
    resolving to `FeatureGenerationUseCase`, with accepted transient
    lifetime;
-   exactly one effective `IFeatureComputer`, resolving to
    `SimpleReturnFeatureComputer`, transient;
-   exactly one effective `IFeatureGenerationValidator`, resolving to
    `FeatureGenerationValidator`, transient;
-   existing Release 1.2 `IDatasetSnapshotStore` registration is reused,
    not duplicated;
-   complete feature graph resolves.

Do not assert irrelevant DI-container internals.

## 6. Resolution Side Effects

Building/resolving the production graph must not:

-   invoke feature generation/computation;
-   perform snapshot lookup;
-   create feature evidence;
-   create/mutate SQLite solely due to resolution;
-   invoke provider/network code;
-   persist feature output;
-   start Worker execution.

Use an isolated disposable path and prove composition-only resolution
creates no database.

## 7. Configuration Coverage

Permanently prove applicable WP09 behavior:

-   exact `Feature:SnapshotIdentity`;
-   exact `Feature:SnapshotVersion`;
-   built-in definition is always `simple-return-lag-1-v1`;
-   valid configuration creates the accepted request;
-   missing/malformed identity fails before execution;
-   missing/malformed/invalid version fails before execution;
-   no configurable formula, lag, or rounding;
-   culture-independent parsing.

Use a non-default culture where useful.

## 8. Worker Black-Box Strategy

Prefer the established Release 1.3 process-test approach.

Run the built Worker as an external bounded process using:

-   isolated temporary directories;
-   isolated SQLite database;
-   synthetic accepted snapshot evidence;
-   deterministic configuration;
-   dummy/non-production provider key only if composition requires it.

The feature path must make zero provider/network calls.

## 9. Snapshot Seeding

Seed snapshots through accepted repository mechanisms using
deterministic synthetic evidence. Do not call live providers or depend
on external historical data. Do not bypass invariants with direct row
corruption except where an existing governed test pattern explicitly
authorizes it.

## 10. Non-Empty Success

Prove a real feature Worker process:

-   selects feature mode;
-   exits `0`;
-   emits `simple-return-lag-1-v1`;
-   emits Feature Definition Identity;
-   emits Feature Set Identity;
-   emits exact snapshot identity/version;
-   emits expected feature count and stable semantic evidence;
-   terminates.

Assert stable semantic markers rather than incidental whitespace.

## 11. Equivalent Separate Processes

Run two separate processes over identical accepted
snapshot/configuration and prove:

-   both exit `0`;
-   same Feature Definition Identity;
-   same Feature Set Identity;
-   same snapshot/version;
-   no feature persistence/run history.

## 12. Empty Snapshot

For an existing zero-observation snapshot prove:

-   it is found, not NotFound;
-   exit `0`;
-   count `0`;
-   deterministic Feature Set Identity;
-   exact snapshot/version preserved.

## 13. Single Observation

For an existing one-observation snapshot prove:

-   exit `0`;
-   count `0`;
-   deterministic Feature Set Identity;
-   no invalid-numeric or NotFound classification.

## 14. Invalid Configuration

Provide enough black-box evidence for malformed/missing Feature
configuration, such as identity without version, version without
identity, malformed identity, or malformed version.

Requirements:

-   non-zero exit;
-   failure before feature execution;
-   no fabricated Feature Set Identity;
-   zero provider/network calls;
-   no feature persistence.

Avoid redundant cases already fully proven at configuration level.

## 15. Snapshot NotFound

Use a syntactically valid absent exact snapshot identity/version and
prove:

-   non-zero exit;
-   accepted `SnapshotNotFound` vocabulary;
-   no Feature Set Identity fabricated;
-   no provider/network fallback.

## 16. Dependency Unavailable

Use a deterministic isolated unavailable-storage scenario consistent
with repository conventions and prove:

-   non-zero exit;
-   accepted `DependencyUnavailable`;
-   no Feature Set Identity fabricated;
-   no provider/network fallback;
-   process terminates.

Prefer portable failure setup.

## 17. Invalid Numeric / Integrity Cases

WP11 owns permanent Application semantic coverage. Add process-level
cases only if cleanly constructible through accepted persisted evidence.
Do not corrupt SQLite merely to force them unless repository test
strategy explicitly authorizes it. Otherwise report the accepted
invariant/predecessor coverage.

## 18. Unknown Exceptions

Do not change production exception behavior. WP11 already proves
Application propagation. Do not manufacture arbitrary runtime defects or
add broad exception normalization.

## 19. Release 1.3 Regression

Preserve existing Release 1.3 composition/Worker tests. Feature mode
must not become a sixth pipeline stage or silently chain after pipeline
execution. Absence of Feature configuration must preserve the existing
pipeline path.

## 20. Bounded One-Shot Evidence

Use the strongest practical evidence without production instrumentation:

-   one terminal semantic result per process;
-   prompt process termination;
-   no recurrence/timer output;
-   no duplicate execution evidence;
-   no persisted run history.

Avoid sleep-based assertions except a process timeout used only as a
safety guard.

## 21. Provider / Network Isolation

All WP12 tests are offline. Dummy credentials may satisfy composition
only. Provider calls, HTTP calls, live credential validation, and real
keys are forbidden.

## 22. Schema / Cleanup

Confirm:

-   SQLite schema v2;
-   no feature tables/catalog/cache/run history;
-   no scheduler/checkpoint/pipeline-state table;
-   no repository/database residue.

Clean disposable database files and `-wal`, `-shm`, `-journal` sidecars.

## 23. Test Isolation

Use unique temporary resources per test. Prevent interference through
shared DB names, mutable environment variables, current-directory
assumptions, or stale output. Follow existing repository
isolation/serialization patterns.

## 24. Test Counts

Report exact before/after counts.

Starting baseline: Domain 11, Application 86, Infrastructure 96,
Architecture 13, total 206.

WP12 should normally increase Infrastructure.Tests only. Do not target
an arbitrary count.

## 25. Required Acceptance Matrix

Classify each row with permanent evidence or an accepted invariant:

1.  one effective feature use-case registration;
2.  correct implementation/lifetime;
3.  one effective feature-computer registration;
4.  correct implementation/lifetime;
5.  one effective validator registration;
6.  correct implementation/lifetime;
7.  snapshot store reused;
8.  composition resolution succeeds;
9.  resolution performs no feature execution;
10. resolution performs no snapshot lookup;
11. resolution creates no database solely by resolution;
12. resolution performs no provider/network call;
13. valid configuration builds exact request;
14. culture-independent configuration parsing;
15. invalid configuration fails before execution;
16. non-empty Worker process exits `0`;
17. non-empty output exposes accepted identities/snapshot/count;
18. two equivalent processes preserve Feature Set Identity;
19. empty snapshot exits `0`, count `0`;
20. single observation exits `0`, count `0`;
21. exact NotFound exits non-zero;
22. NotFound fabricates no Feature Set Identity;
23. dependency unavailable exits non-zero;
24. unavailable fabricates no Feature Set Identity;
25. provider/network calls zero;
26. no feature persistence/run history;
27. schema remains v2;
28. process is bounded/one-shot;
29. Release 1.3 pipeline tests remain passing;
30. production delta zero.

Distinguish direct proof from invariant-based coverage.

## 26. Validation

After adding tests:

1.  run targeted new Infrastructure tests;
2.  run full Infrastructure.Tests;
3.  run full permanent suite;
4.  run `git diff --check`;
5.  run `git diff --cached --check`;
6.  inspect whitespace in new untracked files directly;
7.  run `eng/verify.ps1 -Configuration Release`.

Confirm:

-   build warnings/errors `0/0`;
-   all tests pass;
-   Architecture.Tests pass;
-   Gitleaks PASS;
-   production delta `0`;
-   package/reference/schema delta `0/0/0`;
-   database/sidecar/generated residue `0`;
-   provider/network activity `0`;
-   real credentials `0`;
-   dependency graph unchanged and acyclic.

No temporary probe/process artifact may remain.

## 27. Regression Requirements

Confirm Release 1.1 historical persistence/retrieval, Release 1.2
snapshot identity/exact lookup/schema v2, Release 1.3
DI/configuration/Worker tests, and all 86 WP11 Application tests remain
passing.

Do not weaken predecessor tests.

## 28. Architecture Boundary

Expected Architecture.Tests delta: `0`. WP13 owns Architecture &
Documentation Evolution.

## 29. Release 1.5 Protection

Do not add feature persistence/catalog/cache, multiple features,
configurable formulas/lags, plugins, DAGs, scheduling, retries, durable
history, notebooks, backtesting, strategies, ML, or MLOps.

## 30. Git Protection

Do not stage, commit, branch, push, create/modify PRs, merge, tag,
release, or rewrite history.

## 31. GitHub Lifecycle

Only #164 may change.

After gates pass:

1.  Backlog → In Progress;
2.  add/validate tests;
3.  post completion evidence only after success;
4.  close #164;
5.  set Project #2 Status Done;
6.  verify #164 CLOSED / Done;
7.  verify #165 OPEN / Backlog unchanged;
8.  verify milestone #45 OPEN.

If lifecycle writes fail, reconcile only #164.

## 32. Stop Conditions

Stop with `RELEASE 1.4 WP12 BLOCKED` if production code,
packages/references/schema changes, provider/network activity, feature
persistence, Release 1.3 semantic changes, WP13 work, or Release 1.5
behavior become necessary; if black-box Worker validation cannot be
achieved within the governed test architecture; or if canonical
verification cannot be restored within test-only scope.

Report the smallest corrective authority required. Do not guess.

## 33. Required Execution Report

Include at least:

1.  executive summary;
2.  authorities reviewed;
3.  Git baseline;
4.  working-tree classification;
5.  lifecycle gates;
6.  initial canonical baseline;
7.  Infrastructure test inventory;
8.  Release 1.3 Worker-test pattern;
9.  WP09 composition reconciliation;
10. WP10 Worker reconciliation;
11. test isolation strategy;
12. registration/lifetime coverage;
13. composition resolution and side-effect proof;
14. configuration/culture coverage;
15. Worker process strategy;
16. snapshot seeding;
17. non-empty success;
18. equivalent separate processes;
19. empty success;
20. single-observation success;
21. invalid configuration;
22. NotFound;
23. dependency unavailable;
24. integrity/numeric process decision;
25. unknown-exception decision;
26. bounded-process evidence;
27. Release 1.3 regression;
28. provider/network isolation;
29. schema/cleanup;
30. exact files added/modified;
31. production delta;
32. package/reference/schema delta;
33. architecture-test delta;
34. test counts before/after;
35. targeted/full test evidence;
36. canonical verification;
37. architecture/security/Gitleaks evidence;
38. whitespace/diff evidence;
39. predecessor regressions;
40. WP12 acceptance matrix;
41. mutation accounting;
42. Git/GitHub protection;
43. findings/blockers;
44. final GitHub state;
45. WP13 handoff;
46. final decision.

On success end exactly with:

`RELEASE 1.4 WP12 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP13 — Architecture & Documentation Evolution — GitHub issue #165`

Do not start WP13.

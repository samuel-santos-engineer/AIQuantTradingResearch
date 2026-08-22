# Release 1.5 WP11 — Composition & Worker Validation

## GitHub Issue
`#178 — Release 1.5 WP11 — Composition & Worker Validation`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP11 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Built-in experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP11 converts the accepted WP08 composition evidence and WP09 removable Worker process evidence into permanent deterministic offline Infrastructure validation.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- accepted WP04–WP07 Application experiment implementation
- WP08 `DependencyInjection.cs`
- WP08 `ExperimentExecutionConfiguration.cs`
- WP09 `Program.cs`
- WP09 `ExperimentExecution.cs`
- WP10 `ExperimentApplicationTests.cs`
- existing Release 1.4 `FeatureCompositionTests.cs` and Infrastructure-test conventions
- existing schema-v2 SQLite test helpers/conventions
- WP01–WP10 completion evidence
- this WP11 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

WP11 is permanent Infrastructure validation only. If a correctly constructed test reveals a production defect, stop and report the defect and smallest corrective authority rather than silently changing production code.

---

## 2. Objective

Add the minimum high-value permanent deterministic offline Infrastructure test suite that proves Release 1.5 composition and Worker execution behavior.

Protect:

- experiment DI registrations;
- exact service lifetimes;
- production graph resolution;
- side-effect-free DI resolution;
- no resolution-time database creation;
- exact Experiment configuration behavior where appropriate;
- schema-v2 synthetic snapshot integration;
- external Worker one-shot execution;
- deterministic second-process Experiment Result identity;
- non-empty success;
- empty success;
- single-observation success;
- malformed/partial Experiment configuration;
- exact NotFound behavior;
- unavailable-storage behavior where safely inducible;
- no fabricated Experiment Result identity on failures;
- no provider/network fallback;
- no experiment persistence;
- predecessor routing protection where practical.

Do not duplicate WP10 Application semantic tests.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`.

Expected lifecycle:

- #168–#177: CLOSED / Done;
- #178 WP11: OPEN / Backlog;
- #179 WP12: OPEN / Backlog;
- #180 WP13: OPEN / Backlog;
- milestone #46: OPEN with 3 open / 10 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected permanent baseline:

- Domain.Tests: 11;
- Application.Tests: 102;
- Infrastructure.Tests: 104;
- Architecture.Tests: 13;
- total: 230;
- SQLite schema: v2.

Expected cumulative accepted Release 1.5 work remains unstaged.

If #177 is not Closed/Done or #179 has started, stop before mutation.

---

## 4. WP11 Lifecycle Start

After starting-state gates pass:

- move only #178 Project #2 Status from Backlog to In Progress.

Read back the state.

Do not mutate #179.

#179 must remain OPEN / Backlog throughout WP11.

---

## 5. Hard File Boundary

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

WP11 must modify/add only the manifest-authorized Infrastructure test path.

Expected logical path:

`tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentCompositionTests.cs`

Use the exact manifest path.

Expected WP11 deltas:

- Infrastructure permanent tests: +1 test file;
- Domain tests: 0 files;
- Application tests: 0 files;
- Architecture tests: 0 files;
- production files: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not stage or commit.

---

## 6. Production Code Freeze

WP11 does not authorize production changes.

Do not modify:

- Application experiment code;
- Application DI;
- Worker configuration;
- Worker execution;
- `Program.cs`;
- Domain;
- Infrastructure production;
- SQLite schema;
- packages/references/projects.

If a permanent composition/Worker test exposes a production defect, stop.

Do not weaken the test to hide the defect.

---

## 7. Existing Infrastructure Test Convention Reuse

Inspect Release 1.4 `FeatureCompositionTests.cs` before writing WP11.

Reuse accepted conventions for:

- temporary directory/database isolation;
- schema-v2 snapshot seeding;
- process execution;
- `--no-build`;
- dummy provider credentials;
- output capture;
- cleanup;
- platform-safe Worker invocation;
- timeout/bounded execution;
- database/WAL/SHM/journal cleanup.

Prefer extending the established pattern conceptually without modifying the Release 1.4 test file unless the manifest explicitly authorizes it.

No new test framework/package.

---

## 8. Test Quality Rules

Every WP11 test must protect a composition or process boundary not already sufficiently protected by WP10.

Avoid:

- retesting pure identity algorithms;
- retesting pure summary arithmetic;
- duplicating validator unit tests;
- implementation-detail-only assertions;
- network-dependent tests;
- timing races;
- arbitrary sleeps.

Tests must be deterministic, bounded, offline, and isolated.

---

## 9. DI Registration Test

Permanently verify production registration of:

- `IExperimentGenerationUseCase → ExperimentGenerationUseCase`;
- `IExperimentSummaryComputer → SimpleReturnDescriptiveSummaryComputer`;
- `IExperimentGenerationValidator → ExperimentGenerationValidator`.

Verify exact accepted lifetimes.

Expected lifetime from WP08: transient.

Also prove Release 1.4 `IFeatureGenerationUseCase` remains available through the same production graph.

Do not require duplicate registrations.

---

## 10. Effective Registration Count

Where repository DI conventions permit descriptor inspection, prove there is exactly one effective registration for each Release 1.5 experiment interface.

Protect against accidental duplicate composition.

Do not impose this rule on unrelated predecessor services unless already governed.

---

## 11. Production Graph Resolution

Build the real production service collection/provider using isolated configuration and dummy credentials where required.

Resolve:

- `IExperimentGenerationUseCase`;
- its accepted transitive dependencies.

Require successful resolution.

Do not invoke the experiment during this test.

---

## 12. Side-Effect-Free Resolution

Permanently prove DI graph construction/resolution does not:

- invoke experiment generation;
- invoke feature generation;
- compute summary evidence;
- create an experiment result;
- call provider/network;
- persist experiment evidence.

Use observable external side effects rather than invasive reflection.

---

## 13. No Resolution-Time Database Creation

Configure an isolated nonexistent SQLite database path.

Build and resolve the experiment production graph.

Assert the database file still does not exist after resolution.

Also assert no WAL/SHM/journal sidecar exists.

This protects WP08's side-effect-free composition contract.

---

## 14. Configuration Validation

Where practical at Infrastructure/Worker boundary, permanently protect WP08 configuration behavior:

- valid exact snapshot identity/version parses;
- missing mandatory Experiment value fails;
- malformed identity fails;
- incoherent/malformed version fails;
- built-in experiment remains `simple-return-descriptive-summary-v1`.

Do not duplicate every WP10 semantic validation case.

If these are more cleanly protected through process tests, prefer process evidence.

---

## 15. Worker Process Harness

Create a deterministic test helper inside the authorized WP11 test file using established Release 1.4 conventions.

The helper must:

- invoke the already-built Worker;
- use `--no-build`;
- set isolated configuration/environment;
- capture stdout/stderr;
- capture exit code;
- enforce a bounded timeout;
- never call a provider;
- clean all temporary state.

Do not modify production code to make process testing easier.

---

## 16. Synthetic Schema-v2 Snapshot Seeding

For successful experiment process tests:

- create an isolated temporary SQLite database;
- initialize only through accepted schema-v2 behavior;
- seed deterministic accepted snapshot evidence using existing Infrastructure APIs/helpers where possible;
- preserve exact snapshot identity/version coherence.

Do not create experiment tables.

Do not alter schema.

---

## 17. Non-Empty Worker Success

Seed a deterministic snapshot that produces a non-empty `simple-return-lag-1-v1` Feature Set.

Run the Worker in Experiment mode.

Require:

- exit code `0`;
- bounded experiment success evidence;
- expected experiment definition;
- valid Experiment Result Identity;
- exact Feature Set/snapshot binding evidence;
- expected count;
- exact invariant mean/minimum/maximum.

Use the accepted WP09 representative evidence if convenient, but derive expected values independently from seeded inputs.

---

## 18. Equivalent Second Process

Run a fresh second Worker process with identical semantic input.

Require:

- exit `0`;
- same Experiment Result Identity;
- same count;
- same mean/minimum/maximum;
- same semantic Feature Set binding.

Do not require operational logs/process IDs to match.

This permanently proves process-level deterministic recomputation.

---

## 19. Empty Snapshot Success

Seed an accepted empty snapshot.

Run Experiment mode.

Require:

- exit `0`;
- count `0`;
- aggregate absence;
- valid deterministic Experiment Result Identity;
- no fabricated numeric zero aggregates;
- no persistence.

---

## 20. Single-Observation Snapshot Success

Seed an accepted one-observation snapshot.

Because Release 1.4 lag-1 feature generation yields an empty Feature Set, require:

- exit `0`;
- count `0`;
- aggregate absence;
- valid Experiment Result Identity.

Where both empty-snapshot and single-observation cases are tested, require distinct Experiment Result identities if their Feature Set identities are distinct.

No global empty sentinel.

---

## 21. Malformed Experiment Configuration

Run Worker with explicit malformed Experiment identity.

Require:

- exit `1`;
- `Invalid mandatory experiment configuration.` or the exact accepted bounded configuration output;
- no Experiment Result Identity;
- no Feature fallback;
- no pipeline fallback;
- no provider/network activity.

Do not assert irrelevant formatting.

---

## 22. Partial Experiment Intent

Provide one Experiment selector but omit/invalidate the other.

Optionally also provide otherwise-valid Feature configuration to make fallback detectable.

Require:

- Experiment mode remains selected;
- exit `1`;
- no Feature execution success evidence;
- no pipeline execution success evidence;
- no Experiment Result Identity.

This protects WP09 mode precedence.

---

## 23. Exact Snapshot NotFound

Use a valid Experiment identity/version that does not exist in the isolated schema-v2 store.

Require:

- bounded accepted NotFound failure category, using actual Release 1.5 vocabulary;
- exit `1`;
- no Experiment Result Identity;
- no provider fallback;
- no persistence.

WP09 observed `FeatureSetNotFound`; reconcile actual contract and assert exact accepted vocabulary.

---

## 24. Unavailable Storage

Safely induce storage/dependency unavailability using an isolated deterministic filesystem/database condition already compatible with existing Infrastructure tests.

Require:

- accepted `DependencyUnavailable` failure;
- exit `1`;
- no Experiment Result Identity;
- no provider fallback.

Do not depend on machine-specific privileged behavior.

If a portable deterministic unavailability case is not possible without weakening the test suite, document and stop only if the WP11 manifest/plan makes this mandatory.

Prefer established Release 1.4 technique.

---

## 25. No Fabricated Identity on Failure

Across malformed configuration, NotFound, and unavailable dependency tests, assert output does not contain a successful Experiment Result Identity.

Do not infer absence solely from exit code.

Use the stable semantic output marker introduced by WP09.

---

## 26. No Provider Fallback

All process tests must operate with:

- dummy provider key only if composition requires it;
- no network connectivity requirement;
- no live provider call.

Where existing test infrastructure can detect provider invocation, assert zero calls.

At minimum, successful and NotFound tests must be fully satisfied from isolated SQLite evidence without acquisition.

---

## 27. No Experiment Persistence

After successful and failed Worker runs, inspect the schema/database.

Require:

- SQLite schema remains v2;
- no experiment table;
- no experiment result/history/registry/cache table;
- no feature persistence expansion caused by Release 1.5.

Do not overfit to unrelated SQLite internal tables.

Use accepted schema inspection conventions.

---

## 28. Predecessor Routing Protection

Where cheap and stable, permanently prove:

- absent Experiment selectors with valid Feature selectors continues to select Release 1.4 Feature mode;
- absent Experiment and Feature selectors continues to preserve Release 1.3 pipeline routing.

Do not create live-provider dependency just to test pipeline routing.

If the existing Release 1.4 composition suite already protects Feature mode sufficiently, avoid redundant process tests and report coverage reuse.

---

## 29. Output Parsing

Parse only stable semantic markers needed for assertions.

Do not snapshot entire console output.

Prefer helper methods that extract:

- Experiment Result Identity;
- count;
- aggregate evidence;
- failure category.

Keep parsing local to the test file.

---

## 30. Process Isolation

Each process test must use:

- unique temporary directory;
- unique database path;
- explicit environment/configuration;
- bounded timeout;
- cleanup in `finally`/disposable fixture.

Tests must be safe under normal parallel execution, or use existing repository collection/parallelization conventions if shared process/global state requires serialization.

Do not globally disable test parallelism unless already established.

---

## 31. Culture Determinism at Worker Boundary

Where practical without global-race risk, run at least one Worker process under a culture such as `pt-BR`.

Require invariant decimal semantic output and stable Experiment Result Identity.

If the process environment inherits culture differently across platforms, set culture through the accepted .NET environment mechanism only within that child process.

Do not mutate production behavior.

---

## 32. No Permanent Test of Unknown Crash Details

WP10 already protects unknown exception propagation at Application seams.

WP11 need not manufacture a Worker crash merely to inspect platform-specific unhandled-exception output.

Protect the absence of broad Worker normalization through production-code inspection and existing Application tests unless a deterministic process seam already exists.

---

## 33. Test Count Discipline

Do not target an arbitrary Infrastructure test delta.

Add the smallest coherent suite that permanently covers WP11 acceptance.

Report:

- Infrastructure baseline: 104;
- Infrastructure final count;
- Infrastructure delta;
- total baseline: 230;
- total final;
- total delta.

Domain/Application/Architecture deltas must be 0.

---

## 34. Targeted Validation

Run the new WP11 tests directly first.

All must pass deterministically.

If process tests fail intermittently, fix the test isolation/harness only within the authorized test file if the production behavior is correct.

Do not add sleeps as a reliability substitute.

---

## 35. Canonical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected:

- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: all pass with WP11 delta;
- Architecture.Tests: 13/13;
- skipped: 0;
- warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of the WP11 test file and relevant untracked governance artifacts.

---

## 36. Residue Validation

After all tests:

- temporary databases: 0;
- WAL files: 0;
- SHM files: 0;
- journal files: 0;
- temporary process directories: 0;
- temporary scripts/projects/hooks: 0;
- generated residue: 0.

Do not count normal ignored build output as unauthorized residue unless repository policy does.

---

## 37. Security / Credential Validation

Require:

- Gitleaks PASS;
- real credentials: 0;
- provider/network calls: 0.

Dummy credential strings must be clearly non-secret test values.

Do not persist them into production configuration.

---

## 38. Architecture / Schema Protection

Confirm:

- production graph unchanged;
- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure;
- cycles 0;
- unexpected edges 0;
- Architecture.Tests remain 13/13;
- package/reference delta 0;
- schema delta 0;
- SQLite schema v2.

WP11 Architecture.Tests delta: 0.

---

## 39. Regression Protection

All predecessor suites must remain green:

- Release 1.1 persistence;
- Release 1.2 dataset identity/snapshot;
- Release 1.3 pipeline;
- Release 1.4 feature semantics/composition;
- Release 1.5 WP10 Application tests.

Do not change production to make regressions pass under WP11.

---

## 40. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- begin WP12;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only the exact manifest-authorized WP11 Infrastructure test path.

---

## 41. Authorized GitHub Mutation Budget

At WP11 start after gates pass:

1. #178 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP11 completion-evidence comment to #178;
3. close #178 as completed;
4. set #178 Project Status to Done.

Do not mutate #179.

Milestone #46 remains OPEN.

---

## 42. Completion Gate

WP11 may close only if:

- #177 is Closed/Done;
- #178 was In Progress during execution;
- #179 remains Open/Backlog;
- exact manifest path accounting passes;
- production delta is 0;
- permanent Infrastructure tests protect experiment DI registrations/lifetimes;
- graph resolution succeeds;
- resolution creates no database;
- resolution causes no execution/provider/persistence side effect;
- non-empty Worker success is permanently tested;
- equivalent second-process Experiment Result identity is stable;
- empty Worker success is tested;
- single-observation Worker success is tested;
- malformed/partial Experiment configuration is tested;
- exact NotFound behavior is tested;
- unavailable dependency behavior is tested where mandated and safely portable;
- failures fabricate no Experiment Result Identity;
- provider fallback is absent;
- experiment persistence is absent;
- schema remains v2;
- predecessor routing is preserved/reused;
- Domain/Application/Architecture test deltas are 0;
- package/project/reference/schema deltas are 0;
- all permanent tests pass;
- Architecture.Tests 13/13;
- warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- provider/network activity 0;
- Release 1.6 work 0.

If any gate fails, do not close #178 or mark Done.

---

## 43. Completion Evidence Comment

On success, post concise evidence to #178 covering:

- exact Infrastructure test file;
- Infrastructure count before/after/delta;
- total count before/after/delta;
- DI registrations/lifetimes;
- side-effect-free resolution and no database creation;
- schema-v2 synthetic snapshot strategy;
- non-empty process success;
- equivalent second-process identity;
- empty/single-observation success;
- malformed/partial configuration;
- NotFound/unavailable behavior;
- no fabricated identity on failure;
- no provider fallback/network;
- no experiment persistence/schema change;
- predecessor routing protection/reuse;
- zero production/Domain/Application/Architecture/package/reference changes;
- Architecture.Tests 13/13;
- canonical verification/Gitleaks/whitespace PASS;
- zero residue;
- #179 preserved Open/Backlog.

---

## 44. Final Read-Back

After successful closure verify:

- #178: CLOSED / Done;
- #179: OPEN / Backlog;
- #180: OPEN / Backlog;
- milestone #46: OPEN;
- milestone counts: 2 open / 11 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 state accurately.

---

## 45. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #177 is not Closed/Done;
- #179+ started unexpectedly;
- WP11 manifest ownership is ambiguous;
- a correct permanent test exposes a production defect;
- satisfying WP11 requires production mutation;
- a process test requires provider/network access;
- schema mutation is required;
- experiment persistence is required;
- premature WP12+ implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- package/project/reference mutation would be required.

Report the smallest corrective authority required.

---

## 46. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP11 manifest path;
6. existing Infrastructure-test conventions reused;
7. permanent test inventory;
8. DI registration/lifetime evidence;
9. side-effect-free resolution evidence;
10. no-resolution-database evidence;
11. schema-v2 seeding strategy;
12. non-empty Worker process evidence;
13. equivalent second-process identity;
14. empty Worker evidence;
15. single-observation Worker evidence;
16. malformed/partial configuration;
17. NotFound behavior;
18. unavailable dependency behavior;
19. no-fabricated-identity evidence;
20. provider/network isolation;
21. no experiment persistence/schema change;
22. predecessor routing coverage/reuse;
23. Infrastructure/total test deltas;
24. full canonical validation;
25. architecture/security/whitespace/residue;
26. production/package/reference/schema protection;
27. GitHub lifecycle mutations;
28. final #178/#179/milestone state;
29. findings/blockers;
30. next authorized WP.

---

## 47. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP11 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Architecture & Documentation Alignment — GitHub issue #179`

Do not begin WP12.

If blocked, end:

`RELEASE 1.5 WP11 BLOCKED`

and identify the smallest corrective authority required.

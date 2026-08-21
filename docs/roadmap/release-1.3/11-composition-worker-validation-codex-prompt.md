# Release 1.3 WP11 — Composition & Worker Validation — Codex Execution Prompt

## 1. Role and objective

Execute **Release 1.3 — WP11: Composition & Worker Validation** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- GitHub issue: **#148**
- Milestone: **#54 — Phase 3 - Release 1.3: Research Pipeline Foundation**
- Recommended model: **GPT-5.6 Terra**

Your objective is to add the minimum permanent, deterministic, offline validation required for the Release 1.3 **composition, configuration, and one-shot Worker execution boundary** established by WP08 and WP09.

WP11 is a permanent validation package.

WP11 must validate the already-accepted composition and Worker behavior without redesigning production semantics.

WP11 must not start WP12 Architecture Evolution.

---

## 2. Mandatory authority

Before any mutation, read completely and reconcile at minimum:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
5. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
6. WP01–WP10 authoritative prompt pairs and accepted execution results.
7. Current Release 1.3 Application pipeline implementation and tests.
8. Current Application dependency registration.
9. Current Infrastructure dependency registration.
10. Current Worker implementation, including:
    - `Program.cs`
    - `PipelineExecutionConfiguration.cs`
    - `PipelineExecution.cs`
11. Existing Release 1.2 composition/configuration/Worker validation tests.
12. Current Infrastructure test project structure and references.
13. Current architecture tests, packages, project references, engineering scripts, and GitHub planning state.

Repository truth wins over assumptions.

Do not change production code merely to make a test easier to write. If accepted production behavior is defective, stop and report the smallest corrective authority required.

---

## 3. Starting-state gates

Before adding tests, prove and report:

- Release 1.2 remains closed.
- Release 1.3 milestone #54 remains open.
- Issues #138–#147 are Closed/Done.
- WP11 issue #148 is Open/Backlog.
- WP12 issue #149 remains Open/Backlog and unchanged.
- #148 dependencies exactly match the authoritative graph: WP07, WP08, WP09.
- No WP12+ implementation has started.
- No Release 1.4 implementation has started.
- Current branch is `main`.
- `HEAD == origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- Existing cumulative Release 1.3 working-tree paths are expected and manifest-authorized.
- No unexpected generated SQLite/WAL/SHM/journal residue exists.
- SQLite schema remains version `2`.
- Canonical Release verification passes before mutation.

Expected permanent baseline before WP11:

- Domain.Tests: `11`
- Application.Tests: `77`
- Infrastructure.Tests: `87`
- Architecture.Tests: `13`
- Permanent total: `188`
- Skipped: `0`

Only after every starting-state gate passes may #148 move Backlog → In Progress.

---

## 4. WP11 ownership boundary

WP11 owns permanent validation of:

- dependency registration;
- service lifetimes where observable;
- graph resolution;
- configuration parsing/validation;
- resolution-side-effect protection;
- one-shot Worker execution;
- Worker exit semantics;
- structured evidence presentation boundary;
- provider/network isolation;
- disposable database cleanup.

WP11 does **not** own:

- Application semantic behavior already tested by WP10;
- SQLite schema/model testing already owned by Release 1.2/WP14;
- new architecture rules;
- documentation updates;
- integration/PR creation.

Do not duplicate WP10 unless a composition-specific seam requires a focused assertion.

---

## 5. Production-code protection

Expected production delta:

- Domain: `0`
- Application: `0`
- Infrastructure: `0`
- Worker: `0`

If a required permanent validation reveals a production defect, stop and report the defect. Do not repair production behavior inside WP11.

Expected package/reference delta: `0/0`.

Expected schema delta: `0`.

Do not create a new test project unless explicitly authorized by the manifest.

If Worker process validation cannot be expressed through existing project/reference structure without adding a project reference, first determine whether process-level execution from the existing test project or repository test harness is sufficient. If not, stop and request corrective authority.

---

## 6. Existing coverage inventory first

Before adding tests, inventory:

- existing Infrastructure.Tests;
- existing Release 1.2 DI/configuration tests;
- existing Worker-related tests/probes if any;
- current Application.Tests from WP10;
- existing architecture rules.

Classify required WP11 coverage as:

- already permanently covered;
- composition-specific gap;
- Worker/process-specific gap;
- architecture-owned by WP12;
- redundant and therefore excluded.

Do not add duplicate tests simply to increase counts.

---

## 7. Dependency-registration coverage

Add permanent tests where missing to prove:

- `IPipelineExecutionUseCase` is registered exactly once;
- it resolves to the accepted `PipelineExecutionUseCase`;
- accepted Release 1.2 dependencies remain resolvable;
- `IMaterializeDatasetUseCase` registration remains intact;
- `IDatasetSnapshotStore` registration remains intact;
- `IDatasetCatalog` registration remains intact;
- pipeline request factory/configuration seam resolves as designed;
- no duplicate pipeline registration exists.

Do not assert implementation details that are not contractually relevant unless needed to protect the accepted composition graph.

---

## 8. Lifetime coverage

Where current DI container inspection makes this meaningful, validate:

- pipeline execution use case lifetime is the accepted lifetime;
- stateless request factory lifetime is the accepted lifetime;
- existing immutable configuration/factory lifetimes remain unchanged;
- no singleton captures mutable per-execution pipeline state;
- no singleton captures an open SQLite connection;
- persistence stores retain accepted lifetimes.

Do not rewrite registrations to satisfy a preferred lifetime. If repository truth differs from accepted WP08 authority, stop and report it.

---

## 9. Resolution-side-effect coverage

Permanent validation must prove, as applicable:

- building the service provider does not execute the pipeline;
- resolving `IPipelineExecutionUseCase` does not execute the pipeline;
- resolving pipeline composition does not call the market-data provider;
- resolving composition does not persist snapshot/catalog evidence;
- resolving composition does not create durable run-history evidence;
- resolving composition does not create a database file merely through resolution where the accepted connection-factory semantics support that guarantee.

Use disposable temporary paths.

No live provider calls.

---

## 10. Configuration coverage

Add permanent coverage for the accepted WP08 configuration behavior.

At minimum prove:

- `Dataset:Target` is required;
- `Dataset:From` is required;
- `Dataset:To` is required;
- valid invariant round-trip `DateTimeOffset` values parse correctly;
- original offset is preserved;
- malformed timestamps fail deterministically;
- invalid `[from,to)` intervals fail deterministically;
- target is preserved exactly;
- no implicit defaults or in-memory fallback are introduced;
- no `Pipeline:*` semantic configuration is required;
- topology, identity scheme, stage order, and failure policy are not configurable.

Where configuration parsing is implemented in a Worker-owned type inaccessible to current test projects, use the smallest manifest-authorized/process-level validation route rather than adding a new project reference without authority.

---

## 11. One-shot Worker execution coverage

Add the minimum permanent validation needed to protect the WP09 process boundary.

Prove:

- one Worker process invocation results in one pipeline invocation;
- no internal loop exists;
- no retry exists;
- no timer/scheduler/background recurrence exists;
- successful execution returns exit code `0`;
- `NewlyAccepted` is success;
- `EquivalentExisting` is success;
- valid empty result is success;
- bounded pipeline failure returns non-zero;
- invalid configuration returns non-zero before execution;
- unknown/unhandled failure behavior remains consistent with WP06/WP09 policy.

Prefer observable process behavior and public seams over brittle source-text matching.

If exactly-once invocation cannot be permanently verified without changing production testability, determine whether existing process output/database effects provide a sufficient black-box proof. Do not add production-only test hooks.

---

## 12. Structured evidence presentation coverage

Validate the Worker consumes the accepted WP07 evidence surface rather than inventing a second semantic model.

Where practical through black-box/process validation, prove output/presentation includes the applicable accepted facts:

- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity when established;
- terminal outcome/disposition;
- ordered stage evidence;
- first failing stage;
- bounded failure category;
- dataset/source/snapshot/version identities where established.

Also prove it does not emit:

- API keys;
- connection strings;
- sensitive temporary paths where avoidable;
- fabricated downstream identities after failure.

Do not make console text formatting itself a semantic contract unless the repository already treats it as one.

Prefer assertions on bounded stable markers/meaning rather than entire multiline output snapshots.

---

## 13. Real offline process proof

Where feasible under the existing test infrastructure, add permanent or tightly integrated deterministic validation for two separate Worker invocations against one disposable file-backed SQLite database with synthetic accepted historical observations:

### First execution

Expect:

- exit `0`;
- `NewlyAccepted`;
- valid semantic execution identity;
- snapshot/catalog evidence exists.

### Equivalent second execution

Expect:

- exit `0`;
- `EquivalentExisting`;
- same semantic execution identity;
- no destructive overwrite.

The two executions must be separate process invocations, not an internal loop.

If process-based permanent tests would be too brittle or would require unauthorized project/reference/tooling changes, keep those exact behaviors protected by the accepted WP09 execution proof and add only the minimum permanent composition/Worker boundary tests justified by repository conventions. Report the decision explicitly.

---

## 14. Failure-process validation

Where safely and deterministically testable, prove:

- missing required configuration returns non-zero before pipeline execution;
- invalid storage/dependency state produces bounded non-zero failure;
- failure presentation identifies the first failing stage;
- no later-stage evidence appears.

Use disposable synthetic paths/state only.

Do not deliberately corrupt repository-owned databases.

---

## 15. Provider/network isolation

All WP11 tests must be offline.

Permanent validation must not call:

- Twelve Data;
- HTTP market-data endpoints;
- any live provider.

If current host construction requires an API key syntactically, use a clearly fake/dummy value and prove the executed path does not call the provider.

Never use a real credential.

---

## 16. SQLite isolation and cleanup

When file-backed SQLite is required:

- use unique temporary directories/files;
- use only synthetic observations;
- clear pools if required by current provider behavior;
- dispose all connections/process resources;
- remove database files;
- remove WAL/SHM/journal files;
- confirm repository residue `0`.

Do not rely on shared developer-local databases.

SQLite schema must remain version `2`.

Do not add schema coverage already owned by Release 1.2 unless needed to establish the process setup.

---

## 17. Test project/file authority

Use `RELEASE_1.3_FILE_MANIFEST.md` as exact file authority.

Preferred permanent validation file if manifest-authorized:

`tests/AIQuantTradingResearch.Infrastructure.Tests/PipelineCompositionTests.cs`

If Worker validation can be represented in that project using existing references/process execution, prefer one focused file.

If the manifest specifies another exact path, follow it.

Do not add packages or project references merely for convenience.

---

## 18. Explicit out of scope

WP11 MUST NOT implement:

- WP12 architecture-test changes;
- WP13 documentation alignment;
- WP14 integration/branch/commit/PR work;
- production refactoring;
- Application semantic tests already owned by WP10;
- schema v3;
- pipeline run-history persistence;
- scheduling;
- cron/timers;
- recurring/background execution;
- retries;
- circuit breakers;
- fallback providers;
- DAGs/plugins;
- parallel/streaming/distributed execution;
- checkpoints/resume;
- metrics/tracing backends;
- feature engineering;
- model training/evaluation;
- MLOps;
- Release 1.4 work.

Do not start WP12.

---

## 19. Test-count accounting

Report exact before/after/delta.

Expected starting baseline:

- Domain: `11`
- Application: `77`
- Infrastructure: `87`
- Architecture: `13`
- Total: `188`

WP11 should normally increase **Infrastructure.Tests** only, unless repository/manifest truth explicitly places process validation elsewhere.

Do not target an arbitrary number of new tests.

Add the minimum permanent set that closes genuine WP11 gaps.

---

## 20. Mandatory validation

After adding WP11 tests, run:

- targeted Infrastructure.Tests;
- any exact Worker/process tests added;
- full permanent test suite;
- Architecture.Tests;
- restore;
- format verification;
- Release build;
- `eng/verify.ps1 -Configuration Release`;
- Gitleaks;
- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace checks for authorized new test files where needed;
- repository database-residue scan.

Required:

- build warnings/errors `0/0`;
- all permanent tests pass;
- skipped tests `0`;
- architecture tests pass;
- production dependency graph unchanged;
- package/reference/schema delta `0/0/0`;
- production delta `0`;
- provider/network calls `0`;
- real credentials `0`;
- temporary residue `0`.

---

## 21. Architecture protection

Prove production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No cycles.

No architecture-test changes in WP11.

No new project-reference edge.

Any newly discovered architecture-rule need is handed to WP12.

---

## 22. Security protection

Do not expose:

- real API keys;
- connection strings;
- credentials;
- sensitive local paths.

Tests may capture process output only as necessary for assertions, and must avoid printing secrets.

Gitleaks must pass.

Use dummy provider configuration only where mechanically required.

---

## 23. Git and GitHub mutation policy

Allowed GitHub mutations for WP11 only:

1. after starting-state gates pass, move #148 to In Progress;
2. after all acceptance gates pass, post bounded completion evidence;
3. close #148;
4. set Project #2 Status to Done.

Issue #149 and later issues are read-only.

Milestone #54 remains open.

Legacy milestone #44 remains unchanged.

Do not stage, commit, push, branch, open PRs, merge, tag, release, rebase, reset, or rewrite history.

---

## 24. Stop conditions

Stop and report **BLOCKED** if:

- starting governance is invalid;
- file manifest authority is insufficient;
- permanent validation requires production redesign;
- a new package/project reference/test project is required without authority;
- real provider access is required;
- real credentials are required;
- Worker validation cannot be made deterministic/offline under existing constraints;
- schema changes are required;
- WP12 architecture work must be started;
- canonical validation fails outside WP11 scope;
- an unexpected repository/GitHub mutation is discovered.

Report the smallest corrective authority required.

Do not broaden WP11.

---

## 25. Acceptance matrix

WP11 completes only if permanent validation proves, as applicable:

- pipeline DI registration;
- Release 1.2 dependency reuse;
- accepted lifetimes;
- graph resolution;
- no resolution-time pipeline execution;
- no resolution-time provider call;
- no resolution-time snapshot/catalog mutation;
- no resolution-time run-history persistence;
- no resolution-time database creation where applicable;
- valid dataset configuration;
- missing/invalid configuration rejection;
- invariant timestamp parsing;
- exact target/offset preservation;
- no `Pipeline:*` semantic configuration;
- one-shot Worker behavior;
- no loop/retry/scheduler;
- success exit `0`;
- `NewlyAccepted` success;
- `EquivalentExisting` success;
- empty success preserved;
- bounded failure non-zero;
- invalid configuration non-zero before execution;
- structured evidence surface reused;
- first-failure presentation;
- no fabricated later-stage evidence;
- provider/network calls `0`;
- real credentials `0`;
- schema v2;
- database residue `0`;
- production delta `0`;
- package/reference/schema delta `0/0/0`;
- architecture-test delta `0`;
- WP12 started `NO`;
- Release 1.4 implementation started `NO`.

---

## 26. Required execution report

Return a detailed numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial canonical baseline
7. Existing Infrastructure test inventory
8. Existing composition/Worker coverage inventory
9. WP10 exclusion/reconciliation
10. WP08 DI/configuration reconciliation
11. WP09 Worker reconciliation
12. Coverage matrix before WP11
13. Test isolation strategy
14. DI registration tests
15. Lifetime tests
16. Resolution-side-effect tests
17. Database-creation-on-resolution tests
18. Provider/network-isolation tests
19. Configuration tests
20. Culture/timezone parsing tests
21. One-shot Worker tests
22. Success exit-code tests
23. NewlyAccepted tests
24. EquivalentExisting tests
25. Empty-success decision/evidence
26. Invalid-configuration tests
27. Bounded-failure tests
28. First-failure evidence tests
29. Structured-evidence presentation tests
30. Real process-proof decision
31. SQLite cleanup
32. Exact files added/modified
33. Production delta
34. Package/reference/schema delta
35. Architecture-test delta
36. Test-count delta
37. Targeted Infrastructure evidence
38. Worker/process evidence
39. Full permanent test evidence
40. Canonical verification
41. Architecture validation
42. Security/offline evidence
43. Provider/network evidence
44. Database-residue evidence
45. Whitespace/diff evidence
46. Release 1.1/1.2 regression
47. WP08/WP09 regression
48. Mutation accounting
49. Git/GitHub protection
50. Planning protection
51. Findings/blockers
52. Acceptance matrix
53. Final repository/GitHub state
54. WP12 handoff
55. Final decision
56. Next authorized work package

End with exactly one terminal marker:

`RELEASE 1.3 WP11 COMPLETE`

or

`RELEASE 1.3 WP11 BLOCKED`

If complete, also state:

`NEXT AUTHORIZED WORK PACKAGE: WP12 — Architecture Evolution — GitHub issue #149`

Do not start WP12.

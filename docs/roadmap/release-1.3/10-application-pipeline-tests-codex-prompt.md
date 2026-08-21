# Release 1.3 WP10 — Application Pipeline Tests — Codex Execution Prompt

## 1. Role and objective

Execute **Release 1.3 — WP10: Application Pipeline Tests** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- GitHub issue: **#147**
- Milestone: **#54 — Phase 3 - Release 1.3: Research Pipeline Foundation**
- Recommended model: **GPT-5.6 Luna**

Your objective is to add the minimum permanent, deterministic, offline **Application-layer test coverage** required to prove the accepted Release 1.3 pipeline semantics, contracts, orchestration, validation, identity, provenance/evidence, and failure behavior.

WP10 is a permanent test package only.

WP10 must add **zero production behavior** and must not start WP11 composition/Worker validation.

---

## 2. Mandatory authority

Before any mutation, read completely and reconcile at minimum:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
5. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
6. WP01–WP09 authoritative prompt pairs and accepted execution results.
7. Current Application pipeline implementation, including:
   - `PipelineIdentity.cs`
   - `PipelineDefinition.cs`
   - `PipelineEvidence.cs`
   - `PipelineExecutionResult.cs`
   - `PipelineExecutionUseCase.cs`
   - `PipelineIdentityComputer.cs`
   - `PipelineValidation.cs`
   - `PipelineExecutionEvidence.cs`
8. Release 1.2 dataset contracts and Application tests reused by the pipeline.
9. Current `AIQuantTradingResearch.Application.Tests` inventory.
10. Current architecture tests, project references, packages, engineering scripts, and GitHub planning state.

Repository truth wins over assumptions.

Do not modify production code merely to make a test easier to write unless a genuine product defect is found. If a product defect blocks WP10, stop and report the smallest corrective authority required.

---

## 3. Starting-state gates

Before adding tests, prove and report:

- Release 1.2 remains closed.
- Release 1.3 milestone #54 is open.
- Issues #138–#146 are Closed/Done.
- WP10 issue #147 is Open/Backlog.
- WP11 issue #148 remains Open/Backlog and unchanged.
- #147 dependencies exactly match the authoritative graph: WP03, WP04, WP05, WP06.
- No WP11+ implementation has started.
- No Release 1.4 implementation has started.
- Current branch is `main`.
- `HEAD == origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- Existing cumulative Release 1.3 working-tree paths are expected and manifest-authorized.
- No unexpected generated SQLite/WAL/SHM/journal residue exists.
- SQLite schema remains version `2`.
- Canonical Release verification passes before mutation.

Expected permanent baseline before WP10:

- Domain.Tests: `11`
- Application.Tests: `60`
- Infrastructure.Tests: `87`
- Architecture.Tests: `13`
- Permanent total: `171`
- Skipped: `0`

Only after every starting-state gate passes may #147 move Backlog → In Progress.

---

## 4. WP10 ownership boundary

WP10 owns **pure Application pipeline tests**.

It must test only semantics and behavior that can be exercised through Application-owned contracts/use cases and hand-written deterministic test doubles.

WP10 must not:

- execute the real Worker process;
- test DI container registration;
- test Worker configuration parsing;
- test SQLite schema or persistence implementation;
- test provider/network behavior;
- use real credentials;
- create durable database files;
- add Infrastructure tests;
- add Worker test infrastructure;
- add architecture tests.

Those concerns belong to WP11/WP12 or existing Release 1.1/1.2 suites.

---

## 5. Production-code protection

Expected production delta:

- Domain: `0`
- Application: `0`
- Infrastructure: `0`
- Worker: `0`

If an Application production defect prevents a required test from passing, stop and report the defect. Do not silently repair production behavior inside WP10.

Expected package/reference delta: `0/0`.

Expected SQLite/schema delta: `0`.

---

## 6. Test design principles

Tests must be:

- permanent;
- deterministic;
- offline;
- culture-independent;
- timezone-independent;
- provider-independent;
- storage-independent;
- fast;
- isolated;
- explicit about semantic intent;
- free of sleep/timing dependencies;
- free of random nondeterminism unless a fixed deterministic seed/value is used for a non-semantic test helper.

Prefer hand-written local stubs/fakes over mocking frameworks unless the project already uses an accepted mocking package.

Do not add a package solely for mocking.

Use the existing test style and naming conventions.

---

## 7. Coverage inventory first

Before adding tests, inventory existing Application tests and map current coverage against Release 1.3 semantics.

Explicitly identify which required behaviors are:

- already covered by Release 1.2 tests;
- structurally enforced by constructors/invariants;
- uncovered and WP10-owned;
- reserved for WP11 Worker/composition validation.

Do not duplicate existing tests without a clear Release 1.3 semantic reason.

Report the coverage matrix before implementation.

---

## 8. Required identity coverage

Add permanent Application coverage for the accepted pipeline identity semantics where not already covered.

At minimum prove applicable cases:

- valid `PipelineDefinitionIdentity`;
- valid `PipelineExecutionIdentity`;
- malformed fingerprint rejection;
- scheme validation;
- Pipeline Definition and Execution identities are distinct typed concepts;
- equivalent semantic reruns produce equivalent Semantic Pipeline Execution Identity;
- `NewlyAccepted` vs `EquivalentExisting` does not change semantic execution identity;
- relevant dataset/source semantic changes produce distinguishable execution identity;
- operational data does not participate in semantic identity;
- canonical identity computation is culture-independent;
- canonical identity computation is local-timezone independent;
- fixed topology participates consistently in definition semantics;
- no mutable Pipeline Version semantics.

Do not expose internal implementation solely for testing if public/internal repository conventions already provide sufficient seams.

---

## 9. Required pipeline-definition and topology coverage

Prove:

- fixed five-stage topology;
- deterministic stage ordering;
- stage vocabulary contains only accepted Release 1.3 stages;
- a valid pipeline definition/request preserves the accepted `DatasetDefinition`;
- target and `[from,to)` boundaries are preserved exactly;
- stages are not dynamically configurable;
- request/definition identity consistency is enforced;
- invalid intervals remain rejected through existing dataset semantics.

Do not create tests for future DAG/plugin behavior.

---

## 10. Required orchestration coverage

Using hand-written Application test doubles, prove the fixed WP05 orchestration:

1. invokes the accepted materialization boundary;
2. invokes snapshot persistence after successful materialization;
3. invokes catalog registration after successful snapshot persistence;
4. returns success after all required stages complete;
5. stops after the first failure;
6. never invokes later dependencies after failure;
7. executes dependencies only once per pipeline execution;
8. does not perform provider/network behavior;
9. preserves exact semantic inputs passed through the pipeline.

Test use-case behavior, not DI/container behavior.

---

## 11. Success coverage

Add permanent tests for applicable successful outcomes:

### Newly accepted

Prove:

- terminal success;
- disposition `NewlyAccepted`;
- correct complete stage evidence;
- established dataset/source/snapshot identities present;
- pipeline definition/execution identities correct;
- provenance/evidence internally consistent.

### Equivalent existing

Prove:

- terminal success;
- disposition `EquivalentExisting`;
- semantic execution identity equal to equivalent first execution;
- no artificial semantic version/run identity drift;
- complete stage evidence remains consistent.

### Empty dataset

Prove:

- valid empty materialization is success;
- no sentinel observation;
- deterministic semantic identity/evidence;
- snapshot/catalog semantics remain representable;
- all required stages complete.

---

## 12. Failure/fail-stop coverage

Add permanent Application coverage for bounded first-failure behavior.

At minimum cover applicable cases:

- source/history unavailable;
- invalid source/dataset evidence;
- snapshot unavailable;
- snapshot integrity conflict;
- catalog unavailable;
- catalog integrity conflict.

For each applicable case prove:

- terminal failure;
- correct first failing stage;
- correct bounded failure category;
- no later-stage invocation;
- stage evidence is a valid prefix;
- only established semantic identities are present;
- no fabricated downstream identity;
- accepted immutable upstream evidence is not reinterpreted;
- no retry/repair/compensation behavior.

Do not reach into Infrastructure-specific exception codes.

---

## 13. Validation-contract coverage

Add tests for WP06 semantic validation where not already structurally covered.

At minimum consider:

- mismatched request/definition identity rejection;
- invalid stage-evidence prefix rejection;
- gaps in stage evidence rejection;
- evidence after failure rejection;
- success without required established output identity rejection;
- failure without failing stage rejection;
- inconsistent snapshot identity/version rejection where representable;
- invalid provenance/lineage relationships rejected;
- malformed identity fingerprints rejected.

If a scenario is impossible to construct because the contract already rejects it earlier, test the earliest authoritative rejection point rather than weakening invariants.

---

## 14. Structured evidence coverage

Test the WP07 `PipelineExecutionEvidence` projection/model as a pure Application concern.

Prove applicable cases:

- success projection exposes accepted semantic facts;
- equivalent rerun projection preserves execution identity;
- empty success projection is valid;
- failure projection preserves first-failure prefix;
- no later-stage evidence appears after failure;
- identities are exposed only when established;
- fixed stage ordering is preserved;
- provenance/lineage are reused, not rebuilt inconsistently;
- no operational timestamps/correlation/path/provider data appear in semantic evidence.

Do not test console formatting; that belongs to WP11/Worker validation if needed.

---

## 15. Unknown-failure behavior

Preserve WP06 unknown-failure rules.

Where safely testable through Application boundaries, prove that an unrelated unexpected exception from a dependency is not swallowed or silently reclassified by `PipelineExecutionUseCase`.

Do not add catch-all handling to production code.

---

## 16. Test doubles

Use small hand-written local doubles as needed for:

- materialization use case;
- snapshot store;
- catalog;
- other Application-owned seams already required by `PipelineExecutionUseCase`.

Test doubles should support:

- call-count inspection;
- controlled success/failure;
- exact argument capture;
- deterministic returned identities/evidence;
- throwing an unrelated exception for unknown-failure propagation proof.

Keep them local to the test file unless existing repository style strongly favors shared helpers.

Do not create a general-purpose mocking/test framework.

---

## 17. Fidelity coverage

Where Application contracts carry these values, prove exact preservation of:

- target text;
- `DateTimeOffset` semantic instant and original offset;
- decimal value;
- Dataset Definition Identity;
- Source State Identity;
- Dataset Snapshot Identity;
- Dataset Version;
- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- provenance;
- lineage.

Do not test SQLite serialization in WP10.

---

## 18. Semantic/operational separation coverage

Prove through the Application API surface that semantic results/evidence do not depend on:

- wall-clock time;
- process ID;
- machine identity;
- paths;
- connection strings;
- logging correlation ID;
- culture;
- local timezone;
- provider order;
- database natural order.

Where direct negative inspection is sufficient, do not invent operational APIs just to test their absence.

---

## 19. Explicit out of scope

WP10 MUST NOT implement or test through real infrastructure:

- Worker process execution;
- DI registration;
- Worker configuration parsing;
- SQLite schema/bootstrap;
- SQLite snapshot/catalog storage;
- network/provider calls;
- real credentials;
- scheduling;
- retries;
- circuit breakers;
- fallback providers;
- DAGs/plugins;
- parallel/streaming/distributed execution;
- checkpoints/resume;
- durable run history;
- metrics/tracing backends;
- feature engineering;
- model training/evaluation;
- MLOps;
- Release 1.4 behavior.

Do not start WP11.

---

## 20. Expected file boundary

Use `RELEASE_1.3_FILE_MANIFEST.md` as exact authority.

Preferred test file:

`tests/AIQuantTradingResearch.Application.Tests/PipelineApplicationTests.cs`

If the manifest permits multiple files and readability requires a split, keep the set minimal and explain why.

Do not modify existing Release 1.2 test files unless necessary and manifest-authorized.

Do not modify production files.

---

## 21. Test-count accounting

Report exact before/after/delta counts.

Expected starting baseline:

- Domain: `11`
- Application: `60`
- Infrastructure: `87`
- Architecture: `13`
- Total: `171`

WP10 should increase only **Application.Tests**.

Do not target an arbitrary number of new tests. Add the minimum permanent set that covers the accepted WP10 matrix without redundant cases.

---

## 22. Mandatory validation

After test implementation, run:

- targeted Domain.Tests;
- targeted Application.Tests;
- full permanent test suite;
- Architecture.Tests;
- restore;
- format verification;
- Release build;
- `eng/verify.ps1 -Configuration Release`;
- Gitleaks;
- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace checks for authorized untracked test files where needed;
- residue scan.

Required:

- build warnings/errors: `0/0`;
- all permanent tests pass;
- skipped tests: `0`;
- architecture tests pass;
- production dependency graph unchanged;
- database residue `0`;
- provider/network calls `0`;
- credentials `0`;
- package/reference/schema delta `0/0/0`;
- production-code delta `0`.

---

## 23. Architecture protection

Prove production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No production code should change in WP10, so architecture behavior should remain unchanged.

No new project references.

---

## 24. Git and GitHub mutation policy

Allowed GitHub mutations for WP10 only:

1. after starting-state gates pass, move #147 to In Progress;
2. after all WP10 acceptance gates pass, post bounded completion evidence;
3. close #147;
4. set Project #2 Status to Done.

WP11 issue #148 and later issues are read-only.

Milestone #54 remains open.

Legacy milestone #44 remains unchanged.

Do not stage, commit, push, branch, create PRs, merge, tag, or release.

---

## 25. Stop conditions

Stop and report **BLOCKED** if:

- starting governance is invalid;
- a required test requires production redesign;
- a required behavior contradicts accepted WP02–WP09 semantics;
- file manifest authority is insufficient;
- new packages/project references are required;
- real SQLite/provider/Worker execution is required for an Application semantic test;
- WP11 work must be started;
- canonical validation fails outside WP10 scope;
- an unexpected repository/GitHub mutation exists.

Report the smallest corrective authority required. Do not broaden WP10.

---

## 26. Acceptance matrix

WP10 completes only if permanent Application coverage proves, as applicable:

- pipeline identity validation;
- pipeline definition/request semantics;
- fixed topology/order;
- semantic identity determinism;
- relevant-change distinguishability;
- `NewlyAccepted`;
- `EquivalentExisting`;
- disposition-independent identity;
- empty success;
- fixed orchestration;
- exactly-once dependency calls;
- fail-stop behavior;
- source unavailable;
- invalid evidence;
- snapshot unavailable;
- snapshot conflict;
- catalog unavailable;
- catalog conflict;
- valid evidence prefix;
- established-identities-only;
- unknown exception propagation;
- structured evidence projection;
- provenance/lineage consistency;
- dataset/pipeline fidelity;
- semantic/operational separation;
- provider/network use `0`;
- SQLite/database use `0`;
- production delta `0`;
- package/reference/schema delta `0/0/0`;
- WP11 started `NO`;
- Release 1.4 implementation started `NO`.

---

## 27. Required execution report

Return a detailed numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial canonical baseline
7. Existing Domain test inventory
8. Existing Application test inventory
9. Release 1.3 coverage matrix before WP10
10. WP02 semantic coverage
11. WP03 identity/provenance/evidence coverage
12. WP04 contract coverage
13. WP05 orchestration coverage
14. WP06 validation/failure coverage
15. WP07 structured-evidence coverage
16. WP08/WP09 exclusion decision
17. Domain test delta decision
18. Application test design
19. Identity tests
20. Definition/topology tests
21. Successful-new tests
22. Equivalent-rerun tests
23. Empty-success tests
24. Fail-stop tests
25. Source failure tests
26. Snapshot failure/conflict tests
27. Catalog failure/conflict tests
28. Validation-invariant tests
29. Unknown-failure propagation tests
30. Structured-evidence tests
31. Fidelity tests
32. Semantic/operational separation tests
33. Test-double design
34. Exact files added/modified
35. Production delta
36. Package/reference/schema delta
37. Test-count delta
38. Targeted Domain evidence
39. Targeted Application evidence
40. Full permanent test evidence
41. Canonical verification
42. Architecture validation
43. Security/offline evidence
44. SQLite/database-use evidence
45. Provider/network-use evidence
46. Whitespace/diff evidence
47. Mutation accounting
48. Git/GitHub protection
49. Planning protection
50. Findings/blockers
51. Acceptance matrix
52. Final repository/GitHub state
53. WP11 handoff
54. Final decision
55. Next authorized work package

End with exactly one terminal marker:

`RELEASE 1.3 WP10 COMPLETE`

or

`RELEASE 1.3 WP10 BLOCKED`

If complete, also state:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Composition & Worker Validation — GitHub issue #148`

Do not start WP11.

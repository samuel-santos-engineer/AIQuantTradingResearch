# Release 1.5 WP10 — Application Experiment Tests

## GitHub Issue
`#177 — Release 1.5 WP10 — Application Experiment Tests`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP10 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Built-in experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP10 converts the accepted Release 1.5 Application semantics and the temporary evidence from WP04–WP09 into permanent deterministic offline Application tests.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- all accepted Release 1.5 Application experiment production files from WP04–WP07
- WP08 DI/configuration only as contextual evidence
- WP09 Worker behavior only as contextual evidence
- existing Release 1.4 Application feature semantic tests and conventions
- existing Application test project conventions
- WP01–WP09 completion evidence
- this WP10 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

WP10 is test-only. If a test reveals a production defect that cannot be tested without changing production code, stop and report the defect and smallest corrective authority rather than silently repairing production behavior.

---

## 2. Objective

Add the minimum high-value permanent deterministic offline Application test suite that proves the accepted Release 1.5 experiment semantics.

The suite must protect:

- experiment definition identity;
- experiment result identity;
- canonical fingerprint construction;
- deterministic summary computation;
- empty/single/non-empty behavior;
- exact decimal evidence;
- Feature Set identity binding;
- provenance and lineage;
- immutable evidence;
- validation precedence;
- bounded failure mapping;
- exact upstream request forwarding;
- exactly-once feature generation;
- exactly-once summary computation on success;
- fail-stop behavior;
- equivalent rerun identity;
- distinct Feature Set identity behavior;
- culture independence;
- numeric overflow behavior;
- unknown-defect propagation.

Do not test Worker/process composition here. WP11 owns that boundary.

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

- #168–#176: CLOSED / Done;
- #177 WP10: OPEN / Backlog;
- #178 WP11: OPEN / Backlog;
- #179–#180: OPEN / Backlog;
- milestone #46: OPEN with 4 open / 9 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected permanent baseline:

- Domain.Tests: 11;
- Application.Tests: 86;
- Infrastructure.Tests: 104;
- Architecture.Tests: 13;
- total: 214;
- SQLite schema: v2.

Expected cumulative accepted Release 1.5 work remains unstaged.

If #176 is not Closed/Done or #178 has started, stop before mutation.

---

## 4. WP10 Lifecycle Start

After starting-state gates pass:

- move only #177 Project #2 Status from Backlog to In Progress.

Read back the state.

Do not mutate #178.

#178 must remain OPEN / Backlog throughout WP10.

---

## 5. Hard File Boundary

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

WP10 must modify/add only the manifest-authorized Application test path.

Expected logical path:

`tests/AIQuantTradingResearch.Application.Tests/ExperimentApplicationTests.cs`

Use the exact manifest path.

Expected WP10 deltas:

- Application permanent tests: +1 test file;
- Domain tests: 0 files;
- Infrastructure tests: 0 files;
- Architecture tests: 0 files;
- production files: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not stage or commit.

---

## 6. Production Code Freeze

WP10 does not authorize production changes.

Do not modify:

- Experiment identity implementation;
- model/contracts;
- summary computer;
- validator;
- use case;
- DI;
- Worker configuration;
- Worker execution;
- Domain;
- Infrastructure;
- schema.

If accepted behavior fails a correctly constructed permanent test, stop and report the production defect.

Do not weaken a test to accommodate a defect.

---

## 7. Existing Test Convention Reuse

Inspect existing Release 1.4 Application feature semantic tests before writing WP10 tests.

Reuse repository conventions for:

- xUnit naming;
- test fixture structure;
- fake/stub implementations;
- deterministic identity construction;
- culture switching/restoration;
- exception assertions;
- immutable collection assertions.

Avoid unnecessary new helper frameworks.

No new package.

---

## 8. Test Quality Rules

Every permanent test must protect a meaningful Release 1.5 semantic contract.

Avoid:

- implementation-detail-only tests;
- duplicate assertions with no new semantic value;
- tautological tests that reproduce production implementation;
- fragile string snapshots of unrelated formatting;
- filesystem/network dependencies;
- timing-sensitive behavior.

Tests must be deterministic and offline.

---

## 9. Experiment Definition Tests

Permanently prove the sole built-in definition:

`simple-return-descriptive-summary-v1`

Protect at minimum:

- exact semantic definition identifier;
- deterministic Experiment Definition Identity;
- scheme `aiq-experiment-identity-v1`;
- valid SHA-256 external fingerprint shape;
- repeated construction produces equivalent identity.

If constructor invariants make unsupported definitions unconstructable, assert the public invariant where useful rather than using reflection or invalid hacks.

---

## 10. Canonical Identity Tests

Protect externally observable canonical identity behavior without duplicating the implementation algorithm wholesale.

Cover:

- definition fingerprint determinism;
- result fingerprint determinism;
- 64 lowercase hexadecimal fingerprint form;
- equivalent semantic evidence → same identity;
- changed semantic evidence → changed identity where constructible;
- exact Feature Set Identity participates in Experiment Result Identity.

Use known deterministic expected fingerprint vectors if WP03/WP04 accepted semantics make stable vectors practical.

At least one exact expected fingerprint vector is strongly preferred for definition identity and one for result identity, if it can be derived independently from the frozen WP03 canonical specification.

---

## 11. Canonical Vector Independence

If exact fingerprint vectors are added:

- derive them from the WP03 specification, not by calling the production identity method and asserting against itself;
- hard-code only the expected canonical result;
- document enough test input to make the vector reviewable.

Do not create a second production canonicalizer.

A tiny test-only independent vector helper is acceptable only if needed and simpler than hard-coded known vectors.

---

## 12. Summary Computation Tests

Test `IExperimentSummaryComputer` / `SimpleReturnDescriptiveSummaryComputer` directly.

Cover:

- empty Feature Set;
- one feature value;
- multiple feature values;
- negative and positive values;
- exact decimal arithmetic;
- minimum/maximum;
- count;
- no rounding/floating-point conversion.

For a representative non-empty set, verify exact expected mean/min/max.

---

## 13. Empty Feature Set

Prove:

- count = 0;
- aggregate values are absent;
- no fake zero mean/min/max;
- computation succeeds.

Where result construction is tested, prove the empty result receives a deterministic Experiment Result Identity bound to its exact Feature Set identity.

No global empty sentinel.

---

## 14. Single Feature Value

For one feature value, prove:

- count = 1;
- mean = exact value;
- minimum = exact value;
- maximum = exact value.

Preserve decimal exactness.

---

## 15. Feature-Generation Empty Cases

At the integration/use-case level, distinguish predecessor inputs that naturally produce empty Feature Sets:

- empty snapshot;
- one-observation snapshot.

Where constructible through fakes, prove both can lead to successful count-zero experiment evidence.

If their Feature Set identities differ, their Experiment Result identities must differ.

---

## 16. Ordering / Evidence Preservation

The descriptive summary is order-insensitive numerically, but Release 1.5 consumes the complete accepted Feature Set evidence.

Tests should prove that:

- no feature value is filtered;
- no feature value is deduplicated;
- count equals exact Feature Set cardinality.

Do not invent ordering semantics beyond the frozen authorities.

---

## 17. Decimal Determinism

Use only `decimal`.

Test representative values with:

- positive;
- negative;
- zero;
- differing scales/trailing zeros where accepted model construction permits.

Prove semantic identity/computation is culture independent and canonical decimal normalization behaves as frozen.

Do not use `double`/`float` for expected values.

---

## 18. Numeric Overflow

Construct a valid evidence case that causes decimal arithmetic overflow in the summary computation where practical.

Prove:

- direct summary computation naturally throws `OverflowException`;
- at the governed integration boundary, decimal overflow maps to the accepted `InvalidNumericEvidence` failure;
- no partial successful Experiment Result is returned;
- no Experiment Result Identity is fabricated.

Do not broadly normalize unrelated exceptions.

---

## 19. Provenance Tests

Protect the accepted provenance chain.

For successful result evidence, assert exact references to:

- Experiment Definition Identity;
- Feature Set Identity;
- predecessor snapshot identity/version where exposed;
- accepted upstream provenance evidence.

Do not fabricate provider/storage-specific provenance.

---

## 20. Lineage Tests

Protect acyclic lineage references through accepted model fields.

Expected conceptual chain:

source state
→ dataset/research dataset
→ snapshot/version
→ feature definition
→ Feature Set
→ experiment definition
→ experiment result

Test the exact public evidence exposed by the accepted model.

Do not invent graph traversal infrastructure.

---

## 21. Immutability Tests

Prove accepted result/evidence objects do not change when caller-owned input collections are mutated after construction, where applicable.

Protect:

- Feature Set evidence snapshotting inherited from Release 1.4 as consumed by experiment semantics;
- experiment provenance/lineage collections if any;
- result evidence collections if any.

Do not use reflection to mutate private state.

---

## 22. Request Validation Tests

Test the WP06 validator through its public contract.

Protect deterministic precedence beginning with invalid request.

Cover constructible invalid request cases such as:

- null request where API permits;
- invalid/missing required predecessor evidence through accepted failure-return path;
- incoherent exact snapshot/version evidence if constructible.

Respect constructor-blocked invariants.

---

## 23. Unsupported Definition Semantics

If unsupported experiment definitions are unconstructable through the accepted immutable public model, record that invariant in the test/report rather than using reflection, serialization tricks, or production changes.

If a public seam permits a safe unsupported-definition fake/value, test mapping to the accepted failure category.

Do not weaken type invariants merely to create a test.

---

## 24. Feature Set Evidence Validation

Test accepted Feature Set/predecessor validation through public boundaries.

Protect:

- exact identity coherence;
- exact snapshot/version binding;
- invalid predecessor evidence fails before summary computation;
- invalid evidence produces no Experiment Result Identity.

Reuse legitimate Release 1.4 feature evidence construction.

---

## 25. Feature Set Identity Integrity Conflict

Where public construction permits contradictory Feature Set identity/content evidence, prove it maps to the accepted integrity-conflict behavior.

If Release 1.4 immutable constructors make the contradiction unconstructable, document the constructor-blocked invariant.

Do not use reflection or corrupt persistence.

---

## 26. Validation Precedence

Permanently protect the WP06 precedence:

1. Invalid request
2. Unsupported experiment definition
3. Invalid Feature Set/predecessor evidence
4. Invalid numeric evidence
5. Feature Set identity integrity conflict

Test all practically constructible competing-failure cases.

Where an earlier/later combination cannot be constructed due to immutable invariants, report it explicitly.

The goal is fail-stop semantic precedence, not artificial invalid-object manufacture.

---

## 27. Use-Case Exact Upstream Forwarding

Test `ExperimentGenerationUseCase` with controlled fakes.

Prove it forwards the exact accepted request to `IFeatureGenerationUseCase`, including:

- exact snapshot identity;
- exact snapshot version;
- exact built-in feature definition semantics required by Release 1.5 integration.

Do not touch SQLite.

---

## 28. Exactly-Once Feature Generation

For successful and relevant failure paths, count calls to `IFeatureGenerationUseCase`.

Prove:

- valid experiment request invokes upstream feature generation exactly once;
- validation failure before upstream invocation invokes it zero times;
- no retry/repeat.

---

## 29. Exactly-Once Summary Computation

Count calls to `IExperimentSummaryComputer`.

Prove:

- successful valid Feature Set → exactly one call;
- request validation failure → zero calls;
- bounded upstream feature failure → zero calls;
- returned Feature Set validation failure → zero calls;
- no retry/repeat.

---

## 30. Bounded Upstream Failure Mapping

Using controlled `IFeatureGenerationUseCase` fakes, permanently test applicable mapping for:

- snapshot/Feature Set not found;
- dependency unavailable;
- invalid upstream evidence;
- invalid numeric evidence;
- integrity conflict.

Use exact accepted Release 1.5 failure names.

For every bounded failure:

- no summary computation where failure precedes it;
- no successful Experiment Result;
- no fabricated Experiment Result Identity.

---

## 31. Equivalent Rerun

Run the Application use case twice with semantically identical controlled upstream evidence.

Prove:

- same Experiment Definition Identity;
- same Feature Set Identity;
- same summary evidence;
- same Experiment Result Identity.

Do not include invocation time/correlation/process metadata.

---

## 32. Distinct Feature Set Identity

Construct two valid Feature Sets that produce equal descriptive summary values but have different valid Feature Set identities because their accepted upstream evidence differs.

Prove:

- summaries may be numerically equal;
- Experiment Result identities are different.

This is a core Release 1.5 semantic guarantee.

---

## 33. Culture Independence

Execute representative identity/computation/use-case tests under at least:

- invariant/default culture;
- `pt-BR`;
- one additional culture if existing test conventions make it cheap.

Prove semantic results and identities are unchanged.

Restore culture in `finally`/disposable scope so tests cannot leak global state.

Avoid parallel culture interference using existing repository conventions.

---

## 34. Unknown Exception Propagation

Controlled fakes must prove unknown defects are not normalized.

At minimum, where public seams permit:

- unknown exception from `IFeatureGenerationUseCase` propagates;
- unknown exception from `IExperimentSummaryComputer` propagates.

Do not catch and compare message text unnecessarily.

Use a dedicated test exception type if useful.

---

## 35. Failure Produces No Partial Result

Across governed failures, assert the returned contract contains only the bounded failure evidence allowed by the accepted model.

No:

- summary on pre-summary failure;
- Experiment Result Identity;
- partial provenance;
- partial success object.

Follow actual contract shape.

---

## 36. No Worker / DI / SQLite Tests

WP10 must not test:

- `Program.cs`;
- `ExperimentExecution`;
- process exit codes;
- DI registration/lifetimes;
- database creation;
- SQLite snapshot integration;
- Worker configuration.

WP11 owns composition and Worker validation.

Application tests may use in-memory fakes only.

---

## 37. No Network / Provider

No test may access:

- Twelve Data;
- HTTP;
- DNS;
- external service;
- real credentials.

Provider/network activity: 0.

---

## 38. Test Count Discipline

Do not target an arbitrary test-count increase.

Add the smallest coherent suite that permanently covers the semantic matrix.

Report:

- Application baseline count;
- Application final count;
- delta;
- total baseline/final/delta.

Domain test delta must be 0.

Infrastructure/Architecture test delta must be 0.

---

## 39. Technical Validation

Run targeted WP10 tests first.

Then run:

`eng/verify.ps1 -Configuration Release`

Require:

- Domain.Tests: 11/11;
- Application.Tests: all pass with the new WP10 delta;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- skipped: 0;
- warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of the WP10 test file and relevant untracked governance artifacts.

Require:

- database/WAL/SHM/journal residue: 0;
- generated residue: 0;
- network/provider activity: 0;
- real credentials: 0.

---

## 40. Regression Protection

All predecessor tests must remain green.

Specifically preserve:

- Release 1.1 persistence behavior;
- Release 1.2 dataset identity/snapshot semantics;
- Release 1.3 five-stage pipeline;
- Release 1.4 deterministic feature semantics;
- Release 1.5 WP04–WP09 production behavior.

No production changes are authorized to make regressions pass.

---

## 41. Architecture / Schema Protection

Confirm:

- production graph unchanged;
- cycles 0;
- packages/references unchanged;
- SQLite schema v2;
- no experiment persistence;
- no Release 1.6 behavior.

Architecture.Tests remain 13/13 with zero WP10 delta.

---

## 42. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- begin WP11;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only the exact manifest-authorized WP10 Application test path.

---

## 43. Authorized GitHub Mutation Budget

At WP10 start after gates pass:

1. #177 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP10 completion-evidence comment to #177;
3. close #177 as completed;
4. set #177 Project Status to Done.

Do not mutate #178.

Milestone #46 remains OPEN.

---

## 44. Completion Gate

WP10 may close only if:

- #176 is Closed/Done;
- #177 was In Progress during execution;
- #178 remains Open/Backlog;
- exact manifest path accounting passes;
- production delta is 0;
- permanent Application tests protect the accepted semantic matrix;
- definition/result identity determinism is tested;
- canonical fingerprint shape/vectors are protected where practical;
- empty/single/non-empty computation is tested;
- exact decimal behavior is tested;
- Feature Set identity binding/distinctness is tested;
- provenance/lineage/immutability are tested where exposed;
- validation precedence is tested for constructible states;
- exact upstream forwarding is tested;
- exactly-once feature/summary calls are tested;
- bounded failures are tested;
- overflow behavior is tested;
- culture independence is tested;
- unknown exceptions propagate;
- failures fabricate no result identity;
- Worker/DI/SQLite permanent tests were not added;
- Domain/Infrastructure/Architecture test deltas are 0;
- package/project/reference/schema deltas are 0;
- SQLite remains v2;
- all permanent tests pass;
- Architecture.Tests 13/13;
- warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- provider/network activity 0;
- Release 1.6 work 0.

If any gate fails, do not close #177 or mark Done.

---

## 45. Completion Evidence Comment

On success, post concise evidence to #177 covering:

- exact test file;
- Application test count before/after/delta;
- total test count before/after/delta;
- identity/canonical coverage;
- summary computation coverage;
- empty/single/non-empty coverage;
- Feature Set binding and distinct-identity coverage;
- provenance/lineage/immutability coverage;
- validation/failure precedence;
- upstream forwarding and call counts;
- overflow/culture/unknown-exception behavior;
- no production/Worker/Infrastructure/schema/package/reference changes;
- no network/provider;
- Architecture.Tests 13/13;
- canonical verification/Gitleaks/whitespace PASS;
- #178 preserved Open/Backlog.

---

## 46. Final Read-Back

After successful closure verify:

- #177: CLOSED / Done;
- #178: OPEN / Backlog;
- #179–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 3 open / 10 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 state accurately.

---

## 47. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #176 is not Closed/Done;
- #178+ started unexpectedly;
- WP10 manifest ownership is ambiguous;
- a required semantic test reveals a production defect;
- testing requires production mutation;
- accepted public contracts cannot construct a requested invalid state;
- premature WP11+ implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- package/project/reference/schema mutation would be required.

For constructor-blocked invalid states, document the invariant rather than treating it as a blocker unless the accepted authority explicitly requires a constructible test.

Report the smallest corrective authority required for genuine blockers.

---

## 48. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP10 manifest path;
6. existing test conventions reused;
7. permanent test inventory;
8. identity/canonical vector evidence;
9. summary computation evidence;
10. empty/single/non-empty evidence;
11. Feature Set identity binding/distinctness;
12. provenance/lineage/immutability;
13. validation precedence;
14. exact upstream forwarding;
15. feature-generation call counts;
16. summary-computation call counts;
17. bounded failure mapping;
18. numeric overflow;
19. culture independence;
20. unknown exception propagation;
21. constructor-blocked invariants;
22. no-partial-result evidence;
23. test count delta;
24. full validation;
25. production/architecture/schema protection;
26. security/whitespace/residue/network evidence;
27. GitHub lifecycle mutations;
28. final #177/#178/milestone state;
29. findings/blockers;
30. next authorized WP.

---

## 49. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP10 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Composition & Worker Validation — GitHub issue #178`

Do not begin WP11.

If blocked, end:

`RELEASE 1.5 WP10 BLOCKED`

and identify the smallest corrective authority required.

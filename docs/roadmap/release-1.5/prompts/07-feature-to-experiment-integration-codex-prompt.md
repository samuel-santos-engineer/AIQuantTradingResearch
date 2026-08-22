# Release 1.5 WP07 — Feature-to-Experiment Integration

## GitHub Issue
`#174 — Release 1.5 WP07 — Feature-to-Experiment Integration`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP07 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP07 composes the already-accepted Release 1.4 feature-generation capability with the Release 1.5 WP04 model/contracts and identity machinery, WP05 deterministic summary computation, and WP06 validation/failure semantics into one bounded Application-owned experiment-generation use case.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP04 experiment model/contracts and canonical identity implementation
- WP05 `SimpleReturnDescriptiveSummaryComputer.cs`
- WP06 `ExperimentGenerationValidator.cs`
- accepted Release 1.4 feature-generation contracts/use case/validator/computer
- relevant Release 1.2 dataset snapshot contracts
- relevant Release 1.3 pipeline boundaries
- Application architecture, error-handling, DI, and observability authorities
- WP01–WP06 completion evidence
- this WP07 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If the accepted contracts cannot be composed without changing frozen semantics or taking ownership from WP08/WP09, stop and request the smallest corrective authority.

---

## 2. Objective

Implement exactly the manifest-authorized Application integration boundary for:

`simple-return-descriptive-summary-v1`

The intended semantic flow is:

`exact experiment request`
`→ existing Release 1.4 feature generation`
`→ accepted Feature Set evidence`
`→ WP06 validation`
`→ WP05 deterministic summary computation`
`→ canonical Experiment Result identity/provenance`
`→ immutable governed experiment result`

WP07 must provide one bounded synchronous use-case execution.

It must not add persistence, DI registration, Worker execution, scheduling, retry, provider acquisition, or permanent tests.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Expected lifecycle:

- #168–#173: CLOSED / Done;
- #174 WP07: OPEN / Backlog;
- #175 WP08: OPEN / Backlog;
- #176–#180: OPEN / Backlog;
- milestone #46: OPEN with 7 open / 6 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- SQLite schema: v2.

Expected accepted WP04–WP06 production artifacts include the experiment identity/model/contracts, summary computer, and validator.

If #173 is not Closed/Done or #175 has started, stop before mutation.

---

## 4. WP07 Lifecycle Start

After starting-state gates pass:

- move only #174 Project #2 Status from Backlog to In Progress.

Read back the state.

If #174 is already In Progress solely because this exact WP07 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #175.

#175 must remain OPEN / Backlog throughout WP07.

---

## 5. Mandatory Contract Reconciliation

Before writing code, inspect the actual accepted interfaces and record:

- `IExperimentGenerationUseCase`;
- experiment request type;
- experiment result/failure type;
- `IExperimentSummaryComputer`;
- WP06 validator interface/API;
- Release 1.4 `IFeatureGenerationUseCase`;
- Release 1.4 feature-generation request;
- Release 1.4 feature-generation success/failure vocabulary;
- Feature Set evidence type;
- WP04 Experiment Definition Identity API;
- WP04 Experiment Result Identity API;
- WP04 result/provenance/lineage constructors/factories.

Do not guess method signatures.

Do not redesign WP04–WP06 merely to simplify orchestration.

A minimal shared-contract refinement is allowed only if explicitly manifest-authorized and necessary to compose already-frozen semantics.

---

## 6. Exact Upstream Boundary

Release 1.5 must consume the existing Release 1.4 feature-generation capability rather than recomputing features.

The experiment request must map deterministically to the exact Release 1.4 feature-generation request required to obtain:

`simple-return-lag-1-v1`

for the exact snapshot identity/version governed by the accepted Release 1.5 request contract.

Do not:

- query `IDatasetSnapshotStore` directly if Release 1.4 already owns that lookup;
- duplicate feature computation;
- duplicate feature validation;
- access SQLite directly;
- call Twelve Data or another provider.

Release 1.4 remains the authoritative upstream feature boundary.

---

## 7. Request Validation First

Before invoking upstream feature generation, perform the WP06 request/definition validation required by the accepted validator API.

Requirements:

- invalid request fails before feature-generation invocation;
- unsupported experiment definition fails before feature-generation invocation when representable;
- no upstream work occurs after an earlier governed request failure.

If WP06 separates request validation from Feature Set validation, preserve that separation.

Do not perform summary computation before request validation.

---

## 8. Exactly One Upstream Feature Invocation

For a valid experiment request, invoke the accepted Release 1.4 feature-generation use case exactly once.

No:

- retry;
- fallback;
- loop;
- duplicate invocation;
- provider bypass;
- alternate feature path.

Forward the exact snapshot identity/version and built-in feature definition required by the accepted contracts.

Record the one-call property in validation evidence.

---

## 9. Upstream Failure Mapping

Map only known bounded Release 1.4 failures into the already-accepted Release 1.5 experiment failure vocabulary.

Reconcile exact enum/type names from repository truth.

Conceptually:

- invalid upstream request/evidence contradiction → corresponding governed experiment invalid/integrity failure as frozen;
- unsupported feature definition, if impossible because WP07 constructs the built-in definition internally, should remain impossible rather than exposed;
- snapshot NotFound → Release 1.5 predecessor/Feature Set NotFound category;
- dependency unavailable → Release 1.5 DependencyUnavailable;
- invalid snapshot/feature evidence → Release 1.5 invalid predecessor evidence category;
- invalid numeric feature evidence → governed invalid numeric evidence where the accepted semantics require it;
- integrity conflict → Release 1.5 IntegrityConflict.

Do not collapse all upstream failures into one generic failure.

Do not fabricate Feature Set or Experiment Result evidence on upstream failure.

---

## 10. Unknown Upstream Defects

Unknown exceptions from the Release 1.4 feature-generation use case must propagate.

Do not catch all exceptions.

Do not normalize unknown defects as:

- DependencyUnavailable;
- InvalidRequest;
- InvalidEvidence;
- IntegrityConflict.

No retry or fallback.

---

## 11. Feature Set Validation

After successful upstream feature generation:

- obtain the immutable Feature Set evidence;
- validate it using the accepted WP06 validation boundary before summary computation;
- preserve exact Feature Set Identity and predecessor provenance/lineage.

If validation fails:

- stop;
- do not invoke summary computation;
- do not construct Experiment Result Identity;
- return only the governed failure evidence.

Do not reimplement WP06 rules inside the use case beyond necessary sequencing.

---

## 12. Exactly One Summary Computation

After successful request and Feature Set validation, invoke:

`IExperimentSummaryComputer`

exactly once.

Use the accepted Feature Set evidence unchanged.

Do not:

- filter;
- sort;
- deduplicate;
- clone into a semantically different set;
- recompute features.

Empty and single-value Feature Sets remain valid inputs.

---

## 13. Numeric Failure Mapping

WP05 allows natural decimal `OverflowException`.

At the integration boundary, map only the numeric exception type explicitly governed by WP06 to the accepted `InvalidNumericEvidence` or exact repository equivalent.

Requirements:

- no partial Experiment Result;
- no Experiment Result Identity;
- no retry/fallback;
- no binary-floating-point workaround;
- unknown computation exceptions propagate.

If WP06 exposes a helper for this mapping, reuse it.

Do not duplicate broader failure logic.

---

## 14. Summary Evidence Validation

If WP06 exposes post-computation summary validation, invoke it exactly where required.

Valid states:

- empty: count 0, aggregates absent;
- single/non-empty: count > 0, aggregates all present.

If WP04 constructors make malformed summary evidence impossible, do not add redundant validation merely for ceremony.

Record the actual accepted boundary.

---

## 15. Experiment Definition Identity

Use WP04 canonical identity machinery to obtain the Experiment Definition Identity for exactly:

`simple-return-descriptive-summary-v1`

Do not hard-code a fingerprint.

Do not duplicate SHA-256/canonical serialization.

The definition identity must be semantic and operational-metadata-independent.

---

## 16. Experiment Result Identity

Only after all required evidence is valid and summary computation succeeds, compute the Experiment Result Identity through WP04 canonical machinery.

The identity must bind exactly the frozen WP03 semantic payload, including:

- Experiment Definition Identity;
- exact Feature Set Identity;
- count;
- aggregate-presence marker;
- mean/minimum/maximum evidence as applicable.

Do not include:

- invocation time;
- duration;
- correlation ID;
- process/machine identity;
- paths;
- credentials;
- logs;
- retry/scheduling state.

Do not compute a result identity on failure.

---

## 17. Result Provenance and Lineage

Construct only the immutable provenance/lineage evidence required by WP04/WP03.

Preserve:

`source state → dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

Reuse accepted predecessor references.

Do not copy provider/storage operational details into experiment identity.

Do not create generalized graph infrastructure.

No cycles.

---

## 18. Successful Result Construction

On complete success, return the accepted immutable experiment result containing:

- exact Experiment Definition Identity;
- exact Feature Set Identity;
- deterministic summary evidence;
- canonical Experiment Result Identity;
- required provenance/lineage.

Use WP04 constructors/factories.

No mutable result.

No persistence side effect.

---

## 19. Empty Feature Set

A successful empty upstream Feature Set must produce:

- successful experiment result;
- count 0;
- absent mean/minimum/maximum;
- deterministic Experiment Result Identity bound to the exact Feature Set Identity.

Do not classify empty as NotFound or invalid.

---

## 20. Single and Non-Empty Feature Sets

Single Feature Set:

- count 1;
- mean/min/max equal the exact feature value.

Non-empty Feature Set:

- use WP05 exact decimal summary evidence.

WP07 does not recompute or reinterpret the statistics.

---

## 21. Equivalent Re-execution

Equivalent accepted semantic input must produce the same Experiment Result Identity.

The use case must introduce no operational identity component.

Different Feature Set identities remain Experiment Result identity-distinct even if their summaries are numerically equal.

---

## 22. Fail-Stop Ordering

The orchestration order must be explicit and deterministic.

Conceptually:

1. request validation;
2. upstream feature-generation invocation;
3. upstream bounded failure mapping;
4. Feature Set validation;
5. summary computation;
6. governed numeric-failure mapping;
7. any required summary/result coherence validation;
8. Experiment Definition/Result identity establishment;
9. immutable successful result construction.

Reconcile exact WP06 API and report the final implemented order.

At the first governed failure:

- stop;
- do not execute later stages;
- return only the corresponding failure.

Unknown defects propagate.

---

## 23. No Release 1.3 Pipeline Mutation

This use case is separate from the fixed Release 1.3 five-stage pipeline.

Do not:

- add an experiment sixth stage;
- automatically invoke experiments after pipeline execution;
- modify pipeline stage definitions;
- modify pipeline identity.

Experiment execution remains separately requested and one-shot.

---

## 24. No DI / Worker Changes

WP07 must not register services or modify Worker behavior.

Do not edit:

- Application `DependencyInjection.cs`;
- Worker configuration;
- Worker `Program.cs`;
- Worker execution helpers.

WP08 owns DI/configuration.

WP09 owns Worker execution.

---

## 25. No Persistence / Provider Changes

Do not introduce:

- experiment persistence;
- experiment registry/history/cache;
- feature persistence expansion;
- SQL;
- migrations;
- schema changes;
- provider calls;
- credentials.

SQLite remains schema v2.

Infrastructure production delta: 0.

---

## 26. Explicit Deferrals

Do not implement:

- additional experiments/statistics;
- experiment persistence/registry/history;
- notebooks/workspaces;
- visualization/API;
- strategies/signals/backtesting;
- portfolio/risk;
- AI/ML/MLOps;
- live acquisition orchestration;
- scheduling/retries/recovery/checkpoints;
- plugins/generalized DAGs;
- distributed execution;
- telemetry backends;
- Release 1.6 work.

---

## 27. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

Expected logical WP07 production path:

`src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationUseCase.cs`

Use the exact manifest path.

Before mutation:

1. enumerate exact WP07-authorized paths;
2. verify no WP08+ path is included;
3. verify no premature integration implementation exists.

A minimal refinement to a shared WP04/WP06 Application contract is permitted only if explicitly authorized by the manifest and strictly necessary to compose frozen semantics.

Expected deltas:

- Application production: exact WP07 manifest path(s);
- Domain: 0;
- Infrastructure: 0;
- Worker: 0;
- permanent tests: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not stage or commit.

---

## 28. No Permanent Tests in WP07

Permanent Application experiment tests belong to WP10.

Do not add permanent tests.

Use a removable offline probe if necessary.

The probe may use deterministic hand-written doubles for:

- feature-generation use case;
- summary computer;
- validator seams.

No SQLite/provider/network.

Remove the probe before final validation.

Permanent test count remains 214.

---

## 29. Required Temporary Acceptance Matrix

Prove all applicable cases through accepted public seams:

1. valid non-empty request invokes upstream feature generation exactly once;
2. valid empty Feature Set succeeds;
3. valid single Feature Set succeeds;
4. valid non-empty Feature Set succeeds;
5. summary computer invoked exactly once after valid Feature Set;
6. equivalent rerun produces identical Experiment Result Identity;
7. different Feature Set identity with equal summary remains result-distinct;
8. invalid request invokes neither feature generation nor summary computation;
9. upstream NotFound maps correctly and summary is not invoked;
10. upstream DependencyUnavailable maps correctly and summary is not invoked;
11. upstream invalid evidence maps correctly;
12. upstream integrity conflict maps correctly;
13. invalid returned Feature Set evidence fails before summary;
14. decimal overflow maps to governed numeric failure with no result identity;
15. unknown upstream exception propagates;
16. unknown summary-computation exception propagates;
17. no provider/storage interaction occurs.

If a case is impossible by construction, report the invariant rather than weakening accepted models.

---

## 30. Call-Count Evidence

The removable probe must explicitly verify, where applicable:

- feature generation: exactly 1 invocation on valid request;
- feature generation: 0 on invalid request;
- summary computation: exactly 1 after valid Feature Set;
- summary computation: 0 after upstream/validation failure.

No retries.

No duplicate calls.

---

## 31. Technical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected final baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of WP07 files and relevant untracked governance artifacts.

Require:

- temporary probe removed;
- database/WAL/SHM/journal residue: 0;
- generated residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 32. Architecture Validation

Confirm graph remains:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

Require:

- unexpected edges: 0;
- cycles: 0;
- no Application → Infrastructure/Worker;
- no new package/reference;
- no schema change.

Architecture.Tests remain 13/13.

---

## 33. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- mutate packages/projects/references/schema;
- begin WP08;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only exact manifest-authorized WP07 Application paths.

---

## 34. Authorized GitHub Mutation Budget

At WP07 start after gates pass:

1. #174 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP07 completion-evidence comment to #174;
3. close #174 as completed;
4. set #174 Project Status to Done.

Do not mutate #175.

Milestone #46 remains OPEN.

---

## 35. Completion Gate

WP07 may close only if:

- #173 is Closed/Done;
- #174 was In Progress during execution;
- #175 remains Open/Backlog;
- exact manifest path accounting passes;
- accepted Release 1.4 feature-generation use case is reused;
- request validation precedes upstream invocation;
- valid request invokes feature generation exactly once;
- Feature Set validation precedes summary computation;
- valid Feature Set invokes summary computation exactly once;
- NotFound/DependencyUnavailable/invalid evidence/integrity failures map distinctly as governed;
- decimal overflow maps to governed numeric failure;
- unknown upstream/computation defects propagate;
- Experiment Result identity is established only after complete valid evidence;
- empty/single/non-empty success passes;
- equivalent rerun identity is stable;
- different Feature Set identities remain result-distinct;
- no WP08 DI or WP09 Worker work exists;
- Domain/Infrastructure/Worker/test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- build warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- provider/network execution 0;
- Release 1.6 work 0.

If any gate fails, do not close #174 or mark Done.

---

## 36. Completion Evidence Comment

On success, post concise evidence to #174 covering:

- exact Application integration files;
- final orchestration order;
- exact Release 1.4 feature-generation reuse;
- one-call upstream/summary evidence;
- request and Feature Set validation ordering;
- NotFound/DependencyUnavailable/invalid evidence/integrity mapping;
- decimal overflow mapping;
- unknown defect propagation;
- canonical Experiment Result identity/provenance construction;
- empty/single/non-empty behavior;
- equivalent rerun and predecessor-distinct identity;
- no DI/Worker/persistence/provider/schema work;
- zero Domain/Infrastructure/Worker/test/package/reference/schema delta;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #175 preserved Open/Backlog.

---

## 37. Final Read-Back

After successful closure verify:

- #174: CLOSED / Done;
- #175: OPEN / Backlog;
- #176–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 6 open / 7 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 artifacts accurately.

---

## 38. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #173 is not Closed/Done;
- #175+ started unexpectedly;
- WP07 manifest ownership is ambiguous;
- Release 1.4 feature-generation contracts cannot be composed with WP04–WP06 without semantic redesign;
- a necessary shared-contract refinement is not manifest-authorized;
- satisfying WP07 requires DI/Worker/persistence/schema work;
- broad unknown-exception normalization would be required;
- premature later-WP implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- package/project/reference/schema mutation is required.

Report the smallest corrective authority required.

---

## 39. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP07 manifest paths;
6. actual contract inventory;
7. final orchestration order;
8. request validation;
9. Release 1.4 feature-generation request mapping;
10. upstream invocation count;
11. upstream bounded failure mapping;
12. Feature Set validation;
13. summary computation invocation count;
14. numeric failure mapping;
15. Experiment Definition/Result identity construction;
16. provenance/lineage;
17. empty/single/non-empty results;
18. equivalent rerun / predecessor-distinct identity;
19. unknown defect propagation;
20. DI/Worker/persistence/provider exclusions;
21. repository delta;
22. temporary acceptance matrix;
23. permanent validation/test counts;
24. architecture/security/whitespace/residue;
25. GitHub lifecycle mutations;
26. final #174/#175/milestone state;
27. findings/blockers;
28. next authorized WP.

---

## 40. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP07 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Dependency Registration & Configuration — GitHub issue #175`

Do not begin WP08.

If blocked, end:

`RELEASE 1.5 WP07 BLOCKED`

and identify the smallest corrective authority required.

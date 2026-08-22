# Release 1.5 WP06 — Experiment Validation & Failure Semantics

## GitHub Issue
`#173 — Release 1.5 WP06 — Experiment Validation & Failure Semantics`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP06 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP06 establishes the deterministic Application-owned validation and failure-semantics boundary over the immutable WP04 experiment contracts and WP05 summary-computation seam.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP04 experiment model/contracts and identity implementation
- WP05 `SimpleReturnDescriptiveSummaryComputer.cs`
- accepted Release 1.4 feature-generation validation/failure semantics
- accepted Release 1.3 pipeline validation/failure semantics
- relevant Application result/failure patterns and architecture rules
- WP01–WP05 completion evidence
- this WP06 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If the current WP04/WP05 contracts cannot express the frozen WP02/WP03 failure semantics without redesigning predecessor work or stealing WP07 orchestration ownership, stop and request the smallest corrective authority.

---

## 2. Objective

Implement exactly the manifest-authorized Application validation/failure boundary for Release 1.5.

WP06 must define and enforce deterministic validation precedence for experiment semantics that can be validated before or around summary computation, while preserving:

- bounded failure distinctions;
- fail-stop behavior;
- no partial successful evidence;
- no fabricated Experiment Result identity after failure;
- natural propagation of unknown defects;
- no retry/fallback behavior;
- no provider/storage coupling;
- no orchestration that belongs to WP07.

WP06 must not implement the feature-to-experiment integration use case.

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

- #168–#172: CLOSED / Done;
- #173 WP06: OPEN / Backlog;
- #174 WP07: OPEN / Backlog;
- #175–#180: OPEN / Backlog;
- milestone #46: OPEN with 8 open / 5 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- SQLite schema: v2.

Expected accepted WP05 production addition:

`src/AIQuantTradingResearch.Application/Experiments/SimpleReturnDescriptiveSummaryComputer.cs`

If #172 is not Closed/Done or #174 has started, stop before mutation.

---

## 4. WP06 Lifecycle Start

After starting-state gates pass:

- move only #173 Project #2 Status from Backlog to In Progress.

Read back the state.

If #173 is already In Progress solely because this exact WP06 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #174.

#174 must remain OPEN / Backlog throughout WP06.

---

## 5. Required Contract Inventory

Before writing code, inspect and report:

- exact `ExperimentRequest` or equivalent request contract;
- exact `ExperimentDefinition` shape;
- exact `ExperimentSummaryEvidence` shape;
- exact `ExperimentResult` / success/failure contract;
- exact bounded failure vocabulary already created by WP04;
- exact `IExperimentSummaryComputer` seam;
- exact canonical Experiment Definition/Result identity APIs;
- whether current contracts can represent invalid request / unsupported definition / invalid evidence / invalid numeric failure / integrity conflict distinctly.

Do not create duplicate result/failure abstractions if accepted ones already exist.

If WP04's failure contract does not align with frozen semantics, stop before broadening it unless the manifest explicitly authorizes the minimal WP04 refinement needed by WP06.

---

## 6. Validation Ownership Boundary

WP06 owns semantic validation and deterministic classification that can be performed without performing WP07 upstream orchestration.

WP06 may validate:

- request coherence;
- supported built-in experiment definition;
- accepted Feature Set evidence supplied to the validation/computation boundary;
- Feature Set identity/provenance coherence when such evidence is available;
- summary evidence coherence after computation if the accepted contract requires result validation;
- count/aggregate presence invariants;
- numeric evidence validity;
- definition/result identity coherence where deterministically checkable using WP04 identity machinery;
- integrity contradictions.

WP06 does not own:

- exact snapshot lookup;
- feature generation invocation;
- dependency-unavailable classification originating from WP07 upstream calls unless only the failure vocabulary/model is represented here;
- Worker configuration;
- DI;
- provider access;
- persistence.

---

## 7. Deterministic Validation Precedence

Freeze and implement one deterministic first-failure precedence consistent with the accepted Release 1.5 authorities and actual contract surface.

Preferred conceptual order, subject to exact repository truth:

1. invalid request;
2. unsupported experiment definition;
3. invalid Feature Set / predecessor semantic evidence;
4. invalid numeric evidence / computation failure;
5. integrity contradiction in identity/result evidence.

Do not blindly use this order if the accepted definition/plan/WP04 failure model fixes a more precise order. Reconcile first and report the final order explicitly.

The same semantic invalid input must classify the same way independent of culture, process, machine, or operational context.

Do not return multiple simultaneous bounded failures unless the accepted contract explicitly requires an aggregate failure model, which Release 1.5 does not intend.

---

## 8. Invalid Request

Define deterministic invalid-request handling for structurally valid types that can still represent incoherent requests.

Examples may include, depending on actual contract:

- missing required semantic reference;
- request definition/evidence mismatch;
- impossible required input binding.

Do not manufacture malformed objects with reflection/unsafe techniques when accepted constructors intentionally prevent them.

Constructor-rejected impossible states are already protected by WP04 model invariants and should be reported as such rather than duplicated through hacks.

Invalid request must fail before summary computation.

---

## 9. Unsupported Experiment Definition

The only supported definition is:

`simple-return-descriptive-summary-v1`

If the accepted public/model surface can represent a different definition, classify it deterministically as unsupported and prevent computation.

If WP04 intentionally makes unsupported definitions unconstructable, do not weaken the model to create a test/probe case. Report the invariant and preserve the boundary.

Do not introduce a registry/plugin lookup merely to support future experiments.

---

## 10. Feature Set Evidence Validation

Where accepted Feature Set evidence is supplied to the validator, preserve and validate Release 1.4 semantic coherence without redefining Feature semantics.

Applicable checks may include:

- exact typed Feature Set Identity is present/valid;
- Feature Set definition is the accepted `simple-return-lag-1-v1` where Release 1.5 requires that exact predecessor;
- count/value collection coherence;
- immutable evidence completeness;
- provenance/lineage references required by WP02/WP03;
- no contradictory predecessor identity/evidence.

Do not:

- recompute feature values;
- resort/rewrite feature evidence;
- query storage;
- call provider;
- mutate Feature Set;
- reimplement `aiq-feature-identity-v1`.

Invalid Feature Set evidence must stop before summary computation.

---

## 11. Numeric Evidence Failure

WP05 intentionally allows natural decimal arithmetic failure rather than fabricating a fallback.

WP06 must establish how governed numeric failure is mapped through the accepted Release 1.5 failure contract.

Requirements:

- decimal overflow / unrepresentable summary evidence must not produce partial success;
- no Experiment Result identity may be created after numeric failure;
- no floating-point fallback;
- no rounding fallback;
- no value skipping;
- no saturation;
- no NaN/infinity sentinel.

Catch only the narrow numeric failure types that the accepted computation semantics classify as governed invalid numeric evidence.

Do not catch unrelated exceptions.

---

## 12. Integrity Contradiction

Preserve WP03 integrity semantics.

If semantically equal Experiment Definition/Result identity claims contradict canonical content, classify the condition as the accepted integrity conflict/contradiction category.

Do not normalize contradiction into equivalence.

Do not overwrite evidence.

Do not "repair" identities automatically.

If such contradiction is unconstructable through accepted immutable public boundaries, document that invariant rather than bypassing it.

---

## 13. Unknown Defect Propagation

Unknown/unrelated programming or system defects must propagate.

Do not introduce:

- catch-all `Exception` normalization;
- broad "dependency unavailable" mapping;
- generic invalid-evidence mapping for unknown defects;
- retry;
- fallback;
- logging-and-success behavior.

Temporary probes should prove unknown exceptions are not silently converted when practical through accepted seams.

---

## 14. Fail-Stop

The first governed failure terminates the semantic operation.

Requirements:

- later validation/computation does not run after an earlier failure;
- no partial successful summary;
- no Experiment Result identity after failed validation/numeric computation;
- no downstream provenance fabricated after failure.

If the validator and summary computer are separate seams, WP06 must make the intended call ordering clear for WP07 without implementing WP07 orchestration.

---

## 15. Successful Empty Result Preservation

An accepted empty Feature Set remains successful:

- count = 0;
- aggregates absent;
- no invalid-evidence classification merely because the set is empty.

WP06 must not reinterpret successful empty evidence as failure.

If post-computation result validation exists, it must accept this state.

---

## 16. Successful Single / Non-Empty Preservation

Valid WP05 evidence must remain valid:

- single value: count 1, mean/min/max equal that value;
- non-empty: coherent count/mean/min/max.

WP06 must not impose extra statistics, thresholds, or domain-specific trading interpretation.

This is descriptive research evidence only.

---

## 17. Result Identity Validation

Use the WP04 canonical identity capability when the accepted WP06 boundary requires checking result identity coherence.

Do not duplicate canonical hashing logic.

If validating a fully formed successful result:

- recomputed expected identity from frozen semantic evidence must equal asserted identity;
- contradiction maps to integrity conflict;
- no operational metadata participates.

If result identity construction is exclusively WP07-owned and no complete result reaches WP06, do not invent post-result validation here. Reconcile the actual contract first.

---

## 18. Provenance / Lineage Validation

Validate only the minimum semantic coherence required by WP03 and available through accepted contracts.

Preserve conceptual lineage:

`source state → dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

Do not introduce generalized lineage graph validation.

Do not require provider/storage operational details.

Do not create cyclic references.

---

## 19. Failure Vocabulary Reconciliation

Use the exact bounded failure categories already accepted by Release 1.5.

Expected conceptual categories include:

- InvalidRequest
- UnsupportedDefinition
- NotFound / predecessor evidence not found
- DependencyUnavailable
- InvalidEvidence / invalid Feature Set evidence
- InvalidNumericInput / equivalent numeric category
- IntegrityConflict

Repository truth controls exact enum names.

WP06 may refine WP04 contract names only if:
- the manifest authorizes the path;
- the refinement is necessary to match already-frozen semantics;
- no new category is invented;
- execution report records the bounded refinement.

Do not change predecessor Release 1.2–1.4 failure vocabularies.

---

## 20. No WP07 Orchestration

Do not implement:

- `ExperimentGenerationUseCase` orchestration assigned to WP07;
- calls to Release 1.4 feature-generation use case;
- exact snapshot lookup;
- dependency invocation;
- upstream NotFound/Unavailable catch/mapping behavior as an orchestration flow;
- result identity construction as part of a full use-case pipeline unless current WP06 manifest explicitly assigns a validator helper only.

WP06 should leave WP07 a clear validation/failure boundary to compose.

---

## 21. No DI / Worker / Persistence

Do not modify:

- `DependencyInjection.cs`;
- Worker configuration;
- Worker execution;
- `Program.cs`;
- Infrastructure production;
- SQLite schema/migrations;
- provider/network code.

SQLite remains v2.

No experiment persistence/registry/history.

---

## 22. Release 1.3 / 1.4 Protection

Preserve Release 1.3:

- fixed five-stage pipeline;
- fail-stop behavior;
- unknown exception propagation;
- no experiment stage.

Preserve Release 1.4:

- exact Feature Set semantics;
- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- feature validation/failure mapping;
- exact snapshot integration;
- feature Worker behavior;
- no feature persistence.

WP06 must not refactor predecessor logic into a generalized failure framework.

---

## 23. Architecture Boundary

Expected dependency graph remains:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

WP06 Application code may reference accepted Application/Domain types only.

No Infrastructure or Worker reference from Application.

No package/project/reference changes.

---

## 24. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

Expected logical WP06 path:

`src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationValidator.cs`

Use the actual exact manifest path.

A narrowly required refinement to an existing WP04 Experiment contract file is permitted only if:

- the manifest includes the shared Experiment area/file for WP06 reconciliation;
- frozen WP02/WP03 semantics cannot otherwise be represented;
- no WP07 behavior is introduced.

Before mutation enumerate authorized paths.

After mutation reconcile exact delta.

Expected category deltas:

- Application production: WP06 validator + only justified bounded shared-contract refinement;
- Domain: 0;
- Infrastructure: 0;
- Worker: 0;
- permanent tests: 0;
- packages/projects/references/schema: 0/0/0/0.

If the file manifest does not permit a necessary contract refinement, stop rather than mutate out of scope.

---

## 25. No Permanent Tests in WP06

Permanent Application tests belong to WP10.

Do not add test files or test cases in WP06.

Use a removable offline probe if needed to prove the validation/failure matrix.

The probe must use hand-written deterministic doubles/evidence only and must be removed before completion.

No SQLite/provider/network.

Permanent test baseline remains 214.

---

## 26. Required Temporary Acceptance Matrix

Using constructors/invariants and a removable offline probe where necessary, prove all applicable constructible cases:

1. valid empty evidence remains valid;
2. valid single-value evidence remains valid;
3. valid non-empty evidence remains valid;
4. invalid request fails before computation;
5. unsupported definition fails before computation, if constructible;
6. invalid Feature Set evidence fails before computation;
7. numeric overflow maps to the governed numeric failure category;
8. numeric failure returns no partial summary/result identity;
9. integrity contradiction maps correctly, if constructible;
10. first governed failure prevents later operations;
11. unknown computation exception propagates;
12. culture does not affect classification;
13. operational metadata does not affect classification;
14. no provider/storage interaction occurs.

If a row is impossible by construction, report the model invariant instead of fabricating invalid objects.

---

## 27. Validation Precedence Evidence

Explicitly record the final deterministic precedence implemented.

For multi-defect constructible evidence, prove the first applicable category wins consistently.

Do not introduce exhaustive aggregate-error collection.

If multi-defect states are structurally impossible due to WP04 constructors, report that fact and prove precedence only across states that can exist through accepted public seams.

---

## 28. Technical Validation

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
- direct whitespace inspection of new/modified WP06 files and relevant untracked governance artifacts.

Require:

- temporary probe removed;
- database/WAL/SHM/journal residue: 0;
- generated residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 29. Architecture Validation

Confirm:

- dependency graph unchanged;
- cycles 0;
- no Application → Infrastructure/Worker edge;
- no experiment Infrastructure implementation;
- no package/reference/schema delta.

Architecture.Tests remain 13/13.

Do not add Architecture.Tests.

---

## 30. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- mutate packages/projects/references/schema;
- begin WP07;
- begin Release 1.6;
- edit semantic authorities merely to make implementation fit.

Git transport mutation budget:

`0`

Repository mutation budget:

only manifest-authorized WP06 Application paths.

---

## 31. Authorized GitHub Mutation Budget

At WP06 start after gates pass:

1. #173 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP06 completion-evidence comment to #173;
3. close #173 as completed;
4. set #173 Project Status to Done.

Do not mutate #174.

Milestone #46 remains OPEN.

---

## 32. Completion Gate

WP06 may close only if:

- #172 is Closed/Done;
- #173 was In Progress during execution;
- #174 remains Open/Backlog;
- exact file-manifest accounting passes;
- deterministic validation precedence is explicit;
- invalid request/unsupported definition/invalid evidence/numeric/integrity categories are preserved as applicable;
- valid empty/single/non-empty evidence remains successful;
- first-failure semantics are proven;
- no partial successful evidence/result identity occurs after failure;
- unknown defects propagate;
- no WP07 orchestration was implemented;
- Domain/Infrastructure/Worker/test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- provider/network execution 0;
- Release 1.6 work 0.

If any gate fails, do not close #173 or mark Done.

---

## 33. Completion Evidence Comment

On success, post concise evidence to #173 covering:

- exact Application files added/refined;
- final validation precedence;
- bounded failure categories;
- valid empty/single/non-empty preservation;
- invalid request / unsupported-definition treatment;
- invalid Feature Set evidence treatment;
- decimal-overflow/numeric mapping;
- integrity contradiction treatment;
- fail-stop/no-partial-result identity behavior;
- unknown exception propagation;
- no WP07 orchestration/DI/Worker/persistence;
- zero Domain/Infrastructure/Worker/test/package/reference/schema delta;
- SQLite v2;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #174 preserved Open/Backlog.

---

## 34. Final Read-Back

After successful closure verify:

- #173: CLOSED / Done;
- #174: OPEN / Backlog;
- #175–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 7 open / 6 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 artifacts accurately.

---

## 35. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #172 is not Closed/Done;
- #174+ started unexpectedly;
- WP06 manifest ownership is ambiguous;
- WP04 failure contracts cannot express frozen WP02/WP03 semantics within authorized refinement;
- WP05 numeric behavior contradicts frozen semantics;
- satisfying validation requires WP07 orchestration;
- broad unknown-exception normalization would be required;
- premature later-WP implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- package/project/reference/schema mutation is required.

Report the smallest corrective authority required.

---

## 36. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. actual contract/failure inventory;
6. exact authorized paths;
7. final validation precedence;
8. invalid request treatment;
9. unsupported-definition treatment;
10. Feature Set evidence validation;
11. numeric failure mapping;
12. integrity contradiction handling;
13. valid empty/single/non-empty preservation;
14. fail-stop/no partial identity behavior;
15. unknown exception propagation;
16. result-identity/provenance validation decision;
17. WP07 orchestration exclusion;
18. predecessor/schema/provider protection;
19. repository delta;
20. temporary acceptance matrix;
21. permanent validation/test counts;
22. architecture/security/whitespace/residue;
23. GitHub lifecycle mutations;
24. final #173/#174/milestone state;
25. findings/blockers;
26. next authorized WP.

---

## 37. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP06 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Feature-to-Experiment Integration — GitHub issue #174`

Do not begin WP07.

If blocked, end:

`RELEASE 1.5 WP06 BLOCKED`

and identify the smallest corrective authority required.

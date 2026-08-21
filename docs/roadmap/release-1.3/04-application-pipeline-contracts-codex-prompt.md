# Release 1.3 WP04 — Application Pipeline Contracts — Codex Execution Authority

## Phase 3 — Release 1.3: Research Pipeline Foundation

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`
**GitHub issue:** `#141`
**Recommended model:** GPT-5.6 Terra
**Work-package type:** Bounded Application contract implementation
**Primary layer:** Application
**Orchestration:** NOT AUTHORIZED

## 1. Mission

Execute **WP04 — Application Pipeline Contracts** only.

Translate the accepted WP02/WP03 pipeline semantics into the minimum coherent, provider-independent and storage-independent **Application-owned C# contract surface** required by later Release 1.3 work.

WP04 must encode already-decided semantics. It must not redesign pipeline topology, identity, canonicalization, provenance, evidence, dataset semantics, or failure policy.

Do not implement pipeline orchestration. WP05 owns fixed pipeline orchestration.

## 2. Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. Release 1.3 GitHub-planning authority and accepted result
5. WP01–WP03 authorities and accepted execution results
6. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
7. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
8. Release 1.2 dataset semantic documents:
   - `RESEARCH_DATASET_DEFINITION.md`
   - `DATASET_IDENTITY_VERSION_PROVENANCE.md`
9. current Application dataset contracts and materialization/integration implementation
10. current Domain, Infrastructure, Worker, tests, architecture tests, DI, configuration, and persistence implementation
11. GitHub issue #141, milestone #54, Project #2, and predecessor state

WP02 and WP03 are semantic authorities. Do not contradict them.

If authorities materially conflict, STOP before mutation and report the conflict plus the smallest corrective authority required.

## 3. Starting-State Gates

Before editing:

- verify repository identity;
- verify branch `main`;
- verify `HEAD = origin/main`;
- report SHA and ahead/behind;
- verify staged paths = 0;
- classify all existing tracked/untracked paths;
- preserve accepted cumulative Release 1.3 artifacts;
- verify #139 and #140 are `Closed / Done`;
- verify #141 is `Open / Backlog`;
- verify #142 is `Open / Backlog`;
- verify milestone #54 is open;
- verify WP04 dependency is exactly WP03;
- verify dependency drift = 0;
- verify no WP05+ implementation exists;
- verify no Release 1.4 implementation exists.

Run canonical baseline validation before mutation.

Expected accepted baseline unless repository truth legitimately differs:

- Domain.Tests: 11
- Application.Tests: 60
- Infrastructure.Tests: 87
- Architecture.Tests: 13
- Permanent total: 171
- Build warnings/errors: 0/0
- SQLite schema: 2
- Dataset identity scheme: `aiq-dataset-identity-v1`
- Pipeline identity scheme: `aiq-pipeline-identity-v1`
- canonical verification: PASS
- Gitleaks: PASS

Only after all starting-state and baseline gates pass may #141 move `Backlog → In Progress`.

## 4. Frozen Pipeline Topology

Preserve exactly:

```text
Explicit pipeline request
  → historical observation retrieval
  → deterministic dataset materialization
  → immutable snapshot persistence
  → catalog registration
  → structured result/evidence
```

The contract surface may represent these stages, but must not make topology configurable.

No DAG, plugin stage collection, arbitrary stage registration, scheduler, loop, parallelism, or dynamic topology.

## 5. Frozen Identity Semantics

Preserve WP03:

- pipeline identity scheme: `aiq-pipeline-identity-v1`;
- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- Dataset Definition Identity;
- Source State Identity;
- Dataset Snapshot Identity / Dataset Version;
- operational invocation/correlation identity is non-semantic;
- SHA-256 fingerprints represented as exactly 64 lowercase hexadecimal characters;
- deterministic length-delimited canonical semantics;
- equivalent reruns have equivalent semantic execution identity;
- `NewlyAccepted` vs `EquivalentExisting` is not identity-bearing;
- no mutable Pipeline Version concept;
- no durable run-history requirement.

WP04 may implement typed values and validation. It must not redesign canonical identity inputs.

## 6. Namespace and Ownership

Prefer a focused Application namespace consistent with repository conventions, for example:

`AIQuantTradingResearch.Application.Pipelines`

If the manifest prescribes an exact namespace/path, the manifest wins.

All WP04 production changes must remain in Application unless an authority explicitly requires otherwise.

Expected layer deltas:

- Domain: 0
- Application: bounded contract additions
- Infrastructure: 0
- Worker: 0

## 7. Typed Pipeline Identities

Implement distinct typed concepts for at least:

- `PipelineDefinitionIdentity`
- `PipelineExecutionIdentity`

Requirements:

- cannot be accidentally interchanged;
- validate the accepted pipeline identity scheme/fingerprint representation;
- preserve scheme explicitly where required by accepted semantics;
- exactly 64 lowercase hexadecimal fingerprint characters;
- immutable value semantics;
- no runtime-generated identity behavior;
- no hashing/canonical computation required in WP04 unless the manifest explicitly assigns it here.

Do not duplicate Release 1.2 dataset identity types.

Reuse existing dataset identities by composition/reference.

## 8. Pipeline Definition Contract

Create the minimum contract representing the semantic pipeline definition/request boundary.

Prefer reuse of existing `DatasetDefinition`.

The contract must represent:

- the accepted fixed Research Pipeline semantic definition;
- dataset definition/request semantics;
- pipeline semantic-model identity/version input if required by WP03;
- Pipeline Definition Identity where semantically appropriate.

Do not duplicate target/from/to if `DatasetDefinition` already owns them.

Do not add physical Worker configuration names.

Do not make stages user-configurable.

## 9. Fixed Stage Contract

Represent the fixed ordered semantic stages with a closed Application-owned vocabulary.

At minimum the model must distinguish:

1. Historical Observation Retrieval
2. Dataset Materialization
3. Snapshot Persistence
4. Catalog Registration
5. Structured Result/Evidence

Use repository naming conventions and WP02's exact semantic interpretation.

The stage representation must:

- be deterministic;
- have fixed order/ordinal semantics;
- reject undefined stages where applicable;
- not expose extension/plugin registration.

Do not create a generic workflow-stage interface.

## 10. Success Disposition

Represent successful semantic disposition without making it identity-bearing.

At minimum:

- newly accepted;
- equivalent existing.

A valid empty dataset is still a success and uses the same disposition model.

Do not add a mutable "latest" or run sequence.

## 11. Pipeline Result Contract

Define a bounded terminal result contract capable of representing:

### Success
- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- successful disposition;
- resulting Dataset Snapshot Identity / Dataset Version;
- semantic evidence/provenance required by WP03;
- empty result where applicable without special failure semantics.

### Failure
- Pipeline Definition Identity where established;
- Semantic Pipeline Execution Identity where WP03 says one is deterministically established;
- first failing stage;
- bounded failure information;
- established upstream semantic evidence;
- no fabricated downstream identity.

Prefer a discriminated success/failure shape consistent with existing Application conventions.

Do not throw away existing lower-level typed failures merely to simplify the result contract.

## 12. Pipeline Evidence Contract

Create the minimum immutable semantic evidence representation required by WP03.

It must be able to explain, where applicable:

- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- pipeline identity scheme;
- Dataset Definition Identity;
- Source State Identity;
- Dataset Snapshot Identity / Dataset Version;
- fixed stage evidence;
- terminal outcome;
- success disposition;
- failure stage/category;
- relationship to Release 1.2 provenance/evidence.

Reuse Release 1.2 semantic types rather than copying them.

No persistence behavior.

## 13. Stage Evidence Contract

Represent immutable evidence for one fixed stage.

At minimum consider:

- stage;
- fixed ordinal/order;
- semantic outcome;
- established semantic input identity references;
- established semantic output identity references;
- bounded failure information when failed.

Keep this minimal.

Do not include:

- wall-clock start/end;
- duration;
- process/thread/machine identity;
- retry count;
- trace/span IDs;
- logging severity;
- database IDs.

WP07 will later own structured execution evidence behavior/formatting; WP04 should provide only the semantic contract seam needed by later work.

## 14. Provenance Contract

Represent pipeline provenance according to WP03 without creating a second dataset provenance model.

Pipeline provenance should reference/reuse accepted Release 1.2 identity/provenance facts.

It must support:

- pipeline definition;
- semantic execution;
- relevant source state;
- resulting dataset evidence when established;
- stage sequence;
- terminal semantic relationship.

Avoid copying entire observation collections unless the semantic authority explicitly requires them.

## 15. Lineage Contract

If WP03 retained a distinct pipeline-lineage concept, encode only that narrow acyclic relationship.

Do not implement a generic graph.

Failure lineage must stop at the last established semantic evidence.

No downstream identity may be invented.

## 16. Operational Invocation Boundary

WP04 must preserve the semantic separation of operational invocation/correlation identity.

If a contract seam is required for later Worker/observability work, it must be clearly non-semantic and must not participate in:

- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- equivalence;
- Dataset identity/version;
- semantic provenance.

Do not generate invocation IDs in WP04.

If no Application contract is required yet, leave the operational identifier out and document the boundary through code structure/comments only where useful.

## 17. Failure Vocabulary Boundary

WP06 owns generalized pipeline validation and failure semantics.

WP04 may introduce only the minimum bounded failure contract required to make pipeline result/stage evidence representable.

Prefer reuse/composition of accepted Application failure vocabulary where semantically correct.

Do not implement:

- SQLite classification;
- retryability;
- exception mapping;
- provider-specific errors;
- resilience policy;
- catch-all exception handling.

## 18. Contract Invariants

Contracts should reject semantically impossible states at construction where consistent with repository conventions.

Examples:

- invalid pipeline fingerprints;
- mismatched scheme;
- success without required established output identity;
- failure without a failing stage;
- contradictory stage order;
- stage evidence after a failed stage;
- Dataset Version inconsistent with Snapshot Identity;
- provenance/evidence identity inconsistency;
- empty success represented as failure.

Do not normalize contradictory input into valid-looking evidence.

## 19. Canonicalization Boundary

WP03 froze canonical semantic representation.

WP04 may expose values required for later canonical identity computation, but do not implement a competing identity algorithm.

If an identity computer is required by the file manifest for WP04, it must implement WP03 exactly. Otherwise defer computation to the work package assigned by the execution plan/manifest.

Never use:

- `GetHashCode`;
- serializer defaults;
- culture-sensitive formatting;
- wall-clock values;
- runtime object identity.

## 20. Dataset Contract Reuse

Reuse current Release 1.2 Application types wherever appropriate, including concepts such as:

- `DatasetDefinition`;
- `DatasetDefinitionIdentity`;
- `ResearchDatasetIdentity`;
- `SourceStateIdentity`;
- `DatasetSnapshotIdentity`;
- `DatasetVersion`;
- `DatasetSnapshotCandidate`;
- `DatasetCoverage`;
- `DatasetProvenance`;
- `DatasetLineage`;
- materialization/persistence/catalog result vocabulary.

Do not alter those types merely for aesthetic consistency unless strictly necessary and authorized.

Any necessary refinement must be minimal, backwards-compatible with accepted Release 1.2 semantics, and explicitly reported.

## 21. No Orchestration

WP04 must not call:

- `IMaterializeDatasetUseCase`;
- `IDatasetSnapshotStore`;
- `IDatasetCatalog`;
- `IHistoricalObservationStore`;

as part of pipeline execution.

Do not implement a pipeline use case/service that sequences stages.

WP05 owns orchestration.

Contracts may reference the existing seam/result types as semantic dependencies if needed.

## 22. No Infrastructure or Host Behavior

Do not add or modify:

- SQLite implementation;
- SQL;
- schema;
- migrations;
- connection factory;
- snapshot/catalog persistence;
- provider integration;
- DI registration;
- configuration;
- Worker execution;
- console/log output.

SQLite must remain schema version 2.

## 23. Tests

Permanent tests are not owned by WP04 unless the authoritative manifest explicitly assigns them.

Expected permanent test delta: 0.

Temporary offline probes may be used only if necessary to validate contract construction/invariants and must be completely removed before completion.

Do not start WP10 Application Pipeline Tests.

## 24. Authorized Files

Use the exact WP04 paths from `RELEASE_1.3_FILE_MANIFEST.md`.

If the manifest allows design freedom within an Application pipeline folder, keep the surface minimal and cohesive.

Expected conceptual files may include equivalents of:

- pipeline identity value types;
- pipeline definition/request contracts;
- fixed stage vocabulary;
- pipeline result/outcome contracts;
- pipeline evidence/provenance/lineage contracts.

Do not create speculative abstractions for future releases.

Report every added/modified file exactly.

## 25. Explicit Scope Protection

Do not start:

- WP05 Fixed Pipeline Orchestration;
- WP06 Pipeline Validation & Failure Semantics;
- WP07 Structured Execution Evidence behavior;
- WP08 Dependency Registration & Configuration;
- WP09 One-Shot Worker Pipeline Execution;
- WP10 Application Pipeline Tests;
- WP11 Composition & Worker Validation;
- WP12 Architecture Evolution;
- WP13 Documentation Alignment;
- WP14 Full Validation, Integration & Acceptance;
- Release 1.4+ work.

## 26. Prohibited Repository Mutations

Unless the manifest explicitly authorizes otherwise, do not modify:

- Domain;
- Infrastructure;
- Worker;
- permanent tests;
- SQLite schema/bootstrap;
- packages;
- project references;
- solution;
- build scripts;
- engineering scripts;
- Release 1.3 definition;
- execution plan;
- file manifest;
- GitHub-planning authority;
- WP02/WP03 semantic documents.

Do not stage, commit, push, branch, create PR, merge, tag, or release.

## 27. Validation

After implementation run:

- direct inspection of every WP04 file;
- trailing-whitespace checks;
- `git diff --check`;
- `git diff --cached --check`;
- restore;
- format verification;
- build;
- Domain.Tests;
- Application.Tests;
- Infrastructure.Tests;
- Architecture.Tests;
- canonical `eng/verify.ps1 -Configuration Release`;
- Gitleaks;
- database residue scan.

Expected permanent counts remain:

- Domain: 11
- Application: 60
- Infrastructure: 87
- Architecture: 13
- Total: 171
- skipped: 0

Expected:

- build warnings/errors: 0/0;
- SQLite schema: 2;
- package/reference delta: 0/0;
- permanent test delta: 0;
- Domain/Infrastructure/Worker delta: 0.

## 28. Semantic Validation Matrix

Prove at minimum that the contract surface can represent:

1. a valid fixed pipeline definition;
2. distinct Pipeline Definition and Execution identities;
3. exact pipeline scheme/fingerprint validation;
4. fixed ordered stages;
5. successful newly accepted result;
6. successful equivalent-existing result;
7. successful empty-dataset result;
8. first-stage failure;
9. intermediate-stage failure;
10. failure without fabricated downstream identity;
11. established upstream evidence on failure;
12. resulting Dataset Snapshot Identity/Version on success;
13. provenance relationship to dataset/source identities;
14. equivalent rerun without identity-bearing disposition drift;
15. operational invocation metadata excluded from semantic identity;
16. immutable evidence;
17. invalid/contradictory contract states rejected.

A temporary probe is acceptable if needed; remove it before completion.

## 29. GitHub Lifecycle

After all starting-state/baseline gates pass:

- move #141 `Backlog → In Progress`.

Only after every WP04 acceptance gate passes:

- post concise completion evidence to #141;
- close #141;
- set Project #2 status to Done.

Verify:

- #142 remains `Open / Backlog`;
- milestone #54 remains open;
- no later issue is mutated.

## 30. Acceptance Matrix

WP04 completes only if:

- WP03 predecessor: PASS
- WP02 topology preserved: PASS
- WP03 identity semantics preserved: PASS
- Release 1.2 dataset contracts reused: PASS
- Application ownership: PASS
- Pipeline Definition Identity contract: PASS
- Pipeline Execution Identity contract: PASS
- fixed stage vocabulary: PASS
- definition/request contract: PASS
- success disposition: PASS
- success result: PASS
- failure result: PASS
- empty success representable: PASS
- stage evidence: PASS
- pipeline evidence: PASS
- provenance: PASS
- lineage, if required: PASS
- operational invocation separation: PASS
- impossible states rejected: PASS
- provider independence: PASS
- storage independence: PASS
- orchestration started: NO
- SQLite/schema leakage: 0
- provider/HTTP leakage: 0
- Domain delta: 0
- Infrastructure delta: 0
- Worker delta: 0
- permanent test delta: 0
- package/reference delta: 0/0
- WP05 started: NO
- Release 1.4 implementation started: NO
- canonical validation: PASS

## 31. Required Execution Report

Report at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP02 Semantic Reconciliation
10. WP03 Identity/Evidence Reconciliation
11. Release 1.2 Dataset Contract Reconciliation
12. Application Ownership Decision
13. Namespace/File Design
14. Pipeline Identity Contracts
15. Pipeline Definition/Request Contract
16. Fixed Stage Vocabulary
17. Success Disposition
18. Pipeline Result Contract
19. Pipeline Evidence Contract
20. Stage Evidence Contract
21. Provenance Contract
22. Lineage Contract
23. Operational Invocation Boundary
24. Failure Vocabulary Boundary
25. Contract Invariants
26. Canonicalization Boundary
27. Dataset Contract Reuse
28. Orchestration Protection
29. Exact Files Added/Modified
30. Domain Delta
31. Application Delta
32. Infrastructure Delta
33. Worker Delta
34. Package/Reference Delta
35. Permanent Test Delta
36. Temporary Probe Evidence
37. WP05 Protection
38. Release 1.4 Protection
39. Security/Offline Evidence
40. Whitespace/Diff Evidence
41. Restore/Build Evidence
42. Permanent Test Evidence
43. Canonical Verification
44. Architecture Validation
45. Semantic Validation Matrix
46. Mutation Accounting
47. Git/GitHub Protection
48. Planning Protection
49. Findings/Blockers
50. Final Repository/GitHub State
51. WP05 Handoff
52. Final Decision
53. Next Authorized Work Package

On success end exactly with:

RELEASE 1.3 WP04 COMPLETE

NEXT AUTHORIZED WORK PACKAGE:
WP05 — Fixed Pipeline Orchestration
GitHub issue #142

If blocked, end with:

RELEASE 1.3 WP04 BLOCKED

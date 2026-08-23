# Release 1.7 Execution Plan

## Governance

Release: **Phase 4 - Release 1.7: Durable Experiment Evidence Discovery**

Baseline: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Status: planning candidate; no implementation or GitHub mutation is authorized.

All WPs preserve schema v3, `aiq-experiment-identity-v1`, Release 1.6 durable semantics, the production graph, and predecessor tests. Dependencies are linear because each boundary freezes the next WP's inputs.

## Work packages

### WP01 - Release & Repository Preflight

Purpose: prove closure, clean baseline, planning state, schema v3, graph, dependencies, and 250 tests. Mutation: none except WP01 lifecycle after all gates. Inputs: accepted planning authorities. Output: evidence-only baseline. Non-goals: design/implementation. Validation: canonical, Git/GitHub, security, manifest, residue. Dependency: none.

### WP02 - Durable Evidence Discovery Semantics

Purpose: freeze exact Snapshot + Experiment Definition query, positive bound, immutable evidence shape, ascending result-identity ordering, empty success, and no regeneration. Mutation: create `DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`. Inputs: WP01/Release 1.6. Non-goals: contracts, SQL, schema, Worker. Validation: canonical examples, ambiguity/scope review, baseline. Dependency: WP01.

### WP03 - Discovery Identity, Provenance & Fidelity

Purpose: freeze identity reuse, exact returned evidence, and contradiction rules. Mutation: create `EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`. Inputs: WP02 and predecessor identity authorities. Non-goals: physical design/implementation. Validation: identity/fidelity matrix. Dependency: WP02.

### WP04 - Application Discovery Contracts

Purpose: add immutable request/result and storage-independent discovery abstraction. Mutation: `ExperimentPersistenceContracts.cs`. Inputs: WP02-WP03. Outputs: typed bounded contracts using existing failures. Non-goals: orchestration, SQLite, DI, Worker, tests. Validation: compile, architecture/leakage, canonical. Dependency: WP03.

### WP05 - Durable Evidence Discovery Use Case

Purpose: validate once, invoke once, preserve success/empty/failure, propagate unknown defects. Mutation: create `DurableExperimentDiscoveryUseCase.cs` only. Inputs: WP04. Non-goals: SQLite, DI, configuration, Worker, or permanent tests. Validation: removable hand-written-double probe, architecture, canonical. Dependency: WP04.

### WP06 - Physical Access Pattern & Schema Decision

Purpose: prove schema v3 supports the predicate and ordering without structural mutation. Mutation: create `EXPERIMENT_DISCOVERY_PHYSICAL_ACCESS.md`. Inputs: WP02-WP05 and actual schema. Outputs: SQL predicate/order, bounded query-plan evidence, schema-v3/no-index decision. Non-goals: implementation. Validation: disposable v3 database plus `EXPLAIN QUERY PLAN`, then cleanup. Dependency: WP05. Structural need blocks for corrective authority.

### WP07 - SQLite Durable Evidence Discovery

Purpose: implement bounded read-only query and immutable reconstruction. Mutation: `SqliteExperimentResultStore.cs`, and mapper only when reuse requires it. Inputs: WP04/WP06. Outputs: ordered, bounded Infrastructure discovery. Non-goals: writes, regeneration, schema, DI, Worker, permanent tests. Validation: removable Infrastructure test-host probe proving read-only behavior and cleanup. Dependency: WP06.

### WP08 - Storage Validation & Failure Mapping

Purpose: verify malformed-row, unavailable-store, integrity, empty, and unknown-defect behavior at the completed storage boundary. Mutation: expected production delta zero; if WP07 did not satisfy the accepted matrix, stop for narrow corrective authority rather than silently redesigning it. Inputs: WP07. Outputs: accepted bounded-classification evidence. Non-goals: new failures, retry, repair, or redesign. Validation: removable offline matrix, read-only proof, canonical. Dependency: WP07.

### WP09 - Dependency Registration & Configuration Boundary

Purpose: register use case/store once and define minimal Worker-owned request configuration. Mutation: Application/Infrastructure DI and create `DurableExperimentDiscoveryConfiguration.cs`. Inputs: WP05/WP08. Outputs: side-effect-free graph/configuration. Non-goals: routing/execution. Validation: real-DI removable probe proving cardinality and zero database/provider side effects. Dependency: WP08.

### WP10 - One-Shot Durable Evidence Discovery Worker

Purpose: add explicit intent, precedence, one invocation, bounded output/exit, termination. Mutation: create `DurableExperimentDiscoveryExecution.cs`; modify `Program.cs`. Inputs: WP09 and fixture contract. Outputs: Durable Discovery -> Durable Experiment -> Experiment -> Feature -> pipeline. Non-goals: SQL, mutation, network, loops/retries/scheduling. Validation: real `--no-build` Worker over repository-native seeded v3 databases; prove empty/non-empty/order/bound/malformed/no-fallback/lower-mode preservation and cleanup. Dependency: WP09.

### WP11 - Application & Infrastructure Discovery Tests

Purpose: convert WP04-WP10 into permanent deterministic regressions. Mutation: two manifest-owned test files. Inputs: stable implementation/fixture. Outputs: fake-based Application and SQLite/DI/process Infrastructure coverage. Non-goals: production fixes without separate authority or packages. Validation: targeted/full suites, exact count reconciliation, isolation/cleanup. Dependency: WP10.

### WP12 - Architecture & Documentation Alignment

Purpose: align stable boundaries/current state. Mutation: manifest docs; Architecture.Tests only for a proven stable non-redundant rule. Inputs: WP02-WP11 truth. Outputs: accurate discovery/schema/routing/test/deferral docs. Non-goals: production/functional tests. Validation: links, stale claims, architecture/full/canonical/security/whitespace. Dependency: WP11.

### WP13 - Full Validation, Integration & Acceptance

Purpose: zero-development acceptance of the exact cumulative candidate. Mutation: repository content zero; WP13 lifecycle only after success. Inputs: WP01-WP12. Outputs: integration-ready decision and separate Git-integration handoff. Non-goals: fixes, transport, merge, Release 1.8. Validation: candidate accounting, canonical/full/process/fresh-checkout/schema/graph/security/docs/residue/GitHub. Dependency: WP12.

## Process fixture contract

WP06/WP07/WP10/WP11/WP13 use an isolated schema-v3 `TemporaryDatabase`; seed `DatasetSnapshotCandidate` through `SqliteDatasetSnapshotStore.Store(...)`; seed `experiment_results` through the production Release 1.6 durable acceptance path; use only existing friend-assembly access; invoke the built Worker with `--no-build` and explicit environment configuration; assert exact identities, ascending order, bound, empty success, single invocation, deterministic exit, and no fallback; remove process/output/database/WAL/SHM/journal/temp artifacts in fixture disposal/finally. Provider/network activity and real credentials are zero.

## Dependency graph

`WP01 -> WP02 -> WP03 -> WP04 -> WP05 -> WP06 -> WP07 -> WP08 -> WP09 -> WP10 -> WP11 -> WP12 -> WP13`

## Proposed GitHub planning state

- Milestone: `Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`
- Description: bounded deterministic discovery of immutable durable Experiment Result evidence by exact Snapshot and Experiment Definition context, preserving schema v3 and excluding registry/history.
- Issues: exactly WP01-WP13 with the titles above; no lifecycle issue or WP14+.
- Assignee: `samuel-santos-engineer`; existing labels only.
- Project #2 defaults: Status `Backlog`, Priority `P1`, Release `1.7`.

| WP | Area |
| --- | --- |
| WP01 | Engineering |
| WP02 | Data |
| WP03-WP05 | Architecture |
| WP06-WP08 | Data |
| WP09 | Configuration |
| WP10 | Host |
| WP11 | Testing |
| WP12 | Documentation |
| WP13 | Engineering |

No GitHub object may be created before separate human-authorized planning.

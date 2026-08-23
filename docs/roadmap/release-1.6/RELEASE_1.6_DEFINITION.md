# Release 1.6 Definition

## 1. Release title

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

## 2. Executive summary

Release 1.6 establishes one durable, immutable, exactly retrievable research
artifact: the accepted Release 1.5 `simple-return-descriptive-summary-v1`
Experiment Result. It persists result evidence without redefining
`aiq-experiment-identity-v1`, without persisting Feature Sets, and without
turning the repository into a generalized experiment registry.

The release adds an explicit Application persistence boundary, an
Infrastructure-owned SQLite implementation, a non-destructive schema-v2 to
schema-v3 migration, exact identity lookup, idempotent equivalent
re-persistence, integrity-conflict detection, bounded failure mapping, and one
explicit one-shot Worker path. Existing in-memory experiment generation remains
unchanged and side-effect free unless the durable path is explicitly selected.

## 3. Authoritative baseline

The definition was derived from this verified baseline:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- local `HEAD` and `origin/main`:
  `18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- Release 1.5 PR #181: merged;
- Release 1.5 milestone #46: closed with 13 closed and 0 open issues;
- Release 1.5 issues #168–#180: 13/13 Closed/Done;
- permanent tests: Domain 11, Application 102, Infrastructure 112,
  Architecture 13, total 238;
- SQLite schema: version 2;
- solution projects: 8;
- production graph: Domain → none, Application → Domain, Infrastructure →
  Application, Worker → Application and Infrastructure;
- Release 1.6 issues, PRs, branches, and implementation: none.

Legacy milestone #47 is open and empty under the historical title
`Phase 4 - Release 1.6: Strategy Framework`. It is read-only planning metadata
in this phase. This definition neither adopts that title nor mutates, populates,
closes, or repurposes the milestone.

The planning-definition prompt pair is out-of-band execution authority and is
not part of this definition artifact.

## 4. Problem and opportunity

Releases 1.1–1.5 provide a reproducible chain from historical observations to
an immutable Experiment Result. The final result remains in memory, however.
After process termination, the platform cannot prove that an accepted result
was durably retained, recover it by its semantic identity, distinguish an
equivalent re-persistence from contradictory evidence, or validate that result
after restart.

This is the narrowest missing lifecycle boundary. Adding more calculations
would increase breadth while leaving research conclusions ephemeral. Adding a
registry, strategy engine, backtester, scheduler, or workspace would require
larger semantics before the first research result is durable.

## 5. Candidate comparison

Scores use 1 (weak/high cost) through 5 (strong/low cost). “Schema” scores
higher when impact is smaller. The comparison reflects repository truth, not a
preselected outcome.

| Candidate | Research value | Coherence/reuse | Offline determinism | Schema/package impact | Operational simplicity | Future leverage | Premature-generalization safety | Release-sized | Decision |
| --- | ---: | ---: | ---: | ---: | ---: | ---: | ---: | ---: | --- |
| Durable Experiment Result evidence | 5 | 5 | 5 | 3 | 4 | 5 | 5 | 5 | Selected |
| Additional deterministic experiments | 4 | 4 | 5 | 5 | 5 | 3 | 3 | 4 | Deferred: breadth before durability |
| Feature persistence/catalog | 3 | 4 | 5 | 3 | 4 | 4 | 3 | 4 | Deferred: recomputable evidence and no correctness need |
| Experiment registry/history | 4 | 3 | 5 | 2 | 2 | 5 | 1 | 2 | Deferred: lifecycle/query/run semantics are not frozen |
| Controlled acquisition | 4 | 3 | 2 | 4 | 1 | 4 | 4 | 2 | Deferred: credentials, provider policy, and network behavior |
| Strategy/signal foundation | 5 | 2 | 4 | 5 | 3 | 5 | 2 | 2 | Deferred: look-ahead, timing, and position semantics unresolved |
| Backtesting foundation | 5 | 2 | 4 | 2 | 1 | 5 | 1 | 1 | Deferred: strategy, execution, P&L, and time scope too broad |
| Scheduling/resilience automation | 2 | 3 | 2 | 2 | 1 | 4 | 3 | 1 | Deferred: clocks, concurrency, recovery, and provider policy |
| Workspace/notebook/visualization | 4 | 2 | 3 | 3 | 2 | 3 | 2 | 2 | Deferred: presentation before durable research artifacts |
| AI/ML foundation | 5 | 1 | 2 | 1 | 1 | 5 | 1 | 1 | Deferred: training/evaluation/model governance not established |

Durable Experiment Result evidence wins because it directly closes the only
ephemeral link in the implemented research chain, reuses established SQLite
and identity patterns, remains fully offline, and forms one bounded vertical
slice. It is Release 1.6-sized because it introduces exactly one durable
artifact and one migration. A registry, feature store, or strategy framework
would be a broader future release, not an incidental extension of this one.

## 6. Selection decision

Release 1.6 selects **Durable Experiment Evidence Foundation**.

The persisted object is exactly one successful Release 1.5 Experiment Result
for `simple-return-descriptive-summary-v1`. Release 1.6 does not persist Feature
Set values. They remain reproducible from the exact Dataset Snapshot and
feature definition; their accepted identities and provenance are referenced.

## 7. Selected capability

The platform can:

1. accept one complete, already validated Experiment Result;
2. validate its identity, summary, provenance, and lineage coherence;
3. persist it atomically as immutable evidence;
4. report `NewlyAccepted` or `EquivalentExisting`;
5. reject contradictory evidence for the same Experiment Result Identity;
6. retrieve the exact evidence by Experiment Result Identity;
7. reconstruct the accepted immutable Application result without semantic loss;
8. reproduce the same outcome after connection and process restart.

There is no update, delete, list, search, comparison, retention, or run-history
operation.

## 8. End-to-end boundary

```text
explicit durable-experiment request
  → existing Release 1.5 experiment generation exactly once
  → validate complete Experiment Result evidence
  → immutable experiment-result store
  → SQLite schema-v3 record accepted atomically
  → exact read-back by Experiment Result Identity
  → NewlyAccepted or EquivalentExisting evidence
  → bounded one-shot Worker presentation and exit
```

The persistence boundary also supports exact lookup independently when an
Experiment Result Identity is supplied. A lookup never regenerates a Feature
Set, executes an experiment, or calls a provider.

## 9. In scope

- provider- and storage-independent Application persistence contracts;
- one immutable Experiment Result store abstraction;
- exact lookup by `ExperimentResultIdentity`;
- a bounded Application durable-experiment orchestration use case;
- complete evidence validation before persistence and after reconstruction;
- `NewlyAccepted` and `EquivalentExisting` dispositions;
- NotFound, unavailable, invalid-evidence, and integrity-conflict distinctions;
- one SQLite experiment-result table and required constraints/indexes;
- schema v2→v3 migration and fresh schema-v3 creation;
- atomic insert/equivalence/conflict handling;
- exact decimal, identity, provenance, and lineage fidelity;
- restart/reopen recovery proof;
- DI/configuration using the existing database-path ownership;
- one explicit, bounded, one-shot durable-experiment Worker mode;
- deterministic permanent Application and Infrastructure coverage;
- architecture and current-state documentation alignment;
- final validation and separately governed integration.

## 10. Out of scope

- Feature Set persistence, feature catalog, or feature cache;
- arbitrary experiment types or configurable aggregate definitions;
- experiment registry, catalog, search, comparison, tagging, or lifecycle;
- invocation/run identity, durable run history, telemetry history, or audit log;
- update, overwrite, delete, retention, compaction, or archival;
- strategy, signal, backtest, portfolio, risk, or trading behavior;
- live acquisition, provider fallback, real credentials, or HTTP execution;
- scheduling, retries, recovery loops, checkpoints, concurrent workers, or DAGs;
- workspace, notebook, UI, API, visualization, or reporting platform;
- AI/ML, model training, evaluation, inference, explainability, or MLOps;
- new projects, packages, or production dependency edges.

## 11. Releases 1.1–1.5 preservation

- **Release 1.1:** historical-observation fidelity, ordering, idempotency,
  conflicts, atomicity, retrieval, connection ownership, and failure mapping
  remain unchanged.
- **Release 1.2:** Dataset Snapshot identity/version, immutable persistence,
  exact lookup, catalog behavior, provenance, lineage, equivalence, and schema-v2
  data remain preserved through migration.
- **Release 1.3:** the fixed pipeline remains exactly five stages;
  `aiq-pipeline-identity-v1`, fail-stop evidence, and pipeline Worker routing do
  not gain an experiment stage.
- **Release 1.4:** `simple-return-lag-1-v1`, `aiq-feature-identity-v1`, exact
  snapshot lookup, in-memory Feature Sets, and feature Worker behavior remain
  unchanged. Feature persistence remains absent.
- **Release 1.5:** `simple-return-descriptive-summary-v1`,
  `aiq-experiment-identity-v1`, in-memory generation, summary arithmetic,
  validation precedence, Feature Set binding, and existing Experiment Worker
  behavior remain unchanged. Persistence is invoked only through the new
  explicit durable boundary.

## 12. Semantic model

The durable semantic artifact is the complete successful Experiment Result,
not a process run and not a database row. Its persisted evidence comprises:

- Experiment Result Identity scheme and fingerprint;
- Experiment Definition Identity scheme and fingerprint;
- exact Feature Set Identity scheme and fingerprint;
- exact Feature Definition Identity referenced by Feature Set provenance;
- exact Dataset Snapshot Identity and Dataset Version;
- the remaining accepted upstream provenance/lineage identity references needed
  to reconstruct and validate the Release 1.5 Experiment Result;
- count;
- aggregate-presence state;
- arithmetic mean, minimum, and maximum when present.

Empty success persists count zero, absent aggregates, and the exact empty
Feature Set binding. Non-empty success persists all three aggregates. Partial
or incoherent evidence is never accepted. Storage metadata cannot change the
meaning of the artifact.

## 13. Identity and equivalence

Release 1.6 reuses `aiq-experiment-identity-v1`. It creates no persistence,
record, invocation, or schema identity.

- same typed Experiment Result Identity plus equivalent canonical semantic
  evidence → `EquivalentExisting`;
- same typed Experiment Result Identity plus any contradictory definition,
  Feature Set, snapshot/version, count, aggregate, provenance, or lineage
  evidence → `IntegrityConflict`;
- absent identity → `NotFound` on lookup;
- persistence disposition does not alter semantic identity;
- row keys, insertion order/time, database path, connection, process, machine,
  Git SHA, and Worker exit code are operational and excluded from equivalence.

The Experiment Result Identity remains the exact lookup key. No surrogate
semantic key is introduced.

## 14. Provenance and lineage

Persisted evidence retains the accepted one-way chain:

```text
source state
  → dataset definition / research dataset
  → dataset snapshot / version
  → feature definition / feature set
  → experiment definition / experiment result
```

Release 1.6 stores the identity-bearing references required to validate and
reconstruct that chain; it does not copy provider payloads, raw observations,
Feature Values, or predecessor canonical bodies into the experiment table.
Persistence is a sink in lineage and never becomes an ancestor of the semantic
Experiment Result.

## 15. Persistence decision

Persist **Experiment Result only**.

Feature Sets remain deterministic and recomputable; storing them is not
required for correctness. Persisting both Feature Sets and Experiment Results
would add two durable models, larger migration and catalog obligations, and an
unjustified cache lifecycle. Result-only persistence is the minimum complete
research-artifact boundary.

Create and read are supported. Re-persistence is idempotent for equivalent
evidence and conflicting for contradictory evidence. There is no update or
delete. Each write is atomic: a complete record is committed or no Release 1.6
state changes. Exact reads are deterministic and have no ordering contract
because lookup returns at most one result.

## 16. Schema decision

Release 1.6 requires **SQLite schema version 3** because a genuinely new durable
`experiment_results` entity is introduced.

The physical model remains Infrastructure-owned and should use a strict,
immutable table keyed by the 64-character lowercase Experiment Result
fingerprint, with explicit scheme/domain constraints, typed predecessor identity
columns, count/presence coherence checks, invariant decimal text, and no update
or delete cascade behavior. Add only indexes required for the exact primary-key
lookup and integrity enforcement; no speculative query indexes are justified.

Migration requirements:

- fresh databases create the complete schema v3 atomically;
- schema v1→v2 behavior remains valid;
- schema v2→v3 preserves all historical observations and Dataset Snapshots;
- migration creates the empty experiment-result structure and changes
  `PRAGMA user_version` only in the same transaction;
- reopening a compatible v3 database is idempotent and non-mutating;
- unsupported or structurally incompatible schemas fail without partial repair;
- no destructive migration, data rewrite, or schema downgrade exists.

## 17. Failure decision

Use bounded, provider-independent failures and reuse predecessor vocabulary when
semantically equivalent:

1. `InvalidRequest` — missing or incoherent store/lookup request;
2. `NotFound` — exact Experiment Result Identity is absent;
3. `DependencyUnavailable` — database/connection cannot serve the operation;
4. `InvalidEvidence` — malformed or unreconstructable semantic evidence;
5. `IntegrityConflict` — the same identity is associated with contradictory
   canonical evidence.

Validation is fail-stop. Persistence establishes no partial success or
fabricated identity. Unknown programming defects propagate and are not broadly
normalized. No retry, overwrite, fallback, repair, or compensation is added.

## 18. Architecture ownership

- **Domain:** zero production delta. Durable Experiment Result storage is an
  Application workflow, not a new market-domain invariant.
- **Application:** owns storage-independent store/lookup contracts, durable
  request/result/failure vocabulary, semantic validation, and orchestration.
- **Infrastructure:** owns SQLite records/mapping, schema v3, migration,
  connection/transaction mechanics, exact persistence/retrieval, and storage
  failure classification.
- **Worker:** owns explicit mode selection, request construction, one-shot
  invocation, bounded presentation, and deterministic exit behavior.
- **Tests:** Application proves semantics with hand-written doubles;
  Infrastructure proves real SQLite, migration, restart, composition, and
  process behavior; Architecture adds only stable non-redundant rules.

The production graph remains Domain → none, Application → Domain,
Infrastructure → Application, Worker → Application and Infrastructure, with
zero cycles.

## 19. DI and configuration impact

Application registration adds the durable orchestration service and
storage-independent contracts using established lifetimes. Infrastructure
registers one SQLite Experiment Result store using the existing
`Persistence:DatabasePath` connection ownership; no second database-path
contract is introduced.

Resolution remains side-effect free: resolving services cannot create or
migrate a database, generate an experiment, persist evidence, or contact a
provider. Configuration validation occurs before execution. Experiment
definition and identity rules remain code-owned, not configurable.

## 20. Worker and hosting decision

Release 1.5 experiment generation remains an in-memory mode. Release 1.6 adds a
separate explicit durable-experiment mode rather than silently making all
experiment executions persistent.

The durable mode executes exactly once per process:

1. construct the exact existing experiment request;
2. generate the Experiment Result once;
3. persist it once through the Application durable use case;
4. optionally perform the bounded exact read-back required to prove acceptance;
5. present safe semantic identity/disposition evidence;
6. return deterministic success/failure status and terminate.

There are no loops, retries, scheduling, provider fallback, hosted background
services, or database-path output. Existing pipeline, Feature, and Experiment
mode precedence remains explicit and unchanged; partial durable intent fails
without falling back to another mode.

## 21. Observability decision

Semantic evidence includes the typed Experiment Result Identity and the
`NewlyAccepted` or `EquivalentExisting` disposition. Operational diagnostics
may report a bounded failure category but do not enter identity or equivalence.

No metrics backend, distributed tracing, durable telemetry, run record,
correlation store, duration identity, or audit-history table is introduced.

## 22. Security and offline decision

Release 1.6 is fully testable offline using synthetic schema-v2/v3 SQLite files
and accepted in-memory Experiment Results. It makes zero Twelve Data/provider
calls and requires no real credential. Database paths, connection strings,
credentials, raw provider payloads, and machine-specific data are excluded from
semantic identity and bounded output. Temporary databases, WAL, SHM, journal,
and process directories must be isolated and removed.

## 23. Package, project, and reference expectations

- package delta: 0;
- project delta: 0;
- project-reference delta: 0;
- solution membership delta: 0;
- production dependency-edge delta: 0.

The existing Microsoft.Data.Sqlite and Microsoft DI/configuration stack is
sufficient. Planning adds no dependency.

## 24. Testing strategy

### Domain

Expected delta zero. Existing value and predecessor invariant tests remain.

### Application

Use hand-written stores/doubles to prove request validation, exact lookup,
`NewlyAccepted`, `EquivalentExisting`, integrity conflict, NotFound,
dependency-unavailable and invalid-evidence propagation, first-failure behavior,
single invocation, unknown-defect propagation, and no fabricated identity.

### Infrastructure

Use isolated offline SQLite to prove fresh v3 creation, v2→v3 migration,
preservation of v1/v2 data, exact insert/read fidelity, empty and non-empty
aggregate reconstruction, idempotency, contradiction detection, atomicity,
restart/reopen recovery, incompatible-schema failure, connection ownership,
side-effect-free DI resolution, Worker process outcomes, and residue cleanup.

### Architecture

Prefer zero delta. Add a rule only if a new stable repository-wide boundary is
not already enforced. Existing 13 rules and the acyclic graph remain mandatory.

### Acceptance

Run all predecessor permanent suites, canonical Release verification, Gitleaks,
formatting, whitespace, schema migration proofs, offline/security checks, and
an exact-SHA fresh checkout before integration acceptance.

## 25. Migration and restart implications

Schema v3 is durable state, so migration and restart are acceptance-critical.
A database containing valid schema-v2 observations and snapshots must reopen as
schema v3 with predecessor rows byte/semantic equivalent and an empty experiment
table. A newly accepted Experiment Result must be exactly retrievable after all
connections are disposed and a new process or connection opens the same file.
An equivalent second write after restart returns `EquivalentExisting`; a
contradictory write remains an integrity conflict. Migration or write failure
must leave the prior committed state usable and must not advance schema version
partially.

## 26. Acceptance principles

Release 1.6 is acceptable only when:

1. one Experiment Result is durably stored and exactly reconstructed;
2. Release 1.5 identity and summary semantics remain unchanged;
3. equivalent re-persistence is idempotent;
4. contradictory evidence cannot overwrite accepted evidence;
5. empty and non-empty results preserve complete fidelity;
6. schema v2→v3 is atomic, non-destructive, and restart-safe;
7. existing v1/v2 data and all Releases 1.1–1.5 tests remain valid;
8. the Worker path is explicit, one-shot, bounded, and offline;
9. no Feature Set or experiment-run persistence appears;
10. the production graph and package/project/reference sets remain unchanged;
11. security, whitespace, residue, canonical, and fresh-checkout gates pass.

## 27. Proposed work-package sequence

This is a definition-level sequence, not an execution plan or file manifest.

| WP | Title | Purpose | Depends on | Expected category delta | Semantic ownership | Model |
| --- | --- | --- | --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | Prove Release 1.5 closure, clean baseline, tests, graph, schema v2, and planning state | Human acceptance/planning | Evidence only | Governance | Luna |
| WP02 | Durable Experiment Evidence Discovery | Freeze durable payload, create/read, immutability, equivalence, conflict, and restart semantics | WP01 | Semantic documentation | Application semantics | Sol |
| WP03 | Persistence Identity, Provenance & Fidelity | Freeze exact identity reuse, persisted provenance/lineage, decimal and empty-result fidelity | WP02 | Semantic documentation | Application semantics | Sol |
| WP04 | Application Persistence Contracts | Define store/lookup requests, results, dispositions, failures, and abstractions | WP03 | Application | Application | Terra |
| WP05 | Durable Experiment Use-Case Integration | Compose existing generation with explicit validation, persistence, and read-back | WP04 | Application | Application | Terra |
| WP06 | Schema-v3 Physical Model | Define experiment-result record, constraints, and non-destructive v2→v3 migration | WP03, WP04 | Infrastructure | Infrastructure storage | Sol |
| WP07 | Experiment Result Persistence | Implement atomic insert, equivalence, and conflict behavior | WP05, WP06 | Infrastructure | Infrastructure storage | Terra |
| WP08 | Exact Experiment Result Retrieval | Implement exact identity lookup and immutable reconstruction | WP07 | Infrastructure | Infrastructure storage | Terra |
| WP09 | Storage Validation & Failure Mapping | Harden invalid evidence, unavailable storage, NotFound, conflict, and unknown-defect boundaries | WP07, WP08 | Application/Infrastructure | Split by abstraction/mechanics | Sol |
| WP10 | Dependency Registration & Configuration | Register the durable graph over existing database-path ownership and prove side-effect-free resolution | WP09 | Application/Infrastructure | Composition | Terra |
| WP11 | One-Shot Durable Experiment Worker | Add explicit bounded generation→persistence→read-back execution without changing existing modes | WP10 | Worker | Worker composition | Terra |
| WP12 | Application & Infrastructure Persistence Tests | Add permanent semantic, migration, fidelity, atomicity, restart, DI, and process coverage | WP11 | Tests | Test boundaries | Luna |
| WP13 | Architecture & Documentation Alignment | Reconcile stable boundaries and current-state documentation; prefer zero architecture-test delta | WP12 | Architecture tests/docs | Architecture/documentation | Terra |
| WP14 | Full Validation, Integration & Acceptance | Reconcile candidate, run full and fresh-checkout proof, and separately govern integration | WP13 | Validation/integration | Release governance | Sol |

The critical path is WP01 → WP02 → WP03 → WP04 → WP05; WP06 follows the
frozen semantics/contracts; WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13 →
WP14 completes the vertical slice. Execution-plan design may refine parallelism
but must not change these semantic dependencies without human approval.

## 28. Model recommendations

- **Sol:** WP02, WP03, WP06, WP09, WP14, where semantic ambiguity, migration,
  failure precedence, or acceptance risk is highest.
- **Terra:** WP04, WP05, WP07, WP08, WP10, WP11, WP13, where frozen semantics
  are translated into bounded contracts, storage, composition, hosting, and
  documentation.
- **Luna:** WP01 and WP12, where authorities are settled and work is primarily
  deterministic reconciliation or permanent coverage.

## 29. Release 1.7+ deferrals

The following remain future capabilities without assigned release numbers:

- Feature Set persistence, catalog, and cache;
- additional experiment definitions and generalized statistics;
- experiment registry, history, comparison, search, retention, and workspace;
- operational run identity and durable pipeline/feature/experiment history;
- strategies, signals, backtesting, portfolio, risk, and trading;
- controlled live acquisition and multi-provider policy;
- scheduling, retries, circuit breakers, checkpoints, and recovery;
- notebooks, visualization, UI, API, and reporting surfaces;
- AI/ML, model training/evaluation/inference, explainability, and MLOps;
- generalized plugins, configurable DAGs, distributed execution, and durable
  telemetry backends.

## 30. GitHub planning recommendation

After human acceptance, separately design the Release 1.6 execution plan and
file manifest. Only after those authorities are accepted should a GitHub
planning authority reconcile the appropriate milestone and create work-package
issues. Legacy milestone #47 must not be renamed or repurposed without explicit
reconciliation authority.

## 31. Risks

- storing insufficient provenance would make exact reconstruction impossible;
- copying full Feature Sets would accidentally expand scope into feature
  persistence;
- loose decimal/text constraints could admit multiple physical forms for one
  semantic value;
- migration defects could endanger accepted v1/v2 data;
- automatic persistence could silently change Release 1.5 behavior;
- broad exception normalization could hide integrity or programming defects;
- speculative indexes/query APIs could become an accidental registry.

Mitigation is the narrow result-only table, exact identity lookup, strict
constraints, transaction-bound migration, explicit Worker mode, exhaustive
offline migration/restart tests, and fail-stop behavior.

## 32. Non-goals

Release 1.6 is not a generic persistence framework, Feature Store, experiment
platform, strategy engine, backtester, scheduler, notebook environment, API,
analytics library, or ML platform. It does not optimize repeated feature
generation, automate acquisition, compare experiments, or retain invocation
history.

## 33. Final decision

**Selected:** Phase 4 — Release 1.6: Durable Experiment Evidence Foundation.

**Persistence:** Experiment Result only.

**Identity:** reuse `aiq-experiment-identity-v1`; no persistence identity.

**Schema:** evolve SQLite atomically from version 2 to version 3.

**Execution:** preserve Release 1.5 in-memory generation and add a separate
explicit one-shot durable-experiment path.

This is the smallest coherent capability that turns the platform's first
quantitative research conclusion into a durable, reproducible artifact without
prematurely introducing a registry, strategy framework, feature store, or
operational automation.

## 34. Next authorized action

Human acceptance of this definition, followed by separately governed Release
1.6 execution-plan and file-manifest design. No implementation or GitHub
planning is authorized by this document.

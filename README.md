![AIQuantTradingResearch Logo](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/blob/main/imgs/AIQuantTradingResearchBanner-01.jpg?raw=true)

# AIQuantTradingResearch

> A production-oriented quantitative research and platform-engineering project built around deterministic market-data pipelines, durable evidence, governed .NET/Python interoperability, observability, and reproducible public-reference deployment.

[![Release](https://img.shields.io/badge/release-1.8-blue)](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestones)
[![Tests](<https://img.shields.io/badge/tests-281%20passing-brightgreen>)](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/tree/main/tests)
[![Architecture Tests](<https://img.shields.io/badge/architecture%20tests-13%20passing-brightgreen>)](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/tree/main/tests/AIQuantTradingResearch.Architecture.Tests)
[![.NET](https://img.shields.io/badge/.NET-C%23-512BD4?logo=dotnet)			](https://dotnet.microsoft.com/)
[![Python](https://img.shields.io/badge/Python-3.13-3776AB?logo=python)](docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md)
[![Docker](https://img.shields.io/badge/Docker-containerized-2496ED?logo=docker)](Dockerfile)
[![License: MIT](https://img.shields.io/badge/license-MIT-yellow.svg)](LICENSE)

AIQuantTradingResearch demonstrates how quantitative research capability can evolve as a maintainable software platform. Architecture, executable quality gates, incremental delivery, transparent technical decisions, and disciplined AI-assisted engineering are treated as part of the product.

	The project is intentionally broader than a collection of trading algorithms or ML experiments. It demonstrates how market-data capabilities can be designed as a maintainable software platform while creating a foundation for later quantitative analytics, AI/ML research, observability, resilience, and cloud-native operation.

**Current closed milestone:** **[Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification — FEASIBLE](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/62)**

**Current accepted milestone:** **[Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization — IN PROGRESS](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/63)**

[What Works Today](#what-works-today) · [Architecture](#architecture) · [Run &amp; Verify](#run--verify) · [Engineering Evidence](#engineering-evidence) · [Implemented Technology](#implemented-technology) · [Roadmap](#engineering-capability-journey) · [Engineering Handbook](#engineering-handbook)

---

## What Works Today

- **Acquisition & Persistence (v1.1):** High-fidelity Twelve Data ingestion to SQLite.
- **Dataset Snapshots (v1.2):** Bounded `[from, to)` datasets tied to deterministic SHA-256 identities.
- **Research Pipeline (v1.3):** Structured 5-stage orchestration from retrieval to registration.
- **Feature Generation (v1.4):** Pure mathematical extraction (`simple-return-lag-1-v1`) over immutable states.
- **Experiment Metrics (v1.5):** Descriptive aggregate computations (mean, min, max).
- **Durable Evidence (v1.6):** Schema v3 persistence mapping outcomes to `NewlyAccepted` or `IntegrityConflict` states.
- **Evidence Discovery (v1.7):** Mandatory bounded discovery (`MaximumResultCount`) returning ordered binary-sorted collections.
- **Python Engineering Foundation (v1.8):** Isolated CPython scientific-stack tooling with a governed and tested one-shot JSON-over-stdio interoperability boundary while .NET remains the canonical pipeline owner.
- **Governed Real-Time-Style Visualization (v1.9):** Canonical .NET visualization read model with atomic JSON handoff to Python/Streamlit, deterministic/replay/simulated provenance disclosure, and truthful `Ready` / `WarmUp` / `Empty` / `Failed` presentation states.
- **OpenTelemetry & Pipeline Observability (v1.10):** OpenTelemetry-based pipeline and boundary observability with truthful Streamlit System Health diagnostics while preserving existing .NET, Python, persistence, and visualization ownership boundaries.
- **Public Reference Deployment Feasibility (Initiative-1.11):** Azure App Service Linux F1 feasibility qualification completed as **FEASIBLE**, validating a public Docker/GHCR reference-deployment path with HTTPS, persistent `/home`, writable SQLite using DELETE journal mode, and a strict `$0.00` recurring-infrastructure-cost boundary.

Historical data flows from Twelve Data into durable SQLite and transformed through an Application-owned pipeline into immutable datasets, deterministic features, experiments, provenance, lineage, and bounded discovery. .NET governs Python 3.13/JSON interoperability, Streamlit visualization, and OpenTelemetry health. A supervised non-root Docker runtime supports public GHCR images and automated Azure App Service F1 deployment with HTTPS, persistent /home, and SQLite compatibility.

> Azure hosts a bounded public reference/demo deployment. F1 has shared capacity, cold starts, throttling, a 60 CPU-minute daily allowance, 1 GB storage, and no SLA. This is not production hosting or production trading architecture.

### [Release 1.1: Market Data Persistence Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/52)

Establish durable, provider-independent historical market-data persistence so normalized observations can be stored, reconstructed, retrieved, and reused deterministically while storage technology remains confined to Infrastructure.

Release 1.1 completes the first provider-backed, durable historical market-data vertical slice.

```text
Twelve Data
    │
    ▼
Infrastructure provider adapter
    │
    ▼
Application use cases
    │
    ├──────────────► Domain model
    │
    ▼
Infrastructure persistence adapter
    │
    ▼
SQLite
```

The current implementation can:

- Acquire historical market observations through the Twelve Data provider integration.
- Keep provider and storage details outside the Domain and Application layers.
- Persist historical observations durably in local SQLite storage.
- Retrieve historical observations deterministically in ascending chronological order.
- Preserve exact target, timestamp/offset, and decimal-value fidelity across persistence.
- Distinguish newly accepted, idempotent, and conflicting persistence outcomes.
- Preserve immutable accepted history and atomic write behavior.
- Map controlled storage failures through Application-owned failure contracts.
- Compose acquisition and persistence through an externally configured Worker execution root.
- Validate architectural boundaries with executable architecture tests.

### [Release 1.2: Research Dataset Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/53)

Establish deterministic, versioned, reproducible, and discoverable research datasets with bounded metadata,
provenance, lineage, and catalog capabilities by reusing Release 1.1 durable historical observations.

Release 1.2 builds on Release 1.1 historical observations. It materializes one
exact-target, `[from,to)` research dataset definition into immutable SQLite
snapshot/catalog evidence. Definitions are ordered by semantic instant; empty
selection is valid; original offsets and decimal values are preserved. The
`aiq-dataset-identity-v1` scheme uses deterministic SHA-256-backed identities
for the definition, research dataset, source state, and snapshot; the snapshot
identity is also the immutable dataset version. Re-running equivalent evidence
is recognized without overwrite, while conflicts remain explicit.

Release 1.2 established the externally configured bounded dataset execution using
`Persistence:DatabasePath`, `Dataset:Target`, `Dataset:From`, and `Dataset:To`.
It remains the persistence foundation composed by Release 1.3.

### [Release 1.3: Research Pipeline Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/54)

Deliver a deterministic fixed one-shot research pipeline by reusing persisted Release 1.1 historical observations
and Release 1.2 dataset materialization, snapshot, and catalog capabilities.

Release 1.3 composes persisted historical observations and the Release 1.2
dataset foundation into one fixed, deterministic, five-stage pipeline:
historical observation retrieval, dataset materialization, immutable snapshot
persistence, catalog registration, and structured result/evidence. Application
owns pipeline contracts, identities, orchestration, validation, and semantic
evidence; Infrastructure owns provider and SQLite mechanics; Worker remains the
outer one-shot composition and trigger boundary.

The `aiq-pipeline-identity-v1` definition and semantic execution identities are
distinct from `aiq-dataset-identity-v1`. Equivalent reruns preserve semantic
execution identity while reporting `NewlyAccepted` or `EquivalentExisting` as
non-identity-bearing dispositions. Empty datasets are valid successes. Failures
stop at the first failing stage and expose only established upstream evidence.
Live acquisition is not a pipeline stage, and the release adds neither schema
evolution nor durable pipeline run history.

### [Release 1.4: Deterministic Feature Engineering Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/45)

Deliver a deterministic, one-shot Feature Engineering Foundation over canonical Release 1.2 dataset snapshots.
The only built-in computation is simple-return-lag-1-v1. Application owns feature semantics and orchestration;
output is in-memory and SQLite remains schema version 2. Feature persistence/catalog/cache, plugins,
generalized DAGs, scheduling, retries, live acquisition, model training, and MLOps are excluded.

Release 1.4 adds one separate, deterministic feature-generation use case over
an exact accepted immutable snapshot. The sole built-in definition is
`simple-return-lag-1-v1`, with decimal result `r[i] = (p[i] / p[i-1]) - 1`.
Application owns feature identities, provenance, validation, exact snapshot
lookup, and computation; Infrastructure reuses snapshot storage without feature
persistence; Worker selects feature mode, executes once, presents bounded
evidence, and exits. `aiq-feature-identity-v1` keeps distinct deterministic
Feature Definition and Feature Set identities bound to the exact snapshot and
version. Empty and single-observation snapshots are successful empty feature
sets. This is not a sixth Release 1.3 pipeline stage, a provider acquisition
path, or a feature engine.

At the Release 1.4 boundary SQLite remains schema version 2. No feature table, catalog, cache, scheduler,
retry loop, or durable feature run history exists.

### [Release 1.5: Deterministic Research Experiment Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/46)

Establish one deterministic offline research experiment over accepted simple-return feature evidence,
producing immutable count, arithmetic mean, minimum, and maximum evidence with canonical experiment identity and provenance.

Release 1.5 adds one separate deterministic experiment over an exact accepted
`simple-return-lag-1-v1` Feature Set: `simple-return-descriptive-summary-v1`.
Application owns immutable experiment contracts, validation, decimal summary
computation, Feature Set-to-Experiment provenance/lineage, and distinct
`aiq-experiment-identity-v1` definition/result identities. Empty Feature Sets
succeed with count zero and absent aggregates; non-empty evidence contains exact
count, arithmetic mean, minimum, and maximum. Results are in-memory only and
remain bound to the exact Feature Set/snapshot evidence.

Worker selects Experiment mode only when `Experiment:SnapshotIdentity` or
`Experiment:SnapshotVersion` is present; that explicit mode takes precedence
over Feature mode, which takes precedence over the existing five-stage pipeline.
Partial Experiment configuration fails without fallback. Experiment execution
does not acquire provider data, persist results, create experiment tables, or
alter SQLite schema v2.

### [Release 1.6: Durable Experiment Evidence Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/47)

Make accepted deterministic Experiment Result evidence durably persistent and exactly retrievable while preserving Release 1.5 semantics through atomic SQLite schema v2→v3 evolution, with no Feature Set persistence, generalized registry/history.

Release 1.6 persists accepted `simple-return-descriptive-summary-v1` Experiment Result evidence in schema v3. `experiment_results` is the single immutable durable-result table. Its exact `aiq-experiment-identity-v1` result identity is the lookup key: first acceptance is `NewlyAccepted`, equivalent reacceptance is `EquivalentExisting`, and contradictory same-identity evidence is `IntegrityConflict`. Exact lookup is read-only and returns durable reduced evidence or `NotFound`; it neither regenerates Feature Values nor calls a provider.

The explicit Durable Experiment Worker mode uses `DurableExperiment:SnapshotIdentity` and `DurableExperiment:SnapshotVersion`, with precedence Durable Experiment → Experiment → Feature → five-stage pipeline. Partial durable intent fails without fallback. Feature Set persistence, experiment registry/history/search, update/delete, retry, and provider acquisition remain deferred.

### [Release 1.7: Durable Experiment Evidence Discovery](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/55)

Bounded deterministic discovery of immutable durable Experiment Result evidence by exact Dataset Snapshot and Experiment Definition context from Release 1.6. Preserves SQLite schema v3.

Release 1.7 adds read-only bounded discovery of accepted durable Experiment
Results for one exact Snapshot Identity and Experiment Definition Identity.
`DurableExperimentDiscovery:MaximumResultCount` is mandatory and positive;
matches are ordered by binary Experiment Result Identity ascending, and no
matches return a successful immutable empty collection rather than `NotFound`.
Discovery reuses `aiq-experiment-identity-v1` and complete durable evidence
without generating, accepting, changing, or repairing Experiment Results.

The Worker selects Discovery before Durable Experiment, Experiment, Feature,
and the fixed pipeline. Partial or malformed discovery intent fails without
fallback. Discovery remains schema-v3 read-only over `experiment_results`, uses
no provider or network fallback, and adds no registry, history, search,
pagination, new index, or persistence mutation.

### [Release 1.8: Python & AI Engineering Foundation](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/56)

Establish a governed Python 3.13 engineering foundation alongside the existing .NET research platform, enabling scientific-computing interoperability without transferring pipeline ownership, persistence responsibility, or application orchestration to Python.

Release 1.8 introduces an isolated CPython scientific-stack environment and a
tested one-shot JSON-over-stdio interoperability boundary. .NET remains the
canonical owner of application orchestration and invokes Python through an
Infrastructure-owned adapter using explicit request/response contracts. Python
executes only within that bounded invocation and does not become an alternative
research pipeline or application host.

The integration establishes deterministic serialization, process execution,
response parsing, validation, timeout and failure handling, and clean process
termination. Python dependencies are explicitly pinned and independently
validated, providing a reproducible foundation for later quantitative,
visualization, and machine-learning capabilities without coupling the Domain or
Application layers to the Python runtime.

Release 1.8 does not introduce model training, machine learning, backtesting,
live trading, Python-owned persistence, direct SQLite access from Python, a
parallel research pipeline, or long-running Python process supervision. The
JSON-over-stdio boundary remains a distinct interoperability mechanism owned and
controlled by .NET.

### [Release 1.9: Real-Time Financial Data Visualization](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/58)

Deliver governed real-time-style financial-data visualization through a canonical .NET-owned visualization read model and atomic JSON handoff consumed by read-only Python 3.13 and Streamlit presentation code.

Release 1.9 adds a distinct visualization path while preserving the established
research architecture. .NET owns construction of the canonical visualization
read model and publishes it through an atomic JSON handoff. Python parses that
contract into presentation-oriented frames, while Streamlit remains a
read-only visualization surface rather than an application, persistence,
provider, or process-supervision boundary.

The visualization contract exposes truthful `Ready`, `WarmUp`, `Empty`, and
`Failed` states and preserves explicit provenance for deterministic, replay,
and simulated data. Simulated or replayed observations are never represented as
live market activity. The handoff is designed for safe concurrent publication
and consumption, including bounded atomic-replacement behavior on Windows.

Release 1.9 keeps the visualization JSON handoff separate from the Release 1.8
one-shot JSON-over-stdio interoperability boundary. Streamlit does not access
SQLite or market-data providers directly, supervise the .NET Worker, or create
an alternative research pipeline. The release establishes governed
real-time-style visualization, not live trading or an execution platform.

### [Release 1.10: OpenTelemetry & Pipeline Observability](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/59)

Establish governed OpenTelemetry-based observability across the research pipeline and cross-language boundaries, with truthful System Health diagnostics exposed through the existing read-only Streamlit presentation surface.

Release 1.10 adds structured observability without changing ownership of the
research pipeline or creating a parallel execution path. .NET remains the
canonical pipeline owner and emits governed telemetry across application,
infrastructure, persistence, visualization, and Python interoperability
boundaries. OpenTelemetry provides the instrumentation foundation while
preserving deterministic execution semantics and existing architectural
separation.

The canonical visualization handoff is extended with truthful System Health
evidence consumed by Python 3.13 and Streamlit. Health presentation distinguishes
operational states without allowing Streamlit to supervise the Worker, access
SQLite directly, invoke providers, or become an execution authority. Existing
deterministic, replay, and simulated provenance remains explicitly disclosed and
is never represented as live trading.

Release 1.10 preserves SQLite schema v4, the one-shot JSON-over-stdio Python
interoperability boundary, and the independent canonical JSON visualization
handoff. It introduces no live trading, model training, backtesting, provider
ownership changes, direct SQLite UI access, or parallel research pipeline.

### [Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestone/62)

Qualify whether the existing governed application can operate as a publicly accessible reference deployment on Azure App Service Linux F1 while preserving its architecture, SQLite persistence model, provider boundaries, truthful provenance, and a strict zero-recurring-infrastructure-cost constraint.

Initiative-1.11 validated Azure App Service Linux F1 in West Central US with a
custom Docker container, public HTTPS/DNS, persistent `/home`, writable SQLite
using DELETE journal mode, public/free GHCR image distribution, and bounded
authenticated Twelve Data connectivity. Persistence, restart/redeployment
behavior, CRUD integrity, contention handling, provider-secret isolation,
failure behavior, and public deployment connectivity were exercised as part of
the feasibility qualification.

The initiative concluded:

`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: FEASIBLE`

with observed recurring infrastructure cost:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

This qualification is explicitly a reference/demo deployment boundary rather
than a production hosting claim. Azure remains an external deployment target,
not an application dependency, and the result does not introduce a production
SLA, paid Azure services, Azure SQL, Azure Container Apps, Azure Files,
mandatory ACR, live trading, or architectural bypasses.

**Initiative-1.11 is not Product Release 1.11.** Product Release 1.11 remains
abandoned/nonexistent; the feasibility evidence from this initiative is consumed
by Release 1.12 Public Reference Deployment Implementation & Stabilization.

| Evidence                                    | Current baseline |
| --------------------------------------------- | -----------------: |
| Permanent automated tests                   |  **281 passing** |
| Architecture tests                          |   **13 passing** |
| Build warnings                              |            **0** |
| Build errors                                |            **0** |
| Canonical repository verification           |         **PASS** |
| Provider/network calls in persistence tests |            **0** |
| Production dependency cycles                |            **0** |

### Release 1.1 showcase

Release 1.1 is a foundation, not a claim that the full quantitative trading vision is complete. Streaming/live feeds, provider failover, trading execution, AI/ML models, APIs, scheduling, advanced resilience, and production deployment remain future capabilities.

The completed vertical slice has also been exercised manually against real AAPL market data and independently inspected at the SQLite boundary. The evidence demonstrates durable persistence, idempotent repeat execution, zero duplicate logical history, database integrity, and restart recovery.

![Release 1.1 Showcase](imgs/release-1.1-showcase.png)

For reproducible steps behind this evidence, see the **[Platform Execution & Verification Guides](docs/guides/README.md)**.

---

## Architecture

The solution uses explicit dependency direction and layer ownership.

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

### Layer responsibilities


| Layer              | Responsibility                                                                                              |
| -------------------- | ------------------------------------------------------------------------------------------------------------- |
| **Domain** | Quantitative concepts and invariants without provider, storage, deployment, or presentation dependencies. |
| **Application** | Use cases, contracts, identities, orchestration, and semantic evidence. |
| **Infrastructure** | Twelve Data, SQLite, telemetry adapters, and the governed local Python process boundary. |
| **Worker** | Composition and bounded execution of the canonical .NET pipeline. |
| **Python/Streamlit** | Read-only consumption and presentation of governed handoff evidence. |

.NET remains the canonical pipeline owner. Streamlit does not directly own provider access, SQLite access, or Worker supervision. The Release 1.8 JSON-over-stdio interoperability boundary and Release 1.9 visualization handoff remain distinct governed contracts. Azure is deployment-only and introduces no Domain or Application dependency.

### Current solution structure

```text
AIQuantTradingResearch.slnx
├── src/
│   ├── AIQuantTradingResearch.Domain
│   ├── AIQuantTradingResearch.Application
│   ├── AIQuantTradingResearch.Infrastructure
│   └── AIQuantTradingResearch.Worker
└── tests/
    ├── AIQuantTradingResearch.Domain.Tests
    ├── AIQuantTradingResearch.Application.Tests
    ├── AIQuantTradingResearch.Infrastructure.Tests
    └── AIQuantTradingResearch.Architecture.Tests
```

The architecture test suite makes dependency direction, ownership, visibility, provider confinement, storage independence, and acyclicity executable rather than relying only on documentation.

---

## Run & Verify

### 🚀 Start Here — Run the Platform Locally

Want to see AIQuantTradingResearch working with real market data?

Start with the **[Local Platform Execution](docs/guides/LOCAL_PLATFORM_EXECUTION.md)** guide — the project's short **Hello World** path from local setup to a real provider-backed execution:

```text
Twelve Data
     ↓
Real historical market data
     ↓
AIQuantTradingResearch
     ↓
SQLite
     ↓
SUCCESS
```

For deeper, independently reproducible evidence, continue with the **[Platform Execution &amp; Verification Guides](docs/guides/README.md)** covering real provider acquisition, durable persistence, idempotent retries, data integrity, and restart recovery.

### Prerequisites

The repository is built around the .NET SDK version pinned by `global.json`.

Release 1.8 also uses machine CPython **3.13.15** as the base runtime and an
ignored, disposable repository-local `.venv` for every project dependency.
The four direct pins are NumPy 2.5.1, pandas 3.0.5, scikit-learn 1.9.0, and
Streamlit 1.61.1. See the [Python developer environment guide](docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md)
and the [interoperability boundary](docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md).
The Python foundation supplies no product ML model, training workflow, or
Streamlit product application. Release 1.9 is planned as real-time financial
data visualization; lightweight ML evaluation is separately planned for
Release 2.0.

For the provider-backed execution path, configuration is supplied externally:

- `TwelveData:ApiKey` — Twelve Data API key.
- `Persistence:DatabasePath` — local SQLite database path.
- `Dataset:Target`, `Dataset:From`, and `Dataset:To` — explicit dataset input;
  timestamps use invariant round-trip `DateTimeOffset` values.
- `Experiment:SnapshotIdentity`, `Experiment:SnapshotVersion` — exact
  immutable snapshot/version input for the code-owned experiment definition.
- `DurableExperimentDiscovery:SnapshotIdentity`,
  `DurableExperimentDiscovery:ExperimentDefinitionIdentity`, and
  `DurableExperimentDiscovery:MaximumResultCount` — exact bounded
  read-only durable-evidence discovery input; all three are mandatory.

Secrets and environment-specific paths should not be committed to the repository.

### Verify the repository

From the repository root:

```powershell
./eng/restore.ps1
./eng/format.ps1
./eng/build.ps1
./eng/test.ps1
./eng/verify.ps1
```

Optional cleanup:

```powershell
./eng/clean.ps1
```

`format.ps1` verifies formatting without rewriting files. `verify.ps1` is the canonical quality gate and delegates the repository restore, formatting verification, build, and test workflow.

A cross-platform build counterpart is available at `eng/build.sh`.

The container is defined by [Dockerfile](Dockerfile). Release 1.12 deployment automation is under [`eng/azure-cli/r1.12-deployment/`](eng/azure-cli/r1.12-deployment/). Credentials, API keys, auth profiles, and machine-local paths must remain outside source control.

The repository validates Domain, Application, Infrastructure, Architecture, and Python behavior through automated suites and canonical engineering scripts. Exact test counts are intentionally omitted because they are volatile; current validation output is authoritative.

### Current execution flow

At Release 1.3, the Worker performs one bounded pipeline invocation and exits:

```text
External dataset and storage configuration
        │
        ▼
      Worker
        │
        ▼
Application-owned fixed Research Pipeline
        │
        ▼
Persisted historical observations → immutable dataset evidence
        │
        ▼
SQLite schema v2 snapshot/catalog evidence
```

The Worker presents bounded semantic evidence and terminates. There is no
pipeline-managed provider acquisition, scheduler, retry, refresh loop, DAG,
checkpoint/resume path, or durable pipeline-run history.

Release 1.4 adds a separate bounded feature mode selected by exact
`Feature:SnapshotIdentity` and `Feature:SnapshotVersion`. It resolves the
Application feature use case once over existing snapshot evidence and exits; it
does not invoke Twelve Data, add a pipeline stage, or persist feature output.

Release 1.5 adds an explicit one-shot Experiment mode selected by exact
`Experiment:SnapshotIdentity` and `Experiment:SnapshotVersion`. It invokes the Application experiment use case once over existing Feature Set evidence, emits
bounded semantic result/failure evidence, and terminates. It is not a sixth
pipeline stage, a provider fallback, an experiment registry, or durable history.

Release 1.7 adds an explicit one-shot Durable Experiment Evidence Discovery
mode. It requires exact Snapshot and Experiment Definition identities plus a
positive maximum, invokes the Application discovery use case once, presents
ordered bounded durable evidence (including successful empty results), and
exits. Its precedence is Discovery → Durable Experiment → Experiment →
Feature → pipeline; it does not write SQLite, regenerate evidence, or call a
provider.

---

## Engineering Evidence

The platform makes its engineering state inspectable through immutable accepted evidence, deterministic identities and provenance, explicit idempotent/conflict/failure semantics, executable architecture rules, governed process boundaries, OpenTelemetry telemetry, secret-safe deployment automation, and exact Git/lifecycle governance.

AI assists implementation and analysis inside explicit contracts, mutation boundaries, validation gates, and reviewable pull requests. It does not replace architecture, testing, or acceptance evidence.

### Persistence semantics

Release 1.1 establishes explicit behavior for durable historical observations:

- **NewlyAccepted** — a previously unseen observation is durably accepted.
- **Idempotent** — an equivalent observation already exists and does not create duplicate history.
- **Conflict** — the same observation identity exists with incompatible data and accepted history is not destructively replaced.
- **Unavailable** — an accepted infrastructure/storage availability failure.
- **InvalidData** — invalid input or persisted data that cannot satisfy the established contract.

Persistence is designed around immutable accepted history, deterministic retrieval, atomic writes, and explicit fidelity guarantees.

### Testing strategy

The current permanent test baseline covers:

- Domain invariants.
- Application persistence contracts.
- Persistence use-case behavior.
- Twelve Data provider behavior.
- SQLite schema and bootstrap.
- Connection lifecycle.
- Persistence and retrieval semantics.
- Idempotency and conflict behavior.
- Atomic rollback.
- Timestamp/offset/decimal fidelity.
- Storage failure mapping.
- Dependency injection and configuration.
- Fixed pipeline identity, orchestration, validation, failure, and evidence semantics.
- Offline composition and separate one-shot Worker-process validation.
- Read-only bounded durable Experiment Evidence Discovery, including exact
  dual-identity filtering, binary identity ordering, empty success, DI, and
  offline Worker-process routing.
- Executable architecture rules.

SQLite persistence tests use isolated local databases and deterministic cleanup. Provider/network access is not required by the persistence test suite.

### Engineering automation

The `eng/` scripts provide repeatable repository operations for restore, formatting verification, build, tests, canonical verification, and cleanup.

The goal is a repository that can prove its expected engineering state instead of relying on manual inspection alone.

---

## AI-Assisted Engineering Approach

AIQuantTradingResearch is also an experiment in **AI-assisted software engineering as a disciplined engineering workflow**, rather than AI-generated code without governance.

Work is decomposed into bounded releases and work packages. Architecture, contracts, acceptance criteria, repository constraints, tests, and validation gates constrain implementation before a work package is considered complete.

The workflow emphasizes:

```text
Engineering intent
      ↓
Architecture & contracts
      ↓
Bounded work package
      ↓
AI-assisted implementation
      ↓
Automated tests & architecture rules
      ↓
Canonical validation
      ↓
Evidence & release acceptance
```

AI assists the engineering process; it does not replace architecture, testing, reviewability, or explicit technical decisions.

---

## What Makes This Project Different


| Typical prototype-oriented project         | AIQuantTradingResearch                                        |
| -------------------------------------------- | --------------------------------------------------------------- |
| Code first                                 | Architecture and contracts before implementation              |
| Documentation added later                  | Documentation treated as part of the deliverable              |
| Design decisions remain implicit           | Decisions and trade-offs are made visible                     |
| Tests focus only on functional behavior    | Functional and architecture rules are executable              |
| Infrastructure leaks into core logic       | Provider/storage concerns are kept behind boundaries          |
| Large feature drops                        | Incremental releases and bounded work packages                |
| AI used primarily for code generation      | AI used inside a governed engineering lifecycle               |
| Current code presented as the final vision | Implemented and planned capabilities are explicitly separated |

---

## Implemented Technology

The current executable platform is centered on:
- C# and .NET
- Python 3.13 interoperability
- Streamlit
- Twelve Data historical acquisition
- SQLite durable evidence
- JSON-over-stdio interoperability
- canonical JSON visualization handoff
- OpenTelemetry-based observability and System Health
- Dockerized runtime composition
- public GHCR container publication
- Azure App Service Linux F1 reference-deployment automation
- PowerShell engineering and deployment automation
- Automated Domain, Application, Infrastructure, Architecture, and Python validation
- GitHub issues, milestones, Project workflow, and documented AI-assisted governance

Implemented deployment capability does not include Azure SQL, Azure Files, Container Apps, ACR, paid monitoring, Kubernetes, ML, backtesting, live trading, or a production SLA.

---

## Planned Technology & Platform Direction

Release 1.12 remaining stabilization covers:

- persistent `/home` SQLite initialization, data update, integrity, and recovery;
- Twelve Data runtime configuration, secret handling, and bounded automation;
- deployed public Streamlit/System Health behavior;
- restart, recycle, redeployment, recovery, cost, and no-bypass validation;
- operational runbooks and final release acceptance.

Later governed direction includes:

- an independent Azure SQL Database Free Offer investigation and architecture decision—it is not adopted by Release 1.12;
- Phase 5 Release 2.0 lightweight machine-learning evaluation;
- Release 2.1 Machine Learning;
- Release 2.2 Explainable AI;
- Release 2.3 Backtesting;
- later resilience, scheduling, and analytics evolution where justified.

Python interoperability, Docker deployment, and OpenTelemetry observability are implemented foundations rather than future-only claims.

---

## Engineering Capability Journey

Each release is intended to add a concrete platform capability while strengthening the engineering system around it.


| Release      | Engineering capability                                                                    |
| -------------- | ------------------------------------------------------------------------------------------- |
| **0.1–0.6** | Architecture, governance, design, resilience, and implementation foundations              |
| **0.7**      | AI Engineering Toolkit                                                                    |
| **0.8**      | Executable .NET solution skeleton                                                         |
| **0.9**      | Build, CI, and platform bootstrap evolution                                               |
| **1.0**      | Provider-backed historical market-data acquisition                                        |
| **1.1** | Durable market-data persistence and deterministic retrieval |
| **1.2** | Immutable datasets, snapshots, versions, and catalog evidence |
| **1.3** | Fixed deterministic research pipeline |
| **1.4** | Deterministic feature generation |
| **1.5** | Deterministic research experiment evidence |
| **1.6** | Durable experiment evidence |
| **1.7** | Bounded durable experiment evidence discovery |
| **1.8** | Python interoperability and governed JSON-over-stdio boundary |
| **1.9** | Governed real-time-style visualization and canonical JSON handoff |
| **1.10** | OpenTelemetry pipeline/boundary observability and truthful System Health |
| **Initiative-1.11** | Azure App Service F1 public-reference feasibility qualification — **FEASIBLE** |
| **1.12** | Public Reference Deployment Implementation & Stabilization — **IN PROGRESS; WP01–WP03 accepted, WP04 next** |
| **2.0** | Lightweight Machine Learning Evaluation — planned |
| **2.1** | Machine Learning — planned |
| **2.2** | Explainable AI — planned |
| **2.3** | Backtesting — planned |

The roadmap evolves incrementally. Completed releases represent implemented evidence; future releases represent direction until formally defined and accepted. The canonical future sequence is 1.9 Visualization → 1.10 Observability → 2.0 Lightweight ML Evaluation → 2.1 Machine Learning → 2.2 Explainable AI → 2.3 Backtesting.

### Public engineering roadmap

The project is planned transparently through GitHub milestones, issues, and the public **AIQuantTradingResearch Engineering Roadmap** project.

- [Engineering Roadmap Project](https://github.com/users/samuel-santos-engineer/projects/2)
- [Repository Issues](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues)
- [Repository Milestones](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/milestones)

This provides traceability from platform direction to release milestones, bounded work packages, implementation, tests, and acceptance evidence.

---

## Why This Project Exists

Many quantitative repositories emphasize strategies, notebooks, or prediction accuracy.

AIQuantTradingResearch explores a complementary question:

> How can a long-lived quantitative research platform evolve data sources, evidence models, analytics, and operations without sacrificing architecture, reproducibility, or maintainability?

The project therefore gives deliberate attention to:

- Architecture and dependency management.
- Engineering governance.
- Testability and deterministic validation.
- Documentation-driven development.
- Explicit trade-offs and technical decisions.
- DevOps and Site Reliability Engineering principles.
- Incremental platform evolution.
- AI-assisted engineering under explicit constraints.

The objective is not merely to produce working features, but to build a transparent reference implementation of the engineering practices required to evolve them responsibly.

---

## Engineering Principles

The project is guided by a small set of enduring principles:

- Architecture before implementation.
- Documentation is part of the deliverable.
- Incremental delivery.
- Automation by default.
- Simplicity over unnecessary complexity.
- Security and observability by design.
- Transparent engineering decisions.
- Functional correctness and architectural integrity.
- Continuous learning and improvement.

---

## Engineering Handbook

The repository contains a structured body of engineering documentation supporting the platform.

### Foundation

- Project Constitution
- Product Vision
- Engineering Guide
- Engineering Playbook
- Engineering Decision Log

### Architecture

- Architecture overview and solution architecture
- Data-platform architecture
- Dependency and boundary definitions
- Design and extensibility guidance
- Resilience and failure-handling guidance
- Implementation architecture and practices
- Architecture Decision Records (ADRs)

### Engineering practices

- Coding standards
- Testing strategy
- Logging and observability guidance
- Dependency-injection guidance
- Contributing guide
- Code of Conduct
- Changelog
- Roadmap
- Engineer growth guidance

Start with the [`docs/`](docs/) directory for the detailed engineering record.

---

## Repository Philosophy

AIQuantTradingResearch is built on the belief that engineering quality is part of functional quality.

The repository values clear architecture, thoughtful technical decisions, transparent trade-offs, high-quality documentation, sustainable design, executable validation, and incremental evolution.

The software should not only solve a problem. It should make the reasoning, constraints, and engineering practices behind the solution inspectable.

---

## Contributing

Contributions are welcome.

Whether improving documentation, reviewing architecture, fixing defects, strengthening tests, or implementing an accepted capability, contributions should preserve the project's architecture and engineering standards.

Contributions are welcome. Before contributing, review [CONTRIBUTING.md](docs/project/CONTRIBUTING.md) and the relevant architecture and release governance.

---

## Disclaimer

AIQuantTradingResearch is an engineering and quantitative research project. It is **not financial advice**, does not guarantee investment performance, and should not be interpreted as a recommendation to buy, sell, or trade any financial instrument.

---

## License

This project is released under the [MIT License](LICENSE).

---

> **AIQuantTradingResearch is a living demonstration of how modern .NET engineering, quantitative research, AI-assisted development, and transparent technical leadership can be combined to build a platform designed to evolve.**

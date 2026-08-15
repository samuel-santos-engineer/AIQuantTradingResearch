# Release 1.1 Execution Plan

## 1. Release Identity

**Release:** 1.1
**Phase:** Phase 3
**Title:** Market Data Persistence Foundation

Authoritative milestone title:

```text
Phase 3 - Release 1.1: Market Data Persistence Foundation
```

Release 1.1 begins only after the successful Release 1.0 closure terminal:

```text
RELEASE 1.0 CLOSED
```

The next authorized lifecycle action from Release 1.0 is governance and GitHub planning for Release 1.1. Release 1.1 implementation remains separately gated by this execution plan, GitHub planning, and WP01 authorization.

---

## 2. Release Objective

Establish the first durable, provider-independent historical market-data persistence vertical slice so normalized market observations acquired through the Release 1.0 market-data boundary can be stored, reconstructed, retrieved, and reused deterministically without coupling Domain or Application to a concrete storage technology.

The intended end-to-end capability is:

```text
Twelve Data
    ↓
HTTP transport
    ↓
Normalization
    ↓
Provider-independent observations
    ↓
Application persistence contract
    ↓
Infrastructure persistence implementation
    ↓
Durable historical data
    ↓
Historical retrieval
    ↓
Application research workflow
    ↓
Domain research values/results
```

Release 1.1 proves persistence as a durable Infrastructure concern while preserving the provider-independent Domain/Application architecture established in Release 1.0.

---

## 3. Architectural Invariants

The production dependency graph remains:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Required architectural invariants:

- Domain remains independent of storage technology, SQL, ORM, database APIs, filesystem mechanics, and persistence configuration.
- Application owns provider-independent persistence/retrieval contracts and approved failure vocabulary.
- Infrastructure owns storage engine mechanics, schema/physical model, serialization/mapping, connection lifecycle, persistence implementation, retrieval implementation, and concrete storage failure handling.
- Worker remains the composition/execution boundary.
- The Release 1.0 provider boundary remains intact.
- `IObservationSource` remains an acquisition abstraction; it must not be repurposed into a persistence abstraction.
- No production project is added unless a later explicit authority proves that the current four-project model cannot satisfy Release 1.1 safely.
- Production cycles remain zero.

---

## 4. Release 1.1 In Scope

Release 1.1 includes:

- persistence technology discovery and evidence-backed selection;
- authoritative persistence semantics for historical observations;
- observation identity and uniqueness rules;
- idempotent duplicate semantics;
- conflicting duplicate semantics;
- provider-independent Application persistence/retrieval contracts;
- Application orchestration required to persist and/or retrieve historical observations;
- Infrastructure-owned physical storage model;
- concrete local/offline storage engine integration;
- schema/bootstrap mechanics;
- durable historical observation writes;
- deterministic historical observation retrieval;
- storage validation and failure mapping;
- dependency registration and configuration;
- Worker composition/execution through the new persistence boundary;
- permanent Domain/Application tests where required;
- permanent Infrastructure persistence tests;
- architecture enforcement for storage boundaries;
- current-state documentation alignment;
- full validation, integration, and technical acceptance;
- Git/GitHub integration after WP16 acceptance;
- human merge gate;
- post-merge Release 1.1 closure;
- explicit authorization of Release 1.2 governance design only after Release 1.1 closure.

---

## 5. Explicitly Out of Scope

Release 1.1 does not implement:

- streaming or real-time feeds;
- WebSocket ingestion;
- event buses;
- distributed pipelines;
- caching architecture;
- data-lake architecture;
- cloud databases;
- Azure deployment;
- multi-provider runtime aggregation;
- provider fallback/failover;
- trading or order execution;
- portfolio management;
- backtesting engine;
- strategy engine;
- AI/ML;
- MLOps;
- plugin framework;
- generalized repository framework;
- generalized ORM abstraction;
- enterprise audit/history framework;
- generalized schema migration platform beyond the minimum persistence slice;
- Release 1.2 implementation.

Planned capabilities may remain documented only when clearly identified as future work.

---

## 6. Release Design Principles

Release 1.1 must follow these principles:

1. **Semantics before technology.** Storage technology must not define Domain/Application meaning.
2. **Evidence before selection.** WP02 selects the persistence technology using explicit zero-cost, local/offline, deterministic criteria.
3. **Durability must be real.** Persistence must survive component/process reconstruction; an in-memory dictionary alone does not satisfy the release objective.
4. **Provider independence remains intact.** Twelve Data is not a persistence contract.
5. **No silent historical mutation.** Duplicate/conflict behavior must be explicit and deterministic.
6. **Retrieval ordering is semantic.** Storage-engine default ordering is never authoritative.
7. **Failure mapping is bounded.** Concrete storage mechanics do not leak into Application/Domain.
8. **Tests are offline and deterministic.** Persistence acceptance must not require live provider access.
9. **Minimal implementation.** Build the smallest durable vertical slice that proves the architecture.
10. **Lifecycle gates are explicit.** WP16 acceptance is not integration, merge is not closure, and closure is required before Release 1.2 governance design begins.

---

## 7. Authoritative Work-Package Sequence

Release 1.1 consists of exactly sixteen implementation work packages:

```text
WP01 — Release & Repository Preflight
WP02 — Persistence Technology Discovery
WP03 — Historical Observation Persistence Semantics
WP04 — Application Persistence Contracts
WP05 — Persistence Use-Case Integration
WP06 — Storage Physical Model
WP07 — Storage Engine & Connection Boundary
WP08 — Observation Persistence
WP09 — Historical Observation Retrieval
WP10 — Storage Validation & Failure Mapping
WP11 — Dependency Registration & Configuration
WP12 — Worker Persistent Market-Data Execution
WP13 — Domain & Application Tests
WP14 — Infrastructure & Persistence Tests
WP15 — Architecture & Documentation Alignment
WP16 — Full Validation, Integration & Acceptance
```

No WP17+ implementation package is authorized by this plan.

---

## 8. Authoritative Dependency Graph

```text
Release 1.0 CLOSED
        │
       WP01
        │
       WP02
        │
       WP03
        │
       WP04
        │
       WP05
        │
        ├─────────────────────┐
        │                     │
       WP06                  WP13
        │
       WP07
        │
       WP08
        │
       WP09
        │
       WP10
        │
       WP11
        │
       WP12
        │
        └──────────┬──────────┘
                   │
                  WP14
                   │
          WP13 ────┤
                   ↓
                  WP15
                   ↓
                  WP16
```

Dependency table:

| Work Package | Depends On |
|---|---|
| WP01 | Release 1.0 CLOSED |
| WP02 | WP01 |
| WP03 | WP02 |
| WP04 | WP03 |
| WP05 | WP04 |
| WP06 | WP05 |
| WP07 | WP06 |
| WP08 | WP07 |
| WP09 | WP08 |
| WP10 | WP09 |
| WP11 | WP10 |
| WP12 | WP11 |
| WP13 | WP03, WP04, WP05 |
| WP14 | WP06, WP07, WP08, WP09, WP10, WP11, WP12 |
| WP15 | WP13, WP14 |
| WP16 | WP15 |

GitHub issue dependencies must match this graph exactly. Missing or artificial dependency edges are planning drift.

---

# 9. WP01 — Release & Repository Preflight

## Objective

Establish the exact Release 1.1 starting state and prove the repository is safe for persistence design.

## Responsibilities

WP01 must verify:

- Release 1.0 terminal is `RELEASE 1.0 CLOSED`;
- PR #102 is merged;
- `main` equals `origin/main`;
- Release 1.0 issue/milestone state is terminal;
- canonical verification passes;
- permanent test baseline is recorded;
- production dependency graph is recorded;
- current Domain/Application/Infrastructure/Worker persistence-related surfaces are inventoried;
- no unauthorized Release 1.1 implementation already exists;
- retired legacy Release 1.1 milestone/planning is inspected but not automatically reactivated;
- no existing persistence implementation conflicts with this plan.

## Required Evidence

At minimum:

```text
main synchronization = PASS
working tree = CLEAN
eng/verify.ps1 = PASS
permanent tests = recorded
Architecture.Tests = recorded
production graph = recorded
unexpected Release 1.1 implementation = 0
```

## Authorized Mutations

Governance/report artifacts explicitly assigned by the file manifest only.

## Prohibited

No persistence package, technology selection, schema, contracts, production code, test code, DI, Worker, commit/push/PR, or WP02 implementation.

## Exit Criteria

WP01 is complete only when the starting repository and GitHub baseline are reconciled with zero implementation drift.

---

# 10. WP02 — Persistence Technology Discovery

## Objective

Select the minimum credible persistence technology through explicit evidence.

## Evaluation Criteria

Candidates must be compared for:

- zero monetary cost;
- local/offline operation;
- .NET support;
- durability;
- transaction capability;
- deterministic testing;
- clean-checkout bootstrap;
- Windows/Linux compatibility;
- GitHub CI feasibility;
- schema evolution feasibility;
- dependency/package impact;
- operational complexity;
- portfolio/recruiting relevance.

## Required Artifacts

At minimum:

```text
MARKET_DATA_PERSISTENCE_ASSESSMENT.md
MARKET_DATA_PERSISTENCE_DECISION.md
```

at the manifest-authorized architecture location.

## Required Decision

The decision must state:

```text
Selected persistence technology
Selection rationale
Rejected alternatives
Known constraints
Release 1.1 usage boundary
```

## Prohibited

No package installation, schema, persistence implementation, DI, Worker changes, or WP03+ implementation.

## Exit Criteria

Exactly one selected technology is evidence-backed and compatible with Release 1.1 constraints.

---

# 11. WP03 — Historical Observation Persistence Semantics

## Objective

Define persistence meaning independent of storage technology.

## Required Semantic Decisions

WP03 must explicitly define:

### Identity

Historical observation identity must be derived from provider-independent semantics, expected conceptually as:

```text
target/instrument + observation instant
```

The exact rule must reconcile with existing Release 1.0 Domain values.

### Timestamp

Persist/reconstruct the semantic `DateTimeOffset` represented by `PriceObservation`.

### Price

Persist the normalized provider-independent price, never provider transport representation.

### Ordering

Canonical historical retrieval order:

```text
observation instant ascending
```

### Duplicate Semantics

Expected target policy:

```text
same identity + equivalent observation
→ idempotent

same identity + conflicting observation
→ deterministic conflict
```

No silent overwrite/correction unless a later explicit authority changes this rule.

### Empty Results

Define valid empty retrieval versus failure behavior at the provider-independent contract level.

## Domain Delta

WP03 must determine whether existing Domain values are sufficient.

A zero Domain delta is valid and preferred when repository truth already expresses all required invariants.

## Prohibited

No SQL, ORM, schema, physical record, storage package, connection, DI, or Worker work.

## Exit Criteria

All persistence semantics needed by later WPs are explicit and technology independent.

---

# 12. WP04 — Application Persistence Contracts

## Objective

Create the minimum provider-independent Application seam for persistence and retrieval.

## Responsibilities

Contracts may express only what Application needs, including where justified:

- target/instrument identity;
- observations;
- retrieval criteria;
- persistence/retrieval result;
- provider-independent persistence failures.

## Prohibited Leakage

Application contracts must not expose:

- SQL;
- table/row concepts;
- concrete database types;
- ORM types;
- filesystem paths;
- storage-engine exceptions;
- provider DTOs.

## Failure Vocabulary

Define only failures Application must understand. Do not mirror every concrete storage exception.

## Prohibited

No concrete Infrastructure implementation, package addition, schema, Worker, or WP05 behavior.

## Exit Criteria

Application owns a minimal technology-independent persistence/retrieval contract surface.

---

# 13. WP05 — Persistence Use-Case Integration

## Objective

Integrate persistence behavior into the Application workflow without storage leakage.

## Responsibilities

Reconcile the exact Application flow for:

```text
acquire normalized observations
→ persist provider-independent observations
→ perform research processing
```

and/or, where explicitly required:

```text
retrieve persisted observations
→ perform research processing
```

Persistence must not silently become a caching policy.

## Requirements

- consume only WP04 contracts;
- preserve Release 1.0 research behavior;
- propagate approved persistence failures;
- keep storage mechanics out of Application;
- remain deterministic.

## Prohibited

No storage engine, SQL/ORM, schema, configuration, Worker, or storage-specific branching.

## Exit Criteria

Application orchestration uses the persistence abstraction correctly with zero storage-technology leakage.

---

# 14. WP06 — Storage Physical Model

## Objective

Define the minimum Infrastructure-owned durable representation.

## Responsibilities

The physical model must represent only what is required to round-trip provider-independent observations, expected to include:

- target/instrument identity;
- observation instant;
- price;
- technically necessary storage metadata only.

## Required Rules

Define:

- primary/unique identity;
- timestamp representation;
- price representation;
- nullability;
- required constraints;
- mapping to/from `PriceObservation`;
- duplicate protection.

## Boundary

```text
PriceObservation
→ Infrastructure mapping
→ persistence record

persistence record
→ Infrastructure mapping
→ PriceObservation
```

Physical records must not leak to Application/Domain.

## Prohibited

No generalized analytics schema, provider schema, enterprise audit framework, or unrelated future model.

## Exit Criteria

A minimal physical model exists and preserves WP03 semantics.

---

# 15. WP07 — Storage Engine & Connection Boundary

## Objective

Introduce the selected concrete storage engine and minimum connection/lifecycle mechanics.

## Responsibilities

Depending on WP02 selection:

- authorized package introduction;
- connection creation;
- local storage initialization;
- schema bootstrap;
- resource disposal;
- deterministic test isolation;
- operational configuration boundary.

## Required Properties

```text
local/offline capable
deterministic
zero production credential requirement unless technology truly requires it
no cloud dependency
no machine-global mutable state
clean-checkout reproducible
```

## Prohibited

No complete persistence/retrieval behavior, Worker integration, generalized migration framework, or cloud hosting.

## Exit Criteria

Infrastructure can safely initialize/connect to the selected storage engine under Release 1.1 constraints.

---

# 16. WP08 — Observation Persistence

## Objective

Implement durable writes for provider-independent historical observations.

## Required Behavior

Implement:

- persistence of valid observations;
- identity enforcement;
- timestamp/price preservation;
- idempotent equivalent duplicates;
- deterministic conflicting-duplicate handling;
- appropriate transaction/atomicity behavior;
- durable state that survives component reconstruction.

## Mandatory Durability Requirement

An in-memory-only implementation does not satisfy WP08.

Later permanent tests must prove:

```text
construct
write
dispose/reconstruct
read
→ same provider-independent observation semantics
```

## Prohibited

No caching, acquisition redesign, provider fallback, Worker behavior, or future pipeline.

## Exit Criteria

Historical observations are durably written under WP03 semantics.

---

# 17. WP09 — Historical Observation Retrieval

## Objective

Implement deterministic retrieval of persisted observations.

## Requirements

- target filtering;
- correct temporal criteria where authorized;
- canonical ascending ordering;
- provider-independent `PriceObservation` reconstruction;
- valid empty behavior;
- deterministic count/range behavior where authorized;
- no storage-record leakage.

## Prohibited

No cache-first behavior, automatic provider fallback/acquisition, streaming, or mixed-source policy.

## Exit Criteria

Historical observations can be reconstructed deterministically through the Application-facing contract.

---

# 18. WP10 — Storage Validation & Failure Mapping

## Objective

Map concrete storage conditions into approved provider-independent failures.

## Responsibilities

Evaluate and map concrete conditions such as:

- storage unavailable/inaccessible;
- corrupt/malformed persisted representation;
- conflicting observation;
- invalid stored value;
- schema/bootstrap failure where applicable.

Do not collapse all exceptions into one failure.

Programming defects must not be silently translated as expected storage outcomes.

## Boundary

Concrete engine exceptions must not cross Infrastructure.

## Exit Criteria

Storage failures are deterministic, intentional, and provider independent at the Application boundary.

---

# 19. WP11 — Dependency Registration & Configuration

## Objective

Wire the persistence implementation into the existing composition model.

## Responsibilities

Register:

- Application persistence contract;
- Infrastructure persistence implementation;
- engine/connection boundary;
- required configuration/options;
- correct lifetimes.

## Rules

- no hidden in-memory fallback;
- no storage mutation during service resolution unless explicit initialization semantics require it;
- preserve Release 1.0 DI composition;
- configuration contains only operationally required values.

## Required Evidence

Concrete Microsoft DI container construction/resolution must be testable offline.

## Exit Criteria

The persistence contract resolves through Infrastructure with correct configuration and lifetime semantics.

---

# 20. WP12 — Worker Persistent Market-Data Execution

## Objective

Prove the persistence slice through the real composition root.

## Target Flow

```text
Worker
→ Application
→ IObservationSource
→ Release 1.0 provider boundary
→ normalized PriceObservation
→ Application persistence contract
→ Infrastructure persistence
→ durable historical data
```

and, where authorized:

```text
durable historical data
→ retrieval
→ Application
→ Domain research
→ Worker result
```

## Requirements

- reuse existing configuration conventions;
- preserve Release 1.0 provider configuration;
- introduce only necessary persistence configuration;
- preserve cancellation and deterministic failure behavior;
- no business logic in Worker.

## Prohibited

No CLI framework, service daemon redesign, scheduler, API, UI, streaming, or Release 1.2 scope.

## Exit Criteria

The Worker composes and executes the persistence-enabled vertical slice without owning persistence business logic.

---

# 21. WP13 — Domain & Application Tests

## Objective

Permanently prove the technology-independent side of Release 1.1.

## Domain Tests

Add tests only if WP03 legitimately changes Domain behavior.

Zero Domain test delta is acceptable when Domain remains unchanged.

## Application Tests

Cover, where implemented:

- persistence invocation;
- retrieval invocation;
- successful propagation;
- empty behavior;
- approved failure propagation;
- duplicate/conflict behavior visible to Application;
- invalid requests;
- Release 1.0 research behavior preservation;
- absence of storage-specific branching.

Use test-owned fakes/stubs for persistence contracts.

## Prohibited

No concrete database requirement in Application tests.

## Exit Criteria

All new provider-independent Application behavior is permanently covered.

---

# 22. WP14 — Infrastructure & Persistence Tests

## Objective

Permanently prove the concrete persistence boundary offline and deterministically.

## Required Coverage

At minimum evaluate and cover:

- schema/bootstrap;
- write;
- read;
- multiple observations;
- multiple targets;
- timestamp round trip;
- price/decimal round trip;
- ascending ordering;
- idempotent duplicate;
- conflicting duplicate;
- empty storage;
- corruption/malformed evidence where deterministic;
- storage failure mapping;
- reconstruction durability;
- DI registration;
- connection/resource lifecycle;
- test isolation.

## Test Isolation Requirements

Each test must use isolated temporary state.

After tests:

```text
repository residue = 0
persistent test storage residue = 0
machine-global dependency = 0
network dependency = 0
live provider dependency = 0
```

## Exit Criteria

The concrete persistence implementation is comprehensively proven offline.

---

# 23. WP15 — Architecture & Documentation Alignment

## Objective

Make Release 1.1 persistence boundaries executable and document current repository truth.

## Architecture Evolution

Add only stable executable rules supported by authority and repository truth.

At minimum evaluate enforcement for:

- Domain cannot depend on storage mechanics;
- Application cannot depend on concrete storage technology;
- Application cannot depend on SQL/ORM/storage-engine namespaces/types;
- persistence contracts remain Application-owned;
- persistence implementation remains Infrastructure-owned;
- existing Release 1.0 provider-boundary rules remain valid;
- production graph remains acyclic.

Do not enforce incidental folder/naming/count conventions unless they are true architectural policy.

## Documentation Alignment

Reconcile current-state documentation affected by persistence, including manifest-authorized documents such as:

- `README.md`
- `SOLUTION_ARCHITECTURE.md`
- `DEPENDENCY_RULES.md`
- `BOUNDARY_DEFINITIONS.md`
- `MODULE_INTERACTIONS.md`
- `PUBLIC_CONTRACTS.md`
- `DEPENDENCY_INJECTION.md`
- `TESTING_STRATEGY.md`
- `PROJECT_STRUCTURE.md`

and storage-specific architecture documents authorized by the manifest.

## Exit Criteria

Executable architecture and documentation both reflect implemented Release 1.1 truth without overstating future capability.

---

# 24. WP16 — Full Validation, Integration & Acceptance

## Objective

Prove the complete Release 1.1 candidate is technically acceptable.

WP16 is validation-first. It must not become a repair package.

## Required Validation

At minimum:

```text
dotnet restore
dotnet build
all permanent test suites
Architecture.Tests
eng/verify.ps1
git diff --check
git diff --cached --check
```

## Persistence Acceptance Scenario

WP16 must prove an equivalent deterministic lifecycle:

```text
normalized observations
→ persist
→ dispose/reconstruct storage component
→ retrieve
→ reconstruct same provider-independent observations
→ use in research workflow
```

## Required Evidence

- durable reconstruction;
- timestamp round-trip;
- price round-trip;
- deterministic ordering;
- duplicate semantics;
- conflict semantics;
- failure mapping;
- Application storage independence;
- Domain storage independence;
- architecture enforcement;
- Worker composition;
- no credential leak;
- no live provider requirement for persistence acceptance;
- no test residue.

## Fresh-Checkout Proof

From committed/controlled repository state where applicable:

```text
fresh checkout
→ restore
→ verify
→ all tests
→ clean repository
```

## Terminal Decisions

Exactly one:

```text
RELEASE 1.1 ACCEPTED
```

or:

```text
RELEASE 1.1 ACCEPTANCE BLOCKED
```

WP16 acceptance does not close Release 1.1.

---

# 25. Git/GitHub Integration Gate

After:

```text
RELEASE 1.1 ACCEPTED
```

a separately authorized integration step may begin.

## Requirements

The integration authority must:

- reconcile the accepted candidate from the manifest and repository truth;
- derive candidate count after governance artifacts are known;
- avoid premature hard-coded candidate counts;
- stage the exact accepted candidate;
- run cached whitespace validation after staging;
- preserve semantic equivalence for any separately authorized whitespace-only correction;
- create the authorized release branch;
- create exactly the authorized integration commit;
- perform post-commit validation;
- push without force;
- create one PR to `main`;
- inspect PR mergeability/checks honestly;
- stop at explicit human merge authorization.

## Candidate Accounting Rule

Do not repeat the Release 1.0 count-recursion pattern.

Use:

```text
accepted WP candidate
+ explicitly governed integration artifacts
→ reconciled candidate N
```

Freeze `N` only after reconciliation.

No later governance artifact may silently change `N`.

---

# 26. Human Merge Gate

The integration step terminates at:

```text
RELEASE 1.1 GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION
```

It must not:

- merge;
- enable auto-merge;
- close WP16;
- close milestone;
- create a tag;
- create a GitHub Release;
- begin Release 1.2.

Human merge authorization is a separate lifecycle decision.

---

# 27. Post-Merge Closure Gate

After explicit human merge, a separate authoritative closure step must prove:

```text
PR merged
main synchronized
accepted candidate represented on main
working tree clean

restore PASS
build PASS
all permanent tests PASS
Architecture.Tests PASS
eng/verify.ps1 PASS
diff checks PASS

fresh checkout PASS
architecture closure PASS
documentation closure PASS
governance closure PASS

WP01–WP16 issues CLOSED / Done
Release 1.1 milestone CLOSED
```

The closure step must not automatically create:

- tag;
- GitHub Release;
- Release 1.2 milestone/issues;
- Release 1.2 implementation.

---

## 28. Release 1.1 Closure Terminal

Only after every closure gate passes:

```text
RELEASE 1.1 CLOSED

NEXT AUTHORIZED LIFECYCLE ACTION:
Release 1.2 governance design may begin.

Release 1.2 implementation remains separately gated.
```

If closure fails:

```text
RELEASE 1.1 CLOSURE BLOCKED
```

Release 1.2 is not authorized.

---

# 29. Validation Baseline

Release 1.1 starts from the Release 1.0 closed baseline:

```text
Domain.Tests         11
Application.Tests    16
Infrastructure.Tests 65
Architecture.Tests   13
Total                105
```

These counts are baseline evidence, not fixed Release 1.1 target counts.

Release 1.1 WPs may legitimately increase test counts.

No prompt may force counts merely to match a predetermined number.

---

# 30. Global Validation Requirements

Every implementation WP must run the minimum validation appropriate to its scope.

At release acceptance/closure, required evidence includes:

```text
restore = PASS
format = PASS
build = PASS
build errors = 0

Domain.Tests = PASS
Application.Tests = PASS
Infrastructure.Tests = PASS
Architecture.Tests = PASS

eng/verify.ps1 = PASS
git diff --check = PASS
git diff --cached --check = PASS
clean-checkout validation = PASS
```

Persistence-specific final evidence includes:

```text
durable write = PASS
read after reconstruction = PASS
deterministic retrieval = PASS
timestamp round-trip = PASS
price round-trip = PASS
ordering = PASS
idempotent duplicate = PASS
conflicting duplicate = PASS
empty-store behavior = PASS
storage failure mapping = PASS
DI resolution = PASS
test residue = 0
live provider dependency for persistence tests = 0
```

---

# 31. Security and Data-Safety Guardrails

Release 1.1 must not introduce:

- real credentials into source/test/docs;
- credential-bearing storage connection strings committed to Git;
- cloud secrets;
- production market data committed as persistence fixtures without explicit authority;
- test databases or storage artifacts committed accidentally;
- silent destructive overwrite of historical observations.

Local storage configuration must use safe placeholders/defaults consistent with the selected technology.

---

# 32. Working-Tree Discipline

Until the dedicated integration step:

- no staging unless a work-package authority explicitly requires it;
- no commits;
- no pushes;
- no PRs;
- no branch proliferation;
- no stash/reset that risks cumulative accepted work.

Each WP must preserve prior accepted Release 1.1 work.

Unexpected mutations are blockers.

---

# 33. GitHub Planning Model

Before WP01 implementation, Release 1.1 GitHub planning must represent:

- exactly one authoritative Release 1.1 milestone;
- exactly sixteen WP issues;
- exact WP dependency graph;
- no WP17+ issue;
- no lifecycle-gate issue unless a later explicit governance design chooses otherwise;
- existing labels/Project conventions reused where possible;
- Release 1.2 planning absent.

The retired legacy milestone #42 must not be reopened automatically.

Any reconciliation of legacy Release 1.1 planning requires explicit GitHub-planning authority.

---

# 34. Required Governance Artifacts Before WP01

Before WP01 implementation, the following must exist:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md

docs/roadmap/release-1.1/prompts/
    release-1.1-github-planning-codex-prompt.md
    release-1.1-github-planning-codex-prompt-chat.md
```

After successful GitHub planning and human acceptance, WP01 may receive a separate authoritative Codex prompt.

---

# 35. Final Governance Decision

This execution plan authorizes Release 1.1 governance/planning and, after GitHub planning is accepted, sequential execution of WP01–WP16 under separately authored prompts.

It does not authorize skipping predecessor gates.

It does not authorize Release 1.2 implementation.

The release lifecycle is:

```text
Release 1.0 CLOSED
→ Release 1.1 Governance
→ Release 1.1 GitHub Planning
→ WP01 ... WP16
→ RELEASE 1.1 ACCEPTED
→ Git/GitHub Integration
→ Human Merge
→ Post-Merge Closure
→ RELEASE 1.1 CLOSED
→ Release 1.2 governance design may begin
```

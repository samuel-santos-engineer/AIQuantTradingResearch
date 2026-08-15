# Release 1.0 Execution Plan

## Metadata

  --------------------------------------------------------------------------------
  Field                               Value
  ----------------------------------- --------------------------------------------
  Project                             AIQuantTradingResearch

  Phase                               Phase 3

  Release                             1.0 --- Market Data Foundation

  Status                              Governance Design

  Predecessor                         Release 0.9 --- Research Platform

  Predecessor Gate                    `RELEASE 0.9 CLOSED`

  Next Release                        1.1

  Next-Release Gate                   `RELEASE 1.1 GOVERNANCE DESIGN AUTHORIZED`
                                      only after successful Release 1.0 closure
  --------------------------------------------------------------------------------

## 1. Release Objective

Establish the platform's first real external-data vertical slice by
integrating exactly one evidence-selected historical market-data
provider through provider-independent Application contracts,
Infrastructure-owned transport and normalization, deterministic offline
verification, executable architectural protection, and one real
end-to-end research execution.

Release 1.0 must prove that real external market data can enter the
research platform without provider-specific concepts contaminating
Domain or Application.

``` text
External Historical Provider
          ↓
Infrastructure Transport
          ↓
Provider DTOs
          ↓
Validation / Normalization
          ↓
Application-Owned Observation Boundary
          ↓
ResearchUseCase
          ↓
Domain Observations
          ↓
Research Outcome
          ↓
Worker Output
```

## 2. Governing Architectural Principle

``` text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
Cycles          -> 0
```

Provider-specific transport concepts belong only in Infrastructure.
Domain and Application remain provider-independent.

## 3. Authorized Release Scope

Release 1.0 may introduce only the capability required for the first
provider-backed historical market-data vertical slice:

``` text
historical market-data provider discovery
one selected real external provider
provider-independent Application contracts
historical market-data requests
provider-specific Infrastructure DTOs
HTTP transport
provider response parsing
normalization into canonical observations
boundary validation and failure mapping
dependency registration
provider configuration
one-shot Worker execution
Domain/Application/Infrastructure tests
Architecture.Tests evolution
documentation alignment
full technical acceptance
Git/GitHub integration
post-merge release closure
```

## 4. Prohibited Release Scope

``` text
database/storage
historical-data persistence
cache
streaming market data
WebSockets
real-time ingestion
order execution
broker integration
portfolio management
risk engine
backtesting engine
pipeline orchestration
scheduled/background ingestion
plugin framework implementation
multiple-provider failover
provider-selection framework
AI/ML models
LLM integration
feature engineering
MLOps
cloud deployment
distributed messaging
production observability expansion
production resilience-platform expansion
Release 1.1 implementation
```

## 5. Provider Strategy

Release 1.0 integrates exactly one real historical market-data provider.
Provider selection is owned by WP02 and must be evidence-based. The
architecture may permit future providers, but Release 1.0 must not
implement multiple providers merely to demonstrate extensibility.

## 6. Work Package Model

Release 1.0 contains 16 implementation/governance work packages followed
by three lifecycle gates:

``` text
WP01–WP16
      ↓
Git/GitHub Integration Gate
      ↓
Human Merge Gate
      ↓
Release 1.0 Closure Gate
```

The closure gate is the only gate that may explicitly authorize Release
1.1 governance design.

## 7. Work Package Specifications

### WP01 --- Release & Repository Preflight

**Objective:** Establish a reproducible Release 1.0 starting point from
the formally closed Release 0.9 repository state.

**Authorized scope:** Read-only inspection of `main`, `origin/main`, Git
status, Release 0.9 closure state, solution/projects, package state,
`.editorconfig`, `.gitattributes`, engineering scripts, architecture
documentation, dependency graph, tests, and canonical Worker behavior.

**Prohibited scope:** No production, test, documentation, package,
project, GitHub, provider, or Release 1.0 implementation mutation.

**Dependencies:** `Release 0.9 CLOSED`.

**Artifacts:** No production artifact required. Execution report is
authoritative validation evidence.

**Validation evidence:**

``` text
main == origin/main
ahead/behind = 0/0
working tree = clean
restore = PASS
format = PASS
build = PASS
eng/verify.ps1 = PASS
git diff --check = PASS
existing tests = PASS
existing Worker baseline = PASS
dependency graph unchanged
```

**Exit criteria:**

``` text
WP01 PREFLIGHT ACCEPTED
WP01 PREFLIGHT BLOCKED
```

### WP02 --- Market Data Provider Discovery

**Objective:** Select exactly one external historical market-data
provider suitable for Release 1.0.

**Authorized scope:** Investigate zero-cost accessibility, historical
availability, instruments, granularity, authentication, quotas/rate
limits, request/response schemas, timestamp and numeric semantics,
errors, licensing/use constraints, documentation quality, API stability,
.NET feasibility, and deterministic testability.

**Prohibited scope:** No permanent provider implementation, DTOs, DI,
Application mutation, multi-provider architecture, or provider
framework.

**Dependencies:** WP01.

**Artifacts:**

``` text
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

**Validation evidence:** Decision matrix covering cost, historical
support, instrument coverage, granularity, authentication, limits,
schema, errors, licensing, testability, and .NET feasibility.

**Exit criteria:** Exactly one provider selected with explicit
limitations and documented rationale.

### WP03 --- Market Data Domain Evolution

**Objective:** Determine whether Release 0.9 Domain concepts can
represent real historical observations and evolve them only where
necessary.

**Authorized scope:** Potential evolution of instrument identity,
observation timestamp/value, historical observation, and observation
sequence. Reuse Release 0.9 concepts whenever semantically correct.

**Prohibited scope:** No provider DTOs, HTTP, JSON, URLs, API keys,
provider errors, persistence annotations, storage concepts, or
provider-specific symbol semantics.

**Dependencies:** WP02.

**Artifacts:** Only required Domain source files. Zero Domain changes is
valid.

**Validation evidence:** For each changed/new Domain type: purpose,
invariants, provider independence, and non-duplication with Release 0.9
concepts.

**Exit criteria:** Domain can represent historical observations without
knowing their provider.

### WP04 --- Market Data Application Contracts

**Objective:** Define the stable provider-independent Application
boundary through which historical observations enter research execution.

**Authorized scope:** Review and minimally evolve `IObservationSource`,
`ResearchRequest`, research outcomes/failures, and Application-owned
market-data contracts. A new abstraction is allowed only if the existing
boundary cannot cleanly represent the capability.

**Prohibited scope:** No `HttpClient`, provider DTOs, provider
namespace, provider URLs, authentication implementation, or JSON
serialization.

**Dependencies:** WP03.

**Artifacts:** Application contract source files only as justified.

**Validation evidence:**

``` text
Application -> Domain
Application -X-> Infrastructure
Application -X-> provider implementation
test-owned implementation can satisfy contract without networking
```

**Exit criteria:** Application can request required historical
observations without knowing the provider.

### WP05 --- Historical Market Data Use-Case Integration

**Objective:** Evolve research orchestration only as necessary to
consume historical observations through the Application-owned boundary.

**Authorized scope:** Application orchestration from request through
observation-source call, Domain observations, existing calculation, and
research outcome.

**Prohibited scope:** No HTTP, parsing, provider DTOs, provider
selection, endpoint logic, persistence, broad resilience, or
logging-platform expansion.

**Dependencies:** WP04.

**Artifacts:** Minimum Application implementation changes.

**Validation evidence:** A test-owned observation source can execute the
complete Application use case with known observations and deterministic
result.

**Exit criteria:** Research execution remains independently executable
without Infrastructure.

### WP06 --- Provider Transport Model

**Objective:** Represent the selected provider's external wire contract
entirely inside Infrastructure.

**Authorized scope:** Provider-specific request representation, response
DTOs, JSON property mapping, symbol representation, timestamp
representation, and metadata needed for parsing.

**Prohibited scope:** No provider DTO may become a Domain model or
Application contract. No universal-provider DTO layer.

**Dependencies:** WP02 + WP04.

**Artifacts:** Infrastructure-owned provider transport DTO/model files.

**Validation evidence:** Representative provider fixtures deserialize
into expected Infrastructure transport models.

**Exit criteria:** Provider wire representation is isolated inside
Infrastructure.

### WP07 --- Provider HTTP Client

**Objective:** Implement the minimum HTTP transport required to retrieve
historical data from the selected provider.

**Authorized scope:** `HttpClient`, URI/request construction, query
parameters, provider authentication mechanism if required, HTTP
invocation, response retrieval, and basic status handling.

**Prohibited scope:** No business calculation, storage, cache,
streaming, WebSocket, multi-provider framework, or broad
resilience-platform expansion.

**Dependencies:** WP06.

**Artifacts:** Infrastructure HTTP transport implementation and only
strictly required configuration contract.

**Validation evidence:** Deterministic fake HTTP transport proves
endpoint, request shape, parameters, response consumption, and basic
failure handling.

**Exit criteria:** Infrastructure can retrieve a raw historical response
through an injectable/testable transport path.

### WP08 --- Market Data Normalization

**Objective:** Convert provider-specific representation into canonical
provider-independent observations.

**Authorized scope:** Provider symbol to canonical identity, external
timestamp/value conversion, ordering, field selection, and provider DTO
to Domain/Application-compatible observation.

**Prohibited scope:** No research calculations, storage transformation,
feature engineering, AI preprocessing, or provider-specific types
escaping Infrastructure.

**Dependencies:** WP03 + WP06 + WP07.

**Artifacts:** Infrastructure mapping/normalization implementation.

**Validation evidence:** Known provider fixtures produce exact expected
normalized count, identity, timestamp, value, and order.

**Exit criteria:** No provider-specific transport representation crosses
the Infrastructure boundary.

### WP09 --- Market Data Validation & Failure Mapping

**Objective:** Make external-data failures explicit and deterministic
before observations reach research logic.

**Authorized scope:** Handle relevant non-success HTTP, empty response,
malformed JSON, missing values, invalid numeric/timestamp data,
provider-declared errors, no observations, and unsupported data where
applicable.

**Prohibited scope:** No broad resilience subsystem or speculative
failure taxonomy.

**Dependencies:** WP07 + WP08.

**Artifacts:** Minimum Infrastructure validation/failure mapping plus
only previously authorized Application failure-contract evolution.

**Validation evidence:** Each defined failure class has deterministic
test evidence. Malformed provider input cannot silently become valid
Domain data.

**Exit criteria:** Known external-boundary failures produce intentional
behavior.

### WP10 --- Dependency Registration & Configuration

**Objective:** Compose the Release 1.0 market-data capability through
existing DI and configuration boundaries.

**Authorized scope:** `AddInfrastructure(...)`, `HttpClient`
registration, provider implementation registration, provider
options/configuration, and Application abstraction to Infrastructure
implementation mapping.

**Prohibited scope:** No service locator, static mutable configuration,
committed secrets, provider framework, plugin system, or
environment-specific architecture.

**Dependencies:** WP05 + WP09.

**Artifacts:** Minimum DI/configuration files in existing projects. No
new project.

**Validation evidence:** Container resolves the complete graph from
`IResearchUseCase` through provider transport.

**Exit criteria:** Complete Release 1.0 production graph is
composition-root driven and resolvable.

### WP11 --- Worker Market Data Execution

**Objective:** Demonstrate the first real provider-backed end-to-end
research execution.

**Authorized scope:** Minimal Worker/configuration evolution to request
a known instrument, invoke the use case, consume provider-backed
observations, surface a result, and terminate.

**Prohibited scope:** No interactive shell, background daemon,
scheduler, hosted ingestion, dashboard, GUI, web API, streaming loop, or
trading operation.

**Dependencies:** WP10.

**Artifacts:** Minimum Worker/configuration changes.

**Validation evidence:** Deterministic offline evidence plus
live-provider demonstration when provider/network availability permits.

**Exit criteria:** A real historical-data request can travel through the
production architecture and produce a research result.

### WP12 --- Domain & Application Tests

**Objective:** Validate Release 1.0 Domain semantics and Application
orchestration independently of provider/network availability.

**Authorized scope:** Tests for WP03--WP05 behavior.

**Prohibited scope:** No live HTTP and no provider DTO assertions.

**Dependencies:** WP03 + WP04 + WP05.

**Artifacts:**

``` text
tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
```

**Validation evidence:** Cover Domain invariants, historical-observation
behavior, request semantics, observation-source invocation,
orchestration, outcomes, and failure propagation where applicable.

**Exit criteria:** Core Domain/Application behavior is reproducible
offline without Infrastructure.

### WP13 --- Infrastructure & Provider Tests

**Objective:** Prove the provider boundary comprehensively while keeping
the canonical automated suite independent of the live internet.

**Authorized scope:** Fake HTTP handlers, provider fixtures,
deserialization, normalization, error, and justified DI tests.

**Prohibited scope:** Canonical tests must not require internet,
provider uptime, API quota, personal API key, or external rate limits.

**Dependencies:** WP06--WP10.

**Artifacts:**

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/**
```

plus justified deterministic fixture files.

**Required scenarios:** Successful response, empty response, malformed
response, HTTP error, provider-declared error, invalid numeric value,
invalid timestamp, normalization, ordering, and mapping when applicable.

**Exit criteria:** Complete Infrastructure/provider boundary is
reproducibly testable offline.

### WP14 --- Architecture Evolution

**Objective:** Convert the Release 1.0 provider boundary into executable
architectural constraints.

**Authorized scope:** Architecture.Tests only for stable structural
rules.

**Prohibited scope:** No stylistic, filename, folder-layout, or brittle
source-text rules without explicit justification.

**Dependencies:** WP11 + WP12 + WP13.

**Artifacts:**

``` text
tests/AIQuantTradingResearch.Architecture.Tests/**
```

**Required invariants:** Retain Release 0.9 rules and evaluate/add
stable enforcement for provider implementation/DTO visibility and
provider-specific dependency isolation from Domain/Application.

**Validation evidence:** Every new architecture rule identifies the
forbidden condition it detects; temporary violating probes may be used
where practical and must be removed.

**Exit criteria:** Release 1.0 provider boundary is protected by
executable architecture rules.

### WP15 --- Documentation Alignment

**Objective:** Align repository documentation with actual Release 1.0
implementation.

**Authorized scope:** Update only documentation made stale by Release
1.0.

**Prohibited scope:** Do not present storage, streaming,
multiple-provider failover, trading, backtesting, AI/ML, or production
deployment as implemented.

**Dependencies:** WP14.

**Artifacts:** Existing architecture docs requiring proven updates plus
WP02 market-data authority docs.

**Validation evidence:** Documentation accurately describes selected
provider, dependency graph, Application abstraction, Infrastructure
transport, normalization, failures, DI/configuration, Worker behavior,
testing boundaries, architecture enforcement, and limitations.

**Exit criteria:** No known material contradiction remains between
documentation and executable repository truth.

### WP16 --- Full Validation, Integration & Acceptance

**Objective:** Determine whether the exact cumulative Release 1.0
candidate is technically acceptable for Git/GitHub integration.

**Authorized scope:** Full candidate reconciliation, technical
validation, architecture review, provider-boundary review, deterministic
tests, documentation review, Worker acceptance, and optional
live-provider acceptance.

**Prohibited scope:** No new features, scope expansion, commit, push,
PR, merge, milestone closure, or Release 1.1 work.

**Dependencies:** WP15 plus all preceding WPs accepted.

**Artifacts:** Authoritative Release 1.0 Acceptance Report.

**Validation evidence:**

``` text
candidate reconciliation
manifest reconciliation
unexpected files = 0
restore PASS
format PASS
build PASS / 0 errors
all tests PASS
architecture tests PASS
eng/verify.ps1 PASS
git diff --check PASS
deterministic provider tests PASS
Worker deterministic acceptance PASS
dependency graph exact
Domain/Application provider independence proven
documentation aligned
repository reproducibility proven
```

Live-provider acceptance failures must be classified as `CODE`,
`NETWORK`, `PROVIDER`, `AUTHENTICATION`, or `RATE LIMIT`.

**Exit criteria:**

``` text
RELEASE 1.0 ACCEPTED
RELEASE 1.0 ACCEPTED WITH OBSERVATIONS
RELEASE 1.0 BLOCKED
```

Only an accepted state permits Git/GitHub integration.

## 8. Dependency Sequence

``` text
WP01
 │
 ▼
WP02
 │
 ▼
WP03
 │
 ▼
WP04
 ├──────────────► WP06
 │                 │
 ▼                 ▼
WP05              WP07
                    │
                    ▼
                   WP08
                    │
                    ▼
                   WP09
        └──────┬────┘
               ▼
              WP10
               │
               ▼
              WP11

WP03 + WP04 + WP05 ─────► WP12
WP06 + WP07 + WP08 + WP09 + WP10 ─────► WP13

WP11 + WP12 + WP13
          │
          ▼
         WP14
          │
          ▼
         WP15
          │
          ▼
         WP16
```

## 9. Git/GitHub Integration Gate

Transport the exact WP16-accepted candidate into Git/GitHub without
technical drift.

**Dependencies:** `RELEASE 1.0 ACCEPTED` or explicitly approved
accepted-with-observations.

**Authorized scope:** Create dedicated branch, exact staging, coherent
commit, post-commit verification, push without force, create/reuse PR,
inspect PR/check/review state.

**Prohibited scope:** No feature corrections during transport. No merge.
No issue/milestone closure unless separately authorized.

**Artifacts:**

``` text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt-chat.md
```

**Exit criteria:** State equivalent to
`RELEASE 1.0 GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION`, or
blocked/action state.

## 10. Human Merge Gate

Codex must stop before merge. Exit requires Release 1.0 PR merged under
explicit human authorization.

## 11. Release 1.0 Closure Gate

**Objective:** Prove the exact accepted Release 1.0 candidate landed on
`main`, remains reproducible after merge, leaves repository/GitHub
governance coherent, and formally terminate Release 1.0.

This is the only Release 1.0 gate that may authorize Release 1.1
governance design.

**Authorized scope:** Validate merged PR/commits, `main`/`origin/main`,
manifest, full verification, provider architecture, tests, Worker
acceptance, documentation, Release 1.0 milestone/issues, branches, and
governance artifacts. Only explicitly authorized issue/milestone
reconciliation may mutate governance.

**Prohibited scope:** No new product behavior, provider enhancement, new
provider, storage, streaming, AI/ML, architecture redesign, or Release
1.1 implementation.

**Dependencies:**

``` text
WP16 accepted
      ↓
Git/GitHub Integration accepted
      ↓
Human Merge completed
      ↓
Closure
```

**Artifacts:**

``` text
docs/roadmap/release-1.0/prompts/release-1.0-closure-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-closure-codex-prompt-chat.md
```

Conditional unblock artifacts are created only if an actual blocker
requires them.

**Required closure evidence:**

``` text
Release 1.0 PR = MERGED
accepted candidate contained in main
branch = main
HEAD = origin/main
ahead/behind = 0/0
working tree = clean
restore PASS
format PASS
build PASS
all tests PASS
eng/verify.ps1 PASS
git diff --check PASS
Domain provider-independent
Application provider-independent
Infrastructure owns provider mechanics
production graph acyclic
offline provider fixtures PASS
Worker acceptance PASS
documentation aligned
Release 1.0 governance reconciled
no Release 1.1 implementation artifacts
```

**Exit criteria:**

``` text
RELEASE 1.0 CLOSED
RELEASE 1.0 CLOSED WITH ACTIONS
RELEASE 1.0 CLOSURE BLOCKED
```

Only `RELEASE 1.0 CLOSED` automatically carries:

``` text
RELEASE 1.1 GOVERNANCE DESIGN AUTHORIZED
```

## 12. Release 1.1 Authorization Contract

When and only when closure returns:

``` text
RELEASE 1.0 CLOSED
RELEASE 1.1 GOVERNANCE DESIGN AUTHORIZED
```

the following become authorized:

``` text
Release 1.1 discovery
objective definition
scope definition
authorized/prohibited boundary design
work-package design
dependency design
artifact planning
validation design
exit-criteria design
execution-plan preparation
file-manifest preparation
GitHub planning preparation
```

This does not authorize Release 1.1 production/test/infrastructure
implementation, branch creation, implementation PR, or Codex
implementation execution.

## 13. Release-Level Exit Criteria

Release 1.0 may be formally closed only when:

``` text
one provider selected through evidence
one provider integrated
provider specifics confined to Infrastructure
Domain provider-independent
Application provider-independent
historical observations reach research execution
normalization deterministic
external failures explicit
canonical automated tests require no internet
real-provider acceptance evidence exists when available
DI/composition explicit
production graph acyclic
architecture rules executable
no storage introduced
no streaming introduced
no AI/ML introduced
no order/trading execution introduced
documentation matches implementation
canonical verification passes
repository reproducibility passes
accepted candidate merged to main
main synchronized and clean
Release 1.0 governance reconciled
Release 1.0 milestone closed
```

## 14. Authority Transition Model

``` text
Governance Design Authority
        ↓
Implementation Authority
        ↓
Technical Acceptance Authority
        ↓
Git/GitHub Integration Authority
        ↓
Human Merge Authority
        ↓
Release Closure Authority
        ↓
Release 1.1 Governance Design Authority
```

No successful gate implicitly grants unrelated next-stage authority
unless this plan explicitly says so.

## 15. Final Lifecycle

``` text
RELEASE 0.9 CLOSED
        ↓
WP01 → WP16
        ↓
RELEASE 1.0 ACCEPTED
        ↓
Git/GitHub Integration
        ↓
Human Merge
        ↓
Release 1.0 Closure
        ↓
RELEASE 1.0 CLOSED
        +
RELEASE 1.1 GOVERNANCE DESIGN AUTHORIZED
```

Release 1.0 is complete only at the closure gate, not at WP16 and not at
merge.

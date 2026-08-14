# Release 0.9 Execution Plan

## Phase 2 --- Release 0.9: Research Platform

## 1. Purpose

This is the authoritative execution plan for **Release 0.9 --- Research
Platform**. It transforms the validated Release 0.8 Solution Skeleton
into the first executable vertical research capability while preserving
architecture, determinism, reproducibility, and governed AI-assisted
engineering.

## 2. Release Mission

> Establish the first executable vertical research capability through a
> minimum research domain model, Application-owned contracts and
> orchestration, a deterministic Infrastructure adapter, Worker
> execution, behavioral tests, and executable architecture
> governance---all offline and independent of real market-data
> providers, persistence, plugins, AI/ML, and cloud infrastructure.

## 3. Starting Baseline

WP01 must verify rather than assume: Release 0.8 is `COMPLETE / CLOSED`;
`main = origin/main`; the working tree is classified; the solution
contains exactly 8 projects (4 production + 4 tests); production graph
is Domain→none, Application→Domain, Infrastructure→Application,
Worker→Application+Infrastructure; cycles=0; Release 0.8
Architecture.Tests baseline is 7/7; canonical verification passes.

## 4. Cross-Cutting Rules

1.  **Authority first:** read this plan, the file manifest, active WP
    prompt, relevant playbooks/docs, then inspect repository reality.
2.  **Research before invention:** unresolved domain/architecture
    questions cause investigation, not speculative code.
3.  **Smallest sufficient delta:** change only what the active WP
    requires.
4.  **Determinism:** the complete 0.9 reference workflow works without
    network, credentials, paid services, databases, or cloud resources.
5.  **Dependency ownership:** Domain owns pure behavior; Application
    owns use cases and required abstractions; Infrastructure implements
    external concerns; Worker composes.
6.  **Evidence before completion:** objective validation and Git-state
    inspection are mandatory.
7.  **Future-boundary protection:** later-release functionality remains
    planned, not implemented.

## 5. Explicitly Out of Scope

Real market-data providers (including Yahoo Finance, Alpha Vantage,
Binance, NASDAQ, CME, B3), HTTP acquisition, brokers/exchanges,
ingestion pipelines, persistence/databases, caching, message brokers,
plugins, strategy/backtesting engines, portfolio/order/risk engines,
AI/ML/LLMs/agents, MLOps, cloud/distributed deployment, REST API, UI,
authentication, production observability expansion, and speculative
future abstractions are not authorized. CI is excluded unless current
repository authority explicitly assigns it to Release 0.9.

## 6. Work Package Summary

  ---------------------------------------------------------------------------
                     \# Type             Work Package     Outcome
  --------------------- ---------------- ---------------- -------------------
                     01 Research         Repository &     Repository is
                                         Release          demonstrably ready
                                         Preflight        and every material
                                                          starting concern is
                                                          classified.

                     02 Research         Research Domain  The minimum domain
                                         Discovery        needed for one
                                                          deterministic
                                                          research operation
                                                          is unambiguous and
                                                          approved.

                     03 Feature          Research Domain  Smallest approved
                                         Model            research Domain
                                                          exists as clean
                                                          dependency-free
                                                          production code.

                     04 Feature          Research         Research use-case
                                         Application      boundary exists
                                         Contracts        without knowledge
                                                          of Infrastructure.

                     05 Feature          Research         Research operation
                                         Execution Use    executes against an
                                         Case             injected
                                                          deterministic
                                                          substitute without
                                                          Worker or
                                                          Infrastructure.

                     06 Feature          Research         Infrastructure
                                         Infrastructure   satisfies the
                                         Adapter          reference research
                                                          use case
                                                          deterministically
                                                          and offline.

                     07 Feature          Dependency       Complete Release
                                         Registration     0.9 dependency
                                                          graph is resolvable
                                                          from the
                                                          composition root.

                     08 Feature          Worker Research  Running the
                                         Execution        application
                                                          executes the
                                                          deterministic
                                                          vertical slice
                                                          through intended
                                                          architecture.

                     09 Tests            Domain Tests     Every material
                                                          Release 0.9 Domain
                                                          invariant has
                                                          meaningful
                                                          automated
                                                          protection.

                     10 Tests            Application      Application
                                         Tests            behavior is proven
                                                          independently of
                                                          Infrastructure and
                                                          Worker.

                     11 Tests            Infrastructure   Deterministic
                                         Tests            adapter is
                                                          behaviorally proven
                                                          and replaceable.

                     12 Tests            Architecture     Actual Release 0.9
                                         Evolution        architecture is
                                                          protected by
                                                          executable rules.

                     13 Documentation    Documentation    A new engineer can
                                         Alignment        understand actual
                                                          0.9 state without
                                                          being misled.

                     14 Research         Full Validation, Return ACCEPTED,
                                         Integration &    ACCEPTED WITH
                                         Acceptance       ACTIONS
                                                          (administrative
                                                          only), or REJECTED
                                                          (mandatory
                                                          technical failure).
  ---------------------------------------------------------------------------

## 7. Work Package Contracts

### WP01 --- Repository & Release Preflight

**Type:** Research

**Objective:** Establish an evidence-based Release 0.9 baseline and
prove Release 0.8 is closed, healthy, and ready for evolution.

**Authorized scope:** Inspect Git/GitHub state, Release 0.8 closure,
roadmap authority, solution/project inventory, references, SDK/build
configuration, source, tests, engineering scripts, documentation,
packages, prerequisites, risks and pre-existing changes.

**Prohibited scope:** No mutation: no source/project/package/docs
changes, issue creation, implementation, commits, pushes, or unrelated
cleanup.

**Dependencies:** Release 0.8 COMPLETE / CLOSED.

**Artifacts:** No implementation/documentation artifact by default;
authoritative WP prompt and Codex report provide governance evidence.

**Validation evidence:** Verify milestone closure, main=origin/main,
classified working tree, 8 projects (4 production/4 tests), accepted
graph, 0 cycles, Architecture.Tests 7/7 baseline, canonical verify PASS,
SDK/toolchain state.

**Exit criteria:** Repository is demonstrably ready and every material
starting concern is classified.

### WP02 --- Research Domain Discovery

**Type:** Research

**Objective:** Discover the minimum domain vocabulary, invariants,
ownership, and behavioral boundary for one meaningful deterministic
research operation before implementation.

**Authorized scope:** Determine minimum research
input/result/observations, Domain vs Application ownership, invariants,
deterministic reference scenario, rejected/future concepts and smallest
useful vertical slice.

**Prohibited scope:** No C#, interfaces, DTO invention, provider
implementation, HTTP, persistence, trading strategy, analytics
framework, AI/ML, speculative aggregates, or forced DDD patterns.

**Dependencies:** WP01.

**Artifacts:** `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md`.

**Validation evidence:** Classify concepts as Domain, Application,
Infrastructure, Worker, future, or rejected; define terminology,
invariants, boundaries, reference scenario, non-goals and implementation
constraints.

**Exit criteria:** The minimum domain needed for one deterministic
research operation is unambiguous and approved.

### WP03 --- Research Domain Model

**Type:** Feature

**Objective:** Implement exactly the approved minimum Domain model from
WP02.

**Authorized scope:** Approved Domain-owned value
objects/entities/immutable records/primitives/invariants and
domain-specific results/errors when justified.

**Prohibited scope:** No Application/Infrastructure/Worker dependencies;
no DI, config, HTTP, serialization, database, logging,
filesystem/network infrastructure, speculative
repositories/services/frameworks.

**Dependencies:** WP02.

**Artifacts:** `src/AIQuantTradingResearch.Domain/**` only as required.

**Validation evidence:** Domain builds independently; project references
remain zero; approved invariants encoded; no boundary leakage; solution
and architecture validation pass.

**Exit criteria:** Smallest approved research Domain exists as clean
dependency-free production code.

### WP04 --- Research Application Contracts

**Type:** Feature

**Objective:** Define the Application boundary through which research is
requested, executed, and returned.

**Authorized scope:** Minimum use-case input/output, execution boundary,
Application-owned external-data abstraction, and required
Application-facing contracts.

**Prohibited scope:** No concrete Infrastructure, HTTP, persistence,
Worker behavior, plugin/provider-specific contracts, generic
repository/service framework, or vendor terminology leakage.

**Dependencies:** WP03.

**Artifacts:** `src/AIQuantTradingResearch.Application/**` only as
required.

**Validation evidence:** Application dependency direction valid; no
Infrastructure reference; contracts match WP02; external needs
dependency-inverted; build/architecture pass.

**Exit criteria:** Research use-case boundary exists without knowledge
of Infrastructure.

### WP05 --- Research Execution Use Case

**Type:** Feature

**Objective:** Implement Application-owned deterministic research
orchestration.

**Authorized scope:** Request validation, obtain observations through
Application abstraction, coordinate approved Domain behavior, construct
result; executable with deterministic substitute.

**Prohibited scope:** No Infrastructure implementation,
network/files/database, console/Worker behavior, resilience
infrastructure, provider-specific behavior, or hidden Domain logic.

**Dependencies:** WP04.

**Artifacts:** `src/AIQuantTradingResearch.Application/**` only as
required.

**Validation evidence:** Application builds; approved request executes;
dependency injected through abstraction; deterministic result possible;
invalid input follows approved model; no Infrastructure dependency.

**Exit criteria:** Research operation executes against an injected
deterministic substitute without Worker or Infrastructure.

### WP06 --- Research Infrastructure Adapter

**Type:** Feature

**Objective:** Provide the minimum deterministic Infrastructure
implementation needed for end-to-end execution without external systems.

**Authorized scope:** One offline, repeatable adapter for the
Application-owned external abstraction; immutable in-memory
deterministic fixture data when appropriate.

**Prohibited scope:** No real providers, HTTP, database, filesystem
persistence, cache, broker, provider-selection framework, plugins,
random data, or current-time dependence.

**Dependencies:** WP05.

**Artifacts:** `src/AIQuantTradingResearch.Infrastructure/**` only as
required.

**Validation evidence:** Implements Application abstraction;
Infrastructure→Application preserved; same input→same observations; no
network/credentials; build/architecture pass.

**Exit criteria:** Infrastructure satisfies the reference research use
case deterministically and offline.

### WP07 --- Dependency Registration

**Type:** Feature

**Objective:** Compose Release 0.9 services through the existing DI
boundaries.

**Authorized scope:** Register only required Application services and
deterministic Infrastructure implementation; justify lifetimes.

**Prohibited scope:** No service locator/global container, reflection
auto-registration framework, plugin scanning, unrelated future services,
or configuration-framework expansion.

**Dependencies:** WP05 + WP06.

**Artifacts:** Minimal existing Application/Infrastructure
registration-file changes.

**Validation evidence:** Host resolves use case and dependencies;
lifetimes justified; Worker does not manually construct graph;
dependency directions unchanged; verify PASS.

**Exit criteria:** Complete Release 0.9 dependency graph is resolvable
from the composition root.

### WP08 --- Worker Research Execution

**Type:** Feature

**Objective:** Turn the minimal Worker into the thinnest executable
entry point for the reference research workflow.

**Authorized scope:** Use existing host/composition, invoke
registrations, construct approved deterministic reference request,
invoke Application use case, surface result, follow approved lifecycle.

**Prohibited scope:** No Domain calculations/research
algorithms/data-generation implementation/provider logic/business
validation leakage/large orchestration/CLI framework/REST/UI/scheduler.

**Dependencies:** WP07.

**Artifacts:** Minimal changes under
`src/AIQuantTradingResearch.Worker/**`.

**Validation evidence:** Prove Worker→Application use case→Application
abstraction→Infrastructure adapter→Domain→result; Worker remains
composition-focused.

**Exit criteria:** Running the application executes the deterministic
vertical slice through intended architecture.

### WP09 --- Domain Tests

**Type:** Tests

**Objective:** Add meaningful behavioral protection for Release 0.9
Domain behavior.

**Authorized scope:** Test actual invariants, valid/invalid
construction, value semantics, transformations/calculations and
boundaries where applicable.

**Prohibited scope:** No Application/Infrastructure/DI tests, arbitrary
coverage target, meaningless constructor tests, or test-per-line
behavior.

**Dependencies:** WP03; executed after vertical slice.

**Artifacts:** `tests/AIQuantTradingResearch.Domain.Tests/**`.

**Validation evidence:** Discovered tests \>0; all pass; tests express
real Domain behavior; deterministic; no external dependencies.

**Exit criteria:** Every material Release 0.9 Domain invariant has
meaningful automated protection.

### WP10 --- Application Tests

**Type:** Tests

**Objective:** Prove research orchestration independently of concrete
Infrastructure.

**Authorized scope:** Deterministic test doubles for Application
abstractions; valid execution, invalid request, dependency response
handling and result construction as defined.

**Prohibited scope:** No real Infrastructure adapter,
network/files/database, implementation-detail mocking, or unnecessary
DI-container testing.

**Dependencies:** WP05 + Domain behavioral baseline.

**Artifacts:** `tests/AIQuantTradingResearch.Application.Tests/**`.

**Validation evidence:** Discovered tests \>0; all pass; core use-case
tests do not require concrete Infrastructure; deterministic
orchestration proven.

**Exit criteria:** Application behavior is proven independently of
Infrastructure and Worker.

### WP11 --- Infrastructure Tests

**Type:** Tests

**Objective:** Prove the deterministic adapter honors its
Application-owned contract.

**Authorized scope:** Test known dataset, repeatability, supported
inputs, defined invalid/unsupported behavior and mapping to approved
concepts.

**Prohibited scope:** No network, real provider, database, credentials,
performance benchmark, or future-provider contract suite.

**Dependencies:** WP06.

**Artifacts:** `tests/AIQuantTradingResearch.Infrastructure.Tests/**`.

**Validation evidence:** Discovered tests \>0; all pass; same input→same
output; zero external dependencies; contract satisfied.

**Exit criteria:** Deterministic adapter is behaviorally proven and
replaceable.

### WP12 --- Architecture Evolution

**Type:** Tests

**Objective:** Extend executable architecture governance to protect
actual Release 0.9 boundaries.

**Authorized scope:** Preserve all Release 0.8 rules and add only
objectively enforceable, justified rules for
Domain/Application/Infrastructure/Worker ownership and acyclicity.

**Prohibited scope:** No brittle naming-only tests, speculative rules,
source-text scanning when semantic inspection exists, removal of valid
0.8 rules, or arbitrary test-count target.

**Dependencies:** WP03--WP11.

**Artifacts:** `tests/AIQuantTradingResearch.Architecture.Tests/**`.

**Validation evidence:** Original seven rules pass; each new rule has
rationale and objective enforcement; full architecture suite passes;
cycles=0.

**Exit criteria:** Actual Release 0.9 architecture is protected by
executable rules.

### WP13 --- Documentation Alignment

**Type:** Documentation

**Objective:** Align current-state documentation with the implemented
Research Platform without rewriting history.

**Authorized scope:** Classify relevant docs
CURRENT-STATE/HISTORICAL/PLANNED/UNRELATED; update only changed
current-state authority, including research model, architecture,
interactions/contracts, DI/testing guidance and navigation where needed.

**Prohibited scope:** No mass rewrite, Release 0.8 history changes,
historical prompt/report changes, future capabilities described as
implemented, or speculative expansion.

**Dependencies:** WP02--WP12.

**Artifacts:** `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md`
plus only relevant existing current-state docs.

**Validation evidence:** Docs agree with code on concepts, boundaries,
abstraction, deterministic adapter, DI, Worker, tests, architecture
rules and current/future distinction; obsolete claims classified.

**Exit criteria:** A new engineer can understand actual 0.9 state
without being misled.

### WP14 --- Full Validation, Integration & Acceptance

**Type:** Research

**Objective:** Prove Release 0.9 from clean state, validate manifest
compliance, integrate through governed GitHub workflow, and make the
release acceptance decision.

**Authorized scope:**
Clean/restore/format/build/test/architecture/verify/smoke validation;
manifest audit; controlled branch/staging/commit/push/PR/review/merge;
post-merge sync; release acceptance.

**Prohibited scope:** No silent repair, redesign, scope expansion,
future provider/persistence/plugins/AI, history rewrite, force push,
fabricated checks/reviews, or review bypass.

**Dependencies:** WP01--WP13 complete.

**Artifacts:** Governance prompt/chat artifacts and GitHub
issue/PR/milestone evidence; separate closure prompt only if needed and
never as WP15.

**Validation evidence:** Prove solution 8/4/4, graph and 0 cycles,
behavioral suites pass, deterministic smoke succeeds, engineering
validation passes, docs aligned, future scope absent, governance
traceable.

**Exit criteria:** Return ACCEPTED, ACCEPTED WITH ACTIONS
(administrative only), or REJECTED (mandatory technical failure).

## 8. Dependency Model

``` text
WP01 -> WP02 -> WP03 -> WP04 -> WP05 -> WP06 -> WP07 -> WP08
                         |                        |
                         +-> WP09                 +-> WP10
                         |                        +-> WP11
                         +----------------------------+
                                                      |
                                                      v
                                                    WP12 -> WP13 -> WP14
```

Serial Codex execution remains WP01 through WP14; this graph records
conceptual ownership/dependencies.

## 9. Testing Transition

Release 0.8 intentionally ended with empty
Domain/Application/Infrastructure test skeletons and 7
Architecture.Tests. Release 0.9 requires meaningful discovered
behavioral tests in all three behavioral test projects while preserving
and extending justified architecture protection. No arbitrary coverage
percentage is required.

## 10. Git/GitHub Governance

Unless an active prompt explicitly authorizes integration,
implementation WPs do not commit, push, merge, or mutate GitHub planning
state. WP14 owns final governed integration unless repository authority
establishes otherwise. Never force-push, rewrite accepted history,
fabricate checks/reviews, expose credentials, stage unrelated work, or
destructively clean unclassified files.

## 11. Release Acceptance Contract

Release 0.9 acceptance requires: 8-project architecture preserved;
accepted graph and zero cycles; WP02 discovery approved; minimum Domain
implemented and tested; Application contracts/use case implemented and
independently tested; deterministic offline Infrastructure adapter
implemented/tested; Worker executes reference flow; Release 0.8
architecture rules still pass plus justified 0.9 rules; clean
reconstruction/restore/format/build/test/verify pass with zero build
errors; current-state docs align; no unauthorized future scope; manifest
compliance and governed integration are proven.

WP14 returns exactly one release decision: `ACCEPTED`,
`ACCEPTED WITH ACTIONS`, or `REJECTED`. `ACCEPTED WITH ACTIONS` is
administrative/governance-only; mandatory technical failure means
`REJECTED`. A separate closure activity may complete remaining
administration without inventing WP15.

## 12. Conclusion

Release 0.9 is the smallest meaningful step from architecture skeleton
to executable research platform. It succeeds when one deterministic
research operation executes end-to-end through the intended boundaries,
meaningful tests protect behavior, executable rules protect
architecture, documentation reflects reality, and the release is
integrated through controlled governance without future-scope leakage.

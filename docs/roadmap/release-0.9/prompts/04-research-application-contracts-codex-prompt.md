# Codex Execution Prompt — Release 0.9 / WP04 Research Application Contracts

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 04 — Research Application Contracts |
| Type | Implementation |
| GitHub Issue | #72 |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP03 — Research Domain Model = `IMPLEMENTATION COMPLETE` |
| Primary Authorities | `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md`, Release 0.9 execution plan and file manifest |
| Execution Mode | Narrowly scoped Application contract implementation |
| Expected Outcome | Define only the minimum Application-owned contracts required to request research, return research outcomes, expose the research-use-case boundary, and express the external observation dependency without implementing orchestration or Infrastructure |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP04 — Research Application Contracts
```

WP04 establishes the **Application boundary** for the first Release 0.9 research vertical slice.

It must define only the contracts necessary for later work packages to implement:

```text
request
result
research-use-case boundary
external observation-source abstraction
expected failure outcomes
```

WP04 must not implement the research use case itself.

WP04 must not implement Infrastructure.

WP04 must not modify Worker.

Do not begin WP05.

---

# 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/04-research-application-contracts-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect the actual WP03 Domain implementation:

```text
src/AIQuantTradingResearch.Domain/PriceObservation.cs
src/AIQuantTradingResearch.Domain/ObservationSeries.cs
src/AIQuantTradingResearch.Domain/MeanPrice.cs
```

Also inspect current Application architecture and guidance:

```text
src/AIQuantTradingResearch.Application/**
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/ERROR_HANDLING.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md
```

The WP02 research-domain authority owns semantics.
The WP03 implementation proves the approved Domain concepts are executable.
WP04 must consume those facts without redefining them.

---

# 3. Accepted WP02/WP03 Boundaries

Approved Domain concepts already implemented:

```text
PriceObservation
ObservationSeries
MeanPrice
ObservationSeries.CalculateMeanPrice()
```

Application-owned concepts approved by WP02:

```text
research target key
requested count
research request/result
research-use-case boundary
external observation-source abstraction
unsupported-target outcome
insufficient-observations outcome
```

Infrastructure-owned responsibility:

```text
deterministic offline fixture adapter
```

Worker-owned responsibility:

```text
reference invocation / composition
```

Do not move Infrastructure or Worker responsibility into Application contracts.

---

# 4. Contract Design Principles

## 4.1 Application Owns Its External Needs

The external observation dependency must be declared by Application.

Conceptually:

```text
Application
   |
   +--> external observation contract
            ^
            |
       Infrastructure implements later
```

Do not make Application depend on Infrastructure.

Do not create provider/vendor-specific terminology.

## 4.2 Contracts Before Orchestration

WP04 defines the shape of the use case.

WP05 implements orchestration.

Do not put use-case implementation inside contract types.

## 4.3 Explicit Expected Outcomes

WP02 approved expected failures such as:

```text
blank target
non-positive requested count
unsupported target
insufficient observations
```

The contracts must make room for these outcomes according to repository error-handling guidance.

Do not invent a generalized application-wide result framework.

## 4.4 Minimal Public Surface

Only create contracts required by this vertical slice.

Avoid:

```text
generic IRequest<T>
generic handler framework
mediator abstractions
CQRS framework
repository abstractions
service base types
provider hierarchy
generic paged/result wrappers
```

unless existing repository authority explicitly requires them.

## 4.5 No Vendor Leakage

Do not use names such as:

```text
Binance
Yahoo
AlphaVantage
CME
NASDAQ
B3
```

in Application contracts.

Use provider-neutral research/observation terminology.

---

# 5. Required Contract Responsibilities

WP04 must define the minimum contracts for the following responsibilities.

## 5.1 Research Target

The use case must receive an opaque research target identifier.

WP02 approved:

```text
non-blank target
```

The target is Application-owned for Release 0.9.

Do not model:

```text
instrument hierarchy
exchange
asset class
symbol metadata
currency model
```

unless authority is amended.

---

## 5.2 Requested Observation Count

The use case must receive a requested observation count.

Approved invariant:

```text
count > 0
```

This is Application request validation, not Domain series validation.

Do not introduce time range/window abstractions beyond the count required by the reference scenario.

---

## 5.3 Research Request Contract

Define the smallest request required to carry:

```text
target
requested count
```

The request must not contain:

```text
provider
connection details
persistence identifiers
strategy
indicator
AI/ML configuration
execution infrastructure
```

---

## 5.4 Observation Source Abstraction

Define an Application-owned abstraction capable of providing observations required by the use case.

Its semantics must support:

```text
target
requested count
deterministic observation retrieval later
unsupported target
insufficient observations
```

The abstraction must return or communicate observations in a form that can be converted/used as the approved Domain model.

Do not expose Infrastructure-specific fixture details.

Do not expose HTTP/network/storage concepts.

---

## 5.5 Research Use-Case Boundary

Define the Application-facing operation later implemented by WP05.

Conceptually:

```text
Execute research request
        ↓
return expected research outcome
```

The boundary must permit Worker to invoke Application without knowing the concrete implementation.

Do not implement the use case in WP04.

---

## 5.6 Research Result Contract

The result must associate the Domain outcome with Application metadata required by WP02.

Approved metadata:

```text
target
observation count
mean-price outcome
```

Do not add:

```text
provider
execution duration
database id
strategy id
confidence score
AI explanation
```

---

## 5.7 Expected Failure Outcomes

The contract design must support explicit expected failures for:

```text
invalid request
unsupported target
insufficient observations
```

WP02 states expected failures should not depend on exceptions as normal control flow.

Do not build a generalized error/result library.

Use the smallest explicit contract appropriate to this single use case.

---

# 6. Canonical Reference Scenario

The Application contracts must be capable of representing:

```text
Request:
  Target = SAMPLE-USD
  Requested count = 3

Expected success:
  Target = SAMPLE-USD
  Observation count = 3
  Mean price = 110.00
```

They must also represent expected failure semantics for:

```text
blank target
count = 0
unsupported target
requested count = 4 when only 3 observations exist
```

Do not implement fixture behavior here.

---

# 7. Exact Naming Policy

Do not blindly use conventional names.

Before creating types:

1. read the WP02 ubiquitous language;
2. inspect Application naming conventions;
3. inspect existing Application source;
4. map each approved concept to the smallest clear type name;
5. record that mapping in the execution report.

Names must express Release 0.9 semantics without pretending the platform already has real market-data providers or broader research infrastructure.

---

# 8. Authorized Scope

WP04 may:

- inspect Release 0.9 authority;
- inspect WP03 Domain types;
- add the minimum Application contract source files;
- minimally adjust Application-local organization if required by existing project conventions;
- use existing Application dependencies already present;
- use Domain types where ownership allows;
- build/format/test/verify;
- inspect project/package references;
- report complete evidence.

Primary mutation boundary:

```text
src/AIQuantTradingResearch.Application/**
```

Only contract-related changes are authorized.

---

# 9. Prohibited Scope

Do not:

- implement research orchestration;
- implement use-case concrete service/handler behavior;
- implement deterministic Infrastructure adapter;
- modify Infrastructure;
- modify Worker;
- register DI beyond what already exists;
- modify Domain unless a blocking authority defect is discovered;
- create Domain/Application/Infrastructure behavioral tests;
- modify Architecture.Tests unless explicitly required by the active prompt;
- add packages;
- add project references beyond the accepted Application -> Domain relationship;
- introduce MediatR or mediator frameworks;
- create HTTP clients;
- create repository/persistence abstractions;
- create plugin contracts;
- create strategy/backtest/AI abstractions;
- create generalized Result/Error frameworks;
- modify GitHub planning;
- stage, commit, push, or create a PR unless separately authorized by release governance;
- begin WP05.

---

# 10. Dependency Rules

Application must remain:

```text
Application -> Domain
```

Application must not depend on:

```text
Infrastructure
Worker
provider implementation
storage implementation
network implementation
```

Inspect the final project-reference graph.

No new production edge is authorized.

---

# 11. Working-Tree / Governance Protection

Record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify every pre-existing file.

Expected untracked artifacts may include:

```text
WP01 prompt/chat
WP02 prompt/chat
WP03 prompt/chat
WP04 prompt/chat
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
WP03 Domain source files
```

depending on prior governed integration timing.

These are intentional artifacts from preceding Release 0.9 work.

Do not delete, overwrite, stage, or "clean" them.

The prompt/chat companion convention remains intentional.

---

# 12. Implementation Procedure

## Step 1 — Read Authority

Read all required Release 0.9 and WP02/WP03 authority completely.

## Step 2 — Verify WP03 Baseline

Confirm:

```text
PriceObservation exists
ObservationSeries exists
MeanPrice exists
Domain remains dependency-free
WP03 validation passed
```

## Step 3 — Build Contract Mapping

Before coding, create an internal mapping:

| WP02 Application Concept | Proposed Contract Type | File | Responsibility |
| --- | --- | --- | --- |

Do not create extra types without an approved responsibility.

## Step 4 — Define Request Contract

Implement target + requested-count semantics at the Application boundary.

## Step 5 — Define Observation Source Contract

Implement the provider-neutral external observation dependency required by WP05/WP06.

## Step 6 — Define Use-Case Boundary

Implement only the abstraction/API through which Worker can invoke the research use case later.

## Step 7 — Define Success Result Contract

Represent target/count + Domain mean outcome.

## Step 8 — Define Expected Failure Contracts

Implement the smallest explicit representation for invalid request, unsupported target, and insufficient observations.

Do not create generic infrastructure.

## Step 9 — Review API Surface

Confirm every public type/member has an approved Release 0.9 responsibility.

## Step 10 — Dependency Inspection

Verify:

```text
Application -> Domain only
no new project references
no unauthorized packages
```

## Step 11 — Format / Build

Run repository formatting verification and solution build.

Require zero build errors.

## Step 12 — Architecture Tests

Run Architecture.Tests.

Existing Release 0.8 rules must remain passing.

## Step 13 — Canonical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit status 0.

## Step 14 — Scope Leakage Review

Search the Application delta for:

```text
Infrastructure
provider vendor names
HTTP
database
repository
plugin
strategy
backtest
AI
ML
Worker implementation
```

Inspect semantics, not text alone.

## Step 15 — Final Git Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
git diff --check
```

Distinguish pre-existing artifacts from WP04-owned changes.

---

# 13. Validation Expectations

WP04 must prove:

```text
Application contracts compile
Application retains only Domain project dependency
no Infrastructure reference
no Worker reference
no package added
request can express SAMPLE-USD / 3
success can express SAMPLE-USD / 3 / MeanPrice(110)
expected failures are explicitly representable
external dependency is Application-owned
no orchestration implementation exists
```

Behavioral testing is not owned by WP04.

Do not create placeholder tests merely to validate contracts.

---

# 14. Public API Review

For every public type/member added, record:

```text
Why is it public?
Which WP02 responsibility requires it?
Which later WP consumes it?
Could it be internal/private instead?
```

Minimize public surface.

---

# 15. Error Semantics Review

The final contract design must distinguish:

```text
Domain invariant violations
Application request validation failures
external observation-source expected failures
successful research outcome
```

Do not conflate them into one generic error mechanism.

Do not use exceptions as normal expected-flow signaling where WP02 explicitly rejected that approach.

---

# 16. Decision Model

Return exactly one:

```text
CONTRACTS COMPLETE
CONTRACTS COMPLETE WITH ACTIONS
CONTRACTS BLOCKED
```

Use `CONTRACTS COMPLETE` when:

```text
all approved Application contracts exist
ownership is correct
expected failures are representable
external abstraction is Application-owned
Application -> Domain remains exact
no orchestration/Infrastructure/Worker implementation leaked in
build/architecture/verify pass
WP05 can safely proceed
```

Use `CONTRACTS COMPLETE WITH ACTIONS` only for non-blocking observations that do not prevent WP05.

Use `CONTRACTS BLOCKED` when the contracts cannot be safely established within authority.

---

# 17. Acceptance Criteria

WP04 passes only when:

- [ ] WP04 prompt read completely.
- [ ] Release 0.9 execution plan and manifest read completely.
- [ ] `RESEARCH_DOMAIN_MODEL.md` read completely.
- [ ] WP03 Domain implementation inspected.
- [ ] Initial Git state classified.
- [ ] Contract mapping established before coding.
- [ ] Target responsibility represented.
- [ ] Requested-count responsibility represented.
- [ ] Research request contract created.
- [ ] Observation-source abstraction created and owned by Application.
- [ ] Research use-case boundary created.
- [ ] Success result contract created.
- [ ] Invalid-request outcome representable.
- [ ] Unsupported-target outcome representable.
- [ ] Insufficient-observations outcome representable.
- [ ] Expected failures do not require exceptions as normal control flow.
- [ ] No generalized error/result framework introduced.
- [ ] No provider/vendor-specific semantics leaked into Application.
- [ ] No Infrastructure implementation created.
- [ ] No Worker implementation created.
- [ ] No orchestration implementation created.
- [ ] No behavioral tests created.
- [ ] No new package added.
- [ ] Application project dependency remains Domain only.
- [ ] No production dependency cycle introduced.
- [ ] Public surface reviewed/minimized.
- [ ] Canonical SAMPLE-USD/count-3 scenario representable.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub planning mutation.
- [ ] WP05 not started.

---

# 18. Expected Output Contract

Return one complete:

```text
Release 0.9 WP04 Research Application Contracts Execution Report
```

Use this structure.

# Release 0.9 WP04 Research Application Contracts Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Objective:
Contracts created:
External abstraction:
Expected outcomes:
Dependencies introduced:
Technical validation:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP03 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths.

## 4. Contract Mapping

| WP02 Application Concept | Implemented Type | File | Responsibility |
| --- | --- | --- | --- |

## 5. Application Source Changes

| Path | Change | Purpose |
| --- | --- | --- |

## 6. Research Request Contract

```text
Type:
Target representation:
Count representation:
Validation semantics:
Assessment:
```

## 7. Observation Source Abstraction

```text
Type:
Ownership:
Input:
Output:
Expected failure semantics:
Infrastructure concepts exposed:
Assessment:
```

## 8. Research Use-Case Boundary

```text
Type:
Operation:
Input:
Output:
Implementation present:
Assessment:
```

## 9. Success Result Contract

```text
Type:
Application metadata:
Domain outcome:
Provider metadata:
Assessment:
```

## 10. Expected Failure Contracts

```text
Invalid request:
Unsupported target:
Insufficient observations:
Generic framework introduced:
Exceptions used as normal expected flow:
Assessment:
```

## 11. Public Surface Assessment

List public types/members and justify them.

## 12. Dependency Assessment

```text
Project references:
Package references:
Application -> Domain:
Infrastructure dependency:
Worker dependency:
Assessment:
```

## 13. Scope-Leakage Assessment

```text
Orchestration implementation:
Infrastructure implementation:
Worker implementation:
Vendor/provider semantics:
Persistence/network semantics:
Future-scope concepts:
Assessment:
```

## 14. Build / Architecture Validation

```text
Build:
Build errors:
Architecture.Tests:
Cycles:
Assessment:
```

## 15. Canonical Verification

```text
Command:
Exit status:
Warnings:
Assessment:
```

## 16. Behavioral-Test Boundary

State that WP04 does not own the later Application behavioral test suite and identify its authoritative later WP.

## 17. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 18. Final Git State

```text
WP04-owned changes:
Pre-existing changes:
Staged:
Tracked modifications:
Unexpected:
```

## 19. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Application-contracts-only | PASS/FAIL | |
| WP02 ownership preserved | PASS/FAIL | |
| Domain not redefined | PASS/FAIL | |
| No orchestration implementation | PASS/FAIL | |
| No Infrastructure implementation | PASS/FAIL | |
| No Worker implementation | PASS/FAIL | |
| No behavioral tests | PASS/FAIL | |
| No unauthorized dependencies | PASS/FAIL | |
| WP05 not started | PASS/FAIL | |

## 20. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 21. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 22. Final Decision

State exactly one:

```text
CONTRACTS COMPLETE
CONTRACTS COMPLETE WITH ACTIONS
CONTRACTS BLOCKED
```

Explain why.

## 23. Next Authorized Work Package

If and only if progression is permitted, identify:

```text
WP05 — Research Execution Use Case
```

Do not begin it.

---

# 19. Final Instruction

Execute Release 0.9 / WP04 — Research Application Contracts.

Use the WP02 `RESEARCH_DOMAIN_MODEL.md` and the WP03 Domain implementation as authority.

Implement only the minimum Application contracts required for:

```text
target
requested count
research request
research result
research-use-case boundary
Application-owned observation-source abstraction
invalid-request outcome
unsupported-target outcome
insufficient-observations outcome
```

Do not implement orchestration.

Do not implement Infrastructure.

Do not modify Worker.

Do not add packages or project references beyond the accepted Application -> Domain relationship.

Do not create generic result/mediator/repository/provider frameworks.

Build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/public surface/final Git scope, and return the complete WP04 execution report.

Finish with exactly one:

```text
CONTRACTS COMPLETE
CONTRACTS COMPLETE WITH ACTIONS
CONTRACTS BLOCKED
```

If complete, identify:

```text
WP05 — Research Execution Use Case
```

as next.

Do not execute WP05.

# Conclusion

WP04 establishes the Application contract boundary without allowing orchestration or infrastructure decisions to leak forward prematurely.

The progression is:

```text
WP03 Domain Implementation
        ↓
Research Request Contract
        ↓
Observation Source Abstraction
        ↓
Use-Case Boundary
        ↓
Success + Expected Failure Contracts
        ↓
Dependency/Public-Surface Validation
        ↓
CONTRACTS COMPLETE
        ↓
WP05 Research Execution Use Case
```

> **Application contracts should define exactly what the use case needs and returns—without knowing how Infrastructure will satisfy those needs.**

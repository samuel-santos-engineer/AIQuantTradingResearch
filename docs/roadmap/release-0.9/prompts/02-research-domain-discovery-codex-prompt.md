# Codex Execution Prompt — Release 0.9 / WP02 Research Domain Discovery

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 02 — Research Domain Discovery |
| Type | Research |
| GitHub Issue | #70 |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Execution Mode | Research, analysis, and durable architecture documentation |
| Primary Agent | Codex |
| Prerequisite | WP01 — Repository & Release Preflight = `READY` |
| Primary Artifact | `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md` |
| Expected Outcome | Discover and document the smallest meaningful deterministic research-domain model and ownership boundaries required for the first executable research vertical slice, without creating production C# implementation |

---

# 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP02 — Research Domain Discovery
```

against the current `AIQuantTradingResearch` repository.

WP02 is the **domain-discovery authority boundary** for Release 0.9.

Its job is to determine, before implementation:

- what the minimum research use case actually means;
- which concepts genuinely belong to Domain;
- which concepts belong to Application;
- what external dependency the Application requires;
- what deterministic reference scenario Release 0.9 will execute;
- what invariants must be protected;
- what concepts are explicitly deferred or rejected.

WP02 must not prematurely freeze implementation based on convenient names.

This is not an implementation work package.

Do not create production C#.

Do not begin WP03.

---

# 2. Authoritative Sources

Read completely before analysis:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/02-research-domain-discovery-codex-prompt.md
```

Review relevant existing architecture/design authority, including when present:

```text
README.md

docs/architecture/**
docs/design/**
docs/domain/**
docs/data/**
docs/handbook/**
docs/project/ROADMAP.md
```

Review existing implementation only to understand the accepted architectural boundaries:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
```

Review current test/architecture boundaries:

```text
tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
tests/AIQuantTradingResearch.Infrastructure.Tests/**
tests/AIQuantTradingResearch.Architecture.Tests/**
```

Do not treat planned/future documentation as current implementation.

---

# 3. Accepted WP01 Starting Baseline

Expected WP01 decision:

```text
READY
```

Expected starting facts:

```text
Release 0.8 = COMPLETE / CLOSED
Release 0.9 authority integrated on main
Milestone #40 = Open
Issues #69–#82 = Open / Todo
WP02 = not started
Solution projects = 8
Production graph = accepted
Cycles = 0
Architecture.Tests = 7/7
eng/verify.ps1 = PASS
Release 0.9 implementation leakage = none
Future-scope implementation leakage = none
```

Verify enough of this baseline to ensure WP02 is operating against the expected state.

Do not repeat WP01 exhaustively unless a material discrepancy is discovered.

---

# 4. Core Discovery Question

WP02 must answer one primary question:

> **What is the minimum domain necessary to execute one meaningful, deterministic, offline research operation in AIQuantTradingResearch?**

The answer must be smaller than the future research ecosystem.

Do not model everything the platform might eventually need.

---

# 5. Discovery Principles

## 5.1 Domain Before Types

Begin with concepts and behavior, not C# classes.

Do not start by inventing:

```text
ResearchRequest
ResearchResult
MarketDataPoint
Symbol
TimeRange
IMarketDataProvider
ResearchService
```

Those names may eventually be correct, but WP02 must justify them rather than assume them.

## 5.2 Smallest Meaningful Vertical Slice

The discovered model must support exactly one meaningful deterministic research execution.

Avoid speculative requirements for future:

```text
providers
strategies
backtesting
portfolios
orders
risk
storage
ML/AI
plugins
cloud
```

## 5.3 Domain Ownership Must Be Explicit

Every concept considered must be classified as:

```text
DOMAIN
APPLICATION
INFRASTRUCTURE
WORKER
FUTURE
REJECTED
```

Do not leave important ownership ambiguous.

## 5.4 Behavior Over Data Bags

A concept belongs in Domain because it owns domain meaning/invariants/behavior, not merely because it contains data.

Avoid anemic or ceremony-heavy modeling.

## 5.5 No Forced DDD

Do not introduce:

```text
aggregate roots
repositories
domain services
domain events
factories
specifications
base entities
strongly typed IDs
```

unless the discovered use case genuinely requires them.

## 5.6 Determinism

The Release 0.9 reference scenario must be reproducible without:

```text
network
credentials
database
clock-dependent behavior
random input
external service
```

Equivalent approved input must produce equivalent behavior/result.

---

# 6. Required Research Topics

WP02 must investigate and document each topic below.

## 6.1 Research Operation Definition

Determine the minimum semantic definition of a "research operation" for Release 0.9.

Answer:

```text
What question is the reference research operation trying to answer?
What input does the operation need?
What observations/data does it consume?
What domain behavior is applied?
What output is meaningful?
What makes the operation complete?
```

The operation should demonstrate architecture, not attempt quantitative sophistication.

---

## 6.2 Minimum Input Model

Determine the minimum information required to request the research operation.

Questions:

```text
Does the domain need an instrument/security identifier?
Does it need an observation interval or range?
Does it need a collection/window size?
Does it need another research parameter?
Which input belongs to Domain versus Application request contract?
What validation is domain-level versus application-level?
```

Do not add input merely because future providers may need it.

---

## 6.3 Minimum Observation Model

Determine the smallest observation representation required by the reference use case.

Questions:

```text
What does one observation represent?
Which values are required?
Which values have domain meaning?
Does time belong in the Domain concept?
Does ordering matter?
Are duplicate observations valid?
Are negative/zero values valid?
What invariants exist?
```

Do not model OHLCV, order books, trades, quotes, or futures metadata unless the reference operation actually requires them.

---

## 6.4 Minimum Result Model

Determine what the research operation should return.

Questions:

```text
What makes the result meaningful?
Which result fields are domain outcomes?
Which are Application metadata?
Does the result need status/error information?
What is deterministic?
What can be validated independently?
```

Avoid building a generic analytics result hierarchy.

---

## 6.5 Domain Invariants

Identify every invariant that materially protects the discovered model.

For each invariant record:

```text
Concept
Invariant
Reason
Invalid example
Expected behavior
Ownership
```

Examples of possible categories, not requirements:

```text
non-empty identifier
positive numeric value
valid sequence/window
minimum observation count
ordered observations
valid range
```

Only include invariants justified by the discovered reference use case.

---

## 6.6 Application Boundary

Determine what belongs in Application rather than Domain.

Identify:

```text
use-case input contract
use-case output contract
orchestration responsibility
external data dependency
validation that is use-case specific
mapping between external observation source and Domain
```

The Application owns abstractions it needs from Infrastructure.

---

## 6.7 Infrastructure Boundary

Define only the responsibility needed for WP06 later.

At this stage, describe conceptually:

```text
deterministic source of approved observations
offline
repeatable
no provider/vendor semantics
implements Application-owned abstraction later
```

Do not design the concrete class.

---

## 6.8 Worker Boundary

Define the minimum Worker responsibility for the reference scenario.

Expected conceptual ownership:

```text
composition
construct approved reference input
invoke Application use case
surface result
```

Worker must not own research/domain logic.

---

## 6.9 Error / Invalid-Input Semantics

Determine the minimum failure model required by the reference use case.

Questions:

```text
What invalid states are impossible to construct in Domain?
What invalid requests are rejected by Application?
What does the external abstraction do for unsupported input?
What belongs to exception semantics versus explicit result semantics?
```

Do not build a generalized error framework.

Align with existing repository error-handling philosophy when available.

---

## 6.10 Deterministic Reference Scenario

Define one canonical reference scenario that WP03–WP11 can implement and test.

It must include:

```text
Reference input
Known deterministic observations
Expected domain behavior
Expected research result
Invalid-case example(s)
```

Requirements:

```text
offline
repeatable
small
human-readable
not tied to a real provider
not dependent on current time
not random
```

The scenario is an engineering fixture, not a market-performance claim.

---

# 7. Candidate Concept Evaluation

For every serious candidate concept, build a table:

| Candidate Concept | Classification | Required Now? | Reason | Key Invariants / Responsibility | Deferred Alternative |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
DOMAIN
APPLICATION
INFRASTRUCTURE
WORKER
FUTURE
REJECTED
```

This is required evidence that WP02 did not simply invent a conventional architecture.

---

# 8. Explicit Future / Rejected Concepts

WP02 must create an explicit section separating Release 0.9 from later scope.

At minimum evaluate:

```text
real market-data provider
provider selection
exchange
broker
asset class hierarchy
OHLCV
order book
trade ticks
database/repository
historical ingestion
cache
strategy
backtest
portfolio
order
risk
plugin
AI/ML model
feature engineering
prediction
cloud deployment
REST API
UI
```

For each relevant concept classify:

```text
FUTURE
REJECTED FOR 0.9
NOT REQUIRED BY REFERENCE USE CASE
```

Do not document future concepts as if they are implemented.

---

# 9. Architecture Compatibility

The discovered design must preserve:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
```

No new production project is allowed.

No new project reference is required by WP02.

The model must be implementable inside the existing projects.

---

# 10. Primary Artifact

WP02 must create exactly one new durable research-domain authority by default:

```text
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Create the `docs/architecture/research/` directory if it does not exist.

Do not create additional research documents unless the execution plan/file manifest plus discovered necessity clearly require them.

The default is one document.

---

# 11. Required `RESEARCH_DOMAIN_MODEL.md` Structure

Use this structure unless repository documentation conventions require a minor formatting adjustment.

```text
# Research Domain Model

## 1. Purpose
## 2. Release 0.9 Context
## 3. Discovery Method
## 4. Reference Research Operation
## 5. Ubiquitous Language
## 6. Concept Ownership Matrix
## 7. Domain Concepts
## 8. Domain Invariants
## 9. Application Boundary
## 10. External Observation Boundary
## 11. Infrastructure Responsibility
## 12. Worker Responsibility
## 13. Error and Invalid-Input Semantics
## 14. Deterministic Reference Scenario
## 15. Candidate Concepts Considered
## 16. Explicit Non-Goals
## 17. Future / Deferred Concepts
## 18. Implementation Constraints for WP03–WP08
## 19. Testing Implications for WP09–WP12
## 20. Open Questions
## 21. Decision Summary
## 22. Conclusion
```

---

# 12. Ubiquitous Language Requirements

Define every approved term concisely.

For each term specify:

```text
Term
Definition
Owner
Used in Release 0.9?
Notes
```

Avoid multiple terms for the same concept.

Avoid implementation names in definitions unless already approved.

---

# 13. Implementation Constraints for WP03–WP08

The artifact must provide explicit constraints later WPs must follow.

At minimum state:

```text
Which Domain concepts WP03 may implement
Which Application contracts WP04 may implement
Which orchestration WP05 may implement
What abstraction WP06 must implement from Application
What DI responsibilities WP07 may register
What Worker flow WP08 may execute
```

These are boundaries, not source-code designs.

Do not specify exact filenames unless necessary and justified.

---

# 14. Testing Implications for WP09–WP12

Document what future tests must prove, conceptually:

```text
Domain invariant behavior
Application orchestration behavior
Deterministic adapter behavior
Architecture ownership/boundaries
```

Do not write tests during WP02.

Do not set an arbitrary coverage percentage.

---

# 15. Open Questions Policy

WP02 should resolve all questions required for WP03.

Open questions may remain only when they:

```text
do not affect WP03 implementation
belong to future releases
are explicitly deferred
```

If a question materially affects the minimum Domain model, WP02 is not complete.

---

# 16. Authorized Scope

WP02 may:

- inspect repository/domain/data/architecture documentation;
- inspect accepted source boundaries;
- analyze existing design constraints;
- reason about the minimum research vertical slice;
- create `docs/architecture/research/`;
- create `RESEARCH_DOMAIN_MODEL.md`;
- modify only that new document during WP02;
- run documentation quality checks;
- run canonical verification to prove documentation-only change does not affect technical baseline;
- inspect Git state before/after;
- return the complete discovery report.

---

# 17. Prohibited Scope

Do not:

- create or modify production C#;
- create or modify tests;
- modify `.csproj`;
- modify `.slnx`;
- change project references;
- add packages;
- modify DI registrations;
- modify Worker;
- implement Infrastructure;
- create provider interfaces/classes in source;
- modify build scripts;
- modify root configuration;
- create CI;
- modify GitHub milestone/issues/project status;
- mark WP02 Done;
- begin WP03;
- create generic DDD infrastructure;
- create additional research docs without demonstrated authority;
- rewrite unrelated architecture documentation.

---

# 18. Git / Working-Tree Protection

Record initial:

```text
git status --short
git branch --show-current
git rev-parse HEAD
```

Expected pre-existing untracked governance may include:

```text
docs/roadmap/release-0.9/prompts/01-repository-release-preflight-codex-prompt.md
docs/roadmap/release-0.9/prompts/01-repository-release-preflight-codex-prompt-chat.md
docs/roadmap/release-0.9/prompts/02-research-domain-discovery-codex-prompt.md
docs/roadmap/release-0.9/prompts/02-research-domain-discovery-codex-prompt-chat.md
```

These are intentional governance artifacts.

Do not modify, stage, delete, or classify valid prompt/chat companions as accidental.

Classify all pre-existing changes before creating the WP02 artifact.

WP02 should introduce only:

```text
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

unless a material authorized exception is proven.

---

# 19. Validation Procedure

## Step 1 — Read Authority

Read WP02 prompt, execution plan, file manifest, and relevant architecture/domain/data/design authority completely.

## Step 2 — Record Starting State

Record repository/branch/HEAD/Git status and confirm WP01 `READY` baseline.

## Step 3 — Inspect Existing Domain/Data Vocabulary

Search current documentation for existing authoritative or planned terminology.

Do not automatically adopt planned terms.

Classify their status.

## Step 4 — Define the Reference Research Operation

Establish the smallest meaningful deterministic operation.

## Step 5 — Discover Candidate Concepts

Identify candidate concepts without implementation bias.

## Step 6 — Classify Ownership

Assign each candidate to Domain/Application/Infrastructure/Worker/Future/Rejected.

## Step 7 — Define Invariants

Identify only the invariants required by the chosen operation.

## Step 8 — Define Boundaries

Define Application external needs, Infrastructure responsibility, and Worker responsibility.

## Step 9 — Define Deterministic Scenario

Create one canonical input/observation/result scenario.

## Step 10 — Validate Architecture Compatibility

Confirm the discovered model fits the existing four production projects and dependency graph.

## Step 11 — Write `RESEARCH_DOMAIN_MODEL.md`

Create the single authoritative artifact.

## Step 12 — Review for Premature Scope

Search the artifact for:

```text
real providers
persistence
plugins
backtesting
AI/ML
cloud
unjustified generic frameworks
```

Ensure these are future/non-goals rather than implementation commitments.

## Step 13 — Documentation Quality Validation

Run applicable Markdown/repository quality checks if available.

At minimum inspect:

```text
git diff --check
```

## Step 14 — Canonical Technical Verification

Run:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Expected:

```text
PASS
Architecture.Tests = existing accepted baseline
Build errors = 0
```

## Step 15 — Final Scope Inspection

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Confirm WP02 introduced no source/test/config/GitHub mutation.

---

# 20. Discovery Quality Tests

Before accepting the artifact, challenge it with these questions:

1. Can the reference operation be explained without implementation terminology?
2. Is every approved Domain concept necessary?
3. Could any Domain concept actually belong to Application?
4. Is any candidate present only because future providers may need it?
5. Can the complete reference scenario execute offline?
6. Can it execute without current time/randomness?
7. Does Application own the external abstraction?
8. Is Infrastructure replaceable?
9. Is Worker composition-focused?
10. Can WP03 implement the Domain without unresolved naming/ownership/invariant decisions?
11. Is future scope clearly separated?
12. Has unnecessary DDD ceremony been avoided?

Any material `No` requires revision before completion.

---

# 21. Finding Severity

Classify findings:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

A blocker prevents WP03.

Examples:

```text
reference operation cannot be defined coherently
core concept ownership remains ambiguous
minimum invariants unresolved
model requires forbidden dependency direction
model requires out-of-scope infrastructure
current authority materially conflicts
```

---

# 22. Decision Model

Return exactly one:

```text
DISCOVERY COMPLETE
DISCOVERY COMPLETE WITH ACTIONS
DISCOVERY BLOCKED
```

Use `DISCOVERY COMPLETE` when:

```text
minimum operation defined
concept ownership resolved
invariants resolved
deterministic scenario defined
WP03 constraints actionable
no blocking open question
artifact complete
```

Use `DISCOVERY COMPLETE WITH ACTIONS` only for non-blocking future/documentation observations that do not affect WP03.

Use `DISCOVERY BLOCKED` when WP03 cannot safely implement the Domain.

---

# 23. Acceptance Criteria

WP02 passes only when:

- [ ] WP02 prompt read completely.
- [ ] Execution plan and manifest read completely.
- [ ] Relevant architecture/domain/data/design docs inspected.
- [ ] WP01 READY baseline confirmed.
- [ ] Initial Git state recorded/classified.
- [ ] Reference research operation defined.
- [ ] Minimum input responsibility defined.
- [ ] Minimum observation model defined.
- [ ] Minimum result responsibility defined.
- [ ] Candidate concepts classified by owner.
- [ ] Approved Domain concepts justified.
- [ ] Domain invariants explicitly documented.
- [ ] Application boundary defined.
- [ ] External observation dependency defined conceptually.
- [ ] Infrastructure responsibility defined.
- [ ] Worker responsibility defined.
- [ ] Error/invalid-input semantics defined sufficiently for WP03–WP05.
- [ ] Deterministic reference scenario defined.
- [ ] Future/rejected concepts explicitly separated.
- [ ] Architecture compatibility proven.
- [ ] No new project/reference required.
- [ ] `RESEARCH_DOMAIN_MODEL.md` created.
- [ ] Required document sections complete.
- [ ] WP03 implementation constraints actionable.
- [ ] WP09–WP12 testing implications documented.
- [ ] No blocking open question remains.
- [ ] No production C# created/modified.
- [ ] No tests created/modified.
- [ ] No DI/Worker/Infrastructure implementation created.
- [ ] No package/config/build mutation.
- [ ] No GitHub planning mutation.
- [ ] `git diff --check` passes.
- [ ] Canonical verification passes.
- [ ] Final Git state inspected.
- [ ] WP03 not started.

---

# 24. Expected Output Contract

Return one complete:

```text
Release 0.9 WP02 Research Domain Discovery Execution Report
```

Use this structure.

# Release 0.9 WP02 Research Domain Discovery Execution Report

## 1. Executive Summary

```text
Release:
Work Package:
Discovery objective:
Reference operation:
Domain concepts approved:
Application concepts approved:
Infrastructure responsibility:
Worker responsibility:
Artifact:
Final decision:
```

## 2. Execution Context

```text
Repository:
Branch:
HEAD:
Initial Git state:
WP01 baseline:
```

## 3. Authoritative Sources Reviewed

List exact paths and classify relevant documents as current/planned/historical where material.

## 4. Existing Vocabulary Assessment

| Existing Term / Concept | Source | Status | Reused? | Reason |
| --- | --- | --- | --- | --- |

## 5. Reference Research Operation

Describe the minimum operation and why it is meaningful for Release 0.9.

## 6. Candidate Concept Matrix

| Candidate | Classification | Required Now | Reason | Responsibility / Invariants |
| --- | --- | --- | --- | --- |

## 7. Approved Domain Model

Describe only approved Domain concepts and their relationships.

## 8. Domain Invariants

| Concept | Invariant | Reason | Invalid Example | Expected Behavior |
| --- | --- | --- | --- | --- |

## 9. Application Boundary

Describe input/output/use-case/external abstraction responsibilities.

## 10. Infrastructure Boundary

Describe deterministic adapter responsibility without concrete implementation design.

## 11. Worker Boundary

Describe composition/reference-execution responsibility.

## 12. Error / Invalid-Input Semantics

Document the agreed ownership and behavior.

## 13. Deterministic Reference Scenario

```text
Input:
Observations:
Domain behavior:
Expected result:
Invalid cases:
Determinism proof:
```

## 14. Future / Rejected Concepts

| Concept | Classification | Reason Deferred/Rejected |
| --- | --- | --- |

## 15. Architecture Compatibility

```text
New projects required:
Project-reference changes required:
Domain dependency:
Application dependency:
Infrastructure dependency:
Worker dependency:
Assessment:
```

## 16. Created Artifact

```text
Path:
Sections:
Scope:
Assessment:
```

## 17. WP03–WP08 Implementation Constraints

List actionable boundaries for later implementation WPs.

## 18. WP09–WP12 Testing Implications

List behaviors/boundaries future test WPs must prove.

## 19. Open Questions

List only non-blocking deferred questions.

If none:

```text
None
```

## 20. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 21. Scope Compliance

| Check | Result | Evidence |
| --- | --- | --- |
| Research/design only | PASS/FAIL | |
| Single durable research artifact | PASS/FAIL | |
| No production code | PASS/FAIL | |
| No test code | PASS/FAIL | |
| No DI/Worker/Infrastructure implementation | PASS/FAIL | |
| No project/package/config mutation | PASS/FAIL | |
| No GitHub planning mutation | PASS/FAIL | |
| WP03 not started | PASS/FAIL | |

## 22. Findings

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

## 23. Acceptance Criteria Matrix

Reproduce applicable criteria with:

```text
PASS
FAIL
N/A
```

## 24. Final Decision

State exactly one:

```text
DISCOVERY COMPLETE
DISCOVERY COMPLETE WITH ACTIONS
DISCOVERY BLOCKED
```

Explain why.

## 25. Next Authorized Work Package

If and only if discovery permits progression:

```text
WP03 — Research Domain Model
```

Do not begin it.

---

# 25. Final Instruction

Execute Release 0.9 / WP02 — Research Domain Discovery.

This is a discovery/documentation work package, not implementation.

Read Release 0.9 authority and relevant repository architecture/domain/data/design documentation.

Define the minimum meaningful deterministic research operation.

Discover the minimum concepts required to support it.

Classify every important concept as:

```text
DOMAIN
APPLICATION
INFRASTRUCTURE
WORKER
FUTURE
REJECTED
```

Define Domain invariants, Application boundaries, conceptual external-observation dependency, deterministic Infrastructure responsibility, Worker responsibility, invalid-input semantics, and one canonical deterministic reference scenario.

Create exactly one durable artifact by default:

```text
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Do not create C#.

Do not create tests.

Do not implement DI, Infrastructure, or Worker behavior.

Do not add packages or projects.

Do not modify GitHub planning.

Validate the document for scope discipline, run `git diff --check`, run canonical verification, and inspect final Git state.

Return the complete **Release 0.9 WP02 Research Domain Discovery Execution Report**.

Finish with exactly one:

```text
DISCOVERY COMPLETE
DISCOVERY COMPLETE WITH ACTIONS
DISCOVERY BLOCKED
```

If complete, identify:

```text
WP03 — Research Domain Model
```

as next.

Do not execute WP03.

---

# Conclusion

WP02 is where Release 0.9 decides what its research domain actually is before code makes those decisions expensive.

The correct sequence is:

```text
Trusted WP01 Baseline
        ↓
Research Operation Defined
        ↓
Concepts Discovered
        ↓
Ownership Classified
        ↓
Invariants Defined
        ↓
Deterministic Scenario Defined
        ↓
Future Scope Rejected/Deferred
        ↓
RESEARCH_DOMAIN_MODEL.md
        ↓
DISCOVERY COMPLETE
        ↓
WP03 Research Domain Model
```

> **The domain model should emerge from the minimum useful research behavior—not from the classes an implementation happens to make convenient.**

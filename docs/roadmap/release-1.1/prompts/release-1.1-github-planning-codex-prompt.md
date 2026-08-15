# Release 1.1 GitHub Planning — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.1 — Market Data Persistence Foundation
**Phase:** Phase 3
**Lifecycle step:** GitHub planning
**Purpose:** Reconcile GitHub planning to the authoritative Release 1.1 repository design before any WP01 implementation begins
**Success terminal:** `RELEASE 1.1 GITHUB PLANNING COMPLETE`
**Failure terminal:** `RELEASE 1.1 GITHUB PLANNING BLOCKED`

This prompt is the authoritative GitHub-planning contract for Release 1.1.

It may reconcile GitHub milestone/issues/Project planning state only within the exact Release 1.1 model defined by:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md
```

It must not start WP01 implementation, modify production/test code, create a release branch, create a PR, or begin Release 1.2.

---

## 1. Mandatory Authorities

Read completely before any GitHub mutation:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md
docs/roadmap/release-1.1/prompts/release-1.1-github-planning-codex-prompt.md
```

Also inspect:

- the successful Release 1.0 post-merge closure result;
- current GitHub milestones;
- current open and closed GitHub issues;
- existing labels;
- existing Project #2:
  `AIQuantTradingResearch Engineering Roadmap`;
- existing Project fields/options;
- retired legacy milestone #42;
- Release 1.0 milestone #41 and issues #86–#101 read-only for convention reference.

Authority precedence:

1. `RELEASE_1.1_EXECUTION_PLAN.md`
2. `RELEASE_1.1_FILE_MANIFEST.md`
3. this planning prompt
4. successful Release 1.0 closure state
5. existing repository/GitHub planning conventions

If existing GitHub planning conflicts materially with Release 1.1 repository authority, do not silently reinterpret the release. Reconcile only where this prompt explicitly authorizes it; otherwise stop and report the minimum human governance decision required.

---

## 2. Release 1.0 Closure Gate

Before planning mutation prove:

```text
Release 1.0 terminal = RELEASE 1.0 CLOSED
Release 1.0 milestone #41 = CLOSED
Release 1.0 issues #86–#101 = 16 CLOSED / 0 OPEN
Release 1.0 Project items = terminal
Release 1.1 implementation started = NO
```

If Release 1.0 is not closed, stop.

---

## 3. Release 1.1 Authoritative Identity

Release 1.1 is:

```text
Phase 3 - Release 1.1: Market Data Persistence Foundation
```

Authoritative objective:

> Establish the first durable, provider-independent historical market-data persistence vertical slice so normalized market observations acquired through the Release 1.0 market-data boundary can be stored, reconstructed, retrieved, and reused deterministically without coupling Domain or Application to a concrete storage technology.

The milestone description must remain concise and derived from this objective.

Recommended milestone description:

```text
Establish durable, provider-independent historical market-data persistence so normalized observations can be stored, reconstructed, retrieved, and reused deterministically while storage technology remains confined to Infrastructure.
```

Do not redefine Release 1.1 as the legacy “Market Data Platform” concept.

---

## 4. Legacy Milestone #42 Reconciliation

A retired legacy milestone exists:

```text
#42 — Phase 3 - Release 1.1: Market Data Platform
```

The successful Release 1.0 closure established it as:

```text
state = CLOSED
open issues = 0
```

This prompt explicitly authorizes narrow reconciliation of milestone #42 only under the following rules:

### Preferred strategy

If milestone #42 remains closed and empty:

- do not reopen it automatically;
- do not repurpose it silently;
- do not delete it;
- preserve it as historical roadmap state.

Create or reconcile exactly one new authoritative Release 1.1 milestone with the title:

```text
Phase 3 - Release 1.1: Market Data Persistence Foundation
```

### Collision rule

If another milestone already has the authoritative title, reconcile duplicates before issue creation.

### Prohibited

Do not:

- reopen #42 merely because the release number matches;
- rename #42 unless a later explicit human authority requires historical repurposing;
- move issues into #42;
- delete #42;
- create two active Release 1.1 milestones.

The final planning state must contain exactly one open authoritative Release 1.1 milestone.

---

## 5. Exactly Sixteen Work Packages

Create/reconcile exactly these sixteen Release 1.1 work-package issues:

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

No WP17+ issue is authorized.

Do not create separate lifecycle-gate issues for:

```text
Git/GitHub Integration
Human Merge
Post-Merge Closure
```

unless a later authority explicitly changes this planning model.

---

## 6. Authoritative Dependency Graph

The exact dependency model is:

```text
WP01 ← Release 1.0 CLOSED
WP02 ← WP01
WP03 ← WP02
WP04 ← WP03
WP05 ← WP04
WP06 ← WP05
WP07 ← WP06
WP08 ← WP07
WP09 ← WP08
WP10 ← WP09
WP11 ← WP10
WP12 ← WP11
WP13 ← WP03 + WP04 + WP05
WP14 ← WP06 + WP07 + WP08 + WP09 + WP10 + WP11 + WP12
WP15 ← WP13 + WP14
WP16 ← WP15
```

Do not add artificial dependency edges.

Do not omit any required edge.

GitHub issue bodies must express dependencies consistently with this graph.

If the repository/project tooling supports formal issue dependencies, use them only if already-established conventions make this safe. Otherwise encode dependencies in the issue body exactly and validate drift = 0.

---

## 7. Issue Title Contract

Issue titles must be:

```text
Release 1.1 WP01 — Release & Repository Preflight
Release 1.1 WP02 — Persistence Technology Discovery
Release 1.1 WP03 — Historical Observation Persistence Semantics
Release 1.1 WP04 — Application Persistence Contracts
Release 1.1 WP05 — Persistence Use-Case Integration
Release 1.1 WP06 — Storage Physical Model
Release 1.1 WP07 — Storage Engine & Connection Boundary
Release 1.1 WP08 — Observation Persistence
Release 1.1 WP09 — Historical Observation Retrieval
Release 1.1 WP10 — Storage Validation & Failure Mapping
Release 1.1 WP11 — Dependency Registration & Configuration
Release 1.1 WP12 — Worker Persistent Market-Data Execution
Release 1.1 WP13 — Domain & Application Tests
Release 1.1 WP14 — Infrastructure & Persistence Tests
Release 1.1 WP15 — Architecture & Documentation Alignment
Release 1.1 WP16 — Full Validation, Integration & Acceptance
```

If repository conventions established in Release 1.0 use an equivalent exact pattern without the release prefix in the visible title, preserve that convention only if duplicate ambiguity remains impossible. Report the final title model.

---

## 8. Required Issue Body Contract

Every Release 1.1 WP issue must contain these sections:

```text
## Objective
## Scope
## Dependencies
## Deliverables
## Validation Evidence
## Exit Criteria
## Out of Scope
## Authority
```

Each issue must reference:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md
```

Issue bodies must be planning summaries, not copies of the full execution plan.

Do not place implementation code in issue bodies.

---

## 9. WP01 Issue Contract

### Objective

Establish the exact Release 1.1 starting state and prove the repository is safe for persistence work.

### Scope

Include:

- Release 1.0 closure verification;
- synchronized clean `main`;
- test/build/architecture baseline;
- current persistence-surface inventory;
- legacy Release 1.1 planning inspection;
- proof that Release 1.1 implementation has not begun.

### Dependencies

```text
Release 1.0 CLOSED
```

### Exit

WP01 completion must not imply WP02 implementation has started.

---

## 10. WP02 Issue Contract

### Objective

Select the minimum credible persistence technology through explicit evidence.

### Deliverables

Expected:

```text
MARKET_DATA_PERSISTENCE_ASSESSMENT.md
MARKET_DATA_PERSISTENCE_DECISION.md
```

### Scope

Technology evaluation only.

### Out of Scope

No packages, schema, production persistence implementation, DI, Worker, or WP03+ implementation.

---

## 11. WP03 Issue Contract

### Objective

Define provider-independent persistence semantics.

Include:

- observation identity;
- timestamp semantics;
- normalized price semantics;
- canonical ordering;
- duplicate/idempotency policy;
- conflicting duplicate behavior;
- empty retrieval behavior;
- Domain delta assessment.

A zero Domain delta must remain explicitly valid.

---

## 12. WP04 Issue Contract

### Objective

Define minimal provider-independent Application persistence/retrieval contracts.

Explicitly prohibit:

```text
SQL
ORM
DbContext
table/row types
filesystem path contracts
storage-engine exceptions
provider transport types
```

---

## 13. WP05 Issue Contract

### Objective

Integrate persistence behavior into Application orchestration while preventing persistence from becoming an implicit cache policy.

Require:

- WP04 contract reuse;
- Release 1.0 behavior preservation;
- provider-independent failure propagation;
- zero storage-engine branching in Application.

---

## 14. WP06 Issue Contract

### Objective

Define the minimum Infrastructure-owned storage physical model.

Require:

- identity/unique constraint;
- timestamp representation;
- price representation;
- nullability/constraints;
- mapping to/from `PriceObservation`.

Do not authorize generalized future analytics schemas.

---

## 15. WP07 Issue Contract

### Objective

Introduce the selected storage engine and connection/lifecycle boundary.

This WP owns:

- persistence package introduction if required;
- central package governance changes;
- Infrastructure project package reference if required;
- connection/init/bootstrap mechanics;
- test-isolation-compatible storage lifecycle.

No cloud dependency is authorized.

---

## 16. WP08 Issue Contract

### Objective

Implement durable historical observation writes.

Require:

- true durability;
- idempotent equivalent duplicates;
- conflicting duplicate handling;
- correct timestamp/price preservation;
- reconstruction survivability.

An in-memory dictionary alone is not acceptable.

---

## 17. WP09 Issue Contract

### Objective

Implement deterministic historical observation retrieval.

Require:

- target filtering;
- canonical ascending ordering;
- valid empty behavior;
- provider-independent observation reconstruction;
- zero storage-record leakage.

---

## 18. WP10 Issue Contract

### Objective

Map concrete storage conditions into approved provider-independent failures.

Explicitly prohibit broad `catch (Exception)` collapse that masks programming defects.

---

## 19. WP11 Issue Contract

### Objective

Wire persistence through existing DI/configuration.

Require:

- correct Application contract → Infrastructure implementation resolution;
- correct lifetimes;
- no hidden in-memory fallback;
- no storage mutation on resolution unless initialization semantics explicitly require it.

---

## 20. WP12 Issue Contract

### Objective

Prove persistence-enabled market-data execution through the Worker composition root.

Require:

```text
Worker
→ Application
→ Release 1.0 acquisition
→ normalized observations
→ persistence contract
→ Infrastructure persistence
```

and authorized retrieval/research continuation.

No CLI/service/API redesign.

---

## 21. WP13 Issue Contract

### Objective

Permanently prove Domain/Application behavior independent of concrete storage.

Expected test surfaces:

```text
Domain.Tests
Application.Tests
```

Concrete database dependency in Application tests is prohibited.

---

## 22. WP14 Issue Contract

### Objective

Permanently prove the selected persistence implementation offline.

Required coverage themes:

- bootstrap/schema;
- write/read;
- multiple targets;
- timestamp/price round trip;
- ordering;
- idempotent duplicate;
- conflicting duplicate;
- empty storage;
- deterministic failure mapping;
- reconstruction durability;
- DI;
- lifecycle;
- isolation;
- zero test residue;
- zero provider/network dependency.

---

## 23. WP15 Issue Contract

### Objective

Align executable architecture enforcement and current-state documentation with Release 1.1 persistence truth.

Architecture scope must evaluate:

- Domain storage independence;
- Application concrete-storage independence;
- Application ownership of persistence contracts;
- Infrastructure ownership of persistence implementation;
- preservation of Release 1.0 provider boundary;
- acyclicity.

Documentation scope must be manifest-based and gap-driven.

---

## 24. WP16 Issue Contract

### Objective

Perform full validation and technical acceptance of the cumulative Release 1.1 candidate.

Require:

- restore/build/test;
- architecture tests;
- canonical verification;
- diff checks;
- durable reconstruction scenario;
- no test residue;
- fresh-checkout validation.

Valid terminal concepts:

```text
RELEASE 1.1 ACCEPTED
RELEASE 1.1 ACCEPTANCE BLOCKED
```

WP16 does not commit, push, create a PR, merge, or close Release 1.1.

---

## 25. Duplicate Reconciliation

Before creating anything:

1. search open and closed milestones for Release 1.1 equivalents;
2. search open and closed issues for equivalent WP01–WP16 issues;
3. inspect retired milestone #42;
4. inspect Project items to avoid duplicate addition.

Rules:

- reuse an existing issue only if scope/title/authority are materially equivalent;
- do not overwrite a conflicting issue into a different work package;
- do not create duplicate WP identities;
- one WP must map to exactly one authoritative issue;
- final authoritative issue count must equal 16.

If an existing issue contains material scope conflict, stop or create the authoritative issue only if doing so does not create ambiguous duplicate planning. Report the decision.

---

## 26. Milestone Creation / Reconciliation

Final required milestone:

```text
Title:
Phase 3 - Release 1.1: Market Data Persistence Foundation

State:
OPEN

Open issues:
16 after planning completes

Closed issues:
0
```

If a new milestone is required, create exactly one.

If the authoritative milestone already exists and is compatible, reuse it.

Final Release 1.1 milestone duplicates:

```text
0
```

Retired #42 remains closed unless separate later human governance explicitly changes it.

---

## 27. Labels

Inspect existing labels before mutation.

Reuse existing semantic labels wherever possible.

Do not create a new label solely because a work package title is new.

Recommended mapping using established repository conventions:

```text
WP01  → research
WP02  → research
WP03  → architecture or feature
WP04  → feature
WP05  → feature
WP06  → feature or infra
WP07  → infra or feature
WP08  → feature
WP09  → feature
WP10  → feature
WP11  → feature or architecture
WP12  → feature
WP13  → tests
WP14  → tests
WP15  → architecture and/or documentation according to existing single-label convention
WP16  → research or validation-equivalent existing label
```

Use only labels that actually exist.

Do not rename/delete/repurpose existing labels.

If the repository convention permits only one primary semantic label per issue, preserve it.

Report the exact final mapping.

---

## 28. Assignee

Assign all sixteen WP issues to:

```text
samuel-santos-engineer
```

if repository permissions and established Release 1.0 convention support it.

Do not invent additional assignees.

---

## 29. Project Integration

Add all sixteen issues to existing Project #2:

```text
AIQuantTradingResearch Engineering Roadmap
```

Do not create a new Project.

Inspect existing field options before writing.

Required fields where corresponding existing options are available:

```text
Release = 1.1
Priority = P1
Area = appropriate existing value
Status = Backlog or existing planning-entry status
```

Do not create or redesign Project fields/options/workflows.

### Project automation

Release 1.0 planning demonstrated that Project automation may override requested status.

If automation changes a newly added issue from the requested planning status:

- do not fight or disable the automation;
- record the final actual status;
- treat it as non-blocking if issue/milestone/dependency/release identity remains correct.

---

## 30. Recommended Area Mapping

Use existing matching Project options only.

Recommended conceptual mapping:

```text
WP01 → Engineering
WP02 → Data
WP03 → Architecture
WP04 → Architecture
WP05 → Architecture
WP06 → Infrastructure
WP07 → Infrastructure
WP08 → Infrastructure
WP09 → Infrastructure
WP10 → Infrastructure
WP11 → Architecture
WP12 → Host
WP13 → Testing
WP14 → Testing
WP15 → Architecture or Documentation according to existing Project conventions
WP16 → Validation
```

If an exact option does not exist, choose the closest established option and report the reconciliation.

Do not create a new option without separate authority.

---

## 31. Priority / Release Fields

Expected:

```text
Priority = P1 on 16/16
Release = 1.1 on 16/16
```

If Project Release option `1.1` does not exist, stop before inventing it unless existing Project governance clearly allows adding release options as part of standard release planning.

If option creation is standard and required, create only the `1.1` option and report it.

Do not modify unrelated release options.

---

## 32. Issue State

Final planning state must be:

```text
WP01–WP16 issues = OPEN
closed Release 1.1 WP issues = 0
```

Planning does not start implementation.

Do not close WP01.

Do not mark WP01 complete.

---

## 33. Repository Mutation Protection

This GitHub-planning execution must not modify repository content.

Do not:

```text
edit RELEASE_1.1_EXECUTION_PLAN.md
edit RELEASE_1.1_FILE_MANIFEST.md
edit planning prompt files
create WP01 implementation files
stage
commit
create branch
push
create PR
modify .github/**
modify scripts
```

The only repository-local state permitted is the pre-existing uncommitted Release 1.1 governance directory created by the human workflow.

Preserve it unchanged.

---

## 34. Release 1.2 Protection

Do not create:

```text
Release 1.2 milestone
Release 1.2 issues
Release 1.2 Project items
Release 1.2 branch
Release 1.2 prompts
```

Release 1.2 governance design is not authorized until Release 1.1 is closed.

---

## 35. Planning Validation Matrix

After all permitted mutations, validate:

| Requirement | Expected |
|---|---|
| Authoritative Release 1.1 milestone | exactly 1 |
| Authoritative milestone state | OPEN |
| WP issues | exactly 16 |
| WP01–WP16 represented | 16/16 |
| WP17+ issues | 0 |
| Lifecycle-gate issues | 0 |
| Open WP issues | 16 |
| Closed WP issues | 0 |
| Milestone assignments | 16/16 |
| Assignee | 16/16 |
| Required issue-body sections | 8/8 each |
| Authority references | 2/2 each |
| Dependency graph drift | 0 |
| Project items added/reconciled | 16/16 |
| Release field | 1.1 on 16/16 |
| Priority field | P1 on 16/16 |
| Area field | populated on 16/16 using existing options |
| Duplicate WP issues | 0 |
| Active conflicting Release 1.1 milestone | 0 |
| Release 1.2 planning created | 0 |
| Repository files modified | 0 |

---

## 36. Final Planning State

Required:

```text
Release 1.1 milestone:
  exactly one authoritative milestone
  OPEN
  16 open issues
  0 closed issues

Release 1.1 issues:
  WP01–WP16 each exactly once
  all OPEN
  all assigned
  dependency drift = 0

Project:
  all 16 items represented
  Release = 1.1
  Priority = P1
  Area populated
  final Status recorded truthfully

Legacy milestone #42:
  CLOSED
  not reused automatically
  not deleted

Release 1.2:
  no active planning
```

---

## 37. Findings Classification

Use:

```text
OBSERVATION
BLOCKER
```

Examples of blockers:

- Release 1.0 not actually closed;
- authoritative milestone identity conflict cannot be reconciled safely;
- duplicate WP issue identities;
- Project Release `1.1` cannot be represented without unauthorized schema redesign;
- dependency graph cannot be represented consistently;
- repository authority conflicts with GitHub planning state;
- creating the required planning state would require rewriting unrelated historical planning.

Do not downgrade material governance conflicts.

---

## 38. Required Execution Report

Return:

```text
# Release 1.1 GitHub Planning Execution Report
```

with:

1. Executive Summary
2. Authorities Reviewed
3. Authentication / Repository Context
4. Release 1.0 Closure Gate
5. Existing GitHub Planning-State Inspection
6. Legacy Milestone #42 Reconciliation
7. Duplicate Reconciliation
8. Authoritative Milestone Result
9. Issue Creation / Reconciliation
10. Work-Package Mapping
11. Dependency Validation
12. Labels
13. Assignees
14. GitHub Project Integration
15. Project Automation Observations
16. Release / Priority / Area Validation
17. Scope Protection
18. Repository Mutation Check
19. Release 1.2 Protection
20. Findings / Observations
21. Planning Acceptance Matrix
22. Final GitHub Planning State
23. Final Decision
24. Next Authorized Action

The Work-Package Mapping must include:

```text
WP
Issue number/link
Title
Milestone
State
Dependencies
Label
Assignee
Project status
Priority
Release
Area
```

Do not omit dependency evidence.

---

## 39. Success Criteria

Release 1.1 GitHub planning succeeds only if:

```text
Release 1.0 closure gate = PASS

authoritative Release 1.1 milestone = exactly 1
authoritative milestone state = OPEN
legacy #42 preserved closed

WP01–WP16 represented = 16/16
duplicate WP issues = 0
WP17+ issues = 0
lifecycle-gate issues = 0

all WP issues = OPEN
milestone assignments = 16/16
assignee assignments = 16/16
required body contract = 16/16
authority references = 16/16
dependency drift = 0

Project items = 16/16
Release = 1.1 on 16/16
Priority = P1 on 16/16
Area populated on 16/16

repository mutations = 0
WP01 implementation started = NO
Release 1.2 planning = 0
```

The exact success terminal is:

```text
RELEASE 1.1 GITHUB PLANNING COMPLETE
```

---

## 40. Failure Criteria

If any mandatory planning criterion fails, end exactly:

```text
RELEASE 1.1 GITHUB PLANNING BLOCKED
```

State the minimum human governance decision required.

Do not start WP01.

---

## 41. Next Authorized Action After Success

A successful GitHub-planning run does not start implementation automatically.

The next action is:

```text
Human review and acceptance of the Release 1.1 GitHub planning state.
```

After human acceptance, the first separately authorized implementation package is:

```text
WP01 — Release & Repository Preflight
```

WP01 requires its own authoritative Codex prompt and standard five-line prompt-chat companion.

Do not start WP01 during this planning run.

---

## Execution Instruction

Read the Release 1.1 execution plan and file manifest completely; prove Release 1.0 is formally closed; inspect all current milestones/issues/labels/Project state; preserve legacy milestone #42 closed as historical planning unless a material conflict requires a human decision; create or reconcile exactly one open milestone named `Phase 3 - Release 1.1: Market Data Persistence Foundation`; create/reconcile exactly sixteen open WP01–WP16 issues using the authoritative titles, issue-body contract, and exact dependency graph; assign them to `samuel-santos-engineer`; reuse existing labels and Project #2 conventions; set Release 1.1, P1, and appropriate existing Area fields; record Project automation truthfully without redesigning it; create no WP17+, lifecycle-gate, or Release 1.2 planning objects; make no repository mutation; return the complete planning report; emit `RELEASE 1.1 GITHUB PLANNING COMPLETE` only when every planning gate passes; and stop before WP01.

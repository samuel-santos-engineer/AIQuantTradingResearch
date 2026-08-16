# Release 1.2 Planning / Definition — Authoritative Codex Prompt

## 1. Authority Identity

This document is the authoritative execution contract for **Release 1.2 planning / definition** in:

`samuel-santos-engineer/AIQuantTradingResearch`

It begins only from the formally closed Release 1.1 baseline.

This authority is **definition-only**. It may inspect, reconcile, reason, and produce a Release 1.2 definition report. It must not activate Release 1.2 planning or implementation.

Success terminal:

`RELEASE 1.2 DEFINITION COMPLETE`

Blocked terminal:

`RELEASE 1.2 DEFINITION BLOCKED`

---

## 2. Accepted Starting Baseline

Treat the following Release 1.1 closure state as the required predecessor baseline and verify it before proceeding:

- Release 1.1: **CLOSED**
- PR #120: **MERGED**
- Accepted merged `main`: `465c7d2f2c1cc5f99b4aa72a8d685db18951a9ad`
- Release 1.1 milestone #52: **CLOSED**
- Issues #103–#118: **16/16 Closed / Done**
- Permanent tests: **145/145**
- Release 1.2 active planning: **0**
- Working tree at closure: **CLEAN**

Do not silently repair predecessor drift. If a material predecessor condition is no longer true, stop and report the smallest corrective authority required.

---

## 3. Historical Release 1.2 Object

The repository/GitHub history contains the legacy milestone:

`#43 — Phase 3 - Release 1.2: Storage`

Its accepted historical state at Release 1.1 closure is:

- State: **CLOSED**
- Issues: **0**
- Historical/legacy planning only

Treat milestone #43 as evidence of earlier roadmap intent, **not** as the authoritative identity or scope of the new Release 1.2.

Do not reopen, rename, delete, repurpose, populate, or otherwise mutate milestone #43.

---

## 4. Purpose

Determine the authoritative **Release 1.2 definition** from current repository truth, roadmap intent, accepted architecture, completed Release 1.0 and Release 1.1 capabilities, and the next coherent platform increment.

The result must establish enough precision for a later, separately authorized design step to create:

- `docs/roadmap/release-1.2/RELEASE_1.2_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.2/RELEASE_1.2_FILE_MANIFEST.md`

This authority itself must **not create those files**.

---

## 5. Authority Precedence

When evidence conflicts, use this order:

1. Current merged `main` and executable repository truth
2. Formal Release 1.1 post-merge closure result
3. Current authoritative roadmap/product/architecture documentation
4. Accepted Release 1.0 and Release 1.1 governance and implementation results
5. Existing GitHub planning conventions and Project #2 taxonomy
6. Closed historical/legacy roadmap objects, including milestone #43

Historical names or plans must not override current repository truth.

---

## 6. Required Read Set

Before defining Release 1.2, inspect completely or sufficiently to establish current truth:

### Release governance
- Release 1.1 execution plan
- Release 1.1 file manifest
- Release 1.1 WP16 authority/result
- Release 1.1 post-merge closure result
- Relevant Release 1.0 closure evidence

### Roadmap/product
- Current repository roadmap
- Project/product vision
- Project status or equivalent roadmap-status artifacts
- Any documentation that explicitly names or describes Release 1.2 or the next Phase 3 increment

### Architecture/data
- Current solution architecture
- Data platform vision
- Data lifecycle
- Data pipeline architecture
- Data storage architecture
- Data provider architecture
- Relevant module/boundary/dependency documentation

### Current implementation
Inspect the actual merged production and permanent-test state necessary to understand what Release 1.1 now provides and what remains absent.

### GitHub
Inspect:
- milestones
- Release 1.2-related issues, if any
- Project #2 fields/options relevant to future planning
- legacy milestone #43
- any duplicate or conflicting historical Release 1.2 artifacts

Inspection is authorized. Mutation is not.

---

## 7. Definition Questions

The execution must answer, with repository-backed reasoning:

1. **What capability should Release 1.2 deliver?**
2. **Why is it the correct next increment after Release 1.1?**
3. **What is already implemented and therefore must not be re-scoped into 1.2?**
4. **What capabilities remain genuinely absent?**
5. **What is the smallest coherent vertical slice that advances the platform without prematurely entering later roadmap phases?**
6. **What architectural layers/modules are expected to participate?**
7. **What existing contracts and invariants must be preserved?**
8. **What new contracts or abstractions are likely required?**
9. **What testing/validation responsibilities belong to the release?**
10. **What documentation alignment is required?**
11. **What must explicitly remain out of scope?**
12. **What should the authoritative Release 1.2 name be?**
13. **How many work packages are justified by the dependency structure?**
14. **What should each work package own at definition level?**
15. **What is the exact dependency graph between those work packages?**
16. **What is the closure gate for Release 1.2?**

Do not force Release 1.2 into the Release 1.1 count of 16 work packages. Derive the count from scope and dependency truth.

---

## 8. Required Reconciliation

Explicitly reconcile the new definition against the completed Release 1.1 platform, including at minimum:

- persisted historical market observations
- SQLite physical model and schema ownership
- connection/bootstrap boundary
- persistence and retrieval semantics
- failure mapping
- DI/configuration
- bounded Worker acquisition → persistence execution
- current permanent test baseline
- current architecture boundaries

The Release 1.2 definition must build on these capabilities rather than recreate them.

---

## 9. Release Identity Design

Propose exactly one authoritative identity in the form:

`Phase 3 - Release 1.2: <Capability Name>`

The name must describe the actual coherent capability increment, not merely a technology or generic noun.

The legacy title `Phase 3 - Release 1.2: Storage` is not automatically authoritative.

Provide:

- proposed title
- one-sentence release objective
- concise rationale
- measurable release outcome
- explicit non-goals

---

## 10. Work-Package Model

Design a complete work-package model at **definition granularity**.

For every proposed WP provide:

- WP number
- title
- purpose
- primary layer/area
- exact predecessor dependencies
- principal deliverable
- principal validation responsibility
- explicit boundary with adjacent WPs

The graph must be intentional:

- no missing required dependency
- no artificial serial dependency
- no hidden lifecycle WP
- no duplicate ownership
- no implementation detail beyond what is necessary to define scope

Include a final validation/integration/acceptance WP when justified by the established release governance model.

Do **not** create GitHub issues.

---

## 11. Closure-Gate Definition

Define the Release 1.2 closure gate at planning level.

It must state the evidence expected before the release can later be declared complete, including as applicable:

- functional acceptance
- architecture acceptance
- permanent tests
- canonical verification
- documentation alignment
- security/offline determinism
- clean integration
- fresh-checkout reproducibility
- GitHub lifecycle completion
- protection of the next release

Do not execute the closure gate now.

---

## 12. GitHub Planning Readiness

Assess whether the resulting definition is sufficiently precise for a later GitHub-planning authority.

Report, without mutation:

- whether a new authoritative Release 1.2 milestone will be required
- whether Project #2 already has `Release = 1.2`
- whether required Area/Priority/Status values appear available
- whether Release 1.2 issues already exist
- whether duplicates/conflicts exist
- whether legacy milestone #43 requires any future handling

Do not add Project options.
Do not create or modify milestones.
Do not create or modify issues.
Do not change Project items.

If future GitHub planning would require a narrow prerequisite mutation, identify it only.

---

## 13. Repository Mutation Prohibition

This authority is inspection and definition only.

Do not:

- create or edit repository files
- stage files
- commit
- create branches
- push
- create PRs
- merge
- tag
- create GitHub Releases
- change packages
- change project references
- change source code
- change tests
- change documentation
- change scripts
- start implementation

The final working tree must remain semantically identical to the starting repository state.

---

## 14. GitHub Mutation Prohibition

Do not mutate:

- milestones
- issues
- labels
- assignees
- Project #2 items
- Project fields/options/schema
- branches
- PRs
- releases

Release 1.2 must remain **inactive** after this definition run.

---

## 15. Out-of-Band Authority Handling

This authority and its five-line chat bootstrap are **out-of-band execution authorities**.

They must not become repository candidate content during this run.

If execution causes untracked copies of these two authority files to appear inside the repository, they may be removed only to restore the pre-execution repository state, provided they were not present as accepted tracked content beforehand.

Do not stage or commit them.

---

## 16. Required Validation Before Decision

Before emitting success:

- verify Release 1.1 remains closed
- verify milestone #52 remains closed
- verify issues #103–#118 remain Closed/Done
- verify legacy milestone #43 remains closed and empty
- verify active Release 1.2 planning remains zero
- verify no repository mutation occurred
- verify no GitHub mutation occurred
- verify no Release 1.2 implementation started
- verify the proposed scope does not duplicate Release 1.1
- verify the WP graph is complete and internally consistent
- verify every proposed WP maps to the proposed Release 1.2 objective
- verify the definition is sufficient to design the execution plan and file manifest next

---

## 17. Required Execution Report

Produce a detailed **Release 1.2 Planning / Definition Execution Report** containing at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository / GitHub Context
4. Release 1.1 Closure Gate
5. Current Platform Capability Baseline
6. Current Roadmap Evidence
7. Legacy Release 1.2 Reconciliation
8. Gap Analysis
9. Candidate Release 1.2 Options Considered
10. Selected Release 1.2 Identity
11. Release Objective
12. Release Outcomes
13. In Scope
14. Explicitly Out of Scope
15. Architecture / Layer Impact
16. Contract / Data / Runtime Impact
17. Work-Package Count Rationale
18. Work-Package Definition Table
19. Dependency Graph
20. Validation / Acceptance Model
21. Documentation Responsibilities
22. GitHub Planning Readiness
23. Project #2 Readiness
24. Legacy Milestone #43 Future Treatment
25. Release 1.3 Protection
26. Repository Mutation Check
27. GitHub Mutation Check
28. Findings / Observations
29. Definition Acceptance Matrix
30. Final Decision
31. Next Authorized Action

The report must clearly distinguish observed repository truth from proposed Release 1.2 definition.

---

## 18. Success Criteria

Success requires all of the following:

- Release 1.1 predecessor closure verified
- current platform baseline reconciled
- roadmap evidence reconciled
- legacy #43 treated only as historical evidence
- exactly one Release 1.2 identity selected
- coherent objective and measurable outcome defined
- in-scope and out-of-scope boundaries explicit
- work-package model complete
- dependency graph explicit and drift-free by design
- closure gate defined
- GitHub planning readiness assessed
- Release 1.3 protected
- repository mutations = 0
- GitHub mutations = 0
- Release 1.2 active planning remains 0
- Release 1.2 implementation remains not started
- next step limited to execution-plan/file-manifest design

On success end exactly with:

`RELEASE 1.2 DEFINITION COMPLETE`

Then state:

`NEXT AUTHORIZED ACTION: Design RELEASE_1.2_EXECUTION_PLAN.md and RELEASE_1.2_FILE_MANIFEST.md from this accepted definition. Do not activate GitHub planning or begin WP01.`

---

## 19. Blocked Behavior

Stop with `RELEASE 1.2 DEFINITION BLOCKED` if any condition prevents a truthful definition, including:

- Release 1.1 closure drift
- active Release 1.2 planning unexpectedly exists
- repository truth materially contradicts roadmap evidence and cannot be reconciled without mutation
- the next coherent capability cannot be determined from available authority
- duplicate/conflicting GitHub state prevents a stable definition
- required evidence is unavailable
- completing the task would require repository or GitHub mutation

For a blocker, report:

- exact blocker
- observed evidence
- why this authority cannot resolve it
- smallest corrective authority required

Do not partially activate Release 1.2.

---

## 20. Scope Protection

This authority ends at **definition**.

It does not authorize:

- creation of Release 1.2 governance files
- GitHub planning
- milestone creation
- issue creation
- Project mutation
- governance integration
- WP01
- production implementation
- Release 1.3 planning

The accepted next step is only the separately authorized design of the Release 1.2 execution plan and file manifest.

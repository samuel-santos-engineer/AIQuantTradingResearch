# Release 1.10 — Definition / Planning Authority

## Model assignment

This authority always defines all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY for release definition, contract/policy/architecture reconciliation, scope boundaries, acceptance criteria, governance, and read-only planning.
- **GPT-5.6 Terra** — RESERVED for later implementation, validation execution, Git, GitHub, merge, and publication mutation authorities after definition acceptance.
- **GPT-5.6 Sol** — RESERVED for supporting analysis, synthesis, exploratory reasoning, and non-authoritative review. Sol does not replace Luna or Terra for authorities assigned to them.

**Selected execution model for this authority: GPT-5.6 Luna.**

## Purpose

Define the canonical planning contract for **Release 1.10 / milestone #59**.

This is planning-only. Inspect current repository/GitHub state after completed Release 1.9, reconcile milestone #59 against roadmap and architecture, and define Release 1.10 before any implementation authority.

## Predecessor baseline

Known post-PR-#241 `main`: `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`.

Verify current `origin/main`. If it legitimately advanced, record and use the current SHA.

Preserve:
- `v1.9.0` -> `e4958721c9a581efbb2552134c00bc146c73f047`
- published Release 1.9
- milestone #58 Closed 0/13
- milestone #59 Open
- #233–#237 Closed/Done
- PR #240 Merged
- PR #241 Merged

Release 1.9 is closed. Do not redefine or mutate it.

## Mutation boundary

### Repository
No implementation mutations. If repository convention permits planning documents to be written, only Release 1.10 planning artifacts under `docs/roadmap/release-1.10/` may be created/updated.

Forbidden: `src/`, `tests/`, Python application/runtime code, packages, schema/migrations, CI/workflows, build/runtime config, generated artifacts, or Release 1.9 historical rewrites.

### Git
ZERO mutations: no branch creation/switch for implementation, staging, commit, amend, rebase, merge, push, tag, branch deletion.

### GitHub
ZERO mutations: no milestone/issue/Project/PR/Release/tag writes.

## Phase 0 — Entry audit

Record current branch, local HEAD, `origin/main`, ahead/behind, `git status --short`, staged/untracked paths, and root structure. Preserve unrelated user work.

Emit:

`RELEASE 1.10 DEFINITION ENTRY BASELINE: READ-ONLY`

## Phase 1 — Canonical evidence

Read authoritative sources, including as applicable:
- `README.md`
- roadmap/index and release sequence
- Release 1.8 definition/acceptance artifacts
- Release 1.9 definition/acceptance artifacts
- architecture/ADR documentation
- testing/security/development conventions
- prior authority conventions where governance consistency matters.

Distinguish canonical sources from historical/supporting artifacts.

## Phase 2 — Milestone #59

Read milestone #59 authoritatively. Record title, state, description, due date, issue counts, associated issues, labels, issue bodies, dependencies/references, and Project status/Release taxonomy where available.

If #59 is an empty placeholder, state that. Do not populate it.

Classify existing items as canonical scope candidate, supporting planning, stale/conflicting, implementation-that-must-wait, or unrelated.

## Phase 3 — Sequence/taxonomy reconciliation

Reconcile:
- 1.8 completed
- 1.9 completed
- 1.10 current accepted/planned milestone
- subsequent milestones if defined.

Verify README/roadmap consistency with #59. Inspect Project Release taxonomy and whether 1.10 exists. Do not mutate taxonomy.

## Phase 4 — Technical baseline

Verify current repository facts:
- .NET/runtime version
- solution/project structure
- Python version policy
- exact dependency policy/pins
- Streamlit version if relevant
- SQLite/schema version
- JSON/stdio and file-handoff boundaries
- provider/data boundaries
- test/architecture/integration/lifecycle organization
- security conventions
- Windows/local constraints
- UI/presentation architecture
- Release 1.9 visualization foundation.

Only report test counts if current evidence verifies them.

Separate inherited permanent architecture, 1.9-specific implementation, reusable 1.10 foundation, and prohibited bypasses.

## Phase 5 — Capability intent

Derive Release 1.10 from canonical evidence, not assumptions.

Produce:

> Release 1.10 adds ______ while preserving ______ and explicitly not adding ______.

Cover user-visible, domain/application, infrastructure, presentation, data/provider, and operational/lifecycle implications.

If evidence is insufficient to uniquely determine scope, do not invent it. Emit:

`INSUFFICIENT CANONICAL SCOPE EVIDENCE`

and identify the smallest unresolved decision.

## Phase 6 — In/out scope

Define evidence-backed in-scope items with rationale, architectural owner/layer, and observable acceptance outcome.

Define explicit non-scope to prevent creep, considering:
- real broker/provider connectivity
- live trading/execution
- portfolio/order management
- generic RPC expansion
- schema redesign
- auth/cloud deployment
- unrelated UI redesign
- dependency modernization
- future milestone capabilities.

Reconcile rather than exclude anything canonical evidence explicitly requires.

## Phase 7 — Architecture contract

For every capability identify producer, domain/application owner, persistence boundary, transport/handoff, consumer, failure ownership, and lifecycle ownership.

Resolve:
- whether presentation may read SQLite
- whether Python may call providers
- whether .NET remains canonical business owner
- reuse/extension of JSON handoff
- schema evolution
- historical/replay/simulated/real-time/live provenance
- startup/restart/readiness ownership
- Empty/Failed/degraded behavior.

Explicitly prohibit architectural bypasses.

## Phase 8 — Data/truthfulness contract

Classify provenance as historical, deterministic replay, simulated live, provider-backed delayed, provider-backed real-time, or broker/exchange live.

UI/docs claims must not exceed actual provenance. Preserve simulation/replay disclosure where applicable.

## Phase 9 — Schema/protocol/dependency impact

Classify each `NO CHANGE EXPECTED`, `CHANGE REQUIRED`, or `DECISION REQUIRED`:
- SQLite schema
- JSON schema/read model
- JSON-over-stdio
- file handoff
- Python dependencies
- .NET dependencies
- Streamlit
- configuration
- persisted state.

For required changes specify rationale, compatibility/versioning expectation, and owning work package. Do not implement.

## Phase 10 — Work packages

Design narrow, independently verifiable Release 1.10 work packages following repository convention.

Each must define identifier, title, objective, scope, non-scope, dependencies, expected repository areas, acceptance criteria, validation, security considerations, and model/authority type.

Do not create GitHub issues.

## Phase 11 — GPT-5.6 authority map

Every future authority/work package MUST visibly define all three:

### GPT-5.6 Luna
Contract, definition, architecture/policy, schema/protocol decisions, acceptance reconciliation, read-only audits.

### GPT-5.6 Terra
Implementation, tests, execution/validation, approved package/schema changes, Git/GitHub mutations, merges/publication.

### GPT-5.6 Sol
Supporting technical analysis, alternatives, synthesis, exploratory/non-authoritative review.

Every authority artifact must contain `## Model assignment`, define Luna/Terra/Sol, and name its selected execution model.

## Phase 12 — Acceptance matrix

Define measurable criteria for domain, application, infrastructure, presentation, architecture/no-bypass, lifecycle/restart, failures, provenance/truthfulness, compatibility, security, tests, residue/process cleanup, and documentation.

Separate mandatory release gates, work-package-local gates, and informational observations.

## Phase 13 — Validation strategy

Define later validation for build, .NET tests, Python tests, architecture tests, integration, lifecycle/restart, security scan, dependency health, schema/protocol compatibility, residue/process/listener checks, and UI/manual showcase if applicable.

Do not freeze stale historical counts.

## Phase 14 — Risk register

For each concrete risk provide trigger, impact, mitigation, owning work package, and release-blocking status. Include architecture drift, scope creep, provenance/truthfulness, lifecycle ownership, compatibility, test brittleness, Windows/local execution, and security where relevant.

## Phase 15 — Planning artifacts

Determine canonical artifacts under `docs/roadmap/release-1.10/`.

Prefer repository convention and minimal proliferation. At minimum consider:
1. `RELEASE_1.10_DEFINITION.md`
2. work-package plan/index
3. acceptance/validation plan if conventionally separate
4. dependency/sequence plan if conventionally separate.

If writing is permitted, only these planning artifacts may change. Never stage/commit them.

## Phase 16 — Next authority

Choose exactly one:

A. If definition accepted but GitHub planning objects are absent:
**Release 1.10 GitHub planning materialization authority — GPT-5.6 Terra**

B. If GitHub planning objects already exist and reconcile:
**Release 1.10 WP01 implementation authority — normally GPT-5.6 Terra**

C. If scope evidence is insufficient:
**Release 1.10 scope decision/reconciliation authority — GPT-5.6 Luna**

No implementation proceeds under C.

## Required definition output

Produce:
1. Release identity
2. predecessor boundary
3. canonical capability statement
4. evidence sources
5. in-scope
6. out-of-scope
7. architecture contract
8. data provenance/truthfulness contract
9. schema/protocol/dependency impact
10. work-package sequence
11. Luna/Terra/Sol authority map
12. acceptance matrix
13. validation strategy
14. risk register
15. planning artifact inventory
16. GitHub planning-state assessment
17. exact next authority.

## Mutation accounting

Final report must state:

`RELEASE 1.10 DEFINITION/PLANNING REPOSITORY MUTATIONS: ZERO OR PLANNING-DOCUMENTS ONLY`

`RELEASE 1.10 DEFINITION/PLANNING GIT MUTATIONS: ZERO`

`RELEASE 1.10 DEFINITION/PLANNING GITHUB MUTATIONS: ZERO`

If planning docs were written, enumerate every changed path.

## Success

PASS only when predecessor state is preserved, #59 is authoritatively read, scope is evidence-backed, boundaries/contracts/impacts/work packages/model map/acceptance/validation/risks are defined, no implementation/Git/GitHub mutation occurs, and exact next authority is identified.

Emit:

`RELEASE 1.10 DEFINITION: ACCEPTED PLANNING BASELINE`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Terminal:

`RELEASE 1.10 DEFINITION / PLANNING AUTHORITY COMPLETE`

## Blocked

If canonical evidence cannot uniquely establish scope, report evidence, competing interpretations, unresolved decision, mutation accounting, and smallest next Luna decision authority.

Emit:

`RELEASE 1.10 DEFINITION: BLOCKED — INSUFFICIENT CANONICAL SCOPE EVIDENCE`

Terminal:

`RELEASE 1.10 DEFINITION / PLANNING AUTHORITY BLOCKED`

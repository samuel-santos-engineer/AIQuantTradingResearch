# Release 1.10 — Work-Package Contract / Manifest Completion Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY for contract definition, architecture/policy reconciliation, scope allocation, acceptance criteria, governance, and read-only/planning authorities.
- **GPT-5.6 Terra** — RESERVED for implementation, validation execution, approved Git/GitHub mutations, merge, and publication after planning acceptance.
- **GPT-5.6 Sol** — RESERVED for supporting analysis, synthesis, alternatives, and non-authoritative review. Sol does not replace Luna or Terra for an authority assigned to them.

**Selected execution model: GPT-5.6 Luna.**

---

# Purpose

Complete the missing **per-work-package contracts** for the already accepted Release 1.10 planning baseline.

This authority exists because the prior:

**Release 1.10 GitHub Planning Materialization Authority — GPT-5.6 Terra**

correctly BLOCKED before mutation.

Its read-only reconciliation established that the accepted Release 1.10 artifacts agree on:

- release-level capability;
- architecture/provenance boundaries;
- eight WP names;
- eight-WP order;

but do not yet define enough deterministic per-WP detail to create GitHub issue bodies without invention.

This authority MUST fill that planning-contract gap.

It MUST NOT implement Release 1.10.
It MUST NOT materialize GitHub planning objects.

---

# Accepted release-level capability

Canonical Release 1.10 capability:

> Governed OpenTelemetry-based pipeline/boundary observability plus a truthful Streamlit System Health view.

Preserve:

- .NET pipeline/business ownership;
- canonical governed JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- established architecture/no-bypass boundaries.

Explicitly exclude unless already required by the accepted Release 1.10 artifacts:

- live providers;
- broker/exchange connectivity;
- trading/execution;
- ML;
- backtesting;
- parallel pipelines;
- direct Streamlit/UI SQLite access;
- direct Python provider access;
- schema migration;
- unselected telemetry dependencies;
- unrelated dependency modernization.

Do not redefine the accepted release capability.

---

# Canonical input artifacts

Read and reconcile:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Also read only the minimum supporting repository evidence needed to allocate contracts safely, including relevant:

- Release 1.9 architecture/contracts;
- current solution/project structure;
- test organization;
- security conventions;
- telemetry/observability-related existing code or boundaries;
- Streamlit presentation structure;
- roadmap/governance conventions.

The three Release 1.10 artifacts remain the primary planning baseline.

---

# Known GitHub reconciliation evidence

The blocked Terra materialization authority established:

- milestone #59: Open, 0 open / 0 closed;
- no assigned milestone #59 issues;
- Project #2 has `Release=1.10` option `e7cc58f9`;
- Project #2 planning Status `Backlog` option `44e8e3fc`;
- no existing semantic-equivalent Release 1.10 WP issues;
- Release 1.9 integrity unchanged.

This Luna authority does NOT need to mutate or materialize those objects.

If fresh read-only GitHub verification is available, it may verify them. Otherwise treat this evidence as the handoff state and focus on repository planning contracts.

---

# Mutation boundary

## Repository content

Planning-document mutations ONLY.

Authorized paths are limited to the existing Release 1.10 planning artifacts:

- `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Prefer modifying only the minimum artifact(s) needed.

A new Release 1.10 planning file may be created ONLY if existing repository convention clearly requires a separate canonical WP-contract manifest and doing so is materially cleaner than extending the existing execution plan/manifest.

If a new file is necessary, it MUST be under:

`docs/roadmap/release-1.10/`

and the existing three artifacts must reference it sufficiently to avoid competing sources of truth.

## Forbidden repository mutations

Do NOT modify:

- `src/`;
- `tests/`;
- runtime/application Python;
- packages/dependencies;
- schemas/migrations;
- build configuration;
- CI/workflows;
- runtime configuration;
- README;
- Release 1.9 artifacts;
- prompts unrelated to this authority.

## Git

ZERO mutations.

Do not stage, commit, branch, checkout/switch, merge, rebase, push, tag, or modify refs.

## GitHub

ZERO mutations.

Do not create/edit issues, milestones, Project items/fields, PRs, Releases, tags, or lifecycle state.

---

# Phase 0 — Entry-state audit

Record:

- current branch;
- local HEAD;
- known/current remote `main` if available without prohibited mutation;
- `git status --short`;
- staged paths;
- untracked paths;
- existing Release 1.10 planning-file state.

Preserve unrelated local work exactly.

Emit:

`RELEASE 1.10 WP CONTRACT COMPLETION ENTRY BASELINE: READ-ONLY`

---

# Phase 1 — Freeze the eight-WP identity

Extract the exact eight work packages from the accepted execution plan.

Record for each:

- WP identifier;
- exact title;
- accepted order;
- existing high-level objective/model assignment if present.

Require exactly eight.

Do NOT rename, split, merge, reorder, add, or remove WPs unless the existing artifacts themselves contain an objective contradiction that makes completion impossible.

Emit:

`RELEASE 1.10 WORK-PACKAGE IDENTITY: PASS — 8/8 FROZEN`

If the artifacts disagree on names/order/count:
BLOCK for reconciliation rather than silently choosing.

---

# Phase 2 — Contract-allocation rules

Allocate release-level requirements to WPs using evidence and architectural ownership, not arbitrary distribution.

Every allocation must satisfy:

1. **Single clear owner** for each implementation responsibility where possible.
2. **No hidden implementation** in validation/documentation WPs.
3. **No duplicated ownership** unless an explicit integration/verification WP intentionally revalidates earlier work.
4. **Dependency-before-consumer** ordering.
5. **Architecture boundaries preserved**.
6. **Data truthfulness preserved end-to-end**.
7. **Acceptance criteria test observable behavior**, not implementation trivia.
8. **Security requirements are proportional and specific**.
9. **Path ownership is bounded**, not a license for arbitrary repository changes.
10. **Later implementation authorities remain narrow and independently auditable**.

If a release-level requirement cannot be assigned to a WP from architectural evidence, mark it as an explicit planning decision and resolve it in this authority with rationale.

Do not invent new product capability.

---

# Phase 3 — Exact dependency topology

Define the complete dependency graph for all eight WPs.

The existing accepted linear sequence may imply ordering, but this authority must make dependency edges explicit.

For each WP state:

- direct predecessor dependencies;
- why each dependency is required;
- whether it is a hard implementation dependency or validation/integration dependency.

Requirements:

- graph must be acyclic;
- every dependency must reference one of the eight WPs or an accepted predecessor release boundary;
- do not use vague `previous WPs` wording;
- distinguish direct dependencies from transitive dependencies;
- preserve accepted ordering unless evidence justifies a narrower dependency graph.

Produce a deterministic adjacency list.

Emit:

`RELEASE 1.10 WP DEPENDENCY TOPOLOGY: PASS — ACYCLIC`

---

# Phase 4 — Complete each WP contract

For **each of the eight WPs**, define all fields below.

## Identity

- WP ID
- exact title
- selected execution model

## Objective

One precise outcome statement.

## In scope

Explicit implementation/planning responsibilities owned by this WP.

## Out of scope

Adjacent responsibilities this WP must not absorb.

## Architecture contract

Specify applicable:

- .NET ownership;
- domain/application ownership;
- infrastructure ownership;
- telemetry ownership;
- JSON handoff ownership;
- Python/presentation ownership;
- Worker/Streamlit independence;
- no-bypass constraints;
- lifecycle ownership;
- failure ownership.

## Data provenance / truthfulness contract

State what telemetry/health information may truthfully claim.

Preserve deterministic/replay/simulated financial-data provenance.

Observability must not be described as proof of live market connectivity.

## Dependencies

Exact direct WP dependencies.

## Expected repository areas / path ownership

Define exact existing directories/files when evidence supports them.

Where implementation will require new files, define the narrow allowed directory/pattern and intended responsibility rather than inventing final filenames unnecessarily.

Classify paths as:

- expected modify;
- expected add;
- validation-only/read-only;
- explicitly forbidden.

Path ownership MUST be specific enough for a later Terra authority to enforce a frozen scope boundary.

## Acceptance criteria

Define objective, measurable WP-local gates.

Use stable behavioral outcomes.

Each criterion should be independently checkable.

## Validation requirements

Specify the exact categories of later validation needed, such as:

- focused .NET tests;
- architecture tests;
- integration tests;
- Python tests;
- lifecycle/restart tests;
- manual Streamlit verification;
- build;
- security scan;
- dependency health;
- residue/process checks.

Where repository commands are already canonical, include them.

Do not invent test counts.

## Security requirements

Define relevant WP-specific security constraints, including as applicable:

- telemetry must not expose secrets;
- no credentials/tokens in spans/logs/attributes;
- bounded/safe attribute values;
- no uncontrolled high-cardinality sensitive data;
- no unsafe external exporter/configuration assumptions;
- no bypass of existing security scanning;
- local-only/development assumptions clearly distinguished from production claims.

Do not force irrelevant boilerplate into every WP; state `No additional WP-specific security requirement beyond release baseline` where appropriate.

## Completion boundary

State what this WP completion authorizes and what still requires a subsequent authority.

---

# Phase 5 — Cross-WP responsibility matrix

Create a deterministic responsibility matrix mapping release concerns to exactly one primary owning WP and optional validating WP(s).

At minimum map:

- observability contract/model;
- OpenTelemetry instrumentation boundary;
- pipeline-stage/boundary telemetry;
- health/read-model projection;
- JSON handoff impact;
- Python parsing/projection impact;
- Streamlit System Health presentation;
- failure/degraded-state semantics;
- lifecycle/restart behavior;
- architecture/no-bypass;
- integration validation;
- security validation;
- documentation/showcase if present in the accepted eight WPs.

Use the actual eight WP names from the plan.

No orphaned release requirement may remain.

No ambiguous dual primary ownership may remain.

Emit:

`RELEASE 1.10 WP RESPONSIBILITY MATRIX: PASS — NO ORPHANED REQUIREMENTS`

---

# Phase 6 — Schema/protocol/dependency decisions by WP

For each previously classified release-level impact, assign its owner WP.

Cover:

- SQLite schema v4;
- JSON schema/read model;
- JSON-over-stdio;
- file handoff;
- .NET telemetry dependencies;
- Python dependencies;
- Streamlit;
- configuration;
- persisted state.

Preserve accepted `NO CHANGE EXPECTED`, `CHANGE REQUIRED`, or `DECISION REQUIRED` classifications unless current evidence proves correction is necessary.

Any dependency selection that is not already accepted must remain a bounded decision for the appropriate WP or require Luna reconciliation before Terra implementation.

Do not select speculative packages just to make the manifest look complete.

---

# Phase 7 — Acceptance coverage audit

Build a release-to-WP acceptance coverage matrix.

Every mandatory Release 1.10 release gate must map to:

- implementing WP;
- validating WP;
- evidence expected at final release acceptance.

Audit at minimum:

- domain/application correctness;
- telemetry correctness;
- infrastructure behavior;
- presentation/System Health truthfulness;
- architecture/no-bypass;
- lifecycle/restart;
- Empty/Failed/degraded behavior;
- compatibility;
- provenance;
- security;
- tests;
- process/listener residue;
- documentation.

If a release-level criterion has no owner:
fix the planning contract before completion.

---

# Phase 8 — Model map completion

For every WP, explicitly define:

### GPT-5.6 Luna
Contract/planning/architecture/reconciliation role relevant to that WP.

### GPT-5.6 Terra
Implementation/execution/validation/Git/GitHub role relevant to that WP.

### GPT-5.6 Sol
Supporting analysis/synthesis/review role relevant to that WP.

Then state:

`Selected execution model: GPT-5.6 <Luna|Terra|Sol>`

Normally implementation WPs select Terra; contract/reconciliation-only WPs may select Luna.

Sol must not silently replace Luna/Terra authority ownership.

Every later WP authority must reproduce this visible model assignment.

---

# Phase 9 — Update canonical planning artifacts

Update the minimum accepted Release 1.10 planning artifacts so the GitHub materialization authority can create issue bodies without inventing any detail.

Preferred source-of-truth design:

- `RELEASE_1.10_DEFINITION.md` — release-level contract;
- `RELEASE_1.10_EXECUTION_PLAN.md` — exact eight per-WP contracts and dependency topology;
- `RELEASE_1.10_FILE_MANIFEST.md` — enforceable expected path ownership and exclusions.

Avoid duplicating large contract text unnecessarily.

Cross-reference canonical sections where appropriate.

After editing, reread all three and prove they agree on:

- eight WP identities/order;
- per-WP objectives;
- scope/non-scope;
- dependencies;
- path ownership;
- acceptance;
- validation;
- security;
- model assignments;
- release exclusions.

---

# Phase 10 — Materialization-readiness simulation

Without GitHub mutation, simulate what the Terra materialization authority would need to create each issue.

For each of eight WPs prove that the artifacts now provide, without inference:

- exact title;
- objective;
- in scope;
- non-scope;
- architecture contract;
- provenance/truthfulness contract;
- direct dependencies;
- expected repository/path ownership;
- acceptance criteria;
- validation requirements;
- security requirements;
- model assignment;
- completion boundary.

Classify each:

`READY FOR DETERMINISTIC ISSUE MATERIALIZATION`

Require 8/8.

Emit:

`RELEASE 1.10 WP CONTRACT MANIFEST: PASS — 8/8 MATERIALIZATION-READY`

If any field would still require Terra to invent planning detail:
BLOCK.

---

# Phase 11 — Scope/security sanity

Perform planning-level sanity only.

Verify:

- no source/test/runtime implementation changes;
- no dependency/package changes;
- no schema migration;
- no secrets introduced into planning docs;
- no credentials/endpoints invented;
- no live-provider/trading claims;
- no architecture bypass authorization;
- no GitHub lifecycle changes.

Do not run the full executable test suite unless repository policy explicitly requires it for documentation-only planning changes.

---

# Phase 12 — Mutation accounting

Final report MUST enumerate exact changed planning paths.

Expected:

`RELEASE 1.10 WP CONTRACT COMPLETION REPOSITORY MUTATIONS: PLANNING DOCUMENTS ONLY`

`RELEASE 1.10 WP CONTRACT COMPLETION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP CONTRACT COMPLETION GITHUB MUTATIONS: ZERO`

If any non-planning repository path changed:
FAIL/BLOCK.

---

# Phase 13 — Next authority

On success, resume the already-defined:

**Release 1.10 GitHub Planning Materialization Authority — GPT-5.6 Terra**

Do not redesign it unless the completed contracts expose a genuine contradiction in its authority.

The resumed Terra authority must reread the updated accepted planning artifacts and rerun its duplicate/idempotence gate before mutation.

---

# Required final report

Report:

## Model assignment
All Luna/Terra/Sol roles and selected Luna execution.

## Entry state
Branch/HEAD/status and preserved local residue.

## Frozen WP identity
Exactly eight IDs/titles/order.

## Dependency topology
Direct dependency adjacency list and acyclic result.

## Per-WP contract summary
For all eight:
- ID/title
- selected model
- objective
- direct dependencies
- path ownership summary
- acceptance gate count
- validation categories
- security summary
- materialization readiness

## Responsibility coverage
Confirm no orphaned release requirements.

## Planning artifacts
Exact changed paths.

## Materialization readiness

`RELEASE 1.10 WP CONTRACT MANIFEST: PASS — 8/8 MATERIALIZATION-READY`

## Mutation accounting

`RELEASE 1.10 WP CONTRACT COMPLETION REPOSITORY MUTATIONS: PLANNING DOCUMENTS ONLY`

`RELEASE 1.10 WP CONTRACT COMPLETION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP CONTRACT COMPLETION GITHUB MUTATIONS: ZERO`

## Next authority

`Release 1.10 GitHub Planning Materialization Authority — GPT-5.6 Terra`

---

# Success markers

`RELEASE 1.10 WORK-PACKAGE IDENTITY: PASS — 8/8 FROZEN`

`RELEASE 1.10 WP DEPENDENCY TOPOLOGY: PASS — ACYCLIC`

`RELEASE 1.10 WP RESPONSIBILITY MATRIX: PASS — NO ORPHANED REQUIREMENTS`

`RELEASE 1.10 WP CONTRACT MANIFEST: PASS — 8/8 MATERIALIZATION-READY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Terminal:

`RELEASE 1.10 WORK-PACKAGE CONTRACT / MANIFEST COMPLETION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- eight-WP identity/order is inconsistent;
- release-level accepted artifacts materially conflict;
- a per-WP responsibility cannot be allocated without inventing product capability;
- path ownership cannot be bounded from repository evidence;
- a mandatory acceptance gate remains ownerless;
- issue materialization would still require Terra to invent contract detail;
- a forbidden repository/Git/GitHub mutation occurs.

Report the exact unresolved contract decision and smallest next Luna reconciliation step.

Terminal:

`RELEASE 1.10 WORK-PACKAGE CONTRACT / MANIFEST COMPLETION AUTHORITY BLOCKED`

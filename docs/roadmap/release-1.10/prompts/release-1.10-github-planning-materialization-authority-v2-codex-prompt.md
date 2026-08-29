# Release 1.10 — GitHub Planning Materialization Authority v2

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, architecture, scope, acceptance, governance, reconciliation, and read-only audit authority.
- **GPT-5.6 Terra** — implementation/execution authority, including explicitly approved GitHub planning mutations.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra for assigned authorities.

**Selected execution model: GPT-5.6 Terra.**

---

# Purpose

Resume the previously blocked Release 1.10 GitHub planning materialization after the missing per-WP contracts were completed by GPT-5.6 Luna.

Materialize the accepted Release 1.10 planning baseline into GitHub planning objects only.

No Release 1.10 implementation is authorized.

---

# Canonical planning sources

Read these current local artifacts in full before any mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

The completed execution plan and file manifest now contain deterministic per-WP contracts.

They are authoritative for:

- exact WP IDs/titles/order;
- objective;
- scope/non-scope;
- architecture/provenance constraints;
- direct dependencies;
- bounded path ownership;
- acceptance criteria;
- validation;
- security;
- Luna/Terra/Sol assignments;
- selected execution model;
- completion boundary.

Do not invent or redistribute planning detail.

If the three artifacts materially conflict, BLOCK before GitHub mutation.

---

# Frozen eight-WP identity

Expected exact order:

1. `WP01 — Observability Selection, Vocabulary & Scope`
2. `WP02 — Application Pipeline Observability Contract`
3. `WP03 — Infrastructure Provider, Persistence & Failure Instrumentation`
4. `WP04 — Worker/Interop Lifecycle and Exporter Isolation`
5. `WP05 — System Health Read Model and Streamlit Presentation`
6. `WP06 — Permanent Observability and No-Bypass Tests`
7. `WP07 — Documentation, Developer Setup & Operational Runbook`
8. `WP08 — Full Validation, Acceptance & PR Readiness`

Expected direct dependency chain:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

Re-derive and verify these from the current planning artifacts.

Do not use this prompt as a substitute if the artifacts differ.

---

# Release capability boundary

Canonical capability:

> Governed OpenTelemetry-based pipeline/boundary observability plus a truthful Streamlit System Health view.

Preserve:

- .NET pipeline/business ownership;
- canonical governed JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- architecture/no-bypass rules.

Exclude:

- live providers;
- broker/exchange connectivity;
- trading/execution;
- ML;
- backtesting;
- parallel pipelines;
- direct Streamlit/UI SQLite access;
- direct Python provider access;
- unauthorized schema migration;
- unselected telemetry dependencies;
- unrelated modernization.

---

# Known pre-materialization GitHub baseline

The prior blocked read-only authority found:

- milestone #59: Open, 0 open / 0 closed;
- no milestone #59 issues;
- Project #2;
- Project Release option `1.10` = `e7cc58f9`;
- Project planning Status `Backlog` = `44e8e3fc`;
- no existing semantic-equivalent Release 1.10 WP issues;
- Release 1.9 integrity unchanged.

These are expectations only.

Re-read GitHub authoritatively before mutation.

If state changed, reconcile idempotently.

---

# Mutation boundary

## Repository

ZERO content mutations.

Do not edit planning docs, README, prompts, source, tests, packages, schema, configuration, or runtime files.

## Git

ZERO mutations.

Do not fetch if fetch is classified as prohibited mutation in the execution environment.

Do not checkout/switch, branch, stage, commit, amend, merge, rebase, push, tag, delete refs, or alter the index/worktree.

## GitHub

Authorized mutations are limited to the minimum deterministic Release 1.10 planning materialization:

1. update milestone #59 description only if required by the accepted definition;
2. create missing canonical WP01–WP08 issues;
3. reconcile exact pre-existing canonical WP issues only when safely idempotent;
4. assign all eight canonical WPs to milestone #59;
5. add each exactly once to canonical Project #2;
6. set Project `Release=1.10`;
7. set Project planning `Status=Backlog`;
8. encode accepted direct dependencies using established repository convention, or issue-body dependency references if no separate mechanism is established.

Forbidden:

- implementation;
- issue closure;
- milestone closure;
- PR creation;
- merges;
- tags/Releases;
- Project field/taxonomy creation;
- Release 1.9 lifecycle mutation.

---

# Phase 0 — Entry audit

Read and record:

- repository identity;
- default branch;
- authoritative remote `main` SHA;
- milestone #59 title/state/description/counts;
- all #59 issues;
- Project #2 identity;
- Project fields/options;
- `Release=1.10` option;
- `Status=Backlog` option;
- all existing Release 1.10 Project items;
- exact-title and semantic-equivalent WP issues.

Emit:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION ENTRY AUDIT: COMPLETE`

No mutation before Phase 0 completes.

---

# Phase 1 — Planning-contract readiness gate

Read all three canonical planning artifacts.

For each WP verify the artifacts provide without inference:

- exact title;
- objective;
- in scope;
- non-scope;
- architecture contract;
- provenance/truthfulness contract;
- direct dependencies;
- bounded path ownership;
- acceptance criteria;
- validation requirements;
- security requirements;
- model assignment;
- selected execution model;
- completion boundary.

Require 8/8.

Emit:

`RELEASE 1.10 WP CONTRACT MANIFEST: PASS — 8/8 MATERIALIZATION-READY`

If Terra would need to invent any field:
BLOCK.

---

# Phase 2 — Freeze materialization manifest

Print a deterministic pre-mutation manifest for WP01–WP08 containing:

- WP ID;
- exact issue title;
- selected execution model;
- direct dependencies;
- objective summary;
- path ownership summary;
- acceptance gate count;
- validation categories;
- security summary.

Require exactly eight and exact accepted order.

Emit:

`RELEASE 1.10 WORK-PACKAGE MANIFEST: 8/8 EXTRACTED FROM ACCEPTED PLAN`

---

# Phase 3 — Duplicate/idempotence gate

For each WP search by:

- exact title;
- WP identifier;
- Release 1.10;
- semantic equivalent.

Classify:

- `ABSENT — CREATE`
- `EXACT EXISTING — REUSE/RECONCILE`
- `CONFLICTING EXISTING — BLOCK`

An existing issue is exact only if identity and purpose clearly match the accepted WP.

Do not create duplicates.

Also inspect Project #2 for duplicate items.

Before mutation report:

- number of issue creates;
- number of issue reconciliations;
- milestone assignments;
- Project additions;
- Release field updates;
- Status field updates;
- dependency updates;
- milestone description update count.

---

# Phase 4 — Milestone #59 contract

Keep milestone #59 Open.

Preserve its title if already canonical.

Update description only if needed to faithfully summarize the accepted definition.

The description must not exceed the accepted scope or imply:

- live market/provider connectivity;
- trading;
- schema migration;
- architecture bypass.

Do not close #59.

---

# Phase 5 — Deterministic issue body

For every canonical WP, construct the body directly from its accepted contract.

Required sections:

## Release
`Release 1.10`

## Work package
WP ID and exact title.

## Model assignment
Define:
- GPT-5.6 Luna — contract/planning/reconciliation;
- GPT-5.6 Terra — implementation/execution/mutations;
- GPT-5.6 Sol — supporting analysis/review;
- selected execution model for this WP.

## Objective
Accepted WP objective.

## In scope
Accepted WP-specific scope.

## Out of scope
Accepted WP-specific non-scope.

## Architecture contract
Accepted WP-specific boundaries/ownership.

## Data provenance / truthfulness
Accepted provenance and claim limitations.

## Dependencies
Exact direct dependencies only.

For WP01, use the accepted predecessor/foundation wording from the plan.

## Expected repository areas / path ownership
Preserve expected-modify, expected-add, validation-only/read-only, and forbidden classifications as defined.

This is future implementation scope guidance, not current mutation authorization.

## Acceptance criteria
All accepted measurable WP-local criteria.

## Validation
Accepted validation categories/commands.

## Security
Accepted WP-specific security requirements.

## Completion boundary
Accepted completion boundary, explicitly requiring a separate execution authority for subsequent work.

No speculative implementation detail.

---

# Phase 6 — Materialize issues

Create only WPs classified `ABSENT — CREATE`.

For `EXACT EXISTING`, reconcile only missing/incorrect accepted planning metadata.

Every final canonical WP issue must be:

- Open;
- assigned to milestone #59;
- body-equivalent to accepted contract;
- uniquely identifiable.

Never close/reopen issues unless a separate authority explicitly permits lifecycle correction.

If an exact existing WP is unexpectedly Closed:
BLOCK rather than reopen it.

---

# Phase 7 — Project #2 materialization

For each of eight WP issues:

- ensure exactly one Project #2 item;
- add if absent;
- set `Release=1.10` using existing canonical option;
- set `Status=Backlog` using existing planning option.

Expected option IDs from prior read:

- Release 1.10: `e7cc58f9`
- Backlog: `44e8e3fc`

Re-read option IDs before mutation; do not blindly trust stale IDs.

Do not create fields/options.

Do not change unrelated Project fields.

---

# Phase 8 — Dependency topology

Materialize/document exact direct chain:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

Interpret arrow as predecessor dependency/order according to the accepted execution plan.

Use the repository's established dependency mechanism if one exists and the accepted artifacts authorize it.

Otherwise the deterministic `Dependencies` section in each issue body is sufficient.

Verify:

- exact direct edges;
- no cycles;
- no orphan references;
- no invented cross-edge.

Emit:

`RELEASE 1.10 WORK-PACKAGE DEPENDENCY TOPOLOGY: PASS`

---

# Phase 9 — Authoritative post-mutation read-back

Read all mutated GitHub objects again.

## Milestone #59

Require:

- Open;
- 8 open / 0 closed;
- exactly the eight canonical Release 1.10 WP issues assigned.

## Issues

For each WP verify:

- issue number;
- exact title;
- Open;
- milestone #59;
- body sections complete;
- selected model correct;
- direct dependencies correct;
- no planning-contract invention.

## Project #2

For each verify:

- exactly one Project item;
- `Release=1.10`;
- `Status=Backlog`;
- no duplicate;
- no unintended field mutation.

Emit:

`RELEASE 1.10 WORK-PACKAGE MANIFEST: PASS — 8/8 MATERIALIZED`

`RELEASE 1.10 MILESTONE #59 MATERIALIZATION: PASS — 8 OPEN / 0 CLOSED`

`RELEASE 1.10 PROJECT MATERIALIZATION: PASS — 8/8 ITEMS`

---

# Phase 10 — Release 1.9 integrity

Read back and verify unchanged:

- `v1.9.0` targets `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release is published, non-draft, non-prerelease;
- milestone #58 Closed 0/13;
- #233–#237 Closed/Done;
- PR #240 Merged;
- PR #241 Merged.

No Release 1.9 mutation is authorized.

---

# Phase 11 — Repository/Git preservation

Verify:

- repository content unchanged by this authority;
- pre-existing local planning edits/residue preserved exactly;
- index unchanged;
- no Git mutation.

Do not clean or stage the planning files.

---

# Phase 12 — Mutation accounting

Enumerate every GitHub mutation by object and action.

Report exact counts for:

- milestone updates;
- issues created;
- issues reconciled;
- milestone assignments;
- Project items added;
- Release field updates;
- Status updates;
- dependency metadata updates.

Required:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION GITHUB MUTATIONS: ACCEPTED PLANNING OBJECTS ONLY`

No undeclared mutation is acceptable.

---

# Phase 13 — Next authority

Identify WP01 as the first execution boundary:

`WP01 — Observability Selection, Vocabulary & Scope`

Read its selected execution model from the accepted plan.

The next authority must be named exactly in the final report using that selected model, normally:

**Release 1.10 WP01 — Observability Selection, Vocabulary & Scope Authority — GPT-5.6 <selected model>**

Do not execute WP01 here.

That authority must again explicitly define Luna/Terra/Sol roles.

---

# Success criteria

PASS only if:

- current planning artifacts are 8/8 materialization-ready;
- duplicate/idempotence gate passes;
- exactly eight canonical WP issues exist;
- all eight are Open and in milestone #59;
- milestone #59 is Open 8/0;
- issue contracts derive deterministically from accepted planning;
- Project #2 contains each exactly once;
- Release=1.10 and Status=Backlog are correct;
- dependency topology matches accepted chain;
- Release 1.9 is unchanged;
- repository mutations = zero;
- Git mutations = zero;
- GitHub mutations are only authorized planning objects;
- exact WP01 next authority is identified.

Success markers:

`RELEASE 1.10 WP CONTRACT MANIFEST: PASS — 8/8 MATERIALIZATION-READY`

`RELEASE 1.10 WORK-PACKAGE MANIFEST: PASS — 8/8 MATERIALIZED`

`RELEASE 1.10 MILESTONE #59 MATERIALIZATION: PASS — 8 OPEN / 0 CLOSED`

`RELEASE 1.10 PROJECT MATERIALIZATION: PASS — 8/8 ITEMS`

`RELEASE 1.10 WORK-PACKAGE DEPENDENCY TOPOLOGY: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 GITHUB PLANNING BASELINE: MATERIALIZED AND ACCEPTED`

Terminal:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- canonical planning artifacts conflict;
- any WP is not deterministically materialization-ready;
- WP identity/order differs unexpectedly;
- duplicate/conflicting issues exist;
- Project Release=1.10 or Backlog options are absent;
- an existing canonical WP has incompatible lifecycle state;
- dependency topology cannot be represented without invention;
- any mutation would exceed authority.

Report exact completed mutations before stopping.

Terminal:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION AUTHORITY BLOCKED`

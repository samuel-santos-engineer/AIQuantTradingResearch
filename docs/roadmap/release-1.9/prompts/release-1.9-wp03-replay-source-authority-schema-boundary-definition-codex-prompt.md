# Release 1.9 — WP03 Replay Source-Authority / Schema Boundary Definition — Codex Authority

## Authority

This document grants a **narrow normative boundary-definition authority** for Release 1.9 WP03, canonical GitHub issue **#228**.

WP03 remains blocked because the real production Replay path reaches persistence/materialization semantics where the current SQLite schema v3 enforces:

`source_authority = 0`

while WP03 replay materialization uses a distinct replay source authority.

The latest production-flow evidence attempt proved:

- actual Worker entry point was invoked in-process;
- production configuration and DI were used;
- execution returned exit code `1`;
- the failure occurs because replay provenance is incompatible with the current persisted `source_authority` constraint;
- labeling replay as historical would violate the fixed architecture;
- changing schema was outside the prior authority;
- no production code or GitHub lifecycle mutation occurred;
- #228 remains Open / Backlog;
- WP04 has not started.

This authority exists only to define the correct governed boundary between replay provenance and the current schema/persistence model.

It does **not** authorize implementation.

It does **not** authorize a schema migration.

It does **not** authorize changing replay authority to historical.

It does **not** authorize WP04.

---

# Objective

Determine exactly how Release 1.9 should represent replay-produced materialization provenance while preserving:

- truthful replay identity/source authority;
- existing historical semantics;
- canonical five-stage pipeline behavior;
- schema integrity;
- persistence/catalog correctness;
- completed WP02 replay semantics;
- WP03's fixed Worker and Dataset-boundary contracts.

The output must choose one unambiguous governed boundary and specify what later implementation authority would be required.

---

# Fixed Architectural Invariants

These are already settled and must not be violated:

## Historical path

`Worker Historical/default`
→ historical acquisition
→ `IHistoricalObservationStore`
→ canonical WP03 materialization
→ `ExecuteCanonical`
→ stages 1–5

## Replay path

`Worker Replay`
→ WP02 replay source
→ WP03 explicit-observation seam
→ `ExecuteCanonical`
→ stages 1–5

Replay must not masquerade as historical acquisition.

## Provenance

Replay-produced data must not be labeled as historical solely to satisfy schema constraints.

## One pipeline

Historical and Replay continue to share the same canonical five-stage executor.

Do not reopen this decision.

---

# Known Schema Boundary

Current schema version:

`SQLite schema v3`

Observed constraint:

`source_authority = 0`

This constraint is compatible with historical authority but rejects the distinct replay source authority used by WP03 Replay materialization.

This authority must determine whether that constraint is:

- intentionally historical-only and therefore Replay should not persist through that path;
- incomplete for Release 1.9 and therefore requires a minimal schema evolution;
- or part of a broader provenance representation that already has another governed path.

Do not assume which is correct before inspecting repository evidence.

---

# Permitted Scope

This authority may read:

- #228;
- Release 1.9 WP03 definition/manifest;
- schema v3 DDL/migrations;
- persistence repositories;
- catalog/materialization persistence flow;
- `source_authority` enum/value definitions;
- historical provenance semantics;
- WP02 replay provenance semantics;
- WP03 materialization/result persistence behavior;
- tests governing source authority;
- migration/versioning conventions;
- release-governance constraints around schema changes.

It may define one normative boundary contract.

If repository governance permits a WP03-owned definition artifact, create only that artifact.

Otherwise return the full normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify schema files;
- create a migration;
- change `PRAGMA user_version`;
- change production code;
- change tests;
- change `source_authority` values in runtime code;
- map replay to historical authority;
- change Worker configuration;
- change Dataset-boundary semantics;
- change WP02 replay contracts;
- alter packages/Python/Streamlit/protocol;
- modify GitHub lifecycle;
- close #228;
- alter Release 1.9 planning;
- start WP04.

This is definition-only authority.

---

# Core Decision

The authority must choose exactly one governed model:

## Model A — Replay is non-persistent at this boundary

Replay traverses the canonical pipeline but does not write through the historical/materialization persistence path constrained by schema v3.

Use only if #228 and repository semantics support Replay as ephemeral/non-catalog-persistent output.

## Model B — Minimal schema evolution for replay provenance

Schema changes minimally to represent replay source authority truthfully.

Use only if #228 requires Replay materialization persistence/catalog participation and repository governance supports a Release 1.9 schema evolution.

## Model C — Existing alternate provenance/persistence path

Replay persists through an already-existing non-historical provenance mechanism without changing schema semantics.

Use only if repository evidence proves such a governed path exists and is appropriate.

## Model D — Another narrowly evidenced model

Allowed only if clearly supported by #228/repository semantics and narrower than a generalized provenance redesign.

Do not choose based on implementation convenience.

---

# Phase 0 — Read Provenance and Persistence Semantics

Before defining anything:

1. Read #228 fully.
2. Read the WP03 manifest/definition.
3. Read schema v3 DDL and migrations.
4. Locate the exact `source_authority` constraint.
5. Identify all valid current values and their meanings.
6. Read the runtime/domain enum/type corresponding to source authority.
7. Read historical persistence/materialization flow.
8. Read replay source-authority assignment.
9. Read catalog registration behavior.
10. Read tests asserting source authority or schema constraints.
11. Determine whether pipeline output persistence is mandatory for #228 acceptance.

Do not mutate anything.

---

# Phase 1 — Semantic Role of `source_authority`

Define exactly what `source_authority` means in the repository.

Answer:

- Is it observation acquisition provenance?
- Materialization provenance?
- Dataset provenance?
- Catalog provenance?
- Storage routing metadata?
- Validation-only metadata?

Determine whether value `0` semantically means:

- historical;
- default;
- only currently-supported authority;
- some other repository-specific meaning.

Do not infer from integer value alone.

---

# Phase 2 — Determine Replay Persistence Requirement

From #228 and repository behavior, determine whether Replay results are required to:

- persist to SQLite;
- register in dataset/materialization catalog;
- remain ephemeral;
- produce an output artifact without catalog persistence;
- reuse existing persistence conditionally.

This is the critical decision driver.

If #228 does not make persistence requirements clear, inspect Release 1.9 authority and predecessor behavior.

If still ambiguous, stop rather than inventing lifecycle semantics.

---

# Phase 3 — Evaluate Candidate Models

Evaluate at minimum:

## Candidate A — Ephemeral Replay materialization

Replay executes all five stages but bypasses persistence/catalog writes that are semantically historical-only.

Assess:
- #228 acceptance fit;
- compatibility with pipeline result expectations;
- whether bypassing persistence changes canonical pipeline semantics;
- whether Worker Replay completion remains meaningful;
- whether later WPs depend on persisted Replay output.

## Candidate B — Schema v4 minimal provenance expansion

Replay remains persistent and schema evolves minimally to permit distinct replay source authority.

Assess:
- exact DDL/migration delta;
- `user_version` impact;
- backward compatibility;
- migration testing burden;
- whether Release 1.9 authority permits schema evolution;
- whether existing reads/queries assume only authority 0.

## Candidate C — Existing alternate persistence path

Assess any repository-supported alternative that already preserves replay provenance without schema change.

## Candidate D — Other narrowly evidenced approach

Only if repository semantics support it.

For each candidate document:
- architectural fit;
- provenance truthfulness;
- persistence semantics;
- schema impact;
- code impact;
- test impact;
- release-governance impact;
- WP04+ implications.

---

# Phase 4 — Normative Decision Gate

Select one candidate only if it is clearly supported by:

- #228;
- Release 1.9 authority;
- existing architecture;
- provenance semantics;
- persistence requirements.

### Hard stop

If two materially different models remain equally valid, stop.

Do not make a schema-vs-ephemeral product decision silently.

Report the unresolved governance choice.

---

# Phase 5 — Define Exact Boundary Contract

For the selected model, specify:

## Replay source authority
- exact semantic identity;
- whether it is persisted;
- whether it appears in catalog/materialization records.

## Historical authority
- unchanged semantics.

## Persistence behavior
- exact Replay write/no-write rule.

## Catalog behavior
- exact Replay registration/no-registration rule.

## Schema behavior
- unchanged v3, or exact future migration requirement.

## Pipeline behavior
- confirm stages 1–5 remain unchanged.

## Worker behavior
- finite Replay completion semantics under this model.

## Error behavior
- what happens when Replay attempts an unauthorized persistence path.

## Compatibility
- Historical mode remains unchanged.

---

# Phase 6 — If Schema Evolution Is Selected

If and only if the normative choice is Model B, define but do not implement:

- target schema version;
- exact new allowed `source_authority` domain;
- migration direction;
- data backfill behavior if any;
- existing-row semantics;
- read/query compatibility;
- rollback expectations if repository governance defines them;
- migration tests required;
- application/runtime tests required.

Do not create migration files under this authority.

---

# Phase 7 — If Ephemeral Replay Is Selected

If and only if Model A is selected, define but do not implement:

- exact persistence boundary to bypass;
- exact catalog behavior;
- how Replay result is returned/observed;
- how Worker knows finite completion succeeded;
- how historical persistence remains untouched;
- tests required to prove no replay write occurs;
- tests required to prove canonical pipeline still runs fully.

Do not implement bypass logic under this authority.

---

# Phase 8 — Future Implementation Test Contract

Define required tests for the later implementation authority.

At minimum include:

- historical provenance unchanged;
- replay provenance not mislabeled as historical;
- canonical five-stage pipeline still runs for Replay;
- selected persistence/catalog semantics;
- schema constraint/migration behavior if applicable;
- real Worker Replay production flow;
- finite completion;
- no unauthorized historical-store usage;
- full regression.

Do not implement tests here.

---

# Decision Discipline

Prefer the narrowest model that satisfies #228 truthfully.

Do not choose schema evolution merely because persistence currently exists.

Do not choose ephemeral Replay merely to avoid migration if #228 requires persisted/cataloged output.

Do not preserve schema v3 at the cost of falsifying provenance.

Every normative choice must include a concise rationale tied to repository evidence.

---

# Stop Conditions

Stop immediately if:

- #228 cannot be read;
- source-authority semantics cannot be determined;
- replay persistence requirements remain ambiguous;
- choosing a model would materially redefine WP04+ behavior;
- schema-governance rules are unclear and schema evolution appears necessary;
- two materially different models remain equally valid.

On stop:

- make zero production changes;
- make zero schema changes;
- make zero GitHub changes;
- report exact unresolved governance choice;
- identify the minimum additional authority required.

---

# Success Criteria

This definition authority succeeds only when one unambiguous Replay provenance/schema boundary is established that specifies:

- semantic meaning of replay source authority;
- whether Replay persists;
- whether Replay registers in catalog;
- whether schema v3 remains unchanged;
- or, if evolution is required, exact future schema contract;
- historical compatibility;
- pipeline compatibility;
- Worker finite-completion behavior;
- error behavior;
- future implementation tests;
- non-goals.

No implementation occurs.

No schema mutation occurs.

No GitHub mutation occurs.

WP04 remains unstarted.

---

# Required Completion Report

Return:

## Source-authority semantics
- meaning of current authority values;
- meaning of Replay authority.

## Replay persistence requirement
- required / forbidden / conditional;
- evidence.

## Candidate analysis
Summarize Models A/B/C/(D if needed).

## Normative selected model
State exactly one:

`MODEL A — REPLAY NON-PERSISTENT AT SCHEMA V3 BOUNDARY`

or

`MODEL B — MINIMAL SCHEMA EVOLUTION REQUIRED FOR REPLAY PROVENANCE`

or

`MODEL C — EXISTING ALTERNATE PROVENANCE PATH`

or a precisely named Model D.

## Boundary contract
- Replay authority;
- persistence;
- catalog;
- schema;
- pipeline;
- Worker completion;
- historical compatibility;
- error behavior.

## Future implementation authority required
State exactly what may need to change in a later execution pass.

## Required future tests
List exact scenarios.

## Mutation proof

If no artifact is authorized/created:

`WP03 REPLAY SOURCE-AUTHORITY/SCHEMA DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP03 REPLAY SOURCE-AUTHORITY/SCHEMA CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement it here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP03 REPLAY SOURCE-AUTHORITY/SCHEMA DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP03 REPLAY SOURCE-AUTHORITY/SCHEMA DEFINITION BLOCKED`

Emit success only when the boundary is unambiguous.

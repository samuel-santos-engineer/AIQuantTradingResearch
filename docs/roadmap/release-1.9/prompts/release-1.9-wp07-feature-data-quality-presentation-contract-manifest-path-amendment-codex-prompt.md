# Release 1.9 — WP07 Feature/Data-Quality Presentation Contract + Manifest/Path-Authority Amendment — Codex Prompt

## Authority

This is a **narrow, definition-only authority** for Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Luna**.

WP07 is blocked before mutation because #232 requires presentation of “supported snapshot, validation, pipeline, and idempotency state,” but the accepted artifacts do not yet fix:

- the exact factual fields;
- their canonical sources;
- whether/how WP06 `VisualizationFrame` may be extended additively;
- exact sections, labels, ordering, and state behavior;
- deterministic functional assertions;
- a WP07-exclusive test path;
- the exact symbol/concern-level shared-file exception.

This authority resolves **only** those gaps.

No production implementation.
No test implementation.
No package/schema/protocol mutation.
No GitHub lifecycle mutation.
No WP08.
No WP09.

---

# Entry State

Expected:

- WP01–WP06: Closed / Done
- #231: Closed / Done
- #232: Open / Backlog
- #233: Open / Backlog
- #234–#237: Open / untouched
- milestone #58: Open
- canonical milestone count: 6 open / 6 closed
- full .NET predecessor: 305/305
- build: 0 errors / 0 warnings
- WP06 Python: 6/6
- WP05 Python: 3/3
- Streamlit: 1.61.1

WP07's prior execution attempt made zero mutations.

---

# Binding Predecessor Contracts

Do not redefine these.

## WP04

Canonical envelope remains:

`aiq-visualization-read-model-v1`

It already owns source/read-model facts, including where present:

- revision;
- source mode;
- source authority;
- target;
- dataset snapshot identity/version;
- presentation state;
- bounded observation window;
- latest observation/count;
- feature identity/value or WarmUp metadata;
- pipeline/status;
- validation/quality;
- safe failure;
- stale metadata.

## WP05

Preserve:

- local atomic JSON;
- read-only Python consumer;
- revision/cache/retry;
- one last-good envelope;
- ProducerUnavailable;
- transport warnings separate from backend state;
- no SQLite/provider/feature recomputation.

## WP06

Preserve the accepted `VisualizationFrame` semantics:

- inherited revision identity;
- exact price/time points;
- latest observation;
- canonical observation count;
- bounded window count/capacity;
- existing factual feature fields;
- existing `pipelineSuccess`;
- backend state;
- transport warning;
- deterministic projection/render inputs.

Current evidence says WP06 does **not** expose all snapshot identity/version, validation/quality, or idempotency details required by #232.

WP07 must not bypass WP06 by independently reading persistence or reparsing unrelated sources.

---

# Objective

Define the smallest truthful WP07 presentation contract for #232.

The contract must answer exactly:

1. What does “supported snapshot state” mean?
2. What does “validation state” mean?
3. What does “pipeline state” mean?
4. What does “idempotency state” mean?
5. Which exact fields are displayed?
6. What is the canonical source of every field?
7. Which fields are already in WP06?
8. Which missing fields may be added as a narrowly additive factual projection?
9. Where is that additive projection constructed?
10. What are the exact section names/labels/order?
11. How do Ready/WarmUp/Empty/Failed/Stale behave?
12. How do transport warnings interact with these sections?
13. What deterministic functional assertions prove correctness?
14. Which shared production symbols may WP07 modify?
15. What one dedicated WP07 test path is authorized?
16. What remains reserved for WP08/WP09?

Do not create new domain semantics merely to make the UI richer.

---

# Phase 0 — Read-Only Evidence

Before defining anything:

1. Read #232 completely.
2. Read the accepted Release 1.9 WP07 definition.
3. Read the implementation manifest.
4. Read the completed WP06 frame contract/path amendment.
5. Inspect current:
   - `python/presentation/realtime_financial_visualization.py`
   - `python/presentation/visualization_read_model.py`
   - WP06 dedicated tests
   - WP05 Python tests
6. Trace the serialized WP04 envelope fields actually available to Python.
7. Trace the canonical .NET sources for:
   - dataset snapshot identity/version;
   - validation/quality evidence;
   - pipeline status/evidence;
   - idempotency/determinism evidence.
8. Read #233/WP08 and WP09 definitions/manifest ownership sufficiently to preserve later-work boundaries.
9. Record the current relevant Python symbols.

No mutation.

---

# Phase 1 — Source-of-Truth Matrix

Create a normative matrix with one row per candidate WP07 field:

- user-visible concept;
- exact field name in the presentation contract;
- canonical source type/property;
- serialized JSON field/path;
- currently present in WP05 consumer? yes/no;
- currently present in WP06 frame? yes/no;
- additive projection required? yes/no;
- state availability rules;
- user-visible label;
- WP07-owned or forbidden/reserved.

Reject any candidate field without a governed canonical source.

No UI reconstruction from SQLite.
No provider lookup.
No feature recomputation.
No parsing of logs/exception text.

---

# Phase 2 — Snapshot Presentation Contract

Define **snapshot state** narrowly from existing canonical persisted/pipeline provenance.

Prefer only factual identity/version fields already present in the accepted envelope.

Determine from repository evidence whether the minimum supported set is:

- snapshot identity;
- dataset version;
- target;

and whether any additional snapshot fact is explicitly required by #232.

Do not expose:

- raw SQLite records;
- storage paths;
- internal hashes not already governed for presentation;
- arbitrary provenance blobs.

Define exact missing/unavailable behavior.

If snapshot identity/version semantics differ by state, specify that exactly.

---

# Phase 3 — Validation / Data-Quality Contract

Identify the exact canonical validation/quality evidence already produced by the pipeline/read model.

Define the smallest presentation projection.

It must be factual, not a new score.

Potential categories may include only those proven in repository evidence, such as:

- valid/accepted;
- invalid;
- unavailable/not-applicable;
- governed failure category.

Do **not** invent:

- numeric quality scores;
- confidence percentages;
- warning thresholds;
- color semantics;
- “good/bad” reinterpretations.

Define exact field names, labels, and state behavior from canonical evidence.

---

# Phase 4 — Pipeline Contract

WP06 already exposes `pipelineSuccess`.

Determine whether #232 requires only that fact or additional existing high-level pipeline status already present in the envelope.

Define the exact permitted projection.

Do not expose:

- stack traces;
- arbitrary stage internals;
- raw provider evidence;
- full diagnostic objects.

If a stage/status summary is already governed and necessary, specify the exact safe fields.

No new pipeline computation.

---

# Phase 5 — Idempotency Contract

This is the highest-risk semantic area.

Trace what the repository actually means by idempotency/determinism in the accepted pipeline/evidence contracts.

Do not equate idempotency with:

- revision equality;
- cache hit;
- duplicate observation handling;
- successful execution;
- snapshot identity alone

unless repository evidence explicitly defines that relationship.

Define only a factual presentation projection that is directly supported by canonical evidence.

For every idempotency field specify:

- exact canonical source;
- meaning;
- allowed values;
- unavailable behavior;
- whether it is per execution, per snapshot, per stage, or another governed scope.

If #232 requires “idempotency state” but the repository exposes no truthful governed idempotency fact suitable for presentation, **STOP** and report that predecessor contract gap instead of inventing one.

---

# Phase 6 — Additive Projection Decision

Determine whether WP07 may extend the existing Python `VisualizationFrame` with narrowly additive factual fields.

Preferred model, if compatible with repository evidence:

**Additive Model A — extend `VisualizationFrame` with optional immutable factual metadata fields sourced from the same accepted WP05 envelope.**

Rules:

- existing WP06 fields and semantics remain unchanged;
- existing WP06 callers/tests remain valid;
- no revision semantics change;
- no new data acquisition;
- no second frame type;
- no side channel;
- no persistence access;
- no recomputation;
- additive fields are optional only where predecessor state legitimately lacks the fact.

If this model cannot preserve WP06 compatibility, stop and explain why.

Do not choose a parallel dashboard data model unless separately authorized.

---

# Phase 7 — Serialization / Consumer Boundary

Trace whether the required canonical fields already cross Worker → JSON → Python.

Two cases:

## Case A — already serialized

WP07 may project them from the accepted WP05 consumer into the additive frame fields.

No .NET change.

## Case B — canonical field exists in WP04/.NET but is not serialized/parsed

This definition must determine whether the accepted envelope contract already semantically includes the field and only a narrow serialization/parser exposure is missing.

If yes, define the minimum additive transport exposure needed, but **do not authorize implementation here**.

If adding the field would change the semantic envelope contract rather than merely expose an already-owned field, stop and request predecessor authority.

Do not silently broaden JSON.

---

# Phase 8 — Exact WP07 Sections

Define deterministic section order.

Unless #232/repository evidence requires different terminology, prefer a minimal order:

1. **Snapshot**
2. **Data Quality**
3. **Pipeline**
4. **Idempotency**

Use repository/user-facing terminology if already fixed.

For each section define:

- exact title;
- exact ordered rows;
- exact labels;
- value formatting rules;
- unavailable representation;
- visibility rules by backend state.

Do not define decorative layout.

---

# Phase 9 — Exact Labels and Values

Every label must map 1:1 to a governed fact.

Define deterministic text values for enum/state presentation.

Rules:

- no marketing language;
- no interpretive adjectives;
- no recommendations;
- no emojis;
- no inferred severity unless canonical;
- no locale-dependent ordering;
- no wall-clock-derived text.

For identities/versions, define whether full value or governed short form is displayed. Prefer full canonical value unless an existing presentation convention fixes shortening.

For booleans, define one exact representation consistently, e.g. `Yes` / `No`, only if no repository convention already exists.

---

# Phase 10 — Backend-State Behavior

Define section behavior for all WP04 states.

## Ready

Show all available factual WP07 fields.

## WarmUp

Snapshot/validation/pipeline/idempotency facts remain visible when canonically available.
Do not treat feature WarmUp as data-quality failure unless canonical evidence says so.

## Empty

Show only facts legitimately available for an Empty envelope.
Do not fabricate snapshot/idempotency facts.

## Failed

Show factual fields retained/present in the accepted envelope plus the existing safe failure surface.
Do not derive missing metadata from the failure message.

## Stale

Show retained factual payload exactly as accepted.
Backend state remains Stale.

No section may reinterpret the state.

---

# Phase 11 — Transport Warning Behavior

Transport warning remains separate.

WP07 sections continue to reflect the last-good accepted frame/envelope where available.

Transport warning must not:

- change validation state;
- change pipeline state;
- change idempotency state;
- change snapshot identity/version;
- become backend Failed/Stale.

If no accepted payload exists, show ProducerUnavailable according to WP05 and do not fabricate WP07 section values.

---

# Phase 12 — Deterministic Assertion Surface

Future WP07 tests must assert **functional presentation data**, not pixels.

Define a pure deterministic projection, e.g. section/row structures, that can be asserted before Streamlit rendering.

For each section tests must assert:

- exact section order;
- exact title;
- exact row order;
- exact labels;
- exact values;
- exact omitted/unavailable rows;
- backend-state behavior;
- transport-warning separation.

No screenshot harness required for WP07.

---

# Phase 13 — WP06 Compatibility Assertions

Required future tests must prove:

- existing WP06 frame fields unchanged;
- price/time unchanged;
- latest/count/window unchanged;
- revision unchanged;
- transport warning unchanged;
- WP06 6/6 still passes;
- additive WP07 fields do not affect WP06 chart semantics.

No existing WP06 assertion may be weakened.

---

# Phase 14 — WP08 / WP09 Boundary

Read accepted definitions and state exact exclusions.

At minimum, unless repository evidence says otherwise:

WP07 does **not** own:

- lifecycle/startup/shutdown demonstration;
- residue/process cleanup demonstration;
- end-to-end operational demonstration owned by WP08;
- permanent integration/architecture test suite owned by WP09;
- screenshot/visual regression infrastructure;
- broad architecture enforcement;
- release-closeout evidence.

WP07 may add only its dedicated focused functional test path.

---

# Phase 15 — Shared Production Path Amendment

The shared production path is expected to remain:

`python/presentation/realtime_financial_visualization.py`

Define a symbol/concern-level WP07 exception permitting only:

- additive factual WP07 projection fields if the selected model requires them;
- pure WP07 section/row projection;
- rendering of Snapshot/Data Quality/Pipeline/Idempotency sections;
- truthful state/unavailable handling;
- preservation of existing WP06 chart/latest/count behavior.

Forbid in this shared path:

- WP08 lifecycle work;
- WP09 architecture/integration harness;
- transport/cache/retry changes;
- new data acquisition;
- feature recomputation;
- styling/theme expansion beyond minimal section rendering;
- new controls unless #232 explicitly requires them.

Use exact current symbols from repository evidence in the final amendment.

---

# Phase 16 — `visualization_read_model.py` Decision

Default: no WP07 mutation authority.

If required fields are already parsed by WP05, keep this path forbidden.

If an already-serialized, already-semantic envelope field needs a narrow parser exposure for WP07, define the exact parser symbol/field exception.

Do not change:

- retries;
- cache;
- revision comparison;
- contract-version validation;
- ProducerUnavailable;
- last-good semantics.

If required data is not serialized at all, follow Phase 7 rather than inventing parser data.

---

# Phase 17 — Dedicated WP07 Test Path

Authorize exactly one dedicated WP07 Python test file using actual repository convention.

Expected candidate, if consistent with the repository:

`python/presentation/test_realtime_financial_visualization_wp07.py`

The final definition must confirm the exact path from repository evidence.

It must be:

- WP07-exclusive;
- focused on feature/data-quality presentation contract;
- separate from WP05 tests;
- separate from WP06 test path;
- separate from WP09 Streamlit/integration tests.

No wildcard directory grant.

---

# Phase 18 — Optional Transport Exposure Path

Only if Phase 7 proves an existing WP04 semantic field is missing solely from serialization, define the **minimum exact path/symbol exceptions** needed to carry that already-owned field through Worker JSON and Python parsing.

This is not automatically authorized.

For every such path prove:

- field already belongs to accepted WP04 envelope semantics;
- no new semantic contract is created;
- no schema/persistence change;
- no revision/state change;
- no unrelated field exposure.

If this cannot be proven, stop with a predecessor-contract blocker.

---

# Phase 19 — Exact Manifest Amendment

Produce an exact WP07 allowlist.

For each path:

- exact path;
- exclusive/shared;
- exact symbols/concerns;
- why required;
- forbidden adjacent concerns.

Prefer the minimum:

1. shared `python/presentation/realtime_financial_visualization.py` WP07 symbol/concern exception;
2. dedicated `python/presentation/test_realtime_financial_visualization_wp07.py`;
3. only if proven necessary, narrow parser/serialization path exception from Phases 16–18.

No broad directory ownership.
No speculative helper file.

---

# Phase 20 — Required Future Test Matrix

The later Terra implementation authority must prove at least:

## Snapshot

- exact identity/version/target fields authorized;
- exact labels/order;
- unavailable behavior.

## Data Quality

- every canonical allowed validation/quality state;
- no invented scoring/severity;
- exact labels/order.

## Pipeline

- `pipelineSuccess` and any other explicitly authorized factual status;
- exact state behavior;
- no raw diagnostics.

## Idempotency

- every governed idempotency value/state;
- exact source semantics;
- unavailable behavior;
- no conflation with revision/cache/duplicates unless canonical.

## Backend states

- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale.

## Transport

- ProducerUnavailable with no payload;
- warning with last-good payload;
- backend metadata unchanged by transport warning.

## Compatibility

- WP06 6/6;
- WP05 3/3;
- price/time/latest/count/window unchanged;
- revision unchanged.

## Boundaries

- no SQLite;
- no provider;
- no feature recomputation;
- no Worker control;
- no WP08;
- no WP09 test reuse;
- no new dependency.

## Regression

- Python compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`;
- build;
- full .NET regression 305/305 unless an explicitly authorized .NET test change changes the count.

---

# Phase 21 — Future Implementation Stop Rule

The later WP07 Terra implementation must stop if:

- any factual field lacks a canonical source;
- idempotency meaning remains ambiguous;
- required data is absent from the accepted transport and cannot be exposed under this amendment;
- a path outside the final allowlist is needed;
- WP06 semantics must change;
- WP08/WP09 work is needed;
- a new dependency is needed;
- screenshot/pixel infrastructure is needed;
- SQLite/provider/recomputation is needed.

No improvisation.

---

# Non-Goals

This authority does not authorize:

- production code;
- test code;
- GitHub mutation;
- closing #232;
- WP08;
- WP09;
- schema changes;
- persistence changes;
- provider changes;
- pipeline algorithm changes;
- feature formula changes;
- new metrics;
- new quality scores;
- trading recommendations;
- styling/theme design;
- lifecycle demonstration;
- permanent architecture/integration tests;
- package changes.

---

# Mutation Policy

Definition-only.

If the accepted Release 1.9 governance permits a dedicated WP07 definition/amendment artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_FEATURE_DATA_QUALITY_PRESENTATION_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

If that exact path is not permitted by governance, create no repository artifact and return the normative definition in chat.

Under no circumstance mutate production, tests, packages, schema, GitHub, Project, or lifecycle state.

---

# Required Completion Report

## Repository evidence

Report:

- #232 wording;
- actual WP04 envelope sources;
- actual JSON/Python availability;
- current WP06 frame fields;
- idempotency evidence source;
- WP08/WP09 boundary;
- manifest ownership.

## Source-of-truth matrix

List every authorized WP07 factual field and exact source.

## Selected additive model

State whether and how `VisualizationFrame` may be extended without changing WP06 semantics.

## Presentation contract

State exact:

- sections;
- order;
- rows;
- labels;
- values;
- unavailable rules;
- Ready/WarmUp/Empty/Failed/Stale behavior;
- transport-warning behavior.

## Deterministic assertion contract

State exact functional test surface.

## Manifest/path amendment

List exact:

- shared production path/symbol exception;
- dedicated WP07 test path;
- parser/serialization exception only if proven necessary;
- forbidden paths.

## Future test matrix

List exact acceptance coverage.

## Mutation proof

If one authorized documentation artifact is created:

`WP07 FEATURE/DATA-QUALITY PRESENTATION DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP07 FEATURE/DATA-QUALITY PRESENTATION DEFINITION MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

---

# Terminal Markers

On success:

`RELEASE 1.9 WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

On blocker:

`RELEASE 1.9 WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Do not emit success unless snapshot, validation/data-quality, pipeline, and idempotency semantics all have canonical sources; the additive WP06 boundary is fixed; presentation labels/order/state behavior are deterministic; and exact WP07 path/test ownership is defined.

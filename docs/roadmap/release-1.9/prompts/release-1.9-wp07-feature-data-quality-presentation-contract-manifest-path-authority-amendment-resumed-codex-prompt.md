# Release 1.9 — WP07 Feature/Data-Quality Presentation Contract + Manifest/Path-Authority Amendment — Resumed

## Authority

Use **GPT-5.6 Luna**.

This is a **definition/documentation-only** authority for Release 1.9 WP07, canonical issue **#232**.

The previous WP07 presentation-definition attempt was blocked because canonical idempotency and data-quality facts did not exist. That blocker is now resolved by accepted semantic definition, path authority, and completed implementation/acceptance.

This authority must now define the exact deterministic WP07 presentation contract and the minimum manifest/path authority required for its later implementation.

Do not implement production code or tests.

Do not close #232.

Do not start WP08/WP09.

---

# Binding Predecessor Authorities

Read and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
3. The completed WP07 canonical semantic-exposure implementation/acceptance evidence whose terminal marker is:
   `RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE COMPLETE`
4. The accepted WP06 visualization-frame contract/manifest amendment:
   `docs/roadmap/release-1.9/RELEASE_1.9_WP06_VISUALIZATION_FRAME_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
5. Existing accepted Release 1.9 manifest and WP05/WP06 authorities needed to preserve ownership boundaries.

Current validated predecessor baseline:

- full .NET: **309/309**;
- Application: **125/125**;
- Infrastructure: **160/160**;
- WP07 semantic-exposure Python: **2/2**;
- WP05 Python: **3/3**;
- WP06 Python: **6/6**;
- build: 0 warnings / 0 errors;
- Streamlit: 1.61.1;
- `pip check`: clean.

Verify repository reality rather than relying only on these counts.

---

# Fixed Canonical Facts

WP07 consumes facts; it does not derive them.

## Idempotency

`PresentationIdempotencyStatus`:

- `NewlyPersisted`
- `EquivalentExisting`
- `Unavailable`

## Data quality

`PresentationDataQualityStatus`:

- `Valid`
- `Invalid`
- `Unavailable`

These meanings, scopes, state behavior, non-equivalences, stale retention, and source ownership are already fixed.

WP07 must not reinterpret them.

---

# Objective

Define the exact WP07 presentation contract for:

- feature status/value;
- snapshot identity/version;
- validation/data-quality status;
- pipeline status;
- idempotency status;
- any other factual metadata explicitly required by #232 and already present in the accepted WP06 frame.

The contract must fix:

1. exact allowed fields;
2. exact source for each field;
3. exact sections;
4. exact labels;
5. exact section/field ordering;
6. exact rendering representation;
7. exact behavior for Ready / WarmUp / Empty / Failed / Stale;
8. exact behavior for transport warnings;
9. exact unavailable-value representation;
10. deterministic assertion surface;
11. exact shared production symbols permitted to change;
12. exact dedicated WP07 presentation test path.

No material presentation choice should remain for the later Terra implementation authority.

---

# Phase 0 — Read-Only Repository Verification

Before defining anything:

1. Read #232.
2. Read all binding predecessor artifacts.
3. Inspect current:
   - `python/presentation/realtime_financial_visualization.py`;
   - `python/presentation/visualization_read_model.py`;
   - dedicated WP05 tests;
   - dedicated WP06 tests;
   - dedicated WP07 semantic-exposure tests;
   - actual `VisualizationFrame` fields;
   - current Streamlit rendering symbols/functions.
4. Verify the reserved path:
   `python/presentation/test_realtime_financial_visualization_wp07.py`
   remains unused.
5. Inspect accepted manifest ownership for WP07/WP08/WP09.
6. Confirm no WP07 presentation implementation already exists.

No mutation except the single authorized documentation artifact at the end.

---

# Phase 1 — Exact Factual Input Inventory

Enumerate every current `VisualizationFrame` factual field.

Classify each as:

- WP06 core visualization fact;
- WP07 feature fact;
- WP07 snapshot fact;
- WP07 data-quality fact;
- WP07 pipeline fact;
- WP07 idempotency fact;
- transport-only fact;
- not authorized for WP07 display.

For every WP07-displayed fact, name its exact upstream source.

Do not add new upstream semantics.

If #232 requires a fact that still does not exist in the current frame/exposure chain, stop and identify the missing predecessor fact. Do not derive it in presentation.

---

# Phase 2 — WP07 Presentation Sections

Define a fixed presentation structure.

The authority must select exact section names and exact order based on #232 and current factual inputs.

At minimum determine whether the contract uses distinct sections equivalent to:

1. Feature
2. Snapshot / Data
3. Data Quality / Validation
4. Pipeline
5. Idempotency

Do not leave names as “or equivalent”.

Choose one canonical label for every section.

Specify whether existing WP06 chart/latest/count content remains before or outside these sections.

WP07 must be additive to WP06.

---

# Phase 3 — Exact Field Labels and Order

For each WP07 section define:

- exact displayed label;
- exact frame property;
- exact order;
- exact representation type.

Examples of representation types that must be resolved, not left open:

- plain text;
- metric;
- caption;
- table row;
- key/value text.

Prefer the smallest deterministic Streamlit surface compatible with existing code.

Do not introduce styling systems, cards, CSS, icons, colors, or layout complexity unless #232 explicitly requires them.

WP08 owns lifecycle/demonstration concerns; WP09 owns permanent integration/architecture testing.

---

# Phase 4 — Feature Presentation Contract

Using only existing canonical frame facts, define exactly:

- feature identity label;
- feature state/value representation;
- WarmUp representation;
- required/current count if available and required;
- numeric formatting for available decimal feature value;
- timestamp representation if available and required.

Do not recompute the feature.

Do not infer feature validity from data quality.

Fix deterministic formatting.

---

# Phase 5 — Snapshot Presentation Contract

Define exact display of supported snapshot metadata already exposed.

Resolve:

- snapshot identity;
- snapshot version;
- missing/unavailable representation;
- whether full identity or bounded deterministic abbreviation is displayed.

If abbreviation is chosen, define exact deterministic rule and preserve full value in the frame without mutation.

Do not expose SQLite records, provider payloads, paths, credentials, or arbitrary provenance.

---

# Phase 6 — Data-Quality Presentation Contract

Define exact rendering for:

- `Valid`;
- `Invalid`;
- `Unavailable`.

The UI must display the canonical status directly.

No score.
No severity.
No confidence.
No inferred explanation.

If a safe existing canonical failure/validation message is displayed, it must already be explicitly authorized by the upstream contract; otherwise status only.

Do not equate `pipelineSuccess` with data quality.

---

# Phase 7 — Pipeline Presentation Contract

Define exact supported pipeline facts already present in the frame.

Resolve:

- exact label for pipeline status;
- exact representation of success/failure;
- whether existing backend presentation state is shown separately;
- whether pipeline failure category/message is available and safe to display.

Do not reinterpret WP04 states.

Do not add pipeline stages.

---

# Phase 8 — Idempotency Presentation Contract

Define exact rendering for:

- `NewlyPersisted`;
- `EquivalentExisting`;
- `Unavailable`.

Use the canonical terms or define exact user-facing labels with an explicit one-to-one mapping.

Do not call `EquivalentExisting` a cache hit, duplicate tick, stale result, or retry.

Do not call `NewlyPersisted` “non-idempotent”.

No derived idempotency indicator.

---

# Phase 9 — State Matrix

Define deterministic WP07 rendering for every WP04 backend state:

- Ready;
- WarmUp / NotReady;
- Empty;
- Failed;
- Stale.

For each state specify which WP07 sections:

- render normally;
- render with `Unavailable`;
- retain last-good facts;
- suppress values;
- show failure metadata.

Use the already-fixed semantic-definition state matrix for idempotency/data quality.

Do not create new backend states.

Payload retention must remain explicit.

---

# Phase 10 — Transport Warning Boundary

Define exactly how WP05 transport warnings coexist with WP07 factual metadata.

Requirements:

- transport warning remains separate;
- it does not alter data quality;
- it does not alter idempotency;
- it does not alter pipeline status;
- last-good factual metadata remains factual.

Specify deterministic placement relative to WP07 sections.

No transport reinterpretation.

---

# Phase 11 — Unavailable Representation

Choose one exact deterministic display token for unavailable factual values/statuses.

Examples might be `Unavailable` or `—`, but select one and define its use.

Do not use blank/null rendering where that would make state ambiguous.

Distinguish:

- canonical enum value `Unavailable`;
- absent optional snapshot/feature value;
- transport ProducerUnavailable.

Do not collapse them semantically.

---

# Phase 12 — Formatting Contract

Fix all deterministic formatting needed for tests.

At minimum resolve:

- decimal feature formatting;
- timestamps;
- snapshot identity/version;
- booleans if displayed;
- status strings;
- count integers.

Use locale-independent deterministic formatting.

Do not introduce timezone conversion unless already fixed upstream.

---

# Phase 13 — Deterministic Functional Assertion Surface

Define exact pure/deterministic symbols that the later WP07 tests may assert without launching a long-running Streamlit process.

Prefer testing:

- frame-to-presentation projection, if needed;
- exact ordered label/value rows/sections;
- exact state behavior;
- exact status mapping.

If the existing `VisualizationFrame` can be rendered deterministically without adding another model, prefer that.

Do not create a redundant semantic model unless necessary.

Specify the exact function/symbol contract the later implementation may add or extend.

---

# Phase 14 — Production Path Authority

Using actual current paths, grant the smallest symbol-level WP07 presentation exception.

Expected shared path:

`python/presentation/realtime_financial_visualization.py`

Preserve WP05/WP06 ownership.

Authorize WP07 only for the exact symbols required to:

- project/render the fixed WP07 sections;
- format the fixed factual values;
- integrate them additively into the existing Streamlit rendering flow.

Do not grant directory-wide ownership.

If another production path is genuinely required, identify it exactly and justify it. If not already safely derivable from current architecture, stop instead of broadening scope.

---

# Phase 15 — Dedicated WP07 Presentation Test Path

Activate exactly the previously reserved path if repository reality confirms it:

`python/presentation/test_realtime_financial_visualization_wp07.py`

This path becomes WP07-exclusive for presentation-contract tests.

It may test:

- exact section order;
- exact labels;
- exact field order;
- exact formatting;
- Ready/WarmUp/Empty/Failed/Stale behavior;
- idempotency values;
- data-quality values;
- snapshot/pipeline/feature display;
- transport-warning separation;
- deterministic sequential frames where relevant to WP07 metadata.

It must not absorb WP08 lifecycle/demonstration or WP09 permanent integration/architecture concerns.

---

# Phase 16 — Shared-File Ownership Matrix

Create an exact table for every production path affected:

- path;
- existing owner;
- WP07 exception;
- exact symbols;
- exact allowed concern;
- forbidden adjacent concerns.

No wildcard grants.

---

# Phase 17 — Test Ownership Matrix

Define:

- dedicated WP07 presentation test path;
- exact concern;
- relationship to WP05/WP06/WP07-semantic-exposure tests;
- explicit WP08/WP09 exclusions.

Do not modify predecessor test ownership.

---

# Phase 18 — Later Implementation Acceptance Tests

Define the required future tests precisely.

At minimum require:

- exact section order;
- exact labels/order within each section;
- exact Ready rendering;
- exact WarmUp rendering;
- exact Empty rendering;
- exact Failed rendering;
- exact Stale retained rendering;
- `NewlyPersisted`;
- `EquivalentExisting`;
- idempotency `Unavailable`;
- data quality `Valid`;
- data quality `Invalid`;
- data quality `Unavailable`;
- feature available and WarmUp;
- snapshot available/unavailable;
- pipeline success/failure;
- transport warning remains separate;
- no recomputation/derivation;
- existing WP06 chart semantics unchanged;
- WP05/WP06 regressions unchanged.

Fix any exact expected strings needed by these tests in this definition.

---

# Phase 19 — Later Implementation Validation Gate

Specify the future consolidated WP07 implementation must run:

- dedicated WP07 presentation tests;
- WP07 semantic-exposure tests;
- WP06 Python 6/6;
- WP05 Python 3/3;
- Python compilation;
- Streamlit 1.61.1 smoke;
- `pip check`;
- build 0/0;
- full .NET regression from current **309/309** predecessor baseline;
- no Streamlit process residue.

WP07 implementation may add Python tests but should not need new .NET tests unless this definition identifies a real uncovered contract requiring them. If it does, exact paths must be authorized here.

---

# Phase 20 — Lifecycle Boundary

The later WP07 implementation authority may close #232 only after all fixed WP07 acceptance criteria pass.

This definition authority itself:

- keeps #232 Open / Backlog;
- keeps #233 Open / Backlog;
- does not start WP08;
- does not mutate GitHub.

---

# Non-Goals

Do not authorize or define:

- WP08 lifecycle ownership;
- WP08 demonstration/residue work;
- WP09 integration/architecture tests;
- schema/persistence changes;
- provider access;
- feature recomputation;
- new validation;
- new idempotency semantics;
- transport changes;
- refresh changes;
- revision changes;
- chart redesign;
- CSS/styling framework;
- new packages;
- HTTP/WebSocket/queue IPC;
- multi-user runtime.

---

# Documentation Mutation

If repository governance permits the expected definition artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_FEATURE_DATA_QUALITY_PRESENTATION_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

No production/test/GitHub mutation.

If the documentation path itself is not authorized, return the complete normative definition in chat and make zero mutations.

---

# Required Completion Report

## Binding predecessor evidence
Confirm the semantic/exposure/WP06 authorities and current baseline.

## Factual input inventory
List exact frame fields WP07 may display.

## Presentation contract
Exact sections, labels, order, representations, formatting.

## State matrix
Ready/WarmUp/Empty/Failed/Stale.

## Transport boundary
Exact warning placement/separation.

## Deterministic assertion surface
Exact functions/symbols and expected structures.

## Production path amendment
Exact shared-file symbol exceptions.

## Test path amendment
Confirm dedicated:
`python/presentation/test_realtime_financial_visualization_wp07.py`

## Future acceptance
Exact required tests and validation commands/count baselines.

## Scope boundary
WP08/WP09 and all forbidden concerns excluded.

## Mutation statement

If one authorized documentation artifact is created:

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT/PATH AMENDMENT MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT/PATH AUTHORITY DEFINED — CONSOLIDATED WP07 IMPLEMENTATION REQUIRES FRESH AUTHORITY`

---

# Stop Conditions

Stop if:

- #232 requires a factual field still absent from the canonical exposure chain;
- current frame contradicts binding semantics;
- exact WP07 rendering requires an unauthorized upstream semantic change;
- another production path is required but cannot be narrowly governed;
- the reserved WP07 test path is already consumed incompatibly;
- WP08/WP09 ownership would be crossed;
- a material label/order/state/format choice cannot be fixed from accepted scope without inventing a new product requirement beyond #232.

When blocked, identify the minimum missing definition authority.

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP07 FEATURE/DATA-QUALITY PRESENTATION CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Do not emit COMPLETE while any material WP07 presentation or path choice remains unresolved.

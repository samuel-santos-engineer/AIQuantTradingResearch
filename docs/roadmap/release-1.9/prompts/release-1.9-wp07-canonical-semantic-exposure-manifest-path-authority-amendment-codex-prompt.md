# Release 1.9 — WP07 Canonical Semantic-Exposure Manifest/Path-Authority Amendment

## Authority

This is a **narrow definition-only manifest/path-authority amendment** supporting Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Luna**.

The following accepted artifact is **binding semantic authority**:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`

It fixes:

- `PresentationIdempotencyStatus`
  - `NewlyPersisted`
  - `EquivalentExisting`
  - `Unavailable`
- `PresentationDataQualityStatus`
  - `Valid`
  - `Invalid`
  - `Unavailable`
- their exact scopes;
- canonical sources;
- backend-state matrix;
- non-equivalences;
- additive Application → WP04 → JSON → WP05 → WP06 → WP07 exposure semantics;
- versioning constraints;
- required future tests.

A subsequent Terra implementation authority was blocked before mutation because the accepted manifest does not authorize the required cross-WP files/symbols.

This authority exists **only** to amend path/symbol ownership narrowly enough to implement that already-defined semantic exposure.

It does not redefine the semantics.

It does not implement them.

---

# Proven Manifest Blocker

The blocked implementation identified these required surfaces as unauthorized:

- `PipelineExecutionResult` / `PipelineExecutionEvidence` — no WP07 ownership.
- `VisualizationReadModelContracts.cs` — WP04-owned.
- `VisualizationReadModelFilePublisher.cs` — absent from accepted manifest.
- `python/presentation/visualization_read_model.py` — WP05-exclusive.
- WP06 amendment authorizes only the shared Streamlit entry point and WP06-exclusive test path, not the upstream semantic-exposure chain.

Treat these as evidence to verify against the current repository and manifest.

No implementation/test/GitHub mutation occurred in the blocked pass.

---

# Objective

Create the **minimum exact manifest/path amendment** that permits a later Terra authority to implement only:

canonical semantic source mapping
→ Application result/evidence
→ WP04 envelope
→ Worker JSON publisher
→ WP05 Python parser
→ WP06 additive factual projection

with focused tests.

The amendment must grant **symbol/concern-level exceptions** across predecessor-owned files.

It must **not transfer general ownership** of WP04, WP05, or WP06 files to WP07.

---

# Phase 0 — Read-Only Verification

Before writing the amendment:

1. Read the binding semantic-definition artifact completely.
2. Read the accepted Release 1.9 implementation manifest.
3. Read all accepted WP04/WP05/WP06 path amendments.
4. Inspect the actual current repository paths/symbols for:
   - `PipelineExecutionResult`;
   - `PipelineExecutionEvidence`;
   - the source persistence outcome used for `PresentationIdempotencyStatus`;
   - the source validation evidence used for `PresentationDataQualityStatus`;
   - `VisualizationReadModelContracts.cs`;
   - WP04 producer/use-case mapping;
   - `VisualizationReadModelFilePublisher.cs`;
   - Worker composition if serialization is wired there;
   - `python/presentation/visualization_read_model.py`;
   - `VisualizationFrame` and frame construction in `realtime_financial_visualization.py`;
   - relevant current test files.
5. Identify the exact namespace/type/member names.
6. Identify whether any listed filename from the blocker differs from current repository reality.
7. Read WP08/WP09 path ownership sufficiently to avoid collision.

No production/test mutation.

---

# Phase 1 — Exact Exposure Chain

Produce a normative chain with exact current symbols:

## Layer A — Canonical source

Identify exact existing source types/properties that determine:

- `NewlyPersisted`;
- `EquivalentExisting`;
- `Unavailable`;
- `Valid`;
- `Invalid`;
- `Unavailable`.

This amendment may authorize **mapping/read access only** to those outcomes.

It must not authorize changing their behavior.

## Layer B — Application projection

Identify exact file(s) containing:

- `PipelineExecutionResult`;
- `PipelineExecutionEvidence`;
- any new immutable enum/value types required by the binding definition.

Authorize only:

- declaration of the two semantic value domains;
- additive immutable properties;
- mapping from canonical source evidence.

No pipeline redesign.

## Layer C — WP04 envelope

Identify exact WP04 contract and producer/mapping files required to carry the two facts.

Authorize only:

- additive immutable envelope fields;
- exact state-matrix mapping from the binding definition;
- preservation/retention behavior already fixed.

No state/revision/window changes.

## Layer D — Worker JSON

Identify the exact publisher/serializer path and symbol.

Authorize only:

- serialization of the two new fields;
- exact enum/string representation required by the semantic definition;
- compatibility with the accepted `aiq-visualization-read-model-v1` conclusion.

No path/lifecycle/atomic-write changes.

## Layer E — WP05 Python parser

Identify exact parser dataclass/model/function symbols.

Authorize only:

- parsing;
- validation;
- preservation of the two fields;
- defined unavailable/backward behavior.

No retry/cache/revision/transport changes.

## Layer F — WP06 projection

Identify exact `VisualizationFrame` declaration/construction symbols.

Authorize only:

- narrowly additive immutable factual fields for the two statuses;
- direct propagation.

No chart/price/time/latest/count/window/feature/state/transport-warning changes.

---

# Phase 2 — Application Path Amendment

Using actual repository paths, define exact shared exceptions for the minimum Application files.

For each path specify:

- existing owner;
- WP07 semantic-exposure exception;
- exact types/members allowed;
- forbidden adjacent concerns.

Allowed concerns should be limited to:

- `PresentationIdempotencyStatus`;
- `PresentationDataQualityStatus`;
- additive `PipelineExecutionResult` / `PipelineExecutionEvidence` exposure;
- direct mapping from accepted source evidence.

Forbidden:

- stage changes;
- new validators;
- persistence behavior;
- Replay semantics;
- feature computation;
- schema.

If semantic types belong in a new file, authorize **one exact new file only** if repository conventions make that necessary.

Prefer existing contract file when architecturally appropriate.

---

# Phase 3 — WP04 Path Amendment

Verify exact path for `VisualizationReadModelContracts.cs` and any producer/use-case file that must map the new statuses.

Grant a shared WP07 semantic-exposure exception only for:

- two additive envelope fields;
- their immutable types if contract layering requires them;
- binding state-matrix propagation.

Preserve:

- contract version conclusion;
- revision;
- state;
- bounded window;
- feature;
- failure/stale behavior.

No general WP04 ownership transfer.

---

# Phase 4 — Worker Publisher Path Amendment

Verify the actual path of `VisualizationReadModelFilePublisher.cs`.

If it exists and is absent from the manifest, add it explicitly as a **shared WP05/WP07 transport-serialization path** with a WP07 exception limited to serializing the two fields.

If serialization occurs elsewhere, name the actual path instead.

Authorize no changes to:

- canonical handoff location;
- startup cleanup;
- temp naming;
- atomic replace;
- flush/close;
- directory ownership;
- lifecycle.

If more than one production publisher path is genuinely required, justify each exact path. Do not grant a directory.

---

# Phase 5 — WP05 Parser Exception

For:

`python/presentation/visualization_read_model.py`

or the verified actual path, preserve WP05 ownership.

Grant WP07 only a symbol/concern exception for:

- two parsed factual status fields;
- exact enum/value validation;
- unavailable/backward behavior fixed by the semantic definition.

Explicitly forbid WP07 changes to:

- read attempts;
- 50 ms retry;
- cadence;
- cache;
- revision comparison;
- last-good;
- ProducerUnavailable;
- contract-version rejection;
- transport warning.

---

# Phase 6 — WP06 Shared Projection Exception

Verify the actual `VisualizationFrame` path.

Expected shared production path:

`python/presentation/realtime_financial_visualization.py`

Preserve WP06 ownership of frame/chart semantics.

Grant WP07 semantic-exposure exception only for:

- adding the two immutable factual fields to `VisualizationFrame`;
- direct propagation during frame construction;
- no rendering of WP07 sections yet.

Explicitly forbid:

- chart changes;
- price/time changes;
- latest/count/window changes;
- feature changes;
- `pipelineSuccess` reinterpretation;
- backend-state changes;
- transport-warning changes;
- WP07 labels/sections/rendering.

---

# Phase 7 — .NET Focused Test Paths

The later implementation must test the source → Application → WP04 → JSON chain.

Inspect current test organization and authorize the minimum exact dedicated/focused paths.

Prefer **dedicated WP07 semantic-exposure test files** rather than expanding predecessor-exclusive test ownership broadly.

Potential categories:

- Application semantic projection tests;
- Infrastructure production/serialization composition tests.

Use actual project naming conventions.

Authorize exact paths, for example only if repository structure supports them:

- `tests/AIQuantTradingResearch.Application.Tests/PresentationSemanticExposureTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationSemanticExposureTests.cs`

Do not blindly use these names; inspect and choose exact paths consistent with the repository.

Each dedicated file is WP07 semantic-exposure-only.

Do not repurpose WP09 architecture/integration test ownership.

---

# Phase 8 — Python Focused Test Path

Authorize one dedicated semantic-exposure parser/frame test path distinct from:

- WP05-exclusive tests;
- WP06-exclusive `test_realtime_financial_visualization_wp06.py`;
- future WP07 presentation test;
- WP09 tests.

Choose an exact repository-consistent path.

Preferred conceptual name:

`python/presentation/test_visualization_semantic_exposure_wp07.py`

but verify naming conventions before fixing it.

This file may test only:

- parser values;
- unavailable/backward behavior;
- frame propagation;
- preservation of WP06 semantics.

No WP07 section/rendering assertions.

---

# Phase 9 — Future WP07 Presentation Test Reservation

This amendment should **not consume** the dedicated test path needed for the later WP07 feature/data-quality presentation contract.

Explicitly reserve a separate later path, expected conceptually as:

`python/presentation/test_realtime_financial_visualization_wp07.py`

if consistent with the accepted repository convention.

The semantic-exposure test and presentation test must remain distinct concerns.

Do not implement either here.

---

# Phase 10 — Manifest Ownership Matrix

Produce an exact table containing:

- path;
- current owner;
- new exception owner/authority;
- exclusive/shared;
- exact allowed symbols;
- exact allowed concern;
- explicitly forbidden adjacent concerns.

Every implementation path required by the exposure chain must appear.

No wildcard grants.

No directory-level grants.

No “related files as necessary”.

---

# Phase 11 — Test Ownership Matrix

For each authorized test path specify:

- project/path;
- WP07 semantic-exposure ownership;
- exact contract tested;
- whether new or shared;
- forbidden WP08/WP09 concerns.

Do not authorize generic test infrastructure changes unless proven necessary.

---

# Phase 12 — Stop Rule for Later Implementation

The future Terra implementation must stop if it requires:

- any production path absent from this amendment;
- any test path absent from this amendment;
- changing source persistence outcomes;
- changing validation behavior;
- schema/persistence changes;
- new contract version;
- WP04 state/revision changes;
- WP05 retry/cache/lifecycle changes;
- WP06 chart semantics;
- WP07 rendering;
- WP08/WP09.

This amendment is exhaustive, not illustrative.

---

# Non-Goals

This amendment does not authorize:

- implementation;
- UI sections;
- WP07 presentation labels/order;
- schema;
- persistence behavior;
- providers;
- pipeline stages;
- feature computation;
- Replay redesign;
- Worker lifecycle changes;
- transport redesign;
- package changes;
- WP08;
- WP09;
- GitHub lifecycle changes.

---

# Mutation Policy

Definition/documentation only.

If governance permits the amendment artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

No production/test/GitHub mutation.

If that documentation path itself is not authorized, return the normative amendment in chat and make zero repository mutations.

---

# Required Completion Report

## Binding semantic authority

Confirm the canonical definition artifact and exact two value domains.

## Verified exposure chain

List exact source → Application → WP04 → publisher → WP05 parser → WP06 symbols.

## Production path amendment

List every exact path and symbol-level exception.

## Test path amendment

List every exact dedicated test path.

## Reserved presentation path

State the later WP07 presentation test path remains separate.

## Forbidden boundaries

Confirm WP04/WP05/WP06 general ownership remains intact and WP08/WP09 remain untouched.

## Mutation proof

If one documentation artifact is created:

`WP07 CANONICAL SEMANTIC-EXPOSURE MANIFEST/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP07 CANONICAL SEMANTIC-EXPOSURE MANIFEST/PATH AMENDMENT MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP07 CANONICAL SEMANTIC-EXPOSURE PATH AUTHORITY AMENDED — EXPOSURE IMPLEMENTATION REQUIRES FRESH AUTHORITY`

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Do not emit success unless every required Application → WP04 → Worker JSON → WP05 → WP06 production path and every required focused test path has an exact, narrow, symbol/concern-level authority.

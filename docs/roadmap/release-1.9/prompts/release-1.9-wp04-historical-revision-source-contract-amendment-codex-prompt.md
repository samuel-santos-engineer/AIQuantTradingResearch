# Release 1.9 — WP04 Historical Revision-Source Contract Amendment — Codex Authority

## Authority

This document grants a **narrow, definition-only contract amendment** for Release 1.9 WP04, canonical GitHub issue **#229**.

WP04 implementation is blocked before mutation because the fixed presentation read-model contract defines envelope revision as:

> source logical tick + snapshot identity

That rule is directly valid for Replay because WP02 exposes logical ticks.

It is not directly valid for Historical mode because the current Historical pipeline path exposes no source logical tick through:

- `PipelineExecutionResult`
- `PipelineExecutionEvidence`
- historical observation contracts
- historical persistence contracts

Assigning a synthetic Historical tick would violate the fixed contract.

Extending predecessor pipeline/evidence contracts without authority would also be ungoverned.

Current proven state:

- no WP04 code mutation occurred
- no schema mutation occurred
- no test mutation occurred
- no GitHub/Project mutation occurred
- #229 remains Open / Backlog
- WP05 remains unstarted
- schema v4 / 290-test predecessor baseline remains preserved

This authority exists only to define a truthful deterministic Historical revision source and any minimum contract exposure required.

It does **not** authorize implementation.

It does **not** authorize WP05.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Define one unambiguous Historical-mode revision contract that is compatible with the already-fixed WP04 read-model semantics without pretending Historical has Replay logical ticks.

The contract must specify:

1. the authoritative Historical revision source;
2. whether that source already exists in snapshot/materialization/persistence metadata;
3. whether a minimum predecessor contract exposure is required;
4. how Historical revisions compare deterministically;
5. how snapshot identity participates;
6. how equal/older/conflicting Historical publications are handled;
7. how Historical and Replay revision semantics coexist under one envelope;
8. session/reset semantics;
9. required future tests;
10. explicit non-goals.

---

# Fixed WP04 Contract Context

Do not reopen these decisions except where this amendment explicitly replaces the Historical revision-source rule.

## Read-model

Model C:

- immutable versioned snapshot
- bounded 64-row accumulated window
- single-writer atomic publication
- mutually exclusive Ready / Empty / WarmUp / Stale / Failed states

## Contract version

`aiq-visualization-read-model-v1`

## Replay revision semantics

Replay remains:

- primary revision source = WP02 source logical tick
- snapshot identity = deterministic tie-breaker
- higher logical tick = newer
- equal logical tick + equal snapshot identity = equivalent
- equal logical tick + different snapshot identity = integrity conflict
- lower logical tick = older/stale

Do not alter Replay revision semantics.

## Session semantics

A new Worker session begins a fresh in-memory publication sequence and does not claim continuity with a prior session.

---

# Permitted Scope

This authority may read:

- #229
- Release 1.9 WP04/WP05 manifest
- `PipelineExecutionResult`
- `PipelineExecutionEvidence`
- dataset snapshot types
- materialization result types
- snapshot persistence models
- experiment-result persistence models
- catalog identity/version fields
- historical observation metadata
- schema v4 snapshot/result keys
- existing deterministic IDs, sequence numbers, revisions, timestamps, or content hashes
- tests that establish snapshot identity/version ordering

It may define one Historical revision contract.

If governance permits one WP04-owned definition artifact, create only that artifact.

Otherwise return the normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify production code
- modify tests
- modify schema
- add synthetic Historical logical ticks
- reuse Replay logical ticks for Historical
- redefine Replay revision semantics
- alter the 64-row window contract
- alter state semantics
- implement WP05
- modify GitHub
- close #229
- change Project state
- alter planning/dependencies

This is definition-only authority.

---

# Core Design Principle

Historical revision must be derived from **truthful existing state or minimally exposed truthful metadata**.

Preferred order:

1. existing monotonic snapshot/materialization version;
2. existing deterministic snapshot sequence;
3. existing persisted version/revision field;
4. stable immutable snapshot identity with an existing ordering source;
5. minimum new predecessor contract exposure, if none of the above exists.

Do not invent ordering where repository semantics do not provide one.

---

# Phase 0 — Inventory Existing Historical Identity/Version Sources

Before defining the contract:

1. Read #229 completely.
2. Read WP04/WP05 definitions.
3. Inspect `PipelineExecutionResult`.
4. Inspect `PipelineExecutionEvidence`.
5. Inspect dataset snapshot model and persistence schema.
6. Inspect experiment-result model.
7. Inspect catalog records.
8. Identify all fields that could truthfully represent:
   - snapshot identity
   - snapshot version
   - materialization revision
   - persisted sequence
   - deterministic creation order
   - immutable content identity
9. Determine whether any such field is monotonic within a Worker session.
10. Determine whether ordering is guaranteed or merely incidental.

Do not mutate anything.

---

# Phase 1 — Candidate Historical Revision Models

Evaluate at minimum:

## Model A — Existing monotonic snapshot/materialization version

Use an already-governed monotonic version/revision if one exists.

Assess:
- truthfulness
- deterministic ordering
- availability at WP04 producer boundary
- stability within session
- compatibility with snapshot identity tie-breaker

## Model B — Existing persisted sequence/identity ordering

Use an existing persisted sequence or ordered key.

Assess:
- whether order is semantically guaranteed
- whether it is available before/at publication
- whether it remains truthful for Historical

## Model C — Snapshot identity only

Use immutable snapshot identity as revision identity without a separate monotonic component.

Assess:
- whether "newer" can be determined
- whether stale/older semantics can be implemented deterministically
- whether this weakens the fixed read-model contract

Use only if repository semantics make identity orderable.

## Model D — Minimum predecessor contract exposure

Expose an already-existing truthful historical materialization/snapshot version through `PipelineExecutionResult` or `PipelineExecutionEvidence`.

Use only if the version exists internally but is not currently surfaced.

This model may define the minimum contract exposure needed for later implementation.

## Model E — Another narrowly evidenced model

Allowed only if clearly supported by repository semantics.

### Hard stop

If no candidate provides truthful deterministic ordering, stop and report that a new historical revision primitive must be normatively introduced under separate authority.

Do not synthesize one.

---

# Phase 2 — Define Historical Revision Type

For the selected model, define the exact conceptual revision structure.

Potential forms:

- `(historicalVersion, snapshotIdentity)`
- `(materializationRevision, snapshotIdentity)`
- another existing ordered source plus snapshot identity

Specify:

- field names/concepts
- data types
- source ownership
- monotonicity guarantee
- comparison semantics
- availability at publication time

Do not force Historical into a field named "logicalTick" if that would be semantically false.

If the common envelope needs a generalized revision type, define the minimum representation required.

---

# Phase 3 — Unify Historical and Replay Under One Envelope

Define how one versioned envelope represents both modes truthfully.

Evaluate a minimal tagged revision model, for example conceptually:

- Replay revision = `{ kind: ReplayTick, primary: logicalTick, snapshotIdentity }`
- Historical revision = `{ kind: HistoricalSnapshotVersion, primary: historicalVersion, snapshotIdentity }`

Do not preselect names unless repository conventions support them.

The unified contract must allow consumers to:

- compare revisions within the same source mode/session
- detect equal publications
- detect conflicts
- reject/ignore older publications

Do **not** require ordering between Historical and Replay revisions across different source modes unless #229 explicitly requires cross-mode comparison.

Prefer mode-local ordering.

---

# Phase 4 — Define Comparison Semantics

For Historical, define exactly:

1. higher primary historical revision = newer
2. equal primary revision + equal snapshot identity = equivalent/idempotent
3. equal primary revision + different snapshot identity = integrity conflict
4. lower primary revision = older/stale

If repository evidence requires different semantics, state them explicitly.

Do not use timestamps as ordering unless repository guarantees monotonic deterministic semantics.

---

# Phase 5 — Define Stale Semantics for Historical

Historical stale must remain structural, not based on invented wall-clock timeout.

Define stale as:

- attempted publication has older Historical revision than current; or
- producer explicitly reports no newer accepted Historical revision according to existing semantics, if such signal exists.

Do not invent a time threshold.

Clarify whether "no new historical revision yet" alone creates Stale or simply leaves current state unchanged.

Prefer not to create Stale unless the producer has an explicit structural basis.

---

# Phase 6 — Snapshot Identity Role

Define exact role of snapshot identity.

It must be:

- immutable
- deterministic
- available at publication
- used as equality/conflict tie-breaker

Specify whether snapshot identity alone is sufficient to identify equivalence.

If snapshot identity can change while semantic revision remains equal, equal-primary/different-identity must remain conflict unless repository semantics prove otherwise.

---

# Phase 7 — Minimum Predecessor Contract Exposure

If the selected truthful Historical revision source exists internally but is not exposed to WP04, define the **minimum** contract change required later.

Potentially permitted future exposure:

- add read-only historical/materialization revision to `PipelineExecutionEvidence`
- add read-only snapshot version to `PipelineExecutionResult`
- expose existing dataset snapshot version/identity without changing pipeline behavior

The definition must specify:

- exact source field
- exact target contract
- why exposure is necessary
- why it is behavior-preserving
- which predecessor tests must guard compatibility

Do not authorize the implementation here.

---

# Phase 8 — Session and Reset Semantics

Define:

- revision ordering scope = current Worker session
- restart does not claim continuity unless the underlying Historical version source itself persists and repository semantics guarantee continuity
- if persistent version exists, state whether WP04 may preserve continuity across sessions
- otherwise reset read-model publication state on Worker restart

Do not invent cross-session continuity.

---

# Phase 9 — Consumer Contract

WP05 must not need to know internal producer mechanics.

Define what WP05 receives:

- source mode
- revision kind/type if required
- primary revision value
- snapshot identity
- comparison result semantics

WP05 must not:

- generate revisions
- normalize identities
- infer Historical tick
- compare Historical revision against Replay revision unless explicitly allowed
- mutate producer revision state

---

# Phase 10 — Required Future Tests

Define tests for later implementation.

At minimum:

## Historical revision source
- truthful revision source exposed
- deterministic ordering
- no synthetic tick

## Comparison
- higher Historical revision replaces
- lower revision ignored/rejected
- equal revision + same identity idempotent
- equal revision + different identity conflict

## Replay preservation
- existing Replay logical-tick semantics unchanged

## Unified envelope
- Historical revision represented truthfully
- Replay revision represented truthfully
- consumer can distinguish revision kind/mode
- no invalid cross-mode ordering assumption

## Session behavior
- restart/reset semantics exact
- no false continuity

## Predecessor compatibility
- pipeline behavior unchanged
- schema v4 unchanged
- WP02/WP03 tests remain passing

Do not implement tests here.

---

# Non-Goals

This amendment does not authorize:

- new domain ordering semantics
- synthetic Historical logical ticks
- timestamp-based revision invention
- cross-mode total ordering
- Streamlit implementation
- schema changes
- persistence redesign
- pipeline algorithm changes
- WP05 work
- GitHub lifecycle mutation

---

# Stop Conditions

Stop if:

- no truthful Historical revision/version source exists
- existing identity/version fields are not deterministically ordered
- using a candidate would require redefining persistence/domain semantics
- a new revision primitive would need to be invented
- cross-mode ordering is required but undefined
- multiple materially different truthful revision sources remain equally valid

On stop:

- make zero production/test/GitHub changes
- report exact unresolved revision-source choice
- identify minimum additional governance authority required

---

# Success Criteria

This amendment succeeds only when one unambiguous Historical revision contract is established that specifies:

- truthful primary Historical revision source
- snapshot identity tie-breaker
- deterministic comparison
- stale/older behavior
- equal/conflict behavior
- mode-local ordering semantics
- session/reset behavior
- minimum predecessor contract exposure, if required
- WP05 consumer semantics
- required future tests
- explicit non-goals

No implementation occurs.

No GitHub mutation occurs.

WP05 remains unstarted.

---

# Required Completion Report

Return:

## Selected Historical revision source
- exact source
- why truthful
- why deterministic
- availability at WP04 boundary

## Historical revision structure
- primary revision
- snapshot identity
- data types/concepts

## Comparison semantics
- newer
- equivalent
- conflict
- older/stale

## Unified envelope semantics
- Historical revision representation
- Replay revision representation
- whether cross-mode ordering exists

## Minimum predecessor exposure
State:
- none required; or
- exact field/type exposure required later

## Session/reset behavior
Exact rules.

## Required future tests
List exact scenarios.

## Non-goals
List exclusions.

## Mutation proof

Expected:

`WP04 HISTORICAL REVISION-SOURCE DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP04 HISTORICAL REVISION-SOURCE CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 HISTORICAL REVISION-SOURCE DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL REVISION-SOURCE DEFINITION BLOCKED`

Emit success only if the Historical revision contract is fully unambiguous.

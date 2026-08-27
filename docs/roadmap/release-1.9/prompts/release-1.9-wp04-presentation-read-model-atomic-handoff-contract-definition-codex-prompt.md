# Release 1.9 — WP04 Presentation Read-Model / Atomic-Handoff Contract Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only authority** for Release 1.9 WP04, canonical GitHub issue **#229**.

WP04 is blocked before mutation because #229 requires a bounded versioned presentation read model and atomic handoff, but the accepted Release 1.9 definition does not fix several material contract choices.

Proven unresolved choices:

- accumulated bounded snapshot/window versus per-increment state;
- handoff representation and versioning;
- atomic replacement semantics;
- retention rules;
- stale-state encoding;
- empty-state encoding;
- pipeline-failure encoding;
- feature-warm-up / not-yet-available encoding.

Multiple valid choices would create incompatible Application and later Streamlit/WP05 contracts.

Current proven state:

- no WP04 repository mutation occurred;
- no WP04 GitHub mutation occurred;
- #229 remains Open / Backlog;
- WP05 has not started;
- schema v4 remains the accepted baseline;
- full regression predecessor baseline remains 290/290;
- completed WP01–WP03 state is preserved.

This authority exists only to define the missing **normative presentation read-model and atomic-handoff contract**.

It does **not** authorize implementation.

It does **not** authorize WP05.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Produce one explicit, minimal, versioned presentation contract that #229 can implement without inventing semantics.

The definition must specify:

1. whether the read model represents:
   - latest increment only;
   - accumulated bounded window;
   - another narrowly justified representation;
2. exact bounded-window semantics, if applicable;
3. exact handoff envelope shape;
4. version identifier semantics;
5. atomic publish/replacement behavior;
6. retention behavior;
7. ordering guarantees;
8. stale-state representation;
9. empty-state representation;
10. pipeline-failure representation;
11. feature-warm-up / not-yet-available representation;
12. whether states are mutually exclusive or composable;
13. producer ownership;
14. consumer ownership;
15. how WP05/Streamlit may consume the contract without redefining it;
16. concurrency/thread-safety expectations;
17. required implementation tests;
18. explicit non-goals.

The contract must be specific enough for a later Terra implementation authority to say:

> Implement exactly this WP04 presentation contract.

---

# Fixed Predecessor Context

Do not reopen these predecessor decisions.

## Schema / provenance

- SQLite schema v4;
- historical authority = 0;
- Replay authority = 1;
- truthful Replay persistence is implemented;
- schema v4 predecessor behavior remains fixed.

## Worker / replay

- WP02 Replay semantics complete;
- WP03 Worker Replay production flow complete;
- canonical five-stage `ExecuteCanonical` pipeline remains shared;
- Replay and Historical remain distinct provenance paths.

## Test baseline

Immediate predecessor full regression:

**290/290 passing**

Do not modify any of this under this definition authority.

---

# Scope

## Permitted

This authority may read:

- #229;
- Release 1.9 WP04/WP05 manifest/definition;
- existing Application presentation/read-model types;
- Worker output/result types;
- current pipeline result models;
- existing snapshot/view-model/cache/handoff patterns;
- Streamlit-facing contracts, if any already exist;
- concurrency primitives already used in the repository;
- serialization/versioning conventions;
- tests that reveal intended UI/presentation semantics;
- bounded collection/window patterns already present.

It may define one normative WP04 read-model/handoff contract.

If governance permits one WP04-owned definition artifact, create only that artifact.

Otherwise return the full normative contract in the completion report.

## Forbidden

Do not:

- modify production code;
- modify tests;
- modify schema;
- modify persistence;
- modify Worker;
- modify Streamlit;
- modify appsettings/config;
- modify package pins/Python/Streamlit version;
- change JSON-over-stdio;
- modify GitHub;
- close #229;
- alter Project state;
- create WP05 implementation or scaffolding;
- alter Release 1.9 planning/dependencies.

This is definition-only authority.

---

# Core Design Principles

## Presentation contract, not domain redesign

The read model must expose already-computed pipeline/persistence state for presentation.

Do not redefine core domain calculations.

## Boundedness

The read model must be bounded in memory/state growth.

No unbounded append-only UI history.

## Atomicity

Consumers must never observe a partially constructed handoff.

## Versionability

The handoff must carry a deterministic version identity so consumers can distinguish newer/older/equal state.

## Explicit non-success states

Stale, empty, pipeline failure, and warm-up/not-ready semantics must be explicit.

Do not encode them through null ambiguity or exception-only behavior when the presentation consumer needs a stable state.

## WP05 boundary

WP04 defines the producer-side read model and handoff.

WP05 may render/consume it, but must not need to reinterpret or invent the underlying state semantics.

---

# Phase 0 — Read Existing Presentation and Result Semantics

Before defining anything:

1. Read #229 completely.
2. Read Release 1.9 WP04 and WP05 definitions.
3. Read pipeline result/output types.
4. Read Worker result/output composition.
5. Read any existing presentation/read-model/snapshot abstractions.
6. Read Streamlit integration points if they already consume backend output.
7. Read concurrency/cache primitives used elsewhere.
8. Read tests involving stale, empty, failure, warm-up, or UI readiness semantics.
9. Identify any existing semantic version/revision/tick identifiers usable for the handoff.

Do not mutate anything.

---

# Phase 1 — Decide Read-Model Granularity

Evaluate at minimum:

## Model A — Latest increment only

The presentation model contains only the latest completed increment/result.

Assess:
- whether #229's "bounded" requirement is trivially satisfied;
- whether WP05 needs historical/window context;
- whether feature warm-up/rolling indicators can be represented adequately.

## Model B — Bounded accumulated window

The presentation model contains a bounded ordered window of recent presentation rows/items.

Assess:
- exact capacity;
- append/replace semantics;
- behavior when capacity is exceeded;
- whether window state is needed for Streamlit rendering or feature context.

## Model C — Snapshot + bounded window

A versioned top-level snapshot includes current status plus a bounded recent window.

Assess:
- whether this is the minimum contract satisfying both state/status and recent-data rendering.

## Model D — Other narrowly evidenced representation

Allowed only if repository/WP04 semantics clearly require it.

Select one only if clearly supported.

### Hard stop

If two materially different models remain equally valid after reading #229/WP05, stop rather than guessing.

---

# Phase 2 — Define Boundedness and Retention

If the selected model includes a window/history, define exactly:

- capacity source:
  - fixed constant;
  - configuration;
  - #229-defined count;
  - other governed source;
- ordering:
  - oldest→newest or newest→oldest;
- insertion semantics;
- duplicate-key semantics;
- replacement/upsert semantics, if any;
- eviction rule when capacity is exceeded;
- whether retention is count-bounded, time-bounded, or both;
- behavior on restart/rebuild;
- whether retention spans Worker process lifetime only or persisted state.

Do not invent persistent UI history unless #229 requires it.

Prefer the smallest bounded state necessary for presentation.

---

# Phase 3 — Define Handoff Envelope

Define one top-level immutable/versioned handoff envelope.

Specify exact conceptual fields, including at minimum where required:

- contract/schema version;
- handoff/revision version;
- generated/published timestamp if needed;
- source mode/provenance if presentation needs it;
- target/dataset identity if needed;
- current presentation state/status;
- bounded payload/window;
- error/failure payload if applicable;
- warm-up metadata if applicable;
- staleness metadata if applicable.

Do not add fields not required by #229/WP05.

The envelope must be stable enough for WP05 consumption.

---

# Phase 4 — Define Version Semantics

Define exactly what "version" means.

Evaluate likely candidates:

- monotonically increasing publish revision;
- logical replay tick;
- dataset snapshot ID;
- pipeline result identity;
- timestamp;
- compound version.

The chosen version must allow consumers to determine:

- equal state;
- newer state;
- older/stale state;
- atomic replacement ordering.

Do not use wall-clock timestamp alone if it cannot guarantee deterministic ordering.

If a publish revision is chosen, define its scope and reset behavior.

---

# Phase 5 — Define Atomic Handoff Semantics

Define exact publish/replace behavior.

At minimum answer:

- producer builds new model off to the side, then publishes atomically;
- consumer sees either old complete snapshot or new complete snapshot, never partial;
- whether handoff uses immutable object replacement;
- whether a lock/interlocked/volatile/atomic-reference pattern is required;
- whether multiple producers are permitted;
- how concurrent reads behave;
- how stale/out-of-order publish attempts are handled;
- whether equal-version publication is idempotent, rejected, or replaces.

Prefer single-writer/multi-reader if consistent with Worker architecture.

Do not invent complex synchronization beyond repository needs.

---

# Phase 6 — Define State Machine

Define explicit presentation states.

At minimum resolve:

## Ready

Valid current presentation payload available.

## Empty

No observations/results are available, but no pipeline failure occurred.

Define whether Empty is:
- a state with empty payload;
- equivalent to Ready + zero rows;
- separate status.

## WarmUp / NotReady

Features/pipeline need more observations before usable output exists.

Define:
- exact state name;
- whether partial rows are exposed;
- whether required observation count / current count is included;
- transition to Ready.

## Stale

A previously valid model exists but is older than the freshness contract.

Define:
- whether stale is determined by version lag, elapsed time, source tick lag, or producer signal;
- whether stale retains last payload;
- whether stale is a status overlay or exclusive state.

## Failed

Pipeline/materialization failure prevents producing a fresh valid model.

Define:
- whether last successful payload is retained;
- error representation;
- whether failure is terminal or recoverable;
- how next successful publish clears it.

Do not collapse these states into generic null/error.

---

# Phase 7 — Define Staleness Semantics

Choose the exact staleness source.

Potential candidates:

- elapsed wall-clock since last successful publish;
- difference between latest source logical tick and published tick;
- explicit producer flag after missed/failed refresh;
- consumer-observed version age.

Determine whether staleness belongs in WP04 producer semantics or WP05 presentation policy.

Prefer producer-defined staleness only if #229 requires it.

If no normative threshold is defined, do not invent a duration.

In that case define a structural stale signal/version relation rather than an arbitrary timeout.

---

# Phase 8 — Define Failure Representation

Define a minimal stable failure payload.

Specify whether it includes:

- error category/code;
- human-safe message;
- internal exception details excluded;
- failed revision/tick;
- timestamp;
- recoverability flag.

Do not expose stack traces or raw exception internals to presentation contracts unless repository convention explicitly requires it.

Distinguish:

- pipeline failure;
- validation/configuration failure;
- warm-up/not-ready;
- empty result.

---

# Phase 9 — Define Warm-Up Semantics

Determine what "feature warm-up" means in current pipeline semantics.

Define:

- exact trigger;
- whether output is absent or partial;
- whether current observation count is exposed;
- whether required minimum count is exposed;
- whether warm-up preserves prior Ready payload;
- transition rule from WarmUp to Ready;
- whether returning to WarmUp after Ready is permitted.

Do not invent feature-specific thresholds; use existing pipeline/feature metadata if available.

---

# Phase 10 — Producer / Consumer Ownership Boundary

Define WP04 responsibilities:

- build presentation model;
- assign version;
- ensure boundedness;
- ensure atomic publish;
- encode state/status;
- expose a read API / handoff abstraction.

Define WP05 responsibilities:

- read latest complete handoff;
- render state faithfully;
- optionally poll/refresh;
- never recompute pipeline semantics;
- never infer missing status from payload shape;
- never mutate producer state.

State explicitly which formatting concerns remain WP05-only.

---

# Phase 11 — Serialization / Compatibility Contract

If the handoff crosses a process/JSON boundary, define serialization compatibility.

If it remains in-process for WP04/WP05, state that clearly.

Define:

- contract version field;
- backward/forward compatibility expectations;
- unknown contract version behavior;
- field-addition expectations.

Do not create a network/API versioning framework unless actually needed.

---

# Phase 12 — Required Future Tests

Define the implementation test contract.

At minimum include:

## Boundedness
- capacity never exceeded;
- deterministic eviction;
- ordering preserved.

## Versioning
- versions increase correctly;
- older publish rejected/ignored according to contract;
- equal-version behavior defined;
- consumers can compare versions.

## Atomicity
- concurrent readers observe only complete old/new snapshots;
- no partial payload;
- single publish appears atomically.

## Empty
- empty state encoded exactly;
- no false failure.

## Warm-up
- exact warm-up state;
- counts/metadata if defined;
- transition to Ready.

## Stale
- stale semantics as defined;
- last payload retention if defined.

## Failure
- failure state encoded;
- last-good retention/clearing behavior as defined;
- recovery on next successful publish.

## Producer/consumer boundary
- WP05-facing read API cannot mutate state;
- consumer receives versioned immutable snapshot.

Do not implement tests here.

---

# Phase 13 — Normative Contract Output

Produce one complete contract containing:

1. selected read-model granularity;
2. boundedness/retention;
3. handoff envelope;
4. version semantics;
5. atomic publish semantics;
6. state machine;
7. staleness;
8. failure representation;
9. warm-up semantics;
10. producer/consumer ownership;
11. serialization/compatibility;
12. required implementation tests;
13. non-goals.

Every material choice must include a concise rationale.

---

# Non-Goals

The definition must not authorize:

- Streamlit UI implementation;
- chart/table design;
- visual styling;
- polling interval invention;
- persistence/schema changes unless #229 explicitly requires them later;
- pipeline algorithm changes;
- WP05 logic;
- new network APIs;
- generalized event streaming;
- unbounded history;
- multi-writer distributed coordination.

---

# Stop Conditions

Stop immediately if:

- #229/WP05 cannot clarify whether latest-only or bounded-window semantics are required;
- no deterministic version source can be chosen without broader architecture changes;
- staleness threshold would require inventing a product policy;
- warm-up semantics are unavailable from pipeline/feature metadata;
- failure representation materially conflicts with existing error contracts;
- multiple materially different handoff models remain equally valid.

On stop:

- make zero production/test/GitHub changes;
- report exact unresolved contract choice;
- identify the minimum additional governance authority required.

---

# Success Criteria

This definition authority succeeds only when one unambiguous WP04 presentation read-model / atomic-handoff contract is established that specifies:

- read-model granularity;
- bounded capacity/retention;
- handoff shape;
- contract version;
- publish version semantics;
- atomic replacement semantics;
- ordering;
- Empty;
- WarmUp/NotReady;
- Stale;
- Failed;
- Ready;
- payload-retention behavior per state;
- producer/consumer ownership;
- concurrency expectations;
- required future tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

WP05 remains unstarted.

---

# Required Completion Report

Return:

## Selected read-model model
State exact model and rationale.

## Boundedness
- capacity source;
- ordering;
- insertion/replacement;
- eviction;
- retention scope.

## Handoff envelope
List exact conceptual fields.

## Versioning
- contract version;
- publish/revision version;
- comparison/order semantics;
- reset behavior.

## Atomic publish
- writer model;
- reader guarantees;
- stale/equal publish handling.

## Presentation states
For each:
- Ready;
- Empty;
- WarmUp/NotReady;
- Stale;
- Failed;

define exact payload/status semantics and transitions.

## Producer/consumer boundary
State exact WP04 vs WP05 ownership.

## Required future tests
List exact scenarios.

## Non-goals
List explicit exclusions.

## Mutation proof

Expected:

`WP04 PRESENTATION READ-MODEL/HANDOFF DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP04 PRESENTATION READ-MODEL/HANDOFF CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 PRESENTATION READ-MODEL/HANDOFF DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 PRESENTATION READ-MODEL/HANDOFF DEFINITION BLOCKED`

Emit success only if the contract is fully unambiguous.

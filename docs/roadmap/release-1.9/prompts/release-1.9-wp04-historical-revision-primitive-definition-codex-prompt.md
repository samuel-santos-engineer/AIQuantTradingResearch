# Release 1.9 — WP04 Historical Revision-Primitive Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only authority** for Release 1.9 WP04, canonical GitHub issue **#229**.

WP04 remains blocked because no truthful deterministic Historical primary revision source exists in the current repository.

Proven evidence:

- `DatasetVersion` is only a wrapper around `DatasetSnapshotIdentity`;
- `DatasetVersion` is not monotonic;
- `SourceStateIdentity` is an immutable hash without ordering semantics;
- snapshot identities are immutable hashes without ordering semantics;
- Historical persistence has no sequence/revision field;
- `PipelineExecutionResult` exposes no monotonic Historical revision;
- `PipelineExecutionEvidence` exposes no monotonic Historical revision;
- synthetic market/source ticks are forbidden;
- timestamp ordering is not semantically governed;
- lexical hash ordering is invalid.

Therefore, a new **Historical presentation revision primitive** must be normatively defined before WP04 implementation can proceed.

This authority exists only to define that primitive.

It does **not** authorize implementation.

It does **not** authorize schema changes.

It does **not** authorize persistence redesign.

It does **not** authorize WP05.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Define one minimal, truthful, deterministic Historical revision primitive suitable for the WP04 presentation read model.

The primitive must:

1. be explicitly presentation/publication-oriented, not source/market time;
2. be monotonic within its governed scope;
3. provide deterministic ordering for Historical publications;
4. pair with immutable snapshot identity as equality/conflict tie-breaker;
5. reset under explicitly defined session semantics;
6. coexist with Replay logical-tick revisions without conflating meanings;
7. require the smallest possible new contract exposure;
8. avoid schema/persistence changes unless absolutely required by the definition;
9. avoid introducing a cross-mode total ordering unless #229 requires it;
10. be specific enough for a later Terra implementation authority.

---

# Fixed WP04 Contract Context

Do not reopen these accepted decisions except where this authority explicitly defines the new Historical revision primitive.

## Read model

Model C:

- immutable versioned snapshot
- bounded accumulated window
- capacity = 64
- single-writer atomic publication
- mutually exclusive:
  - Ready
  - Empty
  - WarmUp
  - Stale
  - Failed

## Contract version

`aiq-visualization-read-model-v1`

## Replay revision

Replay remains source-derived:

- primary = WP02 logical tick
- tie-breaker = snapshot identity
- higher logical tick = newer
- equal tick + same snapshot identity = equivalent
- equal tick + different snapshot identity = integrity conflict
- lower tick = older/stale

Do not change Replay semantics.

---

# Core Semantic Distinction

The definition must preserve this distinction:

## Replay

**Source revision**

Represents actual WP02 Replay logical progression.

## Historical

**Presentation/publication revision**

Represents deterministic ordering of accepted Historical presentation publications within a governed scope.

It must **not** claim to represent:

- market time
- source tick
- observation timestamp
- database sequence unless one is explicitly introduced and governed
- snapshot hash order

This distinction must be visible in naming/type semantics.

---

# Permitted Scope

This authority may read:

- #229
- Release 1.9 WP04/WP05 definitions
- Worker lifetime/execution model
- read-model producer lifecycle
- `PipelineExecutionResult`
- `PipelineExecutionEvidence`
- snapshot/materialization identities
- existing session/process identity conventions
- concurrency/publication store design patterns
- tests covering Worker restart/session behavior

It may define one Historical revision primitive and the minimum future contract exposure required.

If governance permits one WP04-owned definition artifact, create only that artifact.

Otherwise return the normative definition in the completion report.

---

# Explicitly Forbidden

Do not:

- modify production code
- modify tests
- modify schema
- add persistent sequence columns
- add synthetic Historical source ticks
- use timestamps as ordered revisions
- order hashes lexically/numerically
- change Replay logical-tick semantics
- alter bounded-window/state semantics
- implement WP05
- modify GitHub
- close #229
- alter planning/dependencies

This is definition-only authority.

---

# Phase 0 — Read Worker and Publication Lifecycle

Before defining the primitive:

1. Read #229.
2. Read WP04/WP05 definitions.
3. Read Worker startup/shutdown/session behavior.
4. Identify where the WP04 producer instance will live.
5. Identify whether there is exactly one producer per Worker process/session.
6. Identify restart behavior.
7. Read existing atomic publication/store patterns.
8. Determine whether Historical publication occurs only after a fully accepted pipeline result.
9. Determine whether failed/empty/warm-up outcomes count as publications.

Do not mutate anything.

---

# Phase 1 — Decide Primitive Scope

Evaluate:

## Model A — Worker-session-local monotonic publication sequence

A single writer increments a counter for each accepted Historical presentation publication.

Properties:
- starts from a defined initial value per Worker/read-model session;
- never claims cross-session continuity;
- ordering is deterministic within session;
- no schema change.

## Model B — Materialization-attempt sequence

Counter advances for every Historical materialization attempt, including failure/empty/warm-up if defined.

Assess whether revision should represent:
- successful accepted publication only; or
- every attempted presentation refresh.

## Model C — Another narrowly justified in-memory sequence

Allowed only if repository lifecycle requires it.

### Preferred direction

Prefer Model A unless #229/repository semantics require attempt-level ordering.

The primitive should generally order **published envelopes**, not hidden internal attempts.

### Hard stop

If publication scope itself is ambiguous and materially affects Stale/Failed semantics, stop and report the unresolved decision.

---

# Phase 2 — Define Exact Primitive Semantics

Define:

- exact conceptual name
- exact numeric type
- initial value
- increment rule
- overflow behavior
- ownership
- scope
- reset behavior

Preferred semantic shape, if repository evidence allows:

`HistoricalPresentationRevision`

with:

- unsigned/non-negative monotonic integer
- session-local
- single-writer owned
- incremented exactly once per accepted new Historical envelope publication
- initial publication revision explicitly defined
- no persistence
- reset when Worker/read-model session restarts

Do not adopt these details blindly; confirm against repository conventions.

---

# Phase 3 — Define Publication Counting Rule

Specify exactly which events consume a new Historical revision.

At minimum decide for:

## Ready publication
Usually yes.

## Empty publication
Define whether it is a new observable envelope and therefore consumes a revision.

## WarmUp publication
Define whether it is a new observable envelope and therefore consumes a revision.

## Failed publication
Define whether failure envelopes consume a revision.

## Stale state
Define whether Stale is:
- a new publication with a new revision; or
- a status derived from rejection/no-newer revision without increment.

Prefer a model where revision identifies published envelope instances consistently.

Do not let revision semantics depend on null payload shape.

---

# Phase 4 — Snapshot Identity Tie-Breaker

Define pairing:

`HistoricalRevision = (presentationRevision, snapshotIdentity)`

or an equivalent tagged representation.

Rules must include:

1. higher presentationRevision = newer
2. equal revision + equal snapshot identity = equivalent/idempotent
3. equal revision + different snapshot identity = integrity conflict
4. lower revision = older/stale

For states without a dataset snapshot identity (for example Empty or some Failed cases), define the tie-breaker representation explicitly.

Possible choices:
- absent snapshot identity + state-specific deterministic identity;
- envelope identity derived from accepted stable fields;
- explicit null permitted only when equality semantics remain deterministic.

Do not leave this ambiguous.

---

# Phase 5 — Unified Revision Type

Define how the common WP04 envelope represents both modes without semantic confusion.

Prefer a tagged revision concept such as:

- Replay revision kind:
  - source logical tick
  - snapshot identity
- Historical revision kind:
  - presentation publication sequence
  - snapshot identity/state identity

Define the minimum fields needed.

The consumer must be able to distinguish revision kind.

Do not expose a single generic field falsely named `logicalTick` for Historical.

---

# Phase 6 — Cross-Mode Ordering

Define whether Historical and Replay revisions can be compared to each other.

Preferred rule unless #229 requires otherwise:

> No total ordering exists across different source modes.

Within a Worker session:

- Historical revisions compare only to Historical revisions.
- Replay revisions compare only to Replay revisions.
- a mode change resets/replaces publication context rather than comparing numeric primary values across modes.

Define exact behavior if Worker mode changes during one process lifetime.

If runtime mode is fixed for the Worker session, state that and avoid cross-mode transition semantics.

---

# Phase 7 — Session / Restart Semantics

Define exact Historical reset behavior.

At minimum:

- sequence scope = current WP04 producer / Worker session
- restart resets the Historical presentation revision sequence
- no continuity claim across process restart
- prior-session envelopes are not ordered against new-session envelopes unless the handoff includes a separate session identity

Determine whether a session identity is required.

If consumers may observe persisted file handoff across Worker restart, define whether the envelope must include:

`sessionIdentity`

to prevent new session revision `1` from being compared incorrectly with old session revision `100`.

If needed, define the minimum truthful session identity source.

Prefer an explicit session discriminator over fake continuity.

---

# Phase 8 — Session Identity, If Required

If the handoff can outlive the Worker process/session, define a session identity.

Requirements:

- unique enough for current local handoff semantics
- no ordering semantics
- immutable during session
- changes on restart
- not a credential
- not derived from wall-clock ordering assumptions

Potential representations:
- generated UUID/session token
- another existing Worker-run identity

This authority may define such an identity only if necessary to prevent invalid cross-session revision comparisons.

It must not create distributed coordination semantics.

---

# Phase 9 — Minimum Future Contract Exposure

Define the smallest implementation surface later required.

Likely categories:

- new WP04 revision value type
- optional revision-kind enum/tag
- optional session identity
- publication store counter
- envelope field adjustment
- comparison helper

Avoid modifying predecessor pipeline contracts unless the presentation producer itself can own the Historical counter after receiving pipeline results.

Preferred outcome:

> no predecessor pipeline contract change required; WP04 producer assigns the Historical presentation revision at publication time.

If this is valid, state it explicitly.

If not, define exact minimum predecessor exposure required.

---

# Phase 10 — Stale / Failed Interaction

Define how the new Historical primitive interacts with existing states.

## Stale

Clarify whether stale:
- reuses the last published revision and marks structural stale metadata; or
- consumes a new presentation revision.

Preferred: if no new accepted source result exists, Stale should not impersonate a newer source publication.

## Failed

Clarify whether a Failed envelope:
- consumes a new publication revision because it is a newly published state; or
- references the failed attempt separately while retaining last successful revision.

Choose one coherent model and define:
- `revision`
- `failedRevision` if needed
- last-good payload relation

This must align with the fixed failure payload containing a failed revision.

---

# Phase 11 — Required Future Tests

Define tests for the later implementation authority.

At minimum:

## Historical sequence
- initial revision exact
- monotonic increment
- no skipped/duplicate increments unless contract allows
- reset on new session

## Publication states
- Ready revision behavior
- Empty revision behavior
- WarmUp revision behavior
- Failed revision behavior
- Stale revision behavior

## Comparison
- higher Historical revision newer
- lower older/stale
- equal + same identity idempotent
- equal + different identity conflict

## Session safety
- prior-session envelope not ordered against new-session envelope incorrectly
- session identity changes on restart if required

## Replay preservation
- Replay logical-tick semantics unchanged

## Unified envelope
- revision kind distinguishes Historical from Replay
- no cross-mode numeric comparison

## Atomicity
- single writer increments/publishes atomically
- readers see consistent revision + payload

Do not implement tests here.

---

# Non-Goals

This definition does not authorize:

- Historical source/market tick invention
- persistent revision columns
- schema changes
- database sequences
- timestamp-based ordering
- cross-mode total ordering
- distributed revision coordination
- multi-writer coordination
- Streamlit implementation
- WP05 work
- GitHub lifecycle mutation

---

# Stop Conditions

Stop if:

- Worker/session ownership cannot be established
- more than one producer may legitimately publish Historical state
- revision needs cross-session continuity to satisfy #229
- defining session identity requires broader architecture
- failure/stale counting semantics cannot be resolved from WP04 contract
- multiple materially different minimal revision primitives remain equally valid

On stop:

- make zero production/test/GitHub changes
- report exact unresolved revision primitive choice
- identify minimum additional governance authority required

---

# Success Criteria

This definition authority succeeds only when one unambiguous Historical revision primitive is established that specifies:

- semantic name
- numeric type
- ownership
- session scope
- initial value
- increment rule
- state-publication counting rules
- snapshot/state identity tie-breaker
- comparison semantics
- session reset behavior
- session identity requirement, if any
- unified Historical/Replay revision representation
- no cross-mode false ordering
- minimum future implementation surface
- required future tests
- non-goals

No implementation occurs.

No schema change occurs.

No GitHub mutation occurs.

WP05 remains unstarted.

---

# Required Completion Report

Return:

## Selected primitive
- exact conceptual name
- type
- owner
- scope
- initial value
- increment semantics

## Publication counting
For:
- Ready
- Empty
- WarmUp
- Stale
- Failed

state whether revision increments and why.

## Identity/tie-breaker
- snapshot identity behavior
- state identity behavior when snapshot absent

## Unified revision envelope
- Historical representation
- Replay representation
- revision kind/tag
- cross-mode comparison rule

## Session/reset
- restart behavior
- session identity requirement and exact semantics if needed

## Minimum future implementation surface
List exact categories of code changes a later authority must permit.

## Required future tests
List exact scenarios.

## Mutation proof

Expected:

`WP04 HISTORICAL REVISION-PRIMITIVE DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP04 HISTORICAL REVISION-PRIMITIVE CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 HISTORICAL REVISION-PRIMITIVE DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 HISTORICAL REVISION-PRIMITIVE DEFINITION BLOCKED`

Emit success only if the primitive is fully unambiguous.

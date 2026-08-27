# Release 1.9 — WP04 Implementation/Completion — Codex Authority

## Authority

This document grants fresh execution authority for:

**Release 1.9 WP04 — canonical GitHub issue #229**

This authority begins from a fixed normative presentation read-model / atomic-handoff contract.

The contract is authoritative input and must not be redesigned.

This authority is for **WP04 only**.

It does not authorize WP05 or later work.

---

# Accepted Current State

Proven predecessor state:

- WP01 #226: Closed / Done
- WP02 #227: Closed / Done
- WP03 #228: Closed / Done
- WP04 #229: Open / Backlog
- WP05 #230 and later WPs remain unstarted
- SQLite schema baseline: **v4**
- historical source authority: `0`
- Replay source authority: `1`
- canonical five-stage `ExecuteCanonical` pipeline preserved
- completed WP02 Replay semantics preserved
- completed WP03 Worker Replay composition and persistence preserved
- full regression predecessor baseline: **290/290 passed**
- build predecessor baseline: **0 errors / 0 warnings**
- milestone #58 remains open
- canonical milestone state: **9 open / 3 closed**
- #230–#237 remain open
- dependency chain remains intact
- #225 and protected milestones remain preserved

The fixed WP04 read-model/handoff contract was defined with **zero mutations**.

---

# Fixed Normative WP04 Contract

## Selected model

**Model C — immutable versioned snapshot plus bounded accumulated window**

The read model provides:

- current presentation state
- latest values
- deterministic bounded price/time window
- no unbounded history
- no direct SQLite access by the consumer

## Bounded window

- fixed capacity: **64 presentation rows**
- ordering: **oldest → newest**
- new rows append in source-time order
- duplicate source-tick row replaces existing row
- out-of-order older row is ignored
- exceeding capacity evicts oldest row
- retention is count-bounded
- retention exists only for current Worker/read-model lifetime
- no durable presentation-history persistence

## Handoff envelope

Immutable envelope contains:

- `contractVersion = "aiq-visualization-read-model-v1"`
- `revision`
- `sourceMode`
- `sourceAuthority`
- `target`
- dataset snapshot identity/version when available
- current presentation state
- bounded ordered observation window
- latest observation
- observation count
- feature definition identity
- feature values or warm-up metadata
- pipeline stage/status summary
- validation/quality status
- failure payload when applicable
- stale metadata when applicable

Must not expose:

- arbitrary payloads
- stack traces
- provider data
- credentials
- raw SQLite records

## Revision semantics

`revision` = source logical tick, with snapshot identity as deterministic tie-breaker.

Comparison:

1. higher logical tick = newer
2. equal tick + equal snapshot identity = equivalent
3. equal tick + different snapshot identity = integrity conflict
4. lower revision = stale/older

A new Worker session starts a fresh in-memory publication sequence and does not claim continuity with a prior session.

## Atomic publish

- one Worker-owned producer
- concurrent readers
- immutable snapshots
- producer builds complete envelope off to the side
- publication atomically replaces current reference
- file handoff, when used:
  - write sibling temp file
  - flush
  - close
  - atomically replace destination
- readers see either old complete or new complete snapshot
- partial files never consumer-visible
- older revisions ignored
- equal revision + equivalent identity = idempotent
- equal revision + different identity = integrity conflict
- no multi-producer or distributed coordination support

## Presentation states

States are mutually exclusive.

### Ready

Valid current snapshot exists.

Expose:

- bounded window
- latest observation
- count
- pipeline evidence
- available feature values

### Empty

No observations/accepted result exists and no pipeline failure occurred.

- window empty
- latest observation absent
- not a failure

### WarmUp / NotReady

Lag-1 feature has insufficient observations.

- existing observations may be shown
- feature values unavailable
- current count exposed
- required count = `2`
- transitions to Ready when existing feature contract produces valid value

### Stale

Last complete payload retained, but no newer source revision is available.

- structural/version-based
- no wall-clock timeout
- stale is not inferred from elapsed time

### Failed

Latest refresh/pipeline operation failed.

- last successful payload may be retained
- state = Failed
- safe failure payload only:
  - stable category
  - safe message
  - failed revision
  - recoverability flag
- no raw stack trace
- next successful publication replaces Failed with Ready

Payload retention must be explicit and never inferred from null shape.

## Producer / consumer ownership

WP04 owns:

- read-model construction
- bounded-window enforcement
- state transitions
- revision assignment
- immutable publication
- atomic local handoff
- safe failure encoding
- stale encoding

WP05 owns:

- reading latest complete envelope
- polling/refresh behavior
- truthful rendering
- styling
- charts/tables

WP05 must not:

- recompute features
- access SQLite directly
- call providers
- reinterpret failures
- mutate producer state

## Non-goals

WP04 does not authorize:

- Streamlit UI implementation
- chart design
- polling interval decisions
- schema/persistence changes
- pipeline algorithm changes
- new APIs/services/dependencies/event streams
- unbounded history
- distributed/multi-writer coordination
- WP05 work

---

# Objective

Implement the fixed WP04 presentation read-model and atomic handoff exactly as defined above.

Then:

1. prove all #229 acceptance criteria;
2. prove boundedness/versioning/state semantics;
3. prove atomic publication under concurrency;
4. preserve all predecessor behavior;
5. run full regression from 290/290 baseline;
6. finalize #229 lifecycle only after all gates pass.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before mutation:

1. Read #229 completely.
2. Read Release 1.9 WP04 manifest/definition.
3. Confirm the fixed normative contract above is consistent with repository reality.
4. Read:
   - pipeline result/output types
   - Worker result/output types
   - current Application presentation-related types
   - existing cache/snapshot/handoff patterns
   - file-handoff utilities if any
   - concurrency primitives used elsewhere
5. Record Git state:
   - branch
   - HEAD
   - origin/main
   - ahead/behind
   - staged paths
   - tracked changes
   - relevant untracked inventory
6. Prove no residual partial WP04 implementation exists.
7. Prove #229 is still Open / Backlog and canonical.

Stop if repository reality conflicts materially with the fixed contract.

---

# Phase 1 — Define Exact WP04 Implementation Surface

Map each #229 acceptance criterion and normative contract element to exact files/types.

Identify the minimum required production surface for:

- immutable read-model envelope
- bounded window item/row type
- revision identity/comparison
- presentation state type
- failure payload
- warm-up metadata
- stale metadata
- single-writer publication store/handoff
- optional file handoff if #229 requires it
- Worker integration point

Do not create a generalized presentation framework.

Prefer narrow internal/Application contracts consistent with repository layering.

---

# Phase 2 — Implement Immutable Contract Types

Implement the minimum immutable/versioned types required by the fixed envelope.

Requirements:

- fixed contract version:
  `aiq-visualization-read-model-v1`
- explicit mutually exclusive state representation
- immutable bounded window exposure
- immutable failure/stale/warm-up metadata
- no raw exception/stack trace exposure
- no raw persistence/provider objects
- no arbitrary dictionaries/blobs unless already governed and explicitly required

If records/immutable collections are standard in the repository, use them.

Do not expose mutable internal collections.

---

# Phase 3 — Implement Revision Semantics

Implement revision comparison exactly:

1. higher logical tick wins
2. equal tick + equal snapshot identity = equivalent/idempotent
3. equal tick + different snapshot identity = integrity conflict
4. lower tick = older/stale and must not replace current published state

Define session-local behavior clearly.

Do not invent cross-process monotonic continuity.

Add focused comparison tests.

---

# Phase 4 — Implement Bounded Window

Implement capacity exactly:

**64 rows**

Rules:

- oldest→newest ordering
- append new source-time row
- duplicate source-tick replaces existing row
- older out-of-order row ignored
- capacity overflow evicts oldest
- never exceed 64
- process-lifetime only
- no persistence

Prove deterministic behavior.

Do not silently sort in a way that changes duplicate or stale semantics.

---

# Phase 5 — Implement State Machine

Implement explicit transitions and payload semantics.

## Ready
- valid payload
- feature values available

## Empty
- no observations
- no failure
- empty window/latest

## WarmUp
- insufficient observations for lag-1 feature
- required count = 2
- current count exposed
- observations may remain visible
- no feature value

## Stale
- previous complete payload retained
- structural revision/version condition only
- no invented wall-clock threshold

## Failed
- latest refresh/pipeline failed
- last successful payload may remain
- safe failure metadata only
- next successful publish replaces failure state with Ready

States must remain mutually exclusive.

Do not infer state from null combinations.

---

# Phase 6 — Implement Atomic In-Memory Publication

Implement single-writer / multi-reader atomic publication.

Requirements:

- producer builds entire immutable envelope before publish
- publish replaces current reference atomically
- readers never observe partially constructed state
- older revision ignored
- equal equivalent revision idempotent
- equal conflicting revision rejected
- read API returns immutable snapshot
- consumer cannot mutate producer-owned state

Use the simplest repository-appropriate primitive:

- atomic reference / `Volatile`
- `Interlocked`
- narrow lock

Do not add unnecessary synchronization framework.

---

# Phase 7 — Implement Atomic File Handoff Only If #229 Requires It

If #229 explicitly requires file handoff:

1. serialize complete envelope to temp sibling
2. flush
3. close
4. atomically replace target
5. ensure readers never observe partial destination
6. preserve last valid destination on failed publish where platform semantics permit

Use existing repository serialization/file conventions.

Do not add a new wire protocol.

If #229 does not require file handoff, do not invent one merely because the normative contract described behavior "where used."

---

# Phase 8 — Worker Integration

Integrate the WP04 producer at the minimum Worker/Application boundary required by #229.

The producer may consume completed pipeline results and existing observation/feature metadata.

Requirements:

- no new domain calculation
- no SQLite read path for presentation
- no provider call
- no recomputation of features
- no Streamlit dependency
- preserve Historical and Replay provenance values
- assign revision from source logical tick + snapshot identity semantics
- publish only complete envelopes

Do not alter pipeline algorithms.

---

# Phase 9 — Focused Boundedness Tests

Required:

- capacity exactly 64
- 65th append evicts oldest
- ordering oldest→newest
- duplicate source tick replaces row
- out-of-order older row ignored
- repeated equivalent publish does not duplicate
- window never grows beyond 64

---

# Phase 10 — Focused Revision Tests

Required:

- higher tick replaces
- lower tick rejected/ignored
- equal tick + equal snapshot identity is idempotent
- equal tick + different identity yields integrity conflict
- new session does not claim prior-session continuity
- stale state follows structural revision semantics only

---

# Phase 11 — Focused State Tests

Required:

## Ready
- correct payload
- feature values exposed

## Empty
- empty payload
- no failure

## WarmUp
- count < 2
- required count = 2
- no feature value
- observations retained if contract permits
- transition to Ready at valid feature availability

## Stale
- last complete payload retained
- no wall-clock threshold used

## Failed
- safe category/message/revision/recoverability
- no stack trace
- last-good retention if defined
- next success clears Failed and publishes Ready

---

# Phase 12 — Concurrency / Atomicity Tests

Required:

- concurrent readers see only complete old/new snapshots
- no reader observes partial window or mixed metadata
- atomic reference replacement verified
- older publication cannot overwrite newer
- equal conflicting publication rejected
- read snapshots are immutable from consumer perspective

Use deterministic tests.

Avoid flaky sleep-based concurrency tests when barriers/events can prove ordering.

---

# Phase 13 — File-Handoff Tests, If Applicable

Only if implemented:

- temp file never exposed as destination
- reader sees old or new complete file
- partial writes not visible
- successful replace publishes complete envelope
- failure leaves no corrupted destination
- unknown contract version rejected by consumer-side parser/reader contract if such reader belongs to WP04 scope

Do not implement WP05 rendering.

---

# Phase 14 — Producer / Consumer Boundary Tests

Prove:

- WP04 read API exposes immutable envelope
- no consumer mutation path
- no SQLite/provider dependency in presentation contract
- no Streamlit reference in Application/read-model layer
- feature values are consumed, not recomputed
- failure semantics explicit
- unknown contract version behavior follows the fixed contract/repository convention

If unknown-version behavior was not otherwise fixed by repository convention, prefer fail-fast/reject rather than reinterpret.

---

# Phase 15 — Predecessor Compatibility

Revalidate predecessor-sensitive behavior where touched:

## WP02
- replay identity
- logical ticks
- restart/resume
- duplicates
- cancellation
- bounds
- finite completion

## WP03
- Worker Historical/Replay dispatch
- Dataset boundary
- schema v4
- authority 0/1
- Replay persistence
- canonical `ExecuteCanonical`
- no Replay historical-store misuse

Do not weaken predecessor tests.

---

# Phase 16 — Build and Full Regression

Run the established repository build.

Require:

- 0 errors
- report warnings exactly

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Pre-WP04 baseline:

**290/290 passing**

Capture:

- exact exit status
- passed
- failed
- skipped
- material warnings

A higher count is expected because WP04 adds tests.

An unexplained lower count is a blocker.

---

# Phase 17 — Diff and Scope Audit

Classify every changed file as:

- WP04 immutable read-model contract
- WP04 bounded window
- WP04 revision/version logic
- WP04 state-machine logic
- WP04 atomic publication/handoff
- Worker/Application integration
- WP04 test
- WP04-required documentation/config artifact

Anything else requires explicit #229 justification.

Prove:

- no WP05 implementation
- no Streamlit code
- no schema changes
- no persistence redesign
- no pipeline algorithm changes
- no new dependency/package
- no provider access from presentation contract
- no SQLite access from presentation contract
- no unbounded history
- no multi-writer/distributed coordination
- predecessor architecture preserved

Anything unexplained blocks acceptance.

---

# Phase 18 — Technical Acceptance Gate

Before GitHub mutation, enumerate every #229 acceptance criterion.

For each report:

- criterion
- implementation evidence
- test evidence
- PASS/FAIL

Additionally require PASS for:

- Model C implemented exactly
- capacity 64
- deterministic ordering/eviction
- duplicate replacement
- stale older-row ignore
- contract version exact
- revision comparison exact
- integrity conflict exact
- atomic immutable publication
- Ready
- Empty
- WarmUp
- Stale
- Failed
- producer/consumer boundary
- no WP05 leakage
- concurrency tests
- predecessor tests
- build
- full regression
- scope audit

If any fails, leave #229 Open / Backlog.

---

# Phase 19 — WP04 GitHub Lifecycle Finalization

Only after full technical acceptance:

1. read #229 current state
2. confirm established completion convention
3. add one concise completion/evidence comment if required
4. transition Project Status from Backlog to authoritative completed state
5. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area
6. close #229
7. keep milestone #58 open
8. read back all mutations

Do not modify #230.

---

# Expected Post-Completion State

After success:

- #226 Closed / Done
- #227 Closed / Done
- #228 Closed / Done
- #229 Closed / Done or authoritative completed state
- #230–#237 remain Open and untouched
- milestone #58 remains Open
- canonical milestone counts:
  - **8 open**
  - **4 closed**
- raw GitHub closed count may additionally include #225
- dependency chain remains intact
- successful WP04 regression count becomes WP05 predecessor baseline
- WP05 #230 becomes next eligible
- WP05 remains unstarted

---

# Stop Conditions

Stop immediately if:

- fixed normative contract conflicts with repository reality and requires redesign
- implementation requires changing capacity/version/state semantics
- no deterministic atomic publication mechanism is possible within current architecture
- implementing read model requires WP05/Streamlit code
- implementation requires schema/persistence redesign
- pipeline/domain calculations must be changed
- predecessor semantics regress
- focused WP04 tests fail
- build fails
- full regression fails
- diff audit reveals unexplained scope
- GitHub lifecycle mutation fails or cannot be proven

On stop:

- preserve valid WP04 work
- do not broaden authority
- report exact blocker and last proven state
- leave #229 open unless technical acceptance fully passed and lifecycle mutation alone failed

---

# Success Criteria

WP04 succeeds only when:

- fixed Model C contract implemented exactly
- immutable versioned envelope implemented
- bounded 64-row window implemented
- ordering/replacement/eviction semantics exact
- revision semantics exact
- atomic single-writer/multi-reader publication proven
- Ready/Empty/WarmUp/Stale/Failed exact
- safe failure payload exact
- producer/consumer boundary enforced
- no WP05 implementation
- no SQLite/provider/Streamlit leakage into Application read-model contract
- predecessor behavior preserved
- build passes
- full regression passes
- final diff remains WP04-scoped
- #229 completed and closed
- milestone #58 remains open
- #230–#237 untouched
- dependency chain intact
- WP05 unstarted

---

# Required Completion Report

Return:

## WP04 contract implementation
- read-model types
- envelope shape
- contract version
- window structure
- revision type/comparison
- state representation

## Atomic handoff
- in-memory publication primitive
- reader guarantees
- older/equal/conflict behavior
- file handoff implementation, if applicable

## State semantics
Report evidence for:
- Ready
- Empty
- WarmUp
- Stale
- Failed

## Validation
Report:
- boundedness tests
- revision tests
- state tests
- concurrency tests
- file-handoff tests if applicable
- producer/consumer boundary tests
- predecessor-sensitive suites
- build errors/warnings
- full regression command and exact counts

## Scope proof
- final diff classification
- no WP05/Streamlit implementation
- no schema/persistence redesign
- no pipeline algorithm change
- no new dependency
- no unbounded history
- no unauthorized foundation/planning changes

## GitHub lifecycle
- #229 before/after
- Project Status before/after
- completion comment
- milestone #58 canonical counts
- #230–#237 untouched

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP05 — #230`

Do not authorize or execute WP05.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP04 IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP04 IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical and lifecycle requirement is freshly proven.

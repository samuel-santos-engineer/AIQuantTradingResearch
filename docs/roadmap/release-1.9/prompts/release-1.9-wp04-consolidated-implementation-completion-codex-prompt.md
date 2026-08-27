# Release 1.9 — WP04 Consolidated Implementation/Completion — Codex Authority

## Authority

Execute **Release 1.9 WP04 — canonical issue #229** only.

Accepted predecessor state:
- #226–#228 Closed / Done.
- #229 Open / Backlog.
- WP05 #230 and later work packages unstarted.
- SQLite schema v4.
- Historical authority `0`; Replay authority `1`.
- Canonical five-stage `ExecuteCanonical` pipeline preserved.
- WP02 Replay semantics and WP03 Worker Replay persistence preserved.
- Full regression baseline: **290/290**.
- Build baseline: **0 errors / 0 warnings**.
- Milestone #58 open with canonical **9 open / 3 closed**.

This authority consolidates two fixed normative contracts and supersedes earlier blocked WP04 execution prompts. Do not redesign either contract.

## Fixed Contract A — Model C presentation handoff

Implement an immutable versioned snapshot plus bounded accumulated window.

Window:
- capacity exactly **64** rows;
- oldest → newest;
- append in source-time order;
- duplicate source-tick replaces the existing row;
- older out-of-order row is ignored;
- overflow evicts oldest;
- current Worker/read-model lifetime only;
- no durable presentation-history persistence.

Envelope:
- `contractVersion = "aiq-visualization-read-model-v1"`;
- tagged revision;
- source mode;
- source authority;
- target;
- dataset snapshot identity/version when available;
- mutually exclusive presentation state;
- bounded observation window;
- latest observation and count;
- feature identity;
- feature values or warm-up metadata;
- pipeline/status evidence;
- validation/quality status;
- safe failure payload when applicable;
- stale metadata when applicable.

Never expose arbitrary payloads, stack traces, provider data, credentials, or raw SQLite records.

States are mutually exclusive:
- **Ready** — valid payload and available feature value.
- **Empty** — no observations/result and no failure.
- **WarmUp** — lag-1 feature unavailable; current count exposed; required count = `2`; observations may remain visible.
- **Stale** — last complete payload retained; structural/version based only; no wall-clock timeout.
- **Failed** — latest operation failed; last-good payload may remain; safe category/message/failed revision/recoverability only; next successful publication clears failure.

Atomicity:
- exactly one WP04 producer;
- concurrent readers;
- build complete immutable envelope off to the side;
- atomically replace current reference;
- readers observe either old complete or new complete envelope, never partial state;
- no distributed or multi-writer coordination.

If #229 explicitly requires file handoff, use temp sibling → flush → close → atomic replace. Do not invent file handoff otherwise.

WP04 owns read-model construction, bounds, state transitions, revision assignment, immutable publication, atomic handoff, safe stale/failure encoding.

WP05 owns only later consumption/rendering. WP04 must contain no Streamlit implementation.

## Fixed Contract B — Historical revision primitive

Replay revision remains unchanged:
- `RevisionKind = ReplayLogicalTick`;
- primary value = actual WP02 logical tick;
- snapshot identity tie-breaker;
- existing Replay ordering semantics remain authoritative.

Historical revision:
- `RevisionKind = HistoricalPresentation`;
- primary value type = `HistoricalPresentationRevision`, a non-negative `ulong`;
- owned by the single WP04 producer;
- session-local only;
- first newly published Historical envelope = `1`;
- increment exactly once for each newly published **Ready**, **Empty**, **WarmUp**, or **Failed** Historical envelope;
- **Stale does not increment** when no newer accepted Historical result exists;
- no persistence;
- Worker/read-model restart resets sequence to `1`;
- represents presentation publication order only, never source time, market time, observation time, or database order.

Historical comparison:
1. higher presentation revision = newer;
2. equal revision + equal identity = equivalent/idempotent;
3. equal revision + different identity = integrity conflict;
4. lower revision = older/stale.

For snapshot-backed states, use immutable `DatasetSnapshotIdentity`.
For snapshotless states, use a deterministic tagged state identity derived only from stable state/category fields. Exclude timestamps, exception text, and the revision itself.

No numeric ordering exists between Historical revisions and Replay logical ticks. A mode change replaces/resets publication context rather than comparing the two kinds.

No predecessor pipeline, schema, persistence, or WP02 contract change is authorized or required.

## Objective

Implement #229 exactly against both fixed contracts. Preserve predecessor behavior, prove boundedness/state/version/concurrency semantics, run full regression from 290/290, and finalize #229 only after every technical gate passes.

## Phase 0 — Pre-mutation proof

Before mutation:
1. Read #229 fully.
2. Read the WP04 Release 1.9 manifest/definition.
3. Prove #229 is canonical Open / Backlog with one Project item and expected P1 / Release 1.9 / Area.
4. Prove #226–#228 completed and WP03→WP04 dependency intact.
5. Prove #230–#237 remain open and untouched.
6. Record branch, HEAD, origin/main, ahead/behind, staged/tracked/untracked state.
7. Read pipeline result/evidence, Worker output types, snapshot identity types, existing Application presentation/cache patterns, concurrency primitives, and relevant tests.
8. Prove no residual partial WP04 implementation exists.

Stop if repository reality conflicts materially with either fixed contract.

## Phase 1 — Minimal implementation surface

Map each #229 acceptance criterion to the minimum types/files needed for:
- immutable read-model envelope;
- bounded row/window type;
- tagged revision representation;
- `HistoricalPresentationRevision`;
- deterministic snapshotless state identity;
- revision comparer;
- presentation state and metadata types;
- single-writer publication store;
- minimum Worker/Application producer integration;
- optional file handoff only if #229 requires it.

Do not build a generalized presentation framework.

## Phase 2 — Implement immutable read model

Use repository-standard immutable types/collections.

Requirements:
- exact contract version string;
- explicit state rather than null inference;
- immutable consumer-facing snapshots;
- no raw persistence/provider objects;
- no mutable internal collections exposed.

## Phase 3 — Implement tagged revision

Historical:
- kind `HistoricalPresentation`;
- primary `HistoricalPresentationRevision`.

Replay:
- kind `ReplayLogicalTick`;
- primary existing WP02 logical tick.

Comparison only within same kind/mode. No cross-mode numeric ordering.

## Phase 4 — Implement HistoricalPresentationRevision

- `ulong`;
- first publication = 1;
- increment once for each newly accepted Ready/Empty/WarmUp/Failed publication;
- Stale retains current revision;
- checked overflow; never wrap;
- reset on Worker/read-model restart;
- no synthetic source tick.

Counter increment and envelope publication must remain consistent under the single-writer model.

## Phase 5 — Implement deterministic identity

Snapshot-backed envelope: use DatasetSnapshotIdentity.

Snapshotless state: deterministic tagged identity from stable state/category fields only.

Equal primary:
- same identity => idempotent;
- different identity => integrity conflict.

## Phase 6 — Implement bounded window

Exactly:
- capacity 64;
- oldest→newest;
- duplicate source tick replaces;
- older out-of-order row ignored;
- overflow evicts oldest;
- never persist presentation history.

## Phase 7 — Implement state machine

Implement Ready, Empty, WarmUp, Stale, Failed exactly as fixed above.

No wall-clock stale threshold.
WarmUp required count = 2.
Failed exposes no stack trace or unsafe exception details.

## Phase 8 — Atomic publication

Use the simplest repository-appropriate `Interlocked`, `Volatile`, or narrow-lock pattern.

Prove:
- complete envelope built before publication;
- old-or-new complete snapshot only;
- older same-kind revision cannot replace newer;
- equal equivalent is idempotent;
- equal conflict rejected;
- consumer cannot mutate published state.

## Phase 9 — Producer integration

Integrate at the minimum Worker/Application boundary required by #229.

Rules:
- consume already-computed observations/features/pipeline evidence;
- do not recompute features;
- no SQLite access in presentation contract;
- no provider calls;
- no Streamlit dependency;
- preserve source mode and authority;
- Historical producer assigns publication revision;
- Replay producer uses actual logical tick;
- publish only complete envelopes.

If implementation unexpectedly requires predecessor pipeline/schema/persistence contract changes, stop.

## Phase 10 — Required focused tests

### Boundedness
- 64 capacity exact;
- 65th accepted row evicts oldest;
- deterministic ordering;
- duplicate replacement;
- older row ignored;
- never >64.

### Historical revision
- first = 1;
- Ready/Empty/WarmUp/Failed increment;
- Stale does not increment;
- monotonic;
- overflow fails, never wraps;
- restart resets;
- no synthetic source tick.

### Revision comparison
- higher Historical replaces;
- lower Historical rejected/stale;
- equal + same identity idempotent;
- equal + different identity conflict;
- Replay logical-tick tests unchanged;
- revision kinds distinct;
- no cross-mode ordering.

### State identity
- deterministic for equivalent stable inputs;
- excludes timestamps, exception text, revision;
- changes when relevant stable state/category changes.

### States
- Ready;
- Empty;
- WarmUp count/required=2/transition;
- Stale payload + Historical revision retention;
- Failed safe payload + recovery.

### Atomicity/concurrency
- concurrent readers see complete old/new snapshots only;
- no mixed revision/window/metadata;
- writer revision and envelope consistent;
- immutable reader view;
- deterministic synchronization, not flaky sleeps.

### Producer/consumer boundary
- no SQLite/provider/Streamlit reference in Application presentation contract;
- no feature recomputation;
- WP05 logic absent.

If file handoff is explicitly required, add old-or-new complete file / no partial destination tests.

## Phase 11 — Predecessor compatibility

Revalidate affected predecessor behavior:

WP02:
- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- finite completion.

WP03:
- Historical/Replay dispatch;
- Dataset boundary;
- schema v4;
- authority 0/1;
- Replay persistence;
- canonical ExecuteCanonical;
- no Replay historical-store misuse.

Do not weaken predecessor tests.

## Phase 12 — Build and full regression

Run established build. Require 0 errors and report warning count exactly.

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Pre-WP04 baseline: **290/290 passing**.

Capture exact exit status, passed, failed, skipped, and material warnings.

Higher count is expected from WP04 tests. Unexplained lower count is a blocker.

## Phase 13 — Diff/scope audit

Classify every changed file as:
- WP04 read-model contract;
- tagged revision representation;
- HistoricalPresentationRevision;
- deterministic state identity;
- bounded window;
- state machine;
- atomic publication/handoff;
- Worker/Application producer integration;
- WP04 test;
- explicitly required WP04 docs/config artifact.

Prove:
- no WP05 implementation;
- no Streamlit code;
- no schema change;
- no persistence redesign;
- no pipeline algorithm change;
- no predecessor contract redesign;
- no new dependency;
- no synthetic Historical source tick;
- no cross-mode total ordering;
- no unbounded history;
- no multi-writer/distributed coordination;
- authority/control files preserved.

Anything unexplained blocks acceptance.

## Phase 14 — Technical acceptance gate

Before GitHub mutation, enumerate every #229 acceptance criterion and report implementation evidence, test evidence, PASS/FAIL.

Additionally require PASS for:
- exact Model C implementation;
- capacity 64;
- bounded ordering/replacement/eviction;
- exact contract version;
- tagged Historical/Replay revisions;
- Historical initial revision 1;
- publication counting rules;
- Stale non-increment;
- deterministic state identity;
- idempotence/conflict semantics;
- no cross-mode ordering;
- atomic immutable publication;
- Ready/Empty/WarmUp/Stale/Failed;
- producer/consumer boundary;
- no WP05 leakage;
- concurrency tests;
- predecessor tests;
- build;
- full regression;
- scope audit.

If any fail, leave #229 Open / Backlog.

## Phase 15 — GitHub lifecycle finalization

Only after technical acceptance:
1. Read #229 current state and established completion convention.
2. Add one concise evidence comment if required.
3. Transition Project Status from Backlog to authoritative completed state.
4. Preserve Priority=P1, Release=1.9, authoritative Area.
5. Close #229.
6. Keep milestone #58 open.
7. Read back every mutation.
8. Do not modify #230.

Expected successful state:
- #226–#229 closed/completed;
- #230–#237 remain open and untouched;
- milestone #58 open;
- canonical milestone counts **8 open / 4 closed**;
- raw closed count may additionally include #225;
- dependency chain intact;
- successful WP04 regression count becomes WP05 predecessor baseline;
- WP05 #230 becomes next eligible but remains unstarted.

## Stop conditions

Stop if:
- either fixed contract requires redesign;
- predecessor pipeline/schema/persistence changes become necessary;
- Historical revision needs cross-session continuity;
- more than one producer is needed;
- cross-mode numeric ordering is needed;
- implementation requires WP05/Streamlit;
- focused/predecessor tests fail;
- build/full regression fails;
- diff scope is unexplained;
- GitHub mutation cannot be proven.

Preserve valid WP04 work; do not broaden authority.

## Required completion report

Report:
- exact read-model/envelope types;
- contract version and bounded-window structure;
- HistoricalPresentationRevision implementation;
- Replay revision representation;
- revision kind, identity, comparison, session/reset behavior;
- atomic publication primitive and guarantees;
- Ready/Empty/WarmUp/Stale/Failed evidence;
- exact focused/predecessor/build/full-regression results;
- final diff classification;
- #229 before/after lifecycle state;
- milestone canonical counts;
- confirmation #230–#237 untouched.

On success state:

`NEXT ELIGIBLE WORK PACKAGE: WP05 — #230`

Do not authorize or execute WP05.

## Terminal markers

Success:

`RELEASE 1.9 WP04 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocker:

`RELEASE 1.9 WP04 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Emit success only when every technical and lifecycle requirement is freshly proven.

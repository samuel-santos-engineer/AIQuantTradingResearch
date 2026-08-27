# Release 1.9 — WP05 Consolidated Implementation / Completion v2 — Codex Authority

## Authority

Execute Release 1.9 WP05, canonical issue **#230**, under this fresh consolidated authority.

This authority supersedes the prior blocked WP05 implementation attempt **only as execution authority**. All previously accepted WP05 definitions remain normative.

The prior blocker is resolved by the authorized documentation artifact:

`docs/roadmap/release-1.9/RELEASE_1.9_WP05_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

Read that artifact before any mutation. Its exact allowlist, shared-path exceptions, forbidden paths, stop rules, and scope-audit rules are binding.

## Model

Use **GPT-5.6 Terra**.

---

# Entry State

Expected lifecycle:

- #229: Closed / Done
- #230: Open / Backlog
- #231: Open / Backlog
- WP06+ unstarted
- milestone #58 open
- canonical milestone count: 8 open / 4 closed
- raw GitHub count includes historical duplicate #225 separately
- SQLite schema v4

Accepted predecessor baseline:

- Infrastructure: **155/155**
- Application: **122/122**
- Domain: **11/11**
- Architecture: **13/13**
- Build: **0 errors / 0 warnings**
- Full .NET regression: **301/301**, 0 failed, 0 skipped

Historical production composition is directly proven:

`PipelineExecution → canonical pipeline → HistoricalPresentationInputs → VisualizationReadModelUseCase → AtomicVisualizationReadModelStore`

Focused Historical composition: **4/4**.

Do not disturb this predecessor state.

---

# Normative Authority Stack

Read and obey, in precedence order for this execution:

1. this fresh implementation/completion authority;
2. `RELEASE_1.9_WP05_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`;
3. accepted WP05 runtime-location/lifecycle definition;
4. accepted WP05 refresh-cadence/retry definition;
5. accepted WP04 presentation read-model/atomic-handoff contract;
6. accepted WP04 Historical revision primitive;
7. accepted WP04 Historical presentation-feature contract;
8. accepted WP02/WP03 predecessor contracts;
9. Release 1.9 accepted definition/manifest and #230.

If these appear materially inconsistent, stop before mutation and report the exact conflict.

Do not invent semantics.

---

# Objective

Complete WP05:

`Worker`
→ proven WP04 immutable read model
→ Worker-owned atomic local JSON handoff
→ bounded read-only Python consumer
→ separate Streamlit entry point

for both Historical and Replay.

WP05 owns transport and presentation consumption only.

It does not own new market-data semantics, feature computation, persistence, schema, or WP06 behavior.

---

# Hard Manifest Gate

Before editing any file, classify it against:

`RELEASE_1.9_WP05_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

Every changed/created path must be:

- explicitly WP05-authorized; or
- an explicitly defined narrow shared-path exception.

For shared paths, modify only the symbols/concerns authorized by the amendment.

If implementation requires any non-allowlisted path:

**STOP.**

Do not use a nearby file as a substitute.
Do not broaden ownership.
Do not amend the manifest during this run.

Report the exact missing path and why it is required.

---

# Fixed Transport Contract

Transport is:

**local atomic JSON**

Default canonical file:

`<LocalApplicationData>\AIQuantTradingResearch\Release1.9\runtime\visualization-read-model.json`

Override:

- .NET key: `Visualization:HandoffPath`
- environment: `Visualization__HandoffPath`

Override must be an absolute file path.

No working-directory fallback.

Worker owns:

- path resolution;
- directory creation;
- temp sibling creation;
- write/flush/close;
- atomic replacement;
- owned-temp cleanup;
- startup removal of prior canonical handoff.

Streamlit/Python is read-only.

---

# Fixed Temporary File Contract

Sibling pattern:

`.visualization-read-model.json.<owned-random-suffix>.tmp`

Only Worker-owned matching temp artifacts may be cleaned.

Never delete unrelated siblings.

---

# Fixed Process Lifecycle

Worker and Streamlit are independently launched.

Neither starts/stops the other.

Worker startup removes the prior canonical handoff before first publication.

Graceful shutdown may leave the last valid envelope.

Abrupt termination may leave stale handoff; next Worker startup removes it.

No supervisor or shutdown IPC.

---

# Fixed Refresh Contract

Streamlit owns automatic and manual refresh.

Configuration:

- `Visualization:RefreshIntervalSeconds`
- environment: `Visualization__RefreshIntervalSeconds`
- default: 2
- minimum: 1
- maximum: 60

Missing => default 2.

Malformed explicit value => fail configuration validation.

Out-of-range explicit value => fail configuration validation.

Manual refresh performs one immediate cycle and does not alter cadence.

---

# Fixed Read / Retry Contract

Each refresh cycle:

1. resolve path;
2. existence check;
3. read UTF-8 JSON;
4. validate exact contract version;
5. validate revision/state;
6. compare within compatible revision context;
7. accept only newer valid revision;
8. retain equivalent cached envelope unchanged.

Maximum reads per cycle: **2**.

One retry only.

Retry delay: **50 ms**.

Retry only for transient I/O, disappearance during read, or parse failure attributable to replacement race.

Missing file receives no same-cycle retry.

No background retry thread.
No exponential backoff.
No jitter.
No watcher.

---

# Fixed Contract Version

Exact:

`aiq-visualization-read-model-v1`

Unknown versions are rejected without reinterpretation.

---

# Fixed Consumer Revision Semantics

Newer compatible revision:

- replace last-good.

Equivalent revision + same identity:

- idempotent;
- preserve current cache.

Lower revision:

- ignore;
- preserve last-good.

Equal revision + different identity:

- integrity conflict;
- preserve last-good.

Historical and Replay revision kinds are never numerically compared.

Do not invent cross-mode ordering.

---

# Fixed Last-Good Bound

Cache at most one last-good envelope.

Transport conditions may attach a consumer warning but must not rewrite WP04 backend state.

Missing file:

- ProducerUnavailable / awaiting publication;
- retain one last-good if present.

Transient I/O:

- retain last-good + transport warning.

Corrupt JSON after retry exhaustion:

- retain last-good + safe read-integrity warning.

Unknown version:

- retain last-good + version warning.

Revision conflict:

- retain last-good + integrity warning.

Do not map these to backend Failed or Stale.

---

# Fixed Consumer Boundary

Python/Streamlit must never:

- query SQLite;
- call providers;
- recompute `simple-return-lag-1-v1`;
- reconstruct pipeline observations;
- mutate producer/read-model state;
- persist presentation history;
- reinterpret canonical failures.

It consumes the WP04 envelope only.

---

# Phase 0 — Pre-Mutation Reconciliation

Before mutation:

1. read #230;
2. read #229 only for predecessor evidence;
3. read the manifest/path amendment;
4. read all fixed WP05 definitions;
5. inspect Worker/DI/Program/Python/test layout;
6. record:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged changes;
   - tracked changes;
   - relevant untracked Release 1.9 authority/control artifacts;
7. classify the documentation amendment as accepted authority, not implementation;
8. prove no unauthorized partial WP05 implementation exists;
9. prove #229 Closed/Done, #230 Open/Backlog, #231 Open/Backlog;
10. confirm predecessor baseline where practical.

Do not delete the manifest amendment.

If unexpected implementation residue exists, reconcile only with proven ownership.

---

# Phase 1 — Produce an Allowlist Execution Map

Before coding, list every file you expect to touch.

For each, state:

- exact path;
- amendment allowlist entry;
- exclusive or shared;
- exact authorized concern.

Do not proceed until every intended path is covered.

This map becomes part of the final scope audit.

---

# Phase 2 — Worker Options / Path Resolution

Implement only in amendment-authorized paths.

Required behavior:

- canonical LocalApplicationData default;
- exact Release1.9 runtime directory;
- canonical filename;
- optional absolute override;
- normalize absolute path;
- reject relative override.

No extra configuration keys.

Add focused tests only in authorized test paths.

---

# Phase 3 — Worker Atomic JSON Publisher

Implement the amendment-authorized narrow publisher.

Required:

1. serialize the complete existing WP04 envelope;
2. write UTF-8 JSON to owned sibling temp;
3. flush;
4. close;
5. atomically replace/move canonical destination;
6. clean owned temp after success/failure where safe.

Canonical file must expose either:

- old complete envelope; or
- new complete envelope.

Never partial JSON.

Use existing .NET/runtime primitives; add no package.

---

# Phase 4 — Serialization

Do not redesign WP04.

JSON must truthfully carry the existing envelope including:

- contract version;
- revision kind/value;
- deterministic identity/tie-breaker;
- source mode;
- source authority;
- target;
- snapshot identity/version where available;
- state;
- bounded observation window;
- latest/count;
- feature identity/value or WarmUp metadata;
- pipeline/status/quality;
- safe failure;
- stale metadata.

Do not serialize stack traces, credentials, provider objects, SQLite records, or arbitrary internals.

Use repository JSON conventions.

---

# Phase 5 — Startup Cleanup

At Worker startup, before first new-session publication:

- resolve handoff path;
- create parent directory;
- remove prior canonical file;
- remove only owned matching stale temp siblings.

Preserve unrelated files.

No cross-process session identity is added.

---

# Phase 6 — Store Decoration / Publication Integration

Use only the exact integration surface authorized by the amendment.

Preserve:

`VisualizationReadModelUseCase → AtomicVisualizationReadModelStore`

The file handoff must observe/decorate the existing publication boundary rather than create a second read model or parallel producer.

Both Historical and Replay publications must reach the file handoff.

Do not change WP04 state/revision semantics.

---

# Phase 7 — Program.cs / DI Shared Exception

Modify `Program.cs` and/or the exact DI shared path only within the amendment's narrow exception.

Allowed concern:

- bind/validate WP05 handoff options if defined;
- register WP05 file publisher/decorator;
- perform Worker-owned startup cleanup;
- compose existing WP04 store with the handoff.

Do not move implementation logic into `Program.cs`.

Do not alter unrelated Worker modes, WP02/WP03 composition, or protocol behavior.

---

# Phase 8 — Python Read Model Consumer

Implement only in authorized Python paths.

The consumer must:

- resolve same effective handoff path;
- read UTF-8 JSON;
- validate contract version;
- validate required envelope structure;
- validate revision/state;
- retain at most one last-good;
- distinguish transport warning from backend state.

Use only existing package pins.

No requirements/package mutation.

---

# Phase 9 — Python Configuration

Implement:

- `Visualization__HandoffPath`
- `Visualization__RefreshIntervalSeconds`

Refresh semantics:

- absent => 2;
- 1–60 integer => valid;
- malformed => fail;
- out-of-range => fail.

Path semantics must match .NET.

If an exact platform-default path cannot be derived consistently under the repository/runtime conventions, stop instead of inventing divergent behavior.

---

# Phase 10 — Python Revision Comparator

Implement exactly the accepted semantics.

Test:

- newer;
- equivalent;
- older;
- equal/conflicting identity;
- Historical/Replay separation.

Do not compare cross-kind numeric values.

---

# Phase 11 — Python Bounded Refresh / Retry

One refresh-cycle function.

Maximum reads: 2.

One retry: 50 ms.

Missing file: no same-cycle retry.

No background thread.

Tests must deterministically prove attempt count and retry behavior without depending on real 50 ms wall-clock timing where mocking the sleep boundary is repository-consistent.

---

# Phase 12 — Last-Good Cache

At most one envelope.

Prove:

- newer replaces;
- equivalent retains;
- older retains;
- conflict retains + warning;
- missing retains + ProducerUnavailable;
- transient error retains + warning;
- corrupt retains + warning;
- unknown version retains + warning.

No unbounded error history.

---

# Phase 13 — Streamlit Entry Point

Implement the authorized separate entry point:

`python/presentation/realtime_financial_visualization.py`

Use pinned Streamlit **1.61.1**.

It must:

- consume only `visualization_read_model.py`;
- support automatic bounded refresh;
- support manual refresh;
- render latest complete envelope;
- never launch/stop Worker;
- never query SQLite/provider;
- never recompute features.

Use supported Streamlit APIs available under the exact installed pin.

No dependency additions.

---

# Phase 14 — Rendering

Render backend states truthfully.

Ready:

- target/mode;
- latest;
- count;
- bounded price/time visualization;
- feature identity/value;
- pipeline/quality.

WarmUp:

- observations;
- count;
- required count 2;
- feature not ready.

Empty:

- only genuine backend Empty.

Failed:

- safe backend failure category/message/recoverability.

Stale:

- backend Stale remains Stale.

ProducerUnavailable:

- transport-only state/warning;
- never backend Empty.

Transport warnings must remain visually/logically distinct from backend state.

Do not add WP06 strategy/control UX.

---

# Phase 15 — Bounded Visualization

Use only the envelope's existing maximum-64 ordered observation window.

No history fetch.

No derived indicators.

No feature recomputation.

No missing-point inference.

Minimum chart/table sufficient for #230.

Avoid WP06 styling expansion.

---

# Phase 16 — .NET Tests

Add only amendment-authorized tests.

Required coverage:

- default path;
- absolute override;
- relative rejection;
- directory ownership;
- prior canonical cleanup;
- owned temp cleanup;
- unrelated sibling preservation;
- exact JSON contract version;
- valid serialization;
- atomic old-or-new visibility;
- no partial canonical visibility;
- Historical handoff;
- Replay handoff;
- relevant WP04 states;
- composition through existing store/decorator.

No new test dependency.

---

# Phase 17 — Python Tests

Add only amendment-authorized Python tests.

Required:

- path default/override;
- refresh default/bounds;
- malformed/out-of-range refresh;
- missing file;
- valid envelope;
- corrupt JSON;
- unknown version;
- revision newer/equivalent/older/conflict;
- cross-kind separation;
- exact max two reads;
- one retry;
- 50 ms retry boundary;
- last-good retention;
- one-cache bound;
- transport/backend-state separation;
- manual refresh logic where testable outside Streamlit UI.

Use existing Python test tooling only.

---

# Phase 18 — Historical End-to-End Evidence

Prove the real governed path:

`Historical Worker composition`
→ existing WP04 producer/store
→ real file publisher
→ canonical JSON file
→ real Python consumer
→ accepted rendering input

The production evidence must use the real publisher.

A test that only manually writes fixture JSON is insufficient as the sole evidence.

Preserve authority 0.

Preserve HistoricalPresentationRevision.

---

# Phase 19 — Replay End-to-End Evidence

Prove:

`Replay Worker composition`
→ existing WP04 producer/store
→ real file publisher
→ canonical JSON file
→ real Python consumer
→ accepted rendering input

Preserve:

- authority 1;
- Replay logical tick;
- finite completion;
- WP02 semantics.

No Replay redesign.

---

# Phase 20 — Startup / Session Evidence

Prove:

- stale canonical file removed at Worker startup;
- unrelated files preserved;
- first new publication creates complete handoff;
- Historical revision reset cannot collide with prior-session file because prior file is removed;
- graceful leftover is cleaned next startup.

---

# Phase 21 — Static Boundary Audit

Search/diff prove WP05 Python/Streamlit contains no:

- SQLite import/connection/query;
- provider access;
- simple-return formula;
- pipeline materialization;
- persistence write;
- Worker process control.

Also prove no new transport beyond atomic JSON.

Hard gate.

---

# Phase 22 — Focused Validation

Run all new WP05 .NET tests.

Run all new WP05 Python tests.

Capture exact commands/counts.

All must pass.

---

# Phase 23 — Predecessor Regression

Revalidate:

## WP04
- Historical production composition;
- Replay production composition;
- Ready/Empty/WarmUp/Stale/Failed;
- 64-row bound;
- Historical revision;
- Replay revision;
- atomic/concurrency/recovery.

## WP03
- Historical/Replay modes;
- dataset boundary;
- schema v4;
- authority 0/1;
- Replay persistence;
- canonical five-stage path.

## WP02
- replay identity;
- ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite completion.

No regression.

---

# Phase 24 — Governed .NET Suites

Run definitively:

- Infrastructure — predecessor 155/155;
- Application — 122/122;
- Domain — 11/11;
- Architecture — 13/13.

Counts may increase only due to authorized tests.

Record passed/failed/skipped/total.

---

# Phase 25 — Build

Run the repository-standard build.

Require:

- exit 0;
- 0 errors;
- report warnings exactly.

Predecessor: 0 errors / 0 warnings.

---

# Phase 26 — Python / Streamlit Validation

Run all repository-governed Python checks applicable to the authorized files.

At minimum:

- Python tests;
- syntax/import/compile validation;
- Streamlit entry-point smoke/import validation where supported without launching an uncontrolled process.

If the accepted manifest/roadmap has an exact Python validation command, use it.

Do not claim full acceptance from static import alone; end-to-end evidence is separately required.

---

# Phase 27 — Full .NET Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**301/301**

Require:

- exit 0;
- 0 failed;
- exact passed/failed/skipped/total;
- any increase explained by authorized WP05 tests;
- no unexplained lost tests.

This final count becomes the WP06 predecessor baseline after successful WP05 closure.

---

# Phase 28 — Manifest Scope Audit

This is mandatory.

For **every changed/created path**, report:

- path;
- amendment allowlist entry;
- exclusive/shared status;
- authorized concern;
- proof actual change stayed within concern.

Then prove zero changes to:

- non-allowlisted paths;
- WP06+ files;
- schema/migrations;
- persistence;
- providers;
- pipeline algorithms;
- package pins;
- Python requirements;
- Streamlit pin;
- JSON-over-stdio boundary;
- unrelated planning.

Any mismatch => BLOCKED.

---

# Phase 29 — Technical Acceptance Matrix

Report PASS/FAIL:

- manifest compliance;
- path resolution;
- absolute override validation;
- Worker directory ownership;
- startup cleanup;
- atomic publication;
- old-or-new visibility;
- no partial JSON;
- contract-version correctness;
- Historical handoff;
- Replay handoff;
- Python parser;
- refresh config;
- retry bound;
- last-good bound;
- revision semantics;
- conflict behavior;
- ProducerUnavailable;
- unknown-version rejection;
- transport/backend separation;
- automatic refresh;
- manual refresh;
- truthful Ready;
- truthful WarmUp;
- truthful Empty;
- truthful Failed;
- truthful Stale;
- bounded visualization;
- no SQLite;
- no provider;
- no feature recomputation;
- no producer mutation;
- Historical end-to-end;
- Replay end-to-end;
- WP02 regression;
- WP03 regression;
- WP04 regression;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- Python validation;
- build;
- full regression;
- final scope audit.

Any FAIL => do not close #230.

---

# Phase 30 — GitHub Completion

Only after all technical gates PASS:

1. add concise implementation/evidence comment to #230;
2. set Project #2 Status = Done;
3. preserve existing:
   - Priority P1;
   - Release 1.9;
   - Area;
4. close #230.

Do not alter #229.

Do not alter #231–#237.

Verify after mutation:

- #230 Closed / Done;
- #231 Open / Backlog;
- milestone #58 remains open;
- canonical milestone count: **7 open / 5 closed**;
- raw GitHub closed count remains one higher because historical duplicate #225 is separately counted.

Do not start WP06.

---

# Stop Conditions

Stop immediately if:

- any required file is not in the amendment allowlist;
- a shared path needs modification beyond its narrow exception;
- WP04 envelope redesign is needed;
- a second producer/read-model path is needed;
- Python/.NET path semantics cannot match;
- a new dependency is required;
- schema/persistence/provider/pipeline redesign is required;
- Replay semantics must change;
- WP06 work is required;
- tests need weakening/skipping;
- Historical or Replay end-to-end evidence cannot be proven;
- any predecessor suite fails;
- Python validation fails;
- build fails;
- full regression fails;
- final manifest audit fails.

On blocker:

- preserve valid authorized partial WP05 state;
- do not close #230;
- do not start WP06;
- report exact blocker;
- identify the minimum fresh authority required.

---

# Required Completion Report

## Entry proof
- Git state;
- lifecycle state;
- predecessor baseline.

## Allowlist execution map
Every intended/actual path with amendment ownership.

## Implementation
- path/config;
- atomic publisher;
- startup cleanup;
- store integration;
- Python consumer;
- Streamlit.

## End-to-end evidence
- Historical Worker → file → consumer;
- Replay Worker → file → consumer.

## Boundary proof
- no SQLite;
- no provider;
- no feature recomputation;
- no WP06.

## Validation
- focused .NET tests;
- focused Python tests;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- WP02/WP03/WP04 evidence;
- build;
- Python/Streamlit validation;
- full .NET regression.

## Manifest scope audit
Every changed path mapped to the amendment.

## GitHub lifecycle
- #230 before/after;
- Project Status;
- milestone counts;
- #231 untouched.

## Next eligible work package

On success state exactly:

`NEXT ELIGIBLE WORK PACKAGE: WP06 — #231`

Do not execute WP06.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical gate passes, the manifest audit passes, and #230 is Closed / Done.

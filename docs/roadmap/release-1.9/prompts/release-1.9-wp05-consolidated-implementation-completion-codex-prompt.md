# Release 1.9 — WP05 Consolidated Implementation / Completion — Codex Authority

## Authority

This document grants a **fresh consolidated implementation/completion authority** for Release 1.9 WP05, canonical issue **#230**.

WP05 may now be retried because all required WP04 predecessor production-composition evidence is complete.

Canonical lifecycle state at entry:

- WP04 #229: **Closed / Done**
- WP05 #230: **Open / Backlog**
- WP06 #231 and later: Open / untouched
- milestone #58: Open
- canonical milestone count: **8 open / 4 closed**
- raw GitHub closed count additionally includes historical duplicate #225
- schema: SQLite v4

Fresh accepted predecessor baseline:

- Infrastructure: **155/155 passed**
- Application: **122/122 passed**
- Domain: **11/11 passed**
- Architecture: **13/13 passed**
- Build: **0 errors / 0 warnings**
- Full regression: **301/301 passed**, 0 failed, 0 skipped

WP04 Historical production composition is directly proven by four focused test-only cases:

`PipelineExecution`
→ canonical pipeline
→ `HistoricalPresentationInputs`
→ `VisualizationReadModelUseCase`
→ `AtomicVisualizationReadModelStore`

Ready, WarmUp, genuine Empty, and canonical Failed are proven.

Replay production composition remains governed by the accepted WP02–WP04 predecessor work.

This authority implements WP05 only.

---

# Model Recommendation

Use **GPT-5.6 Terra**.

This pass combines bounded cross-process transport, Python/Streamlit consumer work, lifecycle/configuration wiring, acceptance tests, and GitHub closure. Terra is the preferred implementation model.

---

# WP05 Objective

Implement the separate Streamlit visualization entry point that consumes the WP04 immutable presentation read model through the fixed local atomic-JSON handoff.

The completed production composition must be:

`Worker`
→ WP04 immutable envelope
→ atomic local JSON handoff
→ Streamlit bounded polling consumer
→ truthful presentation

for both:

- Historical
- Replay

WP05 must remain a consumer/presentation layer.

It must not:

- access SQLite;
- call providers;
- recompute features;
- reinterpret backend failures;
- mutate producer state.

---

# Fixed Cross-Process Transport Contract

Transport:

**local atomic JSON file**

Canonical default base:

`Environment.SpecialFolder.LocalApplicationData`

Canonical default directory:

`<LocalApplicationData>\AIQuantTradingResearch\Release1.9\runtime`

Canonical file:

`visualization-read-model.json`

Temporary sibling pattern:

`.visualization-read-model.json.<owned-random-suffix>.tmp`

Paths are normalized to absolute paths.

No working-directory fallback.

---

# Fixed Handoff Path Configuration

Key:

`Visualization:HandoffPath`

Environment mapping:

`Visualization__HandoffPath`

Semantics:

- optional;
- when absent, use canonical default;
- when present, must be a full absolute file path;
- relative values are invalid;
- Worker and Streamlit must resolve the same effective path.

No additional handoff-path configuration keys are authorized.

---

# Fixed Ownership

## Worker

Worker owns:

- path resolution;
- parent-directory creation;
- temporary sibling files;
- atomic replacement of canonical file;
- cleanup of its own temporary artifacts;
- startup removal of prior canonical handoff.

## Streamlit

Streamlit owns:

- path resolution;
- reading only;
- bounded polling;
- bounded retry;
- last-good cache;
- rendering.

Streamlit must never:

- create the runtime directory as transport owner;
- write handoff files;
- replace handoff files;
- delete handoff files;
- signal Worker shutdown.

---

# Fixed Startup / Shutdown Contract

Worker and Streamlit are independently launched.

Neither starts or stops the other.

Worker startup:

- removes prior canonical handoff before first publication;
- prevents cross-session Historical revision-reset confusion.

Worker graceful shutdown:

- may leave last valid envelope.

Worker abrupt termination:

- may leave stale canonical file;
- next Worker startup removes it.

No mutual shutdown signaling.

No supervisor.

---

# Fixed Missing-File Semantics

When the handoff file is missing:

Streamlit exposes transport-local:

`ProducerUnavailable`

or equivalent accepted consumer wording meaning awaiting first publication.

Missing transport must **not** be mapped to WP04 `Empty`.

Streamlit retries only on its next refresh cycle.

Last-good envelope may be retained with a transport warning.

---

# Fixed Refresh Model

Streamlit owns:

- automatic periodic refresh;
- manual refresh.

Manual refresh:

- triggers one immediate refresh cycle;
- does not alter periodic cadence.

---

# Fixed Refresh Configuration

Key:

`Visualization:RefreshIntervalSeconds`

Environment mapping:

`Visualization__RefreshIntervalSeconds`

Default:

`2`

Minimum:

`1`

Maximum:

`60`

Semantics:

- missing key => default 2;
- valid integer 1–60 => accepted;
- malformed => configuration failure;
- out-of-range => configuration failure.

Do not silently default malformed/out-of-range explicit values.

---

# Fixed Refresh Cycle

Each refresh cycle:

1. resolve canonical handoff path;
2. check existence;
3. read UTF-8 JSON;
4. validate contract version:
   `aiq-visualization-read-model-v1`;
5. validate revision/state;
6. compare only within the same revision kind/context;
7. replace cached envelope only for newer valid revision;
8. retain cache unchanged for equivalent revision.

No consumer mutation of producer state.

---

# Fixed Retry Contract

Maximum reads per refresh cycle:

`2`

Retry:

- one retry only;
- fixed delay: **50 ms**.

Retry only for:

- transient I/O;
- disappearance during read;
- parse failure caused by atomic-replace race.

Missing-file check:

- no retry inside the same cycle.

Forbidden:

- exponential backoff;
- jitter;
- background retry thread;
- unbounded retry;
- filesystem watcher.

After retry exhaustion:

- retain last-good if present;
- surface safe transport/read warning;
- wait for next scheduled/manual cycle.

---

# Fixed Revision Semantics

## Equivalent

Same revision + same identity:

- idempotent;
- retain cache;
- no state transition.

## Newer

Newer valid revision:

- replace cache once.

## Older

Older revision:

- ignore;
- preserve cache.

## Conflict

Equal revision + different identity:

- consumer integrity error;
- preserve last-good.

## Cross-kind

Historical and Replay revisions are never numerically compared.

A mode/context change replaces publication context according to accepted WP04 semantics.

Do not invent cross-mode ordering.

---

# Fixed Last-Good Retention

At most one cached last-good envelope.

## Missing file

- expose ProducerUnavailable/transport warning;
- retain last-good if present.

## Transient I/O

- retain last-good;
- transport warning.

## Corrupt JSON after retry exhaustion

- retain last-good;
- safe read-integrity warning.

## Unknown contract version

- reject;
- retain last-good;
- version warning.

## Revision conflict

- retain last-good;
- integrity warning.

These transport conditions must **not** mutate WP04 backend state to Failed or Stale.

---

# Fixed Resource Bounds

- one canonical file;
- Worker-owned bounded temporary siblings;
- at most two reads per refresh cycle;
- one cached last-good envelope;
- no historical file accumulation;
- no unbounded retry/error state;
- no background polling thread beyond the Streamlit refresh mechanism.

---

# Fixed WP04 Consumer Boundary

WP05 reads only the latest complete WP04 envelope.

WP05 must not:

- recompute `simple-return-lag-1-v1`;
- reinterpret canonical validation;
- query SQLite;
- call providers;
- materialize pipeline data;
- mutate revision/state;
- persist presentation history.

Historical and Replay envelopes are already production-proven predecessor outputs.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before mutation:

1. Read #230.
2. Read #229 only for predecessor evidence.
3. Read accepted Release 1.9 WP05 definition/manifest.
4. Read the fixed:
   - WP04 read-model contract;
   - runtime-location/lifecycle contract;
   - refresh-cadence/retry contract.
5. Inspect current Worker publication integration.
6. Inspect current Python/Streamlit project layout and package pins.
7. Record Git:
   - branch;
   - HEAD;
   - origin/main;
   - ahead/behind;
   - staged paths;
   - tracked changes;
   - relevant untracked Release 1.9 authority/control files.
8. Prove:
   - #229 Closed / Done;
   - #230 Open / Backlog;
   - #231–#237 Open / untouched;
   - no partial WP05 implementation exists unless clearly attributable to a prior blocked attempt.
9. Run/confirm predecessor baseline:
   - Infrastructure 155/155;
   - Application 122/122;
   - Domain 11/11;
   - Architecture 13/13;
   - build 0/0;
   - full regression 301/301.

If unexpected partial WP05 code exists, reconcile only if ownership is provable. Do not destroy predecessor work.

---

# Phase 1 — Map Authorized WP05 Files

Read the accepted WP05 manifest/issue and identify exact authorized paths.

Do not create files outside authorized WP05 Worker/configuration/Python/Streamlit/test paths unless the manifest clearly permits them.

Classify intended changes before coding:

- Worker handoff path/configuration;
- Worker JSON serializer/publisher;
- Worker startup cleanup;
- Python handoff configuration;
- Python envelope parser/validator;
- Python refresh/cache/retry logic;
- Streamlit entry point/rendering;
- tests.

If the manifest does not authorize a required path, stop rather than broaden scope.

---

# Phase 2 — Define JSON Serialization Mapping From Existing WP04 Envelope

Do not redesign the envelope.

Serialize the existing immutable WP04 envelope to JSON with the fixed:

`contractVersion = aiq-visualization-read-model-v1`

Preserve:

- revision kind/value;
- deterministic identity/tie-breaker;
- source mode;
- source authority;
- target;
- snapshot identity/version when present;
- state;
- bounded ordered observations;
- latest observation/count;
- feature identity/value or WarmUp metadata;
- pipeline/status;
- validation/quality;
- safe failure payload;
- stale metadata.

Do not expose:

- stack traces;
- credentials;
- provider objects;
- SQLite records;
- arbitrary internal payloads.

Use existing repository JSON conventions if present.

---

# Phase 3 — Worker Handoff Path Resolution

Implement the fixed path resolver.

Requirements:

- `Visualization:HandoffPath`;
- environment override via standard .NET configuration mapping;
- absolute override only;
- canonical LocalApplicationData default;
- normalized absolute path;
- no current-directory fallback.

Tests:

- default path;
- absolute override;
- relative override rejection;
- Worker/consumer path symmetry where feasible.

Do not add extra keys.

---

# Phase 4 — Worker Startup Cleanup

At Worker startup, before new-session publication:

- resolve handoff path;
- create parent directory as Worker owner;
- remove prior canonical handoff if it exists;
- clean only Worker-owned temp siblings when governed by the fixed pattern.

Do not delete unrelated files.

Tests:

- prior canonical file removed;
- unrelated sibling preserved;
- owned stale temp cleanup bounded;
- missing directory/file safe.

---

# Phase 5 — Atomic JSON Publisher

Implement Worker-owned atomic file publication.

Required algorithm:

1. build complete serialized envelope in memory;
2. write UTF-8 JSON to owned temporary sibling;
3. flush;
4. close;
5. atomically replace/move destination according to platform-safe existing .NET convention;
6. clean owned temp on failure where safe.

Reader-visible guarantee:

- old complete envelope; or
- new complete envelope;
- never partial canonical file.

No multiple-writer coordination.

No distributed locking.

Tests must prove atomic old-or-new visibility under concurrent reads to the extent deterministic repository tests permit.

---

# Phase 6 — Worker Publication Integration

Connect the existing WP04 atomic in-memory publication to the cross-process file publisher at the narrow Worker-owned boundary.

Requirements:

- both Historical and Replay successful WP04 publications reach the handoff;
- Ready/WarmUp/Empty/Failed/Stale serialize truthfully;
- do not bypass `AtomicVisualizationReadModelStore`;
- do not create a second read model;
- no SQLite/provider access.

Prefer a narrow publication adapter/subscriber boundary consistent with current architecture.

If the existing WP04 store has no safe publication hook and a material contract redesign would be required, stop.

---

# Phase 7 — Python Configuration

Implement Python-side resolution for:

- `Visualization__HandoffPath`;
- `Visualization__RefreshIntervalSeconds`.

Path behavior must match .NET semantics.

Refresh:

- absent => 2;
- integer 1–60 => accepted;
- malformed/out-of-range => fail fast with safe configuration error.

Do not introduce extra configuration.

If Python cannot derive the Windows LocalApplicationData default using existing standard environment semantics without ambiguity, use the repository/platform convention proven by the runtime environment. If no unambiguous equivalent exists, stop rather than invent a divergent path.

---

# Phase 8 — Python Envelope Parser / Validator

Implement a narrow typed/validated consumer representation.

Validate:

- UTF-8 JSON;
- exact contract version;
- required state;
- revision kind/value;
- deterministic identity;
- source mode/authority;
- required state-specific fields.

Reject unknown contract versions.

Do not reinterpret unknown fields into new semantics.

Use only existing Python dependencies/pins.

No package changes unless explicitly authorized by the WP05 manifest; otherwise stop.

---

# Phase 9 — Revision Comparator

Implement fixed consumer revision comparison.

Historical:

- compare Historical presentation revisions only within Historical context.

Replay:

- compare Replay logical ticks only within Replay context.

Equal revision:

- same identity => equivalent;
- different identity => conflict.

Lower:

- older/stale transport input; ignore.

Cross-kind/context:

- never numeric compare.

Tests:

- newer;
- older;
- equivalent;
- conflict;
- Historical vs Replay separation.

---

# Phase 10 — Bounded Reader / Retry

Implement one refresh-cycle function.

Maximum:

- two read attempts.

Retry delay:

- 50 ms.

Retry only:

- transient I/O;
- disappearance during read;
- parse failure consistent with replacement race.

Missing file:

- no same-cycle retry.

After exhaustion:

- return safe transport/read warning;
- retain last-good externally.

Tests must verify exact attempt count.

Do not add background thread.

---

# Phase 11 — Last-Good Cache

Implement one-envelope maximum cache.

Behavior:

- newer valid => replace;
- equivalent => retain unchanged;
- older => retain;
- conflict => retain + integrity warning;
- missing => retain + ProducerUnavailable/transport warning;
- corrupt => retain + read-integrity warning;
- unknown version => retain + version warning.

No historical accumulation.

No backend-state mutation.

Tests must prove one-cache bound.

---

# Phase 12 — Streamlit Entry Point

Implement the separate WP05 Streamlit entry point using existing Streamlit **1.61.1** and existing exact package pins.

It must:

- read only through the WP05 consumer;
- render latest complete envelope;
- support automatic periodic refresh;
- support manual refresh;
- never start/stop Worker;
- never access SQLite/provider;
- never compute feature values.

Use existing repository Streamlit conventions if present.

Do not alter the existing JSON-over-stdio boundary.

---

# Phase 13 — Bounded Refresh in Streamlit

Implement periodic refresh at configured cadence.

Default:

2 seconds.

Bounds:

1–60 seconds.

Manual refresh:

- one immediate cycle;
- cadence unchanged.

Use Streamlit-supported mechanisms already available under pinned version.

Do not add a polling daemon/background thread.

If Streamlit 1.61.1 does not provide the assumed refresh mechanism, inspect installed API/repository conventions and use the smallest supported bounded rerun approach without adding dependencies.

---

# Phase 14 — Truthful Rendering

Render the fixed backend states without reinterpretation.

## Ready

Show:

- target/source mode;
- latest observation;
- count;
- bounded price/time window;
- available feature identity/value;
- pipeline/quality status.

## WarmUp

Show:

- existing observations;
- current count;
- required count 2;
- feature unavailable/not ready.

## Empty

Show genuine backend Empty.

Do not use Empty for missing handoff.

## Failed

Show safe canonical failure category/message and recoverability.

Retain/display last-good payload only where envelope/consumer contract permits.

## Stale

Render backend Stale as backend Stale.

Transport warnings remain distinct.

## ProducerUnavailable

Transport-local only.

Do not convert to WP04 state.

---

# Phase 15 — Price/Time Visualization

Implement the minimum WP05 visualization required by #230/accepted definition.

Use only the bounded ordered observation window from the envelope.

Do not:

- query history;
- reconstruct from SQLite;
- compute new indicators;
- infer missing points.

Keep chart/table presentation simple and truthful.

No WP06 styling expansion.

---

# Phase 16 — Consumer Failure / Warning Rendering

Surface safe consumer-local warnings for:

- ProducerUnavailable;
- transient I/O;
- corrupt JSON;
- unknown version;
- revision conflict.

Warnings must not overwrite the backend state.

No raw exception/stack trace in normal UI.

---

# Phase 17 — Worker Transport Tests

Add focused .NET tests for:

- default path;
- absolute override;
- relative rejection;
- startup canonical cleanup;
- owned temp cleanup;
- unrelated file preservation;
- atomic old-or-new publication;
- valid JSON envelope;
- exact contract version;
- Historical publication;
- Replay publication;
- all WP04 states where applicable;
- no partial canonical file visibility.

Do not weaken predecessor concurrency tests.

---

# Phase 18 — Python Consumer Tests

Using the repository's existing Python test mechanism, add tests for:

- default/override path;
- refresh default/bounds;
- malformed/out-of-range refresh;
- missing file;
- valid envelope;
- corrupt JSON;
- unknown version;
- newer/equivalent/older/conflicting revision;
- cross-kind non-comparison;
- exact two-attempt retry;
- 50 ms retry behavior using deterministic mocking where appropriate;
- last-good retention;
- one-envelope cache bound;
- manual refresh logic if separable;
- no backend-state reinterpretation.

Do not add test dependencies unless already pinned/authorized.

---

# Phase 19 — Static Consumer Boundary Proof

Search/diff prove Streamlit/Python WP05 code contains no:

- SQLite connection/query;
- provider call;
- `simple-return-lag-1-v1` formula;
- pipeline materialization;
- Worker process control;
- schema mutation;
- persistence write.

Hard gate.

---

# Phase 20 — Real Historical Production Composition

Prove:

`Worker Historical`
→ existing proven WP04 envelope
→ atomic JSON file
→ Python consumer
→ parsed/cached envelope
→ Streamlit rendering input

At minimum, use an integration/evidence test that writes via the real Worker publisher and reads via the real Python consumer where repository tooling permits.

Do not manually create the handoff file as the only production evidence.

---

# Phase 21 — Real Replay Production Composition

Prove the same for Replay:

`Worker Replay`
→ existing WP04 envelope
→ atomic JSON file
→ Python consumer
→ parsed/cached envelope
→ Streamlit rendering input

Preserve:

- source authority 1;
- Replay logical-tick revision;
- finite completion semantics.

No Replay redesign.

---

# Phase 22 — Startup / Session Evidence

Prove:

- prior canonical handoff removed at Worker startup;
- first new publication creates new complete handoff;
- Historical revision reset cannot be confused with prior session because prior file was removed;
- graceful shutdown may leave last-good file;
- next startup removes it.

Do not invent session IDs.

---

# Phase 23 — Refresh / Retry Acceptance

Prove:

- default 2 s;
- 1 s min;
- 60 s max;
- malformed/out-of-range failure;
- manual immediate cycle;
- maximum two reads;
- one 50 ms retry;
- missing file no same-cycle retry;
- next cycle can recover;
- no unbounded state.

---

# Phase 24 — WP04 Predecessor Regression

Revalidate WP04:

- Historical production composition;
- Replay production composition;
- Model C;
- Ready/Empty/WarmUp/Stale/Failed;
- 64-row bound;
- Historical revision;
- Replay revision;
- atomic in-memory publication;
- concurrency;
- recovery.

Historical focused acceptance predecessor:

4/4.

WP04 read-model predecessor:

7/7.

---

# Phase 25 — WP02 / WP03 Regression

Revalidate:

## WP02

- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- finite completion.

## WP03

- Historical/Replay Worker modes;
- dataset boundary;
- schema v4;
- authority 0/1;
- Replay persistence;
- canonical pipeline.

No regressions permitted.

---

# Phase 26 — Build and .NET Suites

Run:

- Infrastructure — predecessor baseline **155/155**;
- Application — **122/122**;
- Domain — **11/11**;
- Architecture — **13/13**.

Counts may rise due to WP05 tests.

Run build:

- require 0 errors;
- report warnings exactly.

---

# Phase 27 — Python Validation

Run the repository-governed Python test/lint/compile checks authorized by WP05.

At minimum:

- consumer tests;
- configuration tests;
- parser/revision/cache/retry tests;
- Streamlit entry-point import/compile/smoke validation where repository conventions support it.

Do not claim UI acceptance from import alone; combine with integration evidence.

Capture exact commands/results.

---

# Phase 28 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**301/301 passed**

Final count should be >=301, with increases explained by WP05 .NET tests.

Require:

- exit 0;
- 0 failed;
- exact passed/failed/skipped/total;
- no unexplained missing predecessor tests.

Also run all governed Python checks.

---

# Phase 29 — Diff / Scope Audit

Classify every changed file as:

- Worker WP05 handoff configuration/path;
- Worker atomic JSON publisher/integration;
- Python WP05 consumer/configuration;
- Streamlit WP05 entry point;
- WP05 .NET test;
- WP05 Python test;
- narrowly required existing config file if manifest-authorized.

Prove:

- no WP06;
- no schema change;
- no persistence redesign;
- no pipeline algorithm change;
- no Replay redesign;
- no SQLite/provider UI access;
- no feature recomputation;
- no new dependency unless explicitly authorized;
- no JSON-over-stdio change;
- no unrelated refactor;
- predecessor authority/control files preserved.

Anything unexplained blocks completion.

---

# Phase 30 — Technical Acceptance Gate

Require explicit PASS for:

- canonical path resolution;
- override validation;
- Worker-only directory ownership;
- startup cleanup;
- atomic file publication;
- old-or-new visibility;
- exact contract version;
- Historical handoff;
- Replay handoff;
- Python parsing;
- refresh default/bounds;
- exact retry count/delay;
- last-good retention;
- revision comparison;
- conflict handling;
- missing-file ProducerUnavailable;
- unknown-version rejection;
- transport/backend-state separation;
- manual refresh;
- automatic bounded refresh;
- Streamlit read-only boundary;
- truthful Ready;
- truthful WarmUp;
- truthful Empty;
- truthful Failed;
- truthful Stale;
- bounded chart/table source;
- Historical production composition;
- Replay production composition;
- startup/session behavior;
- no SQLite/provider access;
- no feature recomputation;
- WP02 regression;
- WP03 regression;
- WP04 regression;
- .NET suites;
- Python validation;
- build;
- full regression;
- scope audit.

Any FAIL stops completion.

---

# GitHub Completion Lifecycle

Only after every technical gate passes:

1. Add concise evidence comment to #230.
2. Set Project #2 Status to `Done`.
3. Preserve:
   - Priority P1;
   - Release 1.9;
   - existing Area classification.
4. Close #230.

Do not modify:

- #229;
- #231–#237;
- protected milestones;
- release taxonomy;
- dependencies unless #230's already-governed dependency edge updates automatically under established convention.

After closure, verify:

- #230 Closed / Done;
- #231 remains Open / Backlog;
- milestone #58 remains Open;
- canonical milestone count becomes **7 open / 5 closed**;
- raw GitHub closed count remains one higher because historical duplicate #225 is separately included.

Do not start WP06.

---

# Stop Conditions

Stop immediately if:

- WP05 manifest does not authorize a required path;
- cross-process publication requires redesign of WP04 envelope;
- Worker publication requires a second producer architecture;
- Python/.NET path semantics cannot be made identical without new contract;
- Streamlit refresh requires a new dependency not authorized;
- transport requires HTTP/socket/queue/watcher;
- schema/persistence/pipeline redesign becomes necessary;
- Replay semantics must change;
- SQLite/provider UI access becomes necessary;
- tests require weakening;
- any predecessor suite fails;
- Python validation fails;
- build/full regression fails;
- scope audit is unexplained.

On stop:

- preserve valid WP05 partial state if safe;
- do not close #230;
- do not start WP06;
- report exact blocker and minimum required fresh authority.

---

# Required Completion Report

Return:

## Transport implementation
- path resolver;
- startup cleanup;
- atomic publisher;
- JSON contract.

## Consumer implementation
- parser;
- revision comparator;
- retry;
- cache;
- warnings.

## Streamlit
- entry point;
- automatic/manual refresh;
- state rendering;
- bounded visualization.

## Production composition
PASS/FAIL:
- Historical Worker → file → consumer;
- Replay Worker → file → consumer.

## Boundary proof
- no SQLite;
- no provider;
- no feature recomputation;
- no producer mutation;
- no WP06.

## Validation
- Infrastructure exact count;
- Application exact count;
- Domain exact count;
- Architecture exact count;
- WP02/WP03/WP04 focused evidence;
- Python commands/results;
- build errors/warnings;
- full regression exact count.

## Scope proof
- changed-file classification;
- dependency/package proof;
- schema/persistence/protocol unchanged.

## GitHub lifecycle
- #230 before/after;
- Project Status;
- milestone canonical/raw counts;
- #231 untouched.

## Next eligible work package

On success:

`NEXT ELIGIBLE WORK PACKAGE: WP06 — #231`

Do not execute WP06.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless all technical gates pass and #230 is Closed / Done.

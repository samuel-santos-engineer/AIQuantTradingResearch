# Release 1.9 — WP03 Worker Replay-Mode/Configuration Completion Amendment — Codex Authority

## Authority

This grants a narrow completion amendment for Release 1.9 WP03, canonical issue #228.

Proven state:
- one private canonical five-stage `ExecuteCanonical` executor is implemented;
- historical and explicit-observation materialization converge on it;
- historical acquisition remains through `IHistoricalObservationStore`;
- replay does not masquerade as historical storage;
- Application focused tests passed 122/122;
- WP02 replay suite passed 142/142;
- full regression passed 288/288;
- build passed with 0 errors and 0 warnings;
- #228 remains Open / Backlog;
- no WP03 lifecycle mutation occurred;
- WP04 has not started.

Remaining acceptance gap: #228 requires manifest-authorized Worker-owned explicit replay mode/configuration and real replay-to-pipeline runtime composition.

Preserve the successful WP03 implementation. Do not roll it back. This authority permits only the minimum Worker execution/configuration/composition work required by #228, validation, and WP03 lifecycle finalization.

## Objective

Complete this production path:

`Worker explicit mode/configuration → WP02 replay source → WP03 observation-input seam → ExecuteCanonical → canonical five-stage pipeline → finite completion`

Preserve the historical path, WP02 replay semantics, one canonical pipeline, and all existing foundations.

## Fixed architecture

Historical:
`Worker → historical materialization → IHistoricalObservationStore → ExecuteCanonical → stages 1–5`

Replay:
`Worker explicit replay mode/config → WP02 replay source → explicit observations → WP03 seam → ExecuteCanonical → stages 1–5`

Do not reopen this architectural decision.

## Permitted scope

Modify only manifest-authorized WP03 Worker execution/configuration surfaces required by #228, including when proven necessary:
- Worker mode selection;
- replay configuration/options and validation;
- configuration binding;
- Worker DI/composition root;
- execution dispatch between historical and replay;
- narrowly required composition calls;
- Worker/runtime integration tests;
- minimal #228-required config documentation/examples.

Existing WP03 pipeline/seam code may change only for a concrete integration defect and only minimally.

## Forbidden

Do not:
- roll back or redesign `ExecuteCanonical` for preference;
- create another executor or replay pipeline;
- bypass the WP03 seam;
- use `IHistoricalObservationStore` for replay;
- redesign WP02 replay contracts;
- build a generalized mode/strategy framework;
- change package pins, Python, Streamlit, SQLite schema, or the governed JSON-over-stdio boundary;
- alter Release 1.9 planning/dependencies;
- modify #225 or protected milestones #59/#60/#50/#51/#61;
- implement WP04+;
- close #228 before all technical gates pass.

## Phase 0 — Prove exact Worker gap

Before mutation:
1. Read #228 and its Release 1.9 manifest.
2. Identify the exact Worker-owned artifacts authorized by the manifest.
3. Read Worker entry point, composition root, configuration conventions, and tests.
4. Read WP02 replay configuration/DI.
5. Read the WP03 seam and `ExecuteCanonical`.
6. Prove current successful WP03 edits are intact.
7. Record Git state and classify existing WP03 diff.

Stop if the Worker requirement or authorized paths are ambiguous.

## Phase 1 — Requirement matrix

For every #228 Worker requirement map:
- required behavior;
- current behavior;
- exact gap;
- authorized file/type;
- configuration source;
- validation rule;
- DI/composition impact;
- test evidence.

Explicitly derive from authority/repository:
- supported mode value(s);
- default mode, if defined;
- required replay configuration;
- invalid/unknown mode behavior;
- missing/invalid replay configuration behavior;
- historical backward compatibility;
- replay-source resolution;
- replay-to-seam composition;
- cancellation propagation;
- finite replay termination.

Do not invent semantics.

## Phase 2 — Minimal Worker configuration

Implement the smallest explicit Worker-owned mode/configuration using existing .NET binding/options/DI conventions.

Require:
- explicit deterministic mode selection;
- explicit replay configuration;
- clear validation;
- historical mode does not require replay-only fields;
- replay mode never silently falls back to historical;
- replay config maps directly to completed WP02 contracts.

Avoid scattered string parsing, global mutable state, and speculative abstractions.

## Phase 3 — Real runtime composition

Preserve historical execution unchanged.

For replay mode prove production wiring:
1. Worker selects replay mode.
2. Worker resolves/constructs the real WP02 replay source/configuration.
3. Replay increments are obtained under WP02 semantics.
4. Explicit observations enter the WP03 seam.
5. The seam calls `ExecuteCanonical`.
6. All five canonical stages execute.
7. Finite replay completes under #228/Worker semantics.

Do not satisfy acceptance with a test-only composition that differs from production wiring.

## Phase 4 — Preserve replay lifecycle semantics

Prove through the real Worker composition:
- replay identity remains explicit/deterministic;
- logical ticks remain ordered/deterministic;
- restart/resume reaches the source correctly;
- duplicates are neither accidentally removed nor introduced;
- cancellation reaches replay and prevents unauthorized continued work;
- bounds remain enforced;
- end-of-replay is handled as finite completion, not accidental failure.

## Phase 5 — Focused Worker/runtime tests

Add tests proving:
- historical mode selects historical path;
- replay mode selects replay path;
- invalid mode fails explicitly;
- default behavior matches #228 if a default exists;
- valid replay configuration binds;
- missing/invalid required replay config fails;
- historical mode does not require replay-only config;
- production replay composition resolves the real WP02 source;
- replay observations reach the real WP03 seam;
- replay reaches the shared `ExecuteCanonical`;
- all five canonical stages execute as required;
- restart/resume, duplicates, cancellation, bounds, and finite completion remain correct.

Exercise production composition as directly as repository conventions allow; do not rely solely on mocks that bypass the wiring under acceptance.

## Phase 6 — Regression guards

Rerun:
- Application focused suite (pre-amendment 122/122);
- WP02 replay suite (pre-amendment 142/142);
- Worker/runtime focused suite.

Higher counts are acceptable when explained by amendment-owned tests. Do not weaken predecessor tests.

## Phase 7 — Build and full regression

Run the established repository build. Require 0 errors and report warnings exactly.

Then run:
`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Pre-amendment full baseline: 288/288 passing.

Capture exit status, passed, failed, skipped, and material warnings. A higher count is expected if tests are added. An unexplained lower count is a blocker.

## Phase 8 — Production-flow proof

Before acceptance, prove actual code paths:

Historical:
`Worker → historical acquisition → IHistoricalObservationStore → WP03 materialization → ExecuteCanonical → stages 1–5`

Replay:
`Worker → explicit replay mode/config → WP02 replay source → WP03 seam → ExecuteCanonical → stages 1–5 → finite completion`

If the real production replay path cannot be proven, stop.

## Phase 9 — Diff/scope audit

Classify every current WP03 changed file as:
- canonical-stage extraction;
- additive observation-input seam;
- Worker mode/configuration;
- Worker runtime composition;
- directly required compatibility/DI;
- WP03 test;
- #228-required docs/config.

Prove:
- exactly one `ExecuteCanonical`;
- both runtime paths converge there;
- no parallel pipeline;
- no replay use of historical storage;
- no generalized mode/ingestion framework;
- no unnecessary WP02 redesign;
- no WP04+ work;
- no unauthorized foundation/planning changes;
- pre-existing authority/control files preserved.

Anything unexplained blocks acceptance.

## Phase 10 — Technical acceptance

Enumerate every #228 criterion and report implementation evidence, test evidence, and PASS/FAIL.

Additionally require PASS for:
- canonical executor/seam;
- historical compatibility;
- explicit Worker mode;
- replay configuration/validation;
- real production replay composition;
- five-stage replay execution;
- identity/ticks/restart/duplicates/cancellation/bounds/end-of-replay;
- Application tests;
- WP02 replay tests;
- Worker/runtime tests;
- build;
- full regression;
- scope audit.

If any fails, leave #228 Open / Backlog.

## Phase 11 — GitHub lifecycle finalization

Only after technical acceptance:
1. Read #228 current state and completion convention.
2. Add one concise evidence comment if required.
3. Move #228 Project Status from Backlog to the authoritative completed state.
4. Preserve P1, Release 1.9, and authoritative Area.
5. Close #228.
6. Keep milestone #58 open.
7. Read back every mutation.
8. Do not modify #229.

Expected canonical milestone state after success: 9 open / 3 closed. Raw closed counts may additionally include historical duplicate #225. Dependency chain remains 11/11.

## Stop conditions

Stop if requirements/authorized Worker paths are ambiguous; successful WP03 state is missing; implementation requires reopening pipeline architecture, a second pipeline, historical-store replay misuse, generalized framework, WP04+, or unauthorized foundation changes; predecessor tests/build/full regression fail; production composition cannot be proven; diff scope is unexplained; or GitHub mutation cannot be proven.

On stop, preserve the successful existing WP03 work. Do not broaden authority.

## Success criteria

Success requires:
- validated canonical executor/seam preserved;
- Worker explicit mode/configuration implemented and validated;
- real Worker replay composition uses WP02 source and WP03 seam;
- historical and replay paths converge on `ExecuteCanonical`;
- all five stages execute through real replay runtime;
- all WP02 replay semantics remain intact;
- every #228 criterion passes;
- focused suites, build, and full regression pass;
- final diff is WP03-scoped;
- #228 is completed and closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- WP04 remains unstarted.

## Required completion report

Report:
- exact Worker mode/config shape and validation;
- production historical and replay flows;
- all changed files;
- PASS/FAIL for every #228 criterion;
- Application, WP02 replay, Worker/runtime, build, and full regression results;
- final diff/scope proof;
- #228 before/after lifecycle state;
- milestone canonical counts;
- confirmation #229–#237 untouched.

On success state:
`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

## Terminal markers

Success:
`RELEASE 1.9 WP03 WORKER REPLAY MODE/CONFIGURATION AND EXECUTION COMPLETE`

Blocker:
`RELEASE 1.9 WP03 WORKER REPLAY MODE/CONFIGURATION BLOCKED`

Emit success only when all technical and lifecycle requirements are freshly proven.

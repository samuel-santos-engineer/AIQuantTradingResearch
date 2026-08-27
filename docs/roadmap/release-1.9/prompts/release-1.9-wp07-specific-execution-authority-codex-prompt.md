# Release 1.9 — WP07 Specific Execution Authority — Codex Prompt

## Authority
Execute Release 1.9 **WP07**, canonical GitHub issue **#232**, and no later work package. Use **GPT-5.6 Terra**.

Before mutation, resolve #232's exact objective, deliverables, semantic contract, manifest-owned paths, acceptance criteria, and boundary with completed WP06 and future WP08/WP09. If a material choice remains undefined, stop before mutation and request the minimum narrow **GPT-5.6 Luna** definition authority. Do not invent semantics.

## Entry baseline
Expected:
- WP01–WP06 Closed / Done.
- #231 Closed / Done.
- #232 Open / Backlog.
- #233–#237 Open / untouched.
- Milestone #58 open; canonical 6 open / 6 closed.
- WP06 Python 6/6.
- WP05 Python regression 3/3.
- Streamlit 1.61.1 import smoke and `pip check` passed.
- Build 0 errors / 0 warnings.
- Full .NET regression **305/305**.

Accepted WP06 surface: deterministic immutable `VisualizationFrame`, exact ordered price/time projection, latest observation, canonical count, bounded window/capacity, factual metadata, backend state, separate transport warning, and minimal line-chart rendering.

## Fixed predecessor boundaries
Preserve WP04 `aiq-visualization-read-model-v1`, 64-row bound, Ready/Empty/WarmUp/Stale/Failed, HistoricalPresentationRevision and ReplayLogicalTick. Preserve WP05 atomic JSON, read-only bounded consumer, one last-good envelope, bounded retry, ProducerUnavailable, and separation of transport/backend state. Preserve WP06 frame identity, price/time, latest/count/window, factual metadata and deterministic rendering inputs.

Unless #232 explicitly owns an additive extension, WP07 must not alter those semantics.

## Phase 0 — Read-only authority resolution
Before changing files:
1. Read #232 completely.
2. Read the accepted Release 1.9 WP07 definition and implementation manifest.
3. Read the completed WP06 visualization-frame amendment.
4. Inspect `python/presentation/realtime_financial_visualization.py`, `python/presentation/visualization_read_model.py`, and WP06 tests.
5. Read WP08/WP09 ownership sufficiently to avoid stealing future work.
6. Read any WP07-specific planning artifact referenced by #232.
7. Record exact objective, deliverables, semantics, authorized production/test paths, shared exceptions, acceptance criteria, non-goals, and WP06/WP07/WP08/WP09 boundaries.

No mutation.

## Phase 1 — Git/lifecycle/predecessor proof
Record branch, HEAD, origin/main, ahead/behind, staged/tracked/relevant untracked paths. Verify #231 Closed/Done, #232 Open/Backlog, #233 Open/Backlog, and no unauthorized partial WP07 implementation.

Before mutation run:
- WP06 Python tests: expected 6/6.
- WP05 Python tests: expected 3/3.
- Python compile/import checks.
- Streamlit 1.61.1 verification.
- `pip check`.
- repository-standard build.
- `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`, expected **305/305**.

If predecessor state is not clean, stop.

## Phase 2 — Manifest execution map
List every intended changed/created path with:
- exact path;
- WP07 manifest entry;
- exclusive/shared ownership;
- exact authorized concern;
- acceptance test.

If any required path is missing, **STOP BEFORE MUTATION** and request a narrow WP07 manifest/path amendment. Do not reuse WP05/WP06/WP09 test paths or create unlisted helpers without authority.

## Phase 3 — Semantic sufficiency gate
For every #232 deliverable determine whether material behavior is fixed: metadata/content, panels/sections, labels, ordering, status presentation, chart/table ownership, controls, summaries, refresh interaction, state behavior, assertion surface, layout and terminology.

If multiple materially different designs satisfy the accepted text, **STOP**. Report the unresolved choice, evidence, candidate models, and minimum narrow Luna definition authority.

## Phase 4 — WP06 boundary
Consume the accepted `VisualizationFrame`. Do not silently change frame identity, price/time, latest/count/window, revisions, transport warnings, backend states, or WP05 cache/retry semantics.

If WP07 needs data absent from the frame, determine whether #232 explicitly authorizes an additive factual projection. Otherwise stop rather than reconstructing data.

## Phase 5 — Implement only WP07
If contract and manifest gates pass, implement the minimum #232 surface. Preserve exact dependencies, Streamlit 1.61.1, schema v4, atomic JSON, and JSON-over-stdio.

Forbidden unless explicitly authorized:
- SQLite/provider access from presentation;
- feature recomputation;
- Worker control;
- new IPC;
- speculative WP08/WP09 hooks;
- broad refactor.

## Phase 6 — Presentation truthfulness
All visible content must derive from accepted frame/envelope facts or explicitly governed WP07 semantics. Never hide or reinterpret WarmUp/Empty/Failed/Stale, fabricate feature values, imply cross-session continuity, or reinterpret Historical/Replay provenance.

## Phase 7 — Focused WP07 tests
Add only WP07-authorized tests. Directly prove every #232 acceptance criterion using deterministic functional assertions. Avoid screenshot/pixel tests unless explicitly required. Do not weaken predecessor tests.

## Phase 8 — Production composition
Where #232 concerns actual presentation composition, prove:
accepted WP05 consumer → WP06 `VisualizationFrame` → WP07 presentation surface.
Do not use manually reconstructed final presentation data as sole evidence. Avoid uncontrolled long-running processes.

## Phase 9 — Static boundary audit
Prove no WP07 introduction of SQLite, providers, duplicated feature formula, Worker control, persistence writes, new IPC, schema/package changes, WP08/WP09 implementation, unbounded history, or predecessor reinterpretation.

## Phase 10 — Python validation
Run:
- WP07-focused tests;
- WP06 6/6 regression;
- WP05 3/3 regression;
- compile/import checks;
- Streamlit smoke;
- `pip check`.
Record exact commands/counts. All pass.

## Phase 11 — .NET/build regression
Run definitive Infrastructure, Application, Domain, and Architecture suites and record fresh counts. Run repository-standard build; require exit 0 and 0 errors/0 warnings unless a pre-existing governed warning is proven.

## Phase 12 — Full regression
Run:
`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor: **305/305**. Require exit 0, 0 failed, exact passed/failed/skipped/total, and no unexplained missing tests.

## Phase 13 — Scope audit
For every changed/created path report path, authority entry, exclusive/shared status, concern, and proof diff stayed within concern. Prove no unauthorized WP05/WP06/WP08/WP09, Worker/.NET production, schema, persistence, provider, package, or protocol mutation.

## Phase 14 — Acceptance matrix
PASS/FAIL:
- #232 objective resolved;
- manifest sufficient;
- semantics sufficient;
- WP06 boundary preserved;
- WP08/WP09 boundary preserved;
- every #232 criterion;
- focused tests;
- production composition where required;
- no SQLite/provider/feature recomputation/Worker control/new transport;
- WP05 Python;
- WP06 Python;
- Streamlit;
- pip check;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- build;
- full regression;
- final scope audit.

Any FAIL keeps #232 open.

## Phase 15 — GitHub completion
Only after every gate passes:
1. add concise #232 evidence if repository convention requires;
2. set Project #2 Status = Done;
3. preserve governed Priority / Release 1.9 / Area;
4. close #232.

Do not modify #231 or #233–#237. Verify #232 Closed/Done, #233 Open/Backlog, milestone open, canonical count **5 open / 7 closed** (raw closed may be one higher due to #225). Do not start WP08.

## Stop conditions
Stop if semantics/path authority is incomplete, predecessor contracts require redesign, missing data requires UI reconstruction, new dependency/schema/persistence/provider work is needed, later-WP ownership is crossed, tests need weakening, production evidence cannot be proven, validation/build/regression fails, or scope audit fails.

On blocker preserve valid authorized state, keep #232 open, do not start WP08, and identify the minimum narrow follow-up authority.

## Required completion report
Report:
- authority resolution;
- entry proof;
- execution map;
- implementation by deliverable;
- focused/production evidence;
- boundary audit;
- WP07/WP06/WP05 Python validation;
- Streamlit/pip check;
- Infrastructure/Application/Domain/Architecture;
- build/full regression;
- path scope audit;
- GitHub lifecycle.

On success state exactly:
`NEXT ELIGIBLE WORK PACKAGE: WP08 — #233`

## Terminal markers
Success:
`RELEASE 1.9 WP07 COMPLETE`

Blocker:
`RELEASE 1.9 WP07 BLOCKED`

Do not emit success unless every #232 semantic, path, acceptance, validation, scope, and lifecycle gate passes.

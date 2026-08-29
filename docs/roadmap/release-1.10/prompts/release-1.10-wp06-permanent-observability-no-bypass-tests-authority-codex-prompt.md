# Release 1.10 WP06 — Permanent Observability and No-Bypass Tests Authority

## Model assignment
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY implementation, validation execution, approved repository/GitHub lifecycle mutations.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

## Identity and entry
Release **1.10**; WP06 **Permanent Observability and No-Bypass Tests**; issue **#247**; milestone **#59**; Project **#2**.
Dependency: `WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`.

Accepted entry unless authoritative inspection contradicts:
- #242–#246 Closed/Done.
- #247 Open/Backlog; #248–#249 Open/Backlog.
- milestone #59 Open, expected **3 open / 5 closed**.
- baseline: Infrastructure 187/187, Application 131/131, Architecture 21/21, Domain 11/11 = **350/350 .NET**.
- Python presentation **21/21**; Streamlit **1.61.1**; `pip check` clean.
- Gitleaks **8.30.1**, 112 commits, no leaks.
- build succeeded, 0 errors.
- duplicate local `AIQuantTradingDev` selector warnings are environment-only; no signing/project config mutation.

Emit:
`RELEASE 1.10 WP06 ENTRY: PASS`

## Authoritative inputs
Read before mutation:
- `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
- #247
- WP02–WP05 production/tests and frozen handoffs
- current Architecture/Python tests and worktree diff.

The reconciled manifest and WP05→WP06 handoff define the exact WP06 path allowlist. Do not invent paths.

Emit:
`RELEASE 1.10 WP06 CONTRACT/HANDOFF CONSUMPTION: PASS`
`RELEASE 1.10 WP06 PATH/TEST OWNERSHIP: FROZEN`

## Permanent invariants

### WP02
Enforce:
- Application BCL `System.Diagnostics` ownership.
- root `pipeline.execute`.
- five governed stage activities.
- `HistoricalObservationRetrieval` measures only `IHistoricalObservationStore.Retrieve(...)`.
- `DatasetMaterialization` starts after retrieval.
- no duplicate opaque interval.
- Infrastructure child topology via `Activity.Current`.

### WP03
Enforce instrumentation ownership only for:
- `SqliteHistoricalObservationStore.Retrieve(string target)`
- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Enforce:
- no duplicate ownership in `SqliteDatasetCatalog`;
- `SqliteHistoricalObservationStore.Persist(...)` remains outside WP03 boundary;
- ActivitySource/Meter remains `AIQuantTradingResearch.Infrastructure`;
- activities remain `provider.operation`, `persistence.operation`;
- bounded operation/duration/failure instruments;
- finite/sanitized failure categories;
- governed parent topology.

### WP04
Enforce:
- no external exporter;
- no exporter package/project dependency;
- BCL-only Worker/interop observations;
- Worker owns lifecycle;
- Streamlit independent;
- Python invoker does not supervise Worker;
- canonical handoff remains cross-runtime presentation boundary.

### WP05
Enforce:
- .NET owns System Health source.
- visualization lifecycle and System Health remain distinct.
- visualization states remain `Ready`, `WarmUp`, `Empty`, `Stale`, `Failed`.
- `aiq-visualization-read-model-v1` remains canonical.
- nested `systemHealth` remains v1-compatible.
- no second health channel.
- health states remain exactly `ready`, `warmup`, `empty`, `failed`, `stale`, `unavailable`.
- `degraded` remains excluded.
- frozen predicates, precedence, finite reason tokens, state/reason mapping, absent-health compatibility, malformed behavior, timestamp/freshness semantics, provenance and deterministic Streamlit mapping remain enforced.
- no independent System Health freshness threshold.

### Cross-cutting no-bypass
Enforce:
- Streamlit does not read SQLite, call providers, supervise Worker, inspect processes/listeners, or own telemetry lifecycle.
- Python does not become a second health authority or parallel persistence/provider path.
- canonical JSON handoff remains UI boundary.
- SQLite schema remains v4; no migration.
- Release 1.8 JSON-over-stdio remains separate.
- deterministic/replay/simulated provenance remains truthful.
- no live-provider implication, trading, ML, backtesting, or parallel pipeline.

## Mutation boundary
Use only exact WP06 paths frozen by manifest/handoff.

Expected mutations: permanent tests and only explicitly authorized test helpers/docs.
Production mutation is expected **ZERO**.

Forbidden unless manifest explicitly authorizes:
- production behavior changes;
- `.csproj`;
- package changes/install;
- schema/migration;
- external exporter;
- runtime health service/channel/provider;
- Streamlit redesign;
- WP07/WP08 implementation;
- Git commit/branch/tag/PR mutations.

If permanent enforcement requires production redesign or a non-allowlisted path, BLOCK for narrow Luna reconciliation.

Preserve all accepted WP01–WP05 work; do not reset/clean/stash/revert unrelated changes.

## Phase 1 — Baseline
Run exact relevant pre-WP06 baseline and report actual counts:
- Infrastructure, Application, Architecture, Domain;
- Python presentation/architecture;
- relevant build;
- `pip check`.

Use only approved local dev signing if Windows Application Control requires it. Do not change tracked signing config.

Emit:
`RELEASE 1.10 WP06 BASELINE: PASS`

## Phase 2 — WP02 permanent tests
Add permanent behavioral/listener/architecture tests proving exact activity topology and boundaries. Prefer behavioral assertions over brittle source text.

Emit:
`RELEASE 1.10 WP06 WP02 PERMANENT OBSERVABILITY TESTS: PASS`

## Phase 3 — WP03 permanent tests
Prove exact owners, activity names, Meter instruments, bounded tokens/duration/failures, finite sanitized categories, parent topology, catalog non-duplication and Persist exclusion. Scope listeners against unrelated parallel activity.

Emit:
`RELEASE 1.10 WP06 WP03 PERMANENT INFRASTRUCTURE TESTS: PASS`

## Phase 4 — WP04 permanent tests
Prove Worker lifecycle ownership, Python no-supervision, no external exporter/config/package/lifecycle path, and canonical handoff independence.

Emit:
`RELEASE 1.10 WP06 WP04 LIFECYCLE/EXPORTER-ISOLATION TESTS: PASS`

## Phase 5 — WP05 permanent tests
Enforce the frozen WP05 truth table:
- exact bounded health states;
- `degraded` excluded;
- predicates and precedence;
- finite reason tokens and mapping;
- nested JSON shape;
- v1/pre-WP05 compatibility;
- malformed-health behavior;
- timestamp/freshness;
- no independent health freshness threshold;
- provenance;
- deterministic presentation mapping.

Make accidental vocabulary expansion fail.

Emit:
`RELEASE 1.10 WP06 WP05 SYSTEM HEALTH CONTRACT TESTS: PASS`

## Phase 6 — Python/Streamlit no-bypass
Use deterministic architecture/source/AST/import-boundary tests as appropriate. Prove canonical-handoff-only consumption and forbid SQLite/provider/Worker/listener bypasses and second health channel. No network dependency.

Emit:
`RELEASE 1.10 WP06 PYTHON/STREAMLIT NO-BYPASS TESTS: PASS`

## Phase 7 — Schema/handoff boundaries
Permanently enforce schema v4, canonical visualization v1, nested health not storage schema, Release 1.8 boundary separation, and no parallel/direct UI path.

Emit:
`RELEASE 1.10 WP06 SCHEMA/HANDOFF BOUNDARY TESTS: PASS`

## Phase 8 — Provenance/truthfulness
Enforce explicit deterministic/replay/simulated provenance; health cannot imply live data/exporter/provider connectivity; absent/malformed health cannot become false healthy.

Emit:
`RELEASE 1.10 WP06 PROVENANCE/TRUTHFULNESS TESTS: PASS`

## Phase 9 — Mutation-resistant negatives
Where technically sound, ensure tests fail on unauthorized future:
- `degraded` addition;
- exporter packages/config;
- Streamlit SQLite/provider/Worker supervision;
- second health handoff;
- schema-version change;
- visualization/health collapse;
- free-form health reasons.

Avoid comment/formatting-only tests.

Emit:
`RELEASE 1.10 WP06 MUTATION-RESISTANT NEGATIVE TESTS: PASS`

## Phase 10 — Focused validation
Run exact WP06-focused tests. Report selection/count/pass/fail/skip.

Emit:
`RELEASE 1.10 WP06 FOCUSED TESTS: PASS`

## Phase 11 — Full validation
Run complete affected baseline and report actual post-WP06 counts; counts may increase.

Include:
- Infrastructure;
- Application;
- Architecture;
- Domain;
- total .NET;
- Python presentation/architecture;
- Streamlit version;
- `pip check`;
- build warnings/errors.

If failure reveals a production defect outside WP06 authority, BLOCK rather than changing production.

Emit:
`RELEASE 1.10 WP06 FULL VALIDATION: PASS`

## Phase 12 — Security
Use **Gitleaks 8.30.1**:
`gitleaks git . --redact --verbose`

Do not weaken PowerShell policy. Require no leaks.

Emit:
`RELEASE 1.10 WP06 GITLEAKS SECURITY GATE: PASS`

## Phase 13 — Residue
Verify no WP06-owned Worker/testhost/Python/Streamlit/listener/temp-result/locked-handoff residue.

Emit:
`RELEASE 1.10 WP06 PROCESS/LISTENER/UI RESIDUE: CLEAN`

## Phase 14 — Path/hunk audit
Prove only frozen WP06 paths changed; production, project/package/schema, exporter/runtime-channel, WP07/WP08 changes are absent.

Emit:
`RELEASE 1.10 WP06 PATH OWNERSHIP: PASS`
`RELEASE 1.10 WP06 FORBIDDEN TARGET AUDIT: PASS`

## Phase 15 — WP07 handoff
Prepare factual handoff for **WP07 — Documentation, Developer Setup & Operational Runbook**, covering WP02 topology, WP03 instrumentation, WP04 lifecycle/no-exporter, WP05 health semantics/presentation, v1 handoff, schema v4, provenance/no-bypass, permanent-test locations/commands, and environment-only dev-signing caveat if still relevant.

Do not implement WP07.

Emit:
`RELEASE 1.10 WP06 DOWNSTREAM HANDOFF: PASS — WP07 READY`

## Phase 16 — Acceptance
Map every #247/release/manifest/handoff criterion to evidence.

Only if all pass emit:
`RELEASE 1.10 WP06 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

## Phase 17 — Mandatory GitHub completion
After acceptance, re-read #247 and its unique Project #2 item.

Then:
1. close #247 if Open;
2. set Project Status to `Done` only if not already Done.

If issue-close automation sets Done, do not issue redundant Status mutation. Count only explicit mutations.

Do not close milestone #59, modify #248/#249, change Release, or start WP07.

Emit:
`RELEASE 1.10 WP06 GITHUB WORK-PACKAGE COMPLETION: PASS`

## Phase 18 — GitHub post-verify
Require #242–#247 Closed/Done; #247 Release=1.10 and milestone #59; milestone remains Open; #248–#249 remain Open/Backlog unless independently changed.

Expected if no independent change: **2 open / 6 closed**.

Emit:
`RELEASE 1.10 WP06 GITHUB COMPLETION POST-VERIFY: PASS`

## Phase 19 — Mutation ledger
Report exact repository paths and explicit GitHub mutations.

Require:
`RELEASE 1.10 WP06 REPOSITORY MUTATIONS: ACCEPTED WP06 PATHS ONLY`
`RELEASE 1.10 WP06 PRODUCTION MUTATIONS: ZERO`
`RELEASE 1.10 WP06 PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`
`RELEASE 1.10 WP06 GIT MUTATIONS: ZERO`
`RELEASE 1.10 WP06 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

## Required success terminal
`RELEASE 1.10 WP06 — PERMANENT OBSERVABILITY AND NO-BYPASS TESTS AUTHORITY COMPLETE`

## Blocked outcome
BLOCK if exact test ownership is not deterministic, a required invariant needs unauthorized production redesign, a production defect lies outside WP06 authority, or validation is blocked without an approved narrow environment unblock.

Before acceptance: #247 remains Open/Backlog; Git/GitHub mutations ZERO; preserve valid WP06 work; WP07 does not start.

Exact blocked terminal:
`RELEASE 1.10 WP06 — PERMANENT OBSERVABILITY AND NO-BYPASS TESTS AUTHORITY BLOCKED`

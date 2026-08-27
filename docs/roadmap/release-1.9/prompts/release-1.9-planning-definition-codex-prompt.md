# AIQuantTradingResearch — Release 1.9 Planning & Definition — Codex Authority

## Mission
Define, but do not implement, **Release 1.9 — Real-Time Financial Data Visualization** for `samuel-santos-engineer/AIQuantTradingResearch`.

This is planning/definition authority only. It does not authorize production implementation, package installation, schema changes, WP execution, detailed GitHub WP creation, or Release 1.10 work.

## Mandatory predecessor discovery
Roadmap reconciliation was merged through PR #224. Before mutation:
- prove PR #224 is MERGED;
- verify its head derives from reconciliation commit `8e1e9deed02c583bff2298a55ab1758dc33fdcf9`;
- discover the actual resulting `main` merge SHA;
- verify `origin/main` contains the PR result;
- safely reconcile local `main` to `origin/main`;
- require local/remote `main` ahead/behind `0/0`.

The discovered `main` merge SHA is the immutable Roadmap Reconciliation / Release Sequencing predecessor for Release 1.9 definition. Never substitute the Release 1.8 closure SHA, PR branch-head SHA, or a guessed SHA.

Stop if this boundary cannot be proven.

## Roadmap gate
Verify live:
- #58 — Release 1.9 Real-Time Financial Data Visualization;
- #59 — Release 1.10 OpenTelemetry & Pipeline Observability;
- #60 — Release 2.0 Lightweight Machine Learning Evaluation;
- #50 — Release 2.1 Machine Learning;
- #51 — Release 2.2 Explainable AI;
- #61 — Release 2.3 Backtesting;
- #49 unchanged;
- #56 closed/unchanged;
- Project #2 Release taxonomy reconciled through 2.3;
- no detailed Release 1.9 WPs/issues yet.

Stop on material drift; do not silently reconcile it here.

## Accepted foundation
Inspect and reuse current accepted repository state. Expected Release 1.8 foundations include:
- CPython 3.13.15;
- ignored isolated `.venv`;
- governed Python dependency mechanism;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- scientific-stack validation;
- versioned JSON-over-stdio .NET↔Python boundary;
- technology-neutral Application invocation contracts;
- Infrastructure-owned Python execution;
- timeout/cancellation/failure translation;
- permanent interoperability tests;
- developer-environment documentation.

Expected permanent .NET baseline is 281/281 unless current accepted `main` proves otherwise.

Do not redesign accepted foundations without demonstrated conflict.

## Product intent
Portfolio proof:

> The platform visibly processes an evolving financial-market data stream through its governed pipeline and presents useful financial, feature, and validation state through a web UI.

Release 1.9 converts existing invisible engineering capability into a demonstrable end-to-end experience without weakening architecture.

## Canonical scope

### Deterministic simulated-live provider
Define a deterministic provider/replay mechanism that emits market observations incrementally and behaves like a live ticker from the pipeline perspective.

Requirements:
- deterministic/reproducible;
- canonical acceptance requires no paid service or external network;
- reuse existing provider/pipeline abstractions;
- preserve contracts and quality semantics;
- controlled cadence/lifecycle;
- no second ingestion architecture;
- clearly label data as simulated/replayed, never real live market data.

### Incremental pipeline execution
Define how observations enter the existing pipeline while preserving ownership of acquisition, validation, normalization/transformation, persistence, feature extraction, and dataset/snapshot state.

The UI must not own a parallel pipeline.

### Streamlit presentation
Use already-governed Streamlit as the outer web presentation adapter. Define:
- app entry point;
- presentation/read-model boundary;
- refresh/update behavior;
- lifecycle;
- developer execution;
- failure presentation.

Streamlit must not own business/pipeline rules.

### Financial visualization
Define useful evolving financial visualization grounded in actual contracts. At minimum consider price/time series, latest observation/state, observation count/window, and supported metadata. Do not fabricate semantics.

### Feature visualization
Expose existing governed feature `simple-return-lag-1-v1`, including unavailable/warm-up handling and deterministic validation. Do not invent predictive features for the UI.

### Dataset/data-quality visualization
Expose only repository-supported snapshot/validation/quality state. Candidate concepts may include accepted/rejected observations, snapshot identity, validation status, quality state, idempotency/replay or persistence state only when existing architecture supports them.

### Demonstration path
Future executable demonstration must prove:

`simulated observation → existing pipeline → persistence/features/validation → UI update`

It must be deterministic enough for local development, automated acceptance, screenshots/video, and portfolio demonstration.

## Explicit non-goals
Exclude:
- ML training/prediction/confidence/feature importance;
- backtesting/trading strategies/portfolio management;
- OpenTelemetry platform integration and Release 1.10 System Health;
- cloud deployment;
- paid-provider dependency;
- production trading/order/broker integration;
- broad frontend framework;
- second data pipeline;
- Streamlit-owned domain/business logic.

Do not pull 1.10/2.0/2.1/2.2/2.3 scope forward.

## Architecture invariants
- Domain remains independent of Streamlit, Python mechanics, UI/filesystem/process concerns.
- Application owns technology-neutral use-case/contracts/orchestration semantics.
- Infrastructure owns runtime/process/persistence/provider adapter mechanics.
- Streamlit is an outer presentation adapter; it may invoke governed application/read contracts and render presentation state, but must not opportunistically bypass boundaries via direct SQLite access, own ingestion/features/validation, or become a source of truth.
- Reuse Release 1.8 .NET↔Python interoperability unless evidence demonstrates it is unsuitable; do not casually introduce another mechanism.

## Foundational technology rule
Preserve:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

Streamlit is already governed. Inventory any proposed dependency; prefer governed/standard capabilities. A newly necessary foundational technology requires a later selection record before implementation. Install nothing here.

## Mandatory repository discovery
Read current authoritative roadmap/governance, Release 1.8 artifacts, roadmap reconciliation, architecture/design, data pipeline/provider/storage/lifecycle/quality, module interactions/contracts/extensibility/configuration/resilience/logging, .NET↔Python interoperability, Streamlit/Python governance, and actual implementation/tests.

Inspect actual current types/projects for:
- provider abstractions;
- pipeline orchestration;
- persistence/SQLite;
- feature extraction and `simple-return-lag-1-v1`;
- snapshots/validation/quality;
- Python invocation;
- composition root;
- tests.

Never invent types/paths from historical assumptions.

## Required definition artifacts
Create exactly these primary artifacts in the established Release 1.9 roadmap location:
1. `RELEASE_1.9_DEFINITION.md`
2. `RELEASE_1.9_EXECUTION_PLAN.md`
3. `RELEASE_1.9_FILE_MANIFEST.md`

They must be mutually consistent.

### Definition requirements
Include:
- title and discovered predecessor merge SHA;
- problem/demo objective;
- scope/non-goals;
- architecture/reuse boundaries;
- simulated-stream semantics;
- pipeline interaction;
- presentation/read-model boundary;
- financial/feature/data-quality visualization;
- deterministic acceptance;
- lifecycle/resilience/security/portability;
- observability boundary vs 1.10;
- ML boundary vs 2.0/2.1;
- testing/docs;
- acceptance criteria;
- risks/trade-offs;
- explicit unresolved decisions.

Do not hide unresolved architectural questions.

### Execution-plan requirements
Derive WPs from actual repository evidence. Concerns should normally include:
- preflight;
- simulated live-provider/replay semantics;
- incremental pipeline execution;
- presentation/read-model contract;
- Streamlit app foundation;
- evolving financial visualization;
- feature/data-quality visualization;
- lifecycle/resilience/determinism;
- permanent tests;
- architecture/docs/developer execution alignment;
- full integration/acceptance;
- closure/PR readiness where appropriate.

Do not force a specific count. Each WP must define objective, scope, non-goals, predecessor/dependency, architectural areas, expected artifacts, verification, completion evidence, and stop conditions. Prefer a linear graph unless parallelism is justified. No WP execution.

### File-manifest requirements
Freeze expected file ownership sufficiently to prevent accidental project structure. Classify each path CREATE/MODIFY/VERIFY/CONDITIONAL, with purpose and owning WP. Distinguish production .NET, Python/Streamlit presentation, tests, docs, config/scripts. Do not invent speculative paths. Materially unresolved execution paths block definition acceptance.

Execution may not create unlisted structural roots without corrective authority.

## UI shape
Keep UI minimal and useful:
- market/live-stream overview;
- evolving financial chart;
- current feature values;
- dataset/data-quality state.

`System Health` belongs to 1.10. Avoid broad navigation/styling frameworks. Clearly label simulated/mock data.

## Determinism
Definition must govern source input, ordering, cadence, timestamps, replay identity, restart, duplicate/idempotency behavior, finite acceptance mode, and accelerated test execution. Automated tests must not rely on long wall-clock sleeps.

## Lifecycle
Define ownership of simulated-provider start/stop/cancellation, Streamlit startup/shutdown, Python process ownership/cleanup, ports/listeners, temporary files, DB residue, and repeatable developer runs. Acceptance must prove no platform-owned orphan processes/listeners. Never kill unrelated Python/VS Code processes.

## Persistence/schema gate
Default: **no schema change**. First satisfy 1.9 with schema v3 and existing persistence/read capability. If repository evidence proves schema change mandatory, document why and stop for separate narrow schema authority. Do not evolve persistence for UI convenience.

## Testing strategy
Plan permanent tests at proper layers: Application orchestration/read contracts; Infrastructure simulated-provider/adapter mechanics; architecture tests where needed; governed Streamlit testing; deterministic end-to-end validation; relevant WP08/WP11 regression. Domain changes only if legitimate Domain behavior changes. No browser automation dependency without separate selection/governance.

## Acceptance expectations
Future acceptance must prove at least:
- deterministic simulated stream;
- incremental existing-pipeline processing;
- persistence remains correct;
- `simple-return-lag-1-v1` remains correct;
- snapshot/data-quality remains correct;
- Streamlit displays evolving financial, feature, and truthful validation/quality state;
- repeatability;
- no global Python pollution;
- no orphan platform processes/listeners;
- canonical .NET/Python regressions;
- build/format/Gitleaks/docs;
- zero ML/OpenTelemetry/backtesting leakage.

## Release integration invariant
Preserve prospectively:

> Release implementation must occur on a dedicated release/working branch. Completion and acceptance do not authorize direct integration into `main`. After acceptance, all governed release artifacts—including documentation—must be committed to the release branch, a PR must be opened against `main`, required verification must pass on the PR candidate, and only then may the PR be merged. The resulting `main` merge SHA becomes the immutable release repository boundary.

Release 1.9 implementation later requires a dedicated working branch. This authority does not authorize implementation on `main`.

## Mutation boundary
May:
- create/update the three definition artifacts;
- create/update this authority pair if repository-resident;
- update only roadmap/navigation docs strictly necessary to reference accepted definition;
- use the repository's governed planning integration workflow if required.

Must not:
- implement code;
- install packages/create environments;
- alter schema;
- create detailed WP issues;
- mutate Project #2 taxonomy;
- execute WPs;
- begin 1.10.

Milestone #58 is the existing placeholder. Detailed GitHub WP materialization requires a separate **Release 1.9 GitHub Planning Authority** after human acceptance.

## DEF1–DEF20
Report PASS/FAIL/NOT-APPLICABLE:
- DEF1 PR #224 merged state proven;
- DEF2 actual roadmap-reconciliation `main` merge SHA discovered;
- DEF3 local/remote `main` synchronized;
- DEF4 canonical roadmap milestone state verified;
- DEF5 Project #2 taxonomy verified;
- DEF6 Release 1.8 foundations inventoried from repository;
- DEF7 current provider/pipeline/persistence architecture inspected;
- DEF8 feature/snapshot/data-quality capabilities inspected;
- DEF9 Python/Streamlit/interoperability foundation inspected;
- DEF10 scope/non-goals frozen;
- DEF11 presentation ownership boundary frozen;
- DEF12 deterministic simulated-live semantics defined;
- DEF13 schema-v3/no-change default reconciled;
- DEF14 `RELEASE_1.9_DEFINITION.md` complete;
- DEF15 execution plan complete/dependency-consistent;
- DEF16 file manifest complete/unambiguous;
- DEF17 all three mutually consistent;
- DEF18 no implementation/package/schema/GitHub-WP mutation;
- DEF19 canonical repository validation passes;
- DEF20 definition-only state ready for human acceptance/GitHub planning.

All applicable gates must PASS.

## Validation
Run non-destructive canonical restore/build/full tests/format/Gitleaks/Markdown links/whitespace/newline/conflict/diff checks. Verify governed Python pins unchanged, no new packages, schema v3, no implementation files, no persistent processes/listeners. Do not mutate runtime state merely to validate docs.

Critically review for parallel pipeline, Streamlit ownership leakage/direct DB bypass, unnecessary dependencies, nondeterministic sleeps, misleading simulated data, OpenTelemetry/ML/backtesting leakage, premature schema evolution, ambiguous paths, weak acceptance proof, or missing lifecycle/failure behavior.

## Stop conditions
Stop with `RELEASE 1.9 DEFINITION BLOCKED` if predecessor merge boundary cannot be proven; main/roadmap state conflicts; #58 is missing/conflicting; accepted foundations materially differ; scope requires a new major decision; new foundational dependency/schema change is mandatory; execution shape remains ambiguous; artifacts contradict; implementation is needed to answer definition; or canonical validation fails.

Report blocker, evidence, partial documentation mutations, and smallest corrective authority.

## Required report
Report:
- PR #224 state/head/discovered main merge SHA and synchronization;
- roadmap/milestone/Project state;
- inspected provider/pipeline/persistence/feature/snapshot/quality/Python evidence;
- final problem/scope/non-goals/architecture/demo path;
- WP count/titles/order/dependency graph;
- manifest CREATE/MODIFY/VERIFY/CONDITIONAL paths;
- DEF1–DEF20 and validation;
- documentation paths changed;
- implementation/package/schema/GitHub-WP/Project mutations all explicitly zero.

## Completion boundary
Successful definition does **not** authorize WP01. It authorizes human acceptance followed by a separate Release 1.9 GitHub Planning Authority, which may later materialize accepted WPs into milestone #58/Project #2. Implementation remains separately authorized.

## Success markers
On success end exactly:

`RELEASE 1.9 DEFINITION COMPLETE`

`RELEASE 1.9: REAL-TIME FINANCIAL DATA VISUALIZATION`

`PREDECESSOR ROADMAP RECONCILIATION BOUNDARY: <main merge SHA>`

`RELEASE 1.9 DEFINITION ARTIFACTS: 3/3`

`RELEASE 1.9 IMPLEMENTATION: NOT AUTHORIZED`

`NEXT AUTHORIZED ACTION: Human acceptance, then define Release 1.9 GitHub Planning under separate authority.`

Do not create WP issues or begin WP01 automatically.

If blocked end exactly:

`RELEASE 1.9 DEFINITION BLOCKED`

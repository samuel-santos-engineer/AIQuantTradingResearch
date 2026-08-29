# Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, scope, reconciliation, acceptance criteria, and governance authority.
- **GPT-5.6 Terra** — PRIMARY implementation/execution authority for approved WP03 source/test mutations, validation, and post-acceptance GitHub lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Work package:

**WP03 — Infrastructure Provider, Persistence & Failure Instrumentation**

Canonical issue:

**#244**

Milestone:

**#59**

Project:

**#2**

Predecessors:

- WP01 / #242 — Closed / Done
- WP02 / #243 — Closed / Done

WP03 consumes the accepted WP01 vocabulary/security contract and the accepted WP02 Application activity/metric contract as immutable upstream inputs.

---

# Canonical inputs

Read in full before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #244
6. accepted WP02 implementation:
   - `src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs`
   - `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs`
   - `src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs`
   - `tests/AIQuantTradingResearch.Application.Tests/PipelineObservabilityTests.cs`
7. exact Infrastructure provider/persistence implementation paths named by the current WP03 manifest
8. relevant Infrastructure tests and architecture tests.

The repository planning artifacts are authoritative for exact WP03 paths/symbols.

Do not invent writable paths.

If WP03's exact implementation/test ownership is not deterministic from the current execution plan/file manifest/issue:
BLOCK before mutation and request a narrow GPT-5.6 Luna reconciliation.

---

# Upstream immutable contract

WP02 established:

- BCL Application observability;
- root `pipeline.execute`;
- five truthful Application stage activities;
- `HistoricalObservationRetrieval` measures only `IHistoricalObservationStore.Retrieve(...)`;
- `DatasetMaterialization` begins only after retrieval;
- no duplicated semantic timing intervals;
- WP03 may nest real Infrastructure activities through ambient `Activity.Current`.

WP03 MUST NOT change:

- WP02 ActivitySource identity;
- WP02 Meter identity;
- WP02 activity names;
- WP02 metric identities;
- WP02 stage timing ownership;
- WP02 business semantics;
- WP02 source/test files unless the current WP03 manifest explicitly authorizes a narrowly defined modification.

Default assumption: WP02 paths are read-only.

---

# WP03 objective

Implement truthful Infrastructure-owned observability for the selected Release 1.10 boundaries covering:

- provider/retrieval operations;
- persistence operations;
- infrastructure failure observations;
- accepted infrastructure latency/count metrics;
- accepted bounded attributes;
- ambient parent/child propagation beneath WP02 Application activities.

Instrumentation must describe actual Infrastructure work, not duplicate Application orchestration durations.

---

# Hard path boundary

Before mutation, print the exact WP03 allowlist from:

- `RELEASE_1.10_EXECUTION_PLAN.md`
- `RELEASE_1.10_FILE_MANIFEST.md`
- issue #244.

Classify each authorized path as:

- `ADD`
- `MODIFY`
- `TEST ADD`
- `TEST MODIFY`

For modified paths, name exact writable symbols/members if the planning contract does so.

No path outside that reconciled allowlist is writable.

If implementation needs an unlisted path:
BLOCK.

Required marker:

`RELEASE 1.10 WP03 PATH CONTRACT: PASS — DETERMINISTIC ALLOWLIST`

---

# Dependency/package authority

Do not assume OpenTelemetry package additions are authorized.

First read WP01/WP03 dependency selection.

Distinguish:

1. BCL `System.Diagnostics` instrumentation;
2. OpenTelemetry API/SDK provider/exporter composition reserved for WP04 or another WP;
3. any exact package dependency explicitly assigned to WP03.

If the planning artifacts do not explicitly authorize an exact package/project mutation needed by implementation:
BLOCK.

No opportunistic package upgrades.

No Python dependency changes.

---

# Architecture boundaries

Preserve:

- Application owns orchestration;
- Infrastructure owns provider/persistence implementation;
- canonical JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- Release 1.8 JSON-over-stdio boundary;
- no direct UI/provider/database bypass.

WP03 MUST NOT implement:

- Worker/interop lifecycle;
- SDK/exporter lifecycle unless explicitly assigned by manifest;
- System Health read model;
- Streamlit health UI;
- schema migration;
- live broker/provider connectivity;
- trading;
- ML/backtesting;
- parallel pipeline;
- WP07 documentation;
- WP08 full release validation.

---

# Telemetry truthfulness rules

Every Infrastructure activity must correspond to a real observable Infrastructure interval.

Provider/retrieval instrumentation:

- starts at the actual Infrastructure retrieval/provider operation boundary;
- ends when that operation returns or throws;
- may be a child of the ambient WP02 `HistoricalObservationRetrieval` activity;
- must not claim ownership of WP02 orchestration duration.

Persistence instrumentation:

- starts at the actual Infrastructure persistence operation boundary;
- ends when persistence returns or throws;
- nests beneath the appropriate ambient Application activity where one exists;
- must not include unrelated upstream/downstream work.

Failure instrumentation:

- records failure at the boundary that actually observes it;
- preserves exception propagation;
- does not convert failures into success;
- does not duplicate exception ownership merely to increase telemetry volume;
- follows WP01 security rules.

Required marker:

`RELEASE 1.10 WP03 INFRASTRUCTURE TELEMETRY TRUTHFULNESS: PASS`

---

# Security/cardinality rules

Prohibit telemetry exposure of:

- secrets;
- API keys/tokens;
- credentials;
- connection strings;
- raw provider responses;
- full SQL statements if they can expose data;
- arbitrary exception messages as metric labels;
- stack traces as metric dimensions;
- GUID/request IDs as metric dimensions;
- timestamps as metric dimensions;
- raw local paths as metric dimensions;
- uncontrolled symbols/tickers as metric dimensions;
- arbitrary dynamic activity/metric names.

Use only the bounded attribute vocabulary accepted by WP01.

Failure detail must be useful but sanitized and bounded.

---

# Git/GitHub lifecycle policy

## During implementation

Git mutations: ZERO.

Do not:

- stage;
- commit;
- push;
- branch;
- merge/rebase;
- tag.

GitHub issue/project mutations are prohibited until every WP03 acceptance gate passes.

## After successful acceptance

This project requires completed work packages to be synchronized in GitHub.

After all WP03 implementation, validation, security, architecture, path, and acceptance gates PASS:

1. close issue #244;
2. set its unique Project #2 item Status to `Done`;
3. preserve Release=`1.10`;
4. preserve milestone #59;
5. keep milestone #59 Open;
6. leave #245–#249 unchanged.

These are the only authorized GitHub mutations.

If final acceptance does not PASS:
perform ZERO GitHub completion mutations.

---

# Phase 0 — Entry audit

Record:

- repository identity;
- branch;
- local HEAD;
- remote `main` if available without mutation;
- `git status --short`;
- staged/untracked paths;
- preserved WP01/WP02 worktree changes;
- issue #244 state;
- issue #244 milestone;
- Project #2 membership;
- Release field;
- Status field;
- milestone #59 state/counts;
- #245–#249 state/status.

Expected GitHub entry:

- #244 Open;
- #244 Backlog;
- Release=1.10;
- milestone #59;
- unique Project #2 item;
- #242/#243 Closed/Done;
- milestone #59 Open.

Emit:

`RELEASE 1.10 WP03 ENTRY BASELINE: ACCEPTED`

---

# Phase 1 — Contract/manifest reconciliation

Read WP03 scope and freeze:

- exact production paths;
- exact test paths;
- exact symbols;
- exact activity names;
- exact metric names/types/units;
- exact parent/child relationships;
- exact failure semantics;
- exact bounded attributes;
- exact package/dependency authority.

Verify compatibility with WP01/WP02.

Emit:

`RELEASE 1.10 WP03 PATH CONTRACT: PASS — DETERMINISTIC ALLOWLIST`

`RELEASE 1.10 WP03 UPSTREAM CONTRACT RECONCILIATION: PASS`

If any required answer is ambiguous:
BLOCK before mutation.

---

# Phase 2 — Baseline validation

Before WP03 mutation, run relevant baseline:

- affected Infrastructure build;
- Infrastructure tests;
- Application tests as needed for integration boundary;
- architecture/no-bypass tests;
- security baseline if repository convention requires it;
- process/listener residue check.

Record exact counts.

Do not repair unrelated failures.

If baseline prevents attribution:
BLOCK.

---

# Phase 3 — Implement Infrastructure observability contract

Within exact authorized WP03 path(s), implement only the frozen Infrastructure observability primitives.

Depending on the manifest, this may include:

- Infrastructure ActivitySource/Meter ownership;
- stable activity names;
- stable metric instruments;
- bounded constants/helpers.

Do not duplicate WP02 source/meter ownership if the accepted contract assigns a separate Infrastructure identity.

Do not introduce a generic telemetry framework.

Do not compose exporters/providers unless explicitly WP03-owned.

---

# Phase 4 — Provider/retrieval instrumentation

Instrument only the exact provider/retrieval symbols authorized by WP03.

Requirements:

- truthful start/end around real Infrastructure retrieval;
- ambient parent from `Activity.Current` where WP02 establishes it;
- correct success/failure status;
- exact accepted metrics;
- bounded attributes only;
- no raw provider payload;
- no business behavior change;
- no retry-policy change;
- no timeout-policy change;
- no exception swallowing.

Verify the Infrastructure retrieval activity is a child of the appropriate WP02 Application retrieval activity when executed through the canonical pipeline.

Emit:

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

---

# Phase 5 — Persistence instrumentation

Instrument only the exact persistence symbols authorized by WP03.

Requirements:

- truthful persistence interval;
- accepted ambient parent;
- exact persistence activity/metric names;
- bounded attributes;
- schema v4 unchanged;
- transaction semantics unchanged;
- query/write semantics unchanged;
- no direct presentation access;
- exception propagation unchanged.

Emit:

`RELEASE 1.10 WP03 PERSISTENCE INSTRUMENTATION: PASS`

---

# Phase 6 — Failure instrumentation

Implement accepted failure telemetry only at actual observing boundaries.

Verify:

- provider failure;
- persistence failure;
- accepted cancellation/timeout semantics if in scope;
- activity status/outcome;
- failure metric recording;
- exception type/category only if bounded and authorized;
- sanitized detail;
- original exception propagation preserved.

No duplicate semantic failure event should falsely imply multiple independent failures.

Emit:

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

---

# Phase 7 — Parent/child topology verification

Run focused integration tests/listeners proving the accepted topology.

At minimum verify, where applicable:

`pipeline.execute`
→ WP02 Application stage
→ WP03 Infrastructure operation

Specifically verify real Infrastructure retrieval nests beneath WP02 `HistoricalObservationRetrieval`.

Verify persistence nests beneath the correct ambient Application stage according to the frozen contract.

No Infrastructure span may replace, rename, or duplicate the WP02 stage.

Emit:

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

---

# Phase 8 — Focused tests

Add/modify only exact WP03 test paths.

Cover as applicable:

- Infrastructure ActivitySource identity;
- Meter identity;
- provider/retrieval activity;
- persistence activity;
- ambient parenting;
- success behavior;
- failure behavior;
- exception propagation;
- metrics;
- bounded attributes;
- absence of sensitive/high-cardinality tags;
- no listener behavior preservation;
- no duplicated WP02 intervals;
- schema/persistence behavior preservation.

Tests must be deterministic and offline.

No live provider/network dependency unless the canonical existing test fixture is explicitly deterministic and authorized.

---

# Phase 9 — Dependency/package audit

Verify exact project/package diff.

Require:

- only explicitly authorized dependency changes, if any;
- otherwise zero project/package mutations;
- no exporter/provider package leakage into wrong layer;
- no unrelated version changes;
- no Python package changes.

Emit:

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS`

---

# Phase 10 — Architecture/no-bypass audit

Run relevant architecture tests and inspect dependencies.

Require:

- Application does not depend on Infrastructure;
- Infrastructure does not move canonical ownership into UI/Python;
- no direct Streamlit SQLite/provider access;
- no alternate pipeline;
- no schema migration;
- no Worker ownership change;
- WP02 contract remains unchanged.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 11 — Security/cardinality audit

Run Gitleaks/security scanning appropriate to all WP03 changed paths.

Inspect telemetry names/tags.

Require all WP01 rules.

Emit:

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

---

# Phase 12 — Focused/full affected validation

Run all issue #244 and plan-required validation.

At minimum:

1. affected build;
2. focused WP03 tests;
3. full Infrastructure test project(s);
4. Application tests relevant to WP02→WP03 integration;
5. architecture/no-bypass tests;
6. security scan;
7. process/listener residue check.

Report exact counts/warnings/errors.

Do not claim WP08/full-release acceptance.

---

# Phase 13 — Functional behavior preservation

Prove observability did not change:

- provider results;
- persistence results;
- exception propagation;
- deterministic/replay provenance;
- schema v4;
- JSON handoff;
- Worker behavior;
- Streamlit behavior.

Emit:

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 14 — WP04 handoff

Verify WP04 can consume the completed infrastructure instrumentation without changing its semantics.

Freeze handoff facts:

- ActivitySource/Meter identities;
- instrument names;
- expected ambient topology;
- failure behavior;
- package/dependency state;
- exporter/provider composition still reserved for WP04 where planned.

Emit:

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

---

# Phase 15 — Exact path/hunk audit

Compare working tree to Phase 0.

Separate:

- pre-existing WP01/WP02/planning changes;
- WP03-created mutations.

Require WP03 delta exactly matches the manifest allowlist.

For modified files, inspect symbol/hunk boundaries.

Zero unexpected paths/hunks.

Emit:

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

---

# Phase 16 — Acceptance matrix

Evaluate every #244/current execution-plan acceptance criterion individually.

For each:

- criterion;
- evidence;
- validation;
- PASS/BLOCK.

Require all PASS.

Emit:

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

Only after this marker may GitHub completion mutations occur.

---

# Phase 17 — GitHub work-package completion

Precondition:

Every prior WP03 gate PASS.

Re-read #244 and its unique Project #2 item.

Require:

- issue #244 is Open or already Closed idempotently;
- milestone #59;
- Release=1.10;
- unique Project item;
- Status=Backlog or already Done idempotently.

Then:

1. close #244 if Open;
2. set Project #2 Status to Done if not Done;
3. preserve Release=1.10;
4. preserve milestone #59;
5. leave milestone #59 Open;
6. leave #245–#249 unchanged.

Maximum authorized GitHub mutations: **2**.

Emit:

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 18 — Post-completion verification

Verify:

- #244 Closed;
- #244 Status=Done;
- #244 Release=1.10;
- #244 milestone #59;
- #245–#249 unchanged;
- milestone #59 still Open;
- #242/#243 remain Closed/Done.

Milestone counts should reflect current authoritative state; do not hard-code them before reading.

Emit:

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 19 — Mutation accounting

Report separately:

## Pre-existing preserved changes
WP01/WP02/planning residue.

## WP03 repository mutations
Exact paths/symbols.

## Git mutations
Must be zero.

## GitHub mutations
Only #244 close and Project Status→Done, maximum 2.

Required:

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

---

# Phase 20 — Next authority

On complete PASS:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

Do not execute WP04 here.

---

# Required final report

Report:

1. model assignment;
2. entry baseline;
3. deterministic allowlist;
4. upstream reconciliation;
5. baseline validation;
6. provider/retrieval instrumentation;
7. persistence instrumentation;
8. failure instrumentation;
9. activity topology;
10. tests;
11. dependency boundary;
12. architecture/no-bypass;
13. security/cardinality;
14. validation counts;
15. functional preservation;
16. WP04 handoff;
17. path/hunk audit;
18. acceptance matrix;
19. GitHub completion mutations;
20. post-completion GitHub state;
21. mutation accounting;
22. next authority.

---

# Success markers

`RELEASE 1.10 WP03 ENTRY BASELINE: ACCEPTED`

`RELEASE 1.10 WP03 PATH CONTRACT: PASS — DETERMINISTIC ALLOWLIST`

`RELEASE 1.10 WP03 UPSTREAM CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP03 INFRASTRUCTURE TELEMETRY TRUTHFULNESS: PASS`

`RELEASE 1.10 WP03 PROVIDER/RETRIEVAL INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 PERSISTENCE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 FAILURE INSTRUMENTATION: PASS`

`RELEASE 1.10 WP03 ACTIVITY TOPOLOGY: PASS`

`RELEASE 1.10 WP03 DEPENDENCY BOUNDARY: PASS`

`RELEASE 1.10 WP03 ARCHITECTURE/NO-BYPASS: PASS`

`RELEASE 1.10 WP03 TELEMETRY SECURITY: PASS`

`RELEASE 1.10 WP03 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.10 WP03 DOWNSTREAM HANDOFF: PASS — WP04 READY`

`RELEASE 1.10 WP03 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

`RELEASE 1.10 WP03 GITHUB WORK-PACKAGE COMPLETION: PASS`

`RELEASE 1.10 WP03 GITHUB COMPLETION POST-VERIFY: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP03 REPOSITORY MUTATIONS: ACCEPTED WP03 PATHS ONLY`

`RELEASE 1.10 WP03 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK before mutation if the WP03 path/symbol/test/package contract is ambiguous.

BLOCK during implementation if:

- an unlisted path is required;
- WP02 must be modified outside explicit authority;
- truthful Infrastructure boundaries cannot be observed;
- a package/dependency change is required but not explicitly authorized;
- instrumentation changes business behavior;
- telemetry duplicates WP02 semantic timing;
- security/cardinality rules fail;
- validation fails due to WP03;
- unexpected paths/hunks appear.

If blocked before `RELEASE 1.10 WP03 ACCEPTANCE: PASS`:
perform ZERO GitHub completion mutations.

If failure occurs after one or both authorized GitHub completion mutations, preserve correct completed state and report exact partial accounting; do not invent rollback authority.

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE PROVIDER, PERSISTENCE & FAILURE INSTRUMENTATION AUTHORITY BLOCKED`

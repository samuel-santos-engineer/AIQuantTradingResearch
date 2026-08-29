# Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Contract & Path Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY authority for contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, path ownership, dependency selection, and read-only/planning decisions.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations, and downstream WP04 execution after Luna freezes a deterministic contract.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

## Authority identity

Release: **1.10**

Work package:

**WP04 — Worker/Interop Lifecycle and Exporter Isolation**

Issue: **#245**

Milestone: **#59**

Project: **#2**

Predecessor:

**WP03 #244 — Closed / Done**

This is a narrow **planning/architecture reconciliation authority** created because the first WP04 Terra implementation authority correctly blocked before implementation mutation.

Confirmed unresolved items:

- exporter technology;
- package IDs, versions, and project-file ownership;
- exact Worker/interop production symbols;
- exact lifecycle-test paths;
- initialization semantics;
- flush/disposal semantics;
- disabled/failure configuration;
- WP05 handoff details.

This authority MUST resolve those ambiguities sufficiently for Terra to execute WP04 deterministically.

It MUST NOT implement WP04.

---

# Accepted entry state

Treat as accepted unless current inspection contradicts it:

- WP01 complete;
- WP02 complete;
- WP03 complete;
- #242–#244 Closed/Done;
- #245 Open/Backlog;
- milestone #59 Open;
- #246–#249 Open/Backlog;
- first WP04 Terra authority made ZERO repository/Git/GitHub mutations;
- WP03 final validation is accepted;
- WP03 Infrastructure telemetry is BCL-only;
- WP03 ActivitySource/Meter: `AIQuantTradingResearch.Infrastructure`;
- WP03 activities: `provider.operation`, `persistence.operation`;
- canonical WP03 ambient topology is frozen.

Emit:

`RELEASE 1.10 WP04 RECONCILIATION ENTRY: PASS`

---

# Authority objective

Freeze a complete, minimal WP04 contract answering all questions Terra needs before implementation.

At minimum freeze:

1. exporter selection;
2. package/dependency selection;
3. exact project-file ownership;
4. exact production paths and symbols;
5. exact test paths and ownership;
6. lifecycle owner;
7. initialization/start semantics;
8. flush/shutdown/disposal semantics;
9. disabled configuration;
10. invalid configuration;
11. exporter initialization failure;
12. runtime/export failure;
13. shutdown/flush failure;
14. failure isolation from core application correctness;
15. bounded resource/configuration metadata;
16. security/cardinality rules;
17. interop/no-bypass preservation;
18. WP05 downstream handoff.

The result must be implementation-ready without requiring Terra to make an architectural choice.

---

# Architectural constraints

All reconciliation decisions must preserve:

- .NET canonical pipeline ownership;
- Release 1.9 canonical visualization JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- Streamlit does not read SQLite;
- Streamlit does not call providers;
- Streamlit does not supervise Worker;
- Release 1.8 JSON-over-stdio boundary remains separate;
- WP03 Infrastructure observability contract;
- no live providers;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no schema migration;
- no direct UI bypass;
- WP05 owns System Health read model/presentation unless an exact minimal upstream contract must be established here.

---

# Mutation boundary

This is a planning authority.

Allowed repository mutations are restricted to the minimum authoritative Release 1.10 planning/architecture artifacts required to record the reconciliation, expected among:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`

Modify `RELEASE_1.10_DEFINITION.md` only if necessary to reconcile a genuine definition-level contradiction; otherwise preserve it.

No production code.

No tests.

No project/package file mutation.

No Git mutation.

No GitHub mutation.

#245 remains Open/Backlog.

---

# Phase 0 — Read-only repository and contract analysis

Read:

1. Release 1.10 definition;
2. execution plan;
3. file manifest;
4. `OPEN_TELEMETRY_SELECTION.md`;
5. #245 read-only;
6. current Worker project;
7. Worker entry point/hosting/lifecycle code;
8. interop/handoff code;
9. Worker tests;
10. Infrastructure/Application tests touching Worker lifecycle;
11. solution/project files;
12. current package graph;
13. existing configuration patterns;
14. WP03 implementation and tests;
15. WP05 planning requirements;
16. current worktree diff/status.

Identify actual candidate symbols/paths rather than inventing new architecture.

Emit:

`RELEASE 1.10 WP04 CURRENT WORKER/INTEROP OWNERSHIP ANALYSIS: COMPLETE`

---

# Phase 1 — Exporter selection

Select exactly one WP04 exporter strategy consistent with Release 1.10 goals and existing architecture.

Evaluate only realistic options supported by the repository and release scope, such as:

- OTLP exporter;
- console exporter;
- in-process/no external exporter;
- another OpenTelemetry exporter only if justified by existing planning/repository evidence.

The selected strategy must support:

- governed observability;
- local deterministic validation;
- exporter failure isolation;
- Worker ownership;
- no Streamlit dependency;
- no requirement for a live external service in permanent tests;
- a truthful WP05 health handoff;
- minimal dependency surface.

Explicitly reject non-selected alternatives and explain why.

Freeze:

`Exporter strategy = <exact selection>`

Emit:

`RELEASE 1.10 WP04 EXPORTER SELECTION: FROZEN`

---

# Phase 2 — Dependency/package contract

Based on the selected exporter, freeze exact dependencies.

For every authorized package specify:

- exact package ID;
- exact version;
- exact owning `.csproj`;
- why required;
- whether direct or transitive;
- whether runtime or test-only.

Prefer the smallest package surface.

Do not add a package merely because it is conventional.

If the selected architecture can remain package-free/BCL-only, explicitly freeze zero package mutations.

Ensure package versions are compatible with the repository's current target framework and dependency policy.

Emit:

`RELEASE 1.10 WP04 PACKAGE/PROJECT CONTRACT: FROZEN`

---

# Phase 3 — Production path and symbol ownership

Map the actual Worker/interop implementation.

Freeze exact existing/new production paths Terra may mutate.

For each path specify exact symbol/method/class responsibility.

Freeze where:

- telemetry provider/exporter is created;
- lifecycle owner resides;
- configuration is parsed;
- initialization occurs;
- shutdown/disposal occurs;
- optional flush occurs;
- failure isolation is enforced;
- any status needed by WP05 originates.

Prefer existing ownership boundaries over introducing new abstractions.

If a new file/class is genuinely required, freeze its exact path, name, responsibility, and dependency direction.

Emit:

`RELEASE 1.10 WP04 PRODUCTION PATH/SYMBOL OWNERSHIP: FROZEN`

---

# Phase 4 — Lifecycle contract

Freeze exact lifecycle semantics.

Answer explicitly:

- owner;
- construction timing;
- initialization timing;
- exactly-once guarantee;
- relationship to Worker start;
- relationship to pipeline execution;
- cancellation token behavior;
- shutdown timing;
- disposal order;
- whether explicit force-flush is used;
- flush timeout/bound;
- behavior if flush fails;
- idempotent disposal expectations;
- process-exit behavior if relevant.

Core rule: telemetry lifecycle must not be recreated per pipeline operation.

Emit:

`RELEASE 1.10 WP04 LIFECYCLE CONTRACT: FROZEN`

---

# Phase 5 — Configuration contract

Freeze the complete WP04 configuration surface.

For each setting specify:

- exact key/name;
- source;
- type;
- default;
- allowed values;
- disabled semantics;
- invalid-value behavior;
- secret/non-secret classification;
- whether it may appear in telemetry;
- cardinality implications.

Avoid unnecessary settings.

Freeze whether exporter is enabled or disabled by default.

Do not hard-code secrets.

Emit:

`RELEASE 1.10 WP04 CONFIGURATION CONTRACT: FROZEN`

---

# Phase 6 — Exporter failure-isolation contract

Freeze exact behavior for:

1. exporter disabled;
2. successful initialization;
3. exporter configuration invalid;
4. exporter initialization throws/fails;
5. export operation fails;
6. exporter destination unavailable;
7. flush times out/fails;
8. disposal fails;
9. Worker cancellation during shutdown.

For every case specify:

- Worker continues or fails;
- pipeline behavior;
- JSON handoff behavior;
- persistence/provider behavior;
- log/telemetry evidence;
- bounded failure/status category;
- WP05-visible state if any;
- retry behavior, if any;
- whether exception propagates.

No vague "best effort" wording. Freeze deterministic semantics.

Emit:

`RELEASE 1.10 WP04 EXPORTER FAILURE-ISOLATION CONTRACT: FROZEN`

---

# Phase 7 — Resource metadata/cardinality/security

Freeze allowed OpenTelemetry resource attributes and any exporter/lifecycle tags.

Require bounded low-cardinality values.

Explicitly forbid:

- credentials;
- API tokens;
- connection strings;
- raw endpoints if sensitive/unbounded;
- symbols/targets as resource dimensions;
- GUID/request IDs;
- raw filesystem paths;
- SQL;
- payloads;
- arbitrary exception messages.

Specify exact service/resource identity if the exporter contract requires it.

Emit:

`RELEASE 1.10 WP04 RESOURCE/CARDINALITY/SECURITY CONTRACT: FROZEN`

---

# Phase 8 — Interop/no-bypass contract

Freeze how WP04 interacts with the canonical JSON handoff and Worker boundary.

Require:

- exporter lifecycle is orthogonal to JSON handoff correctness;
- no exporter dependency in Streamlit;
- no exporter ownership in Python;
- no direct SQLite/provider access from Streamlit;
- no schema change;
- no new JSON-over-stdio coupling;
- no parallel pipeline;
- telemetry failure cannot corrupt handoff payload.

If WP04 needs to publish minimal lifecycle state for WP05, freeze its .NET ownership without implementing the WP05 read model.

Emit:

`RELEASE 1.10 WP04 INTEROP/NO-BYPASS CONTRACT: FROZEN`

---

# Phase 9 — Test path ownership

Inspect existing tests and freeze exact WP04 test paths.

For each path specify what it must prove.

Freeze deterministic coverage for applicable:

- lifecycle initialization exactly once;
- ownership;
- disabled exporter;
- successful exporter;
- invalid config;
- initialization failure;
- export failure;
- unavailable destination;
- flush/shutdown/disposal;
- cancellation;
- no per-pipeline recreation;
- no Streamlit ownership;
- failure isolation;
- resource/cardinality/security;
- no process/listener/exporter residue;
- architecture/no-bypass.

Prefer existing test files. If new test files are necessary, freeze exact paths/names.

Do not require external network access in permanent tests unless explicitly unavoidable and justified.

Emit:

`RELEASE 1.10 WP04 TEST PATH OWNERSHIP: FROZEN`

---

# Phase 10 — WP05 handoff contract

WP05 is:

**System Health Read Model and Streamlit Presentation**

Freeze exactly what WP04 provides downstream without implementing WP05.

Define, as applicable:

- source of truth for exporter/lifecycle state;
- bounded state vocabulary;
- enabled/disabled distinction;
- healthy/available distinction;
- initialization-failure state;
- runtime-export failure state;
- last-success/last-failure timestamp semantics only if required;
- stale/unknown semantics;
- whether state is persisted, in-memory, or represented through the canonical handoff;
- which layer owns transformation into the WP05 read model;
- what Streamlit is forbidden from doing.

Do not let Streamlit inspect exporter internals directly.

Emit:

`RELEASE 1.10 WP04 → WP05 HANDOFF CONTRACT: FROZEN`

---

# Phase 11 — Update authoritative planning artifacts

Write the reconciled decisions into the minimum authoritative planning/architecture files.

The execution plan must contain enough detail for Terra to implement without architectural choice.

The file manifest must list exact WP04 production/test/project paths and mutation type.

`OPEN_TELEMETRY_SELECTION.md` must record exporter/dependency/lifecycle choices if it is the canonical architecture location.

Keep documents mutually consistent.

Emit:

`RELEASE 1.10 WP04 PLANNING ARTIFACT RECONCILIATION: PASS`

---

# Phase 12 — Cross-contract consistency

Re-read all modified and inherited artifacts.

Prove:

- no contradiction with WP01;
- no contradiction with WP02;
- no contradiction with WP03;
- no contradiction with Release 1.10 definition;
- no WP05 implementation leakage;
- package/path/test ownership is exact;
- failure semantics are deterministic;
- Terra has no unresolved architecture choice.

Emit:

`RELEASE 1.10 WP04 CROSS-CONTRACT CONSISTENCY: PASS`

---

# Phase 13 — Terra implementation simulation

Perform a read-only simulated implementation walk.

For every planned Terra mutation answer:

- exact file;
- exact symbol;
- exact dependency;
- exact behavior;
- exact test proving it;
- exact failure semantics.

If Terra would still have to choose among reasonable alternatives, reconciliation is incomplete.

Only if deterministic emit:

`RELEASE 1.10 WP04 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 14 — Scope and mutation audit

Report exact repository mutations made by this Luna authority.

Allowed: only reconciled planning/architecture docs.

Require:

- production code mutations ZERO;
- test mutations ZERO;
- project/package file mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO.

#245 remains Open/Backlog.

Emit:

`RELEASE 1.10 WP04 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP04 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP04 RECONCILIATION GITHUB MUTATIONS: ZERO`

---

# Phase 15 — Downstream execution authority

If and only if the reconciliation is deterministic, authorize resumption of:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

The existing Terra authority should consume the reconciled planning artifacts; it need not be architecturally replaced.

Emit:

`RELEASE 1.10 WP04 CONTRACT/PATH RECONCILIATION: PASS`

`RELEASE 1.10 WP04 → TERRA IMPLEMENTATION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

## Required terminal

Success:

`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION CONTRACT & PATH RECONCILIATION AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION CONTRACT & PATH RECONCILIATION AUTHORITY BLOCKED`

## Blocked rules

BLOCK if Luna cannot freeze a deterministic contract from repository/release evidence without exceeding Release 1.10 scope.

If blocked:

- preserve any valid planning analysis;
- do not mutate production/test/project/package files;
- Git/GitHub mutations remain ZERO;
- #245 remains Open/Backlog;
- WP05 does not start;
- identify the exact governance/architecture decision still required.

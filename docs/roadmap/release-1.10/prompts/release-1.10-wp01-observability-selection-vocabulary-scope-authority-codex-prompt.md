# Release 1.10 WP01 — Observability Selection, Vocabulary & Scope Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY for contract definition, architecture/policy decisions, vocabulary, scope boundaries, dependency-selection governance, acceptance reconciliation, and read-only/planning authorities.
- **GPT-5.6 Terra** — RESERVED for later implementation, tests, validation execution, approved dependency/package changes, Git/GitHub mutations, merge, and publication.
- **GPT-5.6 Sol** — RESERVED for supporting technical analysis, alternatives, synthesis, and non-authoritative review. Sol does not replace Luna or Terra for assigned authorities.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Work package:

**WP01 — Observability Selection, Vocabulary & Scope**

Canonical GitHub issue:

**#242**

Milestone:

**#59**

WP01 is the foundational contract authority for WP02–WP08.

It MUST establish the observability contract that later Terra implementation authorities consume.

It MUST NOT implement downstream instrumentation.

---

# Accepted Release 1.10 capability

> Governed OpenTelemetry-based pipeline/boundary observability plus a truthful Streamlit System Health view.

Preserve:

- .NET pipeline/business ownership;
- canonical governed JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated financial-data provenance;
- Worker/Streamlit independence;
- architecture/no-bypass rules.

Exclude:

- live provider/broker/exchange connectivity;
- trading/execution;
- ML;
- backtesting;
- parallel pipelines;
- direct Streamlit/UI SQLite access;
- direct Python provider access;
- schema migration unless explicitly re-authorized;
- unrelated dependency modernization.

Observability MUST NOT be presented as proof of live market connectivity.

---

# Canonical planning sources

Read before making any decision:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. GitHub issue #242
5. relevant current architecture and dependency-management files.

Use issue #242 and the planning artifacts as the exact WP01 contract.

If they materially disagree:
BLOCK before changing planning artifacts.

---

# Dependency boundary

WP01 is the first Release 1.10 WP.

It depends on the accepted Release 1.9 predecessor architecture and Release 1.10 planning baseline, not on another 1.10 WP.

Downstream chain:

`WP01/#242 → WP02/#243 → WP03/#244 → WP04/#245 → WP05/#246 → WP06/#247 → WP07/#248 → WP08/#249`

WP01 must produce enough deterministic contract detail for WP02 to begin without inventing vocabulary, telemetry semantics, dependency policy, or security/cardinality rules.

---

# Mutation boundary

## Repository

This is a contract/planning authority.

Only WP01-owned planning/contract documentation paths explicitly authorized by the accepted file manifest may change.

If the accepted manifest assigns WP01 no repository mutation, remain read-only and report the contract as authority output.

Do not broaden path ownership.

## Forbidden implementation mutations

Do NOT modify:

- production source;
- tests;
- `.csproj` package references;
- Python dependencies;
- schema/migrations;
- Streamlit runtime code;
- Worker runtime code;
- exporter configuration;
- CI/workflows;
- runtime secrets/configuration.

WP01 may SELECT/APPROVE dependency families and versions only to the extent required by its accepted contract, but MUST NOT install/add them.

## Git

ZERO mutations unless the accepted WP01 contract explicitly authorizes Git mutation. Default and expected: ZERO.

Do not stage, commit, push, branch, merge, tag, or rewrite history.

## GitHub

ZERO mutations.

Do not edit/close issue #242, Project fields, milestone #59, or other issues.

Lifecycle transitions belong to a later execution/acceptance authority.

---

# Phase 0 — Entry audit

Record:

- repository identity;
- current branch;
- local HEAD;
- authoritative remote `main` SHA if available without prohibited mutation;
- `git status --short`;
- staged/untracked paths;
- issue #242 state/title/body;
- milestone #59 state/counts;
- current Project #2 metadata for #242 if available.

Preserve unrelated local work.

Emit:

`RELEASE 1.10 WP01 ENTRY BASELINE: READ-ONLY`

---

# Phase 1 — Reconcile exact WP01 contract

From issue #242 and canonical planning artifacts extract:

- objective;
- in scope;
- out of scope;
- architecture contract;
- provenance/truthfulness contract;
- expected path ownership;
- acceptance criteria;
- validation requirements;
- security requirements;
- completion boundary.

Produce an exact reconciliation table.

Do not silently expand WP01.

Emit:

`RELEASE 1.10 WP01 CONTRACT RECONCILIATION: PASS`

---

# Phase 2 — Inventory current observability foundation

Read current repository evidence and classify what already exists.

Inspect as applicable:

- .NET target/runtime;
- dependency/version centralization;
- logging abstractions;
- `ILogger` usage;
- `Activity` / `ActivitySource`;
- `DiagnosticSource`;
- `Meter` / metrics APIs;
- existing health/status/read-model concepts;
- Worker lifecycle logging;
- provider/infrastructure instrumentation;
- persistence instrumentation;
- JSON handoff/read-model boundary;
- Python logging/telemetry;
- Streamlit health/status presentation;
- test helpers for time/telemetry/lifecycle;
- configuration patterns;
- security/redaction conventions.

Classify each capability:

- `EXISTS — REUSE`
- `PARTIAL — GOVERN/EXTEND LATER`
- `ABSENT — REQUIRED LATER`
- `OUT OF SCOPE`

Do not implement missing pieces.

---

# Phase 3 — OpenTelemetry dependency selection contract

Make the canonical Release 1.10 dependency-selection decision.

Determine from repository architecture and accepted scope which OpenTelemetry .NET packages/components are actually required later.

Distinguish:

1. API/instrumentation dependency;
2. SDK/provider dependency;
3. exporter dependency;
4. hosting/extensions dependency;
5. testing dependency.

For each candidate state:

- selected or rejected;
- exact purpose;
- owning future WP;
- whether required for Release 1.10;
- compatibility with current target framework;
- version-selection rule;
- security/operational implications.

Prefer the smallest dependency surface.

Do NOT select exporters merely because they are common.

If the release requires only in-process governed observability and no external collector/export target has been accepted, preserve exporter isolation and do not imply a production exporter.

If exact package versions can be deterministically selected from repository policy/current ecosystem evidence, record them as the approved later implementation target.

If exact versions cannot be safely fixed without fresh package evidence, define the selection constraint and assign exact resolution to the appropriate Terra implementation authority.

Do NOT modify package files.

Emit one:

`RELEASE 1.10 WP01 OPENTELEMETRY DEPENDENCY SELECTION: ACCEPTED`

or

`RELEASE 1.10 WP01 OPENTELEMETRY DEPENDENCY SELECTION: DECISION REQUIRED`

A decision-required result is blocking if WP02 cannot proceed deterministically.

---

# Phase 4 — Canonical observability vocabulary

Define a finite Release 1.10 vocabulary.

At minimum distinguish:

- trace;
- span/activity;
- metric;
- log;
- pipeline operation;
- pipeline stage;
- boundary;
- provider operation;
- persistence operation;
- handoff/publication operation;
- Worker lifecycle;
- interop operation;
- health state;
- failure/error;
- duration/latency;
- success/failure outcome.

For every term define:

- canonical meaning;
- owner/layer;
- allowed use;
- prohibited ambiguity.

Do not use `health`, `ready`, `live`, `real-time`, `provider`, or `success` ambiguously.

The vocabulary must be consumable by issue #243 without reinterpretation.

---

# Phase 5 — Instrumentation scope map

Define exactly which Release 1.10 boundaries MAY/MUST be observable later.

Map at minimum:

- application pipeline entry/exit;
- major application stages accepted by the architecture;
- provider/infrastructure boundary;
- persistence boundary;
- canonical visualization/read-model publication;
- JSON/file handoff boundary if applicable;
- Worker startup/readiness/shutdown;
- interop invocation/lifecycle where applicable;
- System Health projection inputs.

For each boundary state:

- owner;
- telemetry signal(s): traces / metrics / logs;
- required or optional;
- success semantics;
- failure semantics;
- duration semantics;
- downstream WP owner.

Explicitly identify boundaries that MUST NOT be instrumented as independent business pipelines.

Emit:

`RELEASE 1.10 WP01 OBSERVABILITY SCOPE MAP: ACCEPTED`

---

# Phase 6 — Naming convention

Define canonical naming rules for:

- `ActivitySource` / instrumentation source names;
- activity/span names;
- meter names;
- metric names;
- attribute/tag names;
- health/read-model field names if WP01 owns vocabulary only;
- error/outcome values.

Requirements:

- deterministic;
- stable;
- low ambiguity;
- layer/boundary ownership visible;
- no accidental provider/vendor coupling;
- no secrets/PII identifiers;
- no unbounded dynamic names.

Prefer semantic-convention compatibility where applicable without claiming unsupported standard conventions.

Do not invent a custom semantic convention where standard OpenTelemetry semantics clearly apply.

---

# Phase 7 — Attribute/tag contract

Define an allowlisted attribute vocabulary.

For each allowed attribute specify:

- name;
- meaning;
- type;
- allowed values or bounded domain;
- cardinality classification;
- owning boundary;
- whether permitted on traces;
- whether permitted on metrics;
- whether permitted in System Health projection.

Classify cardinality:

- `STATIC`
- `LOW`
- `BOUNDED`
- `PROHIBITED-HIGH`

Explicitly prohibit or tightly govern:

- raw symbols/tickers as metric dimensions unless bounded/accepted;
- file paths;
- exception messages as metric labels;
- stack traces as attributes;
- arbitrary user/provider payload;
- timestamps as dimensions;
- GUID/request IDs as metric labels;
- secrets/tokens/credentials;
- connection strings;
- sensitive environment values.

Trace correlation identifiers may be allowed where standard and non-sensitive, but not as high-cardinality metric dimensions.

Emit:

`RELEASE 1.10 WP01 ATTRIBUTE/CARDINALITY CONTRACT: ACCEPTED`

---

# Phase 8 — Metrics contract

Define only the metrics required by accepted Release 1.10 behavior.

For each metric specify:

- canonical name;
- instrument type;
- unit;
- description;
- recording boundary;
- allowed attributes;
- cardinality limits;
- success/failure behavior;
- owning future WP.

Possible categories to evaluate, not blindly adopt:

- operation count;
- failure count;
- duration;
- current health/readiness state;
- publication/handoff success;
- lifecycle events.

Avoid duplicate metrics that encode the same fact.

Do not define financial market/business metrics unless accepted Release 1.10 scope requires them.

---

# Phase 9 — Trace/span contract

Define:

- root/parent ownership;
- operation/span boundaries;
- parent-child relationships;
- status/outcome rules;
- exception-recording policy;
- duration ownership;
- correlation across application/infrastructure/handoff boundaries;
- what does NOT cross process boundaries.

Do not create a distributed-tracing claim if Release 1.10 has no accepted cross-process context propagation contract.

Worker/Streamlit independence must remain truthful.

---

# Phase 10 — Logging relationship

Define how existing logging relates to OpenTelemetry.

Clarify:

- whether logs remain existing `ILogger` behavior;
- whether log export is in scope;
- whether trace/span IDs may enrich logs;
- whether duplicate telemetry/logging is prohibited;
- which failures require logs versus trace status versus metrics.

Do not force migration of existing logging unless accepted.

---

# Phase 11 — System Health semantics

WP05 will implement the truthful Streamlit System Health view.

WP01 must define the vocabulary it consumes without implementing it.

Define finite health semantics such as the exact accepted states from the planning contract.

For every state define:

- meaning;
- evidence required;
- what it does NOT prove;
- stale/degraded behavior;
- relationship to Worker readiness;
- relationship to provider/persistence/handoff failures;
- relationship to deterministic/replay/simulated financial data.

System Health MUST NOT imply:

- live broker connectivity;
- live exchange connectivity;
- successful trading capability;
- end-to-end distributed tracing where none exists.

If the accepted plan preserves existing Ready/WarmUp/Empty/Failed states, reconcile observability health semantics with them rather than inventing contradictory state machines.

---

# Phase 12 — Exporter isolation contract

Define exporter policy.

Answer deterministically:

- Is an external exporter required for Release 1.10 acceptance?
- Is console/debug export allowed only for development?
- Is OTLP configuration accepted, optional, deferred, or out of scope?
- Who owns exporter creation/disposal?
- What happens when exporter configuration is absent?
- What happens when an exporter fails?
- Can exporter failure affect the business pipeline?
- Can Streamlit depend on an exporter?

Default architecture requirement unless accepted evidence says otherwise:

**observability/exporter failure must not create a parallel business pipeline or grant Streamlit ownership of Worker telemetry infrastructure.**

WP04 later implements lifecycle/isolation behavior.

---

# Phase 13 — Security and privacy contract

Define mandatory telemetry safety rules.

At minimum:

- no secrets;
- no API keys/tokens;
- no credentials;
- no connection strings;
- no sensitive config values;
- no raw arbitrary provider payloads;
- no uncontrolled exception payloads in attributes;
- no high-cardinality sensitive dimensions;
- bounded naming/attributes;
- safe error classification;
- no security claim based solely on telemetry presence.

Define what may appear in:

- span attributes;
- metric attributes;
- logs;
- health JSON/read model;
- Streamlit System Health.

Map security validation ownership to WP06/WP08 as accepted.

Emit:

`RELEASE 1.10 WP01 TELEMETRY SECURITY CONTRACT: ACCEPTED`

---

# Phase 14 — Performance/failure budget contract

Define qualitative/bounded requirements for later implementation:

- telemetry must not become canonical business state;
- telemetry failure must not corrupt persistence/handoff;
- instrumentation must not create unbounded memory/cardinality;
- exporter behavior must be isolated;
- observability must not materially alter deterministic business outcomes;
- shutdown/disposal must be bounded;
- no orphan telemetry-owned processes/listeners after validation.

If the accepted plan contains numeric budgets, preserve them.

Do not invent arbitrary numeric performance targets without evidence.

---

# Phase 15 — Downstream WP handoff matrix

Produce exact contract handoffs:

## WP02 / #243
Application instrumentation contract to implement.

## WP03 / #244
Infrastructure/provider/persistence/failure instrumentation contract.

## WP04 / #245
Worker/interop lifecycle and exporter isolation contract.

## WP05 / #246
System Health read model/presentation vocabulary and truthfulness contract.

## WP06 / #247
Permanent tests/no-bypass/cardinality/security test obligations.

## WP07 / #248
Documentation/setup/runbook terminology and disclosure obligations.

## WP08 / #249
Full validation/acceptance evidence required.

For every handoff identify exact WP01 decisions that become immutable inputs unless Luna later reopens them.

Emit:

`RELEASE 1.10 WP01 DOWNSTREAM CONTRACT HANDOFF: PASS — WP02–WP08 COVERED`

---

# Phase 16 — Planning artifact update

Use the accepted WP01 path ownership from `RELEASE_1.10_FILE_MANIFEST.md`.

Update only authorized Release 1.10 contract/planning documentation needed to persist WP01 decisions.

Prefer extending existing canonical artifacts over creating duplicate sources of truth.

If a dedicated observability contract document is explicitly authorized by the manifest, create it at the exact authorized path.

Do not modify implementation files.

After editing, reread all affected planning docs and verify no conflict with:

- Release definition;
- execution plan;
- file manifest;
- issue #242 contract.

---

# Phase 17 — WP01 acceptance verification

Verify every accepted WP01 criterion from issue #242 and execution plan.

At minimum require deterministic answers for:

- dependency-selection policy;
- vocabulary;
- observability boundary scope;
- naming;
- attributes/cardinality;
- metrics;
- traces;
- logging relationship;
- health semantics;
- exporter isolation;
- security/privacy;
- downstream ownership.

No criterion may be marked PASS solely because this prompt requested it.

Use repository evidence and persisted planning output.

Emit:

`RELEASE 1.10 WP01 ACCEPTANCE: PASS`

---

# Phase 18 — Implementation prohibition audit

Prove:

- no production source changed;
- no tests changed;
- no packages added;
- no schema changed;
- no runtime config changed;
- no exporter installed/configured;
- no Streamlit implementation changed;
- no GitHub object changed;
- no WP02 work implemented.

If any implementation mutation occurred:
BLOCK.

---

# Phase 19 — Mutation accounting

Enumerate exact repository planning paths changed.

Required expected markers:

`RELEASE 1.10 WP01 REPOSITORY MUTATIONS: CONTRACT/PLANNING PATHS ONLY`

`RELEASE 1.10 WP01 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP01 GITHUB MUTATIONS: ZERO`

If WP01 path ownership requires zero repository mutations, report:

`RELEASE 1.10 WP01 REPOSITORY MUTATIONS: ZERO`

---

# Phase 20 — Next authority

On PASS, WP01's contract becomes the immutable input to:

**Release 1.10 WP02 — Application Pipeline Observability Contract Authority — GPT-5.6 Terra**

The WP02 authority must again explicitly define:

- GPT-5.6 Luna;
- GPT-5.6 Terra;
- GPT-5.6 Sol;
- selected execution model.

Do not execute WP02 here.

Do not close #242 or change Project status under this authority unless a separate lifecycle authority explicitly authorizes it.

---

# Required final report

Report:

1. Model assignment
2. Entry baseline
3. WP01 contract reconciliation
4. Existing observability inventory
5. dependency-selection decision
6. canonical vocabulary
7. scope map
8. naming contract
9. attribute/cardinality contract
10. metrics contract
11. trace/span contract
12. logging relationship
13. System Health semantics
14. exporter isolation
15. security/privacy
16. performance/failure constraints
17. downstream WP02–WP08 handoff
18. changed planning paths
19. WP01 acceptance result
20. exact next authority.

---

# Success markers

`RELEASE 1.10 WP01 CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP01 OPENTELEMETRY DEPENDENCY SELECTION: ACCEPTED`

`RELEASE 1.10 WP01 OBSERVABILITY SCOPE MAP: ACCEPTED`

`RELEASE 1.10 WP01 ATTRIBUTE/CARDINALITY CONTRACT: ACCEPTED`

`RELEASE 1.10 WP01 TELEMETRY SECURITY CONTRACT: ACCEPTED`

`RELEASE 1.10 WP01 DOWNSTREAM CONTRACT HANDOFF: PASS — WP02–WP08 COVERED`

`RELEASE 1.10 WP01 ACCEPTANCE: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Terminal:

`RELEASE 1.10 WP01 — OBSERVABILITY SELECTION, VOCABULARY & SCOPE AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- issue #242 conflicts materially with accepted planning;
- OpenTelemetry dependency selection remains insufficient for WP02;
- vocabulary/boundaries/cardinality/security rules cannot be made deterministic from accepted architecture;
- a required decision would invent new Release 1.10 product scope;
- WP01 path ownership cannot safely persist required decisions;
- any implementation/GitHub mutation occurs;
- WP02 would still need to invent a WP01-owned contract.

Report the exact unresolved decision and smallest next Luna reconciliation step.

Terminal:

`RELEASE 1.10 WP01 — OBSERVABILITY SELECTION, VOCABULARY & SCOPE AUTHORITY BLOCKED`

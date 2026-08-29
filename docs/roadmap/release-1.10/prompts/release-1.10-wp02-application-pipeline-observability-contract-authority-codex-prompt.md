# Release 1.10 WP02 — Application Pipeline Observability Contract Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, architecture, scope, vocabulary, acceptance, governance, and reconciliation authority.
- **GPT-5.6 Terra** — PRIMARY implementation/execution authority for approved source/test/package mutations and validation.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra for assigned authorities.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Work package:

**WP02 — Application Pipeline Observability Contract**

Canonical GitHub issue:

**#243**

Dependency:

**WP01 / #242 — Observability Selection, Vocabulary & Scope**

Downstream:

`WP02/#243 → WP03/#244 → WP04/#245 → WP05/#246 → WP06/#247 → WP07/#248 → WP08/#249`

---

# Purpose

Implement only the **application-layer pipeline observability contract** assigned to WP02.

Consume the accepted WP01 observability selection as immutable input.

WP02 must establish the application-owned OpenTelemetry instrumentation surface required by later infrastructure, lifecycle, health, and validation WPs without absorbing their responsibilities.

---

# Canonical inputs

Read in full before mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #243
6. relevant current application architecture/tests.

WP01 contract is authoritative for:

- OpenTelemetry family selection;
- vocabulary;
- names/naming rules;
- allowed attributes;
- cardinality;
- metrics;
- trace/span semantics;
- health vocabulary;
- logging relationship;
- exporter isolation;
- telemetry security;
- downstream ownership.

Do not reinterpret WP01.

If #243/planning/WP01 materially conflict:
BLOCK before implementation.

---

# Pre-entry state

WP01 produced the authorized untracked artifact:

`docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`

Preserve it.

Do not discard, overwrite, stage, or accidentally absorb unrelated residue unless the accepted WP02 manifest explicitly requires editing it.

Issue #242 remains Open/Backlog; this authority does not change its lifecycle.

---

# Release boundaries

Preserve:

- .NET pipeline/business ownership;
- canonical JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated financial-data provenance;
- Worker/Streamlit independence;
- no-bypass architecture.

WP02 MUST NOT implement:

- provider/infrastructure instrumentation owned by WP03;
- persistence instrumentation owned by WP03;
- Worker/interop lifecycle/exporter isolation owned by WP04;
- System Health read model/Streamlit owned by WP05;
- permanent cross-cutting/no-bypass suite owned by WP06 except focused WP02 tests required by its own contract;
- docs/runbook owned by WP07;
- full release validation/PR readiness owned by WP08;
- live provider/trading behavior;
- schema migration;
- direct UI/SQLite access;
- exporter deployment.

---

# Mutation boundary

Use `RELEASE_1.10_FILE_MANIFEST.md` as the authoritative WP02 path boundary.

## Repository

Only paths explicitly assigned to WP02 may change.

Before mutation, print the exact WP02 allowed-path manifest classified as:

- expected modify;
- expected add;
- validation-only/read-only;
- forbidden.

If implementation requires a path outside that contract:
BLOCK and request Luna reconciliation.

## Packages

Only dependency changes explicitly authorized by WP01 + WP02 planning are permitted.

WP01 selected the OpenTelemetry API/SDK family but deferred exact package versions to authoritative Terra execution.

If WP02 is the owning package-resolution WP, resolve exact compatible package version(s) using repository version policy and authoritative package evidence available in the execution environment.

Requirements:

- smallest dependency surface;
- no exporter package unless explicitly WP02-owned and accepted;
- no unrelated upgrades;
- exact version pinning consistent with repository policy;
- record rationale.

If package resolution belongs to a later WP per accepted plan, do not add it here.

## Git

Repository implementation mutations are allowed; Git history mutations are NOT unless explicitly authorized below.

Default:

- no branch creation;
- no staging;
- no commit;
- no push;
- no merge;
- no tag.

This WP authority prepares and validates the implementation in the working tree only.

## GitHub

ZERO mutations.

Do not edit/close #243, #242, milestone #59, Project #2, or create a PR.

---

# Phase 0 — Entry audit

Record:

- branch;
- local HEAD;
- authoritative remote `main` if available without prohibited Git mutation;
- ahead/behind if determinable;
- `git status --short`;
- staged paths;
- untracked paths;
- WP01 artifact presence;
- issue #243 contract;
- milestone #59 state/counts;
- Project #2 status for #243 if available.

Classify all pre-existing local residue.

Preserve it exactly unless explicitly WP02-owned.

Emit:

`RELEASE 1.10 WP02 ENTRY BASELINE: ACCEPTED`

---

# Phase 1 — Contract reconciliation

Reconcile issue #243 against:

- definition;
- execution plan;
- file manifest;
- WP01 selection record.

Extract exact:

- objective;
- in scope;
- non-scope;
- direct dependency;
- architecture contract;
- provenance contract;
- path ownership;
- acceptance;
- validation;
- security;
- completion boundary.

Emit:

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

---

# Phase 2 — Baseline validation

Before implementation, run the minimum baseline checks required by the accepted plan/repository convention.

At minimum:

- build affected .NET solution/projects;
- focused existing Application tests;
- architecture tests relevant to dependency direction if practical;
- verify no owned processes/listeners are left behind.

Record exact counts/results where tools report them.

If baseline is already failing in an area WP02 will touch, distinguish pre-existing failure from WP02 regression.

Do not repair unrelated baseline failures.

---

# Phase 3 — Resolve exact OpenTelemetry package/API surface

If WP02 owns the first concrete package addition:

Determine the minimal exact package/API set needed for **application-layer instrumentation only**.

Prefer application code depending on stable OpenTelemetry-compatible .NET abstractions such as `System.Diagnostics.ActivitySource`, `System.Diagnostics.Metrics.Meter`, and WP01-approved APIs where architecture permits, rather than coupling application business logic to exporter/provider implementation.

Explicitly decide:

- which project owns package/API references;
- exact package/version if needed;
- whether BCL diagnostics are sufficient for the Application layer;
- whether SDK/provider packages belong later to WP04/infrastructure composition;
- why no exporter dependency is added.

Do not leak SDK/exporter composition into the Application layer unless the accepted architecture explicitly requires it.

Emit:

`RELEASE 1.10 WP02 DEPENDENCY/API SURFACE: ACCEPTED`

---

# Phase 4 — Application observability abstraction

Implement the smallest application-owned observability contract required by WP01/WP02.

The design must:

- preserve dependency direction;
- avoid vendor/exporter coupling;
- avoid telemetry becoming business state;
- expose deterministic instrumentation semantics;
- be testable without external collectors/exporters;
- use WP01 canonical names and attributes;
- keep high-cardinality data out of metric dimensions;
- preserve business behavior when telemetry is absent/unobserved.

Use existing repository patterns where possible.

Do not create a generic telemetry framework beyond Release 1.10 needs.

---

# Phase 5 — Pipeline operation instrumentation

Instrument only application-owned pipeline operations/stages assigned to WP02.

For each accepted operation:

- create/own the correct activity/span boundary;
- record duration via accepted metric contract if WP02-owned;
- record success/failure outcome;
- use allowlisted attributes only;
- preserve exception semantics;
- avoid swallowing/reclassifying business exceptions;
- avoid changing functional output.

No dynamic span names from symbols, IDs, file names, or payloads.

No raw market/provider payload in telemetry.

---

# Phase 6 — Stage/boundary semantics

Implement WP01's application-stage vocabulary exactly.

For every instrumented stage verify:

- stable canonical name;
- correct parent/child relationship;
- correct application ownership;
- correct success definition;
- correct failure definition;
- bounded attributes;
- no infrastructure implementation detail leaking into application telemetry.

WP03 must be able to add provider/persistence child instrumentation without rewriting WP02's contract.

Emit:

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

---

# Phase 7 — Metrics implementation

Implement only WP02-owned metrics from the WP01 contract.

For each:

- correct instrument type;
- canonical name;
- unit;
- description;
- recording point;
- allowlisted attributes;
- bounded cardinality;
- deterministic success/failure behavior.

Do not add financial/business market metrics not accepted by WP01.

Do not add duplicate metrics for facts already represented by the accepted contract.

---

# Phase 8 — Trace/status/error behavior

Implement accepted trace behavior:

- correct ActivitySource ownership;
- correct parent/root semantics;
- status/outcome mapping;
- exception recording policy;
- no exception swallowing;
- no sensitive exception payload in attributes;
- no false distributed-tracing claims.

If no listener/provider is configured, business behavior must remain unchanged.

---

# Phase 9 — Logging relationship

Preserve existing logging behavior unless the accepted contract explicitly requires enrichment.

If correlation enrichment is WP02-owned:

- use standard trace/span correlation;
- do not duplicate entire telemetry payloads into logs;
- do not add secrets/high-cardinality values;
- preserve existing log severity semantics.

No log exporter setup.

---

# Phase 10 — Focused tests

Add/modify only WP02-owned focused tests allowed by the file manifest.

Test at minimum as applicable:

- canonical ActivitySource/name;
- activity emitted for accepted pipeline operation when listener is active;
- no behavior change when no listener exists;
- parent/child application-stage relationships;
- success outcome;
- failure outcome;
- exception propagation;
- metric name/type/unit;
- metric success/failure recording;
- attribute allowlist;
- prohibited high-cardinality metric attributes;
- no secrets/raw payload attributes;
- deterministic behavior;
- no provider/persistence instrumentation incorrectly implemented at application layer.

Tests must not require an external collector/exporter.

---

# Phase 11 — Architecture/no-bypass focused validation

Verify WP02 did not introduce:

- Application → Infrastructure dependency;
- Application → Streamlit/Python dependency;
- direct SQLite access in presentation;
- provider calls from Python;
- exporter/provider composition in application business code;
- parallel business pipeline;
- telemetry-owned canonical state.

Run existing architecture tests relevant to these boundaries.

Any violation is release-blocking.

Emit:

`RELEASE 1.10 WP02 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 12 — Security validation

Verify:

- no secrets/tokens/credentials;
- no connection strings;
- no arbitrary provider payload;
- no uncontrolled exception text as metric labels;
- no GUID/request IDs as metric dimensions;
- no timestamps as dimensions;
- no raw file paths as metric dimensions;
- no uncontrolled symbol/ticker metric dimensions unless explicitly allowed by WP01;
- bounded attribute domains.

Run repository security scanning appropriate to changed/staged-equivalent files without staging.

If the repository's scanner is Git-aware, use a safe mode that does not require mutation.

Emit:

`RELEASE 1.10 WP02 TELEMETRY SECURITY: PASS`

---

# Phase 13 — Focused validation suite

Run all WP02 acceptance validations required by issue #243.

At minimum as applicable:

- build;
- Application tests;
- focused new observability tests;
- Architecture tests;
- dependency health/restore;
- security scan;
- residue/process/listener check.

Record exact commands and counts/results.

No full-release claim.

---

# Phase 14 — Diff/path audit

Compare working-tree changes against:

1. pre-entry residue;
2. exact WP02 file manifest.

Classify every changed/untracked path as:

- pre-existing WP01 authorized artifact;
- WP02 authorized mutation;
- unrelated pre-existing residue;
- unexpected.

Require zero unexpected paths.

Emit:

`RELEASE 1.10 WP02 PATH OWNERSHIP: PASS`

Do not stage files.

---

# Phase 15 — Downstream compatibility

Prove WP03 can consume WP02 without rewriting its application contract.

Specifically confirm:

- provider/persistence instrumentation can nest under/correlate with application operations;
- WP04 can compose SDK/provider/exporter lifecycle outside application business ownership;
- WP05 can later consume governed health/read-model state rather than direct telemetry infrastructure;
- WP06 can add permanent no-bypass/cardinality/security tests;
- no downstream WP is forced to violate WP01.

Emit:

`RELEASE 1.10 WP02 DOWNSTREAM HANDOFF: PASS — WP03 READY`

---

# Phase 16 — WP02 acceptance

Evaluate every issue #243 acceptance criterion individually.

Do not mark PASS based on intent.

Require repository evidence and validation output.

Emit:

`RELEASE 1.10 WP02 ACCEPTANCE: PASS`

---

# Phase 17 — Mutation accounting

Report exact repository mutations introduced by WP02.

Separate:

- pre-existing WP01 artifact;
- WP02 added paths;
- WP02 modified paths.

Required:

`RELEASE 1.10 WP02 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 GITHUB MUTATIONS: ZERO`

Repository mutation marker:

`RELEASE 1.10 WP02 REPOSITORY MUTATIONS: ACCEPTED WP02 PATHS ONLY`

No stage/commit/push/issue update.

---

# Phase 18 — Next authority

On PASS, next:

**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority — GPT-5.6 Terra**

Do not execute WP03.

Do not close #243.

Do not change Project Status.

A later Git/GitHub lifecycle authority must handle commits/issue status if the release workflow requires it.

---

# Required final report

Report:

1. model assignment;
2. entry state and preserved residue;
3. contract reconciliation;
4. baseline validation;
5. dependency/API decision;
6. application observability abstraction;
7. pipeline/stage instrumentation;
8. metrics;
9. trace/error/logging behavior;
10. focused tests;
11. architecture/no-bypass;
12. security;
13. validation results;
14. exact path diff;
15. downstream compatibility;
16. acceptance;
17. mutation accounting;
18. exact next authority.

---

# Success markers

`RELEASE 1.10 WP02 CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP02 DEPENDENCY/API SURFACE: ACCEPTED`

`RELEASE 1.10 WP02 APPLICATION PIPELINE BOUNDARIES: PASS`

`RELEASE 1.10 WP02 ARCHITECTURE/NO-BYPASS: PASS`

`RELEASE 1.10 WP02 TELEMETRY SECURITY: PASS`

`RELEASE 1.10 WP02 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP02 DOWNSTREAM HANDOFF: PASS — WP03 READY`

`RELEASE 1.10 WP02 ACCEPTANCE: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP02 REPOSITORY MUTATIONS: ACCEPTED WP02 PATHS ONLY`

`RELEASE 1.10 WP02 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 GITHUB MUTATIONS: ZERO`

Terminal:

`RELEASE 1.10 WP02 — APPLICATION PIPELINE OBSERVABILITY CONTRACT AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- WP01/#243/planning artifacts conflict;
- exact WP02 path ownership is insufficient;
- required package/API selection cannot be made safely;
- implementation requires WP03+ scope;
- architecture direction would be violated;
- telemetry changes business behavior;
- cardinality/security contract cannot be satisfied;
- focused tests fail due to WP02;
- unexpected paths appear;
- Git/GitHub mutation occurs.

Preserve evidence and report the smallest Luna/Terra reconciliation required.

Terminal:

`RELEASE 1.10 WP02 — APPLICATION PIPELINE OBSERVABILITY CONTRACT AUTHORITY BLOCKED`

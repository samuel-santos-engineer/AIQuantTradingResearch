# Release 1.10 — OpenTelemetry & Pipeline Observability

## Model assignment

- **GPT-5.6 Luna** — primary definition, architecture, policy, acceptance, and read-only reconciliation authority.
- **GPT-5.6 Terra** — later implementation, validation execution, Git/GitHub, merge, and publication authority.
- **GPT-5.6 Sol** — supporting alternatives, synthesis, and non-authoritative review.

**Selected execution model for this definition: GPT-5.6 Luna.**

## Identity and predecessor

Release 1.10 is the open milestone #59, **Phase 4 - Release 1.10: OpenTelemetry & Pipeline Observability**. It follows the completed Release 1.9 visualization release. The planning predecessor is current `origin/main` as verified at definition time; the known post-PR-#241 boundary is `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`.

Preserved boundaries are Release 1.9, tag `v1.9.0` at `e4958721c9a581efbb2552134c00bc146c73f047`, schema v4, the canonical .NET → JSON handoff → Python/Streamlit presentation flow, and all completed Release 1.9 lifecycle state.

## Canonical capability statement

> Release 1.10 adds bounded, governed OpenTelemetry-based pipeline and boundary observability plus a truthful Streamlit **System Health** view, while preserving .NET ownership of business/pipeline semantics, the existing canonical JSON handoff, schema v4, deterministic/replay/simulated provenance, and Worker/Streamlit independence; it explicitly does not add live-provider connectivity, trading, ML, backtesting, a parallel pipeline, or an ungoverned telemetry backend.

The release makes stage timing, throughput, provider behavior, persistence latency, boundary failures, and appropriate Python-boundary health observable without making telemetry a new business source of truth.

## Evidence basis

- `docs/project/ROADMAP.md`: canonical sequence and milestone #59 intent.
- `README.md`: Release 1.10 is the current accepted next milestone.
- `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md`: System Health/OpenTelemetry are explicitly deferred from 1.9.
- `docs/roadmap/release-1.9/RELEASE_1.9_EXECUTION_PLAN.md`: WP08–WP11 preserve lifecycle, handoff, and acceptance boundaries; OpenTelemetry remains future work.
- `docs/roadmap/release-1.9/RELEASE_1.9_FILE_MANIFEST.md`: no exporter/backend was introduced in 1.9.
- `docs/architecture/implementation/OBSERVABILITY_MODEL.md` and `LOGGING_STRATEGY.md`: structured, owned, contextual observability and current semantic evidence are distinct from a telemetry backend.
- `docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md`: Application owns pipeline semantics; Infrastructure owns provider/storage mechanics; the pipeline remains fixed and sequential.
- Milestone #59 live description: governed OpenTelemetry for stage timing, latency, throughput, provider behavior, persistence failures, and a System Health presentation; selection record required before implementation; no ML.

## Scope

### In scope

1. A foundational OpenTelemetry selection record before package/exporter/backend implementation.
2. A minimal versioned observability contract for pipeline stage duration, outcome, throughput/count evidence, provider behavior, persistence latency/failure, and .NET↔Python boundary health.
3. Instrumentation at existing ownership boundaries only: Application pipeline semantics, Infrastructure provider/storage mechanics, Worker/process boundary, and the existing Streamlit adapter.
4. A System Health presentation that consumes the canonical observability/read-model contract and reports bounded health, latency, failure, and provenance context.
5. Deterministic offline fixtures and tests for success, empty, failed, cancellation, restart, stale/missing handoff, and telemetry-disabled/degraded behavior.
6. Explicit sampling/redaction/cardinality rules, correlation identity, clock/timing policy, exporter failure isolation, and bounded shutdown.

### Out of scope

- Real broker/provider or live market connectivity, credentials, trading, order/portfolio management.
- ML training/evaluation, explainability, backtesting, or 2.0+ capabilities.
- A second/parallel pipeline, scheduler, DAG, generic RPC, or replacement of the JSON-over-stdio boundary.
- Direct Streamlit access to SQLite/providers, UI-side business logic, or a new presentation framework.
- Schema redesign or migration unless a later accepted contract proves a minimal persistence requirement; telemetry is not persisted by default.
- Cloud deployment, authentication, hosted telemetry, vendor lock-in, or an exporter/backend choice before the selection record.
- Dependency modernization unrelated to the selected telemetry stack.

## Architecture and truthfulness contract

The existing five-stage pipeline and its Application contracts remain canonical. Instrumentation observes execution and must not change stage ordering, result identity, replay semantics, or persistence truth. Domain remains telemetry-free. Application owns semantic event names and correlation fields without referencing exporter APIs. Infrastructure owns provider/storage instrumentation and adapter mechanics. Worker owns process-lifecycle and boundary timing. Streamlit consumes only the governed handoff/read model and presents health; it does not query SQLite, invoke providers, or supervise Worker processes.

Telemetry provenance must distinguish historical, deterministic replay, simulated live, delayed provider, real-time provider, and live broker data. Release 1.9 simulated/replay evidence remains explicitly non-live. A System Health row may say “unavailable,” “degraded,” or “not collected” rather than infer health from missing telemetry. Telemetry failure never turns a successful/failed pipeline result into a different business result.

Required invariants:

- no secrets, raw credentials, provider payloads, unrestricted exception text, or high-cardinality user/data values in telemetry;
- bounded metric dimensions and stable correlation/run identity;
- exporter/backend failure is isolated, observable, and cannot block or alter canonical pipeline completion;
- cancellation and shutdown flush only within an explicit bounded budget;
- instrumentation is disabled or safely degraded when no approved exporter is configured;
- read-model revision and schema-v4 semantics remain unchanged unless separately authorized.

## Impact classification

| Surface | Release 1.10 planning classification |
|---|---|
| SQLite schema/persistence | **NO CHANGE EXPECTED**; telemetry is operational and non-authoritative. Any exception requires a new schema authority. |
| Canonical JSON/read model | **CHANGE REQUIRED** only for bounded health metadata if proven necessary; version additively and preserve old consumers. |
| JSON-over-stdio | **NO CHANGE EXPECTED**; boundary telemetry is additive and must not pollute protocol stdout. |
| File handoff | **NO CHANGE EXPECTED**; health reads use existing governed handoff. |
| Python dependencies | **DECISION REQUIRED** through an OpenTelemetry selection record; no installation in planning. |
| .NET dependencies | **DECISION REQUIRED** through the same selection record; no package change in planning. |
| Streamlit | **NO VERSION CHANGE EXPECTED**; System Health is an additive consumer surface. |
| Configuration | **CHANGE REQUIRED** only for bounded opt-in telemetry configuration with safe defaults and no secrets. |
| Persisted state | **NO CHANGE EXPECTED**. |

## Work-package sequence

| WP | Title | Primary authority/model | Boundary |
|---|---|---|---|
| 01 | Observability Selection, Vocabulary & Scope | Luna definition | Select OpenTelemetry stack and stable semantic vocabulary; no implementation. |
| 02 | Application Pipeline Observability Contract | Terra after Luna | Additive technology-neutral events/metrics and correlation semantics at existing Application boundaries. |
| 03 | Infrastructure Provider, Persistence & Failure Instrumentation | Terra | Observe existing provider/storage mechanics without changing behavior or schema. |
| 04 | Worker/Interop Lifecycle and Exporter Isolation | Terra | Bound process/boundary timing, cancellation, restart, redaction, and exporter failure isolation. |
| 05 | System Health Read Model and Streamlit Presentation | Terra | Render canonical health state through the existing handoff; no direct DB/provider access. |
| 06 | Permanent Observability and No-Bypass Tests | Terra | Layer-appropriate deterministic tests, security, compatibility, and residue proof. |
| 07 | Documentation, Developer Setup & Operational Runbook | Terra | Truthful setup, local simulated/replay disclosure, troubleshooting, and operational guidance. |
| 08 | Full Validation, Acceptance & PR Readiness | Terra | Fresh release gates, exact manifest, security/residue proof, dedicated branch → PR → verification; no automatic merge. |

Each WP requires the full Luna/Terra/Sol map, names its selected execution model, and must not consume later-release scope. Detailed GitHub issues are not created by this definition.

## Acceptance matrix

| Gate | Measurable outcome |
|---|---|
| Contract | Stable versioned vocabulary, correlation identity, timing units, outcome states, and backward-compatibility rules. |
| Architecture | No Domain telemetry dependency; no provider/UI/SQLite bypass; existing pipeline and handoff remain canonical. |
| Provenance | Every displayed claim identifies the applicable historical/replay/simulated/provider provenance and never overclaims live data. |
| Health | Ready, warm-up, empty, failed, stale, degraded, and unavailable states are deterministic and distinguishable. |
| Failure/resilience | Telemetry/exporter failure, timeout, cancellation, restart, and missing handoff cannot corrupt business results or orphan owned processes. |
| Security | Redaction, bounded cardinality, secret scan, dependency/license review, and no protocol stdout contamination pass. |
| Compatibility | Existing Release 1.9 consumers and schema v4 remain valid; additive versioning is proven. |
| Tests | Focused WP gates, architecture/no-bypass tests, full .NET/Python regressions, and deterministic System Health tests pass. |
| Residue | No owned process, listener, temporary handoff, database sidecar, or telemetry-buffer residue remains after bounded runs. |
| Documentation | Commands, paths, versions, simulated-data warnings, configuration defaults, and troubleshooting are validated. |

Mandatory release acceptance is separate from informational telemetry volume/latency observations. Exact test totals are frozen only by each execution authority from the live predecessor baseline.

## Validation strategy

Later execution must run focused WP suites, build, full .NET and governed Python suites, Streamlit checks, `pip check`, schema-v4/no-bypass checks, security/Gitleaks, documentation/link/command checks, compatibility checks, lifecycle/restart/cancellation, exporter failure/degraded behavior, and complete process/listener/file/database residue audits. Tests use fixed synthetic/replay inputs, explicit clocks/seeds where applicable, and no live provider/network dependency. Any package, schema, exporter, or backend addition requires the accepted selection/contract authority first.

## Risks and controls

| Risk/trigger | Impact | Control / owner | Blocking? |
|---|---|---|---|
| Unselected telemetry package/backend | ungoverned dependency or vendor lock-in | WP01 selection record | Yes |
| Instrumentation changes timing/behavior | altered pipeline truth | additive observation, regression proof; WP02–04 | Yes |
| High-cardinality or sensitive attributes | privacy/cost/security exposure | allowlist, redaction, bounded dimensions; WP01/04 | Yes |
| Exporter blocks shutdown | process/listener residue | bounded flush and cancellation; WP04 | Yes |
| UI bypasses canonical data | contradictory health claims | read-model-only architecture tests; WP05/06 | Yes |
| Provenance overclaim | users mistake simulation for live health | explicit provenance states and docs; WP05/07 | Yes |
| Windows/local runtime variance | irreproducible acceptance | isolated deterministic fixtures and owned cleanup; WP04/06 | Yes |
| Test brittleness/telemetry absence | false acceptance | semantic assertions plus safe unavailable state; WP06 | Yes |
| Scope leakage into ML/backtesting/cloud | release boundary loss | explicit exclusions and PR diff gate; WP08 | Yes |

## Planning artifacts and next authority

Canonical planning artifacts are:

- `RELEASE_1.10_DEFINITION.md` — this contract;
- `RELEASE_1.10_EXECUTION_PLAN.md` — WP sequence and release gates;
- `RELEASE_1.10_FILE_MANIFEST.md` — exact path ownership and exclusions.

No GitHub planning objects are created by this authority. The exact next authority is:

**Release 1.10 GitHub planning materialization authority — GPT-5.6 Terra**, after human acceptance of this definition and its planning artifacts.

## Planning-only boundary

This authority created no implementation, test, package, schema, runtime, Git, or GitHub state. Release 1.10 remains planned and implementation-unauthorized.

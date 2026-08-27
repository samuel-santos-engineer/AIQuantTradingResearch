# Release 1.9 — Real-Time Financial Data Visualization — Execution Plan

## Governing boundary

This plan derives from `RELEASE_1.9_DEFINITION.md` at predecessor merge
`3a02f035a253e4e16f479e1866c9a5195f5cfbdb`. Execute only after human
acceptance and a separate Release 1.9 GitHub Planning Authority. The graph is
linear: WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 →
WP11 → WP12. No work package is executed by this definition.

## WP01 — Release & Repository Preflight

Freeze predecessor, #58, canonical persistence schema v4, current test baseline, Python foundation,
and zero premature implementation. Read-only only; stop on drift.

## WP02 — Deterministic Simulated-Live Replay Semantics

Implement a bounded `IObservationSource` replay adapter with fixed ordered
fixture, target, UTC instants, decimals, logical ticks, replay identity,
restart, duplicate, cancellation, and finite test mode. No provider calls,
second ingestion path, or sleep-based tests. Verify repeated identical replay;
stop if a dependency/provider is required.

## WP03 — Incremental Existing-Pipeline Orchestration

Feed each replay increment through the existing five-stage pipeline and retain
snapshot/catalog/idempotency evidence. Application owns orchestration and
Worker owns explicit mode/configuration. No topology or schema change. Verify
five-stage evidence and finite repeated replay; stop on parallel-path pressure.

## WP04 — Presentation Read-Model Contract

Define bounded versioned Application display contracts for price/time,
latest/count/window, feature warm-up/value, snapshot/version, and
pipeline/quality/failure; implement atomic local handoff mechanics. No direct
SQLite UI reads or arbitrary payloads. Stop if schema, HTTP/service, or a new
dependency is required.

## WP05 — Streamlit Presentation Foundation

Add the minimal governed Streamlit entry point and Worker-owned startup/shutdown
configuration consuming only the read model. Prove simulated label, bounded
refresh, safe empty/failure state, and owned-process cleanup. No dashboard
framework or System Health.

## WP06 — Evolving Financial Visualization

Render truthful price/time, latest observation, count/window, and supported
metadata as replay advances. Verify deterministic sequential frames; do not
invent indicators or predictive charts.

## WP07 — Feature and Data-Quality Visualization

Render only `simple-return-lag-1-v1` values/warm-up and supported snapshot,
validation, pipeline, and idempotency state. Stop if the view needs unsupported
semantics; do not add ML or quality logic.

## WP08 — Lifecycle, Resilience, and Deterministic Demonstration

Prove restart, cancellation, bounded refresh, process/listener ownership,
temporary handoff/database residue, and finite local demonstration. No
OpenTelemetry platform work. Stop on cross-process ownership ambiguity.

## WP09 — Permanent Integration and Architecture Tests

Make replay → pipeline → read model → Streamlit permanently deterministic at
the right layers and enforce no UI/provider/SQLite bypass. No browser automation
or external test framework. Stop for a proven predecessor defect.

## WP10 — Architecture, Documentation, and Developer Alignment

Align current-state architecture, setup, simulated-data warning, lifecycle,
security, troubleshooting, and branch/PR workflow. Do not define 1.10/2.0.

## WP11 — Full Integration and Acceptance

Independently prove the deterministic portfolio path, regressions, canonical persistence schema-v4,
security, residue, and exclusion gates. No closure integration or scope growth.

## WP12 — Closure and PR Readiness

Inventory governed artifacts and prove the dedicated release-branch candidate
is ready for a separate commit/PR/merge authority. Direct push to `main` is
prohibited.

Each WP requires predecessor closure, manifest-owned artifacts, objective,
scope/non-goals, architecture areas, deterministic verification, completion
evidence, and a stop condition before successor authorization.

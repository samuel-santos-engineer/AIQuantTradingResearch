# Release 1.12 Execution Plan

Authoritative identity: `Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization`.

## Dependencies and work packages

The dependency graph is:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

1. **WP01 — Release Contract, Deployment Architecture & Reproducibility Boundary** — GPT-5.6 Luna. Freeze manifests, invariants, deployment boundary, and acceptance evidence.
2. **WP02 — Productionized Container & Runtime Composition** — GPT-5.6 Terra. Build the minimal reproducible application image without changing product architecture.
3. **WP03 — GHCR Publication & Azure F1 Deployment Automation** — GPT-5.6 Terra. Publish free-registry digests and automate only the approved F1 resource envelope.
4. **WP04 — Persistent SQLite Initialization, Data Update & Recovery** — GPT-5.6 Terra. Implement and validate `/home` initialization, update, integrity, recovery, and DELETE journal operation.
5. **WP05 — Twelve Data Runtime Configuration, Secrets & Bounded Automation** — GPT-5.6 Terra. Add only secret-safe bounded refresh and deterministic failure isolation.
6. **WP06 — Public Streamlit/System Health Deployment & Truthful Diagnostics** — GPT-5.6 Terra. Connect only governed read models and expose truthful bounded health/provenance.
7. **WP07 — Deployment Stability, Recovery, Cost & No-Bypass Validation** — GPT-5.6 Terra. Validate restarts, recycles, redeployments, cost, security, and architecture boundaries.
8. **WP08 — Documentation, Operational Runbook & Release Acceptance** — GPT-5.6 Luna final acceptance; Terra may perform only separately authorized validation/publication/lifecycle mutations.

Every WP must name Luna/Terra/Sol, its exact paths, dependencies, mutation boundary, acceptance marker, validation commands, and lifecycle rule. After an exact acceptance marker, its issue may be closed and Project Status set to Done only when automation has not already done so. Milestone #63 remains open until final release completion.

## Validation matrix

| Gate | Required evidence |
|---|---|
| Repository/Git | clean scope, exact manifest, no unrelated work, no forbidden release mutation |
| Build/tests | relevant .NET build/tests, Python tests, Streamlit 1.61.1 and dependency health |
| Security | Gitleaks 8.30.1 policy, no credentials/API keys/secrets in Git, image, logs, or output |
| Container | reproducible build, digest and provenance, Linux runtime and health |
| Azure F1 | West Central US, F1/Free plan, HTTPS, required settings, complete inventory |
| Persistence | `/home` SQLite integrity, DELETE journal, restart/recycle/redeploy recovery |
| Provider | bounded Twelve Data success, missing/invalid/network failure isolation, recovery |
| Presentation | canonical read-model chain, truthful System Health, no Streamlit bypass |
| Cost/cleanup | no paid dependency, `$0.00` recurring infrastructure, teardown/read-back evidence |

## Fixed constraints

No Azure SQL, Azure Files, Container Apps, mandatory ACR, paid tier/networking/monitoring, live trading, ML/backtesting, schema migration, production SLA, or Release 2.0 scope. Azure remains deployment-only. The inherited strict cost marker is:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

## Lifecycle and mutation audit

Planning may create only the Release 1.12 artifacts and the approved milestone/Project/issue governance objects. It must not alter Initiative-1.11 issues #252–#257 or milestone #62. Implementation WPs may mutate only their separately authorized surfaces. Tags, GitHub Releases, and milestone closure require final release authority.

## Handoff

Next authority: GPT-5.6 Luna — WP01 Release Contract, Deployment Architecture & Reproducibility Boundary. No implementation or Azure execution is authorized by this plan.

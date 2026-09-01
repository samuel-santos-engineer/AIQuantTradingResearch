# Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization

## Governance

Release 1.12 is the product release following Release 1.10 and preceding Release 2.0. Product Release 1.11 remains abandoned/nonexistent. Initiative-1.11 is a completed, non-release feasibility predecessor and is not reclassified as Release 1.12.

Model map: GPT-5.6 Luna owns contract, architecture, governance, and final acceptance; GPT-5.6 Terra owns implementation and empirical execution under explicit WP authority; GPT-5.6 Sol provides supporting analysis only. Selected planning model: GPT-5.6 Luna.

## Capability and inherited feasibility

Release 1.12 makes the already-qualified bounded reference deployment reproducible and supportable. It consumes, and does not repeat, Initiative-1.11 feasibility evidence:

- Azure App Service Linux F1 in West Central US with custom Docker and public HTTPS.
- Persistent `/home` with SQLite; DELETE journal mode is selected and WAL is not selected.
- Public/free GHCR distribution and bounded Twelve Data connectivity with runtime-only secrets.
- Public Streamlit/System Health reference presentation and the existing .NET/Python boundaries.
- `ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`.

The result is a bounded recruiter/reference/demo environment, not production hosting. F1 limitations remain 60 CPU minutes/day, 1 GB storage, shared capacity, throttling, cold starts, and no SLA.

## Scope

In scope: deterministic container composition; free image publication; reproducible F1 provisioning; `/home` SQLite initialization/update/recovery; secret-safe bounded Twelve Data refresh; public Streamlit/System Health; restart/recycle/redeploy recovery; low-cost diagnostics; deployment, rollback, secret rotation, cost, teardown, and acceptance runbooks.

Out of scope: Azure SQL, Azure Files, Container Apps, mandatory ACR, paid tiers/networking/monitoring, live trading, orders, portfolio management, ML, backtesting, production SLA/HA claims, unrelated schema migration, parallel pipelines, and any Streamlit SQLite/provider/Worker-supervision bypass.

Release sequence: `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`. Release 2.0 remains Lightweight Machine Learning Evaluation and does not absorb deployment scope.

## Contracts and acceptance

The canonical pipeline remains .NET-owned with atomic JSON/read-model handoff to Python and Streamlit. Provenance must remain deterministic, replay, or simulated where applicable; no live-market claim is permitted. Existing schema and protocol contracts remain unchanged unless separately authorized.

Release acceptance requires every WP marker, reproducible image digest/provenance, HTTPS, persistent `/home`, SQLite integrity with DELETE mode, bounded Twelve Data success/failure behavior, secret absence from Git/images/logs/public output, truthful health, restart/recycle/redeploy recovery, no-bypass checks, complete resource inventory, `$0.00` recurring infrastructure, and cleanup evidence.

## Boundary

This definition authorizes planning only. It creates no Azure resources, Docker/GHCR state, provider requests, implementation, tag, GitHub Release, or merge. Each WP requires its own authority and explicit model map.

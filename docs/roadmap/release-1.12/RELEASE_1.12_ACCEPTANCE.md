# Release 1.12 Acceptance Record

## Status

WP01-WP07 are accepted and lifecycle-complete. WP08 documentation, operational runbook, final substantive acceptance, and later lifecycle/publication work remain in progress. This document does not claim that Release 1.12 is published, tagged, closed, production-ready, highly available, or covered by an SLA.

## Accepted facts

Release 1.12 implements and stabilizes a public reference/demo deployment while preserving the .NET-owned pipeline, JSON handoff, and read-only Python/Streamlit presentation boundary.

```text
Current region: West US 2
Plan: Azure App Service Linux F1/Free
Image: sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
Database path: /home/data/aiquant.db
SQLite schema: 4
SQLite journal mode: delete
Recurring infrastructure architecture target: $0.00
```

The accepted runtime uses custom Docker, public/free GHCR, public/default HTTPS/DNS, bounded Twelve Data configuration, persistent `/home`, and public Streamlit/System Health. `TwelveData__ApiKey` is configured at runtime by name only; its value is never included in documentation or acceptance evidence.

## Historical evidence and regional topology

Initiative-1.11 is historical Azure F1 feasibility evidence. It is not Product Release 1.11. West Central US remains historical accepted evidence. The deterministic constrained-infrastructure recovery order is West Central US → West US 2 → Central US → South Central India. A recovered target requires independent user-controlled runtime-secret configuration and is not production HA or automatic failover.

## Current deployment and operational verification

WP07 accepted restart, bounded recycle-equivalent, and same-immutable-digest redeploy continuity. These scenarios preserved `/home` persistence, schema v4, DELETE journal mode, integrity and quick checks, and public Streamlit/System Health recovery without claiming a quantitative SLA.

Operational procedures and safe verification commands are maintained in [Release 1.12 Operations Runbook](../../guides/RELEASE_1.12_OPERATIONS_RUNBOOK.md). The accepted WP04 verifier remains the governed tool for fresh persistence evidence, with a new RunId for each qualifying execution.

## Limitations and non-goals

This is a bounded public reference/demo environment. F1 shared capacity, throttling, cold starts, and service limits apply. The architecture does not adopt Azure SQL, Azure Files, Container Apps, mandatory ACR, paid monitoring/networking, load balancers, Traffic Manager, Front Door, automatic paid upgrades, production HA, or an SLA.

The release does not authorize live trading, orders, portfolio management, ML, backtesting, a schema migration, automatic secret copying, or Release 2.0 work.

## Remaining WP08 and later release work

WP08 must complete its documentation validation, final substantive acceptance, lifecycle transition, and any separately authorized release publication. Until then, milestone #63 remains open and no Release 1.12 publication or closure claim is made.

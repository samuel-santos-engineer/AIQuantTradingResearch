# Azure F1 Feasibility Execution Plan

## Authority and ordering

Exactly six sequential work packages are defined. No predecessor may be bypassed:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06`

Each represented issue, if later authorized, must remain open until its exact acceptance marker passes; then it is closed and its Project status is set to Done, unless a later authority explicitly defers that lifecycle.

## Work packages

### WP01 — Feasibility Contract, Evidence Matrix & Resource Plan

Model: GPT-5.6 Luna. Freeze the matrix, resource inventory, security/redaction, strict-zero accounting, cleanup, allowed probe paths, and PASS/NOT FEASIBLE rules. No Azure resources.

Acceptance: `AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

### WP01 completion record

`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

`AZURE F1 WP01 LIFECYCLE: ARTIFACT-GOVERNED — NO GITHUB ISSUE BY DESIGN`

This is the completed WP01 planning record. It records contract and resource-plan readiness only; it does not claim empirical Azure feasibility, create Azure resources, or authorize WP02 execution.

### WP02 — Minimal Docker + App Service F1 Execution Probe

Model: GPT-5.6 Terra. Using only an isolated probe, prove actual F1 availability, custom Linux Docker startup, public HTTPS where available, `/home` enablement/writability, restart/recycle, image redeployment, and exact resource inventory.

Acceptance: `AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

### WP03 — Persistent SQLite Filesystem, Locking & Journal Qualification

Model: GPT-5.6 Terra. Test CRUD, transactions, bounded concurrent reads/writes, lock/busy behavior, restart/recycle/redeployment persistence, integrity/quick checks, interruption recovery where safe, rollback-journal, and WAL. Select no journal mode until evidence exists.

Acceptance: `AZURE F1 WP03 — SQLITE PERSISTENCE & JOURNAL QUALIFICATION: PASS`

### WP04 — Twelve Data Outbound Connectivity, Secrets & Failure Isolation

Model: GPT-5.6 Terra. Prove DNS/TLS/HTTPS, safe secret injection, one minimal real request only under a later explicit execution authority, timeout/error behavior, no secret leakage, and isolation from product architecture.

Acceptance: `AZURE F1 WP04 — TWELVE DATA CONNECTIVITY & SECRET ISOLATION: PASS`

### WP05 — F1 Resource Envelope & Strict-Zero-Cost Qualification

Model: GPT-5.6 Terra. Measure cold start, memory, CPU and CPU-minute usage, storage/image/log growth, headroom, registry/monitoring costs, full resource inventory, and actual recurring cost.

Acceptance: `AZURE F1 WP05 — RESOURCE ENVELOPE & STRICT-ZERO-COST: PASS`; required proof includes `ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`.

### WP06 — Feasibility Acceptance, Cleanup & Architecture Decision

Primary model: GPT-5.6 Luna. Terra may perform only explicitly authorized validation/cleanup. Reconcile all evidence, select exactly `FEASIBLE` or `NOT FEASIBLE`, and prove cleanup/disposition and zero unintended recurring cost. No Phase B authorization follows automatically.

Acceptance: `AZURE F1 WP06 — FEASIBILITY ACCEPTANCE & CLEANUP: PASS`

## Current documentation evidence

Access date: 2026-08-30. These sources establish hypotheses only:

| Source | URL | Claim | Evidence class |
|---|---|---|---|
| Configure a Custom Container | https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container | Linux custom-container `/home` persistence is controlled by App Service storage settings; writes outside persistent paths are not durable | Documentation, not empirical |
| Quickstart: Run a Custom Container on App Service | https://learn.microsoft.com/en-us/azure/app-service/quickstart-custom-container | F1 is presented as a selectable App Service tier and custom-container workflow exists | Documentation, not empirical |
| App Service Plans overview | https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans | Free/Shared tiers are distinct from dedicated tiers; availability/features depend on OS, region, and tier | Documentation, not empirical |
| App Service pricing | https://azure.microsoft.com/en-us/pricing/details/app-service/ | Pricing must be checked for the actual account/region and does not by itself prove zero total cost | Documentation/pricing context, not empirical |
| Operating System Functionality | https://learn.microsoft.com/en-us/azure/app-service/operating-system-functionality | App Service applications can make outbound network connections; exact candidate behavior still requires execution | Documentation, not empirical |

## Execution stop rules

Stop immediately before broader mutation if an Azure subscription/region, F1 capability, registry path, secret policy, cost, or cleanup authority is unavailable. Preserve redacted evidence. `NOT FEASIBLE` is an accepted terminal decision. No fallback to Hugging Face, Container Apps/Azure Files, or Release 2.0 is automatic.

## GitHub representation boundary

This planning authority creates no milestone, issue, Project item, Release option, label, or other GitHub object. The initiative is non-release work and must not be assigned the `2.0` Project Release value or an invented `1.11` value. Any later issue tracking, if needed, requires a separate governance decision and must preserve the canonical `1.10 -> 2.0` sequence. No issue or Project item may be closed from this planning authority.

## Next authority

After WP01’s exact acceptance and any separately authorized governance lifecycle, the next execution authority is GPT-5.6 Terra for WP02. Terra may create only the isolated resources explicitly named by the accepted WP01 contract.

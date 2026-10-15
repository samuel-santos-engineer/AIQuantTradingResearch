# Release 1.14 WP07 — Administrative REST Routing and Azure App Settings Credential Confinement Design

## Role and authority

You are Codex working in `samuel-santos-engineer/AIQuantTradingResearch`. This is a **planning-only, repository-read-only design investigation**. Produce a chat-only report; do not write, edit, stage, commit, push, open PRs, or create issues. Do not run applications, builds, tests, Workers, databases, containers, Azure CLI, authentication, network probes, or deployments. Public documentation research is permitted only if available without authentication; cite URLs and clearly distinguish documentation from repository evidence. If uncertain, stop and report the gap.

**Non-negotiable:** `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged. No public Worker activation, A2/SQLite implementation selection, new storage service, or new recurring Azure expense is authorized. Existing approved `/home` SQLite is the sole permitted production storage candidate; diagnostics must never open it or its sidecars. Multiple-instance and overlapping-revision analysis is **deferred for this diagnostic-design phase**, not waived for later public activation.

## Owner-approved design decisions

1. A **permanent**, versioned administrative diagnostic REST capability for WP07 and future releases, **disabled by default**.
2. Passive, fixed-schema, read-only observations only. No shell execution, arbitrary paths, SQL, Worker spawning, active safety qualification, or privileged helper.
3. **Azure App Service application settings** are the preferred credential storage mechanism; do not introduce Key Vault or other Azure resources without new approval.
4. Dedicated, cryptographically random 256-bit API key, separate from Twelve Data and Vike credentials; a dedicated request header is a candidate (e.g., `X-AIQ-Admin-Key`). Keep a separate disabled-by-default enablement setting. Exact names and header choice remain design proposals, not deployed facts.
5. The initial routing candidate is a supported same-container HTTP router and isolated administrative handler alongside Streamlit, with the existing externally routed port preserved. This is **not yet selected or implemented**.
6. Earlier conditional approval for C1a–C1d, C2a–C2b and C3a–C3d on T1/T2 does **not** authorize any execution or REST collection under this prompt.

## Required repository baseline

- Report branch, HEAD, and `git status --short` before and after; preserve all existing modified and untracked WP07 work. No fetch required.
- Inspect, with exact file/line citations, `Dockerfile`, `container/entrypoint.sh`, public Streamlit launch, bridge/gate, Azure deployment scripts, any settings/secret-handling helpers, the historical ASP.NET SQLite qualification evidence endpoint, and tests governing presentation isolation.
- Verify rather than assume whether Twelve Data/Vike credentials use App Service settings, another source, or merely documented configuration. **Never print actual setting values, environment dumps, secrets, tokens, or connection strings.**

## Design investigation questions

### A. HTTP composition

- Identify feasible **supported** routing/hosting patterns inside the current custom Linux image: single front listener, loopback Streamlit and admin handler, or a simpler supported alternative. Compare operational and security tradeoffs and dependencies. Do not assume Streamlit supports arbitrary REST routes.
- Determine how to preserve Streamlit WebSocket upgrades, session behavior, health checks, port 8501 exposure, startup/restart semantics, readiness and graceful shutdown, and current public UI behavior.
- Specify listener bindings and direct-backend bypass prevention. Distinguish source-proven facts from unverified Azure behavior. Do not select or add packages yet.
- Explain how the disabled state can preserve existing UI without opening an administrative route or introducing unnecessary resource overhead on F1.

### B. App Service setting delivery and confinement

- Trace Azure App Service application-setting delivery to container startup, including inherited environment, `gosu`, `env -i` or equivalent sanitization, and process ancestry. Do not read live settings.
- Design a **concrete least-privilege startup handoff** so only the admin handler can read the API key, while Streamlit, public Worker descendants and other untrusted roles cannot. Compare options (e.g., process-specific environment, inherited file descriptors, protected ephemeral secret file) as **candidates only**; address Linux same-UID access, `/proc`, inherited descriptors, dumpability, code ownership, and accidental logging. Do not claim same-UID isolation is secure without evidence.
- Examine whether the current `/app` ownership/writability prevents trusting the handler or secret bootstrap. Identify the minimum required immutable-code and UID/GID boundaries without authorizing permission changes.
- State explicitly if strong confinement cannot be achieved with the current startup and UID arrangement; provide fail-closed alternatives and tradeoffs rather than silently weakening requirements.
- Design key generation (outside app), initial provisioning, rotation, revocation, startup validation, missing/malformed-key behavior and separate enablement flag. Note that changing App Service settings may restart the application; do not assume zero disruption.
- Explain which secret metadata can be audited without exposing secret values. Do not request publishing profiles or full settings listings.

### C. Authentication, authorization and disclosure

- Specify exact HTTP request validation order: disabled check, route/method/body/query/header bounds, authentication before collection, rate/concurrency control and fixed JSON response. Discuss whether a uniform 404 disabled behavior leaks less than distinct unauthorized responses.
- Assess dedicated API-key header versus `Authorization: Bearer` for compatibility and logging exposure; recommend one with justification. Require HTTPS, no key in URLs, constant-time verification of fixed-size digests, no CORS access, no sensitive logs, no redirects carrying credentials.
- Explain trust assumptions for Azure TLS termination, reverse-proxy headers and backend loopback. Never treat an untrusted `X-Forwarded-*` header as authentication.
- Provide a minimal, versioned endpoint/response contract, error codes, `Cache-Control: no-store`, `SELF_REPORTED` context labeling, and explicit `OBSERVED`/`NOT_RUN`/`UNAVAILABLE`/`UNKNOWN` semantics. Do not claim independent attestation.
- Preserve passive C1–C3 evidence mapping while distinguishing collector identity from Streamlit/Worker identity. Exclude raw private database details, arbitrary PID/path inspection and secrets.

### D. F1 boundedness and operational lifecycle

- Evaluate provisional limits (single in-flight collection, no queue, burst one / six per minute, <=8 KiB request headers, <=16 KiB response, <=1 second collection deadline) as **candidates**, not qualified limits. Describe bounded input parsing, finite metadata reads and timeouts that do not leak tasks.
- Identify expected processes, memory/CPU overhead, logging footprint and failure modes on F1; do not invent quota headroom or cost guarantees.
- Define default-off behavior, authorized temporary enablement, independent key revocation, disablement and rollback to the approved image/configuration, with no new persistent store.
- Preserve the public Worker gate and synthetic BTC-like twelve-selection identity. Do not describe simulated candles as observed BTC/USD or ETH/USD prices.

## Deliverable: chat-only design report

Organize as:

1. **Baseline and executed actions** (including explicit prohibited actions not performed).
2. **Source-indexed architecture and secret-flow inventory**, with file:line evidence and unresolved facts.
3. **HTTP routing alternatives and recommendation**, including WebSocket/health compatibility and deployment implications.
4. **Credential confinement threat model and candidate startup handoff**, including a process/UID/secret-access matrix and fail-closed gaps.
5. **Proposed administrative API security and JSON schema contract**, clearly labeled as design-only.
6. **Azure F1 cost/resource and rollback implications**, including what cannot be established offline.
7. **Smallest possible future implementation design authority**: exact source/test allowlist proposed, staged offline/local/Azure qualifications, pass/fail criteria and STOP gates; do not execute any stage.
8. **Decision matrix**: REPOSITORY-PROVEN / CONDITIONALLY FEASIBLE / UNKNOWN / BLOCKED, with remaining decisive evidence.
9. **Owner decisions needed**, followed by **STOP — owner approval required**.

Do not present this investigation as authorization to implement or deploy. The target state remains **NO-GO for public Worker activation** and `PUBLIC_RUNTIME_VERIFIED=False`.

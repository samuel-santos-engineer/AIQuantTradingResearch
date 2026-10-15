# Release 1.14 WP07 — Administrative REST Architecture Design-Closure Approval

## Authority and purpose

The owner approves **planning selection only** of the following **candidate architecture** for detailed design closure:

1. A supported ASP.NET Core router with YARP as a **candidate dependency**, combined with a fixed administrative diagnostics handler in the same trusted process.
2. Azure App Service application settings as the preferred store for a dedicated diagnostic API key.
3. A finite trusted bootstrap that transfers the key to the administrative process using a bounded anonymous pipe, without command-line or public-child environment disclosure.
4. A dedicated unprivileged administrative UID/GID distinct from the Streamlit/presentation identity; no supplementary groups unless explicitly justified.
5. Trusted bootstrap, router and diagnostic code non-writable by presentation/public Worker identities.
6. A permanent but **disabled-by-default**, authenticated, passive, read-only diagnostic capability. Disabled mode preserves direct Streamlit startup on port 8501; proposed enabled mode places the trusted router on port 8501 and Streamlit on loopback port 8502.

**This is NOT implementation, package, schema, deployment, Azure-setting, or activation authority.** These are design candidates subject to closure and owner approval. Do not interpret this document as approval of YARP version, process credentials, new permissions, runtime diagnostics, or any source edits.

## Non-negotiable project boundaries

- Keep `PUBLIC_RUNTIME_VERIFIED=False`; do not enable public Worker execution or change its gate.
- The existing approved `/home` SQLite database remains the sole storage candidate. No additional persistent store, diagnostic journal, secret file, DB opening, or SQLite factory invocation.
- No shell/command execution, arbitrary path/PID input, active capability experiments, Worker launches, or private acquisition from the diagnostic API.
- Do not reuse Twelve Data, Vike, or historical qualification credentials.
- Do not adopt the existing SQLite qualification endpoint as the diagnostic runtime; it executes qualification and conflicts with the public listener.
- Multi-instance and overlapping-revision analysis is **deferred for this diagnostic-API design phase only**; do not claim deployment-wide admission safety or relax the separate pre-activation obligations.
- No additional recurring Azure costs or new Azure resources are authorized.

## Authorized actions — repository-only design closure

Read relevant source, tests, build/package manifests, Dockerfile, entrypoint, release governance and locally available dependency information. Read Git branch, HEAD and before/after status. Do not change files or Git state. No builds, tests, container/app execution, Azure/GitHub operations, network/authentication, package installation, secret retrieval or external endpoint probing. If documentation research requires network access, stop and ask for separate approval rather than silently expanding authority.

Treat the prior Codex assessment as planning evidence, not as proof of deployed behavior. Verify repository claims with exact paths and line references. Preserve all existing tracked and untracked work.

## Required design-closure deliverables

### A. Exact HTTP composition contract

- Establish the feasible ASP.NET hosting and YARP package/version choice from existing repository framework and package conventions; mark any version not verifiable locally as unresolved.
- Specify exact listener/backend bindings, reserved admin path handling, request-method restrictions, WebSocket upgrades, Streamlit session and Origin behavior, forwarding-header trust, backend readiness, liveness and shutdown ordering.
- Define disabled-mode equivalence: no router/admin process, direct Streamlit on port 8501, no admin route collection.
- Identify bypass routes and how admin credentials are stripped before any UI forwarding. Explicitly address loopback's limits against same-container processes.

### B. Bootstrap and credential confinement protocol

- Define exact proposed App Setting names and parsing semantics (e.g., `AIQ_ADMIN_DIAGNOSTICS_ENABLED` exact `true`; dedicated 32-random-byte base64url key), without reading any live values.
- Specify process creation order, UID/GID transitions, anonymous-pipe ownership and descriptor lifecycle, environment allowlists, failure paths, bootstrap exit and no resident privileged helper.
- State which trusted processes can see plaintext key material; distinguish digest verification from secure erasure claims.
- Define handling for missing/malformed settings, broken pipe, child startup failure, key rotation/revocation and restart impact. No secret-bearing files, args, logs or inherited descriptors.
- Detail `/proc`, ptrace, dumpability, capabilities, supplementary-group and writable-code risks; specify how offline/local tests can substantiate isolation without asserting it prematurely.

### C. Passive collector and API contract

- Fixed `GET /admin/diagnostics/v1/environment` candidate; no request body, query, arbitrary path/PID or generic operations.
- Dedicated `X-AIQ-Admin-Key` candidate with bounded decoding, constant-time fixed-size digest comparison, duplicate-header rejection, HTTPS requirement, no CORS and sanitized errors.
- Authentication **before** collection, strict no-store responses, versioned JSON statuses `OBSERVED`, `UNAVAILABLE`, `UNKNOWN`, `NOT_RUN` and self-reported context limitations.
- Map C1a–C1d, C2a–C2b, C3a–C3d to permitted passive metadata only; avoid private DB, sidecars, environment dumps, command lines and raw mount sources/options.
- Review proposed limits (single collector/no queue, burst one, six/minute, 8 KiB headers, 16 KiB response, 1-second deadline) as **unqualified candidates**. Address unbounded/blocking reads and timeout-task accumulation.

### D. Exact implementation proposal (not permission to implement)

- Produce a literal-path source/test allowlist proposal, starting with the prior assessment's Dockerfile, `container/entrypoint.sh`, proposed bootstrap, `Directory.Packages.props`, isolated AdminDiagnostics project and tests.
- Check actual package conventions, build/test integration, supported runtimes, container copying/publishing and required new files. Identify every dependency and transitive risk; no package installation.
- Define offline security tests, local container qualification, Azure passive qualification, rollback and finite request/resource budgets. Separate future approvals for each stage.
- Define STOP conditions for WebSocket/UI regression, credential leakage, privilege widening, inability to protect trusted code, private SQLite access, unbounded F1 resource use, or any need to change the public Worker gate.

### E. Decision register

For each candidate mechanism, classify `REPOSITORY-PROVEN`, `CONDITIONALLY FEASIBLE`, `UNKNOWN`, or `BLOCKED`; identify precise missing evidence, recommended decision and whether further owner approval is required. Do not conflate a planning choice with a qualified security boundary.

## Output and stop

Return a **chat-only**, source-indexed design-closure report containing:

1. Branch, HEAD, and before/after working-tree status.
2. Architecture, secret-flow and lifecycle diagrams in text if helpful.
3. Exact candidate dependency/version and proposed source/test paths, with unresolved items clearly marked.
4. Threat and failure-mode matrix.
5. Bounded staged qualification and rollback plan.
6. Specific owner decisions required before implementation authorization.
7. Explicit **GO/NO-GO for requesting a separate offline implementation authority** (not public activation).

Do not write a report file, commit, create PR/issues, or execute anything beyond authorized repository reads.

Finish with: **STOP — owner review required. `PUBLIC_RUNTIME_VERIFIED=False`; public Worker activation remains NO-GO.**

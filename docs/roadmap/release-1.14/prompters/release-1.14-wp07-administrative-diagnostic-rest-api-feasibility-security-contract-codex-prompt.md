# Release 1.14 WP07 — Administrative Diagnostic REST API Feasibility and Security Contract

## Role and authority

Act as a repository-evidence investigator and security-contract author for `samuel-santos-engineer/AIQuantTradingResearch`. **This is planning-only, read-only authority.** Produce a detailed feasibility assessment and proposed security/API contract **in your final chat response only**. Do not edit, create, stage, commit, or delete repository files; do not open pull requests or alter GitHub Projects/issues. Do not implement or deploy an endpoint. Do not run application code, tests, builds, containers, databases, Workers, probes, Azure commands, or network requests. Public documentation research is optional only if separately permitted by the execution environment; label any external findings distinctly from repository evidence. If any step requires expanded authority, stop and ask.

Owner decisions already established:

1. Prefer an **administrative REST diagnostic API** over SSH as the evidence-reporting transport.
2. Make it a **permanent, reusable administrative capability**, **disabled by default**, suitable for future release validation.
3. For this phase, **defer multiple runtime instances and overlapping deployment revisions** from the diagnostic API's scope. Do not analyze or solve them in this work package. This is a scope deferral, **not** a waiver of global no-overlap safety requirements before any public Worker activation.
4. Initial capability is **passive, read-only reporting** of the serving application container and selected application validation facts. No active safety experiments.
5. Existing approved `/home` SQLite database is the **only permitted storage candidate**; the API must neither introduce storage nor become an admission authority.
6. `PUBLIC_RUNTIME_VERIFIED=False` remains mandatory. **NO-GO for implementation selection and public Worker activation** unless separately authorized after qualification.
7. Synthetic preview identity is a **BTC-like simulation** with a frozen **twelve-selection contract**; do not describe it as observed BTC/USD or ETH/USD market data.
8. Previously conditionally approved C1a–C1d, C2a–C2b, C3a–C3d shell diagnostics were **never executed**. That approval does **not** authorize implementing or calling a REST equivalent.

## Baseline and evidence discipline

- Inspect branch, HEAD, and `git status --short` before and after; do not clean or modify the working tree. Expected historical baseline: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`; report actual differences without resetting.
- Read governing Release 1.14 WP07 documents and relevant code, including the container entrypoint/Dockerfile, Streamlit or other web routing, public runtime bridge and gate, authentication/authorization facilities, telemetry, configuration, existing HTTP interfaces, SQLite factory, and test conventions. Use exact file paths and line references.
- Treat existing uncommitted documents/scaffolding as evidence, **not** approved implementation. Distinguish source-proven facts, proposals, unverified Azure behavior, and security assumptions.
- Do not print secrets, environment-variable values, tokens, connection strings, database contents, private file paths beyond already-approved nonsecret literals, or private application data.

## Questions to resolve

### A. Architectural feasibility

1. Determine whether the current deployed-facing application is Streamlit-only, whether it exposes an existing server-side HTTP route mechanism, and whether a separate listener or reverse proxy would be needed. **Do not assume arbitrary REST routes can be added to Streamlit.**
2. Compare the smallest viable integration patterns: (i) existing supported server-side route if proven, (ii) same-container sidecar listener with secure routing if feasible, (iii) separately hosted administrative service only as an architectural comparison, **not** a proposed extra-cost dependency. Assess F1 compatibility, exposure, maintenance, failure isolation, deployment implications, and added attack surface.
3. Explicitly distinguish an HTTP response from **proof it ran in the intended application process/container**. Propose nonsecret context attestations and their limits without claiming cryptographic attestation where none exists.
4. Identify which checks can run within the application's current unprivileged identity and which would require privilege changes; prefer **no privilege elevation** and mark unsupported checks `UNAVAILABLE`, not silently substitute root observations.

### B. Security contract (proposed, not implemented)

Specify:

- **Default-deny enablement:** a dedicated explicit opt-in, disabled for all current environments by default; safe startup behavior if missing/invalid; no enablement tied to `PUBLIC_RUNTIME_VERIFIED` and no possibility of toggling that Worker gate through diagnostics.
- **Authentication and authorization:** recommend a concrete feasible mechanism based on repository capabilities; explain where checks occur **before collection**, threat model for Internet exposure, TLS termination/trust boundary, replay/CSRF concerns as applicable, credential rotation, least privilege, and avoidance of shared secrets in URLs or responses. If no credible administrative identity mechanism exists, mark implementation **BLOCKED** pending separate design/authority rather than suggesting an unauthenticated endpoint.
- **Fixed operations and schema:** no generic shell, SQL, arbitrary paths, PID input, plugin execution, file browsing, environment dump, or user-supplied command parameters. Use allowlisted, nonsecret metadata only.
- **Resource and abuse limits:** single bounded request cost, finite work, response size cap, concurrency cap, conservative rate limiting, no periodic background polling, no unbounded filesystem traversal, and deterministic timeout/fail-closed behavior; assess feasibility on Azure F1 without claiming quota headroom.
- **Data minimization:** distinguish raw evidence collected locally from fields safe to return; avoid raw mount sources/options, process command lines, environment, descriptors, secret-bearing errors, hostnames, account/subscription IDs unless strictly necessary and authorized.
- **Logging:** no credentials or raw diagnostic payloads in logs; specify minimal audit events and retention dependencies without adding persistent stores.
- **Lifecycle:** permanently present code but disabled by default, separately authorized temporary enablement per environment, versioned schema, explicit disable/rollback, future release extensions subject to review, no silent privilege expansion.
- **Trust limits:** API cannot independently prove private SQLite isolation, complete Worker-tree death, crash durability, cross-UID cancellation, or global admission safety.

### C. Proposed versioned response schema

Draft a compact example for a possible `GET /admin/diagnostics/v1/environment` (route is **illustrative**, not approved). Include stable fields for schema version, observation timestamp, check status, effective diagnostic identity, selected capability/security metadata, `/home` mount type, allowlisted ownership/mode facts, runtime gate **without changing it**, and explicit `NOT_RUN`/`UNAVAILABLE`/`UNKNOWN` results. Avoid fabricated observed values: use placeholders or clearly synthetic examples. Do not include secrets, raw arbitrary paths, or actual private DB metadata. Define error codes, authorization failures, and cache behavior (`no-store`).

Map each **previously approved but unexecuted** C1a–C1d, C2a–C2b, C3a–C3d observation to: (1) safe REST field candidate, (2) required process context, (3) information-disclosure risk, and (4) evidence limitation. A REST design is **not** authorized to collect these fields yet.

### D. Validation and release governance

Propose a staged qualification plan, with **separate future authority for every active step**:

1. Repository-only contract approval.
2. Implementation design review and explicit source-change authorization.
3. Offline unit/security tests with synthetic fixtures, including disabled-by-default and unauthorized-access denial.
4. Local container qualification without private database access or Worker execution.
5. Separately authorized Azure deployment and authenticated passive checks, with exact rollback and zero-new-recurring-cost evaluation.
6. Separate later active safety qualification for A2/SQLite, process-tree proof, C2, and global admission; **not part of this diagnostic API contract**.

For each stage state prerequisites, allowed actions, forbidden actions, expected evidence, pass/fail and stop conditions. Note that the two existing Azure F1 apps are distinct targets; **do not investigate instance overlap or revision overlap in this phase**.

## Required chat-only deliverable

Use these sections:

1. **Baseline and actions** — branch/HEAD/status, exact categories of reads, confirmation of zero mutations/execution.
2. **Source-indexed architecture inventory** — cite precise files/lines; identify unknowns.
3. **Integration options and recommendation** — feasibility matrix, F1/cost and security tradeoffs, explicit blockers.
4. **Administrative security contract** — authentication, authorization, default-off, least privilege, minimization, boundedness, audit, disablement.
5. **Proposed API schema** — illustrative JSON and explicit status semantics.
6. **C1–C3 mapping** — all ten checks with caveats; no claim that they ran.
7. **Threat model and mitigations** — unauthorized discovery, bypass, spoofed context, replay, information leaks, denial of service, accidental Worker enablement.
8. **Phased qualification and rollback plan** — separate future approvals and gates.
9. **Open decisions for owner** — prioritize only decisions that truly block design; give a recommended default, not an implicit authorization.
10. **GO/NO-GO matrix and explicit STOP** — distinguish contract feasibility from implementation/deployment authorization.

**Do not create an ADR, markdown file, PR, issue, test, code, schema, endpoint, or Azure resource.** If the present architecture cannot safely host an authenticated REST endpoint, state that plainly and recommend the smallest further planning investigation. End with: **STOP — owner review and separate authority required. `PUBLIC_RUNTIME_VERIFIED=False`; public Worker activation remains NO-GO.**

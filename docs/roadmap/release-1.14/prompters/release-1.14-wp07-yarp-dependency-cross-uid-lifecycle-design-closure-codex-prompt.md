# Release 1.14 WP07 — YARP Dependency and Cross-UID Lifecycle Design Closure

## Authority and objective

**PLANNING ONLY / READ ONLY.** Investigate the two remaining design blockers for the permanent, disabled-by-default administrative diagnostic REST API: (1) exact ASP.NET/YARP dependency suitability and (2) a safe, bounded cross-UID process lifecycle. Deliver a source-indexed design-closure report and stop for owner approval. This prompt **does not** authorize implementation, dependency restore/installation, source edits, builds, tests, container execution, Azure operations, GitHub mutations, database access, or Worker execution.

The existing branch and HEAD previously reported were `draft/release-1.14-wp07-partial-public-disabled` and `67893f51931ee183f71050a83b81127e6e648b87`; verify read-only and report discrepancies. Preserve all tracked and untracked work. Record `git status --short` before and after; do not clean, reset, checkout, or stash.

## Fixed decisions and non-goals

- The API is a **permanent, passive, administrative observation mechanism**, **disabled by default**. It is not SQLite admission, an A2 isolation proof, C2 enforcement, or permission to activate a public Worker.
- Preferred secret store: **Azure App Service application settings**, with proposed `AIQ_ADMIN_DIAGNOSTICS_ENABLED` and `AIQ_ADMIN_DIAGNOSTICS_KEY`. These names remain proposals until implementation authority.
- Preferred enabled topology: one ASP.NET router/admin process listening on `0.0.0.0:8501`, fixed forwarding to Streamlit on `127.0.0.1:8502`; reserved `/admin` paths never fall through. Disabled mode retains direct Streamlit on port 8501 with no router process.
- Preferred auth: one independently generated 256-bit key in canonical unpadded base64url, supplied as `X-AIQ-Admin-Key`; authenticate before passive collection; no credential forwarding or logging.
- Candidate confinement: finite trusted bootstrap, dedicated unprivileged router/admin UID/GID, separate presentation UID/GID, immutable trusted code, bounded anonymous-pipe key handoff, sanitized child environments and descriptors. The bootstrap and handler are trusted to see the credential; the front router sees presented credentials.
- No resident root helper, generic process-control endpoint, shell, arbitrary command/path/PID, SQL/database reads, private Worker access, new persistent storage or recurring paid service.
- Multiple-instance/revision-overlap analysis is explicitly deferred for **this diagnostic API phase**; do not assert it is resolved for public activation.
- `PUBLIC_RUNTIME_VERIFIED=False`; public Worker activation remains **NO-GO**.

## Workstream A — Exact YARP dependency verification

1. Inspect `global.json`, `Directory.Build.props`, `Directory.Packages.props`, `AIQuantTradingResearch.slnx`, existing ASP.NET framework references, package policies, lockfiles (if any), Docker publish conventions and existing license/security review practices.
2. Identify the **exact** `Yarp.ReverseProxy` version proposed for .NET 10, distinguishing prior unverified candidate `2.3.0` from evidence. Use only allowed read-only local metadata and, if permitted by the environment, public package/vendor documentation; do **not** install, restore or fetch executable packages. Clearly label any item that cannot be verified without restore or package inspection as **UNKNOWN**.
3. Report direct and transitive dependency graph evidence, license and notices, published support/compatibility, known advisories with source/date, and central-package/transitive-pinning implications. Do not claim a clean graph or security clearance from documentation alone.
4. Compare a combined ASP.NET/YARP router/admin against a minimal built-in ASP.NET reverse-proxy alternative **only to establish whether YARP is justified**. Assess WebSocket upgrades, Streamlit session/cookie/Origin behavior, health/readiness, header filtering, request-size and connection bounds, F1 memory/startup overhead and rollback. Do not change the selected topology without an explicit recommendation and owner decision.
5. Produce a dependency disposition: **GO / CONDITIONAL / NO-GO** for a later *separately authorized* offline dependency-restore and implementation stage, with exact remaining proof obligations.

## Workstream B — Cross-UID lifecycle and PID 1 design

1. Read `Dockerfile`, `container/entrypoint.sh`, existing launch/subprocess paths and relevant offline test scaffolding. Trace **actual** current UID, ownership, environment sanitation, PID 1, signal propagation and child-reaping behavior; distinguish repository facts from assumptions about live Azure.
2. Design and compare at least two explicit lifecycle models:
   - A **fixed unprivileged presentation lifecycle agent** owning Streamlit, accepting only narrowly typed readiness/stop messages on inherited private descriptors.
   - An alternative concrete PID 1 supervision/handoff design that preserves cross-UID separation without a resident root helper or general process-control service.
3. For each model specify: which process is PID 1; who starts each child; UID/GID and supplementary groups; exact environment/FD allowlists; key-pipe creation/transfer/EOF and startup deadlines; child readiness; router vs Streamlit failure policy; signal ownership; termination escalation; zombie reaping; restart behavior; and how container shutdown remains bounded. **Do not assume one UID can signal another.**
4. Define the minimal trust boundary: root-owned non-writable bootstrap/router binaries, libraries and parent paths; writable cache separation; `/proc`/ptrace/dumpability/capability constraints; no key in public environments, command lines, files, logs, inherited descriptors or dumps. Identify which claims need local container proof rather than code inspection.
5. Specify deterministic failure cases: malformed/missing setting; short/extra pipe bytes; FD leak; UID mismatch; router crash; Streamlit crash; hung readiness; failed cross-UID shutdown; PID 1 exit; collector timeout; UI WebSocket disconnect; disabled-mode startup. Distinguish **prelaunch fail-closed disabled mode** from **enabled-startup controlled container failure**.
6. Recommend **one** lifecycle candidate if evidence permits. Otherwise state the blocking design choice and a bounded investigation needed to resolve it. Do not silently select numeric UIDs or add a helper with broader authority.

## Workstream C — Final contract and proposed implementation envelope

- Preserve `GET /admin/diagnostics/v1/environment`, passive fixed versioned JSON, `SELF_REPORTED` qualification, no private DB/sidecar reads, no arbitrary inputs, auth before reads, sanitized fixed errors, `Cache-Control: no-store` and resource limits subject to qualification.
- Keep `/admin` reserved, exact route/method validation, no CORS, no credential-bearing redirects, no untrusted forwarded-header authentication, no credential forwarding, no accidental WebSocket timeout inherited from the diagnostic collector.
- Produce a **literal proposed source/test path allowlist**, including any new lifecycle agent path(s), solution/package manifests, Dockerfile/entrypoint, administrative project, security/routing/bootstrap tests. Explicitly mark it **PROPOSAL ONLY**; identify excluded Worker, SQLite, provider and public-gate paths.
- Draft a finite staged qualification plan: (1) separately approved dependency verification/restore if needed, (2) offline synthetic tests, (3) local container UID/FD/ownership/HTTP/WebSocket/shutdown checks, (4) later Azure T1/T2 passive qualification. Each stage requires separate owner authority. Include bounded execution/resource budgets as proposals, measurable pass/fail gates, stop conditions and rollback.
- Do not access or modify Azure settings, including existing Twelve Data/Vike values; current live secret provenance remains unverified.

## Required output

Return a concise but technically complete **source-indexed report** with:

1. Baseline and exact actions taken; before/after repository status.
2. Fact/evidence table with literal repository paths and line references; links/dates for any public documentation used.
3. YARP version/dependency/license/advisory assessment, explicitly separating verified and unknown facts.
4. Cross-UID lifecycle comparison and one recommended PID 1/control protocol, or a justified unresolved decision.
5. Credential/descriptor and immutable-code threat analysis.
6. HTTP/Streamlit compatibility and F1 resource unknowns.
7. Proposed final literal-path implementation envelope and offline qualification matrix.
8. Decision register: **REPOSITORY-PROVEN / DOCUMENTED / CONDITIONALLY FEASIBLE / UNKNOWN / BLOCKED**.
9. Explicit **GO/NO-GO for requesting narrow offline implementation authority**, listing any prerequisites. Do not interpret GO as implementation authorization.
10. Explicit STOP and owner decisions required.

If read-only inspection cannot support a claim, label it **UNKNOWN**; do not manufacture certainty. No mutation, installation, runtime execution or deployment is authorized. End with `PUBLIC_RUNTIME_VERIFIED=False` and **public Worker activation NO-GO**.

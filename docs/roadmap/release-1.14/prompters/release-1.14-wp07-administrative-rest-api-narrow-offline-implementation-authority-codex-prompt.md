# Release 1.14 WP07 — Administrative REST API Narrow Offline Implementation Authority

## Authority and objective

This prompt grants **narrow, conditional, repository-local implementation authority** for the permanent, disabled-by-default administrative diagnostic REST API. Implement the selected **Model A** candidate and execute only bounded dependency verification and synthetic offline tests. This is **not** authority for local-container qualification, Azure changes, deployment, enabling diagnostics, private SQLite inspection, or public Worker activation.

Treat the latest planning report, **“YARP Dependency and Cross-UID Lifecycle Design Closure”**, as the design baseline. Before edits, confirm branch `draft/release-1.14-wp07-partial-public-disabled`, expected baseline HEAD `67893f51931ee183f71050a83b81127e6e648b87`, and record `git status --short`. The branch may have pre-existing tracked modifications and untracked files. **Preserve them without overwriting, cleaning, stashing, resetting, or committing.** If baseline differs materially, STOP and report. No push, PR, merge, release, or GitHub mutation.

## Approved candidate decisions (implementation targets, not qualified facts)

- **Model A:** finite trusted bootstrap initially PID 1, fork a fixed unprivileged presentation lifecycle agent, then drop to dedicated administrative identity and `exec` the ASP.NET Core router/admin as PID 1. No resident root helper. The agent owns and reaps Streamlit under the presentation identity; a fixed private channel mediates bounded readiness and stop messages.
- **Routing:** YARP direct forwarding to exactly `127.0.0.1:8502` for non-admin UI traffic; router/admin binds `0.0.0.0:8501` only when explicitly enabled. Preserve Streamlit HTTP, WebSocket, Origin, cookies and session behavior. Reserve `/admin` entirely; never forward any `/admin` path. Disabled mode retains the current direct Streamlit launch on port 8501 with no router/agent.
- **Secret storage:** Azure App Service application settings are the preferred deployment mechanism, but **do not read or change live settings**. `AIQ_ADMIN_DIAGNOSTICS_ENABLED` enables only for exact lowercase `true`; `AIQ_ADMIN_DIAGNOSTICS_KEY` must be canonical, unpadded base64url of exactly 32 random bytes (43 characters). Do not generate or persist a production key. Use synthetic credentials only in tests.
- **Credential handoff:** trusted bootstrap receives configuration, creates the presentation branch **before** creating the key pipe, then transfers exactly 32 key bytes and EOF via an anonymous pipe to the dedicated administrative process. No secret in argv, child environment, file, logs, or presentation descriptors. Administrative handler retains a fixed-size verification digest, closes the key FD, and checks one `X-AIQ-Admin-Key` header using bounded canonical decoding and constant-time digest comparison.
- **Confinement:** distinct administrative/presentation identities, no supplementary groups or retained capabilities, and root-owned trusted bootstrap/router/agent/dependencies and parent directories non-writable by public identities. Do not treat same-UID environment sanitization as isolation. Exact numeric UID/GID selection must be collision-checked against existing image identities before use; if no defensible non-conflicting values can be established from repository evidence, STOP and request owner approval rather than guessing.
- **Lifecycle:** fixed messages `START_UI`, `READY`, `FAILED`, `STOP_UI`, `STOPPED`, max 32-byte frames, finite transitions, one UI launch, no restart loop, no caller-selected PID/path/signal. Agent closes its lifecycle FD before Streamlit `exec`; router never assumes cross-UID signal rights. Ensure PID 1 reaping, bounded startup/readiness/shutdown, and fail-closed behavior on malformed channels, unexpected exits and descriptor leaks. Proposed budgets: 5s bootstrap/key handoff, 30s UI readiness, 5s enabled shutdown (2s TERM, 1s after KILL, remainder for reaping). These are **test targets**, not proven guarantees.

## Mandatory dependency gate — before feature implementation

1. Evaluate exact `Yarp.ReverseProxy` **2.3.0** via a single bounded restore/artifact review under existing .NET 10 SDK and central package-management conventions. Record actual resolved graph, including `System.IO.Hashing`, target assets, transitive pin effects, package/license notices, advisories and vulnerability audit results. Do not claim a clean audit from metadata alone.
2. Preserve existing signing conventions; do not disable repository security controls or modify global tooling. Prefer reproducible lock/artifact disposition, but **do not create an unapproved lockfile or notice path**.
3. If dependency resolution fails, introduces unexpected packages, unresolved critical/high advisories, licensing uncertainty, or requires paths beyond the allowlist, **STOP before feature edits** and report the evidence. Do not silently substitute another YARP version or implement a custom proxy.

## Literal mutation allowlist

Only the following existing or new paths may be modified/created, after baseline and dependency gates:

```text
Dockerfile
container/entrypoint.sh
container/admin_diagnostics_bootstrap.py
container/presentation_lifecycle_agent.py
Directory.Packages.props
AIQuantTradingResearch.slnx
src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj
src/AIQuantTradingResearch.AdminDiagnostics/Program.cs
src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsConfiguration.cs
src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsAuthentication.cs
src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsContracts.cs
src/AIQuantTradingResearch.AdminDiagnostics/EnvironmentMetadataCollector.cs
src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsRouting.cs
src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs
tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj
tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsSecurityTests.cs
tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsRoutingTests.cs
tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs
python/presentation/test_admin_diagnostics_bootstrap.py
python/presentation/test_presentation_lifecycle_agent.py
```

No Worker `Program.cs`, SQLite factory/schema/database/sidecar, market-data provider, public runtime gate, private research, deployment scripts, or other path may change. If the package audit requires a new license notice/lock artifact, or the lifecycle design needs another source/test path, **STOP and request literal allowlist expansion** before creating it. Do not modify pre-existing user changes without first comparing and preserving their intent; if edits would conflict, STOP.

## API and passive evidence contract

- Only `GET /admin/diagnostics/v1/environment` collects evidence. No arbitrary shell/SQL/Worker execution, PID or path arguments, request body, query string, file browsing, or database/sidecar access.
- Authenticate **before** any evidence collection. Reject missing, duplicate, malformed or oversized credentials; strip `X-AIQ-Admin-Key` from all UI-forwarded traffic. Never log key values, digests, request credential headers or raw exception details. HTTPS is required at ingress; forwarded headers never authenticate users. No CORS, cookie-based admin auth or credential-bearing redirects.
- Disabled/unknown administrative routes: `404`; bad key `401`; malformed/method/size `400/405/413`; busy `429`; unavailable/deadline `503`. Use sanitized fixed error codes and `Cache-Control: no-store`.
- Versioned JSON schema `aiq-admin-environment-v1`; self-reported `collectorRole`, `buildIdentity`, `attestation: SELF_REPORTED`, and per-check statuses `OBSERVED`, `UNAVAILABLE`, `UNKNOWN`, `NOT_RUN`. Never claim passive reads prove Streamlit/Worker identity, independent attestation, persistent SQLite isolation, tree-death guarantees, or global admission.
- Fixed passive C1a–C3d candidates only: collector UID/GID/groups, selected security flags, fixed PID-1 metadata, trusted-code ownership/modes, `/home` filesystem type and fixed parent metadata, kernel release, bounded namespace relationships, cgroup/procfs types. Exclude environment values, descriptors, raw mount sources/options, private DB and sidecar metadata. Any check without a provably bounded read remains `UNAVAILABLE`.
- Candidate resource bounds: one collector, no queue, burst one and maximum six requests per rolling minute, 8 KiB request headers, 16 KiB response, one-second collection deadline, finite read sizes. A timed-out collection must not continue accumulating or release its slot while still running. Keep UI/WebSocket deadlines separate.

## Offline-only implementation and qualification

- Work in a new isolated `net10.0` administrative project, without references to private Infrastructure or SQLite. Use existing centrally managed test conventions. Publish/copy admin artifacts explicitly in Dockerfile; protect trusted code while keeping presentation writable caches separately scoped. Preserve default-off current behavior.
- Execute **only** synthetic repository-local build/unit/integration tests; no container build/run, live service startup, HTTP network probing, Azure, DB, Worker, or external application execution. A narrowly bounded package restore is the sole permitted dependency network activity. Proposed total offline test ceiling: **15 minutes**; do not create unbounded retry loops.
- Test exact enable parsing, malformed/duplicate credentials, constant-time verification behavior as reasonably testable, auth-before-read, disabled 404, `/admin` non-forwarding, credential stripping, bounded serialization/collection, no private references, lifecycle message transitions, descriptor policy, child-environment allowlisting, failure cases, no restart, and no change to `PUBLIC_RUNTIME_VERIFIED=False`.
- Where process-level claims (actual UID/FD/PID 1, `/proc`, signal/reaping, WebSockets, F1 overhead) cannot be proven offline without a container, mark **NOT QUALIFIED**; do not claim pass from mocks alone. Do not launch a real Worker or inspect existing SQLite files.

## Required stop conditions

STOP on credential exposure, pre-existing change conflict, missing UID/GID collision evidence, unresolved package security/licensing/graph issues, extra required mutation paths, inability to maintain disabled-mode behavior, privilege expansion, private persistence access, uncontrolled background work, Streamlit regression, or any need to alter public Worker gates. Report partial work and status rather than expanding authority.

## Final Codex report

Provide: (1) exact pre/post branch, HEAD and working-tree status; (2) changed-file inventory limited to the allowlist; (3) resolved dependency graph/license/security findings and any limitations; (4) implemented design and deviations; (5) test commands/results and elapsed bounds; (6) synthetic-only security evidence versus unqualified process/container claims; (7) explicit STOP items and rollback instructions for local changes; (8) **GO/NO-GO for requesting separately authorized local-container qualification**, not for Azure deployment or Worker activation.

No commit, push, PR, Azure operation or deployment. End with **STOP — owner review required**.

**Invariants:** `PUBLIC_RUNTIME_VERIFIED=False`; public Worker activation **NO-GO**; approved SQLite-only persistence remains untouched; multiple-instance/revision-overlap analysis remains deferred for this diagnostic API phase.

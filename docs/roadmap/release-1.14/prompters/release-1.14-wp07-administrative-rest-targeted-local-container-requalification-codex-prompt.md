# Release 1.14 WP07 — Administrative REST Targeted Local-Container Requalification

## Authority and decision

Owner authorizes **one bounded, local-only, read-only-to-source container requalification** of the existing uncommitted WP07 administrative diagnostics candidate following the targeted repairs. This is qualification authority, **not** implementation, deployment, public activation, or permission to repair failures.

**Baseline:** branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. The worktree is intentionally dirty. Preserve all preexisting changes, untracked files, and unrelated work. Confirm branch, HEAD, and `git status --short` before and after; do not reset, stash, clean, commit, checkout, or alter tracked/untracked source.

**Standing gates:** `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability **NO-GO**; Azure passive qualification **NO-GO**; public Worker activation **NO-GO**. Azure ingress TLS trust remains unresolved and out of scope.

## Evidence and objective

Previous local qualification: image build and identities passed (`aiq=1000:1000`, `aiq-admin=1100:1100`); disabled UI/health passed; enabled startup exited 64 after 4.410 seconds with generic unavailable outcomes. The previous disabled-route HTTP 200 was a contract conflict, not proof of collection. Owner approved **Option A**: disabled mode directly runs Streamlit on port 8501; no administrative router/collector; generic UI fallback including HTTP 200 is acceptable.

Subsequent authorized repairs touched exactly six files: `container/admin_diagnostics_bootstrap.py`, `container/presentation_lifecycle_agent.py`, `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`, `python/presentation/test_admin_diagnostics_bootstrap.py`, `python/presentation/test_presentation_lifecycle_agent.py`, and `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsRoutingTests.cs`. All 53 synthetic tests passed (12 bootstrap, 7 agent, 34 .NET). Bootstrap now validates FD types, explicitly sets/verifies inheritable final descriptors, corrects `PR_SET_KEEPCAPS=8`, checks post-drop restrictions; startup emits sanitized fixed stage codes. A separate Linux descriptor scenario failed its assumed FD 3/4 setup before `exec`; descriptor survival and actual enabled startup are **not yet qualified**.

**Goal:** determine whether the repaired candidate starts and functions under the intended local container model, capture safe discriminating evidence if it does not, and preserve the exact security and lifecycle boundaries. No claims about Azure TLS, F1 production capacity, or public Worker readiness.

## Hard execution envelope

- **One** disposable Docker image build, **at most two** disposable application-container starts (one enabled, one disabled), **at most one** application container concurrently. Do not retry a failed build/start, even with changed arguments; STOP and report.
- **Ten minutes maximum** wall-clock for the entire qualification, including build, probes, and cleanup. Use explicit finite command/probe timeouts. If budget cannot be honored, STOP safely.
- **Zero authenticated administrative diagnostic collections**. Do not send a valid key to any HTTP endpoint; no collection of diagnostic payloads. Synthetic key may be supplied only to the enabled startup through a secure ephemeral mechanism that does not place it in Docker command-line arguments, `docker inspect` environment/config, logs, host files, or repository files. If this cannot be accomplished with the existing implementation and permitted local facilities, mark enabled qualification **NOT RUN** and STOP; do not weaken the constraint.
- Bind any published host ports **only to `127.0.0.1`**; no public interfaces, remote ingress, Azure, cloud, external API calls, databases, private providers, Workers, GitHub mutations, or deployment.
- Use finite CPU/memory/PID constraints: **0.5 CPU, 512 MiB RAM, 64 PIDs** for application containers. No privileged containers, `--cap-add`, host networking, Docker socket mounts, host PID namespace, broad bind mounts, host secrets, or extra Linux capabilities. No security-policy relaxations to make tests pass.
- Do not alter source, tests, documentation, Dockerfile, entrypoint, configuration, package locks, repository artifacts, or Git state. Do not invoke repair tools, formatters, generators, or commands that write to the worktree. Do not prune shared Docker caches or unrelated images/containers. Remove only resources created by this qualification.
- Preserve the production ingress TLS denial policy: do not trust spoofable forwarded headers, forge a TLS signal, add HTTP bypasses, or attempt to prove authenticated API access over local plaintext HTTP. No authenticated HTTP requests are permitted.
- Do not print, log, inspect, hash, or expose synthetic credential bytes; do not print full environment, `/proc/*/environ`, sensitive FDs, pipe contents, or raw potentially sensitive logs. Fixed stage codes, numeric FD flags/types and sanitized status observations are allowed.

## Mandatory ordered procedure and STOP gates

### 0. Preflight (no mutation)

Confirm branch/HEAD, record exact preexisting status, Docker Engine availability and architecture, image/container naming isolation, and a bounded cleanup plan. Confirm ability to build without source writes and to inject a canonical 32-byte synthetic base64url key solely within disposable startup (not Docker configuration or host files). If not possible, STOP before building. Note any uncertainties; do not silently broaden authority.

### 1. Build and static image/account verification

Build exactly one uniquely named local qualification image from the existing candidate. Confirm success and inspect image/account metadata: `aiq=1000:1000`, `aiq-admin=1100:1100`, collision guard outcome, trusted-code ownership/permissions at relevant paths, executable presence, and disabled/enabled entrypoint configuration. Check no new unexpected writable trusted-code surface. Sampling does **not** qualify complete dependency immutability. On build, UID, permissions, or configuration failure, STOP and clean up.

### 2. Enabled-mode qualification FIRST

Run one disposable enabled application container under the resource/loopback/security restrictions. Generate and pass a synthetic canonical key only inside the disposable startup process, without exposing it in Docker arguments, image, host files, `docker inspect`, or logs. Do not use real credentials.

**Early mandatory evidence, before HTTP/UI tests:**

1. Capture sanitized fixed startup-stage outcome codes, exit code, elapsed startup time, OOM/restart indicators and bounded logs. If startup fails, record first distinguishable stage and STOP; do not retry or patch.
2. Verify expected root bootstrap is finite; administrative router is PID 1 after identity drop under `1100:1100`, presentation lifecycle agent/Streamlit under `1000:1000`; no resident privileged helper; no supplementary groups; zero effective/permitted/inheritable capabilities and `NoNewPrivs=1` where expected. Check that root-owned trusted code is not presentation-writable.
3. **Descriptor survival across `exec` is mandatory:** verify final intended descriptor numbers, types, inheritability/close-on-exec state, and successful post-`exec` consumption by the intended process **without accessing credential contents or dumping FDs**. Distinguish observation of a live channel from proof of exact FD flags; mark any unobservable detail **NOT QUALIFIED** rather than inferring it. Do not use a descriptor experiment that assumes the initial OS-assigned FD numbers are 3/4.
4. Confirm the fixed lifecycle protocol achieves bounded readiness and normal shutdown without cross-UID signaling, restart loops or secret disclosure. If process or FD inspection would require privilege escalation, STOP that check and report **NOT QUALIFIED** rather than adding privileges.

Only if enabled startup and core isolation succeed: probe health and root UI over loopback; verify Streamlit content and bounded WebSocket upgrade/connection compatibility via the intended router path, with no authentication bypass and no administrative collection. Probe reserved `/admin` routing only through **unauthenticated** requests; ensure no collection, no UI proxying into administrative handler, and no credential reflection. Do not infer Azure TLS trust from any local result.

### 3. Disabled-mode Option A qualification

After enabled container has stopped and been removed, start **at most one** disabled application container. Verify PID 1 is direct Streamlit under `1000:1000`, no administrative router/agent/collector, no supplemental groups/capabilities, `NoNewPrivs=1`; health/root UI return expected results and normal stop is bounded. Administrative-looking URL may return generic UI with HTTP 200; **do not fail on status 200 alone**. Verify it is only Streamlit fallback and no collector exists or executes. Never require disabled-mode HTTP 404.

### 4. Resource observation and cleanup

Record bounded CPU/memory/PID snapshots with clear local-only qualification caveats. Do not extrapolate Azure F1 capacity. Stop/remove only created containers, remove only created image and temporary build context, leave shared cache untouched. Verify branch/HEAD and before/after worktree status match exactly. Report resource IDs/digests only when non-sensitive and relevant.

## Immediate STOP conditions

STOP on any source change, credential exposure or uncertain injection secrecy, enabled startup failure, FD loss/type mismatch, UID/GID collision, retained unexpected capabilities, unauthorized privilege, insecure trusted-code permissions, unexpected administrative collection, TLS-policy bypass, public host-port binding, unbounded child/restart behavior, OOM, budget exhaustion, cleanup risk, or requirement for additional builds/starts/source edits. Preserve sanitized evidence, perform safe cleanup, and await owner review. Never auto-fix.

## Required final report

1. Baseline branch/HEAD and before/after status comparison; exact actions and budget accounting (builds, starts, concurrency, duration, authenticated collections).
2. A matrix with **PASS / FAIL / NOT RUN / NOT QUALIFIED** for build/accounts, trusted-code sample, enabled stage/exit, PID1 and UID/GID, capabilities/NoNewPrivs, FD survival across exec, key confinement, lifecycle/readiness/shutdown, HTTP/root UI, WebSocket, unauthenticated `/admin`, disabled Option A identity/route, resource limits and cleanup.
3. On failure, first identifiable sanitized stage code and precise evidence vs hypotheses, with no credential material or raw secret-bearing dumps.
4. Remaining uncertainties: full dependency immutability, Azure ingress TLS trust, Azure F1 overhead, and public Worker safety remain independent.
5. Clear **GO/NO-GO for requesting the next separate authority**, not for Azure deployment or API activation. State `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability **NO-GO**, Azure passive qualification **NO-GO**, public Worker activation **NO-GO**.

**STOP after reporting. No further action without explicit owner authority.**

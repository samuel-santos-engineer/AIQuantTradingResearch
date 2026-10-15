# Release 1.14 WP07 — Administrative REST Narrow Local-Container Qualification Authority

## Authority and objective

Owner authorizes **only a bounded local-container qualification** of the existing, uncommitted administrative REST API offline candidate on branch `draft/release-1.14-wp07-partial-public-disabled`, baseline HEAD `67893f51931ee183f71050a83b81127e6e648b87`. The candidate previously passed 45 synthetic tests (33 .NET, six bootstrap Python, six lifecycle-agent Python) and Release publish. These are reported baselines, **not** proof of container behavior. Preserve all pre-existing tracked modifications and untracked files.

The purpose is to collect reproducible evidence about container image identity, trusted-code immutability, secret/descriptor confinement, Model A PID 1 lifecycle, disabled-mode compatibility, and enabled-mode Streamlit HTTP/WebSocket/health behavior using **synthetic data only**. This is not authority to deploy, enable the Azure API, qualify Azure ingress TLS, or activate any public Worker.

## Preflight: fail closed

1. Record branch, HEAD, `git status --short`, exact image/build context and available local container runtime. Do not clean/reset/stash or commit.
2. Read the existing Dockerfile, entrypoint, administrative bootstrap/router/agent, and relevant tests. Confirm that all actions remain inside this authority; report any discrepancy and STOP.
3. Confirm that no real Twelve Data, Vike, Azure, production, database, or diagnostic key is supplied. Use a newly generated **synthetic** 32-byte key only for the local run; never print or persist its value. No production settings or mounted host secrets.
4. If Docker/container runtime is unavailable, the required base image cannot be resolved without unauthorized changes, or safe isolation cannot be established, STOP with evidence. Do not install Docker, change system permissions or silently broaden scope.

## Authorized execution envelope

- **Local only:** One image build of the existing candidate and a finite set of disposable containers; no registry push or remote deployment. A failed build may be retried once **only** after an environmental/transient failure that requires no source edit. Otherwise STOP.
- **No source edits, package changes, license changes, lockfile creation, or dependency upgrades.** No repository mutations except incidental ignored build artifacts already produced by normal builds; enumerate any such artifacts. Do not modify Dockerfile, scripts or tests to make the qualification pass.
- **Time and resource budget:** maximum **10 minutes of container execution total**, maximum **six container starts**, maximum **two concurrent containers** (prefer one), maximum **three authenticated diagnostic collection requests**, and no polling loops. Set finite per-command timeouts and container CPU/memory/PID constraints where the local runtime supports them. If constraints cannot be applied, record and decide whether safe to continue; never claim boundedness without evidence.
- Bind published test ports to **127.0.0.1 only**. No externally reachable host interfaces. No privileged containers, host PID/network namespace, host `/proc`, host `/home`, Docker socket, cloud credentials, or host database mounts. Do not use `--privileged` or broad added capabilities.
- Do not launch a public Worker, private research workload, SQLite connection, data acquisition, or arbitrary shell through the API. Diagnostic operations remain fixed passive metadata only.
- Local process inspection is allowed **only** for the disposable qualification containers and their known processes; collect redacted identity, capability, descriptor *counts/types* and ownership metadata, not secret values or full environments. Never print keys, pipe bytes, Authorization headers or raw credentials.
- If the image cannot run under these restrictions, STOP rather than weakening the security boundary.

## Qualification matrix (finite, no unbounded probing)

### A. Build and identity

- Build once using the candidate Dockerfile; record image ID/digest and build result. Verify `1100:1100` account creation or explicit collision rejection, and preservation of presentation/public-runtime role assignments. Do not assume `aiq` is numerically 1000 without inspection.
- Inspect actual `/etc/passwd` and `/etc/group` identity mapping **inside the disposable image/container** without changing them. Confirm distinct administrative and presentation effective UID/GID, no supplementary-group overlap that defeats separation, and no unexpected retained capabilities.
- Inspect ownership/modes of trusted bootstrap, administrative binaries, dependencies, lifecycle agent and their parent directories. Verify presentation identity cannot modify trusted administrative code. If unable to prove, mark NOT QUALIFIED and STOP on a critical violation.

### B. Disabled-mode regression

- With diagnostics absent/disabled, confirm **direct Streamlit on port 8501**, no persistent administrative router or lifecycle agent, and no diagnostic collection. Verify a bounded local UI health/HTTP request. Do not assume any Azure-facing route behavior.

### C. Enabled-mode identity and lifecycle

- With an **isolated synthetic** key and exact enable flag, confirm router/admin on port 8501 and Streamlit on loopback 8502, with the presentation agent under its assigned identity. Confirm the router/admin's PID 1 role **in the actual local container PID namespace**, not by host PID assumptions.
- Verify credential handoff: presentation/Streamlit process environments, arguments and inherited descriptors must not contain the key or credential-pipe FD; do not dump secret-bearing admin/bootstrap memory or environment. Inspect FD targets/ownership metadata only where permitted. Distinguish verified evidence from inaccessible evidence.
- Exercise a bounded normal startup, one controlled UI shutdown, and at most one synthetic abnormal failure scenario **only if safely isolated**. Check fixed lifecycle messages, agent child ownership, reaping, exit status, absence of orphaned processes in the disposable container, and no restart loop. A container namespace teardown is not independent public Worker tree-death proof.
- Verify UI HTTP readiness and a **finite** Streamlit WebSocket handshake/session interaction through the router, including path/header handling. Avoid altering Streamlit Origin/CSRF settings to make the test pass. Record any test that cannot be run as NOT QUALIFIED, not PASS.

### D. Administrative route and TLS boundary

- Verify `/admin` never falls through to Streamlit, disabled route denial, key-header stripping from UI forwarding, and authentication-before-collection **only where existing local transport checks permit**.
- **Do not bypass, patch or weaken the existing TLS/forwarded-header policy.** If local HTTP requests are denied by that policy, record the denial as expected/observed and mark authenticated API availability **NOT QUALIFIED**. Do not invent a trusted ingress, fake `X-Forwarded-Proto`, install a reverse proxy, or assert Azure TLS behavior.
- Maximum three successful authenticated passive collections, if possible without changing the trust policy. Do not access SQLite, private files or arbitrary paths to obtain diagnostics.

### E. Resource and cleanup

- Collect bounded local observations of CPU, RSS/memory, process count, startup time and log volume in disabled and enabled modes where feasible. These are **local measurements**, not Azure F1 headroom or a zero-cost guarantee.
- Stop and remove **only** disposable containers/images created by this qualification, identifying them explicitly. Preserve pre-existing local resources and the repository worktree. If cleanup fails, report exact remaining artifacts; do not use broad prune commands.

## Mandatory STOP conditions

Stop immediately on credential exposure; same-UID administrative/presentation execution; writable trusted code by presentation; unexpected privileged capability; uncontrolled process spawning/restarts; leaked descriptors; unexpected dependency changes; private DB/Worker access; exposed non-loopback host port; API auth bypass; UI regression that threatens isolation; unbounded/hung execution; or any requirement to modify source or cloud configuration. Capture minimal sanitized evidence, terminate disposable local resources, and request owner review.

## Deliverable

Return a **source- and command-indexed qualification report** with:

1. Baseline branch/HEAD and before/after worktree status; exact local runtime and image identity.
2. Matrix for each A–E check: `PASS`, `FAIL`, `NOT_RUN`, or `NOT_QUALIFIED`, with observed evidence, command/exit status and explicit limitations.
3. UID/GID, groups, capabilities, trusted-code ownership, PID 1, FD confinement, startup/shutdown, HTTP/WebSocket/health, default-off and resource results (sanitized).
4. Clear separation of **local-container evidence** from unresolved Azure ingress TLS, Azure F1 capacity, SQLite/A2/C2/global admission and independent Worker termination.
5. Resource/time/container-start/request budget accounting and precise cleanup results.
6. **GO/NO-GO** recommendation for a *separate* Azure ingress TLS trust design and any later Azure passive qualification; list blockers and the smallest next requested authority.

**No commits, PRs, Azure calls, deployments, configuration changes, production secrets, or public Worker activation.** Do not infer Azure readiness from a local pass.

`PUBLIC_RUNTIME_VERIFIED=False`. Public Worker activation **NO-GO**. Administrative API availability **NO-GO** pending ingress TLS trust qualification.

**STOP after reporting. Owner approval required for any subsequent stage.**

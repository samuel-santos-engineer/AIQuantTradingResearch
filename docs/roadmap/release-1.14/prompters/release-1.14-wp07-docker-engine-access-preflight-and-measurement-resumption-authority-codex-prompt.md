# Release 1.14 WP07 — Docker Engine Access Preflight and Conditional Disabled-Mode Measurement Resumption

## Authority status
Owner-authorized, narrowly scoped Codex instruction. This is **not** authorization for general elevated access, Docker administration, repository repairs, Azure operations, or deployment. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification, and public Worker activation remain **NO-GO**.

## Baseline and preservation
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Preserve the existing dirty worktree exactly; record before/after Git status. Do not reset, stash, checkout, commit, clean, modify or create repository files.
- Previous measurement-only attempt stopped at preflight because Docker Desktop Linux Engine named-pipe access returned permission denied. Zero builds and starts occurred. Treat this as an **execution-context access blocker**, not an application failure.

## Stage 1 — Read-only access diagnosis (always permitted)
1. Confirm branch, HEAD, and unchanged worktree. STOP on mismatch.
2. Determine, using non-elevated, non-mutating checks, whether Docker Desktop's Linux Engine is available from the **current** execution context. Record the exact error class, without printing secrets, user tokens or private host configuration.
3. Distinguish, where evidence permits, between Docker Desktop not running, incorrect engine context, unavailable named pipe, OS access denial, and Codex sandbox denial. Do not infer the cause from a generic permission error.
4. Inspect only the minimal current-context configuration needed to identify the intended Docker Desktop Linux Engine endpoint. Do not enumerate unrelated named pipes, accounts, credentials, containers, images or host resources.
5. If access is denied, **STOP** and report the minimum specific access adjustment that the owner would need to approve. Do not change Docker Desktop settings, Windows ACLs, groups, sandbox settings, environment policies or host configuration. Do not self-elevate, request broad host access or bypass the sandbox.

## Stage 2 — Conditional measurement resumption
Proceed **only if Docker access already works within the permitted context, or the host explicitly grants a narrowly scoped, separately confirmed access request to the Docker Desktop Linux Engine named pipe**. An instruction in this document alone does not grant OS or sandbox privileges. If a tool requests unrestricted host access or blanket sandbox bypass, STOP rather than accept it.

If Stage 2 is reachable, execute the previously approved **disabled-mode measurement-only** protocol, without expanding it:
- Verify Docker is targeting the intended Linux engine. Use unchanged candidate source; at most **one** disposable local image build and **one** disposable disabled-mode container start, with **no retries**.
- No credentials, administrative API collections, authenticated calls, databases, Workers, Azure access, or production resources.
- Bind published ports to host loopback only. No host mounts, privileged mode, extra capabilities, Docker socket mount, or host networking. Retain bounded resources: **0.5 CPU, 512 MiB RAM, 64 PIDs**.
- Observe readiness for at most **30 seconds** from Docker-recorded start. Capture monotonic/start-relative timestamps, listener presence, fixed `/healthz` (or actual previously verified health path) and root HTTP results, and bounded sanitized error categories: connection refused, timeout, HTTP non-200, transport error, or other. Do not mislabel unclassified errors.
- Capture a small bounded set of sanitized startup/shutdown logs; no environment dumps, arbitrary file contents, key material, exception stack traces, or unrelated Docker metadata. If log capture fails, mark **NOT_OBSERVED** rather than silently asserting success.
- Confirm disabled Option A: direct Streamlit on 8501 as PID 1, UID/GID 1000:1000, no administrative router/agent/collector; generic UI fallback including HTTP 200 is permitted. Verify groups/capabilities/NoNewPrivs only through permitted, non-privileged observation.
- Issue one normal stop with **five-second grace**; capture exit status, stop timing, and whether force-kill was required. No abnormal-failure experiments.
- Remove only the container, image and temporary context created by this run. Do not prune shared caches or touch unrelated resources.
- Cap complete preflight-through-cleanup work at **ten minutes**; STOP and safely clean up on any boundary violation or unexpected access request.

## Hard STOP conditions
- Wrong branch/HEAD; unexpected worktree changes; need for source edits or implementation repair.
- Docker engine access requires elevation, host ACL changes, group changes, blanket sandbox disablement or access beyond the named Linux Engine endpoint without separate explicit approval.
- Permission denied persists; wrong engine endpoint; any privileged container requirement; access to unrelated containers/images.
- Any exposure risk to credentials, private metadata, or host resources; any attempt to weaken TLS or enable the administrative API.
- Budget exhaustion, unbounded wait, second build/start, or inability to clean up disposable resources.

## Deliverable and disposition
Provide a concise evidence matrix with `PASS / FAIL / NOT_RUN / NOT_QUALIFIED`, including:
- Access-check results and whether Stage 2 was permitted; precise blocker if not.
- Branch/HEAD, before/after status, build/start counts, runtime budgets and cleanup.
- If Stage 2 ran: readiness timeline, HTTP error categories, listener state, process identity, bounded logs, stop timing and exit code.
- Explicit distinction between observed facts, hypotheses and missing evidence; smallest justified next authority.

Do not treat one successful local run as Azure F1 capacity proof or complete credential/FD confinement qualification. Do not modify source or commit. **STOP for owner review**. Keep `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification and public Worker activation **NO-GO**.

# Release 1.14 WP07 — Administrative REST Disabled-Mode Bounded Runtime Observation

## Owner authorization and objective

This is a **narrow, observation-only Codex authority** to investigate the previously observed disabled-mode Streamlit readiness failure and forced shutdown (exit 137). Determine whether the failure reflects initialization timing, listener/routing/application failure, or signal/shutdown behavior. Do **not** diagnose by changing code or relaxing security.

Prior evidence: branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`; previous disabled container ran direct Streamlit as PID 1 under `aiq=1000:1000`, but finite health/root checks failed and Docker stop required SIGKILL after five seconds. Earlier disabled run had passed health/root and exited cleanly. Exact HTTP error categories and startup timestamps were not retained. The enabled router's exit 64 is separately explained by an unconditional return; **enabled mode is out of scope**.

## Mandatory preflight and STOP gates

1. Verify branch, HEAD, `git status --porcelain=v1`, and relevant source/image configuration without modifying files. Record existing dirty state. Do not reset, stash, clean, stage, commit, or touch unrelated work.
2. Confirm Docker is available and that the test can run entirely locally with a disposable container and **unchanged candidate image contents**. If the previous image is absent, at most **one** local image build from the unchanged worktree is permitted solely to reproduce the candidate; record image ID and build result. No Dockerfile or source edits. If a reproducible build would require code/dependency changes, STOP.
3. Confirm the run can enforce one disabled-mode container, loopback-only host binding, no extra capabilities, no privileged mode, no host mounts, no production secrets, no Azure/network service dependencies, and resource limits at most 0.5 CPU, 512 MiB memory, 64 PIDs. If not, STOP.
4. Use the exact disabled-by-default setting (do not set `AIQ_ADMIN_DIAGNOSTICS_ENABLED=true`); do not provide `AIQ_ADMIN_DIAGNOSTICS_KEY` or any other credential. Never start the administrative router, lifecycle agent, database, private provider or Worker.
5. Do not run arbitrary process commands inside the container or inspect private process memory, credential bytes, environment dumps, unrelated files or cross-UID `/proc` entries. Fixed read-only process/listener observations only, if feasible without privilege escalation.

## Authorized bounded experiment

- **One** disposable disabled-mode container start; **zero retries** or abnormal-failure experiments. Maximum **one** application container concurrently.
- From container start, observe for **at most 30 seconds** using a monotonic clock. Capture timestamped, bounded observations at a small fixed cadence (for example 0, 2, 5, 10, 15, 20, 30 seconds), stopping earlier if sufficient evidence is obtained. Do not extend the window to wait for readiness.
- At each sampled point, record only: elapsed seconds, whether container is running, Streamlit PID 1 identity/status (where safely observable), listener present/absent/unknown on expected port 8501, and fixed health/root probe outcomes. Classify each probe as HTTP status, connection refused, connect timeout, read timeout, reset, malformed response, or other sanitized error category. Retain bounded response timing. For root, only note whether expected Streamlit markup is present; do not dump HTML.
- Capture a **small, bounded, timestamped and redacted** subset of startup logs relevant to Streamlit startup, listener readiness and shutdown. No raw environment, keys, arbitrary stack traces, full process listings or file contents. If safe redaction cannot be guaranteed, report `NOT_OBSERVED` instead.
- Record the image/container stop-signal configuration if available without mutation. At the end of the observation window, send the normal Docker stop request with **five-second grace**, record stop-request timing, signal delivery evidence if safely available, graceful/forced outcome, final exit code, and any bounded shutdown logs. Do not change `STOPSIGNAL`, inject alternate signals or extend the grace period.
- Track resource snapshots if available without extra privilege; these are observations, not proof of Azure F1 capacity.
- Do not request or test `/admin` beyond what is needed for Option A (normally unnecessary). HTTP 200 generic UI fallback is **permitted**; absence of an administrative collector, not HTTP 404, is the disabled-mode security contract.

## Execution budgets and cleanup

- Maximum **one image build**, **one container start**, **one concurrent container**, **30 seconds of runtime observation**, **five seconds stop grace**, **zero authenticated collections**.
- The previous overall ten-minute execution envelope remains a hard cap from preflight through cleanup. Measure and report full elapsed time; if approaching the cap, stop safely and clean up.
- Remove only this authority's disposable container, image if created solely for this experiment, and temporary build context. Do not prune shared build cache, volumes, unrelated containers/images or repository artifacts.
- Compare before/after Git status and, where practicable, existing file hashes; any unapproved source mutation is an immediate STOP.

## Required report

Provide a concise evidence table with `PASS`, `FAIL`, `NOT_RUN`, `NOT_QUALIFIED` and precise observation timestamps:

1. Baseline identity and unchanged-source/image provenance.
2. Startup-relative readiness sequence, including exact sanitized HTTP error categories, response timing, and listener state.
3. Earliest demonstrated transition from not-ready to ready, or the bounded reason readiness remains unproven.
4. Effective stop configuration, observed shutdown timeline, final exit code, and whether graceful termination is **proven** or still **unknown**.
5. Resource/privilege boundary and cleanup results.
6. Distinguish **observed facts**, **source-proven behavior**, and **hypotheses**; recommend the smallest next authority (repair versus measurement adjustment) without implementing it.
7. Full budget accounting and explicit GO/NO-GO recommendation.

## Absolute exclusions and final gate

**No source/test/documentation edits, GitHub operations, deployment, Azure operations, public ingress, TLS-policy changes, privileged/cross-UID inspection, database/SQLite reads, Worker execution, authenticated API calls, or enabled-mode testing.** No new persistent infrastructure or paid resources. If a safety boundary or budget cannot be honored, STOP, clean up, and report what is known.

This authority **does not** approve repairs or another broad local-container requalification. Preserve Option A: disabled mode executes direct Streamlit on port 8501; generic UI fallback, including HTTP 200, is acceptable. Preserve `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**. End with **STOP — owner review required**.

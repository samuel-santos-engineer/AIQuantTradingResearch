# Release 1.14 WP07 — Disabled-Mode Measurement-Only Qualification Authority (Codex)

## Owner decision and scope

Authorize **one narrowly bounded, local-only, measurement-only qualification** of the existing disabled-mode public Streamlit path. The previous run demonstrated HTTP readiness at approximately 9.51 seconds and normal exit 0 after stop. The earlier failure's root cause remains unknown. **Do not treat a successful sample as a latency guarantee.**

This authority improves evidence quality, not implementation. It does not authorize repairs, source or documentation edits, commits, GitHub operations, Azure operations, deployment, database access, Workers, public administrative API activation, or TLS-policy changes.

## Baseline and preservation gates

- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`. Verify both; STOP if mismatched.
- Preserve the existing dirty worktree, including untracked files. Capture `git status --porcelain=v1` before/after and use a reliable, nontruncated hash manifest for relevant existing files. Never delete or overwrite user files.
- Do not reuse deleted qualification image identifiers. A single disposable image may be built from the **unchanged** candidate if needed; do not alter the build context's source contents.
- Confirm local Docker availability and the ability to perform sanitized observation without privileged access. STOP if elevated permissions, extra capabilities, host mounts, cross-UID `/proc` inspection, or secret access would be required.

## Permitted experiment and hard limits

- **One** image build maximum; **one** disabled-mode container start maximum; **one** concurrent application container; **zero retries**.
- Maximum **30 seconds** of startup/readiness observation measured from the container's recorded start; maximum **5 seconds** normal-stop grace. Entire preflight-to-cleanup activity must finish within **10 minutes**; record its timing separately from runtime observation.
- Use loopback-only published host port, no mounts, no `--privileged`, no `--cap-add`, no real credentials, no administrative enablement/key, and no external network probes. Use the prior bounded envelope: 0.5 CPU, 512 MiB memory, 64 PIDs.
- Zero authenticated administrative collections; no diagnostic API probes, private data providers, SQLite access, Worker operations, or production configuration.
- No source edits, scripts saved into the repository, or changes to the Dockerfile/entrypoint. Temporary host-side probe material, if essential, must be confined to an isolated disposable directory outside the repository and removed at completion.

## Required observations

1. **Clock and probe reliability:** Before starting the container, validate the host HTTP probe and its timeout/error classifier using harmless loopback behavior. A failed setup must STOP before container start; do not improvise after startup. Capture both UTC and monotonic relative timing; distinguish container start, probe attempts, listener observation, readiness, stop request, and container exit.
2. **Listener and HTTP:** At bounded intervals, record whether port 8501 is listening and classify health/root attempts as `CONNECTION_REFUSED`, `CONNECT_TIMEOUT`, `READ_TIMEOUT`, `HTTP_STATUS`, `PROBE_ERROR`, or `NOT_RUN`. Record HTTP status and bounded elapsed milliseconds only; for the root page, report presence/absence of Streamlit markup without dumping HTML. Do not collapse specific failures into `OTHER` if the underlying exception can be classified safely.
3. **Bounded sanitized logs:** Establish the collection mechanism **before** container start. Collect a fixed maximum number of startup/shutdown log lines and bytes, with timestamps and redaction/allowlist rules. Only report benign lifecycle/Streamlit readiness or signal information. Do not print environment variables, arbitrary exception traces, request headers, tokens, filesystem contents, or credential-like strings. If safe bounded capture is not possible, mark `NOT_OBSERVED` and explain; do not weaken controls.
4. **Disabled identity and Option A:** Verify direct Streamlit PID 1, UID/GID 1000:1000, no supplementary groups, no effective/permitted/inheritable capabilities, `NoNewPrivs=1`, and no administrative router/agent/collector. The disabled `/admin` URL **need not return 404**; generic UI fallback including 200 is acceptable. Avoid collecting administrative data.
5. **Stop:** After readiness or the 30-second deadline, issue a single normal container stop with 5-second grace. Capture requested stop signal where safely observable, stop/exit timestamps, exit code, OOM status and whether forced termination occurred. Do not infer signal delivery or handler internals from exit code alone.
6. **Resource snapshot:** Capture at most a small number of bounded CPU/memory/task snapshots, labeled as local observations only; do not extrapolate Azure F1 capacity.
7. **Cleanup:** Remove only this experiment's disposable container, image and temporary context. Do not prune shared caches or unrelated resources. Verify before/after Git status and reliable file-hash comparison.

## STOP conditions

STOP immediately on unexpected process identity, privilege or capability increase, administrative collector activity, secret exposure, failed preflight, unclassifiable unsafe output, inability to maintain the limits, or need for source repair. If readiness is absent at 30 seconds, record failure, perform bounded cleanup, and STOP. Do not run a second container or broaden the experiment.

## Required report

Provide: exact baseline and unchanged-worktree evidence; image/container counts and budget; timestamped startup timeline; per-probe sanitized error category; listener and HTTP results; log-capture outcome; Option A identity findings; stop signal evidence versus inference; exit/OOM results; resource snapshots; cleanup verification; and a matrix using `PASS`, `FAIL`, `NOT_OBSERVED`, or `NOT_QUALIFIED` with explicit limits. State whether the evidence supports startup-delay variability, without claiming a definitive root cause. Recommend the smallest **separate** next authority for enabled-mode shutdown semantics and self-owned FD observation, if appropriate.

## Persistent release gates

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, Azure passive qualification and public Worker activation remain **NO-GO**. No public Worker activation, Azure deployment, ingress TLS trust shortcut, or expanded qualification is authorized.

**STOP — owner review required after the single observation.**

# Release 1.14 WP07 — Administrative REST Lifecycle and Descriptor Observability: Read-Only Diagnosis Authority

## Authority and decision

Owner authorizes **targeted, read-only diagnosis only** of the second local-container requalification results. This authority does **not** authorize repair, instrumentation edits, tests, process execution, container builds/runs, deployment, or a new qualification. Stop after producing the diagnosis and request separate owner approval for any changes.

The repository is `samuel-santos-engineer/AIQuantTradingResearch`, branch `draft/release-1.14-wp07-partial-public-disabled`, reference HEAD `67893f51931ee183f71050a83b81127e6e648b87`. These are expected identifiers, **not permission to reset or checkout**. Preserve the existing dirty worktree, including all prior candidate implementation and unrelated edits. Do not commit, stage, stash, clean, rebase, checkout, or alter any file.

## Authoritative observed evidence

The latest bounded local-container run built one image and started two disposable containers. Source and 70 captured file hashes matched before/after. Enabled startup succeeded: router PID 1 `1100:1100`; lifecycle agent and Streamlit `1000:1000`; no resident root helper; empty supplementary groups, zero effective/permitted/inheritable capabilities, `NoNewPrivs=1`. Enabled health/root returned HTTP 200, basic WebSocket handshake HTTP 101 and close exchange passed, and unauthenticated `/admin` returned empty HTTP 503 with `no-store` under unchanged TLS denial policy. Enabled shutdown completed within five seconds but exited **64** with `AIQ_ADMIN_FAILURE=Shutdown`. FD metadata inspection from outside the process was permission-denied; descriptor flags and key confinement remain **NOT QUALIFIED**.

Disabled mode used direct Streamlit PID 1 as `1000:1000`, with no administrative router or agent. Finite HTTP health/root checks failed, generic fallback was not exercised, and shutdown exceeded the five-second stop deadline and required forced termination (**137**). The cause is unknown. A prior local qualification had observed successful disabled health/root and normal exit; do not assume either result is representative without examining differences.

Observed local snapshots: enabled 98.45 MiB / 20 tasks, disabled 52.89 MiB / 4 tasks, under 0.5 CPU, 512 MiB, 64 PIDs. No Azure F1 capacity inference is permitted. The run reported 514.8 seconds build-through-cleanup; full preflight-to-final timing was not measured. Zero authenticated collections. Cleanup completed.

Earlier targeted repairs corrected FD inheritability, `PR_SET_KEEPCAPS=8`, added sanitized stage codes, and reconciled **Option A**. All 53 synthetic tests passed, but a standalone Linux descriptor experiment failed its setup precondition and did not reach `exec`. Successful enabled startup provides only indirect evidence of FD-channel survival.

## Questions to answer (in priority order)

### A. Disabled readiness and forced shutdown

1. Trace the exact disabled-mode chain from `container/entrypoint.sh` through `env -i`, UID/GID drop, `exec`, Streamlit command/port, readiness path and signal handling. Identify whether Streamlit really becomes PID 1 and what its default SIGTERM behavior is, distinguishing repository facts from external assumptions.
2. Inspect the qualification probe expectations and timing **only if the probe script or captured evidence is available**; otherwise explicitly list missing evidence (startup timestamps, stdout/stderr stage logs, HTTP status/errors, probe duration, stop signal/grace period, process state). Do not invent probe details.
3. Compare with the first disabled-mode PASS run and isolate plausible differences in image, code, CPU constraints, scheduling, test order, startup warmup, process signal behavior or Docker invocation. Label each explanation PROVEN / SUPPORTED HYPOTHESIS / UNKNOWN.
4. Explain what would discriminate startup delay, health-path mismatch, Streamlit exception, and signal/shutdown handling; propose the smallest **future** bounded observation, not an execution now.

### B. Enabled exit 64 on normal stop

1. Trace signal delivery to admin router PID 1, ASP.NET host lifetime, cancellation, lifecycle stop message, agent acknowledgment/EOF, Streamlit termination, exit-code propagation and all branches emitting `AIQ_ADMIN_FAILURE=Shutdown` or exit 64.
2. Distinguish clean shutdown expected semantics from a genuinely failed stop protocol; identify any race or bounded-timeout interaction with Docker's five-second grace period.
3. Specify what log stage/outcome and timing evidence would identify the first failing transition without leaking secrets. Avoid recommending unbounded waits, restart loops or broad exception dumps.

### C. Safe FD and credential observability

1. Inspect actual descriptor mapping and self-check code in bootstrap, presentation agent and .NET admin process. Determine which FD facts can be validated **within the owning process** without cross-UID `/proc` inspection: descriptor presence, socket/pipe type, inheritable/close-on-exec state, expected endpoint direction, closed extras and expected EOF semantics.
2. Separate Linux/Python/C# APIs and privilege assumptions; distinguish self-reported runtime assertions from independent proof. Do not propose logging descriptor contents, raw environment variables, key bytes, key hashes, token values or unrestricted FD listings.
3. Design a fixed, sanitized, disabled-by-default, bounded attestation scheme (if necessary), including safe PASS/FAIL/NOT_RUN states, no sensitive metadata and explicit tests; do **not** implement it.
4. Explain why full credential confinement and complete trusted-dependency immutability remain unqualified even if descriptor checks pass.

### D. Contract and regression preservation

Preserve **Option A**: diagnostics disabled by default, direct Streamlit on port 8501, no admin router/agent/collector, generic UI fallback including HTTP 200 permitted. In enabled mode, dedicated `aiq-admin=1100:1100` router PID 1, `aiq=1000:1000` presentation identity, no resident root helper, immutable trusted code, finite lifecycle, no cross-UID signaling, and no private provider/SQLite/Worker startup dependencies. Keep HTTP/WebSocket/Origin behavior and TLS ingress trust policy unchanged; never accept forwarded TLS headers as proof or bypass HTTPS enforcement.

## Permitted operations

- Read-only Git metadata/status/diff inspection, source reads, targeted searches, and reading existing test files and locally available prior qualification reports.
- Reason about relevant publicly documented OS/runtime semantics, clearly separating source-derived facts from assumptions. External research is optional, not a prerequisite; cite specific documentation if used.
- Compare the source to the user's observed evidence and report a minimal proposed future repair/observation envelope.

## Prohibited operations

- **No writes of any kind** in the repository or generated source/test/documentation files; no autoformat, patch, commit, branch modification, or dependency changes.
- **No Python/.NET test or application execution**, subprocess experiments, Docker commands, HTTP probes, Azure calls, GitHub mutations, deployments, DB/SQLite access, Workers, credential access or key generation.
- No permission escalation, `/proc` cross-UID workarounds, security-policy weakening, arbitrary environment dumps, or attempts to obtain production secrets.
- Do not silently broaden to multiple-instance/revision-overlap analysis, Azure ingress qualification or public Worker activation.

## Required report format

1. **Baseline and actions:** branch, HEAD, initial/final worktree status and exact read-only inspection categories; disclose any unavailable artifacts.
2. **Evidence matrix:** disabled readiness, disabled stop, enabled stop, FD self-observation, key confinement, each with PROVEN / SUPPORTED HYPOTHESIS / UNKNOWN and source file + line anchors.
3. **Causal trees:** for both shutdown paths and disabled HTTP readiness, identifying the earliest possible divergence and discriminating evidence.
4. **FD-observability design:** safe assertions, security implications, proof limits and candidate focused tests, without implementation.
5. **Smallest next authority:** literal file paths that would need changes **if justified**, narrowly scoped changes and tests, whether a read-only runtime probe is needed first, and strict STOP gates. Do not assume code changes are required.
6. **GO/NO-GO and STOP:** state that this authority concludes without repairs or container requalification.

## Mandatory release gates

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability **NO-GO**; Azure passive qualification **NO-GO**; public Worker activation **NO-GO**. Successful enabled startup and basic WebSocket are useful local observations only, not production or credential-confinement clearance.

**STOP — return the diagnosis for owner review.**

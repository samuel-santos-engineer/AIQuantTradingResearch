# Codex Authority — Release 1.14 WP07: .NET 10.0.12 Reaper Evidence and Guardian Trust Contract (Read-Only)

## Authority class and decision boundary

**READ-ONLY DESIGN/EVIDENCE INVESTIGATION ONLY.** The owner authorizes investigation, not implementation. Conclude with **DESIGN CLOSED** or **DESIGN NOT CLOSED**, supported by a traceable evidence ledger. A design is CLOSED only if every mandatory lifecycle, security, failure-containment, and timing obligation has a concrete defensible mechanism and a verification plan; do not force closure.

**Frozen baseline:** repository `AIQuantTradingResearch`, branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Preserve all dirty/untracked work, all tracked contents, and Git metadata. Confirm branch, HEAD, and initial/final `git status --porcelain=v1`; STOP on mismatch. Read-only commands only. Do not checkout, reset, clean, stage, commit, push, install, build, test, or run containers. Do not generate repository artifacts. An answer in Codex's response is sufficient; no files need be created.

## Established evidence; do not overstate

The second owner-executed enabled-mode qualification reported .NET runtime `10.0.12`, `AIQ_ADMIN_WAIT=TARGET_ANY_RETURN_ERROR_ECHILD`, `AgentTerminationUnverified`, container exit `64`, no OOM, and no forced-termination evidence. The router was PID 1 / UID:GID 1100:1100, while agent and Streamlit ran as 1000:1000. Agent STOP acknowledgment and channel EOF preceded the failed `waitpid(-1, WNOHANG)` verification. **ECHILD is confirmed; the identity of any competing reaper and the agent's actual exit code are NOT confirmed.** The guardian design has been assessed **DESIGN NOT CLOSED**. Preserve that classification unless new evidence warrants changing it.

## Objective A — exact runtime reaping behavior

1. Inspect existing repository files, dependency pins, Dockerfile/runtime image references, and available locally installed or owner-supplied **.NET 10.0.12** native source artifacts. Determine the precise relevant runtime build/commit or explain why it cannot be established. Do not conflate SDK 10.0.103, local runtime 10.0.3, and observed container runtime 10.0.12.
2. Trace PID 1 SIGCHLD handling, native child reaping, order relative to managed POSIX signal callbacks, and status retention. Cite exact file paths, symbols, line ranges, source provenance, and version. Determine whether `waitpid(-1, WNOHANG)` in the application can race a runtime reaper for the externally forked agent.
3. Investigate supported public APIs or documented contracts for obtaining an **actual kernel exit status** of a child forked before the .NET router `exec`, versus `Process.Start`-created children. Distinguish child-status ownership, process disappearance, pidfd readiness, and status recovery. Explicitly identify unsupported internal hooks or assumptions.
4. If matching source is absent, record `NOT_VERIFIED`; request the minimum specific owner-supplied source/version evidence required. Do not infer exact 10.0.12 behavior solely from other patch versions. Public documentation/source browsing is permissible only if accessible without executing builds or downloading/installing binaries; cite source versions precisely. Do not claim source inspection when only an earlier report is available.

## Objective B — explicit guardian trust and lifecycle contract

Evaluate the proposed enabled-only topology: router remains PID 1 / UID:GID 1100:1100; unprivileged UID 1000 guardian is direct parent of UID 1000 agent, which owns Streamlit. Finite root bootstrap only; no resident privileged helper. Disabled Option A remains direct Streamlit PID 1 and unchanged.

1. Define threat assumptions: trusted immutable code and image provenance, bootstrap-opened channel identity and descriptor confinement, same-UID process attack surface, whether guardian status attestation is acceptable evidence of a **kernel wait on the bound agent**. Separate trust in channel possession from trust in report truthfulness. Do not assume same-UID isolation is stronger than it is.
2. State explicitly whether **independent guardian exit-code-0 proof** is a mandatory requirement under the existing shutdown contract. If proposing to replace it with authenticated, trusted guardian `RESULT` plus independently observed guardian closure, explain the exact trust-policy change and flag **OWNER DECISION REQUIRED**. No silent relaxation.
3. Specify a bounded protocol/state machine: guardian binds agent PID from its own fork, observes only that child, handles EINTR, ECHILD, nonzero/signal exits, sends one authenticated fixed-format result, and closes; reject early EOF, malformed/partial/duplicate/reordered/stale frames, unverified exit, cancellation and timeout. No arbitrary text, credentials or raw process data in diagnostic output.
4. Establish guardian failure containment and termination observation: crash before/after wait or report, guardian hangs, agent/UI orphaning, router cancellation, simultaneous failures, container PID 1 exit. Distinguish **application clean shutdown PASS** from **container failure containment**. Do not treat container teardown, pidfd readiness, EOF, `ECHILD`, disappearance, or a self-report as independently proving exit 0.
5. Evaluate whether all mandatory steps fit one monotonic four-second coordinated deadline and five-second Docker stop grace, including host draining and resource disposal. Do not reset deadlines per phase. No cross-UID signaling, added privileges/capabilities, privileged helper, broadened `/proc` access, secrets exposure, extra paid resources, or Azure calls.
6. Compare against simpler supported alternatives (exclusive reaping in .NET PID 1, child-specific wait/pidfd, existing agent restructure). Identify why each does or does not satisfy the same proof obligations; do not choose guardian merely by default.

## Deliverable — read-only owner decision memo in Codex response

Provide:

- Baseline and preservation evidence, and exact sources inspected, with version and line-level citations where available.
- Evidence ledger: **CONFIRMED / SUPPORTED / UNPROVEN / NOT_VERIFIED**, separating this run's evidence from runtime-source interpretation.
- Runtime 10.0.12 reaping finding, including exact code paths or explicit source-availability blocker.
- A trust-contract decision table: guardian attestation, guardian termination, process-tree containment, required owner approvals, and whether each obligation is closed.
- State machine and failure matrix, with terminal PASS/FAIL/NOT_QUALIFIED outcomes; no `ECHILD`-as-success shortcut.
- A comparison of alternatives, security impacts, bounded timing, and verification strategy (offline deterministic tests vs later separately authorized real Linux PID 1 tests).
- Final verdict **DESIGN CLOSED** or **DESIGN NOT CLOSED**. If CLOSED, propose the **smallest provisional exact-path implementation allowlist** and proof gates, but **do not implement**. If NOT CLOSED, list specific blockers and the minimum next read-only evidence or owner decision needed; do not propose an implementation authority as if approved.

## Non-negotiable STOP conditions

STOP without modification on branch/HEAD mismatch; inaccessible essential source; inability to establish status provenance; ambiguous guardian integrity/exit obligations; unbounded cleanup; changes requiring privileged signaling, credentials exposure, or frozen PID 1 model alteration. Report blockers rather than inventing evidence.

**Forbidden:** source/test/script/runbook edits; Docker, HTTP or Azure probes; builds, tests, package restore/install; Git writes; new credentials or secret inspection; Worker/SQLite/provider access; deployment; further qualification runs; public runtime activation.

**Release gates unchanged:** `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification, and public Worker activation remain **NO-GO**. **STOP — owner review required.**

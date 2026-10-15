# WP07 — Read-Only Qualification Plan

Date: 2026-10-10. Status: planning only; offline implementation remains frozen; public invocation disabled.

## Authority and evidence boundary

This document implements the owner's request for a read-only qualification plan. Only this planning artifact is created. No proposed test below is executed by this authority. No source/test changes, privilege changes, deployments, Azure API calls, load generation, restart, storage mutation or recovery operation are authorized. Any active Azure test needs its own explicit owner authority identifying the target, literal diagnostic paths/image, operations, request/CPU/time budget, rollback and cleanup. Read-only investigation of Azure also requires a separately agreed access scope; this plan makes no cloud calls.

The controlling snapshot is [the frozen evidence assessment](WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md), including its twelve source/test hashes and signing exception. Existing offline passes establish policy behavior only. The external-supervision candidate, supervisor-owned durable journal, bounded credential-checked IPC, occupation before Worker creation, authenticated fresh quota and mandatory abuse/headroom controls remain conditional architecture decisions. This plan does not resolve them by assumption or amend approved numerical/security contracts.

The target has no demonstrated writable cgroup delegation. Do not retry with privileges, a new paid resource or additional service under this plan. A failed or unavailable prerequisite is **blocked**, a violated invariant is **failed**, and absent/skipped evidence is **unproven**. None counts as passed. No hard process-tree CPU ceiling, end-to-end isolation or production request-size enforcement is claimed.

## Evidence packet and common test bounds

Every proposed execution packet must identify the candidate source hashes, immutable image digest, OS/kernel/runtime, target scope, role map, backend/mount identity, actual signed/unsigned artifact status, exact harness version, start/end UTC and monotonic times. Record case ID, observer identity, actions, expected/actual results, exit statuses, protected process identities, journal hashes/transitions and sanitized denial reasons. Use random synthetic canaries, not real private credentials or provider requests. Redact tokens and user data; retain canary digests, not secret contents.

Future local harnesses must run in a disposable, explicitly authorized environment. Do not exercise real identity changes, namespaces or process killing on the developer host without that scope. Proposed default per-case limits: 30 seconds wall time, one admitted Worker, at most two controlled descendants, no detached background tasks, at most 32 client processes and 256 scheduled protocol operations. Payloads stop at the rejection boundary rather than unbounded flooding. Observation buffers are bounded to 2 MiB per case; artifacts must fit a preapproved storage budget. Timeouts cause stop and controlled cleanup, never admission-state clearing.

These are harness safety caps, not changes to product limits. Any resource envelope that cannot contain a valid contract test requires a revised execution packet before running it. For target execution, reserve quota for diagnostics and cleanup before starting. Stop on missing/freshness-uncertain telemetry, <=20% headroom, unexpected private access, unexpected process, unexplained occupation loss, unbounded output or inability to restore the target. No automatic retries of failed target cases.

## Q-ID — Supervisor, Worker and broker identity separation

Required evidence:

- A concrete fixed-role launch sequence and process-tree diagram: bootstrap privilege lifetime; supervisor UID/GID 1001, UI 1002 and broker/Worker candidate 1003; real/effective/saved IDs, supplementary groups, capabilities, namespaces, inherited descriptors, parent identities and credential-drop order. Private identity 1000 must remain protected.
- Kernel-observed process identity, not PID alone: process start identity plus pidfd where supported, namespace identity and a non-reusable invocation token. An independent observer must bind READY/START/cleanup to that boundary.
- Permission/access matrix for socket, journal, executable, configuration, private cache and synthetic canary locations; environment/descriptor allowlists; authenticated bounded startup handshake and parent-death detection.

Proposed bounded procedures, not executed:

1. Launch the fixed roles once, inspect credentials before READY and after credential drop, and test one permitted and one denied operation for each matrix cell. UI/Worker must not read or mutate supervisor state or private canaries.
2. From a deliberately adversarial Worker and then one descendant, attempt broker socket access/impersonation, forged READY/START, stale-token replay, signal delivery, ptrace/process-memory access, `/proc` descriptor/environment access, executable/configuration replacement and journal/lock access. Perform each attempt once per role and record the kernel result and broker response.
3. Test EOF, partial handshake, wrong credentials, duplicate START and reused PID/token. Observe that no new Worker starts and occupation remains conservative after uncertainty.
4. Terminate each owning role at the prescribed handshake boundary in an isolated harness. Independently observe descendant exit before any subsequent admission. Exercise UI death and supervisor death separately; do not infer cleanup from a released flock or elapsed time.

Pass gate: all prohibited access is denied by the demonstrated boundary, only the fixed authenticated role may start the registered invocation, and no overlapping tree is admitted. **Distinct supervisor/UI UIDs do not prove the Worker-to-broker boundary:** broker and Worker sharing UID 1003 may permit signals, tracing or impersonation. If the candidate cannot prevent these, stop and request an architecture/security-contract amendment; do not silently assign a new identity or relax the invariant. A mock credential check or an honest Worker run cannot pass this gate.

## Q-ST — Azure storage durability and admission state

Required evidence: exact proposed backend and mount semantics; service-supported durability/locking guarantees and their limits; ownership enforcement; atomic replacement and flush behavior; cross-process and cross-instance coordination scope; recovery after container/app/host events; and independent persistence observation. Existing Linux temporary-file tests are insufficient for Azure CIFS. A single instance cannot prove cross-instance exclusivity.

Proposed procedures in a dedicated diagnostic namespace with explicit future authority:

1. Pre-provision protected lock/state through the trusted owner path. Try missing state, wrong ownership/mode, symlink/hardlink, truncated/oversized/duplicate-field state and read/write denial; none may initialize an empty budget or launch a Worker.
2. Use two independent contenders at most: hold the admission lock, attempt a bounded competing acquisition, and independently reopen/verify the committed state after release. Where platform scope permits multiple instances, repeat with two instances; otherwise leave that obligation blocked.
3. At each of five persistence boundaries—before temporary write, after file flush, after replace, after directory flush, and before acknowledgment—inject one interruption. Repeat each boundary at most three times. After restart, state must be either the intact prior state or intact committed state; any uncertainty denies admission. A reservation acknowledged as durable must not disappear.
4. Separately exercise process death, authorized container/app restart and backend interruption. A process kill is not power-loss or host-loss evidence. If actual host/storage failure cannot safely be exercised, document reliance on authoritative backend guarantees and the remaining untested failure modes for owner decision.
5. Verify full reservation/count history and occupation survive recovery, including scope/boot changes and clock rollback. No public command, stale-lock deletion, lease timeout or age-based reset may grant a clean budget. Recovery clearing requires independently verified absence of the owned tree and an authorized durable transition.

Pass gate: the chosen backend provides demonstrated ownership, coordination and persistence for the declared deployment scope; ambiguous/missing state denies. If target filesystem semantics cannot supply them, backend selection stays unresolved. No schema/backend change is authorized here.

## Q-QF — Quota authentication, freshness and accounting

Required evidence: concrete credential-free-from-owner acquisition path, least-privilege authentication proof without emitting tokens, exact resource/application/instance scope, daily and short-period CPU units/limits/usage/reset, source observation timestamp, publication lag bounds and replay identity. Document the distinction between source observation time, retrieval time and reset time. A successful authenticated response or fresh HTTP retrieval alone does not prove fresh counters.

Proposed procedures:

1. Examine the authoritative API/backend contract and sanitized sample schema. If no trustworthy observation timestamp or publication-lag guarantee exists, mark freshness blocked; do not synthesize `observedAt` from receipt time or `nextResetTime`.
2. With an independent clock and controlled local fixtures, test observation ages 59, 60 and 61 seconds; future time, clock rollback, missing counters, wrong scope/units, duplicates, replay/conflict and expired reset. Test headroom above, exactly at and below 20% for both counters. Apply frozen inclusive/exclusive semantics; any discovered ambiguity requires review.
3. For a future authorized target acquisition, capture a bounded sequence of at most ten observations over at most five minutes, with no generated workload unless separately authorized. Correlate acquisition and source timestamps and scope; report uncertainty rather than subtracting an invented lag.
4. Only with separately authorized workload, reconcile Worker, broker/supervisor and orphan CPU with platform counters across reset boundaries. Record counter resolution, lag, missing exits and measurement uncertainty; charge full reservations and never use incomplete accounting as proof of cleanup.

Pass gate: authenticated evidence genuinely satisfies <=60-second observation freshness and >20% headroom in both quota classes, with fail-closed behavior on loss. Prove the generation/supervision allocation <=10% of the verified daily quota, using its actual limit. Current null acquisition remains unavailable; no managed identity/RBAC action is implied.

## Q-CF — Combined-failure recovery

Use the same independent observer and durable store identity as Q-ID/Q-ST. Define exactly who owns cleanup when each role dies, who can recover persistent occupation, and how death is distinguished from loss of observability. Public clients may never trigger recovery commands.

| Injection boundary | Paired failure | Required observation |
|---|---|---|
| Durable reservation before PREPARE | Supervisor death + storage unavailable | No START; reservation/occupation retained or admission denied on uncertainty |
| READY before registered commit | Broker death + lost acknowledgment | No unregistered launch; denial and conservative occupation |
| Running commit before START | Supervisor death + IPC EOF | No second admission; potential-start state preserved |
| START sent before acknowledgment | Worker/descendant alive + broker death | Independent ownership/cleanup proof required; no timeout reopening |
| Worker exit before independent verification | Verifier unavailable + stale quota | Neither settlement nor new generation permitted |
| Verified state before settlement flush | Storage interruption + controller restart | Prior/committed intact state or fail-closed uncertainty; no counter discount |
| Recovery observation | PID reuse + clock rollback | Invocation boundary remains distinguishable; stale identity cannot clear occupation |

Proposed bound: one execution per row, then up to two repeats only after reviewed local results; one Worker/two descendants maximum. Target repetitions need explicit budgets and authorization. Establish fault ordering with observer events, not sleeps. Report admission-lock lifetime, occupation lifetime, actual descendant exits and durable state separately. If cleanup cannot be established within 30 seconds, terminate the diagnostic under its authorized cleanup plan and keep admission denied; do not claim the safety cap is a product recovery deadline.

Pass gate: every interleaving preserves no overlap and durable conservative accounting. Locks, process exit codes, numeric CPU reports or successful IPC alone cannot establish tree death. These procedures extend the earlier guardian handshake/parent-death/orphan proof obligations; no completed implementation is presumed.

## Q-AB — Abuse resistance and health preservation

Required evidence: an authoritative durable global limiter across the declared instance scope, bounded client/session state, restart semantics, no eviction-based budget reset, and health admission independent of public request queues. Client identifiers may supplement, but never replace, global controls.

Proposed deterministic local procedures with controlled clocks and recorded schedules:

- Exercise generation count limits at 3/900 seconds and 15/86400 seconds, full 16 CPU-second reservation, 60/240 reserved-second caps, 10-second session cooldown and three expensive attempts/client/60 seconds. Test exact boundaries, rejection charging semantics, clock rollback and restart without granting a new budget.
- Exercise cached-read ceilings of 30 global/60 seconds, two global/one second, six/session/60 seconds and one active cached read. Rotate tabs/sessions/client identifiers; ensure global controls remain binding. Every actual Worker spawn, including warm cache reads, still requires occupied/reserved admission; these ceilings are not promised throughput.
- At 1,024 supplemental records and 30-minute idle expiry, attempt one additional identity and expiration/reentry. Test that bounded state cannot erase still-relevant global counters or occupation. Long windows use controlled clocks locally, not accelerated unreviewed target time changes.
- Send bounded malformed/oversized requests, slow header/body delivery, replayed handshakes and disconnections. Denial must create no Worker/queue growth, emit no private details, and retain bounded allocations and deadlines. Test 4,096/4,097-byte request boundaries through the eventual production ingress, not only helper calls.

Future target profile proposal: at most 32 clients, 256 scheduled attempts and five minutes, with expensive starts capped by the approved rolling generation limits and the remaining quota budget. Probe health at most once/second (maximum 300 probes), recording actual sample counts; insufficient sample counts remain inconclusive. Before a profile, measure an authorized baseline. Warm health p95 <=1 second, each <=3 seconds; loaded p95 <=2 times baseline and zero unexpected 5xx/timeouts. Cold/warm request p95 <=5/2 seconds, Worker RSS <=160 MiB and container <=640 MiB. Do not execute if resource measurement or cleanup cannot be bounded.

Reconcile measured per-job CPU <=20 seconds (the 16-second reservation plus four-second overshoot allowance), global quota headroom and the daily allocation. This is an acceptance measurement, **not a kernel enforcement guarantee**. Without writable cgroups, an uncontrolled runaway process cannot be made safe merely by polling; an adversarial CPU test requires a separately proven bounded cleanup mechanism and explicit authority. Otherwise report this obligation blocked.

Pass gate: no bypass through identity rotation, concurrency, warm reads, malformed traffic, state eviction or restart; health and resource criteria met with interpretable evidence. Existing offline tests do not implement the complete durable abuse system and cannot satisfy this gate.

## Sequence, authorization packets and review outcome

1. Review this plan and frozen hashes. Confirm deployment scope and criterion semantics; keep all unresolved amendments visible.
2. Produce evidence-only interface/backend/authentication decisions. Resolve Q-ID shared-UID feasibility, Q-ST backend guarantees and Q-QF source timestamp proof before asking to implement a harness or live wiring.
3. Seek separate literal-path authority for any harness/source/test changes and exact authority for local identity/failure probes. Run bounded local gates only after approval.
4. Prepare an explicit Azure diagnostic execution packet: named app/resource/instance, immutable diagnostic and restoration digests, exact storage namespace/operations, identity permissions, restart/failure actions, request and quota budgets, independent observer, stop conditions, original-image restoration, artifact/image cleanup and cleanup verification. No new services, paid resources, privileges or access grants without express approval.
5. After separate target authority, execute prerequisite cases before combined-failure/load cases. Stop on any violated invariant or unavailable prerequisite. Record pass/fail/blocked/unproven per case and scope; never replace target evidence with local fixture output.
6. Assemble a sanitized qualification assessment under this prompters directory. Owner review must address residual measurement uncertainty, signing and every unresolved proof gate before any production integration/activation decision. This plan does not grant WP07 acceptance, issue closure or WP08 acceptance.

Current disposition: **all five qualification areas remain unproven or blocked for production**. No procedures were executed while writing this plan. The frozen implementation and disabled public invocation remain unchanged.

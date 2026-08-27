# Release 1.9 WP08 — Worker Lifecycle/Liveness Test Seam and Cancellation Adapter

Status: normative definition and manifest/path authority. Implementation requires a fresh consolidated WP08 authority.

## Purpose and boundary

This amendment resolves the WP08 cancellation-liveness blocker with two separate
mechanisms:

1. a production-valid Worker process-lifetime cancellation adapter; and
2. a bounded, test/demo-only liveness seam.

The Worker and Streamlit remain independently launched peers. The harness may
orchestrate them only for acceptance. No mechanism here makes either process a
supervisor of the other.

## Selected production cancellation adapter

Use standard .NET `Console.CancelKeyPress` in the Worker top-level composition.
`Program.cs` owns one `CancellationTokenSource`, registers one handler, sets
`ConsoleCancelEventArgs.Cancel = true`, and cancels the source. The source token
is passed to the existing `SimulatedLiveVisualizationExecution.Execute` call.
The handler is idempotent: repeated signals call `Cancel()` on the same source
and have no additional effect. The registration is detached and the CTS
disposed after execution completes.

This is selected over `PosixSignalRegistration` because the governed acceptance
environment is Windows, the Worker is already a console application, and the
standard console event requires no package, listener, IPC, or host redesign.
`IHostApplicationLifetime` is not selected because the current top-level host
is built but not run as a long-lived service. A combined signal abstraction is
unnecessary for the governed Windows acceptance boundary.

## Exact cancellation flow and exit semantics

The only cancellation path is:

`CTRL+C/Console.CancelKeyPress → Worker CTS → existing Execute(CancellationToken) → graceful unwind → process exit`.

The execution token is the same token already consumed by WP02 replay
cancellation semantics. No replay source, logical tick, fixture, count, or
end-of-replay behavior changes.

Intentional cancellation is not pipeline failure. When the existing execution
observes the token, `Program.cs` catches the resulting
`OperationCanceledException` at the Worker boundary, performs normal disposal,
emits no raw stack trace, and returns exit code `0`. Natural finite completion
retains its existing exit code and behavior. An unexpected lifecycle or other
exception retains the existing non-zero failure behavior. Cancellation during
an atomic handoff does not interrupt an already-running file write; existing
atomic-write guarantees remain authoritative.

## Test/demo-only liveness seam

The seam is an internal `IWorkerLifecycleLivenessGate` owned by the Worker. Its
production implementation is a no-op that completes immediately. It is not a
configuration key, environment variable, control file, listener, IPC channel,
sleep, loop, keepalive, or user-facing feature.

The test implementation is an in-memory cancellation-aware gate. It signals
`Entered` exactly once after the real first valid Worker publication has been
accepted by the existing presentation path, then awaits the Worker lifetime
token. Cancellation releases the gate. The gate has no data, persistence,
revision, replay, or observation side effects.

The gate is activated only by the WP08 test entry argument
`--wp08-test-liveness`, supplied by the harness when launching the Worker
process. The argument is a test/demo-only process entry mode, is rejected in
normal application modes, has no user configuration equivalent, and is not
accepted by Streamlit. The normal Worker path always uses the no-op gate.
This explicit test entry is the narrowest process-level activation that lets
the real Worker executable and the real production cancellation adapter be
tested without a hidden environment backdoor.

The gate is placed after the first successful real Worker publication and
before the next replay step/terminal completion. It therefore provides a
legitimate live Worker with a valid handoff while preserving the existing
three-observation Replay execution. No observation is added, delayed, or
reordered, and no Replay loop is introduced.

## Liveness and readiness

WP08 liveness is proven only when all conditions hold:

- the harness-owned Worker PID is alive;
- the Worker has entered the test-only gate;
- the gate's `Entered` signal is observed;
- the canonical handoff is a complete parseable accepted v1 envelope produced
  by the real Worker; and
- the production CTS token is not yet cancelled.

The process being alive alone is insufficient. The harness must send the real
console cancellation signal only after this readiness point. If natural
completion occurs before readiness, graceful cancellation is not proven.

## Signal portability and harness fallback

For governed Windows acceptance, WP08 sends CTRL+C/CTRL_BREAK using the
standard process-console mechanism to the tracked Worker process. No socket,
pipe, file, or custom signal transport is permitted.

The harness waits the already-fixed WP08 graceful cancellation bound of 5
seconds. If the Worker has not exited, it records graceful cancellation as a
failure, then may terminate only the tracked Worker PID/process tree using the
already-fixed 2-second forced-cleanup fallback. Forced termination is cleanup
only and never satisfies the graceful-cancellation assertion.

## Race behavior

- Before execution: a signal cancels the Worker CTS before dispatch; the
  existing execution receives an already-cancelled token and unwinds normally.
- During the gate: the gate releases on the same token and the Worker exits
  intentionally.
- During replay: existing WP02 token observation and cancellation semantics
  apply unchanged.
- After natural completion: the harness records natural completion, not
  graceful cancellation.
- Repeated signals: idempotent CTS cancellation; no duplicate publication or
  cleanup effect.
- During publication: existing atomic handoff behavior remains unchanged.

## Restart and shutdown integration

After a successful session-A graceful cancellation, WP08 applies its accepted
canonical/temp handoff residue rules, starts session B at the same isolated
runtime path, verifies startup cleanup, and proves a fresh valid publication.
No cross-session revision ordering is inferred.

The fixed WP08 shutdown order remains: capture final observation; request
Worker cancellation; await Worker exit/cleanup; terminate tracked Streamlit;
await Streamlit exit; verify process/listener residue; remove only harness-owned
temporary resources.

## Exact path authority

| Path | Owner | WP08 exception | Forbidden |
| --- | --- | --- | --- |
| `src/AIQuantTradingResearch.Worker/Program.cs` | WP03/WP05/Worker composition | Own/dispose the CTS, register `Console.CancelKeyPress`, recognize the test-only `--wp08-test-liveness` entry mode, inject the no-op or test gate, pass the token, and translate intentional cancellation to exit code 0 | Replay logic, Streamlit launch/control, IPC, listeners, persistence, schema, packages, broad Worker refactor |
| `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs` | WP03/WP08 | Add only the optional internal gate invocation after the first successful publication; preserve the existing `Execute(CancellationToken)` behavior and all Replay semantics | Replay source changes, sleeps, loops, observations, ticks, persistence, protocol changes |
| `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs` | WP08 | One narrow internal adapter/gate definition containing the CTS registration and test-only in-memory gate; no general lifecycle framework | Public API, user configuration, IPC, listeners, keepalive, supervision |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs` | WP08 | Launch the real Worker with the test-only entry mode, prove gate readiness, send the real console signal, assert exit/residue, and integrate restart/refresh/handoff/database checks | WP09 permanent architecture tests, browser tests, broad process killing, production supervisor |

No Application, Domain, Streamlit, schema, persistence, package, WP05, WP06,
WP07, or WP09 path is authorized by this amendment. The existing WP08
manifest-authorized production simulation/configuration paths remain consumed
without redesign.

## Required future acceptance

The implementation authority must prove the production-default no-op path,
test-gate entry/readiness, real Worker process signal delivery, CTS propagation,
intentional exit code 0, repeated-signal safety, natural-completion distinction,
forced-fallback classification, zero owned process/listener residue, restart
cleanup, WP02 replay preservation, all accepted WP05–WP08 demonstrations, and
the existing WP05/WP06/WP07/.NET regression gates. No new package or persistent
evidence path is allowed.

`WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM AND CANCELLATION-ADAPTER CONTRACT DEFINED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

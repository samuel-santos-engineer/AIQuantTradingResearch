# Release 1.9 WP08 — Liveness-Seam / Bounded-Refresh Reconciliation

Status: normative definition-only amendment. It supersedes only the timing of
the test/demo liveness hold in the Worker lifecycle seam. All other WP08
lifecycle, cancellation, Windows signal, residue, and predecessor contracts
remain binding.

## Reconciled conflict

The prior test seam held immediately after P1, preventing the next normal Replay
step and therefore preventing the required newer publication. The accepted
three-observation replay already provides enough normal execution for P1 and P2;
no Replay or publication change is needed.

## Selected model: two-phase per-process progression

When `--wp08-test-liveness` is absent, the seam remains a complete no-op and
production behavior is unchanged. When present, one execution-local, internal
counter/state is created for that Worker process and starts at
`AwaitInitialPublication`:

1. invocation after P1: signal initial readiness and pass through immediately;
2. invocation after the first qualifying newer publication P2: signal
   cancellation readiness and enter the existing cancellation-aware hold;
3. no P3 is required or permitted for this proof.

The state is not static, persisted, serialized, shared between processes, or
exposed to Application, WP05, WP06, or WP07. A new Worker process starts again
at P1 pass-through.

## P1 and P2 semantics

P1 is the first real Worker publication produced by the existing Replay and
pipeline execution. It must be an atomically published, parseable
`aiq-visualization-read-model-v1` envelope and satisfies initial readiness. The
seam does not hold after P1, so normal execution proceeds.

P2 is the next qualifying real Worker publication produced by the existing
Replay/pipeline loop. With the governed three-observation fixture and the
accepted one-observation request, it is the publication for the next logical
tick and is genuinely newer under the existing WP05 revision comparison. The
seam enters its cancellation-aware hold immediately after P2 publication and
before the next replay request. It does not manufacture, duplicate, delay, or
rewrite P2.

No new notion of newer is introduced: P2 must be newer according to the
existing replay logical-tick/revision comparison already used by WP05. An
equivalent or older envelope never satisfies the P2 gate.

## Readiness and timing

Initial readiness requires the Worker process to be alive and P1 to be a real,
complete, parseable handoff. It is not cancellation readiness.

Cancellation readiness requires all of the following:

- P2 is a real, complete, parseable handoff;
- P2 is newer than P1 under the existing comparison;
- P2 is observed within the unchanged 8-second bounded-refresh window;
- the Worker process remains alive; and
- the seam is in its post-P2 cancellation hold.

The bounded-refresh timer and 8-second deadline are unchanged from the accepted
WP08 lifecycle contract. The timer starts according to that contract after the
initial valid observation/P1; it is not reset for P2. The harness sends targeted
`CTRL_BREAK_EVENT` only after cancellation readiness.

## Cancellation and races

After cancellation readiness, the existing Windows helper sends targeted
`CTRL_BREAK_EVENT`; `Console.CancelKeyPress` cancels the Worker CTS; the hold
observes the existing execution token; and the Worker exits intentionally with
code 0. No release channel is added.

If cancellation arrives after P1 but before P2, the existing token semantics
apply, but the run does not satisfy bounded-refresh acceptance. If P2 occurs
quickly, the hold begins immediately and preserves P2 for observation. If the
harness observes P1 slowly, Worker still proceeds to P2 and then holds. If
natural completion occurs before P2, the run fails the bounded-refresh and
cancellation-readiness assertions; the harness must not fabricate P2.
Repeated cancellation remains idempotent. Cancellation during publication
retains existing atomic-file guarantees.

## Replay and production preservation

The three-observation fixture, requested count, logical ticks, end-of-replay
result, duplicate behavior, source authority, persistence, and replay
cancellation semantics are unchanged. The seam controls only whether Worker
orchestration holds after a qualifying publication. There is no production delay,
infinite loop, extra observation, new publication, IPC, listener, control file,
configuration key, or environment variable.

Streamlit remains an independent peer. It may read P1 and P2 normally and has no
control over seam progression or Worker cancellation.

## Exact path amendment

| Path | Owner | Narrow amendment | Forbidden |
| --- | --- | --- | --- |
| `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs` | WP03/WP08 | Replace the test-mode first-publication hold condition with one execution-local progression that passes P1 and holds after the first qualifying newer P2; preserve the existing cancellation token and all replay logic | Replay source/ticks/counts, persistence, publication semantics, delays, loops, IPC, Streamlit control |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs` | WP08 | Assert P1 pass-through, real newer P2 within 8 seconds, post-P2 liveness hold, targeted CTRL_BREAK, exit 0, and no forced kill; retain the existing 4/4 Windows/helper assertions | WP09 paths, fabricated P2, broad process termination, production supervisor |

`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
remains accepted predecessor WP08 work and is unchanged. Its `CreateProcessW`,
`CREATE_NEW_PROCESS_GROUP`, targeted `CTRL_BREAK_EVENT`, quoting, safe handles,
and cleanup behavior are not reopened.

## Required future assertions

The implementation authority must prove P1 validity and pass-through, genuine P2
newness and 8-second observation, post-P2 Worker liveness, targeted graceful
cancellation with exit code 0, no forced kill on the passing path, production
no-op behavior without the test argument, unchanged Replay semantics, and all
existing WP08 refresh/restart/Streamlit/residue and predecessor regression gates.

`WP08 LIVENESS-SEAM/BOUNDED-REFRESH RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP08 LIVENESS-SEAM/BOUNDED-REFRESH CONTRACT RECONCILED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

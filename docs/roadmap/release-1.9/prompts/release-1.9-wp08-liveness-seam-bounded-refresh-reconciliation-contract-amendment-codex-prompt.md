# Release 1.9 — WP08 Liveness-Seam / Bounded-Refresh Reconciliation Contract Amendment

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only reconciliation authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to reconcile a proven contradiction between:

1. the accepted WP08 post-publication liveness seam; and
2. the accepted WP08 bounded-refresh requirement.

It may amend only the **test/demo liveness-seam progression semantics** needed to make both requirements simultaneously satisfiable.

It must preserve:

- the accepted production cancellation adapter;
- the accepted Windows isolated process-group / targeted `CTRL_BREAK_EVENT` helper;
- Replay semantics;
- Worker/Streamlit production independence;
- bounded-refresh semantics themselves;
- all valid partial WP08 implementation and focused tests.

No implementation is authorized here.
No GitHub mutation.
No WP09.

---

# Proven conflict

Current accepted/implemented behavior:

- Worker is launched with `--wp08-test-liveness`.
- A real first canonical handoff is published.
- Immediately after that publication, the liveness seam waits indefinitely until the Worker cancellation token is requested.
- This correctly makes the Worker remain alive for targeted `CTRL_BREAK`.
- However, because execution is blocked after the first publication, no subsequent replay step/newer publication can occur.
- WP08 separately requires a **newer publication within 8 seconds** for bounded-refresh acceptance.

Therefore the current seam and bounded-refresh acceptance are structurally incompatible.

This is not an implementation defect.

---

# Preserved valid implementation

The following valid partial WP08 work must be treated as accepted predecessor state:

Production:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Tests:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Validated evidence:
- focused WP08: **4/4 passed**;
- `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- real Worker `--wp08-test-liveness`;
- real atomic handoff publication;
- targeted `CTRL_BREAK_EVENT`;
- graceful Worker exit code `0`;
- deterministic Windows command-line quoting;
- build: 0 warnings / 0 errors.

Do not reopen the Windows signal mechanism.

---

# Entry state

Expected:
- #233 Open / Backlog.
- #234 Open / Backlog.
- no GitHub mutation from blocked WP08 pass.
- focused WP08 4/4.
- build green.
- no completed Streamlit/restart/full-demo lifecycle mutation.

Verify read-only.

---

# Binding authorities to read

Read completely:

1. WP08 lifecycle/bounded-demonstration/process-residue contract.
2. WP08 Worker lifecycle/liveness test-seam + cancellation-adapter contract.
3. WP08 Windows isolated process-group console-signal test-helper authority.
4. Current partial implementation and WP08 focused tests.
5. #233 acceptance text.

This amendment supersedes only the contradictory portion of artifact 2 concerning **when/how the test liveness seam holds execution relative to bounded refresh**.

Everything else remains binding.

---

# Objective

Define one exact deterministic liveness progression that permits:

1. real first publication;
2. real newer publication within the existing **8-second** bounded-refresh requirement;
3. Worker remains alive after the required refresh;
4. harness sends the already-accepted targeted `CTRL_BREAK_EVENT`;
5. existing Worker CTS is canceled;
6. Worker exits gracefully with code `0`.

Do this without:

- adding observations;
- changing Replay ticks;
- changing Replay source behavior;
- introducing production delays;
- custom IPC;
- network listener;
- control file;
- user-facing config;
- changing the 8-second refresh bound.

---

# Phase 0 — Inspect actual seam placement

Read-only inspect the current partial implementation.

Determine exactly:
- where the liveness seam is invoked;
- whether it is called after every publication or only once;
- what state is available at the call;
- whether publication revision/count/tick is available;
- how execution returns to the replay loop;
- whether the seam can distinguish first vs subsequent publication without adding canonical semantics.

No mutation.

---

# Phase 1 — Reconciliation models

Evaluate only narrow test/demo progression models.

## Model A — first publication passes, second publication holds

Under `--wp08-test-liveness`:

- first real publication signals readiness and returns immediately;
- execution proceeds normally;
- second/newer real publication occurs;
- after second publication, seam enters cancellation-aware hold;
- harness observes the newer publication;
- harness sends `CTRL_BREAK`;
- hold releases via existing Worker token;
- Worker exits gracefully.

This is preferred if the seam is naturally invoked per publication.

## Model B — bounded publication-count gate

The seam tracks only test-liveness invocation count:

- invocation 1: pass-through;
- invocation 2+: cancellation-aware hold.

No domain/replay semantic changes.

## Model C — two-phase internal seam state

Internal states conceptually:

`AwaitInitialPublication`
→ `AwaitRefreshPublication`
→ `CancellationHold`

The state is test/demo orchestration only.

No externally visible canonical state.

## Model D — another minimal deterministic model

Only if actual code structure makes A/B/C impossible.

Choose exactly one.

---

# Phase 2 — Hard semantic rule

The reconciliation must preserve this distinction:

- **bounded refresh is produced by normal existing Replay/Worker execution**;
- **liveness is supplied only after the refresh proof point**.

The seam may control whether Worker orchestration waits, but it must never manufacture the newer publication.

---

# Phase 3 — Exact publication progression

Define exact expected sequence.

Preferred normative sequence:

### Publication P1
- produced by existing Replay execution;
- canonical handoff atomically published;
- satisfies initial Worker readiness;
- liveness seam does **not** block permanently;
- Worker continues.

### Publication P2
- produced by the next existing Replay step;
- revision/tick/data must be genuinely newer under existing semantics;
- canonical handoff atomically replaces P1;
- must be observable within the existing 8-second bound;
- after P2 publication, liveness seam enters cancellation-aware hold.

### Cancellation
- harness has observed P2;
- Worker remains alive;
- harness sends targeted `CTRL_BREAK_EVENT`;
- existing `Console.CancelKeyPress` cancels Worker CTS;
- hold releases;
- Worker exits code 0.

If current Replay naturally produces more than two publications before the harness observes P2, define whether the seam holds at exactly P2 to prevent race.

Prefer hold exactly after the first qualifying newer publication.

---

# Phase 4 — What counts as “newer”

Do not invent a new definition.

Use the already accepted WP08/WP05 revision/refresh contract.

Extract the exact existing comparison:
- revision;
- logical tick;
- source timestamp;
- or another already-governed field.

State it precisely.

P2 must satisfy the existing newer-publication rule.

---

# Phase 5 — Seam activation

Preserve:

`--wp08-test-liveness`

No new argument.
No environment variable.
No user-facing setting.

When absent:
- seam is a complete no-op;
- production behavior remains unchanged.

When present:
- only the amended two-phase progression applies.

---

# Phase 6 — Seam state ownership

Define the narrowest state owner.

Prefer:
- one execution-local/internal counter or state object owned by the simulated live visualization execution for that process run.

Requirements:
- resets each Worker process;
- not persisted;
- not serialized;
- not shared across restart;
- not exposed to WP05/WP06/WP07;
- not a canonical domain concept.

No static cross-process/session state.

---

# Phase 7 — No harness release channel

The prior conflict might tempt a harness-controlled release.

Avoid it unless absolutely necessary.

Preferred amendment must **not** add:
- named event;
- pipe;
- socket;
- file sentinel;
- HTTP endpoint;
- environment mutation;
- stdin command protocol.

The seam should advance automatically based on actual publication progression.

If a harness-controlled non-cancellation release is truly required, STOP and explain why an IPC-like control channel is unavoidable.

---

# Phase 8 — Readiness semantics amendment

Define two distinct test observations:

## Initial readiness
P1 exists and validates.

This means:
- Worker launched;
- real pipeline ran;
- first canonical handoff exists.

It does not yet mean “ready for cancellation”.

## Cancellation readiness
P2/newer publication exists and validates, and Worker is now in cancellation hold.

Only this state authorizes the harness to send `CTRL_BREAK`.

This distinction must be explicit.

---

# Phase 9 — Bounded-refresh semantics

Preserve the exact existing **8-second** maximum.

Timer origin must remain the one fixed by the lifecycle contract.

Do not reset/reinterpret the timer merely to accommodate the seam.

Specify:
- P1 observation time;
- bounded-refresh timer start;
- P2 observation condition;
- deadline.

If existing artifact already fixes these, repeat exactly.

---

# Phase 10 — Race prevention

Define deterministic handling.

## Harness observes P1 slowly
Worker must still proceed to P2; after P2 it holds, preventing natural completion.

## Worker produces P2 quickly
Seam holds immediately after P2, so P2 remains observable and Worker remains alive.

## Harness sends cancellation after P1 but before P2
This is invalid test ordering. Harness must not send until cancellation readiness.

If cancellation nevertheless arrives:
- existing token semantics apply;
- run does not satisfy bounded-refresh acceptance.

## Replay naturally completes before P2
Acceptance fails; do not fabricate P2.

## Repeated cancellation
Existing idempotent cancellation behavior remains.

---

# Phase 11 — Natural publication count

Inspect current Replay flow.

If the accepted three-observation Replay produces enough Worker publications for P1/P2:
- use them unchanged.

If it produces only one presentation publication despite three observations:
- STOP. Do not change Replay or publication semantics under this authority.

The amendment succeeds only if normal existing execution already provides a qualifying P2 once the seam stops blocking P1.

---

# Phase 12 — Cancellation hold semantics

After qualifying P2:

- Worker remains alive;
- no additional replay step proceeds;
- no P3 is required;
- hold waits only on existing Worker cancellation token;
- targeted `CTRL_BREAK` releases it;
- intentional cancellation remains exit 0;
- no pipeline failure.

No timeout inside production seam unless already defined.

Harness owns bounded timeout.

---

# Phase 13 — Restart semantics

Each new Worker process starts seam progression fresh:

- P1 pass;
- P2 hold;
- cancellation.

Do not carry seam phase across restart.

Preserve existing handoff cleanup/revision restart semantics.

---

# Phase 14 — Streamlit compatibility

The amendment must not alter Streamlit.

During P1 → P2:
- Streamlit/parser may observe normal handoff replacement.

At P2 hold:
- Streamlit can continue reading last canonical P2 envelope according to existing refresh/cache semantics.

No Streamlit control of seam.

---

# Phase 15 — Exact path/symbol amendment

Inspect binding path authority.

Authorize only the minimum existing production symbol needed to change seam progression.

Expected likely path:
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

If the accepted helper owns seam state, authorize that exact existing helper instead/as well.

Do not authorize changes to:
- `WorkerLifecycleCancellation.cs` unless truly required;
- Windows process helper;
- WP05/WP06/WP07 production;
- Replay source.

Authorize the existing WP08 test path for adjusted assertions:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

No new test file unless absolutely required.

---

# Phase 16 — Preservation of Windows helper

Explicitly state:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

is accepted predecessor WP08 work and is **not reopened** by this amendment.

Its:
- `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- targeted `CTRL_BREAK_EVENT`;
- quoting;
- safe handles;
- cleanup

remain unchanged.

---

# Phase 17 — Focused test amendment

Later Terra implementation must update/add focused assertions proving:

1. P1 is real and valid.
2. Worker does not enter terminal hold after P1.
3. P2 is genuinely newer.
4. P2 occurs within 8 seconds.
5. Worker remains alive after P2.
6. targeted CTRL_BREAK then exits Worker 0.
7. no forced kill in success path.
8. no Replay semantic change.
9. production run without flag remains unchanged.

Preserve existing 4/4 Windows/helper coverage.

Report new exact focused count after implementation.

---

# Phase 18 — Full demonstration sequence amendment

The consolidated WP08 demonstration must now follow:

1. launch Worker with `--wp08-test-liveness`;
2. observe P1;
3. start/continue bounded-refresh observation according to existing contract;
4. observe qualifying P2 within 8 seconds;
5. verify Worker remains alive in cancellation hold;
6. perform Streamlit/chain observations as fixed by artifact 1;
7. send targeted CTRL_BREAK;
8. observe graceful Worker exit 0;
9. perform restart sequence;
10. complete shutdown/residue audit.

If artifact 1 requires Streamlit launch before P1/P2, preserve its ordering. Only seam progression changes.

---

# Phase 19 — Regression requirements

Later implementation must rerun:

- focused WP08;
- full finite demonstration;
- build;
- Application;
- Infrastructure;
- Domain;
- Architecture;
- full .NET;
- WP05 Python 3/3;
- WP06 Python 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- Streamlit/pip checks.

Reference full .NET predecessor before WP08 tests remains 309/309.

---

# Phase 20 — Non-goals

Do not authorize:

- harness-controlled release IPC;
- extra Replay observations;
- changed Replay logical ticks;
- changed publication semantics;
- changed 8-second bound;
- production delay;
- infinite loop;
- new listener;
- new config/env setting;
- Windows helper redesign;
- custom IPC;
- Streamlit supervision;
- package changes;
- schema/persistence changes;
- WP09.

---

# Documentation artifact

If governed, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIVENESS_SEAM_BOUNDED_REFRESH_RECONCILIATION_CONTRACT_AMENDMENT.md`

No production/test/GitHub mutation.

Otherwise return normative definition in chat.

---

# Required completion report

## Conflict confirmation
Explain P1 terminal hold vs 8-second P2 requirement.

## Selected reconciliation model
Exact phase/counter/state behavior.

## P1 semantics
Exact readiness/pass-through behavior.

## P2 semantics
Exact newer-publication/hold behavior.

## Cancellation readiness
Exact condition for CTRL_BREAK.

## Race semantics
P1/P2/cancellation/natural completion.

## Replay preservation
Explicit zero semantic change.

## Path amendment
Exact production/test symbols.

## Windows helper preservation
Explicit unchanged status.

## Required future tests
Exact matrix.

## Mutation statement

If doc created:

`WP08 LIVENESS-SEAM/BOUNDED-REFRESH RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP08 LIVENESS-SEAM/BOUNDED-REFRESH RECONCILIATION MUTATIONS: ZERO`

## Next step

On success:

`WP08 LIVENESS-SEAM/BOUNDED-REFRESH CONTRACT RECONCILED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

---

# Stop conditions

Stop if:

- existing normal Replay/Worker flow cannot produce a qualifying P2;
- reconciliation requires adding/changing Replay observations or ticks;
- a harness-controlled IPC release is required;
- 8-second bound must change;
- Windows signal helper must be redesigned;
- a new production configuration surface is required.

Report the minimum next semantic decision if blocked.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 LIVENESS-SEAM AND BOUNDED-REFRESH RECONCILIATION CONTRACT AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP08 LIVENESS-SEAM AND BOUNDED-REFRESH RECONCILIATION CONTRACT AMENDMENT BLOCKED`

Do not emit COMPLETE unless the exact amended seam permits a real qualifying newer publication within the unchanged bounded-refresh contract and then holds the Worker for the already-governed graceful cancellation proof.

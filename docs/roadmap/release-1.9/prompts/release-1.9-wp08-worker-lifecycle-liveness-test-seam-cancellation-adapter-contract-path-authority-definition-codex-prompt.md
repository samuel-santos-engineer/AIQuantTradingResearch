# Release 1.9 — WP08 Worker Lifecycle/Liveness Test-Seam + Cancellation-Adapter Contract and Path-Authority Definition

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only authority** for Release 1.9 WP08, canonical issue **#233**.

The prior WP08 graceful-cancellation definition blocked because:

- `Program.cs` has no process-lifetime cancellation wiring;
- `SimulatedLiveVisualizationExecution.Execute` accepts a token but current Worker composition does not supply one;
- the real Replay execution is synchronous, finite, and only three observations long;
- therefore the Worker naturally exits before the harness can deterministically prove graceful cancellation;
- no legitimate bounded liveness condition exists under current production composition.

This authority is explicitly allowed to define **both**:

1. the minimum Worker-owned process-lifetime cancellation adapter; and
2. one narrow lifecycle/liveness test seam that allows WP08 to hold the Worker in a legitimate cancellable state for finite acceptance without changing Replay semantics.

No implementation is authorized.

No custom IPC.
No production sleep loop.
No Replay redesign.
No Streamlit supervision.

---

# Entry state

Expected:

- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor 309/309.
- build 0 warnings / 0 errors.
- WP05 Python 3/3.
- WP06 Python 6/6.
- WP07 semantic 2/2.
- WP07 presentation 2/2.
- no repository/GitHub mutation from the immediately preceding blocked definition pass.

Verify read-only.

---

# Binding predecessor authorities

Read and preserve:

1. accepted WP02 replay cancellation semantics;
2. WP03 Worker mode/configuration;
3. WP05 Worker/Streamlit independence and runtime lifecycle;
4. accepted WP08 lifecycle/bounded-demonstration/process-residue contract;
5. current `Program.cs`;
6. current `SimulatedLiveVisualizationExecution.Execute`;
7. existing Worker/Infrastructure test conventions;
8. Release 1.9 manifest/path ownership;
9. #233 acceptance text.

Do not redefine Replay logical ticks, duplicate behavior, finite completion, or cancellation semantics inside Replay.

---

# Objective

Define an exact minimal contract such that later Terra implementation can prove:

- Worker process owns a real process-lifetime cancellation source;
- that cancellation source flows into existing execution token parameters;
- WP08 harness can keep Worker execution legitimately alive long enough to request graceful cancellation;
- the liveness mechanism does not alter production Replay semantics;
- the liveness mechanism does not become a production keepalive;
- no custom IPC/listener/control file is introduced;
- the seam is test/demo-focused, narrow, bounded, and removable from acceptance reasoning outside WP08.

Also define exact path/symbol authority for implementation and focused tests.

---

# Phase 0 — Read-only architecture inspection

Inspect:

- `src/AIQuantTradingResearch.Worker/Program.cs`;
- Worker hosting model;
- DI composition;
- `SimulatedLiveVisualizationExecution`;
- any interfaces around execution;
- existing Worker tests;
- any internal test seams already used for Worker composition;
- visibility/friend-assembly conventions;
- test project dependencies;
- existing cancellation-token abstractions.

Determine whether an existing abstraction can support a narrow liveness seam without new public production semantics.

No mutation.

---

# Phase 1 — Separate production lifecycle from test liveness

Normatively define two independent concerns:

## Production cancellation adapter
Always production-valid:
- process lifetime signal;
- Worker-owned CTS or host lifetime token;
- token propagation into execution;
- graceful cancellation handling.

## WP08 liveness test seam
Only for deterministic acceptance:
- makes execution remain cancellable long enough to issue the production cancellation signal;
- must not change market/replay semantics;
- must not change observation count/ticks;
- must not alter production behavior when seam is absent.

These must be separate concepts.

---

# Phase 2 — Select production cancellation mechanism

Given the Worker is a Generic Host console app targeting `net10.0`, evaluate:

### Model A — Generic Host lifetime token
Use host/application lifetime infrastructure if current architecture can expose `ApplicationStopping`/host lifetime without converting the app into a new long-running service.

### Model B — `Console.CancelKeyPress` + Worker CTS
Top-level/composition-owned CTS, `e.Cancel = true`, token flows to execution.

### Model C — standard OS signal registration
Use `PosixSignalRegistration` where needed, only if standard and compatible.

### Model D — narrow combined adapter
One helper encapsulates standard .NET console/process signals cross-platform.

Choose one exact model.

No custom IPC.

---

# Phase 3 — Define cancellation adapter API

Define exact conceptual API/symbols.

Preferred shape may be equivalent to a tiny internal component such as:

`WorkerLifetimeCancellation`

with responsibilities only to:

- own a `CancellationTokenSource`;
- register the chosen process signal(s);
- expose `CancellationToken`;
- dispose/unregister handlers.

Do not create a generic lifecycle framework.

If `Program.cs` can safely own these directly with minimal logic, prefer no helper.

Fix exact ownership/disposal semantics.

---

# Phase 4 — Liveness seam candidate analysis

Evaluate narrow test-seam models that do **not** change Replay semantics.

Possible candidates:

## Seam A — injectable pre-execution gate
A narrow execution gate awaited immediately before real execution dispatch.

Test harness can provide a cancellable gate that:
- signals “entered”;
- waits until cancellation;
- production default completes immediately.

Pros:
- no Replay change;
- no observation/tick change;
- deterministic cancellation before dispatch.

Question:
- does #233 require cancellation during live replay execution specifically, or merely live Worker graceful cancellation?

If cancellation-before-dispatch is insufficient, reject.

## Seam B — injectable lifecycle hold after readiness publication but before finite completion
A test-only lifecycle coordination seam around Worker orchestration, not replay source.

Must preserve real pipeline publication and then remain alive until cancellation.

Production default no-op.

This may be preferable if #233 requires real Worker readiness before cancellation.

## Seam C — injectable `IWorkerLifetimeGate`/internal delegate
A single internal seam with production no-op implementation and test-controlled wait.

Must not expose user configuration.

Must not become a production feature.

## Seam D — test-only composition entry
A test assembly can invoke an internal Worker composition method with a custom lifetime token/gate, while actual `Program.cs` remains thin.

Only valid if process-level acceptance still uses the real production `Program.cs` adapter where required.

Do not select a seam that bypasses the production cancellation adapter entirely.

---

# Phase 5 — Liveness semantic requirements

The selected seam must satisfy all:

- production default behavior unchanged;
- no new config key;
- no environment variable;
- no sleep delay;
- no infinite loop;
- no change to replay fixture;
- no change to logical tick;
- no new observation;
- no new persistence;
- no Streamlit dependency;
- deterministic “ready-to-cancel” signal available to the test harness;
- cancellation releases the seam;
- bounded fallback/cleanup possible.

Define exactly where the seam sits in Worker orchestration.

---

# Phase 6 — Readiness semantics for the seam

Define exact test-visible readiness.

Potentially:
- seam signals `Entered`;
- Worker process remains alive;
- cancellation token not yet requested.

If #233 requires evidence of a real publication before cancellation, the seam must be placed **after** that publication and readiness must include a valid handoff.

If #233 only requires Worker graceful cancellation independently, a pre-execution seam may suffice.

Resolve from #233, not convenience.

---

# Phase 7 — Cancellation flow

Define exact flow:

external process signal
→ production cancellation adapter
→ Worker-owned token
→ selected liveness seam and/or existing execution token
→ graceful unwind
→ process exit.

If seam is waiting when signal arrives:
- seam exits due to token;
- Worker terminates gracefully.

If execution has started:
- token reaches existing `SimulatedLiveVisualizationExecution.Execute`.

No alternate cancellation channel.

---

# Phase 8 — Exit behavior

Define exact exit semantics.

Intentional cancellation:
- not pipeline failure;
- exact process exit code;
- no raw stack trace;
- cleanup/disposal runs.

Natural completion:
- normal existing behavior.

Unexpected exception:
- existing non-zero failure behavior.

Forced harness kill:
- cleanup fallback only, not proof of graceful cancellation.

---

# Phase 9 — Test seam visibility

Select exact visibility:

- `internal`;
- nested/internal delegate;
- internal interface plus `InternalsVisibleTo`;
- another repository-consistent pattern.

Prefer the narrowest visibility.

Do not expose a public production API merely for tests.

No user configuration surface.

---

# Phase 10 — Production path authority

Authorize exact path/symbol changes.

Expected:

`src/AIQuantTradingResearch.Worker/Program.cs`

Allowed:
- lifecycle adapter registration/ownership;
- token propagation;
- seam invocation/composition if necessary;
- graceful cancellation exit handling.

If helper required, authorize one exact new Worker file only.

No broad Worker path grant.

---

# Phase 11 — Application/Infrastructure path authority

If the seam naturally belongs outside Worker, justify exact layer and path.

Default preference:
- Worker-owned seam, not Application/Infrastructure domain logic.

Do not put test-liveness semantics into canonical pipeline contracts.

No changes to `SimulatedLiveVisualizationExecution` if token signature already suffices, unless a precise orchestration overload is strictly necessary.

---

# Phase 12 — Test path authority

Verify existing WP08 test path:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Authorize it for:
- seam-controlled liveness;
- real process signal delivery;
- graceful exit assertions;
- fallback cleanup;
- restart integration.

If a small unit test is needed for the adapter/helper, authorize one exact additional path only if necessary.

No WP09 paths.

---

# Phase 13 — Process-level proof requirement

The later Terra implementation must still prove the real production adapter at process level.

A pure unit test of the seam is insufficient.

At least one focused WP08 test must:
- launch real Worker process;
- establish seam-defined liveness/readiness;
- send the real production cancellation signal;
- observe graceful exit;
- verify no process residue.

The seam is a deterministic condition, not a replacement for process-level proof.

---

# Phase 14 — No Replay semantic change

Explicitly define:

- existing three-observation Replay remains unchanged;
- no added observations;
- no loop;
- no delay in Replay source;
- no changed end-of-replay result;
- no changed cancellation handling inside Replay source;
- no changed starting tick/count semantics.

The seam must be outside Replay semantics.

---

# Phase 15 — Integration with accepted WP08 demonstration

Amend the WP08 demonstration cancellation phase.

Define exact sequence:

1. launch Worker in harness-owned isolated environment with liveness seam enabled through test composition only;
2. reach the exact readiness point;
3. send production process cancellation signal;
4. confirm adapter requests Worker token;
5. seam/execution observes cancellation;
6. Worker exits gracefully within existing WP08 timeout;
7. verify exit code and residue;
8. continue accepted restart demonstration.

Do not change other WP08 timing values unless proven necessary.

---

# Phase 16 — How seam is activated

This is critical.

The seam must **not** be activated through a production user-facing config key.

Select one exact activation model:

- test-only DI composition;
- internal overload invoked by test harness;
- assembly-visible factory;
- environment-internal test hook only if repository governance explicitly allows such hooks.

Prefer test composition/internal API over environment variable.

Real production `Program.cs` process proof must still exercise cancellation adapter.

If process launch cannot inject the seam without a user-facing hook, define a narrow **test-only process entry point** only if the manifest permits it and it still uses the same production composition/adaptor.

Do not invent a hidden production backdoor casually.

---

# Phase 17 — Manifest ownership matrix

Produce exact table:

- path;
- owner;
- WP08 exception;
- symbols;
- allowed concern;
- forbidden adjacent concerns.

At minimum:
- `Program.cs`;
- optional one helper;
- `WP08LifecycleDemonstrationTests.cs`;
- optional one unit-test path if truly necessary.

No wildcard grants.

---

# Phase 18 — Required future tests

Later implementation must prove:

## Adapter
- process signal registered;
- token canceled;
- repeat signal safe;
- disposal/unregistration.

## Seam
- production default no-op;
- test seam reaches readiness;
- waits without sleep-loop;
- cancellation releases;
- no semantic data change.

## Process
- real Worker launch;
- deterministic liveness;
- graceful signal;
- exact exit;
- no residue.

## Restart
- graceful A shutdown;
- B restart;
- accepted cleanup/revision rules.

## Regression
- replay tests unchanged;
- Worker production flow unchanged when seam disabled;
- WP05–WP07 regressions;
- full .NET/build.

---

# Phase 19 — Scope exclusions

No:
- custom IPC;
- HTTP;
- socket listener;
- named pipe;
- control file;
- user-facing liveness config;
- production keepalive;
- arbitrary sleeps;
- infinite loop;
- Replay changes;
- schema/persistence changes;
- Streamlit supervision;
- WP09.

---

# Documentation artifact

If governed, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_LIFECYCLE_LIVENESS_TEST_SEAM_CANCELLATION_ADAPTER_CONTRACT_PATH_AUTHORITY_DEFINITION.md`

No production/test/GitHub mutation.

Otherwise return normative definition in chat.

---

# Required completion report

## Selected adapter
Exact mechanism/API/path.

## Selected seam
Exact location/activation/readiness semantics.

## Cancellation flow
Signal → adapter → token → seam/execution → exit.

## Production default
Proof semantics unchanged when seam absent.

## Process-level proof
Exact future harness flow.

## Path authority
Exact files/symbols/tests.

## Replay compatibility
Explicit unchanged contract.

## WP08 demonstration amendment
Exact cancellation/liveness step.

## Required tests
Exact matrix.

## Mutation statement

If doc created:

`WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM DEFINITION MUTATIONS: ZERO`

## Next step

On success:

`WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM AND CANCELLATION-ADAPTER CONTRACT DEFINED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

---

# Stop conditions

Stop if:

- no seam can provide legitimate liveness without altering Replay semantics;
- process-level signal cannot be delivered in the governed environment;
- seam activation would require a hidden production backdoor or user-facing config;
- required path authority conflicts irreconcilably with WP09;
- new package/runtime service required.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM AND CANCELLATION-ADAPTER CONTRACT/PATH-AUTHORITY DEFINITION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 WORKER LIFECYCLE/LIVENESS TEST-SEAM AND CANCELLATION-ADAPTER CONTRACT/PATH-AUTHORITY DEFINITION BLOCKED`

Do not emit COMPLETE unless one exact production cancellation adapter and one exact bounded test-only liveness seam are fully defined with deterministic process-level acceptance and no Replay semantic change.

# Release 1.9 — WP08 Worker Graceful-Cancellation / Liveness Contract + Manifest/Path-Authority Amendment

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to define and authorize the minimum Worker-owned production lifecycle mechanism required to satisfy the already-accepted WP08 finite-demonstration contract.

It may introduce one narrowly scoped production cancellation/liveness contract and the minimum exact path/symbol authority needed to wire it.

It does **not** implement code/tests.
It does **not** mutate GitHub.
It does **not** close #233.
It does **not** start WP09.

---

# Proven blocker

The binding WP08 lifecycle/demonstration contract requires proof that a live Worker can first receive a **graceful cancellation request** before any harness-only forced termination fallback.

Current production evidence:

- `SimulatedLiveVisualizationExecution.Execute` accepts a cancellation token.
- `Program.cs` invokes it without supplying a production lifecycle cancellation token.
- Current Replay input is finite and synchronous (three observations), so natural completion is not proof of graceful cancellation.
- The existing WP08 harness/test-only path cannot supply a missing production lifecycle token from outside the process.
- The accepted WP08 definition currently authorizes no production change for this gap.

Therefore WP08 implementation is blocked until a minimal Worker-owned cancellation/liveness contract is defined.

---

# Entry state

Expected:

- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor: 309/309.
- build: 0 warnings / 0 errors.
- Python predecessors:
  - WP05 3/3
  - WP06 6/6
  - WP07 semantic 2/2
  - WP07 presentation 2/2
- no repository/GitHub mutation from the blocked WP08 implementation pass.

Verify read-only.

---

# Binding predecessor contracts

Read and preserve:

1. WP02 replay cancellation semantics.
2. WP03 Worker Historical/Replay configuration and dispatch.
3. WP04/WP05/WP06/WP07 accepted production surfaces.
4. WP05 independent Worker/Streamlit process ownership.
5. WP08 lifecycle/bounded-demonstration/process-residue definition.
6. Release 1.9 manifest/path ownership.
7. #233 exact acceptance criteria.

Do not redefine replay cancellation behavior.

Do not make Streamlit responsible for Worker lifecycle.

---

# Objective

Define one minimal Worker-owned production lifecycle contract that provides:

1. a real process-lifetime cancellation signal;
2. a token passed from Worker entry composition into `SimulatedLiveVisualizationExecution.Execute`;
3. deterministic Worker response to graceful cancellation;
4. bounded harness observability;
5. no new production IPC;
6. no network listener;
7. no Worker↔Streamlit supervision;
8. no semantic change to Replay cancellation itself.

Also amend manifest/path authority narrowly so a later Terra implementation may wire and test this exact mechanism.

---

# Phase 0 — Read-only repository inspection

Inspect:

- Worker `Program.cs`;
- `SimulatedLiveVisualizationExecution.Execute`;
- Worker host model;
- current target framework/runtime;
- current console/process lifetime conventions;
- existing cancellation-token usage;
- existing tests for Worker process execution;
- existing .NET mechanisms already available without new packages.

Determine whether Worker is:

- a generic host;
- a console app with top-level statements;
- another model.

Use actual repository structure to select the lifecycle source.

No mutation.

---

# Phase 1 — Candidate cancellation sources

Evaluate only standard .NET process-lifetime mechanisms already available.

Preferred candidates may include, depending on current Worker architecture:

## Model A — `Console.CancelKeyPress`

Worker owns a `CancellationTokenSource`.

`Console.CancelKeyPress`:
- sets `e.Cancel = true`;
- requests cancellation on the CTS;
- Worker passes CTS.Token into execution;
- process remains alive long enough for graceful unwind.

## Model B — `PosixSignalRegistration`

Where platform/runtime support is appropriate:
- register SIGINT/SIGTERM;
- request cancellation;
- allow graceful unwind.

## Model C — Generic Host `IHostApplicationLifetime`

Only if Worker already uses a Generic Host.

Do not introduce Generic Host solely to gain cancellation unless repository evidence proves that is the narrowest compatible option.

## Model D — combined console + OS signal adapter

Only if one mechanism is insufficient cross-platform and combination remains minimal.

Do not select a new custom pipe/socket/file-control protocol.

---

# Phase 2 — Selection criteria

Choose exactly one canonical production lifecycle model.

It must:

- work with current target/runtime;
- require no new package;
- be testable from the WP08 harness;
- not add network IPC;
- not make Streamlit the controller;
- reuse existing cancellation token flow;
- preserve finite natural completion;
- allow force-kill only as harness fallback.

Document why rejected candidates are broader or less compatible.

---

# Phase 3 — Canonical Worker liveness contract

Define what “live Worker” means for WP08.

At minimum, after process launch and before cancellation:

- process is still running;
- Worker has entered execution;
- cancellation token is not yet requested;
- real execution has not already naturally completed.

Because current Replay fixture is very short, determine whether a **test/demo-only liveness extension** is needed.

Hard rule:
- do not change production Replay semantics merely to keep Worker alive.

If the real Worker cannot remain alive long enough to receive graceful cancellation without modifying production semantics, define the smallest **WP08-only harness-controlled execution condition** that preserves production logic, such as selecting an existing mode/configuration that blocks legitimately, or injecting a test-only controlled source through already-authorized test composition.

If no such condition exists without changing production behavior, STOP and report that the cancellation contract also requires a separate bounded-liveness test seam.

Do not invent an artificial sleep in production.

---

# Phase 4 — Cancellation request semantics

Define exact graceful request behavior.

For the selected lifecycle mechanism, specify:

- external process signal/event used by the harness;
- Worker-side handler;
- whether the signal is consumed (`e.Cancel = true` or equivalent);
- exact CTS ownership;
- exact cancellation token propagated to execution;
- whether repeat signals are idempotent;
- behavior if signal arrives before execution starts;
- behavior if signal arrives after natural completion.

No ambiguous behavior.

---

# Phase 5 — Worker exit semantics

Define exact expected result after graceful cancellation.

Specify:

- whether cancellation is treated as successful bounded shutdown;
- expected process exit code;
- whether `OperationCanceledException` is swallowed/translated;
- whether cancellation should produce normal error output;
- whether canonical handoff file may remain;
- whether cleanup runs.

Prefer an explicit non-error exit for intentional graceful cancellation if consistent with existing conventions.

Do not equate cancellation with pipeline failure.

---

# Phase 6 — Harness fallback semantics

The WP08 harness remains external and test-only.

Define:

1. request graceful cancellation;
2. wait exact graceful timeout from the already accepted WP08 definition;
3. if Worker does not exit, harness may use the exact forced-termination fallback fixed by the WP08 definition;
4. forced termination is a failed graceful-cancellation assertion even if cleanup later succeeds, unless #233 explicitly accepts fallback success.

Clarify whether fallback is:
- cleanup-only after failed acceptance; or
- an accepted secondary path.

Prefer cleanup-only unless accepted WP08 definition says otherwise.

---

# Phase 7 — Signal portability

Define supported platforms for the acceptance harness based on repository environment/governance.

If Windows is canonical for current local acceptance:
- define how harness sends Ctrl+C/CTRL_BREAK or equivalent supported signal to the owned process.

If cross-platform acceptance is required:
- define platform-specific signal adapters using standard library/runtime only.

Do not broaden to a custom production protocol merely for portability.

---

# Phase 8 — Race handling

Define deterministic races:

## Cancellation before execution starts
Worker should preserve request and pass an already-canceled token.

## Cancellation during execution
Execution observes token according to existing WP02 semantics.

## Natural completion before cancellation
Harness must not claim graceful cancellation proof; test should be inconclusive/fail and use the liveness contract to avoid this race.

## Repeated cancellation
No duplicate side effects; cancellation remains idempotent.

## Cancellation during handoff publication
Preserve atomic-file guarantees and existing cancellation boundaries. Do not interrupt file writes unless existing code already safely observes cancellation there.

---

# Phase 9 — No-liveness-redesign rule

Do not authorize:

- infinite Worker loops;
- arbitrary production delays;
- polling daemons;
- new keepalive endpoints;
- HTTP listeners;
- named pipes;
- sockets;
- control files;
- new background services;
- Streamlit-originated shutdown.

The lifecycle addition must remain a thin adapter from process lifetime → existing cancellation token.

---

# Phase 10 — Exact production path amendment

Inspect actual repository paths and authorize the minimum exact shared path/symbol exceptions.

Expected likely path:

`src/AIQuantTradingResearch.Worker/Program.cs`

Authorize only:

- construction/ownership of the lifecycle `CancellationTokenSource`;
- registration of the selected standard process cancellation mechanism;
- propagation of the token into the existing execution call;
- deterministic intentional-cancellation exit handling;
- disposal/unregistration.

If a dedicated Worker lifecycle helper is architecturally necessary, authorize **one exact new file only** after proving `Program.cs` would otherwise contain non-composition logic.

Prefer no new production helper.

Do not grant broad Worker directory ownership.

---

# Phase 11 — Execution signature path

If `SimulatedLiveVisualizationExecution.Execute` already accepts a token and no change is required there, state explicitly:

`NO CHANGE AUTHORIZED`

If a narrow call-site/overload compatibility change is required, name the exact path/symbol and why.

Do not redesign its API.

---

# Phase 12 — Focused test path amendment

The accepted WP08 definition already names:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Verify it.

Authorize this path to test:

- real Worker process launch;
- liveness condition;
- graceful cancellation signal;
- expected exit;
- forced cleanup fallback;
- cancellation token behavior as observable through process outcome;
- no process residue.

If the cancellation adapter itself requires a small unit test not feasible in the demonstration file, authorize one additional exact test path only if strictly necessary.

Do not use WP09 paths.

---

# Phase 13 — Testability seam decision

Determine whether process-level graceful cancellation can be proven solely from `WP08LifecycleDemonstrationTests.cs`.

If yes:
- no additional seam.

If no, define one narrow internal/public symbol that can be tested without changing production semantics.

Examples:
- a lifecycle registration helper returning a disposable token owner;
- a small `WorkerLifetimeCancellation` component.

Only select this if process-level proof alone cannot deterministically validate the adapter.

No generalized lifecycle framework.

---

# Phase 14 — Updated WP08 demonstration integration

Amend the existing WP08 definition by specifying how its cancellation phase now operates.

The later Terra authority must:

1. launch Worker under the exact liveness condition;
2. prove Worker-ready/liveness;
3. send the selected graceful cancellation request;
4. observe graceful exit within the pre-defined timeout;
5. verify expected exit code;
6. verify process residue zero;
7. continue restart/residue checks as already fixed.

Do not change other WP08 timing values unless strictly required by this new cancellation mechanism. If a timing value must change, explicitly justify and define the exact new value here.

---

# Phase 15 — Restart compatibility

Define how graceful cancellation interacts with restart proof.

After session A graceful cancellation:

- apply the existing WP08 canonical handoff residue rule;
- launch session B at same isolated runtime path;
- preserve prior-session cleanup semantics;
- prove B startup/publication as already fixed.

No new restart semantics.

---

# Phase 16 — Error/failure behavior

Define clear categories:

## Intentional cancellation
Not a pipeline failure.

## Unexpected lifecycle adapter failure
Worker exits non-zero / acceptance failure according to existing conventions.

## Forced harness termination
Cleanup fallback; does not prove graceful cancellation.

## Natural finite completion
Valid Worker completion generally, but insufficient for the specific WP08 graceful-cancellation acceptance test.

These distinctions must be testable.

---

# Phase 17 — Manifest ownership matrix

Produce exact table:

- path;
- existing owner;
- WP08 lifecycle exception;
- exact symbols;
- exact allowed concern;
- forbidden adjacent concerns.

At minimum cover:

- Worker `Program.cs`;
- WP08 lifecycle test path;
- optional helper only if selected.

No wildcard grants.

---

# Phase 18 — Required future tests

Later implementation must prove:

- lifecycle CTS owned by Worker;
- selected process signal requests cancellation;
- token reaches existing execution;
- intentional cancellation exits according to contract;
- repeated cancellation is safe;
- natural completion is not misreported as graceful cancellation;
- forced termination is fallback only;
- no Worker process residue;
- no Streamlit lifecycle coupling;
- no new listener/IPC;
- WP02 cancellation semantics unchanged;
- full WP08 finite demonstration now passes;
- predecessor regressions remain green.

---

# Phase 19 — Future validation gates

Later Terra implementation must run:

## WP08
- focused lifecycle demonstration tests;
- finite demonstration.

## Python predecessors
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/Streamlit/pip.

## .NET
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full regression from 309/309 plus authorized test delta.

No new package.

---

# Phase 20 — WP08/WP09 boundary

This cancellation adapter belongs to WP08 because it is required for #233 lifecycle acceptance.

WP09 remains owner of permanent integration/architecture coverage.

Do not place cancellation adapter tests in WP09.

Do not start WP09.

---

# Non-goals

Do not define/authorize:

- production supervisor;
- Streamlit → Worker shutdown;
- Worker → Streamlit shutdown;
- HTTP;
- WebSocket;
- socket listener;
- named pipe;
- control file;
- background keepalive;
- infinite loop;
- schema/persistence changes;
- Replay redesign;
- new cancellation semantics inside replay source;
- package additions;
- WP09.

---

# Documentation mutation

If governance permits, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_GRACEFUL_CANCELLATION_LIVENESS_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

No production/test/GitHub mutation.

Otherwise return the normative definition in chat.

---

# Required completion report

## Selected lifecycle mechanism
Exact .NET mechanism and why selected.

## Worker liveness
Exact liveness/readiness condition used for cancellation proof.

## Cancellation contract
Signal → handler → CTS → token → execution.

## Exit semantics
Exact exit code/error/cancellation handling.

## Harness fallback
Exact bounded behavior and acceptance meaning.

## Race semantics
Before/during/after/repeated cancellation.

## Manifest amendment
Exact production/test/helper paths and symbols.

## WP08 demonstration amendment
Exact updated cancellation phase.

## Required future tests
Exact acceptance matrix.

## Mutation statement
If doc created:

`WP08 WORKER GRACEFUL-CANCELLATION/LIVENESS DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP08 WORKER GRACEFUL-CANCELLATION/LIVENESS DEFINITION MUTATIONS: ZERO`

## Next step
On success:

`WP08 WORKER GRACEFUL-CANCELLATION/LIVENESS CONTRACT AND PATH AUTHORITY DEFINED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

---

# Stop conditions

Stop if:

- no standard process-lifetime mechanism can be used without a broader runtime redesign;
- Worker cannot be kept legitimately live for the cancellation proof without a new production semantic;
- process signal delivery is impossible in the governed environment without custom IPC;
- required path ownership collides irreconcilably with another WP;
- a new package is required.

When blocked, report the minimum next architectural decision.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 WORKER GRACEFUL-CANCELLATION/LIVENESS CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP08 WORKER GRACEFUL-CANCELLATION/LIVENESS CONTRACT AND MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Do not emit COMPLETE unless the Worker has one precise production-owned process-lifetime cancellation mechanism, a deterministic liveness proof, exact exit/fallback semantics, and exact path/test ownership.

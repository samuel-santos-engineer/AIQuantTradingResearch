# Release 1.9 — WP08 Lifecycle, Bounded-Demonstration, Process/Residue Contract + Manifest/Path-Authority Definition

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only authority** for Release 1.9 WP08, canonical issue **#233**.

It is explicitly authorized to select and define the minimum new WP08 lifecycle/demonstration/residue semantics required to make #233 implementable and objectively acceptable.

It may also amend Release 1.9 manifest/path authority narrowly for WP08.

It does **not** implement production code/tests, execute the final demonstration, close #233, mutate GitHub, or start WP09.

---

# Entry state

Binding lifecycle state:

- #232: Closed / Done.
- #233: Open / Backlog.
- milestone #58: Open.
- WP09 and later work remain unstarted.
- immediately preceding WP08 execution attempt made zero repository/GitHub mutations.

Accepted predecessor validation reference:

- .NET: **309/309**;
- build: 0 warnings / 0 errors;
- WP05 Python: 3/3;
- WP06 Python: 6/6;
- WP07 semantic exposure: 2/2;
- WP07 presentation: 2/2;
- Streamlit: 1.61.1;
- `pip check`: clean.

Verify repository reality read-only.

---

# Binding predecessor contracts

Read all accepted Release 1.9 artifacts relevant to:

- release manifest/work-package ownership;
- WP05 atomic JSON handoff;
- WP05 runtime location/lifecycle;
- WP05 refresh cadence/retry;
- WP05 manifest/path amendment;
- WP06 frame/path contract;
- WP07 presentation/exposure contracts;
- Worker composition and existing cancellation/restart behavior;
- #233 exact body/acceptance criteria.

Preserve fixed WP05 production rules unless #233 requires a narrowly defined **test/demo orchestration layer**:

- Worker and Streamlit are independently launched;
- neither production process starts/stops the other;
- Worker owns canonical handoff writes and startup cleanup;
- Streamlit is read-only;
- graceful Worker shutdown may leave the last valid canonical envelope;
- next Worker startup removes prior canonical handoff;
- abrupt termination may leave stale canonical/temp artifacts subject to the accepted cleanup rules;
- Streamlit owns bounded refresh.

Do not redefine these production responsibilities.

---

# Objective

Resolve every material contract choice that blocked WP08:

1. exact bounded demonstration duration;
2. readiness conditions;
3. success-observation conditions;
4. Worker/Streamlit launch ownership during the WP08 harness;
5. shutdown/cancellation ownership;
6. allowed termination methods;
7. shutdown ordering;
8. restart proof;
9. cancellation proof;
10. bounded-refresh proof;
11. process/listener ownership proof;
12. canonical handoff final-state rule;
13. temporary handoff residue rule;
14. temporary database residue rule;
15. log/evidence residue rule if needed;
16. focused WP08 acceptance/evidence ownership;
17. exact production/shared symbols, test paths, and evidence paths authorized for later implementation.

No material choice above may remain open on COMPLETE.

---

# Phase 0 — Read-only discovery

Inspect:

- #233 exact acceptance text;
- accepted manifest;
- Worker entry point and cancellation handling;
- Worker test helpers;
- Streamlit entry point;
- WP05 handoff publisher/path resolver;
- current temporary database behavior in tests/runtime;
- existing process-launch helpers;
- current listener/socket usage, if any;
- existing Release 1.9 evidence conventions;
- WP09 reserved test/architecture paths.

Determine which #233 requirements are already production behavior and which require only a WP08 harness/test.

No mutation yet.

---

# Phase 1 — Production-vs-harness boundary

Define a strict boundary:

## Production behavior
Existing Worker/Streamlit ownership and lifecycle semantics remain unchanged unless #233 proves an actual missing production behavior.

## WP08 harness
A test/demo orchestrator may launch, observe, cancel, restart, and terminate independent production processes solely for finite acceptance proof.

The harness is not a production supervisor.

It must not introduce runtime coupling.

It must not become a reusable production daemon/service.

Fix this distinction normatively.

---

# Phase 2 — Demonstration clock contract

Select exact deterministic bounds.

Define:

- maximum total wall-clock duration for one WP08 local demonstration;
- per-process startup timeout;
- readiness timeout;
- observation timeout;
- graceful shutdown timeout;
- forced-termination timeout/fallback;
- restart timeout;
- any polling interval used only by the harness.

Choose conservative finite values compatible with:

- Streamlit refresh default = 2 seconds;
- existing Worker startup;
- CI/local reliability;
- no unnecessarily long acceptance run.

All values must be exact.

No unbounded wait.

No sleep-only success criterion.

---

# Phase 3 — Worker readiness

Define an exact observable Worker-ready condition.

Prefer existing factual evidence such as:

- process remains alive;
- canonical handoff file exists;
- valid `aiq-visualization-read-model-v1` envelope parses;
- expected source mode/revision exists.

Do not add a network health endpoint.

Specify which condition(s) are necessary and sufficient.

---

# Phase 4 — Streamlit readiness

Define exact Streamlit-ready proof without changing production architecture.

Use an existing Streamlit listener/process signal if reliable.

Fix:

- listener ownership;
- loopback binding expectations;
- port allocation strategy for tests/demonstration;
- readiness observation;
- prohibition on fixed shared ports if parallelism could conflict.

If a listener is used, it must belong to Streamlit, not Worker.

Do not add a Worker listener.

Do not require browser automation unless #233 explicitly demands it.

---

# Phase 5 — Success-observation condition

Define exact finite success for the demonstration.

The observation must prove real:

`Worker → canonical atomic JSON → WP05 parser/refresh → WP06 frame → WP07 presentation`

to the extent #233 requires.

Choose an objective signal that can be captured without inventing a new transport.

If direct browser inspection is unnecessary, define a deterministic harness/test observation using existing parser/projection symbols plus a live Worker-produced handoff.

If Streamlit must be live concurrently, require its readiness separately.

Do not fabricate a handoff fixture for the end-to-end demonstration.

---

# Phase 6 — Bounded refresh proof

Define exactly how WP08 proves bounded refresh.

Use the accepted WP05 cadence contract:

- default 2 seconds;
- configured range 1–60 seconds;
- manual refresh does not alter cadence;
- at most two reads/cycle;
- fixed retry behavior.

WP08 must prove runtime observation within a finite expected window without redefining cadence.

Specify:

- number of required sequential observations;
- whether one newly published revision must be observed after initial readiness;
- maximum allowed observation window.

Do not require sub-second timing precision.

---

# Phase 7 — Cancellation contract

Define exact cancellation proof for Worker.

Determine the existing supported cancellation mechanism from production code.

Fix:

- how the harness requests graceful cancellation;
- what constitutes successful cancellation;
- timeout;
- fallback if graceful cancellation fails;
- expected exit code/process state;
- file-residue expectations after graceful cancellation.

Do not invent a new production shutdown protocol if existing cancellation already suffices.

---

# Phase 8 — Restart contract

Define exact restart proof.

At minimum resolve:

1. run Worker session A;
2. obtain valid publication;
3. terminate/cancel A using governed method;
4. observe allowed post-A handoff state;
5. launch Worker session B against the same canonical runtime location;
6. prove prior canonical handoff cleanup occurs before new-session publication;
7. prove B publishes a valid new-session envelope;
8. for Historical, revision resets according to accepted WP04 semantics where applicable;
9. no cross-session revision comparison is attempted.

Fix whether Replay restart must also be demonstrated or whether one mode is sufficient according to #233.

---

# Phase 9 — Streamlit shutdown contract

Define how the WP08 harness terminates Streamlit.

Since production Streamlit does not receive Worker shutdown signaling:

- harness owns demo-process termination;
- first request the least-forceful supported termination available to the launched process;
- wait exact graceful timeout;
- then force-kill only as bounded fallback.

Define acceptable exit observation.

Do not add production mutual shutdown signaling.

---

# Phase 10 — Shutdown ordering

Select one exact demonstration shutdown order.

Base it on independence and residue safety.

Define ordering for:

- Worker cancellation;
- final handoff observation if required;
- Streamlit termination;
- harness cleanup.

If different ordering is required for restart subtest, define it explicitly.

Do not leave ordering implementation-defined.

---

# Phase 11 — Process/listener residue

Define completion criteria.

At end of each focused test/demonstration:

- no Worker process launched by harness remains;
- no Streamlit process launched by harness remains;
- no child process owned by them remains where ownership can be determined;
- no Streamlit listener/port launched by harness remains bound;
- no Worker listener exists unless already part of accepted production behavior.

Do not kill unrelated processes by name globally.

Harness cleanup must track process identities/PIDs it owns.

Define exact port/listener ownership verification.

---

# Phase 12 — Canonical handoff residue

Define exact allowed final canonical-file state.

Reconcile with WP05:

- graceful Worker shutdown may leave last valid envelope;
- next Worker startup removes prior canonical file.

Choose what WP08 demonstration should assert after final shutdown.

Options must be resolved explicitly, e.g.:
- last valid canonical file is allowed after final graceful shutdown and is not considered residue; or
- harness-owned isolated runtime directory is removed after all processes stop.

Distinguish production semantics from test-harness cleanup.

Do not make Streamlit delete the canonical file.

---

# Phase 13 — Temporary handoff residue

Define exact rule for:

`.visualization-read-model.json.<owned-random-suffix>.tmp`

At final acceptance:

- zero harness/Worker-owned temp sibling files should remain unless #233/accepted WP05 explicitly allows a crash artifact during an intermediate crash test;
- any intermediate abrupt-termination artifact must be cleaned by the next Worker startup if that is the behavior under test;
- final state must be exact.

Define bounded cleanup behavior.

---

# Phase 14 — Temporary database residue

Inspect current Worker/test database conventions.

Define:

- whether WP08 uses an isolated temporary database;
- who creates it;
- whether it is copied/seeded or created through existing test/runtime mechanisms;
- who owns cleanup;
- whether WAL/SHM/journal sidecars count as residue;
- final required state.

Do not alter persistence schema.

Do not delete a user/developer database.

Only harness-owned isolated temporary database artifacts may be cleaned.

---

# Phase 15 — Evidence/log residue

If #233 requires captured demonstration evidence, define exact artifact ownership.

Prefer in-test assertions/output over persistent logs.

If persistent evidence is required:

- authorize one exact path;
- define format;
- define whether it is committed or ephemeral.

Do not create an unbounded log directory.

If no persistent artifact is necessary, explicitly define evidence as test/command output only.

---

# Phase 16 — Manifest/path authority

Amend path authority minimally.

Identify exact paths/symbols for later WP08 implementation.

Potential categories:

## Production/shared paths
Authorize only if actual production behavior must change.

If no production change is required, state that explicitly.

## WP08 harness/test path
Authorize one dedicated focused WP08 path (or exact existing manifest-reserved path).

Do not reuse WP05/WP06/WP07-exclusive test files.

Do not consume WP09 permanent integration/architecture paths.

## Optional helper
Authorize a helper path only if necessary and exact.

Prefer one focused test/harness file.

No directory-wide wildcard authority.

---

# Phase 17 — WP08 acceptance ownership

Define exactly which evidence belongs to WP08 versus WP09.

WP08 owns finite lifecycle demonstration evidence for #233.

WP09 retains permanent integration/architecture regression ownership as already planned.

WP08 may use focused executable tests for its own acceptance without converting them into WP09 architecture tests.

Specify exact boundary.

---

# Phase 18 — Required future WP08 tests

Define exact later tests/evidence.

At minimum, if required by #233:

- Worker ready from real handoff;
- Streamlit ready on harness-owned loopback listener;
- live Worker handoff consumed through existing Python parser/projection/presentation;
- bounded refresh observes a newer publication;
- graceful Worker cancellation;
- bounded forced fallback path is safe/testable if required;
- Worker restart;
- prior-session canonical cleanup;
- new-session publication;
- revision reset/no cross-session comparison;
- Streamlit independent shutdown;
- no harness-owned process residue;
- no listener residue;
- zero temp handoff residue final state;
- zero temp database/sidecar residue final state;
- canonical handoff final state matches contract;
- predecessor WP05/WP06/WP07 semantics unchanged.

Do not require a test not supported by #233.

---

# Phase 19 — Future implementation validation gate

The later consolidated WP08 authority must run:

## WP08
- all dedicated focused lifecycle/demonstration tests;
- finite local demonstration under the exact clock contract;
- final residue audit.

## Python predecessors
- WP05 3/3;
- WP06 6/6;
- WP07 semantic exposure 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

## .NET
- build 0 warnings / 0 errors;
- Application;
- Infrastructure;
- Domain;
- Architecture;
- full regression from predecessor **309/309**, plus only any explicitly authorized new .NET WP08 tests.

Fix whether WP08 tests are Python, .NET, or both based on actual path ownership discovered.

---

# Phase 20 — GitHub/lifecycle boundary

This definition authority makes zero GitHub mutations.

Later implementation may close #233 / set Done only after:

- all #233 criteria pass;
- demonstration is finite and successful;
- residue is clean according to this contract;
- predecessor regression passes;
- scope audit passes.

WP09 remains unstarted during this definition.

---

# Non-goals

Do not define or authorize:

- production process supervisor;
- Worker starting/stopping Streamlit;
- Streamlit starting/stopping Worker;
- HTTP API;
- Worker listener;
- WebSocket;
- queue;
- shared memory;
- schema change;
- persistence redesign;
- new database contract;
- adaptive refresh;
- new semantic status;
- chart/presentation redesign;
- package additions;
- WP09 permanent integration/architecture tests;
- release/milestone closure.

---

# Documentation mutation

If consistent with accepted Release 1.9 documentation governance, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`

No other repository mutation.

If this documentation path itself is not governed, return the normative definition in chat and make zero mutations.

---

# Required completion report

## Binding evidence
#233, predecessor contracts, baseline.

## Lifecycle contract
Exact launch ownership, readiness, success observation, cancellation, restart, Streamlit shutdown, shutdown order.

## Clock contract
Every exact timeout/duration/polling bound.

## Refresh contract
Exact sequential observation proof.

## Residue contract
Process, listener, canonical handoff, temp handoff, temp database/sidecars, evidence/logs.

## Production-vs-harness boundary
Explicitly state whether production changes are required.

## Manifest/path amendment
Exact WP08 production/test/helper/evidence paths and symbols.

## WP08/WP09 boundary
Exact ownership split.

## Future acceptance
Exact tests/demonstration/regression gates.

## Mutation statement
If one authorized doc is created:

`WP08 LIFECYCLE/DEMONSTRATION/RESIDUE DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP08 LIFECYCLE/DEMONSTRATION/RESIDUE DEFINITION MUTATIONS: ZERO`

## Next step
On success:

`WP08 LIFECYCLE/DEMONSTRATION/RESIDUE CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH CONSOLIDATED AUTHORITY`

---

# Stop conditions

Stop if:

- #233 requires a semantic choice outside lifecycle/demonstration/residue scope;
- a required production redesign conflicts with WP05;
- a required path belongs exclusively to WP09 and cannot be narrowly separated;
- a material requirement cannot be fixed without changing schema/persistence/transport/presentation semantics;
- documentation governance forbids the amendment path (return definition in chat instead).

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 LIFECYCLE, BOUNDED-DEMONSTRATION, PROCESS/RESIDUE CONTRACT AND MANIFEST/PATH-AUTHORITY DEFINITION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 LIFECYCLE, BOUNDED-DEMONSTRATION, PROCESS/RESIDUE CONTRACT AND MANIFEST/PATH-AUTHORITY DEFINITION BLOCKED`

Do not emit COMPLETE while any material timing, readiness, shutdown, restart, residue, evidence, or path choice remains unresolved.

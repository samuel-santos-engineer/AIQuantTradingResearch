# Release 1.9 WP08 — Lifecycle, Bounded Demonstration, Process and Residue Contract

Status: normative definition/path authority. Implementation requires a fresh consolidated WP08 authority.

## Scope and production boundary

WP08 owns finite acceptance evidence for #233: restart, cancellation, bounded refresh, process/listener ownership, temporary handoff/database residue, and a finite local demonstration. Production Worker and Streamlit remain independently launched peers. Neither production process starts, stops, supervises, or signals the other. The WP08 harness is test/demo-only orchestration and is not a production supervisor.

## Exact demonstration clock

One demonstration attempt has a maximum wall-clock duration of 30 seconds. The harness uses these hard deadlines:

| Activity | Bound |
|---|---:|
| Worker process startup | 10 seconds |
| Streamlit process startup | 10 seconds |
| readiness after process start | 10 seconds |
| first real handoff observation | 10 seconds |
| bounded-refresh/new-revision observation | 8 seconds |
| graceful Worker cancellation wait | 5 seconds |
| graceful Streamlit termination wait | 5 seconds |
| forced termination wait after kill request | 2 seconds |
| restart-session publication | 10 seconds |
| harness polling interval | 250 milliseconds |

The 30-second total deadline supersedes any individual bound when reached. No sleep-only success criterion or unbounded wait is permitted.

## Readiness and success

Worker readiness is sufficient only when the harness-owned Worker PID remains alive and the canonical handoff path contains a complete parseable `aiq-visualization-read-model-v1` envelope with the expected accepted source mode and revision. A process merely remaining alive is insufficient.

Streamlit readiness is sufficient when the harness-owned Streamlit PID remains alive and its own loopback listener is accepting a connection on a harness-selected ephemeral port. The harness never uses a fixed shared port and never adds a Worker listener. No browser automation is required.

Demonstration success requires a real Worker-produced handoff, direct consumption through the existing WP05 parser and WP06 frame projection, and a WP07 presentation-section projection containing the same canonical revision/status facts. The harness may inspect parser/frame/projection values directly; it may not substitute a fabricated handoff fixture for this end-to-end proof.

## Bounded refresh proof

After the first valid handoff, the harness must observe one subsequent newer accepted Worker publication through the existing Streamlit refresh path within 8 seconds. The proof uses the fixed WP05 cadence: default 2 seconds, configured range 1–60 seconds, at most two reads per cycle, and the existing 50 ms retry. WP08 changes none of these rules. Equivalent revisions do not count as a newer observation.

## Cancellation and restart

The harness requests the Worker’s existing supported graceful cancellation mechanism. Cancellation succeeds only when the harness-owned Worker PID exits within 5 seconds and no owned child remains. If graceful cancellation does not complete, the harness may terminate only that tracked PID/process tree using a bounded 2-second fallback; it must report the fallback and still require zero owned process residue.

Restart proof is: session A publishes a valid envelope; the harness cancels/terminates A; the harness verifies the allowed post-A file state; session B starts against the same isolated canonical runtime path; Worker startup removes the prior canonical file before B publishes; B publishes a fresh valid envelope within 10 seconds; and no cross-session revision comparison is attempted. Historical revision reset to the accepted WP04 session-local sequence is verified where applicable. Replay restart uses the existing logical-tick semantics and is not redefined.

Streamlit shutdown is harness-owned. After the final required observation, the harness requests the least-forceful supported termination of its tracked Streamlit process, waits 5 seconds, then uses only the bounded 2-second fallback if needed. Shutdown order is fixed: capture final observation, request Worker cancellation, await Worker exit/cleanup, terminate Streamlit, await Streamlit exit, verify listener/process residue, then remove only harness-owned temporary resources.

## Process and listener ownership

The harness records every PID it starts and may inspect/terminate only those PIDs and their owned children. It must not kill processes by image name or broad command-line match. Completion requires zero tracked Worker/Streamlit processes, zero owned children, and no harness-selected loopback listener still bound. No Worker listener is introduced.

## File and database residue

Each demonstration uses an isolated harness-owned runtime directory and temporary database location resolved by existing configuration mechanisms. The Worker owns the canonical handoff file, parent directory creation, atomic replacement, and its temporary siblings. Streamlit never creates, deletes, or writes the canonical handoff.

After a graceful final Worker shutdown, the last valid canonical envelope may remain as an allowed production artifact. Before the next Worker session, startup cleanup must remove that prior canonical file. At final harness cleanup, the harness removes only its isolated runtime directory, including the canonical file and any `.visualization-read-model.json.<owned-suffix>.tmp` siblings, and requires zero such files afterward. An abrupt-stop subtest may observe an intermediate stale/temp artifact, but the next Worker startup must clean it and final state must still be zero.

The database is harness-owned and isolated; it is created through existing runtime/test configuration, never copied from or used as a developer database. Cleanup occurs only after all owned processes exit. SQLite WAL, SHM, journal, and temporary sidecars count as residue and must be absent when cleanup completes. No schema or persistence behavior changes.

Evidence is command/test output only. No persistent log or evidence path is authorized or required.

## Manifest/path authority

No production path requires a WP08 change. The following exact focused test path is newly authorized for WP08 lifecycle evidence:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

It may contain only harness-owned process launch/observation/cancellation/restart, real handoff/parser/frame proof, listener checks, and temporary resource cleanup. It must use existing production entry points and configuration; it may not become a production supervisor.

The existing shared `SimulatedLiveVisualizationExecution.cs` and `SimulatedLiveVisualizationConfiguration.cs` remain production-owned predecessor paths and may be consumed but not redesigned here. The existing `SimulatedLiveVisualizationExecutionTests.cs` remains shared WP03/WP08/WP09 ownership; this amendment does not broaden it. No WP05, WP06, WP07, or WP09-exclusive test path is consumed.

## WP08 / WP09 ownership boundary

WP08 owns finite, executable lifecycle/demonstration and residue evidence for #233. WP09 retains permanent replay→pipeline→read-model→Streamlit integration and architecture regression ownership. WP08 must not add architecture rules, permanent cross-layer suites, browser tests, or broad integration infrastructure.

## Required future tests and acceptance

The later WP08 implementation must prove real Worker readiness, real Streamlit listener readiness, Worker→atomic JSON→WP05 parser→WP06 frame→WP07 projection, one newer publication within the fixed refresh bound, graceful cancellation, bounded forced fallback, restart and prior-session cleanup, Historical session-local revision reset, independent Streamlit shutdown, zero tracked process/listener residue, zero final canonical/temp handoff residue, zero temporary database/sidecar residue, and preservation of WP05–WP07 semantics. It must also run WP05 3/3, WP06 6/6, WP07 semantic 2/2, WP07 presentation 2/2, compilation, Streamlit 1.61.1, `pip check`, Application/Infrastructure/Domain/Architecture suites, build 0/0, and full .NET regression from 309/309 plus only explicitly authorized WP08 tests.

## Explicit exclusions

No production supervisor, process coupling, Worker listener, HTTP/WebSocket/queue/shared-memory transport, schema/persistence/provider change, adaptive refresh, new semantic status, chart redesign, package change, WP07 redesign, WP09 permanent tests, release closure, or milestone closure is authorized.

`WP08 LIFECYCLE/DEMONSTRATION/RESIDUE DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP08 LIFECYCLE/DEMONSTRATION/RESIDUE CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH CONSOLIDATED AUTHORITY`

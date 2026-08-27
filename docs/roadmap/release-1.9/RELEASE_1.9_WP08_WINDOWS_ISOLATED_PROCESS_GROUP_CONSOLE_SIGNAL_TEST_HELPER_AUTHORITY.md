# Release 1.9 WP08 — Windows Isolated Process-Group Console-Signal Test Helper

Status: normative definition and test-path authority. Implementation requires a
fresh consolidated WP08 authority.

## Scope

This authority defines only the Windows test/demo helper required to prove the
already-selected Worker cancellation adapter at process level. It preserves the
existing partial production implementation exactly:

- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

No additional production change is authorized. The helper is test-only and does
not alter Replay, Streamlit ownership, persistence, or any production protocol.

## Selected Windows model

Use inherited-console plus a distinct process group. Launch the real Worker with
`CreateProcessW` and `CREATE_NEW_PROCESS_GROUP` (`0x00000200`). Do not use
`CREATE_NEW_CONSOLE`: the Worker may inherit the test runner's console, but its
distinct non-zero process-group ID is the only signal target. The harness sends
`CTRL_BREAK_EVENT` (`1`) to the Worker's process-group ID, never to group `0`.

This model avoids attaching the test runner to another console and permits
targeted console-control delivery without broadcasting CTRL+C/CTRL_BREAK to the
runner. The helper records the returned process ID and treats it as the process
group ID because `CREATE_NEW_PROCESS_GROUP` makes the child the group leader.

## Native API surface

The single test-only helper may P/Invoke only these standard Windows APIs:

- `CreateProcessW` with `CREATE_NEW_PROCESS_GROUP | CREATE_UNICODE_ENVIRONMENT`;
- `GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, ownedProcessGroupId)`;
- `WaitForSingleObject` for bounded waiting;
- `GetExitCodeProcess` for exit read-back; and
- `CloseHandle` through BCL safe-handle wrappers for process and thread handles.

No `AttachConsole`, `FreeConsole`, `SetConsoleCtrlHandler`, Job Object, shell
command, `taskkill`, or broad process API is required. The test runner remains in
its existing console and no global handler is installed.

## Helper contract

The exact internal WP08 helper is:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

It accepts an absolute Worker executable path, a fully specified argument list,
working directory, and the existing harness-owned environment map. It creates a
Unicode command line, launches with the flags above, and returns an internal
owned-process value containing only the safe process handle, thread handle,
process ID, process-group ID, and bounded exit/output metadata required by the
test.

The process inherits the test runner's standard handles. The helper does not
redirect unbounded stdout/stderr and therefore cannot deadlock on output pipes;
the Worker acceptance path has no required protocol on stdout. Exit-code and
bounded lifecycle results are the authoritative evidence. If bounded diagnostics
are needed, the helper may use fixed-size in-memory capture only under a separate
authority; no persistent log path is authorized.

The helper owns every native handle it receives. Disposal closes the thread and
process safe handles exactly once after bounded wait/read-back. It never exposes a
raw native handle to callers.

## Command-line and environment

The harness passes the real Worker DLL/host command and the already-authorized
`--wp08-test-liveness` argument. No new liveness environment variable or user
configuration is introduced. Existing isolated values remain the only runtime
inputs: absolute handoff path, isolated temporary database path, replay mode,
replay identity/target/ticks/count, dataset bounds, and required API-key test
placeholder.

Because `CreateProcessW` receives one command-line string, the helper must use a
small tested Windows quoting routine. For each argument, emit it unchanged when
it contains no whitespace, tab, quote, or is empty; otherwise surround it with
quotes, double every run of backslashes that precedes a quote, escape each quote
with a backslash, and double trailing backslashes before the closing quote. The
executable path is quoted using the same rule. Tests cover spaces, empty values,
quotes, and trailing backslashes. The environment block is Unicode and preserves
the supplied key/value pairs without adding a control channel.

## Signal operation

The exact operation is `RequestCtrlBreak()`:

1. verify the owned process/group identity is non-zero and the process handle is
   still valid;
2. call `GenerateConsoleCtrlEvent(1, ownedProcessGroupId)` exactly once;
3. report the native success/failure result; and
4. rely on Worker exit code/read-back to prove graceful cancellation.

No retry is required. A repeated request is safe at the harness level but is not
needed for acceptance and must never target group zero. API success alone is not
graceful-cancellation proof.

## Test-runner protection and cleanup

The helper proves protection by asserting a distinct non-zero group ID, recording
the runner PID separately, never broadcasting, and never invoking image-name
termination. It does not attach/detach consoles or change runner handlers.

The passing path waits the binding graceful Worker timeout of 5 seconds and
requires Worker exit code `0` after the real CTRL_BREAK. If graceful exit fails,
the focused test fails; only then may the helper terminate its owned process (and
only its owned descendants if later evidence proves any exist) with the binding
2-second cleanup bound. Forced cleanup is not acceptance success. All handles are
closed and the isolated runtime/database directory is cleaned only after owned
process exit.

The Worker acceptance path launches no child process, so no Job Object is
authorized. A Job Object is not needed for graceful signaling and would add
native surface without an ownership requirement. If a future implementation
proves child creation, it must stop for a separate authority rather than add one
here.

## Process-level acceptance flow

`WP08LifecycleDemonstrationTests.cs` launches the real Worker through the helper
with `--wp08-test-liveness`, waits for the real handoff and post-publication gate,
verifies the process is alive, calls `RequestCtrlBreak()`, waits up to 5 seconds,
asserts exit code `0`, and verifies no owned process residue. It then uses the
already-fixed WP08 restart, Streamlit-independence, handoff, database/sidecar,
refresh, and final-residue protocol.

The helper does not replace the production `Console.CancelKeyPress → CTS →
execution token` path; it exists only to deliver that signal safely. No forced
kill is accepted on the passing path.

## Platform scope

This helper and process-level graceful-cancellation acceptance are explicitly
Windows-only, matching the governed local environment. On non-Windows systems,
the test is platform-conditioned out by repository test governance; it is not
silently counted as passed and no custom cross-platform IPC fallback is added.

## Exact path authority

| Path | Owner | WP08 exception | Forbidden |
| --- | --- | --- | --- |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs` | WP08 | Internal Windows-native CreateProcessW/process-group/CTRL_BREAK helper, safe handles, quoting, bounded wait, and owned cleanup | Production interop, Job Objects, broad termination, shell wrappers, IPC, listeners |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs` | WP08 | Use the helper for real Worker liveness, signal, exit, restart, Streamlit independence, and residue acceptance | WP09 tests, production supervisor, Replay changes, new paths |

No production path is changed by this authority. No WP05, WP06, WP07, or WP09
test path is consumed.

## Required future tests

The implementation authority must prove distinct process-group identity,
safe launch, exact quoting/environment propagation, real handoff readiness,
targeted CTRL_BREAK delivery, test-runner protection, graceful exit code `0`,
forced-fallback classification, deterministic handle disposal, zero process and
listener residue, restart compatibility, and preservation of the existing Worker
cancellation/liveness and Replay semantics. It must run the accepted WP05–WP08,
Python, .NET, build, and scope gates without new packages.

`WP08 WINDOWS PROCESS-GROUP/SIGNAL TEST-HELPER DEFINITION MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP08 WINDOWS PROCESS-GROUP/SIGNAL TEST-HELPER AUTHORITY DEFINED — CONSOLIDATED WP08 IMPLEMENTATION MAY RESUME`

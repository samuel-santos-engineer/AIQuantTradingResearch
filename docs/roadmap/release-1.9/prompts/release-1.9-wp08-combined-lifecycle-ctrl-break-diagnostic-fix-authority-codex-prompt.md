# Release 1.9 — WP08 Combined-Lifecycle CTRL_BREAK Diagnostic + Fix Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow diagnostic-first authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to diagnose and, if root cause is proven, fix the combined-lifecycle graceful-cancellation failure where:

- standalone targeted CTRL_BREAK passes with Worker exit code `0`;
- the combined lifecycle reaches Streamlit readiness and completes the real Worker P2 → governed Python probe path;
- targeted CTRL_BREAK then causes Worker A to exit `-1073741502` (`0xC0000142`) instead of required exit `0`.

The accepted standalone cancellation path remains binding predecessor evidence and must not be broken.

No broad redesign.
No Replay changes.
No WP05/WP06/WP07 semantic changes.
No WP09.

# Accepted predecessor state

Preserve all valid WP08 work:

Production:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Tests/helpers:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Python:
- `python/presentation/wp08_presentation_chain_probe.py`

Accepted evidence:
- standalone CTRL_BREAK focused test passes;
- `CreateProcessW + CREATE_NEW_PROCESS_GROUP` works;
- P1 pass-through works;
- genuine P2 occurs within fixed 8-second bound;
- post-P2 hold works;
- standalone targeted CTRL_BREAK exits Worker with code 0;
- Python probe exists and invalid-path exit 2 is proven;
- combined lifecycle reaches Streamlit owned loopback listener;
- combined lifecycle reaches Worker P2;
- combined lifecycle runs governed Python probe before shutdown;
- combined lifecycle fails only when targeted CTRL_BREAK is sent to Worker A.

Accepted regression reference:
- focused WP08 predecessor: 4/4;
- .NET predecessor: 313/313;
- Python WP05 3/3, WP06 6/6, WP07 semantic 2/2, WP07 presentation 2/2;
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check` clean.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- no GitHub lifecycle mutations.

# Proven failure

Combined lifecycle reproducibly produces:

`Worker A exit = -1073741502 = 0xC0000142`

instead of required:

`Worker A exit = 0`

The standalone CTRL_BREAK test still passes.

Therefore do not assume the production cancellation adapter is generally broken.

# Objective

Determine the exact root cause category and then do one of:

## Outcome A — diagnostic-only resolution
If the failure is caused by test/harness/helper behavior, define and implement the exact minimal fix in the authorized test/helper surface.

## Outcome B — narrow production fix
If the failure is proven to arise from a production cancellation/lifecycle defect exposed only in combined topology, implement the minimum production fix within the exact authorized production surface.

## Outcome C — blocked
If root cause cannot be proven safely with the allowed diagnostics, stop with exact missing authority.

Do not apply speculative fixes.

# Phase 0 — Read-only code inspection

Inspect current:

- `WorkerLifecycleCancellation.cs`;
- `Program.cs`;
- `SimulatedLiveVisualizationExecution.cs`;
- `WindowsIsolatedProcessGroup.cs`;
- `WP08LifecycleDemonstrationTests.cs`;
- `wp08_presentation_chain_probe.py`;
- exact Streamlit launch logic in the combined test.

Compare standalone and combined CTRL_BREAK paths line-by-line.

Document every difference in:
- process creation flags;
- inherited handles;
- stdout/stderr redirection;
- working directory;
- environment;
- console attachment;
- process group;
- launch ordering;
- other child processes alive at signal time;
- timing;
- cleanup;
- Python probe completion;
- Streamlit listener/process state.

No mutation yet.

# Phase 1 — Decode the Windows exit condition

Treat `0xC0000142` as a Windows process-termination code that needs factual root-cause confirmation.

Do not guess from the symbolic code alone.

Use authoritative Windows/.NET semantics available in the environment.

If external documentation is needed, prefer Microsoft documentation.

Do not change code based only on the hex value.

# Phase 2 — Diagnostic visibility authority

This authority explicitly permits **bounded diagnostic visibility** needed to identify the failure.

Allowed diagnostic additions are limited to the existing WP08 test/helper and, if strictly necessary, the Worker cancellation adapter.

Diagnostics must be:
- bounded;
- safe;
- test-oriented;
- no credentials/provider data;
- no persistent tracing subsystem;
- no persistent evidence directory unless already governed.

Preferred diagnostics:
- Worker stdout/stderr;
- exit code;
- timestamps;
- PIDs;
- signal-send success/failure;
- Streamlit process/listener state;
- probe exit/stdout/stderr;
- owned handle/console state.

# Phase 3 — Helper diagnostics

If `WindowsIsolatedProcessGroup.cs` does not currently capture Worker stdout/stderr in the combined scenario, this authority permits one narrow helper enhancement **only if needed** to capture bounded diagnostics.

Allowed:
- redirect stdout/stderr safely;
- asynchronously drain both streams;
- retain bounded output;
- expose captured output after exit;
- preserve exact process-group creation and CTRL_BREAK behavior.

Forbidden:
- signal redesign;
- process-group redesign;
- Job Object;
- generic supervision;
- shell wrapper.

If capture can be added only in `WP08LifecycleDemonstrationTests.cs`, prefer that.

# Phase 4 — Standalone-vs-combined A/B/C/D matrix

Run deterministic diagnostics:

A. Worker only, standalone CTRL_BREAK.
B. Worker + Streamlit alive, no Python probe.
C. Worker + Python probe completed, no Streamlit.
D. Worker + Streamlit alive + Python probe completed.
E. Only if needed: isolate stdio/handle mode while preserving the same Worker group/signal path.

All scenarios must:
- use the same Worker `--wp08-test-liveness`;
- use the same Windows helper;
- reach P2 hold;
- send the same targeted CTRL_BREAK;
- record exit code and bounded diagnostics.

Do not broaden beyond what is needed to isolate root cause.

# Phase 5 — Process topology diagnostics

For each scenario record:
- Worker PID/group ID;
- Streamlit PID;
- probe PID if active;
- whether probe exited before signal;
- console association;
- stdio mode;
- inherited-handle state;
- listener state;
- CTRL_BREAK API result;
- Worker exit code;
- bounded Worker stdout/stderr.

Answer:
“Which additional process/state changes Worker exit from 0 to 0xC0000142?”

# Phase 6 — Console/process-group hypothesis

Test whether failure is caused by console-control propagation or shared-console interactions.

Answer:
- Is Streamlit or probe in the same console?
- Are they in the test runner's process group?
- Does targeted CTRL_BREAK affect any process other than Worker group?
- Is Worker group ID stable/non-zero?
- Does any attach/detach occur?
- Does presence of Python/Streamlit alter console-handler state?

No process-group redesign unless root cause proves it necessary.

# Phase 7 — Stdio/handle hypothesis

Test whether failure is caused by:
- redirected stdout/stderr;
- inherited pipe handles;
- parent closing handles before Worker exit;
- async-read blocking;
- console-vs-pipe mode;
- probe/Streamlit handle inheritance.

If root cause is handle-related, fix only helper/test behavior.

# Phase 8 — Streamlit-specific hypothesis

If B fails but C passes, isolate Streamlit.

Determine whether:
- Streamlit launch changes console state;
- Streamlit inherits control handling;
- its launch wrapper changes parent console;
- readiness code changes process handles;
- its output capture affects Worker.

Prefer harness-only fix.

# Phase 9 — Probe-specific hypothesis

If C fails but B passes, isolate the probe.

Determine whether:
- probe launch changes console state;
- it inherits Worker-related handles;
- its completion closes/changes handles;
- shell semantics are used;
- stdout/stderr capture affects shared pipes.

Keep probe semantics unchanged unless a concrete launch defect is proven.

# Phase 10 — Timing/race hypothesis

If results vary, capture exact timestamps:
- P2 observed;
- probe start/exit;
- Streamlit readiness;
- CTRL_BREAK sent;
- cancellation handler observed if available;
- Worker exit.

Do not fix with arbitrary sleeps.

If race is proven, use event/state-based sequencing from already-governed observables.

# Phase 11 — Worker cancellation adapter diagnostics

Only if harness/helper diagnostics are insufficient, permit a temporary bounded stderr trace in existing Worker cancellation path.

Allowed events:
- cancellation handler entered;
- CTS cancellation requested;
- execution cancellation observed;
- intentional cancellation path selected;
- process exit path selected.

No stack traces.
No semantic changes.

Remove diagnostic-only production output after root cause/fix if not needed.

# Phase 12 — Root-cause classification

Classify exactly one:

- **Class H** — Harness sequencing/cleanup defect.
- **Class P** — Process-group/console helper defect.
- **Class S** — Stdio/inherited-handle defect.
- **Class T** — Timing/race defect.
- **Class W** — Worker cancellation/exit-path defect.
- **Class U** — Unresolved.

No speculative classification.

# Phase 13 — Authorized fix surface by class

## Class H
May modify:
- `WP08LifecycleDemonstrationTests.cs` only.

## Class P
May modify:
- `WindowsIsolatedProcessGroup.cs`
- `WP08LifecycleDemonstrationTests.cs`

Must preserve isolated group, targeted CTRL_BREAK, runner safety.

## Class S
May modify:
- helper and/or lifecycle test only.

## Class T
May modify:
- lifecycle test sequencing only unless the proven race is in production state.

No sleep-only fix.

## Class W
May modify only where root cause lies:
- `WorkerLifecycleCancellation.cs`
- `Program.cs`
- `SimulatedLiveVisualizationExecution.cs`

Must preserve standalone exit 0, Replay semantics, and existing process signal contract.

## Class U
STOP. No speculative fix.

# Phase 14 — Minimal fix principle

Any fix must:
- directly match proven root cause;
- be the smallest possible;
- preserve all passing behavior;
- add no package/config/custom IPC;
- preserve Worker/Streamlit independence;
- preserve Replay;
- preserve WP05/WP06/WP07 semantics.

No unrelated refactor.

# Phase 15 — Focused post-fix proof

After fix, require:

1. original standalone CTRL_BREAK still exits 0.
2. combined Streamlit + probe + Worker CTRL_BREAK exits 0.
3. no forced kill in passing paths.
4. P2 still within 8 seconds.
5. Python probe still succeeds.
6. Streamlit readiness/listener still succeeds.
7. no diagnostic process/listener residue.
8. combined path passes repeatedly, preferably 3 consecutive runs if timing budget permits; otherwise use the maximum governed repetition and report count.

# Phase 16 — Regression after fix

Run:
- focused WP08;
- build;
- Application;
- Infrastructure;
- Domain;
- Architecture;
- full .NET from 313/313 baseline plus authorized delta;
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- Streamlit 1.61.1;
- `pip check`.

No GitHub lifecycle mutation under this authority.

# Phase 17 — Scope audit

List every changed path as:
- diagnostic-only;
- root-cause fix;
- test assertion.

Prove zero:
- Replay changes;
- WP05/WP06/WP07 semantic changes;
- probe semantic redesign;
- Release 1.8 endpoint expansion;
- custom IPC;
- package changes;
- WP09;
- GitHub lifecycle mutation.

# GitHub lifecycle boundary

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open.

GitHub mutations: zero.

# Required completion report

## Accepted predecessor
Standalone passing path, combined failure, 313/313 baseline.

## Diagnostic matrix
A/B/C/D(/E) scenarios and exit codes.

## Worker diagnostics
Bounded stdout/stderr and lifecycle signals.

## Root cause
Exact Class H/P/S/T/W/U with evidence.

## Fix
Exact path/symbol and why minimal.

## Standalone preservation
Original CTRL_BREAK exit 0.

## Combined proof
Streamlit + probe + Worker CTRL_BREAK exit 0.

## Regression
Focused WP08, .NET, Python.

## Residue
No diagnostic process/listener residue.

## Scope
Forbidden categories zero.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 COMBINED-LIFECYCLE CTRL_BREAK DIAGNOSTIC/FIX GITHUB MUTATIONS: ZERO`

## Next step

On successful fix:

`WP08 COMBINED-LIFECYCLE CTRL_BREAK FIXED — FINAL LIFECYCLE-HARNESS COMPLETION REQUIRES FRESH AUTHORITY`

# Stop conditions

Stop if:
- root cause remains Class U;
- diagnostics require a new tracing subsystem;
- fix requires custom IPC;
- fix requires Replay changes;
- fix requires WP05/WP06/WP07 semantic changes;
- fix requires package addition;
- runner safety cannot be guaranteed;
- combined exit 0 cannot be reproduced after fix;
- standalone path regresses.

Preserve valid partial WP08 work and keep #233 Open / Backlog.

# Terminal markers

Success:

`RELEASE 1.9 WP08 COMBINED-LIFECYCLE CTRL_BREAK DIAGNOSTIC AND FIX COMPLETE`

Blocked:

`RELEASE 1.9 WP08 COMBINED-LIFECYCLE CTRL_BREAK DIAGNOSTIC AND FIX BLOCKED`

Do not emit COMPLETE unless the root cause is proven, the minimal fix is implemented, standalone cancellation remains valid, and the combined Streamlit + probe + Worker targeted CTRL_BREAK path exits gracefully with code 0.

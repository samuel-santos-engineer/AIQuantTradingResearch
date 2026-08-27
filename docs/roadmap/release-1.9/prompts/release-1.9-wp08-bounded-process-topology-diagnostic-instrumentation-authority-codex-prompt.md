# Release 1.9 — WP08 Bounded Process-Topology Diagnostic Instrumentation Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow diagnostic-only authority** for Release 1.9 WP08, canonical issue **#233**.

Its sole purpose is to add the minimum bounded instrumentation required to complete the previously mandated A/B/C/D process-topology diagnostic matrix for the combined-lifecycle CTRL_BREAK failure and to classify the root cause out of `Class U`.

This authority does **not** authorize a fix.

No production semantic change.
No signal-mechanism redesign.
No Replay change.
No WP05/WP06/WP07 change.
No probe semantic change.
No package.
No GitHub lifecycle mutation.
No WP09.

---

# Accepted predecessor state

Preserve all current valid WP08 work exactly.

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
- standalone Worker + targeted CTRL_BREAK exits `0`;
- combined Streamlit + Worker + probe reproducibly exits Worker with `0xC0000142`;
- Streamlit readiness passes;
- Worker P2 handoff passes;
- governed probe passes;
- focused standalone test still passes after the combined failure;
- P2 remains within fixed 8-second bound;
- existing Windows helper semantics remain valid;
- no forced fix applied.

Regression reference:
- focused WP08 predecessor: 4/4;
- .NET predecessor: 313/313;
- Python WP05 3/3, WP06 6/6, WP07 semantic 2/2, WP07 presentation 2/2;
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check` clean.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations zero.

---

# Proven diagnostic blocker

The prior diagnostic/fix authority required a bounded A/B/C/D matrix, but the existing harness/helper does not expose enough safe diagnostics to determine:

- Worker stdout/stderr around cancellation;
- console/process-group topology;
- inherited-handle state;
- exact launch-mode differences;
- exact signal timing;
- exact process-tree state.

Therefore root cause remains unresolved `Class U`.

This authority exists only to make those observations safely available.

---

# Objective

Add bounded instrumentation sufficient to run and interpret:

A. Worker only.
B. Worker + Streamlit.
C. Worker + governed Python probe.
D. Worker + Streamlit + governed Python probe.

For each scenario capture enough evidence to classify the failure as one of:

- Class H — harness sequencing/cleanup;
- Class P — process-group/console helper;
- Class S — stdio/inherited-handle;
- Class T — timing/race;
- Class W — Worker cancellation/exit path;
- Class U — unresolved.

No fix may be implemented here.

---

# Binding diagnostic constraints

Read the prior:

`release-1.9-wp08-combined-lifecycle-ctrl-break-diagnostic-fix-authority-codex-prompt.md`

and preserve its root-cause classes, A/B/C/D scenario definitions, and stop conditions.

This authority amends only the diagnostic-visibility/path scope.

---

# Phase 0 — Read-only inventory

Inspect current:

- Worker helper launch implementation;
- lifecycle test launch orchestration;
- Streamlit process launch;
- Python probe launch;
- stdio redirection;
- handle inheritance;
- process creation flags;
- process-group IDs;
- working directories;
- environment construction;
- cleanup sequence.

Record exactly which desired diagnostics are currently unavailable.

No mutation yet.

---

# Phase 1 — Authorized diagnostic paths

This authority permits bounded diagnostic changes only in:

1. `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Production files are **read-only** under this authority.

`wp08_presentation_chain_probe.py` is **read-only**.

No new helper path.
No new test path.
No documentation mutation required unless repository governance explicitly demands one.

---

# Phase 2 — Worker stdout/stderr capture

Authorize the Windows process helper to capture bounded Worker stdout/stderr if not already available.

Requirements:

- preserve `CreateProcessW`;
- preserve `CREATE_NEW_PROCESS_GROUP`;
- preserve targeted `CTRL_BREAK_EVENT`;
- preserve runner safety;
- preserve all existing quoting/environment behavior;
- do not change Worker semantics.

Capture model must be exact and bounded.

Preferred:
- redirect stdout and stderr to anonymous pipes owned by the helper;
- asynchronously drain both;
- cap retained diagnostic text to a fixed maximum per stream;
- continue draining after cap to avoid child blockage;
- expose bounded text to the test after process exit.

Choose an exact maximum, e.g. 64 KiB per stream, unless repository conventions dictate another bounded value.

No unbounded buffering.

---

# Phase 3 — Native handle instrumentation

Expose only the minimum helper diagnostics needed to understand launch topology:

- Worker PID;
- process-group ID;
- creation flags;
- whether stdout redirected;
- whether stderr redirected;
- whether handles are inherited;
- exact inherited handle count if knowable from helper-controlled handles;
- whether helper-side pipe handles are open/closed;
- process handle state at signal time.

Do not expose raw handles outside the helper API unless absolutely necessary.

No general-purpose native diagnostics API.

---

# Phase 4 — Console/process-group instrumentation

Capture bounded factual metadata:

- Worker process-group ID;
- whether Worker inherits parent console under the accepted model;
- parent/test process ID;
- Streamlit PID;
- probe PID;
- whether those processes were launched with new process-group flags;
- whether any attach/detach API was invoked;
- `GenerateConsoleCtrlEvent` target group;
- API return value;
- last-error code if API fails.

Do not add `AttachConsole`, `FreeConsole`, `SetConsoleCtrlHandler`, or other native calls solely for observation unless required to read metadata and already safe.

Prefer passive observation.

---

# Phase 5 — Streamlit/probe launch diagnostics

Inside `WP08LifecycleDemonstrationTests.cs`, record for each launched process:

- PID;
- parent/owned process relation if available;
- working directory;
- executable path;
- argument list;
- stdout/stderr redirection mode;
- shell/no-shell mode;
- environment overrides count/names relevant to WP08;
- launch timestamp;
- readiness/completion timestamp;
- exit code.

Do not log full environment values if they may contain sensitive data.

Record keys only where values are unnecessary.

---

# Phase 6 — Timing instrumentation

Capture monotonic timestamps for:

- Worker process launch;
- P1 observed;
- P2 observed;
- Streamlit launch;
- Streamlit ready;
- probe launch;
- probe exit;
- CTRL_BREAK request;
- `GenerateConsoleCtrlEvent` return;
- Worker exit;
- cleanup start/end.

Use one monotonic clock source in the test.

No wall-clock dependence for sequencing conclusions.

---

# Phase 7 — Owned process-tree snapshot

At the moment immediately before CTRL_BREAK in each A/B/C/D scenario, capture:

- Worker alive;
- Streamlit alive/not present;
- probe alive/exited/not present;
- listener state;
- owned PID set;
- any known child PIDs under the harness-owned process tree if available through standard OS/BCL tooling.

Do not enumerate or inspect unrelated process command lines unnecessarily.

No global process-tree manipulation.

---

# Phase 8 — Listener snapshot

For B and D, capture:

- loopback port;
- listener owner PID;
- Worker PID;
- Streamlit PID;
- listener readiness result.

For A/C:
- assert no Streamlit listener expected.

This is diagnostic evidence only.

---

# Phase 9 — Scenario matrix implementation

Extend the existing lifecycle test path with bounded diagnostic scenarios:

## A — Worker only
Reach P2 hold.
Send targeted CTRL_BREAK.
Capture diagnostics.

## B — Worker + Streamlit
Streamlit ready.
No probe.
Reach P2 hold.
Send targeted CTRL_BREAK.
Capture diagnostics.

## C — Worker + probe
No Streamlit.
Reach P2 hold.
Run governed probe and let it exit.
Send targeted CTRL_BREAK.
Capture diagnostics.

## D — Worker + Streamlit + probe
Streamlit ready.
Reach P2 hold.
Run governed probe and let it exit.
Send targeted CTRL_BREAK.
Capture diagnostics.

All scenarios must use identical Worker launch helper and signal path.

Do not change Worker arguments except for existing governed configuration differences needed by Streamlit/probe integration.

---

# Phase 10 — Optional E scenario

Add one E scenario only if A/B/C/D identifies a stdio/handle difference but not its exact cause.

E may vary one diagnostic-only launch property, such as:
- Worker stdio capture enabled vs disabled;
- Streamlit stdio mode;
- probe stdio mode.

E must change exactly one factor.

No broad combinatorial matrix.

If A/B/C/D is sufficient, do not add E.

---

# Phase 11 — Diagnostic output format

Diagnostics must be emitted as bounded test output only.

No persistent diagnostic file.
No new log directory.

Prefer one structured per-scenario block containing:

- scenario name;
- PIDs;
- group ID;
- launch modes;
- timing deltas;
- signal API result;
- Worker exit code;
- bounded stdout;
- bounded stderr.

Do not expose secrets.

---

# Phase 12 — Root-cause classification gate

After matrix execution, classify exactly one root-cause class using evidence.

Examples:

## Class H
Only sequencing/cleanup ordering differs; topology/stdio unchanged.

## Class P
Failure correlates with console/process-group topology or signal propagation.

## Class S
Failure correlates with stdio/pipe/handle inheritance.

## Class T
Same topology but timing/order changes outcome reproducibly.

## Class W
Worker receives cancellation but exits incorrectly independent of harness topology.

## Class U
Evidence remains insufficient.

This authority ends at classification.

Do not fix even if root cause is obvious.

---

# Phase 13 — No-fix hard gate

Under this authority, **do not modify behavior to fix the failure**.

Forbidden:
- changing signal flags;
- changing process-group creation;
- changing Worker cancellation code;
- changing test sequencing for success rather than diagnosis;
- changing Replay;
- adding delays to mask race;
- changing Streamlit/probe semantics.

Only instrumentation and scenario setup are authorized.

If a diagnostic change accidentally alters behavior materially, revert that diagnostic approach and use a passive alternative.

---

# Phase 14 — Validation

After instrumentation:

Run:
- scenario A/B/C/D matrix;
- original standalone focused CTRL_BREAK test;
- existing focused WP08 tests if time permits;
- build.

Do not require full 313/313 regression unless instrumentation changes the test/helper in a way that could affect it broadly.

However, if the matrix completes and helper changes are nontrivial, run Infrastructure and full .NET to ensure diagnostic instrumentation itself is non-breaking.

Report exact counts.

---

# Phase 15 — Cleanup/residue

After each scenario:
- no Worker residue;
- no Streamlit residue;
- no probe residue;
- no owned listener residue;
- no orphaned pipe handles.

Use forced cleanup only as diagnostic cleanup fallback, not acceptance.

Do not globally kill by process name.

---

# Phase 16 — Scope audit

Changed paths may be only:

- `WindowsIsolatedProcessGroup.cs`
- `WP08LifecycleDemonstrationTests.cs`

Everything else must be unchanged under this authority.

Prove zero:
- production mutation;
- Python probe mutation;
- Replay mutation;
- WP05/WP06/WP07 mutation;
- package change;
- GitHub mutation;
- WP09.

---

# Lifecycle boundary

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open.

GitHub mutations:
`ZERO`

---

# Required completion report

## Instrumentation added
Exact paths/symbols.

## Scenario matrix
A/B/C/D results with:
- exit codes;
- process-group IDs;
- stdio modes;
- listener/process topology;
- timing;
- bounded stdout/stderr.

## Differential finding
What exact additional process/state correlates with failure.

## Root-cause classification
Class H/P/S/T/W/U.

## Confidence/evidence
Why the class is supported.

## Residue
No diagnostic process/listener/handle residue.

## Validation
Standalone + focused/build/full results run.

## Scope
Only two diagnostic paths changed.

## Lifecycle
#233/#234 unchanged.

## Mutation statement

`WP08 BOUNDED PROCESS-TOPOLOGY DIAGNOSTIC INSTRUMENTATION GITHUB MUTATIONS: ZERO`

## Next step

If classified H/P/S/T/W:

`WP08 ROOT CAUSE CLASSIFIED — NARROW CLASS-SPECIFIC FIX AUTHORITY REQUIRED`

If still U:

`WP08 ROOT CAUSE REMAINS UNRESOLVED — ADDITIONAL DIAGNOSTIC AUTHORITY REQUIRED`

---

# Terminal markers

Successful classification:

`RELEASE 1.9 WP08 BOUNDED PROCESS-TOPOLOGY DIAGNOSTIC INSTRUMENTATION COMPLETE`

Still unresolved:

`RELEASE 1.9 WP08 BOUNDED PROCESS-TOPOLOGY DIAGNOSTIC INSTRUMENTATION BLOCKED`

Do not emit COMPLETE unless the A/B/C/D matrix is safely executed and the root cause is classified out of Class U.

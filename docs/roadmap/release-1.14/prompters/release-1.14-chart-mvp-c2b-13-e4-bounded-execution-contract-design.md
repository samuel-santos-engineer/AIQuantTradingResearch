# C2B-13 — E4 bounded execution-contract design

Date: 2026-10-16. **Documentation proposal only; no runner/test creation or execution.**
`PUBLIC_RUNTIME_VERIFIED=False`; public dataset/candles/indicators unavailable;
activation/Azure deployment NO-GO; advanced WP07 ON HOLD. Read with the
[resource/decision worksheet](release-1.14-chart-mvp-c2b-13-e4-resource-and-owner-decision-worksheet.md).

## Static inventory and unresolved host facts

Repository observations: `global.json` requests SDK 10.0.103 but allows `latestFeature`
roll-forward and prereleases; it is not an exact selected installed executable. Central
build targets net10.0. Package declarations are Microsoft.NET.Test.Sdk 17.14.1, xunit
2.9.3 and xunit.runner.visualstudio 3.1.4, not evidence of installed resolved binaries.
Directory.Build.props imports optional local overrides; Directory.Build.targets has an
opt-in Debug signing Exec. No local credential/signing configuration was read or changed.
Future proof must explicitly disable signing and reject unreviewed imports/targets; no
credential operation is authorized. Release configuration alone does not qualify a build.

Expected logical tree, not a measured process inventory: Windows PowerShell 5.1 launcher
-> proposed external coordinator -> dotnet CLI/MSBuild/build helpers for the build phase;
then dotnet/VSTest/testhost -> CPython oracle during proof. VSTest, discovery, compiler
servers, MSBuild nodes and test adapters may create additional children. Stdout/stderr
drain threads/tasks are not processes, but consume memory/CPU and require cancellation.
Actual executable count, runtime architecture, installed SDK/interpreter, ambient jobs,
helpers, callbacks and parent/descendant behavior are **UNVERIFIED**. No toolchain version
command, testhost discovery or native probe was run. Existing reference code launches an
environment-selected Python with `-I -B`, bounded character reads and a timeout followed
by tree kill and a final wait; none proves this proposed whole-tree boundary.

## Containment alternatives and conditional recommendation

| Alternative | Benefit | Gap / disposition |
| --- | --- | --- |
| Windows Job Object with kill-on-close and launch-time association | Candidate kernel ownership/accounting domain for child tree; avoids relying on PowerShell polling for ownership | Preferred design candidate only; access, nested-job compatibility, inheritance, accounting and limits need separate qualification |
| Suspended creation then explicit AssignProcessToJobObject | Prevents useful child execution before assignment/resume | Coordinator death between creation and assignment can strand a suspended child; not accepted without independent closure of that window |
| Qualified pre-existing bounded runner | Could avoid new harness and reuse accepted process/byte bounds | No such E4-qualified runner identified in inspected repository; require exact revision/API/ownership evidence, not assumed availability |
| Polling/Process.Kill/process-name scan | Simple diagnostic signals | No race-free ownership, parent-death or hard CPU/RSS proof; reject as sole containment |
| Different already-approved isolated host mechanism | Could provide stronger existing containment | None established; new service/privilege/paid resource is not selected |

Microsoft documents child-job inheritance, nested-job constraints and breakaway rules;
association can fail and accounting depends on the job chain. The design must deny
breakaway and fail closed on unsuccessful association, not silently fall back to polling.
See [AssignProcessToJobObject](https://learn.microsoft.com/en-us/windows/win32/api/jobapi2/nf-jobapi2-assignprocesstojobobject).
Microsoft's [launch-time job-list discussion](https://devblogs.microsoft.com/oldnewthing/20230209-00/?p=107812)
describes the suspended-create/assignment orphan window and a launch-time job-list
alternative. That published option is not proof it is usable on this host/toolchain.

Proposed lifecycle, requiring separate design acceptance and implementation authority:

1. Independent coordinator creates a non-inheritable kill-on-close job and applies reviewed
   limits before launching work. No child or helper receives a handle that can keep the job
   alive or change policy. Controller's own health/resources need an external owner/watchdog
   contract; do not claim child-job limits bound a hung controller.
2. Create each root with verified launch-time association and narrowly inherited pipe handles;
   reject unsupported/nested-job/access/breakaway conditions before work. Explicit assignment
   after an already-running launch is not equivalent. Any partial launch is a failed batch.
3. Keep owned stable process handles and creation identity, not PIDs alone. Maintain process
   event/accounting reconciliation, including short-lived children and build helpers; test
   adapters cannot introduce unreviewed out-of-job services. No parent-created external helper
   may execute proof work outside the accounted tree.
4. On normal exit, cancellation, limit breach, malformed output or parent failure, stop admission;
   request cooperative cancellation within approved grace, then terminate the job if needed.
   Kill-on-last-handle-close is a crash backstop, not proof of completed cleanup.
5. Verify job active count zero, reconcile membership events and signal/exit state of every
   retained process handle within bounded final wait. Drain/close pipes and join drain tasks
   within approved bounds. PID absence, parent exit or one successful kill call is insufficient.
6. If reconciliation, final wait, orphan state or controller continuity is uncertain, latch the
   session blocked and deny all repeats; retain sanitized evidence for explicit owner recovery.
   No automatic reset or retry. A later runner qualification must cover controller death at
   every launch stage; no such failure injection is authorized now.

Job memory limits concern committed virtual memory, not an identical RSS ceiling; keep
RSS measurement/abort distinct and state polling overshoot. CPU time, wall deadlines,
kernel/job accounting and notification behavior require distinct verified semantics.
See Microsoft's [extended-limit structure](https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-jobobject_extended_limit_information).
No claimed hard CPU/RSS guarantee, Job Object experiment or native code accompanies this plan.

## Literal source, executable and output proposal

First proof source remains ONLY the two future candidate paths:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Neither exists by authority of this task. A separate runner is likely necessary to keep
containment independent of the testhost. Candidate additional request:
`eng/qualification/e4/Invoke-E4BoundedProof.ps1`, Windows PowerShell 5.1.26100.9444-compatible.
This path is NOT approved, created or a claim that a single PowerShell script can satisfy
the native boundary. If reviewed implementation requires a compiled helper, propose its
literal project/source/output paths separately before creation. No runtime exporter seam,
production friendship change, Python production port or dependency is selected.

| Executable/input | Proposed resolution rule | Current status |
| --- | --- | --- |
| PowerShell | Exact Windows PowerShell 5.1.26100.9444 host, noninteractive/no profile; no pwsh substitution | Owner preference known; actual selected host binary identity unverified |
| .NET | Candidate `C:\Program Files\dotnet\dotnet.exe`; exact SDK/runtime/testhost/adapter paths, file identities and versions must be supplied/accepted | Candidate path only; no installed-path or hash assertion; SDK roll-forward must be resolved without editing global.json now |
| CPython | Candidate `C:\projects\github\AIQuantTradingResearch\.venv\Scripts\python.exe`; exact version/runtime/executable identity reviewed | Presence/version/capability unverified; no PATH or AIQ_WP02_REFERENCE_PYTHON fallback |
| Build inputs/cache | Reviewed project graph and existing offline package cache, locked/resolved package inventory | Cache path/content and resolution unverified; no restore permitted; missing input STOP |
| Environment | Minimal explicit PATH/runtime inputs, fixed culture/timezone; reviewed TEMP/TMP/DOTNET_CLI_HOME/NUGET_PACKAGES; no credentials/private paths | Exact values TBD before authority; no broad inherited env dump or secrets in logs |

No command is ready for execution. Future build/test commands need absolute argument
arrays without shell concatenation, reviewed imports/analyzers/source generators/adapters,
no restore and no network. `--no-restore` still permits builds/writes; a separately reviewed
build phase must place every project's intermediate/output under owned external paths.
Then use a separately bounded no-build/no-restore test phase, with approved test filter,
no discovery of unrelated tests, no parallel workers or persistent build servers. None of
these flags by itself proves no network or no helpers. Offline cache alone is not network
denial: an existing effective egress-denied context must be verified under later authority;
new firewall/configuration changes are not authorized. Job Objects and `-I -B` do not
establish filesystem/network confidentiality.

Candidate root (not created, verified or authorized):
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b12-e4-proof-candidate`.
Proposed direct evidence names: `proof-summary.json`, `comparison-summary.json`, `runner.log`.
Proposed build directory names: `build\artifacts`, `build\intermediate`, `tool-home`, `tmp`.
Those directory names are not wildcard output permission: exact per-project generated file
inventory, cache behavior and directory quotas remain **TBD — blocking** until reviewed.
No source-tree obj/bin, TestResults/TRX, dumps, persistent rows/vectors/base64, snapshots,
private inputs or additional files allowed by inference. Build/test stderr and exception
messages must be sanitized before persistent evidence, not blindly retained.

Before later execution, require absent/empty resolved root, non-reparse immutable ancestry,
ownership, available quota and no protected/private path overlap. Enumerate exact created
files with ownership and byte totals. Cleanup only those literal owned files beneath the
verified root; no computed unverified recursive delete. Unexpected output latches STOP;
preserve bounded safe evidence, never overwrite/delete unrelated output to force a pass.

## Bounded test-only frame and independence contract

Propose one uncompressed UTF-8 frame with a four-byte unsigned big-endian byte length;
length must be positive and within accepted cap before body allocation. Read fixed header
first; partial header/body, timeout, duplicate/unknown key, wrong type/count/sequence or
extra bytes fail. Strict JSON has no duplicate keys, NaN/floats or BOM; integers/scale-exact
strings and explicit nulls, fixed field order, version/case/selection/row identity. The
format is test-only, pending independent review, not a release/API schema or accepted hash.
Use one bounded incremental buffer and backpressure; account for decoder/parser copies.
Drain bounded stderr concurrently; never deadlock on unread pipes or accept truncation.

Oracle receives only frozen specification/version selectors, never production rows or
expected outputs. It independently derives complete datasets and windows, emitting bounded
chunks with sequence/row count and a final independently computed summary. C# compares
every byte/value/null/identity in memory and checks frozen fingerprints. No produced
datasets/analytics persist. Buffers must fit the proposed caps; chunking test transport
does not authorize public pagination. Independently review exact serialization before
comparison. No signed key, pin, derived release hash or golden artifact created here.

Reviewer role: an independent numerical/spec reviewer who did not author the production
generator/calculator or oracle, assigned explicitly by owner before comparison. Evidence:
reviewed spec sections, oracle revision/authorship, hand-derived vectors, rounding and
serialization derivations, review findings/resolution. No reviewer is assigned or review
claimed complete now. No production imports/copied output, translated calculator oracle
or tolerance. Existing embedded generator reference is corroborating historical evidence,
not complete new B1 indicator qualification.

Preserve all three complete datasets 8160/2040/340; all twelve fixed calculation/visible
windows and frozen dataset/window/visible hashes; 250 hidden warmup rows; full B1 null/value
and identity comparisons. Use frozen SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9,
population Bollinger20×2, scale 10^12, signed ties-to-even and sqrt midpoint comparison
`4A` versus `400(2k+1)^2`. Detailed count/byte inventory remains in the unchanged
[C2B-12 worksheet](release-1.14-chart-mvp-c2b-12-e4-proof-coverage-and-resource-worksheet.md).
No production Python port, provider inputs or warmup publication. A mismatch stops;
never revise constants or expected output to achieve parity.

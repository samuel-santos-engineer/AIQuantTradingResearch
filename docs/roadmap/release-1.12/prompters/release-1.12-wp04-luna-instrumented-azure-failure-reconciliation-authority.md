# GPT-5.6 Luna — Release 1.12 WP04 Instrumented Azure Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the instrumented Azure initialize failure and determine the narrowest next evidence-producing step.
- **GPT-5.6 Terra** — execute only the later explicitly authorized remediation/investigation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current App Service state after restoration:

```text
Running/Normal
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Failed fresh initialize RunId:

```text
initialize-392a46ae50404c5182f5d76515d8b71e
```

This RunId is permanently forbidden for reuse.

## 2. Established result

The exact instrumented digest was deployed successfully and normal runtime readiness passed.

The one authorized initialize attempt established:

```text
helper provenance match = True
Windows PowerShell = 5.1.26100.9444
temporary settings application = succeeded
explicit qualification restart = performed once
token = undisclosed
terminal helper result = NOT_EMITTED
D3 record = NOT_PROVEN
instrumented diagnostic boundary = NOT_AVAILABLE
temporary settings after helper ended = all six still present
restoration to empty pre-attempt state = PASS
final normal runtime = Running/Normal
```

No retry occurred.

No source/Git/Docker/GHCR/GitHub lifecycle mutation occurred.

## 3. Critical distinction

The current failure is **not equivalent** to the prior terminal `Timeout` failures.

The committed helper did not emit its governed terminal result.

Therefore Luna must first determine whether the failure occurred in:

```text
A. helper process/control flow
B. parent invocation/wrapper process
C. Azure CLI subprocess invocation
D. PowerShell terminating-error behavior
E. process/session termination outside helper logic
F. request execution before terminal telemetry
G. cleanup/finally path before terminal reporting
H. another local execution boundary
```

Do not infer an Azure listener/request failure from absence of terminal output.

Do not infer that the 180-second helper deadline expired unless supported by execution evidence.

## 4. Reconciliation objective

Read-only inspect the exact committed helper and the execution evidence available from the failed run to answer:

1. What code paths can terminate the script before a terminal helper marker/result is emitted?
2. Is every `throw`, terminating error, native-command failure, `exit`, return path, trap, or `finally` behavior covered by terminal telemetry?
3. Can `az` or another native process terminate/abort the invocation without reaching the helper's terminal reporting path?
4. Can restoration logic exist only outside the helper, explaining why six settings remained after helper termination?
5. Does `$ErrorActionPreference`, `-ErrorAction Stop`, `$LASTEXITCODE`, pipeline behavior, or Windows PowerShell 5.1 exception semantics create an uncovered path?
6. Is there a caller/wrapper timeout or process-kill boundary independent of the helper's internal 180-second budget?
7. Was the helper launched synchronously in the same process, via child PowerShell, job, timeout wrapper, CI runner, or Codex execution layer?
8. Is there any evidence of host/session termination, tool timeout, or external cancellation?
9. Can current source diagnostics ever be observed if Azure application logging remains disabled?
10. What is the minimum next step that makes the **helper termination boundary** observable before another Azure initialize attempt?

## 5. Required read-only inspection scope

Inspect at minimum:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

and any directly invoked local wrapper/helper used for the failed qualification, if committed and available.

Inspect call sites for:

```text
Invoke-WebRequest
az webapp config appsettings
az webapp restart
az webapp show
native command invocation wrappers
try/catch/finally
throw
exit
return
trap
$ErrorActionPreference
$LASTEXITCODE
Start-Process
PowerShell child-process invocation
jobs/runspaces
timeout/watchdog wrappers
```

Do not modify files.

## 6. Terminal telemetry audit

Map every helper termination path into one of:

```text
T1 — emits governed terminal success result
T2 — emits governed terminal HTTP failure result
T3 — emits governed terminal transport failure result
T4 — emits governed terminal semantic failure result
T5 — exits/throws without terminal telemetry
T6 — can be externally terminated before telemetry
```

For every `T5` or `T6`, identify the exact code/process boundary.

A single proven `T5` or `T6` is sufficient to reject another identical initialize attempt.

## 7. Instrumented diagnostic observability audit

The instrumented image emits `WP04_DIAG_*` events, but current Azure application/container logging remains disabled.

Determine exactly whether any existing read-only Azure surface can retrieve those stdout/stderr events without enabling logging.

Classify:

```text
DIAGNOSTIC_IMAGE_EMISSION = PROVEN
DIAGNOSTIC_AZURE_CAPTURE = PROVEN | NOT_PROVEN
DIAGNOSTIC_AZURE_RETRIEVAL = PROVEN | NOT_PROVEN
```

If capture/retrieval is not proven, state plainly that source instrumentation alone cannot locate the Azure boundary under the current platform telemetry configuration.

Do not enable logging under this authority.

## 8. Candidate decisions

Select exactly one:

### D1 — H1_HELPER_TERMINAL_TELEMETRY_CORRECTION

Use if a concrete helper control-flow path can terminate without a terminal sanitized marker/result.

Expected next step:

```text
Terra one-path helper remediation
```

Prefer this if the missing terminal result is caused by committed helper behavior.

### D2 — H2_CALLER_WRAPPER_TERMINATION_CORRECTION

Use if the helper is correct but the invocation wrapper/process can kill or truncate it before completion.

Expected next step:

```text
Terra caller/wrapper remediation
```

Exact path/process boundary required.

### D3 — H3_LOCAL_EXECUTION_OBSERVABILITY_ONLY

Use if no code defect is proven, but a one-run local execution wrapper can capture:

```text
start timestamp
child PID
exit code
stdout
stderr
elapsed time
termination reason
```

without Azure mutation.

Expected next step:

```text
Terra local wrapper instrumentation authority
```

No Azure retry yet.

### D4 — H4_ENABLE_NARROW_AZURE_LOG_CAPTURE

Use only if helper termination is fully explained/covered but Azure stdout diagnostics are otherwise unretrievable, and enabling a narrow, temporary, $0-compatible logging surface is the minimum remaining evidence path.

Expected next step:

```text
Terra temporary Azure diagnostic-capture authority
```

Luna must specify exact settings, duration, cleanup, and cost/security proof.

### D5 — H5_SOURCE_DIAGNOSTIC_CHANNEL_REDESIGN

Use if Azure stdout capture is structurally unavailable or unsuitable and the application-owned diagnostic evidence must be exposed through another governed channel.

Expected next step:

```text
Luna/Terra diagnostic-channel redesign
```

This requires separate architecture reconciliation before implementation.

### D6 — H6_REPEAT_INITIALIZE

Use only if Luna proves:

```text
helper terminal telemetry path is complete
caller cannot terminate/truncate execution
diagnostic capture/retrieval is available
the previous no-terminal-output event was externally transient and identified
```

Absence of evidence is not justification.

## 9. Decision preference

Prefer the narrowest step that resolves the **missing terminal-helper-output boundary** before touching Azure again.

Default ordering unless evidence proves otherwise:

```text
D1 > D2 > D3 > D4 > D5 > D6
```

Do not choose D6 merely because Azure is currently `Running/Normal`.

## 10. Retry/timing policy

Preserve exactly:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
client evidence-poll budget <= 180 seconds
listener lifetime <= 180 seconds
```

This reconciliation does not authorize changing them.

## 11. Source/image/Azure preservation

Under this Luna authority:

```text
repository mutations = 0
Git mutations = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
Azure restarts = 0
Azure logging changes = 0
PR/lifecycle mutations = 0
```

Preserve deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Do not restore the prior image absent separate evidence.

## 12. Failed RunIds

All known failed initialize RunIds remain forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
```

Also preserve the malformed historical identifier as non-reusable if encountered.

## 13. Required output

Return:

- exact helper termination-path map;
- exact uncovered `T5`/`T6` path if any;
- exact caller/wrapper process model;
- whether external truncation/kill is possible;
- whether restoration is helper-owned or caller-owned;
- whether current instrumented stdout/stderr is captured by Azure;
- whether it is retrievable read-only;
- D1–D6 comparison;
- exact selected decision;
- exact next authority type;
- exact path allowlist if source remediation is chosen;
- whether new source commit is required;
- whether new image is required;
- whether Azure mutation is required in the next step;
- retry/timing preservation statement;
- failed RunId preservation statement;
- zero-mutation audit.

## 14. Terminal markers

Required:

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY COVERAGE: <COMPLETE|INCOMPLETE|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CALLER/WRAPPER EXTERNAL TERMINATION BOUNDARY: <PROVEN|NOT_PROVEN|NOT_APPLICABLE>`

`RELEASE 1.12 WP04 — DIAGNOSTIC IMAGE EMISSION: PROVEN`

`RELEASE 1.12 WP04 — DIAGNOSTIC AZURE CAPTURE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — DIAGNOSTIC AZURE RETRIEVAL: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE FAILURE DECISION: <D1|D2|D3|D4|D5|D6>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE FAILURE RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one next-authority marker matching the selected decision:

`RELEASE 1.12 WP04 — TERRA HELPER TERMINAL TELEMETRY REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA CALLER/WRAPPER REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA LOCAL EXECUTION OBSERVABILITY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA TEMPORARY AZURE DIAGNOSTIC CAPTURE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA DIAGNOSTIC CHANNEL REDESIGN AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA REPEAT INITIALIZE AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA INSTRUMENTED AZURE FAILURE RECONCILIATION COMPLETE`

# Release 1.12 WP04 — Terra External Bounded Validator Supervisor

**Selected execution model: GPT-5.6 Terra**

## Reconciled gate

The next required action is external observation of the validator process. The validator has repeatedly reached durable `BUILD_LEDGER` without `SERIALIZE`, and its internal catch/failure-diagnostic path has not been observed.

This authority is specifically for constructing and running a bounded external supervisor, then continuing validator-only remediation if the resulting evidence is sufficient.

No runner, production, W5, or external-service mutation is authorized.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

Before execution, compute and retain the current ValidatorSHA256. Any validator byte change requires a fresh hash and fresh structural roots.

## Supervisor location and mutation boundary

Create the supervisor outside tracked repository paths. It is a disposable/local validation artifact, not production code.

It may create durable sanitized diagnostic evidence under the Architecture-B evidence area.

It must not stage, commit, push, edit the runner, edit tracked production source, mutate GitHub/Azure/Docker/GHCR, configure secrets, invoke W5, or allocate a W5 RunId.

## Required supervisor observations

Launch the validator as a child process and durably capture:

```text
SupervisorSHA256
ValidatorSHA256
RunnerSHA256
ChildPID
ProcessStartUtc
ProcessEndUtc or TimeoutUtc
ElapsedMilliseconds
ExitObserved
ExitCode if available
TimeoutObserved
KillRequired
KillResult if applicable
StdoutArtifactPath
StderrArtifactPath
SanitizedStdoutTail
SanitizedStderrTail
LastDurableCheckpoint
LastDurableCheckpointTimestamp
FailureDiagnosticPresent
CanonicalCheckpointCounts
```

The supervisor must capture stdout and stderr independently and must not depend on the interactive Codex session returning child output.

## Bounded execution

Use a finite timeout and record its exact duration.

The timeout must be long enough to avoid classifying normal local serialization latency as a hang, but it must not permit an unbounded wait.

If the timeout expires and the child remains alive, record that fact before termination.

A timeout-triggered child termination by the supervisor is a local diagnostic cleanup action, not evidence that the original validator process would have terminated itself. Preserve that distinction.

## Required outcome classification

Classify from observed evidence:

### A — NORMAL_EXIT_BEFORE_SERIALIZE

Use only if:

```text
child exited independently
SERIALIZE absent
```

Retain exact exit code and output tails.

### B — SERIALIZATION_HANG_OR_NONRETURN

Use only if:

```text
child remained alive through bounded timeout
last durable checkpoint = BUILD_LEDGER
SERIALIZE absent
no independent exit before timeout
```

### C — HOST_OR_PROCESS_TERMINATION

Use only when external evidence demonstrates termination independent of supervisor timeout cleanup.

### D — PROGRESSED_BEYOND_BUILD_LEDGER

Use if `SERIALIZE`, `PUBLISH`, or `REOPEN` appears. Report the exact furthest durable checkpoint.

### E — DIFFERENT_FAILURE_BOUNDARY

Use when the fresh execution fails before or elsewhere than the historical boundary.

### F — INSUFFICIENT_EXTERNAL_EVIDENCE

Use when the supervisor itself cannot establish process lifecycle truth.

Do not infer a serializer root cause merely from A/B/C.

## If B is proven

If `SERIALIZATION_HANG_OR_NONRETURN` is proven, Terra may add validator-only, sanitized diagnostic markers around individual serialization sub-operations:

```text
SERIALIZE_ENTER
SERIALIZE_OBJECT_READY
SERIALIZE_CONVERTJSON_ENTER
SERIALIZE_CONVERTJSON_RETURN
SERIALIZE_TEMPWRITE_ENTER
SERIALIZE_TEMPWRITE_RETURN
SERIALIZE_REOPEN_ENTER
SERIALIZE_REOPEN_RETURN
```

These are diagnostic markers only and must not be written to or counted as canonical acceptance checkpoints.

After marker instrumentation:

```text
parse
→ fresh ValidatorSHA256
→ fresh roots
→ rerun under external supervisor
```

Use the last retained diagnostic marker to isolate the non-returning sub-operation.

## If serializer non-return is isolated

Terra may perform bounded, non-recursive object-shape diagnostics: top-level property names/types, collection counts, bounded nesting summaries, cycle/self-reference checks, unexpected `ErrorRecord`, `InvocationInfo`, `PSObject`, process/runspace/stream objects, and approximate bounded payload characteristics.

Do not recursively dump arbitrary object graphs or secrets.

Once evidence identifies an ordinary validator-local defect, apply the narrowest correction and continue the existing autonomous loop:

```text
correct validator
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
→ repeat until structural PASS or governance BLOCKED
```

Do not return merely for another ordinary validator/finalizer defect.

## Mandatory escalation

STOP as `BLOCKED` only if the next required action is:

```text
runner modification
tracked production-source modification
Architecture-B/P01-P20/SV01-SV36/G01-G13 change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/GitHub/Docker/GHCR/external-service mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

Also stop if a correctly functioning external supervisor plus safe validator-only instrumentation cannot distinguish the failure sufficiently for a truthful correction.

## Structural PASS remains unchanged

Final acceptance still requires a fresh single-validator-hash run with:

```text
PRE_CLEANUP       = exactly 1
CLEANUP_COMPLETE  = exactly 1
BUILD_LEDGER      = exactly 1
SERIALIZE         = exactly 1
PUBLISH           = exactly 1
REOPEN            = exactly 1

SV records = 36
SV failures = 0
Evidence references = 36
Resolved = 36
Unresolved = 0
Runtime P01-P20 PASS claims = 0
Final ledger reopen/parse = PASS
GitDiffCheckOutput = PRESENT
GitDiffCheckExitCode = 0
DisposableRoot = ABSENT after cleanup
Runner hash = frozen hash
Validator hash = final fresh hash
tracked/staged mutations = 0
W5 invocation = NO
W5 RunId allocation = NO
```

## Required immediate handoff

At minimum report:

```text
RunnerSHA256
ValidatorSHA256
SupervisorPath
SupervisorSHA256
SupervisorObservationLedgerPath
ChildPID
TimeoutSeconds
ProcessStartUtc
ProcessEndUtc
ElapsedMilliseconds
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
StdoutArtifactPath
StderrArtifactPath
SanitizedStdoutTail
SanitizedStderrTail
LastDurableCheckpoint
LastDurableCheckpointTimestamp
FailureDiagnosticPresent
CanonicalCheckpointCounts
ProcessOutcomeClassification
NextValidatorLocalAction
ExactMutationAccounting
```

If autonomous remediation proceeds beyond this observation, also return all subsequent validator hashes, fresh roots, defects, corrections, and the final structural gate result.

## Terminal markers

Observation successfully established:

```text
RELEASE 1.12 WP04 — TERRA EXTERNAL BOUNDED VALIDATOR SUPERVISOR: PASS
RELEASE 1.12 WP04 — PROCESS OUTCOME: <A|B|C|D|E>
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — PRODUCTION MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
```

Supervisor unable to establish lifecycle truth:

```text
RELEASE 1.12 WP04 — TERRA EXTERNAL BOUNDED VALIDATOR SUPERVISOR: NOT_READY
RELEASE 1.12 WP04 — PROCESS OUTCOME: F_INSUFFICIENT_EXTERNAL_EVIDENCE
```

A later full structural PASS still requires fresh GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation before W5 is authorized.

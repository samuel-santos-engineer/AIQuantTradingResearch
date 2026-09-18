# Release 1.12 WP04 — Terra Recover Detached Supervisor Result & Continue

**Selected execution model: GPT-5.6 Terra**

## Reconciled state

The detached artifact-first supervisor has been successfully launched:

```text
DETACHED_SUPERVISOR_LAUNCHED=True
Bounded observation window = 60 seconds
```

It is independently observing the validator and writing local durable diagnostic artifacts.

No W5 execution, W5 RunId allocation, or external-service action occurred.

**Launch success is not diagnostic PASS.** Do not classify exit, hang/non-return, host termination, or serializer root cause until the durable supervisor artifacts are recovered.

## Immediate mission

Recover the already-launched supervisor result. **Do not launch another validator or supervisor first.**

Locate the exact durable supervisor root from the launch record and inspect:

```text
observation.json
stdout.log
stderr.log
canonical checkpoint artifact
failure-diagnostic.json if present
supervisor source/hash
```

If the 60-second observation window is still legitimately in progress, wait only for that existing bounded run to reach its declared terminal state. Do not extend the timeout and do not create an overlapping run.

## Required recovery proof

Report from durable artifacts:

```text
SupervisorRoot
SupervisorState
SupervisorPID
SupervisorStartUtc
SupervisorTerminal
TimeoutSeconds
RunnerSHA256
ValidatorSHA256
ChildPID
ChildStartUtc
ChildExitUtc
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
StdoutPath
StderrPath
SanitizedStdoutTail
SanitizedStderrTail
LastDurableCheckpoint
LastDurableCheckpointTimestamp
FailureDiagnosticPresent
CanonicalCheckpointCounts
```

If `observation.json` is nonterminal after the declared window, inspect actual supervisor/child process existence before classification.

## Outcome classification

Use exactly one evidence-supported classification:

```text
A_NORMAL_EXIT_BEFORE_SERIALIZE
B_SERIALIZATION_HANG_OR_NONRETURN
C_HOST_OR_PROCESS_TERMINATION
D_PROGRESSED_BEYOND_BUILD_LEDGER
E_DIFFERENT_FAILURE_BOUNDARY
F_INSUFFICIENT_EXTERNAL_EVIDENCE
```

`B` requires proof that the child remained alive through the 60-second bound, `BUILD_LEDGER` remained the last durable canonical checkpoint, `SERIALIZE` was absent, and no independent exit occurred before timeout.

`C` requires externally observed termination independent of supervisor-triggered timeout cleanup.

A supervisor-triggered kill after timeout is diagnostic cleanup, not spontaneous validator termination.

## Autonomous continuation

If the recovered evidence establishes an ordinary validator-local defect or sufficiently isolates the failing sub-operation, **continue without returning merely to request another authority**.

Authorized continuation:

```text
recover durable supervisor evidence
→ classify process outcome
→ add narrow validator-only diagnostic markers if needed
→ diagnose validator-local root cause
→ apply narrow validator-only correction
→ WinPS 5.1 parser PASS
→ freeze fresh ValidatorSHA256
→ fresh DisposableRoot/DurableRoot
→ full SV01-SV36 from SV01
→ repeat until structural PASS or governance BLOCKED
```

If `B_SERIALIZATION_HANG_OR_NONRETURN` is proven but the exact sub-operation is not yet known, add sanitized durable diagnostic markers around:

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

These are diagnostic markers only, not canonical acceptance checkpoints.

Every validator byte change requires a new validator SHA-256 and fresh roots. Cross-hash PASS carry-forward is forbidden.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

No runner or tracked production edit is authorized.

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

Also stop if the completed detached observation remains insufficient and truthful diagnosis would require crossing one of those boundaries.

## Structural PASS gate remains unchanged

Final fresh single-validator-hash run requires exactly one durable accepted checkpoint each:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

plus:

```text
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

## Mutation accounting

Report exact tracked repository, staged, commit, push, GitHub, Azure, Docker/GHCR, production, external, W5, validator-local, and supervisor/watchdog mutation counts/artifacts.

## Required terminal handoff

Return the recovered supervisor fields above plus:

```text
ProcessOutcomeClassification
RootCauseClassification
DiagnosticMarkersAdded
CorrectionsApplied
SubsequentValidatorSHA256s
FreshStructuralRunAttempts
FinalStructuralResult
FirstBlockingDefect
FirstProhibitedRequiredAction
ExactMutationAccounting
```

## Next gate

If the autonomous loop reaches complete structural PASS, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**.

If governance `BLOCKED`, STOP for blocker-specific authority.

W5 remains prohibited.

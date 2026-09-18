# Release 1.12 WP04 — Terra Detached Supervisor / Artifact-First Execution

**Selected execution model: GPT-5.6 Terra**

## Reconciliation

The previous external-supervisor attempt is **NOT_READY / NO DIAGNOSTIC CREDIT**.

Observed only:

```text
Codex tool result: Script running with cell ID 17
```

The Codex session ended before the expected supervisor result was returned or proven durable.

Validator stdout before that attempt showed:

```text
STRUCTURAL_ONLY=TRUE
GOVERNED_W5_RUNID_ALLOCATED=NO
GOVERNED_W5_WRAPPER_INVOKED=NO
FINALIZE=PRE_CLEANUP
FINALIZE=CLEANUP_COMPLETE
FINALIZE=BUILD_LEDGER
FINALIZE=BUILD_LEDGER
```

`failure-diagnostic.json` was absent.

Therefore none of the following is proven:

```text
child exit
child exit code
timeout
hang/non-return
host termination
stderr
exception type/message/stack
supervisor observation ledger
supervisor completion
```

Do not infer any of them.

The duplicate visible `BUILD_LEDGER` line is console output only; it is not evidence of duplicate accepted durable checkpoints.

## Mission

Remove dependence on the lifetime of an interactive Codex tool cell.

Build and execute a **detached/artifact-first bounded supervisor** whose diagnostic truth is persisted incrementally to disk and can be inspected by a later Codex command/session even if the launching tool call disconnects.

Continue autonomously after successful observation if an ordinary validator-only defect becomes provable.

## Frozen boundary

Require:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

Compute and retain the actual current ValidatorSHA256 before launch.

No runner or production edit is authorized.

## Artifact-first supervisor design

Create a fresh durable supervisor root before launching the validator.

The supervisor must write its own state incrementally and atomically. At minimum retain:

```text
supervisor-source.ps1
supervisor-source.sha256.txt
observation.json
stdout.log
stderr.log
```

`observation.json` must be created **before** child launch with an initial state such as:

```text
State = SUPERVISOR_INITIALIZED
SupervisorPID
SupervisorStartUtc
RunnerSHA256
ValidatorSHA256
TimeoutSeconds
ChildPID = null
ChildStartUtc = null
ChildExitUtc = null
ExitObserved = false
ExitCode = null
TimeoutObserved = false
KillRequired = false
KillResult = null
LastDurableCheckpoint = null
LastDurableCheckpointTimestamp = null
SupervisorTerminal = false
```

Update `observation.json` atomically after every material transition.

## Required durable state machine

Persist these states when reached:

```text
SUPERVISOR_INITIALIZED
CHILD_STARTED
CHILD_OBSERVING
CHILD_EXITED
TIMEOUT_OBSERVED
CHILD_TERMINATION_REQUESTED
CHILD_TERMINATION_COMPLETED
ARTIFACTS_FINALIZED
SUPERVISOR_COMPLETE
```

Only applicable states need occur, but every transition that occurs must be durable before proceeding.

The final record must distinguish independent child exit from supervisor-triggered cleanup.

## Launch semantics

The supervisor must own the child process independently of the interactive Codex output stream.

Do not make diagnosis depend on the original tool invocation remaining attached.

Redirect child stdout and stderr directly to durable files from process launch where WinPS 5.1-compatible APIs permit.

Record `ChildPID` and `ChildStartUtc` immediately after successful process creation.

## Bounded timeout

Use a finite timeout and persist its exact value before child launch.

If the child remains alive at timeout:

1. persist `TIMEOUT_OBSERVED`;
2. record process-alive truth and last durable checkpoint;
3. set `KillRequired=true`;
4. only then terminate the child for local cleanup;
5. persist termination result.

A supervisor-triggered kill must never be reported as spontaneous validator termination.

## Recovery after Codex/session interruption

After launch, if the invoking Codex command/session loses attachment or reports only a running cell:

**do not immediately launch another validator.**

Instead, in a fresh command/session:

1. locate the exact durable supervisor root created before launch;
2. read `observation.json`;
3. inspect whether `SupervisorPID` and `ChildPID` are still alive;
4. inspect stdout/stderr files;
5. inspect canonical checkpoint artifact;
6. wait only within the already-declared timeout contract if observation is still legitimately in progress;
7. finalize/recover supervisor evidence if the supervisor has completed but the interactive caller disconnected.

Do not create overlapping diagnostic validator executions.

## External watchdog permission

If Codex's execution environment kills the parent supervisor when the tool cell/session ends, Terra may replace it with a more durable local-only watchdog/launcher mechanism that survives caller disconnection, provided it:

```text
remains local-only
stays outside tracked repository paths
has a bounded lifetime
writes the same durable observation contract
does not mutate external services
does not modify runner/production
does not invoke W5
```

Use only WinPS 5.1-compatible mechanisms.

## Classification gate

After durable observation, classify only from artifacts:

```text
A_NORMAL_EXIT_BEFORE_SERIALIZE
B_SERIALIZATION_HANG_OR_NONRETURN
C_HOST_OR_PROCESS_TERMINATION
D_PROGRESSED_BEYOND_BUILD_LEDGER
E_DIFFERENT_FAILURE_BOUNDARY
F_INSUFFICIENT_EXTERNAL_EVIDENCE
```

`B` requires proof that the child remained alive through the bounded timeout with `BUILD_LEDGER` last and `SERIALIZE` absent.

`C` requires evidence of termination independent of supervisor cleanup.

## Autonomous continuation

If A/B/C/D/E supplies sufficient evidence for an ordinary validator-local defect, continue under existing autonomous remediation authority:

```text
diagnose
→ narrow validator-only instrumentation/correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
→ repeat
```

Do not return merely because another ordinary validator/finalizer defect appears.

Every validator byte change requires a fresh hash and fresh structural run.

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

Also stop if an artifact-first detached supervisor/watchdog cannot obtain lifecycle truth without crossing one of those boundaries.

## Mutation accounting

Require exact accounting for:

```text
tracked repository mutations
staged paths
commits
pushes
GitHub mutations
Azure mutations
Docker/GHCR mutations
production mutations
external mutations
W5 executions
W5 RunIds
validator-local mutations
supervisor/watchdog artifacts
```

## Required immediate handoff

Return, from durable artifacts rather than transient console state:

```text
RunnerSHA256
ValidatorSHA256
SupervisorRoot
SupervisorPath
SupervisorSHA256
ObservationPath
SupervisorState
SupervisorPID
SupervisorStartUtc
TimeoutSeconds
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
ProcessOutcomeClassification
SupervisorTerminal
NextValidatorLocalAction
ExactMutationAccounting
```

## Terminal markers

Successful lifecycle observation:

```text
RELEASE 1.12 WP04 — TERRA DETACHED ARTIFACT-FIRST SUPERVISOR: PASS
RELEASE 1.12 WP04 — PROCESS OUTCOME: <A|B|C|D|E>
RELEASE 1.12 WP04 — SUPERVISOR TERMINAL ARTIFACT: PROVEN
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — PRODUCTION MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
```

Insufficient observation:

```text
RELEASE 1.12 WP04 — TERRA DETACHED ARTIFACT-FIRST SUPERVISOR: NOT_READY
RELEASE 1.12 WP04 — PROCESS OUTCOME: F_INSUFFICIENT_EXTERNAL_EVIDENCE
```

A full structural PASS still requires fresh GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation before W5 is authorized.

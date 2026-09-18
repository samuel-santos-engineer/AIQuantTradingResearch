# Release 1.12 WP04 — Terra Serialization Diagnostic Reproduction Only

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns this narrow validator-only diagnostic reproduction. GPT-5.6 Luna retains contract and acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Reconciled state

The retained failed package proves only:

```text
PRE_CLEANUP       = 1
CLEANUP_COMPLETE  = 1
BUILD_LEDGER      = 1
SERIALIZE         = 0
PUBLISH           = 0
REOPEN            = 0
```

Binding identities:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
FBF3B0FD537E62BAD0261F7E0E2475516E4067E6B6DDDD54970A00097150B26B

Historical failed DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\5a8321d8667e4e5aa32cbf7fa8d47ce8
```

The retained package contains no sanitized serializer exception artifact.

Therefore:

```text
Failure boundary = PROVEN
Exact serializer exception = NOT_PROVEN
Serialization root cause = NOT_PROVEN
Correction selection = NOT_AUTHORIZED
```

No runner change occurred.
No W5 execution occurred.
No W5 RunId was allocated.

## Mission

Reproduce the failure diagnostically with the **same frozen validator bytes and runner bytes**, while adding no validator or runner source mutation.

Capture the exact serializer exception and sufficient sanitized context to permit a truthful subsequent correction decision.

This authority is diagnostic only.

It grants no structural acceptance credit.

## Mandatory no-edit gate

Before execution require:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
FBF3B0FD537E62BAD0261F7E0E2475516E4067E6B6DDDD54970A00097150B26B
```

If either differs:

```text
STOP
DIAGNOSTIC_IDENTITY_MISMATCH
```

Do not edit either file to add logging.

## Diagnostic capture mechanism

Capture the existing process's native PowerShell exception surfaces externally.

Use a disposable diagnostic launcher/harness outside tracked repository paths if necessary.

The harness may:

```text
invoke the unchanged validator
capture stdout
capture stderr
capture process exit code
capture PowerShell ErrorRecord details exposed to the caller
retain sanitized diagnostic artifacts
```

It must not:

```text
modify validator bytes
modify runner bytes
change validator control flow
replace the serializer
skip earlier structural checks
fabricate the ledger
fabricate checkpoint records
invoke W5
allocate a W5 RunId
perform external mutations
```

Prefer observation of the real unchanged failing execution over synthetic unit reproduction.

## Fresh diagnostic roots

Do not reuse the historical failed root as an execution root.

Create fresh disposable/durable diagnostic roots as required by the unchanged validator.

The resulting execution remains diagnostic-only even if it progresses farther than the historical run.

No checkpoint/SV result from this reproduction receives acceptance credit.

## Required exception evidence

Retain a sanitized diagnostic artifact containing, when available:

```text
TimestampUtc
RunnerSHA256
ValidatorSHA256
DiagnosticDurableRoot
ProcessExitCode
LastDurableCheckpoint
FirstMissingCheckpoint
ExceptionType
FullyQualifiedErrorId
CategoryInfo
SanitizedExceptionMessage
SanitizedScriptStackTrace
InvocationInfoPosition
SerializationOperationIdentity
SerializerIdentity
SerializerArgumentsOrShape
TemporaryLedgerPathIdentity
```

Do not retain secrets, credentials, tokens, Twelve Data API keys, or sensitive environment values.

If the native exception surface does not expose one of these fields, record it as `NOT_AVAILABLE`; do not invent it.

## Required boundary verification

From the diagnostic run's durable checkpoint artifact, independently report exact accepted counts for:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

If the reproduction fails at a different boundary, report the actual observed boundary and do not force the historical interpretation.

## Root-cause decision gate

After diagnostic capture, classify only from evidence.

Examples of possible classifications include:

```text
JSON_SERIALIZATION_DEPTH_DEFECT
NON_SERIALIZABLE_OBJECT_DEFECT
CYCLIC_OBJECT_GRAPH_DEFECT
SERIALIZER_ARGUMENT_DEFECT
TEMPORARY_PATH_DEFECT
FILE_WRITE_DEFECT
ENCODING_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
OTHER_VALIDATOR_SERIALIZATION_DEFECT
NOT_REPRODUCED
INSUFFICIENT_EVIDENCE
```

Do not choose a correction during this authority.

## Mutation accounting

Require:

```text
validator source mutations = 0
runner source mutations = 0
authority-introduced tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
W5 executions = 0
W5 RunIds = 0
external mutations = 0
```

A disposable local-only diagnostic harness is permitted and must remain outside tracked repository paths.

## Required terminal result

If exact exception/root-cause evidence is captured:

```text
RELEASE 1.12 WP04 — TERRA SERIALIZATION DIAGNOSTIC REPRODUCTION: PASS
RELEASE 1.12 WP04 — SERIALIZATION FAILURE BOUNDARY: <OBSERVED>
RELEASE 1.12 WP04 — SERIALIZER EXCEPTION CAPTURE: PASS
RELEASE 1.12 WP04 — SERIALIZATION ROOT-CAUSE CLASSIFICATION: <CLASSIFICATION>
RELEASE 1.12 WP04 — VALIDATOR MUTATION: NO
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — STRUCTURAL ACCEPTANCE CREDIT: NONE
```

If the exception cannot be captured truthfully:

```text
RELEASE 1.12 WP04 — TERRA SERIALIZATION DIAGNOSTIC REPRODUCTION: NOT_READY
RELEASE 1.12 WP04 — SERIALIZER EXCEPTION CAPTURE: NOT_PROVEN
RELEASE 1.12 WP04 — SERIALIZATION ROOT-CAUSE CLASSIFICATION: INSUFFICIENT_EVIDENCE
RELEASE 1.12 WP04 — VALIDATOR MUTATION: NO
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — STRUCTURAL ACCEPTANCE CREDIT: NONE
```

## Required handoff

Return:

```text
RunnerSHA256
ValidatorSHA256
DiagnosticDurableRoot
DiagnosticCheckpointArtifactPath
DiagnosticCheckpointCounts
ProcessExitCode
LastDurableCheckpoint
FirstMissingCheckpoint
ExceptionType
FullyQualifiedErrorId
CategoryInfo
SanitizedExceptionMessage
SanitizedScriptStackTrace
InvocationInfoPosition
SerializationOperationIdentity
SerializerIdentity
SerializerArgumentsOrShape
TemporaryLedgerPathIdentity
RootCauseClassification
DiagnosticArtifactPath
ExactMutationAccounting
```

## Stop and next gate

STOP after diagnostic classification.

Do not edit the validator under this authority.

The next authority will select and execute the narrow validator correction based on the captured evidence, then require a fresh validator hash, fresh roots, and complete SV01-SV36 restart.

W5 remains prohibited.

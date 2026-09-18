# Release 1.12 WP04 — Terra Autonomous Validator Remediation Until Blocked

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra is authorized to diagnose, instrument, correct, and rerun the **validator/finalizer only** until either:

1. the complete fresh Architecture-B structural run reaches terminal PASS; or
2. a mandatory escalation boundary below is reached.

GPT-5.6 Luna retains the frozen contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Binding starting state

Verified identities before the latest diagnostic launch:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

CurrentValidatorSHA256 =
FBF3B0FD537E62BAD0261F7E0E2475516E4067E6B6DDDD54970A00097150B26B
```

Historical failed structural run proved durable cardinality:

```text
PRE_CLEANUP       = 1
CLEANUP_COMPLETE  = 1
BUILD_LEDGER      = 1
SERIALIZE         = 0
PUBLISH           = 0
REOPEN            = 0
```

The first known failure boundary is immediately after `BUILD_LEDGER`, at or within the ledger serialization operation.

A diagnostic launcher was executed but did not return captured process output through the prior session. Therefore the exact serializer exception remains unavailable.

Latest diagnostic attempt:

```text
validator edits = NO
runner edits = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Expanded authority

The previous diagnostic-only restriction is superseded by this authority.

Terra may now autonomously perform **validator-only and local diagnostic-harness corrections/instrumentation** necessary to expose and fix the failure, without returning for a new authority after each ordinary defect.

The objective is to continue until terminal structural PASS or a genuine governance/architecture/external-system blocker is reached.

## Mandatory initial verification

Before any edit:

```text
git status --short
staged paths
runner SHA256
current validator SHA256
Windows PowerShell version
```

Require:

```text
runner SHA256 = 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
Windows PowerShell = 5.1.26100.9444
```

If runner hash differs, STOP.

Do not assume the validator hash remains `FBF3...B26B` if another session legitimately changed its local bytes; report actual state before proceeding.

## Autonomous diagnostic permission

Terra may add narrow validator-owned sanitized diagnostic instrumentation when external capture is insufficient.

Permitted instrumentation includes:

```text
operation-scoped try/catch
sanitized exception type/message capture
FullyQualifiedErrorId capture
CategoryInfo capture
ScriptStackTrace capture
operation-stage markers
serializer argument/shape metadata
temporary-path identity metadata
durable diagnostic artifact writes
explicit terminating-error behavior
```

Diagnostic artifacts must exclude:

```text
secrets
credentials
tokens
Twelve Data API key
sensitive environment values
raw authentication material
```

Instrumentation must not alter production code or the frozen runner.

## Autonomous correction permission

Once evidence identifies a validator/finalizer defect, Terra may apply the narrowest correction and continue.

Permitted validator-only correction classes include:

```text
SERIALIZATION_OPERATION_DEFECT
JSON_SERIALIZATION_DEFECT
JSON_DEPTH_DEFECT
NON_SERIALIZABLE_OBJECT_DEFECT
CYCLIC_OBJECT_GRAPH_DEFECT
COLLECTION_MATERIALIZATION_DEFECT
SERIALIZER_ARGUMENT_DEFECT
LEDGER_OBJECT_DEFECT
LEDGER_METADATA_DEFECT
TEMPORARY_PATH_DEFECT
FILE_WRITE_DEFECT
FILE_FLUSH_CLOSE_DEFECT
ENCODING_DEFECT
TEMPORARY_JSON_PARSE_DEFECT
CHECKPOINT_PERSISTENCE_DEFECT
CHECKPOINT_ORDERING_DEFECT
ATOMIC_PUBLICATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LEDGER_FINALIZATION_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_DIAGNOSTIC_HARNESS_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

This list is not exhaustive for ordinary validator-only defects.

Do not weaken acceptance criteria to make a defect disappear.

## Diagnose → correct → validate loop

For each failure:

```text
observe exact failure
→ retain sanitized evidence
→ classify defect
→ apply narrow validator-only correction
→ parse
→ freeze validator bytes
→ compute fresh ValidatorSHA256
→ create fresh roots
→ restart complete SV01-SV36 from SV01
```

If a new validator defect appears:

```text
repeat loop
```

Do not stop merely because another validator-only defect is discovered.

Do not reuse PASS evidence across validator hashes.

## Instrumentation-byte rule

Any validator instrumentation changes validator bytes.

Therefore, after instrumentation:

```text
compute new ValidatorSHA256
```

If instrumentation is later removed or changed:

```text
compute another new ValidatorSHA256
fresh roots
restart SV01-SV36
```

A diagnostic run may be used only diagnostically unless the exact frozen validator bytes used in that run are subsequently accepted as the final candidate and the run itself satisfies the entire structural contract.

## Frozen runner

The runner is immutable under this authority.

Require before every governed fresh run and at terminal verification:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

No runner correction is authorized.

## Windows PowerShell contract

Binding target:

```text
Windows PowerShell 5.1.26100.9444
```

Require parser errors = 0 before every fresh run.

Do not introduce PowerShell 7-only syntax/cmdlets or unsupported newer .NET APIs.

## Architecture-B contract remains frozen

Do not change:

```text
P01-P20
SV01-SV36
G01-G13
RunnerSHA256 identity
P19 validator-owned post-cleanup finalization
P20 observation-only
runtime P01-P20 PASS claims = 0
cross-hash PASS carry-forward = forbidden
```

## Checkpoint contract

Successful fresh execution must durably retain exactly one accepted record for each, in order:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Console duplication is diagnostic only; retained durable cardinality controls.

No checkpoint may be persisted before its semantic condition is true.

## Serialization contract

`SERIALIZE` may exist only after:

```text
final ledger object exists
mandatory metadata populated
serialization succeeds
temporary ledger path is same durable filesystem/directory boundary
temporary write succeeds
write flushed/closed
temporary file exists
temporary JSON independently reopens/parses
```

## Publication contract

`PUBLISH` may exist only after:

```text
same-filesystem atomic publication succeeds
final sv01-sv36-ledger.json exists
```

## Reopen contract

`REOPEN` may exist only after:

```text
published ledger independently reopens
JSON parses
mandatory metadata exists
terminal invariants reverify
```

## Fresh-run terminal requirements

Require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0

PRE_CLEANUP = 1
CLEANUP_COMPLETE = 1
BUILD_LEDGER = 1
SERIALIZE = 1
PUBLISH = 1
REOPEN = 1

final sv01-sv36-ledger.json exists
final ledger reopen/parse = PASS
GitDiffCheckOutput property = PRESENT
GitDiffCheckExitCode = 0

DisposableRoot absent
DurableRoot survives
runner survives
validator survives
checkpoint artifact survives
ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup = frozen runner hash
ValidatorHashAfterCleanup = final fresh validator hash

staged paths = 0
authority-introduced tracked repository mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Local harness permission

Terra may create, modify, rerun, and delete disposable local-only diagnostic harnesses outside tracked repository paths.

Retain sanitized harness source/hash/output under the durable evidence root when materially needed to support a diagnosis.

Harness activity grants no acceptance credit by itself.

## Mandatory escalation boundary

Continue autonomously until one of these occurs.

STOP and report `BLOCKED` if resolution requires any of:

```text
runner modification
tracked production-source modification
Architecture-B contract change
P01-P20 change
SV01-SV36 change
G01-G13 change
acceptance weakening
mandatory evidence deletion
P19/P20 reinterpretation
cross-hash PASS composition
Azure mutation
Docker/GHCR mutation
GitHub mutation
Twelve Data secret configuration
other external/production mutation
W5 execution
W5 RunId allocation
```

Also STOP if, after reasonable validator-only diagnostic instrumentation, the failure cannot be observed sufficiently to choose a truthful correction.

When blocked, report the exact first prohibited action that would be required.

## Prohibited regardless of iteration count

```text
runner edit
production edit
manual checkpoint fabrication
manual final-ledger fabrication
manual promotion of failed provisional ledger
deleting mandatory ledger metadata to force serialization
acceptance weakening
old-hash PASS carry-forward
staging/commit/push
GitHub/Azure/Docker/GHCR mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

## Mutation accounting

At every terminal result require:

```text
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

Validator and disposable harness files are local governance/validation artifacts and must be accounted for separately from tracked production mutations.

## PASS terminal markers

Only after complete fresh structural success:

```text
RELEASE 1.12 WP04 — TERRA AUTONOMOUS VALIDATOR REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <FINAL_HASH>
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 RETAINED CHECKPOINT CARDINALITY: EXACTLY_ONE_EACH
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL ATOMIC LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 FINAL LEDGER REOPEN/PARSE: PASS
RELEASE 1.12 WP04 — R1 GITDIFFCHECKOUTPUT METADATA: PRESENT
RELEASE 1.12 WP04 — R1 RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 VALIDATOR HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## BLOCKED terminal markers

If a mandatory escalation boundary is reached:

```text
RELEASE 1.12 WP04 — TERRA AUTONOMOUS VALIDATOR REMEDIATION: BLOCKED
RELEASE 1.12 WP04 — FIRST BLOCKING DEFECT: <CLASSIFICATION>
RELEASE 1.12 WP04 — FIRST PROHIBITED REQUIRED ACTION: <ACTION>
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — PRODUCTION MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Required handoff

On PASS or BLOCKED return:

```text
RunnerSHA256
StartingValidatorSHA256
FinalValidatorSHA256
DiagnosticIterations
FreshStructuralRunAttempts
DefectsObserved
CorrectionsApplied
SanitizedDiagnosticEvidencePaths
FinalDurableRoot
FinalDisposableRoot
FinalCheckpointArtifactPath
FinalCheckpointCounts
LedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
GitDiffCheckExitCode
GitDiffCheckOutput
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
FinalLedgerReopenParseResult
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
FirstBlockingDefect
FirstProhibitedRequiredAction
ExactMutationAccounting
```

## Next gate

If PASS: STOP for a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**.

If BLOCKED: STOP and request a new authority scoped specifically to the demonstrated blocker.

W5 remains prohibited in both cases.

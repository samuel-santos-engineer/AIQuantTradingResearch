# Release 1.12 WP04 — Terra Post-BUILD_LEDGER / Pre-SERIALIZE Finalizer Remediation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns this validator-only finalizer diagnosis, correction, and fresh rerun. GPT-5.6 Luna retains the frozen Architecture-B contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Reconciled failure boundary

The fresh Path-B structural run durably reached, in order:

```text
1. PRE_CLEANUP
2. CLEANUP_COMPLETE
3. BUILD_LEDGER
```

It stopped before a durable:

```text
SERIALIZE
```

Therefore:

```text
Last accepted retained checkpoint = BUILD_LEDGER
First missing retained checkpoint = SERIALIZE
Final atomic ledger publication = NOT_REACHED
REOPEN = NOT_REACHED
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

The duplicate `BUILD_LEDGER` console text is diagnostic output from the checkpoint-writer pipeline and is **not** evidence of a duplicate accepted durable checkpoint record.

Classification:

```text
VALIDATOR_FINALIZER_FAILURE
Failure interval = AFTER BUILD_LEDGER / BEFORE SERIALIZE
Production defect = NO
Runner defect = NO
W5 defect = NO
```

No acceptance credit is granted to `SERIALIZE`, `PUBLISH`, or `REOPEN`.

## Mission

Use the retained failed-run evidence to diagnose the exact first failure after the durable `BUILD_LEDGER` boundary and before successful `SERIALIZE`.

Correct only the validator/finalizer as required.

Then:

```text
freeze corrected validator
→ compute fresh ValidatorSHA256
→ create fresh roots
→ restart complete SV01-SV36 from SV01
→ prove all six durable checkpoints
→ publish final atomic ledger
→ reopen/parse
→ terminal structural verification
```

Do not resume the failed run from `BUILD_LEDGER`.

Do not manually promote its provisional ledger.

## Failed-run evidence is diagnostic only

Preserve the failed run's durable evidence for diagnosis/audit.

It may prove where execution stopped, but it provides **zero carry-forward acceptance credit** to a corrected validator hash.

After any validator byte correction:

```text
old validator hash = historical
old run root = historical
old SV evidence = historical
old checkpoints = historical
old provisional ledger = historical
```

A complete fresh SV01-SV36 run is mandatory.

## First action — inspect exact retained failure evidence

Before editing:

1. identify the failed run's exact `DurableRoot`;
2. identify its exact validator SHA-256;
3. verify frozen runner SHA-256;
4. inspect the retained checkpoint artifact;
5. prove exactly one accepted retained record for each:
   - `PRE_CLEANUP`
   - `CLEANUP_COMPLETE`
   - `BUILD_LEDGER`;
6. prove no accepted retained record for:
   - `SERIALIZE`
   - `PUBLISH`
   - `REOPEN`;
7. inspect the provisional/in-memory-ledger evidence retained at `BUILD_LEDGER`;
8. inspect sanitized stderr/stdout/failure evidence;
9. inspect the exact current validator source between ledger build and serialization;
10. identify the first failing operation.

Retain a diagnosis record:

```text
FailedRunDurableRoot
FailedValidatorSHA256
RunnerSHA256
LastAcceptedCheckpoint
FirstMissingCheckpoint
DuplicateConsoleBuildLedgerClassification
FirstFailingOperation
FailureType
ObservedFailure
RootCause
CorrectionRequired
```

## Duplicate console diagnostic rule

Do not infer a duplicate durable checkpoint from repeated console text.

Determine checkpoint cardinality from the retained checkpoint artifact itself.

Required failed-run interpretation:

```text
Accepted PRE_CLEANUP records = 1
Accepted CLEANUP_COMPLETE records = 1
Accepted BUILD_LEDGER records = 1
```

If the retained artifact contradicts this, stop and report the actual durable record count rather than relying on console output.

## SERIALIZE semantic contract

The corrected validator must not persist `SERIALIZE` until all are true:

```text
final ledger object already exists
mandatory metadata is populated
temporary ledger path is under the same DurableRoot directory/filesystem boundary
serialization completed
temporary-file write completed
file handle flushed/closed
temporary file exists
temporary JSON independently reopens/parses
```

Only then:

```text
Write-Checkpoint SERIALIZE
```

If any pre-`SERIALIZE` operation fails:

```text
no SERIALIZE checkpoint
no PUBLISH checkpoint
no REOPEN checkpoint
no PASS
retain sanitized failure evidence
non-success termination
```

## Source-accurate diagnosis and correction

Inspect the actual current source; do not assume the cause.

Potential validator-only defect classes include, but are not limited to:

```text
serialization exception
object/metadata shape defect
JSON depth/type defect
temporary-path construction defect
directory/path state defect
WinPS 5.1 incompatibility
file-write/flush/close defect
temporary-file parse-check defect
checkpoint ordering defect
local finalizer control-flow defect
```

Do not preselect one without evidence.

Apply the narrowest source-accurate correction.

Do not alter the frozen acceptance contract merely to bypass the failing operation.

## Windows PowerShell baseline

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Require after correction:

```text
validator parser errors = 0
runner parser errors = 0
```

No PowerShell 7-only syntax/cmdlets or unsupported newer .NET assumptions.

## Frozen runner

Require exact:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

before and after remediation.

Runner mismatch:

```text
STOP
RUNNER_IDENTITY_MISMATCH
```

No runner edit is authorized.

## Fresh validator / rerun rule

If validator bytes change:

1. freeze corrected bytes;
2. compute fresh `ValidatorSHA256`;
3. retain exact validator;
4. create fresh `DisposableRoot`;
5. create fresh `DurableRoot`;
6. execute SV01-SV36 from SV01.

Any subsequent validator byte change repeats this rule.

Cross-hash PASS carry-forward is forbidden.

## Full terminal sequence

The fresh corrected run must durably prove exactly the successful semantic sequence:

```text
PRE_CLEANUP
→ CLEANUP_COMPLETE
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Require one accepted durable record per checkpoint.

Diagnostic console duplication does not affect acceptance if durable cardinality is exactly one per successful checkpoint.

## PUBLISH contract

After `SERIALIZE`, require a WinPS 5.1-compatible same-filesystem atomic publication of the temporary ledger to:

```text
sv01-sv36-ledger.json
```

Persist `PUBLISH` only after:

```text
publication succeeds
final ledger exists
```

## REOPEN contract

Persist `REOPEN` only after the published final ledger:

```text
reopens from disk
parses successfully
contains required final metadata
reverifies all terminal invariants
```

## Terminal invariants

Before Terra PASS require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0

PRE_CLEANUP = exactly 1 accepted retained record
CLEANUP_COMPLETE = exactly 1 accepted retained record
BUILD_LEDGER = exactly 1 accepted retained record
SERIALIZE = exactly 1 accepted retained record
PUBLISH = exactly 1 accepted retained record
REOPEN = exactly 1 accepted retained record

final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput metadata is present
GitDiffCheckExitCode = 0

DisposableRoot absent
DurableRoot survives
runner survives
validator survives
checkpoint artifact survives
ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup = exact frozen runner hash
ValidatorHashAfterCleanup = exact fresh validator hash

staged paths = 0
authority-introduced tracked repository mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Iterative validator-only remediation

Terra may correct ordinary validator/finalizer defects discovered while resolving this boundary without requesting new authority.

Every validator byte change requires:

```text
fresh validator hash
fresh roots
complete SV01-SV36 restart
```

Authorized classes include:

```text
VALIDATOR_FINALIZER_FAILURE
SERIALIZATION_DEFECT
LEDGER_OBJECT_DEFECT
LEDGER_METADATA_DEFECT
TEMPORARY_PATH_DEFECT
FILE_WRITE_DEFECT
JSON_PARSE_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
CHECKPOINT_ORDERING_DEFECT
ATOMIC_PUBLICATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

## Mandatory escalation

Stop only if resolution requires:

```text
runner change
tracked production-source change
Architecture-B change
P01-P20/SV01-SV36/G01-G13 change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/Docker/GHCR/GitHub mutation
other external/production mutation
```

## Prohibited

```text
resuming failed run from BUILD_LEDGER for acceptance
manually adding SERIALIZE/PUBLISH/REOPEN records
manual final-ledger fabrication
manual promotion of provisional ledger
treating console duplication as durable duplication without inspecting artifact
old-hash PASS carry-forward
runner edit
production edit
staging/commit/push
GitHub/Azure/Docker/GHCR mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

## Mutation accounting

Require:

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

## Required PASS markers

Only after a corrected fresh run reaches terminal verification:

```text
RELEASE 1.12 WP04 — TERRA POST-BUILD_LEDGER SERIALIZE-BOUNDARY REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 RETAINED CHECKPOINT CARDINALITY: EXACTLY_ONE_EACH
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 SERIALIZE BOUNDARY: PASS
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

## Required handoff

Return:

```text
FailedRunDurableRoot
FailedValidatorSHA256
RunnerSHA256
FailedRunCheckpointArtifactPath
FailedRunAcceptedCheckpointCounts
FirstFailingOperation
FailureType
RootCause
CorrectionSummary
FreshValidatorSHA256
FreshDurableRoot
FreshDisposableRoot
FreshCheckpointArtifactPath
FreshCheckpointSequenceObserved
FreshCheckpointAcceptedCounts
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
ExactMutationAccounting
```

## Stop and next gate

After terminal PASS, STOP.

The next artifact is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner SHA-256 and fresh corrected validator SHA-256.

W5 remains prohibited until Luna PASS.

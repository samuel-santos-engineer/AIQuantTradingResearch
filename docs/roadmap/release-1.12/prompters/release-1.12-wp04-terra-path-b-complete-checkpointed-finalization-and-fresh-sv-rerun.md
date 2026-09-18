# Release 1.12 WP04 — Terra Path-B Complete Checkpointed Finalization & Fresh SV Rerun

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns completion and execution of the already-authorized Path-B validator-only remediation. GPT-5.6 Luna retains contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Current state

Path-B validator correction is in progress.

Frozen runner:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

The validator has changed and therefore:

```text
previous validator hashes = historical only
fresh ValidatorSHA256 = REQUIRED
complete SV01-SV36 restart = REQUIRED
prior evidence acceptance carry-forward = FORBIDDEN
```

New fail-closed retained finalization checkpoints have been added:

```text
PRE_CLEANUP
→ CLEANUP_COMPLETE
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Reported:

```text
runner bytes changed = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Mission

Finish the Path-B validator correction, freeze its bytes, compute its fresh SHA-256, and execute one complete fresh Architecture-B SV01-SV36 validation from SV01 through final atomic ledger reopen/verification.

Do not stop after validator editing, checkpoint creation, provisional evidence generation, serialization, or publication.

This authority is terminal only at:

```text
PASS with final durable package
```

or a mandatory escalation boundary with retained evidence of the first real blocker.

## Checkpoint semantics

The retained checkpoint sequence must be monotonic and fail closed:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

For the successful run, prove all six in order.

Each checkpoint must retain at least:

```text
Checkpoint
TimestampUtc
RunnerSHA256
ValidatorSHA256
DurableRoot
Status
EvidenceReference
```

Checkpoint meaning:

```text
PRE_CLEANUP
= all pre-cleanup structural evidence required for finalization is durably persisted.

CLEANUP_COMPLETE
= disposable root cleanup completed and absence is proven while durable root remains.

BUILD_LEDGER
= final in-memory ledger object was built from the fresh run's own evidence.

SERIALIZE
= complete final ledger serialized to a same-durable-directory temporary file and closed/flushed.

PUBLISH
= temporary ledger atomically published as final sv01-sv36-ledger.json.

REOPEN
= final ledger reopened, parsed, and terminal invariants reverified.
```

A later checkpoint may not exist unless all earlier checkpoints succeeded.

Any exception/failure between checkpoints must retain the last completed checkpoint plus sanitized failure evidence and must not emit PASS.

## Finalizer correction gate

Before running SV01:

1. complete all intended validator edits;
2. parse validator under Windows PowerShell 5.1;
3. require parser errors = 0;
4. freeze validator bytes;
5. compute fresh ValidatorSHA256;
6. retain exact validator bytes under the new durable run;
7. do not edit validator after hash freeze.

Any validator byte change after freeze requires:

```text
new hash
fresh roots
complete restart from SV01
```

## Frozen runner gate

Before and after execution require exact:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Runner mismatch is a mandatory stop. No runner edit is authorized.

## Fresh-run rule

Create fresh:

```text
DisposableRoot
DurableRoot
```

All acceptance-bearing artifacts must originate from the new validator hash and this fresh run.

Do not reuse for acceptance:

```text
old SV evidence
old manifests
old resolution reports
old provisional ledgers
old checkpoints
old finalization evidence
old PASS records
```

Historical material may remain for audit only.

## Full structural run

Execute the canonical frozen Architecture-B validation:

```text
P01-P20 unchanged
SV01-SV36 unchanged
G01-G13 unchanged
G01-G11 < G12 < G13
runtime P01-P20 PASS claims during structural validation = 0
P19 validator-owned structural finalization
P20 observation-only
```

Structural validation must not execute G12/G13:

```text
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Finalization sequence

After SV01-SV36 pre-cleanup checks complete:

### PRE_CLEANUP

Persist all required fresh-run evidence durably, including:

```text
36 per-SV evidence artifacts
evidence manifest
pre-cleanup resolution material
runner
validator
hash/parser evidence
Git evidence available at this stage
mutation evidence
provisional ledger/checkpoint state
```

Then retain `PRE_CLEANUP`.

### CLEANUP_COMPLETE

Clean only DisposableRoot.

Prove:

```text
DisposableRoot exists = False
DurableRoot exists = True
runner exists = True
validator exists = True
retained evidence exists = True
```

Retain `CLEANUP_COMPLETE`.

### BUILD_LEDGER

Perform final post-cleanup resolution and final observations.

Require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
```

Build the complete final ledger object including all mandatory metadata.

Retain `BUILD_LEDGER`.

### SERIALIZE

Serialize the complete final ledger to a temporary file in the same durable directory as the target final ledger.

Require:

```text
serialization completed
file handle closed
temporary file exists
temporary JSON parses successfully
```

Retain `SERIALIZE`.

### PUBLISH

Atomically publish the temporary file to:

```text
sv01-sv36-ledger.json
```

Use a WinPS 5.1-compatible same-filesystem publication mechanism.

Require:

```text
publication operation succeeded
final file exists
```

Retain `PUBLISH`.

### REOPEN

Reopen the final file from disk and parse it independently.

Reverify:

```text
RunnerSHA256
ValidatorSHA256
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
GovernedW5WrapperInvoked = false
GovernedW5RunIdAllocated = false
GitDiffCheckOutput property present
GitDiffCheckExitCode = 0
DisposableExistsAfterCleanup = false
DurableExistsAfterCleanup = true
RunnerExistsAfterCleanup = true
ValidatorExistsAfterCleanup = true
LedgerExistsAfterCleanup = true
RunnerHashAfterCleanup = exact frozen runner hash
ValidatorHashAfterCleanup = exact fresh validator hash
```

Retain `REOPEN`.

Only then may PASS be emitted.

## GitDiffCheckOutput requirement

Under exact:

```text
Windows PowerShell 5.1.26100.9444
```

capture `git diff --check` operation-scoped.

Persist:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
```

`GitDiffCheckOutput` must be present even when empty.

CRLF advisories may be `ADVISORY_ONLY` only with exit code 0.

## Evidence references

After cleanup every one of the 36 `EvidenceReference` values must resolve to retained evidence and its claimed locator.

Retain:

```text
evidence manifest
evidence-reference resolution report
```

Require:

```text
resolution attempts = 36
resolved = 36
unresolved = 0
```

## Iterative correction permission

If an ordinary validator/finalizer defect is discovered, Terra may correct it and continue without a new authority.

But every validator-byte change requires:

```text
fresh ValidatorSHA256
fresh roots
full SV01-SV36 restart
```

Do not stop after an ordinary validator defect if it is locally correctable.

Authorized defect classes include:

```text
VALIDATOR_DEFECT
LEDGER_FINALIZATION_DEFECT
CHECKPOINT_DEFECT
ATOMIC_PUBLICATION_DEFECT
SERIALIZATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LEDGER_METADATA_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
GIT_CAPTURE_DEFECT
```

## Mandatory escalation only

Stop for governance/production escalation if correction requires:

```text
runner change
production-source change
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
manual final-ledger fabrication
manual promotion of provisional ledger outside validator control flow
prior-hash PASS carry-forward
runner edit
production edit
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
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

## Terminal PASS requirements

Require all:

```text
fresh validator hash frozen
WinPS 5.1.26100.9444
runner parser = 0
validator parser = 0
SV records = 36
SV failures = 0
runtime predicate PASS claims = 0
evidence references = 36
resolved = 36
unresolved = 0
PRE_CLEANUP = PASS
CLEANUP_COMPLETE = PASS
BUILD_LEDGER = PASS
SERIALIZE = PASS
PUBLISH = PASS
REOPEN = PASS
final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput present
git diff --check exit = 0
DisposableRoot absent
DurableRoot survives
runner/validator/ledger/manifest/resolution report survive
runner hash reverified
validator hash reverified
staged paths = 0
tracked mutations = 0
W5 invoked = NO
W5 RunId allocated = NO
```

## Required PASS markers

```text
RELEASE 1.12 WP04 — TERRA PATH-B CHECKPOINTED FINALIZATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINALIZATION PRE_CLEANUP: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION CLEANUP_COMPLETE: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION BUILD_LEDGER: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION SERIALIZE: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION PUBLISH: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION REOPEN: PASS
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
RunnerSHA256
FreshValidatorSHA256
ValidatorPath
DurableRoot
DisposableRoot
LedgerPath
ProvisionalLedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
CheckpointEvidencePath
CheckpointSequenceObserved
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
ManifestExistsAfterCleanup
ResolutionReportExistsAfterCleanup
FinalLedgerReopenParseResult
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
RuntimePredicatePassClaims
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
ExactMutationAccounting
```

## Stop and next gate

After terminal PASS, STOP.

The next artifact must be a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner hash and this fresh validator hash.

Do not execute W5 before Luna PASS.

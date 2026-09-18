# Release 1.12 WP04 — Terra Complete Remaining Four Durable Checkpoint Call Sites

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns completion of the already-authorized validator-only source-accurate checkpoint integration and the subsequent fresh structural run. GPT-5.6 Luna retains contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Reconciled current state

Source-accurate integration has partially succeeded:

```text
PRE_CLEANUP       = DURABLE CALL SITE INTEGRATED
CLEANUP_COMPLETE  = DURABLE CALL SITE INTEGRATED
BUILD_LEDGER      = NOT YET INTEGRATED
SERIALIZE         = NOT YET INTEGRATED
PUBLISH           = NOT YET INTEGRATED
REOPEN            = NOT YET INTEGRATED
```

`CLEANUP_COMPLETE` is correctly placed only after disposable-root absence is proven.

No fresh structural validation has started.

```text
Runner changed = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

Frozen runner identity remains:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Classification:

```text
VALIDATOR REMEDIATION = IN_PROGRESS
Completed durable call sites = 2/6
Remaining durable call sites = 4/6
Production defect = NO
Runner defect = NO
Fresh validation candidate = NONE
```

## Mission

Preserve the two completed call-site integrations and perform source-accurate full-line rewrites for exactly the four remaining semantic boundaries:

```text
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Then statically reconcile all six durable call sites, parse under Windows PowerShell 5.1, freeze validator bytes, compute a fresh validator SHA-256, create fresh roots, and execute the complete fresh SV01-SV36 structural run through terminal atomic-ledger reopen verification.

Do not start SV01-SV36 until all six call sites pass preflight.

Do not stop merely after completing the four rewrites.

## Preservation gate for completed call sites

Before editing, inspect and retain evidence that:

```text
PRE_CLEANUP
```

persists through `Write-Checkpoint` only after required pre-cleanup evidence is durably retained and before disposable cleanup.

Also prove:

```text
CLEANUP_COMPLETE
```

persists through `Write-Checkpoint` only after:

```text
DisposableRoot absent = proven
DurableRoot survives = proven
```

Do not rewrite either completed call site unless necessary to repair an independently demonstrated validator defect.

If either completed call site's bytes change, include it in semantic reconciliation and treat the final validator hash normally as fresh.

## Source-accurate rewrite protocol

For each remaining checkpoint:

1. inspect the exact current validator source;
2. locate the complete packed source line containing the semantic boundary;
3. capture the exact original full line;
4. enumerate every operation and control-flow effect on that line;
5. construct a complete source-accurate replacement preserving all original semantics;
6. insert the `Write-Checkpoint` call only after the checkpoint condition is true;
7. replace the exact current full-line source;
8. verify the intended replacement exists;
9. verify no operation was silently lost;
10. retain before/after reconciliation evidence.

Do not apply stale patch context.

Do not rewrite unrelated code.

Multi-line replacement is permitted when necessary for correctness and source maintainability, but semantic ordering must remain equivalent except for the authorized durable checkpoint insertion.

## BUILD_LEDGER placement

Persist `BUILD_LEDGER` only after all of:

```text
post-cleanup observations complete
final evidence-reference resolution complete
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
final in-memory ledger object built
mandatory final metadata populated
```

The checkpoint must not claim ledger construction before the final object actually exists.

## SERIALIZE placement

Persist `SERIALIZE` only after all of:

```text
final ledger serialized to a temporary file
temporary file is in the same durable directory/filesystem as final ledger
write completed
file handle flushed/closed
temporary file exists
temporary JSON reopens/parses successfully
```

Serialization failure must prevent this checkpoint and all later checkpoints.

## PUBLISH placement

Persist `PUBLISH` only after:

```text
atomic publication/replacement succeeds
final sv01-sv36-ledger.json exists
```

Publication failure must be terminating and must not permit `PUBLISH`, `REOPEN`, or terminal PASS.

## REOPEN placement

Persist `REOPEN` only after the published final ledger is independently reopened from disk, parsed, and its terminal invariants reverified.

At minimum reverify:

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
RunnerHashAfterCleanup = frozen runner hash
ValidatorHashAfterCleanup = fresh frozen validator hash
```

Only after those checks pass may `REOPEN` be retained.

## Six-call-site static reconciliation gate

Before validator freeze, require exactly:

```text
PRE_CLEANUP       = 1 durable successful-path call
CLEANUP_COMPLETE  = 1 durable successful-path call
BUILD_LEDGER      = 1 durable successful-path call
SERIALIZE         = 1 durable successful-path call
PUBLISH           = 1 durable successful-path call
REOPEN            = 1 durable successful-path call

Total = 6
Missing = 0
Duplicates = 0
Stdout-only substitutes = 0
```

Retain a six-record reconciliation containing:

```text
Checkpoint
CurrentSourceEvidence
SemanticBoundary
PreservedOperations
DurableWriteInvocation
Result
```

Require all six `Result = PASS`.

## Parser and runtime

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Require:

```text
validator parser errors = 0
runner parser errors = 0
```

No PowerShell 7-only syntax/cmdlets.

## Freeze and fresh hash

After all six call sites and parser gates pass:

1. freeze validator bytes;
2. compute fresh `ValidatorSHA256`;
3. retain the exact frozen validator;
4. bind execution to that hash;
5. do not edit validator bytes afterward.

Any later validator byte change requires:

```text
new ValidatorSHA256
new roots
SV01-SV36 restart from SV01
```

## Fresh-run gate

Only after six-call-site reconciliation and validator freeze:

```text
create fresh DisposableRoot
create fresh DurableRoot
execute SV01-SV36 from SV01
```

No prior validator hash/root/evidence contributes acceptance credit.

Preserve the frozen Architecture-B contract:

```text
P01-P20 unchanged
SV01-SV36 unchanged
G01-G13 unchanged
G01-G11 < G12 < G13
runtime P01-P20 PASS claims = 0
P19 = validator-owned structural finalization
P20 = observation-only
cross-hash PASS carry-forward = forbidden
```

Structural validation must not execute W5.

## Required durable checkpoint sequence

The fresh run must retain, in exact semantic order:

```text
PRE_CLEANUP
→ CLEANUP_COMPLETE
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Require six successful retained records and no missing checkpoint.

Stdout is diagnostic only.

## Terminal structural requirements

Before Terra PASS require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
all mandatory final metadata present
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
DisposableRoot absent
DurableRoot survives
runner survives
validator survives
ledger survives
manifest survives
resolution report survives
checkpoint artifact survives
runner hash reverified
validator hash reverified
staged paths = 0
authority-introduced tracked repository mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Iterative validator correction permission

Terra may correct ordinary validator-only defects encountered while completing or executing this authority.

Every validator-byte change after freeze requires a fresh hash and complete restart.

Authorized defect classes include:

```text
CHECKPOINT_CALLSITE_INTEGRATION_DEFECT
SOURCE_FORMATTING_DEFECT
PATCH_CONTEXT_DRIFT
CHECKPOINT_PERSISTENCE_DEFECT
VALIDATOR_DEFECT
LEDGER_FINALIZATION_DEFECT
SERIALIZATION_DEFECT
ATOMIC_PUBLICATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LEDGER_METADATA_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

Do not return for new authority for an ordinary validator-only defect that can be corrected without changing the frozen contract.

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
starting SV01-SV36 before 6/6 call-site preflight PASS
stale-context patching
dropping existing operations from packed lines
unrelated validator refactoring
stdout-only checkpoint acceptance
manual checkpoint fabrication
manual final-ledger fabrication
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

Only after the complete fresh run reaches terminal verification:

```text
RELEASE 1.12 WP04 — TERRA REMAINING FOUR CHECKPOINT CALLSITES: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 CHECKPOINT CALLSITES: 6/6
RELEASE 1.12 WP04 — R1 CHECKPOINT SEMANTIC RECONCILIATION: 6/6 PASS
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
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

## Required handoff

Return:

```text
RunnerSHA256
PreFreezeValidatorSHA256
FreshValidatorSHA256
CheckpointCallsiteCount
CheckpointCallsiteMissing
CheckpointCallsiteDuplicates
CheckpointSemanticReconciliationPassCount
CallsiteReconciliationEvidencePath
ValidatorParserErrors
RunnerParserErrors
DurableRoot
DisposableRoot
CheckpointArtifactPath
CheckpointSequenceObserved
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

The next authority is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner SHA-256 and fresh validator SHA-256.

W5 remains prohibited until Luna PASS.

# Release 1.12 WP04 — Terra Source-Accurate Six-Checkpoint Call-Site Rewrite & Terminal Path-B Run

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns this validator-only source-accurate call-site remediation and subsequent fresh structural execution. GPT-5.6 Luna retains contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Current boundary

The actual validator finalizer has been located and a durable `Write-Checkpoint` implementation has been added.

The remaining defect is that the six existing checkpoint call sites are embedded in long single-line statements and require source-accurate full-line rewriting.

No fresh validation run has started.

```text
Runner changed = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
Validator complete = NO
```

Required durable checkpoints:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Classification:

```text
VALIDATOR_DEFECT
CHECKPOINT_CALLSITE_INTEGRATION_DEFECT
SOURCE_FORMATTING / PATCH_CONTEXT_CONSTRAINT
Production defect = NO
Runner defect = NO
```

## Mission

Inspect the exact current validator source and rewrite the six actual full-line call-site statements so each checkpoint is persisted through the new durable `Write-Checkpoint` implementation at the correct semantic boundary.

Do not use stale snippets or approximate textual replacements.

After all six call sites are integrated:

```text
parse
→ freeze validator
→ compute fresh ValidatorSHA256
→ fresh roots
→ complete fresh SV01-SV36
→ complete all six retained checkpoints
→ publish final atomic ledger
→ reopen/verify
→ terminal PASS or first real escalation blocker
```

Do not stop after editing.

## Source-accurate rewrite protocol

For each checkpoint:

1. locate the exact current source line containing the semantic boundary;
2. capture the complete original line exactly;
3. understand all statements and control-flow effects packed into that line;
4. construct a complete replacement line/block preserving every pre-existing semantic operation;
5. add exactly the required `Write-Checkpoint` invocation at the correct point;
6. replace using exact current-source matching;
7. immediately verify the old full line no longer exists and the intended replacement does;
8. parse the complete validator under Windows PowerShell 5.1 after each logical rewrite or after the safe atomic edit set;
9. retain a sanitized before/after call-site reconciliation artifact.

Do not normalize or refactor unrelated code merely for readability.

A multi-line replacement is permitted if required for correctness, provided semantics are preserved exactly except for the authorized checkpoint persistence.

## Six semantic placements

### PRE_CLEANUP

Persist only after all required pre-cleanup evidence is durably written and immediately before disposable cleanup begins.

### CLEANUP_COMPLETE

Persist only after:

```text
DisposableRoot absent = proven
DurableRoot survives = proven
```

### BUILD_LEDGER

Persist only after:

```text
post-cleanup observations complete
evidence resolution complete
36 resolved / 0 unresolved
final ledger object built
```

### SERIALIZE

Persist only after:

```text
final ledger serialized to same-durable-directory temporary file
write/flush/close completed
temporary file exists
temporary JSON parse-check succeeds
```

### PUBLISH

Persist only after:

```text
atomic publication to sv01-sv36-ledger.json succeeds
final ledger path exists
```

### REOPEN

Persist only after:

```text
final ledger reopened from disk
JSON parse succeeds
terminal invariants reverified
```

No checkpoint may be emitted before its condition is true.

## Checkpoint invocation contract

Each call must persist through the validator's new `Write-Checkpoint` implementation.

The retained record must contain at least:

```text
Checkpoint
TimestampUtc
RunnerSHA256
ValidatorSHA256
DurableRoot
Status
EvidenceReference
```

Stdout-only messages are diagnostic and do not satisfy the contract.

Checkpoint persistence failure must be terminating/fail closed.

## Call-site completeness preflight

Before freezing validator bytes, statically verify exactly one successful-path durable call site for each:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Require:

```text
required checkpoint names = 6
durable successful-path call sites = 6
missing = 0
duplicates = 0
stdout-only substitutes = 0
```

Retain this preflight evidence.

## Semantic-preservation preflight

For every rewritten packed line, prove:

```text
all original non-checkpoint operations preserved
original ordering preserved unless checkpoint insertion requires a precise split
original error handling preserved
original fail-closed behavior preserved
no acceptance condition weakened
no finalization stage skipped
```

Retain a six-record reconciliation:

```text
Checkpoint
OriginalSourceEvidence
ReplacementSourceEvidence
PreservedOperations
InsertedCheckpointPosition
SemanticPreservationResult
```

All six must PASS.

## Windows PowerShell

Binding target:

```text
Windows PowerShell 5.1.26100.9444
```

Require validator parser errors = 0.

Do not use PowerShell 7-only syntax/cmdlets.

## Frozen runner

Require before and after:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Any mismatch is a mandatory stop. No runner correction is authorized.

## Fresh validator identity

Once all six call sites and finalizer implementation are complete:

```text
freeze validator bytes
compute FreshValidatorSHA256
```

Retain exact frozen validator bytes.

Any later validator-byte change requires:

```text
new ValidatorSHA256
fresh roots
full SV01-SV36 restart
```

## Fresh full run

Create fresh DurableRoot and DisposableRoot and execute canonical Architecture-B SV01-SV36 from SV01.

No prior validator/hash/root evidence contributes acceptance credit.

Preserve:

```text
P01-P20 unchanged
SV01-SV36 unchanged
G01-G13 unchanged
runtime P01-P20 PASS claims = 0
P19 validator-owned structural finalization
P20 observation-only
cross-hash PASS carry-forward = forbidden
```

No governed W5 execution or RunId allocation.

## Required terminal sequence

The fresh run must durably prove:

```text
PRE_CLEANUP
→ CLEANUP_COMPLETE
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Then require:

```text
SV records = 36
SV failures = 0
EvidenceReference count = 36
Resolved = 36
Unresolved = 0
final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput property present
git diff --check exit = 0
DisposableRoot absent
DurableRoot survives
runner survives
validator survives
ledger survives
manifest survives
resolution report survives
runner hash reverified
validator hash reverified
staged paths = 0
authority-introduced tracked mutations = 0
W5 invoked = NO
W5 RunId allocated = NO
```

## Iterative correction permission

Terra may correct ordinary validator-only defects discovered during this execution without another authority.

Every validator byte change requires a fresh validator hash and complete restart.

Authorized classes include:

```text
CHECKPOINT_CALLSITE_INTEGRATION_DEFECT
SOURCE_FORMATTING_DEFECT
PATCH_CONTEXT_DRIFT
VALIDATOR_DEFECT
CHECKPOINT_PERSISTENCE_DEFECT
LEDGER_FINALIZATION_DEFECT
SERIALIZATION_DEFECT
ATOMIC_PUBLICATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LEDGER_METADATA_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

Do not stop at an ordinary locally correctable validator defect.

## Mandatory escalation

Stop only if correction requires:

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
partial/stale-context patching
dropping operations from packed source lines
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

Only after terminal fresh-run verification:

```text
RELEASE 1.12 WP04 — TERRA SIX-CHECKPOINT CALLSITE REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 CHECKPOINT CALLSITES: 6/6
RELEASE 1.12 WP04 — R1 CHECKPOINT SEMANTIC PRESERVATION: 6/6 PASS
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
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
PreEditValidatorSHA256
FreshValidatorSHA256
RunnerSHA256
CallsiteReconciliationEvidencePath
CheckpointCallsiteCount
CheckpointCallsiteMissing
CheckpointCallsiteDuplicates
CheckpointSemanticPreservationPassCount
ValidatorParserErrors
DurableRoot
DisposableRoot
CheckpointArtifactPath
CheckpointSequenceObserved
LedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
SVRecordCount
SVFailedCount
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

After PASS, STOP.

The next artifact is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner hash and fresh validator hash.

W5 remains prohibited until Luna PASS.

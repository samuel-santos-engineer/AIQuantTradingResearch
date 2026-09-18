# Release 1.12 WP04 — Terra Path-B Validator Finalizer Correction & Full Rerun

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns the validator-only finalizer correction and complete Architecture-B structural rerun authorized here. GPT-5.6 Luna retains contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Trigger and binding diagnosis

Path A has been exhausted.

Frozen runner:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Path-A validator:

```text
ValidatorSHA256 =
6B3B988C8D8A90C2875935567AF99FEF7AF3601C55CB6F8252B2A6ECF4B13193
```

Newest failed Path-A durable root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\a6fdb5fffc15439cb59d2aad266e3f1f
```

Observed repeatedly:

```text
SV01-SV36 evidence = PRESENT
manifest = PRESENT
resolution report = PRESENT
runner = PRESENT
validator = PRESENT
provisional ledger = PRESENT
final atomic sv01-sv36-ledger.json = ABSENT
```

Therefore:

```text
PATH A = EXHAUSTED
PATH B = REQUIRED
Classification = VALIDATOR_DEFECT / LEDGER_FINALIZATION_DEFECT
Production defect = NO
Runner defect = NO
Runner correction required = NO
Validator correction required = YES
Fresh RunnerSHA256 required = NO
Fresh ValidatorSHA256 required = YES
```

No W5 wrapper was invoked and no W5 RunId was allocated.

## Authority boundary

```text
RUNNER BYTES FROZEN
VALIDATOR-ONLY CORRECTION AUTHORIZED
FRESH VALIDATOR HASH REQUIRED
FRESH DURABLE/DISPOSABLE ROOTS REQUIRED
FULL SV01-SV36 RESTART REQUIRED
NO PATH-A PASS CARRY-FORWARD
NO MANUAL FINAL-LEDGER SYNTHESIS
NO PRODUCTION MUTATIONS
NO W5
```

The Path-A validator hash `6B3B988C...B13193` is historical only after any byte correction.

## Mission

Correct the actual validator finalization defect so a fresh execution reaches terminal atomic publication of:

```text
sv01-sv36-ledger.json
```

The correction must make final publication an executable part of the validator's own fail-closed control flow.

Then freeze the corrected validator, compute a fresh validator SHA-256, and rerun the entire Architecture-B structural validation from SV01.

Do not reuse Path-A per-SV evidence, manifest, resolution report, provisional ledger, or PASS observations for acceptance credit.

## Phase 1 — inspect the finalization failure

Before editing, inspect the current validator and newest retained Path-A evidence to determine the concrete point at which execution stops between provisional state and final atomic publication.

Retain a sanitized diagnosis containing:

```text
FailedValidatorSHA256
FailureStage
LastCompletedFinalizationStep
FirstUncompletedFinalizationStep
ObservedFailureOrTermination
RootCause
CorrectionScope
ProductionDefect = NO
RunnerDefect = NO
```

Do not change contract semantics to work around the failure.

## Phase 2 — narrow validator-only correction

Modify only the disposable/local validator implementation necessary to make finalization complete reliably under Windows PowerShell 5.1.

The corrected finalizer must execute, in fail-closed order:

```text
1. complete all SV01-SV36 structural checks
2. persist all per-SV retained evidence
3. persist evidence manifest
4. persist evidence-resolution report
5. persist provisional ledger/checkpoint
6. clean disposable root
7. prove disposable root absent
8. prove durable root survives
9. prove runner/validator/evidence artifacts survive
10. reverify runner hash
11. reverify validator hash
12. finalize P19 observations
13. perform final post-cleanup EvidenceReference resolution
14. require 36 resolved / 0 unresolved
15. capture/persist mandatory final metadata
16. build final ledger object
17. serialize final ledger to a temporary durable file
18. flush/close the temporary file
19. atomically publish/replace final `sv01-sv36-ledger.json`
20. prove final file exists
21. reopen final file
22. parse final file
23. reverify exact counts/hashes/metadata
24. emit PASS markers
```

If any step fails:

```text
do not emit PASS
retain sanitized failure evidence
return non-success
```

## Atomic publication contract

The validator must own the final publication operation.

Use a same-durable-directory temporary file and a Windows PowerShell 5.1-compatible atomic publication mechanism.

Requirements:

```text
temporary and final ledger reside on same filesystem/directory boundary
serialization completes before publication
file handle is closed before publication
publication failure is terminating/fail-closed
final ledger existence is explicitly verified
final ledger is reopened and parsed
temporary file is cleaned where safe
```

Do not manually create the final ledger outside validator execution.

Do not weaken atomicity merely to obtain a file.

## Windows PowerShell 5.1

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Avoid PowerShell 7-only syntax/cmdlets and unsupported newer .NET assumptions.

Parser errors must equal 0 before any fresh governed structural run.

## Frozen runner gate

Before and after all work require:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

If runner bytes change:

```text
STOP
RUNNER_IDENTITY_MISMATCH
```

No runner remediation is authorized.

## Fresh validator identity

After correction and before structural execution:

```text
freeze validator bytes
compute ValidatorSHA256
```

Require:

```text
FreshValidatorSHA256 !=
6B3B988C8D8A90C2875935567AF99FEF7AF3601C55CB6F8252B2A6ECF4B13193
```

Any subsequent validator-byte change requires another fresh hash and complete restart.

## Fresh-run rule

Create fresh roots and execute SV01-SV36 from the beginning.

Forbidden acceptance carry-forward:

```text
old SV records
old per-SV evidence
old manifest
old resolution report
old provisional ledger
old finalization observations
old PASS markers
```

Historical roots remain audit evidence only.

## Canonical structural requirements

Preserve without reinterpretation:

```text
Architecture B = FROZEN_RUNNER_PLUS_INDEPENDENT_VALIDATOR
P01-P20 = unchanged
SV01-SV36 = unchanged
G01-G13 = unchanged
G01-G11 < G12 < G13
runtime P01-P20 PASS claims during structural validation = 0
P19 structural finalization = validator-owned
P20 structural verification = observation-only
cross-hash PASS carry-forward = forbidden
```

## Evidence-reference terminal requirement

Fresh final package must prove:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
```

Every reference must resolve after disposable cleanup to retained evidence and a concrete locator.

Retain the manifest and resolution report.

## Mandatory final ledger metadata

Final `sv01-sv36-ledger.json` must persist at least:

```text
RecordType
RunnerSHA256
EvidenceWriterOrValidatorSHA256
PowerShellVersion
SVRecordCount
SVFailedCount
FirstFailedSV
RuntimePredicatePassClaims
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
DisposableRoot
DurableRoot
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
ExactMutationAccounting
```

`GitDiffCheckOutput` must exist even if empty.

Each SV record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

## Git capture

Under WinPS 5.1 capture `git diff --check` operation-scoped:

```text
capture output
capture immediate LASTEXITCODE
persist both
```

CRLF advisories are `ADVISORY_ONLY` only when exit code is 0.

Require staged paths = 0.

## Iteration permission

Terra may continue correcting ordinary validator-only defects found during Path B without returning for a new authority.

Every validator-byte change requires:

```text
new ValidatorSHA256
fresh roots
SV01-SV36 restart
```

Permitted classifications include:

```text
VALIDATOR_DEFECT
LEDGER_FINALIZATION_DEFECT
ATOMIC_PUBLICATION_DEFECT
EVIDENCE_REFERENCE_DEFECT
EVIDENCE_MANIFEST_DEFECT
LEDGER_METADATA_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
DISPOSABLE_EVIDENCE_LIFECYCLE_DEFECT
HASH_BINDING_DEFECT
GIT_CAPTURE_DEFECT
```

Do not stop at the first ordinary validator defect. Correct and restart until terminal PASS or mandatory escalation.

## Mandatory escalation

STOP only if resolution requires:

```text
runner change
production-source change
Architecture-B change
P01-P20/SV01-SV36/G01-G13 contract change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/Docker/GHCR/GitHub mutation
other production/external mutation
```

## Prohibited

```text
W5 execution
W5 RunId allocation
runner edit
production edit
manual patching of final ledger
manual conversion of provisional ledger into acceptance ledger
cross-hash evidence carry-forward
staging
commit
push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data secret configuration
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

## Terminal PASS gate

Only emit PASS after a fresh validator run proves all of:

```text
runner exact hash = PASS
fresh validator hash = PASS
WinPS exact version = PASS
runner parser = 0
validator parser = 0
SV unique records = 36
SV failed records = 0
runtime predicate PASS claims = 0
evidence references = 36
resolved = 36
unresolved = 0
manifest retained = PASS
resolution report retained = PASS
provisional ledger retained as applicable = PASS
disposable root absent = PASS
durable root survives = PASS
runner survives = PASS
validator survives = PASS
final ledger exists = PASS
final ledger reopen/parse = PASS
final ledger metadata complete = PASS
GitDiffCheckOutput present = PASS
git diff --check exit = 0
runner post-cleanup hash = exact
validator post-cleanup hash = exact
staged paths = 0
tracked mutation count = 0
W5 invoked = NO
W5 RunId = NO
```

## Required PASS markers

```text
RELEASE 1.12 WP04 — TERRA PATH-B VALIDATOR FINALIZER CORRECTION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL ATOMIC LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 FINAL LEDGER REOPEN/PARSE: PASS
RELEASE 1.12 WP04 — R1 GITDIFFCHECKOUTPUT METADATA: PRESENT
RELEASE 1.12 WP04 — R1 POST-CLEANUP FINALIZATION: PASS
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
Path = B_FRESH_VALIDATOR
PathAValidatorSHA256
PathBValidatorSHA256
RunnerSHA256
FailureDiagnosis
CorrectionSummary
ValidatorPath
DurableRoot
LedgerPath
ProvisionalLedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
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

The next artifact is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner hash and the new Path-B validator hash.

Do not create or execute W5 authority before Luna PASS.

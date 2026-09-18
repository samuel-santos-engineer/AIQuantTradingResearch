# Release 1.12 WP04 — Terra Architecture-B Evidence-Reference & Finalization-Metadata Remediation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the narrow local/disposable validator remediation and complete structural revalidation authorized here.

GPT-5.6 Luna owns the frozen Architecture-B contract and the subsequent independent reconciliation.

GPT-5.6 Sol is supporting analysis only and must not replace Luna or Terra.

## Trigger

Independent Luna reconciliation of the first Architecture-B candidate failed.

Frozen runner:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Failed validator:

```text
ValidatorSHA256 =
663353B91D10F473DCA59E95C2831BC0CE556F6C8D700173D55EE34D7508889E
```

Failed durable ledger:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\8f080befe3fd40f09e3010791dbc47f9\sv01-sv36-ledger.json
```

Luna independently found:

```text
ledger records = 36
recorded failures = 0
unresolved EvidenceReference values = 34
required GitDiffCheckOutput metadata = MISSING
git diff --check exit = 0
git diff --check output = CRLF advisories only

R01-R19 = PASS
R20 = FAIL
R21-R23 = NOT_PROVEN
R24-R30 = PASS
```

Classification:

```text
VALIDATOR_DEFECT / EVIDENCE_REFERENCE_DEFECT
Production defect = NO
Runner correction required = NO
Validator correction required = YES
Fresh RunnerSHA256 required = NO
Fresh ValidatorSHA256 required = YES
```

## Authority boundary

```text
RUNNER BYTES MUST REMAIN UNCHANGED
TRACKED PRODUCTION SOURCE MUST REMAIN UNCHANGED
VALIDATOR MAY BE CORRECTED
FRESH VALIDATOR HASH REQUIRED
FULL SV01-SV36 RERUN REQUIRED
NO CROSS-HASH PASS CARRY-FORWARD
NO GOVERNED W5 EXECUTION
NO GOVERNED W5 RUNID
```

The failed validator hash is historical evidence only and grants no acceptance credit.

## Mission

Correct the disposable Architecture-B validator so that:

1. every acceptance-bearing `EvidenceReference` resolves to a retained artifact or a retained artifact plus an explicit resolvable locator;
2. every referenced artifact survives disposable cleanup;
3. the final ledger persists all mandatory finalization metadata, including `GitDiffCheckOutput`;
4. evidence references remain independently inspectable after cleanup;
5. the exact frozen runner remains unchanged;
6. a fresh validator hash is computed;
7. the entire SV01-SV36 sequence is rerun from SV01 against the unchanged runner and fresh validator;
8. the final durable package is self-sufficient for subsequent Luna reconciliation.

Do not merely patch the existing JSON ledger.

The validator implementation must be corrected and the complete validation rerun must generate a new durable package.

## Frozen runner gate

Before any validator mutation:

```text
compute retained/source RunnerSHA256
require exact equality to:
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

After all work and after disposable cleanup, recompute it again.

If runner bytes differ at any point:

```text
STOP
CLASSIFICATION = RUNNER_IDENTITY_MISMATCH
```

Do not repair or alter the runner under this authority.

## Evidence-reference contract

Every SV01-SV36 record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Every acceptance-bearing `EvidenceReference` must resolve after cleanup.

A valid reference must identify retained evidence unambiguously using one of these forms:

```text
1. absolute durable artifact path; or
2. durable-root-relative artifact path with a top-level DurableRoot; or
3. retained artifact path plus explicit structured locator such as:
   file + JSON property
   file + record ID
   file + line/range
   file + named section
```

The final package must allow Luna to follow each reference without relying on:

```text
DisposableRoot
interactive session state
temporary variables
deleted files
unretained console output
implicit knowledge
historical validator iterations
```

Generic labels such as:

```text
runner source
validator source
ledger
hash evidence
negative test
see above
```

are insufficient unless they are mapped to a concrete retained artifact and locator.

## Evidence-reference resolution proof

The corrected validator must perform a final independent resolution pass over all 36 records after cleanup.

For every record, retain a resolution result equivalent to:

```text
CheckId
EvidenceReference
ResolvedArtifact
ResolvedLocator
ArtifactExists
LocatorResolved
ResolutionResult
```

Acceptance requires:

```text
SV records = 36
EvidenceReference resolution attempts = 36
unresolved EvidenceReference values = 0
```

If one reference does not resolve, structural validation fails closed.

## Retained evidence manifest

Create and retain a durable evidence manifest that maps stable evidence identifiers to concrete artifacts.

At minimum:

```text
EvidenceId
ArtifactPath
ArtifactSHA256
ArtifactType
Purpose
RetainedAfterCleanup
```

The manifest itself must survive cleanup and be referenced by the final ledger metadata.

Do not use the manifest to hide missing evidence: each mapped artifact must actually exist and its hash must verify.

## Evidence quality

Preserve the frozen Luna requirement:

```text
Observed = substantive independently inspectable observation
EvidenceReference = resolvable retained evidence/source reference
ValidationMethod = concrete method used
```

Do not replace unresolved references with generic file paths that do not contain the claimed evidence.

For source/order/hash/lifecycle/negative-test checks, the retained artifact and locator must expose the observation Luna needs to inspect.

## Required retained artifacts

Retain, as applicable to the validator's actual methods:

```text
exact frozen runner bytes
exact fresh validator bytes
runner hash evidence
validator hash evidence
PowerShell version evidence
runner parser evidence
validator parser evidence
canonical P01-P20 extraction/mapping evidence
predicate-schema evidence
aggregation/fail-closed evidence
G01-G13/order evidence
exact-byte copy/hash evidence
negative-test evidence
P03/P04 binding evidence
P19 finalization evidence
P20 observation-only evidence
Git/repository evidence
secret-hygiene evidence
mutation-accounting evidence
evidence manifest
evidence-reference resolution report
complete SV01-SV36 ledger
iteration history
sanitized terminal summary
```

## Mandatory final ledger metadata

The new final ledger must persist at least:

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

`GitDiffCheckOutput` must be persisted even when it contains only advisory output or is empty.

Do not represent missing output by omitting the property.

## WinPS 5.1 Git capture

Use operation-scoped native capture under:

```text
Windows PowerShell 5.1.26100.9444
```

For `git diff --check`:

1. execute the command;
2. capture output;
3. capture `$LASTEXITCODE` immediately;
4. restore local error-handling state if temporarily changed;
5. persist command, output, exit code, classification, and result.

Known CRLF advisories may be:

```text
GitDiffCheckClassification = ADVISORY_ONLY
GitDiffCheckResult = PASS
```

only when:

```text
GitDiffCheckExitCode = 0
```

## Architecture-B invariants

Do not change:

```text
RunnerSHA256 = sole governed execution identity
validator = independent verifier
P01-P20 contract
SV01-SV36 contract
G01-G13 contract
P19 meaning
P20 meaning
two-root lifecycle
cross-hash carry-forward prohibition
runtime P01-P20 PASS claims during structural validation = 0
```

## Full SV01-SV36 rerun

After validator corrections are complete:

1. freeze validator bytes;
2. compute fresh `ValidatorSHA256`;
3. create a fresh DisposableRoot;
4. create a fresh DurableRoot;
5. execute the complete structural validation from SV01;
6. do not carry PASS records from `663353B9...8889E`;
7. produce exactly 36 new SV records;
8. retain all referenced evidence;
9. persist provisional durable evidence;
10. clean DisposableRoot;
11. prove DisposableRoot absent;
12. prove DurableRoot survives;
13. prove runner, validator, ledger, manifest, and resolution report survive;
14. recompute runner and validator hashes;
15. finalize P19/post-cleanup observations;
16. run the final EvidenceReference resolution pass;
17. require unresolved count = 0;
18. persist all finalization metadata including `GitDiffCheckOutput`;
19. atomically finalize ledger;
20. reopen and parse the final ledger;
21. reverify record count, failure count, hashes, references, and metadata.

## Iterative remediation permission

Terra may keep correcting ordinary validator/evidence/harness defects discovered during this remediation without requesting another authority.

Every validator-byte change requires:

```text
fresh ValidatorSHA256
complete SV01-SV36 restart
```

Authorized iterative classifications:

```text
VALIDATOR_DEFECT
EVIDENCE_REFERENCE_DEFECT
EVIDENCE_MANIFEST_DEFECT
STRUCTURAL_EVIDENCE_DEFECT
LEDGER_COMPLETENESS_DEFECT
LEDGER_FINALIZATION_DEFECT
LEDGER_METADATA_DEFECT
VALIDATION_METHOD_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
DISPOSABLE_EVIDENCE_LIFECYCLE_DEFECT
HASH_BINDING_DEFECT
GIT_CAPTURE_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

Retain failed iteration evidence.

## Mandatory escalation

STOP for Luna if correction would require:

```text
CONTRACT_AMBIGUITY
Architecture-B change
P01-P20 change
SV01-SV36 change
acceptance weakening
cross-hash PASS composition
P19/P20 reinterpretation
governance reinterpretation
```

STOP for separate production remediation if:

```text
PRODUCTION_DEFECT
tracked production-source change
Azure mutation
Docker/GHCR mutation
deployment mutation
GitHub mutation
```

## Prohibited shortcuts

Do not:

```text
edit the old ledger to manufacture compliance
copy old PASS records into the new ledger
treat recorded Result=PASS as evidence resolution
use deleted disposable paths as final EvidenceReference values
use historical candidate evidence for acceptance credit
change runner bytes
execute governed W5
allocate governed W5 RunId
stage/commit/push
mutate GitHub/Azure/Docker/GHCR
configure Twelve Data secrets
```

## Fresh candidate success gate

Require one exact pair:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
<NEW HASH, NOT 663353B91D10F473DCA59E95C2831BC0CE556F6C8D700173D55EE34D7508889E>
```

with:

```text
WinPS 5.1.26100.9444 = PASS
runner parser errors = 0
validator parser errors = 0
P01-P20 structural contract = PASS
runtime predicate PASS claims = 0
SV records = 36
SV failures = 0
EvidenceReference count = 36
resolved EvidenceReference count = 36
unresolved EvidenceReference count = 0
evidence manifest = retained and hash-verified
resolution report = retained
GitDiffCheckOutput property = PRESENT
git diff --check exit = 0
P19 finalization = PASS
P20 observation-only = PASS
DisposableRoot absent = True
DurableRoot survives = True
runner survives = True
validator survives = True
ledger survives = True
manifest survives = True
resolution report survives = True
runner hash reverified = PASS
validator hash reverified = PASS
staged paths = 0
authority-introduced tracked mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Mutation accounting

Required:

```text
authority-introduced tracked repository mutations = 0
staging = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
governed W5 executions = 0
governed W5 RunIds = 0
external mutations = 0
```

## Required PASS markers

Only after the fresh complete rerun passes emit:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B EVIDENCE-REFERENCE REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_SHA256>
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 EVIDENCE MANIFEST: PASS
RELEASE 1.12 WP04 — R1 EVIDENCE RESOLUTION REPORT: PASS
RELEASE 1.12 WP04 — R1 GITDIFFCHECKOUTPUT METADATA: PRESENT
RELEASE 1.12 WP04 — R1 POST-CLEANUP FINALIZATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 VALIDATOR HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 STRUCTURAL CLEARANCE READY FOR LUNA RECONCILIATION: YES
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
IterationCount
RunnerPath
RunnerSHA256
ValidatorPath
FreshValidatorSHA256
DurableRoot
LedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
ManifestExistsAfterCleanup
ResolutionReportExistsAfterCleanup
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

## Failure terminal report

If a mandatory escalation boundary is reached, emit:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B EVIDENCE-REFERENCE REMEDIATION: STOPPED
FIRST BLOCKING GATE: <GATE>
CLASSIFICATION: <CLASS>
RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
VALIDATOR SHA256: <CURRENT_HASH>
PRODUCTION DEFECT: <YES/NO>
RUNNER CORRECTION REQUIRED: <YES/NO>
VALIDATOR CORRECTION REQUIRED: <YES/NO>
FRESH RUNNER HASH REQUIRED: <YES/NO>
FRESH VALIDATOR HASH REQUIRED: <YES/NO>
GOVERNED W5 WRAPPER INVOKED: NO
GOVERNED W5 RUNID ALLOCATED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
PUBLICATION BLOCKER: UNRESOLVED
```

Retain sanitized evidence for the exact blocker.

## Stop and next gate

After Terra PASS, STOP.

Do not create or execute a W5 authority.

The next artifact must be a fresh **GPT-5.6 Luna Architecture-B final structural reconciliation** bound to:

```text
unchanged RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

fresh ValidatorSHA256 =
<new Terra result>
```

Only a subsequent Luna PASS may authorize a separate Terra governed W5 runtime authority.

Until then:

```text
R1 structural contract = NOT_ACCEPTED
Governed W5 execution = NOT_AUTHORIZED
Governed W5 RunId allocation = NOT_AUTHORIZED
W5 governed acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
Publication blocker = UNRESOLVED
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
```

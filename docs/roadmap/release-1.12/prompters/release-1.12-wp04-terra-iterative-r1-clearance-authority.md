# Release 1.12 WP04 — Terra Iterative R1 Structural Clearance Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns iterative local/disposable runner and harness correction, validation execution, durable evidence generation, and convergence to the fixed R1 structural contract.

GPT-5.6 Luna owns contract/policy interpretation, reconciliation, acceptance, governance, and authorization of governed W5 runtime execution.

GPT-5.6 Sol is supporting analysis only and must not silently replace Luna or Terra.

# Purpose

This authority replaces the inefficient one-defect-per-authority loop for **R1 structural clearance**.

Terra may keep diagnosing, correcting, hashing, and revalidating the disposable/local R1 runner and its structural-validation harness until either:

```text
A. the complete fixed R1 structural contract passes; or
B. a mandatory escalation boundary is reached.
```

Terra is expected to use the repository context, retained evidence, current runner source, prior failed ledgers, and validation results to choose the narrowest technically sound correction at each iteration.

Terra must not weaken, redefine, waive, or bypass any acceptance requirement merely to converge.

# Binding current state

The currently loaded narrow correction identified:

```text
First real failure: SV09
Classification: RUNNER_DEFECT
Production defect: NO
Fresh runner hash required: YES
```

The current self-contained runner already contains:

```text
canonical P01-P20 definitions
fail-closed aggregation framework
G01-G13 framework
```

but lacks the required executable governed source-wrapper → exact `$env:W` copy lifecycle.

The immediate known correction is:

```text
add Copy-GovernedWrapperToW
make G08 validate/prepare W as a destination
make G10 create exact W from governed source
make G11 prove source/W pre-hash equality
```

Current governance:

```text
Governed W5 wrapper invoked: NO
Governed W5 RunId allocated: NO
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
WP04 #263: OPEN
Milestone #63: OPEN
WP05: NOT_STARTED
```

# Fixed R1 contract

The following acceptance contract is immutable under this authority.

Terra may correct implementation/harness mechanics to satisfy it, but may not change its meaning.

## PowerShell baseline

```text
Windows PowerShell 5.1.26100.9444
```

Do not upgrade PowerShell.

Do not rely on PowerShell 7-only behavior.

## Canonical P01-P20

Exactly:

```text
P01 Windows PowerShell version == 5.1.26100.9444
P02 complete runner/harness parser errors == 0
P03 exact-byte source/pre-execution wrapper identity == PASS
P04 exact-byte pre/post-execution wrapper identity == PASS
P05 production wrapper execution completed == PASS
P06 ArchiveRetrieval == RETRIEVAL_FAILED
P07 FreshExtraction == NOT_APPLICABLE
P08 EvidenceCheckpoint == PASS
P09 FinalLifecycleResult == SUCCESS
P10 RestoreOnly count == 1
P11 unexpected real external invocations == 0
P12 external-call ledger contains only governed intercepted calls == PASS
P13 repository modified-path scope unchanged == PASS
P14 staged path count == 0
P15 git diff --check == PASS
P16 secret hygiene == PASS
P17 durable sanitized evidence retention == PASS
P18 complete durable W5 scenario ledger retention == PASS
P19 disposable sandbox cleanup == PASS
P20 error-precedence accounting == PASS
```

Every predicate must structurally support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require:

```text
exactly 20 IDs
missing = 0
duplicates = 0
extras = 0
```

## Fail-closed aggregation

Require:

```text
missing predicate -> fail
duplicate predicate -> fail
unresolved predicate -> not accepted
failed predicate -> fail
only exactly P01-P20 all resolved PASS -> ALL_PASS
```

During R1 structural validation:

```text
runtime predicate PASS claims = 0
```

## Exact-byte wrapper contract

One governed production wrapper source and one exact `$env:W` copy.

The runner must contain executable byte-preserving copy behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

or an executable helper such as:

```powershell
Copy-GovernedWrapperToW -SourceWrapper $SourceWrapper -DestinationWrapper $env:W
```

whose implementation uses byte-preserving file-copy semantics.

Prohibited for wrapper copying:

```text
Get-Content / Set-Content
Out-File
Add-Content
text reconstruction
encoding conversion
line-ending normalization
```

## G01-G13

Require the logical sequence:

```text
G01 exact Windows PowerShell version
G02 parser errors = 0
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute/valid destination
G08 W destination prepared/eligible for governed copy
G09 governed source wrapper validated
G10 governed source → exact W byte-preserving copy
G11 source-pre SHA-256 == W-pre SHA-256
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

Require:

```text
G01-G11 < G12 < G13
```

For this R1 authority:

```text
G12 execution = FORBIDDEN
G13 execution = FORBIDDEN
```

## SV08-SV14

Require:

```text
SV08 executable governed-source SHA-256
SV09 executable byte-preserving source → W copy
SV10 executable W-pre SHA-256
SV11 source/W pre equality before RunId
SV12 executable source/W post hashes
SV13 complete post equality assertions
SV14 mismatch fail closed
```

Structural future-run assertions:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

## P03/P04

Require:

```text
P03 -> actual SV08-SV11 pre exact-byte evidence
P04 -> actual SV12-SV14 post exact-byte evidence
```

## P19

Require:

```text
P01-P18
→ P20
→ durable evidence persistence
→ disposable cleanup
→ verify disposable absent
→ evaluate P19
→ finalize P19 outside disposable root
→ aggregate P01-P20
```

## P20

P20 observes/accounts for production-derived error precedence.

The runner/harness must not reimplement production persistence/archive/lifecycle policy to manufacture expected results.

# Canonical SV01-SV36

Every complete iteration validates exactly:

```text
SV01 exact Windows PowerShell 5.1.26100.9444
SV02 parser errors = 0
SV03 exactly canonical P01-P20 definitions
SV04 predicate fields complete
SV05 exact P01-P20 aggregation membership
SV06 unresolved predicates fail closed
SV07 runtime predicate PASS claims = 0
SV08 executable governed-source SHA-256
SV09 executable byte-preserving source → W copy
SV10 executable W-pre SHA-256
SV11 source/W pre equality before RunId
SV12 executable source/W post hashes
SV13 complete post equality assertions
SV14 mismatch fail closed
SV15 G01-G11 before RunId
SV16 G01-G11 before wrapper invocation
SV17 RunId before wrapper invocation
SV18 executable S validation
SV19 executable W validation
SV20 child S visibility
SV21 child W visibility
SV22 child S/W mismatch fail closed
SV23 P03 binding
SV24 P04 binding
SV25 P19 post-cleanup order
SV26 P20 observation-only contract
SV27 minimum shim surface
SV28 unexpected calls fail closed
SV29 no production-policy duplication
SV30 secret hygiene
SV31 independent disposable/durable roots
SV32 disposable cleanup
SV33 runner survives cleanup
SV34 ledger survives cleanup
SV35 runner hash reverified
SV36 repository/Git invariants
```

Every SV record requires:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

# Iterative correction authority

Terra may perform multiple correction iterations in this one authority.

For each iteration:

1. identify the first real failed SV/gate;
2. classify the failure;
3. determine whether it is within Terra's authorized correction domain;
4. make the narrowest correction;
5. compute a fresh runner hash if runner bytes changed;
6. freeze that hash for the iteration;
7. restart SV01-SV36 from SV01;
8. retain the failed iteration's sanitized evidence;
9. continue if the failure is an authorized correctable defect.

Terra does **not** need a new Markdown authority between ordinary correctable R1 defects.

# Correctable defect classes

Terra may autonomously correct:

```text
RUNNER_DEFECT
HARNESS_DEFECT
STRUCTURAL_EVIDENCE_DEFECT
DISPOSABLE_EVIDENCE_LIFECYCLE_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
LEDGER_COMPLETENESS_DEFECT
LEDGER_ORDERING_DEFECT
HASH_BINDING_DEFECT
SHIM_FAIL_CLOSED_DEFECT
```

only when the correction:

```text
does not change production source
does not change the fixed P01-P20 contract
does not change the fixed SV01-SV36 contract
does not weaken fail-closed behavior
does not add real external mutations
does not execute governed W5
does not allocate a governed W5 RunId
```

# Mandatory escalation boundaries

Terra must STOP and require Luna reconciliation/definition if it encounters:

```text
CONTRACT_AMBIGUITY
acceptance criteria conflict
need to add/remove/redefine P01-P20
need to add/remove/redefine SV01-SV36
need to weaken an acceptance predicate
need to reinterpret a governance boundary
need to change the meaning of P19 or P20
need to authorize cross-hash evidence composition
```

Terra must STOP and require a separate production-remediation authority if it encounters:

```text
PRODUCTION_DEFECT
required tracked production-source change
required Azure configuration mutation
required Docker/GHCR mutation
required deployment mutation
required GitHub mutation
```

Terra must not silently reclassify these as runner/harness defects.

# Runner hash discipline

Every runner-byte change requires:

```text
new SHA-256
full SV01-SV36 restart
```

No structural PASS may carry forward from another runner hash.

Historical failed hashes remain immutable evidence only.

For every iteration retain:

```text
IterationId
RunnerSHA256
FirstFailedSV
FailureClassification
ProductionDefect
FreshHashRequired
FailureEvidencePath
```

# Durable iteration evidence

Use durable evidence roots outside disposable working roots.

Preferred structure:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<iteration-guid>
```

Preserve failed iteration evidence.

Do not overwrite or rewrite historical ledgers to make them pass.

# Two-root lifecycle

For the final successful candidate require independent:

```text
DisposableRoot
FreshDurableRoot
```

Before disposable cleanup, persist required evidence durably.

Then:

1. remove disposable root;
2. prove disposable root absent;
3. prove durable root exists;
4. prove retained runner exists;
5. prove complete ledger exists;
6. reopen/parse ledger;
7. recompute runner SHA-256;
8. prove retained hash == final frozen hash;
9. finalize SV32-SV35 outside disposable root.

Do not recreate the disposable root afterward.

# Fresh Git evidence

Each final acceptance iteration requires fresh repository evidence.

Use operation-scoped Windows PowerShell 5.1 native capture for:

```text
git diff --check
```

Retain:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
AuthorityIntroducedTrackedMutations
```

Known CRLF advisory output is acceptable only when:

```text
GitDiffCheckExitCode = 0
GitDiffCheckClassification = ADVISORY_ONLY
```

Expected baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

# Mutation boundary

Authorized:

```text
local/disposable runner corrections
local/disposable harness corrections
local structural-validation execution
durable local evidence creation
disposable cleanup
```

Forbidden:

```text
tracked repository edits
staging
commit
push
GitHub mutation
Azure mutation
Docker/GHCR mutation
production mutation
governed W5 wrapper execution
governed W5 RunId allocation
```

Required final accounting:

```text
authority-introduced tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
external mutations = 0
```

# Secret hygiene

Never retain or print secret values.

Twelve Data secret configuration remains forbidden in WP04.

# Completion discipline

Do not stop successfully at an intermediate correction.

Examples that are **not** terminal success:

```text
Copy-GovernedWrapperToW added
SV09 passes
new hash generated
parser passes
P01-P20 present
partial ledger created
SV01-SV31 pass
cleanup pending
```

Keep iterating through authorized defect classes until full structural clearance or a mandatory escalation boundary.

# Structural clearance threshold

R1 structural clearance requires one final exact runner hash satisfying all:

```text
PowerShell = 5.1.26100.9444
parser errors = 0
canonical P01-P20 count = 20
predicate required fields complete
aggregation membership exact
unresolved fail-closed behavior proven
runtime predicate PASS claims = 0
SV09 executable governed source → W copy PASS
SV08-SV14 coherence PASS
G01-G13 structural ordering PASS
P03/P04 bindings PASS
P19/P20 structural contracts PASS
SV records exactly 36
SV IDs exactly SV01-SV36
SV failures = 0
independent observations complete
evidence references complete
validation methods complete
shim boundary PASS
no production-policy duplication PASS
secret hygiene PASS
two-root lifecycle PASS
disposable root absent
durable root survives
runner survives
ledger survives
retained runner hash reverified
fresh git diff --check PASS
repository invariant PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required successful terminal markers

Only after complete structural clearance emit:

```text
RELEASE 1.12 WP04 — TERRA ITERATIVE R1 STRUCTURAL CLEARANCE: PASS
RELEASE 1.12 WP04 — R1 FINAL RUNNER HASH: <SHA256>
RELEASE 1.12 WP04 — R1 CORRECTION ITERATIONS: <COUNT>
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 PARSER ERRORS: 0
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION: PASS
RELEASE 1.12 WP04 — R1 UNRESOLVED PREDICATES FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING COPY: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 G01-G13 STRUCTURAL ORDER: PASS
RELEASE 1.12 WP04 — R1 COMPLETE SV01-SV36 DURABLE LEDGER: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — R1 STRUCTURAL CLEARANCE READY FOR LUNA: YES
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

# Required successful handoff

Return:

```text
IterationCount
IterationLedger
FinalDurableRoot
FinalRunnerPath
FinalRunnerSHA256
SVLedgerPath
PowerShellVersion
ParserErrorCount
CanonicalPredicateCount
PredicateFieldFailures
AggregationMembershipCount
AggregationMissingIds
AggregationDuplicateIds
RuntimePredicatePassClaims
SV09CopyEvidence
SV08SV14Coherence
G01G13Ordering
SVRecordCount
SVFailedCount
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
ExactMutationAccounting
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
```

# Escalation terminal report

If a mandatory escalation boundary is reached, retain evidence and emit:

```text
RELEASE 1.12 WP04 — TERRA ITERATIVE R1 STRUCTURAL CLEARANCE: STOPPED
FIRST BLOCKING GATE: <SV/GATE>
CLASSIFICATION: <CLASS>
PRODUCTION DEFECT: <YES/NO>
CONTRACT CHANGE REQUIRED: <YES/NO>
TRACKED PRODUCTION CHANGE REQUIRED: <YES/NO>
FRESH RUNNER HASH REQUIRED: <YES/NO>
GOVERNED W5 WRAPPER INVOKED: NO
GOVERNED W5 RUNID ALLOCATED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
```

Include the narrowest evidence-supported next action.

# Luna handoff

After complete Terra structural PASS, run the existing:

```text
Release 1.12 WP04 — Luna Final Self-Contained Runner SV01-SV36 Reconciliation
```

against the final runner hash and durable ledger.

Terra structural PASS does **not** itself authorize W5.

Only a subsequent Luna reconciliation PASS may authorize a separate governed W5 runtime authority.

# Final stop

**KEEP ITERATING THROUGH AUTHORIZED R1 RUNNER/HARNESS/EVIDENCE DEFECTS UNTIL COMPLETE STRUCTURAL CLEARANCE OR A MANDATORY ESCALATION BOUNDARY.**

Do not execute governed W5 under this authority.

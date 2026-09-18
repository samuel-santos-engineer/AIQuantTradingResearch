# Release 1.12 WP04 — Terra Resume and Finish R1 Durable Validation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the local/disposable runner completion and structural-validation execution authorized here.

GPT-5.6 Luna owns subsequent read-only reconciliation, acceptance, governance, and authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Luna or Terra.

# Why this authority exists

Repeated Luna reconciliation correctly returned `NOT_READY` because Terra had created a newest `runner.ps1` candidate but had **not completed the durable SV01–SV36 validation package**.

This authority is an execution-resumption authority.

It does **not** authorize another partial runner-only iteration.

Its required terminal outcome is exactly one of:

```text
A. COMPLETE finalized self-contained runner + complete durable SV01-SV36 ledger
B. FIRST_REAL_FAILURE retained durably with exact failed gate/SV
```

Do not stop merely because a runner file has been created.

# Binding current state

Current governance:

```text
R1 structural contract: NOT_ACCEPTED
Governed W5 execution: NOT_AUTHORIZED
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
WP04 #263: OPEN
Milestone #63: OPEN
WP05: NOT_STARTED
```

Newest known candidate root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7de8206cd44e4ab69e8c1139b9a9ecc7
```

Known state of that root at last Luna inspection:

```text
runner.ps1: PRESENT
complete durable SV01-SV36 ledger: ABSENT
```

Available older ledgers belong to failed historical hashes and provide no acceptance credit.

Historical failed hashes include:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

# Mission

Resume from the newest candidate.

First inspect its exact bytes.

If it already satisfies the complete self-contained runner contract, **do not rewrite it**. Freeze its SHA-256 and immediately proceed through the entire structural validation and durable-ledger lifecycle.

If it is deficient, make only the narrowest local/disposable correction required, freeze the resulting new SHA-256, and restart structural validation from SV01.

The authority is incomplete until the full durable package is finalized or a real failure is durably retained.

# Self-contained runner requirements

One exact final runner hash must contain all required structural behavior.

## Canonical P01–P20

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

Every predicate must support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require exactly 20 unique IDs, no missing IDs, duplicates, or extras.

## Fail-closed aggregation

The final runner must prove:

```text
missing predicate -> fail
duplicate predicate -> fail
unresolved predicate -> not accepted
failed predicate -> fail
only exactly P01-P20 all resolved PASS -> ALL_PASS
```

During structural validation:

```text
runtime P01-P20 PASS claims = 0
```

## SV09 executable byte-preserving copy

Require executable behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

with:

```text
SourceWrapper = governed production wrapper
source validated before copy
destination = exact $env:W
copy failure fails closed
no text transformation
source-pre SHA-256 computed
W-pre SHA-256 computed
source-pre == W-pre before RunId allocation
```

# Exact-byte controls

SV08–SV14 must coherently bind one governed source and one `$env:W` copy.

Require future-run structural assertions:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

Mismatch must fail closed.

# Pre-RunId boundary

Require:

```text
G01 exact Windows PowerShell 5.1.26100.9444
G02 parser errors = 0
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute
G08 W expected/fresh
G09 governed source validated
G10 source → W byte-preserving copy
G11 source/W pre-hash equality
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

Structural order:

```text
G01-G11 < G12 < G13
```

For this authority:

```text
G12 execution = FORBIDDEN
G13 execution = FORBIDDEN
```

# P03/P04

Require:

```text
P03 -> actual SV08-SV11 pre exact-byte evidence
P04 -> actual SV12-SV14 post exact-byte evidence
```

# P19

Require structural ordering:

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

# P20

P20 observes production-derived error precedence.

Do not duplicate production persistence/archive/lifecycle policy.

# Mandatory SV01–SV36 run

Validate the final frozen runner from SV01.

Produce exactly 36 unique records:

```text
SV01 ... SV36
```

Each record requires:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Checks:

```text
SV01 Windows PowerShell exactly 5.1.26100.9444
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

# Evidence quality

Do not emit aggregate-only structural evidence.

For every SV record:

- `Observed` must contain substantive independently inspectable evidence;
- `EvidenceReference` must resolve;
- `ValidationMethod` must identify how the evidence was established.

Generic `PASS`, `true`, or restatement of `Expected` is insufficient where source/control-flow/hash/path evidence is required.

# Windows PowerShell

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Final frozen runner parser errors:

```text
0
```

# Durable evidence root

Create/finalize one fresh durable root for the final runner hash:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Retain at minimum:

```text
runner.ps1
runner-sha256 evidence
parser evidence
canonical P01-P20 evidence
aggregation/fail-closed evidence
SV01-SV36 observation ledger
SV09 executable-source evidence
G01-G13 ordering evidence
fresh Git/repository evidence
sanitized validation summary
```

Do not place required retained evidence only beneath a disposable root.

# Two-root lifecycle

Use independent:

```text
DisposableRoot
FreshDurableRoot
```

Before cleanup, persist all evidence needed to survive cleanup.

Then:

1. remove DisposableRoot;
2. prove DisposableRoot absent;
3. prove FreshDurableRoot exists;
4. prove retained runner exists;
5. prove retained ledger exists;
6. reopen/parse retained ledger;
7. recompute retained runner SHA-256;
8. prove recomputed hash equals frozen FinalRunnerSHA256;
9. finalize SV32–SV35 evidence outside DisposableRoot.

Do not recreate DisposableRoot afterward.

# Fresh Git evidence

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
```

Capture `$LASTEXITCODE` immediately.

Known CRLF advisory output is acceptable only when:

```text
GitDiffCheckExitCode = 0
GitDiffCheckClassification = ADVISORY_ONLY
```

Restore prior error handling immediately after capture.

Expected repository baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

# Mutation boundary

Authorized:

```text
local/disposable runner correction if required
local/disposable structural validation
fresh durable local evidence
disposable cleanup
```

Forbidden:

```text
tracked repository edits
staging
commit
push
GitHub mutations
Azure mutations
Docker/GHCR mutations
production mutations
governed W5 execution
governed W5 RunId allocation
```

Required:

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

# Execution discipline

This authority must not terminate successfully at any of these intermediate states:

```text
runner created
runner corrected
runner parser-valid
runner hashed
SV09 found
P01-P20 found
partial SV ledger created
SV01-SV31 passed
disposable cleanup pending
```

A success terminal state requires the **complete durable post-cleanup package**.

# PASS threshold

Require all:

```text
one final frozen runner hash
PowerShell = 5.1.26100.9444
parser errors = 0
canonical P01-P20 count = 20
predicate fields complete
aggregation membership exact
unresolved fail-closed behavior proven
runtime predicate PASS claims = 0
SV09 executable copy proven
SV08-SV14 coherence proven
G01-G13 structural ordering proven
P03/P04 bindings proven
P19/P20 contracts proven
SV record count = 36
SV IDs exactly SV01-SV36
SV failures = 0
independent observations complete
evidence references complete
validation methods complete
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

# Required PASS markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — TERRA R1 RESUMED VALIDATION: COMPLETE
RELEASE 1.12 WP04 — R1 FINAL SELF-CONTAINED RUNNER: PASS
RELEASE 1.12 WP04 — R1 FINAL RUNNER HASH: <SHA256>
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 PARSER ERRORS: 0
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION MEMBERSHIP: PASS
RELEASE 1.12 WP04 — R1 UNRESOLVED PREDICATES FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING COPY: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 G01-G13 STRUCTURAL ORDER: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 DURABLE LEDGER: PASS
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
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

# Required handoff

Return:

```text
InputCandidateRoot
InputCandidateRunnerHash
RunnerSourceChanged
DisposableRoot
FreshDurableRoot
FinalRunnerSHA256
RetainedRunnerPath
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
FirstFailedSV if any
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

# Failure terminal state

At the first real failure:

1. retain sanitized durable failure evidence when possible;
2. identify exact first failed gate/SV;
3. identify the final runner hash to which the failure applies;
4. classify:
   - `RUNNER_DEFECT`
   - `STRUCTURAL_EVIDENCE_DEFECT`
   - `HARNESS_DEFECT`
   - `REPOSITORY_INVARIANT_DEFECT`
   - `CONTRACT_AMBIGUITY`
5. state `Production defect: YES/NO`;
6. state `Fresh runner hash required: YES/NO`;
7. do not manufacture later PASS claims;
8. do not invoke W5;
9. do not allocate a W5 RunId;
10. STOP.

# Final stop

**STOP ONLY AFTER COMPLETE DURABLE SUCCESS OR THE FIRST REAL DURABLY RETAINED FAILURE.**

After complete PASS, execute the existing:

```text
Release 1.12 WP04 — Luna Final Self-Contained Runner SV01-SV36 Reconciliation
```

against the finalized Terra candidate.

Even on Terra PASS:

```text
Governed W5 execution = NOT_AUTHORIZED until Luna reconciles
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
Publication blocker = UNRESOLVED
```

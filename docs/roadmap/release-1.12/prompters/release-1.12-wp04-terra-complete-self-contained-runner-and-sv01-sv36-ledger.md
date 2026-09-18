# Release 1.12 WP04 — Terra Complete Self-Contained Runner & SV01–SV36 Durable Ledger

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the disposable/local runner completion, structural validation execution, and durable evidence generation authorized here.

GPT-5.6 Luna owns subsequent reconciliation, acceptance, governance, and authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

# Binding predecessor

Latest Luna result:

```text
RELEASE 1.12 WP04 — LUNA R1 SV03-RESTORED RECONCILIATION: NOT_READY
REASON: NEW SELF-CONTAINED RUNNER AND COMPLETE SV01-SV36 LEDGER NOT SUPPLIED
```

Newest candidate root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7de8206cd44e4ab69e8c1139b9a9ecc7
```

Observed contents:

```text
runner.ps1
```

Missing:

```text
complete durable SV01-SV36 observation ledger
```

No completion may be inferred from `runner.ps1` merely existing.

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

# Mission

Complete the missing Terra work now.

Use the newest candidate runner as an input candidate, inspect it, and determine whether it is one self-contained artifact containing both:

1. canonical P01–P20 definitions, required fields, aggregation membership, and unresolved fail-closed behavior; and
2. executable byte-preserving governed-wrapper source → exact `$env:W` copy behavior.

If it satisfies both, freeze its exact bytes/hash and produce the complete fresh durable SV01–SV36 ledger.

If it does not, make the narrowest disposable/local runner correction required, generate a new hash, and then produce the complete fresh SV01–SV36 ledger.

Do not stop after runner creation.

The required deliverable of this authority is the **finalized durable structural evidence package**, or retained evidence for the first real failure.

# Historical hash boundaries

Historical failed hashes include:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

Do not combine acceptance evidence across hashes.

The final ledger must bind entirely to one retained runner SHA-256.

# Initial candidate inspection

Inspect:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7de8206cd44e4ab69e8c1139b9a9ecc7\runner.ps1
```

Before any structural PASS claim, establish:

```text
runner exists
runner SHA-256
Windows PowerShell version
parser error count
canonical P01-P20 definition count
SV09 executable copy presence
aggregation/fail-closed logic presence
pre-RunId ordering presence
P19/P20 structure presence
```

If source changes are necessary after hashing, compute a new hash and bind all evidence to the final bytes only.

# Required canonical P01–P20 contract

The final runner must structurally define exactly:

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

Each definition must structurally support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Structural validation must not manufacture runtime PASS values.

# Aggregation contract

The final runner must structurally prove:

```text
exact predicate membership = P01-P20
membership count = 20
missing = 0
duplicates = 0
extras = 0

missing predicate -> fail closed
duplicate predicate -> fail closed
unresolved predicate -> not accepted
failed predicate -> fail
only all 20 resolved PASS -> ALL_PASS
```

During this authority:

```text
runtime predicate PASS claims = 0
```

# Required SV09 contract

The final runner must contain executable file-copy behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

Require:

```text
SourceWrapper = governed production wrapper source
source validated before copy
destination = exact $env:W
copy is byte-preserving file operation
copy failure fails closed
source-pre SHA-256 computed
W-pre SHA-256 computed
source-pre == W-pre asserted before future RunId allocation
```

No text I/O may implement wrapper copying.

# SV08–SV14 exact-byte coherence

Require one coherent artifact pair:

```text
governed wrapper source
$env:W copied wrapper
```

The final runner must structurally preserve:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

Mismatch fails closed.

# Pre-RunId sequence

Require independently inspectable structural evidence:

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

Require:

```text
G01-G11 < G12 < G13
```

For this authority:

```text
G12 execution = FORBIDDEN
G13 execution = FORBIDDEN
```

# P03/P04 bindings

Require:

```text
P03 -> actual SV08-SV11 pre-execution exact-byte evidence
P04 -> actual SV12-SV14 pre/post exact-byte evidence
```

# P19 ordering

Require structural proof:

```text
P01-P18
→ P20
→ persist durable evidence outside disposable root
→ remove disposable root
→ verify disposable root absent
→ evaluate P19
→ finalize P19 outside disposable root
→ aggregate P01-P20
```

# P20 contract

P20 observes/accounts for production-derived error precedence.

Do not duplicate production persistence/archive/lifecycle policy.

# Complete SV01–SV36 durable ledger

Produce exactly 36 unique records:

```text
SV01 ... SV36
```

Every record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Required checks:

```text
SV01 exact Windows PowerShell 5.1.26100.9444
SV02 parser errors = 0
SV03 exactly canonical P01-P20 definitions
SV04 predicate fields complete
SV05 exact P01-P20 aggregation membership
SV06 unresolved predicates fail closed
SV07 runtime predicate PASS claims = 0
SV08 executable source SHA-256
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

Every `Observed` value must contain substantive, independently inspectable evidence.

Every `EvidenceReference` must resolve against the final retained runner or fresh durable evidence.

Every `ValidationMethod` must explain how the observation was established.

Do not use generic topology text as executable evidence.

# Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Final runner parser errors:

```text
0
```

# Fresh durable package

If the existing newest root is only an incomplete candidate, do not pretend it is finalized.

Create a fresh durable final candidate root unless the existing root can be completed without violating historical-evidence immutability.

Preferred:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Retain:

```text
final runner exact bytes
final runner SHA-256
parser evidence
P01-P20 structural evidence
SV01-SV36 ledger
SV09 source evidence
aggregation/fail-closed evidence
fresh git diff --check evidence
sanitized validation summary
```

# Two-root lifecycle

Use an independent disposable working root and durable evidence root.

After pre-cleanup evidence is durable:

1. remove disposable root;
2. verify disposable root absent;
3. verify durable root exists;
4. verify final runner exists;
5. verify SV ledger exists;
6. reopen and parse ledger;
7. recompute final runner SHA-256;
8. verify hash identity;
9. finalize SV32-SV35 outside disposable root.

Do not recreate the disposable root after cleanup.

# Fresh Git/repository evidence

Use the proven operation-scoped Windows PowerShell 5.1 native capture.

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

Known CRLF advisory text is acceptable only with:

```text
GitDiffCheckExitCode = 0
GitDiffCheckClassification = ADVISORY_ONLY
```

Expected repository baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

# Mutation boundary

Authorized:

```text
disposable/local runner completion
disposable validation evidence
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
production mutations
Azure mutations
Docker/GHCR mutations
governed W5 execution
governed W5 RunId allocation
```

Required accounting:

```text
tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
production mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
external mutations = 0
```

# PASS threshold

PASS requires:

```text
one final self-contained runner hash
canonical P01-P20 count = 20
predicate fields complete
aggregation membership exact
unresolved fail-closed behavior proven
runtime predicate PASS claims = 0
SV09 executable byte-preserving copy proven
SV08-SV14 coherent
PowerShell = 5.1.26100.9444
parser errors = 0
SV records = exactly 36
SV failures = 0
independent observations complete
evidence references resolvable
validation methods complete
G01-G13 structural order proven
P03/P04 bindings proven
P19/P20 contracts proven
two-root lifecycle proven
post-cleanup hash reverified
fresh git diff --check PASS
repository invariant PASS
secret hygiene PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required success markers

Only on full PASS emit:

```text
RELEASE 1.12 WP04 — R1 SELF-CONTAINED RUNNER COMPLETION: PASS
RELEASE 1.12 WP04 — R1 FINAL RUNNER HASH: <SHA256>
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION MEMBERSHIP: PASS
RELEASE 1.12 WP04 — R1 UNRESOLVED PREDICATES FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING COPY: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 COMPLETE SV01-SV36 DURABLE LEDGER: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA SELF-CONTAINED RUNNER AND LEDGER COMPLETE
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
SVRecordCount
SVFailedCount
FirstFailedSV if any
G01G13Ordering
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

# Failure boundary

At the first real failure:

- retain sanitized durable failure evidence when possible;
- report exact failed gate/SV;
- classify the defect;
- state whether another runner hash is required;
- do not manufacture later PASS claims;
- do not execute W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP AFTER ONE SELF-CONTAINED RUNNER AND ITS COMPLETE DURABLE SV01-SV36 LEDGER ARE FINALIZED, OR AFTER THE FIRST REAL FAILURE IS RETAINED.**

Even on PASS:

```text
Luna reconciliation = REQUIRED
Governed W5 execution = NOT_AUTHORIZED YET
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
Publication blocker = UNRESOLVED
```

After full PASS, execute the existing:

```text
Release 1.12 WP04 — Luna R1 SV03-Restored New-Hash Reconciliation Authority
```

against the finalized durable candidate.

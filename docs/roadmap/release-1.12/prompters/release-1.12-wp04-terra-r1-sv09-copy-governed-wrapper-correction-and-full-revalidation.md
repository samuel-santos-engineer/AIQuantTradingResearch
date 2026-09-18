# Release 1.12 WP04 — Terra R1 SV09 Copy-GovernedWrapperToW Correction & Full Revalidation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the narrow disposable/local runner correction and complete structural-validation execution authorized here.

GPT-5.6 Luna owns subsequent read-only reconciliation, acceptance, governance, and authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Luna or Terra.

# Binding predecessor

The resumed R1 validation reached the first real failure at:

```text
SV09
```

The newest self-contained runner now contains:

```text
canonical P01-P20 definitions: PRESENT
fail-closed accounting framework: PRESENT
G01-G13 framework: PRESENT
executable source-wrapper → $env:W copy: ABSENT
```

Current defect:

```text
G08 requires W to already exist.
The runner therefore cannot establish the required governed source → W copy lifecycle.
```

Classification:

```text
RUNNER_DEFECT
```

Production defect:

```text
NO
```

Fresh runner hash required:

```text
YES
```

Governance remains:

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

# Mission

Make exactly the narrow runner correction required to satisfy SV09 while preserving the self-contained runner work that already exists.

Add an executable byte-preserving:

```text
Copy-GovernedWrapperToW
```

operation that copies the governed source wrapper to exact `$env:W`.

Correct the pre-RunId lifecycle so `$env:W` is a validated destination for the copy rather than an artifact that must already contain the copied wrapper before the copy operation.

Then:

1. generate a fresh runner SHA-256;
2. freeze those exact bytes;
3. restart validation from SV01;
4. complete all SV01-SV36 checks;
5. retain the complete durable post-cleanup ledger.

Do not stop after adding the function.

# Narrow correction boundary

Preserve unless an exact dependency requires adjustment:

```text
canonical P01-P20 definitions
P01-P20 required fields
fail-closed aggregation
G01-G13 framework
P03/P04 bindings
P19 ordering
P20 observation-only contract
minimum shim boundary
no-production-policy-duplication contract
two-root lifecycle
Windows PowerShell 5.1 compatibility
```

Do not redesign unrelated runner logic.

# Required Copy-GovernedWrapperToW operation

Implement executable behavior equivalent to:

```powershell
function Copy-GovernedWrapperToW {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourceWrapper,

        [Parameter(Mandatory = $true)]
        [string]$DestinationWrapper
    )

    [IO.File]::Copy($SourceWrapper, $DestinationWrapper, $true)
}
```

Exact implementation may differ, but the operation must use byte-preserving file-copy semantics.

The governed call must bind:

```text
SourceWrapper      = governed production wrapper source
DestinationWrapper = exact $env:W
```

Acceptable direct core operation:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

Do not use:

```text
Get-Content / Set-Content
Out-File
Add-Content
text encoding conversion
line-ending normalization
string reconstruction
```

for wrapper copying.

# Corrected G08/G09/G10/G11 lifecycle

The current incorrect lifecycle assumes `$env:W` already exists before the governed copy.

Correct the structure so the logical sequence is:

```text
G01 exact Windows PowerShell version
G02 parser-valid runner
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute and valid destination path
G08 W destination prepared/eligible for governed copy
G09 governed source wrapper validated
G10 Copy-GovernedWrapperToW(source, exact W)
G11 source-pre SHA-256 == W-pre SHA-256
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

The destination's parent may be prepared locally if required.

G08 must **not** require the final copied wrapper bytes to pre-exist.

The exact copied wrapper must be created by G10.

# Fail-closed copy behavior

Require:

```text
source missing -> fail before copy
source invalid -> fail before copy
W invalid/non-absolute -> fail before copy
W destination preparation failure -> fail
copy exception -> fail
W missing after copy -> fail
source/W hash mismatch -> fail
any G01-G11 failure -> no RunId
any G01-G11 failure -> no wrapper invocation
```

# Source identity

The runner must prove that `SourceWrapper` resolves to the governed production wrapper source.

Do not permit an arbitrary substitute fixture to satisfy SV09.

Retain the source-resolution evidence used by the structural validator.

# Exact-byte hash lifecycle

Before any future RunId:

```text
SourcePreSHA256 = SHA256(governed source)
Copy-GovernedWrapperToW
WPreSHA256 = SHA256(exact W)
assert SourcePreSHA256 == WPreSHA256
```

Future post-wrapper structural contract remains:

```text
SourcePostSHA256 = SHA256(governed source)
WPostSHA256 = SHA256(exact W)

SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

Mismatch fails closed.

# Preserve canonical P01–P20

The corrected runner must continue to define exactly:

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

Require exactly 20 unique predicate IDs.

Structural validation runtime PASS claims remain:

```text
0
```

# P03/P04 bindings

After correction require:

```text
P03 -> actual source-pre / copy / W-pre / pre-hash equality evidence
P04 -> actual source/W post-hash equality evidence
```

# Fresh runner hash

Because executable runner source changes, compute a new SHA-256.

The new hash must differ from all predecessor failed hashes, including:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

Once computed:

```text
FINAL RUNNER BYTES = FROZEN
```

Any subsequent source edit invalidates the candidate and requires another hash plus complete SV01-SV36 restart.

# Complete SV01-SV36 restart

Restart from SV01.

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

Validate:

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

# SV09 acceptance evidence

SV09 must independently retain and reference:

```text
Copy-GovernedWrapperToW function/operation source
byte-preserving [IO.File]::Copy operation
governed SourceWrapper binding
exact $env:W destination binding
G08 destination-preparation behavior
G09 source-validation behavior
G10 copy invocation
copy failure handling
post-copy W existence check
source-pre SHA-256 operation
W-pre SHA-256 operation
G11 equality assertion
G11 precedes G12
```

Generic prose is insufficient.

# SV08-SV14 coherence

Prove every check binds to the same:

```text
governed source wrapper
exact $env:W copied wrapper
```

No substitute path or fixture may be used for acceptance.

# Evidence quality

Every SV record must have substantive, independently inspectable `Observed` evidence.

Every `EvidenceReference` must resolve to the frozen runner or retained durable evidence.

Every `ValidationMethod` must state how the check was established.

Do not accept aggregate-only `PASS` assertions.

# Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Parser errors for the final frozen runner:

```text
0
```

# Durable evidence lifecycle

Create one fresh durable root for the new runner hash:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Retain:

```text
final runner exact bytes
final runner SHA-256
parser evidence
P01-P20 evidence
aggregation/fail-closed evidence
SV01-SV36 ledger
SV09 executable copy evidence
G01-G13 ordering evidence
fresh Git/repository evidence
sanitized validation summary
```

Use a separate disposable root.

After pre-cleanup evidence is durable:

1. remove disposable root;
2. prove disposable root absent;
3. prove durable root exists;
4. prove runner exists;
5. prove ledger exists;
6. reopen/parse ledger;
7. recompute runner SHA-256;
8. prove it equals final frozen hash;
9. finalize SV32-SV35 outside the disposable root.

# Fresh Git evidence

Use operation-scoped Windows PowerShell 5.1 native capture.

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

Known CRLF advisories are acceptable only when:

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
local/disposable runner correction
local structural validation
fresh durable local evidence
disposable cleanup
```

Forbidden:

```text
tracked repository mutation
staging
commit
push
GitHub mutation
Azure mutation
Docker/GHCR mutation
production mutation
governed W5 execution
governed W5 RunId allocation
```

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
external mutations = 0
```

# W5 boundary

This authority remains structural only.

Require:

```text
GovernedW5WrapperInvoked = NO
GovernedW5RunIdAllocated = NO
runtime P01-P20 execution = NO
runtime predicate PASS claims = 0
```

# Completion discipline

Do not terminate successfully at:

```text
Copy-GovernedWrapperToW added
parser passes
new hash computed
SV09 passes
partial ledger produced
SV01-SV31 passes
cleanup pending
```

Success requires the complete durable post-cleanup SV01-SV36 package.

# PASS threshold

Require all:

```text
narrow SV09 correction implemented
G08 no longer incorrectly requires copied W bytes to pre-exist
G10 creates exact W from governed source
new frozen runner hash
PowerShell 5.1.26100.9444
parser errors = 0
canonical P01-P20 = 20
aggregation/fail-closed contract PASS
runtime predicate PASS claims = 0
SV09 executable copy PASS
SV08-SV14 coherence PASS
G01-G13 order PASS
SV records exactly 36
SV failures = 0
independent observations complete
evidence references complete
validation methods complete
P03/P04 PASS
P19/P20 structural contracts PASS
secret hygiene PASS
two-root lifecycle PASS
post-cleanup hash reverified
fresh git diff --check PASS
repository invariant PASS
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

# Required success markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — R1 SV09 COPY-GOVERNED-WRAPPER CORRECTION: PASS
RELEASE 1.12 WP04 — R1 G08 DESTINATION LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 G10 GOVERNED SOURCE-TO-W COPY: PASS
RELEASE 1.12 WP04 — R1 FINAL RUNNER HASH: <SHA256>
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 PARSER ERRORS: 0
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION: PASS
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
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA SV09 CORRECTION AND FULL REVALIDATION COMPLETE
```

# Required handoff

Return:

```text
InputRunnerPath
InputRunnerSHA256
RunnerSourceChanged
DisposableRoot
FreshDurableRoot
FinalRunnerSHA256
RetainedRunnerPath
SVLedgerPath
PowerShellVersion
ParserErrorCount
CanonicalPredicateCount
AggregationMembershipCount
RuntimePredicatePassClaims
CopyGovernedWrapperToWSourceReference
G08ObservedLifecycle
G10ObservedCopy
G11ObservedHashEquality
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

# Failure boundary

At the first real failure:

- retain sanitized durable failure evidence when possible;
- identify exact first failed gate/SV;
- identify the runner hash;
- classify the defect;
- state `Production defect: YES/NO`;
- state `Fresh runner hash required: YES/NO`;
- do not manufacture later PASS claims;
- do not invoke W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP ONLY AFTER COMPLETE DURABLE SV01-SV36 SUCCESS OR THE FIRST REAL DURABLY RETAINED FAILURE.**

After a complete Terra PASS, execute the existing:

```text
Release 1.12 WP04 — Luna Final Self-Contained Runner SV01-SV36 Reconciliation
```

against the new frozen runner and its complete durable ledger.

Even on Terra PASS:

```text
Governed W5 execution = NOT_AUTHORIZED until Luna reconciliation passes
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
Publication blocker = UNRESOLVED
```

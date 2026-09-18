# Release 1.12 WP04 — Terra Perform SV09 Copy-Item Correction & Fresh New-Hash Ledger

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the disposable runner correction and structural validation execution authorized here.

GPT-5.6 Luna owns subsequent reconciliation and any authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

# Current state

The correction has **not yet been executed**.

Binding state:

```text
Failed historical runner SHA-256:
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8

Known first defect:
SV09 — executable byte-for-byte wrapper copy exists

Classification:
RUNNER_DEFECT

Production defect:
NO

Fresh runner hash required:
YES

Governed W5 wrapper invoked:
NO

Governed W5 RunId allocated:
NO

W5 governed acceptance:
NOT_GRANTED

W6/W7/W8:
NOT_RUN

Publication blocker:
UNRESOLVED
```

Historical failure evidence must remain immutable.

# Mission

Execute the missing correction now.

Create a **new disposable runner artifact** containing an actual Windows PowerShell 5.1-compatible byte-preserving file copy from the governed production wrapper source to the exact path represented by `$env:W`.

Then generate a new runner hash and a complete fresh durable SV01–SV36 observation ledger for that exact new artifact.

This authority is execution authority, not planning authority.

Do not stop merely because the required correction is understood.

# SV09 mandatory executable correction

The runner must perform an actual file copy equivalent to:

```powershell
Copy-Item -LiteralPath $GovernedWrapperSource -Destination $env:W -Force -ErrorAction Stop
```

A suitable byte-preserving `[System.IO.File]::Copy(...)` is also acceptable if required by the existing runner structure.

The copy operation must be executable production-wrapper-to-sandbox-wrapper behavior, not a comment, description, topology record, or simulated result.

## Source requirements

Before the copy:

```text
governed wrapper source path resolved
source path nonempty
source path absolute
source exists as a file
source validation fails closed
```

## Destination requirements

Before/after the copy:

```text
$env:W nonempty
$env:W absolute
$env:W belongs to the fresh disposable scenario
copy destination == exact $env:W
copied file exists
```

## Byte-preservation requirements

Do not use text I/O for the wrapper copy.

Forbidden:

```text
Get-Content
Set-Content
Out-File
Add-Content
string replacement
regex transformation
encoding conversion
newline normalization
templating
patching
```

The copy must preserve source bytes.

# Exact-byte proof

After copying and before any possible RunId allocation:

```text
SourcePreSHA256 = SHA256(governed wrapper source)
WPreSHA256      = SHA256($env:W)
```

Require:

```text
SourcePreSHA256 == WPreSHA256
```

Mismatch must fail closed.

The runner must also preserve the future post-execution contract:

```text
SourcePostSHA256 = SHA256(governed wrapper source)
WPostSHA256      = SHA256($env:W)
```

and structurally require:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

# Pre-RunId boundary

The corrected runner must structurally prove:

```text
G01 exact Windows PowerShell 5.1.26100.9444
G02 parser errors = 0
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute
G08 W belongs to expected fresh scenario
G09 governed wrapper source validated
G10 actual byte-preserving source → W copy completed
G11 source-pre SHA-256 == W-pre SHA-256
G12 allocate governed W5 RunId
G13 invoke exact wrapper at W
```

**For this authority, stop before G12.**

Therefore:

```text
governed W5 RunId allocation = FORBIDDEN
governed W5 wrapper invocation = FORBIDDEN
```

Any G01–G11 failure must terminate before G12/G13.

# Artifact identity

The following runner is permanently historical/failed:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

The corrected runner must have:

```text
NewRunnerSHA256 != C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

No acceptance evidence may be combined across the two hashes.

# Fresh two-root lifecycle

Create:

```text
DisposableRoot = new temporary working root
DurableRoot = new %LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Require:

```text
DisposableRoot != DurableRoot
DurableRoot is not under DisposableRoot
```

Retain under DurableRoot:

```text
corrected runner source
new runner SHA-256
parser evidence
SV01-SV36 ledger
supporting source/control-flow observations
fresh git diff --check evidence
sanitized validation report
```

Preserve all historical durable roots.

# Complete SV01–SV36 validation

The source correction invalidates old-hash structural acceptance credit.

Validate all checks against the new artifact:

```text
SV01 exact Windows PowerShell 5.1.26100.9444
SV02 parser errors = 0
SV03 exactly canonical P01-P20 definitions
SV04 predicate fields complete
SV05 all P01-P20 participate in aggregation
SV06 unresolved predicates fail closed
SV07 runtime predicate PASS claims = 0
SV08 executable governed-source SHA-256 calculation
SV09 executable byte-preserving governed-source → W copy
SV10 executable W-pre SHA-256 calculation
SV11 source/W pre equality assertion before RunId
SV12 executable source/W post SHA-256 calculations
SV13 complete post equality assertions
SV14 exact-byte mismatch fails closed
SV15 G01-G11 precede RunId allocation
SV16 G01-G11 precede wrapper invocation
SV17 RunId allocation precedes wrapper invocation
SV18 executable S validation
SV19 executable W validation
SV20 executable child S visibility
SV21 executable child W visibility
SV22 child S/W mismatch fails closed
SV23 P03 binds to actual pre exact-byte evidence
SV24 P04 binds to actual pre/post exact-byte evidence
SV25 P19 occurs after disposable cleanup
SV26 P20 remains observation-only / production-derived
SV27 shim surface limited to git, az, required helper calls
SV28 unexpected calls fail closed
SV29 production-policy duplication absent
SV30 secret hygiene
SV31 disposable/durable roots independent
SV32 disposable root cleanup succeeds
SV33 retained runner survives cleanup
SV34 retained ledger survives cleanup
SV35 retained runner hash reverified after cleanup
SV36 repository invariants and fresh git diff --check evidence
```

# SV ledger schema

Produce exactly 36 unique records.

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

`Observed` must contain substantive, independently inspectable evidence.

For SV09 specifically, retain:

```text
resolved governed source path
source validation source reference
actual Copy-Item/file-copy source line
copy API/command
destination expression
proof destination is exact W
copy failure behavior
source-pre hash operation
W-pre hash operation
pre-RunId equality assertion
```

Generic topology prose does not satisfy SV09.

# P01–P20 structural contract

The new runner must structurally retain exactly:

```text
P01 Windows PowerShell version == 5.1.26100.9444
P02 parser errors == 0
P03 source/pre exact-byte identity == PASS
P04 source/pre/post exact-byte identity == PASS
P05 wrapper execution completed == PASS
P06 ArchiveRetrieval == RETRIEVAL_FAILED
P07 FreshExtraction == NOT_APPLICABLE
P08 EvidenceCheckpoint == PASS
P09 FinalLifecycleResult == SUCCESS
P10 RestoreOnly count == 1
P11 real external invocations == 0
P12 external-call ledger only governed intercepted calls == PASS
P13 repository modified-path scope unchanged == PASS
P14 staged paths == 0
P15 git diff --check == PASS
P16 secret hygiene == PASS
P17 durable sanitized evidence retention == PASS
P18 complete durable W5 scenario ledger retention == PASS
P19 disposable sandbox cleanup == PASS
P20 error precedence == PASS
```

This run is structural only:

```text
runtime P01-P20 PASS claims = 0
```

# P19 ordering

Require structural proof:

```text
P01-P18
→ P20
→ durable evidence persistence
→ delete DisposableRoot
→ verify DisposableRoot absent
→ evaluate P19
→ finalize P19 outside DisposableRoot
→ aggregate P01-P20
```

Do not recreate DisposableRoot after cleanup.

# P20

P20 observes production-derived error precedence.

Do not reimplement production archive/lifecycle policy in the runner.

# Git diff check

Use the proven operation-scoped Windows PowerShell 5.1 native-command capture.

Freshly retain:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
```

Capture `$LASTEXITCODE` immediately.

Known CRLF advisory text is acceptable only when:

```text
exit code = 0
classification = ADVISORY_ONLY
```

Restore the prior error-handling behavior immediately after the Git invocation.

# Cleanup and durable finalization

After all pre-cleanup evidence is durably retained:

1. delete DisposableRoot;
2. prove DisposableRoot no longer exists;
3. prove DurableRoot exists;
4. prove retained runner exists;
5. prove retained ledger exists;
6. reopen and parse retained ledger;
7. recompute retained runner SHA-256;
8. prove recomputed hash == new runner hash;
9. finalize post-cleanup evidence outside DisposableRoot.

# Repository boundary

Expected existing repository state:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Do not alter those scripts.

This authority authorizes:

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

Disposable/local evidence-file creation and cleanup are authorized.

# PASS threshold

PASS requires:

```text
new runner hash exists and differs from failed hash
actual byte-preserving source → W copy exists
SV09 independently inspectable = PASS
SV08-SV14 coherent against same source/W artifact
PowerShell exactly 5.1.26100.9444
parser errors = 0
SV records = exactly 36
SV failures = 0
required fields complete
evidence references resolve
validation methods complete
P01-P20 structural contract complete
runtime predicate PASS claims = 0
G01-G13 structural order = PASS
P19/P20 contracts = PASS
two-root lifecycle = PASS
post-cleanup hash reverified
fresh git diff --check = PASS
secret hygiene = PASS
repository mutation boundary = PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required success output

Only on full PASS emit:

```text
RELEASE 1.12 WP04 — R1 SV09 COPY-ITEM/FILE-COPY CORRECTION: PASS
RELEASE 1.12 WP04 — R1 OLD RUNNER HASH: C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
RELEASE 1.12 WP04 — R1 NEW RUNNER HASH: <NEW_SHA256>
RELEASE 1.12 WP04 — R1 EXECUTABLE BYTE-PRESERVING SOURCE-TO-W COPY: PASS
RELEASE 1.12 WP04 — R1 COPY DESTINATION IS EXACT W: PASS
RELEASE 1.12 WP04 — R1 COPY FAILURE FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 NEW-HASH SV01-SV36 DURABLE LEDGER: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNTIME P01-P20 PASS CLAIMS: 0
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
RELEASE 1.12 WP04 — TERRA SV09 CORRECTION EXECUTION COMPLETE
```

# Required handoff

Return at minimum:

```text
FreshDurableRoot
DisposableRoot
OldRunnerSHA256
NewRunnerSHA256
RetainedRunnerPath
exact corrected copy statement
SV09 source references
PowerShell version
parser error count
SVLedgerPath
SVRecordCount
SVFailedCount
P01P20DefinitionCount
RuntimePredicatePassClaims
SV08-SV14 coherence result
G01-G13 ordering result
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
exact mutation accounting
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
```

# Failure rule

At the first real failure:

- retain sanitized failure evidence when possible;
- report the exact failed gate/SV;
- classify the defect;
- state whether another runner hash is required;
- do not manufacture subsequent PASS claims;
- do not invoke W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP AFTER THE NEW DISPOSABLE RUNNER CORRECTION AND COMPLETE NEW-HASH SV01–SV36 LEDGER GENERATION.**

Even after PASS:

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

The next gate after a full Terra PASS is the existing:

```text
Release 1.12 WP04 — Luna R1 New-Hash SV01–SV36 Reconciliation Authority
```

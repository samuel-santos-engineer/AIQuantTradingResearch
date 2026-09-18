# Release 1.12 WP04 — Terra Execute SV09 Runner Correction & New-Hash Validation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the runner correction, disposable/local validation execution, and durable evidence generation authorized here.

GPT-5.6 Luna owns subsequent reconciliation and authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

# Binding current state

The latest Luna result is:

```text
RELEASE 1.12 WP04 — LUNA R1 NEW-HASH RECONCILIATION: NOT_READY
REASON: SV09 runner correction and a complete new-hash SV01-SV36 ledger were not supplied.
```

The only currently available structural observation ledger remains bound to failed runner:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

Known defect:

```text
SV09 — executable byte-for-byte wrapper copy exists
Classification: RUNNER_DEFECT
Production defect: NO
Fresh runner hash required: YES
```

Current governance:

```text
R1 new runner artifact: NOT_ACCEPTED
Single-artifact structural contract: NOT_ACCEPTED
Governed W5 execution: NOT_AUTHORIZED
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
WP04 #263: OPEN
Milestone #63: OPEN
WP05: NOT_STARTED
```

# Mission

**Perform the correction now.**

Do not stop after inspection, planning, or restating the defect.

Create a corrected disposable/local R1 runner artifact that actually copies the governed production wrapper byte-for-byte to the exact path represented by `$env:W`.

Then:

1. compute a new runner SHA-256;
2. create a fresh durable evidence candidate;
3. run the complete new-hash SV01–SV36 structural validation;
4. retain independently inspectable evidence;
5. clean the disposable root;
6. finalize post-cleanup evidence;
7. stop without executing governed W5.

# Narrow runner correction

The corrected runner must execute an actual Windows PowerShell 5.1-compatible byte-preserving file copy:

```text
governed production wrapper source
        ↓
byte-preserving file copy
        ↓
exact $env:W destination
```

Use a binary/file copy mechanism such as:

```powershell
[System.IO.File]::Copy(...)
```

or an equivalently byte-preserving Windows PowerShell 5.1-compatible file-copy operation.

Do not copy wrapper content through text pipelines.

Forbidden for wrapper copying:

```text
Get-Content | Set-Content
Out-File
text encoding conversion
newline normalization
regex/string transformation
templating
patching
```

# Required executable sequence

The corrected runner must structurally implement and prove:

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
G11 source SHA-256 == W pre-execution SHA-256
G12 allocate governed W5 RunId
G13 invoke exact W wrapper
```

For this structural validation run, execution must stop before G12.

No governed W5 RunId may be allocated.

No governed W5 wrapper may be invoked.

# Copy failure behavior

The executable copy must fail closed.

If source validation, copy, destination validation, hashing, or equality assertion fails:

```text
RunId allocation = NO
wrapper invocation = NO
structural validation = FAIL
```

# Exact artifact binding

The corrected runner must prove:

```text
copy destination == $env:W
pre-hash destination == $env:W
child-visible W == $env:W
future wrapper invocation target == $env:W
```

No alternate/secondary copied wrapper may satisfy SV09.

# Exact-byte contract

Preserve/revalidate:

```text
source-pre hash
W-pre hash
source-post hash
W-post hash
```

with future governed-run assertions:

```text
source-pre == W-pre
source-pre == source-post
W-pre == W-post
source-post == W-post
```

Mismatch fails closed.

# P03/P04 binding

Require:

```text
P03 -> actual source/W pre-execution identity evidence
P04 -> actual source/W pre/post identity evidence
```

No topology-only substitute.

# New hash boundary

Because runner source changes:

```text
old hash C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
= HISTORICAL FAILED ARTIFACT
```

The corrected runner must receive a different SHA-256.

Do not carry structural PASS claims from the old hash.

# Fresh roots

Create:

```text
DisposableRoot = fresh local temporary validation root
DurableRoot = fresh %LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<guid>
```

Require:

```text
DisposableRoot != DurableRoot
DurableRoot not descendant of DisposableRoot
```

Preserve all historical durable roots.

# Complete SV01–SV36 validation

Validate the new runner artifact from scratch.

Produce exactly 36 records:

```text
SV01 ... SV36
```

Each record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Every observation must be independently inspectable.

Every evidence reference must resolve against the retained new-hash runner or retained fresh durable evidence.

## Required checks

```text
SV01 exact Windows PowerShell 5.1.26100.9444
SV02 parser errors = 0
SV03 exactly P01-P20 definitions
SV04 predicate fields complete
SV05 all P01-P20 included in aggregation
SV06 unresolved predicates fail closed
SV07 zero runtime predicate PASS claims
SV08 executable source-wrapper SHA-256 calculation
SV09 executable byte-preserving source → W copy
SV10 executable W pre SHA-256 calculation
SV11 source/W pre equality assertion before RunId
SV12 executable source/W post hashes
SV13 post equality assertions
SV14 mismatch fail closed
SV15 G01-G11 before RunId allocation
SV16 G01-G11 before wrapper invocation
SV17 RunId before wrapper invocation
SV18 executable S validation
SV19 executable W validation
SV20 executable child S visibility
SV21 executable child W visibility
SV22 child S/W mismatch fail closed
SV23 P03 binding
SV24 P04 binding
SV25 P19 post-cleanup ordering
SV26 P20 observation-only/error-precedence contract
SV27 minimum shim surface
SV28 unexpected calls fail closed
SV29 no production-policy duplication
SV30 secret hygiene
SV31 independent disposable/durable roots
SV32 disposable cleanup
SV33 runner survives cleanup
SV34 ledger survives cleanup
SV35 runner hash reverified after cleanup
SV36 repository invariants and fresh git diff --check capture
```

# SV09 evidence threshold

SV09 must retain concrete observations for:

```text
source path
source validation
copy statement/source line
copy API/operation
destination expression
proof destination == W
copy failure handling
```

Generic statements such as:

```text
copy exists
wrapper copied
topology correct
```

are insufficient.

# Canonical P01–P20 structural definitions

The new runner must structurally define exactly:

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

During this structural run:

```text
runtime P01-P20 PASS claims = 0
```

# P19 ordering

Prove structurally:

```text
P01-P18
→ P20
→ durable evidence persistence
→ disposable cleanup
→ verify disposable absent
→ evaluate P19
→ persist/finalize P19 outside disposable root
→ aggregate P01-P20
```

# P20

P20 must observe production-derived error precedence.

Do not reimplement production policy.

# Shim boundary

Allowed interception surface remains only:

```text
git
az
required helper calls
```

Unexpected external calls fail closed.

# Git diff capture

Use the already-proven operation-scoped Windows PowerShell 5.1 mechanism.

Freshly record:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
```

Capture `$LASTEXITCODE` immediately.

Known CRLF advisory output is acceptable only with actual exit code 0 and classification `ADVISORY_ONLY`.

Restore error handling immediately after the native command.

# Two-root finalization

After structural validation:

1. persist corrected runner under DurableRoot;
2. persist new runner SHA-256;
3. persist complete SV01–SV36 ledger;
4. persist supporting source/reference evidence;
5. remove DisposableRoot;
6. verify DisposableRoot absent;
7. verify DurableRoot present;
8. verify runner present;
9. verify ledger present;
10. reopen/parse ledger;
11. recompute runner SHA-256;
12. finalize SV32–SV35 post-cleanup evidence durably.

Do not recreate DisposableRoot after cleanup.

# Repository and mutation boundary

Expected repository baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Do not edit, stage, revert, clean, commit, or push repository content.

Authorized mutations are limited to disposable/local evidence artifacts.

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

# Acceptance threshold

Terra may report PASS only when:

```text
new runner hash != old failed hash
runner parser errors = 0
SV records = exactly 36
SV01-SV36 unique
required fields complete
independent observations complete
evidence references resolvable
validation methods complete
SV failures = 0
SV09 executable byte-preserving copy = PASS
SV08-SV14 coherence = PASS
P01-P20 structural contract = PASS
runtime P01-P20 PASS claims = 0
pre-RunId boundary = PASS
S/W child visibility contract = PASS
P19 ordering = PASS
P20 contract = PASS
shim/no-policy-duplication = PASS
two-root lifecycle = PASS
fresh git diff --check = PASS
secret hygiene = PASS
repository mutation boundary = PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required success markers

Only on complete PASS emit:

```text
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE WRAPPER-COPY CORRECTION: PASS
RELEASE 1.12 WP04 — R1 OLD RUNNER HASH: C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
RELEASE 1.12 WP04 — R1 NEW RUNNER HASH: <NEW_SHA256>
RELEASE 1.12 WP04 — R1 SV09 BYTE-PRESERVING SOURCE-TO-W COPY: PASS
RELEASE 1.12 WP04 — R1 SV09 COPY DESTINATION BINDS TO W: PASS
RELEASE 1.12 WP04 — R1 SV09 COPY FAILURE FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE CONTROL COHERENCE: PASS
RELEASE 1.12 WP04 — R1 NEW-HASH SV01-SV36 DURABLE-LEDGER VALIDATION: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 CHILD S/W VISIBILITY: PASS
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
RELEASE 1.12 WP04 — TERRA SV09 CORRECTION AND NEW-HASH VALIDATION COMPLETE
```

# Required handoff report

Return:

```text
FreshDurableRoot
DisposableRoot
OldRunnerSHA256
NewRunnerSHA256
RetainedRunnerPath
exact runner-source correction
SV09 source references
PowerShell version
parser error count
SV ledger path
SV record count
SV failed count
P01-P20 definition count
runtime predicate PASS claim count
SV08-SV14 coherence result
G01-G13 ordering result
git diff --check exit code/output/classification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
modified tracked paths before/after
staged paths before/after
exact mutation accounting
governed W5 wrapper invoked
governed W5 RunId allocated
```

# Failure boundary

At the first real failure:

- retain sanitized failure evidence under the fresh durable root when possible;
- identify exact failed SV/gate;
- classify runner/harness/evidence/repository/contract defect;
- state whether another fresh runner hash is required;
- do not manufacture later PASS claims;
- do not execute W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP AFTER THE SV09 CORRECTION AND COMPLETE NEW-HASH SV01–SV36 VALIDATION.**

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

After PASS, execute the existing:

```text
Release 1.12 WP04 — Luna R1 New-Hash SV01–SV36 Reconciliation Authority
```

against the newly retained candidate.

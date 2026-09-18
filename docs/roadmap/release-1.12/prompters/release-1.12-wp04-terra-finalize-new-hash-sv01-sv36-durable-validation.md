# Release 1.12 WP04 — Terra Finalize New-Hash SV01–SV36 Durable Validation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the disposable/local validation execution and durable evidence generation authorized here.

GPT-5.6 Luna owns subsequent reconciliation, acceptance, governance, and any authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

# Binding predecessor state

The SV09 source correction has now been created in a disposable runner.

The corrected executable operation is:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

The corrected runner also verifies source-wrapper / `$env:W` SHA-256 equality before a future governed W5 RunId could be allocated.

However:

```text
new-hash durable SV01-SV36 validation: NOT_COMPLETED
R1 structural acceptance: NOT_GRANTED
governed W5 wrapper invoked: NO
governed W5 RunId allocated: NO
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
publication blocker: UNRESOLVED
WP04 #263: OPEN
milestone #63: OPEN
WP05: NOT_STARTED
```

Historical failed runner:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

Historical failure evidence remains immutable.

# Mission

Do **not** redesign SV09.

Take the already-created corrected disposable runner, verify its exact bytes, assign its new SHA-256, retain that exact artifact durably, and complete the missing full new-hash SV01–SV36 structural validation.

This authority exists specifically to finish the validation that has not yet been completed.

No acceptance claim may be made until the complete durable ledger passes.

# Initial runner gate

Before creating acceptance evidence:

1. identify the corrected disposable runner path;
2. prove it exists;
3. parse it using Windows PowerShell 5.1;
4. verify the executable statement exists:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

5. verify `$SourceWrapper` is the governed wrapper source;
6. verify `$env:W` is the exact destination used by the future child/wrapper invocation;
7. verify source/W pre-hash equality occurs before any future RunId allocation;
8. compute the corrected runner SHA-256.

Require:

```text
NewRunnerSHA256 != C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

If any initial gate fails, retain sanitized failure evidence and STOP.

# Freeze corrected runner identity

Once the corrected runner SHA-256 is computed, treat those bytes as frozen for this validation.

Do not edit the runner after the new hash is established.

If any runner edit becomes necessary:

```text
current validation candidate = INVALIDATED
new hash = REQUIRED
full SV01-SV36 validation = RESTART_REQUIRED
```

No cross-hash carry-forward.

# Fresh durable candidate

Create a new durable evidence root:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Retain:

```text
corrected runner exact bytes
runner SHA-256
PowerShell/parser evidence
SV01-SV36 observation ledger
supporting source/control-flow evidence
fresh git diff --check evidence
sanitized validation summary
```

The durable root must be independent of the disposable working root.

Preserve all historical durable roots.

# Windows PowerShell contract

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Parser errors for the frozen corrected runner:

```text
0
```

Do not use PowerShell 7-only behavior.

# Complete SV01–SV36 validation

Generate exactly 36 unique structural records for the frozen new-hash runner:

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

Every `Observed` value must be independently inspectable.

Every `EvidenceReference` must resolve to the retained corrected runner or fresh durable evidence.

Generic `PASS`, `true`, topology prose, or restatement of `Expected` is insufficient where source/control-flow/hash/path evidence is required.

## Required checks

```text
SV01 Windows PowerShell exactly 5.1.26100.9444
SV02 corrected frozen runner parser errors = 0
SV03 exactly canonical P01-P20 definitions
SV04 predicate fields complete
SV05 all P01-P20 participate in final aggregation
SV06 unresolved predicates fail closed
SV07 runtime predicate PASS claims during structural validation = 0
SV08 executable governed-source SHA-256 calculation
SV09 executable byte-preserving governed-source → W file copy
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

# SV09 required observation

SV09 must now cite the actual frozen corrected source.

At minimum retain:

```text
SourceWrapper resolution reference
SourceWrapper validation reference
[IO.File]::Copy($SourceWrapper, $env:W, $true) source reference
destination binding to $env:W
copy exception/failure behavior
source-pre SHA-256 source reference
W-pre SHA-256 source reference
pre-RunId equality assertion source reference
```

The validation must explicitly classify the copy as file/binary byte-preserving behavior, not text transformation.

# SV08–SV14 coherence

Prove all exact-byte controls refer to the same pair:

```text
governed production wrapper source
$env:W copied wrapper
```

Require structural future-run assertions:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

Mismatch fails closed.

# G01–G13 ordering

Retain independently inspectable source/control-flow evidence proving:

```text
G01 exact WinPS version
G02 parser
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute
G08 W expected/fresh
G09 source wrapper validated
G10 source → W file copy
G11 source/W pre-hash equality
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

Require:

```text
G01-G11 < G12 < G13
```

For this authority, G12 and G13 are structural references only.

They must **not execute**.

# Canonical P01–P20 structure

Require exactly:

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

Structural validation must claim:

```text
runtime P01-P20 PASS claims = 0
```

# P19 ordering

Prove structurally:

```text
P01-P18
→ P20
→ persist durable evidence outside disposable root
→ delete disposable root
→ verify disposable root absent
→ evaluate P19
→ finalize P19 outside disposable root
→ aggregate P01-P20
```

Do not recreate the disposable root after cleanup.

# P20

P20 must observe/account for production-derived error precedence.

The runner must not duplicate production persistence/archive/lifecycle policy.

# Shim boundary

Permitted interception surface:

```text
git
az
required helper calls
```

Unexpected calls must fail closed.

# Fresh Git evidence

Use the already-proven operation-scoped Windows PowerShell 5.1 native capture.

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

Restore prior error-handling behavior immediately after the Git command.

# Repository invariant

Expected baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Record before/after:

```text
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
AuthorityIntroducedTrackedMutations
```

Do not edit, stage, revert, clean, commit, or push repository content.

# Durable cleanup/finalization

After pre-cleanup structural evidence is retained durably:

1. remove the disposable validation root;
2. prove disposable root absent;
3. prove fresh durable root exists;
4. prove retained frozen runner exists;
5. prove retained SV ledger exists;
6. reopen and parse retained ledger;
7. recompute retained runner SHA-256;
8. prove it equals `NewRunnerSHA256`;
9. finalize SV32–SV35 observations durably outside the disposable root.

# Mutation boundary

Authorized:

```text
disposable/local runner/evidence creation
fresh durable local evidence creation
disposable cleanup
```

Forbidden:

```text
tracked repository mutations
staging
commit
push
GitHub mutation
production mutation
Azure mutation
Docker/GHCR mutation
governed W5 execution
governed W5 RunId allocation
```

Required counts:

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

Terra may emit PASS only if all are true:

```text
corrected runner frozen
new runner hash != historical failed hash
PowerShell = 5.1.26100.9444
parser errors = 0
SV record count = 36
SV IDs exactly SV01-SV36
required-field failures = 0
independent-observation failures = 0
evidence-reference failures = 0
validation-method failures = 0
SV failures = 0
SV09 actual [IO.File]::Copy source-to-W evidence = PASS
SV08-SV14 coherence = PASS
G01-G13 structural ordering = PASS
P01-P20 structural contract = PASS
runtime P01-P20 PASS claims = 0
P19 ordering = PASS
P20 evidence contract = PASS
shim/no-policy-duplication = PASS
two-root lifecycle = PASS
post-cleanup runner hash reverified
fresh git diff --check = PASS
secret hygiene = PASS
repository invariant = PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required success markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — R1 CORRECTED RUNNER FROZEN: PASS
RELEASE 1.12 WP04 — R1 OLD RUNNER HASH: C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
RELEASE 1.12 WP04 — R1 NEW RUNNER HASH: <NEW_SHA256>
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE IO.FILE COPY: PASS
RELEASE 1.12 WP04 — R1 SV09 SOURCE-TO-W BYTE PRESERVATION: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 NEW-HASH SV01-SV36 DURABLE VALIDATION: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
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
RELEASE 1.12 WP04 — TERRA NEW-HASH DURABLE STRUCTURAL VALIDATION COMPLETE
```

# Required handoff report

Return:

```text
DisposableRunnerPath
DisposableRoot
FreshDurableRoot
OldRunnerSHA256
NewRunnerSHA256
RetainedRunnerPath
ExactCopyStatement
PowerShellVersion
ParserErrorCount
SVLedgerPath
SVRecordCount
SVFailedCount
RequiredFieldFailures
IndependentObservationFailures
EvidenceReferenceFailures
ValidationMethodFailures
P01P20DefinitionCount
RuntimePredicatePassClaims
SV08SV14Coherence
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

At the first failure:

- retain sanitized durable failure evidence when possible;
- identify exact first failed gate/SV;
- classify the defect;
- state whether another runner hash is required;
- do not manufacture later PASS claims;
- do not execute W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP AFTER COMPLETING THE NEW-HASH DURABLE SV01–SV36 VALIDATION OR RETAINING THE FIRST REAL FAILURE.**

Even after a complete Terra PASS:

```text
Luna reconciliation = REQUIRED
governed W5 execution = NOT_AUTHORIZED YET
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
publication blocker = UNRESOLVED
```

After PASS, execute the existing:

```text
Release 1.12 WP04 — Luna R1 New-Hash SV01–SV36 Reconciliation Authority
```

against the fresh durable candidate.

# Release 1.12 WP04 — Luna Final Self-Contained Runner SV01–SV36 Reconciliation

## Authority identity

**Selected execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns read-only contract reconciliation, acceptance criteria, governance, and authorization of the next phase.

GPT-5.6 Terra owns implementation, validation execution, and explicitly authorized mutations.

GPT-5.6 Sol is supporting analysis only and must not silently replace Luna or Terra.

# Invocation precondition

Execute this authority **only after** GPT-5.6 Terra completes:

```text
Release 1.12 WP04 — Terra Complete Self-Contained Runner & SV01–SV36 Durable Ledger
```

and supplies one finalized durable candidate containing:

```text
one retained runner artifact
one frozen runner SHA-256
canonical P01-P20 structural accounting
fail-closed P01-P20 aggregation
executable byte-preserving governed-wrapper → $env:W copy
complete SV01-SV36 durable observation ledger
post-cleanup durable evidence
fresh repository/Git evidence
```

If the Terra authority has not completed, STOP:

```text
RELEASE 1.12 WP04 — LUNA FINAL SELF-CONTAINED RUNNER RECONCILIATION: NOT_READY
REASON: FINALIZED RUNNER AND COMPLETE DURABLE SV01-SV36 LEDGER NOT SUPPLIED
```

Do not infer completion from the presence of `runner.ps1` alone.

# Historical failure boundaries

Historical failed runner hashes include:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

Historical incomplete candidate root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7de8206cd44e4ab69e8c1139b9a9ecc7
```

Historical artifacts are evidence only.

No PASS may be composed across different runner hashes.

# Current governance

Before this reconciliation:

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

Read-only reconcile the final Terra durable candidate.

The acceptance question is:

> Does one exact retained runner hash independently prove the complete R1 structural contract required to authorize a separate governed W5 runtime execution?

No mutation is authorized.

# Required Terra handoff

Require at minimum:

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

Missing required handoff evidence fails closed.

# R01 — Final artifact identity

Verify:

```text
FinalRunnerSHA256 exists
retained runner exists
retained ledger exists
retained ledger parses
ledger binds to FinalRunnerSHA256
independent retained-runner SHA-256 == FinalRunnerSHA256
RunnerHashAfterCleanup == FinalRunnerSHA256
```

If the final runner differs from historical candidates, require the new hash to be distinct.

No cross-hash carry-forward.

# R02 — Windows PowerShell/parser

Require:

```text
PowerShellVersion = 5.1.26100.9444
ParserErrorCount = 0
```

# R03 — Complete SV identity

Require exactly:

```text
SVRecordCount = 36
IDs = SV01-SV36
missing = 0
duplicates = 0
extras = 0
SVFailedCount = 0
```

# R04 — SV record schema

Every SV record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Missing/null/unresolved structural evidence fields fail.

# R05 — Independent observations

Every `Observed` value must contain substantive independently inspectable evidence appropriate to the check.

Reject generic assertions such as:

```text
PASS
true
contract satisfied
topology present
```

where executable source, control-flow, path, hash, ledger, or post-cleanup evidence is required.

# R06 — Evidence references and methods

Require:

```text
every EvidenceReference resolves
every ValidationMethod is explicit
executable behavior cites retained source or equivalent parsed evidence
durable lifecycle observations cite retained durable evidence
```

Comments alone are insufficient for executable behavior.

# R07 — Canonical P01–P20 definitions

Require exactly 20 definitions in the same retained runner:

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

Require:

```text
CanonicalPredicateCount = 20
missing = 0
duplicates = 0
extras = 0
```

# R08 — Predicate fields

Require every P01-P20 definition to structurally support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Equivalent field names require explicit lossless mapping.

# R09 — Exact aggregation membership

Require:

```text
AggregationMembershipCount = 20
AggregationMissingIds = none
AggregationDuplicateIds = none
extras = none
```

Aggregation must operate on exactly P01-P20.

# R10 — Unresolved fail-closed behavior

Verify executable/structural logic proves:

```text
missing predicate -> fail
duplicate predicate -> fail
unresolved predicate -> not accepted/fail closed
failed predicate -> fail
only all 20 resolved PASS -> ALL_PASS
```

# R11 — Zero structural-run runtime PASS claims

Require:

```text
RuntimePredicatePassClaims = 0
```

Structural definition/validation is not W5 runtime acceptance.

# R12 — SV08-SV14 exact-byte contract

Verify one coherent artifact pair:

```text
governed production wrapper source
exact copied wrapper at $env:W
```

Require:

```text
SV08 source SHA-256 calculation
SV09 executable byte-preserving source → W copy
SV10 W-pre SHA-256
SV11 source-pre == W-pre before RunId
SV12 source/W post hashes
SV13 complete post equality assertions
SV14 mismatch fail closed
```

# R13 — SV09 executable copy

Require independently inspectable executable behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

Verify:

```text
SourceWrapper is governed source
source validated before copy
destination is exact $env:W
copy is file/binary byte-preserving
copy failure fails closed
no text transformation
```

# R14 — Exact-byte artifact coherence

Verify all SV08-SV14 controls refer to the same governed source and same `$env:W`.

Require future-run structural assertions:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

# R15 — G01-G13 ordering

Verify:

```text
G01 exact Windows PowerShell
G02 parser
G03 S nonempty
G04 S absolute
G05 S fresh/existing
G06 W nonempty
G07 W absolute
G08 W expected/fresh
G09 source validated
G10 source → W byte-preserving copy
G11 source/W pre-hash equality
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

Require:

```text
G01-G11 < G12 < G13
```

# R16 — S/W child boundary

Verify executable evidence proves:

```text
S exact validation
W exact validation
child receives exact S
child receives exact W
child mismatch fails before G12/G13
```

# R17 — P03/P04 binding

Require:

```text
P03 -> actual SV08-SV11 pre exact-byte evidence
P04 -> actual SV12-SV14 post exact-byte evidence
```

# R18 — P19 post-cleanup ordering

Verify:

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

# R19 — P20 evidence contract

Verify P20 observes/accounts for production-derived error precedence.

Reject runner-side production-policy duplication.

# R20 — Shim boundary

Require interception surface limited to:

```text
git
az
required helper calls
```

Unexpected calls fail closed.

# R21 — No production-policy duplication

Verify runner does not reimplement persistence/archive/lifecycle semantics to manufacture W5 results.

# R22 — Secret hygiene

Verify retained source/evidence contains no exposed secrets.

Do not print secret values.

# R23 — Two-root durable lifecycle

Require:

```text
DisposableRoot != FreshDurableRoot
FreshDurableRoot not descendant of DisposableRoot
DisposableExistsAfterCleanup = False
DurableExistsAfterCleanup = True
RunnerExistsAfterCleanup = True
LedgerExistsAfterCleanup = True
RunnerHashAfterCleanup == FinalRunnerSHA256
```

# R24 — Fresh Git/repository invariant

Require fresh evidence:

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

Expected baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Known CRLF advisory output is acceptable only when:

```text
GitDiffCheckExitCode = 0
GitDiffCheckClassification = ADVISORY_ONLY
```

# R25 — Mutation accounting

Require Terra introduced:

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

Disposable/local evidence creation is permitted.

# R26 — W5 execution boundary

Require:

```text
GovernedW5WrapperInvoked = NO
GovernedW5RunIdAllocated = NO
runtime P01-P20 execution = NO
RuntimePredicatePassClaims = 0
```

# Luna decision

## Decision A — ACCEPT

Select only if R01-R26 all PASS.

Emit:

```text
RELEASE 1.12 WP04 — LUNA FINAL SELF-CONTAINED RUNNER RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 FINAL RUNNER ARTIFACT: ACCEPTED
RELEASE 1.12 WP04 — R1 FINAL RUNNER HASH: VERIFIED
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: ACCEPTED
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED FIELDS: ACCEPTED
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION MEMBERSHIP: ACCEPTED
RELEASE 1.12 WP04 — R1 UNRESOLVED PREDICATE FAIL-CLOSED CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING COPY: ACCEPTED
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: ACCEPTED
RELEASE 1.12 WP04 — R1 SV01-SV36 SINGLE-ARTIFACT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 PRE-RUNID FAIL-CLOSED BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — R1 CHILD S/W VISIBILITY: ACCEPTED
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: ACCEPTED
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 TWO-ROOT DURABLE EVIDENCE LIFECYCLE: ACCEPTED
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1 CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK CAPTURE CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 REPOSITORY MUTATION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_FOR_SEPARATE_TERRA_AUTHORITY
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Then STOP.

This authorization does not execute W5.

The next artifact is a separate GPT-5.6 Terra governed W5 runtime execution authority.

## Decision B — REJECT

If any R01-R26 gate fails, emit:

```text
RELEASE 1.12 WP04 — LUNA FINAL SELF-CONTAINED RUNNER RECONCILIATION: FAIL
RELEASE 1.12 WP04 — R1 FINAL RUNNER ARTIFACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Report:

```text
first failed reconciliation gate
first failed SV if applicable
exact deficient retained evidence
classification:
  RUNNER_DEFECT
  STRUCTURAL_EVIDENCE_DEFECT
  HARNESS_DEFECT
  REPOSITORY_INVARIANT_DEFECT
  CONTRACT_AMBIGUITY
production defect: YES/NO
fresh runner hash required: YES/NO
narrowest authorized next correction
```

Do not authorize W5.

# Prohibitions

Luna must not:

- edit repository files;
- edit/regenerate the runner;
- regenerate the ledger;
- execute W5;
- allocate a W5 RunId;
- invoke Azure/Docker/GHCR;
- stage/commit/push;
- mutate GitHub;
- close #263;
- set Project #2 Done;
- begin WP05;
- infer missing evidence;
- combine evidence across runner hashes.

# Final state

Regardless of outcome:

```text
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
W5 acceptance = NOT_GRANTED until a governed runtime W5 execution passes P01-P20
W6/W7/W8 = NOT_RUN
Publication blocker = UNRESOLVED
```

# Final stop

**STOP AFTER LUNA FINAL SELF-CONTAINED RUNNER RECONCILIATION.**

If Decision A is reached, create a separate **GPT-5.6 Terra governed W5 runtime execution authority** as the next Markdown artifact.

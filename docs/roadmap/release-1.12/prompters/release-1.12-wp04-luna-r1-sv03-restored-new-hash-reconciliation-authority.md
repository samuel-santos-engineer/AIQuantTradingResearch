# Release 1.12 WP04 — Luna R1 SV03-Restored New-Hash Reconciliation Authority

## Authority identity

**Selected execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns contract interpretation, reconciliation, acceptance criteria, governance, and authorization of the next phase.

GPT-5.6 Terra owns implementation, validation execution, and explicitly authorized mutations.

GPT-5.6 Sol is supporting analysis only and must not silently replace Luna or Terra.

# Invocation precondition

Execute this authority **only after** GPT-5.6 Terra completes:

```text
Release 1.12 WP04 — Terra R1 SV03 Canonical P01–P20 Restoration Authority
```

and supplies a finalized fresh candidate containing:

```text
one new runner artifact
one new runner SHA-256
canonical P01-P20 structural definitions
fail-closed P01-P20 aggregation
executable byte-preserving source → $env:W copy
complete fresh SV01-SV36 durable ledger
```

If that Terra execution has not completed, STOP:

```text
RELEASE 1.12 WP04 — LUNA R1 SV03-RESTORED RECONCILIATION: NOT_READY
REASON: NEW SELF-CONTAINED RUNNER AND COMPLETE SV01-SV36 LEDGER NOT SUPPLIED
```

Do not infer completion from the Terra authority being loaded.

# Binding historical failures

Historical failed runner with missing SV09 control:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

Historical runner with SV09 corrected but SV03 missing:

```text
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

Historical SV03 failure evidence:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7a65ccaf4ee94e78ba16d3959d91ecbe\failure-ledger.json
```

Both hashes are historical failure evidence only.

No structural PASS credit may be composed across hashes.

# Current governance before reconciliation

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

Perform a read-only reconciliation of the new self-contained R1 runner artifact.

Determine whether one exact new runner hash independently proves the complete structural contract:

```text
SV01-SV36
canonical P01-P20 definitions
required predicate fields
exact aggregation membership
unresolved fail-closed behavior
zero structural-run runtime PASS claims
SV08-SV14 exact-byte controls
SV09 executable source → W copy
G01-G13 ordering
S/W child visibility
P03/P04 bindings
P19 ordering
P20 observation-only contract
minimum shim surface
no production-policy duplication
secret hygiene
two-root lifecycle
fresh Git/repository invariants
zero prohibited mutations
zero W5 execution/RunId allocation
```

This authority is read-only.

# Required Terra handoff

Require at minimum:

```text
FreshDurableRoot
DisposableRoot
OldRunnerSHA256
NewRunnerSHA256
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

Missing required handoff evidence fails closed.

# R01 — New artifact identity

Verify:

```text
NewRunnerSHA256 exists
NewRunnerSHA256 != C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
NewRunnerSHA256 != 19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
retained runner exists
retained ledger exists
retained ledger parses
ledger binds to NewRunnerSHA256
independent retained-runner hash recomputation == NewRunnerSHA256
```

No cross-hash evidence composition.

# R02 — Windows PowerShell/parser

Require:

```text
Windows PowerShell = 5.1.26100.9444
ParserErrorCount = 0
```

# R03 — SV cardinality

Require exactly:

```text
36 records
SV01-SV36
missing = 0
duplicates = 0
extras = 0
```

# R04 — SV evidence schema

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

Verify each `Observed` field contains substantive independently inspectable evidence.

Reject generic assertions such as:

```text
PASS
true
contract exists
topology valid
```

where source, control-flow, path, hash, or durable observation evidence is required.

# R06 — Evidence references/methods

Require:

```text
every EvidenceReference resolves
every ValidationMethod is explicit
source/control-flow checks cite executable retained source or equivalent parsed evidence
```

Comments alone are insufficient for executable behavior.

# R07 — SV03 canonical P01–P20 definitions

This is the formerly failed gate.

Require exactly 20 canonical definitions in the same new runner:

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
count = 20
IDs exactly P01-P20
missing = 0
duplicates = 0
extras = 0
```

# R08 — SV04 predicate fields

Every P01–P20 definition must structurally support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Equivalent fields require explicit lossless mapping.

Structural definitions must not manufacture runtime results.

# R09 — SV05 aggregation membership

Verify the runner's final predicate aggregation includes exactly:

```text
P01 ... P20
```

Require:

```text
membership count = 20
missing = 0
duplicates = 0
extras = 0
```

# R10 — SV06 unresolved fail-closed

Verify executable/structural logic proves:

```text
missing predicate -> fail
duplicate predicate -> fail
unresolved predicate -> not accepted/fail closed
failed predicate -> fail
all 20 resolved PASS -> only state eligible for ALL_PASS
```

# R11 — SV07 zero runtime PASS claims

Require structural validation evidence:

```text
RuntimePredicatePassClaims = 0
```

Definition presence is not runtime acceptance.

# R12 — SV08–SV14 exact-byte controls

Verify one coherent artifact pair:

```text
governed production wrapper source
exact $env:W copied wrapper
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

# R13 — SV09 executable copy preservation

Require the new self-contained runner still contains executable behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

Verify:

```text
SourceWrapper is governed source
source validated
destination == exact $env:W
copy is file/binary byte-preserving
copy failure fails closed
no text transformation
```

SV03 restoration must not regress SV09.

# R14 — G01–G13 ordering

Verify:

```text
G01-G11 < G12 < G13
```

with:

```text
G10 actual source → W file copy
G11 source/W pre-hash equality
G12 governed W5 RunId allocation
G13 exact W wrapper invocation
```

All G01–G11 failures must prevent G12/G13.

# R15 — S/W boundary

Verify executable evidence proves:

```text
S nonempty/absolute/fresh/existing
W nonempty/absolute/expected
W is exact copied-wrapper destination
child receives exact S
child receives exact W
child mismatch fails before RunId/wrapper
```

# R16 — P03/P04 bindings

Require:

```text
P03 -> actual SV08-SV11 pre exact-byte evidence
P04 -> actual SV12-SV14 post exact-byte evidence
```

No generic substitution.

# R17 — P19 ordering

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

# R18 — P20 contract

Verify P20 observes production-derived error precedence.

Reject production-policy duplication.

# R19 — Shim boundary

Require only:

```text
git
az
required helper calls
```

Unexpected calls fail closed.

# R20 — No production-policy duplication

Verify the runner does not reimplement persistence/archive/lifecycle semantics merely to manufacture expected W5 results.

# R21 — Secret hygiene

Verify retained source/ledger/evidence contains no exposed secrets.

Do not print secret values during reconciliation.

# R22 — Two-root lifecycle

Require:

```text
DisposableRoot != FreshDurableRoot
FreshDurableRoot not descendant of DisposableRoot
DisposableExistsAfterCleanup = False
DurableExistsAfterCleanup = True
RunnerExistsAfterCleanup = True
LedgerExistsAfterCleanup = True
RunnerHashAfterCleanup == NewRunnerSHA256
```

# R23 — Fresh Git/repository evidence

Require fresh evidence containing:

```text
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
AuthorityIntroducedTrackedMutations
```

Expected baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Known CRLF advisories are acceptable only with:

```text
GitDiffCheckExitCode = 0
GitDiffCheckClassification = ADVISORY_ONLY
```

# R24 — Mutation boundary

Require:

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

Local disposable/durable evidence creation is permitted.

# R25 — W5 boundary

Require:

```text
GovernedW5WrapperInvoked = NO
GovernedW5RunIdAllocated = NO
runtime P01-P20 execution = NO
RuntimePredicatePassClaims = 0
```

Structural validation must not be treated as W5 acceptance.

# Luna decision

## Decision A — ACCEPT

Select only if R01–R25 all PASS.

Emit:

```text
RELEASE 1.12 WP04 — LUNA R1 SV03-RESTORED NEW-HASH RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 NEW SELF-CONTAINED RUNNER: ACCEPTED
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

`AUTHORIZED_FOR_SEPARATE_TERRA_AUTHORITY` does not execute W5.

A separate Terra W5 runtime authority is required.

## Decision B — REJECT

If any R01–R25 gate fails, emit:

```text
RELEASE 1.12 WP04 — LUNA R1 SV03-RESTORED NEW-HASH RECONCILIATION: FAIL
RELEASE 1.12 WP04 — R1 NEW SELF-CONTAINED RUNNER: NOT_ACCEPTED
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Then report:

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
W5 acceptance = NOT_GRANTED until a governed runtime W5 run passes P01-P20
W6/W7/W8 = NOT_RUN
Publication blocker = UNRESOLVED
```

# Final stop

**STOP AFTER LUNA SV03-RESTORED NEW-HASH RECONCILIATION.**

If Decision A is reached, the next Markdown artifact is a separate **GPT-5.6 Terra governed W5 runtime execution authority**.

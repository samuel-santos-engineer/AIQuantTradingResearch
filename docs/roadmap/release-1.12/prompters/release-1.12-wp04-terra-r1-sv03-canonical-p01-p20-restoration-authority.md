# Release 1.12 WP04 — Terra R1 SV03 Canonical P01–P20 Restoration Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this narrow disposable runner correction and complete structural revalidation.

GPT-5.6 Luna owns subsequent reconciliation, acceptance, governance, and authorization of any governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

# Binding predecessor

Latest candidate:

```text
Runner SHA-256:
19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C

Parser errors:
0

SV09 executable byte-preserving copy:
PRESENT

First failed structural gate:
SV03 — exactly canonical P01-P20 definitions

Classification:
RUNNER_DEFECT

Production defect:
NO

Fresh runner hash required:
YES
```

Failure evidence:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\7a65ccaf4ee94e78ba16d3959d91ecbe\failure-ledger.json
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

# Mission

Correct the disposable R1 runner so the **same single runner artifact** contains both:

1. the already-proven executable SV09 source → `$env:W` byte-preserving copy control; and
2. the complete canonical P01–P20 structural definition/accounting contract.

Then assign a **new runner hash** and restart the complete SV01–SV36 validation from SV01.

Do not patch only the ledger.

Do not combine the P01–P20 structure from an older runner with the SV09 control from `19E551...`.

The corrected artifact must be self-contained.

# Preserve SV09 correction

The new runner must retain executable behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

and preserve:

```text
governed SourceWrapper validation
exact destination == $env:W
byte-preserving file copy
copy failure fails closed
source-pre SHA-256
W-pre SHA-256
source-pre == W-pre before RunId allocation
future source/W post-hash contract
```

No regression of SV08–SV14 is permitted.

# SV03 correction

Add/restore the canonical predicate definitions directly into the runner's structural contract.

The runner must define **exactly 20 canonical predicates**:

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

# Predicate structural representation

Each predicate definition must be structurally inspectable and contain at least:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Equivalent field names are acceptable only if the mapping is explicit and lossless.

At structural-validation time:

```text
Observed = unresolved/not-executed structural state
Result = NOT_RUN / UNRESOLVED equivalent
```

Do **not** claim runtime predicate PASS.

# SV04 required-field contract

The runner must prove every P01–P20 record supports the required runtime accounting fields.

Missing/null structural definition fields fail closed.

# SV05 aggregation membership

The runner must explicitly aggregate exactly P01–P20.

Require structural evidence proving:

```text
aggregation membership count = 20
IDs = exactly P01 ... P20
missing = 0
duplicates = 0
extras = 0
```

The aggregate may report runtime success only when all 20 runtime predicate records have resolved PASS.

# SV06 unresolved fail-closed behavior

Before a governed W5 runtime execution, unresolved predicates must prevent aggregate acceptance.

Structurally prove:

```text
any missing predicate -> FAIL
any duplicate predicate -> FAIL
any unresolved predicate -> NOT_ACCEPTED/FAIL_CLOSED
any failed predicate -> FAIL
only all 20 resolved PASS -> ALL_PASS
```

Do not manufacture placeholder PASS values.

# SV07 zero runtime PASS claims

The structural validation itself must prove:

```text
runtime predicate PASS claims = 0
```

Definition presence is not runtime acceptance.

# P03/P04 binding

Restore/retain actual bindings:

```text
P03 -> SV08/SV09/SV10/SV11 pre-execution exact-byte evidence
P04 -> SV12/SV13/SV14 post-execution exact-byte evidence
```

P03/P04 cannot be satisfied by generic text.

# P19 ordering

The single corrected runner must preserve:

```text
P01-P18
→ P20
→ durable evidence persistence
→ disposable sandbox cleanup
→ verify sandbox absent
→ evaluate P19
→ finalize P19 outside sandbox
→ aggregate P01-P20
```

# P20 contract

P20 must observe/account for production-derived error precedence.

Do not duplicate production archive/lifecycle policy in the runner.

# Single-artifact rule

After correction:

```text
one corrected runner
one new SHA-256
one complete structural contract
one fresh SV01-SV36 validation
```

Forbidden:

```text
SV09 evidence from 19E551...
P01-P20 evidence from an older runner
cross-hash acceptance composition
ledger-only repair without runner correction
```

# Fresh hash

Because the runner changes:

```text
NewRunnerSHA256 != 19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
```

Once the new hash is computed, freeze those bytes.

Any later source edit invalidates the candidate and requires another new hash/full validation.

# Complete SV01–SV36 restart

Restart validation from SV01.

Do not resume at SV03.

Produce exactly:

```text
SV01 ... SV36
```

with fields:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Required checks remain:

```text
SV01 exact Windows PowerShell 5.1.26100.9444
SV02 parser errors = 0
SV03 exactly canonical P01-P20 definitions
SV04 predicate fields complete
SV05 all P01-P20 participate in aggregation
SV06 unresolved predicates fail closed
SV07 runtime predicate PASS claims = 0
SV08 executable source-wrapper SHA-256
SV09 executable byte-preserving source → W copy
SV10 executable W-pre SHA-256
SV11 pre-RunId source/W equality
SV12 source/W post hashes
SV13 post equality assertions
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
SV31 independent roots
SV32 disposable cleanup
SV33 runner survives cleanup
SV34 ledger survives cleanup
SV35 runner hash reverified
SV36 repository/Git invariants
```

# PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Parser errors for final frozen runner:

```text
0
```

# Durable evidence lifecycle

Create a fresh durable candidate root independent from the disposable root.

Retain:

```text
corrected runner exact bytes
new SHA-256
parser evidence
P01-P20 structural definition evidence
SV01-SV36 observation ledger
SV09 source references
aggregation/fail-closed evidence
fresh git diff --check evidence
sanitized validation summary
```

Preserve all historical roots.

After pre-cleanup evidence is durable:

1. delete disposable root;
2. verify it is absent;
3. verify durable root exists;
4. verify runner exists;
5. verify ledger exists;
6. reopen/parse ledger;
7. re-hash runner;
8. verify retained hash == new hash;
9. finalize post-cleanup evidence outside disposable root.

# Git diff capture

Use the proven operation-scoped Windows PowerShell 5.1 native capture.

Freshly retain:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
```

Known CRLF advisory text is acceptable only with:

```text
exit code = 0
classification = ADVISORY_ONLY
```

# Repository/mutation boundary

Expected repository baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Do not edit, stage, revert, clean, commit, or push repository content.

Required mutation accounting:

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

Local disposable/durable evidence creation is authorized.

# W5 boundary

This authority is structural only.

Require:

```text
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
runtime P01-P20 execution = NO
runtime predicate PASS claims = 0
```

# PASS threshold

PASS only if:

```text
new hash differs from 19E551...
parser errors = 0
SV03 canonical P01-P20 count = 20
SV04 fields complete
SV05 aggregation membership exact
SV06 unresolved fail-closed
SV07 runtime PASS claims = 0
SV08-SV14 exact-byte controls PASS
SV09 executable file copy preserved
SV01-SV36 exactly 36 unique records
all evidence independently inspectable
all evidence references resolve
all validation methods complete
SV failures = 0
two-root lifecycle PASS
post-cleanup hash reverified
fresh git diff --check PASS
repository mutation boundary PASS
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

# Required success markers

Only on complete PASS emit:

```text
RELEASE 1.12 WP04 — R1 SV03 CANONICAL P01-P20 RESTORATION: PASS
RELEASE 1.12 WP04 — R1 OLD RUNNER HASH: 19E551532EF218120DF4BCC7DE43FA3E670FC709BD29030E807E66B177514D3C
RELEASE 1.12 WP04 — R1 NEW RUNNER HASH: <NEW_SHA256>
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 AGGREGATION MEMBERSHIP: PASS
RELEASE 1.12 WP04 — R1 UNRESOLVED PREDICATES FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING COPY: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 NEW-HASH SV01-SV36 DURABLE VALIDATION: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
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
RELEASE 1.12 WP04 — TERRA R1 SV03 RESTORATION COMPLETE
```

# Required handoff

Return:

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

- retain sanitized durable failure evidence;
- identify exact first failed SV/gate;
- classify the defect;
- state whether another runner hash is required;
- do not manufacture later PASS claims;
- do not invoke W5;
- do not allocate a W5 RunId;
- STOP.

# Final stop

**STOP AFTER THE SV03 RUNNER CORRECTION AND COMPLETE NEW-HASH SV01–SV36 REVALIDATION.**

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

After a complete Terra PASS, run the existing **GPT-5.6 Luna R1 New-Hash SV01–SV36 Reconciliation Authority** against the new retained artifact.

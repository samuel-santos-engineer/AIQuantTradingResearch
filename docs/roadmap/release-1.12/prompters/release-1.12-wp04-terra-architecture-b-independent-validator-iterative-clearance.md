# Release 1.12 WP04 — Terra Architecture-B Independent Validator Implementation & Iterative R1 Clearance

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns the local/disposable implementation of the independent R1 structural validator, validation execution, durable evidence generation, and iterative correction of authorized validator/harness/evidence defects.

GPT-5.6 Luna owns the frozen contract, architecture, governance, reconciliation, acceptance, and later authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Luna or Terra.

# Binding Luna decision

Luna selected and froze:

```text
RELEASE 1.12 WP04 — LUNA R1 EVIDENCE-WRITER ARCHITECTURE RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 EVIDENCE-WRITER ARCHITECTURE: B
```

Architecture B is:

```text
FROZEN_RUNNER_PLUS_INDEPENDENT_VALIDATOR
```

This authority implements exactly that architecture.

# Frozen architecture

## Runner

The runner is the sole governed execution artifact.

Identity:

```text
RunnerSHA256
```

Runner responsibilities:

```text
canonical P01-P20 definitions/schema
fail-closed aggregation behavior
G01-G13 framework
S/W validation
governed source-wrapper identity
byte-preserving governed source → exact $env:W copy
pre/post exact-byte hash operations/assertions
future governed W5 invocation boundary
future P01-P20 runtime observation collection
future W5 durable scenario evidence required by P17/P18
```

The runner does **not** own structural certification of itself.

Every runner-byte change requires:

```text
fresh RunnerSHA256
complete SV01-SV36 restart
```

Cross-runner-hash acceptance carry-forward is forbidden.

## Independent validator

The validator owns:

```text
explicit SV01-SV36 mapping
inspection of the frozen runner
control-flow/order inspection
negative/fail-closed structural tests
PowerShell/parser verification
hash-binding verification
P01-P20 definition/aggregation verification
substantive Observed values
EvidenceReference generation
ValidationMethod generation
durable structural ledger writing
post-cleanup structural finalization
Git/repository invariant capture
validator self-hash retention
binding all evidence to exact RunnerSHA256
```

Validator identity:

```text
EvidenceWriterOrValidatorSHA256
```

The validator hash identifies the verifier. It does not replace `RunnerSHA256` as the governed execution identity.

# Current known runner state

The latest runner work materially includes:

```text
canonical P01-P20 definitions
G01-G13 framework
executable byte-preserving source → W copy
copy positioned before W existence/pre-hash validation
Windows PowerShell parser errors = 0
```

Terra must discover the exact latest runner path and compute its SHA-256 before validation.

Do not assume an old historical runner hash.

# Mission

Implement one independent local/disposable `validator.ps1` for Architecture B and use it to validate the exact frozen runner.

Terra may iterate autonomously on:

```text
validator defects
validator PowerShell 5.1 compatibility defects
structural-evidence defects
ledger defects
evidence-reference defects
validation-method defects
local harness defects
disposable/durable lifecycle defects
hash-binding defects
negative-test defects
Git evidence-capture defects
```

until either:

```text
A. one frozen RunnerSHA256 + one frozen ValidatorSHA256 produce a complete durable SV01-SV36 ALL_PASS package; or
B. a mandatory escalation boundary is reached.
```

Do not stop after merely creating `validator.ps1`.

# Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Do not upgrade PowerShell.

Validator and runner parser errors must be:

```text
0
```

# Canonical P01-P20 contract

The validator must prove that the frozen runner defines exactly:

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

Every predicate definition/schema must structurally support:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

# Fail-closed aggregation

Prove the frozen runner's aggregation contract:

```text
missing predicate -> fail
duplicate predicate -> fail
unresolved predicate -> not accepted
failed predicate -> fail
only exactly P01-P20 all resolved PASS -> ALL_PASS
```

During this structural authority:

```text
runtime P01-P20 PASS claims = 0
```

The validator must not execute governed W5 merely to prove structural behavior.

# G01-G13 contract

Validate the frozen runner's logical sequence:

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

Under this authority:

```text
G12 execution = FORBIDDEN
G13 execution = FORBIDDEN
```

# Exact-byte contract

Validate executable byte-preserving copy behavior equivalent to:

```powershell
[IO.File]::Copy($SourceWrapper, $env:W, $true)
```

or an equivalent helper whose destination is exact `$env:W`.

Reject text-based copying or reconstruction.

Require structural future-run hash assertions:

```text
SourcePreSHA256 == WPreSHA256
SourcePreSHA256 == SourcePostSHA256
WPreSHA256 == WPostSHA256
SourcePostSHA256 == WPostSHA256
```

Mismatch must fail closed.

# Canonical SV01-SV36

The validator must explicitly map and evaluate exactly:

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

Require exactly 36 unique IDs, zero missing, duplicates, or extras.

# SV record schema

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

Evidence quality is binding:

```text
Observed = substantive independently inspectable observation
EvidenceReference = resolvable retained evidence/source reference
ValidationMethod = concrete method used
```

Generic `PASS`, `true`, or repetition of Expected is insufficient where source, ordering, hash, lifecycle, or negative-test evidence is required.

# Validator implementation rules

The validator may:

```text
read the frozen runner
parse/inspect runner source
invoke safe local structural tests
create disposable fixtures
create negative-test fixtures
hash files
parse PowerShell
capture Git read-only state
write local durable structural evidence
clean disposable local artifacts
```

The validator must not:

```text
alter the frozen runner during a validation iteration
execute governed W5
allocate a governed W5 RunId
manufacture runtime P01-P20 PASS values
reimplement production archive/extraction/persistence/lifecycle semantics
alter tracked production source
stage/commit/push
mutate GitHub
mutate Azure
mutate Docker/GHCR
configure Twelve Data secrets
```

# Validator hash discipline

Every validator-byte change requires:

```text
fresh ValidatorSHA256
complete SV01-SV36 restart
```

If runner bytes change:

```text
fresh RunnerSHA256
fresh validation candidate
complete SV01-SV36 restart
```

No PASS record may be silently carried from a prior runner or validator hash.

Retain failed iteration evidence.

# Iterative correction loop

For every iteration:

1. identify exact RunnerSHA256;
2. identify exact ValidatorSHA256;
3. validate WinPS/parser prerequisites;
4. execute SV01-SV36 in order;
5. stop evaluation at the first real failed structural gate if continuing would create misleading PASS claims;
6. retain sanitized failure evidence;
7. classify the failure;
8. if authorized, make the narrowest validator/harness/evidence correction;
9. generate fresh ValidatorSHA256;
10. restart from SV01;
11. continue until ALL_PASS or mandatory escalation.

Terra does not need another authority between authorized validator iterations.

# Correctable classifications

Terra may autonomously correct:

```text
VALIDATOR_DEFECT
HARNESS_DEFECT
STRUCTURAL_EVIDENCE_DEFECT
LEDGER_COMPLETENESS_DEFECT
LEDGER_ORDERING_DEFECT
EVIDENCE_REFERENCE_DEFECT
VALIDATION_METHOD_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
HASH_BINDING_DEFECT
NEGATIVE_TEST_DEFECT
DISPOSABLE_EVIDENCE_LIFECYCLE_DEFECT
GIT_CAPTURE_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

provided the correction does not alter the frozen acceptance contract or production source.

# Runner-defect handling

If validation exposes a genuine `RUNNER_DEFECT`:

- retain exact evidence;
- determine whether the required correction is strictly within the already-frozen runner contract;
- if it is a local/disposable runner implementation defect and requires no contract interpretation or production mutation, Terra may narrowly correct the runner;
- compute a fresh RunnerSHA256;
- freeze the new runner bytes;
- restart the entire validator sequence from SV01 with the current/fresh validator;
- record the prior runner hash as failed historical evidence.

If correcting the runner would change P01-P20, SV01-SV36, architecture B, governance, production behavior, or tracked production source, STOP for escalation.

# Mandatory escalation

STOP for Luna if:

```text
CONTRACT_AMBIGUITY
architecture-B ambiguity
P01-P20 change required
SV01-SV36 change required
acceptance weakening required
cross-hash evidence composition required
governance reinterpretation required
P19/P20 meaning change required
```

STOP for separate production remediation if:

```text
PRODUCTION_DEFECT
tracked production-source change required
Azure mutation required
Docker/GHCR mutation required
deployment mutation required
GitHub mutation required
```

# Candidate binding

Every durable ledger must bind at least:

```text
RecordType = R1StructuralValidation
RunnerSHA256 = <exact frozen runner>
EvidenceWriterOrValidatorSHA256 = <exact frozen validator>
PowerShellVersion = 5.1.26100.9444
SVContract = SV01-SV36
PredicateContract = P01-P20
GovernedW5Executed = false
GovernedW5RunIdAllocated = false
```

# Two-root lifecycle

Use independent:

```text
DisposableRoot
DurableRoot
```

The final successful candidate must retain under DurableRoot at least:

```text
runner.ps1 exact frozen bytes
validator.ps1 exact frozen bytes
RunnerSHA256 evidence
ValidatorSHA256 evidence
parser evidence
P01-P20 structural evidence
aggregation/fail-closed evidence
SV01-SV36 ledger
negative-test evidence
G01-G13/order evidence
exact-byte copy/hash evidence
Git/repository evidence
sanitized summary
iteration history
```

Before cleanup, persist all evidence needed afterward.

Then:

1. remove DisposableRoot;
2. prove DisposableRoot absent;
3. prove DurableRoot exists;
4. prove retained runner exists;
5. prove retained validator exists;
6. prove retained ledger exists;
7. recompute RunnerSHA256;
8. recompute ValidatorSHA256;
9. prove both equal frozen identities;
10. finalize post-cleanup records outside DisposableRoot;
11. atomically finalize ledger;
12. reopen/parse ledger;
13. prove exactly 36 records and zero failures.

Do not recreate DisposableRoot afterward.

# P19 ownership

Architecture B freezes P19 structural finalization as validator-owned.

The validator must structurally prove ordering equivalent to:

```text
pre-cleanup structural evidence persisted
→ disposable cleanup
→ disposable absence proven
→ durable survival proven
→ P19 structural observation finalized
→ final ledger atomically finalized
```

This is structural validation only. Do not claim runtime W5 P19 PASS.

# P20 ownership

The validator owns structural verification that P20 is observation-only and that the runner does not manufacture production lifecycle/error-precedence semantics.

The validator may inspect source/control flow and safe local fixtures.

Do not execute governed W5.

Do not duplicate production policy to manufacture P20.

# Fresh Git evidence

Use operation-scoped Windows PowerShell 5.1 native capture for:

```text
git diff --check
```

Capture `$LASTEXITCODE` immediately.

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

Expected repository baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

# Mutation boundary

Authorized:

```text
local/disposable runner correction when strictly allowed
local/disposable validator implementation/correction
local structural validation
durable local evidence
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
W5 executions = 0
W5 RunIds = 0
external mutations = 0
```

# Secret hygiene

No secret values may be retained or printed.

Twelve Data secret configuration remains forbidden in WP04.

# Structural clearance threshold

Success requires one exact pair:

```text
RunnerSHA256
ValidatorSHA256
```

with:

```text
PowerShell = 5.1.26100.9444
runner parser errors = 0
validator parser errors = 0
canonical P01-P20 count = 20
predicate schema PASS
aggregation/fail-closed PASS
runtime predicate PASS claims = 0
SV01-SV36 exactly 36
SV failures = 0
SV evidence substantive
EvidenceReferences resolvable
ValidationMethods substantive
SV08-SV14 PASS
G01-G13 structural order PASS
P03/P04 bindings PASS
P19 structural finalization PASS
P20 observation-only contract PASS
no production-policy duplication PASS
secret hygiene PASS
two-root lifecycle PASS
DisposableRoot absent
DurableRoot survives
runner survives
validator survives
ledger survives
runner hash reverified
validator hash reverified
fresh git diff --check PASS
repository invariants PASS
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

# Required successful terminal markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B INDEPENDENT VALIDATOR: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: <SHA256>
RELEASE 1.12 WP04 — R1 VALIDATOR SHA256: <SHA256>
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER ERRORS: 0
RELEASE 1.12 WP04 — R1 VALIDATOR PARSER ERRORS: 0
RELEASE 1.12 WP04 — R1 CANONICAL P01-P20 DEFINITIONS: 20
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE COHERENCE: PASS
RELEASE 1.12 WP04 — R1 G01-G13 STRUCTURAL ORDER: PASS
RELEASE 1.12 WP04 — R1 P03/P04 BINDINGS: PASS
RELEASE 1.12 WP04 — R1 P19 VALIDATOR FINALIZATION: PASS
RELEASE 1.12 WP04 — R1 P20 OBSERVATION-ONLY CONTRACT: PASS
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 VALIDATOR HASH REVERIFICATION: PASS
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
IterationHistoryPath
RunnerSourcePath
RunnerSHA256
ValidatorPath
EvidenceWriterOrValidatorSHA256
DurableRoot
SVLedgerPath
PowerShellVersion
RunnerParserErrorCount
ValidatorParserErrorCount
CanonicalPredicateCount
PredicateFieldFailures
AggregationMembershipCount
RuntimePredicatePassClaims
SVRecordCount
SVFailedCount
SV08SV14Coherence
G01G13Ordering
P03P04BindingEvidence
P19FinalizationEvidence
P20ObservationOnlyEvidence
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
ExactMutationAccounting
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
```

# Failure/escalation terminal report

On mandatory escalation emit:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B INDEPENDENT VALIDATOR: STOPPED
FIRST BLOCKING GATE: <SV/GATE>
CLASSIFICATION: <CLASS>
RUNNER SHA256: <HASH>
VALIDATOR SHA256: <HASH>
PRODUCTION DEFECT: <YES/NO>
CONTRACT/ARCHITECTURE CHANGE REQUIRED: <YES/NO>
TRACKED PRODUCTION CHANGE REQUIRED: <YES/NO>
FRESH RUNNER HASH REQUIRED: <YES/NO>
FRESH VALIDATOR HASH REQUIRED: <YES/NO>
GOVERNED W5 WRAPPER INVOKED: NO
GOVERNED W5 RUNID ALLOCATED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
```

Retain sanitized evidence for the exact first blocking gate.

# Luna handoff

After complete Terra PASS, STOP.

The next authority is a **GPT-5.6 Luna Architecture-B final R1 structural reconciliation** over the exact frozen RunnerSHA256, ValidatorSHA256, and complete durable SV01-SV36 ledger.

Terra PASS alone does not authorize W5.

Until Luna subsequently grants structural acceptance:

```text
R1 structural contract = NOT_ACCEPTED
Governed W5 execution = NOT_AUTHORIZED
W5 governed acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
Publication blocker = UNRESOLVED
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
```

# Final instruction

**KEEP ITERATING THROUGH AUTHORIZED RUNNER/VALIDATOR/HARNESS/EVIDENCE DEFECTS UNTIL ONE ARCHITECTURE-B RUNNER+VALIDATOR PAIR PRODUCES A COMPLETE DURABLE SV01-SV36 ALL_PASS PACKAGE OR A MANDATORY ESCALATION BOUNDARY IS REACHED.**

Do not execute governed W5 under this authority.

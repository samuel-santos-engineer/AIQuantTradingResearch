# Release 1.12 WP04 — Luna R1 SV Evidence-Writer Architecture Reconciliation

## Authority identity

**Selected execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns contract, architecture, governance, reconciliation, and acceptance criteria. GPT-5.6 Terra owns later implementation/validation under a separate authority. GPT-5.6 Sol is supporting analysis only and must not replace Luna or Terra.

## Authority type

READ-ONLY architecture/contract reconciliation. No implementation, Git/GitHub/Azure/Docker/GHCR mutations, W5 execution, or W5 RunId allocation.

## Trigger

Terra reached a mandatory escalation boundary. The current self-contained runner has canonical P01–P20 definitions, gate/execution functions, byte-preserving governed source → `$env:W` copy, and zero Windows PowerShell parser errors, but has no durable SV evidence-writer implementation, explicit SV01–SV36 mapping, or complete durable SV ledger finalizer.

Classification: `CONTRACT_AMBIGUITY / runner-architecture gap`. Production defect: `NO`. W5 wrapper invoked: `NO`. W5 RunId allocated: `NO`.

The ambiguity is whether “self-contained runner” requires the execution harness itself to generate structural certification, or whether an independent validator may inspect/finalize the frozen runner while preserving the runner as the governed execution identity.

## Current governance

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

## Binding invariants

Preserve all:

```text
Windows PowerShell = 5.1.26100.9444
canonical P01-P20 unchanged
canonical SV01-SV36 unchanged
fail-closed acceptance unchanged
every runner-byte change => fresh RunnerSHA256
no structural PASS carry-forward across runner hashes
runtime P01-P20 PASS claims during structural validation = 0
W5 execution forbidden
W5 RunId allocation forbidden
tracked production-source mutation forbidden in R1 structural work
production policy must not be duplicated
secret hygiene mandatory
durable evidence survives disposable cleanup
historical failed ledgers remain immutable
```

## Architecture decision

Select exactly one architecture. Do not blend them.

### A — MONOLITHIC_SELF_CONTAINED_RUNNER

`runner.ps1` owns P01–P20, execution/gates, source→W copy, SV01–SV36 mapping, structural validation, durable ledger writing, and post-cleanup finalization.

Assess the risk that this mixes execution with self-certification and expands runtime-harness responsibilities.

### B — FROZEN_RUNNER_PLUS_INDEPENDENT_VALIDATOR

`runner.ps1` owns the governed execution contract. `validator.ps1` independently inspects the frozen runner, executes structural checks, maps SV01–SV36, and writes/finalizes the durable structural ledger.

Primary governed execution identity remains `RunnerSHA256`. The validator has its own retained SHA-256 and every ledger is bound to both hashes.

Explicitly rule whether:

```text
"self-contained runner"
=
self-contained execution harness for governed W5

and does not require the runner to generate its own structural certification.
```

If B is selected, freeze:

Runner owns:
```text
canonical P01-P20 definitions/schema
fail-closed aggregation behavior
G01-G13 framework
S/W validation
governed source identity
byte-preserving source → W copy
pre/post exact-byte hash operations/assertions
future governed W5 invocation boundary
future P01-P20 runtime observation collection
future W5 durable scenario evidence required by P17/P18
```

Validator owns:
```text
explicit SV01-SV36 mapping
frozen-runner source/control-flow inspection
negative/fail-closed structural tests
PowerShell/parser verification
hash-binding verification
P01-P20 definition/aggregation verification
Observed/EvidenceReference/ValidationMethod records
durable structural ledger writer/finalizer
post-cleanup structural finalization
Git/repository invariant capture
validator self-hash retention
binding all evidence to exact RunnerSHA256
```

Validator must not execute W5, allocate a W5 RunId, manufacture runtime P01-P20 PASS, duplicate production archive/extraction/persistence/lifecycle policy, alter frozen runner bytes, alter tracked production source, or mutate Azure/GitHub/Docker/GHCR.

### C — VERSIONED_RUNNER_VALIDATOR_BUNDLE

Candidate consists of `runner.ps1` + `validator.ps1`, identified by RunnerSHA256, ValidatorSHA256, and BundleManifestSHA256.

If C is selected, explicitly reconcile this with the existing “one final frozen runner hash” requirement and state whether the acceptance identity is being changed.

## Required decision criteria

Evaluate A/B/C against:

```text
D01 P01-P20 unchanged
D02 SV01-SV36 unchanged
D03 frozen RunnerSHA256 remains execution identity
D04 fresh hash on runner-byte change
D05 no cross-hash PASS carry-forward
D06 explicit SV mapping owner
D07 explicit durable-ledger owner
D08 substantive Observed/EvidenceReference/ValidationMethod
D09 independent verification where practical
D10 no production-policy duplication
D11 P20 observation-only preserved
D12 P19 post-cleanup semantics preserved
D13 two-root lifecycle preserved
D14 WinPS 5.1 preserved
D15 zero tracked-repository mutation
D16 W5 execution prohibited
D17 W5 RunId prohibited
D18 minimize runner responsibility expansion
D19 Terra can iterate without renewed architecture ambiguity
D20 clear terminal Luna reconciliation package
```

## Structural ledger contract

Whichever architecture is selected, freeze each SV record as:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require exactly SV01–SV36: 36 unique records, zero missing, duplicates, or extras.

Top-level/finalization metadata must include:

```text
RecordType
RunnerSHA256
EvidenceWriterOrValidatorSHA256
PowerShellVersion
SVRecordCount
SVFailedCount
FirstFailedSV
RuntimePredicatePassClaims
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
DisposableRoot
DurableRoot
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
GitDiffCheckExitCode
GitDiffCheckClassification
ExactMutationAccounting
```

`Observed` must be substantive and independently inspectable. `EvidenceReference` must resolve. `ValidationMethod` must state the concrete method. Generic `PASS`, `true`, or repetition of Expected is insufficient for source/order/hash/lifecycle/negative-test evidence.

## Finalization semantics

The selected owner must support:

```text
write provisional SV evidence
persist required artifacts to DurableRoot
clean DisposableRoot
prove DisposableRoot absent
prove DurableRoot/runner/ledger survive
recompute RunnerSHA256
finalize SV32-SV35 outside DisposableRoot
atomically finalize durable ledger
reopen/parse final ledger
verify exactly 36 SV records and failure count
verify retained runner hash
```

Fail closed on any finalization failure.

## Candidate binding

If a separate validator/evidence writer is selected, require a durable header equivalent to:

```text
RecordType = R1StructuralValidation
RunnerSHA256 = <frozen runner>
EvidenceWriterOrValidatorSHA256 = <frozen verifier>
PowerShellVersion = 5.1.26100.9444
SVContract = SV01-SV36
PredicateContract = P01-P20
GovernedW5Executed = false
GovernedW5RunIdAllocated = false
```

The verifier hash identifies the verifier but does not replace RunnerSHA256 as execution identity unless Luna explicitly selects C and reconciles that contract change.

## Required Luna output

Return:

```text
SelectedArchitecture
RejectedArchitectures
SelfContainedRunnerDefinition
RunnerResponsibilities
EvidenceWriterResponsibilities
CandidateIdentityRule
RunnerHashRule
EvidenceWriterHashRule
SVMappingOwner
DurableLedgerOwner
PostCleanupFinalizerOwner
P19Ownership
P20Ownership
CrossHashCarryForwardRule
RuntimePredicatePassClaimRule
W5ExecutionRule
W5RunIdRule
TerraResumeAuthorized
RequiredNextTerraScope
MutationAccounting
```

## PASS decision

If one coherent architecture can be frozen without weakening the fixed acceptance contract, emit:

```text
RELEASE 1.12 WP04 — LUNA R1 EVIDENCE-WRITER ARCHITECTURE RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 EVIDENCE-WRITER ARCHITECTURE: <A|B|C>
RELEASE 1.12 WP04 — R1 SELF-CONTAINED RUNNER DEFINITION: FROZEN
RELEASE 1.12 WP04 — R1 SV01-SV36 MAPPING OWNER: FROZEN
RELEASE 1.12 WP04 — R1 DURABLE LEDGER OWNER: FROZEN
RELEASE 1.12 WP04 — R1 CANDIDATE IDENTITY RULE: FROZEN
RELEASE 1.12 WP04 — R1 RUNNER HASH RULE: FROZEN
RELEASE 1.12 WP04 — R1 EVIDENCE-WRITER HASH RULE: FROZEN
RELEASE 1.12 WP04 — R1 CROSS-HASH PASS CARRY-FORWARD: FORBIDDEN
RELEASE 1.12 WP04 — R1 RUNTIME PREDICATE PASS CLAIMS DURING STRUCTURAL VALIDATION: 0
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — TERRA R1 RESUME: AUTHORIZED_FOR_SEPARATE_AUTHORITY
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

## NOT_READY / FAIL

If evidence is insufficient:

```text
RELEASE 1.12 WP04 — LUNA R1 EVIDENCE-WRITER ARCHITECTURE RECONCILIATION: NOT_READY
```

Identify the exact missing contract input.

If requirements are internally contradictory and cannot be reconciled without changing the fixed acceptance contract:

```text
RELEASE 1.12 WP04 — LUNA R1 EVIDENCE-WRITER ARCHITECTURE RECONCILIATION: FAIL
```

Identify the exact contradiction.

Do not authorize Terra resume on NOT_READY or FAIL.

## Mutation accounting

This Luna authority performs:

```text
tracked repository mutations = 0
staging = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
W5 executions = 0
W5 RunIds = 0
```

## Stop

After PASS, STOP. Do not implement the selected architecture.

The next artifact must be a separate **GPT-5.6 Terra implementation and complete SV01-SV36 validation authority** implementing exactly Luna's selected architecture.

Until later Terra execution and subsequent Luna structural reconciliation:

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

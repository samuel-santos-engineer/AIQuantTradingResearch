# Release 1.12 WP04 — Single Converge Authority: C2 → W5

**Selected execution model: GPT-5.6 Terra**

## Objective

Use one Codex execution to remediate all remaining disposable C2 harness/validator defects, exhaustively reconcile every structural gate, and—only after R01-R14 are ALL_PASS—continue directly into governed W5. Do not stop for ordinary in-scope defects.

Principle:

```text
ONE AUTHORITY PER GOVERNANCE BOUNDARY
NOT ONE AUTHORITY PER DEFECT
```

## Starting state

Latest rejected/superseded tuple:

```text
Root=C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\f815c45fcbde427195b020a1c50279fd
Runner=54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
Harness=F9B0E4EF36C66C74C78BDDDBC08523E87C2C0B5EF28C80CEE5684FBA3E176778
Validator=77D24D68EC55939667B80C8A6CFFB0E8503F5DD18BCA976FD0FFE403A1752525
Child=CBE8527B2372AFCF33D978665C7D240C4BE5548982AAEDE152E4028077258B12
```

Latest Luna matrix:

```text
R01 PASS
R02 PASS
R03 PASS
R04 PASS
R05 PASS
R06 PASS
R07 NOT_PROVEN
R08 PASS
R09 PASS
R10 PASS
R11 PASS
R12 PASS
R13 FAIL
R14 NOT_PROVEN
```

Defect owner: `HarnessPlusValidator`. Frozen-runner or production change is NOT required.

R07 defect: T01-T18 events are distinct but contracts remain generic. They do not independently enumerate/prove G01-G11, every RunId-allocation site, every wrapper-invocation site, and dominance.

R13 defect: all 11 corruption fixtures were baseline-accepted/executed/rejected, but retained fixture records lack durable per-fixture fields:
`FixtureId`, `CorruptionApplied`, `PostCorruptionValidationResult`, `ExpectedRejectionPredicate`, `ObservedRejectionPredicate`, `Result`, `EvidenceReference`.

R14 must be recomputed after R07/R13 are valid.

## Frozen boundaries

Do not change the frozen runner, tracked/production source, production wrapper/helper, architecture/policy, Git/GitHub, Azure, Docker/GHCR, Twelve Data, secrets contract, W6/W7/W8, publication, or lifecycle.

Frozen runner hash remains exactly:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Structural byte-changing remediation is authorized only in disposable/local-only harness, validator, and child probe if necessary.

No tracked/staged mutation and no real external call is authorized.

## Mandatory convergence loop

For each attempt:

1. Evaluate ALL R01-R14; never stop diagnostic evaluation at the first ordinary failure.
2. Retain the complete matrix and all defects.
3. Repair every defect wholly inside harness/validator/child scope.
4. Every byte change invalidates prior candidate evidence: re-hash, create a fresh durable root, and restart the full structural cycle.
5. Repeat internally until R01-R14 ALL_PASS.

Stop only for a genuine governance-boundary blocker.

### R07 required repair

Replace generic T contracts with explicit contracts proving at least:

```text
T01 canonical top-level entrypoint
T02 shared orchestration reachability
T03 identity/hash gate ordering
T04 exact-byte gate ordering
T05 explicit G01-G11 enumeration
T06 executable/assertion evidence for every G01-G11 gate
T07 interception installation ordering
T08 SandboxRoot binding
T09 production reachable-surface derivation
T10 harness interception-surface derivation
T11 executed-approved-surface derivation
T12 three-way equality dominates RunId allocation
T13 positive/negative probes dominate RunId allocation
T14 every RunId-allocation site explicitly enumerated
T15 every wrapper-invocation site explicitly enumerated
T16 mandatory gates dominate every RunId/wrapper site
T17 no alternate/bypass path reaches RunId/wrapper
T18 structural stop immediately before RunId allocation; RunId=0, wrapper=0, realExternal=0
```

Each T record must contain a unique event and contract-specific `Observed`, not a generic assertion label.

Retain a G01-G11 table:

```text
GateId
GateContract
SourceLocation
DominatedRunIdSites
DominatedWrapperSites
Observed
Result
EvidenceReference
```

Independently enumerate every RunId-allocation and wrapper-invocation site from executable AST/control flow. For each site retain source identity and mandatory dominators.

Require:

```text
UncoveredRunIdSites=0
UncoveredWrapperSites=0
AlternateBypassPaths=0
```

Generic `dominance=True` without inventories is invalid.

### R13 required repair

Retain exactly 11 durable corruption-fixture records:

```text
01 Missing event
02 Duplicate EventId
03 ProbeId/EventId mismatch
04 AssertionKey mismatch
05 EvidenceReference to wrong event
06 F05 generic aggregate substitution
07 F10 generic aggregate substitution
08 F15 generic aggregate substitution
09 T-family generic aggregate substitution
10 I-family generic aggregate substitution
11 Literal PASS with failing underlying event
```

Every record must include:

```text
FixtureId
FixtureContract
BaselineArtifactIdentity
BaselineValidationResult
CorruptionApplied
CorruptedArtifactIdentity
ValidatorInvocationIdentity
PostCorruptionValidationResult
ExpectedRejectionPredicate
ObservedRejectionPredicate
Result
EvidenceReference
```

Require:

```text
FixtureCount=11
BaselineAccepted=11
CorruptedExecuted=11
Rejected=11
UnexpectedAccepted=0
MissingRequiredFields=0
UnresolvedEvidenceReferences=0
```

An aggregate `11/11` alone is invalid.

### R14 required derivation

Recompute only from the fresh evidence chain:

```text
raw events
→ 53 event-specific named records
→ H/T/F/I aggregates
→ positive/negative manifests
→ G01-G11 + site/dominance evidence
→ 11 durable corruption records
→ three-way surface equality
→ three C2 summaries
→ SV01-SV36
→ BUILD_LEDGER → SERIALIZE → PUBLISH → REOPEN
```

After REOPEN independently resolve all 53 named mappings, G01-G11, all RunId/wrapper sites, all 11 fixtures, and all 36 SV references.

## Complete structural acceptance

Every fresh attempt must re-prove, not carry forward:

```text
R01 exact tuple/root/frozen runner
R02 WinPS 5.1.26100.9444; parser 0/0/0/0
R03 53 records/files/ProbeIds/mappings
R04 53 semantic requirements/AssertionKeys/EventIds; generic aggregates=0; failures/unresolved=0
R05 F05/F10/F15/HNEG06 event-specific
R06 H01-H08 ALL_PASS
R07 explicit T01-T18 + G01-G11 + complete RunId/wrapper-site dominance
R08 independently derived complete production external surface
R09 ProductionSurface == HarnessSurface == ExecutedApprovedSurface
R10 F01-F18 ALL_PASS
R11 Git negatives=5; Azure=10; Helper=10; failures=0; escapes=0
R12 I01-I09 ALL_PASS
R13 11/11 durable executable corruption fixtures
R14 SV01-SV36=36/0; evidence=36/36; checkpoints exact; runtime claims=0
```

Also require:

```text
W5 RunIds=0
W5 wrapper invocations=0
real external calls=0
authority-introduced tracked changes=0
staged paths=0
git diff --check=PASS
```

Only the two pre-existing tracked WP04 modifications may remain.

## Acceptance-equivalent adversarial pass

Before W5, independently validate the finalized fresh root without trusting producer PASS markers.

Evaluate ALL R01-R14 and print the complete matrix even if failures exist.

If a non-PASS is harness/validator/child-local, automatically return to the convergence loop.

Only `R01-R14 = ALL_PASS` authorizes W5 inside this authority.

Emit then:

```text
RELEASE 1.12 WP04 — C2 SINGLE-CONVERGE STRUCTURAL ACCEPTANCE: PASS
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_WITHIN_THIS_AUTHORITY
```

## W5 — continue in the same Codex execution

Do not ask for another prompt.

Freeze/reverify the structurally accepted tuple immediately before W5.

Canonical W5:

```text
real production wrapper execution under governed local interception
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=SUCCESS
RestoreOnly=exactly 1
RealExternalCalls=0
durable sanitized evidence retained
complete W5 ledger retained
```

### Fresh RunId rule

Do not allocate a W5 RunId until every structural and W5 pre-RunId gate passes. Allocate exactly one fresh never-used RunId. Every allocated W5 RunId is permanently single-use.

### P01-P20

Require exactly:

```text
P01 WinPS 5.1.26100.9444
P02 parser 0 errors
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 EvidenceCheckpoint PASS
P09 Final SUCCESS
P10 RestoreOnly 1
P11 real external calls 0
P12 external ledger only governed intercepted calls
P13 repo modified scope unchanged
P14 staged 0
P15 git diff --check PASS
P16 secret hygiene PASS
P17 durable sanitized evidence retention
P18 complete durable W5 scenario ledger retention
P19 disposable sandbox cleanup PASS
P20 error precedence PASS
```

W5 acceptance only `P01-P20=ALL_PASS`. P19 is evaluated after cleanup. P20 is observation-only.

### W5 convergence

If a W5 attempt exposes a harness/validator-only defect:

- retain the failed attempt;
- permanently consume its RunId;
- repair only disposable harness/validator/child scope;
- structural acceptance is invalidated by any byte change;
- create fresh hashes/root and rerun the entire C2 convergence + adversarial R01-R14 validation;
- only after ALL_PASS allocate another fresh RunId and retry W5.

Continue internally until W5 ALL_PASS or a genuine governance-boundary blocker occurs.

## Absolute stop conditions

Stop instead of broadening authority if correction/action requires:

```text
frozen runner mutation
tracked/production source mutation
new Luna architecture/policy decision
real external invocation
Azure call/mutation
GitHub mutation
Docker/GHCR call/mutation
Twelve Data call/configuration
secret-contract change
W6/W7/W8
publication/lifecycle mutation
```

## Final PASS markers

Only after structural and W5 acceptance:

```text
RELEASE 1.12 WP04 — SINGLE CONVERGE C2→W5 AUTHORITY: PASS
RELEASE 1.12 WP04 — C2 R01-R14: ALL_PASS
RELEASE 1.12 WP04 — C2 T01-T18 SEMANTIC CONTRACTS: ACCEPTED
RELEASE 1.12 WP04 — C2 G01-G11 DOMINANCE EVIDENCE: ACCEPTED
RELEASE 1.12 WP04 — C2 RUNID-SITE INVENTORY/DOMINANCE: ACCEPTED
RELEASE 1.12 WP04 — C2 WRAPPER-SITE INVENTORY/DOMINANCE: ACCEPTED
RELEASE 1.12 WP04 — C2 CORRUPTION FIXTURES 11/11: ACCEPTED
RELEASE 1.12 WP04 — C2 SV01-SV36: 36/0
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: PASS
RELEASE 1.12 WP04 — GOVERNED W5 RUNID: FRESH_SINGLE_USE
RELEASE 1.12 WP04 — W5 P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. Do not execute W6/W7/W8 or lifecycle/publication.

## Required consolidated handoff

Return one report containing:

```text
StructuralRetryAttemptCount
StructuralSupersededRoots
FinalStructuralDurableRoot
RunnerSHA256
HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
WindowsPowerShellVersion
ParserErrorCounts
R01-R14FinalMatrix
NamedProbeCount
UniqueEventIdCount
GenericAggregateNamedEvidenceCount
G01-G11Results
RunIdAllocationSiteCount
RunIdAllocationSiteInventory
UncoveredRunIdSiteCount
WrapperInvocationSiteCount
WrapperInvocationSiteInventory
UncoveredWrapperSiteCount
AlternateBypassPathCount
CorruptionFixtureCount
CorruptionFixtureBaselineAcceptedCount
CorruptionFixtureRejectedCount
CorruptionFixtureMissingFieldCount
CorruptionFixtureResults
NegativeGitCount
NegativeAzureCount
NegativeHelperCount
NegativeFailureCount
RealExternalEscapeCount
SVRecordCount
SVFailedCount
ResolvedSVEvidenceCount
StructuralCheckpointCounts
StructuralAdversarialReconciliationResult
W5AttemptCount
ConsumedFailedW5RunIds
AcceptedW5RunId
W5DurableEvidenceRoot
P01-P20Results
W5AllPass
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
RealExternalCallCount
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckResult
SecretHygieneResult
ExactMutationAccounting
BoundaryBlocker
NextAuthorizedAction
```

If PASS:

```text
NextAuthorizedAction=GPT-5.6 Luna final W5 acceptance reconciliation / W6 sequencing authority
```

If blocked, report **all known blockers**, not merely the first.

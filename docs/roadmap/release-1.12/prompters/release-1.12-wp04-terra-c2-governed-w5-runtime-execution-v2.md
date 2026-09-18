# Release 1.12 WP04 — Terra C2 Governed W5 Runtime Execution V2

**Selected execution model: GPT-5.6 Terra**

## Authority

Execute exactly **one** fresh governed W5 attempt through the independently accepted, remediated C2 top-level runtime entry point.

This authority is runtime-only. It does not authorize source correction, harness/validator/runner modification, W6/W7/W8, publication, staging, commit, push, GitHub lifecycle mutation, Azure mutation, Docker/GHCR mutation, or Twelve Data configuration.

## Exact accepted tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
636FF0C654CA0A5901D4A8A5D27CA848DD2C04C1B9A155235FAA1F762C10EBAD

ValidatorSHA256 =
CC5BED1E30889E8ACCF7A912A870ECBFA9F5EDC9D0DB68074294F0FBD88EACFB

StructuralDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\3039ad1db25f4716a52ac8ce5a638841
```

Binding Luna markers:

```text
RELEASE 1.12 WP04 — LUNA C2 REMEDIATED RUNTIME FINAL STRUCTURAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 REMEDIATED THREE-ARTIFACT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: ACCEPTED
RELEASE 1.12 WP04 — C2 REMEDIATED HARNESS IDENTITY: ACCEPTED
RELEASE 1.12 WP04 — C2 REMEDIATED VALIDATOR IDENTITY: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
```

## Critical execution rule

Invoke W5 **only through the harness's accepted top-level governed runtime entry point**.

Forbidden:

```text
dot-sourcing the harness to call internal functions
calling Invoke-G12Runtime directly
constructing a parallel runtime orchestration
editing the harness
editing the validator
editing the frozen runner
bypassing the top-level dispatch
```

The accepted top-level path itself must own the runtime orchestration.

## Recover exact retained artifacts

The workspace harness may be absent by design.

Recover/copy the retained durable harness into a fresh disposable W5 execution root without editing it.

Before RunId allocation, recompute and require exact:

```text
RunnerSHA256
HarnessSHA256
ValidatorSHA256
```

Any mismatch:

```text
FAIL CLOSED
RunId allocation = NO
wrapper invocation = NO
```

## PowerShell preflight

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

## Pre-RunId gates

Execute the accepted harness top-level runtime path.

Require all accepted G01-G11 and exact-byte gates to complete before RunId allocation, including:

```text
frozen runner identity
harness identity
production-wrapper source identity
exact-byte disposable wrapper copy
copy-pre identity
source/copy equality
repository invariants
interception prerequisites
required S/W prerequisites
```

If any pre-RunId gate fails, STOP with no RunId.

## Fresh RunId

Only after all pre-RunId gates pass, allow the accepted harness to allocate exactly one fresh:

```text
initialize-w5-<fresh-guid>
```

Never reuse any historical or failed W5/diagnostic RunId.

Once allocated, the RunId is permanently single-use regardless of outcome.

## Canonical W5 runtime scenario

The accepted top-level harness must invoke the exact copied production wrapper once.

The production-derived target scenario is:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycle = SUCCESS
RestoreOnly count = 1
real external calls = 0
```

The harness must not manufacture these results.

## Governed interception boundary

Only the structurally accepted minimum interceptions are allowed:

```text
git
az
required existing helper invocation(s)
```

All intercepted call shapes must be within the accepted allowlist.

Any unexpected call fails closed.

No real Azure/GitHub/Docker/GHCR/Twelve Data/external-service call is authorized.

## Canonical P01-P20

```text
P01 WinPS version 5.1.26100.9444
P02 parser 0 errors
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 EvidenceCheckpoint PASS
P09 FinalLifecycle SUCCESS
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

Acceptance requires exactly:

```text
P01-P20 = ALL_PASS
```

No partial acceptance.

## P19 ordering

P19 remains independent-validator-owned.

Required sequence:

```text
runtime execution complete
→ durable raw evidence checkpoint
→ disposable cleanup
→ validator observes cleanup complete
→ P19 evaluation
→ final W5 ledger publication
```

P19 evaluated before cleanup is FAIL.

## P20 semantics

P20 remains independent-validator-owned and observation-only.

Do not manufacture an error solely to obtain P20 PASS.

## Repository invariants

The only expected pre-existing modified tracked paths are:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

## Evidence retention

Use fresh W5 disposable and durable roots independent from the structural root.

Retain sanitized evidence sufficient to prove:

```text
accepted tuple identities
top-level runtime entry used
G01-G11 results/order
RunId allocation/order
wrapper invocation/order/cardinality
source/copy pre/post hashes
archive/extraction observations
checkpoint result
final lifecycle
RestoreOnly count
external-call ledger
repo invariants
secret hygiene
raw pre-cleanup evidence
cleanup
P19 finalization
P20 observation
P01-P20 ledger
```

## Failure rule

On any failure:

- do not edit runner/harness/validator/production source;
- do not autonomously retry;
- if a RunId was allocated, permanently retire it;
- retain sanitized failure evidence;
- identify the first FAIL or NOT_PROVEN predicate and defect owner;
- STOP for reconciliation.

## PASS markers

Only if P01-P20 are all PASS:

```text
RELEASE 1.12 WP04 — C2 W5 GOVERNED EXECUTION V2: PASS
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. W6 requires separate authority.

## FAIL markers

If any predicate fails or is NOT_PROVEN:

```text
RELEASE 1.12 WP04 — C2 W5 GOVERNED EXECUTION V2: FAIL
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: NOT_ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Required handoff

Return:

```text
RunnerSHA256Before
RunnerSHA256After
HarnessSHA256Before
HarnessSHA256After
ValidatorSHA256Before
ValidatorSHA256After
WindowsPowerShellVersion
ParserErrorCounts
TopLevelRuntimeEntrypointUsed
PreRunIdGateResult
G01-G11Result
W5RunId
RunIdAllocationCount
RunIdOrderingResult
ProductionWrapperInvocationCount
WrapperInvocationOrderingResult
SourceWrapperSHA256Before
SandboxWrapperSHA256Before
SourceWrapperSHA256After
SandboxWrapperSHA256After
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
RealExternalCallCount
ExternalCallLedgerResult
RepoModifiedScopeResult
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
SecretHygieneResult
DurableEvidenceRetentionResult
W5DurableRoot
CompleteW5LedgerPath
DisposableRootAfterCleanup
P19EvaluationTiming
P20Observation
P01
P02
P03
P04
P05
P06
P07
P08
P09
P10
P11
P12
P13
P14
P15
P16
P17
P18
P19
P20
P01-P20Aggregate
GovernedW5Acceptance
ExactMutationAccounting
FirstFailingOrNotProvenPredicate
DefectClassification
NextAuthorizedAction
```

# Release 1.12 WP04 — Terra C2 Governed W5 Fresh Runtime — Retry Within Authorized Scope

**Selected execution model: GPT-5.6 Terra**

## Authority

Execute the freshly authorized governed W5 scenario through the independently accepted final C2 top-level runtime contract.

This authority is bound to the exact accepted tuple below.

If Codex finds a defect that is correctable entirely inside disposable/local W5 execution orchestration, evidence collection, or validator mechanics already governed by this contract, it must correct it and keep trying. It must not stop after every in-scope correctable issue.

However, **a W5 RunId is single-use**. Any attempt that allocates a RunId consumes it permanently. A retry after such a failure must allocate a different fresh RunId, and only after all pre-RunId gates pass again.

No retry may cross the explicit stop boundaries below.

## Binding Luna acceptance

```text
RELEASE 1.12 WP04 — LUNA C2 SCOPE-FAITHFUL INTERCEPTION FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FINAL THREE-ARTIFACT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 COPIED-WRAPPER INTERCEPTION SCOPE: ACCEPTED
RELEASE 1.12 WP04 — C2 EXACT AZ ARGUMENT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
```

## Exact accepted tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
D14E0D634203FB8121564513669DD4B1743F91E0D3F71445644BF1B5CD9ECB27

ValidatorSHA256 =
451FC907DA208462DBCEAE56D504FDBB83D6D143CF68C5EDD0F46AFDE313F867

AcceptedStructuralDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\8ab9bec858594a2daab1fe20f2b4502c
```

Any tuple mismatch fails closed before RunId allocation.

## Frozen artifacts

The accepted runner/harness/validator bytes are frozen for W5.

Do not edit them during W5.

If execution reveals a defect requiring a byte change to any accepted artifact:

```text
STOP
W5 acceptance = NOT_GRANTED
fresh structural validation + Luna reconciliation required
```

The retry-within-scope rule does not authorize changing the accepted tuple.

## Windows PowerShell baseline

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

## Pre-RunId gates — every attempt

Before allocating any RunId, freshly require:

```text
exact runner identity
exact harness identity
exact validator identity
production-wrapper source identity
exact-byte disposable wrapper copy
G01-G11
scope-faithful interception installation
Push-Location SandboxRoot binding
copied-wrapper-scope interception probes
exact normalized az argument contract
unexpected-call probes
real git escape blocked
real az escape blocked
real helper escape blocked
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
repository invariants
staged paths = 0
git diff --check = PASS
```

If a pre-RunId gate fails:

- allocate no RunId;
- invoke no governed wrapper;
- if the problem is only disposable execution/evidence setup and does not alter the accepted tuple, correct it and rerun preflight;
- otherwise STOP at the relevant governance boundary.

## RunId allocation

Only after all pre-RunId gates pass may the accepted top-level harness allocate exactly one fresh:

```text
initialize-w5-<fresh-guid>
```

Never reuse any historical, diagnostic, failed, or previously allocated RunId.

Maintain a durable attempt ledger containing every allocated W5 RunId and final disposition.

## Governed W5 scenario

Execute through the accepted **top-level governed runtime entrypoint** only.

The accepted runtime path must own:

```text
RunId allocation
SandboxRoot working-directory binding
executable git/az/helper interception
copied production-wrapper invocation
raw observation capture
durable pre-cleanup evidence checkpoint
cleanup
independent validator/finalizer handoff
```

Do not dot-source the harness to call internal runtime functions.

Do not invoke an internal runtime function directly.

Do not construct parallel orchestration.

## Expected production-derived W5 outcome

The wrapper itself must produce/drive:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycle = SUCCESS
RestoreOnly count = 1
```

The harness/validator must not manufacture these outcomes.

## External-call boundary

Require:

```text
real external calls = 0
```

Only the accepted exact governed interception call shapes may execute.

Unexpected calls fail closed.

No real:

```text
git external process escape
az external process escape
workspace helper escape
Azure operation
GitHub operation
Docker/GHCR operation
Twelve Data operation
network/service mutation
```

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

No partial acceptance and no cross-attempt composition.

## P19 ordering

P19 is validator-owned.

Require:

```text
runtime execution complete
→ durable raw evidence checkpoint
→ disposable cleanup
→ validator observes cleanup
→ P19 evaluation
→ final W5 ledger publication
```

P19 evaluated before cleanup is FAIL.

## P20 semantics

P20 is validator-owned and observation-only.

Do not inject an artificial production error solely to obtain P20 PASS.

## Retry-within-authorized-scope protocol

If an attempt fails, classify the first failure.

### A. Failure before RunId allocation

If correctable without changing the accepted runner/harness/validator/production bytes and without crossing a forbidden mutation boundary:

```text
correct disposable execution/evidence setup
→ rerun complete pre-RunId gates
→ continue
```

No RunId was consumed.

### B. Failure after RunId allocation

The allocated RunId is permanently retired.

Retain complete sanitized evidence for that attempt.

A retry is allowed only when:

```text
the defect is not in frozen runner/harness/validator/production bytes
the correction is entirely local/disposable execution or evidence mechanics
no production/tracked mutation is required
no real external operation is required
the accepted contract remains unchanged
```

Then:

```text
correct local disposable mechanics
→ fresh disposable root
→ complete pre-RunId gates from zero
→ allocate a NEW fresh RunId
→ execute a new W5 attempt
```

Never reuse or overwrite evidence for the failed RunId.

### C. Mandatory stop conditions

STOP immediately if the first unresolved defect requires any of:

```text
runner byte change
harness byte change
validator byte change
production-wrapper byte change
tracked-source modification
change to either pre-existing tracked WP04 script
new architecture/policy decision
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
real external call
relaxation of accepted interception contract
relaxation of P01-P20
W6/W7/W8
publication/lifecycle closure
```

Also stop if repeated failures indicate the accepted contract itself is defective rather than disposable execution mechanics.

## Attempt isolation

Every allocated RunId must have its own:

```text
fresh disposable root
fresh durable W5 evidence root
raw evidence checkpoint
sanitized ledger
cleanup record
P01-P20 result
failure/success disposition
```

No PASS credit may be combined across attempts.

The final accepted W5 attempt must independently satisfy all P01-P20.

## Repository invariants

Only these two pre-existing tracked modifications may remain:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require for every attempt and final handoff:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

## Secret hygiene

No secret value may appear in:

```text
console output
durable evidence
interception ledger
failure ledger
P01-P20 ledger
retry history
```

WP04 must not configure the Twelve Data secret.

## Mutation accounting

This authority permits:

```text
local disposable W5 filesystem artifacts
local durable sanitized W5 evidence
fresh single-use W5 RunId identifiers
```

It permits zero:

```text
tracked-source mutations
staging
commits
pushes
GitHub mutations
Azure mutations
Docker/GHCR mutations
external-service mutations
production mutations
```

Count each allocated W5 RunId in the attempt ledger even though it is not a repository/cloud mutation.

## PASS markers

Only when one fresh W5 attempt independently produces P01-P20 ALL_PASS:

```text
RELEASE 1.12 WP04 — TERRA C2 GOVERNED W5 FRESH RUNTIME: PASS
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. W6 requires separate authority.

## Boundary-stop markers

If no in-scope retry can reach ALL_PASS:

```text
RELEASE 1.12 WP04 — TERRA C2 GOVERNED W5 FRESH RUNTIME: STOPPED
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: NOT_ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Return the exact boundary-crossing defect requiring new authority.

## Required final handoff

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
AttemptCount
AllocatedW5RunIds
RetiredFailedW5RunIds
SuccessfulW5RunId
AttemptLedgerPath
PreRunIdGateResult
G01-G11Result
CopiedWrapperScopeResult
ExactAzContractResult
ExecutableInterceptionBoundaryResult
ProductionWrapperInvocationCountForSuccessfulAttempt
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
SuccessfulW5DurableRoot
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
RetryCorrectionsApplied
ExactMutationAccounting
FirstBoundaryCrossingDefectIfAny
NextAuthorityRequired
```

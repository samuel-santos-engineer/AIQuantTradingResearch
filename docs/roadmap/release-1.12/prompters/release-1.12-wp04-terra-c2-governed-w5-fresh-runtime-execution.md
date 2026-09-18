# Release 1.12 WP04 — Terra C2 Governed W5 Fresh Runtime Execution

**Selected execution model: GPT-5.6 Terra**

## Authority

Execute exactly one fresh governed W5 runtime attempt using the accepted C2 three-artifact architecture.

This authority is bound to:

```text
frozen structural runner
+ frozen local-only W5 runtime harness
+ independent validator/finalizer
```

It authorizes one fresh W5 RunId allocation and one production-wrapper invocation only after all pre-RunId gates pass.

It does not authorize W6/W7/W8, publication, staging, commit, push, GitHub lifecycle changes, Azure mutation, Docker/GHCR mutation, production-source modification, or Twelve Data secret configuration.

## Accepted prerequisite identities

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
7056320ED8D2B2F1C1EABB3E6CEE838791FBB1DFA1F67E09620887130E6F8926

ValidatorSHA256 =
3947E35A1062E577D59256A29D40B2573C433363E8538B39FC721E47628B2F93

C2StructuralDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\b811cb62d9534c72b8e6d478d55493df
```

Accepted controlling markers:

```text
RELEASE 1.12 WP04 — LUNA C2 W5 HARNESS FINAL STRUCTURAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 THREE-ARTIFACT STRUCTURAL CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 W5 HARNESS IDENTITY: ACCEPTED
RELEASE 1.12 WP04 — C2 RUNNER/HARNESS CONTRACT BINDING: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
```

## Restore the exact frozen harness

The disposable workspace harness is intentionally absent.

Recover the exact retained durable `harness.ps1` from the accepted C2 DurableRoot into a fresh disposable execution root.

Before any RunId allocation:

```text
compute recovered harness SHA256
require exact HarnessSHA256
compute frozen runner SHA256
require exact RunnerSHA256
compute validator SHA256
require exact ValidatorSHA256
```

A hash mismatch fails closed and prohibits RunId allocation and wrapper invocation.

Do not edit the recovered harness.

## PowerShell gate

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

No PowerShell 7 assumptions.

## Fresh W5 RunId

Only after all accepted pre-RunId gates pass, allocate exactly one fresh:

```text
initialize-w5-<fresh-guid>
```

Never reuse any historical W5 or diagnostic RunId, including:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
```

The allocated RunId is single-use even if execution later fails.

## Canonical W5 scenario

Execute the real production wrapper through the frozen C2 harness.

Required production-derived scenario:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycle = SUCCESS
RestoreOnly count = 1
real external calls = 0
```

Do not manufacture these values in the harness.

The harness may only provide the accepted minimum governed interceptions and raw observations.

## Canonical P01-P20

Acceptance requires:

```text
P01 Windows PowerShell version = 5.1.26100.9444
P02 parser errors = 0
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval = RETRIEVAL_FAILED
P07 FreshExtraction = NOT_APPLICABLE
P08 EvidenceCheckpoint = PASS
P09 FinalLifecycle = SUCCESS
P10 RestoreOnly count = 1
P11 real external calls = 0
P12 external ledger contains only governed intercepted calls
P13 repo modified scope unchanged
P14 staged paths = 0
P15 git diff --check = PASS
P16 secret hygiene = PASS
P17 durable sanitized evidence retention = PASS
P18 complete durable W5 scenario ledger retention = PASS
P19 disposable sandbox cleanup = PASS
P20 error precedence = PASS
```

Only:

```text
P01-P20 = ALL_PASS
```

grants W5 acceptance.

## G01-G11 and ordering

Use the accepted harness implementation exactly.

Require durable proof:

```text
G01-G11 complete before RunId allocation
source/copy identity assertion complete before RunId allocation
G01-G11 complete before wrapper invocation
fresh RunId allocated before wrapper invocation
```

Do not bypass a failed gate.

## Exact-byte protections

Require:

```text
production-wrapper source SHA captured
disposable wrapper copy byte-identical before execution
copy-pre SHA = source SHA
pre-execution equality asserted before RunId
source/copy post hashes captured
source/copy post identities equal pre identities
frozen runner hash unchanged
frozen harness hash unchanged
validator hash unchanged
```

## External-call boundary

Require:

```text
real external calls = 0
```

Only accepted governed interceptions may appear in the external-call ledger.

Any unexpected call fails closed.

No Azure/GitHub/Docker/GHCR/Twelve Data/external-service mutation is authorized.

## P19 — independent post-cleanup finalization

P19 remains validator-owned.

Required ordering:

```text
wrapper/harness execution completes
→ durable raw evidence checkpoint
→ disposable cleanup completes
→ validator observes disposable root absent
→ P19 evaluated
→ final W5 ledger publication
```

Do not evaluate P19 before cleanup.

## P20 — observation only

P20 remains validator-owned and observation-only.

Do not inject an artificial failure solely to create P20 PASS.

Use the accepted raw error-precedence observations.

## Repository invariants

Expected pre-existing tracked modifications are exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
pre-existing tracked modified paths remain exactly those two
staged paths = 0
git diff --check exit code = 0
```

Do not stage, commit, or push.

## Secret hygiene

No secret may appear in retained or console evidence.

Twelve Data secret configuration remains forbidden in WP04.

## Durable W5 evidence

Create fresh W5 disposable and durable roots independent from the C2 structural DurableRoot.

Retain enough sanitized evidence to independently prove every P01-P20 predicate, including:

```text
RunId
runner/harness/validator identities
source/copy pre/post identities
G01-G11 ordering
RunId ordering
wrapper invocation
archive/extraction observations
checkpoint observation
final lifecycle
RestoreOnly count
external-call ledger
repo-state evidence
git diff --check output
secret-hygiene result
pre-cleanup raw evidence
post-cleanup P19 evidence
P20 raw observation
complete final P01-P20 ledger
```

## Failure semantics

If execution fails after RunId allocation:

```text
retain the RunId permanently as used
retain sanitized failure evidence
do not retry with the same RunId
do not silently launch another governed W5 attempt
```

Return for reconciliation/remediation.

Do not modify the frozen harness, runner, validator, or production source under this authority.

## PASS terminal markers

Only if P01-P20 all pass:

```text
RELEASE 1.12 WP04 — C2 W5 GOVERNED EXECUTION: PASS
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. Do not begin W6.

## FAIL terminal markers

If any P predicate fails or is NOT_PROVEN:

```text
RELEASE 1.12 WP04 — C2 W5 GOVERNED EXECUTION: FAIL
RELEASE 1.12 WP04 — C2 W5 GOVERNED P01-P20: NOT_ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Report the first failing/not-proven predicate and defect ownership.

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
RunnerParserErrorCount
HarnessParserErrorCount
ValidatorParserErrorCount
W5RunId
W5DisposableRoot
W5DurableRoot
G01-G11Result
RunIdOrderingResult
WrapperInvocationOrderingResult
SourceWrapperSHA256Before
SandboxWrapperSHA256Before
SourceWrapperSHA256After
SandboxWrapperSHA256After
WrapperExecutionCompleted
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
GitDiffCheckOutput
SecretHygieneResult
DurableEvidenceRetentionResult
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

## Next gate

A successful `P01-P20 = ALL_PASS` grants W5 acceptance only.

Return for the separately governed W6 authority. Do not infer or execute W6/W7/W8.

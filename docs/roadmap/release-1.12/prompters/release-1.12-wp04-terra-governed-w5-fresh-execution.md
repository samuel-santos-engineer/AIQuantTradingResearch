# Release 1.12 WP04 — Terra Governed W5 Fresh Execution

**Selected execution model: GPT-5.6 Terra**

## Authority

Execute the now-authorized **fresh governed W5** for Release 1.12 WP04.

This authority follows the accepted Luna Architecture-B Final R1 Structural Reconciliation. It authorizes exactly one fresh W5 execution attempt under the frozen runner/validator structural contract, including allocation of one fresh W5 RunId.

It does **not** authorize W6/W7/W8, publication, staging, commit, push, GitHub lifecycle mutation, Azure mutation, production mutation, or tracked source correction.

## Accepted prerequisite gate

Treat these markers as the controlling prerequisite:

```text
RELEASE 1.12 WP04 — LUNA ARCHITECTURE-B FINAL R1 STRUCTURAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 STRUCTURAL CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — POWERSHELL WRAPPER SERIALIZER DEFECT REMEDIATION: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
```

Bound identities:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
E4E3773E480B5CE9A8E15FBB3939BFBAD43B29D7DC3A8D5FE795B9F9C8DD4F0F

StructuralDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\2999a1f7b02240aabbad42f013f18dbb
```

Before W5, reverify the frozen runner hash exactly.

## PowerShell baseline

```text
Windows PowerShell 5.1.26100.9444
Language/runtime target: Windows PowerShell 5.1
```

Require parser errors = 0 for the production wrapper and the frozen runner before execution.

Do not use PowerShell 7 assumptions.

## W5 canonical scenario

Execute the production wrapper through the frozen Architecture-B runner so the production-derived scenario is:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycle = SUCCESS
RestoreOnly count = 1
real external calls = 0
```

This must exercise the real production wrapper logic with governed interception/shims only where Architecture B permits.

Do not simulate the final P01-P20 results.

## Fresh RunId

Allocate exactly one fresh W5 RunId only after all frozen runner pre-RunId gates pass.

Required shape:

```text
initialize-w5-<fresh-guid>
```

Never reuse:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
```

or any historical/diagnostic RunId.

Retain the actual allocated RunId in the durable W5 ledger.

## Canonical P01-P20

Acceptance requires all twenty predicates to PASS:

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

Acceptance is only:

```text
P01-P20 = ALL_PASS
```

No partial acceptance.

## P19 ordering — mandatory

P19 must be evaluated by the independent validator/finalization flow **after** disposable cleanup has actually completed.

Do not evaluate P19 before cleanup and then infer later success.

The retained final W5 ledger must contain the post-cleanup P19 result.

## P20 semantics — mandatory

P20 is observation-only.

Do not inject a new error solely to manufacture P20 PASS.

Use the frozen Architecture-B error-precedence observation contract.

## Exact-byte and identity requirements

Require:

```text
source wrapper SHA captured
sandbox wrapper copy is byte-for-byte identical before execution
copy-pre SHA equals source SHA
source/copy identity assertion occurs before RunId allocation
source and copy post hashes captured
post-execution hashes equal their pre-execution identities
frozen runner hash remains exact
```

Any mismatch fails closed.

## G-gate ordering

Preserve the frozen Architecture-B ordering:

```text
G01-G11 complete before RunId allocation
G01-G11 complete before wrapper invocation
fresh RunId allocation occurs only after those gates
wrapper invocation occurs only after fresh RunId allocation
```

Do not weaken or reorder these gates.

## External-call boundary

Require:

```text
real external calls = 0
```

The external-call ledger may contain only calls intercepted by the governed W5 harness.

Unexpected external calls fail closed.

No Azure, GitHub, Docker/GHCR, Twelve Data, or other external-service mutation is authorized.

## Repository state

Two pre-existing governed tracked modifications are expected and must remain unchanged:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
pre-existing tracked modified paths = exactly those two
staged paths = 0
git diff --check exit code = 0
```

Do not stage or commit them.

## Secret hygiene

No secrets may appear in:

```text
stdout
stderr
durable W5 ledger
external-call ledger
checkpoint artifacts
diagnostics
temporary retained evidence
```

Twelve Data secret configuration remains forbidden in WP04.

## Durable evidence

Use fresh independent W5 disposable and durable roots.

Retain sanitized durable evidence sufficient to independently establish every P01-P20 result, including:

```text
actual RunId
runner identity
source/copy hashes
pre/post exact-byte evidence
wrapper terminal result
archive/extraction classification
checkpoint result
final lifecycle
RestoreOnly count
external-call ledger
repo modified scope
staged-path count
git diff --check output and exit code
secret-hygiene evidence
P19 post-cleanup evidence
P20 observation
complete final P01-P20 ledger
```

The disposable sandbox must be absent before final P19 PASS is published.

## Execution outcome

### Complete PASS

Only if P01-P20 are all independently supported and PASS, emit:

```text
RELEASE 1.12 WP04 — W5 GOVERNED EXECUTION: PASS
RELEASE 1.12 WP04 — W5 GOVERNED P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. Do not start W6.

### Failure

If any P predicate fails or is not proven, emit:

```text
RELEASE 1.12 WP04 — W5 GOVERNED EXECUTION: FAIL
RELEASE 1.12 WP04 — W5 GOVERNED P01-P20: NOT_ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Retain the failed RunId permanently as single-use and never retry with it.

Classify the first failing/not-proven predicate and the defect owner.

Do not autonomously change production source or the frozen runner under this authority.

## Required handoff

Return:

```text
RunnerSHA256Before
RunnerSHA256After
ValidatorSHA256
WindowsPowerShellVersion
ParserErrorCount
W5RunId
W5DisposableRoot
W5DurableRoot
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
P01-P20 results individually
P01-P20 aggregate
GovernedW5Acceptance
ExactMutationAccounting
FirstFailingOrNotProvenPredicate
DefectClassification
NextAuthorizedAction
```

## Next gate after PASS

A W5 `ALL_PASS` grants W5 governed acceptance only.

Do not infer W6/W7/W8 acceptance.

Return for the next separately governed WP04 authority.

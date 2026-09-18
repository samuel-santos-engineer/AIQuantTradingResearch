# Release 1.12 WP04 — Luna C2 Scope-Faithful Interception Final Structural Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform the fresh independent read-only reconciliation of the final C2 scope-faithful interception tuple.

Do not modify any artifact or repository file. Do not allocate a W5 RunId. Do not invoke the governed production wrapper. Do not perform real external calls or GitHub/Azure/Docker/GHCR mutations.

## Exact bound tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
D14E0D634203FB8121564513669DD4B1743F91E0D3F71445644BF1B5CD9ECB27

ValidatorSHA256 =
451FC907DA208462DBCEAE56D504FDBB83D6D143CF68C5EDD0F46AFDE313F867

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\8ab9bec858594a2daab1fe20f2b4502c
```

No acceptance evidence may be composed from superseded harness/validator hashes.

## R01 — Identities and evidence root

Independently recompute and verify the exact three hashes.

Verify the durable structural ledger, evidence manifest, resolution report, probe evidence, and retained copies all belong to this exact tuple.

## R02 — Windows PowerShell baseline

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

## R03 — Copied-wrapper command-resolution scope

Inspect the final harness and evidence.

Prove the future copied-wrapper execution scope is bound to:

```text
Push-Location $SandboxRoot
try/finally location restoration
```

Prove the copy-equivalent child probe actually executes from `SandboxRoot`.

Prove the production wrapper's relative helper form resolves to the sandbox shadow helper under that same scope.

Direct harness invocation of the shadow helper is not sufficient.

Require:

```text
C2 COPIED-WRAPPER INTERCEPTION SCOPE = PASS
workspace real helper escape = BLOCKED
```

## R04 — git/az executable interception

Prove calls originating from the copy-equivalent child/wrapper scope resolve to the governed PowerShell 5.1 function shims.

Require executable proof, not merely shim definitions.

Require:

```text
real git escape = BLOCKED
real az escape = BLOCKED
```

## R05 — Exact az argument contract

Inspect the actual final allowlist implementation.

Require:

```text
previous wildcard log-download matcher = ABSENT
broad --log-file * matcher = ABSENT
arbitrary-path acceptance = ABSENT
```

For every permitted az operation verify normalized exact argument semantics.

For a dynamic log-file path, require validation against the one exact governed destination under the current SandboxRoot after path normalization.

Reject traversal, alternate roots, workspace destinations, extra arguments, and arbitrary caller paths.

Require:

```text
C2 EXACT AZ ARGUMENT CONTRACT = PASS
```

## R06 — I01-I09 independent reconciliation

Verify fresh evidence under the bound final tuple:

```text
I01 approved git from child/copy-equivalent scope
I02 approved az from child/copy-equivalent scope
I03 approved relative helper from child/copy-equivalent scope
I04 unexpected git fails closed
I05 unexpected az fails closed
I06 unexpected helper fails closed
I07 real-command/helper escape blocked
I08 durable sanitized interception ledger complete
I09 disposable artifacts cleaned and working location restored
```

Require:

```text
I01-I09 = ALL_PASS
```

No direct-harness-only proof may substitute for copied-wrapper-scope proof.

## R07 — Executable interception boundary

Require independently:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

Evidence must bind:

```text
exact final harness hash
exact final validator hash
copied-wrapper working-directory scope
git interception
az interception
relative helper interception
exact az allowlist
I01-I09
escape prevention
cleanup/location restoration
```

## R08 — Pre-RunId ordering

Prove the final top-level governed runtime path structurally orders:

```text
identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ executable interception installation
→ Push-Location SandboxRoot
→ copied-wrapper-scope interception probes
→ exact az-contract validation
→ unexpected-call probes
→ real-command escape probe
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY PASS
→ only then W5 RunId allocation
→ only then copied-wrapper invocation
```

Require RunId allocation to be unreachable before the boundary passes.

## R09 — Top-level reachability and no bypass

Prove the corrected scope/interception logic is on the accepted top-level governed runtime path.

No alternate runtime path may:

```text
allocate RunId earlier
invoke copied wrapper without SandboxRoot binding
invoke copied wrapper without executable interceptions
bypass exact allowlist validation
```

## R10 — No policy duplication

Verify harness/validator do not manufacture production outcomes or lifecycle semantics.

They must not synthesize:

```text
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnly result
P01-P20 runtime outcomes
```

## R11 — Fresh SV01-SV36

Independently inspect the final fresh ledger.

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Every SV record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Pay special attention to SV16, SV27, SV28, and SV29.

## R12 — Runtime exclusion

Require:

```text
W5 RunId allocations = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

## R13 — Finalization / retained evidence

Verify exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Verify durable copies/evidence remain resolvable after cleanup.

Verify the workspace-only harness is absent after cleanup and the retained durable harness is byte-identical to the bound hash.

## R14 — Repository and mutation invariants

Only these two pre-existing modified tracked paths may exist:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
W5 mutations = 0
```

## Reconciliation classification

Classify every R01-R14 material gate:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

Do not repair evidence or artifacts during this authority.

## PASS markers

Only if every material gate independently passes:

```text
RELEASE 1.12 WP04 — LUNA C2 SCOPE-FAITHFUL INTERCEPTION FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FINAL THREE-ARTIFACT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 COPIED-WRAPPER INTERCEPTION SCOPE: ACCEPTED
RELEASE 1.12 WP04 — C2 EXACT AZ ARGUMENT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. W5 requires a separate GPT-5.6 Terra authority.

## Failure behavior

If a gate fails, identify:

```text
first FAIL/NOT_PROVEN gate
exact evidence
defect owner
whether harness-only / validator-only / harness+validator remediation is sufficient
whether production/tracked change is required
```

No W5 authorization on failure.

## Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
DurableRootObserved
WindowsPowerShellVersion
ParserErrorCounts
CopiedWrapperWorkingDirectoryBinding
WorkingDirectoryRestorationResult
ChildScopeProbeResult
RelativeHelperResolutionResult
WorkspaceHelperEscapeResult
GitInterceptionResult
AzInterceptionResult
HelperInterceptionResult
WildcardLogDownloadMatcherAbsent
BroadLogFileWildcardAbsent
ExactAzContractResult
DynamicLogFileValidationResult
I01
I02
I03
I04
I05
I06
I07
I08
I09
InterceptionProbeAggregate
C2ExecutableInterceptionBoundary
RealGitEscapeResult
RealAzEscapeResult
RealHelperEscapeResult
PreRunIdOrderingResult
TopLevelRuntimeBindingResult
NoBypassResult
NoPolicyDuplicationResult
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
CanonicalCheckpointCounts
WorkspaceHarnessAfterCleanup
DurableHarnessCopyResult
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
ExactMutationAccounting
R01-R14Summary
FinalReconciliationResult
FirstFailingOrNotProvenPredicate
DefectClassification
NextAuthorizedAction
```

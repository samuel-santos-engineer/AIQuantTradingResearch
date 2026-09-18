# Release 1.12 WP04 — Terra C2 Scope-Faithful Interception Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate the confirmed C2 disposable-harness and validator defects from the failed Luna executable-interception reconciliation.

Within the exact local-only harness/validator scope defined here, **do not stop at the first correctable defect**. Diagnose, correct, re-hash, and rerun the required structural cycle until the final candidate satisfies every acceptance gate.

This retry authority does **not** permit crossing governance boundaries. Stop only if the remaining defect requires a forbidden mutation, production/tracked-source change, frozen-runner change, external mutation/call, W5 RunId allocation, W5 wrapper runtime execution, or a new Luna contract decision.

## Binding Luna failure

```text
RELEASE 1.12 WP04 — LUNA C2 EXECUTABLE INTERCEPTION FINAL STRUCTURAL RECONCILIATION: FAIL
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
```

Material failures:

```text
R03 executable copied-wrapper interception = FAIL
R04 exact allowlist = FAIL
R05 I01-I09 = NOT_PROVEN
R06 real-command escape prevention = FAIL
R07 pre-RunId ordering = NOT_PROVEN
R08 top-level runtime binding = NOT_PROVEN
R11 interception summary predicate = NOT_ACCEPTED
```

Defect owner:

```text
C2 disposable harness
+ corresponding independent-validator structural-contract insufficiency
```

Production wrapper defect:

```text
NO
```

## Frozen runner

MUST remain byte-identical:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Superseded candidate identities:

```text
HarnessSHA256 =
BA951DDB1A823BCA32CABBC749693BB574CAFB0622E2F0C3AD827591FAE0C705

ValidatorSHA256 =
4D58D14C9C7490419CACE1C40BB746894A001D92439BDF82AA561A3A449E355F
```

These remain historical evidence only after remediation bytes change.

## PowerShell baseline

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only assumptions.

# Required correction A — copied-wrapper working directory

The prior harness created the shadow helper under:

```text
SandboxRoot\eng\...
```

but invoked:

```powershell
& $W
```

without changing the working directory.

The production wrapper uses relative helper invocation:

```powershell
".\$helper"
```

Therefore the future governed wrapper invocation must execute with:

```text
current working directory = SandboxRoot
```

Use a fail-safe scoped pattern compatible with Windows PowerShell 5.1, e.g. governed `Push-Location` / `Pop-Location` with `try/finally`, so the location is restored even on failure.

Do not change the production wrapper.

## Required structural proof for correction A

Before W5 RunId allocation, prove using a **child script invocation matching the copied wrapper's actual execution scope** that:

```text
child script current location = SandboxRoot
relative helper path resolves to SandboxRoot shadow helper
workspace real helper is not selected
location restoration occurs after probe
```

Directly invoking the sandbox helper from the harness is insufficient.

# Required correction B — exact normalized az contracts

Remove broad wildcard `az` matching.

In particular, arbitrary acceptance such as:

```text
--log-file *
webapp show*
```

is forbidden.

Derive each permitted `az` invocation from the production wrapper's actual reachable W5 call surfaces.

Normalize arguments into an exact contract.

Each allowlisted operation must define:

```text
az subcommand identity
required argument names
required literal argument values
permitted argument ordering normalization
permitted dynamic values
forbidden extra arguments
expected governed local result
durable ledger representation
```

## Dynamic log-file path rule

A dynamic log-file value may be accepted only when it validates to the exact governed sandbox destination for the current disposable execution.

The rule must prove:

```text
path is absolute after normalization
path is under the exact current SandboxRoot
path equals the specifically expected governed log-file destination
no .. traversal after normalization
no alternate root
no workspace path
no arbitrary caller-supplied path
```

Do not implement this as wildcard string matching.

# Required correction C — child-scope interception proof

Structural validation must execute a disposable child script that reproduces the copied wrapper's relevant command-resolution model.

The child-scope probe must prove:

```text
git resolves to governed script-scope interception
az resolves to governed script-scope interception
relative helper resolves to sandbox shadow helper
approved calls return governed local results
unexpected calls fail closed
real commands/helpers are not reached
```

The validator must reject harness-scope-only proof.

# Required correction D — validator contract

Update only the disposable/local validator so PASS requires copied-wrapper-scope evidence.

Strengthen at least:

```text
SV16
SV27
SV28
SV29
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
```

The validator must explicitly reject:

```text
direct sandbox-helper invocation as helper-scope proof
harness-scope shim visibility as copied-wrapper-scope proof
wildcard az allowlists
arbitrary dynamic log-file paths
```

# Required interception probes

Re-run and strengthen I01-I09:

```text
I01 approved git call from child/copy-equivalent scope
I02 approved az call from child/copy-equivalent scope
I03 approved relative helper call from child/copy-equivalent scope
I04 unexpected git call fails closed
I05 unexpected az call fails closed
I06 unexpected helper call fails closed
I07 real-command/helper escape blocked from child/copy-equivalent scope
I08 complete sanitized durable interception ledger
I09 disposable interception/location artifacts cleaned; location restored
```

Require:

```text
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

# Pre-RunId ordering

Prove the future top-level runtime path orders:

```text
identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ interception installation
→ SandboxRoot working-directory binding
→ child/copy-equivalent approved probes
→ unexpected-call probes
→ real-command escape probe
→ exact az-contract validation
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY PASS
→ only then future RunId allocation
→ only then future copied-wrapper invocation
```

This authority stops before the final two actions.

# Retry-until-PASS protocol

This section is binding.

If any harness/validator-only structural test, parser check, I01-I09 predicate, SV01-SV36 predicate, evidence-resolution check, cleanup check, hash check, or repository-invariant check fails:

1. classify the first failure;
2. determine whether it is correctable entirely inside the disposable/local harness or validator;
3. if YES, correct it immediately;
4. recompute every changed artifact hash;
5. mark all prior candidate hashes and PASS evidence from those hashes superseded;
6. create a **fresh structural durable root**;
7. rerun the complete required validation from the beginning;
8. repeat until the final single-hash candidate passes every gate.

Do not ask for a new authority merely because an in-scope harness/validator defect is discovered.

Do not silently carry forward PASS evidence across changed hashes.

## Mandatory stop conditions

STOP without further retry if correction would require any of:

```text
frozen runner byte change
tracked production-wrapper/source change
change to the two pre-existing tracked WP04 scripts
new architecture/policy decision outside this contract
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
real external call
W5 RunId allocation
governed W5 wrapper runtime execution
W6/W7/W8
publication/lifecycle closure
```

On such a stop, retain sanitized evidence and return the first boundary-crossing defect requiring new authority.

# Fresh complete structural cycle

The final candidate must run all canonical SV01-SV36 from SV01.

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

Each SV record retains exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

# Runtime exclusions

Throughout all retry iterations:

```text
W5 RunId allocations = 0
governed W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

# Finalization

For the final successful structural candidate require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

All final evidence references must resolve after cleanup.

# Repository invariants

Only these two pre-existing tracked modifications may remain:

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

# Durable evidence and retry history

For each superseded retry candidate retain a sanitized retry record containing:

```text
attempt number
candidate harness hash
candidate validator hash
first failure
correction made
reason retry remained within authority
superseded status
```

The final durable root must additionally retain:

```text
final runner/harness/validator copies and hashes
exact git allowlist
exact az allowlist
exact helper contract
dynamic log-file path validator evidence
child/copy-equivalent scope probe evidence
working-directory binding/restoration evidence
I01-I09 evidence
real-command escape evidence
C2_EXECUTABLE_INTERCEPTION_BOUNDARY evidence
SV01-SV36 ledger
36/36 evidence manifest/resolution report
checkpoint evidence
repo invariants
retry-history manifest
```

# PASS markers

Only after one final single-hash candidate passes all gates:

```text
RELEASE 1.12 WP04 — TERRA C2 SCOPE-FAITHFUL INTERCEPTION REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 COPIED-WRAPPER INTERCEPTION SCOPE: PASS
RELEASE 1.12 WP04 — C2 EXACT AZ ARGUMENT CONTRACT: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 REMEDIATED HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 REMEDIATED VALIDATOR IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 SCOPE-FAITHFUL INTERCEPTION RECONCILIATION: YES
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP for Luna reconciliation.

# Required handoff

Return:

```text
RunnerSHA256
HistoricalHarnessSHA256
HistoricalValidatorSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededHarnessSHA256s
SupersededValidatorSHA256s
RetryHistoryPath
CopiedWrapperWorkingDirectoryBinding
WorkingDirectoryRestorationResult
ChildScopeProbeMechanism
RelativeHelperResolutionResult
WorkspaceHelperEscapeResult
GitInterceptionResult
AzInterceptionResult
HelperInterceptionResult
ApprovedGitCallShapes
ApprovedAzCallShapes
ApprovedHelperCallShapes
DynamicLogFileExpectedPath
DynamicLogFileValidationResult
WildcardAzContractsRemaining
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
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
RealGitEscapeResult
RealAzEscapeResult
RealHelperEscapeResult
PreRunIdOrderingResult
TopLevelRuntimeBindingResult
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
CanonicalCheckpointCounts
DurableRoot
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
DisposableRootAfterCleanup
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
ExactMutationAccounting
FinalStructuralResult
FirstBoundaryCrossingDefectIfAny
NextAuthorityRequired
```

# Next gate

Even after Terra PASS, W5 remains unauthorized.

The next authority is a fresh **GPT-5.6 Luna C2 Scope-Faithful Interception Final Structural Reconciliation** bound to the final tuple and final durable root.

Only a Luna PASS may authorize another fresh governed W5 execution and RunId allocation.

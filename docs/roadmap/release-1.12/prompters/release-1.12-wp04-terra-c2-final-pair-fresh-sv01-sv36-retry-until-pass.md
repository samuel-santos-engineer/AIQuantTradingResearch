# Release 1.12 WP04 — Terra C2 Final-Pair Fresh SV01–SV36 — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue the existing scope-faithful C2 remediation authority from the current corrected harness/validator state.

The child/copy-equivalent interception probe has passed, but the validator was subsequently changed. Therefore **all structural PASS credit preceding the final validator byte change is superseded**.

Run a completely fresh structural cycle from SV01 under the exact current final harness/validator pair.

Within the already authorized local-only harness/validator scope, if the fresh cycle finds another correctable defect, fix it and keep trying until one final single-hash pair passes every gate.

Do not cross any W5/runtime/external/governance boundary.

## Binding current facts

Already established without crossing W5:

```text
child script current directory = SandboxRoot
relative helper resolution = sandbox shadow helper
git function shim visible from child/copy-equivalent scope
az function shim visible from child/copy-equivalent scope
W5 RunId allocations = 0
W5 wrapper invocations = 0
real external calls = 0
```

Validator correction now explicitly requires:

```text
Push-Location $SandboxRoot
copied-wrapper-scope probe
absence of the superseded wildcard log-download matcher
```

Because validator bytes changed after the preceding successful structural cycle:

```text
preceding SV01-SV36 PASS credit = SUPERSEDED
fresh SV01-SV36 under final pair = REQUIRED
```

## Frozen runner

Must remain:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Do not modify it.

## Step 1 — Freeze current candidate identities

Before structural validation, compute and record:

```text
RunnerSHA256
CurrentHarnessSHA256
CurrentValidatorSHA256
```

Require runner exact match.

The harness and validator hashes become the candidate tuple for this attempt.

## Step 2 — PowerShell/parser gate

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

## Step 3 — Re-prove scope-faithful interception

Do not carry forward the earlier child-scope PASS merely because the harness bytes appear unchanged.

Under the current candidate tuple, freshly prove:

```text
Push-Location SandboxRoot occurs before copy-equivalent child execution
child current location = SandboxRoot
relative helper resolves to sandbox shadow helper
workspace real helper is not selected
git shim resolves in child/copy-equivalent scope
az shim resolves in child/copy-equivalent scope
approved calls are governed locally
unexpected calls fail closed
real git escape blocked
real az escape blocked
real helper escape blocked
location restored after probe
```

## Step 4 — Exact az contract

Freshly prove:

```text
previous wildcard log-download matcher = ABSENT
broad --log-file * matcher = ABSENT
broad webapp show* matcher = ABSENT unless represented solely as normalized exact contracts
```

Every permitted `az` operation must be validated through normalized exact arguments.

For dynamic log-file values require the exact governed sandbox destination, not an arbitrary wildcard path.

## Step 5 — I01-I09

Freshly execute:

```text
I01 approved git call from child/copy-equivalent scope
I02 approved az call from child/copy-equivalent scope
I03 approved relative helper call from child/copy-equivalent scope
I04 unexpected git fails closed
I05 unexpected az fails closed
I06 unexpected helper fails closed
I07 real-command/helper escape blocked from child/copy-equivalent scope
I08 durable sanitized interception ledger complete
I09 disposable artifacts cleaned and working location restored
```

Require:

```text
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

## Step 6 — Fresh SV01-SV36 from zero

Run all canonical SV01-SV36 under this exact pair.

No previous SV record receives acceptance credit.

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

Each record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

The corrected validator must enforce at least:

```text
SV16 — pre-wrapper gates/order
SV27 — executable minimum interception from copied-wrapper scope
SV28 — unexpected calls fail closed
SV29 — no production-policy duplication
C2_EXECUTABLE_INTERCEPTION_BOUNDARY — copied-wrapper-scope executable proof
```

## Step 7 — Future runtime ordering

Structurally prove:

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
→ only then future W5 RunId allocation
→ only then future copied-wrapper invocation
```

This authority stops before the final two actions.

## Retry-until-PASS rule

If any in-scope harness/validator structural defect appears:

```text
classify
→ correct locally
→ recompute changed hashes
→ supersede all prior candidate PASS credit
→ allocate a new fresh structural durable root
→ restart from Step 1 / SV01
→ continue until one final single-hash candidate passes every gate
```

Do not stop merely to request another authority for a defect wholly correctable inside the disposable/local harness or validator.

### Mandatory stop boundary

Stop if correction requires:

```text
frozen runner modification
tracked/production source modification
change to either pre-existing tracked WP04 script
new architecture/policy decision not covered here
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
real external call
W5 RunId allocation
governed W5 wrapper execution
W6/W7/W8
publication/lifecycle closure
```

## Runtime exclusions across every retry

Require throughout:

```text
W5 RunId allocations = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

## Finalization

For the final successful pair require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Evidence must remain resolvable after cleanup.

## Repository invariants

Only these two pre-existing modified tracked paths may remain:

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

## Durable evidence

Use a fresh durable root for each byte-distinct candidate.

Final successful root must retain:

```text
runner/harness/validator copies + hashes
scope-faithful child probe evidence
Push-Location/location-restoration evidence
relative-helper resolution evidence
exact git/az/helper allowlists
dynamic log-file exact-path evidence
wildcard-absence evidence
I01-I09 evidence
real-command escape evidence
future runtime ordering evidence
C2_EXECUTABLE_INTERCEPTION_BOUNDARY evidence
fresh SV01-SV36 ledger
36/36 evidence manifest/resolution
finalization checkpoints
repo invariants
retry history
```

## PASS markers

Only after a fresh complete structural cycle passes under one final tuple:

```text
RELEASE 1.12 WP04 — TERRA C2 FINAL-PAIR FRESH STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — C2 COPIED-WRAPPER INTERCEPTION SCOPE: PASS
RELEASE 1.12 WP04 — C2 EXACT AZ ARGUMENT CONTRACT: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 FINAL HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FINAL VALIDATOR IDENTITY: FROZEN
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

## Required handoff

Return:

```text
RunnerSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededCandidateHashes
DurableRoot
PushLocationSandboxRootProof
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
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
RealGitEscapeResult
RealAzEscapeResult
RealHelperEscapeResult
PreRunIdOrderingResult
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
RuntimePredicatePassClaims
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
CanonicalCheckpointCounts
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

## Next gate

A Terra PASS still does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Scope-Faithful Interception Final Structural Reconciliation
```

Only its PASS may authorize a fresh Terra W5 execution and new RunId allocation.

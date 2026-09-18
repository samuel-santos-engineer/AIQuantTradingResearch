# Release 1.12 WP04 — Luna C2 Executable Evidence Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform an independent, read-only final reconciliation of the fresh C2 executable-evidence tuple.

This authority MUST NOT modify the runner, harness, validator, child probe, production/tracked source, repository state, Git/GitHub, Azure, Docker/GHCR, Twelve Data configuration, or external services.

Do not allocate a W5 RunId. Do not invoke the governed W5 wrapper.

## Exact bound tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
E24A04AE1FDCE7CD26B8B4C37A3E7C4CE11793BF7A52899CE219299AF3293A41

ValidatorSHA256 =
BE46E21FA5CA9056BA8F5D1474B6F151D62BD5759B028FC9FD342E2D7792AC51

ChildProbeSHA256 =
A010BF7B2638F2B4698E6CFDBBE26ECD449172D4636F9C4B861861CE425B3387

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\9b6d36100cbb4d8695e23ae7f2306b9e
```

Do not compose acceptance evidence from earlier roots or hashes.

# R01 — Exact identities and durable binding

Independently recompute all four hashes.

Require exact equality to the bound tuple.

Verify the retained harness, validator, child probe, named-probe records, manifests, inventories, SV ledger, evidence-resolution artifacts, checkpoints, and final summary all belong to this exact durable root and tuple.

# R02 — Exact Windows PowerShell parser proof

Require actual execution under:

```text
Windows PowerShell 5.1.26100.9444
```

Independently verify parser results for:

```text
runner
harness
validator
generated/retained child probe
```

Require parser errors `0/0/0/0`.

A generic parser statement without exact WinPS 5.1 evidence is NOT_PROVEN.

# R03 — Named-probe manifest completeness

Require a durable canonical manifest with exactly:

```text
H01-H08 = 8
T01-T18 = 18
F01-F18 = 18
I01-I09 = 9
Total = 53
```

Require:

```text
ExpectedNamedProbeCount = 53
ObservedNamedProbeCount = 53
UniqueNamedProbeIdCount = 53
MissingNamedProbeCount = 0
UnexpectedNamedProbeCount = 0
DuplicateNamedProbeCount = 0
```

No subset aggregation is acceptable.

# R04 — Individual record integrity

Inspect the retained H/T/F/I record artifacts.

Every one of the 53 records must individually expose:

```text
ProbeId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require each `Result` to be derived from its actual `Expected` and `Observed` evidence.

Reject literal/optimistic PASS assignment independent of executed/asserted evidence.

Require:

```text
NamedProbeFailedCount = 0
NamedProbeUnresolvedEvidenceCount = 0
```

# R05 — H01-H08 / child artifact / containment

Require all eight H records individually visible and evidence-derived.

Independently verify:

```text
parent harness parses
child probe parses
here-string boundaries are valid
shared parent orchestration belongs to parent AST
top-level dispatch belongs to parent AST
RunId allocation site belongs to parent script
wrapper invocation site belongs to parent script
child probe contains only intended child/copy-equivalent logic
```

Require:

```text
H01-H08 = ALL_PASS
```

Verify durable child-probe copy/hash identity and byte equality.

# R06 — T01-T18 top-level runtime reachability

Inspect all 18 individual T records and their evidence.

Require executable/AST/control-flow evidence for:

```text
future W5 invocation → exact top-level dispatch
dispatch → shared orchestration
all required pre-RunId gates on same path
all RunId sites dominated by preflight
all wrapper sites dominated by RunId + preflight
no bypass path
structural execution stops immediately before allocation
structural execution allocates 0 RunIds
structural execution invokes 0 wrappers
structural execution makes 0 real external calls
```

Require:

```text
T01-T18 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
```

The C2 summary must be derived from T records plus required direct dominance/shared-path evidence.

# R07 — Production reachable-external inventory

Independently inspect the exact production wrapper.

Derive the complete reachable external-call inventory across nominal, failure, finally, and restoration paths.

At minimum reconcile all currently identified shapes:

```text
git:
1. rev-parse HEAD
2. merge-base --is-ancestor ...
3. ls-files --error-unmatch -- <wrapper>
4. diff --quiet HEAD -- <wrapper>

az:
1. webapp show — image query
2. webapp show — state query
3. webapp config appsettings list
4. webapp log download --log-file <exact evidence-root>/raw-app-service-logs.zip

helper:
1. Deferred
2. RestoreOnly + RestorationDescriptor
```

If another reachable external executable/script/process call exists, the inventory is incomplete and this gate FAILS.

# R08 — Executed approved surface and F01-F18

Require three distinct durable inventories:

```text
ProductionReachableExternalSurface
HarnessGovernedInterceptionSurface
ExecutedApprovedProbeSurface
```

Require exact equality:

```text
ProductionReachableExternalSurface
==
HarnessGovernedInterceptionSurface
==
ExecutedApprovedProbeSurface
```

Verify each approved Git/Azure/helper shape was actually executed through the interception layer and has evidence of normalized arguments, interceptor selection, synthetic result, ledger record, and no real-process selection.

Inspect all F01-F18 records individually and require:

```text
F01-F18 = ALL_PASS
C2FullReachableExternalSurface = PASS
```

No representative/subset probing is sufficient.

# R09 — I01-I09 child/copy-equivalent interception

Inspect all nine I records individually.

Require actual child/copy-equivalent scope evidence proving:

```text
git interception selected
az interception selected
relative helper resolves to sandbox shadow helper
unexpected git fails closed
unexpected az fails closed
unexpected helper fails closed
real git/az/workspace-helper escape blocked
complete durable interception ledger
cleanup/location restoration
```

Require:

```text
I01-I09 = ALL_PASS
C2ExecutableInterceptionBoundary = PASS
```

The C2 summary must be evidence-derived.

# R10 — Negative fail-closed probes

Independently verify negative probes cover at least:

```text
git:
unsupported subcommand
extra argument
altered governed path/value

az:
unsupported operation
extra argument
altered RG/app
arbitrary log-file path
traversal/alternate-root log-file path

helper:
unsupported mode
missing descriptor
altered/arbitrary descriptor
replayed descriptor
mismatched RG/app
unsupported extra shape where applicable
```

Require proof that none reaches a real external command/helper.

# R11 — Deferred/RestoreOnly descriptor contract

Verify exact two-shape helper semantics:

```text
Deferred
→ exact governed args
→ sanitized non-empty opaque descriptor

RestoreOnly
→ exact governed args
→ exact preceding descriptor
→ same synthetic identity/RG/app
→ one-time use
```

Require rejection of missing, altered, arbitrary, cross-attempt, mismatched, and replayed descriptors.

Verify this represents the production argument surface without duplicating Azure lifecycle policy.

# R12 — SV01-SV36 acceptance relevance

Require exactly:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Each SV record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Critically, verify the SV contract makes the 53-record completeness, evidence resolution, H/T/F/I aggregates, three C2 summaries, runtime exclusion, and final ledger consistency acceptance-relevant.

A 36/0 ledger that could still pass with missing named probes is FAIL.

# R13 — Durable derivation and reopen consistency

Independently trace:

```text
53 individual evidence-derived records
→ H/T/F/I aggregates
→ three C2 summaries
→ SV acceptance
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

After reopen require:

```text
HRecordCount = 8
TRecordCount = 18
FRecordCount = 18
IRecordCount = 9
NamedProbeRecordCount = 53
UniqueNamedProbeIdCount = 53
NamedProbeFailedCount = 0
NamedProbeUnresolvedEvidenceCount = 0

H01-H08 = ALL_PASS
T01-T18 = ALL_PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS

C2TopLevelRuntimeReachability = PASS
C2FullReachableExternalSurface = PASS
C2ExecutableInterceptionBoundary = PASS

FinalStructuralLedgerConsistency = PASS
```

Reject any hard-coded aggregate that is not consistent with member records.

# R14 — Runtime exclusion, finalization, repository invariants

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Require:

```text
W5 RunIds allocated = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

Only the two pre-existing WP04 tracked modifications may remain:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
Git/GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
W5 mutations = 0
```

# Reconciliation classification

Classify each R01-R14:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

This is read-only reconciliation. Do not repair artifacts.

# PASS markers

Only if every material gate passes:

```text
RELEASE 1.12 WP04 — LUNA C2 EXECUTABLE EVIDENCE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE MANIFEST 53/53: ACCEPTED
RELEASE 1.12 WP04 — C2 INDIVIDUAL EVIDENCE-DERIVED PROBE RECORDS: ACCEPTED
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — C2 COMPLETE EXECUTED EXTERNAL-CALL INVENTORY: ACCEPTED
RELEASE 1.12 WP04 — C2 THREE-WAY EXTERNAL-SURFACE EQUALITY: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: ACCEPTED
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ACCEPTED
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. A separate GPT-5.6 Terra W5 runtime authority is required.

# Failure behavior

On FAIL/NOT_PROVEN return:

```text
first failing gate
exact evidence
defect owner
whether harness-only / validator-only / harness+validator remediation suffices
whether runner/production/tracked change is required
```

Do not authorize W5.

# Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
ChildProbeSHA256Observed
DurableRootObserved
WindowsPowerShellVersion
ParserErrorCountsRunnerHarnessValidatorChild

ExpectedNamedProbeCount
ObservedNamedProbeCount
UniqueNamedProbeIdCount
MissingNamedProbeCount
UnexpectedNamedProbeCount
DuplicateNamedProbeCount
NamedProbeFailedCount
NamedProbeUnresolvedEvidenceCount

HRecordCount
H01-H08Aggregate
TRecordCount
T01-T18Aggregate
FRecordCount
F01-F18Aggregate
IRecordCount
I01-I09Aggregate

ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionSurfaceInventory
ExecutedApprovedProbeSurfaceInventory
ThreeWaySurfaceEqualityResult
ApprovedGitShapeProbeResults
ApprovedAzShapeProbeResults
ApprovedHelperShapeProbeResults
NegativeGitProbeResults
NegativeAzProbeResults
NegativeHelperProbeResults

ChildProbeByteEquality
ChildScopeGitInterceptionResult
ChildScopeAzInterceptionResult
ChildScopeHelperInterceptionResult
WorkspaceHelperEscapeResult
RealExternalEscapeResult

C2TopLevelRuntimeReachabilityDerivation
C2FullReachableExternalSurfaceDerivation
C2ExecutableInterceptionBoundaryDerivation
C2TopLevelRuntimeReachabilityReopened
C2FullReachableExternalSurfaceReopened
C2ExecutableInterceptionBoundaryReopened

SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
FinalStructuralLedgerConsistency

GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
CanonicalCheckpointCounts

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

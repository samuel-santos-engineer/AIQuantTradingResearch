# Release 1.12 WP04 — Terra C2 Semantic Individual Probes + Negative Matrix Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue C2 remediation entirely within the disposable harness + validator scope.

The latest Luna reconciliation is binding:

```text
R01 = PASS
R02 = PASS
R03 = PASS by count
R04 = FAIL — named records are blanket/duplicated aggregate claims
R10 = FAIL — negative matrix incomplete
R11 = FAIL — descriptor rejection semantics incomplete
R12 = FAIL — SV acceptance cannot prove individual named-record contract
R13 = FAIL — aggregate/round-trip cannot repair invalid member evidence
R14 = PASS
```

First failing gate: **R04**.

W5 remains forbidden.

Correct the disposable harness/validator so every H/T/F/I record has its own semantic requirement, own observed assertion, and resolvable evidence; implement and execute the complete negative matrix; prove descriptor correlation/rejection/replay semantics; then re-hash, create a fresh durable root, and rerun the complete structural cycle from zero.

Within harness/validator-only scope, keep diagnosing/correcting/re-hashing/restarting until PASS.

# Superseded tuple

```text
Runner =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Harness =
E24A04AE1FDCE7CD26B8B4C37A3E7C4CE11793BF7A52899CE219299AF3293A41

Validator =
BE46E21FA5CA9056BA8F5D1474B6F151D62BD5759B028FC9FD342E2D7792AC51

ChildProbe =
A010BF7B2638F2B4698E6CFDBBE26ECD449172D4636F9C4B861861CE425B3387

Root =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\9b6d36100cbb4d8695e23ae7f2306b9e
```

Preserve the root as rejected evidence. Do not reuse it for acceptance.

# 1. Prohibit blanket probe cloning

Forbidden:

```text
multiple ProbeIds sharing one generic Requirement
multiple ProbeIds sharing one generic Observed assertion
multiple ProbeIds pointing to one aggregate evidence object without distinct member evidence
Result=PASS copied across a family
```

Example of specifically rejected evidence:

```text
Requirement: complete approved external-call shape assertion
Observed: approved=True;threeWay=True
EvidenceReference: c2-executable-evidence.json#/ApprovedEntries
```

That may support an aggregate but cannot serve as the individual evidence for F01-F18.

Every named record must prove a distinct contract.

# 2. Required record semantics

Every H/T/F/I record must retain:

```text
ProbeId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Additionally retain an internal stable `AssertionKey` or equivalent so the validator can prove one-to-one mapping between the named record and the executed assertion.

Require:

```text
53 named IDs
53 distinct semantic contracts
53 mapped assertion keys
0 missing mappings
0 ambiguous mappings
0 unresolved evidence
0 failed assertions
```

Two records may reference the same underlying artifact only when their `EvidenceReference` resolves to distinct fields/events/assertions within it and their semantic requirements differ.

# 3. H01-H08 exact semantic contracts

Retain eight distinct records proving, respectively:

```text
H01 here-string delimiters/structure valid
H02 parent harness parses under exact WinPS 5.1
H03 generated child probe parses under exact WinPS 5.1
H04 shared orchestration definition belongs to parent AST outside string extents
H05 top-level dispatch belongs to parent AST outside string extents
H06 RunId allocation site belongs to parent script outside child text
H07 future governed-wrapper invocation site belongs to parent script outside child text
H08 child probe contains only intended child/copy-equivalent logic and no parent orchestration duplication
```

Each record needs its own observed evidence.

# 4. T01-T18 exact semantic contracts

Retain 18 distinct records:

```text
T01 future W5 invocation maps to exact accepted top-level entrypoint
T02 top-level entrypoint reaches shared parent orchestration
T03 identity/hash gates are reachable and ordered
T04 exact-byte wrapper-copy gate is reachable and ordered
T05 G01-G11 are reachable and ordered
T06 executable interception installation is reachable and ordered
T07 SandboxRoot binding is reachable and ordered
T08 complete three-way external-surface equality gate is reachable and ordered
T09 Deferred/RestoreOnly positive probes are reachable and ordered
T10 exact approved git/az/helper probes are reachable and ordered
T11 complete negative/escape matrix is reachable and ordered
T12 executable-interception PASS gate dominates RunId allocation
T13 full-reachable-surface PASS gate dominates RunId allocation
T14 every RunId allocation site is dominated by all mandatory pre-RunId gates
T15 every governed wrapper invocation site is dominated by RunId allocation and preflight
T16 no alternate/bypass top-level path exists
T17 structural top-level execution reaches the explicit stop immediately before RunId allocation
T18 structural execution records 0 W5 RunIds, 0 wrapper invocations, 0 real external calls
```

No T record may merely inherit a family aggregate.

# 5. F01-F18 exact semantic contracts

Use exactly these 18 distinct full-surface contracts:

```text
F01 complete production reachable-external-surface inventory derived from wrapper
F02 complete harness governed-interception inventory
F03 complete actually-executed approved-probe inventory
F04 exact three-way equality: production == interception == executed
F05 all four approved Git shapes individually executed and matched
F06 both distinct approved `az webapp show` shapes individually executed and matched
F07 approved `az webapp config appsettings list` shape executed and matched
F08 approved `az webapp log download` shape executed with exact governed log-file root
F09 Deferred helper exact argument shape executed and matched
F10 Deferred emits non-empty sanitized opaque descriptor correlated to synthetic attempt/RG/app
F11 RestoreOnly exact argument shape executed using exact preceding descriptor
F12 RestoreOnly descriptor accepted only for same attempt/RG/app and only once
F13 complete Git negative matrix executed and fail-closed
F14 complete Azure negative matrix executed and fail-closed
F15 complete helper/descriptor negative matrix executed and fail-closed
F16 child/finally-path helper resolution stays in sandbox and workspace-helper escape is blocked
F17 no wildcard interception contracts and no production reachable external surface omitted
F18 cleanup/location restoration complete with durable executed-call ledger retained
```

Every F record must point to evidence specifically proving that requirement.

# 6. I01-I09 exact semantic contracts

Use exactly:

```text
I01 child/copy-equivalent approved Git interception executes through sandbox shim
I02 child/copy-equivalent approved Azure interception executes through sandbox shim
I03 child/copy-equivalent approved relative helper resolves to sandbox shadow helper
I04 child-scope unexpected/unsupported Git invocation fails closed without real escape
I05 child-scope unexpected/unsupported Azure invocation fails closed without real escape
I06 child-scope unexpected/unsupported helper invocation fails closed without workspace escape
I07 real git/az/workspace-helper executable escape is blocked
I08 complete child interception ledger is durably retained and resolves to executed events
I09 child/sandbox cleanup and working-location restoration complete
```

# 7. Complete Git negative matrix

Execute and retain separate negative assertions for at least:

```text
GNEG01 unsupported subcommand
GNEG02 approved command with extra argument
GNEG03 ls-files with altered governed wrapper path
GNEG04 diff with altered governed wrapper path
GNEG05 merge-base with altered expected source/ancestor identity
```

If the exact interceptor contract exposes another meaningful governed argument, test alteration of it too.

For every case require:

```text
rejected = true
real process selected = false
real external call count delta = 0
negative ledger event retained = true
```

F13 derives from all Git negative records.

# 8. Complete Azure negative matrix

Execute and retain separate negative assertions for at least:

```text
ANEG01 unsupported operation
ANEG02 approved operation with extra argument
ANEG03 image-show with altered resource group
ANEG04 image-show with altered webapp name
ANEG05 state-show with altered resource group/app
ANEG06 appsettings-list with altered resource group/app
ANEG07 log-download with altered resource group/app
ANEG08 log-download with arbitrary outside-root path
ANEG09 log-download with traversal path
ANEG10 log-download with alternate-root path that is syntactically valid but not the governed evidence root
```

For every case require rejection without real external escape.

F14 derives from the complete Azure negative set.

# 9. Complete helper/descriptor negative matrix

The harness must execute and retain at least:

```text
HNEG01 unsupported RestorationMode/helper shape
HNEG02 RestoreOnly missing descriptor
HNEG03 RestoreOnly empty descriptor
HNEG04 RestoreOnly altered descriptor
HNEG05 RestoreOnly arbitrary fabricated descriptor
HNEG06 RestoreOnly replay of already-consumed descriptor
HNEG07 RestoreOnly descriptor from different synthetic attempt
HNEG08 RestoreOnly correct descriptor but mismatched ResourceGroup
HNEG09 RestoreOnly correct descriptor but mismatched WebAppName
HNEG10 RestoreOnly extra unsupported argument/shape, if parameter binding permits such a call to reach the shadow contract
```

Also prove:

```text
Deferred descriptor is non-empty
descriptor is sanitized/opaque
descriptor is correlated to attempt identity + RG + app
first exact RestoreOnly consumes it successfully
second use fails
altered/fabricated/cross-attempt/mismatched uses fail
no rejection reaches workspace helper or real external process
```

F10-F12 and F15 must derive from these positive/negative records.

# 10. Negative matrix manifest

Create a durable negative manifest with exact expected IDs.

Require validator equality:

```text
ExpectedNegativeIds == ObservedNegativeIds
```

At minimum:

```text
Git negative count >= 5
Azure negative count >= 10
Helper negative count >= 9
MissingNegativeCount = 0
Unexpected acceptance = 0
NegativeFailureCount = 0
NegativeRealExternalEscapeCount = 0
```

If HNEG10 is structurally not applicable because PowerShell parameter binding rejects it before shadow-helper contract entry, retain an explicit evidence-derived `NOT_APPLICABLE_BY_BINDING` assertion and do not count it as a silent omission.

# 11. Positive external-shape manifest

Retain individually executed positive events for:

```text
Git x4
Azure x4
Helper x2
```

Require:

```text
ExpectedApprovedShapeCount = 10
ObservedApprovedShapeCount = 10
UniqueApprovedShapeCount = 10
MissingApprovedShapeCount = 0
ApprovedShapeFailureCount = 0
RealExternalCallCount = 0
```

If independent wrapper inspection discovers additional reachable external shapes, increase the expected count and update the manifests before acceptance.

# 12. Child/copy-equivalent artifact

Continue to retain the distinct child probe by hash.

The child probe must execute real structural interception probes from SandboxRoot, not merely inspect parent records.

Require its evidence to feed I01-I09 directly.

# 13. Three-way equality

Recompute from explicit inventories:

```text
ProductionReachableExternalSurface
==
HarnessGovernedInterceptionSurface
==
ExecutedApprovedProbeSurface
```

Equality must compare normalized call identities and governed argument contracts, not just counts.

# 14. C2 summary derivation

Compute:

```text
C2TopLevelRuntimeReachability
```

only from T01-T18 plus direct control-flow/dominance evidence.

Compute:

```text
C2FullReachableExternalSurface
```

only from F01-F18 plus exact three-way equality and complete positive/negative manifests.

Compute:

```text
C2ExecutableInterceptionBoundary
```

only from I01-I09 + H01-H08 + child-scope escape evidence.

Literal PASS assignment is forbidden.

# 15. SV01-SV36

Keep exactly 36 SV records, separate from named probes.

SV must fail if any acceptance-relevant named/negative/positive evidence is missing.

Require SV coverage of:

```text
53/53 semantic named-record mapping
53/53 evidence resolution
H aggregate
T aggregate
F aggregate
I aggregate
positive-shape manifest completeness
negative-matrix completeness
descriptor rejection/replay semantics
three-way equality
all three C2 summaries
durable reopen consistency
runtime exclusion
repo invariants
```

A 36/0 ledger is valid only if those dependencies are actually enforced.

# 16. Fresh durable tuple

Any harness/validator/child byte correction requires re-hashing changed artifacts.

Create a fresh durable root and restart the entire structural cycle.

Do not reuse prior roots.

# 17. Durable round trip

Require:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After reopen independently validate all named records, positive/negative manifests, C2 summaries, SV records, and evidence references.

# 18. Retry-until-PASS

For every defect correctable entirely within disposable harness/validator scope:

```text
diagnose
→ correct
→ re-hash
→ preserve/supersede old root
→ fresh durable root
→ restart complete cycle
→ repeat until PASS
```

Do not stop for another locally correctable probe/record/serialization/validator defect.

## Mandatory stop boundary

STOP if correction requires:

```text
frozen runner change
production/tracked source change
new Luna architecture/policy decision
staging/commit/push
Git/GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
real external invocation
W5 RunId allocation
governed W5 wrapper execution
W6/W7/W8
publication/lifecycle mutation
```

# 19. Runtime/repository invariants

Throughout:

```text
W5 RunIds allocated = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

Only the two pre-existing WP04 tracked modifications may remain.

# PASS markers

Only after a fresh tuple satisfies all requirements:

```text
RELEASE 1.12 WP04 — TERRA C2 SEMANTIC INDIVIDUAL PROBES + NEGATIVE MATRIX REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE SEMANTIC MAPPING: 53/53 PASS
RELEASE 1.12 WP04 — C2 POSITIVE APPROVED-SHAPE MANIFEST: COMPLETE
RELEASE 1.12 WP04 — C2 NEGATIVE FAIL-CLOSED MATRIX: COMPLETE
RELEASE 1.12 WP04 — C2 DESCRIPTOR CORRELATION/REPLAY REJECTION: PASS
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ALL_PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 THREE-WAY EXTERNAL-SURFACE EQUALITY: PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE EVIDENCE: 53/53 RESOLVED
RELEASE 1.12 WP04 — C2 SV EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: PASS
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 SEMANTIC-EVIDENCE FINAL RECONCILIATION: YES
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
FinalHarnessSHA256
FinalValidatorSHA256
FinalChildProbeSHA256
WindowsPowerShellVersion
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot

NamedProbeRecordCount
UniqueNamedProbeIdCount
DistinctSemanticRequirementCount
MappedAssertionKeyCount
MissingAssertionMappingCount
AmbiguousAssertionMappingCount
NamedProbeFailedCount
NamedProbeUnresolvedEvidenceCount

H01-H08RecordsAndEvidence
T01-T18RecordsAndEvidence
F01-F18RecordsAndEvidence
I01-I09RecordsAndEvidence

ExpectedApprovedShapeCount
ObservedApprovedShapeCount
ApprovedShapeFailureCount
ApprovedPositiveManifestPath

ExpectedGitNegativeIds
ObservedGitNegativeIds
GitNegativeFailureCount
ExpectedAzureNegativeIds
ObservedAzureNegativeIds
AzureNegativeFailureCount
ExpectedHelperNegativeIds
ObservedHelperNegativeIds
HelperNegativeFailureCount
NegativeRealExternalEscapeCount
NegativeManifestPath

DeferredDescriptorGenerationEvidence
RestoreOnlyFirstUseEvidence
DescriptorReplayRejectionEvidence
AlteredDescriptorRejectionEvidence
ArbitraryDescriptorRejectionEvidence
CrossAttemptDescriptorRejectionEvidence
MismatchedResourceGroupRejectionEvidence
MismatchedWebAppRejectionEvidence

ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionSurfaceInventory
ExecutedApprovedProbeSurfaceInventory
ThreeWaySurfaceEqualityResult

C2TopLevelRuntimeReachabilityDerivation
C2FullReachableExternalSurfaceDerivation
C2ExecutableInterceptionBoundaryDerivation

SVRecordCount
SVFailedCount
ResolvedSVEvidenceCount
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
FinalStructuralResult
FirstBoundaryCrossingDefectIfAny
NextAuthorityRequired
```

# Next gate

A Terra PASS does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Semantic Executable-Evidence Final Reconciliation
```

Only Luna acceptance may authorize a fresh governed W5 runtime and W5 RunId.

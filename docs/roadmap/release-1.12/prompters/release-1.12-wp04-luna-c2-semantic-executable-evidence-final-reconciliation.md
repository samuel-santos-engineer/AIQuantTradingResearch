# Release 1.12 WP04 — Luna C2 Semantic Executable-Evidence Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform a fresh independent read-only reconciliation of the latest C2 structural tuple.

Do not modify the frozen runner, disposable harness/validator/child probe, tracked or production source, repository state, Git/GitHub, Azure, Docker/GHCR, Twelve Data configuration, or any external service.

Do not allocate a W5 RunId. Do not invoke the governed W5 wrapper.

## Exact bound tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
4DE9F18E9CF978B30C107CBA2F17E09C29302AC1E9469453129A9FF3B9D23596

ValidatorSHA256 =
B5F270A95FB19A1A1F999C2155854860FCF45F07147D9F4C353A63BCC69F82A1

ChildProbeSHA256 =
A21499B3717ED832F47159A7B4F7B0941139684284710200803D291A6053F24D

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\bb1af71a7e5349ab98c38fcb77e06a92
```

No earlier tuple/root may contribute acceptance credit.

# R01 — Tuple identity

Recompute all four hashes and require exact equality.

Verify all retained evidence belongs to this exact tuple/root.

# R02 — Exact Windows PowerShell proof

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
child parser errors = 0
```

# R03 — Physical named-evidence topology

Require exactly:

```text
H01-H08 = 8
T01-T18 = 18
F01-F18 = 18
I01-I09 = 9
Total = 53
```

Require one physically retained evidence file per named probe under the durable root, e.g.:

```text
named-evidence/<ProbeId>.json
```

Require:

```text
53 expected
53 observed
53 unique IDs
53 physically present files
0 missing
0 duplicate
0 unexpected
0 unresolved
```

# R04 — Semantic individuality

This is a critical regression gate.

Inspect every one of the 53 records and its physical evidence file.

Each named probe must have its own:

```text
ProbeId
AssertionKey
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require distinct semantic requirements and one-to-one assertion mappings.

Reject:

```text
blanket family records
copied aggregate claims
one generic Observed value cloned across multiple probe IDs
optimistic/literal PASS assignment
aggregate-only evidence used as individual proof
```

Two probes may share a source artifact only when they resolve to distinct evidence fields/events/assertions and prove different contracts.

Require:

```text
DistinctSemanticRequirementCount = 53
MappedAssertionKeyCount = 53
MissingAssertionMappingCount = 0
AmbiguousAssertionMappingCount = 0
NamedProbeFailedCount = 0
```

# R05 — H01-H08

Independently reconcile the eight semantic contracts:

```text
H01 here-string structure valid
H02 parent harness parses under exact WinPS 5.1
H03 child probe parses under exact WinPS 5.1
H04 shared orchestration belongs to parent AST outside strings
H05 top-level dispatch belongs to parent AST outside strings
H06 RunId allocation site belongs to parent script
H07 future wrapper invocation site belongs to parent script
H08 child contains only intended child/copy-equivalent logic
```

Require `H01-H08 = ALL_PASS`.

# R06 — T01-T18

Independently reconcile each T record against its evidence.

Require distinct proof of shared top-level dispatch, ordered pre-RunId gates, three-way equality gate, positive and negative probe reachability, RunId dominance, wrapper dominance, bypass absence, structural stop, and zero runtime activity.

Require:

```text
T01-T18 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
```

The C2 summary must be derived from member evidence.

# R07 — Positive approved external-call surface

Independently inspect the production wrapper and derive its complete reachable external-call surface.

At minimum reconcile:

```text
Git x4
- rev-parse HEAD
- merge-base --is-ancestor
- ls-files --error-unmatch -- <wrapper>
- diff --quiet HEAD -- <wrapper>

Azure x4
- webapp show: image query
- webapp show: state query
- webapp config appsettings list
- webapp log download with governed log-file path

Helper x2
- Deferred
- RestoreOnly + exact preceding RestorationDescriptor
```

If production inspection reveals additional reachable external shapes, acceptance requires them too.

Require individually executed positive evidence for every production shape.

# R08 — Three-way external-surface equality

Require durable normalized inventories:

```text
ProductionReachableExternalSurface
HarnessGovernedInterceptionSurface
ExecutedApprovedProbeSurface
```

Require exact semantic equality, including governed argument contracts:

```text
Production == Harness == Executed
```

Count-only equality is insufficient.

# R09 — F01-F18 semantic individuality

Require these distinct contracts to be individually evidenced:

```text
F01 production reachable-surface inventory
F02 harness interception inventory
F03 executed approved-probe inventory
F04 exact three-way equality
F05 all four Git positive shapes
F06 both distinct az webapp show shapes
F07 appsettings-list shape
F08 log-download exact governed-root shape
F09 Deferred exact shape
F10 descriptor generation/correlation
F11 RestoreOnly exact descriptor shape
F12 same-attempt/RG/app + one-use descriptor semantics
F13 complete Git negative matrix
F14 complete Azure negative matrix
F15 complete helper/descriptor negative matrix
F16 child/finally helper sandbox resolution + workspace escape block
F17 no wildcard contracts / no omitted reachable surface
F18 cleanup/location restoration + durable call ledger
```

Require `F01-F18 = ALL_PASS`.

# R10 — Complete Git negative matrix

Require executed evidence for at least:

```text
GNEG01 unsupported subcommand
GNEG02 approved command + extra argument
GNEG03 ls-files altered wrapper path
GNEG04 diff altered wrapper path
GNEG05 merge-base altered source/ancestor identity
```

For each:

```text
rejected = true
real process selected = false
real external call delta = 0
negative ledger event retained = true
```

# R11 — Complete Azure negative matrix

Require executed evidence for at least:

```text
ANEG01 unsupported operation
ANEG02 approved operation + extra argument
ANEG03 image-show altered RG
ANEG04 image-show altered app
ANEG05 state-show altered RG/app
ANEG06 appsettings-list altered RG/app
ANEG07 log-download altered RG/app
ANEG08 arbitrary outside-root log path
ANEG09 traversal log path
ANEG10 alternate-root log path
```

Every case must fail closed without real Azure execution.

# R12 — Helper descriptor semantics and negative matrix

Require positive proof:

```text
Deferred emits non-empty sanitized opaque descriptor
descriptor correlated to synthetic attempt + RG + app
first exact RestoreOnly succeeds
descriptor is consumed once
```

Require negative proof for at least:

```text
HNEG01 unsupported mode/shape
HNEG02 missing descriptor
HNEG03 empty descriptor
HNEG04 altered descriptor
HNEG05 fabricated descriptor
HNEG06 replayed descriptor
HNEG07 descriptor from another synthetic attempt
HNEG08 correct descriptor + mismatched RG
HNEG09 correct descriptor + mismatched app
HNEG10 unsupported extra shape, or explicit NOT_APPLICABLE_BY_BINDING evidence
```

No negative case may reach the workspace helper or real external process.

# R13 — I01-I09 child/copy-equivalent interception

Require actual child-scope evidence for:

```text
I01 approved Git interception
I02 approved Azure interception
I03 approved relative helper interception
I04 unexpected Git fail-closed
I05 unexpected Azure fail-closed
I06 unexpected helper fail-closed
I07 real git/az/workspace-helper escape blocked
I08 complete durable child interception ledger
I09 cleanup/location restoration
```

Require:

```text
I01-I09 = ALL_PASS
C2ExecutableInterceptionBoundary = PASS
```

# R14 — SV01-SV36, durable derivation, runtime exclusion

Require exactly:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
ResolvedSVEvidenceCount = 36
RuntimePredicatePassClaims = 0
```

SV acceptance must actually depend on:

```text
53/53 semantic mapping
53/53 physical evidence resolution
H/T/F/I aggregates
positive-shape manifest
complete negative matrices
descriptor rejection/replay semantics
three-way equality
three C2 summaries
durable reopen consistency
runtime exclusion
repository invariants
```

Trace:

```text
individual executable evidence
→ 53 semantic named records
→ H/T/F/I aggregates
→ three C2 summaries
→ SV acceptance
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Require checkpoints exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After reopen require all acceptance predicates still PASS.

Require throughout:

```text
W5 RunIds allocated = 0
W5 wrappers invoked = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

Only the two pre-existing WP04 tracked modifications may remain.

# Classification

Classify R01-R14 individually:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

Do not repair artifacts in this Luna authority.

# PASS markers

Only if all gates pass:

```text
RELEASE 1.12 WP04 — LUNA C2 SEMANTIC EXECUTABLE-EVIDENCE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE SEMANTIC MAPPING 53/53: ACCEPTED
RELEASE 1.12 WP04 — C2 PHYSICAL NAMED EVIDENCE 53/53: ACCEPTED
RELEASE 1.12 WP04 — C2 POSITIVE APPROVED-SHAPE MANIFEST: ACCEPTED
RELEASE 1.12 WP04 — C2 NEGATIVE FAIL-CLOSED MATRIX: ACCEPTED
RELEASE 1.12 WP04 — C2 DESCRIPTOR CORRELATION/REPLAY REJECTION: ACCEPTED
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
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

On FAIL/NOT_PROVEN report:

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
ParserErrorCounts

NamedProbeRecordCount
PhysicalNamedEvidenceFileCount
UniqueNamedProbeIdCount
DistinctSemanticRequirementCount
MappedAssertionKeyCount
MissingAssertionMappingCount
AmbiguousAssertionMappingCount
NamedProbeFailedCount
NamedProbeUnresolvedEvidenceCount

H01-H08Aggregate
T01-T18Aggregate
F01-F18Aggregate
I01-I09Aggregate

ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionSurfaceInventory
ExecutedApprovedProbeSurfaceInventory
ThreeWaySurfaceEqualityResult

ApprovedGitShapeProbeResults
ApprovedAzShapeProbeResults
ApprovedHelperShapeProbeResults

GitNegativeMatrixResults
AzureNegativeMatrixResults
HelperNegativeMatrixResults
NegativeRealExternalEscapeCount

DeferredDescriptorGenerationEvidence
RestoreOnlyFirstUseEvidence
DescriptorReplayRejectionEvidence
AlteredDescriptorRejectionEvidence
ArbitraryDescriptorRejectionEvidence
CrossAttemptDescriptorRejectionEvidence
MismatchedResourceGroupRejectionEvidence
MismatchedWebAppRejectionEvidence

C2TopLevelRuntimeReachabilityDerivation
C2FullReachableExternalSurfaceDerivation
C2ExecutableInterceptionBoundaryDerivation

SVRecordCount
UniqueSVCheckIdCount
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

R01-R14Summary
FinalReconciliationResult
FirstFailingOrNotProvenPredicate
DefectClassification
NextAuthorizedAction
```

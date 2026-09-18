# Release 1.12 WP04 — Luna C2 Semantic Executable-Evidence Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform an independent, read-only final reconciliation of the latest completed post–descriptor-replay C2 structural tuple.

This authority is bound exclusively to the tuple and durable root below. No earlier or later root may contribute acceptance credit.

Do not mutate files, repository state, Git/GitHub, Azure, Docker/GHCR, Twelve Data, tracked/production source, the frozen runner, disposable harness, validator, or child probe.

Do not allocate a W5 RunId. Do not invoke the governed W5 wrapper.

## Exact bound tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
51D222163C2F2F92DF2F887B6D793F190084ABFC7D6E55F6E61CE6CC419BCA26

ValidatorSHA256 =
B5F270A95FB19A1A1F999C2155854860FCF45F07147D9F4C353A63BCC69F82A1

ChildProbeSHA256 =
2A63D4DE384FAFFBBD5EDB1555053D91D57A4F2FF897D3BFF98548AEC7317DEF

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\ee8da7b665384172bf34c6c151b3e16d
```

Discovery asserted this is the newest **completed, internally consistent, no-failure** root after descriptor-replay remediation. Newer failed/incomplete roots are not acceptance candidates.

## Known discovery invariants to independently verify

```text
Descriptor-replay remediation evidence present = YES
Durable root finalization complete = YES
PRE_CLEANUP = 1
CLEANUP_COMPLETE = 1
BUILD_LEDGER = 1
SERIALIZE = 1
PUBLISH = 1
REOPEN = 1
W5 RunIds allocated = 0
Governed W5 wrapper invocations = 0
Real external calls = 0
HNEG06 replay record = PASS
```

Do not accept these merely because discovery reported them; verify from retained evidence.

# R01 — Exact tuple/root identity

Recompute all four hashes from the retained/current artifacts and require exact equality with the bound tuple.

Require all acceptance evidence to resolve under the exact durable root above.

Reject composition from:
- `bb1af71a7e5349ab98c38fcb77e06a92`;
- any earlier C2 root;
- any newer failed/incomplete root;
- current mutable files whose bytes differ from the bound artifacts.

Any mismatch = FAIL and W5 remains blocked.

# R02 — Exact Windows PowerShell/parser proof

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
child parser errors = 0
```

# R03 — Named-record and physical-evidence completeness

Require exactly:

```text
H01-H08 = 8
T01-T18 = 18
F01-F18 = 18
I01-I09 = 9
Total named records = 53
```

Require one retained physical evidence artifact for every ProbeId.

Verify:

```text
Expected = 53
Observed = 53
Unique ProbeIds = 53
Physical named-evidence files = 53
Missing = 0
Duplicate = 0
Unexpected = 0
Unresolved = 0
```

# R04 — Individual semantic evidence

This remains a critical regression gate.

For every H/T/F/I record require:

```text
ProbeId
AssertionKey (or equivalent stable one-to-one assertion identity)
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Independently establish:

```text
DistinctSemanticRequirementCount = 53
MappedAssertionKeyCount = 53
MissingAssertionMappingCount = 0
AmbiguousAssertionMappingCount = 0
NamedProbeFailedCount = 0
NamedProbeUnresolvedEvidenceCount = 0
```

Reject copied family assertions, blanket aggregate rows, literal/synthetic PASS, or aggregate-only evidence masquerading as individual evidence.

Shared source artifacts are permitted only when the individual records resolve to distinct evidence fields/events/assertions proving distinct contracts.

# R05 — H01-H08

Verify individually:

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

Require H01-H08 = ALL_PASS.

# R06 — T01-T18 and top-level runtime reachability

Verify every T record individually.

Require evidence-derived proof of:
- canonical top-level entrypoint;
- shared orchestration reachability;
- identity/hash and exact-byte gates;
- G01-G11 ordering;
- interception installation;
- SandboxRoot binding;
- three-way external-surface equality;
- positive and negative probe reachability;
- pre-RunId dominance;
- wrapper-invocation dominance;
- absence of alternate/bypass paths;
- structural stop immediately before RunId allocation;
- zero W5 RunIds, wrapper invocations, and real external calls.

Require:

```text
T01-T18 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
```

The summary must derive from member evidence.

# R07 — Independently derive production reachable external surface

Inspect the bound production wrapper/source and independently derive the complete reachable external-process surface.

At minimum reconcile:

```text
Git:
1. rev-parse HEAD
2. merge-base --is-ancestor ...
3. ls-files --error-unmatch -- <wrapper>
4. diff --quiet HEAD -- <wrapper>

Azure:
5. webapp show — image query
6. webapp show — state query
7. webapp config appsettings list
8. webapp log download --log-file <governed evidence root>

Helper:
9. Deferred with LifecycleAction None / RestorationMode Deferred
10. RestoreOnly with exact preceding RestorationDescriptor
```

If source inspection reveals another reachable external shape, it is mandatory for acceptance.

# R08 — Positive execution and three-way equality

Require three durable normalized inventories:

```text
ProductionReachableExternalSurface
HarnessGovernedInterceptionSurface
ExecutedApprovedProbeSurface
```

Require exact semantic equality, including governed argument shapes:

```text
ProductionReachableExternalSurface
==
HarnessGovernedInterceptionSurface
==
ExecutedApprovedProbeSurface
```

Count equality alone is insufficient.

Every approved production shape must have an individually executed structural probe.

# R09 — F01-F18

Require individual evidence for exactly these semantic contracts:

```text
F01 production reachable-surface inventory
F02 harness interception inventory
F03 executed approved-probe inventory
F04 exact three-way equality
F05 all four Git positive shapes
F06 both distinct az webapp show shapes
F07 appsettings-list shape
F08 log-download exact governed-root shape
F09 Deferred exact helper shape
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

Require F01-F18 = ALL_PASS.

# R10 — Complete Git negative matrix

Require executed fail-closed evidence for at least:

```text
GNEG01 unsupported subcommand
GNEG02 approved command with extra argument
GNEG03 ls-files altered governed wrapper path
GNEG04 diff altered governed wrapper path
GNEG05 merge-base altered expected source/ancestor identity
```

For every case:

```text
Rejected = true
RealProcessSelected = false
RealExternalCallDelta = 0
NegativeLedgerEventRetained = true
```

# R11 — Complete Azure negative matrix

Require executed fail-closed evidence for at least:

```text
ANEG01 unsupported operation
ANEG02 approved operation with extra argument
ANEG03 image-show altered ResourceGroup
ANEG04 image-show altered WebAppName
ANEG05 state-show altered ResourceGroup/WebAppName
ANEG06 appsettings-list altered ResourceGroup/WebAppName
ANEG07 log-download altered ResourceGroup/WebAppName
ANEG08 arbitrary outside-root log path
ANEG09 traversal log path
ANEG10 alternate-root log path
```

No case may invoke real Azure.

# R12 — Descriptor correlation, mismatch, replay, and helper negatives

This is a critical post-remediation gate.

Positive proof must establish:

```text
Deferred emits a non-empty sanitized opaque descriptor
descriptor correlates to synthetic attempt identity + ResourceGroup + WebAppName
first exact RestoreOnly use succeeds
successful RestoreOnly consumes descriptor exactly once
```

Require separately executed fail-closed evidence for at least:

```text
HNEG01 unsupported mode/shape
HNEG02 missing descriptor
HNEG03 empty descriptor
HNEG04 altered descriptor
HNEG05 arbitrary fabricated descriptor
HNEG06 replay of already-consumed descriptor
HNEG07 descriptor from different synthetic attempt
HNEG08 correct descriptor with mismatched ResourceGroup
HNEG09 correct descriptor with mismatched WebAppName
HNEG10 unsupported extra shape OR explicit evidence-derived NOT_APPLICABLE_BY_BINDING
```

For HNEG06 specifically, do not accept only a retained `Result=PASS`. Trace the record to executable evidence showing:

```text
first exact RestoreOnly = accepted
descriptor state = consumed
second exact RestoreOnly with same descriptor = rejected
workspace helper escape = false
real external call delta = 0
```

Require altered, fabricated, cross-attempt, RG-mismatched, and app-mismatched descriptors to be independently rejected.

# R13 — I01-I09 executable child/copy-equivalent boundary

Require child-scope evidence for:

```text
I01 approved Git interception
I02 approved Azure interception
I03 approved relative helper interception
I04 unexpected Git fail-closed
I05 unexpected Azure fail-closed
I06 unexpected helper fail-closed
I07 real git/az/workspace-helper escape blocked
I08 complete durable child interception ledger
I09 child cleanup/location restoration
```

Require:

```text
I01-I09 = ALL_PASS
C2ExecutableInterceptionBoundary = PASS
```

# R14 — SV01-SV36, derivation chain, durable reopen, and runtime exclusion

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
ResolvedSVEvidenceCount = 36
RuntimePredicatePassClaims = 0
```

SV acceptance must fail closed if any acceptance-relevant named record, physical evidence file, positive shape, negative probe, descriptor semantic, inventory equality, or C2 summary is missing/failing.

Trace the actual derivation:

```text
individual executable evidence
→ 53 individual named records
→ H/T/F/I aggregates
→ positive/negative manifests
→ three-way external-surface equality
→ three C2 summaries
→ SV01-SV36
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Require checkpoint sequence exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After REOPEN, independently require all acceptance predicates still resolve and pass.

Verify:

```text
W5 RunIds allocated = 0
Governed W5 wrapper invocations = 0
Real external calls = 0
Runtime P01-P20 PASS claims = 0
Authority-introduced tracked modifications = 0
Staged paths = 0
git diff --check exit code = 0
```

Only the pre-existing WP04 tracked modifications may remain.

# Classification rule

Classify every gate R01-R14 as:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

This Luna authority is read-only. Do not remediate defects.

# Required PASS markers

Only if every material gate passes:

```text
RELEASE 1.12 WP04 — LUNA C2 SEMANTIC EXECUTABLE-EVIDENCE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 EXACT POST-DESCRIPTOR-REPLAY TUPLE: ACCEPTED
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

Then STOP. A separate GPT-5.6 Terra W5 governed-runtime authority is required.

# Failure behavior

On FAIL or NOT_PROVEN return:

```text
First failing gate
Exact failing evidence/predicate
Defect owner
Harness-only / validator-only / harness+validator / runner / production / policy classification
Whether bounded Terra remediation is sufficient
Whether a new Luna policy/architecture decision is required
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

# Release 1.12 WP04 — Luna Fresh Event-Specific Evidence Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform a read-only final reconciliation of the fresh completed C2 durable root below, including the newly completed executable validator semantic-corruption matrix.

This authority is bound exclusively to:

```text
DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\f815c45fcbde427195b020a1c50279fd

FrozenRunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

The user report did not supply the fresh Harness/Validator/Child hashes. Therefore this authority MUST derive those identities from the finalized retained root before any substantive acceptance check and must bind all later checks to those exact retained identities.

No earlier root or tuple may contribute acceptance credit.

Do not modify any file. Do not allocate a W5 RunId. Do not invoke the governed W5 wrapper. Do not make external calls or mutate Git/GitHub, Azure, Docker/GHCR, Twelve Data, tracked source, production source, or lifecycle state.

# R01 — Fresh tuple derivation and immutable binding

From the finalized root, identify the exact retained harness, validator, and child artifacts and recompute:

```text
HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
```

Recompute the runner and require the frozen hash above.

Require the root's retained manifests/ledger to identify the same artifacts/hashes.

Require current bytes, where applicable, to equal the retained candidate bytes; otherwise classify the current files as later/superseding bytes and reconcile only the finalized retained artifacts.

Before proceeding beyond R01, print:

```text
FRESH_BOUND_TUPLE
RunnerSHA256=<...>
HarnessSHA256=<...>
ValidatorSHA256=<...>
ChildProbeSHA256=<...>
DurableRoot=C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\f815c45fcbde427195b020a1c50279fd
```

R01 FAIL/NOT_PROVEN if:
- any candidate artifact is missing;
- any hash cannot be independently recomputed;
- retained identity metadata disagrees with bytes;
- evidence is composed from another root;
- the root is not finalized/reopened consistently.

# R02 — WinPS/parser proof

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner/harness/validator/child parser errors = 0/0/0/0
```

# R03 — Named topology

Require exactly:

```text
H01-H08 = 8
T01-T18 = 18
F01-F18 = 18
I01-I09 = 9
Total = 53
```

Require 53 unique ProbeIds, 53 physical named-evidence files, 53 resolved event mappings, zero missing/duplicate/unexpected/unresolved.

# R04 — Event-specific semantic individuality

For every named record require an exact resolved event with:

```text
EventId
ProbeId
AssertionKey
Contract/Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Require:

```text
DistinctSemanticRequirementCount = 53
UniqueAssertionKeyCount = 53
UniqueEventIdCount = 53
ProbeToEventMappingCount = 53
MissingProbeEventMappings = 0
AmbiguousProbeEventMappings = 0
GenericAggregateNamedEvidenceCount = 0
FailedNamedEvents = 0
UnresolvedNamedEvents = 0
```

Unique IDs wrapping duplicated generic observations do not pass.

Explicitly reject named evidence based only on generic observations such as:

```text
approved=True;threeWay=True
allPass=True
familyPassed=True
surfaceComplete=True
count-only aggregate
literal/synthetic PASS
```

# R05 — Event-specific regression spot checks

Directly trace:

```text
F05 -> all four approved Git execution events
F10 -> Deferred descriptor-generation/capture/correlation event
F15 -> helper-negative constituent event IDs/outcomes
HNEG06 -> first RestoreOnly accepted, descriptor consumed, replay rejected, no escape
```

Each must resolve to actual event-specific observations, not aggregate labels.

# R06 — H01-H08

Require all eight semantic contracts individually event-derived and ALL_PASS.

# R07 — T01-T18

Require all 18 control-flow/runtime-structure contracts individually event-derived.

Verify canonical entrypoint, ordered pre-RunId gates, G01-G11, interception installation, SandboxRoot, three-way equality, positive/negative reachability, dominance over every RunId allocation and wrapper invocation, no bypass path, structural stop, and zero runtime activity.

Require:

```text
T01-T18 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
```

# R08 — Independent production reachable external surface

Independently derive the production wrapper's complete reachable external-process surface.

At minimum:

```text
Git x4:
rev-parse HEAD
merge-base --is-ancestor
ls-files --error-unmatch -- <wrapper>
diff --quiet HEAD -- <wrapper>

Azure x4:
webapp show — image
webapp show — state
webapp config appsettings list
webapp log download --log-file <governed evidence root>

Helper x2:
Deferred
RestoreOnly with exact preceding descriptor
```

Any additional reachable production shape is mandatory.

# R09 — Three-way equality and positive executions

Require exact normalized semantic equality:

```text
ProductionReachableExternalSurface
==
HarnessGovernedInterceptionSurface
==
ExecutedApprovedProbeSurface
```

Every approved shape must have its own executed event. Count equality is insufficient.

# R10 — F01-F18

Require all 18 records individually event-derived and ALL_PASS.

Specifically prove inventories, equality, Git/Azure/helper positive executions, descriptor generation/use/replay state, complete Git/Azure/helper negative sets, sandbox helper resolution, no wildcard/omitted surface, cleanup/location restoration, and durable ledger.

Require:

```text
F01-F18 = ALL_PASS
C2FullReachableExternalSurface = PASS
```

# R11 — Complete fail-closed negative matrix

Require actual event-specific mismatch/rejection evidence for all required Git, Azure, and helper/descriptor negatives.

At minimum include:
- Git unsupported command, extra arg, altered wrapper paths, altered merge-base identity;
- Azure unsupported operation, extra arg, altered RG/app, outside-root path, traversal path, alternate-root path;
- helper unsupported shape, missing/empty/altered/fabricated/replayed/cross-attempt descriptor, mismatched RG/app, and unsupported extra shape or evidence-derived binding N/A.

Every applicable negative must prove:

```text
rejected = true
real external escape = false
real external call delta = 0
event retained = true
```

# R12 — I01-I09

Require all child/copy-equivalent interception records individually event-derived:

```text
approved Git/Azure/helper interception
unexpected Git/Azure/helper rejection
real executable/workspace-helper escape blocked
durable child interception ledger
cleanup/location restoration
```

Require:

```text
I01-I09 = ALL_PASS
C2ExecutableInterceptionBoundary = PASS
```

# R13 — Full executable semantic-corruption matrix

This is a mandatory fresh gate.

Require evidence that each fixture was first **baseline-accepted**, then corrupted, actually executed through the validator, and rejected for the intended semantic reason.

Require exactly these 11 cases:

```text
01 Missing event
02 Duplicate EventId
03 ProbeId/EventId mismatch
04 AssertionKey mismatch
05 EvidenceReference to wrong event
06 F05 generic aggregate substitution
07 F10 generic aggregate substitution
08 F15 generic aggregate substitution
09 T-family generic aggregate substitution
10 I-family generic aggregate substitution
11 Literal PASS with failing underlying event
```

For each require durable evidence of:

```text
FixtureId
BaselineValidationResult = ACCEPTED
CorruptionApplied
PostCorruptionValidationResult = REJECTED
ExpectedRejectionPredicate
ObservedRejectionPredicate
Result = PASS
EvidenceReference
```

Require:

```text
ExpectedCorruptionFixtures = 11
BaselineAccepted = 11
CorruptedExecuted = 11
PostCorruptionRejected = 11
UnexpectedAccepted = 0
Unresolved = 0
```

Merely retaining an `ALL_PASS` aggregate is insufficient.

# R14 — SV, durable derivation/reopen, and invariants

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
ResolvedSVEvidenceCount = 36
RuntimePredicatePassClaims = 0
```

Trace:

```text
raw execution/assertion events
→ 53 named records
→ H/T/F/I aggregates
→ positive/negative manifests
→ three-way equality
→ three C2 summaries
→ SV01-SV36
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Require the validator to reject the inverse anti-pattern:

```text
family aggregate → cloned named PASS records
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

After REOPEN, independently resolve all 53 named-event mappings, all 36 SV references, and all 11 corruption-fixture records from disk.

Require:

```text
W5 RunIds allocated = 0
Governed W5 wrapper invocations = 0
Real external calls = 0
Runtime P01-P20 PASS claims = 0
Authority-introduced tracked modifications = 0
Staged paths = 0
git diff --check = PASS
```

Only the two pre-existing WP04 tracked modifications may remain.

# Classification

Classify R01-R14 individually:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL/NOT_PROVEN blocks W5.

This is read-only reconciliation. Do not repair defects.

# PASS markers

Only if every gate passes:

```text
RELEASE 1.12 WP04 — LUNA C2 EVENT-SPECIFIC EXECUTABLE-EVIDENCE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FRESH TUPLE F815C45F: ACCEPTED
RELEASE 1.12 WP04 — C2 NAMED PROBES 53/53 EVENT-SPECIFIC: ACCEPTED
RELEASE 1.12 WP04 — C2 PROBE-TO-EVENT MAPPINGS 53/53: ACCEPTED
RELEASE 1.12 WP04 — C2 GENERIC AGGREGATE NAMED EVIDENCE 0: ACCEPTED
RELEASE 1.12 WP04 — C2 VALIDATOR SEMANTIC-CORRUPTION FIXTURES 11/11: ACCEPTED
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — C2 NEGATIVE FAIL-CLOSED MATRIX: ACCEPTED
RELEASE 1.12 WP04 — C2 DESCRIPTOR CORRELATION/REPLAY REJECTION: ACCEPTED
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

# Failure handoff

On FAIL/NOT_PROVEN report:

```text
FirstFailingGate
ExactEvidence
DefectOwner
HarnessOnly
ValidatorOnly
HarnessPlusValidator
RunnerChangeRequired
ProductionTrackedChangeRequired
NewLunaDecisionRequired
BoundedTerraRemediationSufficient
```

Do not authorize W5.

# Required result handoff

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
UniqueProbeIdCount
DistinctSemanticRequirementCount
UniqueAssertionKeyCount
UniqueEventIdCount
ProbeToEventMappingCount
MissingProbeEventMappings
AmbiguousProbeEventMappings
GenericAggregateNamedEvidenceCount
FailedNamedEvents
UnresolvedNamedEvents

F05EventEvidence
F10EventEvidence
F15EventEvidence
HNEG06ReplayEvidence

H01-H08Aggregate
T01-T18Aggregate
F01-F18Aggregate
I01-I09Aggregate

ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionSurfaceInventory
ExecutedApprovedProbeSurfaceInventory
ThreeWaySurfaceEqualityResult

GitNegativeMatrixResults
AzureNegativeMatrixResults
HelperNegativeMatrixResults
NegativeRealExternalEscapeCount

CorruptionFixtureCount
CorruptionFixtureBaselineAcceptedCount
CorruptionFixtureExecutedCount
CorruptionFixtureRejectedCount
CorruptionFixtureUnexpectedAcceptedCount
CorruptionFixtureResults

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
GitDiffCheckResult
ExactMutationAccounting

R01-R14Summary
FinalReconciliationResult
FirstFailingOrNotProvenPredicate
DefectClassification
NextAuthorizedAction
```

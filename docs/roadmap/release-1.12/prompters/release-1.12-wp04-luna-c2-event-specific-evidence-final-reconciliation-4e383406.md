# Release 1.12 WP04 — Luna C2 Event-Specific Executable-Evidence Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform the independent, read-only final reconciliation of the fresh C2 event-specific evidence tuple below.

This authority is bound exclusively to this tuple/root. Earlier or later roots contribute no acceptance credit.

Do not mutate the frozen runner, harness, validator, child probe, tracked/production source, repository state, Git/GitHub, Azure, Docker/GHCR, Twelve Data, or any external system.

Do not allocate a W5 RunId. Do not invoke the governed W5 wrapper.

## Exact bound tuple

```text
DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\4e3834066fee4c37ae6d368fa2b83cce

RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
F9B0E4EF36C66C74C78BDDDBC08523E87C2C0B5EF28C80CEE5684FBA3E176778

ValidatorSHA256 =
695A278066B178FD2E6347A5780065B7B4BCCDB13ED67CF2426860C036925A5B

ChildProbeSHA256 =
CBE8527B2372AFCF33D978665C7D240C4BE5548982AAEDE152E4028077258B12
```

## R01 — Tuple identity and root isolation

Recompute all four hashes and require exact equality.

Require every acceptance-relevant artifact to belong to the exact durable root.

Reject evidence composition from any superseded/incomplete root.

## R02 — Exact PowerShell/parser proof

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
child parser errors = 0
```

## R03 — Named topology and physical evidence

Require exactly:

```text
H01-H08 = 8
T01-T18 = 18
F01-F18 = 18
I01-I09 = 9
Total = 53
```

Require:

```text
53 named records
53 unique ProbeIds
53 physical named-evidence files
53 event mappings
0 missing
0 duplicate
0 unexpected
0 unresolved
```

## R04 — Event-specific semantic individuality

This is the critical regression gate.

For every named record require an exact resolved event with:

```text
EventId
ProbeId
AssertionKey
Contract/Requirement
Expected
Observed
Result
EvidenceSource/Reference
ValidationMethod
```

Independently require:

```text
UniqueProbeIdCount = 53
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

Reject unique IDs that merely wrap duplicated generic observations.

Forbidden as individual named evidence:

```text
approved=True;threeWay=True
allPass=True
familyPassed=True
surfaceComplete=True
count-only aggregate
literal/synthetic PASS
```

A shared raw artifact is acceptable only if the named record resolves to a distinct contract-specific event/assertion inside it.

## R05 — Mandatory event-specific spot checks

Inspect at least these records directly.

### F05

Must resolve to event-specific evidence identifying all four approved Git interception executions and their outcomes.

### F10

Must resolve to the exact Deferred descriptor-generation/capture event.

Verify the retained observation proves:
- non-empty descriptor;
- sanitized/opaque handling;
- correlation to the generated synthetic identity;
- exact event identity.

A representation such as:

```text
length=66; matchesGenerated=True
```

is acceptable only when traceable to the actual descriptor-generation assertion/event and not merely copied text.

### F15

Must resolve to the actual helper-negative event set, with the ten expected helper-negative IDs/outcomes or an explicitly justified binding-level N/A where allowed.

### HNEG06

Must prove:
- first exact RestoreOnly accepted;
- descriptor consumed;
- second use of same descriptor rejected;
- replay rejection reason retained;
- workspace-helper escape false;
- real external-call delta zero.

Any failed spot check = R05 FAIL.

## R06 — H01-H08

Verify all eight records are individually event-derived and semantically match their contracts.

Require H01-H08 = ALL_PASS.

## R07 — T01-T18 / top-level runtime reachability

Verify every T record resolves to its own control-flow/assertion event.

Require evidence-derived proof of:
- canonical entrypoint;
- shared orchestration;
- ordered pre-RunId gates;
- G01-G11;
- interception installation;
- SandboxRoot;
- three-way equality gate;
- positive/negative probe reachability;
- mandatory dominance over all RunId sites;
- wrapper invocation dominance;
- no bypass path;
- structural stop before RunId allocation;
- RunId=0, wrapper=0, real external=0.

Require:

```text
T01-T18 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
```

## R08 — Independent production external-surface derivation

Independently derive the complete production reachable external-process surface.

At minimum:

```text
Git x4:
rev-parse HEAD
merge-base --is-ancestor ...
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

If source inspection reveals additional reachable shapes, those are mandatory.

## R09 — Positive surface and three-way equality

Require normalized inventories:

```text
ProductionReachableExternalSurface
HarnessGovernedInterceptionSurface
ExecutedApprovedProbeSurface
```

Require semantic equality, not count equality:

```text
Production == Harness == Executed
```

Every approved shape must have its own executed event.

## R10 — F01-F18

Verify all F records individually.

Require event-specific evidence for:
- F01/F02/F03 each inventory;
- F04 actual equality comparison;
- F05 Git x4 executions;
- F06 Azure show x2;
- F07 appsettings list;
- F08 log download/path constraint;
- F09 Deferred invocation;
- F10 descriptor generation/correlation;
- F11 first RestoreOnly;
- F12 descriptor consumption/replay state transition;
- F13 Git negative set;
- F14 Azure negative set;
- F15 helper negative set;
- F16 child/finally sandbox helper resolution;
- F17 no wildcard/omitted surface;
- F18 cleanup/location restoration and durable ledger.

Require:

```text
F01-F18 = ALL_PASS
C2FullReachableExternalSurface = PASS
```

## R11 — Complete negative matrices

Verify the negative matrix contains actual event-specific mismatch/rejection evidence, not only aggregate PASS.

At minimum require:

```text
Git:
unsupported subcommand
extra argument
altered ls-files wrapper path
altered diff wrapper path
altered merge-base identity

Azure:
unsupported operation
extra argument
altered RG/app across approved shapes
outside-root log path
traversal log path
alternate-root log path

Helper:
unsupported mode/shape
missing descriptor
empty descriptor
altered descriptor
fabricated descriptor
replayed descriptor
cross-attempt descriptor
mismatched RG
mismatched app
unsupported extra shape or evidence-derived NOT_APPLICABLE_BY_BINDING
```

Require every applicable case to prove:

```text
rejected = true
real external escape = false
real external call delta = 0
event retained = true
```

## R12 — I01-I09 child/copy-equivalent interception

Require event-specific child evidence for:

```text
I01 approved Git
I02 approved Azure
I03 approved helper
I04 unexpected Git rejected
I05 unexpected Azure rejected
I06 unexpected helper rejected
I07 real git/az/workspace-helper escape blocked
I08 durable complete child interception ledger
I09 cleanup/location restoration
```

Require:

```text
I01-I09 = ALL_PASS
C2ExecutableInterceptionBoundary = PASS
```

## R13 — Validator semantic-corruption defenses

Independently inspect retained validator self-probe evidence.

Require rejection of invalid fixtures/copies covering at least:

```text
missing event
duplicate EventId
ProbeId/EventId mismatch
AssertionKey mismatch
EvidenceReference to wrong event
F05 generic aggregate substitution
F10 generic aggregate substitution
F15 generic aggregate substitution
T-family generic aggregate substitution
I-family generic aggregate substitution
literal PASS with missing/failing underlying event
```

Require every corruption probe to be actually executed and rejected.

The validator must reject semantic duplication even if IDs are unique.

## R14 — SV, derivation, reopen, runtime/repository invariants

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
ResolvedSVEvidenceCount = 36
RuntimePredicatePassClaims = 0
```

Trace the enforced derivation direction:

```text
raw execution/assertion events
→ 53 individual named records
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

Prove the inverse anti-pattern is rejected:

```text
family aggregate → cloned named PASS records
```

Require checkpoints exactly once and in order:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After REOPEN independently resolve all 53 event mappings and all 36 SV references from disk.

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

Only the two pre-existing tracked WP04 changes may remain.

# Classification

Classify each R01-R14:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

Do not repair anything under this Luna authority.

# PASS markers

Only if all material gates pass:

```text
RELEASE 1.12 WP04 — LUNA C2 EVENT-SPECIFIC EXECUTABLE-EVIDENCE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 EXACT EVENT-SPECIFIC TUPLE: ACCEPTED
RELEASE 1.12 WP04 — C2 NAMED PROBES 53/53 EVENT-SPECIFIC: ACCEPTED
RELEASE 1.12 WP04 — C2 PROBE-TO-EVENT MAPPINGS 53/53: ACCEPTED
RELEASE 1.12 WP04 — C2 GENERIC AGGREGATE NAMED EVIDENCE 0: ACCEPTED
RELEASE 1.12 WP04 — C2 VALIDATOR SEMANTIC-CORRUPTION PROBES: ACCEPTED
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

Then STOP.

A separate **GPT-5.6 Terra** W5 governed-runtime authority is required before any W5 RunId allocation or wrapper execution.

# Failure handoff

On FAIL/NOT_PROVEN return:

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

ValidatorSemanticCorruptionProbeResults

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

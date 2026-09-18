# Release 1.12 WP04 — Terra C2 Event-Specific Named-Evidence Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Binding diagnosis

Latest Luna reconciliation is binding:

```text
R01-R03 = PASS
R04 = FAIL — semantic individuality
First failing gate = R04
Defect owner = HARNESS + VALIDATOR
W5 execution = NOT_AUTHORIZED
W5 RunId allocation = NOT_AUTHORIZED
```

Rejected tuple:

```text
Runner =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Harness =
51D222163C2F2F92DF2F887B6D793F190084ABFC7D6E55F6E61CE6CC419BCA26

Validator =
B5F270A95FB19A1A1F999C2155854860FCF45F07147D9F4C353A63BCC69F82A1

Child =
2A63D4DE384FAFFBBD5EDB1555053D91D57A4F2FF897D3BFF98548AEC7317DEF

Root =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\ee8da7b665384172bf34c6c151b3e16d
```

Preserve this root as rejected evidence. It grants no W5 acceptance credit.

## Mission

Remediate only the disposable harness + validator evidence architecture so each acceptance-relevant named H/T/F/I contract references its **own exact executed/asserted event**, rather than a family aggregate.

The current defect is not missing labels, IDs, AssertionKeys, or physical files. Those are insufficient.

The invalid pattern is:

```text
F05 -> approved=True;threeWay=True
F10 -> approved=True;threeWay=True
F15 -> approved=True;threeWay=True
...
```

even when each record has a distinct Requirement or AssertionKey.

The required pattern is:

```text
named contract
→ unique assertion/event identity
→ contract-specific observed values
→ exact retained event
→ evidence-derived Result
```

## Frozen boundaries

MUST NOT change:

```text
frozen runner
tracked/production source
production wrapper/helper
Git/GitHub state
Azure
Docker/GHCR
Twelve Data configuration
WP04 architecture/policy
```

MUST NOT:

```text
allocate a W5 RunId
invoke governed W5 wrapper
make a real external call
stage/commit/push
run W6/W7/W8
perform publication/lifecycle mutation
```

The frozen runner hash must remain:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

## 1. Event-specific evidence rule

For every named probe H01-H08, T01-T18, F01-F18, I01-I09, retain an event/assertion object containing at least:

```text
EventId
ProbeId
AssertionKey
Contract
Expected
Observed
Result
EvidenceSource
```

`EventId` must uniquely identify the exact assertion execution.

Require:

```text
NamedProbeCount = 53
UniqueProbeIdCount = 53
UniqueAssertionKeyCount = 53
UniqueEventIdCount = 53
ProbeToEventMappingCount = 53
MissingProbeEventMappings = 0
AmbiguousProbeEventMappings = 0
FailedNamedEvents = 0
UnresolvedNamedEvents = 0
```

An event may reference a common raw artifact, but `Observed` must contain the contract-specific fact(s) extracted/asserted for that ProbeId.

Generic aggregate observations such as these are forbidden as named evidence:

```text
approved=True;threeWay=True
allPass=True
count=18
familyPassed=True
surfaceComplete=True
```

They may exist only as downstream aggregate summaries.

## 2. F-family must become event-specific

At minimum require these distinct observations:

```text
F01 Observed = normalized production inventory itself or exact identity/hash + member set
F02 Observed = normalized interception inventory itself or exact identity/hash + member set
F03 Observed = normalized executed-approved inventory itself or exact identity/hash + member set
F04 Observed = explicit equality comparison of the three normalized sets

F05 Observed = four individually identified Git approved execution events and their outcomes
F06 Observed = two individually identified az webapp-show execution events and outcomes
F07 Observed = exact appsettings-list execution event and matched normalized contract
F08 Observed = exact log-download execution event + governed evidence-root/path validation
F09 Observed = exact Deferred invocation event + normalized argument contract
F10 Observed = exact descriptor-generation event + non-empty/sanitized/opaque/correlation fields
F11 Observed = exact first RestoreOnly invocation + descriptor identity + accepted outcome
F12 Observed = first-use/consumption/replay state transition evidence
F13 Observed = Git negative event IDs + per-case rejected/no-escape outcomes
F14 Observed = Azure negative event IDs + per-case rejected/no-escape outcomes
F15 Observed = helper negative event IDs + per-case rejected/no-escape outcomes
F16 Observed = exact child/finally helper-resolution event + workspace-escape assertion
F17 Observed = exact wildcard/omission comparison against independently derived production inventory
F18 Observed = exact cleanup/location restoration events + durable call-ledger identity
```

F05, F10, and F15 are mandatory spot checks because Luna specifically found them invalid.

## 3. T-family must become event-specific

Each T01-T18 record must reference its own control-flow/runtime-structure assertion event.

Examples:

```text
T03 -> event showing identity/hash gates and exact ordering
T08 -> event showing the three-way equality gate's location/reachability
T12 -> event showing interception PASS gate dominates every RunId allocation site
T14 -> event enumerating every RunId allocation site and proving mandatory dominators
T16 -> event proving no alternate/bypass top-level path
T17 -> event proving structural execution reaches stop immediately before RunId allocation
T18 -> event with observed counters: RunId=0, wrapper=0, real external=0
```

Do not satisfy multiple T records with one generic reachability aggregate.

## 4. I-family must become event-specific

Each I01-I09 must reference its exact child/copy-equivalent execution/assertion event.

At minimum:

```text
I01 exact approved Git child interception event
I02 exact approved Azure child interception event
I03 exact approved helper child interception event
I04 exact unexpected Git rejection event
I05 exact unexpected Azure rejection event
I06 exact unexpected helper rejection event
I07 exact escape-attempt/block assertion(s)
I08 exact child interception ledger completeness assertion
I09 exact cleanup/location-restoration assertion
```

## 5. H-family

Apply the same event-specific rule to H01-H08. Do not assume prior H records are exempt merely because Luna's first concrete examples were F/T/I.

## 6. Preserve and bind the improved negative matrix

The prior run retained 25 negative records with real normalized mismatch messages. Preserve that semantic quality.

Every negative record must remain individually executable/evidence-derived.

The named F13/F14/F15 records must now reference the actual constituent negative event IDs and summarize their exact outcomes, rather than a generic family PASS.

Descriptor replay must retain event-specific evidence including:

```text
first exact RestoreOnly accepted
descriptor consumed
same descriptor second use rejected
normalized replay rejection reason
workspace-helper escape = false
real external-call delta = 0
```

## 7. Validator requirements

The validator must independently reject a structurally complete but semantically invalid ledger.

Add/retain fail-closed validation proving:

```text
53 unique ProbeIds
53 unique AssertionKeys
53 unique EventIds
53 exact ProbeId -> EventId mappings
every EvidenceReference resolves
resolved event ProbeId == named record ProbeId
resolved event AssertionKey == named record AssertionKey
resolved event Contract == named Requirement/contract mapping
Observed is contract-specific
Result derives from the resolved event
no forbidden generic aggregate observation is used as individual named evidence
```

Do not rely only on uniqueness. A unique EventId wrapping duplicated aggregate content must fail.

## 8. Executable validator self-probes

Before candidate acceptance, exercise validator negatives using disposable copies/fixtures. At minimum prove rejection of:

```text
one missing event
duplicate EventId
ProbeId/EventId mismatch
AssertionKey mismatch
EvidenceReference to wrong event
F05 replaced by generic approved=True;threeWay=True
F10 replaced by generic approved=True;threeWay=True
F15 replaced by generic approved=True;threeWay=True
T record replaced by generic family aggregate
I record replaced by generic family aggregate
literal Result=PASS with failing/missing underlying event
```

These are harness/validator tests only and must not mutate production/tracked source.

## 9. Aggregate derivation direction

Enforce only this direction:

```text
raw executable/assertion events
→ individual named records
→ family aggregates
→ positive/negative manifests
→ C2 summaries
→ SV01-SV36
→ final structural ledger
```

Forbidden:

```text
family aggregate
→ cloned named PASS records
```

## 10. SV dependency

SV01-SV36 remains exactly 36 records.

A 36/0 result is acceptable only if SV acceptance fails whenever any individual event-specific mapping fails.

Explicitly test at least one disposable corruption where SV would previously have stayed green despite a generic F row; validator/final acceptance must now fail.

## 11. Fresh tuple/root after every correction

Any byte change to harness, validator, or child invalidates prior candidate hashes.

For every correction:

```text
re-hash changed artifacts
create a fresh durable root
restart complete structural cycle from the beginning
rebuild all event-specific evidence
serialize
publish
reopen
revalidate
```

Never patch a finalized root in place.

## 12. Retry-until-PASS

This authority explicitly authorizes bounded iterative remediation:

```text
diagnose
→ correct harness/validator/child as needed
→ re-hash
→ create fresh root
→ rerun complete structural cycle
→ validate
→ if harness/validator-only defect remains, repeat
```

Continue until PASS rather than stopping after each locally correctable defect.

STOP immediately if a required correction crosses a frozen/governance boundary.

## 13. Full final structural gates

Final fresh candidate must prove all of the following again, not carry them forward by assertion:

```text
exact WinPS 5.1.26100.9444
parser 0/0/0/0
53 named records
53 physical named-evidence files
53 semantic contracts
53 unique assertion keys
53 unique event IDs
53 resolved one-to-one event mappings
H01-H08 ALL_PASS
T01-T18 ALL_PASS
F01-F18 ALL_PASS
I01-I09 ALL_PASS
positive approved-shape manifest complete
negative fail-closed matrix complete
descriptor correlation/replay rejection PASS
three-way external-surface equality PASS
C2TopLevelRuntimeReachability PASS
C2FullReachableExternalSurface PASS
C2ExecutableInterceptionBoundary PASS
SV01-SV36 = 36/0
SV evidence references = 36/36 resolved
runtime P01-P20 PASS claims = 0
W5 RunIds allocated = 0
governed W5 wrapper invocations = 0
real external calls = 0
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check = PASS
```

Checkpoint order/count exactly:

```text
PRE_CLEANUP = 1
CLEANUP_COMPLETE = 1
BUILD_LEDGER = 1
SERIALIZE = 1
PUBLISH = 1
REOPEN = 1
```

Reopen must independently re-resolve all 53 event mappings from disk.

## PASS markers

Only after a fresh complete cycle passes:

```text
RELEASE 1.12 WP04 — TERRA C2 EVENT-SPECIFIC NAMED-EVIDENCE REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 NAMED PROBES: 53/53 EVENT-SPECIFIC
RELEASE 1.12 WP04 — C2 PROBE-TO-EVENT MAPPINGS: 53/53 RESOLVED
RELEASE 1.12 WP04 — C2 GENERIC AGGREGATE NAMED EVIDENCE: 0
RELEASE 1.12 WP04 — C2 VALIDATOR SEMANTIC-CORRUPTION PROBES: ALL_PASS
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ALL_PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 NEGATIVE FAIL-CLOSED MATRIX: COMPLETE
RELEASE 1.12 WP04 — C2 DESCRIPTOR CORRELATION/REPLAY REJECTION: PASS
RELEASE 1.12 WP04 — C2 THREE-WAY EXTERNAL-SURFACE EQUALITY: PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: PASS
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 EVENT-SPECIFIC EVIDENCE RECONCILIATION: YES
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. Terra PASS does not authorize W5.

## Required handoff

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

F05EventId
F05Observed
F10EventId
F10Observed
F15EventId
F15Observed
HNEG06EventId
HNEG06Observed

ValidatorSemanticCorruptionProbeResults

H01-H08Aggregate
T01-T18Aggregate
F01-F18Aggregate
I01-I09Aggregate

PositiveApprovedShapeManifestResult
NegativeMatrixRecordCount
NegativeMatrixResult
DescriptorReplayResult

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

## Next gate

After PASS:

```text
GPT-5.6 Luna — C2 Event-Specific Executable-Evidence Final Reconciliation
```

Only that separate Luna reconciliation may authorize a fresh W5 governed runtime.

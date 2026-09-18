# Release 1.12 WP04 — Terra C2 Explicit Probe-Record Retention Continuation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue the existing **C2 Executable Evidence-Derived Probes Remediation — Retry Until PASS**.

The latest candidate is **NOT_ACCEPTED** because, despite `SV_COUNT=36` and `SV_FAILED=0`, the validator does not retain every required named T/F/I/H probe as an individually visible evidence-derived record.

This is an in-scope disposable validator/harness evidence-model defect. Correct it, re-hash every changed artifact, create a fresh durable root, and restart the complete structural cycle from zero.

Do not request another Luna decision unless correction crosses the mandatory governance boundary below.

## Latest incomplete tuple — superseded for acceptance

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
E24A04AE1FDCE7CD26B8B4C37A3E7C4CE11793BF7A52899CE219299AF3293A41

ValidatorSHA256 =
6017E28E606B6CBF8D054C158E8448750B68D0245772F64184490B232CB9A066

ChildProbeSHA256 =
A010BF7B2638F2B4698E6CFDBBE26ECD449172D4636F9C4B861861CE425B3387

SupersededDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\a51f2554ca924657a0dc5116a064d328
```

Observed diagnostic state:

```text
SV_COUNT = 36
SV_FAILED = 0
C2 acceptance = NOT_ACCEPTED
W5 RunIds allocated = 0
W5 wrapper invocations = 0
real external service calls = 0
```

Preserve this root as incomplete/superseded evidence. Do not compose final acceptance from it.

# Core correction

Preserve the already executed evidence semantics, but make **every required named probe an explicit durable record**.

Required named probe families:

```text
H01-H08  = 8 records
T01-T18  = 18 records
F01-F18  = 18 records
I01-I09  = 9 records
```

Total named probe records:

```text
53
```

These 53 records are distinct from the canonical 36 SV records.

Do **not** force them into the 36-SV count by dropping or merging named probes.

# Probe record schema

Every H/T/F/I record must be individually retained with at least exactly these semantic fields:

```text
ProbeId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Additional metadata may be retained only if needed for auditability, but the seven fields above must always be present and non-empty except where a deliberately empty observed value is itself the tested condition.

Require:

```text
ProbeId unique
Result derived from Expected vs Observed
EvidenceReference resolvable
ValidationMethod identifies executable/assertion/AST evidence used
```

No probe `Result=PASS` may be assigned independently of its observed assertion.

# H01-H08 retention

Retain exactly eight individually visible records:

```text
H01
H02
H03
H04
H05
H06
H07
H08
```

Require:

```text
HRecordCount = 8
UniqueHProbeIdCount = 8
HFailedCount = 0
HResolvedEvidenceCount = 8
```

The aggregate:

```text
H01-H08 = ALL_PASS
```

must be computed from those eight records.

# T01-T18 retention

Retain exactly eighteen individually visible records:

```text
T01 ... T18
```

Require:

```text
TRecordCount = 18
UniqueTProbeIdCount = 18
TFailedCount = 0
TResolvedEvidenceCount = 18
```

The aggregate:

```text
T01-T18 = ALL_PASS
```

must be computed from those eighteen records.

# F01-F18 retention

Retain exactly eighteen individually visible records:

```text
F01 ... F18
```

Require:

```text
FRecordCount = 18
UniqueFProbeIdCount = 18
FFailedCount = 0
FResolvedEvidenceCount = 18
```

The aggregate:

```text
F01-F18 = ALL_PASS
```

must be computed from those eighteen records.

No subset aggregation is sufficient.

# I01-I09 retention

Retain exactly nine individually visible records:

```text
I01 ... I09
```

Require:

```text
IRecordCount = 9
UniqueIProbeIdCount = 9
IFailedCount = 0
IResolvedEvidenceCount = 9
```

The aggregate:

```text
I01-I09 = ALL_PASS
```

must be computed from those nine records.

No subset aggregation is sufficient.

# Canonical named-probe manifest

Create a durable manifest containing the exact expected IDs:

```text
H01 H02 H03 H04 H05 H06 H07 H08

T01 T02 T03 T04 T05 T06 T07 T08 T09
T10 T11 T12 T13 T14 T15 T16 T17 T18

F01 F02 F03 F04 F05 F06 F07 F08 F09
F10 F11 F12 F13 F14 F15 F16 F17 F18

I01 I02 I03 I04 I05 I06 I07 I08 I09
```

The validator must compare:

```text
ExpectedNamedProbeIds
==
ObservedNamedProbeIds
```

Require:

```text
ExpectedNamedProbeCount = 53
ObservedNamedProbeCount = 53
UniqueNamedProbeIdCount = 53
MissingNamedProbeCount = 0
UnexpectedNamedProbeCount = 0
DuplicateNamedProbeCount = 0
NamedProbeFailedCount = 0
NamedProbeUnresolvedEvidenceCount = 0
```

Any mismatch = FAIL.

# Preserve executable derivation

Do not solve this by serializing synthetic PASS rows after the fact.

Each retained record must point to the actual executable/assertion-derived evidence already required by the parent authority, including:

```text
exact Windows PowerShell parser/AST proof
child/copy-equivalent artifact and execution
actual git interception probes
actual az interception probes
actual Deferred/RestoreOnly helper probes
negative fail-closed probes
external-surface inventory/equality
shared top-level dispatch/control flow
RunId dominance
wrapper dominance
structural stop
escape prevention
cleanup/location restoration
```

If the existing harness does not expose enough raw evidence to construct a required record truthfully, correct the disposable harness within this authority and rerun.

# Three C2 summaries

Compute only from explicit retained records plus required direct structural invariants:

```text
C2TopLevelRuntimeReachability
← T01-T18 + required shared-path/dominance evidence

C2FullReachableExternalSurface
← F01-F18 + three-way inventory equality

C2ExecutableInterceptionBoundary
← I01-I09 + H01-H08 + child-scope/escape evidence
```

No literal/optimistic PASS assignment.

# SV01-SV36 remains separate

Retain the canonical SV ledger as exactly 36 records.

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

The SV records must explicitly validate the named-probe manifests/aggregates and make them acceptance-relevant.

At minimum, SV acceptance must fail if any of the following fail:

```text
H named-record completeness
T named-record completeness
F named-record completeness
I named-record completeness
53/53 named-probe manifest equality
named-probe evidence resolution
H aggregate
T aggregate
F aggregate
I aggregate
three C2 summaries
final ledger consistency
```

A `36/0` SV ledger is insufficient if the 53 named records are incomplete.

# Durable artifacts

The final durable root must retain, at minimum:

```text
runner copy/hash
harness copy/hash
validator copy/hash
child probe copy/hash
named-probe-manifest.json
h01-h08-records.json
t01-t18-records.json
f01-f18-records.json
i01-i09-records.json
named-probe-evidence-resolution.json
production external-surface inventory
harness interception inventory
executed approved-probe inventory
three-way equality evidence
SV01-SV36 ledger
SV evidence manifest/resolution
final structural summary
retry history
repository invariants
```

Equivalent filenames are acceptable if clearly identified in the handoff.

# Reopen/round-trip checks

After final publication/reopen, independently require:

```text
HRecordCount = 8
TRecordCount = 18
FRecordCount = 18
IRecordCount = 9
NamedProbeRecordCount = 53
UniqueNamedProbeIdCount = 53
MissingNamedProbeCount = 0
UnexpectedNamedProbeCount = 0
DuplicateNamedProbeCount = 0
NamedProbeFailedCount = 0
NamedProbeUnresolvedEvidenceCount = 0

H01-H08 = ALL_PASS
T01-T18 = ALL_PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS

C2TopLevelRuntimeReachability = PASS
C2FullReachableExternalSurface = PASS
C2ExecutableInterceptionBoundary = PASS
```

The reopened durable artifacts, not merely in-memory state, are authoritative for this gate.

# Fresh tuple requirement

Any validator byte change requires:

```text
new ValidatorSHA256
fresh durable root
complete structural restart
```

If harness or child probe bytes change, also require new hashes for them.

The frozen runner hash must remain exact.

Do not reuse the `a51f2554...` root for final acceptance.

# Full restart

Rerun the entire parent authority from zero, including:

```text
WinPS 5.1 parser gates
parent/child containment
complete production external inventory
complete harness interception inventory
complete executed approved-probe inventory
three-way equality
all positive call-shape probes
all negative fail-closed probes
H01-H08
T01-T18
F01-F18
I01-I09
all three C2 summaries
SV01-SV36
all evidence resolution
finalization
serialization/publication/reopen
repository/runtime invariants
```

# Retry-until-PASS

For every defect correctable wholly within disposable harness/validator scope:

```text
diagnose
→ correct
→ recompute changed hashes
→ preserve/supersede prior root
→ create fresh durable root
→ restart complete structural cycle
→ continue until PASS
```

Do not stop after another local retention, mapping, serialization, aggregation, or probe defect.

## Mandatory stop boundary

STOP only if correction requires:

```text
frozen runner change
production/tracked-source change
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

# Runtime/repository invariants

Throughout:

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

# Finalization

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

# PASS markers

Only after one fresh tuple retains and resolves all 53 named records and all other parent-authority gates:

```text
RELEASE 1.12 WP04 — TERRA C2 EXPLICIT PROBE-RECORD RETENTION: PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE MANIFEST: 53/53 COMPLETE
RELEASE 1.12 WP04 — C2 HERE-STRING PROBES H01-H08: ALL_PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 NAMED PROBE EVIDENCE: 53/53 RESOLVED
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 SV EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: PASS
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 EXECUTABLE-EVIDENCE FINAL RECONCILIATION: YES
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
ParserErrorCountsRunnerHarnessValidatorChild
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot

HRecordCount
UniqueHProbeIdCount
HFailedCount
HResolvedEvidenceCount
H01-H08Aggregate
HRecordsPath

TRecordCount
UniqueTProbeIdCount
TFailedCount
TResolvedEvidenceCount
T01-T18Aggregate
TRecordsPath

FRecordCount
UniqueFProbeIdCount
FFailedCount
FResolvedEvidenceCount
F01-F18Aggregate
FRecordsPath

IRecordCount
UniqueIProbeIdCount
IFailedCount
IResolvedEvidenceCount
I01-I09Aggregate
IRecordsPath

ExpectedNamedProbeCount
ObservedNamedProbeCount
UniqueNamedProbeIdCount
MissingNamedProbeCount
UnexpectedNamedProbeCount
DuplicateNamedProbeCount
NamedProbeFailedCount
NamedProbeUnresolvedEvidenceCount
NamedProbeManifestPath
NamedProbeEvidenceResolutionPath

ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionSurfaceInventory
ExecutedApprovedProbeSurfaceInventory
ThreeWaySurfaceEqualityResult

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
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
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

A Terra PASS still does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Executable Evidence Final Reconciliation
```

Only that Luna PASS may authorize a fresh governed W5 runtime and a new W5 RunId.

# Release 1.12 WP04 — Terra C2 Executable Evidence-Derived Probes Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate the Luna-confirmed C2 structural-evidence defect entirely within the disposable/local harness + validator boundary.

The prior tuple is rejected for W5 because its acceptance predicates were hard-coded rather than derived from executed assertions, its pre-RunId probe covered only a subset of the production wrapper's reachable external-call surface, and it did not retain a distinct generated child/copy-equivalent probe artifact.

Rewrite/correct the harness and validator so every acceptance predicate is evidence-derived from executable probes, the complete reachable external surface is exercised, and child/copy-equivalent interception evidence is independently retained and validated.

Then re-hash all changed artifacts, create a fresh durable root, and restart the entire structural cycle from zero.

Within harness/validator-only scope, diagnose, fix, re-hash, restart, and keep trying until PASS. Do not stop after each locally correctable defect.

## Binding Luna result

```text
R01 tuple identity/binding = PASS
R02 Windows PowerShell-specific parser proof = NOT_PROVEN
R03 here-string / parent ownership = NOT_PROVEN
R04-R08 dispatch/shared path/dominance/structural stop = NOT_PROVEN
R09 T01-T18 = FAIL
R10 full external surface = FAIL
R11 executable interception boundary = FAIL
R12 SV01-SV36 acceptance relevance = FAIL
R13 joint aggregate / durable round trip = FAIL
R14 finalization artifacts = PASS as artifacts only
```

Binding markers:

```text
RELEASE 1.12 WP04 — LUNA C2 TOP-LEVEL RUNTIME REACHABILITY FINAL RECONCILIATION: BLOCKED
RELEASE 1.12 WP04 — C2 GOVERNED STRUCTURAL EVIDENCE: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
```

## Superseded tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

RejectedHarnessSHA256 =
838659E521EFADBC099A945B10A73882DE7DD9281176BDDDC3FCF3D3BA8EFFFF

RejectedValidatorSHA256 =
C5BFF19D7A4B88EFBB13C8AC6FC4D77A7B71E629D987C704CD5B27638030C7AB

RejectedDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\c62ea146dfb1405eb973d28e7a8f8256
```

Preserve the rejected root unchanged. It has no W5 authorization value.

## Frozen/production boundaries

Frozen runner remains immutable:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Do not change the production wrapper or either tracked WP04 script.

No staging, commit, push, GitHub, Azure, Docker/GHCR, Twelve Data, or other real external mutation/call is authorized.

# A — No literal acceptance PASS assignments

Acceptance predicates MUST be computed from individual assertion/probe records.

Forbidden pattern:

```powershell
$T01 = 'PASS'
$F01 = 'PASS'
$I01 = 'PASS'
$C2TopLevelRuntimeReachability = 'PASS'
```

or any semantic equivalent that assigns PASS independently of observed evidence.

Each probe record must minimally carry:

```text
ProbeId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

`Result` must be computed from the assertion's actual observed value versus expected value.

Aggregates must be computed from member records:

```text
T01-T18 aggregate ← T01..T18 results
F01-F18 aggregate ← F01..F18 results
I01-I09 aggregate ← I01..I09 results
```

C2 summaries must be computed from the relevant aggregates/assertions, never assigned optimistically.

Any missing/unresolved member must fail closed.

# B — Complete production reachable-external-call inventory

Independently derive the exact reachable external-call inventory from the production wrapper.

At minimum reconcile and exercise all currently identified shapes:

```text
git:
1. rev-parse HEAD
2. merge-base --is-ancestor <expected-source> HEAD
3. ls-files --error-unmatch -- <wrapper-path>
4. diff --quiet HEAD -- <wrapper-path>

az:
1. webapp show — image query shape
2. webapp show — state query shape
3. webapp config appsettings list
4. webapp log download --log-file <exact evidence-root>\raw-app-service-logs.zip

helper:
1. Deferred:
   -ResourceGroup <rg>
   -WebAppName <app>
   -Phase initialize
   -LifecycleAction None
   -RestorationMode Deferred

2. RestoreOnly:
   -ResourceGroup <rg>
   -WebAppName <app>
   -RestorationMode RestoreOnly
   -RestorationDescriptor <exact preceding descriptor>
```

If source inspection finds any additional reachable external executable/script call, add it to both the production inventory and governed interception/probe inventory before acceptance.

Require exact inventory equality:

```text
ProductionReachableExternalSurface
==
HarnessGovernedInterceptionSurface
==
ExecutedApprovedProbeSurface
```

A subset is FAIL.

# C — Execute every approved call shape

The structural probe must actually invoke every governed approved call shape through the interception layer.

For each shape retain evidence of:

```text
normalized command identity
normalized ordered argument vector
expected dynamic values
observed dynamic values
interceptor selected
real external process not selected
synthetic result returned
ledger record emitted
probe result
```

Do not infer coverage merely because an allowlist entry exists.

# D — Negative probes

For each external kind, execute negative cases proving fail-closed behavior.

At minimum:

```text
git:
- unsupported subcommand
- approved command with extra argument
- approved command with altered governed path/value

az:
- unsupported operation
- approved operation with extra argument
- altered resource group/app
- arbitrary --log-file path
- traversal/alternate-root log path

helper:
- unsupported mode
- missing descriptor for RestoreOnly
- altered/arbitrary descriptor
- replayed descriptor
- mismatched RG/app
- extra unsupported argument if applicable
```

Negative probes must prove the real command/helper is not reached.

# E — Distinct child/copy-equivalent probe artifact

Generate a distinct disposable child-probe `.ps1` artifact.

It must be retained by hash/copy in the durable evidence root before disposable cleanup.

Require:

```text
ChildProbePath
ChildProbeSHA256
DurableChildProbePath
DurableChildProbeSHA256
byte equality = PASS
```

The child probe must execute from:

```text
SandboxRoot
```

and reproduce the copied wrapper's relevant command-resolution scope.

It must prove from that child process/script scope:

```text
git shim visible and selected
az shim visible and selected
relative helper resolves to sandbox shadow helper
workspace helper not selected
approved child-scope calls succeed through interceptions
unexpected child-scope calls fail closed
working location restored
```

Direct parent-harness invocation is not a substitute.

# F — Here-string containment proof

If a here-string is used to construct the child probe, retain evidence proving:

```text
H01 delimiters balanced
H02 parent harness parses in exact Windows PowerShell 5.1
H03 generated child probe parses in exact Windows PowerShell 5.1
H04 shared parent orchestration AST belongs to parent script, outside expandable/literal string AST extents
H05 top-level dispatch AST belongs to parent script
H06 RunId allocation site belongs to parent script
H07 governed wrapper invocation site belongs to parent script
H08 child artifact contains only intended child-scope logic, not parent orchestration
```

Require `H01-H08 = ALL_PASS`.

# G — Exact Windows PowerShell proof

R02 must no longer rely on a generic parser claim.

Run structural validation under:

```text
Windows PowerShell 5.1.26100.9444
```

Record the actual version and parser errors for:

```text
runner
harness
validator
generated child probe
```

Require all parser counts `0`.

# H — Shared top-level orchestration evidence

Retain the clean shared orchestration design, but prove it from executable/AST/control-flow evidence.

Both:

```text
StructuralProbe
ExecuteGovernedW5
```

must enter the same parent-level pre-RunId orchestration.

The structural execution must traverse the actual shared preflight and stop immediately before RunId allocation.

Future W5 execution must be structurally reachable from the same stop/continue point but MUST NOT execute here.

# I — T01-T18 must be evidence-derived

Execute/derive each T probe separately.

Require actual evidence for:

```text
T01 future W5 invocation maps to exact top-level dispatch
T02 dispatch reaches shared orchestration
T03 identity/hash gates occur on shared path
T04 exact-byte wrapper-copy gate occurs on shared path
T05 G01-G11 occur on shared path
T06 interception installation occurs on shared path
T07 SandboxRoot binding occurs on shared path
T08 complete surface equality gate occurs on shared path
T09 Deferred/RestoreOnly probes occur on shared path
T10 exact git/az/helper probes occur on shared path
T11 negative/escape probes occur on shared path
T12 executable-interception PASS dominates RunId allocation
T13 full-surface PASS dominates RunId allocation
T14 all RunId sites dominated by all pre-RunId gates
T15 all wrapper sites dominated by RunId + preflight
T16 no alternate/bypass path
T17 structural execution reaches explicit stop immediately before allocation
T18 structural execution has 0 RunIds / 0 wrappers / 0 real external calls
```

`T01-T18 = ALL_PASS` only if every record's evidence-derived Result is PASS.

# J — F01-F18 must be evidence-derived

Rebuild F01-F18 as individual executable/assertion-derived records.

They must cover:

```text
complete source inventory
complete harness allowlist inventory
executed approved-probe inventory
three-way equality
Deferred exact shape
descriptor generation
descriptor correlation
RestoreOnly exact shape
single-use/replay behavior
negative descriptor cases
finally-path child-scope resolution
workspace helper escape prevention
all four Git shapes
all four Azure shapes
both helper phases
wildcard absence
no omitted reachable external surface
cleanup/location restoration
```

Adjust exact F numbering if necessary while preserving exactly F01-F18 and recording the requirement text for each.

# K — I01-I09 must be evidence-derived

Rebuild I01-I09 from actual child/copy-equivalent interception execution:

```text
I01 approved git interception from child scope
I02 approved az interception from child scope
I03 approved relative helper from child scope
I04 unexpected git fails closed
I05 unexpected az fails closed
I06 unexpected helper fails closed
I07 real external/helper escape blocked
I08 complete durable interception ledger
I09 cleanup + location restoration
```

Require `I01-I09 = ALL_PASS`.

# L — C2 summary derivation

Compute:

```text
C2ExecutableInterceptionBoundary
```

only from the required I/H/escape/child-scope assertions.

Compute:

```text
C2FullReachableExternalSurface
```

only from complete production inventory + harness inventory + executed approved-probe inventory equality and F assertions.

Compute:

```text
C2TopLevelRuntimeReachability
```

only from T assertions plus shared-dispatch/dominance/structural-stop evidence.

No literal PASS assignment is allowed for any C2 summary.

# M — SV01-SV36 acceptance relevance

The validator must not merely count 36 records.

Each SV result must be derived from actual evidence and must bind acceptance-critical properties.

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

Each record fields exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

At minimum the SV contract must make these acceptance-critical:

```text
exact Windows PowerShell/parser proof
parent/child containment
shared top-level dispatch
pre-RunId dominance
wrapper dominance
complete production surface inventory
complete harness interception inventory
executed approved-probe inventory
three-way surface equality
child/copy-equivalent interception
negative fail-closed probes
no real external escape
F01-F18 aggregation
I01-I09 aggregation
T01-T18 aggregation
all three C2 summaries
final ledger consistency
runtime exclusion
repo invariants
```

# N — Durable round trip

All individual probe records, aggregates, inventories, and three C2 summaries must be serialized from actual observed results.

Require:

```text
BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

After reopen independently recompute/check that:

```text
T01-T18 = ALL_PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
H01-H08 = ALL_PASS
C2TopLevelRuntimeReachability = PASS
C2FullReachableExternalSurface = PASS
C2ExecutableInterceptionBoundary = PASS
SV01-SV36 = ALL_PASS
FinalStructuralLedgerConsistency = PASS
```

A hard-coded aggregate inconsistent with member records must FAIL.

# O — Fresh tuple / restart

Any harness or validator correction invalidates the prior tuple.

For every attempt:

```text
verify frozen RunnerSHA256
compute HarnessSHA256
compute ValidatorSHA256
compute ChildProbeSHA256
create fresh durable root
run complete validation from zero
publish/reopen evidence
```

Do not compose evidence across roots.

# P — Retry-until-PASS

For every defect correctable wholly inside disposable harness/validator scope:

```text
diagnose
→ correct
→ recompute hashes
→ preserve/supersede prior root
→ fresh durable root
→ restart complete structural cycle
→ repeat until PASS
```

Do not stop after another local evidence/serialization/probe defect.

## Mandatory stop boundary

STOP if correction requires:

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

# Q — Runtime/repository invariants

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

# R — Finalization checkpoints

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

Only after one fresh tuple satisfies all executable evidence requirements:

```text
RELEASE 1.12 WP04 — TERRA C2 EXECUTABLE EVIDENCE-DERIVED PROBES REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 WINDOWS POWERSHELL 5.1 PARSER PROOF: PASS
RELEASE 1.12 WP04 — C2 CHILD/COPY-EQUIVALENT PROBE ARTIFACT: PASS
RELEASE 1.12 WP04 — C2 HERE-STRING CONTAINMENT H01-H08: ALL_PASS
RELEASE 1.12 WP04 — C2 COMPLETE EXECUTED EXTERNAL-CALL INVENTORY: PASS
RELEASE 1.12 WP04 — C2 THREE-WAY EXTERNAL-SURFACE EQUALITY: PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
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

Then STOP for fresh Luna reconciliation.

## Required handoff

Return:

```text
RunnerSHA256
FinalHarnessSHA256
FinalValidatorSHA256
ChildProbeSHA256
DurableChildProbeSHA256
WindowsPowerShellVersion
ParserErrorCountsRunnerHarnessValidatorChild
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot
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
ChildProbePath
DurableChildProbePath
ChildProbeByteEquality
ChildScopeGitInterceptionResult
ChildScopeAzInterceptionResult
ChildScopeHelperInterceptionResult
WorkspaceHelperEscapeResult
RealExternalEscapeResult
H01-H08Aggregate
T01-T18Records
T01-T18Aggregate
F01-F18Records
F01-F18Aggregate
I01-I09Records
I01-I09Aggregate
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

## Next gate

A Terra PASS under this authority still does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Executable Evidence Final Reconciliation
```

Only that Luna PASS may authorize a fresh governed W5 runtime and new W5 RunId allocation.

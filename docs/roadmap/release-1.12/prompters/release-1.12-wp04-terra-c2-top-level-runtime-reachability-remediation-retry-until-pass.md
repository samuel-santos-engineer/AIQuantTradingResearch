# Release 1.12 WP04 — Terra C2 Top-Level Runtime Reachability Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate the newly confirmed C2 structural inconsistency:

```text
C2FullReachableExternalSurface = PASS
C2ExecutableInterceptionBoundary = PASS
C2TopLevelRuntimeReachability = FAIL
```

The previous tuple is **not accepted for W5**. Correct the disposable/local harness and validator so the exact accepted top-level runtime entrypoint is structurally proven to traverse every required pre-RunId gate before RunId allocation and copied-wrapper invocation.

Then create a fresh tuple/root and rerun the complete structural cycle from zero.

Within harness/validator-only scope, diagnose, correct, re-hash, restart, and keep trying until PASS. Do not stop after each locally correctable defect.

## Binding Luna result

```text
RELEASE 1.12 WP04 — LUNA C2 FULL REACHABLE EXTERNAL-SURFACE FINAL RECONCILIATION: BLOCKED
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: FAIL
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
```

First failing gate:

```text
R13
```

The durable ledger itself reports:

```text
C2TopLevelRuntimeReachability = FAIL
```

Therefore the prior structural PASS markers are superseded for acceptance.

## Superseded tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
F80C9F8C9D4EB0570AEDE97F5A77B46DF4B34B6C15F20CFC16A3BC7183381915

ValidatorSHA256 =
6A65A8BD18E80592C7364624B3E0F71ED7457193C9AD3602A10AC2A2AE1531A0

SupersededDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\d0f09b1de28f46f195a1726cc820948b
```

Preserve this root unchanged as rejected/superseded evidence.

## Frozen runner

The runner remains immutable:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

## Remediation objective

Prove **reachability from the actual top-level governed runtime entrypoint**, not merely existence or independent success of internal gates.

The structural contract must prove one continuous control-flow chain:

```text
accepted top-level runtime entrypoint
→ identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ executable interception installation
→ Push-Location SandboxRoot
→ complete production external-surface inventory
→ harness interception inventory
→ exact inventory equality
→ Deferred/RestoreOnly descriptor probes
→ exact git/az/helper probes
→ unexpected-call fail-closed probes
→ real-command/helper escape probes
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
→ C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
→ only then RunId allocation site
→ only then copied-wrapper invocation site
```

This authority MUST NOT execute the final two runtime actions.

# Required correction A — actual top-level path

Inspect the harness's real top-level parameter/dispatch logic.

Identify the exact invocation that a future W5 authority will execute.

The validator must prove that this exact entrypoint reaches the governed runtime orchestration path.

Reject proof based only on:

```text
internal function existence
direct internal function invocation
dot-sourced harness internals
standalone probe mode that future W5 does not traverse
dead/unreachable code
parallel validation-only path
```

# Required correction B — continuous ordering proof

Produce structural evidence binding each required gate to the next gate on the same accepted top-level control-flow path.

For each transition record:

```text
FromNode
ToNode
SourceLocation
ReachabilityCondition
OrderingEvidence
BypassPossible
Result
```

Require every transition PASS and:

```text
BypassPossible = False
```

for all acceptance-critical transitions.

# Required correction C — RunId allocation dominance

Locate every possible W5 RunId allocation site.

Prove:

```text
all required pre-RunId gates dominate every RunId allocation site
```

No alternate branch, fallback, error path, or secondary entrypoint may allocate a W5 RunId before both:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
```

# Required correction D — wrapper invocation dominance

Locate every copied/production-wrapper invocation site reachable in governed W5 mode.

Prove:

```text
fresh W5 RunId allocation
dominates
copied-wrapper invocation
```

and all pre-RunId gates dominate both.

No wrapper invocation may be reachable through an alternate path that bypasses the governed preflight.

# Required correction E — top-level probe

Add or correct a structural-only top-level reachability probe that:

1. enters through the exact future W5 top-level harness invocation;
2. traverses the actual dispatch/orchestration path;
3. observes all pre-RunId gates in order;
4. stops immediately before actual W5 RunId allocation;
5. allocates no RunId;
6. invokes no governed wrapper;
7. performs no real external call;
8. records the complete ordered path.

Do not simulate acceptance by directly invoking internal gate functions out of order.

Require:

```text
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

# Required correction F — validator aggregation consistency

The validator must make these predicates jointly acceptance-critical:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
C2_FULL_REACHABLE_EXTERNAL_SURFACE
C2_TOP_LEVEL_RUNTIME_REACHABILITY
```

Final structural PASS must be impossible unless all three equal PASS.

Require fail-closed behavior if any is:

```text
missing
unresolved
FAIL
not serialized
not published
missing after reopen
```

The final aggregate must not report structural PASS while any of these fields is FAIL.

# Required correction G — durable summary round trip

Require all three actual validated predicates to flow through:

```text
in-memory
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

The reopened final ledger must explicitly contain:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
```

# Preserve full-surface remediation

Freshly re-prove without weakening:

```text
Deferred exact helper contract
RestoreOnly + RestorationDescriptor exact contract
descriptor correlation
descriptor one-time use
descriptor negative/replay rejection
finally-path sandbox helper resolution
workspace helper escape blocking
complete production/harness surface equality
exact git contracts
exact az contracts
dynamic log path normalization
no wildcard contracts
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
```

# New top-level reachability probes

Require at least:

```text
T01 future W5 invocation maps to exact accepted top-level entrypoint
T02 top-level entrypoint reaches governed runtime orchestration
T03 identity/hash gates reachable and ordered
T04 exact-byte wrapper-copy gate reachable and ordered
T05 G01-G11 reachable and ordered
T06 executable interception installation reachable and ordered
T07 SandboxRoot binding reachable and ordered
T08 full-surface equality gate reachable and ordered
T09 Deferred/RestoreOnly probes reachable and ordered
T10 exact git/az/helper probes reachable and ordered
T11 unexpected-call/escape probes reachable and ordered
T12 executable-interception PASS gate reachable and dominant
T13 full-reachable-surface PASS gate reachable and dominant
T14 every RunId allocation site dominated by all pre-RunId gates
T15 every wrapper invocation site dominated by RunId allocation and preflight
T16 no alternate/bypass top-level path
T17 structural top-level probe stops before RunId allocation
T18 structural top-level probe allocates 0 RunIds, invokes 0 wrappers, makes 0 real external calls
```

Require:

```text
T01-T18 = ALL_PASS
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

# Fresh SV01-SV36

After any harness or validator byte change:

```text
recompute hashes
→ supersede prior evidence
→ create fresh durable root
→ rerun SV01-SV36 from zero
```

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

The validator must make top-level runtime reachability acceptance-relevant in the canonical structural ledger.

# Final structural aggregate

Require exactly:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
T01-T18 = ALL_PASS
SV01-SV36 = ALL_PASS
36/36 evidence references resolved
```

Only then may the final structural result be PASS.

# Retry-until-PASS

For every defect correctable entirely within disposable/local harness or validator scope:

```text
diagnose
→ correct
→ recompute all changed hashes
→ preserve/supersede prior root
→ allocate fresh durable root
→ restart complete structural validation
→ repeat until PASS
```

Do not stop for a new authority merely because another local structural inconsistency appears.

During retries, actively check for contradictions among all serialized summary predicates before declaring PASS.

## Mandatory stop boundary

STOP if remediation requires:

```text
frozen runner change
production wrapper change
tracked-source change
new architecture/policy decision outside this contract
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

# Runtime exclusions

Throughout this authority:

```text
W5 RunIds allocated = 0
W5 wrappers invoked = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

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

After REOPEN verify all three C2 summary predicates equal PASS and the final aggregate is internally consistent.

# Repository invariants

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
```

# PASS markers

Only after one final fresh tuple passes every gate:

```text
RELEASE 1.12 WP04 — TERRA C2 TOP-LEVEL RUNTIME REACHABILITY REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ALL_PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: PASS
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 TOP-LEVEL FINAL RECONCILIATION: YES
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
SupersededHarnessSHA256
SupersededValidatorSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot
AcceptedTopLevelInvocation
TopLevelDispatchSourceLocation
GovernedRuntimeOrchestrationSourceLocation
TopLevelControlFlowEdges
RunIdAllocationSites
WrapperInvocationSites
RunIdDominanceResult
WrapperInvocationDominanceResult
BypassPathAnalysis
T01
T02
T03
T04
T05
T06
T07
T08
T09
T10
T11
T12
T13
T14
T15
T16
T17
T18
TopLevelProbeAggregate
C2TopLevelRuntimeReachabilityInMemory
C2TopLevelRuntimeReachabilityBuildLedger
C2TopLevelRuntimeReachabilitySerialized
C2TopLevelRuntimeReachabilityPublished
C2TopLevelRuntimeReachabilityReopened
C2ExecutableInterceptionBoundaryReopened
C2FullReachableExternalSurfaceReopened
FinalStructuralLedgerConsistency
F01-F18Aggregate
I01-I09Aggregate
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
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

A Terra PASS does not itself authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Top-Level Runtime Reachability Final Reconciliation
```

Only that Luna PASS may authorize a fresh governed W5 runtime and new RunId allocation.

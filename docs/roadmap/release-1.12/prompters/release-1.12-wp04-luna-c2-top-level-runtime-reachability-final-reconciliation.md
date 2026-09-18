# Release 1.12 WP04 — Luna C2 Top-Level Runtime Reachability Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform a fresh independent read-only reconciliation of the clean shared-orchestration C2 tuple. Do not modify runner, harness, validator, production/tracked source, repository state, GitHub, Azure, Docker/GHCR, or external services. Do not allocate a W5 RunId and do not invoke the governed production wrapper.

## Exact bound tuple

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
838659E521EFADBC099A945B10A73882DE7DD9281176BDDDC3FCF3D3BA8EFFFF

ValidatorSHA256 =
C5BFF19D7A4B88EFBB13C8AC6FC4D77A7B71E629D987C704CD5B27638030C7AB

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\c62ea146dfb1405eb973d28e7a8f8256
```

No acceptance evidence may be composed from superseded tuples or roots.

## R01 — Identity and evidence binding

Recompute all three hashes and require exact equality.

Verify all acceptance artifacts, ledgers, manifests, probe evidence, and retained copies belong to this exact tuple/root.

## R02 — Windows PowerShell / parsing

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
harness parser errors = 0
validator parser errors = 0
```

Independently parse both the parent harness and generated child-probe script.

## R03 — Here-string containment / parent ownership

Verify the clean rewrite corrected the prior root cause.

Require:

```text
shared orchestration function is defined in parent harness
top-level dispatch is defined in parent harness
RunId allocation site is parent-harness code
future wrapper invocation site is parent-harness code
none of the above are accidentally embedded in child-probe here-string content
child probe contains only intended child-scope proof logic
```

Reconcile H01-H08 and require `ALL_PASS`.

## R04 — Exact top-level dispatch

Identify the exact structural and future W5 top-level invocations.

Require both:

```text
StructuralProbe
ExecuteGovernedW5
```

to dispatch into the same parent-level shared orchestration function.

Reject:

```text
parallel implementations
dead dispatch
unreachable dispatch
direct internal invocation substituted for top-level proof
function-name/string presence without actual control-flow proof
```

## R05 — Shared pre-RunId path

Require both modes to share one actual path through:

```text
identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ executable interception installation
→ Push-Location SandboxRoot
→ full reachable-surface inventory/equality
→ Deferred/RestoreOnly descriptor probes
→ exact git/az/helper probes
→ unexpected-call fail-closed probes
→ real-command/helper escape probes
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
→ C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
→ explicit structural stop/continue point
```

Structural mode must stop there.

Future W5 mode must be structurally capable of continuing from that same point, but MUST NOT be executed during this reconciliation.

## R06 — RunId dominance

Locate every reachable W5 RunId allocation site.

Require every mandatory pre-RunId gate and both pre-RunId summary predicates to dominate every allocation site.

No alternate branch/fallback/error path may allocate first.

## R07 — Wrapper-invocation dominance

Locate every governed copied-wrapper invocation site.

Require:

```text
all pre-RunId gates
→ fresh W5 RunId allocation
→ copied-wrapper invocation
```

No bypass invocation path is permitted.

## R08 — Structural stop safety

Require the actual structural top-level mode to traverse the shared preflight and stop immediately before RunId allocation.

Observed structural execution must prove:

```text
W5 RunIds allocated = 0
W5 wrappers invoked = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

## R09 — T01-T18

Independently reconcile T01-T18 and require `ALL_PASS`.

Pay special attention to:

```text
T01 exact future W5 invocation maps to accepted top-level dispatch
T02 dispatch reaches shared orchestration
T14 all RunId sites dominated by preflight
T15 wrapper sites dominated by RunId + preflight
T16 no alternate/bypass path
T17 structural mode stops before RunId allocation
T18 structural mode has 0 RunIds / 0 wrappers / 0 real external calls
```

Require:

```text
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

## R10 — Full reachable external surface

Reconcile the complete production external surface against the harness interception inventory across nominal, failure, finally, and restoration paths.

Require equality, not subset matching.

Freshly require:

```text
F01-F18 = ALL_PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
Deferred exact helper contract = PASS
RestoreOnly + RestorationDescriptor contract = PASS
descriptor correlation/single-use/replay rejection = PASS
finally-path sandbox helper interception = PASS
workspace helper escape = BLOCKED
exact git contracts = PASS
exact az contracts = PASS
wildcard contracts = 0
```

## R11 — Executable interception boundary

Independently reconcile I01-I09 under child/copy-equivalent scope.

Require:

```text
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
real git escape = BLOCKED
real az escape = BLOCKED
real workspace-helper escape = BLOCKED
```

## R12 — Fresh SV01-SV36

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

Every SV record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Verify top-level runtime reachability is acceptance-relevant, not informational only.

## R13 — Joint aggregate and durable round trip

This is mandatory.

Final structural PASS must require all three:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
```

Prove each actual validated predicate survives:

```text
in-memory
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

The reopened final ledger must contain all three as PASS.

Require:

```text
FinalStructuralLedgerConsistency = PASS
```

Reject any structural PASS if one predicate is missing, unresolved, or FAIL.

## R14 — Finalization / repository invariants

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Verify durable evidence remains resolvable after cleanup.

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
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
W5 mutations = 0
```

## Reconciliation result

Classify R01-R14 individually:

```text
PASS
FAIL
NOT_PROVEN
```

Any FAIL or NOT_PROVEN blocks W5.

Do not repair artifacts during this authority.

## PASS markers

Only if every material gate passes:

```text
RELEASE 1.12 WP04 — LUNA C2 TOP-LEVEL RUNTIME REACHABILITY FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 CLEAN HARNESS STRUCTURE: ACCEPTED
RELEASE 1.12 WP04 — C2 SHARED PRE-RUNID ORCHESTRATION PATH: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL PROBES T01-T18: ACCEPTED
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: ACCEPTED
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — C2 FINAL STRUCTURAL LEDGER CONSISTENCY: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. A separate GPT-5.6 Terra W5 authority is required.

## Failure behavior

On FAIL/NOT_PROVEN return:

```text
first failing gate
exact evidence
defect owner
whether harness-only / validator-only / harness+validator remediation suffices
whether runner/production/tracked change is required
```

No W5 authorization.

## Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
DurableRootObserved
WindowsPowerShellVersion
ParserErrorCounts
ParentHarnessParseResult
GeneratedChildParseResult
H01-H08Aggregate
AcceptedStructuralTopLevelInvocation
AcceptedFutureW5TopLevelInvocation
SharedTopLevelOrchestrationFunction
StructuralStopSourceLocation
RunIdAllocationSites
WrapperInvocationSites
TopLevelControlFlowEdges
RunIdDominanceResult
WrapperInvocationDominanceResult
BypassPathAnalysis
T01-T18Aggregate
C2TopLevelRuntimeReachabilityInMemory
C2TopLevelRuntimeReachabilityBuildLedger
C2TopLevelRuntimeReachabilitySerialized
C2TopLevelRuntimeReachabilityPublished
C2TopLevelRuntimeReachabilityReopened
C2FullReachableExternalSurfaceReopened
C2ExecutableInterceptionBoundaryReopened
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

# Release 1.12 WP04 — Terra C2 Shared Top-Level Orchestration Continuation

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue the currently authorized **C2 Top-Level Runtime Reachability Remediation — Retry Until PASS**. The newly isolated defect is within the already authorized disposable harness + validator scope. No Luna architecture decision or production change is required.

Do not stop merely because this root cause has now been localized. Implement the correction, re-hash, create fresh evidence, restart the complete structural cycle, and keep retrying until PASS unless a governance boundary is crossed.

## Confirmed root cause

The current harness has two disconnected paths:

```text
-InterceptionProbe
    → performs the proven pre-RunId structural work

-ExecuteGovernedW5
    → Invoke-ApprovedTopLevelRuntime
    → obsolete placeholder calls
       Install-ApprovedInterceptions
       Persist-RawEvidenceCheckpoint
       Handoff-RawEvidenceToValidator
```

Additionally, the governed-W5 dispatch occurs after an unconditional:

```text
structural interception probe required
```

guard.

Therefore the future W5 entrypoint cannot reach the preflight path whose structural properties were proven.

Classification:

```text
Frozen runner defect = NO
Harness defect = YES
Validator defect = YES
Production-wrapper defect = NO
Production/tracked-source change required = NO
W5 RunId consumed = NO
```

## Required design

Refactor the disposable harness so **structural probing and future governed W5 execution enter one shared top-level orchestration path**.

There must be one authoritative sequence:

```text
accepted top-level dispatch
→ identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ install executable interceptions
→ Push-Location SandboxRoot
→ full reachable-surface inventory/equality
→ Deferred/RestoreOnly descriptor probes
→ exact git/az/helper probes
→ unexpected-call fail-closed probes
→ real-command/helper escape probes
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
→ C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
→ STRUCTURAL-ONLY STOP POINT
→ W5 RunId allocation
→ copied-wrapper invocation
→ raw evidence checkpoint
→ cleanup
→ validator/finalizer handoff
```

The structural mode and future W5 mode may differ **only at the explicit stop/continue decision immediately before RunId allocation**.

## Harness requirements

Replace the disconnected `-InterceptionProbe` versus `-ExecuteGovernedW5` orchestration with one shared top-level function/path.

Permitted conceptual form:

```text
Invoke-ApprovedTopLevelRuntime -Mode StructuralProbe
Invoke-ApprovedTopLevelRuntime -Mode ExecuteGovernedW5
```

or an equivalent WinPS 5.1-safe design.

Both modes MUST traverse the same actual pre-RunId code path.

### Structural mode

Must:

```text
traverse every real pre-RunId gate
record ordered reachability evidence
stop immediately before RunId allocation
allocate 0 RunIds
invoke 0 governed wrappers
perform 0 real external calls
```

### Future W5 mode

Must traverse the identical pre-RunId path and, only after every gate passes, continue to:

```text
fresh RunId allocation
copied-wrapper invocation
```

Do not execute this mode under this authority.

## Remove obsolete/dead orchestration

Remove or replace obsolete placeholder-only runtime calls if they are not the actual accepted implementation:

```text
Install-ApprovedInterceptions
Persist-RawEvidenceCheckpoint
Handoff-RawEvidenceToValidator
```

No dead placeholder path may remain as the accepted future W5 route.

If names remain, they must resolve to the actual governed implementation and be proven on the shared path; mere function-name presence is insufficient.

## Dispatch/guard correction

The unconditional structural-probe guard must not make the governed W5 dispatch unreachable.

Require a single dispatch arrangement in which:

```text
StructuralProbe
ExecuteGovernedW5
```

both enter the shared orchestration function.

No acceptance-critical dispatch may appear after an unconditional terminating guard that prevents its reachability.

## Validator correction

The validator must prove actual control flow, not function-name/string presence.

It must verify:

```text
future W5 CLI/top-level invocation
→ actual dispatch branch
→ shared top-level orchestration
→ same pre-RunId gates as structural probe
→ explicit structural stop point
→ RunId site after stop point
→ wrapper site after RunId site
```

Reject:

```text
function-name regex alone
string occurrence alone
dead code
unreachable dispatch
parallel probe-only path
direct internal-function invocation as top-level proof
```

## Dominance requirements

Locate every reachable W5 RunId allocation site and copied-wrapper invocation site.

Require:

```text
all pre-RunId gates dominate every RunId allocation site
every RunId allocation site dominates its wrapper invocation
no alternate/bypass path exists
```

## T01-T18

Re-run and strengthen T01-T18 under the refactored shared path.

Require especially:

```text
T01 exact future W5 invocation maps to accepted top-level dispatch
T02 dispatch reaches shared orchestration
T03-T13 all pre-RunId gates occur on that same path
T14 all RunId sites dominated by all pre-RunId gates
T15 all wrapper sites dominated by RunId + preflight
T16 no alternate/bypass path
T17 structural mode stops immediately before RunId allocation
T18 structural mode: RunIds 0 / wrappers 0 / real external calls 0
```

Require:

```text
T01-T18 = ALL_PASS
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

## Preserve all prior gates

Freshly re-prove:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
Deferred/RestoreOnly exact helper contract = PASS
finally-path interception = PASS
surface inventory equality = PASS
exact git contract = PASS
exact az contract = PASS
wildcard contracts = 0
```

## Validator aggregation

Structural PASS must require all three:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
```

All three must survive:

```text
in-memory
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Any missing, unresolved, or FAIL value makes the final structural result FAIL.

## Fresh tuple requirement

Any harness or validator byte change invalidates prior acceptance evidence.

After correction:

```text
compute new HarnessSHA256
compute new ValidatorSHA256
verify frozen RunnerSHA256
create fresh durable root
restart complete validation from zero
```

Do not promote or overwrite prior roots.

## Fresh SV01-SV36

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

The canonical structural ledger must make top-level runtime reachability acceptance-relevant.

## Retry-until-PASS

For every defect wholly correctable inside disposable harness/validator scope:

```text
diagnose
→ fix
→ re-hash
→ preserve/supersede prior evidence
→ fresh durable root
→ restart full structural cycle
→ continue until PASS
```

Do not stop after another local defect.

## Mandatory stop boundary

STOP if correction requires:

```text
frozen runner change
production wrapper change
tracked-source change
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

## Runtime exclusions

Throughout:

```text
W5 RunIds allocated = 0
W5 wrappers invoked = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

## Repository invariants

Only the two pre-existing WP04 tracked modifications may remain. Require:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

## Finalization

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After reopen require:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
FinalStructuralLedgerConsistency = PASS
```

## PASS markers

Only after a fresh single tuple passes every gate:

```text
RELEASE 1.12 WP04 — TERRA C2 SHARED TOP-LEVEL ORCHESTRATION REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 SHARED PRE-RUNID ORCHESTRATION PATH: PASS
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

Then STOP for fresh Luna reconciliation.

## Required handoff

Return:

```text
RunnerSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot
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
ObsoletePlaceholderDisposition
StructuralGuardDisposition
T01-T18Aggregate
C2TopLevelRuntimeReachabilityInMemory
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

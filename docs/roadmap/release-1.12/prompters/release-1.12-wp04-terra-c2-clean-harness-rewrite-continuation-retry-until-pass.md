# Release 1.12 WP04 — Terra C2 Clean Harness Rewrite Continuation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue the existing C2 top-level runtime reachability remediation. The newly confirmed root cause remains entirely within disposable harness + validator scope.

Replace the disposable harness with a **clean implementation** rather than incrementally patching the malformed structure. Update the validator to prove actual dispatch/control flow. Then re-hash, create a fresh durable root, and restart the complete structural cycle from zero.

Keep correcting and retrying within harness/validator-only scope until PASS. Do not stop after another locally correctable defect.

## Confirmed root cause

The intended top-level orchestration implementation was accidentally written inside the child probe's PowerShell here-string.

Consequences:

```text
intended orchestration code is emitted into generated child probe script
intended orchestration code is not defined in the parent harness
structural probe path and future W5 path are disconnected
future W5 top-level reachability cannot be proven
text-position/function-name validation can produce misleading evidence
```

Classification:

```text
Frozen runner defect = NO
Disposable harness defect = YES
Validator defect = YES
Production/tracked-source defect = NO
Architecture decision required = NO
W5 RunIds consumed = 0
```

## Binding invariants

Frozen runner remains:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Production/tracked scripts must remain unchanged.

Throughout this authority:

```text
W5 RunIds allocated = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

# A — Clean rewrite, not textual surgery

Create a fresh disposable harness implementation with explicit, visually/auditably separate sections.

Do not copy forward malformed here-string placement merely to minimize the diff.

Recommended structure:

```text
1. param(...) / top-level mode declaration
2. pure utility functions
3. exact git/az interception functions
4. shadow-helper construction
5. child-probe script construction
6. full-surface probe functions
7. shared pre-RunId orchestration
8. structural stop decision
9. future governed W5 continuation
10. final top-level dispatch
```

Every here-string must have an explicit narrow purpose and clear start/end boundaries.

After construction, parse both:

```text
parent harness
generated child probe script
```

and independently prove that shared orchestration functions exist in the **parent harness**, not inside generated child text.

# B — One shared orchestration path

Both modes must enter the same parent-harness orchestration:

```text
StructuralProbe
ExecuteGovernedW5
        │
        ▼
Invoke-ApprovedTopLevelRuntime
        │
        ├─ identity/hash gates
        ├─ exact-byte wrapper copy
        ├─ G01-G11
        ├─ executable interceptions
        ├─ Push-Location SandboxRoot
        ├─ complete reachable-surface equality
        ├─ Deferred/RestoreOnly descriptor probes
        ├─ exact git/az/helper probes
        ├─ unexpected-call probes
        ├─ escape probes
        ├─ C2_EXECUTABLE_INTERCEPTION_BOUNDARY
        ├─ C2_FULL_REACHABLE_EXTERNAL_SURFACE
        └─ explicit pre-RunId stop/continue point
                 │
           ┌─────┴─────┐
           │           │
     Structural     ExecuteGovernedW5
        STOP             │
                    RunId allocation
                         │
                    wrapper invocation
```

This authority executes only StructuralProbe.

# C — Structural stop semantics

The structural mode must traverse the real shared path and stop at one explicit location immediately before the first possible W5 RunId allocation.

Require:

```text
all pre-RunId gates executed
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
stop point reached
RunId allocation count = 0
wrapper invocation count = 0
real external calls = 0
```

The structural mode must not be a parallel substitute implementation.

# D — Future W5 path

The future `ExecuteGovernedW5` dispatch must be structurally reachable through the same shared orchestration and must continue only after the structural stop point.

Do not execute it here.

Remove obsolete placeholder-only runtime wiring. No accepted W5 route may depend on dead or undefined placeholders such as:

```text
Install-ApprovedInterceptions
Persist-RawEvidenceCheckpoint
Handoff-RawEvidenceToValidator
```

unless those names are deliberately retained as actual defined governed implementations and their real behavior is structurally proven.

# E — Here-string containment proof

Add explicit structural checks:

```text
H01 every here-string delimiter is balanced
H02 generated child-probe content parses under WinPS 5.1
H03 parent shared-orchestration definition is outside all here-strings
H04 parent top-level dispatch is outside all here-strings
H05 RunId allocation site is outside child-probe text
H06 future wrapper invocation site is outside child-probe text
H07 child probe contains only intended child-scope proof logic
H08 no parent orchestration function is accidentally duplicated into generated child content
```

Require:

```text
H01-H08 = ALL_PASS
```

# F — Validator rewrite: semantic/control-flow evidence

Update the validator so acceptance does not depend on textual position or mere function-name occurrence.

It must prove actual parent-harness structure and dispatch using evidence such as:

```text
PowerShell AST node type
parent AST extent/source range
function definition ownership
top-level parameter/dispatch AST
command invocation target
control-flow/branch relationship
explicit structural stop node
RunId allocation node
wrapper invocation node
```

Use WinPS 5.1-compatible parsing/AST APIs.

Reject acceptance based solely on:

```text
regex/string matching
IndexOf ordering
function-name presence
text appearing before/after another text
content found inside a here-string
dead/unreachable definitions
direct internal invocation not used by top-level dispatch
```

# G — Actual top-level reachability

Require a continuous structural proof:

```text
future W5 CLI invocation
→ parent top-level dispatch
→ shared orchestration function
→ every required pre-RunId gate
→ structural stop/continue point
→ RunId allocation site
→ wrapper invocation site
```

Require all pre-RunId gates to dominate every reachable RunId site, and RunId allocation to dominate every reachable governed wrapper invocation.

No bypass branch may exist.

# H — T01-T18

Freshly execute T01-T18 against the clean harness.

Require:

```text
T01-T18 = ALL_PASS
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

T01/T02/T16/T17 must specifically be based on the clean parent-harness dispatch/orchestration structure, not generated child content.

# I — Preserve complete external-surface contract

The rewrite must preserve and freshly prove:

```text
Deferred exact helper contract
RestoreOnly + RestorationDescriptor exact contract
descriptor correlation
descriptor single-use/replay rejection
finally-path sandbox helper resolution
workspace helper escape blocked
complete production/harness external-surface equality
exact git contracts
exact az contracts
dynamic log path normalization
wildcard contracts = 0
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
```

Do not simplify away previously accepted safety gates.

# J — Joint final predicates

Final structural acceptance requires all:

```text
C2ExecutableInterceptionBoundary = PASS
C2FullReachableExternalSurface = PASS
C2TopLevelRuntimeReachability = PASS
H01-H08 = ALL_PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
T01-T18 = ALL_PASS
SV01-SV36 = ALL_PASS
```

All three C2 summary predicates must round-trip through:

```text
in-memory
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

and remain PASS.

# K — Fresh tuple and complete restart

The clean rewrite changes harness bytes and validator bytes.

Therefore:

```text
compute new HarnessSHA256
compute new ValidatorSHA256
verify frozen RunnerSHA256
preserve all prior roots
create fresh durable root
rerun complete structural validation from zero
```

No acceptance credit may be composed from earlier tuples.

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

# L — Retry-until-PASS

If any failure is correctable entirely within disposable harness/validator scope:

```text
diagnose
→ fix cleanly
→ re-hash
→ supersede prior candidate evidence
→ fresh durable root
→ restart complete cycle
→ continue until PASS
```

Prefer correcting the underlying structure over accumulating brittle textual patches.

## Mandatory stop boundary

STOP if correction requires:

```text
frozen runner modification
production/tracked-source modification
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

# M — Repository invariants

Only the two pre-existing WP04 tracked modifications may remain.

Require:

```text
authority-introduced tracked modifications = 0
staged paths = 0
git diff --check exit code = 0
```

# N — Finalization

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

After reopen require all structural summaries and aggregate consistency.

# PASS markers

Only after one fresh clean tuple passes:

```text
RELEASE 1.12 WP04 — TERRA C2 CLEAN HARNESS REWRITE: PASS
RELEASE 1.12 WP04 — C2 HERE-STRING CONTAINMENT H01-H08: ALL_PASS
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

Then STOP for Luna.

# Required handoff

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
ParentHarnessParseResult
GeneratedChildParseResult
H01-H08Aggregate
SharedTopLevelOrchestrationFunction
AcceptedStructuralTopLevelInvocation
AcceptedFutureW5TopLevelInvocation
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

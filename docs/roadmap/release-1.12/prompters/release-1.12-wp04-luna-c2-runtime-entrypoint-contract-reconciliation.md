# Release 1.12 WP04 — Luna C2 Runtime Entrypoint Contract Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Reconcile the newly proven C2 runtime-entrypoint defect before any further W5 execution.

This is a read-only contract/architecture authority. Do not modify runner, harness, validator, tracked source, Git/GitHub state, Azure, Docker/GHCR, production, or external services. Do not allocate a W5 RunId and do not invoke the production wrapper.

## Accepted identities and prior structural state

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
7056320ED8D2B2F1C1EABB3E6CEE838791FBB1DFA1F67E09620887130E6F8926

ValidatorSHA256 =
3947E35A1062E577D59256A29D40B2573C433363E8538B39FC721E47628B2F93
```

Prior C2 reconciliation accepted the structural runner/harness binding. New runtime inspection now proves that the frozen harness has no top-level governed runtime execution path.

Do not treat the failed W5 launch as a production-wrapper defect.

## Decisive new evidence

Observed in the exact frozen harness:

```text
top-level executable path:
  -StructuralProbe at lines 57-62

all non-probe top-level invocation:
  line 63 throws:
  "Governed W5 execution authority required"

Invoke-G12Runtime:
  defined internally
  not called by an approved top-level runtime orchestration path

top-level governed shim installation:
  absent

top-level copied production-wrapper invocation:
  absent
```

Attempt result:

```text
W5 RunId allocated = NO
production wrapper invoked = NO
external mutations = 0
repository mutations = 0
P05 = NOT_PROVEN
W5 acceptance = NOT_GRANTED
```

## Reconciliation question

Determine whether the accepted C2 structural contract was incomplete because it validated runtime primitives/order structurally without requiring an executable top-level governed runtime entry point.

The expected narrow defect class is:

```text
C2_HARNESS_CONTRACT_DEFECT / MISSING_TOP_LEVEL_RUNTIME_ORCHESTRATION
```

Confirm or reject this classification from exact source/evidence.

## Required source inspection

Inspect the exact retained harness and validator and identify:

1. top-level parameter/dispatch structure;
2. `-StructuralProbe` path;
3. non-probe guard at line 63;
4. `Invoke-G12Runtime` definition and callers;
5. shim installation functions and callers;
6. production-wrapper invocation function and callers;
7. RunId allocation operation and callers;
8. G01-G11 executable operations and ordering;
9. exact-byte source/copy operations and ordering;
10. cleanup/evidence handoff operations;
11. validator checks that previously allowed structural PASS despite no reachable top-level runtime path.

## Required decision

State explicitly:

```text
Frozen runner defect = YES/NO
Frozen C2 harness defect = YES/NO
Prior C2 structural-validation contract defect = YES/NO
Production-wrapper defect = YES/NO
Harness byte change required = YES/NO
New HarnessSHA256 required = YES/NO
Validator byte change required = YES/NO
New ValidatorSHA256 required = YES/NO
Fresh SV01-SV36 required = YES/NO
Fresh Luna C2 reconciliation required = YES/NO
W5 currently authorized = YES/NO
W5 RunId currently authorized = YES/NO
```

## Required remediation contract if defect confirmed

Define the minimum harness change as an explicit governed top-level runtime entry point.

It must be impossible to reach runtime execution merely by dot-sourcing/calling an internal function through an unapproved side path.

The top-level runtime path must mechanically orchestrate, in the accepted order:

```text
1. verify frozen runner identity
2. verify harness self-identity as required by the execution authority
3. verify production-wrapper source identity
4. create exact-byte disposable production-wrapper copy
5. execute G01-G11
6. assert source/copy equality
7. only then allocate one fresh W5 RunId
8. install only approved minimum git/az/helper interceptions
9. verify required S/W child visibility
10. invoke the copied real production wrapper exactly once
11. record raw external-call and lifecycle observations
12. create durable pre-cleanup evidence checkpoint
13. perform disposable cleanup
14. hand raw evidence to the independent validator/finalizer
```

The harness must not self-award P01-P20.

P19 remains validator-owned after cleanup.

P20 remains validator observation-only.

## Structural-contract correction

The fresh validator must add an explicit reachability predicate proving that, from the harness's approved top-level runtime entry point:

```text
runtime orchestration is reachable
G01-G11 are on that reachable path
RunId allocation is reachable only after pre-RunId gates
shim installation is reachable before wrapper invocation
production-wrapper invocation is reachable exactly once after RunId
cleanup/evidence handoff is reachable afterward
```

Structural probes must terminate before actual governed RunId allocation or production-wrapper invocation.

A collection of individually defined internal functions is insufficient.

## Existing SV01-SV36

Do not silently renumber or alter the canonical 36 checks.

Decide how the missing top-level-reachability requirement is incorporated without weakening them. Prefer strengthening the existing executable/runtime binding checks and their retained evidence rather than inventing runtime PASS claims.

If a new explicit structural predicate outside SV01-SV36 is necessary, name it and require it in addition to all 36 canonical checks.

## Required terminal markers

If the defect and remediation contract are fully reconciled, emit:

```text
RELEASE 1.12 WP04 — LUNA C2 RUNTIME ENTRYPOINT CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FROZEN HARNESS RUNTIME CAPABILITY: NOT_ACCEPTED
RELEASE 1.12 WP04 — C2 RUNTIME ENTRYPOINT DEFECT: CONFIRMED
RELEASE 1.12 WP04 — C2 HARNESS REMEDIATION: REQUIRED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then require a Terra harness-remediation + fresh structural-validation authority.

If evidence contradicts the reported defect, identify the exact approved reachable runtime entry point and why the attempted authority failed to invoke it. Do not authorize W5 directly from this reconciliation.

## Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
TopLevelHarnessEntrypoints
StructuralProbeSourceLocation
RuntimeGuardSourceLocation
InvokeG12RuntimeCallerSet
ShimInstallationCallerSet
ProductionWrapperInvocationCallerSet
RunIdAllocationCallerSet
ApprovedRuntimePathReachability
DefectClassification
FrozenRunnerDefect
FrozenC2HarnessDefect
PriorC2StructuralValidationContractDefect
ProductionWrapperDefect
HarnessByteChangeRequired
NewHarnessSHA256Required
ValidatorByteChangeRequired
NewValidatorSHA256Required
FreshSV01SV36Required
FreshLunaC2ReconciliationRequired
GovernedW5ExecutionCurrentlyAuthorized
GovernedW5RunIdCurrentlyAuthorized
ExactMutationAccounting
RequiredStructuralContractCorrection
NextAuthorityRequired
```

# Release 1.12 WP04 — Luna C2 Full Helper-Surface Contract Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform a fresh read-only reconciliation of the newly proven pre-RunId C2 helper-surface contract gap.

Do not modify runner, harness, validator, production/tracked source, repository state, GitHub, Azure, Docker/GHCR, or external services. Do not allocate a W5 RunId and do not invoke the production wrapper.

## Accepted tuple now shown incomplete for W5 runtime

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
D14E0D634203FB8121564513669DD4B1743F91E0D3F71445644BF1B5CD9ECB27

ValidatorSHA256 =
451FC907DA208462DBCEAE56D504FDBB83D6D143CF68C5EDD0F46AFDE313F867

StructuralDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\8ab9bec858594a2daab1fe20f2b4502c
```

The tuple remains historically accepted for the evidence it proved, but W5 runtime authorization is suspended because the full reachable helper surface was not represented.

## New decisive evidence

Production wrapper reachable helper calls include at least:

```text
line 359: Deferred helper invocation
line 393: RestoreOnly helper invocation with -RestorationDescriptor
```

The frozen disposable shadow helper reportedly:

```text
supports Deferred
does not expose RestorationDescriptor
does not support the full RestoreOnly call shape
```

Therefore the pre-RunId full-surface allowlist/interception gate cannot pass.

Observed fail-closed result:

```text
W5 wrapper executions = 0
W5 RunIds allocated = 0
real external invocations = 0
tracked-source mutations = 0
Azure/Docker/GHCR/Git/GitHub mutations = 0
```

## Reconciliation objective

Determine the exact complete helper-call surface reachable in the governed W5 scenario, including success, failure, and finally/restoration paths.

Do not limit analysis to the nominal Deferred call.

The governing principle is:

```text
PRE-RUNID INTERCEPTION COVERAGE MUST INCLUDE EVERY REACHABLE EXTERNAL CALL SURFACE
```

## R01 — Production wrapper helper surface

Inspect the exact production wrapper and enumerate every reachable helper invocation in the W5 path.

For each invocation return:

```text
source location
lifecycle phase
command/helper identity
parameter names
literal values
dynamic values
required types/shapes
reachability condition
whether reachable from success path
whether reachable from failure path
whether reachable from finally
```

At minimum reconcile:

```text
Deferred
RestoreOnly
-RestorationDescriptor
```

Also search for any additional helper call surface not previously represented.

## R02 — Shadow helper contract

Inspect the exact frozen harness/shadow-helper generation.

Compare its accepted parameter contract to every production helper invocation from R01.

Classify each production call:

```text
COVERED
NOT_COVERED
NOT_REACHABLE
```

Any reachable NOT_COVERED call confirms the defect.

## R03 — RestorationDescriptor semantics

Determine the minimum safe disposable representation required for:

```text
-RestorationDescriptor
```

The harness must accept and validate the production wrapper's actual argument shape without manufacturing lifecycle policy.

Define:

```text
expected parameter type/shape
allowed source
required identity/correlation to prior Deferred state
sanitized ledger representation
forbidden arbitrary values
governed local RestoreOnly result
```

Do not change production semantics.

## R04 — Exact helper allowlist

Define the complete exact helper allowlist for the governed W5 scenario.

It must cover every reachable production helper call while rejecting any unapproved helper shape.

No broad wildcard helper matching.

Require exact normalized contracts analogous to the accepted az contract.

## R05 — Finally-path coverage

The validator contract must explicitly prove helper interception on the wrapper's `finally` path.

Structural validation must not pass merely because the nominal Deferred helper call is intercepted.

Require a child/copy-equivalent probe that exercises the helper call shape corresponding to:

```text
RestoreOnly + RestorationDescriptor
```

without executing governed W5.

## R06 — Full reachable-surface inventory

Reconcile not only helper calls but whether the accepted pre-RunId contract actually inventories every reachable external surface across:

```text
nominal path
failure path
finally path
cleanup/restoration path
```

Return any additional omissions involving:

```text
git
az
helper
other executable/script/process invocation
```

If another omission exists, include it in the same remediation contract now rather than creating another one-defect cycle.

## Required decisions

Return explicitly:

```text
Frozen runner defect = YES/NO
C2 harness defect = YES/NO
C2 validator/structural-contract defect = YES/NO
Production-wrapper defect = YES/NO
Harness byte change required = YES/NO
Validator byte change required = YES/NO
Production-wrapper byte change required = YES/NO
New HarnessSHA256 required = YES/NO
New ValidatorSHA256 required = YES/NO
Fresh SV01-SV36 required = YES/NO
Fresh Luna reconciliation required = YES/NO
W5 currently authorized = YES/NO
W5 RunId currently authorized = YES/NO
```

Expected absent contradictory evidence:

```text
Frozen runner defect = NO
C2 harness defect = YES
C2 validator/structural-contract defect = YES
Production-wrapper defect = NO
Harness byte change required = YES
Validator byte change required = YES
Production-wrapper byte change required = NO
New HarnessSHA256 required = YES
New ValidatorSHA256 required = YES
Fresh SV01-SV36 required = YES
Fresh Luna reconciliation required = YES
W5 currently authorized = NO
W5 RunId currently authorized = NO
```

## Remediation contract requirements

If confirmed, the next Terra authority must remediate the disposable harness and validator only.

It must:

```text
support every reachable helper call shape
support Deferred exactly
support RestoreOnly exactly
accept/validate RestorationDescriptor exactly
ledger every governed helper call
reject unexpected helper call shapes
prove nominal-path helper interception
prove finally-path helper interception
prove real workspace-helper escape blocked
preserve lifecycle policy in production wrapper
```

## Full-surface pre-RunId probe requirement

Define a strengthened probe suite that proves before RunId allocation:

```text
all reachable git call shapes covered
all reachable az call shapes covered
all reachable helper call shapes covered
Deferred helper shape covered
RestoreOnly + RestorationDescriptor shape covered
unexpected helper shape rejected
finally-path helper resolution stays in sandbox
real helper escape blocked
```

Add a structural summary predicate:

```text
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
```

This must be required in addition to:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

## Validator correction

Fresh validator logic must fail if any reachable external call from nominal/failure/finally paths lacks an exact governed interception contract.

It must compare a production-wrapper reachable-surface inventory against the harness allowlist/interception inventory.

A subset match is insufficient.

## Retry policy for next Terra authority

The next Terra remediation authority should use retry-until-PASS within disposable harness/validator scope:

```text
discover additional in-scope omission
→ correct harness/validator
→ re-hash
→ supersede prior candidate evidence
→ fresh durable root
→ rerun complete structural cycle
→ repeat until final pair passes
```

Stop only for a boundary-crossing defect requiring frozen runner, production/tracked source, external mutation, or new policy/architecture authority.

## PASS markers

If reconciliation confirms and fully specifies the defect:

```text
RELEASE 1.12 WP04 — LUNA C2 FULL HELPER-SURFACE CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 RESTOREONLY HELPER-SURFACE GAP: CONFIRMED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL-SURFACE VALIDATION: REQUIRED
RELEASE 1.12 WP04 — C2 HARNESS/VALIDATOR REMEDIATION: REQUIRED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
DeferredHelperSourceLocation
RestoreOnlyHelperSourceLocation
AllReachableHelperCallSurfaces
AllReachableGitCallSurfaces
AllReachableAzCallSurfaces
OtherReachableExternalCallSurfaces
ShadowHelperCurrentParameterContract
DeferredCoverageResult
RestoreOnlyCoverageResult
RestorationDescriptorCoverageResult
RestorationDescriptorRequiredShape
FinallyPathCoverageResult
RealWorkspaceHelperEscapeRisk
FullReachableExternalSurfaceInventory
HarnessAllowlistInventory
InventoryDifference
DefectClassification
FrozenRunnerDefect
C2HarnessDefect
C2ValidatorStructuralContractDefect
ProductionWrapperDefect
HarnessByteChangeRequired
ValidatorByteChangeRequired
ProductionWrapperByteChangeRequired
NewHarnessSHA256Required
NewValidatorSHA256Required
FreshSV01SV36Required
FreshLunaReconciliationRequired
GovernedW5ExecutionCurrentlyAuthorized
GovernedW5RunIdCurrentlyAuthorized
RequiredHelperAllowlist
RequiredFullSurfaceProbeContract
RequiredValidatorCorrection
ExactMutationAccounting
NextAuthorityRequired
```

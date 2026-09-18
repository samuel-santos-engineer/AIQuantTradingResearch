# Release 1.12 WP04 — Luna C2 Runtime Interception Contract Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Reconcile the newly proven **pre-RunId C2 runtime interception/orchestration defect**.

This is read-only. Do not modify runner, harness, validator, production/tracked source, repository state, GitHub, Azure, Docker/GHCR, or external services. Do not allocate a W5 RunId and do not invoke the production wrapper.

## Accepted tuple under examination

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
636FF0C654CA0A5901D4A8A5D27CA848DD2C04C1B9A155235FAA1F762C10EBAD

ValidatorSHA256 =
CC5BED1E30889E8ACCF7A912A870ECBFA9F5EDC9D0DB68074294F0FBD88EACFB
```

Preflight failure evidence:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w5-c2\preflight-fail-9e9a4cb28b264c4594d5b83acbd638de\w5-prerunid-failure.json
```

## New decisive evidence

The accepted harness top-level runtime path is reachable, but the governed interception boundary is not executable as required:

```text
copied wrapper invocation = & $W
wrapper local-validation fixture/callback = not supplied
Install-ApprovedInterceptions = records ledger entries only
executable git shim = not installed
executable az shim = not installed
required helper shim/interception = not installed
```

Therefore normal copied-wrapper execution would be able to reach real `git`, `az`, and helper operations, which the W5 authority forbids.

Observed fail-closed result:

```text
W5 RunId allocated = NO
production wrapper invoked = NO
real external calls = 0
tracked mutation = 0
staged paths = 0
git diff --check = PASS
```

## Reconciliation question

Determine the exact defect and minimum safe remediation.

Expected classification:

```text
C2_HARNESS_RUNTIME_INTERCEPTION_ORCHESTRATION_DEFECT
```

Specifically determine whether the harness must invoke the production wrapper through its existing local-validation fixture/callback seam and/or install actual executable scoped shims so that all governed `git`, `az`, and helper calls are intercepted rather than merely described in a ledger.

Do not infer a seam that exact production-wrapper source does not provide.

## Required source inspection

Inspect exact harness, validator, copied/production wrapper, and retained preflight evidence.

Identify:

1. harness top-level runtime invocation site;
2. exact `& $W` call;
3. `Install-ApprovedInterceptions` implementation and actual side effects;
4. production wrapper parameters and local-validation fixture/callback seam, if present;
5. every reachable `git`, `az`, and helper invocation in the governed W5 path;
6. how prior structural validation represented shim installation;
7. why that evidence passed without proving executable interception;
8. whether a harness-only change is sufficient;
9. whether validator bytes must change to prevent recurrence.

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

## Required remediation contract if confirmed

Prefer the narrowest mechanism already supported by the production wrapper.

The remediated harness must ensure that, before any W5 RunId allocation, structural/runtime preflight can prove the copied wrapper will execute with a **real executable governed interception boundary**.

Required properties:

```text
real git invocation cannot escape
real az invocation cannot escape
real helper invocation cannot escape
only explicit approved call shapes are accepted
unexpected calls fail closed
all intercepted calls are durably ledgered
no global machine/session mutation survives the disposable harness
production lifecycle policy remains in the production wrapper
harness does not manufacture W5 outcomes
```

If the wrapper already exposes a local-validation fixture/callback seam, define exactly how the harness must bind it.

If executable command shims are required, define their scope, lifetime, precedence, cleanup, and fail-closed behavior.

Do not authorize tracked production-wrapper modification unless exact source proves no existing safe seam can satisfy the contract.

## RunId ordering correction

Require the remediated architecture to prove **before RunId allocation**:

```text
interception implementation installed/bound
all required wrapper external-call surfaces mapped
escape-to-real-command probe = blocked/fail-closed
approved intercepted-call probe = handled locally
unexpected-call probe = fail-closed
```

Only then may a later W5 authority allocate a fresh RunId.

## Structural validation correction

Fresh structural validation must not accept:

```text
"shim names exist"
"ledger entries exist"
"Install-ApprovedInterceptions function exists"
```

as executable-interception proof.

It must prove that an invocation originating from the copied wrapper resolves to the governed interception implementation and cannot reach the real external command.

Preserve canonical SV01-SV36 numbering. Strengthen applicable evidence, especially:

```text
SV16 G01-G11 before wrapper
SV27 minimum shims
SV28 unexpected calls fail closed
SV29 no policy duplication
```

Add a separate structural summary predicate if needed:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

This is structural, not a runtime P01-P20 PASS claim.

## Mutation boundary

This Luna authority permits zero mutations.

If reconciliation confirms harness-only remediation, next authority must remain local-only/disposable and introduce zero tracked-source mutations.

If validator correction is required, it remains local-only and requires a new validator hash.

## PASS markers

If fully reconciled:

```text
RELEASE 1.12 WP04 — LUNA C2 RUNTIME INTERCEPTION CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION DEFECT: CONFIRMED
RELEASE 1.12 WP04 — C2 HARNESS RUNTIME INTERCEPTION REMEDIATION: REQUIRED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then identify the exact Terra remediation/validation authority required.

## Required handoff

Return:

```text
RunnerSHA256Observed
HarnessSHA256Observed
ValidatorSHA256Observed
PreflightEvidencePath
HarnessWrapperInvocationSourceLocation
ProductionWrapperFixtureOrCallbackContract
ReachableGitCallSurfaces
ReachableAzCallSurfaces
ReachableHelperCallSurfaces
InstallApprovedInterceptionsObservedBehavior
ExecutableGitInterceptionPresent
ExecutableAzInterceptionPresent
ExecutableHelperInterceptionPresent
RealCommandEscapePossibleBeforeRemediation
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
RequiredInterceptionMechanism
RequiredPreRunIdInterceptionProof
ExactMutationAccounting
NextAuthorityRequired
```

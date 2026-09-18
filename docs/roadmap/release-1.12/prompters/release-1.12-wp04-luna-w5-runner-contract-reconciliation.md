# Release 1.12 WP04 — Luna W5 Runner Contract Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Reconcile the newly proven W5 runner/governance-contract ambiguity before any further W5 execution.

This is a **read-only contract/architecture decision authority**. Do not modify runner bytes, validator bytes, production/tracked source, Git state, GitHub, Azure, Docker/GHCR, or external services. Do not allocate a W5 RunId and do not invoke the production wrapper.

## Accepted structural baseline

The prior Architecture-B structural contract remains accepted for its proven structural purpose:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
E4E3773E480B5CE9A8E15FBB3939BFBAD43B29D7DC3A8D5FE795B9F9C8DD4F0F

RELEASE 1.12 WP04 — LUNA ARCHITECTURE-B FINAL R1 STRUCTURAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 STRUCTURAL CONTRACT: ACCEPTED
```

Do not revoke structural acceptance merely because W5 runtime capability was not part of what the frozen runner actually exposes.

## New decisive evidence

The subsequent W5 authority attempted to use the exact frozen runner as the W5 execution path.

Observed:

```text
-ValidateOnly:
  succeeds
  explicitly reports no RunId
  explicitly reports no wrapper invocation

normal entry point:
  terminates before W5 setup with:
  "Separate authority required for governed W5."
```

Consequently:

```text
W5 RunId allocated = NO
production wrapper invoked = NO
Azure mutations = 0
GitHub mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
tracked-source mutations = 0

P05 wrapper execution completed = NOT_PROVEN
W5 P01-P20 = NOT_ALL_PASS
W5 governed acceptance = NOT_GRANTED
```

## Question to decide

Determine the canonical relationship between:

```text
A. frozen Architecture-B structural runner
B. governed W5 runtime harness/execution path
```

The prior authority assumed W5 would execute “through the frozen runner,” but the frozen runner itself expressly exposes no W5 runtime entry point.

A separate runtime path cannot simply be invented under the failed W5 authority because doing so could reimplement frozen runner guarantees or change the accepted contract.

## Required source inspection

Read the exact retained frozen runner and accepted validator artifacts.

Identify and cite/source-locate:

1. runner parameters/entry points;
2. `-ValidateOnly` behavior;
3. the `"Separate authority required for governed W5."` guard;
4. all runner-owned G01-G11/pre-RunId protections;
5. runner-owned source/copy hash protections;
6. child-variable visibility protections;
7. unexpected-external-call fail-closed protections;
8. P19/P20 structural semantics;
9. which protections are purely structurally validated versus actually executable by a future W5 runtime harness.

Do not infer a runtime API that does not exist.

## Decision constraints

The selected contract must preserve:

```text
Architecture B = FROZEN_RUNNER_PLUS_INDEPENDENT_VALIDATOR
RunnerSHA256 remains the accepted structural execution identity unless an explicit new structural cycle is deliberately required.
Validator remains independent.
P01-P20 semantics unchanged.
SV01-SV36 semantics unchanged.
G01-G11 ordering/protections unchanged.
P19 remains validator-owned post-cleanup finalization.
P20 remains validator observation-only.
cross-hash PASS carry-forward remains forbidden where a new structural hash is required.
real external calls must remain zero for W5.
```

Do not weaken acceptance criteria merely to make W5 executable.

## Candidate contract classes

Evaluate at minimum these classes without presuming a winner:

### C1 — Existing runner has an overlooked authorized runtime interface

Select only if exact source proves such an interface already exists and can execute W5 without byte changes or contract invention.

### C2 — Separate governed W5 runtime harness is the intended Architecture-B execution component

Under this model:

```text
frozen runner = structural contract/reference artifact
independent validator = structural/finalization verifier
separate W5 harness = runtime executor
```

If selecting C2, specify exactly which runner-validated invariants the W5 harness must consume/reuse rather than independently reinterpret, and define how its identity must be frozen and structurally validated before W5.

### C3 — Frozen runner itself must gain an explicit W5 runtime entry point

Select only if Architecture B requires one executable artifact to own both structural and runtime execution.

This necessarily changes runner bytes and invalidates the current runner hash as the runtime identity. Define the exact structural revalidation consequences before any implementation.

### C4 — Other

Permitted only with explicit rationale and equally strict preservation of the accepted invariants.

## Required decision

Choose one canonical contract class.

State explicitly:

```text
Frozen structural runner defect = YES/NO
Prior W5 authority contract defect = YES/NO
Runner byte change required = YES/NO
New runner hash required = YES/NO
New validator hash required = YES/NO
Fresh SV01-SV36 required = YES/NO
Separate W5 harness permitted = YES/NO
Separate W5 harness identity/hash required = YES/NO
Pre-W5 structural validation of harness required = YES/NO
W5 currently authorized = YES/NO
W5 RunId allocation currently authorized = YES/NO
```

## If C2 is selected

Define a narrow implementation authority for Terra to create a **disposable/local-only governed W5 runtime harness outside tracked repository source**, unless exact evidence requires a different location.

The harness must not duplicate policy. It must mechanically implement/consume the frozen contract necessary to:

```text
verify exact runner identity
verify exact production-wrapper source identity
perform G01-G11 before RunId
allocate exactly one fresh W5 RunId only after gates
create exact-byte disposable wrapper copy
provide only minimum governed interceptions/shims
invoke the real production wrapper
record external-call ledger
record wrapper/lifecycle observations
perform cleanup
hand evidence to independent validator/finalizer
```

Define whether the existing validator can validate the harness identity/structure or whether a validator-only extension and fresh structural cycle is required.

Do not authorize actual W5 execution in this Luna decision artifact.

## If C3 is selected

Define the minimum runner-interface addition and require:

```text
new runner hash
fresh validator compatibility determination
fresh complete SV01-SV36
fresh Luna structural reconciliation
```

before W5 execution can be authorized again.

Do not implement the runner change here.

## Required terminal markers

If reconciliation reaches a complete decision, emit:

```text
RELEASE 1.12 WP04 — LUNA W5 RUNNER CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — W5 RUNNER/CONTRACT AMBIGUITY: RESOLVED
RELEASE 1.12 WP04 — CANONICAL W5 EXECUTION CONTRACT: <C1|C2|C3|C4>
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then state the exact next authority required.

If evidence is insufficient to decide, emit:

```text
RELEASE 1.12 WP04 — LUNA W5 RUNNER CONTRACT RECONCILIATION: FAIL
RELEASE 1.12 WP04 — W5 RUNNER/CONTRACT AMBIGUITY: UNRESOLVED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

and identify the exact missing evidence.

## Required handoff

Return:

```text
RunnerSHA256Observed
ValidatorSHA256Observed
RunnerEntryPointsObserved
ValidateOnlySemantics
NormalEntryPointSemantics
GuardSourceLocation
G01-G11Ownership
ExactByteProtectionOwnership
ChildVisibilityProtectionOwnership
ExternalCallProtectionOwnership
P19Ownership
P20Ownership
SelectedContractClass
FrozenStructuralRunnerDefect
PriorW5AuthorityContractDefect
RunnerByteChangeRequired
NewRunnerHashRequired
NewValidatorHashRequired
FreshSV01SV36Required
SeparateW5HarnessPermitted
SeparateW5HarnessIdentityRequired
PreW5HarnessStructuralValidationRequired
GovernedW5ExecutionCurrentlyAuthorized
GovernedW5RunIdCurrentlyAuthorized
ExactMutationAccounting
NextAuthorityRequired
```

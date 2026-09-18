# Release 1.12 WP04 — Terra W5 Runtime Interception Converge + Execute

**Selected execution model: GPT-5.6 Terra**

## Objective

Continue the single-converge workflow. C2 structural convergence is complete, but W5 execution is correctly blocked because the future route lacks an executable disposable local interception environment for the copied production wrapper's reachable `git` / `az` / helper calls.

Construct that W5 runtime interception boundary entirely within disposable/local-only scope, then invalidate/rebuild structural evidence as required, exhaustively re-prove C2, and proceed directly through W5 in this same Codex execution.

Do not return after merely constructing the interception harness.

## Current structural anchor

```text
DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\d28fc11136b04d5aa0fc1924d723e57f

RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
F0A69A2AF8DD112FABBFA85CCB6BA0855BAE648058F4CF46981ED8D10E741EAA

ValidatorSHA256 =
32FC1234F3BF34AE5E2F7D06E875FC18E066C03356292205F7C7A946D031AC17
```

Derive the current child hash from retained bytes.

Current structural status:

```text
R01-R14 = ALL_PASS
R07 = PASS
R13 = PASS
R14 = PASS
SV01-SV36 = 36/0
structural W5 RunIds = 0
structural wrapper invocations = 0
structural real external calls = 0
```

This root is structurally accepted evidence but becomes superseded for W5 authorization if any harness/validator/child byte changes.

## Identified blocker

The explicit future W5 route is:

```text
Invoke-SharedPreRunIdOrchestration
→ Invoke-GovernedW5AfterPreRunIdGates
→ fresh RunId allocation
→ copied production wrapper invocation
```

The copied wrapper currently lacks an executable local interception environment guaranteeing that its reachable `git`, `az`, and helper process invocations cannot escape to real executables/services.

Running W5 without that boundary is FORBIDDEN.

## Authorized remediation scope

Byte-changing remediation is allowed only in disposable/local-only:

```text
W5 runtime harness
C2 harness if needed to bind/test W5 interception
validator
child probe if needed
temporary executable shadows/shims
temporary sandbox
durable sanitized local evidence/ledgers
```

Do NOT change:

```text
frozen runner
tracked/production source
production wrapper/helper
Git/GitHub
Azure
Docker/GHCR
Twelve Data
secrets contract
W6/W7/W8
publication/lifecycle
```

No real external call is authorized.

## Runtime interception invariant

Before W5 RunId allocation, establish executable proof of:

```text
ProductionReachableExternalSurface
==
W5RuntimeGovernedInterceptionSurface
==
W5RuntimeExecutedApprovedProbeSurface
```

The runtime environment must ensure the copied production wrapper resolves all reachable external commands to governed disposable interception.

At minimum cover the known production surface:

```text
git:
- rev-parse HEAD
- merge-base --is-ancestor ...
- ls-files --error-unmatch -- <wrapper>
- diff --quiet HEAD -- <wrapper>

az:
- webapp show — image query
- webapp show — state query
- webapp config appsettings list
- webapp log download --log-file <governed evidence-root path>

helper:
- Deferred / LifecycleAction None / RestorationMode Deferred
- RestoreOnly with exact preceding RestorationDescriptor
```

If fresh production-source inspection discovers another reachable shape, it is mandatory.

## Interception mechanics

Use a disposable local execution environment that makes real process escape impossible.

Requirements:

```text
1. Exact approved command/argument allowlist.
2. Unsupported or altered shape fails closed.
3. No fallback to real git/az/helper.
4. Child/copy execution sees the interception environment.
5. PATH/command resolution is explicitly proven.
6. Workspace helper resolution is explicitly proven.
7. Real executable escape probes are blocked.
8. Call ledger records every intercepted attempt.
9. Unexpected calls are retained and fail the attempt.
10. Cleanup/restoration is deterministic.
```

Do not implement production policy in the shim. It may emulate only the minimum external behavior required to drive the production wrapper through the canonical W5 retrieval-failure scenario.

## Helper descriptor semantics

Preserve:

```text
Deferred emits non-empty sanitized opaque descriptor
descriptor correlated to synthetic attempt identity + RG + app
RestoreOnly accepts exact preceding descriptor
same attempt/RG/app required
one-time use
replay rejected
altered/fabricated/cross-attempt/mismatched RG/app rejected
```

## Canonical W5 emulation outcome

The governed interception must drive production-derived wrapper behavior to:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycle = SUCCESS
RestoreOnly = exactly 1
RealExternalCalls = 0
```

Do not hard-code wrapper P06-P10 PASS claims. The production wrapper must derive those outcomes from intercepted external behavior and its own production logic.

## Required runtime interception probes before RunId

Before any W5 RunId allocation, execute approved and negative probes proving the runtime boundary.

Require approved probes for every production-reachable shape.

Require negative probes at least equivalent to the accepted C2 matrices:

```text
Git negatives = 5
Azure negatives = 10
Helper negatives = 10
```

Every negative must prove:

```text
Rejected=true
RealProcessSelected=false
RealExternalCallDelta=0
DurableEventRetained=true
```

Also explicitly probe:

```text
real git escape
real az escape
workspace-helper escape
PATH fallback escape
child-process inheritance of interception environment
```

All must be blocked.

## Byte-change invalidation and complete structural rerun

Any harness/validator/child/W5-runtime-harness byte change invalidates the prior candidate for W5.

After constructing/fixing interception:

```text
recompute all hashes
→ create fresh durable root
→ restart complete C2 structural cycle
→ evaluate ALL R01-R14
→ perform independent adversarial reconciliation
```

Do not carry forward `d28fc111...` acceptance merely by assertion.

Require again:

```text
R01-R14 = ALL_PASS
SV01-SV36 = 36/0
53 event-specific named mappings
G01-G11 = 11/11
RunId sites fully covered
wrapper sites fully covered
11/11 durable corruption fixtures
three-way structural external-surface equality
zero structural W5 RunIds
zero structural wrapper invocations
zero real external calls
zero authority-introduced tracked changes
staged paths = 0
git diff --check = PASS
```

## Convergence loop

Do not return for ordinary harness/validator/runtime-interception defects.

Loop:

```text
evaluate ALL structural + interception gates
→ collect ALL defects
→ fix every disposable/local-only defect
→ rehash
→ fresh root
→ rerun complete structural cycle
→ adversarially reconcile ALL R01-R14
→ repeat until ALL_PASS
```

STOP only if correction requires a forbidden governance-boundary crossing.

## W5 pre-RunId gate

Only after fresh structural R01-R14 ALL_PASS, reverify:

```text
frozen runner exact
fresh harness/validator/child/runtime hashes exact
WinPS = 5.1.26100.9444
parser = 0
source/pre exact-byte ready
runtime interception installed
approved runtime surface exact
negative runtime probes all pass
real external escape count = 0
unexpected-call count = 0
repo mutation scope unchanged
staged paths = 0
```

Then and only then allocate one fresh, never-used W5 RunId.

Every allocated W5 RunId is permanently single-use.

## Execute W5

Execute the copied production wrapper through the governed runtime boundary.

Require canonical P01-P20 exactly:

```text
P01 WinPS version 5.1.26100.9444
P02 parser 0 errors
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 EvidenceCheckpoint PASS
P09 Final SUCCESS
P10 RestoreOnly 1
P11 real external calls 0
P12 external ledger only governed intercepted calls
P13 repo modified scope unchanged
P14 staged 0
P15 git diff --check PASS
P16 secret hygiene PASS
P17 durable sanitized evidence retention
P18 complete durable W5 scenario ledger retention
P19 disposable sandbox cleanup PASS
P20 error precedence PASS
```

Acceptance only:

```text
P01-P20 = ALL_PASS
```

P19 must be evaluated after cleanup. P20 is observation-only.

## W5 retry behavior

If W5 exposes a defect entirely inside disposable harness/validator/runtime interception:

```text
retain failed attempt
→ permanently consume its RunId
→ diagnose ALL current defects
→ repair all local defects
→ invalidate structural tuple
→ rehash/fresh root
→ rerun complete C2 R01-R14
→ rerun runtime interception probes
→ only after ALL_PASS allocate a new fresh RunId
→ retry W5
```

Continue until P01-P20 ALL_PASS.

Do not reuse any W5 RunId.

## Absolute stop conditions

STOP if any required correction/action needs:

```text
frozen runner mutation
tracked/production source mutation
new Luna architecture/policy decision
real external invocation
Azure call/mutation
GitHub mutation
Docker/GHCR call/mutation
Twelve Data call/configuration
secret-contract change
W6/W7/W8
publication/lifecycle mutation
```

If stopped, report ALL known blockers.

## Required success markers

Only after fresh structural acceptance + runtime interception acceptance + W5 acceptance:

```text
RELEASE 1.12 WP04 — W5 RUNTIME INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 R01-R14: ALL_PASS
RELEASE 1.12 WP04 — C2 SV01-SV36: 36/0
RELEASE 1.12 WP04 — W5 RUNTIME THREE-WAY EXTERNAL-SURFACE EQUALITY: PASS
RELEASE 1.12 WP04 — W5 RUNTIME NEGATIVE MATRIX: ALL_PASS
RELEASE 1.12 WP04 — W5 REAL EXTERNAL ESCAPES: 0
RELEASE 1.12 WP04 — GOVERNED W5 RUNID: FRESH_SINGLE_USE
RELEASE 1.12 WP04 — W5 P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. Do not execute W6/W7/W8 or publication/lifecycle.

## Required consolidated handoff

Return:

```text
StructuralRetryAttemptCount
StructuralSupersededRoots
FinalStructuralDurableRoot

RunnerSHA256
HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
W5RuntimeHarnessSHA256
WindowsPowerShellVersion
ParserErrorCounts

R01-R14FinalMatrix
SVRecordCount
SVFailedCount
ResolvedSVEvidenceCount

ProductionReachableExternalSurfaceInventory
W5RuntimeGovernedInterceptionSurfaceInventory
W5RuntimeExecutedApprovedProbeSurfaceInventory
W5RuntimeThreeWayEquality

RuntimeApprovedProbeResults
RuntimeGitNegativeResults
RuntimeAzureNegativeResults
RuntimeHelperNegativeResults
RuntimeEscapeProbeResults
RuntimeRealExternalEscapeCount

W5AttemptCount
ConsumedFailedW5RunIds
AcceptedW5RunId
W5DurableEvidenceRoot

P01-P20Results
W5AllPass
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
RealExternalCallCount

PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckResult
SecretHygieneResult
ExactMutationAccounting

BoundaryBlocker
NextAuthorizedAction
```

On success:

```text
NextAuthorizedAction=GPT-5.6 Luna final W5 acceptance reconciliation / W6 sequencing authority
```

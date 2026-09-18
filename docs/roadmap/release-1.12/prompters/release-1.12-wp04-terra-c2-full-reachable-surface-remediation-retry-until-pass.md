# Release 1.12 WP04 — Terra C2 Full Reachable External-Surface Remediation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate the confirmed disposable C2 harness + validator contract gap so the governed pre-RunId interception boundary covers the **entire reachable external-call surface** of the production wrapper, including the mandatory `finally` / `RestoreOnly` helper call.

Then run a fresh complete structural validation cycle under one final harness/validator pair.

Within disposable/local harness and validator scope, **keep correcting and retrying until PASS**. Do not stop for another authority merely because an additional in-scope omission or validation defect is discovered.

Stop only when correction would cross a mandatory governance boundary defined below.

## Binding Luna reconciliation

```text
RELEASE 1.12 WP04 — LUNA C2 FULL HELPER-SURFACE CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 RESTOREONLY HELPER-SURFACE GAP: CONFIRMED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL-SURFACE VALIDATION: REQUIRED
RELEASE 1.12 WP04 — C2 HARNESS/VALIDATOR REMEDIATION: REQUIRED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
```

Decisions:

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

## Frozen runner

MUST remain byte-identical:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

## Superseded accepted candidate

Historical only after byte changes:

```text
HarnessSHA256 =
D14E0D634203FB8121564513669DD4B1743F91E0D3F71445644BF1B5CD9ECB27

ValidatorSHA256 =
451FC907DA208462DBCEAE56D504FDBB83D6D143CF68C5EDD0F46AFDE313F867
```

Do not compose PASS evidence across hashes.

## PowerShell baseline

```text
Windows PowerShell = 5.1.26100.9444
```

No PowerShell 7-only syntax or assumptions.

# Canonical reachable external-call inventory

The validator and harness must bind to this complete production-wrapper inventory and independently verify it against exact source.

## git

```text
git rev-parse HEAD
git merge-base --is-ancestor <expected-ancestor> <candidate/head>
git ls-files --error-unmatch -- <wrapper>
git diff --quiet HEAD -- <wrapper>
```

Exact dynamic values must be constrained by the governed structural/runtime context.

## az

```text
image webapp show
state webapp show
app-settings list
webapp log download --log-file <exact evidence-root>/raw-app-service-logs.zip
```

Use normalized exact contracts. No broad wildcard command matching.

Dynamic log-file validation must require the exact governed destination under the current evidence root after canonical path normalization.

## helper — Deferred

Exact production shape:

```text
-ResourceGroup <governed RG>
-WebAppName <governed app>
-Phase initialize
-LifecycleAction None
-RestorationMode Deferred
```

## helper — RestoreOnly

Exact production shape:

```text
-ResourceGroup <same governed RG>
-WebAppName <same governed app>
-RestorationMode RestoreOnly
-RestorationDescriptor <exact descriptor emitted by governed Deferred shadow>
```

The RestoreOnly call is mandatory because the production wrapper reaches it through the outer `finally`, including failure paths.

## Local-only operations

Archive expansion, filesystem evidence writes, JSON conversion, and callbacks are local operations and are not external-process escapes.

The validator must still confirm that no additional executable/script/process surface is reachable beyond the complete inventory above.

# Required correction A — two-shape shadow helper

Replace the Deferred-only shadow-helper contract with an exact two-shape contract.

The shadow helper must accept parameters sufficient to bind both exact production call shapes, including:

```text
RestorationDescriptor
```

It must reject any unsupported shape.

## Deferred behavior

For the exact Deferred call:

1. validate ResourceGroup/WebAppName against the governed synthetic execution context;
2. require:
   ```text
   Phase = initialize
   LifecycleAction = None
   RestorationMode = Deferred
   RestorationDescriptor absent
   ```
3. emit a non-empty sanitized opaque descriptor;
4. bind the descriptor to:
   ```text
   synthetic probe/attempt identity
   governed ResourceGroup
   governed WebAppName
   preceding Deferred invocation
   ```
5. durably ledger descriptor identity in sanitized form;
6. do not implement real restoration policy or external mutation.

## RestoreOnly behavior

For the exact RestoreOnly call:

1. require:
   ```text
   same governed ResourceGroup
   same governed WebAppName
   RestorationMode = RestoreOnly
   RestorationDescriptor present
   Phase absent/unset unless exact production binding proves otherwise
   LifecycleAction absent/unset unless exact production binding proves otherwise
   ```
2. accept only the exact descriptor previously emitted by the corresponding Deferred shadow;
3. require descriptor correlation to the same synthetic probe/attempt identity;
4. require one-time use;
5. reject:
   ```text
   missing descriptor
   empty descriptor
   altered descriptor
   arbitrary descriptor
   descriptor from another probe/attempt
   descriptor from another RG/app
   second use/replay
   unsupported extra parameter shape
   ```
6. durably ledger the governed RestoreOnly observation;
7. return only the minimum governed local result required by the production wrapper;
8. do not reproduce Azure restoration/lifecycle policy.

# Required correction B — finally-path copied-wrapper-scope proof

The structural probe must prove the RestoreOnly shape from the same copy-equivalent child scope used to prove runtime helper resolution.

Require:

```text
child scope current location = SandboxRoot
relative helper resolves to sandbox shadow helper
Deferred shape succeeds
descriptor emitted
RestoreOnly with exact descriptor succeeds
descriptor correlation succeeds
descriptor one-time use enforced
workspace real helper never resolves
working location restored after probe
```

Direct harness-only helper invocation is insufficient.

# Required correction C — negative helper probes

Before any future RunId allocation prove fail-closed behavior for:

```text
RestoreOnly without descriptor
RestoreOnly with empty descriptor
RestoreOnly with modified descriptor
RestoreOnly with arbitrary descriptor
RestoreOnly with descriptor from another synthetic identity
RestoreOnly with mismatched ResourceGroup
RestoreOnly with mismatched WebAppName
RestoreOnly replay / second use
unsupported RestorationMode
unexpected helper parameter shape
```

All must fail locally without reaching the real workspace helper.

# Required correction D — full reachable-surface equality

The validator must independently derive or inspect the production wrapper's reachable external-call inventory and compare it to the harness allowlist/interception inventory.

Require:

```text
ProductionReachableExternalSurface == HarnessGovernedInterceptionSurface
```

A subset is FAIL.

An unexplained superset is FAIL.

The comparison must cover:

```text
nominal path
failure path
finally path
restoration path
```

Require summary predicate:

```text
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
```

# Required correction E — preserve previous scope-faithful protections

Do not regress:

```text
Push-Location $SandboxRoot
try/finally location restoration
copy-equivalent child-scope proof
git function interception
az function interception
relative helper shadow resolution
workspace helper escape blocking
exact normalized az contracts
dynamic log-file exact-path validation
unexpected git/az/helper fail-closed behavior
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
```

# Strengthened structural probes

Freshly prove at least:

```text
F01 complete production external-surface inventory
F02 complete harness interception inventory
F03 inventory equality
F04 Deferred exact helper shape accepted
F05 Deferred emits non-empty sanitized opaque descriptor
F06 descriptor correlated to synthetic identity/RG/app
F07 RestoreOnly exact helper shape accepted
F08 RestoreOnly exact descriptor correlation accepted
F09 descriptor replay rejected
F10 altered/arbitrary descriptor rejected
F11 mismatched identity/RG/app rejected
F12 finally-path helper resolution stays in sandbox
F13 workspace helper escape blocked
F14 exact git surface covered
F15 exact az surface covered
F16 no wildcard az/helper contracts
F17 no additional reachable external surface omitted
F18 cleanup/location restoration complete
```

Require:

```text
F01-F18 = ALL_PASS
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
```

These are structural predicates, not W5 P01-P20 claims.

# I01-I09

Re-run the established executable-interception probes under the final tuple and strengthen helper coverage so I03/I06/I07 evidence includes the complete Deferred + RestoreOnly surface where applicable.

Require:

```text
I01-I09 = ALL_PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

# Fresh SV01-SV36

Run all canonical SV01-SV36 from zero under the final tuple.

The corrected validator must make the full-surface equality and finally-path helper proof acceptance-relevant, especially through SV16/SV27/SV28/SV29 and the summary predicates.

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

# Pre-RunId future ordering

Structurally prove:

```text
identity/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ executable interception installation
→ SandboxRoot working-directory binding
→ full reachable-surface inventory/equality validation
→ Deferred/RestoreOnly descriptor probes
→ exact git/az/helper contract probes
→ unexpected-call probes
→ real-command/helper escape probes
→ C2_EXECUTABLE_INTERCEPTION_BOUNDARY PASS
→ C2_FULL_REACHABLE_EXTERNAL_SURFACE PASS
→ only then future W5 RunId allocation
→ only then future copied-wrapper invocation
```

This authority MUST stop before the final two actions.

# Retry-until-PASS protocol

If any defect is discovered that is correctable wholly within disposable/local harness or validator scope:

```text
classify
→ correct
→ recompute changed hashes
→ supersede all prior candidate PASS evidence
→ create a fresh durable structural root
→ restart the complete structural cycle
→ keep trying
```

Do not return merely because another harness/validator omission is found.

During each retry, re-inventory the complete wrapper external surface so multiple omissions are corrected in the same authority.

## Mandatory stop boundary

STOP if correction requires:

```text
frozen runner change
production wrapper change
tracked-source change
change to either pre-existing modified WP04 script
new architecture/policy decision outside this contract
real external process/service invocation
Azure/GitHub/Docker/GHCR/Twelve Data mutation
staging/commit/push
W5 RunId allocation
governed W5 wrapper execution
W6/W7/W8
publication/lifecycle closure
```

# Runtime exclusions

Across every retry:

```text
W5 RunId allocations = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
```

# Finalization

For the final successful candidate require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

# Repository invariants

Only these two pre-existing modified tracked paths may remain:

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

# Durable evidence

The final fresh durable root must retain:

```text
final runner/harness/validator copies + hashes
production reachable-surface inventory
harness interception inventory
inventory equality report
exact git allowlist
exact az allowlist
exact Deferred helper contract
exact RestoreOnly helper contract
descriptor correlation evidence
descriptor negative/replay evidence
finally-path child-scope evidence
F01-F18 evidence
I01-I09 evidence
C2_EXECUTABLE_INTERCEPTION_BOUNDARY evidence
C2_FULL_REACHABLE_EXTERNAL_SURFACE evidence
SV01-SV36 ledger
36/36 evidence manifest/resolution
checkpoint evidence
repository invariants
retry history
```

No secrets in durable evidence.

# PASS markers

Only after one final single-hash tuple passes all gates:

```text
RELEASE 1.12 WP04 — TERRA C2 FULL REACHABLE EXTERNAL-SURFACE REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 DEFERRED/RESTOREONLY HELPER CONTRACT: PASS
RELEASE 1.12 WP04 — C2 FINALLY-PATH HELPER INTERCEPTION: PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 FINAL HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FINAL VALIDATOR IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 FULL-SURFACE FINAL RECONCILIATION: YES
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
HistoricalHarnessSHA256
HistoricalValidatorSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededCandidateHashes
DurableRoot
ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionInventory
SurfaceInventoryEqualityResult
DeferredHelperContract
RestoreOnlyHelperContract
RestorationDescriptorShape
DescriptorCorrelationResult
DescriptorSingleUseResult
DescriptorNegativeProbeResults
FinallyPathChildScopeResult
WorkspaceHelperEscapeResult
ExactGitContractResult
ExactAzContractResult
WildcardContractCount
F01
F02
F03
F04
F05
F06
F07
F08
F09
F10
F11
F12
F13
F14
F15
F16
F17
F18
FullSurfaceProbeAggregate
C2_FULL_REACHABLE_EXTERNAL_SURFACE
I01
I02
I03
I04
I05
I06
I07
I08
I09
InterceptionProbeAggregate
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
PreRunIdOrderingResult
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
RuntimePredicatePassClaims
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
CanonicalCheckpointCounts
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
DisposableRootAfterCleanup
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

A Terra PASS does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Full Reachable External-Surface Final Reconciliation
```

Only that Luna PASS may authorize a fresh W5 runtime authority and new RunId allocation.

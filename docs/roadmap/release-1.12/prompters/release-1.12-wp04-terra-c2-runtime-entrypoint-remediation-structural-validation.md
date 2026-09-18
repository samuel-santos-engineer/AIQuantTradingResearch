# Release 1.12 WP04 — Terra C2 Runtime Entrypoint Remediation & Fresh Structural Validation

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate the confirmed C2 harness runtime-entrypoint defect, update the independent validator to prove top-level runtime reachability, and perform one complete fresh structural validation cycle.

This authority does **not** authorize governed W5 execution or W5 RunId allocation.

Do not modify the frozen structural runner, tracked production source, Git/GitHub state, Azure, Docker/GHCR, production, or external services.

## Binding Luna decision

```text
RELEASE 1.12 WP04 — LUNA C2 RUNTIME ENTRYPOINT CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FROZEN HARNESS RUNTIME CAPABILITY: NOT_ACCEPTED
RELEASE 1.12 WP04 — C2 RUNTIME ENTRYPOINT DEFECT: CONFIRMED
RELEASE 1.12 WP04 — C2 HARNESS REMEDIATION: REQUIRED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: NOT_AUTHORIZED
```

Confirmed classification:

```text
Frozen runner defect = NO
Frozen C2 harness defect = YES
Prior C2 structural-validation contract defect = YES
Production-wrapper defect = NO
Harness byte change required = YES
New HarnessSHA256 required = YES
Validator byte change required = YES
New ValidatorSHA256 required = YES
Fresh SV01-SV36 required = YES
Fresh Luna C2 reconciliation required = YES
```

## Historical identities

Frozen runner — immutable:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Superseded harness after remediation begins:

```text
7056320ED8D2B2F1C1EABB3E6CEE838791FBB1DFA1F67E09620887130E6F8926
```

Superseded validator after remediation begins:

```text
3947E35A1062E577D59256A29D40B2573C433363E8538B39FC721E47628B2F93
```

Preserve historical artifacts for audit. Do not grant new PASS credit from their hashes.

## PowerShell target

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only syntax, cmdlets, or runtime assumptions.

## Harness remediation — narrow scope

Add an explicit, approved **top-level governed runtime entry point** to the disposable/local-only C2 harness.

The existing structural-probe path must remain available for structural validation.

The runtime path must be explicit at top-level dispatch. It must not require dot-sourcing the harness or directly invoking an internal function from an external side path.

Use a clear runtime switch/mode whose semantics are structurally inspectable.

The top-level runtime orchestration must reach, in order:

```text
1. verify frozen RunnerSHA256
2. verify expected HarnessSHA256/execution identity as applicable
3. verify production-wrapper source identity
4. create exact-byte disposable production-wrapper copy
5. execute G01-G11
6. assert source/copy byte identity
7. only after all pre-RunId gates, allocate exactly one fresh W5 RunId
8. install only the approved minimum git/az/helper interceptions
9. validate required S/W child visibility
10. invoke the copied real production wrapper exactly once
11. record raw external-call/lifecycle observations
12. persist durable pre-cleanup raw evidence
13. perform disposable cleanup
14. hand raw evidence to the independent validator/finalizer
```

### Critical structural-validation prohibition

During this authority:

```text
actual governed W5 RunId allocation = 0
actual production-wrapper invocation = 0
real external calls = 0
```

Reachability/order must be proven with structural probes/sentinels/fault injection that stop before those runtime actions.

## Runtime guard semantics

Replace the prior unconditional non-probe guard with dispatch that distinguishes:

```text
approved structural validation mode
approved governed runtime mode
everything else → fail closed
```

Do not create a generic arbitrary-function invocation interface.

## Policy ownership

The harness remains execution mechanics only.

It must not manufacture or independently decide:

```text
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnly result
P01-P20 PASS/FAIL
```

It may retain raw observations required for independent validation.

P19 remains validator-owned after actual cleanup.

P20 remains validator observation-only.

## Minimum interception boundary

Runtime reachability must include installation of only the already-approved minimum governed interception surface needed for W5:

```text
git
az
required existing helper invocation(s)
```

Expected call shapes must be explicit.

Unexpected calls must fail closed and be durably observable.

No broad wildcard interception.

## Validator remediation

Update the independent validator narrowly so structural PASS requires **reachable top-level runtime orchestration**, not merely the presence of disconnected functions.

The validator must prove from the approved top-level runtime entry point:

```text
runtime orchestration reachable
RunnerSHA256 verification reachable
production-wrapper identity/copy operations reachable
G01-G11 reachable and ordered before RunId
source/copy equality reachable before RunId
RunId allocation reachable only after all pre-RunId gates
minimum shim installation reachable before wrapper invocation
S/W validation and child visibility reachable before wrapper invocation
production-wrapper invocation reachable exactly once after RunId
raw evidence checkpoint reachable after wrapper execution
cleanup reachable after execution
validator/finalizer handoff reachable after cleanup
```

A defined-but-uncalled internal function is a structural failure.

## Reachability acceptance predicate

Do not renumber the canonical SV01-SV36.

Strengthen the applicable executable/runtime-binding SV evidence to include top-level reachability.

Additionally create one explicit non-runtime structural summary predicate:

```text
C2_TOP_LEVEL_RUNTIME_REACHABILITY
```

Required result:

```text
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

This predicate is in addition to SV01-SV36 and does not count as a P01-P20 runtime PASS claim.

Its evidence must identify:

```text
top-level runtime dispatch source location
reachable call chain
G01-G11 position
RunId allocation position
shim installation position
wrapper invocation position
evidence checkpoint position
cleanup position
validator handoff position
probe stop point proving zero runtime execution
```

## Fresh identities

Any harness byte change requires a new:

```text
HarnessSHA256
```

Any validator byte change requires a new:

```text
ValidatorSHA256
```

After the final byte change, freeze both.

No PASS carry-forward across earlier remediation hashes.

## Fresh complete SV01-SV36

Run all canonical checks from SV01 using exactly:

```text
RunnerSHA256 = 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
HarnessSHA256 = <new final hash>
ValidatorSHA256 = <new final hash>
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
C2_TOP_LEVEL_RUNTIME_REACHABILITY = PASS
```

Each SV record retains exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

## Canonical SV01-SV36 semantics

Preserve unchanged:

```text
SV01 exact WinPS 5.1.26100.9444
SV02 parser 0
SV03 exactly P01-P20
SV04 predicate fields complete
SV05 aggregation membership
SV06 unresolved fail closed
SV07 zero runtime PASS claims
SV08 source wrapper SHA operation
SV09 byte-for-byte copy
SV10 copy-pre SHA
SV11 pre-RunId equality assertion
SV12 source/copy post hashes
SV13 post equality
SV14 mismatch fail closed
SV15 G01-G11 before RunId
SV16 G01-G11 before wrapper
SV17 RunId before wrapper
SV18 executable S validation
SV19 executable W validation
SV20 child S visibility
SV21 child W visibility
SV22 child mismatch fail closed
SV23 P03 binding
SV24 P04 binding
SV25 P19 ordering
SV26 P20 observation-only
SV27 minimum shims
SV28 unexpected calls fail closed
SV29 no policy duplication
SV30 secret hygiene
SV31 roots independent
SV32 disposable cleanup
SV33 runner survives
SV34 ledger survives
SV35 hash reverified
SV36 repo invariants
```

For SV08-SV28 where relevant, evidence must now prove reachability from the approved top-level runtime dispatch, not merely function existence.

## Durable evidence

Use a new durable root independent from historical structural roots.

Retain exact:

```text
frozen runner bytes/hash
new frozen harness bytes/hash
new frozen validator bytes/hash
top-level dispatch source evidence
runtime reachability call-chain evidence
C2_TOP_LEVEL_RUNTIME_REACHABILITY evidence
G01-G11 reachability/order evidence
RunId reachability/order evidence
shim reachability evidence
wrapper-invocation reachability evidence
checkpoint/cleanup/handoff reachability evidence
all 36 SV evidence artifacts
evidence manifest
resolution report
final SV ledger
repo-state evidence
git diff --check output
```

All evidence references must resolve after disposable cleanup.

## Finalizer checkpoints

Require exactly once and in order:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

## Repository invariants

Only these two pre-existing tracked modifications may remain:

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

## Mutation exclusions

Forbidden:

```text
tracked-source edits
staging
commit
push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
production mutation
real external calls
actual W5 RunId allocation
actual W5 wrapper invocation
W6/W7/W8
publication
WP04 lifecycle closure
```

## Correction loop

Local-only harness/validator defects discovered during this authority may be corrected.

Every correction that changes bytes invalidates the prior candidate hash.

After the final correction:

```text
new fresh structural root
complete SV01-SV36 from SV01
fresh C2_TOP_LEVEL_RUNTIME_REACHABILITY
no cross-hash composition
```

The frozen runner must never change.

## PASS terminal markers

Only after the final single-hash cycle passes:

```text
RELEASE 1.12 WP04 — TERRA C2 RUNTIME ENTRYPOINT REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 TOP-LEVEL RUNTIME REACHABILITY: PASS
RELEASE 1.12 WP04 — C2 REMEDIATED HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 REMEDIATED VALIDATOR IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 RUNTIME RECONCILIATION: YES
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP.

## Required handoff

Return:

```text
RunnerSHA256
HistoricalHarnessSHA256
HistoricalValidatorSHA256
FinalHarnessSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
RunnerParserErrorCount
HarnessParserErrorCount
ValidatorParserErrorCount
TopLevelRuntimeEntrypoint
TopLevelRuntimeEntrypointSourceLocation
DefaultFailClosedGuardResult
RuntimeReachabilityResult
RuntimeReachableCallChain
G01-G11ReachabilityResult
RunIdReachabilityOrderingResult
ShimInstallationReachabilityResult
ChildVisibilityReachabilityResult
WrapperInvocationReachabilityResult
WrapperInvocationCardinalityStructuralResult
CheckpointReachabilityResult
CleanupReachabilityResult
ValidatorHandoffReachabilityResult
C2_TOP_LEVEL_RUNTIME_REACHABILITY
StructuralProbeStopPoint
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
DurableRoot
CanonicalCheckpointCounts
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
FinalLedgerReopenParseResult
DisposableRootAfterCleanup
DurableHarnessCopyResult
DurableValidatorCopyResult
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
GitDiffCheckOutput
CorrectionsApplied
SupersededHarnessSHA256s
SupersededValidatorSHA256s
ExactMutationAccounting
FinalStructuralResult
FirstBlockingDefect
NextAuthorityRequired
```

## Next gate

A Terra PASS does not authorize W5.

The next authority must be a fresh **GPT-5.6 Luna C2 Remediated Runtime Structural Reconciliation** bound to the final:

```text
RunnerSHA256
HarnessSHA256
ValidatorSHA256
DurableRoot
```

Only that Luna reconciliation may authorize another fresh governed W5 execution and RunId allocation.

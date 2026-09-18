# Release 1.12 WP04 — Terra C2 Validator Final-Ledger Summary Continuation — Retry Until PASS

**Selected execution model: GPT-5.6 Terra**

## Authority

Continue the already-authorized C2 full reachable external-surface remediation.

The latest structural attempt proved the functional contract but is **not acceptance-complete** because the durable final ledger omitted the mandatory summary field:

```text
C2FullReachableExternalSurface = PASS
```

Correct this **validator-only finalization/serialization defect**, recompute the validator hash, supersede the incomplete candidate evidence, allocate a fresh durable structural root, and rerun the complete required structural cycle from zero.

Within the already-authorized local validator/harness scope, keep correcting and retrying until one final single-hash tuple produces a complete durable PASS ledger.

Do not cross W5 or any production/external governance boundary.

## Binding latest incomplete candidate

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
F80C9F8C9D4EB0570AEDE97F5A77B46DF4B34B6C15F20CFC16A3BC7183381915

IncompleteValidatorSHA256 =
9585718C1C1D81094615EC470368422CA70DDC94829AA16821E0B2245DE70D79

IncompleteAttemptDurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\83150fb419454aa1b89ab86e0a3f186a
```

Observed on that incomplete attempt:

```text
SV01-SV36 = 36 unique / 0 failures
evidence references = 36/36 resolved
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
W5 RunId allocated = False
W5 wrapper invoked = False
real external calls = 0
git diff --check = 0
```

These functional results are useful diagnostic evidence only. They do **not** grant final structural acceptance because the required durable summary field was omitted.

## Defect classification

```text
Frozen runner defect = NO
Harness defect currently requiring change = NO
Validator defect = YES
Production-wrapper defect = NO
Tracked-source defect = NO

Required correction = validator-only
```

Do not modify the harness merely to avoid recomputing/revalidating the final tuple.

If fresh validation exposes a genuine harness defect, it remains correctable under the existing retry-until-PASS harness/validator authority, but any harness byte change requires a new harness hash and full restart.

## Required validator correction

Locate the current final-ledger construction/serialization path by semantic structure, not by assuming the previous target line still exists.

The prior textual patch miss is not evidence that the contract changed; inspect the current validator and patch the actual final-ledger object/record construction.

The durable final published ledger must explicitly serialize:

```text
C2FullReachableExternalSurface = PASS
```

The field must be sourced from the actual validated summary predicate. It must not be hard-coded to PASS independently of the predicate.

Require fail-closed behavior:

```text
predicate missing       → finalization FAIL
predicate unresolved    → finalization FAIL
predicate != PASS       → finalization FAIL
serialization omission  → finalization FAIL
reopened ledger missing → finalization FAIL
reopened value != PASS  → finalization FAIL
```

## Serialization round-trip proof

Before granting final structural PASS, prove:

```text
in-memory C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
→ final ledger object contains C2FullReachableExternalSurface
→ serialized durable ledger contains field
→ published ledger contains field
→ reopened durable ledger contains field
→ reopened value exactly PASS
```

Add/retain a validation assertion that makes future omission acceptance-relevant.

## Preserve full-surface contract

Do not weaken or bypass any previously required gate.

Freshly require:

```text
C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
F01-F18 = ALL_PASS
I01-I09 = ALL_PASS
```

The complete production external-surface inventory must still equal the governed harness interception inventory, including:

```text
exact git shapes
exact az shapes
Deferred helper shape
RestoreOnly + RestorationDescriptor shape
finally-path interception
descriptor correlation
descriptor single-use/replay rejection
workspace helper escape blocking
```

## Fresh hashes

After the validator byte change compute:

```text
RunnerSHA256
HarnessSHA256
FinalValidatorSHA256
```

Require runner exact match.

If harness is unchanged, require its exact expected hash:

```text
F80C9F8C9D4EB0570AEDE97F5A77B46DF4B34B6C15F20CFC16A3BC7183381915
```

The validator hash must differ from the incomplete validator hash if bytes changed.

## Fresh durable root

Do not overwrite or promote:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\83150fb419454aa1b89ab86e0a3f186a
```

Retain it as incomplete/superseded evidence.

Create a new durable structural root for the corrected final tuple.

## Fresh complete structural cycle

Because validator bytes change, all preceding structural PASS credit is superseded for acceptance.

Rerun from the beginning:

```text
PowerShell/parser gates
identity/hash gates
complete reachable-surface inventory
harness interception inventory
inventory equality
scope-faithful child probes
Deferred/RestoreOnly descriptor probes
negative/replay probes
exact git contract
exact az contract
F01-F18
I01-I09
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
C2_FULL_REACHABLE_EXTERNAL_SURFACE
SV01-SV36
evidence-reference resolution
finalization
serialization
publication
reopen
repository invariants
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
```

## Finalization checkpoints

Require exactly once and ordered:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

The `REOPEN` validation must explicitly verify:

```text
C2FullReachableExternalSurface exists
C2FullReachableExternalSurface == PASS
```

## Retry-until-PASS rule

If any defect is found that is correctable wholly within disposable/local harness or validator scope:

```text
classify
→ correct
→ recompute all changed hashes
→ supersede prior candidate PASS credit
→ create a fresh durable root
→ restart the complete structural cycle
→ continue until PASS
```

Do not stop after another local validator finalization defect.

### Mandatory stop boundary

STOP if correction requires:

```text
frozen runner modification
production/tracked source modification
new architecture/policy decision outside the accepted full-surface contract
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

Across every retry:

```text
W5 RunId allocations = 0
W5 wrapper invocations = 0
real external calls = 0
runtime P01-P20 PASS claims = 0
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

## PASS markers

Only after the corrected final tuple completes the fresh cycle and the reopened durable ledger contains the required summary:

```text
RELEASE 1.12 WP04 — TERRA C2 FULL REACHABLE EXTERNAL-SURFACE REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 DEFERRED/RESTOREONLY HELPER CONTRACT: PASS
RELEASE 1.12 WP04 — C2 FINALLY-PATH HELPER INTERCEPTION: PASS
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ALL_PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: PASS
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE LEDGER SUMMARY: PASS
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

Then STOP for Luna.

## Required handoff

Return:

```text
RunnerSHA256
HarnessSHA256
IncompleteValidatorSHA256
FinalValidatorSHA256
WindowsPowerShellVersion
ParserErrorCounts
RetryAttemptCount
SupersededDurableRoots
FinalDurableRoot
C2FullReachableExternalSurfaceInMemory
C2FullReachableExternalSurfaceBuildLedger
C2FullReachableExternalSurfaceSerialized
C2FullReachableExternalSurfacePublished
C2FullReachableExternalSurfaceReopened
LedgerSummaryRoundTripResult
ProductionReachableExternalSurfaceInventory
HarnessGovernedInterceptionInventory
SurfaceInventoryEqualityResult
DeferredHelperContractResult
RestoreOnlyHelperContractResult
DescriptorCorrelationResult
DescriptorReplayResult
FinallyPathChildScopeResult
WorkspaceHelperEscapeResult
F01-F18Aggregate
I01-I09Aggregate
C2_EXECUTABLE_INTERCEPTION_BOUNDARY
C2_FULL_REACHABLE_EXTERNAL_SURFACE
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
CanonicalCheckpointCounts
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
ExactMutationAccounting
FinalStructuralResult
FirstBoundaryCrossingDefectIfAny
NextAuthorityRequired
```

## Next gate

A PASS under this continuation still does not authorize W5.

Next authority:

```text
GPT-5.6 Luna — C2 Full Reachable External-Surface Final Reconciliation
```

Only that Luna PASS may authorize a fresh governed W5 runtime.

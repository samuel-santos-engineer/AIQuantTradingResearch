# Release 1.12 WP04 — Terra C2 W5 Harness Implementation & Structural Validation

**Selected execution model: GPT-5.6 Terra**

## Authority

Implement and structurally validate the newly selected **C2 governed W5 runtime harness**.

Canonical architecture:

```text
frozen structural runner
+ independent validator
+ separately frozen disposable/local-only W5 runtime harness
```

This authority does **not** authorize governed W5 execution, W5 RunId allocation, production-wrapper invocation as an actual W5 attempt, Azure/GitHub/Docker/GHCR/external-service mutation, tracked repository mutation, staging, commit, push, W6/W7/W8, or publication.

The objective is to produce:

1. one frozen local-only W5 harness identity/hash;
2. a validator updated only as necessary to structurally validate that harness;
3. one fresh validator identity/hash;
4. complete fresh SV01-SV36 structural evidence for the three-artifact C2 architecture;
5. durable evidence suitable for a new independent Luna reconciliation.

## Accepted C2 decision

Treat as binding:

```text
SelectedContractClass = C2
FrozenStructuralRunnerDefect = NO
PriorW5AuthorityContractDefect = YES
RunnerByteChangeRequired = NO
NewRunnerHashRequired = NO
NewValidatorHashRequired = YES
FreshSV01SV36Required = YES
SeparateW5HarnessPermitted = YES
SeparateW5HarnessIdentityRequired = YES
PreW5HarnessStructuralValidationRequired = YES
GovernedW5ExecutionCurrentlyAuthorized = NO
GovernedW5RunIdCurrentlyAuthorized = NO
```

## Frozen structural runner

The runner remains immutable:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Its role is structural contract/reference identity, not runtime execution.

Do not modify its bytes.

## Prior validator

Historical accepted validator:

```text
E4E3773E480B5CE9A8E15FBB3939BFBAD43B29D7DC3A8D5FE795B9F9C8DD4F0F
```

Because C2 adds a third structurally governed artifact, the validator must receive a new hash after the narrow harness-validation extension.

Do not weaken or reinterpret existing SV semantics.

## PowerShell baseline

```text
Windows PowerShell 5.1.26100.9444
Language/runtime target: Windows PowerShell 5.1
```

All new harness and validator code must parse and operate under this baseline.

## Harness location/lifecycle

Create the W5 runtime harness as a **disposable/local-only artifact outside tracked repository source**.

Requirements:

```text
not tracked
not staged
not committed
not pushed
not installed into production
not published
not reused across incompatible hashes
exact harness bytes retained under DurableRoot for audit/reconciliation
disposable execution copy/root removable
```

The durable retained harness copy is evidence, not repository source.

## Harness role

The harness must mechanically implement the already-frozen execution guarantees necessary for W5 without duplicating production lifecycle policy.

It may:

```text
verify frozen runner SHA256
verify production-wrapper source identity
perform the frozen pre-RunId G01-G11 gates
create exact-byte disposable wrapper copy
verify source/copy hashes before execution
allocate exactly one fresh RunId only after gates — RUNTIME CAPABILITY ONLY, NOT DURING STRUCTURAL VALIDATION
install minimum governed git/az/helper interceptions
fail closed on unexpected external calls
invoke the real production wrapper — RUNTIME CAPABILITY ONLY, NOT DURING STRUCTURAL VALIDATION
record external-call observations
record wrapper/lifecycle observations
perform disposable cleanup
handoff retained evidence to independent validator/finalizer
```

During this authority, structural validation must prove these capabilities and ordering without allocating a real W5 RunId and without invoking the production wrapper as a W5 attempt.

## Forbidden harness behavior

The harness must not:

```text
duplicate production lifecycle policy
reimplement archive/extraction policy
manufacture P01-P20 PASS results
manufacture ArchiveRetrieval/FreshExtraction classifications
manufacture EvidenceCheckpoint or FinalLifecycle
reinterpret P19
inject an error solely to make P20 pass
call real Azure/GitHub/Docker/GHCR/Twelve Data/external services
modify tracked source
modify the frozen runner
silently weaken G01-G11
allocate a W5 RunId during structural validation
invoke the production wrapper during structural validation
```

## Minimum interception boundary

Only the minimum governed interception surface needed by the already-established W5 scenario is permitted.

At minimum evaluate/implement the required local interception for:

```text
git
az
existing helper invocation(s) required by the production wrapper
```

Every expected intercepted call must be explicitly allowlisted by executable shape.

Every unexpected call must fail closed and be durably recorded.

Do not add broad wildcard shims that could hide new external behavior.

## G01-G11 ownership

Use the exact accepted frozen runner as the canonical source for G01-G11 definitions and ordering.

The harness must implement those gates mechanically.

The updated validator must prove structural equivalence between:

```text
frozen runner's G01-G11 contract
and
harness executable G01-G11 behavior/order
```

Do not copy only prose labels. Validate executable predicates/order.

## Exact-byte protections

Structurally prove the harness contains executable behavior for:

```text
source wrapper SHA capture
byte-for-byte disposable copy
copy-pre SHA
source/copy equality before RunId
source/copy post SHA
post-execution equality
fail closed on mismatch
```

The validator must bind P03/P04 to those executable harness operations.

## RunId ordering

Structurally prove:

```text
G01-G11 complete before RunId allocation
source/copy identity assertion complete before RunId allocation
RunId allocation complete before wrapper invocation
```

During structural validation:

```text
actual W5 RunId allocation count = 0
actual production-wrapper invocation count = 0
```

The validator may use isolated structural probes/fault injection that do not create a governed W5 RunId.

## Child visibility

Preserve and structurally validate the frozen contract for child visibility of required `S` and `W` values.

Mismatch must fail closed before production-wrapper execution.

## P19/P20 ownership

Preserve:

```text
P19 = independent validator-owned post-cleanup finalization
P20 = independent validator observation-only
```

The harness may emit raw observations needed by the validator but must not self-award P19/P20 PASS.

## Validator extension

Make only the minimum validator-local changes necessary to bind the C2 harness.

The fresh validator must retain:

```text
plain-dictionary final ledger materialization
JavaScriptSerializer safe input
same-directory temporary ledger
temporary reopen/parse
canonical SERIALIZE
atomic PUBLISH
final REOPEN
36 SV records
seven canonical fields per SV record
durable evidence-reference resolution
```

Extend evidence/metadata to include:

```text
HarnessSHA256
HarnessPathIdentity
HarnessHashAfterCleanup/retention
harness structural validation evidence
runner↔harness contract binding evidence
```

Do not add runtime P01-P20 PASS claims.

## SV01-SV36 fresh validation

Run the complete SV01-SV36 from SV01 under one fresh validator hash and one frozen harness hash.

No cross-hash PASS carry-forward.

The canonical SV definitions remain:

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

Interpret the executable runtime component in these checks as the C2 harness where appropriate, while preserving the frozen runner as the canonical structural reference.

Any semantic change beyond that C2 binding is prohibited.

## SV record contract

Each record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
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

## Evidence retention

Use fresh independent roots.

Retain:

```text
exact frozen runner bytes/hash
exact frozen harness bytes/hash
exact fresh validator bytes/hash
harness source/shape evidence
G01-G11 binding evidence
exact-byte operation evidence
RunId-order structural evidence
wrapper-invocation-order structural evidence
interception allowlist evidence
unexpected-call fail-closed evidence
child-visibility evidence
P19/P20 ownership evidence
canonical checkpoint evidence
all 36 SV evidence artifacts
evidence manifest
resolution report
final sv01-sv36-ledger.json
git diff --check output
repo-state evidence
```

Every EvidenceReference must resolve after disposable cleanup.

## Canonical finalizer checkpoints

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

The only pre-existing tracked modifications remain:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Require:

```text
authority-introduced tracked modifications = 0
pre-existing tracked modified paths = exactly 2
staged paths = 0
git diff --check exit code = 0
```

## Structural validation must not become W5

Hard gate:

```text
GovernedW5RunIdAllocated = NO
ProductionWrapperInvokedAsW5 = NO
RealExternalCalls = 0
```

If a structural probe needs to test ordering, use an isolated sentinel/fault-injection path that terminates before actual RunId allocation/wrapper execution.

## Autonomous validator/harness correction

Ordinary local-only harness or validator defects may be corrected under this authority.

Every byte change requires:

```text
fresh HarnessSHA256 if harness changed
fresh ValidatorSHA256 if validator changed
fresh roots
complete SV01-SV36 from SV01
```

No cross-hash composition.

Do not modify the frozen runner.

## Mandatory blocker boundary

STOP if the next required action is:

```text
frozen runner modification
tracked production-source modification
P01-P20 semantic change
SV01-SV36 semantic change beyond C2 binding
G01-G11 weakening/reinterpretation
P19/P20 reinterpretation
acceptance weakening
real W5 RunId allocation
real W5 wrapper invocation
Azure/GitHub/Docker/GHCR/external-service mutation
Twelve Data secret configuration
```

## PASS terminal markers

Only after one single-hash harness/validator structural run completely passes:

```text
RELEASE 1.12 WP04 — TERRA C2 W5 HARNESS STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — C2 W5 HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 RUNNER/HARNESS CONTRACT BINDING: PASS
RELEASE 1.12 WP04 — C2 SV01-SV36 DURABLE LEDGER: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 STRUCTURAL RECONCILIATION: YES
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP for independent Luna reconciliation.

## Required handoff

Return:

```text
RunnerSHA256
HarnessSHA256
ValidatorSHA256
WindowsPowerShellVersion
RunnerParserErrorCount
HarnessParserErrorCount
ValidatorParserErrorCount
HarnessLocation
HarnessTrackedStatus
HarnessStagedStatus
HarnessContractRole
G01-G11BindingResult
ExactByteBindingResult
RunIdOrderingStructuralResult
WrapperInvocationOrderingStructuralResult
ChildVisibilityStructuralResult
InterceptionAllowlistResult
UnexpectedCallFailClosedResult
P19OwnershipResult
P20OwnershipResult
SupervisorRoot
DurableRoot
SupervisorTerminal
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
RunnerHashAfterCleanup
HarnessHashAfterCleanup
ValidatorHashAfterCleanup
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
GitDiffCheckOutput
RealExternalCallCount
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
CorrectionsApplied
SubsequentHarnessSHA256s
SubsequentValidatorSHA256s
ExactMutationAccounting
FinalStructuralResult
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## Next gate

A Terra structural PASS does **not** authorize W5.

The next required authority is a fresh **GPT-5.6 Luna C2 W5 Harness Structural Reconciliation**, bound to:

```text
frozen RunnerSHA256
final HarnessSHA256
final ValidatorSHA256
final DurableRoot
```

Only Luna acceptance may authorize a subsequent fresh governed W5 execution and RunId allocation.

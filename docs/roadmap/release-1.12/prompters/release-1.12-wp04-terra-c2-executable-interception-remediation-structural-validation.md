# Release 1.12 WP04 — Terra C2 Executable Interception Remediation & Fresh Structural Validation

**Selected execution model: GPT-5.6 Terra**

## Authority

Remediate only the confirmed C2 disposable-harness executable-interception defect and the corresponding independent-validator structural gap, then run one complete fresh structural validation cycle.

This authority does **not** authorize W5 RunId allocation or production-wrapper execution.

## Binding Luna decision

```text
RELEASE 1.12 WP04 — LUNA C2 RUNTIME INTERCEPTION CONTRACT RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION DEFECT: CONFIRMED
RELEASE 1.12 WP04 — C2 HARNESS RUNTIME INTERCEPTION REMEDIATION: REQUIRED
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
```

## Binding historical tuple

Frozen runner — MUST NOT CHANGE:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Superseded harness:

```text
636FF0C654CA0A5901D4A8A5D27CA848DD2C04C1B9A155235FAA1F762C10EBAD
```

Superseded validator:

```text
CC5BED1E30889E8ACCF7A912A870ECBFA9F5EDC9D0DB68074294F0FBD88EACFB
```

Preserve historical artifacts/evidence for audit; do not compose PASS across hashes.

## Exact production-wrapper facts

The production wrapper is not defective and MUST NOT be edited.

Observed reachable external-call surfaces include:

```text
helper call: line 359
git calls: lines 321, 323, 325, 327
az calls: lines 177, 332, 338, 340
```

The wrapper exposes:

```text
-LocalValidation
-PersistEvidenceCheckpointCallback
```

`-LocalValidation` is a fixture-test path and is **not** a governed W5 runtime callback. Do not misuse it as the runtime interception mechanism.

The prior harness invoked:

```text
& $W
```

and its `Install-ApprovedInterceptions` only wrote ledger records. That is insufficient.

## PowerShell baseline

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only assumptions.

## Required harness remediation

Implement a **real process-scoped executable interception boundary** before any future RunId allocation.

The interception implementation must cover:

```text
git
az
the required helper executable/script invocation
```

It must operate for calls originating inside the copied production wrapper.

### Required properties

```text
1. only explicit approved call shapes are accepted
2. approved calls execute only governed local behavior
3. every intercepted call is durably ledgered
4. unexpected call shapes fail closed
5. real git cannot be reached
6. real az cannot be reached
7. real helper execution cannot escape the governed interception
8. interception scope is disposable/process-scoped
9. interception does not persist global machine/session mutation
10. cleanup removes disposable interception artifacts
11. production lifecycle policy remains exclusively in the production wrapper
12. harness does not manufacture W5 P01-P20/lifecycle outcomes
```

Use a PowerShell-5.1-compatible mechanism whose command-resolution precedence is explicit and testable.

Do not rely on ledger declarations as proof of interception.

## Approved-call contract

Derive the exact allowlist from the production wrapper and previously governed W5 scenario.

Do not create wildcard allowances such as arbitrary:

```text
git *
az *
helper *
```

For every permitted call shape retain:

```text
command identity
normalized arguments
expected local governed result
ledger representation
```

Any argument/call-shape mismatch must fail closed.

## Pre-RunId executable-interception probes

The remediated harness must provide a structural/preflight probe mode that proves all of the following without allocating a RunId or executing the production wrapper's governed W5 path:

### I01 — approved git probe

A representative approved git call originating through the same command-resolution scope resolves to the governed git interception and produces the expected local result/ledger entry.

### I02 — approved az probe

A representative approved az call resolves to the governed az interception and produces the expected local result/ledger entry.

### I03 — approved helper probe

A representative approved helper call resolves to the governed helper interception and produces the expected local result/ledger entry.

### I04 — unexpected git call

A non-allowlisted git call fails closed and does not reach real git.

### I05 — unexpected az call

A non-allowlisted az call fails closed and does not reach real az.

### I06 — unexpected helper call

A non-allowlisted helper call fails closed and does not reach the real helper.

### I07 — real-command escape probe

Prove that the copied-wrapper execution scope cannot resolve the tested governed call surfaces to the real external commands while interception is active.

### I08 — ledger completeness

All approved and rejected probes produce the required sanitized durable interception observations.

### I09 — cleanup

After probe cleanup, disposable interception artifacts are absent and no persistent/global command-resolution mutation remains.

Acceptance:

```text
I01-I09 = ALL_PASS
```

These are structural/preflight predicates, not W5 P01-P20 PASS claims.

## Runtime orchestration ordering

The future governed runtime path must be structurally proven to order:

```text
artifact/hash gates
→ exact-byte wrapper copy
→ G01-G11
→ executable interception installation/binding
→ approved-call probes
→ unexpected-call probes
→ real-command escape probe
→ interception boundary ALL_PASS
→ only then RunId allocation
→ copied wrapper invocation
```

This authority MUST stop before the last two runtime actions.

## Validator remediation

Modify only the disposable/local-only independent validator as required so it cannot accept a descriptive interception boundary.

Fresh validation must prove calls from the copied-wrapper command-resolution scope resolve to the executable governed interception.

Strengthen evidence for at least:

```text
SV16 G01-G11 before wrapper
SV27 minimum shims
SV28 unexpected calls fail closed
SV29 no policy duplication
```

Preserve canonical SV01-SV36 numbering and meanings.

Require separate summary predicate:

```text
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
```

Its durable evidence must cover I01-I09 and the future runtime ordering.

## Fresh identities

After final corrections freeze:

```text
RunnerSHA256 = 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
HarnessSHA256 = <new final hash>
ValidatorSHA256 = <new final hash>
```

Every byte-changing correction invalidates the prior candidate hash.

No cross-hash composition.

## Fresh complete SV01-SV36

Run all 36 checks from SV01 using the final tuple.

Require:

```text
SVRecordCount = 36
UniqueSVCheckIdCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS
I01-I09 = ALL_PASS
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

## Structural runtime exclusions

During this entire authority:

```text
Governed W5 RunId allocations = 0
Governed W5 wrapper invocations = 0
Real external calls = 0
```

No structural probe may accidentally cross the runtime boundary.

## Durable evidence

Create a fresh durable structural root.

Retain:

```text
frozen runner copy/hash
new final harness copy/hash
new final validator copy/hash
exact allowlist
interception implementation evidence
I01-I09 evidence
command-resolution/escape evidence
C2_EXECUTABLE_INTERCEPTION_BOUNDARY evidence
runtime ordering evidence
SV01-SV36 ledger
36/36 evidence manifest/resolution
six-checkpoint finalization evidence
repo-state evidence
git diff --check evidence
cleanup evidence
```

## Finalization checkpoints

Exactly once and in order:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

## Repository invariants

Exactly the two pre-existing tracked modifications may remain:

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

## Forbidden mutations

```text
frozen runner modification
production-wrapper modification
tracked-source modification
staging
commit
push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
production mutation
real external calls
W5 RunId allocation
W5 wrapper execution
W6/W7/W8
publication
WP04 lifecycle closure
```

## Correction loop

Local-only harness/validator corrections are permitted within this authority.

After every byte change, discard prior candidate PASS credit.

The final accepted structural cycle must be wholly under one final harness/validator hash pair.

## PASS markers

Only after I01-I09 and fresh SV01-SV36 all pass under the final tuple:

```text
RELEASE 1.12 WP04 — TERRA C2 EXECUTABLE INTERCEPTION REMEDIATION: PASS
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: PASS
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ALL_PASS
RELEASE 1.12 WP04 — C2 REMEDIATED HARNESS IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 REMEDIATED VALIDATOR IDENTITY: FROZEN
RELEASE 1.12 WP04 — C2 FRESH SV01-SV36: ALL_PASS
RELEASE 1.12 WP04 — C2 EVIDENCE REFERENCES: 36/36 RESOLVED
RELEASE 1.12 WP04 — C2 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — READY FOR FRESH LUNA C2 INTERCEPTION RECONCILIATION: YES
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
ParserErrorCounts
ExecutableInterceptionMechanism
InterceptionScope
ApprovedGitCallShapes
ApprovedAzCallShapes
ApprovedHelperCallShapes
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
RealGitEscapeResult
RealAzEscapeResult
RealHelperEscapeResult
FutureRuntimeInterceptionOrderingResult
GovernedW5RunIdAllocated
GovernedW5WrapperInvoked
RealExternalCallCount
SVRecordCount
UniqueSVCheckIdCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
CanonicalCheckpointCounts
DurableRoot
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
DisposableRootAfterCleanup
DurableHarnessCopyResult
DurableValidatorCopyResult
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckExitCode
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

The next authority must be a fresh **GPT-5.6 Luna C2 Executable Interception Final Structural Reconciliation** bound to the new final runner/harness/validator tuple and durable root.

Only a subsequent Luna PASS may authorize a new governed W5 execution and fresh RunId allocation.

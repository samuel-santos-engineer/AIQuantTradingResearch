# Release 1.12 WP04 — Terra Freeze DTO-Normalized Validator & Full Supervised SV Rerun

**Selected execution model: GPT-5.6 Terra**

## Reconciled defect

The marker-based supervised run isolated the non-return interval to:

```text
CONVERTJSON_ENTER = durable
CONVERTJSON_RETURN = absent
child alive through bounded timeout
=> CONVERTJSON_NONRETURN
```

Root-cause class:

```text
VALIDATOR_DEFECT / SERIALIZER_INPUT_OBJECT_SHAPE
```

A validator-only correction has been applied before serialization: every SV record is materialized to a plain seven-field DTO containing exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

This preserves the frozen SV record contract while removing live/complex PowerShell object shape from the serializer input.

No runner, production, W5, or external-service state changed.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

The previous marker validator hash is historical after the DTO-normalization byte change:

```text
5D9BCAD9869A22FD1EA6519A363FD5E078685754005F6439151CE862111FD489
```

No acceptance credit may be composed across validator hashes.

## DTO contract verification

Before execution, inspect the corrected source and prove:

1. normalization occurs after SV record construction/validation and before ledger serialization;
2. each normalized SV record contains exactly the canonical seven fields;
3. no mandatory field is omitted or renamed;
4. values are preserved semantically from the source SV record;
5. no acceptance predicate is changed;
6. no evidence-reference resolution rule is changed;
7. no SV01-SV36 requirement is changed;
8. no P01-P20 runtime PASS claim is introduced;
9. canonical finalization checkpoint semantics are unchanged;
10. diagnostic serializer markers remain diagnostic-only.

Retain a sanitized source-location/normalization proof artifact.

## Fresh identity gate

Because validator bytes changed:

```text
parse with Windows PowerShell 5.1.26100.9444
require parser errors = 0
compute fresh ValidatorSHA256
retain exact validator bytes/hash
verify frozen RunnerSHA256
create fresh independent DisposableRoot
create fresh independent DurableRoot
create fresh supervisor root
```

Require staged paths = 0 before execution.

## Complete fresh structural run

Run the full Architecture-B structural validator from **SV01** under the detached artifact-first supervisor.

No carry-forward from prior hashes or runs.

The supervisor must durably observe the complete process lifecycle independently of the Codex session.

Use the proven bounded supervisor mechanism. If the corrected validator progresses normally beyond the historical 60-second non-return boundary, allow the complete structural run to finish under a bounded execution budget appropriate to the full validator. Record the exact bound used. Do not use an unbounded wait.

## Serializer remediation proof

The fresh run must demonstrate:

```text
CONVERTJSON_ENTER
CONVERTJSON_RETURN
```

in order.

If `CONVERTJSON_RETURN` remains absent and the child remains alive through the bound, the DTO normalization has not resolved the non-return; diagnose further under validator-only authority.

If ConvertTo-Json returns but a later finalizer operation fails/non-returns, use the existing diagnostic marker sequence to isolate it and continue validator-only remediation.

## Canonical finalization proof

Final structural PASS requires exactly one durable canonical checkpoint each:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Diagnostic markers do not substitute for these checkpoints.

## Full SV01-SV36 acceptance

Require a single fresh validator hash to prove:

```text
SVRecordCount = 36
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

Every `EvidenceReference` must resolve after disposable cleanup to a retained artifact or retained locator.

## Final ledger acceptance

Require:

```text
final sv01-sv36-ledger.json exists
atomic publication completed
final ledger independently reopens/parses
required ledger metadata present
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
```

The retained evidence package must include the evidence manifest and resolution report.

## Cleanup and identity acceptance

Require:

```text
DisposableRoot = absent after cleanup
DurableRoot = survives
runner survives
validator survives
checkpoint artifact survives
diagnostic marker artifact survives
final ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorHashAfterCleanup = fresh ValidatorSHA256
```

## Repository/governance invariants

Require:

```text
authority-introduced tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
external-service mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

Validator-local disposable/durable diagnostic artifacts are permitted and must be accounted exactly.

## Autonomous remediation loop

If this fresh run encounters another ordinary validator/finalizer defect:

```text
diagnose from retained evidence
→ apply narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
```

Continue until complete structural PASS or mandatory governance BLOCKED.

Do not return merely because a new validator-local defect is discovered.

## Mandatory blocker boundary

STOP only if the next required action is:

```text
runner modification
tracked production-source modification
Architecture-B/P01-P20/SV01-SV36/G01-G13 contract change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/GitHub/Docker/GHCR/external-service mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

## Required handoff

Return:

```text
RunnerSHA256
HistoricalMarkerValidatorSHA256
FreshDtoValidatorSHA256
ParserResult
DtoNormalizationProofPath
DisposableRoot
DurableRoot
SupervisorRoot
SupervisorTimeoutSeconds
SupervisorTerminal
ChildPID
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
DiagnosticMarkerSequence
CONVERTJSON_ENTER_Count
CONVERTJSON_RETURN_Count
LastCanonicalCheckpoint
CanonicalCheckpointCounts
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
FinalLedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
GitDiffCheckExitCode
GitDiffCheckOutput
FinalLedgerReopenParseResult
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
CorrectionsApplied
SubsequentValidatorSHA256s
FreshStructuralRunAttempts
FinalStructuralResult
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## PASS markers

On complete structural PASS:

```text
RELEASE 1.12 WP04 — TERRA DTO-NORMALIZED ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — CONVERTTO-JSON NONRETURN REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <FRESH_HASH>
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 RETAINED CHECKPOINT CARDINALITY: EXACTLY_ONE_EACH
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL ATOMIC LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 FINAL LEDGER REOPEN/PARSE: PASS
RELEASE 1.12 WP04 — R1 READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Next gate

If complete structural PASS is achieved, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the final validator hash and frozen runner hash.

W5 remains prohibited until Luna accepts that structural gate.

# Release 1.12 WP04 — Terra Recover DTO-Supervised Structural Run

**Selected execution model: GPT-5.6 Terra**

## Bound execution identity

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
04D3A45D101EFACCE6C5EECA5030209CD60793A00E49528D7335100A550B880C

Windows PowerShell parser errors = 0
SUPERVISOR_LAUNCHED = True
```

This validator contains the seven-field DTO normalization intended to remediate the proven `CONVERTJSON_NONRETURN`.

No W5 execution or W5 RunId allocation occurred.

## Immediate gate

Recover the **existing** detached supervised structural run. Do not launch another validator or supervisor first.

Locate the exact supervisor/DurableRoot created for validator hash `04D3A45D...B880C` and recover all terminal artifacts.

If the run is still legitimately active within its declared bound, wait only for that existing run. Do not overlap executions.

## First decisive check — serializer remediation

Recover the durable diagnostic marker sequence and determine whether this exact run proves:

```text
CONVERTJSON_ENTER = present
CONVERTJSON_RETURN = present
```

If both are present in order, record:

```text
CONVERTTO_JSON_NONRETURN_REMEDIATION = PASS
```

If `CONVERTJSON_ENTER` is present, `CONVERTJSON_RETURN` absent, and the child remained alive through the bounded timeout, record:

```text
CONVERTTO_JSON_NONRETURN_REMEDIATION = FAIL
```

and continue validator-only diagnosis under the existing autonomous authority.

If the child exited independently, report the actual exit/failure rather than calling it a non-return.

## Finalizer progression

Recover the exact ordered diagnostic markers:

```text
SERIALIZATION_SEQUENCE_ENTER
CONVERTJSON_ENTER
CONVERTJSON_RETURN
TEMPWRITE_ENTER
TEMPWRITE_RETURN
TEMPPARSE_ENTER
TEMPPARSE_RETURN
PUBLISH_ENTER
PUBLISH_RETURN
FINALREOPEN_ENTER
FINALREOPEN_RETURN
```

Also recover canonical checkpoint cardinality:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Structural PASS requires each canonical checkpoint exactly once.

## SV01-SV36 gate

For this single validator hash require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Every retained SV record must have exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Every evidence reference must resolve after disposable cleanup.

## Final ledger gate

Require:

```text
final sv01-sv36-ledger.json exists
atomic publication = PASS
final ledger reopen/parse = PASS
GitDiffCheckOutput = PRESENT
GitDiffCheckExitCode = 0
evidence manifest retained
resolution report retained
```

## Cleanup and identity gate

Require:

```text
DisposableRoot absent after cleanup
DurableRoot survives
runner survives
validator survives
checkpoint artifact survives
diagnostic marker artifact survives
ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorHashAfterCleanup =
04D3A45D101EFACCE6C5EECA5030209CD60793A00E49528D7335100A550B880C
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

## Autonomous continuation on validator-local failure

If the recovered run does not structurally pass because of another ordinary validator/finalizer defect:

```text
diagnose retained evidence
→ narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ complete SV01-SV36 from SV01
```

Continue until structural PASS or mandatory governance BLOCKED.

Do not combine PASS evidence across validator hashes.

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
ValidatorSHA256
SupervisorRoot
SupervisorState
SupervisorTerminal
SupervisorTimeoutSeconds
ChildPID
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
DiagnosticMarkerSequence
CONVERTJSON_ENTER_Count
CONVERTJSON_RETURN_Count
ConvertToJsonRemediationResult
CanonicalCheckpointCounts
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
DurableRoot
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
GitDiffCheckExitCode
GitDiffCheckOutput
FinalLedgerReopenParseResult
DisposableRootAfterCleanup
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
FreshStructuralRunAttempts
CorrectionsApplied
SubsequentValidatorSHA256s
FinalStructuralResult
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## Structural PASS terminal markers

If this or an authorized subsequent fresh single-hash run completely passes:

```text
RELEASE 1.12 WP04 — TERRA DTO-NORMALIZED ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — CONVERTTO-JSON NONRETURN REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <FINAL_HASH>
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

On complete structural PASS, STOP for a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**, bound to the final validator hash and frozen runner hash.

W5 remains prohibited until that Luna gate accepts the structure.

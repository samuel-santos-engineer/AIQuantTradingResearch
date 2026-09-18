# Release 1.12 WP04 — Terra Recover JavaScriptSerializer Structural Run

**Selected execution model: GPT-5.6 Terra**

## Bound execution identity

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
498B15A3D542D8076087A6318F17B9BE270008D9731AF146D7A5F8875949EE98

Windows PowerShell parser errors = 0
SUPERVISOR_LAUNCHED = True
```

No W5 execution or W5 RunId allocation occurred.

## Immediate action

Recover the **existing** detached supervised full structural run bound to validator `498B15A3...9EE98`. Do not launch another run first.

Recover the exact supervisor root, DurableRoot, stdout/stderr, canonical checkpoint artifact, diagnostic serializer-marker artifact, final ledger if present, evidence manifest, resolution report, and failure diagnostic if present.

If the existing bounded run is still legitimately active, wait only within its already-declared bound. Do not overlap runs.

## Serializer replacement gate

Prove whether the replacement `System.Web.Script.Serialization.JavaScriptSerializer` operation returned.

Report the diagnostic marker names actually frozen in this validator and their explicit mapping to the replacement serializer if historical `CONVERTJSON_*` labels remain.

Require an ordered durable enter/return pair around `JavaScriptSerializer.Serialize(...)`.

If enter is present and return absent while the child remains alive through timeout, classify the replacement serializer as non-returning.

If it returns, record:

```text
FINAL_LEDGER_SERIALIZER_REMEDIATION = PASS
```

and continue through the remaining finalizer gates.

## Finalizer progression

Recover and reconcile, in source order:

```text
serializer enter/return
temporary-file write enter/return
temporary-file parse enter/return
final publication enter/return
final-ledger reopen enter/return
```

Also recover canonical checkpoint counts:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Structural PASS requires exactly one of each canonical checkpoint.

## Full structural acceptance

For this single validator hash require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Every retained SV record must contain exactly:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

All evidence references must resolve after disposable cleanup.

## Final ledger acceptance

Require:

```text
final sv01-sv36-ledger.json exists
atomic publication = PASS
final ledger independently reopens/parses = PASS
required ledger metadata present
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
evidence manifest retained
resolution report retained
```

## Cleanup and identity acceptance

Require:

```text
DisposableRoot absent after cleanup
DurableRoot survives
runner survives
validator survives
checkpoint artifact survives
diagnostic marker artifact survives
final ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorHashAfterCleanup =
498B15A3D542D8076087A6318F17B9BE270008D9731AF146D7A5F8875949EE98
```

## Governance invariants

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

## Autonomous validator-only continuation

If this exact run reveals another ordinary validator/finalizer defect, continue without returning merely for another validator authority:

```text
diagnose retained evidence
→ narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
```

No cross-hash PASS composition.

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
SerializerImplementation
SerializerDiagnosticMarkerMapping
SerializerEnterCount
SerializerReturnCount
SerializerRemediationResult
DiagnosticMarkerSequence
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

## PASS terminal markers

If this or an authorized subsequent single-hash fresh run fully passes:

```text
RELEASE 1.12 WP04 — TERRA JAVASCRIPTSERIALIZER ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — FINAL-LEDGER SERIALIZER REMEDIATION: PASS
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

On complete structural PASS, STOP for a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the final validator hash and frozen runner hash.

W5 remains prohibited until Luna accepts that structural gate.

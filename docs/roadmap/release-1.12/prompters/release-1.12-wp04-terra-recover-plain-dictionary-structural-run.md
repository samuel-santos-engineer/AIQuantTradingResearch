# Release 1.12 WP04 — Terra Recover Plain-Dictionary Structural Run

**Selected execution model: GPT-5.6 Terra**

## Bound identity

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
7BDEA3A1B21C3594A0ED3546FC769C8F4831185423570D12749529CB9FB203AD

Windows PowerShell parser errors = 0
SUPERVISOR_LAUNCHED = True
```

No W5 execution or W5 RunId allocation occurred.

## Immediate action

Recover the **existing** detached supervised structural run for validator `7BDEA3A1...203AD`. Do not launch another run first.

Recover its exact supervisor root, DurableRoot, stdout/stderr, canonical checkpoint artifact, diagnostic marker artifact, failure diagnostic if present, final ledger if present, evidence manifest, and evidence-reference resolution report.

If still active within its declared bounded execution budget, wait only for that existing run. Do not overlap executions.

## Serializer/root-cause remediation gate

This validator is intended to eliminate the proven PowerShell-wrapper cycle by serializing only plain ordered dictionaries.

Recover durable proof that the replacement serializer operation both entered and returned.

Also verify the published/reopened JSON contains the required ledger values and metadata without PowerShell wrapper-derived fields.

Required outcome:

```text
POWERSHELL_WRAPPER_SERIALIZER_DEFECT_REMEDIATION = PASS
```

only if serialization returns and the resulting JSON contract is preserved.

## Finalizer progression

Reconcile the actual diagnostic marker sequence around:

```text
serializer
temporary-file write
temporary-file parse
final publication
final-ledger reopen
```

Then reconcile canonical checkpoint counts:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Structural PASS requires exactly one durable occurrence of each canonical checkpoint.

## Full SV01-SV36 acceptance

For this single validator hash require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Each retained SV record must contain exactly:

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

## Final ledger acceptance

Require:

```text
final sv01-sv36-ledger.json exists
atomic publication = PASS
final ledger independently reopens/parses = PASS
36 SV records retained
seven canonical fields retained per SV record
required top-level metadata retained
no wrapper-derived/runtime-object fields
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
evidence manifest retained
resolution report retained
```

## Cleanup and identity

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
7BDEA3A1B21C3594A0ED3546FC769C8F4831185423570D12749529CB9FB203AD
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

If this run reveals another ordinary validator/finalizer defect, continue:

```text
diagnose retained evidence
→ narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ complete SV01-SV36 from SV01
```

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
SerializedLedgerShapeVerification
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

On complete structural PASS:

```text
RELEASE 1.12 WP04 — TERRA PLAIN-DICTIONARY ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — POWERSHELL-WRAPPER SERIALIZER DEFECT REMEDIATION: PASS
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

If complete structural PASS is achieved, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**, bound to final validator hash and frozen runner hash.

W5 remains prohibited until Luna accepts the structural gate.

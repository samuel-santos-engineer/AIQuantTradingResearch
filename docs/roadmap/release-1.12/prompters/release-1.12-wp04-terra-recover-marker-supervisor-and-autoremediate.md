# Release 1.12 WP04 — Terra Recover Marker Supervisor & Autoremediate

**Selected execution model: GPT-5.6 Terra**

## Bound identities

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorSHA256 =
5D9BCAD9869A22FD1EA6519A363FD5E078685754005F6439151CE862111FD489

Windows PowerShell parser errors = 0
Supervisor bound = 60 seconds
SUPERVISOR_LAUNCHED = True
```

No W5 invocation or W5 RunId allocation occurred.

## Gate

The launch is valid but is not yet diagnostic acceptance. Recover the **existing** detached supervisor result. Do not launch another validator or supervisor first.

Locate its durable supervisor root and recover:

```text
observation.json
stdout.log
stderr.log
canonical checkpoint artifact
diagnostic serializer-marker artifact
failure-diagnostic.json if present
```

If the declared 60-second observation is still legitimately active, wait only for that existing run to finish. Do not extend the bound or overlap executions.

## Required marker reconciliation

Report the ordered durable diagnostic marker sequence from this exact validator hash.

Expected marker vocabulary:

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

Also report:

```text
LastCanonicalCheckpoint
LastDiagnosticMarker
FirstMissingExpectedDiagnosticMarker
```

## Exact non-return classification

A non-return classification requires:

```text
corresponding *_ENTER is durable
matching *_RETURN is absent
child remained alive through the 60-second bound
no independent child exit occurred first
```

Allowed classifications:

```text
CONVERTJSON_NONRETURN
TEMPWRITE_NONRETURN
TEMPPARSE_NONRETURN
PUBLICATION_NONRETURN
FINALREOPEN_NONRETURN
POST_FINALREOPEN_NONRETURN
DIFFERENT_FAILURE
INSUFFICIENT_EVIDENCE
```

Do not infer a hang when the child independently exited.

## Autonomous remediation

Once an operation is isolated, diagnose and correct the ordinary validator-local defect without returning merely for another validator authority.

If `CONVERTJSON_NONRETURN` is proven, first perform bounded/non-recursive sanitized object-shape inspection, including:

```text
top-level property names and types
collection counts
bounded nesting/type summaries
cycle/self-reference indicators
serializer arguments/depth
unexpected ErrorRecord / InvocationInfo
unexpected PSObject/runtime wrapper graphs
unexpected Process/Runspace/Stream objects
bounded payload characteristics
```

For another isolated operation, diagnose that operation directly.

Apply the narrowest validator-only correction preserving all mandatory evidence, metadata, SV records, references, and fail-closed semantics.

Every validator byte change requires:

```text
WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh DisposableRoot
→ fresh DurableRoot
→ fresh supervisor root
→ full SV01-SV36 from SV01
```

Cross-hash PASS composition is forbidden.

Continue through ordinary validator/finalizer defects until complete structural PASS or a mandatory governance blocker.

## Structural PASS requirements

Require exactly one canonical durable checkpoint each:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

and:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
DisposableRoot absent after cleanup
DurableRoot survives
runner hash = frozen hash
validator hash = final fresh hash
staged paths = 0
authority-introduced tracked repository mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Mandatory blocker boundary

STOP only if the next required action is:

```text
runner modification
tracked production-source modification
Architecture-B/P01-P20/SV01-SV36/G01-G13 change
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
StartingValidatorSHA256
SupervisorRoot
SupervisorState
SupervisorTerminal
ChildPID
TimeoutSeconds
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
LastCanonicalCheckpoint
DiagnosticMarkerSequence
LastDiagnosticMarker
FirstMissingExpectedDiagnosticMarker
NonReturnOperationClassification
RootCauseClassification
CorrectionsApplied
SubsequentValidatorSHA256s
FreshStructuralRunAttempts
FinalStructuralResult
FinalDurableRoot
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
GitDiffCheckExitCode
FinalLedgerReopenParseResult
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## Next gate

If complete structural PASS is reached, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**.

If governance BLOCKED, STOP for blocker-specific authority.

W5 remains prohibited.

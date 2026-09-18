# Release 1.12 WP04 — Terra Freeze Marker Validator & Supervised Isolation Run

**Selected execution model: GPT-5.6 Terra**

## Reconciled state

Durable diagnostic-only markers have been added around the actual finalizer operations:

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

Canonical six-checkpoint semantics remain unchanged.

Runner mutation: NO.
W5 invoked: NO.
W5 RunId allocated: NO.

Marker installation alone receives no structural acceptance credit. The next gate is to freeze the modified validator under a fresh hash and execute it under the proven detached bounded supervisor.

## Frozen runner/runtime

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

## Pre-execution gate

Before launching:

1. Parse the marker-instrumented validator under Windows PowerShell 5.1.26100.9444; require zero parser errors.
2. Compute and retain the exact fresh `ValidatorSHA256`.
3. Retain an exact copy of those validator bytes and hash.
4. Verify the runner hash still equals the frozen runner hash.
5. Create fresh independent DisposableRoot, DurableRoot, and supervisor root.
6. Confirm staged paths = 0.
7. Confirm no W5 RunId exists or is allocated.
8. Confirm no W5 wrapper invocation occurs.

If any pre-execution invariant fails, stop without running the validator.

## Execution

Launch exactly one fresh validator execution under the detached artifact-first supervisor.

Use the already-proven bounded observation contract:

```text
TimeoutSeconds = 60
```

Do not launch an overlapping validator/supervisor.

Supervisor evidence must be durable independently of the Codex session.

## Required observations

Recover:

```text
ValidatorSHA256
SupervisorRoot
SupervisorState
SupervisorTerminal
ChildPID
ChildStartUtc
ChildExitUtc
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
stdout
stderr
canonical checkpoint artifact
diagnostic marker artifact
failure-diagnostic.json if present
```

Report the exact ordered diagnostic marker sequence observed.

## Isolation gate

Determine:

```text
LastCanonicalCheckpoint
LastDiagnosticMarker
FirstMissingExpectedDiagnosticMarker
```

Classify the first proven non-return interval:

```text
CONVERTJSON_NONRETURN
TEMPWRITE_NONRETURN
TEMPPARSE_NONRETURN
PUBLICATION_NONRETURN
FINALREOPEN_NONRETURN
POST_FINALREOPEN_NONRETURN
DIFFERENT_FAILURE
```

Only classify an operation as non-returning when its `*_ENTER` marker is durable, its matching `*_RETURN` is absent, and the child remained alive through the bounded timeout.

If the child exits independently, report the exit instead; do not call it non-return.

## Autonomous diagnosis and correction

Once the operation is isolated, continue under validator-only authority.

For `CONVERTJSON_NONRETURN`, inspect the ledger object in a bounded, non-recursive, sanitized manner before correction:

```text
top-level property names/types
collection counts
bounded nesting/type summaries
cycle/self-reference indicators
serializer arguments/depth
unexpected ErrorRecord/InvocationInfo
unexpected PSObject/runtime wrapper graphs
unexpected process/runspace/stream objects
bounded payload characteristics
```

For another isolated operation, diagnose that operation directly.

Apply only the narrowest validator-local correction supported by evidence.

Do not remove mandatory SV records, evidence references, metadata, acceptance fields, or finalization guarantees.

## Rehash/restart invariant

Every validator byte change requires:

```text
WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh DisposableRoot
→ fresh DurableRoot
→ fresh supervisor root
→ full SV01-SV36 from SV01
```

No cross-hash PASS composition.

Continue autonomously through ordinary validator/finalizer defects until complete structural PASS or mandatory governance BLOCKED.

## Final structural PASS

Require:

```text
canonical PRE_CLEANUP = exactly 1
canonical CLEANUP_COMPLETE = exactly 1
canonical BUILD_LEDGER = exactly 1
canonical SERIALIZE = exactly 1
canonical PUBLISH = exactly 1
canonical REOPEN = exactly 1

SV records = 36
SV failures = 0
Evidence references = 36
Resolved = 36
Unresolved = 0
Runtime P01-P20 PASS claims = 0

final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput present
GitDiffCheckExitCode = 0

DisposableRoot absent after cleanup
DurableRoot survives
runner survives and hash matches
validator survives and hash matches
retained evidence resolves after cleanup

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
InstrumentedValidatorSHA256
ParserResult
DisposableRoot
DurableRoot
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

## Terminal markers

If isolation succeeds and remediation continues:

```text
RELEASE 1.12 WP04 — TERRA MARKER-BASED NONRETURN ISOLATION: PASS
RELEASE 1.12 WP04 — NONRETURN OPERATION: <CLASSIFICATION>
```

If the autonomous loop reaches structural PASS:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
```

Then STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**.

W5 remains prohibited.

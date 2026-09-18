# Release 1.12 WP04 — Terra Exact Serializer-Stage Marker Rerun

**Selected execution model: GPT-5.6 Terra**

## Reconciled state

The proven process boundary remains:

```text
BUILD_LEDGER
→ serialization/finalization non-return
```

External bounded supervision already proved the child remained alive through the 60-second bound without reaching canonical `SERIALIZE`.

The next validator revision must add durable diagnostic markers around the **actual current finalizer operations**, specifically:

```text
ConvertTo-Json
temporary-file write
temporary-file parse
final publication
final-ledger reopen
```

No W5, runner, production, or external-service action has occurred.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

No runner or production edit is authorized.

## Source-first placement gate

Before editing, inspect the exact current validator finalizer source.

Identify the precise statements/functions implementing:

1. ledger `ConvertTo-Json`;
2. temporary ledger write;
3. temporary ledger reopen/parse;
4. final atomic publication;
5. final published-ledger reopen/parse.

Do not insert markers based on assumed source layout.

Retain a sanitized source-location map showing the actual operation and its before/after marker names.

## Required diagnostic marker sequence

Use a separate durable diagnostic marker artifact, not the canonical checkpoint artifact.

Bracket the real operations with exactly these semantic marker pairs (names may include a stable prefix if the current diagnostic format requires it):

```text
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

Also retain a marker immediately before entering this finalization sequence:

```text
SERIALIZATION_SEQUENCE_ENTER
```

Marker semantics are strict:

- `*_ENTER` must be durable immediately before the named operation.
- `*_RETURN` must be durable only after the named operation returns successfully.
- Markers must not recursively serialize the ledger object.
- Marker persistence must be simple, bounded, sanitized, and independent of the operation being diagnosed.
- These records are diagnostic only and must never count as canonical acceptance checkpoints.

## Canonical checkpoint semantics

Do not alter the canonical six-checkpoint contract:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Diagnostic `PUBLISH_ENTER/PUBLISH_RETURN` does not replace canonical `PUBLISH`.

Canonical checkpoint ordering/meaning remains frozen.

## Fresh validator identity

The marker change modifies validator bytes.

Therefore require:

```text
WinPS 5.1 parser errors = 0
fresh ValidatorSHA256
exact validator bytes retained
fresh DisposableRoot
fresh DurableRoot
```

No structural acceptance credit carries from any prior validator hash.

## Detached supervised execution

Run the newly frozen validator under a fresh detached artifact-first supervisor using the proven bounded pattern.

Use a finite 60-second observation bound unless retained evidence establishes that the diagnostic marker writes themselves materially require a different local bound. Do not silently extend it.

Persist supervisor lifecycle evidence independently of the Codex session.

## Non-return isolation rule

At timeout/exit, inspect both:

```text
last canonical checkpoint
last durable diagnostic marker
```

Classify the exact first non-return interval from the last `ENTER` whose corresponding `RETURN` is absent.

Examples:

```text
CONVERTJSON_ENTER present
CONVERTJSON_RETURN absent
=> CONVERTJSON_NONRETURN

TEMPWRITE_ENTER present
TEMPWRITE_RETURN absent
=> TEMPWRITE_NONRETURN

TEMPPARSE_ENTER present
TEMPPARSE_RETURN absent
=> TEMPPARSE_NONRETURN

PUBLISH_ENTER present
PUBLISH_RETURN absent
=> PUBLICATION_NONRETURN

FINALREOPEN_ENTER present
FINALREOPEN_RETURN absent
=> FINALREOPEN_NONRETURN
```

If all diagnostic pairs return but canonical progress still stalls, classify the actual next source operation rather than forcing one of these labels.

## Autonomous correction

Once the exact non-return operation is proven, diagnose its validator-local root cause and apply the narrowest correction.

If `CONVERTJSON_NONRETURN` is proven, perform bounded/non-recursive ledger object-shape inspection before correction, including top-level names/types, collection counts, bounded nesting, cycle/self-reference checks, serializer arguments/depth, and unexpected runtime graph objects.

If another operation is isolated, diagnose that operation directly.

Then continue autonomously:

```text
narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
→ repeat on ordinary validator-only defects
```

Do not return merely after identifying an ordinary validator/finalizer defect.

## Structural PASS gate

Final fresh single-hash execution still requires exactly one canonical durable checkpoint each:

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
final ledger exists and independently reopens/parses
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
DisposableRoot absent after cleanup
DurableRoot survives
runner/validator/evidence artifacts survive
runner hash reverified
validator hash reverified
staged paths = 0
authority-introduced tracked repository mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Mandatory escalation

STOP as `BLOCKED` only if the next required action is:

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
InstrumentedValidatorSHA256
SourceLocationMapPath
DiagnosticMarkerArtifactPath
DiagnosticMarkerSequence
LastDiagnosticMarker
FirstMissingDiagnosticMarker
LastCanonicalCheckpoint
SupervisorRoot
SupervisorObservationPath
ChildPID
TimeoutSeconds
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
NonReturnOperationClassification
RootCauseClassification
CorrectionsApplied
SubsequentValidatorSHA256s
FreshStructuralRunAttempts
FinalStructuralResult
ExactMutationAccounting
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## Immediate success marker

For successful isolation:

```text
RELEASE 1.12 WP04 — TERRA EXACT SERIALIZER-STAGE ISOLATION: PASS
RELEASE 1.12 WP04 — NONRETURN OPERATION: <CLASSIFICATION>
RELEASE 1.12 WP04 — RUNNER MUTATION: NO
RELEASE 1.12 WP04 — PRODUCTION MUTATION: NO
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
```

If the autonomous loop subsequently reaches complete structural PASS, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**.

W5 remains prohibited until Luna grants the later structural gate.

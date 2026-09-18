# Release 1.12 WP04 — Terra Serialization Process-Boundary Autoremediation

**Selected execution model: GPT-5.6 Terra**

## Reconciled state

Latest fresh run durably recorded `PRE_CLEANUP`, `CLEANUP_COMPLETE`, and `BUILD_LEDGER`, but not `SERIALIZE`, `PUBLISH`, or `REOPEN`.

`failure-diagnostic.json` was absent. Therefore the validator catch path was not observed. This narrows the investigation to a failure during/immediately after serialization that may be a hang, process/host termination, serializer non-return, catch-path bypass, or another validator-local process-boundary failure. **Do not claim a hang or process termination until externally proven.**

No runner, production, W5, or external-service state changed.

Frozen runner SHA-256:

`54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983`

## Mission

Move observation outside the validator process. Diagnose and autonomously correct validator-only defects until a complete fresh Architecture-B structural PASS or a mandatory governance blocker.

### External supervisor

Create/use a disposable local-only supervisor outside tracked repository paths. It may capture PID, start/end timestamps, exit code, stdout/stderr, last durable checkpoint and timestamp, elapsed time after `BUILD_LEDGER`, bounded process-alive observations, and locally exposed host/runtime termination information.

Retain sanitized supervisor source/hash and an observation ledger under a durable diagnostic root.

The supervisor must not modify the runner or production, invoke W5, allocate a W5 RunId, perform external mutations, or fabricate acceptance evidence.

### Bounded observation

Use a finite, recorded timeout sufficient to distinguish ordinary serialization latency from a stall.

If the process remains alive through the bound with `BUILD_LEDGER` last, no `SERIALIZE`, and no normal exit, classify `SERIALIZATION_HANG_OR_NONRETURN`.

If it exits, retain the exact exit code, process lifetime, stdout/stderr tail, last checkpoint, and any available local host/runtime diagnostics. Do not infer a specific root cause without evidence.

### Narrow validator instrumentation

If needed, add sanitized durable diagnostic stage markers separate from the canonical checkpoint artifact:

`SERIALIZE_ENTER → SERIALIZE_OBJECT_READY → SERIALIZE_CONVERTJSON_ENTER → SERIALIZE_CONVERTJSON_RETURN → SERIALIZE_TEMPWRITE_ENTER → SERIALIZE_TEMPWRITE_RETURN → SERIALIZE_REOPEN_ENTER → SERIALIZE_REOPEN_RETURN`

These are diagnostic markers only, never acceptance checkpoints.

If evidence points to `ConvertTo-Json` or equivalent non-return/host failure, inspect the ledger object with bounded non-recursive observations: top-level names/types, collection counts, bounded nesting/type summaries, self-reference/cycle checks, unexpected `ErrorRecord`/`InvocationInfo`/`PSObject` or live process/runspace/stream objects, and bounded payload characteristics. Never recursively dump an unbounded graph or secrets.

## Autonomous correction authority

Once evidence proves an ordinary validator-local cause, apply the narrowest correction. Permitted classes include bounded DTO/PSCustomObject materialization, accidental runtime-object/reference removal while preserving required values, serializer depth/argument correction, collection materialization, WinPS 5.1 serializer compatibility, local finalizer control flow, temporary-file write/reopen, checkpoint, atomic publication, ledger reopen, evidence-reference, metadata, and local harness defects.

Do not delete mandatory evidence or weaken acceptance.

Every validator byte change requires:

`WinPS 5.1 parse → freeze bytes → fresh ValidatorSHA256 → fresh DisposableRoot/DurableRoot → restart SV01-SV36 from SV01`

Cross-hash PASS carry-forward is forbidden.

Runtime target: **Windows PowerShell 5.1.26100.9444**.

## Terminal PASS gate

A final fresh run must prove exactly one durable accepted checkpoint in order:

`PRE_CLEANUP → CLEANUP_COMPLETE → BUILD_LEDGER → SERIALIZE → PUBLISH → REOPEN`

It must also prove: 36 SV records, 0 failures, 36 evidence references, 36 resolved, 0 unresolved, 0 runtime P01-P20 PASS claims; final `sv01-sv36-ledger.json` exists and independently reopens/parses; `GitDiffCheckOutput` is present with exit 0; DisposableRoot is absent; DurableRoot and runner/validator/checkpoint/ledger/manifest/resolution report survive; runner and validator hashes reverify; staged paths and authority-introduced tracked repository mutations are zero; W5 wrapper invocation and W5 RunId allocation are both NO.

## Continue-until-blocked rule

Do not stop for ordinary validator-only defects. Continue observation → diagnosis → validator-only correction → fresh hash → fresh full run until PASS.

STOP as `BLOCKED` only if the next required action is runner modification, tracked production-source modification, Architecture-B/P01-P20/SV01-SV36/G01-G13 change, acceptance weakening, P19/P20 reinterpretation, cross-hash PASS composition, Azure/GitHub/Docker/GHCR or other external/production mutation, Twelve Data secret configuration, W5 execution, or W5 RunId allocation.

Also stop if bounded external supervision plus safe validator-local instrumentation still cannot distinguish the failure sufficiently for a truthful correction.

## Mutation accounting

Report tracked repository mutations, staged paths, commits, pushes, GitHub/Azure/Docker/GHCR/production/external mutations, W5 executions, W5 RunIds, local validator mutations, and local supervisor/harness artifacts.

## PASS markers

```text
RELEASE 1.12 WP04 — TERRA SERIALIZATION PROCESS-BOUNDARY AUTOREMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <FINAL_HASH>
RELEASE 1.12 WP04 — R1 SERIALIZATION PROCESS-BOUNDARY ROOT CAUSE: <CLASSIFICATION>
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

## Required handoff

Return: RunnerSHA256, StartingValidatorSHA256, FinalValidatorSHA256, SupervisorArtifactPath/SHA256, SupervisorObservationLedgerPath, ObservedProcessOutcome, ObservedExitCode, ObservedTimeoutSeconds, LastDurableCheckpoint, FailureDiagnosticPresent, DiagnosticStageMarkerPath, RootCauseClassification, CorrectionsApplied, FreshStructuralRunAttempts, FinalDurableRoot, FinalCheckpointArtifactPath/Counts, LedgerPath, EvidenceManifestPath, EvidenceReferenceResolutionReportPath, SVRecordCount, SVFailedCount, Resolved/UnresolvedEvidenceReferenceCount, GitDiffCheckExitCode/Output, FinalLedgerReopenParseResult, RunnerHashAfterCleanup, ValidatorHashAfterCleanup, W5 invocation/RunId status, FirstBlockingDefect, FirstProhibitedRequiredAction, and ExactMutationAccounting.

## Next gate

On PASS, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**. On BLOCKED, STOP for blocker-specific authority. W5 remains prohibited.

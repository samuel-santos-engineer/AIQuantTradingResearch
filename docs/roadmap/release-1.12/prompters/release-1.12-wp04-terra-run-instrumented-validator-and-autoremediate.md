# Release 1.12 WP04 — Terra Run Instrumented Validator & Autoremediate Until PASS/BLOCKED

## Authority identity
**Selected execution model: GPT-5.6 Terra**

Continue under the existing autonomous validator-remediation authority. GPT-5.6 Luna retains final structural acceptance authority.

## Reconciled state
Validator-only sanitized durable failure instrumentation is implemented. On failure it must retain `<DurableRoot>\failure-diagnostic.json` containing the failing stage/SV, exception type and sanitized message, FullyQualifiedErrorId, and source position.

Runner mutation = NO. Production mutation = NO. W5 wrapper invoked = NO. W5 RunId allocated = NO.

Because instrumentation changed validator bytes, every prior validator SHA-256 is historical for acceptance.

## Immediate mission
Do not stop at instrumentation completion.

1. Inspect current validator bytes.
2. Verify runner SHA-256 is exactly `54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983`.
3. Parse with Windows PowerShell `5.1.26100.9444`.
4. Freeze the instrumented validator and compute fresh ValidatorSHA256.
5. Create fresh DisposableRoot and DurableRoot.
6. Execute complete SV01-SV36 from SV01.
7. On failure, inspect that run's durable `failure-diagnostic.json`.
8. Classify the exact validator/finalizer defect and apply the narrowest validator-only correction.
9. After every validator byte change: parse, freeze, compute a new SHA-256, create fresh roots, and restart SV01-SV36 from SV01.
10. Repeat until terminal structural PASS or the mandatory escalation boundary is reached.

Do not return merely because an ordinary validator/finalizer defect is found.

## Diagnostic contract
The diagnostic artifact must be bound to the exact fresh DurableRoot and validator hash. Use it as primary evidence for `FirstFailingStage`, `FirstFailingSV`, `ExceptionType`, `SanitizedExceptionMessage`, `FullyQualifiedErrorId`, `SourcePosition`, and `RootCauseClassification`.

If diagnostic persistence itself fails, correct that as an ordinary validator-only defect. Never persist secrets, tokens, credentials, or API keys.

## Autonomous correction scope
Terra may autonomously correct validator-only defects involving serialization/JSON, object shape, collection materialization, temporary paths/files, encoding/write/flush/close, temporary JSON parse, checkpoint persistence/order, atomic publication, ledger reopen, evidence references, ledger metadata, failure instrumentation, PowerShell 5.1 compatibility, and local orchestration/harness behavior.

Preserve P01-P20, SV01-SV36, G01-G13, P19, P20, and the frozen runner. Cross-hash PASS carry-forward is forbidden.

## Successful checkpoint contract
A successful fresh run must retain exactly one accepted durable record, in semantic order, for:

`PRE_CLEANUP → CLEANUP_COMPLETE → BUILD_LEDGER → SERIALIZE → PUBLISH → REOPEN`

Console duplication does not determine durable cardinality.

## Terminal structural gate
Require all of:

- SVRecordCount = 36; SVFailedCount = 0.
- EvidenceReferenceCount = 36; Resolved = 36; Unresolved = 0.
- RuntimePredicatePassClaims = 0.
- Six checkpoints, exactly one accepted durable record each.
- Final `sv01-sv36-ledger.json` exists and independently reopens/parses.
- `GitDiffCheckOutput` present and GitDiffCheckExitCode = 0.
- DisposableRoot absent; DurableRoot survives.
- Runner, validator, checkpoint artifact, ledger, manifest, and resolution report survive.
- Runner hash after cleanup equals frozen runner hash.
- Validator hash after cleanup equals final frozen validator hash.
- Staged paths = 0; authority-introduced tracked repository mutations = 0.
- W5 wrapper invoked = NO; W5 RunId allocated = NO.

## Mandatory escalation
Continue until PASS unless the required next action is any of:

- runner modification;
- tracked production-source modification;
- Architecture-B, P01-P20, SV01-SV36, or G01-G13 change;
- acceptance weakening;
- P19/P20 reinterpretation;
- cross-hash PASS composition;
- Azure, Docker/GHCR, GitHub, or other external/production mutation;
- Twelve Data secret configuration;
- W5 execution or W5 RunId allocation.

If required, STOP as `BLOCKED` and identify the first prohibited required action.

## Mutation accounting
At terminal result report exactly: tracked repository mutations introduced, staged paths, commits, pushes, GitHub mutations, Azure mutations, Docker/GHCR mutations, production mutations, W5 executions, W5 RunIds, and external mutations. Account local validator and disposable diagnostic artifacts separately.

## PASS markers
```text
RELEASE 1.12 WP04 — TERRA INSTRUMENTED VALIDATOR AUTOREMEDIATION: PASS
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

## Required handoff
Return: RunnerSHA256, StartingInstrumentedValidatorSHA256, FinalValidatorSHA256, FreshStructuralRunAttempts, FailureDiagnosticPaths, DefectsObserved, CorrectionsApplied, FinalDurableRoot, FinalDisposableRoot, FinalCheckpointArtifactPath, FinalCheckpointCounts, LedgerPath, EvidenceManifestPath, EvidenceReferenceResolutionReportPath, SVRecordCount, SVFailedCount, ResolvedEvidenceReferenceCount, UnresolvedEvidenceReferenceCount, GitDiffCheckExitCode, GitDiffCheckOutput, FinalLedgerReopenParseResult, RunnerHashAfterCleanup, ValidatorHashAfterCleanup, GovernedW5WrapperInvoked, GovernedW5RunIdAllocated, FirstBlockingDefect, FirstProhibitedRequiredAction, ExactMutationAccounting.

## Next gate
On PASS, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation**. On BLOCKED, STOP for blocker-specific authority. W5 remains prohibited.

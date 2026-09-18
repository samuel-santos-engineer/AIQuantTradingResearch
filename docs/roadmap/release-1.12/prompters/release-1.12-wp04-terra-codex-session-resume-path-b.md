# Release 1.12 WP04 — Terra Codex Session Resume: Path-B Checkpoint Integration

## Execution identity

**Selected execution model: GPT-5.6 Terra**

This artifact resumes, but does not broaden, the existing Path-B validator-only authority after a Codex session restart.

GPT-5.6 Luna retains contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Mandatory first action

**Inspect the current file before editing.**

Do not assume that prior session line numbers, patch contexts, in-memory state, or source snippets remain valid.

Current validator:

```text
.wp04-architecture-b-validator.ps1
```

Locate it from the current project working tree and inspect its actual bytes/source structure before applying any mutation.

## State handoff

Frozen runner identity:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

The runner has not changed.

The current validator already contains the durable `Write-Checkpoint` implementation.

Completed source-accurate durable checkpoint integrations:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
```

`CLEANUP_COMPLETE` was placed only after disposable-root absence is proven.

Remaining source-accurate full-line checkpoint integrations:

```text
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Current execution boundary:

```text
Fresh SV01-SV36 run started = NO
Runner changed = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

No prior validator evidence may be reused for fresh acceptance.

## Resume mission

Continue the existing Path-B authority from the actual current source.

1. Verify the runner hash before editing.
2. Inspect `.wp04-architecture-b-validator.ps1`.
3. Confirm `Write-Checkpoint` exists.
4. Confirm `PRE_CLEANUP` and `CLEANUP_COMPLETE` already persist durably.
5. Do not disturb those completed call sites unless an independently demonstrated defect requires it.
6. Locate the exact current packed/full-line source statements for:
   - `BUILD_LEDGER`
   - `SERIALIZE`
   - `PUBLISH`
   - `REOPEN`
7. Rewrite each against the exact current source, preserving all pre-existing operations and control-flow semantics.
8. Integrate each durable checkpoint only after its semantic condition is true.
9. Reconcile all six call sites.
10. Parse under Windows PowerShell 5.1.
11. Freeze validator bytes.
12. Compute a fresh ValidatorSHA256.
13. Create fresh disposable and durable roots.
14. Execute the complete fresh SV01-SV36 run.
15. Continue through all six retained checkpoints, final atomic ledger publication, reopen/parse, and terminal verification.

Do not stop after merely completing the source edits if execution can safely continue.

## Source-accurate rewrite rule

For each remaining call site:

```text
inspect exact current full line
→ capture all existing operations
→ derive replacement from current bytes
→ preserve operation ordering/error semantics
→ insert Write-Checkpoint at exact semantic boundary
→ verify replacement
```

Do not reuse stale patch context from the prior Codex session.

Do not perform broad formatting/refactoring.

## Remaining checkpoint semantics

### BUILD_LEDGER

Persist only after:

```text
post-cleanup observations complete
evidence-reference resolution complete
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
final ledger object exists
mandatory final metadata is populated
```

### SERIALIZE

Persist only after:

```text
final ledger serialized to same-durable-directory temporary file
write/flush/close complete
temporary file exists
temporary JSON reopens/parses
```

### PUBLISH

Persist only after:

```text
atomic publication succeeds
final sv01-sv36-ledger.json exists
```

### REOPEN

Persist only after:

```text
published final ledger reopened from disk
JSON parse succeeds
terminal invariants reverified
```

## Six-call-site preflight

Before freezing validator bytes require:

```text
PRE_CLEANUP       = 1 durable successful-path call
CLEANUP_COMPLETE  = 1 durable successful-path call
BUILD_LEDGER      = 1 durable successful-path call
SERIALIZE         = 1 durable successful-path call
PUBLISH           = 1 durable successful-path call
REOPEN            = 1 durable successful-path call

Total = 6
Missing = 0
Duplicates = 0
Stdout-only substitutes = 0
```

Retain source reconciliation evidence for all six.

## PowerShell baseline

Binding target:

```text
Windows PowerShell 5.1.26100.9444
```

Require parser errors = 0.

Do not use PowerShell 7-only syntax/cmdlets.

## Fresh validator rule

The current validator is incomplete until all six call sites pass reconciliation.

After completion:

```text
freeze validator bytes
compute fresh ValidatorSHA256
```

Any later validator byte change requires:

```text
new ValidatorSHA256
fresh roots
full SV01-SV36 restart
```

## Fresh validation rule

Do not start the fresh SV run until:

```text
runner hash exact = PASS
six call sites = PASS
semantic reconciliation = PASS
validator parser = PASS
validator frozen = PASS
fresh validator hash = captured
```

Then execute SV01-SV36 from the beginning.

No old evidence/root/hash contributes acceptance credit.

## Terminal sequence

Fresh execution must durably prove:

```text
PRE_CLEANUP
→ CLEANUP_COMPLETE
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

Then require:

```text
SV records = 36
SV failures = 0
Evidence references = 36
Resolved = 36
Unresolved = 0
final sv01-sv36-ledger.json exists
final ledger independently reopens/parses
GitDiffCheckOutput metadata present
git diff --check exit = 0
DisposableRoot absent
DurableRoot survives
runner/validator/ledger/manifest/resolution report/checkpoints survive
runner hash reverified
validator hash reverified
staged paths = 0
tracked repository mutations introduced by authority = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Iterative validator-only remediation

Ordinary validator-only defects discovered after resume remain Terra-correctable under this authority.

If validator bytes change after a hash was frozen:

```text
fresh hash
fresh roots
restart SV01-SV36
```

Do not ask for another authority merely because additional safe execution time is required.

Escalate only if resolution requires a runner, production, frozen-contract, or external-system mutation.

## Prohibited

```text
stale-context patching
starting SV before 6/6 call-site preflight
runner modification
production-source modification
manual checkpoint fabrication
stdout-only checkpoint acceptance
manual final-ledger fabrication
old-hash PASS carry-forward
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

## Mutation accounting

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
W5 executions = 0
W5 RunIds = 0
external mutations = 0
```

## Terminal PASS markers

Only after complete fresh validation and finalization:

```text
RELEASE 1.12 WP04 — TERRA PATH-B SESSION-RESUME EXECUTION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 CHECKPOINT CALLSITES: 6/6
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL ATOMIC LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 FINAL LEDGER REOPEN/PARSE: PASS
RELEASE 1.12 WP04 — R1 GITDIFFCHECKOUTPUT METADATA: PRESENT
RELEASE 1.12 WP04 — R1 RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 VALIDATOR HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Required handoff

Return:

```text
RunnerSHA256
FreshValidatorSHA256
ValidatorPath
CheckpointCallsiteCount
CheckpointCallsiteMissing
CheckpointCallsiteDuplicates
CheckpointSemanticReconciliationPassCount
ValidatorParserErrors
DurableRoot
DisposableRoot
CheckpointArtifactPath
CheckpointSequenceObserved
LedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
GitDiffCheckExitCode
GitDiffCheckOutput
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
FinalLedgerReopenParseResult
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
```

## Stop and next gate

After terminal PASS, STOP.

The next gate is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner hash and fresh validator hash.

W5 remains prohibited until Luna PASS.

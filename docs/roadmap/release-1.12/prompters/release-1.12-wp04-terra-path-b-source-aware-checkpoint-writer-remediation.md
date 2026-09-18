# Release 1.12 WP04 — Terra Path-B Source-Aware Retained Checkpoint Writer Remediation

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns this validator-only remediation. GPT-5.6 Luna retains the frozen contract and final structural acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Trigger

The current Path-B validator has checkpoint messages on stdout, but does **not** yet persist the required retained checkpoint artifact.

An attempted source patch failed because the validator source no longer matched the assumed patch context.

No validation run started.

Binding state:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

runner changed = NO
validation run started = NO
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

Classification:

```text
VALIDATOR_DEFECT / CHECKPOINT_PERSISTENCE_DEFECT
PATCH_CONTEXT_DRIFT = TRUE
Production defect = NO
Runner defect = NO
```

## Mission

Inspect the **actual current validator source** first, locate the real finalization control flow, and implement the narrowest validator-only retained checkpoint writer against that source.

Do not retry a stale textual patch blindly.

After the correction:

```text
freeze validator bytes
compute fresh ValidatorSHA256
run parser/preflight gates
execute complete fresh SV01-SV36
complete PRE_CLEANUP → CLEANUP_COMPLETE → BUILD_LEDGER → SERIALIZE → PUBLISH → REOPEN
reach terminal final-ledger verification
```

Do not stop after editing unless a mandatory escalation boundary is reached.

## Source-aware remediation rule

Before mutation:

1. locate the exact current validator file;
2. compute its pre-edit SHA-256;
3. parse it under Windows PowerShell 5.1;
4. inspect the actual functions/blocks responsible for:
   - durable-root creation;
   - retained evidence writing;
   - provisional ledger writing;
   - cleanup;
   - ledger construction;
   - serialization;
   - publication;
   - reopen/verification;
   - current stdout checkpoint messages;
5. retain a sanitized source-structure observation;
6. derive the edit from the actual source structure.

Do **not** require old line numbers, stale context, or an obsolete function layout.

If the validator architecture has changed but still supports the frozen contract, adapt the patch to the current implementation.

## Retained checkpoint writer contract

Implement one narrow validator-owned durable checkpoint mechanism.

It must write under the current run's `DurableRoot`, never under `DisposableRoot`.

Recommended retained artifact identity:

```text
finalization-checkpoints.jsonl
```

or an equivalently durable structured artifact.

Each successful checkpoint record must contain at least:

```text
Checkpoint
TimestampUtc
RunnerSHA256
ValidatorSHA256
DurableRoot
Status
EvidenceReference
```

Required checkpoint names, exactly:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

Checkpoint persistence must be fail closed:

```text
checkpoint write failure -> validator failure
checkpoint write failure -> no later checkpoint
checkpoint write failure -> no PASS
```

Stdout messages may remain as diagnostics, but stdout alone is not acceptance evidence.

## Self-hash/freeze handling

The validator cannot truthfully persist its final validator hash before its bytes are frozen.

Therefore:

1. complete validator edits first;
2. freeze validator bytes;
3. compute fresh `ValidatorSHA256`;
4. supply/bind that frozen hash to execution using the validator's existing governed mechanism or a narrow non-production parameter/environment binding;
5. require the validator to verify its retained copy/hash against that value;
6. checkpoint records must contain the fresh frozen validator hash.

Do not edit validator bytes after hash freeze.

Any byte change after freeze requires a new hash and full restart.

## Checkpoint ordering

The durable artifact must prove monotonic order:

```text
PRE_CLEANUP
<
CLEANUP_COMPLETE
<
BUILD_LEDGER
<
SERIALIZE
<
PUBLISH
<
REOPEN
```

No duplicates for a successful terminal run unless the validator explicitly records attempts with unique sequence numbers and the final ledger unambiguously identifies the successful sequence.

Prefer one successful record per required checkpoint.

## Placement semantics

Persist checkpoints only after the corresponding condition is true:

```text
PRE_CLEANUP
after all required pre-cleanup evidence is durably retained.

CLEANUP_COMPLETE
after DisposableRoot absence and DurableRoot survival are proven.

BUILD_LEDGER
after final post-cleanup observations/reference resolution are complete and the final ledger object is built.

SERIALIZE
after final ledger JSON is fully serialized to the same-directory temporary file, closed, and parse-checked.

PUBLISH
after atomic publication to final sv01-sv36-ledger.json succeeds and final file existence is proven.

REOPEN
after the final ledger is reopened, parsed, and terminal invariants are reverified.
```

## Atomic checkpoint persistence

Use a WinPS 5.1-compatible durable write strategy.

The checkpoint artifact itself must survive process exit and disposable cleanup.

If using JSONL append:

```text
write one complete serialized record per append
flush/close before continuing
treat append failure as terminating
```

If using whole-file atomic replacement:

```text
same durable directory temp file
serialize complete state
close
atomic replace/move
reopen/verify as appropriate
```

Do not use an in-memory-only collection as the retained proof.

## Windows PowerShell compatibility

Binding target:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only syntax/cmdlets or unsupported newer .NET assumptions.

Require validator parser errors = 0 after correction.

## Runner preservation

Before and after work require:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Runner mismatch:

```text
STOP
CLASSIFICATION = RUNNER_IDENTITY_MISMATCH
```

No runner edit is authorized.

## Fresh validator and full rerun

After checkpoint-writer correction:

```text
FreshValidatorSHA256 = REQUIRED
Fresh DurableRoot = REQUIRED
Fresh DisposableRoot = REQUIRED
SV01-SV36 restart from SV01 = REQUIRED
```

No acceptance credit may be carried from any prior validator hash or prior durable root.

## Terminal execution requirement

Once the corrected validator passes parser/preflight gates, execute the complete fresh structural run.

Do not return merely:

```text
checkpoint writer implemented
parser passed
new validator hash generated
```

Continue through finalization unless a real blocker occurs.

Successful finalization must prove:

```text
SV records = 36
SV failures = 0
EvidenceReference count = 36
Resolved = 36
Unresolved = 0
PRE_CLEANUP retained = PASS
CLEANUP_COMPLETE retained = PASS
BUILD_LEDGER retained = PASS
SERIALIZE retained = PASS
PUBLISH retained = PASS
REOPEN retained = PASS
final sv01-sv36-ledger.json exists
final ledger reopens/parses
GitDiffCheckOutput metadata present
runner hash reverified
validator hash reverified
DisposableRoot absent
DurableRoot survives
W5 invoked = NO
W5 RunId = NO
```

## Iterative permission

Terra may iteratively correct ordinary validator-only issues discovered while implementing/executing this source-aware remediation.

Every validator byte change requires:

```text
fresh ValidatorSHA256
fresh roots
complete SV01-SV36 restart
```

Authorized defect classes:

```text
VALIDATOR_DEFECT
CHECKPOINT_PERSISTENCE_DEFECT
PATCH_CONTEXT_DRIFT
LEDGER_FINALIZATION_DEFECT
SERIALIZATION_DEFECT
ATOMIC_PUBLICATION_DEFECT
REOPEN_DEFECT
EVIDENCE_REFERENCE_DEFECT
LEDGER_METADATA_DEFECT
POWERSHELL_5_1_COMPATIBILITY_DEFECT
LOCAL_ORCHESTRATION_DEFECT
```

Do not stop at an ordinary locally correctable validator defect.

## Mandatory escalation

Stop only if resolution requires:

```text
runner change
tracked production-source change
Architecture-B contract change
P01-P20/SV01-SV36/G01-G13 change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/Docker/GHCR/GitHub mutation
other production/external mutation
```

## Prohibited

```text
blind retry of stale patch context
manual checkpoint fabrication
stdout-only checkpoint acceptance
manual final-ledger fabrication
manual promotion of provisional ledger
old-hash PASS carry-forward
runner edit
production edit
staging/commit/push
GitHub/Azure/Docker/GHCR mutation
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

Disposable validator/evidence files outside tracked repository content are permitted and must be accounted for separately.

## Required PASS markers

Only after the complete fresh run reaches terminal verification emit:

```text
RELEASE 1.12 WP04 — TERRA PATH-B SOURCE-AWARE CHECKPOINT REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FRESH VALIDATOR SHA256: <NEW_HASH>
RELEASE 1.12 WP04 — R1 RETAINED CHECKPOINT ARTIFACT: PASS
RELEASE 1.12 WP04 — R1 FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
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
PreEditValidatorSHA256
FreshValidatorSHA256
RunnerSHA256
SourceStructureObservationPath
CorrectionSummary
ValidatorPath
DurableRoot
DisposableRoot
CheckpointArtifactPath
CheckpointSequenceObserved
LedgerPath
ProvisionalLedgerPath
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
SVRecordCount
SVFailedCount
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

The next artifact is a fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the unchanged runner and fresh validator hash.

W5 remains prohibited until Luna PASS.

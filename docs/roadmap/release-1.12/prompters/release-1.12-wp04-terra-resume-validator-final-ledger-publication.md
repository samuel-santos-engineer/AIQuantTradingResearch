# Release 1.12 WP04 — Terra Resume Validator Final-Ledger Publication

## Authority identity

**Selected execution model: GPT-5.6 Terra**

Terra owns this continuation of the already-authorized Architecture-B validator remediation. Luna retains contract/acceptance authority. GPT-5.6 Sol is supporting analysis only.

## Current candidate

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Current ValidatorSHA256 =
6B3B988C8D8A90C2875935567AF99FEF7AF3601C55CB6F8252B2A6ECF4B13193

DurableRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\1247217974394729b1163c60eea1229e
```

Reported retained evidence:

```text
SV01-SV36 per-check evidence artifacts = PRESENT
evidence manifest = PRESENT
evidence resolution report = PRESENT
parser/hash evidence = PRESENT
frozen runner = PRESENT
fresh validator = PRESENT
final sv01-sv36-ledger.json = ABSENT
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

Classification:

```text
VALIDATOR FINALIZATION ISSUE
Production defect = NO
Runner defect = NO
```

## Mission

Continue the existing Terra remediation and reach a terminal result.

Do not stop merely because the evidence artifacts already exist.

First determine whether final ledger publication can truthfully complete using the exact frozen validator bytes/hash above without modifying validator code and without retroactively manufacturing acceptance evidence.

### Path A — no validator-byte change required

If the existing validator already contains the required finalization implementation and the prior run merely ended before executing it:

1. verify RunnerSHA256 exactly;
2. verify ValidatorSHA256 exactly;
3. verify all 36 retained per-SV artifacts belong to this exact runner/validator pair;
4. verify manifest and resolution report belong to this exact pair;
5. resume/re-execute the validator's existing finalization path as permitted by its implementation;
6. do not manually synthesize or patch the ledger outside that path;
7. publish the final durable ledger atomically;
8. reopen and parse it;
9. verify exactly 36 SV records and 0 failures;
10. verify 36 evidence references and 0 unresolved;
11. verify every reference resolves after cleanup;
12. verify all mandatory metadata is present, including `GitDiffCheckOutput`;
13. reverify runner and validator hashes;
14. verify disposable root absent and durable artifacts survive;
15. verify staged paths 0 and fresh `git diff --check` exit 0;
16. emit terminal PASS only if every gate passes.

### Path B — validator-byte change required

If the validator does not contain a sufficient finalization implementation, or code must change for any reason:

```text
6B3B988C...B13193 becomes historical evidence only.
```

Then:

1. make the narrowest validator-only correction;
2. do not change runner bytes;
3. compute a fresh ValidatorSHA256;
4. create a fresh disposable/durable iteration;
5. restart complete SV01-SV36 from SV01;
6. generate all evidence anew for the fresh validator hash;
7. complete cleanup, resolution, ledger finalization, reopen/reparse, and hash reverification;
8. no cross-validator-hash PASS carry-forward.

Terra may continue iterating through ordinary validator/finalization/evidence defects under this authority until PASS or a mandatory escalation boundary.

## Frozen runner

Runner bytes must remain unchanged:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Any runner identity mismatch:

```text
STOP
CLASSIFICATION = RUNNER_IDENTITY_MISMATCH
```

Do not correct the runner here.

## Final ledger requirements

Final `sv01-sv36-ledger.json` must contain exactly 36 unique SV records, zero failures, and required fields per record:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Top-level/finalization metadata must include at least:

```text
RecordType
RunnerSHA256
EvidenceWriterOrValidatorSHA256
PowerShellVersion
SVRecordCount
SVFailedCount
FirstFailedSV
RuntimePredicatePassClaims
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
DisposableRoot
DurableRoot
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
EvidenceManifestPath
EvidenceReferenceResolutionReportPath
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
ExactMutationAccounting
```

`GitDiffCheckOutput` must be present even if empty.

## Evidence-resolution terminal gate

After disposable cleanup, perform/reconcile the final resolution pass:

```text
SV records = 36
EvidenceReference count = 36
ResolvedEvidenceReference count = 36
UnresolvedEvidenceReference count = 0
```

Every reference must resolve to retained evidence and its claimed locator.

The evidence manifest and resolution report must themselves survive and be resolvable.

## Windows PowerShell / Git

Require:

```text
Windows PowerShell = 5.1.26100.9444
runner parser errors = 0
validator parser errors = 0
```

Capture `git diff --check` operation-scoped under WinPS 5.1, preserving output and immediate `$LASTEXITCODE`.

CRLF advisories may be `ADVISORY_ONLY` only with exit code 0.

## Prohibited

```text
manual patching of old ledger
manufactured PASS records
cross-validator-hash PASS carry-forward
runner modification
tracked production-source modification
staging/commit/push
GitHub mutation
Azure mutation
Docker/GHCR mutation
Twelve Data secret configuration
governed W5 execution
governed W5 RunId allocation
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

## Mandatory escalation

Stop only if:

```text
CONTRACT_AMBIGUITY
Architecture-B change required
P01-P20 or SV01-SV36 change required
acceptance weakening required
P19/P20 reinterpretation required
cross-hash evidence composition required
PRODUCTION_DEFECT
tracked production-source change required
external mutation required
```

Ordinary validator/finalizer/evidence defects remain Terra-correctable.

## PASS markers

Only after complete terminal validation emit:

```text
RELEASE 1.12 WP04 — TERRA ARCHITECTURE-B FINAL-LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <HASH>
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL LEDGER: PASS
RELEASE 1.12 WP04 — R1 GITDIFFCHECKOUTPUT METADATA: PRESENT
RELEASE 1.12 WP04 — R1 POST-CLEANUP FINALIZATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 VALIDATOR HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

## Required handoff

Return:

```text
ExecutionPath = A_NO_VALIDATOR_CHANGE | B_FRESH_VALIDATOR
RunnerSHA256
FinalValidatorSHA256
DurableRoot
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
GitDiffCheckClassification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
ValidatorExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
RuntimePredicatePassClaims
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
```

## Stop

After PASS, STOP.

The next gate is a fresh **GPT-5.6 Luna Architecture-B final structural reconciliation** bound to the unchanged runner hash and the final validator hash produced here.

W5 remains prohibited until that Luna reconciliation passes.

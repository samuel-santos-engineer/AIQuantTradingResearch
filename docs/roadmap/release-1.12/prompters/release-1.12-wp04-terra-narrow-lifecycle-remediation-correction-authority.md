# GPT-5.6 Terra — Release 1.12 WP04 Narrow Lifecycle Remediation Correction Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns contract, architecture, reconciliation, acceptance criteria, and the D2 correction boundary.
- **GPT-5.6 Terra** — PRIMARY: implement the narrow wrapper-only correction needed to complete the governed pre-restoration evidence checkpoint and prove error precedence.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Starting source anchor:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure state must remain unchanged.

Retained diagnostic evidence must remain preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding reconciliation result

The prior implementation passed these contracts:

```text
Immediate backward compatibility = PASS
Deferred mode = PASS
RestoreOnly mode = PASS
restoration descriptor safety = PASS
outer-finally restoration = PASS
explicit restart remediation = PASS
null/absent restoration = PASS
RB2 actual RunId capture = PASS
```

Still not proven:

```text
ERROR PRECEDENCE = NOT_PROVEN
WINDOWS POWERSHELL 5.1 FULL LIFECYCLE GATE = NOT_PROVEN
PRE-RESTORATION EVIDENCE CHECKPOINT = NOT_PROVEN
```

Publication readiness:

```text
REMEDIATION_REQUIRED
```

Decision:

```text
D2 — NARROW LOCAL REMEDIATION REQUIRED
```

## 3. Proven blocking defect

The canonical wrapper currently reaches restoration after preserving only part of the governed evidence state.

It does not yet deterministically complete, before restoration:

```text
raw Azure log archive retrieval or durable retrieval-failure record
raw archive path/size/SHA-256 metadata when retrieved
fresh-window extraction
poll-observation retention
deepest execution-boundary derivation
complete EVIDENCE_CHECKPOINT=PASS|FAIL
```

Therefore the wrapper can still restore qualification settings before the full governed evidence checkpoint exists.

## 4. Correction objective

Make the canonical initialize wrapper own the full pre-restoration evidence checkpoint.

Required ordering:

```text
Deferred qualification/poll terminal
→ actual RunId binding
→ helper transcript retained
→ poll observations retained
→ D3 payload retained if present
→ raw Azure archive retrieval attempted
→ archive retained OR retrieval failure durably recorded
→ archive SHA-256/size/path recorded if retrieved
→ fresh-window extraction completed/retained
→ deepest boundary derived
→ EVIDENCE_CHECKPOINT=PASS|FAIL durably recorded
→ RestoreOnly attempted exactly once from finally
```

No restoration may start before checkpoint status is deterministically established.

## 5. Exact tracked allowlist

Authorize exactly one tracked path:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Mutation counts:

```text
CREATE = 0
MODIFY = 1
DELETE = 0
TRACKED PATH COUNT = 1
```

Do not modify:

```text
verify-persistent-sqlite-webapp.ps1
```

The helper contract already passed reconciliation.

If implementation proves the helper must change, STOP and return to Luna rather than expanding scope.

## 6. No source/runtime/image remediation

Binding:

```text
source runtime remediation = NO
helper remediation = NO under this correction
new image = NO
Docker/GHCR = NO
```

This remains host-side orchestration only.

## 7. Evidence root ownership

The wrapper must use a durable non-repository evidence directory.

It must bind durable metadata to the **actual helper-generated RunId** under RB2.

A local pre-run correlation identifier may exist only as:

```text
CorrelationId
```

or equivalent and must never be used as qualification attribution.

## 8. Poll-observation retention

Before restoration, persist all safe poll observations required to explain terminal behavior:

```text
timestamp
HTTP status or transport class
terminal/retry classification
elapsed time
actual RunId where available
```

Do not persist tokens or authorization values.

The retained poll observations must be sufficient for later Luna reconciliation without depending on transient console output.

## 9. Raw Azure archive retrieval

The wrapper must attempt supported raw App Service log-archive retrieval before restoration.

Required outcomes:

### Retrieval success

Persist:

```text
archive local path
archive byte length
archive SHA-256
retrieval UTC
archive readability/listing result
```

Retain the raw archive unchanged.

### Retrieval failure

Persist a durable terminal record:

```text
ARCHIVE_RETRIEVAL=FAIL
classification
timestamp
safe error summary
```

Do not treat archive retrieval failure as permission to skip restoration.

Do not make a second diagnostic/qualification run.

## 10. Fresh-window extraction

When archive retrieval succeeds:

```text
extract from retained raw archive
do not overwrite/delete raw archive
filter using governed capture-start UTC
retain relevant platform/container/WP04_DIAG records
bind attribution to actual RunId where possible
persist extraction result
```

Required terminal extraction state:

```text
PASS
EMPTY
FAIL
```

`EMPTY` is a valid preserved result.

## 11. Deepest-boundary derivation

Before restoration, derive exactly one boundary using retained target-attributable evidence:

```text
B0 — AZURE_MUTATION_ONLY
B1 — CONTAINER_STARTED
B2 — QUALIFICATION_ENTERED
B3 — SQLITE_QUALIFICATION_STARTED
B4 — ARTIFACT_WRITE_SUCCEEDED
B5 — LISTENER_STARTING
B6 — LISTENER_STARTED
B7 — REQUEST_ARRIVED
B8 — HANDLER_ENTERED
B9 — EVIDENCE_RETRIEVAL_SUCCEEDED
BX — NOT_PROVEN
```

Persist:

```text
DEEPEST_BOUNDARY=<value>
```

No acceptance credit is assigned by this local implementation authority.

## 12. Complete evidence checkpoint

Implement one explicit checkpoint record before restoration:

```text
ACTUAL_RUN_ID=<value|NOT_PROVEN>
HELPER_TERMINAL_RETAINED=PASS|FAIL
HELPER_TRANSCRIPT_RETAINED=PASS|FAIL
POLL_OBSERVATIONS_RETAINED=PASS|FAIL
D3_PAYLOAD_STATE=RETAINED|NOT_OBSERVED|FAIL
RAW_ARCHIVE_STATE=RETAINED|RETRIEVAL_FAILED|FAIL
ARCHIVE_METADATA_STATE=PASS|NOT_APPLICABLE|FAIL
FRESH_EXTRACTION_STATE=PASS|EMPTY|FAIL|NOT_APPLICABLE
DEEPEST_BOUNDARY=<B0..B9|BX>
EVIDENCE_CHECKPOINT=PASS|FAIL
```

Checkpoint must be written durably before `RestoreOnly`.

## 13. Checkpoint semantics

`EVIDENCE_CHECKPOINT=PASS` requires:

```text
actual RunId known
helper terminal retained
transcript retained
poll observations retained
D3 state known
archive retained OR terminal archive retrieval failure durably recorded
archive metadata recorded if archive retained
fresh extraction retained if archive retained
deepest boundary derived
```

A terminal retrieval failure can still produce checkpoint PASS if the failure itself is durably preserved and all other required state is retained.

The checkpoint concerns **reconcilability**, not diagnostic success.

## 14. Outer-finally contract

Preserve:

```text
RestoreOnly attempted exactly once from finally
```

But structure control flow so the protected body always attempts to establish the checkpoint first.

Equivalent required shape:

```powershell
$checkpointEstablished = $false
try {
    # Deferred qualification
    # preserve terminal output
    # archive retrieval
    # extraction
    # deepest-boundary derivation
    # write checkpoint
    $checkpointEstablished = $true
}
catch {
    # durably record evidence-phase failure when possible
    # durably write EVIDENCE_CHECKPOINT=FAIL when possible
}
finally {
    # exactly one RestoreOnly attempt
}
```

Do not add a second restoration attempt.

## 15. Error precedence contract

This correction must prove final lifecycle result precedence.

Track at least three independent statuses:

```text
QualificationResult
EvidenceCheckpointResult
RestorationResult
```

Required final precedence:

```text
if RestorationResult = FAIL
    FinalLifecycleResult = RESTORATION_FAILED
else if EvidenceCheckpointResult = FAIL
    FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
else
    FinalLifecycleResult reflects QualificationResult
```

The original qualification terminal classification must still be retained separately even when a later result dominates.

Do not mask the original result.

## 16. Full lifecycle PowerShell 5.1 gate

Using local mocks/stubs only, validate the complete wrapper flow under:

```text
Windows PowerShell 5.1.26100.9444
```

No Azure mutation is allowed.

Required full-lifecycle scenarios:

### L1 — success path

```text
Deferred qualification terminal success
archive retrieval success
fresh extraction success
checkpoint PASS
RestoreOnly PASS
final result preserves qualification success
```

### L2 — archive retrieval failure

```text
archive retrieval terminal failure durably recorded
checkpoint remains reconcilable
RestoreOnly still executes once
final result preserves evidence/archive classification
```

### L3 — extraction failure

```text
archive retained
extraction FAIL durably recorded
checkpoint FAIL
RestoreOnly executes once
FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
```

### L4 — qualification/poll failure

```text
qualification terminal failure retained
evidence preservation still attempted
RestoreOnly executes once
qualification failure remains separately visible
```

### L5 — restoration failure

```text
qualification/evidence state retained
RestoreOnly FAIL
FinalLifecycleResult = RESTORATION_FAILED
original qualification/evidence results remain retained
```

### L6 — unexpected exception before normal checkpoint completion

```text
best-effort checkpoint FAIL durably recorded
RestoreOnly executes once
no second restoration attempt
```

## 17. Archive fixture validation

Use synthetic local ZIP/log fixtures only.

Validate:

```text
SHA-256 computation
byte-length capture
archive readability
fresh-window extraction
EMPTY classification
diagnostic-line extraction
deepest-boundary derivation
```

No live Azure log download during this authority.

## 18. Secret hygiene

All local fixtures and outputs must contain no real secrets.

Scan changed script plus local validation outputs for accidental exposure patterns.

Expected:

```text
secret findings = 0
```

Do not echo or persist:

```text
HttpEvidenceToken
TwelveData__ApiKey
Authorization header
registry credentials
connection strings
cookies
```

## 19. Existing H1 compatibility

This correction must not alter the already-accepted helper contract.

Confirm:

```text
Immediate = unchanged
Deferred = unchanged
RestoreOnly = unchanged
LifecycleAction=None = unchanged
RB2 helper ownership = unchanged
```

The correction is wrapper sequencing/evidence ownership only.

## 20. Local-only authority

Forbidden:

```text
Azure app-setting mutation
Azure restart
Azure logging mutation
new diagnostic run
acceptance retry
Twelve Data secret configuration
git add
git commit
git push
PR
merge
Docker build
GHCR publication
GitHub/Project/issue/milestone mutation
```

## 21. Tracked mutation audit

At completion prove:

```text
exactly 1 authorized tracked modified path
0 unauthorized tracked paths
0 tracked creates
0 tracked deletes
0 staged paths
```

Run:

```text
git diff --check
```

Expected:

```text
PASS
```

Unrelated untracked user content remains untouched.

## 22. Required output

Return:

- exact changed path;
- concise diff behavior;
- poll-retention design;
- archive retrieval/failure design;
- archive metadata design;
- fresh-window extraction design;
- deepest-boundary derivation;
- complete checkpoint semantics;
- error-precedence implementation;
- L1–L6 results;
- Windows PowerShell 5.1 parse/binding/full-lifecycle results;
- secret scan;
- tracked mutation audit;
- whether all previously NOT_PROVEN gates are now proven;
- readiness for Luna reconciliation.

## 23. Terminal markers

`RELEASE 1.12 WP04 — NARROW LIFECYCLE REMEDIATION CORRECTION: PASS`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — POLL OBSERVATIONS RETENTION: IMPLEMENTED`

`RELEASE 1.12 WP04 — RAW ARCHIVE RETRIEVAL CHECKPOINT: IMPLEMENTED`

`RELEASE 1.12 WP04 — RAW ARCHIVE FAILURE DURABILITY: IMPLEMENTED`

`RELEASE 1.12 WP04 — ARCHIVE SHA256/SIZE/PATH RETENTION: IMPLEMENTED`

`RELEASE 1.12 WP04 — FRESH-WINDOW EXTRACTION: IMPLEMENTED`

`RELEASE 1.12 WP04 — DEEPEST-BOUNDARY DERIVATION: IMPLEMENTED`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: IMPLEMENTED`

`RELEASE 1.12 WP04 — RESTORE-ONLY ATTEMPT COUNT: 1`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FULL LIFECYCLE GATE: PASS`

`RELEASE 1.12 WP04 — LOCAL FULL-LIFECYCLE VALIDATION L1-L6: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — AUTHORIZED TRACKED MODIFY COUNT: 1`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — NARROW LIFECYCLE REMEDIATION CORRECTION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-CORRECTION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA NARROW LIFECYCLE REMEDIATION CORRECTION COMPLETE`

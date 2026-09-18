# GPT-5.6 Terra — Release 1.12 WP04 Corrected Bounded Qualification Diagnostic Capture Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns contract, architecture, reconciliation, acceptance criteria, and the M1 evidence-preservation protocol.
- **GPT-5.6 Terra** — PRIMARY: execute exactly one corrected, diagnostic-only Azure qualification capture.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Governed deployed image:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure state:

```text
six qualification settings = ABSENT
diagnostic logging = DISABLED
WEBSITES_PORT = 8501
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

## 2. Governing Luna reconciliation

Binding:

```text
P6 — MULTI-LAYER EVIDENCE-PRESERVATION DEFECT
M1 — PROCEDURE-ONLY MULTI-LAYER EVIDENCE PRESERVATION
procedure-only correction sufficient = YES
tracked helper remediation = NO
Azure log procedure remediation = YES
new image = NO
logging readiness gate = REQUIRED
original timeout cause = T7
```

This authority implements that procedure only.

## 3. Mission

Execute exactly one fresh **execution-depth-only** qualification diagnostic attempt.

The mission is to preserve enough durable evidence for Luna to determine the deepest runtime boundary reached.

This is not an acceptance retry.

Binding:

```text
QUALIFICATION ACCEPTANCE ATTEMPTS = 0
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN_BY_THIS_AUTHORITY
REOPEN = NOT_AUTHORIZED
```

Even if a valid D3 payload is observed, return it as diagnostic evidence only.

## 4. Fresh RunId

Generate exactly one:

```text
initialize-<fresh-guid-N>
```

Never generate a second RunId under this authority.

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
initialize-fa16107e99d640d88bb9de91ef0da8ab
initialize-f4da0cd4cf44419aa900f849c2358212
```

The new RunId becomes permanently single-use regardless of result.

## 5. Evidence directory — before Azure mutation

Create a durable, non-repository directory:

```text
$EvidenceRoot = Join-Path $env:TEMP ("AIQuantTradingResearch\wp04\" + $RunId)
```

Prove:

```text
EvidenceRoot exists
EvidenceRoot is not under repository root
EvidenceRoot is writable
```

Do not delete this directory at the end.

Return its exact path.

## 6. Required evidence files

Before Azure mutation, establish paths for:

```text
run-metadata.txt
local-dry-run.txt
helper-transcript.txt
poll-observations.txt
azure-logging-prestate.json
azure-logging-readiness.txt
azure-log-archive.zip
archive-metadata.txt
fresh-diagnostic-lines.txt
restoration-observations.txt
final-state.txt
```

Equivalent names are allowed only if every evidence role remains explicit.

## 7. Local synthetic dry-run gate

Before any Azure mutation, prove the evidence machinery locally using synthetic non-secret data.

The dry run must verify:

```text
directory/file creation
UTF-8 text write/read
exact synthetic RunId preservation
stdout capture
stderr capture
exit-code capture
timestamp persistence
SHA-256 hashing
archive/file retention
fresh-line output write
final-state output write
```

Use Windows PowerShell 5.1-compatible mechanisms.

No Azure command may mutate state during this gate.

Write the result durably to:

```text
local-dry-run.txt
```

Required:

```text
LOCAL EVIDENCE DRY RUN = PASS
```

Otherwise STOP with zero Azure mutations.

## 8. Durable run metadata checkpoint

Before Azure mutation write and read back:

```text
RunId=<fresh-run-id>
CapturePurpose=EXECUTION_DEPTH_ONLY
SourceCommit=2532f6abd4677edfb205c26c083a534783038979
ImageDigest=sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
EvidenceRoot=<exact-path>
MetadataUtc=<UTC ISO-8601>
```

If exact RunId read-back fails:

```text
STOP BEFORE AZURE MUTATION
```

## 9. Read-only Azure preflight

Before mutation prove:

```text
HEAD = exact governed source commit
wrapper provenance = PASS
linuxFxVersion = exact governed image
WEBSITES_PORT = 8501
startup override = none
registry username/password = absent
SCM basic auth = false
FTP basic auth = false
App Service = Running
filesystem/container logging = disabled
web-server logging = disabled
detailed errors = disabled
failed-request tracing = disabled
all six qualification names = ABSENT
```

The six names:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

Any present/null/empty qualification name fails preflight.

Persist sanitized logging pre-state to:

```text
azure-logging-prestate.json
```

Do not persist secrets.

## 10. Twelve Data boundary

Do not configure, alter, print, synthesize, or require:

```text
TwelveData__ApiKey
```

Normal public-root `503` remains a downstream WP05 blocker and is not a diagnostic gate.

## 11. Authorized Azure mutation sequence

Exactly one logical sequence:

```text
1. enable filesystem container/application logging
2. apply qualification settings for the fresh diagnostic RunId
3. restart exactly once
4. execute bounded helper/evidence polling while durably capturing output
5. retrieve and retain raw Azure log archive
6. extract fresh-window evidence from retained archive
7. restore qualification settings
8. verify exact ABSENT
9. restore logging
10. verify exact logging pre-state
```

No second attempt.

No blind repeated restart.

Count actual Azure mutations.

## 12. Logging enablement

Enable only the narrow filesystem/container/application logging needed to capture custom-container stdout/stderr.

Do not enable:

```text
HTTP/web-server logging
detailed errors
failed-request tracing
```

After the enable mutation, perform read-only configuration read-back.

Required:

```text
requested filesystem/container logging state = enabled
```

If not proven, restore if necessary and stop.

## 13. Logging readiness gate

After successful logging read-back:

```text
WP04_LOGGING_ENABLED_UTC=<UTC ISO-8601>
```

Persist it to `azure-logging-readiness.txt`.

Then wait:

```text
15 seconds
```

This is the Luna-approved bounded propagation stabilization interval.

Do not exceed 30 seconds.

After the wait, perform another read-only logging-state check.

Required:

```text
logging still enabled
```

If it is not, stop before qualification mutation and restore logging.

## 14. Diagnostic capture start

After logging readiness succeeds and immediately before qualification settings are applied, persist:

```text
WP04_DIAGNOSTIC_CAPTURE_START_UTC=<UTC ISO-8601>
```

to both:

```text
run-metadata.txt
azure-logging-readiness.txt
```

This timestamp is the fresh evidence boundary.

## 15. Qualification activation

Use only the governed qualification configuration:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__RunId=<fresh-run-id>
PersistentSqliteQualification__HttpEvidenceToken=<fresh-high-entropy-token>
```

Use:

```text
PersistentSqliteQualification__EvidenceOutputPath
```

only if required by the existing governed helper/runtime.

Never print or persist the token.

Do not configure Twelve Data.

## 16. Restart

Perform exactly one App Service restart.

Persist only safe restart metadata:

```text
request UTC
result/status
```

Restart count must be:

```text
1
```

unless preflight/readiness fails before restart, in which case it is `0`.

## 17. Durable helper invocation

Invoke the existing governed wrapper/helper.

Do not reconstruct its qualification semantics.

Capture output durably under Windows PowerShell 5.1.

The evidence must survive console closure and Azure restoration.

At minimum preserve:

```text
safe stdout
safe stderr
process/helper exit code
elapsed time
poll observations
terminal markers/fields
exact RunId if emitted
```

Write durable combined safe output to:

```text
helper-transcript.txt
```

and safe poll observations to:

```text
poll-observations.txt
```

If separate stdout/stderr files are easier under Windows PowerShell 5.1, they may additionally be retained.

Do not rely solely on `Tee-Object` unless stderr and exit-code semantics are explicitly preserved.

## 18. Required helper terminal attribution

Search the retained helper evidence for:

```text
initialize-<fresh-guid-N>
```

and specifically for terminal RunId if emitted.

Classify:

```text
HELPER TRANSCRIPT RUNID ATTRIBUTION = PASS | FAIL
```

Failure does not authorize another run.

Continue to log retrieval and restoration.

## 19. Polling semantics

Preserve existing governed helper semantics:

```text
404 = retryable
503 = retryable
generic 5xx = not generically retryable
transport NONE = only as already governed
Timeout = terminal under existing hard deadline
```

Do not widen the deadline.

Persist safe poll timestamps/status/classes.

## 20. Evidence payload

If the endpoint returns a payload, preserve a sanitized copy containing only the governed 11 fields:

```text
RecordVersion
Phase
RunId
DatabasePathIdentity
SchemaVersion
JournalMode
AcceptedEvidenceIdentity
AcceptedEvidenceCount
IntegrityCheck
QuickCheck
PersistenceContinuity
```

Never treat it as acceptance credit here.

## 21. Raw Azure log retrieval checkpoint

After helper terminal state, while logging remains enabled:

Retrieve the supported App Service filesystem/container log archive.

Required local target:

```text
azure-log-archive.zip
```

Do not use Kudu `/home`.

Do not inspect SQLite.

Do not use Python for database evidence.

If retrieval fails, persist the terminal retrieval failure in `archive-metadata.txt`, then proceed to restoration.

Do not make a second diagnostic attempt.

## 22. Raw archive durability gate

If archive retrieval succeeds, prove:

```text
file exists
length > 0
archive can be opened/listed
```

Compute:

```text
SHA-256
```

using Windows PowerShell 5.1-compatible `Get-FileHash`.

Persist:

```text
ArchivePath=
ArchiveBytes=
ArchiveSha256=
ArchiveRetrievedUtc=
ArchiveReadable=PASS|FAIL
```

to:

```text
archive-metadata.txt
```

Do not modify or delete the raw archive afterward.

## 23. Fresh-window extraction

Extract from the retained raw archive into a separate local extraction directory.

Do not overwrite the raw archive.

Filter records using:

```text
timestamp >= WP04_DIAGNOSTIC_CAPTURE_START_UTC
```

Then preserve relevant fresh records for:

```text
fresh RunId
WP04_DIAG_*
QUALIFICATION_ENTERED
SQLITE_QUALIFICATION_STARTED
ARTIFACT_WRITE_SUCCEEDED
LISTENER_STARTING
LISTENER_STARTED
REQUEST_ARRIVED
HANDLER_ENTERED
EVIDENCE_RETRIEVAL_SUCCEEDED
LISTENER_STOPPING
LISTENER_STOPPED
WORKER_EXITING
container/platform startup or exit records
```

Write sanitized results to:

```text
fresh-diagnostic-lines.txt
```

## 24. Attribution rules

Prefer exact RunId attribution.

If a diagnostic line lacks RunId, it may receive target credit only if all are proven:

```text
record timestamp is within fresh capture window
record belongs to the container instance created by the one authorized restart
no other qualification run occurred in the window
source/context unambiguously identifies the qualification container stream
```

State the attribution basis.

Do not credit historical lines.

## 25. Empty archive/fresh window

If archive exists and is readable but no records fall in the fresh window, persist:

```text
FRESH_WINDOW_RESULT=EMPTY
```

If fresh records exist but no application diagnostic events exist:

```text
APPLICATION_DIAGNOSTIC_EVENTS=NONE_OBSERVED
```

These are valid preserved diagnostic outcomes.

## 26. Evidence checkpoint before restoration

Before restoring logging, require durable local evidence status for:

```text
run metadata = PASS
local dry run = PASS
helper transcript = RETAINED
poll observations = RETAINED
archive = RETAINED or terminal retrieval failure durably recorded
fresh extraction = RETAINED
```

Persist:

```text
EVIDENCE_CHECKPOINT_BEFORE_RESTORATION=PASS
```

If any expected artifact cannot be written, still restore Azure state, but classify diagnostic evidence preservation as FAIL.

## 27. Qualification restoration

Restore all six qualification settings to exact pre-state.

Because all six were absent:

```text
target = ABSENT
```

After delete/restoration, read back.

If any appears `PRESENT_NULL` or otherwise present:

- perform only read-only rechecks;
- maximum 120 seconds;
- minimum 5-second interval;
- do not issue a second delete/write.

Persist every sanitized read-back timestamp/state to:

```text
restoration-observations.txt
```

Required final:

```text
all six = ABSENT
```

## 28. Logging restoration

Only after evidence checkpoint and qualification restoration:

Restore exact logging pre-state:

```text
filesystem/container logging = disabled
web-server logging = disabled
detailed errors = disabled
failed-request tracing = disabled
```

Read back and persist result.

No normal-mode recovery restart is authorized.

## 29. Final-state evidence

Persist to:

```text
final-state.txt
```

the sanitized final proof:

```text
six qualification names = ABSENT
logging = DISABLED
linuxFxVersion = exact governed image
WEBSITES_PORT = 8501
startup override = none
registry credentials = absent
SCM basic auth = false
FTP basic auth = false
restart count
Azure mutation count
evidence directory path
```

No secret values.

## 30. Evidence retention

At procedure completion:

```text
EvidenceRoot MUST remain on disk
raw archive MUST remain on disk if retrieved
helper transcript MUST remain on disk
fresh extraction MUST remain on disk
```

Do not clean up evidence.

Return the exact EvidenceRoot path to the user for Luna reconciliation.

## 31. Deepest execution boundary

Using only preserved target-attributable evidence, classify:

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

## 32. Diagnostic result

Classify exactly one:

```text
C1 — CONTAINER_OR_PLATFORM_STARTUP_FAILURE_PROVEN
C2 — QUALIFICATION_RUNTIME_FAILURE_BEFORE_LISTENER_PROVEN
C3 — LISTENER_STARTUP_FAILURE_PROVEN
C4 — REQUEST_PATH_FAILURE_PROVEN
C5 — HANDLER_OR_RESPONSE_FAILURE_PROVEN
C6 — HELPER_TRANSPORT_OR_DEADLINE_FAILURE_PROVEN
C7 — DIAGNOSTIC_PATH_SUCCEEDED
C8 — EVIDENCE_INSUFFICIENT
C9 — EVIDENCE_PRESERVATION_FAILED
```

Use `C9` if the corrected evidence-preservation checkpoint itself fails.

## 33. No acceptance credit

Regardless of C-class:

```text
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN_BY_THIS_AUTHORITY
QUALIFICATION ACCEPTANCE ATTEMPTS = 0
```

Any observed payload goes to Luna.

## 34. Mutation boundary

Authorized:

```text
temporary filesystem/container logging enable/restore
temporary qualification settings apply/restore
exactly one restart
```

Forbidden:

```text
TwelveData secret mutation
source edits
helper edits
Git staging/commit/push
Docker build
GHCR publication
image change
port change
startup override change
registry credential change
SCM/FTP auth change
reopen
acceptance retry
PR/issue/Project/milestone mutation
WP05 start
```

Return exact actual Azure mutation count.

## 35. Required return evidence

Return:

- fresh RunId;
- EvidenceRoot;
- local dry-run result;
- metadata checkpoint;
- preflight;
- logging enable/readiness timestamps;
- capture-start timestamp;
- restart result/count;
- helper transcript path and RunId-attribution result;
- helper terminal fields;
- poll observations;
- raw archive path/bytes/SHA-256/readability or durable retrieval failure;
- fresh extraction path/result;
- target-attributable diagnostic lines;
- deepest boundary;
- C1–C9;
- diagnostic-only payload if any;
- restoration observations;
- final six-setting state;
- final logging state;
- final image/port/auth/registry state;
- exact mutation count;
- evidence-retention proof;
- zero source/Git/GitHub/image mutations.

## 36. Terminal markers

Required:

`RELEASE 1.12 WP04 — CORRECTED BOUNDED QUALIFICATION DIAGNOSTIC CAPTURE: <PASS|BLOCKED|FAIL>`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — DIAGNOSTIC PURPOSE: EXECUTION_DEPTH_ONLY`

`RELEASE 1.12 WP04 — FRESH DIAGNOSTIC RUN ID: <run-id|NONE>`

`RELEASE 1.12 WP04 — EVIDENCE ROOT: <path|NONE>`

`RELEASE 1.12 WP04 — LOCAL EVIDENCE DRY RUN: <PASS|FAIL>`

`RELEASE 1.12 WP04 — PREFLIGHT: <PASS|FAIL>`

`RELEASE 1.12 WP04 — LOGGING READINESS GATE: <PASS|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — DIAGNOSTIC CAPTURE START UTC: <timestamp|NONE>`

`RELEASE 1.12 WP04 — HELPER TRANSCRIPT RETAINED: <YES|NO>`

`RELEASE 1.12 WP04 — HELPER TRANSCRIPT RUNID ATTRIBUTION: <PASS|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — RAW AZURE LOG ARCHIVE: <RETAINED|RETRIEVAL_FAILED|NOT_REACHED>`

`RELEASE 1.12 WP04 — RAW ARCHIVE SHA256: <sha256|NONE>`

`RELEASE 1.12 WP04 — RAW ARCHIVE BYTES: <bytes|0>`

`RELEASE 1.12 WP04 — FRESH-WINDOW EXTRACTION: <PASS|EMPTY|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — EVIDENCE CHECKPOINT BEFORE RESTORATION: <PASS|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — DEEPEST QUALIFICATION EXECUTION BOUNDARY: <B0|B1|B2|B3|B4|B5|B6|B7|B8|B9|BX>`

`RELEASE 1.12 WP04 — QUALIFICATION DIAGNOSTIC RESULT: <C1|C2|C3|C4|C5|C6|C7|C8|C9>`

`RELEASE 1.12 WP04 — DIAGNOSTIC D3 PAYLOAD OBSERVED: <YES|NO>`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN_BY_THIS_AUTHORITY`

`RELEASE 1.12 WP04 — QUALIFICATION SETTINGS RESTORATION: <PASS|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — FINAL QUALIFICATION SETTINGS STATE: <ABSENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — LOGGING RESTORATION: <PASS|FAIL|NOT_REACHED>`

`RELEASE 1.12 WP04 — LOGGING FINAL STATE: <DISABLED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — DIAGNOSTIC RESTART COUNT: <0|1>`

`RELEASE 1.12 WP04 — AZURE MUTATION COUNT: <actual-count>`

`RELEASE 1.12 WP04 — QUALIFICATION ACCEPTANCE ATTEMPTS: 0`

`RELEASE 1.12 WP04 — REOPEN ATTEMPTS: 0`

`RELEASE 1.12 WP04 — TWELVE DATA SECRET CONFIGURATION: FORBIDDEN`

`RELEASE 1.12 WP04 — SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — LOCAL EVIDENCE RETAINED FOR LUNA: <YES|NO>`

`RELEASE 1.12 WP04 — FRESH DIAGNOSTIC RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — CORRECTED BOUNDED DIAGNOSTIC CAPTURE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-CORRECTED-DIAGNOSTIC RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA CORRECTED BOUNDED QUALIFICATION DIAGNOSTIC CAPTURE COMPLETE`

# GPT-5.6 Luna — Release 1.12 WP04 Multi-Layer Evidence-Preservation Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: define the corrected diagnostic evidence-preservation protocol after proven multi-layer procedure failure.
- **GPT-5.6 Terra** — execute only the later corrected diagnostic capture authorized from this protocol.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure state:

```text
six qualification settings = ABSENT
diagnostic logging = DISABLED
WEBSITES_PORT = 8501
governed image unchanged
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

No cleanup is currently required.

## 2. Proven evidence-preservation failure

Latest diagnostic RunId:

```text
initialize-f4da0cd4cf44419aa900f849c2358212
```

Result:

```text
C8 — EVIDENCE INSUFFICIENT
BX — EXECUTION DEPTH NOT PROVEN
```

Post-diagnostic reconciliation proved:

```text
HELPER TERMINAL CHANNEL = DEFECTIVE
AZURE CONTAINER LOG CHANNEL = NOT_PROVEN
LOG RETRIEVAL CHANNEL = DEFECTIVE
FRESH-WINDOW FILTER = NOT_PROVEN
LOGGING READINESS GATE = REQUIRED
EVIDENCE-PRESERVATION FAILURE CLASS = P6
PROCEDURE-ONLY CORRECTION SUFFICIENT = YES
TRACKED HELPER REMEDIATION REQUIRED = NO
AZURE LOG PROCEDURE REMEDIATION REQUIRED = YES
NEW IMAGE REQUIRED = NO
ORIGINAL TIMEOUT CAUSE = T7
POST-DIAGNOSTIC DECISION = D4
```

The failure is an evidence-preservation failure, not a proven application/runtime failure.

## 3. Remediation objective

Define a corrected procedure that makes a future single diagnostic run **reconcilable after restoration**.

The future run must leave durable, local, untracked evidence containing:

```text
exact fresh RunId
capture start UTC
helper stdout/stderr
helper terminal fields
poll observations
helper exit code
helper elapsed time
Azure logging configuration/readiness evidence
downloaded log archive
fresh-window extracted diagnostic records
restoration observations
final six-setting absence proof
final logging-disabled proof
```

The evidence must survive Azure restoration and remain available until Luna reconciliation is complete.

## 4. No tracked implementation change

Binding:

```text
tracked helper remediation = NO
repository source remediation = NO
image rebuild = NO
GHCR publication = NO
```

This is a procedure-only correction.

Do not modify:

```text
initialize-qualification.ps1
verify-persistent-sqlite-webapp.ps1
Worker source
entrypoint
Dockerfile
diagnostic instrumentation
```

## 5. Windows PowerShell target

All execution must be compatible with:

```text
Windows PowerShell 5.1.26100.9444
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

Do not use PowerShell 7-only syntax or newer unsupported .NET convenience APIs.

## 6. Local evidence directory

A future Terra authority must create a local **untracked** evidence directory before Azure mutation.

Preferred location:

```text
$env:TEMP\AIQuantTradingResearch\wp04\<fresh-run-id>\
```

or another explicitly non-repository temporary path.

It must contain, at minimum:

```text
run-metadata.txt
helper-transcript.txt
helper-stdout.txt or equivalent durable merged capture
helper-stderr.txt if separately available
poll-observations.txt
azure-logging-prestate.json
azure-logging-readiness.txt
azure-log-archive.zip
fresh-diagnostic-lines.txt
restoration-observations.txt
final-state.txt
```

Exact filenames may differ, but the evidence roles must all be durable.

Do not place secrets in any file.

## 7. Evidence retention rule

Critical invariant:

```text
DO NOT DELETE LOCAL EVIDENCE BEFORE LUNA RECONCILIATION
```

The archive and transcript must remain available after Azure state is restored.

A future Terra run must return the local evidence directory path.

Cleanup of local evidence is a later explicit action, not part of the diagnostic run.

## 8. Run metadata checkpoint

Before any Azure mutation, durably write:

```text
RunId=<fresh-run-id>
CaptureStartUtc=<UTC ISO-8601>
SourceCommit=2532f6abd4677edfb205c26c083a534783038979
ImageDigest=sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
Purpose=EXECUTION_DEPTH_ONLY
```

This file is the attribution root.

If it cannot be created and read back:

```text
STOP BEFORE AZURE MUTATION
```

## 9. Durable helper capture

The wrapper/helper invocation must be captured durably under Windows PowerShell 5.1.

The procedure must preserve:

```text
all safe stdout
all safe stderr
terminal result/class
terminal phase
terminal RunId
terminal exit code
terminal restoration state
terminal elapsed milliseconds
poll status/class observations
```

Use a Windows PowerShell 5.1-compatible capture method such as explicit redirection/teeing or transcript handling whose behavior is verified before Azure mutation.

Do not rely solely on transient console pipeline output.

Required attribution:

```text
helper evidence must contain exact fresh RunId
```

If it does not, helper evidence is not target-attributable.

## 10. Secret-safe capture

Never persist:

```text
PersistentSqliteQualification__HttpEvidenceToken value
TwelveData__ApiKey
registry password
Authorization header
cookies
connection strings
raw environment dump
raw app-settings values
```

If the helper emits a secret unexpectedly:

```text
capture = security failure
restore Azure state
do not publish artifact
report sanitized failure
```

## 11. Azure logging enablement

The corrected future procedure may temporarily enable only:

```text
filesystem container/application logging
```

Do not enable:

```text
HTTP/web-server logging
detailed errors
failed-request tracing
```

unless a later Luna authority explicitly changes this.

Record pre-state durably before mutation.

## 12. Logging readiness gate

Because the prior procedure did not establish log-channel readiness:

```text
LOGGING READINESS GATE = REQUIRED
```

The future procedure must not restart immediately after issuing the logging-enable mutation.

It must establish readiness using the narrowest supported read-only proof available.

Preferred proof order:

```text
1. Azure configuration read-back proves filesystem/container logging enabled
2. if platform exposes no stronger readiness signal, perform a bounded propagation wait after successful read-back
```

A bounded wait is permitted only after read-back confirms the requested logging configuration.

Default bounded propagation wait:

```text
15 seconds
```

Do not exceed:

```text
30 seconds
```

without new Luna authority.

This wait is procedural stabilization, not evidence that logs are already materialized.

## 13. Diagnostic capture start boundary

Use two timestamps:

```text
WP04_LOGGING_ENABLED_UTC
WP04_DIAGNOSTIC_CAPTURE_START_UTC
```

`WP04_DIAGNOSTIC_CAPTURE_START_UTC` must be recorded **after** logging readiness and immediately before qualification settings/restart.

Only records at or after this timestamp may receive fresh-run diagnostic credit.

## 14. Single future diagnostic run

The corrected procedure may later authorize exactly one new RunId and one restart.

This Luna remediation authority itself authorizes:

```text
diagnostic attempts = 0
Azure mutations = 0
```

A separate Terra authority is required for execution.

## 15. Log retrieval checkpoint

For the future run, Azure logging must remain enabled until all of these are complete:

```text
helper terminal state reached
log archive retrieval succeeds or reaches a bounded terminal failure
archive file exists locally
archive file size > 0
archive can be opened/listed
fresh-window extraction is attempted
extraction result is written durably
```

Only after this checkpoint may logging restoration begin.

If archive retrieval fails:

```text
DIAGNOSTIC RESULT = FAIL
```

but Azure restoration must still proceed.

## 16. Archive retention

The downloaded raw archive must be retained unchanged.

Record:

```text
archive local path
archive byte length
archive SHA-256
retrieval UTC
```

Use Windows PowerShell 5.1-compatible hashing, e.g. `Get-FileHash -Algorithm SHA256`.

Do not delete or overwrite the archive during extraction.

## 17. Fresh-window extraction

Extract/filter from the retained archive, not directly from an ephemeral stream.

The extraction procedure must preserve:

```text
source archive filename
source log filename
record timestamp
record text
```

Filter first by:

```text
timestamp >= WP04_DIAGNOSTIC_CAPTURE_START_UTC
```

Then identify:

```text
exact RunId
WP04_DIAG_*
qualification event vocabulary
container/platform startup records
```

Do not require every diagnostic line itself to contain RunId if the log stream/container instance can be unambiguously tied to the fresh attempt window; state the attribution basis.

## 18. Empty-log handling

If the archive is valid but contains no fresh-window records:

```text
FRESH LOG RESULT = EMPTY
```

This is durable evidence and is superior to losing the archive.

Do not delete it.

If fresh records exist but no `WP04_DIAG_*` records exist:

```text
APPLICATION DIAGNOSTIC EVENTS = NONE_OBSERVED
```

Again, retain the archive.

## 19. Restoration checkpoint

After evidence preservation checkpoint, restore:

```text
qualification settings
then diagnostic logging
```

Qualification restoration requires final exact:

```text
all six names = ABSENT
```

If initial read-back shows `PRESENT_NULL`, perform only bounded read-only rechecks:

```text
maximum 120 seconds
minimum interval 5 seconds
```

No second delete/write.

Logging restoration requires exact pre-state.

## 20. Restoration evidence must also be durable

Write post-restoration observations to local evidence before the procedure exits.

Include:

```text
timestamps
six-setting presence states
logging state
image identity
port
startup override
SCM/FTP state
registry credential presence state
```

No secret values.

## 21. Failure-safe ordering

The corrected procedure must use a failure-safe structure equivalent to:

```text
create evidence directory
write metadata
prove preflight
enable logging
prove logging config + readiness
record capture start
apply qualification settings
restart once
capture helper output durably
retrieve archive durably
extract fresh evidence durably
finally:
    restore qualification settings
    verify final ABSENT
    restore logging
    verify logging pre-state
    write final-state evidence
retain all local evidence
```

Restoration remains mandatory even when helper/log retrieval fails.

## 22. No acceptance credit

The future corrected run remains:

```text
EXECUTION_DEPTH_ONLY
```

Even if D3 payload is observed:

```text
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN_BY_DIAGNOSTIC_AUTHORITY
```

Luna must reconcile it afterward.

## 23. Evidence-channel acceptance gates

Before a future Terra diagnostic run can be considered procedurally valid, require:

```text
RUN METADATA DURABLE = PASS
HELPER OUTPUT DURABLE = PASS
LOGGING READINESS = PASS
RAW ARCHIVE DURABLE = PASS or TERMINAL_RETRIEVAL_FAILURE_DURABLY_RECORDED
FRESH EXTRACTION DURABLE = PASS
RESTORATION EVIDENCE DURABLE = PASS
LOCAL EVIDENCE RETAINED = PASS
```

If the helper fails before archive retrieval, archive retrieval must still be attempted if safe.

## 24. Procedure dry-run gate

Before any future Azure mutation, Terra must dry-run the local-only evidence machinery using synthetic non-secret text.

Prove:

```text
directory creation
metadata write/read
stdout capture
stderr capture
RunId preservation
SHA-256 hashing
archive/file retention
fresh-line output write
final-state output write
```

This dry-run must perform:

```text
Azure mutations = 0
```

If local evidence machinery fails, stop before Azure mutation.

## 25. New image decision

Binding:

```text
NEW IMAGE REQUIRED = NO
```

The deployed instrumentation capability remains accepted by source/image lineage.

## 26. Helper remediation decision

Binding:

```text
TRACKED HELPER REMEDIATION REQUIRED = NO
```

The correction belongs to the diagnostic execution procedure.

Do not commit a helper change merely to capture its output.

## 27. Azure log procedure decision

Binding:

```text
AZURE LOG CAPTURE PROCEDURE REMEDIATION REQUIRED = YES
```

The future procedure must retain the raw archive and establish the readiness/preservation checkpoints above.

## 28. Original timeout status

Preserve:

```text
ORIGINAL TIMEOUT CAUSE = T7
```

No runtime root cause is proven yet.

## 29. RunId preservation

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb849ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
initialize-fa16107e99d640d88bb9de91ef0da8ab
initialize-f4da0cd4cf44419aa900f849c2358212
```

No new RunId is generated under this Luna authority.

## 30. Mutation boundary

Under this Luna authority:

```text
Azure mutations = 0
Azure restarts = 0
logging mutations = 0
qualification attempts = 0
diagnostic attempts = 0
repository edits = 0
helper edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
GitHub/lifecycle mutations = 0
```

## 31. Decision

Selected remediation model:

```text
M1 — PROCEDURE-ONLY MULTI-LAYER EVIDENCE PRESERVATION
```

Selected decision:

```text
D1 — CORRECTED BOUNDED DIAGNOSTIC CAPTURE MAY BE PREPARED
```

This does not itself authorize execution.

## 32. Required Luna output

Confirm:

- P6 remains the governing failure class;
- procedure-only correction is sufficient;
- tracked helper remediation is not required;
- new image is not required;
- logging readiness gate is required;
- durable local helper transcript is mandatory;
- raw Azure log archive retention is mandatory;
- archive hash/size/path metadata is mandatory;
- restoration occurs only after evidence-preservation checkpoint;
- evidence survives reconciliation;
- diagnostic remains execution-depth-only;
- original timeout remains T7;
- acceptance/diagnostic retry remains unauthorized until separate Terra authority;
- zero-mutation audit.

## 33. Terminal markers

Required:

`RELEASE 1.12 WP04 — MULTI-LAYER EVIDENCE-PRESERVATION REMEDIATION: PASS`

`RELEASE 1.12 WP04 — GOVERNING FAILURE CLASS: P6`

`RELEASE 1.12 WP04 — REMEDIATION MODEL: M1_PROCEDURE_ONLY`

`RELEASE 1.12 WP04 — PROCEDURE-ONLY CORRECTION SUFFICIENT: YES`

`RELEASE 1.12 WP04 — TRACKED HELPER REMEDIATION REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE LOG PROCEDURE REMEDIATION REQUIRED: YES`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — LOGGING READINESS GATE: REQUIRED`

`RELEASE 1.12 WP04 — DURABLE LOCAL RUN METADATA: REQUIRED`

`RELEASE 1.12 WP04 — DURABLE HELPER TRANSCRIPT: REQUIRED`

`RELEASE 1.12 WP04 — RAW AZURE LOG ARCHIVE RETENTION: REQUIRED`

`RELEASE 1.12 WP04 — RAW ARCHIVE SHA256/SIZE/PATH: REQUIRED`

`RELEASE 1.12 WP04 — FRESH-WINDOW EXTRACTION FROM RETAINED ARCHIVE: REQUIRED`

`RELEASE 1.12 WP04 — EVIDENCE CHECKPOINT BEFORE LOGGING RESTORATION: REQUIRED`

`RELEASE 1.12 WP04 — LOCAL EVIDENCE RETENTION THROUGH LUNA RECONCILIATION: REQUIRED`

`RELEASE 1.12 WP04 — FUTURE DIAGNOSTIC PURPOSE: EXECUTION_DEPTH_ONLY`

`RELEASE 1.12 WP04 — ORIGINAL TIMEOUT CAUSE: T7`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — DIAGNOSTIC RETRY UNDER THIS AUTHORITY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — MULTI-LAYER EVIDENCE-PRESERVATION REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA CORRECTED BOUNDED DIAGNOSTIC CAPTURE AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA MULTI-LAYER EVIDENCE-PRESERVATION REMEDIATION COMPLETE`

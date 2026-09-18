# GPT-5.6 Terra — Release 1.12 WP04 Bounded Qualification Diagnostic Capture Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, diagnostic acceptance criteria.
- **GPT-5.6 Terra** — PRIMARY: execute exactly one fresh qualification-specific diagnostic capture within this authority.
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

Current governed deployed image:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current qualification settings:

```text
all six governed names = ABSENT
```

Historical failed RunId:

```text
initialize-fa16107e99d640d88bb9de91ef0da8ab
```

Historical result:

```text
Q5 — RESTORATION_FAILED
```

Historical Q5 remains valid.

Timeout reconciliation:

```text
deepest target-attributable boundary = BX
timeout cause = T7
new bounded diagnostic capture = REQUIRED
decision = D2
```

## 2. Purpose

This is a **diagnostic capture**, not an acceptance retry.

Execute exactly one fresh initialize-mode qualification startup with a new RunId solely to capture target-attributable execution-depth evidence.

The capture must distinguish among:

```text
container startup failure
qualification entry failure
SQLite qualification failure
artifact-write failure
listener startup failure
request-path failure
handler/evidence-response failure
helper transport/deadline failure
```

No D3 acceptance credit is granted by this authority even if a valid D3 payload happens to be observed.

Any observed payload is diagnostic evidence only and must be returned to Luna for reconciliation.

## 3. Explicit non-acceptance boundary

Binding:

```text
QUALIFICATION DIAGNOSTIC ATTEMPT != WP04 ACCEPTANCE RETRY
```

Therefore:

```text
initialize D3 acceptance = remains NOT_PROVEN
reopen = NOT_AUTHORIZED
redeploy continuity = NOT_AUTHORIZED
final WP04 acceptance = NOT_AUTHORIZED
```

Do not close #263 or advance WP05.

## 4. Windows PowerShell binding

All PowerShell execution must target:

```text
Windows PowerShell 5.1.26100.9444
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

No PowerShell 7-only syntax/APIs.

## 5. Fresh diagnostic RunId

Generate exactly one fresh RunId:

```text
initialize-<fresh-guid-N>
```

It must not equal any prior RunId.

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
initialize-fa16107e99d640d88bb9de91ef0da8ab
```

Never generate a second RunId under this authority.

## 6. Preflight — fail closed before mutation

Before any diagnostic/logging/qualification mutation, prove read-only:

```text
HEAD = 2532f6abd4677edfb205c26c083a534783038979
wrapper provenance gate = PASS
wrapper tracked at HEAD = PASS
wrapper worktree equals committed wrapper = PASS
HEAD descends from governed baseline 4822f9847a90a7d86c6bf771603defe9d7abf258
linuxFxVersion = exact governed digest
WEBSITES_PORT = 8501
startup override = none
registry username/password = absent
SCM basic auth = false
FTP basic auth = false
App Service = Running
diagnostic filesystem/container logging = disabled
```

Also prove all six qualification settings are `ABSENT`:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

If any name exists, including null/empty:

```text
PRECONDITION = FAIL
```

STOP with zero mutation.

## 7. Twelve Data boundary

Do not configure, alter, read, print, or synthesize:

```text
TwelveData__ApiKey
```

Qualification mode is governed as independent of the Twelve Data secret.

Normal public-root `503` remains:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

It is irrelevant to this diagnostic capture.

## 8. Diagnostic logging pre-state

Read and record current logging state before mutation.

Expected:

```text
filesystem/container application logging = disabled
web-server logging = disabled
detailed errors = disabled
failed-request tracing = disabled
```

If state differs, do not normalize it silently. STOP and return the mismatch for Luna unless the exact pre-state can be safely preserved under this authority.

## 9. Capture isolation boundary

Immediately before enabling diagnostics/qualification mutation, record:

```text
WP04_DIAGNOSTIC_CAPTURE_START_UTC=<UTC ISO-8601>
```

Only log records at or after this boundary are admissible.

Historical qualification records receive zero evidence credit.

## 10. Authorized diagnostic mutations

The only authorized mutation sequence is:

```text
1. enable App Service filesystem container/application logging
2. apply the minimum existing governed qualification settings for one initialize diagnostic run
3. perform exactly one App Service restart
4. perform bounded evidence-endpoint polling using the existing governed helper/request semantics
5. retrieve fresh-window container/application logs
6. restore qualification settings to exact pre-state
7. restore diagnostic logging to exact pre-state
```

Count actual Azure CLI/API mutations precisely.

No mutation may be repeated merely because the attempt fails.

## 11. Qualification settings for diagnostic run

Use only the existing governed setting names required by the runtime/helper:

```text
Worker__Mode = PersistentSqliteQualification
PersistentSqliteQualification__Phase = initialize
PersistentSqliteQualification__HttpEvidenceEnabled = true
PersistentSqliteQualification__RunId = <fresh-diagnostic-run-id>
PersistentSqliteQualification__HttpEvidenceToken = <fresh-high-entropy-token>
```

Use `PersistentSqliteQualification__EvidenceOutputPath` only if required by the already-governed implementation.

Do not add new configuration names.

Never print the token.

## 12. Diagnostic runtime path

Expected design path:

```text
entrypoint qualification branch
Twelve Data bypass
Streamlit suppressed
Worker qualification execution
SQLite qualification
atomic evidence artifact write
HTTP listener start on 0.0.0.0:8501
bounded evidence endpoint handling
Worker exit after retrieval or timeout
```

This expected path is not evidence until observed in fresh logs.

## 13. Existing diagnostic events

Capture target-attributable occurrences of:

```text
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
```

Return sanitized lines with timestamps and safe `WP04_DIAG_*` fields.

Do not return unrelated historical events.

## 14. Polling contract

Use the already-governed evidence endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<fresh-run-id>
```

with the temporary token header.

Preserve existing retry policy:

```text
404 = retryable
503 = retryable
generic 5xx = not generically retryable
transport NONE = only as already governed
Timeout = terminal under hard deadline
```

Do not widen timeout/retry semantics.

Record each safe poll observation:

```text
timestamp
status/class
elapsed time
```

Do not print token/header contents.

## 15. Bounded capture window

Maximum active diagnostic capture:

```text
15 minutes
```

The helper/request hard deadline remains the already-governed bounded value; do not extend it.

The broader 15-minute window exists only to retrieve fresh logs and restore state.

## 16. Log retrieval

After the single diagnostic attempt terminates or the helper reaches terminal state, download/retrieve the App Service filesystem container/application logs using the supported Azure surface.

Inspect only:

```text
timestamp >= WP04_DIAGNOSTIC_CAPTURE_START_UTC
```

Return only sanitized relevant lines.

Do not use Kudu `/home`.

Do not query SQLite directly.

Do not use Python to inspect the database.

## 17. Deepest execution boundary

Classify exactly one:

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

Use only fresh target-attributable evidence.

## 18. Diagnostic result classes

Return exactly one:

### C1 — CONTAINER_OR_PLATFORM_STARTUP_FAILURE_PROVEN

Fresh logs prove failure before qualification entry.

### C2 — QUALIFICATION_RUNTIME_FAILURE_BEFORE_LISTENER_PROVEN

Fresh logs prove qualification entry/SQLite/artifact path and a failure before successful listener startup.

### C3 — LISTENER_STARTUP_FAILURE_PROVEN

Fresh logs prove listener startup was attempted but did not reach `LISTENER_STARTED`.

### C4 — REQUEST_PATH_FAILURE_PROVEN

Require:

```text
LISTENER_STARTED = proven
REQUEST_ARRIVED = absent in fresh window
helper polling = proven
```

### C5 — HANDLER_OR_RESPONSE_FAILURE_PROVEN

Require request arrival/handler entry but no successful evidence retrieval.

### C6 — HELPER_TRANSPORT_OR_DEADLINE_FAILURE_PROVEN

Use only if application-side evidence proves the listener/handler path succeeded sufficiently but helper transport/deadline prevented retrieval.

### C7 — DIAGNOSTIC PATH SUCCEEDED

Require:

```text
EVIDENCE_RETRIEVAL_SUCCEEDED
```

and/or exact valid fresh RunId evidence observed.

This is **diagnostic success only**, not D3 acceptance credit.

### C8 — EVIDENCE INSUFFICIENT

Use when fresh target-attributable evidence remains insufficient.

## 19. Restoration order

After evidence/log retrieval, restore:

```text
qualification settings first
diagnostic logging second
```

Preserve exact pre-state.

No normal-mode restart is authorized after restoration unless already unavoidable in the existing helper and explicitly counted; do not add one for health recovery.

## 20. Qualification-setting restoration verification

Historical Q5 requires stronger verification.

For all six names originally absent, require final:

```text
ABSENT
```

If the first post-delete read is `PRESENT_NULL` or otherwise present:

- do **not** issue another delete/write;
- perform bounded read-only rechecks;
- maximum restoration verification window: **120 seconds**;
- poll no faster than every **5 seconds**.

If all six become absent within the window:

```text
QUALIFICATION SETTINGS RESTORATION = PASS
```

If not:

```text
QUALIFICATION SETTINGS RESTORATION = FAIL
```

This does not assert eventual consistency; it is only bounded verification.

## 21. Logging restoration verification

Restore exact logging pre-state and prove it read-only.

Expected final:

```text
filesystem/container logging = disabled
web-server logging = disabled
detailed errors = disabled
failed-request tracing = disabled
```

If logging restoration fails, prioritize restoration and stop.

## 22. Final configuration proof

After restoration prove:

```text
all six qualification settings = ABSENT
linuxFxVersion = exact governed digest
WEBSITES_PORT = 8501
startup override = none
registry credentials = absent
SCM basic auth = false
FTP basic auth = false
diagnostic logging = disabled
```

Do not require normal root health.

## 23. Secret hygiene

Never print:

```text
qualification token
TwelveData__ApiKey
registry password
connection strings
Authorization headers
cookies
raw environment
raw app-settings dump
```

Return only presence/state metadata.

## 24. Mutation accounting

Return actual count, separated by operation.

Expected logical mutation categories:

```text
logging enable
qualification settings application
restart
qualification settings restoration
logging restoration
```

If one logical action maps to multiple Azure API/CLI writes, count actual mutations, not just categories.

Required zero:

```text
Twelve Data secret mutation = 0
source mutation = 0
Git mutation = 0
GitHub mutation = 0
Docker build = 0
GHCR publication = 0
image mutation = 0
port mutation = 0
startup-command mutation = 0
SCM/FTP mutation = 0
registry-credential mutation = 0
reopen attempts = 0
acceptance retries = 0
```

## 25. No acceptance credit

Even if C7 occurs:

```text
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN_BY_THIS_AUTHORITY
```

Return the payload/evidence to Luna.

A later Luna reconciliation may decide whether the diagnostic evidence justifies a separately governed acceptance attempt or another action.

## 26. Failed RunId preservation

After this run, the new diagnostic RunId is also single-use and must never be reused, regardless of outcome.

Maintain the complete forbidden set.

## 27. Lifecycle boundary

Do not:

```text
open/merge PR
close #263
set Project #2 Done
close milestone
start WP05
```

WP04 remains incomplete.

## 28. Required return evidence

Return:

- fresh diagnostic RunId;
- capture start UTC;
- complete preflight markers;
- logging pre-state;
- exact sanitized mutation list/count;
- restart count;
- helper terminal fields;
- safe poll observations;
- sanitized fresh-window diagnostic lines;
- deepest B0–B9/BX boundary;
- exact C1–C8 result;
- any observed D3 payload clearly marked diagnostic-only;
- qualification-setting restoration timeline/read-backs;
- final six-setting absence proof;
- logging restoration proof;
- final image/port/auth/config proof;
- qualification acceptance attempts = 0;
- reopen attempts = 0;
- source/Git/GitHub mutations = 0.

## 29. Terminal markers

Required:

`RELEASE 1.12 WP04 — BOUNDED QUALIFICATION DIAGNOSTIC CAPTURE: <PASS|BLOCKED|FAIL>`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — DIAGNOSTIC PURPOSE: EXECUTION_DEPTH_ONLY`

`RELEASE 1.12 WP04 — FRESH DIAGNOSTIC RUN ID: <run-id|NONE>`

`RELEASE 1.12 WP04 — DIAGNOSTIC CAPTURE START UTC: <timestamp|NONE>`

`RELEASE 1.12 WP04 — PREFLIGHT: <PASS|FAIL>`

`RELEASE 1.12 WP04 — TWELVE DATA SECRET CONFIGURATION: FORBIDDEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — DEEPEST QUALIFICATION EXECUTION BOUNDARY: <B0|B1|B2|B3|B4|B5|B6|B7|B8|B9|BX>`

`RELEASE 1.12 WP04 — QUALIFICATION DIAGNOSTIC RESULT: <C1|C2|C3|C4|C5|C6|C7|C8>`

`RELEASE 1.12 WP04 — DIAGNOSTIC D3 PAYLOAD OBSERVED: <YES|NO>`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN_BY_THIS_AUTHORITY`

`RELEASE 1.12 WP04 — QUALIFICATION SETTINGS RESTORATION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — FINAL QUALIFICATION SETTINGS STATE: <ABSENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — LOGGING RESTORATION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — LOGGING FINAL STATE: <DISABLED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — DIAGNOSTIC RESTART COUNT: <0|1>`

`RELEASE 1.12 WP04 — AZURE MUTATION COUNT: <actual-count>`

`RELEASE 1.12 WP04 — QUALIFICATION ACCEPTANCE ATTEMPTS: 0`

`RELEASE 1.12 WP04 — REOPEN ATTEMPTS: 0`

`RELEASE 1.12 WP04 — SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — FAILED RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — BOUNDED QUALIFICATION DIAGNOSTIC CAPTURE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-DIAGNOSTIC QUALIFICATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA BOUNDED QUALIFICATION DIAGNOSTIC CAPTURE COMPLETE`

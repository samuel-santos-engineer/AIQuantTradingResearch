# GPT-5.6 Terra — Release 1.12 WP04 Instrumented Image Azure Initialize Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: deploy the exact published instrumented runtime image and execute exactly one fresh Azure initialize qualification attempt with bounded, sanitized evidence collection.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Published source commit:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Parent:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Published instrumented candidate:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Exact candidate digest:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure runtime before this authority:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

App Service:

```text
aiqr112wp035ec325382770.azurewebsites.net
Linux App Service F1 / Free
West Central US
WEBSITES_PORT=8501
SCM basic auth=false
FTP basic auth=false
registry credentials absent
```

## 2. Purpose

This authority has two narrow goals:

1. place the exact instrumented candidate digest on the existing App Service without changing architecture or registry security;
2. execute exactly one **fresh initialize** qualification attempt and capture the narrowest available diagnostic evidence needed to locate the request path.

This is **not** final WP04 acceptance.

This authority does not authorize reopen/restart-continuity qualification, redeploy-continuity qualification, PR creation, lifecycle completion, or WP05.

## 3. Binding qualification policy

Preserve exactly:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
client evidence-poll total wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

Do not broaden retry behavior.

Do not change request timeout behavior.

Do not reuse a failed RunId.

## 4. Instrumented diagnostic contract

The candidate includes the governed vocabulary:

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

Safe fields only:

```text
WP04_DIAG_EVENT
WP04_DIAG_RUN_ID
WP04_DIAG_PHASE
WP04_DIAG_ELAPSED_MS
WP04_DIAG_OUTCOME
```

and approved fixed listener metadata where emitted:

```text
WP04_DIAG_LISTENER_HOST
WP04_DIAG_LISTENER_PORT
WP04_DIAG_ROUTE
WP04_DIAG_HTTP_METHOD
```

Never expose:

```text
token
headers
query string
response body
raw evidence JSON
SQLite contents
credentials
environment dump
raw exception text
stack trace
```

## 5. Preflight — read-only

Before any Azure mutation, prove:

```text
local source HEAD = d0f72660610b9b479f437fb69182cb1cc1a0a31f
remote governed branch tip = same commit
candidate tag resolves to sha256:892d246e...
anonymous manifest read = PASS
App Service = Running/Normal
current deployed image = sha256:17da5a99...
SCM basic auth = false
FTP basic auth = false
registry credential settings = absent
temporary WP04 qualification settings = absent
WEBSITES_PORT = 8501
plan = F1 / Free
region = West Central US
```

If any security or identity assertion differs, STOP.

## 6. Exact-digest Azure deployment

Deploy only this exact immutable image reference:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Do not use a mutable tag for Azure runtime configuration.

Do not introduce registry username/password settings.

Do not enable SCM or FTP basic authentication.

Do not alter:

```text
WEBSITES_PORT
Always On
health-check path
App Service plan/SKU
region
storage setting
networking
startup command
logging configuration
```

After deployment, read back `linuxFxVersion` and prove the exact digest is configured.

If the platform requires an implicit restart as part of container configuration replacement, record it as platform-attributable. Do not add a second explicit restart merely for convenience.

## 7. Normal-mode readiness check after image deployment

Before qualification settings are applied, perform only read-only checks sufficient to establish that the app returned to the expected normal runtime state.

At minimum capture:

```text
App Service state
availability state if exposed
configured exact image digest
temporary qualification setting count = 0
```

A successful normal public response may be checked if already part of the established deployment procedure, but do not infer WP04 persistence acceptance from it.

If the new image cannot reach normal runtime without changing Azure configuration, STOP and restore the prior exact digest if safe under Section 14.

## 8. Fresh initialize RunId

Generate exactly one new RunId:

```text
initialize-<fresh-guid-or-equivalent>
```

Requirements:

```text
must not equal any historical failed RunId
must not be reused after this authority
must be recorded exactly in the final evidence
```

Known forbidden RunIds:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

The malformed historical identifier previously listed by Luna also remains non-reusable if encountered.

## 9. Qualification settings

Apply only the established temporary qualification settings required by the committed helper/runtime contract.

They may include only the already-governed activation inputs such as:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
fresh RunId
temporary high-entropy evidence token
configured durable evidence-output path if already required by committed contract
```

Do not add new settings.

Do not print the evidence token.

Do not persist the token outside the temporary Azure setting lifecycle.

## 10. Qualification restart budget

Authorize exactly one explicit App Service restart for the fresh initialize qualification after temporary settings are applied, if the established helper requires it.

Maximum under this authority:

```text
qualification explicit restarts = 1
```

No second qualification restart.

No identical retry.

No reopen attempt.

No redeploy-continuity attempt.

## 11. Initialize qualification execution

Run the committed Windows PowerShell 5.1-compatible helper from source commit:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Use:

```text
Windows PowerShell 5.1.26100.9444
```

Preserve:

```text
hard 180-second outer deadline
per-request timeout capped by remaining deadline
retry statuses exactly 404 and 503
transport retry set empty
Timeout terminal
sanitized client failure classification
```

Exactly one fresh initialize execution is authorized.

## 12. Evidence acceptance

If the helper returns HTTP 200, accept only an exact governed D3 JSON record attributable to the fresh RunId.

Required fields:

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

For initialize require at minimum:

```text
Phase = initialize
RunId = exact fresh RunId
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceCount = 1
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Do not infer success from App Service `Running`.

No target-attributable record means no initialize acceptance credit.

## 13. Diagnostic evidence collection

### Primary rule

Collect diagnostic evidence only through **already available read-only surfaces**.

Permitted examples include:

```text
existing App Service/container log stream if accessible without changing logging configuration
existing platform log files/endpoints if already populated
existing Azure activity/runtime metadata
client-side helper telemetry
```

Do not enable or reconfigure logging.

Do not enable detailed errors.

Do not enable failed-request tracing.

Do not enable filesystem/application logging.

Do not enable SCM basic auth.

Do not use Kudu VFS to retrieve `/home`.

Do not query SQLite directly.

### If diagnostic output is available

Filter strictly to:

```text
WP04_DIAG_*
```

for the exact fresh RunId.

Return only sanitized diagnostic lines/records.

Classify the deepest proven boundary:

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

### If diagnostic output is not retrievable

Record exactly:

```text
INSTRUMENTED_DIAGNOSTICS_PRESENT_IN_IMAGE = PROVEN
AZURE_DIAGNOSTIC_OUTPUT_RETRIEVAL = NOT_AVAILABLE_WITH_CURRENT_READ_ONLY_SURFACES
```

Do not mutate logging merely to obtain it.

If the initialize attempt fails and diagnostic output cannot be retrieved, stop after restoration and return for Luna reconciliation.

## 14. Failure restoration

Temporary qualification settings must always be restored to their pre-attempt state.

On any initialize failure:

1. remove/restore all temporary qualification settings;
2. return app to normal mode;
3. perform the minimum restart required by the established restoration helper if and only if necessary;
4. verify:
   ```text
   temporary qualification settings = 0
   App Service = Running/Normal
   SCM basic auth = false
   FTP basic auth = false
   registry credentials absent
   ```

### Image disposition after failed initialize

If the new instrumented image itself reaches normal runtime successfully and no defect in the image is proven, leave the exact instrumented digest deployed in normal mode.

If the instrumented image is proven unable to support normal runtime independent of the qualification attempt, restore the prior exact digest:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Do not restore merely because the qualification request times out.

Record exact image disposition.

## 15. Success boundary

If fresh initialize succeeds:

- restore temporary qualification settings;
- return app to normal mode;
- keep the instrumented exact digest deployed;
- do not execute reopen;
- do not execute restart continuity;
- do not execute redeploy continuity.

Return:

```text
NEXT = TERRA/LUNA CONTINUITY QUALIFICATION AUTHORITY
```

according to the terminal marker below.

## 16. Failure boundary

If fresh initialize fails:

- no second initialize;
- no retry beyond the helper's governed 404/503 policy;
- restore temporary settings;
- preserve normal runtime;
- capture deepest available diagnostic boundary;
- return:

```text
NEXT = LUNA INSTRUMENTED AZURE FAILURE RECONCILIATION
```

## 17. No source/Git/Docker/GHCR mutation

Required zero:

```text
repository edits = 0
staged paths = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
```

The published candidate is immutable.

## 18. No GitHub lifecycle mutation

Required zero:

```text
PR creation = 0
PR merge = 0
issue mutation = 0
Project #2 mutation = 0
milestone mutation = 0
tag/release mutation = 0
WP05 start = 0
```

Issue `#263` remains open.

Milestone `#63` remains open.

## 19. Mutation accounting

Maximum authorized Azure mutations:

```text
exact-digest image configuration update = 1
temporary qualification-settings application = 1
explicit qualification restart = 1
temporary qualification-settings restoration = 1
restoration restart = 0 or 1 only if established helper requires it
prior-image restoration = 0 or 1 only if Section 14 condition is met
```

Required zero:

```text
logging mutations = 0
SCM policy mutations = 0
FTP policy mutations = 0
registry credential mutations = 0
plan/SKU mutations = 0
region mutations = 0
health-check mutations = 0
Always On mutations = 0
source/Git mutations = 0
Docker/GHCR mutations = 0
GitHub lifecycle mutations = 0
```

Count actual operations precisely.

## 20. Required return evidence

Return:

- exact preflight image/state/security facts;
- exact image configuration mutation result;
- read-back `linuxFxVersion`;
- normal-mode readiness result;
- fresh initialize RunId;
- helper exit/result;
- elapsed wall-clock time;
- terminal client HTTP status/failure class;
- D3 record if returned;
- exact diagnostic `WP04_DIAG_*` evidence if retrievable;
- deepest proven lifecycle/request boundary;
- temporary-setting restoration result;
- final App Service state;
- final exact deployed image digest;
- SCM/FTP policy state;
- registry credential state;
- exact mutation audit;
- next-authority classification.

Never return the evidence token.

## 21. Terminal markers

Always require:

`RELEASE 1.12 WP04 — INSTRUMENTED IMAGE AZURE QUALIFICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED EXACT-DIGEST AZURE DEPLOYMENT: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED NORMAL-RUNTIME READINESS: <PASS|FAIL>`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 QUALIFICATION RUNTIME: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE QUALIFICATION MUTATION AUDIT: PASS`

### If initialize succeeds

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: PROVEN`

`RELEASE 1.12 WP04 — TERRA CONTINUITY QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA INSTRUMENTED IMAGE AZURE QUALIFICATION COMPLETE`

### If initialize fails

`RELEASE 1.12 WP04 — INSTRUMENTED AZURE INITIALIZE QUALIFICATION: FAIL`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — INSTRUMENTED DIAGNOSTIC DEEPEST BOUNDARY: <EVENT|NOT_AVAILABLE>`

`RELEASE 1.12 WP04 — LUNA INSTRUMENTED AZURE FAILURE RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA INSTRUMENTED IMAGE AZURE QUALIFICATION COMPLETE`

### If preflight/deployment itself blocks

`RELEASE 1.12 WP04 — INSTRUMENTED IMAGE AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Terra — Release 1.12 WP04 Azure Hard-Deadline Initialize Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform one fresh Azure initialize qualification attempt using the published hard-deadline helper.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source/tooling commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Parent:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Remote branch:

```text
origin/release/1.12-wp04-persistent-sqlite
```

must equal:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Runtime image remains unchanged:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Binding helper behavior:

```text
outer evidence-poll wall-clock budget <= 180 seconds
deadline source = monotonic Stopwatch
request timeout capped by remaining whole seconds
retry sleep capped by remaining milliseconds
retryable HTTP statuses = exactly {404,503}
retryable transport classes = {NONE}
Timeout = terminal
```

Binding operator shell:

```text
Windows PowerShell 5.1.26100.9444
```

## 2. Purpose and scope

Run exactly **one fresh initialize qualification attempt** against the currently deployed exact runtime digest using the published hard-deadline helper.

This authority is initialize-only.

It does **not** pre-authorize:

```text
restart/reopen continuity
same-digest redeploy continuity
additional initialize retries
retry-policy changes
helper/source changes
Docker/GHCR publication
PR creation
WP04 acceptance/lifecycle closure
WP05 start
```

Outcome handling:

```text
initialize succeeds -> capture accepted baseline evidence, restore normal runtime, STOP
initialize fails -> capture sanitized terminal classification, restore normal runtime, STOP for Luna reconciliation
```

## 3. Azure precheck

Before mutation, verify read-only:

```text
source/tooling branch tip =
  579bbbe3f24de13f87c9e94c9b480e029e70e709

deployed runtime digest =
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03

App Service state = Running/Normal
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
temporary qualification settings = absent
temporary evidence token = absent
```

Verify helper contract from committed source:

```text
Windows PowerShell AST errors = 0
Stopwatch deadline accounting present
180-second outer bound present
404 retry present
503 retry present
Timeout retry absent
generic transport retry absent
generic 5xx retry absent
RandomNumberGenerator.Fill absent
RandomNumberGenerator.Create present
```

If any precheck drifts, STOP.

Required marker:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE PRECHECK: PASS`

## 4. Governed helper

Use exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

from commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Do not substitute inline scripts or an older helper.

Do not use:

- PowerShell 7;
- direct SQLite shell/Python evidence;
- Kudu VFS evidence retrieval;
- raw exception output;
- untracked helper variants.

## 5. Fresh initialize identity

Generate a new high-entropy RunId:

```text
initialize-<fresh-guid-N>
```

It must not equal any failed historical RunId:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
```

Generate a fresh evidence token using the committed Windows PowerShell 5.1-compatible RNG path.

Never print the token.

## 6. Qualification settings

Apply only the governed temporary settings required for initialize qualification, including the existing canonical configuration:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<fresh-run-id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<fresh-secret>
```

Do not introduce registry credentials.

Do not change SCM/FTP auth policy.

Do not change runtime image.

## 7. Lifecycle action

Perform exactly one governed App Service:

```text
Restart
```

No additional blind restart is authorized.

No redeploy is authorized.

## 8. Evidence polling contract

The committed helper must govern polling.

Binding behavior:

```text
outer total wall-clock budget <= 180 seconds
404 -> retry
503 -> retry
all other HTTP statuses -> terminal
all transport failures -> terminal
Timeout -> terminal
retry delay target = 5 seconds, capped by remaining deadline
request timeout capped by remaining deadline
```

Do not override the helper to allow more time.

Do not restart polling outside the same helper invocation.

## 9. Initialize success contract

Success requires one returned canonical evidence record with exact attribution:

```text
RecordVersion = 1
Phase = initialize
RunId = exact fresh RunId
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceIdentity = non-empty
AcceptedEvidenceCount = 1
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Capture:

```text
BASELINE_ACCEPTED_EVIDENCE_IDENTITY=<exact value>
BASELINE_ACCEPTED_EVIDENCE_COUNT=1
```

No success credit may be inferred from App Service `Running`.

No success credit may be inferred from historical evidence.

After valid evidence retrieval, require normal helper restoration.

Then STOP.

Do not run restart/reopen continuity under this authority.

Required markers:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — INITIALIZE BASELINE EVIDENCE ATTRIBUTION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE RESTART-CONTINUITY QUALIFICATION AUTHORITY: READY`

## 10. Initialize failure contract

If initialize fails for any reason:

Capture only sanitized governed diagnostics:

```text
fresh RunId
terminal/last WP04_HTTP_EVIDENCE_POLL_ATTEMPT
WP04_HTTP_EVIDENCE_STATUS
WP04_HTTP_EVIDENCE_FAILURE_CLASS if emitted
helper termination mode
evidence record present/absent
elapsed qualification polling time if safely available
```

Do not emit:

```text
token
headers
request URL
response body
raw exception message
stack trace
credentials
```

Require restoration:

```text
temporary qualification settings count = 0
temporary token absent
App Service = Running/Normal
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
runtime digest unchanged
```

Then STOP.

Do not perform a second initialize.

Do not run restart continuity.

Do not redeploy.

The failed fresh RunId becomes forbidden for reuse.

Required markers:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE FAILURE CLASSIFICATION: CAPTURED`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE FAILURE: PASS`

`RELEASE 1.12 WP04 — LUNA INITIALIZE FAILURE RECONCILIATION AUTHORITY: READY`

## 11. Runtime/source preservation

No Git mutation:

```text
staging = 0
commits = 0
pushes = 0
```

No Docker/GHCR mutation:

```text
builds = 0
publications = 0
```

No runtime image change:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

## 12. Azure mutation envelope

Authorized maximum:

```text
temporary qualification settings application = 1
Restart lifecycle action = 1
temporary settings restoration = 1
```

Count actual operations precisely.

Forbidden:

```text
second initialize attempt
second restart
redeploy
runtime image update
registry credentials
SCM auth change
FTP auth change
logging policy change
plan/SKU mutation
region mutation
port mutation
```

## 13. GitHub/lifecycle prohibition

Required zero:

```text
PR creation = 0
PR merge = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
WP05 start = 0
```

## 14. Stop conditions

STOP immediately if:

- branch tip differs from governed commit;
- deployed runtime digest differs;
- SCM/FTP auth policy differs;
- registry credentials are present;
- temporary qualification state already exists unexpectedly;
- Windows PowerShell 5.1 cannot be used;
- helper no longer proves the 180-second hard bound;
- any transport retry is detected;
- token/secret output occurs;
- restoration fails;
- evidence RunId differs from fresh RunId;
- evidence record is malformed;
- qualification requires direct SQLite or Kudu evidence;
- a second Azure attempt appears necessary.

Do not widen scope.

## 15. Required mutation audit

Return exact counts:

```text
temporary settings applications
temporary settings restorations
Restart lifecycle actions
additional restart actions
redeploy actions
SCM policy mutations
FTP policy mutations
registry credential mutations
Git mutations
Docker/GHCR mutations
PR mutations
issue mutations
Project #2 mutations
milestone mutations
tag/release mutations
```

## 16. Required return evidence

Return:

- source/tooling commit;
- runtime image digest;
- Windows PowerShell version;
- precheck result;
- fresh RunId;
- sanitized poll telemetry;
- elapsed bounded polling duration if available;
- full accepted initialize evidence record if successful;
- baseline accepted-evidence identity/count if successful;
- exact failure status/class if failed;
- helper termination mode;
- final temporary-setting count;
- final Azure state;
- exact mutation audit.

Never return secret-bearing values.

## 17. Terminal markers

Always on valid precheck:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE PRECHECK: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HARD-DEADLINE QUALIFICATION RUNTIME: PASS`

On success:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — INITIALIZE BASELINE EVIDENCE ATTRIBUTION: PASS`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE RESTART-CONTINUITY QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE HARD-DEADLINE INITIALIZE QUALIFICATION COMPLETE`

On failure:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE FAILURE CLASSIFICATION: CAPTURED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE FAILURE: PASS`

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA INITIALIZE FAILURE RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE HARD-DEADLINE INITIALIZE QUALIFICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — AZURE HARD-DEADLINE INITIALIZE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

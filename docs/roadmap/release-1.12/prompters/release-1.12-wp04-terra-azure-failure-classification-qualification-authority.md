# GPT-5.6 Terra — Release 1.12 WP04 Azure Failure-Classification Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform one fresh Azure initialize attempt solely to classify the previously opaque terminal/transport failure using the published sanitized diagnostics helper.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Parent:

```text
23eaca6b6dd6b8593b5a70498423b2fb742a6b6c
```

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Published helper behavior:

```text
retryable HTTP statuses = exactly {404,503}
transport exceptions = terminal pending classification
sanitized diagnostics =
  WP04_HTTP_EVIDENCE_POLL_ATTEMPT
  WP04_HTTP_EVIDENCE_STATUS
  WP04_HTTP_EVIDENCE_FAILURE_CLASS where needed
Windows PowerShell 5.1 compatibility = preserved
```

Forbidden historical failed RunIds:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
```

## 2. Purpose and scope

This authority is **classification-only**.

Run exactly one fresh initialize qualification attempt to capture the sanitized status/failure class if the attempt fails.

This authority does **not** pre-authorize:

```text
restart/reopen continuity
same-digest redeploy continuity
retry-policy broadening
source remediation
new commit
new image
PR creation
WP04 final acceptance
issue/project/milestone closure
WP05 start
```

If initialize succeeds completely, record the accepted initialize evidence and STOP for next authority.

If initialize fails, record the exact sanitized failure classification and STOP for Luna reconciliation.

## 3. Binding shell/runtime

Use:

```text
Windows PowerShell 5.1.26100.9444
```

Do not use PowerShell 7.

Do not infer success from `$LASTEXITCODE` alone after a terminating PowerShell exception.

Success requires normal helper completion plus valid evidence record.

## 4. Azure precheck

Before mutation, verify read-only:

```text
source branch tip =
  31c7fed07d556fbba693c348a338d7d35d5202a4

SCM basic auth = false
FTP basic auth = false

deployed runtime image digest =
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03

registry credentials = absent
temporary qualification settings = absent
temporary evidence token = absent
normal runtime = restored
```

Verify committed helper:

```text
retryable statuses = exactly {404,503}
transport retry logic = absent
sanitized failure-class output = present
RandomNumberGenerator.Fill = absent
RandomNumberGenerator.Create = present
```

If any state has drifted, STOP.

Required marker:

`RELEASE 1.12 WP04 — AZURE FAILURE-CLASSIFICATION PRECHECK: PASS`

## 5. Governed helper

Use exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

from:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Do not substitute:

- inline polling logic;
- untracked scripts;
- PowerShell 7;
- direct SQLite shell/Python;
- Kudu VFS evidence retrieval;
- raw HTTP diagnostics that expose secrets.

## 6. One fresh initialize attempt

Generate a fresh RunId:

```text
initialize-<fresh-guid-N>
```

It must not match either historical failed RunId.

Phase:

```text
initialize
```

Use the helper's governed temporary settings flow, including:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<fresh-run-id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<fresh secret>
```

Lifecycle action:

```text
Restart
```

Polling:

```text
404 = retry
503 = retry
all other HTTP statuses = terminal
transport exception = terminal
poll interval = 5 seconds
maximum window = 180 seconds
```

Never print the token.

## 7. Required failure classification capture

If the initialize attempt fails, capture only:

```text
fresh RunId
last/terminal WP04_HTTP_EVIDENCE_POLL_ATTEMPT
terminal WP04_HTTP_EVIDENCE_STATUS
terminal WP04_HTTP_EVIDENCE_FAILURE_CLASS if emitted
whether helper terminated normally or by exception
whether any evidence record was returned
```

For example:

```text
STATUS=401
FAILURE_CLASS=<none>
```

or:

```text
STATUS=NONE
FAILURE_CLASS=ConnectFailure
```

or:

```text
STATUS=200
FAILURE_CLASS=MalformedPayload
```

Do not include:

```text
token
headers
request URL
response body
exception message
stack trace
credentials
```

## 8. Failure path

On any failed initialize:

1. require helper restoration/finally cleanup;
2. verify temporary qualification-setting count returns to `0`;
3. verify temporary token absent;
4. verify App Service returns to `Running/Normal`;
5. verify SCM/FTP basic auth remain `false`;
6. verify runtime digest unchanged;
7. STOP.

Do not run restart continuity.

Do not run redeploy continuity.

Do not create PR.

The failed fresh RunId becomes permanently forbidden for reuse.

Required markers:

`RELEASE 1.12 WP04 — AZURE FAILURE CLASSIFICATION CAPTURED: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER CLASSIFICATION ATTEMPT: PASS`

`RELEASE 1.12 WP04 — LUNA FAILURE-CLASS RECONCILIATION AUTHORITY: READY`

## 9. Success path

If the fresh initialize attempt succeeds, require exact record:

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
BASELINE_ACCEPTED_EVIDENCE_IDENTITY
BASELINE_ACCEPTED_EVIDENCE_COUNT = 1
```

Require post-helper restoration to normal runtime.

Then STOP.

Do **not** run restart/redeploy continuity under this classification-only authority.

Required markers:

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE CONTINUITY QUALIFICATION AUTHORITY: READY`

## 10. Source/image preservation

No repository/Git mutations:

```text
staging = 0
commits = 0
pushes = 0
```

No Docker/GHCR mutations:

```text
builds = 0
publications = 0
```

Runtime image must remain:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

## 11. Azure mutation envelope

Authorized only:

```text
one temporary qualification settings application
one Restart lifecycle action
one temporary settings restoration
```

Count actual operations precisely.

Not authorized:

```text
additional blind restart
redeploy
SCM policy change
FTP policy change
registry credential creation
logging configuration mutation
App Service plan mutation
region mutation
port mutation
runtime image mutation
```

## 12. GitHub/lifecycle prohibition

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

## 13. Stop conditions

STOP immediately if:

- source branch tip differs;
- runtime digest differs;
- SCM/FTP auth policy drifts;
- registry credentials appear;
- helper does not match published diagnostic contract;
- Windows PowerShell 5.1 execution cannot be used;
- token or secret is disclosed;
- restoration fails;
- terminal failure cannot be classified even with the new diagnostics;
- evidence record is malformed;
- exact RunId attribution fails;
- direct SQLite/Kudu evidence would be required.

If failure remains unclassified after this diagnostic remediation, return that fact exactly; do not improvise another Azure attempt.

## 14. Required mutation audit

Return exact counts for:

```text
temporary settings applications
temporary settings restorations
Restart lifecycle actions
redeploy actions
SCM policy mutations
FTP policy mutations
registry credential mutations
repository/Git mutations
Docker/GHCR mutations
PR mutations
issue mutations
Project #2 mutations
milestone mutations
```

## 15. Required return evidence

Return:

- precheck result;
- Windows PowerShell version;
- source commit;
- runtime digest;
- fresh RunId;
- sanitized polling telemetry;
- exact terminal status/failure class if failed;
- helper termination mode;
- evidence-record presence/absence;
- accepted evidence record if successful;
- final temporary-setting count;
- final Azure runtime state;
- exact mutation audit.

Never return secret-bearing values.

## 16. Terminal markers

Always on valid precheck:

`RELEASE 1.12 WP04 — AZURE FAILURE-CLASSIFICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 CLASSIFICATION RUNTIME: PASS`

If failed but classified:

`RELEASE 1.12 WP04 — AZURE FAILURE CLASSIFICATION CAPTURED: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER CLASSIFICATION ATTEMPT: PASS`

`RELEASE 1.12 WP04 — FAILURE CLASSIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FAILURE-CLASS RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE FAILURE-CLASSIFICATION QUALIFICATION COMPLETE`

If initialize succeeds:

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — FAILURE CLASSIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE CONTINUITY QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE FAILURE-CLASSIFICATION QUALIFICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — AZURE FAILURE-CLASSIFICATION QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

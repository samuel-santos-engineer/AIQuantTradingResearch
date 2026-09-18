# GPT-5.6 Terra — Release 1.12 WP04 Azure Qualification Resume Authority (Post-503 Remediation)

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: resume Azure WP04 qualification using the published Windows PowerShell 5.1-compatible helper remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
23eaca6b6dd6b8593b5a70498423b2fb742a6b6c
```

Parent:

```text
da5108a6ad73b6e1a23df2b9df3d170d1658886a
```

Runtime image remains unchanged:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Why no image rebuild is required:

```text
eng/ is excluded from Docker context
Dockerfile does not copy eng/
```

Published helper behavior:

```text
retryable HTTP statuses: exactly {404,503}
terminal status policy preserved
sanitized attempt/status output enabled
Windows PowerShell 5.1 compatibility preserved
RandomNumberGenerator.Create()/GetBytes() compatibility preserved
```

Failed prior initialize RunId:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
```

Disposition:

```text
FORBIDDEN TO REUSE
```

## 2. Binding shell/runtime

Execute helper/operator PowerShell under:

```text
Windows PowerShell 5.1.26100.9444
```

Do not substitute PowerShell 7.

Do not reinterpret `$LASTEXITCODE` as helper success when a terminating PowerShell exception occurs.

Qualification success requires:

- helper returns normally;
- required evidence is captured;
- exact record validation passes;
- required PASS markers are emitted.

## 3. Azure precheck

Before mutation, verify read-only:

```text
source branch tip =
  23eaca6b6dd6b8593b5a70498423b2fb742a6b6c

SCM basic auth = false
FTP basic auth = false

configured runtime image digest =
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03

registry credentials = absent
temporary D3 settings = absent
temporary evidence token = absent
```

Verify committed helper:

```text
retryable statuses = exactly {404,503}
RandomNumberGenerator.Fill = absent
RandomNumberGenerator.Create = present
```

If any state has drifted, STOP before mutation.

Required marker:

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME PRECHECK: PASS`

## 4. Use only the committed governed helper

Use exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

from source commit:

```text
23eaca6b6dd6b8593b5a70498423b2fb742a6b6c
```

Do not use:

- inline substitutes;
- untracked scripts;
- direct SQLite shell/Python inspection;
- Kudu VFS evidence retrieval;
- ad hoc HTTP loops replacing the helper.

## 5. Fresh initialize qualification

Generate a new RunId:

```text
initialize-<fresh-guid-N>
```

It must differ from all historical failed/accepted IDs.

Phase:

```text
initialize
```

Qualification settings:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<fresh initialize run id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<helper-generated secret>
```

Lifecycle action:

```text
Restart
```

Helper polling contract:

```text
retry 404
retry 503
poll every 5 seconds
maximum 180 seconds
all other governed terminal conditions remain terminal
```

The token must never be printed.

If initialize fails, STOP. Do not proceed to restart/redeploy continuity.

## 6. Initialize acceptance record

Require exact record semantics:

```text
RecordVersion = 1
Phase = initialize
RunId = exact fresh initialize run id
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

After helper cleanup/restoration require:

```text
temporary qualification settings = absent
temporary token = absent
normal Worker + Streamlit composition restored
qualification endpoint unavailable in normal mode
```

Required markers:

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

## 7. Restart/reopen continuity

Only after initialize passes:

Generate:

```text
reopen-restart-<fresh-guid-N>
```

Use a fresh helper-generated token.

Phase:

```text
reopen
```

Use ordinary App Service restart as the lifecycle boundary.

Require record:

```text
RecordVersion = 1
Phase = reopen
RunId = exact fresh restart/reopen run id
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
AcceptedEvidenceIdentity = BASELINE_ACCEPTED_EVIDENCE_IDENTITY
AcceptedEvidenceCount = BASELINE_ACCEPTED_EVIDENCE_COUNT
```

Require post-run cleanup/restoration.

Markers:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER RESTART: PASS`

If continuity differs, STOP.

## 8. Same-digest redeploy boundary proof

Before redeploy/reopen, prove a true Azure redeployment/container replacement boundary distinct from ordinary restart.

The operation must:

- preserve the exact immutable image digest;
- cause an actual deployment/container replacement boundary;
- preserve `/home`;
- preserve F1/Free;
- preserve West Central US;
- preserve public HTTPS/default DNS;
- preserve port/runtime configuration;
- preserve no-registry-credentials state;
- preserve SCM and FTP basic auth false.

Do not count a no-op setting rewrite as a redeploy unless Azure evidence proves actual replacement.

If the boundary cannot be deterministically proven, STOP for Luna reconciliation.

Required marker:

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOY BOUNDARY: PROVEN`

## 9. Same-digest redeploy/reopen continuity

After boundary proof:

Generate:

```text
reopen-redeploy-<fresh-guid-N>
```

Use a fresh helper-generated token.

Phase:

```text
reopen
```

Apply temporary qualification settings, perform the proven same-digest redeploy operation, retrieve evidence using the committed helper, then restore settings.

Require:

```text
RecordVersion = 1
Phase = reopen
RunId = exact fresh redeploy/reopen run id
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
AcceptedEvidenceIdentity = BASELINE_ACCEPTED_EVIDENCE_IDENTITY
AcceptedEvidenceCount = BASELINE_ACCEPTED_EVIDENCE_COUNT
```

Markers:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER REDEPLOY: PASS`

## 10. Final Azure state

Require:

```text
SCM basic auth = false
FTP basic auth = false
runtime image digest =
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
registry credentials = absent
temporary D3 settings = absent
temporary evidence token = absent
normal Worker + Streamlit composition = restored
qualification endpoint = unavailable in normal mode
database path = /home/data/aiquant.db
schema version = 4
journal mode = delete
```

Also confirm public Streamlit endpoint returns normal application behavior after final restoration.

Required marker:

`RELEASE 1.12 WP04 — FINAL AZURE STATE RESTORATION: PASS`

## 11. PR creation gate

Only after every Azure qualification gate above passes, create exactly one WP04 PR:

```text
base: main
head: release/1.12-wp04-persistent-sqlite
```

PR must reference:

- issue `#263`;
- source commit `23eaca6b6dd6b8593b5a70498423b2fb742a6b6c`;
- runtime image digest `sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03`;
- helper-only source/image provenance distinction;
- Windows PowerShell 5.1 compatibility;
- initialize evidence;
- restart/reopen continuity;
- same-digest redeploy/reopen continuity;
- schema v4;
- DELETE journal;
- exact evidence identity/count continuity;
- SCM basic auth false;
- FTP basic auth false;
- no registry credentials;
- no README change;
- no schema migration.

Do not merge.

Do not close #263.

Do not mutate Project #2 Status.

Do not close milestone #63.

Do not begin WP05.

Required marker if created:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

## 12. Authorized mutation envelope

Azure qualification may perform only the temporary settings and lifecycle operations required by the helper and the exact restart/redeploy proof.

Repository/Git mutations:

```text
0
```

Docker/GHCR mutations:

```text
0
```

GitHub mutation:

```text
at most 1 PR creation, and only after all qualification gates pass
```

Explicitly forbidden:

```text
new commit
push
force push
tag
release
image build
image publication
SCM policy change
FTP policy change
registry credential creation
PR merge
issue close
Project #2 mutation
milestone mutation
WP05 start
```

## 13. Mutation accounting

Return exact counts for:

```text
temporary settings applications
temporary settings restorations
ordinary restart operations
same-digest redeploy operations
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

Count only actual operations.

## 14. Stop conditions

STOP if:

- source branch tip differs;
- committed helper differs from approved policy;
- Windows PowerShell 5.1 execution fails;
- Azure auth policy drifts;
- runtime digest drifts;
- registry credentials appear;
- token leaks;
- initialize fails;
- exact RunId attribution fails;
- evidence record is malformed;
- restart/reopen continuity fails;
- same-digest redeploy boundary cannot be proven;
- redeploy changes image digest;
- `/home` persistence is not preserved;
- normal runtime restoration fails;
- temporary settings fail to restore;
- direct SQLite/Kudu evidence would be needed;
- PR creation would require any additional source mutation.

Do not widen scope.

## 15. Required return evidence

Return:

- precheck results;
- committed helper source proof;
- Windows PowerShell version used;
- fresh initialize RunId and sanitized record;
- baseline evidence identity/count;
- restart/reopen RunId and sanitized record;
- exact continuity comparison;
- exact same-digest redeploy mechanism;
- proof it is distinct from ordinary restart;
- redeploy/reopen RunId and sanitized record;
- exact continuity comparison;
- final Azure state;
- PR number if created;
- exact mutation audit.

Never return:

- evidence token;
- authorization header;
- secret app-setting values;
- publishing credentials.

## 16. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME PRECHECK: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 QUALIFICATION RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER RESTART: PASS`

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOY BOUNDARY: PROVEN`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER REDEPLOY: PASS`

`RELEASE 1.12 WP04 — FINAL AZURE STATE RESTORATION: PASS`

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME MUTATION AUDIT: PASS`

If PR created:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL ACCEPTANCE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE QUALIFICATION RESUME COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

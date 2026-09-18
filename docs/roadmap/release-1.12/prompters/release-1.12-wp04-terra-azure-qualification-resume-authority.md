# GPT-5.6 Terra — Release 1.12 WP04 Azure Qualification Resume Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: resume Azure WP04 qualification using the newly committed governed helper against the already-deployed exact runtime digest.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
da5108a6ad73b6e1a23df2b9df3d170d1658886a
```

Parent:

```text
9d08c6c09192b63b955f86feb3aba136657df3e6
```

Runtime image remains unchanged by governance decision:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Reason:

```text
eng/ excluded by .dockerignore
Dockerfile copies only src/, python/, and container/
```

Therefore the source/tooling commit advanced while the runtime image payload remains valid and unchanged.

Accepted source-publication markers:

`RELEASE 1.12 WP04 — RNG SOURCE PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION IMAGE PRESERVATION: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE QUALIFICATION RESUME AUTHORITY: READY`

## 2. Reusable Azure state already proven

The prior blocked Azure authority already proved:

```text
Plan: F1 / Free
Region: West Central US
HTTPS/public app: yes
SCM basic auth: false
FTP basic auth: false
Configured runtime image:
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
Registry username/password introduced: no
```

Actual prior mutations:

```text
SCM policy update to false: 1
Exact-digest image reference update: 1
D3 settings: 0
Qualification restart/redeploy: 0
PR/lifecycle mutations: 0
```

These successful pre-initialize state transitions do not need to be repeated unless read-only inspection shows drift.

## 3. Exact resume precheck

Before qualification mutation, perform read-only verification that:

- source branch tip is `da5108a6ad73b6e1a23df2b9df3d170d1658886a`;
- committed helper contains the compatible `RandomNumberGenerator.Create()` / `GetBytes()` implementation;
- no active `RandomNumberGenerator.Fill` remains in that helper;
- SCM basic auth is still `false`;
- FTP basic auth is still `false`;
- configured image is still the exact digest:
  `sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03`;
- no registry credentials exist;
- no stale temporary D3 settings or token remain.

If any drift is detected, STOP.

Required marker:

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME PRECHECK: PASS`

## 4. Use only the committed governed helper

Use:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

from source commit:

```text
da5108a6ad73b6e1a23df2b9df3d170d1658886a
```

Do not use:

- inline substitute scripts;
- untracked Docker helper;
- direct REST ad hoc replacement;
- direct SQLite queries;
- Kudu VFS evidence retrieval.

The helper must generate the temporary evidence token itself.

## 5. Initialize qualification

Generate a **fresh** initialize RunId.

Do not reuse historical run IDs.

Phase:

```text
initialize
```

Required qualification configuration:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<fresh initialize run id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<helper-generated secret>
```

The helper must:

- apply temporary settings;
- activate qualification mode through the governed lifecycle action;
- poll the public HTTP evidence endpoint;
- require exact RunId;
- restore temporary settings;
- never print the token.

## 6. Initialize acceptance record

Require:

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

After helper restoration require:

```text
normal Worker + Streamlit composition restored
qualification endpoint unavailable in normal mode
```

Required markers:

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

If initialize fails, STOP.

## 7. Restart/reopen continuity qualification

Only after initialize passes:

- generate a fresh reopen RunId;
- use a fresh helper-generated token;
- phase = `reopen`;
- apply temporary qualification settings;
- perform an ordinary App Service restart;
- retrieve through the governed HTTP endpoint;
- restore settings.

Require:

```text
RecordVersion = 1
Phase = reopen
RunId = exact fresh reopen run id
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
AcceptedEvidenceIdentity = BASELINE_ACCEPTED_EVIDENCE_IDENTITY
AcceptedEvidenceCount = BASELINE_ACCEPTED_EVIDENCE_COUNT
```

Required:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER RESTART: PASS`

If continuity differs, STOP.

## 8. Prove a true same-digest redeploy boundary

A redeploy must be distinct from ordinary restart.

Before executing it, identify and record the exact Azure operation that:

- preserves the exact same immutable image digest;
- causes a genuine deployment/container replacement boundary;
- does not introduce paid services;
- does not change port configuration;
- does not change registry credentials;
- preserves `/home`.

Do not treat "rewrite same setting" as sufficient unless Azure evidence proves an actual replacement/redeployment boundary.

If no deterministic same-digest redeploy mechanism can be proven, STOP for Luna reconciliation.

Required marker before continuity execution:

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOY BOUNDARY: PROVEN`

## 9. Same-digest redeploy/reopen continuity

After boundary proof:

- generate a fresh redeploy reopen RunId;
- generate a fresh token through the helper;
- phase = `reopen`;
- apply temporary qualification settings;
- perform the proven same-digest redeploy operation;
- retrieve evidence through public HTTP;
- restore settings.

Require:

```text
RecordVersion = 1
Phase = reopen
RunId = exact fresh redeploy reopen run id
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
AcceptedEvidenceIdentity = BASELINE_ACCEPTED_EVIDENCE_IDENTITY
AcceptedEvidenceCount = BASELINE_ACCEPTED_EVIDENCE_COUNT
```

Required markers:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER REDEPLOY: PASS`

## 10. Final Azure state

At completion require:

```text
SCM basic auth: false
FTP basic auth: false
Configured image:
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
Temporary D3 settings: absent/restored
Temporary evidence token: absent/restored
Registry credentials: absent
Normal composition: Worker + Streamlit
Streamlit externally reachable
Qualification endpoint unavailable
Database path: /home/data/aiquant.db
Schema: 4
Journal: delete
```

Required marker:

`RELEASE 1.12 WP04 — FINAL AZURE STATE RESTORATION: PASS`

## 11. PR creation gate

Only after all qualification markers above pass, create exactly one WP04 PR:

```text
base: main
head: release/1.12-wp04-persistent-sqlite
```

The PR must reference:

- issue `#263`;
- source commit `da5108a6ad73b6e1a23df2b9df3d170d1658886a`;
- runtime image digest `sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03`;
- source/image provenance explanation for the helper-only final commit;
- initialize/restart/redeploy qualification evidence;
- schema v4;
- DELETE journal;
- persistence continuity identity/count;
- SCM basic auth false;
- FTP false;
- no README/schema change.

Do not merge.

Do not close #263.

Do not change Project #2 status.

Do not close milestone #63.

Required marker if created:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

## 12. Exact mutation accounting

Authorized mutations if all stages complete:

```text
Initialize:
  temporary qualification settings apply
  governed lifecycle restart/recycle
  temporary settings restore

Restart/reopen:
  temporary qualification settings apply
  one ordinary App Service restart
  temporary settings restore

Same-digest redeploy/reopen:
  temporary qualification settings apply
  one proven same-digest redeploy
  temporary settings restore

GitHub:
  exactly 1 PR creation after all Azure gates pass
```

Expected zero:

```text
Repository mutations: 0
Git staging: 0
Git commits: 0
Git pushes: 0
Git tags: 0
Docker builds: 0
GHCR publications: 0
SCM policy mutations: 0
FTP policy mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
PR merges: 0
```

Count only actual mutations.

## 13. Stop conditions

STOP if:

- committed helper is not the remediated source;
- SCM/FTP drifted;
- deployed digest drifted;
- registry credentials appear;
- token generation fails under Windows PowerShell 5.1;
- token leaks;
- exact RunId attribution fails;
- initialize fails;
- restart/reopen continuity fails;
- same-digest redeploy boundary cannot be proven;
- redeploy changes image digest;
- normal runtime fails to restore;
- temporary settings fail to restore;
- direct SQLite/Kudu evidence would be required;
- PR creation would require additional repository mutation.

Do not widen scope.

## 14. Required return evidence

Return:

- resume precheck;
- helper source commit proof;
- initialize RunId and sanitized record;
- baseline evidence identity/count;
- restart/reopen RunId and sanitized record;
- continuity comparison;
- exact same-digest redeploy mechanism;
- proof it is distinct from ordinary restart;
- redeploy/reopen RunId and sanitized record;
- continuity comparison;
- final Azure state;
- PR number if created;
- exact mutation accounting.

Never return:

- evidence token;
- authorization header;
- publishing credential;
- secret app-setting value.

## 15. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — AZURE QUALIFICATION RESUME PRECHECK: PASS`

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

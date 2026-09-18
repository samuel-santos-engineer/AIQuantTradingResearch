# GPT-5.6 Terra — Release 1.12 WP04 Exact-Digest Azure HTTP Evidence Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: deploy the exact published candidate digest, restore SCM basic auth to the governed state, and execute Azure initialize/restart/redeploy persistence qualification through the application-owned HTTP evidence channel.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed candidate

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Exact candidate commit:

```text
9d08c6c09192b63b955f86feb3aba136657df3e6
```

Parent:

```text
2add79d2063292687d9813f9022206e84b664627
```

Exact published tag:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-9d08c6c09192b63b955f86feb3aba136657df3e6
```

Authoritative immutable digest:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Anonymous manifest read already passed.

Remote WP04 branch tip equals the exact candidate commit.

Accepted publication markers:

`RELEASE 1.12 WP04 — HTTP EVIDENCE PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE PUBLICATION VALIDATION GATES: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUSH: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE BUILD: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE ANONYMOUS READ: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE SOURCE-IMAGE PROVENANCE: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA EXACT-DIGEST AZURE QUALIFICATION AUTHORITY: READY`

## 2. Accepted runtime/evidence architecture

Evidence architecture:

```text
E1 — H1_APPLICATION_HTTP_EVIDENCE
```

Runtime composition:

```text
R1 — Q1_QUALIFICATION_SINGLE_LISTENER_SUBSTITUTION
```

Normal runtime:

```text
Worker starts
Streamlit starts
Streamlit owns 0.0.0.0:8501
qualification endpoint unavailable
```

Qualification HTTP mode requires both:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Then:

```text
Worker alone owns 0.0.0.0:8501
Streamlit is not started
Worker serves GET /internal/wp04/persistence-qualification
Worker exits after exact evidence retrieval or bounded timeout
```

Evidence request contract:

```text
GET /internal/wp04/persistence-qualification?runId=<exact-run-id>
X-WP04-Evidence-Token: <temporary high-entropy token>
```

Polling contract:

```text
interval: 5 seconds
maximum window: 180 seconds
```

Evidence source:

```text
H1-S2 — application reads its own atomic durable JSON artifact
```

## 3. Azure target contract

Preserve the already-governed target:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/default HTTPS/DNS
persistent /home
SQLite database: /home/data/aiquant.db
SQLite journal: DELETE
public/free GHCR
strict $0 reference/demo
```

No paid service may be introduced.

No Azure SQL.

No Azure Files.

No Container Apps.

No mandatory ACR.

No port-configuration change.

## 4. Exact precheck before mutation

Before any Azure mutation, verify and report:

- correct subscription;
- correct resource group;
- correct App Service/Web App;
- plan is F1 / Free;
- region is West Central US;
- app is currently reachable/running enough for management operations;
- current image reference;
- no registry username/password settings exist;
- FTP basic auth is `allow=false`;
- SCM basic auth is currently expected to be `allow=true` from the prior Kudu investigation;
- no stale qualification settings remain;
- no stale qualification token remains;
- no unrelated configuration drift is observed;
- current candidate image digest resolves exactly to:
  `sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03`.

Required marker:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE PRECHECK: PASS`

If any unrelated drift is detected, STOP.

## 5. Restore SCM basic auth to governed state

Because E1/R1 no longer uses Kudu, restore App Service SCM basic publishing credentials to:

```text
allow=false
```

FTP must remain:

```text
allow=false
```

After restoration verify:

```text
SCM basic auth: allow=false
FTP basic auth: allow=false
```

Do not alter unrelated publishing settings.

Required marker:

`RELEASE 1.12 WP04 — SCM BASIC AUTH RESTORATION: PASS`

## 6. Deploy the exact immutable digest

Set the Web App image reference to the exact immutable digest:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Do not deploy by mutable tag.

Do not alter registry credentials.

After deployment, verify the configured image reference still equals the exact digest.

App `Running` state alone is not qualification evidence.

Required marker:

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

## 7. Initialize qualification

Use the committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Use a **fresh** initialize run ID.

Do not reuse any historical run ID.

Required phase:

```text
initialize
```

Required temporary settings include:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<fresh initialize run id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<fresh secret token>
```

Use the helper's governed lifecycle path.

Because qualification mode substitutes the Worker as the sole listener on 8501, a restart/recycle is expected to activate the mode.

The helper must poll the public HTTP evidence endpoint and validate the exact run.

## 8. Initialize acceptance record

Require exactly:

```text
RecordVersion = 1
Phase = initialize
RunId = <exact fresh initialize run id>
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceIdentity = non-empty
AcceptedEvidenceCount = valid positive count
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Capture as baseline:

```text
BASELINE_ACCEPTED_EVIDENCE_IDENTITY
BASELINE_ACCEPTED_EVIDENCE_COUNT
```

Do not expose the capability token.

Temporary qualification settings must restore successfully.

After restoration, ordinary runtime composition must return:

```text
Worker + Streamlit
Streamlit owns 8501
qualification endpoint unavailable
```

Required markers:

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

If initialize does not pass, STOP. Do not proceed to reopen/redeploy.

## 9. Restart/reopen continuity qualification

Only after initialize passes, use a **fresh** reopen run ID.

Phase:

```text
reopen
```

Qualification settings:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=reopen
PersistentSqliteQualification__RunId=<fresh reopen run id>
Persistence__DatabasePath=/home/data/aiquant.db
PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__HttpEvidenceToken=<fresh secret token>
```

Perform an ordinary App Service restart as the lifecycle boundary.

Retrieve evidence through the public governed endpoint.

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

No identity drift.

No count drift unless the existing D3 contract explicitly and deterministically requires it; if any unexpected count change occurs, STOP for Luna reconciliation.

Restore temporary settings.

Verify ordinary Worker + Streamlit runtime returns.

Required markers:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER RESTART: PASS`

## 10. True same-digest redeploy boundary

A redeploy must be **distinct from a normal restart**.

Before mutation, determine and record the exact Azure-supported operation that will cause the Web App/custom-container instance to re-resolve/recreate deployment state while retaining the same exact immutable digest.

Do not assume that simply writing the same image setting produces a meaningful redeploy boundary.

Acceptable evidence must show a distinct deployment/container-replacement event.

If no deterministic same-digest redeploy mechanism can be proven, STOP and return for Luna reconciliation.

Do not proceed on assumption.

## 11. Same-digest redeploy/reopen qualification

Once a valid redeploy operation is proven:

1. prepare fresh reopen qualification settings with a new run ID and fresh token;
2. keep the exact same immutable digest;
3. perform the proven same-digest redeploy operation;
4. wait only through the governed HTTP readiness polling;
5. retrieve the exact fresh reopen evidence record;
6. validate continuity against initialize baseline.

Required record:

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

Restore temporary settings.

Verify ordinary Worker + Streamlit runtime returns.

Required markers:

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOY BOUNDARY: PROVEN`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER REDEPLOY: PASS`

## 12. Final Azure state verification

At the end, require:

```text
Configured image:
  exact digest sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03

SCM basic auth:
  allow=false

FTP basic auth:
  allow=false

Temporary D3 settings:
  absent/restored

Temporary evidence token:
  absent/restored

Normal composition:
  Worker + Streamlit

Streamlit:
  externally reachable on 8501

Qualification endpoint:
  unavailable in normal runtime

Registry username/password:
  absent

Persistent DB:
  /home/data/aiquant.db

Journal:
  delete

Schema:
  4
```

No secret values may be printed.

Required:

`RELEASE 1.12 WP04 — FINAL AZURE STATE RESTORATION: PASS`

## 13. PR creation gate

Only after all of the following pass:

- exact-digest deployment;
- SCM basic-auth restoration;
- initialize;
- restart/reopen;
- proven true same-digest redeploy;
- redeploy/reopen;
- final normal-runtime restoration;
- exact final Azure-state verification;

then create the WP04 PR from:

```text
release/1.12-wp04-persistent-sqlite
```

to:

```text
main
```

The PR must reference:

- issue `#263`;
- exact candidate commit;
- exact immutable image digest;
- initialize/restart/redeploy qualification evidence;
- preservation of the original WP04 acceptance boundary;
- no README/schema change;
- SCM basic-auth restored to false.

Do **not** merge the PR under this authority.

Do **not** close #263.

Do **not** mutate Project #2 status.

Do **not** close milestone #63.

Required marker if PR is created:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

## 14. No lifecycle completion under this authority

Even after a successful PR creation:

```text
#263 remains Open
Project #2 status remains Todo/In Progress as governed
milestone #63 remains Open
```

Final Luna acceptance must occur before merge/lifecycle completion.

WP05 must not begin yet.

## 15. Exact mutation accounting

Authorized Azure mutations, if all stages are reached:

```text
SCM basic-auth restoration:
  1 persistent policy update to allow=false

Exact-digest image deployment:
  1 image-reference mutation

Initialize:
  temporary D3 app-setting apply
  1 governed lifecycle restart/recycle
  temporary D3 app-setting restore

Restart/reopen:
  temporary D3 app-setting apply
  1 App Service restart
  temporary D3 app-setting restore

Same-digest redeploy/reopen:
  temporary D3 app-setting apply
  1 proven same-digest redeploy operation
  temporary D3 app-setting restore
```

Authorized GitHub mutation only after all Azure gates pass:

```text
PR creation: exactly 1
```

Expected zero:

```text
Repository mutations: 0
Git commits: 0
Git pushes: 0
Git tags: 0
GHCR publications: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
PR merges: 0
```

Count only mutations actually performed.

## 16. Stop conditions

STOP immediately if:

- deployed digest differs from the exact candidate;
- SCM basic auth cannot be restored to false;
- FTP changes unexpectedly;
- registry credentials appear;
- qualification endpoint cannot be reached in qualification mode;
- Streamlit remains bound to 8501 in qualification mode;
- token leaks;
- exact RunId attribution fails;
- initialize fails;
- restart/reopen identity/count continuity fails;
- normal runtime does not restore after qualification;
- same-digest redeploy boundary cannot be proven;
- redeploy changes the image digest;
- temporary settings fail to restore;
- final endpoint remains exposed in normal runtime;
- any direct SQLite inspection would be required;
- any repo/Git/GHCR mutation would be required;
- any paid Azure resource would be required.

Do not widen scope.

## 17. Required return evidence

Return:

- exact precheck state;
- SCM/FTP policy before and after restoration;
- exact deployed digest;
- initialize run ID and sanitized record;
- initialize baseline evidence identity/count;
- restart/reopen run ID and sanitized record;
- restart continuity comparison;
- exact same-digest redeploy mechanism used;
- proof it was distinct from ordinary restart;
- redeploy/reopen run ID and sanitized record;
- redeploy continuity comparison;
- final Azure state;
- normal-runtime restoration evidence after each qualification phase;
- PR number if created;
- exact mutation accounting.

Do not return any capability token, authorization header, publishing credential, or secret.

## 18. Terminal markers

Full Azure qualification success requires:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE PRECHECK: PASS`

`RELEASE 1.12 WP04 — SCM BASIC AUTH RESTORATION: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

`RELEASE 1.12 WP04 — AZURE HTTP INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER INITIALIZE: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER RESTART: PASS`

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOY BOUNDARY: PROVEN`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION AFTER REDEPLOY: PASS`

`RELEASE 1.12 WP04 — FINAL AZURE STATE RESTORATION: PASS`

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION MUTATION AUDIT: PASS`

If all gates pass and PR is created:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL ACCEPTANCE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA EXACT-DIGEST AZURE HTTP EVIDENCE QUALIFICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

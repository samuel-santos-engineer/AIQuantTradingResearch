# GPT-5.6 Terra — Release 1.12 WP04 Exact-Digest Azure Durable-D3 Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: deploy the exact immutable candidate digest to Azure and execute governed D3 qualification.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Authoritative candidate source commit:

`2add79d2063292687d9813f9022206e84b664627`

Authoritative candidate tag:

`wp04-2add79d2063292687d9813f9022206e84b664627`

Authoritative immutable digest:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

Image repository:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch`

Accepted publication evidence:

- exact commit push passed;
- exact candidate image build passed;
- one exact GHCR tag published;
- anonymous manifest read returned the same immutable digest;
- source-image provenance passed.

Accepted markers:

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE ANONYMOUS READ: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE SOURCE-IMAGE PROVENANCE: PASS`

## 2. Exact Azure deployment identity

The only image identity authorized for Azure under this authority is:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

Do not deploy by mutable tag for acceptance.

Do not substitute another digest.

## 3. Azure precheck

Before mutation, read-only verify:

- App Service Linux;
- SKU = F1 / Free;
- region = West Central US;
- Web App currently readable;
- persistent `/home` storage remains enabled;
- SQLite database path remains `/home/data/aiquant.db`;
- governed create-parent behavior remains enabled;
- no registry username/password is configured;
- no stale D3 qualification settings remain;
- no paid service is introduced;
- no unrelated app-setting drift is present.

Also capture the currently configured image reference for mutation accounting.

Required:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE PRECHECK: PASS`

## 4. Deploy exact immutable digest

Update only the Web App container image reference to:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

Do not change:

- SKU;
- region;
- persistent storage;
- SQLite path;
- registry credentials;
- unrelated application settings.

Wait for platform state sufficient to continue.

`Running` / `Normal` is necessary operational evidence but is not D3 acceptance.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

## 5. Governed D3 helper

Use the committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Governed durable artifact:

```text
/home/data/wp04-qualification/evidence.json
```

Governed retrieval surface:

```text
https://<app>.scm.azurewebsites.net/api/vfs/home/data/wp04-qualification/evidence.json
```

The helper provides:

- explicit phase;
- explicit run ID;
- explicit durable artifact path;
- `Restart` or `None` lifecycle mode;
- D3 setting snapshot/application;
- Kudu VFS retrieval;
- JSON validation;
- exact run-ID validation;
- sanitized output;
- best-effort restoration.

Credentials must remain local and secret.

Direct SQLite inspection remains forbidden.

## 6. Qualification A — initialize

Generate one unique initialize run ID.

Execute the helper using the exact implemented parameter names with:

```text
Phase = initialize
RunId = <unique initialize run id>
EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
LifecycleAction = Restart
```

Require the retrieved artifact to prove:

```text
RecordVersion = 1
Phase = initialize
RunId = exact initialize run ID
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceIdentity = non-empty
AcceptedEvidenceCount = valid
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Capture:

```text
WP04_BASELINE_EVIDENCE_IDENTITY=<value>
WP04_BASELINE_EVIDENCE_COUNT=<value>
```

The helper must restore temporary settings successfully.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

## 7. Qualification B — restart/reopen continuity

Generate a new unique reopen run ID.

Execute:

```text
Phase = reopen
RunId = <unique restart/reopen run id>
EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
LifecycleAction = Restart
```

Require:

- exact run-ID attribution;
- `Phase = reopen`;
- `SchemaVersion = 4`;
- `JournalMode = delete`;
- `IntegrityCheck = ok`;
- `QuickCheck = ok`;
- `PersistenceContinuity = true`;
- accepted evidence identity/count satisfy the application-defined continuity semantics relative to the initialize baseline.

Do not infer continuity from platform state.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

## 8. Qualification C — same-digest redeploy/reopen

This is a mandatory acceptance gate distinct from ordinary restart.

### 8.1 Read-only redeployment-mechanism proof

Before any redeployment mutation, identify an Azure-supported operation that proves a genuine App Service deployment/container replacement boundary while preserving the exact same immutable digest.

The mechanism must be materially distinct from an ordinary Web App restart.

Do not assume that re-setting the same image reference causes a new container instance.

Capture the evidence that makes the selected operation a deployment/replacement boundary.

Required before mutation:

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOYMENT MECHANISM: PROVEN`

If this cannot be proven, STOP for Luna reconciliation without performing a speculative redeploy.

### 8.2 Prepare exact reopen run

Generate a new unique redeploy/reopen run ID.

The D3 configuration active for the post-redeployment application instance must be exactly:

```text
Worker__Mode = PersistentSqliteQualification
PersistentSqliteQualification__Phase = reopen
PersistentSqliteQualification__RunId = <unique redeploy run id>
PersistentSqliteQualification__EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
```

The exact implemented setting names take precedence.

### 8.3 True same-digest redeployment

Perform the proven redeployment/container replacement action while keeping the exact configured digest:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

Prove:

- digest before = exact authorized digest;
- digest after = exact authorized digest;
- lifecycle event was distinct from ordinary restart;
- persistent `/home` was retained.

### 8.4 Post-redeploy artifact retrieval

Retrieve and validate the post-redeploy record through the committed helper/Kudu workflow.

Use `LifecycleAction=None` only if doing so does **not** overwrite the intended run identity or cause another lifecycle boundary.

Require:

```text
Phase = reopen
RunId = exact redeploy run ID
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Require accepted evidence identity/count continuity relative to the initialize baseline according to the application-owned semantics.

If the helper cannot safely retrieve the already-produced redeploy record without mutating/replacing the run configuration, STOP for Luna reconciliation.

Required:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

## 9. Cleanup and restoration

After successful qualification:

- restore/remove all temporary D3 settings;
- restore temporary diagnostic/logging settings if any were used;
- retain ordinary persistence settings;
- retain the exact final candidate digest;
- verify no registry credentials were introduced;
- verify Web App returns to ordinary runtime configuration/state.

If cleanup/restoration fails, qualification is not accepted.

Required:

`RELEASE 1.12 WP04 — FINAL AZURE QUALIFICATION CLEANUP: PASS`

## 10. Final Azure acceptance gate

All of the following must pass:

- exact immutable digest deployment;
- Kudu VFS durable artifact retrieval;
- exact run attribution;
- initialize qualification;
- restart/reopen continuity;
- true same-digest redeploy/reopen continuity;
- schema v4;
- DELETE journal;
- integrity check;
- quick check;
- evidence continuity semantics;
- persistent `/home`;
- no direct SQLite inspection;
- strict F1 / $0 architecture;
- cleanup/restoration.

Required:

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

## 11. PR creation boundary

Only after the final Azure acceptance gate passes, this authority permits creation of one WP04 pull request.

The PR must:

- target canonical `main`;
- use the current WP04 branch;
- identify source commit:
  `2add79d2063292687d9813f9022206e84b664627`;
- identify immutable image digest:
  `sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`;
- summarize local validation;
- summarize initialize/restart/redeploy evidence;
- reference issue `#263` appropriately.

Do not merge.

Do not close #263.

Do not set Project #2 to Done.

Do not close milestone #63.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

If any Azure acceptance gate remains blocked, PR creation is forbidden.

## 12. Exact mutation accounting

Report exact actual mutations.

Potential authorized mutations:

```text
Azure image-reference updates: exact count
Azure temporary D3 app-setting updates: exact count
Azure restart actions: exact count
Azure true same-digest redeployment actions: exact count
Azure restoration app-setting updates: exact count
GitHub PR creates: 0 or 1
```

Expected zero:

```text
Repository file mutations: 0
Git staging mutations: 0
Git commit mutations: 0
Git pushes: 0
Git tag mutations: 0
Docker image publications: 0
GHCR publications: 0
Registry credential mutations: 0
SKU/paid-service mutations: 0
Provider mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone closures: 0
```

Required:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION MUTATION AUDIT: PASS`

## 13. Stop conditions

STOP immediately if:

- exact digest differs from the authorized digest;
- F1 quota/suspension returns;
- persistence settings drift;
- registry credentials would be required;
- Kudu artifact retrieval fails;
- exact run attribution fails;
- helper restoration fails;
- restart/reopen continuity fails;
- a true same-digest redeployment boundary cannot be proven;
- `LifecycleAction=None` cannot safely validate the post-redeploy record;
- direct SQLite inspection appears necessary;
- paid infrastructure would be required.

Do not downgrade same-digest redeploy to restart.

Do not create a PR after a blocked Azure qualification.

## 14. Return evidence

Return:

- Azure image reference before/after;
- exact deployed digest;
- initialize sanitized durable record;
- restart/reopen sanitized durable record;
- same-digest redeployment mechanism and proof;
- redeploy/reopen sanitized durable record;
- baseline and continuity evidence identity/count values;
- cleanup/restoration evidence;
- PR number if created;
- exact mutation accounting.

Do not return credentials, tokens, authorization headers, or publishing passwords.

## 15. Terminal markers

### Full success

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE PRECHECK: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOYMENT MECHANISM: PROVEN`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — FINAL AZURE QUALIFICATION CLEANUP: PASS`

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — FINAL ACCEPTANCE/LIFECYCLE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA EXACT-DIGEST AZURE QUALIFICATION COMPLETE`

### Blocked

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: NO`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

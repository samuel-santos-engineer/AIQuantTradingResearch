# GPT-5.6 Terra — Release 1.12 WP04 Candidate Republication & Azure Durable-D3 Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: publish the one-path remediation candidate, deploy its exact immutable image, and execute governed Azure durable-D3 qualification.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Historical superseded candidate:

```text
source commit:
79c0d40f863a2ad92ae25ba20f927806c86a59be

historical image digest:
sha256:dccdaba04a39df969d7bd8b00c20cd8312928140995b0d528e9fa1fba85519ea
```

Accepted one-path remediation state:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Local validation already passed:

- PowerShell AST errors = 0;
- synthetic validation = 8 cases passed;
- Gitleaks = pass;
- direct SQLite command/API hits = 0;
- `git diff --check` = pass;
- staged paths = 0;
- working diff = exactly one authorized modified path.

## 2. Exact repository publication allowlist

Only this path may be staged and committed:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Before staging:

- confirm working diff is exactly that one modified tracked path;
- confirm no unrelated staged path exists;
- confirm `git diff --check` passes;
- confirm branch/upstream state is understood;
- confirm the historical candidate commit remains in ancestry.

Required:

`RELEASE 1.12 WP04 — REPUBLICATION ONE-PATH PRECHECK: PASS`

## 3. Corrected signing contract remains binding

Binding gate:

```text
Release build: required
Release warnings: 0
Release errors: 0
Release Authenticode signature: NOT required

Debug local signing contract: required
Expected signer: CN=AIQuantTradingDev
Mechanism: existing Debug-only AutoSignTestBinaries path
```

If Debug signing cannot be validated under the existing contract, STOP.

No manual Release signing.
No tracked signing-policy change.

Required:

`RELEASE 1.12 WP04 — CORRECTED SIGNING GATE: PASS`

## 4. Re-run publication validation before commit

Run:

- Release build;
- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted WP04/D3/SQLite tests;
- Debug signing contract validation;
- PowerShell AST parse;
- synthetic helper validation;
- Gitleaks for the exact one-path remediation;
- `git diff --check`;
- exact staged-path audit after selective staging.

Required:

`RELEASE 1.12 WP04 — REPUBLICATION VALIDATION GATES: PASS`

## 5. Selective stage, commit, and push

Stage exactly the one authorized helper path.

Create one narrow remediation commit and capture:

```text
parent SHA
new candidate commit SHA
commit message
exact path count/status
```

Push the current WP04 branch.

Required:

`RELEASE 1.12 WP04 — FINAL RETRIEVAL REMEDIATION COMMIT: PASS`

`RELEASE 1.12 WP04 — FINAL RETRIEVAL REMEDIATION PUSH: PASS`

## 6. Build and publish a new immutable candidate image

Build the existing container composition from the new candidate commit.

Publish to the existing public/free GHCR repository.

Requirements:

- candidate-specific tag tied to the new source commit;
- capture immutable digest;
- anonymous manifest read must pass;
- no registry username/password;
- no Dockerfile or entrypoint mutation;
- source-to-image provenance recorded.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE ANONYMOUS READ: PASS`

Record:

```text
WP04_FINAL_CANDIDATE_COMMIT=<sha>
WP04_FINAL_CANDIDATE_DIGEST=<sha256:...>
```

## 7. Azure precheck before exact-digest deployment

Read-only verify:

- App Service plan = F1 / Free;
- region = West Central US;
- Web App state/config readable;
- persistent storage enabled;
- SQLite path remains `/home/data/aiquant.db`;
- configured parent-creation behavior remains governed;
- no registry username/password;
- no stale D3 settings;
- no paid services;
- no unrelated app-setting drift.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE AZURE PRECHECK: PASS`

## 8. Deploy the new exact immutable digest

Update the Web App image reference to the new exact immutable digest.

Do not use a mutable tag as acceptance identity.

Do not change SKU, region, persistent storage, SQLite path, registry credentials, or unrelated app settings.

Wait for healthy platform state sufficient to continue.

`Running` / `Normal` alone is not D3 acceptance evidence.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

## 9. Governed Kudu artifact surface

Use the committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Governed artifact surface:

```text
https://<app>.scm.azurewebsites.net/api/vfs/home/data/wp04-qualification/evidence.json
```

Persistent artifact:

```text
/home/data/wp04-qualification/evidence.json
```

Credentials remain local and secret.

Direct SQLite inspection is forbidden.

## 10. Qualification A — initialize

Generate a unique initialize run ID.

Execute the helper with:

```text
Phase = initialize
RunId = <unique initialize run id>
EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
LifecycleAction = Restart
```

Use exact implemented parameter names.

Require:

```text
RecordVersion = 1
Phase = initialize
RunId = exact initialize run ID
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceIdentity = present/non-empty
AcceptedEvidenceCount = valid
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

Capture baseline evidence identity/count.

Temporary settings must restore successfully.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

## 11. Qualification B — restart/reopen continuity

Generate a new unique reopen run ID.

Execute the helper with:

```text
Phase = reopen
RunId = <unique restart/reopen run id>
EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
LifecycleAction = Restart
```

Require exact run-ID attribution plus schema 4, journal delete, integrity ok, quick-check ok, persistence continuity true, and application-defined evidence identity/count continuity.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

## 12. Qualification C — true same-digest redeploy/reopen continuity

This remains a distinct acceptance gate.

Required boundary:

- exact same immutable image digest;
- genuine App Service deployment/container replacement event;
- materially distinct from ordinary `Restart`;
- persistent `/home` retained;
- subsequent application-owned D3 `reopen` evidence proves continuity.

### 12.1 Read-only mechanism reconciliation

Before mutating Azure, identify the Azure-supported action that will constitute a **true same-digest redeployment/container replacement boundary**.

Do not assume that re-setting an identical image value necessarily causes replacement.

If the available Azure tooling cannot prove this property, STOP for Luna reconciliation.

Required:

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOYMENT MECHANISM: PROVEN`

### 12.2 Interaction with helper lifecycle modes

The committed helper supports `Restart` or `None`.

Do not silently use `Restart` for the redeploy gate.

Use only a sequence that proves:

1. exact D3 `reopen` phase/run ID/evidence path are active for the instance created by the redeployment boundary;
2. the true same-digest redeployment occurs;
3. the durable artifact is produced by the post-redeployment application instance;
4. `LifecycleAction=None` retrieves/validates that post-redeployment artifact without replacing the intended run identity or causing an unintended lifecycle boundary;
5. temporary D3 settings are restored afterward.

If the helper cannot support this safely, STOP.

Required:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

## 13. Final cleanup/restoration

After qualification:

- confirm temporary D3 settings are restored;
- restore temporary logging/configuration exactly;
- preserve ordinary persistence settings;
- preserve final exact candidate digest;
- verify no registry credentials were introduced;
- verify ordinary runtime state.

Required:

`RELEASE 1.12 WP04 — FINAL AZURE QUALIFICATION CLEANUP: PASS`

## 14. Final WP04 Azure acceptance gate

All must pass:

- Kudu VFS durable artifact retrieval;
- exact run attribution;
- initialize;
- restart/reopen;
- true same-digest redeploy/reopen;
- schema v4;
- DELETE journal;
- integrity/quick-check;
- evidence continuity semantics;
- persistent `/home`;
- no direct SQLite inspection;
- no paid infrastructure;
- immutable digest provenance;
- restoration/cleanup.

Required:

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

## 15. PR creation boundary

Only after all Azure gates pass:

- create the WP04 PR;
- target the canonical branch;
- include final candidate source SHA and image digest;
- summarize local validation and Azure initialize/restart/redeploy evidence;
- do not merge;
- do not close #263;
- do not mutate Project #2 Status;
- do not close milestone #63.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

If any Azure gate remains blocked, PR creation remains forbidden.

## 16. Mutation accounting

Report all actual mutations.

Potential authorized mutations:

```text
Git staged paths: exactly 1
Git commits: 1
Git pushes: 1
Docker image builds: exact count
GHCR image publications: exact count
Azure image-reference updates: exact count
Azure temporary D3 app-setting updates: exact count
Azure restart actions: exact count
Azure true same-digest redeployment actions: exact count
Azure restoration app-setting updates: exact count
GitHub PR creates: 0 or 1
```

Expected zero:

```text
Additional tracked repository paths modified: 0
Tracked signing-policy mutations: 0
README mutations: 0
Schema migrations: 0
Package/dependency mutations: 0
Dockerfile/entrypoint mutations: 0
Registry credential mutations: 0
SKU/paid-service mutations: 0
Provider mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone closures: 0
```

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE QUALIFICATION MUTATION AUDIT: PASS`

## 17. Stop conditions

STOP if:

- more than the one authorized remediation path would be committed;
- corrected Debug signing contract fails;
- publication validation regresses;
- Gitleaks fails;
- anonymous GHCR read fails;
- exact source/digest provenance is ambiguous;
- F1 quota/suspension returns;
- persistence configuration drifts;
- Kudu durable-artifact retrieval fails;
- run attribution is ambiguous;
- helper restoration fails;
- restart/reopen continuity fails;
- a true same-digest redeploy boundary cannot be proven;
- `LifecycleAction=None` cannot safely validate the post-redeploy artifact;
- paid infrastructure becomes necessary.

Do not self-authorize wider remediation.

## 18. Return evidence

Return:

- final remediation commit SHA and parent;
- new immutable GHCR digest;
- anonymous-read result;
- Azure exact-digest deployment evidence;
- initialize durable record;
- restart/reopen durable record;
- same-digest redeploy mechanism and proof;
- same-digest redeploy/reopen durable record;
- baseline and continuity identity/count values;
- cleanup/restoration evidence;
- PR number if created;
- exact mutation accounting.

Do not return secrets, Kudu credentials, publish-profile material, or tokens.

## 19. Terminal markers

### Full success

`RELEASE 1.12 WP04 — REPUBLICATION VALIDATION GATES: PASS`

`RELEASE 1.12 WP04 — FINAL RETRIEVAL REMEDIATION COMMIT: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

`RELEASE 1.12 WP04 — SAME-DIGEST REDEPLOYMENT MECHANISM: PROVEN`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE QUALIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — FINAL ACCEPTANCE/LIFECYCLE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA CANDIDATE REPUBLICATION/AZURE QUALIFICATION COMPLETE`

### Blocked

`RELEASE 1.12 WP04 — CANDIDATE REPUBLICATION/AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

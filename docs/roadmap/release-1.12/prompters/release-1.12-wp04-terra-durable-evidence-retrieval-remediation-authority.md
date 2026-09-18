# GPT-5.6 Terra — Release 1.12 WP04 Durable-Evidence Retrieval Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the Luna-approved one-path Azure/SCM durable-evidence retrieval remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Historical candidate source:

`79c0d40f863a2ad92ae25ba20f927806c86a59be`

Historical immutable image digest:

`sha256:dccdaba04a39df969d7bd8b00c20cd8312928140995b0d528e9fa1fba85519ea`

That candidate remains valid historical publication/deployment evidence but is no longer the final WP04 candidate.

Accepted Luna decision:

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL DECISION: R1`

`RELEASE 1.12 WP04 — SUPPORTED AZURE ARTIFACT SURFACE: DEFINED`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

## 2. Exact mutation allowlist

Only this tracked path may be modified:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other tracked or untracked repository path is authorized.

Explicitly forbidden:

- Worker source changes;
- test source changes;
- README;
- Dockerfile/entrypoint;
- schema;
- Domain/Application/Infrastructure changes;
- Python/Streamlit;
- package/project dependency files;
- WP03 scripts;
- Initiative-1.11 scripts;
- unrelated WP04 helpers.

## 3. Mission

Extend the existing verification helper into the complete deterministic Azure D3 qualification workflow governed by Luna.

The helper must support:

- temporary D3 setting application;
- explicit phase;
- explicit run ID;
- explicit evidence-output path;
- caller-controlled lifecycle action boundary;
- Kudu VFS/file retrieval of the exact application-owned durable artifact;
- local JSON validation;
- exact run-ID attribution;
- restoration of temporary settings;
- nonzero failure on invalid evidence.

This is deployment/validation tooling only.

It must not alter application runtime ownership or persistence semantics.

## 4. Supported artifact surface

The concrete governed retrieval surface is the App Service **Kudu VFS/file API** targeting:

```text
/home/data/wp04-qualification/evidence.json
```

The helper must use an Azure/App Service-supported SCM/Kudu file surface.

Authentication:

- may be acquired only through the authorized operator's local Azure context;
- must never be printed;
- must never be written into tracked files;
- must never be returned in execution evidence;
- must never be committed.

Do not use generic stdout/log scraping as evidence transport.

Do not use direct SQLite inspection.

Required:

`RELEASE 1.12 WP04 — KUDU DURABLE ARTIFACT RETRIEVAL IMPLEMENTATION: PASS`

## 5. Parameter/input contract

The helper must explicitly support, through parameters or established repository conventions:

- Azure subscription/context if already required by neighboring scripts;
- resource group;
- Web App name;
- qualification `Phase`;
- qualification `RunId`;
- qualification `EvidenceOutputPath`;
- caller-selected lifecycle action mode as needed by the script contract.

At minimum, the helper must be able to execute deterministic initialize/reopen verification using explicit caller-provided run IDs.

Do not silently generate a replacement run ID if the caller supplied one.

Default evidence path may be:

```text
/home/data/wp04-qualification/evidence.json
```

only if doing so matches the governed durable-artifact contract.

## 6. Temporary D3 configuration contract

The helper may set only the qualification settings required by the approved D3 design:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=<initialize|reopen>
PersistentSqliteQualification__RunId=<caller-provided run id>
PersistentSqliteQualification__EvidenceOutputPath=<persistent artifact path>
```

Use the exact implemented configuration names if repository code establishes slightly different casing/nesting.

The helper must preserve and later restore prior values where applicable.

It must not mutate:

- SQLite database path;
- parent-directory creation setting;
- persistent storage setting;
- registry credentials;
- SKU;
- region;
- unrelated application settings.

Required:

`RELEASE 1.12 WP04 — D3 TEMPORARY SETTING WORKFLOW: PASS`

## 7. Lifecycle-action separation

The helper must preserve caller control over the lifecycle action being qualified.

It must support a workflow where:

1. temporary D3 settings are applied;
2. the governed lifecycle action is performed;
3. the durable artifact is retrieved;
4. the artifact is validated;
5. temporary settings are restored.

For restart qualification, the lifecycle action is exactly one governed Web App restart.

For same-digest redeploy qualification, the future publication/qualification authority must choose an Azure-supported action that genuinely constitutes a deployment/container replacement boundary distinct from an ordinary restart.

Do not silently downgrade a redeploy qualification into a restart.

If the helper exposes a switch/parameter for lifecycle action, its behavior must be explicit and auditable.

## 8. Kudu retrieval behavior

Implement deterministic retrieval of the exact durable evidence file through Kudu VFS/file API semantics.

The helper must:

- construct the correct SCM/Kudu endpoint from the Web App identity;
- target the persistent `/home` artifact path;
- perform authenticated retrieval using local operator credentials/context;
- reject unsuccessful HTTP/API responses;
- reject empty content;
- parse the response as JSON;
- avoid writing authentication material to console;
- avoid leaking authorization headers or tokens in verbose/error output.

If a temporary local copy is used:

- use a local temp path;
- ensure it is not added/staged;
- clean it up after validation.

Required:

`RELEASE 1.12 WP04 — KUDU FILE RETRIEVAL CONTRACT: PASS`

## 9. Durable artifact validation contract

Validate all required fields from the retrieved JSON.

Required checks:

```text
RecordVersion == 1
Phase == <expected phase>
RunId == <exact expected run id>
DatabasePathIdentity == aiquant.db
SchemaVersion == 4
JournalMode == delete
AcceptedEvidenceIdentity is present/non-empty
AcceptedEvidenceCount is valid
IntegrityCheck == ok
QuickCheck == ok
PersistenceContinuity == true
```

The helper must fail nonzero on:

- missing artifact;
- stale run ID;
- mismatched phase;
- malformed JSON;
- missing required field;
- wrong schema;
- wrong journal;
- integrity failure;
- quick-check failure;
- false persistence continuity;
- unsupported retrieval response.

Required:

`RELEASE 1.12 WP04 — DURABLE ARTIFACT FIELD VALIDATION: PASS`

## 10. Continuity comparison support

The helper must expose enough validated output for future Terra qualification to compare:

- initialize evidence identity/count;
- restart/reopen evidence identity/count;
- same-digest redeploy/reopen evidence identity/count.

It may print sanitized validated field values.

It must not invent continuity semantics beyond what the application emits.

Do not query SQLite directly to establish continuity.

## 11. Restoration contract

Whether validation succeeds or fails, the helper must make a best-effort restoration of temporary D3 settings.

Restoration must be explicit and observable.

If restoration fails:

- return nonzero;
- report restoration failure;
- do not falsely report qualification success.

Do not delete or change the persistent SQLite database.

Do not delete the durable evidence artifact unless separately governed.

Required:

`RELEASE 1.12 WP04 — D3 SETTING RESTORATION CONTRACT: PASS`

## 12. Secret hygiene

The script must not print:

- Kudu/SCM passwords;
- publish-profile credentials;
- bearer/access tokens;
- authorization headers;
- private keys;
- connection secrets.

If commands used by the script can echo credentials, suppress/sanitize them.

Required:

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL SECRET HYGIENE: PASS`

## 13. Local validation

Perform only local/read-only validation sufficient to establish the script's correctness before any new publication.

At minimum:

- PowerShell AST parse = 0 errors;
- static inspection of parameter handling;
- static/controlled validation of Kudu endpoint/path construction;
- JSON validation logic exercised using synthetic local records for:
  - valid artifact;
  - stale run ID;
  - malformed JSON;
  - wrong schema;
  - wrong journal;
  - failed integrity;
  - failed quick-check;
  - false persistence continuity;
- failure paths return nonzero;
- secret-bearing values are not printed;
- no direct SQLite command appears in the helper;
- `git diff --check` passes;
- Gitleaks passes for the modified helper.

No Azure mutation is authorized by this authority.

Required:

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL POWERSHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL GITLEAKS: PASS`

## 14. Exact diff audit

Before completion, prove:

```text
git diff --name-status
```

shows exactly:

```text
M eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other tracked path may differ because of this remediation.

Unrelated untracked work remains untouched.

Required:

`RELEASE 1.12 WP04 — ONE-PATH RETRIEVAL REMEDIATION PAYLOAD: PASS`

## 15. Publication boundary

This authority permits implementation and local validation only.

It does **not** authorize:

- staging;
- commit;
- push;
- Docker image build/publication;
- GHCR mutation;
- Azure mutation;
- D3 app-setting mutation;
- restart;
- redeploy;
- PR creation;
- merge;
- issue closure;
- Project #2 mutation;
- milestone mutation.

After local validation passes, STOP and return evidence for a separate Terra candidate-publication/Azure-qualification authority.

## 16. Mutation accounting

Expected repository mutation:

```text
Tracked repository paths modified: exactly 1
```

Expected zero:

```text
Git staging mutations: 0
Git commits: 0
Git pushes: 0
Docker mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
Schema migrations: 0
Package/dependency mutations: 0
```

Required:

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL REMEDIATION MUTATION AUDIT: PASS`

## 17. Stop conditions

STOP if:

- a second repository path is required;
- direct SQLite inspection appears necessary;
- Kudu retrieval requires committing credentials;
- application code must change;
- schema must change;
- package/dependency change is required;
- Docker/entrypoint change is required;
- the helper cannot restore temporary D3 settings safely;
- deterministic exact-run attribution cannot be implemented;
- Gitleaks fails.

Do not self-authorize scope expansion.

## 18. Return evidence

Return:

- one-path diff summary;
- helper parameter contract;
- Kudu endpoint/path construction behavior;
- authentication mechanism class with secrets omitted;
- JSON validation behavior;
- restoration behavior;
- synthetic/local validation results;
- AST/Gitleaks/diff-check results;
- exact mutation accounting.

Do not return credentials or tokens.

## 19. Terminal markers

On full local success:

`RELEASE 1.12 WP04 — KUDU DURABLE ARTIFACT RETRIEVAL IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — D3 TEMPORARY SETTING WORKFLOW: PASS`

`RELEASE 1.12 WP04 — DURABLE ARTIFACT FIELD VALIDATION: PASS`

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — ONE-PATH RETRIEVAL REMEDIATION PAYLOAD: PASS`

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA CANDIDATE REPUBLICATION/AZURE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA DURABLE-EVIDENCE RETRIEVAL REMEDIATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — DURABLE-EVIDENCE RETRIEVAL REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Luna — Release 1.12 WP04 Azure Durable-Evidence Retrieval Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the missing Azure/SCM durable-artifact retrieval capability and define the minimum corrective path.
- **GPT-5.6 Terra** — may implement and validate only after Luna defines the exact remediation boundary.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Current authoritative candidate source:

`79c0d40f863a2ad92ae25ba20f927806c86a59be`

Current public GHCR image:

`wp04-79c0d40f863a2ad92ae25ba20f927806c86a59be`

Current immutable digest:

`sha256:dccdaba04a39df969d7bd8b00c20cd8312928140995b0d528e9fa1fba85519ea`

Accepted publication evidence:

- candidate commit created and pushed;
- anonymous GHCR manifest read passed;
- Azure Web App image reference updated to the exact immutable digest;
- Azure returned `Running` / `Normal`.

No D3 qualification settings have been applied to this candidate yet.

No restart/redeploy qualification attempt has occurred.

No PR has been created.

## 2. Proven blocker

The committed governed helper:

`eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1`

currently:

- prints run ID/path information;
- calls configuration read-back tooling;
- does **not** apply D3 settings;
- does **not** retrieve:
  `/home/data/wp04-qualification/evidence.json`;
- does **not** access any Azure/SCM-supported file surface;
- does **not** parse/validate the durable artifact;
- does **not** prove run-ID attribution.

The companion verification tooling explicitly validates configuration only.

Therefore the current committed candidate does not contain the governed Azure durable-evidence retrieval/validation path required by the E2 acceptance contract.

Continuing with ad-hoc Azure/SCM retrieval logic would violate governance.

## 3. Mission

Perform **zero-mutation Luna reconciliation** and determine the minimum valid remediation needed to complete WP04 Azure D3 qualification.

The reconciliation must preserve:

- the existing D3 application-owned durable artifact design;
- schema v4;
- SQLite DELETE journal mode;
- no direct SQLite inspection;
- persistent `/home`;
- App Service F1 / West Central US;
- strict $0;
- current candidate lineage and immutable-digest provenance;
- existing .NET ownership boundaries;
- no Streamlit/Python persistence ownership.

## 4. Evaluate exactly these remediation classes

### R1 — Extend the existing tracked verification helper

Select if the missing behavior can be implemented entirely inside:

`eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1`

The helper may then:

- apply temporary D3 settings;
- generate/pass explicit run IDs;
- invoke controlled restart/redeploy phases as separately authorized;
- retrieve the durable artifact from an approved Azure/SCM surface;
- parse JSON;
- validate run-ID attribution and expected D3 fields;
- restore temporary settings.

Use only if no additional repository path is required.

### R2 — Add one narrowly scoped Azure/SCM artifact-retrieval helper

Select if Azure/SCM file retrieval is sufficiently distinct that a separate script is cleaner and auditable.

If selected, define:

- exact new path;
- exact purpose;
- exact invocation relationship with `verify-persistent-sqlite-webapp.ps1`;
- why one additional helper is required;
- why this remains deployment/validation tooling only.

### R3 — Use an already tracked existing Azure/SCM helper

Select only if an existing tracked script already provides a supported `/home` file retrieval surface that can be reused without direct SQL or unrelated behavior.

Identify the exact path and command.

### R4 — No valid supported retrieval path

Select if App Service F1 / SCM cannot provide a reliable supported file retrieval surface compatible with the WP04 contract.

If selected, stop and define what acceptance-contract change or platform change would be required.

Required:

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL DECISION: <R1|R2|R3|R4>`

## 5. Supported Azure/SCM retrieval contract

If R1, R2, or R3 is selected, Luna must define the exact supported retrieval surface.

The solution must:

- read the application-owned artifact from persistent `/home`;
- use an Azure-supported App Service / Kudu / SCM / file API or command;
- avoid direct SQLite access;
- avoid shelling into the database;
- avoid changing app ownership boundaries;
- avoid paid services;
- be deterministic enough to retrieve the exact file produced by the governed run;
- permit exact run-ID attribution.

The authority must identify the concrete mechanism, not merely state "use SCM".

Examples of acceptable mechanism classes include an established Kudu VFS/file API, Azure CLI command that exposes App Service filesystem content, or another documented App Service file surface already compatible with the repository's tooling.

Do not authorize generic log scraping as a substitute.

Required:

`RELEASE 1.12 WP04 — SUPPORTED AZURE ARTIFACT SURFACE: DEFINED`

## 6. D3 setting/application responsibility

Reconcile whether the verification helper itself should apply temporary D3 settings or whether setting mutation should remain in the future Terra execution authority.

Preferred separation:

- helper = deterministic configure/read/validate/restore workflow if this matches repository deployment-tooling style;
- Terra authority = controls when each governed initialize/reopen/redeploy phase is invoked.

Do not duplicate configuration logic across multiple scripts unless necessary.

Required:

`RELEASE 1.12 WP04 — D3 QUALIFICATION TOOLING RESPONSIBILITY: DEFINED`

## 7. Minimum validation contract for the remediation

Any implementation selected under R1/R2/R3 must prove locally, where feasible:

1. PowerShell AST parse success;
2. exact parameter handling for:
   - run ID;
   - phase;
   - evidence output path;
3. deterministic artifact retrieval function/command construction;
4. JSON parse success/failure handling;
5. exact run-ID equality check;
6. validation of:
   - `RecordVersion`
   - `Phase`
   - `DatabasePathIdentity`
   - `SchemaVersion`
   - `JournalMode`
   - `AcceptedEvidenceIdentity`
   - `AcceptedEvidenceCount`
   - `IntegrityCheck`
   - `QuickCheck`
   - `PersistenceContinuity`;
7. explicit nonzero failure for:
   - missing artifact;
   - stale run ID;
   - malformed JSON;
   - unsupported retrieval result;
8. cleanup/restoration behavior;
9. no secrets printed;
10. no direct SQLite use.

Required:

`RELEASE 1.12 WP04 — DURABLE RETRIEVAL VALIDATION CONTRACT: DEFINED`

## 8. Candidate/publication impact

If R1 or R2 requires repository changes:

- current commit `79c0d40f...` remains a valid historical candidate but is **not final WP04 candidate**;
- current digest `sha256:dccdaba...` remains historical deployment evidence;
- a new candidate commit and new immutable image digest will be required;
- Azure must later be updated to the new exact digest before D3 qualification;
- no current Azure D3 acceptance credit exists.

If R3 requires no repository change:

- existing candidate may remain authoritative;
- Terra may proceed with governed Azure qualification using the existing helper.

Required:

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

## 9. Repository-change determination

Return exactly one:

- `REPOSITORY CHANGE REQUIRED: YES`
- `REPOSITORY CHANGE REQUIRED: NO`
- `REPOSITORY CHANGE REQUIRED: UNDETERMINED`

If YES, define the exact path allowlist for Terra.

Do not widen into:

- README;
- Dockerfile;
- entrypoint;
- schema;
- Domain;
- unrelated Application;
- Python;
- Streamlit;
- WP03;
- Initiative-1.11 scripts;
- package/dependency files.

## 10. Mutation audit

This authority permits zero mutations:

```text
Repository mutations: 0
Git mutations: 0
Docker mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
```

Required:

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL RECONCILIATION MUTATION AUDIT: PASS`

## 11. Return evidence

Return:

- inspection of the current verification helper;
- inspection of nearby deployment tooling;
- exact supported Azure/SCM retrieval surface;
- R1/R2/R3/R4 decision;
- exact path allowlist if repository change is required;
- exact validation contract;
- current candidate finality;
- next Terra authority readiness.

Do not return secrets.

## 12. Terminal markers

### If R1 or R2 is selected

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL DECISION: <R1|R2>`

`RELEASE 1.12 WP04 — SUPPORTED AZURE ARTIFACT SURFACE: DEFINED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION`

`RELEASE 1.12 WP04 — TERRA DURABLE-EVIDENCE RETRIEVAL REMEDIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA DURABLE-EVIDENCE RETRIEVAL RECONCILIATION COMPLETE`

### If R3 is selected

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL DECISION: R3`

`RELEASE 1.12 WP04 — SUPPORTED AZURE ARTIFACT SURFACE: DEFINED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED`

`RELEASE 1.12 WP04 — TERRA AZURE D3 QUALIFICATION RESUME AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA DURABLE-EVIDENCE RETRIEVAL RECONCILIATION COMPLETE`

### If R4 is selected

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — DURABLE EVIDENCE RETRIEVAL DECISION: R4`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

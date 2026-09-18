# GPT-5.6 Luna — Release 1.12 WP04 Kudu Durable-Evidence Retrieval Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: contract, evidence reconciliation, supported-surface determination, and next-step governance. Read-only/planning only.
- **GPT-5.6 Terra** — implementation, Azure/Git/GitHub mutations, validation execution, publication, and lifecycle after Luna authorizes them.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Authoritative source commit:

`2add79d2063292687d9813f9022206e84b664627`

Authoritative immutable image digest:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

Exact-digest deployment succeeded.

Observed Azure state:

```text
App state: Running / Normal
Initialize RunId: wp04-initialize-ba06eadf02a14b40bbe1f9789205cc73
Temporary D3 settings applied: True
LifecycleAction: Restart
Temporary D3 settings restored: True
Helper exit code: 1
Failure: Kudu evidence artifact retrieval failed.
```

Accepted markers:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE PRECHECK: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE EXACT-DIGEST DEPLOYMENT: PASS`

Blocked markers:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: NO`

No restart/reopen qualification, same-digest redeploy qualification, PR, issue, Project, or milestone mutation occurred after the blocked initialize attempt.

## 2. Purpose

Determine, without mutation, which boundary failed:

1. **Artifact production failure** — the application did not create the governed durable JSON artifact.
2. **Artifact retrieval transport failure** — artifact may exist, but the selected Kudu VFS/auth/path mechanism cannot retrieve it.
3. **SCM/Kudu availability or endpoint incompatibility** — the App Service Linux/F1 environment does not expose the assumed surface as governed.
4. **Authentication contract failure** — the selected local publishing-credential method cannot authorize the Kudu request.
5. **Path mapping failure** — `/home/data/...` does not map to the assumed `/api/vfs/home/data/...` SCM path.
6. **Timing/readiness failure** — retrieval occurred before the qualification process had deterministically completed.
7. **Helper contract defect** — status/response handling collapses distinguishable failures into generic `Kudu evidence artifact retrieval failed`.

Do not infer which case occurred from the generic error alone.

## 3. Read-only evidence collection

Inspect only existing repository code, scripts, Azure configuration, and non-mutating Azure/SCM metadata.

Required repository review:

- committed `verify-persistent-sqlite-webapp.ps1`;
- `PersistentSqliteQualificationExecution.cs`;
- Worker mode/configuration binding;
- durable-file write implementation;
- entrypoint/process behavior;
- existing WP04 Azure scripts;
- any repository documentation establishing App Service SCM/Kudu behavior.

Required Azure/SCM read-only review, where supported:

- Web App SCM/Kudu endpoint availability;
- whether SCM basic-auth/publishing credentials are enabled/usable;
- exact HTTP status/response metadata from the failed retrieval path if already locally available;
- App Service platform configuration relevant to SCM access;
- whether the selected API endpoint is supported for Linux App Service F1;
- whether `/home` persistent content is exposed through that SCM surface;
- whether the VFS API path convention for `/home/...` is correct;
- whether an alternative **supported, non-shell, non-SQLite** Azure file retrieval surface exists.

Do not mutate app settings, restart, redeploy, enable logs, or run a new D3 attempt.

## 4. Evidence-channel invariants

The following remain binding unless explicitly re-governed:

- application-owned D3 execution remains required;
- schema v4 remains required;
- SQLite DELETE journal remains required;
- direct SQLite shell/API/Python inspection is forbidden;
- Streamlit/Python must not own persistence;
- temporary qualification settings must be restored;
- secrets/credentials must never appear in evidence;
- `Running` / `Normal` does not prove persistence qualification;
- historical stdout records cannot prove this initialize attempt;
- no blind restart/logging retry is authorized;
- no PR may be created while Azure qualification is blocked.

## 5. Required technical reconciliation

Luna must answer each question explicitly.

### A. Did application execution plausibly reach D3 qualification?

Use repository/runtime contract evidence only.

Determine whether the configured Worker mode plus restart should cause the Worker process to execute qualification and attempt durable artifact creation.

Return:

```text
D3 execution path reachable: YES | NO | INDETERMINATE
```

### B. Is the current Kudu VFS URI contract correct?

Evaluate the current constructed URI:

```text
https://<app>.scm.azurewebsites.net/api/vfs/home/data/wp04-qualification/evidence.json
```

Determine whether this exact path is supported for the target App Service environment.

Return:

```text
Kudu VFS URI contract: VALID | INVALID | UNPROVEN
```

### C. Is the current Kudu authentication mechanism supported?

Determine whether locally acquired publishing credentials are a supported authentication method for the selected SCM/Kudu API on this app and whether the app/platform settings permit it.

Return:

```text
Kudu authentication contract: VALID | INVALID | UNPROVEN
```

### D. Can the failure be distinguished without another D3 mutation?

Determine whether existing response/status/error detail, SCM metadata, or a read-only probe can distinguish:

- 401/403 auth failure;
- 404 file/path absence;
- unsupported endpoint;
- network/TLS failure;
- malformed response;
- other transport error.

Return:

```text
Failure classification from existing/read-only evidence: <classification or INDETERMINATE>
```

### E. Is Kudu VFS still a valid governed acceptance channel?

Select exactly one:

```text
K1 — KEEP_KUDU_VFS
K2 — CORRECT_KUDU_VFS_PATH_OR_AUTH
K3 — REPLACE_WITH_OTHER_SUPPORTED_APP_SERVICE_FILE_SURFACE
K4 — REVISE_APPLICATION_EVIDENCE_CHANNEL
K5 — INSUFFICIENT_EVIDENCE
```

A replacement surface must remain:

- supported by Azure/App Service;
- application-owned artifact based;
- non-SQLite-inspection;
- deterministic;
- scriptable;
- secret-safe;
- compatible with persistent `/home`;
- suitable for initialize/restart/redeploy evidence.

## 6. Repository-change decision

Return exactly one:

```text
REPOSITORY CHANGE REQUIRED: NO
```

or:

```text
REPOSITORY CHANGE REQUIRED: YES
```

If `YES`, define the **minimum exact path allowlist**.

Prefer the smallest possible change.

Potential outcomes include:

- one-path correction to `verify-persistent-sqlite-webapp.ps1`;
- one-path enhancement to expose precise HTTP status/retrieval diagnostics;
- broader application change only if the current artifact channel itself is proven invalid.

Do not authorize unrelated paths.

## 7. Retry governance

A new Azure D3 attempt may be authorized only if Luna establishes a deterministic corrected evidence path.

If a retry is authorized, specify:

- exact candidate finality;
- whether a new source commit/image is required;
- whether the currently deployed immutable digest may remain;
- whether only helper/operator logic changes;
- exact mutation scope;
- whether initialize must be rerun from a fresh run ID;
- whether restart/redeploy gates remain unchanged.

No generic “retry Kudu” instruction is acceptable.

## 8. Candidate finality rule

Current candidate source/image finality depends on reconciliation:

### If no repository change is required

The source commit/image may remain authoritative:

```text
2add79d2063292687d9813f9022206e84b664627
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

### If a tracked repository change is required

The current source/image becomes historical for final acceptance:

```text
CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION
```

A new commit and immutable image digest will then be required.

## 9. Mutation prohibition

This Luna authority is read-only/planning only.

Expected mutations:

```text
Repository mutations: 0
Git staging: 0
Git commits: 0
Git pushes: 0
Docker/GHCR mutations: 0
Azure app-setting mutations: 0
Azure restart/redeploy mutations: 0
Provider mutations: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE RECONCILIATION MUTATION AUDIT: PASS`

## 10. Required output

Return:

- D3 execution-path assessment;
- exact Kudu URI assessment;
- exact Kudu auth assessment;
- best-supported failure classification;
- selected K1–K5 decision;
- repository-change decision;
- exact remediation allowlist if any;
- candidate finality;
- whether a new source commit/image is required;
- exact next Terra authority scope;
- confirmation that no mutation occurred.

## 11. Terminal markers

Required reconciliation markers:

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE CLASSIFICATION: <value>`

`RELEASE 1.12 WP04 — KUDU EVIDENCE CHANNEL DECISION: <K1|K2|K3|K4|K5>`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE RECONCILIATION MUTATION AUDIT: PASS`

If a deterministic remediation is established:

`RELEASE 1.12 WP04 — TERRA KUDU RETRIEVAL REMEDIATION/RETRY AUTHORITY: READY`

Final marker:

`RELEASE 1.12 WP04 — LUNA KUDU RETRIEVAL FAILURE RECONCILIATION COMPLETE`

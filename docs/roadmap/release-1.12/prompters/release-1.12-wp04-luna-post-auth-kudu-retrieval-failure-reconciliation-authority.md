# GPT-5.6 Luna — Release 1.12 WP04 Post-Auth Kudu Retrieval Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: classify the post-auth Kudu retrieval failure, reconcile the evidence-channel contract, and define the minimum next authority. Read-only/planning only.
- **GPT-5.6 Terra** — implementation, validation execution, approved Azure/Git/GitHub mutations, publication, merge, and lifecycle.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Authoritative source commit remains:

`2add79d2063292687d9813f9022206e84b664627`

Authoritative immutable image digest remains:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

The exact digest is already deployed.

Accepted SCM remediation evidence:

```text
SCM policy before: allow=false
SCM policy after: allow=true
FTP policy after: allow=false
Authenticated SCM probe after remediation: HTTP 200
```

Fresh initialize attempt:

```text
RunId:
wp04-initialize-b9a49417252443caa8411a728b0b9f59

Temporary D3 settings applied: True
LifecycleAction: Restart
Temporary D3 settings restored: True
Helper exit code: 1
Observed failure:
Kudu evidence artifact retrieval failed.
```

Observed mutation accounting:

```text
Azure SCM basic-auth policy updates: 1
Azure temporary D3 app-setting batch operations: 2
Azure restart actions: 1
Repository/Git/Docker/GHCR/image-reference/PR/issue/Project/milestone mutations: 0
```

Accepted markers:

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — KUDU AUTHENTICATION PATH AFTER REMEDIATION: PASS`

Blocked markers:

`RELEASE 1.12 WP04 — SCM AUTH REMEDIATION/INITIALIZE RETRY: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

## 2. Reconciliation purpose

The prior authentication-policy failure is resolved.

The current failure is **not** permitted to be classified as authentication failure merely because the helper still reports the generic string:

```text
Kudu evidence artifact retrieval failed.
```

Luna must determine which post-auth boundary is now failing.

Candidate classifications:

1. `ARTIFACT_NOT_PRODUCED`
2. `ARTIFACT_PATH_NOT_FOUND`
3. `KUDU_VFS_PATH_MAPPING_INVALID`
4. `KUDU_VFS_ENDPOINT_UNSUPPORTED_OR_INCOMPATIBLE`
5. `KUDU_RESPONSE_STATUS_NOT_EXPOSED_BY_HELPER`
6. `QUALIFICATION_PROCESS_NOT_READY_BEFORE_RETRIEVAL`
7. `QUALIFICATION_PROCESS_FAILED_BEFORE_ARTIFACT_WRITE`
8. `ARTIFACT_WRITE_PATH_OR_PERMISSION_FAILURE`
9. `KUDU_RETRIEVAL_SUCCEEDED_BUT_RESPONSE_HANDLING_FAILED`
10. `INDETERMINATE`

Do not choose a classification without evidence.

## 3. Read-only-only boundary

This Luna authority permits no mutation.

Do not:

- alter repository files;
- alter Azure settings;
- restart the Web App;
- redeploy;
- rerun D3;
- enable new logging;
- inspect SQLite directly;
- create a PR;
- mutate issue/Project/milestone state.

Use only:

- repository source review;
- existing local command output;
- existing Azure/SCM metadata;
- read-only SCM/Kudu requests;
- read-only App Service configuration;
- existing platform/runtime diagnostics that require no configuration change.

## 4. Required repository review

Inspect the committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Determine exactly:

- which HTTP method is used;
- exact Kudu URI construction;
- authentication method;
- timeout behavior;
- retry behavior;
- status-code handling;
- exception handling;
- whether response body/status are intentionally discarded;
- whether 404, 401, 403, 5xx, timeout, DNS/TLS, and malformed response collapse into the same generic failure;
- whether artifact retrieval happens immediately after restart or polls for deterministic completion.

Inspect application code responsible for durable artifact creation and determine:

- exact artifact path normalization;
- directory creation semantics;
- atomic write semantics;
- failure handling;
- whether write failure exits qualification nonzero;
- whether the Worker process exits after qualification;
- whether entrypoint/supervision could restart or replace the Worker before retrieval;
- whether any stdout/stderr evidence from the fresh run already exists.

## 5. Required read-only Kudu probes

Because SCM authentication is now proven functional, use read-only requests to classify the VFS/path contract without triggering a new D3 run.

At minimum, assess:

### A. SCM root

Confirm authenticated SCM access remains successful.

Return:

```text
SCM authenticated base access: PASS | FAIL
```

### B. VFS root/path hierarchy

Using authenticated read-only Kudu VFS requests, determine whether these surfaces are accessible:

```text
/api/vfs/
/api/vfs/home/
/api/vfs/home/data/
/api/vfs/home/data/wp04-qualification/
```

Do not create/delete/modify anything.

Capture only sanitized status codes and non-secret structural metadata.

Required output:

```text
/api/vfs/                             -> <status>
/api/vfs/home/                        -> <status>
/api/vfs/home/data/                   -> <status>
/api/vfs/home/data/wp04-qualification/ -> <status>
```

### C. Exact evidence artifact

Probe:

```text
/api/vfs/home/data/wp04-qualification/evidence.json
```

Capture sanitized HTTP status.

Interpretation guidance:

- `200` → artifact exists; helper response handling may be defective.
- `404` with parent directory accessible → artifact absent, likely production/write/timing failure.
- parent directory absent → path creation/write failure or path assumption invalid.
- VFS root/path itself unsupported → Kudu path/surface problem.
- `401/403` → authentication/authorization remains unresolved despite SCM base success.
- `5xx`/transport failure → SCM/Kudu platform or request-surface problem.

Do not expose artifact body unless existing governance already permits sanitized JSON evidence and it contains no secrets.

## 6. Required App Service runtime/path reconciliation

Read-only determine whether, for this Linux App Service container:

```text
/home
```

is actually exposed by Kudu VFS under:

```text
/api/vfs/home/
```

Do not assume Linux App Service behavior from Windows Kudu semantics.

Return exactly:

```text
KUDU /home VFS mapping:
VALID | INVALID | UNPROVEN
```

If invalid or unsupported, identify the Azure-supported read-only file surface that can retrieve application-owned `/home` files without direct SQLite inspection.

## 7. Required timing reconciliation

Determine whether the current helper has a deterministic completion/readiness boundary between:

1. restart;
2. application startup;
3. D3 qualification execution;
4. atomic artifact write;
5. retrieval.

Return exactly:

```text
D3 retrieval timing contract:
DETERMINISTIC | RACY | UNPROVEN
```

If retrieval is immediate or uses only platform `Running` state, classify it as insufficient unless repository evidence proves D3 completion is synchronized.

## 8. Required artifact-production assessment

Using only existing evidence and read-only surfaces, return:

```text
Fresh initialize artifact production:
PROVEN | NOT_PROVEN | PROVEN_ABSENT
```

Definitions:

- `PROVEN` — exact fresh run artifact exists and is attributable.
- `PROVEN_ABSENT` — supported read-only surface proves the expected artifact/path is absent after the completed attempt.
- `NOT_PROVEN` — evidence cannot distinguish absence from unsupported retrieval/timing.

Do not infer production merely from D3 path reachability.

## 9. Required decision

Select exactly one reconciliation decision:

```text
P1 — HELPER_DIAGNOSTICS_ONLY
P2 — HELPER_TIMING_AND_DIAGNOSTICS_REMEDIATION
P3 — KUDU_PATH_CORRECTION
P4 — APPLICATION_ARTIFACT_WRITE_REMEDIATION
P5 — REPLACE_KUDU_WITH_OTHER_SUPPORTED_FILE_SURFACE
P6 — NO_REPOSITORY_CHANGE_AZURE_ONLY_RETRY
P7 — INSUFFICIENT_EVIDENCE
```

Decision constraints:

### P1 — HELPER_DIAGNOSTICS_ONLY
Use only if the evidence channel/path is valid and the immediate problem is that the helper hides decisive status/response details.

### P2 — HELPER_TIMING_AND_DIAGNOSTICS_REMEDIATION
Use if Kudu/path are valid but retrieval lacks a deterministic wait/poll boundary.

### P3 — KUDU_PATH_CORRECTION
Use if Kudu is valid but the exact URI/path mapping is wrong.

### P4 — APPLICATION_ARTIFACT_WRITE_REMEDIATION
Use only if read-only evidence proves the app is failing to create/write the artifact and the defect is in tracked application/runtime code.

### P5 — REPLACE_KUDU_WITH_OTHER_SUPPORTED_FILE_SURFACE
Use if Kudu VFS cannot serve `/home` reliably for this Linux App Service target.

### P6 — NO_REPOSITORY_CHANGE_AZURE_ONLY_RETRY
Use only if existing read-only evidence proves the current helper/channel is correct and the blocked attempt was transient, with a deterministic retry justification.

### P7 — INSUFFICIENT_EVIDENCE
Use if read-only evidence cannot safely establish a deterministic correction.

## 10. Repository-change decision

Return exactly one:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES
```

or:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO
```

If `YES`, define the minimum exact tracked path allowlist.

Prefer the smallest possible path set.

Likely one-path outcomes may include:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Do not authorize application-code changes unless P4 is evidence-backed.

## 11. Candidate finality

If a tracked repository change is required:

```text
RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION
```

Then a new candidate commit and immutable image digest are required unless Luna explicitly proves the changed tracked file is operator-only and cannot affect the image/runtime artifact.

If no repository change is required:

```text
RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED
```

The current commit/digest remain:

```text
2add79d2063292687d9813f9022206e84b664627
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

### Important operator-script distinction

If the only tracked change is the PowerShell verification helper and the Docker build context/runtime image does not consume that helper, Luna must explicitly decide whether a new immutable image is actually necessary.

Do not automatically require a new image solely because a tracked operator script changed.

Return one:

```text
NEW IMAGE REQUIRED: YES
NEW IMAGE REQUIRED: NO
```

with rationale.

## 12. SCM policy state

Current governed SCM policy after remediation:

```text
allow=true
```

Do not revert it under this Luna authority.

Determine whether it should remain enabled through remaining WP04 qualification.

Return:

```text
SCM basic-auth policy during remaining WP04 qualification:
KEEP_ENABLED | RECONCILE_LATER
```

## 13. Exact next Terra scope

If a deterministic correction is established, define the next Terra authority narrowly.

Examples:

- one-path helper diagnostics/timing remediation + local validation only;
- one-path path correction + local validation only;
- Azure-only initialize retry with current candidate;
- alternative supported file-surface validation;
- application artifact-write remediation if proven.

The next authority must not automatically include restart/reopen, same-digest redeploy, PR creation, or lifecycle completion unless initialize itself has first passed.

## 14. Mutation audit

Expected mutations under this Luna authority:

```text
Repository mutations: 0
Git mutations: 0
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

`RELEASE 1.12 WP04 — POST-AUTH KUDU FAILURE RECONCILIATION MUTATION AUDIT: PASS`

## 15. Required output markers

Return all of:

`RELEASE 1.12 WP04 — POST-AUTH KUDU FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — SCM AUTHENTICATED BASE ACCESS: <PASS|FAIL>`

`RELEASE 1.12 WP04 — KUDU HOME VFS MAPPING: <VALID|INVALID|UNPROVEN>`

`RELEASE 1.12 WP04 — D3 RETRIEVAL TIMING CONTRACT: <DETERMINISTIC|RACY|UNPROVEN>`

`RELEASE 1.12 WP04 — FRESH INITIALIZE ARTIFACT PRODUCTION: <PROVEN|NOT_PROVEN|PROVEN_ABSENT>`

`RELEASE 1.12 WP04 — POST-AUTH KUDU FAILURE CLASSIFICATION: <value>`

`RELEASE 1.12 WP04 — POST-AUTH KUDU RECONCILIATION DECISION: <P1|P2|P3|P4|P5|P6|P7>`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — POST-AUTH KUDU FAILURE RECONCILIATION MUTATION AUDIT: PASS`

If deterministic remediation exists:

`RELEASE 1.12 WP04 — TERRA POST-AUTH KUDU REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA POST-AUTH KUDU FAILURE RECONCILIATION COMPLETE`

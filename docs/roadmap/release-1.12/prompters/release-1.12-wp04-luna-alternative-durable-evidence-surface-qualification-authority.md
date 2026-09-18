# GPT-5.6 Luna — Release 1.12 WP04 Alternative Durable-Evidence Surface Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: identify and qualify a supported, deterministic Azure/App Service surface for retrieving the application-owned durable WP04 evidence artifact. Read-only/planning only.
- **GPT-5.6 Terra** — implementation, validation execution, approved Azure/Git/GitHub mutations, publication, merge, and lifecycle after Luna defines the contract.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Authoritative candidate source commit:

`2add79d2063292687d9813f9022206e84b664627`

Authoritative immutable image digest:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

Current Azure image deployment remains the exact immutable digest above.

SCM basic publishing-credentials policy:

```text
allow=true
```

FTP basic-auth policy remains:

```text
allow=false
```

Accepted post-auth reconciliation:

```text
Authenticated SCM base: HTTP 200
/api/vfs/: HTTP 200
/api/vfs/home/: HTTP 404
/api/vfs/home/data/: HTTP 404
/api/vfs/home/data/wp04-qualification/: HTTP 404
exact evidence artifact: HTTP 404
```

Kudu root entries observed:

```text
site
LogFiles
services
ASP.NET data
```

No `home` mapping is exposed.

Accepted markers:

`RELEASE 1.12 WP04 — POST-AUTH KUDU FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — KUDU HOME VFS MAPPING: INVALID`

`RELEASE 1.12 WP04 — D3 RETRIEVAL TIMING CONTRACT: RACY`

`RELEASE 1.12 WP04 — FRESH INITIALIZE ARTIFACT PRODUCTION: NOT_PROVEN`

`RELEASE 1.12 WP04 — POST-AUTH KUDU RECONCILIATION DECISION: P7`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED`

No retry, restart, redeploy, repository, or GitHub mutation is currently authorized.

## 2. Purpose

Determine whether a supported, deterministic, non-SQLite Azure/App Service evidence-retrieval surface exists for the application-owned durable artifact:

```text
/home/data/wp04-qualification/evidence.json
```

This authority is for **surface discovery and qualification only**.

Do not rerun D3.

Do not modify the repository.

Do not modify Azure.

Do not restart or redeploy.

## 3. Evidence-channel invariants

Any acceptable replacement evidence surface must preserve all of the following:

- evidence remains application-owned;
- evidence payload remains the D3 JSON qualification record;
- persistent storage remains `/home`;
- SQLite remains owned by the .NET application/provider boundary;
- direct SQLite shell/API/Python inspection remains forbidden;
- Streamlit/Python does not acquire persistence ownership;
- no secret is written into the artifact;
- no credential/token/header is returned as evidence;
- retrieval is scriptable and deterministic;
- exact run-ID attribution remains mandatory;
- initialize/restart/redeploy acceptance semantics remain unchanged;
- strict F1 / $0 architecture remains unchanged;
- no paid service may be introduced;
- no separate persistence implementation may be introduced merely for evidence transport.

## 4. Required research scope

Qualify the actual Linux App Service environment and supported surfaces that can access application-owned persistent `/home` content.

At minimum investigate:

### A. App Service SCM/Kudu surfaces

Determine whether any SCM endpoint other than the currently tested VFS mapping exposes Linux persistent `/home` content.

Examples to investigate only if supported/documented in the actual target environment:

- Kudu command APIs;
- Kudu file APIs;
- SCM diagnostic APIs;
- Linux-specific filesystem mappings;
- ZIP/file download surfaces;
- container-instance filesystem endpoints.

Do not execute shell commands under this authority unless they are strictly read-only and the resulting method would itself be acceptable as a governed evidence surface. Direct SQLite access remains forbidden.

### B. Azure Resource Manager / Azure CLI surfaces

Determine whether Azure Resource Manager, Azure CLI, or App Service management APIs expose a supported read-only way to retrieve a specific file from the running Linux Web App persistent `/home`.

The surface must support deterministic retrieval of the exact artifact, not just logs.

### C. App Service deployment/storage surfaces

Determine whether `/home` content can be read via a supported App Service content/deployment endpoint distinct from the invalid Kudu `/api/vfs/home/...` assumption.

### D. Application-owned HTTP evidence endpoint

Assess whether exposing the sanitized D3 evidence through the application itself is a valid fallback.

If considered, determine the minimum architecture needed so that:

- the application remains the owner of evidence generation;
- only the sanitized D3 record is exposed;
- no SQLite access is introduced;
- no arbitrary file browser is exposed;
- exact run-ID matching remains required;
- normal runtime does not leak qualification state;
- endpoint availability can be tightly scoped to qualification mode;
- secrets are never emitted;
- it works under the existing public App Service boundary;
- it does not create a new production API contract beyond WP04 qualification unless separately governed.

This option must not be selected merely because it is easy; compare it against supported Azure-native retrieval surfaces first.

### E. Application-owned stdout/log transport

Reconsider only if there is a deterministic, supported log-access path that can attribute an exact D3 run and does not repeat the previously rejected nondeterministic stdout/logging behavior.

Historical generic log-stream retry is not acceptable.

## 5. Required evidence for each candidate surface

For every candidate retrieval surface, return:

```text
Surface name
Support status: SUPPORTED | UNSUPPORTED | UNPROVEN
Works on Linux App Service F1: YES | NO | UNPROVEN
Can retrieve exact /home artifact: YES | NO | UNPROVEN
Requires Azure mutation: YES | NO
Requires repository change: YES | NO
Requires new image: YES | NO
Requires paid service: YES | NO
Secret-safe: YES | NO | UNPROVEN
Deterministic run attribution possible: YES | NO | UNPROVEN
Suitable for initialize/restart/redeploy qualification: YES | NO | UNPROVEN
```

Do not collapse multiple distinct mechanisms into one row.

## 6. Candidate decision set

Select exactly one outcome:

```text
A1 — SUPPORTED_AZURE_NATIVE_FILE_RETRIEVAL_SURFACE
A2 — SUPPORTED_SCM_NON_VFS_FILE_RETRIEVAL_SURFACE
A3 — APPLICATION_OWNED_SANITIZED_HTTP_EVIDENCE_ENDPOINT
A4 — DETERMINISTIC_SUPPORTED_LOG_TRANSPORT
A5 — NO_ACCEPTABLE_SURFACE_PROVEN
```

### A1
Use only if an Azure management/storage surface can deterministically retrieve the exact persistent `/home` artifact without direct SQLite inspection.

### A2
Use only if a supported SCM/Kudu surface distinct from the invalid `/api/vfs/home/...` mapping can retrieve the exact artifact.

### A3
Use only if Azure-native/SCM file retrieval is unavailable and a narrowly scoped application-owned evidence endpoint is the smallest safe deterministic solution.

### A4
Use only if a deterministic, exact-run attributable log transport is proven and does not recreate the earlier nondeterministic stdout problem.

### A5
Use if no surface can be proven without broader architecture reconsideration.

## 7. Timing contract requirement

The selected surface must also solve the current racy retrieval behavior.

Define a deterministic readiness mechanism.

Acceptable patterns may include:

- polling the selected evidence surface for the exact expected run ID until bounded timeout;
- an application-owned readiness/qualification-complete signal;
- another deterministic completion contract.

Unacceptable:

- fixed arbitrary sleep only;
- relying only on App Service `Running`;
- assuming the artifact exists immediately after restart;
- repeated blind restarts.

Return:

```text
Selected readiness mechanism: <description>
Bounded timeout: <value or policy>
Retry interval/backoff: <value or policy>
Terminal failure semantics: <description>
```

## 8. Repository-change decision

Return exactly one:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES
```

or:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO
```

If `YES`, define the minimum exact tracked path allowlist.

Potential examples, depending on the selected surface:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

or, only if A3 is selected and application change is necessary:

```text
MODIFY src/AIQuantTradingResearch.Worker/...
MODIFY tests/...
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Do not authorize broader paths than evidence requires.

## 9. New-image decision

Return exactly one:

```text
RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: YES
```

or:

```text
RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO
```

Rules:

- helper/operator-script-only change that is not consumed by the runtime image should normally imply `NO`, if proven;
- Worker/runtime application change implies `YES`;
- do not automatically rebuild merely because a tracked script changes.

## 10. Candidate finality

If no runtime-image change is required, the current immutable image may remain authoritative:

```text
2add79d2063292687d9813f9022206e84b664627
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

Return:

```text
RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED
```

If a runtime-image-affecting change is required:

```text
RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION
```

## 11. SCM basic-auth policy

Current policy remains:

```text
allow=true
```

Because Kudu `/home` VFS is invalid, Luna must decide whether this policy still has a governed purpose.

Return one:

```text
SCM BASIC AUTH POLICY: KEEP_ENABLED_FOR_SELECTED_SURFACE
SCM BASIC AUTH POLICY: NO_LONGER_REQUIRED_RESTORE_FALSE_LATER
SCM BASIC AUTH POLICY: INDETERMINATE
```

Do not mutate the policy under this authority.

## 12. Exact next Terra scope

If A1–A4 is selected, define the next Terra authority narrowly.

The next authority should normally cover only:

- the exact minimum remediation needed for the selected surface;
- local validation;
- no D3 retry until the remediation itself passes.

If `REPOSITORY CHANGE REQUIRED: NO`, the next Terra authority may instead be an Azure-only/read-only surface proof plus one fresh initialize retry.

Do not authorize restart/reopen, same-digest redeploy, PR creation, or lifecycle completion until initialize passes.

## 13. Mutation prohibition

This Luna authority is read-only/planning only.

Expected mutations:

```text
Repository mutations: 0
Git mutations: 0
Docker/GHCR mutations: 0
Azure configuration mutations: 0
Azure restart/redeploy mutations: 0
Provider mutations: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — ALTERNATIVE EVIDENCE SURFACE QUALIFICATION MUTATION AUDIT: PASS`

## 14. Required output

Return:

- candidate-surface comparison;
- selected A1–A5 decision;
- selected deterministic readiness contract;
- repository-change decision;
- exact remediation allowlist if any;
- new-image decision;
- candidate finality;
- SCM basic-auth policy disposition;
- exact next Terra authority scope;
- confirmation that no mutation occurred.

## 15. Terminal markers

Required:

`RELEASE 1.12 WP04 — ALTERNATIVE DURABLE EVIDENCE SURFACE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — EVIDENCE SURFACE DECISION: <A1|A2|A3|A4|A5>`

`RELEASE 1.12 WP04 — DETERMINISTIC READINESS CONTRACT: DEFINED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — ALTERNATIVE EVIDENCE SURFACE QUALIFICATION MUTATION AUDIT: PASS`

If A1–A4 yields a deterministic next step:

`RELEASE 1.12 WP04 — TERRA ALTERNATIVE EVIDENCE SURFACE REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA ALTERNATIVE EVIDENCE SURFACE QUALIFICATION COMPLETE`

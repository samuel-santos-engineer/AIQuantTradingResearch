# GPT-5.6 Luna — Release 1.12 WP04 Evidence-Channel Architecture Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: re-govern the WP04 evidence-channel architecture after Azure-native/SCM durable-file retrieval was proven unavailable for the target Linux App Service environment.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub/Azure mutations, image publication, deployment, merge, and lifecycle after Luna defines an executable contract.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current preserved candidate source:

`2add79d2063292687d9813f9022206e84b664627`

Current preserved immutable image:

`sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`

Current deployed image is the exact immutable digest above.

Current SCM publishing-credentials policy:

```text
allow=true
```

Accepted evidence-surface qualification:

`RELEASE 1.12 WP04 — ALTERNATIVE DURABLE EVIDENCE SURFACE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — EVIDENCE SURFACE DECISION: A5`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

Read-only investigation established:

- authenticated SCM root is reachable;
- Kudu `/api/vfs/home/...` is unavailable for this Linux App Service target;
- no alternative SCM/ARM/CLI/deployment exact-file retrieval surface was proven;
- stdout/log transport remains nondeterministic for exact durable-run attribution;
- application-owned HTTP evidence endpoint does not currently exist;
- such an endpoint would require a runtime application change and therefore a new image;
- no D3 retry is currently authorized;
- SCM basic auth is no longer required for the rejected Kudu evidence path and should be restored to `allow=false` later under separate Terra authority.

## 2. Purpose

Re-govern the WP04 acceptance evidence channel so the original persistence acceptance boundary remains intact without relying on an unsupported Azure file-retrieval surface.

Luna must decide whether to authorize a **narrow application-owned HTTP qualification evidence endpoint** or select another architecture-level remediation.

This authority is read-only/planning only.

## 3. Original WP04 acceptance boundary remains binding

The following may not be weakened merely to unblock validation:

- application-owned SQLite initialization;
- schema version `4`;
- SQLite journal mode `DELETE`;
- persistent database path `/home/data/aiquant.db`;
- persistent `/home`;
- accepted-evidence identity/count semantics;
- idempotency/conflict semantics;
- restart/reopen continuity;
- true redeploy/reopen continuity;
- integrity check `ok`;
- quick-check `ok`;
- exact run-ID attribution;
- strict F1 / $0 reference deployment;
- no direct SQLite shell/Python inspection;
- no Python/Streamlit persistence ownership;
- no provider bypass;
- no production-SLA claims.

The problem to solve is evidence transport, not persistence semantics.

## 4. Primary architecture option to evaluate

Evaluate:

```text
H1 — APPLICATION_OWNED_SANITIZED_QUALIFICATION_HTTP_ENDPOINT
```

The endpoint must expose only the already-governed sanitized D3 qualification record produced by the .NET application.

It must not become a generic file browser, SQLite endpoint, database API, or production administration API.

## 5. Required H1 architecture contract

If H1 is selected, define all of the following.

### 5.1 Ownership

The endpoint must remain within the .NET application/runtime ownership boundary.

It must not move SQLite ownership to:

- Streamlit;
- Python;
- deployment scripts;
- Kudu;
- Azure management tooling.

### 5.2 Activation scope

The endpoint must be available only when explicitly enabled for governed persistence qualification.

Preferred contract:

```text
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

or an equivalent explicit qualification-only setting.

Normal runtime must not expose the endpoint by default.

### 5.3 Endpoint surface

Define one narrow endpoint, for example:

```text
GET /internal/wp04/persistence-qualification
```

The exact path is a Luna decision.

It must return only one sanitized qualification record.

No arbitrary path/file/query access.

No mutation verbs.

No SQLite query parameters.

### 5.4 Exact-run attribution

The request must require the expected run identity, either by:

- path parameter;
- query parameter;
- request header;
- or exact comparison performed by the verification helper after retrieval.

The response is accepted only if:

```text
RunId == expected run ID
```

A stale prior record must fail.

### 5.5 Payload contract

The endpoint payload must contain only the governed record fields:

```text
RecordVersion
Phase
RunId
DatabasePathIdentity
SchemaVersion
JournalMode
AcceptedEvidenceIdentity
AcceptedEvidenceCount
IntegrityCheck
QuickCheck
PersistenceContinuity
```

No:

- secrets;
- environment dumps;
- credentials;
- connection strings;
- full filesystem paths beyond the already-governed non-secret database identity;
- arbitrary diagnostic state.

### 5.6 Source of truth

The HTTP response must be derived from the canonical application-owned qualification result.

Luna must select one:

```text
H1-S1 — serve the in-memory canonical qualification record
H1-S2 — serve the atomic durable qualification artifact written by the application
```

If `H1-S2` is selected, the application itself may read its own governed artifact.

Deployment scripts must still not directly inspect `/home`.

### 5.7 Process lifetime

The current qualification mode may exit after execution.

Luna must explicitly reconcile how an HTTP endpoint remains available long enough for retrieval.

Select one:

```text
H1-L1 — Worker qualification host remains alive in bounded evidence-serving mode
H1-L2 — existing ASP.NET/host process serves the evidence while qualification state is retained
H1-L3 — another narrow application-owned lifecycle contract
```

Do not leave this ambiguous.

### 5.8 Readiness contract

Define deterministic polling:

```text
Expected RunId: exact
Poll interval: bounded
Overall timeout: bounded
HTTP 404/409/425 or equivalent before readiness: retryable
Exact matching record: success
Malformed/wrong-run/failure payload: terminal failure
Timeout: terminal failure
```

Fixed sleep alone is forbidden.

App Service `Running` alone is insufficient.

### 5.9 Public exposure/security

Because the reference Web App is public, Luna must explicitly govern access.

Select a minimal mechanism appropriate to a qualification-only endpoint, such as:

```text
H1-A1 — high-entropy per-run capability token supplied through temporary app setting/request header
H1-A2 — endpoint only exposes sanitized non-secret record and requires exact unpredictable RunId
H1-A3 — platform-authenticated/internal-only route if already available at $0
```

Do not introduce a paid service.

Do not invent a production security claim.

Any token must:

- never be persisted in the JSON evidence record;
- never be printed;
- be temporary;
- be restored/removed after qualification.

### 5.10 Normal-runtime behavior

After temporary qualification settings are restored:

- the endpoint must be disabled or unavailable;
- ordinary Worker/Streamlit runtime must resume;
- no qualification secret/token remains;
- no stale qualification endpoint should remain publicly usable.

## 6. Alternative architecture options

Compare H1 against:

```text
H2 — DETERMINISTIC APPLICATION-OWNED LOG/EVENT TRANSPORT
H3 — RESTRUCTURE QUALIFICATION INTO EXISTING PUBLIC APPLICATION SURFACE
H4 — DEFER WP04 AZURE CONTINUITY ACCEPTANCE AND REDEFINE RELEASE SCOPE
H5 — NO_SAFE_ARCHITECTURE
```

H2 may be selected only if exact run attribution and deterministic retrieval are proven.

H3 must not contaminate Streamlit with persistence ownership.

H4 is a release-governance change and requires explicit statement of what acceptance is removed/deferred and why; it must not silently weaken WP04.

H5 means WP04 remains blocked.

## 7. Required decision

Select exactly one:

```text
E1 — H1_APPLICATION_HTTP_EVIDENCE
E2 — H2_DETERMINISTIC_LOG_EVENT_TRANSPORT
E3 — H3_EXISTING_APPLICATION_SURFACE
E4 — H4_RELEASE_SCOPE_REGOVERNANCE
E5 — H5_NO_SAFE_ARCHITECTURE
```

If E1 is selected, fully define sections 5.1–5.10.

## 8. Repository-change decision

Return exactly one:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES
```

or:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO
```

If `YES`, define the minimum exact path allowlist.

For E1, likely categories may include:

- Worker qualification execution/hosting;
- Worker configuration binding;
- targeted tests;
- verification helper.

Do not authorize unrelated files.

Do not authorize README changes.

Front-door README preservation policy remains active.

## 9. Schema decision

The evidence-channel remediation must not change SQLite schema unless technically unavoidable and separately proven.

Expected:

```text
SCHEMA CHANGE REQUIRED: NO
```

If Luna concludes otherwise, STOP and require separate schema governance.

## 10. New candidate/image decision

If runtime code changes:

```text
NEW SOURCE COMMIT REQUIRED: YES
NEW IMAGE REQUIRED: YES
CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION
```

If only operator tooling changes and the deployed application remains unchanged, Luna may preserve the image only with explicit rationale.

## 11. Existing candidate evidence preservation

Even if superseded, preserve provenance of:

```text
source commit:
2add79d2063292687d9813f9022206e84b664627

image digest:
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

Do not rewrite history.

## 12. SCM basic-auth restoration

Because Kudu `/home` retrieval is rejected, current:

```text
SCM basic publishing credentials allow=true
```

is no longer required by the evidence contract.

Luna must include in the next applicable Terra authority:

```text
restore SCM publishing-credentials policy to allow=false
```

unless the selected replacement architecture has a proven need for SCM basic auth.

FTP must remain:

```text
allow=false
```

## 13. Validation requirements for an E1 remediation

If E1 is selected, the future Terra implementation must validate at minimum:

- Release build: 0 warnings / 0 errors;
- existing Domain tests;
- existing Application tests;
- existing Architecture tests;
- existing Infrastructure tests;
- targeted qualification HTTP tests;
- endpoint disabled in normal runtime;
- exact-run success;
- stale-run rejection;
- malformed payload rejection;
- not-ready polling behavior;
- bounded timeout;
- secret/capability-token non-disclosure;
- no direct SQLite access from helper;
- no Streamlit/Python persistence access;
- atomic durable artifact semantics remain intact if retained;
- `git diff --check`;
- Gitleaks for all changed paths;
- corrected Debug signing contract remains valid.

## 14. Future Azure acceptance sequence

If the remediation reaches executable state, preserve this sequence:

1. publish exact new candidate commit/image if required;
2. deploy exact immutable digest;
3. restore SCM basic auth to `false` if no longer needed;
4. run fresh initialize qualification;
5. retrieve exact-run evidence through the new governed surface;
6. run restart/reopen continuity;
7. run true same-digest redeploy/reopen continuity;
8. restore temporary qualification settings;
9. verify endpoint disabled in normal runtime;
10. create WP04 PR only after all Azure gates pass;
11. final Luna acceptance;
12. Terra merge/lifecycle only after exact acceptance marker.

Do not skip initialize merely because earlier attempts occurred.

## 15. Mutation prohibition

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

`RELEASE 1.12 WP04 — EVIDENCE-CHANNEL ARCHITECTURE RE-GOVERNANCE MUTATION AUDIT: PASS`

## 16. Required output

Return:

- comparison of H1–H5;
- exact E1–E5 decision;
- if E1, complete endpoint/lifetime/readiness/security/source-of-truth contract;
- repository-change decision;
- exact path allowlist;
- schema-change decision;
- new-source-commit decision;
- new-image decision;
- current candidate finality;
- SCM basic-auth restoration requirement;
- exact next Terra authority scope;
- confirmation of zero mutations.

## 17. Terminal markers

Required:

`RELEASE 1.12 WP04 — EVIDENCE-CHANNEL ARCHITECTURE RE-GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — EVIDENCE-CHANNEL ARCHITECTURE DECISION: <E1|E2|E3|E4|E5>`

`RELEASE 1.12 WP04 — ORIGINAL PERSISTENCE ACCEPTANCE BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — SCHEMA CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — SCM BASIC AUTH RESTORATION: REQUIRED_UNLESS_SELECTED_SURFACE_NEEDS_IT`

`RELEASE 1.12 WP04 — EVIDENCE-CHANNEL ARCHITECTURE RE-GOVERNANCE MUTATION AUDIT: PASS`

If E1–E3 defines an executable implementation:

`RELEASE 1.12 WP04 — TERRA EVIDENCE-CHANNEL IMPLEMENTATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA EVIDENCE-CHANNEL ARCHITECTURE RE-GOVERNANCE COMPLETE`

# GPT-5.6 Terra — Release 1.12 WP04 SCM Authentication Remediation & Initialize Retry Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform the narrowly authorized Azure SCM authentication-policy mutation and rerun only the initialize qualification.
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

Accepted Luna reconciliation:

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — KUDU RETRIEVAL FAILURE CLASSIFICATION: SCM_AUTHENTICATION_POLICY_DISABLED_HTTP_401`

`RELEASE 1.12 WP04 — KUDU EVIDENCE CHANNEL DECISION: K2`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: PRESERVED`

Observed policy state:

```text
SCM publishing-credentials policy allow=false
Authenticated SCM base request: HTTP 401
Authenticated Kudu VFS artifact request: HTTP 401
```

No repository or image change is required.

## 2. Exact Azure mutation authority

This authority permits exactly one persistent Azure configuration correction:

> Enable the App Service **SCM basic publishing credentials policy** for the governed Web App.

The intended resulting policy state is:

```text
SCM publishing credentials basic authentication: allow=true
```

Use the supported Azure CLI / ARM surface for the Web App SCM basic-auth publishing-credentials policy.

Do not change FTP basic-auth policy unless the selected supported Azure command/API necessarily couples the setting and that coupling is explicitly evidenced before mutation. If FTP would also change unexpectedly, STOP.

Required:

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION PRECHECK: PASS`

## 3. Mandatory pre-mutation checks

Before enabling SCM basic auth, read-only verify:

- exact target subscription/resource group/Web App;
- App Service plan remains F1 / Free;
- region remains West Central US;
- exact deployed digest remains:
  `sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378`;
- SCM basic-auth policy is currently `allow=false`;
- no registry credentials are configured;
- no stale D3 settings remain;
- no unrelated app-setting drift exists.

Capture the pre-mutation SCM policy state.

## 4. Apply SCM authentication correction

Enable only the SCM publishing-credentials basic-auth policy.

After mutation, read back the policy and require:

```text
allow=true
```

Do not print:

- publishing username;
- publishing password;
- Authorization headers;
- bearer tokens;
- publish profile contents.

Required:

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION: PASS`

## 5. Read-only SCM/Kudu authentication verification

Before rerunning D3, verify the corrected authentication path using a non-mutating SCM/Kudu request.

Acceptable verification:

- authenticated SCM base request no longer returns the prior policy-caused `401`; and/or
- authenticated Kudu VFS request reaches the endpoint such that a missing artifact is distinguishable from authentication rejection.

Do not require the evidence artifact to exist yet.

The critical gate is:

```text
SCM authentication policy no longer blocks Kudu access
```

If requests still return `401`, STOP.

Required:

`RELEASE 1.12 WP04 — KUDU AUTHENTICATION PATH AFTER REMEDIATION: PASS`

## 6. Initialize qualification retry only

Generate a **fresh** initialize run ID.

Do not reuse:

`wp04-initialize-ba06eadf02a14b40bbe1f9789205cc73`

Use the committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Run the initialize attempt with:

```text
Phase = initialize
RunId = <fresh unique initialize run ID>
EvidenceOutputPath = /home/data/wp04-qualification/evidence.json
LifecycleAction = Restart
```

Use exact implemented parameter names.

The helper must:

- snapshot temporary D3 settings;
- apply D3 settings;
- perform the one authorized restart;
- retrieve the durable artifact through Kudu VFS;
- parse/validate locally;
- restore temporary D3 settings;
- return nonzero on retrieval/validation/restoration failure.

## 7. Initialize acceptance contract

Require the retrieved artifact to prove:

```text
RecordVersion = 1
Phase = initialize
RunId = exact fresh initialize run ID
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

Temporary D3 settings must restore successfully.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

## 8. Stop boundary after initialize

This authority ends immediately after the initialize retry result and cleanup verification.

It does **not** authorize:

- restart/reopen continuity qualification;
- same-digest redeploy/reopen qualification;
- additional restart attempts;
- image redeployment;
- repository change;
- Docker/GHCR publication;
- PR creation;
- issue closure;
- Project #2 mutation;
- milestone mutation.

A separate Terra continuation authority is required after initialize passes.

If initialize fails for any reason other than the already-corrected SCM auth policy, STOP and return the exact failure classification.

## 9. SCM policy final state

The SCM basic-auth policy correction is an explicitly governed Azure configuration requirement for the Kudu evidence channel.

Do **not** automatically revert it under this authority.

Report final state:

```text
SCM publishing-credentials policy allow=true
```

If later governance decides the evidence channel should be removed or disabled after WP04/Release 1.12 acceptance, that must be separately authorized.

## 10. Mutation accounting

Report exact actual mutations.

Authorized potential mutations:

```text
Azure SCM basic-auth policy updates: 1
Azure temporary D3 app-setting application: exact count
Azure restart actions: 1
Azure temporary D3 app-setting restoration: exact count
```

Expected zero:

```text
Repository file mutations: 0
Git staging mutations: 0
Git commit mutations: 0
Git pushes: 0
Git tags: 0
Docker mutations: 0
GHCR mutations: 0
Azure image-reference updates: 0
Azure SKU/region mutations: 0
Registry credential mutations: 0
Provider mutations: 0
GitHub PR mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — SCM AUTH REMEDIATION/INITIALIZE RETRY MUTATION AUDIT: PASS`

## 11. Stop conditions

STOP if:

- target Web App identity is ambiguous;
- exact deployed digest changed;
- SCM policy cannot be changed independently as governed;
- enabling SCM policy would introduce a paid service;
- SCM requests still return HTTP 401 after remediation;
- Kudu URI remains unsupported after auth succeeds;
- artifact retrieval fails;
- run-ID attribution fails;
- D3 field validation fails;
- helper restoration fails;
- direct SQLite inspection appears necessary;
- additional repository/image remediation appears necessary.

Do not self-authorize restart/reopen continuation after initialize success.

## 12. Return evidence

Return:

- pre/post SCM policy state;
- non-secret proof that SCM/Kudu authentication is no longer blocked;
- fresh initialize run ID;
- sanitized initialize durable record;
- baseline evidence identity/count;
- D3 restoration result;
- exact mutation accounting.

Do not return credentials, tokens, Authorization headers, or publish-profile contents.

## 13. Terminal markers

### Success

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — SCM BASIC AUTH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — KUDU AUTHENTICATION PATH AFTER REMEDIATION: PASS`

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — SCM AUTH REMEDIATION/INITIALIZE RETRY MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA RESTART/REDEPLOY QUALIFICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA SCM AUTHENTICATION REMEDIATION & INITIALIZE RETRY COMPLETE`

### Blocked

`RELEASE 1.12 WP04 — SCM AUTH REMEDIATION/INITIALIZE RETRY: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

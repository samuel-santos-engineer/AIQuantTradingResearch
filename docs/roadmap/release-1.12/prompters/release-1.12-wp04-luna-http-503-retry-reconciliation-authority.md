# GPT-5.6 Luna — Release 1.12 WP04 HTTP 503 Retry Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the Azure helper's HTTP transient-readiness behavior and define the exact one-path remediation boundary.
- **GPT-5.6 Terra** — implementation, validation, publication, and resumed Azure qualification only under subsequent explicit authorities.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
da5108a6ad73b6e1a23df2b9df3d170d1658886a
```

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Current Azure state after the failed initialize attempt:

```text
App Service state: Running / Normal
SCM basic auth: false
FTP basic auth: false
temporary D3 settings: restored
temporary evidence token: restored/absent
runtime image: unchanged exact digest
PR/lifecycle mutations: none
```

Observed initialize RunId:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
```

This attempt is **not accepted** as qualification evidence and must not be reused for a future governed retry.

## 2. Observed blocker

The committed helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

currently treats only HTTP `404` as retryable during evidence polling.

The Azure App Service public endpoint returned transient HTTP:

```text
503
```

during qualification startup.

The helper therefore threw:

```text
Application-owned HTTP evidence retrieval failed.
```

before evidence capture.

Important observations:

- temporary qualification settings were restored;
- no token/settings residue remains;
- App Service is back in normal mode;
- `LASTEXITCODE=0` is not valid success evidence for this PowerShell terminating exception path;
- no qualification acceptance credit is earned from this failed attempt.

## 3. Reconciliation purpose

Determine whether HTTP `503` should be treated as a bounded transient readiness condition for the qualification endpoint, and if so define the smallest safe helper-only correction.

The correction must preserve:

- Windows PowerShell 5.1 compatibility;
- 5-second polling interval;
- 180-second maximum polling window;
- exact RunId attribution;
- capability-token secrecy;
- deterministic terminal failures;
- temporary-setting restoration;
- application-owned evidence source;
- no direct SQLite access;
- no Kudu VFS dependency.

## 4. Candidate decision options

Evaluate:

### H1 — Add `503` as retryable transient status

Treat:

```text
404
503
```

as retryable during the bounded polling window.

Record only sanitized observability such as:

```text
HTTP status
attempt number
```

Never print:

- token;
- authorization header;
- secret app settings;
- full response bodies if they may contain unrelated platform content.

### H2 — Broaden to a generic 5xx retry policy

Retry all 5xx responses.

Reject unless explicitly proven safe; this may mask terminal application failures.

### H3 — Change App Service startup/runtime behavior

Modify application/container/Azure behavior instead of helper polling.

Reject unless H1 is proven unsafe or insufficient.

### H4 — No helper change

Treat 503 as terminal and leave qualification blocked.

Select only if 503 cannot safely be classified as transient startup/readiness behavior.

## 5. Required decision

Select exactly one:

```text
D1 — H1_RETRY_404_AND_503
D2 — H2_RETRY_GENERIC_5XX
D3 — H3_RUNTIME_OR_AZURE_CHANGE
D4 — H4_NO_CHANGE
```

Prefer `D1` if evidence supports 503 as transient App Service startup/unavailability during the bounded qualification window.

## 6. Exact repository scope if D1

Exact allowlist:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Path count:

```text
1
```

Operation count:

```text
1 MODIFY
```

No other tracked path is authorized.

## 7. Required retry contract if D1

The helper must:

```text
poll every 5 seconds
stop after 180 seconds
retry 404
retry 503
treat exact valid 200 payload as success
treat wrong-run/malformed/invalid-token/application-failure responses as terminal
```

Do not retry arbitrary status codes.

Do not convert authentication/authorization failures into transient conditions.

At minimum, these should remain terminal unless the existing helper already defines stricter behavior:

```text
400
401
403
409 if currently terminal
5xx other than 503
```

Luna must preserve the already-governed endpoint semantics and not silently broaden policy.

## 8. Sanitized polling telemetry

If D1 is selected, authorize sanitized console output such as:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=<STATUS>
```

or an equivalently narrow format.

Do not output:

- URL query secrets if any;
- token;
- headers;
- response body unless already sanitized;
- Azure publishing credentials.

The goal is diagnostic attribution, not verbose logging.

## 9. PowerShell error semantics

The helper must continue to fail by terminating exception/non-success control flow when qualification cannot be proven.

Do not use `$LASTEXITCODE` as the authoritative helper-success mechanism in Windows PowerShell 5.1.

For future operator evidence, define success based on:

- helper completing without terminating exception;
- required PASS markers/output;
- accepted evidence record.

If a wrapper needs a process exit code, it must invoke PowerShell in a way that explicitly maps terminating exceptions to nonzero process exit status.

This reconciliation does not require such a wrapper unless already present.

## 10. Required validation contract if D1

Define a Terra one-path remediation validation suite covering at minimum:

- Windows PowerShell 5.1 AST parse: 0 errors;
- synthetic `404 -> 404 -> 200` succeeds;
- synthetic `503 -> 503 -> 200` succeeds;
- synthetic mixed `404 -> 503 -> 200` succeeds;
- `401` terminal;
- `403` terminal;
- malformed `200` terminal;
- wrong RunId terminal;
- timeout after bounded attempts fails;
- temporary-setting restoration still executes on terminal failure;
- sanitized status/attempt output present;
- token never printed;
- direct SQLite scan: 0;
- active Kudu-VFS scan: 0;
- Gitleaks: pass;
- `git diff --check`: pass;
- exact diff: one authorized path.

Preserve existing RNG compatibility coverage under Windows PowerShell 5.1.

## 11. Source/image finality

If D1 requires a tracked helper change:

```text
NEW SOURCE COMMIT REQUIRED: YES
```

Because the helper remains under `eng/`, and Luna previously established that `eng/` is excluded from the image:

```text
NEW IMAGE REQUIRED: NO
```

unless new repository evidence contradicts that prior finding.

The runtime image digest remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Current source commit finality becomes:

```text
SUPERSEDED_AFTER_REMEDIATION
```

if D1/D2 requires a tracked helper correction.

## 12. Azure state preservation

No Azure mutation is authorized under this Luna authority.

Preserve:

```text
SCM basic auth: false
FTP basic auth: false
runtime image digest unchanged
no temporary D3 settings
no qualification restart/redeploy
```

The failed initialize RunId:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
```

must not be reused.

The next governed retry must use a fresh initialize RunId and fresh helper-generated token.

## 13. Next Terra authority if D1

If `D1` is selected, the next authority should be a **one-path local helper remediation authority** only.

It may authorize:

```text
working-tree modification of the helper
Windows PowerShell 5.1 validation
synthetic retry-policy tests
security/static validation
```

It must not yet authorize:

```text
staging
commit
push
Docker/GHCR
Azure
D3 settings
restart/redeploy
PR
lifecycle
```

After local success, a separate one-path source publication authority is required, followed by another Azure qualification resume authority.

## 14. Mutation prohibition

This Luna authority is read-only/planning only.

Required zero mutations:

```text
Repository: 0
Git: 0
Docker/GHCR: 0
Azure: 0
Provider: 0
PR: 0
Issue: 0
Project #2: 0
Milestone: 0
```

## 15. Required output

Return:

- classification of HTTP 503;
- H1–H4 comparison;
- exact D1–D4 decision;
- exact retryable-status set;
- exact terminal-status policy;
- sanitized telemetry contract;
- one-path allowlist/count if applicable;
- Windows PowerShell 5.1 validation contract;
- source/image finality decision;
- failed initialize RunId disposition;
- Azure-state preservation decision;
- next Terra scope;
- zero-mutation confirmation.

## 16. Terminal markers

Required:

`RELEASE 1.12 WP04 — HTTP 503 RETRY RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — HTTP 503 RETRY DECISION: <D1|D2|D3|D4>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: <EXACT_SET>`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 COMPATIBILITY: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — RETRY REMEDIATION PATH COUNT: <N>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT SOURCE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — HTTP 503 RETRY RECONCILIATION MUTATION AUDIT: PASS`

If executable:

`RELEASE 1.12 WP04 — TERRA HTTP 503 RETRY REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA HTTP 503 RETRY RECONCILIATION COMPLETE`

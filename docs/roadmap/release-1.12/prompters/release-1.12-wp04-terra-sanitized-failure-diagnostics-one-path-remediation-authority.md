# GPT-5.6 Terra — Release 1.12 WP04 Sanitized Failure Diagnostics One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path sanitized failure-classification correction.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
23eaca6b6dd6b8593b5a70498423b2fb742a6b6c
```

Current source finality:

```text
SUPERSEDED_AFTER_REMEDIATION
```

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Luna decision:

```text
R2 — O2_ONE_PATH_SANITIZED_FAILURE_CLASSIFICATION
```

Current retry policy remains unchanged:

```text
retryable HTTP statuses = {404,503}
transport exceptions = terminal pending classification
```

Forbidden failed RunIds:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
```

Azure state must remain unchanged under this authority.

## 2. Exact implementation allowlist

Exactly one tracked path may be modified:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required path count:

```text
1
```

No other tracked path is authorized.

Preserve unrelated untracked files untouched.

## 3. Required diagnostic behavior

Add sanitized failure classification so that every evidence-poll outcome can be attributed without exposing secret-bearing details.

For HTTP responses, emit only:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=<HTTP_STATUS>
```

For transport exceptions with no usable HTTP response, emit:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=NONE
WP04_HTTP_EVIDENCE_FAILURE_CLASS=<SANITIZED_CLASS>
```

For terminal semantic failures after HTTP 200, emit:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=200
WP04_HTTP_EVIDENCE_FAILURE_CLASS=<SANITIZED_SEMANTIC_CLASS>
```

Allowed semantic classes should be narrow and stable, such as:

```text
MalformedPayload
WrongRunId
InvalidEvidenceRecord
```

Equivalent names are allowed if equally specific and non-secret.

## 4. Transport-class sanitization

When Windows PowerShell 5.1 / .NET Framework exposes a `System.Net.WebExceptionStatus`, map it to a sanitized class only.

Potential allowed classes include:

```text
NameResolutionFailure
ConnectFailure
ConnectionClosed
KeepAliveFailure
PipelineFailure
ProxyNameResolutionFailure
ReceiveFailure
RequestCanceled
SecureChannelFailure
SendFailure
Timeout
TrustFailure
UnknownError
ProtocolError
```

If an exception cannot be safely classified, use:

```text
UnknownError
```

Do not emit raw exception messages.

## 5. Secret-hygiene boundary

Strictly prohibited output:

```text
evidence token
authorization header
request headers
secret app-setting values
request URI if it can contain sensitive values
response body
raw exception message
stack trace
publishing credentials
registry credentials
```

Sanitized class/status/attempt only.

## 6. Retry policy must not change

Preserve exactly:

```text
retry 404
retry 503
poll every 5 seconds
maximum 180 seconds
```

Everything else remains terminal.

Do not add retries for:

```text
400
401
403
409
500
502
other 5xx
transport Timeout
ConnectFailure
NameResolutionFailure
ConnectionClosed
other transport exceptions
malformed payload
wrong RunId
invalid evidence record
```

This remediation is diagnostic only.

## 7. Windows PowerShell 5.1 requirement

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Do not use PowerShell 7-only syntax, cmdlets, or .NET APIs unavailable to Windows PowerShell 5.1.

Preserve the approved RNG implementation:

```text
RandomNumberGenerator.Create()
GetBytes()
Dispose()
```

## 8. Required local validation

Run locally only.

### Parse gate

Require:

```text
Windows PowerShell 5.1 AST errors = 0
```

### HTTP status behavior

Synthetic validation must prove at minimum:

```text
404 -> retry + sanitized status
503 -> retry + sanitized status

400 -> terminal + sanitized status
401 -> terminal + sanitized status
403 -> terminal + sanitized status
409 -> terminal + sanitized status
500 -> terminal + sanitized status
502 -> terminal + sanitized status
```

### Transport behavior

Synthetic or equivalent Windows PowerShell 5.1 validation must prove:

```text
Timeout -> terminal + status NONE + sanitized failure class
NameResolutionFailure -> terminal + status NONE + sanitized failure class
ConnectFailure -> terminal + status NONE + sanitized failure class
ConnectionClosed -> terminal + status NONE + sanitized failure class
Unknown/unmapped transport failure -> terminal + status NONE + UnknownError
```

### Semantic behavior

Prove:

```text
malformed HTTP 200 -> terminal + sanitized semantic class
wrong RunId -> terminal + sanitized semantic class
invalid evidence record -> terminal + sanitized semantic class
```

### Restoration

Prove final restoration logic remains intact on terminal failure.

Do not use Azure for this validation.

### Security/static gates

Require:

```text
token disclosure = 0
authorization-header disclosure = 0
request-URI disclosure = 0
response-body disclosure = 0
raw exception-message disclosure = 0
stack-trace disclosure = 0
generic 5xx retry logic = absent
transport retry logic = absent
direct SQLite evidence access = 0
active Kudu-VFS evidence retrieval = 0
Gitleaks = pass
git diff --check = pass
```

### RNG regression

Require:

```text
RandomNumberGenerator.Fill active references = 0
RandomNumberGenerator.Create present
GetBytes present
```

## 9. Exact diff gate

Before completion require:

```text
tracked changed paths = exactly 1
changed path =
  eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
staged paths = 0
unauthorized tracked paths = 0
```

Do not stage anything.

## 10. Explicitly forbidden mutations

This authority does **not** authorize:

```text
git add
git commit
git push
Docker build
Docker image publication
GHCR mutation
Azure mutation
App Service settings mutation
initialize qualification
restart qualification
redeploy qualification
PR creation
PR merge
issue mutation
Project #2 mutation
milestone mutation
tag/release mutation
```

## 11. Source/image implications

After successful local remediation:

```text
new source commit required later = YES
new image required = NO
```

Reason:

```text
only eng/ helper changes
eng/ remains excluded from Docker image
runtime image remains unchanged
```

Runtime digest remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

## 12. Azure preservation

Require zero Azure mutation.

Preserve:

```text
SCM basic auth = false
FTP basic auth = false
runtime image digest unchanged
registry credentials absent
temporary D3 settings absent
temporary evidence token absent
normal runtime restored
```

Do not rerun initialize.

## 13. Stop conditions

STOP if:

- more than one tracked path needs modification;
- retry policy must broaden;
- transport exceptions would need to become retryable;
- secret-safe classification cannot be achieved;
- Windows PowerShell 5.1 compatibility fails;
- restoration semantics regress;
- runtime/container/Azure changes appear necessary;
- Docker/image rebuild appears necessary;
- direct SQLite/Kudu evidence would be required.

Do not widen scope.

## 14. Mutation audit

Expected mutation:

```text
Repository working tree:
  exactly 1 tracked path modified
```

Required zero:

```text
staging: 0
commits: 0
pushes: 0
Docker: 0
GHCR: 0
Azure: 0
PR: 0
issue: 0
Project #2: 0
milestone: 0
tags/releases: 0
```

## 15. Required return evidence

Return:

- exact modified path;
- concise implementation summary;
- Windows PowerShell 5.1 AST result;
- HTTP-status validation matrix;
- transport-class validation matrix;
- semantic-failure validation;
- exact retry policy proof;
- secret-hygiene proof;
- restoration-path proof;
- RNG regression proof;
- direct SQLite scan;
- active Kudu-VFS scan;
- Gitleaks result;
- `git diff --check`;
- tracked/staged path counts;
- exact mutation accounting.

## 16. Required terminal markers

Full success requires:

`RELEASE 1.12 WP04 — SANITIZED FAILURE DIAGNOSTICS ONE-PATH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — TRANSPORT EXCEPTION RETRY POLICY: TERMINAL_PENDING_CLASSIFICATION`

`RELEASE 1.12 WP04 — SANITIZED HTTP FAILURE CLASSIFICATION: PASS`

`RELEASE 1.12 WP04 — SANITIZED TRANSPORT FAILURE CLASSIFICATION: PASS`

`RELEASE 1.12 WP04 — SANITIZED SEMANTIC FAILURE CLASSIFICATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FAILURE DIAGNOSTICS COMPATIBILITY: PASS`

`RELEASE 1.12 WP04 — SECRET-HYGIENE DIAGNOSTIC BOUNDARY: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE RESTORATION PATH: PASS`

`RELEASE 1.12 WP04 — RNG COMPATIBILITY REGRESSION: PASS`

`RELEASE 1.12 WP04 — FAILURE DIAGNOSTICS PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — FAILURE DIAGNOSTICS MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA SANITIZED FAILURE DIAGNOSTICS REMEDIATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA SANITIZED FAILURE DIAGNOSTICS PUBLICATION AUTHORITY: READY`

Blocked:

`RELEASE 1.12 WP04 — SANITIZED FAILURE DIAGNOSTICS ONE-PATH REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

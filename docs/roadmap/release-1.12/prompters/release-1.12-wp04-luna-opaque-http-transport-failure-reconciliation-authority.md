# GPT-5.6 Luna — Release 1.12 WP04 Opaque HTTP/Transport Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: classify the new opaque HTTP/transport failure and define the narrowest safe diagnostic/remediation boundary.
- **GPT-5.6 Terra** — implementation, validation, publication, and resumed Azure qualification only under later explicit authority.
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

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Binding operator shell:

```text
Windows PowerShell 5.1.26100.9444
```

Current retry contract:

```text
retryable: {404,503}
terminal: 400,401,403,409,other 5xx,malformed 200,wrong RunId,explicit application failure,timeout
poll interval: 5 seconds
maximum window: 180 seconds
```

## 2. Failed governed initialize attempt

Fresh failed RunId:

```text
initialize-3dab96f04cb84902b64df269ebae3459
```

Accepted facts:

```text
qualification settings applied: yes
qualification settings restored: yes
temporary qualification-setting count after failure: 0
token disclosed: no
evidence record returned: no
App Service recovered to Running/Normal: yes
404 telemetry observed: no
503 telemetry observed: no
restart/redeploy continuity attempted: no
PR created: no
```

Disposition:

```text
NOT ACCEPTED
FORBIDDEN TO REUSE
```

This run earns no qualification acceptance credit.

## 3. Key interpretation boundary

Do **not** conclude that the `{404,503}` remediation failed.

The absence of a `404` or `503` telemetry record proves only that the helper did not classify the failing observation through either governed retry branch.

The failure may be:

- another terminal HTTP status;
- a Windows PowerShell / .NET transport exception;
- DNS/connect/TLS/socket/connection-reset behavior;
- an HTTP exception path where status extraction is incomplete;
- another deterministic helper terminal condition.

No specific cause may be asserted without evidence.

## 4. Reconciliation purpose

Determine whether the current helper provides enough sanitized diagnostics to classify non-`404`/`503` failures under Windows PowerShell 5.1.

If not, define the smallest helper-only observability correction that:

- reveals sanitized failure class/status;
- preserves secret hygiene;
- does not broaden retry policy;
- does not change Azure/runtime architecture;
- preserves final restoration;
- preserves exact RunId semantics;
- remains Windows PowerShell 5.1-compatible.

## 5. Candidate decisions

Evaluate exactly:

### O1 — Existing diagnostics are sufficient

Use existing committed/helper output or already-available non-mutating Azure evidence to classify the failure without repository change.

Select only if the exact terminal status/transport class can be proven.

### O2 — One-path sanitized terminal/transport diagnostics

Modify only the helper so every evidence-poll failure emits a narrow sanitized classification before terminal handling.

Examples of acceptable diagnostic fields:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=<HTTP_STATUS_OR_NONE>
WP04_HTTP_EVIDENCE_FAILURE_CLASS=<SANITIZED_CLASS>
```

For transport exceptions, acceptable class values may include only non-secret categories such as:

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

The exact classification mechanism must be compatible with Windows PowerShell 5.1 / .NET Framework.

### O3 — Broaden retry policy now

Retry additional statuses or transport exceptions before identifying the actual failure.

Reject unless direct evidence proves the specific condition is safely transient.

### O4 — Runtime/Azure architecture change

Change app/container/Azure behavior.

Reject unless O1/O2 cannot provide safe classification.

## 6. Required decision

Select exactly one:

```text
R1 — O1_EXISTING_DIAGNOSTICS_SUFFICIENT
R2 — O2_ONE_PATH_SANITIZED_FAILURE_CLASSIFICATION
R3 — O3_BROADEN_RETRY_POLICY
R4 — O4_RUNTIME_OR_AZURE_CHANGE
```

Prefer `R2` if the exact failure remains opaque.

## 7. Exact repository scope if R2

Allowlist:

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

## 8. Diagnostic contract if R2

The helper must emit sanitized diagnostics for **all** evidence-poll outcomes:

For HTTP responses:

```text
attempt number
HTTP status
```

For transport exceptions with no usable HTTP response:

```text
attempt number
status = NONE
failure class = sanitized transport/WebException category
```

For terminal payload validation failures:

```text
attempt number
status = 200
failure class = sanitized semantic category
```

Example semantic categories:

```text
MalformedPayload
WrongRunId
InvalidEvidenceRecord
```

Do not emit:

- exception message if it could include URLs/secrets;
- request URI if query data could contain sensitive values;
- headers;
- token;
- response body;
- stack trace containing secret-bearing values;
- publishing credentials.

## 9. Retry policy preservation

This reconciliation must **not** widen the retry set.

Remain:

```text
retryable HTTP statuses = {404,503}
```

Everything else remains terminal until separately governed.

Transport exceptions remain terminal under this authority unless a later Luna reconciliation explicitly classifies a specific condition as safely retryable.

## 10. Windows PowerShell 5.1 compatibility

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Any implementation decision must use APIs available under the Windows PowerShell 5.1 / .NET Framework environment.

Do not use PowerShell 7-only behavior or APIs.

## 11. Validation contract if R2

A later Terra remediation authority must prove locally/synthetically:

```text
Windows PowerShell 5.1 AST errors: 0

404 -> retry
503 -> retry

400 -> terminal + sanitized status
401 -> terminal + sanitized status
403 -> terminal + sanitized status
409 -> terminal + sanitized status
500 -> terminal + sanitized status
502 -> terminal + sanitized status

transport timeout -> terminal + status NONE + sanitized class
name resolution failure -> terminal + status NONE + sanitized class
connection failure/closed -> terminal + status NONE + sanitized class

malformed 200 -> terminal + sanitized semantic class
wrong RunId -> terminal + sanitized semantic class

token disclosure: 0
authorization-header disclosure: 0
unsanitized response-body disclosure: 0

restoration path preserved
RNG compatibility preserved
direct SQLite scan: 0
active Kudu-VFS scan: 0
Gitleaks: pass
git diff --check: pass
tracked changed paths: exactly 1
staged paths: 0
```

Synthetic equivalents are acceptable if they exercise the same Windows PowerShell 5.1 exception/status handling path.

## 12. Source/image implications

If `R2` is selected:

```text
NEW SOURCE COMMIT REQUIRED: YES
NEW IMAGE REQUIRED: NO
```

Reason:

```text
only eng/ helper changes
eng/ remains excluded from Docker image
```

Current source finality becomes:

```text
SUPERSEDED_AFTER_REMEDIATION
```

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

## 13. Azure state preservation

No Azure mutation is authorized under this Luna authority.

Preserve:

```text
SCM basic auth: false
FTP basic auth: false
runtime image digest unchanged
registry credentials absent
temporary D3 settings absent
temporary evidence token absent
normal runtime restored
```

Do not rerun initialize yet.

Do not perform restart/redeploy continuity.

## 14. Failed RunId disposition

The following failed RunIds remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
```

The next governed initialize retry must use a fresh RunId.

## 15. Mutation prohibition

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

## 16. Required output

Return:

- whether the failure is currently classifiable;
- O1–O4 comparison;
- exact R1–R4 decision;
- exact diagnostic fields/categories;
- exact retry policy preservation statement;
- one-path allowlist/count if applicable;
- Windows PowerShell 5.1 compatibility statement;
- source/image finality decision;
- failed RunId disposition;
- Azure-state preservation decision;
- next Terra scope;
- zero-mutation confirmation.

## 17. Terminal markers

Required:

`RELEASE 1.12 WP04 — OPAQUE HTTP/TRANSPORT FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — OPAQUE FAILURE DECISION: <R1|R2|R3|R4>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — TRANSPORT EXCEPTION RETRY POLICY: TERMINAL_PENDING_CLASSIFICATION`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 COMPATIBILITY: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — DIAGNOSTIC REMEDIATION PATH COUNT: <N>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT SOURCE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — OPAQUE FAILURE RECONCILIATION MUTATION AUDIT: PASS`

If executable remediation is selected:

`RELEASE 1.12 WP04 — TERRA SANITIZED FAILURE DIAGNOSTICS REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA OPAQUE HTTP/TRANSPORT FAILURE RECONCILIATION COMPLETE`

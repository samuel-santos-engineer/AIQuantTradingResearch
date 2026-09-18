# GPT-5.6 Terra — Release 1.12 WP04 HTTP 503 Retry One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path HTTP 503 retry correction.
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
D1 — H1_RETRY_404_AND_503
```

Exact retryable status set:

```text
{404,503}
```

Rejected:

```text
generic 5xx retry policy
runtime/container/Azure redesign
no-change
```

Failed initialize RunId:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
```

Disposition:

```text
FORBIDDEN TO REUSE
```

Azure state remains good and must not be mutated under this authority.

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

## 3. Required behavior change

Change the HTTP evidence polling behavior so that only these statuses are retryable:

```text
404
503
```

Preserve:

```text
poll interval: 5 seconds
maximum polling window: 180 seconds
exact RunId matching
token secrecy
temporary-setting restoration
application-owned HTTP evidence source
Windows PowerShell 5.1 compatibility
```

Do **not** broaden to arbitrary `5xx`.

## 4. Terminal conditions

These conditions must remain terminal:

```text
400
401
403
409
other 5xx
malformed 200 payload
wrong RunId
explicit application failure
timeout after bounded polling window
```

Do not silently reinterpret authentication/authorization failures as startup transients.

## 5. Sanitized retry diagnostics

Add narrow, non-secret polling diagnostics sufficient to explain transient startup behavior.

Preferred format:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT=<N>
WP04_HTTP_EVIDENCE_STATUS=<STATUS>
```

Equivalent wording is allowed if equally narrow.

Never print:

```text
evidence token
authorization header
secret app-setting values
publishing credentials
unsanitized response content
```

## 6. Windows PowerShell 5.1 requirement

Binding shell/runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Do not use PowerShell 7-only language features, cmdlets, or .NET APIs unavailable to Windows PowerShell 5.1.

The previously approved RNG implementation using:

```text
RandomNumberGenerator.Create()
GetBytes()
Dispose()
```

must remain intact.

## 7. Required local validation

Run all validation locally only.

### Parse/runtime compatibility

Require:

```text
Windows PowerShell 5.1 AST errors: 0
```

### Synthetic retry-policy cases

At minimum prove:

```text
404 -> 404 -> 200 : success
503 -> 503 -> 200 : success
404 -> 503 -> 200 : success
401 : terminal
403 : terminal
400 : terminal
409 : terminal
500 : terminal
502 : terminal
malformed 200 : terminal
wrong RunId : terminal
bounded timeout : terminal failure
```

If the existing synthetic harness covers equivalent cases, extend/reuse it rather than creating unrelated infrastructure.

### Restoration behavior

Prove that terminal retrieval failure still preserves/fires the helper's final restoration logic.

No Azure mutation is allowed to prove this; use local/synthetic validation.

### Security/static gates

Require:

```text
retryable status set is exactly {404,503}
generic 5xx retry logic: absent
token disclosure: absent
direct SQLite evidence access: 0
active Kudu-VFS evidence retrieval: 0
Gitleaks: pass
git diff --check: pass
```

### RNG regression gate

Re-run or preserve evidence that:

```text
RandomNumberGenerator.Fill active reference: 0
RandomNumberGenerator.Create compatible implementation: present
32-byte token generation contract: preserved
```

## 8. Exact diff gate

Before completion require:

```text
tracked changed paths: exactly 1
changed path:
  eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
staged paths: 0
unauthorized tracked paths: 0
```

Do not stage anything.

## 9. Explicitly forbidden mutations

This authority does **not** authorize:

```text
git add
git commit
git push
Docker build
Docker image tag/publication
GHCR mutation
Azure mutation
App Service settings change
restart
redeploy
D3 qualification
PR creation
PR merge
issue mutation
Project #2 mutation
milestone mutation
release/tag mutation
```

## 10. Source/image implications

This local implementation authority does not publish a source commit.

Expected after successful local remediation:

```text
new source commit required later: YES
new image required: NO
```

Reason:

```text
only eng/ helper changes
eng/ remains excluded from Docker image
runtime image digest remains unchanged
```

## 11. Azure preservation

Require zero Azure mutation.

Preserve:

```text
SCM basic auth: false
FTP basic auth: false
runtime image:
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
temporary D3 settings: absent
temporary token: absent
```

Do not reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
```

## 12. Stop conditions

STOP if:

- more than one tracked path needs modification;
- helper requires runtime/container/Azure changes;
- retryable set would exceed `{404,503}`;
- token secrecy cannot be preserved;
- Windows PowerShell 5.1 validation fails;
- existing restoration semantics regress;
- direct SQLite or Kudu VFS evidence would be required;
- Docker/image rebuild appears necessary.

Do not widen scope.

## 13. Required mutation audit

Expected actual mutations:

```text
Repository working tree:
  1 tracked path modified

All others:
  0
```

Required zeros:

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

## 14. Required return evidence

Return:

- exact modified path;
- concise code-change description;
- Windows PowerShell 5.1 AST result;
- synthetic retry matrix result;
- proof retryable set is exactly `{404,503}`;
- proof other listed statuses remain terminal;
- sanitized polling telemetry proof;
- restoration-path proof;
- RNG regression proof;
- direct SQLite scan;
- active Kudu-VFS scan;
- Gitleaks result;
- `git diff --check`;
- tracked/staged path counts;
- exact mutation accounting.

## 15. Required terminal markers

Full success requires:

`RELEASE 1.12 WP04 — HTTP 503 ONE-PATH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — HTTP TERMINAL STATUS POLICY PRESERVATION: PASS`

`RELEASE 1.12 WP04 — SANITIZED HTTP POLLING TELEMETRY: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HTTP RETRY COMPATIBILITY: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE RESTORATION PATH: PASS`

`RELEASE 1.12 WP04 — RNG COMPATIBILITY REGRESSION: PASS`

`RELEASE 1.12 WP04 — HTTP 503 REMEDIATION PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — HTTP 503 REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA HTTP 503 RETRY REMEDIATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA HTTP 503 REMEDIATION PUBLICATION AUTHORITY: READY`

Blocked:

`RELEASE 1.12 WP04 — HTTP 503 ONE-PATH REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

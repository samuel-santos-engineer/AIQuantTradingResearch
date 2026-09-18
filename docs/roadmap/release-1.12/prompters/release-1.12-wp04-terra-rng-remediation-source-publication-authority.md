# GPT-5.6 Terra — Release 1.12 WP04 RNG Remediation Source Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the validated one-path RNG compatibility source correction.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Accepted reconciliation:

```text
P1 — C1_ONE_PATH_RNG_COMPATIBILITY_CORRECTION
```

Previous source candidate:

```text
9d08c6c09192b63b955f86feb3aba136657df3e6
```

Parent of that candidate:

```text
2add79d2063292687d9813f9022206e84b664627
```

Runtime image remains authoritative and unchanged:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

No new image is required because:

```text
eng/ is excluded by .dockerignore
Dockerfile copies only src/, python/, and container/
```

Validated remediation path:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Accepted local validation:

```text
Windows PowerShell AST: 0 errors
interactive RNG: 32 bytes
encoded token: non-empty
two generated tokens: different
synthetic helper validation: 8 passed
RandomNumberGenerator.Fill active references: 0
RandomNumberGenerator.Create compatible reference: 1
direct SQLite scan: 0
active Kudu-VFS scan: 0
Gitleaks: pass
git diff --check: pass
tracked changed paths: 1
staged paths: 0
Azure mutations: 0
Docker mutations: 0
GHCR mutations: 0
Git publication mutations: 0
PR/lifecycle mutations: 0
```

Accepted markers:

`RELEASE 1.12 WP04 — RNG ONE-PATH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL RNG COMPATIBILITY: PASS`

`RELEASE 1.12 WP04 — RNG CRYPTOGRAPHIC STRENGTH PRESERVATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE HELPER BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA RNG COMPATIBILITY REMEDIATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA RNG REMEDIATION PUBLICATION AUTHORITY: READY`

## 2. Exact publication payload

Exactly one tracked path may be staged:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required operation:

```text
MODIFY
```

Required staged path count:

```text
1
```

No other tracked path is authorized.

Preserve unrelated untracked files untouched.

## 3. Publication precheck

Before staging, prove:

```text
tracked changed paths: exactly 1
authorized path: exactly the helper above
staged paths: 0
unauthorized tracked paths: 0
```

Re-run:

- Windows PowerShell AST parse;
- actual Windows PowerShell token-generation compatibility check;
- synthetic helper validation;
- active `RandomNumberGenerator.Fill` scan;
- compatible `RandomNumberGenerator.Create()` scan;
- direct SQLite scan;
- active Kudu-VFS evidence retrieval scan;
- Gitleaks on the helper;
- `git diff --check`.

Do not print a generated token.

If any gate fails, STOP before staging.

Required marker:

`RELEASE 1.12 WP04 — RNG SOURCE PUBLICATION PRECHECK: PASS`

## 4. Stage exact path

Stage exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

After staging require:

```text
staged paths: 1
staged unauthorized paths: 0
unstaged tracked changes: 0
```

## 5. Commit

Create exactly one commit containing only the one-path RNG compatibility correction.

Use a concise WP04-specific commit message identifying Windows PowerShell RNG compatibility.

Capture:

```text
<NEW_SOURCE_COMMIT>
```

Require its parent to be:

```text
9d08c6c09192b63b955f86feb3aba136657df3e6
```

If parent differs unexpectedly, STOP before push.

Required marker:

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE COMMIT: PASS`

## 6. Push

Push the exact new commit non-force to the governed WP04 branch:

```text
release/1.12-wp04-persistent-sqlite
```

Require remote branch tip to equal:

```text
<NEW_SOURCE_COMMIT>
```

No force push.

Required marker:

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUSH: PASS`

## 7. Image preservation

Do not build or publish a Docker image.

The runtime image remains:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Re-prove, read-only, that the changed `eng/` helper is excluded from image contents.

Record the provenance relationship truthfully:

```text
governed source/tooling commit: <NEW_SOURCE_COMMIT>
runtime image payload source: application/container payload from 9d08c6c09192b63b955f86feb3aba136657df3e6
runtime image digest: sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
reason image remains valid: only eng/ operator tooling changed and eng/ is excluded from image
```

Required marker:

`RELEASE 1.12 WP04 — RNG REMEDIATION IMAGE PRESERVATION: PASS`

## 8. Azure state preservation

No Azure mutation is authorized.

Preserve:

```text
SCM basic auth: false
FTP basic auth: false
deployed image:
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
D3 temporary settings: none
qualification restart/redeploy: none
```

Do not contact Azure except read-only verification if strictly needed; prefer no Azure operation.

## 9. No PR/lifecycle mutation

Do not:

- create PR;
- merge PR;
- close #263;
- change Project #2;
- close milestone #63;
- begin WP05.

The Azure qualification must resume first using the newly committed helper.

## 10. Exact mutation accounting

Authorized:

```text
Git staging:
  1 path

Git commit:
  exactly 1

Git push:
  exactly 1 non-force branch update
```

Expected zero:

```text
Docker builds: 0
GHCR publications: 0
Azure mutations: 0
Provider mutations: 0
Git tags: 0
Force pushes: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
```

## 11. Stop conditions

STOP if:

- more than one tracked path is changed;
- parent is not the expected current candidate;
- validation regresses;
- a new image appears necessary;
- push requires force;
- Azure mutation would be needed;
- unrelated untracked files would be touched.

Do not widen scope.

## 12. Required return evidence

Return:

- exact staged path;
- publication precheck results;
- new full source commit SHA;
- exact parent SHA;
- remote branch tip;
- post-push Git status;
- read-only proof that no image rebuild is required;
- preserved runtime image digest;
- exact mutation accounting.

## 13. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — RNG SOURCE PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION IMAGE PRESERVATION: PASS`

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE QUALIFICATION RESUME AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA RNG REMEDIATION SOURCE PUBLICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — RNG REMEDIATION SOURCE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

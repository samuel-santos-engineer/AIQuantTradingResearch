# GPT-5.6 Terra — Release 1.12 WP04 Hard Deadline Timeout Source Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the validated one-path hard-deadline timeout remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Runtime image remains authoritative and unchanged:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Validated remediation path:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Accepted local validation:

```text
Windows PowerShell AST errors = 0
outer evidence-poll budget = 180 seconds max
deadline mechanism = monotonic Stopwatch
request timeout capped by remaining whole seconds
retry sleep capped by remaining milliseconds
accelerated 3-second timing test = pass
observed request cap = 2 seconds
observed retry sleep cap = 2942 ms
retryable HTTP statuses = exactly {404,503}
retryable transport classes = {NONE}
Timeout = terminal
HTTP/transport/semantic terminal policies = preserved
local validation = 8/8 pass
sanitized failure diagnostics = preserved
restoration path = pass
RNG compatibility = pass
Gitleaks = pass
git diff --check = pass
tracked changed paths = exactly 1
staged paths = 0
Azure mutations = 0
```

## 2. Exact publication payload

Stage exactly one tracked path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required staged path count:

```text
1
```

No other tracked path is authorized.

Preserve all unrelated untracked files untouched.

## 3. Publication precheck

Before staging, prove:

```text
HEAD = 31c7fed07d556fbba693c348a338d7d35d5202a4
tracked changed paths = 1
authorized changed path = exact helper above
staged paths = 0
unauthorized tracked paths = 0
```

Re-run the minimum binding validation:

- Windows PowerShell 5.1 AST parse;
- hard-deadline timing validation;
- static proof that retryable HTTP statuses remain exactly `{404,503}`;
- static proof that retryable transport classes remain `{NONE}`;
- proof that `Timeout` remains terminal;
- sanitized diagnostics validation;
- restoration-path validation;
- RNG compatibility checks;
- secret-disclosure scans;
- direct SQLite scan;
- active Kudu-VFS scan;
- Gitleaks;
- `git diff --check`.

If any gate fails, STOP before staging.

Required marker:

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUBLICATION PRECHECK: PASS`

## 4. Stage exact path

Stage exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

After staging require:

```text
staged paths = 1
staged unauthorized paths = 0
unstaged tracked changes = 0
```

## 5. Commit

Create exactly one commit containing only the hard-deadline timing remediation.

Use a concise WP04-specific message identifying the bounded 180-second evidence-poll deadline correction.

Capture:

```text
<NEW_SOURCE_COMMIT>
```

Require parent:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

If parent differs unexpectedly, STOP before push.

Required marker:

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE COMMIT: PASS`

## 6. Push

Push the exact new commit non-force to:

```text
release/1.12-wp04-persistent-sqlite
```

Require remote branch tip to equal:

```text
<NEW_SOURCE_COMMIT>
```

No force push.

Required marker:

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUSH: PASS`

## 7. Runtime image preservation

Do not build or publish a Docker image.

Runtime image remains:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Re-prove read-only that:

```text
eng/ is excluded by .dockerignore
Dockerfile runtime build context/copies do not include eng/
```

Record provenance truthfully:

```text
governed source/tooling commit = <NEW_SOURCE_COMMIT>
runtime image payload source remains prior application/container payload
runtime image digest remains sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
reason = helper-only eng/ change cannot alter image contents
```

Required marker:

`RELEASE 1.12 WP04 — HARD DEADLINE IMAGE PRESERVATION: PASS`

## 8. Azure state preservation

No Azure mutation is authorized.

Preserve:

```text
SCM basic auth = false
FTP basic auth = false
deployed image digest unchanged
temporary D3 settings = absent
temporary evidence token = absent
registry credentials = absent
normal runtime = restored
```

Do not perform initialize/restart/redeploy qualification under this authority.

## 9. Failed initialize disposition

These failed RunIds remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
```

The future resumed initialize qualification must generate a fresh RunId and fresh token.

## 10. No PR/lifecycle mutation

Do not:

- create or merge PR;
- close #263;
- change Project #2 Status;
- close milestone #63;
- begin WP05;
- create release/tag.

Azure qualification must resume first.

## 11. Exact mutation accounting

Authorized:

```text
Git staging:
  exactly 1 path

Git commit:
  exactly 1

Git push:
  exactly 1 non-force branch update
```

Expected zero:

```text
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
Provider mutations = 0
Git tags = 0
Force pushes = 0
PR mutations = 0
Issue mutations = 0
Project #2 mutations = 0
Milestone mutations = 0
```

Count only mutations actually performed.

## 12. Stop conditions

STOP if:

- more than one tracked path is changed;
- Windows PowerShell 5.1 validation regresses;
- hard 180-second bound no longer proves;
- retryable HTTP set differs from exactly `{404,503}`;
- retryable transport set differs from `{NONE}`;
- `Timeout` becomes retryable;
- secret-hygiene boundary regresses;
- parent is not the expected source commit;
- push requires force;
- image rebuild appears necessary;
- Azure mutation would be required;
- unrelated untracked files would be touched.

Do not widen scope.

## 13. Required return evidence

Return:

- exact staged path;
- publication precheck results;
- new full source commit SHA;
- exact parent SHA;
- remote branch tip;
- post-push Git status;
- Windows PowerShell 5.1 compatibility result;
- hard-deadline timing result;
- retryable/terminal policy result;
- image-exclusion proof;
- preserved runtime image digest;
- exact mutation accounting.

## 14. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE IMAGE PRESERVATION: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA AZURE HARD-DEADLINE INITIALIZE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA HARD DEADLINE SOURCE PUBLICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — HARD DEADLINE SOURCE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

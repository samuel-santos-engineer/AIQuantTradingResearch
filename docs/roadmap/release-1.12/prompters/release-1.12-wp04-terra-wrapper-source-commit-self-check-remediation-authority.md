# GPT-5.6 Terra — Release 1.12 WP04 Wrapper Source-Commit Self-Check One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path wrapper source-commit self-check remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Current governed wrapper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current normal front-door observation:

```text
DNS = PASS
TLS 1.3 = PASS
normal root HTTP = 503
NORMAL FRONT-DOOR REACHABILITY = FAIL
```

This `503` is a separate blocker. This authority does not remediate, retry, restart, or investigate it further.

## 2. Proven blocker

The published wrapper contains:

```text
embedded expectedCommit = 047e2ce8e3c614495f8c05cbe01d0b75617d56a7
actual HEAD             = 4822f9847a90a7d86c6bf771603defe9d7abf258
```

The self-check is blocking and exits before Azure mutation.

Selected prior decision:

```text
R1 — SOURCE_COMMIT_SELF_CHECK_REMEDIATION_REQUIRED
```

## 3. Exact mutation allowlist

Exactly one tracked path is authorized:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

No second path is authorized.

Do not stage under this remediation authority.

## 4. Remediation objective

Correct the wrapper's source-commit self-check so that it matches the source state under which the wrapper itself is executed.

The remediation must preserve the purpose of the provenance gate:

```text
qualification wrapper must not proceed when repository source identity does not match the governed expected source identity
```

The immediate required expected commit is:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

## 5. Binding provenance behavior

The wrapper must:

1. obtain the actual local source commit deterministically;
2. require exact equality with the governed expected commit;
3. fail closed before Azure mutation if the local source identity is:
   - missing;
   - malformed;
   - different;
   - unavailable because the Git command fails.

Exact comparison must be deterministic and non-fuzzy.

Do not use substring matching.

## 6. Self-referential publication constraint

This remediation changes the wrapper itself, so after publication the repository HEAD will necessarily move to a new commit.

Therefore the implementation must not create a permanent self-referential deadlock by hard-coding only the pre-remediation commit in a way that immediately becomes stale after publication.

The remediation must adopt one of these Luna-compatible source identity patterns:

### Preferred pattern A — governed source baseline / ancestor proof

Treat:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

as the minimum governed baseline and verify that the current HEAD descends from it, while also proving the wrapper file content is the committed version at HEAD.

### Preferred pattern B — externally supplied exact governed commit

Require the expected commit to be supplied by the invoking authority/wrapper parameter and compare exact equality to `git rev-parse HEAD`, with no silent default to a stale hard-coded value.

### Pattern C — another exact non-self-invalidating mechanism

Only if it preserves the same provenance semantics and can be proven locally.

Do **not** implement:

```text
hard-coded expectedCommit = current pre-publication HEAD
```

if that value will be stale immediately after the remediation is committed.

## 7. Required reconciliation within this implementation

Before editing, inspect the existing provenance intent and choose exactly one implementation pattern:

```text
A — governed baseline ancestry + committed-wrapper proof
B — externally supplied exact governed commit
C — other non-self-invalidating exact mechanism
```

Return the selected pattern and why it preserves the original governance intent.

If the existing calling convention cannot support a non-self-invalidating fix in one path, STOP for Luna re-governance.

## 8. Fail-closed behavior

For provenance failure:

```text
wrapper exit = 1
temporary settings application = 0
restart = 0
qualification helper invocation = 0
Azure mutation count = 0
```

Required fixed safe failure classes should distinguish, as appropriate:

```text
SourceCommitUnavailable
SourceCommitMalformed
SourceCommitMismatch
SourceBaselineNotAncestor
WrapperNotCommittedAtHead
```

Use only classes relevant to the selected pattern.

## 9. Existing image preflight must remain unchanged

Preserve all validated image-preflight behavior:

```text
source = siteConfig.linuxFxVersion
exactly one DOCKER|…@sha256:<64-lowercase-hex>
ordinal exact digest equality
expected digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
blank/null/whitespace/tag-only/malformed/multiple/partial/wrong digest = fail closed
Azure image query failure = fail closed
```

Do not weaken this gate.

## 10. Retry/timing policy preservation

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
per-request timeout = 20 seconds
evidence-poll total wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

Do not add a readiness retry here.

Do not alter timeout values.

## 11. Normal front-door 503 preservation

The currently observed normal root `503` is a separate unresolved blocker.

This authority must not:

```text
restart App Service
change settings
redeploy
change image
enable logging
run initialize
run reopen
attempt to clear the 503
```

Required conclusion after remediation:

```text
NORMAL FRONT-DOOR 503 = UNRESOLVED
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

The next step after source publication will require Luna reconciliation of the `503` / normal-runtime availability boundary before any new initialize attempt.

## 12. Windows PowerShell 5.1 compatibility

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Require:

```text
AST parse errors = 0
```

Do not introduce PowerShell 7-only syntax or unsupported .NET APIs.

## 13. Local validation matrix

Validate locally with stubs/mocks only. No Azure mutation.

At minimum:

### V1 — valid governed source identity

Expected:

```text
provenance gate passes
```

### V2 — Git command failure

Expected:

```text
fail closed
Azure/downstream call count = 0
```

### V3 — missing/blank source identity

Expected:

```text
fail closed
Azure/downstream call count = 0
```

### V4 — malformed source identity

Expected:

```text
fail closed
Azure/downstream call count = 0
```

### V5 — wrong source identity / invalid ancestry

Expected:

```text
fail closed
Azure/downstream call count = 0
```

### V6 — wrapper worktree/content mismatch if selected pattern checks committed content

Expected:

```text
fail closed
Azure/downstream call count = 0
```

### V7 — image preflight exact-match success remains unchanged

Expected:

```text
PASS
```

### V8 — image preflight wrong digest remains fail-closed

Expected:

```text
downstream call count = 0
```

### V9 — provenance failure barrier

For every failed provenance fixture:

```text
settings application calls = 0
restart calls = 0
qualification helper calls = 0
```

### V10 — secret hygiene

Output must contain no:

```text
evidence token
headers
query string
credentials
registry secrets
connection strings
raw exception
stack trace
environment dump
```

## 14. Static/security gates

Require:

```text
Windows PowerShell 5.1 AST errors = 0
local fixture matrix V1–V10 = PASS
git diff --check = PASS
Gitleaks = PASS
secret-output scan = 0 matches
```

## 15. Git/worktree constraints

Before implementation, require:

```text
HEAD = 4822f9847a90a7d86c6bf771603defe9d7abf258
tracked/staged state consistent with prior publication completion
```

At completion under this remediation authority:

```text
tracked modified paths = 1
staged paths = 0
unauthorized tracked modifications = 0
unrelated untracked files preserved
```

Expected sole tracked modification:

```text
M eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Do not stage.

## 16. No runtime image requirement

This path is under:

```text
eng/**
```

and remains excluded from the established Docker runtime context.

Therefore:

```text
new source commit required later = YES
new runtime image required = NO
GHCR publication required = NO
```

Do not build Docker.

## 17. Explicit prohibitions

Do not:

```text
modify any second file
stage
commit
push
build Docker
publish GHCR
mutate Azure
restart/redeploy
enable logging
run initialize
run reopen
create PR
mutate issue
mutate Project #2
mutate milestone
create tag/release
begin WP05
```

## 18. Stop conditions

STOP if:

- one-path remediation cannot avoid becoming stale immediately after its own publication;
- provenance semantics require a second path;
- image-preflight logic would need weakening;
- retry/timing policy would need change;
- Azure mutation is needed for validation;
- Windows PowerShell 5.1 compatibility cannot be preserved.

Return for Luna re-governance instead of widening scope.

## 19. Failed RunId preservation

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No new RunId is authorized.

## 20. Mutation accounting

Authorized:

```text
tracked source modifications = 1
```

Required zero:

```text
staged paths = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
restarts = 0
logging changes = 0
qualification attempts = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

## 21. Required return evidence

Return:

- exact previous self-check logic;
- selected non-self-invalidating provenance pattern A/B/C;
- exact new provenance logic;
- exact failure classes;
- proof the remediation will remain valid after its own publication;
- V1–V10 results;
- PowerShell 5.1 AST result;
- image-preflight preservation result;
- retry/timing preservation result;
- front-door `503` preservation statement;
- Gitleaks result;
- secret-output result;
- tracked/staged state;
- Azure zero-mutation confirmation;
- new image required = NO;
- exact mutation audit.

## 22. Terminal markers

On success:

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK REMEDIATION: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE PATTERN: <A|B|C>`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE FAIL-CLOSED GATE: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE POST-PUBLICATION STABILITY: PASS`

`RELEASE 1.12 WP04 — IMAGE PREFLIGHT CONTRACT: PRESERVED`

`RELEASE 1.12 WP04 — PREFLIGHT FAILURE AZURE MUTATION COUNT: 0`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 WRAPPER VALIDATION: PASS`

`RELEASE 1.12 WP04 — WRAPPER SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — PER-REQUEST TIMEOUT: 20_SECONDS`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503: UNRESOLVED`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: YES`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_STATE`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA WRAPPER SOURCE-COMMIT SELF-CHECK PUBLICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA WRAPPER SOURCE-COMMIT SELF-CHECK REMEDIATION COMPLETE`

On block:

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

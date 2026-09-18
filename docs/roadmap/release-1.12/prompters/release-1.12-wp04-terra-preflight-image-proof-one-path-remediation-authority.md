# GPT-5.6 Terra — Release 1.12 WP04 Fail-Closed Preflight Image-Proof One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path fail-closed image-preflight remediation.
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
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Current deployed instrumented Azure image expected by governance:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Selected Luna reconciliation:

```text
PREFLIGHT IMAGE PROOF CLASSIFICATION = P4
PREFLIGHT FAIL-CLOSED ENFORCEMENT = NOT_PROVEN
TIMEOUT CAUSE = NOT_PROVEN
DECISION = D1
```

Latest failed RunId:

```text
initialize-94c5801cdc0b49e5933760c02313d486
```

It is permanently forbidden for reuse.

## 2. Critical path-state correction

Luna identified the image preflight as existing in an **untracked local wrapper**.

Candidate path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Because the wrapper is reported as untracked, its repository operation is:

```text
CREATE
```

not `MODIFY`.

Before editing, prove:

```text
git ls-files --error-unmatch eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

does not resolve the path as tracked.

Required classification:

```text
PATH_EXISTS_LOCALLY = True
PATH_TRACKED = False
AUTHORIZED_REPOSITORY_OPERATION = CREATE
```

If the path is already tracked, STOP and return a governance-state mismatch rather than silently changing the operation classification.

## 3. Exact authorized path

Exactly one repository candidate path is authorized:

```text
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

No second tracked or candidate path is authorized.

The file may already physically exist as an untracked local wrapper; this authority permits editing that local file so that it becomes the sole future tracked publication candidate.

Do not stage it under this authority.

Preserve all other unrelated untracked files.

## 4. Remediation objective

Make the qualification wrapper fail closed before **any Azure mutation** unless the currently configured App Service image identity is:

1. present;
2. immutable by digest;
3. exactly equal to:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

A blank, malformed, tag-only, or mismatched image identity must terminate locally before:

```text
temporary settings application
App Service restart
qualification helper invocation that mutates Azure
```

## 5. Binding image-identity source

The previous wrapper used:

```powershell
az webapp config container show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query 'dockerCustomImageName' `
  --output tsv
```

That command/property shape is not sufficient because it can return blank with exit code `0`.

The remediation must use a read-only Azure CLI/API surface that reliably returns the actual configured Linux container identity for this App Service.

Preferred canonical property:

```text
siteConfig.linuxFxVersion
```

Expected configured value shape:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

The wrapper may obtain that value using an existing supported read-only Azure CLI command, for example a query against `az webapp show` or another already-proven read-only surface.

Do not depend on `dockerCustomImageName` if it is absent on this target.

## 6. Required normalization contract

Normalize only the expected fixed prefix if present:

```text
DOCKER|
```

After normalization, require the exact image reference to contain:

```text
@sha256:
```

Extract the digest portion and require exact equality with:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Do not accept:

```text
tag-only references
partial digest matches
prefix-only matches
regex substring matches
case-insensitive fuzzy matches
blank strings
null
whitespace-only values
multiple values
```

Use deterministic exact comparison after normalization.

## 7. Fail-closed preflight contract

Before any mutation, emit safe preflight telemetry:

```text
WP04_PREFLIGHT_IMAGE_IDENTITY=<safe configured image identity or NONE>
WP04_PREFLIGHT_IMAGE_DIGEST=<exact digest or NONE>
WP04_PREFLIGHT_IMAGE_EXPECTED_DIGEST=sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
WP04_PREFLIGHT_IMAGE_MATCH=<True|False>
```

Then enforce:

### Success

```text
identity present
immutable digest present
exact digest match = True
```

Only then may the wrapper proceed to later qualification steps.

### Failure

For blank, malformed, tag-only, or mismatch:

```text
WP04_PREFLIGHT_IMAGE_MATCH=False
wrapper exit = 1
Azure mutation count = 0
qualification helper invocation = 0
restart = 0
temporary settings application = 0
```

## 8. Fixed safe preflight failure classes

Use fixed non-secret classes.

At minimum:

```text
ImageIdentityMissing
ImageIdentityMalformed
ImageDigestMissing
ImageDigestMismatch
AzureImageQueryFailure
```

If the wrapper already has a terminal telemetry convention, use it consistently.

Do not print raw Azure error bodies or credentials.

## 9. Azure CLI failure handling

Read-only image-query execution must be validated using both:

```text
captured output
$LASTEXITCODE
```

Required:

```text
$LASTEXITCODE -ne 0 => fail closed
blank output with exit 0 => fail closed
multiple/unexpected values => fail closed
```

Do not treat CLI process success as proof of semantic image identity.

## 10. Windows PowerShell 5.1 compatibility

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Do not introduce:

```text
PowerShell 7-only syntax
ternary operators
null-coalescing operators
PS7-only cmdlets
unsupported modern .NET APIs
```

Require AST parse errors:

```text
0
```

## 11. Local validation matrix

Perform local validation without mutating Azure.

Mock/stub the read-only image-query result and downstream mutation/helper calls.

At minimum prove:

### V1 — Exact configured digest

Input:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Expected:

```text
MATCH=True
preflight passes
downstream path may be invoked by fixture
```

### V2 — Blank output, exit 0

Expected:

```text
MATCH=False
class=ImageIdentityMissing
exit=1
downstream mutation/helper call count=0
```

### V3 — Query process failure

Expected:

```text
class=AzureImageQueryFailure
exit=1
downstream call count=0
```

### V4 — Tag-only image

Expected:

```text
class=ImageDigestMissing or ImageIdentityMalformed
exit=1
downstream call count=0
```

### V5 — Wrong digest

Expected:

```text
class=ImageDigestMismatch
exit=1
downstream call count=0
```

### V6 — Partial/substring digest

Expected:

```text
FAIL
exact equality required
```

### V7 — Whitespace/null

Expected:

```text
FAIL
```

### V8 — Multiple/unexpected values

Expected:

```text
FAIL
```

### V9 — Exact mutation barrier

For every failing fixture prove:

```text
settings application calls = 0
restart calls = 0
qualification helper calls = 0
```

### V10 — Safe output

Prove no:

```text
evidence token
headers
query string
credentials
raw exception
stack trace
environment dump
```

## 12. Timeout/retry policy preservation

Do not alter the committed qualification helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
evidence-poll outer wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

The current timeout cause remains:

```text
NOT_PROVEN
```

This authority does not investigate or remediate it.

## 13. No runtime image requirement

The wrapper path is under:

```text
eng/**
```

and does not enter the established Docker runtime context.

Therefore:

```text
new source commit required later = YES
new runtime image required = NO
current deployed instrumented image remains valid
```

Do not build Docker.

Do not publish GHCR.

## 14. Azure preservation

No Azure mutation is authorized.

Do not:

```text
apply settings
restart
redeploy
change image
enable logging
change SCM/FTP
change registry credentials
run initialize
run reopen
```

A read-only Azure query is not required for implementation validation; prefer stubs/mocks locally.

If a live read-only query is used solely to confirm command shape, it must perform zero mutation and must not invoke the qualification helper.

## 15. Git/worktree gate

At completion, because this path begins untracked, require:

```text
tracked modified paths = 0
staged paths = 0
authorized new untracked candidate path = 1
unauthorized new/modified candidate paths = 0
```

Expected candidate:

```text
?? eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Do not stage.

Do not delete or alter unrelated untracked files.

## 16. Static/security gates

Require:

```text
Windows PowerShell 5.1 AST parse errors = 0
local fixture matrix = PASS
git diff --check equivalent for candidate content = PASS where applicable
Gitleaks candidate scan = PASS
secret-output scan = 0 matches
```

Because an untracked file is not included by ordinary `git diff --check`, validate its whitespace/content explicitly or with an equivalent temporary/no-index method without staging.

## 17. Explicit prohibitions

Do not:

```text
modify verify-persistent-sqlite-webapp.ps1
modify any tracked source
stage
commit
push
build Docker
publish GHCR
mutate Azure
restart/redeploy
run another initialize
run reopen
enable logging
create PR
mutate issue
mutate Project #2
mutate milestone
create tag/release
begin WP05
```

## 18. Stop conditions

STOP if:

- the candidate wrapper is actually tracked;
- a second path is required;
- exact digest proof cannot be made fail-closed;
- a safe reliable Azure property cannot be identified;
- Windows PowerShell 5.1 compatibility cannot be maintained;
- timeout/retry helper policy must change;
- implementation validation requires Azure mutation;
- the wrapper cannot prevent downstream mutation on preflight failure.

Return for Luna re-governance rather than widening scope.

## 19. Mutation accounting

Authorized local candidate mutation:

```text
1 untracked wrapper path edited/created
```

Required zero:

```text
tracked path modifications = 0
staged paths = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

## 20. Failed RunIds

All failed RunIds remain forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

The malformed historical identifier remains forbidden if encountered.

## 21. Required return evidence

Return:

- candidate path tracking-state proof;
- authorized operation classification (`CREATE`);
- exact read-only Azure property/source selected;
- exact normalization logic;
- exact digest equality logic;
- exact failure classes;
- proof blank output fails closed;
- proof wrong digest fails closed;
- proof downstream mutation/helper calls = 0 for every failed preflight fixture;
- V1–V10 results;
- Windows PowerShell 5.1 AST result;
- Gitleaks result;
- secret-output scan;
- retry/timing preservation statement;
- tracked/staged/untracked candidate counts;
- Azure zero-mutation confirmation;
- new image required = NO;
- exact mutation audit.

## 22. Terminal markers

On success:

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF REMEDIATION: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT WRAPPER REPOSITORY OPERATION: CREATE`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE IDENTITY SOURCE: siteConfig.linuxFxVersion`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE IDENTITY NONEMPTY GATE: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT IMMUTABLE DIGEST GATE: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT EXACT DIGEST EQUALITY GATE: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT FAIL-CLOSED ENFORCEMENT: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT FAILURE AZURE MUTATION COUNT: 0`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PREFLIGHT VALIDATION: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: NOT_PROVEN`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: YES`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA PREFLIGHT IMAGE-PROOF SOURCE PUBLICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA PREFLIGHT IMAGE-PROOF REMEDIATION COMPLETE`

On path-state mismatch:

`RELEASE 1.12 WP04 — PREFLIGHT WRAPPER TRACKING STATE: MISMATCH`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

On any other block:

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Luna — Release 1.12 WP04 Committed Azure Helper RNG Compatibility Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: read-only reconciliation of the committed Azure helper's Windows PowerShell RNG incompatibility and exact remediation boundary.
- **GPT-5.6 Terra** — implementation, validation, exact Git/GHCR publication, Azure qualification, PR/lifecycle mutations only under subsequent explicit authorities.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current candidate source:

```text
9d08c6c09192b63b955f86feb3aba136657df3e6
```

Current candidate image:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Accepted architecture:

```text
E1 — H1_APPLICATION_HTTP_EVIDENCE
R1 — Q1_QUALIFICATION_SINGLE_LISTENER_SUBSTITUTION
```

Azure state already proven under the blocked exact-digest authority:

```text
Plan: F1 / Free
Region: West Central US
SCM basic auth: false
FTP basic auth: false
Configured image: exact digest
  sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
Registry credentials introduced: no
```

Actual mutations already performed:

```text
SCM basic-auth policy update: 1
Exact-digest image-reference update: 1
D3 setting mutations: 0
Qualification restarts: 0
Redeploy qualification mutations: 0
PR mutations: 0
Lifecycle mutations: 0
```

Accepted blocked markers:

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

## 2. Newly discovered blocker

The committed governed Azure helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

uses:

```text
RandomNumberGenerator.Fill
```

The interactive Windows PowerShell runtime used for the governed Azure execution does not support that API.

Therefore:

- qualification cannot safely begin;
- the committed helper cannot generate the governed high-entropy evidence token in that runtime;
- an inline substitute is forbidden because qualification must use the committed governed helper;
- repository mutation is outside the blocked Azure authority;
- no D3 settings/restart/redeploy should occur until reconciliation/remediation is complete.

A compatible RNG pattern was reportedly already proven in an untracked local Docker helper. Treat that only as read-only evidence; do not automatically promote or copy it without verifying its security and runtime compatibility.

## 3. Reconciliation purpose

Determine the smallest repository correction that:

1. preserves cryptographically strong temporary token generation;
2. works in the project's interactive Windows PowerShell execution environment;
3. remains compatible with the helper's existing behavior;
4. changes no HTTP evidence semantics;
5. changes no persistence semantics;
6. changes no runtime/container composition;
7. requires no schema/README change;
8. avoids inline/manual token substitution.

This is a tooling/runtime-compatibility correction, not architecture re-design.

## 4. Required read-only inspection

Inspect:

- the committed Azure helper's exact RNG/token-generation code;
- the untracked local Docker helper's already-working compatibility pattern, if available;
- PowerShell runtime/version actually used by the governed Azure workflow;
- .NET APIs available to that runtime;
- token encoding/length requirements;
- any existing repository conventions for cryptographic random bytes.

Do not mutate any file.

## 5. Security invariant

The replacement must remain cryptographically strong.

Forbidden:

- `Get-Random` as the sole security source;
- timestamp-derived tokens;
- GUID-only tokens unless cryptographic strength is explicitly proven sufficient by repository policy;
- predictable PRNGs;
- hard-coded tokens;
- tokens printed to stdout;
- tokens persisted in evidence;
- tokens committed to source;
- manual inline token injection during Azure qualification.

Preferred shape, if compatible with Windows PowerShell/.NET Framework:

```text
System.Security.Cryptography.RandomNumberGenerator.Create()
GetBytes(byte[])
Dispose()
```

or another cryptographically equivalent API available in the actual runtime.

Luna must determine the exact safe implementation contract rather than assuming this example.

## 6. Remediation options

Evaluate:

### C1 — One-path helper RNG compatibility correction

Modify only:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Replace only the incompatible RNG call with a cryptographically strong API supported by the governed Windows PowerShell runtime.

All helper inputs/outputs, HTTP polling, token secrecy, D3 settings, restoration logic, exact-run validation, and Azure lifecycle behavior remain unchanged.

### C2 — Shared compatibility helper abstraction

Introduce or modify another tracked helper/module to centralize RNG behavior.

Reject unless C1 cannot safely satisfy the runtime/security contract.

### C3 — Change governed execution runtime

Require PowerShell 7+ or another shell/runtime rather than changing the helper.

Reject unless the repository/release already guarantees that runtime. Do not impose a new external prerequisite merely to avoid a one-line/narrow compatibility correction.

### C4 — Caller-supplied token

Allow an external caller to generate/inject the token.

Reject unless no application-owned helper solution is safe; this risks weakening the committed-helper governance boundary.

### C5 — No safe correction

Qualification remains blocked.

## 7. Required decision

Select exactly one:

```text
P1 — C1_ONE_PATH_RNG_COMPATIBILITY_CORRECTION
P2 — C2_SHARED_COMPATIBILITY_ABSTRACTION
P3 — C3_EXECUTION_RUNTIME_CHANGE
P4 — C4_CALLER_SUPPLIED_TOKEN
P5 — C5_NO_SAFE_CORRECTION
```

Prefer `P1` if cryptographic strength and actual Windows PowerShell compatibility are proven.

## 8. Exact repository allowlist if P1

If P1 is selected, exact remediation allowlist must be:

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

The existing eight-path candidate history remains preserved, but a new source commit will be required because this helper is tracked.

## 9. Required compatibility tests if P1

Define the exact Terra validation contract.

At minimum require:

- Windows PowerShell parse: 0 errors;
- token generation executes successfully in the actual governed interactive Windows PowerShell runtime;
- generated token is non-empty;
- expected entropy/byte length is preserved;
- two generated tokens differ;
- token is not printed;
- token is not written into evidence JSON;
- HTTP header construction remains correct;
- helper's synthetic/local validation cases still pass;
- 5-second polling unchanged;
- 180-second maximum unchanged;
- exact RunId validation unchanged;
- temporary-setting restoration unchanged;
- direct SQLite scan: 0;
- active Kudu-VFS evidence retrieval scan: 0;
- Gitleaks: pass;
- `git diff --check`: pass;
- exact diff: one authorized path.

If feasible, also run PowerShell 7 parse/execution as a compatibility check, but Windows PowerShell compatibility is the binding gate.

## 10. Candidate/image finality decision

Determine whether the helper-only tracked correction requires a new container image.

Important distinction:

- source candidate must change because a tracked governed helper changes;
- the helper is deployment/operator tooling and may or may not be copied into the runtime image.

Inspect the Docker build context and Dockerfile to determine whether this helper path affects image contents/digest.

Return exactly:

```text
NEW SOURCE COMMIT REQUIRED: YES
NEW IMAGE REQUIRED: YES|NO
```

Do not assume.

If the helper is copied into the image by broad repository `COPY`, a new exact image is required even if runtime application behavior is unchanged.

If it is provably excluded from the image, Luna may select `NEW IMAGE REQUIRED: NO`, but must define how source/image provenance remains truthful.

## 11. Azure state preservation

No Azure mutation is authorized here.

The already-correct state should remain:

```text
SCM basic auth: false
FTP basic auth: false
Image: sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Do not re-enable SCM basic auth.

Do not apply D3 settings.

Do not restart/redeploy.

## 12. Qualification retry governance

The failed pre-initialize attempt consumed no D3 run and produced no qualification evidence.

Therefore the future retry must:

- use the remediated committed helper;
- generate a fresh initialize RunId;
- generate a fresh high-entropy token through the helper;
- begin again at initialize;
- not claim credit from any pre-remediation attempt.

The existing exact-digest deployment/precheck evidence may be reused only to the extent Luna explicitly allows after source/image finality is decided.

If a new image is required, exact-digest deployment must be repeated for the new image.

## 13. Next Terra authority

If P1 is selected, authorize a **local one-path remediation authority** next.

It may permit:

```text
one tracked helper modification
Windows PowerShell compatibility validation
synthetic/local helper tests
static/security gates
```

It must not yet permit:

```text
staging
commit
push
Docker/GHCR publication
Azure mutation
D3 settings
restart/redeploy
PR/lifecycle
```

After local success, use a separate publication authority.

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

- actual Windows PowerShell runtime compatibility finding;
- committed helper RNG finding;
- untracked helper compatibility finding, if available;
- C1–C5 comparison;
- exact P1–P5 decision;
- exact cryptographic RNG replacement contract;
- exact path allowlist/count;
- compatibility/security validation contract;
- new source commit decision;
- new image decision with Dockerfile/build-context evidence;
- current candidate finality;
- Azure-state preservation decision;
- exact next Terra scope;
- zero-mutation confirmation.

## 16. Terminal markers

Required:

`RELEASE 1.12 WP04 — AZURE HELPER RNG COMPATIBILITY RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — AZURE HELPER RNG COMPATIBILITY DECISION: <P1|P2|P3|P4|P5>`

`RELEASE 1.12 WP04 — CRYPTOGRAPHIC TOKEN STRENGTH: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — RNG REMEDIATION PATH COUNT: <N>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — SCM BASIC AUTH STATE: PRESERVE_FALSE`

`RELEASE 1.12 WP04 — AZURE HELPER RNG COMPATIBILITY RECONCILIATION MUTATION AUDIT: PASS`

If executable:

`RELEASE 1.12 WP04 — TERRA RNG COMPATIBILITY REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA AZURE HELPER RNG COMPATIBILITY RECONCILIATION COMPLETE`

# GPT-5.6 Terra — Release 1.12 WP04 Local Debug Signing Prerequisite Recovery Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: restore the established local Debug signing prerequisite and rerun only the blocked Debug signing gate.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source/tooling base:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Current instrumentation state:

```text
4 authorized tracked paths modified
0 staged paths
0 unauthorized tracked paths
```

Authorized modified paths already present and must remain untouched except for validation:

```text
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
```

Current deployed runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

No Docker, GHCR, Azure, Git, PR, issue, Project, milestone, tag, or release mutation has occurred.

## 2. Established signing contract

Binding signing policy:

```text
Release build: required
Release build warnings: 0
Release build errors: 0
Release Authenticode signature: NOT required
Debug local signing contract: required
Expected signer subject: CN=AIQuantTradingDev
Signing mechanism: existing Debug-only AutoSignTestBinaries target
```

Do not change this contract.

Do not weaken or bypass signing.

Do not add Release signing.

Do not change tracked signing policy.

## 3. Current blocker

Configured certificate thumbprint:

```text
96557D981D61698D46D4985E40C28626E625286D
```

Current execution context check:

```text
Cert:\CurrentUser\My
```

does not contain that certificate.

Therefore the Debug signing gate is blocked by a local prerequisite, not by an accepted code/test failure.

## 4. Authority objective

Restore or select a valid local code-signing certificate satisfying:

```text
Subject = CN=AIQuantTradingDev
usable private key = present
certificate store = CurrentUser\My
SignTool-accessible from this execution context
appropriate for code signing
```

Then rerun only the Debug signing gate and the minimum read-only checks needed to prove no tracked repository mutation occurred.

This authority does not authorize any instrumentation code change.

## 5. Allowed local-environment actions

Authorized local-only actions:

1. Inspect `Cert:\CurrentUser\My` for a valid existing `CN=AIQuantTradingDev` certificate.
2. If a valid existing certificate is present:
   - select its actual thumbprint;
   - update only the existing **ignored/local developer signing override** mechanism already used by this repository, if needed.
3. If no valid certificate exists:
   - generate a new self-signed local code-signing certificate with subject:
     ```text
     CN=AIQuantTradingDev
     ```
   - place it in:
     ```text
     Cert:\CurrentUser\My
     ```
   - ensure it has a private key;
   - configure the repository's existing ignored/local developer signing override to use the new thumbprint;
   - trust it locally only if required by the repository's established Debug signing flow.
4. Rerun the existing Debug signing gate.

No tracked repository file may be changed.

## 6. Certificate-generation constraints

If generation is required, preserve the existing local-development intent.

Use a Windows PowerShell 5.1-compatible path.

Required certificate properties:

```text
Subject = CN=AIQuantTradingDev
private key = present
purpose = code signing
store = CurrentUser\My
```

Do not:

```text
export private key
print private key material
commit certificate material
copy certificate/private key into repository
upload certificate anywhere
use production certificates
use Azure Key Vault
change CI signing policy
```

If local trust installation is required, it is authorized only for the generated `CN=AIQuantTradingDev` development certificate and only within the current user context where practical.

## 7. Thumbprint handling

Treat thumbprints as non-secret identifiers.

After selecting/generating the certificate:

```text
CERT_SUBJECT=CN=AIQuantTradingDev
CERT_THUMBPRINT=<actual thumbprint>
CERT_HAS_PRIVATE_KEY=True
```

The prior absent thumbprint:

```text
96557D981D61698D46D4985E40C28626E625286D
```

may be superseded locally.

Do not alter tracked policy merely to preserve that historical thumbprint.

## 8. Local override boundary

If the repository already uses an ignored local props/config file for the signing thumbprint, update only that existing ignored/local file.

Before modification, prove:

```text
file is ignored or otherwise explicitly local-only
file is not staged
file is not tracked
```

After modification, prove:

```text
tracked diff unchanged except for the already-authorized 4 instrumentation paths
staged paths = 0
local override remains untracked/ignored
```

If the only available signing configuration would require a tracked repository change, STOP.

## 9. Debug signing validation

Rerun the repository's existing Debug build/test signing gate using the established mechanism.

Required result:

```text
Debug signing target executes
SignTool finds selected certificate
signing succeeds
signed test binaries report signer subject = CN=AIQuantTradingDev
Debug build/test gate = PASS
```

Use the repository's existing validation commands where available.

Do not manually sign unrelated binaries as a substitute for the established target.

## 10. Instrumentation preservation checks

After signing recovery, verify read-only:

```text
tracked modified paths = exactly the same 4 authorized paths
staged paths = 0
unauthorized tracked paths = 0
```

Do not edit instrumentation while performing signing recovery.

The previously passed results remain historical evidence:

```text
Release build = PASS
full tests = PASS
HTTP integration = PASS
git diff --check = PASS
Gitleaks = PASS
allowlist audit = PASS
```

Only the blocked Debug signing gate must be rerun here unless the repository's signing target itself necessarily reruns a bounded subset.

## 11. Explicit prohibitions

Do not:

```text
change signing policy
disable signing
skip signing target
use Release Authenticode signing
modify csproj/props/targets in tracked source
stage files
commit
push
build Docker image
publish GHCR image
mutate Azure
restart/redeploy App Service
create PR
mutate issue
mutate Project #2
mutate milestone
create tag/release
begin WP05
```

## 12. Stop conditions

STOP if:

- no valid `CN=AIQuantTradingDev` certificate can be created/selected;
- certificate lacks a private key;
- SignTool still cannot access the certificate;
- recovery requires tracked repository changes;
- signing only passes by disabling/bypassing the target;
- signer subject differs from `CN=AIQuantTradingDev`;
- any unauthorized tracked path changes;
- staging occurs;
- any Docker/Azure/GitHub mutation appears necessary.

## 13. Mutation accounting

Authorized local-only mutations may include:

```text
certificate creation/import in CurrentUser certificate stores
local trust-store change if required
ignored/local signing override update
```

Required zero:

```text
tracked repository mutations beyond existing 4 instrumentation paths = 0
staging = 0
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

Count local certificate/trust/override mutations precisely.

## 14. Required return evidence

Return:

- whether an existing certificate was selected or a new one generated;
- certificate subject;
- certificate thumbprint;
- private-key presence;
- certificate store;
- whether local trust was changed;
- whether ignored/local override was changed;
- proof the override is untracked/ignored;
- Debug signing command/gate result;
- SignTool result;
- signer subject verification;
- tracked modified paths;
- staged path count;
- unauthorized tracked path count;
- exact local-environment mutation audit;
- exact repository/Azure/GitHub zero-mutation audit.

Do not return private key material.

## 15. Terminal markers

On success:

`RELEASE 1.12 WP04 — LOCAL DEBUG SIGNING PREREQUISITE RECOVERY: PASS`

`RELEASE 1.12 WP04 — DEBUG SIGNER SUBJECT: CN=AIQuantTradingDev`

`RELEASE 1.12 WP04 — DEBUG SIGNING GATE: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — TRACKED MUTATION PATH COUNT: 4`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION IMPLEMENTATION AUTHORITY: RESUME_READY`

`RELEASE 1.12 WP04 — TERRA LOCAL DEBUG SIGNING PREREQUISITE RECOVERY COMPLETE`

On block:

`RELEASE 1.12 WP04 — LOCAL DEBUG SIGNING PREREQUISITE RECOVERY: BLOCKED`

`RELEASE 1.12 WP04 — DEBUG SIGNING GATE: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

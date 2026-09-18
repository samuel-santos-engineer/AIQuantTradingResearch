# GPT-5.6 Terra — Release 1.12 WP04 Local Release-Signing Recovery & Publication-Gate Resume Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: restore the repository's approved local Release-signing configuration and rerun the blocked publication gates without widening the staged candidate.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

The exact governed candidate index is already staged:

```text
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
ADD eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required exact index semantics:

```text
3 modified + 1 added = 4 governed paths
```

Already passed:

- `origin/main` = `40a9a236dae789864f35e64bb5c1afd358e7b3db`
- WP04 branch one commit ahead; no remote divergence
- exact governed four-path index
- no unauthorized staged paths
- `git diff --cached --check`
- Release build: 0 warnings, 0 errors
- Domain tests: 11 passed
- Application tests: 136 passed
- Architecture tests: 27 passed
- Infrastructure tests: 195 passed
- targeted D3 tests: 2 passed
- PowerShell AST parse: 0 errors

Current blocker:

```text
AIQuantTradingResearch.Worker.exe  NotSigned
AIQuantTradingResearch.Worker.dll  NotSigned
```

Expected signing identity:

`CN=AIQuantTradingDev`

No commit/push/GHCR/Azure/PR mutation occurred.

## 2. Mission

Restore the repository's approved **local development Release-signing configuration** so the staged candidate can pass the mandatory signing gate.

Then rerun the publication gates from the unchanged staged candidate.

This authority is a local signing-recovery/resume authority only.

It does **not** authorize:

- changing the four-path staged payload;
- weakening or bypassing signing;
- changing repository signing policy;
- committing;
- pushing;
- building/publishing GHCR images;
- mutating Azure;
- creating a PR;
- lifecycle mutation.

## 3. Preserve the exact staged index

Before touching signing state, capture:

- `git status --short`
- `git diff --cached --name-status`
- `git diff --cached --check`

Prove the index contains exactly the four governed paths.

Do not unstage them unless a read-only reconciliation proves the signing recovery cannot proceed otherwise.

Required:

`RELEASE 1.12 WP04 — SIGNING RECOVERY INDEX PRESERVATION PRECHECK: PASS`

## 4. Reconcile the repository signing contract

Read-only inspect the repository-local signing configuration and prior local-development signing mechanism.

Identify the exact existing contract for:

- expected certificate subject/name;
- certificate store/location;
- thumbprint/configuration flow if used;
- local ignored props/settings file if used;
- build target responsible for signing;
- whether both `.exe` and `.dll` are expected to be Authenticode-signed;
- how the repository distinguishes local dev signing from CI/release publication signing.

Do not alter tracked signing-policy files.

Required:

`RELEASE 1.12 WP04 — RELEASE SIGNING CONTRACT RECONCILIATION: PASS`

## 5. Recover the local development certificate/configuration

If the expected `CN=AIQuantTradingDev` local certificate is missing, expired, unusable, or no longer trusted, recover it using the repository's established local-development signing procedure.

Permitted local-only recovery may include:

- generating/regenerating the expected local development signing certificate;
- importing it into the appropriate current-user certificate store;
- trusting it if repository instructions require local trust;
- updating the established **ignored/local-only** signing props/configuration with the new thumbprint/path as required.

Constraints:

- no tracked repository path may change;
- no signing bypass;
- no `Sign=false`, `SkipSigning`, or equivalent;
- no self-invented alternative subject;
- no export/share of private key material;
- no user secrets in logs/artifacts.

If recovery would require modifying a tracked signing-policy file, STOP for Luna re-governance.

Required:

`RELEASE 1.12 WP04 — LOCAL RELEASE SIGNING RECOVERY: PASS`

## 6. Rebuild from the preserved candidate

Run the same Release build against the unchanged staged candidate.

Required build result:

```text
0 warnings
0 errors
```

Then verify Authenticode signatures for at least:

```text
src/AIQuantTradingResearch.Worker/bin/Release/net10.0/AIQuantTradingResearch.Worker.exe
src/AIQuantTradingResearch.Worker/bin/Release/net10.0/AIQuantTradingResearch.Worker.dll
```

Required:

- status = valid/signed according to the repository's accepted local signing contract;
- signer subject includes `CN=AIQuantTradingDev`;
- no `NotSigned`.

Required markers:

`RELEASE 1.12 WP04 — RELEASE BUILD AFTER SIGNING RECOVERY: PASS`

`RELEASE 1.12 WP04 — WORKER EXE SIGNATURE: PASS`

`RELEASE 1.12 WP04 — WORKER DLL SIGNATURE: PASS`

## 7. Resume the blocked publication gates

After signing is restored, rerun all gates that were required before candidate commit:

- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted D3 tests;
- PowerShell AST parse;
- Gitleaks against the exact four governed paths;
- `git diff --cached --check`;
- exact staged-path audit.

Do not rely solely on the prior pre-recovery pass.

Required:

`RELEASE 1.12 WP04 — POST-SIGNING STAGED CANDIDATE VALIDATION: PASS`

## 8. Final staged-payload integrity check

Verify:

```text
git diff --cached --name-status
```

still equals exactly:

```text
M src/AIQuantTradingResearch.Worker/Program.cs
M src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
M tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
A eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Also verify:

- no tracked signing/config file changed;
- no unrelated file became staged;
- unrelated untracked work remains untouched;
- README unchanged.

Required:

`RELEASE 1.12 WP04 — SIGNING RECOVERY FOUR-PATH INDEX: PASS`

## 9. Mutation accounting

Report exact actual mutations.

Permitted local-only mutations may include:

```text
Local certificate store mutations: exact count
Local trust-store mutations: exact count
Ignored/local-only signing configuration mutations: exact count
Build output mutations: expected local build artifacts only
```

Expected zero:

```text
Tracked repository mutations outside existing four-path candidate: 0
Git staging payload changes: 0
Git commits: 0
Git pushes: 0
Docker mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/PR/lifecycle mutations: 0
```

Required:

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY MUTATION AUDIT: PASS`

## 10. Stop conditions

STOP if:

- the four-path index changes;
- a tracked signing-policy file must be modified;
- the required certificate identity cannot be established;
- signing succeeds only by bypassing/weakening the repository policy;
- `.exe` or `.dll` remains unsigned;
- any post-recovery validation gate fails;
- Gitleaks fails;
- an unrelated path becomes staged.

Do not commit or push under this authority.

## 11. Return evidence

Return:

- signing contract reconciliation;
- certificate recovery action taken;
- signer subject/thumbprint evidence with private material omitted;
- Release build result;
- `.exe` signature result;
- `.dll` signature result;
- rerun test results;
- PowerShell result;
- Gitleaks result;
- staged diff/path audit;
- exact mutation accounting.

Do not return secrets or private key material.

## 12. Terminal markers

On full success:

`RELEASE 1.12 WP04 — LOCAL RELEASE SIGNING RECOVERY: PASS`

`RELEASE 1.12 WP04 — RELEASE BUILD AFTER SIGNING RECOVERY: PASS`

`RELEASE 1.12 WP04 — WORKER EXE SIGNATURE: PASS`

`RELEASE 1.12 WP04 — WORKER DLL SIGNATURE: PASS`

`RELEASE 1.12 WP04 — POST-SIGNING STAGED CANDIDATE VALIDATION: PASS`

`RELEASE 1.12 WP04 — SIGNING RECOVERY FOUR-PATH INDEX: PASS`

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA CANDIDATE PUBLICATION/AZURE VALIDATION RESUME: READY`

`RELEASE 1.12 WP04 — TERRA LOCAL SIGNING RECOVERY COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — LOCAL RELEASE SIGNING RECOVERY: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

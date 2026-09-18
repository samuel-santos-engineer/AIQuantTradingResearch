# GPT-5.6 Terra — Release 1.12 WP04 Debug-Signing Recovery & Candidate Publication/Azure Validation Resume Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: recover the existing local Debug signing path if needed, validate the corrected signing gate, then resume the already-governed candidate publication and Azure durable-evidence qualification.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

The exact governed staged candidate remains:

```text
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
ADD eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Exact staging semantics:

```text
3 modified + 1 added = 4 governed paths
```

The previous signing-gate contradiction has been reconciled by Luna.

Accepted Luna decision:

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: S1`

Corrected signing contract:

```text
Release build: required; 0 warnings and 0 errors
Release Authenticode signature: not required
Debug local signing contract: required
Expected signer: CN=AIQuantTradingDev
Signing mechanism: existing Debug-only AutoSignTestBinaries target
```

No manual Release signing is authorized.

No tracked signing-policy change is authorized.

## 2. Preserve candidate integrity

Before any local signing recovery:

- verify the staged index remains exactly the four governed paths;
- verify `git diff --cached --check` passes;
- verify no unauthorized staged path exists;
- verify `origin/main` and branch divergence remain acceptable;
- verify no unrelated tracked work has appeared.

Required:

`RELEASE 1.12 WP04 — PUBLICATION RESUME INDEX PRECHECK: PASS`

## 3. Reconcile current Debug signing state

Read-only inspect:

- ignored `Directory.Build.local.props`;
- configured thumbprint;
- current `Cert:\CurrentUser\My`;
- tracked `AutoSignFirstPartyDebugBinaries` / `AutoSignTestBinaries` conditions.

Confirm whether the configured certificate is presently usable.

If already usable, do not regenerate it.

If missing/unusable, recover only the established local development certificate path.

Required:

`RELEASE 1.12 WP04 — DEBUG SIGNING STATE RECONCILIATION: PASS`

## 4. Permitted local Debug-signing recovery

Only if needed, Terra may:

- generate/regenerate a local development signing certificate with subject `CN=AIQuantTradingDev`;
- install it into the appropriate current-user certificate store;
- trust it if required by the existing repository workflow;
- update only ignored/local-only signing configuration with the resulting thumbprint.

Forbidden:

- tracked repository signing-policy changes;
- Release-signing additions;
- manual signing of Release artifacts;
- disabling signing;
- changing signer subject;
- exporting or exposing private-key material.

Required:

`RELEASE 1.12 WP04 — DEBUG LOCAL SIGNING RECOVERY: PASS`

## 5. Validate the corrected signing gate

### Release build

Run the repository's Release build.

Required:

```text
warnings = 0
errors = 0
```

Do **not** require Release `.exe` or `.dll` Authenticode signatures.

Required:

`RELEASE 1.12 WP04 — RELEASE BUILD GATE: PASS`

### Debug signing

Run the existing Debug build/signing path with the existing `AutoSignTestBinaries` contract enabled through the local ignored props configuration.

Verify the relevant first-party Worker binary/binaries are signed using:

`CN=AIQuantTradingDev`

At minimum verify the artifact(s) actually targeted by `AutoSignFirstPartyDebugBinaries`.

Do not infer which files are signed; inspect the tracked target and verify exactly what it signs.

Required:

`RELEASE 1.12 WP04 — DEBUG SIGNING CONTRACT: PASS`

## 6. Re-run complete staged-candidate publication gates

After the corrected signing gate passes, rerun:

- Release build;
- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted D3/SQLite tests;
- PowerShell AST parse;
- Gitleaks against the exact four governed paths;
- `git diff --cached --check`;
- exact staged-path audit.

Required:

`RELEASE 1.12 WP04 — STAGED CANDIDATE VALIDATION: PASS`

## 7. Selective staging integrity

Confirm the index still equals exactly:

```text
M src/AIQuantTradingResearch.Worker/Program.cs
M src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
M tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
A eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other path may be staged.

Required:

`RELEASE 1.12 WP04 — FOUR-PATH SELECTIVE STAGING: PASS`

## 8. Candidate commit and push

Create one narrow WP04 remediation commit containing exactly the four governed paths.

Capture:

- commit SHA;
- parent SHA;
- commit message;
- exact path count/status.

Push the current WP04 branch.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE PUSH: PASS`

The new commit SHA becomes the authoritative WP04 candidate source anchor.

## 9. Build and publish immutable candidate image

Build the container from the new candidate commit using the existing container contract.

Publish to the existing public/free GHCR repository.

Capture:

- immutable digest;
- anonymous manifest read;
- no registry username/password;
- source/digest provenance.

Do not modify Dockerfile/entrypoint.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE ANONYMOUS READ: PASS`

Record:

```text
WP04_DURABLE_D3_CANDIDATE_COMMIT=<sha>
WP04_DURABLE_D3_CANDIDATE_DIGEST=<sha256:...>
```

## 10. Azure deployment precheck

Before changing the Web App image, verify:

- App Service Linux F1 / Free;
- region = West Central US;
- current state readable;
- persistent storage enabled;
- database path remains `/home/data/aiquant.db`;
- create-parent setting remains enabled;
- no registry username/password;
- no temporary D3 settings;
- no paid service mutation.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE-EVIDENCE PRECHECK: PASS`

## 11. Deploy exact new digest

Update the Web App to the exact newly published immutable digest.

Do not use a mutable tag as acceptance identity.

Do not change:

- SKU;
- region;
- persistent storage;
- SQLite path;
- registry credentials;
- unrelated app settings.

Wait for `Running` / `Normal`.

Required:

`RELEASE 1.12 WP04 — AZURE EXACT-DIGEST DEPLOYMENT: PASS`

## 12. Durable evidence qualification — initialize

Generate a unique run ID.

Apply only the temporary settings required by the implemented D3 contract:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<unique initialize run id>
PersistentSqliteQualification__EvidenceOutputPath=<persistent /home artifact path>
```

Use exact implemented keys if casing/nesting differs.

Perform only the required controlled restart.

Retrieve the durable artifact through the governed supported Azure/SCM/file mechanism.

Require direct attribution to the run ID.

Validate:

```text
RecordVersion=1
Phase=initialize
RunId=<exact initialize run id>
DatabasePathIdentity=aiquant.db
SchemaVersion=4
JournalMode=delete
AcceptedEvidenceIdentity=<value>
AcceptedEvidenceCount=<value>
IntegrityCheck=ok
QuickCheck=ok
PersistenceContinuity=true
```

Capture:

```text
WP04_BASELINE_EVIDENCE_IDENTITY=<value>
WP04_BASELINE_EVIDENCE_COUNT=<value>
```

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

## 13. Durable evidence qualification — restart/reopen

Without changing image digest or database path:

- set phase to `reopen`;
- generate a new unique run ID;
- preserve the same evidence artifact path;
- perform exactly one governed Web App restart;
- retrieve the new artifact.

Require:

- direct run-ID attribution;
- schema `4`;
- journal `delete`;
- integrity `ok`;
- quick-check `ok`;
- baseline evidence identity preserved;
- evidence count satisfies the governed continuity expectation;
- persistence continuity true.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

## 14. Durable evidence qualification — same-digest redeploy/reopen

Prove continuity across a real deployment lifecycle boundary while preserving the exact same immutable digest.

Use an Azure-supported action that genuinely causes deployment/container replacement distinct from an ordinary restart.

Before the action:

- phase = `reopen`;
- generate a new unique redeploy run ID;
- preserve artifact path.

Afterward:

- wait for `Running` / `Normal`;
- retrieve the new artifact;
- prove direct run-ID attribution;
- prove image digest unchanged;
- prove evidence identity/count continuity;
- prove schema/journal/integrity/quick-check continuity.

Required:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

If the available action cannot be proven to represent a true deployment boundary, STOP for Luna reconciliation.

## 15. Cleanup/restoration

After qualification:

- remove all temporary D3 settings;
- restore any temporary logging state exactly;
- preserve ordinary persistence settings;
- verify app remains `Running` / `Normal`;
- verify exact candidate digest remains configured.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE-EVIDENCE CLEANUP: PASS`

## 16. Final Azure WP04 gate

All must pass:

- durable application-owned artifact retrieval;
- direct run attribution;
- initialize;
- restart/reopen;
- same-digest redeploy/reopen;
- schema v4;
- DELETE journal;
- integrity/quick-check;
- evidence identity/count continuity;
- persistent `/home`;
- no direct SQLite inspection;
- no paid infrastructure;
- provenance preserved.

Required:

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

## 17. PR creation boundary

Only after the full Azure qualification passes:

- create the WP04 PR;
- target the canonical branch;
- include concise validation evidence;
- do not merge;
- do not close #263;
- do not mutate Project #2 Status;
- do not close milestone #63.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

If Azure qualification fails, PR creation remains forbidden.

## 18. Mutation accounting

Report exact actual mutations.

Potential authorized mutations:

```text
Local certificate store mutations: exact count, only if needed
Local trust-store mutations: exact count, only if needed
Ignored/local signing config mutations: exact count, only if needed
Git commits: 1
Git pushes: 1
Docker image builds: exact count
GHCR image publications: exact count
Azure image-reference updates: exact count
Azure app-setting updates: exact count
Azure restarts: exact count
Azure same-digest redeployment actions: exact count
GitHub PR creates: 0 or 1
```

Expected zero:

```text
Tracked signing-policy mutations: 0
Release manual-signing mutations: 0
README mutations: 0
Schema migrations: 0
Package/dependency mutations: 0
Dockerfile/entrypoint mutations: 0
Registry credential mutations: 0
SKU/paid-service mutations: 0
Provider mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone closures: 0
```

Required:

`RELEASE 1.12 WP04 — PUBLICATION RESUME MUTATION AUDIT: PASS`

## 19. Stop conditions

STOP if:

- staged payload changes;
- Debug signing cannot be validated under the existing tracked contract;
- Release signing appears necessary despite S1;
- signing would require tracked policy changes;
- local validation regresses;
- Gitleaks fails;
- immutable digest cannot be proven;
- anonymous GHCR read fails;
- Azure F1 quota/suspension returns;
- persistence settings drift;
- durable artifact retrieval fails;
- run attribution is ambiguous;
- restart/redeploy continuity fails;
- paid infrastructure would be required.

Do not self-authorize a wider scope.

## 20. Return evidence

Return:

- certificate recovery result, if any;
- Debug signing evidence;
- Release build evidence;
- full publication-gate results;
- candidate commit SHA;
- image digest;
- Azure initialize/restart/redeploy D3 records;
- evidence identity/count continuity;
- cleanup/restoration evidence;
- PR number if created;
- exact mutation accounting.

Do not return secrets or private-key material.

## 21. Terminal markers

### Full success

`RELEASE 1.12 WP04 — DEBUG SIGNING CONTRACT: PASS`

`RELEASE 1.12 WP04 — STAGED CANDIDATE VALIDATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — PUBLICATION RESUME MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — FINAL ACCEPTANCE/LIFECYCLE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA DEBUG-SIGNING RECOVERY/PUBLICATION RESUME COMPLETE`

### Blocked

`RELEASE 1.12 WP04 — DEBUG-SIGNING RECOVERY/PUBLICATION RESUME: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

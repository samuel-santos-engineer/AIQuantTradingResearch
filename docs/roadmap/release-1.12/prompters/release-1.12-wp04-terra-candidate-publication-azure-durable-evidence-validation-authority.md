# GPT-5.6 Terra — Release 1.12 WP04 Candidate Publication & Azure Durable-Evidence Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: selectively stage the governed four-path payload, commit/push it, publish the corresponding immutable image, deploy that exact digest to Azure, and execute the governed durable-evidence qualification sequence.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Previous candidate lineage:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Previous deployed image digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Luna reconciliation established:

`RELEASE 1.12 WP04 — VERIFICATION HELPER RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — VERIFICATION HELPER PAYLOAD DECISION: P1`

`RELEASE 1.12 WP04 — FOUR-PATH PUBLICATION SEMANTICS: PRESERVED`

`RELEASE 1.12 WP04 — DURABLE D3 PAYLOAD GOVERNANCE AMENDMENT: PASS`

Exact publication semantics:

```text
3 modified tracked paths + 1 added tracked path = 4 exact governed paths
```

## 2. Exact governed publication payload

Only these four paths may enter the new candidate commit:

```text
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
ADD eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other tracked or untracked path may be staged.

Explicitly exclude:

- `README.md`
- `prompters/`
- unrelated untracked helpers
- Dockerfile/entrypoint
- schema files
- package/project dependency files
- Python/Streamlit
- unrelated deployment scripts

## 3. Pre-publication reconciliation

Before staging, verify:

- working tree contains exactly the three governed tracked modifications;
- the governed helper exists untracked;
- no unauthorized tracked diff exists;
- no unauthorized file is staged;
- local validation evidence remains intact;
- current branch is the WP04 branch;
- local branch ancestry includes `ef4a5caf4768ab82c66f0e539d92c1631761b500`;
- `main`/remote state has not introduced a conflict requiring re-governance.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PUBLICATION PRECHECK: PASS`

## 4. Selective staging

Stage only the exact four governed paths.

Use explicit path-based staging. Do not use broad staging commands such as:

```text
git add .
git add -A
git add --all
```

After staging, prove the index contains exactly:

```text
3 modified + 1 added = 4 governed paths
```

Required:

`RELEASE 1.12 WP04 — FOUR-PATH SELECTIVE STAGING: PASS`

## 5. Re-run publication gates

Before commit, re-run the established local publication gates against the exact staged candidate:

- Release build;
- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted SQLite/D3 tests;
- PowerShell AST validation;
- Gitleaks;
- `git diff --cached --check`;
- exact staged-path audit.

Signing contract remains mandatory. Do not bypass signing.

Required:

`RELEASE 1.12 WP04 — STAGED CANDIDATE VALIDATION: PASS`

## 6. Candidate commit and push

Create one narrow WP04 remediation commit containing exactly the four governed paths.

Capture:

- commit SHA;
- commit message;
- exact path count/status;
- parent SHA.

Push the current WP04 branch.

Do not merge.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE PUSH: PASS`

The new commit SHA becomes the authoritative WP04 candidate source anchor.

## 7. Build and publish immutable candidate image

Build the container from the new candidate commit using the existing repository container contract.

Publish to the existing public/free GHCR repository.

Capture and verify:

- source commit embedded/associated with image build as repository convention allows;
- immutable GHCR digest;
- anonymous manifest read;
- no registry username/password required;
- no tag/digest ambiguity.

Do not change Dockerfile/entrypoint.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE ANONYMOUS READ: PASS`

Record:

```text
WP04_DURABLE_D3_CANDIDATE_COMMIT=<sha>
WP04_DURABLE_D3_CANDIDATE_DIGEST=<sha256:...>
```

## 8. Azure deployment precheck

Before changing the Web App image, verify:

- App Service Linux F1 / Free;
- region = West Central US;
- current state readable;
- persistent storage enabled;
- database path remains `/home/data/aiquant.db`;
- parent-directory initialization setting remains enabled;
- no registry username/password;
- no temporary D3 settings;
- no paid services;
- logging restored to its prior disabled state unless the verification helper explicitly requires otherwise.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE-EVIDENCE PRECHECK: PASS`

## 9. Deploy exact new digest

Update the Web App to the exact newly published immutable digest.

Do not use a mutable tag as acceptance identity.

Do not alter:

- SKU;
- region;
- persistent storage;
- SQLite path;
- registry credentials;
- unrelated app settings.

Wait for the Web App to return to `Running` / `Normal`.

Required:

`RELEASE 1.12 WP04 — AZURE EXACT-DIGEST DEPLOYMENT: PASS`

## 10. Durable evidence location

Use an application-owned qualification artifact under persistent `/home`.

The exact artifact path must:

- be under persistent App Service storage;
- not overlap the SQLite database path;
- be writable by the governed non-root runtime;
- contain no secrets;
- be retrieved only through a supported Azure/SCM/file surface;
- never require direct SQLite inspection.

Use one stable governed artifact path unless the helper contract specifically requires otherwise.

## 11. Qualification A — initialize

Generate a unique run ID for the initialize attempt.

Apply only the temporary qualification settings required by the implemented contract, including:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__RunId=<unique initialize run id>
PersistentSqliteQualification__EvidenceOutputPath=<persistent /home artifact path>
```

Use the exact configuration keys implemented by the candidate if their names differ in casing/nesting.

Perform exactly one controlled restart if required to activate the settings.

Use the governed verification helper to retrieve and validate the durable artifact.

The record must be directly attributable to the initialize run ID.

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

Capture the initialize baseline:

```text
WP04_BASELINE_EVIDENCE_IDENTITY=<value>
WP04_BASELINE_EVIDENCE_COUNT=<value>
```

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

## 12. Qualification B — restart/reopen continuity

Without changing the image digest or database path:

1. set phase to `reopen`;
2. generate a new unique restart/reopen run ID;
3. preserve the same durable evidence output path;
4. perform exactly one governed Web App restart;
5. retrieve the durable artifact through the supported Azure/SCM surface.

The artifact must be attributable to the restart/reopen run ID.

Validate:

- `Phase=reopen`;
- schema remains `4`;
- journal remains `delete`;
- integrity remains `ok`;
- quick-check remains `ok`;
- accepted evidence identity equals `WP04_BASELINE_EVIDENCE_IDENTITY`;
- accepted evidence count equals the governed expected continuity value defined by the implementation/test contract;
- persistence continuity is true.

No direct SQLite query is permitted.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

## 13. Qualification C — same-digest redeploy/reopen continuity

Prove continuity across a true deployment lifecycle boundary while preserving the exact same immutable image digest.

Use an Azure-supported same-digest redeployment/recycle action that genuinely exercises App Service deployment/container replacement without changing the image identity.

Before the redeployment:

- set `Phase=reopen`;
- generate a new unique redeploy/reopen run ID;
- preserve the same evidence output path.

After the deployment boundary:

- wait for `Running` / `Normal`;
- retrieve the durable artifact;
- prove direct attribution to the redeploy/reopen run ID.

Validate:

- exact image digest unchanged;
- `Phase=reopen`;
- schema `4`;
- journal `delete`;
- integrity `ok`;
- quick-check `ok`;
- accepted evidence identity remains equal to the initialize baseline;
- accepted evidence count satisfies the governed continuity expectation;
- persistence continuity is true.

Required:

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

If the available Azure action cannot be proven to create a real redeployment lifecycle boundary distinct from an ordinary restart, STOP for Luna reconciliation rather than falsely crediting it.

## 14. Cleanup and restoration

After qualification:

- remove all temporary D3 settings:
  - Worker mode
  - phase
  - run ID
  - evidence output path
- restore any temporary logging state exactly;
- preserve the durable evidence artifact unless the verification contract explicitly requires cleanup;
- re-read persistence configuration;
- verify the Web App remains `Running` / `Normal`;
- verify the exact candidate digest remains configured.

Required:

`RELEASE 1.12 WP04 — AZURE DURABLE-EVIDENCE CLEANUP: PASS`

## 15. Final Azure WP04 evidence gate

All of the following must be proven before publication can advance:

- durable application-owned D3 artifact retrieval;
- direct run attribution;
- initialize success;
- restart/reopen continuity;
- same-digest redeploy/reopen continuity;
- schema v4;
- DELETE journal;
- integrity `ok`;
- quick-check `ok`;
- governed evidence identity/count continuity;
- persistent `/home` use;
- no direct SQL deployment bypass;
- no secret leakage;
- zero paid infrastructure;
- candidate commit/digest provenance.

Required:

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

## 16. PR boundary

Only after all Azure gates above pass:

- create the WP04 pull request for the exact four-path candidate commit;
- target the canonical branch;
- include concise evidence of local validation and Azure initialize/restart/redeploy qualification;
- do not merge;
- do not close #263;
- do not mutate Project #2 status;
- do not close milestone #63.

Capture PR number/URL.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

If any Azure acceptance gate fails, do **not** create the PR.

## 17. Mutation accounting

Report exact mutations.

Potential authorized mutations:

```text
Git staging: exact 4 governed paths
Git commits: 1
Git pushes: 1
Docker image builds: exact count
GHCR image publications: exact count
Azure image-reference updates: exact count
Azure qualification app-setting updates: exact count
Azure restarts: exact count
Azure same-digest redeployment actions: exact count
Azure temporary-setting removals/restorations: exact count
GitHub PR creates: 0 or 1, only after full Azure PASS
```

Expected zero:

```text
README mutations: 0
Schema migrations: 0
Package/dependency mutations: 0
Dockerfile/entrypoint mutations: 0
Registry credential mutations: 0
SKU/resource paid-service mutations: 0
Provider mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone closures: 0
```

Required:

`RELEASE 1.12 WP04 — CANDIDATE PUBLICATION/AZURE VALIDATION MUTATION AUDIT: PASS`

## 18. Stop conditions

STOP immediately if:

- staged payload differs from the exact four paths;
- local validation regresses;
- signing would need bypass;
- image digest cannot be proven;
- anonymous GHCR read fails;
- F1 quota/suspension returns;
- persistence settings drift;
- direct SQL would be needed;
- durable artifact cannot be retrieved;
- run attribution is ambiguous;
- restart continuity fails;
- same-digest redeploy continuity cannot be proven;
- paid infrastructure would be required;
- PR payload would differ from the governed candidate.

Do not self-authorize another architecture change.

## 19. Return evidence

Return:

- selective staging audit;
- new candidate commit SHA;
- push result;
- new immutable image digest;
- anonymous manifest-read evidence;
- Azure deployment evidence;
- initialize run ID + full D3 artifact;
- restart/reopen run ID + full D3 artifact;
- redeploy/reopen run ID + full D3 artifact;
- baseline and continuity evidence identity/count;
- cleanup/restoration evidence;
- PR number if created;
- exact mutation accounting.

Do not return secrets.

## 20. Terminal markers

### Full success

`RELEASE 1.12 WP04 — FOUR-PATH SELECTIVE STAGING: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — AZURE DURABLE INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART/REOPEN PERSISTENCE CONTINUITY: PASS`

`RELEASE 1.12 WP04 — AZURE SAME-DIGEST REDEPLOY/REOPEN CONTINUITY: PASS`

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PUBLICATION/AZURE VALIDATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — FINAL ACCEPTANCE/LIFECYCLE AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA CANDIDATE PUBLICATION/AZURE VALIDATION COMPLETE`

### Azure validation blocked

`RELEASE 1.12 WP04 — AZURE APPLICATION-OWNED PERSISTENCE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — CANDIDATE PR CREATED: NO`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

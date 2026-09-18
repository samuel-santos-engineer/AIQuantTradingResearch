# GPT-5.6 Terra — Release 1.12 WP03 Implementation Authority

**Selected execution model: GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, path designation, governance, acceptance criteria.
- **GPT-5.6 Terra** — PRIMARY: implementation, validation execution, approved Git/GitHub/Azure/GHCR mutations, PR publication.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna or Terra.

## 1. Mission

Implement:

**Release 1.12 WP03 — GHCR Publication & Azure F1 Deployment Automation**

GitHub work package:

`#262`

This authority implements and validates the Luna-approved four-script automation boundary, performs the required interactive authenticated GHCR/Azure qualification through the established operator handoff, and publishes the exact implementation as a dedicated PR.

**PR merge is NOT authorized by this authority.**

WP03 lifecycle closure is NOT authorized by this authority.

## 2. Canonical base

Expected canonical `main`:

`a3fd52b96e5479e4d11824cfad46bd63d0435811`

Before mutation prove:
- `origin/main` equals expected SHA;
- local `main` equals `origin/main`;
- ahead/behind `0/0`;
- staging empty;
- unrelated requester-only/local files remain excluded.

Fresh evidence controls. If canonical `main` has advanced unexpectedly, STOP for reconciliation.

## 3. Binding Luna contract

Luna emitted:

`RELEASE 1.12 WP03 — REPOSITORY PATH DESIGNATION: PASS`

`GPT-5.6 TERRA WP03 MUTATION CONTRACT: ONLY WP03_AUTHORIZED_PATH_SET MAY BE MUTATED`

This authority embeds that contract verbatim.

### WP03_AUTHORIZED_PATH_SET

| Operation | Path |
|---|---|
| CREATE | `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/cleanup-f1-reference.ps1` |
| CREATE | `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1` |
| CREATE | `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/publish-ghcr-image.ps1` |
| CREATE | `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/verify-f1-reference.ps1` |

Binding counts:

`AUTHORIZED_PATH_COUNT=4`
`CREATE_PATH_COUNT=4`
`MODIFY_PATH_COUNT=0`
`DELETE_PATH_COUNT=0`
`RENAME_PATH_COUNT=0`
`SCRIPT_PATH_COUNT=4`

No other repository path may be mutated.

If another path becomes necessary, STOP and return to GPT-5.6 Luna for amendment.

## 4. Explicit deny boundary

Do not mutate:

- `Dockerfile`
- `.dockerignore`
- `container/entrypoint.sh`
- `requirements.txt`
- `Directory.Build.props`
- `Directory.Build.targets`
- `Directory.Packages.props`
- `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a1.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a2.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a3.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a4.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a5.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a6.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a7.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a8.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a9.ps1`
- `eng/azure-cli/r1.12-deployment/wp02-deployment/a10.ps1`

All 73 paths under `eng/azure-cli/r1.11-qualification/` are immutable historical evidence for this WP.

Do not mutate application pipeline source, Python presentation/read-model source, tests, package files, schema/migrations, workflows, or prompter/control artifacts.

## 5. Implementation requirements

### 5.1 `publish-ghcr-image.ps1`

Implement a bounded operator script that:

- accepts explicit parameters rather than embedding credentials;
- builds the repository Docker image from the existing `Dockerfile`;
- targets the intended public/free GHCR repository;
- applies deterministic Release 1.12-compatible image tagging;
- authenticates only through operator-provided/existing secure GitHub/Docker context;
- never prints or persists credentials;
- pushes the image;
- reads back the pushed digest;
- validates the public manifest anonymously after publication;
- fails closed on build/tag/push/digest/public-readback failure;
- returns non-zero on failure;
- emits concise machine-readable or unambiguous evidence suitable for Terra evaluation.

Do not commit a PAT/token, Docker auth, username-specific path, or secret-bearing command.

### 5.2 `deploy-f1-reference.ps1`

Implement bounded Azure CLI deployment automation for the qualified reference architecture:

- Azure App Service Linux;
- Free/F1;
- West Central US;
- custom Docker image from public GHCR;
- default/public HTTPS/DNS;
- HTTPS-only;
- persistent `/home` enabled through the appropriate App Service storage setting;
- no ACR;
- no Azure Files;
- no Azure SQL;
- no paid monitoring/networking/tier dependency;
- no production-SLA claim.

Resource names, resource group, app name, image reference, and other environment-specific values must be parameters or bounded generated values, not hard-coded personal identifiers.

The script must:
- verify Azure authentication/subscription context before mutation;
- make intended mutations explicit;
- tag/identify WP03-owned resources sufficiently for bounded verification/cleanup;
- fail closed;
- avoid deleting unrelated resources;
- support safe re-execution where feasible;
- expose resulting app/resource identifiers as evidence.

Do not implement WP04 database initialization/recovery or WP05 Twelve Data secret/provider automation.

### 5.3 `verify-f1-reference.ps1`

Implement read-only verification for the deployed WP03 boundary.

Verify at minimum:
- expected owned resource group/app/plan identity;
- Linux App Service;
- Free/F1 plan/tier;
- West Central US;
- expected public GHCR image reference;
- HTTPS-only;
- persistent App Service storage setting enabled;
- no ACR dependency introduced by WP03;
- resource ownership/tagging sufficient for bounded cleanup.

Verification must not modify Azure resources.

Where feasible, verify public endpoint reachability without claiming WP06 System Health acceptance.

### 5.4 `cleanup-f1-reference.ps1`

Implement bounded cleanup for only WP03-owned reference resources.

Requirements:
- require explicit target/resource ownership inputs;
- verify ownership/tagging before deletion;
- refuse ambiguous/unowned deletion;
- delete only the WP03-qualified resource boundary;
- read back absence after deletion;
- fail closed if cleanup is incomplete;
- never delete unrelated resource groups/resources.

Cleanup is part of WP03 qualification so recurring infrastructure cost returns to `$0.00` after the probe unless an explicit later authority authorizes retained infrastructure.

## 6. Cross-WP isolation

### WP04
Do not implement:
- SQLite initialization;
- data update/recovery;
- journal-mode logic;
- schema changes/migrations.

The deployment script may only enable persistent `/home` at the App Service boundary.

### WP05
Do not implement:
- Twelve Data API key provisioning;
- provider secrets;
- provider scheduling/data automation.

### WP06
Do not modify Streamlit/System Health behavior or claim WP06 acceptance.

### WP07
Do not claim final release stability/cost/no-bypass acceptance.

### WP08
Do not modify final runbook/release documentation.

## 7. Static implementation validation before cloud mutation

Before authenticated external execution:

1. prove only the four authorized paths were created/changed;
2. PowerShell parse/syntax validation for all four scripts;
3. `git diff --check`;
4. Gitleaks/secret screening;
5. inspect for hard-coded credentials/tokens/subscription IDs/tenant IDs/personal paths;
6. inspect for forbidden paid-resource dependencies;
7. inspect for WP04/WP05/WP06 implementation leakage;
8. inspect cleanup ownership safeguards;
9. inspect error handling and non-zero failure behavior.

Required marker:

`RELEASE 1.12 WP03 — STATIC IMPLEMENTATION VALIDATION: PASS`

Do not proceed to authenticated mutations if this fails.

## 8. Interactive Windows operator handoff

Authenticated Docker/GHCR/Azure execution must occur under the established interactive Windows user context.

Terra must generate exact copy/paste PowerShell commands for each phase.

For every command block Terra must state:
- purpose;
- whether read-only or mutating;
- expected external mutations;
- expected stdout/stderr evidence;
- expected exit code;
- rollback/cleanup implication.

Then Terra MUST STOP and wait for the user to return actual stdout/stderr and exit codes.

Terra must independently evaluate returned evidence.

Never:
- weaken Docker pipe ACLs;
- transfer Docker auth files;
- transfer Azure auth cache/profile/tokens;
- request secrets in chat;
- echo secrets;
- infer successful execution without evidence.

User-executed mutations count in the final audit.

## 9. External qualification sequence

Execute in bounded phases.

### Phase A — environment/auth read-only checks

Generate exact commands to establish:
- Docker availability;
- Azure CLI availability/version;
- Azure signed-in context;
- GitHub/GHCR authentication mechanism availability without revealing secrets;
- repository/current branch/base state.

No cloud mutation.

Required:

`RELEASE 1.12 WP03 — OPERATOR ENVIRONMENT GATE: PASS`

### Phase B — GHCR publication

Run the approved `publish-ghcr-image.ps1`.

Expected mutation classes:
- local Docker build/tag;
- public GHCR package/image publication.

Capture:
- image tag;
- digest;
- push success;
- anonymous manifest/public-readback success.

Required:

`RELEASE 1.12 WP03 — GHCR PUBLICATION: PASS`

`RELEASE 1.12 WP03 — GHCR DIGEST READBACK: PASS`

`RELEASE 1.12 WP03 — GHCR ANONYMOUS PUBLIC MANIFEST: PASS`

### Phase C — Azure F1 deployment

Run `deploy-f1-reference.ps1`.

Expected mutation classes:
- bounded Azure resource group/resources required for App Service Linux F1 reference deployment;
- app configuration required by WP03.

Capture exact created/updated resources and settings.

Required:

`RELEASE 1.12 WP03 — AZURE F1 DEPLOYMENT: PASS`

### Phase D — read-only deployment verification

Run `verify-f1-reference.ps1`.

Prove:
- Linux App Service;
- Free/F1;
- West Central US;
- public GHCR image;
- HTTPS-only;
- persistent `/home` setting;
- no ACR dependency;
- bounded WP03 ownership.

Required:

`RELEASE 1.12 WP03 — AZURE F1 READBACK VERIFICATION: PASS`

`RELEASE 1.12 WP03 — STRICT-ZERO-COST ARCHITECTURE: PASS`

This marker means the selected WP03 architecture uses only the qualified free dependencies. Do not overclaim future billing guarantees.

### Phase E — cleanup

Run `cleanup-f1-reference.ps1`.

Prove owned Azure resources are removed and read-back confirms absence.

Required:

`RELEASE 1.12 WP03 — AZURE QUALIFICATION CLEANUP: PASS`

If the public GHCR image/tag is intentionally retained as free publication evidence, record it explicitly. Do not claim zero external residue; distinguish free retained GHCR evidence from Azure recurring resources.

## 10. Repository validation after external qualification

Re-run:
- exact four-path diff check;
- PowerShell syntax validation;
- `git diff --check`;
- Gitleaks;
- forbidden-path check;
- no secret-bearing evidence added to repository.

No generated execution logs/evidence files may be added unless Luna separately authorizes paths.

Required:

`RELEASE 1.12 WP03 — POST-QUALIFICATION REPOSITORY VALIDATION: PASS`

## 11. WP03 implementation acceptance gate

Before Git publication, all must pass:

- exact 4/4 path compliance;
- four scripts exist and are syntactically valid;
- Gitleaks clean;
- whitespace clean;
- no committed secrets;
- GHCR image publication successful;
- digest readback successful;
- anonymous public manifest successful;
- Azure App Service Linux F1 deployment successful;
- West Central US proven;
- public GHCR/no-ACR proven;
- HTTPS-only proven;
- persistent `/home` setting proven;
- no WP04–WP06 bypass;
- no paid-resource dependency introduced;
- cleanup of owned Azure qualification resources proven;
- exact external mutation audit available.

Acceptance marker:

`RELEASE 1.12 WP03 — GHCR PUBLICATION & AZURE F1 DEPLOYMENT AUTOMATION: PASS`

If any condition fails, do not publish implementation as accepted.

## 12. Git publication authority

After WP03 implementation acceptance passes:

- create a dedicated branch from the reconciled canonical base;
- stage only the four authorized paths;
- never use `git add .` or `git add -A`;
- prove staged payload exactly 4/4 creates;
- commit once;
- push once;
- create one non-draft PR to `main`.

Preferred branch:

`feat/release-1.12-wp03-ghcr-azure-f1`

Preferred commit:

`Implement Release 1.12 WP03 GHCR and Azure F1 automation`

Preferred PR title:

`Release 1.12 WP03: GHCR publication and Azure F1 deployment automation`

## 13. PR payload gate

Before authority completion prove using authoritative Git comparison:

- exactly four paths;
- all four are Luna-authorized;
- all are creates;
- zero unrelated paths;
- no denylisted path;
- PR base is `main`;
- expected head commit is present.

Required:

`RELEASE 1.12 WP03 — IMPLEMENTATION PR PAYLOAD 4/4: PASS`

## 14. Merge and lifecycle prohibition

This authority DOES NOT authorize:
- PR merge;
- closing #262;
- setting #262 Done;
- milestone mutation;
- tag/release publication.

At PR boundary:
- #262 must remain Open/Todo;
- milestone #63 must remain Open.

After this authority, a separate GPT-5.6 Terra merge/post-merge/lifecycle authority is required.

## 15. Mutation audit

Report exact counts for:

### Repository/Git/GitHub
- repository creates: expected 4;
- repository modifies/deletes/renames: expected 0;
- branches;
- commits;
- pushes;
- PRs;
- PR merges: 0;
- issue mutations: 0;
- Project mutations: 0;
- milestone mutations: 0.

### Docker/GHCR
- builds;
- tags;
- pushes;
- retained image tags/digests;
- local cleanup operations if any.

### Azure
- resource groups created/updated/deleted;
- App Service plans created/updated/deleted;
- web apps created/updated/deleted;
- app-setting/config mutations;
- other resource mutations;
- final owned Azure resource residue.

### Other
- provider mutations: 0;
- package mutations: 0;
- schema mutations: 0.

Count user-executed mutations.

## 16. Required markers

`RELEASE 1.12 WP03 — TERRA BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP03 — AUTHORIZED PATH CONTRACT 4/4: PASS`

`RELEASE 1.12 WP03 — STATIC IMPLEMENTATION VALIDATION: PASS`

`RELEASE 1.12 WP03 — OPERATOR ENVIRONMENT GATE: PASS`

`RELEASE 1.12 WP03 — GHCR PUBLICATION: PASS`

`RELEASE 1.12 WP03 — GHCR DIGEST READBACK: PASS`

`RELEASE 1.12 WP03 — GHCR ANONYMOUS PUBLIC MANIFEST: PASS`

`RELEASE 1.12 WP03 — AZURE F1 DEPLOYMENT: PASS`

`RELEASE 1.12 WP03 — AZURE F1 READBACK VERIFICATION: PASS`

`RELEASE 1.12 WP03 — STRICT-ZERO-COST ARCHITECTURE: PASS`

`RELEASE 1.12 WP03 — AZURE QUALIFICATION CLEANUP: PASS`

`RELEASE 1.12 WP03 — POST-QUALIFICATION REPOSITORY VALIDATION: PASS`

`RELEASE 1.12 WP03 — GHCR PUBLICATION & AZURE F1 DEPLOYMENT AUTOMATION: PASS`

`RELEASE 1.12 WP03 — STAGED PAYLOAD 4/4: PASS`

`RELEASE 1.12 WP03 — COMMIT: PASS`

`RELEASE 1.12 WP03 — PUSH: PASS`

`RELEASE 1.12 WP03 — IMPLEMENTATION PR PAYLOAD 4/4: PASS`

`RELEASE 1.12 WP03 — MUTATION AUDIT: PASS`

Boundary:

`RELEASE 1.12 WP03 — IMPLEMENTATION PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

Next:

`RELEASE 1.12 WP03 — MERGE/POST-MERGE/LIFECYCLE AUTHORITY: READY TO CREATE`

Terminal:

`RELEASE 1.12 WP03 — IMPLEMENTATION AUTHORITY COMPLETE`

If blocked:

`RELEASE 1.12 WP03 — IMPLEMENTATION AUTHORITY BLOCKED`

State the exact failed gate and do not widen the path set or infer external success.

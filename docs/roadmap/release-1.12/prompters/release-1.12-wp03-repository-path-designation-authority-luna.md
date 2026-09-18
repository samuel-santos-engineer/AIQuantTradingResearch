# GPT-5.6 Luna — Release 1.12 WP03 Repository Path Designation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: contract, policy, architecture, path designation, governance, acceptance criteria, read-only reconciliation.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub/Azure/GHCR mutations, publication, merge, and lifecycle under a later explicit authority.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory review only; never silently replaces Luna or Terra.

## 1. Mission

Designate the exact literal repository mutation contract for:

**Release 1.12 WP03 — GHCR Publication & Azure F1 Deployment Automation**

GitHub work package:

`#262`

This authority is strictly read-only and exists because the frozen Release 1.12 manifest does not itself authorize unspecified future implementation paths.

No WP03 implementation mutation may begin until this authority emits an exact literal path contract.

## 2. Canonical starting point

Expected canonical `main` after PR #271:

`a3fd52b96e5479e4d11824cfad46bd63d0435811`

Expected:
- local `main` = `origin/main`
- ahead/behind `0/0`
- staging empty

Fresh empirical evidence controls.

Expected lifecycle:
- #261 WP02 — Closed/Done
- #262 WP03 — Open/Todo
- milestone #63 — Open, 6 open / 2 closed

## 3. Release 1.12 inherited architecture

Preserve the Release 1.12 product contract:

- Azure App Service Linux F1
- West Central US
- custom Docker
- public/default HTTPS/DNS
- persistent `/home`
- writable SQLite
- SQLite DELETE journal mode
- public/free GHCR image distribution
- bounded Twelve Data outbound/authenticated connectivity
- Streamlit public demo
- strict recurring infrastructure cost `$0.00`
- reference/demo use only
- no production SLA claims

Preserve application architecture:
- .NET owns canonical pipeline behavior
- canonical JSON handoff remains intact
- Python/Streamlit consumes governed read model
- no direct Streamlit SQLite/provider/Worker supervision bypass
- deterministic/replay/simulated provenance remains truthful
- System Health remains truthful

## 4. WP03 purpose boundary

WP03 is specifically:

**GHCR Publication & Azure F1 Deployment Automation**

WP03 should establish the repository implementation needed to:
- build/publish the Release 1.12 container image to public/free GHCR;
- deploy/update that image to Azure App Service Linux F1;
- configure only the deployment-level settings required for the WP03 boundary;
- provide bounded/reproducible automation for image publication and F1 deployment;
- preserve `$0.00` architecture constraints;
- expose exact commands/automation suitable for the established interactive Windows operator handoff where authenticated local execution is required.

WP03 is not the place to implement:
- persistent SQLite initialization/recovery logic owned by WP04;
- Twelve Data runtime secret/config automation owned by WP05;
- Streamlit/System Health public diagnostics behavior owned by WP06;
- final stability/cost/no-bypass qualification owned by WP07;
- final documentation/runbook/release acceptance owned by WP08.

## 5. Historical Initiative-1.11 evidence boundary

The repository now contains published Initiative-1.11 qualification scripts under:

`eng/azure-cli/r1.11-qualification/`

These are historical feasibility evidence.

They MAY be:
- read;
- referenced;
- used as design evidence;
- mechanically adapted only if Luna explicitly designates a new Release 1.12 path for implementation.

They MUST NOT be modified in place for WP03 unless Luna explicitly concludes that mutating historical evidence is required and justified.

Default governance preference:

**historical Initiative-1.11 qualification artifacts remain immutable; Release 1.12 implementation gets Release 1.12-specific paths.**

## 6. Existing Release 1.12 deployment/control structure

Inspect the now-published repository state, especially:

`eng/azure-cli/r1.12-deployment/`

and any related Release 1.12 deployment scripts/configuration published by PR #271.

Do not assume filenames imply WP03 ownership.

Determine which existing files:
- are WP02 historical/operator artifacts;
- are reusable read-only evidence;
- are suitable for WP03 modification;
- should remain untouched;
- indicate the correct directory convention for new WP03 artifacts.

Also inspect:
- `Dockerfile`
- `.dockerignore`
- `container/entrypoint.sh`
- Release 1.12 planning artifacts
- repository CI/workflow conventions
- existing GHCR/container publication conventions
- existing Azure CLI automation conventions
- current documentation structure

## 7. Frozen planning sources

Read at minimum:

- `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

Treat them as frozen historical planning artifacts.

Do not edit them under this authority.

If the frozen manifest lacks literal WP03 paths, this authority supplies the separately required WP03 literal path contract.

## 8. Required repository inspection

Perform read-only inspection sufficient to answer:

### GHCR publication
- What repository files are required to build/tag/push the existing Docker image to GHCR?
- Does existing CI/CD infrastructure already support GHCR publication?
- Is a GitHub Actions workflow appropriate/necessary?
- Is an operator PowerShell script appropriate/necessary?
- Is image naming/tagging policy already defined?
- What authentication boundary is required without committing credentials?

### Azure F1 deployment automation
- What exact scripts/configuration are required to create/configure/update the App Service F1 reference deployment?
- Which operations require Azure CLI in interactive user context?
- Which environment/resource identifiers must remain parameters rather than hard-coded repository values?
- Which app settings belong to WP03 versus WP04/WP05?
- How is `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true` represented if needed at this boundary?
- How is the GHCR public image configured without making ACR mandatory?
- How are region/plan/tier defaults represented truthfully?

### Validation
- What test/validation paths are required for WP03?
- Can validation be performed through scripts only?
- Is a dedicated test path needed?
- Are GitHub workflow syntax/static checks needed?
- Are Azure dry-run/read-only checks possible?
- What must be validated interactively by the user because authenticated Azure/Docker context is unavailable to Terra sandbox execution?

## 9. Cross-WP boundaries

### WP04 — SQLite persistence
Do not authorize implementation of:
- DB initialization/recovery workflows;
- SQLite schema changes;
- database migration;
- persistence recovery logic;
- journal-mode implementation changes.

WP03 may only carry deployment settings necessary to mount/use persistent `/home` if that is required to deploy the container boundary.

### WP05 — Twelve Data
Do not authorize:
- API-key provisioning workflow;
- provider secret storage implementation;
- provider data-update automation;
- bounded provider scheduling.

WP03 may define generic secret/app-setting plumbing only if strictly necessary for deployment architecture and if it does not implement WP05 behavior.

### WP06 — Streamlit/System Health
Do not authorize presentation/diagnostic behavior changes.

### WP07
Do not authorize final cost/stability/recovery qualification tooling unless required merely to validate WP03 mechanics.

### WP08
Do not authorize final runbook/release documentation unless a small directly-WP03 artifact is indispensable to operate the automation.

## 10. Path-operation taxonomy

Every designated path must have exactly one operation:

- `CREATE`
- `MODIFY`
- `DELETE`
- `RENAME_FROM`
- `RENAME_TO`

For existing paths, prove current repository existence.

For new paths, prove nonexistence on canonical `main`.

No category-only authorization is valid.

## 11. Candidate path categories Luna must decide

Luna must explicitly decide whether WP03 needs literal paths in each category:

1. GHCR workflow
2. GHCR/operator publication script
3. Azure F1 deployment script
4. Azure F1 configuration/app-settings script
5. deployment validation script
6. deployment configuration/template file
7. Docker/container file modification
8. GitHub Actions/workflow file
9. test file
10. direct WP03 documentation
11. shared engineering helper
12. any other required category

For every category output either:
- exact literal path(s), or
- `NO WP03 PATH REQUIRED`

## 12. Preferred path-design principles

Prefer:
- Release 1.12-specific paths;
- small bounded scripts;
- parameterized resource names;
- public GHCR;
- no committed secrets;
- no ACR dependency;
- no paid Azure dependencies;
- no architecture bypass;
- deterministic/reproducible operator commands;
- idempotent or safely re-runnable automation where feasible;
- explicit create/update/delete boundaries.

Avoid:
- mutating historical `r1.11-qualification` evidence;
- root-level ad hoc scripts;
- embedding subscription IDs;
- embedding tenant IDs;
- embedding actual API keys/tokens;
- coupling WP03 to WP04/WP05 behavior;
- broad CI/CD machinery not required by the release.

## 13. Interactive execution handoff requirement

The project has a binding operator handoff for authenticated local execution:

- Windows user executes Docker/WSL/Azure CLI commands interactively when required;
- no Docker pipe ACL/security weakening;
- no Azure auth cache/token/profile transfer;
- Terra must provide exact copy/paste commands;
- every command must be mutation-classified;
- expected evidence/mutations must be stated;
- Terra must STOP and wait for stdout/stderr plus exit codes;
- user-executed mutations count in the audit.

Luna must designate repository paths that support this model.

Do not assume cloud execution from the model sandbox.

## 14. Literal allowlist contract

At completion, emit:

`WP03_AUTHORIZED_PATH_SET`

as an exact sorted literal block.

No:
- globs;
- wildcards;
- directory-only entries;
- `...`;
- “related files”;
- future unspecified paths.

For each path include:
- operation;
- rationale;
- WP03 ownership;
- whether production/config/script/test/docs;
- whether interactive operator execution is expected.

## 15. Explicit denylist

Emit:

`WP03_EXPLICIT_DENY_SET`

At minimum consider denying:
- frozen Release 1.12 planning docs;
- historical Initiative-1.11 qualification scripts;
- WP02 container files unless modification is explicitly necessary;
- WP04/WP05/WP06 implementation paths;
- application pipeline source;
- Python presentation/read-model source;
- requirements/package files;
- schema/migration files;
- unrelated prompters/governance controls.

A denylist path/category may be omitted only if Luna clearly explains why it is irrelevant.

## 16. Mutation counts

Report:

- `AUTHORIZED_PATH_COUNT`
- `EXISTING_PATH_COUNT`
- `CREATE_PATH_COUNT`
- `MODIFY_PATH_COUNT`
- `DELETE_PATH_COUNT`
- `RENAME_PATH_COUNT`
- `PRODUCTION_SOURCE_PATH_COUNT`
- `TEST_PATH_COUNT`
- `CONFIG_PATH_COUNT`
- `SCRIPT_PATH_COUNT`
- `WORKFLOW_PATH_COUNT`
- `DOC_PATH_COUNT`

Counts must reconcile exactly to the literal set.

## 17. Publication requirement for path designation

Decide exactly one:

`WP03 PATH DESIGNATION PUBLICATION REQUIREMENT: REQUIRED`

or

`WP03 PATH DESIGNATION PUBLICATION REQUIREMENT: NOT REQUIRED`

Default expectation: the designation itself is governance output and need not be committed unless it changes repository source-of-record governance.

Do not create a publication requirement merely because WP02 had a separate designation artifact.

## 18. Terra mutation contract

If path designation passes, emit exactly:

`GPT-5.6 TERRA WP03 MUTATION CONTRACT: ONLY WP03_AUTHORIZED_PATH_SET MAY BE MUTATED`

Terra must embed the literal allowlist verbatim into the later implementation authority.

No new path may be invented by Terra.

If implementation discovers a necessary unlisted path, Terra must STOP and return to Luna for amendment.

## 19. Required validation-contract design

Luna must define the minimum WP03 implementation acceptance contract, including at least:

- exact path-set compliance;
- Git diff/whitespace checks;
- Gitleaks/secret screening;
- GHCR publication mechanism validation;
- image naming/tagging validation;
- Azure App Service F1 deployment automation validation;
- F1 tier/region/public GHCR/no-ACR constraint validation;
- no committed secrets;
- no WP04/WP05/WP06 bypass;
- no paid-resource dependency;
- operator handoff evidence for authenticated execution;
- exact mutation audit.

Do not execute those mutations here.

## 20. Lifecycle boundary

This authority must not mutate:
- #262;
- Project #2 Status;
- milestone #63;
- Release assignments.

WP03 remains Open/Todo until Terra implementation passes exact acceptance and a later merge/post-merge/lifecycle authority closes #262 and confirms Done.

## 21. Read-only mutation prohibition

Authorized:
- file reads;
- Git reads;
- GitHub reads;
- repository searches;
- hashes/diffs/counts;
- read-only secret screening;
- read-only inspection of historical evidence.

Not authorized:
- repository file writes;
- staging;
- commits;
- branches;
- pushes;
- PRs;
- merges;
- issue/Project/milestone mutation;
- Docker execution;
- Azure mutation;
- GHCR mutation;
- provider calls;
- package/schema changes.

## 22. Required output structure

The final Luna report must include:

1. canonical base reconciliation;
2. frozen-plan findings;
3. repository/deployment-structure findings;
4. historical Initiative-1.11 boundary;
5. per-category path decisions;
6. exact `WP03_AUTHORIZED_PATH_SET`;
7. exact `WP03_EXPLICIT_DENY_SET`;
8. mutation counts;
9. interactive handoff requirements;
10. WP03 implementation validation contract;
11. path-designation publication requirement;
12. Terra mutation contract;
13. read-only mutation audit.

## 23. Acceptance gates

### Gate A — canonical base
Current main state proven.

### Gate B — frozen planning alignment
WP03 scope aligns with Release 1.12 planning.

### Gate C — empirical repository fit
Paths derive from actual repository structure.

### Gate D — historical evidence protection
Initiative-1.11 evidence is not silently converted into mutable Release 1.12 implementation.

### Gate E — cross-WP isolation
WP04–WP08 ownership boundaries preserved.

### Gate F — exact literal closure
Every implementation path is literal and operation-scoped.

### Gate G — zero-secret/zero-paid dependency design
No credential or paid-resource architecture is introduced.

### Gate H — operator handoff
Authenticated Azure/GHCR execution boundary is explicit.

### Gate I — Terra contract
A later Terra authority can execute without inventing paths.

### Gate J — read-only audit
No mutation occurred.

## 24. Required markers

`RELEASE 1.12 WP03 — CANONICAL BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP03 — FROZEN PLANNING ALIGNMENT: PASS`

`RELEASE 1.12 WP03 — REPOSITORY STRUCTURE RECONCILIATION: PASS`

`RELEASE 1.12 WP03 — INITIATIVE-1.11 HISTORICAL BOUNDARY: PASS`

`RELEASE 1.12 WP03 — CROSS-WP ISOLATION: PASS`

`RELEASE 1.12 WP03 — PATH CATEGORY DECISIONS: PASS`

`RELEASE 1.12 WP03 — LITERAL FILE ALLOWLIST: PASS`

`RELEASE 1.12 WP03 — EXPLICIT DENYLIST: PASS`

`RELEASE 1.12 WP03 — MUTATION COUNT RECONCILIATION: PASS`

`RELEASE 1.12 WP03 — INTERACTIVE OPERATOR HANDOFF CONTRACT: PASS`

`RELEASE 1.12 WP03 — IMPLEMENTATION VALIDATION CONTRACT: PASS`

`RELEASE 1.12 WP03 — PATH DESIGNATION PUBLICATION DECISION: PASS`

`RELEASE 1.12 WP03 — READ-ONLY MUTATION AUDIT: PASS`

Acceptance:

`RELEASE 1.12 WP03 — REPOSITORY PATH DESIGNATION: PASS`

Terra handoff:

`GPT-5.6 TERRA WP03 MUTATION CONTRACT: ONLY WP03_AUTHORIZED_PATH_SET MAY BE MUTATED`

`RELEASE 1.12 WP03 — IMPLEMENTATION AUTHORITY: READY TO CREATE`

Terminal:

`RELEASE 1.12 WP03 — REPOSITORY PATH DESIGNATION AUTHORITY COMPLETE`

If exact literal closure cannot be achieved:

`RELEASE 1.12 WP03 — REPOSITORY PATH DESIGNATION AUTHORITY BLOCKED`

State the unresolved category/path requirement precisely and do not authorize Terra implementation.

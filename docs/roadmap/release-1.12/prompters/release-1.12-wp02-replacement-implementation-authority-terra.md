# GPT-5.6 Terra — Release 1.12 WP02 Replacement Implementation Authority
## Productionized Container & Runtime Composition

**Selected execution model: GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, and literal repository-path designation.
- **GPT-5.6 Terra** — PRIMARY for this authority: implementation and validation within the exact Luna-designated path contract, approved Git/PR mutations, Docker execution, and WP lifecycle completion when all gates are satisfied.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

---

## 1. Authority replacement

This authority **replaces the prior category-based GPT-5.6 Terra WP02 implementation authority**.

The prior authority MUST NOT be used independently for mutation because it did not embed the literal path allowlist required by the frozen Release 1.12 manifest.

This replacement authority incorporates the completed Luna governance amendment and is the controlling implementation authority for:

`#261 — Release 1.12 WP02: Productionized Container & Runtime Composition`

---

## 2. Canonical starting state

Expected canonical implementation base:

`d63f8748772f579f2c46cf79df3563627b31a958`

Expected state:
- local `main` = `origin/main`, ahead/behind `0/0`;
- Release 1.12 planning artifacts published on `main`;
- #260 = Closed/Done;
- #261 = Open/Todo;
- #262 = Open/Todo and dependency-gated;
- milestone #63 = Open, 7 open / 1 closed;
- Initiative-1.11 unchanged;
- `v1.10.0` remains anchored to `eb9601596d9a9dd68f1f8a7c963906a76e5a2833`.

Fresh reconciliation controls if state has changed externally.

---

## 3. Binding Luna governance amendment

The completed Luna designation established:

`RELEASE 1.12 WP02 PATH DESIGNATION: LUNA GOVERNANCE AMENDMENT`

`THE FROZEN RELEASE 1.12 MANIFEST REMAINS HISTORICAL; THIS AUTHORITY SUPPLIES THE SEPARATELY REQUIRED WP02 LITERAL PATH CONTRACT`

Publication decision:

`WP02 PATH DESIGNATION PUBLICATION REQUIREMENT: NOT REQUIRED`

Binding mutation rule:

`GPT-5.6 TERRA WP02 MUTATION CONTRACT: ONLY WP02_AUTHORIZED_PATH_SET MAY BE MUTATED`

This replacement Terra authority embeds that path set verbatim below.

---

## 4. WP02_AUTHORIZED_PATH_SET — BINDING

```text
.dockerignore
Dockerfile
container/entrypoint.sh
```

`AUTHORIZED_PATH_COUNT=3`

All three paths were designated by Luna as:
- currently nonexistent at the designation base;
- `CREATE` only;
- mandatory for WP02.

### Per-path authorization

| Literal path | Authorized operation | Classification | WP02 responsibility |
|---|---|---|---|
| `.dockerignore` | `CREATE` | Container definition | Exclude secrets, caches, build outputs, and unrelated content from Docker build context |
| `Dockerfile` | `CREATE` | Container definition | Define reproducible Linux multi-stage runtime composition |
| `container/entrypoint.sh` | `CREATE` | Runtime supervision script | Start/supervise approved runtime processes; readiness/failure behavior; signal handling; child cleanup |

### Absolute closure rule

Terra MAY mutate only these three literal paths.

Terra MUST BLOCK before mutation of any other tracked or untracked repository path.

This includes:
- generated tracked files;
- formatter-generated files;
- lockfiles;
- test snapshots;
- config files;
- documentation;
- project files;
- package manifests;
- temporary repository files.

If implementation requires any fourth repository path, STOP and request a new GPT-5.6 Luna path-designation amendment.

---

## 5. Explicit denied paths

The Luna designation explicitly denied WP02 mutation of:

```text
docker-compose.yml
src/AIQuantTradingResearch.Worker/Program.cs
src/AIQuantTradingResearch.Infrastructure/PythonIntegration/PythonCapabilityInvoker.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
python/presentation/realtime_financial_visualization.py
python/presentation/visualization_read_model.py
requirements.txt
eng/azure-cli/wp02-azure-cli-docker-app-service/check-docker-6.ps1
eng/azure-cli/wp02-azure-cli-docker-app-service/check-docker-17.ps1
eng/azure-cli/wp03-azure-cli-docker-sqlite/check-sqlite-01.ps1
eng/azure-cli/wp04-azure-cli-docker-conn-12-data/check-conn-01.ps1
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
```

The denylist is not exhaustive. Any repository path absent from `WP02_AUTHORIZED_PATH_SET` is forbidden.

Pre-existing untracked `prompters/` content remains excluded.

---

## 6. Binding architecture

Implementation MUST preserve:
- Azure App Service Linux F1 reference target;
- West Central US;
- custom Linux Docker;
- public/default HTTPS/DNS downstream;
- persistent `/home` deployment boundary;
- configuration-driven writable SQLite;
- SQLite DELETE journal selection;
- public/free GHCR downstream;
- strict recurring infrastructure cost `$0.00`;
- reference/demo-only claims;
- no production SLA/HA claims;
- .NET canonical pipeline ownership;
- atomic JSON handoff;
- Python parser → frame → presentation → Streamlit;
- Streamlit no SQLite/provider/Worker-supervision ownership;
- Release 1.8 JSON-over-stdio separation;
- Release 1.10 observability/System Health truthfulness;
- deterministic/replay/simulated provenance.

WP02 MUST NOT introduce Azure SQL, Azure Files, Container Apps, mandatory ACR, paid services, ML, backtesting, live trading, schema migration, or architecture bypass.

---

## 7. Implementation objective

Using only the three authorized new files, implement a productionized Linux container/runtime composition that:

- builds the existing application/runtime without modifying application source;
- starts the approved .NET Worker/pipeline and Python/Streamlit presentation processes;
- preserves the existing configuration-driven JSON handoff;
- preserves existing SQLite configuration ownership without implementing WP04 persistence behavior;
- exposes the required Streamlit listener;
- provides deterministic startup and process supervision;
- surfaces unrecoverable child-process failure truthfully;
- handles container termination signals;
- cleans up child processes;
- does not require provider connectivity merely to start;
- contains no secrets/auth caches;
- remains suitable for later Azure F1 deployment by WP03+.

---

## 8. Dockerfile requirements

`Dockerfile` must, within the existing repository architecture:

- target Linux;
- use deterministic/governed runtime and build images compatible with the repository's current .NET/Python requirements;
- use multi-stage construction where appropriate;
- restore/build/publish the existing .NET application without editing project files;
- prepare the existing Python runtime without modifying `requirements.txt`;
- include only runtime content required by the approved composition;
- copy `container/entrypoint.sh`;
- establish deterministic working directories;
- define the expected public listener/port behavior;
- invoke the designated entrypoint;
- avoid credentials, auth caches, tokens, local `.env`, and developer-only state;
- not require Azure credentials;
- not require GHCR/ACR logic;
- not hard-code provider secrets;
- not implement WP03 deployment automation.

If a required package cannot be installed from existing governed repository inputs without modifying another path, BLOCK.

---

## 9. .dockerignore requirements

`.dockerignore` must minimize build context and prevent accidental inclusion of inappropriate local content.

It should exclude, where empirically applicable:
- `.git`;
- local IDE/editor state;
- build outputs;
- test/result caches;
- Python caches/virtual environments;
- local secrets/environment files;
- Azure/GitHub/Docker credential material if present in the repository working context;
- unrelated local/untracked `prompters/`;
- other non-runtime local artifacts.

Do not exclude files actually required by the Docker build.

No other ignore/config file may be modified.

---

## 10. Entrypoint/supervision requirements

`container/entrypoint.sh` must be a narrow deployment-composition script, not application business logic.

It must:
- be suitable for the Linux image;
- start the existing required .NET and Streamlit processes using existing application/configuration seams;
- avoid transferring application ownership to Streamlit;
- preserve JSON handoff ownership;
- use environment/configuration already supported by the application;
- expose failures rather than silently restarting forever;
- propagate/handle termination signals;
- terminate child processes on shutdown;
- return a failing exit code for unrecoverable required-child failure;
- avoid orphan processes;
- produce useful stdout/stderr diagnostics without exposing secrets;
- avoid provider calls of its own;
- avoid Azure CLI/SDK logic;
- avoid persistence initialization/recovery logic owned by WP04.

If existing applications cannot be composed correctly without source/config changes outside the three paths, BLOCK rather than widening scope.

---

## 11. No-test-file rule

Luna designated:

- no WP02 test path required;
- existing test projects provide applicable seams;
- external/container validation is sufficient for WP02-specific composition behavior.

Therefore Terra MUST NOT create or modify repository test files.

Validation may execute existing tests read-only.

---

## 12. No-documentation-file rule

Luna designated no WP02 documentation mutation.

Do not modify planning artifacts, README files, runbooks, or release documentation.

WP08 retains documentation ownership.

---

## 13. No-config/source mutation rule

Luna designated no WP02 path for:
- Streamlit server configuration;
- .NET runtime configuration;
- JSON handoff configuration;
- SQLite path configuration;
- additional container health support.

Therefore implementation must consume existing seams through Dockerfile/entrypoint runtime invocation only.

If this proves impossible, BLOCK.

---

## 14. Docker execution handoff

The user's authenticated/interactive Windows environment is the valid Docker Desktop Linux-engine execution context.

When real Docker execution is required, Terra must provide exact copy/paste PowerShell commands in bounded batches.

Each batch must state:
1. purpose;
2. exact commands;
3. classification:
   - `READ-ONLY`
   - `DOCKER LOCAL MUTATION`
   - `LOCAL GIT MUTATION`
   - `GITHUB MUTATION`
4. expected mutations;
5. exact stdout/stderr/exit-code evidence required;
6. STOP and wait.

Never infer Docker results.

No Docker pipe ACL/security weakening is authorized.

---

## 15. Required validation

### Repository/path validation
- prove starting working-tree state;
- identify pre-existing unrelated untracked files;
- after implementation prove the only repository mutations are the exact three authorized paths;
- `git diff --check`;
- no staged mutation before explicit staging phase.

### Existing application validation
Execute established relevant validation without modifying tests:
- .NET build;
- existing relevant .NET tests;
- architecture tests;
- Python tests;
- `pip check` if applicable without environment mutation beyond the approved validation environment;
- Gitleaks.

Use the repository's current canonical commands discovered from existing governance/scripts/docs.

### Docker build
Prove a clean image build succeeds.

Record:
- exact build command;
- image identifier/tag used locally;
- exit code;
- material warnings/errors.

### Runtime
Prove:
- container starts;
- required .NET process starts;
- Streamlit process starts;
- expected listener is reachable;
- startup does not require an Azure credential;
- startup does not require a provider request;
- JSON handoff/runtime composition does not bypass existing ownership;
- configuration remains externally supplied.

### Failure behavior
Using safe local conditions, prove required-child failure is surfaced truthfully and does not leave the container falsely healthy.

Do not mutate application source/config to induce failure.

### Shutdown/residue
Prove:
- graceful container stop;
- entrypoint receives/handles termination;
- child processes terminate;
- no container/process/listener residue remains.

### Secret/image safety
Inspect:
- image configuration;
- image history;
- relevant runtime logs;
- build context implications.

Prove no real secrets, auth caches, tokens, or credentials are embedded/disclosed.

### Scope
Prove:
- Azure mutations = 0;
- GHCR mutations = 0;
- provider requests = 0;
- package manifest changes = 0;
- schema changes = 0;
- repository mutations outside three paths = 0.

---

## 16. Provider and Azure prohibition

WP02 does not authorize:
- Twelve Data requests;
- Azure login/resource operations;
- Azure App Service creation/configuration;
- GHCR push/publication;
- ACR;
- Azure Files;
- Azure SQL;
- deployment automation.

Docker validation is local only.

---

## 17. Git workflow

Only after implementation and all pre-PR validations pass:

1. inspect `git status --short`;
2. prove repository mutation set equals exactly the intended subset of the three authorized paths;
3. selectively stage literal paths only;
4. NEVER use `git add .` or `git add -A`;
5. prove staged-set equality;
6. inspect `git diff --cached`;
7. run `git diff --cached --check`;
8. commit;
9. push dedicated branch;
10. create one non-draft PR to `main`;
11. verify PR payload exactly matches the staged authorized path set.

Preferred branch:

`feat/release-1.12-wp02-container-runtime`

Preferred commit:

`Release 1.12 WP02: productionize container runtime composition`

Preferred PR title:

`Release 1.12 WP02: Productionized Container & Runtime Composition`

No force push.

---

## 18. PR merge authority

This replacement authority authorizes implementation, validation, branch/commit/push, and creation of the WP02 implementation PR.

**It does not authorize merging the WP02 implementation PR.**

After verified PR creation emit:

`RELEASE 1.12 WP02 — IMPLEMENTATION PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

A separate narrow GPT-5.6 Terra merge + post-merge verification authority is required.

Accordingly, #261 MUST remain Open/Todo after this authority stops at the verified PR boundary.

---

## 19. Lifecycle boundary

Do NOT close #261 under this authority unless a later explicit authority expands the boundary to include merge/post-merge/lifecycle completion.

The governed sequence is:

1. this replacement Terra authority implements/validates and creates exact WP02 PR;
2. separate Terra authority merges and verifies it;
3. only after exact WP02 acceptance on merged `main`, close #261;
4. allow Project automation to set Done;
5. explicitly set Done only if automation does not;
6. verify milestone #63 remains Open;
7. verify #262 becomes next-ready.

Never close #261 before merged acceptance.

---

## 20. Mutation accounting

Report exact counts for:
- repository files created;
- repository files modified;
- repository files deleted;
- temporary files created/removed;
- Docker image builds;
- local container runs;
- branches;
- staging mutations;
- commits;
- pushes;
- PRs;
- PR merges;
- issue mutations;
- Project mutations;
- milestone mutations;
- tag/release mutations;
- Azure mutations;
- GHCR mutations;
- provider requests;
- package changes;
- schema changes.

Expected repository path ceiling: 3.

Expected protected domains:
- PR merge = 0
- issue = 0
- Project = 0
- milestone = 0
- tag/release = 0
- Azure = 0
- GHCR = 0
- provider = 0
- package manifest = 0
- schema = 0

---

## 21. Stop conditions

Immediately BLOCK if:
- canonical base/governance cannot be reconciled;
- any authorized path unexpectedly already exists with unreconciled content;
- a fourth repository path is required;
- an authorized path requires an operation other than `CREATE`;
- application source/config must change;
- a package manifest must change;
- a schema change is needed;
- Docker composition would bypass .NET/Python/Streamlit ownership;
- provider connectivity is required merely to start;
- secrets would need to enter the image;
- real Docker validation cannot be executed/proven;
- exact PR payload cannot be proven.

Do not improvise around a stop condition.

---

## 22. Acceptance gates

### Gate A — base/governance reconciliation
Canonical base, planning publication, Luna amendment, and #261 state proven.

### Gate B — literal path contract
Exact three-path `CREATE`-only allowlist acknowledged before mutation.

### Gate C — implementation
All required composition implemented using only those paths.

### Gate D — architecture/no-bypass
Existing ownership and cross-WP boundaries preserved.

### Gate E — existing test suite
Relevant build/tests/Gitleaks pass without test/source mutation.

### Gate F — real Docker validation
Build/start/listener/process/failure/shutdown/residue behavior proven.

### Gate G — secret isolation
Build context/image/history/logs are clean.

### Gate H — mutation-set closure
Repository mutations remain a subset of exactly the three authorized paths.

### Gate I — Git/PR integrity
Selective staging, commit, push, and non-draft PR payload proven exact.

### Gate J — mutation audit
All mutations accounted for and protected domains remain zero.

---

## 23. Required markers

`RELEASE 1.12 WP02 — REPLACEMENT AUTHORITY BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP02 — LUNA LITERAL PATH CONTRACT: PASS`

`RELEASE 1.12 WP02 — AUTHORIZED PATH MUTATION CLOSURE: PASS`

`RELEASE 1.12 WP02 — PRODUCTIONIZED CONTAINER DEFINITION: PASS`

`RELEASE 1.12 WP02 — RUNTIME COMPOSITION & SUPERVISION: PASS`

`RELEASE 1.12 WP02 — ARCHITECTURE & NO-BYPASS: PASS`

`RELEASE 1.12 WP02 — EXISTING BUILD & TEST VALIDATION: PASS`

`RELEASE 1.12 WP02 — LOCAL DOCKER BUILD & RUNTIME VALIDATION: PASS`

`RELEASE 1.12 WP02 — CONTAINER SECRET ISOLATION: PASS`

`RELEASE 1.12 WP02 — GIT/PR PAYLOAD VERIFICATION: PASS`

`RELEASE 1.12 WP02 — MUTATION AUDIT: PASS`

Implementation acceptance:

`RELEASE 1.12 WP02 — PRODUCTIONIZED CONTAINER & RUNTIME COMPOSITION: PASS`

Required boundary:

`RELEASE 1.12 WP02 — IMPLEMENTATION PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

Next governance action:

`RELEASE 1.12 WP02 — MERGE + POST-MERGE VERIFICATION AUTHORITY: READY TO CREATE`

Terminal:

`RELEASE 1.12 WP02 — REPLACEMENT IMPLEMENTATION AUTHORITY COMPLETE`

On failure:

`RELEASE 1.12 WP02 — REPLACEMENT IMPLEMENTATION AUTHORITY BLOCKED`

State the exact failed gate and perform no lifecycle closure.

---

## 24. Completion boundary

This authority completes at a validated, pushed, exact-scope, non-draft WP02 implementation PR.

It does not merge that PR and does not close #261.

The implementation is constrained absolutely to:

```text
.dockerignore
Dockerfile
container/entrypoint.sh
```

Any expansion requires a new GPT-5.6 Luna governance amendment.

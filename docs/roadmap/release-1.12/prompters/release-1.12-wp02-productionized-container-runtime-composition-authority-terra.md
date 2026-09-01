# GPT-5.6 Terra — Release 1.12 WP02: Productionized Container & Runtime Composition
## Execution Authority

**Selected execution model: GPT-5.6 Terra**

### Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, planning.
- **GPT-5.6 Terra** — PRIMARY for implementation, validation execution, approved repository/Git/GitHub mutations, container build work, and lifecycle completion under this authority.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna or Terra.

## Mission
Execute Release 1.12 WP02: `Productionized Container & Runtime Composition`.

WP02 turns the frozen Release 1.12 deployment/runtime contract into the productionized container/runtime composition required by downstream deployment work.

## Required starting state
Freshly reconcile:
- WP01 #260 Closed/Done.
- WP02 #261 Open/Todo.
- milestone #63 Open, 7 open / 1 closed.
- #262–#267 dependency-gated.
- Product Release 1.11 abandoned/nonexistent.
- release sequence `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`.
- Initiative-1.11 and Release 1.10 historical governance unchanged.

Expected planning publication:
- PR #268: `Docs: publish Release 1.12 planning artifacts`
- branch `docs/release-1.12-planning-artifacts`
- commit `917207333f133b961f72b94525a17ed0d0aae954`
- parent `20a8fccd6e7a5b895e717f946f4501edd7ab8ffa`
- last reported Open, non-draft, exact 3/3 Markdown payload.

## Binding publication gate
WP02 MUST NOT implement from a stale `main` that lacks the frozen Release 1.12 planning contract.

Before implementation, verify either:
1. PR #268 has been merged and local/origin `main` contain the exact three planning artifacts; or
2. a higher explicit authority has reconciled an equivalent publication boundary preserving the frozen contract.

If PR #268 remains Open with no higher reconciliation, BLOCK before production/source mutation.

This authority does not authorize merging PR #268.

## Binding architecture
Preserve:
- Azure App Service Linux F1, West Central US.
- custom Docker.
- public/default HTTPS/DNS.
- persistent `/home`.
- configuration-driven writable SQLite; DELETE journal selected.
- public/free GHCR.
- bounded Twelve Data connectivity.
- strict recurring infrastructure cost `$0.00`.
- reference/demo-only claims; no production SLA/HA claims.
- .NET pipeline ownership.
- atomic JSON handoff to Python.
- Python parser → frame → presentation → Streamlit.
- Streamlit does not own SQLite/provider/Worker supervision.
- Release 1.8 JSON-over-stdio remains separate.
- Release 1.10 System Health/observability remains truthful.
- deterministic/replay/simulated provenance remains truthful.

If implementation requires violating any invariant, BLOCK for Luna reconciliation.

## WP02 objective
Implement a deterministic Linux container/runtime composition suitable for downstream WP03 deployment automation, without Azure resource mutation or provider-side execution.

Prove:
- exact runtime processes;
- startup order/supervision;
- public listener/port;
- internal communication;
- readiness/failure semantics;
- graceful shutdown;
- deterministic filesystem paths;
- persistent vs ephemeral storage boundaries;
- environment/configuration inputs;
- reproducible build;
- secret-safe image;
- local compatibility;
- bounded behavior suitable for F1;
- no hidden Azure dependency in application code.

## Runtime composition
Explicitly account for:
- .NET Worker/pipeline process;
- canonical JSON handoff producer;
- Python/Streamlit process;
- startup/supervision wrapper if needed;
- handoff paths;
- stdout/stderr/log ownership;
- signal handling;
- child cleanup;
- public port exposure.

Supervision must:
- have one clear container entrypoint;
- fail closed on unrecoverable startup errors;
- not conceal child-process failures;
- avoid orphan/background residue;
- terminate children on shutdown;
- preserve useful diagnostics;
- never make Streamlit owner/supervisor of .NET application responsibilities.

## Container contract
Implement/reconcile the production container definition:
- Linux target.
- deterministic build context.
- explicit base image(s).
- governed runtime/tooling versions.
- no unnecessary build/runtime packages.
- multi-stage build where appropriate.
- no credentials/secrets/auth caches in layers.
- exclude nonessential source/test artifacts from runtime image where practical.
- deterministic working directories.
- explicit runtime-user decision.
- explicit listening port.
- repository-owned entrypoint/start command.
- `/home` treated as Azure persistent boundary, not assumed during local build.
- SQLite path remains configuration-driven.

Do not introduce mandatory ACR, Azure SQL, Azure Files, Container Apps, paid services, or Azure-specific application ownership.

## Configuration/environment
Implement only the required configuration surface, including as applicable:
- runtime mode;
- public port;
- visualization/read-model handoff path;
- SQLite path;
- logging/diagnostics;
- Twelve Data secret variable name only;
- downstream data-update control inputs;
- Streamlit/server binding.

Rules:
- no secret defaults;
- no real `.env`;
- no provider token in image;
- no GitHub/Azure/Docker auth material;
- missing secrets must fail safely;
- defaults remain safe for local development.

## Filesystem contract
Distinguish:
1. immutable image content;
2. ephemeral container filesystem;
3. persistent Azure `/home`;
4. generated JSON/read-model handoff;
5. SQLite DB;
6. optional persisted diagnostics;
7. temporary/runtime files.

Do not assume `/home` persistence in ordinary local Docker execution without an explicit mount.

## Health/readiness
Implement only container/runtime health allocated to WP02.

Distinguish:
- process started;
- required child failed;
- listener unavailable;
- startup timeout/failure;
- required handoff path unavailable.

Do not implement WP06 public System Health presentation or claim provider/data freshness from mere container liveness.

## Reproducible local validation
Provide exact commands for:
- clean build;
- image build;
- local run;
- optional volume mounts;
- safe placeholder environment injection;
- listener verification;
- process verification;
- graceful stop;
- residue check.

Docker Desktop Linux engine/WSL2 execution may require the user's interactive Windows PowerShell context. No Docker pipe ACL/security weakening is authorized.

## Repository mutation surface
First reconcile the frozen WP01 file manifest and state the exact WP02-owned path set before editing.

Allowed only if assigned to WP02:
- `Dockerfile`/container definitions;
- `.dockerignore`;
- narrow entrypoint/supervision scripts;
- deployment/runtime configuration;
- tests for container/runtime composition;
- WP02 documentation/manifest updates;
- minimal startup/configuration code strictly required by the contract.

Forbidden without Luna reconciliation:
- trading logic;
- ML/backtesting;
- unrelated domain/business code;
- unrelated schema;
- WP03 Azure provisioning;
- WP04 persistence initialization/recovery;
- WP05 provider automation;
- WP06 public System Health UI;
- WP08 final runbook/release work;
- unrelated refactors;
- Release 2.0+ scope;
- Initiative-1.11 historical evidence.

## Dependency policy
No package/dependency addition for convenience.

Any new dependency must be necessary, minimal, policy-compatible, justified, and validated. Material unplanned dependency expansion requires Luna reconciliation.

## Validation matrix
Run the repository's established relevant validation:
- `git diff --check`;
- relevant .NET build;
- affected .NET tests;
- affected Python tests;
- architecture tests;
- Gitleaks;
- `pip check` if Python dependencies/environment touched;
- version checks where relevant;
- real container build;
- container start;
- local listener check;
- process-topology verification;
- child-failure behavior;
- graceful stop;
- zero residual process/listener;
- image/history secret scan;
- runtime log/secret disclosure scan;
- exact changed-path ownership audit.

Never infer Docker validation when it has not executed.

## Container acceptance evidence
Prove:
- image builds from clean/reconciled state;
- image starts with documented safe inputs;
- required processes start;
- listener is reachable;
- startup failures surface truthfully;
- no orphan residue after stop;
- paths match contract;
- SQLite path remains configurable;
- `/home` is not hard-coded as local-only behavior;
- provider call is not required just to start;
- no secret embedded in image/history/layers;
- no Azure credentials required;
- no paid component required.

Do not push to GHCR under WP02 unless the frozen manifest explicitly assigns image publication to WP02. Default GHCR publication ownership is WP03.

## Git workflow
After implementation/validation:
- inspect the full working tree;
- selectively stage only WP02-owned paths;
- prove staged-set equality;
- inspect cached diff;
- run `git diff --cached --check`;
- create a coherent WP02 implementation commit;
- push a dedicated branch;
- create a non-draft PR to `main`;
- verify exact payload/base/head.

Preferred branch:
`feat/release-1.12-wp02-container-runtime`

Preferred commit:
`Release 1.12 WP02: productionize container runtime composition`

Preferred PR title:
`Release 1.12 WP02: Productionized Container & Runtime Composition`

No force push.

## Merge boundary
Implementation PR merge is NOT automatically authorized by implementation success.

If no separate explicit merge grant exists, stop at a verified open PR and emit:

`RELEASE 1.12 WP02 — IMPLEMENTATION PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

If separately authorized, perform exact post-merge verification before lifecycle closure.

## Lifecycle
Issue #261 MUST NOT close before the exact WP02 acceptance marker and any required post-merge verification.

After true WP02 completion:
1. close #261;
2. allow Project automation to set Done;
3. if automation does not, explicitly set Done;
4. count only explicit mutations;
5. verify milestone #63 remains Open;
6. verify #262 is next-ready.

Do not close #262–#267.

## Mutation accounting
Report exact counts for:
- repository files;
- temporary files;
- branches;
- staging/index;
- commits;
- pushes;
- PRs;
- merges;
- issue mutations;
- Project mutations;
- milestone mutations;
- tags/releases;
- Docker image builds;
- local container runs;
- GHCR mutations;
- Azure mutations;
- provider requests;
- package changes;
- schema changes;
- production/source changes.

Expected:
- Azure 0
- provider 0
- milestone 0
- tag/release 0
- GHCR publication 0 unless explicitly assigned
- issue closure exactly 1 only after acceptance
- Project Status mutation 0 if automation sets Done

## Interactive handoff
For Docker/Windows-local work, provide one bounded batch at a time with:
1. purpose;
2. exact copy/paste PowerShell;
3. classification (`READ-ONLY`, `LOCAL REPOSITORY MUTATION`, `LOCAL GIT MUTATION`, `DOCKER LOCAL MUTATION`, `GITHUB MUTATION`);
4. expected mutations;
5. exact stdout/stderr/exit codes required;
6. STOP and wait.

Never infer success or request credential/auth-cache transfer.

## Acceptance gates
A. Publication/base reconciliation.
B. Exact WP02-owned manifest.
C. Container implementation.
D. Runtime supervision.
E. Configuration/filesystem correctness.
F. Secret/security isolation.
G. Build/test/container validation.
H. No-bypass/no-scope-drift.
I. Exact Git/PR payload.
J. Mutation audit.
K. Lifecycle closure.

## Required markers
`RELEASE 1.12 WP02 — PUBLICATION & BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP02 — OWNED FILE MANIFEST: PASS`

`RELEASE 1.12 WP02 — PRODUCTIONIZED CONTAINER DEFINITION: PASS`

`RELEASE 1.12 WP02 — RUNTIME COMPOSITION & SUPERVISION: PASS`

`RELEASE 1.12 WP02 — CONFIGURATION & FILESYSTEM CONTRACT: PASS`

`RELEASE 1.12 WP02 — CONTAINER SECURITY & SECRET ISOLATION: PASS`

`RELEASE 1.12 WP02 — LOCAL CONTAINER BUILD & RUNTIME VALIDATION: PASS`

`RELEASE 1.12 WP02 — NO-BYPASS & SCOPE CONTROL: PASS`

`RELEASE 1.12 WP02 — GIT/PR PAYLOAD VERIFICATION: PASS`

`RELEASE 1.12 WP02 — MUTATION AUDIT: PASS`

WP acceptance:
`RELEASE 1.12 WP02 — PRODUCTIONIZED CONTAINER & RUNTIME COMPOSITION: PASS`

If merge not authorized:
`RELEASE 1.12 WP02 — IMPLEMENTATION PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

After actual completion:
`RELEASE 1.12 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`

Next:
`RELEASE 1.12 WP03 — EXECUTION AUTHORITY: READY`

Terminal:
`RELEASE 1.12 WP02 — EXECUTION AUTHORITY COMPLETE`

Blocked:
`RELEASE 1.12 WP02 — EXECUTION AUTHORITY BLOCKED`

## Completion boundary
WP02 completes only when the frozen planning package is reconciled into the implementation base, the productionized container/runtime composition is implemented and validated, the Git/PR payload is exact, any required merge boundary is satisfied, and #261 is Closed/Done.

WP03 requires a separate explicit GPT-5.6 Terra authority.

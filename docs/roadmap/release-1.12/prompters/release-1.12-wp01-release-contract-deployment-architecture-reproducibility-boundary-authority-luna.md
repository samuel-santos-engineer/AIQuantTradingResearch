# Release 1.12 WP01 — Release Contract, Deployment Architecture & Reproducibility Boundary
## Execution Authority

**Selected execution model: GPT-5.6 Luna**

### Model authority map
- **GPT-5.6 Luna** — PRIMARY: contract, policy, architecture, definition reconciliation, acceptance criteria, governance, read-only/planning execution.
- **GPT-5.6 Terra** — downstream implementation, validation execution, approved Git/GitHub/Azure mutations, publication and merge only under separate explicit authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Mission
Execute Release 1.12 WP01: `Release Contract, Deployment Architecture & Reproducibility Boundary`.

WP01 converts the accepted Release 1.12 planning package into the implementation-grade contract governing WP02–WP08. This authority MUST NOT implement deployment, provision Azure resources, build/push containers, call Twelve Data, migrate schema, publish a release, or merge implementation.

## 2. Required starting state
Freshly reconcile before mutation:
- local `main` and `origin/main` expected at `20a8fccd6e7a5b895e717f946f4501edd7ab8ffa`;
- PR #259 merged;
- Initiative-1.11 remains non-release and Product Release 1.11 abandoned/nonexistent;
- sequence `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`;
- milestone #62 Closed 0/6;
- Release 1.12 milestone #63 Open with eight open WPs;
- #260 Open/Todo; #261–#267 dependency-gated;
- Project #2 Release `1.12` exists and applies only to Release 1.12 work;
- local planning artifacts exist: `RELEASE_1.12_DEFINITION.md`, `RELEASE_1.12_EXECUTION_PLAN.md`, `RELEASE_1.12_FILE_MANIFEST.md` under `docs/roadmap/release-1.12/`;
- those planning artifacts are uncommitted/unpublished unless fresh evidence proves otherwise;
- no Release 1.12 Azure/Docker/provider implementation has begun.

Material conflict => BLOCK and reconcile; never silently adapt.

## 3. Binding inherited architecture
Consume, do not repeat, Initiative-1.11 feasibility:
- Azure App Service Linux F1, West Central US;
- custom Docker;
- public/default HTTPS/DNS;
- persistent `/home`;
- writable SQLite on `/home`, **DELETE journal mode selected**, WAL not selected;
- public/free GHCR;
- bounded authenticated Twelve Data connectivity;
- secret-safe runtime configuration;
- Streamlit public reference/demo frontend;
- exact recurring infrastructure-cost requirement: `ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`.

Carry F1 disclosure: 60 CPU minutes/day, 1 GB storage, shared capacity, throttling/cold starts, no production SLA, bounded recruiter/reference/demo usage only.

## 4. Contract to freeze
WP01 MUST freeze implementation-grade contracts for:

### Deployment topology
Source → deterministic container build → public GHCR image/digest → Azure F1 Linux custom-container web app → persistent `/home` → SQLite → existing application/runtime → public Streamlit/System Health, plus bounded Twelve Data outbound access, update automation, diagnostics, recovery and teardown. No unqualified paid component.

### Container/runtime composition
Define exact existing .NET/Python/Streamlit processes, startup/supervision ownership, lifecycle/failure behavior, filesystem paths, ports/listeners, environment variables, readiness behavior, graceful shutdown/restart, deterministic build inputs, image metadata and local-development compatibility. Streamlit MUST NOT acquire canonical database/provider/Worker ownership.

### GHCR provenance
Define image naming, immutable digest, human-readable tags, source-SHA relationship, release-tag relationship, public pull, rollback selection and evidence proving credentials/secrets are absent from layers/history/metadata. No mandatory ACR.

### Azure F1 resource contract
Freeze minimum region/F1 plan/web-app/settings/storage/runtime configuration, naming, inventory, restart/recycle/redeploy behavior, retention/cleanup, and fail-closed guards preventing paid SKU/dependency selection.

### Persistent SQLite
Freeze Azure path under `/home`, configurable local path, DELETE journal mode, initialization, schema compatibility, migration ownership, concurrency assumptions, integrity checks, transaction/rollback, restart/redeploy persistence, corruption recovery, reference-grade backup/export and storage-budget guardrails. No Azure SQL adoption.

### Twelve Data and bounded automation
Freeze secret source/name, request budget, entitlement/rate-limit assumptions, timeout/retry/backoff boundary, update cadence/trigger, workload ownership, missing/invalid-secret behavior, network/provider failure isolation and secret-leak evidence. WP01 performs no provider calls.

### Public Streamlit/System Health
Freeze public endpoint responsibility, truthful healthy/ready/degraded/empty/failure semantics, freshness/provenance disclosure, safe diagnostics, secret/internal-path suppression and no direct Streamlit SQLite/provider/Worker bypass.

### Reproducibility
Define clean-clone prerequisites and reproducible build, image publication, Azure deployment/configuration, DB initialization, secret injection, public verification, restart/redeploy verification, rollback/recovery, teardown and cost verification. Identify interactive authenticated steps versus automatable steps.

### Strict-$0 enforcement
Define fail-closed controls proving no mandatory paid App Service SKU, ACR, Azure Files, Azure SQL, paid monitoring/networking/scaling or other recurring Azure dependency. Final acceptance retains `$0.00`.

### Security/secrets
Freeze Gitleaks policy, runtime secret injection, image-history/layer inspection, log/public-output inspection, environment redaction, auth-cache/profile exclusion, certificate/private-key exclusion and cleanup. `invalid-wp04-probe-key` remains synthetic historical invalid-auth test data only; scanning is not weakened for other values.

## 5. Application invariants
Preserve .NET pipeline ownership; canonical visualization/read-model ownership; atomic JSON handoff; Python parser → frame → presentation → Streamlit; Streamlit no SQLite/provider/Worker supervision; separate Release 1.8 JSON-over-stdio; truthful deterministic/replay/simulated provenance; truthful Release 1.10 observability/System Health; existing schema/version boundaries absent separate authority; local development; Azure concerns at deployment/config boundaries where practical.

Any required violation => BLOCK for Luna architecture reconciliation.

## 6. Frozen exclusions
No Product Release 1.11 resurrection; Release 2.0 ML; Azure SQL; Azure Files; Container Apps; mandatory ACR; paid tiers/networking/monitoring; production SLA/HA; live trading/order execution; portfolio management; ML/backtesting; unrelated schema migration; parallel pipelines; UI/provider/database bypass; unrelated refactors.

## 7. File-manifest reconciliation
Reconcile the three Release 1.12 planning artifacts and define exact downstream mutation surfaces across root/container files, .NET runtime composition, Python/Streamlit, configuration, `eng/`, tests, docs and GitHub workflows if selected.

Distinguish: (1) WP01 changes; (2) each downstream WP's expected paths; (3) forbidden paths; (4) generated/local/runtime artifacts never committed. No implementation file changes merely to prepare them.

## 8. Downstream WP contract freeze
Freeze implementation-ready entry contracts for:
- **WP02 — Productionized Container & Runtime Composition** — GPT-5.6 Terra.
- **WP03 — GHCR Publication & Azure F1 Deployment Automation** — GPT-5.6 Terra.
- **WP04 — Persistent SQLite Initialization, Data Update & Recovery** — GPT-5.6 Terra.
- **WP05 — Twelve Data Runtime Configuration, Secrets & Bounded Automation** — GPT-5.6 Terra.
- **WP06 — Public Streamlit/System Health Deployment & Truthful Diagnostics** — GPT-5.6 Terra.
- **WP07 — Deployment Stability, Recovery, Cost & No-Bypass Validation** — GPT-5.6 Terra.
- **WP08 — Documentation, Operational Runbook & Release Acceptance** — GPT-5.6 Luna final acceptance; Terra only for separately authorized validation/publication/lifecycle mutations.

Each must have dependencies, exact mutation surfaces, forbidden mutations, validation/evidence, exact acceptance marker and lifecycle condition.

## 9. WP01 mutation policy
Allowed: read-only Git/GitHub reconciliation; `git fetch` with remote-tracking mutation recorded; necessary edits to the three Release 1.12 planning artifacts; one narrowly justified architecture/contract artifact only if existing files cannot coherently hold the contract; documentation-only manifest reconciliation.

Forbidden: production/source implementation; packages; schema; Docker build/push; Azure mutation; GHCR mutation; provider requests; secret mutation; tag/release; commit/push/PR/merge under this authority. No count-padding artifacts.

## 10. GitHub lifecycle
WP01 is issue **#260**. Verify Open, milestone #63, Project Release `1.12`, Status `Todo` before closure.

Only after exact acceptance: close #260; rely on Project automation for Done; if it does not fire, set Done explicitly; never perform/count redundant status mutation; verify milestone #63 remains Open with seven WPs; verify #261 is next dependency-ready. No other WP closes here.

## 11. Publication boundary
The planning artifacts are expected unpublished. Determine the coherent publication boundary after WP01 contract edits. Report exact unpublished path set and hand publication to a separate **GPT-5.6 Terra publication authority**. WP02 MUST NOT silently absorb unpublished governance artifacts into an unrelated implementation commit.

## 12. Validation
Before acceptance: `git diff --check`; established Gitleaks scan; Markdown/reference/path sanity; planning cross-consistency; exact sequence/milestone/issue references; no Product 1.11 resurrection; no Release 2.0 contamination; no unsupported Azure claims; no paid mandatory dependency; no implementation/source/package/schema changes; no secrets/auth caches/environment files; exact mutation audit.

Current Azure capability claims not already proven by feasibility must use authoritative Microsoft evidence or be deferred to downstream empirical validation.

## 13. Mutation accounting
Report separately: repository files; remote-tracking refs; branches; index; commits; pushes; PRs; issues; Project item/fields; milestone; Azure; Docker/GHCR; provider requests; package/schema/production mutations.

Expected implementation/infrastructure/provider mutations: **0**. Count #260 closure only after acceptance; count Project Status only if explicitly changed.

## 14. Interactive handoff
For authenticated/local execution provide one bounded batch at a time with purpose, exact PowerShell, classification, expected mutations, requested stdout/stderr/exit codes, then STOP. Never infer success or weaken security.

## 15. Acceptance gates
A — current-state reconciliation.
B — deployment/runtime/GHCR/F1/SQLite/provider/public-health/security architecture frozen.
C — clean-clone reproducibility contract frozen.
D — strict-$0 fail-closed controls frozen.
E — WP02–WP08 ownership/evidence/acceptance non-ambiguous.
F — file manifest accurate without implementation mutation.
G — whitespace/secrets/references/consistency/governance validation passes.
H — exact mutation audit; implementation/Azure/Docker/provider/package/schema = zero.
I — only after A–H, #260 Closed/Done; #63 remains Open.

## 16. Exact markers
`RELEASE 1.12 WP01 — CURRENT-STATE RECONCILIATION: PASS`
`RELEASE 1.12 WP01 — DEPLOYMENT ARCHITECTURE CONTRACT: PASS`
`RELEASE 1.12 WP01 — REPRODUCIBILITY BOUNDARY: PASS`
`RELEASE 1.12 WP01 — STRICT-ZERO-COST ENFORCEMENT CONTRACT: PASS`
`RELEASE 1.12 WP01 — DOWNSTREAM WP OWNERSHIP CONTRACT: PASS`
`RELEASE 1.12 WP01 — FILE MANIFEST RECONCILIATION: PASS`
`RELEASE 1.12 WP01 — VALIDATION: PASS`
`RELEASE 1.12 WP01 — MUTATION AUDIT: PASS`
`RELEASE 1.12 WP01 — RELEASE CONTRACT, DEPLOYMENT ARCHITECTURE & REPRODUCIBILITY BOUNDARY: PASS`
`RELEASE 1.12 WP01 — GITHUB LIFECYCLE: CLOSED/DONE`
`RELEASE 1.12 WP02 — EXECUTION AUTHORITY: READY`
`RELEASE 1.12 WP01 — EXECUTION AUTHORITY COMPLETE`

On unresolved gate:
`RELEASE 1.12 WP01 — EXECUTION AUTHORITY BLOCKED`

## 17. Completion boundary
WP01 completes only after the implementation-grade contract and downstream boundaries are frozen, validation/mutation audit pass, and #260 is Closed/Done. WP02 still requires a separate explicit **GPT-5.6 Terra** execution authority.

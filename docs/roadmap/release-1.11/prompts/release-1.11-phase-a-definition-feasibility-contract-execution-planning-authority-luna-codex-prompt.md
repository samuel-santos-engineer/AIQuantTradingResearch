# Release 1.11 Phase A — Definition, Feasibility Contract & Execution Planning Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY: contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, planning.
- **GPT-5.6 Terra** — implementation, empirical validation, approved Git/GitHub/Azure mutations under later explicit authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

## Mission
Define and plan **Release 1.11 Phase A — Azure App Service Linux F1 Feasibility Qualification**.

Question to prove empirically:

> Can AIQuantTradingResearch use Azure App Service Linux F1 + custom Docker + persistent `/home` + writable SQLite + Twelve Data outbound HTTPS as a truthful, provider-independent, strict-$0 public reference deployment?

Planning/governance only. Do not implement production deployment or change production architecture.

## Binding baseline
- Release 1.10 lifecycle-complete; `v1.10.0` remains anchored to `eb9601596d9a9dd68f1f8a7c963906a76e5a2833`.
- PR #251 merged and verified.
- Current verified development main: `fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`.
- PR #251 canonical payload: 29/29.
- Build 0/0; Infrastructure 191/191; Application 136/136; Architecture 27/27; Domain 11/11; Python 25/25; Python 3.13.15; Streamlit 1.61.1; pip/Gitleaks/docs/invariants clean.
- Independently re-read Git/GitHub state. If main legitimately advanced, record the new baseline and prove `fe74af1d...` remains ancestor.

Emit `RELEASE 1.11 PHASE A PLANNING ENTRY: PASS`.

## Frozen deployment decisions
### Hugging Face Docker Spaces — ABANDONED
No Release 1.11 implementation, fallback, sync, or deployment role.
Emit `RELEASE 1.11 DEPLOYMENT DECISION: HUGGING FACE ABANDONED`.

### Azure Container Apps + Azure Files — DEFERRED
Future-only; not Phase A implementation scope.
Emit `RELEASE 1.11 DEPLOYMENT DECISION: CONTAINER APPS/AZURE FILES DEFERRED`.

### Sole Phase A candidate
**Azure App Service Linux F1 + custom Docker + persistent `/home` + SQLite**.
Candidate only, not yet proven.
Emit `RELEASE 1.11 PHASE A TARGET: AZURE APP SERVICE LINUX F1`.

## Provider-independence invariant
Azure is a deployment target, never an application dependency. No Azure-specific dependency/concept may enter Domain, Application, market-data abstractions, analytics, storage abstractions, or canonical pipeline semantics.

Database path remains configuration:
- local hypothesis: `/app/data/aiquant.db`
- Azure hypothesis: `/home/data/aiquant.db`

Emit `RELEASE 1.11 PROVIDER-INDEPENDENCE CONTRACT: FROZEN`.

## Production architecture freeze
Until Phase A passes, do not:
- redesign production persistence around Azure;
- add Azure SDKs to product layers;
- replace SQLite abstractions;
- alter pipeline ownership/schema solely for hosting;
- restructure Streamlit solely for Azure;
- implement production deployment CI/CD;
- publish the production/reference deployment;
- introduce paid resources as hidden dependencies.

Only a later Terra authority may create an isolated feasibility probe.
Emit `RELEASE 1.11 PHASE A PRODUCTION ARCHITECTURE FREEZE: ACTIVE`.

## Strict-$0 contract
Empirically prove actual recurring infrastructure cost = **$0.00**.

Explicitly inventory/prevent accidental paid dependencies including ACR, Azure Files, Application Insights, Log Analytics, paid App Service SKU, separately billed storage/networking/custom-domain resources, or other metered services.

Prefer an already-free registry path such as GHCR if a registry is required, subject to actual account constraints.

Emit `RELEASE 1.11 PHASE A STRICT-ZERO-COST CONTRACT: FROZEN`.

## Mandatory feasibility matrix
Plan exact evidence for all gates:
1. F1 available to actual subscription/region.
2. Custom Linux Docker runs on actual F1.
3. Persistent `/home` enabled and writable.
4. SQLite created beneath `/home`.
5. Runtime INSERT/UPDATE/SELECT succeeds.
6. Required bounded concurrent reads/writes succeed for single-instance topology.
7. Data survives app restart.
8. Data survives container restart/recycle.
9. Data survives Docker image redeployment.
10. `PRAGMA integrity_check` succeeds after persistence tests.
11. SQLite locking/journal behavior empirically qualified.
12. WAL versus appropriate rollback-journal mode explicitly tested; WAL never assumed safe.
13. Twelve Data outbound DNS/TLS/HTTPS succeeds.
14. Twelve Data secret injection is safe and not leaked.
15. CPU/memory/storage usage fits F1 with documented headroom.
16. Public HTTPS behavior demonstrated if available under zero-cost candidate.
17. Every deployed resource inventoried.
18. Actual recurring cost proven `$0.00`.
19. No production architecture change required to pass.

Emit `RELEASE 1.11 PHASE A FEASIBILITY MATRIX: COMPLETE`.

## Phase A hypothesis
```text
Internet
   |
 HTTPS
   v
Azure App Service Linux F1
   |
   v
Minimal Feasibility Docker Container
   |-- outbound HTTPS --> Twelve Data
   |
   +-- SQLite --> /home/data/feasibility.db
                    |
                    v
             persistent storage
```
The feasibility DB is not the production DB.

## Work packages
### WP01 — Azure F1 Feasibility Contract, Evidence Matrix & Resource Plan
**GPT-5.6 Luna**
Freeze evidence, resource inventory, allowed/disallowed resources, region/account assumptions, strict-$0 accounting, security, cleanup, PASS/BLOCK criteria. No Azure resources.

### WP02 — Minimal Docker + App Service F1 Execution Probe
**GPT-5.6 Terra**
Later prove custom Docker, F1 execution, public endpoint, `/home` write/persistence, deterministic probe identity, restart/recycle. Minimal and isolated.

### WP03 — Persistent SQLite Filesystem, Locking & Journal Qualification
**GPT-5.6 Terra**
Later test create/open, INSERT/UPDATE/SELECT, transactions, concurrent readers/bounded writer+readers, restart/redeploy persistence, integrity/quick checks, journal modes, lock/busy behavior, failure/recovery evidence. WAL not presumed valid.

### WP04 — Twelve Data Outbound Connectivity, Secrets & Failure Isolation
**GPT-5.6 Terra**
Later prove DNS/TLS/HTTPS, secret injection, minimal real request, timeout/error handling, zero credential leakage. No provider redesign.

### WP05 — F1 Resource Envelope & Strict-$0 Cost Qualification
**GPT-5.6 Terra**
Measure cold start, startup/idle/peak memory, CPU/CPU-minute implications, SQLite/image/storage/log growth, headroom, complete resource inventory, actual recurring cost. Pricing assumptions alone are insufficient.

### WP06 — Phase A Feasibility Acceptance, Cleanup & Architecture Decision
**GPT-5.6 Luna** for acceptance/reconciliation; Terra only for explicitly authorized execution/cleanup.
Aggregate evidence and produce exactly one:
- `AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: FEASIBLE`
- `AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: NOT FEASIBLE`

NOT FEASIBLE is valid and must not trigger architectural compromise. Temporary resources must be explicitly retained under later authority or cleaned up after evidence capture.

Default dependency:
`WP01 → WP02 → WP03 → WP04 → WP05 → WP06`

No Phase B before WP06 FEASIBLE.
Emit:
- `RELEASE 1.11 PHASE A WORK-PACKAGE PLAN: COMPLETE`
- `RELEASE 1.11 PHASE A DEPENDENCY GRAPH: FROZEN`

## GitHub reconciliation
Read-only reconcile next canonical milestone/release taxonomy.
Expected, subject to verification:
- milestone #60
- Release = 1.11
- Project #2

Prepare exact WP issue definitions with title, objective, scope, exclusions, dependencies, evidence, acceptance criteria, selected model, lifecycle.

Every WP must include:
> After the exact acceptance gate passes, close the GitHub WP issue and set Project #2 Status to Done before proceeding, unless explicit authority defers lifecycle. If automation already sets Done on issue closure, do not redundantly mutate Status.

Never close before exact acceptance marker.
Emit `RELEASE 1.11 PHASE A GITHUB LIFECYCLE PLAN: COMPLETE`.

## Required planning artifacts
Create/update:
`docs/roadmap/release-1.11/`

1. `RELEASE_1.11_DEFINITION.md`
2. `RELEASE_1.11_PHASE_A_FEASIBILITY_CONTRACT.md`
3. `RELEASE_1.11_EXECUTION_PLAN.md`
4. `RELEASE_1.11_FILE_MANIFEST.md`

Definition must distinguish:
- Phase A = Azure F1 feasibility qualification.
- Phase B = public reference deployment implementation, unauthorized until Phase A passes.

Manifest must separate planning/governance paths, expected probe paths, evidence paths, and forbidden production-architecture paths.

Do not create Phase B implementation files.
Emit `RELEASE 1.11 PHASE A PLANNING ARTIFACTS: COMPLETE`.

## Research/evidence
Use current authoritative Microsoft/Azure documentation for changeable capabilities/pricing. Record source title/URL, access date, claim supported, and whether documentation or empirical evidence.

Documentation can prove only documented capability. Actual execution against the user's Azure environment is required for empirical PASS.

Never convert documentation into empirical feasibility markers.

## Planning validation
Validate Markdown, links, cross-references, dependency consistency, model assignments, acceptance markers, no contradiction with Release 1.10/PR #251, no Hugging Face scope, no Container Apps/Azure Files implementation, no Phase B implementation, and no production architecture mutation.

Run product tests only if governance genuinely requires them for touched contracts.
Emit `RELEASE 1.11 PHASE A PLANNING VALIDATION: PASS`.

## Mutation boundary
This is a Luna planning authority.

Allowed only if established governance explicitly permits within planning:
- four planning artifacts;
- milestone #60 reconciliation/creation;
- Phase A WP issue creation;
- Project #2 additions;
- Release=1.11 and initial Status fields;
- planning branch/commit/PR if established workflow requires.

Prefer conservative separation if prior governance requires a later publication authority.

Forbidden:
- Azure resource/subscription mutation;
- Docker deployment;
- production Dockerfile implementation;
- GHCR publication;
- Twelve Data live execution;
- production source/test architecture changes;
- implementation dependencies/schema migration;
- Phase B implementation;
- release tag/publication;
- milestone closure;
- implementation merge.

Report exact mutations.
Emit `RELEASE 1.11 PHASE A MUTATION AUDIT: PASS`.

## Planning success semantics
Planning COMPLETE means the feasibility contract is complete, executable, bounded, and non-circular. It does **not** mean Azure F1 is feasible.

Future WP06 must produce exactly one:
`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: FEASIBLE`
or
`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: NOT FEASIBLE`

## Required success markers
`RELEASE 1.11 PHASE A PLANNING ENTRY: PASS`
`RELEASE 1.11 DEPLOYMENT DECISION: HUGGING FACE ABANDONED`
`RELEASE 1.11 DEPLOYMENT DECISION: CONTAINER APPS/AZURE FILES DEFERRED`
`RELEASE 1.11 PHASE A TARGET: AZURE APP SERVICE LINUX F1`
`RELEASE 1.11 PROVIDER-INDEPENDENCE CONTRACT: FROZEN`
`RELEASE 1.11 PHASE A PRODUCTION ARCHITECTURE FREEZE: ACTIVE`
`RELEASE 1.11 PHASE A STRICT-ZERO-COST CONTRACT: FROZEN`
`RELEASE 1.11 PHASE A FEASIBILITY MATRIX: COMPLETE`
`RELEASE 1.11 PHASE A WORK-PACKAGE PLAN: COMPLETE`
`RELEASE 1.11 PHASE A DEPENDENCY GRAPH: FROZEN`
`RELEASE 1.11 PHASE A PLANNING ARTIFACTS: COMPLETE`
`RELEASE 1.11 PHASE A GITHUB LIFECYCLE PLAN: COMPLETE`
`RELEASE 1.11 PHASE A PLANNING VALIDATION: PASS`
`RELEASE 1.11 PHASE A MUTATION AUDIT: PASS`
`RELEASE 1.11 PHASE A → FEASIBILITY EXECUTION HANDOFF: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

## Exact success terminal
`RELEASE 1.11 PHASE A — DEFINITION, FEASIBILITY CONTRACT & EXECUTION PLANNING AUTHORITY COMPLETE`

## Block conditions
BLOCK rather than broaden scope if:
- repository/GitHub baseline cannot be reconciled;
- milestone/release taxonomy inconsistent;
- strict-$0 acceptance cannot be defined without unapproved paid dependency;
- Azure-specific Domain/Application changes are required;
- SQLite WAL correctness is assumed rather than tested;
- Hugging Face re-enters scope;
- Container Apps/Azure Files becomes current implementation;
- Phase B mixes into Phase A;
- documentation substitutes for empirical proof;
- planning contradicts established architecture;
- unauthorized Git/GitHub/Azure mutation would be required.

Preserve evidence and identify the narrow Luna reconciliation required.

## Exact blocked terminal
`RELEASE 1.11 PHASE A — DEFINITION, FEASIBILITY CONTRACT & EXECUTION PLANNING AUTHORITY BLOCKED`

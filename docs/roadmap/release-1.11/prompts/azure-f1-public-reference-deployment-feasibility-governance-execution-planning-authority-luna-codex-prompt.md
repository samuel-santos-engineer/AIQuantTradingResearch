# Azure F1 Public Reference Deployment — Feasibility Governance & Execution Planning Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY: contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — implementation, empirical validation execution, approved Git/GitHub/Azure mutations, cleanup, merge/publication under explicit authority.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, exploratory/non-authoritative review; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

# Mission
Define and freeze the governance, feasibility contract, work-package plan, evidence model, resource controls, cleanup contract, and Terra execution boundary for the non-release initiative:

**Public Reference Deployment / Azure App Service F1 Feasibility Qualification**

This initiative is NOT Release 1.11 and MUST NOT become a numbered release under this authority.

This is planning/governance authority only. No Azure resource creation, deployment, production architecture implementation, or live Twelve Data execution is authorized.

# Canonical version-policy baseline
The following policy is binding:

`RELEASE VERSION-SEQUENCING POLICY: PRESERVE 1.10 → 2.0; RELEASE 1.11 ABANDONED`

Therefore:
- Release 1.11 is abandoned as a release identity;
- do not create milestone 1.11;
- do not create Project #2 Release option 1.11;
- do not create Release 1.11 WP issues;
- milestone #60 remains Release 2.0;
- milestone #61 and later milestone identities remain unchanged;
- Release 1.10 history remains immutable.

Emit:
`AZURE F1 FEASIBILITY GOVERNANCE ENTRY: PASS`

# Repository baseline
Independently verify:
- local `main`;
- `origin/main`;
- divergence;
- staging/worktree;
- current latest canonical development commit;
- Release 1.10 tag/release historical anchor;
- prior blocked 1.11 authorities caused zero repository/GitHub/Azure mutation.

Known prior verified development main:
`fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`

If main advanced legitimately, record the new baseline and prove the known commit remains an ancestor.

Preserve unrelated local prompt/control files.

Emit:
`AZURE F1 FEASIBILITY REPOSITORY BASELINE: VERIFIED`

# Frozen deployment decision
The feasibility target is:

**Azure App Service Linux F1 + custom Docker + persistent `/home` + writable SQLite**

Frozen alternatives:
- **Hugging Face Docker Spaces: ABANDONED**
- **Azure Container Apps + Azure Files: DEFERRED**

Do not re-open these alternatives unless empirical Azure F1 qualification fails and a later authority explicitly requests target reconciliation.

Emit:
`AZURE F1 FEASIBILITY TARGET: AZURE APP SERVICE LINUX F1`
`AZURE F1 ALTERNATIVE: HUGGING FACE ABANDONED`
`AZURE F1 ALTERNATIVE: CONTAINER APPS/AZURE FILES DEFERRED`

# Strict-zero-cost contract
Recurring infrastructure cost must be:

`$0.00`

Planning assumptions are insufficient.

The eventual Terra execution must prove:
- actual F1 availability in the operator's subscription/region;
- no paid App Service plan;
- no paid Azure Files;
- no paid Azure Container Registry requirement;
- no separately metered resource unintentionally created;
- no paid monitoring/logging dependency;
- all resources inventoried;
- final observed/estimated recurring infrastructure cost is `$0.00`.

Prefer an already-free image-registry path such as GHCR if a registry is required and independently qualified. Do not make ACR mandatory.

Emit:
`AZURE F1 STRICT-ZERO-COST CONTRACT: FROZEN`

# Provider-independence contract
Azure is deployment infrastructure only.

Forbidden architecture coupling:
- Azure-specific code in Domain;
- Azure-specific code in Application;
- Azure-specific market-data abstractions;
- Azure-specific analytics abstractions;
- Azure-specific persistence abstractions;
- Azure SDK dependencies introduced merely for hosting;
- Azure-specific database semantics leaking into product contracts.

Database path must remain configuration.

Illustrative paths:
- local: `/app/data/aiquant.db`
- Azure candidate: `/home/data/aiquant.db`

Emit:
`AZURE F1 PROVIDER-INDEPENDENCE CONTRACT: FROZEN`

# Production architecture freeze
Until empirical feasibility acceptance passes:
- no production architecture change for Azure;
- no production deployment architecture commitment;
- no replacement database;
- no schema migration;
- no dual mutable SQLite authorities;
- no GitHub Actions canonical-database redesign;
- no production Docker/deployment implementation beyond an explicitly isolated feasibility probe;
- no Phase B/public-production implementation.

A NOT FEASIBLE result is valid.

Emit:
`AZURE F1 PRODUCTION ARCHITECTURE FREEZE: ACTIVE`

# Current authoritative-source requirement
Azure platform behavior and pricing change over time.

Luna must consult current authoritative Microsoft/Azure documentation and record:
- source title;
- official URL;
- access date;
- exact claim supported;
- whether the claim is documentation evidence or empirical evidence.

At minimum re-verify:
- Linux custom-container support on F1;
- F1 resource limits/pricing;
- persistent `/home` behavior;
- `WEBSITES_ENABLE_APP_SERVICE_STORAGE`;
- custom-container registry options;
- restart/recycle/redeployment persistence semantics;
- relevant App Service storage caveats.

Documentation proves only documented capability. It cannot satisfy an empirical PASS gate.

Emit:
`AZURE F1 AUTHORITATIVE DOCUMENTATION MATRIX: COMPLETE`

# Governance identity
This is a **non-release feasibility initiative**.

Luna must determine and freeze how it is tracked without creating a false release identity.

Preferred governance:
- no numbered release milestone;
- no Project Release value fabricated for this initiative;
- initiative-specific planning/evidence artifacts in a clearly named non-release directory;
- optional GitHub issues only if existing project governance supports non-release work without assigning a false Release value.

If Project #2 requires a Release value for every item, do not invent one. Instead specify a safe alternative or defer issue creation.

Emit:
`AZURE F1 NON-RELEASE GOVERNANCE IDENTITY: FROZEN`

# Required planning artifacts
Determine exact repository paths using existing naming conventions.

Preferred directory:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/`

Required planning artifacts:
1. `AZURE_F1_FEASIBILITY_DEFINITION.md`
2. `AZURE_F1_FEASIBILITY_CONTRACT.md`
3. `AZURE_F1_EXECUTION_PLAN.md`
4. `AZURE_F1_FILE_MANIFEST.md`

If repository conventions require another non-release directory, use it and explain why.

These artifacts are planning/governance only.

They must explicitly state:
- not Release 1.11;
- not a modification of Release 2.0;
- no production architecture change before feasibility PASS.

Emit:
`AZURE F1 PLANNING ARTIFACT SET: FROZEN`

# Evidence directory contract
Define a deterministic evidence structure before execution.

Preferred:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/evidence/`

Define evidence categories for:
- subscription/region/F1 availability;
- resource inventory;
- Docker/App Service startup;
- public HTTPS;
- `/home` mount/persistence;
- SQLite CRUD;
- SQLite transactions/concurrency;
- journal-mode qualification;
- restart/recycle;
- image redeployment;
- integrity checks;
- Twelve Data outbound connectivity;
- secret-handling verification;
- CPU/memory/storage envelope;
- logs/failure isolation;
- cost evidence;
- cleanup evidence;
- final acceptance.

Secrets, tokens, API keys, subscription identifiers beyond necessary non-sensitive evidence, and sensitive environment dumps must never be committed.

Define sanitization/redaction rules.

Emit:
`AZURE F1 EVIDENCE CONTRACT: FROZEN`

# Resource lifecycle contract
Every eventual Azure/cloud resource must have:
- purpose;
- resource type;
- region;
- pricing tier;
- owner;
- creation authority;
- creation timestamp/evidence;
- expected cost;
- cleanup condition;
- cleanup authority;
- final disposition.

Temporary feasibility resources are the default.

Retention after qualification requires explicit later authority.

A failed experiment must not leave forgotten billable resources.

Emit:
`AZURE F1 RESOURCE LIFECYCLE CONTRACT: FROZEN`

# Six-work-package execution plan
Freeze exactly six sequential WPs.

## WP01 — Feasibility Contract, Evidence Matrix & Resource Plan
**Model: GPT-5.6 Luna**

Purpose:
Finalize the non-release feasibility contract before cloud mutation.

Must freeze:
- test matrix;
- evidence requirements;
- resource inventory template;
- cost gates;
- security/redaction rules;
- cleanup contract;
- exact PASS/BLOCK semantics;
- allowed probe files;
- forbidden production paths.

No Azure resources.

Acceptance marker:
`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

Lifecycle:
If represented by a GitHub issue, after exact acceptance close the issue and set Project #2 Status to Done before proceeding, unless explicit authority defers lifecycle. If automation already sets Done on closure, do not redundantly mutate Status.

## WP02 — Minimal Docker + App Service F1 Execution Probe
**Model: GPT-5.6 Terra**

Purpose:
Empirically prove the hosting substrate.

Must prove:
- actual F1 availability;
- custom Linux Docker runs;
- public HTTPS endpoint;
- persistent App Service storage can be enabled;
- `/home` is writable;
- restart/recycle behavior;
- image redeployment behavior;
- exact resources created.

Only a minimal isolated feasibility probe is allowed.

Acceptance marker:
`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

## WP03 — Persistent SQLite Filesystem, Locking & Journal Qualification
**Model: GPT-5.6 Terra**

Purpose:
Determine whether SQLite is operationally truthful on Azure App Service persistent `/home`.

Must empirically test:
- create/open;
- INSERT;
- UPDATE;
- SELECT;
- transaction commit/rollback;
- bounded concurrent read/write behavior;
- lock/busy behavior;
- restart persistence;
- container recycle persistence;
- image redeployment persistence;
- `PRAGMA integrity_check`;
- `PRAGMA quick_check`;
- recovery after interrupted/failed operation where safely testable;
- rollback-journal mode;
- WAL mode.

WAL must NOT be presumed safe or selected by preference. Record actual behavior and select only an evidence-supported journal mode.

Acceptance marker:
`AZURE F1 WP03 — SQLITE PERSISTENCE & JOURNAL QUALIFICATION: PASS`

## WP04 — Twelve Data Outbound Connectivity, Secrets & Failure Isolation
**Model: GPT-5.6 Terra**

Purpose:
Prove required outbound provider connectivity without leaking secrets or coupling architecture to Azure.

Must test:
- DNS;
- TLS;
- outbound HTTPS;
- safe secret injection;
- minimal real Twelve Data request;
- timeout behavior;
- provider error behavior;
- application/probe failure isolation;
- logs contain no API key or secret.

Do not redesign provider abstractions.

Acceptance marker:
`AZURE F1 WP04 — TWELVE DATA CONNECTIVITY & SECRET ISOLATION: PASS`

## WP05 — F1 Resource Envelope & Strict-$0 Qualification
**Model: GPT-5.6 Terra**

Purpose:
Prove the candidate fits F1 and remains actually free.

Measure/evidence:
- cold start;
- memory;
- CPU utilization/CPU-minute constraints;
- storage use;
- SQLite growth assumptions;
- image size;
- log growth;
- headroom;
- restart behavior under resource pressure where safe;
- complete Azure resource inventory;
- registry cost;
- monitoring/logging cost;
- actual recurring infrastructure cost.

Acceptance requires:
`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

Acceptance marker:
`AZURE F1 WP05 — RESOURCE ENVELOPE & STRICT-ZERO-COST: PASS`

## WP06 — Feasibility Acceptance, Cleanup & Architecture Decision
**Primary model: GPT-5.6 Luna**
**Terra role: explicitly authorized validation/cleanup mutations only**

Reconcile all evidence.

Choose exactly one:
`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: FEASIBLE`

or

`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: NOT FEASIBLE`

FEASIBLE requires all mandatory gates.

NOT FEASIBLE is a valid outcome and must not trigger architectural compromise.

Resource cleanup:
- clean temporary resources unless explicit later authority permits retention;
- prove cleanup/disposition;
- prove no unintended recurring cost remains.

Acceptance marker:
`AZURE F1 WP06 — FEASIBILITY ACCEPTANCE & CLEANUP: PASS`

# Dependency graph
Freeze:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06`

No WP may bypass an unmet predecessor gate.

Emit:
`AZURE F1 WORK-PACKAGE DEPENDENCY GRAPH: FROZEN`

# Empirical feasibility matrix
The execution plan must require proof of all of these:
1. F1 available to actual subscription/region.
2. Custom Linux Docker runs on F1.
3. Persistent `/home` enabled and writable.
4. SQLite created under `/home`.
5. Runtime INSERT/UPDATE/SELECT succeeds.
6. Transactions behave correctly.
7. Bounded concurrent reads/writes are characterized.
8. Lock/busy behavior is characterized.
9. Data survives app restart.
10. Data survives container restart/recycle.
11. Data survives Docker image redeployment.
12. `PRAGMA integrity_check` passes.
13. `PRAGMA quick_check` passes.
14. Journal/locking behavior is qualified.
15. WAL vs rollback-journal explicitly tested.
16. Twelve Data outbound DNS/TLS/HTTPS works.
17. Secrets are injected without leakage.
18. Provider timeout/error behavior is isolated.
19. Public HTTPS behavior is acceptable for a reference deployment.
20. CPU/memory/CPU-minute envelope is compatible with F1.
21. Storage/image/log envelope fits with headroom.
22. All resources are inventoried.
23. Actual recurring infrastructure cost is `$0.00`.
24. No production architecture change is required.
25. Temporary-resource cleanup/disposition is proven.

Emit:
`AZURE F1 FEASIBILITY MATRIX: COMPLETE`

# GitHub governance planning
Read current issue/Project conventions and determine whether non-release initiative WPs can be represented without a fake Release value.

For each WP, prepare an exact issue specification containing:
- title;
- objective;
- scope;
- exclusions;
- dependencies;
- evidence;
- acceptance marker;
- selected model;
- lifecycle rule.

Do not create issues under this authority unless existing governance explicitly allows planning authority to do so and no false Release assignment is required.

If Project #2 cannot represent non-release work cleanly, state that issues should remain outside Project #2 or that a later governance adjustment is required. Do not misuse Release 2.0.

Emit:
`AZURE F1 GITHUB GOVERNANCE PLAN: COMPLETE`

# File-manifest contract
The manifest must separate:
- planning/governance paths;
- isolated feasibility-probe paths;
- evidence paths;
- temporary/generated evidence excluded from Git;
- forbidden production architecture paths.

Forbidden production paths must include, at minimum, any change whose purpose is:
- Azure coupling in Domain/Application;
- production schema migration;
- replacement persistence architecture;
- production deployment cutover;
- canonical database synchronization redesign;
- Release 2.0 capability modification.

Emit:
`AZURE F1 FILE MANIFEST CONTRACT: FROZEN`

# Phase B prohibition
No public-production implementation or architecture change is authorized by a FEASIBLE result alone.

A FEASIBLE result permits only a later Luna governance decision about whether/how to proceed with public reference deployment.

Emit:
`AZURE F1 PHASE B AUTHORIZATION: NOT GRANTED`

# Planning mutation boundary
Allowed only if established governance permits planning artifacts:
- the four initiative planning artifacts;
- narrowly necessary planning branch/commit/PR;
- optional non-release WP issue setup only if it can be represented truthfully.

Preferred conservative behavior:
if prior governance separates definition from publication/GitHub mutation, stop after producing the planning candidate and exact mutation plan.

Forbidden:
- Azure resource creation;
- Docker deployment;
- live Twelve Data request;
- production implementation;
- release milestone creation;
- Project Release option creation;
- use of Release 2.0 as a false container for the initiative;
- tags/releases;
- milestone closure;
- production architecture mutation.

# Planning validation
Validate:
- no Release 1.11 identity remains in newly proposed governance except historical explanation;
- Release 2.0 scope is untouched;
- six WPs and dependency graph are exact;
- every empirical gate has evidence ownership;
- resource cleanup is mandatory;
- strict-$0 is explicit;
- provider independence is explicit;
- secrets cannot enter committed evidence;
- no Phase B implementation is authorized.

Emit:
`AZURE F1 FEASIBILITY PLANNING VALIDATION: PASS`

# Mutation audit
Report exact counts:
- repository-content edits;
- commits;
- pushes;
- issue creations/edits/closures;
- Project mutations;
- milestone mutations;
- Release-option mutations;
- Azure resource mutations;
- external registry mutations;
- live provider requests.

Count only explicit mutations actually performed.

Emit:
`AZURE F1 FEASIBILITY PLANNING MUTATION AUDIT: PASS`

# Execution handoff
Produce the exact next authority required.

Normally:
- if WP01 is not completed by this planning authority, next = **GPT-5.6 Luna WP01 authority**;
- if WP01 is fully frozen, accepted, and any required lifecycle is complete, next = **GPT-5.6 Terra WP02 execution authority**.

Do not authorize WP02 Azure mutation unless WP01 acceptance is exact and governance representation is reconciled.

Emit:
`AZURE F1 → FEASIBILITY EXECUTION HANDOFF: PASS`

# Required success markers
`AZURE F1 FEASIBILITY GOVERNANCE ENTRY: PASS`
`AZURE F1 FEASIBILITY REPOSITORY BASELINE: VERIFIED`
`AZURE F1 FEASIBILITY TARGET: AZURE APP SERVICE LINUX F1`
`AZURE F1 ALTERNATIVE: HUGGING FACE ABANDONED`
`AZURE F1 ALTERNATIVE: CONTAINER APPS/AZURE FILES DEFERRED`
`AZURE F1 STRICT-ZERO-COST CONTRACT: FROZEN`
`AZURE F1 PROVIDER-INDEPENDENCE CONTRACT: FROZEN`
`AZURE F1 PRODUCTION ARCHITECTURE FREEZE: ACTIVE`
`AZURE F1 AUTHORITATIVE DOCUMENTATION MATRIX: COMPLETE`
`AZURE F1 NON-RELEASE GOVERNANCE IDENTITY: FROZEN`
`AZURE F1 PLANNING ARTIFACT SET: FROZEN`
`AZURE F1 EVIDENCE CONTRACT: FROZEN`
`AZURE F1 RESOURCE LIFECYCLE CONTRACT: FROZEN`
`AZURE F1 WORK-PACKAGE DEPENDENCY GRAPH: FROZEN`
`AZURE F1 FEASIBILITY MATRIX: COMPLETE`
`AZURE F1 GITHUB GOVERNANCE PLAN: COMPLETE`
`AZURE F1 FILE MANIFEST CONTRACT: FROZEN`
`AZURE F1 PHASE B AUTHORIZATION: NOT GRANTED`
`AZURE F1 FEASIBILITY PLANNING VALIDATION: PASS`
`AZURE F1 FEASIBILITY PLANNING MUTATION AUDIT: PASS`
`AZURE F1 → FEASIBILITY EXECUTION HANDOFF: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal
`AZURE F1 PUBLIC REFERENCE DEPLOYMENT — FEASIBILITY GOVERNANCE & EXECUTION PLANNING AUTHORITY COMPLETE`

# Block conditions
BLOCK if:
- repository/version baseline cannot be proven;
- governance cannot represent a non-release initiative without falsely assigning Release 2.0 or inventing 1.11;
- strict-$0 cannot be made an enforceable acceptance gate;
- provider independence cannot be preserved;
- feasibility requires production architecture change before qualification;
- evidence/secret handling cannot be made safe;
- resource cleanup ownership is undefined;
- authoritative Azure documentation contradicts the assumed F1/custom-container/persistence basis so materially that the feasibility plan is invalid;
- broader roadmap mutation is required.

On BLOCK:
- perform no Azure mutation;
- perform no production implementation;
- preserve canonical `1.10 → 2.0`;
- identify the minimum reconciliation authority needed.

# Exact blocked terminal
`AZURE F1 PUBLIC REFERENCE DEPLOYMENT — FEASIBILITY GOVERNANCE & EXECUTION PLANNING AUTHORITY BLOCKED`

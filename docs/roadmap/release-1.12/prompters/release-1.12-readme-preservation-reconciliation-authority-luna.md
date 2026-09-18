# GPT-5.6 Luna — Release 1.12 Front-Door README Preservation & Reconciliation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: read-only contract, policy, architecture, preservation classification, reconciliation, and amendment-definition authority.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations, amendment publication, and merge only under separately issued authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## Mission
Perform a read-only preservation reconciliation between the canonical pre-update README and the README proposed by PR #273. Identify useful information that was removed, compressed, weakened, or materially reframed.

The governing approach is **additive and conservative**:

`USEFUL EXISTING INFORMATION → PRESERVE`

Conciseness is subordinate to truth and preservation.

## Zero-mutation boundary
This authority authorizes **zero mutations**. Do not edit/stage/commit/push, amend or merge PR #273, create another PR, or mutate issues, Project #2, milestone #63, tags/releases, Azure, Docker, GHCR, provider resources, packages, or schema.

## Reconciliation objects
Canonical pre-update object:

`6f8b19634301d03ce48dfc9f3c9ef1cfbc9a5b3d:README.md`

PR #273 expected proposed object:

`8050c01a246a954066bc8d15e3fd161abebab1e6:README.md`

Fresh Git/GitHub evidence controls. If PR #273 head advanced, record it and reconcile the current head after proving it remains the same README-only publication line.

## PR #273 merge hold
The prior PR #273 merge authority is superseded/held pending preservation reconciliation.

`FRONT-DOOR README PR #273 — MERGE HOLD FOR PRESERVATION RECONCILIATION: ACTIVE`

## Required evidence
Inspect read-only:
- complete base README;
- complete proposed README;
- diff stat and numstat;
- unified diff;
- heading/section order in both;
- removed, added, and materially rewritten blocks;
- links, badges, tables, code/examples;
- release/capability history;
- architecture and ownership boundaries;
- engineering principles;
- validation/testing information;
- roadmap/planned technology;
- setup/operator material if present;
- recruiter-facing context;
- governance/AI-assisted engineering material.

Read surrounding sections; do not classify solely from isolated diff hunks.

## Classification contract
Every materially removed or weakened information block gets one primary classification.

### RESTORE
Use when useful and still-true information was removed or materially weakened. Strong-default categories include architecture, ownership boundaries, deterministic/reproducibility principles, release history, capability journey detail, data flow, provider/persistence boundaries, interoperability, visualization, observability/System Health, testing/validation strategy, security principles, limitations/non-goals, governance, roadmap rationale, setup/usage guidance, project motivation, and recruiter-facing engineering evidence.

Restoration may reorganize wording but must preserve substantive information, terminology, constraints, and useful context.

### SUPERSEDED
Use only when a newer accepted statement conveys the same useful information more accurately. For every such item identify the old information, new replacement location/text, and why no substantive information is lost.

A shorter generic summary is not sufficient evidence of supersession.

### INTENTIONALLY_REMOVE
Use narrowly for objectively false/stale current-state claims, unsupported volatile counts, exact duplication without value, broken/redundant presentation, obsolete milestone status, or future-only wording for capabilities already implemented. State the reason.

If uncertain between RESTORE and INTENTIONALLY_REMOVE, choose RESTORE.

## Material weakening rule
Treat information as removed when some wording remains but important substance is lost. Examples include detailed architecture becoming “uses Python,” deterministic evidence becoming “stores data in SQLite,” validation strategy becoming “tested,” capability history losing intermediate stages, or limitations disappearing.

## Section inventory
Inventory every meaningful base README section with:
- Base heading
- Proposed equivalent or NONE
- Disposition: PRESERVED / PARTIALLY_PRESERVED / REMOVED / REFRAMED
- Classification: RESTORE / SUPERSEDED / INTENTIONALLY_REMOVE / NO_ACTION
- Information-loss risk: NONE / LOW / MEDIUM / HIGH
- Binding amendment instruction

Do not skip sections merely because they are historical.

## Removed-block inventory
Create stable identifiers:

`README-PRESERVE-001`, `README-PRESERVE-002`, ...

For every material removed/weakened block record:
- base location/heading;
- description of lost information;
- proposed replacement location if any;
- classification;
- rationale;
- substantive elements that must survive;
- whether restoration may be reorganized;
- status wording that must be updated.

## Current-state corrections that MUST survive
Do not revert accepted current-state corrections.

**Current closed milestone**

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification — FEASIBLE`

Preserve:

`Initiative-1.11 ≠ Product Release 1.11`

**Current accepted milestone**

`Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization — IN PROGRESS`

Preserve:
- WP01 Closed/Done;
- WP02 Closed/Done;
- WP03 Closed/Done;
- WP04 next/Open/Todo;
- Release 1.12 not complete.

Preserve product sequence:

`1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`

## Implemented-status corrections that MUST survive
These are implemented foundations and MUST NOT be restored as future-only:
- Python 3.13 interoperability;
- governed one-shot JSON-over-stdio;
- Docker/containerized runtime composition;
- cloud-native/reference deployment foundation through public GHCR and Azure App Service Linux F1 deployment automation;
- OpenTelemetry-based pipeline/boundary observability;
- truthful Streamlit System Health.

Policy:

`PRESERVE INFORMATION; UPDATE STATUS`

not:

`RESTORE STALE STATUS`

## Architectural truths that must survive
Ensure preservation/restoration of useful explanations of:
- .NET canonical pipeline ownership;
- durable SQLite evidence;
- deterministic retrieval/evidence boundaries;
- immutable dataset/snapshot/catalog/experiment concepts where documented;
- Release 1.8 JSON-over-stdio boundary;
- Release 1.9 visualization read model/canonical JSON handoff;
- distinction between interoperability and visualization handoff;
- Streamlit not directly owning provider/SQLite/Worker supervision;
- deterministic/replay/simulated provenance;
- Release 1.10 OpenTelemetry observability;
- truthful System Health;
- Dockerized runtime;
- GHCR publication;
- Azure App Service F1 reference/demo scope;
- no production SLA claim;
- remaining Release 1.12 stabilization boundary.

Additional useful architecture in the base README remains subject to the preservation default.

## Historical-information policy
Historical material should remain when it demonstrates engineering progression or explains architecture. Do not collapse the journey into only the latest milestone.

Preserve useful 1.1–1.4 material already present, then represent:
- 1.5 deterministic experiment;
- 1.6 durable experiment evidence;
- 1.7 bounded durable experiment discovery;
- 1.8 Python interoperability;
- 1.9 governed visualization;
- 1.10 observability/System Health;
- Initiative-1.11 FEASIBLE;
- 1.12 IN PROGRESS;
- Phase 5 2.0–2.3 planned.

If richer historical descriptions exist, preserve that richness while updating stale “current” labels.

## Validation/evidence preservation
Do not delete useful testing/validation methodology merely because exact counts are volatile. Classify obsolete numerical counts separately from durable validation information. Preserve useful methodology.

## Front-door readability
Reorganization for scanability is allowed, including summary tables or concise lead sections, but it must not erase substantive engineering evidence.

A recruiter/engineer should understand what the system does, what is implemented, why the architecture is disciplined, how it evolved, how it is validated, what is deployed, what remains in progress, and what is planned.

## Required reconciliation report
Produce:
1. canonical evidence;
2. base/proposed README line and size counts;
3. section inventory;
4. removed/weakened block inventory;
5. classification counts;
6. high-risk information-loss items;
7. binding restoration requirements;
8. stale statements that must remain removed/updated;
9. exact amendment scope;
10. Terra handoff decision.

Empirical counts required:
- `BASE_SECTION_COUNT`
- `PROPOSED_SECTION_COUNT`
- `PRESERVED_SECTION_COUNT`
- `PARTIALLY_PRESERVED_SECTION_COUNT`
- `REMOVED_SECTION_COUNT`
- `REFRAMED_SECTION_COUNT`
- `RESTORE_ITEM_COUNT`
- `SUPERSEDED_ITEM_COUNT`
- `INTENTIONALLY_REMOVE_ITEM_COUNT`
- `NO_ACTION_ITEM_COUNT`
- `HIGH_RISK_INFORMATION_LOSS_COUNT`

## Terra amendment contract
If amendment is required, Luna must define a bounded handoff for Terra.

Preferred path:

`README.md`

Preferred publication strategy:
- amend existing PR #273 branch;
- no duplicate PR;
- stage only README.md;
- one amendment commit unless fresh evidence requires otherwise;
- revalidate payload 1/1;
- merge remains separately gated.

The handoff must enumerate every `README-PRESERVE-*` item required for restoration/update.

This Luna authority does NOT authorize implementation.

## Required markers
`FRONT-DOOR README PRESERVATION — CANONICAL OBJECT RECONCILIATION: PASS`

`FRONT-DOOR README PRESERVATION — BASE SECTION INVENTORY: PASS`

`FRONT-DOOR README PRESERVATION — PROPOSED SECTION INVENTORY: PASS`

`FRONT-DOOR README PRESERVATION — REMOVED BLOCK INVENTORY: PASS`

`FRONT-DOOR README PRESERVATION — MATERIAL WEAKENING ANALYSIS: PASS`

`FRONT-DOOR README PRESERVATION — ARCHITECTURE INFORMATION: RECONCILED`

`FRONT-DOOR README PRESERVATION — HISTORICAL INFORMATION: RECONCILED`

`FRONT-DOOR README PRESERVATION — VALIDATION INFORMATION: RECONCILED`

`FRONT-DOOR README PRESERVATION — IMPLEMENTED TECHNOLOGY STATUS: PRESERVED`

`FRONT-DOOR README PRESERVATION — CURRENT MILESTONE STATUS: PRESERVED`

`FRONT-DOOR README PRESERVATION — STALE STATUS RESTORATION: PROHIBITED`

`FRONT-DOOR README PR #273 — MERGE HOLD FOR PRESERVATION RECONCILIATION: ACTIVE`

## Decision
If material information loss exists and can be bounded:

`FRONT-DOOR README PRESERVATION — RECONCILIATION: PASS`

`FRONT-DOOR README PR #273 — AMENDMENT REQUIRED`

`FRONT-DOOR README PR #273 — TERRA AMENDMENT AUTHORITY: READY TO CREATE`

If no material loss exists:

`FRONT-DOOR README PRESERVATION — RECONCILIATION: PASS`

`FRONT-DOOR README PR #273 — AMENDMENT NOT REQUIRED`

Do not authorize merge in either case.

If evidence/classification is incomplete:

`FRONT-DOOR README PRESERVATION — RECONCILIATION: BLOCKED`

## Mutation audit
Expected:
- repository mutations: 0
- branches: 0
- commits: 0
- pushes: 0
- PR creates/edits/merges: 0
- issue/Project/milestone mutations: 0
- Azure/Docker/GHCR/provider mutations: 0
- package/schema mutations: 0

Report actual read-only fetch/view/diff operations where applicable.

Required:

`FRONT-DOOR README PRESERVATION — MUTATION AUDIT: PASS`

Terminal:

`RELEASE 1.12 — FRONT-DOOR README PRESERVATION & RECONCILIATION AUTHORITY COMPLETE`

On failure:

`RELEASE 1.12 — FRONT-DOOR README PRESERVATION & RECONCILIATION AUTHORITY BLOCKED`

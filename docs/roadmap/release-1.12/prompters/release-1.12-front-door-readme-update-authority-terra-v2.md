# GPT-5.6 Terra — Release 1.12 Front-Door README Current-State Update Authority

**Selected execution model: GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, definition, governance, reconciliation, and acceptance-criteria authority.
- **GPT-5.6 Terra** — PRIMARY: bounded README implementation, validation execution, Git/GitHub publication.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory review only; never silently replaces Luna or Terra.

## 1. Mission

Update the repository front-door `README.md` so it accurately presents the accepted AIQuantTradingResearch platform state immediately after Release 1.12 WP03 and before WP04.

This is a documentation-only current-state reconciliation.

The README must be useful as a recruiter/engineer landing page and clearly distinguish:
- what is implemented today;
- the current closed milestone;
- the current accepted/in-progress milestone;
- what remains in Release 1.12;
- later platform direction.

## 2. Canonical base

Expected canonical `main`:

`6f8b19634301d03ce48dfc9f3c9ef1cfbc9a5b3d`

Fresh Git evidence controls.

Before mutation prove:
- local `main` = `origin/main`;
- ahead/behind `0/0`;
- staging empty;
- unrelated local/untracked controls are preserved.

If canonical `main` has advanced unexpectedly, STOP for reconciliation.

## 3. Exact mutation contract

Authorized repository mutation:

| Operation | Path |
|---|---|
| MODIFY | `README.md` |

`AUTHORIZED_PATH_COUNT=1`

No other repository path may be mutated.

Never stage unrelated local controls/prompts.

## 4. Binding milestone presentation

The README must present these two front-door milestone labels exactly.

### Current closed milestone

**Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification — FEASIBLE**

This initiative is complete/closed/accepted.

It must remain explicit that:

`Initiative-1.11 ≠ Product Release 1.11`

Do not imply that Product Release 1.11 exists.

### Current accepted milestone

**Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization — IN PROGRESS**

Current accepted progression:
- WP01 — complete / Closed / Done
- WP02 — complete / Closed / Done
- WP03 — complete / Closed / Done
- WP04 — next / Open / Todo
- milestone #63 — Open, 5 open / 3 closed

Do not claim Release 1.12 is complete.

Release 1.10 remains an important completed historical capability milestone, but it is not the README's `Current closed milestone` label.

## 5. Top-level project description

Replace stale/outdated introductory wording with a concise current description.

The description should position AIQuantTradingResearch as a production-oriented quantitative research and platform-engineering project demonstrating disciplined architecture and AI-assisted engineering.

The description should reflect the implemented platform:

- deterministic market-data research pipeline;
- Twelve Data acquisition;
- durable SQLite evidence;
- deterministic dataset/snapshot/catalog/experiment evidence;
- Python interoperability;
- governed visualization;
- Streamlit presentation;
- OpenTelemetry-based pipeline/boundary observability;
- truthful System Health;
- Dockerized runtime composition;
- public GHCR image publication;
- Azure App Service Linux F1 reference deployment automation;
- bounded governance, validation, and reproducibility.

Do not claim:
- production trading;
- production SLA;
- completed Release 1.12;
- implemented ML/XAI/backtesting;
- adopted Azure SQL;
- Kubernetes;
- Azure Container Apps;
- Azure Files;
- mandatory ACR;
- paid monitoring/networking architecture.

## 6. What Works Today

Rewrite/reconcile `What Works Today` so it represents the current accepted platform rather than an obsolete Release 1.5/1.7-era state.

It must cover, concisely:

### Data and deterministic research
- Twelve Data historical acquisition;
- durable SQLite persistence;
- deterministic retrieval;
- immutable dataset/snapshot/catalog evidence;
- deterministic research pipeline;
- feature generation;
- experiment execution/evidence;
- durable experiment evidence/discovery.

### Python interoperability — IMPLEMENTED
Explicitly state that Python interoperability is implemented.

Represent:
- Python 3.13;
- governed one-shot JSON-over-stdio boundary;
- .NET remains canonical owner of the pipeline/contract;
- Python is not a bypass around canonical application ownership.

Do not describe Python interoperability as future work.

### Governed visualization — IMPLEMENTED
Represent:
- canonical .NET visualization read model;
- atomic JSON handoff;
- Python parser/frame/presentation;
- Streamlit presentation;
- Ready/WarmUp/Empty/Failed truthfulness;
- deterministic/replay/simulated provenance disclosure;
- no direct Streamlit provider/SQLite/Worker-supervision ownership.

### OpenTelemetry observability — IMPLEMENTED
Explicitly state that OpenTelemetry-based observability is implemented.

Represent:
- pipeline/boundary observability;
- truthful System Health presentation;
- Release 1.10 as the completed capability foundation.

Do not describe observability/OpenTelemetry as merely planned.

### Docker/containerization — IMPLEMENTED
Explicitly state that Docker/containerized runtime composition is implemented.

Represent:
- existing Dockerfile/container runtime composition;
- bounded process/runtime supervision;
- validated containerized execution.

Do not describe Docker as merely planned.

### Cloud-native/reference deployment — IMPLEMENTED FOUNDATION
Explicitly state that cloud-native/reference deployment capability has been implemented through the accepted Release 1.12 WP03 boundary.

Represent:
- public/free GHCR image publication;
- immutable public image/digest qualification;
- Azure App Service Linux F1 deployment automation;
- West Central US qualified deployment;
- public HTTPS;
- public GHCR image;
- persistent `/home`;
- port 8501;
- no mandatory ACR;
- reference/demo scope.

Use careful wording:
- deployment **automation/foundation** is implemented;
- Release 1.12 stabilization is still in progress;
- this is not a production SLA architecture.

## 7. Implemented Technology

Create/reconcile a clearly identifiable **Implemented Technology** section.

It must reflect actual current status and include at least:

- C# / .NET
- Python 3.13
- governed JSON-over-stdio Python interoperability
- Twelve Data
- SQLite
- deterministic research/evidence pipeline
- canonical JSON visualization handoff
- Streamlit
- OpenTelemetry-based observability
- truthful System Health
- Docker
- containerized runtime composition
- public/free GHCR container publication
- Azure App Service Linux F1 reference deployment automation
- persistent App Service `/home` deployment configuration
- PowerShell engineering/deployment automation
- automated Domain/Application/Infrastructure/Architecture/Python validation
- Gitleaks/secret screening
- GitHub planning/governance/project workflow

### Mandatory implemented-status correction

The following MUST NOT appear as future-only/planned capabilities:

`Python interoperability`

`Docker`

`cloud-native deployment`

`OpenTelemetry-based observability`

They are implemented foundations.

If existing README prose currently places them under planned/future technology, remove or rewrite that stale classification.

## 8. Planned Technology & Platform Direction

Create/reconcile a clearly identifiable **Planned Technology & Platform Direction** section.

It must begin from the current accepted boundary rather than repeating implemented foundations.

### Release 1.12 remaining work

Represent the remaining sequence:

**WP04 — Persistent SQLite Initialization, Data Update & Recovery**
- deployment-oriented persistent SQLite initialization;
- bounded data update;
- recovery;
- persistence correctness.

**WP05 — Twelve Data Runtime Configuration, Secrets & Bounded Automation**
- runtime configuration;
- secret isolation;
- bounded provider automation.

**WP06 — Public Streamlit/System Health Deployment & Truthful Diagnostics**
- public application boundary;
- truthful deployed diagnostics/System Health.

**WP07 — Deployment Stability, Recovery, Cost & No-Bypass Validation**
- stability;
- recovery;
- strict-zero-cost validation;
- architecture/no-bypass validation.

**WP08 — Documentation, Operational Runbook & Release Acceptance**
- final operational documentation;
- runbook;
- Release 1.12 acceptance.

### Post-1.12 independent investigation

Represent:

**Azure SQL Database Free Offer Investigation & Architecture Decision**

This is:
- independent;
- later;
- not part of Release 1.12;
- not currently adopted;
- candidate architecture may preserve SQLite locally while evaluating Azure SQL for Azure deployment only if separately approved.

Do not present Azure SQL as implemented or committed.

### Phase 5 direction

Represent:
- Release 2.0 — Lightweight Machine Learning Evaluation
- Release 2.1 — Machine Learning
- Release 2.2 — Explainable AI
- Release 2.3 — Backtesting

These remain planned.

### Mandatory planned-status correction

Do not list these as future work:
- Python interoperability;
- Docker/containerization;
- cloud-native/reference deployment foundation;
- OpenTelemetry observability.

Future work may build on them, but the foundational capabilities themselves are implemented.

## 9. Engineering Capability Journey

Update the Engineering Capability Journey through the current project state.

Preserve earlier meaningful stages, then include at minimum:

- **1.5** — deterministic experiment capability
- **1.6** — durable experiment evidence
- **1.7** — bounded durable experiment evidence discovery
- **1.8** — Python interoperability / governed JSON-over-stdio boundary
- **1.9** — governed real-time-style visualization and canonical JSON handoff
- **1.10** — OpenTelemetry-based pipeline/boundary observability and truthful System Health
- **Initiative-1.11** — Azure App Service F1 public-reference deployment feasibility qualification — **FEASIBLE**
- **1.12** — Public Reference Deployment Implementation & Stabilization — **IN PROGRESS**
  - WP01 accepted
  - WP02 accepted
  - WP03 accepted
  - WP04 next
- **2.0** — Lightweight Machine Learning Evaluation — planned
- **2.1** — Machine Learning — planned
- **2.2** — Explainable AI — planned
- **2.3** — Backtesting — planned

Explicitly preserve:

`Initiative-1.11 ≠ Product Release 1.11`

Product-release sequence:

`1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`

Do not convert Initiative-1.11 into a product semantic version.

## 10. Release 1.10 historical capability

Where useful, preserve:

**Phase 4 - Release 1.10: OpenTelemetry & Pipeline Observability**

as the completed observability capability milestone in the journey/history.

Do not use it as the README's current closed milestone because the binding front-door label is Initiative-1.11.

## 11. Quality and evidence presentation

Inspect stale badges, exact test counts, release labels, and “current” language.

Do not preserve obsolete counts as if current.

Choose one:
1. derive current exact counts from canonical evidence and update them; or
2. replace volatile exact counts with durable truthful validation wording.

Never invent test counts.

Do not invent a `1.12.0` tag/version before Release 1.12 publication.

## 12. Front-door readability requirements

The resulting README should read coherently for a recruiter or engineer unfamiliar with the repository.

Preferred information order:

1. project description;
2. current milestone state;
3. What Works Today;
4. architecture/operating truth;
5. Implemented Technology;
6. Planned Technology & Platform Direction;
7. Engineering Capability Journey;
8. supporting engineering/governance details.

Avoid making the front door a raw governance log.

Use concise milestone state and capability summaries while preserving technical credibility.

## 13. Validation

Before publication run and record:

- canonical-base reconciliation;
- exact path-set verification;
- `git diff --check`;
- Markdown structure/link sanity;
- Gitleaks;
- only `README.md` changed;
- no secret/token/credential content;
- no accidental semantic Release 1.11;
- no claim Release 1.12 is complete;
- WP04 remains next;
- Python interoperability appears as implemented;
- Docker/containerization appears as implemented;
- cloud-native/reference deployment foundation appears as implemented;
- OpenTelemetry observability appears as implemented;
- none of those four remain classified as future-only;
- Azure SQL remains future investigation only;
- ML/XAI/backtesting remain planned;
- Initiative-1.11 current closed milestone label is exact;
- Release 1.12 current accepted milestone label is exact.

## 14. Required acceptance markers

`FRONT-DOOR README — CANONICAL BASE RECONCILIATION: PASS`

`FRONT-DOOR README — EXACT PATH 1/1: PASS`

`FRONT-DOOR README — CURRENT CLOSED MILESTONE: INITIATIVE-1.11 FEASIBLE: PASS`

`FRONT-DOOR README — CURRENT ACCEPTED MILESTONE: RELEASE 1.12 IN PROGRESS: PASS`

`FRONT-DOOR README — DESCRIPTION RECONCILIATION: PASS`

`FRONT-DOOR README — WHAT WORKS TODAY RECONCILIATION: PASS`

`FRONT-DOOR README — PYTHON INTEROPERABILITY IMPLEMENTED: PASS`

`FRONT-DOOR README — DOCKER IMPLEMENTED: PASS`

`FRONT-DOOR README — CLOUD-NATIVE DEPLOYMENT FOUNDATION IMPLEMENTED: PASS`

`FRONT-DOOR README — OPENTELEMETRY OBSERVABILITY IMPLEMENTED: PASS`

`FRONT-DOOR README — IMPLEMENTED TECHNOLOGY RECONCILIATION: PASS`

`FRONT-DOOR README — PLANNED TECHNOLOGY DIRECTION RECONCILIATION: PASS`

`FRONT-DOOR README — ENGINEERING CAPABILITY JOURNEY RECONCILIATION: PASS`

`FRONT-DOOR README — IMPLEMENTED/PLANNED BOUNDARY: PASS`

`FRONT-DOOR README — VALIDATION: PASS`

Acceptance:

`RELEASE 1.12 — FRONT-DOOR README CURRENT-STATE RECONCILIATION: PASS`

## 15. Git publication

After acceptance:

- create a dedicated branch from canonical base;
- stage only `README.md`;
- never use `git add .` or `git add -A`;
- prove staged payload 1/1;
- commit once;
- push;
- create one non-draft PR to `main`;
- prove PR payload exactly `README.md`.

Preferred branch:

`docs/release-1.12-front-door-current-state`

Preferred commit:

`Update README for current Release 1.12 progression`

Preferred PR title:

`Docs: update front door for current Release 1.12 progression`

## 16. Merge/lifecycle boundary

This authority does **not** authorize PR merge.

It does not authorize:
- issue #263 mutation;
- Project status mutation;
- milestone #63 mutation;
- tags/releases;
- Azure;
- Docker;
- GHCR;
- Twelve Data/provider calls;
- packages;
- schema.

Required:

`FRONT-DOOR README — STAGED PAYLOAD 1/1: PASS`

`FRONT-DOOR README — COMMIT: PASS`

`FRONT-DOOR README — PUSH: PASS`

`FRONT-DOOR README — PR PAYLOAD 1/1: PASS`

`FRONT-DOOR README — PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

## 17. Mutation audit

Expected:
- repository modifies: 1
- repository creates/deletes/renames: 0
- branches: 1
- commits: 1
- pushes: 1
- PRs: 1
- merges: 0
- issue mutations: 0
- Project mutations: 0
- milestone mutations: 0
- Azure/Docker/GHCR/provider mutations: 0
- package/schema mutations: 0

Report actual counts.

Terminal:

`RELEASE 1.12 — FRONT-DOOR README UPDATE AUTHORITY COMPLETE`

If any gate fails:

`RELEASE 1.12 — FRONT-DOOR README UPDATE AUTHORITY BLOCKED`

Do not widen the repository path set.

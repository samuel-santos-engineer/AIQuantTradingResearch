# Azure F1 Public Reference Deployment — Feasibility Definition

## Governance identity

This is a non-release feasibility initiative: **Public Reference Deployment / Azure App Service F1 Feasibility Qualification**. It is not Release 1.11, does not modify Release 2.0, and does not create a numbered release boundary.

The canonical product sequence remains `1.10 → 2.0`. Milestone #60 remains `Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`; no Release 1.11 milestone, Project Release option, or work-package issues are created by this initiative.

## Question

Can a minimal, provider-independent reference deployment run on Azure App Service Linux F1 with a custom Docker container, persistent `/home`, writable SQLite, and bounded Twelve Data HTTPS access at an actual recurring infrastructure cost of `$0.00`?

The answer is empirical. Documentation, pricing pages, local Docker execution, or an architectural diagram cannot produce a `FEASIBLE` result.

## Frozen target and exclusions

- Candidate: Azure App Service Linux F1 + custom Docker + persistent `/home` + SQLite.
- Local database hypothesis: `/app/data/aiquant.db`.
- Azure database hypothesis: `/home/data/aiquant.db`.
- Hugging Face Docker Spaces: abandoned.
- Azure Container Apps/Azure Files: deferred.
- Azure is deployment infrastructure only; it must not enter Domain, Application, market-data, analytics, persistence abstractions, or canonical pipeline semantics.
- Release 1.10, schema v4, canonical JSON handoff, Worker/Streamlit independence, and production persistence remain unchanged.
- No production deployment, Phase B implementation, Azure SDK in product layers, migration, provider redesign, or hidden paid dependency is authorized.

## Decision gate

WP06 must emit exactly one result:

- `AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: FEASIBLE`, only when every mandatory evidence row passes and cleanup/cost proof is complete; or
- `AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: NOT FEASIBLE`, when any mandatory row fails or the strict-zero/provider-independence contract cannot be satisfied.

`NOT FEASIBLE` is valid and does not authorize an architectural compromise or automatic fallback.

## Model map

- GPT-5.6 Luna: contract, reconciliation, acceptance, and architecture decision.
- GPT-5.6 Terra: isolated probe implementation, empirical execution, measurement, and explicitly authorized cleanup.
- GPT-5.6 Sol: supporting analysis only; never substitutes for Luna or Terra.

Selected planning model: GPT-5.6 Luna. No Azure mutation is authorized by this artifact.

## Acceptance boundary

Phase A qualifies only the hosting substrate and deployment hypothesis. A `FEASIBLE` result permits a later Luna decision about public reference deployment; it does not authorize Phase B, production cutover, release tagging, or changes to the product architecture.

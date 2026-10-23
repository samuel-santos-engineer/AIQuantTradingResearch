# Release 1.15 — Codex Prompt: Add Research Apps to Existing Azure F1 Plans

## Owner authorization and objective

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

The owner authorizes a **capacity-first assessment and, only if all gates pass, provisioning of exactly two additional Azure Web Apps** in the **same Azure subscription and existing App Service Plans** already used by the project's F1 applications:

- **Production:** one additional app in **West Central US** on the existing production F1 App Service Plan.
- **Staging:** one additional app in **West US 2** on the existing staging F1 App Service Plan (spelling: staging).

The intended future architecture may use a separate research container and UI container per environment. **Do not change Release 1.15 governance, WP01 contracts, WP02 implementation plans, application code, deployment configuration or existing apps in this task.** This task assesses infrastructure feasibility and creates only the two new apps if proven feasible. `PUBLIC_RUNTIME_VERIFIED=False`.

## Mandatory preflight — read-only first

1. Read `AGENTS.md`, the Release 1.15 roadmap, existing Azure/deployment documentation, repository LF policy and prior Azure evidence. Inventory dirty/untracked files, particularly Codex prompts; preserve them without reset, cleanup or overwrite. Record the exact current branch and HEAD.
2. Authenticate through the existing approved Azure CLI context; identify and record the **exact subscription ID**, tenant, existing production and staging Web App resource IDs, resource groups, plan resource IDs, regions, operating-system kinds, F1 SKU, Linux/container capability, and any shared-plan applications. Do not infer resource IDs from naming patterns. Never switch subscription implicitly or create a new subscription.
3. Confirm production's existing plan is in **West Central US** and staging's existing plan is in **West US 2**. If either differs, **STOP**; do not silently select another plan or region.
4. Assess actual subscription/region capacity and plan constraints: Free F1 SKU, existing app count, plan resource limits, available quotas, OS/container compatibility, and the impact of sharing compute/free-tier limits. Distinguish documented limits from Azure CLI/ARM observations. If quotas cannot be conclusively queried, document the uncertainty and stop before provisioning. Confirm the additional Linux Web Apps can use the planned deployment model on those *exact* existing plans; do not assume Linux custom-container support from F1 SKU alone.
5. Verify effective permissions to create Web Apps in the existing resource groups and bind them to the existing plan IDs, plus any relevant Azure Policy or resource locks. Use read-only operations for the preflight; no probe app creation.
6. Present a short capacity/compatibility table with evidence (resource IDs may be recorded in repository documentation if existing project policy allows; never record secrets, tokens or credentials). Explicitly distinguish subscription quota, plan resource limits and shared compute capacity.

## Provisioning authorization and stop conditions

**Proceed only if every preflight condition is verified and no ambiguity remains.** The owner authorizes exactly two new **F1 Web App resources**, one per environment, attached to the existing corresponding F1 plans. No new App Service Plan may be created. No paid-tier change, plan resize, region substitution, OS conversion, new resource group, extra app, container registry, networking resource or existing-app mutation is authorized.

- Select new globally unique app names that follow existing repository/Azure naming conventions, clearly distinguish research from existing UI apps, and are available. Record proposed names and their exact target plan IDs before creating resources. If naming convention is unclear or names conflict, stop and ask.
- Provision the production app only on the existing West Central US production plan; provision the staging app only on the existing West US 2 staging plan. Ensure new apps do not receive provider credentials, production data, live ingestion, public research endpoints, or deployment artifacts as part of this task.
- Set only the minimum app properties required to create compatible empty app shells. Do not enable Always On, paid capabilities or settings unsupported by F1.
- Treat the operation as a two-resource change: if the first succeeds and the second fails, **do not delete or mutate the first automatically**; report partial state and request owner disposition.
- Never claim an app is private merely because no container is deployed. New Azure Web Apps may have public hostnames; do not deploy a raw-data API or any private content. Assess future service-to-service networking separately before selecting the two-app architecture.
- Do not deploy code or containers, modify DNS/custom domains, configure authentication/secrets, or alter the current live production/staging UI.

## Post-provision verification

Use read-only Azure CLI/ARM queries to verify for **each** new app: full resource ID, subscription, resource group, region, exact existing plan resource ID, Linux/OS kind, plan SKU still F1, provisioning state, hostname, and that the original applications and plans remain unchanged. Distinguish an Azure resource successfully created from a functioning deployed runtime. Do not assert that separate Web Apps can communicate over private loopback: `127.0.0.1` is scoped to each app/container and does not cross App Services.

## Repository evidence and PR

Preserve this **executed Codex prompt** as a tracked UTF-8/LF Markdown artifact under `docs/roadmap/release-1.15/prompters/` and index it in `RELEASE_1.15_FILE_MANIFEST.md`. Record the preflight, exact resource observations, decisions, stop conditions, and (if provisioning occurs) verification results in a narrowly scoped Release 1.15 infrastructure evidence document. Do not expose secrets or tokens. If local file scope requires separate exact-path approval under `AGENTS.md`, stop before modifying repository files and request approval; do not bypass that gate.

Run Markdown link/anchor checks, UTF-8 without BOM, LF/final newline, and `git diff --check`. Do not run product builds/tests unless separately authorized. Open a dedicated **documentation/evidence PR** for review; do not merge it, close WP02 #368, or modify the merged WP01 record. Do not change architecture/governance yet: the owner will decide whether to amend the single-container WP01 design after the Azure feasibility evidence.

## Required Codex response

Report: (1) subscription and existing plan IDs/regions/SKUs, (2) compatibility/quota/capacity findings with uncertainties, (3) whether provisioning was permitted, (4) any created app names and resource IDs, (5) existing-resource unchanged checks, (6) files changed and PR link if created, (7) explicit blockers and owner decisions needed. Preserve `PUBLIC_RUNTIME_VERIFIED=False`.

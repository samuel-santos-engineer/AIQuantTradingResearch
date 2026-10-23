# Release 1.15 — Codex prompt: validate and provision two additional Azure F1 Web Apps

## Owner authority
The owner authorizes creation of **exactly two additional, empty Linux Azure Web Apps**, one production research-app shell in **West Central US** and one staging research-app shell in **West US 2**, using **only the existing subscription and existing F1 App Service Plans** listed below. This authorization supersedes the earlier demand to prove future shared-compute workload headroom before creating empty app shells. It does **not** waive actual Azure policy/permission/ARM validation, and does not authorize source implementation, deployment, governance amendment, paid resources, or modification of existing apps/plans.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Preserve `PUBLIC_RUNTIME_VERIFIED=False`.

## Exact infrastructure scope
Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`.

**Production**
- Region: `westcentralus` (West Central US)
- Resource group: `rg-aiq-r112-wp03-wcus-5ec325382770`
- Existing plan resource ID: `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp03-wcus-5ec325382770`
- Existing app: `aiqr112wp035ec325382770` (do not modify)

**Staging**
- Region: `westus2` (West US 2)
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Existing plan resource ID: `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5`
- Existing app: `aiqr112wp05wus27f5eabb5` (do not modify)

Source of initial findings: open PR #381 at `a92734f2cbabd9afa04a48e7380dabd93e71bf09`, `docs/roadmap/release-1.15/RELEASE_1.15_AZURE_EXISTING_F1_PLANS_CAPACITY_EVIDENCE.md`. **Re-query Azure live**; do not treat recorded observations as proof of current state.

## Step 1 — repository and Azure preflight (read-only)
1. Read `AGENTS.md`, Release 1.15 governance, WP01 merged owner acceptance, existing WP02 preflight and PR #381 evidence/prompt, repository LF policy. Inventory tracked modifications and untracked files, especially prompt artifacts. Preserve all user work. Do not reset, clean, overwrite, stash, force-push or absorb pending WP02 files. If repository publication would conflict, use a separate clean worktree based on current `main` and leave other work untouched.
2. Verify authenticated Azure CLI subscription is **exactly** the ID above, enabled, without changing account context or exporting credentials. Confirm both exact plans still exist in their stated resource groups/regions, are **F1 Free**, `reserved=true` (Linux), and existing sites remain attached and unchanged. Record current app count on each plan, relevant documented F1 per-plan app limit, and regional quota, distinguishing plan app count from worker instances and CPU/RAM headroom.
3. Check permission to create `Microsoft.Web/sites` and attach the named existing plans, resource locks, and **effective** Azure Policy/deny assignments at subscription/resource-group/resource scope. Inspect inherited initiative definitions/assignments sufficiently to determine applicability to the proposed Linux sites. If a deny/permission outcome cannot be resolved, **STOP before create** and report the exact ambiguity.
4. Select two unique names conforming to existing naming conventions and clearly indicating Release 1.15 research service and prod/staging roles; verify global name availability. Record proposed names and full target IDs in evidence before mutation. Do not infer plan IDs from naming patterns.
5. Where supported, perform a read-only ARM template validation and/or what-if for each **exact** proposed `Microsoft.Web/sites` resource, bound to the exact existing plan ID and Linux kind, without changing any other resource. If validation/what-if is unavailable or inconclusive, explain whether policy/permission/plan eligibility can still be established by authoritative read-only checks; do not treat an ambiguous result as PASS. No probe creation.

### Revised capacity acceptance criterion for *empty app shells*
Creation may proceed when the target plan is verified existing Linux F1, nominal documented app-count room remains, regional quota is not exhausted for any required new plan allocation (none authorized), the effective permission/policy/lock outcome permits the exact site creation, and ARM/plan eligibility is sufficiently validated. **Do not require measured future CPU/RAM workload headroom for an empty shell.** This does not imply that two deployed containers will perform adequately on shared F1 compute. If any required gate is conclusively failed or unresolved, stop and publish evidence only.

## Step 2 — narrowly scoped provisioning (only after Step 1 passes)
- Create exactly **one empty Linux Web App** in each named resource group, region and **existing** F1 plan. Use exact plan resource IDs and minimal required site configuration. Use Azure CLI/ARM with explicit subscription/resource-group/plan identity; do not rely on implicit defaults.
- Do **not** create or resize App Service Plans; create resource groups, networking, registry, secrets, identity grants, provider connections or other resources; change existing app settings, routing, deployments, DNS, access rules or billing tier; or select a paid fallback.
- Do not deploy a research API, image, application code or raw/private data. An empty app may have a public hostname: do not claim it is private or ready for production.
- If the first creation succeeds and the second fails, preserve the first, record the exact partial state and stop. Do not automatically delete, retry with different region/plan or modify the old apps.

## Step 3 — verify and document
- Re-query both new sites and verify subscription, resource ID, resource group, location, Linux kind, serverFarmId **exactly matching the original plan ID**, provisioning state and hostname. Verify both old sites remain on the same plans, plans remain F1 Linux and no unauthorized new resources were created. Distinguish resource creation from runtime success.
- Record executed commands in sanitized form, policy/ARM validation outcomes, quota/app counts, all resource IDs, any failed commands and resulting recovery instructions. Never include access tokens, credentials, app-setting values or personal identifiers.
- Preserve the **executed prompt** as a tracked Markdown file under `docs/roadmap/release-1.15/prompters/` and index it in `RELEASE_1.15_FILE_MANIFEST.md`. Add/update a narrowly scoped infrastructure evidence document; prefer continuing PR #381 if safe and within its evidence scope, otherwise create a separate documentation-only PR. Do not merge PRs without owner instruction. Do not modify pending WP02 plan/evidence/source files. If `AGENTS.md` requires exact-path approval for a new documentation path, stop and request it rather than bypassing the rule.
- Validate links, JSON if present, UTF-8 without BOM, LF/final newline, exact changed-file inventory, and `git diff --check`. No product tests or builds are authorized by this task.

## Explicit boundaries and report
This is **infrastructure provisioning only**, not approval of a two-container governance amendment. Separate Azure Web Apps do not share `127.0.0.1`; service-to-service network authentication, confidentiality, ingestion isolation and cross-container storage/coordination remain undecided. Keep WP02 #368 open, preserve merged WP01 history, and maintain `PUBLIC_RUNTIME_VERIFIED=False`.

Return: Azure validation result; exact names/IDs and provisioning state of created apps (or why blocked); old-resource unchanged evidence; PR URL/head SHA and exact changed paths; remaining security/network/runtime design decisions. Do not claim successful creation if only ARM validation passed.

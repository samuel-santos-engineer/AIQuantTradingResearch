# Release 1.15 - Existing Azure F1 plan capacity assessment

Date: 2026-10-23. **PROVISIONING BLOCKED: no Azure resources created or changed.** `PUBLIC_RUNTIME_VERIFIED=False`. This is infrastructure evidence only; no Release1.15 architecture amendment, WP02 implementation, provider call or deployment.

## Scope and provenance

[Executed owner authority](prompters/release-1.15-codex-azure-existing-f1-plans-additional-apps.md) permits exactly two empty research app shells only after every capacity/compatibility/permission/policy gate is verified without ambiguity. No new plan, region substitution, paid tier or existing-app mutation is allowed.

Source branch at start: `feat/release-1.15-wp02-runtime-isolation`; HEAD `6f76697707e2363cc52820e0c805172815b92159`. Dirty inventory: existing manifest edit, untracked WP02 plan/evidence/prompt, and this Azure prompt. All original dirty files are hash-snapshotted and preserved; a separate worktree/branch publishes only this evidence, executed prompt and a baseline-derived manifest index. Pending WP02 work is not staged or overwritten. The isolated manifest does not include the local uncommitted WP02 index.

Existing approved Azure CLI account used; no login/token retrieval, credential export or subscription switch. Subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`; tenant `3c614e64-8b9a-45b6-a57d-6c071dfeac0b`; subscription state Enabled. No account names/emails, owner paths, image registry names, tokens or app-setting values are recorded here.

## Exact observed resources

| Environment | Resource group | Existing app ID | Exact existing plan ID |
| --- | --- | --- | --- |
| Production | rg-aiq-r112-wp03-wcus-5ec325382770 | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/sites/aiqr112wp035ec325382770` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp03-wcus-5ec325382770` |
| Staging | rg-aiq-r112-wp05-wus2-7f5eabb5 | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr112wp05wus27f5eabb5` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5` |

IDs above were returned by Azure, not synthesized to discover resources. Resource-group/plan names are infrastructure identifiers, permitted by this authority. Another unrelated F1 plan/app also exists in West Central US; it is included in regional usage but is not an alternate target and was not modified.

## Capacity and compatibility matrix

| Check | Production | Staging | Interpretation |
| --- | --- | --- | --- |
| Required region | West Central US | West US 2 | PASS, exact target regions |
| Observed plan SKU / OS | F1 / Free, reserved=true, linux | F1 / Free, reserved=true, linux | PASS for existing plan metadata |
| Existing app kind / state | app,linux,container / Running | app,linux,container / Running | Existing custom-container configuration observed; no new deployment |
| Apps attached to exact target plan | 1 | 1 | No other app on either target plan in subscription inventory |
| Regional Microsoft.Web F1 quota current/limit | 2 /32 instances | 1 /31 instances | Quota headroom; not per-plan app count or free CPU/RAM evidence |
| Exact-plan status | Ready | Ready | Administrative status, not a reservation for a new app |
| ARM maximumNumberOfWebApps | null | null | Not an observed plan-specific numeric capacity guarantee |
| Exact-plan skus F1 | Returned; scaleType none, maximum0 | Returned; scaleType none, maximum0 | No scale-out capacity; maximum0 is not a zero-app limit |
| Effective resource-group permissions | actions=*; notActions=[] observed | same | Positive allow evidence for sites/write and plan join; not proof against policy/deny conditions |
| Resource locks | No locks in group query; subscription locks endpoint empty | same | No lock observed, no lock changes |
| Azure Policy | SecurityCenterBuiltIn inherited assignment, enforcementMode Default, parameters={} | same | Assignment identified; full initiative/effective denial evaluation not completed |
| Measured shared compute headroom | NOT MEASURED | NOT MEASURED | No workload or quota consumption test authorized/performed |
| New-app provisioning eligibility | NOT conclusively verified | NOT conclusively verified | Required all-gates/no-ambiguity condition not met |

Microsoft's [App Service limits](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/azure-subscription-service-limits#app-service-limits) documents10 Free apps per plan, Linux code/containers, shared compute, CPU time quotas and no Free Always On, VNet integration or private endpoints. These documented limits indicate nominal app-count room; they do not reserve target-plan resources or establish present compute headroom. Regional F1 usage counts instances, not apps; adding a site to an existing plan must not be described as adding a plan/worker quota allocation. Sharing F1 does not provide dedicated compute or an availability guarantee. Storage/resource competition and workload viability remain unqualified.

Existing container kinds plus the documented Linux/container capability support nominal deployment compatibility, not security, target-image functionality or private cross-app networking. Two separate App Services do not share127.0.0.1. Empty shells can have public hostnames; no app may be labeled private solely because nothing is deployed.

## Read-only commands and failures

Executed with secret-free projections: az account show; az webapp list/show; az appservice plan list; az rest GET target serverfarms and /skus (2025-03-01); regional Microsoft.Web /locations/{westcentralus,westus2}/usages (2025-03-01); resource-group Microsoft.Authorization/permissions (2022-04-01); az lock list --resource-group; subscription Microsoft.Authorization/locks GET (2016-09-01); az policy assignment list and show SecurityCenterBuiltIn. Read repository governance/roadmap/prior evidence, AGENTS.md and LF policy. No settings/credentials endpoints called.

Initial sandboxed account query failed accessing the CLI profile; approved execution outside the sandbox succeeded using the same context. `az lock list --scope` was rejected as an unsupported CLI argument; corrected read-only subscription locks REST query succeeded with an empty array. These failures were not treated as successful checks. No retry changed Azure configuration.

## Decision and unchanged-resource checks

**STOP before provisioning.** No new app names selected/reserved, no name-availability request, no create/update/delete/plan resize, no deployment artifact or secrets supplied. The conditional authority is not exercised because effective policy eligibility and compute-capacity assurance remain incomplete. This is uncertainty, not a finding that F1 forbids a second Linux app or that quota is exhausted.

Read-only re-query confirmed both original apps remain Running on the same exact plan IDs with unchanged kinds; both plans remain F1, reserved=true, one site, in their required regions. These are projected-property comparisons, not full configuration byte equivalence or a claim to exclude unrelated external changes. No Azure mutating command was issued.

Owner disposition: retain current single-container WP01 design while resolving evidence; or authorize a separately scoped read-only continuation to resolve effective policy/deny applicability and define an acceptable capacity criterion for empty shells versus future research workload. Do not change architecture, allocate paid resources or use probe creation as the verification mechanism. Any later two-app design needs a separate service-to-service security/network decision. No WP02 issue closure or acceptance is requested.

## Publication scope and validation

Exactly three files in this dedicated evidence PR: this document; the executed Azure prompt; RELEASE_1.15_FILE_MANIFEST.md append-only index. All output UTF-8/no BOM/LF/final newline; relative links and git diff --check verified before commit. No product tests/builds run; no governance, WP01/WP02 plans, application source or deployment configuration changed. PR remains open/unmerged. `PUBLIC_RUNTIME_VERIFIED=False`.

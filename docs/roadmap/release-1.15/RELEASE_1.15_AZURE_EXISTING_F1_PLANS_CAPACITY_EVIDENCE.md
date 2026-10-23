# Release 1.15 - Existing Azure F1 plan capacity assessment

Date: 2026-10-23. **INITIAL ASSESSMENT: provisioning blocked; superseded by the dated empty-shell creation evidence below.** `PUBLIC_RUNTIME_VERIFIED=False`. This is infrastructure evidence only; no Release1.15 architecture amendment, WP02 implementation, provider call or deployment.

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

## Follow-up authority and pre-create validation - 2026-10-23

[New executed owner authority](prompters/release-1.15-codex-create-two-existing-plan-f1-apps.md) supersedes the earlier future CPU/RAM headroom gate for empty shells only. The prior BLOCKED assessment above remains historical; it is not a finding of incompatibility. Actual policy/permission/ARM validation remains required. Current subscription remains the exact authorized Enabled subscription; no context switch.

Re-queried both exact plans via ARM: F1, reserved=true, correct regions, one app each. Nominal documented10-app limit leaves app-count room. Regional F1 instances remain2/32 West Central US and1/31 West US2; no new plan/worker allocation is proposed. Both old apps remain attached/Running in baseline observations. Permissions at both groups include actions=* with no exclusions; subscription and group deny-assignment queries empty; subscription locks query empty; no extra group/plan policy assignment found. The inherited SecurityCenterBuiltIn initiative was resolved against actual definitions/default parameters:14 Disabled,107 AuditIfNotExists,95 Audit effects, total216, no Deny/Modify/DeployIfNotExists. No automatic policy deployment effect is selected. ARM Provider validation supplies additional exact-resource permission/policy/provider checks; this is not a universal future-policy guarantee.

Both exact one-resource templates passed `az deployment group validate --validation-level Provider`, exit0, error=null, provisioningState=Succeeded, before creation. These validation results do not mean sites already exist. No deployment-create operation used. Production validation templateHash223583418337200373; staging1894376322856501043. Each template contains only one Microsoft.Web/sites2025-03-01 resource, kind app,linux, exact existing serverFarmId, reserved=true, siteConfig.alwaysOn=false; no image/runtime/artifact/secrets/identity/network resource. Check-name-availability returned true for both names below.

| Role | Available proposed name | Full proposed site ID | Exact target plan ID |
| --- | --- | --- | --- |
| prod | aiqr115researchprodwcus7f5eabb5 | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/sites/aiqr115researchprodwcus7f5eabb5` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp03-wcus-5ec325382770` |
| staging | aiqr115researchstagingwus27f5eabb5 | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5` |

These names/IDs and validation evidence were recorded before mutation. Revised empty-shell acceptance gates PASS; future deployed-workload capacity, runtime security and cross-app networking remain unqualified. Next step is exactly two site PUTs, sequentially, no paid/new-plan fallback; partial success must be preserved and reported.

Read-only tooling issues retained: PowerShell5.1 redirection wrote local policy JSON as UTF-16, so the initial UTF-8 parser failed; decoding the actual file encoding resolved it. Azure CLI plan show returned null reserved/sites projections; direct ARM properties verified Linux and app count instead. Neither was treated as failed Azure eligibility or grounds for weakening a gate. No existing app-setting values or owner credentials were read/published.

## Actual provisioning and post-create verification - 2026-10-23

**Exactly two authorized empty Linux Web Apps CREATED.** Each sequential `az rest --method put --url https://management.azure.com{exactSiteId}?api-version=2025-03-01 --body @site-body.json` exited0; bodies were the validated single-site location/kind/properties only, not deployment templates that create other resources. No retry, paid fallback, plan creation/resize or existing-site PUT was issued. Direct GETs independently confirmed both site resources exist; validation alone is not the creation evidence.

| Role | Created name / hostname | Site ID | Verified plan | Region | State / provisioningState |
| --- | --- | --- | --- | --- | --- |
| prod | aiqr115researchprodwcus7f5eabb5.azurewebsites.net | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/sites/aiqr115researchprodwcus7f5eabb5` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp03-wcus-5ec325382770/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp03-wcus-5ec325382770` | West Central US | Running / null (not returned) |
| staging | aiqr115researchstagingwus27f5eabb5.azurewebsites.net | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5` | `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5` | West US 2 | Running / null (not returned) |

Both sites: kind app,linux; reserved=true; alwaysOn=false; linuxFxVersion empty; identity=null. No image, application deployment, data/provider connection, key/credential, DNS/access-rule/auth setting or identity grant was supplied. Their default hostnames can be public; no claim of private ingress, working research service or successful HTTP runtime is made. No browser/product/runtime test was performed. GET provisioningState is null, so no fabricated Succeeded site field is reported; site PUT success, resource existence and Running management state are the observed outcomes.

Both original apps' projected id/kind/serverFarmId/state compare exactly to pre-create snapshots and remain Running. Both exact plans remain F1, reserved=true, correct regions; site counts legitimately increased1 to2. Plans are not byte-identical because adding an app changes membership; no direct plan configuration mutation occurred. Each group resource inventory now contains exactly the existing plan, existing app and authorized new app, no additional registry/network/resource group/plan/identity resource. This is an observed final inventory, not a full historical audit or exclusion of outside actors. No existing secrets/settings were read for a full-config comparison.

The revised empty-shell infrastructure objective is complete. Future CPU/RAM/shared quota performance remains unmeasured and was explicitly removed as an empty-shell gate, not certified. Service-to-service authentication/confidentiality, public-host restrictions, cross-app storage/coordination and network architecture remain undecided. Separate apps never share127.0.0.1. WP01 single-image engineering baseline and WP02 plans are unchanged; #368 remains OPEN. No governance amendment, provider activation, deployment or research runtime acceptance is implied. `PUBLIC_RUNTIME_VERIFIED=False`.

## Follow-up publication validation

Follow-up changes exactly this evidence document, the manifest index and the new executed provisioning prompt. Overall PR #381 also retains the original assessment prompt. All original dirty/untracked WP02 and owner files retain their snapshotted hashes/status. UTF-8/no BOM/LF/final newline, relative Markdown links, exact-file scope, staged blobs and git diff --check checked before commit. No product builds/tests. PR stays OPEN and UNMERGED for owner review.

# Release 1.13 --- Terra Dual-Region Azure Web App Deployment Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this bounded operational deployment. Release 1.13
remains frozen. GPT-5.6 Luna remains governance/architecture authority
if a material boundary must change; GPT-5.6 Sol may support analysis
only.

## Frozen and deployment boundaries

Frozen Release 1.13: - tag `v1.13.0` - accepted release commit
`28aeb4961e505a4f8b89baf8832bb1064c07d033`

Do not move, recreate, or reinterpret `v1.13.0`.

Authorized post-release deployment source:
`91eb0605ff8be05ca61ba2198afa42100ab16d53`

Source PR: `#309`

PR #309 adds the Streamlit title `AI Quant Trading Research`, favicon
support, `favicon.ico`, and `imgs/icon.png`.

Before mutation verify this exact commit exists in `origin/main`. If
main advanced, do not silently deploy later source: build exact commit
`91eb0605ff8be05ca61ba2198afa42100ab16d53` unless new authority changes
the boundary.

## Purpose

Build one immutable application container from the authorized commit,
publish it once to the established public/free GHCR repository, resolve
its digest, deploy the **same digest** to both existing Azure regional
web apps, and independently validate both public endpoints.

## Regional rule

Both regions are valid and separate contexts.

The known West Central US target is: - App Service
`aiqr112wp035ec325382770` - plan `asp-aiq-r112-wp03-wcus-5ec325382770`

West US 2 remains a valid project deployment/recovery region.

Do not guess the West US 2 resource names. Discover them from
authoritative Azure/repository evidence before mutation. Do not imply
either region globally supersedes the other.

## Mandatory discovery gate

Before mutation record for both existing targets: -
subscription/context; - resource group; - App Service; - App Service
Plan; - region; - OS; - SKU/tier; - running state; - current container
image identity where observable; - public HTTPS endpoint.

One target must verify as West Central US and one as West US 2.

If no valid existing West US 2 target exists, stop. Infrastructure
creation is not authorized.

## Cost/infrastructure boundary

Zero new infrastructure and zero tier expansion.

Do not create/delete/move an App Service, plan, resource group,
registry, or other infrastructure. Do not change region, SKU/tier, or
enable paid recurring features. If deployment requires any such action,
stop at the governance boundary.

## Repository boundary

No repository mutation is authorized. Do not modify, commit, push, or
create a PR for README, source, tests, dependencies, schema,
Docker/build/deployment/configuration files, roadmap files,
`favicon.ico`, `imgs/icon.png`, or any other path.

## Immutable build

Build from exact commit: `91eb0605ff8be05ca61ba2198afa42100ab16d53`

Use the established Docker build and GHCR workflow. Create a unique
image tag identifying this deployment/source commit; do not overwrite
historical accepted tags.

Record: - full GHCR image reference; - tag; - immutable digest; -
successful digest retrieval.

The digest is the authoritative dual-region artifact identity.

## Same-digest invariant

Deploy that exact same image digest to: 1. existing West Central US web
app; 2. existing West US 2 web app.

Never rebuild independently between regions.

If Azure configuration requires a tag, prove both configured references
resolve to the same recorded digest at deployment time. Final acceptance
cannot contain different digests.

## Secrets and providers

Use existing authorized secret/configuration mechanisms only. Never
print, retrieve for reporting, commit, echo, rotate, or expose secret
values.

Preserve: - public historical path: Vike/cache; - BTC/USD and ETH/USD; -
Twelve Data private/internal research role; - no anonymous public Twelve
Data fallback.

Do not change provider configuration to make deployment pass. If
credentials/configuration require mutation outside existing deployment
mechanisms, stop.

## Azure execution

For each target, reverify identity/region immediately before mutation.
Change only the container image reference needed for this artifact and
restart only as required by the normal App Service deployment mechanism.
Wait for stable running state and inspect bounded runtime evidence
without exposing secrets.

No unrelated App Service settings may change.

## Public acceptance --- independently on both regions

Verify: - public HTTPS page loads; - browser/page title is exactly
`AI Quant Trading Research`; - favicon is served/visible without
application error; - Market Research default is BTC/USD, 1h, 30D; -
candlesticks and distinct volume render; - Vike Historical OHLCV
provenance and UTC last-updated evidence appear; - ETH/USD renders; -
`ETH/USD / 4h / 90D` renders, then default `BTC/USD / 1h / 30D` can be
restored; - Market Research, ML & Automation Studies, and System Health
navigation works; - ML surface remains informational; - System Health
remains controlled and does not reintroduce unsupported
`st.autorefresh`; - footer remains
`Research and demonstration application • No trade execution`.

Confirm no observed public exposure of API keys, credentials, traceback,
raw stderr/provider response, internal exception, `/app/...`,
Worker/cache/database/internal paths.

Do not intentionally break configuration to manufacture failure testing.

## Cross-region reconciliation

Record for each region: - region, App Service, plan, endpoint; -
configured image reference and immutable digest; - runtime state; -
title/favicon result; - BTC default result; - ETH result; - alternate
interval/range result; - provenance result; -
navigation/footer/disclosure result; - overall result.

PASS requires the same immutable digest in both regions.

## Regression protection

Confirm deployment did not alter README, repository source,
dependencies, schema, tests, provider architecture, Azure SKU/tier,
resource region, secrets, frozen `v1.13.0`, the Release 1.13 GitHub
Release, or Release 1.14 state.

No rebuild/redeployment beyond what is necessary for this exact
authorized artifact is permitted.

## Retry-until-governance-boundary rule

Do not stop merely because a build, push, Azure query, deployment,
restart, startup, health check, browser validation, container pull, or
tooling step fails. Diagnose, correct within this authority, rerun, and
continue until acceptance passes.

Ordinary failures are: `TECHNICAL FAILURE — CONTINUE FIXING`

Stop only if the next corrective action requires crossing authority:
new/deleted infrastructure, tier/cost change, repository mutation,
dependency/schema/architecture/provider change, unauthorized secret
mutation, different source commit, different final regional digests,
frozen-release mutation, or Release 1.14 work.

Governance stop: `GOVERNANCE RESTRICTION — STOPPED`

Report the exact restriction, failed evidence, and required corrective
action without performing it.

## No lifecycle mutation

Do not create/move a version tag, modify `v1.13.0`, modify the published
Release 1.13 GitHub Release, mutate milestones/issues merely for this
deployment, or create Release 1.14 lifecycle state.

## Required completion report

Return: - source commit; - build context; - image tag and immutable
digest; - GHCR result; - both resource groups/App
Services/plans/regions/SKUs; - pre- and post-deployment image identity
for both; - proof of identical final digest; - runtime and public
endpoint results; - title/favicon results; - BTC/ETH and
alternate-selection results; - Vike/UTC provenance results; -
navigation/footer/disclosure results; - zero-cost/tier preservation; -
repository/frozen-release protection; - technical failures corrected; -
governance restrictions, if any.

End with exactly one of:

`POST-1.13 DUAL-REGION AZURE WEB APP DEPLOYMENT: PASS`

or

`POST-1.13 DUAL-REGION AZURE WEB APP DEPLOYMENT: BLOCKED`

PASS requires both existing regional web apps to run the same immutable
artifact and pass independent validation. This authority does not
unfreeze Release 1.13 or authorize Release 1.14 implementation.

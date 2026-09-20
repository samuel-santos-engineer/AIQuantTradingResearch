# Release 1.13 --- Terra Fresh Dual-Region Azure Web App Deployment Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this bounded deployment operation. GPT-5.6 Luna
remains governance/architecture authority if a material boundary must
change. GPT-5.6 Sol may support analysis only.

## Frozen release and fresh source boundary

Release 1.13 remains frozen:

-   tag: `v1.13.0`
-   accepted release commit: `28aeb4961e505a4f8b89baf8832bb1064c07d033`

Do not move, recreate, retag, or reinterpret `v1.13.0`.

This fresh deployment authority is bound **exclusively** to canonical
source commit:

`8f459f75fbf11b689c430bf43fb335b9be0f455e`

That commit includes: - PR #309 presentation/title/favicon assets; - PR
#310 Dockerfile packaging correction.

PR #310 publication evidence: - branch commit
`4c891793f8c225aa22aefed797f4748e26563a78`; - changed path `Dockerfile`
only; - Dockerfile copies `favicon.ico` and `imgs/icon.png` into
`/app/`; - canonical merge commit
`8f459f75fbf11b689c430bf43fb335b9be0f455e`.

The previous deployment authority bound to `91eb0605...` is superseded
for execution purposes and must not be used.

If `origin/main` advances, do not silently deploy the later state. Build
exact commit `8f459f75fbf11b689c430bf43fb335b9be0f455e` unless new
authority changes this boundary.

## Purpose

Build exactly one immutable application container from the authorized
canonical commit, publish it once through the established public/free
GHCR mechanism, resolve its immutable digest, deploy that **same
artifact/digest** to both existing Azure regional web apps, and
independently validate both public endpoints.

No repository mutation is authorized.

## Mandatory pre-deployment identity gate

Before any GHCR or Azure mutation, record:

-   local `main` SHA;
-   `origin/main` SHA;
-   working-tree status;
-   proof canonical history contains
    `8f459f75fbf11b689c430bf43fb335b9be0f455e`;
-   proof PR #309 and PR #310 are merged/reachable;
-   proof `v1.13.0` still resolves to
    `28aeb4961e505a4f8b89baf8832bb1064c07d033`;
-   proof the authorized Dockerfile includes the required favicon/icon
    packaging;
-   proof no repository modification is required to build.

Untracked authority documents may remain present but must be untouched
and excluded from build provenance where appropriate.

## Regional rule

Both regions are valid, distinct resource contexts.

Known West Central US target: - App Service: `aiqr112wp035ec325382770` -
App Service Plan: `asp-aiq-r112-wp03-wcus-5ec325382770`

West US 2 is also a valid project deployment/recovery region.

Do not guess West US 2 resource names. Discover the existing target from
authoritative Azure/repository evidence.

Do not rewrite evidence to imply West Central US invalidates West US 2
or vice versa.

## Mandatory Azure discovery gate

Before mutation, identify and record both **existing** targets:

-   subscription/context;
-   resource group;
-   App Service;
-   App Service Plan;
-   region;
-   OS;
-   SKU/tier;
-   current running state;
-   current configured container image identity where observable;
-   public HTTPS endpoint.

Verify one target is West Central US and one is West US 2.

If there is no existing valid West US 2 App Service target, stop.
Infrastructure creation is forbidden.

## Cost and infrastructure boundary

This authority permits no new infrastructure and no paid-tier expansion.

Do not: - create/delete/move App Services; - create/delete/move App
Service Plans; - create resource groups or registries; - change
region; - change plan/SKU/tier; - enable paid recurring features; -
introduce new recurring-cost resources.

Preserve the existing zero-cost/free-tier governance constraints.

If deployment requires a cost/tier/infrastructure change, stop.

## Repository boundary

No tracked or untracked repository mutation is authorized.

Do not modify, stage, commit, push, or create a PR for: - README; -
Dockerfile; - favicon/icon assets; - application source; - tests; -
dependencies; - schema; - workflows; - deployment/configuration files; -
governance/roadmap files; - Release 1.14 material; - any other
repository path.

The repository is immutable input for this authority.

## Immutable image build and publication

Build from exact commit:

`8f459f75fbf11b689c430bf43fb335b9be0f455e`

Use the established Docker build context and existing GHCR publication
mechanism.

Create a unique image tag tied clearly to this post-release
deployment/source commit. Do not overwrite accepted historical tags in a
way that obscures provenance.

After publication record: - full GHCR image reference; - tag; -
immutable digest; - successful digest resolution.

Confirm the published image contains the required runtime assets: -
`/app/favicon.ico`; - `/app/icon.png`; - expected Streamlit presentation
application.

Do not publish separate builds for each region.

## Same-artifact invariant

Deploy the exact same immutable image artifact to:

1.  existing West Central US web app;
2.  existing West US 2 web app.

The immutable digest is authoritative.

If Azure accepts a tag rather than a digest reference, prove both final
configured references resolve to the same recorded digest at deployment
time.

Different final regional digests are not acceptable.

## Secret boundary

Use existing authorized secret/configuration mechanisms only.

Never print, echo, expose, commit, or include secret values in evidence.

Do not rotate or replace Vike/Twelve Data credentials.

If a missing/invalid secret requires credential or secret mutation
beyond the existing deployment mechanism, stop and report the governance
restriction.

## Provider/data boundary

Preserve: - public historical market path: Vike/cache; - BTC/USD and
ETH/USD; - Twelve Data: private/internal research role; - no anonymous
public Twelve Data fallback.

Do not change provider/data configuration to make deployment pass.

## Azure execution

Immediately before each regional mutation, reverify resource identity,
region, plan, and SKU.

Change only the container image reference necessary to run the
authorized artifact.

Restart only if required by the normal App Service deployment mechanism.

Wait for stable running state and inspect bounded startup/runtime
evidence without exposing secrets.

Do not change unrelated App Service settings.

## Public browser acceptance --- both regions independently

### Identity

Confirm: - public HTTPS endpoint loads; - browser/page title is exactly
`AI Quant Trading Research`; - favicon is successfully served/visible; -
no favicon-related application error occurs.

### Market Research

Confirm default: - Market Research; - BTC/USD; - 1h; - 30D; -
candlesticks; - distinct volume; - Vike Historical OHLCV provenance; -
UTC last-updated evidence.

Confirm ETH/USD renders valid historical candlestick/volume data.

Validate: `ETH/USD / 4h / 90D`

Then restore and verify: `BTC/USD / 1h / 30D`

### Navigation and boundaries

Confirm: - Market Research; - ML & Automation Studies; - System Health.

ML & Automation Studies remains informational only.

System Health remains controlled and does not reintroduce unsupported
`st.autorefresh`.

Confirm footer:
`Research and demonstration application • No trade execution`

### Disclosure/security

Confirm no observed public exposure of: - API key or credential; -
traceback; - raw stderr; - raw provider response; - internal
exception; - `/app/...` internal path; - Worker/cache/database internal
path.

Do not intentionally break configuration to manufacture failure
evidence.

## Cross-region reconciliation

Record for each region: - region; - resource group; - App Service; - App
Service Plan; - SKU/tier; - endpoint; - pre-deployment image identity; -
final configured image identity; - immutable digest; - runtime state; -
title result; - favicon result; - BTC default result; - ETH result; -
alternate interval/range result; - provenance result; -
navigation/footer/disclosure result; - overall result.

PASS requires both final regional targets to resolve to the same
immutable digest.

## Regression/frozen-boundary protection

Confirm this operation did not alter: - README; - repository source; -
Dockerfile; - dependencies; - schema; - tests; - provider
architecture; - Azure resource region; - Azure plan/SKU; - secrets; -
`v1.13.0`; - Release 1.13 GitHub Release; - Release 1.13
milestone/issues; - Release 1.14 state.

## Retry-until-governance-boundary rule

Do not stop merely because a build, GHCR push, Azure query, deployment,
container pull, restart, startup, health check, browser validation, or
tooling operation fails.

Diagnose, correct within this authority, rerun relevant checks, and
continue until acceptance passes.

Ordinary failures are:

`TECHNICAL FAILURE — CONTINUE FIXING`

Stop only when the next correction requires crossing authority,
including: - repository mutation; - building/deploying another source
commit; - new/deleted/moved Azure infrastructure; - tier/SKU/cost
change; - dependency/schema/architecture/provider change; - unauthorized
secret mutation; - accepting different final regional
artifacts/digests; - frozen Release 1.13 lifecycle mutation; - Release
1.14 work.

Governance stop:

`GOVERNANCE RESTRICTION — STOPPED`

Report the exact restriction, failed evidence, and required corrective
action without performing it.

## No release-lifecycle mutation

Do not: - create/move/change a version tag; - modify `v1.13.0`; - modify
the published Release 1.13 GitHub Release; - mutate Release 1.13
milestone/issues; - create Release 1.14 lifecycle state.

## Required completion report

Return: - exact authorized source commit; - pre-deployment repository
identity; - build context/command; - image tag; - immutable digest; -
GHCR publication result; - confirmation runtime image contains
favicon/icon/application; - West Central US resource group/App
Service/plan/SKU; - West US 2 resource group/App Service/plan/SKU; -
pre-deployment image identity for both; - final configured image
identity for both; - proof both resolve to the same immutable digest; -
runtime/startup result for both; - public endpoints; - title and favicon
validation for both; - BTC/USD default validation for both; - ETH/USD
validation for both; - `ETH/USD / 4h / 90D` validation for both; -
restored default validation for both; - Vike/UTC provenance validation
for both; - navigation/footer/disclosure validation for both; -
zero-cost/tier preservation; - repository/frozen-release protection; -
technical failures and corrections; - governance restrictions, if any.

End with exactly one of:

`POST-1.13 FRESH DUAL-REGION AZURE WEB APP DEPLOYMENT: PASS`

or

`POST-1.13 FRESH DUAL-REGION AZURE WEB APP DEPLOYMENT: BLOCKED`

PASS requires both existing regional web apps to run the same immutable
artifact derived from exact commit
`8f459f75fbf11b689c430bf43fb335b9be0f455e` and to pass independent
public validation.

This authority does not unfreeze Release 1.13 and does not authorize
Release 1.14 implementation.

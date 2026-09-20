# Release 1.13 WP07 --- Terra Azure F1 Integration, Stability, Security, and Zero-Cost Validation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Luna owns the Release 1.13 contract and later substantive WP07
acceptance. GPT-5.6 Terra owns bounded integration/validation
implementation under this authority. GPT-5.6 Sol may support analysis
only and may not expand authority.

## Canonical predecessor

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Required canonical `origin/main`:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

This SHA contains WP01-WP06 accepted, merged, and lifecycle-complete.

Expected governance state:

-   issue #293 Closed / WP06 Done;
-   issue #294 Open / WP07 non-Done;
-   issue #295 Open;
-   milestone #64 Open;
-   Project Release field `1.13`;
-   no Release 1.13 tag or GitHub Release.

Verify live truth before mutation.

## Work package

**WP07 --- Azure F1 integration, stability, bounded-resource, no-bypass,
security, and zero-recurring-cost validation**

Canonical execution-plan purpose:

> Validate Azure F1 integration, stability, bounded resources, no-bypass
> architecture, security, and zero recurring cost.

WP07 is not a feature-design work package. Its purpose is to make the
already accepted Release 1.13 system demonstrably operable within the
existing F1 deployment topology and to validate that topology.

## Existing accepted architecture

Preserve:

`Streamlit -> bounded local Worker invocation -> HistoricalQuery mode -> HistoricalMarketDataReadService -> cache -> Vike when governed -> presentation-safe JSON -> Streamlit`

Preserve the legacy Worker/read-model behavior used by System Health.

Do not replace this architecture with a persistent HTTP service, broker,
database coordination, Python provider client, or paid infrastructure.

## Current F1 constraints

Treat the established constraints as hard design inputs:

-   Azure App Service Linux F1;
-   shared capacity;
-   approximately 60 CPU minutes/day;
-   approximately 1 GB storage;
-   throttling/cold starts/no SLA;
-   persistent `/home`;
-   SQLite DELETE journal mode where applicable;
-   custom Docker;
-   public/free GHCR;
-   recurring infrastructure target `$0.00`.

Do not introduce a paid Azure resource, managed cache, queue, database,
monitoring service, or data feed.

## Purpose

WP07 must establish evidence that the accepted WP05/WP06 application can
operate under the existing F1 deployment model.

This includes, as necessary and explicitly bounded:

-   Docker/runtime integration for the fixed Worker path;
-   startup/runtime composition;
-   `/home` cache persistence assumptions;
-   Vike server-side configuration wiring;
-   Streamlit/Worker co-location;
-   cold-start behavior;
-   bounded subprocess/resource behavior;
-   controlled provider/cache outage behavior;
-   security/no-secret/no-bypass validation;
-   deployment smoke validation against the existing F1 app;
-   zero-recurring-cost evidence.

WP07 does not add new product features.

## Mandatory inspection before mutation

Inspect at minimum:

1.  Release 1.13 WP01 contract and execution plan;
2.  WP05 bridge architecture;
3.  WP05 and WP06 accepted implementation;
4.  Worker project output/publish layout;
5.  all Dockerfiles/container build files;
6.  startup/entrypoint scripts;
7.  Azure/App Service deployment workflows/scripts;
8.  GHCR workflow/configuration;
9.  `.github/workflows/` relevant to deployment/build;
10. environment/configuration conventions and secret references;
11. current Python requirements;
12. cache path behavior;
13. SQLite/persistent `/home` behavior;
14. existing deployment/runbook documentation;
15. existing F1 qualification tests/probes;
16. issue #294, Project state, milestone state.

Do not expose secret values while inspecting configuration.

## Exact-literal allowlist gate

**Before first mutation**, report and freeze the smallest exact literal
mutation allowlist.

No directory wildcard.

Expected categories, not automatic authorization:

-   minimum Docker/container path(s) required to ensure the accepted
    Worker DLL is present at
    `/app/worker/AIQuantTradingResearch.Worker.dll`;
-   minimum startup/runtime path(s), if required;
-   focused deployment/integration validation scripts/tests;
-   existing Azure/GHCR workflow only if a change is demonstrably
    required;
-   WP07 runbook/validation evidence document if consistent with
    repository precedent;
-   `RELEASE_1.13_FILE_MANIFEST.md`;
-   this Terra authority record.

Do not mutate any path merely because it is in an expected category.

If a necessary change falls outside the frozen literal allowlist, stop
at governance unless it is frozen before mutation as part of the initial
smallest justified set.

## First principle --- inspect before changing deployment

The existing deployment may already package the Worker correctly.

Prove whether the built container contains:

`/app/worker/AIQuantTradingResearch.Worker.dll`

and whether Streamlit can invoke it.

Do not modify Docker/startup merely to create activity.

If the existing image is already correct, prefer validation-only work.

## Runtime integration requirements

Validate:

-   Streamlit and Worker execute in the same container/runtime where
    expected;
-   `dotnet` is available to the Streamlit process;
-   fixed Worker DLL path exists and is executable through `dotnet`;
-   Worker historical mode exits after one request;
-   no persistent companion service;
-   subprocess timeout/reaping remains bounded;
-   malformed requests remain controlled;
-   stdout protocol remains clean;
-   System Health legacy path remains available.

## Cache and persistence

Validate the accepted historical cache path:

`/home/aiq-market-cache`

Requirements:

-   compatible with App Service persistent `/home`;
-   bounded storage behavior;
-   atomic replacement;
-   existing retention remains effective;
-   no cache growth without bound;
-   no schema/database migration;
-   restart/cold-start does not corrupt valid cache;
-   stale cache remains usable under governed failure behavior.

Do not create a second Python cache.

## Vike configuration

Validate server-side Vike configuration without exposing credentials.

Requirements:

-   API key/configuration remains server-side;
-   no browser/Python exposure;
-   no checked-in secret;
-   no raw provider response in public UI;
-   no Twelve Data public fallback;
-   Vike public historical path remains the only public acquisition
    provider.

If live Vike validation requires a secret that is not already available
through the governed environment, do not invent or expose it. Report the
exact configuration boundary.

## Resource boundedness

Validate F1-appropriate behavior:

-   no background high-frequency polling;
-   no per-hover/zoom/pan provider call;
-   unchanged selection does not repeatedly spawn acquisition;
-   one-shot Worker process exits;
-   lock wait bounded;
-   35-second lease preserved;
-   Python timeout bounded;
-   request/response payload bounded;
-   cache retention bounded;
-   no persistent local API daemon;
-   no unnecessary concurrent Worker fan-out.

Where practical, collect lightweight evidence of process
duration/memory/container behavior without adding monitoring
infrastructure.

Do not claim production capacity or SLA from a small smoke test.

## Cold-start and restart behavior

Validate controlled behavior when:

-   App Service/container starts cold;
-   no historical cache exists;
-   valid cache exists;
-   cache is stale;
-   provider unavailable;
-   Worker invocation fails;
-   System Health handoff is not yet published.

The public application must fail safely and recover without internal
leakage.

## Public UI smoke contract

Against the deployed/runtime-equivalent application, validate as far as
the environment permits:

-   app starts;
-   Market Research is first/default;
-   BTC/USD default;
-   1h default;
-   30D default;
-   BTC/USD and ETH/USD selectable;
-   1h/4h/1d selectable;
-   1D/7D/30D/90D selectable;
-   candlestick and volume render when data available;
-   provenance is Vike with UTC last-updated;
-   stale/empty/unavailable states controlled;
-   Twelve Data boundary visible;
-   ML & Automation Studies informational surface visible;
-   System Health reachable;
-   no-trade footer visible;
-   no secret/internal error rendered.

Do not add browser automation dependency solely for this validation.

## Live deployment mutation boundary

WP07 may validate the existing F1 deployment and may perform only
deployment actions already established by repository workflow if
necessary to validate the accepted candidate.

Before any live deployment action:

1.  identify the exact existing workflow/mechanism;
2.  identify image/tag/revision being deployed;
3.  prove it uses existing zero-cost resources only;
4.  prove no infrastructure creation/upgrade;
5.  prove no secret value will be printed;
6.  record rollback/recovery path.

Do not create new Azure resources.

Do not change App Service plan/tier.

Do not purchase capacity.

Do not mutate DNS/custom domains.

Do not delete production/reference resources.

If repository governance reserves actual deployment for a later separate
lifecycle authority, stop and report rather than assuming permission.

## GHCR boundary

Use only the existing public/free GHCR pattern.

Do not create paid registries or unrelated packages.

Do not delete historical images unless separately authorized.

## Security validation

Run focused scans/proofs for:

-   Vike/Twelve Data keys absent from repository/public bundle;
-   no provider URL/key in browser-facing code except approved display
    text;
-   no shell interpolation;
-   fixed Worker invocation;
-   no arbitrary command/path from user input;
-   bounded protocol;
-   no raw exception/stderr;
-   no filesystem path leakage;
-   no cache traversal;
-   no HTTP provider bypass in Python;
-   Twelve Data unchanged/private/internal.

## Dependency boundary

No new Python or .NET package is expected or authorized.

Preserve:

-   numpy==2.5.1
-   pandas==3.0.5
-   scikit-learn==1.9.0
-   streamlit==1.61.1
-   plotly==7.1.0

If a new dependency is required, stop at governance.

## Schema boundary

No schema change or migration is authorized.

## Product boundary

Do not add:

-   indicators;
-   Release 1.14 features;
-   ML models/features/training;
-   signals;
-   forecasts;
-   strategies;
-   backtesting;
-   trading/order controls;
-   automated trading.

## Required validation

At minimum run/report applicable evidence for:

### Repository/build

-   Release build, 0 warnings/errors target;
-   .NET regression suites;
-   Python presentation suite;
-   Python compile;
-   `pip check`;
-   dependency preservation;
-   `git diff --check`.

### Container/runtime

-   container/image build or equivalent deterministic Docker validation;
-   Worker DLL path proof;
-   `dotnet` availability;
-   Streamlit startup proof;
-   historical Worker smoke request;
-   malformed-request controlled response;
-   process exit/reaping proof.

### Cache

-   `/home` path compatibility;
-   fresh cache;
-   stale cache;
-   missing cache;
-   provider failure with stale cache;
-   bounded retention/storage behavior.

### Architecture/security

-   provider no-bypass scan;
-   secret scan;
-   fixed invocation;
-   no shell;
-   no Python provider client;
-   no Twelve Data public fallback;
-   no internal leakage.

### F1

-   no persistent companion;
-   bounded subprocess behavior;
-   no high-frequency provider polling;
-   no paid resource;
-   zero-recurring-infrastructure-cost proof based on actual resource
    topology/configuration.

### Deployment/runtime smoke

If governed live validation is permitted, record: - deployed
revision/image; - app URL; - HTTP/startup result; - public surface smoke
results; - controlled failure behavior; - no secret/internal leakage.

If live validation is blocked by governance or unavailable credentials,
distinguish that from an implementation defect.

## Documentation

Record WP07 evidence in the smallest repository-native artifact
consistent with existing Release 1.13/earlier release precedent.

If a new WP07 validation document is needed, freeze its exact path
before mutation and explicitly select **GPT-5.6 Terra** where it is a
Codex control/prompter artifact.

Authority record path:

`docs/roadmap/release-1.13/prompters/release-1.13-wp07-terra-azure-f1-integration-stability-security-zero-cost-validation.md`

Update `RELEASE_1.13_FILE_MANIFEST.md` only as necessary.

## Root README protection

`README.md` remains byte-for-byte unchanged.

## Git workflow

Create a dedicated WP07 branch from:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

Create a bounded PR against `main` for any tracked WP07
changes/evidence.

Do not merge it.

Issue #294 remains Open/non-Done during implementation/validation.

A candidate requires separate GPT-5.6 Luna substantive acceptance.

## Retry-until-governance-boundary rule

Do not stop merely because a build, test, Docker build, runtime smoke,
provider request, cache check, deployment probe, security scan, or
tooling step fails.

Diagnose and correct failures within the frozen exact allowlist and
authority, rerun checks, and continue.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

Stop only if the next corrective action requires crossing authority,
including:

-   new dependency;
-   schema migration;
-   architecture redesign;
-   provider-contract redesign;
-   unexpected unapproved path;
-   paid resource;
-   Azure plan/tier change;
-   new managed service;
-   secret exposure;
-   README;
-   Release 1.14/2.0 feature work;
-   WP08;
-   merge/tag/GitHub Release/milestone closure.

Report exact restriction, failed evidence, and required authority.

## Candidate PASS boundary

A WP07 PASS means:

-   the bounded implementation/validation candidate is ready for Luna
    substantive acceptance;
-   the F1/runtime evidence required by the work package has been
    collected to the extent authorized;
-   no governance boundary has been silently crossed.

It does **not** authorize merge, issue closure, WP08, tag, milestone
closure, or GitHub Release.

## Required final report

Return:

-   canonical predecessor;
-   issue #294/Project state;
-   inspected deployment/runtime paths;
-   exact pre-mutation literal allowlist;
-   exact changed paths;
-   whether Docker/startup mutation was actually necessary;
-   Worker DLL/path evidence;
-   Streamlit/Worker integration evidence;
-   cache `/home` evidence;
-   Vike configuration/security evidence;
-   resource-boundedness evidence;
-   cold-start/restart findings;
-   F1 zero-cost topology evidence;
-   live deployment/runtime smoke evidence or exact governance
    limitation;
-   build/.NET/Python test results;
-   secret/no-bypass/whitespace/dependency results;
-   README proof;
-   implementation/validation head;
-   PR number/state if created;
-   milestone #64 and issue #295 state;
-   technical failures corrected;
-   governance restrictions;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST VALIDATION: PASS`

or

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST VALIDATION: BLOCKED`

A PASS creates an unmerged WP07 candidate/evidence set only and does not
authorize merge, WP08, tag, milestone closure, or Release 1.13
publication.

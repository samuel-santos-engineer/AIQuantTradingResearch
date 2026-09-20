# Release 1.13 WP07 --- Terra Local Real-Credential Differential Diagnosis Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this zero-mutation local differential diagnosis.
GPT-5.6 Luna retains governance and final substantive acceptance
authority. GPT-5.6 Sol may support analysis only.

## Bound candidate

Canonical predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Candidate head:

`7eafa22e890b120278187c6e9f06033f1038f6ac`

Deployed candidate image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-7eafa22e890b120278187c6e9f06033f1038f6ac`

Deployed digest:

`sha256:83fde5ebda8135bbe654293257270c1970130982cd19bc05f17d2f9df0da3e2c`

The user has confirmed:

`local Vike__ApiKey is configured and Codex was restarted`

Treat this statement as authorization to let the existing
application/Worker use the local environment secret for diagnosis. It is
not authorization to inspect or disclose the secret value.

## Purpose

Run the exact Release 1.13 historical-data path locally with the real
Vike credential and compare its result with the deployed Azure behavior.

The primary question is:

**Does the exact candidate successfully obtain BTC/USD and ETH/USD
historical OHLCV locally when `Vike__ApiKey` is supplied through the
local environment?**

This is a differential diagnostic experiment.

It authorizes **zero tracked repository mutation** and **zero Azure
mutation**.

## Secret-handling boundary

`Vike__ApiKey` is a secret.

The application may consume it through the inherited environment.

Codex must never:

-   print it;
-   echo it;
-   interpolate it into visible command output;
-   display its length;
-   display any prefix/suffix;
-   hash it;
-   serialize it;
-   write it to a file;
-   put it in a command transcript;
-   put it in Docker build args/layers;
-   put it in Git;
-   place it in Markdown;
-   screenshot it;
-   log request headers containing it;
-   retrieve it through an environment dump;
-   send it anywhere except the Vike request made by the governed
    application/provider path.

Do not run commands such as:

-   `echo $env:Vike__ApiKey`
-   `set`
-   `Get-ChildItem Env:`
-   `docker inspect` if it would reveal environment values
-   any equivalent environment dump.

Safe presence check only:

`present/nonempty = true|false`

Do not reveal any other property of the value.

## Candidate integrity

Before testing:

-   verify current repository/candidate identity;
-   verify `7eafa22e890b120278187c6e9f06033f1038f6ac` is the intended PR
    #304 candidate;
-   do not modify or commit anything;
-   preserve all untracked authority artifacts;
-   do not clean/reset/delete unrelated work.

If the local worktree is not at the candidate, use a safe detached
worktree/container/image mechanism rather than mutating unrelated user
work.

## Preferred test order

### Test A --- Host/local Worker path

If the candidate can be run directly from the local build/runtime while
inheriting the user environment safely, invoke the existing one-shot
Worker `HistoricalQuery` contract.

Do not create a new Vike probe.

Exercise:

1.  BTC/USD, interval 1h, range 30D.
2.  ETH/USD, interval 1h, range 30D.

Capture only sanitized application result metadata:

-   success/failure;
-   typed failure category if failure;
-   candle count if success;
-   canonical symbol;
-   interval/range;
-   provenance/provider identity;
-   validation/freshness timestamp where already part of the safe
    contract;
-   Worker exit status.

Do not print raw candles unless needed; a count and safe metadata are
sufficient.

### Test B --- Exact candidate container/image

Preferably also test the exact candidate image/container so the
comparison includes the Linux/container runtime.

Inject the secret **only at container runtime** using a mechanism that
does not print it or bake it into the image.

Do not place the value literally in a committed script or visible
transcript.

Run the same existing Worker `HistoricalQuery` calls:

-   BTC/USD / 1h / 30D;
-   ETH/USD / 1h / 30D.

Again capture only sanitized result metadata.

If safe runtime injection cannot be performed without exposing the value
in the available tool transcript, do not perform that injection. The
host/local governed Worker test is still useful; report the limitation.

## Governed path only

The test must use:

`HistoricalQuery -> HistoricalMarketDataReadService -> governed cache -> VikeHistoricalMarketDataProvider`

Do not:

-   use curl directly against Vike with the key;
-   create a Python Vike client;
-   write an ad hoc HTTP probe carrying the key;
-   bypass the Worker/read service;
-   use Twelve Data fallback.

A direct unauthenticated connectivity check is unnecessary because prior
diagnosis already proved Vike DNS/HTTPS reachability and typed
authentication behavior with a synthetic key.

## Cache considerations

Avoid allowing an existing valid cache entry to falsely prove current
provider authentication.

Determine safely whether the query result is served from cache versus
newly acquired from Vike.

Use a temporary isolated local cache location if the existing
application supports selecting one through existing configuration
without source mutation and without violating the architecture.

If an isolated cache cannot be selected safely, report whether cache
state makes the provider-authentication conclusion ambiguous.

Do not delete production/Azure cache.

Do not modify deployed cache.

For the differential experiment, evidence of a fresh provider-backed
acquisition is preferred.

## Expected diagnostic branches

### Branch 1 --- Local real credential succeeds for BTC and ETH

If both instruments succeed through the governed provider path with
evidence of real Vike acquisition:

Conclude that:

-   the candidate Vike request/parse/canonicalization path works with
    the real credential in the tested local environment;
-   the credential is accepted by Vike in that test;
-   the deployed failure is therefore narrowed toward Azure runtime
    environment/configuration delivery, deployed cache/runtime behavior,
    or another Azure-specific execution difference.

Do not claim this alone proves Azure failed to inject the environment
variable.

Recommend the next narrow authority for deployed-container
configuration/runtime evidence.

### Branch 2 --- Local real credential returns AuthenticationOrConfiguration

If the real local credential receives the same typed
authentication/configuration failure:

Conclude only what the evidence supports.

Possible areas include:

-   credential validity/permissions;
-   Vike account/key activation;
-   provider authentication expectation;
-   local configuration binding.

Do not rotate/change the credential under this authority.

Report the exact typed result and minimum next diagnostic/configuration
action.

### Branch 3 --- Local real credential fails with another typed failure

Report the exact safe typed category and localize the layer.

If evidence indicates request shape, parsing, validation, Worker, cache,
or another source defect, identify the minimum exact repository paths
that a later corrective authority would require.

Do not mutate them.

### Branch 4 --- Host succeeds but exact container fails

This is especially valuable evidence.

Compare safe runtime/configuration behavior and identify the minimum
container/environment difference.

Do not change Docker/entrypoint under this authority.

### Branch 5 --- Test cannot safely consume the environment secret

Stop rather than exposing it.

Report exactly what prevents safe inherited-secret execution and the
minimum mechanism needed.

## No mutation authority

Do not change:

-   tracked source;
-   tests;
-   docs;
-   PR #304;
-   Azure;
-   App Service settings;
-   GHCR image;
-   deployment;
-   dependencies;
-   schema;
-   README;
-   issues;
-   Project status;
-   milestones;
-   tags;
-   GitHub Releases.

No new commit is authorized.

## Retry-until-governance-boundary rule

Do not stop merely because a local build, Worker invocation, container
start, cache setup, Vike acquisition, or diagnostic command initially
fails.

Diagnose and retry within this zero-mutation authority.

Stop only when further progress would require:

-   exposing the secret;
-   tracked mutation;
-   Azure/configuration mutation;
-   deployment mutation;
-   architecture bypass;
-   another explicit governance expansion.

## Required evidence

Report:

-   candidate full SHA;
-   whether local `Vike__ApiKey` presence was safely confirmed as
    true/false;
-   whether its value was ever displayed: must be `no`;
-   host/local Worker BTC/USD result;
-   host/local Worker ETH/USD result;
-   exact-container BTC/USD result, if safely performed;
-   exact-container ETH/USD result, if safely performed;
-   whether each successful result was cache-backed or provider-backed,
    where determinable;
-   typed failure category for every failure;
-   safe provenance evidence;
-   Worker exit evidence;
-   whether the local real credential is accepted by Vike through the
    governed application path;
-   differential conclusion versus deployed Azure unavailable state;
-   minimum next corrective/diagnostic authority;
-   exact repository paths needed if source mutation later becomes
    justified;
-   whether Azure configuration/runtime evidence is the remaining gap;
-   confirmation zero tracked/Azure/deployment/lifecycle mutation
    occurred;
-   PR #304 state;
-   issue #294/#295 and milestone #64 state.

## Completion marker

End with exactly one of:

`RELEASE 1.13 WP07 LOCAL REAL-CREDENTIAL DIFFERENTIAL DIAGNOSIS: PASS`

or

`RELEASE 1.13 WP07 LOCAL REAL-CREDENTIAL DIFFERENTIAL DIAGNOSIS: BLOCKED`

A PASS means the differential experiment produced sufficient evidence to
choose the next narrow action. It does not mean WP07 acceptance has
passed and does not authorize merge.

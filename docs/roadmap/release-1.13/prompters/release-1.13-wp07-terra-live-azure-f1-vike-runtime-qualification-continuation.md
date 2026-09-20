# Release 1.13 WP07 --- Terra Live Azure F1 Qualification Continuation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra resumes the blocked WP07 validation. GPT-5.6 Luna retains
later substantive acceptance authority. GPT-5.6 Sol may support analysis
only.

## Resumption basis

Canonical predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

Candidate PR:

`#304`

Expected branch:

`feature/release-1.13-wp07-azure-f1-validation`

Reported candidate head:

`5625f8a`

WP07 previously stopped correctly because the existing App Service
lacked the governed `Vike__ApiKey` setting.

The user has now confirmed:

`Vike__ApiKey is configured`

Treat that statement only as authorization to resume qualification.
Never print, retrieve, disclose, echo, persist, or commit the secret
value.

## Purpose

Resume WP07 only far enough to establish live Azure F1 qualification
evidence for the existing PR #304 candidate.

Primary goals:

1.  verify the setting name exists without reading its value;
2.  deploy the accepted WP07 candidate using the repository's existing
    governed deployment mechanism, if deployment is already within the
    established WP07 boundary;
3.  validate live Vike-success behavior;
4.  validate public runtime/UI behavior;
5.  validate security, boundedness, and zero-cost topology;
6.  leave PR #304 unmerged and issue #294 Open for separate Luna
    substantive acceptance.

This continuation does not authorize WP07 merge/lifecycle.

## Secret-handling rules

Never output the value of `Vike__ApiKey`.

Never:

-   query or print the secret value;
-   place it in command arguments that become logs where avoidable;
-   write it to a repository file;
-   write it to Docker build args/layers;
-   place it in GitHub Actions output;
-   expose it through Streamlit/browser;
-   include it in screenshots or evidence;
-   copy it into Markdown;
-   rotate/delete/replace it.

Only safe setting-name/existence evidence is required.

If a tool/API would necessarily return the secret value, do not invoke
that operation. Use a safe metadata/name-only mechanism.

## Candidate integrity gate

Before deployment, verify:

-   PR #304 is Open and unmerged;
-   branch matches expected branch;
-   current PR head matches the reported candidate or record the full
    SHA if `5625f8a` was abbreviated;
-   exact changed paths remain the frozen five-path WP07 allowlist;
-   no unauthorized drift;
-   README unchanged;
-   dependencies unchanged;
-   issue #294 Open/non-Done;
-   #295 Open;
-   milestone #64 Open;
-   no Release 1.13 tag/GitHub Release.

If substantive candidate drift exists, stop for renewed
authority/acceptance.

## Configuration gate

Safely verify:

-   existing App Service remains the intended West US 2 Linux F1/Free
    resource;
-   `Vike__ApiKey` setting name exists;
-   no secret value is displayed;
-   HTTPS-only remains enabled;
-   no paid-resource/topology mutation occurred.

Do not change plan/tier/resources.

## Deployment gate

Use only the repository's existing deployment/image workflow and
existing F1 App Service.

Before triggering deployment, record:

-   exact workflow/mechanism;
-   source commit/head;
-   intended image/revision/tag;
-   existing GHCR target;
-   existing App Service target;
-   proof no new resource or paid service is created.

Deploy only PR #304's WP07 candidate/equivalent validated tree.

Do not deploy arbitrary local/uncommitted files.

Do not merge PR #304 as a means of deployment unless merge is required
by existing workflow; if merge is required, stop because merge needs
separate lifecycle authority.

## Live runtime qualification

After deployment, validate the existing public app:

`https://aiqr112wp035ec325382770.azurewebsites.net/`

Validate:

-   HTTPS responds;
-   Streamlit application starts;
-   Market Research is the default;
-   BTC/USD default;
-   1h default;
-   30D default;
-   BTC/USD and ETH/USD selectable;
-   1h/4h/1d selectable;
-   1D/7D/30D/90D selectable;
-   historical candles and volume render from the governed Vike path;
-   provenance identifies Vike;
-   last-updated visibly communicates UTC;
-   no Vike key is exposed;
-   Twelve Data boundary messaging is visible;
-   ML & Automation Studies informational surface is reachable;
-   System Health is reachable;
-   no-trade footer is visible;
-   no stack trace, Worker path, cache path, provider HTTP body, raw
    stderr, or internal exception is exposed.

Where browser automation is unavailable, use the strongest
repository-native/runtime/API evidence available and state limitations
precisely.

## Vike-success qualification

Establish evidence that a real historical query succeeds through:

`Streamlit -> one-shot Worker -> HistoricalMarketDataReadService -> governed cache/Vike -> safe JSON -> Streamlit`

Do not validate Vike by creating a separate Python/provider call that
bypasses the architecture.

Prefer a Worker HistoricalQuery smoke request inside the
deployed/container-equivalent environment and/or evidence from the
actual public Market Research path.

Verify at least:

-   BTC/USD succeeds;
-   one additional matrix selection, preferably ETH/USD, succeeds;
-   response contains canonical candles;
-   provenance is Vike;
-   timestamp is UTC;
-   secret remains server-side;
-   process exits;
-   no public Twelve Data fallback.

## Cache/failure evidence

Validate, without destructive production manipulation where possible:

-   valid cache reuse;
-   repeated same selection does not imply provider call per UI
    interaction;
-   stale-cache semantics remain governed;
-   `/home/aiq-market-cache` is writable by `aiq`;
-   retention remains bounded;
-   restart/cold-start assumptions remain compatible.

Do not deliberately delete valuable live cache merely to prove a failure
path if existing automated/container tests already establish it.

## F1 resource validation

Confirm:

-   Linux F1/Free remains unchanged;
-   no additional Azure resource;
-   no paid plan/service;
-   no persistent companion service;
-   one-shot Worker exits;
-   no high-frequency polling;
-   no provider call on hover/zoom/pan;
-   cache remains bounded;
-   no new recurring infrastructure cost.

Do not overclaim SLA, capacity, or performance.

## Validation baseline

Preserve/reconfirm as applicable:

-   Release build: 0 warnings / 0 errors;
-   Python presentation suite: 35/35;
-   Python compile: pass;
-   `pip check`: pass;
-   PowerShell parser: 0 errors;
-   whitespace: pass;
-   secret scan: pass;
-   provider no-bypass: pass;
-   README/dependencies unchanged;
-   Worker DLL exists at
    `/app/worker/AIQuantTradingResearch.Worker.dll`;
-   `dotnet` available;
-   entrypoint syntax valid.

Rerun any check affected by deployment/runtime evidence.

## Mutation boundary

The five-path WP07 candidate scope remains frozen.

This continuation is primarily deployment/runtime validation.

Do not modify tracked files unless an ordinary technical defect is
discovered and the correction is already within those exact five paths.

If a correction changes the candidate head, commit/push to PR #304 and
report the new head; the final candidate will require Luna acceptance.

If correction requires a sixth path, dependency, architecture change,
schema, provider redesign, or new Azure resource, stop at governance.

## Retry-until-governance-boundary rule

Do not stop merely because deployment, cold start, HTTP startup, Vike
query, cache permission, container startup, test, scan, or runtime smoke
initially fails.

Diagnose and retry/correct within the existing five-path authority.

Examples of technical failures to continue fixing:

-   transient App Service cold start;
-   image pull/startup delay;
-   bounded Vike/network transient;
-   cache-directory permission issue within authorized entrypoint path;
-   existing deployment workflow invocation issue;
-   test/tooling failure.

Stop only when correction requires crossing governance.

Report technical fixes as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

A governance stop must report:

`GOVERNANCE RESTRICTION — STOPPED`

## Forbidden actions

Do not:

-   reveal or rotate the Vike key;
-   add another secret;
-   create Azure resources;
-   change F1 tier;
-   add paid services;
-   change provider contract;
-   add Twelve Data public fallback;
-   add dependencies;
-   change schema;
-   modify README;
-   implement indicators/ML/trading;
-   perform WP08;
-   merge PR #304;
-   close #294;
-   mark WP07 Done;
-   close milestone #64;
-   create tag/GitHub Release.

## Candidate completion boundary

A successful continuation converts the prior BLOCKED WP07 candidate into
a fully evidenced, still-unmerged WP07 candidate suitable for **GPT-5.6
Luna final substantive acceptance**.

PR #304 must remain open/unmerged.

Issue #294 must remain Open/non-Done.

## Required final report

Return:

-   canonical predecessor;
-   PR #304 branch/full head/state;
-   exact five changed paths;
-   safe `Vike__ApiKey` setting-name existence proof with no value;
-   Azure F1 topology proof;
-   deployment mechanism;
-   deployed candidate revision/image evidence;
-   public app HTTP/startup result;
-   Market Research live smoke result;
-   BTC/USD Vike-success evidence;
-   ETH/USD or second-selection success evidence;
-   provenance/UTC evidence;
-   cache `/home` evidence;
-   Worker process/path/exit evidence;
-   System Health/ML/footer/navigation smoke results;
-   secret/no-bypass/internal-leakage results;
-   zero-cost/bounded-resource evidence;
-   build/test/compile/dependency/whitespace results;
-   whether any technical correction changed the candidate head;
-   final candidate head;
-   issue #294/Project state;
-   #295/milestone #64 state;
-   PR #304 still unmerged;
-   no tag/GitHub Release;
-   governance restrictions, if any;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 LIVE AZURE F1 AND VIKE RUNTIME QUALIFICATION: PASS`

or

`RELEASE 1.13 WP07 LIVE AZURE F1 AND VIKE RUNTIME QUALIFICATION: BLOCKED`

A PASS does not authorize merge, issue closure, WP08, tag, milestone
closure, or Release 1.13 publication.

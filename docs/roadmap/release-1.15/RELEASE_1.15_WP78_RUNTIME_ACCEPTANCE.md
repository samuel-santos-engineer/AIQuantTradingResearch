# Release 1.15 WP78 - Azure staging runtime/browser acceptance

Date: 2026-11-21 (America/Sao_Paulo). **BLOCKED ON SERVING; NO MUTATIONS.**
Base fresh main `ab051b154a3485b3ab6ac40ee914daba4dbc9985` contains merged
#476. Separate clean evidence branch preserves owner edits/untracked prompts on
main. Existing governance, WP75/WP76/WP77 evidence and merged helper reviewed;
no previous findings rewritten. Dual-image Linux Docker, authenticated cross-app
contract, API52731/UI8501 and existing security/storage remain unchanged.

## Exact staging scope and current evidence

Subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, group
`rg-aiq-r112-wp05-wus2-7f5eabb5`, existing shared WestUS2 Linux F1 plan.
API `aiqr115researchstagingwus27f5eabb5`; UI `aiqr112wp05wus27f5eabb5`.
Production was not accessed. WP77 already proved private equal-key readback;
no key or settings collection was reread in WP78.

| Acceptance item | Result and limit |
| --- | --- |
| API configured identity | Exact accepted2744c4dc... child, empty startup; fresh config GET PASS |
| UI configured identity | Exact accepted6b59d205... child, empty startup; fresh config GET PASS |
| API/UI running-image identity | UNKNOWN; no qualified fresh exact main-container digest binding established |
| API /health | NOT ACCEPTED: DNS/TCP/TLS passed; HTTP-header timeout, no status/Alive |
| UI /_stcore/health | NOT ACCEPTED: DNS/TCP/TLS passed; HTTP-header timeout, no status/ok |
| Retained same key | true per merged WP77 private readback; not rechecked or disclosed here |
| Authenticated BTC/ETH snapshots | NOT_RUN; dependent API health prerequisite failed |
| Cloud genuine candle state | UNVERIFIED; no data/key/private-state access |
| Actual browser/controls/indicators | NOT_RUN; API/UI serving prerequisites failed, no browser session launched |

Exact current configured references:

- API: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`.
- UI: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`.

These are CONFIGURED_ONLY, never relabeled as running-image proof. The index/
localID3e323... and diagnostic API child were not substituted. No rebuild,
publication, image PATCH or settings save was needed or performed.

## Timestamped bounded operations

1. UTC2026-11-21T19:39:50.293916+00:00 (Sao Paulo16:39:50-03:00):
   two exact config/web GETs, one API then one UI, one ARM send each, no retry,
   2.712s total. Both accepted immutable references and empty startup matched.
   Existing installed CLI direct worker,15s owned cap per request, combined
   output1MiB, no full config/private value output. No complete WP77 audit repeated.
2. UTC2026-11-21T19:40:18.673755+00:00 (Sao Paulo16:40:18-03:00):
   one GET `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health`.
   DNS resolved; one selected socket connected; TLS certificate/hostname verified.
   Sent1HTTP request; TimeoutError atHTTP header stage,3.815s; no HTTP status.
3. UTC2026-11-21T19:40:57.820178+00:00 (Sao Paulo16:40:57-03:00):
   one GET `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/_stcore/health`.
   Same staged DNS/TCP/TLS checks passed; sent1HTTP request; TimeoutError atHTTP
   header stage,3.628s; no HTTP status. This independent UI serving check was not
   an API retry. No follow-up page/assets or browser load occurred.
4. UTC2026-11-21T19:41:38.811068+00:00 (Sao Paulo16:41:38-03:00):
   one existing LinuxRunningContainersPerHost detector GET per exact staging app,
   two ARM sends,15s owned cap each/no retry. Each returned1structured dataset
   and1table with rows. No raw records, text, timestamps, IDs, addresses, image
   candidates or environment values were exposed. The current parser does not
   qualify a fresh main-container-to-exact-child witness; running identity stays
   UNKNOWN. This is not proof the Azure response lacks all image information,
   nor proof the accepted images are/are not running. No broad log fetch.

The one-request HTTP workers enforce8s overall owned-process caps,2s connect/
TLS and3s HTTP-header socket bounds,7.5s internal deadline,4096-byte accepted
body cap plus one overflow sentinel. No proxy, credentials, redirects, socket
fallback, blind retry or polling. Staged timeouts intentionally bound F1 use;
they do not prove the apps could never respond after a longer cold start.
No response body/header/IP/key was output or persisted. Process/pipe cleanup
completed through existing Windows Job capture. Native DNS is bounded by the
outer process, not claimed cancellable inside Python.

## HTTP403 interpretation and dependent acceptance stop

Fresh requests did NOT return403; they timed out before a status. Historical
TLS-valid API403 remains unexplained and is not overwritten or called reproduced.
The reviewed ResearchSnapshotEndpoint.cs routes anonymous GET /health to200Alive
before X-API-Key validation. Invalid snapshot authentication returns401, not403.
That source does not prove which image/process handled an earlier403. No key
mismatch, platform cause, access restriction, quota or missing database is
inferred from these fresh header timeouts. Since403 did not persist in this
attempt, no ingress/auth metadata follow-up was automatically triggered.

API health failure blocks authenticated snapshot acceptance; UI health failure
also prevents claiming browser startup/acceptance. No real browser clicks were
attempted and none are counted as a pass. No private cloud state was read, no
provider/acquisition/import/data transport occurred, and no missing-data diagnosis
is made without an actual API state response. No new endpoint or security bypass.

## Tests, scope, operations and flags

| Offline command actually run | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp77_ui_promotion.py -q` | 10 passed, exit0 |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q` | 10 passed, exit0 |

20 distinct tests passed, zero failures/skips. These existing fake tests are not
cloud/browser acceptance. No code change/new dependency or broad requalification.
Syntax compilation of the existing helpers, UTF-8/LF/no BOM, Markdown table/link
sanity, exact one-file documentation scope and git diff --check passed. Prior
historical artifacts unchanged. PR stays OPEN/UNMERGED for owner review.

Actual live inventory: **4ARM resource GETs +2anonymous HTTPS health GETs**;
normal CLI authentication only. Zero Azure mutation/restart, quota/plan-usages,
logging-configuration/key/settings/private data reads, auth/access-rule/TLS/storage
changes, registry/Docker/provider/public-production probes, image operations,
credential rotation or browser session. No periodic monitoring or automatic retry.

Next smallest concrete action needs a new bounded scope: review existing API/UI
startup evidence from the returned detector records using a qualified fixed-field
projection, or one explicitly approved longer cold-start serving window if the
owner chooses. Do not repeat quota/logging/classifier work or reconfigure images
based on these timeouts. Any actual access/security/code fix requires specific
observed cause and separate narrow approval; no speculative mutation follows.

`UI_IMAGE_CONFIGURED=true`; `SAME_KEY_PRESENT=true` (WP77 evidence).
`UI_RUNNING_IMAGE_VERIFIED=false` / UNVERIFIED; `API_HEALTH_ACCEPTED=false`;
`BTC_ETH_BROWSER_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`;
`liveAttemptReady=false`. No release acceptance or OCI running equivalence claim.

# Release 1.15 WP79 - startup/serving execution evidence

Date: 2026-11-21 (America/Sao_Paulo). **CURRENT HARD QUOTA BLOCKER CONFIRMED;
NO MUTATIONS OR HTTP RETRIES.**

Base fresh main `632e636b9c39c94130aa1b58336f13a6d15471a2` contains merged
#477/WP78. Historical WP78 evidence was reviewed and is unchanged. New isolated
branch preserves owner modified/untracked prompt files on main and unrelated
branches. No application source/dependency/image change.

## Source and existing startup evidence review

Reviewed WP78 runtime report, WP77 image/key evidence, accepted Dockerfiles,
research bootstrap and ResearchSnapshotEndpoint.cs. Expected API is UID1654,
binds0.0.0.0 on52731 under Azure transport and responds anonymous GET /health
with200Alive before key validation. Expected UI is UID12002, Streamlit binds
0.0.0.0 on8501, with its existing entrypoint/empty Azure startup contract.
Source expectations do not attest actual Azure ports/process/image identity.

WP78's detector responses were discarded after safe summary projection: they
reported structured tables, but no qualified exact child/main-container witness
was established. There are no retained raw startup records to reinterpret in
WP79. No historical raw response was retrieved from private artifacts. Fresh
startup records would be needed for event/schema interpretation; the safety
read below found a hard quota condition first, so no new detector query was made.
This is an explicit evidence limit, not a claim Azure exposes no such witness.

## Actual bounded execution

### 1. API site safety read

UTC2026-11-21T19:48:43.201305+00:00
(Sao Paulo2026-11-21 16:48:43.201305-03:00): one exact API site ARM GET,
15s owned-process cap, one resource send, fixed projected fields only.

| Field | Observed |
| --- | --- |
| Exact expected plan association | true |
| Linux / WestUS2 / HTTPS-only | true / true / true |
| API state | **QuotaExceeded** |
| API usage | **Exceeded** |
| API availability | Normal |
| Running | false |

Stopped the sequential safety check at this newly observed hard state; UI site
read NOT_RUN. This was a site/readiness check, not repeated plan-usages polling.

### 2. One necessary quota classification after new hard blocker

WP79 permits a quota read only after a new concrete blocker makes it essential.
The fresh QuotaExceeded/Exceeded state above met that exception. Exactly one
shared-plan usages GET was then issued, no retry or other diagnostics.
UTC2026-11-21T19:49:04.021843+00:00
(Sao Paulo2026-11-21 16:49:04.021843-03:00):

| Counter | Current | Limit | Azure supplied nextResetTime |
| --- | --- | --- | --- |
| WPStopRequests | **39** | **15** | `2026-11-21T20:00:00` |
| CpuTime | 7799ms | 3600000ms | `2026-11-22T00:00:00` |

Reset fields have no timezone suffix; if interpreted asUTC, the stop reset would
be17:00 Sao Paulo. No hard guarantee of app recovery at that time is inferred.
No third resource request, automatic retry, periodic quota check or health probe.
Existing capture uses bounded pipes/1MiB, suspended owned Windows Jobs, cleanup,
15s per-worker bounds; raw settings/logs/IDs/IPs/error bodies were not output.
Normal existing CLI authentication only; no credentials were inspected/reported.

## Narrow supported diagnosis and limits

**Immediate current serving blocker: API is platform QuotaExceeded, with shared
plan stop-request usage39/15.** CPU is within the returned daily allowance.
These are direct fresh ARM observations. They do not establish who/what caused
the stops, whether the accepted container restarted/crashed/pulled successfully,
or whether routing/ports/startup/data/auth settings are correct in the runtime.
No causal claim about an image, missing key, missing SQLite or user action.

WP78's ~3s header timeouts and earlier API403 remain separate historical events.
The current quota result does not retroactively prove their origin/cause.
No auth/ingress rule or app setting was changed based on any HTTP status.
No longer-header health window was attempted because WP79 explicitly requires
stopping on observed hard quota state. No need to launch more requests against
an already-blocked app simply to repeat a timeout/403.

| Acceptance item | Status |
| --- | --- |
| API configured image | Accepted2744c4dc... / empty startup per WP78; not reread |
| UI configured image | Accepted6b59d205... / empty startup per WP78; not reread |
| API running image identity | UNVERIFIED |
| UI running image identity | UNVERIFIED |
| API current state | QuotaExceeded / Exceeded |
| UI current state | NOT_READ in WP79; do not infer from shared quota alone |
| API/UI health statuses in WP79 | NOT_RUN, no HTTP status |
| Authenticated BTC/ETH snapshots / cloud data | NOT_RUN / UNVERIFIED |
| Real browser chart acceptance | NOT_RUN |
| Same retained key | true per WP77 private readback, not reread |

## Validation and inventory

| Executed offline command | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp77_ui_promotion.py -q` | 10 passed, exit0 |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q` | 10 passed, exit0 |

20 distinct tests passed, zero failures/skips. No code change/new test or
package/network-dependent test. Existing helper syntax compilation, UTF-8/LF/no
BOM, Markdown tables, exact one-file scope and git diff --check passed before PR.
No cloud/browser acceptance inferred from fake tests. All historical evidence
preserved; follow-on PR OPEN/UNMERGED.

Actual operations: **2read-only ARM GETs** (API site then plan usages justified
by the newly observed hard blocker). Zero UI/site/config/logging/settings/key/
new detector/HTTP/browser/data/provider/registry/Docker operations. Zero Azure
mutation/restart/scale/auth/TLS/port/storage changes, image substitution or
production access. No data transfer/import, secret rotation or quota workaround.

Single most useful next action, requiring a new narrow approval after this stop:
one fixed-window exact-API startup/termination-event projection to determine
whether container exit/restart/pull failure evidence explains stop accumulation,
using fixed safe event fields, not another blind health/quota loop or broad logs.
No corrective mutation is justified until the causal event evidence exists.
No new generic diagnostic framework or speculative fix is proposed.

`UI_IMAGE_CONFIGURED=true` and `SAME_KEY_PRESENT=true` remain historical WP77
facts. `UI_RUNNING_IMAGE_VERIFIED=false` / UNVERIFIED;
`API_HEALTH_ACCEPTED=false`; `BTC_ETH_BROWSER_ACCEPTED=false`;
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

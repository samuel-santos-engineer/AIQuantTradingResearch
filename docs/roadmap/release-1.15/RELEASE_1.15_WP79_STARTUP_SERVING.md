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


## Owner continuation after successful quota reading - 2026-11-21

Owner said quota reset and continue. The preceding separate owner-requested
quota check atUTC2026-11-21T20:44:13.468546+00:00 reported stop0/15 and
CPU7799/3600000ms. This continuation issued **zero quota/usages reads**.
AtUTC2026-11-21T20:48:19.322810+00:00 (Sao Paulo17:48:19-03:00), the first
exact API site safety GET failed: one ARM send, ConnectionError, failureStage
ARM_TRANSPORT, no returned HTTP status/site state.15s owned-worker limit with
3s connect/10s socket bounds, no retry or raw error output; cleanup completed.
UI site, startup detectors, longer health, authenticated data/browser NOT_RUN.
No Azure mutation, keys/settings/logging/data/registry/Docker/provider action.

This does not establish quota exhaustion or another application failure; the
last successful quota observation stays0/15 and current site state is UNKNOWN.
No code/test changes; earlier20-test pass stands. Evidence UTF-8/LF/no BOM and
diff checks passed. No new work package was created. PR #478 stays OPEN/UNMERGED.
Next smallest bounded action is one existing startup-detector projection under
a new explicit continuation, without another quota loop. No automatic retry.
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Approved single API startup-detector continuation - 2026-11-21

Reconciled main632e636... and OPEN #478 head17fb2d6... before execution; clean
existing PR worktree, owner edits untouched. Authority covered only one API
LinuxRunningContainersPerHost GET plus offline interpretation, no follow-up.

UTC2026-11-21T21:20:41.168706+00:00 (Sao Paulo18:20:41.168706-03:00):
exact API detector resource, API-version2023-12-01, fixed requested UTC window
**2026-11-21T21:05:41.168706+00:00 to2026-11-21T21:20:41.168706+00:00**.
One authenticated read-only ARM send, **HTTP400**,1.605s total;15s owned-process
cap,3s connect/10s socket,1MiB combined capture/body cap. No redirects/resource
retry/fallback/polling. Owned Windows Job/pipe cleanup verified. Only fixed status/
stage emitted: BLOCKED, STATUS_OR_SCHEMA, ValueError. Raw response/error/header/
record/address/identifier/key/settings values were not persisted or output.

No usable structured detector result was obtained. Dataset/table/rows,
startup/pull/termination/exit/restart events, timestamps, fresh app/main-container
attribution, exit codes and exact runtime child match all remain **UNKNOWN**.
HTTP400 is a rejected diagnostic request, not evidence of a container startup
failure, unhealthy image or current quota condition. Its detailed server reason
was not retained, so the specific rejection cause is unproven.

A transient safe projection was prepared offline with three fake assertions
(empty/missing data stays unverified, startup marker count, no identifying fake
value output); those assertions passed but the projection never received real
detector data because the server rejected the request. This is not a claim of
full parser/schema qualification or three distinct unittest cases. No source
helper/framework or dependency was added to the repository.

Offline installed-SDK inspection confirms DiagnosticsOperations exposes
get_site_detector_response with datetime start_time/end_time and optional
time_grain. A fake credential and capture-only transport, which throws before
network access, confirmed GET on the same known resource and startTime/endTime
query keys. Its UTC serialization was `...T21:05:41.000Z`/`...T21:20:41.000Z`;
the installed SDK default API-version was2025-05-01. The actual live request used
2023-12-01 and microsecond+00:00 timestamps. These observed serialization/version
differences do **not** prove the cause of400 and do not authorize a version
upgrade or another request. No real token/authentication/network occurred in
the fake SDK inspection. No raw historical detector values were accessed.

Live inventory: **one API detector GET only**. No API site/health follow-up,
quota/usages, logging/config/settings/key read, UI fallback, registry/Docker,
provider/data/authenticated snapshot/browser, Azure mutation/restart/scale/image
change or import. Historical403, header timeouts and quota readings remain
distinct and unchanged. No new state/cause inferred from the rejected request.

Smallest next observation requires separate owner approval: one replacement
same-detector read with installed-SDK-serialized UTC-Z timestamps and the
explicit existing2023-12-01 API version, retaining the same bounded window,
no retries and safe projection. First verify that exact query offline; do not
switch endpoints or versions automatically. If rejected again, retain only a
fixed allowlisted error-code category to distinguish unsupported time input
from resource/permission failure, never a raw error body. No new work package.

Documentation-only append; UTF-8/LF/no BOM, historical evidence prefix and diff
checks passed. No application/source changes or unnecessary20-test rerun;
previous executed20-test result stands. PR #478 OPEN/UNMERGED.
UI_IMAGE_CONFIGURED=true/SAME_KEY_PRESENT=true remain WP77 evidence;
UI_RUNNING_IMAGE_VERIFIED=false; API_HEALTH_ACCEPTED=false;
BTC_ETH_BROWSER_ACCEPTED=false; PUBLIC_RUNTIME_VERIFIED=False;
liveAttemptReady=false.


## Approved UTC-Z replacement detector read - 2026-11-21

Owner separately approved one replacement read on the same endpoint/version.
Before execution, an installed SDK fake-credential/capture-only transport test
explicitly pinned API-version2023-12-01 and verified the known detector GET,
startTime/endTime serialized as UTC-Z; zero real authentication/network calls.

UTC2026-11-21T21:27:20.582650+00:00 (Sao Paulo18:27:20.582650-03:00):
one API LinuxRunningContainersPerHost GET, fixed window
2026-11-21T21:05:41.000Z to2026-11-21T21:20:41.000Z (same15-minute interval,
second-aligned SDK serialization), API-version2023-12-01. **HTTP200**, one ARM
send,5.930s total,15s hard owned-process/1MiB bounds, no retry/redirect/fallback.
Owned Job/pipe cleanup verified. No quota/site/health/UI/key/settings/logging
configuration request or Azure mutation followed.

The local worker then failed before projecting detector data. It used Path to
load the offline projection but omitted its import. This is a Codex worker
implementation defect, not an Azure container/serving failure. Structured result
was BLOCKED/STATUS_OR_SCHEMA with exception OTHER; offline source inspection
identified the missing import. HTTP200/JSON transport completed, but no usable
startup/table/event/timestamp/exit/image findings were retained. Raw response
was transient only and discarded; no recovery from logs/private artifacts and
no additional live read was attempted. All runtime attribution remains UNKNOWN.

Offline correction added the missing Path import to the transient worker.
Two FULL-worker fake cases (missing dataset; synthetic startup-message dataset)
then passed with mocked CLI/request and network creation forbidden. They assert
DETECTOR_PROJECTED, no fake-sensitive text output and runtime identity remaining
UNVERIFIED. Two fake cases are not a newly executed application unittest suite;
no real token/key/network was used. This closes the local projection defect
but does not reconstruct the discarded actual response or establish startup.
No new repository framework/helper/source was added.

The UTC-Z request succeeded where the earlier request returned400; this is
consistent with a request-format sensitivity, not a proven server root cause
because the prior error detail was not retained. Historical400/403/timeouts
and quota observations remain separate, not rewritten. No image/key/config
change is justified by a projection defect or metadata HTTP200 alone.

Next smallest owner-approved action, if chosen: one same-endpoint/version/window
read through the corrected FULL-worker-fake-tested projection, safe fixed fields
only; no generic framework or new work package. No automatic repeat under this
exhausted one-call approval. UI_RUNNING_IMAGE_VERIFIED=false;
API_HEALTH_ACCEPTED=false; BTC_ETH_BROWSER_ACCEPTED=false;
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false. PR #478 OPEN/UNMERGED.
Evidence-only append passed UTF-8/LF/no BOM/diff checks; prior20 application
fake tests were not unnecessarily rerun. No secret/private raw response persisted.


## Approved corrected projection read - 2026-11-21

UTC2026-11-21T21:33:30.229966+00:00 (Sao Paulo18:33:30.229966-03:00):
one separately approved API detector GET, same2023-12-01 version and fixed
UTC-Z15-minute window2026-11-21T21:05:41.000Z to21:20:41.000Z.
**HTTP200; DETECTOR_PROJECTED;1.709s**, one ARM send,15s/1MiB bounds,
no retries/fallbacks/redirects; owned Job/pipe cleanup verified.

Safe projection actually returned: structured dataset true, table true,
containsRows true, rowCount1; recognized semantic field kinds OTHER/REASON/STATE.
No qualified TIMESTAMP/APP/ROLE/IMAGE fields were recognized. Thus freshness,
exact app/main-container binding, runtime image identity, exit code and restart
remain UNKNOWN/UNVERIFIED; qualified main/running-expected-image record counts0.
All fixed event-marker counts0 (startup/pull/termination/exit/restart/pull-failure/
quota/listener/configuration/storage). Zero pattern matches do **not** prove
those events absent: this bounded summary/schema is insufficient for causality.
No raw summary text/status/record/IDs/addresses/private values were output or
persisted. The earlier projection defect is corrected; the result is now an
actual inconclusive projected read, not a hidden local exception.

No subsequent API site/health, UI, quota/logging/settings/key, authenticated
BTC/ETH/browser/provider/data or Azure mutation/restart operation occurred.
No current healthy main-container or startup failure is established. Historical
403, timeouts, quota and earlier rejected/read failures remain unchanged.

Evidence-only append passed UTF-8/LF/no BOM and diff checks; no code/new package
or unnecessary application-test rerun. Prior full-worker2fake cases and20
application fake tests remain their separately dated executed results.

Single next useful scope, if owner chooses: one cold-start-aware API /health
observation with up to20s HTTP-header allowance and a fixed overall owned-worker
bound, no credential/redirect/retry, because the earlier~3s header bound did not
resolve serving and this detector does not provide a cause. It would test actual
serving, not certify running image identity. No such HTTP request is authorized
or executed by this detector-only approval. No new work package/framework needed.
PR #478 OPEN/UNMERGED; UI_RUNNING_IMAGE_VERIFIED=false;
API_HEALTH_ACCEPTED=false; BTC_ETH_BROWSER_ACCEPTED=false;
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

# Release 1.15 WP75 - accepted BTC/ETH staging deployment handoff

Date: 2026-11-20 (America/Sao_Paulo). Status: **OFFLINE WORKFLOW READY;
LIVE DEPLOYMENT NOT RUN; AUTHENTIC CLOUD DATA STATE UNVERIFIED**.
Base main: `35a77d663d454624b96219d4ca1a10617eaeac6f` (merged #470).
No RepoDigest grammar work. SHA256 only. Owner checkout and unrelated PRs,
including #457, are preserved. This PR remains OPEN/UNMERGED.

## Short inventory and chosen path

| Artifact / existing code | Decision |
| --- | --- |
| Accepted UI local image `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` | Reuse exact bytes. Immutable registry mapping remains unknown; do not use local ID as a manifest digest. |
| Accepted API child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` | API candidate; historical Phase G publication chain is recorded, current availability requires approved readback. Never substitute diagnostic child d33626a9... |
| `container/release_1_15/research-api.Dockerfile` / research bootstrap | UID1654, port52731, serve mode; requires private state directory and retained service key. No rebuild selected. |
| `container/release_1_15/research-chart.Dockerfile` | UID12002, port8501, existing WP61 Streamlit/C# snapshot client; no rebuild selected. |
| `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py` | Historical f1e491... image and Windows-specific CLI launcher; do not invoke its Apply/SettingsApply for WP75. Reuse its documented nonsecret API setting contract only. |
| `eng/docker/release-1.15/wp64_oci_provenance.py` | Existing offline index/child/config hash-chain verifier; preserve distinction between accepted local ID and registry digest. No classifier extension. |
| `wp39_local_history.py` and initializer | Local private import/runtime tooling; not an Azure upload channel. Do not run against retained local data under WP75. |
| Historical Phase G | API serving failed/quota exceeded; UI wiring/key absent then. These are historical, not refreshed facts. |

New `eng/azure-cli/r1.15-qualification/wp75_staging_plan.py` is a dependency-free,
Linux/Windows Python command planner. It constructs direct argv for the exact
existing subscription/group/apps. It never executes a subprocess or HTTP, reads
credentials or infers publication. Default prints PLAN_ONLY with no commands.
Named phases print concrete owner-reviewable argv, not live execution. Tests do
not prove the installed Azure CLI merge implementation or actual Azure state.

## Operator runbook - commands and separate live approvals

Run from a reviewed checkout with Python and existing authenticated tools; do
not install/upgrade anything. All text in this handoff uses UTF-8/LF/no BOM.

```text
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase Publish
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase API
```

These are safe offline commands. `commandArgv` entries are the exact commands
to execute only after each named live scope is approved, sequentially with no
shell interpolation. Stop on a failed/uncertain command; no automatic retry,
restart, rollback or UI promotion. Azure settings commands use merge-style
`az webapp config appsettings set --settings ... --output none`, not full
collection PUT, no slot-settings. Before a real save, approved private in-memory
comparison must confirm installed CLI merge/preservation of unrelated settings,
keys and slot flags (the earlier WP36 fake probe is version-specific). Do not
claim this planner has performed that preservation proof.

### 1. Accepted UI publication/mapping (separate approval)

If an existing published mapping is independently verified first, use it and
skip publication. No verified current WP61 mapping is in the repository.
Otherwise the Publish phase emits exactly:

```text
docker tag sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp75-wp61-3e323b0d3008
docker push ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp75-wp61-3e323b0d3008
```

Proposed approval: these exact tag/push operations using normal existing registry
authentication, followed by bounded read-only metadata verification of that tag
and its selected Linux/amd64 child/config. No build, pull, image substitution or
credential-store inspection. Publication exports the image layers; accepted
public-source image contents must be confirmed suitable before approving export.
If the tag already exists unexpectedly, stop; do not overwrite it without review.
A separately approved exact-tag metadata check can establish absence before push.
Tagging/pushing is not authorized or executed by WP75.

Readback must capture raw bounded metadata privately, hash exact bytes before
JSON interpretation, select exactly one Linux/amd64 child and verify config/user/
entrypoint/port against the accepted image using existing WP64 verifier. Bind
registry root/runtime/config to accepted local mapping evidence; no local-ID equals
config/index assumption. Inspect only allowlisted config fields, never dump env.
If a mapping cannot be proven, stop before UI configuration. Preserve the verified
immutable `ghcr.io/...@sha256:<64 lowercase hex>` reference for phase 4. Also
verify current anonymous access to the exact accepted API child. Manifest
availability alone does not prove image layer pull or Azure runtime success.

### 2. Fresh staging baseline and API configuration (separate approval)

Exact subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, group
`rg-aiq-r112-wp05-wus2-7f5eabb5`, West US2 Linux F1 plan
`asp-aiq-r112-wp05-wus2-7f5eabb5`. API
`aiqr115researchstagingwus27f5eabb5`; UI `aiqr112wp05wus27f5eabb5`.
Production `aiqr112wp035ec325382770` is excluded.

Approve projected read-only quota/site/config checks first: remaining stop/CPU
quota, Running/Normal state, exact plan/region/Linux/classic-container identity,
HTTPS-only, image/startup and logging. Stop on quota pressure or drift. Preserve
a fresh private rollback baseline including only necessary settings delta values,
key equality/presence booleans and slot metadata; never print secrets. Do not use
the Windows-only blocked WP62 live adapter. Normal Azure authentication may be
used in a later approved cross-platform execution, without printing tokens.

After exact approval, execute the API phase's three argv entries: merge the ten
reviewed nonsecret runtime settings; PATCH config/web to the **accepted API
child** and empty startup; PATCH HTTPS-only true. The patch touches no other web
properties. Settings/image saves can recycle the app. No extra restart by default.
The direct commands include all exact targets; they are not templates pointing
to an old image. Never set platform-injected WEBSITE identity markers yourself.
Do not expose settings command output (`--output none`). No key appears in argv.

Use the SAME existing recoverably retained service key privately in both apps as
`AIQ_RESEARCH_API_KEY`. If UI setting is absent, owner enters the retained value
privately in the portal after a separate exact-setting approval. No generation,
rotation, copying into commands/chat/files/clipboard or assumption of equality.
Preserve App Service authentication/slot metadata and unrelated settings. UI
`AIQ_RESEARCH_CA_FILE`, if present, must refer to a valid available trust input;
a local Docker CA path must not be silently retained or removed. Public Azure
HTTPS normally uses system trust when that optional override is absent. Any
necessary override removal requires a specific approved delta, not wholesale
settings replacement. Confirm settings/API endpoint contract before UI promotion.

### 3. API serving and authentic data acceptance (separate approval)

Observe TLS-validated API `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health`
for at most 180 seconds, at most nine requests, eight seconds/request, maximum
4 KiB body, no redirects/restarts. Expect 200 and exact `Alive`. Stop immediately
on quota, redirect, auth rejection, repeated failure or uncertainty; do not consume
the whole budget by default. A supervised first request may end the phase early.

**Critical concrete blocker:** serve mode validates/open existing private state;
it does not import a dataset. A missing directory can fail startup; missing state
can yield DATASET_NOT_READY even after health passes. No authentic cloud state,
UID1654 directory ownership, witness/HMAC generation or valid BTC/ETH import is
currently verified. Image/config deployment alone cannot complete the mission.

If the accepted cloud state is absent, authorize a separate secure data handoff
and existing `operator-chart-stdin` initializer invocation, with its exact private
input, owner permissions and stdin configuration reviewed first. That path needs
the private export available in the SAME API container and uses retained service/
HMAC configuration; it is not achievable by a public HTTP import endpoint (none
exists). Existing local Import is not an Azure transport. Do not embed data or
keys in a public image or create a new endpoint. WP75 provides no implemented
Azure private upload/exec adapter and does not claim otherwise. The precise next
transport/initialization command depends on approved process access and private
input location, both unverified; stop rather than invent them. Retain the prior
local accepted chart. Ephemeral F1 `/tmp` state is intentionally non-durable;
restart/replacement can require reimport, not automatic provider acquisition.

Only after health and private state are ready, perform separately approved
server-side authenticated `/v1/research/snapshot` requests for BTC/USD and ETH/USD
using existing client/contract, never raw URLs with keys or console data dumps.
Require accepted identity, genuine completed candles, C# analytics and explicit
OHLC-only volume unavailable. 401/503/429 does not pass. Preserve rate limits.

### 4. UI configuration second (separate approval)

After the verified UI immutable digest is known, generate exact commands:

```text
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase UI --ui-digest sha256:<verified-published-digest>
```

The argument is a nonsecret metadata digest. Replace the placeholder with the
verified digest, not a tag/local ID; no placeholder command is executable as-is.
Planner rejects the accepted local ID as an unproven mapping. Execute only after
API/data acceptance: merge port8501, AIQ_RESEARCH_CHART=1, exact API HTTPS URL;
PATCH verified image/empty startup, PATCH HTTPS-only true. Secrets/UI ingress auth,
TLS CA configuration, plan/logging/slot/storage settings are preserved. A supplied
digest is syntax-checked, not independently attested by this offline planner.

Bounded UI health: `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/_stcore/health`;
then existing authenticated browser access, no production URL. No new anonymous
public dataset routes. Health or an HTML shell is not chart acceptance.

### 5. Browser acceptance (separate approval)

Use real browser evidence on deployed staging: BTC -> ETH -> BTC, actual
candlestick and SMA traces, UTC labeling, C# indicators with warmup hidden,
Volume unavailable disclosure and no invented volume bars; range/interval
selection, zoom/pan and Return to Live; authenticated session and snapshot state.
Confirm no synthetic fallback and no provider key in browser traffic. Preserve
private candle content in owner-only evidence; publish only sanitized categories.
Capture selected instrument/trace counts and change results; clicks alone do not
pass. Stop on failure. Do not change PUBLIC_RUNTIME_VERIFIED/liveAttemptReady
until genuine full acceptance is separately recorded and reviewed.

### Rollback strategy

Before each app's mutation, capture its complete needed rollback baseline
privately. On failure stop UI promotion; do not auto-retry. Proposed separately
approved rollback restores that app's original image/startup and only changed
nonsecret setting entries/HTTPS flag; restore original slot flags if changed,
never replace unrelated settings or keys. The Rollback planner emits an image
PATCH only for a verified same-repository immutable baseline with empty startup:

```text
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase Rollback --role API --rollback-reference ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:<captured-original-digest>
```

For nonempty original startup, Code/empty-image mode or another original registry,
use the exact captured baseline with a reviewed explicit PATCH instead; this
planner intentionally cannot represent those cases. The image command alone is
not full rollback. Setting/HTTPS restoration is manual/private from captured
values (including removal only of verified newly added names). Code/config
rollback cannot recover lost ephemeral SQLite data or reset F1 quotas. No
recreation/deletion/paid upgrade or shared-plan change by default.

## Offline execution evidence

Implementation is three focused paths: new cross-platform planner, its fake-only
tests and this runbook/evidence. No application, Dockerfile, dependencies or
RepoDigest classifier changed. Existing Windows PowerShell scripts untouched.

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: **10 passed**, exit0, zero skips/failures.
- `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py`: PLAN_ONLY, executionImplemented=false, zero Azure/Docker operations, exit0.
- Syntax compilation, UTF-8/LF/no BOM, Markdown link/table sanity, exact three-path
  scope and git diff --check passed before PR publication.

Tests cover default/import nonexecution, exact API-first ordering, immutable
SHA256/local-ID rejection, accepted API child, nonsecret merge argv, exclusion of
keys/slot/log/plan/production mutation, rollback limits and sanitized failures.
They are synthetic command checks, not actual CLI preservation, registry or
cloud acceptance. No broader unrelated suite/requalification was needed.

Actual operations: repository reads, fake tests/static checks and ordinary Git/
GitHub publication only. **No Docker daemon, registry, Azure, credentials, public
HTTP/browser, retained runtime/data/TLS, image export/build/pull/push/tag or
provider calls. No deployment occurred.** Existing publication necessity is
NOT PROVEN; proposed exact publication is an explicit fallback, not an inference
from malformed RepoDigests. OCI provenance UNVERIFIED; flags remain false.

Single next action: obtain approval for the exact accepted UI publication/readback
phase in section1 (including the bounded exact-tag conflict check), or supply an
already verified immutable mapping to skip publication. This moves artifact
delivery forward; API private data acceptance remains a separate concrete blocker.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Approved continuation - accepted UI publication/readback - 2026-11-20

Owner directly approved the exact next publication/readback phase after PR #471
was opened. This supersedes the earlier NOT RUN status for this phase only;
Azure deployment/browser/data acceptance remain NOT RUN. No other live phase
is inferred from this approval. Historical claims above remain dated evidence.

Existing WP61 evidence confirms an allowlisted public-source build context,
no private datasets/keys/TLS/fixtures, eleven application file hashes matching
the accepted commit; no universal inherited-layer audit is claimed. The exact
new tag conflict check used an anonymous scoped GHCR pull token in memory and
one tag GET: HTTP404, target tag absent. Two requests total, no retries; bodies
bounded at 1 MiB, socket timeout10s, TLS validation, no proxy inheritance.
Token/value/raw-error output was not published or persisted.

Exactly the reviewed Docker tag and push commands ran against accepted UI ID
`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`
and tag `ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp75-wp61-3e323b0d3008`.
Tag: exit0, 0.288s (10s cap). Push: exit0, 5.417s (120s cap). Existing qualified
Windows Job capture bounded combined stdout/stderr to1MiB and verified owned
process/pipe cleanup. No Docker inspect/context/info/version, build/pull/export,
container/volume operation or retry. Existing normal registry authentication was
used by Docker; credential stores and values were not inspected. Push reported
the root below; no raw Docker output was published or persisted.

Anonymous readback at UTC2026-11-21T01:31:15.825319+00:00
(Sao Paulo2026-11-20 22:31:15.825319-03:00) used six requests: scoped token,
exact tag manifest, one selected Linux/amd64 runtime manifest, config blob
including one approved-host HTTPS redirect without Authorization forwarding,
and exact accepted API child manifest. Each request10s socket timeout, 1MiB
body cap; seven-request budget, no retries. Raw metadata/token stayed in memory;
only sanitized verification fields were retained. No full config/Env dump.

| Verified UI metadata | Result |
| --- | --- |
| Registry index SHA256 | `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` |
| Selected Linux/amd64 child | `sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde` |
| Config SHA256 | `sha256:224cb12b6ad9ca5edcb6c8675fbd19ec5ce06ab3cd38a557a2bc513bf994e7e8` |
| Local ID mapping | MATCH to INDEX (verified from raw-byte hash chain, not assumed) |
| Existing WP64 verifier | MATCH, chain MATCH, UI contract MATCH |
| Revision label | UNKNOWN; absence is not proof of wrong source |
| Layer content verification | false; metadata chain verification is not layer-content/runtime attestation |

**Verified deployable UI reference:**
`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`.
Accepted API child anonymous manifest GET/hash/media-type verification passed for
`sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`.
API layers/config/runtime were not requalified by that one manifest check.

The existing offline verifier labels publication RECORDED_MATCH_NOT_FRESH_ATTESTATION:
this records actual readback at the timestamp above, not continuing reachability.
UI OCI metadata provenance/mapping is now verified for that readback; universal
layer provenance and actual Azure running identity remain unverified. Prior
UNVERIFIED findings are not retroactively rewritten. Publication was an explicitly
approved delivery action; its necessity was not proven by rejected RepoDigests.

Concrete safe planner command now available (still prints argv, executes nothing):

```text
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase UI --ui-digest sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde
```

No source changes or new tests were needed for this operational continuation;
the prior10-test pass stands. UTF-8/LF/no BOM and diff checks rerun for this append.
Exactly2 Docker commands and8 registry HTTP requests total across conflict check
and readback; zero Azure/health/browser/provider/data/credential-store operations.
No authenticated chart acceptance or Azure deploy has occurred.

Single next approval requested: fresh **read-only staging baseline preflight**,
up to8 exact ARM GETs: shared plan usages and metadata; each exact staging app's
site, config/web and config/logs. Existing authenticated CLI only;15s/request,
120s total, no retries; projected nonsecret identity/quota/state/usage/availability/
HTTPS/image/startup/logging metadata only. No app-settings/key reads, mutation,
restart, public probe or deployment. Stop on quota pressure/error/drift. This
prepares the reviewed API-first configuration phase without claiming it is approved.
Private settings/key-preservation and cloud data handoff remain later explicit scopes.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Approved continuation - quota-first read-only preflight stopped - 2026-11-20

Owner approved the proposed maximum8 projected ARM reads, 15s each/120s total,
no retries, no settings/keys/mutations. A transient installed-CLI Python worker
was prepared offline with exact GET target allowlist, no redirects/retries,
3s connect/10s socket read limits,1MiB response cap, and fixed safe projections.
The parent used the existing owned Windows Job/pipe capture with a15s per-worker
request deadline. Installed CLI source inspection used signature/disassembly
because inspect.getsource was unavailable; no authentication ran in that step.

Observed UTC2026-11-21T01:39:15.283189+00:00
(Sao Paulo2026-11-20 22:39:15.283189-03:00): **STOP_PREFLIGHT at QUOTA**.
One worker/query invocation attempted; duration0.914s, process exit0 with a
structured BLOCKED result. Owned Job/pipe cleanup verified. Fixed category:
ARM_TRANSPORT_STATUS_OR_SCHEMA_UNVERIFIED. No quota counters, HTTP status,
Azure configuration baseline or current app state were successfully returned.
The worker did not retain the internal HTTP-send count on this failure path,
so actual completed ARM request count is UNKNOWN (not asserted as one).
The remaining7 approved metadata queries were NOT_RUN. No automatic retry.
No settings/key read, registry/Docker action, health/browser probe, mutation,
restart, data transfer or deployment occurred in this continuation.

This result does not establish quota exhaustion, authentication failure or
Azure app health; it only blocks the current preflight. Earlier verified UI
publication/mapping remains valid as timestamped evidence. Source/runtime
implementation and existing10-test result unchanged. This evidence append was
checked for UTF-8/LF/no BOM and git diff --check before publication to #471.

Smallest next step: offline fake-transport qualification of this temporary
worker's installed-CLI wrapper and fixed failure-stage reporting. A new live
quota attempt requires separate approval after that work, because the approved
preflight stopped on first failure and prohibited retries. Do not deploy from an
incomplete baseline. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Read-only quota and remaining metadata continuation - 2026-11-20

Owner requested a fresh quota check, then explicitly requested continuing the
remaining metadata queries. The transient worker's earlier failure was addressed
by initializing CLI rest context and allowing normal CLI authentication transport;
no credential value was printed. Two earlier quota-worker attempts sent zero ARM
requests. A subsequent single actual quota GET returned CPU time0/3600000ms and
WPStopRequests0/15. The helper required three selected counters and therefore
reported BLOCKED despite capturing those two valid counters; short-period CPU
remains UNVERIFIED. Do not equate that helper validation result with exhausted
quota. No further quota GET was made in the remaining-query continuation.

At UTC2026-11-21T01:52:59.656918+00:00
(Sao Paulo2026-11-20 22:52:59.656918-03:00), the approved remaining-query run
performed four exact ARM GETs sequentially, with one send each, no retry.
Total14.346s, owned Windows Job/pipe cleanup verified; per-worker15s bounds.

| Read | Result |
| --- | --- |
| Shared plan | PASS; exact West US2 Linux F1 identity |
| API site | PASS; exact expected plan/site, Running, usage/availability Normal, HTTPS-only true |
| API config/web | PASS; accepted child2744c4dc... configured, startup empty |
| API config/logs | BLOCKED after10.539s; ARM_TRANSPORT_STATUS_OR_SCHEMA_UNVERIFIED, no usable logging projection |
| UI site, config/web, config/logs | NOT_RUN; stopped at prior failure |

Safe accepted API reference:
`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`.
This is CONFIGURED_ONLY, not proof of running container bytes, health or dataset.
The logging failure's exception type was withheld as OTHER; no HTTP status or
specific cause is established. No logging retry occurred. Baseline remains
incomplete; do not reconstruct missing logging/UI fields from past findings.

No app-settings/key read, Azure mutation/restart, registry/Docker action, data
transfer or health/browser probe occurred in these query continuations. Actual
ARM inventory across fresh quota plus remaining run:1quota GET +4metadata GETs;
previous zero-send worker attempts are not counted as cloud reads. Normal CLI
authentication may perform its own token traffic, not additional resource queries.
No code/test changes; prior10 focused tests stand. This append passed UTF-8/LF/no
BOM and diff checks. PR remains OPEN/UNMERGED.

Next smallest scoped action: separately approve one replacement read of only the
failed API config/logs endpoint, with fixed transport/status/schema classification
and no raw log data. No deployment from this incomplete baseline.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Approved API logging-only retry - 2026-11-20

Owner separately approved one replacement API config/logs GET only.
UTC2026-11-21T01:55:37.917047+00:00 (Sao Paulo2026-11-20 22:55:37-03:00):
PASS, one ARM request,1.640s,15s process cap, owned Job/pipe cleanup verified.
Application filesystem logging Off; HTTP filesystem logging disabled;
failed-request tracing disabled; detailed errors disabled. Only projected
nonsecret configuration was retained, not logs/content/credentials.
No logging change, settings/key read, restart, health probe or other query.
The previous failure remains historical; its cause was not established by success.
UI site/web/logging remain NOT_RUN. Next phase needs approval for those3 reads
only,15s each/45s total, no retries, stopping on state/drift/error. No deployment
from the still-incomplete two-app baseline. No code changes; prior tests stand;
UTF-8/LF/no BOM and diff checks passed for this append. PR #471 OPEN/UNMERGED.
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Approved UI baseline reads - 2026-11-20

UTC2026-11-21T01:58:04.458619+00:00 (Sao Paulo2026-11-20 22:58:04-03:00):
3 approved exact GETs, no retries,12.983s total, owned Job/pipe cleanup verified.
UI site PASS: expected plan/region/Linux, Running, usage/availability Normal,
HTTPS-only true. UI web PASS: startup empty, historical configured image
`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07`.
That image is not the newly verified WP61 runtime child6b59d205...; no accepted
WP61 UI deployment is inferred. UI logging GET BLOCKED after10.580s, fixed
ARM_TRANSPORT classification, ConnectionError; no HTTP status/raw error shown.
Stop condition observed; no retry or mutation/probe/settings/key access.
UI logging remains missing, so the two-app baseline is still incomplete.
Next smallest scope: one separately approved bounded UI config/logs retry only.
No code changes; prior10 tests stand. Evidence LF/UTF-8/no BOM/diff checks passed.
PR #471 remains OPEN/UNMERGED. PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## WP76 - actual staging execution stopped by fresh quota - 2026-11-20

Owner authorized actual API-first/UI-second deployment subject to fresh quota,
rollback/settings preservation and stop conditions. Live reconciliation found
#471 already MERGED (head8e4248db1aede8c1fcf19ab8b6291bc1515c5801), not open.
Current main `d392efcda4bf8deb461a7229ed56c5aa5bc3cad9` contains that merge and
#470. A separate clean follow-on branch was created from main; the #471 branch,
owner worktree and unrelated PRs including #457 were not overwritten/modified.
No registry re-query or publication was needed: WP75's verified mapping remains
timestamped historical evidence, not a new freshness claim.

### Pending logging retry executed once

UTC2026-11-21T02:30:04.689122+00:00
(Sao Paulo2026-11-20 23:30:04.689122-03:00): one exact UI config/logs GET,
PASS in2.194s, one ARM send,15s process cap, owned Job/pipe cleanup verified.
Application filesystem level Off; HTTP filesystem logging, failed-request tracing
and detailed errors disabled. Only projected fixed logging fields retained.
No unrelated metadata query was folded into this logging-only operation.

Offline installed Azure CLI merge proof also passed using the existing WP36
fake-data verification, CLI2.89.1, no Azure call/real key. This proves the tested
installed merge path on fake values, not actual retained-setting preservation.

### Fresh deployment preflight stopped before mutation

UTC2026-11-21T02:31:53.551494+00:00
(Sao Paulo2026-11-20 23:31:53.551494-03:00): one exact shared-plan usages GET,
worker result **BLOCKED**, failedStage **FRESH_QUOTA**, category
**QUOTA_PRESSURE**,1.013s, one ARM send, process exit0 with structured failure;
owned Job/pipe cleanup verified. Request uses15s hard worker timer,3s connect/
10s read limits,1MiB cap, no redirects or resource retries; total120s cap.

The worker rejects numeric watched CpuTime/WPStopRequests values unless
0<=current<limit. At least one returned watched pair failed that condition.
Its failure path did not retain which pair or its numeric values; do not invent
the exhausted counter, exact usage, reset time or cause. This is the fresh
preflight outcome, not proof that all earlier successful0-counter readings were
wrong. No second quota GET or automatic retry was made in WP76. Future reporting
should project both sanitized numeric counter pairs before evaluating the gate.

**Stopped as WP76 explicitly requires on quota pressure.** All remaining plan,
site/web, settings/slot rollback baseline reads in that worker were NOT_RUN.
The baseline was designed for private in-memory retention only, but no settings,
key or slot baseline was actually read because quota failed first. No private
settings/credentials/datasets were printed or persisted. No full rollback
baseline is claimed; no secret/data transport or initializer was accessed.

API accepted-image/configuration apply, authenticated snapshot checks, UI
promotion and browser acceptance were NOT_RUN. No health request was sent to
consume app quota. Existing configured API accepted-child and UI old-image
observations remain historical only, not freshly reverified by this preflight.
The accepted UI child remains the WP75 verified6b59d205... artifact; no diagnostic
API substitution, repatch, image build/push or deployment occurred. No config
rollback was necessary or attempted because nothing was mutated.

### Validation and exact inventory

| Executed offline validation | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q` | 10 passed, exit0, zero failures/skips |
| `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py` | PLAN_ONLY, exit0, zero operations |
| Installed CLI existing fake merge probe | PASS, CLI2.89.1, azureCalls0, actualKeyUsed=false |

The default planner still lists generic design prerequisites; its static blocker
list is not a new assertion that WP75's recorded UI publication is unknown.
No source changes were necessary; this follow-on PR changes only this append.
Syntax compilation, UTF-8/LF/no BOM, Markdown link/table sanity, unchanged
planner/tests bytes, prior-evidence byte-prefix and git diff --check passed.

Live inventory: **2 exact read-only ARM resource requests** (one UI logging,
one plan usages); normal existing CLI authentication only; no resource retry.
No Docker, registry, settings/key access, Azure mutation/restart/scale/resource
recreation, public/health/browser/provider/data-transfer operation or deployment.
No quota workaround or paid upgrade. PR remains OPEN/UNMERGED.

Concrete blocker: fresh F1 quota preflight not passing. The smallest next action
is a separately requested single projected quota-only read after recovery,
retaining both counter values and supplied reset fields if present. Do not run
another deployment attempt or app probe before that succeeds. No new diagnostic
work package, classifier change or rebuild is needed. Missing cloud data/key
channels may still be later blockers, but were not reached or diagnosed here.
Docker-first/dual-process, API-first/UI-second and auth/TLS/storage/ports unchanged.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## WP76 resumed - supplied quota accepted, API serving blocked - 2026-11-21

Updated owner authority accepts supplied stop requests0/15 and CPU5596/3600000ms
as successful quota preflight and forbids another quota/plan-usages GET or the
completed UI logging retry. Both prohibitions were respected: **zero quota and
zero logging reads**. Base main `54c222100ab7b9fbecc26527ebfa5f0084985a4a`
contains merged #470/#471/#472/#473. A clean isolated follow-on branch preserves
the owner's currently modified WP76 prompt on local main and all historical
WP75/WP76 evidence. No other branch/PR was overwritten.

### Fresh target and private baseline read

UTC2026-11-21T12:18:15.594608+00:00
(Sao Paulo2026-11-21 09:18:15.594608-03:00):9 exact ARM resource requests,
6.443s total, no resource retries, owned Windows Job/pipe cleanup verified:
plan GET; for each exact staging app, site GET, config/web GET,
config/appsettings/list POST (read-only) and config/slotConfigNames GET.
Normal existing CLI authentication only; no token/secret output. Existing
installed CLI2.89.1 fake-data merge proof passed offline with azureCalls0 before
these operations. It is not a claim of an actual settings save.

Transport allowed only those exact targets and normal login transport, no
redirects/resource retries,1MiB response cap,3s connect/10s socket limit,
15s hard per-request worker timer and120s total process cap. Entire settings
collections/keys and slot metadata remained private in worker memory only,
never printed/persisted or sent to Codex as values. References were cleared on
worker exit; no secure-memory-erasure guarantee is claimed.

| Fresh check | Result |
| --- | --- |
| Exact plan | West US2 Linux F1 identity PASS |
| Both staging sites | Running, usage/availability Normal, HTTPS-only PASS |
| API image | Accepted Linux/amd64 child2744c4dc... configured; startup empty |
| API reviewed nonsecret settings | All match; no configuration save required |
| API retained service-key setting | Present (value withheld) |
| UI image | Still older a06baf... child, not accepted WP61 child |
| UI reviewed nonsecret settings | Not all match (no values/unrelated names exposed) |
| UI retained service-key setting | **Absent**; same-key condition false |
| Both slot baselines | Captured privately in memory, not altered |

The private settings/image/startup/HTTPS/slot baseline was captured for safe
comparison, then cleared. No secret rollback artifact was persisted. With no
mutation it was not needed for restoration; any future mutation must recapture
and check a current in-memory rollback baseline. Logging was not reread because
it was already complete and no logging change is proposed. No full auth/TLS
configuration requalification is inferred from these checks.

Accepted API already matched image/settings/empty-startup/HTTPS contracts;
**no needless API repatch, settings save or restart was performed**. UI key
absence is a concrete promotion blocker. No key generated, copied into a
command, rotated or set in Azure. UI configuration remains untouched until
API authentic data acceptance and a privately established same-key channel.

### API runtime attempt and required stop

UTC2026-11-21T12:18:47.221351+00:00
(Sao Paulo2026-11-21 09:18:47.221351-03:00): one anonymous TLS-validating GET to
the exact API /health,8s hard owned-worker cap,7s socket timeout,4096-byte
accepted body cap plus one overflow sentinel, no proxy/credentials/redirects.
It returned fixed **STOP_TLS_OR_TRANSPORT**, no HTTP status or Alive response,
7.837s elapsed. No raw body/error/identifier was exposed. Owned process cleanup
completed. TLS verification was enabled; no successful handshake is claimed.
The sanitized result does not distinguish DNS/connect/TLS/read-timeout causes.

WP76 explicitly requires stopping on transport/TLS failures; **no retry** was
made, not a nine-request poll loop. Authenticated BTC/ETH snapshot verification,
private state/UID/data inspection, UI image/settings promotion and browser
acceptance are **NOT_RUN**. Health failure is not attributed to service-key
mismatch, missing database, quota, the image or an Azure exception without
evidence. A configured accepted image is not proof it is serving.

### Validation, operation inventory and next concrete action

| Executed offline validation | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q` | 10 passed, exit0, zero failures/skips |
| `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py` | PLAN_ONLY, exit0, zero operations |
| Existing installed CLI fake merge-preservation probe | PASS, CLI2.89.1, no actual key or Azure call |

Only this evidence appendix changes; planner/source/tests remain unchanged.
Its generic default blocker text is not refreshed cloud evidence or a reversal
of WP75's verified UI mapping. Syntax compilation, UTF-8/LF/no BOM, Markdown
links/tables, prior-evidence prefix, exact one-path scope and git diff --check
passed before publishing the follow-on PR, which remains OPEN/UNMERGED.

Actual live inventory:9 read-only ARM resource requests and1 API health GET.
**Zero quota/plan-usages/logging/registry/Docker/provider calls; zero Azure
mutations/restarts/deployment, key rotation, data transfer/import or UI/browser
probes.** Same fixed accepted API2744c4dc... and UI6b59d205... child artifacts;
no diagnostic substitution, build/push, production target or quota workaround.
Auth/TLS/storage/ports and API-first/UI-second sequence preserved.

Immediate next action requires a new narrow scope: a single exact API serving
attempt with fixed DNS/connect/TLS/HTTP stage reporting, or existing supported
startup evidence if preferred, because current transport failure stopped this
execution. Do not repeat quota/logging/classifier work. Before UI promotion,
owner must also enter the SAME retained research key privately as UI
AIQ_RESEARCH_API_KEY using the established owner-controlled portal workflow,
under an exact-setting authorization; do not request the key in chat. Private
Azure data availability remains unverified, not diagnosed as missing. No broad
private-data transfer or new import endpoint is proposed without an actual
state result. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## WP76 approved continuation - staged API serving request - 2026-11-21

Owner said continue after the prior stopped serving attempt and the proposed
narrow staged check. UTC2026-11-21T12:22:10.907962+00:00
(Sao Paulo2026-11-21 09:22:10.907962-03:00): exactly one GET to API /health,
no redirect/proxy/credentials/retry/fallback. DNS resolved, first selected socket
connected, TLS certificate/hostname validation passed. **HTTP403**, body not
Alive; bounded body <=4096 bytes (one overflow sentinel permitted). Response
body/headers/addresses were not output or persisted. Probe1.061s, total1.178s,
8s owned-process cap and verified cleanup. This is a fresh request, not a
retroactive explanation of the previous transport failure.

API is reachable through validated TLS, but serving acceptance FAIL: not200Alive.
403 origin remains UNDETERMINED; no claim of quota, service-key mismatch,
application auth, access restriction, database failure or running image identity.
No authenticated snapshot/data probe or UI promotion. Stop condition respected;
no automatic follow-up. Zero ARM/quota/logging/settings reads, Azure mutation,
registry/Docker/provider/data/key operations in this continuation.

Evidence-only append: LF/UTF-8/no BOM and diff checks passed; no source/test
changes or redundant suite rerun. Existing10-test pass remains historical.
Next concrete scope, if separately authorized: selected read-only API ingress/
authentication configuration metadata to classify the403, with values/credentials
withheld; do not change access restrictions or auth based on this status alone.
UI key absence remains a later promotion blocker. PUBLIC_RUNTIME_VERIFIED=False;
liveAttemptReady=false. PR #475 remains OPEN/UNMERGED.

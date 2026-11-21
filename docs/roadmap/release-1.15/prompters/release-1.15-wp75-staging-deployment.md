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

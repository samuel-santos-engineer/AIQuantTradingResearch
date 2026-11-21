# WP77 - accepted UI image promotion attempt - 2026-11-21

Status: **STOPPED BEFORE WRITE: UI baseline state/security drift**.
UI image deployment has NOT succeeded in this attempt; integration remains pending.
Owner explicitly authorized promotion before API acceptance, superseding the
prior API-first runtime gate for this image-only operation. API403 was not used
as a gate. Required UI baseline checks still apply and stopped execution.

## Reconciliation and scope

Base main `54c222100ab7b9fbecc26527ebfa5f0084985a4a` contains merged #470/#471/
#472/#473. PR #475 was verified OPEN at
`ac59e40ef961bb086cba1e8338ce0cb1ac6f94ea`; it was read but not modified.
New isolated follow-on branch preserves the owner's modified WP76 prompt and
untracked WP77 prompt on main. Historical WP75/WP76 evidence unchanged.

Exact UI target:
`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`.
Accepted API child2744c4dc... remains selected; no API mutation/query was issued.
The index/local ID3e323b0d... is not used as the runtime target. Historical UI
baseline a06baf... is not substituted for the accepted UI child.

## Minimal execution implementation

`eng/azure-cli/r1.15-qualification/wp77_ui_promotion.py` implements only the
exact UI baseline -> one targeted config/web PATCH -> private preservation and
image readback transaction, using an injected request function. Its CLI default
is PLAN_ONLY with no live adapter/process/network. No live switch was added.
The approved operation was invoked through a transient existing-installed-CLI
worker, using exact method/path/body allowlists, no redirects/resource retries,
3s connect/12s socket limits,25s hard per-request worker timer,1MiB capture/body
limits,180s owned Windows Job cap. Normal CLI authentication only. Worker files
contain reviewed code/targets only; no baseline secret persisted or output.

Before writing, the transaction requires exact site/platform/plan and Running/
Normal/HTTPS checks, expected old-or-target image/empty startup, port8501,
private settings/slot baseline. Missing UI key does NOT block image promotion.
The PATCH can change ONLY linuxFxVersion to the exact target and appCommandLine
to empty; no settings write/restart/API operation is available. It skips PATCH
if already configured. Then it checks image/startup, unchanged settings/slots,
selected web security fields and plan/HTTPS. No full auth requalification claim.
Private baseline dictionaries are cleared after use, without secure-erasure claim.

## Actual Azure result

UTC2026-11-21T12:30:56.609889+00:00
(Sao Paulo2026-11-21 09:30:56.609889-03:00):

| Observed item | Result |
| --- | --- |
| Exact UI site GET | HTTP200,1.041s |
| Exact resource identity and Linux/WestUS2 checks | Passed before the later combined gate |
| Combined expected-plan/Running/usageNormal/availabilityNormal/HTTPS gate | Failed: SITE_STATE_OR_SECURITY_DRIFT |
| Failed stage | UI_SITE_BASELINE |
| Total bounded worker | 1.413s, owned Job/pipe cleanup verified |
| PATCH attempts | **0** |
| Azure may have changed from this attempt | **false** |
| Retry / rollback / explicit restart | **0 / 0 / 0** |

The first transaction version retained only the combined gate, not individual
failed booleans or raw state. Which member failed is UNKNOWN. Do not invent
quota pressure, plan drift, stopped state or HTTPS disablement as the cause.
HTTP200 here is a management response, not application health. No follow-up
query was executed after the stop. Fixed per-field siteChecks booleans were
added OFFLINE afterward for a future separately approved attempt; they do not
retroactively classify this response. No raw private value/error/address was
printed or persisted.

The site/config baseline is unsafe/incomplete under this gate, so WP77's stop
condition overrides proceeding blindly. config/web/settings/slot reads, image
PATCH/readback, health/startup/403 ingress-auth diagnostics and browser tests
were NOT_RUN. No original-image rollback baseline was freshly captured, and
no rollback was needed because no mutation occurred. Last historical UI key
absence is not a new key observation in WP77.

| Required acceptance flag | Current evidence |
| --- | --- |
| UI_IMAGE_CONFIGURED | UNVERIFIED in this attempt (boolean verification false) |
| UI_RUNNING_IMAGE_VERIFIED | UNVERIFIED |
| API_HEALTH_ACCEPTED | false; historical403, no new probe |
| SAME_KEY_PRESENT | UNVERIFIED/false; no private settings read reached |
| BTC_ETH_BROWSER_ACCEPTED | false, NOT_RUN |

## Offline validation and limits

Initial fake suite found one readback-variable defect (1 failure in8 tests);
it was corrected BEFORE the Azure operation. Eight fake tests then passed.
After the stopped operation, per-field fixed baseline reporting was added and
all8 tests passed again. Existing WP75 suite:10 passed. Final distinct total:
**18 passed, zero failures/skips**; repeated runs are not extra distinct cases.

```text
python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp77_ui_promotion.py -q
python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q
python -B eng/azure-cli/r1.15-qualification/wp77_ui_promotion.py
```

Default: PLAN_ONLY/azureOperations0. Tests cover missing-key non-gating,
minimal one-PATCH scope, idempotence, settings/slot preservation, baseline drift,
ambiguous-write no retry/rollback, post-write preservation failure reporting,
non-disclosure and default/import process/network denial. Syntax compilation,
UTF-8/LF/no BOM, exact three-path scope and git diff --check passed before PR.
Cloud results above are independent of fake-test assertions; fake success is
not proof of running image or settings preservation in Azure.

Live inventory: **one UI site ARM GET, zero writes**. No API, quota/usages,
logging, registry/GHCR/Docker/image, settings/key, slot/auth-restriction write,
TLS/storage/plan/resource mutation, provider/data/import/health/browser or
production operations. All existing data/settings/key/security retained by no
mutation, not by a completed post-PATCH preservation test. No automatic retry.

Next smallest exact action requires owner approval: one UI site-only projected
read using the new fixed checks, to identify which required baseline condition
changed (no quota/logging/private settings read). Once safely resolved/recovered,
resume this exact accepted-image transaction; do not weaken the security gate or
block it on API403. Missing UI key must later be entered by the owner through
the established private portal workflow as the SAME retained API research key;
never request the value in chat or rotate it. No rollback of a future correctly
configured image solely for key absence. No integration success inferred.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Approved single site-only follow-up - 2026-11-21

UTC2026-11-21T12:33:35.048221+00:00 (Sao Paulo2026-11-21 09:33:35-03:00):
owner approved exactly one read-only UI site check; one ARM GET,1.178s total,
15s owned-process bound/cleanup. Fresh fixed projection: state **QuotaExceeded**,
usage **Exceeded**, availability **Normal**, exact plan/Linux/WestUS2 and
HTTPS-only checks true. Running and usageNormal checks false. This fresh result
identifies an actual current hard quota-state blocker; it is not a retroactive
raw-response classification or numeric quota-counter measurement. The earlier
owner-supplied0/15 quota observation remains historical. No quota/usages GET,
logging/settings/key reads, retry, health probe or mutation occurred.
No image write, automatic restart/rollback, scaling or quota workaround.
UI promotion must wait until the app is no longer quota-exceeded; one future
site-only recovery confirmation needs owner authorization. PR #476 stays open;
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false. Evidence-only append
passed UTF-8/LF/no BOM and diff checks; source/tests unchanged.


## Owner-approved quota check and conditional resumption - 2026-11-21

Owner requested one quota check and continuation if reset. At
UTC2026-11-21T18:04:12.642298+00:00 (Sao Paulo15:04:12-03:00), one shared-plan
usages GET returned WPStopRequests0/15 and CpuTime5596/3600000ms: available
for returned counters. No quota retry. Supplied reset fields were retained only
as returned metadata, not interpreted as runtime readiness.

Conditional UI transaction resumed once atUTC2026-11-21T18:04:40.736887+00:00
(Sao Paulo15:04:40-03:00). Four exact ARM requests returned HTTP200: site0.927s,
web0.903s, private settings-list0.635s, slotConfigNames0.865s. All five fixed
siteChecks passed: expectedPlan/running/usageNormal/availabilityNormal/httpsOnly.
UI key setting remained absent (value never read out/persisted).

The transaction STOPPED at PRIVATE_SLOT_BASELINE with RESPONSE_IDENTITY.
The expected slot resource-ID comparison failed; actual ID/name values were not
retained, so absence versus differing identity is not established. Resource
request scope was exact, but response identity was not proven. No automatic
retry or relaxation. PATCH attempts0; azureMayHaveChanged=false. Worker3.746s,
owned Job/pipe cleanup verified. Private settings stayed transient in memory.
No image/settings/security write, API/health/browser/key/data operation or rollback.

Offline review located existing WP21/WP22 slot-metadata validation, but that
alone does not explain this response or justify silently skipping identity.
No source changes or test rerun in this evidence-only continuation. Prior18-test
pass remains historical. This append passed UTF-8/LF/no BOM and diff checks.
Total live inventory:1quota GET +4transaction reads, zero mutation/retries.

Next minimal separately authorized operation: one exact slotConfigNames GET
projecting only id-present/exact-UI-resource-match and fixed schema/type flags,
never raw slot names, IDs or settings. This can distinguish a nullable envelope
from genuine target drift before a source-backed validation correction; do not
rerun all baseline queries blindly. UI_IMAGE_CONFIGURED remains unverified;
PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false. PR #476 OPEN/UNMERGED.


## Approved slot-envelope read and offline correction - 2026-11-21

UTC2026-11-21T18:12:55.614504+00:00 (Sao Paulo15:12:55-03:00): one separately
approved exact UI slotConfigNames GET;1.680s total,15s owned-worker cap, no retry.
Only fixed flags retained: id field PRESENT and explicitly NULL; name present/
non-null but not the assumed slot-resource name; properties object with exactly
all3 standard keys, each nullable string-list typed, no unexpected property keys.
No raw ID/name/list/setting value was output or saved. No mutation or additional
site/quota/logging/health read occurred. No claim of cross-target drift follows
from a null ID; this establishes the observed nullable envelope shape only.

Offline correction adds slot_properties for this exact endpoint only, called
after exact UI site validation. Explicit null id is accepted; missing id, any
wrong non-null id or nonstring id still reject. All3 exact property keys and
nullable-string-list types are required, with no extra keys. Normal site/web/
settings responses retain strict non-null identity validation. Request transport
still binds exact UI path and forbids redirects. No broader identity bypass,
settings write, slot normalization or change to PATCH scope.

Existing WP22 nullable-list preservation logic was reviewed offline; lists are
preserved verbatim, not normalized. The null-ID exception is justified by this
approved observation plus exact transport/site binding, not a universal claim
that all Azure resources allow missing IDs. Added2 fake tests for null-ID
promotion/preservation and missing/wrong-ID/schema/type rejection before PATCH.
Final tests:10 WP77 +10 WP75 = **20 distinct tests passed**, zero failures/skips.
No live transaction rerun under this read-only approval. Syntax, UTF-8/LF/no BOM
and diff checks passed before committing. Historical earlier failures preserved.

Next exact approval: rerun the corrected UI-only transaction once (4baseline
reads, at most1targeted accepted-image PATCH,4post-check reads;9resource requests
maximum), no quota/logging/API reads, settings write/restart/retry. Key absence
does not gate this image promotion; integration flags remain false until accepted.
PR #476 OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

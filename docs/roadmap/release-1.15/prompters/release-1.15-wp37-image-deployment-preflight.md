# Release 1.15 WP37 - Research image deployment preflight

Status: OWNER-APPROVED APPLY EXECUTED ONCE; configuration readback passed. Subsequent authorized read-only investigation confirmed HTTP 403 and recurring QuotaExceeded; no healthy serving established. Attempt STOPPED; no retry or automatic recovery. The actual-result section below supersedes pre-approval checkpoints. Date: 2026-11-02. UTF-8/LF/no BOM mandatory.

Baseline: current main containing merged PR #424, `9196ba26893a80f79111318a88f3d7ad509f3ca1`. Work is isolated from the owner's dirty checkout. Reviewed the merged WP36 helper, PowerShell wrapper, runbook and tests. No redundant controller, new dependencies, application changes, image rebuild/repush or historical private-record access.

## Actual sanitized preflight

Default Python Plan: `PLAN_ONLY`, exit 0. Read-only Inspect executed before and after the targeted CLI invocation fix, both `METADATA_VERIFIED_NO_MUTATION`, exit 0. The second run validates the corrected direct installed-CLI entrypoint and explicit subscription. Inspect uses only anonymous registry manifest reads and projected app/plan metadata; no settings/key/connection-string APIs or live health probes.

| Field | Current observation |
| --- | --- |
| Subscription | `7f5eabb5-80f5-4369-88be-b14a6de763f1` |
| Group | `rg-aiq-r112-wp05-wus2-7f5eabb5` |
| Research app | `aiqr115researchstagingwus27f5eabb5` |
| Plan | `asp-aiq-r112-wp05-wus2-7f5eabb5`, Linux F1, West US 2 |
| State | Running |
| Hosting | Empty `linuxFxVersion`, no classic image/sidecar; startup override empty |
| Platform HTTPS-only | False |
| Anonymous registry access | PASS; exact OCI index and Linux/amd64 runtime manifest bytes match their SHA-256 digests |
| Ten settings/key facts | Prior WP36 single save and private preservation checks PASS; not repeated or re-read in WP37 |
| Azure deployment/image readback after mutation | NOT PERFORMED |
| Azure `/health` | UNVERIFIED; no live probe |

Fixed image: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`. Runtime Linux/amd64 manifest: `sha256:fb9aab67753be84ca3300876fa4ed4d669ab0d897beb49f3fb8f2eae58b9b012`.

No drift from the expected initial state was observed. Read-only metadata and registry access do not establish that this exact F1 app accepts Code-to-classic conversion or that Azure can pull/start the image. An owner-approved apply is a bounded compatibility attempt, not proven support. No plan upgrade, sidecar migration, new resource or alternate artifact fallback.

## Necessary targeted corrections

1. ARM operations now invoke the exact installed Azure CLI Python entrypoint directly, with explicit subscription and unchanged fixed resource/mutation allowlist. Previously `az.cmd` introduced an intermediary shell whose Python child could survive a shell timeout. Direct subprocess invocation with a 25-second timeout removes that intermediary. This is not PowerShell policy bypass.
2. Health requests now run in an isolated, directly launched Python child with a whole-process timeout of at most 8 seconds, capped by the remaining 180-second observation budget. A socket timeout alone does not cover total DNS/TLS/header/body duration. The child uses normal TLS verification, fixed anonymous GET, no redirects, no API key, reads at most six body bytes and returns only status/body-match booleans. Timeout kills/reaps the child; no shell, extra service or persistent worker. Parent refuses late success after the observation deadline. Ordinary OS scheduling/process-creation/termination overhead is not claimed to be zero.
3. Apply now rejects pre-write drift from `EMPTY_CODE_MODE` and platform HTTPS-only false. Shared Inspect still allows recognizing the expected deployed image for post-apply readback. A second apply after a completed/partial attempt must not silently become a restart retry.

The three mutations, separate approvals and existing settings implementation are otherwise unchanged. No settings action or old WP21 controller is invoked by Apply.

## Exact proposed live operation - NOT authorized by WP37 prompt alone

Only after explicit separate owner approval:

1. PATCH the exact research `/config/web`, API `2023-12-01`, with only:

```json
{"properties":{"linuxFxVersion":"DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f","appCommandLine":""}}
```

2. PATCH that research site with only `{"properties":{"httpsOnly":true}}`.
3. POST that research site's `/restart` **once**. Configuration changes may also implicitly recycle it; no extra restart loop.

The fixed resource prefix is `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5`. No settings API, key access, slot flags, logging, plan or UI changes. Factual key/settings switches are satisfied by the earlier verified save; they are not live authorization.

Before writes, repeat the safe preflight and save only the existing minimal sanitized original mode/startup-empty/HTTPS-only/plan/state metadata. No full baseline or automated rollback. Following approved restart, read back exact image/platform HTTPS-only, then observe only `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health` for 200 and exact `Alive` within the bounded observation. No authenticated snapshot or provider request.

Preferred command, **do not execute without separate live approval**:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp36ResearchDeploy.ps1 -Action Apply -ApproveExactResearchDeploy -OwnerKeyPresent -OwnerRuntimeSettingsConfirmed
```

If PowerShell policy prevents execution, do not bypass it. Read-only Python Plan/Inspect are qualified. Any proposed use of the Python Apply equivalent must be explicitly included in the owner approval:

```text
python -B eng/azure-cli/r1.15-qualification/wp36_research_deploy.py --action Apply --approve-exact-research-deploy --owner-key-present --owner-runtime-settings-confirmed
```

On rejection, uncertain timeout or failed health: stop, report safe failure stage/error code and current metadata when available. No automatic recovery/retry, settings replacement or plan upgrade. Manual owner action may be required; exact Code-mode restoration is not proven. The initial mode was empty, startup empty, HTTPS-only false and state Running. No restoration is claimed unless separately authorized and verified.

## Tests and exact paths

`python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp36_research_deploy.py -v`: **38 passed**, exit 0. All regression tests are fake-only. Coverage includes existing 30 gates/preservation tests plus direct CLI/subscription, whole-request deadline, timeout suppression, late Alive rejection, final remaining budget/no extra probe, malformed worker results, redirect/auth rejection and pre-apply drift rejection. Existing settings tests use fake adapters; no real save was repeated.

Actual Python default Plan and corrected Inspect: PASS as above. No live health worker was executed. No .NET build, Docker build/push or dependency install. PowerShell 5.1.26100.9711 wrapper AST parse: zero errors; unchanged wrapper runtime is still subject to policy, not claimed newly qualified. No bypass. UTF-8/no BOM/LF scan and `git diff --check` run before publication.

Changed paths:

- `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py`
- `tests/runtime/release_1_15/test_wp36_research_deploy.py`
- `docs/roadmap/release-1.15/prompters/release-1.15-wp37-image-deployment-preflight.md`

Excluded UI app `aiqr112wp05wus27f5eabb5` is untouched. Settings save is not repeated. PR remains OPEN/UNMERGED; merge is not deployment approval. No owner Apply approval received at this report checkpoint.

`liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Actual owner-approved Apply outcome

The owner separately approved the exact live operation and reviewed Python Apply, including the bounded anonymous health observation and stop-without-recovery condition. Reviewed branch/head before execution: PR #425 OPEN at `eb8936aaadc7c2d41d32f759377a85e0d7f79247`, clean isolated worktree. No settings-save approval was reused as image approval.

Executed once:

```text
python -B eng/azure-cli/r1.15-qualification/wp36_research_deploy.py --action Apply --approve-exact-research-deploy --owner-key-present --owner-runtime-settings-confirmed
```

Actual result: inherited helper enum `WP36_BLOCKED`, `failedStage=ANONYMOUS_HEALTH`, `failureReason=HEALTH_REJECTED`, `azureDeployed=true`, `azureHealth=UNVERIFIED`, `azureMayHaveChanged=true`, `manualRecoveryRequired=true`, `currentState=UNVERIFIED`. The execution tool reported nonzero exit 1. This is a failed overall qualification, not a successful working deployment.

| Phase | Actual evidence |
| --- | --- |
| Fresh registry/metadata preflight | PASS; expected immutable image/Linux-amd64 and initial empty Code mode, HTTPS-only false, Running Linux F1 |
| Sanitized original snapshot | Saved locally before mutation; no key/settings values or full configuration archive |
| Image/startup PATCH | Azure accepted; helper progressed beyond this stage |
| HTTPS-only PATCH | Azure accepted; helper progressed beyond this stage |
| Explicit restart | Exactly one POST completed; no restart loop |
| Configuration readback | PASS; expected immutable classic image and platform HTTPS-only true; metadata validator required Running/Linux F1/exact target/plan/startup-empty at that readback |
| Anonymous HTTPS health | FAILED with `HEALTH_REJECTED`; no 200 `Alive` acceptance |
| Final current-state inspection after health failure | UNVERIFIED; helper caught inspection failure and did not expose its details |
| Automatic retry/recovery | NONE; stopped as authorized |

The health classifier rejects redirects (301/302/303/307/308) and authentication/access statuses (401/403) under `HEALTH_REJECTED`. The exact observed status was not retained in this helper's report. Do not invent a particular redirect, authentication challenge or F1 quota cause. No response body, headers or secrets were printed. There was no additional health request after the terminal result.

The last confirmed configuration is the requested immutable image with empty startup and platform HTTPS-only true; its readback also confirmed Running on the existing Linux F1 plan. **That is last-confirmed state, not verified final/current state.** No current health or process-alive claim follows from it. The final metadata inspection was inconclusive and no further query was issued after the stopped attempt.

Azure accepted the configuration transition on this exact F1 resource, so there is no evidence of a configuration-API rejection for F1. This does not establish successful image pull, healthy runtime startup or complete F1 runtime compatibility. The local image/registry evidence remains distinct from Azure health failure.

No settings API, key read/rewrite, SettingsApply, slot/logging/plan/UI mutation, registry push, image rebuild, authenticated endpoint or provider operation occurred in this attempt. Configuration updates may have implicitly recycled the app, in addition to the single explicitly approved restart.

### Required owner disposition

Manual owner review is required; no automatic rollback has been performed or claimed. Do not rerun Apply: the expected initial mode/HTTPS state has changed and the drift guard should stop a repeat. No recovery has been authorized by this outcome.

A proposed next authority can narrowly permit sanitized read-only current app/mode/state inspection and collection of the exact anonymous health status/redirect classification, without following redirects, settings/key reads, explicit restart or other mutation. That diagnostic was **not executed** after this failure. Any recovery or further live request needs separate owner disposition under the WP37 stop condition. Original sanitized metadata was empty Code mode/startup, HTTPS-only false, Running; restoring Code mode remains unproven and must not be assumed safe or complete.

PR #425 remains OPEN/UNMERGED. This evidence-only update does not alter the failed helper or repeat qualification. Prior **38 fake-only tests** and UTF-8/LF checks still apply; no new code change warrants repeating tests. `liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Follow-up investigation authorized by "continue to fix"

No new mutation, restart, settings/key read, rollback, rebuild, push, provider call or paid resource was performed. The following are subsequent read-only observations; they supersede only the earlier unknown current-state/status fields, not the original attempt history.

- Exact research app: `state=QuotaExceeded`, `usageState=Exceeded`, `availabilityState=Normal`, `enabled=true`, `kind=app,linux,container`.
- Config still matches the approved immutable image and empty startup; no sidecar. Platform HTTPS-only is true.
- Anonymous `/health`: **HTTP 403**, no redirect. A bounded body classification matched the platform site-disabled/stopped page, not an IP-forbidden page or authentication challenge. No raw HTML/headers were published.

### Quota evidence and reset experiment (read-only)

The site usage endpoint returned mostly sentinel limits (`-1`) and an unhelpful zero filesystem limit. It did not establish the exceeded allowance; the exact existing plan's read-only usage endpoint supplied finite limits instead. Plan-reported values:

| Counter | Observed current | Reported limit | Interpretation |
| --- | ---: | ---: | --- |
| `WPStopRequests` | 17 | 15 | Exceeded counter |
| `CpuTime` | 2 ms | 3,600,000 ms | Below limit as reported |
| `CpuTimeShortPeriod` | 0 ms | 180,000 ms | Below limit as reported |
| `BytesSent` | 2,105 | 173,015,040 | Below limit |
| `MaxMemoryWorkingSetShortPeriod` | 0 | 1,073,741,824 | Below limit as reported |
| `FileSystemStorage` | 696,320 | 1,073,741,824 | Below limit |

These are plan-reported counters, not proof of which process/app generated every stop. Do not infer daily CPU exhaustion, silently convert sentinel limits into enforced quotas, or assert the research application itself crashed without startup evidence.

Azure reported the stop-counter reset at 23:00 on 2026-11-02. After that boundary, a read-only check observed `WPStopRequests=0/15`, app `Running`, usage `Normal`, HTTPS-only true. **No stop/start/restart or other recovery caused this transition.** The first anonymous health request timed out. A subsequent bounded read-only cold-start health observation terminated with HEALTH_REJECTED. The next targeted state/quota check found `WPStopRequests=26/15`, app `QuotaExceeded/Exceeded`, HTTP 403 with the site-disabled classification. Next reported reset: 2026-11-03 00:00:00. Waiting for one reset therefore did not resolve the recurring stops.

[Microsoft's quota documentation](https://learn.microsoft.com/azure/app-service/web-sites-monitor) explains quota enforcement and HTTP 403 in Free/Shared plans. Our particular counter/limits and natural reset are direct API evidence; the documentation does not by itself explain why this app/plan repeatedly records stop requests. Do not promise another reset or an explicit restart will fix it. The startup/stop cause remains unresolved.

### Startup diagnostic access and blocker

The documented ARM container-log read action was unavailable before reset and timed out (20-second bound) on a later attempt; no raw log file was downloaded or retained. One timeout surfaced only command/resource metadata, not log content. No publishing credentials or Kudu credentials were requested.

Read-only detector discovery found Linux startup/deployment detectors. Initial schema/classification reads of `LinuxAppDown` and `DeploymentFailureAnalysisLinux` returned datasets but did not establish a startup cause. The fixed scans did not find the application's fixed startup-failure message, image-pull denial, permission failure or architecture failure; absence of a match is not evidence those causes are excluded.

**Automatic approval review rejected the next selected detector-summary read**, stating that new authenticated diagnostic queries need specific authorization and conflict with the retained query restriction. No workaround or alternate read of those summaries was attempted. Owner approval was requested for exactly two existing detector GETs: `/detectors/LinuxAppDown` and `/detectors/DeploymentFailureAnalysisLinux`, API `2023-12-01`, on the exact research resource. Only selected redacted Status/Message/Title/Description summaries would be shown; no raw tables, settings, keys or mutations. Those reads remain pending at this report checkpoint.

### Offline observability corrections, not a runtime fix

The helper now retains the exact rejected/last health HTTP status in `healthHttpStatus`. Failure inspection permits reporting an actual non-Running state such as QuotaExceeded instead of rejecting it merely because Apply requires Running. Normal preflight still requires Running and now gives a fixed `AZURE_QUOTA_EXCEEDED` reason when applicable, preventing a mutation/restart attempt against a quota-blocked app. Target/plan/region/image checks remain enforced. No live Apply or settings action was rerun to verify this correction.

`python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp36_research_deploy.py -q`: **42 passed**, fake-only, exit 0. Four added tests cover quota preflight rejection, non-Running failure evidence, exact status retention and failed-Apply status/current-state reporting with no extra mutations. UTF-8/LF/no BOM and diff checks precede commit. Same three WP37 paths; no application/runtime artifact change.

Current outcome: **NOT FIXED**; exact image and HTTPS-only assigned, recurring stop-request quota exhaustion and HTTP 403 confirmed, underlying stop/startup cause pending authorized diagnostics. No upgrade, repeated deployment, extra restart or automatic recovery. `liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Specifically approved detector reads - completed

The owner subsequently approved the exact `LinuxAppDown` and `DeploymentFailureAnalysisLinux` detector GETs, API `2023-12-01`, on the same research app. Both completed within their 20-second bounds. Only selected redacted summary fields were exposed; no raw tables, settings, key material, image changes, restart or other mutation.

Actual selected findings:

- `DeploymentFailureAnalysisLinux`: Success; as of **2026-11-02 23:00:00 UTC**, no Configuration Checks, Uploaded Package Validations or Package Mounting Checks issues found.
- `LinuxAppDown`: a top-level application-exceptions section with an empty Description, and an organic availability/SLA summary. No specific exception was reported in the selected fields. The mere presence of the section is not proof an exception occurred.
- A follow-up read of the **same approved LinuxAppDown endpoint** inspected nested Status/Message/Title/Description summary fields, still without raw table output. It supplied only a performance/availability title and a link-description directing the reader to the Application Insights Failures blade; no error/cause.

These checkpointed generic deployment checks do not prove a healthy Docker startup, successful runtime pull, process survival or current HTTP health. They do not explain the previously observed stop-request quota exhaustion. No additional anonymous health request or quota/state query ran during this detector-only step. The last observed health/state remain HTTP 403 and recurring QuotaExceeded; do not present older observations as freshly sampled.

The previous automatic-review block on these two selected reads is resolved by the explicit owner approval. Their results are inconclusive rather than a basis for speculative source/runtime changes. No application correction was made from them, and there is no new healthy-serving claim.

Next proposed diagnostic scope, **pending approval**: three existing detector GETs on the same app, `LinuxLogViewer`, `LinuxRunningContainersPerHost`, and `LinuxAppDown2`, API `2023-12-01`, 20 seconds each, selected redacted startup/error summaries only. No raw table output, settings/key access, logging configuration changes, restart or other mutation. No such query has run at this checkpoint. These need specific additional authority because the current approval names only the two completed detectors.

Evidence-only update to the existing WP37 document/PR; no code changes or new test run. Prior **42 fake-only tests** remain the actual code validation. UTF-8/LF/no BOM and `git diff --check` checked before commit. PR #425 remains OPEN/UNMERGED.

`liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

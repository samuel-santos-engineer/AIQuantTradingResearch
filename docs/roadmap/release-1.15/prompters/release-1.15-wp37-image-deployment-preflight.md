# Release 1.15 WP37 - Research image deployment preflight

Status: OWNER-APPROVED APPLY EXECUTED ONCE; configuration readback passed, anonymous health REJECTED, final current state UNVERIFIED. Attempt STOPPED; no retry or automatic recovery. The actual-result section below supersedes pre-approval checkpoints. Date: 2026-11-02. UTF-8/LF/no BOM mandatory.

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

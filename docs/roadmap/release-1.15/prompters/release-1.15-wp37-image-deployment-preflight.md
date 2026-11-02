# Release 1.15 WP37 - Research image deployment preflight

Status: PREPARED FOR SEPARATE OWNER APPLY APPROVAL. Azure deployment NOT performed; Azure health UNVERIFIED. Date: 2026-11-02. UTF-8/LF/no BOM mandatory.

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

# Release 1.15 WP80: Research API standalone acceptance

## Verdict

BLOCKED: the first fresh API site observation reports `QuotaExceeded`, usage `Exceeded`, and not Running. WP80 stopped immediately. No configuration correction is justified by this observation.

Observation: 2026-11-21 21:50:48.452602 UTC / 2026-11-21 18:50:48.452602 America/Sao_Paulo.

## Repository and historical evidence

Base: `632e636b9c39c94130aa1b58336f13a6d15471a2` (merged PR #477). WP79 [PR #478](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/478) remained open/unmerged at reviewed head `7a79de30e18e9c0851b2d2f8ece832d30b887e3d`; its branch and evidence were preserved. WP78 configuration evidence and WP79 quota recovery, subsequent transport failure, inconclusive detector and 20-second health-header timeout remain historical observations, not current runtime proof.

## Offline contract review

- `container/release_1_15/research-api.Dockerfile`: Linux image, user 1654:1654, bind address `0.0.0.0`, exposed port 52731, shell bootstrap entrypoint.
- `container/release_1_15/research-bootstrap.sh`: executes the Worker with `--research-snapshot-api`.
- `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs`: explicit Kestrel listener uses port 52731 and the configured bind address; the listener does not depend on `ASPNETCORE_URLS`. Anonymous GET `/health` returns HTTP 200 `Alive` before service-key validation. Snapshot authentication is a separate gate.

These source contracts do not attest an Azure process, listener, deployed image or dataset. Public HTTPS remains port 443; no port changes were made.

## Live inventory and evidence

Exactly one read-only ARM GET targeted research staging app `aiqr115researchstagingwus27f5eabb5` in the established resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. The projected response verified expected plan, West US 2, Linux, HTTPS-only and availability `Normal`; site state was `QuotaExceeded`, usage was `Exceeded`, and Running was false. Availability Normal does not override the quota blocker.

The owned worker used a 15-second capture bound, 3-second connect/10-second read timeouts, a 1 MiB capture cap and no retries or redirects. Observed execution was 1.437 seconds; owned worker/pipe cleanup completed. No raw response or credentials are included here.

| Acceptance item | WP80 result | Interpretation |
| --- | --- | --- |
| Configured accepted image | NOT_RUN | Historical WP78 configuration only; config/web was not reread after the stop. |
| Actual running-image identity | UNVERIFIED | No runtime digest witness. |
| Azure routing port | NOT_RUN | No settings collection or port projection read. |
| Actual listener | UNKNOWN | Source expects `0.0.0.0:52731`; no runtime witness. |
| Lifecycle/site state | BLOCKED | Fresh `QuotaExceeded`, usage `Exceeded`, not Running. |
| Anonymous `/health` | NOT_RUN | Stopped before serving probes. Prior timeout remains separate. |
| Authenticated BTC/USD snapshot | NOT_RUN | Health prerequisite unmet; cloud data availability UNKNOWN. |
| Authenticated ETH/USD snapshot | NOT_RUN | Health prerequisite unmet; cloud data availability UNKNOWN. |

No quota/usages endpoint, config/web, app-settings, startup detector, UI, production, registry, Docker, provider, health or authenticated API operation followed. No secret access, deployment, restart, configuration mutation or import occurred. Current counter values and the cause of quota recurrence are UNKNOWN.

## Focused offline validation

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 distinct tests passed, exit 0.
- `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase API`: offline plan-only invocation; no live adapter or Azure operation.
- In-memory Python syntax compilation of the existing bounded capture helper and planner: PASS.
- Changed-file UTF-8/LF/no-BOM check and `git diff --check`: PASS.
- Documentation-only delta: this file. No application or helper changes; owner checkout and unrelated prompts preserved.

Offline tests establish neither Azure health nor dataset availability.

## Next smallest action

Owner review of this blocker. A separately authorized, single bounded API `LinuxAppDown` startup diagnostic could investigate termination evidence; it must preserve UNKNOWN when timestamps, role, image or cause are absent. Do not automatically poll quota, repeat health or change configuration. Resume dependent acceptance only after fresh evidence supports recovery. WP81/WP82 were not started.

`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. Historical local acceptance is unchanged.

# Release 1.15 WP54 - single-attempt acquisition proposal and Phase A evidence

Date: 2026-11-08. Baseline main/#440 squash `0fcb75560e3841674316f1c666066e494550857a`, verified merged. Clean isolated worktree. **Phase A complete; Phase B STOPPED pending separate explicit owner approval.** `LIVE_ACQUISITION_NOT_AUTHORIZED / NOT_RUN`.

This proposal is reviewable authorization scope, not permission to execute. Twelve Data remains the sole provider; BTC/USD then ETH/USD authentic 1-minute OHLCV with owner-confirmed BTC/ETH base units. No source migration, numeric-volume relaxation, missing/zero filling, quote conversion, fallback or acceptance change. Prior WP47 approval is consumed; its exact volume subtype/credits remain unknown.

## Phase A actually executed

Read contributor/LF governance, current Prepare/Acquire/identity and diagnostic implementation, merged WP49-WP53 findings. Confirmed main/#440 SHA and clean isolated branch. Source diff against WP53 preparation baseline `f7481558d8a2da69463a56a2392deef120e76f46` is empty for Worker, Twelve Data adapter and workflow. This supports expected compatibility, **not fresh validation of private prepared artifacts**.

Executed cached-only tests:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp54-empty-feed'
New-Item -ItemType Directory -Force -Path $feed | Out-Null
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
& '<existing-venv-python.exe>' -B -m unittest test_research_chart test_research_snapshot_client -q
```

**262 run:261 passed,1 skipped**, all commands exit0:163 C# pass,38 workflow run/37 pass/1 host symlink skip,61 chart/client pass. Existing owner-checkout venv used; no installation. Restore used empty local feed/global cache with NuGetAudit=false, no downloads. Existing CA1711/SYSLIB1045/CA1861/CA2219 and bare-mode Streamlit ScriptRunContext warnings remain. WP53's earlier Linux38/38 success is historical evidence, not rerun in WP54; no Docker command executed here.

No operational inspection of WP53 preparation, ACL/TLS/destination, private pointer, provider credential/presence, Docker/resources or historical payloads. No actual Acquire/Import/Start/Verify/browser or provider/DNS/HEAD/account/credit/support/Azure/registry/deployment operation. No source/test change demonstrated necessary.

## Exact proposed approval - not yet received

Approval would cover **both** the following read-only operational preflight and, only on success, **one** live Acquire invocation. If the owner wants preflight only, no key read or send may follow. Previous general session permissions and PR merges do not authorize this phase.

### A. Immediate retained-preparation preflight

Use only the fresh WP53-owned preparation, located using its existing private local location pointer in the owner's temporary area. Reading that nonsecret pointer would be part of this approval; its path/value is not published here. Do not search other private preparations, read historical responses or reuse WP47 resources.

In the same owner context that created the protected directory:

1. Read only its nonsecret identity manifest, actual publish/context publication trees and local.json metadata. Run the existing `validate_preparation_identity` from this reviewed workflow; compare the complete40-file mapping and nonsecret identities recorded below. Validate the exact `publish/AIQuantTradingResearch.Worker.dll` location used by worker(), matching loaded/current workflow and copied context. No manual manifest rewrite/upgrade.
2. Check current owner ACL and path/reparse/link ownership, without permission changes. The restricted execution context may legitimately lack access; use only the already authorized owner context, not a weakened ACL.
3. Inspect only the newly prepared **public** CA certificate to validate current UTC validity and expected wp07-research identity. Do not read service.key, server.pfx, initialize.json or other private contents. Do not refresh/regenerate TLS. Stop on expiry, uncertainty or mismatch.
4. Check absence of bundle, import receipt, private database/state/destination conflicts using filesystem existence only. Read no bundle/receipt/history. Confirm installed dotnet prerequisite and selected executable location. No Worker acquisition or lower-level provider command used as a preflight probe.
5. Stop on any failure or stale identity. Preserve existing resources; request separate specific preparation/resource authority if a replacement is needed. Do not run Prepare, Docker inspect/run/build, pull, restart, rebuild, edit config, change ACL or delete anything under this approval.

Recorded WP53 comparison values (not freshly checked in Phase A):

- workflow SHA-256: `9a63181918d35653e03e6c91d58d322f09de5de170580ac46eab35ba70450e8f`.
- Worker.dll SHA-256: `5e67728a906d8f85b7b074fc1c0e45a6682401f38b8658ece2e65a76825bba14`.
- complete publication mapping digest: `9f486cb9ca575e9856f8f40fa4f04477d84c6938d5c7da3437a9f0b056b6217e` (SHA-256 of canonical sorted compact JSON mapping).

Existing Prepare entry is `python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory <new-owned-temporary-directory>`. It is identified for clarity only: **not proposed for execution in Phase B**. It entails Docker/resource creation and fresh service/TLS material and would need separate authority if retained WP53 preparation fails. No further Docker authority is needed for the proposed host-only Acquire using the retained preparation.

### B. One conditional live acquisition invocation

Only after every immediate preflight succeeds:

1. Access only the established Windows User environment setting `TwelveData__ApiKey`, after identity validation, in owner context. No earlier presence query. Missing/invalid channel stops with only a fixed sanitized category; never print its value, length, fingerprint or existence detail beyond the minimum failure category.
2. Transfer in memory to the existing transient process `AIQ_WP39_PROVIDER_KEY` channel. Preserve/restore any prior process value and clear local references in finally; do not change Windows User/Machine settings, generate a second key, store a key file, use clipboard, CLI secret arguments, transcript changes, or policy bypass. Use a nonsecret allowlisted child tool environment plus this one transient channel, not unrelated credential environments. Memory strings/environment are transient plaintext, not secure erasure; no false wipe guarantee.
3. Invoke **once**, through the existing Python operator entry (not direct dotnet Worker):

```text
python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <verified-WP53-preparation> --execute-provider-approved --base-asset-volume-confirmed
```

The guard runs before channel access and again before child dispatch. Python removes the provider process channel from Worker environment and sends the bounded JSON configuration via stdin; C# uses Authorization header, not URL/arguments. Existing5-second HTTP deadline,1MiB streamed response bound,20-second Worker operation bound and40-second parent bound remain unchanged.

**One attempt means one operator invocation**, with at most two sequential sends: BTC/USD first; ETH/USD only after BTC passes. Exactly fixed time_series request: interval1min, outputsize1000, timezoneUTC, orderASC; <=24h,15-second grace. No separate discovery/HEAD/DNS/capability request, pagination, retry, second invocation, symbol substitution or fallback. Normal name resolution intrinsic to the approved GET is not an additional probe. Stop immediately at first failure. There is no assertion of guaranteed volume availability or plan capability; prior base-unit confirmation is owner-reported, not newly proved by fake tests.

4. Capture only strict reconstructed version1 diagnostic fields or the fixed `ACQUIRED_NOT_IMPORTED` success status. Never forward raw stderr/stdout, provider body, invalid volume, header, exception or private path. Unknown/untrusted output becomes a fixed sanitized failure, not an excerpt. Permitted ten fields: version,event,failedStage,instrument,httpStatus,requestsStarted,responsesReceived,outcomeUnknown,failureCode,bundlePublished. Record actual exit0/1 and authority consumed status, not invented counts.
5. On failure, report the six-code subtype when actually emitted, exact counters/status/stage, unknown-outcome and bundle flag. No additional request for diagnosis or credit check. Send count is not proof of charge; credit consumption remains unknown.
6. On fixed success and atomic bundle publication, label **acquired, not imported** only. Do not run Verify/Import/Start/browser, open raw bundle, inspect rows or claim the chart works. Current success output does not provide full send/response counts; do not fabricate them. Preserve the newly acquired private bundle for separately approved verification/import.

### Retention and cleanup

Retain the WP53 preparation/images and any permitted newly generated bundle; no old assets are touched. On failure preserve only existing private operational artifacts and a sanitized status; do not create/store raw provider captures. Restore transient key/approval channels and local references in finally. No Docker/volume/container/image cleanup because none are created by this proposed invocation. No private paths or credentials committed to evidence.

## Current disposition and next owner decision

`LIVE_ACQUISITION_NOT_AUTHORIZED / NOT_RUN`. Approval received for Phase A source/tests/Git lifecycle only. Immediate operational preflight NOT RUN; provider credential access/presence NOT RUN; live invocation count0. Approval obtained=false, consumed=false. No Phase B authority is inferred from this proposal or PR.

Owner decision requested after reviewing this concrete proposal: authorize the retained-WP53 read-only operational preflight above and **exactly one conditional Twelve Data acquisition**, including the exact transient key source/channel, or keep Phase B pending. This explicit stop comes from the WP54 prompt's Authority section and Phase A step3, not from a new licensing/governance gate.

Only repository paths changed: this `docs/roadmap/release-1.15/prompters/release-1.15-wp54-controlled-acquisition.md` and append-only `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. UTF-8/LF/no BOM and git diff --check verified before commit/PR. Leave PR OPEN/UNMERGED. No code acceptance change, merge or deployment.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

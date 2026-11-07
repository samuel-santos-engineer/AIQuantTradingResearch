# WP43 - merged parser offline preparation

Status: **OFFLINE_PREPARATION_COMPLETE_NO_LIVE_AUTHORITY**. Date: 2026-11-07.

## Baseline and archive disposition

PR #429 final head `d08e626997cf2a1f4592ada6ff53810b6bf28515` was squash-merged normally, without protection bypass or branch deletion, as `c4a622839c26e930140ce0b826b7269cd59d80b9`. GitHub showed CLEAN/MERGEABLE; protected main had no required status-check contexts and no checks were reported. Its final inventory was 68 files, including previously reviewed WP41/WP42 work. Existing 185-test offline evidence remained applicable to unchanged code during the archival commit.

Actual owner checkout inventory: 65 untracked Markdown candidates, case-insensitive suffix and nested paths included. 58 new archive documents were added, seven already had equivalent tracked contents (two differed only in trailing whitespace); none excluded/blocked. One extra per-file inventory document records every exact path, original byte size, purpose, sensitivity review and disposition: [archive inventory](release-1.15-pr429-owner-markdown-inventory.md). No owner original was modified/deleted/staged, no ignored file force-added, and modified manifest/non-Markdown scripts preserved. Only copies normalized to UTF-8/LF/no BOM with trailing whitespace removal. Relative Markdown links resolved. Historical prompts are archive records, not active permission.

New isolated branch starts clean from the confirmed merge. Current governance and merged WP41/WP42 runbooks/producer/consumer were reviewed. No architecture, endpoint, limits, importer, provenance, API, chart UX or no-retry changes. No credential read or historical private-artifact inspection.

## Reproducible offline preparation

Use existing tools and cached inputs only. PowerShell commands are 5.1 compatible. Run from the isolated source checkout. `$prepared` is a NEW owned temporary directory; never overwrite older attempts.

```powershell
$prepared = Join-Path $env:TEMP 'aiq-wp43-prepared-20261107'
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory $prepared
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source (Join-Path $prepared 'empty-feed') -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Invoke existing project virtual-environment Python; no dependency installation:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

Actual chart/client command used the existing project virtual-environment executable. All commands exited 0. Prepare verified cached pinned bases and dependency image, used empty-feed cache-only restore, published current C# source, and built two local images with --pull=false and --network=none. No old prepared binary/image was reused as proof. No PowerShell runtime/policy bypass, network restore, image pull or publication.

## Actual build and resource evidence

- AIQuantTradingResearch.Worker: `0e0f56a57007fedc1140c4d32af154a43d712035c8362fe3e9f382660b3255fb`.
- AIQuantTradingResearch.Infrastructure: `ab4e92b7b383c6f3a5f8ed8a035645bb1e16ed8cc66dc3f8760fac6d4c8a5e4d`.
- research local image: `sha256:1ddc2479b3acd6abe697d5728369a4f72c3f659ebb02fb9b13c7bd2b52a77f66`.
- ui local image: `sha256:c9ada5487d001ba7455c3387fae4c158df6718d2b2ec144bed12a0a2711fc0cd`.

Both assembly hashes match the freshly built Release outputs; owned image tags match the new private manifest. Toolchain: .NET SDK 10.0.103; Python 3.13.15; Docker 29.7.2 Linux/x86_64. New private directory ACL inheritance disabled; owner/SYSTEM protection established by unchanged Prepare. No private absolute paths, runtime config, PFX or key material published.

Public certificate: SAN wp07-research, valid **2026-11-07 20:48:18 UTC through 2026-11-08 20:49:18 UTC**, verified at 2026-11-07 20:50:10 UTC. If expired before any later separately approved operation, prepare a new owned directory and revalidate actual identities/lifetime; do not extend/overwrite old artifacts.

Zero session containers and zero owned state volumes. Manifest present; bundle, import receipt, private SQLite and qualification absent. Prepared local images/runtime retained privately for owner review; no real import/start/browser occurred.

The newly published Worker ran through the actual Python worker/strict child parser, with approval marker and provider process channel removed from child environment and empty stdin. This bounded negative probe returned:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"MISSING_APPROVAL","bundlePublished":false}
```

This was not an Acquire action or provider probe. No real key was read; no credential-setting presence check was necessary here.

## Fake regression evidence

| Suite | Result |
| --- | --- |
| C# acquisition/adapter/initializer | 101 passed, 0 failed/skipped, exit 0 |
| Python workflow/diagnostic | 23 passed, exit 0 |
| Existing chart/client | 61 passed, exit 0 |
| Total | 185 passed |

Actual C# Worker fake-handler envelopes round-trip through actual Python validation for every WP42 new code. Version-1 producer/consumer compatibility and strict malformed/oversized/forged/duplicate/type validation, canary non-disclosure, exact HTTP/transport/cancellation/body-read counters, second-symbol failure/no partial bundle and synthetic-only provenance are covered. Tests use only synthetic fixtures and owned local children, never authentic observations. Existing CA1711, SYSLIB1045, CA1861, CA2219 analyzer warnings and Streamlit bare-mode notices remain. No new failure or skipped runtime test. No broad requalification/browser run claimed.

## Historical facts, stop conditions and review

Preserve WP41's observed result: BTC/USD HTTP 200, response_parse / INVALID_PROVIDER_PAYLOAD, one send/one response, ETH/USD not requested, no bundle/import. Exact old payload defect and charged credits remain unknown. WP42's eleven new codes are prospective; these tests do not explain the historical response.

Stop on changed source/build/manifest identity, ownership conflict, existing destination artifacts, expired TLS, unavailable cached input, failed offline test or malformed/unknown diagnostic. Do not fetch provider data to investigate. No real Acquire, DNS/HEAD/HTTP/capability probe, retries, credential reads, Azure operations or registry publication are authorized. No new live approval is requested by this PR.

No runtime session started; nothing requires stopping. If later cleanup is authorized, match owned image tags/IDs to the private manifest; remove only confirmed owned resources, never prune or delete older evidence. If a later separately approved session starts, use the existing Stop action and preserve state/volume. Owner branches and original untracked files remain untouched.

This PR changes exactly this runbook and an execution-plan append. UTF-8/LF/no BOM, independent CRLF check and git diff --check required and passed. Leave new WP43 PR OPEN/UNMERGED for owner review. No application code changed.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

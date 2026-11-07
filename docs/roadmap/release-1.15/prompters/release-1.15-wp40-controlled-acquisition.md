# WP40 - controlled acquisition preparation and review

Status: **AWAITING_NEW_OWNER_PROVIDER_APPROVAL**. Date: 2026-11-07.

## Baseline and boundaries

Clean isolated branch from synchronized main `c69832585a44c6253063b60d26d48b8ae487db26`, merged PR #427. The owner's original dirty checkout was preserved. This PR changes documentation only; it qualifies the existing merged implementation using fresh offline outputs. All authored text is UTF-8/LF without BOM.

The previous approved attempt failed without a bundle and consumed its authorization. Its request count, HTTP status and credits remain unknown. The earlier WP39 runbook's instruction to leave #427 open is historical; #427 is now merged. No new provider authorization is implied by this preparation or any CLI flag.

## Actual offline evidence

| Check | Observed result |
| --- | --- |
| Fresh Prepare | Exit 0, PREPARED_NOT_IMPORTED; cached-only restore/publication and two local image builds |
| Toolchain | .NET SDK 10.0.103; Python 3.13.15; local Docker desktop-linux, 29.7.2, linux/x86_64 |
| Cached inputs | Existing pinned ASP.NET/Python bases and dependency image verified by unchanged preparer; builds use no pulls and network=none |
| Private ownership | New temporary preparation directory, inheritance removed, owner and SYSTEM full-control ACL entries; no old attempt reused |
| Provider channel | Exact Windows User registry value NAME present; value never read. Presence does not establish validity or account access |
| Fresh diagnostic wiring | Newly published C# Worker through actual Python worker/strict parser: MISSING_APPROVAL, zero sends/responses |
| Worker assembly SHA-256 | `2e70f7bd7145eb1331e46d4baa2721e7a665feae9b834d50f6498b176aea224e`; matches fresh Release build |
| Infrastructure assembly SHA-256 | `06f1949f8060aba72e7e24e3d31d79ebd7862950b8d48e9b6cb0dc603f6ce652`; matches fresh Release build |
| Research image | `sha256:5abc8ff77dabf952ac9b89dbca3650c0d49bc13627530a4b45de28a9045f6c02`; owned tag matches private manifest |
| UI image | `sha256:96538411945c6f0a145d5fef37b51d2098676b7bb9d4b69d0682428579a88114`; owned tag matches private manifest |
| Public TLS certificate | SAN wp07-research; valid 2026-11-07 19:11:34 UTC through 2026-11-08 19:12:34 UTC; private PFX not inspected |
| Artifact presence | local manifest present; bundle, import receipt, private SQLite and qualification absent |
| Owned resources | Zero session containers and zero owned state volumes; prepared images retained for later owner decision |
| Focused C# suites | 84 passed, 0 failed/skipped, exit 0 |
| Python workflow/diagnostic suite | 22 passed, exit 0 |
| Python chart/client regressions | 61 passed, exit 0, installed project virtual environment |
| Real provider / Azure / registry publication | NOT RUN; zero requests/operations in WP40 |
| Import/start/verify/browser | NOT RUN; no authentic bundle exists. Additional fake Docker/browser end-to-end smoke not run |

Fresh binary gate probe (approval marker and provider process channel removed from the child environment; empty stdin):

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"MISSING_APPROVAL","bundlePublished":false}
```

This is an offline negative probe, not an Acquire invocation or provider failure. It establishes newly compiled C# output reaches the actual Python parser. Existing tests additionally exercise bounded 2048-byte diagnostics, 4096-byte stdin, duplicate/extra/malformed fields, unknown outcomes, two-symbol counters, fake HTTP/transport/cancellation, no-overwrite/publication failure, secret canaries and synthetic-only handler provenance. Static/fake tests do not establish provider availability or authentic acceptance.

## Commands executed

Commands below are Windows PowerShell 5.1 compatible, from the isolated WP40 worktree. The private preparation is represented by `$prepared`; its absolute owner path is deliberately omitted from publication.

```powershell
$prepared = Join-Path $env:TEMP 'aiq-wp40-prepared-20261107'
python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory $prepared
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source (Join-Path $prepared 'empty-feed') -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
# Invoke the existing project virtual-environment Python, without installing packages:
python -B -m unittest test_research_chart test_research_snapshot_client -q
git diff --check
```

The first no-restore C# attempt failed NETSDK1004 because the new test worktree lacked assets. Empty-feed/cache-only restore succeeded; the subsequent test command passed all 84 tests. Existing SyntheticSnapshotQualificationTests analyzer warnings (CA1711, SYSLIB1045, CA1861, CA2219) and Streamlit bare-mode ScriptRunContext notices remain. No PowerShell script execution/policy bypass was needed; the existing Python implementation was exercised directly. The chart/client command actually used the existing project virtual-environment interpreter.

## Exact proposed new attempt - NOT AUTHORIZED

Request separate owner approval for **one Acquire invocation**, at most two sequential HTTPS GET requests to `https://api.twelvedata.com/time_series`: BTC/USD followed by ETH/USD, each `interval=1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`. Accept only the existing bounded latest-24-hour observations with 15-second completed-candle grace. No retries, pagination, fallback, DNS/HEAD/account/capability probes or additional endpoints. Actual credits and response availability are **unknown**; the existing Plan's expectedCredits value is an estimate, not observed quota evidence. Base-asset volume units are owner-confirmed BTC/ETH, not independently measured here.

Before a later approved invocation, recheck source/build identity, owned resources, absent destination bundle and unexpired TLS. Expired TLS or changed inputs require fresh offline preparation and review of its identity. Do not overwrite this preparation or reuse the historical failed attempt.

Only after explicit new approval: read the named Windows User provider setting inside the approved operation, map it temporarily into process-only AIQ_WP39_PROVIDER_KEY, call the existing Python Acquire once with --execute-provider-approved and --base-asset-volume-confirmed, and restore the previous process channel in finally. Never put the value into arguments, files, Docker, browser, logs or output. A CLI flag cannot grant authority. No acquisition command is executed by this document.

On failure, stop immediately and retain private evidence. Report only child exit, artifact presence and allowlisted failedStage, instrument, httpStatus, requestsStarted, responsesReceived, outcomeUnknown, failureCode and bundlePublished. Unknown parent counts/publication remain null; do not infer credits or provider receipt. No retry or import after a failed/unknown attempt.

Only successful observed acquisition permits later existing Import -> Start -> Verify and local browser checks. Then verify BTC/ETH UTC/OHLCV/volume/counts/gaps, TLS snapshot identity, switching, retained presets, zoom/refresh and observed-provider disclosure; never substitute fixtures. No authentic qualification is claimed now.

## Safe stop and retained outputs

No chart processes were started, so none require termination. Retain private runtime/TLS and local images pending review. If cleanup is later needed, match image tags and IDs against the private manifest before removing only those tags; never prune. If a later owned session is started, use existing Python --action Stop --directory $prepared, which preserves private state/volume; never stop unrelated processes. Do not publish private artifacts or inspect historical attempts. All preparation outputs remain outside Git.

## Review inventory and readiness

Changed paths: this new runbook and `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`. No source, tests, contracts, query, UI, fixture mode or dependencies changed. The offline implementation is qualified; authentic local and public qualification remain absent. Azure remains deferred/unresolved.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

Stop at **AWAITING_NEW_OWNER_PROVIDER_APPROVAL**. Leave this new PR OPEN/UNMERGED.

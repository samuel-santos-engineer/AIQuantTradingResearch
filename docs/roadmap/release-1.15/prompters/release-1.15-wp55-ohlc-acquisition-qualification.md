# Release 1.15 WP55 - OHLC-only acquisition qualification

Date: 2026-11-08. Phase A executed from exact main `61a1f804526b145e8a3d47f1daf4795f3fbec375`, the verified PR #442 squash merge. Isolated branch `feat/release-1.15-wp55-ohlc-qualification`; initially clean. Owner checkout and all retained preparations/resources preserved. Twelve Data remains the sole authoritative upstream; instruments BTC/USD and ETH/USD. No new provider or licensing gate.

## Executed offline qualification

Reviewed the current operator workflow, WP52 identity implementation, WP53 preparation evidence, PR #442 runbook and contract amendment. No production fix was necessary. Volume remains a required field with nullable values in explicit OHLC-only mode; strict OHLCV/numeric zero behavior is unchanged. The merged focused tests cover both instruments, missing/null/numeric/zero volume through adapter, atomic bundle, import/reopened SQLite, transformed HTTPS snapshot, strict Python client and chart; malformed price/time/symbol and stale preparation failures remain fail closed. All fixture data is synthetic offline evidence, not actual provider acquisition or imported history.

| Executed check | Run | Pass | Fail | Skip | Exit |
| --- | ---: | ---: | ---: | ---: | ---: |
| Full Application | 290 | 290 | 0 | 0 | 0 |
| Exact affected Infrastructure classes | 235 | 235 | 0 | 0 | 0 |
| Python workflow | 39 | 38 | 0 | 1 | 0 |
| Chart/client | 63 | 63 | 0 | 0 | 0 |
| Total | 627 | 626 | 0 | 1 | 0 |

Windows owner context was used for synthetic local certificate tests; .NET SDK 10.0.103, .NET/ASP.NET runtime 10.0.3 and Python 3.13.15. Existing installed owner venv supplies Streamlit/Plotly chart dependencies. Cache-only restores for both test projects used `--source <empty-local-feed> -p:NuGetAudit=false --verbosity quiet`, exits 0; no package download/install. No Docker command was used. Existing analyzer and bare-mode Streamlit warnings remain. Windows unprivileged symlink test honestly skipped; prior Linux proof is recorded in WP53 and was not rerun here.

```text
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore -c Release --logger "trx;LogFileName=application.trx" --results-directory <owned-test-output> --verbosity quiet
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests|FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchSnapshotEndpointTests" --logger "trx;LogFileName=infrastructure.trx" --results-directory <owned-test-output> --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
```

Chart/client uses `PYTHONPATH=python/presentation`. These are focused Infrastructure tests, not a full-suite claim. PR #442's exact-base comparison remains the applicable broad evidence: final PR 634 passed/29 failed of663, baseline 621 passed/29 failed of650; all29 failure identifiers match. Twelve missing Debug Worker prerequisites, sixteen missing checkout-local interpreter prerequisites and one explicitly unset E4 oracle selector remain separate open items. No oracle permission inferred or unrelated harness repair attempted. See [matched failure matrix](release-1.15-twelvedata-crypto-ohlc-only.md#pr-442-verification-gate-closure---2026-11-08).

## Supported operational flow reviewed, not executed

- `Plan` is offline but was reviewed in source rather than invoked. Its existing fixed parameters specify `time_series`, UTC/ASC one-minute bars, outputsize1000, latest completed minutes within24 hours and15-second completion grace. Credit charges/account capabilities remain unverified; no account/credit lookup permitted.
- `Prepare` requires a new directory beneath the local temporary root, cached dependency image `aiq-c2b36:candidate` at `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`, pinned ASP.NET digest `sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f` and pinned Python digest `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`. Their current presence has NOT been inspected. It applies private owner ACLs, cache-only publishes Worker, generates normal fresh local service/TLS configuration privately, copies the complete publish tree and UI context, and builds two uniquely owned images with `--pull=false --network=none`. It records a workflow/full-publication identity. Old WP53 identity is stale and must not be reused or edited.
- `Acquire` requires separate NEW live authority. Intended command after fresh preparation: `python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <fresh-matching-preparation> --execute-provider-approved --ohlc-only`. Existing `AIQ_WP39_PROVIDER_KEY` process channel goes through bounded stdin and Authorization header, never secret CLI arguments. Guard validates full publication/context and workflow before channel access and again before dispatch. At most two sequential requests: BTC first, ETH only after BTC succeeds; first failure stops, no retry/pagination; publication is atomic after both validate. There is no additional diagnostic request in that approval. Destination, fresh identity, ownership/ACL and public certificate validity must be verified under their applicable permission before acquisition. No credential-presence inspection occurred.
- `Import`, `Start`, `Verify` are NOT automatically allowed by preparation or acquisition. Import requires observed-provider bundle and no owned running chart, initializes private SQLite and writes a receipt. Start creates/seeds the owned Docker volume and starts authenticated dual-process local chart; Verify consumes both snapshots and checks their bounds/generation. These require separate owner authority; no TLS private material/service key/historical bundle will be inspected in Phase A/B. Actual browser qualification is separate. Local and public readiness are distinct.

## Approval boundary and current disposition

| State | Evidence |
| --- | --- |
| OFFLINE_QUALIFIED | Executed627 tests:626 pass,1 skip, all exits0. |
| Docker/local preparation approval | NOT_GRANTED for WP55 at this evidence revision. |
| PREPARED | NOT_RUN. |
| Live acquisition approval | NOT_GRANTED for WP55. |
| ACQUIRED | NOT_RUN. |
| IMPORTED | NOT_RUN. |
| LOCAL_CHART_VERIFIED | NOT_RUN; authentic candles are not claimed visible. |
| PUBLIC_RUNTIME_VERIFIED | False. |

Next exact approval requested: Docker context/engine and the three cached-input identity inspections; **one** existing Prepare invocation in a new owned temporary directory, including its two cached-input network-disabled builds, local normal configuration generation, owner ACL and public-certificate/manifest/full publish-context checks. No pulls, registry/Azure/provider request, credential inspection, application/test container startup, existing-resource modification/deletion or actual Acquire/Import/Start/Verify/browser. Stop on any missing cached input/mismatch/denial/unexpected behavior; retain newly owned outputs for owner review. Only after preparation succeeds will a separate single-attempt acquisition request be presented with concrete evidence. This document does not grant either approval.

No operational commands, provider/DNS/HEAD/catalog/account/credit calls, credential/presence inspection, private historical response access, real preparation/acquisition/import/start/verify/browser, Docker/registry/Azure operation, deployment or retained-resource deletion occurred. Ordinary offline suites use disposable synthetic fixtures and their owned local subprocesses. Repository changes are this evidence note and an append-only execution-plan entry; no production code/dependency/schema/UI changes. OPEN/UNMERGED PR for owner review. Text UTF-8/LF/no BOM and `git diff --check` verified before commit.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

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

## Phase B - separately approved and executed

Owner explicitly approved the bounded Phase B request: Docker context/engine and three exact cached inputs; one fresh Prepare, two pull-disabled/network-disabled image builds, normal private configuration generation, owner ACL/public-certificate/full identity checks. No acquisition or credential inspection included. The Phase A table above is historical; current PREPARED status is PASS as follows.

- `docker context show`: desktop-linux; `docker info --format '{{.OSType}} {{.ServerVersion}}'`: linux29.7.2. Each command exit0.
- `docker image inspect <exact-input> --format '{{.Id}}'` for each of the three inputs listed above: all exact expected identities matched, exit0. No pulls/registry access.
- Executed the existing command **exactly once**: `python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory <new-owned-temporary-directory>`. New directory selected privately, not published. A wrapper passed only allowlisted Windows tool/cache environment names and DOTNET_CLI_TELEMETRY_OPTOUT=1, no provider/service channel queries/copies. Timeout240 seconds. Exit0, exact fixed stdout `PREPARED_NOT_IMPORTED; no provider calls or private state created`, empty stderr. No retry.
- Prepare ran its cache-only restore/Worker publish, generated its normal local service/TLS configuration, copied publish/UI context, and built two unique owned images with `--pull=false --network=none`. No application container or volume created/started. Fresh preparation and both images retained for the owner. Existing resources and WP53 preparation untouched.
- Owner ACL: root inheritance protected, exactly owner and SYSTEM FullControl. Eight newly generated output paths checked for owner and allowed principals using ACL metadata only; configuration/key/PFX contents not read. These checks do not claim protection against a malicious administrator/owner.
- Real `validate_preparation_identity` passed in the owner context, recomputing the entire publish and context/research trees, workflow loaded/on-disk bytes and version1 manifest. 40 publication files match, not just the entry DLL. No manifest repair/bypass.
- Public `runtime/tls/ca.pem` decoded only: expected DNS wp07-research, currently valid, notAfter2026-11-09T18:53:46Z. No TLS private material inspected. Public certificate/configured directory consistency checked. Recheck freshness immediately before any later attempt; this is local test-server TLS, not an upstream connection/probe.
- Fresh bundle/private database/import receipt/qualification file absent. Generated configuration existence checked without content. No provider credential presence/value inspection, acquisition, imported prices, or public readiness inference.

| Nonsecret preparation identity | Verified value |
| --- | --- |
| Workflow SHA-256 | 62471759b4a8d858616cdf19a92c6391b0f1211a8c2a0e2b4f6570a69f1a6c25 |
| Worker DLL SHA-256 | 3b5dac87c3966931ad909c7f165a7cec65fa7b89a2a4f4604b57b262a0024a48 |
| Full publication mapping SHA-256 | 3d7ac2251abfa9aa5b9ed2019b2cf5ae15c76cf7bd9cd8b56587b5215276b221 |
| Research image | sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 |
| UI image | sha256:bb093dcd2b866044adb5002a8916909f0cc5aa896778e030dc89c7a25bb748c1 |

Mapping digest is SHA-256 over UTF-8 json.dumps(publication,sort_keys=True,separators=(',',':')). No secret fingerprint or private location included.

Current state: **PREPARED=True; ACQUIRED=False; IMPORTED=False; LOCAL_CHART_VERIFIED=False; PUBLIC_RUNTIME_VERIFIED=False**. Offline tests remain627 run626 passed1 skip; no new unit-test count claimed for these operational checks. No production fix/source change needed.

Next authority required is a NEW single-attempt acquisition approval. Proposed existing command is `python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <this-verified-fresh-preparation> --execute-provider-approved --ohlc-only`, with the exact validated directory substituted privately. Approval must explicitly include immediate identity/owner ACL/public TLS validity/empty destination checks and reading the exact Windows User TwelveData__ApiKey into transient AIQ_WP39_PROVIDER_KEY, clearing that transient channel afterward without touching the retained Windows value. Maximum two sequential time_series requests: BTC/USD first, ETH/USD only after BTC success;1min/UTC/ASC/outputsize1000, completed minutes with15-second grace and24-hour age bound; no retry/pagination/exploratory calls. Provider availability/account capability/charges/actual returned coverage remain unknown. No volume required in explicit price-only mode, no invented units. Capture only the fixed envelope/publication outcome, no payload/price/key/private path. Stop at first failure/uncertainty; do not retry. Import, runtime, browser and any additional resource changes remain separate future authority.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.

## Phase C - separately approved single acquisition succeeded

Owner explicitly authorized immediate identity/owner ACL/public TLS/empty-destination checks and **one conditional Acquire** in explicit `--ohlc-only` mode. Authority included reading only Windows User TwelveData__ApiKey into an isolated transient AIQ_WP39_PROVIDER_KEY channel; no exploratory requests, retries, import, runtime, Docker changes, Azure or deployment. Prior diagnostic/other work-package permissions were not reused.

Immediate owner-context rechecks passed before credential access: protected owner/SYSTEM ACL and ownership of root/eight generated output paths; current complete preparation identity; public certificate validity/DNS; host .NET10 runtime; absent bundle/private database/import receipt/qualification and pending bundle destinations. No configuration/PFX private contents read. An exclusive nonsecret local invocation marker prevents accidentally repeating the consumed approval.

Executed the existing command once with the concrete matching directory substituted privately:

```text
python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <verified-fresh-WP55-preparation> --execute-provider-approved --ohlc-only
```

Wrapper used an allowlist of nonsecret tool/cache environment names plus the isolated transient provider channel; no secret arguments/clipboard/files/logs. The exact Windows User registry value was read only after checks passed, never enumerated, printed or fingerprinted. The channel reference was cleared in finally; retained Windows credential and parent operator environment unchanged. This removes the transient mapping/reference, not a claim of guaranteed zeroization of managed-process memory. Timeout55 seconds; existing Worker limits remain unchanged. No retry or second invocation occurred.

| Sanitized outcome | Observed value |
| --- | --- |
| Preflight | PASS |
| Acquire invocations | 1 |
| Acquire exit | 0 |
| Fixed workflow result | ACQUIRED_NOT_IMPORTED |
| Publication | Atomic two-symbol bundle present; successful workflow. |
| BTC/USD accepted rows | 1000 |
| ETH/USD accepted rows | 1000 |
| Gap count | 0 for both symbols. |
| Volume available | false for both symbols; OHLC-only mode retained. |
| Request/response count basis | Successful sequential two-symbol workflow implies2/2; no separate HTTP trace or extra probe. |
| Transient channel | Cleared in finally; no persisted secret. |

Acquisition observed at 2026-11-08T19:04:15.974819+00:00 (UTC). Successful fixed CLI stdout matched exactly, stderr empty. Only generated nonsecret provenance outcome metadata (provider/kind/status/symbol counts/gaps/volumeAvailable) and publication-file existence were inspected; candle response bodies, raw prices, observations rows, import data and secret fingerprints were not read or published. Provenance reports provider twelvedata, kind ObservedProvider and status AcquiredNotImported. Successful code path validates completed/aligned UTC one-minute OHLC and its existing24-hour/15-second/order/window limits before atomic publication. No claim of independent request tracing, account credits, actual price accuracy audit or browser visibility is made.

Current state: **PREPARED=True; ACQUIRED=True; IMPORTED=False; LOCAL_CHART_VERIFIED=False; PUBLIC_RUNTIME_VERIFIED=False**. Authentic acquired bundle and preparation/images retained privately, unchanged old preparations/resources. No Import/Start/Verify or browser operation performed; no new Docker command or Azure/registry/deployment operation in Phase C. Approval is consumed; any additional acquisition requires NEW authority.

Next concrete owner decision is separate import/local runtime verification authorization: use existing `--action Import`, then `--action Start --port <available-loopback-port>`, then `--action Verify`, all against this exact preparation. Import reads the newly acquired observed-provider bundle through its existing verification, initializes private SQLite and creates its receipt. Start uses its existing owned state-volume seed helper and dual research/UI containers, session-labelled networks, immutable prepared images and local TLS/service configuration; only UI127.0.0.1 port published. Verify performs authenticated local HTTPS consumption for both instruments and compares bounds/generation. A bounded real-browser smoke may then prove both charts, unavailable-volume disclosure and supplied price indicators without recording raw prices/keys. These actions, their new local resources/configuration access and any browser are **not executed or authorized by the acquisition approval**. They need separate explicit owner authority; no additional provider calls, image builds/pulls, Azure or deployment included.

`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED; truthful acquisition success does not imply imported history or public readiness.

# Release 1.14 C2B-2 — Preserved WP07 and public shell/API evidence

Date: 2026-10-15. Authority: owner-issued C2B-2 preservation and continuation packet.
Disposition: **PARTIAL — local shell and read-only unavailable API implemented; owner review required.**
`PUBLIC_RUNTIME_VERIFIED=False`. Public generation and Azure deployment remain **NO-GO**.
This record neither accepts unfinished WP07 nor qualifies a working synthetic chart.

## Baseline and preservation

- PR #332 is merged: <https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/332>.
- Verified remote `main`: `dcd4adcdc8fb7d64a45cea6fb94b75fdbd395233`.
- Parked original: `C:\projects\github\AIQuantTradingResearch`, regular clone, branch
  `draft/release-1.14-wp07-partial-public-disabled`, HEAD
  `67893f51931ee183f71050a83b81127e6e648b87`, 151 dirty/untracked status entries.
  The earlier 150-entry disposition includes 44 explicitly deferred paths. The later
  owner authority document accounts for the extra entry; it does not authorize merging those 44 paths.
- Active checkout: `%LOCALAPPDATA%\Temp\aiq-r114-main-332`, initially clean `main`;
  current development branch `feature/release-1.14-chart-mvp-c2b`.
  It is a separate clone; no shared Git-directory dependency or submodules.
- Private archival copy: `%LOCALAPPDATA%\Temp\aq38935b\w`, with owner-restricted ACLs.
  All 25,822 inventoried files were copied and SHA-256 verified, including ignored
  files and repository metadata. No cache/build exclusions or destructive stash/reset.
  This is a verified recovery snapshot, not WORM storage; retain the parked source too.
- Archive companions: `manifest.json`, `staged.patch`, `unstaged.patch`, `status.txt`,
  `refs.txt`, `deferred-inventory.csv`, and post-work `preservation-after.json`.
  Manifest SHA-256:
  `23E0C48210299C83D5222F40C21048E9538CEAEB3ADFACF3BFEFD9B21CF1E8D8`.
  The local inventory retains file-level deferred dispositions. No archive contents,
  credentials, private datasets or deferred runtime files belong in this PR.
- Final preservation comparison: 25,822 files, zero hash mismatches; original Git
  status, full refs, branch and HEAD equal their archived starting values. Original
  checkout remains untouched and parked. Git's ownership check required a command-only
  `safe.directory` value for read-only inspection; no global configuration was changed.

## Hosting and implementation boundary

Pinned Streamlit 1.61.1 exposes the supported `st.App` ASGI API, custom middleware,
and script-CLI hosting. See [Streamlit App documentation](https://docs.streamlit.io/1.61.0/develop/api-reference/server/st.app).
`public_preview_app.py` uses that API and installed Starlette middleware; no new
package, reverse proxy, public port, process role, database or paid resource is added.
The public entrypoint retains sanitized `env -i`, non-root startup, fixed port 8501
and private-path guards. Private startup remains unchanged. Telemetry, file watching,
client exception details and secrets-file discovery are disabled for public startup.
This does not prove a kernel filesystem/network boundary on Azure.

`PublicBoundary` handles exactly six read-only routes before the Streamlit SPA.
Other `/api` and `/admin` routes return sanitized 404; reserved WebSocket upgrades
are rejected. Streamlit `/`, static/WebSocket routes and `/_stcore/health` retain
framework handling. There is no administrative functionality or readiness setter.
The API imports only standard-library parsing, time, JSON and async utilities;
it has no provider, Worker, file/cache, credential, database or generator access.

The UI clears retained chart responses while the existing false gate is in force,
shows disabled selection controls and explicit unavailability, and adds accessible
architecture/governance text and an inline SVG. Twelve Data remains private research;
Vike remains inactive. Shared algorithms imply no shared publication rights.

## Frozen coverage and storage/scheduler decisions

The existing generator, seed 114, `aiq-synthetic-ohlcv-v1`, all qualification
fingerprints, 12 interval/range combinations, 250 calculation-only warmup candles
and deterministic algorithms are unchanged. Qualified reference coverage remains
`[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`; it is not current rolling coverage.
For API input validation, the maximum visible reference window is the final 90 days,
`[2026-07-03T00:00:00Z, 2026-10-01T00:00:00Z)`. No actual dataset is published.

No storage is needed or selected for fixed unavailable responses. Immutable snapshots
would simplify read-only publication but still require qualified provenance, atomic
replacement, permissions and bounded loading. Bounded in-memory storage loses state
on restart and requires measured memory/loading costs. SQLite adds transaction,
mount-locking, durability, access-control and recovery obligations. All remain
deferred for a future ready-data implementation; no journal/backend adoption occurs.
Daily generation, refresh and cold-start catch-up are deferred. Requests never
generate or retry generation; Azure F1 Always On is not assumed. Rolling generation
requires a separate contract/version and qualification decision.

## Versioned JSON contracts

Anonymous same-origin GET only. Every response is a JSON object with required keys
`apiVersion: "chart-preview-v1"`, `state: string`, `code: string`, `data: object|null`.
There are no arbitrary exception strings or request echoes. No extra keys are emitted
except the `contract` object on the dataset response. All responses have
`Content-Type: application/json; charset=utf-8`, `Cache-Control: no-store`,
`X-Content-Type-Options: nosniff`, and exact Content-Length; maximum encoded body 4,096 bytes.

| GET path | HTTP | State / code | Exact data shape |
| --- | --- | --- | --- |
| `/api/v1/health` | 200 | `alive` / `OK` | `{ "service": "public-preview" }` |
| `/api/v1/status` | 200 | `degraded` / `DATASET_NOT_READY` | `{ "web": "ready", "dataset": "unavailable", "indicators": "unavailable", "generationEnabled": false }` |
| `/api/v1/dataset` | 503 | `unavailable` / `DATASET_NOT_READY` | `null`, plus contract below |
| `/api/v1/candles` | 503 | `unavailable` / `DATASET_NOT_READY` | `null`; no candle rows |
| `/api/v1/indicators` | 503 | `unavailable` / `DATASET_NOT_READY` | `null`; no analytics rows |
| `/api/v1/diagnostics` | 200 | `degraded` / `DATASET_NOT_READY` | `{ "categories": ["DATASET_NOT_READY"], "retryAutomatically": false }` |

`web: ready` means the ASGI route is serving, not that charts or deployment are qualified.
The dataset `contract` has exact fields: `label` (the frozen synthetic label),
`identity` (instrument `btc-like-simulation`, source `independent-synthetic`, status
`simulated`, marketType `simulation`, priceUnit `illustrative-price-unit`, volumeUnit
`illustrative-activity-unit`), `generatorVersion`, `seed: 114`,
`qualifiedReferenceStartUtc`, `qualifiedReferenceEndExclusiveUtc`,
`maximumVisibleDays: 90`, `intervals: ["1h","4h","1d"]`,
`ranges: ["1D","7D","30D","90D"]`, `hiddenWarmupCandles: 250`,
`actualCoverage: null`, `datasetFingerprint: null`, `rollingRefreshEnabled: false`.
Reference metadata never asserts data availability, generated-at time or fresh coverage.
Ready candle/indicator response schemas require separate qualified implementation.

Example: `GET /api/v1/candles?interval=1d&from=2026-09-01T00:00:00Z&to=2026-10-01T00:00:00Z`
returns HTTP 503:

```json
{"apiVersion":"chart-preview-v1","state":"unavailable","code":"DATASET_NOT_READY","data":null}
```

## Query, error and workload bounds

Health/status/dataset/diagnostics accept no query fields. Candles/indicators accept
only optional `instrument`, `interval`, `from`, `to`, `limit`; indicators also accepts
`indicator`. Defaults: synthetic instrument, `1h`, `2026-09-01T00:00:00Z`, fixed
exclusive end, `2160`, and `sma20`. Indicators permitted: `sma20`, `ema20`, `rsi14`,
`macd12_26_9`, `bollinger20_2`; fixed approved parameters cannot be overridden.
Bounds use exact UTC `YYYY-MM-DDTHH:MM:SSZ`, aligned interval boundaries, start < end,
within the reference visible window, at most 90 days and 2,160 rows. Limit is integer
1..2160 and must accommodate the requested row count. No alternative source/context,
paths or arbitrary indicator parameters. Reject duplicates, unknown/blank fields,
malformed percent escapes/UTF-8 and more than eight query pairs.

| Condition | HTTP / stable code |
| --- | --- |
| Query/identity/selection error | 400 / `INVALID_QUERY`, `INVALID_INSTRUMENT`, `INVALID_INTERVAL`, `INVALID_INDICATOR`, `INVALID_RANGE`, `INVALID_LIMIT`, `ROW_LIMIT_EXCEEDED` |
| Body, duplicate/nonzero Content-Length, Transfer-Encoding, receive timeout/disconnect | 400 / `INVALID_REQUEST` |
| Non-GET / unknown reserved route | 405 / `METHOD_NOT_ALLOWED`; 404 / `NOT_FOUND` |
| Path + raw query > 4,096 UTF-8 bytes / headers > 16,384 bytes | 414 or 431 / `REQUEST_TOO_LARGE` |
| Read rate / independent health rate exhausted | 429 / `RATE_LIMITED`, Retry-After 1 |
| Read admission occupied / unexpected internal failure | 503 / `SERVICE_BUSY` or `SERVICE_UNAVAILABLE` |
| Missing qualified public data | 503 / `DATASET_NOT_READY` |

`DATASET_STALE` and `DATASET_INTEGRITY_FAILURE` are reserved readiness failures for
future qualified storage validation. This API opens no artifacts and therefore
cannot distinguish stale/corrupt files; both remain unavailable regardless of disk
contents. No stale/corrupt artifact detection is claimed by this milestone.

Per-server-process, bounded sliding windows admit at most two read requests/second
and 30/minute; health has an independent 10/second, 120/minute allowance. Read receive
deadline is 250 ms, one in-flight receive, no queue; clock rollback denies further
admission. Health does not wait for the read slot. Output is constant bounded work.
These are application-level limits after ASGI parsing, not transport ingress memory
limits, durable global admission, Azure quotas, or measured F1 headroom. Root/UI traffic
still uses Streamlit controls. Multi-instance/restart abuse protection, transport limits
and target resource qualification remain open and cannot activate generation.

## Verification

Run from the active checkout; `.venv` is an ignored copy of the existing pinned local
environment. No dependency files changed. Commands are Windows PowerShell 5.1 compatible.

```powershell
$env:PYTHONDONTWRITEBYTECODE='1'
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_public_preview*.py' -v
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_*.py' -v
dotnet build src/AIQuantTradingResearch.Worker -c Debug -p:NuGetAudit=false --nologo
dotnet test AIQuantTradingResearch.slnx -c Release -p:NuGetAudit=false --nologo
dotnet test tests/AIQuantTradingResearch.AdminDiagnostics.Tests -c Release -p:NuGetAudit=false --nologo
git diff --check
```

NuGetAudit is disabled only for these cached offline test commands; package/security
policy files and signing configuration are unchanged. Debug Worker is built for the
existing Python bridge fixtures; solution tests use the Release Worker too.

| Final check | Result |
| --- | --- |
| Focused public shell/API | 14 passed, exit 0 |
| Full Python presentation | 171 run: 160 passed, 11 platform skips, zero failures, exit 0 |
| .NET solution Release | 570 passed: Domain 11, Application 215, Architecture 27, Infrastructure 317; exit 0 |
| Separately packaged offline protocol | 50 passed, exit 0 |
| Debug Worker build | Zero warnings/errors, exit 0 |
| Diff whitespace | Pass |

Focused coverage: all 12 selections, UTC/alignment/row bounds, malformed and private
override requests, methods/body/headers/response bounds, rate/concurrency/timeouts,
sanitized unexpected errors, send failure without duplicate response, no file/process
access, reserved admin/WebSocket paths, unqualified UI cache clearing and all four pages.
Real localhost Streamlit HTTP checks demonstrate `/` HTML 200, `/_stcore/health` 200,
the six exact JSON route statuses above, `/admin` 404 and POST dataset 405. ASGI tests
cover 400/414/429/431/503 busy behavior. These are not Azure or browser-layout results.
Initial harness cleanup and timeout failures were corrected; the existing three-page
navigation expectation was updated to four pages without weakening other assertions.

## Changed paths and acceptance limits

Only these eight paths belong to this change:

1. `container/entrypoint.sh`
2. `python/presentation/public_preview_app.py` (new)
3. `python/presentation/public_preview_api.py` (new)
4. `python/presentation/realtime_financial_visualization.py`
5. `python/presentation/test_public_preview_api.py` (new)
6. `python/presentation/test_public_preview_shell.py` (new)
7. `python/presentation/test_market_research_ui.py`
8. This evidence document.

| Requirement | Disposition |
| --- | --- |
| C2B-API-01 | Local unavailable response/query contracts implemented; ready-data schemas pending |
| C2B-API-02 | Local liveness/readiness separation and Streamlit health HTTP pass |
| C2B-API-03 | Fixed reference, synthetic identity and 90-day validation pass; rolling data deferred |
| C2B-API-04 | Local public API has no provider/file/Worker access; target kernel isolation unqualified |
| C2B-API-05 | Local safe failure taxonomy pass; stale/corrupt storage classification deferred |
| C2B-API-06 | Partial local bounds; F1 headroom, durable abuse, storage/scheduler and transport qualification blocked |
| C2B-API-07 | False gate retained, held WP07 untouched, no deployment or public generation |

No Docker build/container test, active Azure operation, target browser smoke, RBAC change,
credential change or deployment was performed. Responsive SVG/text construction and
Streamlit AppTest are not visual/mobile browser qualification. Offline generator,
indicator, provenance and infrastructure regressions do not prove native descriptor
closure, kernel waits, crash-safe exclusivity or production isolation. Those WP07,
production-integration and owner-activation obligations remain open.

PR preparation is authorized; merging requires successful protections/checks and owner
review of these results. A merged shell would still not constitute C2B or WP07 acceptance.

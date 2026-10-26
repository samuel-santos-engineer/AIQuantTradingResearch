# Release 1.15 WP03/WP04 - Functional foundation preflight

Date: 2026-10-24. Documentation-only exact-path proposal. Issues #369/#370; owner review required.

## Baseline, authority and readiness

[Current authority](prompters/release-1.15-wp03-wp04-foundation-codex.md) approves WP05 planning, not merging #386/#387 or implementing source. [Complete eight-prompt inventory](RELEASE_1.15_WP03_WP04_PROMPT_PROVENANCE.md) preserves all owner originals and reports exact archive destinations, byte counts, hashes and collisions. This PR has seven documentation paths only. `PUBLIC_RUNTIME_VERIFIED=False`.

Verified main:4578f8645c9626f4daa7ec76923953e8892c93d6 (#385 merge). #386 remains OPEN/UNMERGED at1829b05cd627f53d556758d797d191af06807263; #387 remains OPEN/UNMERGED atc59634534fb65c85785971fcdd11a23729b5e0b8; both bases4578f8645c9626f4daa7ec76923953e8892c93d6. No merges authorized or performed. An isolated new worktree/branch uses main, not either pending PR. Main still contains fixture previous-key overlap; future source uses the owner's single active service-key decision, accepted standard synchronous HTTPS/native-DNS limitation and two independent non-root single-principal-process Linux/amd64 images. D1-B supersedes single-image/root-supervisor/loopback language, not D3/D4/D5 semantics.

Live GitHub issue reads confirm #368-#373 OPEN. Open PR inventory contains only #386/#387 before this deliverable. No WP03 committed historical-state backend or WP04 accepted calculated-state implementation found on main/open PRs. No WP03/WP04 test/acceptance evidence is invented. #387's approved14-path integration proposal remains downstream; do not copy its fixture503 into a production service or generate fake successful snapshots.

Read AGENTS.md, .gitattributes, repository LF governance, Docker-first/historical-interface/synthetic-scope amendments, Release1.15 execution plan/manifest, WP01 decisions/D3-D5/foundation and engineering contracts, WP02 evidence and WP03/WP04 prompts. Inspected current C# projects, SQLite factory/store/schema, Twelve Data client/normalizer/value/metadata, indicator/ATR classes, tests and dependencies. Reviewed #387's plan from its isolated committed checkout rather than assuming it is on main. Historical OPEN/proposal text is interpreted with dated owner acceptance/D1-B supersession; D2 implementation details remain proposed below.

Commands actually used: git status --porcelain -uall, branch/rev-parse, fetch origin main, worktree add, gh pr view386/387, gh issue view368-373, gh pr list --state open, rg --files/rg -n and UTF-8 reads; Python standard-library SHA-256/content comparisons for owner inventory. No application tests/builds/restores, fault injection, Docker, Azure CLI, live URLs, credentials/provider/registry/deployment operations. Product tests below are proposals only, not new results. Prior14+10 fixture methods are historical local evidence, not CI/WP03/WP04 qualification.

## Actual reuse boundaries

| Existing literal path | Observed component | Reuse decision |
| --- | --- | --- |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs` | Forces PRAGMA journal_mode=DELETE and existing schema bootstrap | Run-only historical component. Do not silently change all databases to WAL. New private research store has separately justified connection/schema path. |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationSchema.cs` | target/time/offset/price only | Not full OHLCV, versions, calculated state or correction lineage. Keep historical table untouched. |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs` | Observation persistence transactions | Reuse transaction/error patterns conceptually, not schema or unsupported recovery claims. |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataClient.cs` | Daily1day request, complete response string | Not bounded1m OHLCV adapter. Preserve daily callers; new adapter tested with injected offline HttpMessageHandler. |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataTimeSeriesNormalizer.cs` | yyyy-MM-dd daily close to PriceObservation | Not a minute OHLCV normalizer. Do not coerce/extend old semantics silently. |
| `src/AIQuantTradingResearch.Application/MarketData/TechnicalIndicatorCalculator.cs` | Fixed-point primitives plus historical hour-grid/provenance constraints | Mathematical reference only; do not call with forged interval/provenance or relabel old output aiq-r115-research-v1. |
| `src/AIQuantTradingResearch.Application/MarketData/WilderAverageTrueRangeCalculator.cs` | ATR recurrence on historical hour grid | Reference only; one-minute segment/checkpoint state implemented separately under frozen D3. |
| `src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj` | Application reference and existing Microsoft.Data.Sqlite dependency | Reuse existing project/package; no new runtime project/package proposed. |

Central Microsoft.Data.Sqlite10.0.11, .NET10 target, existing xUnit2.9.3/Microsoft.NET.Test.Sdk17.14.1 stay pinned. No dependency installation/upgrades, solution/project edit or paid storage/service. Standard System.Numerics/crypto/IO suffice for proposed types/arithmetic/checksums. Source pins are not installed runtime or Azure durability evidence.

## Minimal private contracts and explicitly proposed storage choices

WP03 defines immutable raw types, not public D4 DTOs. Proposed names/signatures for review (not implemented):

- `ResearchInstrument` exactly BTC/USD or ETH/USD; `CompletedResearchCandle` with UTC minute opening, strictly positive OHLC, high/low ordering, nonnegative base-asset volume and exact scaled integer values. Use D3 bounds/scale10^12; reject invalid precision instead of clamp. Closing time must be <=trusted observed UTC minus configured completion grace. Preserve actual received/corrected provenance privately; unknown volume units are a capability failure, never zero filling.
- `IResearchCandleSource.ReadCompletedAsync(ResearchPollRequest, CancellationToken)` returns bounded validated candidate batch, actual poll observation and quota outcome; no DB mutation. Max batch1000, one request active, no live call by default. Cancellation and response-byte/time bounds checked during acquisition. Quota configuration is explicit, not claimed measured from fixtures.
- `IResearchCommittedStateReader.ReadAsync(ResearchInstrument, CancellationToken)` returns one immutable validated committed view: persistent generation, per-instrument version, actual retained rows, segment/index/validity, aligned private calculated rows/classifications/events/intervals, commit/poll metadata, integrity/recovery flags, algorithm identity and transform-configuration revision. Reader has no write/provider/HTTP capability; never exposes a half transaction. In WP03-only mode calculated values remain explicitly unavailable/null; there is no Ready research claim or D4 endpoint.
- `IResearchCommitProjection.Prepare(candidate, priorCheckpoint, cancellation)` returns a complete private research projection/checkpoint for the same candidate revision, with no storage writes. WP03's initial implementation records truthful unavailable research. WP04 later supplies real D3 calculation BEFORE the single WP03 transaction. Version/predecessor compare rejects stale projections; no second independent indicator writer. `IResearchCalculatedStateReader` will expose the calculated portion of this same committed view, not a separately advancing in-memory cache.
- `IResearchStateWriter.CommitAsync(preparedCandidate, expectedGenerationAndVersion, cancellation)` is internal to ingestion/application ownership. It atomically persists candle changes, derived validity/research/events, segment/correction intent, checkpoint reference and incremented version; publishes immutable memory only after durable transaction success. Duplicate identical batch is idempotent; actual corrections increment version. Successful empty polls update poll metadata without inventing a candle commit/version. No raw state reader crosses to UI; WP05 runs inside research and builds whitelisted D4 only after D5 transformation.

**Storage proposal requires owner acceptance through scoped implementation review; it is not a frozen D2 decision.** Dedicated research DB avoids mutating historical tables and DELETE factory. Proposed minimal logical schema in new ResearchStateSchema.cs: metadata(single generation/schema/algorithm/config revision); instruments(instrument/version/last commit/poll/segment head); candles(instrument/opening, OHLCV integer-text, segment/index); calculated_rows(same key, thirteen nullable scaled outputs plus classification validity/score/trend/regime); events(generation/instrument/segment/time/type unique); commit_evidence(identity/previous checksum/current checksum/correction intent); checkpoints(instrument/revision/algorithm/lineage/checksum/versioned canonical private payload); active_transform_config(private keyEpoch/public transformEpoch/config checksum with research-only key locator). These are explicit required fields/groups, not new SQL created in this task. Final constraints/column mapping are reviewed in WP03 implementation; WP04 must reconcile against that actual accepted schema. No arbitrary JSON model-extension storage leaking into D4.

Candidate backend: SQLite WAL with synchronous=FULL, foreign keys enabled, verified returned journal settings, bounded busy timeout; separate research-only directory0700/files0600 including WAL/SHM/checkpoint/watermark/lock. Justification: concurrent coherent read transactions while the sole writer commits; preserve historical DELETE databases. WAL is conditional on filesystem suitability; unsupported locking/flush/WAL fails startup rather than silently falls back or moves to a different Azure path. D2 mount/backend choice and actual Azure persistence/rename/fsync semantics remain unproven; no existing mount is declared safe by this plan.

Writer exclusivity proposal: acquire one research-owned lifetime exclusive sidecar OS file lock before initialization/ingestion/recovery; every supported writer entry uses the same canonical DB/lock identity, holds ownership through shutdown, and bounded transaction admission never steals stale ownership based on wall time. SQLite transactions additionally serialize commits. Local lock success does NOT prove cross-container/app incarnation exclusion on an Azure mount. Final runtime remains disabled until overlap/lock semantics are qualified or a separately reviewed alternative is accepted. No distributed lease service/paid infrastructure assumed.

Recovery protocol: record deterministic checksum-chained commit intent and resulting state in SQLite transaction; after commit atomically replace outside-DB watermark (generation, instrument commit versions/checksums) and only report its actual protection result. Commit success with watermark failure means valid committed data plus Degraded protection/bounded retry, not an invented transaction failure. Startup validates integrity/schema/lineage/checkpoint/retention before reads or new writes. DB ahead: verify each unwitnessed commit chain and replay before advancing witness. Watermark ahead/conflicting/corrupt lineage: Recovery, stop ingestion/valid publication, never silently lower witness or generate a fresh lineage to hide it. New generation only for verified new dataset/rollback recovery. Coordinated rollback of both DB and watermark can escape detection: explicitly best-effort, not anti-rollback security.

Checkpoint proposal every5 successful instrument commits, correction evidence ordinarily capped20/instrument, both configurable hypotheses inherited from planning, not accepted constants. Protect unresolved recovery dependencies from pruning; if bounded evidence budget is exhausted, stop/recover rather than delete required proof or grow without bound. Store current recursive checkpoint with matching commit inside SQLite; if emitting external checkpoint copies, treat them as atomically replaced derived copies validated against committed identity, never a second authority. No checkpoint/copy/watermark cross-file atomicity claim. Hold a sufficiently early baseline plus committed subsequent replay evidence. If a retained correction predates provable reconstruction, invalidate affected results and start a truthful new segment; never reseed recursive state from the remaining1000 visible rows while claiming continuity.

D5 durable active tuple uses private32-byte key/private keyEpochUUID/public positive transformEpoch and transform algorithm; service API key is unrelated. WP03 persists/coherently reads tuple lineage; WP05 alone derives daily HMAC output. No real keys are retrieved/generated now. Config activation/uncertainty stops publication; exact restart/rollback/config witness tests remain required. Simplified service-key policy does not remove transformEpoch.

## Exact source/test mutation proposal

All paths below NEW at inspected baseline. No wildcard write grants. Each table is an owner-review proposal, not permission. Shared WP03 contracts/store must have one active owning PR; reconcile after every merge. Existing Program.cs/DI, Worker lifecycle, providers, old schema, public app, Dockerfiles and project/package files remain run-only until separately enumerated runtime integration authority.

| ID | Literal proposed WP03 path | Role / missing contract |
| --- | --- | --- |
| A01 | `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs` | Raw/committed/projection/checkpoint types and immutable reader/writer boundaries described above |
| A02 | `src/AIQuantTradingResearch.Application/MarketData/ResearchIngestionService.cs` | Bounded validation, fair scheduler/admission, completion policy, correction/gap staging and single transaction orchestration |
| A03 | `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` | Separate1m bounded transport/normalization with injected handler; no default credential/live activation |
| A04 | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateSchema.cs` | Explicit versioned research-only schema, no migration of historical observation DB |
| A05 | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` | Reader/writer/verified connection pragmas, compare-and-commit and retention atomics |
| A06 | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateRecovery.cs` | Checkpoint/commit/watermark validation, bounded evidence pruning and integrity state machine |
| A07 | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchWriterOwnership.cs` | Lifetime research DB lock/ownership and sidecar path/permission checks |
| A08 | `tests/AIQuantTradingResearch.Application.Tests/ResearchIngestionServiceTests.cs` | Offline scheduler/validation/correction/gap/commit-state tests |
| A09 | `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs` | Injected offline HTTP responses, minute OHLCV/unit/bounds/cancellation negatives |
| A10 | `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteResearchStateStoreTests.cs` | Actual temporary SQLite DB, coherent transaction/retention/idempotence/exclusive writer tests |
| A11 | `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStateRecoveryTests.cs` | Deterministic failure boundaries, witness/checkpoint/rollback/pruning negatives |

WP03 bounded implementation sequence: first one separate PR containing A01-A11 under explicit owner approval, incrementally implementing raw candle commit/reader then the SAME store's recovery/ownership/ingestion tests. The first substep is a functional offline vertical slice, not full WP03 acceptance. Do not split away recovery obligations to close #369 early. If review benefits from two PRs, A01/A04/A05/A10 can be first, with A02/A03/A06-A09/A11 second and necessary explicit A01/A05/A10 follow-up authority; no acceptance until both complete. No autonomous host background poller or production startup is introduced by these library/service paths.

| ID | Literal proposed WP04 path | Role / dependency |
| --- | --- | --- |
| B01 | `src/AIQuantTradingResearch.Application/MarketData/PrivateResearchCalculator.cs` | New D3 fixed-point recurrences, checkpoint state and prior-only regime/trend engine |
| B02 | `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchCalculatorTests.cs` | New independent vectors, warmup, boundaries, exact restart/retention/replay |
| B03 | `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchReference.cs` | New separately authored C# reference using independent control flow/derivations, never importing production calculator outputs |
| B04 | `tests/AIQuantTradingResearch.Infrastructure.Tests/CommittedResearchProjectionTests.cs` | New real WP03 transaction plus WP04 projection, stale revision/correction/restart/event retention tests |

WP04 additionally proposes LIMITED MODIFICATION of A01/A05 after their actual accepted implementation: refine calculated-reader/checkpoint type mapping and bind the projection without a second writer. These two future existing paths require fresh reconciliation/explicit authority, not a guessed schema. They remain owned by WP03 while its PR is active. No WP04 source implementation until WP03 acceptance. Separate WP04 implementation PR only after schema/type/checkpoint inventory proves the proposed mapping; add paths only by reviewed amendment.

Future evidence paths (proposals, not created): `docs/roadmap/release-1.15/RELEASE_1.15_WP03_IMPLEMENTATION_EVIDENCE.md`, `docs/roadmap/release-1.15/RELEASE_1.15_WP04_IMPLEMENTATION_EVIDENCE.md`. Existing plan/manifest may receive dated evidence links within that later exact scope. No Docker/PowerShell harness path is authorized/proposed for mutation now; Linux runtime qualification will need a separate literal-path proposal once actual dependencies exist.

## Functional decision / acceptance matrix

| WP / actual component | Missing contract / proposed paths | Deterministic offline proof | Dependency / owner / acceptance evidence |
| --- | --- | --- | --- |
| WP03 daily adapter exists | Minute OHLCV/completion/unit rules A01-A03/A08/A09 | Canned BTC/ETH1m bodies; incomplete/future/misaligned/duplicate/conflicting/negative/impossible/precision/unknown-volume cases; truncated/oversized/invalid response and cancellation | WP03; no real provider capability/quota assertion |
| WP03 observation store exists | Atomic complete raw/research identity A01/A04/A05/A10 | Temporary DB exact committed reader, version0 empty, successful empty poll, commit failure no memory advance, identical idempotence, correction version increment,1001st row prunes oldest without reseed | WP03; private rows never become a public D4 success automatically |
| WP03 scheduler missing | Fair configured bounded polling/backfill A02/A08 | Fake clock/credits; one-inflight, round-robin BTC/ETH, live-priority/backfill spare credits, exhaustion/no burst,429/retry policy, restart credit uncertainty fail-closed, cancellation | WP03; measured provider capabilities later separate authority |
| WP03 lineage/recovery missing | WAL/lock/checkpoint/witness A05-A07/A10/A11 | Two writer attempts, held lock/loss/no steal; failures before/after transaction/checkpoint/witness/rename/prune; DB-ahead, witness-ahead, corrupt sidecar/hash/version, mismatched generation; bounded retry and evidence-aware pruning | WP03 local tests; actual Azure mount durability/exclusion WP07, no local proof substituted |
| WP04 historical calculators exist | Exact minute D3 checkpoint B01-B03 | Independent flat/ramp/down/alternating/mixed/extreme/ties/sqrt boundary vectors, null positions, all seeded states and bit-exact reference comparison | WP04 only after accepted WP03; documentation arithmetic is not a new golden test |
| WP04 regimes/events missing | Prior300 calibration, validity/event replay B01/B02/B04, limited A01/A05 |400 unavailable/401 eligible, strict equality boundaries/precedence, no lookahead, gap/reset, corrections with/without sufficient evidence, event identity/idempotence/predecessor pruning, restart after retained window shifted | WP04 with WP03 atomic writer; no separate post-commit indicator patching |
| WP05 real server/client missing | #38714 paths reconciled later | Actual C# committed raw/calculated view transformed then Python validated full/unchanged; no fixture503/synthetic backend | WP05; current approved planning not merge/source authority |
| WP06/WP07 integration missing | Final UI + staging pair, separately scoped | Browser/control/viewport/disconnect; paired immutable Linux image/local resource evidence, then explicitly authorized staging F1 proof | WP06/WP07, no production inspection/publishing |

## Numerical and recovery test requirements

Frozen D3 uses integer scale10^12, BigInteger intermediates, symmetric signed nearest-even and canonical integer checkpoint encoding; bit-exact replay, not float tolerance. Seed positions: SMA/EMA20/Bollinger20 at20, RSI14 at15, MACD line26/signal-histogram34, ATR14 at14 including first high-low. Bollinger population sqrt rounding compares4A against400(2k+1)^2; negative bands/MACD valid. Flat RSI50; slope first25. Do not reuse hour-grid validation or transform inputs.

Independent reference B03 must be authored separately before production golden expectations. Use literal hand-derived vectors and analytical invariants, independently implemented quotient/remainder rounding and square-root midpoint tests, then compare complete series and checkpoints. Families: constant, rising, falling, mixed/alternating, extremes, true signed half ties, flat zero denominators, invalid precision/magnitude, gap/out-of-order/duplicate/correction and cancellation. WP01 flat/ramp/ATR anchors are specifications to independently derive, not copied calculator outputs. No new expected hashes or test counts invented here.

Trend votes use close versus EMA20, histogram sign, RSI>55/<45; sum -3..3, Bullish>=2/Bearish<=-2. Ratios normalized ATR/width/slope5 follow exact D3 formulas. Classification begins401 with samples101..400 as calibration, excludes current sample. Type7 percentiles P40/P70/P80 use exact rounded interpolation. Readiness then Volatile, Trending, Ranging, Undetermined precedence; equality does not satisfy strict comparisons. Before eligibility public-facing classification/score fields remain null even if private trend votes exist. Verify flat401 Neutral/Undetermined independently.

Checkpoint contains segment/index/last timestamp/previous close;20 closes; EMA12/20/26 seeded flags/pre-seed sums/counts/current values; Wilder gain/loss/ATR states; MACD signal seed/state; six EMA20 values; prior300 feature triples with validity/timestamps; last valid trend; algorithm/commit lineage checksum. Keep bounded checkpoint state across1000-row pruning. Test uninterrupted versus checkpoint-restart bit-for-bit, then retained historical correction replay from valid preceding state; insufficient evidence invalidates/restarts continuity honestly. Emit SegmentStart at first actual row and TrendTransition at first valid or changed trend, not regime-only changes; retain event while its own row survives even if predecessor is pruned. Complete correction replay replaces affected events/validity, never append duplicates.

Future focused commands after exact authorization: `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter FullyQualifiedName~ResearchIngestionServiceTests`; Infrastructure filters for TwelveDataMinuteCandleSourceTests, SqliteResearchStateStoreTests, ResearchStateRecoveryTests; WP04 Application PrivateResearchCalculatorTests and Infrastructure CommittedResearchProjectionTests. Use deterministic fake clocks/canned handlers and owned temp DBs; no credentials/network. Capture actual commands, runtime/native SQLite versions, counts, seeds, failures, source hashes and cleanup. Missing offline restore/build artifacts block execution rather than authorize package installation. Full relevant suite only if changed shared contract regressions justify it.

Run-only literal regressions: `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataClientTests.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataTimeSeriesNormalizerTests.cs`, `tests/AIQuantTradingResearch.Application.Tests/TechnicalIndicatorCalculatorTests.cs`, `tests/AIQuantTradingResearch.Application.Tests/WilderAverageTrueRangeCalculatorTests.cs`. Existing Application/Infrastructure test csproj files and source csproj files are read/build-only; automatic C# globs cover proposed source/tests without edits. No prior Release1.14 tests rewritten to fit a new API.

## Decisions and remaining blockers

Owner review requested on dedicated private research DB/schema, conditional WAL/FULL and lifetime lock design, bounded checkpoint/evidence retention and interface shape. These are justified D2 proposals, not already proven feasible or a silent contract freeze. Actual mount locking/fsync/rename/persistence, overlapping research app writers and storage rollback semantics remain WP07 proof obligations. No unsupported distributed guarantees or new paid resources.

No measured Twelve Data1m availability, volume units or quotas are claimed from fixtures; later live acquisition requires explicit authority. No licensing gate introduced. Real indicator state/401st readiness, coherent correction publication, durable D5 config/witness recovery and numerical independence remain implementation evidence gaps. WP03-first acceptance is offline-functional acceptance only; it cannot establish public/Azure readiness.

Sequence: review this plan -> explicitly authorize WP03 literal paths and resolve D2 proposals -> implement/test separate WP03 PR -> owner acceptance -> reconcile actual WP03 contracts and authorize WP04 separate PR -> accept numerical/committed-state evidence -> reconcile #387's14 paths -> WP05 -> WP06 -> separately authorized WP07 staging-only paired-image/browser/F1 work. Never merge #386/#387 implicitly, close issues early, modify production or fabricate a backend.

Future staging targets only: UI aiqr112wp05wus27f5eabb5 and research aiqr115researchstagingwus27f5eabb5, WestUS2. Registry push/settings/credentials/deployment/assessment each need separate authority and reviewed paired digests/config; no production fallback. No Azure operation in this task. `PUBLIC_RUNTIME_VERIFIED=False`.

## Documentation validation evidence

Seven exact documentation paths validated;35 relative Markdown targets/anchors resolved;15 proposed new source/test paths confirmed absent on baseline. All8 original prompt byte counts/SHA-256 values and archived destination hashes verified. Four existing prompt archives remain unchanged; four new archives preserve wording with declared whitespace-only normalization, including separate WP05 owner-copy collision disposition. Manifest is append-only. Independent UTF-8/no BOM/LF/final-newline scans and git diff --check PASS; staged blobs checked separately before commit. Original checkout status/HEAD and all11 dirty/untracked file hashes match the preflight snapshot. No product tests executed. Planning readiness is not implementation acceptance; leave this PR OPEN/UNMERGED.

## WP03 simple functional implementation - actual evidence 2026-10-24

### Authority and scope

Owner separately authorized exactly A01-A11 from this merged plan through the supplied WP03 simple-functional implementation authority. Verified #388 MERGED atb066124713f6a6f25ae7fdb0f57300ed4a83c853; isolated implementation branch starts at that current main. #386 remains OPEN at1829b05cd627f53d556758d797d191af06807263 and #387 OPEN atc59634534fb65c85785971fcdd11a23729b5e0b8; neither merged. Both last verified base4578f8645c9626f4daa7ec76923953e8892c93d6. No WP02 pending code adopted.

Exactly11 new approved C# source/test paths plus append-only updates to this existing plan and existing manifest:13 total. No startup, DI, project/package, historical factory/schema/calculator, Docker, WP04/05/06, Azure/provider/credential/registry change. No new evidence or prompt file is created. New discovered prompt `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` remains untouched/untracked in the owner checkout; proposed same relative destination archive awaits explicit new-path approval. Original7920bytes, SHA-25608fd7d1fadd5e38e4d413c5e6b972d54ff3e6801fb67668e92b750e2625c7a7c. Its wording was read from the supplied attachment and original hash inventoried; no archive content invented.

All previous8 prompt dispositions remain in the [provenance inventory](RELEASE_1.15_WP03_WP04_PROMPT_PROVENANCE.md). Four pre-existing committed duplicates, normalized historical archives and distinct WP05 owner-copy remain unchanged; there is no new collision resolution or duplicate archive. A full12-file before/after inventory is below, including9 untracked prompts. Owner branch remains feat/release-1.15-wp02-runtime-isolation at6f76697707e2363cc52820e0c805172815b92159; full status, names, sizes and hashes unchanged. No personal host path recorded.

### What actually works

- CompletedResearchCandle is an immutable private BTC/ETH raw OHLCV record, exact12-place representability, price<=10^9/volume<=10^12, positive/OHLC-order/UTC-minute bounds. ResearchPollBatch defensively copies at most1000 rows; duplicates/order/instrument/completion/grace reject before commit. Provenance is Twelve Data/base-asset volume; this is not a claim of live acquisition verification.
- TwelveDataMinuteCandleSource is a separate opt-in injected-handler transport, hardcoded HTTPS provider origin,1min request, bounded1000 rows/1MiB UTF-8 body, deadline/cancellation, metadata/duplicates/precision checks and generic failures without inner exception/provider text. Built-in handlers have redirects/proxy disabled. A valid current forming bar is checked but excluded until its close plus grace; future-dated/malformed data rejects. Volume capability requires explicit confirmation. Tests inject only fake handlers/dummy key: ZERO live provider calls and no real credential acquisition.
- ResearchIngestionService is caller-driven, single-flight, round-robin BTC/ETH, configured per-process credits/minute including failed attempts, monotonic observed-minute checks and no immediate retry/background service. Atomic store Commit compares expected generation/version and commits the whole prepared raw state, rather than accepting a stale writer view. No default live activation.
- SqliteResearchStateStore owns a separate research.sqlite under an explicitly supplied dedicated private directory and lifetime local file-share lock. It verifies WAL/FULL, bounded busy wait and foreign keys. Existing historical DELETE-mode factory and observation tables are unchanged. Dedicated schema is intentionally smaller than the earlier logical proposal: metadata, per-instrument complete bounded JSON raw snapshots, raw checkpoints and a bounded commit-hash chain. No unused analytics/config tables or guessed WP04 schema were added.
- A single SQLite transaction updates actual retained candles/segments/poll metadata and per-instrument version; generation persists, identical data is idempotent, corrections increment version/reset affected continuity, gaps stay gaps, oldest rows prune above1000 without renumbering surviving segment indices. Empty polls record success without inventing a candle commit/version. Rows older than retained window reject rather than reintroduce deleted history.
- Immutable IResearchCommittedStateReader.Read(instrument) returns generation, instrument/version, raw candles with segment/index, commit/poll times and integrity. It copies rows into read-only collections. ResearchReady is always false; status Unavailable (or internal recovery blocks reads). This is the actual WP04 input, not a D4/public DTO. No calculated rows, HMAC transform config or service-key mechanism is falsely represented as implemented.
- Every5 changed instrument commits write a checksummed RAW-state checkpoint descriptor inside the transaction. Outside watermark is flushed and replaced after SQL commit; failures return committed state with Degraded protection, never a false rolled-back result. Verified DB-ahead chains reconcile on restart; witness-ahead/wrong lineage/missing-after-mutation, corrupt DB/checkpoint/state/chain fail closed. Up to128 commit witnesses are retained; a lagging unresolved outside witness is never pruned to fit the bound: the next transaction rolls back instead. No cross-file atomicity, directory-fsync or coordinated rollback detection is claimed.

### Actual commands/results

Existing ignored project.assets.json and generated NuGet props/targets were reused from the protected checkout into ignored obj directories of the fresh worktree for Domain/Application/Infrastructure/Worker and two test projects. NO restore, package install or upgrade. Logs/build outputs are task-owned local temporary artifacts, not repository additions. SDK10.0.103; MSBuild18.0.11; VSTest18.0.1; Windows win-x64; Microsoft.NETCore/App and ASP.NET10.0.3; native SQLite3.53.3 observed in an executed test. These are Windows local proofs, not Linux image/F1 proofs.

| Command (repo-relative projects) | Final executed result | Scope |
| --- | --- | --- |
| `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter FullyQualifiedName~ResearchIngestionServiceTests --logger "console;verbosity=minimal"` |13 passed,0failed/0skipped, exit0 | New validation/grace/fairness/quota/single-flight/cancellation cases |
| `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter "FullyQualifiedName~SqliteResearchStateStoreTests&#124;FullyQualifiedName~ResearchStateRecoveryTests&#124;FullyQualifiedName~TwelveDataMinuteCandleSourceTests" --logger "console;verbosity=detailed"` |47 passed,0failed/0skipped, exit0 | New real DB, recovery and offline HTTP tests, including adapter->ingestion->SQLite BTC+ETH integration |
| `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --no-restore --filter "FullyQualifiedName~TechnicalIndicatorCalculatorTests&#124;FullyQualifiedName~WilderAverageTrueRangeCalculatorTests" --logger "console;verbosity=minimal"` |49 passed,0failed/0skipped, exit0 | Existing unchanged calculator regressions |
| `dotnet build src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj -c Release --no-restore` |Exit0,0warnings/0errors | Supplies existing Worker-launch regression prerequisite, no startup edits |
| `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --no-restore --filter "FullyQualifiedName~SqlitePersistenceTests&#124;FullyQualifiedName~SqliteDatasetTests&#124;FullyQualifiedName~TwelveDataClientTests&#124;FullyQualifiedName~TwelveDataTimeSeriesNormalizerTests" --logger "console;verbosity=minimal"` |59 passed,0failed/0skipped, exit0 | Existing unchanged persistence/provider regressions |

Final total168 cases:60 new WP03 cases and108 named regression cases. Repeated attempts are not extra unique tests. No full-suite, CI, Linux, Docker, browser, provider capability or Azure evidence inferred. The final minute-filter correction reran the complete new Infrastructure47-case suite; Application code/cases and existing regression code were unchanged since their recorded successful runs.

Earlier failures retained: initial Infrastructure compilation failed on escaping in authored test JSON strings (no tests ran); corrected only the allowed test file. Next42-case run had41pass/1failure: corrupted checkpoint escaped as KeyNotFoundException; sanitized fail-closed constructor path corrected. Expanded44-case run passed, then46 and final47 passed as integration/version/forming-minute checks were added. The first existing Infrastructure regression run had56pass/3failure because tests invoke Worker Release --no-build and Release output was absent in the fresh worktree (exit1/listener not reachable). Read-only test inspection identified that prerequisite; building unchanged Worker Release with no restore then rerunning the SAME59 cases passed. No weakened assertions/skips or old test edits. Initial authored analyzer warnings were corrected; remaining Debug Infrastructure compilation warnings are in unchanged SyntheticSnapshotQualificationTests.cs (CA1711/SYSLIB1045/CA1861/CA2219), not new code.

Negative evidence: exact closing/grace tick boundaries; invalid/oversize/precision/malformed UTF-8/duplicate metadata/unknown volume capability/status/HTTP401/429/500 and no retry; generic errors contain no dummy-key/response/path inner exception; no current/future fabricated bar; input copy and immutable snapshots; gap/correction/version/idempotence/instrument isolation; bounded1000 pruning; stale expected revision; before-commit/checkpoint fault rollback; watermark write/replace degradation and restart reconciliation; ahead/missing/mismatched witness/corrupt checkpoint/DB/state/chain; evidence-budget exhaustion; local held-lock exclusion; cancellation and concurrent coherent reads. Tests do not certify cross-process Linux lock semantics or directory modes from Windows.

### Exact source/test hashes for review

| Approved changed path | SHA-256 |
| --- | --- |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs` | `32c95cbd553fce51b9083b7be45de63b2f37b73fcf89b21fe188483ed349b2d8` |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchIngestionService.cs` | `3f3f5b7da33b01b6b6d04218acb612a233dc660ef344750796742a6f7475f270` |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataMinuteCandleSource.cs` | `96a5cec54940d707b19c012805d03a67c77a1f8f8b1626b16f20ebfe8a544a65` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateSchema.cs` | `6c375b3643235e5bad56a319a76c335f3b436588702c44af4cadf826793e4b09` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` | `3d735f61ac79f6f5e5d5719e65ee887e43bd9e8e921566406b684efc94ede3b6` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateRecovery.cs` | `2b59eb9de8bb99517ca96d3ae9244c32ac38855314ab857bd631c2ee12b51238` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchWriterOwnership.cs` | `626d28a245be4d8fba1ab7ca3a6d125149ece0888299cf5910b997023a1908c9` |
| `tests/AIQuantTradingResearch.Application.Tests/ResearchIngestionServiceTests.cs` | `0174916ad62d48a0a9d1ee8a00083344ae891c7d5e2c96ad26816a9933289c3d` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/TwelveDataMinuteCandleSourceTests.cs` | `7479828260f914a990e16bdcc68ae6187e0566979124c707c6184918a2691a9b` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteResearchStateStoreTests.cs` | `7318d726dd0ce4deb72d4ca9e4a5be2c5ef4ae593042c7adf8713c129881f6fd` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStateRecoveryTests.cs` | `1de34adda013129e7bfe3d9eea5fba5e2112c4cf67d42a3582ccbe0765d4f117` |

### Complete protected owner inventory (before = after)

| Original dirty/untracked path | Bytes | SHA-256 |
| --- | --- | --- |
| `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md` | 5865 | `eaf83cb62981f8a37c84fbea68930c383bbddfe1b23ebdb7c782480d457bcbf3` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_EVIDENCE.md` | 3858 | `1534c46fad27e485566fe7b3ba50dbefef4b4b20dd546dfb1fa96a14b2c2b579` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md` | 11665 | `cd21cdbcc176d206da8ccbc73876b63d9e8d27358fd5a463448285351f761dbc` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-azure-existing-f1-plans-additional-apps.md` | 6991 | `fc1cae2eb9c524bd9659edd2477629f315a8bb5caa6cb585beea32d0af215184` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-create-two-existing-plan-f1-apps.md` | 7888 | `5b6a452d1c82e155e3df129f8719cba5ac1b872af092753e319b0d2c558e0f71` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md` | 10144 | `a670a7e32627c13dc45cc8c17a386af39e94ce33dbe4ef6f505760ef22b18548` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-dual-docker-api-key-codex-prompt.md` | 6619 | `35097506ecd266af3c4f543c7936b1375224925e05a9c5084ae45cdf63b3ffb0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-pr384-merge-and-next-preflight-codex.md` | 6183 | `32249579c08b2d49a81edfe09640ad2b8e49a726c4cf06e73070a05f13b42fc8` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-simplified-lab-hardening-codex.md` | 6609 | `f28dd004399d5649c1822c6e8ffb4bf9e85038b3320c1c800ea2c0b2d01608b0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` | 7920 | `08fd7d1fadd5e38e4d413c5e6b972d54ff3e6801fb67668e92b750e2625c7a7c` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-wp04-foundation-codex.md` | 7870 | `9466670fa4c172e0d1481525956df823c30156d5db8b1e3247f96f4533087c43` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp05-staging-only-integration-codex.md` | 10602 | `e08e63e06d64ec9fcb061c8781d8e238b5ba26124740a16abeaef49dbf3936e0` |

### Honest limitations / next authority

1. No runtime composition, host/background ingestion or readiness activation exists. A coherent caller-driven library/service and real private persistence work; application startup would need separately approved `src/AIQuantTradingResearch.Worker/Program.cs` and any actual DI path discovered during that integration (currently existing `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs` and `src/AIQuantTradingResearch.Application/DependencyInjection.cs` are unmodified, not implicit grants). No additional source was essential to this tested offline subset.
2. Checkpoints describe raw commit consistency ONLY. D3 recursive indicator seed/rings/prior300 calibration/bit-exact replay are NOT implemented here, belong to WP04 and need actual contract/store reconciliation within separately approved paths. There are no classifications/events/Ready claims. Corrections conservatively reset affected continuity because numerical reconstruction is unavailable. Protected raw commit chain is bounded, not a complete arbitrary historical correction archive.
3. Durable D5 private-key/keyEpoch/transformEpoch activation/recovery is NOT implemented; future WP03/WP05 reconciliation must supply it before transformed publication. No D4 endpoint/full/unchanged successful snapshot is fabricated. Missing raw lineage/checkpoint evidence blocks reads/commits, not bypasses protection.
4. Credits/fair alternation are per service lifetime; no persistent/global provider-quota guarantee across restarts, backfill-priority scheduler or measured plan entitlement. Adapter requests bounded latest1000 only; older archive/backfill recovery outside retained range is not implemented. Future orchestration must avoid ambiguous restart credits; no automatic polling/paid service/live credential change added to hide this limitation.
5. Lifetime local ownership, WAL/flush/rename permissions are verified only on this Windows local test filesystem. Unix0700/0600/reparse checks are implemented but Linux/private-container mount/thread/process/overlap security is unqualified. No cross-App-Service exclusion guarantee, directory flush or atomic SQL+watermark promise. DB+watermark coordinated rollback can remain undetected. A first watermark failure before any witness exists leaves subsequent mutated lineage uncertain and fails closed; publication stays disabled.
6. No live provider calls or acquisition rights/quotas/volume-unit entitlement proof; no secret retrieval. Production completely untouched, including read-only probes. Eventual deployment/assessment is staging-only to approved UI/research apps after explicit separate paired-image/config publication authority. No license gate or paid dependency introduced.

Recommendation: owner review of the functioning WP03 subset, not WP03 acceptance or WP04 readiness. #369/#370/#371 stay open. Confirm missing recovery/epoch/calculated-state obligations and narrowly scope next authority before WP04/WP05 runtime integration. Leave implementation PR OPEN/UNMERGED. `PUBLIC_RUNTIME_VERIFIED=False`.

Publication validation: exact13-path diff (A01-A11 plus2 authorized existing docs),11 source SHA-256 matches,28 relative Markdown targets/anchors, append-only historical documentation, independent UTF-8/no BOM/LF/final newline and git diff --check PASS. Original12 dirty/untracked file paths/sizes/hashes, complete status and HEAD preserved. Staged blobs independently scanned before commit. No generated build/log artifacts staged; no issue acceptance/merge/deployment.

## WP04 private research - actual implementation evidence 2026-10-24

### Verified baseline, authority and scope

Current main/isolated starting HEAD bf4e744330986eac8234cda7a73ce866aa092242 (#389 squash), containing #388 b066124713f6a6f25ae7fdb0f57300ed4a83c853. #386 OPEN at1829b05cd627f53d556758d797d191af06807263; #387 OPEN atc59634534fb65c85785971fcdd11a23729b5e0b8; GitHub reported both bases4578f8645c9626f4daa7ec76923953e8892c93d6. Neither merged/cherry-picked. #370 remains OPEN.

Four new B01-B04 and two existing A01/A05 files implement the explicitly authorized six-path scope. Only this preflight and existing manifest receive dated append-only documentation (8 changed files total). Automated review initially misread A01 scope; re-read explicit six-path authorization and direct retry succeeded. Automated review rejected documentation twice despite attachment line28; no writes occurred from those attempts. Owner then directly approved BOTH existing docs through the approval question, permitting these appends. No indirect workaround or new archive/evidence file. ResearchStateSchema.cs, ResearchStateRecovery.cs, ResearchIngestionService.cs and Worker Program bytes match baseline. No DI/provider/Python/Docker/project/dependency/lockfile or pending387 edits.

### Actual calculations and atomic private reader

PrivateResearchCalculator implements aiq-r115-research-v1 on validated raw private minute OHLCV: SMA20/EMA20, population Bollinger20x2, Wilder RSI14/ATR14, MACD12/26/9, normalized ATR/width/slope5, trend score and regimes. BigInteger scale10^12 with signed nearest-even, exact seeds/null positions, first-bar ATR, flat RSI50, signed MACD/negative bands and integer square-root midpoint rule. All13 calculated quantities are canonical FIXED12 STRINGS or null; integer checkpoint values canonical INTEGER strings. This avoids decimal/double significant-digit loss for allowed large normalized ratios. Raw WP03 candle decimals remain unchanged/exact. No D5-transformed inputs or custom numerical dependency.

First valid classification401 uses exactly prior300 feature samples101..400 after100 warmup, never current. Type7 percentiles, strict comparisons and readiness->Volatile->Trending->Ranging->Undetermined precedence. Null score/trend/regime until eligible. SegmentStart at actual segment origin; TrendTransition first valid/change only, not regime-only; previous trend/current regime retained. Enclosing generation/instrument and segment/time/type identify events. Corrections rebuild affected segment/events without stale markers.

Existing immutable committed reader now carries optional ResearchCalculation/Classification/events and ResearchCalculationCheckpoint. New explicit SqliteResearchStateStore(directory, calculateResearch:true) opts in; default raw mode remains compatible/Unavailable. ResearchReady reflects eligible last classification, separate from later freshness/transport. A raw-mode writer cannot overwrite a calculated instrument. No startup/DI/UI/HTTP activation.

Existing instruments.payload and checkpoints.payload/checksum TEXT columns support calculated rows and a versioned recursive frame. Actual before/after sqlite_schema DDL identity test PASSES; no table/column/migration or schema-file change. A05 verifies expected generation/version and calculates/persists raw+calculated+checkpoint in the SAME existing writer transaction/hash chain. Existing outside watermark remains post-commit. Projection/checkpoint/before-commit fault tests roll back all state. No second writer/cache authority or caller-supplied projection injection.

Calculated checkpoints persist every changed commit for exact version alignment; raw-only cadence5 preserved. Frame contains before-first-retained and after-last state: segment/index/time/previous close,20 closes, EMA12/20/26 seed sums/current values, Wilder gain/loss/ATR sums/means, MACD signal seed/state,6 EMA20 values, prior300 indexed/timestamped features and last trend. Fixed periods/index encode seeded flags/counts unambiguously. Canonical integers/frame shape/depth/rings/lineage/version/time validated; WP03 checksums/watermark remain recovery authority.

Bounded projection considers <=2000 actual rows before pruning, retains latest1000, advances baseline across pruning without reseeding visible history. Nonflat restart matches uninterrupted projection bit-for-bit. Event survives while its own row remains even if predecessor pruned. Corrections/gaps honestly rewarm. Already-pruned raw history without seed evidence stays unavailable until a genuine new segment can rewarm. Private limits4MiB row payload/512KiB checkpoint wrapper/256KiB recurrence state; not new D4 wire limits or Azure resource claims. SQL+outside watermark not cross-file atomic; coordinated rollback limits unchanged.

### Independent proof and actual results

PrivateResearchReference was authored BEFORE production engine: full batch arrays/separate EMA/Wilder passes, independent quotient/remainder rounding and Newton root; no production helpers/output import. Compares ALL13 outputs and classifications for flat/rising/falling/mixed/intrabar/large/extreme-ratio families, plus literal seed/ATR/MACD/RSI and signed/sqrt midpoint anchors. ZERO integer tolerance. Covers400/401, no lookahead, strict equality, Volatile precedence, independently identified regime-only changes, stale/mixed/malformed checkpoints, gaps, pruning, cancellation. True SQLite integration separately covers commit/restart, nonflat checkpoint, corrections/invalidated readiness, duplicate/stale lineage, transactional faults, event retention and unchanged DDL.

Existing ignored NuGet assets/props/targets reused for six required projects; NO restore/install. SDK10.0.103/VSTest18.0.1, Windows win-x64; executed version test runtime10.0.3/native SQLite3.53.3. No signing/policy change. Suite duration is not performance/F1 qualification.

| Actual executed command | Final result |
| --- | --- |
| `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter FullyQualifiedName~PrivateResearchCalculatorTests --logger "console;verbosity=minimal"` |30 pass,0fail/0skip, exit0 |
| `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter "FullyQualifiedName~CommittedResearchProjectionTests&#124;FullyQualifiedName~SqliteResearchStateStoreTests&#124;FullyQualifiedName~ResearchStateRecoveryTests&#124;FullyQualifiedName~TwelveDataMinuteCandleSourceTests" --logger "console;verbosity=detailed"` |58 pass,0fail/0skip, exit0:11 new SQLite+47 WP03 |
| `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --no-restore --filter "FullyQualifiedName~ResearchIngestionServiceTests&#124;FullyQualifiedName~TechnicalIndicatorCalculatorTests&#124;FullyQualifiedName~WilderAverageTrueRangeCalculatorTests" --logger "console;verbosity=minimal"` |62 pass,0fail/0skip, exit0:13 WP03+49 numerical regressions |

150 unique final cases:41 new WP04+109 regressions. #389168 remains historical; its other59 Worker/persistence/provider cases NOT rerun/counted here. Windows local only, not CI/Linux/browser/Azure/provider capability. HTTP uses offline fixtures only, zero live calls or credentials.

Earlier evidence: initial24 Application/10 Infrastructure passed before representation review. Code review identified decimal precision risk; new output fields changed to exact strings with ratio/intrabar/shape cases. Expanded compilation failed when a test-helper replacement referenced family in midpoint test; fixed that authorized region. Expanded27 then final30 Application cases passed; final58 Infrastructure and62 regressions passed. No skip/xfail/deleted failing assertion. New CA1822/xUnit2031/CA1861 warnings corrected; remaining Infrastructure warnings belong to unchanged SyntheticSnapshotQualificationTests.cs (CA1711/SYSLIB1045/CA1861/CA2219).

### Exact source/test paths and hashes

| Authorized path | SHA-256 |
| --- | --- |
| `src/AIQuantTradingResearch.Application/MarketData/PrivateResearchCalculator.cs` | `1d4c090abcf390f14d4f9889ef98a9ef29475da1cafbae3047c2b22190c980b0` |
| `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchCalculatorTests.cs` | `175bf5177641ccf3234ea82be20fd9535b895909628f9b0ae7a7a7c4e16613c4` |
| `tests/AIQuantTradingResearch.Application.Tests/PrivateResearchReference.cs` | `31441e5ec0f2224a3c20c2bada6d49455d2cda212235c452d528811eb5a8cc54` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/CommittedResearchProjectionTests.cs` | `d1c8c0de0edba2e799fcdaeccc7e3e9edbc115e1983e903c0cf2abb6fc9a5ce3` |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs` | `b4e40269e7b3916cdda66ae47b16511d7e8d7ff3092be15eb7f401d02ecf4a0c` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` | `1b9160487e5f1fb0b2396f7c9f2e2cc568299ab07f9027b67635528783bfa487` |

### Complete protected owner inventory - before = after

Original branch feat/release-1.15-wp02-runtime-isolation at6f76697707e2363cc52820e0c805172815b92159; complete status and13 original sizes/hashes unchanged. Ten untracked Codex prompts. Previous8 archive dispositions unchanged in [provenance](RELEASE_1.15_WP03_WP04_PROMPT_PROVENANCE.md). New WP03 and current WP04 prompts remain ORIGINAL/PENDING new archive-path approval: proposed destinations `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` and `docs/roadmap/release-1.15/prompters/release-1.15-wp04-simple-research-implementation-codex.md`. No archives moved/deleted/duplicated/overwritten; original byte counts/hashes below. No personal host paths.

| Original relative path | Bytes | SHA-256 |
| --- | --- | --- |
| `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md` | 5865 | `eaf83cb62981f8a37c84fbea68930c383bbddfe1b23ebdb7c782480d457bcbf3` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_EVIDENCE.md` | 3858 | `1534c46fad27e485566fe7b3ba50dbefef4b4b20dd546dfb1fa96a14b2c2b579` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md` | 11665 | `cd21cdbcc176d206da8ccbc73876b63d9e8d27358fd5a463448285351f761dbc` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-azure-existing-f1-plans-additional-apps.md` | 6991 | `fc1cae2eb9c524bd9659edd2477629f315a8bb5caa6cb585beea32d0af215184` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-create-two-existing-plan-f1-apps.md` | 7888 | `5b6a452d1c82e155e3df129f8719cba5ac1b872af092753e319b0d2c558e0f71` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md` | 10144 | `a670a7e32627c13dc45cc8c17a386af39e94ce33dbe4ef6f505760ef22b18548` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-dual-docker-api-key-codex-prompt.md` | 6619 | `35097506ecd266af3c4f543c7936b1375224925e05a9c5084ae45cdf63b3ffb0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-pr384-merge-and-next-preflight-codex.md` | 6183 | `32249579c08b2d49a81edfe09640ad2b8e49a726c4cf06e73070a05f13b42fc8` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-simplified-lab-hardening-codex.md` | 6609 | `f28dd004399d5649c1822c6e8ffb4bf9e85038b3320c1c800ea2c0b2d01608b0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` | 7920 | `08fd7d1fadd5e38e4d413c5e6b972d54ff3e6801fb67668e92b750e2625c7a7c` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-wp04-foundation-codex.md` | 7870 | `9466670fa4c172e0d1481525956df823c30156d5db8b1e3247f96f4533087c43` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp04-simple-research-implementation-codex.md` | 8128 | `f3fff48a3254c2836c3d2b9acd4d895aa9328fda82df105d44a14ef15d9e0b18` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp05-staging-only-integration-codex.md` | 10602 | `e08e63e06d64ec9fcb061c8781d8e238b5ba26124740a16abeaef49dbf3936e0` |

### Remaining obligations / review disposition

Owner review required, not automatic WP03/WP04 acceptance or issue closure. No schema amendment needed for current existing-column binding; later storage changes need scoped review. Missing historical seed evidence remains unavailable. WP05 must reconcile actual fixed12-string indicators/classifications/events and immutable reader against approved #38714-path design; NEVER serialize private checkpoints/raw values. Durable D5 config/transformEpoch activation still WP03/WP05 work; no transform factor or service-key rotation here.

Runtime composition/DI, provider capability/volume entitlement/quotas/restart-wide accounting, Linux private files/processes/mount/WAL/fsync/exclusion, shared F1 and final image/browser qualification remain separate. Local lock is not cross-App-Service fencing. No production/staging Azure/URLs/settings/secrets, live provider, registry/Docker/promotion/deployment operation. Future publishing/assessment STAGING ONLY under separate WP07 authority. Preserve two non-root images, one service key and accepted synchronous HTTPS/native-DNS limitation. Leave implementation PR OPEN/UNMERGED; `PUBLIC_RUNTIME_VERIFIED=False`.

WP04 publication validation: exact8-path scope (6 source/test plus2 directly approved existing docs),6 source hashes verified,30 relative Markdown links/anchors, append-only historical docs, UTF-8/no BOM/LF/final newline and git diff --check PASS. Original status/HEAD and13 dirty/untracked paths/sizes/hashes preserved. Staged blobs independently scanned; no generated artifacts staged.

## WP05 implementation boundary reconciliation - 2026-10-24

Status: BLOCKED at durable D5 integration; owner scope decision required. This is an append-only finding, not WP05 acceptance or a replacement planning PR. The owner directly approved the fourteen WP05 implementation paths, these two existing evidence documents, and one implementation PR left open/unmerged. No additional source path is thereby authorized.

The isolated implementation branch starts at merged WP04 `2ad60bf5c0c670d3f4934b5ee7100df7bf05234f`. Original dirty checkout remains separate. Previous live checks found PR389/390 merged and PR386/387 open; neither open branch was adopted. No application implementation, build, test suite, Docker workload, provider call, Azure operation or deployment was performed in this reconciliation pass.

### Concrete storage mismatch

- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` exposes immutable committed research reads, but its constructor acquires writer ownership, can initialize the database, validates recovery and updates the watermark. It is not a separately qualified read-only database opener.
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateRecovery.cs` computes its data hash from instruments and checkpoints. Its witness binds generation, revision and the existing commit chain, without a D5 active configuration tuple, public transform epoch or configuration activation state.
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchWriterOwnership.cs` permits only research.sqlite and its WAL/SHM files, writer.lock, watermark.json and watermark.json.pending. It rejects unrelated private-directory entries. Adding a transform configuration sidecar there is incompatible with current ownership validation.
- D5 in `RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md`, under "D5-A durable epoch, rollover and unchanged rules", requires a durable active tuple bound to dataset lineage and fail-closed recovery when configuration/epoch evidence is uncertain. Atomic activation, witness and rollback handling are explicitly WP03 obligations. The current WP05 authority prohibits modifying WP03 writer/recovery/schema.

A separate environment value or independently hashed file can supply bytes, but does not establish the missing durable active-epoch recovery contract. A response-only checksum or in-memory counter must not be represented as that proof. No alternate persistence mechanism, schema change, key generation, key rotation or production activation has been introduced.

### Narrow amendment proposed for owner review

Before claiming a working durable WP05 runtime, authorize a focused WP03 prerequisite proposal covering the literal existing paths `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs`, `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateRecovery.cs`, `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateSchema.cs`, `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs`, and `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteResearchStateStoreTests.cs`. The existing test filename was verified; the prerequisite design and scope still require owner approval. This proposal is not permission to modify those paths.

The prerequisite must define initial provisioning, generation binding, exact restart reuse, same-day epoch changes, crash/recovery witness behavior and coherent publication checks. Reuse the existing writer and recovery model; do not add an independent writer or assume that a second config file is transactional with SQLite. If the owner instead chooses to defer durable activation, explicitly approve an offline-only transform/endpoint subset and retain fail-closed runtime behavior; that would be partial WP05, not the working-runtime acceptance target.

No implementation PR is opened from this documentation-only blocker finding. Existing fourteen-path implementation approval remains recorded; no source/test completion is claimed. Native Linux durability and future separately authorized staging qualification remain outstanding. Production remains off limits. `PUBLIC_RUNTIME_VERIFIED=False`.

## Authorized WP03 D5 prerequisite implementation - 2026-10-24

Owner directly authorized the five-path WP03 prerequisite after the preceding blocker report. This resolves the absence of a local durable configuration mechanism; it does not claim WP05 endpoint/client completion or Azure durability. The preceding blocker entry remains historical evidence. Scope: exactly five source/test paths below and append-only evidence in these two existing documents. No other WP05 implementation paths have been edited yet.

### Implementation and compatibility

- Add an explicitly provisioned singleton `transform_configuration` table inside the existing private SQLite database. Persist canonical padded base64 of the 32-byte key, private key UUID, public positive epoch, algorithm, dataset generation and activation revision. No secret sidecar or ownership-allowlist change. The tuple is private plaintext at rest, protected by existing private database permissions, not encryption.
- Include the canonical tuple and activation revision in the existing recovery data hash. Initial provisioning/rotation uses the existing exclusive writer, FULL/WAL transaction, storage revision and commit chain. Instrument generation, versions, rows, poll/commit times and calculation checkpoints are not invented or advanced by configuration activation.
- Legacy databases without the table retain their original data hashes and raw/calculated read behavior. The table is created only during explicit activation. This is an additive schema extension; metadata schema_version remains 1. Compatibility is tested with the current binary; old binaries are not qualified to open a provisioned D5 database and must not be used as a rollback strategy.
- Initial epoch must be 1. Rotation requires expected current epoch, next public epoch and a distinct private key UUID. Exact tuple reinstall is a no-op. Stale expectations, wrong generation, skipped/decreasing epochs and overflow are rejected. No automatic key creation, runtime provisioning command or key-rotation service is added.
- Recovery requires the external watermark to cover at least the tuple activation revision. Failure after the SQLite commit but before successful witness replacement prevents publication and restart. It never guesses the new tuple or silently falls back to the old epoch. Precommit faults roll back and can retry. Manual recovery of an unwitnessed activation is deliberately not automated.
- `IResearchTransformStateReader.UseTransformSnapshot` validates and supplies one coherent committed state/tuple under the existing store lock. The synchronous callback must stage and commit a whitelisted response before returning; it must not escape a snapshot for later publication or perform asynchronous publication. Rotation, ingestion mutations and ownership release cannot interleave, including reentrant mutation/disposal. WP05 still owns actual response staging, UTC checks, bounds and deadlines.
- Private configuration is defensively copied, excluded from default JSON serialization and given a non-secret ToString. Only test keys were used. No key/factor/private UUID is a public response contract.

### Executed offline evidence

Commands (PowerShell-compatible, no restore):

```powershell
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter 'FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchStateRecoveryTests|FullyQualifiedName~CommittedResearchProjectionTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests' --logger 'console;verbosity=normal'
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter 'FullyQualifiedName~PrivateResearchCalculatorTests|FullyQualifiedName~ResearchIngestionServiceTests|FullyQualifiedName~TechnicalIndicatorCalculatorTests|FullyQualifiedName~WilderAverageTrueRangeCalculatorTests' --logger 'console;verbosity=normal'
```

Final results: Infrastructure 83/83; Application 92/92; both exit 0. Total 175 distinct selected cases, including 24 new prerequisite cases (36 total in SqliteResearchStateStoreTests). These are focused regressions, not the full repository suite. Source tests use fake provider inputs and do not acquire market data. No dependency installation or restore.

The first focused run passed 27/31: four tamper tests reached rejection but cleanup failed because the test's external SQLite connection used default pooling. Setting Pooling=False fixed the test handle lifetime; no production rejection assertion was weakened. Subsequent final tests pass. Two introduced analyzer warnings (invariant conversion and blocking Task.Wait) were corrected. Remaining build warnings originate in unchanged SyntheticSnapshotQualificationTests: CA1711, SYSLIB1045, CA2219 and CA1861.

Negative/compatibility evidence includes missing configuration, invalid key length/identities/epoch, defensive copies/no default serialization leakage, wrong-generation/stale/skip/same-private-UUID rotation, exact reinstall, cancellation, precommit rollback, postcommit/watermark/write-replace faults during initial activation AND rotation, key/private UUID/public epoch/generation tampering, restored old witness, missing witness, 135 subsequent commits through chain pruning, exact calculated checkpoint/row preservation, two-instrument tuple reuse across restart, concurrent rotation blocked until staging returns, reentrant commit/rotation/disposal refusal and callback-failure guard release. Native Linux filesystem persistence, actual process crash/power loss and cloud restarts are not established by injected .NET exceptions on Windows.

### Exact source/test paths and SHA-256

| Path | SHA-256 |
| --- | --- |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchStateContracts.cs` | `286582a0d475767a021ad18d3accff3c6349ac28b66dc5873a1e512a4914c1a3` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs` | `073aed9fc7ce369dae321db63b8770e2410f7be9527f60de7e2c7b21fc610c9b` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateRecovery.cs` | `903c57f16f2260d6a583a90b3abd59e245ed0c47044666e1b4b264e49d1035fb` |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStateSchema.cs` | `9441a82f3165eedee82a5567f5a87215b5d6dd711d4b4d3e5c5225a6e9cf8b88` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteResearchStateStoreTests.cs` | `36ef1d95587531472e207620f356a4f1d4789f912195688296e7e00f979b32d9` |

### Preserved owner work and prompt dispositions

Original branch `feat/release-1.15-wp02-runtime-isolation`, HEAD `6f76697707e2363cc52820e0c805172815b92159`: full dirty/untracked status and all 14 owner-file sizes/SHA-256 match the before snapshot. Original manifest and two untracked WP02 evidence/plan files are unchanged. The prior full 13-file inventory remains above; the additional untracked `docs/roadmap/release-1.15/prompters/release-1.15-wp05-real-snapshot-implementation-codex.md` is 8976 bytes, SHA-256 `08dfbe33dc10fc2309133bde544cc233c7fe292e70575fe06c532199d5e1384d`.

Every original untracked prompt under `docs/roadmap/release-1.15/prompters/` remains preserved:

| Filename | Existing disposition; no archive edits in this task |
| --- | --- |
| release-1.15-codex-azure-existing-f1-plans-additional-apps.md | Existing exact archive duplicate |
| release-1.15-codex-create-two-existing-plan-f1-apps.md | Existing exact archive duplicate |
| release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md | Historical/superseded single-image authority archive |
| release-1.15-wp02-dual-docker-api-key-codex-prompt.md | Existing exact archive duplicate |
| release-1.15-wp02-pr384-merge-and-next-preflight-codex.md | Existing normalized equivalent archive |
| release-1.15-wp02-simplified-lab-hardening-codex.md | Existing normalized archive |
| release-1.15-wp03-simple-implementation-codex.md | Original retained; separate archive-path approval pending |
| release-1.15-wp03-wp04-foundation-codex.md | Existing normalized archive |
| release-1.15-wp04-simple-research-implementation-codex.md | Original retained; separate archive-path approval pending |
| release-1.15-wp05-real-snapshot-implementation-codex.md | New original retained; separate archive-path approval pending |
| release-1.15-wp05-staging-only-integration-codex.md | Existing owner-copy archive; distinct pending PR387 attachment preserved |

### Remaining obligations

WP05 fourteen-path D4/D5 transformer, bounded authenticated endpoint, strict Python HTTPS client, independent cross-language vectors and container qualification remain unimplemented in this prerequisite. No production/staging/provider/registry operation, Docker workload, UI change or deployment occurred. PR386/387 remain open and unmerged; neither source branch was adopted. Leave the prerequisite implementation PR open/unmerged for review, do not close issues or imply WP05 acceptance. Coordinated rollback of database plus witness is still outside the inherited best-effort protection; hash binding is not cryptographic anti-rollback or encryption. Azure mounts, Linux permissions/exclusion/fsync and real crash behavior require separate qualification. `PUBLIC_RUNTIME_VERIFIED=False`.

## WP05 authenticated snapshot implementation after PR391 - 2026-10-24

Owner authority: functional WP05 within the fourteen literal paths verified in open PR387's plan, plus append-only evidence in these two existing documents. Isolated branch starts from fresh main `20a8647c901d3bffc1516b80603e7dab30d01c68`, the verified PR391 squash merge, containing merged PR389/390. Live PR386/387 remained OPEN; neither branch was imported or merged. The WP05 preflight document is not present on this main; its proposed literal names were read directly from PR387 without importing its documentation. No new prompt archive path was authorized.

### Functional implementation

- Actual authenticated HTTPS POST /v1/research/snapshot, with one active environment service key. Length-safe SHA-256/constant-time comparison rejects missing, malformed, wrong and duplicate keys before reading private state. Anonymous /health emits only Alive. Query parameters, unsupported methods/media/encoding and unknown request fields fail closed. Generic bounded errors do not contain private exceptions, keys or raw rows.
- The real IResearchTransformStateReader.UseTransformSnapshot callback performs public transformation AND bounded complete serialization under WP03 publication ownership. Only whitelisted public bytes leave it. Network transmission follows that staged-buffer commit; it cannot recall bytes already committed before a later rotation. No raw state/checkpoints/config are returned, and no read endpoint ingests or rotates keys.
- Durable generation/private tuple/public epoch comes from PR391, not environment-derived D5 identity. Daily SHA-256 HMAC uses the exact LF message, unsigned big-endian digest, all 256 bits, integer factor formula and symmetric ties-even at scale 10^12. Transform price-valued indicators after calculation, including negative bands/MACD. Volume, RSI and dimensionless features remain unscaled.
- Full emits all six exact collections. Unchanged matches all four identity fields and omits all six collections entirely. Both BTC/USD and ETH/USD share the daily factor while retaining separate committed versions, rows and classifications. Truthful partial startup/calibration, empty initialized state, gaps, unavailable intervals, stale/current status and valid classification eligibility are preserved.
- Request bytes are counted while reading, including chunked bodies, at 4096. Success serialization is capped during writing at 4194304. Strict UTF-8, duplicate keys, lexical string/key/number limits, number exponent/magnitude, member/depth/array/token limits and canonical counters/fixed-12 strings are checked. The source rejects date rollover during staging, backward day/epoch and caller-known future same-generation identity; nominal processing cancellation discards staged output. Wire observations/status use microsecond precision consistently.
- Python uses direct synchronous stdlib HTTPSConnection, certificate and hostname verification, no proxies/redirects/plaintext/decompression fallback. Entire bounded JSON/semantic validation precedes atomic cache replacement. Immutable defensive public views, per-instrument request epochs/connectivity/last sync, late-response/collision/rollback rejection, and last-good preservation on failure are implemented. No indicator recomputation in Python.
- Default Worker behavior remains unchanged except one explicit early --research-snapshot-api mode. No provider, background ingestion or automatic D5 provisioning is started. The mode requires an existing verified/provisioned research.sqlite and startup tuple validation. Reopening the existing WP03 store retains its writer ownership and startup recovery/watermark behavior; HTTP snapshot calls do not mutate data.

### Local composition and deferred packaging

Run the built Worker with --research-snapshot-api only after separately providing a test/provisioned existing private store and test TLS material. Configuration names: AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_API_KEY, AIQ_RESEARCH_TLS_PFX, optional AIQ_RESEARCH_TLS_PASSWORD, optional AIQ_RESEARCH_BIND_ADDRESS (default127.0.0.1; only explicit0.0.0.0 alternative). HTTPS port52731. No real credentials were retrieved or supplied. Invalid/missing config exits generically; there is no public readiness or automatic seed.

Python ResearchSnapshotClient accepts a server-side HTTPS base URL, service key, nominal timeout <=5 seconds and optional public CA file. connection_state(instrument) keeps observations separate; no browser/UI integration was added. The stdlib-only executable mode is a test conformance driver.

Two new Dockerfiles package a prepublished linux/amd64 framework-dependent Worker and a stdlib-only client driver. They declare distinct non-root UID1654/1655 and single principal processes, preserve pinned bases and install nothing. They are prepared source artifacts, NOT built/qualified images. The PowerShell5.1 runner requires cached bases, an offline published build, a closed disposable preprovisioned test database and test TLS fixture. Default mode checks input files only; -Execute is for future separately authorized Linux screening. It copies inputs, uses an internal network and resource limits, label-checks cleanup, restores its test-key environment and retains owned local logs/copies. It does not repair storage or provision keys. A timed-out Docker CLI can require later daemon resource reconciliation.

No Docker invocation, Linux container experiment, image publication, staging, Azure, provider acquisition or production inspection occurred. Linux qualification is explicitly deferred by current authority; script/parser checks do not qualify Linux private mount permissions, non-root behavior, SQLite persistence or F1 resources.

### Actual offline test commands and outcomes

```powershell
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-restore --filter 'FullyQualifiedName~ResearchSnapshotTransformerTests|FullyQualifiedName~PrivateResearchCalculatorTests|FullyQualifiedName~ResearchIngestionServiceTests|FullyQualifiedName~TechnicalIndicatorCalculatorTests|FullyQualifiedName~WilderAverageTrueRangeCalculatorTests' --logger 'console;verbosity=normal'
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter 'FullyQualifiedName~ResearchSnapshotEndpointTests|FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchStateRecoveryTests|FullyQualifiedName~CommittedResearchProjectionTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests' --logger 'console;verbosity=detailed'
python -B -m unittest python.presentation.test_research_snapshot_client -v
```

Final exit0: Application112/112, Infrastructure91/91, Python29/29; total232 selected cases, zero failures/skips. This includes 20 new Application, 8 new Infrastructure and29 new Python cases, plus175 predecessor regressions. Not the full repository suite, CI, UI/browser, Linux or Azure evidence. Runtime .NET10.0.3; CPython3.13.15. Existing fake-source tests make no Twelve Data/provider requests. No restore/install occurred.

PowerShell5.1.26100.9711: AST parse and native argument quoting unit check PASS, targeted compatibility Windows PowerShell5.1.26100.9444; exact .9444 binary not executed. Docker runner NOT RUN. New source analyzer warnings were resolved; remaining Infrastructure build warnings are in unchanged SyntheticSnapshotQualificationTests: CA1711, SYSLIB1045, CA1861, CA2219.

Intermediate failures were investigated and corrected: initial test certificate lost usable Windows private-key ownership (reimport its test PFX); one overly broad leakage canary matched required lastSuccessfulProviderPollAt (now tests the actual private field); rejecting oversize without closing the HTTP connection caused a subsequent reused connection failure (errors now close the connection). A later Unicode-boundary test literal was changed by Windows PowerShell native-pipe encoding; ASCII Unicode escapes now exercise the intended multibyte value. Final rejection assertions were not weakened.

### Independent proof and security negatives

The separate CPython reference does not import the production transformer. It computes the 70-byte message, HMAC, factor and integer rounding independently. Test key is the explicit bytes00..1f fixture; private test key UUID11111111-1111-4111-8111-111111111111 is not a real credential. Shared corpus records exact message hex, lengths, digests, factors and outputs. For2026-10-23: digest1482db2988ff8a6447697ecbca8ad4bf2abd46062ed46f625ce13b725610051c, factor916024341133; price100 ->91.602434113300 and signed-7 ->-6.412170387931. For2026-10-24: digest10cd612d8d85ad234bb2b9bb91453a5b523f50bf5443231468b3544c219591e7, factor913126768802. C# literals were checked against these separately authored expectations, not generated from C# outputs.

Real integration commits401 offline dummy OHLCV observations per instrument through the actual WP03 writer with WP04 calculations, provisions the durable fixture tuple, starts actual Kestrel HTTPS on an ephemeral loopback port, and invokes the strict Python client. The Python reference independently constructs every expected constant-series public candle, seeded indicator, classification, event, interval and status. Only random public lineage labels are taken from the response. Complete expected and actual UTF-8 bytes match; no raw-price result is used as a numerical oracle.

| Final observed instrument | Actual and independent expected bytes | Matching SHA-256 |
| --- | --- | --- |
| BTC/USD | 320587 | e18c8f9d928b255f0ceaa6ffad4256aff4c5fc03fb9442d29858baa0f19fcbcb |
| ETH/USD | 320587 | 79bf127a1cb14995cc387c5567c17862c2ae8e88d7276ebad3798a1dac32af88 |

These are executed-run hashes, not stable golden hashes: each test uses fresh random generation/segment UUIDs. Both runtime-generated expected/actual buffers match exactly within the run. The invariant numerical HMAC/factor corpus is stable. A real1000-row calculated store serialized to806376 bytes, well below4MiB; this measures this fixture, not every possible maximum-size field combination or memory/resource qualification.

Negatives include wrong/missing/duplicate API keys, bad method/media/encoding, malformed UTF-8/duplicate/surrogate/trailing JSON, exact4096 positive/+1 rejection, chunked oversize with no Content-Length, lexical/depth/member/token/key/string/array bounds including multibyte UTF-8, canonical numbers/counters/types, precision-ceiling+one-unit, invalid order/alignment/segments/intervals/events/status/readiness, stale request epochs, identity collisions/decrements/new generation, midnight/backward time, durable unconfigured/corrupted state, no raw/key/checkpoint canaries, actual untrusted-CA and wrong-host TLS rejection, no redirect/encoded/non200 acceptance, independently verified signed ties and signed indicator projection, shared BTC/ETH factor, per-instrument/two-session state, immutable views and preserved last-good/last-sync on failures.

### Exact implementation paths and SHA-256

| Path | SHA-256 |
| --- | --- |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchSnapshotContracts.cs` | `b358a8d1e94ce309e8d69582563d0837afc584f7b7fe0c0ef06c594e0ae83dae` |
| `src/AIQuantTradingResearch.Application/MarketData/ResearchSnapshotTransformer.cs` | `b3c5df4dd8480ae4d0bd6e5f8ca5eec269118c184069fdea7536703d1e8be884` |
| `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs` | `c4829cfff0f26c1a13724fa2658bf447cc880fcb693246da93969d42da9881d7` |
| `src/AIQuantTradingResearch.Worker/ResearchSnapshotSource.cs` | `eec18721d8bd9b5ec6a2ef361e792ee2998f1c1a53ac4972722fbd15c843c253` |
| `src/AIQuantTradingResearch.Worker/Program.cs` | `ba909a33e8fdc706c64ee9da97d6ef78aeaf09bcc599081bb0d71d3773414d09` |
| `python/presentation/research_snapshot_client.py` | `2a842d2884dd347ff01a9b668400a4c3357ae3d1087382fde6eb336524ebccf7` |
| `python/presentation/test_research_snapshot_client.py` | `e9dcebe5b35074a87a6f23f37cc1f3111ee652b3f7ecb6eea3424bd9554dd73d` |
| `tests/AIQuantTradingResearch.Application.Tests/ResearchSnapshotTransformerTests.cs` | `701c818ee3b5a79a6ea51127357def40dcd2580e35112507ea713e4d9d9c6de1` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs` | `ebdacefd874b6a27c08185247eecbdf4e5ddf730636efa65937c388e3ab5fa0b` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/research_snapshot_reference.py` | `b88554f0d3337d2aae9aadaaf60842ffb580295ee690466a44c101cf9c37d7dd` |
| `tests/runtime/release_1_15/research_snapshot_contract_vectors.json` | `963e15e570411b4993d1ca1ec24b457aa8dd495035ff5237c1f61e7196a5142a` |
| `container/release_1_15/research-api.Dockerfile` | `ad2f140ed3f46bd5b4b67f84c27f4013c02c657193a64d8c5cd452fc39bd1a8d` |
| `container/release_1_15/snapshot-client.Dockerfile` | `86aaad8fd2245eb8fe54fb5ce985bd779497c428804e30be38f4dc0f59c60d93` |
| `eng/docker/release-1.15/Invoke-Wp05SnapshotQualification.ps1` | `3a43c2e1d5b6d37312881f7303124f43d451ca1d0cd9f5ed5788be900c552edc` |

### Complete owner checkout preservation and prompt provenance

Original branch feat/release-1.15-wp02-runtime-isolation at6f76697707e2363cc52820e0c805172815b92159. Full before/after status and all15 dirty/untracked file sizes/SHA-256 matched after implementation. No original was staged, overwritten, renamed, reset, cleaned or stashed. This includes twelve untracked prompts; existing archive dispositions in prior sections remain unchanged. WP03 simple, WP04 simple, WP05 real-snapshot and the new WP05 after-PR391 prompt remain original/pending separate new archive-path authority; no archive was created or overwritten.

| Original relative path | Bytes | Before = after SHA-256 |
| --- | --- | --- |
| `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md` | 5865 | `eaf83cb62981f8a37c84fbea68930c383bbddfe1b23ebdb7c782480d457bcbf3` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_EVIDENCE.md` | 3858 | `1534c46fad27e485566fe7b3ba50dbefef4b4b20dd546dfb1fa96a14b2c2b579` |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md` | 11665 | `cd21cdbcc176d206da8ccbc73876b63d9e8d27358fd5a463448285351f761dbc` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-azure-existing-f1-plans-additional-apps.md` | 6991 | `fc1cae2eb9c524bd9659edd2477629f315a8bb5caa6cb585beea32d0af215184` |
| `docs/roadmap/release-1.15/prompters/release-1.15-codex-create-two-existing-plan-f1-apps.md` | 7888 | `5b6a452d1c82e155e3df129f8719cba5ac1b872af092753e319b0d2c558e0f71` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md` | 10144 | `a670a7e32627c13dc45cc8c17a386af39e94ce33dbe4ef6f505760ef22b18548` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-dual-docker-api-key-codex-prompt.md` | 6619 | `35097506ecd266af3c4f543c7936b1375224925e05a9c5084ae45cdf63b3ffb0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-pr384-merge-and-next-preflight-codex.md` | 6183 | `32249579c08b2d49a81edfe09640ad2b8e49a726c4cf06e73070a05f13b42fc8` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-simplified-lab-hardening-codex.md` | 6609 | `f28dd004399d5649c1822c6e8ffb4bf9e85038b3320c1c800ea2c0b2d01608b0` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-simple-implementation-codex.md` | 7920 | `08fd7d1fadd5e38e4d413c5e6b972d54ff3e6801fb67668e92b750e2625c7a7c` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp03-wp04-foundation-codex.md` | 7870 | `9466670fa4c172e0d1481525956df823c30156d5db8b1e3247f96f4533087c43` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp04-simple-research-implementation-codex.md` | 8128 | `f3fff48a3254c2836c3d2b9acd4d895aa9328fda82df105d44a14ef15d9e0b18` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp05-real-snapshot-implementation-codex.md` | 8976 | `08dfbe33dc10fc2309133bde544cc233c7fe292e70575fe06c532199d5e1384d` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp05-snapshot-after-pr391-codex.md` | 6805 | `e53dafb80a4697b2947cfc654e13ee7cd16006608e7c290b9da2e27c201bcaf9` |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp05-staging-only-integration-codex.md` | 10602 | `e08e63e06d64ec9fcb061c8781d8e238b5ba26124740a16abeaef49dbf3936e0` |

### Remaining obligations and review disposition

Functional offline vertical slice complete for owner review, not WP05 acceptance. Leave one implementation PR OPEN/UNMERGED and issue371 open. WP06 still owns real Streamlit/UI scheduling/render integration; the client driver is not that application. Future explicit runtime provisioning, deployment configuration/HTTPS ingress, dual Linux image build/mount/process/resource behavior, durable restart/power-loss and staging qualification remain separate. Existing WP03 exclusive-writer and best-effort coordinated-rollback limits still apply.

Nominal Python timeout is5s and server processing budget4s. Direct synchronous stdlib DNS/compound header operations are not claimed to have a hard total wall-clock bound; no resolver worker, subprocess cancellation or new cancellation framework is introduced. Socket phase timeouts and monotonic checks prevent accepting a late result, but adversarial slow-header wall-clock behavior has not been qualified. This availability limitation is distinct from whole-response atomic/fail-closed validation. Production remains strictly untouched; no staging or public activation. PUBLIC_RUNTIME_VERIFIED=False.

WP05 publication validation: exact14 implementation paths plus2 approved existing append-only docs; all source SHA-256 values verified;33 relative Markdown targets/anchors PASS; UTF-8/no BOM/LF/final newline and git diff --check PASS. Original15-file preservation inventory includes every dirty/untracked prompt; no generated build output or new archive is included.


## WP07 actual-browser / Linux qualification attempt (2026-10-24)

**Disposition: INCOMPLETE / NOT ACCEPTED. PUBLIC_RUNTIME_VERIFIED=False.**
No issue is closed. No provider, Azure, production, image publication or deployment operation was performed.
Qualification branch starts at merged PR #393 (`3a068341509c78af548b734ace1de737cc8dc613`).
The original dirty checkout remains on its original HEAD; all 17 dirty/untracked files (14 owner prompts) retain their byte sizes and SHA-256. Open #386/#387 were context only.

### Approved implementation inventory

- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: opt-in bounded coordinator using an exclusively owned temporary WP03 SQLite store, committed/calculated 401 rows per instrument, witnessed D5 tuple, localhost test CA and the actual C# HTTPS endpoint. The test service key and D5 key remain test-only and distinct.
- `tests/runtime/release_1_15/wp07_browser_qualification.py`: actual installed Chrome DevTools automation using existing websockets; real Streamlit entrypoint, UI server-side CA-validated HTTPS, isolated Chrome profile, loopback listeners, owned-process cleanup, outside-repository result/screenshot artifacts. No browser framework install or production source change.
- This existing evidence document: append-only results.

Streamlit 1.61.1, Plotly 7.1.0, Python 3.13.15, websockets 16.1.1, Chrome 155.0.8059.86; Windows host, not Linux qualification. The explicit browser test is opt-in through AIQ_WP07_BROWSER_PYTHON; an ordinary run without that variable does not execute browser qualification and must not be cited as browser PASS.

### Actual evidence and acceptance matrix

| Area / obligation | Result | Evidence / limit |
|---|---|---|
| WP06 BTC/ETH real browser | PASS, bounded smoke | Both actual graphs have 401 candlesticks, 401 volume values and the C#-supplied SMA20 trace; instrument switch changes chart title/volume units. Constant deterministic fixture, not provider observations. |
| Transformed disclosure / UTC / hover | PASS, observed | Page disclosure, transformed chart/trace labels, UTC axes; pointer inspection captured transformed OHLC hover with UTC time. |
| Presets 100 / 300 / 1000 | PASS | Actual supported selectbox interactions; visible spans 100 / 300 / 401 minutes respectively. The 1000 preset cannot invent rows beyond the fixture's retained 401. |
| RSI14 / MACD panels | PASS | Actual popover/selectbox interactions; resultant graph trace names verified, not inferred from clicks. |
| Modebar / zoom / refresh retention | PASS, bounded smoke | Actual Plotly modebar and pointer drag. Both x and y ranges remained equal after Refresh. Pan-specific gesture NOT RUN. |
| Return to Live | **FAIL, reproduced twice** | Page reports Live follow after button activation but actual graph remains zoomed. No production repair authorized in the initial three-path manifest. |
| Narrow layout | PASS in initial smoke only | 390px emulated viewport; actual chart width 348px. Screenshot hash below. Extended run stops at Return to Live, before repeating this case. |
| Correction / epoch / date / generation, gaps, optional markers, disconnect/reconnect | NOT RUN | Requires extending the disposable test scenario; no claims from unit tests alone. |
| Per-session/instrument viewport isolation, repeated disposal/resource stability | NOT RUN | Instrument switching alone does not establish viewport isolation or absence of leaks. |
| WP02 / WP07 Linux dual-container | **NOT RUN / BLOCKED** | Local Docker desktop-linux daemon 29.7.2 available, exact pinned SDK/ASPNET/Python bases absent. No pulls, substitutions, builds or container starts. Existing snapshot-client is a driver, not the real Streamlit UI. |
| WP03 operational foundation | Offline subset PASS | 43 Application + 83 Infrastructure tests; no provider polling/quota, restart-durable credit accounting, Linux mount/power-loss or production ingestion proof. |
| WP05/WP06 focused Python | PASS | 51 tests. Does not waive browser Return to Live failure. |
| Legacy Release 1.14 hover | Baseline FAIL | 8 tests: 7 pass, 1 fails expecting Volume versus Simulated activity. Unrelated baseline test/source left unchanged. |

### Concrete viewport failure

Before pointer zoom, ETH/USD x was `2026-10-01T01:41:00Z` to `2026-10-01T06:41:00Z`, y was `[90.58462928981888, 92.6202389367811]`.
After pointer zoom, x was `2026-10-01 02:55:39.3893` to `2026-10-01 05:25:53.1298`, y was `[91.0940119676723, 92.11673692744638]`.
Refresh preserved those exact zoomed x/y ranges. Return to Live then left those same graph ranges in place for the bounded 45-second wait, although the page caption said Live follow. The repeat captured the same outcome. This is a failed functional check, not an accepted UX limitation.

Initial successful subset: one opt-in xUnit coordinator PASS (9-second test); extended qualification: one coordinator FAIL (about 65 seconds), with the above earlier individual browser assertions passing. Early harness iterations also failed on selectbox markup/scroll timing; these were automation failures, not application defects. The final harness retains the failing Return to Live assertion rather than suppressing it.
Initial narrow-screen PNG SHA-256: `14a14b602ae3bbdb6993c8c4af33d921c107718dcbf68677c153161e8748b337`. Artifacts remain in task-owned temporary storage; no personal local paths, credentials or private databases are published.

### Reproduction (Windows PowerShell 5.1; existing offline dependencies)

Run from this qualification branch, with existing restored build assets; no restore/install:

```powershell
$env:AIQ_WP07_BROWSER_PYTHON = '<existing-venv>\Scripts\python.exe'
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter FullyQualifiedName~OwnedBrowserQualificationAgainstRealHttpsSnapshot --logger 'console;verbosity=detailed'
$env:PYTHONPATH = (Join-Path (Get-Location) 'python\presentation')
& $env:AIQ_WP07_BROWSER_PYTHON -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client -v
& $env:AIQ_WP07_BROWSER_PYTHON -B -m unittest python.presentation.test_market_research_ui -v
```

The focused Application filter was `FullyQualifiedName~ResearchIngestionServiceTests|FullyQualifiedName~PrivateResearchCalculatorTests` (43/43). The focused Infrastructure filter was `FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchStateRecoveryTests|FullyQualifiedName~CommittedResearchProjectionTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests` (83/83), both `dotnet test <respective-project> --no-restore --filter <filter>`. Existing unrelated analyzer warnings remain; no new dependencies.

### Remaining acceptance blockers

1. Repair and requalify Return to Live only after literal-path application/test approval; no silent waiver.
2. Complete the NOT RUN browser scenarios with bounded disposable fixtures and session/resource observations.
3. Make the exact pinned Linux bases and offline Linux publish artifacts available under separate authority; qualify real UI packaging in explicitly approved paths, then execute distinct non-root single-process containers and TLS negative/restart/witness/mount/resource tests. Do not replace absent images or cite Windows tests as Linux proof.
4. Provider rate/credit/polling evidence, native Linux durability limitations (including no demonstrated directory-fsync guarantee), restart witness recovery and coordinated rollback limits remain distinct from successful offline tests.

WP02 #368, WP03 #369, WP06 #372 and WP07 #373 remain unaccepted for their outstanding obligations. Public activation and production deployment are not authorized by this qualification.


### WP07 owner-authorized Return to Live correction (2026-10-24)

This addendum supersedes the preceding Return to Live FAIL disposition for the corrected head only; it preserves the failed baseline evidence. The owner approved three potential repair paths. Only two needed edits:

- `python/presentation/research_chart.py`: one-line change adding the existing reset epoch to Plotly's `uirevision`. An explicit Return to Live or preset reset now invalidates Plotly's retained user ranges. Ordinary refresh and manual exploration retain the same UI revision. No new protocol, component mechanism, indicator computation or dependency.
- `python/presentation/test_research_chart.py`: regression test proves refresh retains the UI revision, live reset changes it and clears saved ranges, and the live range/data match the initial figure. `research_chart_component.js` required no change.

The unchanged real-browser regression now **PASSes** (one opt-in xUnit test, exit 0, 27.1991 seconds): actual ETH pointer zoom changes x/y, Refresh retains them, and Return to Live restores x to `[2026-10-01T01:41:00Z, 2026-10-01T06:41:00Z]` and y to `[90.58462928981888, 92.6202389367811]`. These are the exact initial ranges, not merely a changed caption. BTC/ETH, all presets, RSI14/MACD, disclosure and narrow-layout assertions also pass in this run. Narrow PNG SHA-256: `59f27a7107542b07fdf8445e0ae8b4e46d833dc7fe0c2cf53c62305f61e7ea7c`.

Focused command `python -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client -v`: **52/52 PASS**, exit 0 (including the new regression). Browser command and prerequisites remain as above. No broad .NET rerun was needed for a Python figure reset change; the earlier 43/83 focused results are unchanged historical evidence.

**WP07 remains INCOMPLETE**: all earlier NOT RUN browser scenarios and pinned Linux-base/container blockers remain. The browser harness reports PARTIAL rather than overall qualification PASS. No production readiness, Azure activation, provider ingestion or release acceptance is inferred. `PUBLIC_RUNTIME_VERIFIED=False`; PR remains OPEN/UNMERGED.


### WP07 qualification continuation: final bounded browser evidence (2026-10-25)

**Owner review disposition: expanded Windows/local browser qualification executed; WP07 overall remains PARTIAL, not accepted. PUBLIC_RUNTIME_VERIFIED=False.** This addendum supersedes the earlier NOT RUN browser rows only for the bounded scenarios below; it does not erase failed attempts or establish Linux/Azure qualification. PR #394 was verified draft/open at `db34c2395017af11003b2e48a0da2c22a9b8afad`, base `3a068341509c78af548b734ace1de737cc8dc613`, before continuation. No competing PR, merge, issue closure, provider call, install, image pull, Azure or deployment operation.

Only the existing C# coordinator, Python browser harness and this append-only evidence document change in this continuation. The already-approved one-line application reset fix and its regression file are unchanged. The total PR inventory remains its five approved paths.

#### Real stack and fixture changes

The opt-in coordinator runs actual Chrome/CDP -> actual Streamlit entrypoint -> UI server-side CA-validated HTTPS -> actual C# snapshot endpoint -> exclusively owned, committed/calculated SQLite WP03/WP04/D5 fixtures. There are initially 401 completed deterministic candles per instrument, no provider observations or acquisition. The test CA/control files now occupy a separate owned directory: placing them in the store directory correctly triggered its file-allowlist protection in an early fixture attempt. Production storage guards were preserved.

Private atomic command/ack files, visible only to the test coordinator/harness, request actual committed correction, UTC-day advancement, transform activation and switching to a separately preprovisioned database generation. These are not application/admin HTTP routes. For outage/authentication tests the same owned loopback TLS endpoint role stops/restarts; for malformed response only, that test role temporarily emits an invalid JSON body over its trusted TLS connection, then returns to the actual endpoint. No test command or fixture enters production code.

Environment remains Windows, Chrome 155.0.8059.86, Python 3.13.15, Streamlit 1.61.1, Plotly 7.1.0 and existing websockets 16.1.1. No dependency changes. Browser uses a fresh owned profile and a second isolated browser context; no attachment to user browsers. Control interaction uses actual pointer events/public accessible elements; delayed popup layout requires a fresh coordinate measurement after scrolling. Final assertions read resulting Plotly state, not clicks alone.

#### Final actual-browser matrix

| Scenario | Result | Concrete bounded evidence |
|---|---|---|
| BTC/ETH, supplied analytics, disclosure, controls | PASS | Actual candlesticks/volume/SMA20, instrument identities, 100/300/1000 presets and RSI14/MACD traces. All original 401 candles stay present; 1000 clips its visible span to available history. |
| Zoom, explicit Pan modebar tool, Refresh, Return to Live | PASS | Separate pointer zoom and pan; x/y stay identical across an actual Refresh button operation. Explicit live reset restores the initial x/y; existing reset regression retained. |
| Genuine nonempty transition markers | PASS | Toggle before correction verifies a nonempty Trend transition trace, not merely an empty named trace. |
| Gap / null overlays / continuity markers | PASS | Commit adds minute 405 after minute 400: exactly 402 actual rows, last times 06:40 and 06:45 UTC, gap rectangle, null latest warmup overlay and connectgaps=false; marker control produces trace state. No invented candles fill the four missing minutes. |
| Committed correction | PASS | Correct last retained close through WP03 Commit; browser version changes and plotted closes differ. UTC live window remains equal. |
| UTC day and transform epoch | PASS | Actual source clock advances Oct 23 to Oct 24; actual witnessed D5 epoch activates 1 -> 2. Each accepted public identity/value set changes; UTC live window remains equal. |
| Generation replacement | PASS | Switch reader to a separately preprovisioned committed database: new generation accepted, old 402-row/gap view replaced by 401 rows and different closes. |
| Disconnect, wrong-key, malformed body | PASS | Each actual transport failure shows Disconnected / last-known history and preserves plotted closes. Restart actual authenticated endpoint and verify Connected recovery. |
| Two independent browser sessions | PASS | Fresh context starts BTC independently; changing second session preset and RSI panel leaves first viewport/panel unchanged. BTC/ETH switches in the first leave the second BTC/RSI state intact and restore the first ETH UTC range. |
| Stale intent and disposal/remount | PASS, bounded | Capture genuine pending pan intent before debounce clears it; replay after explicit reset. Three RSI/MACD/None remounts cannot overwrite the latest live UTC x range. This is not exhaustive arbitrary-callback or adversarial race proof. |
| Real user-enabled 60-second refresh | PASS | Checkbox selected state verified; actual last-successful-sync caption changes after **59.1883 seconds** of observation (after activation/settling). Timer was not shortened. |
| Narrow layout and visible-secret negative | PASS | 390px browser viewport, actual graph <=390px; screenshot visually shows stacked controls/disclosure. Screenshot is the above-chart viewport; chart width is separately read from the real graph. Fake service key absent from rendered text/graph serialization. |
| Hover | OBSERVED | Earlier actual pointer run captured transformed OHLC hover and UTC text in the rendered page. The final clean harness does not contain a dedicated hover-popup assertion; do not label that as a separate automated PASS. |
| Repeated refresh/resource behavior | OBSERVED, bounded | Eight post-GC refresh samples; details below. Not long-run or server/F1 capacity proof. |

The final explicit opt-in xUnit browser coordinator **passed 1/1, exit 0**, total **1.9766 minutes** (~118.6 seconds). Its report deliberately remains PARTIAL because the whole WP07/Linux gate is not complete. No final case is silently waived. Earlier popup/control timing failures, one intentionally preserved missing-intent NOT RUN, and an abnormal ~12.9-hour interrupted browser connection run remain failed/limited attempts, not clean bounded passes. The final rerun captures the stale intent promptly and verifies selected control states. The initial ambiguous Refresh prefix could hit the auto-refresh checkbox; final exact-label-first matching plus actual committed mutations supersede earlier refresh-only assertions.

#### Resource / artifact observations

Eight samples after CDP-requested garbage collection each have **2 documents, 2,076 DOM nodes and 441 listeners**. Used JS heap ranges **25,283,336 to 25,612,476 bytes** (~0.33 MB growth); this is a short observation, not proof of no heap leak. The graph's last Plotly redraw measurement stays **30.6 ms** throughout unchanged refreshes; that is not full request latency or CPU accounting. Browser GC sampling distinguishes uncollected transient listeners seen in earlier samples from retained live objects. Native timer enumeration, long-run server memory/CPU and 1/3/5 session capacity are NOT RUN.

Final result JSON SHA-256: `de819c70ccedc19e17b0f60aeee9b3c153c86ac4cda6009883017177f6f661f6`.
Final narrow-controls PNG SHA-256: `ed10852aa06acc71a4fa4ab9ecfba198938f39a58619bcba9752d90b4de4caf7`.
Artifacts remain in owned temporary storage; no personal evidence-root paths, private databases, keys or host identifiers are committed/published.

#### Focused regression commands and results

From this isolated checkout, existing offline build assets and virtualenv only:

```powershell
$env:AIQ_WP07_BROWSER_PYTHON = '<existing-venv>\Scripts\python.exe'
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter FullyQualifiedName~OwnedBrowserQualificationAgainstRealHttpsSnapshot --logger 'console;verbosity=detailed'
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --no-restore --filter 'FullyQualifiedName~ResearchSnapshotEndpointTests&FullyQualifiedName!~OwnedBrowserQualificationAgainstRealHttpsSnapshot' --logger 'console;verbosity=minimal'
$env:PYTHONPATH = (Join-Path (Get-Location) 'python\presentation')
& $env:AIQ_WP07_BROWSER_PYTHON -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client -v
& $env:AIQ_WP07_BROWSER_PYTHON -B -m unittest python.presentation.test_market_research_ui -v
```

The initial final build uses --no-restore; later final browser reruns use --no-build --no-restore. The browser opt-in variable was actually set; ordinary opt-out execution is not counted. Focused Application filter from the preceding section: **43/43 PASS**; focused Infrastructure filter: **83/83 PASS**; existing endpoint negatives/integration: **8/8 PASS**; focused Python: **52/52 PASS**. Legacy synthetic UI rerun: **7/8 PASS, one unchanged Volume vs Simulated activity assertion FAIL**. No baseline source/test edits. Existing analyzer warnings remain distinct from qualification failures.

#### Linux blocker and minimal separately reviewable proposal

Read-only inventory reconfirmed local Docker desktop-linux / engine 29.7.2, linux platform. All exact pinned bases are missing:

- ASP.NET: `mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f`.
- Python: `python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`.
- SDK: `mcr.microsoft.com/dotnet/sdk:10.0@sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc`.

No pulls, image substitutions, builds, container starts or shared-F1 conclusions. No qualified offline Linux publish/private closed DB+SAN-correct TLS input pairing is established. Existing `container/release_1_15/ui.Dockerfile` still starts the WP02 fixture app, and snapshot-client is a test driver. Neither is claimed as final chart UI qualification.

Smallest follow-up proposal, **NOT implemented or authorized by this evidence**: (1) new `container/release_1_15/research-chart.Dockerfile`, using the accepted pinned base/qualified dependency context and actual merged presentation assets, one non-root Streamlit process, no private filesystem mount; (2) extend existing `eng/docker/release-1.15/Invoke-Wp05SnapshotQualification.ps1` to pair actual research and chart roles on an owned network with explicit fixture inputs; (3) if required, extend the already-approved C# test coordinator for an explicitly requested closed, witnessed disposable fixture export outside the repository. Obtain separate literal-path/fixture-input approval before any such change. First make exact cached bases and offline Linux publish inputs available under separate authority. Then test non-root roles, independent processes, TLS/hostname/key negatives, mount/WAL/exclusive writer/restart witness and CPU/memory/startup/latency. No substitute architecture or deployment proposed.

#### Remaining release disposition and preservation

| Work package | Supported here | Still unqualified |
|---|---|---|
| WP02 #368 | Host-local server-side authenticated HTTPS and fault/reconnect behavior | Actual separate non-root Linux chart/research containers; mount/lifecycle/resources; Azure/F1 separately |
| WP03 #369 | Offline commit/correction/gap/generation fixtures, recovery/integrity suites | Provider acquisition/quota, restart-durable credits, native Linux/Azure filesystem and power-loss semantics, cross-instance exclusion |
| WP06 #372 | Expanded real-browser Windows chart functionality, bounded session/remount cases | Dedicated final hover assertion, exhaustive race/long-run stability/capacity, Linux image/browser pairing |
| WP07 #373 | Reproducible bounded real-browser result and explicit regression evidence | Linux prerequisites/qualification, native resource/storage/transport evidence and separately authorized staging |

Original checkout HEAD/status and byte sizes/SHA-256 of **all 18 dirty/untracked owner files**, including prompts, are rechecked unchanged. Only isolated qualification worktree files are committed. UTF-8/no BOM/LF/final newline checks and git diff --check are mandatory for all five PR text paths. PR stays **draft/open/unmerged**; no issue acceptance or closure. `PUBLIC_RUNTIME_VERIFIED=False`.

Source SHA-256 for the final executable qualification revision:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: `680c88fa83b9921501dd714e14df5ea339c489120c029a954843265d766c92ee`.
- `tests/runtime/release_1_15/wp07_browser_qualification.py`: `8035b502ac955eaf6f0d7fbc9ff04b83dfdd529443adbd80e8c5da16e4bb9df4`.
- `python/presentation/research_chart.py`: `c5abd2bfea8f4d0dae7623a2e69436a4169578f012b9bbee8dd57ce6dafe87ea`.
- `python/presentation/test_research_chart.py`: `db79cd8951b7730828da93e61a6f097730aaa252bc037f10532131ac0f7a5b50`.


## WP07 real Linux dual-container integration (2026-10-25)

**Local outcome: PASS_LOCAL_ONLY. Release/WP acceptance and staging remain unverified; PUBLIC_RUNTIME_VERIFIED=False.** This new implementation starts from exact merged PR #394/main `6d16a41edbd4a0a576f0d87cd62b0677f2218ccf`. It supersedes the prior missing-runtime-base blocker only for this local qualified pair. It does not change historical Release 1.14 evidence or retroactively convert earlier NOT RUN cases to executed cases.

### Scope, environment and executed inputs

The owner directly approved five literal paths and pulls of the exact pinned bases. Only these paths change:

1. New `container/release_1_15/research-chart.Dockerfile`: real Streamlit entrypoint and its explicit public-module/component/local Plotly dependencies; one non-root principal process.
2. New `eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1`: Windows PowerShell 5.1 wrapper, existing Python standard-library orchestration, allowlisted build context, bounded/owner-labeled Docker operations, fixture volumes, actual HTTPS/browser/lifecycle checks and cleanup. Default is read-only Docker/input preflight; mutation needs explicit -Execute.
3. Existing `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: explicit opt-in test-only closed fixture/TLS export; no production provisioning surface.
4. Existing `tests/runtime/release_1_15/wp07_browser_qualification.py`: separate actual-container UI mode using existing Chrome/CDP, preserving the merged Windows scenario. Captures actual page requests and rejects external CDN/browser-to-upstream HTTP requests.
5. This existing foundation evidence document: append only.

No application contracts, dependencies, project files, existing Dockerfiles, startup/DI, provider adapters, Azure, registry push, deployment, issue closure or merge changes. PR #386/#387 were not imported. Original protected checkout HEAD/status and bytes/hashes of all **19** dirty/untracked owner files, including prompts, are preserved. All work is in an isolated worktree.

Docker Desktop local named-pipe context `desktop-linux`, engine 29.7.2, linux/amd64. Explicit approved runtime pulls:

- `mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f`
- `python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`

The SDK image was **not pulled**: an existing-asset `dotnet publish --no-restore --no-self-contained -p:UseAppHost=false` produced portable framework-dependent IL plus cached Linux x64 SQLite native assets, subsequently executed by the actual Linux runtime. No package restore/install/upgrade. Qualified cached dependency image ID remains `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`; only its /opt/venv contents enter the new UI image. Both Docker builds use --platform linux/amd64, --pull=false and --network=none. Context contains copied publish/public source only, never fixture DBs, HMAC material, service.key or TLS private keys.

Actual retained-image runtimes: Microsoft.NETCore.App and Microsoft.AspNetCore.App **10.0.13**; CPython **3.13.16**, Streamlit **1.61.1**, Plotly Python **7.1.0**. These Linux patch versions come from the repository's exact existing pins, not package upgrades. Browser observes bundled Plotly.js **4.1.1**, served through supported components-v2/local assets, and installed host Chrome **155.0.8059.86**. The runner targets PowerShell 5.1 (including requested 5.1.26100.9444 syntax); actual available execution engine is **5.1.26100.9711**, so exact .9444 patch execution is not claimed.

### Actual fixture and isolation model

The explicit export test commits actual WP03/WP04 state and activates D5 through its real private store. Both instruments are checked Unavailable at candle 400 and Valid at 401, then committed past retention with one missing minute; final retained count is exactly 1,000 each and the latest continuity has enough observations for valid research. These are deterministic fake completed candles, **not provider observations**. The export closes the writer before Docker copies. A random test-only service key is separate from test-only D5 material; no real credential is acquired or used.

Named volumes hold research data and TLS material. Docker daemon copies populate an owned data/ directory as UID/GID1654, mode0700; files0600. Research uses /private/data, its own writable volume and a separate read-only TLS volume. The stopped population container is removed and recreated with read-only TLS **before any application startup**; no privileged/root runtime helper is launched. UI mounts only public ca.pem read-only, never research data/PFX/provider/HMAC material. Root filesystems are read-only; tmpfs, cap-drop=ALL, no-new-privileges, memory and PID limits are explicit.

Research is on an internal Docker network with no host publication. UI joins that network for HTTPS hostname `wp07-research` and a separate owned bridge for **127.0.0.1-only ephemeral UI publication**. This second bridge is local test ingress, not Azure ingress evidence. Research container loopback is not used by UI. API key is passed by environment variable name (not a literal command argument) and used only by server-side HTTPS. CA+hostname validation is active. No assertions about Azure private networking/F1 capabilities follow.

### Executed qualification matrix

| Case | Result | Evidence and limit |
|---|---|---|
| Two actual Linux images | PASS | linux/amd64, research UID/GID1654 and UI12002. docker top shows one principal application process each; stats PIDs include threads, not extra application roles. |
| Native private SQLite and file protection | PASS, local | Actual native Linux SQLite opens and runtime enforces WAL mode; /private/data 1654:1654 0700, DB/watermark0600. UI test confirms no /private or /tls/server.pfx. |
| Actual transformed full/unchanged | PASS | Strict client inside UI validates 1,000 BTC and ETH rows, valid latest classification, supplied SMA, gap metadata, transformed values and unchanged identity. Client UID12002; no provider/HMAC environment. |
| HTTPS/authentication negatives | PASS | Missing/wrong key401, wrong hostname rejection, untrusted CA rejection. No TLS bypass. Research has no published host port. |
| Actual container browser | PASS | Chrome against real Streamlit image: BTC/ETH components, 1,000-retention/gap, supplied RSI, transformed disclosure, no visible service key, supported v2 and local Plotly asset loading. No Windows replacement backend/UI. |
| Independent research outage / UI continuity | PASS | Browser requests stop/start through owned test control files: UI process stays alive, shows disconnected last-good values, then reconnects with the same validated closes. No admin route added. |
| Graceful restart and witnessed persistence | PASS | Same persisted identities/rows/closes after browser-directed stop/start; real store startup validates its recovery witness. |
| Abrupt process restart | PASS, quiescent | SIGKILL then start research; full/unchanged identities and data match. This is **not** interrupted-write transaction atomicity or host power-loss proof. |
| Same-volume competing writer | PASS, local | Second non-root research process on the same owned volume exits1/fail-closed while original remains healthy. Does not prove cross-host Azure mount leases. |
| Logs / private boundary | PASS, bounded | Service key absent from captured app logs, rendered page/graph; no raw or key files enter UI build context/mounts. Tests use fake secrets only. |
| Resource/startup/latency | OBSERVED | Actual samples below, not F1 benchmarks or service guarantees. |
| Cleanup | PASS final run | Owned containers, networks, volumes and temporary dependency tag removed; qualified final two image tags retained. Earlier attempt images removed by exact labels, no prune/forced unrelated deletion. |
| Interrupted-write WAL atomicity, host power loss, Azure persistent mounts, shared-F1 1/3/5-session capacity | NOT RUN | Remain separate qualification work; Windows/offline fault tests do not prove these native/cloud conditions. |

### Exact retained pair and artifacts

These are **local content-addressed image/config IDs**, not pushed registry RepoDigests or deployment/promotion identifiers. Docker reported matching local descriptors; no registry publication took place.

- Research: `sha256:acf31c4363ba4ca9d0d90451a4ce8805218ef1cc94bce25ff683293fd33db3dd`
- UI: `sha256:27dc8960609c3fbd5cb239aead8221967abaf0ff67f8793679abfc3aba2b5550`
- Portable publish manifest SHA-256: `94545ea795181bd513978a1bc85f88ee918eeb467135612051d3cad6344ea4ac`
- Final local result JSON SHA-256: `9bc11e288968d4d1650fd0c816897f6c0da7f3674be86238ffe57f6471a53b6f`
- Actual Linux UI chart PNG SHA-256: `f238bb8f8a81ce01b5e5d802372c9cb58dab041c5ea9aec38bb3e6a4e099bd84`

The PNG was visually inspected: the constant educational fixture's dense candles and volume render in the actual Plotly component. It is a fixture screenshot, not a real market chart or visual-production acceptance. Private DBs/keys and personal evidence-root paths are not committed or published. Raw owned logs/artifacts remain local.

### Actual measurements and interpretation

Final start-to-completed-initial-probe: **6.0388 seconds**, including TLS/client validation, both full/unchanged instruments and negative probes; this is not an isolated HTTP-ready or usable-chart latency. Initial full roundtrip+validation: BTC **0.9710s**, ETH **0.3788s**; unchanged: BTC **0.2346s**, ETH **0.1818s**. One sample per operation, not percentiles or a CI/F1 SLA.

Final docker stats after restart/contender work: research **142.5 MiB /256 MiB, 64.72% CPU,17 task/thread PIDs**; UI **168.2 MiB /512 MiB,1.40% CPU,13 task/thread PIDs**. Earlier smaller samples are not substituted for these final measurements. Host regressions also ran during this session; these are local bounded observations, not isolated steady-state benchmarks or shared Azure F1 headroom proof. Browser executes on the Windows client against Linux services.

### Reproduction and focused regressions

Use existing offline-restored project assets and installed virtualenv. Fixture output must be a fresh, nonexisting owned path under TEMP; never reuse owner/private production data. Example PowerShell5.1 commands:

```powershell
$publish = Join-Path $env:TEMP 'aiq-wp07-linux-publish'
dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-restore --no-self-contained -p:UseAppHost=false -c Release -o $publish
$env:AIQ_WP07_EXPORT = Join-Path $env:TEMP ('aiq-wp07-linux-fixture-'+[guid]::NewGuid().ToString('N'))
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore --filter FullyQualifiedName~ExportOwnedLinuxQualificationFixture --logger 'console;verbosity=normal'
./eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1 -PublishedResearchDirectory $publish -FixtureDirectory $env:AIQ_WP07_EXPORT -Python '<existing-venv>\Scripts\python.exe' -Execute -KeepImages
```

Preflight without -Execute checks local daemon, exact cached bases and closed input files only. Runner does not pull images, restore packages, provision runtime data or deploy. Required bases must be present under explicit pull authority. KeepImages retains only the run's image tags; all other owned resources are reconciled. On timeouts it terminates only its still-running owned browser process tree; cleanup failures are reported as FAIL_CLEANUP, not PASS.

Executed results: **1 fixture-export test**, **43 Application**, **83 Infrastructure**, **8 endpoint** and **52 Python** tests PASS; merged explicit Windows browser coordinator **1/1 PASS**, exit0, **2.2234 minutes**; actual Linux runner **PASS_LOCAL_ONLY**, exit0, including seven browser cases and native lifecycle/negative checks. Focused filters and no-restore commands remain the preceding WP07 section's exact commands; fixture-export invocation is above. Legacy synthetic UI rerun remains **7/8 PASS, one pre-existing Volume vs Simulated activity hover assertion FAIL**, unchanged test/source. No opt-out browser execution is counted as a browser pass.

Failed attempts remain evidence: copying into an already read-only TLS mount, initial probe search path, root-owned volume root, and internal-only network lacking a published host port were test/setup failures corrected inside the approved scope. One cleanup inspection timeout left an owned stopped research container and two volumes; exact-label retry removed all three. One automatic approval-review timeout prevented a focused-test launch; its single authorized retry executed successfully. No gate bypass or fabricated result. Existing analyzer warnings remain separate from test failures.

### Remaining disposition

WP02 #368 and WP07 #373 now have actual local Linux role/HTTPS/browser/restart evidence; WP03 #369 has native same-host writer exclusion and quiescent persistence evidence; WP06 #372 has actual-container chart smoke plus preserved merged Windows coverage. None is accepted/closed here. Pending: interruption during an actual native write, host/Azure filesystem durability and rollout overlap, prolonged/concurrent load, exact .9444 PowerShell patch execution, and independently authorized staging/F1 verification. Retained images are local only; no Azure resources/settings, live provider, registry push, public activation, issue closure or merge. `PUBLIC_RUNTIME_VERIFIED=False`.

### Source SHA-256
- `container/release_1_15/research-chart.Dockerfile`: `c74bc6a5b1aebaf236d524246ab6983bdc2bf62bcfcceb14edaa98c709c921de`.
- `eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1`: `0be3606f67813c2b428b9910e1ce314f5e64318006083d066230b8ec725d6ca7`.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: `bf7379ea3c060931433932facc8b2f4cf8ef34efa697c4adfcdeb47958742f3a`.
- `tests/runtime/release_1_15/wp07_browser_qualification.py`: `a75a21755020039526000c7cc6bb19149fb5f486a568c8af3385e66db5bea233`.


## WP07 Azure staging readiness preparation (2026-10-25)

**Outcome: PASS_LOCAL_ONLY re-confirmed; Azure deployment BLOCKED / NOT RUN; PUBLIC_RUNTIME_VERIFIED=False.** Preparation starts from merged PR #395/main `71e4f8ccce877547d11c965ded92280fb02e65de`. No application or deployment implementation changes. No Azure mutation, registry push, credentials acquisition, provider call, production operation, issue closure or merge. PR #386/#387 were not imported.

### Exact paths and read-only reconciliation

Only this existing document (append only) and new [staging readiness operator runbook](prompters/release-1.15-wp07-azure-staging-readiness-runbook.md) change. The runbook contains the requirement/evidence/result/remediation matrix, runtime variables, ports/entrypoints, certificate/state prerequisites, ordered withheld deployment/validation/rollback checklist and minimum future authority. A new mutation-capable script is unnecessary. No additional licensing/policy gate is introduced. UTF-8/LF and separate CRLF verification apply.

Read canonical governance, execution plan, acceptance/file manifest, prior evidence, actual Dockerfiles and source; GitHub issues #368/#369/#372/#373 and #395 merge state were checked live. #395 is MERGED at the expected squash SHA. Original checkout remains on its existing dirty branch; isolated readiness branch starts at merged main. All 20 original modified/untracked owner files were inventoried by byte size and SHA-256 before work, with HEAD/status rechecked after work.

Azure CLI 2.89.1 used existing authentication for read-only projections of exactly the named staging apps/shared F1 plan. Confirmed both belong to `asp-aiq-r112-wp05-wus2-7f5eabb5`, SKU F1/Free capacity 1, Linux West US 2. Both report Running and Always On false; neither has a health-check path or startup override. These management observations are not runtime tests. The research shell has empty image assignment, no app settings or custom storage mounts, httpsOnly=false and publicNetworkAccess Enabled. UI retains its prior image assignment, port8501, storage=false and no custom mounts; legacy provider setting names remain, while Release 1.15 research client settings are absent. No secret setting value, registry credential, publishing profile or token was requested/output. Current rollback image namespace is deliberately omitted from public evidence.

Commands used: `gh pr view 395 --json state,mergedAt,mergeCommit`; `gh issue view <368|369|372|373> --json number,title,state,body`; `az appservice plan show`, `az webapp show`, `az webapp config show` with safe field projections; `az webapp config appsettings list` projected to names and three non-secret port/storage/startup fields only; `az webapp config storage-account list` projected to names. Initial Windows batch quoting of `keys(@)` failed; the Python CLI retry showed the response was an array, so the corrected `[].name` projection succeeded, returning [] for both apps. These failed query attempts made no mutations. All final required inventory queries exited0. CLI null plan metadata was retained as unknown, not spare capacity.

### Re-executed local qualification

Environment: Windows PowerShell **5.1.26100.9711** (compatible 5.1 syntax; requested patch 5.1.26100.9444 not separately available), Docker **29.7.2 linux/amd64**, existing Python environment, Streamlit **1.61.1**, Plotly Python **7.1.0**, actual browser Plotly.js **4.1.1**, Chrome **155.0.8059.86**. No installed dependencies, restores or pulls. Existing pinned cached runtime bases and qualified Python dependency image reused.

First script invocation was denied by process execution policy before any build/start; retry used process-only RemoteSigned, not a machine policy change. Successful commands (operator-owned private paths intentionally represented as variables):

```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process -Force
& ./eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1 `
  -PublishedResearchDirectory $PublishedResearchDirectory `
  -FixtureDirectory $FixtureDirectory -Python $ExistingPython -Execute -KeepImages
$env:PYTHONPATH='python/presentation'
& $ExistingPython -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client
```

- Docker runner exit **0**, **PASS_LOCAL_ONLY**. Actual Linux C# API and Streamlit pair; real browser BTC/ETH1000 rows, retention/gap, supplied RSI, outage/last-good/reconnect and no browser research/CDN requests PASS.
- API full/unchanged and missing/wrong service key, wrong hostname/untrusted CA rejection PASS. Private directory1654:1654 mode0700; DB/witness0600; UI no private mounts PASS. Graceful restart and SIGKILL at quiescence preserve identities/rows; same-volume second writer exits unsuccessfully; logs contain no fixture service key. No interrupted-write, power-loss or cross-host/Azure mount proof is implied.
- Python **52/52**, exit **0**, **17.851s**. Expected bare-mode Streamlit missing-ScriptRunContext warnings; no assertion failure. No broad .NET rerun or legacy hover test alteration. Prior #395 .NET results remain prior evidence, not newly executed counts.
- Existing portable publish reused, manifest SHA-256 `94545ea795181bd513978a1bc85f88ee918eeb467135612051d3cad6344ea4ac`; deterministic fixture/TLS files remained external to build contexts. No claim of fresh application compilation at this baseline.
- Startup to completed initial API/negative probe **6.380852s**, not isolated chart time or Azure cold-start guarantee. BTC full/unchanged **1.076409/0.354337s**; ETH **0.559888/0.199549s**. One stats sample: research **154.6MiB/256MiB**, **0.06% CPU**,16 tasks; UI **183.8MiB/512MiB**, **1.14% CPU**,13 tasks. Task counts include threads; each has one principal app process. Not peak measurements, F1 guarantees or concurrent-session qualification.
- All owned runtime containers/networks/volumes cleaned by the runner; image pair retained locally for review. No unrelated process/resource cleanup. Old qualified pair left untouched.

New **local image/config identities**, not pushed registry RepoDigests:

| Role | Image ID | User |
|---|---|---|
| Research | `sha256:aadc31bf0ef06d51b9d32c38cc0cf74ff0a7a32a2543b11d226a8cceabb35f62` | 1654:1654 |
| UI | `sha256:fc1c57f4bdb2fcb87de2c169aff60ab5f7ee657a8660041af7c3563ad3de9e42` | 12002:12002 |

Local result SHA-256: `497a4d3b907292f336c45b752c09d2de93559d09b5d20fc0decbe53788dd5040`.
Actual Linux browser screenshot SHA-256: `0290355d85e9995410b48b74b1e47b3d6cb6ec9b9f1042d00d92d6a5ec7d3792`.
Private local report/screenshot paths and personal identifiers are omitted. Docker labels/tags differ from the prior run, so new image IDs are not a registry substitution or a claim of byte-identical rebuilds.

### Concrete blockers and withheld operations

1. **Transport BLOCKED:** actual research UseHttps-only listener52731 conflicts with documented App Service frontend TLS termination/HTTP backend routing. WEBSITES_PORT and external httpsOnly do not fix this. No HTTP fallback, proxy, sidecar or contract amendment implemented. Separate narrow proposal needed before promotion.
2. **Storage/writer BLOCKED:** no research mount exists. Persistent /home does not prove WAL/UID/sync/rename/replacement safety; SQLite WAL is not a network-filesystem solution. ResearchWriterOwnership explicitly provides local-process ownership, not cross-App-Service leasing. No durable Azure backend selected or qualified; no silent ephemeral-state or journal-mode substitution.
3. **State/provisioning BLOCKED:** existing DB/witness/D5 tuple required at startup; no operational initializer/live ingestion runtime established by this read-only snapshot entrypoint. Local test exporter/fake state cannot be deployed. Secure non-test initialization/acquisition/recovery procedure remains unspecified.
4. **Configuration NOT READY:** research lacks image/secrets/HTTPS-only; UI still has legacy provider-setting names and lacks client settings. Remote auth, CA/hostname, direct unauthorized denial and no-leak behavior remain NOT RUN.
5. **F1/resource NOT RUN:** same plan membership confirmed; concurrent deployed workloads, actual idle/cold startup, quotas, 1/3/5 sessions, Azure filesystem and rollout overlap unverified. No Always On, paid tier or new resource assumed.

References and inference limits are linked in the runbook, including Microsoft custom-container/Linux FAQ/hosting-plan docs and SQLite WAL documentation. Next action is owner review of this preparation PR and a narrowly scoped transport/storage/provisioning resolution. Only after resolution/local requalification would exact paired registry publication and changes to the two named staging apps, secret/CA/state configuration, bounded staging verification and coordinated rollback need explicit deployment authority. Production/provider activation remain excluded. Deployment permission alone would not resolve technical blockers. No WP acceptance or PUBLIC_RUNTIME_VERIFIED change follows from these local results.


## WP07 staging-blocker implementation increment (2026-10-25)

Baseline: merged PR #396/main `8bbb8790fff96c895b2a3b4ec9d0200f1ac51d74`, verified by fetch and isolated branch creation. PR #386/#387 not imported. **PUBLIC_RUNTIME_VERIFIED=False; no Azure mutation, credentials acquisition, registry push, deployment, provider call, production operation, issue closure or merge.** All new/modified text is UTF-8 without BOM, LF, final newline.

### Exact implementation inventory and disposition

| Area | Disposition | Implemented / remaining |
|---|---|---|
| Platform transport | IMPLEMENTED_LOCAL | Explicit staging-only HTTP backend52731; default local HTTPS unchanged; same auth/bounds/handler. Actual Azure routing, frontend httpsOnly and ingress verification NOT_RUN |
| Durable private storage / overlap | BLOCKED | Existing local WAL, witness and writer lock preserved; no proven suitable existing F1 durable backend or cross-host exclusion. No lease, schema, journal-mode or backend substitution |
| Operator D5 initialization/import | IMPLEMENTED_LOCAL | Production operator command, explicit generation/epochs, bounded stdin secrets, private completed-row import, exact retry/fail-closed behavior; Linux modes/reopen verified |
| Real acquisition and operational Azure delivery | NOT_RUN | No real provider export obtained, no live provider call, continuous ingestion runtime or Azure initialization claimed |

Exactly eight changed paths:

1. `src/AIQuantTradingResearch.Worker/Program.cs`: dispatch only explicit single-argument --research-initialize.
2. `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs`: default HTTPS plus explicit staged platform backend mode and fail-closed settings.
3. New `src/AIQuantTradingResearch.Worker/ResearchStateInitialization.cs`: bounded operator bootstrap/import through existing store/calculations, no network.
4. `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs`: optional explicit initial generation, existing-generation mismatch rejected; schema/witness/D5 algorithm unchanged.
5. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: transport configuration, parsed bind address, authentication/routing/body bounds.
6. New `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStateInitializationTests.cs`: bootstrap/retry/negative/import/readiness/recovery checks.
7. Existing [staging runbook](prompters/release-1.15-wp07-azure-staging-readiness-runbook.md): factual updates, complete command/input specification and remaining operator prerequisites.
8. This existing evidence document: append only, historical prefix preserved.

No Python, Dockerfile, package, project, schema, owner prompt or unrelated path changes. Original checkout HEAD/status and all **21** modified/untracked owner files were inventoried by bytes/SHA-256, preserved in the separate worktree workflow, and rechecked before publication.

### Implementation and security boundaries

Platform mode requires `AIQ_RESEARCH_TRANSPORT=azure-app-service-http`, explicit `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1`, bind0.0.0.0, exact staging WEBSITE_SITE_NAME/WEBSITE_HOSTNAME and nonempty WEBSITE_INSTANCE_ID. These operator-provided variables are **not attestation** of the platform, HTTPS-only policy or ingress rules. Unknown/missing values reject startup. Default unset/local-https still requires a PFX; Python HTTPS/CA/hostname validation is untouched. The Azure frontend terminates TLS, and its backend HTTP hop carries X-API-Key in plaintext inside the platform trust boundary. No forwarded header confers trust, no proxy/sidecar is introduced, and no end-to-end encryption claim is made. Actual Azure frontend-only reachability, HTTPS-only and unauthorized denial remain required before activation. Production app identity is not allowed by this mode.

Initializer accepts max4096 UTF-8 stdin bytes with a30-second deadline: explicit generation/keyEpoch/transformEpoch,32-byte HMAC Base64 and distinct service API key. Generic success/failure diagnostics never interpolate secrets, paths or raw rows. It creates a new generation only when explicitly supplied to a new store, activates D5 through existing transaction/witness logic, and refuses wrong generation/key/epoch, active writer or invalid witness. Matching configuration and identical import retry do not replace witness. No automatic reset, delete or rotation; initial epoch must1. Existing DB left inactive by a crash before D5 activation fails closed for explicit operator recovery.

Optional private import is bounded1MiB and1..1000 rows each BTC/USD and ETH/USD. It rejects malformed/duplicate/extra fields, malformed UTF-8, uncompleted/future/duplicate/out-of-order rows and invalid decimal/OHLC bounds before creating state; Linux denies group/other-readable import files and reparse/symlink ancestry. Both instrument batches validate before state writes. Existing Commit and PrivateResearchCalculator provide retention, gaps, replay/calculation and witness behavior; no fabricated gap fill or Python analytics. Imports are first-load-only or exact retries, not a replacement corrections engine. BTC/ETH commits are separate, not a cross-instrument transaction; a matching retry can resume an empty second instrument after a partial valid import. Nonidentical re-import into nonempty state requires the existing WP03 ingestion path, not forced replacement.

The importer accepts an operator-asserted Twelve Data export, not cryptographically verified provenance. Real completed one-minute observations must be obtained through separately authorized private acquisition and mapped to the documented input; none were acquired here. No import means empty/unready;400 contiguous rows remain unready,401 become eligible through actual C# calculations; a gap resets readiness. Local qualification inputs are explicitly disposable test observations, never real provider data or Azure bootstrap material. API key is validated for separation but not persisted in D5 state; delivery to both server-side roles remains an operator action.

### Executed offline and local Linux evidence

Tools: SDK10.0.103, Windows PowerShell5.1.26100.9711, Docker29.7.2 Linux/amd64, existing Streamlit1.61.1/Plotly Python7.1.0, Chrome155.0.8059.86/Plotly.js4.1.1. No installations, restores, image pulls or dependency changes. Existing ignored NuGet build assets were copied into this isolated worktree; source and owner files were not copied into its commit.

| Command / check | Final result |
|---|---|
| `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter '(FullyQualifiedName~ResearchStateInitializationTests\|FullyQualifiedName~ResearchSnapshotEndpointTests\|FullyQualifiedName~SqliteResearchStateStoreTests\|FullyQualifiedName~ResearchStateRecoveryTests\|FullyQualifiedName~CommittedResearchProjectionTests\|FullyQualifiedName~TwelveDataMinuteCandleSourceTests)&FullyQualifiedName!~ExportOwnedLinuxQualificationFixture&FullyQualifiedName!~OwnedBrowserQualificationAgainstRealHttpsSnapshot'` | **118/118**, exit0,4s; opt-in no-op export/Windows-browser cases excluded |
| Application filter `FullyQualifiedName~ResearchIngestionServiceTests\|FullyQualifiedName~PrivateResearchCalculatorTests`, same project pattern/--no-restore -c Release | **43/43**, exit0,1s |
| Application filter `FullyQualifiedName~ResearchSnapshotTransformerTests`, --no-restore --no-build -c Release | **20/20**, exit0,83ms |
| `python -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client`, existing venv, PYTHONPATH=python/presentation | **52/52**, exit0,10.254s; expected bare-mode Streamlit warnings |
| `dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-restore --no-self-contained -p:UseAppHost=false -c Release -o <owned-temp-publish>` | exit0; new changed binaries, Linux SQLite native asset present |
| Existing `Invoke-Wp07DualContainerQualification.ps1 -PublishedResearchDirectory <new-publish> -FixtureDirectory <closed-local-fixture> -Python <existing-python> -Execute -KeepImages` | exit0, **PASS_LOCAL_ONLY**, actual default-HTTPS pair and Chrome |
| Supplemental isolated Linux initializer/backend procedure below | exit0, **PASS_LOCAL_ONLY**, seven checks; no public port or cloud operation |

Initial focused33/33 and broader120/120 runs preceded the final correction; the120 count included two inactive opt-in methods and is not the final asserted count. A subsequent parsed-address regression intentionally failed1/1, reproduced and corrected below. Existing synthetic qualification analyzer warnings were untouched; newly introduced initializer warnings were corrected. Known legacy `Volume` versus `Simulated activity` hover assertion remains a documented baseline failure, untouched and not rerun as part of these focused suites.

Actual default-HTTPS runner PASS: both BTC/ETH1000-row full/unchanged contracts, missing/wrong key and wrong hostname/untrusted CA negatives, private UID/modes and UI no private mounts, C# supplied analytics, real browser gaps/retention/RSI/instrument selection, outage/last-good/reconnect, no browser upstream/CDN traffic or fixture key in logs; graceful restart, quiescent SIGKILL recovery and same-volume competing writer denial. No interrupted-write/power-loss, distributed lease, Azure mount or1/3/5-session qualification implied.

Final local image/config IDs (not registry RepoDigests): research `sha256:b3b46a03a6af7915d4c286d9fccb8eb2f17cb28a0c7dc4248f7ffa4dff103938`; UI `sha256:33c512467c2ac785a4accbfa1060667341d762ee44b85823940ddf3ae0d3ef29`. Publish manifest SHA-256 `891138e3494039728539f3b1deef03cc6f6aef3b6d438795d12d162333701d38`. Default-pair report SHA-256 `45985a5fda096c901d204c2c54acfbfcc648f48b2fed48491d79256a419395f7`; screenshot `a7adea3c1db4cf6085b2a0b51566c48bb6b28215232f65910258a29986578645`. Startup through initial API/negative probe2.758960s, not Azure cold-chart time. Stats sample research148.4MiB/256MiB,0.79% CPU,17 tasks; UI168.4MiB/512MiB,0.91% CPU,12 tasks; task counts include threads, one principal app process each. Not peaks or F1 capacity claims.

Supplemental Linux procedure used these final images: create exact-owner-labeled private volume/internal network and a stopped nonroot initializer container; daemon-copy only disposable401-row/instrument import and empty0700 research directory with1654 ownership, not into image layers; pass freshly generated test-only separated secrets through stdin; run production --research-initialize; assert exact retry witness equality and wrong-generation exit1; start explicit HTTP-mode research only on internal Docker network (no published port); test both401-row snapshots with valid key plus missing/wrong-key401 through a disposable Python probe; assert initializer exit1 while API owns writer; SIGKILL API at quiescence and rerun initializer, asserting unchanged witness; inspect ownership/modes using stat-only nonroot probe. Directory1654:1654/0700 and all state files0600. UI's production HTTPS client was not relaxed for this backend probe. Seven final checks PASS, cleanupErrors empty. Supplemental final report SHA-256 `58ba46234dbe293f734c5915256574e8554da234d8b30d021d0130d507b940b9`.

**Failed attempts retained honestly:** first supplemental HTTP startup failed because the new guard compared parsed IPAddress objects by reference. Exact regression using IPAddress.Parse(0.0.0.0) reproduced ArgumentException; value equality fixed it. Default HTTPS had passed independently and did not exercise this new mode. Rebuilt/retested final118 assertions and both Linux procedures after correction. Next supplemental attempt passed six functional checks but its final private-DB-copy permissions measurement triggered its own secret-output guard (private SQLite contains D5 material by design); no secret bytes printed. Replaced measurement with stat-only output; standalone modes test and final complete seven-check run passed. Failed reports were not relabeled PASS. All attempts cleaned only their exact owned containers/volumes/networks; qualified image pairs retained locally. No unrelated resources/processes removed.

### Storage and actual Azure findings

Safe fresh read-only projections reconfirmed research Running shell, httpsOnly=false, publicNetworkAccess Enabled, no custom storage mounts and existing plan F1. No secret values inspected. [Microsoft Linux FAQ](https://learn.microsoft.com/en-us/troubleshoot/azure/app-service/faqs-app-service-linux-new) describes SMB-backed persistent /home versus container-local storage; [SQLite WAL documentation](https://sqlite.org/wal.html) requires same-host shared memory and rejects network-filesystem WAL. Current FileShare.None ownership explicitly is not a cross-App-Service lease. Consequently no supported existing free persistent arrangement satisfying WAL plus overlapping replacement exclusion is established. Local Docker volume/reopen tests cannot establish Azure durability, permission/flush/rename behavior or replacement lease semantics. Storage remains **BLOCKED**, not a fake local-disk or SMB solution. No tier/service/backend migration is proposed or executed.

Next owner review: accept/reject the explicit platform plaintext backend trust boundary and operator initializer implementation independently of storage. Resolve a genuinely supported durable storage/exclusion mechanism before authorizing registry promotion or staging configuration. Actual non-test acquisition, secure Azure state/secrets delivery, frontend HTTPS-only/access controls, exact registry digests, F1 concurrent cold/warm load and per-site browser acceptance remain **NOT_RUN**. Do not infer deployment readiness from this useful local increment; do not automatically close WP issues. PR is OPEN/UNMERGED for review, not a release acceptance.

Final executable source/test SHA-256 inventory:

- `src/AIQuantTradingResearch.Worker/Program.cs`: `f59195a5ee43bd4badeb4f7ebdc99fbc84508cdee7c9a36daf26735be494b9d9`.
- `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs`: `bfe4589b476a0d8e508281238b403668541116081423e68d0253937b552e79ff`.
- `src/AIQuantTradingResearch.Worker/ResearchStateInitialization.cs`: `8d14c2ae4689e10490b3b8a8f2be84e709b3562d70c9b35aca1696f377e33f48`.
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteResearchStateStore.cs`: `38cd027ec5b006d3f768884d4a6a5f0f635aad658ccc0b2d81ab9807d8c9955b`.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs`: `d757df16f8b9df8b177fa416b314d36824f81feefc221dccc21b3f638e5c3dea`.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStateInitializationTests.cs`: `a8c9014d05de297d2a5f88f9bdd5a2f6c7e12ac99005d2454479fae9af1145cc`.


## WP07 authoritative SQLite durability / writer-exclusion disposition (2026-10-25)

**Outcome B: IMPLEMENTED_LOCAL fail-closed boundary and reproducible Linux fault evidence; Azure durability and cross-host fencing remain BLOCKED. PUBLIC_RUNTIME_VERIFIED=False.** SQLite remains the sole authoritative datastore. Fresh main baseline is merged PR #397 `fa0b17e5608fdabc44b8cb88d9a68340d70733b9`; no changes from open #386/#387 imported. Original owner checkout HEAD/status and all **22** modified/untracked files were recorded by bytes/SHA-256 and preserved in an isolated worktree.

### Exact seven-path increment

1. New `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs`: reject unsupported App Service writers before directory, lock or SQLite creation; validate local Linux UID/mount identity/options.
2. `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchWriterOwnership.cs`: invoke the boundary before any filesystem mutation.
3. New `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStorageBoundaryTests.cs`: App Service/no-override, network/unknown/tmpfs/read-only filesystem, mount mismatch, UID and malformed-kernel negatives.
4. New `tests/runtime/release_1_15/wp07_sqlite_fault_probe.cs`: test-only internal-fault driver; not production code or an image layer.
5. New `eng/docker/release-1.15/Invoke-Wp07SqliteQualification.ps1`: Windows PowerShell5.1, nonmutating Docker preflight by default; explicit -Execute for bounded offline compilation and local Linux probes only.
6. Existing [WP07 staging runbook](prompters/release-1.15-wp07-azure-staging-readiness-runbook.md): precise layout, restart/failure semantics, compatibility/cost matrix and SQLite-preserving next decision.
7. This existing evidence document: append only, historical prefix unchanged.

No Worker/initializer/transport/client/chart, schema, package, project, Dockerfile, owner prompt or provider changes. Existing platform HTTP code, default local HTTPS, constant-time API-key checks, strict server-side Python HTTPS and operator D5 initializer/import remain. The shared store boundary now intentionally prevents App Service API/initializer writable startup; this is not an Azure read-only-service exception.

### What the boundary establishes

ValidateHost denies any presence of WEBSITE_INSTANCE_ID, WEBSITE_SITE_NAME, WEBSITE_HOSTNAME or WEBSITE_ROLE_INSTANCE_ID, explicit azure-app-service-http transport, and unknown storage scope. local-qualification scope never overrides these denials. This is fail-closed deployment policy for the unsupported platform, **not** kernel attestation or a distributed fence; a privileged operator stripping all markers is outside this guarantee and is not a supported deployment path.

Local Linux writers require real/effective/saved/filesystem UID1654, readable/valid kernel mount metadata, a recognized backing filesystem (ext4/xfs/btrfs/overlay), writable mount options and optional exact expected mountpoint. Deepest mount wins, so a nested tmpfs/share cannot hide behind an allowed parent. Device/root/mountpoint/type are observed identity, not a reboot-stable disk identity or proof of durability. Overlay is allowed solely for disposable local qualification. Windows host tests do not certify these Linux checks. Existing0700/0600 protection, local lock, WAL/FULL, explicit generation/D5 tuple, witness and fail-closed integrity logic are unchanged. No automatic generation/epoch regeneration or witness override.

Layout: a research-only volume /private, data/ containing DB/WAL/SHM/watermark/writer.lock/pending witness; test-only sibling control/ contains readiness markers. Recover/backup the entire consistent lineage, never a live DB alone or an independently restored old witness. Existing witness flush(true)/same-directory replacement remains **without a directory-fsync/host-power-loss guarantee**. No backup or live Azure storage manipulation was performed.

### Documented versus observed platform facts

Fresh read-only Azure CLI2.89.1 using existing authentication reconfirmed research shell Running, httpsOnly=false, public network enabled, no configured custom storage mounts; shared plan F1 capacity1. No settings/credential values, tokens, publishing profiles or provider calls. No Azure mutation or active Azure filesystem test. These observations do not certify runtime capacity/storage.

[Microsoft Linux App Service FAQ](https://learn.microsoft.com/en-us/troubleshoot/azure/app-service/faqs-app-service-linux-new) documents network-backed persistent /home and separate ephemeral container/host storage. [SQLite WAL documentation](https://sqlite.org/wal.html) requires same-host shared memory and disallows assuming WAL safety on a network filesystem. Existing local writer ownership expressly is not a cross-App-Service lease. Consequently the currently available documented F1 surfaces do not establish durable local WAL plus a fenced authoritative writer across host replacements. This is a concrete compatibility blocker, not a claim that an unexecuted Azure fault test failed.

The runbook matrix retains SQLite for every alternative: ephemeral local storage rejected for durability; /home/SMB/NFS WAL unqualified; a compute-tier upgrade alone does not solve either requirement; single-host persistent block-backed local filesystem is a candidate. Recommend first establishing whether an already-owned suitable Linux host exists; otherwise a small Linux VM with an explicitly non-shared persistent managed disk is the minimal Azure candidate. It requires new recurring compute/disk/backup and possible transfer costs and a research-host topology decision, not a free F1 setting. No size/price/resource purchase is selected. [Managed disk documentation](https://learn.microsoft.com/en-us/azure/virtual-machines/disks-types) supports the block-storage candidate, not application acceptance. Keep UI on F1 and separate images/HTTPS identities only under separately authorized hosting/deployment design. Across-host takeover must fence the old host before disk movement; no automatic distributed failover is implemented.

### Executed commands and results

Tools: .NET SDK10.0.103, PowerShell5.1.26100.9711 (requested patch5.1.26100.9444 unavailable), Docker29.7.2 local desktop-linux named pipe, existing Streamlit1.61.1/Plotly Python7.1.0, Chrome155.0.8059.86/Plotly.js4.1.1. Existing pinned cached runtime bases/qualified dependency context reused. No package installation, dependency upgrades, restores, registry publication or Azure deployment. Only existing ignored NuGet assets were copied to the isolated build tree.

- `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter '(FullyQualifiedName~ResearchStorageBoundaryTests|FullyQualifiedName~ResearchStateInitializationTests|FullyQualifiedName~ResearchSnapshotEndpointTests|FullyQualifiedName~SqliteResearchStateStoreTests|FullyQualifiedName~ResearchStateRecoveryTests|FullyQualifiedName~CommittedResearchProjectionTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests)&FullyQualifiedName!~ExportOwnedLinuxQualificationFixture&FullyQualifiedName!~OwnedBrowserQualificationAgainstRealHttpsSnapshot'`: **137/137**, exit0,6s. Includes19 new boundary cases; inactive opt-in methods excluded.
- Application project same offline command with filter `FullyQualifiedName~ResearchIngestionServiceTests|FullyQualifiedName~PrivateResearchCalculatorTests|FullyQualifiedName~ResearchSnapshotTransformerTests`: **63/63**, exit0,1s.
- Existing venv `python -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client`, PYTHONPATH=python/presentation: **52/52**, exit0,12.376s. Expected bare-mode Streamlit warnings. Existing synthetic test analyzer warnings untouched; no new production warning left. Known legacy Volume/Simulated activity hover assertion remains a baseline failure, untouched/not rerun.
- `dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-restore --no-self-contained -p:UseAppHost=false -c Release -o <owned-temp-publish>`: exit0, new binaries with Linux SQLite native dependency.
- Existing `Invoke-Wp07DualContainerQualification.ps1 -PublishedResearchDirectory <new-publish> -FixtureDirectory <closed-test-fixture> -Python <existing-python> -Execute -KeepImages`: exit0, **PASS_LOCAL_ONLY**, actual default-HTTPS Linux pair and browser.
- New `Invoke-Wp07SqliteQualification.ps1 -ResearchImage <immutable-local-id> -PublishedResearchDirectory <new-publish>`: exit0, **PREFLIGHT_ONLY**, no compiler/container/volume mutation. Windows PowerShell5.1 parser passed.
- Same new harness with `-Execute`: final exit0, **PASS_LOCAL_ONLY_AZURE_BLOCKED**,12 reported cases (one deliberately demonstrates a limitation), cleanupErrors empty. Offline compiler/reference assemblies and already published dependencies only; image Infrastructure DLL hash matched the compile dependency. Test driver uses the existing friend-test assembly identity and is read-only mounted into the running container, absent from image layers. Probe memory256MiB/PIDs64, no network, dropped capabilities, no-new-privileges, read-only rootfs. Kernel-reported local volume type and binary/probe hashes are in the report.

### Deterministic Linux fault/overlap results

| Case | Observed result | What it does not prove |
|---|---|---|
| Same volume, separate container writer while first lives | Second rejected; after first SIGKILL+wait, reopen validates same generation/version/D5 and checkpoint | Cross-host distributed fence |
| Separate volumes, two live writer containers | **Both run**, intentional negative control | No global writer exclusion exists across independent volumes |
| SIGKILL at armed before-commit marker | Uncommitted mutation rolls back; version1 and old tuple reopen Protected | Host power loss |
| SIGKILL at watermark-replace marker | DB version2 committed, pending witness written/flushed; existing chain validates and witness reconciles; checkpoint succeeds | Atomic DB+witness transaction or directory-fsync guarantee |
| SIGKILL at transform-before-commit | Old tuple/version remain recoverable | External D5 secret acquisition or Azure initialization |
| SIGKILL at transform-after-commit | Activation newer than witness is rejected; no reset/automatic healing | Availability through uncertain activation |
| Read-only mount / wrong mount path / UID12002 | All reject | Actual Azure mount behavior |
| Two simulated App Service instance markers | Both reject before a fresh state directory exists, even with local-qualification scope | Live Azure execution or cross-instance lease implementation |
| Tampered witness | Reopen rejected | Detection of coordinated rollback of every artifact |
| Directory/file protection and canary output checks |1654:1654,0700/0600; no test HMAC material in probe diagnostics | Full production secret-delivery audit |

These fault tests observe a precise marker while the transaction/activation is paused, then kill the exact owned container; they are stronger than quiescent SIGKILL, but the host and storage remain running. **Host power loss, physical disk removal, Azure replacement, cross-host fencing and Azure durability remain NOT_RUN/BLOCKED.** No false PASS for those obligations.

Initial harness execution failed at the watermark scenario because its hook was armed during constructor witness reconciliation; it interrupted startup rather than the intended mutation. Fixed test-only arming after constructor completion, reran all cases successfully. The earlier failed report remains FAIL. A subsequent read-only-mount negative was added and all137 assertions, final images/browser and full harness rerun. No production recovery behavior was weakened to pass tests. An initial script-writing command had a nested here-string parser error before executing/writing; corrected quoting, then PowerShell5.1 syntax and execution passed.

Default pair final local image/config IDs, **not registry RepoDigests**: research `sha256:284736e3d0c8b7cc1755141192eecd8611ffbd165f5c4b966cc8bbaa6e325fd5`; UI `sha256:828bc0a1236d378a40acff5bd5c74537b818cc0427418640f37ec6f1c90cae3a`. Publish manifest `3afa5b4e37aa79b5d09a51ae919fe3f7d81f62b343c37ca7b6d022fad248a15e`; dual report `24fa73380ea640febf90e851868e339c3e720781afc7201da14cc7a31da0afba`. Default pair still passes full/unchanged BTC/ETH1000, HTTPS hostname/CA/key rejection, private UI isolation, real browser analytics/gap/retention/instrument/outage/reconnect/no-upstream, same-volume exclusion and graceful/quiescent restart. Startup through initial API probe2.728218s; sample research107.6MiB/256MiB,0.04% CPU,17 tasks; UI168.1MiB/512MiB,0.63% CPU,13 tasks. Tasks include threads, one principal application each. Samples are not peaks, benchmarks or F1 capacity. Local images retained for review; all exact owned runtime containers/networks/volumes cleaned. No owner/unrelated resource cleanup.

Final SQLite harness report SHA-256: `7f2b98d72fa3015d95478938f967aa2634bacf56bc46a43dff727a65f25f5468`. Observed local filesystem stat: `ext2/ext3` (local Docker only). Infrastructure binary: `8755579b20172dc16756b3bfb27f1a362477f3ae295ef3a515280de81ee6a588`; test probe: `d20d5307883d1532593119f0668ffd4c5063d21682a751a77f04a0220540baee`.

Final executable source/test hashes:

- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs`: `c3fa41261ac35d6037c1b92fabed9e89465f611afa52fbc81fc5f44603941e5b`.
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchWriterOwnership.cs`: `fed3a3623d08996075195e1ebfd56bbda2b41f4be37bfae3a5bcd116cab00c2e`.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStorageBoundaryTests.cs`: `585443d9f3c50163609687ec397f9a73c921f246db7c712d06165a2da2fa422b`.
- `tests/runtime/release_1_15/wp07_sqlite_fault_probe.cs`: `bd77fe9d80ad0eba7b3be2dfb1bd62369e8a929984116d9612d7a636dfd1296e`.
- `eng/docker/release-1.15/Invoke-Wp07SqliteQualification.ps1`: `2a9088bfea07168448907aceb2ccfe983fbe460dbf3b296500e71a840f6559e1`.

Next: owner review of Outcome B and a SQLite-preserving research-host/storage decision. No existing F1 writer deployment is authorized or technically qualified; removing the boundary requires a separately reviewed supported mechanism and real takeover/storage evidence. No merge or issue closure. PUBLIC_RUNTIME_VERIFIED=False.


## WP07 single-writer historical chart delivery (2026-10-25)

**Outcome: working local Linux/Chrome BTC/ETH chart path, PASS_LOCAL_ONLY. Real-data acquisition and Azure staging remain NOT_RUN; PUBLIC_RUNTIME_VERIFIED=False.** Baseline is fresh merged PR #398/main `00e8b52bb93edfb8f418808705e414a61d9105f2`. Existing SQLite/WAL/crash/D5 witness and same-volume exclusion evidence from #398 is **accepted and reused**, not reopened. Owner now requires one writer and controlled stop/verify/start; no pooling, distributed coordination, cross-host fencing, automatic failover or host-power-loss tests were added or rerun. No #386/#387 changes imported.

### Exact three-file increment

1. New `eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1`: Windows PowerShell5.1 local operator Validate/Start/Stop; existing qualified immutable images, existing default local named volume, strict server-side HTTPS, loopback-only UI, no live-writer replacement, no volume initialization/deletion.
2. Existing `docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md`: current owner scope, executable bootstrap/launch/replacement path and precise outstanding online decisions.
3. This existing foundation evidence document: append only, historical prefix preserved.

No application/backend/UI, schema, D5 algorithm, SQLite journal, storage boundary, package/project/Dockerfile or owner prompt changes. All authored text UTF-8/no BOM/LF/final newline; git diff --check and independent CRLF checks. Original owner checkout HEAD/status and **23** dirty/untracked file bytes/SHA-256 recorded and preserved in an isolated worktree.

### Topology and implementation facts

Use the already-owned workstation Docker Desktop Linux/amd64 engine and its existing local named-volume capability. This is a working **local** path, not an always-reachable research host or public staging. New launcher accepts only the local Windows named-pipe Docker context and qualified local image IDs; verifies platform/users and default local volume driver/no options without dumping private metadata. It reads an already supplied API key environment, does not retrieve credentials or generate/retrieve provider secrets. Research alone mounts the private data volume and PFX; UI receives only public CA and service key. Research network is internal; only Streamlit is published to127.0.0.1 at an operator-selected free port. Existing strict ResearchSnapshotClient authenticates and validates both instrument snapshots before UI health acceptance. No browser/upstream or provider acquisition is added.

Default Action=Validate / absent -Execute makes no Docker mutation. Start -Execute refuses existing live session or any running container using the state volume, removes only its stopped session-owned containers/networks, creates one researcher and one UI, and records public identity/row-count evidence. Stop -Execute stops UI then research and verifies stopped. Retry/start preserves the named volume, D5 generation/epoch and all state; the launcher never creates or deletes the volume. Failed starts clean only newly created invocation-owned resources and preserve the supplied volume. Test harness later deletes **only its separately labeled disposable fixture volume**, never a real operator volume. Resource labels and running state are projected; no container secret environments or volume option values are read into diagnostics.

The script uses the existing unpassworded-PFX local TLS profile with SAN wp07-research and CA validation; encrypted-PFX delivery is not added or qualified. Current default research HTTPS52731 and App Service storage denial remain untouched. No Azure-marker stripping or same-container fallback. The local tool is deliberately not a remote/public deployment tool.

### Fresh Azure facts and concrete online blocker

Read-only CLI inventory using existing authentication reconfirmed staging UI Running/HTTPS-only/image assigned, research Running shell/HTTPS-only false/no assigned image/no custom mount, both Always On false, existing plan F1. An initial boolean-projection query had Windows quoting errors; corrected separate image-presence projections succeeded without printing registry namespaces. No app setting values, credentials, registry authentication, Azure mutations, remote deployment, real provider calls or production operations.

No suitable always-reachable existing research host was established. F1 remains blocked by retained ResearchStorageBoundary. The runbook gives one concrete next route: owner designates an already-owned reachable single Linux host with persistent local/block storage, TLS/DNS and manual stop/start, or explicitly authorizes the minimal single-instance compute/disk cost/topology change if none exists. This does **not** reopen generic durability research or require distributed fencing for this milestone. No resource, recurring price, tier change or deployment was selected/created.

Real completed-observation acquisition is also **NOT_RUN / not authorized**. Existing TwelveDataMinuteCandleSource/ResearchIngestionService contracts remain the private source/commit path, not a newly activated polling runtime. The runbook documents a bounded authorized private BTC/USD+ETH/USD1min export -> existing production --research-initialize stdin/import -> closed consistent state -> fresh local named-volume seed -> HTTPS API/UI launch procedure. Configuration/API/HMAC/export remain private; no test exporter becomes operational initialization. After first seed, volume is sole active authority and the host copy is offline backup only. No actual real export, initial provider request, credentials read, continuous ingestion or real-data migration is claimed.

### Executed focused local evidence

Reused exact #398 local images (no build/rebuild/pull/push): research `sha256:284736e3d0c8b7cc1755141192eecd8611ffbd165f5c4b966cc8bbaa6e325fd5`; UI `sha256:828bc0a1236d378a40acff5bd5c74537b818cc0427418640f37ec6f1c90cae3a`. These are local IDs, not registry RepoDigests. Existing closed deterministic WP07 fixture state was copied only into a new task-owned disposable volume by daemon archive, UID1654/0700/0600, never image layers or public staging. Both instruments have1000 deterministic **test** candles and an intentional gap; these are not real acquired observations. Existing fake API key/CA/PFX used locally; no actual credentials retrieved. Fixture observation times are old and UI status is honestly stale.

Commands exercised through Windows PowerShell5.1.26100.9711 (requested patch5.1.26100.9444 not separately available), existing Python/Chrome and Docker29.7.2:

```powershell
# Variables contain existing owned inputs; no secret argument values.
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 `
  -ResearchImage $ResearchImage -UiImage $UiImage -StateVolume $FixtureVolume `
  -TlsDirectory $FixtureTlsDirectory -Session $OwnedSession -Port $FreeLoopbackPort
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 -Action Start -Execute `
  -ResearchImage $ResearchImage -UiImage $UiImage -StateVolume $FixtureVolume `
  -TlsDirectory $FixtureTlsDirectory -Session $OwnedSession -Port $FreeLoopbackPort
# Repeat Start while running: expected rejection, existing session unaffected.
# Existing browser harness uses loopback URL and task-owned file control for a normal research stop/start.
& $ExistingPython -B ./tests/runtime/release_1_15/wp07_browser_qualification.py
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 -Action Stop -Session $OwnedSession -Execute
# Start again with same inputs; compare both public identity tuples and row counts, then Stop.
$env:PYTHONPATH='python/presentation'
& $ExistingPython -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client
```

| Check | Actual result |
|---|---|
| Default nonmutating preflight | PASS, exit0; no runtime resource creation |
| API key absent | Expected FAIL/exit1 before start; no fallback |
| New launcher to real C# HTTPS snapshots | PASS, exit0; BTC/USD1000 + ETH/USD1000, strict client |
| Start over live writer/session | Expected FAIL/exit1; existing session preserved |
| Existing real-Chrome container browser acceptance | PASS, exit0;7 reported browser cases |
| Controlled full Stop/verify/Start | PASS, exit0; same public identity tuples and row counts for both instruments |
| Final Stop and exact task cleanup | PASS; cleanupErrors empty; launcher never removed state volume |
| Python chart/client regression | **52/52**, exit0,10.054s; expected bare-mode Streamlit warnings |
| Immutable identity probe regression | PASS; old mappingproxy serialization raises TypeError, explicit public dict copy serializes successfully |
| Windows PowerShell5.1 syntax / UTF-8 LF | PASS |

Actual browser Chrome155.0.8059.86, Plotly.js4.1.1 / pinned Streamlit1.61.1 and Plotly Python7.1.0: BTC candles/SMA20/modebar and verified component; retained1000/gap shapes; supplied RSI14; ETH1000; normal research stop -> Disconnected/last-known with unchanged displayed closes; normal restart -> Connected with same closes; transformed educational labeling and no service key visible; no HTTP(S) browser requests to provider/research/CDN. Browser ran against the **new launcher**, not a Windows surrogate or solely the old runner. Existing unchanged UI behavior/controls reuse accepted prior qualification; no new hover/zoom-retention claim beyond executed browser cases. No general #398 fault matrix, SIGKILL/power-loss or cross-host experiment rerun.

Initial launcher starts failed at the new snapshot probe while both containers were running. Root cause was JSON serialization of ResearchSnapshotClient's immutable identity mapping, not TLS or a service defect. Explicit dict copies of public identity fields corrected the new tool; standalone regression reproduced the old TypeError and successful correction. Added only safe exception-category and status/exit diagnostics; no raw exception payload/key/header logged. Both failed attempts cleaned their own containers/networks and retained supplied state; disposable harness volumes subsequently cleaned. Failed reports remain FAIL. Final full launcher/browser/replacement run passed.

No .NET compilation or tests repeated because application code/images were unchanged; accepted #398: 137 Infrastructure/63 Application and SQLite fault results remain prior evidence, **not newly executed counts**. Known legacy Volume/Simulated activity hover assertion remains untouched and not rerun. No dependencies installed or upgraded. No Azure browser/runtime acceptance, provider acquisition, real-data bootstrap, registry digest publication or deployment occurred. No running preview was left unattended; operator can restart with the documented launcher after preparing owned inputs.

Final chart-launch report SHA-256: `0425efde0dddd3ffd6d818a5bdec847009659d57dd89c362eafb0d66158855fd`; browser screenshot SHA-256: `a7adea3c1db4cf6085b2a0b51566c48bb6b28215232f65910258a29986578645` (owner-local chart.png, personal evidence paths omitted). Launcher SHA-256: `3952462af2afde378ad1dae8b11e0f81011b94fd607350cd7146a4c46288e8e5`.

Next action: owner supplies/authorizes a bounded real completed BTC/ETH export and designates/authorizes a single reachable persistent research host plus exact staging deployment operations. Current local path works with disposable data; no live Azure/real-provider chart claim. One review PR, OPEN/UNMERGED; no issue closure. PUBLIC_RUNTIME_VERIFIED=False.


## WP07 staging delivery checkpoint - 2026-10-25

Baseline PR #399 merged at `654274a3aca706008ea82bb2852b11d19200c092`; clean isolated documentation branch. Fresh read-only management-plane inspection of the exact staging group found only its F1 plan and the two existing apps. UI: assigned image, HTTPS-only, port8501, storage disabled, no custom mounts or research settings. Research: shell without image/mounts/research settings; HTTPS-only false. Both Running and public ingress allowed; these are configuration observations, not service or browser acceptance. No suitable separate research host was found within that group; no unrelated environment was scanned.

The existing strict HTTPS client needs no application correction. No approved authentic completed-minute BTC/ETH export was supplied or found among scoped operator inputs; fixture data cannot substitute. Stop pending two owner inputs: real export or bounded acquisition authority, and a suitable persistent research host explicitly assigned to these staging apps. Existing App Service fail-closed storage guard is untouched. See the existing staging readiness runbook's dated checkpoint for the decision matrix, named-resource preparation commands, rollback limits and actual acceptance sequence. Host-specific research deployment remains unresolved until host designation. No hypothetical host is presented as verified.

Executed: fresh main fetch/ancestor check and GitHub PR399 merged-SHA confirmation; safe staging resource/plan/apps/config/mount/port/research-setting-name inventory; static client/storage-boundary inspection and repository export/operator filename inventory. Initial sandboxed GitHub read failed with network access denied; authorized escalated retry succeeded. Azure commands returned exit0. No provider keys or secret values printed; no live provider requests, Azure mutations, image pushes, deployments, application edits or new tests. Accepted #395-#399 local test/browser evidence was reused without rerunning or relabeling it as Azure.

Changed paths: this append-only evidence file and `docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md` (append-only). Validation PASS: git diff --check; independent strict UTF-8/LF, no BOM/CRLF, final-newline and append-only checks for both files. Owner checkout HEAD/status and all24 pre-existing dirty/untracked file hashes/lengths are unchanged. The first preservation check hit Git sandbox ownership protection; a command-local safe.directory assertion for this known repository allowed the read-only retry, with no global configuration change. Leave the focused PR OPEN/UNMERGED. PUBLIC_RUNTIME_VERIFIED=False; actual staging acceptance NOT_RUN.


## WP07 staging unblock operator increment - 2026-10-25

Baseline merged PR400 `2957c01d03fb48c306bd8d343bdcc424c44878ba`. Added a nonmutating-default PowerShell5.1 import wrapper around existing C# initialization and focused offline tests; no application changes. Existing runbook now contains exact import commands and one unprovisioned WestUS2 Standard_B1s + two E4 disks + static IP proposal, approximately USD16.04/month before operations/transfer/taxes/backup and USD16.56 allowing separately listed mount meters if applicable. Rates read from Microsoft public Retail Prices API, not an account quote or Azure runtime evidence.

PASS_LOCAL: `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp07_import_operator.py -v`, with AIQ_WP07_IMPORT_PUBLISHED pointing privately to the accepted existing publish directory:8/8, exit0, final run2.874s. Negative integration test requires stage=initializer, proving child launch and rejection rather than a preflight failure; state remains absent. First seven-test preflight run passed, but a separate API compatibility probe found StandardInputEncoding unsupported on Windows PowerShell5.1; corrected to UTF-8 BaseStream write. Three subsequent eight-test runs passed, the last after adding exception-path owned-process cleanup. PowerShell5.1 parser also passed. Initial compatibility command continued after a nonterminating PowerShell property error; its trailing success text was not counted as evidence. Actual installed shell5.1.26100.9711; compatibility targeted5.1, exact owner patch9444 not installed. No successful authentic import, repeated SQLite fault matrix, app rebuild, broad test suite or browser run claimed.

VERIFIED_AZURE_STAGING_MANAGEMENT: safe read-only projections reconfirmed only the exact named group/apps/plan and settings/mount metadata, unchanged from PR400. No production/unrelated resource inventory. NOT_RUN: live acquisition, paid resources, image publication, Azure mutations/deployment, actual Azure TLS/browser/restart/session acceptance. Only two owner decisions remain: authentic export/bounded acquisition and assigned persistent staging host/proposed budget. PUBLIC_RUNTIME_VERIFIED=False.

Final hygiene PASS: PowerShell5.1 parser, git diff --check, strict UTF-8/no BOM/LF/final-newline for all four changed files and append-only document prefix checks. Original owner HEAD/status and all25 pre-existing dirty/untracked file SHA256/lengths preserved. Changed paths: new eng/docker/release-1.15/Invoke-Wp07ResearchImport.ps1; new tests/runtime/release_1_15/test_wp07_import_operator.py; append-only existing staging readiness runbook and this evidence file. No merge or issue closure.


## Authentic staging chart assignment: read-only executable preflight - 2026-10-25

Baseline merged PR401 `f19d63647efcda5fe734363d87c430ffe8208ae2`. Added fixed-target Test-Wp07StagingChartReadiness.ps1 and offline assessment tests; no governance, application, research/store, container or dependency changes. VM proposal withdrawn, no paid-resource prerequisite reinstated. Current disposition and exact staging commands/acceptance limits are appended to the existing operational runbook.

Acquisition:0 requests/0 credits. Presence-only checks of three known provider process-environment names found none; no alternative approved channel/export or account-specific credit/base-volume evidence supplied. Research URL/key also absent from process environment. No provider/Azure credential retrieval, live calls, private-file search or fixture relabeling. Existing source outputsize1000 and local credit counter do not establish account-specific billing or volume units. Import/initializer remain unchanged and were inspected, not rerun.

PASS_LOCAL: `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp07_staging_readiness.py -v`,8/8, exit0, final2.781s. Earlier run8/8 in2.879s before adding transport-error categories. Actual Windows PowerShell5.1.26100.9711; exact9444 patch unavailable. Initial direct invocation was blocked by execution policy; tests and explicit read-only execution used a child-process-only Bypass, no persistent policy change. An early CLI query quoting error was corrected before live script execution. A static read initially targeted the protected older checkout and found missing paths; rerun in the isolated approved baseline succeeded, no owner edits.

VERIFIED_AZURE_STAGING_MANAGEMENT: fresh safe exact-resource inventory; then `powershell.exe -NoProfile -ExecutionPolicy Bypass -File ./eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1 -InspectAzure -ProbeUi`, exit0. UI configured as legacy image/HTTPS, research shell unconfigured,3 UI research setting names missing. No compatible endpoint established. First two public probes UNAVAILABLE, reason unknown; follow-up `-ProbeUi` after safe diagnostic improvement, exit0, observed HTTP200 root and health (four GET attempts total), without any restart/deployment. This is HTTP-only evidence, not browser/research/cold-start acceptance. Script report retains chartAcceptance=NOT_RUN even on HTTP200.

NOT_RUN: authentic import/acquisition, publication/deployment or any Azure mutation, browser acceptance, research TLS/auth/data/indicators/restart/capacity tests. Retained guard rejects the only designated research App Service; no permitted compatible backend established. Next decision is designation of an already-owned compatible endpoint serving existing staging, or deferral; no new VM or storage exception. PUBLIC_RUNTIME_VERIFIED=False. One focused PR, OPEN/UNMERGED.

Final hygiene: strict UTF-8/no BOM/LF/final-newline checks passed for all four paths; documents append-only. git diff --check passed. Original owner HEAD/status and all26 dirty/untracked file hashes/lengths unchanged. Exact paths: NEW eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1; NEW tests/runtime/release_1_15/test_wp07_staging_readiness.py; APPEND existing staging readiness runbook and this evidence file. No secrets, candle exports, image namespaces or personal paths published.


## WP09 exact-site non-durable writer implementation - 2026-10-25

Baseline PR402 squash `d209f0ae5fb144ed3165719ebd3a400cb326e3ad`, fresh main ancestry verified; isolated worktree. Implemented only an explicit dedicated scope+flag exception in ResearchStorageBoundary for the exact research staging site/default hostname, nonempty instance, role0, disabled App Service storage, Linux UID1654 and expected root mount. Fixed state path /tmp/aiq-research; writable root overlay only, not network/persistent/tmpfs/bind/subdir/shared-propagation mounts. Private0700 directory, symlink checks and bounded real write probe precede SQLite ownership/open. All default App Service denial, WAL/witness/D5/single-process locking, authentication, transport, provenance, initialization and analytics remain intact. Existing diagnostics now explicitly label appServiceDurableWriterSupported=false rather than implying the new disposable exception is impossible. No production extension.

PASS_LOCAL commands/results:
- Offline restore using only the existing user NuGet package cache, NuGetAudit=false; no package download/upgrade.
- `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchStorageBoundaryTests|FullyQualifiedName~ResearchSnapshotEndpointTests|FullyQualifiedName~ResearchStateInitializationTests"`: final86 reported passed,0 failed. Windows run does not execute the Linux-only private-directory test body or opt-in browser fixture bodies; no Linux/browser acceptance inferred from that count. Initial85-test run preceded the role-instance negative case; subsequent86-test runs passed. New CA1416 warnings were corrected with precise Linux annotations; existing SyntheticSnapshotQualificationTests analyzer warnings remain unrelated.
- Offline `dotnet publish ... --no-restore -c Release -p:UseAppHost=false` to owned temporary output succeeded.
- PS5.1 `Invoke-Wp09EphemeralQualification.ps1` default: PREFLIGHT_ONLY. -Execute:11 actual Linux Docker cases PASS_LOCAL: writable root overlay/store+D5 reopen/second writer denial; default App Service denial; wrong site; missing instance; wrong UID; read-only root; tmpfs; real symlink; broad permissions; malformed DB startup; missing DB startup. Cached immutable Linux image used only as runtime, changed publish bind-mounted in owned test copy. Test friend assembly never packaged in an image. No network/provider/real candles. Exact labeled containers cleaned up, no volume created. No general fault matrix/power-loss work repeated.
- First two smoke attempts failed with Docker exit125 before test execution due to nested bind inside read-only publish mount. Corrected test harness to a private publish copy; ten cases passed, then an added malformed-database case yielded11/11. Failure is not characterized as a production-boundary failure; no boundary relaxation used to pass.
- `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp07_staging_readiness.py -v`:8/8, exit0,6.642s after the diagnostic field rename.
- Windows PowerShell5.1.26100.9711 parser passed for the new runner; compatibility targets5.1, exact9444 patch unavailable. An initial tool-side nested here-string failed to parse before writing the runner; corrected delimiter construction.

Minimum governance amendment appended to existing WP01 engineering contract; runbook includes scope conditions, disposable-state/reimport/rollback, exact existing-app config/stop/start templates and truthful bootstrap limitations. The current image entrypoint does not auto-initialize; secure authentic-input delivery and same-container controlled initializer/startup sequencing remain operational prerequisites, not invented working Azure commands. Frontend HTTPS/platform HTTP retains its existing explicit assertions and is not claimed end-to-end TLS. No data/credential handling added to public endpoints.

VERIFIED_AZURE_STAGING: none newly run in WP09. Named app's actual mountinfo/UID/filesystem, frontend/backend routing, idle/cold start and shared F1 CPU/memory/session behavior remain NOT_RUN; prior management inventory is reused only as metadata. No mount layout, data survival or F1 availability claim. BLOCKED_INPUT: authorized private data/account prerequisites still unavailable, acquisition0 requests/0 credits; no secret lookup. Practical private bootstrap delivery still needs a concrete authorized mechanism. NOT_RUN: Azure mutation, image publication, deployment, restart, real-data import/acquisition, research TLS/auth negatives or browser acceptance. No paid resources, merge or WP closure. PUBLIC_RUNTIME_VERIFIED=False.

Final hygiene PASS: nine changed files strict UTF-8/no BOM/LF/final-newline; all three documents append-only; git diff --check; PowerShell parser. Owner HEAD/status and all27 dirty/untracked file SHA256/lengths unchanged. Docker labeled-container inventory empty after smoke cleanup. Exact changed-file inventory:
- docs/roadmap/release-1.15/RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md
- docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md
- docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md
- eng/docker/release-1.15/Invoke-Wp09EphemeralQualification.ps1
- eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1
- src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStorageBoundaryTests.cs
- tests/runtime/release_1_15/test_wp07_staging_readiness.py
- tests/runtime/release_1_15/wp09_ephemeral_probe.cs


## WP10 private-stdin bootstrap and two-image local qualification - 2026-10-25

Baseline PR403 merged at `894b075e65863c042e9c39167fdbd71612020c90`, verified ancestry and GitHub merge. Isolated worktree; no C#/governance/storage-guard change. New shell entrypoint adds explicit operator-stdin initialization followed by exec of unchanged endpoint, plus read-only diagnostics; default serve remains. Existing qualification context now includes the shell. New PS5.1 wrapper/Python runner builds both existing images from pinned cached inputs and tests sequencing privately. No public import endpoint or automatic polling/fetch/fixture bootstrap.

PASS_LOCAL: cache-only restore and fresh offline Worker publish succeeded. Invoke-Wp10BootstrapQualification.ps1 default PREFLIGHT_ONLY; -Execute built research/UI in three bounded runs (9,10, then11 cases), final eleven passed: diagnostic on read-only root, unknown mode, missing export, lost state, invalid stdin, same-container initializer/server, missing-key401, BTC+ETH full C# fixture snapshots, no secret diagnostics and isolated UI image health with no credentials/network. No failing code/smoke runs. Fixture401 rows/symbol are fabricated offline tests, not real observations; local platform-HTTP/simulated Azure markers are not actual Azure or end-to-end TLS proof. Both expected UID images; all owned containers, private test input/context/env files cleaned. Local image IDs recorded in runbook, no registry digests fabricated. No broad SQLite/browser qualification repeated.

VERIFIED_AZURE_STAGING management only: exact research app Running Linux shell, imageAssigned=false, startupConfigured=false, AlwaysOn=false, one listed instance. No UID/mount/runtime command executed in Azure. The current Microsoft documented app-container exec channel needs CLI2.90+; installed CLI help rejects webapp exec, exit1. Initial sandbox help was profile-permission blocked; escalated help confirmed unsupported command. No CLI/extension install, RBAC/SSH change or SCM credential retrieval. Do not infer actual private file/stdin delivery from local Docker or documentation. Kudu filesystem is not assumed equal to application filesystem.

BLOCKED_INPUT: presence-only provider environment checks all false, no approved alternate export/channel, account-specific credit or base-volume facts. Provider requests0/credits0. Same-container secure delivery on actual Azure still unproven; an authenticated import route would need separate design review and is not implemented. Task goal not achieved in staging yet. NOT_RUN: image push, Azure config/restart/import, runtime guard verification, authentic observations, TLS/auth/browser/cold-start/sessions. PUBLIC_RUNTIME_VERIFIED=False.

Next narrow mutation request is only publish the tested research candidate, apply diagnostic mode to the exact existing research staging app for a <=60s observation, stop/restore its original shell/configuration; no UI mutation/data/provider/new resource. Capture exact baseline and actual registry digest privately first; failure to establish safe rollback/log capture stops before mutation. Runbook provides precise commands and limits; no mutation authorized by this PR.

Final checks PASS: strict UTF-8/no BOM/LF/final-newline for all7 changed paths, append-only documents, git diff --check, Windows PowerShell5.1 parser for both changed PS scripts and Python AST. No labeled test containers remain. Original owner HEAD/status and all28 dirty/untracked SHA256/lengths preserved. Actual shell5.1.26100.9711; exact9444 patch unavailable. Changed paths:
- container/release_1_15/research-api.Dockerfile
- container/release_1_15/research-bootstrap.sh
- docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md
- docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md
- eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1
- eng/docker/release-1.15/Invoke-Wp10BootstrapQualification.ps1
- tests/runtime/release_1_15/wp10_bootstrap_qualification.py


## WP11 actual diagnostic attempt and verified recovery - 2026-10-25

**INCOMPLETE: compatibility unverified; exact baseline restored.** Owner-approved PR404 research-only diagnostic was executed once, after verifying merged baseline c300087aa250d86de24a64ed4bfe0805d3716c9d, approval comment, protected baseline, rights, existing public registry access and log retrieval. No prior diagnostic marker found. No UI/provider/import/paid-resource/production operation. One research upload; anonymous leaf digest `sha256:5b48f5d703acd7dca7e722ab9bc0028cbe8b80a3526c7a60800699785f6abed3` verified against the tested OCI index and independent config/rootfs/UID archive. Initial index-versus-config readback failed closed before Azure mutation; fixed with independent proof and no repeat push.

Actual Azure start succeeded23:44:39.4309906Z; first stop succeeded23:45:03.555938Z:24.125s server-clock control-plane window. Local request at23:44:37.522609Z to stop event26.033s. Watchdog stop also succeeded23:45:08.4815716Z. The controller could not confirm its initial stop and returned rollback-unconfirmed before restoration. Mandatory recovery continued: verified research Stopped, restored baseline image/startup/logging/settings, restored original shell Running, then independently verified full configurable equality/state at23:47:38.711405Z. Only server-generated audit/ETag metadata excluded. No unresolved rollback. Protected original failure and independent recovery records retained.

No attributable candidate UID/mount/guard output in archives or bounded retained-logstream check (zero bytes). No leaf digest reference recovered in logs. Baseline startup-failure indicator was already present; do not blame the attempt or claim filesystem incompatibility. No second diagnostic run occurred. Neither A nor B acceptance established; C/no-mutation would be false. WP11 not accepted, PUBLIC_RUNTIME_VERIFIED=False.

Narrow code: actual guard-only CLI probe (no SQLite/service/provider), fixed-target publication/watchdog/finally rollback controller, negative/local tests. Post-live controller fix retries transient stop verification and distinguishes stop-ack timing; not requalified in Azure. No host guard/default/production policy or data-contract change. Private configuration, registry namespace, user identity/evidence roots and raw logs not published.

PASS_LOCAL commands: cache-only restore; fresh offline publish; dotnet test Infrastructure.Tests --no-restore -c Release --filter FullyQualifiedName~ResearchStorageBoundaryTests (50 passed); Invoke-Wp09EphemeralQualification.ps1 -Execute (13 Linux cases, no candle rows); unittest discover -s tests/runtime/release_1_15 -p test_wp11_research_diagnostic.py -v (14 passed). Original local probe marker assertion failed, corrected without policy changes. Windows PowerShell5.1 parser; exact9444 patch unavailable, installed9711. No broad SQLite/browser qualification or authentic-data work. See appended staging runbook section for exact timeline, artifacts, failures and controller limitations. Focused PR remains OPEN/UNMERGED.

Final hygiene PASS: strict UTF-8/no BOM/LF/final-newline for all12 paths, append-only evidence/runbook, git diff --check. Owner HEAD/status and all29 dirty/untracked SHA256/lengths unchanged. No second diagnostic or UI action. Exact changed paths:
- container/release_1_15/research-bootstrap.sh
- docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md
- docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md
- eng/azure-cli/r1.15-qualification/Invoke-Wp11ResearchDiagnostic.ps1
- eng/azure-cli/r1.15-qualification/wp11_research_diagnostic.py
- eng/docker/release-1.15/Invoke-Wp09EphemeralQualification.ps1
- src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs
- src/AIQuantTradingResearch.Worker/Program.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchStorageBoundaryTests.cs
- tests/runtime/release_1_15/test_wp11_research_diagnostic.py
- tests/runtime/release_1_15/wp09_ephemeral_probe.cs
- tests/runtime/release_1_15/wp10_bootstrap_qualification.py


### WP11 attributable-evidence amendment ? offline qualification only

The original 24.125-second Azure control-plane window remains inconclusive: no
attributable runtime frame was recovered. Delayed or empty platform log delivery,
a container startup failure, and an early exit are hypotheses, not established
causes. Control-plane start success and downloaded historical markers do not prove
that the candidate entrypoint executed. Exact baseline rollback was independently
verified after manual recovery; that history is unchanged.

The diagnostic entrypoint now emits one bounded JSON frame containing version 1,
a unique 128-bit attempt nonce, immutable candidate leaf digest, UTC observation
second, the fixed `research-bootstrap/diagnose` entrypoint identifier, four numeric
UID fields, root overlay/writability booleans, and the six fixed storage-probe
booleans. Missing or malformed correlation is rejected before probing. No mount
sources, environment values, database, API, imports or provider calls are emitted
or initiated. The existing production storage guard and exact-site policy remain
unchanged. Historical archive reduction is retained solely for inspection; the
controller no longer accepts archive markers as runtime evidence.

The validator requires strict UTF-8, at most 4,096 bytes, exact fields/types, no
duplicate keys, matching nonce/digest, consistent guard/UID/mount conclusions,
and `attempt start <= observation <= receipt` within 60 seconds. Receipt requires
an independently authenticated private channel bound to the actual candidate
execution and registry-qualified immutable leaf. The payload's digest is only
self-reported correlation, not attestation. Clock skew or delayed receipt fails
closed; a nonce must never be reused. No currently verified F1 private retrieval
channel supplies this binding. Therefore an unconditional preflight blocks
publication and Azure execution; no setting or operator claim can unlock it.
There is no new public diagnostic endpoint or persistent evidence artifact.

Offline verification:

- `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp11_research_diagnostic.py -v`:
  **24 passed**, exit 0. All 14 prior tests remain. New cases cover malformed,
  missing, partial, oversized, replayed, mismatched, future/delayed and inconsistent
  evidence; duplicate/private fields; preflight blocking; early exit without a
  frame; startup failure after watchdog arming; watchdog stop success/failure.
  Existing stop-confirmation and exact rollback failure tests remain passing.
- Two actual Linux executions used only the cached research image, a read-only
  bind of the amended shell, UID 1654, read-only root, dropped capabilities,
  no-new-privileges and no network. Missing correlation: rejected, exit 1, empty
  stdout. Valid correlation: exit 0, frame parsed by the strict validator, all
  UID fields 1654, default storage denial and probe cleanup confirmed. These
  prove local shell serialization/default denial only, not Azure source identity
  or the fully opted-in Azure write boundary. No image was built or published.
- The existing WP10 bootstrap fixture was adjusted to consume the new frame;
  its import/endpoint suite was not rerun under this no-import amendment.
- PowerShell wrapper unchanged; no new PowerShell syntax. Windows PowerShell
  5.1.26100.9444 was not available for exact-build execution qualification.

A later retry needs separate owner authority for the exact corrected image
publication, named research staging target, at-most-60-second run, independent
watchdog and exact rollback, plus a concrete private retrieval facility. Before
removing the preflight block, that facility must demonstrate authenticated
application-container source identity, independent immutable-image binding,
bounded retrieval and timeout behavior, and observation/receipt freshness.
A successful control-plane operation, HTTP 200, configuration echo or log marker
alone is insufficient. Missing collection continues to require stop/restoration
and an INCOMPLETE result; a failed stop/restore remains critical, never acceptance.
The watchdog deadline is not proof of kernel termination or a hard cloud stop SLA.
No such channel or Azure qualification was performed by this amendment.

`AZURE_DIAGNOSTIC_NOT_RUN`; `F1_COMPATIBILITY_UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.
PR #405 remains OPEN/UNMERGED; UI, production and provider operations untouched.

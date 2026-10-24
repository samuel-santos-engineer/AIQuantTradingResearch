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

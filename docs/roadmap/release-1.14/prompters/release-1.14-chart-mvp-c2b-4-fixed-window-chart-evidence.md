# Release 1.14 C2B-4 — Fixed-window chart qualification evidence

Date: 2026-10-15 (owner timezone). **Disposition: safe local shell verified; data publication BLOCKED.**
`PUBLIC_RUNTIME_VERIFIED=False`. Production public activation and Azure deployment remain **NO-GO**.
This milestone delivers the permitted blocked-publication dossier and stronger HTTP
regressions, not an operational candle chart or acceptance of WP07.

## Baseline, authority and exact change inventory

Canonical active checkout: `C:\projects\github\AIQuantTradingResearch`.
Initial `main` and verified remote `main`:
`c9461b20dfcb3f7ec60ffffbcd6013f2aecde285`, containing merged PR #333.
The sole initial untracked item was the owner-pasted C2B-4 authority document.
The owner explicitly authorized preserving it and proceeding. No reset, clean,
stash, private checkout inspection or archive operation was used.
Branch: `feature/release-1.14-chart-mvp-c2b-4`.

Exactly three paths are included:

1. `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-4-fixed-window-chart-qualification-codex.md` — preserve owner authority.
2. `python/presentation/test_public_preview_api.py` — two added HTTP tests, no production change.
3. `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-4-fixed-window-chart-evidence.md` — this dossier.

No edits to generator, canonical contracts, fingerprints, indicator calculator,
Worker, bridge, runtime gate, API implementation, UI, startup, adapters, dependencies,
schemas, Docker configuration, protected WP07 repository or archive. Preservation
here means no operation on those protected locations; prior archive verification
was not rerun or represented as fresh evidence.

## A. Existing path and actual publication interruption

The implemented offline path is:

1. `SyntheticHistoricalMarketDataProvider`: independent SHA-256 counter algorithm,
   scaled integer arithmetic, complete fixed dataset, aggregation and selected calculation window.
2. `HistoricalMarketDataReadService.PublicSynthetic`: context-specific cache key,
   bounded acquisition and cache validation through `SyntheticPublicationPolicy`;
   candle/cache `CalculationVersion` remains null. No trusted cache-hit shortcut.
3. `HistoricalMarketDataQueryExecution`: validate the full calculation window,
   run shared `TechnicalIndicatorCalculator.CalculatePublic`, then project exactly
   the visible rows after 250 warmup rows. Validate source, window/dataset identity,
   fixed parameters and derived publication again. Response uses `aiq-ta-v1`.
4. `historical_market_bridge`: bounded v2 input/output validation, expected dataset,
   window and visible-row fingerprints, strict analytics identity/alignment/decimal validation.
   Its public invocation is stopped by the false runtime gate before process launch.
5. Streamlit/Plotly: existing figure builder and gated rendering; while unqualified,
   retained chart response is removed and selection controls remain disabled.

The HTTP API is a separate, bounded unavailable implementation. It has **no**
connection to Worker output, fixtures, snapshots, private files or the figure builder.
`public_preview_app.py` serves the six routes through supported Streamlit middleware
on the existing port. There is no ready-data endpoint implementation to qualify merely
by adding a file. This trace is source inspection plus offline tests, not an active
public generation pipeline or proof of OS-level isolation.

## B. Architecture comparison

| Dimension | Immutable versioned snapshot | Synthetic-only SQLite | Existing on-demand generator/Worker |
| --- | --- | --- | --- |
| Provenance/source independence | Exact canonical bytes, pinned hashes and independently audited exporter could bind identity; hash alone does not authenticate an untrusted producer | Must bind rows and metadata to complete canonical/window hashes in one accepted version; database label alone proves nothing | Existing generator/reference tests bind algorithm, seed, bytes and provenance; runtime composition still unqualified |
| Reproducibility | Strong candidate if exporter reuses frozen serialization and cross-runtime proof | Reconstruct canonical ordering/decimals; DB physical-file hash is not the candle fingerprint | Existing deterministic reference and overlap tests pass |
| Atomic publication/integrity | Candidate same-filesystem temporary write, verify, atomic pointer/version replacement; reader pins one verified version; race/symlink/rollback proof outstanding | Transactional replacement and readers pin committed version; mount/WAL/locking/crash durability evidence outstanding | Current bounded request/cache publication design retained; lifecycle admission/cleanup proofs outstanding |
| Warmup and indicators | Need all twelve calculation windows or complete data plus deterministic window derivation; never calculate from visible rows only | Need exact ordered 250-row prehistory and fixed range identity, not arbitrary SQL-derived seeds | Existing full-window calculation and visible-only projection already tested offline |
| UTC/gaps | Preserve exact complete grid and exclusive fixed end; no rolling regeneration or filled gaps | Enforce order, alignment, complete groups and canonical precision across reads | Existing continuity, aggregation and rejection tests retained |
| Read concurrency and abuse | Eliminates request generation **mechanically**, but verification/loading/serialization and request allocation remain bounded work | Adds query/connection/lock costs and writable-store attack surface | Requires exclusive generation admission, bounded capture, accounting and independent cleanup verification |
| Startup/restart/recovery | Missing/corrupt/unknown version must stay unavailable; no startup generation; no older snapshot fallback unless separately authorized | Missing/corrupt/inconsistent DB must stay unavailable; automatic repair/rebuild not approved | Unknown admission or cleanup state denies generation; held WP07 mechanisms cannot be assumed safe |
| Confidentiality | Synthetic bytes still need trusted provenance, private-path exclusion, protected mounts/environment/FDs and no provider access | Separate database and access policy required; same UID or public DB name is not isolation | Worker/private filesystem, network and inherited-state boundaries still require target evidence |
| CPU/memory/F1 | Likely simplest candidate; hash/parse/analytics/object overhead and ingress/health headroom unmeasured | More storage/connection/durability complexity; F1 filesystem semantics and headroom unverified | Existing CPU/memory/reservation/recovery obligations unresolved; no new measurements here |
| Persistence/cost | Can potentially be packaged read-only without another service; image integrity/build/export authority still required | No package/resource/backend selected; existing mount semantics not proven | No added resource authorized; platform quota/availability remains unqualified |
| Current contract status | **Not selected for implementation/publication**; requires exact amendment and proof | **Not selected**; adds unresolved mechanism obligations without removing publication gate | Existing offline implementation only; public execution **ON HOLD** |

**Recommendation:** immutable, fingerprint-verified data is the smallest *candidate*
for a future read-only publication architecture. It can remove public-request generation
but cannot, under the existing authority, remove or satisfy WP07 gates by assertion.
The selected executable behavior for C2B-4 remains the existing unavailable API and shell.
No snapshot exporter, loader, sample dataset, SQLite table or alternate Python generator
was added. No ready-data response schema or new cache contract was invented.

### Exact contract conflict and owner decision needed

- `RELEASE_1.14_DEFINITION.md`, complete-generation rule: complete base once per cold
  Worker request; no deployment/startup/background generation; optimizations require
  separately accepted equivalence proof.
- `RELEASE_1.14_EXECUTION_PLAN.md`, WP02: same cold-request design, no startup/background generation.
- `PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, Section 10, global abuse
  amendment item 5: cache hits remain inside existing cold-generation/publication
  contracts; the amendment explicitly does not authorize exported fixtures or startup pregeneration.
- Section 11: missing mandatory authorization, provenance, integrity or durable-state
  evidence fails closed; held WP07 and `PUBLIC_RUNTIME_VERIFIED=False` remain intact.

C2B-4 explicitly permits choosing a blocked-publication outcome if those frozen
contracts prevent safe publication; its conditional preference for snapshots is not
an approval to supersede them. Historical proposed-policy wording is retained as such,
not promoted to proof of accepted runtime architecture.

A future owner decision would need an exact read-only-publication amendment: trusted
offline production/export authority; permitted immutable artifact and canonical format;
hash/provenance trust anchor; validation/atomic publication/retention rules; mapping of
all existing safety gates to a no-generation public reader; ready response schemas and
byte/concurrency/abuse bounds. Decide separately whether any mandatory generation
admission/recovery requirements become inapplicable to that reader, and why. Preserve
confidentiality, fail-closed integrity and no public provider access. Activation remains
separate and cannot be achieved by flipping or bypassing the current gate.

## C. Frozen contract and fingerprint proof

No generator implementation, test golden or qualification map changed relative to
the baseline. Existing independently authored embedded CPython reference tests were
executed in the full Infrastructure suite; they compare complete byte arrays, hashes,
approved row vectors, aggregation and all twelve calculation/visible windows against C#.
The reference is authored from the frozen algorithm, not copied from generator output.
These are isolated offline tests, not public generation or exported production fixtures.

| Interval | Complete rows | Canonical bytes | Dataset SHA-256 |
| --- | ---: | ---: | --- |
| 1h | 8,160 | 596,926 | `5b187d5bea1fd202077c33713855f9c0518446ce6a8864a5249daa0e7e45dc29` |
| 4h | 2,040 | 151,407 | `d713767c7afeb5d3f535e8276e1cecb905b3f2d6c096aac5a62214f62ccc22ec` |
| 1d | 340 | 25,957 | `0fc878f11193073aa88f71b026825c9f7cbb4cb18d5c87fec2d66a9e44291c81` |

Coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, algorithm
`aiq-synthetic-ohlcv-v1`, seed 114. Canonical complete bytes total 774,290; this is
not an RSS estimate or a ready HTTP response-size budget.

| Interval | 1D visible/calculation | 7D | 30D | 90D |
| --- | --- | --- | --- | --- |
| 1h | 24/274 | 168/418 | 720/970 | 2160/2410 |
| 4h | 6/256 | 42/292 | 180/430 | 540/790 |
| 1d | 1/251 | 7/257 | 30/280 | 90/340 |

All twelve exact window and visible-row fingerprints remain in the unchanged
`SyntheticHistoricalMarketDataProviderTests.Windows` and production qualification maps;
the test compares each canonical byte sequence with CPython. Every calculation has
exactly 250 preceding candles. A future snapshot cannot trim to 90 visible daily rows
and still calculate the frozen daily indicators. Recursively seeded indicators from
different range windows need not be equal. No gap filling or wall-clock market-staleness
claim is added; fixed reference end is not today's rolling coverage.

Unchanged Application calculator tests cover independent constant/rising/falling/mixed
vectors, exact signed rounding, square-root midpoint boundaries, nullable alignment,
maximum inputs, no lookahead, malformed/gapped/duplicate/order errors and provenance.
Unchanged Worker tests cover all twelve full-window/visible projections and final
publication checks; unchanged Python bridge tests reject rehashed/tampered rows,
identity/parameter mismatch, malformed decimals and analytics alignment errors.

## D. Verification commands and results

Run in the canonical checkout with its existing ignored pinned `.venv`; scripts and
commands remain Windows PowerShell 5.1 compatible. No packages or restores were needed.

```powershell
$env:PYTHONDONTWRITEBYTECODE='1'
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_*.py' -v
dotnet test AIQuantTradingResearch.slnx -c Release --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.AdminDiagnostics.Tests -c Release --no-restore --nologo
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_public_preview*.py' -v
& '.venv\Scripts\python.exe' -B -m unittest discover -s python/presentation -p 'test_*.py' -v
git diff --check
```

| Run | Result / exit |
| --- | --- |
| Baseline Python | 171 run: 160 passed, 11 platform skips; exit 0 |
| Baseline .NET solution | 570 passed: Domain 11, Application 215, Architecture 27, Infrastructure 317; exit 0 |
| Baseline packaged offline protocol | 50 passed; exit 0 |
| Expanded focused shell/API | 16 passed, zero skips; exit 0 |
| Final Python regression | 173 run: 162 passed, 11 platform skips; exit 0 |
| Protected implementation comparison | No diff against baseline |
| Whitespace check | Pass |

The eleven platform skips are retained Linux-specific tests, not silently accepted
isolation evidence. New tests add six real HTTP invalid-query cases and ordinary
localhost server restart/readiness validation. They do not perform Worker fault
injection, native FD operations, snapshot publication or crash-safe recovery qualification.

### Endpoint evidence, including negatives

| Route under `/api/v1` | Normal/unavailable HTTP + code | Real HTTP negative |
| --- | --- | --- |
| health | 200 / `OK`, state alive | 400 / `INVALID_QUERY` |
| status | 200 / `DATASET_NOT_READY`, web ready, dataset/indicators unavailable, generation false | 400 / `INVALID_QUERY` |
| dataset | 503 / `DATASET_NOT_READY`, data null, fixed reference metadata, actual coverage/fingerprint null | 400 / `INVALID_QUERY` |
| candles | 503 / `DATASET_NOT_READY`, data null | 400 / `INVALID_QUERY` |
| indicators | 503 / `DATASET_NOT_READY`, data null | 400 / `INVALID_QUERY` |
| diagnostics | 200 / `DATASET_NOT_READY`, category only, no automatic retry | 400 / `INVALID_QUERY` |

Each negative sends an unknown private-path-shaped query; exact safe JSON failure
is asserted without echoing input, with JSON content type, no-store and bounded body.
Each normal response asserts JSON version and size; restart rechecks status/code and
no promotion of dataset/indicator readiness. `/` HTML and `/_stcore/health` return 200;
`/admin` returns 404 and POST dataset returns 405. All observed on actual localhost
Streamlit serving the existing `st.App` entrypoint, not mocked HTTP handlers.

Existing unit negatives retain malformed UTF-8/percent escapes, unknown/duplicate
fields, private instrument/source/context overrides, dates, alignment, range and row
limits, body/method/header bounds, oversized output, safe exception handling, failed
transport without a second response, rate limits, concurrency/timeout and reserved
WebSocket/admin paths. Readiness remains independent of liveness. Existing AppTest
checks all four pages, synthetic label, illustrative/fixed coverage messaging,
disabled selection and removal of old charts without bridge calls.

Snapshot/DB corrupt/missing-file classification is **not implemented or tested**:
there is no loader/store to corrupt and every dataset stays unavailable. No fake
integrity success is inferred. Clean HTTP server restart proves only that unavailable
state persists; it does not qualify crash recovery, private file isolation or admission.
No real browser/mobile rendering, screenshot, Docker or Azure execution occurred.

## E. Threat assessment, decision matrix and next actions

The API currently imports no provider, file/cache, credential, Worker or generator
implementation. Sanitized public startup and false gate are unchanged. Static
separation and negative tests do not replace target kernel confidentiality evidence.
Read limits are per server process (2/second, 30/minute; independent health allowance),
one receive slot with 250 ms deadline, no queue, 4,096-byte application request/response
bounds. They are neither durable deployment-global controls nor transport ingress
memory caps. A future ready chart cannot fit 2,160 rows plus analytics inside the
current constant-error 4,096-byte REST response cap; it needs an explicit bounded
ready-response design. The bridge's separate 2 MiB capture cap is not automatic REST authority.

| Area | Verdict | Evidence / remaining obligation |
| --- | --- | --- |
| Local shell and unavailable API | **GO for review** | Actual local HTTP, negative queries, AppTest and restart pass |
| Offline fixed generator and analytics | **GO for retained regression evidence** | Independent complete bytes/hashes and all twelve windows pass; not publication acceptance |
| Local candle/indicator publication | **BLOCK** | Frozen cold-request/export rules, unchanged gate, no qualified immutable reader or public runtime |
| Snapshot/SQLite integrity and atomicity | **GAP** | No implementation selected; trust, access, race, crash and rollback proofs absent |
| Confidentiality / G-B2 | **BLOCK for activation** | Target mount, UID, environment, FD, `/proc` and provider-network negatives still required |
| Admission/recovery / G-B1/G-B4 | **BLOCK for generation** | No independent cleanup/exclusivity proof; held supervisor/native work untouched |
| Resource/abuse / G-B3/G-B7 | **GAP/BLOCK for activation** | No durable cross-restart/global proof, ingress protection or measured headroom; no hard CPU claim |
| Functional preservation / G-B5 | **Offline evidence retained** | Cannot be promoted to integrated runtime acceptance |
| F1 operations / G-B6 | **UNTESTED/NO-GO** | No current filesystem/resource/cold-start/restoration/cost evidence collected |
| Production public activation | **NO-GO** | Separate approved architecture, qualification and activation authority required |
| Azure deployment | **NO-GO** | Not authorized; no changes made |

Next actions: owner reviews the exact immutable-publication amendment proposal above;
then separately authorizes a literal-path offline format/export/validator qualification
scope if chosen. Resolve read-only isolation, trust anchors, resource/abuse budgets,
response schemas, corruption/rollback/restart behavior and gate mapping before any
publication integration. Existing fixed window and private-provider exclusions remain
non-negotiable. Target Azure/browser qualification and public activation are distinct
later authorities; no paid infrastructure, Always On assumption or WP07 resurrection.

Rollback: this PR changes tests and documentation only. Declining it leaves PR #333's
safe unavailable behavior unchanged. If merged later, any rollback goes through a normal
reviewed revert; no reset/clean, private-state migration, database repair, deployment,
automatic regeneration, alternative provider fallback or gate override is part of it.

This focused PR is for owner review only. No automatic merge, issue closure, deployment
or public generator activation is authorized or claimed.

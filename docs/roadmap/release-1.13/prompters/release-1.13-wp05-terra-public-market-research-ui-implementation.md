# Release 1.13 WP05 --- Terra Public Market Research UI Implementation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

-   **GPT-5.6 Luna** owns the accepted Release 1.13 product/UI
    architecture and later substantive acceptance.
-   **GPT-5.6 Terra** owns this bounded WP05 implementation and
    empirical validation.
-   **GPT-5.6 Sol** is supporting analysis only.

## Canonical predecessor

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

Expected canonical `origin/main`:

`cc96c638b3c255499412bd79aca3953cb2973902`

Verify live repository truth before mutation.

Expected WP05 issue: `#292`

WP01-WP04 must remain accepted, merged, and lifecycle-complete.

## Purpose

Implement the Release 1.13 public **Market Research** visualization
surface:

-   BTC/USD and ETH/USD;
-   1h, 4h, and 1d intervals;
-   1D, 7D, 30D, and 90D ranges;
-   default BTC/USD, 1h, 30D;
-   historical candlesticks;
-   volume;
-   OHLCV hover inspection;
-   bounded client-side zoom/pan;
-   responsive Streamlit layout;
-   cache/local-first consumption of the governed WP04 read model.

WP05 is visualization implementation, not trading functionality.

## Frozen first-screen contract

The accepted first screen remains substantially:

`AI Quant Trading Research                                      ● Data ready`

`Historical Market Research`

Navigation:

`Market Research | ML & Automation Studies | System Health`

Market controls:

`BTC/USD | ETH/USD`

Interval:

`1h | 4h | 1d`

Range:

`1D | 7D | 30D | 90D`

Default:

`Market Research / BTC/USD / 1h / 30D`

Primary content:

-   instrument title;
-   historical candlestick chart;
-   volume;
-   OHLCV hover;
-   zoom/pan;
-   truthful data state.

WP06, not WP05, owns the final provenance/boundary messaging, ML &
Automation Studies informational copy, System Health relationship, and
controlled public copy refinement.

WP05 may create the minimum navigation/frame placeholders required to
preserve the accepted structure, but must not consume WP06's
documentation/policy scope.

## Mandatory repository inspection before mutation

Inspect at least:

1.  merged WP01 contract;
2.  merged WP02 market-data contracts;
3.  merged WP04 cache/read-model contracts and service;
4.  `python/presentation/realtime_financial_visualization.py`;
5.  `python/presentation/visualization_read_model.py`;
6.  all current tests for those presentation modules;
7.  `requirements.txt`;
8.  Dockerfile only to understand runtime packaging;
9.  existing Python integration/boundary mechanisms between .NET and
    presentation;
10. existing Worker composition only as an inspected/protected boundary;
11. Release 1.13 file manifest;
12. current application/infrastructure tests relevant to no-bypass
    architecture.

Before first edit, report the minimum **exact literal mutation
allowlist**.

No directory wildcards.

Do not mutate any path before the allowlist is frozen.

## Critical integration gate

WP04 deliberately did not modify Worker composition or Python.

Therefore, before implementing UI rendering, determine the smallest
repository-native way for Streamlit to consume the WP04
presentation-safe historical read result without bypassing:

`Streamlit -> governed read model/service -> cache -> IHistoricalMarketDataProvider -> Vike`

The UI must never call Vike or Twelve Data directly.

The browser must never receive a provider credential.

Do not invent a second market-data acquisition pipeline in Python.

Do not parse Vike responses in Python.

Do not reproduce WP04 freshness/cache/provider logic in Python.

If the correct integration requires a production .NET/Worker/composition
path that was not anticipated by the WP05 authority, include that exact
path in the proposed pre-mutation allowlist **only if it is the minimum
bridge inherently required for WP05** and explain why.

If the bridge would require a new service, schema, broad architectural
change, or more than a minimal repository-native handoff, stop at
governance and report the required authority rather than bypassing WP04.

## Expected mutation categories

The final allowlist may reasonably contain:

-   `python/presentation/realtime_financial_visualization.py`;
-   a new narrowly scoped historical-market presentation/read-model
    consumer module if separation materially improves correctness;
-   focused Python presentation tests;
-   `requirements.txt` if Plotly is selected and approved below;
-   the minimum existing .NET composition/handoff path only if
    inspection proves it is required to expose WP04's governed result;
-   focused .NET tests for that bridge if needed;
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`;
-   `docs/roadmap/release-1.13/prompters/release-1.13-wp05-terra-public-market-research-ui-implementation.md`.

This is not a wildcard grant. Freeze exact paths first.

Root `README.md` remains forbidden.

## Existing presentation preservation

The current Streamlit module contains Release 1.10-1.12
visualization/system-health behavior and tests.

Do not casually delete or invalidate historical accepted semantics
merely to replace the first screen.

Refactor only as necessary to make **Market Research** the default
Release 1.13 surface while preserving required legacy/system-health
capabilities behind the accepted navigation.

Existing tests may be updated only when Release 1.13 intentionally
supersedes a presentation expectation and the change is within the
frozen allowlist.

Do not weaken integrity/no-bypass tests to make the UI pass.

## Plotly dependency gate

WP01 selected Plotly conceptually because Streamlit's existing line
chart does not satisfy candlestick + volume requirements.

Plotly is **not currently pinned in `requirements.txt`**.

Before editing `requirements.txt`:

1.  confirm Plotly is still the smallest suitable solution;
2.  select an exact version compatible with Python 3.13 and Streamlit
    1.61.1;
3.  verify its license from first-party/package metadata;
4.  verify installation does not introduce prohibited paid/runtime
    services;
5.  assess package/bundle impact for Azure F1;
6.  record the selected exact pin and evidence in the implementation
    report.

Only the direct Plotly pin needed for the chart is authorized. Do not
opportunistically upgrade numpy, pandas, scikit-learn, or Streamlit.

If Plotly cannot satisfy these constraints, stop before dependency
mutation and report the governance restriction.

## Market controls

Implement exactly:

Symbols: - `BTC/USD` - `ETH/USD`

Intervals: - `1h` - `4h` - `1d`

Ranges: - `1D` - `7D` - `30D` - `90D`

Defaults: - BTC/USD; - 1h; - 30D.

Do not add other instruments, intervals, exchanges, providers, or
ranges.

Provider selection is not a public control.

## Chart requirements

Render:

-   OHLC candlesticks;
-   volume aligned to the same time axis;
-   OHLCV hover information;
-   zoom;
-   pan;
-   responsive width/layout.

Prefer one coherent Plotly figure with candlestick and volume
panels/traces when this gives the clearest bounded UI.

Do not add:

-   SMA/EMA;
-   RSI;
-   MACD;
-   Bollinger Bands;
-   signals;
-   predictions;
-   buy/sell markers;
-   portfolio widgets;
-   order controls;
-   strategy overlays.

Those are outside WP05/Release 1.13 or reserved for 1.14+.

## Client interaction / provider-call rule

Hover, zoom, pan, and ordinary chart interaction must be client-side and
must not trigger provider acquisition.

Streamlit reruns caused by UI controls must consume the governed WP04
cache/read-model path.

Do not create automatic provider refresh on every rerun.

Do not add a high-frequency autorefresh loop for historical candles.

If an explicit data refresh action is retained, it must still route
through the governed service/cache and obey freshness semantics.

## Data-state behavior

WP05 must correctly render the presentation-safe WP04 states without
leaking internals.

At minimum:

-   fresh data -\> chart;
-   stale-but-usable data -\> chart remains usable, with state available
    for WP06 disclosure;
-   empty successful dataset -\> controlled empty state;
-   unavailable/no usable cache -\> controlled unavailable state;
-   malformed presentation handoff -\> controlled error state.

The public unavailable text ultimately required is:

`Historical market data is temporarily unavailable. Please try again later.`

WP05 may implement this accepted text because it is already frozen by
WP01, but do not expose provider URLs, credentials, HTTP traces, stack
traces, filesystem paths, SQLite paths, exception text, or raw failure
payloads.

## Provenance boundary

The chart data must retain enough presentation metadata for WP06 to
show:

`Data source: Vike • Historical OHLCV • Last updated: <UTC timestamp>`

WP05 need not finalize all WP06 copy, but must not discard
provider/provenance/last-updated information from the governed result.

Never display the API key or provider request URL.

## Responsive behavior

The Market Research chart must be usable at normal desktop widths and
degrade reasonably at narrower Streamlit layouts.

Avoid fixed pixel widths that force horizontal scrolling.

Use Streamlit/Plotly responsive container behavior.

Do not introduce a frontend framework or custom JavaScript bundle merely
for responsiveness.

## Resource constraints

Preserve:

-   Azure App Service Linux F1;
-   60 CPU minutes/day;
-   1 GB storage;
-   `$0.00` recurring infrastructure.

Do not add:

-   managed cache;
-   database service;
-   message broker;
-   background polling service;
-   analytics service;
-   CDN requirement;
-   paid chart service.

## Security

Provider credentials remain server-side.

No API key in:

-   Python constants;
-   chart JSON;
-   browser-visible state;
-   query parameters;
-   exception messages;
-   tests/fixtures;
-   committed files.

No direct browser call to Vike/Twelve Data.

## Twelve Data boundary

Twelve Data remains private/internal research infrastructure.

Do not:

-   modify its adapter;
-   use it as public fallback;
-   expose its values through the public Market Research path;
-   expose its key;
-   route Streamlit directly to it.

Run existing Twelve Data regression tests.

## WP06 boundary

WP06 remains separately governed.

Do not fully implement:

-   final Vike attribution/provenance copy beyond what WP05 requires to
    preserve data;
-   final Twelve Data research-boundary explanatory section;
-   ML & Automation Studies roadmap content;
-   final System Health relationship/reorganization;
-   final footer/policy copy beyond preserving the accepted structure.

Do not begin WP07 deployment validation.

## Package/schema/deployment boundary

Apart from the specifically governed Plotly pin, no dependency changes.

No schema/database migration.

No Azure mutation.

No Docker/GHCR publication.

Do not deploy.

Do not create tags or GitHub Releases.

Do not modify root README.

## Test requirements

Add/update deterministic tests proving, as applicable:

-   default Market Research screen;
-   default BTC/USD;
-   default 1h;
-   default 30D;
-   exactly two symbol options;
-   exactly three interval options;
-   exactly four range options;
-   canonical selection mapping;
-   historical read-model input projection;
-   candlestick trace uses open/high/low/close;
-   volume trace uses canonical volume;
-   time axis uses canonical UTC open times;
-   hover exposes OHLCV;
-   chart supports bounded zoom/pan;
-   responsive container rendering;
-   fresh state renders chart;
-   stale state keeps chart usable;
-   empty success is distinct from unavailable;
-   unavailable uses controlled public text;
-   raw exceptions/provider URLs/filesystem paths are not rendered;
-   chart interaction configuration itself does not perform provider
    calls;
-   Python contains no Vike/Twelve Data acquisition implementation;
-   no provider key is browser-visible;
-   provenance/last-updated metadata survives projection for WP06;
-   existing System Health/legacy presentation invariants remain valid
    where not superseded.

If a .NET handoff bridge is required, add tests proving:

-   it calls the governed WP04 read service, not the provider directly;
-   request selection maps exactly to canonical symbol/interval/range;
-   output contains canonical OHLCV plus presentation-safe metadata;
-   no secrets/raw exceptions are serialized;
-   no duplicate cache/freshness/provider policy is created.

## Validation

Before declaring PASS:

-   prove predecessor `cc96c638b3c255499412bd79aca3953cb2973902`;
-   prove exact changed-path allowlist;
-   prove README unchanged;
-   `git diff --check`;
-   isolated secret scan;
-   Release .NET build;
-   relevant Application/Infrastructure/Vike/Twelve Data/Architecture
    tests;
-   all relevant Python presentation tests;
-   focused new WP05 UI tests;
-   dependency installation/import test in the qualified repository
    environment if Plotly added;
-   exact dependency diff proving only authorized Plotly pin;
-   verify Plotly license/version/bundle evidence;
-   verify no schema change;
-   verify no Azure/Docker/GHCR/deployment;
-   verify no direct provider/browser bypass;
-   verify no Release 1.14 indicator work;
-   verify issue #292 remains open/non-Done;
-   verify milestone #64 remains open.

## Retry-until-governance-boundary rule

Do not stop merely because implementation, integration, dependency
installation, UI tests, Python tests, .NET tests, build, lint, or
validation initially fails.

Diagnose and make corrections permitted by the frozen WP05 allowlist,
rerun checks, and continue until acceptance conditions pass.

Report ordinary corrected failures as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop only if the next corrective action requires:

-   bypassing WP04;
-   direct Python/browser provider acquisition;
-   Vike/Twelve Data adapter change;
-   WP01/WP02/WP04 contract change;
-   unapproved dependency beyond Plotly;
-   schema migration;
-   broad new service/architecture;
-   non-allowlisted path;
-   Azure/Docker/GHCR/deployment;
-   root README;
-   Release 1.14/2.0;
-   secrets;
-   merge/lifecycle action;
-   WP06/WP07 work outside the bounded overlap described above.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

State the exact restriction, evidence, and required authority.

## Git workflow

Use a dedicated WP05 branch.

Do not push directly to `main`.

After all implementation gates pass, commit the exact authorized paths
and open a bounded PR.

Do not merge.

Do not close issue #292.

Do not mark WP05 Done.

Do not begin WP06.

## Acceptance boundary

A WP05 implementation PASS creates a candidate only.

A separate GPT-5.6 Luna substantive acceptance must review:

-   integration/no-bypass architecture;
-   Plotly dependency decision;
-   chart semantics;
-   control matrix/defaults;
-   data states;
-   client interaction behavior;
-   security;
-   F1 resource impact;
-   preservation of existing presentation behavior.

Only after separate acceptance and lifecycle completion may WP06 begin.

## Required final report

Return:

-   inspected canonical main SHA;
-   issue #292 / Project state;
-   branch;
-   exact files inspected;
-   exact literal allowlist frozen before mutation;
-   historical read-model integration mechanism selected;
-   proof there is no Python/browser provider bypass;
-   Plotly version/license/compatibility/bundle decision;
-   exact dependency diff;
-   exact files created/modified;
-   control/default implementation;
-   chart implementation;
-   hover/zoom/pan behavior;
-   responsive behavior;
-   fresh/stale/empty/unavailable behavior;
-   provenance metadata preservation;
-   tests and results;
-   existing presentation regression results;
-   .NET regression results;
-   Vike/Twelve Data preservation;
-   security/secret results;
-   F1/resource findings;
-   technical failures corrected;
-   governance restrictions encountered;
-   README unchanged;
-   confirmation no schema/Azure/Docker/GHCR/deployment/indicator work
    occurred;
-   commit SHA(s);
-   PR number/URL;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP05 PUBLIC MARKET RESEARCH UI IMPLEMENTATION: PASS`

or

`RELEASE 1.13 WP05 PUBLIC MARKET RESEARCH UI IMPLEMENTATION: BLOCKED`

A PASS does not authorize merge or WP06.

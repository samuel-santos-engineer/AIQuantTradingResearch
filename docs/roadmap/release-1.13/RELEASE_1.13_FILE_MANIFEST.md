# Release 1.13 File Manifest

## Planning-authority mutation set

This authority may add or modify only these governance records:

- `docs/project/ROADMAP.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`
- `docs/roadmap/release-1.13/prompters/release-1.13-historical-market-visualization-provider-abstraction-governance-authority-luna.md`

`README.md` is forbidden and must remain unchanged. Historical Release 1.12 records are excluded because they are historical authority records, not current-roadmap surfaces.

## Future ownership

No source, test, dependency, configuration, deployment, or UI path is authorized now. Each Release 1.13 WP must supply its own exact literal path allowlist before implementation. WP01 owns the contract/selection record listed above and its `release-1.13-` authority record; WP02-WP04 must later enumerate literal provider/adapter/cache/read-model paths; WP05-WP06 must later enumerate literal presentation/read-model consumer paths; WP07 must later enumerate literal validation/deployment-evidence paths; and WP08 must later enumerate literal documentation/runbook/acceptance paths.

## WP02 frozen implementation mutation set

WP02 may add or modify only these exact paths for the provider-independent historical market-data abstraction:

- `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataContracts.cs`
- `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataContractsTests.cs`
- `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
- `docs/roadmap/release-1.13/prompters/release-1.13-wp02-terra-provider-independent-historical-market-data-abstraction-implementation.md`

This set excludes existing Twelve Data implementation paths, provider adapters, cache/read-model/persistence paths, Streamlit/UI paths, dependency manifests, and deployment configuration.

## WP03 frozen implementation mutation set

WP03 may add or modify only these exact paths for the Vike historical-OHLCV adapter:

- `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeConfiguration.cs`
- `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeHistoricalMarketDataProvider.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/VikeHistoricalMarketDataProviderTests.cs`
- `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
- `docs/roadmap/release-1.13/prompters/release-1.13-wp03-terra-vike-historical-ohlcv-adapter-implementation-validation.md`

This set excludes Twelve Data changes, application-contract changes, dependency manifests, cache/read-model/persistence, Streamlit/UI, and deployment configuration.

## Forbidden paths and artifacts

Forbidden: root `README.md`; historical Release 1.12 records; credentials, API keys, certificates, machine-local configuration, caches/build outputs; Azure profile material; unrelated packages; database migrations; Docker/GHCR publication artifacts; tags/releases; Release 1.14 implementation; and Release 2.0 implementation.

## Validation

Before a planning PR is proposed, prove the exact path set, Markdown formatting, no secrets, unchanged README, all `release-1.13-` prompter naming, preserved Release 2.0/2.1/2.2/2.3 identities, no Product Release 1.11 resurrection, and coherent sequence `1.10 -> 1.12 -> 1.13 -> 1.14 -> 2.0`.

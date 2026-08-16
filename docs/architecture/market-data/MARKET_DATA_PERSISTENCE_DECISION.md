# Market Data Persistence Decision

## Status

Accepted for Release 1.1

## Decision context

- Decision date: 2026-08-15
- Owner/context: AIQuantTradingResearch Release 1.1 / WP02
- Evidence: [Market Data Persistence Technology Assessment](MARKET_DATA_PERSISTENCE_ASSESSMENT.md)

## Selected persistence technology

**SQLite**, accessed from .NET through the accepted `Microsoft.Data.Sqlite` integration.

SQLite is the single primary durable persistence technology selected for Release 1.1 historical market-data observations.

## Selection rationale

SQLite provides the smallest credible durable vertical slice: zero monetary cost, embedded offline operation, ACID transactions, a cross-platform single-file database, deterministic temporary-file tests, clean-checkout bootstrap without a service, and a Microsoft-maintained lightweight ADO.NET provider. It preserves the existing architecture while avoiding server, credential, port, and container lifecycle that Release 1.1 does not require.

## Rejected alternatives

- **PostgreSQL:** mature .NET support, concurrency, schema evolution, and portfolio relevance, but its server, configuration, credentials, and CI lifecycle are disproportionate to the bounded local Release 1.1 slice. Existing PostgreSQL-related central package versions are unused evidence, not a prior decision.
- **DuckDB:** credible embedded durability and strong analytical capability, but its .NET client is officially secondary-tier and its analytical specialization and write-concurrency model add uncertainty without a current release requirement.

## Known constraints

- SQLite permits only one pending writer transaction per database.
- SQLite uses dynamic typing and four primitive storage classes.
- WP14 proves lossless decimal-price and offset-aware timestamp round trips through Infrastructure tests.
- Complex schema changes may require controlled table reconstruction.
- The implemented Release 1.1 slice uses `Persistence:DatabasePath`, schema version 1, disposable isolated test files, immutable/idempotent/conflict semantics, and bounded failure mapping.

## Release 1.1 usage boundary

- SQLite is the Release 1.1 durable persistence mechanism for historical market-data observations only.
- Storage-specific mechanics belong in Infrastructure.
- Domain and Application remain independent of SQLite and all storage-engine types.
- Provider acquisition and persistence remain separate concerns; Twelve Data cannot appear in persistence contracts.
- Tests must run offline without live market-data access and use isolated disposable state where persistence is exercised.
- Secrets or credentials must never be committed; the bounded SQLite model should not require database credentials.
- Schema, provider-independent contracts, physical model, package/version selection, migration mechanism, DI, Worker integration, and permanent tests remain owned by WP03–WP16.
- This is not a permanent platform-wide database commitment beyond Release 1.1.

## Architectural consequences

- Infrastructure uses a local SQLite database file behind provider-independent Application contracts.
- Clean-checkout initialization and reconstruction durability must be executable and testable.
- Later tests must distinguish in-memory behavioral checks from file-backed durability proof.
- Operational complexity stays local, but file locking, placement, cleanup, and integrity become explicit Infrastructure concerns.

## Downstream implications

- **WP03:** define provider-independent historical-observation persistence semantics without SQLite types.
- **WP04–WP05:** define and orchestrate storage-independent Application contracts.
- **WP06–WP10:** own SQLite physical representation, connection/bootstrap, persistence, retrieval, and bounded failure mapping.
- **WP07:** owns evidence-backed package/version changes if still required.
- **WP11–WP12:** own configuration, DI, and Worker composition without leaking storage mechanics.
- **WP13–WP15:** own permanent behavior, Infrastructure durability, architecture enforcement, and documentation.
- **WP16:** validates the cumulative implementation without corrective design.

## Not authorized by this decision

This WP02 decision did not authorize implementation at selection time. Subsequent WP06–WP15 authorities govern the implemented schema, connection, persistence, tests, DI, and Worker composition. It still does not authorize migrations beyond schema version 1, retries, or a platform-wide database commitment.

## Reconsideration triggers

Reassess this Release 1.1-bounded decision if authoritative requirements introduce multi-process concurrent writes, remote shared access, operational high availability, data volumes or analytical workloads shown to exceed SQLite's validated bounds, or a material .NET/platform compatibility failure. Reconsideration requires separate authority and evidence; it is not implicit permission to add another primary database.

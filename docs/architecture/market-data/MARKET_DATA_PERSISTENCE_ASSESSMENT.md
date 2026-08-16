# Market Data Persistence Technology Assessment

> Historical WP02 discovery artifact. Its future/undecided language records the state before WP06–WP15 implementation; the accepted current state is documented in `MARKET_DATA_PERSISTENCE_DECISION.md` and the solution architecture documents.

## Context

- Release: Phase 3 — Release 1.1: Market Data Persistence Foundation
- Work package: WP02 — Persistence Technology Discovery
- Evidence date: 2026-08-15
- Decision owner: AIQuantTradingResearch Release 1.1 governance

## Decision question

Which single technology provides the smallest credible durable persistence mechanism for historical market-data observations while remaining free, offline, deterministic, cross-platform, and confined to Infrastructure?

## Scope

This assessment selects technology only. It does not authorize packages, schema, migrations, contracts, source code, dependency injection, Worker behavior, or tests. Those decisions remain with WP03–WP16.

## Repository baseline

WP01 established that Domain owns provider-independent observation values, Application owns research contracts and orchestration, Infrastructure owns Twelve Data transport and normalization, and Worker owns composition. No durable storage abstraction, engine, schema, migration, connection configuration, or runtime database exists.

`Directory.Packages.props` contains unused versions for `Npgsql.EntityFrameworkCore.PostgreSQL`, Entity Framework tooling, and `Testcontainers.PostgreSql`. No project references them. They are inventory evidence only: they are not a prior selection or an architectural requirement, though they could reduce later package-governance work if PostgreSQL were selected.

## Architectural constraints

- Preserve `Domain → none`, `Application → Domain`, `Infrastructure → Application`, and `Worker → Application, Infrastructure`.
- Keep storage mechanics inside Infrastructure and keep Twelve Data outside persistence contracts.
- Require real durability and transactional writes without a live provider.
- Support deterministic offline tests and clean-checkout bootstrap on Windows, Linux, and GitHub-hosted runners.
- Optimize for the smallest credible vertical slice, not maximum platform capability.
- Do not make a platform-wide commitment beyond Release 1.1.

## Mandatory criteria and rating method

Every candidate is assessed against the thirteen mandated criteria. Ratings are `Strong`, `Acceptable`, `Weak`, or `Disqualifying`. They are qualitative: no weights or false numerical precision are used. No candidate has a hard disqualifier for this release, so engineering judgment resolves the trade-off.

## Candidate set

1. **SQLite** — embedded relational baseline, credible because Release 1.1 needs a small transactional durable store.
2. **PostgreSQL** — client/server relational baseline, credible because it offers mature transactions, schema evolution, .NET access, and strong industry relevance.
3. **DuckDB** — materially different embedded analytical database, credible because historical market data is read-intensive and analytical, while still supporting persistent files and transactions.

The set is bounded to three serious candidates. Adding document stores or cloud services would not improve this release decision: they add operational or semantic complexity without a demonstrated requirement.

## Evidence method

Claims use current official database and .NET/provider documentation. CI and repository-fit conclusions are explicitly engineering inferences from documented deployment models and the repository's existing .NET build.

### Primary sources

- SQLite: [About SQLite](https://sqlite.org/about.html), [features and supported platforms](https://www.sqlite.org/features.html), [transaction guarantees](https://www.sqlite.org/transactional.html), and [ALTER TABLE support](https://sqlite.org/lang_altertable.html).
- .NET/SQLite: [Microsoft.Data.Sqlite overview](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/), [transactions](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/transactions), [data types](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/types), and [native bundles](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/custom-versions).
- PostgreSQL: [license](https://www.postgresql.org/about/licence/), [supported platforms](https://www.postgresql.org/docs/current/supported-platforms.html), [transaction processing](https://www.postgresql.org/docs/current/transaction-id.html), and [table evolution](https://www.postgresql.org/docs/current/ddl-alter.html).
- .NET/PostgreSQL: [Npgsql overview](https://www.npgsql.org/) and [basic usage and transactions](https://www.npgsql.org/doc/basic-usage.html).
- DuckDB: [client support tiers](https://duckdb.org/docs/current/clients/overview), [persistent databases](https://duckdb.org/docs/stable/clients/cli/overview), [transactions](https://www.duckdb.org/docs/lts/sql/statements/transactions), [concurrency](https://duckdb.org/docs/stable/connect/concurrency.html), [ALTER TABLE](https://duckdb.org/docs/lts/sql/statements/alter_table), and [platform installation](https://duckdb.org/install/).

## Per-candidate evidence

### SQLite

- **Model/deployment:** Embedded relational engine; a complete database is a single cross-platform file, with no server process or administration.
- **.NET path:** Expected future dependency impact is `Microsoft.Data.Sqlite`, a lightweight Microsoft-maintained ADO.NET provider. The main package includes a native SQLite bundle across platforms. No package is installed by WP02.
- **Cost/offline:** SQLite core is public domain and works fully offline.
- **Durability/transactions:** SQLite documents ACID and serializable transactions, including resilience to process, OS, and power interruption.
- **Testing/bootstrap:** A test can use a unique temporary file to prove reconstruction durability, or an in-memory database only where durability is not the subject. Bootstrap can create a new local file from repository-controlled initialization. This is an engineering inference from the embedded single-file model.
- **Platforms/CI:** SQLite documents Windows and Linux support. The bundled .NET provider makes clean GitHub runner bootstrap credible without a service container.
- **Schema evolution:** Core `ALTER TABLE` covers rename/add/drop operations; more complex changes require controlled table reconstruction. Feasible, but less capable than PostgreSQL.
- **Dependency/operations:** One direct ADO.NET package is the likely minimum; no daemon, port, account, password, or container is inherently required.
- **Advantages:** Smallest operational surface; deterministic file isolation; real relational constraints and transactions; easy clean-checkout use.
- **Disadvantages/risks:** One pending writer per database; dynamic typing and four primitive storage classes require explicit mapping and constraints. Microsoft documents `decimal` and `DateTimeOffset` as text representations, so later WPs must prove exact round trips rather than assume database numeric/time semantics.

### PostgreSQL

- **Model/deployment:** Separate client/server relational database.
- **.NET path:** Expected future dependency could be Npgsql's ADO.NET provider or its EF Core provider. Npgsql documents data sources, pooling, parameters, and transactions. No package is installed by WP02.
- **Cost/offline:** PostgreSQL and Npgsql use liberal open-source licenses and can run locally without monetary cost.
- **Durability/transactions:** Mature transactional server with explicit transactions, isolation, savepoints, and MVCC.
- **Testing/bootstrap:** Deterministic testing is credible through an isolated local server or disposable container, but it requires process/container lifecycle and readiness management. This is more operational work than an embedded file.
- **Platforms/CI:** PostgreSQL officially supports current Windows and Linux. GitHub CI is feasible through service/container orchestration, an engineering inference from that server model.
- **Schema evolution:** Strong `ALTER TABLE` support for columns, constraints, defaults, types, and names.
- **Dependency/operations:** Requires a .NET provider plus a server installation or container; configuration normally includes host, database, identity, and potentially credentials.
- **Advantages:** Strong concurrency, mature schema evolution, production-operational relevance, and excellent portfolio recognition.
- **Disadvantages/risks:** Server lifecycle, credentials/configuration, ports, clean-checkout provisioning, and CI services exceed the minimum needs of the Release 1.1 local durable slice.

### DuckDB

- **Model/deployment:** Embedded columnar analytical SQL database with transient or persistent file operation.
- **.NET path:** DuckDB lists .NET among its clients but classifies it as secondary support: it receives features without community-support coverage. A future DuckDB .NET package would be required; none is installed by WP02.
- **Cost/offline:** Local embedded operation is open source and requires no hosted service.
- **Durability/transactions:** Supports persistent databases, ACID transactions, rollback, and snapshot isolation.
- **Testing/bootstrap:** Unique temporary files make deterministic offline tests and clean bootstrap credible, by inference from its embedded persistent-file model.
- **Platforms/CI:** Official distributions cover Windows and Linux; service-free GitHub runner use is credible.
- **Schema evolution:** Transactional `ALTER TABLE` supports common additions, removals, renames, defaults, types, and primary keys.
- **Dependency/operations:** Embedded and low-operations, but the .NET integration has a weaker official support tier than SQLite's Microsoft provider.
- **Advantages:** Strong analytical query orientation and efficient local columnar processing for future research workloads.
- **Disadvantages/risks:** Stable embedded read-write operation is centered on a single process; multi-process writes add emerging or external coordination. The analytical strengths are not required to prove the minimal Release 1.1 persistence slice.

## Mandatory criteria matrix

| Mandatory criterion | SQLite | PostgreSQL | DuckDB |
| --- | --- | --- | --- |
| 1. Zero monetary cost | Strong — public domain | Strong — liberal open source | Strong — open-source local use |
| 2. Local/offline operation | Strong — embedded | Acceptable — local server required | Strong — embedded |
| 3. .NET support | Strong — Microsoft.Data.Sqlite | Strong — mature Npgsql | Acceptable — secondary-tier .NET client |
| 4. Durability | Strong — ACID single file | Strong — mature server durability | Strong — persistent ACID database |
| 5. Transaction capability | Strong — serializable; one writer | Strong — concurrency, isolation, savepoints | Strong — ACID and snapshot isolation |
| 6. Deterministic testing | Strong — isolated temporary file | Acceptable — isolated server/container | Strong — isolated temporary file |
| 7. Clean-checkout bootstrap | Strong — create local file | Weak — provision/start server and database | Strong — create local file |
| 8. Windows/Linux compatibility | Strong — documented cross-platform | Strong — both officially supported | Strong — distributions for both |
| 9. GitHub CI feasibility | Strong — no service required | Acceptable — service/container orchestration | Strong — no service required |
| 10. Schema evolution feasibility | Acceptable — limited ALTER; rebuild for complex change | Strong — broad ALTER TABLE | Strong — broad transactional ALTER TABLE |
| 11. Dependency/package impact | Strong — likely one direct provider package | Weak — provider plus server/test lifecycle | Acceptable — provider/native integration |
| 12. Operational complexity | Strong — zero-configuration embedded engine | Weak — server, port, identity, lifecycle | Strong — embedded, with concurrency caveats |
| 13. Portfolio/recruiting relevance | Acceptable — demonstrates disciplined embedded persistence | Strong — widely transferable server-database skills | Acceptable — distinctive analytical technology |

## Trade-off analysis

PostgreSQL wins concurrency, schema evolution, and recruiting familiarity, but Release 1.1 does not require a networked multi-user server. Its service bootstrap and credential surface are real costs, not portfolio benefits for this narrow slice.

DuckDB is operationally close to SQLite and more analytical, but its .NET client support is officially secondary and its analytical specialization is premature for basic durable writes and deterministic retrieval.

SQLite meets every hard constraint with the smallest package and operational footprint. Its limitations are bounded and testable: later work must explicitly preserve decimal and offset-aware timestamp values and account for single-writer behavior.

## Security and credential implications

SQLite needs no network listener, user account, or database password for the bounded local file model, reducing secret and attack surface. File locations and permissions still require deliberate handling. PostgreSQL introduces credentials and a listening service; DuckDB resembles SQLite's local-file posture. No candidate was contacted or provisioned during WP02.

## Local developer experience

SQLite offers the shortest path from clean checkout to a durable local store: restore the eventual provider package, initialize a repository-controlled file, and use per-test temporary paths. PostgreSQL adds installation/container readiness. DuckDB is similarly simple operationally, but weaker .NET support increases integration uncertainty.

## CI and testing implications

SQLite supports service-free, offline tests with unique temporary database files and explicit cleanup. A reconstruction test must close and reopen the file; in-memory tests alone cannot prove durability. PostgreSQL needs isolated server/database lifecycle. DuckDB also supports temporary-file testing but would add a less-established .NET boundary.

## Package and dependency implications

The likely minimum future SQLite integration is `Microsoft.Data.Sqlite`; WP07 owns any package addition and exact version. The existing PostgreSQL/EF/Testcontainers central entries stay unchanged and unused. This assessment installs or restores no persistence-specific dependency.

## Schema-evolution implications

SQLite evolution is feasible through repository-controlled versioning and transactions, with table reconstruction for changes beyond its limited direct `ALTER TABLE` operations. The exact migration mechanism, schema, numbering, and initialization code remain undecided and belong to later work packages.

## Operational-complexity implications

SQLite removes server provisioning, network readiness, accounts, and database credentials from Release 1.1. It introduces responsibility for file placement, locking, cleanup, backup expectations, and atomic initialization. Those concerns are narrower than operating PostgreSQL and no broader than the release requires.

## Portfolio and recruiting implications

PostgreSQL has the strongest immediate keyword recognition. SQLite still provides credible evidence of relational modeling, transactions, durability, deterministic integration tests, schema lifecycle, and clean architecture. Choosing the smallest correct mechanism—and documenting why a larger server is premature—is itself useful engineering evidence.

## Risks and unresolved questions

- WP03 must define provider-independent persistence semantics before physical mapping.
- Later work must prove lossless `decimal` and `DateTimeOffset` round trips under SQLite's storage model.
- Duplicate/conflict semantics, ordering, transaction boundaries, schema details, migration strategy, file locations, cleanup, and failure mapping remain deliberately unresolved.
- Concurrency beyond the bounded Worker/test process could trigger reconsideration.
- The exact future package version must be selected under WP07 authority and current compatibility evidence.

## Recommendation

The WP02 recommendation was **SQLite**, accessed in .NET through the then-future minimum `Microsoft.Data.Sqlite` integration; subsequent WP07 implementation accepted that recommendation.

This recommendation is limited to Release 1.1 and does not make SQLite the permanent platform-wide database.

## Authorization boundary

This assessment authorizes no implementation. It does not authorize packages, schema, migrations, contracts, source or test changes, DI, Worker behavior, runtime files, or WP03 work.

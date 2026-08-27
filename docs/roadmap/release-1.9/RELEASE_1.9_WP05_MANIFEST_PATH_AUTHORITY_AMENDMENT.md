# Release 1.9 WP05 — Manifest / Path-Authority Amendment

Status: normative supplement to `RELEASE_1.9_FILE_MANIFEST.md` for WP05
implementation only.

This amendment authorizes only the minimum files needed to implement the
already-fixed local atomic-JSON handoff, Worker lifecycle ownership, bounded
consumer refresh, and read-only Streamlit presentation. It does not change
transport, envelope, revision, refresh, schema, dependency, or WP06 semantics.

## Existing WP05 paths retained

These accepted-manifest paths remain authorized:

- `python/presentation/realtime_financial_visualization.py` — WP05–WP07
  production Streamlit adapter; WP05 may add only the initial bounded
  consumer/rendering surface and must not add WP06 visualization scope.
- `python/presentation/visualization_read_model.py` — exclusive WP05
  production parser, configuration, revision comparison, retry, and cache
  surface; it remains read-only and must not access SQLite, providers, or
  pipeline/feature code.

## Added production paths

| Path | Ownership | WP05-authorized concern | Forbidden adjacent concern |
| --- | --- | --- | --- |
| `src/AIQuantTradingResearch.Worker/VisualizationHandoffOptions.cs` | Exclusive WP05 | Worker binding/validation for `Visualization:HandoffPath` and `Visualization:RefreshIntervalSeconds`, canonical default-path values, and Worker startup-session options. | Replay/pipeline configuration, process supervision, Streamlit launch, schema, persistence, provider, or package changes. |
| `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs` | Exclusive WP05 | Serialization of the existing WP04 envelope, Worker-owned parent-directory creation, owned temporary sibling cleanup, UTF-8 flush/close, atomic replacement, and prior canonical-file startup cleanup. | New envelope fields, new transport, SQLite/provider access, durable presentation history, feature computation, or revision/state changes. |
| `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublishingStore.cs` | Exclusive WP05 | Narrow decorator that publishes only after the existing `IVisualizationReadModelStore` accepts a complete envelope, while preserving the existing in-memory store and publication result. | Replacement of `AtomicVisualizationReadModelStore`, producer semantics, second read model, persistence, or cross-process coordination. |

The publisher/decorator may use only standard .NET libraries and existing
project references. No package or project-file change is authorized.

## Added test paths

| Path | Ownership | Authorized coverage |
| --- | --- | --- |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs` | Exclusive WP05 | Default/absolute path validation, relative rejection, startup cleanup, owned temporary-file cleanup, unrelated-file preservation, valid contract-version JSON, Historical and Replay publication, all applicable WP04 states, and deterministic old-or-new atomic visibility. |
| `python/presentation/test_visualization_read_model.py` | Exclusive WP05 | Python path/refresh configuration, UTF-8 parser and exact contract version, state validation, Historical/Replay revision comparison, missing/corrupt/unknown-version handling, two-attempt/50-ms retry, last-good retention, one-envelope cache bound, and read-only transport warnings. It must use the repository’s existing no-pytest executable-test convention. |

The existing manifest path
`python/presentation/test_realtime_financial_visualization.py` remains
WP09-owned. WP05 must not modify it under this amendment.

## Narrow shared-path exceptions

### `src/AIQuantTradingResearch.Worker/Program.cs`

Existing ownership remains WP03/WP05. WP05 may modify only composition/wiring:

- bind the two authorized `Visualization__...` environment/configuration keys;
- register the authorized WP05 options, publisher, and publishing-store
  decorator;
- preserve the existing Historical/Replay mode selection and all other
  execution branches.

WP05 may not place serializer, file-I/O, retry, rendering, or lifecycle
algorithm code in `Program.cs`.

### `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`

Existing WP02 registration ownership remains intact. WP05 may add only the
registrations needed to construct the three authorized WP05 production types
and to decorate the existing WP04 `IVisualizationReadModelStore`. No existing
replay/provider/pipeline/persistence registration may be changed.

No changes are authorized to
`AtomicVisualizationReadModelStore.cs`; WP04 retains exclusive ownership of
its in-memory semantics.

## Explicitly forbidden paths and categories

WP05 may not modify or create:

- any schema, migration, SQLite repository, persistence, or database-runtime
  path;
- any provider, market-data, observation-source, replay, dataset, or pipeline
  algorithm path;
- `src/AIQuantTradingResearch.Application/Visualization/*` or existing WP04
  contract/use-case paths, except that the existing contracts are consumed
  unchanged;
- `src/AIQuantTradingResearch.Infrastructure/Research/*`;
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`
  or `SimulatedLiveVisualizationConfiguration.cs` (WP03/WP08 ownership);
- `tests/AIQuantTradingResearch.Application.Tests/*`, existing WP04 store
  tests, or WP02/WP03/WP04/WP09 architecture/integration test files;
- `python/validation/*` and any generated runtime/handoff/database file;
- `requirements.txt`, `.csproj` files, `.venv`, Python/Streamlit pins, or
  machine/global environment configuration;
- JSON-over-stdio/Python capability transport files;
- WP06–WP12 production, test, documentation, or planning paths;
- release planning/authority files other than this amendment artifact;
- HTTP, sockets, queues, shared memory, watchers, background retry threads,
  supervisors, Worker/Streamlit process control, or a second pipeline.

## Later implementation rule

The later WP05 implementation authority may change only the paths and symbols
listed above. Every changed path must be classified against this amendment
before closure. If implementation discovers any required file, symbol, package,
or ownership surface outside this allowlist, it must stop immediately and
request a fresh path-authority amendment. It must not improvise by editing the
closest existing file.

The final WP05 scope audit must prove that WP03/WP04/WP06+ ownership, schema,
persistence, provider, pipeline, package, Python foundation, and JSON-over-
stdio boundaries remain unchanged.

`WP05 MANIFEST/PATH-AUTHORITY AMENDMENT MUTATIONS: ONE DOCUMENTATION ARTIFACT`

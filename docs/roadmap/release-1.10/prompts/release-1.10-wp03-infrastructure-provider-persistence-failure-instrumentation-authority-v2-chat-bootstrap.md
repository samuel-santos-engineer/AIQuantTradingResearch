Execute `release-1.10-wp03-infrastructure-provider-persistence-failure-instrumentation-authority-v2-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — contract/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — execute now; WP03 implementation, validation, mutation accounting, and post-acceptance GitHub completion.
- **GPT-5.6 Sol** — supporting analysis/synthesis/non-authoritative review.

Use **GPT-5.6 Terra**.

The Luna reconciliation passed and is authoritative.

Frozen production targets only:
- `SqliteHistoricalObservationStore.Retrieve(string target)`
- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Explicitly forbidden:
- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Focused tests only:
- existing `SqlitePersistenceTests.cs`
- existing `SqliteDatasetTests.cs`
using exact paths from the reconciled manifest.

BCL-only:
- `System.Diagnostics`
- `System.Diagnostics.Metrics`
- no helper file
- no project/package/schema/migration mutation.

Infrastructure source and meter:
`AIQuantTradingResearch.Infrastructure`

Activities:
- `provider.operation`
- `persistence.operation`

Use exact metric names/types/units, attributes, and failure categories from the reconciled execution plan/manifest. Do not invent replacements.

Preserve WP02. Real Infrastructure activities inherit ambient `Activity.Current`; canonical historical retrieval must prove:
WP02 `HistoricalObservationRetrieval` → WP03 `provider.operation`.

Run attributable baseline, focused tests, affected Infrastructure/Application suites, topology, Meter/Activity listener validation, architecture/no-bypass, Gitleaks/security, functional preservation, forbidden-target audit, residue, and exact path/hunk audit.

Git mutations: ZERO.

Only after:
`RELEASE 1.10 WP03 ACCEPTANCE: PASS`

close #244 and set its unique Project #2 item to Done. Preserve Release=1.10, milestone #59 Open, and #245–#249 unchanged. Maximum GitHub mutations: 2.

On PASS, next:
**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority — GPT-5.6 Terra**

End only with the exact V2 COMPLETE or V2 BLOCKED terminal marker.

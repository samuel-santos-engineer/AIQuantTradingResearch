Execute `release-1.10-wp02-application-pipeline-observability-contract-authority-codex-prompt.md` as the sole WP02 authority.

## Model assignment
- **GPT-5.6 Luna** — contract/architecture/scope/reconciliation.
- **GPT-5.6 Terra** — execute now; WP02 implementation and validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis/non-authoritative review.

Use **GPT-5.6 Terra**.

Work package: **WP02 / #243 — Application Pipeline Observability Contract**.

Consume as immutable inputs:
- the three canonical Release 1.10 planning artifacts;
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`;
- issue #243.

Preserve the untracked WP01 selection artifact.

Implement only WP02-owned application-layer pipeline observability: minimal dependency/API surface, application observability abstraction, canonical ActivitySource/stage boundaries, WP02-owned metrics, accepted trace/status/error behavior, and focused tests.

Do not absorb WP03 provider/persistence instrumentation, WP04 lifecycle/exporter composition, WP05 System Health UI/read model, WP06 permanent cross-cutting suite, WP07 docs, or WP08 full validation.

Use the accepted file manifest as a hard path boundary. If a needed path is outside it, BLOCK.

No exporter deployment, live provider/trading behavior, schema migration, direct UI/SQLite access, or high-cardinality/sensitive telemetry.

Run focused build/tests/architecture/security/residue validation and prove WP03 can consume WP02 without rewriting its contract.

Repository mutations: WP02-authorized paths only.
Git mutations: ZERO.
GitHub mutations: ZERO.
Do not stage/commit/push or close #243.

On PASS, next:
**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority — GPT-5.6 Terra**

End only with the exact COMPLETE or BLOCKED terminal marker.

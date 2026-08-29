Execute `release-1.10-wp02-path-contract-reconciliation-authority-codex-prompt.md` as the narrow Luna reconciliation authority for blocked WP02.

## Model assignment
- **GPT-5.6 Luna** — execute now; path ownership, BCL/package decision, additive application observability contract, WP03 handoff.
- **GPT-5.6 Terra** — reserved for resumed WP02 implementation/validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis/non-authoritative review.

Use **GPT-5.6 Luna**.

Target: WP02 / issue #243.

Resolve only the four blocking gaps:
1. exact writable Application files/symbols;
2. exact dedicated WP02 test paths;
3. whether the Application layer uses BCL `System.Diagnostics` only or requires an OpenTelemetry package;
4. exact additive observability contract shape exposed to WP03.

Read the canonical Release 1.10 planning docs, `OPEN_TELEMETRY_SELECTION.md`, issue #243, current Application source/tests, and dependency files.

Persist only planning/contract changes needed to make the existing Terra WP02 authority deterministic. At minimum update the file manifest with exact writable paths/symbols and the execution plan with the BCL/package decision and WP03 handoff.

No production source changes.
No test changes.
No package changes.
No runtime/config/schema changes.
Git mutations: ZERO.
GitHub mutations: ZERO.

Require a materialization simulation proving Terra can resume without inventing any path, API, symbol, or downstream contract.

On PASS, resume:
**Release 1.10 WP02 — Application Pipeline Observability Contract Authority — GPT-5.6 Terra**

End only with the exact COMPLETE or BLOCKED terminal marker.

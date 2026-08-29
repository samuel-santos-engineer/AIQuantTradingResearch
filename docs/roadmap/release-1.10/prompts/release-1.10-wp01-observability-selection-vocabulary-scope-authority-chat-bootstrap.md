Execute `release-1.10-wp01-observability-selection-vocabulary-scope-authority-codex-prompt.md` as the sole WP01 authority.

## Model assignment
- **GPT-5.6 Luna** — execute now; dependency-selection contract, observability vocabulary, architecture/scope boundaries, cardinality/security policy, reconciliation.
- **GPT-5.6 Terra** — reserve for downstream implementation, tests, package changes, validation execution, Git/GitHub mutations.
- **GPT-5.6 Sol** — supporting technical analysis, alternatives, synthesis, non-authoritative review.

Use **GPT-5.6 Luna**.

Work package: **WP01 / issue #242 — Observability Selection, Vocabulary & Scope**.

Read the three canonical Release 1.10 planning artifacts and issue #242. Reconcile the exact WP01 contract before decisions.

Establish deterministic downstream contracts for:
- minimal OpenTelemetry dependency selection/policy;
- canonical observability vocabulary;
- pipeline/boundary instrumentation scope;
- source/span/meter/metric/attribute naming;
- attribute allowlist and cardinality;
- metrics;
- trace/span relationships;
- existing logging relationship;
- truthful System Health semantics;
- exporter isolation;
- telemetry security/privacy;
- performance/failure constraints;
- exact handoffs to WP02–WP08.

Preserve .NET pipeline ownership, schema v4, governed JSON handoff, deterministic/replay/simulated provenance, Worker/Streamlit independence, and no-bypass architecture. Observability must not imply live providers or trading.

Persist decisions only in WP01-authorized Release 1.10 planning/contract paths from the accepted file manifest. No production source, tests, package installation, schema/runtime/config, or downstream implementation.

Git mutations: ZERO.
GitHub mutations: ZERO.

Require WP01 acceptance to prove WP02 can proceed without inventing WP01-owned semantics.

On PASS, next authority:
**Release 1.10 WP02 — Application Pipeline Observability Contract Authority — GPT-5.6 Terra**

Do not execute WP02 or close #242.

End only with the exact COMPLETE or BLOCKED terminal marker.

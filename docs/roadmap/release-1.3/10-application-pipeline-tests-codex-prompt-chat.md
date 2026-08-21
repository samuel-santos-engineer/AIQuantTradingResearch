Execute Release 1.3 WP10 — Application Pipeline Tests exactly under `10-application-pipeline-tests-codex-prompt.md`.
Read all authorities first, prove every starting-state gate, inventory existing coverage, and only then move issue #147 to In Progress.
Add only permanent, deterministic, offline Application tests for WP03–WP07 pipeline identity/contracts/orchestration/validation/evidence semantics using hand-written test doubles; production code must remain unchanged.
Do not test real Worker/DI/SQLite/provider behavior, add packages/references/schema changes, start WP11+, or introduce Release 1.4 behavior; run all targeted/full/canonical/security/architecture gates and report exact test-count deltas.
Close #147 only after full acceptance, keep #148 Open/Backlog, and end with `RELEASE 1.3 WP10 COMPLETE` or `RELEASE 1.3 WP10 BLOCKED`.

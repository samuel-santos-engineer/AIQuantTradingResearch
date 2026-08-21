Execute Release 1.3 WP12 — Architecture Evolution exactly under `12-architecture-evolution-codex-prompt.md`.
Read all authorities first, prove every starting-state gate against the 197-test baseline, inventory all 13 existing architecture tests, and only then move issue #149 to In Progress.
Use a zero-delta-first approach: add or modify architecture tests only for a genuinely new, stable, repository-wide, non-redundant rule; production code, functional tests, documentation, schema, packages, and project references must remain unchanged.
Prove the production graph remains Domain → none, Application → Domain, Infrastructure → Application, Worker → Application/Infrastructure with zero cycles; do not start WP13+, and keep #150/#151 Open/Backlog.
Close #149 only after full acceptance and end with `RELEASE 1.3 WP12 COMPLETE` or `RELEASE 1.3 WP12 BLOCKED`.

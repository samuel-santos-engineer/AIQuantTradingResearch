Read `09-dataset-catalog-persistence-lookup-codex-prompt.md` completely as the sole WP09 authority and reconcile accepted WP01-WP08 state before mutation.
Implement only the existing Application IDatasetCatalog over SQLite schema v2 with immutable registration, exact Snapshot Identity lookup, equivalence/conflict, and metadata fidelity.
Do not begin WP10+, redesign snapshot persistence, change schema/packages/references, add tests, or perform Git transport; run all required lifecycle, lookup, fidelity, regression, architecture, security, whitespace, build, and canonical gates.
Do not stage, commit, push, branch, or open a PR; leave #130 Open/Backlog and preserve Release 1.1 behavior.
Close #129/mark Done only after every gate passes; otherwise stop on the precise authority or implementation blocker.

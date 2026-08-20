Read `07-dataset-physical-storage-model-codex-prompt.md` completely as the sole WP07 authority and reconcile accepted WP01-WP06 state before mutation.
Implement only the Infrastructure-owned SQLite schema-v2 physical dataset/catalog model and deterministic v1-to-v2 evolution while preserving Release 1.1 history and semantics.
Do not implement WP08+ persistence/retrieval, add packages, tests, DI, Worker behavior, Release 1.3, or alter contracts; run all required schema, regression, architecture, security, whitespace, build, and lifecycle gates.
Do not stage, commit, push, branch, or open a PR; leave WP08/#128 Open/Backlog and finish only with the full WP07 report and its prescribed terminal.
If every gate passes close #127/mark Done; otherwise stop with the precise blocker and smallest corrective authority required.

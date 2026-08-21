Execute Release 1.3 WP09 — One-Shot Worker Pipeline Execution exactly under `09-one-shot-worker-pipeline-execution-codex-prompt.md`.
Read all authorities first, prove every starting-state gate, and only then move issue #146 to In Progress.
Implement only the minimum Worker path that uses WP08 configuration, resolves the accepted pipeline use case, invokes it exactly once, projects WP07 structured evidence, returns deterministic exit behavior, and terminates.
Prove two separate offline Worker invocations (`NewlyAccepted` then `EquivalentExisting`) without provider calls; do not add loops, retries, scheduling, schema changes, durable run history, permanent tests, WP10+ work, or Release 1.4 behavior.
Return the required report and stop at `RELEASE 1.3 WP09 COMPLETE` or `RELEASE 1.3 WP09 BLOCKED`; do not start WP10.

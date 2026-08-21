Execute Release 1.4 WP10/#162 — One-Shot Worker Feature Execution — under the full WP10 authority and accepted WP04–WP09 boundaries.
Implement only the Worker path that constructs the WP09 request, resolves `IFeatureGenerationUseCase`, executes once, presents bounded deterministic evidence, returns deterministic status, and terminates.
Preserve empty/single success, unknown-defect propagation, schema v2, provider/network isolation, and the Release 1.3 five-stage Worker/pipeline behavior.
Do not add loops, retries, scheduling, persistence, packages/references, permanent tests, or WP11; clean disposable residue and run the complete offline matrix and canonical verification.
Close #162 only after success, leave #163 Open/Backlog, and end with `RELEASE 1.4 WP10 COMPLETE`.

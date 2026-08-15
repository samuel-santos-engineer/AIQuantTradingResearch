Read and execute `docs/roadmap/release-1.0/prompts/08-market-data-normalization-semantic-unblock-codex-prompt.md` completely.
Treat it as explicit human authority resolving B08-01 with `close`, B08-03 with explicit `adjust=splits`, and B08-02 with an exchange-local daily-date anchor at 00:00 carrying the resolved exchange offset.
Verify current official Twelve Data documentation still supports `adjust=splits`, then make only the minimum authorized WP07 correction that adds `adjust=splits` to the existing `/time_series` request.
Do not implement the WP08 normalizer, change Domain/Application/Worker/DI semantics, map failures, use live provider access, mutate GitHub, stage, commit, push, or begin WP09.
Run all required validation and scope/security checks, return the complete semantic-unblock report, then stop so the existing authoritative WP08 prompt can be resumed separately.

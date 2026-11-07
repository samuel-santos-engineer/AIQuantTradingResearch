# Codex execution prompt — Release 1.15 WP07 SQLite durability and writer exclusion

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Baseline: merged `main` including PR #397, squash `fa0b17e5608fdabc44b8cb88d9a68340d70733b9`.

## Owner decision and goal

**SQLite is the authoritative durable research datastore.** Preserve SQLite and its existing generation/version, D5 transform configuration, outside-DB watermark/witness, ingestion/calculation, and fail-closed integrity semantics. Do not replace SQLite with Azure SQL, PostgreSQL, blob-backed pseudo-SQLite, or a different authoritative database. Deliver the smallest defensible implementation that establishes durable SQLite persistence and exclusive writer ownership across restarts and potentially overlapping Azure App Service container replacements. Open a PR for owner review; do not merge.

## First inspect reality

Read merged Release 1.15 governance, WP07 staging runbook and evidence, SQLite store/recovery/writer ownership code, research endpoint and initializer, Dockerfiles, local qualification runner, and Azure F1 constraints. Check the existing named staging research app and plan read-only if existing credentials allow; do not request credentials or expose settings. Distinguish platform-documented facts from tested facts and assumptions. In particular, investigate whether existing F1 Linux App Service storage offers a *genuinely durable local filesystem with SQLite WAL-compatible locking/shared memory and safe cross-replacement exclusivity*. Persistent `/home` or Azure Files alone does **not** establish this. Never claim a network SMB/NFS share safely supports SQLite WAL without concrete platform and end-to-end evidence.

## Implement, where technically supportable

1. Keep SQLite as the sole authoritative store and preserve the DB + WAL/SHM + external witness as one consistent recoverable lineage. Specify and implement an explicit storage layout and startup validation: ownership UID 1654, private permissions, filesystem/mount identity, durable write/sync/reopen semantics, witness consistency, and fail-closed behavior on missing, corrupt, stale, mismatched or nonpersistent state. Never silently regenerate D5, reset generations, overwrite witness, import fake data, or downgrade durability.
2. Establish a real exclusive-writer protocol that handles **overlapping replacements across distinct App Service instances**, not merely `FileShare.None` or a local lockfile. If cross-instance exclusion cannot be guaranteed on the available storage/platform, fail closed before opening the writable store. Research API read-only behavior may be considered only if compatible with existing state guarantees and without relaxing integrity checks. Prove takeover/fencing semantics rather than relying on an assumed single F1 instance.
3. Design restart and recovery behavior for graceful stop, abrupt termination, interrupted writes, WAL replay/checkpoint, partial state activation, and replacement overlap. Test durability at the strongest reproducible level; distinguish quiescent SIGKILL from an interrupted write and a genuine host power loss. Never mark unexecuted cases PASS.
4. Retain PR #397's staging HTTP backend and local HTTPS default, server-side API-key enforcement, strict UI HTTPS verification, and operator-only D5 initializer/import. Avoid unrelated changes to chart, schema, dependencies, governance, provider activation, and credentials.
5. Extend focused automated tests and a bounded reproducible Linux/amd64 integration harness to exercise same-volume and **distinct-container/instance** contention, restart, witness tampering, storage-mount mismatch, partial commit/recovery and secret isolation. Report exact commands, exit codes, image IDs (not registry digests), limitations and relevant resource observations.

## Decision if F1 cannot satisfy the requirement

Do **not** manufacture a workaround or silently choose a paid service, new storage account, or different database. Provide a concrete, evidence-backed compatibility matrix of options **that retain SQLite**, including why local ephemeral storage, Azure Files/SMB with WAL, and any feasible persistent local/block-backed volume do or do not satisfy the required durability and cross-instance exclusion. Recommend the smallest viable SQLite-preserving hosting/storage adjustment, with explicit resource/cost implications, and state which owner decision would be needed. Implement only the safe, locally verifiable components and leave Azure durability BLOCKED if no supported solution exists on existing F1 resources. Do not add licensing or policy approval gates.

## Execution and repository boundaries

- Work from fresh merged main in an isolated worktree. Inventory and preserve the original owner's dirty/untracked files, HEAD and status byte-for-byte. Do not reset, stash, clean or mutate the owner checkout.
- Reuse existing pinned .NET 10, Python, Docker bases and dependencies. No silent restores/upgrades, new paid infrastructure, registry push, Azure configuration mutations, deployment, live provider calls, or production operations.
- Keep changes narrow and reviewable; update the existing WP07 staging runbook and append to the Release 1.15 foundation preflight evidence without rewriting historical records.
- Run focused .NET/Python tests and the actual Linux dual-container/browser suite where prerequisites are available. If a test cannot run, identify the exact missing prerequisite and leave it NOT RUN.
- Validate UTF-8 without BOM, LF/final newline, `git diff --check`, exact file inventory and preservation of owner files. Document any known unrelated legacy failure separately.
- Create **one open, unmerged PR** against main. Include implementation scope, exact changed files, tests/evidence, risk/rollback, what is and is not proven, and remaining staging actions. Do not close WP02/WP03/WP06/WP07/WP08 automatically.

## Acceptance and reporting

Success means either (A) SQLite durability and cross-instance writer fencing have been **implemented and demonstrated** against a supported storage mechanism, or (B) a narrowly scoped fail-closed implementation and a rigorous, reproducible demonstration that existing F1 storage cannot satisfy the requirement, with a precise SQLite-preserving next option. Do not label (B) as Azure-ready. Keep `PUBLIC_RUNTIME_VERIFIED=False` until actual Azure staging acceptance. Do not deploy or merge the PR; return its URL, head SHA, tests, and unresolved blockers.

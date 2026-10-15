# WP07 — Repository-Only SQLite and Container Identity Evidence Milestone

**Date:** 2026-10-10  
**Status:** Proposed Codex execution prompt — read-only inspection only.  
**Target:** The owner's **current local working tree** at `C:\projects\github\AIQuantTradingResearch`, including uncommitted and untracked material. Do not substitute `origin/main`, a GitHub snapshot, or an earlier frozen SHA for local evidence.

## Authority and hard boundaries

The owner authorizes **read-only repository inspection and a response-only evidence report** for this milestone. No file creation in the repository is authorized by this prompt. Do not edit, format, stage, commit, branch, stash, reset, clean, delete, or overwrite any tracked/untracked file. Do not execute the application, Worker, broker, supervisor, SQLite CLI, database connections, builds, tests, harnesses, fault injection, containers or deployment scripts. Do not call Azure, query live resources, alter RBAC/identities, retrieve credentials, or access real SQLite database contents. Do not run `PRAGMA` against the deployed or local database. No new database, auxiliary journal, Blob resource, storage account, package or service. Do not publish PRs or activate public generation.

Use commands that only read repository files and Git metadata. Do not print secret values or copy environment files. If a command might initialize files, create caches, access a database or run startup code, **do not run it**. Preserve the frozen offline implementation and all unrelated worktree changes. `PUBLIC_RUNTIME_VERIFIED=False` remains mandatory. WP07 #322 remains blocked and PR #330 must not be changed.

## Owner-established architecture constraints

- The **existing approved SQLite database under Azure `/home`** is the **sole database/storage candidate** for this phase. No auxiliary database or transactional storage resource.
- C2 residual Azure F1 quota exhaustion/unavailability risk has been accepted; local-budget admission still requires qualification.
- Public synthetic Worker and descendants must never access private historical observations, provider credentials, SQLite file/sidecars, broker authority, or private research paths.
- Admission must be durably occupied and budget-charged before any Worker spawn; one authoritative global domain; uncertain failure denies; complete Worker-tree termination independently verified before settlement.
- SQLite transactions are a possible admission primitive, **not proof** of Azure `/home` mount locking, fsync, crash durability, global topology or process-tree cleanup.
- A2 distinct broker/Worker UID remains a **candidate**, not an approved or qualified implementation.

## 1. Establish the exact working-tree baseline (read-only)

Report: current branch, `HEAD` SHA, relationship to `origin/main` only if local refs already exist (no fetch), `git status --short`, and the exact tracked/untracked state of files inspected. Do not assume documentation edits are committed. Capture whether WP07 offline source/test paths and governance documents are locally modified without changing them. If the working tree or a referenced path is unavailable, mark it **not inspected**, not passed.

Use `git ls-files`, `git status --short`, `git grep -n` (tracked files), and safe read-only file listing/reading. Include untracked documents by direct filesystem reads when relevant; `git grep` alone will miss them. Do not use `git diff` options that write files or trigger external tooling.

## 2. Actual SQLite adapter and schema initialization

Locate and cite **exact repository-relative paths and line ranges** for:

- SQLite persistence adapter(s), connection factory, connection string/path construction, schema initialization/migration/DDL, and transaction management.
- Existing table definitions, constraints, unique keys and any database version/migration tracking. Identify whether `historical_observations` and any admission-related table actually exist in source, without assuming a new table exists.
- SQLite pragmas (`journal_mode`, `synchronous`, `busy_timeout`, `locking_mode`, `foreign_keys`), open flags, pooling, WAL/rollback behavior and checkpoint/sidecar handling, **only where explicit in code/config**. If absent, say **not explicitly configured** rather than guessing runtime defaults.
- Which roles currently open SQLite, and whether database initialization can run during startup or Worker execution.
- Whether the current schema could safely support an additional admission record **conceptually**, without proposing or performing a schema migration. Distinguish application-level semantics from SQLite engine guarantees and filesystem-dependent properties.

Do **not** infer transaction durability from historical-observation idempotency tests or a persistence guide alone. Cite tests separately from production implementation.

## 3. Database path resolution and Azure `/home`

Trace the path from configuration input (e.g., `Persistence:DatabasePath` / `Persistence__DatabasePath`) through application wiring to the exact SQLite open call. Inspect checked-in Dockerfile, entrypoint, deployment templates, workflows and safe configuration examples **without opening secrets**. Record:

- The documented or configured Azure path under `/home`, its derivation and fallback/error behavior.
- Whether `/home` persistence depends on an App Service setting and whether that setting is **only documented** or **verified in the local repository**. Repository configuration cannot establish live Azure settings or mount semantics.
- Directory creation, ownership, permissions, symlink/path traversal protections and sidecar (`-wal`, `-shm`, `-journal`) access implications.
- Any other SQLite database paths and whether the approved DB is demonstrably the same file used by production private research.

Do not inspect actual database rows or run SQLite. Distinguish source/config evidence, owner-stated deployment facts and unverified live-runtime assumptions.

## 4. Container identity and broker/Worker authority

Inspect Dockerfile(s), entrypoint/start scripts, user/group setup, `USER` directives, `su`/`setpriv`/`runuser`/`gosu`/`chroot`/namespace/capability/`seccomp` usage, Python/C# process spawning, IPC and environment construction. For each role (web/UI, broker, supervisor/guardian, Worker, descendants), document:

- Intended UID/GID versus actual **statically provable** startup UID; distinguish missing explicit `USER` from proven Azure effective identity.
- Who owns/opens SQLite and whether a Worker could read DB files or journal sidecars under currently visible permissions.
- Who can spawn, signal, reap and verify Worker descendants; whether A2's cross-UID launch/cancellation mechanism is actually implemented or merely proposed.
- Exposed file descriptors, inherited environment, control sockets and filesystem access paths that could undermine confidentiality.
- Which role has authority to settle occupied state, and what happens if broker and supervisor die together.

Treat kernel capabilities, namespace availability and Azure F1 runtime behavior as **unverified** absent separately authorized target evidence. Never equate different UIDs alone with complete isolation.

## 5. Admission journal fit and failure-state analysis (design only)

Produce a state-transition table for **proposed** `FREE -> OCCUPIED -> SETTLEMENT_PENDING -> FREE` behavior, including crash windows before/after commit, before/after spawn, uncertain spawn, timeout, stale PID, broker/supervisor death, SQLite busy/corruption, `/home` interruption, clock rollback and restart. Show which cases must deny generation and require independent cleanup proof. No executable SQL, schema migration or code patch is authorized.

Assess whether one **existing database file** could hold trusted admission state while the public Worker has **zero file-level access** to that same file and all SQLite sidecars. Explicitly flag any conflict with the present private research process identity, current ownership, or worker startup path. SQLite has no built-in per-table OS-file access control; trusted broker-only DB access is the intended candidate boundary, not a proven property.

Evaluate deployment topology: one qualified admission domain across every enabled entry point; an instance-local lock is insufficient if another instance can admit. Mark unverified Azure scale/recycle overlap assumptions.

## 6. Evidence classification and verdict

For each finding, label **PROVEN BY REPOSITORY**, **DOCUMENTED ONLY**, **OWNER-STATED**, **DESIGN INFERENCE**, **REQUIRES AZURE QUALIFICATION**, or **BLOCKED/UNKNOWN**. Provide a concise matrix:

1. SQLite adapter and exact schema identified
2. Path resolves to the approved `/home` database
3. Private data isolation from public Worker
4. Atomic pre-spawn durable admission design
5. SQLite filesystem locking and crash durability on actual Azure `/home`
6. Cross-instance/global admission topology
7. A2 identity transition and cleanup authority
8. Independent process-tree termination proof
9. Zero additional recurring resources
10. C2 budgets/health-breaker and accounting integration

For each: repository evidence with path:line, confidence, gap and **minimum next evidence**. Do not mark Azure/runtime criteria passed based on repository inspection. Explicitly separate “SQLite is suitable in principle” from “this deployed SQLite file is qualified.”

## 7. Response format (no repository output file)

Return the following sections directly in the Codex response:

A. Exact baseline and inspection coverage  
B. SQLite adapter/schema/transaction evidence (path:line)  
C. Database path and `/home` deployment evidence (path:line)  
D. Container identity, DB access and A2 authority map (path:line)  
E. Admission crash-window/fail-closed table  
F. Ten-item evidence/qualification matrix  
G. Security blockers and minimum separately authorized qualification work  
H. Explicit no-mutation/no-execution attestation and unchanged `git status --short`

Avoid speculation presented as fact. Quote only the minimal necessary source fragments. Do not expose secrets, database content or tokens. End with a decision: **repository evidence sufficient for design planning / insufficient for runtime qualification**, or **blocked by a specific source-level contradiction**. Propose exact subsequent documentary tasks and, separately, any eventual Azure probes that would require fresh owner authority. Do not perform them.

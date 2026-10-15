# Codex Prompt — WP07 A2 + SQLite Mechanism Closure Specification

## Authority and purpose

**READ-ONLY ANALYSIS AND CHAT-ONLY DOCUMENTARY OUTPUT.** Inspect the current local working tree, including tracked modifications and untracked WP07 files, to prepare a source-referenced **WP07 A2 + SQLite Mechanism Closure Specification**. Do not edit or create repository files, including the proposed ADR; do not commit, stage, push, open or modify PRs/issues, or modify any local file. Do not run builds, tests, SQLite commands/PRAGMAs, containers, application processes, deployment scripts, or Azure diagnostics. Do not access live database contents, secrets, or production configuration. Read-only source inspection (`git status`, `git diff`, `git show`, file reads, searches) is permitted. Report findings in the Codex response only. If the requested evidence cannot be obtained under this boundary, classify it as unresolved; do not expand authority implicitly.

This is **not** authorization to adopt A2, implement admission tables, modify identities, change the schema, qualify Azure F1, or activate public runtime. Preserve `PUBLIC_RUNTIME_VERIFIED=False`.

## Controlling context and constraints

- Repository: `AIQuantTradingResearch`; WP07 of Release 1.14; draft/unmerged public runtime remains disabled.
- Existing approved SQLite database under Azure App Service `/home` is the **sole permitted storage candidate**. No auxiliary SQLite file, Blob, Azure SQL, other transactional resource, paid tier, or privileged fallback.
- Public preview is **synthetic-only**. Preserve the frozen **BTC-like simulation identity** and **twelve-selection contract**; do not label synthetic candles as observed BTC/USD or ETH/USD spot-market data. Do not silently revise instrument terminology.
- Private research tables and data must remain confidential. Public UI, public Worker, descendants and untrusted processes must have **zero filesystem access** to the SQLite file, parent access paths and all relevant sidecars; synthetic-only dependency injection and environment sanitization are insufficient as isolation proof.
- One globally authoritative admission domain; occupation and conservative budget charge must be durably committed **before** spawn. No overlapping Worker trees, including during recycle, restart, supervisor/broker combined failure, or uncertain acknowledgments. No automatic release on timeout, PID absence, lease expiry or lock release. Independent full-tree death proof is required before durable settlement can reopen admission.
- A2 distinct-UID isolation is a **preferred qualification candidate only**, not adopted or production-proven. Current startup uses named `aiq`; offline metadata names broker and Worker UID 1003. Do not claim credential separation exists.
- C2's **exact wording has been approved** in four governance documents, but its implementation, qualification and activation have not. Preserve the accepted residual F1 quota-exhaustion risk while maintaining mandatory local limits and health breakers. Do not conflate policy approval with runtime readiness.
- The existing A2/SQLite ADR is a **proposal**, not an adopted exclusive architecture. Treat its original “off-repository draft” provenance statement as historical; verify its current working-tree location.
- Existing qualification-plan sampling conflict: once/second versus controlling candidate observer cap once/ten seconds. Resolve at the documentary level by identifying authoritative sources and a proposed correction; do not edit files.

## Starting evidence to verify, not assume

Use the local `docs/roadmap/release-1.14/prompters/WP07_SQLITE_CONTAINER_IDENTITY_EVIDENCE_REPORT.md` and the proposed A2/SQLite ADR, if present, as baselines. Recheck relevant local sources and record exact paths and line ranges. Prior repository-only evidence reported:

- `SqliteConnectionFactory.cs` uses `ReadWriteCreate`, enforces DELETE journal mode and invokes schema bootstrap; `SqliteSchemaBootstrapper.cs` targets `user_version=4`. No admission/occupation table or production admission adapter was identified.
- Dockerfile default database path `/runtime/aiq.sqlite`; deployment script intent `/home/data/aiquant.db`; actual Azure settings and mount behavior unverified.
- `Dockerfile`, `container/entrypoint.sh`, public bridge and Worker startup currently share the `aiq` identity for relevant roles; numeric UID assumptions and sidecar permissions are unqualified.
- Offline broker/supervisor/identity modules do not implement a live independent termination verifier, distinct-UID launch, or SQLite-backed C2 admission.
- Private research processes legitimately write existing SQLite tables; “sole admission writer” must **not** be mistaken for sole database writer.

If current sources differ, report the delta; do not overwrite earlier evidence silently. Begin with branch, HEAD, `git status --short`, relationship to local `origin/main` without fetching, and inspection inventory. Finish by checking status again and attesting no mutation.

## Required analysis and deliverables

### 1. Role and access matrix

Build a matrix for container startup/root bootstrap, public UI, trusted supervisor, trusted SQLite admission authority, broker, public Worker and descendants, private research Worker, and independent termination verifier. For each, specify candidate UID/GID and supplementary groups (or TBD), launch authority, database directory/file/sidecar access, inherited descriptors, code/config trust boundary, and ability to spawn, signal, inspect and terminate cross-UID processes. Distinguish **required isolation** from **available implementation** and **Azure F1 feasibility evidence**. Analyze whether a broker can both access private SQLite and run under the same UID as the public Worker; identify viable separation alternatives without adopting one. Include symlink/path traversal, parent-directory permissions, sidecar creation, startup chown, and private-research coexistence.

### 2. Trusted admission authority and SQLite transaction contract

Propose, without SQL or migration, the **single authoritative admission-state writer** and relationships to supervisor, broker, verifier and legitimate private database writers. Define conceptual durable states (for example UNINITIALIZED, FREE, OCCUPIED, SETTLEMENT_PENDING, DENIED/QUARANTINED), transitions, ownership, state lineage/generation and proof binding. Distinguish initial provisioning from ordinary recovery: missing/corrupt database, missing admission state, unexpected schema version or failed integrity verification must **deny**, never initialize a clean budget automatically. Specify pre-spawn atomic reservation plus C2 charge, commit-before-spawn, uncertain commit/spawn acknowledgment, cancellation, verified-death settlement, rolling history preservation, bounded SQLite contention and restart behavior. Consider compatibility with existing `ReadWriteCreate`, schema v4, DELETE journal mode, private transactions and migration discipline. Do not infer Azure durability from source-level transactions.

### 3. Independent complete Worker-tree termination proof

Describe a concrete candidate mechanism for process-tree identity, descendant discovery and binding to a reservation generation, cross-UID cleanup, PID reuse defense, orphan/adopted descendants, combined broker/supervisor death, restart/recycle overlap and loss of verifier. Distinguish what can be proved in Linux containers from what must be verified on **actual Azure F1**. No PID-only, signal-only, timeout-only, lease-only, flock-only or process-group-only shortcut may clear occupancy. If independent proof cannot be constructed under F1 privileges, explicitly classify the design **BLOCKED** rather than assuming a working guardian.

### 4. Deployment-wide topology and SQLite authority

Define the global admission scope for all entry points, workers, instances, restarts, deployment slots and recycle overlaps. Identify whether a single shared SQLite `/home` path is sufficient only **conditional on** filesystem lock, atomic commit, crash durability, path identity and cross-instance consistency evidence. Address stale mounts, read/write errors, process concurrency and fallback paths. Propose a minimal separately authorized **sanitized read-only Azure inventory** and later bounded qualification plan, clearly separated from current work. Do not propose an auxiliary database or external lock service.

### 5. C2 health, accounting and abuse contract

Reconcile the approved C2 wording with frozen offline `QuotaGate` usage. Specify the relationship among **240 seconds reservation window**, **300 seconds potential measured CPU** (15 × 20 seconds), 16 reserved CPU seconds/spawn, daily/rolling admissions, global expensive-attempt limits, cached-read limits and attributable CPU outside the Worker tree. Identify mathematical contradictions without silently changing values. Define candidate global health breaker and observable signals without assuming Azure quota telemetry; distinguish reservations from hard CPU ceilings. Address once/second versus once/ten-seconds sampling conflict and source precedence. Provide proposed documentary correction text only, not file edits.

### 6. Reconciliation of four ADR/document conflicts

For each, provide existing claim, controlling evidence, proposed precise correction, and authority needed to apply it:

1. Synthetic BTC-like identity and twelve-selection contract versus spot-market wording.
2. A2 preferred qualification candidate versus exclusive/adopted architecture wording.
3. Approved C2 policy wording versus unqualified C2 runtime implementation.
4. Original off-repository draft provenance versus current untracked repository location.

Also report any other material conflict discovered, with source citations.

### 7. Decision and qualification matrix

For each mechanism, assign exactly one classification: **FEASIBLE BY REPOSITORY EVIDENCE**, **CONDITIONALLY FEASIBLE**, **BLOCKED**, or **REQUIRES AZURE F1 EVIDENCE**. Explain what repository evidence supports, what is only a design inference, the minimum decisive evidence, and whether implementation selection is currently permissible. A safety requirement alone is never proof of a feasible enforcement mechanism. Include explicit confidentiality, no-overlap, and zero-additional-recurring-cost gates. State whether any candidate is ready for implementation selection (default **NO** unless justified by concrete evidence).

## Required response structure

A. Baseline and no-mutation scope

B. Source-indexed facts and differences from earlier report

C. Role/credential/filesystem access matrix

D. Trusted admission-writer and SQLite state/transaction contract

E. Independent tree-death proof and combined-failure analysis

F. Deployment/global authority and Azure F1 evidence gaps

G. C2 quota, health, accounting and sampling reconciliation

H. Exact documentary conflict/correction matrix

I. Mechanism feasibility and go/no-go decision matrix

J. Next separately authorizable evidence milestones (documentation first; Azure qualification later)

K. Final `git status --short` comparison, no-action attestation, and confirmation that `PUBLIC_RUNTIME_VERIFIED=False` was not modified

Cite exact local file paths and line ranges for every repository-derived conclusion. Mark unproven statements **DESIGN INFERENCE**, **OWNER CONSTRAINT**, or **REQUIRES AZURE EVIDENCE** as appropriate. Do not manufacture runtime observations. If no mechanism can satisfy the mandatory constraints on F1 with the allowed resources, state that the public runtime remains blocked and identify the unsatisfied requirement rather than weakening it.

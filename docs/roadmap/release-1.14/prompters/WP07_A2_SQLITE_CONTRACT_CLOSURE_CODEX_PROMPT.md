# WP07 — A2 + SQLite Contract Closure

## Codex authority: documentation-only analysis and proposed specification

**Status:** Request for a source-referenced contract-closure deliverable, **not** adoption of A2, approval of implementation, or permission to change the repository.

Inspect the **current local working tree** of `AIQuantTradingResearch`, including tracked modifications and untracked WP07 documents/modules. Do not substitute a remote `main` snapshot for local evidence. Record branch, HEAD, local status, and whether `PUBLIC_RUNTIME_VERIFIED=False` remains intact. Do not fetch, checkout, stage, commit, or change files.

Produce the complete **WP07 A2 + SQLite Contract Closure Specification** in your response (Markdown), suitable for subsequent owner review and optional separately authorized saving. Do not write it into the repository or modify any existing ADR, plan, code, schema, tests, or configuration. If the owner later authorizes specific documentary edits, that will be a separate action.

## Controlling boundaries

- Public preview is **synthetic-only**, preserving the approved **BTC-like simulation identity and frozen twelve-selection contract**; never imply observed BTC/USD or ETH/USD spot-market data.
- `PUBLIC_RUNTIME_VERIFIED=False`; public Worker execution and activation remain disabled.
- The **existing approved SQLite database under `/home`** is the **sole permitted storage candidate**. No Blob, Azure SQL, second SQLite database, auxiliary lock/JSON journal as production authority, or new recurring-cost service.
- A2 distinct-identity separation is **preferred for qualification only**, not adopted or demonstrated on Azure F1.
- Preserve private research data and legitimate private database writers. Public UI, synthetic Worker, and all descendants must have **zero file, directory traversal, sidecar, descriptor, and alternate-path access** to the private SQLite database.
- No overlapping Worker process trees across brokers, supervisors, crashes, restarts, recycle overlap, or enabled instances. Never release occupation merely on PID absence, signal success, lock release, lease expiry, or timeout.
- Approved **C2 wording** permits unknown Azure quota telemetry subject to mandatory conservative local safeguards. It does not approve integration, numerical qualification, or activation.
- Zero additional recurring cost; no assumption of writable cgroup CPU delegation, privileged helpers, PID namespace support, cross-UID signaling, or durable `/home` filesystem guarantees without evidence.

## Source baseline to reconcile

Read and cite actual file paths and line ranges from the current tree, including at minimum:

- `docs/roadmap/release-1.14/prompters/WP07_SQLITE_CONTAINER_IDENTITY_EVIDENCE_REPORT.md`
- The existing A2/SQLite architecture decision record and the latest mechanism-closure report, if present locally; identify exact filenames and their tracked/untracked status.
- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`, `RELEASE_1.14_EXECUTION_PLAN.md`, `RELEASE_1.14_FILE_MANIFEST.md`
- `docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_QUALIFICATION_PLAN.md`
- `Dockerfile`, `container/entrypoint.sh`, `python/presentation/historical_market_bridge.py`
- `python/public_runtime/{bootstrap,broker,supervisor,journal,quota,ipc}.py` where present
- SQLite connection factory, storage configuration, schema bootstrapper, schema definitions, private stores and Worker composition/configuration sources.

Do not assume historical reports are accurate if the current tree differs; state differences explicitly. The prior mechanism report identified `ReadWriteCreate`, DELETE journal mode, schema v4, `/runtime/aiq.sqlite` image fallback versus `/home/data/aiquant.db` deployment intent, shared named `aiq` identity, broker/Worker UID 1003 metadata, a JSON/lock offline journal, an offline package not copied into the image, and an unimplemented independent verifier. Verify these rather than merely repeating them.

## Required contract sections

### 1. Explicit trust and role/access matrix

Define candidate responsibilities for finite root bootstrap, public UI, trusted supervisor, SQLite admission-transition authority, broker, public Worker and descendants, private research Worker, and independent verifier. For each, document:

- proposed privilege/identity relationship (UID/GID numeric choices **TBD** unless proved), process parentage and launch path;
- read/write/execute/traverse access to approved DB, parent directory, rollback journal and possible WAL/SHM sidecars, code/config, control sockets and `/proc`;
- allowed inherited file descriptors, environment, supplementary groups, mounts and credentials;
- who can spawn, signal, terminate, verify, reserve and settle;
- immutable trusted-code requirements, symlink/canonical-path resistance, and private-writer coexistence.

Distinguish **sole admission-state transition writer** from sole writer of the SQLite file: existing private stores remain legitimate writers, and file-level SQLite permissions cannot enforce table-level isolation. State whether a broker with raw DB access can safely share credentials or a filesystem view with the public Worker; do not claim safety without a complete mechanism.

### 2. Constrained A2 launch, cancellation and credential-drop contract

Describe at least one concrete candidate sequence from UI request through authenticated supervisor/admission/broker interactions, committed reservation, one-use launch authorization, Worker execution, cleanup and settlement. Define replay/duplicate-delivery handling and exact trust boundaries. Specify how a trusted actor can launch and **terminate the full descendant boundary across UID separation** without an assumed permanent privileged helper or unavailable Linux capability. If the mechanism cannot be supported from current evidence, label it **BLOCKED / requires Azure F1 evidence** and identify the minimal decisive test; do not invent a working mechanism.

### 3. SQLite authority, lineage and transaction contract

Specify conceptual state and invariants, **without SQL or schema changes**:

- controlled `UNINITIALIZED` provisioning versus normal recovery; no automatic creation/reset of missing admission state;
- `FREE`, `OCCUPIED`, `QUARANTINED`, `SETTLEMENT_PENDING`, and durable return to `FREE`;
- unique generation, durable state revision/lineage, canonical DB identity, launch intent/status, charge history and proof binding;
- atomic pre-spawn occupation **and full budget charge**, committed before any Worker spawn; no transaction held for the Worker lifetime;
- uncertainty windows: commit acknowledgment lost, launch acknowledgment lost, broker/supervisor/verifier crash, SQLite busy/corrupt/unavailable, private-write contention, clock rollback, uncertain settlement;
- no second launch from token replay, process restart or ambiguous delivery; SQLite commit and process spawn are not one atomic operation;
- version-4 schema/private-store compatibility, migration/initialization authority and journal-mode/durability assumptions;
- what happens if all trusted actors die and only persistent records remain.

Describe the ownership of **each** transition and who may verify it. Do not turn existing `ReadWriteCreate` into silent admission recovery. Treat the frozen `admission.json`/`admission.lock` as historical offline scaffolding, **not** an allowed production authority.

### 4. Independent whole-tree death-proof contract

Propose an evidence-binding model for all Worker descendants: generation identity, kernel execution boundary, process start/boot identity, proof provenance, observer independence and complete-boundary destruction. Compare candidate PID namespace, pidfd, cgroup, or other Linux mechanisms **only as candidates**; distinguish what the repository proves from what requires Azure F1 capability checks. Address orphan descendants, PID reuse, lost handles, escaped descendants, broker death, simultaneous broker/supervisor death, verifier death and recycle overlap. If no independent proof survives a failure, admission must remain occupied/quarantined, not reopen on time passage.

### 5. Deployment-global exclusion and existing `/home` SQLite

State the necessary single-authority topology across all enabled instances, slots, revisions, process restarts and recycle overlaps. Specify evidence needed for exact approved file identity, mount topology, DELETE-mode locking, acknowledged-commit durability, sidecar security and stale-mount/split-brain avoidance. Explicitly disallow `/runtime/aiq.sqlite` as an admission fallback. Do not claim Azure F1 supports any mechanism without target evidence.

### 6. C2 health, accounting and abuse contract

Preserve approved C2 policy language and distinguish reserved from measured CPU: `15 × 16 = 240` reserved seconds and `15 × 20 = 300` measured job seconds, **before attributable overhead**. Define unresolved total-attributable CPU and verified allowance test (including uncertainty), admission and cached-read abuse limits, global health breaker inputs, missing-evidence behavior, freshness, hysteresis and sustained-safe reopening. Reconcile **qualification observer maximum once per 10 seconds per observer** versus the outdated once/second text; distinguish observer sampling from runtime breaker sampling. Do not invent numeric thresholds or claim Azure quota headroom.

### 7. Documentary discrepancy and correction ledger

Give exact source references and **proposed text corrections only** (do not edit files) for:

- BTC-like simulation/twelve-selection identity versus misleading BTC/USD and ETH/USD market labeling;
- A2 preferred qualification candidate versus exclusive adoption language;
- exact C2 wording approved versus earlier “in-principle” characterization;
- ADR originally off-repository versus now untracked in repository;
- outdated C1 quota-source gate versus approved C2 wording;
- once/second versus once/ten-seconds qualification observer;
- offline JSON/lock journal versus sole-existing-SQLite storage constraint;
- Docker image packaging and current `aiq` account/permissions mismatch.

For each proposed edit identify exact file, section/lines, governing evidence and **separate owner authority required**. Do not infer permission to change any file.

### 8. Feasibility decision and gates

Provide a compact matrix with **exactly one** classification per mechanism: `FEASIBLE BY REPOSITORY EVIDENCE`, `CONDITIONALLY FEASIBLE`, `BLOCKED`, or `REQUIRES AZURE F1 EVIDENCE`. Separate design-rule adequacy from enforcement feasibility. Include minimum decisive evidence, security failure mode, and whether a source change, schema migration, Azure inventory, or bounded runtime test would require distinct authorization. Explicitly identify any mechanism that cannot be made feasible under the no-new-storage/no-recurring-cost constraint.

Conclude with a **GO/NO-GO for implementation selection** (not public activation), listing mandatory blockers. A design may remain NO-GO without proposing a forbidden fallback.

## Execution restrictions and output contract

**Permitted:** static, read-only inspection of local repository files and Git metadata (`status`, `rev-parse`, source search, line-numbered reads); reasoning and Markdown output in Codex's response.

**Prohibited:** modifying/creating repository files or other local artifacts; `git fetch`/checkout/stage/commit/push; PR/issue/project changes; builds/tests; Python or .NET execution; SQLite opens, queries or PRAGMAs; container execution; Azure calls or diagnostics; secret reads; deployment, schema migration or public activation. Do not run destructive or state-changing probes. If a needed fact is unavailable, mark it unverified.

Final response structure:

A. Baseline, scope and no-mutation attestation (compare starting and ending status)
B. Source-evidence ledger
C. Role/access and A2 launch-control contract
D. SQLite authority, states and failure-window contract
E. Independent whole-tree death-proof contract
F. Global topology and Azure F1 qualification dependencies
G. C2 accounting, health and sampling contract
H. Documentary conflict/correction ledger
I. Feasibility matrix and implementation-selection GO/NO-GO
J. Minimal separately authorizable next milestones

Do not characterize this exercise as architecture adoption or authorization for implementation. **Keep `PUBLIC_RUNTIME_VERIFIED=False`.**

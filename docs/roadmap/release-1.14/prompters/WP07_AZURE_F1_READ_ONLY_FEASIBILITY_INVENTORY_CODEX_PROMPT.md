# WP07 — Azure F1 Read-Only Feasibility Inventory — Codex Authority Request

**Status:** Proposed Codex prompt for owner review. **Not an authorization to access Azure.**

## 1. Mission and decision boundary

Establish whether the currently deployed Azure Linux App Service F1 environment plausibly supports the A2 identity-separation candidate, complete Worker-boundary termination proof, and the approved existing `/home` SQLite database as the sole durable admission authority. Gather **sanitized, non-mutating evidence** only, after the owner separately authorizes Azure read-only inventory. The result is an evidence report and a feasibility decision matrix, **not** implementation selection or runtime qualification.

The current repository contract-closure specification concluded **NO-GO for implementation selection**. Do not treat this inventory as an architecture adoption. A2 remains preferred for qualification only. No auxiliary databases, JSON/lock production admission authority, Azure Blob, paid services, or privileged fallback are permitted.

`PUBLIC_RUNTIME_VERIFIED=False` must remain unchanged. Preserve the frozen BTC-like synthetic simulation identity and twelve-selection contract; do not imply observed BTC/USD or ETH/USD spot-market candles.

## 2. Authority gate: stop before any Azure access

**This prompt alone grants authority to draft a plan and perform local, repository-only, read-only preparation.** It does **not** authorize Azure CLI commands, Kudu/SCM requests, portal actions, SSH, `az webapp ssh`, container `exec`, process inspection on Azure, reading app settings, or any remote diagnostics. Before the first Azure call, present a compact proposed inventory command list with target resource identifiers, exact information retrieved, privacy/redaction handling, and anticipated side effects. **STOP and request explicit owner approval for that exact Azure read-only scope.** If approval is not given, deliver a repository-only inventory plan marked **AZURE EVIDENCE NOT COLLECTED**.

After separately granted authority, use only the explicitly approved commands and resources. Do not broaden scope implicitly. A command that might mutate state or run an application, even briefly, requires a separate authority and is excluded from this milestone.

## 3. Baseline and sources to inspect locally (read-only)

Record `git branch --show-current`, `git rev-parse HEAD`, `git status --short` before and after. Inspect the actual working tree, including modified/untracked documents, without checkout, fetch, stash, add, commit or writing repository files. Prefer exact source/line citations.

Relevant sources include:

- `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_CONTRACT_CLOSURE_SPECIFICATION.md`
- `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_MECHANISM_CLOSURE_SPECIFICATION.md`
- `docs/roadmap/release-1.14/prompters/WP07_SQLITE_CONTAINER_IDENTITY_EVIDENCE_REPORT.md`
- `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_ARCHITECTURE_DECISION_RECORD.md` (proposal only)
- `docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_QUALIFICATION_PLAN.md` (known sampling/C1 conflicts)
- Four locally amended Release 1.14 governance documents, especially approved C2 wording
- `Dockerfile`, `container/entrypoint.sh`, `python/presentation/historical_market_bridge.py`
- `python/public_runtime/{bootstrap,broker,supervisor,journal,quota}.py`
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/` and `src/AIQuantTradingResearch.Worker/Program.cs`
- `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1`
- `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1`

Treat the reported repository baseline as historical until independently checked. Do not assume that Docker fallback `/runtime/aiq.sqlite` is the deployed SQLite path or that deployment scripts prove current Azure configuration.

## 4. Proposed Azure evidence tracks — each separately approval-gated

### AZ-1. Deployment scope and global admission topology

Propose sanitized, read-only inventory of subscription/resource group/app identity (avoid unnecessary identifiers in the report), Linux App Service F1 SKU, active slot(s), instance/scale settings, deployment/recycle behavior visible through metadata, image reference/digest if exposed, and whether any other enabled public entry point or overlapping revision could spawn Workers. Identify which observations are metadata and which would require live process diagnostics. Do not assume single instance or absence of recycle overlap.

### AZ-2. Database configuration and `/home` filesystem identity

Propose nonsecret checks of the configured persistence path, whether App Service storage is enabled, mount type/source and visibility, and whether all relevant processes/instances resolve to the same backing database lineage. **Do not open, query, connect to, hash, copy, back up, run PRAGMA against, or modify the SQLite file.** Do not read private tables or journal/sidecar contents. If file metadata, mount inspection or path traversal could expose sensitive information, request separate, narrow approval or mark unverified. A path string or `WEBSITES_ENABLE_APP_SERVICE_STORAGE` setting alone is not locking/fsync proof.

### AZ-3. Container identities and filesystem access feasibility

Propose sanitized, read-only evidence for effective UID/GID and supplementary groups of relevant roles, whether startup actually uses shared `aiq`, file/directory/sidecar *permission metadata only* if approved, writable trusted code/configuration, symlink/canonical-path risks, inherited descriptor exposure, and `/proc` visibility. No `chown`, `chmod`, `setcap`, identity changes, new users, privileged launches or file creation. Do not infer confidentiality from clean environments or synthetic-only DI.

### AZ-4. Kernel and process-boundary facilities

Propose non-invasive capability discovery for user/PID namespaces, pidfds, procfs restrictions, cgroup v2 membership and delegation, seccomp/no-new-privileges constraints, available signal/control permissions and lifecycle semantics. **No test namespace creation, Worker spawn, fork bomb, signal, kill, capability elevation, process-group manipulation, or orphan experiment.** A listed kernel feature is not proof that a constrained broker can use it or prove complete-tree death. Prior evidence indicates cgroup v2 without writable CPU delegation; reconfirm only if approved.

### AZ-5. SQLite durability and cross-instance semantics

Document the actual filesystem/mount identity and locate authoritative Azure/SQLite documentation relevant to rollback-journal DELETE mode, locking, fsync/flush and network filesystems. Distinguish vendor documentation, observed configuration and untested guarantees. **No live SQLite connection, contention, restart, corruption or crash test in this milestone.** If durability cannot be established from passive evidence, classify **REQUIRES SEPARATELY AUTHORIZED QUALIFICATION**; do not silently switch to WAL, a second SQLite file or another backend.

### AZ-6. F1 resource and C2 feasibility

Propose nonsecret metadata for SKU and quota/usage availability, without inventing quota telemetry or daily allowance. Evaluate what would be needed to measure total attributable CPU and global health, while preserving C2's approved local safeguards and the distinction between 240 reserved seconds and up to 300 measured job seconds before overhead. Do not run synthetic load or probes. Qualification observer proposal is at most one check per ten seconds per observer, within a finite declared batch; this is not authorization to execute health probes.

## 5. Confidentiality, cost and command safety

- No secrets, credentials, tokens, connection strings, private data, environment dumps, database contents or identifying application logs in output.
- Redact nonessential resource IDs and paths in any shared report while preserving enough evidence to reproduce approved checks privately.
- Read-only Azure control-plane calls may still have audit/logging implications; list them before seeking approval.
- Do not enable diagnostics, install extensions, restart/recycle, scale, deploy, modify app settings, grant permissions, change networking, or create resources.
- Do not execute Worker processes or exercise admission paths. Keep public runtime disabled.
- No GitHub actions, PRs, issues, commits or branch changes.
- No builds/tests or runtime processes on the target unless a later, explicit qualification authority covers them.

## 6. Required report format

Return in the chat (or as a proposed text artifact only if separately authorized to write a file):

A. **Authority record** — exact approved scope, or clearly state no Azure authorization and no Azure calls.

B. **Baseline** — branch, HEAD, unchanged status, source references and repository assumptions.

C. **Proposed/approved command ledger** — exact commands or APIs, purpose, target, read-only rationale, privacy implications, result or `NOT EXECUTED`.

D. **Evidence matrix** — AZ-1 through AZ-6, each labeled `PROVEN BY REPOSITORY`, `OBSERVED ON AZURE`, `DOCUMENTED EXTERNALLY`, `DESIGN INFERENCE`, `UNKNOWN`, or `BLOCKED`. Do not elevate metadata to runtime qualification.

E. **Mechanism assessment** — database/sidecar exclusion; cross-UID launch/control; independent complete-tree proof; shared SQLite locking/durability; deployment-global no-overlap; C2 accounting and zero added recurring cost. For each: `POTENTIALLY FEASIBLE`, `BLOCKED`, or `REQUIRES QUALIFICATION`, with minimum decisive evidence.

F. **Safety/decision** — explicitly state **NO-GO for implementation selection** unless separate future authority and evidence actually close all mandatory gates. No public activation.

G. **No-mutation attestation** — before/after repository status, exact Azure operations (if approved), and explicit declaration of no database reads/writes, Workers, probes, configuration changes or new resources.

## 7. Stop conditions

Immediately stop and report `BLOCKED / OWNER DECISION REQUIRED` if any inventory command needs elevated privileges, modifies the target, may read private SQLite contents or secrets, creates a process boundary, sends signals, triggers Worker execution, relies on paid infrastructure, or requires authority beyond the owner's exact approval.

**Final instruction:** Start with local repository-only preparation and an exact proposed Azure read-only command list. **Do not execute Azure inventory until the owner expressly approves that list.**

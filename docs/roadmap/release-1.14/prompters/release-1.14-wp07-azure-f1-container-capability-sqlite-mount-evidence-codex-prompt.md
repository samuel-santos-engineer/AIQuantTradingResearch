# WP07 — Azure F1 Container Capability and SQLite Mount Evidence

**Authority: planning and repository-only inspection. STOP before Azure/container commands.**
**Status: evidence milestone, not architecture adoption or implementation authorization.**

## 1. Mission and controlling context

You are Codex operating against the **current local working tree** of `AIQuantTradingResearch`. Inspect its actual branch, HEAD, tracked modifications, untracked WP07 documents and relevant source. Do not assume the previous reported baseline is still current.

WP07 is a synthetic-only, deterministic public preview. Preserve the **approved BTC-like simulation identity and twelve-selection contract**; do not describe synthetic candles as observed BTC/USD or ETH/USD market prices. `PUBLIC_RUNTIME_VERIFIED=False` is a mandatory gate and must remain unchanged. Private research data must remain confidential; public Worker process trees must never overlap; uncertain state must fail closed. The existing approved SQLite database under Azure `/home` is the **only permitted database/storage authority** for admission in this phase. No auxiliary database, Blob, JSON/lock production journal or paid resource fallback. A2 distinct-identity isolation remains a **preferred qualification candidate, not an adopted architecture**. Exact C2 policy wording is approved, but integration and numerical qualification are not.

The immediately preceding **approved Azure metadata-only inventory** reported two running Linux container apps, one in West Central US and one in West US 2, each on F1/Free, one configured worker, Always On false, no returned slots, and the same configured historical image digest. This is user-provided evidence, not proof of live image/process identity, global exclusion, mount properties or Worker reachability. Previous inventory consisted of eight approved metadata reads only; **that approval does not carry forward** to this milestone.

## 2. Strict authorization boundary

**Stage A only is authorized by this prompt:** read-only inspection of repository files and preparation of a proposed diagnostic command manifest and risk review. No repository file edits, output files written in the working tree, builds, tests, application execution, SQLite connection/PRAGMA, local container execution, Azure calls, GitHub calls, network requests, commits or deployment. Return the report in the Codex response unless separately authorized to save it.

**Stage B is NOT authorized.** Do not execute any Azure CLI, `az webapp ssh`, Kudu/SCM endpoint, SSH, container `exec`, `/proc` inspection, `stat`, `find`, `readlink`, `mount`, `cat`, `id`, `capsh`, namespace probe, app setting listing, filesystem access or remote HTTP probe under this prompt. First produce exact proposed commands and STOP for explicit owner approval specifying targets and commands. No command may be run merely because it is labelled read-only.

Do not open or query SQLite databases, journals, WAL/SHM files, research files, environment variable values, tokens, connection strings, logs, or private data. Do not invoke Worker code, launch processes for experimentation, create namespaces/cgroups, send signals, modify permissions, use `sudo`, restart/recycle, change app settings, scale, deploy, or allocate resources. Avoid any diagnostic that could trigger public runtime execution or incur recurring cost. If an inspection would require such activity, classify it as **requires separate, more invasive qualification authority** rather than proposing it as a safe read.

## 3. Stage A — repository evidence and exact target mapping

1. Capture branch, HEAD, relationship to locally known `origin/main` **without fetch**, and before/after `git status --short`. Record the source of each claim. Do not alter Git state.
2. Read the latest local WP07 reports, ADR, contract closure specification, qualification plan, four governing release/strategy documents, deployment scripts, Dockerfile, `container/entrypoint.sh`, `python/presentation/historical_market_bridge.py`, `python/public_runtime/` and SQLite configuration/adapter/bootstrap sources. Treat untracked and modified files as evidence, not approved policy unless authority is explicit.
3. Identify the **exact two Azure app resource IDs/names, resource groups and subscriptions** from already available sanitized repository evidence or the user's previously approved inventory, if present. Do not guess. If missing, use explicit placeholders and ask for owner confirmation; do not search Azure to discover them.
4. Identify repository-proven container default database path versus intended approved `/home` path, role startup behavior, image digest configuration, existing public gate and source-level Worker-capable routes. Distinguish configured from live effective behavior.
5. Identify the security boundary: root bootstrap, UI, private Worker, proposed admission authority, broker, public Worker, verifier; database/parent/sidecar traversal; inherited descriptors; immutable trusted code; and no supplementary-group leakage. Record current mismatches without prescribing implementation changes.

## 4. Stage B proposal — exact sanitized container evidence plan (DO NOT EXECUTE)

For **each** app, draft an explicit command-by-command plan. For each proposed command, provide: exact invocation with placeholders for confirmed target identifiers; host vs container execution context; required access; expected output **field names only**; sensitive-output risk; non-mutating justification; minimal redaction; and a STOP/skip condition. Separate commands into the following evidence tracks:

**AZ-C1 — Runtime identity and code trust.** Effective UID/GID and supplementary group identifiers of the inspected process; process capability sets (names or bitmasks), `NoNewPrivs`, seccomp/LSM indicators if available; process identity relationships; relevant filesystem owner/mode for application code. Do not dump complete `/proc/<pid>/status`, command lines, environment, maps, FDs, or process trees with private arguments. Propose narrowly filtered outputs only. Reading status is not permission to read memory or private descriptors.

**AZ-C2 — SQLite path and mount identity.** Nonsecret *path identity* of the configured persistence setting (without listing all app settings), canonical `/home` mount type/options and backing mount identification to the extent safely observable, relevant parent directory traversal/ownership/mode, and metadata-only DB/sidecar existence/access attributes **only if** an exact safe path and permission boundary can be established. Do not open the database or sidecars, enumerate private directories, dump mounts containing credentials, or follow untrusted symlinks. Flag any path-resolution step that risks private content.

**AZ-C3 — Kernel/process boundary feasibility.** Read-only indicators for user/PID namespace availability, process supervision primitives, pidfd support, relevant `/proc` visibility, cgroup version and delegation *metadata*. Do not attempt `unshare`, `clone`, `setns`, `prctl`, `setcap`, `kill`, `pidfd_open` experiments, cgroup writes or any privilege escalation. Metadata does not prove launch/control capability or complete-tree termination proof.

**AZ-C4 — Deployment-wide admission domain.** Compare both app identities, intended persistence configuration, shared versus separate `/home` storage domains, and plausible revision/recycle overlap. One configured worker and equal image digests are not proof of a single global SQLite authority. Do not infer common file identity from matching path strings. Note any Azure platform facts that cannot be established with the proposed commands.

**AZ-C5 — C2/F1 observations.** Identify only nonsecret, read-only resource metadata that could eventually bound the verified applicable allowance and health/accounting design. Do not infer available CPU quota from F1 SKU alone. Do not start health polling, load probes, Worker requests, metering tests or cost-generating resources. Preserve the distinction between 240 reserved seconds and 300 measured job seconds, plus overhead and uncertainty. The observer qualification cap is at most one check per ten seconds per observer, not permission to run a probe now.

For each track, distinguish (a) what Stage A already proves, (b) what the proposed Stage B metadata could show, and (c) what still needs **separately authorized active qualification**. Prefer the smallest set of commands needed for a decision; avoid broad discovery commands, output dumps and repeated polling.

## 5. Security and reliability gates

- **No public SQLite access:** DB, parent traversal, sidecars, inherited FDs, alternative paths and private research data must be excluded from UI, public Worker and descendants. Source-level DI separation is insufficient.
- **No overlapping Worker trees:** a durable pre-spawn occupation and budget charge, deployment-wide authority, replay-safe launch and independently proven whole-boundary death are mandatory. PID disappearance, signals, leases, `flock` release and timeouts are insufficient.
- **No automatic recovery reset:** current `ReadWriteCreate`/bootstrap cannot treat missing admission state as a fresh FREE state.
- **A2 constraints:** do not assume working cross-UID launch/cancellation, permanent privileged helpers, writable cgroup delegation or PID namespace creation. Evidence must establish actual feasibility.
- **Storage constraint:** the existing approved SQLite file under `/home` is the sole candidate; Azure filesystem locking/flush/crash semantics remain unqualified. No fallback architecture is authorized.
- **C2 constraints:** approved local admission safeguards, global health breaker, full CPU attribution and abuse controls remain mandatory; unknown quota telemetry alone is not an automatic denial under approved C2 wording, but missing mandatory safety evidence is.
- **Activation:** `PUBLIC_RUNTIME_VERIFIED=False`; no implementation selection or activation from metadata alone.

## 6. Required Codex response format

Return a single structured report in the response, **not a repository file**, containing:

A. Baseline, exact read-only actions performed, before/after status, and no-mutation attestation.

B. Source-indexed repository evidence, with file paths and line numbers; explicitly distinguish user-provided prior Azure observations from repository evidence.

C. Target mapping for the two Azure apps; placeholders for any unverified resource IDs; no invented identifiers.

D. A **proposed Stage B command manifest**, grouped by app and track, with exact commands, expected output fields, redaction and skip criteria. Mark every command `NOT EXECUTED — OWNER APPROVAL REQUIRED`.

E. Risk register: sensitive output, accidental SQLite access, possible service effects, insufficient privileges, filesystem ambiguity, and global-admission split authority.

F. Decision matrix with classifications `REPOSITORY-PROVEN`, `PREVIOUSLY OBSERVED AZURE METADATA`, `REQUIRES READ-ONLY CONTAINER EVIDENCE`, `REQUIRES ACTIVE QUALIFICATION`, or `BLOCKED`. Do not label A2 or SQLite runtime-safe based only on metadata.

G. Exact owner-approval request for Stage B, listing target apps, command IDs and requested scope. **Stop after requesting approval.** Do not chain Stage B execution into the same response.

H. Explicit final outcome: `NO-GO for implementation selection` unless independently qualified evidence already proves all mandatory mechanisms (not expected at this stage); `PUBLIC_RUNTIME_VERIFIED=False` unchanged.

## 7. Completion criterion

Success means a small, auditable, least-privilege **proposal** for container evidence that the owner can approve or reject command by command. It does **not** mean container evidence was collected, that Azure F1 supports A2, that SQLite `/home` is durable, or that WP07 may be implemented or activated.

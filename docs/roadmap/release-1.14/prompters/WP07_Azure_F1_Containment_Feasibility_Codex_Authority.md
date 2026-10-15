# Codex Authority — WP07 Azure F1 Containment Feasibility Investigation

**Project:** `samuel-santos-engineer/AIQuantTradingResearch`  
**Related:** Release 1.14, WP07, issue #322, draft PR #330  
**Authority class:** Narrow diagnostic investigation only — **not** implementation, merge, activation, or production acceptance authority.

## Owner authorization and objective

The owner authorizes a narrowly bounded, sequential **feasibility investigation** of PID-namespace-based process containment on the **existing tested Azure Linux F1 application instance**, followed **only if the first phase is successful** by filesystem isolation and cumulative CPU-accounting verification. The goal is to determine whether the free-tier runtime can provide independently demonstrable fail-closed isolation for a synthetic-only public Worker **without writable cgroup delegation**.

This prompt does **not** authorize modifying the repository, PR #330, issue #322, deployed production application, app settings, pricing tier, resource group, network configuration, or private research data. Public Worker invocation must remain disabled (`PUBLIC_RUNTIME_VERIFIED = False`). Do not merge or close WP07.

## Established baseline (do not reinterpret as acceptance)

The owner accepted the Azure F1 diagnostic as **instance-specific WP07 feasibility evidence**. On the tested West Central US application container: cgroup v2 CPU controller exists, but the cgroup filesystem is read-only even for root; no writable delegation exists; unprivileged user/mount namespaces, masking `/home`, and child-subreaper activation succeeded. Existing `aiq` permissions allow access to private market-cache and `/runtime` paths, so a complete private-data boundary is **not** proven. Original image was restored; Streamlit health and HTML were verified; West US 2 unchanged; both plans F1. These findings do not establish PID-namespace availability, process containment, cumulative CPU enforcement, or private filesystem isolation.

## Absolute restrictions

1. **Read-only discovery first.** Inspect existing project documentation, draft PR #330, deployed image metadata, and available non-sensitive capability evidence without modifying any tracked/untracked repository file. Do not create branches, commits, PRs, issues, or edit GitHub state.
2. **No standing deployment authority.** The previously performed temporary-image diagnostic does not authorize another image swap. Before any Azure-hosted active probe, present a specific, reversible execution plan and obtain **separate explicit owner approval** for that plan. If that approval is unavailable, stop after read-only discovery and report the blocker. Do not infer permission to deploy from this document.
3. **No privilege escalation or added resources.** No privileged container, host modifications, paid services, SKU changes, additional Azure services, persistent daemons, package installation, secrets, or outbound provider requests.
4. **No private-data reads.** Never open, enumerate contents of, copy, print, or transmit private market observations, credentials, tokens, `/runtime` artifacts, or provider files. Only non-sensitive permission/access tests against dedicated synthetic fixtures are permitted in a separately approved probe.
5. **No public activation.** Do not run public synthetic requests through the currently disabled Worker launch path; do not change production routing or `PUBLIC_RUNTIME_VERIFIED`.
6. **Fail closed and stop.** Abort if environment identity, permission scope, resource safety, reversibility, or private-data boundaries cannot be established. Never substitute process groups, subreapers, namespaces, or reservation ledgers for guarantees they do not prove.
7. **Bounded tests only.** No stress tests, fork bombs, indefinite processes, uncontrolled CPU loops, deliberate crashes of the production Streamlit process, or tests involving other tenants. All probe processes must have strict monotonic deadlines and explicit cleanup.

## Phase 0 — Read-only baseline and probe proposal

- Confirm exact target instance, active image digest, Linux kernel, current F1 tier, deployment and restoration mechanism, public invocation disabled, and healthy Streamlit endpoints; redact all secrets and identifiers unnecessary for evidence.
- Review WP07 contracts and PR #330 for existing admission, lock, CPU reservation, cleanup, and fail-closed semantics; distinguish implemented scaffolding from accepted behavior.
- Produce a threat model: Streamlit parent dies, guardian dies, Worker forks descendants, descendant ignores TERM, orphan process survives, lock owner dies, CPU ledger is incomplete, inherited file descriptor exposes private content, mount namespace is escaped.
- Produce an **owner approval checkpoint** describing exact ephemeral probe code, intended process tree, runtime limits, target deployment action (if any), rollback, health verification, and evidence files. **STOP here pending separate approval for any active Azure execution.**

## Phase 1 — PID namespace and crash containment (first active phase, only after approval)

Use only short-lived synthetic fixture processes, never private provider code. Verify and record:

1. Whether `CLONE_NEWPID`/`unshare --pid --fork` or equivalent PID namespace creation works under the actual `aiq` user and intended mount/user namespace configuration; distinguish namespace creation from effective containment.
2. Whether the dedicated guardian can become PID 1 inside the new PID namespace and establish a verifiable launch handshake **before** Worker creation.
3. Whether guardian/PID-1 exit terminates **all** Worker descendants, including TERM-resistant descendants; verify via independent outer-namespace identities (boot ID + PID start time where available), not merely PID reuse-prone numbers.
4. Whether parent-death detection, parent-to-guardian pipe EOF, guardian crash, handshake interruption, and timeout cause bounded cleanup without affecting the real Streamlit parent. Simulate parent death using a **dedicated disposable harness parent**.
5. Whether a guardian can retain admission ownership until cleanup, and what happens if the guardian itself dies. Do not claim that an open-file-description lock survives when its final holder exits.
6. Whether nested namespace, `/proc`, process-tree escape, or reparenting cases leave descendants outside the kill boundary.

**Phase-1 decision gate:** If PID namespace creation, guardian PID-1 lifecycle, or independent verification of complete descendant cleanup fails or is untestable, **STOP active investigation** and report an architecture blocker. Do not proceed to filesystem or CPU probes as if containment were established.

## Phase 2 — Filesystem isolation (only after Phase 1 passes and is authorized)

Investigate a minimal read-only Worker mount view using dedicated synthetic fixtures. Verify inaccessible private mount roots (`/home`, market-cache, `/runtime`, provider settings) **without reading private data**, and verify no inherited sensitive descriptors, symlink/path traversal, alternate bind mounts, `/proc` references, writable mount escape, or credential environment leakage. Determine whether a restricted network namespace with no external provider connectivity is supported; do not send real network requests to providers. Record the exact UID/GID, mount propagation, namespace creation order, and whether setup can fail closed. Any inability to establish the boundary is a blocker, not a waived test.

## Phase 3 — CPU accounting and failure recovery (only after Phase 1 passes and is authorized)

- Compare `wait4`/`getrusage(RUSAGE_CHILDREN)` and any permitted per-process kernel accounting with CPU used by live, adopted, and reaped descendants; test only bounded low-load synthetic work.
- Test parent/guardian failure, incomplete settlement, lock loss, boot change, PID reuse, and unverifiable residual processes. Document which usage is measurable, which is conservatively reserved, and which may remain **unbounded** after guardian death.
- Evaluate existing provisional WP07 budgets (one globally admitted child; 60 aggregate child CPU-seconds per rolling 15 minutes; 240/day; conservative 16-second reservation; 15-second admission-to-reap deadline) as **requirements to prove or explicitly amend**, not as evidence of enforcement.
- Distinguish admission throttling from hard cumulative CPU enforcement. Never claim `RUSAGE_CHILDREN`, elapsed time, or reservations alone enforce CPU ceilings. If no credible crash-safe upper bound exists, report **CPU containment not established** and recommend owner-reviewed contract revision.

## Safety, evidence and restoration

Any separately approved Azure diagnostic must use a temporary, non-public probe harness, minimal ephemeral artifacts, strict timeouts, and a predeclared restoration path. Record pre/post image digests, tier, health and HTML checks, active public invocation gate, and no-change confirmation for West US 2. On any failure, restore original image and independently verify health; if restoration fails, stop and escalate to owner. Do not leave probe processes, images deployed, scheduled jobs, or modified settings. Diagnostic images may exist only under separately approved retention/cleanup rules.

Store sanitized findings outside the repository unless the owner later authorizes an exact evidence path. Do not include secrets, raw private data, environment dumps, full process environment, or unredacted Azure credentials. Include command descriptions, observed exit status, kernel error codes, timestamps, process identity evidence, cleanup proof, and precise limitations. Mark tests that were not run as **NOT TESTED**, not passed.

## Required final report

Return a concise evidence-based report with:

1. **Scope and authority:** read-only-only or separately approved active diagnostic; exact approvals and target.
2. **Capability matrix:** PID namespace, guardian PID 1, parent death, guardian death, descendant cleanup, lock continuity, filesystem isolation, CPU accounting, network isolation; each **PASS / FAIL / NOT TESTED**, with supporting sanitized evidence.
3. **Safety and restoration:** pre/post state, health, image, F1 tier, invocation disabled, other region unchanged, cleanup proof.
4. **Guarantee gap analysis:** original WP07 requirements versus measured capabilities, distinguishing fail-closed detection, accounting, and actual kernel enforcement.
5. **Decision:** feasible with evidence; blocked; or inconclusive. State exactly what further owner authorization or architecture amendment is needed.
6. **Repository impact:** explicit statement of no repository/GitHub modifications, or stop and disclose any unexpected change.

## Completion rule

This authority ends with the feasibility report. **It does not authorize implementation, PR updates, merge, WP07 closure, dependent WP05/WP08 activation, deployment of a revised Worker, or public access.** Seek a new literal-path mutation allowlist and separate owner implementation authority for any subsequent architecture change.

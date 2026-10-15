# Codex Prompt — WP07 Option B / C2 Contract Amendment (Planning Only)

## Authority and intent

The owner has **accepted the residual availability risk** that Azure Linux F1 quota exhaustion may occur despite conservative local admission budgets, potentially making the public synthetic chart unavailable. This is an **in-principle C2 risk decision**, not approval of effective policy text, implementation, diagnostics, deployment, or activation.

**Your assignment is READ-ONLY repository analysis and a proposed, reviewable contract amendment.** Do not modify repository files, create commits, push, open/update/merge PRs, close issues, run tests/probes, call Azure, change RBAC, build images, deploy, or enable public invocation. Do not create a file in the repository. Return the proposed wording and exact literal-path documentation mutation proposal in your response only. The owner will separately authorize any actual documentation edits.

Controlling baseline: `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, Section 10, and the Release 1.14 definition, execution plan and file manifest. Read these exact files and relevant existing WP07 planning records before drafting. Do not assume uncommitted documents are approved policy. Keep `PUBLIC_RUNTIME_VERIFIED = False`, PR #330 draft, issue #322 open and the offline policy implementation frozen.

## Owner decision to encode

Propose **C2 — conservative local-budget admission** in place of the current requirement that each new generation has authoritative Azure quota observations no older than 60 seconds and more than 20% remaining headroom in both quota classes. The owner expressly accepts residual Azure F1 quota exhaustion and temporary public-chart unavailability. The owner **does not** accept weaker confidentiality, overlapping Worker trees, uncertain-state reopening, uncontrolled abuse, new recurring costs or privileged infrastructure.

No other architecture alternative is approved by this decision. A2 (distinct broker UID) remains a preferred candidate for qualification; B2 (existing transactional storage) remains conditional on actual resource, confidentiality, consistency and permanent zero-incremental-recurring-cost evidence. Do not select a backend, change a role map, or claim target feasibility.

## Required proposed normative text

Draft a precise, narrowly scoped replacement for the quota freshness/headroom prerequisite, preserving all unaffected Section 10 requirements. Include:

1. **Admission domain and durable state.** One authoritative admission domain across every enabled public entry point; exactly one occupied Worker tree; durably reserve/occupy before any Worker creation. No admission after supervisor/guardian death, released flock, lost lease, process timeout, restart, clock rollback, missing/corrupt journal or unverifiable cleanup. Independent process-tree death verification and durable settlement are prerequisites for clearing occupation.
2. **Conservative global budgets.** Retain candidate upper bounds pending qualification: 16 reserved CPU-seconds per actual Worker spawn; no more than 3 admissions per rolling 900 seconds and 15 per rolling 86,400 seconds; reserved caps 60/240 seconds in those windows; full reservation for uncertain launches; no refund based on incomplete measurement. Both count and reservation limits apply. These are **not hard CPU ceilings**. An actual warm-cache Worker spawn is also charged.
3. **Honest quota semantics.** Azure quota measurements without a verifiable observation timestamp are unknown, not fresh. Missing/stale/unverifiable telemetry does not alone bar admission under C2 if every independently mandatory local, isolation, journal and health condition is met. Authenticated and trustworthy evidence of exhaustion or unsafe conditions may tighten denial. Do not assert >20% actual Azure headroom when unknown; no owner credentials in the app.
4. **Availability-risk acceptance.** Explicitly state that F1 quota exhaustion, throttling and temporary chart unavailability may occur even with compliant budgets; other application CPU, supervisor/broker/orphan CPU, anonymous ingress, telemetry lag and runaway CPU are not controlled by reservation arithmetic. No writable cgroup delegation was demonstrated; no kernel hard process-tree CPU ceiling is claimed.
5. **Accounting reconciliation.** 15 admissions × 16 reserved seconds = 240 reserved seconds, whereas 15 × 20 measured seconds = 300 measured job seconds before supervisor, idle, orphan and other costs. The proposed <=10% daily allocation cannot be inferred from 240 reserved seconds; define an evidence-based total-attributable-CPU evaluation and require tighter budgets if necessary. Do not invent a verified daily F1 quota or measurements.
6. **Abuse and health protection.** Preserve global authoritative admission limits, supplementary per-client/session throttling, bounded validation, no unbounded queues/retries, cached-read rate/concurrency limits and independent health responsiveness. Candidate limits from the qualification plan (e.g., 10-second session cooldown; 3 expensive attempts/client/60 seconds; 30 cached reads/global/60 seconds; 2/global/second; 6/session/60 seconds; one active cached read) are **candidates**, not qualified production values. Specify a fail-closed global health circuit breaker, but leave trip/recovery thresholds explicitly unresolved pending owner review and evidence. Breaker recovery never clears occupation or replenishes budgets.
7. **Non-negotiable security.** Synthetic-only anonymous content; no public Twelve Data/Vike access, private cache, observations, credentials, research data or provider network. Fixed-role broker/Worker isolation and durable journal are unqualified. No public recovery command or assumption that same-UID peer credentials prove broker isolation.
8. **Governance and acceptance.** Reconcile G-B0 through G-B7 and the relevant Release 1.14 acceptance criteria, distinguish C2 policy acceptance from qualification, identify obsolete C1 freshness/headroom assertions precisely, and preserve all remaining isolation, recovery, abuse, resource and Azure F1 operational gates. Public invocation remains disabled until separate owner acceptance and activation authority.

## Repository analysis and output format

Read only. Produce these sections in order:

A. **Baseline citations:** exact repository-relative path, heading and line range for each controlling passage requiring amendment; distinguish committed policy from uncommitted proposals.

B. **Proposed Section 10 amendment:** exact replacement/addition wording, clearly marked `PROPOSED — NOT EFFECTIVE`.

C. **Release 1.14 reconciliation:** precise proposed edits for definition, execution plan, manifest and G-B0–G-B7 / AC mappings; do not invent AC numbers or claim gate passage.

D. **Security invariants retained:** explicit before/after matrix, with confidentiality, no overlap, durable recovery, abuse, cost and activation boundaries unchanged.

E. **Open decisions:** health-breaker trip/recovery thresholds, qualified measured CPU envelope, total attributable CPU vs daily F1 allowance, admission topology, backend durability, Worker/broker boundary and cached-read ingress enforcement.

F. **Exact documentation mutation allowlist proposal:** literal repository-relative paths only, with per-file rationale and operation; no wildcards or inferred paths. The expected initial candidates are:
- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

If additional paths are necessary, **propose** them separately; do not mutate any file. Existing prompter documents are evidence, not automatically approved mutation targets.

G. **Owner decision checklist:** explicit approvals needed before effective contract edits, diagnostic authority, source/test mutation, live integration, PR merge, deployment and activation.

H. **Actions performed:** attest to read-only repository analysis and list any limitations. Do not claim verification you did not perform.

## Stop conditions

Stop and report a blocker rather than improvising if controlling text cannot be located, an existing approved invariant conflicts with C2, exact AC/gate mappings are ambiguous, or the proposal would require privileged Azure changes, a new paid resource, private-data access or unapproved implementation. Do not alter the frozen source/test snapshot or its SHA-256 evidence.

# WP07 — Architecture Alternatives Decision Record

Date: 2026-10-10. Status: **proposed for owner decision; no alternative selected or activated.**

## Context and decision boundary

This record compares three independent architecture choices following the approved planning/evidence framework and [read-only architecture closure](WP07_READ_ONLY_ARCHITECTURE_CLOSURE.md). The [frozen assessment](WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md) remains the implementation baseline. The owner recommends preserving confidentiality and no-overlapping-Worker admission while considering a documented quota-availability amendment. That recommendation authorizes this document, not a contract change.

Only documentary research and repository reads were performed. No tests, harnesses, fault injection, Azure queries/diagnostics, code changes, RBAC/configuration changes, deployment or activation occurred. Existing resources, pricing coverage and target capabilities have not been inventoried in this review. “Existing transactional resource” is a conditional alternative, not a claim that an eligible resource exists.

Non-negotiable gates for every combination:

- Public UI/Worker and descendants cannot access private observations, provider credentials, supervisor authority or broker control authority.
- One authoritative durable admission domain covers every public entry point. Occupation is committed before Worker creation and retained through uncertain failures; no overlapping Worker trees, timeout-based reopening or public recovery commands.
- Independent process-tree cleanup verification precedes durable settlement. A lock release, lease expiry, PID disappearance or CPU report alone is insufficient.
- Corrupt/missing journal, uncertain ownership, unsupported isolation or unverifiable recovery denies generation.
- Public invocation remains disabled until separately authorized implementation, qualification and owner acceptance.

Zero recurring cost is evaluated literally: an existing billable resource does not prove zero added recurring charges, and credits/trials do not prove permanent free operation. No paid fallback is presumed.

## A. Broker/Worker identity and kernel boundary

| Criterion | A1 — Same UID, additional kernel isolation | A2 — Distinct broker UID |
|---|---|---|
| Confidentiality | Conditional on private PID/mount/proc/network views, unavailable control endpoints/descriptors, immutable code and constrained syscall/capability access. UID equality and peer-UID checks cannot authenticate broker versus Worker. | Stronger direct credential separation for ordinary file/signal/tracing permissions. Still needs private filesystem/network/descriptor isolation and authenticated IPC; distinct UID alone is insufficient. |
| Crash-safe admission | Same-identity broker can potentially signal/reap Worker, but only if Worker cannot disable or impersonate the broker. Combined broker/guardian death remains a proof obligation. | Broker cannot assume permission to signal or change into another UID. Needs a complete fixed-role launch/cleanup mechanism established before permanent privilege drop; no resident privileged launcher is accepted by default. |
| Azure F1 feasibility | Unknown: required unprivileged namespace/syscall facilities and .NET compatibility are not proved. Writable cgroup delegation remains unavailable in prior evidence. | Account creation is not the main obstacle: per-job identity transition, cleanup authority and namespace support are unresolved under the unprivileged-runtime constraint. |
| Zero recurring cost | No additional service necessarily required; diagnostic and runtime consumption still use existing F1 allowance. | No additional service necessarily required if finite bootstrap suffices; a new supervisor service would violate the current boundary. |
| Complexity | High: all shared-identity escape/control paths must be closed together. | High: simpler credential argument, harder launch and cross-identity cancellation/lifecycle design. |
| Residual availability risk | Broker compromise or unproven cleanup must leave occupation blocked. Namespace failures and resource starvation deny use. | Lost cleanup authority or fixed-role helper death must leave occupation blocked. Additional role/handshake increases failure paths. |

Linux signal permissions depend on credentials; ptrace also considers dumpability, capabilities and security modules. Non-dumpable is not a signal boundary. Child PID namespaces restrict process visibility but do not hide sockets or inherited file descriptors. These support A1's mechanism argument, not its target qualification. Sources: [kill(2)](https://man7.org/linux/man-pages/man2/kill.2.html), [ptrace(2)](https://www.man7.org/linux/man-pages/man2/ptrace.2.html), [PID namespaces](https://www.man7.org/linux/man-pages/man7/pid_namespaces.7.html).

**Proposed decision:** prefer A2's credential separation as the security argument to investigate, without selecting an implementation. Retain A1 only if a complete kernel boundary protects broker authority against an adversarial Worker. Reject either option if it requires weakening confidentiality or no-overlap. A2 would require explicit amendment of the prior shared broker/Worker role map and review of launch/cleanup authority. No fourth numeric identity or privileged helper is assigned here.

**Exit evidence:** immutable role/FD/path/capability map, Worker-to-broker access argument, fixed launch sequence, independent descendant identity/cleanup model, and separately authorized Q-ID/Q-CF evidence. No architecture choice is feasible merely because ordinary honest Worker execution succeeds.

## B. Durable journal backend

| Criterion | B1 — Existing filesystem journal | B2 — Existing transactional storage resource |
|---|---|---|
| Confidentiality | Depends on actual mount ownership/ACLs and a Worker-inaccessible view; successful chmod/chown is not proof. | Depends on narrowly scoped service authorization, token/endpoint isolation and protected state access. Existing app-wide identity may expose excessive authority. |
| Crash-safe admission | Requires verified locks, atomic replacement, acknowledged file/directory persistence and conservative recovery across deployment scope. Current Azure evidence is insufficient. | Supported atomic transaction or conditional-write semantics can provide one authoritative state version. Still requires fencing, durable occupation and cleanup before clearing; service lock/lease loss is not permission to launch. |
| Azure F1 feasibility | Platform `/home` persists when enabled, but exact journal semantics are unqualified. Custom-mounted storage guidance warns against lock-dependent components. | Conditional on a genuinely existing eligible resource, permitted network access, suitable consistency/scope and credentials. No inventory or target verification performed. |
| Zero recurring cost | May use included App Service storage without a new resource; inclusion/settings must be verified. | Unproved: existing capacity may still incur transaction, storage or network charges. Requires explicit evidence of permanent zero incremental charges under bounded operation. |
| Complexity | Lower local protocol complexity; high filesystem durability/permissions proof burden. | Higher network/auth/version/retry complexity; clearer supported atomicity if the chosen resource supplies it. |
| Residual availability risk | Mount/lock/flush uncertainty or corrupt state blocks admissions; recovery may need operator intervention. | Service/network/auth outages, throttling and uncertain acknowledgments block admissions. Conservative occupation may survive an otherwise recovered service. |

App Service persistence documentation does not establish this journal's complete crash transaction. Microsoft's custom-storage guidance discourages components relying on file handles/locks. Sources: [persistent container storage](https://learn.microsoft.com/azure/app-service/configure-custom-container), [storage mounts](https://learn.microsoft.com/en-us/azure/app-service/configure-connect-to-azure-storage?pivots=container-linux&tabs=portal).

As an illustrative B2 primitive, Azure Blob Storage documents strong consistency and ETag conditional updates. A single bounded state object updated with version preconditions could be evaluated; this is not a multi-object transaction guarantee or resource selection. A failed/unknown write acknowledgment requires authoritative reread or continued denial, not a blind new reservation. Source: [Blob concurrency](https://learn.microsoft.com/en-us/azure/storage/blobs/concurrency-manage).

Existing Blob resources are not automatically free: pricing includes storage and transaction dimensions. No amount, free allowance or account eligibility is asserted here. Source: [Blob billing factors](https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview).

**Proposed decision:** prefer supported transactional semantics if an existing resource meets confidentiality, zero recurring cost and deployment-wide atomicity. Otherwise retain B1 only with backend-specific supported guarantees and target qualification. If neither meets all gates, generation stays unavailable; do not default to ephemeral state, SQLite on an unsupported mount or lease expiry.

**Exit evidence:** exact existing resource/backend identity, authoritative atomicity/persistence guarantees, ownership/auth scope, bounded cost model including denied requests/retries, and separately authorized Q-ST/Q-CF evidence. B2 requires a backend/credential/protocol amendment and separate literal-path authority; no migration is authorized.

## C. Quota-availability policy

| Criterion | C1 — Strict Azure quota freshness | C2 — Explicit conservative local-budget amendment |
|---|---|---|
| Confidentiality | Collector must isolate workload identity/header/tokens from public roles; no owner credentials. | Avoids mandatory quota-token acquisition for admission, but cannot weaken private-data or broker boundaries. Optional telemetry still needs isolation. |
| Crash-safe admission | Adds a quota predicate to the same durable occupied state; fresh telemetry never clears occupation. | Same durable state and cleanup gates remain mandatory. Budgets cannot reset on process restart, identity rotation, clock rollback or storage uncertainty. |
| Azure F1 feasibility | Blocked by undocumented actual observation time/lag for quota usage; authentication alone cannot fix this. | Potentially removes that particular blocker. Does not solve kernel isolation, journal feasibility, accounting or lack of hard CPU containment. |
| Zero recurring cost | Needs approved acquisition without new paid service; no live adapter qualified. | No new service inherently required. Requests, CPU, journal operations and health traffic still consume existing allowances; zero cost must hold for the selected backend too. |
| Complexity | High acquisition/auth/freshness/replay/reset reconciliation burden; least ambiguity when source evidence exists. | Lower acquisition burden, higher policy/accounting and shared-platform-load risk. Needs explicit limits, conservative charging and health circuit breaker. |
| Residual availability risk | Often unavailable until trustworthy <=60-second observations exist; stale/missing evidence denies. | Higher risk of shared app/platform quota exhaustion or health degradation despite local limits. No authoritative claim of >20% platform headroom when telemetry is unavailable. |

The app usage schema documents counter, limit, name, reset and unit but no actual observation timestamp. Receipt time and reset time cannot satisfy the current rule. Source: [List Usages](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/list-usages?view=rest-appservice-2025-05-01). C1 remains the effective policy unless the owner explicitly accepts an amendment.

### Proposed C2 amendment for a separate owner decision

Replace only the requirement that new generation must have authoritative <=60-second Azure quota observations and >20% remaining quota in both classes. Permit admission under a qualified, durable, global local reservation policy when such telemetry is unavailable. Keep telemetry advisory unless its authority/freshness is proven; trustworthy evidence of exhaustion or unsafe headroom may tighten denial. Missing telemetry must be disclosed as unknown, never represented as fresh or healthy.

Retain the existing candidate limits as upper bounds pending qualification: one occupied job; full 16 CPU-second reservation/job; at most three admissions/rolling 900 seconds and fifteen/rolling 86400 seconds; reserved caps 60/240 seconds. Each actual Worker spawn, warm or cold, consumes admission. Charge before creation; no refunds based on incomplete CPU measurements. Preserve client/session/cached-read controls and bounded cheap denials. Local state spans the entire admission domain, not one replica's private counter.

These numerical reservations are **not hard CPU ceilings**. At the proposed measured 20-second/job acceptance value, fifteen jobs represent up to 300 seconds of measured job consumption, not 240 seconds; supervisor/idle/orphan and other app costs are additional. Thus 240 reserved seconds does not prove the <=10% daily allocation. The accounting model must show total attributable consumption and uncertainty against the verified allowance; tighten admission limits if needed. Do not change constants merely to mask this gap.

Require a qualified global health breaker: deny generation on unsafe health, accounting loss, uncertain cleanup, journal/clock failure or unsupported topology. Recovery from the breaker cannot clear occupation or replenish rolling budgets. Exact trip/recovery thresholds and baseline/reset accounting need a separate reviewed policy specification; current health acceptance criteria are not automatically a complete runtime breaker.

Explicitly accept that local budgets cannot observe all F1 consumption, guarantee platform headroom or prevent runaway CPU without kernel containment. Other app work, platform accounting lag, anonymous ingress and unmeasured failures can still cause quota throttling/unavailability. If that residual availability risk is unacceptable, C1 remains required and generation remains disabled.

Do not alter confidentiality, process-tree death proof, durable admission or no-overlap. Do not claim that measured <=20 seconds/job enforces a kernel ceiling. Retain resource/abuse qualification and owner acceptance before activation. The amendment must name and reconcile every affected controlling document and G-B quota/headroom criterion before implementation; this ADR does not perform that reconciliation.

**Proposed decision:** investigate C2 as the most targeted policy change, since C1's documented timestamp blocker is concrete. Prefer explicit residual availability risk over invented freshness. No C2 approval is inferred from the owner's recommendation.

## Combined decision and consequences

The choices are independent prerequisites, not substitutes. C2 cannot make A1/A2 or B1/B2 safe; a strong backend cannot establish tree death; distinct UIDs cannot establish durable occupation. No combination is presently qualified.

Recommended review direction: A2 boundary/launch argument; B2 only if an eligible existing resource and permanent zero-cost operation can be established, otherwise qualified B1 or continued denial; C2 as an explicit proposed availability-policy amendment. This is a research preference, not an implementation instruction.

If no option meets confidentiality, no-overlap and zero recurring cost, the valid outcome is continued disabled public invocation. Do not trade those gates for availability. Remaining evidence requests are documentary: exact resource/cost inventory provided under separate access authority, kernel boundary design, backend guarantees, and owner disposition of C2's residual risk. Harness/source changes, privilege or RBAC changes, active diagnostics, deployment and activation each require separate explicit authorization.

Owner decision record: **pending** for A, B and C. Planning/evidence framework approval remains recorded; frozen implementation unchanged.

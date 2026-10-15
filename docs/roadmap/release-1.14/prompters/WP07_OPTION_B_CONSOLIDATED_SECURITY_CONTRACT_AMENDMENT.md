# WP07 — Consolidated Option B Security-Contract Amendment Proposal

**Project:** AIQuantTradingResearch  
**Release:** 1.14 — Technical Analysis & Quantitative Feature Foundation  
**Work package:** WP07 — Public Runtime Isolation / Private Preservation  
**Related:** Issue #322; draft PR #330; milestone #65  
**Status:** **PROPOSAL — OWNER REVIEW REQUIRED; NOT AN EFFECTIVE POLICY AMENDMENT**  
**Planning direction:** Owner selected Option B (external supervision with explicitly bounded availability risk), and approved inclusion of anonymous-abuse / F1-quota protections in this proposal.  
**Authority:** Read-only architecture planning and preparation of this review artifact only. No repository mutations, implementation, active probes, deployments, merges, WP07 acceptance, or public activation.

## 1. Purpose and controlling authority

This proposal consolidates the Option B security-contract changes required before WP07 implementation can resume. The controlling policy remains **Section 10** of `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` until an explicit owner-approved amendment is applied under a separate, literal-path documentation mutation authority.

Documents potentially requiring subsequent reconciliation (not authorized for mutation here):

- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` — Section 10, controlling policy.
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` — requirements, limits, acceptance criteria.
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md` — WP07/WP08 dependencies and gates.
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` — any later exact-path authorization alignment.

This document does **not** itself amend any repository file or supersede Section 10. Exact section/line edits must be independently reviewed against the then-current repository version.

## 2. Evidence and constraints

1. The tested West Central US Azure Linux F1 application container exposed cgroup v2 and the CPU controller, but its cgroup filesystem was read-only, including for root; no writable delegated public cgroup was available. This is **instance-specific evidence**, not a claim about every F1 host.
2. Unprivileged user/mount namespaces and masking `/home` succeeded in the tested instance; the existing `aiq` identity could access the private market cache and `/runtime`. A complete confidentiality boundary was **not** demonstrated.
3. Phase 1A observed PID-namespace-init death terminating TERM-resistant descendants in one corrected test; universal lifecycle/escape guarantees were not established.
4. Phase 1B **failed** the guardian-only admission model: after guardian SIGKILL, the admission lock was acquired about **1.674915 ms** later, before registered guardian, Worker and grandchild pidfds signalled exit. No second Worker was launched; continued runnable CPU use during the gap was not measured. A guardian-held flock is therefore **not** sufficient admission fencing.
5. Filesystem/network isolation, remaining lifecycle scenarios and hard cumulative CPU enforcement were **not** qualified. Phase 2/3 active tests were stopped.
6. The Phase 1B temporary GHCR image version `1367169458` was reported deleted and registry absence, preserved production digest and unchanged Azure image references verified by the owner. The original application image had been restored and Streamlit health verified.
7. Draft PR #330 is partial fail-closed implementation with `PUBLIC_RUNTIME_VERIFIED = False`; it does not establish WP07 completion. Issue #322 remains open.

## 3. Security-contract decision and threat model

**Selected planning direction:** Option B — retain the approved C# synthetic generator, `aiq-ta-v1` analytics, canonical contracts and bounded Worker byte transport under an externally supervised execution model. Accept a **documented, measured residual availability/resource risk** in place of hard per-request and rolling CPU-consumption enforcement. Do **not** relax confidentiality, synthetic-only provenance, global admission correctness or fail-closed cleanup.

Protected assets: private Twelve Data/Vike observations, provider secrets, private market caches and `/runtime` handoffs; public application availability; finite F1 CPU/memory/quota; deterministic synthetic data integrity and provenance.

Threats to address: anonymous floods and client-identity rotation; simultaneous tabs/sessions; costly cold-generation amplification; malicious or malformed inputs; Worker/guardian/supervisor/UI death; interrupted handshake; stale or corrupt ledger; orphan processes; private filesystem, descriptor, environment, `/proc`, mount or network access; quota starvation by repeated cache misses **and** excessive cache hits.

An anonymous IP, cookie or Streamlit session is not a trustworthy unique user identity. Per-client limits are supplementary; **global limits are mandatory**. No protection implemented only inside Streamlit can eliminate all inbound HTTP/platform overhead or guarantee Azure-metered quota preservation.

## 4. Requirement-by-requirement disposition

| Contract requirement | Proposed disposition | Owner-review consequence |
|---|---|---|
| Anonymous deterministic BTC-like synthetic OHLCV only; exact public labeling | **Preserve** | No public observed candles or provider fallback |
| Existing C# synthetic generator, canonical fingerprints, `aiq-ta-v1`, 12 selections and 250 hidden warmup candles | **Preserve** | No Python production port or parallel algorithm |
| Canonical v2 response/provenance, bounded raw UTF-8 Worker transport | **Preserve** | Changes require separate explicit contract review |
| No anonymous Twelve Data/Vike calls, observations or secrets | **Preserve — mandatory** | Fail confidentiality gate if unverified |
| Private filesystem, environment, inherited descriptor, process and network boundary | **Preserve — mandatory** | Namespace creation alone is insufficient |
| Globally exclusive public generation admission and cleanup verification | **Preserve — mandatory** | Phase 1B regression must be defeated |
| Guardian-only lock ownership as sufficient admission proof | **Reject / supersede design assumption** | Durable occupied state must survive guardian/supervisor failure |
| Hard per-request CPU-seconds enforcement | **Revise** | Cooperative cancellation and deadlines are not hard CPU enforcement |
| Hard 60 CPU-seconds/rolling 15 min and 240/day consumption ceilings | **Revise** | Propose *admission-reservation budgets*, not enforced consumption ceilings |
| Conservative per-job reservation, timeout, bounded capture and retry | **Preserve as provisional controls; revalidate** | Reservation is not proven upper bound on actual CPU |
| F1 free tier, no privileged container, no added paid services | **Preserve** | Target-runtime evidence required |
| Anonymous request abuse and quota starvation controls | **Add — mandatory (Amendment F)** | New gate G-B7 before activation |
| Public invocation disabled until full acceptance and separate activation | **Preserve — mandatory** | `PUBLIC_RUNTIME_VERIFIED = False` remains |

## 5. Proposed normative amendments (candidate policy language)

### A — Execution and synthetic-only provenance

1. Anonymous historical-market requests **SHALL** use only the independently deterministic approved BTC-like synthetic OHLCV implementation and approved analytics. The C# generator, shared indicator calculator, canonical dataset/version/seed/fingerprints, 12 selections, 250 hidden warmup candles and visible-only projections **SHALL** remain authoritative.
2. The public execution path **SHALL NOT** construct or call private Twelve Data/Vike adapters, consume observed-data caches, perform provider fallback or access private handoffs.
3. The existing bounded Worker byte protocol **SHALL** be preserved unless separately amended. Public requests **SHALL** be strictly validated before costly work.

### B — Supervisor, admission and crash recovery

1. A trusted admission authority **SHALL** durably establish an **occupied** state and conservative reservation **before any Worker can exist**. All admission contenders in the explicitly defined admission domain **SHALL** use the same enforcement path.
2. At most **one** public synthetic generation job **SHALL** be admitted within that domain. An unregistered-Worker creation window is unacceptable; the startup protocol **SHALL** prevent Worker creation before auditable registration and verified START.
3. Guardian death, supervisor death, final flock-holder death, UI death, expired lease/deadline, numeric PID reuse or process-group kill **SHALL NOT** alone clear occupied state.
4. New admission **SHALL** require independently verified complete termination/cleanup of the prior registered execution boundary and durable settlement. Missing, corrupt, ambiguous, inconsistent or inaccessible evidence **SHALL** fail closed, including after restart.
5. Boot identity, process start identity and available pidfd evidence **SHALL** be treated according to their actual persistence limitations. PIDfds cannot simply be reconstructed as equivalent evidence after arbitrary owner death. Time passage alone **SHALL NOT** prove cleanup.
6. Recovery **MAY** require documented operator intervention, but **SHALL NOT** clear occupation without adequate independent evidence. A durable occupied record prevents overlap; by itself it does **not** kill processes or bound CPU.
7. The architecture **SHALL** define its admission domain (single container vs multiple instances/replicas), its shared-state durability, and the behavior when those assumptions are violated. A local lock **SHALL NOT** be called deployment-global without proof.

### C — Revised CPU and memory resource contract

1. The existing rolling **60 CPU-seconds/15 minutes** and **240 CPU-seconds/day** values are proposed as **provisional conservative admission-reservation budgets**, not hard actual CPU-consumption ceilings. The **16 CPU-second/job** reservation and proposed **3 admissions/15 minutes, 15/day** are also provisional pending arithmetic, policy and F1 capacity validation.
2. Admission accounting **SHALL** be atomic, durable and fail closed on inconsistent state, clock rollback, missing evidence or incomplete settlement. Conservative full-reservation charging **SHALL** apply when exact use is unknown, without claiming that reservation bounds actual CPU.
3. The architecture **SHALL** bound request/response sizes, concurrency, retries, in-flight allocations and operation deadlines; **SHALL** document and measure possible overshoot, supervisor overhead and cleanup latency; and **SHALL NOT** claim that `wait4`, `rusage`, cooperative cancellation, wall-clock deadlines, `DOTNET_PROCESSOR_COUNT=1` or a reservation ledger independently enforce hard CPU ceilings.
4. The approved residual risk **MAY** include CPU use beyond a reservation, temporary public-chart unavailability, manual recovery and measured Streamlit degradation. It **SHALL NOT** include private-data exposure, provider calls, unverified overlapping jobs or silent reopening after crash.
5. The public workload **SHALL** reserve measurable operating headroom for Streamlit health, ordinary chart viewing and platform quota. Exact CPU/memory/performance thresholds require a separate owner decision informed by measured F1 behavior; internal budget values are not Azure's platform-metered quota.

### D — Confidentiality and isolation (not relaxed)

1. Public Worker execution **SHALL** have no effective access to private provider credentials, market caches, research artifacts, private `/runtime` handoffs or other private observations through mounts, inherited descriptors, environment, symlinks, `/proc` or alternative paths.
2. Public execution **SHALL** be denied outbound provider access. Network-namespace or equivalent isolation is a candidate to be separately verified; no mechanism is presumed sufficient without target-runtime evidence.
3. Public/private startup separation **SHALL** prevent inherited private configuration or handles. A separate process with the same effective private permissions **SHALL NOT** count as an isolation boundary.
4. No private file contents or credentials **SHALL** be collected in verification artifacts.

### E — Availability, failure and operations

1. Unknown cleanup or admission state **SHALL** deny new public generation requests. Failure responses **SHALL** be bounded, non-retrying by default and free of sensitive details.
2. The application **MAY** prefer temporary generation unavailability over unsafe readmission. Operator recovery **SHALL** be documented, observable, auditable and evidence-based.
3. Public activation **SHALL** require separate authorization after every applicable gate passes. Draft/merged code or an accepted planning amendment **SHALL NOT** imply activation authority.

### F — Anonymous request abuse prevention and Azure F1 quota protection (**owner-approved inclusion in proposal**)

1. The public application **SHALL** reject invalid, oversized or disallowed requests using cheap bounded validation **before** expensive generation or cache work.
2. It **SHALL** enforce both (a) per-client/session throttling as defense in depth and (b) **durable global admission frequency and quota-reservation limits** that cannot be bypassed merely by changing tabs, sessions, IP addresses or query parameters.
3. Anonymous client identifiers **SHALL NOT** be treated as verified identities; IP-based controls **SHALL** account for shared addresses and proxy/header trust. Client identity rotation **SHALL NOT** bypass global controls.
4. At most one generation job **SHALL** be admitted in the approved domain. There **SHALL** be no unbounded waiting queue, automatic retry storm or speculative generation triggered by denial.
5. Validated cache hits **MAY** serve public requests without new generation **only within existing cold-generation, cache-publication, provenance and validation contracts**. Cache hits **SHALL** have independent bounded response/rate/concurrency protection. This amendment **DOES NOT** authorize startup pregeneration, exported fixtures, background generation or cache-rule changes.
6. If durable admission/budget state, resource health or recovery evidence is unknown or unavailable, new expensive generation **SHALL** fail closed. Denials **SHALL NOT** launch a Worker.
7. Rate controls **SHALL** protect measured F1 headroom for Streamlit and health responses. Sustained request floods **SHALL** be tested under separately authorized, bounded conditions; no claim is made that application-level limits prevent all ingress CPU or Azure platform quota usage.
8. **Provisional evaluation values only:** one concurrent generation; 10-second session cooldown; three expensive requests/client/minute; three new generation admissions/15 minutes; 15/day; 60 reserved CPU-seconds/15 minutes; 240 reserved CPU-seconds/day; no waiting queue; explicit retries only; fail-closed uncertainty. Values require consistency checks, time-window definitions, multi-instance scope and separate threshold approval. The original 4096-byte input, 2 MiB stdout, 16 KiB stderr, 1-second admission, 10-second operation and 15-second admission-to-reap figures remain provisional pending contract reconciliation.

## 6. Mandatory acceptance gates

| Gate | Required evidence | Blocking failure |
|---|---|---|
| **G-B0 — Contract authority** | Owner approves exact policy changes, residual-risk acceptance, numerical limits, admission domain and conflicts reconciled across controlling documents | No implementation authority |
| **G-B1 — Crash-safe admission** | Durable occupied state before spawn; no overlapping Worker after guardian/supervisor/UI failure, final-holder death, interrupted handshake or restart; explicit Phase 1B regression | Any reopening before independently verified cleanup |
| **G-B2 — Confidentiality** | Target-runtime negative tests of private cache, `/runtime`, credentials, mount/symlink/FD/`/proc`/environment and outbound-provider access | Any public path to private observations or credentials |
| **G-B3 — Resource-risk qualification** | Atomic reservations, rolling-window accounting, overshoot/CPU/memory measurements, deadlines, bounded capture, retry controls, supervisor overhead, no hard-ceiling misrepresentation | Unknown risk or uncontrolled accounting/admission |
| **G-B4 — Lifecycle and recovery** | Ordinary exit, parent/guardian/supervisor death, combined failures, PID namespace behavior, orphan/escape cases, ledger corruption, restart, operator recovery | Unsafe automatic reopening or unverified cleanup |
| **G-B5 — Functional preservation** | Exact synthetic fingerprints, 12 selections, warmup, analytics, Worker transport, private-provider negatives and public response provenance | Any observed-data leakage or contract regression |
| **G-B6 — Azure F1 operational qualification** | Separately authorized target-runtime tests of cold/warm latency, memory, startup, availability, health, restoration, cost/plan preservation and documented failure impact | Unacceptable F1 impact or unverified target behavior |
| **G-B7 — Abuse and quota-starvation protection** | Bounded flood/identity-rotation/concurrent-tab/cache-hit/invalid-input tests; no Worker on denial; durable global budgets across restart; fail-closed corrupt ledger; verified health headroom | Saturation amplification, limit bypass or uncontrolled queue/retries |

**Ordering:** G-B0 precedes any implementation authority. G-B1 and G-B2 are non-negotiable prerequisites to public Worker activation. G-B3, G-B4, G-B5, G-B6 and **G-B7** must also pass before WP07 acceptance and any separately approved public activation. Active verification requires distinct authorization; a planning gate does not authorize probes.

## 7. Residual-risk acceptance matrix

| Risk | Proposed owner treatment |
|---|---|
| Actual CPU use exceeds a conservative reservation | **Conditionally acceptable** with measurement, operating headroom and explicit threshold approval; not called a hard bound |
| Public charts unavailable while occupied state is uncertain | **Acceptable** fail-closed outcome |
| Evidence-based manual recovery after crash | **Conditionally acceptable** with an approved runbook |
| Measured degradation of Streamlit under F1 load | **Conditionally acceptable** within separately approved thresholds |
| Anonymous floods exhausting planned generation admissions | **Must be limited** by global budgets and cheap denials; residual HTTP ingress costs disclosed |
| Private observed-data or credential exposure | **Unacceptable** |
| Public provider calls or observed-data fallback | **Unacceptable** |
| Overlapping generation after uncertain cleanup | **Unacceptable** |
| Silent lock/lease/TTL-based reopening | **Unacceptable** |
| Claiming reservation budgets are enforced Azure quota ceilings | **Unacceptable** |

## 8. Unresolved owner decisions before G-B0

1. **Admission domain:** one container instance, one App Service application, or deployment-wide across replicas/regions? Identify how every public entry point shares authoritative admission/budget state, or explicitly constrain topology and verify it.
2. **Recovery:** accept indefinite generation unavailability when complete cleanup cannot be independently verified? Define operator identity, proof and audit/runbook requirements.
3. **Numerical limits:** approve or revise provisional 16-second reservation, 60/240 rolling budgets, 3/15 admission counts, 10-second cooldown and 3/client/minute, with precise rolling-window/timezone and restart semantics.
4. **Headroom:** define measurable maximum cold/warm latency, memory and CPU impact, health response expectations and the permitted percentage of F1 quota allocated to public generation.
5. **Admission ownership:** specify trusted supervisor startup/lifetime, durable occupied record, storage location/permissions, crash recovery, process identity evidence and no unregistered-child window. Do not presume this mechanism exists today.
6. **Filesystem/network boundary:** define the minimal public Worker filesystem and provider-network denial strategy, and how private caches/`/runtime` remain inaccessible even though the original `aiq` identity could access them.
7. **Failure policy:** define what is acceptable if the host is killed or restarted mid-job, including process lifecycle and durable state recovery without unsafe clearing.
8. **Cache-hit abuse:** specify whether cached chart reads and health endpoints use distinct rate classes and how ingress saturation is handled without adding paid services.

These are open design/owner questions, not silently approved defaults.

## 9. Governance and next authorization sequence

1. Owner reviews this proposal and makes an **explicit security-contract decision** on all unresolved points. Approval of the planning direction and Amendment F inclusion is not automatically final G-B0 approval.
2. If approved, issue a **separate exact literal-path documentation mutation allowlist** and review the resulting Section 10 and Release 1.14 changes; no mutation authority is inferred from this file.
3. Separately authorize read-only supervisor architecture preflight; require a credible admission, crash-recovery and isolation argument before any active test.
4. Separately authorize implementation paths and any dependency/startup/container changes, then bounded local/target-runtime verification with restoration and evidence controls.
5. Complete gates G-B1–G-B7, conduct explicit acceptance review, and obtain separate authorities for PR merge, WP07 closure, deployment and public activation.

**Hold state:** PR #330 stays draft/unmerged; issue #322 stays open; WP05 dependency remains blocked; `PUBLIC_RUNTIME_VERIFIED = False`; no additional probes, implementation, deployment or public invocation authorized by this proposal.

## 10. Owner review checklist (decision record — not yet signed)

- [ ] Accept Option B as the *effective* revised security contract, with explicit CPU risk disclosure.
- [ ] Approve the exact Section 10 amendment and cross-document precedence.
- [ ] Approve Amendment F and G-B7 as mandatory activation requirements (inclusion in proposal already approved).
- [ ] Decide admission domain and topology restrictions.
- [ ] Approve numerical thresholds and Azure F1 headroom criteria.
- [ ] Approve fail-closed/manual recovery policy and evidence requirements.
- [ ] Approve confidential filesystem/network isolation requirements without relaxation.
- [ ] Authorize documentation mutations via separate literal-path allowlist (if desired).

**No signature, implementation approval, test approval, deployment approval, PR merge approval, or activation approval is implied.**

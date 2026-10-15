# WP07 — Consolidated Option B Planning and Qualification

**Project:** AIQuantTradingResearch · **Release:**1.14 · **Issue:**#322 · **Draft PR:**#330 · **Milestone:**65  
**Assembled:**2026-10-10 (America/Sao_Paulo)  
**Status:**Owner-approved planning dispositions; effective security-contract amendment and G-B0 completion pending.

## Purpose and current authority

This artifact consolidates the planning topics and their full contents for repository review. The owner explicitly authorized assembly into a WP07 Markdown file in this directory. This authorization is applied to this new artifact only; no other repository documents or implementation paths are changed. No commit, PR update/merge, issue closure, active probe, deployment or public activation is performed.

External supervision remains the preferred contract-preserving candidate. The owner approved the recommendations in the G-B0 decision record, including a single qualified public instance, durable occupied state, evidence-based recovery and reservation budgets. Those planning approvals do not silently amend controlling Section10 or qualify an implementation. Active containment development remains stopped pending an effective security-contract decision and separately scoped authority.

Controlling policy: [Section10](../../../architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md). Reconciliation of that policy and the Release1.14 definition, execution plan and file manifest requires separately specified literal-path documentation authority. Unresolved F1 headroom, measured overshoot, cache-hit limits and health thresholds prevent declaring G-B0 complete.

## Reading guide and status precedence

1. The security-contract proposal preserves confidentiality, synthetic provenance, global admission and cleanup guarantees while proposing explicitly accepted resource/availability risk in place of hard CPU ceilings.
2. The decision record gives concrete recommendations approved in conversation. Its original “UNAPPROVED” heading is retained as historical source content; approval concerns the recommendations, not unresolved thresholds or implementation authority.
3. The architecture comparison predates the selected Option B direction; its alternative discussion is evidence and tradeoff analysis, not a renewed selection request.
4. The Phase1B report records the admission-lock failure and successful restoration. Its image-deletion deadline is historical: the exact Phase1B image cleanup is now complete, as recorded below. Earlier diagnostic image obligations are not represented as cleared.
5. The measurement plan identifies the remaining owner decisions, prerequisite authority and sequential verification. No proposed test was run by consolidation.

The current status in this introduction takes precedence over historical status text in reproduced source sections. Their contents are preserved for traceability, without implying that proposed policy language is effective or that untested capabilities passed.

## Current hold and evidence summary

- `PUBLIC_RUNTIME_VERIFIED = False`; public invocation remains disabled.
- PR#330 remains draft/unmerged and issue#322 remains open; dependent WP05 activation stays blocked.
- Writable cgroup delegation was unavailable on the tested Azure F1 instance; universal F1 capability claims are excluded.
- Phase1A observed one corrected PID1-death descendant cleanup. Phase1B showed admission reopening approximately1.674915ms after guardian SIGKILL before registered pidfds signalled exit. Runnable CPU in that gap was not measured.
- Durable occupied state is required to defeat final-holder death; it does not itself terminate processes or bound CPU.
- Filesystem/network isolation, remaining lifecycle cases and resource-risk qualification remain outstanding.
- The Phase1B registry version1367169458, tag `wp07-lock-d43703d7`, was deleted and independently returned404. Its local image tag was removed; production digest and both Azure image references were preserved.

## Consolidated source contents

The following sections reproduce the full planning/evidence texts with heading levels adjusted for this document. Local temporary/attachment paths identify assembly inputs; the contents needed for review are embedded here, so those paths need not remain available.

## 1. Option B security-contract amendment proposal

Source SHA-256: `e4aa03bde022acf05ecf9b38554be9bffdc7957f29b95c6ecdddb9ebfa2255ee`

### WP07 — Consolidated Option B Security-Contract Amendment Proposal

**Project:** AIQuantTradingResearch
**Release:** 1.14 — Technical Analysis & Quantitative Feature Foundation
**Work package:** WP07 — Public Runtime Isolation / Private Preservation
**Related:** Issue #322; draft PR #330; milestone #65
**Status:** **PROPOSAL — OWNER REVIEW REQUIRED; NOT AN EFFECTIVE POLICY AMENDMENT**
**Planning direction:** Owner selected Option B (external supervision with explicitly bounded availability risk), and approved inclusion of anonymous-abuse / F1-quota protections in this proposal.
**Authority:** Read-only architecture planning and preparation of this review artifact only. No repository mutations, implementation, active probes, deployments, merges, WP07 acceptance, or public activation.

#### 1. Purpose and controlling authority

This proposal consolidates the Option B security-contract changes required before WP07 implementation can resume. The controlling policy remains **Section 10** of `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` until an explicit owner-approved amendment is applied under a separate, literal-path documentation mutation authority.

Documents potentially requiring subsequent reconciliation (not authorized for mutation here):

- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` — Section 10, controlling policy.
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` — requirements, limits, acceptance criteria.
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md` — WP07/WP08 dependencies and gates.
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` — any later exact-path authorization alignment.

This document does **not** itself amend any repository file or supersede Section 10. Exact section/line edits must be independently reviewed against the then-current repository version.

#### 2. Evidence and constraints

1. The tested West Central US Azure Linux F1 application container exposed cgroup v2 and the CPU controller, but its cgroup filesystem was read-only, including for root; no writable delegated public cgroup was available. This is **instance-specific evidence**, not a claim about every F1 host.
2. Unprivileged user/mount namespaces and masking `/home` succeeded in the tested instance; the existing `aiq` identity could access the private market cache and `/runtime`. A complete confidentiality boundary was **not** demonstrated.
3. Phase 1A observed PID-namespace-init death terminating TERM-resistant descendants in one corrected test; universal lifecycle/escape guarantees were not established.
4. Phase 1B **failed** the guardian-only admission model: after guardian SIGKILL, the admission lock was acquired about **1.674915 ms** later, before registered guardian, Worker and grandchild pidfds signalled exit. No second Worker was launched; continued runnable CPU use during the gap was not measured. A guardian-held flock is therefore **not** sufficient admission fencing.
5. Filesystem/network isolation, remaining lifecycle scenarios and hard cumulative CPU enforcement were **not** qualified. Phase 2/3 active tests were stopped.
6. The Phase 1B temporary GHCR image version `1367169458` was reported deleted and registry absence, preserved production digest and unchanged Azure image references verified by the owner. The original application image had been restored and Streamlit health verified.
7. Draft PR #330 is partial fail-closed implementation with `PUBLIC_RUNTIME_VERIFIED = False`; it does not establish WP07 completion. Issue #322 remains open.

#### 3. Security-contract decision and threat model

**Selected planning direction:** Option B — retain the approved C# synthetic generator, `aiq-ta-v1` analytics, canonical contracts and bounded Worker byte transport under an externally supervised execution model. Accept a **documented, measured residual availability/resource risk** in place of hard per-request and rolling CPU-consumption enforcement. Do **not** relax confidentiality, synthetic-only provenance, global admission correctness or fail-closed cleanup.

Protected assets: private Twelve Data/Vike observations, provider secrets, private market caches and `/runtime` handoffs; public application availability; finite F1 CPU/memory/quota; deterministic synthetic data integrity and provenance.

Threats to address: anonymous floods and client-identity rotation; simultaneous tabs/sessions; costly cold-generation amplification; malicious or malformed inputs; Worker/guardian/supervisor/UI death; interrupted handshake; stale or corrupt ledger; orphan processes; private filesystem, descriptor, environment, `/proc`, mount or network access; quota starvation by repeated cache misses **and** excessive cache hits.

An anonymous IP, cookie or Streamlit session is not a trustworthy unique user identity. Per-client limits are supplementary; **global limits are mandatory**. No protection implemented only inside Streamlit can eliminate all inbound HTTP/platform overhead or guarantee Azure-metered quota preservation.

#### 4. Requirement-by-requirement disposition


| Contract requirement                                                                                             | Proposed disposition                             | Owner-review consequence                                                  |
| ------------------------------------------------------------------------------------------------------------------ | -------------------------------------------------- | --------------------------------------------------------------------------- |
| Anonymous deterministic BTC-like synthetic OHLCV only; exact public labeling                                     | **Preserve**                                     | No public observed candles or provider fallback                           |
| Existing C# synthetic generator, canonical fingerprints,`aiq-ta-v1`, 12 selections and 250 hidden warmup candles | **Preserve**                                     | No Python production port or parallel algorithm                           |
| Canonical v2 response/provenance, bounded raw UTF-8 Worker transport                                             | **Preserve**                                     | Changes require separate explicit contract review                         |
| No anonymous Twelve Data/Vike calls, observations or secrets                                                     | **Preserve — mandatory**                        | Fail confidentiality gate if unverified                                   |
| Private filesystem, environment, inherited descriptor, process and network boundary                              | **Preserve — mandatory**                        | Namespace creation alone is insufficient                                  |
| Globally exclusive public generation admission and cleanup verification                                          | **Preserve — mandatory**                        | Phase 1B regression must be defeated                                      |
| Guardian-only lock ownership as sufficient admission proof                                                       | **Reject / supersede design assumption**         | Durable occupied state must survive guardian/supervisor failure           |
| Hard per-request CPU-seconds enforcement                                                                         | **Revise**                                       | Cooperative cancellation and deadlines are not hard CPU enforcement       |
| Hard 60 CPU-seconds/rolling 15 min and 240/day consumption ceilings                                              | **Revise**                                       | Propose*admission-reservation budgets*, not enforced consumption ceilings |
| Conservative per-job reservation, timeout, bounded capture and retry                                             | **Preserve as provisional controls; revalidate** | Reservation is not proven upper bound on actual CPU                       |
| F1 free tier, no privileged container, no added paid services                                                    | **Preserve**                                     | Target-runtime evidence required                                          |
| Anonymous request abuse and quota starvation controls                                                            | **Add — mandatory (Amendment F)**               | New gate G-B7 before activation                                           |
| Public invocation disabled until full acceptance and separate activation                                         | **Preserve — mandatory**                        | `PUBLIC_RUNTIME_VERIFIED = False` remains                                 |

#### 5. Proposed normative amendments (candidate policy language)

##### A — Execution and synthetic-only provenance

1. Anonymous historical-market requests **SHALL** use only the independently deterministic approved BTC-like synthetic OHLCV implementation and approved analytics. The C# generator, shared indicator calculator, canonical dataset/version/seed/fingerprints, 12 selections, 250 hidden warmup candles and visible-only projections **SHALL** remain authoritative.
2. The public execution path **SHALL NOT** construct or call private Twelve Data/Vike adapters, consume observed-data caches, perform provider fallback or access private handoffs.
3. The existing bounded Worker byte protocol **SHALL** be preserved unless separately amended. Public requests **SHALL** be strictly validated before costly work.

##### B — Supervisor, admission and crash recovery

1. A trusted admission authority **SHALL** durably establish an **occupied** state and conservative reservation **before any Worker can exist**. All admission contenders in the explicitly defined admission domain **SHALL** use the same enforcement path.
2. At most **one** public synthetic generation job **SHALL** be admitted within that domain. An unregistered-Worker creation window is unacceptable; the startup protocol **SHALL** prevent Worker creation before auditable registration and verified START.
3. Guardian death, supervisor death, final flock-holder death, UI death, expired lease/deadline, numeric PID reuse or process-group kill **SHALL NOT** alone clear occupied state.
4. New admission **SHALL** require independently verified complete termination/cleanup of the prior registered execution boundary and durable settlement. Missing, corrupt, ambiguous, inconsistent or inaccessible evidence **SHALL** fail closed, including after restart.
5. Boot identity, process start identity and available pidfd evidence **SHALL** be treated according to their actual persistence limitations. PIDfds cannot simply be reconstructed as equivalent evidence after arbitrary owner death. Time passage alone **SHALL NOT** prove cleanup.
6. Recovery **MAY** require documented operator intervention, but **SHALL NOT** clear occupation without adequate independent evidence. A durable occupied record prevents overlap; by itself it does **not** kill processes or bound CPU.
7. The architecture **SHALL** define its admission domain (single container vs multiple instances/replicas), its shared-state durability, and the behavior when those assumptions are violated. A local lock **SHALL NOT** be called deployment-global without proof.

##### C — Revised CPU and memory resource contract

1. The existing rolling **60 CPU-seconds/15 minutes** and **240 CPU-seconds/day** values are proposed as **provisional conservative admission-reservation budgets**, not hard actual CPU-consumption ceilings. The **16 CPU-second/job** reservation and proposed **3 admissions/15 minutes, 15/day** are also provisional pending arithmetic, policy and F1 capacity validation.
2. Admission accounting **SHALL** be atomic, durable and fail closed on inconsistent state, clock rollback, missing evidence or incomplete settlement. Conservative full-reservation charging **SHALL** apply when exact use is unknown, without claiming that reservation bounds actual CPU.
3. The architecture **SHALL** bound request/response sizes, concurrency, retries, in-flight allocations and operation deadlines; **SHALL** document and measure possible overshoot, supervisor overhead and cleanup latency; and **SHALL NOT** claim that `wait4`, `rusage`, cooperative cancellation, wall-clock deadlines, `DOTNET_PROCESSOR_COUNT=1` or a reservation ledger independently enforce hard CPU ceilings.
4. The approved residual risk **MAY** include CPU use beyond a reservation, temporary public-chart unavailability, manual recovery and measured Streamlit degradation. It **SHALL NOT** include private-data exposure, provider calls, unverified overlapping jobs or silent reopening after crash.
5. The public workload **SHALL** reserve measurable operating headroom for Streamlit health, ordinary chart viewing and platform quota. Exact CPU/memory/performance thresholds require a separate owner decision informed by measured F1 behavior; internal budget values are not Azure's platform-metered quota.

##### D — Confidentiality and isolation (not relaxed)

1. Public Worker execution **SHALL** have no effective access to private provider credentials, market caches, research artifacts, private `/runtime` handoffs or other private observations through mounts, inherited descriptors, environment, symlinks, `/proc` or alternative paths.
2. Public execution **SHALL** be denied outbound provider access. Network-namespace or equivalent isolation is a candidate to be separately verified; no mechanism is presumed sufficient without target-runtime evidence.
3. Public/private startup separation **SHALL** prevent inherited private configuration or handles. A separate process with the same effective private permissions **SHALL NOT** count as an isolation boundary.
4. No private file contents or credentials **SHALL** be collected in verification artifacts.

##### E — Availability, failure and operations

1. Unknown cleanup or admission state **SHALL** deny new public generation requests. Failure responses **SHALL** be bounded, non-retrying by default and free of sensitive details.
2. The application **MAY** prefer temporary generation unavailability over unsafe readmission. Operator recovery **SHALL** be documented, observable, auditable and evidence-based.
3. Public activation **SHALL** require separate authorization after every applicable gate passes. Draft/merged code or an accepted planning amendment **SHALL NOT** imply activation authority.

##### F — Anonymous request abuse prevention and Azure F1 quota protection (**owner-approved inclusion in proposal**)

1. The public application **SHALL** reject invalid, oversized or disallowed requests using cheap bounded validation **before** expensive generation or cache work.
2. It **SHALL** enforce both (a) per-client/session throttling as defense in depth and (b) **durable global admission frequency and quota-reservation limits** that cannot be bypassed merely by changing tabs, sessions, IP addresses or query parameters.
3. Anonymous client identifiers **SHALL NOT** be treated as verified identities; IP-based controls **SHALL** account for shared addresses and proxy/header trust. Client identity rotation **SHALL NOT** bypass global controls.
4. At most one generation job **SHALL** be admitted in the approved domain. There **SHALL** be no unbounded waiting queue, automatic retry storm or speculative generation triggered by denial.
5. Validated cache hits **MAY** serve public requests without new generation **only within existing cold-generation, cache-publication, provenance and validation contracts**. Cache hits **SHALL** have independent bounded response/rate/concurrency protection. This amendment **DOES NOT** authorize startup pregeneration, exported fixtures, background generation or cache-rule changes.
6. If durable admission/budget state, resource health or recovery evidence is unknown or unavailable, new expensive generation **SHALL** fail closed. Denials **SHALL NOT** launch a Worker.
7. Rate controls **SHALL** protect measured F1 headroom for Streamlit and health responses. Sustained request floods **SHALL** be tested under separately authorized, bounded conditions; no claim is made that application-level limits prevent all ingress CPU or Azure platform quota usage.
8. **Provisional evaluation values only:** one concurrent generation; 10-second session cooldown; three expensive requests/client/minute; three new generation admissions/15 minutes; 15/day; 60 reserved CPU-seconds/15 minutes; 240 reserved CPU-seconds/day; no waiting queue; explicit retries only; fail-closed uncertainty. Values require consistency checks, time-window definitions, multi-instance scope and separate threshold approval. The original 4096-byte input, 2 MiB stdout, 16 KiB stderr, 1-second admission, 10-second operation and 15-second admission-to-reap figures remain provisional pending contract reconciliation.

#### 6. Mandatory acceptance gates


| Gate                                              | Required evidence                                                                                                                                                                              | Blocking failure                                                     |
| --------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------------------------------------------------------- |
| **G-B0 — Contract authority**                    | Owner approves exact policy changes, residual-risk acceptance, numerical limits, admission domain and conflicts reconciled across controlling documents                                        | No implementation authority                                          |
| **G-B1 — Crash-safe admission**                  | Durable occupied state before spawn; no overlapping Worker after guardian/supervisor/UI failure, final-holder death, interrupted handshake or restart; explicit Phase 1B regression            | Any reopening before independently verified cleanup                  |
| **G-B2 — Confidentiality**                       | Target-runtime negative tests of private cache,`/runtime`, credentials, mount/symlink/FD/`/proc`/environment and outbound-provider access                                                      | Any public path to private observations or credentials               |
| **G-B3 — Resource-risk qualification**           | Atomic reservations, rolling-window accounting, overshoot/CPU/memory measurements, deadlines, bounded capture, retry controls, supervisor overhead, no hard-ceiling misrepresentation          | Unknown risk or uncontrolled accounting/admission                    |
| **G-B4 — Lifecycle and recovery**                | Ordinary exit, parent/guardian/supervisor death, combined failures, PID namespace behavior, orphan/escape cases, ledger corruption, restart, operator recovery                                 | Unsafe automatic reopening or unverified cleanup                     |
| **G-B5 — Functional preservation**               | Exact synthetic fingerprints, 12 selections, warmup, analytics, Worker transport, private-provider negatives and public response provenance                                                    | Any observed-data leakage or contract regression                     |
| **G-B6 — Azure F1 operational qualification**    | Separately authorized target-runtime tests of cold/warm latency, memory, startup, availability, health, restoration, cost/plan preservation and documented failure impact                      | Unacceptable F1 impact or unverified target behavior                 |
| **G-B7 — Abuse and quota-starvation protection** | Bounded flood/identity-rotation/concurrent-tab/cache-hit/invalid-input tests; no Worker on denial; durable global budgets across restart; fail-closed corrupt ledger; verified health headroom | Saturation amplification, limit bypass or uncontrolled queue/retries |

**Ordering:** G-B0 precedes any implementation authority. G-B1 and G-B2 are non-negotiable prerequisites to public Worker activation. G-B3, G-B4, G-B5, G-B6 and **G-B7** must also pass before WP07 acceptance and any separately approved public activation. Active verification requires distinct authorization; a planning gate does not authorize probes.

#### 7. Residual-risk acceptance matrix


| Risk                                                           | Proposed owner treatment                                                                                                   |
| ---------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| Actual CPU use exceeds a conservative reservation              | **Conditionally acceptable** with measurement, operating headroom and explicit threshold approval; not called a hard bound |
| Public charts unavailable while occupied state is uncertain    | **Acceptable** fail-closed outcome                                                                                         |
| Evidence-based manual recovery after crash                     | **Conditionally acceptable** with an approved runbook                                                                      |
| Measured degradation of Streamlit under F1 load                | **Conditionally acceptable** within separately approved thresholds                                                         |
| Anonymous floods exhausting planned generation admissions      | **Must be limited** by global budgets and cheap denials; residual HTTP ingress costs disclosed                             |
| Private observed-data or credential exposure                   | **Unacceptable**                                                                                                           |
| Public provider calls or observed-data fallback                | **Unacceptable**                                                                                                           |
| Overlapping generation after uncertain cleanup                 | **Unacceptable**                                                                                                           |
| Silent lock/lease/TTL-based reopening                          | **Unacceptable**                                                                                                           |
| Claiming reservation budgets are enforced Azure quota ceilings | **Unacceptable**                                                                                                           |

#### 8. Unresolved owner decisions before G-B0

1. **Admission domain:** one container instance, one App Service application, or deployment-wide across replicas/regions? Identify how every public entry point shares authoritative admission/budget state, or explicitly constrain topology and verify it.
2. **Recovery:** accept indefinite generation unavailability when complete cleanup cannot be independently verified? Define operator identity, proof and audit/runbook requirements.
3. **Numerical limits:** approve or revise provisional 16-second reservation, 60/240 rolling budgets, 3/15 admission counts, 10-second cooldown and 3/client/minute, with precise rolling-window/timezone and restart semantics.
4. **Headroom:** define measurable maximum cold/warm latency, memory and CPU impact, health response expectations and the permitted percentage of F1 quota allocated to public generation.
5. **Admission ownership:** specify trusted supervisor startup/lifetime, durable occupied record, storage location/permissions, crash recovery, process identity evidence and no unregistered-child window. Do not presume this mechanism exists today.
6. **Filesystem/network boundary:** define the minimal public Worker filesystem and provider-network denial strategy, and how private caches/`/runtime` remain inaccessible even though the original `aiq` identity could access them.
7. **Failure policy:** define what is acceptable if the host is killed or restarted mid-job, including process lifecycle and durable state recovery without unsafe clearing.
8. **Cache-hit abuse:** specify whether cached chart reads and health endpoints use distinct rate classes and how ingress saturation is handled without adding paid services.

These are open design/owner questions, not silently approved defaults.

#### 9. Governance and next authorization sequence

1. Owner reviews this proposal and makes an **explicit security-contract decision** on all unresolved points. Approval of the planning direction and Amendment F inclusion is not automatically final G-B0 approval.
2. If approved, issue a **separate exact literal-path documentation mutation allowlist** and review the resulting Section 10 and Release 1.14 changes; no mutation authority is inferred from this file.
3. Separately authorize read-only supervisor architecture preflight; require a credible admission, crash-recovery and isolation argument before any active test.
4. Separately authorize implementation paths and any dependency/startup/container changes, then bounded local/target-runtime verification with restoration and evidence controls.
5. Complete gates G-B1–G-B7, conduct explicit acceptance review, and obtain separate authorities for PR merge, WP07 closure, deployment and public activation.

**Hold state:** PR #330 stays draft/unmerged; issue #322 stays open; WP05 dependency remains blocked; `PUBLIC_RUNTIME_VERIFIED = False`; no additional probes, implementation, deployment or public invocation authorized by this proposal.

#### 10. Owner review checklist (decision record — not yet signed)

- [ ]  Accept Option B as the *effective* revised security contract, with explicit CPU risk disclosure.
- [ ]  Approve the exact Section 10 amendment and cross-document precedence.
- [ ]  Approve Amendment F and G-B7 as mandatory activation requirements (inclusion in proposal already approved).
- [ ]  Decide admission domain and topology restrictions.
- [ ]  Approve numerical thresholds and Azure F1 headroom criteria.
- [ ]  Approve fail-closed/manual recovery policy and evidence requirements.
- [ ]  Approve confidential filesystem/network isolation requirements without relaxation.
- [ ]  Authorize documentation mutations via separate literal-path allowlist (if desired).

**No signature, implementation approval, test approval, deployment approval, PR merge approval, or activation approval is implied.**

## 2. Owner decision recommendations and approval record

Source SHA-256: `e80b23575ba334cbf5c444be5b6e750175871ec1b522bedaa0284b118f96a761`

### WP07 Option B — proposed G-B0 owner decision record

UNAPPROVED recommendations. Read-only planning; no effective policy amendment or implementation/probe authority. Section10 remains controlling. Owner proposal08cbbaf8-db19-4246-9d25-b420070e2ea6 is the input. Active containment development remains stopped.

| Decision | Recommended owner disposition | Evidence still needed |
|---|---|---|
| Security contract | Keep external supervision/C# Worker. Replace claims of hard CPU consumption ceilings with explicitly named reservation budgets and measured residual availability risk. Preserve confidentiality and no overlap as mandatory. | Exact Section10 replacement and reconciled release documents |
| Admission domain | One explicitly qualified public application-container instance, across all sessions/processes. Restrict topology to one enabled public instance; other region remains disabled. Do not claim deployment-global guarantees. Any replica/placement assumption change disables generation until reviewed. | Credible enforcement of topology, identity and shared-state assumptions; local verification alone cannot prove no other serving instance |
| Recovery | Accept indefinite generation unavailability when cleanup/settlement is uncertain. Only an authenticated operator with independent cleanup proof and an audited procedure can recover. No TTL/boot/PID-number reset. | Runbook/proof format, operator authorization and restart/simultaneous-failure tests |
| Budget timing | Rolling900seconds and rolling86400seconds, using UTC event timestamps with explicit rollback detection; retain records over restart. Monotonic deadlines within one lifetime. Missing/corrupt/unverifiable history denies generation. | Durable clock/high-water/restart rules and atomic cross-process accounting |
| Numerical evaluation |16 reserved CPU-seconds/admission, at most3 admissions/900seconds and15/86400seconds, and60/240 reservation caps. All successful starts and uncertain launches count; unknown usage charges full reservation. No actual CPU upper-bound claim. | Owner approves arithmetic/policy and later capacity measurements |
| Client defense |10second session cooldown and3 expensive attempts/client/rolling60seconds as supplementary controls, applied alongside global rules. Specify retry/denial counting to avoid alternative entry-point bypass. No trusted identity assumption. | Cheap bounded identifier handling, proxy trust and bounded state retention; rotation/flood negatives |
| Queue and deadlines |No application waiting queue; bounded lock acquisition up to1second. Operation10seconds,total15seconds,TERM grace at most1second, then kill/reap. Deadline miss keeps occupation blocked until cleanup proof. | Scheduling/cleanup overshoot measurements; these figures are controls/targets, not universal kill guarantees |
| Supervisor ownership |Trusted supervisor outside UI and Worker namespace; occupied record/reservation committed before any Worker can exist, registration and READY before START. No sole guardian flock proof. | Read-only architecture preflight must establish storage durability, spawn-gap safety, final-holder death recovery and all admission paths |
| Confidentiality |Minimal public mount view, sanitized environment and descriptor allowlist, private-root/proc/alternate-path denial, provider-network denial. Fail closed if any boundary is unverified. | Synthetic-only target-runtime negatives; no secret/private contents in evidence |
| Cache-hit abuse |Separate bounded chart-read and health rate/concurrency classes; generation ledger never controls all ingress costs. Preserve existing cache/publication semantics. | Specific rate thresholds, bounded validation/read cost and ingress saturation behavior |
| Headroom |Carry forward coldp95<=5seconds,warmp95<=2seconds,childRSS<=160MiB,totalworking-set<=640MiB as evaluation targets. No claim they are measured or adequate. | Owner must choose acceptable health latency/error rate, generation share of F1 quota, measured CPU overshoot and recovery impact before qualification |

Arithmetic:3*16=48 reserved seconds/900seconds, leaving12 unusable seconds under the16second unit;15*16=240/86400seconds. Admission counts and reservation caps must both hold; reservation amounts are policy charges, not evidence of real CPU consumed. “15/day” here means rolling24hours, not calendar midnight reset. Rejected requests consume validation/ingress CPU even without a Worker. A cold2CPU-second target remains a performance target rather than a hard kernel ceiling.

Unresolved thresholds are deliberately not fabricated: cache-hit/global HTTP rate, chart/health concurrent limits, health latency/error targets, permitted F1 quota percentage and maximum acceptable measured overshoot. If these need measurements first, owner should authorize only a read-only measurement-plan preflight, then separately bounded diagnostics; do not declare G-B0 complete while claiming final numerical acceptance without evidence. Distinguish approval of a candidate investigation from final security-contract effectiveness.

Crash state requirements: durable prepared/occupied -> registered/START -> running -> cleanup verified -> settled -> cleared. Never clear solely because a lock became available. Intermediate states and partial writes deny generation. Supervisor death leaves occupation blocking; successor requires independently credible boundary identity/evidence. The state record alone neither kills processes nor protects private data. Exact storage location and recovery mechanism remain architecture decisions, not granted implementations.

Suggested next owner decision: approve/revise each recommended disposition above and explicitly resolve headroom/cache-hit thresholds or authorize their separate planning. Then provide an exact literal-path documentation mutation authority for Section10 and the release definition/execution plan/manifest. Review exact document changes before granting separate supervisor preflight/implementation authority. Approval of this recommendation record alone grants no repository editing, probe, deployment or activation.

Hold:PUBLIC_RUNTIME_VERIFIED=False; PR330 draft/unmerged; issue322 open; WP05 dependent activation blocked. No repository/GitHub/Azure changes made by this planning step. Working tree includes two pre-existing untracked prompters (feasibility authority and consolidated OptionB proposal); both preserved.

## 3. Read-only architecture comparison

Source SHA-256: `27b3a5c71414c67ec58923f48117a321ba42dc82951a0f597ab1e0be331d990e`

### WP07 read-only architecture comparison

Scope: source/document inspection and official technical documentation only. No generator execution, benchmark, Azure probe, implementation, dependency installation, repository/PR/issue changes or public activation. Baseline67893f51931ee183f71050a83b81127e6e648b87. Public gate remains false. This comparison is separate from the expressly authorized temporary-package deletion and authentication flow.

#### Evidence and decision

Phase1A proved one observed kernel namespace-init death cleanup. Phase1B disproved sole-guardian admission ownership: lock acquired1.674915ms after SIGKILL before guardian/Worker/grandchild pidfds signalled exit. Known fixtures explicitly cleaned afterward. No runnable-time or CPU attribution measurement was made. Azure F1 writable cgroup delegation is absent. Filesystem/network boundary and complete lifecycle/CPU safety remain unproven.

Recommendation under unchanged contracts: retain disabled public invocation. External supervision best preserves the merged C# implementations and frozen Worker transport, but is not yet qualified on F1. In-process generation is a plausible owner-reviewed architectural alternative for trusted bounded computation; it does not satisfy the current process containment contract by moving work into the UI. It requires explicit contract changes and a separate implementation allowlist before investigation becomes implementation.

#### Option comparison

| Concern | Crash-safe external supervision | In-process deterministic generation |
|---|---|---|
| Execution placement | A trusted supervisor outside Streamlit and outside the Worker PID namespace, potentially in the existing application container | Generator/analytics run inside the presentation host process; a thread or embedded runtime is still in-process |
| Existing code reuse | Reuses approved C# provider, shared calculator, read service, cache and Worker byte protocol | Streamlit is Python; C# cannot simply be called without new embedding/native interface or hosting redesign. A Python port creates another implementation |
| UI death | Supervisor must detect disposable parent death/EOF, kill namespace-init, audit complete exit/reap and settle before readmission | Actual process death terminates its own threads, provided no child/native helper is launched. UI session disconnect/rerun or Future timeout does not kill computation |
| Supervisor/owner death | Raw final-holder flock is insufficient. Durable occupied fencing must survive death and prevent next admission until verified recovery | A single process dying leaves no own threads; remaining multi-process hosts still need global admission and crash-safe shared state |
| Blast radius | Can terminate an isolated request without normally terminating UI | Runaway native call, deadlock, memory exhaustion or process crash affects presentation; hard kill means losing UI/all sessions |
| CPU enforcement | Need accounting for all descendants and supervisor overhead plus independently enforceable limits; delegation absent here | Work shares process CPU, GC/native activity and session threads; cooperative checks and deterministic operation counts are not a hard CPU-seconds ceiling |
| Filesystem/credentials | Separate sanitized environment/FD/mount/network view can form a boundary if verified | Same process has same credentials, mounts, environment and descriptors; module/DI separation alone provides no OS boundary |
| Bounds | Current4096B byte input,2MiB capture,16KiB stderr,8KiB chunks,1s admission,10s operation,15s cleanup need full proof | Must replace stdio/capture/reap clauses with bounded input/object/serialization and synchronous compute semantics, retaining byte limits where requests remain serialized |
| Memory/performance | Child startup plus separate .NET/runtime160MiB target; container640MiB target, F1 profiling unperformed | Removes process startup only if no bridge child is used; adds generator/analytics objects to presentation memory, no independent child RSS boundary; performance gains unmeasured |
| Policy alignment | Current architecture most compatible; guardian persistence/paths/startup and recovery still need authority | Explicit current rule forbids Python generator and parallel analytics pipeline; child-specific budgets/stdio transport must be amended |

#### External supervision: minimum credible design and proof obligations

1. Define external as outside both the UI process and request namespace. No proposal assumes a new Azure service, paid resource, privileged host agent or writable cgroup. An in-container supervisor is a new startup/lifecycle role needing owner authority; platform orchestration is not an established substitute.
2. Persist an atomic occupied generation/owner record and reserve CPU before allowing a child to exist. All admission contenders use the same trusted entry point. Supervisor flock loss does not authorize launching another request: durable occupied state remains blocking. Avoid TTL-based reopening. An unregistered-child creation window must itself fail closed; namespace ownership must be durably recorded/audited before START.
3. Guardian READY must precede Worker creation; supervisor START follows identity verification. Bound setup, EOF monitoring, ordinary completion, TERM/kill and outer audit. Cover UI death, guardian death, supervisor death, multiple failures, interrupted writes/handshake, process escape and restart. Stable identities include boot plus start ticks/pidfds where available; numeric PID alone is insufficient.
4. Recovery under a serialized lock reads the occupied record and independently verifies the complete registered kill boundary is gone; only then finalizes a conservative settlement and clears occupation. Live/unknown members, unregistered spawn possibilities, stale boot identity, corruption or inaccessible evidence keep admission disabled. PIDfds do not survive an arbitrary new process unless a still-live owner transfers them; reopening a numeric PID is not equivalent. Boot change or elapsed lease alone is not cleanup proof. Availability may require explicit owner recovery.
5. Separate global admission and budget state from per-selection cache locks. Settlement failures keep the request occupied. Reconciliation must avoid silent deletion of debt, clock-rollback resets and cross-process races. Charge the full conservative reservation when exact usage is unknown, but first prove it upper-bounds all permitted CPU consumption. Runtime threads mean DOTNET_PROCESSOR_COUNT=1 does not establish total single-threaded execution.
6. No claim that this design enforces hard CPU budgets. Kernel cgroup cpu.stat is accounting; cpu.max is bandwidth, not by itself a rolling15minute/day budget. Poll/kill has delay and needs a proven overshoot bound. Without delegation or another proven mechanism, wait4/rusage, wall deadlines and reservations do not solve arbitrary crash-safe process-tree CPU ceilings.
7. Required tests before acceptance: all remaining Phase1 death/EOF/exit/handshake/timeout/nested/reparenting cases; final lock-holder and simultaneous failures; durable recovery and orphan registration gaps; synthetic-only filesystem/FD/proc/symlink/environment/network negatives; live/adopted/reaped descendant CPU reconciliation; integration negative tests and WP08 parent-plus-child performance. No new active test authorized by this comparison.

#### In-process generation: three concrete interpretations

A. Embedded C# provider and calculator in Streamlit via a new bridge/runtime-host interface: may preserve algorithms, but no such embedding is declared in requirements.txt. Adds integration, lifetime/native-runtime/cancellation/resource risks and dependency/container/path amendments. A .NET process invoked from Python is external execution, not this option.

B. Native production Python port: technically possible using arbitrary-precision integers and independently verified canonical bytes, but expressly excluded by Release1.14 definition line190. The existing CPython reference embedded in a C# test is an independent oracle, not an approved production generator. Port both generator and aiq-ta-v1 or change where shared calculation is hosted; preserve exact rounding/nullable alignment, all250warmup, all12window fingerprints and visible projection. This is a new implementation requiring cross-runtime qualification, not a transport-only refactor.

C. Move presentation execution into a .NET host so approved generator/calculator are native calls: avoids algorithm port but is a substantial presentation/hosting redesign. It exceeds current WP07 scope and is not selected or authorized here.

Under any option, generate the complete8160/2040/340 dataset only on a cold request; preserve frozen seed/version/scales/fingerprints, all12selections,250hidden warmup and bounded cache/atomic replacement. No startup pregeneration, immutable exported fixture, background generator or lazy-window optimization is implicitly allowed. Cache-hit validation, publication/provenance checks and visible-only analytics still apply.

Current C# generator executes synchronously and checks cancellation/deadline between bounded loop batches; shared calculator checks its token across loops. These are useful cooperative bounds, not independently enforced wall/CPU limits. Future.result(timeout) only stops waiting; cancelling a running future does not terminate it. Never release the shared admission permit while a timed-out computation continues. Keep permit until actual completion, or fail the host closed; then prove recovery across processes. Native hashing, serialization, allocation/GC and embedded runtime behavior need separate audit.

Resource accounting must be rewritten honestly: generation CPU moves into the previously additional parent budget; child CPU quotas cannot become vacuously satisfied by having zero children. Specify per-instance generation plus presentation overhead, multi-process admission, cancellation semantics, request/session limits, memory and failure blast radius. Hard per-request CPU termination is unavailable without killing the host or an external isolation mechanism; an owner may deliberately choose a trusted bounded-algorithm/cooperative-cancellation contract instead, but must explicitly amend the hard-containment requirement.

A secret-free public host must still be unable to mount/read private/provider data and must have no inherited sensitive descriptors/environment or reachable private fallback. In-process execution does not create that boundary. No actual private data was opened during this comparison.

#### Explicit decisions needed before implementation

Choose unchanged isolated-Worker requirements (remain blocked pending a proven supervisor/CPU boundary) or a revised trusted in-process model (explicitly accept shared process failure and cooperative bounds). Do not conflate these guarantees. Freeze generation host/language, shared-calculator placement, no-child assumptions, admission/recovery owner, per-instance CPU meaning, filesystem/network boundary, replacement transport/serialization rules and qualification gate. Then approve exact changed paths and any dependencies/container/startup amendments. Current comparison grants no implementation/deployment authority.

#### Sources inspected

Local Release1.14 definition lines106,190,198,341–375; WP07 preflight; generator GetHistoricalCandlesAsync/GenerationBudget/GenerateCompleteDataset; shared TechnicalIndicatorCalculator; Worker HistoricalMarketDataQueryExecution; public DI; historical_market_bridge admission/gate; container entrypoint; root Dockerfile and requirements; independent CPython reference tests; Phase1A/1B sanitized diagnostic evidence. No tests or benchmarks were run because the task is read-only.

Official documentation:
- .NET cooperative cancellation: https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads
- Python future timeouts/cancellation: https://docs.python.org/3.13/library/concurrent.futures.html
- Kernel cgroup delegation/accounting/bandwidth and lifecycle: https://www.kernel.org/doc/html/latest/admin-guide/cgroup-v2.html
- GitHub package deletion scope requirement: https://docs.github.com/en/rest/packages/packages#delete-package-version-for-a-user

Repository preservation: HEAD unchanged; no tracked diff; sole pre-existing untracked authority file retained, SHA256012258B1C1E63DBDDE4C6F5BBC1123BFC8656F1100A166B1148219DCB2384BFC. PR330/issue322 not edited. Public gate false. Findings stored outside repository.

## 4. Phase 1B diagnostic evidence and limitations

Source SHA-256: `a50c8d11de450de7e81d122272154a3c8766356db40cd8820af485532bee0688`

### WP07 Phase 1B feasibility result — BLOCKED

Scope: owner explicitly approved PHASE1B_APPROVAL.md and exact hashed probe code. One bounded active case ran on the established West Central US Azure F1 application instance, 2026-10-10 21:56:28–21:57:22 UTC. No further active probes were run. A read-only preflight Azure plan query initially timed out; a second read-only preflight passed before deployment.

#### Measured admission failure

The guardian was PID1 of the new PID namespace. The helper released its inherited admission-lock descriptor before the guardian READY handshake; the observer closed its inherited holder, and Worker closed its holder before forking its grandchild. A distinct observer descriptor confirmed admission busy before the crash. Worker and setsid grandchild both resisted TERM.

After guardian SIGKILL, the observer acquired the admission lock in 0.001674915 seconds. Guardian, Worker and grandchild pidfds all still reported no exit notification at first acquisition. Result: FAIL / admission-reopened-before-complete-tree-exit. This directly disproves this sole-guardian-holder model's guarantee that admission stays locked until independently verified complete tree exit. Pidfd states do not establish how much runnable CPU any member consumed during this gap; no CPU experiment was run. No second Worker was launched to exploit the overlap.

All three known fixture processes and helper were cleaned, identity records removed, and own synthetic lock unlinked. Case elapsed0.085083348 seconds; discarded stderr0 bytes. Explicit finally cleanup means this case does not independently prove spontaneous kernel descendant termination before cleanup; the earlier corrected Phase1A did demonstrate termination before explicit fixture kills.

#### Capability matrix

| Capability | Status | Evidence / limit |
|---|---|---|
| Unprivileged PID namespace and guardian PID1 | PASS | Current run, actual aiq UID1000, kernel6.6.157.1-1.azl3 |
| READY/START before Worker creation | PASS | Audited kernel credentials and local PID1 |
| Guardian death kills TERM-resistant descendants | PASS, observed prior case | Corrected Phase1A; not universal lifecycle proof |
| Admission continuity through final-holder death | FAIL | Lock reopened before all registered pidfds signalled exit |
| Known current fixture cleanup | PASS | pidfds, helper reap, removed process identity records |
| Parent death/pipeEOF, orderly exit, handshake interruption, timeout | NOT TESTED | Remaining Phase1 cases stopped |
| Nested namespace/proc/reparenting escape | NOT TESTED | Remaining Phase1 cases stopped |
| Writable cgroup delegation | FAIL, prior diagnostic | Read-only cgroup mount; no delegated writable subtree |
| Complete private filesystem boundary | NOT TESTED | Prior original view permits private-path access; no new private reads |
| Cumulative CPU accounting/enforcement | NOT TESTED | Phase3 not reached |
| Network isolation | NOT TESTED | No network namespace probe/provider traffic |

#### Restoration and preservation

Original immutable image sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a restored; startup override remained empty. Streamlit health200/ok and root200/Streamlit HTML verified. Target instance hash unchanged, other region image/startup unchanged, both plans remained F1. Dedicated SCM evidence file deleted and404 verified; dedicated directory verified empty, deleted and404 verified. No private data or credentials were read or persisted by the probe. No additional resources, packages, scheduled jobs or settings changes. Public invocation remains PUBLIC_RUNTIME_VERIFIED=False.

No repository files were changed; existing untracked authority file preserved. PR330 remains OPEN/DRAFT at67893f51931ee183f71050a83b81127e6e648b87; issue322 remains OPEN. No acceptance, activation, merge or revised Worker deployment.

#### Decision and remaining authority

BLOCKED for the tested guardian-only admission architecture. PID namespace termination alone does not preserve admission ownership through guardian death. Do not proceed to Phase2/3 on this evidence. A new owner-reviewed architecture must define crash-safe admission ownership and orphan recovery, then prove remaining Phase1 lifecycle/escape cases. Filesystem, network and hard cumulative CPU guarantees still require separately authorized verification. Reservations, elapsed deadlines and rusage do not themselves enforce hard aggregate CPU ceilings. Implementation requires a new exact-path mutation allowlist; this diagnostic grants none.

Temporary registry image tag wp07-lock-d43703d7, digest sha256:71006f6c73c589cade31dfcdbd3d7917034facd0c106d56f5089ee5a38294a00 is no longer deployed. Owner-managed deletion deadline:2026-10-11 20:56:16 UTC (17:56:16 Sao Paulo), conservatively less than24h after publication. No cleanup job created. Earlier retained diagnostic tags retain their previously reported deadlines.

Raw sanitized evidence: phase1b-azure-result.json; restoration: phase1b-safety-report.json; reviewed plan/code hashes: SHA256SUMS.txt. The controller's static authority label predates approval; this report records the subsequent explicit owner approval without altering approved code.

## 5. Remaining-threshold measurement and qualification plan

Source SHA-256: `934e3ffc37e522aab1db1cde91607ff5654cf15732be2fde6444d014f6aa2d58`

### WP07 Option B — remaining-threshold measurement plan

Status: PLANNING ONLY. The owner approved the recommended dispositions of the G-B0 decision record74bf3f777efd4005955fe564c14d8c96, then requested continuation. This records that conversational approval; the earlier artifact's original UNAPPROVED heading is historical. No effective Section10 amendment, repository mutation or active-probe authority follows. G-B0 remains incomplete pending residual thresholds and exact documentation reconciliation.

#### Decision worksheet

| Open decision | Evidence to collect under later authority | Proposed decision rule |
|---|---|---|
| Health headroom | Same-instance baseline and bounded-workload health latency/errors, UI responsiveness and container memory | Owner chooses an absolute latency ceiling and permitted degradation/error rate before tests; failure is not excused by average latency |
| Cache-hit rate/concurrency | Full validation, disk read, analytics/projection, serialization and delivery costs across all12selections; cold cache is distinguished | Bound accepted cache-hit work globally and per session, separately from generation; use measured per-read cost plus headroom to choose numeric rate and concurrent-read cap |
| F1 generation share | Current applicable plan quota, authoritative available platform counters, baseline UI costs, whole-container and generator/supervisor usage | Owner chooses quota-share fraction and safety margin. Do not infer Azure-metered quota from reservation ledger or host wall time |
| CPU overshoot | Actual CPU and uncertainty for Worker descendants, setup/guardian/supervisor, cancellation/cleanup and all injected failure paths | Owner chooses maximum acceptable measured overshoot and unmeasurable-risk treatment. Unknown consumption never becomes zero or a proven upper bound |
| Recovery impact | Evidence-based cleanup time, denied-generation duration, runbook intervention | Indefinite fail-closed generation unavailability is accepted; no deadline requires unsafe reopening |

Useful sizing constraints, not enforcement proofs: with allowed available measured CPU headroom H and conservative observed cache-read cost C, proposed accepted rate R must satisfy R*C < H after reserving overhead/safety margin; concurrent-read cap must also fit memory and latency targets. Distributions, uncertainty, background CPU, cache misses and denial cost matter. Unknown C/H prevents threshold qualification. Reject costs and transport/Streamlit overhead still consume quota; application limits cannot guarantee resistance to unlimited ingress.

Existing approved evaluation targets remain coldp95<=5s,warmp95<=2s,childRSS<=160MiB,totalworking-set<=640MiB. They are not test results. No new health/cache-hit/quota-share/overshoot number is silently approved here.

#### Sequential qualification plan

1. Contract reconciliation first: exact proposed Section10 and release-document edits require a separate literal-path documentation allowlist. Freeze admission domain, numerical evaluation settings, clocks, fail-closed recovery, and which remaining thresholds are provisional versus acceptance requirements. Do not assert G-B0 complete while effective policy and final risk decisions are outstanding.
2. Read-only supervisor architecture preflight requires separate authority under the consolidated proposal. Establish durable spawn registration, ownership/recovery and confidentiality arguments before writing probe code. State precisely how supervisor death and the Phase1B final-holder race stay blocked. If no credible mechanism exists, stop; headroom measurements cannot compensate for overlap or confidentiality failure.
3. Separately authorized offline implementation/testing comes before target execution. Preserve exact C# generator and shared analytics; inspect/audit no private provider calls, all12windows/250warmup, bounded bytes, cache and session semantics. Do not use production UI as a harness while public invocation is disabled.
4. Only after the prerequisite architecture/contract authority, prepare exact ephemeral synthetic lifecycle/isolation probe code and obtain separate owner approval. Repeat Phase1B regression, parent/guardian/supervisor/combined failures, unknown spawn/registration, restart/corrupt history, nested/reparenting and synthetic-only private-boundary negatives. Keep private contents and credentials out of evidence. Any unverified cleanup or confidentiality aborts later stages.
5. Resource characterization requires its own bounded plan: same target identity, immutable image, instance/topology, F1 preservation, original-image restoration and health verification, exact process/request counts, capture caps, deadlines, cancellation and independent cleanup. Start with low-load isolated single requests. No sustained flood, CPU loop, unlimited retries or production-parent death. Approved numerical generation admissions apply; do not bypass them for profiling without a specific owner amendment.
6. All12cold selections fit the15admissions/rolling24h evaluation budget but cannot run as one rapid burst with3/rolling900s. Schedule under actual durable admission timestamps. Offline selection coverage is not target-runtime performance evidence. Include failures/uncertain starts in admission counts and reservations. Twelve cold samples are functional coverage, not statistically credible p95 estimation; larger sampling needs separate explicit allocation across windows/days and authority.
7. Warm/cache-hit and denied/invalid traffic tests follow only after their bounded workload plan and candidate limiter implementation are approved. Verify real cache hits do not generate; note analytics may still compute per response. Distinguish cached chart rerender with no service request from validated cache reads. Keep health checks in a distinct protected class. Identity rotation/tabs cannot bypass global generation/read controls. Bound limiter bookkeeping; no unbounded client registry or waiting queue.
8. Report observed distributions/sample size, full container memory, actual CPU where available, accounting coverage/uncertainty, supervisor/setup overhead, deadline/cleanup overshoot and preserved health. Obtain explicit owner acceptance of final thresholds before G-B3/G-B6/G-B7 or public activation. Testing does not authorize acceptance/merge/deployment.

#### Required future active-probe approval packet

Exact code hashes and ephemeral artifact paths; target and deployment/retention/rollback actions; bounded process tree; finite total requests including health/denials; request spacing and concurrency; fixture data inventory; instance identity/topology checks; admission budget accounting; monotonic deadlines/output caps; stop triggers; independent cleanup and original health verification; secret-redaction transport; image deletion authority/deadline. Approval of this measurement plan is not approval of an unseen execution packet.

#### Stop/hold state

No repository files modified, commands executing the generator run, load traffic, probes, deployment or GitHub mutations. Both pre-existing untracked prompters preserved. PUBLIC_RUNTIME_VERIFIED=False. External supervision remains preferred. Containment development remains stopped until effective security-contract and separately scoped next-step authority exist.

## 6. Completed Phase1B image cleanup record

```json
{
    "registryDeleted":  true,
    "completedUtc":  "2026-10-10T22:04:28.6880868Z",
    "earlierDiagnosticImages":  "Outside this exact cleanup scope; previous obligations not represented as cleared",
    "authentication":  "Owner-approved delete:packages refresh completed; no credentials persisted in evidence",
    "localTagRemoved":  true,
    "scope":  "Phase1B image only",
    "registryVersionReturns404":  true,
    "tag":  "wp07-lock-d43703d7",
    "digest":  "sha256:71006f6c73c589cade31dfcdbd3d7917034facd0c106d56f5089ee5a38294a00",
    "packageVersion":  1367169458,
    "allRegistryPagesChecked":  46,
    "productionDigestPreserved":  true,
    "bothAzureImageReferencesUnchanged":  true
}
```

## 7. Controlling-document reconciliation checklist

**Status:**Review preparation only. No edits to these four documents are made or authorized by this checklist. Locations refer to the inspected repository version and must be rechecked before applying any amendment. Existing source excerpts above remain intact.

| Document / current location | Candidate reconciliation | Preserved requirements |
|---|---|---|
| [Controlling policy, Section10](../../../architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md#10-release-114-amendment--synthetic-public-preview-and-retained-secondary-provider) | Review exact adoption of AmendmentsA–F, OptionB resource-risk disclosure, admission domain and G-B0–G-B7. Establish precedence explicitly. | Synthetic-only public data, private separation, publication authorization and inactive-provider boundary |
| [Release definition](../RELEASE_1.14_DEFINITION.md), resource targets and residual gates | Relabel60/240 as reserved CPU admission budgets, add16/job and3/15 rolling counts, distinguish2CPU-second cold performance target from enforcement, document unresolved thresholds and durable recovery. Update obsolete statement that all design selections are resolved. |4096B input, bounded capture, cache limits, synthetic/analytics identities, confidentiality and no-overlap |
| [Execution plan](../RELEASE_1.14_EXECUTION_PLAN.md), WP07/WP08 and common authority rules | Add contract-authority gate before resumed implementation; map crash-safe supervision, G-B1–G-B7, abuse/cache-hit/health protections and separately authorized measurement to accountable WPs. Avoid treating accepted planning as runtime acceptance. |WP07 before dependent WP05 acceptance; WP08 integrated evidence and separate lifecycle approval |
| [File manifest](../RELEASE_1.14_FILE_MANIFEST.md), WP07 and shared-path ownership | Reconcile documentation scope and planned supervision/test seams only after exact paths are proposed and owner-authorized. Record this consolidated planning artifact without inferring authority for new guardian/helper/source files. |Literal mutation allowlists, sequencing, preserved private adapters and unrelated files |

### Specific language conflicts to resolve

- Definition resource-table “CPU” values must distinguish actual observed consumption, performance targets and policy reservation units. A reservation ledger must not be described as kernel enforcement or Azure-metered quota.
- Definition's statement that remaining gates are solely evidence gates must acknowledge the still-open health, cache-hit, quota-share and acceptable-overshoot decisions.
- Per-key cache lease semantics must remain distinct from global admission. Neither35second lease expiry nor available flock clears occupied state.
- The authoritative admission domain must be explicitly one qualified public instance across its sessions/processes; topology violation fails closed. This does not establish regional/deployment-wide lock correctness.
- Historical text about current implementation must be labelled against its baseline; original-code observations are not descriptions of accepted production runtime.

### Remaining owner decision packet

1. Specify health latency/error and acceptable degradation thresholds.
2. Specify permitted F1 quota share and measured CPU-overshoot acceptance, including unknown-accounting treatment.
3. Specify independent cache-hit rate/concurrency and limiter-state bounds; confirm protected health class.
4. Provide exact documentation mutation paths if applying policy changes is desired. The four linked paths are candidates, not granted permissions.
5. Approve exact policy diff and G-B0 disposition before separately granting supervisor architecture preflight, implementation and any active verification.

The current request continues planning. It does not waive the earlier explicit hold or authorize guessing unresolved thresholds. Public invocation remains disabled, and no acceptance/activation follows from this checklist.
## 8. Proposed numerical thresholds for explicit owner review

**Status:**Proposed by Codex at the owner's request; not yet accepted, implemented or measured. These values complete the decision packet, not G-B0. They are conservative starting criteria chosen for a low-volume synthetic preview; no benchmark supports their feasibility. Failure requires review or continued unavailability, not silent relaxation.

| Boundary | Proposed acceptance/evaluation value | Precise scope and failure treatment |
|---|---|---|
| Health latency |Warm running-host healthp95<=1second; every sampled response<=3seconds; zero unexpected5xx/timeouts in each authorized qualification batch |Report startup/restoration separately, not hidden in warm figures. Baseline and loaded figures must both be disclosed with sample count. Fail qualification on breach; no claim of availability under arbitrary ingress |
| Health headroom |Loaded healthp95 no more than2times the same-instance warm baseline, while also satisfying the absolute1second limit |Protect an independent health path/class. Controlled monitoring at most1check/10seconds per test observer, finite predeclared count; this is a test sampling cap, not authorization to throttle Azure platform probes |
| Cached service reads, global |At most30 accepted cached-chart service reads in rolling60seconds; maximum2 in any rolling1second; one active cached read across the admitted instance |No waiting queue; excess requests receive cheap bounded Busy/unavailable without Worker launch. Apply accounting before disk validation/projection/analytics/serialization; invalid/denied ingress costs remain outside this protection |
| Cached service reads, session |At most6 accepted reads/rolling60seconds per session, additionally subject to all global controls |Session rotation cannot bypass global limits. Hover/zoom/ordinary client rerender remains acquisition-free. These counters are separate from expensive attempts and generation reservations |
| Limiter state |Maximum1024 live supplementary client/session limiter records, idle retention30minutes; no silent counter-reset eviction that grants admission |At capacity, reject new expensive/client-class admissions cheaply or apply the strict global-only denial policy; existing global history survives. Implementation must bound identifier lengths and bookkeeping bytes separately |
| Generation and supervision daily CPU evaluation allowance |At most10% of applicable documented daily F1 CPU quota, provisionally360CPU-seconds if the target quota is verified as3600seconds/day |Include Worker descendants, request setup/guardian and attributed supervision/cleanup overhead. This is measured allocation/qualification, not kernel enforcement. Cache-hit/denial/UI/HTTP/background CPU must be measured separately and still fit total operating headroom |
| Platform headroom trigger |Deny new generation when verified remaining applicable daily or short-window CPU quota is<=20%; missing/stale/unverifiable quota/resource evidence also denies generation |Define sampling freshness<=60seconds and validated counter/reset semantics before implementation. No assumed Azure API availability. Monitoring and denial do not guarantee quota preservation; inbound CPU and measurement lag remain residual risks |
| CPU overshoot acceptance |For each fully accounted qualification job, actual attributable request CPU must be<=20CPU-seconds, i.e. at most4seconds above the16second policy reservation |Include descendants and per-job setup/cleanup/supervision. Incomplete accounting does not pass. Normal cold-work2CPU-second performance target remains. This is an observed acceptance criterion, not a crash-safe hard ceiling |
| Timeout/cleanup |Retain10second operation,15second admission-to-cleanup target and<=1second TERM grace |Report every deadline breach and measured cleanup lag; any unfinished boundary retains durable occupation and denies readmission. No numerical overshoot allowance permits overlap or clearing unknown state |
| Memory/latency |RetainchildRSS<=160MiB,totalworking-set<=640MiB,coldp95<=5seconds,warmp95<=2seconds |Include supervisor and parent/container overhead in total measurements. Report sample counts and method;12selections alone are not robustp95 evidence |

### Arithmetic and interpretation

Approved planning reservation budgets remain60/900seconds and240/86400seconds, with16/job and3/15admission caps. The proposed10% daily evaluation allowance is a separate measured policy allocation. At15jobs with observed per-job CPU<=20seconds, attributable jobs total<=300seconds;60seconds remain within the360second evaluation allowance for other generation-supervisor activity. This arithmetic is conditional on complete accounting and observations; it does not upper-bound arbitrary failures, guarantee Azure quota treatment, or establish a rolling-window CPU ceiling.

Microsoft currently documents60CPUminutes/day for the Free tier. It also documents a short CPU quota and application stoppage when relevant quotas are exceeded. Therefore the target's actual counters, quota scope and reset behavior must be verified separately; rolling24hour application admission is not Azure's daily reset. No guessed short-window allowance is proposed.

Sources checked2026-10-10:
- [Microsoft App Service resource limits](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/azure-subscription-service-limits#app-service-limits)
- [Microsoft App Service quotas and metrics](https://learn.microsoft.com/en-au/azure/app-service/web-sites-monitor)

### Owner decision and implementation boundaries

Owner review should accept/revise the numerical values above, the accounting/freshness requirements and the disclosed residual ingress/CPU risk. Approval would still require exact Section10/release-document reconciliation and separately scoped architecture/implementation/test authority. If fresh platform counters or complete attributableCPU evidence cannot be obtained without violating free-tier/resource restrictions, stop for owner revision; do not label reservation data as platform quota evidence.

This section does not authorize cache-rule changes, pregeneration, a production Python port, new services/dependencies, active load tests or public activation. Confidentiality, synthetic provenance, no overlapping jobs and evidence-based cleanup are unchanged mandatory gates.

## 9. Final owner-approval packet

**Pending owner decision.** No approval is inferred from continued planning. The following text is a proposed authorization the owner may explicitly approve or revise; inclusion here does not make it operative.

### Proposed decision text

> I approve OptionB external supervision as the security-contract direction and the numerical evaluation criteria in Section8, including their explicit distinction from hard CPU enforcement or Azure quota guarantees. Confidentiality, synthetic-only publication, no overlapping jobs and independently verified cleanup remain mandatory. I accept fail-closed generation unavailability and evidence-based manual recovery. Unknown accounting or missing fresh resource evidence does not pass qualification.
>
> I authorize documentation-only reconciliation strictly within these four existing paths:
>
>1. `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
>2. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
>3. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
>4. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`
>
> Apply the OptionB amendmentsA–F, G-B0–G-B7, approved planning dispositions and Section8 criteria consistently; retain the selected C# generator/shared analytics/Worker byte transport and all confidentiality/publication restrictions. Supersede guardian-only flock admission assumptions and hard CPU-consumption claims only as explicitly described by OptionB. Preserve all unrelated files and requirements. Record planning authority separately from untested runtime evidence, and present the resulting exact documentation diff for owner review.
>
> This does not authorize implementation, supervisor/probe code, new dependencies/services, active tests, deployment, staging/commit/push, PR modification or merge, issue closure, WP07 acceptance or public activation. Separate approval of the resulting policy diff is required before treating the documentation as the effective amended security contract. Subsequent read-only supervisor preflight also needs its own scope. `PUBLIC_RUNTIME_VERIFIED=False` remains.

### Review qualifications before signing

- The numbers in Section8 are proposed engineering criteria chosen for a low-volume preview, not validated capacity. Threshold approval is risk/policy approval, not a passing test result.
- Confirm that platform-metric freshness<=60seconds is acceptable even if its absence makes generation unavailable; no new monitoring service is assumed.
- Confirm the specific measured20CPU-second/job criterion,360second/day evaluation allocation under verified3600second/day quota, and the residual lag/ingress risks. No hard crash-safe CPU bound is asserted.
- Health warm-target measurements must report startup/restoration separately and disclose sample size; health-path rate protection must not block platform probes.
- Public invocation remains disabled until every applicable gate, substantive acceptance and separate activation authority is satisfied.

### Documentation-only completion evidence

After explicit authorization, provide the exact four-file diff; mapping of amendments/thresholds to policy and release requirements; numeric/window/counter consistency checks; Markdown/link checks appropriate to the edit; unchanged source/startup/dependency/provider files and preserved unrelated worktree; explicit remaining runtime gates. No test execution is needed to establish runtime behavior because this step changes documentation only. A pending owner review must not be labelled G-B0 complete.

Until this packet is explicitly approved or revised, further useful work is owner decision-making. No additional containment development or active testing is undertaken.

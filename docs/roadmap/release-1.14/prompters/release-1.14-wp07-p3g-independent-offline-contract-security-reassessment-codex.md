# Release 1.14 WP07-P3G — Independent Read-Only Offline Contract Security Reassessment

## Authority and purpose
Owner explicitly authorizes **P3G independent read-only reassessment only**. Evaluate whether the two P3E blockers are actually closed by P3F, whether prior P3C safeguards remain effective, and whether any material static-model acceptance bypass remains. **Do not repair, implement, execute tests, or select a native technology.** A passing source review is not native qualification.

## Protected checkout and scope
- Protected branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Primary read targets (no writes):
  - `container/admin_lifecycle_native_boundary_contracts.py` — expected SHA-256 `5231BD87F3ECF828ACD13609EB3A7E56980B4A94C6E3E797F56728984CF65AD2`
  - `python/presentation/test_admin_lifecycle_native_boundary_contracts.py` — expected SHA-256 `B1639A4DC14671B04F664CE556C60E0E037ACCA0BC4DF9A6E6631378F33D4FEA`
- Read supporting frozen codec, accepted `container/admin_lifecycle_launch_plan.py`, launch-plan tests, local packaging/bootstrap/legacy runtime code and relevant docs only as needed for source-backed comparison. Do not interpret local untracked runtime code as merged production behavior.
- Record complete initial/final `git status` including untracked entries, branch, HEAD, local refs and baseline/final file hashes. Preserve existing dirty checkout exactly. Do not fetch or switch branches; do not modify Git metadata or worktree. If hashes or protected state disagree, STOP and report.

## Mandatory independent analysis
1. **P3E blocker 1 — gate provenance reconciliation.** Independently trace all constructors and transitions of the agent-exec boundary. For **every shared endpoint ID** across current inventory and gate history, require identical channel, creator, alias relation, and position, including current handles relabeled `credential`. Analyze the exact reported credential-relabeling counterexample, incomplete/omitted inventory, duplicate IDs, alias copies, A_EXEC/ACK provenance and explicit closure before exec. Verify that the contract cannot accept contradictory histories through alternative construction paths or permissive defaults. Cite exact file/line evidence.
2. **P3E blocker 2 — startup-prefix integrity.** Independently trace startup-ledger constructor, advance and completion paths. Check valid empty, partial and complete prefixes; reject `('bogus',)*4`, wrong types, unknown, duplicate, skipped, reordered and overlong histories, including any bypass via direct construction or `listener` advancement. Check prerequisites for readiness and exposure; do not treat event-count equality as semantic proof. Cite exact evidence.
3. **Preserve earlier P3C protections.** Reassess gate/ACK mandatory history and alias closure; direct-constructor completed-wait bounds; child-specific positive normal exit-zero and sticky failures (no ECHILD, EOF, absence, pidfd readiness, wrong PID, signals or nonzero exit as proof); strict EOF→agent wait→guardian wait, HOST_DISPOSED→router wait, mandatory waits→drain/absence; distinct A/G/R numeric PID and parent bindings and exclusive status owners; absolute monotonic deadline and overflow checks; permanent hardening declarations and trusted/presentation session distinction.
4. **Descriptor and protocol composition.** Cross-check P3F model with accepted launch plan and frozen 64-byte `aiq-admin-lifecycle-v1` codec: metadata lifecycle, guardian/agent-bootstrap gate ACK versus exec/readiness, RA/SR/SG closure, R_EXPOSE gating, FD3/4/5 mappings, FD5 conditional design only, inherited alias cleanup and UI exec isolation. Explicitly distinguish what each standalone validator proves from what remains uncomposed across validators. Do not claim native or deployment feasibility.
5. **All 30 tests — read-only effectiveness review.** Inspect each test method and its assertions without running tests or importing project code. Confirm P3F exact-counterexample coverage, reason-specific rejections and valid positive cases. Identify uncovered acceptance paths with minimal **source-derived** counterexamples; do not execute them. Report whether the reported 30-pass outcome was merely user/Codex reported (not rerun here).
6. **Residual feasibility risks.** Separately list unqualified native descriptor allocation/closure, unknown scratch slots, exclusive SIGCHLD/kernel wait ownership, PID1 sessions, UID/GID/capabilities and base-image collisions/digests, libc/ABI/toolchain, CLOCK_MONOTONIC/deadline feasibility, four-second shutdown versus five-second Docker stop grace, and disabled Option A/public/Azure gates.

## Prohibitions and STOP conditions
Strictly read-only source inspection and safe hash/status commands. **No file edits, patches, imports, test runs, builds, restores, scripts, native operations, process launches for qualification, network/fetch, Docker, Azure, commits, PRs, merges, branch/ref changes, or deployment.** Do not add files, dependencies or authority. If a needed conclusion requires execution, mark it UNVERIFIED rather than executing. If preservation fails or a material conflict with frozen contracts is found, STOP and escalate.

## Required report
- Evidence table: each P3E blocker and each earlier P3C protection as **PASS / GAP / BLOCK**, with severity, exact line references, counterexample where applicable and impact. Distinguish static-model flaws from runtime exploits.
- Test review accounting for all 30 methods; coverage holes; frozen protocol/launch-plan composition boundary; outstanding native qualification gaps.
- Baseline versus final SHA-256, full protected-state preservation proof, commands used, and explicit confirmation no tests or modifications occurred.
- Smallest safe separately authorized next increment, if any, with exact allowlist and validation/STOP conditions. **No automatic approval of corrections, bootstrap language, ABI, implementation or activation.**
- End with exactly one verdict:
  - `WP07-P3G INDEPENDENT REVIEW ACCEPTED — OWNER DECISION REQUIRED`
  - `WP07-P3G BLOCKED — OWNER REVIEW REQUIRED`

`PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain **NO-GO**.

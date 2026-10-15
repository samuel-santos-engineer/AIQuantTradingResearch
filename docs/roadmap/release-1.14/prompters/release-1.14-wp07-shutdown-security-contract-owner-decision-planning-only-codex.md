# Codex Authority — Release 1.14 WP07: Shutdown Security Contract and Owner Decision Record

**Authority class:** Planning-only / read-only. **No implementation authority.**

## Mission

Prepare an evidence-grounded proposed shutdown security-contract amendment, threat-model assessment, and explicit owner decision record for the unresolved enabled-mode agent termination problem. Do not decide policy on the owner's behalf. The required output is a recommendation with clearly separated existing requirements, proposed changes, alternatives, risks, and owner approval questions.

## Fixed baseline and preservation

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- The working tree is intentionally dirty, including untracked files. Preserve every existing change. Capture initial and final branch, HEAD and `git status --porcelain=v1`; distinguish status-level preservation from content-hash verification. Do not clean, reset, checkout, stash, stage, commit, push, rebase, or modify files.
- If baseline differs, STOP and report. Do not attempt repair.

## Established evidence — do not overclaim

- Owner qualification recorded runtime `10.0.12`, `ECHILD` from the application waiter, `AgentTerminationUnverified`, Docker exit 64, no OOM. Agent exit code 0 was **not** proven.
- Public .NET `v10.0.12` source contains PID 1 child-reaping behavior that can race application `waitpid(-1, WNOHANG)`. This is a supported mechanism, **not proof of the actual status consumer** in the recorded run. Exact observed container binary/source provenance remains unverified.
- The existing agent waits for Streamlit and reports STOPPED / closes the lifecycle channel before its own final exit. ACK, EOF, liveness and disappearance are not proof of agent exit 0.
- Existing router remains PID 1 UID/GID 1100:1100; agent and UI UID/GID 1000:1000; finite root bootstrap only. Disabled-mode Option A is frozen. No resident root helper, cross-UID signaling, credential leakage, or broadened privileges.
- Shutdown has one shared monotonic four-second coordination deadline and five-second Docker grace; failure containment must not be presented as clean shutdown.

## Read-only sources to examine

Inspect only existing repository source, relevant existing design/runbooks and sanitized owner qualification reports if locally accessible. Priority files:

- `Dockerfile`
- `container/admin_diagnostics_bootstrap.py`
- `container/presentation_lifecycle_agent.py`
- `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs`
- `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs`
- Relevant tests and qualification runbook, read-only.

If externally available source is consulted, identify exact version/URL and separate public-source evidence from observed-binary evidence. Do not download binaries or restore dependencies. Do not require network access; mark missing evidence as missing.

## Deliverable A — existing versus proposed security contract

Create an **in-response** table separating: (1) binding current requirements, (2) candidate policy amendments, (3) non-negotiable prohibitions, (4) qualification-only obligations. Address specifically:

1. Agent exit zero: whether an authenticated, trusted guardian report based on a direct kernel wait may count as verified **agent** exit status, and the precise trust assumptions required. No agent self-report, EOF, PID disappearance or unbound PID observation may substitute.
2. Guardian integrity: threat actors and same-UID boundaries (agent/UI/guardian UID 1000), executable integrity, FD confinement, status channel provenance, impersonation/injection risks, compromised or buggy guardian behavior, and whether trust is acceptable under the existing contract or requires amendment.
3. Guardian closure: distinguish (a) independently observed guardian termination, (b) independently observed guardian exit **code 0**, and (c) guardian's truthful report of agent exit zero. Specify the security and failure-detection tradeoff if (b) is not mandatory; never silently waive it.
4. Failure containment: differentiate clean shutdown PASS from terminal FAIL with PID 1 exit/container-wide teardown; address orphaned agent/UI, guardian hangs/crashes, simultaneous failures and deadline exhaustion. Identify which containment properties need Linux evidence.
5. Router/PID 1 .NET reaper interactions, identity binding, PID reuse, timing, rollback, no additional privileged process, no exposure of synthetic credentials, and unchanged disabled mode.

## Deliverable B — alternatives and threat-model decision matrix

Compare at least:

- Existing .NET PID 1 wait approach (including runtime reaper race).
- Child-specific PID/pidfd waiting without independent status ownership.
- Dedicated unprivileged guardian as direct agent parent with trusted wait-result attestation.
- Alternative parentage or supervision model that preserves router PID 1 and all frozen identity/privilege constraints, if any defensible option exists.

For each: status provenance, guardian/helper closure, trust assumptions, cleanup on combined failures, shared-deadline feasibility, supported API status, compatibility, and explicit unresolved proofs. Do not claim design closure without evidence.

## Deliverable C — proposed owner decision record (NOT an approval)

Draft a compact decision record with three independent choices and an explicit **UNDECIDED** default:

- **D1 — Agent evidence:** May a bootstrap-bound, trusted UID 1000 guardian's *actual kernel-wait* report be accepted as authoritative evidence of the agent's exit 0? Enumerate conditions and risks for YES and NO.
- **D2 — Guardian closure:** Must guardian exit code 0 itself be independently proven, or is authenticated result + independently observed guardian termination acceptable? Explain what failures would be undetectable under the latter policy.
- **D3 — Failure containment:** Is PID 1 exit and container-runtime teardown acceptable containment after an application shutdown FAIL, provided it never upgrades the verdict to PASS? State required evidence and limitations.

For each decision: exact proposed normative wording, default recommendation (if defensible), alternative, residual risk, required evidence, and owner signature/approval placeholder. Explicitly flag any relaxation of current security guarantees. Do not treat the owner as having approved any choice.

## Deliverable D — gates and next narrow authority

- State whether the **security contract is ready for owner decision** (distinct from implementation design being closed).
- State **DESIGN CLOSED** or **DESIGN NOT CLOSED** for the actual shutdown implementation, supported by explicit proof obligations.
- Identify minimum additional evidence needed for any implementation authority: observed runtime image/build provenance, guardian trust/integrity, reliable status ownership, guardian closure, orphan containment and shared-deadline behavior.
- Recommend a narrowly scoped next authority **only conditionally**, after owner decisions. Do not generate an implementation allowlist unless architecture and contract are genuinely closed; no edits under this authority.
- Final verdict must retain `PUBLIC_RUNTIME_VERIFIED=False`, administrative API availability NO-GO, production ingress TLS NO-GO, Azure passive qualification NO-GO and public Worker activation NO-GO.

## Execution restrictions

**Allowed:** read-only inspection, bounded source comparison, reasoning, and an in-chat report/decision draft.

**Forbidden:** modifying or creating repository files, changing Git state, builds, tests, Docker, host qualification, Azure, Worker, deployments, package restores, network binary downloads, process signaling, credential access or disclosure, privileged operations, implementation, and claiming policy approval. Do not write an ADR to disk without separate authority.

**STOP conditions:** unexpected branch/HEAD, unavailable essential evidence, an option requiring additional privilege/cross-UID signaling, unsupported status reconstruction, deadline relaxation, security downgrade presented as implementation detail, or an attempt to treat missing status as PASS.

## Required response structure

1. Baseline and preservation evidence.
2. Evidence ledger: confirmed / supported / unproven / unavailable.
3. Existing contract vs proposed amendments.
4. Threat model and alternatives matrix.
5. D1/D2/D3 proposed owner decision record (all UNDECIDED).
6. Proof obligations and smallest conditional next authority.
7. Explicit **DESIGN CLOSED / DESIGN NOT CLOSED**, release gates and **STOP — owner review required**.

**No implementation. No runtime qualification. No owner decisions inferred.**

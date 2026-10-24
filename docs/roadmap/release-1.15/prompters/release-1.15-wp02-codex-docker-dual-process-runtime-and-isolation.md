# Release 1.15 WP02 — Docker Dual-Process Runtime and Isolation

## Authority and objective

Implement **WP02 / issue #368** in `samuel-santos-engineer/AIQuantTradingResearch`, using the owner-approved WP01 architecture and contracts. WP01 issue #367 is closed; PR #380 was squash-merged as `6f76697707e2363cc52820e0c805172815b92159`. Verify GitHub `main` and the latest issue/PR state rather than trusting cached refs.

Goal: a reproducible **single Docker-first Linux/amd64 image** running separate private C# research and public Streamlit application processes under a minimal trusted PID1 supervisor, with enforced filesystem, credential, process and network isolation, bounded lifecycle and independently witnessed failure behavior. Implement only the **WP02 runtime/security boundary**; do not implement WP03 ingestion/SQLite recovery, WP04 numerical research, WP05 production snapshot protocol/HMAC, WP06 production chart, WP07 Azure qualification or WP08 acceptance.

Owner acceptance of D1-A/D3-A/D4-A/D5-A authorizes their use as the engineering baseline, **not** claims of verified production behavior. In particular, D1-A's retained-root supervisor, narrowly scoped capabilities, process-tree closure and fail-closed semantics must be tested rather than assumed. `PUBLIC_RUNTIME_VERIFIED=False` throughout WP02.

## Preflight and provenance

1. Inspect `AGENTS.md`, `.gitattributes`, `docs/governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md`, `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md`, `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, and Release 1.15 definition, execution plan, manifest, issue register and acceptance documents.
2. Read merged WP01 artifacts on `main`: `RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md`, `RELEASE_1.15_WP01_DECISION_REGISTER.md`, `RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md`, `RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md`, plus the S2 feasibility evidence. Explicit owner acceptance on PR #380 supersedes the earlier assessment's OPEN/PROVISIONAL/BLOCKED labels **for the approved WP01 contracts only**; preserve historical reports and do not invent production tests.
3. Verify clean worktree, local/remote `main` alignment, and no unrelated uncommitted changes. Use `git fetch` and `git merge --ff-only origin/main` only when safe; no reset/rebase/force-push. **Inventory all untracked Codex prompt files. They are valuable provenance artifacts: do not delete, relocate, overwrite or ignore them merely to clean the tree.** If a prompt blocks checkout, stop and request guidance. Include this executed WP02 prompt in the WP02 PR and index it in the release manifest; preserve wording, normalize UTF-8/LF.
4. Read WP02 issue #368 and all governing exact-path/dependency rules. Before source mutation, establish an **exact-path WP02 implementation manifest** derived from actual repository layout and existing Docker/runtime ownership. Record proposed paths and expected impacts in `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md`. If governance requires separate owner authorization of that exact-path plan, or if changes exceed the manifest, **stop and submit the plan for owner review instead of implementing**. Do not interpret this prompt as overriding a narrower governance gate.
5. Work on a dedicated WP02 branch from current `main`; do not change the default deployment or Release 1.14 historical artifacts. Do not merge your own PR.

## WP02 implementation scope

### S1. Docker-first, one image, two roles

- Produce the smallest maintainable immutable Linux/amd64 image consistent with pinned project dependencies. Run trusted PID1 supervisor, private research child (UID/GID 12001) and public Streamlit child (UID/GID 12002); no supplementary groups. Research binds only `127.0.0.1:52731`; public UI binds internal `0.0.0.0:8501`. No raw API/public research listener, sidecar proxy, separate paid resource or CDN assumption.
- Use bounded offline fixtures/stubs only where WP03–WP06 implementation is absent. A stub may demonstrate role/network behavior but must be labeled as a fixture and **must not** be mistaken for a production snapshot, real candle feed, working public chart or acceptance evidence.
- Enforce independent private and public writable areas, private `/private` ownership 0700, sensitive files 0600, umask 0077, no public read/traversal of raw DB/checkpoints/watermarks/config/keys or research process channels. Use sanitized per-role environments and FDs, with no secrets in public environment, argv, logs or assets. Include explicit negative tests from public identity.

### S2. Supervisor trust and privilege

- Implement D1-A's **explicitly trusted retained-root PID1** model only if demonstrably feasible under the selected Linux runtime: minimized bootstrap capabilities and post-bootstrap effective/permitted/bounding sets exactly `CAP_SETUID`, `CAP_SETGID`, `CAP_SETPCAP`, `CAP_KILL`, empty ambient/inheritable; non-root children empty effective/permitted/bounding/ambient/inheritable, `no_new_privs`, and reviewed seccomp syscall restrictions. Do not silently substitute S2's broad default-root fixture.
- The bootstrap may use only the documented transient powers needed to establish verified ownership; no privileged Docker fallback, broad `--cap-add`, host PID namespace, weakened private permissions or unreviewed helper. Verify all effective identities, groups, thread credentials, capability sets, inherited descriptors, environment and denied cross-role access **from inside the final image**.
- Handle fork/exec/threading safety, `clone3`/`clone` namespace restrictions, `pidfd_open`/`pidfd_send_signal`, wait/reaping ownership and `/proc` visibility accurately. No unsupported assumption that `no_new_privs` blocks `unshare`, or that a pidfd makes a non-child waitable. If required kernel facilities or outer container policy are unavailable, **fail closed and report a blocker**.

### S3. Lifecycle, containment and no-overlap

- Implement fixed role state transitions, health/startup/shutdown deadlines, bounded logs, restart backoff/crash-loop budget and peer-independent health as proposed in D1-A. Do not infer descendant closure from process-group death or two empty `/proc` scans.
- Establish an **independently qualified complete descendant-closure witness** before permitting same-role replacement, including fork/setsid escape, UID/process identity and PID-reuse races, launch interruption and concurrent child exit. If this cannot be proven in the selected runtime, keep same-role restart **disabled** and fail closed; do not invent a witness or add a privileged fallback. A blocked restart is a valid honest WP02 result, not an invitation to weaken isolation.
- Confirm no role overlap within the tested PID namespace; disclose that deployment overlap, distributed single-writer exclusion and Azure F1 container-recreation behavior remain WP03/WP07 obligations.

### S4. Verification and evidence

- Add focused deterministic tests for the supervisor state machine, fail-closed transitions, bounded restart/shutdown, peer isolation, signal/wait and process-tree cleanup. Add Linux/amd64 image-level adversarial tests using fake secrets for public UID denial, socket bindings, capability/FD/env leakage, escalation attempts, setsid descendants, failed census and resource-bounded crash loops.
- Record exact commands, image digest, pinned versions, test fixtures, Linux kernel/container limits, pass/fail counts, observed timing and memory, and unresolved failures. Separate independently observed process death from supervisor self-report. Do not claim Azure F1 compatibility from local Docker.
- If the final image cannot meet D1-A without an amendment, stop and open a narrowly scoped **WP02 design-blocker** documentation report; do not change accepted D1-A unilaterally.

## Documentation, prompt artifacts and PR

- Create/update `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md` with exact paths, architectural rationale, scope boundaries, test plan and deviations.
- Create `docs/roadmap/release-1.15/RELEASE_1.15_WP02_EVIDENCE.md` with verified results, failures and remaining WP03/WP05/WP07 dependencies. Include a clear WP02 PASS / PARTIAL / BLOCKED recommendation, with evidence rather than optimism.
- Preserve this prompt under `docs/roadmap/release-1.15/prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md`; include the prompt in the PR and link it from `RELEASE_1.15_FILE_MANIFEST.md`. Inventory any other untracked prompt files and request explicit owner direction if unrelated.
- Update only WP02-relevant release docs when needed for accurate traceability. Historical WP01 records and governance are not rewritten merely to change past labels; if a concise owner-acceptance reconciliation is necessary, add an explicitly dated note without erasing history.
- Enforce UTF-8/LF/no BOM/final LF, check relative links and `git diff --check`; run the project's applicable tests and clearly disclose tests not run.
- Open a **non-draft review PR** linked to #368 only after preflight, exact-path authorization and substantive implementation/evidence. Leave the PR unmerged and issue #368 open for owner review. Do not start WP03, call Twelve Data, deploy to Azure, spend on resources, promote an image or change `PUBLIC_RUNTIME_VERIFIED=False`.

## Stop and handoff rules

Stop on dirty/diverged baseline, untracked prompt preservation conflict, unapproved exact-path expansion, governing-document conflict, inability to implement the required privilege/closure boundary, security regression, or unverified kernel dependency. Return the smallest actionable blocker with exact evidence and a proposed owner choice. Never report WP02 complete from fixture-only evidence or because Docker starts once.

At handoff return: PR URL/commit SHA, changed-path inventory, image/build digest and reproducible commands, precise test results and negative cases, security/lifecycle evidence, outstanding risks with WP ownership, prompt provenance confirmation, and a clear WP02 recommendation. `PUBLIC_RUNTIME_VERIFIED=False`.

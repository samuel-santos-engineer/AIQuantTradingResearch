# Release 1.15 WP02 - Exact-path implementation plan

Date: 2026-10-23. Issue [#368](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/368). **SOURCE APPROVAL PENDING; IMPLEMENTATION NOT STARTED.** All new/modified text must be UTF-8 without BOM, LF only, final newline; separately scan CR bytes and run git diff --check. `PUBLIC_RUNTIME_VERIFIED=False`.

## Baseline and authority reconciliation

Live main and PR #380 squash merge: `6f76697707e2363cc52820e0c805172815b92159`. Issue #367 CLOSED, #368 OPEN. Explicit owner acceptance on PR #380 establishes D1-A/D3-A/D4-A/D5-A as the engineering baseline with downstream conformance preserved. Historical OPEN/PROVISIONAL/BLOCKED labels describe pre-acceptance review and are not rewritten or treated as current WP01 rejection. No production qualification is inferred.

Local main was fast-forwarded safely to that merge; branch `feat/release-1.15-wp02-runtime-isolation` begins there. No tracked modifications existed. The sole untracked file is the executed WP02 prompt, expressly authorized for inclusion; no unrelated prompts/conflicts found. Prior WP01 branch and historical S1/S2 files are preserved.

The [tracked WP02 preflight](prompters/release-1.15-wp02-codex-docker-dual-process-runtime.md), Required preflight item2, says: "Propose the exact literal paths to change and run-only paths; obtain scoped implementation authority before writing code." The [new authority](prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md), item4, explicitly preserves narrower gates. This document submits those paths for approval; it does not grant itself source authority. No implementation PR is opened before that gate and substantive evidence.

Read alongside [accepted baseline specification](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md), [qualification owners](RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md), [S2 historical evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), [execution plan](RELEASE_1.15_EXECUTION_PLAN.md), [acceptance](RELEASE_1.15_ACCEPTANCE.md), [Docker policy](../../governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md) and [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md).

## Literal source/test/script proposal

These ten paths are NEW proposals, not authorized writes. Existing root Dockerfile/default image, container/entrypoint.sh, production Worker/Streamlit, packages/lockfiles/build policy/CI and historical feasibility files stay unchanged. A separate Dockerfile preserves default Release 1.14 deployment behavior.

| Literal path | Proposed responsibility |
| --- | --- |
| `container/release_1_15/runtime/Dockerfile` | Explicit Linux/amd64 candidate with existing digest-pinned SDK/ASP.NET/Python bases, existing Python pins, native supervisor build and two labeled fixture roles |
| `container/release_1_15/runtime/supervisor.c` | Single-threaded Linux C PID1; direct credential/capability/seccomp/pidfd/wait calls; fixed commands; bounded logs/census/health/shutdown; no privileged helper |
| `container/release_1_15/runtime/lifecycle_policy.h` | Bounded deterministic state/deadline/backoff policy, no replacement without independently qualified complete closure witness |
| `tests/runtime/release_1_15/test_lifecycle_policy.c` | Deterministic policy tests with simulated time/failures; no witness => no replacement |
| `tests/runtime/release_1_15/fixtures/ResearchFixture.csproj` | Standalone net10.0 Web SDK fixture; no new package or production project edits |
| `tests/runtime/release_1_15/fixtures/Program.cs` | Loopback C# health and truthful fixture/unavailable responses only; no ingestion, SQLite, algorithms or production snapshot/HMAC |
| `tests/runtime/release_1_15/fixtures/public_app.py` | Existing-pinned Streamlit fixture, explicit disconnected/no production chart claim |
| `tests/runtime/release_1_15/fixtures/adversary.c` | Bounded fake-secret-only child modes for setsid/fork/credential/FD/proc/escalation/failure cases inside owned containers |
| `tests/runtime/release_1_15/test_image_boundary.py` | Independent final-image observations, fixed owned resource IDs, strict deadlines/output bounds; no supervisor-log-only death claim |
| `eng/docker/release-1.15/Invoke-Wp02Qualification.ps1` | Windows PowerShell5.1-compatible runner, loopback-only host port, bounded builds/tests, digest/evidence collection, cleanup of confirmed label/ID-owned resources |

Documentation scope for this WP02 work:

| Literal path | Purpose |
| --- | --- |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md` | New now: literal proposal and approval gate |
| `docs/roadmap/release-1.15/RELEASE_1.15_WP02_EVIDENCE.md` | New now: actual preflight, failures and unexecuted qualification |
| `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md` | Append-only WP02 index |
| `docs/roadmap/release-1.15/prompters/release-1.15-wp02-codex-docker-dual-process-runtime-and-isolation.md` | Preserve owner-untracked executed authority; normalize only UTF-8/LF |

Approve/amend the ten source paths, four documentation paths and proposed native single-threaded C approach before implementation. Native language/toolchain is a new proposed choice, not retroactively inferred from WP01. Existing SDK/compiler/header availability and offline base/dependency assets must be verified before build; do not assume a C compiler exists. Any new compiler package, base image, dependency, helper or extra path requires an amendment, not silent installation.

## Proposed implementation sequence

1. Verify approved paths, digest-pinned offline assets and compiler/headers. Record actual toolchain/kernel/runtime versions; stop on missing prerequisites. Build an explicitly selected candidate only, never replace default image/startup. Fixture labels remain visible in UI/API/evidence.
2. Single-threaded PID1 validates its dedicated PID namespace, complete proc/thread visibility, role UID reservation, x86-64 ABI, outer seccomp/LSM policy and pidfd/signal/wait facilities. Bootstrap may use only accepted transient CHOWN/FOWNER/DAC_OVERRIDE/SETUID/SETGID/SETPCAP/KILL; no privileged/host-PID/broad cap-add fallback or unknown mount repair.
3. Establish no-symlink `/private` ownership0700 and sensitive fake/config files0600/umask0077; disjoint role scratch. Reduce PID1 effective/permitted/bounding to exactly SETUID/SETGID/SETPCAP/KILL, empty ambient/inheritable and no_new_privs. Trusted root can impersonate research: its compromise is outside the role confidentiality claim.
4. Fixed fork/exec launches sessions, clears supplementary groups, all capability sets/bounding and inherited FD>=3; all real/effective/saved UID/GID values12001 research or12002 public. FD0=/dev/null, bounded per-role stdout/stderr pipes; empty-map environment allowlists and private config only. Verify dumpability/proc denials after exec, not merely before credential changes. Apply accepted child seccomp restrictions: architecture/x32 rejection, namespace clone flags, unshare/setns/ptrace/process_vm denial, clone3 ENOSYS; actual runtime fallback must pass or fail closed.
5. Research binds127.0.0.1:52731, Streamlit0.0.0.0:8501 internally. Host publishes UI only on127.0.0.1 using an unused port; no raw/admin/public research route. Loopback allows transformed-query access, not role authentication. Offline fixture does not implement WP03-WP06.
6. Use accepted90s startup,30s health/2s probes/three failures and10s TERM+5s closure. One exclusive wait owner coordinates direct/adopted children; pidfds do not create waitability or signal permission. Bounded census covers threads/task identity, max256, PID-reuse ambiguity or denial fails closed. Log draining cannot block lifecycle deadlines.
7. Keep same-role replacement DISABLED until complete descendant closure is independently qualified. Empty group/two UID scans are screening only. Failure leads to bounded ContainmentFailed/shutdown; uncertain ownership requires bounded peer shutdown and nonzero container exit. Test backoff policy deterministically without enabling an unqualified restart branch. Any alternative witness/helper/cgroup proposal requires review. Deployment overlap and storage writer exclusion stay WP03/WP07.

## Tests and run-only impact

Run-only preservation/regression references (no edits): `tests/feasibility/release_1_15/runtime/test_supervisor.py`, `tests/feasibility/release_1_15/runtime/supervisor.py`, `tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj`, `tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj`, `python/presentation/test_public_runtime_isolation.py`. Verify paths/prerequisites and select proportionate checks after implementation; no broad requalification or restore is inferred when production paths stay unchanged. Historical tests prove only their historical fixture.

- Deterministic policy: legal/illegal transitions,90s grace,three failures,10+5s stop,1/2/4/8/16/30s backoff,5min reset,10failures/10min budget, queue/counter bounds, peer independence and failed census/signal/wait/no-witness rejection.
- Final-image negatives: independent PID1/child/thread credential/groups/capsets/no_new_privs/seccomp/FD/env/socket/file checks. Public UID denied fake DB/sidecars/checkpoints/watermark/config/key traversal/reads and research process channels; no secret in public argv/env/logs/assets/errors. Check setuid/file-capability escalation and namespace attempts with bounded fake fixtures only.
- Lifecycle: root-child failure, setsid/fork escape pressure, concurrent exits, launch interruption, PID reuse/attribution uncertainty, failed census/signal/wait/missing kernel facility, peer health and container TERM. Independent observer records task identities/death, not just supervisor logs. Finite scans do not prove universal closure; absent a qualified witness, restart remains disabled.
- Bounded screening: explicit Linux/amd64,1CPU/512MiB/64PIDs, per-case/suite deadlines and capped logs. Record source/image/base digests, commands, versions/kernel/profile, counts/failures, startup/shutdown timing/memory, and owned cleanup. No global prune/unrelated process termination. Local profile is not F1 evidence.

## Dependencies, risks and review exit

No package/project-wide build/CI/production application changes. Preserve Streamlit1.61.1, Plotly7.1.0 and net10.0 pins. Standalone fixture project consumes current root build policy. Native compiler/header/assets availability, child exec/dumpability, exact capset reduction, outer filter/thread compatibility and complete closure witness are unresolved. No image digest exists until an image is built. Docker access alone proves none of these.

WP03 owns ingestion/storage durability/recovery/exclusive writer; WP04 numerics; WP05 protocol/HMAC; WP06 UI; WP07 actual F1, deployment overlap and combined load; WP08 final acceptance. Accepted WP01 choices are not tests. Do not introduce provider/legal milestones, change D1-A or claim fixture completion as WP02 acceptance.

After literal-path approval: implement privilege boundary/policy first, then offline roles, final-image adversarial/lifecycle tests and evidence; submit non-draft review PR linked #368 only after substantive execution. Stop/report required amendments or security blockers. Never enable restart to manufacture a PASS. Current recommendation: **BLOCKED AT EXACT-PATH APPROVAL**, not demonstrated Linux infeasibility. No source/tests/build/native experiment/provider/Azure/registry/deployment execution yet. `PUBLIC_RUNTIME_VERIFIED=False`.

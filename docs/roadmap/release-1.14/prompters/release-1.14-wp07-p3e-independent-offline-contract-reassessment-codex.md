# Codex Authority — Release 1.14 WP07-P3E
## Independent Read-Only Security Reassessment of Corrected Native-Boundary Contracts

**Owner authorization:** WP07-P3E only. **Mode:** strictly read-only investigation. Do not infer authorization for repair, implementation, native qualification, commits, or deployment.

## Mission
Independently determine whether WP07-P3D's offline corrections close the four WP07-P3C findings without weakening other accepted lifecycle obligations. Report source-backed PASS/GAP/BLOCK judgments. Synthetic/static correctness is not Linux kernel evidence.

## Protected baseline and scope
- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Preserve every pre-existing dirty/untracked item, refs and local state.
- Review primary files, **without modifying them**:
  - `container/admin_lifecycle_native_boundary_contracts.py` — expected SHA-256 `0DDA0B31936AD4E96EBC665D804DBD84C5E3E6B7C2C01570FFE92D594061AA5A`
  - `python/presentation/test_admin_lifecycle_native_boundary_contracts.py` — expected SHA-256 `14012FEDF467E9144982C06CA125F81EB5AB823492ABB2589AB8DCA365ABB124`
- Read only relevant accepted launch-plan/protocol and existing documentation for comparison; do not modify any files.
- Prior P3C findings: (1) gate/ACK omitted when inventory incomplete; (2) completed-wait constructor bypasses deadline; (3) wait-before-disposal accepted by chronology ledger; (4) distinct required roles can share PID.
- P3D reported 27 focused tests passing; **do not rerun tests** under this authority. Do not treat that report as independent proof.

## Mandatory review work
1. **Verify baseline and preservation.** Capture initial branch, HEAD, refs, complete Git status (including untracked paths) and SHA-256 of both primary files. If hashes differ, STOP and report discrepancy; do not silently adapt the target. Capture final evidence and compare. Avoid any command that writes into checkout, caches, Git metadata or ignored paths.
2. **P3C-1 gate/ACK:** Trace all constructors, defaults, validators and transitions, including empty/partial inventories. Require mandatory agent-bootstrap gate provenance, `A_EXEC` and `A_EXEC_ACK` evidence, explicit gate/alias closure before agent exec, and preservation of RA3/metadata4. Verify conditional FD5 cannot be treated as a proved native allocation. Identify any bypass via alternative construction or missing history.
3. **P3C-2 wait timing:** Verify direct construction and every state transition enforce positive child-specific normal exit-zero, nonresetting absolute monotonic deadline, observation bounds, monotonic `last_now`, sticky failures and rejection of ECHILD, wrong PID, EOF, absence, signal/nonzero exit and notifier-only evidence. Inspect overflow/negative/equal-boundary behavior.
4. **P3C-3 chronology:** Reconstruct the actual event dependency graph and timestamp constraints, not merely label ordering. Confirm UI completion/RA closure, EOF→agent wait→guardian wait, host disposal→router wait, SR/SG closure and mandatory positive waits before adopted-child drain/terminal absence. Inspect the corrected fixture (reported disposal=14, waits=15/16/17, drain/absence=18/19) and test equal, early, future and absent prerequisites. Reconcile with one shared four-second shutdown deadline and five-second Docker stop grace as *design obligations*, not native proof.
5. **P3C-4 global bindings:** Validate unique positive A/G/R PIDs, correct numeric parent-child graph (guardian→agent; supervisor→guardian/router), exclusive required-child reaper, and absence of status misattribution. Inspect duplicate PID, wrong parent, swapped owner, missing role and constructor-bypass paths.
6. **Regression and integration:** Independently inspect all 27 test methods, assertions and fixtures for meaningful reason-specific negative coverage, false positives and untested paths. Compare offline contracts with accepted static launch-plan obligations, frozen 64-byte `aiq-admin-lifecycle-v1` codec, C1 ACK-versus-readiness separation, metadata once-validation/closure, readiness-before-`R_EXPOSE`, permanent UID/GID/capability drop, trusted/presentation session separation and no pre-fork secrets. Flag only genuine contract mismatches; do not demand native execution from an offline contract.
7. **Threat-model edge cases:** Analyze incomplete or contradictory injected inventories, alias cycles, live-endpoint closure, remap scratch uncertainty, timeout arithmetic, partial shutdown evidence, repeated/duplicate events, inconsistent launch/PID bindings and any accepted state whose prerequisites were never proved. Provide minimal conceptual counterexamples with file:line citations; **do not execute** them.

## Hard prohibitions
No edits, new files, patches, tests, Python imports/execution, builds, restores, dependency installs, generated caches, native syscalls/process experiments, Docker, Azure, network/fetch, Git mutation (checkout/reset/stash/clean/add/commit/push/PR/merge), or changes to frozen contracts. Do not select bootstrap language, ABI, libc, toolchain or image. No production/public-runtime activation.

## Required report
- A concise **P3C-1 through P3C-4 closure matrix** with PASS/GAP/BLOCK and exact file:line evidence, distinguishing reported P3D changes from independently inspected behavior.
- Other regressions or new acceptance bypasses ranked by severity, with explicit smallest counterexample and missing negative test; separate BLOCK from nonblocking future native-qualification GAP.
- Summary of 27 test-method inspection, including whether test count and intent are source-supported; clearly state no tests were run.
- Comparison to frozen protocol and static launch plan, including explicit static-vs-kernel evidence boundary.
- Initial/final protected-checkout evidence and primary file hashes; disclose limits of any hashing/snapshot method.
- Smallest safe separately authorized next increment, if needed, with exact path allowlist and STOP conditions. If accepted, recommend only a **separately authorized read-only** bootstrap-language/ABI feasibility investigation; no automatic implementation.
- End with exactly one verdict:
  - `WP07-P3E INDEPENDENT REVIEW ACCEPTED — OWNER DECISION REQUIRED`
  - `WP07-P3E BLOCKED — OWNER REVIEW REQUIRED`

`PUBLIC_RUNTIME_VERIFIED=False`. Production, public-runtime and Azure qualification remain **NO-GO**.

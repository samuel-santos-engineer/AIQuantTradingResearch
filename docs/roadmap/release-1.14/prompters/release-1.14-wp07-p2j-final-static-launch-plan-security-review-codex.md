# Codex Authority — Release 1.14 WP07-P2J
## Independent Final Static Launch-Plan Security Review (READ ONLY)

**Owner authority:** Conduct only this bounded, independent, read-only review. This is not approval to modify the repository, execute tests, allocate native FDs, implement runtime, commit, open PRs, merge, deploy, or change production gates.

### Baseline and protection
- Work on the existing intentionally dirty protected checkout. Do not checkout, reset, clean, stash, fetch, switch branches, or alter refs. Record initial and final branch, full HEAD SHA, refs and complete Git status. Inspect without mutation; compare hashes of relevant and Git-listed files when feasible, noting ignored-file limitations.
- Expected model SHA-256: `A85903BA0F5F199E84A5D89DEAC8E8975630903837C6DB35412A9095E0167525`.
- Expected test SHA-256: `DD1610E65CC6713DF158B854C1F6A3BAF568C59E19DBDF62E39733E86BA87019`.
- Last reported remote `main` merge: `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`; do not contact remote to refresh.
- `PUBLIC_RUNTIME_VERIFIED=False`; production, public-runtime and Azure qualification remain NO-GO.

### Primary inspection allowlist
- `container/admin_lifecycle_launch_plan.py`
- `python/presentation/test_admin_lifecycle_launch_plan.py`
- Read frozen protocol and existing lifecycle source **only as needed** to establish compatibility; do not edit any path.

### Independent review obligations
1. **Phase/trace reconciliation:** Independently inspect trace-derived phase ownership, checkpoint projection and `PHASE_TRACE_MISMATCH` rejection. Verify guardian metadata lifetime is consistent with actual closure before post-hardening. Look for contradictory phases accepted via missing checkpoints, aliases, or canonical-equality fallback.
2. **UI bootstrap versus UI exec:** Verify child-bootstrap inherits all actual parent endpoints, closes every unused copy, and reaches an explicit descriptor-free UI-exec boundary. Ensure RA, metadata, credentials, gate and aliases cannot survive UI exec; `UI_EXEC_ENDPOINT_LEAK` must reject leaks. UI readiness must follow exec.
3. **FD5 candidate:** Trace endpoint creation, parent possession, forks, inherited guardian metadata FD5 closure, occupancy/vacancy check, and proposed transient agent-bootstrap gate assignment. Verify no live endpoint is overwritten and agent FD3 RA / FD4 metadata remain unchanged. Guardian FD4 remains its separate gate endpoint. FD5 is still **conditional**, not authorized for native allocation.
4. **Frozen metadata and C1/C2/C3:** Verify positive symbolic PID bindings precede valid emission/validation; preparation is not emission; metadata stream ordering and role/subject restrictions match the frozen protocol. Check ACK-before-exec, explicit gate and alias closure after ACK and before agent exec, CLOEXEC as backstop, agent FD4 metadata survival then one-time validation/close, and postexec `A_READY`.
5. **Shutdown and kernel-proof boundaries:** Review RA/SR/SG ownership/closure through UI_STOPPED, permitted EOF, R_PROTOCOL_DONE, HOST_DISPOSED, guardian agent wait, AGENT_RESULT, supervisor guardian/router waits, adopted-child drain and terminal absence. Require independent actual positive bound exit-zero waits as obligations; do not treat labels, EOF, ECHILD or absence as kernel proof. Preserve one shared 4-second monotonic shutdown deadline, 5-second Docker grace and frozen startup deadlines.
6. **Identity, session and secrecy:** Confirm canonical topology, parentage, UID/GID constraints, session separation, permanent hardening, secret-free pre-fork and router-local credential ordering remain immutable static obligations. Do not claim they are runtime qualified.
7. **Tests:** Inspect all 42 test methods and helper behavior **without running them**. Evaluate six P2I negatives and prior tests for genuine semantic rejection, mutation paths, reason-specific assertions, and missing bypass cases. Cite exact file/line evidence for any deficiency.
8. **Cross-contract assessment:** Distinguish (a) source-supported static invariants, (b) declarations only, (c) missing model checks, and (d) actual native evidence not available. Do not infer runtime readiness from 42 previously reported passing tests.

### Required report
- Provide a concise PASS/GAP/BLOCK matrix with file/line citations and a concrete explanation of every finding.
- Give an explicit decision whether the **complete static model** is acceptable and whether FD5 is ready for a **separate owner numeric design decision**, not implementation.
- Identify the smallest next bounded increment (or owner decision), exact files, validation, STOP conditions; do not initiate it.
- Record baseline SHA checks, initial/final Git preservation evidence, and that no tests, builds, network, native operations, commits, PRs, Docker or Azure actions occurred.
- End with **exactly one** verdict:
  - `WP07-P2J REVIEW ACCEPTED — OWNER DECISION REQUIRED`
  - `WP07-P2J BLOCKED — OWNER REVIEW REQUIRED`

### Immediate STOP conditions
Unexpected baseline hashes, any need to mutate files or refs, a frozen protocol/topology/descriptor-map revision, unresolvable ownership or inheritance inconsistency, need for native execution, or any action outside this read-only authority. Report the blocker; do not self-authorize a fix.

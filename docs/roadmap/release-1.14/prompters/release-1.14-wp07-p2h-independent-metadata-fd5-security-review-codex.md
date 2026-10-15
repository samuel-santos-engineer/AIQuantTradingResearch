# Codex Authority — Release 1.14 WP07-P2H
## Independent Metadata-Binding, Endpoint-Handoff and FD5 Security Review

**Authority type:** READ-ONLY inspection and written assessment. **No implementation authority.**

### Context and immutable boundaries
- P2G reports 36/36 focused Python tests passing (exit 0), but independent acceptance is outstanding.
- Protected intentionally dirty checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Preserve branch, HEAD, refs, index, worktree and unrelated files. Do not checkout, reset, stash, clean, fetch, commit, push, open PR or merge.
- Expected P2G SHA-256: `container/admin_lifecycle_launch_plan.py` = `8B5FF7F07222DF99212CCC23D27627A13CBD3B981D41EF2573BF6A2E422F62C9`; `python/presentation/test_admin_lifecycle_launch_plan.py` = `DA884689AE8727A35391D1C9E9AB8DEFDC0E5203F1DD400B53273A9A624CE5D1`. Verify before review; STOP on mismatch.
- Merged frozen offline protocol is reference-only, including `container/admin_lifecycle_protocol.py` and relevant tests. No changes to frozen protocol, role graph, PID bindings, metadata semantics, C1/C2/C3, conditional P2-C4, descriptor maps or timing/wait contracts.
- Proposed agent-bootstrap gate **FD5 remains conditional**, not approved for native allocation. Agent FD3 RA and FD4 metadata retain their approved lifetimes; guardian FD4 is its distinct gate endpoint.
- `PUBLIC_RUNTIME_VERIFIED=False`; public and production gates remain NO-GO.

### Permitted actions
Read source, tests, protocol and local documentation as needed. Run read-only Git status/ref/hash inspection and source-level reasoning. **Do not execute tests, builds, restore, Python imports, native probes, Docker, Azure, network calls or commands that modify files or caches.** Do not write review artifacts to the repository. Return findings in the Codex response only.

### Independent review obligations
1. **Metadata protocol compatibility:** Trace root/router/guardian/agent fork-return PID establishment, metadata preparation, emission and validation. For every emitted record establish positive sender and subject PID bindings and correct record order, including `META_AGENT`. Distinguish a prepared template from a valid emitted record; compare against actual frozen protocol validators, not only canonical trace equality.
2. **Endpoint provenance:** Trace creation, transfer and possession by each parent before fork, and enumerate *all* inherited child copies, not only retained subsets. Identify any implicit disappearance, invented ownership or missing close transition. Confirm root pre-fork remains secret-free and router-only credential generation remains after separation.
3. **FD5 safety:** Examine guardian metadata FD5 at the agent fork, its agent-side inherited copy, explicit child-side closure and proof of vacant agent-bootstrap FD5 before gate assignment. Reject overwrite/remap of live endpoints; verify RA FD3 and metadata FD4 unchanged. Inspect alias creation, duplicated ends, explicit pre-exec closure, CLOEXEC as backstop and UI zero-inheritance.
4. **Ordering and shutdown:** Validate `A_EXEC_ACK` before gate closure/exec, metadata FD4 postexec one-time validation/close, `A_READY` only after exec, readiness/exposure ordering, shutdown endpoint ledger and D1/D2 independently positive bound waits before T2 drain/absence. Preserve single 4-second CLOCK_MONOTONIC shutdown deadline, 5-second Docker grace, 5-second privileged bootstrap and 30-second total startup deadlines. Treat all waits as symbolic obligations, never actual kernel proof.
5. **Test audit:** Inspect all 36 test methods, especially six P2G negatives. Determine whether each relevant mutation is rejected for its intended semantic reason rather than only by exact canonical comparison. Identify missing or bypassable cases, including invalid/missing PID bindings, early metadata emission/validation, parent-unavailable endpoints, unclosed inherited copies, occupied FD5, alias survival and shutdown closure.
6. **Regression and boundaries:** Confirm no accidental revision to topology (S→R/G→A→UI), identities/sessions, hardening, frozen `aiq-admin-lifecycle-v1` 64-byte protocol or descriptor maps. Clearly separate model assertions from actual OS enforcement.

### Evidence and reporting
- Cite exact repository paths and line numbers for each PASS, GAP or BLOCK finding; explain exploitability or consequence of each gap without claiming runtime exploitation.
- Record initial/final full Git status, branch, HEAD, local refs and hashes of the two P2G files; compare other Git-listed files when feasible. State limits of ignored-file coverage. No remote recheck if network access is forbidden.
- Give a phase-by-phase handoff/FD5 trace and metadata binding/emission trace, plus shutdown closure sequence.
- If blocked, propose **the smallest separately authorized offline correction** with exact paths, test expectations and STOP conditions. If accepted, propose precise wording for the next owner decision concerning FD5; **do not independently freeze FD5 or authorize native implementation**.

### Mandatory verdict (choose exactly one)
**`WP07-P2H REVIEW ACCEPTED — OWNER DECISION REQUIRED`**

or

**`WP07-P2H BLOCKED — OWNER REVIEW REQUIRED`**

Stop on unexpected changes, hash mismatches, frozen-contract changes, need for an additional file or native execution. Do not proceed automatically to a new phase. End with `PUBLIC_RUNTIME_VERIFIED=False` and all production gates **NO-GO**.

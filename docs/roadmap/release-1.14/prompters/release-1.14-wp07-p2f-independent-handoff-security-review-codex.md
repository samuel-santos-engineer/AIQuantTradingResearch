# Codex Authority — Release 1.14 WP07-P2F

## Independent Read-Only Endpoint-Handoff and Shutdown Security Review

**Owner authorization:** Perform a bounded, independent **read-only** review of the WP07-P2E model after the WP07-P2E-F1 test-helper correction. This authority does **not** accept P2E, freeze FD5, or authorize native implementation.

## Protected baseline

- Protected checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`; preserve its existing dirty state, branch, HEAD, refs, index and worktree.
- Remote `main` last reported at PR #331 merge `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`. Do not fetch or change refs to confirm it.
- Expected P2E model SHA-256: `4A95E8F4C4473BE8A0F49417DFAD5AA56CFA75F34DEC5E65304B08C142A22CAB`.
- Expected P2E-F1 test SHA-256: `A9492E7A5EEB6F4FFA547B3207D0483F3FD4C02E2A6AB2390657AC3EC03DBC0A`.
- Codex reported 30/30 offline tests passed, exit 0, after correction. Treat as reported evidence, **not** native proof.
- `PUBLIC_RUNTIME_VERIFIED=False`; production/public runtime remains **NO-GO**.

## Permitted inspection

Primary files (read only):

1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

Read frozen protocol, existing security contracts, and legacy implementation files **only as necessary for cross-checks**. No changes to any path. No tests, builds, restores, process/FD experiments, native operations, Docker, Azure, network, GitHub writes, commits, PRs or merges. Do not clean, stash, reset, checkout, fetch or otherwise disturb the protected checkout.

## Required review

1. **Baseline and preservation:** Record initial/final complete Git status, branch/HEAD/refs and both SHA-256 values. If expected baselines differ, STOP and report. Record any limitations of file hashing, including ignored files.
2. **Endpoint provenance and handoff:** Independently reconstruct creation, possession, transfer, fork and closure for each logical endpoint. Verify every child-inherited endpoint is owned by the correct parent at the relevant boundary; detect impossible creation, missing transfers, duplicate ownership and invalid phase ordering. Distinguish modeled declarations from validator-enforced consistency.
3. **Conditional FD5 occupancy:** Evaluate whether agent-bootstrap FD5 is provably free before gate assignment, including guardian metadata FD5, inherited aliases and remap/dup hazards. Keep agent RA FD3 and metadata FD4 unchanged; guardian gate FD4 remains separate. Identify every unresolved condition; **do not freeze or allocate FD5**.
4. **Gate/exec/UI closure:** Trace `A_EXEC`, `A_EXEC_ACK`, explicit closure of gate FD5 and every alias before agent exec, CLOEXEC backstop, surviving agent FD3/FD4, one-time metadata validation/closure, and zero UI inheritance. Check that CLOEXEC alone cannot satisfy explicit-close obligations.
5. **Shutdown ledger:** Review RA, SR, SG and other endpoint ownership/closure snapshots through STOP, UI completion, router host disposal, D1 guardian's positive bound kernel wait for agent exit zero, D2 supervisor's independent positive bound kernel wait for guardian exit zero, supervisor's router wait, and T2 adopted-child drain/terminal absence. Reject EOF/ECHILD/absence or reports as substitutes for mandatory positive waits. Verify one shared 4-second CLOCK_MONOTONIC shutdown deadline and 5-second Docker grace remain obligations, not runtime proof.
6. **Test security review:** Inspect all 30 tests and helper behavior without executing them. Determine whether negatives truly mutate validator inputs and fail for the intended reason. Specifically assess missing-parent handoff, phase order, duplicate creation, inherited FD5 occupied, overwritten live endpoint, gate aliases, CLOEXEC-only, shutdown retained aliases, session alias and premature `R_EXPOSE`. Identify gaps with precise line references.
7. **Contract compatibility:** Check P2-C2 (agent FD4 metadata postexec), P2-C3 (separate transient gate), conditional P2-C4 (FD5 candidate), C1 launch gate, T9–T11 protocol/metadata, identity/session and credential-order restrictions. Do not silently amend frozen maps, message formats or trust boundaries.
8. **Next bounded recommendation:** State whether P2E's **offline static model** can be accepted, whether conditional FD5 is sufficiently modeled for a separate numeric owner decision, and what remains unqualified. If blocked, propose the smallest correction with exact path allowlist, tests and STOP conditions. If accepted, propose the next **separately authorized** step without assuming native/runtime authority.

## Mandatory STOP conditions

Stop and report without mutation if baseline hashes differ, required evidence is unavailable, any apparent frozen-contract conflict requires owner resolution, or review would require editing, running tests/native operations, adding files, changing Git state or expanding scope. Do not treat an internally consistent canonical table as independent kernel proof.

## Required output

Begin with exactly one verdict:

- `WP07-P2F REVIEW ACCEPTED — OWNER DECISION REQUIRED`
- `WP07-P2F BLOCKED — OWNER REVIEW REQUIRED`

Provide a source-linked findings matrix (PASS / GAP / BLOCK), explicit FD5 inheritance and shutdown traces, test coverage findings, precise owner decision proposal, expected and observed hashes, initial/final preservation evidence, and clear distinction among **static validation**, **native qualification**, and **public runtime qualification**. No automatic implementation or PR.

`PUBLIC_RUNTIME_VERIFIED=False`; all production gates **NO-GO**.

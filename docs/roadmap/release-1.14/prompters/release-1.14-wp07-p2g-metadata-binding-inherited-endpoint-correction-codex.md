# Codex Authority — Release 1.14 WP07-P2G
## Offline Metadata Binding and Inherited-Endpoint Closure Correction

**Owner authorization:** WP07-P2G authorized. This document authorizes only the bounded offline correction below. It does not authorize acceptance of P2E, numeric/native FD5 allocation, runtime integration, commits, PRs, merges, deployment, or production activation.

### Starting state and mandatory preservation
- Protected intentionally dirty checkout: `draft/release-1.14-wp07-partial-public-disabled`, expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Record actual initial state; **STOP** on unexpected divergence rather than repairing it.
- Expected model SHA-256: `4A95E8F4C4473BE8A0F49417DFAD5AA56CFA75F34DEC5E65304B08C142A22CAB`.
- Expected tests SHA-256: `A9492E7A5EEB6F4FFA547B3207D0483F3FD4C02E2A6AB2390657AC3EC03DBC0A`.
- Last reported remote main `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`; do not fetch or contact remote to recheck.
- Preserve frozen `aiq-admin-lifecycle-v1` protocol, C1, P2-C2, P2-C3, conditional P2-C4, role graph, UID/GID and session separation, descriptor maps, deadlines, D1/D2/T2 wait obligations, credential sequencing, and `PUBLIC_RUNTIME_VERIFIED=False`.
- Agent FD3 RA, FD4 metadata survive exec (FD4 validates once and closes). Agent-bootstrap FD5 remains **proposed/conditional**, temporary gate only, not approved for native allocation. Guardian FD4 remains its own gate endpoint.

### Exact write allowlist
1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

Read frozen protocol files and local legacy files only as needed for source-backed comparison. Do not modify any other file, Git metadata, or refs. No checkout, reset, stash, clean, fetch, package install, Docker, Azure, native process/FD/privilege actions, commits, PRs, or merges.

### Required corrections
1. **Binding before valid metadata:** Distinguish preparation of an incomplete metadata template from actual emission and validation of frozen metadata records. Model establishment of relevant positive sender/subject PID bindings through the required fork/parentage events **before** valid metadata emission. In particular, root router/guardian metadata must not be emitted before their required child bindings exist; guardian metadata including `META_AGENT` must not be emitted or validated before the agent PID is established. Do not invent placeholder PIDs or loosen the frozen protocol.
2. **Consistent endpoint acquisition and transfer:** Derive child inheritance from endpoints actually held by its parent at the fork boundary. Model endpoint creation, transfers, retention, duplication, and explicit closure as distinguishable transitions; reject missing-parent handoffs and silently disappearing inherited endpoints.
3. **Inherited FD5 occupancy:** Reconcile the corrected metadata timeline with guardian FD5 metadata ownership. If guardian FD5 is live at agent fork, represent its inherited agent-bootstrap copy and explicit safe closure before the proposed agent FD5 gate assignment. Reject occupied-slot overwrites, alias leakage, and any remapping or loss of agent FD3 RA / FD4 metadata. Preserve guardian FD4 gate endpoint and the separate gate lifetime: `A_EXEC_ACK` then explicit gate/alias closure before agent exec, with CLOEXEC as a backstop.
4. **Exec, UI, and shutdown:** Preserve the exact postexec FD3/FD4 semantics, metadata validate-once/close behavior, no UI control-FD inheritance, and phase-specific shutdown ownership/closure tied to mandatory bound-wait and disposal milestones. No EOF/ECHILD/absence as positive exit-zero proof.
5. **Semantic validation:** Add explicit reason-specific checks for binding-before-emission, parent possession, inherited-copy closure, FD5 vacancy, and closure ordering. A negative case must not pass solely because a final canonical-table equality check rejects an arbitrary mutation.

### Focused offline tests
- Retain all 30 existing test intentions and assertions. Add negative tests for metadata emission/validation before PID establishment; missing/invalid PID bindings; missing inherited-copy close; inherited FD5 still occupied at gate assignment; overwriting a live endpoint; and missing parent ownership at fork.
- Verify the intended semantic rejection path (including reason-specific `PlanError` where feasible) and positive canonical trace acceptance.
- Run only: `python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v`.
- Require all tests to pass, exit 0. On any failure, stop and report; do not expand scope or weaken tests.

### STOP conditions
Stop and request owner review if correction requires any third write path, a frozen metadata/protocol revision, changed topology or descriptor maps, an alternative numeric gate FD, native execution, runtime integration, or bypass of a security obligation. Do not silently reinterpret protocol records or mark static labels as kernel evidence.

### Evidence and final report
- Record initial/final full `git status`, branch, HEAD, local refs, and hashes of both allowlisted files; compare non-allowlisted Git-listed file hashes before/after where practical. Do not disturb pre-existing dirty content.
- Explain the corrected chronological trace, exact PID-binding events, metadata prepare/emit/validate steps, parent endpoint possession, inherited FD5 clearance, gate closure, and shutdown ledger with file/line references.
- Report test command, individual new negative tests, totals and exit code; distinguish model assertions from actual kernel proof.
- End with exactly one verdict: **`WP07-P2G OFFLINE CORRECTION COMPLETE — OWNER REVIEW REQUIRED`** or **`WP07-P2G BLOCKED — OWNER REVIEW REQUIRED`**.
- Keep `PUBLIC_RUNTIME_VERIFIED=False`; production/public runtime remain **NO-GO**. No automatic progression to P2E acceptance or FD5 numeric/native allocation.

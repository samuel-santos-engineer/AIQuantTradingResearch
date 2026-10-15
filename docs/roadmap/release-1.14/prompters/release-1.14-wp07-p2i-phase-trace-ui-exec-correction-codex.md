# Codex Authority — Release 1.14 WP07-P2I

## Phase/Trace Reconciliation and Descriptor-Free UI Exec — Offline Correction

**Owner authorization:** WP07-P2I is explicitly authorized. This authority is limited to the correction below; it is not permission to implement or qualify a runtime.

## Protected baseline and prerequisites

- Work in the existing intentionally dirty checkout. Record initial and final branch, HEAD, local refs, full `git status`, and file hashes. Do not checkout, reset, clean, stash, fetch, commit, push, open PRs or merge.
- Expected initial SHA-256: `container/admin_lifecycle_launch_plan.py` = `8B5FF7F07222DF99212CCC23D27627A13CBD3B981D41EF2573BF6A2E422F62C9`; `python/presentation/test_admin_lifecycle_launch_plan.py` = `DA884689AE8727A35391D1C9E9AB8DEFDC0E5203F1DD400B53273A9A624CE5D1`. **STOP** if either differs; do not overwrite unexpected work.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False`, disabled Option A and all production/public-runtime **NO-GO** gates.
- Frozen topology: finite secret-free root PID1 bootstrap; S directly parents R/G; G directly parents A; A parents UI. Preserve distinct permanent S/G/R identities, A/UI UID1000, sessions, hardening, credential timing, deadlines and D1/D2/T2 positive-bound-wait obligations.
- Preserve frozen `aiq-admin-lifecycle-v1` wire protocol and metadata PID-binding/order requirements; preserve C1 launch ACK versus postexec readiness, P2-C2 agent metadata FD4, P2-C3 separate gate, conditional P2-C4 agent-bootstrap FD5. FD5 is **not frozen for native allocation**.
- Fixed process-local maps: S FD3/4/5, G FD3/4/5, R FD3/4/5/6; A FD3 RA and FD4 metadata across exec (validate once, then close); proposed transient agent-bootstrap gate FD5, explicitly closed with all aliases after ACK and before agent exec. UI must execute with no inherited control/credential/metadata/gate FDs.

## Exact modification allowlist

1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

No other file may be created, modified, deleted or formatted. Inspect existing frozen protocol/reference files read-only if needed. No dependencies, native FD operations, forks, identity changes, Docker, Azure or network calls.

## Required correction

1. **Reconcile phase declarations and canonical trace.** Fix the P2H contradiction: guardian metadata is declared owned at `post_hardening` but is closed before that milestone in the trace. Prefer trace-derived phase ownership or a validated projection; if keeping independent declarations, enforce bidirectional consistency at each relevant boundary. Do not merely modify both constants to match without a semantic consistency check. Preserve valid metadata emission and validation order from P2G.
2. **Model UI child-bootstrap separately from UI exec.** The UI child may transiently inherit agent RA or other open endpoints immediately after fork. Represent the complete inherited snapshot, explicit closure of each unwanted copy, and an unambiguous UI-exec boundary. Validate that the UI-exec survivor set contains **no** control, credential, metadata, launch-gate or aliases. Do not equate a clean post-fork snapshot with a clean exec boundary.
3. **Keep endpoint provenance and FD5 conditionality intact.** Preserve full parent-possession/fork-copy accounting, guardian metadata FD5 inherited-copy closure in agent bootstrap, vacancy check before gate assignment, unchanged A FD3/FD4 and G FD4 lifetimes, and explicit pre-agent-exec gate/alias closure plus CLOEXEC backstop. No remapping or replacement FD proposal.
4. **Preserve shutdown and protocol obligations.** Retain closure ledgers, `UI_STOPPED` and subsequent RA/SR/SG sequencing, shared four-second shutdown deadline, required positive bound kernel-wait obligations and terminal absence only after waits and drain. Model labels remain non-kernel evidence.
5. **Strengthen semantic validation.** Reject phase/trace disagreements and any UI-exec control endpoint leak for specific reasons, not solely a final equality check against a canonical fixture. Preserve immutable/frozen model behavior.

## Required focused tests and validation

- Preserve all **36 existing test intentions**; do not delete, skip, weaken, or replace assertions with tautologies.
- Add negatives that mutate guardian metadata lifetime/trace independently in either direction; verify a specific phase/trace mismatch rejection.
- Add negatives for UI-exec with inherited RA, metadata, credential, gate or alias; missing UI child-bootstrap close; premature or misordered closure; and cases where fork inheritance is legitimate but exec inheritance is not.
- Retain tests for metadata PID establishment/emission, FD5 vacancy, complete inherited-copy accounting, C1/C2/C3, deadline and shutdown obligations.
- Run exactly the focused offline suite: `python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v`. Require exit 0 and report test count and individual negative-test evidence. No broader tests/builds without new authority.
- Record before/after SHA-256 of both allowlisted files and verify non-allowlisted tracked files and protected Git state unchanged. If a test fails, a frozen contract must change, an additional path is needed, or any native execution becomes necessary: **STOP** and report, without expanding scope.

## Completion report

Report concise source line evidence for: phase/trace reconciliation; guardian metadata lifetime; UI child-bootstrap and UI-exec survivor validation; FD5 vacancy/alias closure; unchanged shutdown obligations; negative tests; test command/exit; before/after hashes; Git preservation and limitations.

Use exactly one verdict:

- **`WP07-P2I OFFLINE CORRECTION COMPLETE — OWNER REVIEW REQUIRED`** if all gates pass.
- **`WP07-P2I BLOCKED — OWNER REVIEW REQUIRED`** otherwise.

Do not claim independent security acceptance, numeric FD5 freezing, native descriptor closure, kernel confinement, bound wait proof or public-runtime qualification. `PUBLIC_RUNTIME_VERIFIED=False`; production/public-runtime **NO-GO**.

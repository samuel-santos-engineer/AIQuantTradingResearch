# Codex Authority — Release 1.14 WP07-P2E-F1

## Purpose and authorization
Owner requests a narrowly bounded follow-up to the blocked WP07-P2E validation. This authority permits **only a test-helper correction** to resolve the two Python unittest errors caused by duplicate `operation` or `owner` arguments in `trace_mutation(operation, owner, **changes)`. It does **not** accept P2E, approve native FD5 allocation, or authorize any runtime integration.

## Protected starting state
- Protected dirty branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Remote `main` reported at PR #331 merge `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`. Do not fetch or alter refs.
- Expected P2E baseline SHA-256:
  - `container/admin_lifecycle_launch_plan.py`: `4A95E8F4C4473BE8A0F49417DFAD5AA56CFA75F34DEC5E65304B08C142A22CAB`
  - `python/presentation/test_admin_lifecycle_launch_plan.py`: `48816EF98D87C31056F6DED06C626F3FB2E759AC40CD29168183E6159C4E7C9B`
- Preserve all existing dirty/untracked state. Record full Git status, branch, HEAD, refs and relevant hashes before and after. If baselines differ, **STOP** and report.

## Exact allowlist
**Only modify:** `python/presentation/test_admin_lifecycle_launch_plan.py`.

**Read-only reference:** `container/admin_lifecycle_launch_plan.py` and frozen protocol/security contract files as needed. Do not edit the model or any other path. Do not change generated artifacts or dependencies.

## Required correction
1. Inspect the exact two errors from the 30-test run. Confirm they result from test-helper selector parameters colliding with the `operation` and `owner` mutation keys, not from model validation.
2. Rename the helper selector parameters to unambiguous names (for example `target_operation`, `target_owner`) and adjust only necessary helper call sites. Preserve the ability to mutate model fields named `operation` and `owner` through `**changes`.
3. Preserve all 30 test intentions, including the 23 pre-P2E tests and seven P2E tests. Do not delete, skip, xfail, weaken assertions, catch-and-ignore exceptions, or modify canonical model data to force a pass.
4. Ensure the two previously errored negative cases actually reach the model validator and demonstrate rejection of the intended invalid `operation` and `owner` mutations. Distinguish assertion success from a helper-level exception.
5. Keep P2-C2/P2-C3/P2-C4 constraints unchanged: agent FD3 RA, FD4 metadata surviving exec then one-time validation/closure, proposed transient FD5 gate with explicit pre-exec closure and CLOEXEC backstop; FD5 remains conditional, not authorized for native allocation.

## Permitted validation
Run only:

```powershell
python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v
```

Require **30 tests passed, exit 0**, with the two previously errored tests explicitly identified as passing the intended negative checks. No other tests, builds, restores, Docker, Azure, networking or runtime process/FD/privilege experiments. If tests fail, **STOP**, report exact output and do not broaden scope.

## Non-negotiable prohibitions
No checkout, reset, stash, clean, branch switching, fetch, commit, push, PR, merge, GitHub mutation, Docker/Azure operation, credential handling, runtime bootstrap/agent/router changes, native descriptor operations, process creation, privilege changes, or changes to the frozen 64-byte protocol, identities, deadlines, kernel-wait obligations or production configuration.

## Required final report
- Verdict: `WP07-P2E-F1 TEST-HELPER CORRECTION COMPLETE — OWNER REVIEW REQUIRED` **or** `WP07-P2E-F1 BLOCKED — OWNER REVIEW REQUIRED`.
- Exact one-file diff summary and why the helper collision is resolved.
- Test command, count, exit code and evidence that both affected negative tests exercised validator rejection.
- Before/after SHA-256 for the test file; unchanged model SHA-256; before/after Git status/HEAD/refs and unrelated-file preservation evidence.
- Any remaining P2E model or handoff risks. Do not claim static tests prove native descriptor closure or kernel waits.
- `PUBLIC_RUNTIME_VERIFIED=False`; production and public runtime gates remain **NO-GO**.

**STOP after reporting.** Further P2E acceptance, independent review, FD5 native allocation and implementation require separate owner authority.

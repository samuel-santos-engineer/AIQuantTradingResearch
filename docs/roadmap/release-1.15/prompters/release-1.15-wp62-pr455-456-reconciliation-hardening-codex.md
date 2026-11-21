# Codex prompt — Release 1.15 WP62: reconcile PR #455 and harden PR #456 offline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Date: 2026-11-15

## Objective and authority

Reconcile documentation PR [#455](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/455) with current `main`, preserving all WP62-B evidence, then strengthen the **offline** Windows/process-safety qualification of stacked helper PR [#456](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/456). Do not merge either PR. Leave both open for owner review. Do not run live `--apply` or perform any Azure/registry/staging operation.

Known reference points (verify fresh, do not assume still current): #455 head `618921017122b7428b288931842683e82042250e`, base `main` at `c4513aebfb69afac9ecbc7633e79e69cae9e950a`; #456 head `02fc2877c443e4b48d7dac31714b429886f89a0a`, base branch `docs/release-1.15-wp62b-bounded-observation`. #455 was reported nonmergeable; #456 mergeable relative to its stacked base. Diagnose actual current merge status and cause before touching branches. Use safe, isolated working trees; preserve any dirty local owner checkout and private files. Do not force-push or rewrite unrelated history. If reconciling #455 requires rewriting its history or creates an ambiguous conflict, stop and request owner direction instead of improvising.

## Part A — PR #455 reconciliation

1. Read current `main`, both PR heads/bases, compare commits, actual diff, and any conflict details using read-only Git/GitHub operations.
2. Reconcile #455 against `main` **only if needed**, keeping its WP62-B observations, provenance, timestamps, quota reset ambiguity, management-event evidence, artifact preparation/publication/manifest verification, and proposed research-only staging plan intact. Do not overwrite or omit existing Release 1.15 evidence from main. Keep the document's status flags unchanged: `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
3. Preserve documentation-only scope for #455. Validate one-file intended scope, links, tables, UTF-8/LF/no BOM, and `git diff --check`.
4. Update #456's stacked base only after #455 is reconciled and verified; ensure the PR #456 diff against #455 remains exactly the intended helper, tests and narrow evidence addition. Do not accidentally duplicate the full #455 evidence in #456's effective diff.
5. Keep both PRs open/unmerged. Do not change PR base branches unless necessary to preserve correct stacking, and explain any such change.

## Part B — offline hardening of PR #456

Review the exact code in `eng/azure-cli/r1.15-qualification/wp62_staging_observation.py` and its tests. Preserve plan-only default and exact-target operation allowlist. Address these risks with the smallest auditable changes:

**Owned CLI process-tree cleanup:** `capture()` currently kills the immediate child on timeout/overflow but does not establish descendant cleanup. On Windows, implement a bounded, fail-closed strategy for owned descendant processes (e.g. an owned Windows Job Object or equivalent narrowly scoped ownership mechanism), without terminating unrelated processes. Cover timeout, combined stdout/stderr overflow, normal completion, and cleanup errors. Ensure pipe readers do not linger and no uncontrolled retry or background process survives. If robust descendant ownership cannot be established offline, keep live apply blocked and report the limitation; never claim Windows qualification based on mocked POSIX behavior.

**Windows ACL qualification:** Test the owner/SYSTEM-only private baseline directory ACL on a genuine local Windows environment **without Azure** and without accessing real retained private baseline/key/TLS files. Verify owner SID, inheritance protection, exactly intended access rules, and inheritance to synthetic recovery files. Ensure failure occurs before any private baseline write or Azure mutation. If Windows is unavailable, perform static review/mock tests only and explicitly report `WINDOWS_ACL_NOT_QUALIFIED`; do not simulate a passing real Windows check.

**CLI authentication and subprocess boundaries:** Inspect installed CLI invocation behavior offline only, without login, token reads, SCM endpoint calls, `az webapp log tail`, or Azure network access. Test command construction, bounded capture, output sanitization, timeouts, and operation allowlist with fakes. If actual SCM authentication/descendant behavior cannot be verified without live access, explicitly mark it UNKNOWN and block live apply; no speculative claim of credential safety.

**Rollback and quota:** Keep one preflight quota read, bounded post-observation quota check, fail-closed rollback on quota uncertainty/exhaustion, and one conditional image rollback only when allowed. Add focused fake tests for quota pressure after mutation, restoration failure, uncertain/late PATCH, logging restoration, and private-ledger preservation. Do not introduce extra quota reads, retries, or automatic restarts. The candidate may remain configured if quota prevents rollback; accurately mark `manualRecoveryRequired` and retain private recovery evidence. Never claim a configuration rollback restores prior health or ephemeral state.

**Approval semantics:** Keep live execution blocked absent separate owner authorization. Command flags are *not* authorization. Prefer an explicit fail-closed operator guard or documented two-step release process if necessary, but do not add a broad governance gate or unrelated infrastructure. This task authorizes **offline implementation and qualification only**, not a live run.

## Constraints

- No Azure REST/CLI management requests, health probes, log streams, restarts, deployment, config mutation, key/secret access, provider requests, registry access, Docker image build/push/pull, or retained-runtime/data access.
- No production/UI changes, API/storage/auth/security-guard changes, dependency upgrades, or source edits outside the narrow helper/test/evidence scope and #455 documentation reconciliation.
- No arbitrary cleanup or deletion of existing branches/files; do not merge PRs.
- Do not print credentials, private paths, raw log payloads, or recovery baseline contents.
- Do not treat 57/15 stop units as 57 API crashes, a below-limit snapshot as durable recovery, or a generic listener marker as running-image attestation.

## Validation and deliverables

1. Run focused fake-only unit tests; include new tests for process-tree termination and failure handling where supported. Report exact counts, exit codes, OS, and whether tests were actually run. Never invent results.
2. Run offline plan-only command and syntax/format/diff checks. Real Windows ACL tests only on a genuinely available Windows host, with synthetic disposable files and no Azure access.
3. Update only approved PR branches/files, with clear commits and concise PR comments describing the reconciliation and hardening, test evidence, and unresolved limitations. Leave #455 and #456 **open/unmerged**.
4. Provide final summary: each PR head/base/mergeability, exact changed paths, evidence preservation, test results, unresolved Windows/SCM/rollback risks, and explicit statement **NO AZURE OPERATIONS PERFORMED**.
5. Stop and ask the owner before any branch-history rewrite, live qualification, registry/Azure call, or other scope expansion.

Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` throughout.

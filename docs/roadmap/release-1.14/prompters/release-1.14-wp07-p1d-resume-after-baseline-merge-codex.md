# Codex Authority — Release 1.14 WP07-P1D: Resume Conditional Four-File PR After Baseline Merge

## Owner authorization and intent
The owner confirms that commit `67893f51931ee183f71050a83b81127e6e648b87` (`67893f5`) was merged. Resume the previously blocked WP07-P1D conditional review → PR → merge workflow **only if independently verified Git facts and every gate below pass**. This is not permission to merge the eleven unrelated baseline paths again, bypass protections, or change production configuration.

## Immutable safety context
- Historical protected branch: `draft/release-1.14-wp07-partial-public-disabled`; historical HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Existing working checkout is intentionally dirty: do not reset, stash, clean, checkout, cherry-pick, commit, or otherwise mutate it. Work in an isolated Git worktree or clone at a separate path. Do not remove the owner's existing worktree.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**. Disabled Option A and T3 unchanged.
- Preserve A1–A3, D1/strict D2/D3, T1–T11 and C1; shutdown shared 4-second `CLOCK_MONOTONIC` deadline, Docker stop grace 5 seconds, privileged bootstrap deadline 5 seconds, total startup deadline 30 seconds. Offline test outcomes never prove actual bound kernel waits, FD integrity, credential confinement or real process-tree cleanup.

## Exact change allowlist
The PR may introduce changes **only** to these four paths, copied exactly from the owner's current corrected WP07-P1D source state after independently recording SHA-256 values:
1. `container/admin_lifecycle_protocol.py`
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs`
3. `python/presentation/test_admin_lifecycle_protocol.py`
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs`

No other tracked, untracked, generated or configuration files may be included in the commit/PR. No code fixes beyond those four paths; if fixes become necessary, stop for owner review rather than silently broadening scope. No runtime integration, identities, credentials, Docker, Azure, networking or deployment.

## Gate 0 — Independently resolve the previously blocked baseline
1. Record the current checkout branch, HEAD, full `git status --porcelain=v1 -uall`, and SHA-256 of the four corrected source files. Do not assume older P1C hashes remain valid: the owner reported a later input-parity fix.
2. Authenticate GitHub and verify the exact target repository and target branch. Fetch remote references **only in the isolated workspace** or with non-destructive reference updates that do not modify the dirty checkout.
3. Confirm the actual `origin/main` tip and prove `67893f51931ee183f71050a83b81127e6e648b87` is an ancestor of `origin/main` (`git merge-base --is-ancestor` or equivalent). A verbal merge confirmation alone is insufficient. If the commit was squash/rebase-merged instead, **STOP** and request owner authorization for a new base interpretation; do not treat matching content as ancestry.
4. Determine whether the four corrected protocol paths already exist on `main`. Compare content and compute the exact four-path delta. If already identical, do not create an empty PR; report `ALREADY IN MAIN — OWNER REVIEW REQUIRED`. If there are conflicting versions or unexpected modifications, STOP.
5. Confirm the isolated branch will be rooted at the verified current `origin/main` tip and its eventual PR diff will contain only the four allowlisted paths, with no inherited eleven-path divergence. STOP on unexpected branch, base or identity ambiguity.

## Gate A — Repeat full independent review and validation against current main
In an isolated worktree/clone rooted at verified `origin/main`, bring in exactly the four corrected protocol files from the owner's existing state without touching that state. Record before/after file hashes, full status and complete changed-path list.

Independently review all six P1C findings and the later input-parity correction:
- No generic `AGENT_RESULT` bypass; validated, bound, zero-outcome report plus distinct injected positive wait observation before offline advancement. Injected observations are not actual kernel evidence.
- Positive, in-range, mutually distinct bound PIDs and strict Python/.NET numeric input parity (including bool, float, null, string, out-of-range, timestamps).
- Every rejected mutating operation enters sticky FAIL, including malformed reports, overflow and clock regression.
- EOF once only on agent→router lifecycle after `UI_STOPPED` within deadline; no premature or repeated success.
- Non-bypassable role/channel/launch/PID/sequence/deadline/type/outcome/state validation; reject internal-proof spoofing, wrong channel and replay.
- Preserve exactly 64-byte `aiq-admin-lifecycle-v1` wire layout, golden vectors, metadata rules, and C1: `A_EXEC`/`A_EXEC_ACK` only on guardian–agent bootstrap launch gate; bootstrap roles rejected on runtime channels; acknowledgment is not exec or readiness.
- No new interpretation of real D1/strict D2 evidence, EOF, `ECHILD`, terminal absence, or T2 drain.

Run the focused offline Python unit suite, focused .NET tests and standalone module build against the isolated candidate. Redirect all .NET outputs/intermediates to a disposable location outside the repository using command properties; no csproj edits, restore or package changes. Record exact commands, exits, counts and warnings. Expected prior reported baseline was 24 Python / 50 .NET tests and zero build warnings/errors, but require **actual fresh passes**, not just historical counts. Verify the four candidate file hashes match the corrected source snapshot and the candidate diff contains only the allowlist. If any test, contract, security or preservation gate fails, STOP; no PR.

## Gate B — Conditional PR creation
Only after Gate 0 and Gate A pass:
1. Create a uniquely named isolated branch from verified current `origin/main`; stage only the four allowlisted files. Inspect `git diff --cached --name-status` and `git diff --cached --check` and ensure the actual commit contains exactly the intended changes.
2. Commit and push **only this isolated branch**; create a PR targeting the verified `main` in `samuel-santos-engineer/AIQuantTradingResearch`. Explain this is offline protocol security/parity code only and does not qualify public runtime. Include review/test evidence and the four-path inventory.
3. Independently inspect GitHub's actual PR base/head, commits and changed files. STOP if any non-allowlisted path, unexpected commit or baseline divergence appears.

## Gate C — Conditional merge
Merge the PR **without another owner confirmation only if** all required CI/status checks, branch protection rules, required reviews, and any repository policy checks genuinely pass; the PR still targets the verified correct repository and `main`; the four-file diff remains unchanged; and no new main divergence or conflict invalidates Gate A. Wait for checks within reasonable execution limits; never assume missing, skipped or pending checks passed. Do not bypass branch protection, fabricate reviews, force-push `main`, use admin override, or self-approve where policy forbids it. If approval or checks cannot be satisfied, leave the PR open and STOP for owner review. After merge, verify GitHub reports merged and `origin/main` includes the merge; report PR URL, commit SHA, and exact four-path merged inventory. Do not modify the original dirty checkout.

## Mandatory final report
Report Gate 0 ancestry and current main SHA, initial/final original-checkout status, corrected-source hashes, isolated-workspace status, Gate A security review findings, exact test commands and results, actual PR changed-path inventory, GitHub check/review outcomes, and whether a merge was **verified**. Use one of:
- `WP07-P1D MERGED — OWNER REVIEW REQUIRED` (only after verified merge)
- `WP07-P1D PR OPEN — MERGE BLOCKED — OWNER REVIEW REQUIRED`
- `WP07-P1D BLOCKED — OWNER REVIEW REQUIRED`
- `WP07-P1D ALREADY IN MAIN — OWNER REVIEW REQUIRED`

Always restate `PUBLIC_RUNTIME_VERIFIED=False` and all production gates **NO-GO**. Stop on any unapproved scope expansion or ambiguity.

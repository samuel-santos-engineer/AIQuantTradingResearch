# Release 1.14 Chart MVP — C2B-11 Codex continuation authority
## Preserve approved register bytes; narrowly except four Markdown hard breaks

**Owner authorization (2026-10-16):** The owner expressly authorizes the **exact four-line whitespace validation exception** described below. This is a continuation of the existing C2B-11 documentation-only authority, **not** a new implementation, test, production, export, deployment, or activation authority.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

**Reported checkpoint (reverify; do not assume):** PR #340 merged at `fda6b1b3df0bcaf2ada1f50f72d1d050605d88f6`. The branch `planning/release-1.14-chart-mvp-c2b-11` has three authorized Markdown documents staged, but no C2B-11 commit, push or PR. `git diff --check` reported four trailing-space Markdown hard breaks in the approved owner register at **lines 3–6**. Codex stopped rather than changing its bytes. The owner has now chosen **preservation plus a four-line exception**, not whitespace normalization.

## 1. Preconditions and preservation

1. Verify PR #340 is merged at the exact SHA above, and verify the existing C2B-11 branch, its parent/main ancestry, worktree status, staged paths and intended three-file scope. **Do not reset, clean, stash, overwrite, amend, switch in or inspect the protected dirty WP07 worktree/archive.** Use only the existing verified-safe checkout.
2. Verify the staged owner register is exactly the previously approved C2B-11 register, byte-for-byte, against the original owner-provided file or previously recorded authoritative SHA-256. Record the actual SHA-256 and comparison method. **Do not invent a hash or treat a self-reported hash in the staged document as independent evidence.** If no independent original/anchor is accessible, STOP and request owner direction.
3. Inspect raw lines **3, 4, 5 and 6** of the register. Confirm each is exactly an intentional Markdown hard break consisting of two terminal ASCII spaces before LF (or the already-approved exact newline convention), with no other trailing whitespace in the register. Preserve the entire file byte-for-byte, including its line endings and terminal newline. Do not normalize, reflow, format, or modify the register to satisfy a checker.
4. Confirm the staged files are exactly the three paths allowed by the prior C2B-11 authority; do not infer path authorization from this continuation. Preserve E1–E8 as **conditional design approvals only**, all qualification obligations, and T as **TBD**. Do not edit historical C2B-8/C2B-9/C2B-10 decisions or governing policy.

## 2. Narrow validation exception — and nothing broader

1. Run normal `git diff --cached --check` and capture its actual output/exit status. This check may report **only four** trailing-whitespace findings, **all in the approved owner register on new-file lines 3–6**. Do not suppress, disable, or globally reconfigure Git whitespace checks. If any other finding exists, STOP.
2. Independently validate the staged diff for the other two authorized Markdown files with `git diff --cached --check -- <exact-other-file-path-1> <exact-other-file-path-2>`; this must exit successfully. Validate all unstaged/untracked changes separately; no unexpected modifications or outputs may be silently included.
3. Verify register line numbers and bytes using a read-only byte-aware check; do not rely on rendered Markdown alone. Confirm the exact approved SHA-256 again after all checks and immediately before commit.
4. Add a narrowly scoped explanation **only in the already-authorized C2B-11 evidence document**, if that document is one of the three allowed staged paths: four intentional two-space Markdown hard breaks at owner-register lines 3–6, owner-authorized on 2026-10-16, preserved without normalization, known `git diff --cached --check` exception, all other staged paths pass, independent register hash unchanged. If no already-authorized evidence path is available, include the exception in the PR body and final report instead; **do not add a fourth path** or edit the register.
5. Repeat scope, relative-link, Markdown heading, owner-decision fidelity, no-executable-work, and source-hash checks after any permitted evidence-only edit. Ensure the exception is not misreported as an overall clean `git diff --check` result. Report the **expected four findings** transparently.

## 3. Complete the previously authorized documentation PR

Only if every precondition and non-excepted validation passes:

1. Commit **only** the three previously authorized C2B-11 Markdown documents on the existing planning branch. Do not include incidental files, alter the approved register, or force-push.
2. Push the branch normally, obeying branch protections. Open **one C2B-11 documentation-only PR** against the verified merged `main`; leave it **open and unmerged** for owner review. Do not merge it or modify GitHub Project metadata.
3. In the PR description, state the exact exception, the preserved owner-register SHA-256 and independent comparison evidence, conditional E1–E8 design approvals, T TBD, inherited-vs-fresh checks, and all unresolved proof/target/resource gates. Explain that the four trailing-space reports are expected and exclusively owner-authorized.
4. Report PR URL, base/head SHA, exact three changed paths, register hash, verification results, exception evidence and any STOP findings. If a prerequisite fails, **STOP before commit/push/PR** and report precisely what needs owner resolution.

## 4. Absolute NO-GO boundaries

- **No** source, test, package, schema, fixture, dataset, snapshot, exporter, runtime API/UI, Docker, credential, Azure, or security-backend modification; no generated market-data output.
- **No** active tests, builds, restores, generation, export/promotion, load/fault/target/browser experiments, cloud operations or public activation. Documentation-only read/validation commands are allowed.
- **No** broad whitespace exception, modified repository lint configuration, bypassed protection, unreviewed branch cleanup, or replacement of the immutable register.
- `PUBLIC_RUNTIME_VERIFIED=False`; public dataset/candles/indicators remain unavailable, Azure deployment/public activation **NO-GO**, and advanced WP07 **ON HOLD**.

**Exit condition:** An owner-reviewable **open, unmerged** C2B-11 documentation PR with the approved owner register preserved byte-for-byte, exactly four disclosed and authorized Markdown hard-break findings, no other whitespace/scope issues, and no implementation or operational activity.

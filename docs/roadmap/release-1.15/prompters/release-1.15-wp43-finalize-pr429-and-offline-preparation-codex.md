# Codex prompt — Finalize PR #429, merge, and prepare WP43

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Release: 1.15
Existing PR: https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/429
Expected current PR head (verify, do not assume): `70bf3ef5d9e7af8dd0311fd86568e4a60f8533af`

## Owner instructions and authority

The owner has explicitly requested: **include all untracked Markdown (`.md`) files in PR #429 and then merge PR #429**. Execute these steps from the owner's actual local repository checkout, where untracked files are visible. Do not infer untracked file contents from GitHub. Preserve the owner's other uncommitted changes. The owner has **not** authorized any new Twelve Data requests, key reads, Azure operations, or retries. After merging, plan WP43 offline only.

## Phase 1 — Inventory and safely include untracked Markdown

1. Locate the intended AIQuantTradingResearch working tree and inspect `git status --porcelain=v1 --untracked-files=all` and `git ls-files --others --exclude-standard -- '*.md'`. Include all eligible untracked Markdown files **within this repository**, including nested directories. Check case-insensitive `.md` extensions on Windows and use the correct working tree. Do not silently omit any Markdown file. Do not accidentally include files from unrelated repositories, external temporary directories, or other Git worktrees.
2. Before staging, enumerate each candidate's repository-relative path, byte size, purpose, and sensitivity risk. Inspect contents **locally** for credentials, access tokens, secrets, private paths, customer information, raw provider responses, and private research artifacts. Do not print sensitive content in chat, logs, or PR comments. If any file is unsafe to publish, do **not** add it; report its path and a sanitized reason, and **stop before merging** so the owner can decide how to handle the conflict with 'all untracked md'. Do not silently redact or delete owner files.
3. Inspect `AGENTS.md`, `.gitignore`, repo governance and documentation conventions. If untracked files are ignored, do not force-add without first checking why. Preserve any already staged or modified non-Markdown files. Avoid broad `git add .`, `git add -A`, or blanket cleanup.
4. Confirm PR #429 is OPEN, mergeable, based on merged #428 commit `7c92eb98b8e9fe7d204375cf43c3e11f3f68a5e7`, and the branch matches the expected reviewed head, or reconcile a newer head safely. If the local files are in a different worktree, copy only the reviewed eligible Markdown contents to an isolated clean checkout of the PR branch, without disturbing owner changes. Stage only the enumerated Markdown paths using explicit pathspecs.
5. Check exact staged inventory and ensure no secrets, binaries, bundles, SQLite, TLS, credentials, response bodies, or unrelated changes are staged. Validate Markdown references, UTF-8/LF/no BOM where repo policy applies, and `git diff --cached --check`. Run relevant offline tests if changes affect technical instructions or test fixtures. Commit and push the reviewed Markdown addition to the **existing PR #429**. Do not create a competing PR. Update PR description with the exact added paths and offline validation evidence.
6. Re-fetch the PR and verify the final head, changed-file inventory, mergeability, and any required checks. If required checks fail or files remain unreviewed, stop and report; do not merge. If all pass, **squash merge PR #429** as explicitly authorized by the owner. Confirm merged status and exact merge commit. Do not delete owner branches or local untracked files as part of merging.

## Phase 2 — WP43 offline-only preparation (after confirmed merge)

1. Start a new isolated branch from the confirmed post-#429 `main`. Review the merged WP41/WP42 runbooks and the updated parser/diagnostic implementation.
2. Create a WP43 runbook documenting the new baseline and a reproducible **offline-only** fresh build/preparation, producer/consumer compatibility, fake-provider regression checks, owned-resource and certificate validity checks, and explicit stop conditions. Do not reuse older prepared images as proof of the new parser. If preparation creates a TLS certificate, record its actual validity window and require re-preparation if expired before a future approved attempt.
3. Run only local fake-based tests and bounded, non-provider diagnostic probes. Never read a real provider key, private historical response, or owner-private acquisition artifacts. Never perform DNS/HTTP probes against Twelve Data, provider Acquire, Azure, registry publication, or a real import. Do not weaken parser validation or use synthetic data as authentic observations.
4. Preserve the last observed historical result: BTC/USD HTTP 200; `response_parse` / `INVALID_PROVIDER_PAYLOAD`; one send and one response; ETH/USD not requested; no bundle/import; historical exact payload defect and credit usage unknown. WP42's eleven diagnostic codes are prospective and cannot retroactively explain that response.
5. Add a concise WP43 entry to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` and a WP43 runbook under `docs/roadmap/release-1.15/prompters/`. Keep architecture, endpoint, limits, provider source, data provenance, importer, API, chart UX, and no-retry guarantees unchanged.
6. Create a **new WP43 PR** and leave it **OPEN/UNMERGED** for owner review. Report tests, exact paths, base/head, actual offline evidence, and any blockers. No future live attempt is authorized by this prompt or by a configured key.

## Mandatory final report

- Untracked Markdown inventory: all eligible paths found, added, and any excluded/blocked files with sanitized reasons.
- PR #429: final head, checks, **merged or stopped**, merge SHA if merged.
- WP43: PR URL/head if created, exact tests and offline checks.
- Confirm zero real provider requests/retries, zero real credential reads, and zero Azure operations.
- Until successful authentic import and public qualification are independently demonstrated, retain:
  - `AUTHENTIC_DATA_NOT_IMPORTED=True`
  - `PUBLIC_RUNTIME_VERIFIED=False`
  - `liveAttemptReady=false`

**Fail closed:** If the local untracked Markdown files cannot be accessed, if any file appears sensitive, or if the PR head/checks are unsafe, do not claim inclusion or merge. Ask the owner for the required local access or decision.

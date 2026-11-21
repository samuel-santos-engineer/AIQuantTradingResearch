# Release 1.15 — WP61 post-merge local browser qualification (Codex)

## Authority and checkpoint

PR #450 (`WP61: local two-candle comparison`) has been squash-merged into `main` as `cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b`. Its final PR head was `f5a0bd174a68385f1850f34e2897857abc230faa`. The revised code's 90 focused tests passed in the prior Codex run, including six deterministic Streamlit widget/model handshake cases, but the **revised image/runtime/browser qualification is NOT_RUN**. Historical browser acceptance applies only to the earlier implementation.

Your task is to qualify the merged implementation safely, not to implement a new feature or deploy publicly. Treat this prompt as authorization for non-destructive local investigation, fake-only tests, and a bounded local UI qualification **only if** all retained-runtime prerequisites can be verified without changing protected assets. If safe prerequisites are unavailable, stop and report the blocker; do not improvise replacement credentials, TLS, data, images, or launcher behavior.

## Required preflight

1. Verify repository identity, clean worktree, current remote `main` and merge commit. Work in an isolated branch/worktree. Do not overwrite operator changes or untracked files. Preserve merged WP58/WP59/WP60/WP61 evidence.
2. Confirm PR #450 is merged, PR #446 remains independent/open unless external changes occurred, and inspect the final merged code and the evidence at `docs/roadmap/release-1.15/prompters/release-1.15-wp61-local-candle-comparison.md`.
3. Re-run `PYTHONPATH=python/presentation python -B -m unittest test_research_chart test_research_snapshot_client -q` from merged source. Record actual result; do not substitute historical counts.
4. Inspect only the minimum local runtime metadata needed to determine whether the existing qualified API, imported named volume, key references, TLS pair, network, and rollback images are available and safe to reuse. Never print or persist secrets, raw historical candles, private file paths, or TLS private material. Recheck temporary TLS validity before any restart.
5. Verify source/image alignment. The old WP61 UI image `sha256:79ed4ff5d92e11866d6d01d70f4b8fff8b597e4533c94103915f4796a18c0163` predates the selector fix and **must not** be presented as qualifying the merged source.

## Bounded qualification

Prefer the supported local, offline UI-only build procedure with existing pinned cached dependency/base images, `--pull=false`, `--network=none`, and an allowlisted public presentation-source context. No API build, new packages, dependency upgrades, image pulls, registry pushes, or external provider access. Record the actual new immutable image ID and verify the copied application-source hashes.

Before using the supported launcher, determine whether it can run without destructive changes to the retained API/volume/key/TLS. The prior supported launcher uses paired Stop/Start and recreates container/network identities. This is permissible **only** if ownership, exact existing asset identities, valid TLS, rollback references, and recoverability are verified and no unexpected operation is required. If any prerequisite is missing or uncertain, stop before Stop/Start and mark runtime/browser qualification BLOCKED. Do not initialize, reimport, modify, migrate, delete, or replace the historical SQLite volume; do not rotate keys or TLS; do not silently use expired preparation manifests. Do not claim byte-for-byte SQLite preservation without a byte audit.

If the safe local runtime is available, run a bounded actual Chrome/Streamlit browser smoke test against loopback only. Use native visible/hit-testable interactions and the existing authenticated local client for independent in-memory expectations; do not inject Streamlit state or bypass application controls. Cover:

- BTC/USD and ETH/USD switching, with independent A/B selections and return to the original instrument.
- A/B first/last, same, reversed, SMA20 warmup/null volume and populated SMA20; exact supplied fields and independently computed signed B-minus-A / A-denominator percentage with half-even formatting.
- Native A/B selection callbacks, reruns, refresh retention, and coexistence with WP59 viewport/pan/presets/Return to Live and WP60 inspection.
- Check for any visible widget/state mismatch or reset-notice regression. Destructive pruning, incompatible generation/date/transform rotation, missing/empty/one-candle states and zero denominator must remain fake-only unless an isolated disposable fake runtime is available. **Never induce them in retained authentic data.**
- Final authenticated BTC/ETH snapshot invariants and preservation of the retained API image, named volume identity, key/TLS references and rollback images.

Do not claim exhaustive race freedom based on one browser pass. Record earlier failed interactions separately from successful attempts and distinguish application failures from browser-driver limitations. Use bounded cleanup of only resources created/owned by this task; leave the previously working environment recoverable.

## Deliverables and acceptance

- A factual evidence append or new appropriately named lower-case hyphenated file under `docs/roadmap/release-1.15/prompters/`, preserving all historical WP61 evidence and explicitly identifying **merged** source and the newly qualified image. No private data, screenshots, raw DTOs, credentials or private paths in Git.
- State separately: unit tests, UI build/source hashes, local runtime, actual browser acceptance, retained assets, and any NOT_RUN/BLOCKED outcomes. Record commands, exit codes, limitations, and relevant image/commit IDs.
- If documentation is changed, open a **documentation-only PR** against current `main` for owner review, and leave it open/unmerged. If no changes are needed or work is blocked before meaningful evidence, report the outcome and do not manufacture a PR.
- Keep the changed-file scope narrow. Check UTF-8/LF/no BOM, `git diff --check`, branch state, and GitHub PR status. Do not modify PR #446 or introduce new implementation/governance gates.

## Hard boundaries

**No Azure, public deployment, F1 quota operations, provider acquisition, new history import, registry publishing, production operations, storage migration, credential/TLS rotation, or changes to retained historical observations.** Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` regardless of local outcomes. Local browser success does not prove public Azure readiness.

Return a concise operator handoff with verified facts, blockers, documentation PR URL (if any), and explicit confirmation that no PR was merged by this task.

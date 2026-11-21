# Codex prompt — Release 1.15: Linux-safe setting-name collector and WP61 UI provenance

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Baseline: `main` after squash merge of PR #458, commit `5217e1959a3a5ed5283c9874f2fd79b3d1a51ec3`.

## Objective
Prepare a reviewable, narrowly scoped PR that (1) qualifies a Linux-compatible, minimal-disclosure collector for the two previously proposed Azure App Service setting-name reads and (2) determines, using offline evidence first, the exact accepted WP61 chart UI artifact provenance and the minimum separately authorized registry verification needed. This is a **preparation and offline qualification task**. Do not execute Azure reads, Azure writes, registry network calls, deployments, restarts, public probes, Docker pulls/pushes/builds, or credential operations. Do not merge the PR.

## First: inspect repository and state
- Fetch and verify current `main`, PR #458 merge, and open PR #457 before branching; do not overwrite or duplicate concurrent work. Review `eng/docker/release-1.15/setting_name_provenance.py`, its tests, and `docs/roadmap/release-1.15/prompters/release-1.15-setting-name-presence-and-image-provenance.md`.
- Review WP61 local acceptance and historical Phase G image publication receipts. Preserve chronological evidence; distinguish local image ID, OCI index digest, Linux/amd64 child manifest digest, and image config digest. Never infer equivalence from tags or truncated hashes.
- Limit changes to a dedicated collector, tests, and a focused addition to the existing provenance document unless another path is strictly necessary and explained in the PR.

## Collector: qualification only, no live network mode
- Design and implement an offline-testable Linux-first collector interface for **exactly two** future read-only ARM POST `config/appsettings/list` requests, in order: research API then UI. A POST to this endpoint is a read/list operation, not a configuration write.
- Fixed subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, group `rg-aiq-r112-wp05-wus2-7f5eabb5`, research site `aiqr115researchstagingwus27f5eabb5`, UI site `aiqr112wp05wus27f5eabb5`, API version `2023-12-01`. Do not add target overrides or production scope.
- Preserve existing allowlists: 13 API names and 5 UI names from the merged parser. Output only PRESENT/ABSENT/UNKNOWN per allowlisted name, plus safe operation metadata. Do not print unknown names, setting values, secret hashes, token/credential details, paths, raw responses, HTTP headers, exceptions containing values, or identifiers derived from secret values.
- Require strict exact-resource identity, response schema and duplicate-field handling. Fail closed on malformed, truncated, over-cap, wrong-resource, unauthorized, or ambiguous responses; report UNKNOWN, never ABSENT, for uncertain collection.
- Define and **offline-qualify** a true upstream response-body cap of 1 MiB **before full materialization**, rather than relying on `az rest --query` or truncating stdout. Use injected fake byte streams and synthetic secret-bearing payloads to test streaming, overflow, errors, early termination, and no leakage. Explicitly distinguish bounded application reads from any unverified HTTP-client buffering, retries, redirects, auth, or decompression behavior.
- Plan for a 15-second deadline per request and a 30-second total request budget; no retries, pagination, fallback, or extra requests. Ensure cleanup and cancellation behavior is bounded and testable. Prefer Python standard library and already pinned dependencies; do not install packages without separately documenting why needed.
- Authentication must remain **unimplemented or disabled** until the actual existing-auth transport, token handling, redirect behavior, internal retries and network/body caps are qualified. Do not call Azure CLI, login, managed identity, ARM, SCM or publishing-credential endpoints. Any executable `--apply` or `--execute` path must fail closed before constructing real network adapters. The owner must separately approve the exact two live reads after qualification.
- Use Linux permission semantics only if temporary storage is genuinely required; prefer no storage of raw responses at all. Do not make Windows ACL or Job Object qualification a prerequisite for this Linux path.

## WP61 UI provenance
- Preserve accepted WP61 UI local image ID `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` and Azure-configured UI reference `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` as **different kinds/unknown mapping**, not a proved mismatch.
- Inspect tracked receipts and metadata offline for an exact accepted UI OCI index, Linux/amd64 child, config hash, registry repository and source commit. If the complete chain cannot be established offline, state `UNVERIFIED`, specify exactly one minimal future registry metadata-only verification footprint, and require separate authorization before calling the registry. Do not rebuild, retag, publish or pull images in this task.
- Keep the accepted research API Linux/amd64 child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` as preferred behavioral baseline. Keep diagnostic API child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c` separate. Do not change configured Azure images.

## Tests and evidence
- Add synthetic tests for two-role ordering, exact allowlists and identity, no-value disclosure, malformed/duplicate JSON, oversized and chunked streams, early stop/cancel, deadlines, error sanitization, redirect/retry rejection if relevant, default plan-only, and explicit live-mode blocking. Preserve existing 12 parser tests and run both suites offline.
- Report actual commands, counts, OS, skipped cases and limitations. Verify Python syntax, UTF-8/LF/no BOM, Markdown links, changed-path inventory, `git diff --check`, and clean branch/PR status.
- Open one reviewable PR against current `main`; provide exact head/base, changed paths, test results, and any remaining unqualified transport behavior. Leave PR open/unmerged. No Azure/registry/Docker operations.

## Follow-on deployment decision (do not execute)
After collector and provenance review, propose the smallest separately authorized live read/registry metadata footprint needed to resolve effective setting-name presence and the WP61 UI image chain. Then prepare an accepted-API-first, UI-second Linux App Service deployment plan with immutable digests, port routing (API 52731, UI 8501), private baseline/recovery, fresh F1 stop-quota check, and bounded rollback. Do not authorize or perform these operations in this PR. Preserve storage, TLS, API-key, and chart behavior; no data imports or volume deletion.

Keep `LIVE_ADAPTER_QUALIFIED=False` for the Windows helper; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false` until actual qualified evidence changes them.

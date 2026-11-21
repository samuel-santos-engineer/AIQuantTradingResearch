# Codex Prompt — Release 1.15 WP57: Corrected UI local runtime and browser requalification

## Mission
Finish local browser acceptance of the existing **authentic Twelve Data BTC/USD and ETH/USD** historical chart after the optional-axis correction in **PR #444**. Preserve the successful WP55 acquisition/import and focus exclusively on a corrected UI artifact, controlled local runtime switch, and browser qualification. **Do not expand scope to other instruments, providers, ML implementation, or public deployment.**

## Starting evidence (verify before acting)
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- **PR #443** (WP55 authentic acquisition/import evidence) OPEN/UNMERGED, head `470561539372adde0ec0452cfc8a930f06b6e755`; base merged PR #442 commit `61a1f804526b145e8a3d47f1daf4795f3fbec375`.
- **PR #444** (WP56 optional-axis fix) OPEN/UNMERGED, head `be6e7a21ce04edf5fa45883383bfaf385228afbd`. Four changed paths: `python/presentation/research_chart.py`, `python/presentation/research_chart_component.js`, `python/presentation/test_research_chart.py`, WP56 evidence.
- WP56: 66 chart/client tests passed; 38 workflow tests passed and 1 skipped; offline real-component lifecycle BTC→ETH→BTC passed. No corrected-code browser test yet.
- WP55: authentic 1-minute BTC/USD and ETH/USD datasets each 1,000 accepted candles, no gaps, null volume, imported into private SQLite; authenticated both-symbol API verification passed. BTC browser passed on old runtime; ETH radio changed but ETH graph did not mount after 30 seconds. Existing chart was `http://127.0.0.1:18555` when last recorded; **do not assume it is still running**.
- State: `AUTHENTIC_DATA_NOT_IMPORTED=False`; `LOCAL_CHART_VERIFIED=False`; `PUBLIC_RUNTIME_VERIFIED=False`.

## Required sequence and approvals

### Phase A — offline, no operational authorization implied
1. Inspect current repository, PR heads/base/branch relationships, owner checkout state, WP55/WP56 runbooks and the actual runtime workflow. Verify the intended corrected code exactly matches reviewed PR #444; do not silently use a stale checkout or an unreviewed build context. Distinguish whether PR #444 is based on main or stacked on PR #443; avoid accidentally including unrelated changes.
2. Determine the **smallest supported** way to create a corrected UI artifact while preserving the already imported private SQLite, Docker volume, authenticated API service, and observed-provider evidence. Document whether the existing workflow supports a UI-only image rebuild and replacement. If it does not, propose a supported fresh, identity-valid preparation path; **never bypass or edit preparation identity guards/manifests**.
3. Re-run the focused offline chart/client and workflow tests as appropriate. Capture commands, exit codes, counts and skips; do not claim broad suite green. Review changed paths and produce a short sanitized execution plan, including rollback strategy, resource names/identities (nonsecret), expected downtime and proof that no data reimport is required.
4. **STOP and ask the owner for a specific operational approval** before Docker inspection/build, local runtime switch, browser access or restart. Do not treat approval to write this prompt or prior WP55/WP56 approvals as authorization. Approval request should cover the minimum concrete operations and resources, not broad or unnecessary gates.

### Phase B — only after explicit owner approval for the specified operations
5. Verify the exact target PR #444 code and relevant existing local resources. Use cached/pinned inputs and pull-disabled/network-disabled builds where the supported workflow permits. No package installation or registry pull. Preserve existing owned and unrelated images, containers, volumes, TLS material, and private data; do not overwrite prior preparation directories.
6. Build a corrected UI artifact via the **supported identity-valid path**. If an isolated UI-only replacement is not supported, stop and request revised approval for the concrete fresh preparation/switch plan. Do not rerun `Acquire` or `Import`, regenerate the database, fabricate candles, or change provider configuration. Do not inspect or publish secrets, raw price payloads, private paths or historical database contents.
7. Switch the **owned local chart UI only**, or the smallest explicitly approved local runtime set required by the supported workflow, preserving the existing authenticated research API and imported data. Record nonsecret image/code identity and operational outcome. Avoid destructive cleanup. If switching fails, revert only the owned UI resources if expressly authorized, then stop and report.
8. In one bounded browser qualification using the corrected runtime, verify **BTC → ETH → BTC** selection and, for each symbol: data-bearing candlestick trace, correct symbol and expected 1,000 retained candles, SMA20 and appropriate price-based indicator alignment, `Volume unavailable` with no fabricated bars, provenance/educational disclosures, and no page/component JavaScript errors. Validate ETH's chart *actually mounts*, not just the radio selection. Confirm both-symbol API evidence remains applicable; do not issue new provider requests. Use reasonable fixed timeouts, report exact stage and sanitized failure category, and stop on failure rather than silently retrying.
9. Close owned browser instances after the bounded test. Preserve authentic data and retained local resources for owner inspection. Do not make screenshots, logs or evidence public if they could expose prices, keys, private paths or configuration. The loopback URL is for local review, not public verification.

### Phase C — evidence and review
10. Record exact tests, build/runtime identities, browser checks, pass/fail/skip, explicit approvals and their consumed scope in a focused evidence update. Keep PR #443's acquisition evidence distinct from PR #444's code fix. If documentation needs a new PR, create it **OPEN/UNMERGED** for owner review, or update an existing appropriate evidence PR only if branch ownership and PR base are verified; do not merge any PR without an explicit instruction.
11. Set `LOCAL_CHART_VERIFIED=True` **only if** the corrected-code real browser passes both symbols end-to-end. Otherwise leave it `False`, identify the exact blocking stage, and propose the smallest next fix. Keep `PUBLIC_RUNTIME_VERIFIED=False` until independently qualified.
12. Summarize what was actually executed, which runtime is active, whether BTC and ETH both passed, any residual risks, and the next owner decision. Do not claim the public milestone complete from local acceptance alone.

## Hard boundaries
- **No new Twelve Data requests, credentials/account/credit inspection, provider diagnostics, acquisition or import.**
- No Azure, registry, public deployment, public DNS activation, new market-data source or instrument scope.
- No Docker inspection/build/restart/switch, real browser navigation, or private data access before narrowly scoped explicit owner approval.
- No `--pull`/network-enabled dependency installation, no identity bypass, no stale preparation reuse, no unapproved resource deletion.
- Preserve strict OHLCV default and explicit `--ohlc-only` behavior, nullable volume semantics, C# indicators and existing research contracts.
- Stop and ask on unexpected code/resource identity mismatches or any operation outside the approved boundary.

## Deliverable / completion statement
Provide a compact acceptance matrix: offline tests, corrected UI build, local switch, BTC browser, ETH browser, BTC return, both-symbol API evidence, imported-data preservation, `LOCAL_CHART_VERIFIED`, `PUBLIC_RUNTIME_VERIFIED`; link any OPEN/UNMERGED PR. Distinguish independently observed facts from inherited WP55 evidence.

# Codex continuation — Release 1.15 WP03/WP04 functional foundation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Date: 2026-10-24
Owner decision: WP05 planning in PR #387 is approved; prioritize actual historical BTC/USD and ETH/USD functionality over further fixture complexity. **Production is strictly off limits.** All eventual publication and milestone assessment must use **staging only**, with separate explicit deployment authorization.

## 1. Verify PRs and preserve local work

1. Read `AGENTS.md`, `.gitattributes`, current main, D1-B governance amendments, Release 1.15 roadmap, WP01 D3/D4/D5 contracts, WP02/WP05 evidence and issues #368–#373. Confirm PR #386 (last known OPEN, head `1829b05cd627f53d556758d797d191af06807263`) and PR #387 (last known OPEN, head `c59634534fb65c85785971fcdd11a23729b5e0b8`) against live GitHub. **Planning approval is not automatic merge approval. Do not merge either PR unless separately explicitly authorized.** If their state changed, record the verified baseline and avoid assuming unmerged code is in main.
2. Preserve the owner's original dirty and untracked files byte-for-byte, including **all untracked Codex prompt `.md` files**. Inventory their exact paths, sizes and SHA-256 hashes before work. Do not silently omit them from the handoff, delete, overwrite, stash, reset, clean, or reformat them. Work in a separate worktree or isolated checkout.
3. **Include the untracked Codex prompt files in the next documentation deliverable** as provenance: inventory their exact current paths and contents; propose precise destination paths under `docs/roadmap/release-1.15/prompters/`, check for already committed duplicates and name collisions, and include their verbatim content in the docs-only PR only if their scope and ownership permit. Do not silently claim authority to move, delete or overwrite the original files. If exact path authorization or collision resolution is needed, list it clearly in the PR for owner decision. Preserve original bytes even if a separate archived copy requires LF normalization; report differences. At minimum include the current WP02 simplified-lab prompt and WP05 staging-only prompt if still untracked, plus any other discovered untracked Codex prompts. Never invent file contents or paths.

## 2. Next deliverable: exact-path WP03/WP04 preflight (documentation only)

The approved WP05 plan establishes that WP03 committed historical state and WP04 private calculated state are not yet accepted. Prepare a minimal, implementation-ready **WP03-first then WP04** plan, not another WP05 mock integration. Before proposing source edits, inspect the real C# projects, existing SQLite/data contracts, provider adapters, tests, and ownership. Define literal existing/new paths (no wildcard grants), interfaces, run-only regressions, dependencies, test vectors, and a bounded sequence of implementation PRs. Prefer reusing existing projects and pinned dependencies; no paid services, new licensing gates, speculative infrastructure or schema invention without explicit justification.

### WP03 functional objective

- Authoritative, private SQLite committed **completed one-minute BTC/USD and ETH/USD OHLCV**, maximum latest 1,000 actual retained candles per instrument. No fabricated candles, fake production market history or higher-resolution data claims.
- Deterministic offline provider fixtures first; real Twelve Data credentials/provider calls are **not authorized** by this prompt. Plan validated candle timestamps/OHLCV, quota fairness, bounded ingestion, gaps/corrections, continuity, atomic commits, generation/version, recovery, checkpoint and outside-SQLite watermark as required by accepted D3 contracts. Separate minimal vertical-slice sequencing from full acceptance requirements without silently weakening contracts.
- Define the coherent read-only committed-state interface that WP04 and WP05 will consume; research-only storage, provider secrets and HMAC secrets never enter UI identity.

### WP04 functional objective

- Real private indicator calculations on committed raw OHLCV: SMA20, EMA20, Bollinger20/2, RSI14, MACD12/26/9, ATR14, trend score and regime readiness/classification, including 401st-candle readiness, continuity reset, corrections/replay, validity and bounded transition events.
- Independently authored numerical vectors, boundary and warmup tests; no calculations from D5 transformed display values. Define the exact immutable calculated-state reader WP05 can consume.
- Respect WP03 writer exclusivity and atomic research state; do not start WP04 implementation on a guessed WP03 schema.

## 3. Proposed sequence and decision table

Produce a compact matrix: work package, actual existing component/evidence, missing contract, exact proposed mutation path, deterministic test, dependency, owner and acceptance evidence. Sequence:

1. WP03 exact-path plan and contract/read interface; offline fixture data and deterministic persistence/recovery tests.
2. WP03 separately authorized implementation PR; owner review and acceptance.
3. WP04 exact-path contract reconciliation against the actual accepted WP03 implementation; separately authorized implementation PR and independent numerical tests.
4. Return to WP05 PR #387's approved integration design; reconcile its 14 proposed paths against actual WP03/WP04 types before authorizing production endpoint/client code.
5. WP06 chart integration; WP07 staging-only paired-image/browser/F1 qualification after explicit separate publishing approval.

No fabricated full/unchanged snapshots and no premature WP05 endpoint that merely returns fixture 503 as if it were a working backend. Keep lab-grade standard synchronous HTTPS, **accepted native DNS timeout limitation**, and **one active service API key**; no DNS worker/subprocess or key rotation feature. Two independently deployed single-process non-root Linux/amd64 Docker images remain the target.

## 4. Documentation PR boundaries

- Create or update only **explicitly enumerated** Release 1.15 roadmap planning/evidence/manifest paths, plus the precise Codex prompt archive paths determined in step 1. Before writing, enumerate each path and whether new or existing; if permission is unclear, leave it proposed rather than mutate it.
- Open a **documentation-only PR**, leave OPEN/UNMERGED for owner review. No WP03/WP04 production source edits in this task, no automatic merge, issue closure or acceptance claims.
- Preserve historical evidence and avoid describing prior test logs as newly executed. All authored/modified text UTF-8 without BOM, LF, terminal newline; independently scan for CRLF, verify relative links and run `git diff --check`. Record exact file scope and owner-file hash preservation.

## 5. Hard environment boundary

**Never inspect or touch production**: no production Azure apps, resource groups, URLs, settings, secrets, registry tags, deployments or production health checks, including read-only probes. No Azure writes or staging publishing under this prompt either. Future authorized rollout/assessment must target only the existing staging UI `aiqr112wp05wus27f5eabb5` and staging research `aiqr115researchstagingwus27f5eabb5` (West US 2), using separately reviewed paired image digests and configuration. Do not use production as fallback. No provider calls, credential retrieval, registry push, deployment, new paid resources or Docker image promotion in this planning step.

## 6. Return report

Report verified #386/#387 status and base SHAs; WP03/WP04 implementation readiness; exact proposed source/test/docs paths; PR URL/head SHA; **complete inventory and disposition of every untracked Codex prompt file** (original path/hash, archived destination or explicit reason pending); tests actually run versus proposed; outstanding dependencies; and `PUBLIC_RUNTIME_VERIFIED=False`.

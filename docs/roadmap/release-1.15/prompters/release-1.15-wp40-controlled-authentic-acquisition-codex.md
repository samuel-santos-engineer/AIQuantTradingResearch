# Codex prompt — Release 1.15 WP40: controlled authentic BTC/ETH acquisition and local qualification

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Begin from clean, synchronized `main` containing merged PR #427, squash commit `c69832585a44c6253063b60d26d48b8ae487db26`. Inspect the merged WP39 runbook, execution plan, acquisition diagnostics, tests, and current source before changing anything. Create a narrowly scoped WP40 branch and PR; leave it OPEN/UNMERGED for owner review.

**This prompt does NOT authorize a live Twelve Data request.** The previous single-attempt authorization was consumed. Do not interpret this prompt, the existence of a key, or any CLI approval flag as new provider authorization. First complete offline preflight, prepare a fresh local runtime, and present an exact bounded live-attempt plan to the owner. Stop at `AWAITING_NEW_OWNER_PROVIDER_APPROVAL`. Only if the owner subsequently provides explicit, separate approval for the bounded live attempt may you execute Acquire once. Never retry automatically or manually under the same approval.

No Azure requests, deployments, registry pushes, cloud-resource changes, credential inspection, key rotation, provider-account probing, or licensing approval gates. Do not request, echo, persist, or expose the API key. Preserve the private Windows User setting and existing secret-channel handling. Never include secrets in logs, CLI args, PR descriptions, browser output, artifacts, or screenshots.

## Goals

1. Confirm the merged WP39 sanitized diagnostic path is wired into the *freshly prepared* C# worker and Python launcher, not an older cached worker binary. Preserve the frozen research snapshot API and Docker-first dual-container architecture.
2. Perform offline preflight only: clean checkout, exact source commit, cached build inputs, toolchain, temporary directory ownership, private channel *presence only* (never read the value outside the approved Acquire path), TLS lifetime, safe Docker resource ownership, bounded diagnostic parser, and absence of any existing destination bundle. Do not contact the provider, including DNS/HEAD/capability checks.
3. Run fake-only diagnostic/negative suites and existing chart/client regressions; report exact commands, counts, exits and environment limitations. Where practical, use fake-provider end-to-end import/start/verify tests, clearly labeled synthetic and never counted as authentic qualification.
4. Produce a precise **owner approval checkpoint**: endpoint `https://api.twelvedata.com/time_series`, at most two sequential GETs, BTC/USD then ETH/USD, interval `1min`, outputsize `1000`, timezone `UTC`, ascending order, latest bounded 24-hour window and 15-second completed-candle grace; no retries, pagination, fallback or additional API probes. State that actual provider credits and response availability are unknown.
5. If and only if explicit new approval is received later, run exactly one bounded Acquire from fresh preparation, using the existing private key channel and base-asset-volume confirmation. Capture only the allowlisted sanitized diagnostic object, child exit code and presence-only bundle artifacts. Do not log raw provider responses, headers, credential values or private path details. If acquisition fails, stop immediately, preserve owned private evidence, report `failedStage`, `instrument`, `httpStatus`, `requestsStarted`, `responsesReceived`, `outcomeUnknown`, `failureCode`, `bundlePublished` exactly as observed, and seek further direction; do not retry.
6. Only if authentic acquisition succeeds, verify provenance and hashes, then execute the existing private Import → Start → Verify path and real local Chrome qualification for BTC/USD and ETH/USD. Verify UTC/OHLCV/volume, candle counts/gaps, authenticated TLS snapshot identity, instrument switching, retained-candle presets, zoom/refresh, no browser key/upstream exposure, and observed-provider labeling. Never replace missing data with fixtures or mark a fixture authentic. Stop owned containers safely while retaining owner-controlled private evidence.
7. Update the WP40 runbook and Release 1.15 execution plan with **actual** state and evidence. Distinguish implemented, offline qualified, locally authentic qualified, and publicly qualified. Correct stale statuses without claiming a success that did not occur.

## Mandatory constraints

- Default behavior is **offline-only** and must terminate before any provider request. A fresh owner approval is required for the next live attempt; no implicit approval or self-approval.
- Preserve WP38 fixture mode and its conspicuous synthetic disclosure. Never claim authentic-data import until the successful observed-provider import and local snapshot verification have actually occurred.
- Keep `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until evidence supports a narrower local-state change; public and Azure flags remain false regardless of local success.
- Preserve the exact existing provider query and per-symbol call budget; no adapter rewrites or expanded API scope without a demonstrated defect and owner discussion.
- Use only existing dependencies and cached images; do not download packages or publish images. Preserve unrelated Docker resources and existing local state.
- PowerShell 5.1 wrapper may be syntax-checked; if execution policy blocks runtime, exercise the same Python implementation directly and disclose the limitation. Do not bypass host policy.
- Preserve fail-closed input, secret isolation, local ownership, no-overwrite semantics, and truthful unknown outcomes. Avoid freeform exceptions and raw HTTP content in diagnostics.
- UTF-8/LF/no BOM for changed text; `git diff --check`, precise changed-file inventory, focused tests, and no private artifacts in git.

## Deliverables

- New WP40 Codex implementation/review PR, OPEN/UNMERGED, referencing merged PR #427.
- Local offline preflight and test evidence, including diagnostic contract assertions and a clean fresh-build identity check.
- WP40 runbook with exact commands, safe stop/cleanup, approved-attempt proposal, diagnostic interpretation, and explicit actual stage reached.
- Execution-plan status update with no new licensing gate or Azure dependency.
- Final concise report: branch/PR/head, changed paths, tests, offline preflight, whether any provider requests occurred, exact observed diagnostic (if separately approved), whether authentic bundle/import/verification exist, and unchanged readiness flags.

**Stop rule:** If the owner has not separately approved the new bounded live attempt, do not Acquire; deliver the offline-qualified PR and request owner review. If the owner approves and the one attempt fails, stop with sanitized evidence and no retry. Do not merge the PR yourself.

# Codex Prompt — Release 1.15 WP13: Practical Azure F1 Research Execution Channel

## Starting point

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
PR #405 has been merged into `main` as squash commit `835891b6ca762698c2ea79d96bf4078dd6f62280`. Verify `main` and the merged code before working. Previous WP11 attempt ran for a 24.125-second Azure start/stop control-plane window, yielded no attributable runtime evidence, and required manual recovery; exact configurable baseline was independently restored. WP12 determined that SCM index authentication works but does not establish execution/image provenance or hard-stop feasibility. The controller intentionally fails closed. `F1_COMPATIBILITY_UNVERIFIED` and `PUBLIC_RUNTIME_VERIFIED=False`.

## Goal

Produce **one concrete, technically credible implementation path** for independently observing the actual research container running on the **existing Azure F1 staging research App Service**, with no new paid infrastructure and without weakening storage, rollback or source-provenance controls. Prefer an implementable, bounded, testable mechanism over further governance documents. Ultimately unblock authentic BTC/USD and ETH/USD historical candle charts in the existing UI.

## Work to perform

1. Inspect the merged WP11/WP12 code, controller, shell, tests, evidence and Azure staging runbook. Determine precisely what is already implemented, what the fail-closed block checks, and which observable evidence is still missing. Inspect GitHub state; preserve user checkout and all dirty/untracked files.
2. Investigate **existing-resource** options for an independently authenticated, container-bound private diagnostic channel: Azure App Service SCM/Kudu process/container APIs, Docker/container log facilities, authenticated container command/exec facilities where genuinely supported, platform diagnostic facilities already enabled, or other first-party existing-plan mechanisms. Verify actual API support and F1/Linux constraints from authoritative documentation and **read-only** staging metadata only. Do not assume SCM access proves container identity, that a nonce/digest printed by the process is attestation, or that an Azure control-plane stop acknowledgment proves kernel termination.
3. Select the simplest viable route, or state clearly and specifically why none can meet the evidence contract under existing F1 constraints. Explicitly distinguish: (a) source authentication, (b) binding to the target process/container, (c) binding to the qualified immutable image, (d) freshness and replay resistance, (e) collection before stop, (f) stop/rollback confirmation. Do not invent an attestation guarantee the platform does not provide.
4. Where feasible, **implement actual code and offline tests** for the channel adapter and its failure modes, keeping publication and Azure execution disabled until prerequisites are genuinely met. Add strict negative tests for stale/unrelated processes, spoofed logs, missing/partial output, timeouts, inaccessible SCM, mismatched image and rollback failure. Reuse existing diagnostic contract and exact-site protections. Avoid new public endpoints, secret leakage, broader Azure permissions or generic production exceptions.
5. Assess whether a ≤60-second candidate window and independent stop/rollback can be enforced in practice. If Azure control-plane behavior cannot prove a hard kernel stop bound, say so; propose a narrower measurable guarantee or an alternative that avoids unsafe mutation. Do not silently relax the requirement.
6. Run focused local tests (Python, .NET, PowerShell syntax, shell/Linux where available), diff checks and UTF-8/LF validation. Report actual commands and outcomes, separating executed checks from unavailable ones.
7. Create **one focused PR** against `main` for justified implementation/test/runbook changes. Do not open a documentation-only PR merely to restate blockers. If no safe implementation is possible, leave the repository unchanged and deliver a concise blocker report plus a specific decision the owner can make. Include a clear go/no-go experiment plan only if technically justified.

## Strict execution and safety limits

- **No Azure mutation:** no image push/publication, configuration changes, restarts, start/stop, diagnostic attempt, imports, credentials or deployment. Read-only metadata checks are allowed only if credentials are already available and no secrets are exposed.
- **No provider calls or credits**; no authentic data import; no changes to the UI app.
- No new paid VM, App Service plan, storage, logging service or infrastructure.
- Preserve exact research App Service identity `aiqr115researchstagingwus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, existing F1 plan, and existing UI App Service `aiqr112wp05wus27f5eabb5` untouched.
- Preserve exact-site non-durable SQLite guard, UID 1654 assumptions as *hypotheses until observed*, WAL/D5/HMAC, single-writer, authentication, fail-closed defaults and no fixture/provider fallback.
- Do not merge any PR, close issues, alter production, modify governance requirements or claim live qualification.
- Never treat previously published candidate digest `sha256:5b48f5d703acd7dca7e722ab9bc0028cbe8b80a3526c7a60800699785f6abed3` as the digest of a newly built candidate without fresh independent verification.

## Final response contract

Report: selected mechanism and why it is supported; source/process/image binding proof or explicit gap; exact changed files and PR URL (if any); test evidence; unverified claims; precise next authorized action with target, bounds, rollback and prerequisites. Preserve `AZURE_DIAGNOSTIC_NOT_RUN`, `F1_COMPATIBILITY_UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False` until real evidence justifies changing them.

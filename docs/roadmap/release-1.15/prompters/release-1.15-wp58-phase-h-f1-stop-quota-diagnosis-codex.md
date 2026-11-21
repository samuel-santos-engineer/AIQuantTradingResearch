# Codex prompt — Release 1.15 / WP58 Phase H: F1 stop-quota diagnosis

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. Focus on the **existing Azure Linux F1 staging research API** and the repeated stop-quota condition. Do not expand the architecture or initiate another deployment.

## Verified starting evidence

- Phase G deployment evidence: PR #447, open/unmerged, head `784ca7fefd6a19365d02dc1bd6c4409043cd4ff1` at the last review. PRs #445 and #446 also open/unmerged.
- The accepted WP57 Python and .NET images were published and OCI-verified. The accepted .NET linux/amd64 runtime manifest `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` was configured on research staging. **Running-container identity is still UNKNOWN.** UI was not promoted.
- API observation after image configuration: no response, repeated TLS-validated HTTP 503, then HTTP 403; no HTTP 200 Alive. At UTC 2026-11-14 19:27:12, research state `QuotaExceeded`, usage `Exceeded`, availability `Normal`, shared F1 `WPStopRequests=26/15`, daily CPU `0/3600000 ms`, short CPU `0/180000 ms` as reported. Counter `nextResetTime=2026-11-14T20:00:00` with **no offset**; do not infer timezone or that reset occurred.
- The 26/15 counter does **not** establish the root cause or prove every stop came from this deployment. No Azure `IOException` established. Local Linux SDK-test `System.IO.IOException` in PR #446 is a separate event. A fake-only local storage probe passed, but is not Azure proof.
- Azure staging API `aiqr115researchstagingwus27f5eabb5`; UI `aiqr112wp05wus27f5eabb5`; resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`; shared F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`; West US 2. Production excluded.
- Private per-app rollback baseline exists and must remain private. No rollback performed. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Goal

Determine, with the **smallest bounded read-only evidence**, whether the stop quota has reset and whether existing platform diagnostics attribute the recurring stop/startup condition. Distinguish quota enforcement from application startup, image identity, routing, and SQLite hypotheses. **Do not claim a cause without timestamped, app-specific evidence.**

## Work in two stages

### Stage A — offline preparation (authorized by this prompt)

1. Verify the current PR #447 evidence and exact target identities; note any head changes rather than assuming the historical SHA remains current. Read only the necessary existing repository documentation and public Microsoft documentation if needed; distinguish recorded evidence from new inference.
2. Inspect existing Azure CLI/ARM query patterns **offline** to choose the minimum projected, nonsecret fields. Avoid generic log dumps, full app settings, environment, credentials, raw request bodies or private paths.
3. Prepare **one small read-only evaluation plan**, with exact API/CLI commands or requests, projections, timeout budgets, and stop conditions. Preferred sequence:
   - **H1 (quota snapshot):** one projected shared-plan usage read for `WPStopRequests` current/limit/nextResetTime and CPU counters; one projected research-site metadata read for state/usage/availability, plus observation UTC timestamp. Treat stale or missing fields as UNKNOWN.
   - **H2 (only if H1 permits):** at most one narrowly targeted existing Azure platform diagnostic query for research API container start/stop/termination, within a fixed 15-minute observation window, returning only event time, source/category, status, error/exception *type* and container/runtime identity **if actually supplied**. No log enabling, full stack traces, raw logs or secret-bearing fields. If no supported safe projection exists, STOP and report UNKNOWN.
4. Record whether the quota reset has actually been observed. The historical offset-free reset timestamp is not proof. If `WPStopRequests` remains exceeded, stop after H1: **no HTTP probe, restart or UI promotion**. If reset, H2 may be proposed but do not automatically probe serving.
5. If the plan requires live Azure access, present its precise bounds and obtain separate owner approval **before execution**. This prompt does not authorize live Azure calls.

### Stage B — only after explicit, separate owner approval

Run exactly the approved projected reads, without retries or mutations. Suggested caps: H1 at most 2 ARM GETs, 15 seconds each; H2 at most 1 targeted diagnostic read, 15 seconds; total 45 seconds excluding approved overhead. No follow-on calls outside the approved budget. Record UTC times, target identity, status, projected booleans/counters, and whether each conclusion is `PASS`, `FAIL`, or `UNKNOWN`. Never invent absent fields. Stop immediately on an exceeded quota, unexpected target, permissions failure, or unexpected response shape.

## Decision table

| Evidence | Conclusion | Next action |
| --- | --- | --- |
| Stop quota still exceeded | Quota enforcement remains an immediate blocker; underlying cause UNKNOWN | Stop, do not restart/redeploy/probe |
| Counter below limit; site still not healthy | Quota reset observed but startup/serving still unverified | Examine H2 evidence; propose one separate narrow serving test only if justified |
| Attributable timestamped startup/termination event | Specific failure category established, not necessarily root cause | Propose one targeted fix or diagnostic, not a broad redesign |
| No usable diagnostics or no runtime binding | UNKNOWN, not proof of absence | State the missing evidence and smallest next test |

## Hard boundaries

- No Azure mutation, restart, redeploy, rollback, app-setting update, logging enablement, new service, paid tier, provider request, data acquisition/import, secret/key access, Docker operation, or production access.
- No repeated health requests, background polling, automatic waiting, CLI installation/update, or quota-reset assumptions.
- Do not modify application code, images, database, or staging UI. Preserve PR #445/#446/#447 as OPEN/UNMERGED; do not merge.
- If documenting results, use a minimal reviewable evidence-only change, sanitize all outputs, and verify UTF-8/LF/no BOM and `git diff --check`. Do not claim tests ran for documentation-only work.
- Maintain `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` until independently proven otherwise.

## Deliverable

Return a concise H1/H2 evaluation proposal (or, after separate authorization, actual evidence), a matrix of known/unknown conclusions, the single smallest next decision, and exact PR/document paths updated. **The primary goal is to understand recurring F1 stops, not to continue deployment blindly.**

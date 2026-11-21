# Codex task — Release 1.15 WP62-B: bounded Azure F1 quota and management-event observation

## Authority and baseline

Owner has approved creating this Codex prompt after PR #452 merged into `main` at `4436aa3ebad42730f8a95688694376a6470cd69d`. This prompt authorizes **only** the two conditional, read-only Azure management GETs specified below, subject to every preflight and stop condition. It does **not** authorize app HTTP probes, starts/stops/restarts, deployment, configuration edits, log streams, credentials/key reads, private dataset access, provider requests, or registry operations. Do not treat a successful quota read as app health or readiness.

Read the merged `docs/roadmap/release-1.15/prompters/release-1.15-wp62a-stop-quota-offline-investigation.md` first and verify exact repository/branch/commit, target IDs, API version, scope and event-filter support offline. Work in a clean isolated checkout based on current `main`; preserve other local work. If any source or target identity differs, stop and report rather than improvise. No code changes or Azure mutations.

## Limits and preflight

- At most **two Azure management GETs total**, strictly sequential, **one invocation each**, **no retries**, no polling, no pagination or nextLink traversal, no discovery calls, no login/token acquisition, no CLI upgrades. Only existing authorized CLI authentication; never expose tokens.
- Per request: **15-second hard wall-clock timeout**; overall **30-second request budget**. Implement bounded streaming response capture, **1 MiB maximum** per response; abort before exceeding the cap. Do not run unbounded CLI capture and truncate afterward.
- Use installed Azure CLI Python-module entry point with argument arrays or an equivalently reviewed safe HTTP mechanism. Never invoke unquoted `az.cmd` with a URL containing `&` or `$filter` that a shell can reinterpret. Verify encoding and exact filter syntax offline before making a request; if unsupported, stop with zero requests.
- Validate subscription/resource group/plan/app identifiers from merged evidence before any call. Do not broaden target scope on errors. Do not inspect any other apps or resources.
- On timeout, authorization/TLS error, unexpected status, unknown response schema, ambiguous reset time, suspicious output, identity mismatch or quota pressure, **stop immediately**; do not attempt a workaround.
- Retain only allowlisted, sanitized scalar fields in evidence. No raw response bodies, headers, URLs containing credentials, access tokens, caller identity/IP/correlation IDs, private paths, logs or payloads.

## GET 1 — exact F1 plan usage metadata

`GET https://management.azure.com/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5/usages?api-version=2023-12-01`

Record UTC observation timestamp and only name/current/limit/unit/nextResetTime for `WPStopRequests`, daily CPU and short-period CPU when explicitly present. Do not assume absent values are zero or offset-free reset timestamps have a timezone. Confirm the stop-quota counter and limit are valid and current. **If current >= limit, or if any essential quota field is missing, stale or ambiguous, stop after GET 1.** No app event read. Do not assume a zero counter will persist.

## GET 2 — conditional, research-app-only management activity

Execute **only if GET 1 proves the quota is below its limit** and all prior checks pass. One request, no fallback, using the WP62-A document's exact fixed window and encoded filter:

- Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`
- API: `2015-04-01`, `microsoft.insights/eventtypes/management/values`
- Window: `2026-11-15T03:00:00Z` through `2026-11-15T04:31:00Z`
- Resource URI exact equality: `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5`
- `$top=20`, and a properly URL-encoded `$filter` using the window and exact resource URI.

Validate returned event resource IDs in memory against the research app (or its configuration children), reject unrelated events. Output only UTC timestamp, allowlisted operation class (`stop`, `start`, `restart`, `config-write`, `deploy`, `other`), status enum, and coarse caller category with identifying details withheld. Report event count and whether a nextLink exists; if so, label evidence **INCOMPLETE** without following it. No events is not proof of no platform recycles or app crashes. A query failure must not trigger a broader activity-log search.

## Interpretation and publication

Separate observed counters/events from hypotheses. Do not attribute shared-plan stop units to the research app without event-level evidence. Do not claim that a management-event absence excludes platform recycling, that keys caused startup failure, or that public chart readiness is verified. Keep `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

Add a concise WP62-B evidence document under `docs/roadmap/release-1.15/prompters/` with the exact number of GETs attempted, timestamp, sanitized results, whether the second read was skipped and why, any incomplete/unknown classifications, and the **smallest separately approvable next action**. Preserve WP62-A and all previous Release 1.15 evidence. Validate UTF-8/LF/no BOM, Markdown links/tables and `git diff --check`; make a **documentation-only PR**, leave it **open and unmerged**. If safe execution cannot be guaranteed, make **zero Azure calls** and document the preflight blocker instead.

# Codex task — Release 1.15 WP62: minimal Azure F1 staging probes

## Authority and objective

The owner reports Azure F1 quota recovery at 2026-11-15 00:05 São Paulo time: stop requests 0/15, daily CPU 0/3,600,000 ms, short-period CPU 0/180,000 ms, research app Running, usage/availability Normal. These are owner-provided observations, not proof of application health. The goal is **a very small, bounded, read-only staging check** to avoid consuming F1 quota unnecessarily. Do not expand this into full deployment qualification.

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Current `main` includes WP58 PR #446 squash merge `fcbb54201e951a8dec5fe71a798aece8e5e22aa8`, WP59–WP61. Public staging URL historically `https://aiqr112wp035ec325382770.azurewebsites.net/`; verify the configured staging target from existing public repository documentation before probing. Do not assume the deployed build is current.

## Strict execution budget

1. First perform **offline/read-only** inspection of existing repository documentation, known public endpoints, and the latest WP58–WP61 evidence. Confirm which URL/path is safe and whether a probe requires authentication. Never print secrets or request new credentials. Avoid Azure CLI authentication, resource inventory, or portal changes if public HTTP is sufficient.
2. At most **two HTTP requests total**, sequentially, to the confirmed staging host, with **no retries, redirects to other hosts, polling, parallel calls, or automatic follow-up requests**. Prefer one lightweight documented health/readiness endpoint and, only if justified by its result and the remaining budget, one lightweight public chart/static route. Do not request candle history, large JSON, images, provider endpoints, or private API data. If the documented endpoint is unavailable or uncertain, stop rather than guess.
3. Per request: connect timeout 3 seconds, total timeout 8 seconds, response body cap 4 KiB, headers only where practical; no credentials/cookies; explicitly disable automatic redirects. Do not use a browser or load a full page (which may trigger many asset/API requests). Stop on 429, 5xx, timeout, TLS errors, unexpected redirect, auth requirement, or any sign of quota pressure. A 401/403 is not evidence of a healthy authenticated chart.
4. Do **not** call Azure management APIs just to refresh quota counters. If quota metrics are already safely available from a previously approved read-only source without additional calls, use them; otherwise rely on the owner's baseline and mark post-probe consumption unknown. No claim of zero CPU usage.
5. Do not restart/stop/start, redeploy, pull/build/push images, change app settings, scale, modify DNS/TLS, initialize SQLite, transport/import private datasets, access retained runtime/data/keys, or call upstream providers. No production/trading actions. No new governance/licensing gates.
6. If the environment cannot guarantee these caps, **do not issue network requests**; prepare a safe, exact proposal for owner review instead. If repository policies require separate authorization for even these public staging probes, stop and request it rather than bypassing them.

## Reporting and evidence

Record UTC and São Paulo timestamps, verified target host and paths (no query secrets), method, request count, elapsed time, HTTP status, redirect target host only if safe, bounded non-sensitive content type/body classification, and why each request was made. Redact tokens, cookies, IP-sensitive headers, trace identifiers and response bodies containing private data. Clearly distinguish *reachable*, *healthy*, *authenticated*, *current deployment*, and *chart ready*; do not infer later states from an HTTP 200 or an App Service Running flag.

Preserve `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` unless a separately authorized full acceptance process proves otherwise; these two lightweight probes are **not** that acceptance process. Preserve the latest Release 1.15 evidence, including historical Azure quota blocker and WP58–WP61 local qualification.

If documentation-only evidence is warranted, add a small, clearly dated WP62 note/PR without rewriting prior findings; avoid unrelated changes. Leave any PR open/unmerged for owner review. Report the exact next smallest step based on observations. Explicitly state whether any HTTP requests actually occurred and whether quota usage after the probes is known.

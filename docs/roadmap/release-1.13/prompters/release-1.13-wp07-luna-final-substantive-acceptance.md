# Release 1.13 WP07 --- Luna Final Substantive Acceptance Authority

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns final substantive acceptance of WP07. GPT-5.6 Terra
may perform only corrections explicitly permitted by this authority.
GPT-5.6 Sol may support analysis/reconciliation but may not assume Luna
acceptance authority.

## Candidate identity

Canonical predecessor / PR base:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

PR: `#304`

Branch: `feature/release-1.13-wp07-azure-f1-validation`

Candidate head:

`6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`

Deployed image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`

Deployed immutable digest:

`sha256:18f60a172d51ff3d4c2b1bdf1b8089ba989cb5ef149adc3e5cf9488c9482f829`

GitHub state was independently checked before this authority was issued:
PR #304 is Open, non-draft, unmerged; its base is the canonical
predecessor above and its head is the candidate above.

## Purpose

Perform adversarial final substantive acceptance of the complete WP07
candidate before any merge/lifecycle authority. Do not merely repeat
Terra's PASS claims. Independently inspect the candidate diff, tests,
runtime/deployment evidence, provider correction, F1 constraints, public
UI contract, security boundaries, and governance state.

## Scope reconciliation

Reconstruct the exact PR #304 changed-path set from GitHub/git and
compare it with accumulated WP07 authorities. Independently review all
implementation-sensitive paths, including container entrypoint, WP07
container validation, presentation/System Health source and test, Vike
provider source and test, and WP07 governance/evidence/manifest
documents.

Fail acceptance if an unexplained path lies outside accumulated WP07
authority. Root `README.md` must remain unchanged.

## Evidence to verify, not blindly trust

Reported regression evidence: Release build 0 warnings/errors; Domain
11/11; Application 168/168; Architecture 27/27; Infrastructure 238/238;
focused Vike 24/24; Python presentation 36/36; Python compile,
`pip check`, PowerShell parse, whitespace, and candidate-diff secret
scan all pass.

Reported real-credential fresh-cache evidence: host and exact candidate
container BTC/USD and ETH/USD 1h/30D each return Fresh, 720 candles,
success, exit 0, without displaying the credential.

Reported final interactive live evidence: defaults BTC/USD/1h/30D;
visible BTC and ETH candlesticks and separate volume bars; Vike
historical OHLCV provenance with UTC last-updated; 4h/90D exercised and
defaults restored; clean System Health and controlled Refresh now; ML &
Automation Studies; Twelve Data private/internal boundary; no-trade
footer; no secret, traceback, internal path, Worker/cache path, raw
stderr/provider body/internal exception.

Reconcile these claims with candidate code/tests and reproducible
evidence available.

## Provider-adapter acceptance

Adversarially inspect `VikeHistoricalMarketDataProvider`.

Confirm the correction is narrowly justified by actual Vike behavior and
does not simply make parsing permissive. Review object envelope/candles
handling; `ts`, `open`, `high`, `low`, `close`, `volume`; retained
compact/array compatibility; UTC epoch handling; decimal precision; OHLC
validity; ascending/unique timestamps; malformed/missing-field
rejection; pagination/cursor behavior; request limit; closed-candle
behavior and reliance on documented provider default rather than
`include_partial=false`; typed HTTP/auth/rate-limit/timeout/cancellation
mapping; provenance; and absence of raw-provider leakage.

Challenge whether `limit=5000` remains bounded and compatible with every
supported range/interval and F1 constraints.

## Test-quality acceptance

Inspect tests for behavioral quality, not counts. Confirm meaningful
coverage of documented named-object candle shape, required fields,
malformed responses, ordering/duplicates, precision, request query
behavior, provider status mapping, cancellation/deadline behavior where
applicable, System Health without `st.autorefresh`, controlled public
failures, and no weakening of previous tests merely to accept the live
payload.

## System Health acceptance

Confirm removal of unsupported `st.autorefresh` is appropriate for
pinned `streamlit==1.61.1`; user-driven `Refresh now` is bounded and
functional; no replacement polling/background loop exists; public errors
do not expose internals; legacy System Health/read-model behavior
remains intact. No dependency addition/Streamlit upgrade is permitted.

## Architecture acceptance

Verify the live path remains:

`Streamlit -> bounded local Worker invocation -> HistoricalQuery -> HistoricalMarketDataReadService -> governed cache -> Vike -> presentation-safe JSON -> Streamlit`

Confirm no Python/browser provider client, no Twelve Data public
fallback, no duplicate cache/freshness authority, one-shot Worker exit,
fixed `/app/worker/AIQuantTradingResearch.Worker.dll`, bounded
subprocess/protocol behavior, and no provider acquisition from
hover/zoom/pan.

## Cache, F1, and security acceptance

Verify `/home/aiq-market-cache` is safely created and writable by `aiq`,
privilege drop is correct, persistent `/home` and bounded retention
remain governed, atomic/stale-cache behavior is not bypassed, and no
background polling/persistent companion/unnecessary fanout exists.

Confirm existing Linux Azure App Service F1/Free, HTTPS-only topology,
no new paid Azure resource/service, recurring infrastructure target
`$0.00`.

Confirm `Vike__ApiKey` remains server-side; no secret is committed,
baked into image/build args, or browser-visible; no unsafe
shell/user-controlled execution; provider failures are sanitized; public
UI leaks no traceback/filesystem/provider body/raw stderr; cache path is
not publicly controllable; Twelve Data remains private/internal.

## Public UI acceptance

Require Market Research default; BTC/USD and ETH/USD; 1h/4h/1d;
1D/7D/30D/90D; default BTC/USD/1h/30D; candlesticks plus volume; Vike
provenance; UTC last-updated; controlled unavailable/stale/empty states;
ML informational surface; System Health; Twelve Data boundary; no-trade
footer; and no indicators/ML signals/forecasting/trading scope creep.

## Governance/lifecycle acceptance

Confirm PR #304 remains Open/unmerged; issue #294 Open/non-Done; issue
#295 Open; milestone #64 Open; no Release 1.13 tag/GitHub Release; WP08
not begun; root README unchanged; dependencies unchanged except
previously accepted Plotly from WP05; no schema change.

Do not merge or mutate lifecycle state.

## Correction authority

If Luna finds an ordinary defect that can be corrected within paths
already changed by PR #304 and already covered by accumulated WP07
authority, make/direct the smallest correction and rerun affected
checks. Any runtime-affecting correction requires renewed runtime
evidence and a new accepted head.

Stop if correction requires a new path, dependency, schema,
architecture/provider-contract redesign, secret/configuration mutation
outside authority, Azure resource/tier change, README, Release 1.14/2.0
work, or lifecycle action.

## Retry-until-governance-boundary rule

Do not stop merely because a build, test, validation, container,
provider, deployment-evidence, or tooling check fails. Diagnose and
correct failures permitted by this authority, rerun checks, and
continue. Stop only when the next corrective action crosses governance.

## PASS standard

PASS only if Luna substantively establishes that candidate
`6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`, or an explicitly corrected
successor within authority, satisfies WP07 Azure F1 integration,
stability, bounded-resource, provider, security, public-runtime, and
zero-cost acceptance without unresolved material defect.

PASS remains unmerged and authorizes only a separate Terra
merge/post-merge lifecycle authority.

## Required final report

Return canonical predecessor; reviewed candidate head; PR
state/base/head; exact changed paths; authority/scope reconciliation;
provider parser/request findings; test-quality/rerun results;
real-credential BTC/ETH findings; Worker/cache/F1 findings; System
Health findings; public UI findings; Vike provenance/UTC;
security/secret/no-bypass; README/dependency/schema; deployed
image/digest; issue #294/Project state; #295/milestone #64; tag/GitHub
Release state; corrections/new head if any; non-blocking residual risks;
and next authority.

End with exactly one of:

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS does not authorize merge, issue closure, Project Done, WP08,
milestone closure, tag, or GitHub Release.

# C2B-37 ? Production publication evidence

Date: 2026-10-17. Owner instruction: "worked. publish to production environment". This explicitly authorizes promotion of the accepted synthetic-chart image to production, superseding the earlier staging-only restriction for this promotion. No new implementation, package installation, provider integration or advanced WP07 activation.

## Outcome: PASS

Production: https://aiqr112wp035ec325382770.azurewebsites.net
Resource group: rg-aiq-r112-wp03-wcus-5ec325382770; West Central US.
Staging remains aiqr112wp05wus27f5eabb5, West US2, unchanged.

Exact staging-accepted OCI image: `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07`.
OCI config: sha256:50358fb5caf32a149c4129261b864e441a72d01bfce4ee748fa6013a5d550b04.
Source baseline: main99118e0c925c359d8237549422a2bc4f03588607, merged PR355/356/357.
No rebuild or source-code changes during production promotion.

## Verified evidence

| Check | Result |
|---|---|
| Production target and rollback snapshot |PASS before mutation; the private snapshot retains image, startup override and selected nonsecret runtime settings|
| Registry |Index and Linux/amd64 manifest hashes/config equal accepted staging image|
| Exact image promotion |Azure linuxFxVersion readback equals certified immutable digest|
| Real browser |All6 controls off/on/off on1D and30D; all12 interval/range selections match exact C# values and timestamps|
| Original controls |Candles,volume,selectors,hover,zoom/pan,modebar and native Technical Analysis popover PASS|
| Panels/disclosure |RSI0-100, separate MACD/ATR axes, all overlays, synthetic title/hover and UTC labeling PASS; screenshot visually reviewed|
| Public data API boundaries |dataset/candles/indicators503 DATASET_NOT_READY JSON; unknown API404 JSON; admin/asset boundaries preserved|
| Liveness/System Health |root and liveness200; fresh browser System Health validates synthetic bundle and reports application responding|
| Azure startup |Accepted image pulled; startup probe succeeded04:08:07UTC and site started; later liveness PASS|
| Preservation |Selected runtime settings, empty startup override/scmNone and staging digest unchanged; owner prompt preserved|

Local Linux acceptance inherited from exact image:49 C# calculator tests,14 C# projection/export tests,43 Python tests; all passed. No broad tests or builds repeated for this deployment. Production smoke verifies deployment behavior, not Azure capacity or advanced containment certification.

Known owner-accepted zoom/pan reset on indicator toggle remains; zoom/pan controls themselves work. Existing Streamlit deprecation warning remains. `PUBLIC_RUNTIME_VERIFIED=False`; raw public data APIs remain intentionally disabled, advanced WP07 ON HOLD. This is acceptance of the existing synthetic chart publication only.

Final checked at UTC: 2026-10-17T04:09:51.077880+00:00.

Repository changes in this turn: this documentation-only evidence file. No source/test/config changes, commit, PR or merge performed. Existing owner-untracked authority remains untouched. Owned verification browser stopped after evidence collection; evidence retained.

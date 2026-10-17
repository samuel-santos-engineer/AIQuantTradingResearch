# Release 1.14 — Milestone 65 closeout and evidence consolidation

**Closeout basis:** owner certification and repository evidence, 2026-10-17. **Documentation only.** This record does not modify runtime code, existing release governance or README files.

## Product scope

The authoritative objective is **“A publicly accessible, educational synthetic crypto-chart application—not a live or real historical market-data platform.”** See [synthetic-only governance](../../governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md), merged in [PR #352](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/352). All public candles, prices and volumes are generated synthetic educational output. No real quote-provider acquisition, live feed, trading execution or real historical market data is authorized.

## Accepted delivery and traceability

- [PR #354](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/354): integrated 49 changed files into the synthetic chart runtime and evidence; 36 focused Linux integration tests reported passed. Original Streamlit/Plotly candles and volume, 12 timeframe/range selections, synthetic disclosure, System Health and API-unavailable boundary.
- [PR #355](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/355): EMA(20) and Bollinger Bands(20,2); owner-reported 52 C# and 40 Python tests.
- [PR #356](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/356): RSI(14) and MACD(12,26,9); owner-reported 52 C# and 41 Python tests.
- [PR #357](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/357): ATR(14); owner-reported 63 C# and 43 Python tests. SMA(20) is retained, making six indicator families total.
- Owner reports local Linux Docker and staging native-browser validation for all 12 selections, all indicator toggles, chart controls, System Health and API boundaries. Final staging-accepted OCI image digest: `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07`. Historical final phase discussion: [PR #357 evidence comment](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/357#issuecomment-6229279131).
- Owner additionally reports publication of the *same exact staging-tested image* to [production](https://aiqr112wp035ec325382770.azurewebsites.net/) and successful six-indicator browser checks. [Staging](https://aiqr112wp05wus27f5eabb5.azurewebsites.net/) was not changed by that publication. These are **owner-certified operational results**, not independent infrastructure or security certification in this document.
- Owner reports a locally saved, **uncommitted** production publication evidence file: `docs/roadmap/release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md`. Its content is **not available in this PR**; it is deliberately not reconstructed or represented as committed evidence. The owner may separately incorporate and review it later.

## Milestone 65 issue reconciliation

| Issue | Closeout disposition | Rationale |
| --- | --- | --- |
| [#323 WP05](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/323) | **Completed for adopted synthetic showcase scope** | Candlestick/volume, synthetic labels, range/timeframe, overlays/panels and browser testing delivered through PRs #354–#357. Original planned issue dependencies were superseded by later owner-authorized Docker-first release decisions; this is not a claim that every early WP07 dependent gate was fulfilled. |
| [#324 WP06](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/324) | **Not planned / superseded** | Original homepage keynote, `?page=data-governance` navigation, locally authored SVG diagram and responsive accessibility deliverables are not evidenced as implemented. Closing the release does **not** assert acceptance of these requirements. Retain original issue for future separately approved design work. |
| [#325 WP08](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/325) | **Not planned / superseded in original scope** | Synthetic chart Docker/browser validation and owner-certified deployment evidence exist, but original cross-WP07 public/private isolation, hard streaming/cancellation and F1 capacity/security acceptance are **not certified**. Do not equate owner-accepted showcase with full original WP08/G-1–G-4 certification. |

Prior [#322 WP07](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/322) was already closed as **not planned** under synthetic-only scope; archived design/security work remains preserved. All original issue descriptions remain untouched.

## Preserved gates and limitations

- `PUBLIC_RUNTIME_VERIFIED=False` remains the repository's formal recorded value. Owner-certified production showcase availability **does not** mean full legacy WP07 isolation/certification or live data API publication. No runtime flag is changed here.
- Raw public historical-data APIs remain disabled (503 `DATASET_NOT_READY` where applicable); advanced WP07 remains ON HOLD.
- No provider contracts or data rights are revisited. No release images are rebuilt or deployed by this closeout.
- No claim is made to independently verify Azure configuration, deployed digest or uncommitted local evidence from this PR.

## Documentation/closeout invariants

This PR must add **only this Markdown closeout record**. Do not modify any `README`, release definition, execution plan, file manifest, governance control, existing owner document, application/runtime/test/config file or deployment resource. PR merger and issue closure are expressly owner-authorized. Do not modify milestone metadata or create a new release unless separately approved.

**Milestone close condition:** after the documentation-only PR is merged, close #323 as completed, #324 and #325 as not planned, with issue-specific rationale and a link to this record; then verify the milestone contains zero open issues. Distinguish “zero open issues” from changing GitHub milestone's own `state`: the connector may not support modifying milestone metadata.

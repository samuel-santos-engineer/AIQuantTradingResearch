# C2B-33 ? Synthetic chart readiness inventory and dependency decision

**Read-only inventory complete; labeling implementation and new-image/browser qualification BLOCKED/INCOMPLETE pending dependency disposition.** This is a two-document review PR against current main, not chart acceptance. PUBLIC_RUNTIME_VERIFIED=False; Azure deployment/activation NO-GO; advanced WP07 ON HOLD. C2B-32 ABORTED; C2B-31 DEFERRED. No provider research, market-data calls, ETH-like series, source changes, image build/push, dataset promotion or Azure operation in C2B-33.

## Governing baseline and exact integration inventory

Fetched origin/main at `ce7afdde4febea8abe30cd2be4873fef29aeb18e`, the merge of #352. Read `docs/governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md` and `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md` from that commit before edits. Synthetic educational BTC-like charts are the product objective; public accessibility does not authorize gate removal. The Docker policy retains historical proposal/checklist wording despite #348's merged status; no governance text is edited here.

Original checkout is preserved at `0e568d208f2769037bf4a7265ea4ba6f4609aacc`, branch `feature/release-1.14-c2b-24-azure-feasibility`, with five owner-untracked prompt files unchanged. Work occurs in a new isolated documentation worktree branched directly from current main. No cherry-pick, rebase, merge or implementation import.

| PR | Live state / head | Paths / dependency disposition |
|---|---|---|
| #346 | OPEN / `0e568d208f2769037bf4a7265ea4ba6f4609aacc` | 20 historical owner/evidence Markdown files; no proposed UI overlap; unchanged |
| #347 | OPEN / `8240d0878dea335c08ad3216600fc7e545446140` | 10 paths; all ten are also changed by #350, which already incorporates these commits with provenance; do not cherry-pick twice |
| #349 | OPEN / `c40ba4e1ba751b5097061d95d76ce81bbd019c0e` | Two packaging/route evidence files; unchanged |
| #350 | OPEN / `e909b3ef1ade83959f41dd15600937e3fdb4ed9d` | 16 paths; qualified immutable reader/producer, Linux candidate Dockerfile/entrypoint, gate, UI acquisition integration and evidence; UI file also needs labeling changes |
| #351 | OPEN / `00e2b527ea801162d60ca06e7a2b563b18a76be9` | Two exact publication/readback evidence files; unchanged |
| #352 | MERGED / merge `ce7afdde4febea8abe30cd2be4873fef29aeb18e` | Synthetic-only product scope, current main |

#350 and current main share merge-base `612483e570d4b8e1f99d512681ca26b1bc1320cd`; main's only intervening delta is the new product-scope governance file. #350's implementation ancestry is 3ee9331 / 83822b4 (#347 provenance-bearing cherry-picks), 346841f (Linux candidate), e909b3e (evidence). This PR is documentation-only on main, so none of those implementation changes is duplicated. Full literal changed-file inventories for all open PRs are preserved in the JSON companion.

Important implementation paths in #350: `Dockerfile`, `container/entrypoint.sh`, `requirements-linux.lock`, `python/presentation/immutable_synthetic_chart.py`, `python/presentation/test_immutable_synthetic_chart.py`, `python/presentation/realtime_financial_visualization.py`, `src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs`, `Program.cs`, and the producer tests, plus ignore/evidence paths. Current main does **not** contain the immutable reader or the final `synthetic-candidate` Docker target. It has the Windows-only local adapter and older Docker public packaging; it cannot independently supply #350's qualified local Linux chart journey.

## Concrete gap matrix

Static inspection uses current main and exact #350 source. `build_market_figure` has identical AST/content semantics on both branches; no runtime import or host-only certification performed.

| Surface / journey | Already satisfied | Safe local-only change proposed | Block / qualification status |
|---|---|---|---|
| Product documentation | New authoritative synthetic-only scope; real-data work aborted | Link future UI evidence to that scope | No new product or provider approval implied |
| Page identity | Caption says BTC-like Market Simulation / Synthetic Historical OHLCV; illustrative/not-observed warning and synthetic-only caption | Prominent exact SYNTHETIC / SIMULATED ? NOT REAL MARKET DATA message; use synthetic-education subtitle instead of generic Historical Market Research | Needs UI follow-up on settled runtime baseline |
| Instrument | Sole internal ID btc-like-simulation; no BTC/USD or ETH/USD selector option | Friendly synthetic BTC-like display label/help via formatting only; preserve internal symbol/identity | No additional series or schema changes |
| Plot title | None currently | Figure-owned synthetic/not-real title, independent of page banners | Missing label; same UI path modified by #347/#350 |
| OHLC/volume/SMA hover | Values/timestamps and original hover preserved | Synthetic/illustrative/UTC wording in all three trace hover templates; simulated price/activity axis copy | Current templates contain no synthetic marker; no data-value change allowed |
| Dates/units | Frozen UTC date window, no rolling updates; page describes illustrative units | Say dates belong to a simulation, not historical observations; avoid USD/BTC-volume implication | No relabeling as BTC/USD, no new timestamp semantics |
| Native PNG/download | Existing Plotly modebar preserved; no new data download endpoint | Figure-owned synthetic label must survive native PNG capture | **Inferred risk**, not fresh export evidence: current figure has no title/annotation; page banners are outside the figure. No PNG download performed here |
| Candles/volume/selectors/TA | #350/C2B-29 historical Linux evidence covers all 12 combinations; browser 1D/30D SMA false?true?false, hover/zoom/pan/modebar | Preserve arrays, C# SMA and native controls; focused label assertions only | Historical evidence only; not a new C2B-33 browser PASS |
| Zoom reset | Owner-accepted reset on SMA toggle documented | Preserve accepted limitation | No compatibility workaround or framework change |
| Unavailable states | Explicit synthetic chart unavailable; no live/observed prices; gate false | Clarify retry/empty/stale copy is for synthetic data if touched | No activation/automatic retry changes |
| Public eligibility/API | #350 local-candidate opt-in rejects inherited Azure/production/private markers; default unavailable, API 503/unknown 404 historical evidence | None: gates and HTTP semantics unchanged | Actual public chart needs separate owner publication/readiness decision; local opt-in is not a public mode |
| Navigation/help | Architecture already describes public synthetic/private separation | Review publicly visible ML/Market Research copy for synthetic-only educational context | Do not broaden real-data/ML scope or rewrite unrelated architecture |

No claim that the current chart displays real prices: existing captions already disclose simulation. Gaps concern making that disclosure survive isolated plot/hover/PNG contexts and improving coherent educational copy. No hypothetical new export or raw API is proposed.

## Why code changes stop here

C2B-33 explicitly says to stop and propose a safe dependency/rebase plan if changes conflict with open #350, rather than copying/overwriting its unmerged code. Label changes target `python/presentation/realtime_financial_visualization.py`, which #350 already modifies for immutable eligibility/acquisition. A main-only UI patch could be textually disjoint; **no textual Git merge conflict was demonstrated or claimed**. The unresolved issue is selecting a reviewable combined runtime baseline: main lacks the immutable reader/packaging, while a main-based PR importing #350 would duplicate its 16-path implementation and violate the requested narrow delta. Copying main's whole UI into the historical image would remove its acquisition/gate changes; an ad hoc runtime overlay would not be qualification of the exact reviewed combined branch. None was attempted.

The current local-only gate correctly supports isolated testing while preserving disabled public readiness. It does not satisfy the product goal by itself, nor authorize a public-mode chart. Do not flip PUBLIC_RUNTIME_VERIFIED, remove Azure/Development checks or change API routes to obtain a preview.

## Concrete safe follow-up proposal for owner review

**Recommended sequence:** separately review and authorize #350's guarded merge into main (C2B-33 does not authorize it); keep #352's scope document and all gates. Resolve #347's superseded overlap as an explicit owner PR decision, not a second cherry-pick. Then create the labels-only follow-up from updated main. #346/#349/#351 remain independently reviewable; no automatic acceptance/closure follows.

**Alternative requiring explicit authority:** a stacked labels-only PR based on #350's branch, incorporating the governing scope and identifying its parent. Build/qualify that exact combined branch, then retarget/rebase after the parent is resolved. Current C2B-33 requests a narrow PR against main; this report does not silently select that different topology.

Follow-up source proposal, not an applied patch: one shared synthetic disclosure used by page and Plotly title; human-friendly instrument display with unchanged internal value; synthetic/UTC hover and illustrative-unit labels; native TA help; focused tests preserving exact OHLCV/SMA arrays and control configuration. Expected presentation/test paths: `python/presentation/realtime_financial_visualization.py`, `python/presentation/test_local_synthetic_chart.py`, and only directly necessary existing UI-copy tests identified by the follow-up preflight. No calculator, producer, reader, schema, API, startup, package or dependency changes are proposed for labels.

Follow-up acceptance must build a **new local screening image** from the exact settled branch (not mutate/reuse historical D as proof of new labels), using the already finite pinned C# bundle under its approved local-only rights. Record new identity separately; run Linux focused label/integrity/ownership and 503/404 checks, non-root/read-only startup, real browser 1D/30D off/on/off and all displayed SMA equality, simulated title/selector/hover/UTC labels, volume/selectors/zoom/pan/modebar and native PNG label evidence. Missing browser access is INCOMPLETE. If generating or changing assets would require additional authority, stop; no producer change, generator recomputation, artifact promotion or image push is inferred. Future production image qualification/promotion and public accessibility remain separate owner decisions.

## Evidence and exact test status

Historical local image descriptor was inspected only: `sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5`, OCI manifest 2574 bytes, linux/amd64 as accepted in #350/#351. No new image was built, tagged, modified or published. It is the historical candidate, not current main's image and not a modified-label image.

Static checks PASS: main/#350 figure AST equal; no figure title; all three hover templates lack synthetic markers; default modebar download configuration has no figure-owned disclosure; main PUBLIC_RUNTIME_VERIFIED is false; #347's ten paths are all included in #350's sixteen. Raw owner-file hashes and PR inventories recorded in [JSON](release-1.14-chart-mvp-c2b-33-readiness-inventory.json). Exact commands: `git fetch origin main`, `git show <SHA>:<path>`, `git diff <baseline> <head> -- <path>`, `git log`, `git merge-base`, `gh pr view ... --json ...`, static Python AST parsing of Git blobs, `docker image inspect aiq-c2b29:qualified --format '{{json .Descriptor}}'`.

**New Linux tests: NOT RUN. New browser/screenshots: NOT RUN/INCOMPLETE. New resource measurement: NOT RUN.** No substituted Windows execution or temporary proxy. Historical #350 evidence includes 28 focused tests, 12 semantic negatives, five filesystem faults, all 12 selections and real 1D/30D browser screenshots; those results are not relabeled as C2B-33 acceptance. Existing screenshots remain at the C2B-29 evidence directory cited by #350; no new screenshot is claimed. PNG label gap is static inference until native export inspection under follow-up authority.

The only new repository paths are this Markdown and its JSON companion under `docs/roadmap/release-1.14/prompters/`. No application/test/governance file changed; no Docker resource, process, port, network, credential operation, source-archive mutation, Azure access, registry push or PR merge. Documentation worktree retained for owner review; no runtime cleanup required. C2B-32's earlier read-only research was aborted without implementation or repository artifacts; it was not resumed or deleted. Source owner files preserved.

**Decision:** owner review of dependency sequencing and label proposal is ready. Synthetic public chart readiness is not accepted; implementation/new-image qualification remains BLOCKED/INCOMPLETE. Keep this PR OPEN/UNMERGED, PUBLIC_RUNTIME_VERIFIED=False, public APIs unavailable, Azure activation NO-GO and WP07 ON HOLD.

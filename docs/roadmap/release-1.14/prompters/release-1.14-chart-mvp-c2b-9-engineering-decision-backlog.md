# C2B-9 — Engineering decision backlog

Date: 2026-10-16 (America/Sao_Paulo). **Decision backlog, not implementation authority.**
P1-P7/S/B1/T are owner-approved selections; canonical C2B-9 wording still awaits explicit
review/acceptance. No mechanisms below are selected. `PUBLIC_RUNTIME_VERIFIED=False`;
production/export/promotion, public activation and Azure deployment remain **NO-GO**.

| Item | Alternatives, not selections | Evidence required | Decision owner / dependency / next authorized stage |
| --- | --- | --- | --- |
| E1 Trust anchor/manifest | Authenticated reviewed release pins vs signatures; concrete schema/serialization/ID/member layout and key lifecycle unselected | Pinned reviewed source/producer/reviewer chain; exact existing canonical hashes; bounds/unknown/duplicate/member/identity/path/link/tamper/race checks; artifact cannot self-certify trust | Owner approves mechanism and finite design/proof scope after canonical review; no schema, key, producer or export authority now |
| E2 Immutable ownership/promotion | Protected image assets vs operator-controlled immutable release/pointer; neither selected | Public principal cannot write code/assets/anchors/ancestors; coherent pinned bytes, atomic whole-release promotion, interrupted/concurrent reader/revocation proof on actual model | Owner; depends E1/E6/E7; separately scoped ownership/storage design before target integration, no UID/mount/Docker edit now |
| E3 Durable security backend/domain | Protected journal vs SQLite vs existing transaction resource; no store/topology/new paid resource selected | Purpose-minimized control data only; isolated least-privilege ownership/auth, bounded atomic concurrency-safe accounting, approved instance/restart domain, persistence/cold/recycle/clock rollback, unknown state denial and no budget resets | Owner; conceptual S already approved, technical proof still absent; separately authorize finite backend feasibility/qualification after scope/budgets accepted |
| E4 Complete B1 parity | Existing C# producer/calculator plus separately authored full-window reference; no production Python port | Exact 3 complete datasets, 12 calculation/visible windows and unchanged hashes; all 12 complete derived outputs independently equal with frozen parameters/rounding/null/seed/250-warmup semantics; new derived hashes not established | Owner accepts independent numerical evidence; depends E1/E8 and separate production/proof authority; no generator/export execution now |
| E5 Ready schema/bytes/full-read lifetime | Whole qualified fixed window or safe unavailable; no new pagination/rebasing; ready shapes/budgets unselected | Measure full maximum-width outputs, approve schema/byte/CPU/RSS/in-flight/send/cancellation bounds and slow-reader/concurrent-denial/health behavior; bridge/cache caps are not implicit REST budgets | Owner; depends E3/E4/E7/E8; separately scope schema/budget qualification before API/UI integration; endpoints unchanged now |
| E6 Retention/recovery/privacy | Eligible whole-release rollback/image replacement and operator-controlled retirement; immutable asset lifetime separate from 24h request cache | Accepted member/count/aggregate/temp/log/retention/recovery ceilings; no unresolved protection expiry or silent fallback; atomic restart/recovery/audit, minimal identifiers/no private contents | Owner; depends E1/E2/E3/E8; bounded lifecycle/privacy design then separately authorized proof; no backend/retention value selected now |
| E7 Isolation/gate applicability | Proven no-public-spawn reader with retained confidentiality/read protection; no blanket WP07 waiver | Effective private mount/path/env/FD/proc/network/derived-data exclusion; trusted read and full-send admission; explicit G-B0-G-B7 mapping, health and durable abuse proof | Owner accepts security contract and evidence; depends E1/E2/E3/E5; later target authority required, advanced WP07 not resumed |
| E8 Literal scope and finite ceilings | Future exhaustive source/test/output path list and independent review; no directory-wide permission | Owner-accepted implementation and test limits, bounded requests/time/parallelism/CPU/RSS/disk/log/shutdown budgets and abort plan; target/browser costs/constraints verified separately | Owner; policy review plus affected E1-E7 decisions precede implementation; no new source/test/artifact paths or commands granted here |

## Threshold and authorization discipline

All missing response/resource/durability/retention/recovery/target/test values remain
**TBD — measurement required**. Existing candidate limits are not freshly measured
proof, hard CPU enforcement or default new artifact/REST budgets. No affected activity
starts before its scope and finite safety ceilings are separately accepted. Frozen
numerical/data contracts are not TBD and cannot be altered to make a test pass.

The next permissible decision is owner review of the exact canonical C2B-9 proposal
and applicable engineering scope. A future finite local offline proof requires its own
literal-path authority, independent reference, accepted budgets and explicit output/
production/export disposition. Target Azure/browser qualification and deployment/public
activation remain separate later authorities. No implementation or operational run is
authorized merely because an item has a named alternative or dependencies.

Unknown mandatory state -> safe unavailable. Planning rollback is a reviewed documentation
revert; future data/control recovery cannot reset budgets, widen tolerances, generate,
repair or reach a private/provider fallback. No gate is marked passed by this backlog.

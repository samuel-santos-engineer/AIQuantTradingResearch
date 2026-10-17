# Release 2.0 ? Linux Docker / development F1 qualification design

Future procedures only. No model fit, build/container/browser experiment, image publication or deployment is authorized during planning. Never present planned assertions as PASS. Failed gates stop the phase.

## Proposed limits ? owner approval/measurement required

| Area | Ceiling proposal | Failure behavior |
|---|---|---|
| Data |2160 visible1h/90D rows; at most8 frozen features;1295/431/431 eligible split labels|Reject wrong source/grid/count/provenance, nonfinite/missing values and target crossing|
| Model |One candidate/config; at most200 iterations; one thread|Convergence warning/extra fit/unexpected threading fails, no tuning loop|
| Offline evaluation |1CPU,512MiB,60sec wall/30sec CPU|Host-controlled timeout/termination, nonzero exit, no partial report; stop if bound cannot be enforced|
| Served report |256KiB JSON maximum, no raw arrays/pickle/weights|Reject extra fields, wrong hash/version/window or undefined/invalid metrics|
| Local final image |1CPU,512MiB,no added swap,64PIDs,32MiB tmpfs,read-only root,no-new-privileges|Verify actual StreamlitUID/GID1000,CapEff0; image startup UID is not proof|
| Latency |Measure fixed cold/warm sample set/concurrency against baseline; propose chart p95 <= max(3sec,1.2x measured baseline)|Approve budget before execution, not after seeing failures|

These are screening limits, not F1 allocations or kernel process-tree containment certification. Existing root bootstrap drops identity before presentation; reverify actual final-image behavior. Timing/thread and report-size proposals require WP01 approval.

## Test matrix and independent evidence

| Layer | Required proof |
|---|---|
| Formula/contracts |Independently authored tiny label/split/metric/confusion vectors; rational/Decimal expectations where applicable; ties and undefined metrics|
| Leakage |Train-only scaler spy; alter future test values without changing train fit; target-time purge; shuffle/gap/duplicate rejects; last unknown target excluded|
| Reproducibility |Two clean offline runs in exact Linux image; split IDs/labels/predictions and canonical report bytes; approved numeric tolerance; timing outside report|
| Negative paths |Wrong generator/source/window/version/hash, changed link/permissions, oversized/nonfinite/extra fields, single-class training, missing features, nonconvergence, timeout/memory pressure and invalid report isolation|
| C# regressions |Focused Application indicator and Infrastructure12-selection projections/export inside Linux SDK test container|
| Python regressions |Evaluator/report/reader/UI/API tests inside actual candidate or Linux test stage; exact commands/counts/skips/exits and final-image evidence|
| Native browser |Actual Linux/amd64 image, observed trace transitions,12 selections/six indicator families/report states; label/keyboard/focus/layout,hover,zoom/pan,UTC,modebar,volume,System Health|
| HTTP/API |root/liveness200;dataset/candles/indicators503 DATASET_NOT_READY JSON;unknown API404 JSON; rate limits/admin/source/assets preserved; no new data/report API|

Literal full-state proposal:12 selections x64 indicator toggle combinations=768 chart states. If a binary ML results expander is added, both states yield1536 cases per environment. Include explicit off/on/off for each family on1D/30D and all12 exact C# alignments. Gray-code order can reduce redundant reruns, never replace real browser checks. Record expected/executed/passed/failed/skipped counts and representative screenshots; zero unexplained failures/skips. ML remains a fixed1h/90D experiment when chart selection changes.

Run the full matrix locally before development. Estimate cost from measured CPU/time; split development work into bounded batches under observed short/day quota headroom and existing rate limits. If the full required coverage cannot fit the approved schedule, stop for an explicit owner test-scope/release decision. No silent pairwise substitution, paid compute or lucky-pass retry loops. Preserve the known owner-accepted indicator-toggle viewport reset; actual zoom/pan controls must still work.

## Future qualification/promotion sequence

1. Refresh main and exact app identities/digests; recheck workflows/hooks/source/CD and branch protections for production triggers. Snapshot development config/digest and verify rollback access. Production only gets read-only immutable-digest comparison, never tests/mutations.
2. Under scoped implementation authority, produce Linux candidate/tests. Existing dependencies only. Build Dockerfile with explicit Linux/amd64 and synthetic-candidate target; evaluation/tests have no network. Approved build dependency retrieval is not provider access; new packages require authority.
3. Run independent contract/negative, C#/Python tests and resource measurements. Capture provenance,actual UID/capabilities,filesystem bounds. Rebuild/requalify any changed source. Record immutable OCI index/platform manifest/config digests, not just tag/local image ID.
4. Serve exact final image on an unused loopback-only host port with owned container/network/browser. Execute full browser/API matrix and screenshots. Preserve failures; clean only owned resources. Do not inject DOM workarounds, count clicks as state changes or expose LAN/public listeners for local qualification.
5. Only after local acceptance, open bounded phase PR, inspect exact paths/README exclusions/CI/production trigger and merge normally if separately authorized. No failed-check or protection bypass.
6. With separate existing-registry/development-deployment authority, verify registry bytes against certificate; capture fresh rollback; promote the same digest to aiqr112wp05wus27f5eabb5 only. No rebuild/tag promotion/new resources/F1 upgrade/production or CD changes.
7. Fresh browser sessions after restart must show the new image/UI before testing. Repeat full development matrix/API/health/startup/digest checks; capture screenshots using supported native interaction. No Azure training. Compare unchanged production digest/settings read-only.
8. On any failed development gate, restore prior development digest/config, verify rollback digest/liveness, preserve failures and stop. Unknown baseline/insufficient rollback permission blocks deployment. Repair requires explicit source paths and new local image qualification, not incidental deployment edits.

## F1 headroom/availability obligations

Existing WestUS2/F1 is shared capacity. CPU short/day exhaustion can suspend service until reset; cold starts/storage/shared capacity are constraints, not a guaranteed local-core allocation. WP04 must obtain operator-visible current limits,remaining headroom,observation timestamps and predicted qualification cost. Missing/stale headroom evidence blocks active runs; never invent freshness or embed owner credentials. Propose qualification CPU <=10% of observed remaining daily headroom and within short-window limits, with rollback/health reserve; owner must approve concrete schedule after measurement. No always-on training, autoscale,paid monitoring or extra persistence.

No current quota/capacity/cgroup/cross-UID/process-tree proof is claimed. Advanced WP07 remains ON HOLD. Additional containment needs require a separate owner decision, not imported deferred architecture.

## Per-phase evidence and release gate

Record baseline/source/mergeSHA, literal changed paths,approved feature/label/split/model versions,source hashes,dependency pins,exact Linux image digests,resource/UID/capability evidence,commands/exits/test counts,matrix case inventory,screenshots/log categories,measured CPU/memory/timings,quota observation time,previous/final development digests,rollback result,unchanged production and limitations. Never publish personal host paths,names,secrets or environment dumps.

Delivery accepts honest reproducible synthetic evaluation and preserved chart behavior, not a minimum predictive score. Poor results are valid. Successful metrics authorize neither real-market claims,raw public APIs nor production deployment. PUBLIC_RUNTIME_VERIFIED=False remains unchanged. Planning merge grants no implementation authority; pause for owner review.

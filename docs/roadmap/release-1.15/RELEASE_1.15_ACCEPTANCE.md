# Release 1.15 Acceptance Matrix — Initial Planning Baseline

**Status:** NOT YET QUALIFIED. This is an acceptance checklist, not passed evidence. `PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment NO-GO pending separately authorized qualification.

| ID | Requirement | Owning WP | Required proof | Status |
| --- | --- | --- | --- | --- |
| R115-01 | BTC/USD and ETH/USD completed 1m UTC candles, no fabrication | WP03 | Offline ingestion/gap/correction vectors | Pending |
| R115-02 | Bounded 1,000 candles per instrument, atomic SQLite and lineage | WP03 | Crash/recovery/pruning/rollback tests | Pending |
| R115-03 | Indicators and regime 401st-candle readiness | WP04 | Independent numerical vectors and replay | Pending |
| R115-04 | Transformed public values and no private secrets/raw leakage | WP05 | HMAC date rollover and negative isolation tests | Pending |
| R115-05 | Versioned full/unchanged contract, whole-response validation | WP05 | C#/Python fixtures and malformed payload tests | Pending |
| R115-06 | Truthful research/freshness/connectivity/validity statuses | WP05–WP06 | API/UI stale/recovery/disconnect tests | Pending |
| R115-07 | Candles/volume/overlays, 100/300/1,000 presets | WP06 | Real browser in Linux container | Pending |
| R115-08 | UTC viewport persists across refresh; session separation | WP06 | Pan/zoom/retention/reconnect browser tests | Pending |
| R115-09 | One Linux image, two non-root supervised processes | WP02 | PID1/startup/restart and identity tests | Pending |
| R115-10 | Private loopback API, filesystem and secret isolation | WP02/WP07 | Container penetration/negative tests | Pending |
| R115-11 | Measured F1 resource constraints and immutable image provenance | WP07 | Docker profile, digest, documented limits | Pending |
| R115-12 | Runbook, evidence traceability and explicit owner disposition | WP08 | Acceptance report and owner review | Pending |

## Evidence protocol
Each WP must link exact test commands, fixture versions, commit/image digests, outputs, limitations and owner decisions. Failed or skipped checks remain Pending/Fail, never inferred Pass. Separate local Docker acceptance from Azure F1 per-site verification and separate deployment authorization. Preserve Release 1.14 historical qualification. Apply UTF-8 LF to all modified text files.

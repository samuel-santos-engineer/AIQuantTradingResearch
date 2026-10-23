# Release 1.15 WP01 - Downstream qualification matrix

Date: 2026-10-22. Read with [closeout](RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md) and [D1-D7 register](RELEASE_1.15_WP01_DECISION_REGISTER.md). `PUBLIC_RUNTIME_VERIFIED=False`.

## Ownership and gate rules

Each row has exactly one primary implementation/qualification WP02-WP07; dependent WPs consume evidence, not share ambiguous primary ownership. These are future responsibilities, not permission to begin any WP. Resolve foundational contract questions in owner-reviewed WP01 documentation before dependent source work. Naming a future technical owner does not require or authorize starting that WP to obtain WP01 acceptance.

Contract rows identify missing interface/trust decisions; qualification rows identify proofs of an implementation after its contract is accepted. WP01 does not require completed production qualification, live provider tests or Azure deployment. It also cannot close by deferring a foundational interface without an accepted disposition. The [execution-plan dependency graph](RELEASE_1.15_EXECUTION_PLAN.md) and [issue register](RELEASE_1.15_ISSUE_REGISTER.md) remain unchanged. WP08 owns final acceptance and runbook handoff, not implementation of any row.

## Contracts, implementation and observable exit evidence

| ID | Obligation and gate type | Primary WP | Dependent WPs | Observable exit evidence | Required environment |
| --- | --- | --- | --- | --- | --- |
| Q01 | D1 foundational supervisor trust/privilege and fixed-role lifecycle contract | WP02 | WP03, WP05, WP07 | Owner-reviewed bootstrap/runtime privilege, numeric UID/GID/group/capability policy, secret ownership, signal/wait and descendant/no-overlap model; explicit deadlines/backoff, no root fixture assumption | Documentation before runtime code; then final Linux image |
| Q02 | Supervisor implementation and full tree recovery qualification | WP02 | WP03, WP05, WP07 | Witness old role/tree dead and reaped before replace; bounded restart/shutdown, parent/peer failure schedules, no orphan/role overlap; health startup/backoff logs | Isolated Linux/amd64 image; deterministic fault fixtures |
| Q03 | Research/public identity, filesystem, environment and capability boundary | WP02 | WP03, WP05, WP07 | Distinct non-root children; public denial of raw DB/sidecars/checkpoints/watermark/keys/environ/FDs; sanitized public environment; no private API external ingress | Final Linux image, fake secrets and hostile public-UID probes |
| Q04 | Completed-minute adapter and private input contracts | WP03 | WP04, WP05, WP07 | BTC/ETH completed UTC-opening OHLCV validation; future/incomplete/order/duplicate/precision/impossible values rejected; no fabricated gap candles | Offline independent provider fixtures and C# tests |
| Q05 | Quota-aware ingestion, fairness/backfill and actual candidate capability | WP03 | WP04, WP07 | Bounded quota/retry/grace scheduling and gap/correction fixtures; separately authorized real Basic credit/symbol/interval observations, truthful unavailable state | Offline first; live Twelve Data only under separate operational authority |
| Q06 | Atomic SQLite snapshot/version/continuity/event transaction | WP03 | WP04, WP05, WP07 | Crash at each write/commit point; no memory publication before commit; stable generation/version and complete atomic rows/validity/events/recovery intent | Disposable private SQLite in actual Linux image |
| Q07 | Journal/checkpoint/watermark persistence protocol and state interface | WP03 | WP04, WP05, WP07 | Reviewed journal/flush/replace/state protocol; checkpoint after successful commits; private keys/sidecars protected; implementation meets declared crash model; no unapproved cadence constants | Documentation plus local Linux filesystem tests; actual Azure semantics Q19 |
| Q08 | Recovery lineage, corrections and continuity reset | WP03 | WP04, WP05, WP07 | DB-ahead/watermark-ahead/corrupt checkpoint/failed watermark combinations; honest Protected/Degraded/Recovery; quarantine uncertain research, rewarm if evidence insufficient | Deterministic Linux crash/replay fixtures, no production DB |
| Q09 | 1,000-row retention with recovery-evidence protection | WP03 | WP04, WP05, WP06 | Pruning cannot remove active recovery/checkpoint evidence or fabricate rows; independent gap bounds, restart and correction tests | Offline Linux SQLite fixtures |
| Q10 | D3 foundational numerical/algorithm and checkpoint-state definitions | WP04 | WP03, WP05, WP06 | Independently review seeds/scales/rounding/bounds, ATR/width/slope normalization, percentile/equality/zero behavior, persisted state and replay tolerance before algorithm source | Specification and independent reference vectors |
| Q11 | Indicators/regime/trend implementation and replay | WP04 | WP03, WP05, WP06, WP07 | Six indicators on raw private values; null warmup; votes/threshold boundaries; 100+300 prior calibration; 400 not ready/401 eligible; no lookahead; gap/correction/prune replay | Offline independent vectors and final Linux integration |
| Q12 | D4 foundational canonical C#/Python wire and error contract | WP05 | WP03, WP04, WP06, WP07 | Accepted exact field spelling, UTC/numeric encoding, schema compatibility, all collection/byte bounds, empty history and transport/error semantics; current request/identity rules | Language-neutral spec and independently reviewed fixtures before client/server code |
| Q13 | D5 foundational transform/rotation/cache identity | WP05 | WP03, WP04, WP06, WP07 | Exact HMAC domain/message/mapping/rounding, volume/dimensionless policy, key/transform epoch and same-day rotation identity; no same-identity/different-array cache alias | Specification plus independent cryptographic/decimal vectors |
| Q14 | Transform/whitelist/isolation implementation | WP05 | WP06, WP07 | Shared positive daily factor on all price-valued fields, unscaled RSI; approved volume policy; full date/rotation replacement; no raw/key/factor/private diagnostic leak | Offline C#/Python vectors and final-image negative API/log tests |
| Q15 | Bounded full/unchanged validation, caching and independent statuses | WP05 | WP06, WP07 | Read-only loopback full/unchanged parity; stream/parser limits, UTF-8/duplicate-key/null/NaN/order/alignment/identity/error negatives; reject whole response; immutable cache bounds; observation times and last-known preservation | Cross-language offline fixtures and Linux client/server integration |
| Q16 | Production Streamlit controls, snapshots and status integration | WP06 | WP07 | BTC/ETH, 100/300/1,000 retained rows/default 300, 60s coalesced refresh, candles/volume/indicators/events/gaps, truthful transformed labels/statuses, no Python research recomputation | Real browser against final Linux image and validated fake API |
| Q17 | D6 production viewport identity/intent and lifecycle integration | WP06 | WP05, WP07 | x/price-y/volume-y before/pending/after; current identity/revision/mount/command/reset fences; refresh/ack/remount/correction/date/rotation; stale callback rejection, explicit resets and honest expiry | Actual browser, offline races against the production adapter/scheduler |
| Q18 | Reconnect, session/instrument isolation and component resource handling | WP06 | WP05, WP07 | Last-known chart survives disconnect; validated replacement only; no cross-session state/health leak; storage denial/corruption/reclamation, detachment/timer/listener and bounded refresh/cold-start tests | Pinned browser + final Linux image, isolated sessions and deterministic transport faults |
| Q19 | Actual Azure F1 private filesystem/deployment feasibility | WP07 | WP02, WP03, WP05 | Identify actual mount/permissions/locking/fsync/rename/restart semantics and loss modes; no unsupported durability guarantee or paid-resource assumption | Separately authorized Azure tests on intended site/configuration; local Docker is not substitute |
| Q20 | Integrated security and combined-failure recovery qualification | WP07 | WP02, WP03, WP04, WP05, WP06 | Final exact-image identity/network/secret/DTO negatives plus combined transaction/lifecycle/transport failure schedules, evidence-preserving recovery and publication quarantine | Actual final Linux/amd64 image, offline fixture system; Azure only separately authorized |
| Q21 | D7 combined load/resource/availability budgets | WP07 | WP02, WP03, WP05, WP06 | Measured 1/3/5-session plus private research load, cold/warm and long-run variability; independent browser/server accounting, bounded caches and Normal/Elevated/Critical policy; explicit capacity NO-GO if insufficient | Controlled Linux screening, then separately authorized F1 observations; no inferred SLA |
| Q22 | Immutable image provenance, operational evidence and readiness handoff | WP07 | WP02, WP03, WP04, WP05, WP06 | Source/build/dependency/image digests, exact commands/failures/limits, per-site qualification/rollback plan, operator recovery evidence handed to WP08 | Documentation plus qualified Linux image; registry/promotion/deployment require separate authority |

## Release acceptance traceability

| Existing acceptance ID | Evidence rows / owner | Current release status |
| --- | --- | --- |
| R115-01 | Q04-Q05 / WP03 | Pending |
| R115-02 | Q06-Q09 / WP03; Q19 platform proof / WP07 | Pending |
| R115-03 | Q10-Q11 / WP04 | Pending |
| R115-04 | Q03 runtime boundary / WP02; Q13-Q14 projection / WP05 | Pending |
| R115-05 | Q12, Q15 / WP05 | Pending |
| R115-06 | Q15 API / WP05; Q16, Q18 UI / WP06 | Pending |
| R115-07 | Q16 / WP06 | Pending |
| R115-08 | Q17-Q18 / WP06 | Pending |
| R115-09 | Q01-Q02 / WP02 | Pending |
| R115-10 | Q03 / WP02; Q20 integrated proof / WP07 | Pending |
| R115-11 | Q19, Q21-Q22 / WP07 | Pending |
| R115-12 | WP08 final acceptance/runbook/handoff after Q22 and predecessor acceptance | Pending |

No production acceptance row becomes Pass from this documentation PR. Existing S1/S2 evidence narrows feasibility risk only; it is not a substitute for the observable exits above. WP08 consolidates pass/fail/pending evidence, runbooks, recovery/rollback and owner disposition after WP07, and owns no unresolved implementation row here.

## Stop and sequencing rule

Stop at the reported foundational decisions; do not manufacture numerical, schema, HMAC or privilege conventions. Owner disposition of those contracts precedes the affected exact-path implementation plans. This matrix grants no source/CI/schema/provider/Azure execution authority, adds no licensing engineering gate, and changes no predecessor dependency. WP01 remains BLOCKED, #367 OPEN and WP02 unstarted. All authored text uses UTF-8/LF; validate links, consistency and git diff --check before publication.

## Foundational proposal handoff - 2026-10-23

The [D1/D3/D4/D5 candidate specification](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md) provides exact unapproved resolutions for Q01 (D1-A, WP02), Q10 (D3-A, WP04), Q12 (D4-A, WP05) and Q13 (D5-A, WP05). Owner acceptance precedes implementation tests; no row is marked complete by proposal publication. Q07/Q08 include durable transform-config/epoch recovery needed by D5 but remain WP03 obligations; Q15/Q17/Q18 consume the agreed D4/D5 identity. The existing22 rows, their single primary owners, dependent WPs and all12 Pending acceptance rows are unchanged. WP08 owns final evidence/runbook handoff only. No source, numerical/crypto test, kernel probe, provider call or Azure qualification is authorized/executed by this amendment.

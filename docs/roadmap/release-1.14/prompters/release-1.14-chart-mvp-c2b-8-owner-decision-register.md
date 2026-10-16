# Release 1.14 — Chart MVP C2B-8 Owner Decision Register

**Decision status:** OWNER SELECTIONS RECORDED — governance reconciliation and implementation authorization remain separate.
**Recorded date:** 2026-10-16 (owner timezone: America/Sao_Paulo).
**Project:** AIQuantTradingResearch
**Scope:** Public Preview, independently generated fixed-window synthetic BTC-like historical OHLCV.
**Basis:** C2B-6 P1–P7 snapshot-governance amendment proposal, C2B-7 snapshot implementation specification, and the owner's explicit C2B-8 selections in this conversation.

## 1. Owner's decisions


| ID | Clause / choice                               | Owner selection                 | Effect and retained conditions                                                                                                                                                                                                                                                                                       |
| ---- | ----------------------------------------------- | --------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| P1 | Narrow offline synthetic-generation exception | **APPROVED**                    | Owner-controlled offline execution of unchanged frozen C# generator may be contemplated only under separately authorized release operations. No anonymous-request, startup, deployment, background, repair or recovery generation. No production/export authority granted by this register.                          |
| P2 | Artifact provenance, manifest and trust       | **APPROVED**                    | Immutable content-bound release identity, independently trusted anchor, full source/fingerprint/member validation, and public UID unable to modify trust anchors or release assets. Concrete format, anchor and ownership implementation remain unselected.                                                          |
| P3 | Offline precomputed analytics exception       | **APPROVED**                    | B1 offline C# indicator calculation, with independent complete parity across all twelve windows; no production Python port or change to frozen numerical semantics. No precomputation or export is authorized yet.                                                                                                   |
| P4 | Fixed windows and REST compatibility          | **APPROVED**                    | Preserve fixed coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, three intervals, twelve qualified end-anchored selections, six GET routes, validation and safe unavailable behavior for unqualified ranges. No rolling/live semantics or silent truncation/rebasing.                                         |
| P5 | Isolation and bounded read admission          | **APPROVED**                    | No public Worker/generator/provider/private fallback; negative isolation proof and bounded verification, read, serialization and full response lifetime. Existing runtime false gate remains; resource and abuse qualification still required.                                                                       |
| P6 | Retention, promotion, rollback and recovery   | **APPROVED**                    | Privileged whole-release promotion/revocation, pinned immutable reader identity, no public repair or automatic fallback, fail-closed 503 `DATASET_NOT_READY` for unqualified data. Artifact retention and recovery budgets still require explicit measurement and acceptance.                                        |
| P7 | Separate qualification and authority gates    | **APPROVED**                    | Exact governance reconciliation, implementation, offline production/export, local qualification, target Azure qualification, deployment and public activation remain distinct scoped approvals. Planning/merge alone cannot activate any gate.                                                                       |
| S  | Durable security-control state                | **APPROVED (narrow exception)** | Permit separately controlled, persistent **security/abuse-accounting** state solely for durable admission and protection, isolated from immutable market data and private research. No mutable public market-data state; no automatic counter reset, fail-open on unknown state or backend selection by implication. |
| B  | Snapshot publication variant                  | **B1 SELECTED**                 | Candles **plus independently verified offline-precomputed indicators**, not B0 candles-only. Indicator readiness remains unavailable until complete parity and integrity gates pass.                                                                                                                                 |
| T  | Unmeasured thresholds                         | **APPROVED: REMAIN TBD**        | Response bytes, CPU/RSS, concurrency, send/cancellation, admission, durability, retention, disk/temp-space, recovery, target-runtime and test ceilings must be measured and separately accepted before the relevant activity. No guessed values.                                                                     |

**Owner comments:** None.
**Deferred selections:** None.
**Requested revisions:** None.
**Rejected selections:** None.

## 2. Exact interpretation and outstanding engineering decisions

The owner approved the *recommendations* as presented. This record does not silently expand them into permissions absent from the underlying C2B-6 P1–P7 text. Any conflict with existing governing documents must be reconciled in an explicit, reviewed policy change before implementation. The accepted narrow security-state exception resolves the conceptual conflict between immutable shared market data and mandatory durable abuse accounting, **but does not qualify a particular store or topology**.

Still unresolved for a future separately authorized design/qualification step:

1. Trusted release-anchor mechanism, manifest serialization/schema, producer/verifier identity, storage ownership and promotion paths; artifact bytes cannot self-certify trust.
2. Durable abuse-accounting backend, authoritative instance/restart domain, ownership, persistence, cold/recycle behavior, clock rollback and fail-closed admission; no new paid resource is implicitly approved.
3. Independent full-window indicator parity oracle and derived fingerprints for all twelve B1 selections; preserve 250 warmup rows as calculation-only.
4. Concrete ready-response schemas and measured maximum response sizes, full-send concurrency and cancellation budgets, immutable release retention/rollback counts and disk/temporary-space ceilings.
5. Exact literal-path implementation/test allowlist, finite resource budgets, target Azure F1 and real-browser qualification procedures, and the applicable WP07 gate reconciliation.

If any required control or budget remains unknown, the corresponding implementation/qualification step must **STOP** rather than treating this decision record as evidence of readiness.

## 3. Frozen boundaries and authorization ledger

- Synthetic identity: `aiq-synthetic-ohlcv-v1`, seed `114`, BTC-like simulation; existing canonical bytes, window identities and accepted fingerprints remain frozen.
- Fixed UTC coverage: `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`; intervals `1h`, `4h`, `1d`; 1/7/30/90-day windows; exactly 250 calculation-only warmup candles.
- Public label: **BTC-like Market Simulation — Synthetic Historical OHLCV**.
- Twelve Data remains private; Vike remains inactive; no public observed/provider market data or private derived artifacts.
- Advanced WP07 lifecycle work remains **ON HOLD**.
- `PUBLIC_RUNTIME_VERIFIED=False` remains unchanged.
- Public generator activation, snapshot creation/export/promotion, production implementation, Azure changes and deployment are **NOT AUTHORIZED by this register**.
- Public dataset/candles/indicators remain unavailable until separately qualified and activated under explicit authority; do not misrepresent a planning decision as readiness.

## 4. Recommended controlled handoff to Codex

1. Preserve this decision register verbatim as an owner record, and compare each approval against exact C2B-6 P1–P7 language and C2B-7 design.
2. Prepare a **documentation-only** reconciled governance amendment explicitly marking the owner-selected clauses and the narrow security-state exception. Identify governing files and clause conflicts precisely; do not imply that accepting a PR alone authorizes runtime changes.
3. Prepare a decision/evidence matrix for unresolved trust/ownership/backend/limits/allowlist details, with `TBD — measurement required` wherever evidence is missing.
4. Open a separate reviewable PR; do not merge unapproved policy edits or modify code, artifacts, generator, protected WP07 state, secrets or Azure resources.
5. Seek separate owner authority for a finite, local offline proof only after policy reconciliation and prerequisites are accepted.

## 5. Decision provenance

**Source of owner approval:** Explicit selections submitted in this ChatGPT conversation on 2026-10-16, all P1–P7 approved, S approved, B1 selected, T approved with TBD retained; no comments. This document is a record of those choices, not a claim that the GitHub repository's governing policy has already been amended.

**Status:** Owner decisions recorded; engineering design and operational qualification pending. **Public activation and Azure deployment: NO-GO.**
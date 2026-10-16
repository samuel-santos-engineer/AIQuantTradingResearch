# Release 1.14 Chart MVP — C2B-11 Engineering Owner Decision Register

**Decision date:** 2026-10-16 (America/Sao_Paulo)  
**Decision source:** Owner selections submitted in this conversation, based on the E1–E8 recommendations in [PR #340](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/340).  
**Status:** Eight conditional design-direction approvals; no implementation or operational authority.  
**PR #340 disposition:** Open and unmerged; these decisions do **not** authorize merging it.  
**Owner comments:** None.

## Decision summary

| Item | Engineering topic | Owner decision |
| --- | --- | --- |
| E1 | Artifact trust and manifest | **Approve recommendation (conditional)** |
| E2 | Immutable image ownership | **Approve recommendation (conditional)** |
| E3 | Durable security accounting | **Approve recommendation (conditional)** |
| E4 | Independent B1 indicator parity | **Approve recommendation (conditional)** |
| E5 | REST response design | **Approve recommendation (conditional)** |
| E6 | Promotion, retention and recovery | **Approve recommendation (conditional)** |
| E7 | Public isolation and security gates | **Approve recommendation (conditional)** |
| E8 | Finite offline proof sequence | **Approve recommendation (conditional)** |

## Exact owner selections and retained conditions

### E1 — Artifact trust and manifest

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Reviewed first-party digest pin, deterministic canonical manifest core, no compression and no initial signing secret. Signatures remain a future alternative.

**Retained conditions:** Trusted source revision, producer/reviewer, manifest schema and threat assumptions still require acceptance.

### E2 — Immutable image ownership

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Image-bundled code, release pin and synthetic assets not writable by the serving UID; whole-image promotion and rollback.

**Retained conditions:** Actual UID, ancestor permissions, mounts, release consistency and Azure deployment behavior must be proven.

### E3 — Durable security accounting

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** SQLite as a security-only local proof candidate, with a protected-journal comparator and a single-instance persistence domain to verify.

**Retained conditions:** No effective backend, Azure topology, retention, storage ownership or new paid service is selected.

**Interpretation:** This approves investigating SQLite as a candidate, **not** selecting SQLite as the production security backend or assuming a single-instance deployment is verified.

### E4 — Independent B1 indicator parity

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Use the existing Infrastructure.Tests friend assembly and independently authored test-only CPython integer/rational oracle for complete exact parity.

**Retained conditions:** All three datasets, twelve windows, derived values, nulls and fingerprints must be independently verified. No production Python port.

### E5 — REST response design

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Serve complete qualified fixed-end windows without pagination or truncation, with illustrative release/version-bound candle and indicator response structures.

**Retained conditions:** Actual ready schemas, maximum response bytes, send/cancellation and resource ceilings remain unapproved.

### E6 — Promotion, retention and recovery

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Operator-controlled whole-image release promotion, revocation and eligible rollback; separate minimal fail-closed security-control recovery.

**Retained conditions:** No automatic fallback, reset or repair; retention, capacity, atomicity and recovery budgets remain TBD.

### E7 — Public isolation and security gates

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Dedicated public no-spawn/no-provider reader context, retaining applicable G-B0–G-B7 obligations and negative isolation tests.

**Retained conditions:** No blanket WP07 waiver. Azure F1 isolation and zero-added-cost feasibility must be proven before activation.

### E8 — Finite offline proof sequence

**Owner decision:** Approve recommendation (conditional).

**Approved design direction:** Start with a separately authorized, bounded in-memory friend-test and independent oracle proof, without exporter, snapshot materialization or integration.

**Retained conditions:** Candidate file paths and commands are proposals only; execution requires separately approved literal scope and finite safety ceilings.

**Interpretation:** This approves the sequence as a proposed approach, **not** permission to create test files, run tests, invoke a generator, generate snapshots or export artifacts.

## Governance and authorization boundary

1. All E1–E8 recommendations are conditionally approved **as design directions only**. The listed qualifications, unmeasured thresholds, technical mechanisms, independent evidence and later owner acceptance remain open. No pending prerequisite is deemed satisfied by these approvals.
2. Previously accepted P1–P7, S and B1 product/governance decisions are not reopened or changed. Threshold decision T remains **TBD — measurement required**; this register invents no numerical limits.
3. This register is a conversation-created owner-decision artifact. It is **not** a repository commit, canonical-policy amendment, PR merge or implementation authority. PR #340 must remain open and unmerged unless the owner separately authorizes its merge.
4. **Not authorized:** source/test/package/schema changes; tests or runtime experiments; offline synthetic generation; snapshot materialization, export, promotion or recovery; public dataset activation; Azure configuration, deployment or target/browser experiments; private-provider access; or resumption of advanced WP07 lifecycle work.
5. The existing frozen `aiq-synthetic-ohlcv-v1`, seed 114, fixed UTC coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, twelve fixed selections, 250 calculation-only warmup rows and exact analytics/fingerprint requirements remain unchanged.
6. **`PUBLIC_RUNTIME_VERIFIED=False`**. Public dataset/candles/indicators remain safely unavailable; public activation and Azure deployment are **NO-GO**. Advanced WP07 remains **ON HOLD**.
7. Any future finite offline proof requires a **separate, explicit owner authority** specifying exact permitted paths, operations, independently reviewed verification, bounded CPU/RSS/time/disk/log/response budgets, cleanup, failure/abort behavior and non-export restrictions. No such authority is granted here.

## Suggested next governance checkpoint (not authorized by this register)

Review this exact register and, if desired, separately authorize a documentation-only C2B-11 PR that records the conditional decisions while preserving all outstanding proof obligations. A later narrowly scoped qualification authority must be independently approved before any executable work.

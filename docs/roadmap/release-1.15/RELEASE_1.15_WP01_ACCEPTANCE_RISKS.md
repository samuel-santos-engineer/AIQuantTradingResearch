# Release 1.15 WP01 - Acceptance, evidence and unresolved risks

Date: 2026-10-21. Issue #367 / milestone #66. Status: planning review, not implementation acceptance. See the [exact-path feasibility plan](RELEASE_1.15_WP01_FEASIBILITY_PLAN.md) and [proposed contract](RELEASE_1.15_WP01_ENGINEERING_CONTRACT.md).

## Acceptance reconciliation

No Release 1.15 runtime acceptance item is passed by this PR. The existing [acceptance matrix](RELEASE_1.15_ACCEPTANCE.md) remains unchanged and Pending. Historical Release 1.14 evidence is a preservation baseline only.

| Acceptance ID / owner | Required deterministic or bounded evidence | Current disposition |
| --- | --- | --- |
| R115-01 / WP03 | Completed minute BTC/ETH fixtures; incomplete/future/misaligned/duplicate/impossible OHLCV rejection; real gaps, fairness and bounded backfill | Pending; daily adapter is not minute support |
| R115-02 / WP03 | 1,000-row pruning, atomic commit, generation/version persistence, correction intent, checkpoint/replay, DB/watermark crash matrix | Pending; actual filesystem durability unproven |
| R115-03 / WP04 | Independently derived six-indicator vectors, percentile/equality cases, 400 not ready versus 401 eligible, no lookahead, replay after pruning/correction | Pending; exact numerical closures required |
| R115-04 / WP05 | Independent HMAC vectors, daily rollover/shared factor, rotation identity, raw/key/sidecar/log isolation negatives | Pending; transformation is not a confidentiality substitute |
| R115-05 / WP05 | C#/Python full/unchanged parity; whole-response rejection of bad UTF-8, duplicate keys, size/row bounds, schema, instrument, epochs, generation/date/version, order/alignment, null/NaN and invalid annotations | Pending; compatible optional fields must not weaken required-field validation |
| R115-06 / WP05-WP06 | Independent stale/recovery/connectivity/validity state combinations; timeout preserves last accepted history; fresh health outside shared cache | Pending; no network success implies Current |
| R115-07 / WP06 | Actual Linux browser candles/volume/overlays, 100/300/1,000 counts, hover, modebar, UTC real gaps and visible off/on/off state | Pending; screenshots/clicks alone insufficient |
| R115-08 / WP06 | S1 and later integration: absolute UTC viewport, correction/date/generation/remount, retention, old-event rejection, two-session isolation and no interaction fetches | Pending; native event path unresolved |
| R115-09 / WP02 | S2 and final image: PID1, two distinct non-root children, bounded independent restart, no overlap and owned-tree termination | Pending; supervision trust model unresolved |
| R115-10 / WP02-WP07 | Public UID denied private files/sidecars/secrets/process channels; loopback-only API; raw DTO/error leakage and hostile request tests | Pending; localhost alone insufficient |
| R115-11 / WP07 | Final Linux image digest, actual CPU/memory/startup evidence, 1/3/5 session observations and separate Azure F1 mount/resource proof | Pending; no benchmark or F1 capacity claimed |
| R115-12 / WP08 | Traceable commands/artifacts/limits, safe operations/recovery runbook, explicit owner acceptance and separately authorized publication | Pending; this PR must remain open for review |

## Technical risk and decision matrix

| Risk | Evidence now | Required closure / release impact |
| --- | --- | --- |
| Browser range events and figure remount | Installed Streamlit includes changing figure spec in element identity; native relayout callback not established | S1 first; supported component only after separate disposition. Release 1.14 reset waiver does not apply |
| Cross-UID supervisor, bootstrap and process-tree ownership | Desired child identities documented; existing entrypoint is not this contract | D1 before runtime code. Prove signaling/reaping/restart and secret inheritance; no silent root/capability workaround |
| Azure private SQLite storage | Existing DELETE factory inspected; no current Azure probe performed | D2 plus separately authorized actual mount/locking/flush/rename/permission/restart evidence. No assumption that WAL or local Docker proves durability |
| Minute data and quota capability | Client/normalizer currently daily close observations | WP03 offline minute adapter plus separately authorized live capability observations; Basic limits/grace/fairness unmeasured. No fabricated source rows or licensing-specific milestone |
| Regime/replay numerical ambiguity | Six indicators and 401st-candle rule specified; percentile/normalization details absent | D3 independent specification and reference vectors before affected implementation |
| Transport limits and schema compatibility | Conceptual 1.0; existing synthetic validators are not the proposed API | D4 freeze encoding, zero-row semantics, collection bounds and optional-field handling; cross-language negative parity |
| HMAC rotation/cache collisions | Day/generation/version identity specified, exact key-change behavior unresolved | D5 ensures changed transformation cannot reuse accepted identity. Never expose raw values/key/factor as a fallback |
| Resource pressure, cache references and F1 suspension | Targets and 12-entry cache are hypotheses | S2 screens only; WP07 establishes actual budgets and resource shedding. No promise of 5-session capacity or OOM recovery |
| Tooling/dependency availability | Existing source/tests inspected; no build/restore/browser/harness executed | Verify existing pinned offline assets before spike authority. Missing dependencies stop execution; no automatic install |
| Recovery evidence contradiction | Best-effort external watermark cannot be transactionally coupled to DB | Explicit DB-ahead/watermark-ahead/degraded/corrupt checkpoint/correction/prune cases; quarantine uncertain research, never invent rollback proof |

These are scoped technical proof obligations, not approvals, and no new licensing-specific implementation gate is created. Unresolved questions block their affected source slices; they do not justify source experiments under documentation authority.

## Verification performed for this planning PR

Repository preflight: primary checkout clean on `main` at `137bb3a8cb1e48dbdfd2650ed3992d45f269cb0e`; separate documentation branch from that exact commit. Read governing prompt, LF policy, Release 1.15 roadmap/acceptance and historical architecture, and Release 1.14 closeout. Inspected current contracts, adapters, SQLite, Worker framework reference, container startup, presentation code and existing tests. Read issue #367 and merged PR #377 using GitHub CLI. These establish the planning baseline only; no Azure state was queried.

Commands/evidence categories:

- `git status --short`, `git rev-parse HEAD`, `git worktree list`: baseline and isolation checks.
- `gh issue view 367 --json title,state,milestone,url` and `gh pr view 377 --json state,mergeCommit`: issue OPEN/milestone 66 and merged baseline match.
- `rg`/file reads: existing paths and contract differences; installed package metadata/implementation inspected without runtime qualification.
- Documentation checks before publication: verify the four-file inventory, UTF-8 decoding, no CRLF/lone CR, final LF, relative Markdown targets, existing read-only/run-only paths, proposed-new paths absent at baseline, and `git diff --check`.

Only document validation applies to this change. No product test command, restore, build, Docker start, browser experiment, provider request, fault injection or Azure operation was executed. Therefore product test counts, browser ranges, resource measurements and feasibility results are **NOT RUN**, not zero-failure passes. Later evidence must identify commit/image/package/fixture versions and actual outputs; never borrow predecessor totals as new tests.

## Review and next authority

Owner review should accept or amend the contract proposal, resolve D1-D7 in the owning work packages, and authorize an exact S1/S2 source boundary before any harness is written. Execution and fault injection need bounded commands and explicit operational authority after harness review. Later implementation PRs must enumerate their own literal files; the roadmap and this proposal are not broad mutation grants.

This planning PR contains only the four documents listed in baseline reconciliation. It does not change source, project/dependency files, runtime flags, README, historical evidence or deployment configuration; does not close #367; and does not qualify WP01's pending spikes. Keep the PR OPEN/UNMERGED for review.

`PUBLIC_RUNTIME_VERIFIED=False`. Release 1.15 public activation/deployment NO-GO. No change to the already accepted Release 1.14 showcase or its historical status.

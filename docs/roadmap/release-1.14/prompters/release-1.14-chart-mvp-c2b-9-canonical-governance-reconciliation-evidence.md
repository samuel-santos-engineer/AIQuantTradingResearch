# C2B-9 — Canonical governance reconciliation evidence

Date: 2026-10-16 (America/Sao_Paulo).
**Owner selections approved; proposed canonical wording pending explicit owner acceptance.**
No implementation/runtime authority. `PUBLIC_RUNTIME_VERIFIED=False`; public data stays
unavailable, public activation/Azure deployment NO-GO and advanced WP07 ON HOLD.

## 1. Authority, safe checkout and guarded merges

The current C2B-9 authority explicitly supersedes the earlier instruction to leave
PR #337 open, accepting exact reviewed PRs #337 and #338 for guarded sequential merge.
No approval was inferred from earlier local documents. Work was confined to the
canonical development checkout `C:\projects\github\AIQuantTradingResearch`, verified
on known planning history/main; protected WP07 worktree, artifacts, archive and internal
references were not inspected or modified.

| PR | Accepted/observed head and scope | Merge result |
| --- | --- | --- |
| [#337](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/337) | `d5ae5624c99f5a4ba0d54c4d48f3d172a5c1ceb6`; exactly 3 C2B-7 Markdown files: authority, readiness evidence, implementation specification | MERGED first, ordinary merge commit `a9d1ca05b7c5d097dd5cd094bf94fc70c4dfe4c2`, `2026-10-16T00:55:03-03:00` |
| [#338](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/338) | `13d51135fdbd32985973a1ca7797ff1f8e6eee64`; exactly 3 C2B-8 Markdown files: owner register, resolution evidence, amendment proposal | MERGED second, ordinary merge commit `b1fad6255919936dc604888be57d87229a5d910e`, `2026-10-16T00:55:34-03:00` |

Both original PR bases were `3891d1da205de28fb7689436593bfac55b773d60`, target main.
Live heads matched accepted heads; inventories/content and whitespace were verified.
No code, test, workflow, runtime or dataset changes. Protections: enforced administrators,
required conversation resolution; zero required approving reviews, no submitted reviews,
no configured required status checks and no repository rulesets. Do not read absent
reviews/CI as an approving review or CI success. Zero unresolved threads for either PR.
Merge commits were permitted and guarded by exact accepted heads; no bypass, force,
branch deletion, issue closure or protection change.

After #337, refreshed main to `a9d1ca05...` and re-evaluated #338 unchanged against it.
GitHub briefly reported UNKNOWN while recalculating, then CLEAN/MERGEABLE. No merge was
attempted during UNKNOWN. The original three-file patch remained exact and disjoint;
behind-base status required no rewrite or conflict resolution. Rechecked protections,
reviews/checks and threads before #338. Both ancestry checks passed after the second merge.

Synchronized main by safe switch/fast-forward: empty porcelain, main == origin/main ==
`b1fad6255919936dc604888be57d87229a5d910e`, ahead/behind **0/0**. Both merge histories
are ancestors. Created `planning/release-1.14-chart-mvp-c2b-9` only after that verification.
The sole current untracked authority was temporarily preserved outside the checkout,
then restored on the new branch. No reset, stash, clean or unrelated cleanup.

## 2. Exact owner record and unchanged historical evidence

Verified **raw Git blob bytes**, not terminal-decoded or working-tree-normalized text,
at accepted #338 head. Register path:
`docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-8-owner-decision-register.md`.
Git blob `af2aaf63c7289e0457e9475e4ec0b90eb25bbaff`, **9,735 bytes, no final LF**,
SHA-256 `AF1CCAE6E30B2482612BF87317253D1E051EE5D784EA6CEB6B8B6735397B1BB8`.
This is an exact match; no newline exception or reconstructed bytes needed.

P1-P7/S/B1/T approval provenance is that owner-supplied register. C2B-5/6/7/8 documents,
historical WP04 authority and the register are preserved byte-for-byte in Git relative
to the merged base. Prior proposed/not-effective statements remain historical; the new
canonical text records current selections without retroactively enacting older proposals.

## 3. Exact seven-path mutation inventory

Four existing authorized canonical documents:

1. `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
2. `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
3. `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
4. `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

Three new Markdown files under `docs/roadmap/release-1.14/prompters/`:

5. `release-1.14-chart-mvp-c2b-9-canonical-governance-reconciliation-evidence.md`
6. `release-1.14-chart-mvp-c2b-9-engineering-decision-backlog.md`
7. `release-1.14-chart-mvp-c2b-9-merge-pr337-pr338-canonical-governance-reconciliation-codex.md`

The optional authority preserves the supplied local record's wording, with trailing
Markdown whitespace normalized and a final LF. The attachment's duplicated `#337#337`
and `#338#338` display references correspond to single PR numbers in the local record;
no substantive authority/order/exclusion was changed. Original local bytes remain in
temporary preservation. No historical prompter is reformatted or edited.

## 4. Per-file actual before/after clause map

All new wording expressly remains pending explicit owner review/acceptance of the
C2B-9 PR. Neither opening/merging planning evidence nor accepted selections grant runtime
permission. Only the four documents above are changed; governing source/test lists are not expanded.

| File / original clause | Proposed narrow change | Selection mapping |
| --- | --- | --- |
| Definition, status/authority | Add current pending-effectiveness cross-reference; preserve dated historical baseline/authority below | C8 / P7/T |
| Definition, complete-generation rule (base :106) | Retain cold Worker rule; identify separate owner-selected offline B1 exception needing canonical acceptance and distinct production/export authority | C1-C3 / P1-P3/B1 |
| Definition, supplemental no-export/cache criteria (base :422) | Retain prohibitions under those criteria; link the separate pending context, not an ordinary cache hit/retention extension | C1/C7/C8 / P1/P6/P7/T |
| Execution plan, authority header | Current proposal status, false gate/holds, historical scope interpretation, no new source permission | C8 / P7/T |
| Execution plan, WP02 | Add forward-looking offline C# exception while preserving existing Worker/seed/bytes/coverage/cache and no startup/background/recovery generation | C1 / P1 |
| Execution plan, WP04 | Add future B1 timing exception and independent 12-window proof; existing Worker per-response and candle/cache CalculationVersion null unchanged; no historical authority edit | C3 / P3/B1 |
| Strategy, Section 10 cached-hit/export clause | Retain existing cache-hit rule and prohibition; point to separate pending Section 12 context | C1/C7/C8 / P1/P6/P7 |
| Strategy, Section 11.1 rolling wording | Replace conflicting rolling requirement with fixed exclusive reference end/max 90 visible days; B1 selected, no SQLite/daily implementation permission | C4 / P4/B1 |
| Strategy, Section 11.4 storage/scheduler and decision disposition | Require immutable storage/restart integrity proof, no daily/rolling scheduler; durable security backend remains unselected; acknowledge selected principles without readiness | C2/C4/C5/C7/C8 / P2/P4/P6/P7/S/B1/T |
| Strategy, new Section 12.1-12.8 | Exact canonical C1-C8 reconciliation summarized below, with explicit pending-effectiveness and no-runtime-authority clause | All P1-P7/S/B1/T |
| Manifest, new current C2B-9 documentation authority mapping | Four canonical docs plus C2B-9 evidence prefix only; older current/present labels are historical; preserve every source/test list unchanged and no new implementation/output paths | C8 plus authority cross-map for all selections |

### C1-C8 completeness and retained boundaries

| Clause | Owner choice reflected | Still withheld / unchanged |
| --- | --- | --- |
| C1 | Narrow owner-controlled offline unchanged C# release context | No current generation/export/promotion/producer paths; no public/startup/background/recovery generation |
| C2 | Immutable identity, independently trusted external anchor, complete member/provenance checks, non-public-writable code/assets/anchor/ancestors | No pins/signatures/schema/keys/principal/mount/UID/path choice or qualified immutability claim |
| C3 | B1 offline C# analytics, all twelve complete independent outputs | 250 calculation-only warmup, fixed numerical/null/rounding/seed/gap semantics; existing Worker/cache and no Python port preserved; parity unqualified |
| C4 | Fixed `[2025-10-26, 2026-10-01)` and 3x4 selections, six GET routes, exact simulation label and safe errors | Unqualified valid ranges -> null/503; no truncation/rebasing/rolling/pagination/live/provider claims |
| C5 | Durable mutable security/abuse state only; conceptual shared-state conflict resolved | No mutable market/private state; no backend/topology/paid resource/retention choice; unknown state denies, no reset by restart/client/release rotation |
| C6 | Effective confinement and admission through full send/cancellation/health; applicable G-B0-G-B7 | No public Worker/generator/provider/private fallback, gate waiver, implementation fix or advanced WP07 revival |
| C7 | Separate immutable lifecycle and operator-only coherent whole-release promotion/revocation/eligible rollback | 24h request-cache unchanged; no public repair/automatic older-file fallback; budgets TBD; missing/untrusted data unavailable |
| C8 | Separate policy/implementation/production/export/local/target/browser/deployment/activation authorities | Unmeasured thresholds TBD; no literal source allowlist expansion, runtime gate change or claimed qualification |

## 5. Fresh checks and inherited tests

Fresh: both live PR heads/inventories/content/protections/reviews/checks/conversations;
ordered exact-head merges and #338 re-evaluation; register blob hash/length/final-newline
check; merge ancestry; clean main equality/0-0; actual-clause comparison to C1-C8 and
the owner register; seven-path Markdown-only allowlist; relative links/new headings;
all original 64-hex fingerprints preserved; manifest source/test literal lists unchanged;
historical prompters/source/tests/gate files unchanged; `git diff --check`.

Audit totals: **6** original 64-hex fingerprint occurrences preserved in the four
canonical documents, **63** source/test/runtime literal references unchanged in the
manifest, and **6** newly added relative Markdown links resolved, including Section 12
heading anchors. All eight new Section 12 clauses were found. This is document/source
preservation evidence, not a new generation or golden-vector qualification run.

No runtime tests rerun or required for this docs-only change. Inherited only: full
Python **162 passed/11 skipped**, .NET **620 passed**, C2B-7 **16 focused local tests**.
No new Azure F1/browser, B1 parity, immutable ownership, durable-store or resource evidence.
No code/schema/fixture/data output, generator/provider call, load/fault/native experiment,
Docker/credential/Azure change, deployment or activation. No Project metadata changed.

## 6. Retained conflicts, STOP findings and next decision

No unresolved merge or scope blocker remains. GitHub's transient UNKNOWN was resolved
before the second merge, not bypassed. Historical pending labels/authorities remain
dated records; current header notes delimit their scope. The rolling requirement's
conflict is addressed in the actual Section 11.1 clause, not left as contradictory new
policy. Broad mutable-state wording in historical C2B-7 is not rewritten; Section 12.5
gives the selected narrow future-context reconciliation and preserves market/private immutability.

Engineering E1-E8 remains blocked on concrete designs, independent complete B1 parity,
scope and accepted finite budgets. See the companion backlog for alternatives, owner,
dependencies, evidence and next stage. No technical conflict is disguised as resolved
by changing documentation; current public data remains unavailable.

Next step: owner review/acceptance of this exact canonical proposal. A later finite
local offline proof needs its own literal mutation/test/output scope, independent
verification and accepted ceilings; export/promotion/target/browser/deployment/activation
remain distinct authorities. Existing safe unavailable behavior is unchanged. Declining
or normally reverting this proposal requires no operational migration or recovery.
The C2B-9 PR must remain **open/unmerged** under this authority; implementation and
public activation/Azure deployment remain **NO-GO**.

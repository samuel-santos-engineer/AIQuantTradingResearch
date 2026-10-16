# Release 1.14 Chart MVP — C2B-11 Codex authority
## Guarded merge of accepted PR #340 and documentation-only owner decision registration

**Authority date:** 2026-10-16 (America/Sao_Paulo).
**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.
**Owner authorization:** The owner explicitly approved **PR #340** and requested this next Codex authority. The owner separately conditionally approved the E1–E8 **design directions**, not their proof or implementation. This document authorizes a guarded merge of the exact accepted PR and a new **documentation-only** C2B-11 decision-registration PR. It does not authorize execution of candidate commands, code changes, tests, snapshots, exports, promotion, Docker/Azure changes, deployment or public activation.

## 1. Immutable boundary and safety

1. `PUBLIC_RUNTIME_VERIFIED=False`. Dataset/candles/indicators remain safely unavailable (`503 DATASET_NOT_READY` for valid unqualified requests); public activation and Azure deployment **NO-GO**; advanced WP07 **ON HOLD**.
2. Preserve `aiq-synthetic-ohlcv-v1`, seed 114, `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 1h/4h/1d × 1/7/30/90-day fixed-end selections, 250 hidden calculation-only warmup, all canonical hashes/fingerprints, frozen C# indicator semantics and all six REST routes. No rolling/current/observed claims; no Twelve Data/Vike/private fallback.
3. Never touch, clean, reset, stash, amend or move the protected dirty WP07 checkout or archive. Use a separately verified clean checkout; stop if uncertain.
4. Do not edit production source, tests, configs, Dockerfiles, CI, schemas, fixtures, packages, snapshots, cloud/Project metadata, historical authorities or canonical governing files under this authority. No branch protection bypass, force-push, unrelated cleanup or automatic branch deletion.
5. All E1–E8 are **conditionally approved design directions only**. The actual digest pin, manifest schema, trusted reviewer/build chain, effective immutable image ownership, durable security backend/domain, parity oracle results, ready API schema, lifecycle budgets, target isolation and finite execution ceilings remain unselected/unproven as stated in the conditions. **T remains TBD.** Unknown mandatory evidence fails closed.

## 2. Verify and merge accepted PR #340

1. Independently fetch current GitHub PR #340, `main`, protection/check/review/thread status and exact changed-file patch. The accepted observation was: base `705ec39bb85f3bc85e2827c31b88c6e6a20568ce`, head **`a633cb2966383cf5efa6166fed7dadd22a99f6bb`**, open/mergeable, exactly four *new* Markdown paths:
   - `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md`
   - `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-10-finite-proof-and-qualification-plan.md`
   - `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-10-merge-and-design-evidence.md`
   - `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-10-merge-pr339-engineering-design-decisions-codex.md`
2. Verify the patch still matches accepted scope/content and the PR has no unresolved conversations or required failing checks. If head, content, protections or mergeability materially differ, **STOP for owner review**; do not silently update the PR or accept a different patch.
3. Merge using an ordinary allowed GitHub method with **expected head SHA** guard; no bypass. Record exact merge SHA and verify #340 merged. Fetch and confirm clean local `main == origin/main`, ahead/behind `0/0`, before branching. Do not infer that merging the C2B-10 recommendations makes their mechanisms effective or authorizes tests.

## 3. C2B-11 documentation-only branch and literal file scope

Create one planning branch from the verified #340 merge and open **one new PR**, left **open and unmerged**.

**Only permitted new Markdown files:**

- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-11-owner-decision-registration-evidence.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-11-merge-pr340-register-decisions-codex.md` (preserve this authority exactly, except nonsemantic Markdown normalization if necessary and disclosed)

The owner decision register already exists as an **untracked local file**. Treat it as an immutable source document, not an invitation to clean or overwrite untracked files. Its supplied source is `/mnt/data/release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md` when accessible; if unavailable, locate the owner-provided existing file safely without touching protected WP07 material. Verify and record its SHA-256 before and after copying into the allowed repository path. If source provenance, contents, or safe path cannot be established, **STOP**. Never replace an existing tracked/untracked file silently.

The register's historical header says PR #340 was open/unmerged and that the **decisions alone** did not authorize merging it. Preserve this text as the dated decision record: the owner's *later explicit approval of PR #340* is the separate merge authority. Do **not** rewrite the owner selections or silently edit the register to manufacture retroactive authority. Explain the chronology in the separate C2B-11 evidence document. If the repository's exact Markdown conventions require a header clarification, seek owner review instead of changing the register's meaning.

## 4. Owner decisions to preserve exactly

All eight decisions: **Approve recommendation (conditional)**; owner comments **None**.

| ID | Approved design direction (not a proven mechanism) | Conditions retained |
| --- | --- | --- |
| E1 | Reviewed first-party digest pin, deterministic canonical manifest core, no compression, no initial signing secret; signatures remain an option | Trusted source revision, producer/reviewer, schema and threat assumptions need acceptance |
| E2 | Image-bundled code/pin/synthetic assets non-writable by serving UID; whole-image promotion/rollback | Effective UID/ancestors/mounts, release consistency and Azure behavior unproven |
| E3 | Security-only SQLite **local proof candidate**, protected-journal comparator, single-instance persistence domain to verify | **No production backend** or topology/retention/ownership/paid service selected |
| E4 | Existing Infrastructure.Tests friend assembly plus independent test-only CPython integer/rational oracle | Three datasets, twelve windows, all derived values/nulls/fingerprints need independent exact parity; no production Python port |
| E5 | Complete qualified fixed-end windows without pagination/truncation; illustrative version-bound response direction | Ready schemas, response bytes, full-send/cancellation and resource ceilings unapproved |
| E6 | Operator whole-image release lifecycle; separate minimal fail-closed security recovery | No automatic fallback/reset/repair; retention, atomicity, capacity and recovery budgets TBD |
| E7 | Dedicated public no-spawn/no-provider reader; applicable G-B0–G-B7 gates and negative isolation | No blanket WP07 waiver; effective F1 isolation and zero-added-cost feasibility unproven |
| E8 | Staged finite in-memory friend-test and independent oracle **as future first proof** | No exporter/materialization/integration; literal paths, finite safety ceilings and execution require later authority |

These conditional approvals are **not** acceptance of an actual schema, pin, key, artifact, storage backend, resource budget, test command, golden output, gate pass or deployment topology. Do not invent values for T or claim that design acceptance fulfills proof obligations.

## 5. Evidence document and validation

Create `release-1.14-chart-mvp-c2b-11-owner-decision-registration-evidence.md` documenting:

- The exact PR #340 accepted head/base, final merge SHA, branch-protection/check/review/thread findings and clean `main` 0/0.
- Exact register source path, SHA-256 and byte-preserving copy result; distinction between earlier register header and later explicit PR #340 acceptance.
- E1–E8 conditional status and all outstanding proofs/T ceilings, mapped to C2B-10 options and the C2B-9 canonical policy; no mechanism asserted qualified.
- New PR URL/base/head, exact three-file diff, whitespace/link checks and preservation of historical records and frozen contracts.
- **No tests run** (previous Python 162 passed/11 skipped, .NET 620 passed and C2B-7 16 focused tests are *inherited*, not fresh). No Azure/browser/isolation/parity/durability/resource evidence created.
- Explicit `PUBLIC_RUNTIME_VERIFIED=False`, public dataset/indicator unavailability, implementation/export/promotion/target/activation NO-GO, advanced WP07 ON HOLD.

Run documentation-only checks (`git diff --check`, literal path allowlist, file hash, relative links/headings and no unrelated tracked/untracked mutation). No restore/build/test/generator/provider/Azure/network/load/fault commands. Stop if any precondition fails; report the partial state without widening scope.

## 6. Exit condition and next boundary

Deliver the guarded merge result, verified new documentation PR **open/unmerged**, exact three-file inventory, SHA/hash evidence, inherited-vs-fresh test disclosure, and unresolved qualification checklist. **Do not merge the new C2B-11 PR.** A later separate owner authority must approve any finite executable proof with literal paths, independent oracle provenance, bounded CPU/RSS/wall/disk/log/output/repetition/shutdown ceilings and explicit abort behavior. Snapshot generation/export/promotion, integration, Azure target/browser qualification, deployment and public activation require still further distinct authorities.

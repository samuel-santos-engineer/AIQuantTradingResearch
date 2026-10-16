# Release 1.14 Chart MVP — C2B-9 Codex authority
## Merge accepted PRs #337 and #338; reconcile canonical governance (documentation only)

**Owner authority:** The owner explicitly accepted PR #337 and PR #338 and requested this next Codex authority. Acceptance authorizes **guarded merges of these exact reviewed PRs**, followed by a **separate documentation-only canonical governance reconciliation PR**. It does **not** authorize implementation, snapshot production/export/promotion, active load testing, Azure changes, deployment or public activation.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

**Starting reference observations (reverify, never assume):** PR #337 head `d5ae5624c99f5a4ba0d54c4d48f3d172a5c1ceb6`, three C2B-7 Markdown files; PR #338 head `13d51135fdbd32985973a1ca7797ff1f8e6eee64`, three C2B-8 Markdown files. Both were observed open, unmerged and mergeable on 2026-10-16. Their current heads, bases, diff, status, reviews, conversations, protections and merge order must be checked afresh. If heads or material content changed, **STOP for owner re-review** rather than silently accepting a different patch.

## A. Non-negotiable preservation rules

1. Keep `PUBLIC_RUNTIME_VERIFIED=False` unchanged. Public Preview remains anonymous, free, synthetic-only and **unavailable** for dataset/candles/indicators until separately qualified. Public activation/Azure deployment **NO-GO**; advanced WP07 lifecycle work **ON HOLD**.
2. Protect the original WP07 partial/dirty worktree and its hash archive: do not switch, reset, clean, stash, amend, move, or inspect protected private artifacts. Use an independently verified clean checkout; do not assume a historical path is safe.
3. No changes to source, tests, packages, lockfiles, CI, schemas, databases, snapshots, fixtures, generator/calculator constants or fingerprints, APIs, UI, Docker, credentials, cloud resources, Project metadata or branch protections. No provider calls, private data, or new services.
4. No force-push, bypass, automatic branch deletion, unrelated cleanup or issue closure. Follow branch protections, required checks/reviews and unresolved-thread rules. Preserve historical proposal records; do not rewrite C2B-5/6/7 or owner decision register.
5. If any prerequisite, policy target, source reference or accepted wording is ambiguous, **STOP and document it**. Do not invent a threshold, trust anchor, store, principal, signature, schema or implementation file allowlist.

## B. Guarded merge of the two accepted PRs

1. Fetch and independently verify remote `main`, both PR states, exact head SHAs, diff inventories and content, mergeability, checks, reviews and unresolved threads. Confirm #337 changes exactly the three previously reviewed C2B-7 Markdown files and #338 exactly the three previously reviewed C2B-8 Markdown files. Confirm the C2B-8 owner decision register is preserved verbatim; if available, verify the recorded SHA-256 `AF1CCAE6E30B2482612BF87317253D1E051EE5D784EA6CEB6B8B6735397B1BB8` against the exact bytes, accounting for whether the published record includes a final newline.
2. Merge **#337 first** via ordinary permitted GitHub merge method, guarded by its exact accepted head and all repository safeguards. Re-fetch and record merge SHA, status and main tree.
3. Re-evaluate **#338 against updated `main`**. If its merge base is behind and it is still cleanly mergeable with the exact accepted three-file patch, and checks/protections/reviews pass, merge via permitted method guarded by its exact accepted head. Do not silently rewrite #338 or bypass safeguards. If conflict, altered head, required check failure or uncertainty: STOP and request owner direction; leave #338 open.
4. Verify merged state, commit ancestry and clean `main == origin/main` (0/0) in the safe checkout. Report both merge SHAs separately. If #337 merges but #338 cannot, do not proceed to canonical reconciliation on incomplete owner-record baseline.

## C. C2B-9 canonical reconciliation: permitted mutation scope

**Create one new planning branch from verified merged `main`, and leave its PR open/unmerged.** This is a **documentation-only policy reconciliation proposal**; no canonical amendment becomes effective solely because this planning PR is opened. The owner has approved P1–P7, S, B1 and T as selections, but has not approved any unreviewed new engineering mechanism or deployment.

**Permitted existing canonical files for proposed text-only changes (only after inspecting actual content and exact conflicting clauses):**

- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` — documentation/authority mapping only; no source allowlist expansion

**New evidence/reconciliation Markdown paths allowed only under:** `docs/roadmap/release-1.14/prompters/`, with prefix `release-1.14-chart-mvp-c2b-9-`. Do **not** edit the historical WP04 implementation-authority document or any historical prompter. If a current WP04 exception needs documenting, put a narrowly scoped forward-looking cross-reference in canonical execution-plan/strategy text; do not retroactively modify historical authority.

### Exact owner-selected decisions to reconcile

- **C1 / P1:** Narrow owner-controlled offline C# generation exception **only for a separately authorized immutable synthetic release**. No public/request/startup/background/recovery generation; no export, promotion or producer authority here. Preserve frozen `aiq-synthetic-ohlcv-v1`, seed 114, and fixed UTC coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`.
- **C2 / P2:** Immutable release identities, independent external trust anchor, manifest/member integrity and provenance, non-public-writable code/assets/anchor/ancestors, operator ownership, and fail-closed verification. Do not select pins vs signatures, serialization, key management, mount/UID or promotion path yet.
- **C3 / P3 + B1:** **B1 selected**: synthetic candles plus offline precomputed C# analytics, independently verified for all twelve qualified windows. Preserve 250 calculation-only warmup rows, fixed SMA20/EMA20/RSI14/MACD12-26-9/Bollinger20x2, exact canonical/rounding/null/seed semantics, unchanged Worker/cache contracts and no production Python port. The exception applies only to a future separately accepted immutable context; no production/export now.
- **C4 / P4:** Exactly 1h/4h/1d intervals × 1/7/30/90-day fixed-end selections, six read-only GET endpoints, existing parameter/default/validation/error semantics. Unqualified valid ranges stay `503 DATASET_NOT_READY`, no silent truncation, rebasing, rolling, pagination or live/observed claims. Exact public label **“BTC-like Market Simulation — Synthetic Historical OHLCV”**. Twelve Data remains private; Vike inactive.
- **C5 / S:** Approved conceptual exception for **durable mutable security-control state only** (abuse accounting/admission), separate from immutable public market-data assets and private research. Require bounded atomic concurrency-safe accounting across approved restart/instance domain, unknown state fails closed, no reset through restart/client/release rotation. No backend/topology/new paid resource/retention/identity selected; do not assert SQLite or a filesystem journal is proven.
- **C6 / P5:** No public Worker/generator/provider/private fallback. Prove confidentiality and read admission through validation, load, projection, serialization, complete send/cancellation, health protection and applicable G-B0–G-B7 gates. No blanket WP07 waiver.
- **C7 / P6:** Separate immutable release lifecycle from 24-hour request-cache policy. Operator-only whole-release promotion/revocation/eligible rollback; pinned coherent verified readers; no public repair or automatic old-file fallback. Missing/untrusted/unqualified data -> safe null/503, cheap independent health. Retention, resource and recovery limits remain TBD.
- **C8 / P7 + T:** Explicit separate authorities for policy effectiveness, literal-path finite implementation, offline production/export, local qualification, target Azure F1/browser qualification, deployment and public activation. Unmeasured response bytes, CPU/RSS, in-flight/send limits, security-store durability, disk/temp/retention/recovery/test budgets **TBD — measurement required**. No activity requiring such limits proceeds until separately accepted.

Reconcile the **actual governing language** in place with narrow, traceable addenda or replacement clauses, identifying each original conflict and retaining unaffected rules. Include a clear effectiveness clause: **owner selections are approved, but newly proposed canonical reconciliation wording remains pending explicit owner review/acceptance of the C2B-9 PR; neither this PR nor any merge constitutes runtime authorization**. Avoid contradictory blanket “no mutable shared state” language; distinguish security-control state from market/private data. Do not falsely mark technical proof or Azure gates as passed.

## D. Required C2B-9 evidence packet

Create, under the allowed prompters directory:

1. `release-1.14-chart-mvp-c2b-9-canonical-governance-reconciliation-evidence.md`: PR #337/#338 guarded merge evidence; exact base/heads/merge SHAs; changed-file inventories; owner-selection provenance; per-file before/after clause map C1–C8 and P1–P7/S/B1/T; explicit retained conflicts; checks; fresh versus inherited tests; STOP findings.
2. `release-1.14-chart-mvp-c2b-9-engineering-decision-backlog.md`: unresolved E1–E8: trust anchor/manifest; immutable ownership/promotion; durable security backend/domain; independent complete B1 parity; ready API schema/byte/read lifetime; retention/recovery/privacy; isolation/gate mapping; literal implementation allowlist and finite test ceilings. For each, list alternatives, evidence needed, decision owner, dependency and next authorized stage; **do not select or implement**.
3. Optionally preserve this exact C2B-9 authority as a third Markdown file under the same prefix, if doing so is consistent with the repository's existing prompter convention. If preserving it, verify its wording is unmodified except for nonsemantic Markdown normalization and disclose any difference.

Keep the PR reviewable and small. Do not introduce new numerical thresholds or ready schemas. No unrequested refactor of governing documents.

## E. Validation and final report

- Fresh GitHub merge/protection/review/changed-file verification for each accepted PR; final main clean and 0/0 before creating the new branch.
- Documentation-only diff and literal-path allowlist audit; `git diff --check`; links/relative paths/heading integrity; compare actual canonical clauses to C1–C8 and owner register; confirm all frozen strings, hashes and gate statements are preserved.
- No runtime tests required for docs-only changes; if not rerun, explicitly label previous Python 162 passed/11 skipped, .NET 620 passed, and C2B-7 16 focused tests as **inherited**, not fresh. Do not claim Azure F1, browser, indicator parity, immutable ownership or durable-store qualification.
- Open **one C2B-9 documentation-only PR**, leave **open and unmerged**. Include scope, review checklist, unresolved E1–E8, and unequivocal NO-GO language.
- Final response must give #337 and #338 merge results and SHAs, C2B-9 PR URL/head/base, exact changed paths, checks, any STOPs and explicit non-authorization statement. If unable to complete any merge safely, stop rather than guessing or broadening scope.

**Exit condition:** Accepted documentation PRs merged under normal safeguards, owner decisions mapped into a separately reviewable canonical governance proposal, and implementation/production/public deployment still disabled. **No code or artifact output is authorized.**

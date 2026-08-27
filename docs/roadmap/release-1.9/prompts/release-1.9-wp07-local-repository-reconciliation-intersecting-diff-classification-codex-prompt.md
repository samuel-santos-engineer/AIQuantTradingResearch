# Release 1.9 — WP07 Local-Repository Reconciliation / Intersecting-Diff Classification Authority

## Authority

This is a **narrow local-repository reconciliation and diff-classification authority** supporting Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Luna**.

This authority is **read-only by default**.

Its purpose is to classify pre-existing local worktree changes that intersect paths authorized for the canonical WP07 semantic-exposure implementation, so a later fresh Terra authority can safely preserve/reuse valid work without overwriting unrelated or ambiguous changes.

It does **not** authorize implementation.

It does **not** authorize cleanup.

It does **not** authorize resetting the repository.

---

# Binding Governance

Treat these accepted artifacts as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

Also read the most recent fresh semantic-exposure implementation authority for context, but do not execute its implementation phases.

Semantic meaning comes from artifact 1.

Allowed implementation surfaces come from artifact 2.

---

# Proven Entry Evidence

The immediately preceding implementation attempt stopped before mutation because the worktree already contained changes on authorized exposure paths, including:

- `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionResult.cs`
- `src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs`

Reported Git state:

- branch: `main`;
- local `main`: `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- `origin/main`: `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- ahead/behind: `0/0`.

No mutation was made by that attempt.

Verify all of this independently.

---

# Accepted Baseline

For reconciliation purposes, use commit:

`3a02f035a253e4e16f479e1866c9a5195f5cfbdb`

as the Git baseline **only after verifying**:

- local `main` resolves to it;
- `origin/main` resolves to it;
- ahead/behind is 0/0;
- no later accepted committed predecessor work exists elsewhere that governance requires as the comparison base.

Important:

The repository may intentionally contain **uncommitted accepted predecessor implementation work** accumulated after this commit.

Therefore:

- Git baseline difference does not automatically mean unauthorized work;
- do not assume dirty files should be reverted;
- classify by content and accepted project history/contracts.

---

# Objective

Produce a deterministic classification of every current worktree change that intersects the WP07 semantic-exposure authorized path set.

Every intersecting hunk/file must be classified as exactly one of:

## Class A — Accepted predecessor work

Valid implementation from completed WP02–WP06 or accepted WP04/WP05 follow-up work that is required to preserve the current accepted predecessor state.

Action rule: **preserve exactly**.

## Class B — Valid partial WP07 semantic-exposure work

A local change that already implements part of the two binding WP07 semantic primitives or their authorized exposure chain and conforms exactly to both binding artifacts.

Action rule: **reuse/preserve; later Terra may complete around it and modify it only within the exact binding authority**.

## Class C — Unrelated local work

A local change unrelated to WP07 semantic exposure and not required to implement it.

Action rule: **preserve untouched; later Terra must work around it**.

## Class D — Conflicting or ambiguous work

A hunk whose provenance or semantic compatibility cannot be established safely, including:

- partially overlaps WP07 but contradicts binding semantics;
- appears to implement an older abandoned contract;
- mixes unrelated and WP07 concerns inseparably;
- provenance cannot be determined;
- would require guessing whether to retain/remove.

Action rule: **block implementation and request a specific provenance/repair authority**.

No fifth catch-all class.

---

# Absolute Safety Rules

Do **not** run or perform:

- `git reset`;
- `git checkout -- <path>`;
- `git restore`;
- `git clean`;
- `git stash`;
- `git revert`;
- destructive rebases;
- file deletion;
- overwriting;
- auto-formatting;
- package restore that rewrites lock/config files;
- test execution that knowingly mutates governed repository artifacts.

Do not edit source/test/docs.

Do not stage or commit.

Do not mutate GitHub/Project.

Read-only commands are permitted.

---

# Phase 0 — Repository Snapshot

Capture:

- `git status --short`;
- `git status --branch`;
- current branch;
- `git rev-parse HEAD`;
- local main SHA;
- origin/main SHA;
- ahead/behind;
- staged diff names;
- unstaged diff names;
- untracked paths;
- ignored paths only if relevant.

Record whether any file is staged.

Do not alter index state.

---

# Phase 1 — Authorized WP07 Path Set

From the binding manifest/path amendment, extract the **exact** authorized semantic-exposure production paths and three dedicated focused test paths.

Also extract:

- exact authorized symbols/concerns;
- predecessor ownership;
- forbidden adjacent concerns;
- reserved later WP07 presentation test path.

Create the authoritative intersection set:

`current dirty paths ∩ WP07 semantic-exposure authorized paths`

Do not broaden it by directory.

---

# Phase 2 — Full Dirty-Path Inventory

Inventory **all** dirty paths, not just the known two.

For each path record:

- tracked/untracked;
- staged/unstaged;
- authorized WP07 path? yes/no;
- predecessor-owned path? which WP;
- test/docs/production;
- approximate diff size.

Non-intersecting dirty paths are not to be deeply classified unless needed to understand dependencies, but they must be recorded so the later authority knows they exist.

---

# Phase 3 — Baseline Diff Capture

For every intersecting tracked path, inspect:

- baseline content at `3a02f035...`;
- current worktree content;
- unstaged diff;
- staged diff if any;
- combined effective diff.

Use zero-context or function-context diffs as useful, but inspect enough surrounding source to understand semantics.

For untracked intersecting paths:

- inspect full file content;
- determine whether the path is one of the newly authorized dedicated WP07 test files;
- classify without adding it to Git.

No mutation.

---

# Phase 4 — Accepted-Predecessor Reconstruction

Use repository evidence and accepted project artifacts to reconstruct what the accepted predecessor worktree should contain.

At minimum inspect documentation/contracts for completed:

- WP02;
- WP03;
- WP04;
- Historical presentation feature work;
- Historical producer integration;
- Historical production-composition tests;
- WP05;
- WP05 deterministic-regression test isolation;
- WP06.

Use current completed-test counts/history as corroborating evidence where available:

- full .NET predecessor 305/305 before WP07 exposure;
- WP05 Python 3/3;
- WP06 Python 6/6.

Do not infer exact code solely from test counts.

---

# Phase 5 — Semantic Fingerprint Search

Search current dirty diffs for fingerprints of WP07 semantic-exposure work, including:

- `PresentationIdempotencyStatus`;
- `PresentationDataQualityStatus`;
- `NewlyPersisted`;
- `EquivalentExisting`;
- `Unavailable`;
- `Valid`;
- `Invalid`;
- new `PipelineExecutionResult` properties;
- new `PipelineExecutionEvidence` properties;
- WP04 envelope fields;
- JSON serialization fields;
- Python parser fields;
- `VisualizationFrame` metadata.

Also search for abandoned/incorrect variants or synonyms.

This determines whether partial WP07 work already exists.

---

# Phase 6 — Hunk-Level Classification

For every intersecting file, classify at **hunk level** when a file contains more than one concern.

For each hunk provide:

- path;
- hunk/function/member;
- concise description;
- evidence;
- Class A/B/C/D;
- preservation rule;
- whether later Terra may edit that same hunk.

### Class A rule

Must be traceable to an accepted predecessor contract/implementation requirement.

### Class B rule

Must satisfy **both** binding WP07 artifacts exactly.

Mere similarity is insufficient.

### Class C rule

Must be demonstrably unrelated to WP07 and safe to preserve.

### Class D rule

Use whenever classification requires guessing.

---

# Phase 7 — Special Review: `PipelineExecutionResult.cs`

Inspect this file in depth.

Determine:

- which current changes predate WP07 semantics;
- whether Historical presentation-input additions belong to accepted WP04 predecessor work;
- whether any canonical semantic status types/properties already exist;
- whether signatures/constructors changed;
- whether changes mix Class A and Class B.

Produce member-level classification.

Do not edit.

---

# Phase 8 — Special Review: `PipelineExecutionUseCase.cs`

Inspect this file in depth.

Determine:

- accepted Historical presentation feature/output wiring;
- accepted Historical producer integration;
- accepted persistence/provenance logic;
- whether any current hunks already map pipeline persistence to WP07 idempotency;
- whether any current hunks already map canonical validation to WP07 data quality;
- whether changes implement abandoned/unauthorized semantics.

Produce function/member-level classification.

Do not edit.

---

# Phase 9 — Other Intersecting Paths

Repeat equivalent analysis for every other dirty path that is authorized by the semantic-exposure amendment.

Pay special attention to predecessor-owned shared files:

- WP04 contracts/producer;
- Worker JSON publisher;
- WP05 Python parser;
- WP06 frame path.

Do not assume they are clean merely because the previous blocker named only two files.

---

# Phase 10 — Dedicated Test Paths

Inspect whether any of the three authorized dedicated semantic-exposure test paths already exist locally.

For each:

- if absent: record `Absent — no reconciliation needed`;
- if untracked/modified: inspect fully and classify;
- if it contains actual WP07 presentation rendering tests rather than semantic exposure, classify as D unless another accepted authority explains it.

Also verify the reserved later WP07 presentation test path has not been consumed.

---

# Phase 11 — Cross-File Consistency

Check whether Class B partial WP07 work is internally coherent.

Examples:

- enum defined but not exposed;
- Application property added but no WP04 field;
- serializer field added without parser;
- parser added without frame;
- status values differ from binding definitions.

Partiality alone does not make it invalid.

A partial chain can remain Class B if each implemented piece is correct and authorized.

Contradiction makes the affected work Class D.

---

# Phase 12 — Optional Read-Only Build/Test Evidence

Do not run tests merely to classify provenance.

You may run non-mutating build/test commands only if:

- repository conventions make them safe;
- they materially distinguish Class A/B/D;
- no package restore or generated-file mutation occurs.

If run, use `--no-restore` where appropriate.

Test success does not override semantic classification.

If uncertain about mutation risk, skip tests.

---

# Phase 13 — Reconciliation Decision

Produce exactly one of these outcomes.

## Outcome 1 — Safe reuse

All intersecting changes are A, B, or C, with no D.

Define a binding preservation rule for the next Terra authority:

- Class A: preserve exactly;
- Class B: treat as already-applied authorized WP07 work; may extend/modify only within binding semantic/path authority;
- Class C: preserve untouched;
- absent authorized paths: may be created/changed only as allowed by the manifest amendment.

State that the later authority must **not require a clean worktree**; it must instead use this classification as the reconciliation rule.

## Outcome 2 — Blocked

One or more Class D hunks exist.

Identify:

- exact path/member;
- exact ambiguity/conflict;
- why it cannot be safely resolved;
- minimum next authority or user decision required.

Do not repair it here.

---

# Phase 14 — Reconciliation Artifact

If documentation governance permits one reconciliation artifact, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_LOCAL_REPOSITORY_RECONCILIATION.md`

However, because this authority is read-only by default, create it **only if an existing accepted governance rule explicitly authorizes such a documentation mutation**.

Otherwise make zero repository mutations and return the reconciliation in chat.

Do not assume permission merely because other definition artifacts existed.

---

# Phase 15 — No Lifecycle Mutation

Regardless of outcome:

- #232 remains Open / Backlog;
- #233 remains Open / Backlog;
- do not start WP08;
- do not mutate milestone state;
- do not add GitHub comments unless separately authorized.

---

# Required Completion Report

## Repository snapshot

Branch, SHAs, ahead/behind, staged/unstaged/untracked summary.

## Authorized intersection set

Exact dirty paths intersecting the semantic-exposure amendment.

## Classification table

For every intersecting hunk/member:

- path;
- member/hunk;
- Class A/B/C/D;
- evidence;
- preservation rule.

## Non-intersecting dirty state

List paths only, with high-level note that they remain untouched.

## Partial WP07 fingerprint

State whether valid partial semantic-exposure work already exists and where.

## Dedicated test-path state

Report all three authorized semantic-exposure test paths and the reserved presentation test path.

## Reconciliation rule

Either Safe reuse rule or exact blocker.

## Mutation proof

If zero repository mutation:

`WP07 LOCAL-REPOSITORY RECONCILIATION MUTATIONS: ZERO`

If one explicitly governed reconciliation document was created:

`WP07 LOCAL-REPOSITORY RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; one explicitly authorized reconciliation documentation artifact created`

## Lifecycle

Confirm #232/#233 unchanged.

## Next step

On safe reuse state exactly:

`WP07 LOCAL REPOSITORY RECONCILED — FRESH SEMANTIC-EXPOSURE IMPLEMENTATION MAY RESUME USING THE CLASSIFICATION RULE`

---

# Terminal Markers

Safe classification:

`RELEASE 1.9 WP07 LOCAL-REPOSITORY RECONCILIATION AND INTERSECTING-DIFF CLASSIFICATION COMPLETE`

Ambiguous/conflicting classification:

`RELEASE 1.9 WP07 LOCAL-REPOSITORY RECONCILIATION AND INTERSECTING-DIFF CLASSIFICATION BLOCKED`

Do not emit COMPLETE if any intersecting hunk remains Class D.

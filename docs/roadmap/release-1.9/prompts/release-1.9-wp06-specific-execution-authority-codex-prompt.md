# Release 1.9 — WP06 Specific Execution Authority — Codex Prompt

## Authority

Execute Release 1.9 **WP06**, canonical GitHub issue **#231**, and no later work package.

This is a **WP06-specific discovery → implementation → validation → completion authority**. It is deliberately bounded by the accepted Release 1.9 roadmap, #231, predecessor contracts, and the repository's manifest/path ownership.

Do not infer WP06 semantics from the issue title alone. Before mutation, read the canonical WP06 definition and manifest and derive the exact accepted objective, deliverables, file ownership, acceptance criteria, dependencies, and non-goals.

If #231 or the accepted Release 1.9 artifacts leave a material contract choice unresolved, stop before mutation and report the minimum narrow definition authority required.

## Recommended model

Use **GPT-5.6 Terra** for the implementation pass.

If the pre-mutation investigation reveals that WP06 is blocked by an unresolved design/governance choice rather than implementation work, stop and recommend a narrow **GPT-5.6 Luna** definition authority instead of inventing semantics.

---

# Canonical Entry State

Expected predecessor lifecycle:

- WP01–WP05: complete
- WP05 #230: **Closed / Done**
- WP06 #231: **Open / Backlog**
- WP07 #232 and later: Open / untouched
- milestone #58: Open
- canonical milestone count after WP05: **7 open / 5 closed**
- raw GitHub count may be one closed item higher because historical duplicate #225 remains separately present
- SQLite schema: **v4**

Accepted immediate predecessor regression baseline:

- full .NET regression: **305/305 passed**
- build: **0 errors / 0 warnings**
- WP05 Worker → atomic JSON → Python consumer composition accepted for Historical and Replay
- Streamlit consumer is bounded and read-only
- WP04 presentation read model remains the canonical presentation contract

Do not assume any more-specific suite count unless freshly verified.

---

# Fixed Predecessor Boundaries

WP06 must preserve all accepted predecessor contracts.

## WP02 Replay

Preserve:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite completion.

## WP03 Pipeline / Schema

Preserve:

- Historical and Replay Worker modes;
- canonical five-stage pipeline;
- Dataset boundary semantics;
- schema v4;
- source authority:
  - 0 = Historical
  - 1 = Replay;
- Replay persistence provenance.

## WP04 Presentation Read Model

Preserve:

- immutable versioned presentation envelope;
- 64-row bounded observation window;
- Ready / Empty / WarmUp / Stale / Failed;
- HistoricalPresentationRevision;
- ReplayLogicalTick revision;
- atomic publication;
- canonical Historical feature projection;
- no SQLite/provider reconstruction.

## WP05 Cross-Process / Streamlit Boundary

Preserve:

- local atomic JSON handoff;
- `aiq-visualization-read-model-v1`;
- Worker-owned writes and startup cleanup;
- read-only Python/Streamlit consumer;
- `Visualization:HandoffPath`;
- bounded refresh;
- default 2 seconds;
- refresh bounds 1–60 seconds;
- max two reads per cycle;
- one 50 ms retry;
- one last-good envelope;
- transport warnings distinct from backend state;
- no SQLite access;
- no provider calls;
- no feature recomputation;
- no Worker process control from Streamlit.

WP06 may extend only what its accepted definition explicitly owns.

---

# Phase 0 — Read-Only Authority Resolution

Before changing any file:

1. Read #231 completely.
2. Read the accepted Release 1.9 definition and WP06 work-package section.
3. Read the Release 1.9 implementation manifest/path ownership for WP06.
4. Read any WP06-specific planning artifact referenced by #231.
5. Read the completed WP05 manifest/path amendment to avoid taking WP05 ownership.
6. Read WP07 and later manifest ownership sufficiently to avoid stealing future paths.
7. Inspect current repository structure around every WP06-authorized path.
8. Inspect current tests relevant to the WP06 objective.
9. Record exact accepted:
   - objective;
   - deliverables;
   - authorized paths;
   - acceptance criteria;
   - dependencies;
   - non-goals;
   - GitHub lifecycle requirements.

Do not mutate in this phase.

---

# Phase 1 — Repository / GitHub Baseline Proof

Record:

- current branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged paths;
- tracked modifications;
- relevant untracked Release 1.9 authority/control files.

Verify:

- #230 Closed / Done;
- #231 Open / Backlog;
- #232–#237 Open / untouched;
- milestone #58 remains open;
- WP05 accepted implementation is present;
- no partial WP06 implementation exists unless clearly attributable to an earlier authorized attempt.

Run or confirm the immediate predecessor baseline before WP06 mutation:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Expected accepted predecessor total:

**305/305**

Also run the repository-standard build.

If the predecessor baseline is not clean, stop for reconciliation unless the discrepancy is fully explained and non-regressive.

---

# Phase 2 — Produce a WP06 Execution Map

Before coding, write a concise execution map containing:

- each required WP06 deliverable;
- exact file path;
- manifest ownership;
- exclusive/shared status;
- acceptance test(s) proving it;
- predecessor boundary it must not violate.

Every intended changed/created file must be authorized.

If any required file is not manifest-authorized:

**STOP BEFORE MUTATION.**

Report the missing path and request a narrow WP06 manifest/path amendment.

Do not substitute a nearby file.

---

# Phase 3 — Contract Sufficiency Gate

For every material behavior required by #231, determine whether the accepted artifacts define it unambiguously.

Examples of material choices include, where relevant:

- user-visible state semantics;
- controls and allowed actions;
- configuration keys/defaults;
- ownership of calculations;
- input/output representation;
- error behavior;
- persistence behavior;
- lifecycle;
- refresh behavior;
- ordering;
- limits/bounds;
- cross-process semantics.

Do not invent any of these.

If multiple materially different implementations satisfy the text, and choosing among them would establish a new contract:

**STOP.**

Return:

- exact unresolved choice;
- repository evidence;
- viable candidate models;
- minimum narrow WP06 contract-definition authority required.

No mutation.

---

# Phase 4 — Implement Only WP06

If and only if the contract and manifest are sufficient, implement the minimum additive WP06 surface.

Rules:

- use existing dependencies;
- preserve exact package pins;
- preserve Python/Streamlit versions;
- preserve schema v4 unless #231 explicitly and unambiguously authorizes a schema change;
- preserve JSON-over-stdio boundary;
- preserve WP05 handoff contract unless #231 explicitly owns a compatible extension;
- prefer existing abstractions over generalized frameworks;
- no speculative hooks for WP07+;
- no unrelated refactor.

The implementation must remain attributable to #231 alone.

---

# Phase 5 — UI / Presentation Boundary Gate

If WP06 touches Python/Streamlit presentation, it must remain a consumer of the accepted WP04/WP05 presentation boundary unless #231 explicitly authorizes an extension.

Hard prohibitions unless explicitly owned by WP06:

- direct SQLite access;
- provider calls;
- feature recomputation;
- market-data reconstruction;
- pipeline execution from UI;
- Worker start/stop control;
- unbounded history;
- new IPC transport;
- reinterpretation of backend failure/state.

If #231 requires information not present in the accepted envelope, do not reconstruct it in Python. Stop and identify the missing predecessor contract.

---

# Phase 6 — Focused Tests

Add only manifest-authorized WP06 tests.

Tests must prove every accepted #231 criterion directly.

Prefer:

- deterministic unit tests;
- focused integration tests through real production composition;
- bounded fixtures;
- no network;
- no timing-dependent sleeps where deterministic synchronization/mocking is available.

Do not weaken predecessor tests.

Do not mark tests skipped/ignored to obtain green results.

---

# Phase 7 — Production-Composition Evidence

Where #231 affects a production path, prove the real path rather than only isolated helpers.

The evidence must traverse the actual accepted composition boundary relevant to WP06.

For Streamlit-facing behavior, use the real WP05 consumer/read-model boundary where practical.

For Worker-facing behavior, use real Worker composition where practical.

A manually constructed final result is not sufficient as the sole production evidence if the acceptance criterion concerns composition.

---

# Phase 8 — Static Boundary Audit

Search/diff for forbidden leakage introduced by WP06.

Depending on affected layers, prove absence of:

- SQLite access from presentation;
- provider calls from presentation;
- duplicated feature formulas;
- new persistence writes;
- new transport mechanisms;
- unbounded history;
- WP07+ implementation;
- schema changes outside explicit authority;
- package/pin changes;
- JSON-over-stdio changes;
- unrelated architecture refactors.

This is a hard gate.

---

# Phase 9 — Focused Validation

Run all WP06-focused tests.

Capture exact:

- commands;
- passed;
- failed;
- skipped;
- total.

All must pass.

If Python is touched, run repository-governed Python tests plus syntax/import/compile checks.

If Streamlit is touched, perform the repository-governed smoke/import validation without inventing uncontrolled process orchestration.

---

# Phase 10 — Predecessor Regression

Revalidate predecessor-sensitive behavior impacted by WP06.

At minimum preserve:

## WP05
- Historical handoff;
- Replay handoff;
- atomic JSON;
- parser;
- revision/cache/retry;
- bounded read-only Streamlit consumer.

## WP04
- all presentation states;
- bounded window;
- revision semantics;
- atomic publication;
- Historical production composition;
- Replay production composition.

## WP03
- Worker modes;
- canonical pipeline;
- schema v4;
- source authority 0/1;
- Replay persistence.

## WP02
- Replay deterministic lifecycle.

Run the existing focused suites appropriate to the actual changed layers.

---

# Phase 11 — Governed Suites

Run definitively:

- Infrastructure;
- Application;
- Domain;
- Architecture.

Record exact fresh counts.

Do not assume the prior per-project counts.

Any regression blocks completion.

---

# Phase 12 — Build

Run the repository-standard build.

Require:

- exit code 0;
- **0 errors**;
- report warnings exactly.

Accepted predecessor build:

**0 errors / 0 warnings**

Any new warning requires explanation and blocks completion unless already governed/accepted.

---

# Phase 13 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**305/305 passed**

Require:

- definitive completion;
- exit code 0;
- 0 failed;
- exact passed/failed/skipped/total;
- any count increase explained by authorized WP06 tests;
- no unexplained disappearance of predecessor tests.

The successful final count becomes WP07's predecessor baseline.

If Python is part of WP06, its governed validation is an additional acceptance gate and is not represented by the .NET count.

---

# Phase 14 — Manifest / Scope Audit

Before GitHub mutation, classify every changed/created file.

For each:

- exact path;
- WP06 manifest entry;
- exclusive/shared;
- authorized concern;
- reason changed.

Prove zero unauthorized changes.

Also prove:

- no WP07+ work;
- predecessor authority/control files preserved;
- no package/pin mutation unless explicitly authorized;
- no schema/persistence/protocol changes unless explicitly authorized by #231;
- no unrelated refactor.

Any unexplained path blocks completion.

---

# Phase 15 — Acceptance Matrix

Report PASS/FAIL for every acceptance criterion from #231 plus:

- predecessor baseline reconciled;
- manifest compliance;
- contract sufficiency;
- focused tests;
- real production composition where required;
- static boundary audit;
- WP02 regression;
- WP03 regression;
- WP04 regression;
- WP05 regression;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- Python validation if applicable;
- build;
- full regression;
- final scope audit.

Any FAIL means WP06 remains open.

---

# Phase 16 — GitHub Lifecycle

Only after every technical and scope gate passes:

1. add concise evidence to #231 if repository convention requires it;
2. set Project #2 Status = `Done`;
3. preserve the existing governed:
   - Priority;
   - Release 1.9;
   - Area;
4. close #231.

Do not alter:

- #230;
- #232–#237;
- milestone membership;
- release taxonomy.

After successful closure verify:

- #231 Closed / Done;
- #232 remains Open / Backlog;
- milestone #58 remains open;
- canonical milestone count becomes **6 open / 6 closed**;
- raw GitHub count remains one closed item higher if historical duplicate #225 is still included.

Do not start WP07.

---

# Stop Conditions

Stop before or during implementation if:

- #231 is materially ambiguous;
- a required path is not authorized;
- a predecessor contract must be redesigned;
- required data is absent from the accepted presentation boundary and would need UI reconstruction;
- a new dependency is required without explicit authority;
- schema/persistence/protocol changes are required without explicit authority;
- WP07+ ownership would be crossed;
- tests need weakening;
- production composition cannot be proven;
- any predecessor suite regresses;
- Python validation fails;
- build fails;
- full regression fails;
- scope audit fails.

On blocker:

- preserve valid predecessor and authorized partial WP06 state;
- do not close #231;
- do not start WP07;
- report the exact blocker;
- identify the minimum narrow follow-up authority.

---

# Required Completion Report

## Authority resolution
- #231 objective;
- accepted deliverables;
- manifest paths;
- material contract proof.

## Entry state
- Git state;
- lifecycle;
- predecessor baseline.

## Execution map
- every intended/actual changed path and ownership.

## Implementation
- concise description by deliverable.

## Focused evidence
- exact commands/results.

## Production composition
- real path exercised;
- PASS/FAIL.

## Boundary audit
- forbidden leakage checks.

## Regression
- WP02;
- WP03;
- WP04;
- WP05;
- Infrastructure;
- Application;
- Domain;
- Architecture;
- Python if applicable;
- build;
- full regression.

## Scope audit
- every changed path mapped to WP06 authority.

## GitHub lifecycle
- #231 before/after;
- Project Status;
- milestone counts;
- #232 untouched.

## Next eligible work package

On success state exactly:

`NEXT ELIGIBLE WORK PACKAGE: WP07 — #232`

Do not execute WP07.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP06 COMPLETE`

On blocker:

`RELEASE 1.9 WP06 BLOCKED`

Do not emit success unless every #231 acceptance criterion, validation gate, scope gate, and GitHub completion gate passes.

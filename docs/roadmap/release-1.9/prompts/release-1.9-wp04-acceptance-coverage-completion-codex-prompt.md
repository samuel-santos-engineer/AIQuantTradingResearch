# Release 1.9 — WP04 Acceptance-Coverage Completion — Codex Authority

## Authority

This document grants a narrow acceptance-coverage completion authority for **Release 1.9 WP04 — canonical GitHub issue #229**.

Current proven state:
- partial WP04 implementation exists;
- build passes with 0 errors / 0 warnings;
- full regression passes **293/293**;
- #229 remains Open / Backlog;
- no GitHub lifecycle mutation occurred;
- WP05 has not started.

Known remaining acceptance gaps include:
- deterministic concurrency proof;
- atomic old-or-new reader visibility;
- complete Ready / Empty / WarmUp / Stale / Failed transition coverage;
- Historical revision overflow evidence;
- Historical revision restart/reset evidence.

Preserve the current WP04 production implementation by default. This is not a redesign or contract-definition authority.

## Objective

Complete only the missing WP04 acceptance evidence required by #229 and the fixed WP04 contracts.

The pass must:
1. inventory current WP04 implementation/tests;
2. map every remaining acceptance gap;
3. add minimum deterministic tests/evidence;
4. change production code only if a focused test proves a concrete WP04 defect;
5. rerun focused, predecessor, build, and full-regression gates;
6. perform final diff/scope audit;
7. close #229 only if every acceptance criterion is freshly proven.

## Fixed Contracts

Do not redesign:

- Model C immutable versioned snapshot + bounded 64-row accumulated window.
- Historical `RevisionKind = HistoricalPresentation`.
- `HistoricalPresentationRevision : ulong`.
- First Historical publication revision = 1.
- Ready / Empty / WarmUp / Failed increment.
- Stale does not increment.
- Restart resets to 1.
- Overflow must fail deterministically, never wrap.
- Replay keeps `RevisionKind = ReplayLogicalTick` and existing WP02 logical-tick semantics.
- No cross-mode numeric ordering.
- Single writer / concurrent readers.
- Readers observe old complete or new complete envelope only.
- States mutually exclusive: Ready, Empty, WarmUp, Stale, Failed.
- No WP05 / Streamlit implementation.
- No schema, persistence, or predecessor-contract redesign unless a focused test proves a concrete WP04 defect that can be fixed within existing WP04 scope.

## Permitted Scope

May:
- inspect current WP04 production/test changes;
- add/refine deterministic WP04 tests;
- add test-only helpers/fixtures/synchronization;
- add targeted assertions/diagnostics;
- make minimal WP04 production fixes only when a failing focused test proves a defect;
- rerun predecessor-sensitive suites;
- run build/full regression;
- finalize #229 after all gates pass.

## Forbidden

Do not:
- redesign Model C;
- change capacity 64;
- redefine Historical or Replay revision semantics;
- add wall-clock stale rules;
- add cross-mode ordering;
- add generalized concurrency or multi-writer support;
- modify schema/persistence model;
- modify Worker configuration semantics;
- modify pipeline algorithms;
- implement WP05/Streamlit;
- alter planning/dependencies;
- close #229 before technical acceptance.

## Phase 0 — Fresh State Proof

Before mutation:
1. Read #229.
2. Read current WP04 production changes.
3. Read current WP04 tests.
4. Record branch, HEAD, origin/main, ahead/behind, staged/tracked/relevant untracked state.
5. Prove #229 remains Open / Backlog.
6. Prove #230–#237 remain open and untouched.
7. Record baseline:
   - build 0 errors / 0 warnings;
   - full suite 293/293.

Do not alter production code in this phase.

## Phase 1 — Acceptance Gap Matrix

Create a complete matrix of #229 criteria and classify each:
- fully proven;
- partially proven;
- unproven.

Explicitly assess:
- bounded capacity/eviction/order/duplicate replacement/older-row ignore;
- Historical initial value and Ready/Empty/WarmUp/Failed increments;
- Stale non-increment;
- overflow;
- restart/reset;
- equal+same identity idempotence;
- equal+different identity conflict;
- lower revision rejection;
- Replay preservation;
- no cross-mode ordering;
- Ready/Empty/WarmUp/Stale/Failed;
- transitions/recovery;
- atomic old-or-new visibility;
- no partial/mixed envelope;
- concurrent readers;
- writer publication consistency;
- consumer immutability;
- no SQLite/provider/Streamlit leakage;
- no feature recomputation.

## Phase 2 — Deterministic Concurrency Design

Use deterministic coordination primitives such as:
- Barrier;
- ManualResetEventSlim;
- CountdownEvent;
- TaskCompletionSource;
- explicit writer/reader phases.

Do not rely primarily on sleeps.

Required proof:
> Readers observe either the complete prior envelope or the complete replacement envelope, never a mixed state.

## Phase 3 — Atomic Old-or-New Reader Proof

Add tests proving:
- snapshot A published;
- snapshot B fully built before publication;
- concurrent readers observe A in full or B in full;
- no reader sees mixed revision/payload/window/state/failure/stale metadata.

Use immutable snapshot/invariant assertions.

If mixed state is possible, a minimal production fix is authorized only to restore the fixed atomic contract.

## Phase 4 — Complete State-Transition Coverage

Add focused tests for:

### Ready
- valid payload;
- feature available;
- Historical revision increments when newly published.

### Empty
- no observations;
- no failure;
- explicit Empty;
- Historical revision increments when newly published.

### WarmUp
- current count < 2;
- required count = 2;
- feature unavailable;
- observations retained as contract allows;
- Historical revision increments;
- transition to Ready once feature valid.

### Stale
- last complete payload retained;
- no wall-clock dependency;
- Historical revision does not increment merely because nothing newer exists;
- newer accepted publication clears/replaces Stale.

### Failed
- safe category/message/failed revision/recoverability;
- no stack trace/raw exception;
- last-good payload retained when contract allows;
- Historical Failed publication increments;
- next success recovers to Ready.

Prove mutual exclusivity explicitly.

## Phase 5 — Historical Overflow Proof

Add a deterministic test that positions the Historical revision counter at/immediately before `ulong.MaxValue` through an existing authorized test seam/helper.

Required:
- final valid increment to `ulong.MaxValue` succeeds if allowed;
- next increment fails deterministically;
- no wrap to 0;
- no wrapped envelope published;
- prior valid published envelope remains intact.

Do not broaden production architecture solely for testability. If a new production seam is required beyond current WP04 surface, stop.

## Phase 6 — Historical Restart/Reset Proof

Prove a new WP04 producer/read-model session:
- starts Historical revision at 1;
- does not continue prior in-memory sequence;
- makes no cross-session ordering claim;
- requires no schema/persistence change;
- keeps Replay semantics separate.

If object lifetime defines the session, construct two independent producer instances.

## Phase 7 — Conflict / Idempotence Completion

Ensure complete coverage:

Historical:
- higher replaces;
- lower rejected/stale;
- equal + same identity idempotent;
- equal + different identity conflict.

Replay:
- existing logical-tick tests remain passing.

Unified:
- revision kinds distinct;
- no Historical-vs-Replay numeric comparison;
- mode/context replacement behavior stays fixed.

## Phase 8 — Consumer Immutability / Boundary

Prove:
- published envelope immutable;
- consumer cannot mutate bounded window or metadata;
- Application presentation contract has no SQLite repository, provider client, or Streamlit dependency;
- feature values are passed through, not recomputed.

## Phase 9 — Minimal Production Fixes Only If Proven

If a focused test fails:
1. identify exact defect;
2. prove defect lies inside current WP04 implementation;
3. make smallest fix;
4. rerun focused test;
5. rerun neighboring WP04 tests.

If a new contract decision is required, stop.

## Phase 10 — Focused WP04 Acceptance Suite

Run complete focused WP04 suite and report exact counts.

Concurrency, overflow, restart, and state transitions must be directly exercised; do not claim indirect coverage unless the test truly proves the semantic.

## Phase 11 — Predecessor Regression Guards

Preserve WP02:
- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite completion.

Preserve WP03:
- Historical/Replay dispatch;
- Dataset boundary;
- schema v4;
- authority 0/1;
- Replay persistence;
- canonical ExecuteCanonical;
- no Replay historical-store misuse.

Do not weaken predecessor tests.

## Phase 12 — Build and Full Regression

Run established build:
- require 0 errors;
- report warnings exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate baseline: **293/293 passed**.

A higher count is expected after new acceptance tests. An unexplained lower count is a blocker.

Capture exact exit status, passed, failed, skipped, and material warnings.

## Phase 13 — Final Diff / Scope Audit

Classify every changed file as:
- pre-existing WP04 implementation;
- acceptance test;
- test-only helper/fixture;
- minimal defect fix proven by focused test;
- explicitly required WP04 documentation artifact.

Prove:
- no WP05 implementation;
- no Streamlit;
- no schema/persistence redesign;
- no pipeline redesign;
- no predecessor contract redesign;
- no new dependency;
- no synthetic Historical source tick;
- no cross-mode ordering;
- no multi-writer/distributed coordination;
- no unrelated refactor;
- authority/control files preserved.

Anything unexplained blocks closure.

## Phase 14 — Technical Acceptance Gate

Before GitHub mutation, enumerate every #229 acceptance criterion with:
- implementation evidence;
- test evidence;
- PASS/FAIL.

Additionally require explicit PASS for:
- deterministic concurrency;
- atomic old-or-new reader proof;
- Ready;
- Empty;
- WarmUp;
- Stale;
- Failed;
- mutual exclusivity;
- WarmUp→Ready;
- Failed→Ready;
- Historical overflow;
- no wrap;
- Historical restart/reset;
- Stale non-increment;
- idempotence/conflict;
- consumer immutability;
- no WP05 leakage;
- predecessor tests;
- build;
- full regression;
- scope audit.

If any item fails, leave #229 Open / Backlog.

## Phase 15 — GitHub Lifecycle Finalization

Only after all technical gates pass:
1. read #229 current state;
2. confirm established completion convention;
3. add one concise evidence comment if required;
4. transition Project Status from Backlog to authoritative completed state;
5. preserve Priority=P1, Release=1.9, authoritative Area;
6. close #229;
7. keep milestone #58 open;
8. read back every mutation;
9. do not modify #230.

Expected success:
- #226–#229 closed/completed;
- #230–#237 open/untouched;
- milestone #58 open;
- canonical milestone counts 8 open / 4 closed;
- raw closed count may additionally include #225;
- dependency chain intact;
- successful WP04 regression count becomes WP05 predecessor baseline;
- WP05 #230 becomes next eligible but remains unstarted.

## Stop Conditions

Stop if:
- missing evidence requires a new contract decision;
- deterministic concurrency proof requires redesign;
- overflow/restart proof requires broad production changes;
- predecessor contract/schema/persistence changes become necessary;
- WP05/Streamlit work becomes necessary;
- predecessor suites regress;
- build/full regression fails;
- diff scope is unexplained;
- GitHub mutation cannot be proven.

Preserve valid current WP04 implementation and do not broaden authority.

## Required Completion Report

Report:
- each previously missing gate and its new proof;
- concurrency synchronization design and old-or-new result;
- PASS/FAIL for Ready, Empty, WarmUp, Stale, Failed, WarmUp→Ready, Failed→Ready, mutual exclusivity;
- Historical overflow/no-wrap/restart/Stale non-increment/idempotence/conflict evidence;
- focused WP04 suite count;
- predecessor suite results;
- build errors/warnings;
- full regression exact counts;
- final diff classification;
- any production defect fix and precise justification;
- #229 before/after lifecycle state;
- milestone canonical counts;
- confirmation #230–#237 untouched.

On success state:

`NEXT ELIGIBLE WORK PACKAGE: WP05 — #230`

Do not authorize or execute WP05.

## Terminal Markers

Success:

`RELEASE 1.9 WP04 ACCEPTANCE-COVERAGE COMPLETION COMPLETE`

Blocker:

`RELEASE 1.9 WP04 ACCEPTANCE-COVERAGE COMPLETION BLOCKED`

Emit success only when every remaining acceptance and lifecycle requirement is freshly proven.

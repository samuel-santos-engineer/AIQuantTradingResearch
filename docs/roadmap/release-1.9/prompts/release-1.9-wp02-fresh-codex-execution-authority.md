# Release 1.9 — WP02 Fresh Codex Execution Authority

## Authority

This document grants fresh execution authority for:

**Release 1.9 WP02 — canonical GitHub issue #227**

This authority supersedes the prior blocked WP02 execution attempt and the prior narrow contract-boundary amendment only for the purpose of executing WP02 from the now-restored clean pre-WP02 repository baseline.

Accepted current state:

- WP01 #226 is technically complete and GitHub-finalized;
- #226 is Closed / Done;
- milestone #58 remains open;
- canonical milestone state before WP02 execution is 11 open / 1 closed;
- historical duplicate #225 remains closed and excluded from the canonical set;
- #227 is open and has not been started successfully;
- #228–#237 remain open and untouched;
- canonical dependency chain remains 11/11;
- local repository reconciliation completed with:
  `OUTCOME A — CLEAN PRE-WP02 BASELINE RESTORED`;
- proven WP02-owned partial artifacts removed:
  - `ReplayRequest.cs`
  - `SimulatedLiveObservationSource.cs`
  - `SimulatedLiveReplayConfiguration.cs`;
- tracked changes = 0;
- staged changes = 0;
- no corruption or conflict markers remain;
- no active patch process remains;
- Release 1.9 authority/control files were preserved;
- no GitHub mutation occurred during reconciliation;
- WP03 has not started.

Terminal reconciliation state:

`RELEASE 1.9 WP02 LOCAL REPOSITORY RECONCILIATION COMPLETE — CLEAN BASELINE`

This authority is for **WP02 only**.

It does not authorize WP03 or later work.

---

# Objective

Execute WP02 #227 completely from the clean baseline.

Before implementation, read #227 and the accepted Release 1.9 definition and derive its exact acceptance criteria.

WP02 is known to require semantics behind `IObservationSource` for:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate behavior;
- cancellation;
- finite replay/end-of-stream behavior.

The prior blocker established that the existing Application contract cannot represent those semantics safely.

Therefore this fresh authority explicitly permits the **minimum Application-layer contract evolution necessary to express those WP02 semantics**, followed by the already-planned Infrastructure replay/configuration/DI implementation, focused tests, full regression, scope audit, and normal WP02 GitHub lifecycle finalization.

Do not broaden beyond what #227 requires.

---

# Predecessor Gate

Before any mutation, freshly prove:

- #226 is closed;
- #226 Project Status is the authoritative completed state;
- #227 is open;
- #227 is the canonical WP02;
- #227 has exactly one Project #2 item;
- #227 fields remain:
  - Status = Backlog
  - Priority = P1
  - Release = 1.9
  - authoritative Area;
- milestone #58 remains open;
- dependency edge WP01 → WP02 remains present under the accepted direction semantics;
- repository has:
  - zero tracked changes;
  - zero staged changes;
  - no residual WP02 partial artifacts;
- WP03 has not started.

If any predecessor invariant is materially different, stop.

---

# Baseline Foundations

Accepted Release 1.9 predecessor foundations include:

- branch `main`;
- predecessor baseline commit:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- CPython 3.13.15 x64 in isolated `.venv`;
- exact pins:
  - NumPy 2.5.1
  - pandas 3.0.5
  - scikit-learn 1.9.0
  - Streamlit 1.61.1;
- SQLite schema v3;
- governed one-shot JSON-over-stdio boundary;
- authoritative regression baseline:
  `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`
  passed 281/281 at WP01 completion.

Treat these as predecessor facts.

Freshly verify only what WP02 requires for safe execution and final regression.

Do not alter any foundation unless #227 explicitly requires it.

---

# Known Contract Boundary

Current Application contract is known to be insufficient.

Observed shape:

`IObservationSource`
- `ObservationSourceResult GetObservations(ResearchRequest request)`

`ResearchRequest`
- target
- requested observation count

This shape does not explicitly represent the WP02 semantics listed above.

Do not implement hidden state behind this interface merely to avoid Application changes.

This authority explicitly permits the minimum coherent Application contract extension required for #227.

---

# Permitted Application Contract Scope

Potentially permitted, when directly required by #227:

- `IObservationSource`;
- `ResearchRequest`;
- `ObservationSourceResult`;
- directly related Application-layer replay/request/result contract types;
- cancellation propagation;
- replay/session identity representation;
- logical tick/position representation;
- restart/resume representation;
- duplicate behavior representation;
- finite replay/end-of-stream representation;
- tests for those contract semantics;
- compatibility updates to existing callers/implementations caused directly by the contract change.

Prefer the smallest explicit contract.

Do not introduce speculative future fields or broad abstractions.

---

# Permitted Infrastructure Scope

WP02 may implement the already-planned Infrastructure work required by #227, including where applicable:

- replay observation source;
- replay configuration;
- dependency-injection registration;
- deterministic replay behavior;
- finite replay lifecycle;
- restart/resume behavior;
- duplicate semantics;
- cancellation support;
- focused infrastructure tests.

The Infrastructure implementation must use the amended public contract rather than bypass it.

---

# Explicitly Forbidden

Do not:

- implement WP03 or later work;
- perform broad Application refactoring;
- introduce a new architectural layer unless #227 cannot be satisfied otherwise;
- change Python version;
- change package pins;
- change Streamlit version;
- change schema version unless #227 explicitly requires a schema change;
- change the one-shot JSON-over-stdio boundary unless #227 explicitly requires a compatible contract adjustment and Release 1.9 authority permits it;
- add unrelated persistence/networking/scheduling infrastructure;
- alter Release 1.9 planning;
- modify dependency edges;
- create replacement WP issues;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #227 before technical acceptance passes;
- hide required semantics in static/global mutable state, undocumented adapter state, side channels, or magic sentinel values.

---

# Phase 0 — Read and Extract

Before implementation:

1. Read #227 completely.
2. Read the relevant Release 1.9 WP02 definition/manifest.
3. Read:
   - `IObservationSource`
   - `ResearchRequest`
   - `ObservationSourceResult`
4. Read all `IObservationSource` implementations.
5. Read all call sites.
6. Read related tests.
7. Extract exact #227:
   - objective;
   - deliverables;
   - acceptance criteria;
   - authorized paths;
   - replay semantics;
   - configuration/DI requirements;
   - lifecycle expectations.
8. Build a requirement-to-contract-and-implementation matrix.

Do not code until the matrix is clear.

If #227 is ambiguous or conflicts materially with Release 1.9 authority, stop.

---

# Phase 1 — Contract Design Gate

For each required semantic:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate behavior;
- cancellation;
- finite replay;

document:

1. why the existing contract cannot safely represent it;
2. the minimum contract element required;
3. affected callers/implementations;
4. expected compatibility behavior;
5. focused test strategy.

Design requirements:

- semantics explicit;
- deterministic where required;
- finite completion distinguishable from failure;
- restart/resume unambiguous;
- cancellation propagated through the public contract;
- duplicate behavior observable and testable;
- naming consistent with repository conventions.

Prefer additive/minimal changes.

Do not preserve an inadequate old API merely to minimize file count.

### Hard stop

If multiple materially different contract architectures remain equally valid and #227 does not resolve them, stop and report the ambiguity rather than guessing.

---

# Phase 2 — Application Contract Implementation

Implement the minimum contract extension established in Phase 1.

Rules:

1. keep the contract small;
2. preserve existing non-replay behavior unless #227 explicitly changes it;
3. update all affected implementations/callers only as needed to compile and preserve behavior;
4. use repository-standard cancellation semantics;
5. do not convert the subsystem broadly to async unless #227 requires it;
6. make finite completion explicit enough for callers/tests to distinguish end-of-replay from empty data or error;
7. avoid compatibility shims that conceal semantics.

Run targeted compile/tests after each logical contract change.

---

# Phase 3 — Infrastructure Replay Implementation

Implement the #227 replay/configuration/DI behavior against the amended contract.

The implementation must satisfy all required semantics:

## Replay identity
- explicit identity used consistently;
- deterministic behavior for the same replay identity where required.

## Logical ticks
- explicit deterministic logical progression;
- ordering stable and testable.

## Restart/resume
- explicit start/resume semantics;
- reproducible restart behavior.

## Duplicate behavior
- exact #227 duplicate semantics;
- no accidental hidden deduplication or duplication.

## Cancellation
- public-contract cancellation reaches the replay implementation;
- no continued work after cancellation when that would violate #227.

## Finite replay
- completion/end state observable;
- reading/requesting beyond available replay data follows #227 semantics.

Add configuration and DI wiring only as required.

Do not introduce hidden state that contradicts the public contract.

---

# Phase 4 — Focused Acceptance Tests

Add/update focused tests that independently prove each WP02 acceptance semantic.

At minimum:

### Replay identity
- distinct identities distinguishable;
- same identity deterministic where required.

### Logical ticks
- expected progression;
- deterministic ordering.

### Restart/resume
- requested restart position/state honored;
- repeated restart behavior stable.

### Duplicate behavior
- exact duplicate semantics proven.

### Cancellation
- cancellation propagates through the public contract;
- expected cancellation result/exception behavior;
- replay does not continue improperly.

### Finite replay
- end-of-replay explicitly observable;
- end is not confused with failure;
- over-read/past-end behavior proven.

Also add compatibility tests for any existing observation source affected by the contract change.

Do not mark acceptance complete from code inspection alone.

---

# Phase 5 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact command;
- exit status;
- passed;
- failed;
- skipped;
- material warnings.

Historical baseline was 281/281.

If WP02 adds tests, a larger total is expected.

A changed count must be fully explained by WP02-owned test changes.

Do not weaken or remove tests to restore the historical count.

---

# Phase 6 — Diff and Scope Audit

Inspect the complete final diff.

Classify every changed file as exactly one of:

- Application contract change required by WP02;
- compatibility update directly caused by that contract;
- Infrastructure replay/configuration/DI implementation;
- WP02-focused test;
- WP02-required documentation.

Anything else is unauthorized unless #227 explicitly requires it.

Prove:

- no WP03+ work;
- no unrelated refactor;
- no package/Python/schema/version change unless explicitly authorized;
- no planning authority mutation;
- no residual hidden-state workaround;
- no stale partial artifacts from the prior blocked attempt.

If unauthorized WP02-session changes exist and ownership is certain, revert only those changes.

Do not disturb pre-existing user work.

---

# Phase 7 — Technical Acceptance Gate

Before GitHub mutation, prove every #227 acceptance criterion individually.

For each criterion report:

- criterion;
- implementation evidence;
- test evidence;
- PASS/FAIL.

If any criterion fails, stop.

Do not transition #227 lifecycle state.

---

# Phase 8 — WP02 GitHub Lifecycle Finalization

Only after technical acceptance is fully proven:

1. read #227 current state;
2. discover the established completion convention from WP01 and current Project #2 configuration;
3. add one concise completion/evidence comment if required;
4. update #227 Project Status from Backlog to the authoritative completed value;
5. preserve:
   - Priority = P1
   - Release = 1.9
   - authoritative Area;
6. close #227;
7. keep milestone #58 open;
8. immediately read back all mutations.

Do not assume the completion status name if Project configuration has changed.

Do not mutate #228.

---

# Expected Post-Completion State

After successful WP02 completion:

- #226 = closed / Done;
- #227 = closed / authoritative completed status;
- #228–#237 remain open and untouched;
- milestone #58 remains open;
- canonical milestone counts become:
  - 10 open
  - 2 closed;
- raw GitHub closed count may additionally include #225;
- dependency chain remains 11/11;
- WP03 #228 becomes next eligible;
- WP03 does not start automatically.

---

# Stop Conditions

Stop immediately if:

- #227 cannot be read;
- #227 identity is ambiguous;
- WP01 completion predecessor gate fails;
- repository is not actually clean at start;
- contract requirements are ambiguous;
- replay identity/restart/duplicate/cancellation/finite semantics cannot be derived from #227;
- contract change would require broad architecture redesign;
- implementation would require WP03+ scope;
- package/Python/schema/protocol changes are needed but not explicitly authorized;
- focused acceptance tests fail for reasons outside WP02;
- full regression fails for reasons not owned by WP02;
- final diff contains unexplained changes;
- GitHub lifecycle mutation cannot be proven;
- milestone #58 is unexpectedly closed;
- protected objects appear changed.

On stop:

- preserve evidence;
- do not broaden scope;
- distinguish pre-existing state from WP02-owned changes;
- report exact blocker and last proven state.

---

# Success Criteria

WP02 succeeds only when:

- all #227 acceptance criteria pass;
- required Application contract semantics are explicit;
- replay identity tested;
- logical ticks tested;
- restart/resume tested;
- duplicate behavior tested;
- cancellation tested;
- finite replay tested;
- Infrastructure replay/configuration/DI implementation uses the amended contract;
- existing affected behavior remains correct;
- focused tests pass;
- full authoritative regression passes;
- final diff remains strictly WP02-scoped;
- no unauthorized foundation changes occur;
- #227 receives required completion evidence;
- #227 Project item reaches authoritative completed state;
- #227 is closed;
- milestone #58 remains open;
- #228–#237 remain open and untouched;
- dependency chain remains intact;
- #225 and protected milestones remain unchanged;
- WP03 has not started.

---

# Required Completion Report

Return:

## WP02 authority
- #227 exact title;
- objective;
- acceptance criteria.

## Contract amendment
- original limitation;
- exact new contract shape;
- requirement-to-contract mapping;
- compatibility impact.

## Implementation
- files changed;
- replay/configuration/DI summary;
- tests added/changed.

## Acceptance semantics
Report PASS/FAIL evidence for:
- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- finite replay.

## Validation
- focused test results;
- full regression command;
- exact passed/failed/skipped counts.

## Scope proof
- final diff classification;
- confirmation no WP03+ work;
- confirmation no unauthorized foundation changes.

## GitHub lifecycle
- #227 state before/after;
- Project Status before/after;
- completion comment status;
- milestone #58 canonical counts;
- confirmation #228–#237 untouched.

## Next eligibility
State:

`NEXT ELIGIBLE WORK PACKAGE: WP03 — #228`

Do not execute or authorize WP03.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP02 COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP02 BLOCKED`

Do not emit the success marker unless all technical and lifecycle requirements are freshly proven.

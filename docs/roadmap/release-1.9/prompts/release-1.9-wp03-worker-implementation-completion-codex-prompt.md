# Release 1.9 — WP03 Worker Implementation/Completion — Codex Authority

## Authority

This document grants fresh execution authority to complete:

**Release 1.9 WP03 — canonical GitHub issue #228**

This authority begins from a partially completed but validated WP03 implementation plus a separately completed normative Worker configuration-definition pass.

Accepted current state:

- WP01 #226: Closed / Done;
- WP02 #227: Closed / Done;
- WP03 #228: Open / Backlog;
- WP04 #229 and later WPs remain unstarted;
- one private canonical five-stage `ExecuteCanonical` executor is implemented;
- historical and explicit-observation materialization converge on that executor;
- historical observation acquisition remains through `IHistoricalObservationStore`;
- replay does not masquerade as historical storage;
- the additive observation-input seam is implemented;
- Application focused tests: 122/122 passed;
- WP02 replay suite: 142/142 passed;
- full regression: 288/288 passed;
- build: 0 errors / 0 warnings;
- no WP03 GitHub lifecycle mutation has occurred;
- no package, Python, schema, planning, or protected-milestone mutation has occurred.

The missing Worker configuration semantics have now been normatively defined.

This authority treats that Worker configuration contract as **fixed input**.

Do not redesign it.

This authority is for WP03 only.

It does not authorize WP04 or later work.

---

# Normative Worker Configuration Contract

The following contract is authoritative for this execution.

## Canonical configuration path

Section:

`Worker`

Keys:

- `Worker:Mode`
- `Worker:Replay:ReplayIdentity`
- `Worker:Replay:Target`
- `Worker:Replay:StartingTick`
- `Worker:Replay:RequestedObservationCount`

Environment-variable overrides use standard .NET double-underscore mapping:

- `Worker__Mode`
- `Worker__Replay__ReplayIdentity`
- `Worker__Replay__Target`
- `Worker__Replay__StartingTick`
- `Worker__Replay__RequestedObservationCount`

## Mode contract

Key:

`Worker:Mode`

Accepted values:

- `Historical`
- `Replay`

Matching:

- case-insensitive ordinal comparison

Missing mode:

- defaults to `Historical`

Unknown explicit value:

- fail fast before execution

Historical mode:

- uses the existing historical path and `IHistoricalObservationStore`

Replay mode:

- requires and validates nested replay settings

## Replay settings

### `Worker:Replay:ReplayIdentity`

- type: string
- required in Replay mode
- default: none
- validation: non-empty; bounded identity under existing WP02 conventions
- mapping:
  - `ReplayRequest.ReplayIdentity`
  - existing replay configuration identity

### `Worker:Replay:Target`

- type: string
- required in Replay mode
- default: none
- validation: non-empty; exact target match under WP02 semantics
- mapping:
  - `ReplayRequest.Target`
  - existing replay configuration target

### `Worker:Replay:StartingTick`

- type: non-negative integer
- required in Replay mode
- default: none
- validation:
  `0 <= StartingTick <= fixture length`
- mapping:
  `ReplayRequest.StartingTick`

### `Worker:Replay:RequestedObservationCount`

- type: positive integer
- required in Replay mode
- default: none
- validation:
  `> 0`
- mapping:
  `ReplayRequest.RequestedObservationCount`
- upper/bound behavior remains governed by WP02 replay semantics

## Binding and validation

The future implementation must bind the `Worker` section into narrow Worker-owned configuration types and validate during startup/configuration construction before execution dispatch.

Required semantics:

- missing/malformed numeric values fail fast using existing .NET configuration/argument-validation conventions;
- Replay mode with missing replay section fails fast;
- Replay mode with missing/invalid required fields fails fast;
- Historical mode does not require replay settings;
- replay-only settings outside Replay mode are ignored for compatibility and do not alter historical execution;
- Replay mode never silently falls back to Historical.

## Backward compatibility

Missing `Worker:Mode` selects Historical mode.

Existing configurations without Worker replay settings retain:

`Worker → historical materialization → IHistoricalObservationStore → ExecuteCanonical → stages 1–5`

Historical materialization, persistence, catalog registration, schema v3, and existing pipeline behavior remain unchanged.

## Lifecycle ownership

Restart/resume:

- selected by `StartingTick`;
- maps directly to `ReplayRequest.StartingTick`.

Cancellation:

- no Worker configuration field;
- runtime cancellation token propagates to WP02 replay execution.

Finite completion:

- fixed by WP02;
- successful end-of-replay terminates Replay-mode execution;
- end-of-replay is not a pipeline failure.

Not configurable at Worker level:

- replay identity determinism;
- logical tick progression;
- duplicate determinism;
- fixture length;
- replay bounds semantics;
- end-of-replay semantics beyond selecting bounded request inputs.

Do not add additional Worker replay fields.

---

# Objective

Implement the fixed Worker configuration contract and complete the real production replay composition required by #228:

`Worker explicit mode/config`
→ `WP02 replay source/configuration`
→ `WP03 observation-input seam`
→ `ExecuteCanonical`
→ canonical stages 1–5
→ finite completion

Preserve the existing Historical path exactly as governed.

After technical acceptance, finalize #228 under the established GitHub lifecycle convention.

---

# Fixed Accepted Architecture

Do not reopen these decisions.

## Historical path

`Worker`
→ historical materialization
→ `IHistoricalObservationStore`
→ WP03 materialization
→ `ExecuteCanonical`
→ stages 1–5

## Replay path

`Worker explicit Replay mode/config`
→ WP02 replay source/configuration
→ explicit replay observations
→ WP03 observation-input seam
→ `ExecuteCanonical`
→ same stages 1–5
→ finite completion

Exactly one canonical five-stage executor must remain.

---

# Permitted Scope

This authority may modify only the minimum WP03 Worker/configuration/composition surface required to implement the fixed contract and complete #228.

Potentially permitted:

- narrow Worker options/configuration types;
- configuration binding;
- validation;
- explicit Historical/Replay dispatch;
- Worker composition root;
- DI wiring required to select/resolve existing WP02 replay implementation;
- mapping from Worker replay settings to WP02 replay request/configuration;
- runtime replay loop/execution required by #228;
- cancellation-token propagation;
- Worker/runtime focused tests;
- narrowly required compatibility updates;
- minimal #228-required configuration documentation/examples if explicitly required.

Existing pipeline/seam code may change only when a concrete integration defect is proven and the correction is minimal.

---

# Explicitly Forbidden

Do not:

- alter the normative Worker configuration contract;
- add modes beyond `Historical` and `Replay`;
- add additional replay configuration fields;
- create a generalized mode/strategy/plugin framework;
- roll back or redesign `ExecuteCanonical` for preference;
- create a second canonical executor;
- create a replay-specific pipeline;
- bypass the WP03 observation seam;
- use `IHistoricalObservationStore` as replay storage;
- redesign WP02 replay contracts;
- change package pins;
- change Python version;
- change Streamlit version;
- change SQLite schema version;
- redesign the one-shot JSON-over-stdio boundary;
- alter Release 1.9 planning;
- change dependencies;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- implement WP04+;
- close #228 before all technical gates pass.

---

# Phase 0 — Fresh State Proof

Before mutation:

1. Read #228 completely.
2. Read the Release 1.9 WP03 manifest.
3. Confirm the fixed normative Worker configuration contract above is consistent with current repository state.
4. Read the Worker entry point/composition root.
5. Read existing Worker configuration conventions.
6. Read WP02 replay configuration/contracts and DI registration.
7. Read WP03 observation seam and `ExecuteCanonical`.
8. Read current Worker tests.
9. Record branch, HEAD, staged/tracked state, and existing WP03 diff.
10. Prove no unrelated/uncertain local changes exist.

Stop if current repository state conflicts materially with the accepted partial WP03 baseline.

---

# Phase 1 — Implement Worker Configuration Types

Implement the smallest Worker-owned configuration types necessary to bind:

- `Worker:Mode`
- nested `Worker:Replay` settings

Requirements:

- names align with the normative keys;
- no aliases;
- no speculative fields;
- no generalized hierarchy beyond Historical/Replay;
- parsing behavior follows existing .NET conventions.

Prefer existing repository options/configuration patterns.

---

# Phase 2 — Implement Validation

Implement fail-fast validation for:

## Mode

- missing mode → Historical;
- explicit `Historical` accepted;
- explicit `Replay` accepted;
- case-insensitive ordinal matching;
- unknown mode rejected before execution.

## Replay settings

When mode = Replay:

- replay section must exist;
- ReplayIdentity required and non-empty;
- Target required and non-empty;
- StartingTick required and non-negative;
- RequestedObservationCount required and > 0;
- any fixture-length/upper-bound validation must map to existing WP02 semantics and must not redefine them.

When mode = Historical:

- replay settings are not required;
- replay-only settings must not alter historical selection/execution.

Do not invent custom exception taxonomies when existing .NET/repository validation conventions suffice.

---

# Phase 3 — Implement Runtime Dispatch

Implement explicit Worker dispatch:

## Historical

If mode is Historical, including default/missing mode:

- preserve the existing historical execution path;
- preserve `IHistoricalObservationStore`;
- preserve existing materialization behavior;
- preserve `ExecuteCanonical`;
- preserve existing persistence/catalog behavior.

## Replay

If mode is Replay:

1. construct/map the fixed replay settings into the completed WP02 request/configuration;
2. resolve/use the real WP02 replay source;
3. preserve ReplayIdentity;
4. preserve Target;
5. preserve StartingTick;
6. preserve RequestedObservationCount;
7. obtain replay increments under WP02 semantics;
8. pass explicit observations into the WP03 observation-input seam;
9. invoke the existing `ExecuteCanonical`;
10. traverse all five canonical stages;
11. terminate normally on WP02 finite end-of-replay.

Replay mode must never silently route to Historical.

---

# Phase 4 — Cancellation and Lifecycle Propagation

Preserve WP02 semantics through Worker composition.

Prove:

- runtime cancellation token reaches WP02 replay execution;
- cancellation prevents unauthorized continued replay/pipeline processing;
- restart/resume is represented only by StartingTick;
- duplicate determinism is unchanged;
- logical tick progression is unchanged;
- bounds remain governed by WP02;
- finite end-of-replay is successful completion, not pipeline failure.

Do not add configuration for behaviors already fixed by WP02.

---

# Phase 5 — Focused Configuration Tests

Add tests proving:

## Mode

- explicit Historical works;
- explicit Replay works;
- missing mode defaults to Historical;
- unknown mode fails before execution;
- case-insensitive matching works as defined.

## Replay settings

- valid Replay settings bind;
- missing replay section fails in Replay mode;
- each required field missing fails;
- empty ReplayIdentity fails;
- empty Target fails;
- negative StartingTick fails;
- zero RequestedObservationCount fails;
- negative RequestedObservationCount fails if binding/type permits such a case;
- malformed numeric values fail according to .NET binding conventions.

## Compatibility

- Historical mode succeeds without replay settings;
- replay-only settings do not cause Historical mode to switch to Replay;
- missing mode with replay-only settings still defaults to Historical.

---

# Phase 6 — Focused Production Composition Tests

Prove real Worker composition.

At minimum:

- Historical mode selects the real historical path;
- Replay mode resolves the real WP02 replay source/configuration;
- all Worker replay settings map exactly to the corresponding WP02 fields;
- replay observations reach the real WP03 observation-input seam;
- replay reaches the shared `ExecuteCanonical`;
- all five canonical stages execute;
- historical and replay paths converge on the same canonical executor;
- no parallel pipeline exists.

Exercise production composition as directly as repository conventions permit.

Do not satisfy these gates only through mocks that bypass the real wiring.

---

# Phase 7 — Replay Semantic Regression

Prove through Worker/runtime tests or predecessor suites as appropriate:

- replay identity;
- logical ticks;
- restart/resume;
- duplicate determinism;
- cancellation;
- bounds;
- finite/end-of-replay.

Do not weaken WP02 behavior.

---

# Phase 8 — Required Regression Guards

Rerun the previously proven suites.

Required baseline guards:

- Application focused suite: pre-implementation **122/122**
- WP02 replay suite: pre-implementation **142/142**

Both must remain fully passing at their current justified counts.

If new tests are added to either suite, explain the higher count.

---

# Phase 9 — Build and Full Regression

Run the established repository build.

Require:

- 0 errors;
- report warnings exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor full-suite baseline:

**288/288 passing**

Capture:

- exact exit status;
- passed;
- failed;
- skipped;
- material warnings.

A higher count is expected when Worker tests are added.

An unexplained lower count is a blocker.

Do not remove/weaken tests merely to pass.

---

# Phase 10 — Production Flow Proof

Before technical acceptance, document actual production code paths.

Historical:

`Worker`
→ Historical/default mode
→ historical materialization
→ `IHistoricalObservationStore`
→ WP03 materialization
→ `ExecuteCanonical`
→ stages 1–5

Replay:

`Worker`
→ `Worker:Mode=Replay`
→ validated `Worker:Replay:*`
→ WP02 replay configuration/request
→ WP02 replay source
→ WP03 explicit-observation seam
→ `ExecuteCanonical`
→ stages 1–5
→ finite completion

The proof must reference actual production composition, not only tests or intended architecture.

---

# Phase 11 — Diff and Scope Audit

Inspect the complete WP03 diff, including earlier validated canonical-stage/seam work plus this Worker completion.

Classify every changed file as:

- canonical-stage extraction;
- additive observation seam;
- Worker configuration type;
- Worker validation;
- Worker runtime dispatch;
- Worker DI/composition;
- directly required compatibility update;
- WP03 test;
- explicitly required config/documentation artifact.

Prove:

- one canonical `ExecuteCanonical`;
- Historical and Replay production paths converge there;
- no parallel pipeline;
- no replay use of historical storage;
- no generalized mode framework;
- no additional replay fields beyond the normative contract;
- no unnecessary WP02 redesign;
- no WP04+ work;
- no unauthorized package/Python/schema/protocol/planning change;
- pre-existing authority/control files remain preserved.

Anything unexplained blocks acceptance.

---

# Phase 12 — WP03 Technical Acceptance Gate

Before GitHub mutation, enumerate every #228 acceptance criterion.

For each, report:

- criterion;
- implementation evidence;
- test evidence;
- PASS/FAIL.

Additionally require PASS for:

- canonical executor preserved;
- observation seam preserved;
- exact Worker configuration contract implemented;
- Historical default behavior;
- explicit Replay selection;
- fail-fast invalid mode/configuration;
- exact WP02 mapping;
- real production replay composition;
- five canonical stages through Replay mode;
- historical compatibility;
- replay identity;
- logical ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- finite completion;
- Application focused suite;
- WP02 replay suite;
- Worker/configuration tests;
- build;
- full regression;
- diff/scope audit.

If any gate fails, leave #228 Open / Backlog and stop.

---

# Phase 13 — WP03 GitHub Lifecycle Finalization

Only after all technical acceptance passes:

1. read #228 current state;
2. confirm established completion convention;
3. add one concise completion/evidence comment if required;
4. transition #228 Project Status from Backlog to the authoritative completed value;
5. preserve:
   - Priority = P1;
   - Release = 1.9;
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. immediately read back all mutations.

Do not modify #229.

---

# Expected Post-Completion State

After successful completion:

- #226 Closed / Done;
- #227 Closed / Done;
- #228 Closed / Done or current authoritative completed state;
- #229–#237 remain Open and untouched;
- milestone #58 remains Open;
- canonical milestone counts:
  - 9 open
  - 3 closed;
- raw GitHub closed count may additionally include historical duplicate #225;
- dependency chain remains 11/11;
- successful final WP03 regression count becomes WP04's predecessor baseline;
- WP04 #229 becomes next eligible;
- WP04 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- #228 or manifest cannot be read;
- current validated WP03 pipeline/seam state is missing;
- normative Worker contract conflicts with repository reality in a way requiring redesign;
- implementation requires changing the normative configuration contract;
- a second pipeline or executor becomes necessary;
- replay would need to use historical storage;
- WP02 semantics regress;
- WP04+ scope becomes necessary;
- unauthorized package/Python/schema/protocol changes become necessary;
- focused tests fail;
- build fails;
- full regression fails;
- production composition cannot be proven;
- diff audit reveals unexplained scope;
- GitHub lifecycle mutation fails or cannot be proven.

On stop:

- preserve validated prior WP03 work;
- do not broaden authority;
- report exact blocker and last proven state;
- leave #228 open unless technical acceptance had fully passed and lifecycle mutation alone failed.

---

# Success Criteria

WP03 succeeds only when:

- fixed Worker configuration contract is implemented exactly;
- Historical/default behavior remains backward compatible;
- Replay mode is explicit and validated;
- all replay settings map exactly to WP02;
- real Worker replay composition uses the WP02 replay source;
- replay observations enter the WP03 seam;
- Historical and Replay converge on `ExecuteCanonical`;
- all five stages execute through real Replay mode;
- all WP02 semantics remain intact;
- every #228 acceptance criterion passes;
- focused configuration/runtime tests pass;
- Application focused suite passes;
- WP02 replay suite passes;
- build passes;
- full regression passes;
- final diff remains WP03-scoped;
- #228 is completed and closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- dependency chain remains intact;
- WP04 has not started.

---

# Required Completion Report

Return:

## Configuration implementation
- exact Worker configuration types;
- exact binding path;
- mode parsing;
- validation behavior;
- default behavior.

## WP02 mapping
For each replay field:
- Worker key;
- mapped WP02 field;
- validation evidence.

## Runtime composition
- actual Historical flow;
- actual Replay flow;
- proof both converge on `ExecuteCanonical`;
- proof Replay uses WP02 source and WP03 seam.

## Validation
Report:
- configuration-focused tests;
- Worker/runtime composition tests;
- Application suite count;
- WP02 replay suite count;
- build errors/warnings;
- full regression command and exact counts.

## Acceptance
Report PASS/FAIL for every #228 criterion and explicitly:
- Historical;
- Replay;
- missing mode;
- unknown mode;
- missing/invalid replay config;
- identity;
- ticks;
- restart/resume;
- duplicates;
- cancellation;
- bounds;
- finite completion;
- five-stage production replay path.

## Scope proof
- full diff classification;
- no contract redesign;
- no parallel pipeline;
- no historical-store replay misuse;
- no generalized mode framework;
- no WP04+ work;
- no unauthorized foundation/planning changes.

## GitHub lifecycle
- #228 before/after;
- Project Status before/after;
- completion comment;
- milestone #58 canonical counts;
- confirmation #229–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP03 WORKER IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker, end with exactly:

`RELEASE 1.9 WP03 WORKER IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical and lifecycle requirement is freshly proven.

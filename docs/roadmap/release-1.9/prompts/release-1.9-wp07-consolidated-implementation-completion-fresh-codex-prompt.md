# Release 1.9 — WP07 Consolidated Implementation + Completion — Fresh Authority

## Authority

Use **GPT-5.6 Terra**.

This is the **fresh consolidated implementation/completion authority** for Release 1.9 WP07, canonical issue **#232**.

WP07 may be closed only if every acceptance gate in this authority passes.

WP08 (#233) must remain Open / Backlog and unstarted.

---

# Binding Authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
3. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_FEATURE_DATA_QUALITY_PRESENTATION_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
4. `docs/roadmap/release-1.9/RELEASE_1.9_WP06_VISUALIZATION_FRAME_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
5. The completed canonical semantic-exposure implementation/acceptance evidence:
   `RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE COMPLETE`
6. The completed local-repository reconciliation/classification evidence:
   `RELEASE 1.9 WP07 LOCAL-REPOSITORY RECONCILIATION AND INTERSECTING-DIFF CLASSIFICATION COMPLETE`
7. Existing Release 1.9 manifest and accepted WP04/WP05/WP06 amendments necessary to preserve predecessor ownership.

Meaning comes from the semantic definition.
Exposure paths come from the semantic-exposure amendment.
Presentation semantics and WP07 presentation paths come from the presentation amendment.
Pre-existing local work preservation comes from the reconciliation rule.

If any binding authorities conflict, stop before mutation.

---

# Reconciliation Rule

Do **not** require a clean worktree.

The completed reconciliation established that intersecting pre-existing dirty changes are accepted **Class A predecessor work**.

Mandatory:

- preserve all Class A predecessor behavior;
- preserve unrelated local dirty state;
- do not reset, restore, checkout, stash, clean, revert, or overwrite;
- add/modify only the exact WP07 presentation symbols authorized by the presentation amendment;
- preserve the already-completed Class B canonical semantic-exposure implementation;
- create the dedicated WP07 presentation test only at its authorized path.

If current dirty state materially differs from the completed reconciliation or completed semantic-exposure state, stop and report the exact new ambiguity.

---

# Entry State

Expected accepted predecessor state:

- WP01–WP06: Closed / Done.
- #231: Closed / Done.
- #232: Open / Backlog.
- #233: Open / Backlog.
- WP08/WP09 unstarted.
- schema v4.
- full .NET: **309/309**.
- Application: **125/125**.
- Infrastructure: **160/160**.
- WP07 semantic-exposure Python: **2/2**.
- WP05 Python: **3/3**.
- WP06 Python: **6/6**.
- build: 0 warnings / 0 errors.
- Streamlit: 1.61.1.
- `pip check`: clean.
- reserved `python/presentation/test_realtime_financial_visualization_wp07.py` is not yet created.

Verify all current facts.

---

# Objective

Implement exactly the fixed WP07 presentation contract in the accepted shared Streamlit surface.

Required production result:

- the exact five presentation sections;
- exact section labels/order;
- exact field labels/order;
- exact value formatting;
- exact `Unavailable` representation;
- exact Ready/WarmUp/Empty/Failed/Stale behavior;
- exact transport-warning separation;
- pure deterministic `project_wp07_presentation_sections(...)` assertion surface;
- additive Streamlit rendering of those sections.

No upstream semantic invention is permitted.

---

# Phase 0 — Binding Contract Extraction

Before mutation, extract from the WP07 presentation amendment:

- exact five section names;
- exact section order;
- exact rows in each section;
- exact row labels;
- exact frame source property for every row;
- exact formatting rules;
- exact unavailable token;
- exact state matrix;
- exact transport-warning placement;
- exact `project_wp07_presentation_sections(...)` input/output contract;
- exact shared production path and authorized symbols;
- exact dedicated WP07 test path;
- explicit forbidden concerns.

Build an implementation checklist from those exact values.

Do not substitute “equivalent” labels.

---

# Phase 1 — Repository / Lifecycle Verification

Record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked state.

Verify:

- #232 Open / Backlog;
- #233 Open / Backlog;
- no WP08/WP09 implementation;
- no unauthorized WP07 presentation implementation already exists;
- completed semantic-exposure fields exist in Application/WP04/JSON/WP05/WP06 exactly as accepted.

No lifecycle mutation yet.

---

# Phase 2 — Predecessor Gate

Before WP07 presentation mutation run:

## .NET
- build;
- full regression.

Expected: **309/309**, 0 failed.

## Python
- WP07 semantic-exposure: **2/2**;
- WP05: **3/3**;
- WP06: **6/6**;
- compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

If predecessor validation fails, stop. Do not mask it with WP07 presentation changes.

---

# Phase 3 — Path/Symbol Hard Gate

Only mutate exact paths/symbols authorized by:

`RELEASE_1.9_WP07_FEATURE_DATA_QUALITY_PRESENTATION_CONTRACT_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

Expected production path:

`python/presentation/realtime_financial_visualization.py`

Expected new test path:

`python/presentation/test_realtime_financial_visualization_wp07.py`

Use actual binding artifact values if more precise.

No helper path may be invented.

No upstream Application/.NET/WP04/WP05 semantic change is authorized unless the presentation amendment explicitly names it; the canonical facts are already exposed.

---

# Phase 4 — Pure Projection

Implement the exact pure deterministic:

`project_wp07_presentation_sections(...)`

contract fixed by the presentation amendment.

Requirements:

- no Streamlit calls inside the pure projection unless the binding contract explicitly says otherwise;
- deterministic ordered result;
- exact five sections;
- exact row order;
- exact labels;
- exact formatted values;
- no semantic derivation beyond presentation formatting;
- no mutation of `VisualizationFrame`;
- no I/O;
- no provider/SQLite access;
- no feature recomputation.

Prefer immutable tuples/records/structures exactly as fixed by the contract.

---

# Phase 5 — Feature Section

Implement exactly the fixed feature section.

Use only accepted frame fields.

Prove:

- feature identity is direct;
- available feature value uses exact deterministic formatting;
- WarmUp uses the exact fixed representation;
- unavailable value uses the fixed unavailable token;
- current/required counts and timestamp are shown only if the binding contract includes them.

Do not compute lag-1 return.

Do not infer data quality.

---

# Phase 6 — Snapshot/Data Section

Implement exact snapshot/data presentation.

Use only accepted snapshot/frame facts.

Prove:

- exact identity representation;
- exact version representation;
- exact missing/unavailable token;
- no SQLite/provider/path/provenance leakage.

If the contract uses deterministic abbreviation, implement the exact rule and nothing else.

---

# Phase 7 — Data-Quality Section

Render direct canonical:

- `Valid`;
- `Invalid`;
- `Unavailable`;

using the exact fixed labels/representation.

No score.
No severity.
No confidence.
No explanation derived from pipeline success.
No validation recomputation.

---

# Phase 8 — Pipeline Section

Render only the exact pipeline facts fixed by the presentation contract.

Preserve distinction between:

- backend presentation state;
- pipeline success/failure;
- failure category/message if explicitly allowed;
- transport warning.

Do not invent pipeline stages or reinterpret WP04 state.

---

# Phase 9 — Idempotency Section

Render direct canonical:

- `NewlyPersisted`;
- `EquivalentExisting`;
- `Unavailable`;

with exact fixed user-facing strings.

Do not derive from:

- revision;
- cache;
- transport retry;
- duplicate tick;
- observation persistence.

Do not label `NewlyPersisted` as “non-idempotent”.

---

# Phase 10 — Five-State Rendering

Implement exact presentation behavior for:

- Ready;
- WarmUp / NotReady;
- Empty;
- Failed;
- Stale.

Use the fixed state matrix.

Prove:

- retained last-good facts remain retained where specified;
- unavailable facts are explicit;
- failure does not silently become data-quality Invalid unless canonical status is Invalid;
- WarmUp does not become failure;
- Stale does not invent wall-clock semantics.

No new state.

---

# Phase 11 — Transport Warning

Keep transport warning separate exactly as fixed.

It must not change:

- data-quality status;
- idempotency status;
- pipeline status;
- backend state.

Render/place it only at the exact presentation location authorized by the contract.

---

# Phase 12 — Streamlit Integration

Integrate the five-section projection into the existing Streamlit entry/rendering flow with the minimum authorized change.

Preserve WP06:

- price/time line chart;
- latest observation;
- count/window;
- existing factual metadata;
- sequential-frame behavior.

Do not redesign layout.

No CSS.
No custom theme.
No new package.
No polling/cache/lifecycle change.

WP07 is additive.

---

# Phase 13 — Dedicated WP07 Presentation Tests

Create only:

`python/presentation/test_realtime_financial_visualization_wp07.py`

or the exact path fixed by the binding amendment.

Implement deterministic functional tests for every acceptance item defined there.

At minimum prove:

- exact five section order;
- exact section names;
- exact field order;
- exact labels;
- exact formatting;
- exact unavailable token;
- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale;
- feature available;
- feature WarmUp/unavailable;
- snapshot available/unavailable;
- data quality Valid/Invalid/Unavailable;
- idempotency NewlyPersisted/EquivalentExisting/Unavailable;
- pipeline success/failure;
- transport warning separation;
- deterministic repeated projection;
- no mutation of input frame.

Use exact strings from the binding contract.

Do not add WP08 lifecycle or WP09 architecture/integration tests.

---

# Phase 14 — Static Semantic Audit

Search/diff prove:

- no feature formula in presentation;
- no SQLite access;
- no provider access;
- no semantic mapping from `pipelineSuccess` to data quality;
- no semantic mapping from revision/cache/retry to idempotency;
- no new backend states;
- no transport/cache/retry changes;
- no chart semantic changes;
- no WP08/WP09 code.

Hard gate.

---

# Phase 15 — Focused Python Validation

Run exact dedicated WP07 presentation tests.

Then run:

- WP07 semantic-exposure: expected **2/2**;
- WP06: expected **6/6**;
- WP05: expected **3/3**.

Report exact final counts.

If predecessor tests need modification to pass, stop unless the binding presentation amendment explicitly authorizes that exact test change.

---

# Phase 16 — Streamlit / Python Guard

Run:

- Python compile checks;
- Streamlit 1.61.1 import smoke;
- `pip check`;
- any repository-standard short Streamlit smoke that does not cross into WP08 lifecycle ownership.

Verify no Streamlit process residue.

Do not perform WP08 demonstration/lifecycle work.

---

# Phase 17 — .NET Regression

Run:

- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Expected predecessor full .NET total remains **309/309**, because WP07 presentation implementation is Python-only unless the binding amendment explicitly authorizes .NET tests.

Require:

- 0 failed;
- 0 skipped unless already governed;
- build 0 warnings / 0 errors.

Any unexpected .NET mutation blocks completion.

---

# Phase 18 — Preservation Audit

Compare final shared presentation file against its pre-WP07 state.

Classify changes as:

- preserved Class A predecessor content;
- preserved completed semantic-exposure content;
- new WP07 presentation content authorized by the presentation amendment.

Prove no predecessor behavior was removed/reinterpreted.

Check unrelated dirty paths remain untouched.

---

# Phase 19 — Scope Audit

List every changed file and exact symbols.

Require zero:

- schema changes;
- persistence changes;
- validation behavior changes;
- provider changes;
- Application semantic changes;
- WP04 state/revision changes;
- Worker JSON contract changes beyond already-completed semantic exposure;
- WP05 parser/cache/retry/lifecycle changes;
- WP06 chart semantic changes;
- new packages;
- WP08;
- WP09.

Only the authorized WP07 presentation production symbol(s) and dedicated WP07 presentation test path should be newly changed by this authority.

---

# Phase 20 — WP07 Completion Gate

Before GitHub mutation, prove all #232 acceptance criteria from the issue and accepted definition are satisfied.

Produce a checklist mapping each #232 requirement to:

- production symbol;
- test evidence;
- validation result.

If any acceptance criterion is unsupported, do not close #232.

---

# Phase 21 — GitHub / Project Completion

Only after all gates pass:

1. update #232 with concise completion evidence if governance requires;
2. close #232;
3. set Project #2 item to Done;
4. preserve its accepted priority/release/category metadata;
5. read back #232 and Project state.

Do not mutate #233 except read-only verification.

Keep:

- #233 Open / Backlog;
- WP08 unstarted;
- milestone #58 open unless independent canonical milestone rules now make closure appropriate; WP07 alone must not close a milestone that still contains open WPs.

Do not start WP08.

---

# Phase 22 — Final Regression After Lifecycle Mutation

No source change should occur during GitHub mutation.

If governance requires, rerun only the minimal read-back/status checks.

Do not create new code after #232 is closed.

---

# Success State

On success:

- fixed WP07 five-section presentation implemented;
- dedicated WP07 presentation tests pass;
- predecessor Python suites pass;
- .NET remains 309/309;
- build 0/0;
- no WP08/WP09 work;
- #232 Closed / Done;
- #233 Open / Backlog.

State:

`NEXT ELIGIBLE WORK PACKAGE: WP08 — #233`

---

# Stop Conditions

Stop if:

- binding authorities conflict;
- current repository differs materially from reconciliation;
- required path/symbol is unauthorized;
- upstream semantic change is required;
- a presentation choice remains undefined;
- predecessor regression fails;
- WP05/WP06 behavior must change;
- WP08/WP09 work becomes necessary;
- dedicated test cannot prove #232 acceptance;
- scope/preservation audit fails;
- GitHub state cannot be truthfully verified.

Preserve valid authorized partial WP07 implementation if appropriate, but do not close #232.

---

# Required Completion Report

## Binding authorities
List all binding artifacts/evidence.

## Entry state
Git/worktree/lifecycle/predecessor baseline.

## Implementation
Exact production path/symbols.

## Five-section contract
Exact section names/order and rendered facts.

## State behavior
Ready/WarmUp/Empty/Failed/Stale.

## Tests
Dedicated WP07 count plus WP07 semantic exposure/WP06/WP05.

## Python guard
Compile/Streamlit/pip/process residue.

## .NET
Application/Infrastructure/Domain/Architecture/build/full 309 baseline.

## Preservation/scope
Exact changed files and zero forbidden concerns.

## #232 acceptance mapping
Requirement → implementation → test.

## GitHub read-back
#232 Closed / Done; #233 Open / Backlog.

## Next work
`NEXT ELIGIBLE WORK PACKAGE: WP08 — #233`

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success or close #232 unless every fixed presentation, regression, preservation, and lifecycle gate passes.

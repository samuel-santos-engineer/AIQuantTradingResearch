# Release 1.9 — WP07 Canonical Semantic-Exposure Implementation + Acceptance — Fresh Authority

## Authority

This is the **fresh implementation and acceptance authority** for the canonical semantic-exposure predecessor work required by Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Terra**.

Two accepted artifacts are jointly **binding**:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

The semantic-definition artifact controls **meaning**.

The manifest/path amendment controls **where and what may change**.

If they conflict, stop before mutation and report the conflict. Do not improvise.

---

# Fixed Semantic Contract

Implement exactly the accepted primitives:

## `PresentationIdempotencyStatus`

- `NewlyPersisted`
- `EquivalentExisting`
- `Unavailable`

Use the exact scope, canonical source, state behavior, and non-equivalences from the binding semantic definition.

## `PresentationDataQualityStatus`

- `Valid`
- `Invalid`
- `Unavailable`

Use the exact validation scope, canonical source, state behavior, and exclusions from the binding semantic definition.

No additional enum members.
No synonyms.
No scores.
No derived presentation semantics.

---

# Fixed Path Contract

Use only the exact production symbols/paths and three dedicated focused test paths authorized by:

`RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`

The amendment is exhaustive.

No wildcard path authority exists.

General WP04/WP05/WP06 ownership remains unchanged.

The reserved later WP07 presentation test path is **not** authorized for this implementation.

---

# Entry State

Expected:

- WP01–WP06 Closed / Done.
- #231 Closed / Done.
- #232 Open / Backlog.
- #233 Open / Backlog.
- WP08/WP09 unstarted.
- schema v4.
- full .NET predecessor baseline: 305/305.
- WP06 Python: 6/6.
- WP05 Python: 3/3.
- build: 0 errors / 0 warnings.
- canonical semantic-definition artifact accepted.
- canonical semantic-exposure path amendment accepted.
- immediately preceding exposure attempt made zero production/test/GitHub mutations.

---

# Objective

Implement and prove the exact additive chain:

canonical source evidence
→ Application semantic projection
→ WP04 envelope
→ Worker JSON
→ WP05 Python parser
→ WP06 additive factual metadata.

This authority ends at **semantic exposure**.

It does not implement WP07 presentation sections, labels, ordering, or rendering.

Success must leave #232 Open / Backlog.

---

# Phase 0 — Read Binding Authorities

Before mutation:

1. Read both binding artifacts completely.
2. Extract:
   - exact semantic source types/properties;
   - exact semantic value domains;
   - exact state matrix;
   - exact JSON/versioning conclusion;
   - every authorized production path;
   - every authorized symbol/concern;
   - all three dedicated test paths;
   - reserved later WP07 presentation path;
   - explicit forbidden adjacent concerns.
3. Build a local execution checklist from those exact facts.

Do not rely on prior chat summaries when the binding files are more specific.

---

# Phase 1 — Repository / Git / Lifecycle Gate

Record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged changes;
- tracked modifications;
- relevant untracked files.

Read GitHub/Project state as permitted and verify:

- #232 Open / Backlog;
- #233 Open / Backlog;
- no WP08/WP09 lifecycle advancement.

Do not mutate GitHub during entry verification.

If unrelated dirty state intersects authorized paths, stop unless existing governance gives a safe reconciliation rule.

---

# Phase 2 — Predecessor Validation

Before implementation run the governed predecessor checks.

## Build

Repository-standard build.

Require 0 errors / 0 warnings.

## Full .NET

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Expected predecessor: **305/305**.

## Python

Run exact accepted commands for:

- WP05 Python: **3/3**;
- WP06 Python: **6/6**;
- compilation/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

If predecessor validation fails nondeterministically, do not mask it with WP07 changes.

Stop and report the predecessor issue.

---

# Phase 3 — Path/Symbol Hard Gate

Compare the implementation plan against the binding path amendment.

For every proposed mutation list:

- exact path;
- exact symbol;
- exact authorized concern.

If any required path/symbol is absent from the amendment:

**STOP BEFORE MUTATION.**

Do not create another helper file.
Do not repurpose a predecessor test.
Do not use the reserved WP07 presentation test path.

---

# Phase 4 — Canonical Source Mapping

Implement only the mappings fixed by the semantic definition.

For idempotency:

- map the exact canonical pipeline persistence source to:
  - `NewlyPersisted`;
  - `EquivalentExisting`;
  - `Unavailable`.

For data quality:

- map the exact canonical validation source to:
  - `Valid`;
  - `Invalid`;
  - `Unavailable`.

Do not modify the source outcomes.

Do not change persistence or validation behavior.

Do not infer values from presentation revision, transport state, cache state, or generic success unless explicitly fixed by the semantic definition.

---

# Phase 5 — Application Semantic Projection

In only the authorized Application path/symbols:

- add the exact immutable semantic value types;
- add the exact additive `PipelineExecutionResult` / `PipelineExecutionEvidence` fields;
- wire direct mapping from canonical source evidence.

Requirements:

- existing callers remain compatible as required;
- no pipeline stage addition;
- no feature change;
- no persistence behavior change;
- no Replay redesign;
- no schema change.

Use exact names fixed by the accepted definition/amendment.

---

# Phase 6 — WP04 Envelope

In only the authorized WP04 symbols:

- add the two canonical factual fields;
- propagate the exact accepted state matrix;
- retain them through Stale only as fixed by the semantic definition;
- preserve unavailable behavior for Empty/Failed/etc. exactly.

Do not alter:

- `aiq-visualization-read-model-v1` except as explicitly permitted by the accepted additive versioning conclusion;
- revision kinds;
- revision comparison;
- state transitions;
- 64-row bound;
- snapshot identity/version;
- feature projection;
- failure payload;
- atomic publication.

---

# Phase 7 — Worker JSON

In only the authorized publisher/serializer symbol:

- serialize the two fields exactly;
- use deterministic accepted enum/string values;
- preserve the accepted contract version;
- preserve all atomic handoff behavior.

Do not change:

- path resolution;
- runtime location;
- temp naming;
- startup cleanup;
- flush/close;
- atomic replacement;
- lifecycle.

---

# Phase 8 — WP05 Parser

In only the authorized `visualization_read_model.py` symbols:

- parse the two canonical fields;
- validate their exact value domains;
- implement exact unavailable/backward behavior from the binding semantic definition;
- preserve them without reinterpretation.

Do not change:

- two-attempt bound;
- 50 ms retry;
- cadence;
- revision comparison;
- cache;
- last-good;
- ProducerUnavailable;
- transport warnings;
- contract-version rejection.

No semantic inference in Python.

---

# Phase 9 — WP06 Additive Projection

In only the authorized `VisualizationFrame` declaration/construction symbols:

- add the two immutable factual metadata fields;
- propagate them directly from the parsed envelope.

Do not render them.

Prove unchanged:

- price/time;
- latest;
- observation count;
- window count/capacity;
- feature identity/value;
- `pipelineSuccess`;
- backend state;
- transport warning;
- chart behavior;
- revision.

No second frame model.

---

# Phase 10 — Dedicated Application Tests

Use only the exact Application test path authorized by the manifest amendment.

Prove the binding semantic mapping.

At minimum:

## Idempotency

- new canonical persistence → `NewlyPersisted`;
- equivalent existing canonical persistence → `EquivalentExisting`;
- binding-defined unavailable case → `Unavailable`;
- first persistence is not treated as “non-idempotent”;
- observation-level idempotency does not overwrite the selected scope.

## Data quality

- accepted validation → `Valid`;
- validation rejection → `Invalid`;
- unavailable case → `Unavailable`;
- WarmUp does not become Invalid merely due to feature unavailability;
- unrelated failure does not become Invalid.

Do not weaken predecessor assertions.

---

# Phase 11 — Dedicated Infrastructure Tests

Use only the exact Infrastructure test path authorized by the amendment.

Exercise the real production composition through:

pipeline/source evidence
→ Application result/evidence
→ WP04 envelope
→ Worker file serialization.

Prove:

- exact semantic values;
- exact JSON fields;
- state matrix;
- unchanged revision/state;
- unchanged atomic transport behavior.

At least one test must establish real `EquivalentExisting` production evidence if the binding definition requires that status to be production-reachable.

---

# Phase 12 — Dedicated Python Semantic-Exposure Tests

Use only the exact Python semantic-exposure test path authorized by the amendment.

Test:

- all three idempotency values;
- all three data-quality values;
- unavailable/backward behavior;
- invalid/unknown-value behavior according to accepted parser convention;
- direct `VisualizationFrame` propagation;
- no effect on existing WP06 fields;
- transport warning separation.

Do **not** use the reserved later WP07 presentation test path.

Do not assert WP07 labels/sections/rendering.

---

# Phase 13 — Real End-to-End Semantic Evidence

Run representative real production flows and inspect the actual handoff consumed by Python.

Prove the chain for every production-reachable semantic status required by the binding artifact.

At minimum report evidence for:

- `NewlyPersisted`;
- `EquivalentExisting`;
- `Valid`;
- `Unavailable`;
- `Invalid` if the accepted semantic definition identifies a real canonical validation-failure path reachable without changing production behavior.

If `Invalid` is legitimately only reachable through a focused canonical failure fixture, distinguish that from production composition evidence and ensure the binding definition permits it.

Never manufacture a production status.

---

# Phase 14 — Static Audit

Search/diff prove no forbidden semantic shortcuts.

Specifically search for accidental mapping from:

- revision equality → idempotency;
- cache hit → idempotency;
- transport retry → idempotency;
- observation duplicate replacement → presentation idempotency;
- generic pipeline success → data quality;
- feature WarmUp → Invalid;
- transport warning → backend quality/idempotency.

Also prove:

- no scores;
- no confidence;
- no thresholds;
- no provider/SQLite presentation access;
- no duplicate feature formula;
- no WP07 rendering.

Hard gate.

---

# Phase 15 — Focused Regression

Run every new dedicated test file plus all directly affected predecessor focused suites.

Report exact counts.

Must include:

- new Application semantic-exposure tests;
- new Infrastructure semantic-exposure tests;
- new Python semantic-exposure tests;
- WP04 focused read-model tests;
- WP05 .NET transport tests;
- WP05 Python 3/3;
- WP06 Python 6/6.

No skip/disable.

---

# Phase 16 — Governed Suites

Run definitive:

- Infrastructure;
- Application;
- Domain;
- Architecture.

Report exact passed/failed/skipped and exit codes.

Run repository-standard build.

Require 0 errors / 0 warnings.

---

# Phase 17 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Predecessor total: **305**.

The final total should increase only by the exact number of newly authorized .NET tests.

Report:

- predecessor total;
- new tests by project;
- expected new total;
- actual passed/failed/skipped;
- exit code.

Zero failures required.

---

# Phase 18 — Python / Dependency Guard

Run:

- new semantic-exposure Python tests;
- WP05 3/3;
- WP06 6/6;
- Python compilation;
- Streamlit 1.61.1 import smoke;
- `pip check`.

Require no new package/pin.

Require no Streamlit process residue.

---

# Phase 19 — Scope Audit

List every changed file.

For each prove:

- path is in the binding amendment;
- symbol is authorized;
- concern is semantic exposure only.

Then prove zero:

- schema changes;
- persistence behavior changes;
- validation behavior changes;
- provider changes;
- pipeline stage changes;
- Replay redesign;
- WP04 state/revision changes;
- WP05 retry/cache/lifecycle changes;
- WP06 chart semantic changes;
- WP07 rendering;
- reserved WP07 presentation-test mutation;
- WP08;
- WP09;
- package changes.

Any unexplained mutation blocks completion.

---

# Phase 20 — GitHub / Project Lifecycle

This authority does **not** complete #232.

Therefore:

- keep #232 Open / Backlog;
- do not mark Done;
- keep #233 Open / Backlog;
- do not start WP08;
- do not alter milestone closure state.

Only perform a GitHub evidence comment if the accepted governance explicitly requires it for predecessor exposure work. Otherwise GitHub mutations are zero.

---

# Acceptance Gate

Complete only if:

1. both semantic primitives match the binding definition exactly;
2. every mutation is authorized by the binding path amendment;
3. canonical source → Application → WP04 → JSON → WP05 → WP06 is proven;
4. WP04 state/revision semantics are unchanged;
5. WP05 transport/cache semantics are unchanged;
6. WP06 chart/frame predecessor semantics are unchanged except the authorized additive factual fields;
7. focused tests pass;
8. governed suites pass;
9. full regression passes;
10. Python guards pass;
11. scope audit passes;
12. no WP07 rendering exists;
13. #232 remains Open / Backlog.

---

# Required Completion Report

## Binding authorities
Name both accepted artifacts.

## Entry state
Git/lifecycle/predecessor evidence.

## Changed paths
Exact path + symbol + amendment clause.

## Semantic implementation
Exact source mappings for both primitives.

## Exposure chain
Application → WP04 → JSON → WP05 → WP06.

## State matrix
Ready / WarmUp / Empty / Failed / Stale evidence.

## Production evidence
Report each reachable semantic status.

## Focused tests
Exact counts.

## Governed regression
Infrastructure / Application / Domain / Architecture / build / full total.

## Python evidence
New tests / WP05 / WP06 / compile / Streamlit / pip.

## Scope proof
All forbidden categories zero.

## Lifecycle
#232 Open / Backlog; #233 Open / Backlog.

If no GitHub mutation:

`WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION GITHUB MUTATIONS: ZERO`

## Next step
On success state exactly:

`WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION COMPLETE — WP07 PRESENTATION CONTRACT/PATH DEFINITION MAY RESUME`

---

# Stop Conditions

Stop if:

- binding artifacts conflict;
- required path/symbol is absent from the amendment;
- implementation requires semantic reinterpretation;
- serializer versioning conflicts;
- schema/persistence/validation behavior must change;
- WP04 state/revision must change;
- WP05 transport/cache/lifecycle must change;
- WP06 chart semantics must change;
- WP07 rendering becomes necessary;
- reserved presentation test path is needed;
- WP08/WP09 is needed;
- build/regression/Python gates fail;
- scope audit fails.

Preserve only valid authorized partial work and report the minimum next authority.

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE BLOCKED`

Do not emit success unless the full real semantic-exposure chain is implemented and accepted while #232 remains Open / Backlog.

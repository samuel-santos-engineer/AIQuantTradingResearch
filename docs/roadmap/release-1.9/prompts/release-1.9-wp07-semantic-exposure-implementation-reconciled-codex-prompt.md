# Release 1.9 — WP07 Canonical Semantic-Exposure Implementation + Acceptance — Reconciled Fresh Authority

## Authority

Use **GPT-5.6 Terra**.

This is the fresh, reconciled implementation/acceptance authority for the canonical semantic-exposure predecessor work required by Release 1.9 WP07 (#232).

Three governance inputs are binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_SEMANTIC_EXPOSURE_MANIFEST_PATH_AUTHORITY_AMENDMENT.md`
3. The completed WP07 local-repository reconciliation / intersecting-diff classification whose terminal result is:
   `RELEASE 1.9 WP07 LOCAL-REPOSITORY RECONCILIATION AND INTERSECTING-DIFF CLASSIFICATION COMPLETE`

The semantic definition controls meaning.
The manifest amendment controls paths/symbols.
The reconciliation controls preservation of pre-existing local work.

If any binding inputs conflict, stop.

---

# Reconciliation Rule — Mandatory

Do **not** require a clean worktree.

The reconciliation established:

- all dirty changes intersecting authorized WP07 exposure paths are **Class A accepted predecessor work**;
- no Class B partial WP07 semantic-exposure work exists;
- no Class D ambiguity exists;
- all Class A hunks must be preserved exactly;
- unrelated local dirty state must remain untouched;
- the three dedicated semantic-exposure test paths are absent and may be created only at their exact authorized paths;
- reserved `python/presentation/test_realtime_financial_visualization_wp07.py` remains unused.

Do not reset, restore, checkout, stash, clean, revert, or overwrite predecessor work.

A dirty authorized file is not itself a blocker.

Modify such a file only by adding the exact Class B WP07 semantic-exposure changes permitted by the manifest amendment, while preserving its existing Class A behavior.

---

# Fixed Semantic Values

Implement exactly:

## `PresentationIdempotencyStatus`

- `NewlyPersisted`
- `EquivalentExisting`
- `Unavailable`

Use the exact scope/source/non-equivalences/state matrix from the semantic definition.

## `PresentationDataQualityStatus`

- `Valid`
- `Invalid`
- `Unavailable`

Use the exact validation scope/source/state matrix from the semantic definition.

No additional values or aliases.

---

# Objective

Implement and prove:

canonical source evidence
→ Application result/evidence
→ WP04 envelope
→ Worker JSON
→ WP05 Python parser
→ WP06 additive factual metadata.

This authority ends there.

Do **not** implement WP07 presentation sections/rendering.

#232 remains Open / Backlog on success.

---

# Phase 0 — Resolve Binding Inputs

Read both binding documentation artifacts completely.

Read the completed reconciliation result/evidence available in the repository/session.

Extract:

- exact canonical source mappings;
- exact state matrix;
- exact enum/type/property names;
- exact JSON/versioning conclusion;
- exact authorized production paths/symbols;
- exact three dedicated test paths;
- reserved later WP07 presentation path;
- exact Class A preservation rule.

Before mutation, produce an implementation map containing only authorized Class B additions.

---

# Phase 1 — Repository Snapshot

Record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked state.

Expected committed baseline:

`3a02f035a253e4e16f479e1866c9a5195f5cfbdb`

with local main/origin main 0/0, subject to fresh verification.

Do not treat reconciled Class A dirty state as failure.

Stop only if:

- new Class D ambiguity appears;
- intersecting dirty state differs materially from the completed reconciliation;
- a binding file changed unexpectedly.

---

# Phase 2 — Predecessor Gate

Run governed predecessor validation without modifying Class A work.

Require:

- build: 0 errors / 0 warnings;
- full .NET predecessor: 305/305 before new tests;
- WP05 Python: 3/3;
- WP06 Python: 6/6;
- Python compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

If test totals differ because additional accepted Class A tests are now present, reconcile the count from current repository evidence rather than deleting anything.

Do not “fix” predecessor tests under this authority.

---

# Phase 3 — Path Hard Gate

For every proposed mutation prove:

- exact path appears in the binding path amendment;
- exact symbol/concern is authorized;
- change is Class B semantic exposure only.

Only the exact three dedicated semantic-exposure test paths may be newly created.

Do not create helper files unless the binding amendment explicitly names them.

---

# Phase 4 — Application Semantic Exposure

Implement the exact semantic types/properties authorized by the amendment.

Map canonical source evidence exactly as fixed by the semantic definition.

Idempotency:

- new canonical pipeline-result persistence → `NewlyPersisted`;
- equivalent existing canonical pipeline-result persistence → `EquivalentExisting`;
- binding-defined no-applicable-result case → `Unavailable`.

Data quality:

- accepted canonical validation → `Valid`;
- canonical validation rejection → `Invalid`;
- binding-defined no-applicable-validation case → `Unavailable`.

Preserve all existing Class A Historical presentation-input and pipeline-composition behavior.

Do not modify persistence or validation behavior.

---

# Phase 5 — WP04 Envelope

Add only the two authorized factual semantic fields.

Apply the exact binding state matrix.

Preserve Class A:

- states;
- Historical/Replay revisions;
- 64-row window;
- snapshot identity/version;
- feature semantics;
- failure/stale behavior;
- atomic publication.

No state/revision reinterpretation.

---

# Phase 6 — Worker JSON

Serialize only the two new fields through the exact authorized publisher symbol.

Preserve:

- `aiq-visualization-read-model-v1` according to the binding versioning conclusion;
- canonical runtime path;
- startup cleanup;
- temp sibling behavior;
- atomic replacement;
- lifecycle.

No transport redesign.

---

# Phase 7 — WP05 Parser

Add only the authorized parser fields/value validation.

Preserve exact canonical enum values.

Do not derive either status.

Preserve Class A:

- two-read maximum;
- 50 ms retry;
- revision comparison;
- last-good cache;
- ProducerUnavailable;
- transport warning;
- contract-version behavior.

---

# Phase 8 — WP06 Additive Projection

Add only the two authorized immutable factual metadata fields to the existing frame/projection symbols.

No WP07 rendering.

Preserve Class A WP06 behavior exactly:

- price/time;
- latest;
- count/window/capacity;
- feature;
- pipelineSuccess;
- backend state;
- transport warning;
- revision;
- chart rendering.

---

# Phase 9 — Dedicated Test Files

Create only the exact three absent semantic-exposure test paths named by the binding manifest amendment.

They must remain separate from the reserved later WP07 presentation test.

## Application dedicated test

Prove canonical semantic mappings and non-equivalences.

## Infrastructure dedicated test

Prove real pipeline/Application → WP04 → JSON exposure.

## Python dedicated test

Prove parser → WP06 factual propagation and predecessor-frame preservation.

Do not repurpose WP04/WP05/WP06-exclusive tests except to run them as regression.

---

# Phase 10 — Required Idempotency Tests

Prove:

- new result → `NewlyPersisted`;
- equivalent existing → `EquivalentExisting`;
- unavailable → `Unavailable`;
- first persistence is not “non-idempotent”;
- observation persistence idempotency is not the selected presentation fact;
- revision equality does not determine it;
- cache/transport do not determine it.

---

# Phase 11 — Required Data-Quality Tests

Prove:

- canonical accepted validation → `Valid`;
- canonical validation rejection → `Invalid`;
- unavailable → `Unavailable`;
- WarmUp alone is not `Invalid`;
- unrelated pipeline failure is not `Invalid`;
- Stale behavior matches the binding definition;
- transport warnings do not alter it.

Do not invent a production validation failure if none is reachable under existing behavior.

Use the exact accepted testing rule from the semantic definition.

---

# Phase 12 — Real Production Composition

Prove actual:

source evidence
→ Application
→ WP04
→ JSON
→ Python
→ WP06.

Report production-reachable cases for:

- `NewlyPersisted`;
- `EquivalentExisting`;
- `Valid`;
- `Unavailable`;
- `Invalid` where genuinely reachable under the binding definition.

Distinguish focused fixture evidence from real production composition.

No fabricated status.

---

# Phase 13 — Static Audit

Search/diff prove zero forbidden shortcuts:

- revision → idempotency;
- cache → idempotency;
- retry → idempotency;
- observation duplicate → presentation idempotency;
- pipeline success → data quality;
- WarmUp → Invalid;
- transport warning → semantic status.

Also prove zero:

- score/confidence/threshold;
- SQLite/provider presentation access;
- duplicate feature computation;
- WP07 section/rendering implementation.

---

# Phase 14 — Focused Regression

Run:

- all three new dedicated semantic-exposure test files;
- affected Application tests;
- affected Infrastructure tests;
- WP04 focused read-model tests;
- WP05 .NET transport tests;
- WP05 Python 3/3;
- WP06 Python 6/6.

Report exact counts.

---

# Phase 15 — Governed Suites

Run definitive:

- Infrastructure;
- Application;
- Domain;
- Architecture.

Then build.

Require zero failures and 0 build warnings/errors.

---

# Phase 16 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Use 305 as the documented predecessor reference, but calculate expected current total from:

- preserved accepted Class A tests;
- exact newly added authorized semantic-exposure .NET tests.

Explain the delta.

Require 0 failed / 0 skipped unless existing governance explicitly permits skips.

---

# Phase 17 — Python Guard

Run:

- new semantic-exposure Python tests;
- WP05 3/3;
- WP06 6/6;
- compile;
- Streamlit 1.61.1 import smoke;
- `pip check`.

No new packages.

No Streamlit process residue.

---

# Phase 18 — Preservation Audit

This is a hard acceptance gate.

For each previously reconciled Class A intersecting path:

1. compare pre-implementation content/hunks with final content;
2. identify only the added Class B semantic-exposure changes;
3. prove no Class A behavior was deleted/reverted/rewritten.

List every changed path and separate:

- preserved Class A content;
- newly added Class B content.

Any unexplained alteration to Class A blocks completion.

---

# Phase 19 — Scope Audit

Prove every mutation is authorized.

Zero:

- schema;
- persistence behavior;
- validation behavior;
- provider;
- pipeline stage;
- Replay redesign;
- WP04 state/revision;
- WP05 transport/cache/lifecycle;
- WP06 chart semantics;
- WP07 rendering;
- reserved WP07 presentation test;
- WP08;
- WP09;
- package/pin.

---

# Phase 20 — Lifecycle

Do not close WP07.

Keep:

- #232 Open / Backlog;
- #233 Open / Backlog;
- milestone open;
- WP08/WP09 unstarted.

GitHub mutations should be zero unless existing governance explicitly requires a narrow evidence comment.

---

# Acceptance Gate

Success requires:

1. binding semantics implemented exactly;
2. binding paths obeyed exactly;
3. reconciliation preservation rule obeyed;
4. Class A work preserved;
5. semantic exposure proven end-to-end;
6. three dedicated test surfaces added and passing;
7. predecessor regressions passing;
8. no WP07 rendering;
9. #232 remains Open / Backlog.

---

# Required Completion Report

## Binding authorities
Name semantic definition, path amendment, and reconciliation result.

## Entry state
Git/worktree and predecessor evidence.

## Changed paths
Exact path/symbol and Class B addition.

## Class A preservation
Explicit proof for each intersecting predecessor file touched.

## Semantic mappings
Exact idempotency and data-quality mapping.

## Exposure chain
Application → WP04 → JSON → WP05 → WP06.

## Production evidence
Exact reachable cases.

## Tests
Three dedicated test counts plus predecessor focused suites.

## Governed validation
Infrastructure/Application/Domain/Architecture/build/full regression.

## Python validation
New semantic exposure/WP05/WP06/compile/Streamlit/pip.

## Scope audit
Forbidden categories zero.

## Lifecycle
#232/#233 unchanged.

If GitHub unchanged:

`WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION GITHUB MUTATIONS: ZERO`

On success state exactly:

`WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION COMPLETE — WP07 PRESENTATION CONTRACT/PATH DEFINITION MAY RESUME`

---

# Stop Conditions

Stop if:

- reconciliation no longer matches current dirty state;
- a Class A hunk would need deletion/rewrite;
- any required path/symbol is unauthorized;
- semantics require reinterpretation;
- schema/persistence/validation behavior must change;
- WP04/WP05/WP06 fixed semantics must change;
- WP07 rendering becomes necessary;
- reserved presentation test path is needed;
- WP08/WP09 is needed;
- validation/regression fails;
- preservation/scope audit fails.

Do not clean the worktree.

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CANONICAL SEMANTIC-EXPOSURE IMPLEMENTATION AND ACCEPTANCE BLOCKED`

Do not emit success unless the semantic exposure is complete and all reconciled Class A predecessor work remains preserved.

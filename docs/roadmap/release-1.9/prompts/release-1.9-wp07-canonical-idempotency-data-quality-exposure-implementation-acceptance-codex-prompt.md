# Release 1.9 — WP07 Canonical Idempotency/Data-Quality Exposure Implementation + Acceptance Authority

## Authority

This is a **narrow predecessor-exposure implementation and acceptance authority** supporting Release 1.9 WP07, canonical issue **#232**.

Use **GPT-5.6 Terra**.

The following artifact is **binding semantic authority**:

`docs/roadmap/release-1.9/RELEASE_1.9_WP07_CANONICAL_IDEMPOTENCY_DATA_QUALITY_SEMANTIC_DEFINITION.md`

Implement only the canonical semantic exposure defined there:

canonical source evidence
→ Application result/evidence
→ WP04 envelope
→ Worker JSON
→ WP05 Python parser
→ narrowly additive WP06 factual metadata.

Do **not** implement WP07 sections/rendering in this authority.

Do not redefine the semantic primitive values, scope, state matrix, non-equivalences, or versioning rules fixed by the binding definition.

---

# Binding Semantic Primitives

## `PresentationIdempotencyStatus`

Exact domain:

- `NewlyPersisted`
- `EquivalentExisting`
- `Unavailable`

Use the exact scope/source/state behavior/non-equivalences fixed by the binding definition.

Do not reinterpret observation persistence idempotency, revision equality, Replay duplicate handling, cache behavior, transport retries, or pipeline success as this fact.

## `PresentationDataQualityStatus`

Exact domain:

- `Valid`
- `Invalid`
- `Unavailable`

Use the exact validation scope/source/state behavior fixed by the binding definition.

Do not introduce scores, thresholds, confidence, severity, freshness, profitability, or subjective quality labels.

---

# Entry State

Expected:

- WP01–WP06: Closed / Done.
- #231: Closed / Done.
- #232: Open / Backlog.
- #233: Open / Backlog.
- WP08/WP09 unstarted.
- Schema: v4.
- Full .NET predecessor: **305/305**.
- WP06 Python: **6/6**.
- WP05 Python: **3/3**.
- Build: **0 errors / 0 warnings**.
- Canonical semantic definition artifact exists and is the only new accepted definition artifact from the immediately preceding pass.

No prior WP07 production implementation should exist.

---

# Objective

Implement and prove the smallest additive path that exposes the two canonical facts to the existing Python presentation boundary.

Success means:

1. canonical source operations determine the two statuses exactly as defined;
2. Application exposes immutable status facts;
3. WP04 envelope carries them without changing existing state/revision semantics;
4. Worker JSON serializes them under the binding versioning contract;
5. WP05 Python parser preserves them exactly;
6. WP06 `VisualizationFrame` may expose them as narrowly additive immutable factual metadata if the binding definition requires it;
7. all predecessor behavior remains compatible;
8. no WP07 UI section/rendering work is performed.

---

# Phase 0 — Binding-Authority Resolution

Before mutation:

1. Read the entire binding semantic definition.
2. Extract verbatim into the execution notes:
   - exact idempotency scope/source;
   - exact data-quality scope/source;
   - state matrix;
   - exact additive exposure chain;
   - JSON/versioning conclusion;
   - required future tests;
   - any exact proposed type/property names;
   - any path constraints.
3. Read #232 only to confirm this remains predecessor exposure for WP07.
4. Read current WP04/WP05/WP06 implementation and tests.
5. Read the canonical persistence and validation source types named by the binding definition.

If repository reality conflicts with the accepted semantic definition, stop and report the exact conflict. Do not reinterpret the definition.

---

# Phase 1 — Git / Lifecycle / Predecessor Gate

Record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/tracked/relevant untracked changes.

Verify:

- #231 Closed / Done;
- #232 Open / Backlog;
- #233 Open / Backlog;
- no WP08/WP09 work;
- no unauthorized partial WP07 presentation implementation.

Before mutation run:

## .NET
- repository-standard build;
- full regression:
  `dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Expected: **305/305**, 0 failed.

## Python
- WP06: **6/6**;
- WP05: **3/3**;
- compile/import checks;
- Streamlit 1.61.1 smoke;
- `pip check`.

If predecessor evidence is not clean, stop for reconciliation.

---

# Phase 2 — Exact Implementation Map

Before coding, identify every required symbol/path from the binding semantic definition and current repository.

Classify each proposed change:

1. canonical source wiring;
2. Application semantic projection;
3. WP04 envelope exposure;
4. Worker JSON serialization;
5. WP05 Python parser;
6. WP06 additive metadata;
7. focused acceptance tests.

For every path record:

- exact path;
- exact symbol;
- why required;
- whether existing manifest/path authority permits it.

### Manifest hard gate

If any required implementation/test path is not already authorized by the accepted Release 1.9 manifest or an accepted amendment:

**STOP BEFORE MUTATION.**

Report the exact missing paths and request a narrow **WP07 canonical semantic-exposure manifest/path-authority amendment**.

Do not treat this implementation authority as implicit broad path permission.

---

# Phase 3 — Canonical Source Wiring

Implement only the mapping from the already-existing governed source outcomes fixed by the binding definition.

Rules:

- do not change persistence behavior;
- do not change validation behavior;
- do not add persistence operations;
- do not add validators;
- do not change ordering;
- do not change transaction semantics;
- do not change schema;
- do not change source authority.

The new semantic status is a projection of canonical evidence, not a new behavior.

---

# Phase 4 — Application Semantic Types

Add the minimum immutable semantic types/properties fixed by the binding definition.

Requirements:

- exact three-value domains;
- no arbitrary strings;
- no UI labels in core Application contracts;
- immutable;
- deterministic;
- optional/tagged only where the binding definition requires availability semantics.

Existing callers must remain source-compatible wherever the definition requires additive compatibility.

No broad refactor.

---

# Phase 5 — Application Result/Evidence Exposure

Expose the two facts at the exact `PipelineExecutionResult` / `PipelineExecutionEvidence` location fixed by the definition.

Preserve:

- existing fields;
- existing pipeline stage count/order;
- existing failure semantics;
- Historical/Replay behavior;
- canonical feature output.

Do not make presentation semantics a sixth pipeline stage.

---

# Phase 6 — WP04 Envelope Exposure

Add only the two canonical factual fields required by the binding definition.

Preserve exactly:

- `aiq-visualization-read-model-v1` versioning conclusion from the binding artifact;
- Ready / Empty / WarmUp / Failed / Stale;
- 64-row bound;
- revision kinds;
- snapshot identity/version;
- failure retention;
- stale semantics;
- immutable publication.

Do not change state because of these metadata facts.

State matrix must match the binding definition exactly.

---

# Phase 7 — Worker JSON Serialization

Serialize the fields exactly under the binding versioning rules.

Requirements:

- stable enum/string representation;
- no arbitrary metadata dictionary;
- no raw persistence objects;
- no exception text;
- no provider data;
- no schema/persistence changes.

If the binding artifact says additive v1 fields are allowed, preserve v1 exactly.

If actual serializer constraints contradict that conclusion, stop rather than silently versioning the contract.

---

# Phase 8 — WP05 Python Parser

Parse the two fields directly.

Requirements:

- preserve exact canonical values;
- reject invalid unknown values according to the accepted parser convention;
- preserve backward/unavailable behavior exactly as defined;
- no derivation from `pipelineSuccess`;
- no derivation from revision;
- no derivation from snapshot identity;
- no cache/retry changes;
- no transport warning reinterpretation.

WP05 remains a transport consumer, not semantic owner.

---

# Phase 9 — WP06 Additive Metadata

Only if required by the binding definition, add the two immutable factual statuses to `VisualizationFrame`.

Rules:

- additive only;
- price/time unchanged;
- latest unchanged;
- count/window/capacity unchanged;
- revision unchanged;
- feature fields unchanged;
- `pipelineSuccess` unchanged;
- backend state unchanged;
- transport warning unchanged;
- chart behavior unchanged.

Do not render new WP07 sections here.

Do not change existing WP06 test expectations except to add compatibility assertions where authorized.

---

# Phase 10 — Focused .NET Tests

Add focused tests only in authorized paths.

Required coverage must include, as applicable to the binding definition:

## Idempotency

- canonical new-result source → `NewlyPersisted`;
- canonical equivalent-existing source → `EquivalentExisting`;
- unavailable source/state → `Unavailable`;
- first successful persistence is not represented as “non-idempotent”;
- observation persistence idempotency does not override the selected pipeline scope;
- revision/cache/transport do not determine status.

## Data Quality

- accepted canonical validation → `Valid`;
- canonical validation rejection → `Invalid`;
- unavailable state → `Unavailable`;
- unrelated failure does not become `Invalid`;
- WarmUp is not automatically `Invalid`;
- Stale retains last-good value where defined;
- transport conditions do not change it.

## Envelope

- exact state matrix;
- exact JSON serialization;
- immutable/additive compatibility;
- revision/state unchanged.

---

# Phase 11 — Focused Python Tests

Add focused parser/frame tests only in authorized paths.

Prove:

- exact parsing of all three idempotency values;
- exact parsing of all three data-quality values;
- unavailable/backward behavior;
- invalid value rejection if governed;
- direct propagation to frame;
- no effect on price/time/latest/count/window;
- no effect on transport warning;
- no WP07 rendering.

---

# Phase 12 — Production Composition Evidence

Prove the actual production chain for representative cases:

canonical source
→ pipeline execution
→ WP04 envelope
→ serialized handoff JSON
→ Python parser
→ WP06 frame metadata.

At minimum prove:

- one `NewlyPersisted` case;
- one `EquivalentExisting` case;
- one `Valid` case;
- one `Invalid` or binding-defined canonical validation-failure case;
- one `Unavailable` case where required.

If a status cannot be reached through the real production path, stop. Do not fabricate acceptance fixtures as the sole proof.

---

# Phase 13 — Static Semantic Audit

Search/diff prove:

- no idempotency inference from revision/cache/transport;
- no observation-idempotency conflation unless binding definition selected it;
- no data-quality inference from generic pipeline success;
- no scores/thresholds/confidence;
- no duplicate validation implementation;
- no feature recomputation;
- no SQLite/provider access added to Python;
- no WP07 UI section/rendering;
- no WP08/WP09 work.

Hard gate.

---

# Phase 14 — Focused Regression

Run all newly affected focused suites.

Report exact counts for:

- canonical persistence/idempotency tests;
- validation/data-quality tests;
- WP04 read-model tests;
- WP05 .NET transport tests;
- new Python parser/frame tests;
- WP06 6/6;
- WP05 Python 3/3.

No test weakening/skipping.

---

# Phase 15 — Governed .NET Suites

Run definitive:

- Infrastructure;
- Application;
- Domain;
- Architecture.

Report exact counts and exit codes.

Then run repository-standard build.

Require:

- 0 errors;
- 0 warnings unless a pre-existing governed warning is proven.

---

# Phase 16 — Full Regression

Run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Predecessor baseline: **305/305**.

The total may increase only because of authorized new tests.

Report:

- passed;
- failed;
- skipped;
- total;
- exit code;
- exact explanation for count increase.

Require zero failures.

---

# Phase 17 — Python / Streamlit Guard

Run:

- all affected Python tests;
- WP05 3/3;
- WP06 6/6;
- compilation;
- Streamlit 1.61.1 import smoke;
- `pip check`.

No Streamlit process residue.

---

# Phase 18 — Final Scope Audit

For every changed file report:

- exact path;
- exact authority category;
- exact symbols changed;
- why required.

Prove zero:

- schema change;
- persistence behavior change;
- provider change;
- Replay semantic change;
- WP04 state/revision change;
- WP05 retry/cache change;
- WP06 chart semantic change;
- WP07 rendering;
- WP08;
- WP09;
- package/pin change;
- new transport.

Any unexplained path blocks acceptance.

---

# GitHub Lifecycle

This authority does **not** complete WP07.

Therefore:

- #232 remains Open / Backlog;
- do not close #232;
- do not mark #232 Done;
- #233 remains Open / Backlog;
- do not start WP08.

A narrow evidence comment is permitted only if established repository governance requires documenting predecessor implementation evidence. Otherwise GitHub mutations are zero.

---

# Success State

On success:

- the two canonical semantic facts are implemented end-to-end through the WP06 factual projection;
- no WP07 rendering exists;
- all compatibility/regression gates pass;
- #232 remains Open / Backlog;
- the next step is to resume the WP07 feature/data-quality presentation contract/path definition using these now-real canonical inputs.

State exactly:

`WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY EXPOSURE COMPLETE — WP07 PRESENTATION DEFINITION MAY RESUME`

---

# Stop Conditions

Stop if:

- binding semantic artifact cannot be implemented without reinterpretation;
- required path lacks manifest authority;
- serializer versioning conflicts with binding definition;
- schema/persistence behavior must change;
- new validation behavior is required;
- WP04 state/revision must change;
- WP05 transport/cache semantics must change;
- WP06 chart semantics must change;
- a production status cannot be truthfully produced;
- WP07 rendering becomes necessary;
- WP08/WP09 path is required;
- regression/build/Python gates fail;
- scope audit fails.

On blocker preserve valid partial implementation only if authorized and report the minimum next authority.

---

# Required Completion Report

## Binding semantics
Report exact values/scopes from the accepted definition.

## Entry evidence
Git/lifecycle + predecessor validation.

## Implementation map
Every changed path/symbol.

## Exposure implementation
Canonical source → Application → WP04 → JSON → WP05 → WP06.

## State matrix evidence
Ready/WarmUp/Empty/Failed/Stale.

## Production composition
Report real cases and observed statuses.

## Test evidence
Focused .NET/Python counts.

## Regression
Infrastructure/Application/Domain/Architecture/build/full regression.

## Python guards
WP05/WP06/compile/Streamlit/pip.

## Scope audit
Prove all forbidden categories untouched.

## Lifecycle
State #232 Open / Backlog and #233 Open / Backlog.

## Mutation statement
If no GitHub mutation:

`WP07 CANONICAL EXPOSURE GITHUB MUTATIONS: ZERO`

## Next step
On success:

`WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY EXPOSURE COMPLETE — WP07 PRESENTATION DEFINITION MAY RESUME`

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY EXPOSURE IMPLEMENTATION AND ACCEPTANCE COMPLETE`

Blocked:

`RELEASE 1.9 WP07 CANONICAL IDEMPOTENCY/DATA-QUALITY EXPOSURE IMPLEMENTATION AND ACCEPTANCE BLOCKED`

Do not emit success unless both canonical facts are proven through the real Application → WP04 → JSON → WP05 → WP06 production chain with all predecessor semantics preserved.

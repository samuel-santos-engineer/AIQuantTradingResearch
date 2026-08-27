# Release 1.9 — WP03 Replay Dataset-Boundary Definition — Codex Authority

## Authority

This document grants a **very narrow normative definition authority** for Release 1.9 WP03, canonical issue **#228**.

WP03 implementation is blocked before mutation because the fixed Worker replay configuration contract does not define how Replay mode supplies the existing pipeline's required `DatasetDefinition`.

Known required `DatasetDefinition` inputs:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`

Known fixed Worker Replay inputs:

- `Worker:Replay:ReplayIdentity`
- `Worker:Replay:Target`
- `Worker:Replay:StartingTick`
- `Worker:Replay:RequestedObservationCount`

The unresolved normative questions are:

1. Does Replay mode still require the existing `Dataset` configuration section?
2. Must `Dataset:Target` equal `Worker:Replay:Target`?
3. How are `Dataset:From` and `Dataset:To` determined in Replay mode?
4. Are those dates validation-only pipeline context, or derived from replay fixture/observations?
5. What happens when Dataset boundaries are missing, malformed, inconsistent, or incompatible with replay bounds?

This authority exists only to define those semantics.

It does **not** authorize implementation.

It does **not** authorize configuration-file changes.

It does **not** authorize GitHub lifecycle mutation.

It does **not** authorize WP04.

---

# Objective

Produce one unambiguous normative Replay-mode Dataset-boundary contract that a later implementation authority can apply without inventing semantics.

The definition must specify exactly:

- whether `Dataset` remains required in Replay mode;
- exact ownership/source of `Dataset:Target`;
- exact ownership/source of `Dataset:From`;
- exact ownership/source of `Dataset:To`;
- target consistency rule;
- temporal-bound consistency rule;
- replay tick/bound interaction;
- validation timing;
- failure behavior;
- historical-mode compatibility;
- which existing contracts remain unchanged.

---

# Fixed Context

The following are already settled and must not be reopened:

## Worker mode/configuration

- `Worker:Mode`
- values: `Historical`, `Replay`
- missing mode defaults to `Historical`
- Replay settings:
  - `ReplayIdentity`
  - `Target`
  - `StartingTick`
  - `RequestedObservationCount`

## Pipeline architecture

Historical:
`Worker → historical materialization → IHistoricalObservationStore → ExecuteCanonical → stages 1–5`

Replay:
`Worker → Replay config → WP02 replay source → WP03 observation-input seam → ExecuteCanonical → stages 1–5`

## Existing pipeline contract

The canonical materialization path still requires a `DatasetDefinition` including:

- target;
- from;
- to.

Do not redesign `DatasetDefinition` under this authority.

---

# Permitted Scope

This authority may:

- read #228;
- read Release 1.9 WP03 definition/manifest;
- read `DatasetDefinition`;
- read Dataset configuration binding/validation;
- read historical materialization semantics;
- read WP02 replay fixtures/contracts;
- read WP03 seam/executor;
- inspect tests that reveal Dataset target/from/to semantics;
- define the Replay-mode Dataset-boundary contract.

If governance permits one WP03-owned definition artifact, create only that artifact.

Otherwise return the normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify Worker code;
- modify Application code;
- modify Infrastructure code;
- modify tests;
- modify appsettings/configuration files;
- add new configuration keys;
- remove existing Dataset keys;
- change `DatasetDefinition`;
- change WP02 replay contracts;
- change schema;
- change package pins;
- change Python;
- change Streamlit;
- change JSON-over-stdio boundary;
- modify GitHub;
- close #228;
- start WP04.

This is definition-only authority.

---

# Decision Questions

The definition must answer all of these explicitly.

## 1. Replay-mode Dataset requirement

Choose exactly one:

### Option A — Existing Dataset section remains required

Replay mode still binds the existing Dataset section.

### Option B — DatasetDefinition is derived for Replay mode

Replay mode derives all DatasetDefinition fields from replay-owned data.

### Option C — Hybrid

Some Dataset fields remain configured and others are derived.

Select one only if clearly justified by #228 and repository semantics.

Do not choose based on convenience.

---

## 2. Target ownership and consistency

Define whether:

- `Dataset:Target` is authoritative;
- `Worker:Replay:Target` is authoritative;
- both are required and must match;
- one is derived from the other.

If both exist, define exact comparison semantics:

- case sensitivity;
- normalization, if any;
- mismatch behavior.

Prefer one semantic source of truth where safely compatible.

---

## 3. Dataset From/To ownership

Define exactly how `Dataset:From` and `Dataset:To` are obtained in Replay mode.

Potential categories, to evaluate but not assume:

- existing Dataset configuration;
- replay fixture metadata;
- first/last selected replay observation timestamps;
- full fixture bounds;
- requested replay slice bounds;
- other existing governed metadata.

For each selected source, explain why it matches current pipeline semantics.

---

## 4. Tick-to-time relationship

If replay logical ticks map to timestamped observations, define whether:

- StartingTick/RequestedObservationCount merely select observations while Dataset From/To remain independent context; or
- Dataset From/To must correspond exactly to the selected replay slice; or
- Dataset From/To constrain which replay observations are valid.

Do not conflate logical tick indices with timestamps without repository evidence.

---

## 5. Boundary validation

Define exact validation for Replay mode:

- missing Dataset section;
- missing Target;
- missing From;
- missing To;
- From > To;
- replay target mismatch;
- replay observations outside Dataset bounds;
- selected replay range partially outside Dataset bounds;
- empty replay slice;
- replay end-of-stream before requested count.

Only define rules required by existing contracts/#228.

Do not invent new product semantics.

---

## 6. Historical compatibility

State explicitly that Historical mode retains its existing Dataset behavior unchanged.

Any Replay-specific rule must not alter Historical semantics.

---

# Phase 0 — Read Existing Semantics

Before defining anything:

1. Read #228.
2. Read WP03 manifest/definition.
3. Read `DatasetDefinition`.
4. Read Dataset configuration binding and validation.
5. Read historical materialization tests.
6. Read WP02 replay fixture/request/result semantics.
7. Read WP03 seam/executor.
8. Identify whether pipeline stages actually consume Target/From/To and how.

Do not mutate anything.

---

# Phase 1 — Semantic Role Analysis

For each Dataset field, document its actual role:

## Target
- used for lookup?
- identity?
- validation?
- output labeling?
- stage behavior?

## From
- historical acquisition bound?
- validation?
- labeling?
- stage computation?

## To
- historical acquisition bound?
- validation?
- labeling?
- stage computation?

This role analysis must drive the Replay definition.

If a field is only historically acquisition-specific, determine whether Replay still needs a value for canonical pipeline semantics or only for contract compatibility.

---

# Phase 2 — Candidate Contract Evaluation

Evaluate the minimum viable contracts.

At minimum compare:

### Candidate 1
Replay requires existing Dataset section unchanged.

### Candidate 2
Replay requires Dataset target but derives From/To from selected replay observations.

### Candidate 3
Replay derives all DatasetDefinition fields from replay selection/fixture.

### Candidate 4
Another repository-supported hybrid, if evidence requires it.

For each candidate assess:

- #228 fit;
- backward compatibility;
- duplication of source-of-truth;
- determinism;
- ease of validation;
- whether it changes existing Dataset semantics;
- whether it requires implementation beyond WP03;
- whether it risks WP04+ scope.

Select the narrowest contract clearly supported by evidence.

### Hard stop

If two materially different candidates remain equally valid after inspection, stop and report the unresolved normative choice instead of guessing.

---

# Phase 3 — Define Normative Contract

Produce one exact contract covering:

## Dataset section requirement
Required / optional / derived.

## Target
Source of truth and consistency rule.

## From
Source and meaning.

## To
Source and meaning.

## Replay slice relation
How StartingTick and RequestedObservationCount interact with From/To.

## Validation
Exact fail/pass rules.

## Error timing
Startup validation vs execution-time validation.

## Historical compatibility
Explicit unchanged behavior.

## Non-goals
Explicitly state what is not being redefined.

Every normative choice must include a concise rationale grounded in repository semantics or #228.

---

# Phase 4 — Future Implementation Test Contract

Define the minimum tests the later implementation authority must add.

At minimum, where applicable:

- Replay with valid Dataset boundaries;
- Dataset target matches replay target;
- target mismatch fails;
- missing required Dataset values fail;
- invalid From/To fails;
- replay slice inside Dataset bounds passes;
- replay slice outside bounds fails if the chosen contract requires it;
- end-of-replay behavior remains WP02-owned;
- Historical mode behavior is unchanged.

Do not implement tests here.

---

# Stop Conditions

Stop if:

- #228 cannot be read;
- Dataset field semantics cannot be determined;
- replay fixture timing metadata is insufficient for any derived-bound design;
- a candidate would require changing `DatasetDefinition`;
- a candidate would require schema/protocol changes;
- a choice materially affects WP04+ behavior;
- two materially different minimal contracts remain equally valid.

On stop:

- make zero production changes;
- make zero GitHub changes;
- report the exact unresolved choice;
- identify the minimum additional authority required.

---

# Success Criteria

This definition authority succeeds only when one complete Replay Dataset-boundary contract is established that specifies:

- Dataset section requirement;
- Dataset target source-of-truth;
- target equality/mismatch semantics;
- From source;
- To source;
- tick/time relationship;
- replay slice/bounds rule;
- missing/invalid behavior;
- validation timing;
- historical compatibility;
- future implementation tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

WP04 remains unstarted.

---

# Required Completion Report

Return:

## Normative Replay Dataset-boundary contract

### Dataset requirement
State whether Replay requires/configures/derives DatasetDefinition.

### Target
- source of truth;
- match rule;
- mismatch behavior.

### From
- source;
- semantic meaning;
- validation.

### To
- source;
- semantic meaning;
- validation.

### Replay slice relation
- StartingTick interaction;
- RequestedObservationCount interaction;
- relation to Dataset bounds.

### Validation/failure
Exact rules for missing, malformed, inconsistent, and out-of-range cases.

### Historical compatibility
Explicit unchanged behavior.

### Required future tests
List exact scenarios.

### Non-goals
List what this authority does not redefine.

## Mutation proof

If no artifact is authorized/created:

`WP03 REPLAY DATASET-BOUNDARY DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP03 REPLAY DATASET-BOUNDARY CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement it here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP03 REPLAY DATASET-BOUNDARY DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP03 REPLAY DATASET-BOUNDARY DEFINITION BLOCKED`

Emit success only if the contract is unambiguous.

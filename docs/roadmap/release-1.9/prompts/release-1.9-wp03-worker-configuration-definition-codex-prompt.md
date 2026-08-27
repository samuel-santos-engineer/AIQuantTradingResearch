# Release 1.9 — WP03 Worker Configuration Definition — Codex Authority

## Authority

This document grants a **narrow specification-definition authority** for Release 1.9 WP03, canonical GitHub issue **#228**.

WP03 implementation remains blocked because the accepted Release 1.9 artifacts and repository do not define the Worker replay configuration contract needed to complete runtime composition.

Proven missing semantics:

- configuration key/path;
- supported mode value(s);
- default mode;
- replay configuration names;
- replay binding semantics;
- required versus optional replay fields;
- invalid mode behavior;
- missing configuration behavior;
- invalid replay configuration behavior.

The repository has no existing Worker mode convention from which these semantics can be derived safely.

No Worker implementation files were modified during the blocked attempt.

No GitHub lifecycle mutation occurred.

The validated WP03 canonical executor/observation seam and completed WP02 replay work remain intact.

WP04 has not started.

This authority exists only to define the missing **normative Worker configuration contract** required by #228.

It does **not** authorize implementation.

It does **not** authorize GitHub lifecycle completion.

It does **not** authorize WP04.

---

# Objective

Produce one explicit, minimal, internally coherent Worker replay configuration specification that is sufficient for a later WP03 implementation authority to complete #228 without inventing semantics.

The specification must define:

1. the exact configuration section/path;
2. the exact supported mode value(s);
3. the default mode and backward-compatibility rule;
4. replay option names;
5. replay option types;
6. required versus optional replay fields;
7. binding semantics;
8. validation semantics;
9. unknown-mode behavior;
10. missing-mode behavior;
11. missing replay-setting behavior;
12. invalid replay-setting behavior;
13. mapping from Worker configuration into the existing WP02 replay configuration/contracts;
14. finite replay completion behavior at the Worker configuration/composition boundary;
15. cancellation-related configuration semantics, if any are actually required;
16. explicit non-goals and compatibility constraints.

The result must be specific enough that a later implementation authority can say:

> Implement exactly this Worker configuration contract.

---

# Fixed Architectural Context

The following architecture is already accepted and must not be reopened:

## Historical path

`Worker`
→ historical materialization
→ `IHistoricalObservationStore`
→ WP03 materialization
→ `ExecuteCanonical`
→ canonical five-stage pipeline

## Replay path

`Worker explicit replay mode/config`
→ WP02 replay source/configuration
→ WP03 explicit-observation seam
→ `ExecuteCanonical`
→ same canonical five-stage pipeline
→ finite replay completion

The configuration specification must fit this architecture.

Do not redesign the replay-to-pipeline boundary.

---

# Existing Proven Technical Baseline

Preserve as context:

- WP02 replay contract and Infrastructure implementation are complete;
- WP03 canonical `ExecuteCanonical` extraction is implemented;
- WP03 additive observation-input seam is implemented;
- historical acquisition remains through `IHistoricalObservationStore`;
- replay does not masquerade as historical storage;
- Application focused tests: 122/122 passed;
- WP02 replay suite: 142/142 passed;
- full regression: 288/288 passed;
- build: 0 errors / 0 warnings;
- #228 remains Open / Backlog;
- WP04 has not started.

Do not modify any of this under this authority.

---

# Scope

## Permitted

This authority may:

- read #228;
- read Release 1.9 authority/definition/manifest files;
- read Worker configuration/composition code;
- read WP02 replay configuration/contracts;
- read WP03 seam/executor code;
- read tests and repository conventions;
- compare naming/configuration patterns already used elsewhere in the repository;
- define the missing Worker configuration contract;
- create or update **one WP03-owned configuration-definition artifact** if #228/Release 1.9 governance permits a local planning/spec artifact;
- otherwise return the full normative definition in the completion report.

## Forbidden

Do not:

- modify Worker production code;
- modify Application or Infrastructure production code;
- modify tests;
- modify package pins;
- modify Python version;
- modify Streamlit version;
- modify SQLite schema;
- modify JSON-over-stdio protocol;
- modify DI registration;
- modify appsettings or runtime configuration files;
- implement mode dispatch;
- create new Project items/issues;
- close #228;
- change #228 Project Status;
- alter dependency edges;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- start WP04;
- broaden the configuration system into a generalized mode framework.

This is definition-only authority.

---

# Design Principles

The normative configuration contract must satisfy all of the following.

## Minimality

Define only what #228 needs.

Do not add speculative future modes or fields.

## Explicitness

Mode selection and replay settings must be explicit enough to bind, validate, test, and reason about deterministically.

## Backward compatibility

Existing historical Worker execution must remain valid under the chosen default/missing-mode semantics.

If historical mode is the backward-compatible default, state that explicitly.

If the accepted Release 1.9 evidence requires explicit mode selection with no default, state that instead.

Do not choose silently.

## Existing-convention preference

Prefer established repository conventions for:

- configuration section naming;
- options binding;
- enum/string values;
- validation;
- failure style;
- case sensitivity;
- default values.

If repository conventions are inconsistent or absent, make a narrow normative choice and document the rationale.

## Direct mapping

Replay configuration should map as directly as practical onto already-completed WP02 replay contracts/configuration.

Avoid duplicate configuration concepts with different names unless a Worker-specific distinction is required.

## Fail-fast invalid configuration

Invalid replay mode/configuration should fail deterministically before ambiguous runtime behavior occurs.

The exact failure behavior must be defined.

---

# Phase 0 — Read Authority and Existing Conventions

Read:

1. canonical issue #228;
2. the accepted Release 1.9 WP03 manifest/definition;
3. Worker entry point;
4. Worker composition root;
5. all Worker configuration/options classes;
6. appsettings/configuration files used by Worker;
7. existing configuration validation patterns;
8. existing mode-like or enum-like configuration patterns anywhere relevant in the solution;
9. completed WP02 replay configuration/contracts;
10. WP03 observation-input seam and `ExecuteCanonical`.

Record all relevant naming and validation conventions.

Do not mutate anything.

---

# Phase 1 — Define Configuration Section/Path

Choose and define exactly one canonical Worker configuration path for execution mode and replay settings.

The definition must specify:

- exact section/path syntax;
- exact casing;
- nesting;
- whether replay settings are nested under the same Worker section or a dedicated replay subsection;
- whether environment-variable overrides follow standard .NET double-underscore mapping;
- whether command-line overrides use normal .NET configuration semantics, if applicable.

Prefer the smallest structure consistent with existing Worker configuration organization.

Do not create duplicate aliases unless repository conventions require them.

---

# Phase 2 — Define Mode Contract

Define:

- exact configuration key for mode;
- exact accepted value for historical mode;
- exact accepted value for replay mode;
- whether matching is case-sensitive or case-insensitive;
- behavior for unknown values;
- behavior when the key is absent;
- default mode, if any.

The specification should generally prefer one of these patterns, based on repository evidence:

### Pattern A — Backward-compatible default

Missing mode means historical behavior.

Unknown explicit mode is invalid.

### Pattern B — Fully explicit

Mode key is mandatory.

Missing or unknown mode is invalid.

Choose one and justify it from #228 plus repository compatibility needs.

Do not define more than historical/replay unless #228 explicitly requires another mode.

---

# Phase 3 — Define Replay Configuration Shape

Read the completed WP02 replay configuration and define the Worker-facing replay settings required to construct/map to it.

For each field define:

- exact key/name;
- type;
- required/optional;
- default, if any;
- valid range/domain;
- mapping target in WP02;
- validation rule;
- error behavior.

Only include fields actually required by #228/WP02 runtime composition.

Potential semantic categories may include, where required by the existing WP02 contract:

- replay identity;
- source/input identity;
- start/restart position or logical tick;
- requested/bounded observation count;
- duplicate behavior if configurable;
- finite replay boundary;
- other existing WP02 replay options.

Do not invent a configuration field for a semantic already fixed by WP02 behavior and not meant to be user-configurable.

---

# Phase 4 — Define Binding Semantics

Specify:

- which Worker options/configuration type owns the mode;
- which type owns replay settings;
- whether nested options are bound separately or together;
- whether validation occurs:
  - at startup;
  - on options construction;
  - on execution dispatch;
- whether invalid replay settings are validated only when replay mode is selected or always.

Prefer:

- historical mode not failing because replay-only settings are absent;
- replay mode failing before execution if required replay settings are absent/invalid.

State this explicitly if selected.

---

# Phase 5 — Define Failure Semantics

Define exact expected behavior for:

## Missing mode
State default or error.

## Unknown mode
Must be deterministic; normally configuration validation failure.

## Replay mode + missing replay section
Define exact failure.

## Replay mode + missing required field
Define exact failure.

## Replay mode + invalid field
Define exact failure.

## Historical mode + malformed replay-only configuration
Decide whether ignored or validated, based on established repository conventions and least-surprise compatibility.

## Binding parse failure
Define how the Worker surfaces it under current .NET conventions.

Do not invent custom exception taxonomies unless required.

Prefer existing framework/repository exception/validation conventions.

---

# Phase 6 — Define Runtime Mapping

Specify the exact conceptual mapping:

`Worker configuration`
→ `Worker mode`
→ `WP02 replay configuration`
→ `WP02 replay source`
→ `WP03 observation-input seam`
→ `ExecuteCanonical`

For every Worker replay setting, state the corresponding WP02 field/property/semantic.

If a Worker setting has no direct WP02 destination, justify why Worker ownership is necessary.

No duplicate semantic source of truth is allowed.

---

# Phase 7 — Define Historical Compatibility

Explicitly specify how existing historical execution behaves after the future implementation.

Prove the definition preserves:

- existing historical acquisition through `IHistoricalObservationStore`;
- existing materialization behavior;
- existing five-stage pipeline behavior;
- existing callers/configuration that currently do not specify replay settings.

This section must make the backward-compatibility rule unambiguous.

---

# Phase 8 — Define Replay Completion/Cancellation Semantics

Configuration should not invent runtime lifecycle semantics already owned by WP02.

Specify only the Worker-level configuration/composition contract needed for:

- finite replay completion;
- cancellation propagation;
- restart/resume selection, if configurable.

State which behaviors are fixed by WP02 and therefore **not configurable**.

---

# Phase 9 — Define Test Contract

Specify the minimum tests the later implementation authority must add.

At minimum include:

## Configuration binding
- historical mode;
- replay mode;
- missing mode;
- unknown mode;
- valid replay settings;
- missing replay section;
- missing required field;
- invalid field.

## Compatibility
- historical mode without replay settings remains valid if that is the chosen default/contract.

## Runtime mapping
- each Worker replay setting maps to the intended WP02 replay configuration value.

## Production composition
- replay mode selects real WP02 replay source;
- historical mode selects historical path.

Do not implement tests under this authority.

---

# Phase 10 — Normative Contract Artifact

Produce a concise normative configuration definition with these sections:

1. Configuration path
2. Mode key and accepted values
3. Default/missing-mode behavior
4. Replay settings table
5. Binding rules
6. Validation/failure rules
7. Mapping to WP02
8. Historical compatibility
9. Non-configurable WP02 semantics
10. Required implementation tests
11. Non-goals

If repository governance permits a WP03-owned planning artifact, create exactly one such artifact in the authorized Release 1.9/WP03 documentation location.

If no artifact path is authorized, do not invent one; return the normative definition in the completion report instead.

---

# Decision Discipline

Where repository conventions clearly determine a choice, follow them.

Where they do not, make the narrowest choice that:

- satisfies #228;
- preserves historical backward compatibility;
- maps directly to WP02;
- minimizes configuration surface;
- fails invalid replay configuration deterministically.

Every normative choice must include a one-sentence rationale.

If a choice would materially affect product/runtime behavior beyond WP03 and cannot be justified from #228 or existing conventions, stop rather than guessing.

---

# Stop Conditions

Stop immediately if:

- #228 cannot be read;
- WP02 replay configuration cannot be mapped confidently;
- historical compatibility requirements are contradictory;
- configuration semantics would require changing WP02 runtime behavior;
- a choice materially affects WP04+ scope;
- a broad generalized configuration/mode architecture would be required;
- accepted Release 1.9 authority conflicts with the proposed contract;
- creating a local spec artifact would require unauthorized repository mutation.

On stop:

- make no implementation changes;
- make no GitHub mutation;
- report the exact unresolved normative decision;
- identify the minimum additional product/governance authority required.

---

# Success Criteria

This definition authority succeeds only when one complete normative Worker configuration contract is established that specifies:

- exact configuration path;
- exact mode key;
- exact historical/replay values;
- case-sensitivity behavior;
- default/missing-mode behavior;
- replay settings names/types;
- required/optional status;
- validation rules;
- invalid/missing behavior;
- binding semantics;
- direct mapping to WP02;
- historical compatibility;
- finite replay/cancellation ownership boundaries;
- implementation test requirements;
- explicit non-goals.

No production implementation may occur.

No GitHub lifecycle mutation may occur.

WP04 must remain unstarted.

---

# Required Completion Report

Return:

## Normative Worker configuration contract

### Configuration path
Exact path/section.

### Mode
- exact key;
- accepted values;
- case behavior;
- missing/default behavior;
- unknown-value behavior.

### Replay settings
For every field:
- key;
- type;
- required/optional;
- default;
- validation;
- WP02 mapping.

### Binding/validation
- binding types;
- validation timing;
- failure behavior.

### Compatibility
- historical mode behavior;
- treatment of replay-only settings outside replay mode.

### Lifecycle ownership
- restart/resume;
- cancellation;
- finite completion;
- which are configurable versus fixed by WP02.

### Required future tests
Exact scenarios the later implementation authority must prove.

### Non-goals
Explicitly list what this definition does not authorize.

## Mutation proof

State exactly what changed.

If no artifact was authorized/created:

`WP03 WORKER CONFIGURATION DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP03 WORKER CONFIGURATION CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement it here.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP03 WORKER CONFIGURATION DEFINITION COMPLETE`

On blocker, end with exactly:

`RELEASE 1.9 WP03 WORKER CONFIGURATION DEFINITION BLOCKED`

Do not emit success unless the full normative contract is unambiguous.

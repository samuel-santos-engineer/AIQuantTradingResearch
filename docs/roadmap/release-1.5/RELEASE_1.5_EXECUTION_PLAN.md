# Release 1.5 Execution Plan

## Phase 4 — Release 1.5: Deterministic Research Experiment Foundation

## 1. Purpose

This execution plan translates the accepted Release 1.5 definition into a bounded sequence of governed work packages.

Release 1.5 introduces exactly one built-in deterministic offline research experiment:

`simple-return-descriptive-summary-v1`

The experiment consumes the accepted Release 1.4 `simple-return-lag-1-v1` Feature Set and produces immutable descriptive-summary evidence:

- count;
- arithmetic mean;
- minimum;
- maximum.

The release must preserve Releases 1.1–1.4, remain provider-independent and offline after historical acquisition, keep SQLite at schema version 2, and avoid general experiment infrastructure.

This plan is authoritative for Release 1.5 execution sequencing. It does not itself authorize implementation.

---

## 2. Accepted Starting Baseline

Release 1.5 begins from the formally closed Release 1.4 baseline:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- accepted starting SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- `main == origin/main`;
- ahead/behind: `0/0`;
- working tree: clean;
- Release 1.3 PR #152: merged;
- Release 1.4 PR #167: merged;
- milestones #44, #45, and #54: closed;
- milestone #46: open, empty, unchanged;
- Release 1.5 issues: none before GitHub planning;
- SQLite schema: version 2;
- Domain.Tests: 11;
- Application.Tests: 86;
- Infrastructure.Tests: 104;
- Architecture.Tests: 13;
- permanent total: 214;
- build warnings/errors: `0/0`;
- Release 1.5 production/test implementation: none.

The accepted definition is:

`docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`

---

## 3. Release Boundary

The accepted end-to-end boundary is:

```text
exact experiment request
  → existing exact-snapshot feature generation
  → feature evidence validation
  → deterministic descriptive summary
  → canonical experiment identity/provenance
  → immutable in-memory result
```

Release 1.5 does not create a generalized experiment engine.

---

## 4. Core Semantic Contract

### 4.1 Built-in experiment

Canonical experiment key:

`simple-return-descriptive-summary-v1`

Input:

- one exact accepted Release 1.4 Feature Set produced by `simple-return-lag-1-v1`.

Output:

- immutable experiment result evidence;
- count;
- arithmetic mean when count > 0;
- minimum when count > 0;
- maximum when count > 0;
- canonical experiment definition identity;
- canonical experiment result identity;
- provenance and acyclic lineage to the exact Feature Set.

### 4.2 Empty input

An empty Feature Set is successful.

Required result:

- count = 0;
- mean absent;
- minimum absent;
- maximum absent;
- deterministic experiment result identity still bound to the exact input Feature Set;
- no sentinel numeric values.

### 4.3 Non-empty input

For accepted feature values `x[0] ... x[n-1]`:

- count = `n`;
- mean = exact governed decimal arithmetic over the accepted values;
- minimum = minimum accepted value;
- maximum = maximum accepted value.

No binary floating-point conversion or convenience rounding may be introduced.

### 4.4 Identity

Release 1.5 owns:

`aiq-experiment-identity-v1`

Distinct identities:

- Experiment Definition Identity;
- Experiment Result Identity.

Canonical identity computation is explicitly owned by Release 1.5 implementation work and must not be deferred ambiguously.

WP03 freezes the canonical encoding and identity semantics.

WP04 must implement the minimum canonical identity-computation machinery required by the immutable model/contracts if that machinery is necessary to construct valid experiment evidence. WP05 computation must consume that accepted identity machinery; WP05 must not invent a competing encoding.

This ownership rule is intentional and prevents cross-work-package ambiguity.

---

## 5. Preservation Rules

### Release 1.1

Preserve historical-observation persistence, retrieval, fidelity, ordering, isolation, atomicity, idempotency/equivalence, failure mapping, and connection ownership.

### Release 1.2

Preserve immutable dataset/snapshot/catalog behavior, exact lookup, dataset identities, provenance, lineage, equivalence, conflict behavior, and schema v2.

### Release 1.3

Preserve the fixed five-stage research pipeline exactly. Experiment generation is not a sixth pipeline stage.

### Release 1.4

Preserve:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- exact snapshot lookup;
- deterministic decimal feature computation;
- feature evidence/provenance;
- feature one-shot Worker mode;
- absence of feature persistence.

---

## 6. Architecture Constraints

Preferred production graph remains:

```text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Required constraints:

- zero dependency cycles;
- Application owns experiment semantics;
- Infrastructure must not acquire experiment semantics;
- Worker remains composition/trigger/presentation only;
- provider and HTTP concerns remain confined outside Domain/Application;
- no new project or package unless separately justified and authorized;
- zero Domain delta is preferred;
- zero Infrastructure production delta is preferred;
- no schema mutation.

---

## 7. Persistence Decision

Release 1.5 experiment results are immutable in-memory evidence only.

Explicitly excluded:

- experiment table;
- experiment catalog;
- experiment registry;
- experiment cache;
- experiment history;
- run history;
- checkpoints;
- scheduler state.

SQLite remains schema version 2.

---

## 8. Failure Boundary

Release 1.5 must retain bounded semantic distinctions where applicable, including:

- invalid request;
- unsupported experiment definition;
- feature/snapshot not found through the accepted upstream boundary;
- dependency unavailable;
- invalid feature evidence;
- invalid numeric evidence;
- integrity contradiction/conflict;
- successful empty result;
- successful non-empty result.

Unknown defects propagate. No catch-all normalization is allowed.

No downstream experiment identity may be fabricated before its required evidence is established.

---

## 9. Operational Exclusions

The following must not affect experiment semantic identity or equivalence:

- invocation time;
- duration;
- machine/process identity;
- correlation IDs;
- file/database paths;
- credentials;
- logging backend;
- culture;
- local timezone;
- provider ordering;
- retry count;
- scheduling state.

---

## 10. Work-Package Dependency Chain

```text
WP01
  ↓
WP02
  ↓
WP03
  ↓
WP04
  ↓
WP05
  ↓
WP06
  ↓
WP07
  ↓
WP08
  ↓
WP09
  ↓
WP10
  ↓
WP11
  ↓
WP12
  ↓
WP13
```

No WP may begin before its predecessor is Closed/Done unless a later corrective authority explicitly changes the dependency.

---

# WP01 — Release & Repository Preflight

## Purpose

Prove the closed Release 1.4 baseline and establish Release 1.5 GitHub/repository starting truth.

## Expected layers

No production/test mutation.

## Required work

- verify `main` synchronization and cleanliness;
- verify Releases 1.3 and 1.4 closure;
- verify legacy milestone #44 closure;
- verify milestone #46 GitHub planning state after the separate GitHub-planning authority;
- verify Release 1.5 issues/work packages exactly match the accepted plan;
- verify schema v2;
- verify dependency graph;
- verify no premature Release 1.5 implementation;
- run canonical verification;
- establish accepted test baseline.

## Exclusions

- no experiment implementation;
- no semantic artifact beyond already accepted planning authorities;
- no schema/package/reference changes.

## Completion gate

Canonical baseline passes and only WP01 lifecycle changes.

## Recommended model

Luna.

---

# WP02 — Experiment Semantic Discovery

## Purpose

Freeze the exact semantics of `simple-return-descriptive-summary-v1`.

## Expected layers

Documentation/semantic authority only.

## Required work

Define precisely:

- accepted Feature Set input;
- ordering treatment;
- count semantics;
- arithmetic-mean semantics;
- minimum/maximum semantics;
- empty-result semantics;
- decimal arithmetic and overflow policy;
- evidence establishment;
- determinism/equivalence;
- invalid evidence;
- operational exclusions;
- Release 1.4 boundary reuse.

## Exclusions

- no production code;
- no tests;
- no persistence;
- no identity encoding implementation.

## Completion gate

One authoritative semantic artifact with no implementation delta.

## Recommended model

Sol.

---

# WP03 — Experiment Identity, Provenance & Evidence

## Purpose

Freeze `aiq-experiment-identity-v1` and exact provenance/lineage semantics.

## Expected layers

Documentation/semantic authority only.

## Required work

Define:

- Experiment Definition Identity;
- Experiment Result Identity;
- canonical SHA-256 encoding;
- canonical field order/domains;
- decimal encoding;
- count/optional aggregate encoding;
- exact Feature Set binding;
- empty-result identity;
- equivalence;
- integrity contradiction;
- evidence-established-only rules;
- acyclic lineage;
- operational exclusions.

## Critical ownership decision

WP03 owns the semantic specification of canonical encoding.

WP04 owns the minimum production implementation of canonical identity computation needed to construct valid immutable experiment model evidence.

No later WP may silently invent or replace the WP03 encoding.

## Exclusions

- no production implementation;
- no persistence;
- no tests.

## Completion gate

Canonical identity/provenance semantics are unambiguous enough for WP04 implementation.

## Recommended model

Sol.

---

# WP04 — Experiment Model & Contracts

## Purpose

Implement the minimum immutable Application-owned experiment model and contract surface.

## Expected layers

Application only; Domain zero-delta-first.

## Expected production responsibilities

- typed experiment definition identity;
- typed experiment result identity;
- built-in experiment definition;
- immutable descriptive-summary evidence;
- immutable experiment result/provenance model;
- request/result contracts;
- use-case/computation seams as required;
- canonical `aiq-experiment-identity-v1` identity computation required to construct valid model evidence.

## Identity implementation authority

WP04 is explicitly authorized to implement the minimal canonical identity computer corresponding exactly to WP03.

This includes deterministic fingerprint construction needed by later computation.

It must not introduce a second identity scheme or defer valid identity construction to WP05.

## Exclusions

- no summary computation behavior beyond construction invariants;
- no orchestration;
- no DI;
- no Worker;
- no persistence;
- no permanent tests unless the accepted manifest later explicitly assigns construction tests here; preferred permanent test delta is zero.

## Completion gate

Valid immutable model/contracts can represent success and bounded failure requirements without ambiguous identity ownership.

## Recommended model

Terra.

---

# WP05 — Deterministic Summary Computation

## Purpose

Implement the built-in deterministic descriptive-summary computer.

## Expected layers

Application only.

## Required behavior

For accepted Feature Set values:

- calculate count;
- calculate exact governed decimal arithmetic mean;
- calculate minimum;
- calculate maximum;
- produce successful count-zero evidence for empty input;
- construct canonical experiment result identity using WP04 identity machinery;
- preserve exact Feature Set provenance/lineage.

## Required invariants

- deterministic ordering/evidence use;
- no floating-point conversion;
- no convenience rounding;
- no partial result after numeric failure;
- equivalent accepted Feature Sets produce equivalent experiment result identity;
- distinct Feature Set identities remain experiment-distinct.

## Exclusions

- no lookup/orchestration;
- no DI;
- no Worker;
- no persistence;
- no retry/scheduling;
- no generalized statistics engine.

## Completion gate

Deterministic offline computation passes a removable focused probe if permanent tests are not yet authorized.

## Recommended model

Terra.

---

# WP06 — Experiment Validation & Failure Semantics

## Purpose

Establish the canonical Application-owned validation boundary and deterministic failure precedence.

## Expected layers

Application only.

## Required work

Validate:

- request coherence;
- supported definition;
- Feature Set evidence;
- identity/provenance coherence;
- cardinality consistency;
- numeric evidence;
- result construction invariants.

Freeze deterministic first-failure precedence.

Preserve:

- fail-stop behavior;
- no fabricated result identity after failure;
- no partial aggregate result;
- unknown exception propagation.

## Exclusions

- no lookup/orchestration;
- no DI/Worker;
- no persistence.

## Completion gate

All constructible validation/failure cases are deterministically classified.

## Recommended model

Sol.

---

# WP07 — Feature-to-Experiment Integration

## Purpose

Implement the one-shot Application use case from exact experiment request through existing Release 1.4 feature generation to immutable experiment result.

## Expected layers

Application only.

## Required flow

```text
request validation
  → exact upstream feature generation
  → upstream failure classification
  → feature evidence validation
  → exactly one summary computation
  → immutable experiment result
```

## Required behavior

- reuse existing Release 1.4 feature-generation boundary;
- no duplicate snapshot/provider orchestration;
- no provider access;
- empty Feature Set succeeds;
- dependency/not-found/invalid evidence remain distinct where surfaced;
- unknown defects propagate;
- computation executes exactly once after valid upstream evidence.

## Exclusions

- no DI;
- no Worker;
- no persistence;
- no pipeline-stage mutation.

## Completion gate

Application integration is complete and bounded.

## Recommended model

Terra.

---

# WP08 — Dependency Registration & Configuration

## Purpose

Register Release 1.5 Application services and define minimal deterministic experiment execution configuration.

## Expected layers

Application composition plus Worker configuration only.

## Required work

- register experiment use case;
- register summary computer;
- register validator;
- reuse Release 1.4 dependencies;
- define minimal explicit experiment-mode configuration;
- bind exact upstream Feature Set/snapshot inputs using accepted existing contracts;
- keep built-in experiment definition code-owned;
- prove DI resolution has no execution side effects.

## Exclusions

- no Worker execution;
- no persistence;
- no configurable formula/statistics set;
- no provider call;
- no schema changes.

## Completion gate

Production graph resolves deterministically without execution or database side effects.

## Recommended model

Terra.

---

# WP09 — One-Shot Worker Experiment Execution

## Purpose

Expose one bounded Worker execution path for the accepted experiment.

## Expected layers

Worker only.

## Required behavior

- explicit experiment mode;
- construct/resolve canonical request;
- invoke experiment use case exactly once;
- present bounded structured semantic evidence;
- exit `0` on successful empty/non-empty result;
- exit non-zero on bounded configuration/semantic failure;
- unknown defects remain unhandled;
- existing Release 1.3 pipeline and Release 1.4 feature modes remain preserved.

## Exclusions

- no loop;
- no scheduling;
- no retry;
- no persistence;
- no provider fallback;
- no generalized command framework.

## Completion gate

Bounded offline process proof passes and temporary state is removed.

## Recommended model

Terra.

---

# WP10 — Application Experiment Tests

## Purpose

Add permanent deterministic offline Application coverage for Release 1.5 semantics.

## Expected layers

Application.Tests only; Domain test delta zero unless separately justified.

## Required coverage

At minimum:

- identity format/canonicalization;
- definition/result identity distinction;
- Feature Set binding;
- empty-result identity;
- count;
- exact decimal mean;
- minimum/maximum;
- ordering/evidence fidelity;
- culture/timezone independence where relevant;
- equivalent recomputation;
- different Feature Set identity distinction;
- invalid request;
- unsupported definition where constructible;
- invalid evidence;
- invalid numeric evidence/overflow;
- integrity contradiction where constructible;
- upstream failure mapping;
- unknown exception propagation;
- exactly one computation after valid evidence;
- immutability/provenance.

Use hand-written test doubles.

## Exclusions

- no SQLite;
- no Worker process;
- no live provider/network;
- no production behavior changes except narrowly necessary testability corrections explicitly within accepted contracts.

## Completion gate

Permanent Application suite passes with documented delta.

## Recommended model

Luna.

---

# WP11 — Composition & Worker Validation

## Purpose

Add permanent offline composition and black-box Worker-boundary coverage.

## Expected layers

Infrastructure.Tests only unless manifest-authorized test placement requires otherwise.

## Required coverage

- DI registration/lifetimes;
- side-effect-free resolution;
- no resolution-time database creation;
- synthetic accepted upstream snapshot/feature evidence using existing schema-v2 facilities;
- non-empty Worker success;
- equivalent second process identity stability;
- empty Feature Set success;
- invalid experiment configuration;
- exact upstream NotFound where applicable;
- unavailable dependency;
- no fabricated experiment identity on failure;
- no experiment persistence;
- no provider fallback;
- temporary database cleanup.

## Exclusions

- no production mutation;
- no schema change;
- no live provider/network.

## Completion gate

Permanent composition/process suite passes offline with zero residue.

## Recommended model

Terra.

---

# WP12 — Architecture & Documentation Alignment

## Purpose

Reconcile stable architecture rules and current-state documentation with Release 1.5.

## Expected layers

Architecture.Tests only if a genuinely new stable non-redundant rule is required; otherwise zero architecture-test delta is preferred. Documentation updates only within manifest-authorized paths.

## Required work

Evaluate whether existing architecture tests already enforce:

- Application semantic ownership;
- dependency direction;
- acyclicity;
- provider confinement;
- Infrastructure visibility boundaries.

Do not add behavioral or naming-specific architecture tests merely to create a delta.

Align current-state documentation with:

- deterministic experiment boundary;
- experiment identity/provenance;
- exact upstream feature reuse;
- one-shot Worker behavior;
- schema v2;
- no experiment persistence;
- updated permanent test counts;
- Release 1.6+ deferrals.

## Completion gate

Architecture is accurately enforced/documented with no stale current-state claims.

## Recommended model

Terra.

---

# WP13 — Full Validation, Integration & Acceptance

## Purpose

Perform complete Release 1.5 candidate reconciliation, technical/semantic acceptance, one-commit integration, fresh-checkout proof, and review-ready PR creation.

## Expected layers

No new semantic capability. Only mechanical corrections explicitly authorized by the final WP authority may occur.

## Required pre-integration gates

- WP01–WP12 Closed/Done;
- exact file-manifest reconciliation;
- prompt-pair governance reconciliation;
- zero unexpected paths;
- zero generated/database residue;
- whitespace checks;
- Release 1.1–1.4 regressions;
- Release 1.5 semantic acceptance;
- schema decision acceptance;
- architecture acceptance;
- documentation acceptance;
- security/offline acceptance;
- canonical verification.

## Integration requirements

If and only if every gate passes:

- create one Release 1.5 integration branch;
- create exactly one integration commit over accepted `main`;
- run post-commit validation;
- validate exact commit from fresh detached checkout/worktree;
- push normally without force;
- create one non-draft review-ready PR to `main`;
- close WP13 issue only after evidence is complete;
- leave milestone #46 open pending human merge and separate post-merge closure.

## Exclusions

- do not merge PR;
- do not close milestone #46;
- do not tag/release;
- do not begin Release 1.6.

## Completion gate

Review-ready PR represents one fully validated Release 1.5 candidate.

## Recommended model

Sol.

---

## 11. Expected Model Allocation

| WP | Work package | Recommended model |
|---|---|---|
| WP01 | Release & Repository Preflight | Luna |
| WP02 | Experiment Semantic Discovery | Sol |
| WP03 | Experiment Identity, Provenance & Evidence | Sol |
| WP04 | Experiment Model & Contracts | Terra |
| WP05 | Deterministic Summary Computation | Terra |
| WP06 | Experiment Validation & Failure Semantics | Sol |
| WP07 | Feature-to-Experiment Integration | Terra |
| WP08 | Dependency Registration & Configuration | Terra |
| WP09 | One-Shot Worker Experiment Execution | Terra |
| WP10 | Application Experiment Tests | Luna |
| WP11 | Composition & Worker Validation | Terra |
| WP12 | Architecture & Documentation Alignment | Terra |
| WP13 | Full Validation, Integration & Acceptance | Sol |

Model choice is advisory. Governance and acceptance gates remain authoritative regardless of model.

---

## 12. Release-Level Acceptance Matrix

Release 1.5 cannot be accepted unless all applicable rows pass:

| Area | Required acceptance |
|---|---|
| Baseline | Closed Release 1.4 baseline reconciled |
| Governance | Exact WP lifecycle and prompt pairs |
| Semantics | `simple-return-descriptive-summary-v1` frozen |
| Identity | `aiq-experiment-identity-v1` deterministic and canonical |
| Input | Exact accepted Release 1.4 Feature Set evidence |
| Empty | Count-zero success, aggregates absent |
| Non-empty | Count/mean/min/max exact and deterministic |
| Provenance | Exact Feature Set binding and acyclic lineage |
| Failures | Bounded distinctions and unknown propagation |
| Application | Semantic ownership preserved |
| Infrastructure | No experiment production implementation |
| Worker | Explicit one-shot offline execution |
| Persistence | No experiment persistence |
| Schema | SQLite remains v2 |
| Provider | No experiment provider/network dependency |
| Architecture | Accepted graph unchanged and acyclic |
| Tests | Permanent semantic/composition coverage |
| Security | Gitleaks and offline gates pass |
| Documentation | Current-state alignment complete |
| Regression | Releases 1.1–1.4 pass |
| Reproducibility | Fresh-checkout candidate passes |
| Integration | One validated commit and review-ready PR |
| Future scope | Release 1.6+ behavior absent |

---

## 13. Release-Level Stop Rules

Any WP must stop rather than guess when:

- predecessor state is not reconciled;
- authority conflicts with the accepted definition/manifest;
- file ownership is ambiguous;
- canonical identity ownership is ambiguous;
- schema mutation appears without authority;
- provider/network behavior appears outside scope;
- a generalized experiment framework would be required;
- Release 1.6+ behavior would be introduced;
- validation cannot prove deterministic behavior;
- unexpected repository/GitHub mutation occurs.

A blocked WP must report the smallest corrective authority required.

---

## 14. GitHub Lifecycle Model

A separate Release 1.5 GitHub-planning authority should:

- reconcile existing empty milestone #46 to the accepted Release 1.5 definition;
- create exactly WP01–WP13 issues;
- assign the established Release 1.5 priority/Area conventions;
- add all WPs to Project #2;
- leave WP01 Open/Backlog until execution begins;
- avoid implementation.

During execution:

- only the active WP lifecycle may change;
- successor WPs remain Open/Backlog;
- milestone #46 remains open through WP13;
- WP13 creates the review-ready PR but does not merge it;
- human merge is followed by a separate post-merge closure authority;
- milestone #46 closes only after merged-main acceptance passes.

---

## 15. Release 1.6+ Deferrals

Release 1.5 does not include:

- experiment persistence/registry/history;
- feature persistence/catalog;
- broader feature libraries;
- configurable statistics;
- research workspace/notebooks;
- visualization/API surface;
- strategies/signals;
- backtesting;
- portfolio/risk;
- AI/ML;
- explainability;
- MLOps;
- live acquisition orchestration;
- scheduling;
- retries/recovery/checkpoints;
- durable execution history;
- telemetry backends;
- plugins;
- expressions;
- generalized DAGs;
- distributed execution.

---

## 16. Completion Definition

Release 1.5 implementation is complete only after:

1. WP01–WP13 are Closed/Done.
2. The exact governed candidate passes final acceptance.
3. One validated integration commit is represented by a review-ready PR.
4. Human review explicitly authorizes merge.
5. A separate post-merge closure authority verifies merged `main`.
6. Milestone #46 is closed only after that merged-main proof.

The terminal planning marker for this artifact is:

`RELEASE 1.5 EXECUTION PLAN DEFINED`

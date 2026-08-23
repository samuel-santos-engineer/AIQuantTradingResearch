# Release 1.6 Execution Plan

## Phase 4 — Release 1.6: Durable Experiment Evidence Foundation

## 1. Purpose

This execution plan operationalizes the accepted Release 1.6 definition without changing its semantics.

Authoritative predecessor baseline:

`18dfb01bf3503d91415b081b11fcdd7249094373`

Release 1.6 persists only accepted Release 1.5 Experiment Result evidence. It preserves `aiq-experiment-identity-v1`, does not persist Feature Sets, does not introduce a generalized experiment registry, and evolves SQLite atomically and non-destructively from schema v2 to schema v3.

This plan does not authorize implementation by itself. Each work package requires its own full Codex authority prompt and exactly five-non-empty-line chat companion.

## 2. Release Boundary

The Release 1.6 vertical slice is:

```text
exact durable-experiment request
  → existing Release 1.5 experiment generation
  → accepted immutable Experiment Result
  → persistence validation
  → atomic SQLite persistence
  → NewlyAccepted | EquivalentExisting | bounded failure
  → exact Experiment Result retrieval
  → immutable reconstructed evidence
```

Existing Release 1.5 in-memory Experiment mode remains unchanged. Durability is explicit, not an implicit side effect.

## 3. Frozen Semantic Decisions

- Persisted artifact: Experiment Result only.
- Feature Set persistence: none.
- Identity authority: existing `aiq-experiment-identity-v1`.
- Persistence-specific semantic identity: none.
- Exact lookup key: typed Experiment Result Identity.
- Equivalent canonical evidence under the same identity: `EquivalentExisting`.
- First accepted evidence: `NewlyAccepted`.
- Contradictory evidence under the same identity: `IntegrityConflict`.
- Empty evidence retains count zero and absent aggregates exactly.
- Non-empty decimal evidence retains exact mean/minimum/maximum.
- Updates/deletes: absent.
- Retry/fallback/repair/overwrite: absent.
- Unknown programming defects propagate.
- SQLite target: schema v3.
- Migration: atomic, non-destructive v2→v3.
- Provider/network access: none for Release 1.6 acceptance.
- Production dependency graph: unchanged.

## 4. Preservation Contract

Release 1.6 must preserve Releases 1.1–1.5, including:

- observation persistence and idempotency;
- immutable dataset/snapshot identity and exact retrieval;
- Release 1.3 five-stage pipeline;
- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- Release 1.5 in-memory Experiment Worker behavior;
- predecessor bounded failures;
- architecture direction and acyclicity.

No Release 1.7+ capability may be introduced.

## 5. Work-Package Governance

Each WP must:

1. verify its predecessor issue is Closed/Done;
2. move only its own issue Backlog → In Progress;
3. reconcile the accepted baseline and cumulative Release 1.6 candidate;
4. modify only manifest-authorized paths;
5. run targeted proof appropriate to the WP;
6. run canonical `eng/verify.ps1 -Configuration Release`;
7. run formatting, Gitleaks, direct whitespace/final-newline, Git diff, residue, graph, package/reference/schema gates;
8. post concise completion evidence;
9. close only its own issue and set it Done;
10. leave the next issue Open/Backlog;
11. perform no staging, commit, branch, push, PR, tag, release, or future-release work unless WP14 explicitly authorizes integration.

Temporary probes are allowed only when explicitly authorized by the WP and must be removed before completion.

## 6. WP01 — Release & Repository Preflight

**Model:** Luna

**Purpose:** Prove the exact Release 1.5 closed baseline and Release 1.6 planning state before implementation.

**Repository delta:** governance prompt pair only; no production/test/schema delta.

**Required proof:**

- `main == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- Release 1.5 PR #181 merged and milestone #46 closed;
- Release 1.6 definition/plan/manifest accepted;
- no premature Release 1.6 implementation;
- schema v2;
- graph unchanged;
- packages/projects/references unchanged;
- canonical 238/238 baseline;
- GitHub Release 1.6 planning objects exact once separately authorized.

**Completion:** no repository implementation mutation.

## 7. WP02 — Durable Experiment Evidence Discovery

**Model:** Sol

**Purpose:** Freeze persistence semantics before code.

**Primary artifact:** `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`

**Decisions to freeze:**

- exact persisted Experiment Result evidence;
- exact lookup semantics;
- `NewlyAccepted` / `EquivalentExisting`;
- contradiction behavior;
- empty/non-empty fidelity;
- immutability;
- restart behavior;
- no Feature Set persistence;
- no registry/history/search;
- no update/delete;
- provider/storage independence at semantic level.

**Production/test delta:** zero.

## 8. WP03 — Persistence Identity, Provenance & Fidelity

**Model:** Sol

**Purpose:** Define durable equivalence and reconstruction rules without creating a new semantic identity scheme.

**Primary artifact:** `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`

**Decisions to freeze:**

- `aiq-experiment-identity-v1` remains authoritative;
- exact semantic fields required for persistence/reconstruction;
- canonical equivalence comparison;
- contradiction rules;
- provenance/lineage preservation;
- operational metadata exclusions;
- empty aggregate representation;
- decimal fidelity;
- no persistence row identity in semantic identity.

**Production/test delta:** zero.

## 9. WP04 — Application Persistence Contracts

**Model:** Terra

**Purpose:** Introduce the minimum Application-owned durable Experiment Result persistence/retrieval contracts.

**Expected ownership:** Application only.

**Expected concepts:**

- exact result-store abstraction;
- persistence request/result/disposition;
- exact retrieval result;
- bounded persistence failure vocabulary;
- immutable contract surfaces.

**Constraints:**

- no SQLite;
- no DI;
- no Worker;
- no migration;
- no persistence implementation.

## 10. WP05 — Durable Experiment Use-Case Integration

**Model:** Terra

**Purpose:** Add Application orchestration for explicit durable Experiment execution while preserving Release 1.5 in-memory execution.

**Expected flow:**

```text
validate durable request
  → invoke existing IExperimentGenerationUseCase exactly once
  → validate accepted Experiment Result
  → persist exactly once
  → return immutable durable outcome
```

Retrieval orchestration may be introduced only to the extent frozen by WP02–WP04.

**Constraints:**

- no Infrastructure;
- no DI;
- no Worker;
- no retry/fallback;
- no implicit persistence in existing Release 1.5 use case.

## 11. WP06 — Schema-v3 Physical Model

**Model:** Sol

**Purpose:** Freeze the SQLite v3 physical model and migration semantics before implementation.

**Primary artifact:** `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`

**Must define:**

- exact table/column/key/check constraints;
- representation of optional aggregates;
- identity/provenance columns;
- uniqueness/integrity rules;
- v2→v3 migration;
- fresh v3 creation;
- atomicity;
- predecessor-data preservation;
- rollback/failure expectations;
- absence of feature/registry/history tables.

**Implementation delta:** zero.

## 12. WP07 — Experiment Result Persistence

**Model:** Terra

**Purpose:** Implement schema-v3 migration and atomic Experiment Result persistence in Infrastructure.

**Expected behavior:**

- first insert → `NewlyAccepted`;
- equivalent repeat → `EquivalentExisting`;
- contradictory same identity → `IntegrityConflict`;
- atomic write;
- exact decimal/empty fidelity;
- bounded storage failure mapping;
- no overwrite/update/delete.

**Expected ownership:** Infrastructure implementation of Application contract.

## 13. WP08 — Exact Experiment Result Retrieval

**Model:** Terra

**Purpose:** Implement exact retrieval by Experiment Result Identity and reconstruct immutable Application evidence.

**Expected behavior:**

- exact identity lookup;
- Found / NotFound / Unavailable distinction;
- exact empty/non-empty reconstruction;
- identity/evidence verification;
- contradictory stored evidence → integrity failure;
- no fabricated evidence;
- no provider/network fallback.

## 14. WP09 — Storage Validation & Failure Mapping

**Model:** Sol

**Purpose:** Harden the persistence boundary with deterministic validation precedence and bounded SQLite/storage failure semantics.

**Expected concerns:**

- invalid persistence/retrieval request;
- invalid Experiment Result evidence;
- dependency unavailable;
- NotFound;
- integrity conflict;
- schema/storage corruption distinctions only if existing vocabulary justifies them;
- unknown defects propagate.

**Constraints:** no broad catch, retry, repair, or fallback.

## 15. WP10 — Dependency Registration & Configuration

**Model:** Terra

**Purpose:** Register Release 1.6 Application/Infrastructure services and bind explicit durable-experiment configuration.

**Expected behavior:**

- singular intended registrations;
- appropriate lifetimes;
- reuse existing experiment generation stack;
- side-effect-free graph resolution;
- no database creation during DI resolution;
- no provider/network execution;
- configuration validation before execution.

No WP11 Worker execution yet.

## 16. WP11 — One-Shot Durable Experiment Worker

**Model:** Terra

**Purpose:** Add an explicit bounded Worker path for durable Experiment evidence without changing existing Experiment, Feature, or Pipeline modes.

**Routing must be explicit and deterministic.**

The WP authority must freeze exact selector precedence before code modification, preserving all predecessor modes.

**Expected behavior:**

- valid durable request invokes durable use case exactly once;
- success exits 0;
- bounded failure exits 1;
- deterministic bounded evidence;
- `NewlyAccepted` and `EquivalentExisting` observable;
- exact retrieval/restart proof;
- malformed durable intent cannot fall through;
- unknown defects remain unhandled.

## 17. WP12 — Application & Infrastructure Persistence Tests

**Model:** Luna

**Purpose:** Add permanent deterministic offline coverage for the complete Release 1.6 semantic/storage boundary.

**Expected Application coverage:**

- exact forwarding;
- exactly-once generation/persistence;
- dispositions;
- failure precedence;
- unknown propagation;
- immutable evidence.

**Expected Infrastructure coverage:**

- fresh schema v3;
- v2→v3 migration;
- predecessor preservation;
- insert/retrieve fidelity;
- empty/non-empty fidelity;
- idempotency;
- contradiction;
- atomicity;
- restart recovery;
- unavailable storage;
- no extra tables;
- no residue/provider/network.

Exact test-count delta is determined by implementation, not predeclared.

## 18. WP13 — Architecture & Documentation Alignment

**Model:** Terra

**Purpose:** Reconcile structural rules and current-state documentation after implementation.

**Architecture rule policy:** zero-delta-first. Add a rule only if Release 1.6 introduces a stable, non-redundant structural invariant not already enforced.

**Expected documentation scope:** only manifest-authorized current-state files.

Must align:

- durable Experiment Result evidence;
- explicit persistence boundary;
- schema v3;
- exact retrieval;
- idempotency/conflict;
- Worker mode;
- DI/configuration;
- testing baseline;
- no Feature Set persistence/registry/provider fallback;
- future deferrals.

## 19. WP14 — Full Validation, Integration & Acceptance

**Model:** Sol

**Purpose:** Reconcile the exact governed Release 1.6 candidate, validate it completely, integrate it as one release commit, push one integration branch, and open one non-draft PR for human review.

**Mandatory pre-staging gates:**

- exact manifest path reconciliation;
- prompt-pair reconciliation;
- every companion exactly five non-empty logical lines;
- final newline and direct whitespace checks;
- out-of-band execution-authority exclusions;
- no unexpected paths;
- semantic acceptance;
- schema-v3 acceptance;
- predecessor regressions;
- graph/packages/projects/references;
- documentation;
- security;
- canonical tests.

**Integration constraints:**

- one integration branch;
- one release commit over authoritative predecessor main;
- exact governed paths only;
- fresh detached-checkout verification;
- push without force;
- one non-draft PR;
- auto-merge disabled;
- PR remains unmerged;
- WP14 issue Closed/Done only after PR/read-back succeeds;
- Release 1.6 milestone remains open awaiting explicit human merge authorization.

## 20. Release-Level Acceptance

Release 1.6 candidate is acceptable only when it proves:

- durable Experiment Result persistence;
- exact retrieval;
- `NewlyAccepted`;
- `EquivalentExisting`;
- contradiction → `IntegrityConflict`;
- exact empty/non-empty decimal fidelity;
- restart-safe retrieval;
- atomic v2→v3 migration;
- predecessor data preserved;
- no Feature Set persistence;
- no experiment registry/history;
- no update/delete;
- no provider fallback;
- no implicit mutation of Release 1.5 Experiment execution;
- unchanged production dependency graph;
- no unnecessary packages/projects/references;
- all permanent tests pass;
- security/whitespace/residue gates pass;
- fresh-checkout proof passes.

## 21. Release 1.7+ Deferrals

Remain explicitly deferred:

- Feature Set persistence/catalog;
- broader experiment libraries;
- experiment registry/history/comparison/search;
- strategies/signals;
- backtesting;
- portfolio/risk;
- controlled live acquisition;
- scheduling/retry/recovery/checkpoints;
- notebooks/visualization/API;
- AI/ML;
- explainability/MLOps;
- generalized plugins/DAGs;
- distributed execution;
- durable telemetry backends.

## 22. GitHub Planning Boundary

After human acceptance of this execution plan and its file manifest, a separate Release 1.6 GitHub-planning authority may:

- reconcile the appropriate Release 1.6 milestone;
- create exactly WP01–WP14 issues;
- configure Project #2 fields;
- preserve predecessor lifecycle.

No GitHub planning mutation is authorized by this document.

## 23. Final Execution Principle

Release 1.6 must make a deterministic Experiment Result durable without turning the platform into a generalized experiment-management system.

Durability is the capability. Generalization is deferred.

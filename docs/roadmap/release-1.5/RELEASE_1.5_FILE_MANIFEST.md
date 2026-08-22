# Release 1.5 File Manifest

## Phase 4 — Release 1.5: Deterministic Research Experiment Foundation

## 1. Purpose

This manifest defines expected Release 1.5 repository mutation ownership for the accepted capability:

`simple-return-descriptive-summary-v1`

It is authoritative together with:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`

The manifest exists to prevent scope drift, ambiguous cross-WP ownership, accidental schema/package/reference changes, and late candidate-reconciliation surprises.

Exact filenames for implementation files may be refined by a WP authority only when the path remains inside the authorized area and the semantic ownership is unchanged. Any material path-class expansion requires corrective authority.

---

## 2. Manifest Principles

1. Release 1.5 is Application-owned semantically.
2. Domain delta is zero-first.
3. Infrastructure production delta is zero.
4. Worker delta is bounded to explicit one-shot experiment configuration/execution.
5. Permanent tests are added only in Application.Tests and Infrastructure.Tests.
6. Architecture.Tests delta is zero-first.
7. SQLite remains schema v2.
8. Package/project/reference delta is zero.
9. No experiment persistence files are authorized.
10. No provider implementation files are authorized.
11. No Release 1.6 implementation is authorized.
12. Every governed Codex full prompt must have exactly one five-non-empty-line chat companion.
13. Planning-definition execution inputs are out-of-band unless a later governance authority explicitly includes them.

---

## 3. Authoritative Planning Artifacts

### Governed Release 1.5 planning artifacts

```text
docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md
docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md
docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md
```

Ownership:

- Definition: accepted Release 1.5 planning authority.
- Execution Plan: human-authorized planning artifact design.
- File Manifest: human-authorized planning artifact design.

These artifacts may be included in the eventual Release 1.5 governed candidate.

---

## 4. Out-of-Band Planning Inputs

The following planning-definition authority pair is execution input and is not automatically part of the governed Release 1.5 candidate:

```text
release-1.5-planning-definition-codex-prompt.md
release-1.5-planning-definition-codex-prompt-chat.md
```

A later GitHub-planning/final-acceptance authority must classify or exclude out-of-band authority copies explicitly.

Do not silently stage them.

---

## 5. Governance Prompt Area

Release 1.5 is expected to govern one GitHub-planning authority pair and WP01–WP13 execution prompt pairs.

Use the repository's established Release 1.5 governance/prompt location and naming convention.

Expected logical pairs:

```text
release-1.5-github-planning-codex-prompt.md
release-1.5-github-planning-codex-prompt-chat.md

01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md

02-experiment-semantic-discovery-codex-prompt.md
02-experiment-semantic-discovery-codex-prompt-chat.md

03-experiment-identity-provenance-evidence-codex-prompt.md
03-experiment-identity-provenance-evidence-codex-prompt-chat.md

04-experiment-model-contracts-codex-prompt.md
04-experiment-model-contracts-codex-prompt-chat.md

05-deterministic-summary-computation-codex-prompt.md
05-deterministic-summary-computation-codex-prompt-chat.md

06-experiment-validation-failure-semantics-codex-prompt.md
06-experiment-validation-failure-semantics-codex-prompt-chat.md

07-feature-experiment-integration-codex-prompt.md
07-feature-experiment-integration-codex-prompt-chat.md

08-dependency-registration-configuration-codex-prompt.md
08-dependency-registration-configuration-codex-prompt-chat.md

09-one-shot-worker-experiment-execution-codex-prompt.md
09-one-shot-worker-experiment-execution-codex-prompt-chat.md

10-application-experiment-tests-codex-prompt.md
10-application-experiment-tests-codex-prompt-chat.md

11-composition-worker-validation-codex-prompt.md
11-composition-worker-validation-codex-prompt-chat.md

12-architecture-documentation-alignment-codex-prompt.md
12-architecture-documentation-alignment-codex-prompt-chat.md

13-full-validation-integration-acceptance-codex-prompt.md
13-full-validation-integration-acceptance-codex-prompt-chat.md
```

Every `*-codex-prompt-chat.md` companion must contain exactly five non-empty logical lines.

If the repository's established governance directory requires a prefix/path not represented above, the GitHub-planning authority must reconcile it before WP01 rather than duplicating prompt files.

---

## 6. Semantic Documentation

Expected Release 1.5 semantic artifacts:

```text
docs/architecture/data/EXPERIMENT_SEMANTICS.md
docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md
```

### WP02 ownership

`EXPERIMENT_SEMANTICS.md`

Owns:

- `simple-return-descriptive-summary-v1`;
- input Feature Set semantics;
- count/mean/min/max;
- empty result;
- decimal arithmetic;
- determinism/equivalence;
- invalid evidence;
- exclusions.

### WP03 ownership

`EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`

Owns:

- `aiq-experiment-identity-v1`;
- definition/result identities;
- canonical encoding;
- SHA-256 fingerprint semantics;
- Feature Set binding;
- empty-result identity;
- provenance/lineage;
- integrity contradiction;
- operational exclusions.

No other WP may create competing semantic authority documents.

---

## 7. Application Production Area

Expected Release 1.5 Application production area:

```text
src/AIQuantTradingResearch.Application/Experiments/
```

The following logical files are authorized.

### WP04 — Model & Contracts

Expected:

```text
src/AIQuantTradingResearch.Application/Experiments/ExperimentIdentity.cs
src/AIQuantTradingResearch.Application/Experiments/ExperimentDefinition.cs
src/AIQuantTradingResearch.Application/Experiments/ExperimentEvidence.cs
src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationContracts.cs
```

WP04 may consolidate these responsibilities into fewer files when that better matches repository conventions, but it must not expand outside the Application experiment area except for the DI file separately owned by WP08.

WP04 owns:

- typed Experiment Definition Identity;
- typed Experiment Result Identity;
- built-in experiment definition;
- immutable summary evidence;
- provenance/lineage references;
- request/result contract model;
- use-case/computer/validator seams where appropriate;
- minimal canonical identity computation implementing WP03.

### Explicit identity ownership

Canonical `aiq-experiment-identity-v1` production computation belongs to WP04.

If a separate helper file is necessary, the following is authorized:

```text
src/AIQuantTradingResearch.Application/Experiments/ExperimentIdentityCanonicalizer.cs
```

or an equivalently named Application-local identity implementation.

WP05 must consume this machinery and must not introduce another encoding.

---

### WP05 — Deterministic Computation

Expected:

```text
src/AIQuantTradingResearch.Application/Experiments/SimpleReturnDescriptiveSummaryComputer.cs
```

Owns:

- count;
- arithmetic mean;
- minimum;
- maximum;
- empty success;
- numeric failure behavior at the computation boundary;
- result construction using WP04 identity machinery.

No generalized statistics engine is authorized.

---

### WP06 — Validation

Expected:

```text
src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationValidator.cs
```

Owns:

- request validation;
- supported-definition validation;
- Feature Set evidence validation;
- identity/provenance coherence;
- numeric evidence validation;
- deterministic failure precedence.

No lookup/orchestration is authorized here.

---

### WP07 — Integration

Expected:

```text
src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationUseCase.cs
```

Owns:

- accepted request orchestration;
- Release 1.4 feature-generation reuse;
- upstream failure mapping;
- exactly one summary-computation invocation after valid evidence;
- immutable result return.

No provider or persistence integration is authorized.

---

## 8. Application DI

### WP08 ownership

Authorized existing file modification:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
```

Permitted changes:

- register Release 1.5 experiment use case;
- register experiment computer;
- register experiment validator;
- preserve accepted lifetimes;
- reuse Release 1.4 dependencies.

No unrelated registration cleanup/refactor is authorized.

---

## 9. Worker Production Area

### WP08 — Configuration

Expected new file:

```text
src/AIQuantTradingResearch.Worker/ExperimentExecutionConfiguration.cs
```

Owns only:

- explicit experiment execution configuration;
- parsing/validation of required upstream identity/version inputs;
- construction of the sole built-in experiment definition.

No configurable formula/statistics set is authorized.

### WP09 — Execution

Expected new file:

```text
src/AIQuantTradingResearch.Worker/ExperimentExecution.cs
```

Expected existing file modification:

```text
src/AIQuantTradingResearch.Worker/Program.cs
```

Owns only:

- explicit experiment-mode selection;
- one-shot experiment execution;
- bounded semantic evidence presentation;
- deterministic exit policy;
- preservation of existing pipeline/feature modes.

No host loop, retry, scheduling, persistence, or provider fallback is authorized.

---

## 10. Domain Production

Expected Release 1.5 Domain production delta:

`0`

No Release 1.5 experiment file is planned under:

```text
src/AIQuantTradingResearch.Domain/
```

If a WP discovers that Domain mutation is necessary, it must stop and request corrective authority unless the final accepted WP prompt explicitly establishes why Application ownership is insufficient.

---

## 11. Infrastructure Production

Expected Release 1.5 Infrastructure production delta:

`0`

No experiment implementation is authorized under:

```text
src/AIQuantTradingResearch.Infrastructure/
```

In particular, do not add:

- experiment repositories;
- experiment stores;
- experiment catalogs;
- experiment SQLite mappings;
- provider adapters;
- network clients;
- caches;
- run-history stores.

Release 1.5 must reuse existing Release 1.4 upstream boundaries.

---

## 12. Persistence / Schema Files

Expected schema delta:

`0`

No migration or schema file is authorized for Release 1.5.

SQLite must remain schema version 2.

Forbidden Release 1.5 persistence artifacts include logical equivalents of:

```text
ExperimentStore.cs
ExperimentCatalog.cs
ExperimentRepository.cs
ExperimentHistory.cs
ExperimentRunStore.cs
FeatureStore.cs
FeatureCatalog.cs
```

No SQL migration creating experiment state is authorized.

---

## 13. Application Permanent Tests

### WP10 ownership

Expected new file:

```text
tests/AIQuantTradingResearch.Application.Tests/ExperimentApplicationTests.cs
```

This file owns permanent deterministic offline semantic coverage for:

- identity;
- canonical fingerprints;
- definition/result distinction;
- exact Feature Set binding;
- empty-result identity;
- count/mean/min/max;
- decimal arithmetic;
- culture independence;
- equivalence;
- distinct upstream identities;
- provenance/immutability;
- validation/failure behavior;
- upstream mapping through hand-written doubles;
- unknown exception propagation;
- exactly-one computation.

Expected Domain.Tests delta:

`0`

No SQLite, Worker process, provider, or network behavior belongs in this file.

---

## 14. Infrastructure Permanent Tests

### WP11 ownership

Expected new file:

```text
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentCompositionTests.cs
```

Owns permanent offline validation for:

- production DI graph;
- lifetimes;
- side-effect-free resolution;
- Worker process execution;
- synthetic schema-v2 upstream evidence;
- non-empty success;
- equivalent second process;
- empty success;
- invalid configuration;
- NotFound/unavailable behavior where applicable;
- no fabricated identity on failure;
- no experiment persistence;
- no provider fallback;
- temporary SQLite cleanup.

No production Infrastructure change is implied by this test location.

---

## 15. Architecture Tests

Expected Release 1.5 Architecture.Tests delta:

`0` preferred.

WP12 may modify/add an architecture test file only if it proves a new stable, structural, non-redundant Release 1.5 boundary is not already covered.

Existing architecture-test files should remain untouched when current rules already enforce:

- dependency direction;
- acyclicity;
- Application ownership;
- provider/HTTP confinement;
- Infrastructure visibility.

Behavioral Worker/identity/schema assertions belong in functional tests or documentation, not architecture tests.

Any architecture-test delta must be explicitly accounted for in WP12.

---

## 16. Current-State Documentation

WP12 may modify only current-state documentation needed to align Release 1.5.

Expected candidate set, subject to repository-truth reconciliation:

```text
README.md
docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/design/CONFIGURATION_MODEL.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/OBSERVABILITY_MODEL.md
docs/architecture/implementation/TESTING_STRATEGY.md
```

WP12 must first inventory actual stale/current claims.

It may update a subset rather than mechanically touching all eight.

Additional documentation paths require explicit evidence that Release 1.5 makes an existing current-state statement stale and must remain within architecture/roadmap documentation scope.

Do not rewrite future-looking documents merely for stylistic consistency.

---

## 17. Package / Project / Reference Files

Expected delta:

`0/0/0`

The following are protected unless corrective authority explicitly approves a required change:

```text
Directory.Packages.props
Directory.Build.props
global.json
*.csproj
*.sln
*.slnx
```

No new package is expected for count/mean/min/max decimal computation.

No new project is expected.

No production reference edge is expected.

---

## 18. Engineering Scripts

Expected delta:

`0`

Protected unless a discovered Release 1.5 acceptance requirement cannot be exercised through existing tooling:

```text
eng/
```

Do not modify verification/build/test/secret-scan scripts merely to accommodate Release 1.5.

---

## 19. Release 1.1–1.4 Protected Production Areas

Release 1.5 must not modify predecessor behavior outside manifest-authorized shared composition/Worker entry points.

Protected semantics include:

- historical observation persistence/retrieval;
- dataset materialization;
- immutable snapshot/catalog;
- `aiq-dataset-identity-v1`;
- fixed Release 1.3 five-stage pipeline;
- `aiq-pipeline-identity-v1`;
- structured pipeline evidence;
- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- feature exact-snapshot integration.

Any predecessor-file mutation not explicitly listed in this manifest requires a stop and reconciliation.

---

## 20. WP-to-File Ownership Matrix

| WP | Authorized logical paths |
|---|---|
| WP01 | Governance/lifecycle only; no production/test files |
| WP02 | `docs/architecture/data/EXPERIMENT_SEMANTICS.md` |
| WP03 | `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md` |
| WP04 | `Application/Experiments/ExperimentIdentity.cs`, `ExperimentDefinition.cs`, `ExperimentEvidence.cs`, `ExperimentGenerationContracts.cs`, optional canonicalizer |
| WP05 | `Application/Experiments/SimpleReturnDescriptiveSummaryComputer.cs` |
| WP06 | `Application/Experiments/ExperimentGenerationValidator.cs` |
| WP07 | `Application/Experiments/ExperimentGenerationUseCase.cs` |
| WP08 | `Application/DependencyInjection.cs`, `Worker/ExperimentExecutionConfiguration.cs` |
| WP09 | `Worker/Program.cs`, `Worker/ExperimentExecution.cs` |
| WP10 | `Application.Tests/ExperimentApplicationTests.cs` |
| WP11 | `Infrastructure.Tests/ExperimentCompositionTests.cs` |
| WP12 | Architecture.Tests only if justified; manifest-authorized current-state docs |
| WP13 | No new semantic files; integration/governance mechanics only |

Paths in the table are relative to their established `src/` or `tests/` project roots where abbreviated.

---

## 21. Expected Production Delta

Planned logical production delta before WP12 documentation:

### Application

Expected new logical files:

1. `Experiments/ExperimentIdentity.cs`
2. `Experiments/ExperimentDefinition.cs`
3. `Experiments/ExperimentEvidence.cs`
4. `Experiments/ExperimentGenerationContracts.cs`
5. optional `Experiments/ExperimentIdentityCanonicalizer.cs`
6. `Experiments/SimpleReturnDescriptiveSummaryComputer.cs`
7. `Experiments/ExperimentGenerationValidator.cs`
8. `Experiments/ExperimentGenerationUseCase.cs`

Expected modified shared file:

9. `DependencyInjection.cs`

Implementation may consolidate the first four model/contract files, so final candidate reconciliation must use actual accepted paths rather than require empty/artificial files.

### Worker

Expected new files:

1. `ExperimentExecutionConfiguration.cs`
2. `ExperimentExecution.cs`

Expected modified file:

3. `Program.cs`

### Domain

`0`

### Infrastructure

`0`

---

## 22. Expected Test Delta

Expected new permanent test files:

```text
tests/AIQuantTradingResearch.Application.Tests/ExperimentApplicationTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentCompositionTests.cs
```

Expected Domain test files:

`0`

Expected Architecture test file delta:

`0` preferred.

Exact final test-count delta is intentionally not predetermined. WP10 and WP11 must report the actual permanent counts.

---

## 23. Candidate Accounting Rules

WP13 must reconcile the final candidate mechanically.

It must classify every Release 1.5 path as one of:

- planning;
- governance prompt;
- semantic documentation;
- Application production;
- Worker production;
- Application test;
- Infrastructure test;
- Architecture test if justified;
- current-state documentation.

Required:

- missing governed paths: `0`;
- unexpected governed paths: `0`;
- duplicates: `0`;
- generated/database residue: `0`;
- malformed chat companions: `0`;
- unclassified candidate paths: `0`.

Do not stage first and discover scope later.

---

## 24. Filename Reconciliation Rule

The logical filenames in this manifest are authoritative planning names, but implementation must follow repository conventions.

If a WP needs to choose between:

- one consolidated file vs. several files;
- a slightly different class-aligned filename;
- an existing shared file vs. a new helper;

the WP may do so only when:

1. the file remains in the same authorized layer/feature area;
2. semantic ownership is unchanged;
3. no additional capability is introduced;
4. the execution report records the reconciliation;
5. WP13 reconciles the actual accepted path into the governed candidate.

Changes to governance prompt names are stricter: actual executed prompt names must be reconciled before final staging, and every full prompt must have exactly one five-line companion.

---

## 25. Explicitly Forbidden Candidate Paths

Release 1.5 must not contain new logical equivalents of:

```text
src/.../ExperimentStore*
src/.../ExperimentCatalog*
src/.../ExperimentRegistry*
src/.../ExperimentHistory*
src/.../ExperimentScheduler*
src/.../ExperimentRetry*
src/.../Backtest*
src/.../Strategy*
src/.../Portfolio*
src/.../MachineLearning*
src/.../Notebook*
src/.../FeatureStore*
src/.../FeatureCatalog*
```

It must not contain:

- schema migrations for experiment state;
- new provider/network adapters;
- generalized statistics plugin systems;
- configurable experiment DAGs;
- durable run-history persistence.

---

## 26. Temporary Probe Rule

WP04–WP09 may use narrowly scoped temporary offline probes when permanent tests are deliberately assigned to WP10/WP11.

Every probe must:

- remain outside the final candidate;
- use no real credentials;
- use no provider/network call;
- remove temporary databases/files;
- leave zero WAL/SHM/journal residue;
- be removed before WP completion;
- be reported in the WP execution evidence.

Temporary probes are not governed candidate files.

---

## 27. Whitespace and Governance Rule

Before every WP closes:

- `git diff --check` must pass;
- `git diff --cached --check` must pass;
- direct whitespace inspection must cover new untracked governed files when Git diff cannot see them;
- chat companions must meet the five-non-empty-line rule.

WP13 must not rely solely on `git diff --check` for untracked files.

---

## 28. Final Integration Protection

WP13 may integrate only the reconciled Release 1.5 candidate.

Corrective-only or out-of-band execution authorities must be explicitly excluded unless separately governed into the candidate.

The final integration commit must not contain:

- temporary probes;
- databases;
- WAL/SHM/journal files;
- build output;
- credentials;
- local paths as accidental generated evidence;
- planning-definition authority copies classified as out-of-band;
- Release 1.6 artifacts.

---

## 29. Post-Merge Expectation

After human merge, a separate Release 1.5 post-merge closure authority must:

- verify the accepted candidate tree on merged `main`;
- rerun canonical verification;
- prove fresh-checkout reproducibility;
- verify final permanent test counts;
- verify schema v2;
- verify Release 1.1–1.5 regressions;
- close milestone #46 only after all gates pass;
- avoid Release 1.6 work.

This manifest does not authorize that closure.

---

## 30. Manifest Completion Marker

The terminal planning marker for this artifact is:

`RELEASE 1.5 FILE MANIFEST DEFINED`

# Release 1.3 WP03 — Pipeline Identity, Provenance & Evidence Semantics — Codex Execution Authority

## Phase 3 — Release 1.3: Research Pipeline Foundation

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`
**GitHub issue:** `#140`
**Recommended model:** GPT-5.6 Sol
**Work-package type:** Semantic architecture / identity model
**Production implementation:** NOT AUTHORIZED

## 1. Mission

Execute **WP03 — Pipeline Identity, Provenance & Evidence Semantics** only.

WP02 froze the meaning and fixed topology of the Release 1.3 Research Pipeline. WP03 must now define the semantic identity, provenance, lineage/evidence, equivalence, canonical representation, and scheme-evolution rules required for later Application contracts and implementation.

WP03 must define **what constitutes semantic pipeline identity and evidence**, not implement contracts, orchestration, persistence, DI, Worker behavior, or tests.

Do not reopen WP02 topology decisions. Do not redesign Release 1.2 dataset identity semantics. Do not start WP04 or later work.

## 2. Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. Release 1.3 GitHub-planning authority and accepted planning result
5. WP01 authority and accepted result
6. WP02 authority and accepted result
7. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
8. Release 1.2:
   - `RESEARCH_DATASET_DEFINITION.md`
   - `DATASET_IDENTITY_VERSION_PROVENANCE.md`
   - current dataset Application contracts and implementation
9. Release 1.1 persistence semantics
10. current architecture, versioning, data lifecycle, observability, resilience, configuration, and pipeline documentation
11. current permanent tests and architecture tests
12. GitHub issue #140, milestone #54, Project #2, and predecessor state

Treat `RESEARCH_PIPELINE_SEMANTICS.md` as the immediate semantic predecessor and do not contradict it.

If authorities materially conflict, STOP before mutation and report the conflict and smallest corrective authority required.

## 3. Starting-State Gates

Before editing:

- verify repository identity;
- verify branch `main`;
- verify local `main` equals `origin/main`;
- report HEAD and ahead/behind;
- verify staged paths = 0;
- classify all existing tracked/untracked paths;
- preserve accepted Release 1.3 governance and WP02 semantic artifacts;
- verify WP02 #139 is `Closed / Done`;
- verify WP03 #140 is `Open / Backlog`;
- verify WP04 #141 is `Open / Backlog`;
- verify milestone #54 remains open;
- verify WP03 dependency is exactly WP02;
- verify dependency drift = 0;
- verify no WP04+ implementation exists;
- verify no Release 1.4 implementation has started.

Run canonical baseline validation before mutation.

Expected accepted baseline unless repository truth legitimately differs:

- Domain.Tests: 11
- Application.Tests: 60
- Infrastructure.Tests: 87
- Architecture.Tests: 13
- Permanent total: 171
- Build warnings/errors: 0/0
- SQLite schema: version 2
- Dataset identity scheme: `aiq-dataset-identity-v1`
- Canonical verification: PASS
- Gitleaks: PASS

Only after all starting-state and baseline gates pass may #140 move `Backlog → In Progress`.

## 4. Frozen WP02 Semantics

WP03 must preserve:

```text
Explicit pipeline request
  → historical observation retrieval
  → deterministic dataset materialization
  → immutable snapshot persistence
  → catalog registration
  → structured result/evidence
```

Preserve:

- fixed topology;
- fixed ordering;
- persisted Release 1.1 observations as source boundary;
- Release 1.2 dataset semantics;
- one-shot bounded execution;
- deterministic re-execution;
- successful empty-dataset execution;
- first-failed-stage termination;
- semantic vs operational separation;
- Application ownership;
- SQLite schema v2;
- no live acquisition, scheduler, retries, DAG, durable run history, distributed execution, feature engineering, or MLOps.

WP03 may refine identity/evidence terminology but may not alter these decisions.

## 5. Core Identity Problem

Define and keep distinct at least these concepts:

1. **Pipeline Definition Identity**
2. **Semantic Pipeline Execution Identity** (or Semantic Run Identity)
3. **Dataset Definition Identity** — reused from Release 1.2
4. **Dataset Snapshot Identity / Dataset Version** — reused from Release 1.2
5. **Relevant Source State Identity** — reused from Release 1.2 where applicable
6. **Operational Invocation / Correlation Identifier** — explicitly non-semantic

Do not collapse these concepts.

The semantic pipeline execution identity must not be a wall-clock timestamp, sequence number, random GUID, database ID, process ID, or log correlation ID.

## 6. Pipeline Definition Identity

Define exactly which semantic facts determine Pipeline Definition Identity.

At minimum reconcile:

- fixed Release 1.3 pipeline topology;
- ordered semantic stages;
- dataset-definition semantics;
- semantic pipeline-model version;
- semantic parameters, if any;
- deterministic rules that can alter semantic output.

Explicitly exclude:

- invocation timestamp;
- duration;
- machine/process identity;
- paths;
- connection strings;
- environment name unless it changes semantic input;
- logging configuration;
- random values;
- provider credentials;
- correlation identifiers;
- database row IDs/natural ordering.

Clarify the relationship between Pipeline Definition Identity and the Release 1.2 Dataset Definition Identity.

## 7. Semantic Pipeline Execution Identity

Define the identity of one semantic execution result independently from an operational process invocation.

It must represent enough semantic state to distinguish executions when relevant semantic inputs or accepted source state change.

At minimum evaluate inclusion of:

- Pipeline Definition Identity;
- relevant Release 1.2 source-state identity;
- resulting Dataset Snapshot Identity / Dataset Version;
- terminal semantic outcome where necessary;
- stage-level semantic evidence where necessary.

Avoid circular identity definitions. Choose an acyclic derivation model and document it explicitly.

Equivalent semantic execution must yield equivalent semantic execution identity.

A second operational invocation of equivalent semantics must not create a new semantic identity solely because it happened later.

## 8. Success and Failure Identity Semantics

Determine whether semantic execution identity exists for:

- successful execution;
- equivalent-existing successful execution;
- valid empty-dataset success;
- failed execution.

If failed executions require semantic identity/evidence, define the minimum deterministic basis without relying on wall-clock/runtime metadata.

Do not force a failed execution to pretend a downstream Dataset Snapshot Identity exists when the pipeline failed before such evidence was established.

Prefer a coherent model that supports stage-attributed failure evidence without fabricating unavailable downstream identities.

## 9. Operational Invocation Identity

Define a clearly separate operational concept for logs/correlation.

Properties:

- may be unique per process invocation;
- may be random or runtime-generated;
- may differ across equivalent semantic executions;
- is not part of semantic pipeline identity;
- is not part of dataset identity;
- does not create a new pipeline version;
- does not affect equivalence;
- is not required to be durably persisted.

Do not choose implementation type (`Guid`, string, etc.) unless required to explain the semantic boundary. WP04/WP07/WP09 own representation/use.

## 10. Pipeline Version Semantics

Determine whether Release 1.3 needs a separately named **Pipeline Version** concept.

If yes, define it precisely and state whether it is:

- equivalent to Pipeline Definition Identity;
- a wrapper over semantic execution identity;
- or a distinct semantic concept.

Do not introduce mutable numeric versions or “latest” semantics without necessity.

Prefer immutable content-derived semantic versioning consistent with Release 1.2 principles.

If a separate Pipeline Version adds no semantic value, explicitly reject it and explain why.

## 11. Canonical Representation

Define a deterministic canonical semantic representation suitable for later implementation.

Requirements:

- explicit scheme identifier;
- explicit type/domain separation;
- versioned representation;
- culture-independent;
- timezone-independent;
- stable field ordering;
- unambiguous field boundaries;
- deterministic UTF-8 representation;
- no serializer-default dependence;
- no `GetHashCode`;
- no runtime object identity;
- no database natural ordering;
- no operational metadata.

Reconcile with the accepted Release 1.2 length-delimited canonicalization model and reuse its principles unless a semantic reason requires otherwise.

Do not implement code.

## 12. Identity Scheme

Choose and freeze a Release 1.3 pipeline identity scheme name.

Preferred form:

`aiq-pipeline-identity-v1`

Validate that this does not collide conceptually with:

`aiq-dataset-identity-v1`

Define scheme ownership and evolution rules.

## 13. Digest / Fingerprint

Decide the digest algorithm and representation.

Unless repository truth provides a reason to differ, prefer consistency with Release 1.2:

- SHA-256;
- exactly 64 lowercase hexadecimal characters.

State that the digest is compact deterministic identity evidence, not:

- authentication;
- authorization;
- digital signature;
- proof of collision impossibility.

No cryptographic redesign beyond identity fingerprinting is authorized.

## 14. Canonical Field Semantics

Specify canonical semantic fields for each pipeline identity type.

At minimum define conceptual canonical inputs for:

### Pipeline Definition Identity
- scheme/domain marker;
- pipeline semantic-model version;
- fixed ordered topology/stages;
- Dataset Definition Identity or exact equivalent semantic dependency;
- semantic parameters.

### Semantic Pipeline Execution Identity
Use an acyclic semantic derivation based on accepted upstream identity/evidence.

Explicitly define handling for:
- successful newly accepted result;
- successful equivalent-existing result;
- empty success;
- stage-attributed failure.

Do not include operational invocation ID or runtime timing.

## 15. Equivalent Re-execution

Freeze:

Equivalent pipeline definition/request + equivalent relevant accepted source state must produce equivalent:

- semantic stage progression where applicable;
- dataset semantic evidence;
- terminal semantic result;
- semantic pipeline execution identity;
- provenance/evidence.

`NewlyAccepted` versus `EquivalentExisting` may describe persistence disposition, but determine whether that distinction is semantic execution identity-bearing or merely evidence/outcome disposition.

Choose one authoritative rule and justify it.

The rule must not cause equivalent reruns to generate artificial new semantic versions.

## 16. Relevant Change Distinguishability

Define changes that must create distinguishable semantic identity/evidence.

Evaluate at least:

- target change;
- `[from,to)` change;
- dataset semantic parameter change;
- pipeline semantic-model version change;
- topology/stage semantic change;
- relevant source-state membership change;
- selected observation instant/offset/decimal change;
- terminal failing stage change;
- bounded semantic failure classification change.

Explicitly exclude unrelated operational changes.

## 17. Provenance Semantics

Define pipeline provenance as semantic explanation of how pipeline evidence was derived.

At minimum provenance should make knowable, where applicable:

- Pipeline Definition Identity;
- semantic execution identity;
- pipeline identity scheme;
- Dataset Definition Identity;
- relevant Source State Identity;
- resulting Dataset Snapshot Identity / Dataset Version;
- fixed ordered stage model;
- terminal outcome;
- failing stage and bounded failure category for failure;
- relationship to accepted Release 1.1/1.2 evidence.

Do not duplicate complete Release 1.2 dataset provenance; reference/reuse it semantically.

## 18. Pipeline Lineage

Determine whether a distinct pipeline-lineage concept is useful.

If retained, keep it narrow and acyclic, for example:

```text
Pipeline Definition
    + relevant persisted source state
    → semantic pipeline execution
    → dataset snapshot/catalog evidence
```

For failure, lineage must stop at the last semantically established evidence and must not fabricate downstream artifacts.

Do not introduce a general graph/DAG lineage engine.

## 19. Pipeline Evidence Semantics

Define semantic **Pipeline Evidence** independently from logging.

Evidence must be sufficient to explain:

- what pipeline definition was executed;
- against what relevant semantic source state;
- ordered stages;
- terminal outcome;
- dataset evidence when produced;
- failure stage/classification when failed;
- semantic identity/provenance relationships.

Pipeline Evidence must be immutable as semantic evidence.

Do not require durable database persistence in Release 1.3.

## 20. Stage Evidence

Define the minimum semantic evidence for each fixed stage.

At minimum evaluate:

- stage identity/name;
- fixed ordinal;
- semantic outcome/disposition;
- established input identity references;
- established output identity references;
- bounded failure category when failed.

Exclude:

- start/end wall-clock timestamps;
- duration;
- machine/process/thread IDs;
- logging severity;
- mutable retry counters;
- backend trace/span IDs.

WP07 will define structured execution evidence contracts/representation.

## 21. Failure Evidence

Failure evidence must:

- identify the first failing stage;
- preserve the bounded semantic failure category;
- preserve established upstream semantic identities;
- not claim downstream completion;
- not imply rollback of already accepted immutable evidence;
- not normalize unknown failures into false semantic categories.

WP06 owns detailed validation/failure semantics; WP03 only freezes identity/provenance/evidence requirements.

## 22. Empty Dataset Evidence

For a valid empty dataset success:

- semantic execution succeeds;
- source state may represent zero selected observations according to Release 1.2;
- Dataset Snapshot Identity / Version remains deterministic;
- pipeline semantic execution identity remains deterministic;
- provenance/evidence explicitly represents successful empty output;
- no sentinel observation or special mutable version is introduced.

## 23. Immutability and Reassignment

Freeze:

- semantic pipeline identities are immutable;
- identity fingerprints cannot be reassigned to contradictory semantic content;
- accepted evidence cannot be silently overwritten;
- contradictory content under equal fingerprint is an integrity conflict;
- operational invocation identifiers do not redefine semantic identity;
- “latest pipeline run” is not a semantic identity concept.

## 24. Collision Handling

As with Release 1.2:

If equal fingerprint/scheme is observed for contradictory canonical semantic content:

- classify as integrity conflict;
- do not overwrite;
- do not silently alias;
- do not generate a replacement identity by adding timestamps/randomness.

No collision-recovery storage mechanism is authorized.

## 25. Scheme Evolution

Define:

- existing identities retain their original scheme;
- future schemes cannot reinterpret old fingerprints;
- scheme identifier participates in interpretation;
- semantic-model evolution that changes canonical meaning requires explicit new scheme/model authority;
- no automatic migration/re-hashing of accepted semantic identities.

## 26. Evidence Persistence Boundary

Freeze:

**No persisted operational pipeline-run history in Release 1.3.**

SQLite remains schema version 2.

Pipeline semantic evidence may be returned/structured by Application and surfaced by Worker/logging, but WP03 must not require new tables/files.

If semantic correctness appears to require durable run-history persistence, STOP and report an authority conflict.

## 27. Architecture Ownership

### Domain
Expected delta: 0.

### Application
Owns:
- pipeline identity concepts;
- semantic provenance/evidence contracts later;
- semantic equivalence rules;
- fixed pipeline orchestration later;
- storage/provider-independent failure semantics.

### Infrastructure
Continues implementing accepted persistence seams. It must not own pipeline semantic identity.

### Worker
May later create operational invocation/correlation information and surface structured evidence, but does not define semantic identity.

Production dependency graph remains unchanged.

## 28. WP04 Handoff

WP03 must provide enough frozen semantics for WP04 to implement Application contracts without reopening identity design.

Explicitly specify required contract concepts/values, while leaving C# type shapes to WP04.

Expected handoff includes:

- Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- optional/rejected Pipeline Version decision;
- identity scheme;
- fingerprint validation semantics;
- pipeline definition semantic content;
- semantic result/evidence model requirements;
- stage evidence requirements;
- provenance/lineage requirements;
- operational invocation separation;
- success/failure/empty representation requirements;
- integrity-conflict rules.

Do not create WP04 contracts.

## 29. Explicit Later-WP Boundaries

Do not start:

- WP04 Application contracts;
- WP05 orchestration;
- WP06 validation/failure implementation;
- WP07 structured evidence implementation;
- WP08 DI/configuration;
- WP09 Worker execution;
- WP10+ tests/architecture/docs/integration.

Do not start Release 1.4 work.

## 30. Authorized Artifact

Create exactly the manifest-authorized WP03 semantic architecture artifact.

Preferred path:

`docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`

If the manifest names another exact path, the manifest wins.

The artifact must include at least:

1. Purpose
2. Authorities and predecessor semantics
3. Vocabulary
4. Identity layers
5. Pipeline Definition Identity
6. Semantic Pipeline Execution Identity
7. Operational Invocation Identity
8. Pipeline Version decision
9. Identity scheme
10. Canonical representation
11. Digest/fingerprint
12. Canonical field semantics
13. Equivalent re-execution
14. Relevant-change distinguishability
15. Provenance
16. Lineage
17. Pipeline evidence
18. Stage evidence
19. Failure evidence
20. Empty-result evidence
21. Immutability
22. Collision handling
23. Scheme evolution
24. Persistence/schema boundary
25. Architecture ownership
26. WP04 handoff
27. Release 1.4+ protection

## 31. Prohibited Mutations

Do not modify:

- production code;
- tests;
- packages/references;
- SQLite schema/bootstrap;
- DI/configuration;
- Worker;
- build/engineering scripts;
- Release 1.3 definition;
- execution plan;
- file manifest;
- planning authority;
- WP02 semantic artifact except to report a genuine authority conflict.

Do not stage, commit, push, branch, create PR, merge, tag, or release.

## 32. Validation

After creating the artifact:

- validate all repository-relative links;
- validate no trailing whitespace;
- `git diff --check`;
- `git diff --cached --check`;
- restore;
- format verification;
- build;
- Domain.Tests;
- Application.Tests;
- Infrastructure.Tests;
- Architecture.Tests;
- canonical `eng/verify.ps1 -Configuration Release`;
- Gitleaks;
- confirm SQLite/database residue = 0.

Expected permanent baseline remains:

- Domain: 11
- Application: 60
- Infrastructure: 87
- Architecture: 13
- Total: 171
- Skipped: 0
- Build warnings/errors: 0/0

Production/test/package/reference/schema delta must be zero.

## 33. GitHub Lifecycle

After starting-state and baseline gates pass:

- move #140 `Backlog → In Progress`.

Only after every WP03 acceptance gate passes:

- post concise completion evidence to #140;
- close #140;
- set Project #2 status to Done.

Verify:

- #141 remains `Open / Backlog`;
- milestone #54 remains open;
- no other issue is modified.

## 34. Acceptance Matrix

WP03 completes only if:

- WP02 predecessor: PASS
- WP02 topology preserved: PASS
- Release 1.2 dataset identity preserved: PASS
- Pipeline Definition Identity: PASS
- Semantic Pipeline Execution Identity: PASS
- operational invocation separation: PASS
- Pipeline Version decision: PASS
- identity derivation acyclic: PASS
- identity scheme: PASS
- canonical representation: PASS
- digest/fingerprint: PASS
- equivalent re-execution semantics: PASS
- relevant-change distinguishability: PASS
- success identity/evidence: PASS
- failure identity/evidence: PASS
- empty-result identity/evidence: PASS
- provenance: PASS
- lineage: PASS
- stage evidence: PASS
- immutability: PASS
- collision handling: PASS
- scheme evolution: PASS
- durable run-history introduced: NO
- schema evolution: NO
- production delta: 0
- permanent test delta: 0
- package/reference delta: 0/0
- WP04 started: NO
- Release 1.4 implementation started: NO
- canonical validation: PASS

## 35. Required Execution Report

Report at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. WP02 Semantic Reconciliation
10. Release 1.2 Identity Reconciliation
11. Identity Vocabulary
12. Identity Layers
13. Pipeline Definition Identity
14. Semantic Pipeline Execution Identity
15. Operational Invocation Identity
16. Pipeline Version Decision
17. Identity Derivation / Acyclicity
18. Identity Scheme
19. Canonical Representation
20. Digest/Fingerprint Decision
21. Canonical Field Semantics
22. Equivalent Re-execution
23. Relevant-Change Distinguishability
24. Success Identity/Evidence
25. Failure Identity/Evidence
26. Empty Dataset Identity/Evidence
27. Provenance Semantics
28. Lineage Semantics
29. Pipeline Evidence
30. Stage Evidence
31. Immutability/Reassignment
32. Collision Handling
33. Scheme Evolution
34. Persistence/Schema Boundary
35. Architecture Ownership
36. WP04 Handoff
37. Artifact Added
38. Production Delta
39. Permanent Test Delta
40. Package/Reference/Schema Delta
41. Validation Evidence
42. Architecture Validation
43. Security/Offline Evidence
44. Whitespace/Link Evidence
45. Mutation Accounting
46. Git/GitHub Protection
47. Planning Protection
48. Findings/Blockers
49. Acceptance Matrix
50. Final Repository/GitHub State
51. Final Decision
52. Next Authorized Work Package

On success end exactly with:

RELEASE 1.3 WP03 COMPLETE

NEXT AUTHORIZED WORK PACKAGE:
WP04 — Application Pipeline Contracts
GitHub issue #141

If blocked, end with:

RELEASE 1.3 WP03 BLOCKED

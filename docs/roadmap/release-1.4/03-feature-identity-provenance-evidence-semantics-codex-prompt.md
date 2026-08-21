# Release 1.4 WP03 — Feature Identity, Provenance & Evidence Semantics — Codex Authority

## Mission

Execute **WP03 — Feature Identity, Provenance & Evidence Semantics** for:

**Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**

GitHub issue: **#155**

WP03 freezes the semantic identity, provenance, lineage, and evidence rules for the single Release 1.4 built-in transformation:

`simple-return-lag-1-v1`

This is a semantic-definition work package. Do not implement feature models, Application contracts, computation, DI, Worker behavior, persistence, schema evolution, or permanent feature tests.

## Governing Authorities

Read completely before any mutation:

1. `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2. `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4. `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5. Release 1.4 GitHub-planning authority and accepted planning result.
6. WP01 authority/result.
7. WP02 authority/result.
8. Release 1.2 dataset identity/provenance authorities and implementation.
9. Release 1.3 pipeline identity/provenance/evidence authorities and implementation.
10. Current repository architecture, data-lifecycle, public-contract, testing, configuration, and observability documentation relevant to identity/evidence semantics.

Repository truth wins over assumptions.

If an accepted predecessor semantic materially conflicts with this authority, stop rather than silently rewriting predecessor behavior.

## Starting-State Gates

Verify before starting:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- Release 1.3 milestone #54: Closed;
- Release 1.4 milestone #45: Open;
- WP01/#153: Closed/Done;
- WP02/#154: Closed/Done;
- WP03/#155: Open/Backlog;
- WP04/#156: Open/Backlog;
- WP04 not started;
- no WP15+;
- no Release 1.5 implementation.

Classify the working tree before mutation.

Preserve all accepted Release 1.4 planning/governance artifacts and the WP02 semantic artifact.

Unexpected paths or premature feature implementation are blockers unless explicitly authorized by an existing governing artifact.

## Authorized Repository Mutation

Create exactly one semantic artifact:

`docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`

Do not modify any other repository file.

Expected WP03 repository delta:

- semantic documentation: `+1 file`;
- Domain production: `0`;
- Application production: `0`;
- Infrastructure production: `0`;
- Worker production: `0`;
- tests: `0`;
- architecture tests: `0`;
- packages: `0`;
- project references: `0`;
- schema: `0`.

Do not stage, commit, push, create a branch, open a PR, merge, tag, or release.

## Predecessor Identity Reconciliation

Before freezing Release 1.4 semantics, inspect and reconcile the accepted identity systems.

### Release 1.2 dataset identity

Preserve:

- identity scheme `aiq-dataset-identity-v1`;
- deterministic canonical representation;
- SHA-256 fingerprints;
- lowercase hexadecimal representation;
- exact dataset definition identity;
- exact source-state identity;
- immutable snapshot/version identity;
- provenance and acyclic lineage;
- distinction between semantic identity and operational invocation details;
- equivalent evidence semantics;
- integrity-conflict behavior.

Do not redefine dataset identity.

### Release 1.3 pipeline identity

Preserve:

- identity scheme `aiq-pipeline-identity-v1`;
- distinct Pipeline Definition and Semantic Pipeline Execution identities;
- deterministic canonical representation;
- SHA-256 fingerprints;
- lowercase hexadecimal representation;
- acyclic identity derivation;
- semantic rerun equivalence;
- disposition-independent execution identity;
- first-failure evidence limits;
- separation of semantic and operational identifiers.

Do not redefine pipeline identity.

### Release 1.4 relationship

Feature identity must compose with accepted dataset evidence without replacing or mutating predecessor identity systems.

Feature generation remains separate from the Release 1.3 five-stage pipeline.

## Identity Scheme

Freeze the Release 1.4 feature identity scheme as:

`aiq-feature-identity-v1`

All semantic feature fingerprints governed by this scheme must use:

- SHA-256;
- exactly 32 digest bytes;
- exactly 64 lowercase hexadecimal characters when rendered;
- deterministic UTF-8 canonical content;
- length-delimited components;
- ordinal byte semantics;
- no culture-dependent formatting;
- no timezone-dependent formatting;
- no machine/runtime-dependent content.

Do not use JSON serialization, object hash codes, reflection order, dictionary enumeration order, filesystem state, process identity, random values, or current time as identity authority.

## Required Identity Types

Freeze two distinct semantic identities.

### 1. Feature Definition Identity

Represents exactly what transformation semantics are requested.

For Release 1.4 there is one supported semantic definition:

`simple-return-lag-1-v1`

The Feature Definition Identity must be derived from semantic definition content only.

It must not include:

- input snapshot identity;
- source state;
- execution time;
- invocation/correlation ID;
- output feature values;
- persistence disposition;
- Worker/process details.

Equivalent requests for the same built-in semantic definition must produce the same Feature Definition Identity.

### 2. Feature Set Identity

Represents the deterministic semantic feature output for one exact accepted input snapshot and one exact Feature Definition Identity.

The Feature Set Identity must bind at minimum:

- `aiq-feature-identity-v1`;
- Feature Definition Identity;
- exact input Dataset Snapshot Identity;
- exact input Dataset Snapshot Version where predecessor semantics require version distinction;
- canonical ordered feature values;
- each feature value's accepted timestamp/offset evidence;
- semantic cardinality.

The identity must make a feature set from snapshot A non-interchangeable with a feature set from snapshot B even if their numeric output happens to be equal.

Do not create a mutable Feature Version concept.

Do not create an Operational Feature Run Identity.

## Acyclic Identity Derivation

Freeze an acyclic derivation graph.

Conceptually:

```text
Feature semantic definition
  → Feature Definition Identity

Accepted Dataset Snapshot Identity/Version
  + Feature Definition Identity
  + deterministic ordered feature evidence
  → Feature Set Identity
```

Feature Definition Identity must not depend on Feature Set Identity.

Feature Set Identity must not feed back into dataset, source-state, snapshot, or pipeline identities.

No circular provenance or identity derivation is permitted.

## Canonical Representation Rules

Freeze canonical identity encoding principles.

Each canonical identity payload must:

1. begin with an explicit scheme/version discriminator;
2. use deterministic UTF-8;
3. encode each logical field as an unambiguous length-delimited component;
4. preserve exact field order defined by the semantic contract;
5. use invariant canonical representations for scalar values;
6. preserve accepted timestamp-offset semantics;
7. preserve decimal semantics without conversion to binary floating point;
8. distinguish absence from an empty string or empty collection;
9. encode collections in semantic order;
10. exclude operational metadata.

Use explicit field/domain labels where needed to prevent cross-type ambiguity.

Do not depend on incidental C# type names or namespace names as semantic identity.

## Canonical Decimal Semantics

Feature values are decimal semantic values.

Identity encoding must preserve the exact semantic decimal value without culture dependence.

Do not use:

- locale-specific decimal separators;
- binary floating-point conversion;
- display-only formatting;
- convenience rounding;
- scientific-notation variability.

The artifact must state the canonical semantic requirement clearly.

Do not prematurely prescribe an implementation API if repository truth does not require one.

## Canonical Timestamp/Offset Semantics

For each feature value, timestamp identity evidence belongs to the current observation `i`, exactly as frozen by WP02.

Preserve the accepted `DateTimeOffset` instant and offset semantics.

Do not:

- replace the accepted offset with local timezone;
- normalize away the offset if that changes accepted evidence;
- use feature execution time;
- use prior observation timestamp;
- use machine timezone.

Canonical encoding must be invariant and round-trippable.

## Feature Definition Canonical Content

For Release 1.4, freeze the semantic content required to distinguish the built-in definition.

At minimum, canonical definition content must capture:

- feature identity scheme/version;
- built-in semantic definition name/version: `simple-return-lag-1-v1`;
- fixed lag: `1`;
- fixed formula semantics: `(p[i] / p[i-1]) - 1`;
- current-observation timestamp ownership;
- accepted decimal arithmetic semantics;
- accepted zero-predecessor invalidity semantics;
- accepted empty/single-observation behavior.

Do not add user-configurable parameters that Release 1.4 does not support.

Do not create aliases that produce different identities for the same semantic definition.

## Feature Set Canonical Content

Freeze the semantic content required for Feature Set Identity.

At minimum, bind:

- feature identity scheme/version;
- Feature Definition Identity;
- exact Dataset Snapshot Identity;
- exact Dataset Snapshot Version when represented by accepted predecessor contracts;
- feature count;
- ordered feature evidence.

For each feature value, bind semantically relevant evidence such as:

- position/order;
- timestamp;
- offset;
- exact decimal value.

Do not include provider names, SQL rows, database IDs, paths, connection strings, logging metadata, execution duration, or Worker output formatting.

## Empty Feature Set Identity

Empty feature sets are valid semantic results.

Both:

- an accepted empty dataset snapshot; and
- an accepted one-observation dataset snapshot

produce a successful empty feature set under WP02 semantics.

They must still produce deterministic Feature Set Identity evidence bound to the exact input snapshot and Feature Definition Identity.

Therefore, two empty feature sets from different accepted snapshot identities are not automatically the same Feature Set Identity.

Do not use a global sentinel empty identity.

Do not omit identity merely because feature cardinality is zero.

## Equivalent Recomputation

Equivalent recomputation means:

- same exact accepted input snapshot identity/version;
- same Feature Definition Identity;
- same deterministic semantic feature evidence.

It must produce the same Feature Set Identity.

Operational invocation differences must not affect semantic identity.

Release 1.4 has no persistence disposition analogous to snapshot `NewlyAccepted` versus `EquivalentExisting`.

Do not invent one.

## Same Values, Different Input Snapshot

Freeze explicitly:

If two different accepted Dataset Snapshot identities happen to produce identical ordered feature values, their Feature Set identities remain distinct because the exact input snapshot is part of semantic lineage and identity.

Numeric coincidence does not erase provenance.

## Definition Change

Any future semantic change to:

- formula;
- lag;
- timestamp ownership;
- numeric semantics;
- invalid numeric behavior;
- empty-result behavior;

must not silently retain the same Feature Definition Identity semantics.

Release 1.4 supports only `simple-return-lag-1-v1`.

Future definitions require separately governed semantic versions/identities.

Do not design the future registry/plugin system now.

## Provenance Semantics

Freeze immutable feature provenance.

A successful Feature Set must be traceable to:

- Feature Definition Identity;
- exact Dataset Snapshot Identity;
- exact Dataset Snapshot Version where applicable;
- predecessor dataset provenance;
- predecessor source-state evidence reachable through accepted dataset lineage;
- Feature Set Identity.

Feature provenance must reuse predecessor evidence rather than duplicating or redefining dataset/source-state identity.

No provider-specific provenance is introduced by feature computation.

## Lineage Semantics

Freeze narrow, acyclic lineage.

Conceptually:

```text
Source State
  → Dataset Definition
  → Dataset Snapshot / Version
  → Feature Definition
  → Feature Set
```

The precise predecessor graph should reflect repository truth, but must remain acyclic.

Feature lineage must not mutate predecessor lineage.

Do not introduce:

- mutable feature version history;
- feature-run history;
- pipeline-run history;
- operational execution lineage;
- persistence lineage.

## Semantic Evidence

Freeze the minimum semantic evidence categories needed by later contracts.

### Successful evidence

Successful feature evidence must be capable of representing:

- Feature Definition Identity;
- exact input Dataset Snapshot Identity/version;
- Feature Set Identity;
- ordered immutable feature values;
- cardinality;
- provenance/lineage;
- successful empty/non-empty outcome.

### Failure evidence

Failure evidence must be capable of representing only semantic facts established before failure.

Potential bounded categories inherited from WP02 include:

- invalid request;
- unsupported definition;
- `NotFound`;
- dependency unavailable;
- invalid predecessor evidence;
- invalid numeric evidence;
- integrity contradiction.

Unknown defects remain outside bounded semantic normalization and must propagate according to later contract design.

WP03 must not implement final exception/result classes.

## Evidence-Established-Only Rule

Do not claim identities before their semantic prerequisites exist.

Examples:

- Feature Definition Identity may exist once a valid supported definition is established.
- Dataset Snapshot Identity/version may be known after exact lookup succeeds.
- Feature Set Identity must not exist until deterministic feature evidence has been successfully computed and validated.
- If numeric computation fails, no Feature Set Identity exists.
- If snapshot lookup is `NotFound`, no Feature Set Identity exists.
- If request validation fails before definition establishment, do not fabricate downstream identity.

Failure evidence must stop at the last established semantic fact.

## Integrity Contradiction

Freeze the principle:

If equal semantic fingerprints/identities are asserted for contradictory canonical semantic content, this is an integrity contradiction.

Do not silently select one value, overwrite, normalize away the contradiction, or treat it as ordinary equivalence.

Exact failure mapping remains WP07-owned.

## Semantic vs Operational Evidence

Semantic identity/evidence must exclude:

- wall-clock execution timestamp;
- elapsed duration;
- correlation ID;
- invocation ID;
- process ID;
- machine name;
- user name;
- current culture;
- current timezone;
- file/database path;
- connection string;
- provider credentials;
- logging scope;
- console formatting;
- retry count;
- scheduling information.

Operational logging may later reference semantic identities, but operational information must not determine them.

## Provider and Storage Independence

Feature identity/provenance/evidence semantics must not depend on:

- Twelve Data;
- HTTP;
- provider response shape;
- SQLite;
- SQL;
- database-generated keys;
- database path;
- connection ownership;
- persistence timing.

Infrastructure remains an implementation boundary.

Feature output remains in memory for Release 1.4.

## Release 1.3 Pipeline Protection

Do not alter:

- `aiq-pipeline-identity-v1`;
- pipeline definition identity;
- semantic pipeline execution identity;
- five-stage topology;
- pipeline evidence;
- pipeline dispositions;
- Worker pipeline semantics.

Feature Definition Identity and Feature Set Identity are separate Release 1.4 identities.

Do not make feature identity part of Release 1.3 pipeline identity.

## Schema and Persistence Protection

SQLite remains schema version `2`.

WP03 must not define or require:

- feature tables;
- feature catalog tables;
- feature run tables;
- feature cache;
- identity persistence;
- provenance persistence;
- schema v3;
- checkpoint state.

Identity/provenance/evidence are semantic contracts first.

Persistence is Release 1.5+ unless separately governed.

## Release 1.5+ Deferrals

Keep explicitly deferred:

- feature persistence and catalog;
- multiple indicators;
- configurable lag;
- arbitrary formulas;
- rolling indicators;
- plugins;
- feature DAGs;
- acquisition orchestration;
- scheduling;
- retries/circuit breakers/fallback;
- checkpoints/resume;
- durable execution history;
- metrics/tracing backends;
- notebooks/workspaces;
- strategies;
- backtesting;
- model training;
- ML/MLOps;
- distributed/streaming execution.

Do not create placeholders or abstractions solely for these future capabilities.

## Required Artifact Structure

`FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md` must contain at minimum:

1. Purpose
2. Authority and predecessor identities
3. Identity vocabulary
4. `aiq-feature-identity-v1`
5. Feature Definition Identity
6. Feature Set Identity
7. Acyclic identity derivation
8. Canonical representation
9. Canonical decimal semantics
10. Canonical timestamp/offset semantics
11. Feature Definition canonical content
12. Feature Set canonical content
13. Empty feature-set identity
14. Equivalent recomputation
15. Same values from different snapshots
16. Definition evolution/versioning rule
17. Provenance
18. Lineage
19. Successful semantic evidence
20. Failure semantic evidence
21. Evidence-established-only rule
22. Integrity contradiction
23. Semantic vs operational evidence
24. Provider/storage independence
25. Relationship to Release 1.3 pipeline
26. Schema/persistence decision
27. Explicit Release 1.5+ deferrals
28. Ownership and WP04+ handoff

Use repository terminology and distinguish implemented predecessor truth from Release 1.4 semantics.

## WP04 Protection

WP04 owns the next model boundary according to the authoritative Release 1.4 plan.

Do not implement or create:

- C# feature identity types;
- feature definition/value/set models;
- Application request/result contracts;
- validators;
- identity computer;
- computation use case;
- DI registrations;
- Worker behavior;
- tests.

Do not begin #156.

## WP05+ Protection

Do not implement contracts, computation, validation, integration, DI, Worker execution, permanent tests, architecture changes, or documentation alignment assigned to later work packages.

## Validation

Run canonical verification after creating the semantic artifact:

```powershell
./eng/verify.ps1 -Configuration Release
```

Expected accepted baseline after WP02:

- Domain.Tests: `11/11`;
- Application.Tests: `77/77`;
- Infrastructure.Tests: `96/96`;
- Architecture.Tests: `13/13`;
- permanent total: `197/197`;
- skipped: `0`;
- build warnings/errors: `0/0`;
- Gitleaks: PASS.

Also require:

```powershell
git diff --check
git diff --cached --check
```

And verify:

- new artifact trailing whitespace: `0`;
- generated/database residue: `0`;
- SQLite/WAL/SHM/journal residue: `0`;
- provider/network execution: `0`;
- real credentials: `0`;
- SQLite schema remains v2;
- production dependency graph unchanged.

If the repository baseline has legitimately changed since WP02, report actual truth rather than falsifying expected counts.

## Mutation Accounting

At completion, reconcile exact mutations.

Expected:

- exactly one new WP03 semantic document;
- production delta `0`;
- permanent-test delta `0`;
- architecture-test delta `0`;
- package/reference/schema delta `0/0/0`;
- Git transport mutations `0`.

No unrelated cleanup is authorized.

## GitHub Lifecycle

After all starting gates pass:

1. move issue #155 from Backlog → In Progress;
2. execute WP03;
3. validate all acceptance gates;
4. post concise completion evidence to #155;
5. close #155;
6. set Project #2 status to Done.

Read-only verify #156 remains Open/Backlog.

Do not mutate #156.

Milestone #45 must remain Open.

## Stop Conditions

Stop with:

`RELEASE 1.4 WP03 BLOCKED`

if any of the following occurs:

- WP02 is not Closed/Done;
- #155 is not Open/Backlog at start;
- WP04/#156 has already started;
- predecessor identity semantics materially conflict;
- feature identity would require changing dataset or pipeline identity;
- identity semantics would require feature persistence or schema v3;
- canonical decimal/timestamp semantics cannot be reconciled with predecessor fidelity;
- circular identity/provenance derivation is required;
- unauthorized production/test files would need mutation;
- canonical verification fails because of WP03;
- unexpected repository paths prevent exact mutation accounting.

Report the smallest corrective authority required.

Do not improvise around a blocker.

## Acceptance Matrix

WP03 passes only if all applicable gates pass:

- starting Git state valid;
- starting GitHub lifecycle valid;
- WP01/WP02 Closed/Done;
- WP04 remains unstarted;
- Release 1.2 dataset identity reconciled;
- Release 1.3 pipeline identity reconciled;
- `aiq-feature-identity-v1` frozen;
- Feature Definition Identity distinct from Feature Set Identity;
- SHA-256 / 64 lowercase hex requirement frozen;
- deterministic UTF-8 length-delimited canonical representation frozen;
- identity derivation acyclic;
- exact snapshot identity/version bound to Feature Set Identity;
- ordered feature evidence bound to Feature Set Identity;
- decimal semantics culture-independent;
- timestamp/offset fidelity preserved;
- empty feature sets retain deterministic identity;
- equivalent recomputation retains identity;
- equal values from different snapshots remain identity-distinct;
- operational invocation data excluded;
- provenance reuses predecessor evidence;
- lineage acyclic;
- evidence-established-only rule frozen;
- integrity contradiction remains distinct;
- provider/storage independence preserved;
- Release 1.3 pipeline identity/topology unchanged;
- SQLite remains v2;
- feature persistence absent;
- Release 1.5+ deferrals preserved;
- exactly one semantic artifact created;
- production/test/package/reference/schema delta `0/0/0/0/0`;
- canonical verification PASS;
- Gitleaks PASS;
- whitespace checks PASS;
- residue `0`;
- #155 Closed/Done;
- #156 Open/Backlog;
- milestone #45 Open.

## Required Final Report

Produce a concise but complete execution report containing:

1. executive summary;
2. authorities reviewed;
3. repository/Git baseline;
4. working-tree classification;
5. lifecycle gates;
6. initial validation baseline;
7. Release 1.2 identity reconciliation;
8. Release 1.3 identity reconciliation;
9. feature identity scheme;
10. Feature Definition Identity;
11. Feature Set Identity;
12. identity derivation graph;
13. canonical encoding;
14. decimal encoding semantics;
15. timestamp/offset encoding semantics;
16. empty feature-set identity;
17. equivalent recomputation;
18. same-value/different-snapshot distinction;
19. provenance;
20. lineage;
21. successful evidence;
22. failure evidence;
23. evidence-established-only behavior;
24. integrity contradiction;
25. semantic/operational boundary;
26. provider/storage independence;
27. Release 1.3 protection;
28. schema/persistence decision;
29. Release 1.5+ deferrals;
30. artifact created;
31. production/test/package/reference/schema deltas;
32. permanent test counts;
33. canonical verification;
34. architecture/security/offline/whitespace/residue evidence;
35. mutation accounting;
36. Git/GitHub protection;
37. final #155/#156 states;
38. findings/blockers;
39. final decision;
40. WP04 handoff.

Successful terminal marker:

`RELEASE 1.4 WP03 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP04 — Feature Domain/Application Model — GitHub issue #156`

If the authoritative execution plan uses a different exact WP04 title, use that exact repository-authoritative title in the final handoff instead of inventing one.

Do not start WP04.

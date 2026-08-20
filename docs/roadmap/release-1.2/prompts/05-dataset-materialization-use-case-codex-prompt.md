# Release 1.2 WP05 — Dataset Materialization Use Case — Codex Prompt

## Role

Act as the **WP05 Dataset Materialization Use Case Executor** for Release 1.2 of
`samuel-santos-engineer/AIQuantTradingResearch`.

Use **GPT-5.6 Terra** for this work package.

WP05 is a bounded **Application orchestration** package. The dataset definition,
identity/version/provenance semantics, and Application dataset contracts are
already accepted. Your task is to implement the smallest deterministic
Application use case that materializes a research dataset from accepted
Release 1.1 historical observations.

Do not redesign WP02–WP04 semantics. Do not implement snapshot persistence,
catalog persistence/modeling, SQLite dataset storage, DI/Worker execution,
final failure mapping, or permanent tests. Do not start WP06.

---

## 1. Mandatory Authorities

Read completely before acting:

```text
RELEASE_1.2_EXECUTION_PLAN.md
RELEASE_1.2_FILE_MANIFEST.md

release-1.2-github-planning-codex-prompt.md
release-1.2-github-planning-codex-prompt-chat.md

01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md

02-research-dataset-definition-reproducibility-model-codex-prompt.md
02-research-dataset-definition-reproducibility-model-codex-prompt-chat.md

03-dataset-identity-version-provenance-semantics-codex-prompt.md
03-dataset-identity-version-provenance-semantics-codex-prompt-chat.md

04-application-dataset-contracts-codex-prompt.md
04-application-dataset-contracts-codex-prompt-chat.md

05-dataset-materialization-use-case-codex-prompt.md
05-dataset-materialization-use-case-codex-prompt-chat.md

docs/architecture/data/RESEARCH_DATASET_DEFINITION.md
docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md
```

Also inspect completely:

```text
accepted WP01 execution result
accepted WP02 execution result
accepted WP03 execution result
accepted WP04 execution result

current Release 1.1 Application persistence contracts
current Release 1.1 historical-observation retrieval boundary
current Domain PriceObservation / observation semantics
current Application use-case conventions
current Application dataset-contract implementation from WP04
current Infrastructure implementation only as needed to understand the
  existing Application abstraction — do not couple WP05 to Infrastructure
current architecture and dependency documentation
current permanent tests and architecture tests

GitHub milestone #53
GitHub issue #125
GitHub issue #126
Project #2
```

Authority precedence:

```text
1. RELEASE_1.2_EXECUTION_PLAN.md
2. RELEASE_1.2_FILE_MANIFEST.md
3. Accepted Release 1.2 GitHub planning state
4. Accepted WP04 contracts/result
5. Accepted WP03 identity/version/provenance semantics
6. Accepted WP02 dataset/reproducibility semantics
7. Accepted WP01 baseline
8. Accepted Release 1.1 repository truth
9. Existing repository architecture/conventions
10. This execution prompt
```

If authorities materially conflict, stop and report the smallest precise
blocker. Never silently reinterpret an accepted decision.

---

## 2. Accepted Starting Baseline

Treat the accepted WP04 report as the expected starting state unless current
evidence proves legitimate drift:

```text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Branch: main
Accepted HEAD/origin at WP04 completion:
  3ae8ba300fcd356b71fb4fdef5258dc23a99abeb
Ahead/behind: 0/0
Staged paths: 0
Tracked modifications: 0

Release 1.2 lifecycle:
  #121: CLOSED / DONE
  #122: CLOSED / DONE
  #123: CLOSED / DONE
  #124: CLOSED / DONE
  #125: OPEN / BACKLOG
  #126: OPEN / BACKLOG
  milestone #53: OPEN
  WP05 dependency: exactly #124
  Release 1.3 implementation: NOT STARTED

Permanent baseline:
  Domain.Tests: 11/11
  Application.Tests: 42/42
  Infrastructure.Tests: 79/79
  Architecture.Tests: 13/13
  Total: 145/145
```

Accepted WP04 Application dataset seam:

```text
AIQuantTradingResearch.Application.Datasets

DatasetIdentity.cs
  DatasetDefinitionIdentity
  ResearchDatasetIdentity
  SourceStateIdentity
  DatasetSnapshotIdentity
  DatasetVersion
  identity scheme: aiq-dataset-identity-v1
  accepted fingerprint representation: 64 lowercase hexadecimal characters

DatasetDefinition.cs
  exact target
  explicit valid [from, to)
  deterministic semantic-instant ascending ordering

DatasetSnapshotCandidate.cs
  immutable candidate
  coverage
  provenance
  lineage
  source authority
  ordered PriceObservation values
  valid empty snapshots
  cross-contract consistency validation

DatasetContracts.cs
  bounded materialization result/failure vocabulary
  IDatasetSnapshotStore
  IDatasetCatalog
```

At WP04 completion the working tree contained 18 accepted cumulative untracked
paths. Recount and classify the actual current state. The number itself is not
authority.

---

## 3. Starting-State Gate

Before any mutation:

1. authenticate GitHub without exposing credentials;
2. verify repository identity;
3. verify current branch;
4. fetch/inspect remote state without changing history;
5. record local HEAD and `origin/main`;
6. verify ahead/behind;
7. enumerate staged, tracked-modified, and untracked paths;
8. classify every existing change against accepted Release 1.2 work;
9. verify #121–#124 are Closed/Done;
10. verify #125 is Open/Backlog;
11. verify #126 is Open/Backlog;
12. verify milestone #53 remains Open;
13. verify WP05 depends exactly on #124;
14. verify no WP06+ lifecycle has advanced;
15. verify Release 1.3 implementation remains unstarted;
16. verify all accepted WP02–WP04 artifacts exist;
17. verify the WP04 dataset contract files match accepted semantics;
18. verify no unauthorized dataset-materialization implementation already
    exists.

If unexplained drift exists, stop before mutation.

Do not move #125 to In Progress until the starting-state and initial technical
baseline gates both pass.

---

## 4. Initial Technical Baseline

Before production mutation, run the canonical repository validation, including
at minimum:

```text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
permanent tests
Architecture.Tests
eng/verify.ps1
git diff --check
git diff --cached --check
```

Use repository-owned scripts when more authoritative.

Record actual:

- warnings/errors;
- suite counts;
- total tests;
- architecture result;
- canonical verification;
- secret scan;
- whitespace status;
- temporary SQLite residue.

Expected permanent baseline is 145/145. If legitimate accepted repository
changes have altered it, report actual values and prove WP05 does not change
permanent tests.

Do not repair unrelated baseline failures.

After the gate passes, move issue #125 Backlog → In Progress.

---

## 5. WP05 Objective

Implement the smallest deterministic Application use case that:

```text
DatasetDefinition
        |
        v
accepted Release 1.1 historical observations
        |
        v
deterministic [from, to) selection
        |
        v
strict ascending semantic-instant ordering
        |
        v
source-state semantic representation
        |
        v
deterministic identities/version
        |
        v
coverage + provenance + lineage
        |
        v
DatasetSnapshotCandidate
        |
        v
DatasetMaterializationResult
```

WP05 owns **materialization**, not durable dataset persistence.

The successful output must be suitable for WP08 snapshot persistence and later
catalog integration without those behaviors occurring in WP05.

---

## 6. Core Architectural Boundary

The use case must live in:

```text
src/AIQuantTradingResearch.Application/**
```

Expected production deltas:

```text
Domain: 0
Application: minimal WP05 implementation
Infrastructure: 0
Worker: 0
```

The use case may depend on:

- Domain values;
- accepted Application dataset contracts;
- an existing Application-owned historical-observation abstraction from
  Release 1.1;
- a minimal Application-owned deterministic identity computation seam/helper
  only if needed to implement the already-settled WP03 identity semantics.

It must not depend on:

- Infrastructure;
- SQLite;
- `Microsoft.Data.Sqlite`;
- provider clients;
- HTTP;
- Worker;
- filesystem/database paths;
- wall-clock services;
- random-number services.

---

## 7. Source-of-Truth Boundary

Release 1.1 accepted historical observations are the authoritative source.

Inspect the existing Application boundary carefully.

Prefer reuse of the existing Application abstraction rather than introducing a
second observation repository/source abstraction.

The materialization use case must not:

- call Twelve Data;
- acquire new market data;
- invoke provider DTOs;
- use provider ordering;
- read SQLite directly;
- reference Infrastructure;
- mutate Release 1.1 historical observations.

It must consume accepted observations through the existing Application seam.

If the current Release 1.1 Application abstraction cannot express the exact
required bounded read without changing its contract, do not silently redesign
Release 1.1. Determine whether deterministic in-memory filtering over the
existing target-scoped retrieval is sufficient for Release 1.2.

Prefer bounded Application orchestration over broad predecessor-contract
mutation.

---

## 8. Dataset Definition Semantics

The input is the accepted WP04 `DatasetDefinition`.

Preserve exactly:

```text
single exact opaque target
[from, to)
from inclusive
to exclusive
semantic-instant comparison
deterministic ascending order
```

Do not:

- normalize target;
- change case;
- trim whitespace;
- reinterpret the target;
- widen/narrow boundaries;
- use local timezone;
- infer missing boundaries;
- introduce open-ended definitions;
- introduce multi-target datasets.

WP04 already validates the definition. WP05 may defensively enforce
cross-boundary invariants where required, but do not duplicate an independent
definition model.

---

## 9. Observation Selection

From the accepted target-scoped Release 1.1 history:

1. preserve only observations whose semantic instant satisfies:

```text
instant >= from
instant < to
```

2. ensure deterministic strict ascending semantic-instant ordering;
3. ensure no duplicate semantic instant appears in the selected materialization;
4. preserve each selected `PriceObservation` exactly.

Do not mutate or reconstruct values unnecessarily.

Fidelity must preserve:

- exact `DateTimeOffset` semantic instant;
- original offset representation;
- exact `decimal` value.

No floating-point conversion.
No rounding.
No timestamp truncation.
No offset normalization.

---

## 10. Successful Empty Materialization

A valid definition that selects zero accepted observations is a successful
materialization.

It must produce a valid `DatasetSnapshotCandidate` with:

```text
observation count = 0
non-null empty observations
valid requested coverage
no fabricated first observation
no fabricated last observation
deterministic zero-count Source State Identity
deterministic Snapshot Identity / Dataset Version
valid provenance
valid lineage
```

Do not map empty selection to:

- NotFound;
- InvalidData;
- failure;
- conflict.

---

## 11. Deterministic Identity Computation

WP03 already fixed the semantic identity model:

```text
scheme: aiq-dataset-identity-v1
canonical representation:
  versioned
  type-separated
  length-delimited
  culture-independent UTF-8
digest:
  SHA-256
output:
  64 lowercase hexadecimal characters
```

WP05 must implement only the minimum deterministic computation needed to
materialize the accepted WP04 identity values.

Do not redesign the scheme.

Do not substitute:

- JSON serialization whose formatting/property behavior becomes identity;
- `GetHashCode`;
- database IDs;
- GUIDs;
- timestamps;
- random values;
- machine/process values;
- culture-sensitive formatting.

### Type separation

Definition, logical dataset, source state, and snapshot identity inputs must be
type/domain separated exactly enough that equal raw component bytes from
different identity concepts cannot be silently interpreted as the same semantic
identity.

### Length delimiting

Variable-length semantic fields must be encoded unambiguously. Do not rely on
separator characters that may appear in target/parameter values.

### Culture independence

All canonical numeric/text representation involved in identity must be
culture-independent.

### Version inclusion

The semantic identity scheme/model version must be explicit in canonical
identity computation so future schemes cannot reinterpret v1 evidence.

---

## 12. Definition Identity

Compute `DatasetDefinitionIdentity` from the accepted definition semantics.

The identity input must cover exactly the WP03-authorized semantic facts,
including as applicable:

- exact target;
- `[from, to)` boundary semantic instants;
- selection semantics;
- deterministic ordering semantics;
- explicitly authorized definition parameters;
- semantic-model/identity-scheme version.

Do not include operational execution facts.

If WP04's definition surface has no additional parameters, do not invent any.

Equivalent definitions must produce equal Definition Identity.

Semantically different definitions must be distinguishable.

---

## 13. Research Dataset Identity

Compute the logical `ResearchDatasetIdentity` according to WP03:

- stable for the same semantic dataset definition;
- distinct concept from a snapshot;
- not a mutable "latest" pointer;
- not derived from operational execution.

Do not let source-state changes silently redefine the logical dataset identity
if WP03 says the logical identity is definition-stable.

Use the accepted semantic relationship rather than inventing a new formula.

---

## 14. Source State Identity

Compute `SourceStateIdentity` from the **relevant selected source state**, not
from unrelated database history.

It must cover:

- exact target;
- selected observation count;
- selected observations in deterministic ascending semantic-instant order;
- each selected observation's semantic instant;
- original offset representation;
- exact decimal value.

An empty selected state must have a deterministic valid Source State Identity.

Do not include:

- database row IDs;
- insertion order;
- database path;
- source query timing;
- provider name/order unless explicitly part of accepted semantic source
  authority;
- unrelated observations outside the definition.

Equivalent relevant source state must yield equal Source State Identity.

Any relevant membership/value/offset change must be distinguishable.

---

## 15. Snapshot Identity and Dataset Version

Compute `DatasetSnapshotIdentity` from the accepted typed relationship between:

```text
DatasetDefinitionIdentity
SourceStateIdentity
```

and any other semantic component explicitly required by WP03.

Do not introduce operational metadata.

`DatasetVersion` must preserve WP04/WP03 semantics:

```text
Dataset Version == immutable deterministic Snapshot Identity
```

Equivalent re-materialization must therefore yield the same semantic Snapshot
Identity and Dataset Version.

---

## 16. Collision / Contradiction Boundary

SHA-256 is compact identity evidence, not proof that contradictory content is
impossible.

WP05 must not:

- claim collision impossibility;
- overwrite evidence;
- silently alias contradictory semantic content;
- use digest equality to justify mutation.

The existing WP04 contracts must remain capable of representing integrity
conflict for later persistence/catalog work.

WP05 itself normally materializes one candidate from one source state and
should not invent a conflict merely because a store/catalog is not consulted.

Do not call `IDatasetSnapshotStore` or `IDatasetCatalog` just to manufacture a
collision check. Persistence/catalog consistency belongs to later WPs.

---

## 17. Coverage Construction

Construct the accepted WP04 coverage representation deterministically.

It must preserve:

- requested `from`;
- requested `to`;
- selected count;
- successful empty semantics;
- actual first selected semantic instant when non-empty and required by the
  WP04 contract;
- actual last selected semantic instant when non-empty and required by the
  WP04 contract.

Do not confuse requested interval with observed data coverage.

Do not calculate:

- quality score;
- expected candle count;
- market-calendar completeness;
- gap repair;
- interpolation;
- resampling.

---

## 18. Provenance Construction

Construct WP04 `DatasetProvenance` using only accepted semantic facts.

It must explain, as represented by the accepted contract:

- accepted Release 1.1 historical observations as source authority;
- definition;
- Definition Identity;
- Research Dataset Identity;
- Source State Identity;
- Snapshot Identity / Dataset Version;
- exact target;
- requested boundaries;
- selected count;
- identity scheme;
- materialized result.

Do not add operational identity inputs such as:

- current time;
- host/machine;
- process;
- user;
- path;
- connection string;
- random execution ID.

If an operational materialization timestamp is not already part of the accepted
WP04 semantic contract, do not add one.

---

## 19. Lineage Construction

Construct the narrow accepted lineage:

```text
snapshot
  -> one definition
  -> one relevant source state
  -> zero or more selected accepted observations
```

Do not introduce:

- parent dataset graphs;
- transformation DAGs;
- pipeline stage IDs;
- feature lineage;
- scheduling;
- refresh chains.

Release 1.3 owns pipeline behavior.

---

## 20. Materialization Use-Case API

Follow existing Application use-case conventions.

Create the minimum clear surface, expected conceptually to resemble:

```text
IMaterializeDatasetUseCase
MaterializeDatasetRequest   (only if DatasetDefinition alone is insufficient)
MaterializeDatasetUseCase
```

Do not force these exact names if repository conventions or the manifest define
different authoritative names.

Prefer using `DatasetDefinition` directly when a wrapper request adds no
semantic value.

Constructor dependencies must be minimal.

The use case must not depend on:

```text
IDatasetSnapshotStore
IDatasetCatalog
IServiceProvider
IConfiguration
ILogger (unless current Application use-case convention makes logging an
  already-established dependency and it remains semantically irrelevant)
Infrastructure concrete types
```

Snapshot persistence/catalog registration is not WP05.

---

## 21. Materialization Result / Failure Handling

Return the accepted WP04 `DatasetMaterializationResult` and failure vocabulary
without redesigning it.

Map only failures that belong at the Application materialization boundary.

Examples to reconcile against actual WP04 contracts:

```text
invalid definition/request
source history unavailable
source history invalid/inconsistent
integrity/semantic contradiction
```

Do not invent a broad WP11 failure taxonomy.

If Release 1.1 retrieval returns an existing `PersistenceFailure`, translate it
at the dataset Application boundary only as necessary; do not expose storage
technology.

Preserve:

- success with empty candidate;
- success with non-empty candidate;
- no null-success ambiguity;
- no partial successful snapshot after a failed source read.

---

## 22. Snapshot Store Protection

`IDatasetSnapshotStore` exists for later work.

WP05 must **not** invoke it.

Specifically, WP05 must not:

- persist a snapshot;
- detect persisted duplicates;
- retrieve persisted snapshots;
- overwrite snapshots;
- create storage transactions;
- interpret physical persistence errors.

WP08 owns snapshot persistence.

---

## 23. Catalog Protection

`IDatasetCatalog` exists as a later seam.

WP05 must **not** invoke it.

Do not:

- register the materialized snapshot;
- query catalog state;
- implement "latest";
- implement catalog search;
- create metadata records;
- perform catalog consistency checks.

WP06 owns catalog metadata/model semantics.
WP09 owns catalog persistence/lookup.
WP10 owns materialization integration across these capabilities.

---

## 24. WP06 Protection

WP06 — Dataset Metadata & Catalog Model — must remain unstarted.

WP05 may populate only the provenance/coverage/lineage values already accepted
by WP04.

Do not add speculative catalog metadata such as:

- title;
- description;
- tags;
- status;
- owner;
- creation timestamp;
- update timestamp;
- physical location;
- storage format;
- arbitrary metadata dictionary;
- search facets.

If materialization needs a fact not representable by WP04 and that fact is
clearly catalog metadata, stop rather than stealing WP06 scope.

---

## 25. Domain Delta

Expected:

```text
Domain production delta: 0
```

Use existing Domain `PriceObservation` and semantic-instant behavior.

Do not move dataset orchestration into Domain.

If a missing invariant genuinely requires Domain ownership, stop and report the
smallest authority gap.

---

## 26. Infrastructure / Worker Delta

Expected:

```text
Infrastructure production delta: 0
Worker production delta: 0
```

No:

- SQLite;
- SQL;
- schema;
- files;
- database;
- provider;
- HTTP;
- DI registration;
- configuration binding;
- Worker invocation.

---

## 27. Package / Reference Delta

Expected:

```text
new package delta: 0
package-version delta: 0
project-reference delta: 0
```

Use .NET platform cryptography APIs if deterministic SHA-256 computation is
needed; do not add a hashing package.

Any new package/reference requirement is a blocker.

---

## 28. Permanent Test Delta

Expected:

```text
permanent test delta: 0
```

WP13 owns Domain/Application dataset tests.

You may create a temporary offline Application-focused probe only if necessary
to validate difficult deterministic identity/materialization behavior before
WP13.

Any temporary probe must:

- use no network/provider;
- use no SQLite/database;
- use synthetic observations;
- contain no secret;
- be removed completely before final validation.

Prefer compile/build plus a narrowly scoped disposable probe rather than
modifying permanent tests.

---

## 29. Determinism Proof

Before acceptance, explicitly prove at least these cases using static reasoning
and, where useful, a temporary offline probe:

1. same definition + same relevant observations → same:
   - Definition Identity;
   - Research Dataset Identity;
   - Source State Identity;
   - Snapshot Identity;
   - Dataset Version;
   - ordered content;
   - semantic provenance/lineage;
2. same semantic inputs under a different current culture → same identities;
3. same observations supplied/retrieved in a different non-authoritative input
   order → same deterministic materialization if the existing source contract
   permits such input;
4. observation outside `[from, to)` → excluded and does not affect relevant
   Source State Identity;
5. selected price change → Source State/Snapshot identity changes;
6. selected original offset representation change at the same semantic instant
   → Source State/Snapshot identity changes;
7. selected membership change → Source State/Snapshot identity changes;
8. definition boundary/target change → Definition/Research Dataset/Snapshot
   identity changes as semantically required;
9. valid zero-row selection → deterministic successful empty candidate;
10. no wall-clock/random/machine/path/database/provider-order input affects
    semantic identity.

Do not weaken accepted semantics merely to simplify the implementation.

---

## 30. Canonicalization Inspection Gate

Because identity computation is high-impact, inspect the implementation
line-by-line before final acceptance.

Prove:

- UTF-8 explicitly used;
- type/domain separation explicit;
- length delimiting unambiguous;
- invariant/culture-independent representation;
- scheme/version explicit;
- target preserved exactly;
- semantic instants represented deterministically;
- original offset included where required;
- decimal represented exactly and invariantly;
- no floating-point intermediary;
- SHA-256 used;
- lowercase 64-hex output;
- no `GetHashCode`;
- no JSON serializer as canonical identity authority;
- no current-time/random inputs;
- no filesystem/database/provider transport inputs.

If any of these are ambiguous, WP05 is not complete.

---

## 31. Application Architecture Gate

After implementation prove the production graph remains:

```text
Domain -> none
Application -> Domain
Infrastructure -> Application
Worker -> Application, Infrastructure
```

Require:

```text
Application -> Infrastructure: 0
Application SQLite references: 0
Application provider/HTTP mechanics: 0
new dependency cycles: 0
```

Run all Architecture.Tests.

---

## 32. Security / Offline Gate

WP05 execution must remain offline except for GitHub lifecycle inspection.

Implementation/probes must use:

```text
provider calls: 0
market-data network calls: 0
live credentials: 0
database calls: 0
database files: 0
connection strings: 0
machine-specific paths in production: 0
secret material: 0
```

Run canonical secret scanning when included in `eng/verify.ps1`.

---

## 33. Whitespace / Artifact Integrity

Run:

```text
git diff --check
git diff --cached --check
```

Accepted Release 1.2 files may remain untracked, so directly validate every
WP05-created untracked file for trailing whitespace and repository final-line
conventions.

Whitespace-only findings inside WP05-created files may be corrected directly
and revalidated. Do not create another authority chain for such corrections.

Do not normalize unrelated accepted files.

---

## 34. Semantic Acceptance Matrix

Explicitly report each row:

| Requirement | Required |
|---|---|
| WP02 semantics preserved | PASS |
| WP03 semantics preserved | PASS |
| WP04 contracts reused | PASS |
| Existing Release 1.1 Application source boundary reused | PASS |
| Exact target | PASS |
| `[from, to)` | PASS |
| Strict ascending materialization | PASS |
| Successful empty materialization | PASS |
| Timestamp semantic instant fidelity | PASS |
| Original offset fidelity | PASS |
| Decimal fidelity | PASS |
| Definition Identity deterministic | PASS |
| Research Dataset Identity deterministic | PASS |
| Source State Identity deterministic | PASS |
| Snapshot Identity deterministic | PASS |
| Dataset Version semantics preserved | PASS |
| Equivalent re-materialization deterministic | PASS |
| Relevant source changes distinguishable | PASS |
| Empty source-state identity deterministic | PASS |
| Canonical v1 representation | PASS |
| SHA-256 / 64 lowercase hex | PASS |
| Culture independence | PASS |
| Type separation / length delimiting | PASS |
| Operational metadata excluded | PASS |
| Coverage deterministic | PASS |
| Provenance deterministic | PASS |
| Lineage deterministic | PASS |
| Snapshot persistence invoked | NO |
| Catalog invoked | NO |
| SQLite/SQL/filesystem leakage | 0 |
| Provider/HTTP leakage | 0 |
| Domain delta | 0 |
| Infrastructure delta | 0 |
| Worker delta | 0 |
| Permanent test delta | 0 |
| Package/reference delta | 0/0 |
| WP06 started | NO |
| Release 1.3 started | NO |

---

## 35. Final Technical Validation

After implementation and after removing all temporary probes/residue, rerun:

```text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
all permanent tests
Architecture.Tests
eng/verify.ps1
git diff --check
git diff --cached --check
```

Expected unchanged permanent counts:

```text
Domain.Tests: 11/11
Application.Tests: 42/42
Infrastructure.Tests: 79/79
Architecture.Tests: 13/13
Total: 145/145
```

Require:

```text
warnings: 0
errors: 0
failed tests: 0
skipped tests: 0
temporary database residue: 0
temporary probe residue: 0
```

If accepted baseline counts have legitimately changed, use actual evidence and
prove WP05's permanent test delta remains zero.

---

## 36. Exact Mutation Accounting

At finalization enumerate:

- Application files added;
- Application files modified;
- Application files deleted;
- Domain delta;
- Infrastructure delta;
- Worker delta;
- permanent tests changed;
- packages changed;
- project references changed;
- solution/build/scripts changed;
- temporary probes created/removed;
- generated/temp files created/removed;
- unexpected paths.

Every WP05 production mutation must map directly to deterministic
materialization.

Do not stage the candidate.

---

## 37. Git / GitHub Protection

Do not:

```text
git add
git commit
git push
create branch
create PR
merge
tag
release
reset
rebase
rewrite history
```

GitHub mutations are limited to issue #125 lifecycle/evidence after gates pass.

Do not alter:

- #126 or later issues;
- dependencies;
- milestone #53 definition;
- Project field schema/options;
- Release 1.1 objects;
- Release 1.3 objects.

---

## 38. Issue Lifecycle

Only after starting-state and baseline validation:

```text
#125 Backlog -> In Progress
```

Only after every acceptance gate passes:

1. post concise completion evidence to #125;
2. close #125;
3. set #125 Project status to Done;
4. verify #126 remains Open/Backlog;
5. leave milestone #53 Open;
6. do not start WP06.

If blocked after moving #125 In Progress, report the blocker and leave the issue
truthful. Never close it on partial completion.

---

## 39. WP06 Handoff

The execution report must tell WP06 exactly what materialization now produces.

Include:

- exact materialization use-case/interface names;
- constructor dependencies;
- exact source-history abstraction reused;
- exact input;
- exact success result;
- exact failure vocabulary;
- selection/filtering behavior;
- ordering behavior;
- empty behavior;
- identity computation ownership/location;
- canonicalization algorithm shape;
- coverage fields populated;
- provenance fields populated;
- lineage fields populated;
- what metadata remains intentionally undefined for WP06;
- confirmation snapshot store/catalog were not invoked;
- confirmation no physical storage design was introduced.

WP06 must be able to design metadata/catalog semantics from concrete
materialization output without guessing.

---

## 40. Required Execution Report

Produce a structured report with at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor / Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Existing Application Convention Reconciliation
10. Release 1.1 Source-Boundary Reconciliation
11. WP02 Semantic Reconciliation
12. WP03 Identity / Version / Provenance Reconciliation
13. WP04 Contract Reconciliation
14. Materialization Use-Case Design
15. Input / Definition Handling
16. Source Retrieval Design
17. Selection Semantics
18. Ordering Semantics
19. Empty Materialization
20. Definition Identity Computation
21. Research Dataset Identity Computation
22. Source State Identity Computation
23. Snapshot Identity / Dataset Version Computation
24. Canonical Representation
25. Coverage Construction
26. Provenance Construction
27. Lineage Construction
28. Result / Failure Handling
29. Snapshot Store Protection
30. Catalog Protection
31. Exact Files Added / Modified
32. Domain / Infrastructure / Worker Delta
33. Package / Reference Delta
34. Permanent Test / Temporary Probe Delta
35. Determinism Evidence
36. Security / Offline Evidence
37. Whitespace / Diff Evidence
38. Restore / Build Evidence
39. Permanent Test Evidence
40. Canonical Verification
41. Architecture Validation
42. Semantic Acceptance Matrix
43. Mutation Accounting
44. Git / GitHub Protection
45. Planning Protection
46. Findings / Blockers
47. Final Repository / GitHub State
48. WP06 Handoff
49. Final Decision
50. Next Authorized Work Package

Use observed facts, not expected values presented as observations.

---

## 41. Required Terminal Summary

If successful, end exactly with:

```text
RELEASE 1.2 WP05 COMPLETE

DATASET MATERIALIZATION USE CASE:
WP02 dataset semantics preserved: PASS
WP03 identity/version/provenance semantics preserved: PASS
WP04 contracts reused: PASS
Release 1.1 historical observations reused as source truth: PASS
Application orchestration: PASS
Exact target selection: PASS
[from, to) selection: PASS
Deterministic ascending ordering: PASS
Successful empty materialization: PASS
Timestamp/offset/decimal fidelity: PASS
Definition Identity computation: PASS
Research Dataset Identity computation: PASS
Source State Identity computation: PASS
Snapshot Identity computation: PASS
Dataset Version semantics: PASS
Canonical identity representation: PASS
SHA-256 / 64 lowercase hex: PASS
Equivalent re-materialization determinism: PASS
Relevant source-change distinguishability: PASS
Coverage construction: PASS
Provenance construction: PASS
Lineage construction: PASS
Snapshot persistence invoked: NO
Catalog invoked: NO
SQLite/SQL/filesystem leakage: 0
Provider/HTTP leakage: 0
Domain delta: 0
Infrastructure delta: 0
Worker delta: 0
Permanent test delta: 0
Package/reference delta: 0/0
WP06 started: NO
Release 1.3 implementation started: NO
Issue #125: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP06 — Dataset Metadata & Catalog Model
GitHub issue #126
```

If blocked, end exactly with:

```text
RELEASE 1.2 WP05 BLOCKED
```

Do not emit the success marker unless all mandatory gates pass.

---

## 42. Final Constraint

WP05 is where accepted dataset semantics become **deterministic Application
behavior**, but it is not where a dataset becomes durable.

The use case must produce the same semantic snapshot candidate whenever the
same Dataset Definition is materialized against the same relevant accepted
Release 1.1 source state.

The implementation must make identity computation explicit, deterministic,
culture-independent, and faithful to WP03 while keeping provider mechanics,
SQLite, catalog persistence, snapshot persistence, Worker execution, and
Release 1.3 pipelines completely outside the boundary.

**Materialize meaning; do not persist it yet.**

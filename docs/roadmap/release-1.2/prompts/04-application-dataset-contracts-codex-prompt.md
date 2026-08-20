# Release 1.2 WP04 — Application Dataset Contracts — Codex Prompt

## Role

Act as the **WP04 Application Dataset Contracts Executor** for Release 1.2 of
`AIQuantTradingResearch`.

This is a bounded **Application contract engineering** work package. Its purpose
is to translate the accepted WP02 dataset/reproducibility model and the accepted
WP03 identity/version/provenance semantics into the smallest precise,
provider-independent and storage-independent Application contract surface needed
by WP05–WP14.

Use **GPT-5.6 Terra** for this work package.

The foundational semantic decisions are already settled. Do not reopen them.
Do not implement materialization orchestration, catalog modeling, SQLite
persistence, Worker execution, or permanent tests. Do not start WP05.

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
docs/architecture/data/RESEARCH_DATASET_DEFINITION.md
docs/architecture/data/DATASET_IDENTITY_VERSION_PROVENANCE.md
```

Also inspect:

```text
accepted WP01 execution result
accepted WP02 execution result
accepted WP03 execution result
accepted Release 1.1 persistence contracts and semantics
current Domain/Application/Infrastructure/Worker source
current Application contract and use-case conventions
current dependency-injection conventions
current data architecture, catalog, glossary, versioning, and public-contract documentation
current permanent tests and architecture tests
GitHub milestone #53
GitHub issue #124
GitHub issue #125
GitHub issue #126
Project #2
```

Authority precedence:

```text
1. RELEASE_1.2_EXECUTION_PLAN.md
2. RELEASE_1.2_FILE_MANIFEST.md
3. Accepted Release 1.2 GitHub planning state
4. Accepted WP03 result and DATASET_IDENTITY_VERSION_PROVENANCE.md
5. Accepted WP02 result and RESEARCH_DATASET_DEFINITION.md
6. Accepted WP01 result
7. Accepted Release 1.1 repository truth
8. Existing repository architecture/governance conventions
9. This execution prompt
```

If authorities materially conflict, stop and report the smallest precise
blocker. Do not silently reinterpret an accepted semantic decision.

---

## 2. Accepted Starting Baseline

Treat the accepted WP03 result as the starting baseline unless current evidence
proves drift:

```text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Branch: main
WP03 accepted HEAD: 3ae8ba300fcd356b71fb4fdef5258dc23a99abeb
local main = origin/main at WP03 completion
ahead/behind: 0/0
staged paths: 0
tracked modifications: 0

Release 1.2:
  milestone #53: OPEN
  issue #121: CLOSED / DONE
  issue #122: CLOSED / DONE
  issue #123: CLOSED / DONE
  issue #124: OPEN / BACKLOG
  issue #125: OPEN / BACKLOG
  issue #126: OPEN / BACKLOG
  WP04 dependency: exactly #123
  Release 1.3 implementation: NOT STARTED

Permanent baseline:
  Domain.Tests: 11/11
  Application.Tests: 42/42
  Infrastructure.Tests: 79/79
  Architecture.Tests: 13/13
  Total: 145/145
```

At WP03 completion, accepted cumulative untracked Release 1.2
governance/semantic files totaled 12 and no unexpected change existed. Recount
and classify the actual current working tree; do not assume the count remains
12.

Do not treat accepted uncommitted Release 1.2 artifacts as drift merely because
they are untracked. Conversely, do not automatically bless a path merely
because it resembles Release 1.2 work.

---

## 3. Starting-State Gate — Mandatory Before Mutation

Before editing production code:

1. authenticate GitHub without exposing credentials;
2. verify repository identity;
3. verify current branch and exact local/remote SHAs;
4. verify ahead/behind;
5. verify staged, tracked-modified, and untracked paths;
6. classify every existing change against accepted cumulative Release 1.2 work;
7. verify #121–#123 are Closed/Done;
8. verify #124 is Open/Backlog;
9. verify #125 remains Open/Backlog;
10. verify #126 remains Open/Backlog;
11. verify milestone #53 remains Open;
12. verify WP04 depends exactly on #123;
13. verify no WP05+ lifecycle was advanced;
14. verify no Release 1.3 implementation has started;
15. verify the accepted WP02 and WP03 semantic artifacts are present;
16. verify no existing Application dataset-contract implementation has appeared
    outside accepted authority.

If unexplained drift exists, stop before mutation and report it.

Do not move #124 to In Progress until the starting-state and baseline validation
gates pass.

---

## 4. Initial Technical Baseline — Mandatory

Before WP04 production mutation, run the repository's canonical baseline,
including at minimum:

```text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
dotnet test / canonical permanent test execution
eng/verify.ps1
git diff --check
git diff --cached --check
```

Use repository-owned scripts/conventions when they are more authoritative than
the illustrative commands above.

Record exact:

- build warnings/errors;
- Domain/Application/Infrastructure/Architecture counts;
- total permanent tests;
- canonical verification result;
- secret-scan result if part of canonical verification;
- architecture-test result;
- temporary database residue;
- whitespace status.

Baseline failure is a blocker unless it is clearly caused only by already
accepted uncommitted Release 1.2 authority artifacts and can be demonstrated as
non-semantic. Do not fix unrelated failures under WP04 authority.

After the baseline passes, move issue #124 from Backlog to In Progress.

---

## 5. WP02 Semantic Contract — Preserve Exactly

WP04 must preserve the accepted WP02 model:

```text
Research Dataset
  immutable, research-owned deterministic materialization of explicitly
  selected accepted observations plus explanatory semantic metadata

Dataset Definition
  exact single target
  required [from, to) semantic-instant boundaries
  explicit inclusion/selection rules
  deterministic ordering
  explicitly authorized parameters

Materialization
  deterministic resolution of a definition against a fixed relevant
  source-observation state

Snapshot
  durable immutable evidence from one successful materialization

Selection
  exact target
  [from, to)
  ascending semantic instant

Empty result
  valid and successful

Fidelity
  original DateTimeOffset offset representation
  exact decimal value
  no conversion, rounding, or truncation
```

Do not change single-target scope into multi-target scope.
Do not change `[from, to)` semantics.
Do not make provider/database ordering authoritative.
Do not make empty materialization a failure.

---

## 6. WP03 Semantic Contract — Preserve Exactly

WP03 settled the foundational semantics. WP04 translates them into Application
contracts; it does not redesign them.

Preserve:

```text
Identity layers:
  Dataset Definition Identity
  logical Research Dataset Identity
  Source State Identity
  Dataset Snapshot Identity

Dataset Version:
  immutable deterministic Snapshot Identity
  not a timestamp
  not a database-generated sequence

Equivalent re-materialization:
  same semantic identities/version when definition and relevant source state
  are equivalent
  operational execution may still be a distinct event

Relevant change:
  selected membership/content, original offset, decimal value, or definition
  semantics change => distinguishable semantic identity

Operational metadata:
  excluded from semantic identity

Identity scheme:
  aiq-dataset-identity-v1

Digest:
  SHA-256
  64 lowercase hexadecimal characters

Canonical semantic representation:
  versioned
  type-separated
  length-delimited
  culture-independent UTF-8

Collision/integrity:
  contradictory content under equal fingerprint is an integrity conflict
  never overwrite or silently alias

Empty materialization:
  deterministic zero-count Source State Identity and Snapshot Identity

Immutability:
  accepted snapshot/version identity cannot be reassigned
  prior snapshots cannot be overwritten

Provenance:
  explains definition, source authority/state, identities, target,
  boundaries, count, scheme, and materialized result

Lineage:
  one snapshot -> one definition + one relevant source state
  source state contains zero or more accepted observations
```

### Important separation

The Application layer may expose the **semantic identity scheme name and
validated identity/fingerprint values** because they are part of the accepted
contract.

Do **not** make Application contracts responsible for:

- invoking SHA-256;
- byte-level canonical serialization;
- selecting cryptographic libraries;
- SQLite representation;
- persistence encoding;
- database-generated identifiers.

If a minimal Application value must validate the accepted external form, it may
validate invariants such as scheme name and 64-character lowercase hexadecimal
shape. Hash computation mechanics remain outside this WP unless an existing
Application convention makes a pure deterministic helper unavoidable. Prefer
values/contracts over hashing implementation.

---

## 7. Objective

Create the **minimum coherent Application seam** that allows later work to:

1. express a deterministic dataset definition/materialization request;
2. carry typed semantic identities and Dataset Version;
3. describe a materialized snapshot without storage technology;
4. carry coverage, provenance, and lineage facts required by WP03;
5. represent a snapshot candidate/result needed by WP05;
6. abstract immutable snapshot persistence needed by WP08;
7. abstract catalog registration and deterministic lookup needed by WP09;
8. represent bounded failures/outcomes without storage/provider leakage.

The result must be sufficient for later work but no broader.

Avoid speculative abstractions and generic repository/CRUD patterns.

---

## 8. Authorized Production Surface

Per the authoritative file manifest, WP04 may modify only:

```text
src/AIQuantTradingResearch.Application/**
```

Authorized classes/concepts include only what is required for the minimum
contract surface:

```text
dataset definition / materialization request
snapshot / descriptor / snapshot candidate
identity / version supporting values where Domain does not already own them
coverage
provenance
lineage
materialization result / failure
snapshot-store abstraction
catalog registration / lookup abstraction
deterministic lookup criteria
minimal supporting Application values
```

Before adding any type, inspect whether Domain or existing Application code
already owns the semantic value.

Prefer focused namespaces and capability-oriented names consistent with the
repository.

---

## 9. Strictly Prohibited Scope

WP04 must not change or add:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
tests/**
Directory.Packages.props
project references
solution files
eng/**
GitHub Actions
SQLite schema/model/bootstrap
SQL
ORM mappings
filesystem/database paths
database files
catalog persistence implementation
snapshot persistence implementation
materialization orchestration
historical-observation acquisition orchestration
Worker execution
scheduler/DAG/streaming/refresh behavior
retry/resilience policy
Release 1.3 pipeline behavior
```

Also prohibited:

- provider DTOs or HTTP types in Application contracts;
- SQLite or `Microsoft.Data.Sqlite` types;
- storage exceptions;
- physical table/column/index concepts;
- database-generated semantic IDs;
- generic `IRepository<T>`;
- generic CRUD abstractions;
- mutable "latest dataset" semantics;
- automatic refresh semantics;
- permanent tests.

If a required contract cannot compile without changing a prohibited surface,
stop and report the smallest corrective authority needed.

---

## 10. Contract Design Principles

Use the current repository's Application conventions unless they conflict with
accepted WP02/WP03 semantics.

The contract surface must be:

- provider-independent;
- storage-independent;
- deterministic;
- explicit rather than magic-string-heavy where semantic values exist;
- immutable/read-only at public boundaries;
- minimal;
- focused on Release 1.2;
- synchronous if current Application boundaries remain synchronous;
- free of `CancellationToken` unless current accepted Application conventions
  already justify it;
- free of implementation-side effects.

Do not introduce async merely because persistence may later be I/O-bound.
WP04 owns contracts within the current architecture, not a broad concurrency
redesign.

---

## 11. Dataset Definition / Request Contract

Create the minimum Application representation needed to request deterministic
materialization.

It must preserve:

- exact opaque target;
- inclusive `from` semantic instant;
- exclusive `to` semantic instant;
- valid `[from, to)` ordering;
- deterministic selection semantics;
- deterministic ascending ordering rule;
- explicitly authorized semantic parameters, if any are already defined by
  WP02/WP03.

Do not trim, uppercase, lowercase, parse, alias, or otherwise normalize the
target.

Do not silently substitute local time or UTC for the original semantic values.
Boundary instants may be compared by semantic instant, but their contract
meaning must remain explicit.

Reject invalid definitions deterministically at the appropriate Application
boundary. Do not invent storage failures for definition validation.

Do not add optional parameters merely for extensibility.

---

## 12. Identity Contract Surface

Represent the four accepted identity concepts so they cannot be casually
confused:

```text
Dataset Definition Identity
Research Dataset Identity
Source State Identity
Dataset Snapshot Identity
```

Prefer distinct strongly typed Application values rather than four unqualified
strings.

Each identity value must:

- be immutable;
- carry or unambiguously associate with the accepted scheme
  `aiq-dataset-identity-v1`;
- preserve the accepted 64-lowercase-hex fingerprint representation;
- reject structurally invalid values deterministically;
- have value semantics suitable for equality;
- not generate itself from time/randomness/database state.

Do not make the four identity types implicitly interchangeable.

---

## 13. Dataset Version Contract

Dataset Version must represent the accepted WP03 meaning:

```text
Dataset Version == immutable deterministic Dataset Snapshot Identity
```

Do not create:

- an integer version counter;
- a timestamp version;
- an auto-incrementing version;
- a mutable revision;
- "latest" as a semantic version.

The Application contract may use a distinct `DatasetVersion` value for clarity,
but it must preserve an explicit one-to-one semantic relationship with Snapshot
Identity and must not introduce an independent identity source.

If a distinct wrapper adds no clarity in the actual repository, document and
use the smallest representation that still makes version semantics explicit.

---

## 14. Snapshot / Candidate / Descriptor Contract

WP05 needs an Application-owned representation of the deterministic result
before Infrastructure persistence exists.

Design the minimum immutable snapshot candidate/descriptor surface that can
carry:

- Dataset Definition;
- Definition Identity;
- Research Dataset Identity;
- Source State Identity;
- Snapshot Identity / Dataset Version;
- exact target;
- selection boundaries;
- ordered observations;
- coverage;
- provenance;
- lineage;
- count.

Use existing Domain `PriceObservation` values where appropriate. Do not invent
Application transport DTOs for timestamp/price merely to avoid using Domain
values.

Observation collection semantics:

- non-null;
- read-only from the contract consumer's perspective;
- strictly ascending semantic instant;
- no duplicate semantic instant;
- exact timestamp/offset/decimal fidelity;
- empty collection valid.

Do not create a persisted-record type in Application.

---

## 15. Coverage Contract

Define only semantic coverage needed to explain the materialized snapshot.

Coverage must be able to explain at least:

- requested `[from, to)` boundaries;
- observation count;
- successful empty materialization;
- when non-empty, the actual first and last selected semantic instants if
  required to explain observed coverage.

Do not infer that requested boundaries equal observed data coverage.
Do not fabricate a first/last observation for an empty snapshot.

Do not add quality scoring, gap repair, completeness percentages, market
calendar logic, or data-quality pipelines. Those are outside WP04.

---

## 16. Provenance Contract

The Application provenance representation must be sufficient to preserve the
accepted WP03 facts without storage mechanics.

It must be able to explain:

- source authority: accepted Release 1.1 historical observations;
- Dataset Definition / Definition Identity;
- relevant Source State / Source State Identity;
- Research Dataset Identity;
- Snapshot Identity / Dataset Version;
- exact target;
- `[from, to)` selection;
- selected observation count;
- identity scheme;
- materialized snapshot/result.

Do not include operational metadata in semantic identity.

Do not require:

- machine name;
- process ID;
- wall-clock execution time;
- filesystem path;
- database path;
- connection string;
- provider response ordering;
- random execution identifier.

If operational evidence already exists elsewhere, keep it separate from
semantic provenance.

---

## 17. Lineage Contract

Represent the accepted lineage relationship explicitly:

```text
one snapshot
  -> one dataset definition
  -> one relevant source state
  -> zero or more accepted PriceObservation values
```

The lineage contract must not imply:

- multiple parent datasets;
- DAG execution;
- transformations/features;
- pipeline stages;
- scheduled refresh;
- streaming lineage.

Release 1.2 lineage is intentionally narrow.

---

## 18. Materialization Result / Failure Boundary

WP04 may define the result/failure vocabulary needed by WP05, but must not
implement materialization.

Separate:

- invalid Application request/definition;
- successful materialization, including successful empty materialization;
- semantic/integrity conflict where accepted identity/content facts
  contradict;
- source-history/storage availability failures only if the Application
  boundary genuinely needs to express them for later orchestration.

Do not leak Release 1.1 `PersistenceFailure` mechanically into dataset contracts
unless semantic reconciliation proves it is the correct Application vocabulary.
Reuse concepts when correct; do not couple unrelated boundaries just to reduce
type count.

Do not create an oversized failure taxonomy in anticipation of WP11.
WP11 owns final dataset validation/failure mapping.

The contract must leave WP11 room to map Infrastructure failures without public
SQLite exception leakage.

---

## 19. Snapshot Store Abstraction

Define only the capability required by later immutable snapshot persistence.

The abstraction must be dataset-specific, not generic CRUD.

It must support the accepted semantics necessary for WP08, such as:

- accepting an immutable snapshot candidate/descriptor;
- preserving snapshot identity/version;
- recognizing equivalent already-accepted snapshot evidence without creating a
  semantically new version;
- exposing deterministic integrity conflict when equal identity contradicts
  content;
- retrieving accepted snapshot evidence by deterministic semantic identity when
  required by later work.

Do not decide:

- SQLite schema;
- SQL;
- transactions;
- file layout;
- serialization;
- physical duplicate strategy;
- indexes;
- migration framework.

If write outcome vocabulary is required, make it semantic and minimal.
Do not use `Created/Updated/Deleted` CRUD vocabulary.

No update/delete/replace operation is authorized.

---

## 20. Catalog Registration / Lookup Abstraction

WP04 may establish the Application seam required for later catalog work, but
WP06 still owns the metadata/catalog model and WP09 owns persistence/lookup
implementation.

Therefore define only the minimum capability-level abstraction and lookup
criteria that are already semantically determined.

Allowed deterministic criteria may include accepted typed values such as:

- exact Research Dataset Identity;
- exact Snapshot Identity / Dataset Version;
- exact Definition Identity;
- exact target plus deterministic definition boundaries where required.

Do not invent rich catalog search/filter/pagination/tagging/query DSLs.
Do not create physical catalog records.
Do not define SQL-oriented lookup objects.
Do not make "latest" a semantic identity operation.

If a catalog method would require WP06 metadata decisions that are not yet
settled, keep the abstraction narrower and explicitly hand the unresolved model
to WP06 rather than guessing.

---

## 21. Catalog Registration Semantics

Any Application catalog-registration contract created in WP04 must preserve:

- immutable accepted snapshot identity;
- no identity reassignment;
- no silent overwrite;
- equivalent registration may be idempotent;
- contradictory content under an equal semantic identity is a deterministic
  integrity conflict;
- lookup result absence is distinguishable from storage failure.

Do not implement catalog behavior.

---

## 22. Domain Delta Gate

The authoritative WP04 surface is Application only.

Expected Domain delta:

```text
0
```

Inspect existing Domain values and use them when they already own the invariant.

If WP04 appears to require a Domain modification, stop. Do not modify Domain
under this authority. Report the exact missing Domain semantic and why the
Application layer cannot safely own it.

---

## 23. Infrastructure and Worker Protection

Expected deltas:

```text
Infrastructure: 0
Worker: 0
```

WP04 must not:

- register services;
- bind configuration;
- create SQLite connections;
- create database files;
- write/read snapshots;
- register/look up catalog records physically;
- execute materialization;
- invoke providers;
- modify Worker composition.

These belong to later WPs.

---

## 24. Package / Project Reference Gate

Expected:

```text
new packages: 0
package version changes: 0
new project references: 0
```

WP04 should compile using the existing dependency graph.

Any need for a new package or project reference is a blocker requiring separate
authority.

---

## 25. Permanent Test Gate

Expected permanent test delta:

```text
0
```

WP13 owns Domain/Application dataset tests.

You may use a narrowly scoped temporary compile/probe artifact only when
necessary to prove a contract invariant that cannot be demonstrated otherwise.
It must:

- remain offline;
- use no provider;
- use no database;
- contain no secret;
- be removed before final validation;
- leave zero residue.

Prefer static inspection plus normal build validation.

---

## 26. Application Architecture Gate

After implementation, prove:

```text
Domain -> none
Application -> Domain
Infrastructure -> Application
Worker -> Application, Infrastructure
```

WP04 must not introduce:

- Infrastructure reference from Application;
- SQLite reference from Application;
- provider/HTTP mechanics into Application dataset contracts;
- cyclic project references;
- public Infrastructure physical types.

Run all existing architecture tests.

---

## 27. API Surface Quality Gate

Inspect every new public Application type.

Require:

- one clear responsibility;
- no storage/provider terminology unless it is an abstract capability name;
- immutable/read-only semantics;
- deterministic validation;
- no nullable ambiguity where success/failure/result invariants can be explicit;
- no unnecessary inheritance hierarchy;
- no generic repository abstraction;
- no speculative extension points;
- no public mutable collection;
- no physical path/connection/SQL types;
- no hidden target normalization;
- no wall-clock/random identity generation.

Use repository naming and namespace conventions.

---

## 28. Semantic Validation Matrix

Before acceptance, explicitly prove each row:

| Requirement | Required result |
|---|---|
| WP02 dataset definition preserved | PASS |
| Single exact target preserved | PASS |
| `[from, to)` preserved | PASS |
| Deterministic ascending ordering preserved | PASS |
| Successful empty materialization representable | PASS |
| Timestamp/offset/decimal fidelity representable | PASS |
| Four WP03 identity concepts distinct | PASS |
| `aiq-dataset-identity-v1` preserved | PASS |
| 64-lowercase-hex identity form preserved | PASS |
| Dataset Version = Snapshot Identity semantics | PASS |
| Equivalent re-materialization representable | PASS |
| Relevant source change distinguishable | PASS |
| Operational metadata excluded from identity | PASS |
| Empty source/snapshot identity representable | PASS |
| Snapshot immutability representable | PASS |
| Identity reassignment prohibited | PASS |
| Provenance facts representable | PASS |
| Narrow lineage representable | PASS |
| Coverage semantics representable | PASS |
| Snapshot candidate/descriptor available to WP05 | PASS |
| Snapshot-store abstraction storage-independent | PASS |
| Catalog seam storage-independent | PASS |
| Deterministic absence distinguishable from failure where applicable | PASS |
| Integrity conflict representable | PASS |
| SQLite/SQL/ORM/filesystem leakage | 0 |
| Provider/HTTP leakage | 0 |
| Generic CRUD repository introduced | NO |
| Materialization implementation | NO |
| Catalog model implementation from WP06 | NO |
| Snapshot persistence implementation | NO |
| Permanent test delta | 0 |
| Package/reference delta | 0/0 |
| WP05 started | NO |
| Release 1.3 implementation started | NO |

If a row cannot be proven, WP04 is not complete.

---

## 29. Security and Offline Gate

WP04 must remain fully offline.

Verify:

- provider calls: 0;
- network calls required by WP04 implementation: 0;
- live credentials: 0;
- connection strings: 0;
- database paths: 0;
- secret material: 0;
- sensitive logging: 0.

Run canonical secret scanning if repository verification includes it.

---

## 30. Whitespace and Artifact Integrity

Before completion run:

```text
git diff --check
git diff --cached --check
```

Because accepted Release 1.2 work may remain untracked, also directly validate
all WP04-created untracked files for trailing whitespace and malformed final
line endings according to repository conventions.

Do not normalize unrelated accepted files.

If whitespace findings occur in WP04-created files, correct exactly those
findings and revalidate. No recursive authority chain is required for
whitespace-only corrections inside files created by this WP.

---

## 31. Final Technical Validation

After the exact WP04 candidate is complete, run the full required validation
again:

```text
restore
format verification
build
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
eng/verify.ps1
git diff --check
git diff --cached --check
```

Expected permanent counts remain:

```text
Domain.Tests: 11
Application.Tests: 42
Infrastructure.Tests: 79
Architecture.Tests: 13
Total: 145
```

If current accepted repository truth has legitimately changed these counts
before WP04, report the actual baseline and prove WP04 itself did not change
permanent test count.

Build must finish with zero warnings and zero errors.

No temporary artifact or database residue may remain.

---

## 32. Mutation Accounting

At finalization, enumerate exactly:

- Application files added;
- Application files modified;
- files deleted;
- Domain delta;
- Infrastructure delta;
- Worker delta;
- permanent test delta;
- package delta;
- project-reference delta;
- solution/build/script delta;
- temporary artifacts created/removed;
- unexpected paths.

Every production mutation must map directly to an authorized WP04 contract.

Do not stage or commit.

---

## 33. Git / GitHub Protection

Repository/Git transport prohibited:

```text
git add
git commit
git push
new branch
pull request
merge
tag
release
reset/rebase/history rewrite
```

GitHub mutation allowed only for issue #124 lifecycle and evidence after gates
pass.

Do not modify:

- issue #125 or later issues;
- issue dependencies;
- milestone definition;
- Project field schema/options;
- Release 1.1 planning;
- Release 1.3 planning.

---

## 34. Issue Lifecycle

Only after starting-state and initial baseline gates pass:

```text
#124 Backlog -> In Progress
```

Only after every WP04 acceptance gate passes:

1. post concise completion evidence to #124;
2. close #124;
3. set its Project status to Done;
4. verify #125 remains Open/Backlog;
5. verify #126 remains Open/Backlog;
6. leave milestone #53 Open.

Do not start WP05.

If blocked after #124 was moved In Progress, report the blocker precisely and
do not falsely close the issue.

---

## 35. WP05 Handoff Requirements

The WP04 report must give WP05 an exact implementation handoff, including:

- exact Application dataset-definition/request type;
- exact identity/version types;
- exact snapshot candidate/descriptor type;
- exact coverage/provenance/lineage types;
- exact materialization result/failure vocabulary;
- exact snapshot-store abstraction;
- exact catalog abstraction/lookup criteria introduced;
- validation invariants WP05 may rely on;
- explicit unresolved matters still owned by WP06+;
- confirmation that WP05 must not redesign WP02/WP03 semantics.

WP05 should be able to implement deterministic materialization without guessing
what WP04 meant.

---

## 36. Required Execution Report

Produce a structured report containing at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor / Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Existing Application Convention Reconciliation
10. WP02 Semantic Reconciliation
11. WP03 Identity / Version / Provenance Reconciliation
12. Contract Surface Design
13. Dataset Definition / Request Contract
14. Identity Contract Model
15. Dataset Version Contract
16. Snapshot Candidate / Descriptor Contract
17. Coverage Contract
18. Provenance Contract
19. Lineage Contract
20. Materialization Result / Failure Contract
21. Snapshot Store Abstraction
22. Catalog Registration / Lookup Abstraction
23. Validation / Invariant Decisions
24. Domain Delta
25. Infrastructure / Worker Delta
26. Exact Files Added / Modified
27. Package / Reference Delta
28. Permanent Test Delta
29. WP05 / WP06+ Protection
30. Security / Offline Evidence
31. Whitespace / Diff Evidence
32. Restore / Build Evidence
33. Permanent Test Evidence
34. Canonical Verification
35. Architecture Validation
36. Semantic Acceptance Matrix
37. Mutation Accounting
38. Git / GitHub Protection
39. Planning Protection
40. Findings / Blockers
41. Final Repository / GitHub State
42. WP05 Handoff
43. Final Decision
44. Next Authorized Work Package

Use actual observed evidence. Do not copy expected values as though observed.

---

## 37. Required Terminal Summary

If successful, end exactly with:

```text
RELEASE 1.2 WP04 COMPLETE

APPLICATION DATASET CONTRACTS:
WP02 dataset-definition semantics preserved: PASS
WP03 identity/version/provenance semantics preserved: PASS
Provider-independent: PASS
Storage-independent: PASS
Dataset definition/request contract: PASS
Four typed identity concepts: PASS
Dataset Version semantics: PASS
Snapshot candidate/descriptor contract: PASS
Coverage contract: PASS
Provenance contract: PASS
Lineage contract: PASS
Successful empty materialization representable: PASS
Timestamp/offset/decimal fidelity representable: PASS
Equivalent re-materialization representable: PASS
Integrity conflict representable: PASS
Snapshot-store abstraction: PASS
Catalog registration/lookup seam: PASS
SQLite/SQL/ORM/filesystem leakage: 0
Provider/HTTP leakage: 0
Domain delta: 0
Infrastructure delta: 0
Worker delta: 0
Permanent test delta: 0
Package/reference delta: 0/0
WP05 started: NO
Release 1.3 implementation started: NO
Issue #124: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP05 — Dataset Materialization Use Case
GitHub issue #125
```

If blocked, end exactly with:

```text
RELEASE 1.2 WP04 BLOCKED
```

Do not print the success marker unless every mandatory gate passes.

---

## 38. Final Constraint

WP04 is the **Application semantic seam**, not an implementation package.

Translate accepted meaning into the smallest explicit contracts that later work
can depend on. Preserve typed identity, deterministic definition, immutable
snapshot semantics, provenance, lineage, and storage/provider independence.

Do not solve WP05 orchestration, WP06 catalog modeling, WP07 physical storage,
WP08/WP09 persistence, WP11 failure mapping, WP12 execution, or WP13 tests.

**The contract must express the accepted dataset semantics precisely without
letting storage technology, provider mechanics, or operational execution become
part of dataset meaning.**

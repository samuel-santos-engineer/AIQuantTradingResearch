# Release 1.1 WP03 — Historical Observation Persistence Semantics — Authoritative Codex Execution Prompt

## 1. Authority

You are executing **Release 1.1 — WP03: Historical Observation Persistence Semantics** for:

```text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Release:    Phase 3 - Release 1.1: Market Data Persistence Foundation
Work item:  GitHub issue #105
WP:         WP03
```

This file is the authoritative WP03 execution contract.

Read this file completely before taking any action.

The standard five-line companion:

```text
docs/roadmap/release-1.1/prompts/03-historical-observation-persistence-semantics-codex-prompt-chat.md
```

is only a bootstrap. It does not duplicate or supersede this contract.

---

## 2. Governing Authority Precedence

Apply authority in this order:

1. Explicit human instructions in the current execution conversation.
2. This authoritative WP03 prompt.
3. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`.
4. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`.
5. The accepted Release 1.1 governance-baseline post-merge closure.
6. The accepted WP01 result ending `RELEASE 1.1 WP01 COMPLETE`.
7. The accepted WP02 result ending `RELEASE 1.1 WP02 COMPLETE`.
8. `MARKET_DATA_PERSISTENCE_ASSESSMENT.md` and `MARKET_DATA_PERSISTENCE_DECISION.md` as accepted WP02 architectural evidence.
9. GitHub issue #105 and the accepted Release 1.1 planning state.
10. Current repository and GitHub truth.
11. Existing repository engineering, architecture, Domain-model, naming, and documentation conventions where they do not conflict with higher authority.

Do not infer authority to expand scope.

If authorities cannot be reconciled without mutation outside this prompt, stop and report `WP03 BLOCKED`.

---

## 3. Purpose

WP03 defines the **provider-independent meaning of historical observation persistence** before Application contracts, physical storage design, or SQLite implementation exist.

This is a **semantic design and Domain-reconciliation work package**.

WP03 must establish explicit answers for:

- historical observation identity;
- timestamp preservation and reconstruction;
- normalized price preservation;
- canonical historical ordering;
- duplicate/idempotency behavior;
- conflicting-observation behavior;
- valid empty historical retrieval semantics;
- the boundary between absence, conflict, invalid semantic state, and later storage/runtime failure concerns;
- whether the existing Release 1.0 Domain model already expresses every required invariant.

The primary expected implementation result is:

```text
Domain delta = 0
```

A Domain change is authorized only when repository truth proves that a provider-independent semantic invariant required by WP03 cannot be represented correctly with the existing Domain model.

WP03 is not a persistence implementation work package.

---

## 4. Accepted Starting Lifecycle State

The accepted WP02 result established:

```text
Release 1.0:                     CLOSED
Release 1.1 governance baseline: MERGED / CLOSED
WP01 / issue #103:               CLOSED / Done
WP02 / issue #104:               CLOSED / Done
WP03 / issue #105:               OPEN / Backlog
WP04–WP16:                       OPEN / Backlog
Milestone #52:                   OPEN
Legacy milestone #42:            CLOSED / empty
Legacy milestone #43:            CLOSED / empty
Active Release 1.2 planning:     0
```

Accepted WP02 repository evidence included:

```text
Branch:       main
HEAD:         9ce7af388b9818bf4374897fc4615e17ccc1615a
origin/main:  same SHA
Ahead/behind: 0/0
Staged:       0
Tracked changes: 0
```

Expected untracked files at WP02 completion were exactly:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt.md
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt-chat.md
```

Treat all SHAs and counts as accepted historical evidence, not permission to fabricate current state.

Fetch/re-query safely and reconcile current Git/GitHub truth before mutation.

An intervening human-approved integration may legitimately change `main`. Proceed only if accepted WP01/WP02 outcomes remain represented and no unauthorized WP03+ implementation has begun.

Never reset, rewrite, discard, delete, or overwrite user work merely to reproduce an old state.

---

## 5. WP03 Governance Pair and Recursion-Safe Handling

The file manifest explicitly authorizes exactly these WP03 governance files:

```text
docs/roadmap/release-1.1/prompts/03-historical-observation-persistence-semantics-codex-prompt.md
docs/roadmap/release-1.1/prompts/03-historical-observation-persistence-semantics-codex-prompt-chat.md
```

These two files are **EXPECTED GOVERNANCE** for WP03.

Their presence as untracked files when execution starts is expected and must not be classified as implementation drift, an unexpected mutation, or a dirty-tree blocker.

The accepted WP01 and WP02 governance pairs are also expected if still untracked.

Do not modify, normalize, stage, commit, delete, relocate, or integrate any governance prompt pair under this authority.

This rule intentionally prevents governance-artifact recursion.

---

## 6. Accepted WP02 Technology Decision

WP02 selected exactly:

```text
SQLite via the future minimum Microsoft.Data.Sqlite integration
```

This decision is authoritative context but **must not leak into WP03 semantics**.

WP02 established these boundaries:

- SQLite is bounded to Release 1.1 historical market-data persistence.
- Storage mechanics belong in Infrastructure.
- Domain and Application remain SQLite-independent.
- Twelve Data acquisition remains separate from persistence.
- Tests must eventually work offline with isolated disposable state.
- WP03–WP16 retain ownership of their assigned semantics, contracts, schema, packages, DI, Worker behavior, and tests.
- SQLite is not declared the permanent platform-wide database.

WP03 must define semantics that would remain valid if the storage technology were later replaced.

Do not add `Microsoft.Data.Sqlite` or any other persistence package.

---

## 7. WP02 Constraints That WP03 Must Resolve Semantically

WP02 identified downstream proof points including:

- SQLite decimal round-trip behavior requires explicit proof later.
- Offset-aware timestamp round-trip behavior requires explicit proof later.
- File locking, placement, cleanup, lifecycle, physical schema, migration mechanism, and exact package version remain later-WP concerns.

WP03 owns only the **semantic requirements** that those later implementations must preserve.

Therefore WP03 must state what must be true of price and timestamp values after persistence/retrieval, but must not prescribe SQLite encodings, columns, affinities, SQL, serializers, conversion APIs, migrations, or physical representations.

---

## 8. Repository Baseline to Reconcile

Inspect the current repository rather than assuming type shapes.

At minimum reconcile:

- `PriceObservation`;
- `ObservationSeries`;
- target/instrument identity values used by Release 1.0;
- observation timestamp representation;
- normalized price representation;
- equality/value semantics already supplied by Domain types;
- Application research request/result types only as consumers of Domain meaning;
- current provider normalization boundary;
- existing Domain invariants and construction validation;
- any existing ordering behavior that is semantic rather than incidental.

WP01/WP02 established the production dependency graph as:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

WP03 must preserve this graph.

---

## 9. Required Semantic Decision — Historical Observation Identity

Define historical observation identity using provider-independent semantics.

The expected conceptual rule is:

```text
target/instrument + observation instant
```

The exact rule must reconcile with existing Release 1.0 Domain values.

The identity rule must:

- be independent of Twelve Data;
- be independent of SQLite;
- not depend on provider request/response identifiers;
- not depend on database-generated keys;
- not depend on row IDs, file paths, table names, or storage-specific metadata;
- distinguish observations for different targets/instruments at the same instant;
- identify observations for the same target/instrument at the same semantic instant as the same persistence identity.

If existing Domain target/instrument semantics are more precise than the conceptual wording above, preserve repository truth rather than inventing a parallel identity model.

---

## 10. Required Semantic Decision — Timestamp Fidelity

`PriceObservation` represents observation time semantically as `DateTimeOffset`.

WP03 must define the persistence invariant such that a persisted and reconstructed observation preserves the same semantic instant and the Domain-required offset-aware meaning.

At minimum decide and report:

- whether identity compares timestamps by semantic instant, exact `DateTimeOffset` value, or another existing Domain equality rule;
- what equality must hold after a persistence round trip;
- whether original offset representation is itself semantically significant or only the represented instant is significant;
- how canonical ordering relates to the timestamp semantic.

Do not design a physical encoding.

Do not convert the Domain contract to provider-local time, database-local time, machine-local time, or an unspecified timezone.

Any ambiguity discovered in existing Domain semantics must be surfaced explicitly.

---

## 11. Required Semantic Decision — Price Fidelity

Persist the normalized provider-independent price represented by the Domain model.

WP03 must establish that persistence/reconstruction:

- operates on the normalized Domain price, not provider transport text;
- preserves the Domain-observable numeric value exactly to the degree required by the existing type;
- does not introduce provider rounding rules;
- does not silently truncate or change scale in a way that changes Domain equality/value;
- does not introduce floating-point semantics if the Domain does not already use them.

Define the semantic round-trip requirement.

Do not choose a SQL type, SQLite affinity, binary representation, string representation, converter, serializer, or schema.

---

## 12. Required Semantic Decision — Canonical Ordering

Canonical historical retrieval order is:

```text
observation instant ascending
```

WP03 must define this ordering as provider-independent semantic behavior.

Reconcile how ties are treated:

- if two observations have the same target/instrument and same identity instant, duplicate/conflict semantics apply;
- do not invent an arbitrary storage-specific tie-breaker;
- do not use insertion order, row ID, provider response order, or database implementation order as semantic ordering.

If retrieval can span multiple targets/instruments under existing or later contracts, identify whether WP03 has enough authority to define cross-target ordering. If not, keep the invariant scoped to the historical series/query semantics actually supported by repository truth and record the boundary for WP04.

---

## 13. Required Semantic Decision — Equivalent Duplicate

The expected target policy is:

```text
same identity + equivalent observation
→ idempotent
```

WP03 must define **equivalent observation** using existing provider-independent Domain semantics.

The rule must make repeated persistence of the same semantic observation safe and deterministic.

Idempotent means the semantic stored history is unchanged by repetition.

Do not prescribe:

- SQL `INSERT OR IGNORE`;
- UPSERT syntax;
- unique indexes;
- transaction mechanics;
- row-count behavior;
- repository return types;
- database-specific conflict clauses.

Those belong to later WPs.

---

## 14. Required Semantic Decision — Conflicting Duplicate

The expected target policy is:

```text
same identity + conflicting observation
→ deterministic conflict
```

A conflicting observation is conceptually an observation with the same persistence identity but non-equivalent provider-independent observation value.

WP03 must define:

- the semantic condition for conflict;
- that conflict must not silently overwrite accepted historical truth;
- that conflict must not silently correct or mutate the existing observation;
- that later Application/Infrastructure contracts must be able to represent this deterministically.

Do not design the later failure/result type in WP03.

Do not introduce an Application error enum or Infrastructure exception.

No silent overwrite/correction is authorized unless a later explicit human authority changes this semantic rule.

---

## 15. Required Semantic Decision — Historical Immutability

Reconcile whether the identity/duplicate rules imply an immutable historical observation model.

Unless repository truth proves otherwise, the required Release 1.1 semantic is:

- a new identity may add a historical observation;
- an equivalent existing identity is idempotent;
- a conflicting existing identity is a conflict;
- there is no update/correction/delete semantic in the Release 1.1 persistence slice.

Explicitly state whether this conclusion follows from accepted authority and repository Domain semantics.

Do not invent administrative correction workflows, version history, soft deletes, mutable records, or data-repair APIs.

---

## 16. Required Semantic Decision — Empty Historical Retrieval

Define valid empty retrieval versus failure at the provider-independent semantic level.

The expected distinction is:

```text
valid query + no matching persisted observations
→ successful empty historical result

invalid semantic request
→ validation failure according to existing/later provider-independent rules

storage/runtime failure
→ not an empty result
```

WP03 must establish the semantic distinction without designing WP04 contracts or WP10 failure mapping.

Do not use `null` as a substitute for an empty historical result unless existing authoritative repository semantics explicitly require it.

Do not classify unavailable storage, corruption, timeout, I/O, connection, or SQL failures as successful emptiness.

---

## 17. Semantic Boundary — Acquisition Versus Persistence

Preserve the Release 1.0 provider boundary.

Twelve Data acquisition and normalization occur before persistence semantics are applied.

WP03 must not:

- persist provider transport DTOs as the semantic source of truth;
- define identity using provider-specific metadata;
- move provider normalization into Domain;
- redefine Twelve Data failure mapping;
- couple persistence semantics to HTTP or provider behavior.

The persisted semantic observation is the normalized provider-independent Domain observation.

---

## 18. Semantic Boundary — Domain Versus Application

WP03 may determine Domain meaning.

WP04 owns Application persistence contracts.

Therefore WP03 must not create or modify:

- persistence interfaces;
- repository interfaces;
- store/read requests;
- persistence results;
- retrieval results;
- persistence failure vocabulary;
- retrieval failure vocabulary;
- use-case orchestration.

If WP03 identifies a semantic requirement that WP04 must expose, record it in the execution report as a **WP04 handoff requirement** rather than implementing it.

---

## 19. Semantic Boundary — Domain Versus Infrastructure

WP06–WP10 own physical storage design and behavior.

WP03 must not create or specify implementation-level:

- tables;
- columns;
- keys/indexes;
- SQL;
- SQLite affinities;
- migrations;
- connection strings;
- connection factories;
- database paths;
- transaction APIs;
- concurrency/locking implementation;
- persistence adapters;
- serialization/conversion code.

WP03 may state only technology-independent invariants that those later mechanisms must satisfy.

---

## 20. Domain Delta Decision Gate

The manifest establishes:

```text
Primary expected outcome:
Domain delta = 0
```

First prove whether existing Domain values already express the required WP03 semantics.

A zero Domain delta is **preferred and fully valid**.

Do not modify Domain merely to create visible implementation output.

A Domain change is justified only if all of the following are true:

1. A mandatory WP03 semantic invariant cannot be expressed truthfully with the existing Domain model.
2. The missing concept is provider-independent.
3. The concept belongs intrinsically to Domain rather than Application persistence contracts.
4. The smallest possible Domain change resolves the gap.
5. The change does not encode SQLite, persistence mechanism, or later-WP policy.
6. Existing Release 1.0 behavior can be preserved.
7. The change fits the manifest-authorized path:
   `src/AIQuantTradingResearch.Domain/**`.

If any proposed change fails these tests, do not make it.

---

## 21. Authorized WP03 Repository Mutation Surface

Normal expected tracked mutation:

```text
none
```

If and only if the Domain Delta Decision Gate proves a change necessary, WP03 may modify only:

```text
src/AIQuantTradingResearch.Domain/**
```

and only for provider-independent persistence semantics/invariants.

The manifest does not authorize WP03 to invent a new documentation artifact merely to create a durable file output.

The complete execution report is the required evidence of the semantic decisions when Domain delta is zero.

---

## 22. Explicitly Prohibited Repository Mutations

Do not modify:

```text
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**
tests/**
Directory.Packages.props
Directory.Build.props
global.json
*.sln
*.slnx
*.csproj
eng/**
.github/**
```

Do not create:

- Application persistence contracts;
- Infrastructure persistence/storage types;
- SQL;
- schema;
- migrations;
- database files;
- SQLite-specific code;
- packages or package versions;
- DI registrations;
- Worker behavior;
- persistence/retrieval tests;
- architecture tests;
- new documentation artifacts not explicitly authorized by the manifest;
- WP04 governance artifacts;
- WP04 implementation.

---

## 23. Working-Tree Classification

Before mutation classify every non-HEAD path as exactly one of:

```text
EXPECTED PRIOR GOVERNANCE
EXPECTED WP03 GOVERNANCE
ACCEPTED WP02 ARTIFACT
AUTHORIZED WP03 DOMAIN DELTA
UNEXPECTED / AMBIGUOUS
```

Expected prior governance may include WP01/WP02 prompt pairs.

Expected WP03 governance is the WP03 prompt pair.

Accepted WP02 artifacts are:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

Do not alter accepted WP02 artifacts.

If an unexpected tracked modification, unrecognized untracked file, staged path, or ambiguous user change could be affected by WP03 actions, stop before destructive mutation and report the exact blocker.

Never clean the repository with broad destructive commands.

---

## 24. GitHub Planning Reconciliation

Before progressing issue #105 verify:

- milestone #52 is OPEN;
- #103 is CLOSED / Done;
- #104 is CLOSED / Done;
- #105 is OPEN / Backlog;
- #106–#118 remain OPEN / Backlog;
- #105 has the planned `architecture` label;
- #105 has priority P1;
- #105 has release 1.1;
- #105 has Area `Architecture`;
- #105 depends exactly on #104;
- no artificial dependency drift exists;
- no WP17+ issue exists;
- no active Release 1.2 planning exists.

Do not repair unrelated planning drift without explicit authority.

If a required WP03 planning precondition is materially wrong, stop and report it.

---

## 25. WP03 Issue-State Handling

Only issue #105 may progress.

After all preconditions pass, move #105:

```text
Backlog → In Progress
```

Do not move #106–#118.

After every WP03 acceptance gate passes:

1. add one concise completion-evidence comment to #105;
2. close #105;
3. allow existing Project automation to move it to `Done`;
4. verify final state.

If automation does not produce the expected terminal state, report the exact observed state. Do not mutate Project schema or workflows.

---

## 26. Initial Technical Baseline Gate

Before any Domain mutation:

1. safely reconcile `main` and `origin/main`;
2. verify no unauthorized WP03+ implementation exists;
3. run repository canonical verification;
4. record restore/build/test results;
5. record build warning/error counts;
6. record permanent test counts;
7. record Architecture.Tests count;
8. confirm the production dependency graph;
9. inspect relevant Domain types completely.

Expected historical baseline from WP02:

```text
Domain.Tests:         11
Application.Tests:    16
Infrastructure.Tests: 65
Architecture.Tests:   13
Total:               105
```

These are historical expectations, not values to fabricate.

If the current accepted baseline legitimately differs, explain why and use repository truth.

If baseline verification fails for reasons unrelated to WP03 and cannot be reconciled without unauthorized repair, stop.

---

## 27. Evidence-First Semantic Analysis

For every required semantic decision, distinguish:

```text
AUTHORITY
REPOSITORY FACT
DERIVED SEMANTIC CONCLUSION
LATER-WP HANDOFF
```

Do not manufacture implementation detail to make a semantic conclusion appear concrete.

At minimum inspect enough repository code to answer:

- What exactly identifies a target/instrument today?
- What exactly is the `PriceObservation` timestamp type and equality behavior?
- What exactly is the price type and equality behavior?
- Does `PriceObservation` itself carry target/instrument identity, or is identity contextual through another Domain/Application value?
- Does `ObservationSeries` impose target identity, ordering, uniqueness, or validation?
- Which invariants already exist?
- Which persistence semantics are new policy rather than new Domain state?

The report must cite exact repository paths/types inspected.

---

## 28. Required WP03 Semantic Decision Record in the Report

The execution report must contain one explicit consolidated decision section covering at least:

```text
Historical identity
Timestamp round-trip invariant
Timestamp identity/equality rule
Price round-trip invariant
Canonical ordering
Equivalent duplicate definition
Equivalent duplicate outcome
Conflicting duplicate definition
Conflicting duplicate outcome
Historical immutability/update policy
Empty retrieval semantics
Acquisition/persistence boundary
Domain/Application handoff boundary
Domain/Infrastructure handoff boundary
Domain delta decision
```

Do not leave any item implicit.

If repository truth prevents a mandatory decision, WP03 is blocked.

---

## 29. Domain Change Procedure — Only If Proven Necessary

If Domain delta is required:

1. state the exact semantic gap before editing;
2. identify the smallest Domain path(s) needed;
3. prove no Application/Infrastructure concept is being pulled into Domain;
4. make the minimal change;
5. do not add tests under WP03;
6. build immediately;
7. run canonical verification;
8. inspect the diff for scope and semantic drift;
9. ensure no storage technology terminology appears in Domain unless it already existed for unrelated reasons.

A Domain change that requires new tests to establish correctness but cannot be safely accepted without them is a blocker because WP03 does not own tests.

Do not bypass that blocker by editing test projects.

---

## 30. Zero-Domain-Delta Procedure

If existing Domain types are sufficient:

- make no tracked source change;
- explicitly state `Domain delta = 0`;
- explain how existing Domain concepts support the required semantics;
- record any semantic policy that belongs to WP04+ rather than Domain state;
- do not create placeholder classes, marker interfaces, comments, or documentation solely to manufacture a diff.

A no-code WP03 is a successful outcome when every semantic decision is explicit and evidence-backed.

---

## 31. Whitespace Policy

WP03 must not create a whitespace-unblock recursion.

For files actually authorized and modified by WP03:

- run `git diff --check`;
- if Git reports whitespace introduced by WP03 in an authorized modified Domain file, correct only the reported whitespace in that authorized file;
- do not normalize unrelated lines or files;
- do not modify governance files for whitespace;
- do not modify accepted WP02 artifacts for whitespace.

If no tracked file is modified, report the diff checks truthfully and do not invent whitespace work.

Any pre-existing whitespace finding outside the authorized WP03 mutation surface is a blocker only if it prevents a mandatory WP03 gate; otherwise record it without modifying it.

---

## 32. Security and Credential Safety

WP03 requires no live provider or database access.

Do not:

- call Twelve Data;
- create/open a live persistence database;
- use cloud resources;
- print secrets;
- inspect unrelated credential stores;
- create `.env` files;
- create credential-bearing connection strings.

No persistence credential is needed for semantic design.

---

## 33. Validation After Semantic Reconciliation

After semantic analysis and any authorized Domain delta:

Run the canonical repository verification.

Record at minimum:

- restore result;
- format verification result;
- build result;
- warning count;
- error count;
- Domain.Tests;
- Application.Tests;
- Infrastructure.Tests;
- Architecture.Tests;
- total permanent tests;
- failed/skipped tests.

If Domain delta is zero, the repository should remain technically unchanged.

If Domain changed, Release 1.0 behavior must remain green.

---

## 34. Architecture Boundary Validation

Confirm after WP03:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Also verify:

- Domain contains no SQLite dependency;
- Domain contains no `Microsoft.Data.Sqlite` dependency;
- Domain contains no SQL/storage-engine mechanics;
- Application persistence contracts still do not exist;
- Infrastructure persistence implementation still does not exist;
- Worker persistence behavior still does not exist.

WP03 must not prematurely consume responsibilities of WP04–WP12.

---

## 35. Git / GitHub Transport Protection

WP03 is not an integration work package.

Do not:

- `git add`;
- stage files;
- create a branch;
- commit;
- amend;
- push;
- force-push;
- create a PR;
- merge;
- create a tag;
- create a GitHub Release.

A safe fetch/re-query for reconciliation is permitted.

Any eventual integration of WP03 outputs belongs to a later explicit authority.

---

## 36. WP04 Protection

WP04 is:

```text
WP04 — Application Persistence Contracts
GitHub issue #106
```

WP03 may hand semantic requirements to WP04 in the execution report but must not execute them.

At final acceptance verify:

- issue #106 remains OPEN / Backlog;
- no WP04 governance pair was created by this run;
- no Application persistence interface/contract was created;
- no persistence failure/result vocabulary was added to Application;
- no WP04 implementation began.

---

## 37. Required WP04 Handoff

The WP03 report must provide a concise handoff identifying what WP04 contracts must preserve without prescribing their shape.

At minimum the handoff must include:

- provider-independent historical identity semantics;
- idempotent equivalent duplicate behavior;
- deterministic conflicting duplicate behavior;
- canonical ordering;
- successful empty retrieval;
- timestamp round-trip semantic requirement;
- price round-trip semantic requirement;
- no silent historical overwrite/correction;
- technology independence.

Do not name interfaces or methods unless they already exist in repository truth.

---

## 38. Completion Evidence Comment

Only after all WP03 gates pass, add a concise comment to issue #105 summarizing:

- WP02 prerequisite accepted;
- Domain delta (`0` or exact minimal paths);
- historical identity decision;
- duplicate/conflict decision;
- ordering decision;
- timestamp and price fidelity decisions;
- empty retrieval decision;
- canonical verification result;
- permanent test count;
- confirmation that WP04 was not started.

Do not paste the entire execution report into GitHub.

---

## 39. WP03 Acceptance Gates

WP03 succeeds only if all applicable gates pass:

### Lifecycle
- WP01 CLOSED / Done.
- WP02 CLOSED / Done.
- WP03 is the only progressed work package.
- WP04–WP16 remain unstarted.
- milestone #52 remains OPEN.
- active Release 1.2 planning remains 0.

### Semantic completeness
- historical identity explicitly defined.
- timestamp semantic and round-trip invariant explicitly defined.
- price semantic and round-trip invariant explicitly defined.
- canonical ordering explicitly defined.
- equivalent duplicate semantics explicitly defined.
- conflicting duplicate semantics explicitly defined.
- no-silent-overwrite policy explicitly defined.
- empty retrieval semantics explicitly defined.
- acquisition/persistence boundary preserved.
- Domain/Application/Infrastructure ownership boundaries preserved.

### Domain
- Domain delta decision explicitly justified.
- preferred `Domain delta = 0` used when existing model is sufficient.
- if Domain changed, only manifest-authorized Domain paths changed.
- no persistence technology leaked into Domain.

### Repository
- no unauthorized tracked path changed.
- no Application/Infrastructure/Worker/test/package/project mutation.
- no schema/database/runtime file created.
- no WP04 artifact created.
- whitespace checks pass for WP03-authorized changes.
- canonical verification passes.
- build errors = 0.
- permanent tests pass.
- Architecture.Tests pass.

### Git/GitHub
- no stage/commit/branch/push/PR/merge/tag/Release.
- issue #105 receives completion evidence.
- issue #105 is CLOSED / Done.
- #106–#118 are not progressed.

If any mandatory gate fails, do not emit the success terminal.

---

## 40. Required Execution Report

Return one complete report with at least these sections:

1. Executive Summary
2. Authorities Reviewed
3. Authentication / Repository Context
4. Initial Git State
5. Working-Tree Classification
6. WP01/WP02 Completion Reconciliation
7. Release 1.1 Planning Reconciliation
8. WP03 Issue-State Handling
9. Initial Technical Baseline
10. Domain Model Inspection
11. Accepted WP02 Technology Boundary
12. Historical Observation Identity
13. Timestamp Semantics
14. Price Semantics
15. Canonical Ordering
16. Equivalent Duplicate Semantics
17. Conflicting Duplicate Semantics
18. Historical Immutability / Mutation Policy
19. Empty Retrieval Semantics
20. Acquisition / Persistence Boundary
21. Domain / Application / Infrastructure Boundary
22. Domain Delta Decision
23. Domain Mutation Evidence, if any
24. Consolidated Persistence Semantic Decision Record
25. WP04 Handoff Requirements
26. Whitespace Evidence
27. Security / Credential Safety
28. Repository Mutation Accounting
29. Final Technical Validation
30. Architecture Boundary Validation
31. Git / GitHub Protection
32. WP04 Protection
33. Findings / Observations
34. WP03 Acceptance Matrix
35. Final GitHub State
36. Final Repository State
37. Final Decision
38. Next Authorized Work Package

Include exact paths, type names, test counts, and Git/GitHub states actually observed.

Do not fabricate hosted CI, reviews, commits, pushes, database behavior, or evidence not observed.

---

## 41. Required Success Terminal

Emit this terminal only if every WP03 acceptance gate passes:

```text
RELEASE 1.1 WP03 COMPLETE

DOMAIN DELTA:
<0 or exact authorized Domain paths changed>

PERSISTENCE SEMANTICS:
Historical identity: <exact decision>
Equivalent duplicate: IDEMPOTENT
Conflicting duplicate: DETERMINISTIC CONFLICT
Historical ordering: OBSERVATION INSTANT ASCENDING
Empty retrieval: SUCCESSFUL EMPTY RESULT

NEXT AUTHORIZED WORK PACKAGE:
WP04 — Application Persistence Contracts
GitHub issue #106
```

If any mandatory gate fails, emit:

```text
RELEASE 1.1 WP03 BLOCKED
```

and identify the exact blocker and minimum corrective authority.

---

## 42. Final Execution Instruction

Execute WP03 as a bounded, technology-independent semantic reconciliation.

Start from the accepted WP02 decision that SQLite via future minimum `Microsoft.Data.Sqlite` integration is the selected Release 1.1 persistence technology, but do not let that implementation choice shape Domain semantics.

Inspect the actual Release 1.0 Domain model first. Prefer and accept `Domain delta = 0` when existing Domain values already express the required meaning.

Define historical identity, timestamp fidelity, price fidelity, canonical ordering, equivalent-duplicate idempotency, conflicting-duplicate deterministic conflict, historical immutability, and successful empty retrieval explicitly. Preserve acquisition/persistence separation and hand only semantic requirements forward to WP04.

Do not add Application contracts, Infrastructure storage, SQLite code, schema, packages, tests, DI, Worker behavior, Git transport, or WP04 artifacts.

WP03 succeeds only when all persistence semantics required by later work packages are explicit and evidence-backed, the repository remains green, issue #105 is CLOSED / Done, and WP04 remains untouched.

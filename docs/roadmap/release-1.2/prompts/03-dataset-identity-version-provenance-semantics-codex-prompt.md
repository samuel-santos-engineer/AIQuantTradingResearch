# Release 1.2 WP03 --- Dataset Identity, Version & Provenance Semantics --- Codex Prompt

## Role

Act as the **WP03 Dataset Identity, Version & Provenance Semantics
Executor** for Release 1.2 of `AIQuantTradingResearch`.

This is a bounded **semantic design and decision** work package. Its
purpose is to convert the accepted WP02 dataset/reproducibility model
into precise, deterministic identity, version, provenance, and lineage
semantics that later Application, catalog, persistence, integration, and
test work can implement without reopening foundational meaning.

Use **GPT-5.6 Sol** for this work package.

Do not implement Application contracts, storage, catalog persistence, or
materialization behavior. Do not start WP04.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
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
docs/architecture/data/RESEARCH_DATASET_DEFINITION.md
```

Also inspect:

``` text
accepted WP01 execution result
accepted WP02 execution result
accepted Release 1.1 persistence semantics and implementation
current Domain/Application/Infrastructure/Worker contracts
current data architecture, glossary, catalog, versioning, and provenance-related documentation
current repository and permanent-test state
GitHub milestone #53
GitHub issue #123
GitHub issue #124
Project #2
```

Authority precedence:

``` text
1. RELEASE_1.2_EXECUTION_PLAN.md
2. RELEASE_1.2_FILE_MANIFEST.md
3. Accepted Release 1.2 GitHub planning state
4. Accepted WP02 result and RESEARCH_DATASET_DEFINITION.md
5. Accepted WP01 result
6. Accepted Release 1.1 repository truth
7. Existing repository architecture/governance conventions
8. This execution prompt
```

If authorities materially conflict, stop and report the smallest precise
blocker. Do not silently reconcile contradictory authority.

------------------------------------------------------------------------

## 2. Accepted Starting Baseline

Treat the accepted WP02 result as the semantic starting baseline unless
current evidence proves drift:

``` text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Branch: main
WP02 accepted HEAD: 3ae8ba300fcd356b71fb4fdef5258dc23a99abeb
local main = origin/main at WP02 completion
staged paths: 0
tracked modifications: 0

Release 1.2:
  milestone #53: OPEN
  issue #121: CLOSED / DONE
  issue #122: CLOSED / DONE
  issue #123: OPEN / BACKLOG
  issue #124: OPEN / BACKLOG
  WP03 dependency: exactly WP02
  dependency drift: 0
  Release 1.3 implementation: NOT STARTED

Technical baseline at WP02:
  Domain.Tests: 11/11
  Application.Tests: 42/42
  Infrastructure.Tests: 79/79
  Architecture.Tests: 13/13
  total permanent tests: 145/145
  canonical verification: PASS
```

The SHA and counts are historical evidence. Re-observe current truth; do
not reset or falsify current state to match them.

Accepted WP02 semantics include:

``` text
Research Dataset:
  immutable, research-owned deterministic materialization
  of explicitly selected accepted observations plus explanatory semantic metadata

Dataset Definition:
  exact single target
  explicit [from, to) semantic-instant boundaries
  deterministic inclusion and ordering
  explicitly authorized deterministic parameters

Materialization:
  deterministic resolution of a definition against a fixed relevant source-observation state

Snapshot:
  durable immutable evidence of one successful materialization

Source truth:
  accepted Release 1.1 PriceObservation history
  exact target + semantic instant identity

Ordering:
  strictly ascending semantic instant

Empty result:
  valid successful materialization

Fidelity:
  original DateTimeOffset offset representation preserved
  exact decimal value preserved

Reproducibility:
  equivalent definition + equivalent relevant source state
  => equivalent ordered content + semantic metadata

Source-history change:
  prior snapshots remain immutable
  changed relevant source state may yield a distinguishable later snapshot

Operational metadata:
  wall clock, machine/process identity, paths, connection strings,
  random values, provider order, DB natural order, culture/local timezone
  do not define semantic equivalence
```

------------------------------------------------------------------------

## 3. Objective

Define the Release 1.2 semantic model for:

``` text
dataset identity
dataset-definition identity
snapshot identity
version semantics
semantic equivalence
source-state identity
provenance
lineage
distinguishability
immutability across re-materialization
```

WP03 must answer, at minimum:

``` text
What entity or concept needs a stable logical identity?
What distinguishes dataset-definition identity from snapshot identity?
What does "version" mean in Release 1.2?
Does equivalent re-materialization create a new semantic version/snapshot identity?
What changes must make two snapshots distinguishable?
What changes must not make them distinguishable?
Which definition facts participate in semantic identity?
Which source-state facts participate in snapshot identity/provenance?
How is successful empty materialization identified?
How does provenance explain the authoritative source state?
How does lineage relate snapshot, definition, and source observations?
Must identity be deterministic and content-derived?
If a digest is used, what semantic representation does it cover?
What canonicalization requirements are necessary before hashing?
Which operational facts are excluded from semantic identity?
How are collisions or algorithm/version evolution treated semantically?
What is deferred to WP04, WP06, WP07, WP08, and WP09?
```

The result must be deterministic, implementation-independent,
provider-independent, and storage-independent while being precise enough
for later implementation.

------------------------------------------------------------------------

## 4. WP03 Core Boundary

WP03 owns **identity, version, provenance, lineage, and semantic
distinguishability requirements**.

WP03 may select representation-level semantics only where the execution
plan and manifest assign them here, including a deterministic
identifier/digest strategy if required to make the model implementable.

WP03 does not own:

``` text
Application interfaces, request/result records, or public failure types
materialization use-case implementation
catalog object implementation beyond semantic requirements
SQLite tables, columns, indexes, DDL, or migrations
snapshot persistence behavior
catalog persistence or lookup behavior
DI/configuration
Worker execution
permanent tests
Release 1.3 pipelines
```

Do not let later physical storage concerns dictate identity semantics.

------------------------------------------------------------------------

## 5. Authorized Repository Scope

Use `RELEASE_1.2_FILE_MANIFEST.md` as the exact path authority.

Create or modify **only WP03 artifacts explicitly assigned by the
manifest**.

Expected WP03 character:

``` text
semantic architecture / decision documentation
possibly repository-owned identity/version/provenance specification if manifest-authorized
no production source
no permanent tests
no packages
no project references
no solution/build/script changes
```

The WP03 prompt pair itself is governance input and must not be
rewritten.

If the manifest provides no legal location for a required output, stop
and report the manifest gap.

------------------------------------------------------------------------

## 6. Prohibited Scope

Do not:

``` text
modify Domain production code
modify Application production code
modify Infrastructure production code
modify Worker production code
modify permanent or architecture tests
add/change packages
change project references
change solution membership
change build policy/scripts/workflows
define SQLite schema or write SQL
implement identifiers in code
implement hashing/canonicalization code
implement dataset materialization
implement snapshot persistence
implement catalog persistence/lookup
implement validation/failure mapping
implement DI/configuration
implement Worker dataset execution
define Release 1.3 pipelines
start WP04
stage, commit, push, branch, PR, merge, tag, or release
close milestone #53
edit issues #124–#136
change planning dependencies
change Project fields other than authorized #123 lifecycle
```

Do not repair unrelated drift.

------------------------------------------------------------------------

## 7. Starting-State Gate

Before changing issue #123:

1.  Verify repository identity and current Git state.
2.  Verify WP01 #121 and WP02 #122 are Closed/Done.
3.  Verify WP03 #123 is Open/Backlog.
4.  Verify WP04 #124 is Open/Backlog.
5.  Verify milestone #53 is Open.
6.  Verify WP03 dependency is exactly #122.
7.  Verify no dependency drift affecting WP03.
8.  Verify Release 1.3 implementation remains unstarted.
9.  Classify every staged, modified, and untracked path.
10. Verify accepted cumulative Release 1.2 governance and WP02
    definition artifacts are preserved.
11. Verify no conflicting WP03 identity/provenance artifact already
    exists.
12. Verify WP04 contracts have not preempted WP03 semantics.

If material ambiguity exists, stop before lifecycle mutation.

------------------------------------------------------------------------

## 8. Initial Technical Baseline

Before moving #123 to In Progress, run the repository's canonical
non-mutating baseline:

``` text
restore
format verification
build
permanent tests
architecture tests
eng/verify.ps1
git diff --check
git diff --cached --check
```

Record actual current counts.

Provider/network calls are prohibited except Git/GitHub metadata and
ordinary package restore if needed.

If baseline validation fails, do not start WP03.

------------------------------------------------------------------------

## 9. Research Method

Reconcile repository truth before deciding.

Inspect at least:

``` text
RESEARCH_DATASET_DEFINITION.md
Release 1.1 PriceObservation identity/fidelity semantics
Release 1.1 persistence idempotency/conflict semantics
Release 1.1 immutable-history behavior
existing versioning strategy documentation
existing data catalog documentation
existing data glossary and architecture
existing public contracts and naming conventions
Release 1.2 execution plan and file manifest
```

For every material conclusion classify it as:

``` text
existing repository fact
accepted WP02 semantic requirement
WP03 decision
deferred representation/implementation
```

Do not import an external data-versioning framework unless explicitly
required.

------------------------------------------------------------------------

## 10. Required Identity Vocabulary

Define precisely and non-circularly at least:

``` text
Dataset Definition Identity
Research Dataset Identity
Dataset Snapshot Identity
Dataset Version
Source State
Source-State Identity
Semantic Identity
Semantic Equivalence
Provenance
Lineage
Canonical Semantic Representation
Digest / Fingerprint (if selected)
Algorithm/Representation Version
```

Clarify which terms are logical concepts versus concrete
representations.

------------------------------------------------------------------------

## 11. Identity Layers

WP03 must explicitly separate identity layers.

At minimum evaluate and settle:

### Dataset Definition Identity

Represents the semantics of the declarative dataset definition.

It must be insensitive to irrelevant operational metadata.

Evaluate participation of:

``` text
exact target
[from, to) boundaries
inclusion semantics
ordering semantics
authorized deterministic definition parameters
future-compatible semantic representation version
```

### Source-State Identity

Represents the authoritative Release 1.1 source state relevant to the
definition.

It must not depend on:

``` text
SQLite row IDs
insertion order
database natural order
machine path
connection string
provider DTO identity
execution time
```

Evaluate whether it must cover the complete ordered selected observation
sequence including exact semantic instant, original offset
representation, and exact decimal value.

### Snapshot Identity

Represents one semantic materialization result.

Settle whether it is derived from:

``` text
definition identity
source-state identity
ordered materialized semantic content
semantic metadata required by WP02
representation/algorithm version
```

Avoid redundant inputs unless redundancy has an explicit integrity
purpose.

### Research Dataset Identity

Decide whether Release 1.2 needs a logical identity above individual
snapshots, and if so what remains stable across distinguishable
snapshots.

Do not invent mutable "latest" semantics.

------------------------------------------------------------------------

## 12. Definition Identity Semantics

Define exactly what makes two definitions semantically equivalent.

At minimum:

``` text
same exact target
same [from, to) boundaries
same inclusion semantics
same deterministic ordering rule
same authorized semantic parameters
same interpretation/version of the definition model
```

Exclude:

``` text
creation time
execution time
machine
process
file path
database path
connection string
display formatting
JSON property order or whitespace
culture/local timezone
```

If a canonical representation is selected, define semantic normalization
rules without tying them to physical persistence.

------------------------------------------------------------------------

## 13. Source-State Semantics

WP02 requires changed relevant source history to be distinguishable.

Define the relevant source state as the exact accepted observations
selected by the definition, in deterministic semantic order.

For each observation, evaluate identity contribution from:

``` text
exact target
absolute semantic instant
original offset representation
exact decimal price
```

Because WP02 explicitly preserves original offset representation and
decimal fidelity, do not silently omit them from equivalence or
provenance unless a compelling accepted authority supports doing so.

Clarify empty source state: an empty selected sequence is a valid,
deterministic source state and must be representable unambiguously.

------------------------------------------------------------------------

## 14. Version Semantics

Define what **Dataset Version** means.

Compare at least:

1.  mutable sequential revision number;
2.  timestamp-based version;
3.  deterministic semantic/content-derived version;
4.  hybrid logical identity + immutable snapshot fingerprint.

The selected model must satisfy:

``` text
determinism
reproducibility
immutability
distinguishability after relevant source change
equivalent re-materialization recognition
no wall-clock dependence
no overwrite semantics
offline/local operation
future algorithm evolution
```

Do not use execution timestamps as semantic versions.

If human-friendly sequential versions are useful only for catalog
presentation, classify them as non-authoritative/deferred unless the
plan requires otherwise.

------------------------------------------------------------------------

## 15. Equivalent Re-materialization

Settle this explicitly.

Given:

``` text
same semantic definition
same relevant authoritative source state
same deterministic semantic model
```

a repeated materialization must be semantically equivalent.

Decide whether it:

``` text
reuses the same deterministic snapshot identity
creates a new operational event but the same semantic snapshot identity
creates a new semantic version
```

Preferred direction: operational executions may differ, but equivalent
materializations resolve to the same semantic identity/version and must
not create distinguishable research evidence merely because they ran
twice.

If repository authority requires another model, follow it and document
why.

------------------------------------------------------------------------

## 16. Changed Source-State Semantics

When the same definition is materialized after the relevant accepted
source history changes:

``` text
old snapshot remains immutable
new materialization must be distinguishable if semantic content/source state differs
old identity/version must not be reassigned
new snapshot must carry provenance explaining the new source state
```

Define what counts as a relevant source-state change.

Examples to evaluate:

``` text
observation added inside [from, to)
observation added outside [from, to)
selected observation price changes through a legitimately distinct accepted source state
selected observation offset representation differs
unrelated target changes
database insertion order changes
database file moves
execution time changes
```

Only semantically relevant changes should alter snapshot identity.

------------------------------------------------------------------------

## 17. Canonical Semantic Representation

Determine whether deterministic content-derived identity requires a
canonical semantic representation.

If yes, specify canonical **semantic encoding requirements**
sufficiently for later implementation, while avoiding storage schema.

At minimum settle:

``` text
field order
sequence order
string encoding expectation
target exactness
timestamp representation
offset representation
decimal representation
boundary representation
empty sequence representation
domain separators/type tags
representation versioning
```

The representation must be unambiguous and culture-independent.

Do not use locale-sensitive formatting, floating point, database
serialization, provider JSON, or platform-specific binary layout.

If choosing a concrete serialization is unnecessary at WP03, define a
normative field/value canonicalization contract instead and explicitly
defer byte encoding.

------------------------------------------------------------------------

## 18. Digest / Fingerprint Decision

Evaluate whether a cryptographic digest is appropriate for deterministic
semantic identity.

Compare at least:

``` text
opaque random identifiers
GUIDs
sequential IDs
raw canonical semantic key
cryptographic content digest
```

For a public, zero-cost, offline reproducibility model, a
content-derived digest is a strong candidate.

If selecting a digest:

``` text
choose a standard non-secret cryptographic hash available without new package dependencies
state whether the digest is identity evidence, not a security/authentication mechanism
define algorithm naming/versioning
define textual representation requirements if needed
define collision handling policy semantically
do not implement it in code
```

Do not claim mathematical impossibility of collisions.

If the execution plan or manifest expects the digest choice to remain
deferred, honor that authority instead.

------------------------------------------------------------------------

## 19. Algorithm and Representation Evolution

The identity model must not become ambiguous if canonicalization or
digest algorithms evolve.

Define a semantic strategy such as:

``` text
identity scheme/version is explicit
algorithm identifier is explicit when applicable
old snapshots retain their original identity scheme
new schemes do not reinterpret old identifiers
equivalence across schemes, if ever needed, requires semantic comparison or explicit migration authority
```

Do not design a migration framework.

------------------------------------------------------------------------

## 20. Provenance Semantics

Provenance must explain **why this snapshot exists and from what
authoritative semantic state it was produced**.

At minimum determine what must be knowable:

``` text
dataset-definition identity/semantics
source-state identity
source authority = accepted Release 1.1 observations
exact target
selection boundaries
observation count
ordered semantic content or sufficient deterministic source-state evidence
identity/canonicalization scheme version
materialization semantic result identity
```

Separate semantic provenance from operational audit facts.

Operational facts such as execution timestamp may be useful
catalog/audit metadata but must not affect semantic identity unless
explicitly required.

Do not persist provenance here.

------------------------------------------------------------------------

## 21. Lineage Semantics

Define lineage relationships conceptually.

At minimum:

``` text
snapshot derives from one dataset definition
snapshot derives from one relevant authoritative source state
source state consists of zero or more accepted Release 1.1 observations selected by the definition
definition and source state together explain the snapshot
```

For empty materializations, lineage must still relate the snapshot to
its definition and an explicitly empty relevant source state.

Do not require one physical lineage row per observation unless later
work chooses that representation.

------------------------------------------------------------------------

## 22. Identity and Immutability Invariants

Establish invariants suitable for later contracts/tests.

At minimum:

``` text
same semantic definition => same definition identity
same relevant ordered source state => same source-state identity
same definition + same relevant source state => same semantic snapshot identity
operational metadata changes alone => no semantic identity change
relevant source semantic change => distinguishable source/snapshot identity
definition semantic change => distinguishable definition/snapshot identity
accepted snapshot identity is immutable
an identity is never reassigned to different semantic content
old snapshots are never overwritten by newer versions
empty materialization has deterministic identity
```

If the chosen model makes any invariant invalid, explain and replace it
explicitly.

------------------------------------------------------------------------

## 23. Collision / Integrity Semantics

If content digests are selected, define bounded collision behavior.

At minimum:

``` text
digest equality is expected to imply semantic equivalence under the same identity scheme
if stored semantic metadata/content contradicts a supposedly equal digest, treat it as an integrity conflict, never overwrite
collision handling must preserve both evidence and fail deterministically
no silent aliasing of materially different snapshots
```

WP11 owns concrete validation/failure mapping.

------------------------------------------------------------------------

## 24. Metadata and Catalog Boundary

WP03 must hand WP06 enough semantics to model catalog metadata without
designing the catalog.

Identify which identity/provenance facts must be catalog-visible, such
as:

``` text
logical dataset/definition identity
snapshot identity/version
source-state identity
target
selection boundaries
observation count
identity scheme/version
provenance relation
```

Defer:

``` text
catalog classes/records if not assigned to WP03
indexes
lookup APIs
SQLite schema
storage layout
pagination/query behavior
```

------------------------------------------------------------------------

## 25. Application Contract Boundary

WP04 will translate WP03 semantics into Application-owned contracts.

Provide WP04 with explicit requirements for:

``` text
identity values that Application must be able to carry
definition semantics
snapshot/version semantics
provenance facts
lineage relationships
equivalence/distinguishability invariants
empty snapshot identity
storage/provider independence
```

Do not create the interfaces or result records in WP03.

------------------------------------------------------------------------

## 26. Physical Storage Boundary

WP07 owns the physical storage model.

WP03 must not specify:

``` text
table names
column names
SQLite types
indexes
foreign keys
DDL
migration mechanics
file paths
```

It may require lossless persistence of selected semantic
identity/provenance values.

WP08 owns immutable snapshot persistence. WP09 owns catalog persistence
and lookup.

------------------------------------------------------------------------

## 27. Decision Alternatives

For each material decision, document:

``` text
decision
alternatives considered
selected option
rationale
consequences
deferred implementation
```

At minimum compare:

1.  random/sequential identity vs deterministic semantic identity;
2.  definition identity vs snapshot identity conflation;
3.  execution timestamp version vs semantic/content-derived version;
4.  source-state identity from physical rows vs semantic observation
    content;
5.  equivalent re-materialization as new version vs same semantic
    version;
6.  operational metadata included vs excluded from identity;
7.  canonical semantic representation versioned vs implicit;
8.  digest-based identity vs non-digest deterministic representation, if
    applicable.

Keep the analysis bounded to Release 1.2.

------------------------------------------------------------------------

## 28. Required WP04 Handoff Contract

End the WP03 artifact with an explicit handoff to WP04.

WP04 must receive settled answers for:

``` text
which identity concepts exist
which are stable across snapshots
which identify immutable snapshots
what version means
what constitutes semantic equivalence
what changes require a new/distinguishable snapshot identity
what provenance must carry
what lineage must express
what operational metadata is excluded
how empty materialization is identified
what canonicalization/digest semantics are normative, if selected
what identity-scheme/version metadata must be carried
```

Explicitly leave these to later WPs:

``` text
Application interface/type shape — WP04
materialization orchestration — WP05
catalog metadata object model — WP06
physical schema — WP07
snapshot persistence — WP08
catalog persistence/lookup — WP09
integration consistency mechanics — WP10
validation/failure mapping — WP11
DI/Worker execution — WP12
tests — WP13/WP14
```

------------------------------------------------------------------------

## 29. Issue #123 Lifecycle

Only after starting-state and baseline gates pass:

1.  Move issue #123 `Backlog → In Progress`.
2.  Perform only authorized WP03 semantic work.
3.  Validate artifacts against this authority and manifest.
4.  Rerun final technical/scope checks.
5.  Post concise evidence to #123.
6.  Close #123.
7.  Set Project Status to `Done`.

Do not modify #124 except to inspect and prove it remains Open/Backlog.

------------------------------------------------------------------------

## 30. Validation Requirements

Before closing WP03, run:

``` text
dotnet restore AIQuantTradingResearch.slnx
dotnet format AIQuantTradingResearch.slnx --verify-no-changes
dotnet build AIQuantTradingResearch.slnx --no-restore
dotnet test AIQuantTradingResearch.slnx --no-build
eng/verify.ps1
git diff --check
git diff --cached --check
```

Also verify:

``` text
Domain production delta: 0
Application production delta: 0
Infrastructure production delta: 0
Worker production delta: 0
permanent test delta: 0
package delta: 0
project-reference delta: 0
solution/build/script delta: 0
WP04 started: NO
Release 1.3 implementation started: NO
provider/network calls during WP03 validation: 0
temporary database residue: 0
```

Run existing repository-relative Markdown/link validation applicable to
new documentation.

For untracked WP03 artifacts, perform an equivalent direct whitespace
check if Git diff checks do not inspect them.

------------------------------------------------------------------------

## 31. Semantic Acceptance Matrix

WP03 passes only if all applicable rows are established:

  Requirement                                  Required result
  -------------------------------------------- ---------------------------------------
  Definition identity semantics                PASS
  Research dataset logical identity decision   PASS
  Snapshot identity semantics                  PASS
  Dataset version semantics                    PASS
  Source-state semantics                       PASS
  Equivalent re-materialization                PASS
  Relevant source change distinguishability    PASS
  Irrelevant operational change exclusion      PASS
  Empty materialization identity               PASS
  Exact target semantics                       PASS
  `[from, to)` boundaries preserved            PASS
  Ascending semantic ordering preserved        PASS
  Timestamp/offset fidelity preserved          PASS
  Decimal fidelity preserved                   PASS
  Canonical semantic representation decision   PASS
  Digest/fingerprint decision                  PASS or explicitly authority-deferred
  Identity scheme evolution semantics          PASS
  Provenance semantics                         PASS
  Lineage semantics                            PASS
  Snapshot immutability                        PASS
  Identity reassignment prohibited             PASS
  Collision/integrity semantics                PASS if digest selected
  WP04 handoff complete                        PASS
  WP06/WP07/WP08/WP09 boundaries preserved     PASS
  Release 1.3 excluded                         PASS
  Cross-document contradictions                0

------------------------------------------------------------------------

## 32. Mutation Acceptance Matrix

  Requirement                          Required result
  ------------------------------------ --------------------------
  WP02 predecessor                     CLOSED / DONE
  Issue #123 initial state             OPEN / BACKLOG
  Issue #124 initial/final state       OPEN / BACKLOG
  Milestone #53                        OPEN
  Authorized WP03 paths                only manifest-authorized
  Production code delta                0
  Permanent test delta                 0
  Package/reference delta              0/0
  Solution/build/script delta          0
  Unauthorized documentation delta     0
  Restore                              PASS
  Format verification                  PASS
  Build                                PASS / 0 errors
  Permanent tests                      PASS
  Architecture tests                   PASS
  Canonical verification               PASS
  Diff/whitespace checks               PASS
  Provider/network calls               0
  Temporary database residue           0
  WP04 started                         NO
  Release 1.3 implementation started   NO
  Issue #123 final state               CLOSED / DONE

------------------------------------------------------------------------

## 33. Blocker Policy

Stop if:

``` text
WP02 is not Closed/Done
#123 is not Open/Backlog at start
#124 has already started
milestone #53 is not the authoritative open Release 1.2 milestone
repository state contains unexplained material drift
Release 1.3 implementation has started
the manifest provides no authorized location for required WP03 output
authorities materially conflict
baseline restore/build/test/architecture/canonical verification fails
WP02 semantics are insufficient or contradictory for deterministic identity
a required decision would necessarily preempt WP04+ implementation rather than specify semantics
```

Report:

``` text
blocker ID
evidence
authority violated
smallest corrective authority required
repository/GitHub mutations avoided
safe resume point
```

Do not create a recursive authority chain for ordinary wording or
whitespace findings. Correct findings inside WP03-authorized artifacts
before acceptance when the correction stays within this authority.

------------------------------------------------------------------------

## 34. Required Execution Report

Produce a structured report containing at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor / Lifecycle Gates
7.  Issue #123 Lifecycle
8.  Initial Technical Baseline
9.  WP02 Semantics Reconciled
10. Identity Vocabulary
11. Identity-Layer Model
12. Dataset Definition Identity
13. Research Dataset Logical Identity
14. Source-State Identity
15. Snapshot Identity
16. Dataset Version Semantics
17. Equivalent Re-materialization
18. Changed Source-State Semantics
19. Canonical Semantic Representation
20. Digest / Fingerprint Decision
21. Algorithm / Representation Evolution
22. Provenance Semantics
23. Lineage Semantics
24. Empty Materialization Identity
25. Identity / Immutability Invariants
26. Collision / Integrity Semantics
27. Metadata / Catalog Boundary
28. Application Contract Boundary
29. Physical Storage Boundary
30. Alternatives and Decisions
31. WP04 Handoff
32. Exact Files Added / Modified
33. Production / Test Delta
34. Package / Reference Delta
35. Security / Offline Evidence
36. Whitespace / Link Evidence
37. Restore / Build / Test Evidence
38. Canonical Verification
39. Architecture Validation
40. Mutation Accounting
41. Git / GitHub Protection
42. Findings / Blockers
43. Semantic Acceptance Matrix
44. Final Repository / GitHub State
45. Final Decision
46. Next Authorized Work Package

Use actual observed evidence, not assumed values.

------------------------------------------------------------------------

## 35. Required Terminal Summary

If successful, end exactly with:

``` text
RELEASE 1.2 WP03 COMPLETE

DATASET IDENTITY, VERSION & PROVENANCE SEMANTICS:
Dataset definition identity: PASS
Research dataset identity model: PASS
Source-state identity: PASS
Snapshot identity: PASS
Dataset version semantics: PASS
Equivalent re-materialization semantics: PASS
Relevant source-change distinguishability: PASS
Operational metadata excluded from semantic identity: PASS
Empty materialization identity: PASS
Canonical semantic representation: PASS
Digest/fingerprint decision: PASS
Identity scheme evolution: PASS
Provenance semantics: PASS
Lineage semantics: PASS
Snapshot immutability: PASS
Identity reassignment prohibited: PASS
Timestamp/offset/decimal fidelity: PASS
WP04 contract handoff: PASS
Production code delta: 0
Permanent test delta: 0
Package/reference delta: 0/0
WP04 started: NO
Release 1.3 implementation started: NO
Issue #123: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP04 — Application Dataset Contracts
GitHub issue #124
```

If the accepted authority explicitly defers a digest choice, replace
only the digest terminal row with:

``` text
Digest/fingerprint decision: AUTHORITY-DEFERRED
```

If blocked, end exactly with:

``` text
RELEASE 1.2 WP03 BLOCKED
```

Do not print the success marker unless every mandatory gate passes.

------------------------------------------------------------------------

## 36. Final Constraint

WP03 is the semantic bridge between WP02 reproducibility and all later
Release 1.2 contracts and persistence.

Prefer **deterministic semantic identity over operational identity**,
preserve immutable research evidence, and ensure equivalent
re-materializations do not become falsely distinct because of execution
circumstances.

Do not optimize for SQLite. Do not design APIs. Do not create pipeline
behavior.

**Identity must explain meaning, version must explain semantic change,
provenance must explain origin, and lineage must explain derivation.**

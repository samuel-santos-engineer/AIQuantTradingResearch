# Release 1.2 WP02 --- Research Dataset Definition & Reproducibility Model --- Codex Prompt

## Role

Act as the **WP02 Research Dataset Definition & Reproducibility Model
Executor** for Release 1.2 of `AIQuantTradingResearch`.

This is a bounded **research, definition, and decision** work package.
Its purpose is to establish the authoritative conceptual model for a
deterministic, versioned, reproducible research dataset built from the
accepted Release 1.1 historical-observation foundation.

Use **GPT-5.6 Sol** for this work package.

Do not implement dataset behavior. Do not start WP03.

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
```

Also inspect:

``` text
accepted WP01 execution result
accepted Release 1.1 persistence semantics and implementation
current Domain/Application/Infrastructure/Worker contracts
current data architecture and glossary documentation
current repository and test state
GitHub milestone #53
GitHub issue #122
GitHub issue #123
Project #2
```

Authority precedence:

``` text
1. RELEASE_1.2_EXECUTION_PLAN.md
2. RELEASE_1.2_FILE_MANIFEST.md
3. Accepted Release 1.2 GitHub planning state
4. Accepted WP01 result
5. Accepted Release 1.1 repository truth
6. Existing repository architecture/governance conventions
7. This execution prompt
```

If authorities materially conflict, stop and report the smallest precise
blocker. Do not silently reconcile contradictory authority.

------------------------------------------------------------------------

## 2. Accepted Starting Baseline

Treat the accepted WP01 result as the starting baseline unless current
evidence proves drift:

``` text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Branch: main
WP01 accepted HEAD: 3ae8ba300fcd356b71fb4fdef5258dc23a99abeb
local main = origin/main at WP01 completion
staged paths: 0
tracked modifications: 0

Release 1.1:
  PR #120: MERGED
  milestone #52: CLOSED
  issues #103–#118: CLOSED / DONE
  persistence regression baseline: PASS

Release 1.2:
  milestone #53: OPEN
  issue #121: CLOSED / DONE
  issue #122: OPEN / BACKLOG
  issue #123: OPEN / BACKLOG
  dependency drift: 0
  legacy milestone #43: CLOSED / EMPTY
  Release 1.3 implementation: NOT STARTED

Technical baseline:
  .NET SDK: 10.0.103
  target framework: net10.0
  Domain.Tests: 11/11
  Application.Tests: 42/42
  Infrastructure.Tests: 79/79
  Architecture.Tests: 13/13
  total permanent tests: 145/145
  canonical verification: PASS
```

The SHA and test counts above are historical WP01 evidence. Re-observe
current truth; do not reset or falsify current state to match them.

------------------------------------------------------------------------

## 3. Objective

Define the Release 1.2 **Research Dataset** concept and its
**reproducibility model** precisely enough that WP03--WP16 can build on
it without reopening foundational meaning.

WP02 must answer, at minimum:

``` text
What is a research dataset in this platform?
What is explicitly not a research dataset?
What source material may participate in a dataset?
What constitutes the dataset definition/specification?
What constitutes a materialized dataset?
What is a dataset snapshot?
What makes a materialization deterministic?
What does reproducibility mean?
What inputs must be fixed for reproducibility?
What outputs must be invariant for equivalent materializations?
How are observation selection boundaries expressed conceptually?
How are ordering and normalization treated?
How does Release 1.1 historical observation truth participate?
What happens when source history changes after a dataset is materialized?
What must be immutable versus recomputable?
What metadata is conceptually required to explain a materialization?
What belongs to WP02 versus WP03 identity/version/provenance?
What belongs to WP06 catalog metadata?
What belongs to WP07+ physical storage?
What belongs to Release 1.3 pipelines and is therefore excluded?
```

The result must be sufficiently deterministic to constrain later
implementation while remaining **storage-independent,
provider-independent, and implementation-independent**.

------------------------------------------------------------------------

## 4. WP02 Core Boundary

WP02 owns **definition and reproducibility semantics**.

WP02 does not own:

``` text
final dataset identifier representation
final version identifier representation
hash/digest algorithm selection
canonical serialization format
provenance identifier encoding
lineage persistence representation
Application interfaces or result types
materialization use-case implementation
catalog object implementation
SQLite tables/columns/indexes/schema
snapshot persistence implementation
catalog persistence/lookup implementation
failure mapping implementation
DI registration
Worker execution
permanent tests
Release 1.3 ingestion/pipeline orchestration
```

Where WP02 needs a concept later owned by WP03+, define the **semantic
requirement** without selecting the later work package's representation.

Example:

``` text
Allowed in WP02:
"Equivalent materializations must be distinguishable from materially different dataset definitions."

Not allowed in WP02:
"The dataset ID is SHA-256 over canonical JSON."
```

------------------------------------------------------------------------

## 5. Authorized Repository Scope

Use `RELEASE_1.2_FILE_MANIFEST.md` as the exact path authority.

You may create or modify **only the WP02 research/definition artifacts
explicitly assigned to WP02 by the manifest**.

Do not invent alternative paths merely because they seem convenient.

If the manifest permits a WP02 definition artifact but does not fully
prescribe its content, produce the smallest coherent authoritative
artifact set necessary to capture the decisions in this prompt.

Expected character of WP02 artifacts:

``` text
research/definition/decision documentation
no production source
no permanent test source
no package changes
no project-reference changes
no solution/build/script changes
no GitHub workflow changes
```

The WP02 prompt pair itself is governance input and must not be
rewritten.

If the manifest provides no legal location for a required WP02 output,
stop and report the manifest gap rather than writing outside authority.

------------------------------------------------------------------------

## 6. Prohibited Scope

Do not:

``` text
modify Domain production code
modify Application production code
modify Infrastructure production code
modify Worker production code
modify permanent tests
modify architecture tests
add packages
change package versions
change project references
change solution membership
change build policy
change engineering scripts
change GitHub workflows
define SQLite schema
write SQL
create database files as repository artifacts
implement dataset materialization
implement snapshot persistence
implement catalog persistence
implement lookup behavior
implement dataset validation/failure mapping
implement DI/configuration
implement Worker dataset execution
define Release 1.3 pipelines
start WP03
stage
commit
push
create a branch
create a PR
merge
tag
create a GitHub Release
close milestone #53
edit issues #123–#136
change planning dependencies
change Project fields other than authorized #122 lifecycle
```

Do not repair unrelated repository drift.

------------------------------------------------------------------------

## 7. Starting-State Gate

Before changing issue #122:

1.  Verify repository identity and current Git state.
2.  Verify WP01 issue #121 is Closed/Done.
3.  Verify WP02 issue #122 is Open/Backlog.
4.  Verify WP03 issue #123 is Open/Backlog.
5.  Verify milestone #53 is Open.
6.  Verify WP02 dependency is exactly WP01.
7.  Verify no dependency drift affecting WP02.
8.  Verify Release 1.3 implementation remains unstarted.
9.  Classify all staged, modified, and untracked paths.
10. Verify accepted cumulative Release 1.2 governance artifacts are
    preserved.
11. Verify no WP02 semantic artifact already exists in a conflicting or
    ambiguous state.
12. Verify no WP03 implementation/decision artifact has already
    preempted WP02.

If any material ambiguity exists, stop before lifecycle mutation.

------------------------------------------------------------------------

## 8. Initial Technical Baseline

Before moving #122 to In Progress, run the canonical non-mutating
baseline appropriate to the repository:

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

If baseline validation fails, do not start WP02.

------------------------------------------------------------------------

## 9. Research Method

Base the definition on repository truth, not generic dataset theory
alone.

Inspect and reconcile at least:

``` text
Release 1.1 PriceObservation semantics
historical observation persistence semantics
target identity behavior
semantic instant identity
timestamp/offset fidelity
decimal fidelity
retrieval ordering
immutable-history behavior
idempotency/conflict semantics
successful empty retrieval
current data architecture documentation
current domain glossary/business concepts
current Application boundaries
Release 1.2 execution plan
Release 1.2 file manifest
```

You may reason from established software/data principles where
repository authority leaves a genuine design choice, but clearly
distinguish:

``` text
existing repository fact
Release 1.2 requirement
WP02 decision
deferred decision
```

Do not browse for or adopt an external dataset framework unless the
execution plan explicitly requires one. WP02 should remain
repository-native and zero-cost.

------------------------------------------------------------------------

## 10. Required Dataset Vocabulary

Establish precise definitions for at least:

``` text
Research Dataset
Dataset Definition
Dataset Materialization
Dataset Snapshot
Source Observation
Selection Boundary
Reproducibility
Determinism
Equivalent Materialization
Dataset Metadata
Provenance
Lineage
Catalog
```

Definitions must avoid circularity.

Where terms belong primarily to later WPs, WP02 should define only
enough meaning to establish boundaries and defer representation.

Use existing repository terminology where it already has authoritative
meaning.

------------------------------------------------------------------------

## 11. Research Dataset Definition

The definition must establish that a Release 1.2 research dataset is a
**research-owned, deterministic materialization of explicitly selected
accepted market observations plus the metadata necessary to explain how
that materialization was produced**.

Reconcile this statement against repository truth rather than copying it
blindly.

At minimum determine whether the dataset is:

``` text
a live query or a durable research artifact
provider transport data or normalized platform data
mutable working state or immutable materialized state
defined by source observations alone or by observations plus selection/transformation rules
allowed to depend on current wall-clock time implicitly
allowed to depend on provider ordering implicitly
allowed to contain observations rejected by Release 1.1 semantics
allowed to rewrite accepted historical observations
```

The preferred direction is:

``` text
not provider transport
not a live provider query
not an implicit "latest data" view
not permission to mutate Release 1.1 history
based on accepted normalized historical observations
explicitly bounded
deterministically ordered
materialized as immutable research evidence
```

If repository authority contradicts any preferred direction, follow
repository authority and document the conflict.

------------------------------------------------------------------------

## 12. Definition vs Materialization vs Snapshot

WP02 must separate three concepts.

### Dataset Definition

The declarative intent/rules that determine what dataset should be
produced.

Conceptually consider:

``` text
source target(s)
selection interval/boundaries
inclusion semantics
ordering semantics
any Release 1.2-authorized deterministic transformation/filter semantics
definition-level parameters
```

Do not invent transformations not required by the accepted Release 1.2
scope.

### Dataset Materialization

The act/result of resolving a dataset definition against accepted source
history.

A materialization must not be confused with the definition itself.

### Dataset Snapshot

The durable immutable representation of one successful materialization.

WP08 owns persistence mechanics. WP02 owns only the semantic
distinction.

State explicitly whether a successful materialization with zero selected
observations is valid, invalid, or deferred. Make the decision
consistent with Release 1.1 successful-empty retrieval and the Release
1.2 purpose.

Do not let physical storage considerations dictate this semantic
decision.

------------------------------------------------------------------------

## 13. Reproducibility Model

Define reproducibility operationally.

A strong default requirement is:

> Given the same authoritative dataset definition and the same
> authoritative source-observation state relevant to that definition,
> materialization must produce semantically equivalent dataset content
> and explanatory metadata independent of execution time, process
> instance, machine, provider response order, or database row-return
> order.

Refine this into repository-specific requirements.

Explicitly decide treatment of:

``` text
execution timestamp
wall-clock "now"
machine path
process ID
random values
unordered collections
database natural row order
provider response order
locale/culture
timezone conversion
floating-point conversion
current provider state
environment-specific configuration
```

Any non-deterministic operational metadata that may legitimately differ
between executions must be clearly separated from the semantic content
used to determine equivalence.

Do not select the final canonical byte representation or digest
algorithm; WP03 owns identity/version/provenance representation.

------------------------------------------------------------------------

## 14. Reproducibility Inputs

Identify the minimal semantic inputs that must be fixed to reproduce a
materialization.

At minimum evaluate:

``` text
dataset definition
target identity
selection boundaries
accepted source observations
source observation semantic instants
source observation values
ordering rule
definition parameters
transformation/filter rules, if any are actually in Release 1.2 scope
```

For every proposed input classify it as:

``` text
required semantic input
explanatory metadata only
operational metadata only
deferred to later WP
out of scope
```

Do not make a physical database row ID, file path, connection string,
provider DTO, or machine-specific value part of dataset semantics.

------------------------------------------------------------------------

## 15. Source Observation Authority

Release 1.2 must reuse Release 1.1 accepted historical observations
rather than redefine market-observation truth.

Establish:

``` text
Release 1.1 persisted observations are the authoritative normalized source material for Release 1.2 datasets
provider DTOs are not dataset source truth
dataset materialization does not rewrite source observations
dataset materialization does not repair source history
source identity retains exact target + semantic instant meaning
source timestamp/offset and decimal fidelity must not be degraded
source retrieval ordering must not be treated as accidental
```

Clarify whether dataset semantic content requires preserving the
original offset representation in addition to the absolute instant.
Follow accepted Release 1.1 fidelity semantics and avoid silently
weakening them.

------------------------------------------------------------------------

## 16. Selection-Boundary Semantics

Define conceptual selection boundaries without choosing storage or API
representation.

Decide:

``` text
whether time boundaries are explicit
whether lower/upper boundaries are inclusive or exclusive
whether an unbounded side is allowed
whether target selection is exact
whether multiple targets are in Release 1.2 scope
how empty selections behave
whether selection depends on insertion order
```

Use the execution plan and accepted definition to determine whether
Release 1.2 is single-target or multi-target. Do not broaden scope
merely for future flexibility.

Boundary semantics must be deterministic and testable by later WPs.

------------------------------------------------------------------------

## 17. Ordering and Canonical Semantic Content

Define semantic ordering independently of physical storage.

At minimum:

``` text
dataset observation order must be deterministic
database natural order is not authoritative
provider response order is not authoritative
semantic instant ordering from Release 1.1 must be preserved where applicable
duplicate semantic identities must not be introduced by dataset materialization
```

If multi-target datasets are in scope, define a deterministic
cross-target ordering rule at the semantic level or explicitly defer it
only if the accepted Release 1.2 definition guarantees single-target
datasets.

Do not choose a serialization format.

------------------------------------------------------------------------

## 18. Source-History Change Model

This is a mandatory WP02 decision.

Distinguish:

``` text
dataset definition stability
source-history state
materialization event
materialized snapshot
```

Decide what happens when accepted Release 1.1 history changes after a
dataset was materialized.

The model must support both facts:

1.  A previously materialized snapshot is immutable research evidence.
2.  Re-materializing the same definition against a materially changed
    authoritative source state may legitimately produce a different
    materialization.

Do not solve this by inventing the final version/provenance identifier.
WP03 owns how distinct states are identified and related.

Define the semantic requirement WP03 must satisfy.

------------------------------------------------------------------------

## 19. Equivalent Materialization

Define semantic equivalence precisely enough for WP03 and later tests.

Evaluate whether equivalence requires equality of:

``` text
dataset definition semantics
selected target(s)
selection boundaries
ordered observation identities
ordered observation values
timestamp/offset fidelity
definition-level parameters
deterministic transformations
semantic metadata
operational execution metadata
```

Operational facts such as execution duration or process identity should
not make otherwise equivalent datasets semantically different unless
repository authority explicitly requires that.

State which fields must be excluded from equivalence.

Do not define hash bytes.

------------------------------------------------------------------------

## 20. Immutability and Re-materialization

Define:

``` text
whether a materialized snapshot is immutable
whether an existing snapshot may be overwritten
whether re-materialization creates/reuses an equivalent semantic result
whether a changed source state may replace an older snapshot
whether old snapshots remain valid historical research evidence
```

Preferred invariant:

``` text
accepted snapshots are immutable
re-materialization never rewrites historical evidence
equivalent re-materialization may be recognized as equivalent
materially different re-materialization must remain distinguishable
```

WP08/WP09 own persistence mechanics; WP03 owns
identity/version/provenance representation.

------------------------------------------------------------------------

## 21. Metadata, Provenance, Lineage, and Catalog Boundary

Define the semantic boundary among:

### Dataset Metadata

Descriptive information needed to understand the dataset.

### Provenance

Evidence of the authoritative source state and definition from which the
materialization arose.

### Lineage

The relationship between the dataset and its source observations/inputs.

### Catalog

The discoverable record/index of dataset materializations and their
metadata.

WP02 must identify **what must be knowable**.

WP03 owns identity/version/provenance semantics in detail.

WP06 owns the metadata/catalog model.

WP09 owns catalog persistence and lookup.

Do not preempt those designs with storage structures or concrete
identifiers.

------------------------------------------------------------------------

## 22. Failure and Validation Boundary

WP02 may identify semantic invalidity conditions necessary to define
reproducibility, for example:

``` text
ambiguous definition
invalid selection boundaries
unsupported target scope
non-deterministic definition input
source state that cannot satisfy required fidelity
```

Do not create public failure enums or Infrastructure exception mappings.

WP11 owns validation and failure mapping.

Distinguish:

``` text
invalid dataset definition
valid definition producing empty dataset
materialization operational failure
source-data semantic failure
```

Only define categories conceptually where required.

------------------------------------------------------------------------

## 23. Release 1.3 Pipeline Exclusion

Release 1.2 is the Research Dataset Foundation, not the pipeline
release.

Explicitly exclude:

``` text
continuous ingestion pipelines
scheduled dataset refresh
event-driven rematerialization
automatic recomputation
stream processing
pipeline DAG orchestration
background dataset monitoring
production scheduling
pipeline retries/resilience
```

A bounded execution path later owned by WP12 may demonstrate dataset
execution, but it must not become Release 1.3 pipeline infrastructure.

------------------------------------------------------------------------

## 24. Decision Alternatives

For each material WP02 decision, document:

``` text
decision
alternatives considered
selected option
rationale
consequences
deferred representation/implementation
```

At minimum compare alternatives for:

1.  live-query dataset vs durable materialized dataset
2.  mutable dataset vs immutable snapshot
3.  implicit current source state vs explicit reproducibility inputs
4.  execution metadata included vs excluded from semantic equivalence
5.  overwrite-on-rematerialization vs preserve historical snapshots
6.  empty selection invalid vs valid empty materialization
7.  source-provider data vs accepted Release 1.1 observations as source
    truth

Keep analysis proportional. Do not create speculative architecture for
later releases.

------------------------------------------------------------------------

## 25. Required WP03 Handoff Contract

End the WP02 definition artifacts with an explicit handoff to WP03.

WP03 must receive settled semantic requirements for:

``` text
what needs identity
what constitutes semantic equivalence
what changes require distinguishability
what source-state facts provenance must explain
what lineage must relate
what must remain immutable
what operational metadata must not affect semantic identity
what definition/source changes may produce a distinct materialization
```

WP02 must explicitly leave these representation decisions open for WP03:

``` text
identifier shape
version shape
digest/hash choice
canonical serialization
provenance encoding
lineage identifier encoding
physical persistence representation
```

If WP02 accidentally decides these, revise the WP02 artifact before
acceptance.

------------------------------------------------------------------------

## 26. Issue #122 Lifecycle

Only after the starting-state and initial technical baseline pass:

1.  Move issue #122 from `Backlog` to `In Progress`.
2.  Perform the authorized WP02 research/definition work.
3.  Validate the artifacts against this authority and the file manifest.
4.  Rerun final technical and scope checks.
5.  Post concise evidence to issue #122.
6.  Close issue #122.
7.  Set Project Status to `Done`.

Do not modify #123 except to inspect and prove it remains Open/Backlog.

------------------------------------------------------------------------

## 27. Validation Requirements

Before closing WP02, run:

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
WP03 started: NO
Release 1.3 implementation started: NO
provider/network calls during WP02 validation: 0
temporary database residue: 0
```

Run any existing repository-relative Markdown/link validation applicable
to newly created documentation.

Do not introduce a new validator merely for WP02.

------------------------------------------------------------------------

## 28. Semantic Acceptance Matrix

WP02 passes only if the accepted artifacts establish all applicable
rows:

  Requirement                                              Required result
  -------------------------------------------------------- -----------------
  Research Dataset defined                                 PASS
  Dataset Definition defined                               PASS
  Materialization defined                                  PASS
  Snapshot defined                                         PASS
  Source truth is Release 1.1 accepted observations        PASS
  Provider transport excluded from source truth            PASS
  Selection boundaries deterministic                       PASS
  Ordering deterministic                                   PASS
  Reproducibility operationally defined                    PASS
  Required reproducibility inputs classified               PASS
  Non-semantic operational metadata separated              PASS
  Equivalent materialization defined                       PASS
  Source-history change behavior defined                   PASS
  Snapshot immutability defined                            PASS
  Re-materialization behavior defined                      PASS
  Empty selection/materialization semantics decided        PASS
  Timestamp/offset fidelity preserved                      PASS
  Decimal fidelity preserved                               PASS
  Dataset does not mutate source history                   PASS
  Metadata/provenance/lineage/catalog boundaries defined   PASS
  WP03 representation decisions deferred                   PASS
  WP06 catalog implementation deferred                     PASS
  WP07+ physical storage deferred                          PASS
  Release 1.3 pipelines excluded                           PASS
  Cross-document contradictions                            0

------------------------------------------------------------------------

## 29. Mutation Acceptance Matrix

  Requirement                          Required result
  ------------------------------------ --------------------------
  WP01 predecessor                     CLOSED / DONE
  Issue #122 initial state             OPEN / BACKLOG
  Issue #123 initial/final state       OPEN / BACKLOG
  Milestone #53                        OPEN
  Authorized WP02 artifact paths       only manifest-authorized
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
  Diff checks                          PASS
  Provider/network calls               0
  Temporary database residue           0
  WP03 started                         NO
  Release 1.3 implementation started   NO
  Issue #122 final state               CLOSED / DONE

------------------------------------------------------------------------

## 30. Blocker Policy

Stop if:

``` text
WP01 is not Closed/Done
#122 is not Open/Backlog at start
#123 has already started
milestone #53 is not the authoritative open Release 1.2 milestone
repository state contains unexplained material drift
Release 1.3 implementation has started
the file manifest provides no authorized location for required WP02 outputs
authorities materially conflict
baseline restore/build/test/architecture/canonical verification fails
a semantic decision cannot be made without preempting WP03 or another later WP
the accepted Release 1.1 observation foundation is insufficient for the Release 1.2 definition
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
whitespace findings. Correct findings inside WP02-authorized artifacts
before acceptance when the correction stays inside this authority.

------------------------------------------------------------------------

## 31. Required Execution Report

Produce a structured report containing at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor / Lifecycle Gates
7.  Issue #122 Lifecycle
8.  Initial Technical Baseline
9.  Existing Data / Persistence Semantics Reconciled
10. Research Dataset Definition
11. Dataset Definition Model
12. Materialization Model
13. Snapshot Model
14. Reproducibility Definition
15. Reproducibility Inputs
16. Source Observation Authority
17. Selection-Boundary Semantics
18. Ordering Semantics
19. Empty Materialization Decision
20. Source-History Change Model
21. Equivalent Materialization Semantics
22. Immutability / Re-materialization Model
23. Metadata / Provenance / Lineage / Catalog Boundary
24. Validation / Failure Concept Boundary
25. Release 1.3 Exclusions
26. Alternatives and Decisions
27. WP03 Handoff
28. Exact Files Added / Modified
29. Production / Test Delta
30. Package / Reference Delta
31. Security / Offline Evidence
32. Whitespace / Link Evidence
33. Restore / Build / Test Evidence
34. Canonical Verification
35. Architecture Validation
36. Mutation Accounting
37. Git / GitHub Protection
38. Findings / Blockers
39. Semantic Acceptance Matrix
40. Final Repository / GitHub State
41. Final Decision
42. Next Authorized Work Package

Use actual observed evidence, not assumed values.

------------------------------------------------------------------------

## 32. Required Terminal Summary

If successful, end exactly with:

``` text
RELEASE 1.2 WP02 COMPLETE

RESEARCH DATASET DEFINITION & REPRODUCIBILITY MODEL:
Research Dataset definition: PASS
Dataset Definition / Materialization / Snapshot separation: PASS
Release 1.1 observations as source truth: PASS
Deterministic selection: PASS
Deterministic ordering: PASS
Reproducibility model: PASS
Equivalent materialization semantics: PASS
Source-history change model: PASS
Snapshot immutability: PASS
Empty materialization semantics: PASS
Timestamp/offset/decimal fidelity: PASS
Metadata/provenance/lineage/catalog boundaries: PASS
WP03 representation decisions deferred: PASS
Production code delta: 0
Permanent test delta: 0
Package/reference delta: 0/0
WP03 started: NO
Release 1.3 implementation started: NO
Issue #122: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE:
WP03 — Dataset Identity, Version & Provenance Semantics
GitHub issue #123
```

If blocked, end exactly with:

``` text
RELEASE 1.2 WP02 BLOCKED
```

Do not print the success marker unless every mandatory gate passes.

------------------------------------------------------------------------

## 33. Final Constraint

WP02 is the semantic foundation for the rest of Release 1.2.

Prefer a **small, explicit, deterministic model** over speculative
flexibility.

Preserve Release 1.1 observation truth. Separate definition from
materialization from snapshot. Define reproducibility without
prematurely defining identifiers. Keep storage, contracts, catalog
implementation, and pipelines out of scope.

**Settle meaning now; leave representation to the work package that owns
it.**

# Release 1.7 WP12 --- Architecture & Documentation Alignment --- Codex Authority

## 1. Mission

Execute Release 1.7 WP12 --- **Architecture & Documentation Alignment**
for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#208`

Frozen Release 1.6 baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor state:

-   WP01--WP11 (#197--#207): CLOSED / Done;
-   WP12 #208: OPEN / Backlog;
-   WP13 #209: OPEN / Backlog;
-   milestone #55: OPEN, 2 open / 11 closed;
-   permanent baseline: 266/266;
-   Domain: 11/11;
-   Application: 119/119;
-   Infrastructure: 125/125;
-   Architecture: 13/13;
-   schema: v3.

WP12 aligns current-state architecture and documentation with the
implemented and permanently tested Release 1.7 reality. It must not
redesign the release, broaden scope, or change production behavior.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_IDENTITY_PROVENANCE_FIDELITY.md`
-   `docs/architecture/data/EXPERIMENT_DISCOVERY_PHYSICAL_ACCESS.md`
-   Release 1.6 durable Experiment architecture/current-state
    documentation;
-   `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
-   `src/AIQuantTradingResearch.Application/Experiments/DurableExperimentDiscoveryUseCase.cs`
-   `src/AIQuantTradingResearch.Application/DependencyInjection.cs`
-   `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultStore.cs`
-   `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryConfiguration.cs`
-   `src/AIQuantTradingResearch.Worker/DurableExperimentDiscoveryExecution.cs`
-   `src/AIQuantTradingResearch.Worker/Program.cs`
-   `tests/AIQuantTradingResearch.Application.Tests/ExperimentDiscoveryApplicationTests.cs`
-   `tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentDiscoveryTests.cs`
-   existing 13 Architecture.Tests;
-   `docs/handbook/ENGINEERING_PLAYBOOK.md`;
-   GitHub issue #208.

The implementation and permanent tests from WP04--WP11 are current-state
evidence. Documentation must describe them accurately rather than
inventing intended behavior.

------------------------------------------------------------------------

## 3. Scope

WP12 owns only:

1.  manifest-authorized current-state architecture/documentation
    alignment;
2.  Architecture.Tests review against the stable Release 1.7 boundaries;
3.  Architecture.Tests changes only if an actual enforceable Release 1.7
    boundary is missing and the manifest authorizes the test path.

Zero-delta-first applies to Architecture.Tests.

Production delta:

`0`

Permanent Application/Infrastructure/Domain test delta:

`0`

Schema/package/project/reference delta:

`0`

Do not begin WP13.

------------------------------------------------------------------------

## 4. Frozen Release 1.7 Current-State Semantics

Documentation must align to these accepted facts.

### Discovery Query

-   exact Snapshot Identity;
-   exact Experiment Definition Identity;
-   mandatory caller-supplied positive maximum;
-   no invented numeric ceiling;
-   Experiment Result Identity ascending binary ordering;
-   zero matches return a successful immutable empty collection;
-   zero matches are not `NotFound`.

### Identity

-   existing `aiq-experiment-identity-v1` remains authoritative for
    Experiment Result identity;
-   no discovery identity;
-   no query identity;
-   discovery does not create or alter Experiment Result identity.

### Evidence Fidelity

Returned `DurableExperimentEvidence` preserves the accepted durable
evidence representation, including:

-   Experiment Result Identity;
-   Snapshot Identity and Snapshot Version;
-   Experiment Definition Identity;
-   Feature Set Identity;
-   provenance/lineage;
-   count;
-   canonical decimal behavior;
-   aggregate presence/absence;
-   mean/minimum/maximum when present;
-   empty Experiment Result semantics.

A returned empty Experiment Result is distinct from a successful
discovery collection containing zero Experiment Results.

### Persistence

-   schema remains v3;
-   existing `experiment_results` table is reused;
-   no table/column/index/migration change;
-   exact dual-identity filtering;
-   explicit binary Experiment Result Identity ordering;
-   parameterized maximum;
-   existing persisted-column mapper reused;
-   discovery is read-only;
-   WP06 accepted a bounded table scan for Release 1.7;
-   no temporary ordering B-tree was required because the existing
    binary primary-key ordering supports the requested order;
-   no structural optimization was authorized.

### Application

-   `DurableExperimentDiscoveryRequest`;
-   immutable `DurableExperimentDiscoveryResult`;
-   `IDurableExperimentEvidenceDiscoveryStore`;
-   `IDurableExperimentDiscoveryUseCase`;
-   valid request invokes the discovery store exactly once;
-   invalid null/non-positive requests produce `InvalidRequest` without
    store invocation;
-   successful empty/non-empty results pass through;
-   bounded classified failures pass through;
-   unknown defects propagate;
-   no retry, fallback, write, provider access, or storage mechanics in
    Application.

### Failure Semantics

Preserve the Release 1.6 failure vocabulary.

For discovery:

-   `InvalidRequest`: Application-prevalidated;
-   `NotFound`: not used for a valid zero-match discovery;
-   `DependencyUnavailable`: directly reachable;
-   `InvalidEvidence`: bounded reconstruction/schema classification;
-   `IntegrityConflict`: preserved lower-layer acceptance invariant, not
    artificially triggered by read-only discovery;
-   unknown defects propagate.

No retry, repair, fallback, skipped-row behavior, partial successful
collection, or mutation is introduced.

### Dependency Injection

-   transient discovery use case;
-   discovery store resolves through the SQLite Experiment Result store;
-   registration cardinality is exact;
-   resolution is side-effect-free;
-   no duplicate production registrations;
-   predecessor DI graph remains valid.

### Worker

Explicit one-shot Discovery mode exists.

Routing precedence is:

`Discovery → Durable Experiment → Experiment → Feature → pipeline`

Discovery configuration requires:

-   exact Snapshot Identity;
-   exact Experiment Definition Identity;
-   positive maximum.

Partial/malformed discovery intent fails and cannot fall back.

Valid discovery resolves and invokes the Application discovery use case
exactly once.

Worker has:

-   no direct SQL;
-   no direct store implementation;
-   no provider fallback;
-   no retry/repair behavior.

### Permanent Regression Baseline

After WP11:

-   Domain: 11;
-   Application: 119;
-   Infrastructure: 125;
-   Architecture: 13;
-   total: 266;
-   skipped: 0.

Permanent discovery coverage includes:

-   request validation;
-   exact one-call forwarding;
-   empty/non-empty pass-through;
-   evidence/provenance/decimal fidelity;
-   bounded failures;
-   unknown defects;
-   exact dual-identity filtering;
-   binary ordering;
-   maximum bounds;
-   empty-result fidelity;
-   read-only state;
-   safe unavailable/invalid-evidence boundaries;
-   DI forwarding/cardinality;
-   repository-native `--no-build` Worker integration.

`IntegrityConflict` remains permanently protected by the Release 1.6
acceptance-conflict coverage rather than by corrupting discovery state.

------------------------------------------------------------------------

## 5. Explicit Exclusions

Do not document excluded capabilities as implemented.

Release 1.7 does not add:

-   registry/history/search semantics beyond the exact bounded discovery
    contract;
-   mutation/edit/delete of Experiment Results;
-   scheduling;
-   background discovery;
-   provider acquisition;
-   network execution;
-   backtesting;
-   portfolio/risk simulation;
-   Machine Learning implementation;
-   Explainable AI implementation;
-   Release 1.8 implementation;
-   new persistence schema;
-   new index;
-   cloud deployment;
-   public API/UI;
-   broad pagination;
-   discovery identity;
-   query identity.

Future-looking documents may retain clearly labeled future concepts, but
current-state sections must not imply those capabilities exist.

------------------------------------------------------------------------

## 6. Architecture Review --- Zero-Delta-First

Inspect all 13 Architecture.Tests against Release 1.7.

Determine whether the existing executable rules already enforce the
stable boundaries, especially:

-   Domain independence;
-   Application independence from Infrastructure/Worker/provider/SQLite;
-   Infrastructure dependency direction;
-   Worker composition-root role;
-   no forbidden reverse dependencies;
-   stable project/reference graph.

Do not add an architecture test simply because Release 1.7 introduced a
new interface/class.

Add or modify an Architecture.Test only when all are true:

1.  there is a genuine architectural boundary;
2.  the boundary is not already enforced;
3.  it can be enforced robustly without implementation-name coupling;
4.  the manifest authorizes the path;
5.  the test provides durable architectural value beyond Release 1.7.

Expected outcome:

`Architecture.Tests delta = 0`

If zero delta is correct, state why.

If an actual missing boundary is found, stop before mutation unless the
WP12 manifest explicitly authorizes that exact architecture-test change.

------------------------------------------------------------------------

## 7. Documentation Discovery

Use the Release 1.7 manifest to identify the exact current-state
documents WP12 may modify.

Search the authorized documents for stale statements concerning:

-   exact-identity-only Experiment retrieval;
-   absence of discovery;
-   Worker routing precedence;
-   schema/current persistence behavior;
-   test totals;
-   Application contracts;
-   Infrastructure ownership;
-   DI;
-   failure semantics;
-   observability/current execution modes;
-   module interactions;
-   public contracts;
-   testing strategy.

Do not mechanically edit every mention of Experiment.

Change only statements whose current-state meaning became stale because
of Release 1.7.

------------------------------------------------------------------------

## 8. Likely Alignment Subjects

Within manifest-authorized files, align relevant sections such as:

### README / Front Door

If authorized:

-   accurately summarize durable Experiment Evidence Discovery as a
    current capability;
-   keep presentation concise;
-   update permanent test baseline from 250 to 266 where the README
    exposes the count;
-   do not turn README into a Release 1.7 design document.

### Data Pipeline Architecture

If authorized:

-   show discovery as a read-only downstream access path over durable
    Experiment evidence;
-   preserve ingestion/Feature/Experiment pipeline semantics;
-   do not imply discovery mutates pipeline state.

### Configuration Model

If authorized:

-   document explicit one-shot discovery selector/configuration;
-   mandatory Snapshot identity, Experiment Definition identity,
    positive maximum;
-   malformed/partial intent no-fallback behavior;
-   routing precedence.

### Module Interactions

If authorized:

-   Worker → Application discovery use case → Application store
    abstraction → Infrastructure SQLite implementation;
-   no reverse dependency;
-   no Worker SQL/store mechanics.

### Public Contracts

If authorized:

-   describe the Release 1.7 Application discovery
    request/result/store/use-case contracts;
-   identify them as storage-independent Application contracts;
-   distinguish durable evidence identity from discovery dimensions.

### Dependency Injection

If authorized:

-   describe discovery use-case/store registrations;
-   preserve transient/current lifetime facts;
-   describe shared SQLite store forwarding only at the
    composition/infrastructure level;
-   no side effects during resolution.

### Observability Model

If authorized:

-   describe deterministic Worker discovery presentation only to the
    degree actually implemented;
-   do not invent telemetry, metrics, tracing, or dashboards.

### Testing Strategy

If authorized:

-   update permanent baseline to 266;
-   document Application/Infrastructure discovery regression coverage;
-   preserve process-level fixture strategy;
-   document that invalid direct `IntegrityConflict` construction is not
    required for read-only discovery coverage.

These are alignment subjects, not automatic file permissions. The
manifest controls.

------------------------------------------------------------------------

## 9. Terminology Discipline

Use repository terminology exactly.

Prefer:

-   `Durable Experiment Evidence Discovery`;
-   `Experiment Result Identity`;
-   `Snapshot Identity`;
-   `Snapshot Version`;
-   `Experiment Definition Identity`;
-   `Feature Set Identity`;
-   `DurableExperimentEvidence`;
-   `aiq-experiment-identity-v1`;
-   `experiment_results`;
-   `InvalidRequest`;
-   `NotFound`;
-   `DependencyUnavailable`;
-   `InvalidEvidence`;
-   `IntegrityConflict`.

Do not introduce alternate names such as:

-   discovery fingerprint;
-   query fingerprint;
-   experiment search ID;
-   discovery registry ID;
-   result lookup hash.

------------------------------------------------------------------------

## 10. Current State vs Future State

Maintain explicit temporal honesty.

Where documents discuss future architecture:

-   preserve it if still valid;
-   label it future/planned;
-   do not rewrite future plans as implemented Release 1.7 behavior.

Where documents describe current state:

-   align exactly to merged/working Release 1.7 implementation evidence;
-   do not leave stale statements saying discovery is absent.

Do not make commitments for Release 1.8.

------------------------------------------------------------------------

## 11. No Architecture & Design Review Register Yet

The human decision is to perform the compact **Architecture & Design
Review Register** only after Release 1.7 is finished, as part of
separately governed Release 1.8 work.

WP12 must therefore not:

-   create that register;
-   perform broad source-code redesign review;
-   propose refactoring work packages;
-   reopen accepted Release 1.7 design;
-   reintroduce backtesting;
-   change roadmap sequencing.

If an observation is not necessary to make Release 1.7 documentation
truthful, leave it for Release 1.8.

------------------------------------------------------------------------

## 12. Documentation Quality

Every changed document must:

-   describe current behavior accurately;
-   avoid duplicated explanations when a canonical document can be
    referenced;
-   preserve existing document structure/style;
-   use relative repository links where appropriate;
-   avoid broken anchors;
-   avoid speculative claims;
-   avoid marketing exaggeration;
-   avoid implementation trivia unless the document owns it;
-   end with exactly one terminal newline;
-   contain no trailing whitespace.

Do not create documentation churn.

------------------------------------------------------------------------

## 13. Link Validation

Validate Markdown links for all WP12-changed documents.

Require:

-   broken local links: 0;
-   broken local anchors introduced by WP12: 0;
-   renamed/moved files: 0 unless explicitly authorized;
-   absolute local workstation paths: 0.

Do not add links to ephemeral validation files.

------------------------------------------------------------------------

## 14. Production and Test Preservation

WP12 production delta must be zero.

Do not modify:

-   Domain production;
-   Application production;
-   Infrastructure production;
-   Worker production;
-   schema;
-   SQL;
-   DI code;
-   packages/projects/references;
-   Domain tests;
-   Application tests;
-   Infrastructure tests.

Architecture.Tests are zero-delta-first as specified above.

WP11 permanent baseline must remain 266 unless an explicitly authorized
architecture-test delta is proven necessary.

------------------------------------------------------------------------

## 15. Canonical Verification

After documentation alignment run canonical verification.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 119/119;
-   Infrastructure.Tests: 125/125;
-   Architecture.Tests: 13/13 unless an explicitly authorized justified
    delta occurred;
-   permanent total: 266/266 under expected zero-delta outcome;
-   skipped: 0;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   Markdown links: PASS;
-   conflict markers: 0;
-   schema: v3;
-   table/column/index/migration delta: 0;
-   package/project/reference delta: 0/0/0;
-   dependency graph unchanged and acyclic;
-   provider/network product activity: 0;
-   real credentials: 0;
-   disposable residue: 0.

------------------------------------------------------------------------

## 16. Manifest Reconciliation

At completion enumerate every WP12 changed path and prove:

-   it is manifest-authorized;
-   the change is architecture/documentation alignment only;
-   no unauthorized file was modified.

Also report:

-   expected cumulative Release 1.7 paths;
-   unexplained paths: 0;
-   staged paths: 0.

Do not stage, commit, branch, push, open a PR, tag, or release.

------------------------------------------------------------------------

## 17. GitHub Lifecycle

Only after every WP12 gate passes:

1.  move #208 Backlog → In Progress if necessary;
2.  post concise completion evidence;
3.  close #208;
4.  set #208 Project Status to Done.

Final required state:

-   #197--#208: 12/12 CLOSED / Done;
-   #209: OPEN / Backlog;
-   milestone #55: OPEN, 1 open / 12 closed;
-   Project membership: 13/13;
-   duplicates: 0;
-   Priority/Release/Area/dependencies unchanged.

Do not transition #209 automatically.

------------------------------------------------------------------------

## 18. Mutation Budget

### Architecture/current-state documentation

Manifest-authorized files only.

### Production

`0`

### Domain/Application/Infrastructure tests

`0`

### Architecture.Tests

Expected `0`; zero-delta-first.

### Schema/table/column/index/migration

`0`

### Packages/projects/references

`0`

### Disposable validation

Allowed; residue 0.

### Git transport

`0`

### GitHub

Only #208 lifecycle.

------------------------------------------------------------------------

## 19. Stop Conditions

Stop with:

`RELEASE 1.7 WP12 BLOCKED`

if:

-   starting state differs materially;
-   manifest ownership is ambiguous for a required document;
-   documentation cannot be made truthful without changing production
    behavior;
-   an actual architecture defect requires production redesign;
-   an Architecture.Test requires unauthorized mutation;
-   schema/index/migration change appears necessary;
-   package/project/reference change appears necessary;
-   Release 1.7 semantics conflict materially across authoritative
    sources;
-   canonical verification fails;
-   Markdown/link integrity cannot be preserved;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

Do not fix a design issue under documentation authority.

------------------------------------------------------------------------

## 20. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  manifest-authorized documents reviewed;
4.  documents changed;
5.  stale statements corrected;
6.  Release 1.7 discovery semantics aligned;
7.  identity/provenance/fidelity alignment;
8.  schema-v3/persistence alignment;
9.  Application/Infrastructure ownership alignment;
10. DI alignment;
11. Worker routing/configuration alignment;
12. failure-semantics alignment;
13. testing-strategy/baseline alignment;
14. Architecture.Tests review and delta with rationale;
15. explicit Release 1.7 exclusions preserved;
16. Release 1.8 review-register deferral preserved;
17. production/test/schema/package/project/reference deltas;
18. canonical verification;
19. Markdown/link/whitespace validation;
20. provider/network/credential isolation;
21. residue;
22. changed-path manifest reconciliation;
23. GitHub lifecycle;
24. final milestone counts;
25. next authorized action.

------------------------------------------------------------------------

## 21. Completion Markers

On success end exactly:

`RELEASE 1.7 WP12 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP13 — Full Validation, Integration & Acceptance — GitHub issue #209`

Do not execute WP13 automatically.

If blocked end exactly:

`RELEASE 1.7 WP12 BLOCKED`

and identify the smallest corrective authority required.

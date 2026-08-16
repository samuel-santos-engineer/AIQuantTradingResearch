# Release 1.1 WP02 --- Persistence Technology Discovery --- Authoritative Codex Execution Prompt

## 1. Authority

You are executing **Release 1.1 --- WP02: Persistence Technology Discovery** for:

```text
Repository: samuel-santos-engineer/AIQuantTradingResearch
Release:    Phase 3 - Release 1.1: Market Data Persistence Foundation
Work item:  GitHub issue #104
WP:         WP02
```

This file is the authoritative WP02 execution contract.

Read this file completely before taking any action.

The standard five-line companion:

```text
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt-chat.md
```

is only a bootstrap. It does not duplicate or supersede this contract.

---

## 2. Governing Authority Precedence

Apply authority in this order:

1. Explicit human instructions in the current execution conversation.
2. This authoritative WP02 prompt.
3. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`.
4. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`.
5. The accepted Release 1.1 governance-baseline post-merge closure.
6. The accepted WP01 execution result ending `RELEASE 1.1 WP01 COMPLETE`.
7. GitHub issue #104 and the accepted Release 1.1 planning state.
8. Current repository and GitHub truth.
9. Existing repository engineering and documentation conventions where they do not conflict with higher authority.
10. External technology documentation used only as evidence for the WP02 comparison, never as authority to expand repository scope.

Do not infer authority to expand scope.

If authorities cannot be reconciled without mutation outside this prompt, stop and report `WP02 BLOCKED`.

---

## 3. Purpose

WP02 selects the **minimum credible persistence technology** for Release 1.1 through explicit, reproducible evidence.

This is a **technology-discovery and architecture-decision work package**.

WP02 must:

- inspect the accepted repository baseline;
- preserve Release 1.0 behavior and Release 1.1 architecture boundaries;
- identify credible persistence candidates;
- compare them using the exact Release 1.1 evaluation criteria;
- distinguish facts, repository evidence, assumptions, and judgment;
- select exactly one persistence technology;
- document the decision and its bounded Release 1.1 usage;
- close WP02 only when the decision is sufficiently specific for WP03 and later work packages.

WP02 must **not implement persistence**.

The output of WP02 is architectural evidence and a decision, not packages, schema, migrations, source code, DI, runtime configuration, database files, containers, or Worker behavior.

---

## 4. Accepted Starting Lifecycle State

WP01 established the accepted starting state:

```text
Release 1.0:                     CLOSED
Release 1.1 governance baseline: MERGED / CLOSED
WP01 / issue #103:               CLOSED / Done
WP02 / issue #104:               OPEN / Backlog
WP03–WP16:                       OPEN / Backlog
Milestone #52:                   OPEN
Legacy milestone #42:            CLOSED / empty
Legacy milestone #43:            CLOSED / empty
Active Release 1.2 planning:     0
```

Accepted repository state at WP01 completion:

```text
Branch:       main
HEAD:         9ce7af388b9818bf4374897fc4615e17ccc1615a
origin/main:  9ce7af388b9818bf4374897fc4615e17ccc1615a
Ahead/behind: 0/0
Staged:       0
Unstaged:     0
```

Expected untracked governance at WP01 completion:

```text
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

Treat these values as expected evidence, not permission to fabricate current state.

Fetch/re-query safely and reconcile current Git/GitHub truth before mutation.

An intervening human-approved commit or governance integration may legitimately change `main`. If current `main` is ahead of the WP01 SHA, inspect the intervening history. Proceed only if the accepted WP01 state and Release 1.1 governance remain represented and no unauthorized Release 1.1 implementation has begun.

Never reset, rewrite, discard, or overwrite user work merely to reproduce an old SHA.

---

## 5. WP02 Governance Pair and Recursion-Safe Handling

The file manifest explicitly authorizes exactly these WP02 governance files:

```text
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt.md
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt-chat.md
```

These two files are **EXPECTED GOVERNANCE** for WP02.

Their presence as untracked files when execution starts is expected and must not be classified as implementation drift, an unexpected mutation, or a dirty-tree blocker.

Do not modify, normalize, stage, commit, delete, relocate, or integrate the WP01 or WP02 governance pairs unless separate explicit authority says otherwise.

This rule intentionally prevents governance-artifact recursion.

---

## 6. Authorized WP02 Repository Mutation Surface

WP02 may create or modify only the two required architecture decision artifacts:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

The expected normal case is that both files are new.

If either path already exists, inspect it before mutation and reconcile whether it is:

- an authorized current WP02 artifact;
- stale planning material;
- accepted prior content;
- unrelated user work; or
- a collision.

Do not overwrite ambiguous or unrelated content.

No other tracked repository path is authorized.

---

## 7. Explicitly Prohibited Repository Mutations

WP02 is decision-only.

Do not modify:

```text
src/**
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

- production code;
- test code;
- persistence interfaces;
- Domain persistence semantics;
- Application persistence contracts;
- database adapters;
- repositories;
- storage clients;
- migrations;
- schema files;
- SQL scripts intended as implementation;
- connection factories;
- connection strings;
- DI registrations;
- Worker persistence behavior;
- runtime database files;
- container definitions;
- CI workflows;
- package references;
- package-version changes;
- secrets or credential files.

Do not start WP03 or any later work package.

---

## 8. WP01 Baseline That Must Inform WP02

Reconcile the accepted WP01 findings rather than rediscovering them from assumption.

WP01 established:

- Domain owns `PriceObservation`, `ObservationSeries`, and `MeanPrice`.
- Application owns provider-independent research requests/results, `IObservationSource`, `IResearchUseCase`, source-failure vocabulary, and orchestration.
- Infrastructure owns Twelve Data transport/authentication mechanics, provider response models, normalization, provider validation/failure mapping, and the concrete `IObservationSource`.
- Worker owns configuration handoff, DI composition, and one-shot research execution.
- no persistence Application contracts exist;
- no durable storage abstraction exists;
- no concrete database/storage engine exists;
- no physical schema or migrations exist;
- no database connection configuration exists;
- no durable serialization/file storage exists;
- no storage-specific runtime files exist;
- no persistence implementation conflict exists.

WP01 also observed centrally governed PostgreSQL-related package versions and `Testcontainers.PostgreSql`, but no production or test project references them.

Those version entries are **inventory evidence only**.

They are not a technology decision, architectural commitment, implementation authorization, or preference.

WP02 must not reverse-engineer a decision from unused package-version entries.

---

## 9. Release 1.1 Architectural Constraints

The technology decision must preserve these release principles:

1. **Semantics before technology.**
2. **Evidence before selection.**
3. **Durability must be real.**
4. **Provider independence remains intact.**
5. **No silent historical mutation.**
6. **Retrieval ordering is semantic.**
7. **Failure mapping is bounded.**
8. **Tests are offline and deterministic.**
9. **Implementation remains minimal.**
10. **Lifecycle gates remain explicit.**

The selected technology must not redefine Domain/Application meaning.

Twelve Data must not become a persistence contract.

Storage-engine mechanics must remain an Infrastructure concern unless a later work package explicitly authorizes a provider-independent Application contract.

---

## 10. Mandatory Evaluation Criteria

Every serious candidate must be evaluated against the criteria defined by the Release 1.1 execution plan:

1. zero monetary cost;
2. local/offline operation;
3. .NET support;
4. durability;
5. transaction capability;
6. deterministic testing;
7. clean-checkout bootstrap;
8. Windows/Linux compatibility;
9. GitHub CI feasibility;
10. schema evolution feasibility;
11. dependency/package impact;
12. operational complexity;
13. portfolio/recruiting relevance.

Do not silently drop, merge, rename, or replace these criteria.

You may add clearly labeled secondary considerations only if they materially distinguish candidates and do not change the authority of the thirteen required criteria.

---

## 11. Candidate Discovery Rules

Compare a sufficiently diverse set of credible technologies.

At minimum, the assessment must include:

- **SQLite** as an embedded relational baseline;
- **PostgreSQL** as a client/server relational baseline;
- at least one additional credible local/offline persistence candidate whose characteristics materially differ from the first two.

The third candidate must be justified by the Release 1.1 use case rather than included merely to satisfy a count.

Examples may include an embedded analytical database or another durable local storage approach, but do not force a candidate that cannot credibly satisfy the release.

You may evaluate more than three candidates if evidence shows they are genuinely plausible.

Do not include obviously unsuitable technologies simply to make the selected option look better.

Do not select a technology before completing the comparison.

---

## 12. Evidence Standard

Technology claims must be evidence-backed.

Prefer evidence in this order:

1. official product/database documentation;
2. official .NET provider/client documentation;
3. official package/project documentation;
4. official GitHub repositories or release documentation;
5. authoritative vendor/community-maintainer documentation;
6. repository-local evidence;
7. clearly labeled engineering inference.

For claims that can change with product versions, package support, licensing, CI compatibility, or operating-system support, verify current authoritative documentation when network access is available.

If network access is unavailable:

- do not fabricate current version/support claims;
- use repository-local evidence where possible;
- label uncertain claims;
- distinguish stable architectural properties from unverified current details;
- block the final selection only if missing evidence is material to the decision.

Do not use popularity, memory, or an unused repository package version as sufficient evidence.

---

## 13. Evidence Recording Requirements

For each serious candidate, record:

- candidate name;
- persistence model;
- deployment model;
- relevant .NET integration path;
- local/offline feasibility;
- durability characteristics;
- transaction characteristics;
- deterministic-test approach;
- clean-checkout bootstrap model;
- Windows compatibility;
- Linux compatibility;
- GitHub CI model;
- schema-evolution approach;
- expected package footprint;
- expected operational footprint;
- Release 1.1 advantages;
- Release 1.1 disadvantages;
- material risks;
- evidence sources;
- unresolved questions, if any.

Use concise citations/links in the architecture assessment where repository documentation conventions permit.

Do not copy large external passages.

---

## 14. Comparison Method

Use a transparent decision method.

The assessment must contain a matrix with all thirteen mandatory criteria and every serious candidate.

For each criterion/candidate intersection, use a bounded qualitative rating such as:

```text
Strong
Acceptable
Weak
Disqualifying
```

or another equally clear ordinal scale.

If using numerical scoring:

- define the scale;
- define any weights before scoring;
- justify weights from Release 1.1 objectives;
- show the unrounded inputs;
- do not create false precision.

A weighted score may support the decision but must not replace engineering judgment.

Any hard disqualifier must be explicit.

---

## 15. Decision Priorities

The selection should optimize for the **smallest credible durable vertical slice**, not the most feature-rich database.

When trade-offs are close, favor the technology that best supports:

- real durability;
- deterministic offline tests;
- minimal operational setup;
- clean local bootstrap;
- clean CI bootstrap;
- clear .NET integration;
- architecture boundary preservation;
- future schema evolution;
- useful portfolio evidence without unnecessary platform complexity.

Portfolio/recruiting relevance matters, but it must not override release correctness, zero-cost operation, determinism, or minimality.

---

## 16. No Premature Architecture Design

WP02 selects technology; it does not perform WP03–WP12 design.

Do not define as final implementation authority:

- exact Domain invariants;
- duplicate-observation semantics;
- exact persistence Application interfaces;
- repository method signatures;
- exact SQL table/column definitions;
- exact indexes;
- migration numbering;
- connection factory classes;
- retry policies;
- storage exception taxonomy;
- DI extension APIs;
- Worker persistence workflow;
- test fixture classes.

If a candidate comparison requires an illustrative example, label it **non-authoritative illustration** and keep it technology-evaluation-only.

Later work packages own those decisions.

---

## 17. Required Assessment Artifact

Create:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
```

The assessment must contain, at minimum:

1. Title and Release/WP context.
2. Decision question.
3. Scope.
4. Repository baseline from WP01.
5. Architectural constraints.
6. Mandatory evaluation criteria.
7. Candidate set and why each is credible.
8. Evidence method and evidence date.
9. Per-candidate evidence.
10. Comparative decision matrix.
11. Trade-off analysis.
12. Security/credential implications.
13. Local developer experience.
14. CI/testing implications.
15. Package/dependency implications.
16. Schema-evolution implications.
17. Operational-complexity implications.
18. Portfolio/recruiting implications.
19. Risks and unresolved questions.
20. Recommendation.
21. Explicit statement that no implementation is authorized by the assessment.

The assessment must make it possible for a reviewer to understand why the selected technology won without reading the decision artifact.

---

## 18. Required Decision Artifact

Create:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

The decision artifact must be shorter and normative.

It must state explicitly:

```text
Selected persistence technology
Selection rationale
Rejected alternatives
Known constraints
Release 1.1 usage boundary
```

It must also include:

- status: `Accepted for Release 1.1` only if all WP02 gates pass;
- decision date;
- decision owner/context;
- evidence reference to `MARKET_DATA_PERSISTENCE_ASSESSMENT.md`;
- architectural consequences;
- what the decision does **not** authorize;
- downstream implications for WP03–WP16;
- reconsideration triggers.

The decision must select **exactly one** technology.

Do not select multiple co-equal primary persistence technologies.

Optional tooling used only for testing or migration support is not a second primary persistence technology, but WP02 must not add such tooling to the repository.

---

## 19. Release 1.1 Usage Boundary

The decision must define a bounded usage statement that is specific enough to guide later work but does not steal later design authority.

At minimum it must state that:

- the selected technology is the Release 1.1 durable persistence mechanism for market-data historical observations;
- storage-specific mechanics belong in Infrastructure;
- Domain and Application remain storage-technology independent;
- provider acquisition and persistence remain separate concerns;
- persistence must operate without live market-data provider access during tests;
- secrets/credentials, if the technology can require them, must never be committed;
- schema, contracts, physical model, DI, Worker integration, and test implementation remain owned by later WPs.

Do not claim the selected technology is the permanent platform-wide choice beyond Release 1.1.

---

## 20. Security and Credential Rules

WP02 must not require or use real secrets.

Do not:

- connect to a live database service;
- provision cloud infrastructure;
- create paid resources;
- print credentials;
- create credential-bearing connection strings;
- add `.env` secrets;
- add database passwords;
- use Twelve Data credentials;
- make live market-data provider calls.

When evaluating a client/server candidate, assess credential/configuration burden conceptually and from documentation only.

Local ephemeral experiments are not authorized if they create repository/runtime state beyond ordinary ignored tooling outputs.

---

## 21. Package and Dependency Protection

Do not add, remove, restore specifically for, or reference new persistence packages as part of WP02 implementation.

Normal existing solution restore/verification is allowed.

The existing central PostgreSQL-related package versions must remain unchanged.

Do not alter:

```text
Directory.Packages.props
```

or any project file.

The assessment may identify the likely future package(s) for each candidate, but must clearly label them as **expected future dependency impact**, not installed dependencies.

---

## 22. Git Starting-State Gate

Before repository mutation:

1. authenticate safely;
2. fetch remote refs without destructive operations;
3. record current branch and HEAD;
4. record `origin/main`;
5. record ahead/behind;
6. record staged paths;
7. record unstaged tracked paths;
8. record untracked paths;
9. classify every non-clean path.

Expected non-clean paths may include only recognized governance artifacts for Release 1.1 work already authorized by the manifest/current authority.

Do not use:

- `git reset --hard`;
- destructive `git clean`;
- automatic stash;
- rebase;
- force checkout;
- history rewrite.

If unrelated user work exists and cannot be safely isolated, stop with `WP02 BLOCKED`.

---

## 23. GitHub Starting-State Gate

Before WP02 execution-state mutation, verify:

```text
Milestone #52: OPEN
Issue #103: CLOSED / Done
Issue #104: OPEN / Backlog
Issues #105–#118: OPEN / Backlog
WP17+: 0
Lifecycle-gate issues: 0
Active Release 1.2 planning: 0
```

Also verify:

- issue #104 belongs to milestone #52;
- issue #104 is assigned to `samuel-santos-engineer`;
- issue #104 has the planned label;
- Project Status is Backlog;
- Priority is P1;
- Release is 1.1;
- Area is Data;
- its dependency on WP01/#103 is represented;
- no artificial dependency has appeared.

If WP01 is not terminal, stop.

If WP03 or later implementation has already started without authority, stop and report the drift.

---

## 24. WP02 Issue-State Handling

After all starting-state gates pass and immediately before substantive WP02 work, move only issue #104:

```text
Backlog -> In Progress
```

Do not move any other Release 1.1 issue.

If Project automation performs an expected transition, record it rather than fighting it.

Do not close #104 until every WP02 acceptance gate passes.

If WP02 becomes blocked after #104 is In Progress, leave it open and report the blocker truthfully. Do not mark it Done.

---

## 25. Repository Baseline Verification

Before authoring the decision, rerun enough baseline validation to prove WP02 is not building on a broken repository.

At minimum:

```text
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Equivalent PowerShell invocation is acceptable when required by the environment.

Record:

- restore result;
- build warnings/errors;
- permanent test totals;
- Architecture.Tests result;
- canonical verification result.

Expected baseline from WP01:

```text
Domain.Tests:         11/11
Application.Tests:    16/16
Infrastructure.Tests: 65/65
Architecture.Tests:   13/13
Total:                105/105
```

A changed test count is not automatically failure if current `main` legitimately changed, but it must be reconciled.

Do not repair unrelated failures under WP02 authority.

---

## 26. Architecture Baseline Verification

Confirm the production dependency graph remains:

```text
Domain         -> none
Application    -> Domain
Infrastructure -> Application
Worker         -> Application, Infrastructure
```

Confirm:

- cycles = 0;
- Domain has no storage-engine dependency;
- Application has no storage-engine dependency;
- no persistence implementation was introduced before WP02;
- Twelve Data mechanics remain Infrastructure-owned.

If this baseline has drifted, stop unless the drift is explicitly accepted by higher authority.

---

## 27. Research Execution

Perform the candidate research only after the starting-state and baseline gates pass.

Research must be bounded to the WP02 decision question.

Do not turn WP02 into a general database survey.

For every candidate:

1. identify the official implementation/integration path relevant to .NET;
2. establish whether zero-cost local use is credible;
3. establish whether offline deterministic testing is credible;
4. establish durability and transaction support;
5. establish Windows/Linux viability;
6. establish clean-checkout/CI bootstrap expectations;
7. establish schema-evolution feasibility;
8. estimate dependency and operational impact;
9. identify meaningful Release 1.1 risks;
10. record evidence.

If a material claim cannot be verified, say so.

---

## 28. Repository-Aware Comparison

The assessment must explicitly account for the repository's actual shape.

Consider:

- C#/.NET primary implementation;
- current layered dependency graph;
- Infrastructure ownership of external mechanics;
- central package management;
- deterministic/offline test strategy;
- VS Code/local development;
- Windows and Linux compatibility;
- GitHub CI feasibility;
- zero-cost constraint;
- portfolio objective;
- Release 1.1 goal of a minimal durable market-data history slice.

Do not assume Kubernetes, Azure, Docker, a permanently running server, or paid managed services.

A candidate may support those environments, but Release 1.1 must not require them unless the evidence shows no simpler credible option and this authority is explicitly amended.

---

## 29. Treatment of PostgreSQL Repository Evidence

The repository contains pre-existing central package-version entries related to PostgreSQL.

WP02 must document this fact in the assessment.

It must also explicitly state:

- the entries are currently unused;
- no production/test project references them at the accepted WP01 baseline;
- they did not constitute a prior Release 1.1 selection;
- they may reduce future package-governance friction if PostgreSQL is selected;
- they must not be scored as proof that PostgreSQL is architecturally required.

This protects the decision from confirmation bias.

---

## 30. Whitespace Handling Without Recursive Authority

WP02 is authorized to handle whitespace findings **only within the two WP02 architecture artifacts it creates or modifies**.

Before completion, run:

```text
git diff --check
```

If Git reports whitespace findings in an authorized WP02 architecture artifact:

- correct only the exact reported whitespace;
- preserve semantic content;
- rerun the check.

This authority covers zero or more whitespace findings in those two WP02 artifacts and does not require a separate whitespace-unblock prompt.

It does **not** authorize whitespace normalization in:

- WP01/WP02 governance prompts;
- existing repository files;
- unrelated documentation;
- source/test/build files.

If `git diff --check` reports a violation outside the authorized WP02 architecture artifacts, stop and report it.

Do not perform broad formatting normalization.

---

## 31. Semantic Review of WP02 Artifacts

Before acceptance, review both artifacts for:

- consistency with each other;
- exact technology name;
- exactly one selected primary technology;
- all thirteen criteria represented;
- rejected alternatives represented fairly;
- no implementation disguised as a decision;
- no contradiction with Release 1.1 architecture principles;
- no accidental provider coupling;
- no premature WP03+ design;
- no secret/credential content;
- no unsupported certainty;
- no stale references to Release 1.0 as current work.

The decision artifact must be derivable from the assessment.

---

## 32. Final Technical Validation

After the two documentation artifacts are complete:

1. rerun `git diff --check`;
2. rerun canonical verification;
3. confirm build warnings/errors;
4. confirm permanent test totals;
5. confirm Architecture.Tests;
6. confirm no generated persistence/runtime files remain;
7. confirm no package/project/source/test changes exist.

Documentation-only WP02 changes must not break the repository.

---

## 33. Candidate Reconciliation

At successful WP02 completion, repository mutations attributable to WP02 must be exactly:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

plus recognized untracked governance pairs that existed as execution inputs and are explicitly excluded from the WP02 implementation candidate.

Required:

```text
Authorized architecture files changed: 2
Unauthorized tracked files changed:    0
Production files changed:              0
Test files changed:                    0
Package/project files changed:         0
Build/CI files changed:                0
WP03+ artifacts created:               0
```

If repository truth requires fewer than two changed architecture files because one or both already contain the exact accepted WP02 result, explain and reconcile rather than manufacturing a diff.

---

## 34. Git Transport Protection

WP02 does not authorize:

- `git add`;
- commit;
- amend;
- branch creation;
- push;
- force push;
- pull request;
- merge;
- tag;
- GitHub Release;
- integration of governance prompts.

The two architecture artifacts are to remain local working-tree candidate changes for later governed integration unless a separate authority says otherwise.

Do not create a WP02 integration branch.

---

## 35. GitHub Scope Protection

WP02 may mutate only issue #104 and its Project Status as required for execution lifecycle.

Allowed:

1. Backlog -> In Progress at start of substantive work.
2. One concise completion-evidence comment after all acceptance gates pass.
3. Close issue #104.
4. Allow/verify the expected Project transition to Done.

Do not modify:

- milestone #52 except its automatic issue counts;
- issues #103 or #105–#118;
- dependencies;
- labels;
- assignees;
- Priority;
- Release;
- Area;
- Project schema;
- legacy milestones;
- Release 1.2 planning.

---

## 36. Completion Evidence Comment

Only after all WP02 acceptance gates pass, add one concise evidence comment to issue #104.

It should summarize:

- selected persistence technology;
- assessment artifact path;
- decision artifact path;
- number of serious candidates evaluated;
- all thirteen mandatory criteria covered;
- repository technical verification result;
- confirmation that no implementation/package/schema/DI/Worker change occurred;
- confirmation that WP03 was not started.

Do not paste the full assessment into GitHub.

---

## 37. Issue Closure

After the completion-evidence comment:

1. close issue #104;
2. verify it is CLOSED;
3. verify Project Status is Done, whether by explicit authorized transition or existing automation;
4. verify #105–#118 remain Backlog;
5. verify milestone #52 remains OPEN;
6. verify no Release 1.2 planning became active.

If issue closure or Project state cannot reconcile, report the actual state and do not emit the success terminal.

---

## 38. WP03 Protection

WP03 is not authorized in this run.

Do not:

- define final historical-observation persistence semantics;
- edit Domain;
- create Domain persistence invariants;
- move issue #105 from Backlog;
- create the WP03 governance pair;
- begin Application contract design;
- create schema or implementation artifacts.

The WP02 decision may identify questions that WP03 must answer, but must not answer them as binding implementation design.

---

## 39. Acceptance Gates

WP02 passes only if all of the following are true.

### Lifecycle

- WP01 is CLOSED / Done.
- WP02 is the only work package progressed.
- WP03–WP16 remain not started.
- Milestone #52 remains OPEN.
- Active Release 1.2 planning remains zero.

### Repository baseline

- current `main` is safely reconciled with `origin/main`;
- no unrelated user work was discarded;
- baseline restore passes;
- baseline build passes with zero errors;
- canonical verification passes;
- permanent tests pass;
- Architecture.Tests pass;
- production dependency graph remains valid.

### Discovery

- at least three credible candidates are evaluated unless a documented evidence-based reason proves fewer exist;
- SQLite is evaluated;
- PostgreSQL is evaluated;
- at least one materially different credible candidate is evaluated;
- all thirteen mandatory criteria are applied;
- evidence is current enough for material claims;
- assumptions and unresolved questions are explicit.

### Decision

- exactly one persistence technology is selected;
- rationale is evidence-backed;
- rejected alternatives are fairly documented;
- known constraints are explicit;
- Release 1.1 usage boundary is explicit;
- no permanent platform-wide commitment is implied;
- no WP03+ implementation design is smuggled into WP02.

### Artifacts

- assessment exists at the exact manifest path;
- decision exists at the exact manifest path;
- artifacts are mutually consistent;
- whitespace check passes;
- no unauthorized tracked file changed.

### Scope

- source changes = 0;
- test changes = 0;
- package changes = 0;
- project changes = 0;
- schema/migration changes = 0;
- DI changes = 0;
- Worker changes = 0;
- database/runtime files = 0;
- Git transport operations = 0.

### GitHub

- issue #104 receives concise completion evidence;
- issue #104 is CLOSED;
- Project Status #104 is Done;
- issues #105–#118 remain Backlog;
- no unrelated planning mutation occurred.

---

## 40. Blocker Policy

Stop and report `WP02 BLOCKED` if any mandatory gate cannot be satisfied without exceeding authority.

Examples:

- WP01 is not actually terminal;
- issue #104 planning state is inconsistent and cannot be safely reconciled;
- unrelated working-tree changes cannot be isolated;
- persistence implementation already exists and conflicts with the accepted baseline;
- a material technology claim cannot be verified and the uncertainty prevents a defensible selection;
- required architecture paths collide with unrelated accepted content;
- technical baseline is red for reasons WP02 cannot repair;
- whitespace failure exists outside the two authorized architecture artifacts;
- more than the authorized two architecture artifacts must change;
- the decision would require package installation or implementation to become credible;
- WP03 or later work has already started without authority.

Do not create a new unblock/governance prompt yourself.

Report the minimum additional authority required.

---

## 41. Required Execution Report

Return a complete execution report with at least these sections:

1. Executive Summary
2. Authorities Reviewed
3. Authentication / Repository Context
4. Initial Git State
5. Working-Tree Classification
6. WP01 Completion Reconciliation
7. Release 1.1 Planning Reconciliation
8. WP02 Issue-State Handling
9. Repository Baseline Verification
10. Production Dependency Graph
11. Persistence Baseline Reconciliation
12. Candidate Discovery Method
13. Evidence Sources
14. Candidate Set
15. Mandatory Criteria Matrix
16. Candidate Trade-Off Analysis
17. Selected Technology
18. Selection Rationale
19. Rejected Alternatives
20. Known Constraints
21. Release 1.1 Usage Boundary
22. Assessment Artifact Evidence
23. Decision Artifact Evidence
24. Whitespace Evidence
25. Security / Credential Safety
26. Repository Mutation Accounting
27. Final Technical Validation
28. Git / GitHub Protection
29. WP03 Protection
30. Findings / Observations
31. WP02 Acceptance Matrix
32. Final GitHub State
33. Final Repository State
34. Final Decision
35. Next Authorized Work Package

Include exact paths, test counts, and Git/GitHub states actually observed.

Do not fabricate hosted CI, reviews, commits, pushes, or external evidence.

---

## 42. Required Success Terminal

Emit this terminal only if every acceptance gate passes:

```text
RELEASE 1.1 WP02 COMPLETE

SELECTED PERSISTENCE TECHNOLOGY:
<exact selected technology>

NEXT AUTHORIZED WORK PACKAGE:
WP03 — Historical Observation Persistence Semantics
GitHub issue #105
```

If any mandatory gate fails, emit:

```text
RELEASE 1.1 WP02 BLOCKED
```

and identify the exact blocker and minimum corrective authority.

---

## 43. Final Execution Instruction

Execute WP02 as a bounded, evidence-first architecture decision.

Do not treat existing PostgreSQL-related package-version entries as a prior decision.

Compare credible alternatives fairly, apply all thirteen Release 1.1 criteria, select exactly one minimum credible durable persistence technology, create only the two manifest-authorized architecture artifacts, preserve all implementation boundaries, and progress only issue #104.

WP02 succeeds only when the selected technology is evidence-backed, the repository remains technically green, no implementation has begun, issue #104 is CLOSED / Done, and WP03 remains untouched.

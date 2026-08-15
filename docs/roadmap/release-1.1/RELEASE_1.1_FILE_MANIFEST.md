# Release 1.1 File Manifest

## 1. Purpose

This manifest defines the authoritative file-ownership and mutation boundaries for:

```text
Phase 3 - Release 1.1: Market Data Persistence Foundation
```

It prevents work packages from inventing uncontrolled file scope while allowing implementation filenames to emerge from evidence-backed design decisions.

This manifest is intentionally **ownership-based** where concrete filenames cannot yet be known safely.

The execution plan is authoritative for behavior and sequencing. This manifest is authoritative for expected artifact classes, allowed mutation surfaces, and prohibited file categories.

---

## 2. Manifest Principles

1. Do not invent implementation filenames before the responsible WP establishes the design.
2. A work package may modify only files explicitly owned by this manifest or by a later narrow unblock authority.
3. Existing files may be modified when their responsibility legitimately evolves under the owning WP.
4. New files must remain inside the owning layer/module/documentation surface.
5. Provider-specific persistence implementation belongs to Infrastructure.
6. Provider-independent persistence contracts belong to Application.
7. Domain changes are permitted only if WP03 proves they are semantically necessary.
8. Worker changes are permitted only for composition/execution assigned to WP12.
9. Test changes belong only to their designated test WPs unless a narrow compile/testability unblock explicitly authorizes otherwise.
10. Governance prompt artifacts belong under `docs/roadmap/release-1.1/prompts/`.
11. No Release 1.2 implementation artifact is authorized.
12. Candidate counts must be derived after reconciliation; do not prematurely hardcode integration totals.

---

# 3. Authoritative Governance Paths

Release 1.1 governance root:

```text
docs/roadmap/release-1.1/
```

Required governance documents:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md
```

Prompt root:

```text
docs/roadmap/release-1.1/prompts/
```

Expected prompt naming convention:

```text
NN-<work-package-slug>-codex-prompt.md
NN-<work-package-slug>-codex-prompt-chat.md
```

Lifecycle governance naming convention:

```text
release-1.1-<lifecycle-step>-codex-prompt.md
release-1.1-<lifecycle-step>-codex-prompt-chat.md
```

Every standard prompt-chat companion must be exactly five lines unless a later explicit governance authority changes the convention.

---

# 4. Required Pre-Implementation Governance Artifacts

Before WP01 implementation:

```text
docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md
docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md

docs/roadmap/release-1.1/prompts/release-1.1-github-planning-codex-prompt.md
docs/roadmap/release-1.1/prompts/release-1.1-github-planning-codex-prompt-chat.md
```

WP01 implementation prompt artifacts are created only after GitHub planning is accepted.

---

# 5. WP01 — Release & Repository Preflight

## Authorized New Governance Files

```text
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.1/prompts/01-release-repository-preflight-codex-prompt-chat.md
```

## Authorized Repository Mutation Surface

WP01 should normally introduce no production/test implementation files.

If a preflight report artifact is later explicitly required, it must be placed under a Release 1.1 governance/report path authorized by the WP01 prompt.

## Prohibited

```text
src/**
tests/**
Directory.Packages.props
Directory.Build.props
*.slnx
eng/**
.github/**
```

unless a later explicit unblock authority is issued.

---

# 6. WP02 — Persistence Technology Discovery

## Required Architecture Artifacts

Authorized architecture area:

```text
docs/architecture/market-data/
```

Required expected artifacts:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt.md
docs/roadmap/release-1.1/prompts/02-persistence-technology-discovery-codex-prompt-chat.md
```

## Prohibited

No:

```text
src/**
tests/**
Directory.Packages.props
*.csproj
*.slnx
eng/**
.github/**
```

WP02 is decision-only.

---

# 7. WP03 — Historical Observation Persistence Semantics

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/03-historical-observation-persistence-semantics-codex-prompt.md
docs/roadmap/release-1.1/prompts/03-historical-observation-persistence-semantics-codex-prompt-chat.md
```

## Authorized Production Surface

Primary expected outcome:

```text
Domain delta = 0
```

If repository truth proves a Domain semantic change is required, only:

```text
src/AIQuantTradingResearch.Domain/**
```

may be modified, and only for provider-independent persistence semantics/invariants.

## Prohibited

No:

```text
Application persistence contracts
Infrastructure storage types
Worker
packages/projects
tests
```

unless separately authorized.

---

# 8. WP04 — Application Persistence Contracts

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/04-application-persistence-contracts-codex-prompt.md
docs/roadmap/release-1.1/prompts/04-application-persistence-contracts-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Application/**
```

Only files needed for provider-independent persistence/retrieval:

- interfaces/contracts;
- requests/results where required;
- persistence/retrieval failure vocabulary;
- minimal supporting Application values.

Concrete filenames must be determined by repository conventions.

## Prohibited

No:

```text
Infrastructure implementation
Worker
storage packages
SQL/ORM types
tests
```

---

# 9. WP05 — Persistence Use-Case Integration

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/05-persistence-use-case-integration-codex-prompt.md
docs/roadmap/release-1.1/prompts/05-persistence-use-case-integration-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Application/**
```

Expected ownership:

- existing research use-case orchestration;
- new Application orchestration only where required by WP04 contracts.

## Prohibited

No:

```text
Infrastructure
Worker
Domain unless WP03 authority requires it
tests
packages/projects
```

---

# 10. WP06 — Storage Physical Model

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/06-storage-physical-model-codex-prompt.md
docs/roadmap/release-1.1/prompts/06-storage-physical-model-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

New files may include Infrastructure-owned:

- persistence record/entity types;
- record-to-Domain mapping types;
- schema representation metadata;
- storage-specific constraints.

Do not invent names in advance.

## Prohibited

No:

```text
Domain physical records
Application storage-engine types
Worker
tests
package changes unless WP07 owns them
```

---

# 11. WP07 — Storage Engine & Connection Boundary

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/07-storage-engine-connection-boundary-codex-prompt.md
docs/roadmap/release-1.1/prompts/07-storage-engine-connection-boundary-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Authorized package/project surfaces if required by WP02 decision:

```text
Directory.Packages.props
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
```

Only the selected persistence technology's minimum packages are allowed.

Solution membership changes are not expected.

## Potential Generated/Runtime Artifacts

Database/storage files created during execution must be temporary/ignored and must not become candidate files unless a later explicit authority says otherwise.

## Prohibited

No:

```text
Application storage-technology types
Domain storage dependencies
Worker implementation
test implementation beyond temporary probes
```

---

# 12. WP08 — Observation Persistence

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/08-observation-persistence-codex-prompt.md
docs/roadmap/release-1.1/prompts/08-observation-persistence-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Expected responsibilities:

- persistence implementation;
- durable write behavior;
- duplicate/idempotency enforcement;
- transaction/atomicity mechanics.

## Prohibited

No:

```text
Application redesign
Worker
Domain changes
test project changes
new packages unless separately authorized
```

---

# 13. WP09 — Historical Observation Retrieval

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/09-historical-observation-retrieval-codex-prompt.md
docs/roadmap/release-1.1/prompts/09-historical-observation-retrieval-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Expected responsibilities:

- retrieval implementation;
- filtering;
- ordering;
- physical-record-to-provider-independent observation reconstruction.

## Prohibited

No:

```text
Application concrete storage knowledge
Worker changes
Domain changes
test project changes
```

---

# 14. WP10 — Storage Validation & Failure Mapping

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/10-storage-validation-failure-mapping-codex-prompt.md
docs/roadmap/release-1.1/prompts/10-storage-validation-failure-mapping-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Only minimal Application changes are permitted if a previously approved WP04 failure contract requires a compile-level reconciliation and the WP10 prompt explicitly authorizes it. Otherwise Application delta must remain zero.

## Prohibited

No:

```text
Worker
Domain
tests
unrelated exception handling
```

---

# 15. WP11 — Dependency Registration & Configuration

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/11-dependency-registration-configuration-codex-prompt.md
docs/roadmap/release-1.1/prompts/11-dependency-registration-configuration-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Infrastructure/**
```

and only when required:

```text
src/AIQuantTradingResearch.Worker/**
```

for configuration handoff/composition boundary, though functional Worker execution remains WP12-owned.

Potential configuration files may be modified only if already part of repository configuration conventions and explicitly authorized by the WP11 prompt.

## Prohibited

No hidden in-memory fallback.

No unrelated DI redesign.

---

# 16. WP12 — Worker Persistent Market-Data Execution

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/12-worker-persistent-market-data-execution-codex-prompt.md
docs/roadmap/release-1.1/prompts/12-worker-persistent-market-data-execution-codex-prompt-chat.md
```

## Authorized Production Surface

```text
src/AIQuantTradingResearch.Worker/**
```

Minimal supporting composition changes in:

```text
src/AIQuantTradingResearch.Infrastructure/**
```

are allowed only if they are direct continuation of WP11 registration and the WP12 prompt explicitly authorizes them.

## Prohibited

No:

```text
Domain
Application contract redesign
new packages
new project references
CLI/service/API redesign
```

---

# 17. WP13 — Domain & Application Tests

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/13-domain-application-tests-codex-prompt.md
docs/roadmap/release-1.1/prompts/13-domain-application-tests-codex-prompt-chat.md
```

## Authorized Test Surface

```text
tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
```

## Testability Exceptions

No production visibility/reference/package change is automatically authorized.

If permanent tests require a narrow testability change, execution must stop and request a separate unblock authority.

## Prohibited

No Infrastructure test implementation.

No production behavior changes.

---

# 18. WP14 — Infrastructure & Persistence Tests

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/14-infrastructure-persistence-tests-codex-prompt.md
docs/roadmap/release-1.1/prompts/14-infrastructure-persistence-tests-codex-prompt-chat.md
```

## Authorized Test Surface

```text
tests/AIQuantTradingResearch.Infrastructure.Tests/**
```

Potential test-only package additions are allowed only through:

```text
Directory.Packages.props
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
```

and only when the WP14 prompt or a later narrow unblock explicitly authorizes them.

## Temporary Test Storage

Temporary database/storage files must not be committed.

They must be:

- isolated;
- cleaned after tests;
- ignored or placed outside repository tracked state.

## Prohibited

No production behavior changes under WP14.

---

# 19. WP15 — Architecture & Documentation Alignment

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/15-architecture-documentation-alignment-codex-prompt.md
docs/roadmap/release-1.1/prompts/15-architecture-documentation-alignment-codex-prompt-chat.md
```

## Authorized Architecture Test Surface

```text
tests/AIQuantTradingResearch.Architecture.Tests/**
```

## Authorized Current-State Documentation Surface

At minimum, after scope reconciliation, authorized existing documents may include:

```text
README.md
docs/architecture/solution/SOLUTION_ARCHITECTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
```

## Authorized New Persistence Documentation Surface

New persistence-specific architecture documents may be created only under:

```text
docs/architecture/market-data/
```

or another already-established architecture subfolder if the WP15 prompt proves that location is more consistent.

The WP15 prompt must enumerate exact changed/new documentation files before mutation.

## Prohibited

No production code, packages, project references, solution/build/script/workflow changes.

---

# 20. WP16 — Full Validation, Integration & Acceptance

## Governance Prompt Pair

```text
docs/roadmap/release-1.1/prompts/16-full-validation-integration-acceptance-codex-prompt.md
docs/roadmap/release-1.1/prompts/16-full-validation-integration-acceptance-codex-prompt-chat.md
```

## Expected Mutation Surface

WP16 is validation-only.

Expected:

```text
production delta = 0
test delta = 0
documentation delta = 0
package/project delta = 0
```

If WP16 detects a defect requiring correction, it must return `BLOCKED` and request a separate narrow unblock.

## Temporary Validation Artifacts

Allowed only when:

- required to prove acceptance;
- not committed;
- removed before completion.

---

# 21. Lifecycle Governance After WP16

After:

```text
RELEASE 1.1 ACCEPTED
```

additional lifecycle governance artifacts may be created under:

```text
docs/roadmap/release-1.1/prompts/
```

Expected categories:

```text
release-1.1-github-integration-codex-prompt.md
release-1.1-github-integration-codex-prompt-chat.md

release-1.1-post-merge-closure-codex-prompt.md
release-1.1-post-merge-closure-codex-prompt-chat.md
```

Unblock/reconciliation prompts may be added only when an actual blocker exists.

---

# 22. Integration Candidate Accounting Rule

Do not predefine a fixed final candidate file count in this manifest.

At integration time derive:

```text
accepted WP01–WP16 candidate
+ explicitly governed integration-lifecycle artifacts
= reconciled integration candidate N
```

Then freeze `N`.

If later governance artifacts are required after `N` is frozen, the authority creating them must explicitly define whether they:

- join the candidate and supersede `N`; or
- are out-of-band and must never be copied/staged.

No implicit count changes are allowed.

---

# 23. Project and Package Manifest Boundaries

## Normally Unchanged

Unless a specific WP explicitly authorizes otherwise:

```text
AIQuantTradingResearch.slnx
Directory.Build.props
global.json
eng/**
.github/**
```

## Package Governance

Persistence technology package additions are owned by WP07.

Test-only package additions are owned by WP14 or a narrow testability unblock.

Application/Domain packages should remain unchanged unless a later explicit authority proves otherwise.

## Project References

Expected production project references remain unchanged:

```text
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

No new project reference is authorized by default.

---

# 24. Test Project Ownership

```text
Domain.Tests
    owned by WP13 for Release 1.1 behavior

Application.Tests
    owned by WP13 for Release 1.1 behavior

Infrastructure.Tests
    owned by WP14 for Release 1.1 persistence behavior

Architecture.Tests
    owned by WP15 for Release 1.1 architecture rules
```

Temporary probes in earlier WPs must be removed before the WP exits unless their promotion to permanent tests is explicitly authorized.

---

# 25. Documentation Ownership

## Provider/Persistence Decision Docs

Owned by WP02:

```text
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PERSISTENCE_DECISION.md
```

## Current-State Architecture Docs

Owned by WP15 after exact gap reconciliation.

## Release Governance Docs

Owned by Release 1.1 governance/lifecycle prompts under:

```text
docs/roadmap/release-1.1/
```

No earlier WP may casually update evergreen architecture documentation merely because implementation changed.

---

# 26. Prohibited Release 1.1 Candidate Artifacts

Unless a later explicit authority exists, the candidate must not include:

- generated database files;
- test database files;
- local filesystem storage artifacts;
- `.env` files;
- secrets;
- real API keys;
- machine-specific connection files;
- IDE state;
- build output;
- package caches;
- temporary comparison/probe files;
- Release 1.2 implementation files;
- cloud deployment assets;
- new GitHub workflows;
- tags/releases represented as repository files.

---

# 27. Generated / Ignored Runtime State

Storage-engine runtime files must be isolated from the committed candidate.

The selected technology must have a clear policy for:

```text
local development storage location
test storage location
cleanup behavior
.gitignore treatment if necessary
```

Any `.gitignore` change requires explicit authorization from the owning WP, expected most likely WP07, and must be limited to the selected persistence technology's generated state.

---

# 28. Architecture Boundary File Rules

No Release 1.1 production file may introduce concrete persistence technology into:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
```

Application may own abstractions and provider-independent failure/result contracts only.

Concrete persistence types must remain within:

```text
src/AIQuantTradingResearch.Infrastructure/**
```

Worker may reference only authorized composition/configuration surfaces.

---

# 29. Friend Assembly / Visibility Rules

Existing testability boundaries must be preserved.

No new:

```text
InternalsVisibleTo
public visibility expansion
test-only production helper
```

is authorized automatically.

If testing requires one, the responsible WP must stop and request a narrowly scoped unblock.

---

# 30. GitHub Planning Artifacts

Release 1.1 GitHub planning should create/reconcile:

- one authoritative Release 1.1 milestone;
- exactly sixteen WP issues;
- Project integration using existing conventions;
- dependencies matching the execution plan.

No GitHub planning artifact is represented as a repository file except the planning prompt/report governance artifacts explicitly stored under Release 1.1 prompts if authorized.

The retired legacy milestone #42 must not be automatically reopened.

---

# 31. Expected WP Mutation Matrix

| WP | Primary Authorized Mutation Surface |
|---|---|
| WP01 | Governance/preflight only |
| WP02 | Persistence assessment/decision documentation |
| WP03 | Domain only if semantics require it |
| WP04 | Application contracts |
| WP05 | Application orchestration |
| WP06 | Infrastructure physical model |
| WP07 | Infrastructure storage engine/connection + required package manifest |
| WP08 | Infrastructure persistence |
| WP09 | Infrastructure retrieval |
| WP10 | Infrastructure validation/failure mapping |
| WP11 | Infrastructure DI/configuration; minimal Worker configuration handoff if authorized |
| WP12 | Worker execution/composition |
| WP13 | Domain.Tests + Application.Tests |
| WP14 | Infrastructure.Tests + test-only dependency support if authorized |
| WP15 | Architecture.Tests + authorized current-state/persistence documentation |
| WP16 | Validation only |

Any deviation requires an explicit authority.

---

# 32. Baseline Files Known to Exist from Release 1.0

Release 1.1 authorities must preserve the accepted Release 1.0 baseline, including:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Application/**
src/AIQuantTradingResearch.Infrastructure/**
src/AIQuantTradingResearch.Worker/**

tests/AIQuantTradingResearch.Domain.Tests/**
tests/AIQuantTradingResearch.Application.Tests/**
tests/AIQuantTradingResearch.Infrastructure.Tests/**
tests/AIQuantTradingResearch.Architecture.Tests/**
```

and current architecture/governance documentation.

This manifest does not authorize deletion of Release 1.0 assets merely because Release 1.1 adds persistence.

---

# 33. Candidate Reconciliation Requirements

Every WP execution report must classify visible changes into:

```text
EXPECTED GOVERNANCE
WP01 AUTHORIZED
WP02 AUTHORIZED
...
WP16 AUTHORIZED
UNBLOCK / RECONCILIATION AUTHORIZED
EXPECTED GENERATED / IGNORED
UNEXPECTED
```

Unexpected changes must be zero before a WP may claim completion.

Prior accepted WP changes must be preserved.

---

# 34. Staging / Commit Policy

WP01–WP16 prompts should normally prohibit:

```text
git add
git commit
git push
PR creation
```

The cumulative Release 1.1 candidate remains uncommitted until the dedicated post-WP16 integration step unless a later explicit governance model changes this strategy.

The integration step owns Git-history transport.

---

# 35. Whitespace / Diff Policy

Before integration:

```text
git diff --check = PASS
```

After staging during integration:

```text
git diff --cached --check = PASS
```

Untracked governance/architecture files must be checked for whitespace before candidate freeze, avoiding the Release 1.0 late-discovery problem.

Any whitespace-only authorization must:

- name exact files;
- name exact findings/scope;
- prove semantic equivalence;
- state whether its own governance artifact is in-band or out-of-band.

---

# 36. Clean-Checkout Requirement

Release 1.1 acceptance/closure must prove that committed repository state alone is sufficient.

No required behavior may depend on:

- untracked local database files;
- developer-machine state;
- secret files;
- pre-existing schema outside repository-controlled initialization;
- machine-global test data.

A fresh checkout must be able to restore/build/test successfully under the authorized environment.

---

# 37. Final Manifest Decision

This manifest freezes Release 1.1 ownership and mutation boundaries while deliberately leaving storage implementation filenames evidence-driven.

The allowed progression is:

```text
governance
→ GitHub planning
→ WP01–WP16
→ technical acceptance
→ Git/GitHub integration
→ human merge
→ post-merge closure
→ Release 1.2 governance design authorization
```

Anything outside these ownership boundaries requires explicit later authority.

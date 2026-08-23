# Release 1.6 WP05 — Durable Experiment Use-Case Integration — Codex Authority

## Mission

Execute only **Release 1.6 WP05 — Durable Experiment Use-Case Integration — GitHub issue #186** for **Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**.

Implement the minimum Application-owned orchestration composing the accepted Release 1.5 Experiment generation use case with the Release 1.6 WP04 durable-evidence contracts.

Required successful semantic flow:

`exact durable experiment request → existing Release 1.5 experiment generation → accepted Experiment Result → reduced durable evidence → durable acceptance → bounded durable outcome`

WP05 must not implement SQLite/schema v3, physical persistence/retrieval, storage-specific validation/failure translation, DI, Worker behavior, or permanent tests.

## Required authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- accepted Release 1.6 GitHub planning/restoration and definition-reconciliation authorities
- accepted WP01–WP04 execution evidence
- current Release 1.5 Experiment generation contracts/use case/models
- `src/AIQuantTradingResearch.Application/Experiments/ExperimentPersistenceContracts.cs`
- this authority and its five-line companion

WP02/WP03 remain semantic authority. WP04 remains contract authority.

## Starting gate

Before mutation verify:

- repository `samuel-santos-engineer/AIQuantTradingResearch`;
- branch `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind `0/0`;
- staged paths 0 and no unexpected tracked modifications;
- #182–#185 CLOSED / Done;
- #186 OPEN / Backlog;
- #187–#195 OPEN / Backlog;
- milestone #47 OPEN, 10 open / 4 closed;
- Project #2 fields correct and predecessor Release restoration 89/89 exact;
- WP04 contracts present and coherent;
- implemented SQLite schema remains v2;
- no premature WP06+ implementation;
- no Release 1.7 work.

Expected untracked Release 1.6 governance/candidate artifacts are not blockers.

If a mandatory gate fails, stop before moving #186 to In Progress.

## Authorized lifecycle

After all gates pass:

1. move #186 Backlog → In Progress;
2. implement only WP05;
3. validate;
4. post concise completion evidence to #186;
5. close #186;
6. set #186 In Progress → Done.

Final lifecycle must be #182–#186 CLOSED / Done, #187–#195 OPEN / Backlog, milestone #47 OPEN with 9 open / 5 closed.

## Manifest authority

`RELEASE_1.6_FILE_MANIFEST.md` is binding. Create/modify only WP05 Application paths assigned there.

Do not invent convenience helpers, aliases, DTOs, validators, mappers, or extra files. If the minimum coherent implementation cannot fit the manifest, stop and request the smallest corrective authority.

## Orchestration semantics

Implement one explicit Application-owned durable Experiment use-case path.

For one valid durable request:

1. validate request-level invariants appropriate to WP05;
2. invoke existing Release 1.5 `IExperimentGenerationUseCase` exactly once;
3. if generation succeeds, consume the complete accepted immutable Experiment Result;
4. project only WP03/WP04-required semantic evidence into the reduced durable evidence contract;
5. invoke `IDurableExperimentEvidenceStore` acceptance exactly once;
6. return a bounded Application outcome representing `NewlyAccepted` or `EquivalentExisting`.

Do not make existing Release 1.5 Experiment generation implicitly durable.

## Release 1.5 reuse

Reuse predecessor generation exactly. Do not duplicate or redefine:

- Feature Set generation;
- experiment computation;
- summary computation;
- definition/result identity construction;
- `aiq-experiment-identity-v1`;
- provenance/lineage semantics;
- accepted Release 1.5 validation.

WP05 consumes successful Release 1.5 Experiment Result evidence.

## Reduced durable evidence

Project the successful Experiment Result without fabricating Feature Values or source observations.

Preserve all WP03/WP04-required evidence, including as represented by accepted contracts:

- Experiment Result Identity;
- Experiment Definition identity/reference;
- exact Feature Set identity/reference;
- exact snapshot identity/version;
- dataset/source provenance references;
- count;
- aggregate-presence state;
- exact decimal mean/minimum/maximum;
- required provenance/lineage references.

Do not recreate a fake Release 1.5 object graph.

## Evidence establishment and short-circuiting

Durable acceptance occurs only after complete successful Release 1.5 generation.

If request validation fails, do not invoke generation.

If generation fails:

- do not invoke the store;
- do not fabricate durable success;
- do not fabricate identity/evidence.

If projection cannot establish complete valid durable evidence, do not invoke the store.

If store acceptance fails, do not fabricate success.

No later step executes after the first bounded failure.

Unknown programming defects propagate.

## Exactly-once semantics

For one valid request:

- generation exactly once;
- after successful generation, durable acceptance exactly once.

No retries, loops, verification regeneration, duplicate store calls, or implicit post-acceptance retrieval.

## Successful outcomes

Preserve explicit:

- `NewlyAccepted`;
- `EquivalentExisting`.

Both are success. `EquivalentExisting` must not become failure, exception, overwrite, retry, or second acceptance.

## Failure vocabulary

Preserve only the accepted bounded vocabulary:

- `InvalidRequest`
- `NotFound`
- `DependencyUnavailable`
- `InvalidEvidence`
- `IntegrityConflict`

Map predecessor/store bounded failures only as required by accepted contracts while preserving distinctions and deterministic first-failure behavior.

Never normalize `IntegrityConflict` to `EquivalentExisting`.

Do not expose SQLite/storage-specific failures.

Do not broadly catch unknown exceptions.

## Identity, decimal, aggregate, provenance fidelity

Preserve the exact Release 1.5 Experiment Result Identity produced by generation. Do not recompute it under a new scheme and do not create a persistence identity.

Preserve exact `decimal` evidence; never convert to `double`/`float`, round, or culture-format semantically.

Preserve aggregate presence:

- empty success: count 0, aggregates absent;
- non-empty success: exact count and exact mean/minimum/maximum present.

Preserve required provenance and acyclic lineage. Storage must not become a lineage parent or semantic node.

## Downstream authority protection

WP05 must not implement responsibilities belonging to:

- WP06 — schema-v3 physical model;
- WP07 — Experiment Result persistence implementation;
- WP08 — exact Experiment Result retrieval implementation;
- WP09 — storage validation/failure mapping;
- WP10 — dependency registration/configuration;
- WP11 — one-shot durable Experiment Worker;
- WP12 — permanent persistence tests;
- WP13 — architecture/documentation alignment;
- WP14 — integration/acceptance.

No SQLite, SQL, connection, transaction, migration, physical mapping, repository implementation, storage exception translation, DI, Worker routing/configuration, or permanent tests.

Implemented schema remains v2.

## Architecture and predecessor preservation

WP05 production changes are Application-only as authorized by the manifest.

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No package/project/reference changes.

Preserve Releases 1.1–1.5, including existing Release 1.5 in-memory Experiment execution and Worker behavior. The durable path is additive and explicit.

## Explicit deferrals

Do not implement Feature Set persistence, registry/history, list/search/comparison, update/delete, additional experiments, strategies/signals/backtesting, scheduling/retries/recovery, provider acquisition/fallback, workspace/UI/API, AI/ML, or Release 1.7 work.

## Temporary offline probe

Permanent WP05 tests are not authorized.

A removable offline probe may verify:

- valid request → generation once;
- exact durable projection;
- store acceptance once;
- `NewlyAccepted`;
- `EquivalentExisting`;
- generation failure prevents store call;
- store failure produces bounded failure;
- unknown defects propagate;
- empty/non-empty fidelity.

Probe must be fully removed, offline, use no real credentials, create no database/provider/network activity, and leave no package/project/reference or generated residue.

## Canonical validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts remain:

- Domain.Tests 11/11
- Application.Tests 102/102
- Infrastructure.Tests 112/112
- Architecture.Tests 13/13
- Total 238/238
- Skipped 0

Require warnings/errors 0/0, formatting PASS, Gitleaks PASS, `git diff --check` PASS, `git diff --cached --check` PASS, direct expected-untracked whitespace/final-newline checks PASS, staged paths 0, residue 0, provider/network activity 0, and real credentials 0.

Verify Domain/Infrastructure/Worker/permanent-test deltas 0, package/project/reference deltas 0/0/0, schema v2, graph unchanged and acyclic, no WP06+ implementation, and no Release 1.7 work.

## Mutation budget

Authorized repository mutation: only manifest-defined WP05 Application path(s).

Authorized GitHub mutations:

1. #186 Backlog → In Progress;
2. completion evidence comment;
3. close #186;
4. #186 In Progress → Done.

Not authorized: staging, commits, branches, pushes, PRs, tags/releases, milestone closure, #187–#195 mutation, definition/plan/manifest edits, WP02/WP03 edits, or WP04 redesign outside explicit manifest authority.

## Stop conditions

Stop with #186 OPEN / In Progress if:

- manifest authority is ambiguous;
- WP04 contracts cannot support orchestration without unauthorized redesign;
- Feature Values would need fabrication;
- Release 1.5 generation semantics would need modification;
- SQLite/schema-v3 decisions become necessary;
- package/reference changes become necessary;
- extra files are required;
- canonical validation fails;
- schema changes from v2;
- unexpected database/provider/network activity occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

## Completion evidence

Post concise #186 evidence covering:

- exact changed paths;
- explicit durable flow;
- exactly-once Release 1.5 generation;
- reduced durable evidence projection;
- exactly-once store acceptance;
- `NewlyAccepted` / `EquivalentExisting`;
- bounded first-failure behavior;
- unknown-defect propagation;
- identity/decimal/provenance fidelity;
- no Feature Value fabrication;
- no SQLite/schema/DI/Worker changes;
- schema v2;
- canonical 238/238;
- next WP06/#187.

## Required execution report

Report starting state, exact paths, orchestration flow, Release 1.5 reuse, call counts, evidence projection, Feature Value exclusion, identity/decimal/provenance fidelity, successful outcomes, bounded failure mapping, short-circuiting, unknown-defect propagation, downstream authority preservation, schema/DI/Worker preservation, temporary probe if used, canonical validation, security/whitespace/residue, mutation accounting, GitHub lifecycle, blockers, and next WP.

## Completion marker

On success end exactly:

`RELEASE 1.6 WP05 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Schema-v3 Physical Model — GitHub issue #187`

If blocked end:

`RELEASE 1.6 WP05 BLOCKED`

and identify the smallest corrective authority required.

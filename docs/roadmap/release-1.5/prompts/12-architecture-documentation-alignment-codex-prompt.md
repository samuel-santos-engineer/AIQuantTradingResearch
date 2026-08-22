# Release 1.5 WP12 — Architecture & Documentation Alignment

## GitHub Issue
`#179 — Release 1.5 WP12 — Architecture & Documentation Alignment`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP12 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is **Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**.

Built-in experiment: `simple-return-descriptive-summary-v1`.
Identity scheme: `aiq-experiment-identity-v1`.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- accepted WP04–WP09 production implementation
- WP10 `ExperimentApplicationTests.cs`
- WP11 `ExperimentCompositionTests.cs`
- existing Architecture.Tests and all 13 current architecture rules
- current-state documentation authorized for WP12 by the manifest
- WP01–WP11 completion evidence
- this WP12 authority and its five-line companion

Repository truth and accepted Release 1.5 authorities take precedence over assumptions.

WP12 aligns architecture enforcement and current-state documentation with the implementation already accepted. It must not invent future behavior.

## 2. Objective

Reconcile Release 1.5 architecture and documentation so the repository accurately describes and, only where warranted, structurally enforces the deterministic research experiment foundation proven through WP04–WP11.

The aligned state must clearly represent:

- Application ownership of experiment semantics, identity, validation, computation, and orchestration;
- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- Feature Set → Experiment Result provenance and acyclic lineage;
- exact snapshot/version-derived feature integration;
- immutable in-memory experiment evidence;
- one-shot explicit Worker Experiment mode;
- Experiment → Feature → Release 1.3 pipeline routing precedence;
- schema v2 unchanged;
- no experiment persistence;
- no provider fallback/acquisition in Experiment mode;
- no scheduling, retries, recovery, registry, history, notebooks, backtesting, AI/ML, or Release 1.6 behavior.

## 3. Expected Starting State

Reconcile rather than assume:

- branch: `main`;
- `HEAD == origin/main == 2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- #168–#178: CLOSED / Done;
- #179: OPEN / Backlog;
- #180: OPEN / Backlog;
- milestone #46: OPEN, 2 open / 11 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 work: none;
- permanent baseline: Domain 11, Application 102, Infrastructure 112, Architecture 13, total 238;
- SQLite schema: v2.

Expected cumulative accepted Release 1.5 changes remain unstaged.

If #178 is not Closed/Done or #180 has started, stop before mutation.

## 4. WP12 Lifecycle Start

After starting-state gates pass, move only #179 Project #2 Status from Backlog to In Progress and read it back.

Do not mutate #180. It must remain OPEN / Backlog throughout WP12.

## 5. Hard File Boundary

Use `RELEASE_1.5_FILE_MANIFEST.md` as the sole path authority.

Before editing, enumerate the exact WP12-authorized paths and classify them as:

- Architecture.Tests candidate path(s), if any;
- current-state documentation paths.

Do not infer authorization from Release 1.4 filenames alone. Use the actual Release 1.5 manifest.

No file outside the WP12 manifest allocation may be changed.

Do not stage or commit.

## 6. Architecture-Test Decision Gate

Inspect all existing 13 Architecture.Tests before changing them.

For each plausible Release 1.5 structural invariant, determine whether it is:

1. already enforced by an existing rule;
2. behavioral and therefore better protected by WP10/WP11 tests;
3. documentation-only/current-state information;
4. a genuinely new stable structural rule that is non-redundant and architecture-testable.

A zero Architecture.Tests delta is explicitly valid and preferred when categories 1–3 cover all Release 1.5 requirements.

Do not add a rule merely to produce a test delta.

## 7. Candidate Structural Invariants

Evaluate, without presuming a new test is required:

- Domain remains dependency-free;
- Application depends only on Domain;
- Infrastructure depends on Application and does not own experiment semantics;
- Worker depends on Application and Infrastructure;
- production graph remains acyclic;
- experiment contracts/semantics remain Application-owned;
- provider/HTTP implementation remains confined to established boundaries;
- Worker owns composition/host execution rather than experiment semantics;
- Release 1.3 pipeline boundaries remain intact.

If existing rules already enforce these, record zero delta.

Do not encode behavioral assertions such as exact summary arithmetic, identities, call counts, exit codes, or configuration validation as architecture rules.

## 8. New Architecture Rule Standard

Add an Architecture.Test only if all are true:

- stable beyond Release 1.5;
- structural rather than behavioral;
- observable from the production dependency/type structure;
- not redundant with an existing rule;
- expressible without brittle namespace/file-name overfitting;
- authorized by the manifest.

If added, explain precisely what regression it prevents that the existing 13 rules do not.

Otherwise preserve 13/13.

## 9. Documentation Truth Source

Documentation must describe the implementation actually accepted through WP04–WP11, not the planning intent where they differ.

Cross-check claims against:

- public contracts and immutable models;
- actual canonical identity implementation;
- summary computer;
- validator;
- experiment use case;
- DI registrations;
- Worker configuration and routing;
- WP10 semantic tests;
- WP11 composition/process tests.

Do not document unimplemented abstractions.

## 10. Built-In Experiment

Current-state docs must identify the sole Release 1.5 built-in experiment as:

`simple-return-descriptive-summary-v1`

Describe it as consuming accepted `simple-return-lag-1-v1` Feature Set evidence and producing immutable descriptive evidence:

- count;
- arithmetic mean when non-empty;
- minimum when non-empty;
- maximum when non-empty.

Empty Feature Sets succeed with count zero and absent aggregates.

## 11. Identity Documentation

Align documentation with `aiq-experiment-identity-v1`:

- distinct Experiment Definition and Experiment Result identities;
- SHA-256 canonical fingerprints;
- 64 lowercase hexadecimal external fingerprint;
- result identity binds the exact Feature Set identity;
- equivalent semantic recomputation yields the same result identity;
- distinct Feature Set identities remain experiment-distinct even when summaries are equal;
- empty successful results receive deterministic identities bound to exact Feature Sets.

Do not restate the full canonical byte-level specification everywhere; link/reference the authoritative identity document where appropriate.

## 12. Ownership Documentation

Make ownership explicit:

Application owns:

- experiment definition/model/contracts;
- identity computation;
- validation/failure semantics;
- deterministic summary computation;
- feature-to-experiment orchestration.

Worker owns:

- explicit configuration binding;
- mode selection;
- one-shot invocation;
- bounded presentation/exit behavior.

Infrastructure does not own experiment semantics or persistence.

## 13. Integration Flow

Document the accepted logical flow accurately:

exact Experiment request
→ existing exact-snapshot Feature generation
→ returned Feature Set validation
→ deterministic descriptive summary
→ canonical experiment identity/provenance
→ immutable in-memory result

No provider acquisition or experiment persistence belongs to this path.

## 14. Worker Routing

Document deterministic routing precedence:

1. any explicit Experiment selector chooses Experiment mode;
2. otherwise Feature selectors choose Release 1.4 Feature mode;
3. otherwise Release 1.3 pipeline behavior remains the fallback path.

Partial/malformed Experiment intent fails; it does not fall back to Feature or pipeline mode.

Avoid documenting incidental console formatting as architecture.

## 15. Configuration Documentation

Document exact Release 1.5 configuration keys:

- `Experiment:SnapshotIdentity`
- `Experiment:SnapshotVersion`

The experiment definition remains code-owned as `simple-return-descriptive-summary-v1`.

Do not document configurable formulas, lags, aggregation selection, retries, schedules, or persistence because they do not exist.

## 16. Failure Semantics Documentation

Describe bounded failure behavior at the appropriate abstraction level, including accepted categories exposed by the current contracts.

Preserve these principles:

- fail-stop validation;
- no partial successful Experiment Result after failure;
- no fabricated Experiment Result identity;
- bounded predecessor failure mapping;
- decimal overflow maps only where the accepted boundary defines it;
- unknown defects propagate rather than being broadly normalized.

Use actual code vocabulary.

## 17. Provenance and Lineage

Document that Experiment Result evidence is bound to the exact Feature Set and predecessor evidence.

Preserve acyclic conceptual lineage through:

source evidence → dataset/snapshot/version → Feature Set → Experiment Result.

Use exact public model terminology rather than inventing a generalized lineage engine.

## 18. Persistence Boundary

State clearly:

- experiment results are in-memory only in Release 1.5;
- no experiment registry/history/cache/table exists;
- feature persistence remains absent unless predecessor architecture says otherwise;
- SQLite remains schema v2;
- no migration is introduced.

Do not imply durability.

## 19. Provider Boundary

State clearly:

- Experiment mode consumes existing accepted local snapshot/feature pathways;
- it does not perform provider acquisition or provider fallback;
- no new network dependency was introduced.

Preserve existing provider-confinement architecture.

## 20. Determinism

Document determinism at a semantic level:

- decimal-only summary computation;
- complete Feature Set cardinality;
- culture-independent canonical identity;
- equivalent semantic reruns produce equivalent identities/evidence;
- no operational timestamps/process metadata participate in semantic identity.

Do not duplicate detailed tests in prose.

## 21. Testing Strategy Alignment

Update authorized testing documentation to reflect the permanent Release 1.5 baseline:

- Domain.Tests: 11;
- Application.Tests: 102;
- Infrastructure.Tests: 112;
- Architecture.Tests: 13 unless a justified WP12 rule is added;
- total: 238 unless a justified Architecture.Tests delta is added.

Describe WP10 Application semantic coverage and WP11 Infrastructure composition/Worker coverage at a useful architectural level.

Do not list every test method.

## 22. Observability Alignment

Where authorized, document the one-shot Experiment path's bounded semantic presentation without implying a telemetry backend.

No durable experiment telemetry, run history, metrics backend, tracing backend, or persistence is introduced by Release 1.5.

Preserve existing observability principles.

## 23. Public Contracts Alignment

Where authorized, document the public experiment contract surface accurately:

- request;
- result/failure contract;
- generation use case;
- summary-computation seam;
- validation seam;
- immutable identity/evidence models.

Do not expose internal helper details as public architecture.

## 24. Dependency Injection Alignment

Where authorized, document the accepted transient registrations:

- `IExperimentGenerationUseCase → ExperimentGenerationUseCase`;
- `IExperimentSummaryComputer → SimpleReturnDescriptiveSummaryComputer`;
- `IExperimentGenerationValidator → ExperimentGenerationValidator`.

State that existing Release 1.4 Feature generation composition is reused.

Resolution remains side-effect-free.

## 25. README Alignment

If README is manifest-authorized for WP12, update only the minimum current-state Release 1.5 representation needed for repository front-door accuracy.

Do not turn README into the detailed experiment specification.

Prefer concise capability/status/navigation updates with links to authoritative architecture documents.

## 26. Explicit Deferrals

Keep Release 1.6+ boundaries explicit where relevant:

- experiment persistence/registry/history/workspace;
- feature persistence and broader feature libraries;
- notebooks/visualization/APIs;
- strategies/signals/backtesting/portfolio/risk;
- AI/ML/explainability/MLOps;
- live acquisition orchestration;
- scheduling/retries/recovery/checkpoints;
- generalized plugins/expressions/DAGs/distributed execution;
- durable telemetry/execution history.

Do not start or design these beyond concise boundary statements already accepted.

## 27. Historical Preservation

Do not rewrite historical Release 1.1–1.4 documents merely to make them sound current unless the WP12 manifest explicitly authorizes a current-state document that spans releases.

Preserve:

- Release 1.1 persistence semantics;
- Release 1.2 dataset semantics;
- Release 1.3 five-stage pipeline;
- Release 1.4 deterministic feature semantics.

Release 1.5 extends them; it does not retroactively redefine them.

## 28. Documentation Link Validation

Validate all modified local Markdown links.

Require:

- no broken relative links introduced;
- no stale Release 1.5 path/name references in modified documents;
- no accidental references to excluded planning-authority files as repository product documentation.

Use existing repository link-validation convention if available.

## 29. Semantic Consistency Sweep

Across all WP12-authorized modified documents, search for contradictions involving:

- experiment name;
- identity scheme;
- ownership layer;
- persistence status;
- schema version;
- Worker routing precedence;
- Feature Set input;
- test baseline;
- Release 1.6 deferrals.

Resolve only contradictions within authorized paths.

If a contradiction exists in an unauthorized path and materially blocks correctness, stop and report it rather than editing outside the manifest.

## 30. Production Freeze

WP12 does not authorize production behavior changes.

Production delta must be 0.

Do not modify Domain, Application, Infrastructure, or Worker production code.

Do not modify packages, project references, or schema.

## 31. Permanent Test Delta

Expected default:

- Domain delta: 0;
- Application delta: 0;
- Infrastructure delta: 0;
- Architecture delta: 0.

Only Architecture.Tests may change, and only if the Architecture-Test Decision Gate proves a genuinely new non-redundant structural rule and the manifest authorizes the path.

A zero Architecture.Tests delta is a successful outcome.

## 32. Targeted Validation

If Architecture.Tests are unchanged, run the existing Architecture.Tests and require 13/13 PASS.

If a justified rule is added, run the targeted new rule and full Architecture.Tests; report baseline/final/delta and rationale.

Validate modified documentation locally before canonical verification.

## 33. Canonical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Default expected counts:

- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: 112/112;
- Architecture.Tests: 13/13;
- total: 238/238;
- skipped: 0;
- warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

If a justified Architecture.Tests delta exists, adjust only that count and total.

## 34. Whitespace and Residue

Run:

- `git diff --check`;
- `git diff --cached --check`;
- direct trailing-whitespace checks for all WP12-authorized modified/new files and relevant untracked governance artifacts.

Require:

- database/WAL/SHM/journal residue: 0;
- temporary probes/scripts/projects: 0;
- generated residue: 0.

## 35. Architecture and Schema Validation

Confirm production graph remains:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

Require:

- unexpected edges: 0;
- cycles: 0;
- package/reference changes: 0;
- SQLite schema: v2;
- experiment persistence: absent;
- provider leakage: absent.

## 36. Security / Network

Require:

- Gitleaks PASS;
- real credentials: 0;
- provider/network activity: 0.

WP12 should not need runtime provider access.

## 37. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- begin WP13;
- begin Release 1.6.

Git transport mutation budget: `0`.

Repository mutation budget: exact WP12 manifest-authorized paths only.

## 38. Authorized GitHub Mutation Budget

At WP12 start after gates pass:

1. #179 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise completion-evidence comment to #179;
3. close #179 as completed;
4. set #179 Project Status to Done.

Do not mutate #180.

Milestone #46 remains OPEN.

## 39. Completion Gate

WP12 may close only if:

- #178 is Closed/Done;
- #179 was In Progress during execution;
- #180 remains Open/Backlog;
- exact manifest path accounting passes;
- Architecture.Tests decision is explicitly justified;
- no redundant/brittle architecture rule is added;
- documentation matches actual WP04–WP11 implementation;
- `simple-return-descriptive-summary-v1` is represented accurately;
- `aiq-experiment-identity-v1` is represented accurately;
- Application/Worker/Infrastructure ownership is accurate;
- Feature Set → Experiment lineage is accurate;
- Worker routing precedence is accurate;
- schema v2/no experiment persistence are explicit;
- no provider fallback/network behavior is implied;
- Release 1.3 and Release 1.4 behavior remains preserved;
- test baseline is accurate;
- modified local Markdown links pass;
- semantic consistency sweep passes;
- production/package/reference/schema deltas are 0;
- all permanent tests pass;
- canonical verification passes;
- warnings/errors 0/0;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- Release 1.6 work 0.

If any gate fails, do not close #179 or mark Done.

## 40. Completion Evidence Comment

On success, post concise evidence to #179 covering:

- Architecture.Tests decision and rationale;
- Architecture.Tests baseline/final/delta;
- exact modified documentation paths;
- experiment/identity/ownership/integration alignment;
- Worker routing/configuration alignment;
- persistence/schema/provider boundaries;
- testing baseline alignment;
- documentation-link and consistency validation;
- full permanent test result;
- zero production/package/reference/schema changes;
- canonical verification/Gitleaks/whitespace PASS;
- zero residue/network activity;
- #180 preserved Open/Backlog.

## 41. Final Read-Back

After successful closure verify:

- #179: CLOSED / Done;
- #180: OPEN / Backlog;
- milestone #46: OPEN;
- milestone counts: 1 open / 12 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 state accurately.

## 42. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #178 is not Closed/Done;
- #180 started unexpectedly;
- WP12 manifest ownership is ambiguous;
- documentation requires editing an unauthorized path to become truthful;
- a genuine new architecture rule requires an unauthorized test path;
- accepted implementation contradicts frozen Release 1.5 semantics materially;
- production mutation would be required;
- package/reference/schema mutation would be required;
- premature WP13 or Release 1.6 work exists;
- canonical verification fails;
- documentation-link/security/whitespace/residue gates fail.

Report the smallest corrective authority required.

## 43. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP12 manifest path inventory;
6. existing 13 Architecture.Tests analysis;
7. candidate structural invariants evaluated;
8. Architecture.Tests decision and rationale;
9. exact documentation paths changed;
10. experiment semantic alignment;
11. identity/provenance alignment;
12. ownership and module-boundary alignment;
13. Worker routing/configuration alignment;
14. persistence/schema/provider boundaries;
15. deterministic behavior documentation;
16. testing/observability/public-contract/DI alignment where authorized;
17. Release 1.6 deferrals;
18. documentation-link validation;
19. semantic consistency sweep;
20. permanent test counts and Architecture delta;
21. canonical validation;
22. graph/schema/package/reference validation;
23. security/whitespace/residue/network evidence;
24. production delta;
25. GitHub lifecycle mutations;
26. final #179/#180/milestone state;
27. findings/blockers;
28. next authorized WP.

## 44. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP12 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP13 — Full Validation, Integration & Acceptance — GitHub issue #180`

Do not begin WP13.

If blocked, end:

`RELEASE 1.5 WP12 BLOCKED`

and identify the smallest corrective authority required.

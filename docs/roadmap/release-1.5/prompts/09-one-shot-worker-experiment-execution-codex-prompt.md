# Release 1.5 WP09 — One-Shot Worker Experiment Execution

## GitHub Issue
`#176 — Release 1.5 WP09 — One-Shot Worker Experiment Execution`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP09 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP09 exposes the already-accepted Release 1.5 experiment Application capability through one explicit, bounded, one-shot Worker execution path.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP04 experiment model/contracts and identity implementation
- WP05 summary computer
- WP06 validator
- WP07 `ExperimentGenerationUseCase.cs`
- WP08 Application DI registrations
- WP08 `ExperimentExecutionConfiguration.cs`
- accepted Release 1.4 Worker feature execution/configuration
- accepted Release 1.3 Worker pipeline execution path
- current `Program.cs`
- current Worker presentation/exit-code conventions
- WP01–WP08 completion evidence
- this WP09 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If one-shot experiment execution cannot be added without changing frozen experiment semantics, predecessor mode semantics, persistence, or provider behavior, stop and request the smallest corrective authority.

---

## 2. Objective

Implement exactly the manifest-authorized Worker experiment execution path.

Intended flow:

`Experiment:* configuration`
`→ ExperimentExecutionConfiguration`
`→ resolve IExperimentGenerationUseCase`
`→ invoke exactly once`
`→ present bounded semantic evidence`
`→ return deterministic process exit code`
`→ terminate`

Experiment execution must be:

- explicit;
- one-shot;
- synchronous/bounded according to accepted contracts;
- offline with respect to provider acquisition;
- non-persistent;
- deterministic in semantic output.

WP09 must not add permanent tests; WP10 and WP11 own those.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`.

Expected lifecycle:

- #168–#175: CLOSED / Done;
- #176 WP09: OPEN / Backlog;
- #177 WP10: OPEN / Backlog;
- #178–#180: OPEN / Backlog;
- milestone #46: OPEN with 5 open / 8 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- SQLite schema: v2.

Expected cumulative Release 1.5 working-tree changes are accepted and unstaged.

If #175 is not Closed/Done or #177 has started, stop before mutation.

---

## 4. WP09 Lifecycle Start

After starting-state gates pass:

- move only #176 Project #2 Status from Backlog to In Progress.

Read back the state.

If #176 is already In Progress solely because this exact WP09 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #177.

#177 must remain OPEN / Backlog throughout WP09.

---

## 5. Mandatory Worker Inventory

Before editing, inspect and report:

- current `Program.cs` mode-selection behavior;
- Release 1.3 pipeline path;
- Release 1.4 feature-mode selection;
- Release 1.4 `FeatureExecution` presentation/exit behavior;
- WP08 `ExperimentExecutionConfiguration`;
- actual `IExperimentGenerationUseCase` method signature;
- actual experiment result/success/failure contract;
- current host/service-provider lifetime/disposal conventions;
- existing logging/output conventions.

Do not guess method signatures or mode precedence.

---

## 6. Explicit Experiment Mode Selection

Experiment mode must be selected only by explicit presence of Release 1.5 experiment configuration.

Use the accepted WP08 configuration namespace and keys.

Expected selectors:

- `Experiment:SnapshotIdentity`
- `Experiment:SnapshotVersion`

Reconcile actual configuration implementation.

Experiment mode must not be selected merely because experiment services are registered.

No automatic experiment execution.

---

## 7. Mode Precedence

Define deterministic mode precedence that preserves predecessor behavior.

Required principle:

- explicit Experiment configuration selects experiment mode;
- absent Experiment configuration preserves Release 1.4 feature mode when its selector is present;
- absent Experiment and Feature selectors preserves the Release 1.3 pipeline path.

If both Experiment and Feature selectors are present, use the precedence already fixed by accepted Release 1.5 authorities or the narrowest deterministic interpretation consistent with explicit experiment selection.

Do not silently run both modes.

Report the final precedence explicitly.

---

## 8. Partial Experiment Configuration

If any Experiment selector is present but the required Experiment configuration is incomplete or malformed:

- treat the invocation as an attempted experiment-mode request;
- fail before experiment use-case invocation;
- return deterministic non-zero exit code;
- do not fall back to feature or pipeline mode.

This prevents malformed experiment intent from accidentally executing another mode.

Use WP08 configuration validation.

---

## 9. One-Shot Use-Case Invocation

For valid Experiment configuration:

- resolve `IExperimentGenerationUseCase`;
- construct the exact accepted request using WP08 configuration;
- invoke the use case exactly once;
- process exactly one returned governed result;
- terminate.

No:

- loop;
- polling;
- retry;
- scheduler;
- daemon behavior;
- repeated experiment execution;
- automatic rerun.

---

## 10. Success Presentation

On successful experiment execution, present concise semantic evidence sufficient to demonstrate the deterministic research result.

Use actual accepted model fields.

Expected semantic evidence includes:

- experiment definition identifier;
- Experiment Definition Identity/fingerprint where exposed;
- exact Feature Set Identity;
- Experiment Result Identity;
- count;
- mean/minimum/maximum for non-empty results;
- explicit absence of aggregates for empty result.

Do not print operationally sensitive values.

Do not dump entire internal objects.

Do not introduce persistence merely for presentation.

---

## 11. Empty Result Presentation

For a successful empty Feature Set:

- exit success;
- present count `0`;
- present aggregate absence clearly;
- present the deterministic Experiment Result Identity.

Do not display fake numeric zero aggregates unless the accepted model itself defines them, which Release 1.5 does not intend.

---

## 12. Single / Non-Empty Presentation

For single/non-empty success:

- present exact decimal mean/minimum/maximum;
- use culture-independent semantic formatting;
- preserve deterministic evidence.

Do not round for convenience.

Do not convert to floating point.

---

## 13. Bounded Failure Presentation

For each bounded Release 1.5 experiment failure returned by the use case:

- present a concise semantic failure category;
- return deterministic non-zero exit code;
- terminate;
- do not fabricate Experiment Result Identity or summary evidence.

Use exact repository failure vocabulary.

Do not expose stack traces for governed failures as normal semantic output.

---

## 14. Exit-Code Policy

Required policy unless an accepted repository convention is more specific:

- successful experiment result → `0`;
- malformed/incomplete Experiment configuration → `1`;
- bounded experiment failure → `1`;
- unknown defects → remain unhandled and follow normal process failure behavior.

Do not invent a complex exit-code taxonomy.

Preserve Release 1.3/1.4 exit semantics outside experiment mode.

---

## 15. Unknown Defect Propagation

Do not add broad exception normalization around experiment execution.

Unknown defects from:

- configuration infrastructure outside governed parsing;
- DI resolution;
- feature generation;
- summary computation;
- experiment identity construction;
- other programming/system failures

must not be rewritten as bounded experiment failures.

No catch-all `Exception` that returns `1` as if the defect were governed.

Preserve natural unhandled-failure behavior.

---

## 16. No Provider Acquisition

Experiment mode consumes already-persisted snapshot evidence through the accepted Release 1.4 feature-generation boundary.

Do not:

- call Twelve Data;
- call HTTP;
- trigger acquisition;
- fall back to provider when snapshot is missing;
- use real credentials.

A dummy provider key may be supplied only if existing composition requires one to build the graph.

No provider/network activity is allowed in WP09 validation.

---

## 17. No Experiment Persistence

Do not persist:

- experiment results;
- experiment identities;
- experiment history;
- summary evidence;
- execution history.

Do not create experiment tables/files.

SQLite remains schema v2.

Existing snapshot reads are allowed only through accepted Release 1.4 infrastructure during offline Worker validation.

---

## 18. Release 1.3 Pipeline Preservation

When Experiment configuration is absent and Feature configuration does not select feature mode, the existing Release 1.3 five-stage pipeline path must remain behaviorally unchanged.

Do not:

- add experiment as a sixth stage;
- invoke experiment after pipeline success;
- alter pipeline identity;
- alter pipeline failure mapping;
- alter provider behavior.

---

## 19. Release 1.4 Feature Mode Preservation

When Experiment configuration is absent and valid Feature configuration selects Release 1.4 feature mode, preserve existing feature execution behavior.

Do not refactor feature mode merely to share presentation code unless the change is manifest-authorized and behaviorally neutral.

WP09 should minimize predecessor edits.

---

## 20. Worker Execution Helper

If the manifest authorizes a dedicated Worker helper, implement the minimum experiment execution helper analogous to accepted Release 1.4 patterns.

Expected logical path:

`src/AIQuantTradingResearch.Worker/ExperimentExecution.cs`

The helper may own:

- one-shot invocation;
- bounded result presentation;
- experiment-mode exit-code mapping.

It must not own:

- experiment semantics;
- summary computation;
- identity computation;
- persistence;
- retries.

Application remains authoritative for semantics.

---

## 21. Program.cs Change

Modify `Program.cs` only as required by the manifest to:

- detect explicit Experiment intent;
- validate/build Experiment configuration;
- select experiment mode;
- invoke the Worker experiment helper once;
- preserve predecessor mode routing.

Keep the change minimal.

Do not introduce a generalized command framework.

---

## 22. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as hard path authority.

Expected logical WP09 paths:

- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/ExperimentExecution.cs`

Use exact manifest paths.

Before mutation:

1. enumerate exact authorized WP09 paths;
2. verify WP10/WP11 test paths are excluded;
3. verify no premature experiment Worker implementation exists.

Expected deltas:

- Worker production: exact WP09 paths only;
- Application: 0;
- Domain: 0;
- Infrastructure: 0;
- permanent tests: 0;
- packages/projects/references/schema: 0/0/0/0.

If actual manifest ownership differs, follow the manifest and report it.

---

## 23. No Permanent Tests in WP09

WP10 owns permanent Application experiment tests.

WP11 owns permanent composition/Worker validation.

Do not add permanent tests.

Use removable offline process/probe evidence only.

Permanent baseline remains 214.

---

## 24. Offline Worker Validation Setup

Use only synthetic accepted evidence.

A removable setup/probe may:

- create a temporary schema-v2 SQLite database;
- seed exact immutable snapshot evidence through accepted storage APIs or a narrowly isolated deterministic fixture;
- run the built Worker with `--no-build`;
- supply dummy provider credentials only to satisfy composition;
- capture exit code and output.

Do not contact a provider.

Remove all temporary databases, WAL/SHM/journal files, fixtures, scripts, and project hooks.

---

## 25. Required Process Acceptance Matrix

Prove through bounded external Worker execution where practical:

1. valid non-empty snapshot → experiment success, exit `0`;
2. same semantic request in a second process → identical Experiment Result Identity;
3. valid empty Feature Set path → success, count `0`, aggregates absent, exit `0`;
4. valid single-observation-derived empty Feature Set path → success, count `0`, aggregates absent, exit `0`;
5. malformed Experiment identity → exit `1`, no use-case success evidence;
6. missing required Experiment configuration after partial Experiment intent → exit `1`, no fallback;
7. exact snapshot NotFound → bounded failure, exit `1`;
8. unavailable storage/dependency where safely inducible offline → bounded failure, exit `1`;
9. no fabricated Experiment Result Identity on failure;
10. no provider/network activity;
11. no experiment persistence tables;
12. absent Experiment selector preserves predecessor routing.

If a case cannot be safely induced without unauthorized production change, report the limitation rather than weaken the architecture.

---

## 26. Equivalent Second Process

For a successful non-empty case:

- execute the built Worker in a fresh second process with identical semantic input;
- require identical Experiment Result Identity;
- require identical count/aggregate semantic evidence.

Operational process metadata may differ and must not affect semantic identity.

---

## 27. Empty / Single Snapshot Semantics

Remember the experiment consumes Release 1.4 Feature Sets:

- an empty snapshot produces an empty Feature Set;
- a one-observation snapshot also produces an empty Feature Set.

Both therefore produce successful Release 1.5 count-zero experiment evidence bound to their respective exact Feature Set identities.

Do not expect their Experiment Result identities to collapse to a global empty sentinel.

If their Feature Set identities differ, their Experiment Result identities must remain distinct.

---

## 28. Output Determinism

Semantic output should be stable and culture-independent.

For decimals, use invariant exact formatting.

Do not include current timestamp/duration in semantic identity lines.

Operational logs may exist according to predecessor behavior, but validation of deterministic evidence should target the semantic output fields.

---

## 29. No Secret Exposure

Do not print:

- provider API keys;
- connection strings containing secrets;
- environment secret values.

Gitleaks must pass.

Use dummy credentials in offline validation.

---

## 30. Technical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected final baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of WP09 files and relevant untracked governance artifacts.

Require:

- temporary process fixtures removed;
- database/WAL/SHM/journal residue: 0;
- generated residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 31. Architecture Validation

Confirm:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure;
- cycles: 0;
- unexpected edges: 0;
- package/reference delta: 0;
- schema delta: 0.

Architecture.Tests remain 13/13.

Do not add Architecture.Tests.

---

## 32. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch integration branch;
- push;
- create/merge PR;
- tag;
- release;
- begin WP10;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only exact manifest-authorized WP09 Worker paths.

---

## 33. Authorized GitHub Mutation Budget

At WP09 start after gates pass:

1. #176 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP09 completion-evidence comment to #176;
3. close #176 as completed;
4. set #176 Project Status to Done.

Do not mutate #177.

Milestone #46 remains OPEN.

---

## 34. Completion Gate

WP09 may close only if:

- #175 is Closed/Done;
- #176 was In Progress during execution;
- #177 remains Open/Backlog;
- exact manifest accounting passes;
- explicit Experiment configuration selects experiment mode;
- partial/malformed Experiment intent fails without fallback;
- valid experiment invokes use case exactly once;
- success exits `0`;
- bounded failure exits non-zero deterministically;
- unknown defects are not broadly normalized;
- semantic output contains sufficient immutable result evidence;
- empty/single/non-empty behavior is correct;
- equivalent second-process identity is stable;
- no Experiment Result identity is fabricated on failure;
- Release 1.3 pipeline path remains unchanged;
- Release 1.4 feature path remains unchanged;
- provider/network activity is zero;
- experiment persistence is absent;
- Application/Domain/Infrastructure/permanent-test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- warnings/errors 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue 0;
- Release 1.6 work 0.

If any gate fails, do not close #176 or mark Done.

---

## 35. Completion Evidence Comment

On success, post concise evidence to #176 covering:

- exact Worker files changed/added;
- final mode precedence;
- explicit Experiment selector/configuration behavior;
- one-shot use-case invocation;
- success/failure exit policy;
- semantic output fields;
- empty/single/non-empty process evidence;
- equivalent second-process Experiment Result identity;
- NotFound/unavailable behavior as tested;
- no fabricated identity on failures;
- unknown defect propagation;
- Release 1.3/1.4 mode preservation;
- no provider/network/persistence;
- zero Application/Domain/Infrastructure/test/package/reference/schema delta;
- SQLite v2;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #177 preserved Open/Backlog.

---

## 36. Final Read-Back

After successful closure verify:

- #176: CLOSED / Done;
- #177: OPEN / Backlog;
- #178–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 4 open / 9 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 artifacts accurately.

---

## 37. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #175 is not Closed/Done;
- #177+ started unexpectedly;
- WP09 manifest ownership is ambiguous;
- predecessor Worker mode behavior cannot be preserved;
- explicit experiment selection requires semantic changes outside WP09;
- satisfying WP09 requires Application/Infrastructure/schema/package/reference changes;
- provider acquisition is required;
- experiment persistence is required;
- broad unknown-exception normalization would be required;
- premature WP10+ implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail.

Report the smallest corrective authority required.

---

## 38. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP09 manifest paths;
6. existing Worker/mode inventory;
7. final mode precedence;
8. Experiment selector/configuration behavior;
9. partial/malformed configuration behavior;
10. one-shot invocation evidence;
11. success presentation;
12. empty/single/non-empty behavior;
13. bounded failure presentation;
14. exit-code policy;
15. unknown defect behavior;
16. equivalent second-process identity;
17. predecessor mode preservation;
18. provider/persistence/schema protection;
19. repository delta;
20. temporary process acceptance matrix;
21. permanent validation/test counts;
22. architecture/security/whitespace/residue;
23. GitHub lifecycle mutations;
24. final #176/#177/milestone state;
25. findings/blockers;
26. next authorized WP.

---

## 39. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP09 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Application Experiment Tests — GitHub issue #177`

Do not begin WP10.

If blocked, end:

`RELEASE 1.5 WP09 BLOCKED`

and identify the smallest corrective authority required.

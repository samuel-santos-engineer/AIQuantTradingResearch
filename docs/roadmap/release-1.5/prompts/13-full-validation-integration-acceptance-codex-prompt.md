# Release 1.5 WP13 — Full Validation, Integration & Acceptance

## GitHub Issue
`#180 — Release 1.5 WP13 — Full Validation, Integration & Acceptance`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP13 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Built-in experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP13 is the final Release 1.5 candidate reconciliation, technical/semantic acceptance, integration, reproducibility, and review-readiness work package.

It may create one integration branch, one integration commit, one normal push, and one non-draft review-ready pull request only after every mandatory pre-staging acceptance gate passes.

WP13 must not merge the PR, close milestone #46, tag, release, delete branches, or begin Release 1.6.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- all accepted Release 1.5 WP01–WP12 governance prompts and companions
- all accepted Release 1.5 production/test/documentation changes
- WP01–WP12 completion evidence
- Release 1.4 post-merge closure state
- predecessor Release 1.1–1.4 semantic, architecture, persistence, and testing authorities
- this WP13 authority and its five-line companion

Repository truth and accepted Release 1.5 authorities take precedence over assumptions.

If any mandatory pre-staging governance/candidate gate fails, stop before staging.

---

## 2. Objective

Prove that the exact Release 1.5 candidate is coherent, complete, deterministic, reproducible, secure, architecture-preserving, schema-v2-preserving, and ready for human review.

If and only if every pre-integration gate passes:

1. create one Release 1.5 integration branch;
2. stage exactly the reconciled governed candidate;
3. create exactly one integration commit over accepted `main`;
4. run post-commit validation;
5. validate the exact commit from a fresh detached checkout/worktree;
6. push normally without force;
7. create one non-draft PR to `main`;
8. post WP13 completion evidence;
9. close #180 and mark it Done.

Leave:

- PR unmerged;
- milestone #46 OPEN;
- Release 1.6 unstarted.

---

## 3. Expected Starting Baseline

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected accepted baseline SHA:
  `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`.

Expected lifecycle:

- #168–#179: CLOSED / Done;
- #180 WP13: OPEN / Backlog;
- milestone #46: OPEN with 1 open / 12 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected permanent technical baseline entering WP13:

- Domain.Tests: 11;
- Application.Tests: 102;
- Infrastructure.Tests: 112;
- Architecture.Tests: 13;
- total: 238;
- SQLite schema: v2.

If #179 is not Closed/Done, or Release 1.5 integration has already started unexpectedly, stop.

---

## 4. WP13 Lifecycle Start

After starting-state reconciliation passes:

- move only #180 Project #2 Status from Backlog to In Progress.

Read back the state.

If #180 is already In Progress solely because this exact WP13 execution partially started, continue idempotently if no unauthorized integration mutation occurred.

Milestone #46 must remain OPEN.

---

# PART A — MANDATORY PRE-STAGING RECONCILIATION

## 5. No Staging Before Reconciliation

This is a hard gate.

Before all sections in Part A pass, do not:

- stage;
- create integration branch;
- commit;
- push;
- create PR.

Do not use staging to discover candidate content.

Reconcile the candidate directly from working-tree/repository truth first.

---

## 6. Accepted Authority Inventory

Inventory all Release 1.5 governance/planning authority artifacts actually present.

Classify each as:

- governed candidate;
- accepted out-of-band execution input;
- corrective authority;
- unexpected.

At minimum reconcile:

### Planning artifacts
- `RELEASE_1.5_DEFINITION.md`
- `RELEASE_1.5_EXECUTION_PLAN.md`
- `RELEASE_1.5_FILE_MANIFEST.md`

### GitHub planning pair
- full prompt
- five-line companion

### WP execution pairs
- WP01 through WP13 full prompts
- WP01 through WP13 companions

### Historical planning-definition pair
Classify exactly according to the accepted manifest/current governance rules.

Do not silently include execution-only/out-of-band authority files in the final candidate.

Do not silently delete files without explicit authority.

---

## 7. Prompt-Pair Governance Gate

Enumerate every governed Release 1.5 full prompt and companion.

Require:

- exactly one companion per governed full prompt;
- missing companions: 0;
- orphan companions: 0;
- duplicate companions: 0;
- malformed companions: 0;
- every governed companion has exactly five non-empty logical lines;
- no trailing whitespace in governed prompt/companion files.

This gate must inspect untracked files directly; do not rely only on `git diff --check`.

If any governed companion violates the five-line contract, stop before staging and report the exact files and smallest corrective authority required.

---

## 8. Actual Prompt Filename Reconciliation

Compare:

- manifest-expected prompt names;
- actual accepted prompts executed for WP01–WP13;
- GitHub-planning authority names.

The actual executed accepted prompt names must be authoritative if they differ only by naming from the manifest and were the prompts under which the WPs were completed.

Do not:

- silently rename prompt files;
- duplicate prompt pairs under alternate names;
- fabricate missing aliases.

If manifest naming and accepted executed names conflict materially, stop before staging and request a narrow governance reconciliation authority.

The final candidate must contain one unambiguous governed prompt pair per authority.

---

## 9. File Manifest Reconciliation

Read `RELEASE_1.5_FILE_MANIFEST.md` completely.

Build the expected candidate path set from:

- planning artifacts;
- governed prompts/companions;
- semantic documentation;
- Application production;
- Worker production;
- Application tests;
- Infrastructure tests;
- Architecture tests if any;
- current-state documentation.

Compare to actual Release 1.5 working-tree changes/untracked governed files.

Require:

- missing governed paths: 0;
- unexpected governed paths: 0;
- duplicate logical artifacts: 0;
- unclassified candidate paths: 0;
- generated/database residue: 0.

Do not force exact predicted filenames when the manifest explicitly allows accepted same-area filename consolidation/reconciliation; use the manifest's filename reconciliation rule.

Record every actual candidate path.

---

## 10. Candidate Category Accounting

Classify every candidate path into one of:

- planning;
- governance prompt;
- semantic documentation;
- Application production;
- Worker production;
- Application test;
- Infrastructure test;
- Architecture test;
- current-state documentation.

Report exact counts.

Confirm expected structural intent:

- Domain production delta: 0;
- Infrastructure production delta: 0;
- package/project/reference delta: 0/0/0;
- schema delta: 0;
- Architecture test delta: 0 unless repository truth proves otherwise.

---

## 11. Out-of-Band / Corrective Authority Exclusion

Identify any authority files that were used to execute planning/corrections but are explicitly out-of-band under the Release 1.5 manifest.

They must not be staged into the final candidate unless a later accepted authority explicitly governs them in.

Do not remove them from the filesystem unless WP13 or an accepted corrective authority explicitly authorizes mechanical exclusion/removal.

If their mere presence prevents a clean post-commit tree and the manifest already authorizes their exclusion/removal, perform only the exact mechanical action after all pre-staging gates pass and before staging.

Record all such exclusions.

---

## 12. Direct Whitespace Gate

Before staging, scan all candidate paths directly for:

- trailing spaces;
- trailing tabs;
- malformed final newline where repository rules require one.

Run:

- `git diff --check`;
- `git diff --cached --check`.

But do not treat those as sufficient for untracked files.

Require direct candidate whitespace findings: 0 before staging.

If findings exist in governed Markdown or source files, use only the mechanical correction authority explicitly allowed by WP13/manifest. Preserve semantic equivalence and report exact corrections.

If correction authority is not explicit, stop.

---

## 13. Residue Gate

Before staging, search for and exclude/remove authorized transient residue:

- SQLite database files;
- WAL;
- SHM;
- journal;
- temporary worktrees;
- process fixtures;
- probe projects/scripts;
- build-generated files accidentally created outside ignored paths;
- temporary output files.

Require candidate residue: 0.

Do not delete unrelated user files.

---

# PART B — RELEASE 1.5 SEMANTIC ACCEPTANCE

## 14. Experiment Definition Acceptance

Verify exactly one Release 1.5 built-in experiment:

`simple-return-descriptive-summary-v1`

Confirm no additional experiment/statistics engine was introduced.

No plugin/generalized expression system.

---

## 15. Experiment Semantic Acceptance

Verify accepted behavior:

### Input
- exact Release 1.4 `simple-return-lag-1-v1` Feature Set evidence.

### Empty
- count 0;
- mean absent;
- minimum absent;
- maximum absent;
- success.

### Single
- count 1;
- mean/min/max equal exact input.

### Non-empty
- count exact;
- decimal mean exact;
- minimum exact;
- maximum exact.

No floating-point conversion.

No convenience rounding.

No filtering/deduplication.

---

## 16. Identity Acceptance

Verify:

- scheme `aiq-experiment-identity-v1`;
- SHA-256;
- 32-byte digest;
- 64 lowercase hexadecimal external form;
- distinct Experiment Definition and Experiment Result identities;
- canonical domains:
  - `experiment-definition`
  - `experiment-result`;
- deterministic BOM-free UTF-8 canonical representation;
- ordinal semantics;
- fixed field order;
- byte-length-delimited framing;
- deterministic count/presence encoding;
- Release 1.4-compatible decimal canonicalization.

---

## 17. Result Identity Binding

Verify Experiment Result Identity binds:

- Experiment Definition Identity;
- exact Feature Set Identity;
- count;
- aggregate presence;
- mean/minimum/maximum when present.

Confirm:

- equivalent reruns → same result identity;
- different Feature Set identities → different result identities even with equal summaries;
- empty results have deterministic identity;
- no global empty sentinel.

---

## 18. Provenance / Lineage Acceptance

Verify acyclic lineage remains:

`source state → dataset/research dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

Confirm:

- predecessor identities are referenced, not redefined;
- no downstream feedback cycle;
- provider/storage operational metadata excluded from semantic identity.

---

## 19. Evidence-Established-Only Acceptance

Verify:

- invalid request → no result identity;
- upstream NotFound/unavailable → no result identity;
- invalid Feature Set evidence → no result identity;
- numeric overflow → no result identity;
- integrity contradiction → no fabricated identity;
- successful empty result is valid semantic evidence.

---

## 20. Failure Acceptance

Reconcile exact implemented Release 1.5 failure vocabulary.

Verify deterministic validation precedence:

1. Invalid request
2. Unsupported experiment definition
3. Invalid Feature Set/predecessor evidence
4. Invalid numeric evidence
5. Feature Set identity integrity conflict

Confirm:

- decimal `OverflowException` maps only to governed numeric failure;
- bounded upstream NotFound preserved;
- DependencyUnavailable preserved;
- invalid upstream evidence preserved;
- integrity conflict preserved;
- unknown defects propagate;
- no catch-all normalization;
- fail-stop behavior.

---

## 21. Application Integration Acceptance

Verify actual WP07 orchestration:

1. request validation;
2. Release 1.4 feature generation exactly once;
3. upstream bounded failure mapping;
4. returned Feature Set validation;
5. summary computation exactly once;
6. canonical experiment identity/provenance;
7. immutable result.

Require:

- zero summary calls after earlier failure;
- no duplicate upstream invocation;
- no retry/fallback.

---

## 22. DI / Configuration Acceptance

Verify:

- exactly one effective transient `IExperimentGenerationUseCase`;
- exactly one effective transient `IExperimentSummaryComputer`;
- exactly one effective transient `IExperimentGenerationValidator`;
- existing Release 1.4 feature graph reused;
- side-effect-free resolution;
- no database creation during resolution;
- no provider/network execution during resolution.

Configuration:

- `Experiment:SnapshotIdentity`;
- `Experiment:SnapshotVersion`;
- built-in experiment code-owned;
- invalid/malformed/incoherent configuration fails before execution;
- culture-independent parsing.

---

## 23. Worker Acceptance

Verify mode precedence:

1. explicit Experiment intent;
2. otherwise Feature intent;
3. otherwise Release 1.3 pipeline.

Verify:

- partial/malformed Experiment intent does not fall back;
- one experiment invocation per process;
- success exit 0;
- bounded failure exit 1;
- unknown defects unhandled;
- semantic evidence output includes result identity/count/aggregates;
- empty aggregate absence explicit.

---

## 24. Process Reproducibility Acceptance

Use permanent WP11 tests and, where useful, a bounded manual offline process proof to confirm:

- non-empty success;
- equivalent second process same result identity;
- empty success;
- single-observation-derived empty success;
- malformed/partial configuration failure;
- exact NotFound;
- unavailable storage;
- no fabricated identity on failure;
- no provider/network fallback.

Do not add new permanent tests in WP13.

---

# PART C — PREDECESSOR / ARCHITECTURE / SCHEMA ACCEPTANCE

## 25. Release 1.1 Regression

Verify permanent coverage still passes for accepted historical-observation persistence semantics, including applicable:

- fidelity;
- ordering;
- idempotency/equivalence;
- conflicts;
- atomicity;
- isolation;
- retrieval;
- failure mapping;
- connection ownership.

Do not rewrite Release 1.1.

---

## 26. Release 1.2 Regression

Verify:

- dataset identities;
- immutable snapshots;
- exact lookup;
- provenance/lineage;
- schema-v2 migration behavior;
- catalog behavior;
- equivalence/conflict semantics;
- bounded execution.

Do not rewrite Release 1.2.

---

## 27. Release 1.3 Regression

Verify:

- fixed five-stage pipeline unchanged;
- sequential one-shot behavior;
- pipeline identity/evidence;
- fail-stop semantics;
- no experiment sixth stage;
- Worker pipeline mode preserved.

---

## 28. Release 1.4 Regression

Verify:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- exact snapshot/version binding;
- empty/single/non-empty feature behavior;
- feature validation/failure mapping;
- feature DI;
- feature Worker mode;
- no feature persistence.

Release 1.5 is downstream, not a redefinition.

---

## 29. Architecture Acceptance

Verify production graph exactly:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

Require:

- cycles: 0;
- unexpected edges: 0;
- Domain experiment delta: 0;
- Infrastructure experiment production delta: 0;
- Architecture.Tests: 13/13;
- Architecture.Tests delta: 0.

Do not add architecture rules in WP13.

---

## 30. Schema / Persistence Acceptance

Verify SQLite schema remains exactly version 2.

Confirm absence of:

- experiment tables;
- experiment registry;
- experiment history;
- experiment cache;
- experiment run history;
- feature persistence expansion;
- scheduler/checkpoint state.

No schema mutation.

---

## 31. Package / Project / Reference Acceptance

Verify:

- package delta: 0;
- project delta: 0;
- project-reference delta: 0;
- solution project count remains accepted baseline;
- SDK/global configuration unchanged unless already governed by predecessor history.

No package update in WP13.

---

# PART D — DOCUMENTATION / SECURITY / TEST ACCEPTANCE

## 32. Documentation Acceptance

Verify WP12 documentation accurately reflects:

- `simple-return-descriptive-summary-v1`;
- `aiq-experiment-identity-v1`;
- Application ownership;
- Feature Set → Experiment provenance;
- one-shot Experiment Worker mode;
- `Experiment:*` configuration;
- schema v2;
- no experiment persistence;
- no provider fallback;
- 238-test baseline entering WP13;
- Release 1.6+ deferrals.

Check manifest-authorized local Markdown links.

Require broken links: 0.

Check focused stale-current-state claims.

Require stale material claims: 0.

---

## 33. Permanent Test Acceptance

Run all permanent tests.

Expected baseline:

- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: 112/112;
- Architecture.Tests: 13/13;
- total: 238/238;
- skipped: 0.

Any unexplained mismatch blocks integration.

WP13 must not change permanent tests unless a strictly mechanical non-semantic correction is explicitly authorized; otherwise stop.

---

## 34. Canonical Verification

Run:

`eng/verify.ps1 -Configuration Release`

Require:

- restore PASS;
- formatting PASS;
- Gitleaks PASS;
- build PASS;
- warnings/errors 0/0;
- all permanent tests PASS.

Run before staging.

Run again after commit.

Run again in fresh checkout.

---

## 35. Security Acceptance

Require:

- Gitleaks PASS;
- real credentials: 0;
- provider/network calls during tests: 0;
- dummy credentials only in isolated tests;
- no secret-bearing output committed.

No live Twelve Data/provider access.

---

## 36. Whitespace Acceptance

Require before staging:

- direct candidate trailing-whitespace findings: 0;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS.

After staging:

- `git diff --cached --check`: PASS.

After commit/fresh checkout:

- repository formatting verification PASS.

---

# PART E — FINAL CANDIDATE DECISION

## 37. Pre-Staging Acceptance Matrix

Before any staging, produce an explicit PASS/FAIL matrix including at least:

- starting Git baseline;
- WP01–WP12 lifecycle;
- candidate path reconciliation;
- missing/unexpected/duplicate paths;
- prompt pairs;
- five-line companions;
- actual prompt filename reconciliation;
- out-of-band exclusion;
- whitespace;
- residue;
- Release 1.5 semantics;
- identity/provenance;
- failures;
- Application integration;
- DI/configuration;
- Worker;
- Releases 1.1–1.4 regressions;
- architecture;
- schema;
- packages/references;
- documentation;
- 238/238 tests;
- Gitleaks;
- Release 1.6 exclusion.

If any row fails, stop before staging.

---

## 38. Release 1.6 Exclusion Audit

Verify no Release 1.6 implementation or lifecycle work exists in the Release 1.5 candidate.

Do not treat pre-existing generic future milestone templates/scripts as Release 1.6 implementation merely because they mention a future release.

Classify semantically.

No Release 1.6 issue/branch/PR/implementation may be created by WP13.

---

# PART F — INTEGRATION

## 39. Integration Branch

Only after the entire pre-staging matrix passes, create:

`release/1.5-deterministic-research-experiment-foundation`

If that exact branch already exists due solely to a partial authorized WP13 run, reconcile its state idempotently.

Do not overwrite unrelated branch history.

Do not force reset remote branches.

---

## 40. Exact Staging

Stage exactly the reconciled governed Release 1.5 candidate.

Do not stage:

- out-of-band planning-definition execution inputs;
- corrective authorities excluded by governance;
- temporary probes;
- databases/residue;
- Release 1.6 artifacts;
- unrelated working-tree files.

After staging, enumerate staged paths and compare to the accepted candidate set.

Require exact equality.

Run:

`git diff --cached --check`

Require PASS.

---

## 41. Integration Commit

Create exactly one commit over accepted `main`.

Commit message:

`feat: establish Release 1.5 deterministic research experiment foundation`

Requirements:

- parent count: 1;
- commits over accepted main: 1;
- no merge commit;
- staged candidate exact;
- no unrelated history rewrite.

Record:

- commit SHA;
- parent SHA;
- tree SHA;
- file count;
- insertions/deletions.

---

## 42. Post-Commit Validation

After commit:

- working tree must be clean except explicitly retained non-candidate execution inputs if governance allows them;
- run canonical Release verification;
- all 238 tests pass;
- Architecture.Tests 13/13;
- Gitleaks PASS;
- formatting PASS;
- graph/schema/package/reference acceptance remains PASS.

If post-commit validation fails, do not push.

Report blocker.

---

## 43. Fresh-Checkout Reproducibility

Create a temporary detached checkout/worktree at the exact integration commit.

From that fresh state:

- run canonical Release verification;
- require 238/238 tests;
- require 13/13 Architecture.Tests;
- build warnings/errors 0/0;
- Gitleaks PASS;
- formatting PASS;
- verify schema v2;
- verify no generated/database residue;
- verify checkout clean after validation.

Remove temporary worktree afterward.

This proof is mandatory before push.

---

## 44. Accepted-Tree Reconciliation

Compare the integration commit tree against the staged/reconciled candidate.

Require:

- exact candidate represented;
- no unexpected paths;
- no missing paths;
- no out-of-band authority files;
- no temporary residue.

Record tree SHA.

---

## 45. Push

Push the integration branch normally.

Requirements:

- no force;
- local branch SHA == remote branch SHA after push;
- ahead/behind `0/0` relative to remote branch.

Do not push `main`.

---

## 46. Pull Request

Create one non-draft PR:

Base:

`main`

Head:

`release/1.5-deterministic-research-experiment-foundation`

Suggested title:

`Release 1.5 — Deterministic Research Experiment Foundation`

PR body must summarize:

- selected experiment;
- deterministic summary evidence;
- identity/provenance;
- Application integration;
- Worker mode;
- test counts;
- architecture/schema preservation;
- security/offline validation;
- fresh-checkout proof;
- explicit deferrals.

Do not enable auto-merge.

Do not merge.

---

## 47. PR Read-Back

Verify:

- PR state OPEN;
- draft false;
- base `main`;
- correct head branch;
- head SHA = exact validated integration commit;
- file count matches accepted candidate;
- one commit over main;
- merge state clean/mergeable if GitHub reports it;
- auto-merge disabled.

Hosted check count may be zero; report actual state rather than inventing checks.

---

# PART G — GITHUB LIFECYCLE COMPLETION

## 48. WP13 Completion Evidence

After integration branch, commit, fresh checkout, push, and PR read-back all pass:

- post concise completion evidence to #180;
- close #180 as completed;
- set #180 Project #2 Status to Done.

Do not close milestone #46.

Milestone must remain OPEN pending human merge and post-merge closure.

---

## 49. Final GitHub Read-Back

Verify:

- #168–#180: 13/13 Closed/Done;
- milestone #46: OPEN;
- open issues under milestone: 0;
- closed issues under milestone: 13;
- PR: OPEN / non-draft / unmerged;
- Release 1.6 work: 0.

Do not close milestone #46 merely because all WPs are closed.

---

## 50. Final Local Git State

Verify:

- current branch: Release 1.5 integration branch;
- local branch == remote branch;
- working tree clean;
- staged paths 0;
- untracked candidate paths 0;
- accepted execution-only exclusions handled as governed;
- no temporary worktree/residue remains.

---

## 51. Mutation Budget

Authorized WP13 Git/repository mutations after all gates pass:

- mechanical candidate cleanup only if explicitly authorized;
- one integration branch;
- one exact candidate staging;
- one integration commit;
- one normal push;
- one PR.

Authorized GitHub lifecycle mutations:

- #180 Backlog → In Progress;
- one completion-evidence comment;
- #180 close;
- #180 Done;
- one PR creation.

Not authorized:

- main push;
- force push;
- merge;
- milestone #46 closure;
- tag;
- release;
- branch deletion;
- Release 1.6 lifecycle.

---

## 52. Stop Conditions

Stop before staging if:

- candidate reconciliation fails;
- prompt-pair governance fails;
- any governed companion is not exactly five non-empty lines;
- actual executed prompt names cannot be reconciled with the manifest;
- out-of-band authority classification is ambiguous;
- unexpected paths exist;
- whitespace findings lack correction authority;
- residue cannot be safely classified/removed;
- semantic acceptance fails;
- predecessor regression fails;
- architecture/schema/package/reference acceptance fails;
- documentation acceptance fails;
- 238/238 tests do not pass;
- Gitleaks/formatting fails;
- Release 1.6 work is present.

Stop before push if:

- commit validation fails;
- fresh checkout fails;
- exact tree reconciliation fails.

Stop before #180 closure if:

- push/PR read-back fails.

Report the smallest corrective authority required.

Do not improvise.

---

## 53. Required Execution Report

Report at least:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. WP01–WP12 lifecycle;
5. candidate path reconciliation;
6. candidate category accounting;
7. governance prompt-pair inventory;
8. five-line companion validation;
9. actual prompt-name reconciliation;
10. out-of-band/corrective exclusions;
11. whitespace/residue findings;
12. Release 1.5 semantic acceptance;
13. identity/provenance/evidence acceptance;
14. Application/failure acceptance;
15. DI/configuration acceptance;
16. Worker/process acceptance;
17. Release 1.1 regression;
18. Release 1.2 regression;
19. Release 1.3 regression;
20. Release 1.4 regression;
21. architecture acceptance;
22. schema/persistence acceptance;
23. package/project/reference acceptance;
24. documentation acceptance;
25. permanent test counts;
26. canonical verification;
27. security/offline acceptance;
28. Release 1.6 exclusion audit;
29. pre-staging acceptance matrix;
30. integration branch;
31. exact staged candidate;
32. integration commit SHA/parent/tree/stats;
33. post-commit validation;
34. fresh-checkout proof;
35. push state;
36. PR state;
37. GitHub lifecycle mutation;
38. final repository state;
39. findings/blockers;
40. final decision;
41. next authorized lifecycle action.

---

## 54. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP13 COMPLETE`

Then:

`NEXT AUTHORIZED LIFECYCLE ACTION: Human review and explicit merge authorization for the Release 1.5 integration PR. The PR must remain unmerged and milestone #46 must remain open.`

If blocked, end:

`RELEASE 1.5 WP13 BLOCKED`

and identify the smallest corrective authority required.

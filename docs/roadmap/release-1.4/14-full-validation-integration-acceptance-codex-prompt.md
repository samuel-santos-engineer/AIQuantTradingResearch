# Release 1.4 --- WP14 Full Validation, Integration & Acceptance --- Codex Authority

## Mission

Execute **Release 1.4 --- WP14: Full Validation, Integration &
Acceptance --- GitHub issue #166**.

WP14 is the final Release 1.4 integration package. It must reconcile the
exact governed candidate, prove the complete Release 1.1--1.4 technical
and semantic acceptance surface, integrate the accepted candidate as
exactly one release commit, validate that exact commit including from a
fresh checkout/worktree, push the integration branch normally, and
create one review-ready pull request.

WP14 must **not merge the pull request**, close the Release 1.4
milestone, delete branches, tag, publish a release, or start Release
1.5.

Recommended model: **GPT-5.6 Sol**.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Before any mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  Release 1.4 planning-definition authority pair.
5.  Release 1.4 GitHub-planning authority pair.
6.  WP01--WP14 full authority/companion pairs.
7.  Any corrective/clarification authorities created during Release 1.4,
    including WP06 identity clarification and lifecycle reconciliation
    evidence where applicable.
8.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
9.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
10. Release 1.3 semantic authorities:
    -   `RESEARCH_PIPELINE_SEMANTICS.md`
    -   `PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
11. Release 1.3 definition/plan/manifest and post-merge closure
    evidence.
12. Release 1.2 definition/plan/manifest and accepted
    dataset/snapshot/catalog semantic authorities.
13. Release 1.1 accepted persistence/retrieval foundation and permanent
    tests.
14. Current production source.
15. Current permanent tests.
16. Current architecture documentation and rules.
17. Current README/current-state documentation.
18. Current Git/GitHub state.
19. Issue #166 and milestone #45.

Repository truth plus accepted authorities govern. Do not infer missing
authority.

------------------------------------------------------------------------

## 2. Mandatory Immediate-Stop Principle

WP14 is integration authority, not broad repair authority.

Before staging or integration mutation, stop immediately with:

`RELEASE 1.4 WP14 BLOCKED`

if any of these are true:

-   candidate paths do not reconcile exactly with the governed manifest;
-   excluded/out-of-band paths remain in the candidate;
-   a governed full prompt lacks exactly one companion;
-   any governed companion violates the required five-non-empty-line
    convention;
-   unexpected generated/database residue exists;
-   direct whitespace inspection finds governed untracked-file defects
    that cannot be corrected mechanically under this authority;
-   production semantics contradict accepted Release 1.4 authorities;
-   package/reference/schema drift exists;
-   predecessor lifecycle is incomplete;
-   #166 is not the sole open Release 1.4 WP issue;
-   integration would require Release 1.5 behavior.

Do not stage first and reconcile later.

------------------------------------------------------------------------

## 3. Starting Git / GitHub Gates

Verify and report before mutation:

-   authenticated repository is
    `samuel-santos-engineer/AIQuantTradingResearch`;
-   default branch is `main`;
-   local branch is `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   no integration branch already exists for this WP unless it is an
    exact resumable artifact from an authorized prior WP14 run;
-   no open Release 1.4 integration PR exists unless it is the exact
    authorized resumable PR;
-   Release 1.3 PR #152 is MERGED;
-   Release 1.3 milestone #54 is CLOSED;
-   Release 1.4 issues #153--#165 are Closed/Done;
-   issue #166 is OPEN / Backlog;
-   milestone #45 is OPEN and contains the expected Release 1.4 issue
    set;
-   Release 1.5 implementation/lifecycle has not started.

Only after all starting gates and candidate reconciliation pass may #166
move Backlog → In Progress.

------------------------------------------------------------------------

## 4. Candidate Reconciliation --- Hard Gate

Use `RELEASE_1.4_FILE_MANIFEST.md` as the hard candidate authority.

Enumerate all Release 1.4 working-tree paths and reconcile:

-   expected governed paths;
-   missing paths;
-   unexpected paths;
-   duplicates;
-   excluded/out-of-band planning-definition authority paths;
-   generated residue;
-   database/WAL/SHM/journal residue.

Classify candidate paths by:

-   production;
-   tests;
-   documentation/governance.

The candidate must be exact before staging.

Report exact counts.

Do not silently include out-of-band prompt files merely because they are
related to Release 1.4.

------------------------------------------------------------------------

## 5. Governance Prompt-Pair Reconciliation

Inventory every governed Release 1.4 full Codex prompt and companion.

Verify:

-   every full prompt has exactly one `-chat.md` companion;
-   every companion has exactly **five non-empty logical lines**;
-   no missing companion;
-   no duplicate companion;
-   no malformed companion;
-   corrective/clarification authorities are included only when governed
    by the manifest/accepted planning authority;
-   explicitly excluded planning-definition prompt pair is not
    accidentally integrated if the manifest excludes it.

Report:

-   full prompt count;
-   companion count;
-   valid five-line companion count;
-   missing;
-   duplicates;
-   malformed.

This is a mandatory pre-staging gate.

------------------------------------------------------------------------

## 6. Direct Whitespace Gate

Because much of the candidate may still be untracked, do not rely solely
on `git diff --check`.

Before staging:

-   directly inspect every governed candidate text file for trailing
    whitespace;
-   run `git diff --check`;
-   run `git diff --cached --check`.

If mechanical whitespace correction is explicitly permitted by the
Release 1.4 plan/WP14 authority, correct only exact whitespace defects
with semantic-equivalence proof.

Do not perform unrelated prose cleanup.

If correction is not clearly authorized, stop.

------------------------------------------------------------------------

## 7. Initial Canonical Baseline

Before integration mutation, run:

`eng/verify.ps1 -Configuration Release`

Expected accepted post-WP13 baseline:

-   Domain.Tests: `11`
-   Application.Tests: `86`
-   Infrastructure.Tests: `104`
-   Architecture.Tests: `13`
-   Permanent total: `214`
-   skipped: `0`
-   build warnings/errors: `0/0`.

Repository truth wins only if an accepted predecessor delta explains a
difference.

Also confirm Gitleaks, format verification, diff checks, cleanliness of
generated/database residue, and unchanged architecture graph.

------------------------------------------------------------------------

## 8. Release 1.4 Semantic Acceptance

Prove the candidate implements exactly one built-in deterministic
feature:

`simple-return-lag-1-v1`

Accepted formula semantics:

`r[i] = (p[i] / p[i-1]) - 1`

Confirm:

-   decimal-only arithmetic;
-   no binary floating-point conversion;
-   no convenience rounding;
-   adjacency from accepted immutable snapshot order only;
-   feature value belongs to current observation `i`;
-   timestamp and original offset of observation `i` are preserved;
-   empty snapshot succeeds with empty feature set;
-   single observation succeeds with empty feature set;
-   `N >= 2` yields exactly `N-1` ordered values;
-   invalid numeric evidence does not produce partial success;
-   equivalent accepted input evidence recomputes equivalent feature
    evidence.

No generalized feature engine may exist.

------------------------------------------------------------------------

## 9. Feature Identity / Provenance Acceptance

Confirm:

-   scheme `aiq-feature-identity-v1`;
-   distinct Feature Definition Identity and Feature Set Identity;
-   SHA-256;
-   exactly 64 lowercase hexadecimal fingerprint characters;
-   deterministic BOM-free UTF-8 canonical representation;
-   ordinal/fixed-field/length-delimited semantics as accepted;
-   canonical decimal representation;
-   timestamp encoding preserves accepted instant/offset semantics;
-   exact dataset snapshot identity/version binding;
-   deterministic empty Feature Set identity bound to exact snapshot;
-   equivalent recomputation yields same Feature Set Identity;
-   equal numeric output from different snapshots remains
    identity-distinct;
-   lineage is acyclic;
-   operational metadata is excluded from semantic identity;
-   contradictory canonical content under equal identity is an integrity
    contradiction.

Do not alter dataset or pipeline identity schemes.

------------------------------------------------------------------------

## 10. Application Boundary Acceptance

Confirm Application owns:

-   feature definition/model;
-   typed feature identities;
-   immutable feature evidence;
-   generation contracts;
-   canonical feature identity computation;
-   deterministic feature computation;
-   validation/failure mapping;
-   exact snapshot generation orchestration;
-   structured semantic result behavior.

Confirm:

-   Domain has no Release 1.4 feature implementation ownership;
-   Infrastructure does not own feature semantics;
-   Worker does not own feature computation semantics.

Production graph must remain unchanged.

------------------------------------------------------------------------

## 11. Exact Snapshot Integration Acceptance

Confirm Release 1.4 feature generation:

1.  validates request;
2.  performs exact `DatasetSnapshotIdentity` lookup using existing
    `IDatasetSnapshotStore`;
3.  preserves exact `DatasetVersion` binding;
4.  classifies NotFound distinctly;
5.  classifies dependency unavailable distinctly;
6.  rejects contradictory/invalid returned snapshot evidence;
7.  invokes deterministic computation exactly once on valid found
    evidence;
8.  produces immutable result/evidence;
9.  does not fall back to provider acquisition;
10. does not persist feature output.

------------------------------------------------------------------------

## 12. Failure Acceptance

Reconcile exact repository vocabulary and prove bounded distinctions for
applicable accepted failures:

-   invalid request;
-   unsupported definition;
-   snapshot NotFound;
-   dependency unavailable;
-   invalid snapshot/evidence;
-   invalid numeric input;
-   integrity conflict.

Confirm:

-   fail-stop behavior;
-   no fabricated downstream Feature Set Identity after failure;
-   no partial feature success after numeric failure;
-   unknown/unrelated exceptions propagate rather than being broadly
    normalized.

Do not invent new failure categories during WP14.

------------------------------------------------------------------------

## 13. DI / Configuration Acceptance

Confirm:

-   exactly one effective `IFeatureGenerationUseCase` registration;
-   `FeatureGenerationUseCase` implementation;
-   accepted transient lifetime;
-   exactly one effective `IFeatureComputer`;
-   `SimpleReturnFeatureComputer`;
-   accepted transient lifetime;
-   exactly one effective `IFeatureGenerationValidator`;
-   `FeatureGenerationValidator`;
-   accepted transient lifetime;
-   existing `IDatasetSnapshotStore` reused;
-   `Feature:SnapshotIdentity`;
-   `Feature:SnapshotVersion`;
-   no configurable formula;
-   no configurable lag;
-   no configurable rounding;
-   culture-independent configuration parsing;
-   production graph resolution has no feature-execution side effect;
-   resolution alone does not create the database.

------------------------------------------------------------------------

## 14. Worker Acceptance

Using permanent WP12 coverage plus focused reruns as appropriate,
confirm:

-   Feature configuration selects feature mode;
-   missing/malformed Feature configuration fails before feature
    execution;
-   feature mode invokes `IFeatureGenerationUseCase` exactly once per
    process;
-   non-empty success exits `0`;
-   equivalent second process exits `0` with identical Feature Set
    Identity;
-   empty snapshot exits `0` with count `0`;
-   single-observation snapshot exits `0` with count `0`;
-   exact NotFound exits non-zero;
-   dependency unavailable exits non-zero;
-   failures fabricate no Feature Set Identity;
-   process terminates;
-   no timer/loop/retry/scheduling;
-   no feature persistence/run history;
-   no provider/network fallback;
-   absence of Feature configuration preserves Release 1.3 pipeline
    mode.

------------------------------------------------------------------------

## 15. Release 1.3 Pipeline Protection

Confirm the Release 1.3 research pipeline remains exactly five stages:

1.  Historical observation retrieval
2.  Dataset materialization
3.  Immutable snapshot persistence
4.  Catalog registration
5.  Structured result/evidence

Feature generation is separate and is not stage six.

Confirm Release 1.3:

-   `aiq-pipeline-identity-v1`;
-   deterministic definition/execution identities;
-   equivalent-rerun semantic identity;
-   fail-stop first-failure behavior;
-   structured evidence prefixes;
-   one-shot Worker behavior;
-   no scheduling/retries/DAG/run-history persistence.

------------------------------------------------------------------------

## 16. Release 1.2 Protection

Confirm:

-   `aiq-dataset-identity-v1`;
-   immutable dataset/snapshot semantics;
-   exact snapshot/version identity;
-   source state/provenance/lineage;
-   catalog lookup;
-   equivalence and integrity-conflict behavior;
-   empty snapshot semantics;
-   SQLite schema v2;
-   no destructive overwrite.

All Release 1.2 permanent regression tests must pass.

------------------------------------------------------------------------

## 17. Release 1.1 Protection

Confirm permanent coverage remains passing for accepted
historical-observation behavior, including applicable:

-   persistence/retrieval;
-   target/time-bound fidelity;
-   timestamp offset fidelity;
-   decimal fidelity;
-   deterministic ordering;
-   idempotency/equivalence;
-   conflicts;
-   atomicity/isolation;
-   failure mapping;
-   configuration/connection ownership.

No provider/network execution is required for WP14 acceptance.

------------------------------------------------------------------------

## 18. Schema / Persistence Acceptance

Inspect schema and migrations directly.

Confirm SQLite is exactly schema version `2`.

Confirm absence of Release 1.4 persistence additions:

-   feature table;
-   feature catalog;
-   feature cache;
-   feature run history;
-   feature execution state;
-   scheduler table;
-   checkpoint table;
-   pipeline run-history/state table.

Package/reference/schema delta must be:

`0 / 0 / 0`

relative to accepted main baseline.

------------------------------------------------------------------------

## 19. Architecture Acceptance

Confirm Architecture.Tests pass and production graph remains:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Confirm:

-   unexpected production edges `0`;
-   cycles `0`;
-   provider/HTTP confinement preserved;
-   Application contract/semantic ownership preserved;
-   WP13's justified zero architecture-test delta remains coherent.

Expected Architecture.Tests: `13/13`.

------------------------------------------------------------------------

## 20. Documentation Acceptance

Reconcile WP13-modified current-state documentation.

Confirm documentation accurately represents:

-   Release 1.1 persistence foundation;
-   Release 1.2 immutable dataset/snapshot/catalog foundation;
-   Release 1.3 fixed five-stage pipeline;
-   Release 1.4 deterministic feature-engineering foundation;
-   `simple-return-lag-1-v1`;
-   `aiq-feature-identity-v1`;
-   Application ownership;
-   exact snapshot lookup;
-   separate feature use case;
-   one-shot Worker mode;
-   schema v2;
-   no feature persistence;
-   no live acquisition in feature generation;
-   current test counts.

Validate local links in all modified Release 1.4 documentation.

Audit stale claims.

------------------------------------------------------------------------

## 21. Release 1.5 Exclusion Audit

Search production, tests, docs, schema, and governance candidate for
accidental implementation of deferred capabilities.

No Release 1.5 behavior may be integrated, including:

-   feature persistence/catalog/cache;
-   multiple indicators/features;
-   configurable formulas/lags;
-   plugins;
-   configurable DAGs;
-   acquisition orchestration;
-   scheduling;
-   retries/resilience automation;
-   durable feature/pipeline histories;
-   notebooks/workspaces;
-   strategies;
-   backtesting;
-   ML;
-   MLOps.

Documentation may mention them only as deferred future work.

Report open Release 1.5 issue/lifecycle state if any exists; WP14 must
not mutate it.

------------------------------------------------------------------------

## 22. Security / Offline Acceptance

Confirm:

-   Gitleaks PASS;
-   no real credentials;
-   no secret-bearing generated files;
-   no provider calls;
-   no live HTTP calls;
-   no network-dependent permanent tests;
-   Worker/process tests use only dummy/non-production credential values
    where required;
-   no sensitive machine-specific path is added to committed
    documentation/code.

GitHub API activity for lifecycle/PR management is governance activity
and is allowed.

------------------------------------------------------------------------

## 23. Candidate Staging Gate

Only after Sections 3--22 pass may the exact governed candidate be
staged.

Stage exactly the Release 1.4 governed candidate.

Then immediately verify:

-   staged path count equals governed candidate count;
-   staged paths exactly equal manifest-authorized candidate;
-   missing staged paths `0`;
-   unexpected staged paths `0`;
-   `git diff --cached --check` PASS;
-   no excluded/out-of-band path staged.

If mismatch occurs, unstage safely and stop.

------------------------------------------------------------------------

## 24. Integration Branch

Create exactly one Release 1.4 integration branch only after candidate
acceptance.

Preferred branch name:

`release/1.4-deterministic-feature-engineering-foundation`

If the execution plan specifies an exact different name, use the plan.

Do not create multiple branches.

Do not delete any branch.

------------------------------------------------------------------------

## 25. Integration Commit

Create exactly **one** Release 1.4 integration commit over accepted
`main`.

Preferred commit message:

`feat: establish Release 1.4 deterministic feature engineering foundation`

If the execution plan specifies exact wording, follow it.

After commit report:

-   commit SHA;
-   parent SHA;
-   parent count;
-   commits over main;
-   file count;
-   insertions/deletions;
-   commit message.

Required:

-   parent count `1`;
-   commits over main `1`.

Do not amend/rewrite unless correcting the still-unpushed WP14
integration commit is explicitly required by a failed mechanical gate
and safe under this authority.

------------------------------------------------------------------------

## 26. Post-Commit Validation

On the integration branch at the exact commit, rerun:

-   canonical `eng/verify.ps1 -Configuration Release`;
-   complete permanent tests;
-   Architecture.Tests;
-   Gitleaks;
-   formatting;
-   diff/cleanliness checks;
-   database/generated residue audit;
-   schema inspection;
-   Release 1.5 exclusion audit as necessary.

Working tree must be clean.

Expected final test baseline unless WP14 authority itself exposes a
governed predecessor difference:

-   Domain `11`;
-   Application `86`;
-   Infrastructure `104`;
-   Architecture `13`;
-   total `214`;
-   skipped `0`.

WP14 should not add tests.

------------------------------------------------------------------------

## 27. Fresh-Checkout / Fresh-Worktree Proof

Validate the **exact integration commit** from a fresh detached
checkout/worktree.

Do not validate a different SHA.

From the fresh environment prove:

-   restore PASS;
-   format PASS;
-   build PASS;
-   warnings/errors `0/0`;
-   Domain.Tests PASS;
-   Application.Tests PASS;
-   Infrastructure.Tests PASS;
-   Architecture.Tests PASS;
-   permanent total `214/214` unless accepted count differs;
-   Gitleaks PASS;
-   canonical verification PASS;
-   database/generated residue `0`;
-   checkout remains clean after validation.

Remove the temporary worktree/check-out validation residue afterward.

Do not remove the integration branch.

------------------------------------------------------------------------

## 28. Push

Only after post-commit and fresh-checkout acceptance pass:

-   push the integration branch normally;
-   no force push;
-   verify local branch SHA == remote branch SHA;
-   verify ahead/behind `0/0`.

Do not push directly to `main`.

------------------------------------------------------------------------

## 29. Pull Request

Create exactly one non-draft Release 1.4 integration PR.

Base:

`main`

Head:

the Release 1.4 integration branch.

Preferred title:

`Release 1.4 — Deterministic Feature Engineering Foundation`

PR body should concisely include:

-   release purpose;
-   built-in `simple-return-lag-1-v1`;
-   deterministic feature identities/provenance;
-   exact snapshot integration;
-   one-shot Worker mode;
-   schema v2 unchanged;
-   no feature persistence/provider acquisition/scheduling/retries;
-   permanent test result;
-   canonical verification;
-   fresh-checkout proof;
-   predecessor regression result;
-   review/merge note.

Do not auto-merge.

Do not merge.

Read back and verify:

-   PR OPEN;
-   non-draft;
-   correct base/head;
-   correct integration SHA;
-   exactly one Release 1.4 commit over main;
-   expected candidate file count;
-   merge state acceptable/clean if GitHub exposes it;
-   auto-merge disabled.

Hosted checks may be absent; report truthfully.

------------------------------------------------------------------------

## 30. GitHub Issue #166 Completion

Only after:

-   candidate acceptance;
-   integration commit;
-   post-commit validation;
-   fresh-checkout validation;
-   successful push;
-   review-ready PR creation/read-back

may #166 receive completion evidence and close.

Then:

-   close #166;
-   set Project #2 Status to Done;
-   read back #166 CLOSED / Done;
-   verify #153--#166 are `14/14 Closed/Done`;
-   leave milestone #45 OPEN for human review/post-merge closure.

Do not close milestone #45 in WP14.

------------------------------------------------------------------------

## 31. Human Merge Boundary

WP14 ends with an **open, review-ready, unmerged PR**.

The next lifecycle action must be human review and explicit merge
authorization.

WP14 must not:

-   merge;
-   enable auto-merge;
-   close milestone #45;
-   delete integration branch;
-   tag;
-   create GitHub Release;
-   start Release 1.5.

------------------------------------------------------------------------

## 32. Mutation Accounting

At completion, report exact authorized mutations.

Expected repository/Git mutations:

-   one integration branch;
-   one integration commit;
-   one normal push;
-   one PR.

Expected GitHub lifecycle mutations:

-   #166 Backlog → In Progress → Done/Closed;
-   completion evidence comment;
-   PR creation.

No other issue/milestone lifecycle mutation.

Report any deviation.

------------------------------------------------------------------------

## 33. Stop Conditions After Integration Begins

If a gate fails after staging/commit creation:

-   preserve evidence;
-   do not hide the failure;
-   do not push a failing commit;
-   do not create a PR for a failing candidate;
-   use only mechanical correction authority explicitly granted by this
    WP14 authority/plan;
-   if semantic/production correction is needed, stop with
    `RELEASE 1.4 WP14 BLOCKED`.

If the integration commit was created but cannot pass within authorized
mechanical corrections, do not rewrite accepted semantics. Report the
smallest corrective authority required.

------------------------------------------------------------------------

## 34. Required Acceptance Matrix

Report PASS/FAIL/NOT REACHED for at least:

1.  starting Git state;
2.  Release 1.3 closure;
3.  Release 1.4 WP01--WP13 lifecycle;
4.  manifest reconciliation;
5.  unexpected candidate paths = 0;
6.  missing candidate paths = 0;
7.  duplicate candidate paths = 0;
8.  governance full/companion reconciliation;
9.  all governed companions exactly five non-empty lines;
10. direct trailing whitespace = 0;
11. initial canonical verification;
12. build warnings/errors = 0/0;
13. permanent tests;
14. architecture tests;
15. feature formula semantics;
16. feature identity/provenance;
17. empty/single semantics;
18. exact snapshot integration;
19. bounded failure mapping;
20. unknown exception propagation;
21. DI/configuration;
22. Worker non-empty success;
23. equivalent separate-process identity;
24. Worker empty success;
25. Worker single-observation success;
26. Worker NotFound;
27. Worker dependency unavailable;
28. Release 1.3 five-stage pipeline protection;
29. Release 1.2 regression;
30. Release 1.1 regression;
31. schema v2;
32. feature persistence absence;
33. provider/network isolation;
34. Release 1.5 exclusion;
35. documentation/local links;
36. Gitleaks/security;
37. staged candidate exactness;
38. one integration commit;
39. post-commit validation;
40. fresh-checkout reproducibility;
41. clean integration worktree;
42. push synchronization;
43. PR correctness;
44. #166 Closed/Done;
45. milestone #45 remains OPEN.

------------------------------------------------------------------------

## 35. Required Execution Report

Report at least:

1.  executive summary;
2.  authorities reviewed;
3.  initial repository/Git state;
4.  Release 1.3 closure;
5.  WP01--WP13 lifecycle;
6.  candidate reconciliation;
7.  candidate path accounting;
8.  governance prompt-pair reconciliation;
9.  direct whitespace gate;
10. initial canonical baseline;
11. Release 1.4 feature semantic acceptance;
12. feature identity/provenance/evidence acceptance;
13. Application ownership/boundary acceptance;
14. exact snapshot integration;
15. failure acceptance;
16. DI/configuration acceptance;
17. Worker acceptance;
18. Release 1.3 pipeline protection;
19. Release 1.2 regression;
20. Release 1.1 regression;
21. schema/persistence acceptance;
22. architecture acceptance;
23. documentation acceptance;
24. Release 1.5 exclusion audit;
25. security/offline acceptance;
26. permanent test counts;
27. candidate staging reconciliation;
28. integration branch;
29. integration commit SHA/parent/count/message/stats;
30. post-commit validation;
31. fresh-checkout/worktree proof;
32. cleanup proof;
33. push state;
34. PR number/title/state/base/head/SHA/commit/file counts/merge state;
35. hosted-check state;
36. #166 lifecycle;
37. milestone #45 state;
38. mutation accounting;
39. final repository state;
40. findings/blockers;
41. full acceptance matrix;
42. final decision;
43. next authorized lifecycle action.

On success end exactly with:

`RELEASE 1.4 WP14 COMPLETE`

Then:

`NEXT AUTHORIZED LIFECYCLE ACTION: Human review and explicit merge authorization for the Release 1.4 integration PR. The PR must remain unmerged and milestone #45 must remain open.`

Do not merge the PR.

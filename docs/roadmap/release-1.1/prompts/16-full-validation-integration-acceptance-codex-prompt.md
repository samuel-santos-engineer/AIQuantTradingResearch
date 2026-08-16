# Release 1.1 WP16 — Full Validation, Integration & Acceptance — Codex Execution Authority

## 1. Authority

You are executing **Release 1.1 — WP16: Full Validation, Integration & Acceptance** for:

`samuel-santos-engineer/AIQuantTradingResearch`

This file is the authoritative WP16 execution contract.

Execute conservatively from repository, Git, GitHub, execution-plan, and file-manifest truth. Do not infer authorization beyond this contract.

Accepted lifecycle state:

- Release 1.0 — CLOSED
- Release 1.1 GitHub planning — COMPLETE
- Release 1.1 governance baseline — integrated into `main`
- WP01–WP15 — COMPLETE / Closed / Done
- WP16 — CURRENT
- Release 1.1 post-merge closure — NOT AUTHORIZED
- Release 1.2 planning/implementation — NOT AUTHORIZED

GitHub identity:

- Milestone: `#52 — Phase 3 - Release 1.1: Market Data Persistence Foundation`
- WP16 issue: `#118 — Full Validation, Integration & Acceptance`
- Required predecessor: `#117`
- Expected initial WP16 state: Open / Backlog

The following remain governing authorities:

1. `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3. accepted WP01–WP15 results;
4. accepted Release 1.1 governance baseline already represented on `main`;
5. current repository and GitHub truth.

If exact counts, paths, signatures, or lifecycle facts differ from assumptions in this prompt, reconcile against the execution plan, file manifest, accepted predecessor evidence, and repository truth. Never falsify a gate merely to reach acceptance.

## 2. Mission

WP16 must transform the accepted cumulative WP01–WP15 working tree into one **fully reconciled, validated, review-ready Release 1.1 integration candidate**.

WP16 owns:

- complete candidate discovery;
- exact file-manifest reconciliation;
- cumulative scope classification;
- full technical validation;
- permanent-test reconciliation;
- architecture validation;
- documentation validation;
- persistence-semantic validation;
- configuration/security validation;
- whitespace validation;
- candidate cleanliness;
- fresh-checkout reproducibility after integration commit;
- one governed integration branch;
- one governed integration commit unless repository truth requires a strictly justified alternative;
- push of that branch;
- one review-ready pull request;
- final WP16 acceptance evidence;
- issue #118 lifecycle completion only after all WP16 acceptance gates pass.

WP16 does **not** own:

- merging its pull request;
- closing milestone #52;
- post-merge synchronization of `main`;
- Release 1.1 final closure;
- creating a Release 1.1 tag;
- creating a GitHub Release;
- Release 1.2 planning or implementation.

A successful WP16 ends with a validated candidate and explicit **human merge authorization required**.

## 3. Non-Negotiable Protection

Do not:

- rewrite accepted WP01–WP15 behavior;
- redesign persistence;
- add features;
- opportunistically refactor;
- change public contracts without a demonstrated acceptance blocker and separate authority;
- change SQLite schema/semantics merely for cleanup;
- add packages without separate corrective authority;
- add project references without separate corrective authority;
- change GitHub Project schema;
- alter unrelated milestones/issues;
- force push;
- rebase or rewrite accepted history;
- merge the WP16 PR;
- close milestone #52;
- start Release 1.2;
- create a tag or GitHub Release.

WP16 is validation/integration/acceptance, not another implementation work package.

## 4. Mandatory Inputs

Before mutation, read completely:

- Release 1.1 execution plan;
- Release 1.1 file manifest;
- WP01–WP15 authoritative prompts/results available in repository/current execution context;
- WP02 persistence decision artifacts;
- current Domain/Application/Infrastructure/Worker source;
- current permanent tests;
- current architecture documents aligned by WP15;
- current package and project-reference configuration;
- engineering verification scripts;
- Git/GitHub integration conventions already established by Release 1.0;
- issue #118;
- milestone #52;
- issues #103–#118;
- current Project #2 fields/status;
- current branches and open PRs relevant to Release 1.1.

The file manifest is the path-level scope authority.

## 5. Starting-State Gate

Before any WP16 mutation, verify:

### Git

- repository identity is correct;
- branch is `main`;
- `main` equals `origin/main`;
- ahead/behind is `0/0`;
- staged paths: `0`;
- no merge/rebase/cherry-pick/revert operation is active;
- no unexpected tracked/untracked path exists.

The expected working tree contains the accepted cumulative WP01–WP15 candidate plus governance prompt pairs that the manifest recognizes or explicitly classifies.

Do not assume every untracked file belongs in the candidate.

### GitHub

Verify:

- #103–#117 are Closed/Done;
- #118 is Open/Backlog;
- milestone #52 is Open;
- #118 is the only remaining substantive Release 1.1 WP issue;
- Release 1.2 active planning = `0`;
- no competing Release 1.1 integration PR exists;
- no Release 1.1 implementation branch already represents the same candidate unless the execution plan explicitly expects reuse.

If starting-state drift cannot be reconciled without unauthorized mutation, stop.

## 6. Candidate Discovery and Classification

Inventory **every** path that differs from accepted `main`.

Classify each as exactly one of:

1. Release 1.1 governed production;
2. Release 1.1 governed permanent test;
3. Release 1.1 governed current-state documentation;
4. Release 1.1 governed decision/research artifact;
5. Release 1.1 governed Codex authority/prompt pair intended by the file manifest;
6. accepted governance-baseline file already committed on `main`;
7. out-of-band execution authority that must not enter the candidate;
8. temporary/generated artifact that must be removed;
9. unexpected/unclassified.

Required before integration:

- category 9 = `0`;
- temporary/generated residue = `0`;
- out-of-band authorities excluded;
- every candidate path justified by the file manifest.

Do not delete unrelated user work. If an unexpected path could be user work, stop and report it.

## 7. Exact File-Manifest Reconciliation

Use `RELEASE_1.1_FILE_MANIFEST.md` as the authoritative candidate definition.

Construct:

- expected Release 1.1 candidate paths;
- actual candidate paths;
- missing paths;
- unexpected paths;
- duplicate logical artifacts;
- manifest paths already present on accepted `main`;
- paths introduced by WP01–WP15;
- paths intentionally out-of-band.

Required:

```text
Missing governed candidate paths: 0
Unexpected governed candidate paths: 0
Duplicate governed paths: 0
```

Do not hard-code a candidate file count from this prompt. Derive the exact count from the manifest and report it.

If the manifest and accepted implementation cannot be reconciled exactly, stop before staging.

## 8. Governance Prompt-Pair Validation

For every governed Codex authority included by the manifest:

- authoritative `*-codex-prompt.md` exists;
- companion `*-codex-prompt-chat.md` exists in the same repository folder;
- companion naming is exact;
- companion is exactly 5 lines where the Release 1.1 convention requires the standard bootstrap;
- companion points to the authoritative prompt rather than duplicating its contract.

Do not include the current WP16 execution authority pair unless the Release 1.1 file manifest explicitly defines it as part of the governed candidate.

If this WP16 pair is execution input/out-of-band under the manifest, keep/remove its repository copy from candidate staging exactly as prior accepted workflow requires, while preserving the authority outside Git.

## 9. Accepted Functional Truth

The final candidate must preserve the accepted WP01–WP15 behavior.

### Application persistence boundary

Preserve:

- provider/storage independence;
- `IHistoricalObservationStore`;
- persistence outcomes:
  - `NewlyAccepted`;
  - `Idempotent`;
  - `Conflict`;
- failures:
  - `Unavailable`;
  - `InvalidData`;
- successful non-null empty historical retrieval;
- dedicated persistence use-case separation from acquisition.

### SQLite physical model

Preserve repository truth established by WP06:

- `historical_observations`;
- schema version 1;
- strict schema;
- without row ID;
- exact opaque target;
- UTC-tick semantic identity/order;
- original offset preservation;
- invariant decimal text;
- composite identity `(target, instant_utc_ticks)`.

### Connection/bootstrap

Preserve:

- `Persistence:DatabasePath`;
- no hidden production path;
- no hidden in-memory fallback;
- fresh open operation-owned connections;
- deterministic disposal;
- non-destructive version-aware bootstrap.

### Write semantics

Preserve:

- new acceptance;
- equivalent duplicate idempotency;
- deterministic conflict;
- immutable history;
- atomic batch behavior;
- no destructive replacement.

### Retrieval

Preserve:

- exact target;
- parameterized SQL;
- explicit ascending semantic-instant order;
- timestamp/offset/decimal fidelity;
- successful empty result.

### Failure mapping

Preserve:

- accepted WP04 vocabulary only;
- covered operational storage failures → `Unavailable`;
- malformed/incompatible stored data → `InvalidData`;
- conflict remains semantic;
- no invented retry policy.

### Composition and Worker

Preserve repository truth from WP11/WP12:

- persistence implementation registered through existing DI architecture;
- exact persistence configuration handoff;
- Release 1.0 provider composition remains intact;
- Worker performs only the bounded persistent market-data execution accepted by WP12;
- no future pipeline/scheduling/resilience scope is introduced.

## 10. Permanent Test Baseline

Accepted WP15 predecessor baseline:

| Suite | Tests |
| --- | ---: |
| Domain.Tests | 11 |
| Application.Tests | 42 |
| Infrastructure.Tests | 79 |
| Architecture.Tests | 13 |
| Total | 145 |

WP16 should not add behavioral tests merely to increase counts.

Before integration, run all permanent suites and reconcile actual counts.

Required:

- failures: 0;
- skipped tests: 0 unless pre-existing accepted repository truth explicitly allows otherwise;
- architecture tests: all pass.

If counts differ, explain the exact authorized delta. Do not silently change the expected total.

## 11. Initial Full Validation

Before staging or issue progression, run from the cumulative candidate working tree:

1. `dotnet restore AIQuantTradingResearch.slnx --nologo`
2. repository format verification using the canonical repository command/convention;
3. solution build;
4. all permanent test suites;
5. `eng/verify.ps1`;
6. `git diff --check`;
7. `git diff --cached --check`.

Also perform the repository's established package vulnerability/security audit if available/used by accepted Release 1.1 work.

Required:

- restore PASS;
- format PASS;
- build warnings 0;
- build errors 0;
- permanent tests 145/145 unless truthfully reconciled otherwise;
- canonical verification PASS;
- diff checks PASS;
- no unresolved package vulnerability blocker.

Only after this baseline passes may #118 move to `In Progress`.

## 12. Architecture Acceptance

Prove the production dependency graph remains:

```text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Required:

- cycles: 0;
- Architecture.Tests: all pass;
- Domain SQLite references: 0;
- Application SQLite references: 0;
- Domain provider-HTTP mechanics: 0;
- Application provider-HTTP mechanics: 0;
- Infrastructure owns Twelve Data transport implementation;
- Infrastructure owns SQLite implementation;
- Worker Infrastructure references remain limited to authorized composition/configuration/runtime handoff.

Do not invent a stricter rule during WP16.

## 13. Documentation Acceptance

Validate every WP15-aligned manifest-authorized document.

Required:

- documented dependency graph matches source;
- storage ownership matches source;
- Application contracts match source;
- schema descriptions match source where documented;
- configuration keys match source;
- Worker behavior matches source;
- testing responsibilities match permanent tests;
- implemented/planned distinctions remain truthful;
- repository-relative Markdown links pass the established audit;
- cross-document contradictions: 0.

Do not rewrite documentation during WP16 except for a narrowly demonstrable acceptance defect that is already within the manifest and requires no semantic redesign. If substantial documentation correction is required, stop and request corrective authority.

## 14. Persistence Acceptance Matrix

Perform focused offline acceptance sufficient to prove the integrated candidate still supports:

| Scenario | Required |
| --- | --- |
| First schema bootstrap | PASS |
| Repeated bootstrap | PASS |
| Schema version 1 | PASS |
| Exact schema identity | PASS |
| Fresh connection ownership/disposal | PASS |
| New observation persistence | PASS |
| Multiple observation persistence | PASS |
| Equivalent duplicate idempotency | PASS |
| Conflicting duplicate detection | PASS |
| Conflict non-destructive | PASS |
| Atomic rollback | PASS |
| Immutable accepted history | PASS |
| Empty retrieval | PASS |
| Exact target isolation | PASS |
| Ascending retrieval | PASS |
| Timestamp/offset fidelity | PASS |
| Decimal fidelity | PASS |
| Storage unavailable mapping | PASS |
| Malformed stored data mapping | PASS |
| DI/configuration resolution | PASS |
| DI resolution creates DB | NO |
| Provider/network calls for offline persistence proof | 0 |
| Temporary SQLite residue | 0 |

Prefer permanent WP13/WP14 tests as evidence. Do not create redundant permanent tests if existing tests already prove a row.

Temporary probes are allowed only when essential to an unproven WP16 acceptance gate, must be isolated/offline, and must be completely removed.

## 15. Worker Closure Validation

Validate the accepted WP12 Worker behavior without making live provider calls.

At minimum prove:

- required configuration is explicit;
- missing mandatory configuration fails deterministically;
- no provider call occurs when required startup/configuration validation fails;
- persistence configuration is not silently defaulted;
- Worker does not expose SQLite mechanics into Application/Domain;
- no Release 1.2 pipeline behavior exists.

Use the exact current WP12 contract and repository truth. Do not invent a new Worker acceptance behavior.

## 16. Security Acceptance

Verify:

- no credentials committed;
- no real API keys committed;
- no machine-specific database paths committed;
- no connection secrets committed;
- no temporary database files committed;
- no sensitive values logged by new Release 1.1 code;
- provider/network calls during offline validation: 0 unless an explicitly accepted existing test uses a deterministic mock boundary rather than live network;
- package vulnerability audit has no unresolved Release 1.1 blocker.

Never print credentials in the report.

## 17. Whitespace and Line-Ending Authority

Run:

- `git diff --check`;
- `git diff --cached --check`.

If whitespace findings occur in governed Release 1.1 candidate files, WP16 is authorized to correct **zero or more** such findings without creating a recursive authority chain, provided all conditions below hold:

1. correction is limited to whitespace actually reported by Git checks;
2. semantic content is unchanged;
3. no unrelated file is normalized;
4. no broad line-ending rewrite is performed;
5. corrected files remain within the manifest-authorized candidate;
6. after correction, both diff checks pass;
7. the report identifies the exact files and number/type of findings corrected.

If semantic equivalence is not clear, stop.

Benign LF/CRLF working-copy notices alone do not authorize normalization.

## 18. Candidate Staging Gate

Only after candidate discovery, manifest reconciliation, and initial validation pass:

- stage exactly the governed Release 1.1 candidate paths not already on accepted `main`;
- stage no out-of-band authority;
- stage no temporary artifact;
- stage no unrelated file.

Then verify:

```text
Missing staged governed paths: 0
Unexpected staged paths: 0
Unstaged governed candidate changes: 0
Untracked governed candidate paths: 0
```

Run `git diff --cached --check`.

Review the complete staged diff.

The staged candidate must exactly represent the Release 1.1 manifest delta from accepted `main`.

## 19. Integration Branch

Create exactly one integration branch from the accepted `main` base after all pre-branch gates pass.

Preferred branch name:

`release/1.1-market-data-persistence-foundation`

If the execution plan or established repository convention specifies another exact branch name, use that authority instead and report it.

Before creating it:

- confirm no conflicting remote/local branch represents a different candidate;
- do not overwrite an existing branch;
- do not force-reset a branch.

If a same-name branch exists unexpectedly, stop and reconcile.

## 20. Integration Commit

Create one governed integration commit containing exactly the staged Release 1.1 candidate.

Preferred commit message:

`feat: establish Release 1.1 market data persistence foundation`

Use the repository's established convention if the execution plan specifies another exact message.

Required:

- parent is the accepted `main` base used for integration;
- commit contains no unexpected path;
- no out-of-band execution authority is included;
- no temporary artifact is included;
- commit diff passes whitespace checks.

Report:

- commit SHA;
- parent SHA;
- tree SHA if useful for later merge verification;
- file count;
- additions/deletions;
- exact governed candidate count.

Do not create multiple cleanup commits merely to separate documentation/tests/implementation. WP01–WP15 form one accepted Release 1.1 candidate unless repository truth requires otherwise.

## 21. Post-Commit Validation

After commit, from the integration branch run again:

1. restore;
2. format verification;
3. build;
4. all permanent tests;
5. `eng/verify.ps1`;
6. committed diff whitespace validation;
7. architecture validation;
8. security/package audit as applicable.

Required:

- same accepted test baseline;
- build warnings/errors 0/0;
- working tree clean;
- no generated database residue.

If post-commit validation fails, do not push a knowingly invalid candidate.

Do not rewrite the commit unless the failure is a strictly mechanical WP16-authorized correction and doing so does not conceal prior evidence. Otherwise stop.

## 22. Fresh-Checkout Reproducibility

Create an isolated temporary detached worktree/check-out at the integration commit.

From that fresh state:

- verify initial status clean;
- restore;
- format verify;
- build;
- run all permanent tests;
- run canonical verification;
- run architecture tests as part of the suite;
- verify repository-relative Markdown links if the audit is available;
- verify no provider/network dependency is required for the permanent suite;
- verify final status clean;
- verify temporary SQLite residue is 0;
- safely remove the temporary worktree.

Do not change global/system Git configuration.

Required:

```text
Fresh checkout restore: PASS
Fresh checkout build: PASS
Fresh checkout permanent tests: all pass
Fresh checkout canonical verification: PASS
Fresh checkout working tree: CLEAN
```

## 23. Push

Only after post-commit and fresh-checkout validation pass:

- push the integration branch normally;
- never force push;
- verify local and remote branch SHAs match;
- verify ahead/behind against upstream is `0/0`.

Do not push directly to `main`.

## 24. Pull Request

Create exactly one review-ready, non-draft PR:

- base: `main`;
- head: Release 1.1 integration branch;
- title: `Release 1.1 — Market Data Persistence Foundation` unless repository authority specifies another exact title.

The PR body must summarize:

- Release 1.1 scope;
- WP01–WP15 completion;
- persistence architecture;
- SQLite decision;
- Application contracts/use case;
- Worker persistent execution;
- permanent test counts;
- architecture/documentation alignment;
- manifest/candidate reconciliation;
- fresh-checkout proof;
- security/offline evidence;
- explicit statement that merge requires human authorization.

Do not self-approve.
Do not enable auto-merge.
Do not merge.

## 25. Pull-Request Scope Validation

After PR creation, verify through GitHub:

- base is `main`;
- head branch is exact;
- head SHA equals integration commit;
- PR contains exactly the governed Release 1.1 candidate delta;
- unexpected PR paths: 0;
- commit count matches intended integration strategy;
- PR is not draft;
- auto-merge disabled;
- merge has not occurred.

Inspect hosted checks if present.

If no hosted checks are configured/reported, state that truthfully. Do not invent CI evidence.

## 26. WP16 Issue Acceptance

Only after:

- manifest reconciliation passes;
- local validation passes;
- integration commit passes;
- fresh checkout passes;
- push is verified;
- PR scope is exact;
- no unresolved blocker exists;

then:

1. post concise acceptance evidence to issue #118;
2. close #118;
3. verify Project status becomes Done under the established workflow;
4. leave milestone #52 **OPEN**;
5. leave PR **OPEN**.

Do not close the milestone. That belongs to post-merge closure.

## 27. Final GitHub State Required

A successful WP16 final state should be equivalent to:

```text
Issues #103–#118: 16 Closed / 16 Done
Milestone #52: OPEN
Open milestone issues: 0
Release 1.1 integration PR: OPEN
PR merge: NOT PERFORMED
Release 1.2 active planning: 0
```

If GitHub automation produces a materially different state, report it.

## 28. Final Repository State Required

A successful WP16 final repository state should be:

- current branch: integration branch;
- HEAD: integration commit;
- upstream: matching remote branch;
- ahead/behind upstream: `0/0`;
- staged: 0;
- unstaged: 0;
- untracked: 0;
- working tree: clean;
- `origin/main` remains the accepted pre-integration base because the PR is not merged.

Do not switch `main` forward locally as though the PR were merged.

## 29. Acceptance Matrix

Report PASS/FAIL or exact values for at least:

| Gate | Requirement |
| --- | --- |
| WP01–WP15 lifecycle | Closed/Done |
| WP16 issue initial state | Open/Backlog |
| Manifest expected paths | derived |
| Missing candidate paths | 0 |
| Unexpected candidate paths | 0 |
| Duplicate governed paths | 0 |
| Out-of-band authorities committed | 0 |
| Restore | PASS |
| Format | PASS |
| Build warnings/errors | 0/0 |
| Domain.Tests | 11/11 |
| Application.Tests | 42/42 |
| Infrastructure.Tests | 79/79 |
| Architecture.Tests | 13/13 |
| Total permanent tests | 145/145 |
| Canonical verification | PASS |
| Diff checks | PASS |
| Architecture graph | PASS |
| Domain/Application SQLite leakage | 0 |
| Persistence acceptance | PASS |
| Worker closure validation | PASS |
| Documentation alignment | PASS |
| Cross-document contradictions | 0 |
| Security/offline validation | PASS |
| Temporary SQLite residue | 0 |
| Integration commit | exactly governed candidate |
| Post-commit validation | PASS |
| Fresh checkout | PASS |
| Fresh-checkout tree | CLEAN |
| Push | PASS |
| PR scope | exact |
| PR merge | NO |
| Issue #118 | Closed/Done |
| Milestone #52 | OPEN |
| Release 1.2 started | NO |

Use actual test counts if repository truth legitimately differs.

## 30. Findings and Blockers

Classify findings as:

- `OBSERVATION`
- `CORRECTED-WHITESPACE`
- `BLOCKER`

Do not classify a real acceptance failure as an observation.

For every blocker state:

- exact failing gate;
- evidence;
- whether repository/GitHub mutation occurred before discovery;
- smallest corrective authority required;
- protected next state.

If blocked, do not emit the success terminal.

## 31. Required Execution Report

Return a structured **Release 1.1 WP16 Execution Report** containing at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Initial Candidate Inventory
8. File-Manifest Reconciliation
9. Governance Prompt-Pair Validation
10. Initial Full Validation
11. Persistence Semantic Reconciliation
12. Application Contract/Use-Case Reconciliation
13. SQLite Physical-Model Reconciliation
14. Connection/Bootstrap Reconciliation
15. Persistence/Write Reconciliation
16. Retrieval Reconciliation
17. Failure-Mapping Reconciliation
18. DI/Configuration Reconciliation
19. Worker Reconciliation
20. WP13/WP14 Test Reconciliation
21. WP15 Architecture/Documentation Reconciliation
22. Architecture Acceptance
23. Documentation Acceptance
24. Persistence Acceptance Matrix
25. Worker Closure Validation
26. Security/Package Validation
27. Whitespace/Diff Evidence
28. Candidate Mutation Accounting
29. Staging Reconciliation
30. Integration Branch
31. Integration Commit
32. Post-Commit Validation
33. Fresh-Checkout Reproducibility
34. Push Evidence
35. Pull Request
36. Pull-Request Scope Validation
37. Hosted Check/Review State
38. Issue #118 Lifecycle
39. Milestone Protection
40. Release 1.2 Protection
41. Acceptance Matrix
42. Findings/Blockers
43. Final GitHub State
44. Final Repository State
45. Final Decision
46. Next Authorized Lifecycle Action

Add additional sections when evidence materially requires them.

## 32. Success Terminal

Only when every mandatory gate passes, end with:

```text
RELEASE 1.1 WP16 COMPLETE

FULL VALIDATION, INTEGRATION & ACCEPTANCE:
Manifest reconciliation: PASS
Governed candidate paths: <actual>/<actual>
Unexpected candidate paths: 0
Build warnings/errors: 0/0
Domain.Tests: 11/11
Application.Tests: 42/42
Infrastructure.Tests: 79/79
Architecture.Tests: 13/13
Permanent tests: 145/145
Canonical verification: PASS
Architecture acceptance: PASS
Documentation acceptance: PASS
Persistence acceptance: PASS
Worker closure validation: PASS
Security/offline validation: PASS
Fresh-checkout reproducibility: PASS
Working tree: CLEAN
Integration branch pushed: PASS
Pull request: OPEN / REVIEW-READY
Pull request merged: NO
Issue #118: CLOSED / DONE
Milestone #52: OPEN
Release 1.2 started: NO

NEXT AUTHORIZED LIFECYCLE ACTION:
Human review and explicit merge authorization for the Release 1.1 integration pull request.
```

Substitute actual candidate/test counts where required.

## 33. Post-WP16 Boundary

After successful WP16:

**STOP.**

Do not:

- merge the PR;
- synchronize `main`;
- close milestone #52;
- perform post-merge verification;
- create a tag;
- create a GitHub Release;
- begin Release 1.2.

A separately authorized **Release 1.1 post-merge closure** must govern those actions after the human reports that the integration PR has been merged.

## 34. Final Principle

Release 1.1 acceptance is not established merely because individual work packages passed.

WP16 must prove that the **combined candidate**:

- exactly matches governance;
- composes correctly;
- preserves architecture;
- preserves semantics;
- is reproducible from a clean checkout;
- contains no accidental files;
- contains no unresolved whitespace/security/residue defect;
- is reviewable as one governed integration unit.

Integrate only what has already been accepted.
Do not use WP16 to invent what Release 1.1 should have been.

# Release 1.6 Git Integration — Codex Authority

## 1. Mission

Integrate the exact accepted Release 1.6 candidate for human review after the
successful WP14 decision:

`RELEASE 1.6 ACCEPTED FOR INTEGRATION`

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Release:

`Phase 4 - Release 1.6: Durable Experiment Evidence Foundation`

This is a Git/GitHub transport authority only. It does not authorize product,
test, schema, documentation-semantic, planning, or lifecycle redesign.

## 2. Required Authorities

Read completely before mutation:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/roadmap/release-1.6/prompts/14-full-validation-integration-acceptance-codex-prompt.md`
- accepted WP01–WP14 evidence;
- accepted corrective/reconciliation authorities;
- this integration authority and its five-line companion.

Repository truth and the accepted WP14 candidate govern. Do not reinterpret
Release 1.6 semantics.

## 3. Accepted Baseline

Require before staging:

- branch `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind `0/0`;
- staged paths `0`;
- WP14 repository-content delta `0`;
- WP14 permanent baseline `250/250` with Architecture.Tests `13/13`;
- schema v3;
- issues #182–#195 `14/14 Closed/Done`;
- milestone #47 `CLOSED`, `0 open / 14 closed`;
- no existing Release 1.6 integration branch or PR unless created solely by a
  partial execution of this exact authority and safely reconcilable;
- no Release 1.7 implementation or planning started by this operation.

If any starting gate fails, stop before staging.

## 4. Governed Candidate

WP14 accepted exactly 71 governed Release 1.6 paths and separately classified
these two planning-definition execution authorities as out of band:

- `docs/roadmap/release-1.6/release-1.6-planning-definition-codex-prompt.md`
- `docs/roadmap/release-1.6/release-1.6-planning-definition-codex-prompt-chat.md`

This integration authority pair is explicitly governed and must be added
unchanged to the accepted WP14 candidate:

- `docs/roadmap/release-1.6/prompts/release-1.6-git-integration-codex-prompt.md`
- `docs/roadmap/release-1.6/prompts/release-1.6-git-integration-codex-prompt-chat.md`

Therefore the exact integration candidate is:

- immutable WP14 candidate: 71 paths;
- integration authority pair: 2 paths;
- exact staged candidate: 73 paths;
- out-of-band excluded paths: 2;
- unexpected, missing, duplicate, or unexplained paths: 0.

Do not stage the two out-of-band planning-definition files. Do not delete or
modify them. They may remain untracked after commit as explicitly excluded
execution inputs.

## 5. Pre-Staging Reconciliation

Before `git add`, prove:

- all 73 governed paths exist;
- every actual candidate path has an authority/category;
- no generated database, WAL, SHM, journal, log, probe, temporary project, or
  worktree residue exists;
- every governed full prompt has exactly one companion;
- every governed companion contains exactly five non-empty logical lines and a
  terminal newline;
- direct trailing whitespace findings are zero across tracked and untracked
  candidate files;
- `git diff --check` passes;
- `git diff --cached --check` passes;
- Markdown links are valid;
- conflict markers are absent;
- Gitleaks and canonical Release verification pass;
- Domain/Application/Infrastructure/Architecture counts are exactly
  `11/111/117/13`, total `250/250`, skipped `0`;
- build warnings/errors are `0/0`;
- package/project/reference deltas are `0/0/0`;
- schema remains v3 and production graph remains acyclic;
- provider/network product activity and real credentials are zero.

Do not correct candidate content under this authority. Any material failure
requires a narrow corrective authority.

## 6. Integration Branch

Only after every pre-staging gate passes, create or safely reuse exactly:

`release/1.6-durable-experiment-evidence-foundation`

Reuse is allowed only when an existing local/remote branch was created by a
partial execution of this exact authority and its history equals the accepted
state. Never force-reset or overwrite unrelated history.

## 7. Exact Staging

Stage exactly the reconciled 73 governed paths.

Do not stage:

- the two out-of-band planning-definition files;
- generated/runtime residue;
- unrelated user files;
- Release 1.7 artifacts.

After staging require:

- staged paths: exactly 73;
- staged set equals governed candidate set;
- unstaged governed candidate paths: 0;
- unexpected staged paths: 0;
- `git diff --cached --check`: PASS.

## 8. Integration Commit

Create exactly one conventional commit over the accepted baseline:

`feat: establish Release 1.6 durable experiment evidence foundation`

Require:

- one parent;
- parent SHA `18dfb01bf3503d91415b081b11fcdd7249094373`;
- one commit over `main`;
- no merge commit;
- exact 73-path candidate represented;
- out-of-band files absent from the commit.

Record commit SHA, parent SHA, tree SHA, file count, insertions, and deletions.

## 9. Post-Commit Validation

Before push, rerun on the exact commit:

- canonical `eng/verify.ps1 -Configuration Release`;
- 250/250 permanent tests;
- 13/13 Architecture.Tests;
- Release build with 0 warnings/errors;
- formatting and Gitleaks;
- schema-v3 and dependency-graph checks;
- candidate/tree equality;
- whitespace and residue checks.

The only permitted working-tree remainder is the two explicitly excluded
out-of-band planning-definition files. If any other change remains, stop.

## 10. Fresh Detached-Checkout Proof

Create a disposable detached worktree at the exact integration commit. From
that worktree run the same canonical verification and require:

- 250/250 tests;
- 13/13 Architecture.Tests;
- formatting and Gitleaks PASS;
- build warnings/errors 0/0;
- schema v3;
- no missing/unexpected candidate path;
- no generated/database residue;
- clean checkout after validation.

Remove only the disposable worktree after proof.

## 11. Push

Push the integration branch normally without force.

Require after push:

- local branch SHA equals remote branch SHA;
- ahead/behind relative to remote branch `0/0`;
- `main` was not pushed or modified.

## 12. Pull Request

Create exactly one open, non-draft PR:

- base: `main`;
- head: `release/1.6-durable-experiment-evidence-foundation`;
- title: `Release 1.6 — Durable Experiment Evidence Foundation`;
- auto-merge: disabled.

The PR body must concisely cover:

- durable Experiment evidence and `aiq-experiment-identity-v1` reuse;
- schema v3 and atomic v2→v3 migration;
- `NewlyAccepted`, `EquivalentExisting`, `IntegrityConflict`, exact retrieval,
  and bounded failures;
- Application/Infrastructure/Worker ownership and routing precedence;
- 250/250 tests and 13/13 architecture tests;
- formatting, Gitleaks, offline/provider isolation, and fresh-checkout proof;
- explicit exclusions and deferred Release 1.7+ capabilities.

Do not merge or enable auto-merge.

## 13. PR Read-Back

Require:

- state `OPEN`;
- draft `false`;
- base `main`;
- exact head branch;
- head SHA equals validated integration commit;
- exactly one commit over main;
- changed-file count equals 73;
- merge state is clean/mergeable when GitHub reports it;
- auto-merge disabled;
- no duplicate Release 1.6 PR.

Hosted checks may be absent; report actual state without inventing evidence.

## 14. Prohibited Operations

Do not:

- modify candidate content;
- stage excluded/out-of-band files;
- force push;
- push `main`;
- merge or auto-merge;
- close or reopen milestone #47;
- mutate issues #182–#195 or Project #2;
- delete branches;
- create a tag or GitHub Release;
- begin Release 1.7 planning or implementation.

## 15. Stop Conditions

Stop before commit if candidate or staged-set reconciliation fails. Stop before
push if post-commit or fresh-checkout validation fails. Stop before reporting
success if PR read-back does not represent the exact validated commit.

On failure, preserve recoverable state, report the exact blocker, and identify
the smallest corrective authority required. Do not improvise.

## 16. Required Report

Return a Release 1.6 Git Integration Execution Report including:

1. authorities and accepted baseline;
2. 71+2=73 candidate reconciliation;
3. out-of-band exclusions;
4. prompt-pair, whitespace, links, security, and residue gates;
5. canonical 250/250 and architecture 13/13 evidence;
6. branch name;
7. staged-path equality;
8. commit SHA, parent, tree, and statistics;
9. post-commit validation;
10. fresh-checkout validation;
11. push/read-back state;
12. PR number, URL, base/head, SHA, file/commit counts, draft/merge/auto-merge state;
13. repository and GitHub mutation accounting;
14. blockers/findings;
15. final decision and next authorized action.

## 17. Success Terminal

Use success only when the pushed PR represents the exact validated 73-path
commit and is ready for explicit human merge authorization:

`RELEASE 1.6 GIT INTEGRATION READY FOR MERGE AUTHORIZATION`

Then:

`NEXT AUTHORIZED ACTION: Human review and explicit merge authorization for the Release 1.6 integration PR. Do not merge automatically.`

Otherwise end:

`RELEASE 1.6 GIT INTEGRATION BLOCKED`

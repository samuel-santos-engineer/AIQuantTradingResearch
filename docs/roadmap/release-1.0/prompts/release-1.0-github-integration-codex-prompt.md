# Release 1.0 Git/GitHub Integration — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.0 — Market Data Foundation
**Lifecycle step:** Post-WP16 Git/GitHub Integration
**Predecessor terminal:** `RELEASE 1.0 ACCEPTED`
**Purpose:** Transport the exact WP16-accepted candidate into Git and GitHub without technical or governance drift
**Success terminal:** `RELEASE 1.0 GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION`
**Failure terminal:** `RELEASE 1.0 GITHUB INTEGRATION BLOCKED`

This prompt is the authoritative integration contract for the technically accepted Release 1.0 candidate.

It is intentionally narrower than WP16. It does not redesign, repair, extend, retest by invention, merge, close the release, close planning objects, tag the release, create a GitHub Release, or begin Release 1.1.

---

## 1. Mission

Transport the exact WP16-accepted cumulative Release 1.0 candidate into a dedicated Git branch, one reconciled commit, a pushed remote branch, and one open pull request for explicit human review.

The integration must prove that:

1. the starting repository still contains the exact accepted candidate;
2. all candidate files remain authorized;
3. no accepted file is omitted;
4. no unauthorized file is introduced;
5. the candidate remains technically valid before integration;
6. branch creation preserves the candidate exactly;
7. staging contains exactly the reconciled candidate plus only integration-governance artifacts explicitly authorized by this prompt;
8. the commit contains no semantic drift;
9. post-commit validation still passes;
10. the pushed branch exactly matches the validated local commit;
11. the PR accurately represents the accepted Release 1.0 candidate;
12. no merge, release closure, planning closure, tag, GitHub Release, or Release 1.1 action occurs.

---

## 2. Governing Authorities

Read completely before any Git or GitHub mutation:

1. `docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md`
3. `docs/roadmap/release-1.0/prompts/16-full-validation-integration-acceptance-codex-prompt.md`
4. the WP16 execution report from the current execution context whose terminal is:
   `RELEASE 1.0 ACCEPTED`
5. all Release 1.0 reconciliation/unblock/authorization authorities necessary to explain candidate files;
6. this Git/GitHub integration prompt and its prompt-chat companion;
7. repository Git/GitHub conventions and PR template;
8. GitHub milestone #41 and issues #86–#101, read-only except where this prompt explicitly authorizes PR creation.

### Accepted WP16 baseline

The accepted report establishes, at minimum:

- Release: 1.0 — Market Data Foundation
- candidate files: **78**
- reconciled candidate files: **78/78**
- tracked candidate modifications: **17**
- untracked candidate files: **61**
- unexpected files: **0**
- staged files: **0**
- production cycles: **0**
- provider leakage: **0**
- Domain.Tests: **11/11**
- Application.Tests: **16/16**
- Infrastructure.Tests: **65/65**
- Architecture.Tests: **13/13**
- permanent tests: **105/105**
- canonical verification: PASS
- `git diff --check`: PASS
- `git diff --cached --check`: PASS
- Release 1.1 started: NO
- WP16 changed the candidate: NO
- accepted base `main`/`origin/main`: `138582919d220427d2e87434533b82bf8801f8db`

These are acceptance anchors. Recheck repository truth. If they no longer reconcile, stop rather than silently adapting the integration scope.

---

## 3. Authority Precedence

If authorities conflict, use:

1. later explicit human authorization;
2. Release 1.0 execution plan;
3. Release 1.0 file manifest;
4. WP16 authoritative prompt;
5. WP16 accepted execution result;
6. explicit Release 1.0 unblock/reconciliation authorities;
7. this integration prompt;
8. repository conventions;
9. GitHub planning metadata.

A material conflict affecting candidate identity, file scope, semantics, branch base, or lifecycle authority is a blocker.

---

## 4. Hard Scope Boundaries

### Authorized

This integration step may:

- inspect repository/Git/GitHub state;
- authenticate with GitHub without exposing credentials;
- rerun validation commands;
- create one dedicated integration branch;
- stage the exact accepted candidate;
- stage this integration prompt and its 5-line companion if they exist locally under the governed Release 1.0 prompt path and are otherwise untracked;
- create one Conventional Commit;
- push the integration branch without force;
- create one pull request targeting `main`;
- inspect the PR, reviews, checks, mergeability, and planning state read-only;
- report the exact merge authorization gate.

### Prohibited

Do not:

- change product behavior;
- change tests;
- change architecture;
- change WP15 documentation;
- alter packages/projects/solution/build/scripts/workflows;
- repair candidate defects;
- normalize unrelated files;
- change Git configuration;
- rewrite history;
- amend after validation unless separately authorized;
- force-push;
- merge the PR;
- enable auto-merge;
- close issue #101;
- close/reopen/rename milestone #41;
- mutate issues #86–#101;
- mutate labels or Project fields/items;
- create a tag;
- create a GitHub Release;
- create Release 1.1 planning;
- begin Release 1.1 implementation;
- emit `RELEASE 1.0 CLOSED`.

Any required semantic correction means the WP16 acceptance baseline is no longer transportable as-is. Stop and report a blocker.

---

## 5. Integration Governance Artifacts

This prompt authorizes these two governance artifacts to be included exactly once in the integration candidate if they are present as local untracked files:

```text
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/release-1.0-github-integration-codex-prompt-chat.md
```

They are integration-governance additions created after WP16 acceptance.

Therefore the expected integration staging set is:

```text
78 WP16-accepted candidate files
+ 2 authorized integration-governance files
= 80 files
```

This **80-file total is authoritative only if both integration artifacts are present locally and byte-preserved at the governed paths**.

If either artifact is absent, duplicated, relocated, modified unexpectedly, or accompanied by additional new files, stop and reconcile explicitly. Do not invent a different total.

Do not modify either integration artifact during execution.

---

## 6. Authentication Preflight

Before mutation:

- confirm active GitHub authentication;
- confirm authenticated account;
- confirm repository access;
- confirm permission to push a branch and create a PR;
- do not print tokens, credential stores, headers, or secret values.

Record only safe identity/access evidence.

Authentication failure is a blocker.

---

## 7. Initial Repository State

Record:

```text
repository
branch
HEAD
origin/main
ahead/behind
upstream
staged files
tracked modifications
untracked files
unexpected files
```

Required initial conditions:

- repository is `samuel-santos-engineer/AIQuantTradingResearch`;
- branch is `main`;
- local `main` equals `origin/main`;
- accepted base is still `138582919d220427d2e87434533b82bf8801f8db`, unless a later explicit human authority changed it;
- ahead/behind is `0/0`;
- no files are staged;
- the accepted cumulative candidate remains intentionally uncommitted;
- only the 78 accepted candidate files plus the two authorized integration-governance files may comprise the integration delta;
- unexpected files = 0.

Do not pull/rebase/reset to resolve divergence without separate authority.

---

## 8. Branch / PR Collision Preflight

Inspect local and remote state for:

```text
release/1.0-market-data-foundation
```

Also search open and closed PRs for an equivalent Release 1.0 integration PR.

Expected:

- no conflicting local branch;
- no conflicting remote branch;
- no existing open equivalent PR;
- no already-merged equivalent integration commit.

If a matching branch or PR exists, do not overwrite or duplicate it. Stop and report the exact state unless it is safely reusable without changing the accepted candidate and this is unambiguously proven.

---

## 9. Candidate Reconciliation

Reconstruct the WP16 candidate from current repository truth.

Required baseline classification:

| Candidate group | WP16 accepted quantity / meaning |
|---|---|
| Release 1.0 governance | 42 files |
| Provider assessment/decision | 2 |
| Domain production delta | 0 |
| Application failure/use-case changes | 3 |
| Infrastructure Twelve Data boundary | accepted WP06–WP10 delta |
| Infrastructure DI registration | accepted WP10 tracked delta |
| Worker execution | 1 |
| Application behavioral tests | 1 |
| Infrastructure/provider tests | 5 |
| Architecture evolution | 1 |
| Documentation alignment | 9 |
| Test-only DI package/version | 2 |
| Total WP16 candidate | 78 |

Do not rely on this table alone. Enumerate actual files and map each one to authority.

Then separately classify the two integration-governance files.

Required result:

```text
WP16 candidate: 78/78 reconciled
Integration governance: 2/2 reconciled
Unexpected: 0
Integration candidate total: 80
```

If repository truth does not support this exact reconciliation, stop.

---

## 10. Pre-Integration Technical Validation

Before branch creation or staging, rerun the accepted validation baseline.

At minimum:

```powershell
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git diff --cached --check
```

Expected accepted baseline:

- restore: PASS;
- build errors: 0;
- Domain.Tests: 11/11;
- Application.Tests: 16/16;
- Infrastructure.Tests: 65/65;
- Architecture.Tests: 13/13;
- total permanent tests: 105/105;
- canonical verification: PASS;
- both diff checks: PASS.

Do not perform a live Twelve Data call or require a real credential.

A failure is a blocker. Do not repair it under this prompt.

---

## 11. Candidate Integrity Before Branch Creation

Before creating the branch, prove:

- production behavior unchanged from WP16;
- test semantics unchanged;
- architecture unchanged;
- WP15 documentation unchanged;
- no candidate file omitted;
- no unexpected file added;
- integration-governance artifacts contain no product/test change;
- Release 1.1 paths/artifacts = 0;
- staged files = 0.

Where practical, record hashes for the two integration-governance files before staging so byte preservation can be checked after commit.

---

## 12. Branch Creation

Create exactly:

```text
release/1.0-market-data-foundation
```

from the accepted `main` base.

The branch creation must preserve all uncommitted candidate files.

Immediately prove:

- branch name is correct;
- base commit is the accepted `main`;
- working candidate is still present;
- no file disappeared;
- no new file appeared;
- candidate reconciliation remains 80/80;
- staged files remain 0.

Do not create additional branches.

---

## 13. Staging

Stage exactly the reconciled 80-file integration candidate.

After staging, require:

```text
unstaged candidate files: 0
untracked candidate files: 0
unexpected staged files: 0
expected staged files: 80
```

Inspect:

```powershell
git status --short
git diff --cached --name-status
git diff --cached --stat
git diff --cached --check
```

Reconcile every staged path to:

- one of the 78 WP16-accepted files; or
- one of the two integration-governance artifacts.

Do not use broad staging as a substitute for reconciliation.

If the staged count is not exactly 80, stop before commit.

---

## 14. Staged Semantic Drift Review

Before commit, prove that staging did not change the accepted candidate.

Inspect at minimum:

- production diff;
- test diff;
- architecture-test diff;
- documentation diff;
- package/project diff;
- governance diff.

The only post-WP16 additions must be the two integration-governance Markdown files.

No execution-generated normalization or formatting delta is permitted.

Required:

```text
WP16 accepted semantic delta changed by integration: NO
Unauthorized staged paths: 0
git diff --cached --check: PASS
```

---

## 15. Commit

Create exactly one Conventional Commit:

```text
feat: implement Release 1.0 market data foundation
```

Do not amend.

Record the resulting full commit SHA.

Immediately verify:

- commit contains exactly 80 files relative to its parent;
- parent is the accepted Release 0.9/main base commit;
- working tree is clean;
- staged files = 0;
- untracked files = 0;
- commit diff check passes.

Use:

```powershell
git diff HEAD^ HEAD --check
```

A mismatch is a blocker. Do not create a corrective second commit under this prompt.

---

## 16. Post-Commit Validation

Validate the committed candidate again.

At minimum run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff HEAD^ HEAD --check
```

Also run the Worker only in a safe offline manner consistent with WP16.

Because the default success path requires a real Twelve Data credential/network, do **not** invent or expose credentials. Validate the deterministic missing-configuration process path and rely on permanent offline tests for controlled success semantics unless an already-existing safe executable test harness proves more without mutation.

Required:

- canonical verification: PASS;
- permanent tests: 105/105;
- architecture tests: 13/13;
- build errors: 0;
- commit diff check: PASS;
- working tree: clean;
- provider calls during offline validation: 0 where observable.

Do not amend the commit after this validation.

---

## 17. Commit Integrity Reconciliation

Compare the commit against the pre-commit staging manifest.

Prove:

```text
expected files: 80
committed files: 80
missing: 0
extra: 0
WP16 candidate omitted: 0
integration governance omitted: 0
unexpected committed files: 0
```

Confirm the two integration-governance files are byte-equivalent to their pre-staging versions if hashes were recorded.

Confirm no product/test/architecture/documentation semantics changed during Git transport.

---

## 18. Push

Push without force:

```text
origin/release/1.0-market-data-foundation
```

Configure upstream tracking.

Then prove:

- local branch SHA = remote branch SHA;
- ahead/behind remote = `0/0`;
- no force push was used;
- `origin/main` was not mutated by the push.

Do not push directly to `main`.

---

## 19. Pull Request Creation

Before creating a PR, search again for an equivalent open PR.

If none exists, create exactly one PR:

**Title**

```text
Release 1.0 — Market Data Foundation
```

**Base**

```text
main
```

**Head**

```text
release/1.0-market-data-foundation
```

Use the repository PR template where applicable.

The body must truthfully summarize:

- Release 1.0 objective;
- WP01–WP16 completion;
- Twelve Data historical daily vertical slice;
- provider-independent Domain/Application boundaries;
- Infrastructure ownership of provider mechanics;
- deterministic normalization/failure semantics;
- DI/configuration behavior;
- Worker one-shot boundary;
- documentation alignment;
- WP16 technical acceptance;
- 105/105 permanent tests;
- 13/13 Architecture.Tests;
- canonical verification PASS;
- no live-provider credential required for acceptance;
- exact integration commit SHA;
- human merge authorization required.

Do not claim hosted CI success unless checks actually exist and pass.

---

## 20. PR Inspection

After PR creation, inspect:

- PR number;
- URL;
- title;
- base;
- head;
- state;
- draft state;
- head commit;
- mergeability;
- merge state;
- reviews;
- review decision;
- checks/check rollup;
- auto-merge state.

Expected desired state:

```text
state: OPEN
draft: NO
base: main
head: release/1.0-market-data-foundation
head commit: exact validated commit
auto-merge: disabled
```

`MERGEABLE` / `CLEAN` is desired but report GitHub truth. If GitHub reports a transient unknown mergeability state, re-inspect reasonably; do not mutate the candidate to force a state.

No hosted checks must be invented. If none are configured/reported, state that explicitly as an observation.

---

## 21. GitHub Planning Protection

Read-only inspect:

- milestone #41;
- issues #86–#101;
- Project state relevant to Release 1.0.

Do not mutate them.

Report their observed states without trying to make them match an expectation.

In particular:

- do not close WP16 issue #101;
- do not close milestone #41;
- do not create Release 1.1 planning;
- do not reopen/retitle retired legacy milestones;
- do not modify Project automation.

Planning closure belongs to the later Release 1.0 closure gate.

---

## 22. Tag / Release Protection

Explicitly prove:

```text
tag created: NO
GitHub Release created: NO
```

The integration PR is not release closure.

---

## 23. Merge Authorization Gate

This prompt must stop before merge.

Required final gate:

```text
PR merge performed: NO
Auto-merge enabled: NO
Human merge authorization required: YES
```

Even if the PR is clean, mergeable, reviewed, and has passing checks, do not merge without a new explicit human instruction.

---

## 24. Blocker Policy

Stop with `RELEASE 1.0 GITHUB INTEGRATION BLOCKED` if any of these occurs:

- WP16 accepted candidate no longer reconciles 78/78;
- either integration-governance artifact cannot be reconciled;
- integration candidate is not exactly 80 files;
- unexpected file exists;
- accepted base changed without authority;
- `main` is divergent;
- pre-integration validation fails;
- staging introduces semantic drift;
- cached diff check fails;
- commit contains missing/extra files;
- post-commit validation fails;
- working tree cannot be made clean without discarding accepted content;
- push requires force;
- remote branch conflicts with different content;
- equivalent PR already exists in a conflicting state;
- PR does not point to the validated commit;
- a real credential is required to continue;
- any prohibited GitHub mutation would be necessary.

Do not repair technical candidate defects. Report the minimum separately authorized correction.

---

## 25. Required Execution Report

Return:

```text
# Release 1.0 Git/GitHub Integration Report
```

with these sections:

1. Executive Summary
2. Authority Review
3. Authentication Preflight
4. Initial Repository State
5. Candidate Reconciliation
6. Integration Governance Reconciliation
7. Manifest Reconciliation
8. Pre-Integration Validation
9. Branch Creation
10. Staging Review
11. Staged Semantic Drift Review
12. Commit
13. Post-Commit Validation
14. Commit Integrity
15. Push
16. Pull Request
17. PR Checks / Review State
18. GitHub Planning Protection
19. Candidate Integrity
20. Final Local/Remote Git State
21. Findings
22. Merge Authorization Gate
23. Final Decision
24. Next Authorized Action

Use exact counts and commit SHAs from repository truth.

---

## 26. Acceptance Matrix

The report must explicitly assess:

| Requirement | Result |
|---|---|
| WP16 terminal `RELEASE 1.0 ACCEPTED` | PASS/FAIL |
| Accepted WP16 candidate | 78/78 |
| Integration governance | 2/2 |
| Integration candidate | 80/80 |
| Unexpected files | count |
| Pre-integration restore | PASS/FAIL |
| Pre-integration build errors | count |
| Pre-integration permanent tests | x/x |
| Pre-integration Architecture.Tests | x/x |
| Pre-integration canonical verification | PASS/FAIL |
| Cached diff check | PASS/FAIL |
| Staged files | count |
| Commit files | count |
| Post-commit permanent tests | x/x |
| Post-commit canonical verification | PASS/FAIL |
| Working tree after commit | CLEAN/NOT CLEAN |
| Local/remote branch SHA equal | YES/NO |
| Force push | NO |
| PR created/reconciled | PASS/FAIL |
| PR points to validated commit | YES/NO |
| Merge performed | NO |
| Auto-merge enabled | NO |
| Planning objects mutated | NO |
| Tag created | NO |
| GitHub Release created | NO |
| Release 1.1 started | NO |

---

## 27. Success Criteria

Integration succeeds only when:

- WP16 acceptance is verified;
- the original accepted candidate reconciles exactly 78/78;
- both integration-governance artifacts reconcile exactly once;
- total integration candidate = 80;
- unexpected files = 0;
- pre-integration validation passes;
- the branch is based on the accepted `main`;
- exactly 80 files are staged;
- cached diff validation passes;
- one commit contains exactly those 80 files;
- post-commit validation passes;
- working tree is clean;
- branch push succeeds without force;
- local and remote branch commits match;
- one open PR targets `main`;
- PR head is the exact validated commit;
- no merge/auto-merge occurs;
- no planning closure occurs;
- no tag/GitHub Release occurs;
- Release 1.1 is not started.

The exact final terminal line must be:

```text
RELEASE 1.0 GITHUB INTEGRATION READY FOR MERGE AUTHORIZATION
```

---

## 28. Failure Criteria

If any mandatory integration criterion fails, do not claim readiness.

The exact final terminal line must be:

```text
RELEASE 1.0 GITHUB INTEGRATION BLOCKED
```

State the minimum next authority required.

---

## 29. Next Authorized Action After Success

After successful integration, the only next action is:

```text
Human review of the Release 1.0 pull request and explicit authorization to merge.
```

Do not interpret authorization to merge as authorization to:

- close issue #101;
- close milestone #41;
- create a tag;
- create a GitHub Release;
- begin Release 1.1.

After the human merges the PR, rerun a separately authored **Release 1.0 post-merge closure prompt**.

That closure prompt must:

1. validate the merged `main`;
2. prove the merged commit contains the accepted candidate;
3. rerun required clean-checkout/canonical validation;
4. reconcile Release 1.0 GitHub planning and closure state under explicit authority;
5. ensure Release 1.0 documentation/governance is complete;
6. emit `RELEASE 1.0 CLOSED` only when every closure criterion passes;
7. only after `RELEASE 1.0 CLOSED`, explicitly authorize the next release lifecycle step for **Release 1.1**.

This integration prompt never authorizes Release 1.1.

---

## 30. Execution Discipline

Preserve the accepted candidate exactly.

Prefer stopping over improvising.

Do not:

- reinterpret the 78-file WP16 baseline;
- silently add integration files beyond the two explicitly authorized;
- “fix” candidate content while staging;
- accept a dirty post-commit tree;
- create multiple commits to work around mistakes;
- force-push;
- merge;
- close governance;
- overstate checks;
- expose credentials;
- begin Release 1.1.

The purpose of this step is controlled transport of an already accepted candidate—not further engineering.

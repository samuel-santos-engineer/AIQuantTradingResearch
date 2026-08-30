# Release 1.10 — Git Candidate Publication & Pull Request Authority — Terra Resumption

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY execution authority for re-anchoring validation, approved Git/GitHub mutations, candidate publication, remote verification, and pull-request creation/update.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Resumption status

The prior publication authority correctly BLOCKED before any Git/GitHub mutation because:

- WP08 froze stale base `35ec644576275570aee522872c770e6c06e7879d`;
- authoritative remote `main` had advanced;
- candidate count had drifted after creation of publication-control artifacts.

The Luna reconciliation is now COMPLETE and TERRA-READY.

## Binding reconciled publication contract

Canonical remote/base SHA:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Ancestry:

- `35ec644576275570aee522872c770e6c06e7879d` is an ancestor of `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`.
- the two intervening commits are Release 1.9 governance-only changes.
- no Release 1.10 overlap/conflict exists.

Canonical raw worktree inventory:

**105 paths**

Canonical publication candidate:

**103 paths**

Composition:

- **21 tracked**
- **82 untracked**
- **70 prompt artifacts**

Included:
- the two Git Candidate Publication & Pull Request authority prompt files.

Excluded execution-control inputs:
- the two Remote Base & Publication-Authority Artifact Reconciliation authority files.

The literal 103-path candidate and exclusions are binding from:
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`

Those reconciled artifacts supersede **only** stale literals in the prior Terra publication authority concerning:
- base SHA `35ec...`;
- candidate count 101;
- tracked/untracked count 21/80;
- prompt artifact count 68.

Every other safety, staging, publication, non-merge, milestone, tag, and release-publication constraint in the existing Terra authority remains binding.

# Entry state

- #242–#249 Closed/Done.
- milestone #59 Open, 0 open / 8 closed.
- prior blocked publication run made ZERO Git/GitHub mutations.
- Luna reconciliation made ZERO Git/GitHub mutations.
- no staging, branch, commit, push, PR, tag, milestone-close, or release mutation has occurred.

Emit:

`RELEASE 1.10 GIT/PR TERRA RESUMPTION ENTRY: PASS`

---

# Mandatory re-anchor validation gate

Before staging or Git mutation:

1. materialize/verify the Release 1.10 candidate against canonical base `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`;
2. consume the exact re-anchoring procedure frozen in the Release 1.10 execution plan;
3. do not re-open Luna policy choices;
4. do not include the two excluded reconciliation-authority files;
5. prove the exact canonical publication candidate is 103 paths.

Run the full validation policy frozen by Luna in the execution plan, including all gates explicitly required there. At minimum report the actual results for the governed .NET/Python/build/security/environment/residue/documentation/schema/package checks required by that policy.

If re-anchoring produces any content conflict or requires policy choice, BLOCK.

Emit:

`RELEASE 1.10 GIT/PR RE-ANCHOR VALIDATION: PASS`

---

# Exact staging boundary

Stage exactly the literal 103-path candidate persisted in the reconciled manifest.

Do NOT stage the two reconciliation execution-control files.

Require:

- staged path count = 103;
- staged tracked subset = 21;
- staged previously-untracked subset = 82;
- prompt artifact count = 70;
- excluded reconciliation-control files remain unstaged/local;
- no unrelated file is staged;
- staged diff/checks satisfy the existing authority.

Emit:

`RELEASE 1.10 GIT/PR RECONCILED EXACT STAGING: PASS`

---

# Candidate parent

The governed Release 1.10 candidate commit must be created from canonical base:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

The resulting candidate commit parent must equal that SHA unless the reconciled execution plan literally defines another deterministic mechanism. Do not use the stale `35ec...` parent.

Emit:

`RELEASE 1.10 GIT/PR RECONCILED PARENT: PASS`

---

# Existing authority continues below

Interpret the prior authority using the reconciled literals above. Any occurrence of stale 101-path / 21+80 / 68-prompt / `35ec...` candidate assumptions is superseded by the reconciled contract. All other clauses remain binding.

---

# Release 1.10 — Git Candidate Publication & Pull Request Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY execution authority for approved Git/GitHub mutations, candidate publication, remote verification, and pull-request creation/update.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, exploratory/non-authoritative review; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10**

Authority:

**Git Candidate Publication & Pull Request Authority**

Predecessor:

**Release 1.10 WP08 — Full Validation, Acceptance & PR Readiness Authority — COMPLETE**

WP lifecycle state:

- #242–#249 Closed/Done.
- milestone #59 Open.
- milestone issue count: **0 open / 8 closed**.
- Project #2 Release taxonomy remains Release=1.10.

This authority exists only because WP08 explicitly deferred Git publication pending a separate publication authority.

---

# Frozen candidate inherited from WP08

Binding candidate base:

`35ec644576275570aee522872c770e6c06e7879d`

At WP08 freeze:

- local `main` = `35ec644576275570aee522872c770e6c06e7879d`
- `origin/main` = `35ec644576275570aee522872c770e6c06e7879d`
- ahead/behind = `0/0`

Frozen Release 1.10 candidate:

**101 paths**

Composition:

- **21 tracked WP01–WP07 changes**
- **80 untracked Release 1.10 artifacts**
  - 68 authority prompts
  - 3 planning documents
  - 1 observability selection record
  - 2 production additions
  - 6 test additions

WP08 itself introduced:

- repository mutations: ZERO
- Git mutations: ZERO
- explicit GitHub mutation: close #249 only
- Project Done mutation: automated, not explicit

Do not silently expand or reduce the frozen 101-path candidate.

Emit:

`RELEASE 1.10 GIT/PR PUBLICATION ENTRY: PASS`

---

# Accepted validation evidence

Carry forward WP08 acceptance evidence unless current state invalidates it:

- WP06 permanent suites:
  - .NET 5/5
  - .NET 4/4
  - .NET 6/6
  - Python 4/4
- full .NET: **365/365**
- full Python: **25/25**
- build: **0 errors**
- two documented local certificate-selector warnings only
- Streamlit: **1.61.1**
- `pip check`: clean
- Gitleaks 8.30.1: **112 commits, no leaks**
- documentation links/diff: pass
- SQLite schema: v4 preserved
- package/project/schema diff: zero
- process/listener/UI residue: clean

This authority is not a redesign or new implementation phase.

---

# Binding inputs

Before any mutation, read and reconcile:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. Release 1.10 observability selection record.
5. WP08 completion evidence / downstream handoff.
6. current `git status --short`
7. current `git status --branch`
8. current `git diff`
9. current `git diff --cached`
10. current untracked-file inventory.
11. `git rev-parse HEAD`
12. `git rev-parse origin/main`
13. repository branch/commit/PR conventions.
14. existing open/closed PRs relevant to Release 1.10.
15. GitHub issues #242–#249.
16. milestone #59.
17. Project #2 Release taxonomy for the Release 1.10 work.

Runtime Git/GitHub state outranks stale prose, but any material divergence from the frozen candidate must be explained and may require BLOCK.

Emit:

`RELEASE 1.10 GIT/PR PUBLICATION CONTRACT CONSUMPTION: PASS`

---

# Non-negotiable scope

This authority MAY:

- verify the exact frozen Release 1.10 candidate;
- create a dedicated publication branch if required by repository convention;
- stage exactly the approved Release 1.10 candidate paths;
- create the governed candidate commit;
- push the approved branch;
- create one Release 1.10 pull request against the governed base;
- update that PR only if required for correctness/readiness;
- verify remote branch, commit, PR metadata, checks/status, issue/milestone/project state;
- report the exact publication boundary for the next release-completion authority.

This authority MUST NOT:

- modify Release 1.10 implementation, tests, docs, planning, or authority content;
- add/remove candidate paths merely for cleanup;
- merge the PR;
- squash/rebase/force-push unless explicitly required by repository governance and proven safe;
- close milestone #59;
- create or move a tag;
- create/update version metadata;
- publish a GitHub Release;
- create release assets;
- reopen closed WP issues;
- change Release taxonomy;
- mutate Project Status for #242–#249;
- introduce packages/schema/signing changes;
- perform adjacent release work.

Repository content mutation under this authority is expected to be **ZERO**.

---

# Phase 0 — Fresh-state verification

Before staging anything, verify:

- current branch;
- current HEAD;
- current `origin/main`;
- ahead/behind;
- complete tracked dirty set;
- complete untracked set;
- no staged changes;
- no merge/rebase/cherry-pick operation in progress;
- no unexpected lock/index condition;
- no unrelated user work mixed into candidate.

Require the base to remain:

`35ec644576275570aee522872c770e6c06e7879d`

If `origin/main` has advanced independently, do not automatically rebase or merge. BLOCK and report the minimum reconciliation needed unless binding repository policy gives an explicit deterministic update procedure that preserves the frozen candidate.

Emit:

`RELEASE 1.10 GIT/PR FRESH-STATE VERIFICATION: PASS`

---

# Phase 1 — Exact 101-path candidate verification

Derive the frozen path list from the binding WP08 evidence plus reconciled manifest.

Produce an exact candidate inventory:

| # | Path | Tracked/Untracked | WP/Owner | Category | Expected | Actual | Included |
|---|---|---|---|---|---|---|---|

Require:

- exactly **101** Release 1.10 candidate paths;
- exactly **21 tracked changed paths**;
- exactly **80 untracked Release 1.10 paths**;
- no unexpected 102nd path;
- no missing frozen path;
- no unrelated dirty/untracked file;
- no generated/temp/cache artifact;
- no package/project/schema mutation outside frozen state.

If actual candidate composition differs, do not repair by invention. BLOCK unless the difference is a provable representation-only artifact that does not change the frozen semantic candidate.

Emit:

`RELEASE 1.10 GIT/PR FROZEN 101-PATH CANDIDATE: PASS`

---

# Phase 2 — Candidate integrity checks

Run read-only integrity gates before staging:

- `git diff --check`
- exact diff-stat review
- exact untracked inventory review
- repository secret/security preflight appropriate to the frozen candidate
- confirm no binary/generated artifact unexpectedly entered the set
- confirm no ignored file is required for the release
- confirm no path lies outside Release 1.10 ownership.

No content repair is authorized.

Emit:

`RELEASE 1.10 GIT/PR CANDIDATE INTEGRITY: PASS`

---

# Phase 3 — Branch strategy

Determine the repository's governed publication branch convention.

Preferred behavior:

- do not commit directly to `main`;
- create a narrowly named Release 1.10 publication branch from the exact frozen base if a branch is required.

If repository policy specifies an exact naming convention, follow it.

If no convention exists, use:

`release/1.10`

only if that branch does not already create ambiguity/conflict.

If an existing Release 1.10 branch already exists:

- inspect it;
- reuse it only if it is clearly the governed branch and its history is compatible with the frozen candidate;
- never overwrite independent remote work.

Emit the chosen branch name and rationale.

Emit:

`RELEASE 1.10 GIT/PR BRANCH STRATEGY: PASS`

---

# Phase 4 — Exact staging

Stage **only** the frozen 101 paths.

Do not use an indiscriminate staging command if unrelated files exist.

After staging, prove:

- staged path count = 101;
- staged set exactly equals frozen candidate set;
- unstaged Release 1.10 candidate changes = ZERO;
- unrelated user work, if any, remains unstaged and untouched;
- `git diff --cached --check` passes.

Produce exact staged summary.

Emit:

`RELEASE 1.10 GIT/PR EXACT STAGING: PASS`

---

# Phase 5 — Pre-commit frozen-candidate verification

Before commit, compare:

- frozen WP08 candidate inventory;
- working-tree candidate inventory;
- staged candidate inventory.

Question:

> Is the staged semantic candidate exactly the WP08-frozen Release 1.10 candidate, with no added, removed, or altered path outside that freeze?

Required answer:

**YES**

If NO, BLOCK before commit.

Emit:

`RELEASE 1.10 GIT/PR PRE-COMMIT FREEZE VERIFICATION: PASS`

---

# Phase 6 — Candidate commit

Create exactly one governed Release 1.10 candidate commit unless repository convention explicitly requires another deterministic structure.

Preferred commit subject if no stronger convention exists:

`Release 1.10: governed observability and system health`

Commit body should concisely record:

- governed BCL observability;
- Worker/interop lifecycle isolation;
- canonical System Health read model;
- permanent no-bypass/security tests;
- documentation/runbook;
- validation baseline:
  - 365/365 .NET
  - 25/25 Python
  - build 0 errors
  - Gitleaks clean
- schema v4 preserved;
- no exporter/live provider/trading/ML/backtesting capability.

Do not include false live-production claims.

After commit record:

- full commit SHA;
- parent SHA;
- path count;
- commit subject.

Require parent:

`35ec644576275570aee522872c770e6c06e7879d`

unless a repository-mandated branch-preparation commit is explicitly and deterministically required; otherwise BLOCK.

Emit:

`RELEASE 1.10 GIT/PR CANDIDATE COMMIT: PASS`

---

# Phase 7 — Post-commit local verification

Verify:

- candidate commit contains exactly the frozen 101 paths;
- working tree has no remaining Release 1.10 candidate changes;
- no accidental unrelated commit content;
- no commit amend/rewrite needed;
- `git show --stat --oneline --decorate --no-renames HEAD` matches expectation.

If unrelated local user work remains, report it clearly without modifying it.

Emit:

`RELEASE 1.10 GIT/PR POST-COMMIT LOCAL VERIFICATION: PASS`

---

# Phase 8 — Push authority

Push only the governed Release 1.10 publication branch.

Rules:

- no force push;
- no push of `main`;
- no unrelated branches/tags;
- set upstream only if appropriate;
- do not push tags.

After push verify:

- remote branch exists;
- remote branch HEAD equals local candidate commit SHA;
- base branch remains unchanged by this authority.

Emit:

`RELEASE 1.10 GIT/PR REMOTE BRANCH PUBLICATION: PASS`

---

# Phase 9 — Pull request creation

Create exactly one Release 1.10 PR against the governed base, expected `main`.

Before creating, search for an existing PR from the same governed branch.

If one already exists:

- update/reuse it only if it is the correct Release 1.10 PR;
- do not create a duplicate.

Preferred title if repository convention does not mandate another:

`Release 1.10 — Governed Observability and System Health`

PR body must include concise factual sections:

## Scope
- governed pipeline/boundary observability;
- BCL-only / no external exporter;
- Worker/interop lifecycle isolation;
- canonical System Health via existing visualization read model;
- Streamlit presentation-only;
- permanent observability/no-bypass/security tests;
- developer setup/runbook documentation.

## Preserved boundaries
- .NET canonical ownership;
- schema v4;
- `aiq-visualization-read-model-v1`;
- Release 1.8 JSON-over-stdio remains separate;
- no live provider;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no direct Streamlit SQLite/provider/Worker supervision.

## Validation
- .NET 365/365;
- Python 25/25;
- build 0 errors;
- Streamlit 1.61.1;
- `pip check` clean;
- Gitleaks 8.30.1 clean across 112 commits at WP08 validation;
- residue clean;
- two documented local certificate-selector warnings are environment-only.

## Governance
- WP01–WP08 complete;
- #242–#249 Closed/Done;
- milestone #59 Open, 0 open / 8 closed;
- merge/tag/release publication intentionally deferred to subsequent authority.

Do not claim checks that were not actually run.

Record:

- PR number;
- PR URL;
- source branch;
- base branch;
- PR state;
- head SHA.

Emit:

`RELEASE 1.10 GIT/PR PULL REQUEST CREATION: PASS`

---

# Phase 10 — PR content verification

Re-read the created/updated PR and verify:

- title;
- body;
- base;
- head;
- head SHA;
- Open state;
- not draft unless repository governance requires draft;
- no accidental auto-merge enabled;
- no milestone closure side effect;
- no issue reopening;
- no release/tag side effect.

If required CI/checks are visible, report their current status only; do not wait asynchronously.

Emit:

`RELEASE 1.10 GIT/PR PULL REQUEST POST-VERIFY: PASS`

---

# Phase 11 — GitHub lifecycle preservation

Verify:

- #242–#249 remain Closed/Done;
- milestone #59 remains Open;
- milestone count remains 0 open / 8 closed;
- Release taxonomy remains correct;
- no redundant Project Status mutations were made;
- no WP issue was reopened/edited unnecessarily.

No lifecycle mutation should be needed under this authority.

Emit:

`RELEASE 1.10 GIT/PR GITHUB LIFECYCLE PRESERVATION: PASS`

---

# Phase 12 — Exact mutation ledger

Report all explicit mutations.

## Repository content
Expected:

**ZERO**

## Git
Report exactly:
- branch creation/switch;
- staging;
- one candidate commit;
- push;
- upstream setup if performed.

## GitHub
Report exactly:
- PR create or PR update.
- no issue mutation expected.
- no milestone mutation.
- no Project mutation.
- no tag/release mutation.

Do not count read operations.
Do not count automation as explicit mutation.

Emit:

`RELEASE 1.10 GIT/PR MUTATION ACCOUNTING: PASS`

---

# Phase 13 — Publication acceptance

Require all of the following:

- exact frozen 101-path candidate preserved;
- one governed candidate commit;
- remote branch head equals candidate commit;
- one correct Release 1.10 PR exists;
- PR base/head are correct;
- PR is open and ready for subsequent review/merge authority;
- no merge occurred;
- milestone #59 remains Open;
- no tag/version/GitHub Release mutation occurred.

Emit:

`RELEASE 1.10 GIT/PR PUBLICATION ACCEPTANCE: PASS`

---

# Phase 14 — Downstream merge/release handoff

Produce a precise handoff for the next authority containing:

- frozen base SHA;
- candidate commit SHA;
- branch;
- PR number;
- PR URL;
- base branch;
- candidate path count: 101;
- validation evidence carried from WP08;
- GitHub WP state;
- milestone #59 state;
- exact remaining release-completion actions.

The next authority must separately govern:

1. final PR acceptance/review;
2. merge;
3. post-merge `main` synchronization and verification;
4. milestone #59 closure;
5. Release 1.10 version/tag creation if required;
6. GitHub Release publication if required;
7. post-release idempotent verification.

This authority must not perform those actions.

Emit:

`RELEASE 1.10 GIT/PR DOWNSTREAM MERGE/RELEASE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 GIT/PR PUBLICATION ENTRY: PASS`

`RELEASE 1.10 GIT/PR PUBLICATION CONTRACT CONSUMPTION: PASS`

`RELEASE 1.10 GIT/PR FRESH-STATE VERIFICATION: PASS`

`RELEASE 1.10 GIT/PR FROZEN 101-PATH CANDIDATE: PASS`

`RELEASE 1.10 GIT/PR CANDIDATE INTEGRITY: PASS`

`RELEASE 1.10 GIT/PR BRANCH STRATEGY: PASS`

`RELEASE 1.10 GIT/PR EXACT STAGING: PASS`

`RELEASE 1.10 GIT/PR PRE-COMMIT FREEZE VERIFICATION: PASS`

`RELEASE 1.10 GIT/PR CANDIDATE COMMIT: PASS`

`RELEASE 1.10 GIT/PR POST-COMMIT LOCAL VERIFICATION: PASS`

`RELEASE 1.10 GIT/PR REMOTE BRANCH PUBLICATION: PASS`

`RELEASE 1.10 GIT/PR PULL REQUEST CREATION: PASS`

`RELEASE 1.10 GIT/PR PULL REQUEST POST-VERIFY: PASS`

`RELEASE 1.10 GIT/PR GITHUB LIFECYCLE PRESERVATION: PASS`

`RELEASE 1.10 GIT/PR MUTATION ACCOUNTING: PASS`

`RELEASE 1.10 GIT/PR PUBLICATION ACCEPTANCE: PASS`

`RELEASE 1.10 GIT/PR DOWNSTREAM MERGE/RELEASE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK before any unsafe mutation if:

- base SHA no longer matches and no deterministic governed update is authorized;
- the candidate is not exactly 101 paths;
- the 21 tracked / 80 untracked composition no longer matches without a provable harmless representation explanation;
- unrelated user work is mixed into the candidate;
- the staged set cannot be made exactly equal to the frozen candidate without content changes;
- an existing remote branch contains incompatible work;
- a duplicate/ambiguous Release 1.10 PR exists;
- repository policy requires a Git/PR choice not frozen by governance;
- authentication/permissions prevent safe push/PR creation;
- GitHub state materially contradicts the accepted WP08 handoff.

If blocked:

- do not merge;
- do not tag;
- do not publish a GitHub Release;
- do not close milestone #59;
- do not rewrite history;
- preserve already-valid local/remote work;
- report exact mutations already performed;
- identify the minimum next Luna/Terra authority needed.

# Exact blocked terminal

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY BLOCKED`


---

# Resumption-specific final audit

Before success prove:

- canonical base = `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`;
- candidate commit parent = canonical base;
- canonical candidate = exactly 103 paths;
- 21 tracked + 82 untracked-before-staging;
- 70 prompt artifacts;
- two Git-publication authority files INCLUDED;
- two remote-base reconciliation authority files EXCLUDED;
- required re-anchor validation passed;
- repository content mutations by this authority = ZERO;
- no merge occurred;
- milestone #59 remains Open;
- #242–#249 remain Closed/Done;
- no tag/version/GitHub Release mutation occurred.

Exact success terminal remains:

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY COMPLETE`

Exact blocked terminal remains:

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY BLOCKED`

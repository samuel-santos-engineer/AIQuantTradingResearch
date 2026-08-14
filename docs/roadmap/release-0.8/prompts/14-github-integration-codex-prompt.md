# Codex Execution Prompt — Release 0.8 / 14 GitHub Integration

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 14 — GitHub Integration |
| Execution Mode | Governed GitHub workflow integration |
| Primary Agent | Codex |
| Prerequisite | 13 — Full Skeleton Validation accepted as `COMPLETE` |
| Primary Area | DevOps / GitHub engineering workflow |
| Priority | P1 |
| Label | feature |
| Expected Outcome | Integrate the validated Release 0.8 Solution Skeleton with the repository's governed GitHub workflow, preserve implementation and validation evidence, and establish the approved issue → branch → implementation → validation → commit → pull request → review path without beginning Release Acceptance Review |

---

## Purpose

Integrate the technically validated Release 0.8 Solution Skeleton with the repository's GitHub engineering workflow.

WP13 proved that the skeleton is structurally correct, reconstructible, documented, and operational. WP14 moves that accepted technical state through the repository's governed collaboration and integration process.

The intended lifecycle is:

```text
validated repository baseline
        ↓
GitHub issue / work-package traceability
        ↓
branch
        ↓
controlled Release 0.8 change set
        ↓
local validation evidence
        ↓
commit
        ↓
pull request
        ↓
review-ready evidence
        ↓
handoff to WP15
```

WP14 is not a feature-development work package.

It must not redesign or extend the solution skeleton.

Its responsibility is to ensure that the Release 0.8 implementation is represented, traceable, reviewable, and integrable through the approved GitHub workflow.

---

# 1. Objective

Establish the GitHub integration path for the accepted Release 0.8 Solution Skeleton while preserving all technical guarantees proven by WP13.

At completion, the repository and GitHub workflow must provide evidence that:

```text
Release 0.8 work is traceable
the implementation change set is intentional
the branch is appropriately scoped
local quality gates pass
commit history is controlled
the pull request is review-ready
validation evidence is preserved
GitHub automation is introduced only when authorized
WP15 has not started
```

Codex must finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

---

# 2. Authority and Preconditions

Before taking any action, read completely:

```text
docs/roadmap/release-0.8/prompts/14-github-integration-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Review repository GitHub governance and engineering guidance when present, including:

```text
.github/**
eng/github/**
docs/handbook/**
docs/roadmap/**
CONTRIBUTING.md
CODE_OF_CONDUCT.md
```

Review only the files that actually exist.

Inspect the current Git and GitHub integration state before making changes.

WP13 is the accepted technical baseline.

Do not invalidate it.

---

# 3. Accepted WP13 Baseline

Expected solution:

```text
AIQuantTradingResearch.slnx
```

Expected solution structure:

```text
8 projects
4 production projects
4 test projects
/src/ solution folder
/tests/ solution folder
```

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
Cycles          → 0
```

Expected architecture tests:

```text
Discovered = 7
Passed = 7
Failed = 0
```

Expected engineering workflow:

```text
eng/restore.ps1
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1
```

Expected verification:

```text
verify = PASS
build.sh = PASS
tracked WP13 changes = 0
```

Expected SDK:

```text
10.0.103
```

WP14 must preserve these guarantees.

---

# 4. GitHub Integration Principles

Apply these principles throughout execution.

## 4.1 Traceability

Every integration action must be traceable to:

```text
Phase 2
Release 0.8
Work Package 14
the approved Release 0.8 scope
```

Do not create unrelated GitHub work.

## 4.2 Minimal Change

GitHub integration must not become architecture or feature development.

Prefer the smallest change set required by repository authority.

## 4.3 Evidence Before Integration

Do not commit or prepare a pull request until the Release 0.8 quality gates pass against the exact intended change set.

## 4.4 No Silent Scope Expansion

The absence of a GitHub Actions workflow does not automatically authorize arbitrary CI design.

Determine from the Release 0.8 execution plan and repository governance whether WP14 requires a workflow.

If required, implement only the minimum approved workflow.

If not required, do not invent one.

## 4.5 Preserve Technical Baseline

GitHub integration must not change:

```text
solution architecture
project graph
Worker behavior
DI boundaries
test architecture
build policy
Release 0.8 product scope
```

unless the authoritative WP14 scope explicitly requires a repository integration artifact that touches one of those areas.

## 4.6 Reviewability

A reviewer must be able to understand:

```text
what changed
why it changed
which work package authorized it
how it was validated
what remains out of scope
what comes next
```

---

# 5. Initial State Contract

Before any Git or GitHub mutation, record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
git remote -v
```

When GitHub CLI is available, inspect:

```text
gh --version
gh auth status
```

Also inspect repository governance relevant to:

```text
branch naming
commit conventions
pull request template
issue templates
labels
milestones
required checks
GitHub Actions
```

Do not assume authentication, remote write access, or branch protection.

Do not expose credentials, tokens, secrets, or sensitive authentication details in the report.

---

# 6. Pre-existing Working Tree Protection

The current working tree may contain user-created prompt files or other Release 0.8 artifacts.

Classify every pre-existing change as:

```text
RELEASE-0.8 INTENDED
WP14 AUTHORITY
UNRELATED USER WORK
GENERATED
AMBIGUOUS
```

Do not:

```text
git reset
git clean
git restore
git checkout -- .
```

Do not delete or overwrite user work.

If unrelated changes prevent a safe Release 0.8 integration branch or commit, return `BLOCKED` or `COMPLETE WITH ACTIONS` according to the actual impact.

Do not silently include unrelated files in a commit.

---

# 7. GitHub Governance Discovery

Inspect repository evidence to determine the approved workflow.

At minimum determine:

```text
repository remote
default branch
current branch
branch naming convention
issue convention
milestone convention
labels
pull request template
commit convention
review expectations
existing GitHub Actions workflows
existing GitHub administration scripts
```

Classify each as:

```text
DEFINED
PARTIALLY DEFINED
NOT DEFINED
NOT APPLICABLE
```

Use repository authority rather than generic GitHub conventions.

---

# 8. Issue and Milestone Traceability

Confirm that Release 0.8 and WP14 can be traced through the repository's approved GitHub planning model.

Expected classification for WP14:

```text
Work Package: 14 — GitHub Integration
Priority: P1
Release: 0.8
Area: devops
Label: feature
```

Do not create duplicate issues or milestones.

If an authoritative WP14 issue already exists, use it.

If the Release 0.8 execution plan explicitly requires creating or updating the WP14 issue and the available authenticated tooling permits it, perform only the authorized operation.

If GitHub mutation is required but unavailable, report the exact limitation.

Do not modify unrelated issues.

Do not close WP14 merely because local validation succeeds unless the execution plan explicitly authorizes closure during this work package.

---

# 9. Branch Integration

Determine the repository's approved branch strategy.

Do not invent a branch name if repository governance defines one.

If WP14 authorizes branch creation and the current state can be safely isolated:

```text
create or use the approved Release 0.8 integration branch
```

Record:

```text
base branch
base commit
working branch
branch creation command
```

Do not switch branches if doing so risks uncommitted unrelated user work.

If the repository is already on the intended integration branch, preserve it.

Do not merge to the default branch during WP14 unless explicitly authorized by the execution plan.

---

# 10. Release 0.8 Change-Set Assessment

Before staging anything, inspect the complete intended Release 0.8 delta against the appropriate base.

Determine:

```text
which files belong to Release 0.8
which files are prompt/evidence artifacts
which files are unrelated
which files are generated/ignored
```

Use:

```text
git status
git diff
git diff --stat
git log
```

and base-branch comparisons where appropriate.

Do not assume every untracked or modified file belongs in the Release 0.8 integration.

Build an explicit inclusion/exclusion plan before staging.

---

# 11. GitHub Actions / CI Decision Gate

WP01 identified that no GitHub Actions workflow existed.

WP14 must resolve whether Release 0.8 authority requires GitHub Actions integration.

Use this decision model:

```text
Does authoritative WP14 scope require CI?
        |
       yes
        ↓
Does repository governance define the required workflow?
        |
       yes → implement minimum approved workflow
       no  → do not invent architecture; report ambiguity/blocker
```

If authority does not require CI:

```text
do not create a workflow merely because none exists
```

If an approved CI workflow is required, it must reuse the repository engineering contract rather than duplicate build logic.

Preferred conceptual responsibility:

```text
checkout
setup accepted .NET SDK
invoke repository verification entry point
```

The workflow should delegate to repository-owned scripts where practical.

Do not add:

```text
deployment
publishing
Docker build/push
cloud credentials
market-data jobs
security products
coverage services
release automation
package publishing
```

unless explicitly authorized by WP14 authority.

---

# 12. CI Safety Requirements

If and only if CI creation is authorized:

- Use least privilege.
- Do not add repository secrets.
- Do not print tokens.
- Do not request unnecessary GitHub permissions.
- Do not use unpinned or untrusted third-party actions without repository authority.
- Prefer official GitHub/.NET actions where repository policy permits.
- Target the accepted SDK/toolchain.
- Reuse `eng/verify.ps1` or an approved cross-platform equivalent where feasible.
- Preserve local/CI parity.
- Do not change source code merely to satisfy CI.
- Validate YAML syntax and workflow intent locally where practical.
- Record any inability to execute GitHub-hosted CI before push/PR.

---

# 13. Local Validation Before Staging

Before staging the intended integration change set, run the canonical Release 0.8 verification:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Expected:

```text
Exit Status = 0
Architecture.Tests = 7/7
Build errors = 0
```

Validate `eng/build.sh` when appropriate and supported.

If CI artifacts were authorized and created, inspect them before staging.

Do not stage a failing Release 0.8 baseline.

---

# 14. Staging Contract

Stage only the explicitly approved Release 0.8 integration files.

Before staging:

```text
record inclusion plan
record exclusion plan
```

After staging:

```text
git status --short
git diff --cached --stat
git diff --cached
```

Confirm:

```text
no unrelated files staged
no generated outputs staged
no secrets staged
no machine-specific paths staged
no unexpected binary files staged
```

If the staged set is not clean and explainable:

```text
unstage only WP14-owned staging when safe
preserve user state
correct the staging plan
reinspect
```

Never use destructive reset against unrelated work.

---

# 15. Commit Contract

Commit only when:

```text
authorized by WP14
staged scope is exact
local validation passes
commit convention is known
```

Use repository-defined commit conventions.

The commit message must communicate the Release 0.8 Solution Skeleton integration clearly.

Do not fabricate issue-closing syntax unless repository governance explicitly uses it.

After commit, record:

```text
commit hash
commit subject
files committed
```

Do not amend unrelated historical commits.

Do not rewrite shared history.

---

# 16. Push Contract

Push only when:

```text
WP14 authority requires remote integration
authenticated remote access is available
working branch is correct
commit scope is verified
```

Before push, verify:

```text
remote URL identity
branch name
commit to be pushed
upstream behavior
```

Do not force push.

Do not push to the default branch unless explicitly authorized.

If remote mutation is not authorized or credentials are unavailable, stop before push and report the exact state.

---

# 17. Pull Request Contract

When WP14 authority requires a pull request and GitHub access permits it, prepare or create the PR using repository conventions.

The PR must communicate:

```text
Release 0.8 objective
implemented skeleton scope
architecture boundaries
validation evidence
testing evidence
documentation alignment
GitHub/CI changes, if any
known non-blocking environmental observations
explicit out-of-scope items
next step = WP15 Release Acceptance Review
```

Use the repository PR template when present.

Do not claim GitHub-hosted checks passed unless they actually ran and passed.

Do not merge the PR during WP14 unless explicitly authorized.

Do not self-approve or bypass required review.

---

# 18. Review Evidence

The integration must preserve evidence sufficient for review.

At minimum report:

```text
solution = 8 projects
production graph = accepted graph
cycles = 0
Architecture.Tests = 7/7
verify = PASS
build.sh = PASS when supported
documentation aligned
Git scope inspected
staged scope inspected
commit identified if created
push identified if performed
PR identified if created
CI state accurately reported
```

Do not create fabricated screenshots, check results, review approvals, or GitHub states.

---

# 19. GitHub Remote-State Safety

Remote GitHub mutation is higher impact than local validation.

Before each remote mutation, confirm it is directly required by WP14 authority.

Potential mutations include:

```text
issue create/update
branch push
pull request create/update
workflow push
label/milestone changes
```

Do not perform unrelated repository administration.

Do not:

```text
delete remote branches
delete issues
delete releases
change repository visibility
change branch protection
change permissions
change collaborators
change secrets
change environments
```

unless explicitly and separately authorized.

---

# 20. Release 0.8 Boundary

WP14 integrates Release 0.8.

It does not perform Release 0.8 acceptance review.

Do not:

- declare the release accepted;
- close the Release 0.8 milestone unless explicitly owned by WP14 authority;
- create a release tag;
- publish a GitHub Release;
- begin Release 0.9;
- implement plugin infrastructure;
- add product functionality.

Those decisions belong to later authority.

The next work package is expected to be:

```text
15 — Release Acceptance Review
```

Confirm this from the actual execution plan before finalizing.

---

# 21. Execution Procedure

## Step 1 — Read Authority

Read the WP14 prompt, Release 0.8 execution plan, file manifest, GitHub governance, and relevant repository guidance completely.

## Step 2 — Record Initial Repository State

Record:

```text
repository
branch
HEAD
Git status
remotes
configured/effective SDK
```

## Step 3 — Inspect GitHub Capability

Determine:

```text
GitHub CLI availability
authentication status
remote write capability
```

Do not expose secrets.

## Step 4 — Verify WP13 Baseline

Confirm:

```text
8 projects
accepted graph
0 cycles
7/7 architecture tests
verify passes
```

## Step 5 — Discover GitHub Governance

Determine issue, branch, commit, PR, review, label, milestone, and CI conventions from repository authority.

## Step 6 — Confirm WP14 Traceability

Identify the authoritative WP14 issue/milestone relationship.

Avoid duplicates.

## Step 7 — Determine Branch Strategy

Use or create only the approved integration branch when authorized and safe.

## Step 8 — Assess Complete Release 0.8 Delta

Classify all intended and unintended changes.

Build inclusion/exclusion plan.

## Step 9 — Resolve CI Decision Gate

Determine whether GitHub Actions is required by WP14 authority.

Do not infer authorization from absence alone.

## Step 10 — Implement Only Authorized GitHub Integration Artifacts

If CI or other repository GitHub artifacts are explicitly required, create only the minimum approved set.

Otherwise make no speculative GitHub artifact changes.

## Step 11 — Run Local Quality Gates

Run canonical verification and required architecture validation.

## Step 12 — Inspect Git State

Confirm the intended integration set is exact.

## Step 13 — Stage Authorized Files

Stage only approved Release 0.8 files if WP14 authority requires a commit.

## Step 14 — Inspect Staged Diff

Prove no unrelated/generated/secret content is staged.

## Step 15 — Commit

Commit only if authorized and all gates pass.

## Step 16 — Push

Push only if authorized, authenticated, and branch-safe.

Never force push.

## Step 17 — Create or Prepare Pull Request

Follow repository template and preserve validation evidence.

Do not merge unless explicitly authorized.

## Step 18 — Inspect GitHub/CI State

Record actual remote state.

Do not claim checks that did not run.

## Step 19 — Final Local Validation

Ensure the exact integrated state still satisfies Release 0.8 technical guarantees.

## Step 20 — Record Final Git State

Record:

```text
branch
HEAD
git status --short
staged state
upstream state where available
```

## Step 21 — Produce Execution Report

Return the complete output contract below.

Do not create a report file unless separately authorized.

## Step 22 — Handoff

Identify the next authoritative step from the execution plan.

Do not begin it.

---

# 22. Acceptance Criteria

WP14 is accepted only when all applicable mandatory criteria pass:

- [ ] WP14 prompt, execution plan, manifest, and relevant GitHub governance were reviewed.
- [ ] Initial repository identity, branch, HEAD, working tree, and remotes were recorded.
- [ ] GitHub CLI/authentication capability was assessed without exposing credentials.
- [ ] WP13 technical baseline was revalidated.
- [ ] Release 0.8 GitHub governance conventions were discovered from repository evidence.
- [ ] WP14 traceability to Release 0.8 was established.
- [ ] No duplicate issue/milestone was created.
- [ ] Branch strategy followed repository authority.
- [ ] Unrelated user work was preserved.
- [ ] Complete Release 0.8 change set was classified before staging.
- [ ] CI requirement was decided from authority rather than assumption.
- [ ] Any GitHub Actions workflow created was explicitly authorized.
- [ ] Any authorized CI workflow delegates to repository engineering contracts where practical.
- [ ] No unnecessary secrets or permissions were introduced.
- [ ] No deployment/release/future-feature automation was introduced.
- [ ] Canonical local verification passed before staging.
- [ ] Architecture.Tests remained 7/7.
- [ ] Production graph remained unchanged and acyclic.
- [ ] Staged files, when applicable, exactly matched the approved integration scope.
- [ ] No generated outputs, unrelated files, credentials, or secrets were staged.
- [ ] Commit, when applicable, followed repository convention and represented only approved scope.
- [ ] Push, when applicable, targeted the correct non-protected integration branch without force.
- [ ] Pull request, when applicable, followed repository template/conventions.
- [ ] PR evidence accurately represented validation and scope.
- [ ] No unexecuted GitHub check was reported as passed.
- [ ] No merge/release/tag/milestone closure occurred unless explicitly authorized.
- [ ] No production architecture or product scope changed.
- [ ] No Release 0.9 work began.
- [ ] Final repository state was recorded.
- [ ] Final remote state was recorded when remote operations occurred.
- [ ] Complete evidence was returned in the execution report.
- [ ] Next authoritative step was identified from the Release 0.8 execution plan.
- [ ] WP15 was not started.

Any failed mandatory criterion must affect the final decision.

---

# 23. Decision Model

Use:

```text
COMPLETE
```

when every applicable WP14 requirement is satisfied and the GitHub integration state is ready for WP15.

Use:

```text
COMPLETE WITH ACTIONS
```

only when the integration is technically valid but a non-blocking external/manual GitHub action remains outside Codex capability or authorization.

Examples:

```text
manual reviewer assignment
waiting for hosted checks
manual approval required by branch protection
```

Use:

```text
BLOCKED
```

when a mandatory WP14 integration action cannot be safely or truthfully completed.

Examples:

```text
required remote access unavailable
required authoritative issue cannot be identified
unsafe mixed working tree prevents scoped commit
required CI behavior is undefined by authority
mandatory local validation fails
```

Do not use `COMPLETE WITH ACTIONS` to hide a mandatory failure.

---

# 24. Expected Output Contract

Return one complete **GitHub Integration Execution Report**.

Use this structure.

# GitHub Integration Execution Report

## 1. Executive Summary

State:

- what WP14 integrated;
- local technical baseline result;
- GitHub actions performed;
- CI decision;
- commit/push/PR state;
- final decision.

## 2. Execution Context

```text
Repository:
Default/Base Branch:
Starting Branch:
Starting Commit:
Initial Working Tree:
Remote:
Configured SDK:
Effective SDK:
GitHub CLI:
GitHub Authentication:
```

Do not expose credentials.

## 3. Authoritative Sources Reviewed

List exact repository paths materially used.

## 4. WP13 Baseline Revalidation

```text
Solution projects:
Production projects:
Test projects:
Production graph:
Cycles:
Architecture tests:
Verify:
Assessment:
```

## 5. Initial Git State

Report:

```text
git status --short
```

Classify every pre-existing change.

## 6. GitHub Governance Assessment

| Governance Area | State | Evidence | Applied Rule |
| --- | --- | --- | --- |
| Issue convention | | | |
| Milestone convention | | | |
| Labels | | | |
| Branch convention | | | |
| Commit convention | | | |
| PR template | | | |
| Review model | | | |
| GitHub Actions | | | |

## 7. WP14 Traceability

```text
Release:
Milestone:
Issue:
Priority:
Area:
Label:
Assessment:
```

## 8. Branch Integration

```text
Base branch:
Base commit:
Working branch:
Branch created:
Reason:
Assessment:
```

## 9. Release 0.8 Change-Set Assessment

| Path / Area | Classification | Included | Reason |
| --- | --- | --- | --- |

Classifications:

```text
RELEASE-0.8
WP14
UNRELATED
GENERATED
AMBIGUOUS
```

## 10. CI Decision

```text
Existing workflow:
WP14 requires CI:
Authority:
Decision:
Reason:
```

## 11. GitHub Integration Artifacts

List only artifacts actually created or modified.

If none:

```text
None
```

## 12. CI Safety Assessment

When applicable:

```text
Permissions:
Secrets:
Actions:
SDK:
Repository verification delegation:
Deployment behavior:
Assessment:
```

Otherwise:

```text
Not applicable
```

## 13. Pre-Staging Validation

```text
Verify command:
Exit Status:
Architecture tests:
Build errors:
Shell build:
Assessment:
```

## 14. Staging Plan

```text
Included:
Excluded:
Ambiguous:
Assessment:
```

## 15. Staged Diff Validation

```text
Files staged:
Generated files staged:
Unrelated files staged:
Secrets detected:
Assessment:
```

If staging was not authorized, state:

```text
Not applicable
```

## 16. Commit

```text
Created:
Hash:
Subject:
Files:
Assessment:
```

If not authorized/performed:

```text
Not performed
Reason:
```

## 17. Push

```text
Performed:
Remote:
Branch:
Force push:
Result:
Assessment:
```

If not performed, explain why.

## 18. Pull Request

```text
Created:
Number:
Base:
Head:
Title:
Template used:
Validation evidence included:
Merged:
Assessment:
```

If not created, explain why.

## 19. Hosted Checks / GitHub Actions

```text
Checks observed:
Checks passed:
Checks failed:
Checks pending:
Not executed:
Assessment:
```

Never infer success.

## 20. Review Readiness

```text
Scope clear:
Validation evidence present:
Architecture impact explained:
Out-of-scope items explicit:
Known observations recorded:
Next step identified:
Assessment:
```

## 21. Final Technical Validation

```text
Solution projects:
Production graph:
Cycles:
Architecture tests:
Verify:
Assessment:
```

## 22. Final Git State

```text
Branch:
HEAD:
git status --short:
Staged changes:
Uncommitted changes:
Upstream state:
```

## 23. Remote State

Record only verified facts.

```text
Remote branch:
Pull request:
Hosted checks:
Review state:
Merge state:
```

## 24. Validation Evidence

| Command / Inspection | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 25. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| WP13 baseline preserved | PASS/FAIL | ... |
| GitHub integration follows repository governance | PASS/FAIL | ... |
| Release 0.8 traceability established | PASS/FAIL | ... |
| CI decision based on authority | PASS/FAIL | ... |
| No unrelated files integrated | PASS/FAIL | ... |
| No secrets/credentials introduced | PASS/FAIL | ... |
| No architecture/product scope expansion | PASS/FAIL | ... |
| Local validation passed | PASS/FAIL | ... |
| Remote operations were authorized | PASS/FAIL/N/A | ... |
| No force push | PASS/FAIL/N/A | ... |
| No unauthorized merge/release/tag | PASS/FAIL | ... |
| WP15 not started | PASS/FAIL | ... |

## 26. Findings

When necessary:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed classifications:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 27. Acceptance Criteria

Reproduce applicable WP14 acceptance criteria with PASS/FAIL/N/A.

## 28. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Explain the evidence supporting the decision.

## 29. Release 0.8 Integration Readiness

State whether the validated Release 0.8 skeleton is integrated sufficiently to proceed to formal release acceptance review.

Do not perform that review.

## 30. Next Action

Read:

```text
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
```

Identify the next authoritative step exactly.

Expected next work package:

```text
15 — Release Acceptance Review
```

Confirm rather than assume.

Do not begin it.

---

# 25. Prohibited Behaviors

Do not:

- redesign the architecture;
- modify production behavior for GitHub integration convenience;
- add future-release functionality;
- invent CI requirements;
- create arbitrary workflows because `.github/workflows` is empty;
- add secrets;
- expose credentials;
- grant broad GitHub permissions;
- stage unrelated user work;
- stage generated build outputs;
- use destructive Git cleanup;
- rewrite shared history;
- force push;
- bypass branch protection;
- self-approve;
- fabricate review/check results;
- merge unless explicitly authorized;
- create release tags;
- publish GitHub Releases;
- close Release 0.8 unless explicitly authorized;
- begin WP15;
- begin Release 0.9.

---

# 26. Completion Model

```text
Read Authority
      ↓
Record Local + Remote State
      ↓
Revalidate WP13 Baseline
      ↓
Discover GitHub Governance
      ↓
Establish WP14 Traceability
      ↓
Resolve Branch Strategy
      ↓
Classify Release 0.8 Delta
      ↓
CI Decision Gate
      ↓
Implement Only Authorized Integration
      ↓
Run Local Quality Gates
      ↓
Inspect + Stage Exact Scope
      ↓
Commit if Authorized
      ↓
Push if Authorized
      ↓
Create/Prepare PR if Authorized
      ↓
Record Hosted Check State
      ↓
Final Technical Validation
      ↓
Record Final Local + Remote State
      ↓
GitHub Integration Readiness
      ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 27. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 14 — GitHub Integration** against the actual current `AIQuantTradingResearch` repository.

WP13 has already established that the technical Solution Skeleton is valid.

WP14 must preserve that baseline while integrating the Release 0.8 work through the repository's governed GitHub workflow.

Read repository authority before performing Git or GitHub mutations.

Determine the actual:

```text
issue convention
milestone convention
branch strategy
commit convention
PR convention
review model
CI requirement
```

Do not invent missing governance.

Protect all pre-existing user work.

Classify the complete Release 0.8 change set before staging.

Resolve the GitHub Actions decision from explicit authority. The absence of a workflow is not by itself authorization to create one.

Run the canonical local verification before integration.

If staging/commit/push/PR operations are authorized:

```text
stage exact scope
inspect staged diff
commit using repository convention
push safely without force
create/prepare PR using repository convention
record actual hosted-check state
```

Do not claim remote operations or checks that did not occur.

Do not merge, tag, publish a release, close the milestone, begin WP15, or begin Release 0.9 unless separately authorized.

Return the complete **GitHub Integration Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

State whether Release 0.8 is integrated sufficiently to proceed to formal acceptance review.

Identify the next authoritative step exactly from the execution plan.

Do not begin it.

---

# Conclusion

Work Package 14 connects the validated Release 0.8 engineering baseline to the project's governed GitHub collaboration model.

The transition is:

```text
Technically Validated Skeleton
        ↓
Traceable GitHub Work
        ↓
Controlled Branch
        ↓
Exact Change Set
        ↓
Quality-Gate Evidence
        ↓
Controlled Commit
        ↓
Safe Push
        ↓
Review-Ready Pull Request
        ↓
Formal Release Acceptance Review
```

WP13 proved that the repository works.

WP14 must prove that the work can move through the project's collaboration and review system without losing scope control, architectural integrity, validation evidence, or traceability.

GitHub integration is therefore not merely a remote push. It is the governed bridge between implementation and formal release acceptance.

> **A technically correct change becomes an engineering deliverable only when its scope, evidence, history, and review path are equally controlled.**

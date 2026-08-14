Execute Phase 2 — Release 0.8, Work Package 14 — GitHub Integration.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/14-github-integration-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the current AIQuantTradingResearch repository state.

Important:

- Follow the authority hierarchy, GitHub integration principles, governance discovery, traceability requirements, branch rules, CI decision gate, staging contract, commit contract, push contract, pull-request contract, acceptance criteria, and output contract defined in the authoritative prompt.
- Revalidate the accepted WP13 technical baseline before performing Git or GitHub mutations.
- Preserve:
  - exactly 8 solution projects
  - 4 production projects
  - 4 test projects
  - /src/ and /tests/ solution organization
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
  - zero production dependency cycles
  - Architecture.Tests = 7/7
  - canonical verify = PASS
- Record the initial repository root, branch, HEAD, working tree, remotes, configured/effective SDK, GitHub CLI availability, and GitHub authentication state.
- Never expose GitHub credentials, tokens, secrets, or sensitive authentication information.
- Inspect repository GitHub governance before deciding how to integrate.
- Determine from repository authority:
  - issue convention
  - milestone convention
  - labels
  - branch naming convention
  - commit convention
  - pull-request template/convention
  - review expectations
  - existing GitHub Actions
  - GitHub administration scripts
- Do not invent missing governance.
- Establish explicit traceability between WP14 and Phase 2 — Release 0.8.
- Reuse the authoritative WP14 issue/milestone relationship if it already exists.
- Do not create duplicate issues or milestones.
- Protect all pre-existing working-tree changes.
- Classify pre-existing changes as:
  RELEASE-0.8 INTENDED
  WP14 AUTHORITY
  UNRELATED USER WORK
  GENERATED
  AMBIGUOUS
- Do not use destructive Git commands such as git clean, git reset, git restore, or checkout operations that could remove user work.
- Determine the approved branch strategy from repository governance.
- Do not invent a branch name when repository authority defines one.
- Do not switch branches if doing so would endanger unrelated uncommitted work.
- Do not merge into the default branch unless the authoritative WP14 scope explicitly permits it.
- Inspect and classify the complete intended Release 0.8 delta before staging anything.
- Build an explicit inclusion/exclusion plan.
- Never assume every modified or untracked file belongs in the Release 0.8 integration.
- Do not stage generated outputs or unrelated user work.

Apply the GitHub Actions / CI decision gate exactly:

1. Determine whether authoritative WP14 scope requires CI.
2. If CI is not required, do not create a workflow merely because .github/workflows is empty.
3. If CI is explicitly required, determine whether repository governance defines the required workflow.
4. If the required CI behavior is undefined, do not invent CI architecture; report the ambiguity according to the prompt.
5. If CI is authorized and sufficiently defined, implement only the minimum approved workflow.
6. Prefer delegation to repository-owned engineering scripts instead of duplicating restore/build/test logic.
7. Do not add deployment, publishing, Docker push, cloud credentials, release automation, market-data jobs, future-product functionality, or unrelated quality systems.
8. Do not add secrets or unnecessary GitHub permissions.
9. Never claim GitHub-hosted CI passed unless it actually ran and passed.

Before staging any Release 0.8 integration changes:

- Run the canonical local verification workflow.
- Require verify = PASS.
- Require Architecture.Tests = 7/7.
- Require zero build errors.
- Validate build.sh when appropriate and supported.
- Confirm the production dependency graph remains unchanged and acyclic.

If staging is authorized:

- Stage only the explicitly approved Release 0.8 integration files.
- Inspect git status --short.
- Inspect git diff --cached --stat.
- Inspect the complete staged diff.
- Confirm:
  - no unrelated files
  - no generated outputs
  - no secrets or credentials
  - no machine-specific paths
  - no unexpected binary artifacts
- Do not proceed to commit unless the staged set is exact and explainable.

If commit is authorized:

- Follow the repository-defined commit convention.
- Commit only the approved Release 0.8 scope.
- Record the resulting commit hash, subject, and committed files.
- Do not rewrite shared history.
- Do not amend unrelated historical commits.

If push is authorized:

- Verify remote identity, branch, commit, and upstream behavior first.
- Push only the approved integration branch.
- Never force push.
- Do not push directly to the default branch unless explicitly authorized.

If pull-request creation is authorized:

- Follow the repository PR template and conventions.
- Accurately describe:
  - Release 0.8 objective
  - Solution Skeleton scope
  - 8-project structure
  - architecture boundaries
  - Architecture.Tests 7/7
  - canonical verification result
  - documentation alignment
  - GitHub/CI changes, if any
  - known non-blocking environmental observations
  - explicit out-of-scope items
  - handoff to WP15 — Release Acceptance Review
- Do not fabricate hosted checks, approvals, review status, or GitHub state.
- Do not merge the PR unless the authoritative WP14 scope explicitly authorizes it.
- Do not self-approve or bypass required review.

Do not:

- redesign architecture
- change production behavior
- change project references
- add future Release 0.9 functionality
- implement plugin infrastructure
- add market data, storage, pipelines, analytics, AI/ML, or MLOps
- invent CI requirements
- expose credentials
- add unnecessary permissions
- stage unrelated work
- force push
- bypass branch protection
- fabricate GitHub results
- create release tags
- publish a GitHub Release
- close the Release 0.8 milestone unless explicitly authorized
- begin WP15
- begin Release 0.9

At the end:

- Revalidate the Release 0.8 technical baseline.
- Confirm exactly 8 projects remain.
- Confirm the production dependency graph remains unchanged.
- Confirm zero cycles.
- Confirm Architecture.Tests remains 7/7.
- Confirm canonical verify remains PASS.
- Record the final branch, HEAD, Git status, staged state, uncommitted state, and verified upstream/remote state.
- Record only GitHub remote facts that were actually observed.
- Produce the complete GitHub Integration Execution Report required by the authoritative prompt.

Finish with exactly one evidence-based decision:

COMPLETE
COMPLETE WITH ACTIONS
BLOCKED

Use COMPLETE WITH ACTIONS only for genuinely non-blocking external/manual actions, such as required reviewer approval or hosted checks still running.

Do not use COMPLETE WITH ACTIONS to hide a mandatory WP14 failure.

State whether Release 0.8 is integrated sufficiently to proceed to formal release acceptance review.

Finally, read:

docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md

Identify the next authoritative step exactly from that document.

The expected next work package is:

15 — Release Acceptance Review

Confirm it from the execution plan rather than assuming it.

Do not begin WP15.

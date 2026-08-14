Execute Phase 2 — Release 0.8, Work Package 15 — Release Acceptance Review.

Use the following repository file as the authoritative execution prompt:

docs/roadmap/release-0.8/prompts/15-release-acceptance-review-codex-prompt.md

Read that prompt completely before taking any action.

Execute it against the actual current AIQuantTradingResearch repository and GitHub state.

Important:

- WP15 is the final Release 0.8 acceptance review.
- This is a review-first work package.
- Do not modify implementation merely to make Release 0.8 pass.
- The default WP15 repository change set is NONE.
- Follow the authority hierarchy, acceptance principles, technical acceptance contract, GitHub acceptance requirements, severity model, acceptance criteria, decision model, and output contract defined in the authoritative prompt.

Begin by recording and verifying:

- repository root
- current branch
- HEAD
- git status
- origin/remotes
- configured SDK
- effective SDK
- GitHub CLI authentication state
- synchronization between local main and origin/main

Expected starting state:

- branch = main
- main synchronized with origin/main
- working tree clean
- WP14 integration merged

Verify the actual state rather than assuming it.

Read completely:

- docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
- docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
- relevant current-state architecture, implementation, testing, engineering, and GitHub governance documentation

Review WP01 through WP14 and build the complete work-package acceptance matrix.

Expected work packages:

01 — Repository Preflight
02 — Root Solution
03 — Production Projects
04 — Project References
05 — Root Build Configuration
06 — Minimal Worker Host
07 — Dependency Registration
08 — Test Projects
09 — Architecture Tests
10 — Solution Organization
11 — Engineering Scripts Integration
12 — Documentation Alignment
13 — Full Skeleton Validation
14 — GitHub Integration

Classify each using only evidence:

COMPLETE
COMPLETE WITH RESOLVED ACTIONS
INCOMPLETE
BLOCKED
NOT APPLICABLE

Do not rely only on previous conversation history or prior reports.
Validate against the actual repository and GitHub state.

Perform the complete Release 0.8 technical acceptance review.

Validate AIQuantTradingResearch.slnx:

- parses successfully
- exactly 8 projects
- exactly 4 production projects
- exactly 4 test projects
- correct /src/ and /tests/ solution organization
- no missing projects
- no unexpected projects
- no duplicate projects

Expected production projects:

- AIQuantTradingResearch.Domain
- AIQuantTradingResearch.Application
- AIQuantTradingResearch.Infrastructure
- AIQuantTradingResearch.Worker

Expected test projects:

- AIQuantTradingResearch.Domain.Tests
- AIQuantTradingResearch.Application.Tests
- AIQuantTradingResearch.Infrastructure.Tests
- AIQuantTradingResearch.Architecture.Tests

Validate the production dependency graph exactly:

Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure

Require:

- zero unexpected production edges
- zero production dependency cycles

Validate that Architecture.Tests still enforce:

- Domain !→ Application
- Domain !→ Infrastructure
- Domain !→ Worker
- Application !→ Infrastructure
- Application !→ Worker
- Infrastructure !→ Worker
- production graph is acyclic

Execute Architecture.Tests directly.

Require exactly:

Discovered = 7
Passed = 7
Failed = 0

Validate the root build/toolchain configuration:

- global.json
- Directory.Build.props
- Directory.Packages.props
- .editorconfig
- accepted SDK
- effective SDK
- net10.0 target framework
- nullable configuration
- implicit usings
- central package management
- analyzer/warning policy

Treat environmental warnings separately from repository defects.

In particular:

- NU1900 caused by vulnerability-feed/network connectivity is non-blocking when restore/build/test succeed.
- Do not disable NuGet auditing merely to eliminate the warning.

Validate the Worker remains the minimal composition root.

Expected lifecycle:

Host.CreateApplicationBuilder
        ↓
AddApplication
        ↓
AddInfrastructure
        ↓
Build
        ↓
RunAsync

Confirm:

- Application registration boundary exists
- Infrastructure registration boundary exists
- intentionally empty registration methods remain acceptable for Release 0.8
- no speculative hosted services exist
- no product functionality has been introduced

Validate the four test-project skeletons.

Do not require artificial placeholder tests in:

- Domain.Tests
- Application.Tests
- Infrastructure.Tests

Their empty state is acceptable when consistent with Release 0.8 authority.

Validate the engineering scripts:

eng/restore.ps1
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1

Confirm canonical orchestration remains:

restore
→ format verification
→ build
→ test

Run:

powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1

Require:

- exit status 0
- zero build errors
- Architecture.Tests 7/7

Run eng/build.sh when the current environment safely supports it.

Require PASS when supported.
If the environment cannot execute it, report the environmental limitation accurately rather than modifying the repository.

Review the clean-reconstruction evidence.

Use WP13 evidence and/or rerun the approved validation sequence when necessary:

verify
→ clean
→ restore
→ format verification
→ build
→ test
→ verify

Do not use destructive Git cleanup.

Confirm clean/reconstruction preserves:

- tracked source
- documentation
- user files
- repository governance files

Validate current-state documentation against the implementation.

Current-state documentation must accurately represent:

- AIQuantTradingResearch.slnx
- exactly 8 projects
- /src/ and /tests/
- accepted dependency graph
- minimal Worker
- Application and Infrastructure registration boundaries
- four test projects
- seven architecture rules/tests
- engineering scripts
- canonical verify workflow
- distinction between implemented Release 0.8 scope and planned future capabilities

Do not rewrite historical execution prompts merely because they describe earlier repository states.

Historical artifacts may remain historical.

Validate that obsolete current-state implementation does not exist for:

- AIQuantTradingResearch.Api
- AIQuantTradingResearch.SharedKernel
- obsolete .sln structures
- obsolete project references

Distinguish:
CURRENT-STATE
HISTORICAL
PLANNED
UNRELATED

Do not treat legitimate historical documentation as a current-state defect.

Perform the GitHub acceptance review using authenticated GitHub access.

Verify actual remote state for:

- repository identity
- default branch
- origin/main
- milestone #39
- WP14 issue #65
- PR #67
- Release 0.8 issues
- open/closed milestone state

Expected milestone:

#39 — Phase 2 - Release 0.8: Solution Skeleton

Expected WP14 issue:

#65 — [Feature]: 14 — GitHub Integration

Expected integration PR:

#67

Verify rather than assume:

- PR #67 exists
- PR #67 is merged
- WP14 governance artifacts are now on main
- local main matches origin/main
- no Git history rewrite was used during WP14 recovery

The following file is intentionally preserved as part of repository history:

docs/roadmap/release-0.8/prompts/14-github-integration-codex-prompt-chat-02.md

Do NOT classify its presence as an accidental or unwanted artifact merely because an earlier WP14 execution had initially treated it as unrelated.

Its preservation was an explicit post-WP14 decision and it is now intentionally part of the repository.

Inspect milestone #39 and all Release 0.8 issues.

Determine:

- total issues
- open issues
- closed issues
- WP14 state
- WP15 state if a WP15 issue exists
- any other remaining Release 0.8 issues

Do not automatically close issues during the review unless the authoritative WP15 contract explicitly authorizes that action.

If an issue remains open only because this acceptance review is still executing, classify that correctly.

If another substantive Release 0.8 issue remains open, determine whether it actually blocks release closure.

Validate Release 0.8 scope boundaries.

Confirm Release 0.8 has NOT prematurely implemented:

- Release 0.9 CI framework
- plugin infrastructure
- market-data providers
- storage engines
- pipelines
- trading/backtesting functionality
- AI/ML functionality
- MLOps
- cloud deployment
- future production capabilities

Planned documentation for future capabilities is acceptable.

Do not reject Release 0.8 because Release 0.9 or later capabilities are intentionally absent.

Do not create GitHub Actions.
CI belongs to later release authority.

At the end, inspect repository cleanliness:

git branch --show-current
git rev-parse HEAD
git status --short
git diff -- .
git diff --cached -- .

WP15 should introduce zero implementation changes.

Do not:

- modify production code
- modify tests
- modify architecture tests
- modify project files
- modify AIQuantTradingResearch.slnx
- modify engineering scripts
- modify documentation
- modify build configuration
- add packages
- create CI
- implement Release 0.9 functionality
- stage files
- commit
- push
- rewrite history
- force push
- create release tags
- publish a GitHub Release
- close milestone #39 unless explicitly authorized by WP15 authority
- begin Release 0.9

Classify every finding as:

BLOCKER
REQUIRED ACTION
RISK
OBSERVATION

A BLOCKER means Release 0.8 cannot be accepted.

A REQUIRED ACTION may represent an administrative/governance closure step after technical acceptance, such as:

- closing the WP15 issue
- closing milestone #39
- recording final release status

Do not treat these administrative closure actions as technical failures.

Produce the complete Release 0.8 Acceptance Review Report required by the authoritative prompt.

The report must include:

- Executive Summary
- Execution Context
- Authoritative Sources Reviewed
- WP01–WP14 Completion Matrix
- Manifest Acceptance
- Solution Acceptance
- Dependency Architecture Acceptance
- Build / Toolchain Acceptance
- Worker / DI Acceptance
- Test Acceptance
- Architecture Test Acceptance
- Engineering Workflow Acceptance
- Clean Reconstruction Acceptance
- Documentation Acceptance
- GitHub Integration Acceptance
- Remaining Release 0.8 Issues
- Scope Boundary Acceptance
- Repository Cleanliness
- Environmental Observations
- Findings
- Acceptance Criteria Matrix
- Release Acceptance Decision
- Required Closure Actions
- Release 0.8 Closure Readiness
- Next Authoritative Step

Finish with exactly one release-level decision:

ACCEPTED
ACCEPTED WITH ACTIONS
REJECTED

Use ACCEPTED only when all mandatory technical and governance criteria pass and no required closure action remains.

Use ACCEPTED WITH ACTIONS when:

- the technical Release 0.8 acceptance passes
- no blocker exists
- only explicit administrative/governance closure actions remain

Use REJECTED when any mandatory Release 0.8 acceptance criterion fails.

Do not use ACCEPTED WITH ACTIONS to hide a technical failure.

Finally, read:

docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md

Identify the exact next authoritative step after WP15.

It may be:

- Release 0.8 closure
- milestone closure
- final release-status recording
- transition preparation for Release 0.9

Determine it from repository authority rather than memory.

Do not perform that next step.

Do not begin Release 0.9.

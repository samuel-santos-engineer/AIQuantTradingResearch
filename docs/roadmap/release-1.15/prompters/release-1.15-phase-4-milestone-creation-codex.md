# Release 1.15 — Phase 4 GitHub Milestone Creation (Codex Prompt)

## Purpose
Create and verify the GitHub milestone for the already-merged Release 1.15 planning package and attach all eight existing work-package issues. This is GitHub project-management administration only, **not** implementation, deployment or approval to close any WP.

## Repository and required exact milestone title
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- Milestone title (copy verbatim): **Phase 4 - Release 1.15 Definition — Historical Crypto Chart and Research Interface**
- Milestone description: `Release 1.15 historical BTC/USD and ETH/USD educational research/chart interface. Tracks WP01–WP08 (issues #367–#374), Docker-first supervised dual-process runtime, private ingestion/SQLite/research, transformed public snapshot API, Streamlit chart UX, Linux/Azure F1 qualification and release acceptance. Planning authority: merged PR #375 and docs/roadmap/release-1.15/. No licensing-specific engineering gate, provider activation or deployment authorization.`
- Milestone state: `open`
- Due date: **unset**; do not invent a deadline.

## Prerequisites and authoritative references
1. Confirm repository and authenticated GitHub identity, with permission to manage milestones and issues.
2. Verify PR #375 is merged and the roadmap exists on `main`:
   - `docs/roadmap/release-1.15/RELEASE_1.15_DEFINITION.md`
   - `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`
   - `docs/roadmap/release-1.15/RELEASE_1.15_ISSUE_REGISTER.md`
3. Read `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md`, `docs/governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md` and root `AGENTS.md`.

## Required execution
Use GitHub CLI (`gh api`) or the GitHub REST API with the authenticated repository context. Never print or commit credentials.

1. **Idempotent lookup:** `GET /repos/samuel-santos-engineer/AIQuantTradingResearch/milestones?state=all&per_page=100` (paginate if needed). Compare exact milestone titles. If exactly one exists, reuse its `number`; if none exists, `POST /repos/samuel-santos-engineer/AIQuantTradingResearch/milestones` with the exact title, description and `state=open`. If multiple exact matches exist, STOP and report ambiguity; do not create another.
2. Confirm the resulting milestone's `number`, `html_url`, `title`, `state` and `description`. If an existing exact-title milestone has a different description, report it rather than overwriting unrelated metadata. If closed, STOP and request owner direction rather than silently reopen.
3. Confirm each of the following is an **issue** in this repository, not a pull request; verify the title and that it belongs to Release 1.15:
   - #367 — WP01 Baseline, Architecture Contract and Feasibility
   - #368 — WP02 Docker Dual-Process Runtime and Isolation
   - #369 — WP03 Historical Ingestion, SQLite and Recovery
   - #370 — WP04 Private Research Indicators and Regime Engine
   - #371 — WP05 Transformed Snapshot API and Cross-Language Contract
   - #372 — WP06 Streamlit Historical Candlestick Research UI
   - #373 — WP07 Integrated Linux Qualification and Azure F1 Readiness
   - #374 — WP08 Release Acceptance, Runbook and Handoff
4. For each issue, inspect its existing milestone. If already assigned to the target milestone, leave it unchanged. If assigned to another milestone, STOP and request owner direction; do not silently move it. Otherwise `PATCH /repos/samuel-santos-engineer/AIQuantTradingResearch/issues/{issue_number}` with `{"milestone": TARGET_MILESTONE_NUMBER}`.
5. Re-read all eight issues independently and verify each `milestone.number` equals the target. Re-read the milestone and confirm `open_issues + closed_issues` reflects the eight assigned issues (additional preexisting issues, if any, must be reported, not silently removed).
6. Report milestone URL/number/title, issue assignment table with verified links and states, any unexpected preexisting assignments or extra issues, and whether all eight are verified.

## Safety and boundaries
- Do **not** create duplicate issues, close/reopen issues, merge PRs, alter labels/assignees, change project taxonomy, invent deadlines, run provider calls, mutate implementation files or deploy Azure.
- This task is explicitly authorized to create the milestone and assign the eight listed issues only, subject to the conflict checks above.
- If the CLI/API lacks permissions or authentication, STOP with the exact missing permission/action; do not fabricate success.
- Do not create a milestone-creation approval gate or a licensing-specific engineering gate.
- Any generated/modified text file MUST use UTF-8 LF (`\n`) exclusively, including PowerShell. Respect `.gitattributes`, run `git diff --check` and independently verify no CRLF in changed text. Do not normalize unrelated historical files.
- No new repository files or PR are required to perform the GitHub milestone operation. This prompt is the only documentation artifact for the operation.

## Completion criteria
One open milestone with the **exact** title, all eight original issues #367–#374 attached, independent verification and a concise evidence report. Never claim success until GitHub confirms it.

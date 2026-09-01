# GPT-5.6 Terra — Release 1.12 Three Planning Artifacts Git/PR Publication Authority

**Selected execution model: GPT-5.6 Terra**

## Model authority
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, planning.
- **GPT-5.6 Terra** — PRIMARY for repository validation, selective staging, Git/GitHub publication, and PR verification.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna/Terra.

## Mission
Publish exactly these three frozen Release 1.12 planning artifacts through one clean Git/PR boundary:

1. `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
2. `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

This authority prevents WP02 implementation from silently absorbing unpublished governance artifacts.

## Required predecessor state
Freshly reconcile:
- local `main` and `origin/main` (expected current baseline `20a8fccd6e7a5b895e717f946f4501edd7ab8ffa`);
- WP01 #260 Closed/Done;
- milestone #63 Open with 7 open / 1 closed;
- #261 WP02 Open/Todo and dependency-ready;
- the three exact artifacts exist locally and are absent from `origin/main`;
- no unrelated Release 1.12 implementation is included.

If any material premise differs, BLOCK rather than silently adapting.

## Exact publication scope
The PR must contain exactly 3 changed paths, all Markdown, exactly equal to the three-path manifest above.

Expected:
- total = 3
- unique = 3
- Markdown = 3
- PowerShell = 0
- production/source/config/package/schema = 0

No count-padding, opportunistic cleanup, or WP02 implementation.

## Governance invariants
Preserve:
- Product Release 1.11 abandoned/nonexistent;
- sequence `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`;
- milestone #63 Open;
- #260 Closed/Done;
- #261 Open/Todo;
- #262–#267 dependency-gated;
- Project #2 Release option `1.12`;
- Initiative-1.11 milestone #62 and #252–#257 unchanged;
- Release 1.10 history unchanged.

No WP, Project, milestone, tag, or release lifecycle mutation is authorized.

## Content validation
Before staging:
- verify exact file existence/path identity;
- run `git diff --check`;
- run established Gitleaks over relevant unpublished content/diff;
- verify no credentials, `.env`, auth caches, private keys/certs, Azure/GitHub/Docker/provider auth material;
- verify no Product Release 1.11 resurrection;
- verify no Release 2.0 scope contamination;
- verify no contradictory WP/milestone references;
- verify mutual consistency across definition/plan/manifest.

`invalid-wp04-probe-key` remains classified only as historical synthetic invalid-auth test data; this does not weaken scanning for any other secret-like value.

## Fresh base reconciliation
Repository:
`C:\projects\github\AIQuantTradingResearch`

Required preflight:
- verify remote URL;
- verify current branch;
- `git fetch origin --prune`;
- verify `main` vs `origin/main`;
- verify HEAD;
- verify staged area empty;
- enumerate modified/untracked paths;
- prove only intended governance paths are in scope.

If `origin/main` advanced, reconcile against the new base before publication.

## Deterministic manifest and selective staging
Use an exact deterministic manifest containing only the three governed paths.

Forbidden:
- `git add .`
- `git add -A`
- broad directory staging.

After selective staging prove:
- staged total 3;
- unique 3;
- Markdown 3;
- no non-Markdown paths;
- exact manifest equality.

On mismatch, safely unstage and BLOCK.

## Branch
Preferred:
`docs/release-1.12-planning-artifacts`

If it conflicts with existing work, choose one narrow equivalent and report it. Never overwrite unrelated history.

## Cached diff gate
Inspect:
- `git diff --cached --name-only`
- `git diff --cached --name-status`
- `git diff --cached --stat`
- `git diff --cached`
- `git diff --cached --check`

Confirm governance/docs-only scope.

## Commit
Preferred subject:
`Docs: publish Release 1.12 planning artifacts`

Expected commit count: 1.

Verify commit parent equals reconciled base and changed set is exactly the 3-path manifest.

## Push
Push the fresh branch without force. Verify remote branch head equals local publication commit. Expected push mutation: 1.

## PR creation
Create exactly one non-draft PR to `main`.

Preferred title:
`Docs: publish Release 1.12 planning artifacts`

PR body should state:
- Release 1.12 planning/governance is frozen;
- WP01 #260 is Closed/Done;
- payload is exactly 3 governance artifacts;
- no implementation/Azure/Docker/provider/package/schema mutation;
- #261 WP02 remains next implementation boundary;
- Product Release 1.11 remains abandoned and Release 2.0 separate.

Expected PR creation mutation: 1.

## Post-create verification
Verify:
- PR number;
- OPEN;
- non-draft;
- base `main`;
- correct head branch;
- head SHA equals publication commit;
- exactly 3 changed paths;
- exact path-set equality;
- Markdown = 3;
- no extra files;
- no lifecycle/Project/milestone/tag/release mutation.

If GitHub file enumeration is incomplete, use authoritative Git base/head diff and report the discrepancy explicitly.

## Merge boundary
**Merge is NOT authorized by this authority.**

Required:
`RELEASE 1.12 — THREE-ARTIFACT PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

If manually merged outside this authority, record it later as an externally performed GitHub merge mutation and perform separate post-merge verification. Never rewrite history to imply Terra authorized the merge.

## Mutation accounting
Report exact counts for:
- repository file mutations caused by this authority;
- temp files;
- branch creation;
- staging/index mutation;
- commits;
- pushes;
- PR creation;
- issue/Project/milestone mutations;
- tag/release mutations;
- Azure;
- Docker/GHCR;
- provider calls;
- package/schema/production mutations.

Expected protected-domain mutations: all 0.

## Interactive handoff
For authenticated Windows execution, provide one bounded batch at a time with:
1. purpose;
2. exact copy/paste PowerShell;
3. classification;
4. expected mutations;
5. exact stdout/stderr/exit codes to return;
6. STOP and wait.

Never infer success or weaken security controls.

## Acceptance gates
A. Fresh base reconciled.
B. Exact three-path scope proven.
C. Content/secret/governance validation passes.
D. Staging exact 3/3.
E. One commit exact 3/3.
F. Push head verified.
G. One non-draft PR exact 3/3.
H. Protected governance/infrastructure unchanged.
I. Mutation audit exact.

## Required success markers
`RELEASE 1.12 — THREE PLANNING ARTIFACTS BASE RECONCILIATION: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS CONTENT VALIDATION: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS STAGING 3/3: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS COMMIT 3/3: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS PUSH: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS PR 3/3 POST-CREATE VERIFICATION: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 — THREE PLANNING ARTIFACTS GIT/PR PUBLICATION: PASS`

`RELEASE 1.12 — THREE-ARTIFACT PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

Terminal:
`RELEASE 1.12 — THREE PLANNING ARTIFACTS GIT/PR PUBLICATION AUTHORITY COMPLETE`

Blocked:
`RELEASE 1.12 — THREE PLANNING ARTIFACTS GIT/PR PUBLICATION AUTHORITY BLOCKED`

## Completion boundary
Completes only after one exact three-file publication PR exists and its payload is independently verified.

Does not authorize PR merge or WP02 implementation. WP02 remains a separate GPT-5.6 Terra implementation authority.

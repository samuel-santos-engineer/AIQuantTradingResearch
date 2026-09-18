# GPT-5.6 Terra — Release 1.12 WP03 PR #272 Merge, Post-Merge Verification & Lifecycle Authority

**Selected execution model: GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, path designation, governance, acceptance criteria.
- **GPT-5.6 Terra** — PRIMARY: approved merge, post-merge validation execution, Git/GitHub lifecycle mutations, and bounded verification.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna or Terra.

## 1. Mission

Complete Release 1.12 WP03 after accepted implementation PR:

**PR #272 — Release 1.12 WP03 GHCR Publication & Azure F1 Deployment Automation**

WP03 issue:

`#262 — GHCR Publication & Azure F1 Deployment Automation`

This authority may:
- reconcile PR #272;
- verify the exact 4/4 payload;
- merge PR #272;
- synchronize `main`;
- perform post-merge repository validation;
- verify the previously-qualified GHCR/Azure boundary without widening WP03;
- close #262 only after exact post-merge acceptance;
- ensure Project #2 Status is Done;
- preserve milestone #63 as Open.

No new implementation edits are authorized.

## 2. Expected pre-merge state

Expected canonical base before PR #272 merge:

`a3fd52b96e5479e4d11824cfad46bd63d0435811`

Expected PR head:

`543f419bffd4c534d7ced2d03e9f502b3b8e9aa9`

Expected PR:
- #272
- state OPEN
- non-draft
- base `main`
- exact four CREATE paths
- WP03 implementation acceptance already PASS.

Fresh evidence controls.

## 3. Binding exact payload

PR #272 must contain exactly:

```text
eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/cleanup-f1-reference.ps1
eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1
eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/publish-ghcr-image.ps1
eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/verify-f1-reference.ps1
```

Expected:
- total paths: 4
- creates: 4
- modifies/deletes/renames: 0

Required:

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE PAYLOAD 4/4: PASS`

If any path differs, STOP.

## 4. Pre-merge reconciliation

Before merge prove:
- `origin/main` expected/reconciled;
- PR #272 is open and non-draft;
- PR base is `main`;
- head SHA is expected;
- mergeability is acceptable;
- no unexpected new commits;
- authoritative base..head Git comparison is exactly 4/4;
- all four paths satisfy Luna's allowlist;
- no denylisted/historical Initiative-1.11 path is touched;
- #262 is Open/Todo;
- milestone #63 remains Open.

Required:

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE RECONCILIATION: PASS`

## 5. Pre-merge static validation

Re-run against PR head:
- PowerShell parse/syntax validation for all four scripts;
- `git diff --check`;
- Gitleaks/secret screening;
- hard-coded secret/credential inspection;
- no package/schema/application changes;
- no WP04/WP05/WP06 bypass;
- no paid dependency introduced by repository content.

Required:

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE VALIDATION: PASS`

## 6. Merge authorization

If all pre-merge gates pass, merge PR #272 exactly once.

Record:
- merge method;
- merged timestamp;
- merge SHA;
- first parent;
- second parent/head where applicable.

Required:

`RELEASE 1.12 WP03 PR #272 — MERGE: PASS`

Do not author an additional implementation commit.

## 7. Post-merge main synchronization

After merge:
- fetch;
- safely switch/synchronize local `main`;
- prove local `main` = `origin/main`;
- prove ahead/behind `0/0`;
- preserve unrelated requester-only/local files;
- staging must remain empty.

Do not claim globally clean working tree if intentional untracked requester-only files remain.

Required:

`RELEASE 1.12 WP03 PR #272 — POST-MERGE MAIN SYNCHRONIZATION: PASS`

## 8. Authoritative merged-payload proof

Use Git parent..merge comparison.

Prove:
- exact four merged paths;
- 4/4 creates;
- no unauthorized paths;
- head path set = merge path set;
- all four files tracked on merged `main`.

Required:

`RELEASE 1.12 WP03 PR #272 — MERGED PAYLOAD 4/4: PASS`

`RELEASE 1.12 WP03 PR #272 — HEAD/MERGE PATH SET EQUALITY: PASS`

## 9. Post-merge repository validation

On merged `main`, re-run:
- PowerShell syntax/parse checks;
- whitespace checks;
- Gitleaks;
- exact-path checks;
- no architecture bypass;
- no WP04/WP05/WP06 implementation leakage;
- no package/schema changes.

Required:

`RELEASE 1.12 WP03 PR #272 — POST-MERGE VALIDATION: PASS`

`RELEASE 1.12 WP03 PR #272 — ARCHITECTURE & NO-BYPASS: PASS`

## 10. External qualification preservation/readback

Previously accepted WP03 evidence includes:

GHCR immutable public image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:51889b708ce5ab251bb8174e94c56bf8ddae06a716f169d2d912de05c09d2c65`

Qualified Azure boundary:
- West Central US
- Linux App Service
- F1/Free
- public GHCR image
- HTTPS
- persistent `/home`
- port 8501
- no ACR
- no WP04 SQLite implementation
- no WP05 secret/provider implementation
- no WP06 health implementation.

Do not repeat destructive qualification merely because the PR merged.

Perform read-only external readback where retained resources/evidence still exist and where authenticated operator context is required.

For interactive commands:
- provide exact copy/paste commands;
- classify them as read-only;
- state expected evidence/exit codes;
- STOP for returned stdout/stderr;
- never request secrets.

If Azure qualification resources were already cleaned up under the implementation authority, verify absence rather than recreate them.

Required:

`RELEASE 1.12 WP03 — POST-MERGE EXTERNAL QUALIFICATION PRESERVED: PASS`

The GHCR digest/public-readback evidence may satisfy retained image verification if independently read back.

## 11. Post-merge cleanliness

Prove:
- no authority-owned staged/unstaged implementation residue;
- the four WP03 files are tracked;
- requester-only/local untracked files remain preserved if still present;
- no generated logs/secrets/evidence were accidentally added.

Required:

`RELEASE 1.12 WP03 PR #272 — POST-MERGE CLEANLINESS: PASS`

## 12. WP03 exact acceptance

Only after Sections 3–11 pass, reaffirm:

`RELEASE 1.12 WP03 — GHCR PUBLICATION & AZURE F1 DEPLOYMENT AUTOMATION: PASS`

This is the gate for lifecycle closure.

## 13. GitHub lifecycle completion

After exact WP03 acceptance:

1. close GitHub issue #262;
2. verify Project #2 Status becomes `Done`;
3. if closing the issue automatically changes Project Status to Done, do not make a redundant explicit Status mutation;
4. only if automation does not set Done, explicitly set the Project item to Done;
5. verify Release assignment remains `1.12`;
6. verify milestone #63 remains Open;
7. verify WP04 #263 remains Open/Todo.

Required:

`RELEASE 1.12 WP03 — GITHUB LIFECYCLE: CLOSED/DONE`

After closure, expected milestone counts:

- milestone #63 Open
- 5 open / 3 closed

Fresh GitHub evidence controls.

## 14. WP04 readiness

After WP03 is Closed/Done and all post-merge gates pass:

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY: READY`

Do not implement WP04 under this authority.

## 15. Mutation audit

Report exact counts.

Expected authority-owned Git/GitHub mutations:
- PR merges: 1
- fetches: exact actual
- local-main synchronization: 1 if required
- issue closures: 1
- explicit Project Status mutations: 0 if automation sets Done, otherwise 1
- milestone mutations: 0
- authored repository edits: 0
- authored commits: 0
- pushes: 0
- new PRs: 0
- tags/releases: 0

External:
- Azure mutations: 0
- Docker mutations: 0
- GHCR mutations: 0
- provider mutations: 0
- package mutations: 0
- schema mutations: 0

Read-only external queries are not mutations.

## 16. Required markers

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE RECONCILIATION: PASS`

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE PAYLOAD 4/4: PASS`

`RELEASE 1.12 WP03 PR #272 — PRE-MERGE VALIDATION: PASS`

`RELEASE 1.12 WP03 PR #272 — MERGE: PASS`

`RELEASE 1.12 WP03 PR #272 — POST-MERGE MAIN SYNCHRONIZATION: PASS`

`RELEASE 1.12 WP03 PR #272 — MERGED PAYLOAD 4/4: PASS`

`RELEASE 1.12 WP03 PR #272 — HEAD/MERGE PATH SET EQUALITY: PASS`

`RELEASE 1.12 WP03 PR #272 — POST-MERGE VALIDATION: PASS`

`RELEASE 1.12 WP03 PR #272 — ARCHITECTURE & NO-BYPASS: PASS`

`RELEASE 1.12 WP03 — POST-MERGE EXTERNAL QUALIFICATION PRESERVED: PASS`

`RELEASE 1.12 WP03 PR #272 — POST-MERGE CLEANLINESS: PASS`

`RELEASE 1.12 WP03 — GHCR PUBLICATION & AZURE F1 DEPLOYMENT AUTOMATION: PASS`

`RELEASE 1.12 WP03 — GITHUB LIFECYCLE: CLOSED/DONE`

`RELEASE 1.12 WP03 PR #272 — MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY: READY`

Terminal:

`RELEASE 1.12 WP03 — PR #272 MERGE, POST-MERGE VERIFICATION & LIFECYCLE COMPLETION AUTHORITY COMPLETE`

If any gate fails:

`RELEASE 1.12 WP03 — PR #272 MERGE, POST-MERGE VERIFICATION & LIFECYCLE COMPLETION AUTHORITY BLOCKED`

Do not close #262 unless exact post-merge WP03 acceptance has passed.

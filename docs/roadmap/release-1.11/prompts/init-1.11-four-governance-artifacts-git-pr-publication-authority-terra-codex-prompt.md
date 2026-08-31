# INIT-1.11 — Four Governance Artifacts Git/PR Publication Authority

## Model assignment
- **GPT-5.6 Luna** — contract/policy/governance owner; the completed publication-scope reconciliation freezes the approved payload.
- **GPT-5.6 Terra** — PRIMARY: validation execution and authorized Git/GitHub publication mutations.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

# Governing predecessor
This authority depends on the completed Luna reconciliation:

`INIT-1.11 — INITIATIVE PLANNING ARTIFACTS PUBLICATION-SCOPE RECONCILIATION AUTHORITY COMPLETE`

Frozen decision:

`INIT-1.11 PUBLICATION SCOPE: APPROVED — FOUR NEW GOVERNANCE ARTIFACTS`

The prior one-file Phase 5 documentation publication authority was correctly BLOCKED and is superseded. Do not retry or emulate it.

# Mission
Publish exactly the four approved, currently untracked Initiative-1.11 governance artifacts as new tracked documentation through:

**dedicated branch → one commit → push → one GitHub PR targeting `main`**

This authority does not authorize merge.

# Canonical identity and invariants
Preserve:
- milestone #62:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`;
- `Initiative-1.11 ≠ Product Release 1.11`;
- Product Release 1.11 remains abandoned;
- product sequence `1.10 → 2.0 → 2.1 → 2.2 → 2.3`;
- numbered open product Release milestones use the accepted Phase 5 organizational baseline;
- milestone #60 current title:
  `Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`;
- #252 WP01 Closed/Done;
- #253–#257 Open/Todo;
- Initiative-1.11 Project Release fields unset;
- no Azure feasibility PASS is implied by publishing planning artifacts;
- WP02 remains pending empirical execution.

# Frozen payload
Directory:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/`

The payload is **exactly the four files approved by the Luna reconciliation**:
1. initiative/feasibility definition;
2. feasibility contract;
3. six-WP execution plan;
4. file manifest.

Read the directory and report the exact four paths before mutation.

Known member:
`docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`

Do not invent filenames.

There must be exactly four files in the directory and all four must be included. No fifth path is authorized.

# Phase A — preflight
Before mutation:
1. verify current branch and HEAD;
2. fetch remote state read-only;
3. verify `origin/main`;
4. report ahead/behind;
5. inspect worktree and staging;
6. staging must be empty;
7. identify the exact four approved paths;
8. prove all four are untracked locally;
9. prove all four are absent from `origin/main`;
10. verify no additional file exists in the initiative directory;
11. verify the four still match the Luna-approved coherent governance package;
12. verify no secrets, conflict markers, trailing whitespace, broken local links, or unsupported feasibility claims;
13. verify current-state milestone #60 title is Phase 5;
14. verify milestone #62 remains Phase 4 Initiative-1.11.

Known prior main baseline:
`fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`

If `origin/main` legitimately advanced, do not force the old SHA. Reconcile safely. BLOCK if the advance creates scope ambiguity or a conflict.

Unrelated local work must not enter the publication payload. Do not reset/discard user work.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PREFLIGHT: PASS`

# Phase B — branch
Create a dedicated branch from current canonical `origin/main`.

Frozen preferred branch:

`docs/init-1.11-azure-f1-feasibility`

If it already exists locally or remotely, inspect before reuse. Never overwrite unrelated history and never force-push.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION BRANCH: PASS`

# Phase C — stage
Stage exactly the four approved new files and nothing else.

Verify:
- staged path count = 4;
- every staged path is under the frozen initiative directory;
- every staged path is one of the four Luna-approved artifacts;
- all are additions/new tracked files;
- no modification/deletion/rename outside the payload;
- no source/test/package/schema/config/runtime path is staged.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION STAGING: PASS`

# Phase D — commit
Create exactly one publication commit.

Frozen commit message:

`docs: publish Azure F1 feasibility initiative`

Verify commit:
- exactly four paths;
- all four are new documentation files;
- no other path;
- commit parent is the intended branch baseline;
- content retains all canonical governance invariants.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION COMMIT: PASS`

# Phase E — targeted validation
Required:
- `git diff --check` equivalent for publication commit;
- Markdown syntax/link sanity;
- exact four-file payload;
- no secrets;
- no merge-conflict markers;
- no trailing whitespace;
- no unsupported empirical Azure feasibility claim;
- no unsupported actual recurring-cost proof;
- no WP02+ PASS claim;
- no Product Release 1.11 revival;
- `Initiative-1.11 ≠ Product Release 1.11` preserved;
- #60 current-state reference uses Phase 5;
- #62 identity remains Phase 4;
- six-WP graph remains `WP01 → WP02 → WP03 → WP04 → WP05 → WP06`.

Do not run expensive product test suites unless repository policy requires them for documentation-only changes.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION VALIDATION: PASS`

# Phase F — push
Push only the dedicated branch to the canonical remote.

Do not force-push.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PUSH: PASS`

# Phase G — create GitHub PR
Create exactly one PR targeting `main`.

Frozen title:

`Docs: publish Initiative-1.11 Azure F1 feasibility governance`

PR body must state:
- this publishes four new planning/governance artifacts;
- Initiative-1.11 is a non-release initiative;
- `Initiative-1.11 ≠ Product Release 1.11`;
- Product Release 1.11 remains abandoned;
- Azure App Service Linux F1 is the sole feasibility candidate, not yet accepted as feasible;
- strict recurring infrastructure cost target is `$0.00`;
- WP01 is accepted;
- WP02 remains pending empirical execution;
- publication does not claim Azure feasibility PASS;
- current numbered product releases remain Phase 5;
- milestone #62 remains the Phase 4 initiative milestone;
- no application/source/test/package/schema change;
- no Azure resource mutation;
- no issue/milestone/Project mutation.

Include targeted validation summary.

Conservative default: do not assign this documentation PR to milestone #62 or any Release milestone unless an already-existing explicit repository rule requires it.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PR: PASS`

# Phase H — PR read-back verification
Read the created PR back and verify:
- state = Open;
- base = `main`;
- head = `docs/init-1.11-azure-f1-feasibility` or safely reconciled equivalent;
- title exactly correct;
- commit payload is expected;
- changed file count = exactly 4;
- all four changed files are additions;
- all four are the approved initiative artifacts;
- no fifth file;
- no unexpected commits;
- no milestone assignment unless explicitly required;
- no issue/Project lifecycle mutation occurred.

Report:
- PR number;
- PR URL;
- branch;
- commit SHA;
- exact four changed paths.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PR VERIFICATION: PASS`

# Merge boundary
**DO NOT MERGE.**

This authority ends with a verified Open PR.

Do not:
- merge or auto-merge;
- close the PR;
- delete branch;
- tag;
- publish a GitHub Release.

Merge requires a separate explicit authority/user authorization.

# GitHub lifecycle boundary
Do not mutate:
- #252–#257;
- milestone #62;
- milestone #60;
- Project #2;
- Project Release taxonomy/values.

This documentation publication is not a WP completion event.

# Azure/runtime boundary
Absolutely no:
- Azure CLI resource creation/update/deletion;
- Azure authentication changes beyond any passive credential availability needed for unrelated environment context;
- registry creation/push;
- Twelve Data requests;
- application execution for feasibility;
- WP02 execution.

# Mutation audit
Report exact counts:
- branches created;
- files staged;
- files committed;
- commits created;
- pushes;
- PRs created;
- PRs merged;
- issue mutations;
- milestone mutations;
- Project mutations;
- Release-field mutations;
- tags;
- GitHub Releases;
- Azure mutations;
- registry mutations;
- Twelve Data requests.

Expected successful publication:
- branches created: 1;
- files staged: 4;
- files committed: 4;
- commits created: 1;
- pushes: 1;
- PRs created: 1;
- PRs merged: 0;
- all GitHub governance/Azure/registry/Twelve Data mutation categories: 0.

Required marker:
`INIT-1.11 FOUR-ARTIFACT PUBLICATION MUTATION AUDIT: PASS`

# Required success markers
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PREFLIGHT: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION BRANCH: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION STAGING: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION COMMIT: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION VALIDATION: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PUSH: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PR: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION PR VERIFICATION: PASS`
`INIT-1.11 FOUR-ARTIFACT PUBLICATION MUTATION AUDIT: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal
`INIT-1.11 — FOUR GOVERNANCE ARTIFACTS GIT/PR PUBLICATION AUTHORITY COMPLETE`

# Block conditions
BLOCK if:
- Luna predecessor cannot be verified;
- directory no longer contains exactly four approved files;
- any artifact is unexpectedly already tracked/published and scope cannot be reconciled without Luna;
- content materially diverged from the approved package;
- additional staged/untracked work cannot be safely isolated;
- remote main advance creates ambiguity/conflict;
- branch collision requires destructive action;
- GitHub credentials/permissions prevent safe publication;
- PR payload is not exactly four new approved files;
- publication would require GitHub governance or Azure mutations.

If partial publication occurred, preserve truthful created objects, report exact state, and do not duplicate branches/commits/PRs blindly.

# Exact blocked terminal
`INIT-1.11 — FOUR GOVERNANCE ARTIFACTS GIT/PR PUBLICATION AUTHORITY BLOCKED`

# Phase 5 Documentation Amendment — Narrow Git/PR Publication Authority

## Model assignment
- **GPT-5.6 Luna** — contract/policy/governance owner; prior documentation amendment is the governing accepted scope.
- **GPT-5.6 Terra** — PRIMARY: validation execution and approved Git/GitHub publication mutations.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

# Mission
Publish the already-completed Phase 5 documentation amendment through the narrowest possible Git branch + commit + GitHub PR workflow.

The only intended repository-content change is the previously accepted current-state title correction in:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`

Expected semantic change:

`Phase 4 - Release 2.0: Lightweight Machine Learning Evaluation`

→

`Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`

This authority does not authorize any additional documentation cleanup or product change.

# Governing acceptance
The predecessor completed with:

`PHASE 5 DOCUMENTATION AMENDMENT AUTHORITY COMPLETE`

The publication authority must independently verify that the local diff still exactly matches that accepted scope before any Git mutation.

# Canonical governance invariants
Preserve:
- product sequence `1.10 → 2.0 → 2.1 → 2.2 → 2.3`;
- `Initiative-1.11 ≠ Product Release 1.11`;
- Product Release 1.11 remains abandoned;
- milestone #62 remains:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`;
- #252 Closed/Done;
- #253–#257 Open/Todo unless legitimate later lifecycle progress exists;
- Initiative-1.11 Project Release fields remain unset;
- milestone #60 current title remains:
  `Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`.

# Phase A — pre-publication verification
Before staging:
1. verify current branch and HEAD;
2. fetch remote state read-only;
3. verify `origin/main`;
4. report ahead/behind;
5. inspect full worktree and staging;
6. verify staging is empty;
7. verify exactly the accepted documentation change is present;
8. ensure no unrelated local changes would enter the commit;
9. verify no trailing whitespace/secrets;
10. verify historical Phase 4 evidence was not rewritten.

Known prior main baseline:
`fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`

If `origin/main` has legitimately advanced, do not force the old SHA. Reconcile safely and ensure the amendment applies cleanly.

Required marker:
`PHASE 5 DOC PUBLICATION PREFLIGHT: PASS`

BLOCK before mutation if the working-tree diff contains unapproved changes that cannot be isolated safely.

# Phase B — branch
Create a dedicated branch from current canonical `origin/main`.

Preferred branch name:
`docs/phase-5-milestone-baseline`

If it already exists, inspect it before reuse. Do not overwrite unrelated history.

Do not reset, discard, or overwrite user work.

Required marker:
`PHASE 5 DOC PUBLICATION BRANCH: PASS`

# Phase C — stage and commit
Stage only:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`

Before commit verify staged diff contains only the approved semantic correction.

Preferred commit message:

`docs: reconcile Phase 5 milestone baseline`

Create exactly one publication commit unless existing branch state requires a documented safe reconciliation.

After commit verify:
- commit contains only allowed file;
- working tree contains no accidental publication changes;
- no production/source/test/package/schema files in commit.

Required marker:
`PHASE 5 DOC PUBLICATION COMMIT: PASS`

# Phase D — validation
Because this is documentation-only, validation is targeted.

Required:
- `git diff --check` equivalent on publication diff;
- Markdown syntax/link sanity for changed file;
- exact old/current milestone title classification remains correct;
- product sequence unchanged;
- Initiative-1.11 remains Phase 4/non-release;
- no Product Release 1.11 introduced;
- no secrets;
- no source/test/package/schema change.

Do not run expensive product test suites unless repository policy explicitly requires them for documentation-only PRs. If mandatory CI runs remotely, let CI govern that requirement.

Required marker:
`PHASE 5 DOC PUBLICATION VALIDATION: PASS`

# Phase E — push
Push only the dedicated branch.

Do not force-push.

Required marker:
`PHASE 5 DOC PUBLICATION PUSH: PASS`

# Phase F — GitHub PR
Create exactly one PR targeting `main`.

Preferred title:

`Docs: reconcile Phase 5 milestone baseline`

PR body must state:
- documentation-only governance reconciliation;
- milestone #60 current-state title changed from Phase 4 to Phase 5;
- historical Phase 4 evidence intentionally preserved;
- milestone #62 / Initiative-1.11 remains Phase 4;
- `Initiative-1.11 ≠ Product Release 1.11`;
- no GitHub milestone/issue/Project mutation;
- no application/test/package/schema/Azure mutation.

Include validation summary.

Do not assign the PR to milestone #62 or a numbered Release milestone unless existing repository governance explicitly requires documentation-only governance PRs to be assigned. Conservative default: no milestone assignment.

Required marker:
`PHASE 5 DOC PUBLICATION PR: PASS`

# Phase G — PR verification
Read the created PR back and verify:
- base = `main`;
- head = dedicated docs branch;
- title correct;
- changed files = exactly 1;
- changed file is exactly the accepted documentation file;
- diff is only the accepted current-state milestone-title correction;
- no unexpected commits/files;
- PR is Open unless merge is separately authorized below.

Report actual PR number and URL.

Required marker:
`PHASE 5 DOC PUBLICATION PR VERIFICATION: PASS`

# Merge boundary
This authority authorizes **publication through PR creation only by default**.

Do NOT merge the PR unless the user has separately and explicitly authorized merge/publication-to-main.

Do not close the PR.

Do not delete the branch.

A successful Open PR is sufficient completion for this authority.

# GitHub lifecycle boundary
Do not alter:
- #252–#257;
- milestone #62;
- milestone #60;
- Project #2;
- Release taxonomy;
- tags;
- GitHub Releases.

This documentation PR is not a WP completion event.

# Mutation audit
Report exact counts:
- branches created;
- files staged;
- commits created;
- pushes;
- PRs created;
- PRs merged;
- repository files in commit;
- milestone mutations;
- issue mutations;
- Project mutations;
- Release-field mutations;
- tags;
- GitHub Releases;
- Azure mutations;
- registry mutations;
- Twelve Data requests.

Expected clean publication:
- branch created: 1;
- files staged/committed: 1;
- commits: 1;
- pushes: 1;
- PRs created: 1;
- PRs merged: 0;
- all governance/Azure mutation categories: 0.

Required marker:
`PHASE 5 DOC PUBLICATION MUTATION AUDIT: PASS`

# Required success markers
`PHASE 5 DOC PUBLICATION PREFLIGHT: PASS`
`PHASE 5 DOC PUBLICATION BRANCH: PASS`
`PHASE 5 DOC PUBLICATION COMMIT: PASS`
`PHASE 5 DOC PUBLICATION VALIDATION: PASS`
`PHASE 5 DOC PUBLICATION PUSH: PASS`
`PHASE 5 DOC PUBLICATION PR: PASS`
`PHASE 5 DOC PUBLICATION PR VERIFICATION: PASS`
`PHASE 5 DOC PUBLICATION MUTATION AUDIT: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal
`PHASE 5 DOCUMENTATION AMENDMENT — GIT/PR PUBLICATION AUTHORITY COMPLETE`

# Block conditions
BLOCK before publication if:
- predecessor amendment cannot be verified;
- diff contains additional unapproved semantic changes;
- staging contains unrelated content that cannot be safely isolated;
- remote main advanced in a way that creates ambiguity/conflict;
- credentials/permissions are insufficient;
- branch or PR collision cannot be reconciled without destructive mutation;
- publication would require changing GitHub governance objects.

If partial publication occurs, preserve truthful created objects, report exact state, and avoid duplicate retries.

# Exact blocked terminal
`PHASE 5 DOCUMENTATION AMENDMENT — GIT/PR PUBLICATION AUTHORITY BLOCKED`

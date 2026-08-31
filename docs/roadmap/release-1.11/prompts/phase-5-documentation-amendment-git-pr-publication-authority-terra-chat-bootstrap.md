Execute `phase-5-documentation-amendment-git-pr-publication-authority-terra-codex-prompt.md`.

Use **GPT-5.6 Terra**.

Publish only the already-approved documentation amendment in:

`docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`

Expected semantic correction:
`Phase 4 - Release 2.0: Lightweight Machine Learning Evaluation`
→
`Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`

Create a narrow branch, one commit, push it, and create one PR targeting `main`.

Preferred:
- branch: `docs/phase-5-milestone-baseline`
- commit: `docs: reconcile Phase 5 milestone baseline`
- PR: `Docs: reconcile Phase 5 milestone baseline`

Verify the PR contains exactly one changed file and only the approved documentation correction.

Do not:
- merge the PR;
- modify #252–#257;
- modify milestones #60/#62;
- modify Project #2;
- create Release 1.11;
- alter product code/tests/packages/schema;
- execute Azure;
- create tags/releases.

Preserve:
`Initiative-1.11 ≠ Product Release 1.11`

Report the actual PR number and URL.

End only with the exact COMPLETE or BLOCKED terminal from the authority.

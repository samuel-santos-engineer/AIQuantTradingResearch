Execute `release-1.9-version-tag-publication-policy-adoption-authority-codex-prompt.md` as the sole authority using GPT-5.6 Luna.

Prior reconciliation exhausted existing evidence and ended TAG-UNRESOLVED. This pass is explicit policy adoption, not discovery-only reconciliation.

Create exactly:
`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_PUBLICATION_POLICY_ADOPTION_AUTHORITY.md`

Adopt a complete forward Release 1.9 publication policy with no unresolved fields: exact release version identity, whether a tag is required, exact tag string, canonical target commit, annotated/lightweight, signed/unsigned, tag message, push policy, whether GitHub Release is required, exact Release metadata/notes/disclosure, milestone #58 closure timing, exact execution order, freshness/security, idempotency, and reusable future stable-release convention.

Preferred policy if repository/project metadata does not contradict it:
- version `1.9.0`
- tag `v1.9.0`
- annotated
- unsigned until a dedicated Git-signing policy exists
- target merged main commit `e4958721c9a581efbb2552134c00bc146c73f047`
- push exact tag to origin
- GitHub Release required
- title `Release 1.9 — Real-Time Financial Data Visualization`
- no custom local assets
- close milestone #58 only after tag + GitHub Release publication succeeds.

Preserve the simulated/replay-data disclosure. Do not infer any use of the local Smart App Control Authenticode certificate for Git tag signing.

No staging, commit, push, tag, GitHub Release, milestone, issue, Project, or PR mutation in this Luna pass. Only the policy artifact may be created.

On success require a fresh GPT-5.6 Terra publication execution authority. End with the exact terminal marker.

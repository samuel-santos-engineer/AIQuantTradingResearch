Execute `release-1.9-tag-github-release-milestone-publication-execution-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Binding adopted policy:
- version `1.9.0`
- tag `v1.9.0`
- target `e4958721c9a581efbb2552134c00bc146c73f047`
- annotated, unsigned
- tag message `Release 1.9 — Real-Time Financial Data Visualization`
- GitHub Release required, same title, draft=false, prerelease=false, no custom assets
- close milestone #58 only after tag and GitHub Release verify successfully.

First verify origin/main still equals the canonical merge commit, PR #238 is merged, #233–#237 remain Closed/Done, and milestone #58 is Open with 0/13. If main advanced, stop before mutation.

Then idempotently verify/create and push only tag `v1.9.0`, verify exact target, idempotently verify/publish the GitHub Release with concise factual notes including the simulated/replay non-live disclosure, then close milestone #58 and read back. Never move an existing tag, duplicate a Release, upload custom assets, mutate issues/Project items, or alter repository content.

End with the authority's exact terminal marker.

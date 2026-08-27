Execute `release-1.9-pr-238-review-checks-merge-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

PR #238 is the canonical Release 1.9 PR with frozen payload: one commit `6b7c2cac8c20e6033666e1dfaf160f629fb7894b`, exactly 286 changed paths, base `main`, head `release/1.9-real-time-financial-data-visualization`.

First check whether #238 is still Open or already Merged. If already Merged, do not merge again; perform idempotent post-merge verification only and report the merge SHA/timestamp, 286-path identity, origin/main containment, milestone #58 still Open, #233–#237 unchanged, and zero new mutations.

If still Open, verify exact payload identity, required reviews/checks, mergeability, and resolve merge method strictly from the binding finalization contract/repository policy. Merge only if every required gate passes. Do not invent merge method.

Do not modify repository files, add commits, push new content, close milestone #58, tag, publish a GitHub Release, mutate #233–#237/Project items, delete branches unless explicitly part of canonical merge policy, or create a follow-up governance PR.

End with the authority's exact terminal marker.

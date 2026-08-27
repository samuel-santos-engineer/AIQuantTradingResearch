# Release 1.9 — Tag + GitHub Release + Milestone #58 Publication Execution Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
Execute the adopted Release 1.9 publication policy.

Binding policy artifact:

`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_PUBLICATION_POLICY_ADOPTION_AUTHORITY.md`

This authority covers only:
1. publication freshness verification;
2. exact tag creation/push;
3. GitHub Release publication;
4. milestone #58 closure;
5. final read-back.

No repository content mutation is authorized.

---

# Frozen canonical state

Treat as binding unless current read-back contradicts it:

## Canonical merge
- PR #238 Merged.
- canonical merge commit:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- origin/main must still equal this commit.
- frozen Release 1.9 payload: 286 paths.

## Work packages
- #233–#237 Closed / Done.

## Milestone
- #58 Open.
- 0 open / 13 closed.

## Publication policy
- version: `1.9.0`
- tag required: `v1.9.0`
- tag target:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- tag type: annotated
- tag signing: unsigned
- tag message:
  `Release 1.9 — Real-Time Financial Data Visualization`
- GitHub Release: required
- Release title:
  `Release 1.9 — Real-Time Financial Data Visualization`
- draft: false
- prerelease: false
- custom assets: none
- milestone #58 closes only after successful tag + GitHub Release publication.

## Technical freshness
Inherited accepted:
- build 0/0
- .NET 339/339
- Python 17/17
- Streamlit 1.61.1
- `pip check` clean
- schema v4
- security clean
- residue clean.

No full rerun required if origin/main is unchanged and no executable repository mutation occurred after PR #238 merge.

---

# Binding sources

Read completely:

1. version/tag publication policy artifact;
2. milestone/tag/GitHub Release publication contract;
3. finalization contract;
4. PR #238 merged state;
5. origin/main current SHA;
6. remote tag state;
7. GitHub Release state;
8. milestone #58;
9. #233–#237.

Do not invent any publication semantic.

---

# Phase 1 — Freshness gate

Before mutation verify:

- origin/main exactly:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- PR #238 remains Merged;
- #233–#237 remain Closed/Done;
- milestone #58 remains Open with 0 open / 13 closed;
- no post-merge executable change requiring revalidation.

If origin/main differs:
STOP.

Required blocker:
`RELEASE CANDIDATE ADVANCED — PUBLICATION REVALIDATION REQUIRED`

No tag/milestone/Release mutation.

---

# Phase 2 — Existing tag check

Read local and remote tags.

For exact tag:
`v1.9.0`

Classify:

## TAG-ABSENT
Proceed.

## TAG-MATCH
Tag already exists and resolves exactly to canonical target commit.
Treat tag creation/push as idempotent; proceed to Release verification/publication.

## TAG-CONFLICT
Tag exists but points elsewhere or has materially conflicting metadata.
STOP.

Never move or force-update the tag.

---

# Phase 3 — Create annotated unsigned tag

Only if TAG-ABSENT:

Create local annotated unsigned tag:

Tag:
`v1.9.0`

Target:
`e4958721c9a581efbb2552134c00bc146c73f047`

Message:
`Release 1.9 — Real-Time Financial Data Visualization`

Do not sign the tag.

Do not use Smart App Control Authenticode identity.

Verify local tag resolves to exact canonical commit.

---

# Phase 4 — Push exact tag

Push exactly:

`v1.9.0`

to:

`origin`

Normal push only.

Forbidden:
- `--force`
- pushing all tags
- moving tag

After push, read remote tag and require exact target commit.

If push fails:
STOP before GitHub Release publication.

---

# Phase 5 — GitHub Release collision check

Read GitHub Releases.

Classify:

## RELEASE-ABSENT
Proceed.

## RELEASE-MATCH
Existing Release for `v1.9.0` matches adopted policy.
Treat publication as idempotent and proceed to milestone gate.

## RELEASE-CONFLICT
Release exists for tag but metadata materially conflicts.
STOP.

Do not duplicate Release.

---

# Phase 6 — Release notes

If Release must be created, compose concise factual notes from accepted Release 1.9 evidence.

Must include:

## Summary
Release 1.9 delivers the real-time financial visualization architecture and governed presentation path.

## Architecture
- .NET Worker/Application producer
- canonical JSON handoff
- Python/Streamlit consumer/presentation

## Lifecycle
- graceful cancellation
- restart/readiness acceptance

## Integration/security
- permanent Ready/WarmUp/Empty/Failed coverage
- no direct presentation SQLite/provider bypass
- schema v4 preserved

## Robustness
- Windows atomic-replacement contention retry fix

## Validation
- .NET 339/339
- Python 17/17
- Streamlit 1.61.1
- `pip check` clean
- security scan clean

## Disclosure
Clearly state:
Release 1.9 visualization/demo flows use deterministic simulated/replay data where applicable and do not represent live market/provider connectivity.

Do not overstate capabilities.
Do not include internal authority mechanics excessively.

---

# Phase 7 — Publish GitHub Release

Only after remote tag is verified.

Create exactly one Release:

Tag:
`v1.9.0`

Title:
`Release 1.9 — Real-Time Financial Data Visualization`

Draft:
`false`

Prerelease:
`false`

Assets:
none

Use source archives provided automatically by GitHub only.

Do not upload:
- binaries
- certificates
- test results
- DBs
- custom archives

After publication, read back and require:
- correct tag
- correct title
- published/non-draft
- non-prerelease
- no custom assets

---

# Phase 8 — Milestone pre-close gate

Only after tag and GitHub Release are both verified:

Read milestone #58.

Require:
- milestone #58
- state Open
- 0 open / 13 closed
- all Release 1.9 work-package issues closed
- PR #238 merged
- tag `v1.9.0` verified
- GitHub Release verified

If any prerequisite fails:
STOP.
Do not close milestone.

---

# Phase 9 — Close milestone #58

Close milestone #58 exactly once.

Read back:

- state Closed
- issue counts remain 0 open / 13 closed

No issue/Project mutations.

---

# Phase 10 — Final publication read-back

Verify:

## Git
- origin/main remains:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- tag `v1.9.0` exists and points to exact commit

## GitHub
- PR #238 Merged
- #233–#237 Closed/Done
- Release `v1.9.0` published
- milestone #58 Closed
- no duplicate Release
- no extra tag
- no unrelated mutation

## Repository
No repository-content mutation from this authority.

---

# Idempotency

## Tag already correct
No-op/read-back.

## Release already correct
No-op/read-back.

## Milestone already Closed
If all metadata/counts match expected final state:
no-op/read-back.

## Any conflicting existing object
STOP.

Never:
- move tag
- recreate Release
- reopen/recascade milestone
- duplicate publication objects.

---

# Explicitly forbidden

Do NOT:
- edit repository files;
- stage/commit/branch/push source content;
- merge PRs;
- delete branches;
- mutate #233–#237;
- mutate Project fields/items;
- create extra tags;
- create prerelease/draft Release;
- upload custom assets;
- alter signing configuration.

Only exact tag push, exact GitHub Release publication, and milestone #58 closure are authorized.

---

# Required success report

## Freshness
origin/main exact canonical commit.

## Tag
- state absent/match
- exact name
- exact target
- type annotated
- unsigned
- push result

## Release
- URL
- title
- tag
- published state
- prerelease false
- assets none

## Milestone
- #58 Closed
- 0 open / 13 closed

## Preservation
- #233–#237 unchanged
- PR #238 Merged
- origin/main unchanged

## Mutation markers

`RELEASE 1.9 PUBLICATION REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.9 PUBLICATION GIT MUTATIONS: TAG v1.9.0 CREATED/PUSHED OR IDEMPOTENTLY VERIFIED; ALL OTHER GIT MUTATIONS ZERO`

`RELEASE 1.9 PUBLICATION GITHUB MUTATIONS: GITHUB RELEASE v1.9.0 PUBLISHED OR IDEMPOTENTLY VERIFIED; MILESTONE #58 CLOSED OR IDEMPOTENTLY VERIFIED; ALL OTHER GITHUB MUTATIONS ZERO`

## Final marker

`RELEASE 1.9 PUBLICATION COMPLETE — TAG v1.9.0 / GITHUB RELEASE / MILESTONE #58 CLOSED`

Terminal:

`RELEASE 1.9 TAG + GITHUB RELEASE + MILESTONE PUBLICATION EXECUTION COMPLETE`

---

# Required blocked report

Include:
- last successful gate;
- exact blocker;
- existing tag/Release/milestone state;
- mutations already performed;
- safest next authority.

Mutation accounting must be exact.

Terminal:

`RELEASE 1.9 TAG + GITHUB RELEASE + MILESTONE PUBLICATION EXECUTION BLOCKED`

Never emit COMPLETE unless tag, Release, and milestone final state are all authoritatively verified.

# Release 1.9 — Milestone / Version-Tag / GitHub-Release Publication Contract Authority

## Model
Use **GPT-5.6 Luna**.

## Purpose
Define the final, post-merge publication lifecycle contract for Release 1.9.

This is a **contract-definition-only** authority. It must decide, independently and from canonical repository/GitHub evidence:

1. whether milestone #58 should be closed;
2. whether Release 1.9 requires a Git tag;
3. the exact tag/version convention, if any;
4. whether a GitHub Release should be published;
5. the exact ordering, content, verification, idempotency, and stop conditions for those actions.

This Luna pass performs no Git or GitHub lifecycle mutation.

---

# Frozen post-merge state

Treat as binding unless current read-only evidence contradicts it:

## Canonical PR
- PR #238 is Merged.
- Merge commit:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- Merged:
  `2026-08-27T13:36:42Z`
- Frozen source commit:
  `6b7c2cac8c20e6033666e1dfaf160f629fb7894b`
- PR payload:
  exactly 286 paths, zero missing/unexpected.
- remote `main` is exactly the merge commit.
- compare result: identical.

## Work packages
- #233–#237 remain Closed / Done.

## Milestone
- #58 remains **Open**.
- 0 open / 13 closed.

## Publication
- remote tags: 0.
- GitHub Releases: 0.

## Technical release acceptance
Inherited accepted evidence:
- build 0 warnings / 0 errors;
- .NET 339/339;
- Python 17/17;
- Streamlit 1.61.1;
- `pip check` clean;
- WP08 lifecycle accepted;
- WP09 permanent integration/no-bypass accepted;
- SQLite schema v4;
- security/Gitleaks clean;
- residue clean.

## Finalization model
The binding Release 1.9 finalization contract selected:
`F-SPLIT`

PR creation/merge was separated from milestone/tag/Release publication.

---

# Binding sources to read

Read completely before deciding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`
2. Release 1.9 definition.
3. Release 1.9 execution plan.
4. Release 1.9 file manifest.
5. roadmap.
6. README.
7. contribution/release/versioning documentation.
8. PR #238 and merge metadata.
9. milestone #58.
10. #233–#237.
11. all repository Git tags, including historical tags if any.
12. all GitHub Releases, including historical releases if any.
13. package/project version declarations, if any.
14. changelog/release-notes conventions, if any.
15. GitHub Actions/workflows related to tags/releases, if any.
16. repository branch protection/release automation relevant to publication.

Historical conventions may establish a pattern only when sufficiently consistent and not contradicted by canonical documentation.

Do not invent semantic-versioning policy merely because SemVer is common.

---

# Output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_MILESTONE_VERSION_TAG_GITHUB_RELEASE_PUBLICATION_CONTRACT_AUTHORITY.md`

No other repository file may change.

---

# 1 — Three independent decisions

Do not bundle these automatically.

Classify each independently.

## Milestone

Choose exactly one:

- `M-CLOSE` — #58 should now be closed.
- `M-DEFER` — closure belongs after another defined event.
- `M-NOT-REQUIRED` — repository convention intentionally leaves milestones open.
- `M-UNRESOLVED`.

## Tag

Choose exactly one:

- `T-CREATE` — a canonical Release 1.9 tag is required and its exact identity is established.
- `T-NOT-REQUIRED`.
- `T-DEFER`.
- `T-UNRESOLVED`.

## GitHub Release

Choose exactly one:

- `R-PUBLISH` — publication is required and exact semantics are established.
- `R-NOT-REQUIRED`.
- `R-DEFER`.
- `R-UNRESOLVED`.

Do not infer T-CREATE from M-CLOSE.
Do not infer R-PUBLISH from T-CREATE.

---

# 2 — Milestone closure contract

If `M-CLOSE`, define exact prerequisites:

- milestone identity = #58;
- milestone title corresponds to Release 1.9;
- 0 open / 13 closed;
- #233–#237 closed;
- PR #238 merged;
- merge commit present on origin/main;
- no required Release 1.9 issue remains outside the milestone.

Define exact mutation:
- close milestone #58 only.

Define read-back:
- state Closed;
- issue counts unchanged;
- no issue/Project mutation.

If canonical evidence requires milestone closure before or after tag/Release, specify exact ordering.

---

# 3 — Version identity

Determine what "Release 1.9" means as a repository version identifier.

Inspect:
- project/package versions;
- historical milestones;
- historical branches;
- docs;
- prior tags/releases;
- version files.

If canonical evidence supports an exact version, record it.

Examples such as `1.9`, `1.9.0`, `v1.9`, or `v1.9.0` are illustrative only and MUST NOT be selected without evidence.

If no exact canonical version identity can be established:
`T-UNRESOLVED`.

---

# 4 — Tag naming

If `T-CREATE`, define:

- exact tag name;
- target commit;
- annotated vs lightweight;
- tag message;
- signed vs unsigned;
- tagger identity requirements;
- whether local tag creation and remote push are both authorized.

Default target should be the canonical merged Release 1.9 commit only if repository policy supports that:
`e4958721c9a581efbb2552134c00bc146c73f047`

Do not tag the pre-merge source commit unless canonical policy explicitly requires it.

---

# 5 — Existing-tag safety

Before future creation, require:

- fetch/read remote tags;
- exact tag does not already exist, OR if it exists it points to the exact expected commit and is treated idempotently.

If an existing canonical tag points elsewhere:
STOP.

Never move/force-update a release tag under the execution authority.

---

# 6 — GitHub Release publication

If `R-PUBLISH`, define exactly:

- target tag;
- release title;
- release notes source;
- draft status;
- prerelease status;
- latest-release semantics if configurable;
- assets;
- discussion/category if relevant.

Do not attach:
- local binaries;
- test outputs;
- certificates;
- signing artifacts;
- arbitrary generated archives;

unless canonical repository policy explicitly requires them.

Prefer source-only GitHub-generated archives if no assets are required.

---

# 7 — Release notes

Determine whether accepted repository documentation already provides sufficient release notes.

If new release-note content is required, define:

- exact source;
- exact required sections;
- whether GitHub auto-generated notes are allowed;
- whether a repository changelog must change.

This Luna pass must NOT silently create a new changelog/release-note artifact beyond the single contract file.

If a repository file must be added/modified before publication:
BLOCK publication and require a separate documentation authority.

---

# 8 — Simulated-data disclosure

If a GitHub Release is published, its notes must preserve the accepted Release 1.9 safety/expectation boundary:

- Release 1.9 visualization uses simulated/replay data where applicable;
- it must not imply live brokerage/provider market-data connectivity if none exists.

Define the exact factual disclosure requirement without marketing embellishment.

---

# 9 — Security publication gate

Before tag/Release execution, require read-only confirmation:

- canonical merge commit unchanged;
- no secret finding relevant to published repository content;
- no private signing material;
- local Smart App Control certificate/config remains local-only;
- no local signed binaries/assets uploaded.

Do not require re-running the entire test matrix unless repository changed after PR #238 merge.

---

# 10 — Technical freshness

Define publication freshness rule.

Preferred:
If `origin/main` remains exactly the verified PR #238 merge commit and no executable repository mutation occurred afterward, inherit accepted 339/339 + 17/17 evidence.

If `origin/main` changes:
STOP and require release-candidate revalidation before publication.

---

# 11 — Ordering model

If multiple actions are authorized, define exact order.

Choose one evidence-backed sequence, for example only if canonical:

A. verify main → close milestone → tag → push tag → publish Release;
B. verify main → tag → publish Release → close milestone;
C. another canonical sequence.

Do not choose based on aesthetics.

---

# 12 — Separation model

If one or more decisions remain unresolved, define the smallest next authority.

Examples:

- milestone closure only;
- tag naming reconciliation;
- GitHub Release publication policy;
- combined tag+Release execution.

The execution authority must never need to invent missing publication semantics.

---

# 13 — Idempotency

Define behavior if:

- milestone already Closed;
- tag already exists at expected commit;
- tag exists at wrong commit;
- GitHub Release already exists for tag;
- GitHub Release exists with conflicting metadata.

Safe expected behavior:
- matching state → read-back/no-op;
- conflicting state → STOP;
- never duplicate releases;
- never move release tag.

---

# 14 — Failure boundaries

Future Terra execution must STOP on:

- origin/main != canonical merge commit;
- milestone count/state mismatch;
- reopened Release 1.9 issue;
- unexpected required issue;
- unresolved tag format;
- tag collision;
- Release collision;
- security concern;
- publication would imply unsupported live-data capability;
- required release-note repository mutation is missing.

No production/test fix under publication authority.

---

# 15 — Final expected state

For each chosen decision, define exact final Git/GitHub state.

Must preserve:
- #233–#237 Closed/Done;
- Project metadata;
- PR #238 Merged;
- canonical origin/main;
- no unrelated issue/Project mutation.

If milestone closes:
- #58 Closed.

If tag created:
- exact tag points to exact commit.

If Release published:
- exactly one Release for exact tag with defined metadata.

---

# 16 — Mutation accounting for future Terra

Require separate reporting:

## Repository
Expected zero.

## Git
Potential tag creation/push only if T-CREATE.

## GitHub
Potential milestone closure and/or GitHub Release publication only as explicitly authorized.

No issue/Project mutation.

---

# 17 — Acceptance matrix

Create a table with:

| ID | Gate |
|---|---|
| MAIN | canonical merge commit |
| ISSUES | #233–#237 |
| MS | milestone identity/count |
| MDEC | milestone decision |
| VERS | version identity |
| TDEC | tag decision |
| TAG | tag identity/target/type |
| RDEC | Release decision |
| NOTES | release notes |
| DISC | simulated-data disclosure |
| SEC | publication security |
| FRESH | technical freshness |
| ORDER | mutation order |
| IDEMP | idempotency |
| FINAL | final state |

For each:
- evidence;
- pass condition;
- stop condition.

---

# 18 — This Luna pass mutation boundary

Allowed repository mutation:

Create exactly:
`docs/roadmap/release-1.9/RELEASE_1.9_MILESTONE_VERSION_TAG_GITHUB_RELEASE_PUBLICATION_CONTRACT_AUTHORITY.md`

Forbidden:
- source/test/Python/docs outside this artifact;
- staging;
- commit;
- branch;
- push;
- milestone mutation;
- tag;
- GitHub Release;
- issue/Project mutation;
- PR mutation.

---

# Required success report

## Artifact
Exact path.

## Milestone decision
M-CLOSE / M-DEFER / M-NOT-REQUIRED.

## Tag decision
T-CREATE / T-NOT-REQUIRED / T-DEFER.

If T-CREATE:
- exact tag;
- exact target;
- tag type/signing/message.

## GitHub Release decision
R-PUBLISH / R-NOT-REQUIRED / R-DEFER.

If R-PUBLISH:
- exact title/tag/notes/draft/prerelease/assets policy.

## Ordering
Exact sequence.

## Freshness/security
Exact inherited/rerun requirements.

## Idempotency
Summary.

## Mutation statement
`RELEASE 1.9 PUBLICATION CONTRACT AUTHORITY MUTATIONS: ZERO Git/GitHub mutations; one authorized contract artifact created`

## Next authority
If executable:
`RELEASE 1.9 PUBLICATION CONTRACT DEFINED — FRESH GPT-5.6 TERRA PUBLICATION EXECUTION AUTHORITY REQUIRED`

Terminal:
`RELEASE 1.9 MILESTONE / VERSION-TAG / GITHUB-RELEASE PUBLICATION CONTRACT AUTHORITY COMPLETE`

---

# Required blocked report

If any essential publication decision is unresolved, this can still be COMPLETE only if the contract explicitly establishes a safe split and identifies the exact next reconciliation authority.

If the sources are materially contradictory and no safe contract can be established:

`RELEASE 1.9 PUBLICATION CONTRACT AUTHORITY MUTATIONS: ZERO Git/GitHub mutations`

Terminal:
`RELEASE 1.9 MILESTONE / VERSION-TAG / GITHUB-RELEASE PUBLICATION CONTRACT AUTHORITY BLOCKED`

Never invent a tag format or GitHub Release convention merely to finish Release 1.9.

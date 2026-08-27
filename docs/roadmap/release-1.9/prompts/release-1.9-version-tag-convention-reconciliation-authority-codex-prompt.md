# Release 1.9 — Version / Tag Convention Reconciliation Authority

## Model
Use **GPT-5.6 Luna**.

## Purpose
Resolve the only remaining publication ambiguity for Release 1.9:

`T-UNRESOLVED — canonical version/tag convention is not yet established`

This is a **documentation-only reconciliation authority**.

It must determine:

1. whether Release 1.9 requires a Git tag at all;
2. if yes, the exact canonical tag string;
3. exact target commit;
4. annotated vs lightweight;
5. signed vs unsigned;
6. exact tag message;
7. whether tag push is required;
8. whether the resolved tag unlocks GitHub Release publication;
9. whether the resolved tag changes milestone #58 timing.

This Luna pass performs no Git or GitHub lifecycle mutation.

---

# Frozen post-merge state

Treat as binding unless read-only evidence disproves it:

## Canonical merge
- PR #238 Merged.
- merge commit:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- frozen source commit:
  `6b7c2cac8c20e6033666e1dfaf160f629fb7894b`
- PR payload:
  exactly 286 paths.
- origin/main:
  exactly `e4958721c9a581efbb2552134c00bc146c73f047`.

## Work packages
- #233–#237 Closed / Done.

## Milestone
- #58 Open.
- 0 open / 13 closed.
- publication contract decision:
  `M-DEFER`.

## Tag / Release
- remote tags: 0.
- GitHub Releases: 0.
- publication contract:
  `T-UNRESOLVED`
  `R-DEFER`.

## Technical baseline
- build 0 warnings / 0 errors.
- .NET 339/339.
- Python 17/17.
- Streamlit 1.61.1.
- `pip check` clean.
- schema v4.
- security clean.
- residue clean.

---

# Binding sources to read

Read completely:

1. `docs/roadmap/release-1.9/RELEASE_1.9_MILESTONE_VERSION_TAG_GITHUB_RELEASE_PUBLICATION_CONTRACT_AUTHORITY.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`
3. Release 1.9 definition.
4. Release 1.9 execution plan.
5. Release 1.9 file manifest.
6. roadmap.
7. README.
8. any versioning/release documentation.
9. project/package version declarations.
10. all historical Git tags.
11. all historical GitHub Releases.
12. historical release branches.
13. historical release PR titles/bodies.
14. GitHub workflows reacting to tags/releases.
15. package/project metadata that encodes versioning.
16. milestone naming conventions.
17. changelog/release-note conventions.
18. repository contribution/release guidance.

Historical evidence may establish convention only if consistent enough to be canonical and not contradicted by current Release 1.9 authority.

Do not infer SemVer merely because it is common.

---

# Output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_CONVENTION_RECONCILIATION_AUTHORITY.md`

No other repository file may change.

---

# 1 — First decision: is a tag required?

Choose exactly one:

## TAG-REQUIRED
Canonical repository/release evidence shows a Git tag is part of release completion.

## TAG-NOT-REQUIRED
Canonical repository/release evidence shows releases intentionally do not use tags.

## TAG-OPTIONAL
Tagging is allowed but not required for Release 1.9 completion.

## TAG-UNRESOLVED
Evidence remains insufficient or contradictory.

Do not proceed to exact naming if TAG-NOT-REQUIRED.

---

# 2 — Version identity

If TAG-REQUIRED or TAG-OPTIONAL, determine the exact canonical Release 1.9 version identity.

Inspect:
- assembly/project versions;
- package versions;
- milestone title;
- release branch names;
- prior version labels;
- docs;
- release notes;
- historical tag names.

Distinguish:
- marketing/release label `1.9`;
- semantic package version `1.9.0`;
- branch shorthand `1.9`;
- tag prefix conventions.

Do not collapse them automatically.

---

# 3 — Tag naming candidates

Evaluate all evidence-backed candidates.

Examples only:
- `1.9`
- `1.9.0`
- `v1.9`
- `v1.9.0`

These examples are NOT authorized defaults.

For each evidence-backed candidate, record:
- source;
- consistency with historical convention;
- compatibility with project/package versioning;
- ambiguity.

Select one only if canonical.

If multiple remain equally valid:
TAG-UNRESOLVED.

---

# 4 — Historical tag convention

If repository has prior tags, build a table:

`tag → release/milestone → target style → annotated/lightweight → signed?`

Determine:
- prefix;
- version-component count;
- pre-release syntax;
- annotated/lightweight consistency.

A single isolated historical tag is weak evidence unless canonical docs support it.

If repository has zero historical tags, state that clearly and rely on other canonical version sources only.

---

# 5 — Historical GitHub Release convention

If repository has prior GitHub Releases, inspect:
- tag names;
- release titles;
- version syntax;
- draft/prerelease flags;
- source branch/commit.

Use this only to resolve version/tag convention if consistent.

If zero Releases:
state that clearly.

---

# 6 — Project/package version evidence

Inspect actual version-bearing project/package metadata.

Examples:
- `<Version>`
- `<VersionPrefix>`
- package version declarations;
- assembly informational version;
- Python package metadata if any.

Determine whether Release 1.9 corresponds canonically to:
- 1.9
- 1.9.0
- another exact form.

Do not change version metadata under this authority.

If code/package metadata remains at an unrelated historical version:
do not invent a tag to paper over the discrepancy.
Report the mismatch.

---

# 7 — Milestone/version relationship

Determine whether milestone title/number establishes a release version convention.

Example:
- milestone named `Release 1.9` may establish release label 1.9,
but not necessarily exact Git tag syntax.

State the exact evidentiary weight.

---

# 8 — Release branch relationship

Current merged branch:
`release/1.9-real-time-financial-data-visualization`

Determine whether repository convention maps:
`release/1.9-*`
to a canonical tag pattern.

Do not infer a tag solely from this branch name unless historical/canonical evidence supports the mapping.

---

# 9 — Exact target commit

If a tag is authorized, determine the target.

Preferred expected target only if canonical:
`e4958721c9a581efbb2552134c00bc146c73f047`

The tag should generally identify the canonical merged Release 1.9 state.

Do not target:
- pre-merge source commit;
- branch tip differing from merged main;
- later unrelated commit;

unless canonical policy explicitly requires it.

Lock exact target SHA.

---

# 10 — Annotated vs lightweight

Determine exact tag type from:
- repository documentation;
- historical tags;
- release tooling/workflows.

Choose:

- `ANNOTATED`
- `LIGHTWEIGHT`
- `UNRESOLVED`

Do not prefer annotated merely because it is common release practice.

---

# 11 — Signed vs unsigned

Determine:

- `SIGNED`
- `UNSIGNED`
- `UNRESOLVED`

Evidence sources:
- repository signing policy;
- historical signed tags;
- release workflow;
- contributor docs.

Important:
The local Smart App Control Authenticode certificate is unrelated to Git tag signing unless canonical docs explicitly connect them.

Do not reuse local Authenticode identity for Git signing by inference.

---

# 12 — Tag message

If annotated/signed tag required, define exact message or template.

Preferred content only if canonical:
`Release 1.9 — Real-Time Financial Data Visualization`

Do not invent message if lightweight tag.

---

# 13 — Tag push

Determine whether publication requires:

- local tag creation only;
- local tag + remote push;
- GitHub API-created tag/ref;
- another canonical mechanism.

Future Terra authority must have one exact path.

Force-moving tags must be forbidden.

---

# 14 — Workflow automation impact

Inspect GitHub Actions/workflows for tag triggers.

If a tag automatically:
- publishes packages;
- creates Release;
- deploys;
- signs artifacts;
- triggers external effects;

the contract must record that.

If tag creation has side effects, future execution authority must treat them as part of the mutation blast radius.

If no tag-triggered workflow exists:
state that clearly.

---

# 15 — Relationship to GitHub Release

After resolving tag, update publication decision:

Choose one:

## RELEASE-UNLOCKED
A GitHub Release can now be defined/published from the canonical tag under a subsequent authority.

## RELEASE-NOT-REQUIRED
Tag may exist without GitHub Release.

## RELEASE-STILL-DEFERRED
Additional release-notes/publication semantics remain unresolved.

Do not publish anything here.

---

# 16 — Relationship to milestone #58

After resolving tag, classify milestone timing:

## MILESTONE-AFTER-TAG
Milestone should close only after tag exists.

## MILESTONE-AFTER-RELEASE
Milestone should close after GitHub Release publication.

## MILESTONE-INDEPENDENT
Milestone closure can proceed independently once work packages/PR are complete.

## MILESTONE-STILL-DEFERRED
Authority remains insufficient.

This updates the prior `M-DEFER` decision without mutating milestone.

---

# 17 — Idempotency

Define future execution behavior:

## Tag absent
Create exact tag only if authorized.

## Tag exists at correct commit
No-op/read-back.

## Tag exists at wrong commit
STOP.

## Similar-but-different tag exists
Do not delete/rename automatically.
Report ambiguity.

Never force-update a release tag.

---

# 18 — Freshness gate

Before future tag execution require:

- origin/main exactly the canonical merge commit;
- PR #238 remains merged;
- #233–#237 remain closed;
- no post-merge executable change on main.

If origin/main advances before tag creation:
STOP and require release-candidate reconciliation/revalidation to determine the correct target.

This is critical: the resolved tag target is SHA-specific.

---

# 19 — Security boundary

Future tag execution must not require:
- private Authenticode material;
- repository secret exposure;
- uploading binaries;
- changing signing setup.

If signed Git tags are canonical, use only the repository's established Git-signing mechanism.

If none exists:
do not invent one.

---

# 20 — Exact decision matrix

Artifact must include:

| ID | Decision |
|---|---|
| REQUIRED | tag required/optional/not-required |
| VERSION | exact Release 1.9 version identity |
| NAME | exact tag name |
| TARGET | exact commit |
| TYPE | annotated/lightweight |
| SIGN | signed/unsigned |
| MESSAGE | tag message |
| PUSH | push mechanism |
| AUTO | tag-triggered automation |
| RELEASE | GitHub Release relationship |
| MILESTONE | milestone relationship |
| FRESH | freshness |
| IDEMP | idempotency |

For each:
- evidence;
- selected result;
- stop condition.

---

# 21 — This Luna pass mutation boundary

Allowed:
Create exactly:
`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_CONVENTION_RECONCILIATION_AUTHORITY.md`

Forbidden:
- all other repository edits;
- staging;
- commit;
- branch;
- push;
- tag;
- milestone mutation;
- GitHub Release;
- PR mutation;
- issue/Project mutation.

---

# Required success report

## Artifact
Exact path.

## Tag requirement
TAG-REQUIRED / TAG-NOT-REQUIRED / TAG-OPTIONAL.

## Version
Exact version identity.

## Tag
Exact name or explicit no-tag decision.

If tag:
- target SHA;
- annotated/lightweight;
- signed/unsigned;
- message;
- push mechanism.

## Automation
Tag-trigger effects.

## GitHub Release relationship
RELEASE-UNLOCKED / NOT-REQUIRED / STILL-DEFERRED.

## Milestone relationship
Exact classification.

## Freshness/idempotency
Exact rules.

## Mutation statement
`RELEASE 1.9 VERSION/TAG RECONCILIATION MUTATIONS: ZERO Git/GitHub mutations; one authorized reconciliation artifact created`

## Next authority

If tag execution is ready:
`RELEASE 1.9 TAG CONVENTION RECONCILED — FRESH GPT-5.6 TERRA TAG/PUBLICATION EXECUTION AUTHORITY REQUIRED`

If no tag required:
identify the next milestone/Release authority instead.

Terminal:
`RELEASE 1.9 VERSION / TAG CONVENTION RECONCILIATION COMPLETE`

---

# Required blocked report

If TAG-UNRESOLVED:
- exact conflicting/insufficient evidence;
- minimum next authority or user decision required;
- no mutation.

Terminal:
`RELEASE 1.9 VERSION / TAG CONVENTION RECONCILIATION BLOCKED`

Never invent a version/tag convention solely to complete the release.

# Release 1.9 — Version / Tag Publication Policy Adoption Authority

## Model
Use **GPT-5.6 Luna**.

## Purpose
Adopt an explicit, forward-looking version/tag/publication policy for Release 1.9 where canonical repository evidence was insufficient to resolve tag semantics.

This is a **policy-definition-only authority**.

It must establish, prospectively and unambiguously:

1. whether Release 1.9 uses a Git tag;
2. the exact version identity;
3. exact tag naming;
4. exact tag target rule;
5. annotated vs lightweight;
6. signed vs unsigned;
7. exact tag message;
8. push policy;
9. milestone #58 closure timing;
10. whether a GitHub Release is required;
11. exact GitHub Release metadata/publishing policy;
12. release-notes/disclosure requirements;
13. freshness, idempotency, and stop conditions.

No Git or GitHub lifecycle mutation is permitted in this Luna pass.

---

# Why policy adoption is required

The prior reconciliation authority concluded:

`TAG-UNRESOLVED`

because the repository has no canonical:

- tag format;
- exact release version identity;
- tag type;
- signing policy;
- tag message convention;
- push policy.

Publication contract currently remains:

- milestone: `M-DEFER`
- tag: `T-UNRESOLVED`
- GitHub Release: `R-DEFER`

Further reconciliation cannot derive a policy that does not exist.

Therefore this authority explicitly adopts one.

---

# Frozen Release 1.9 state

Treat as binding:

## Canonical merge
- PR #238 Merged.
- merge commit:
  `e4958721c9a581efbb2552134c00bc146c73f047`
- origin/main equals that merge commit.
- frozen Release 1.9 payload: 286 paths.

## Work packages
- #233–#237 Closed / Done.

## Milestone
- #58 Open.
- 0 open / 13 closed.

## Publication
- remote tags: 0.
- GitHub Releases: 0.

## Technical
- build 0 warnings / 0 errors.
- .NET 339/339.
- Python 17/17.
- Streamlit 1.61.1.
- `pip check` clean.
- SQLite schema v4.
- security clean.
- residue clean.

---

# Binding sources to read

Read completely:

1. finalization contract;
2. milestone/tag/Release publication contract;
3. version/tag reconciliation artifact;
4. Release 1.9 definition;
5. Release 1.9 execution plan;
6. Release 1.9 manifest;
7. roadmap;
8. README;
9. current project/package version metadata;
10. current tags/releases;
11. repository workflows triggered by tags/releases;
12. branch/PR/release documentation;
13. accepted simulated/replay disclosure language.

These sources constrain the adopted policy but do not need to supply an existing tag convention.

---

# Canonical output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_PUBLICATION_POLICY_ADOPTION_AUTHORITY.md`

No other repository file may change.

---

# Policy decision 1 — Release version identity

Adopt one exact version identity for Release 1.9.

Preferred candidate:

`1.9.0`

Use this only if consistent with Release 1.9 naming and no project/package metadata contradicts it.

If metadata materially conflicts, select the least disruptive exact version identity and document why.

The adopted identity must distinguish:

- human release label: `Release 1.9`
- canonical release version: exact adopted version
- Git tag name: separately defined below.

No ambiguity after adoption.

---

# Policy decision 2 — Tag required

Adopt:

`TAG-REQUIRED`

unless there is a concrete safety/automation reason not to tag.

Rationale to document:
- Release 1.9 has a canonical merged commit;
- all work packages are closed;
- milestone is complete;
- tag provides immutable release identity.

If tag creation would trigger unsafe/unwanted automation, the policy may instead adopt `TAG-NOT-REQUIRED`, but must state the exact reason.

---

# Policy decision 3 — Tag naming

Adopt one exact format prospectively.

Preferred:

`v1.9.0`

if the adopted version identity is `1.9.0`.

Required rule:

`v<MAJOR>.<MINOR>.<PATCH>`

for future stable releases unless repository governance later supersedes it.

Do not adopt:
- multiple equivalent aliases;
- both `1.9.0` and `v1.9.0`;
- moving tags.

One canonical tag only.

---

# Policy decision 4 — Tag target

Adopt:

`Tag the canonical merged release commit on origin/main.`

For Release 1.9 the exact target is:

`e4958721c9a581efbb2552134c00bc146c73f047`

Future rule:
- stable release tag points to the canonical merged release commit on the default branch;
- never pre-merge source commit;
- never later unrelated commit.

If origin/main advances before execution:
STOP and require freshness reconciliation before tagging.

---

# Policy decision 5 — Tag type

Adopt one exact type.

Preferred:

`ANNOTATED`

Rationale:
- stable release tags should carry explicit release metadata;
- they are distinguishable from incidental lightweight references.

If repository/tooling constraints make lightweight tags materially preferable, adopt `LIGHTWEIGHT` and explain.

No ambiguity.

---

# Policy decision 6 — Tag signing

Adopt one exact rule:

Preferred:
`UNSIGNED unless a dedicated Git tag-signing identity/policy is separately configured.`

Important:
- local Smart App Control Authenticode certificate is unrelated;
- do not reuse local Authenticode identity for Git tag signing;
- no private key material may be introduced solely for Release 1.9.

Future signed-tag adoption requires a separate security/signing policy.

---

# Policy decision 7 — Tag message

If annotated, adopt exact Release 1.9 message:

`Release 1.9 — Real-Time Financial Data Visualization`

Future template:

`Release <major.minor> — <release title>`

or exact repository-appropriate equivalent.

If lightweight, message is not applicable.

---

# Policy decision 8 — Tag push

Adopt:

- create local canonical tag;
- push that exact tag to `origin`;
- normal push only;
- never force-update/move a release tag.

Idempotency:
- existing correct tag → no-op/read-back;
- existing tag at wrong commit → STOP.

---

# Policy decision 9 — GitHub Release usage

Adopt one exact rule.

Preferred:

`GITHUB-RELEASE-REQUIRED`

for stable releases.

If adopted, GitHub Release must be created from the canonical tag.

If the project intentionally prefers tags only, adopt:
`GITHUB-RELEASE-NOT-REQUIRED`.

No ambiguity.

---

# Policy decision 10 — GitHub Release metadata

If required, adopt:

## Tag
exact canonical tag.

## Title
`Release 1.9 — Real-Time Financial Data Visualization`

## Draft
`false`

## Prerelease
`false`

## Latest
Use GitHub default/latest stable behavior if available; do not manually override unless needed.

## Assets
No custom local assets.

Use source archives automatically provided by GitHub.

Do not upload:
- binaries;
- signed local build outputs;
- certificates;
- test artifacts;
- database files.

---

# Policy decision 11 — Release notes

Adopt one exact release-notes policy.

Preferred:

Manual concise notes derived from accepted Release 1.9 evidence, including:

1. Release 1.9 summary.
2. Real-time/simulated visualization architecture.
3. Worker → canonical JSON handoff → Python/Streamlit flow.
4. WP08 lifecycle/restart/cancellation.
5. WP09 permanent integration/no-bypass coverage.
6. schema v4 preservation.
7. Windows atomic-replacement robustness fix.
8. developer documentation/security alignment.
9. validation:
   - 339/339 .NET
   - 17/17 Python
   - Streamlit 1.61.1
   - `pip check` clean
10. simulated/replay disclosure.

Do not list internal authority mechanics excessively.

---

# Policy decision 12 — Simulated/replay disclosure

Every Release 1.9 GitHub Release note must state clearly:

- demonstration/visualization flows use deterministic simulated/replay data where applicable;
- this is not a live market-data/broker connectivity release.

The wording must remain factual and concise.

---

# Policy decision 13 — Milestone closure

Adopt exact rule:

Preferred:

`Close the release milestone after canonical tag creation and GitHub Release publication succeed.`

Thus for Release 1.9:

1. verify canonical main;
2. create/push tag;
3. publish GitHub Release;
4. close milestone #58;
5. final read-back.

If GitHub Release is not required:
close milestone after successful tag publication.

If tag is not required:
define another exact closure point.

Do not leave `M-DEFER`.

---

# Policy decision 14 — Ordering

The artifact must define one exact execution order.

Preferred full publication sequence:

1. read-back canonical origin/main;
2. verify #233–#237 Closed/Done;
3. verify milestone #58 Open / 0 open / 13 closed;
4. verify no conflicting tag/Release;
5. confirm technical/security freshness;
6. create canonical annotated unsigned tag;
7. push tag to origin;
8. read back tag target;
9. publish GitHub Release;
10. read back Release;
11. close milestone #58;
12. read back milestone Closed;
13. final tag/Release/main/issues verification.

No reordering in Terra execution without amendment.

---

# Policy decision 15 — Technical freshness

Adopt:

If origin/main remains exactly:

`e4958721c9a581efbb2552134c00bc146c73f047`

and no repository executable content changed after accepted PR #238 merge verification, inherit:

- build 0/0;
- .NET 339/339;
- Python 17/17;
- security clean;
- residue clean.

No full rerun required solely to tag/publish.

If main advances:
STOP and require release-candidate revalidation.

---

# Policy decision 16 — Security

Before publication execution:

- verify canonical commit unchanged;
- verify no new secret finding;
- no private tag-signing material needed under unsigned policy;
- no local Authenticode artifacts uploaded;
- no custom release binary assets.

No new security tooling.

---

# Policy decision 17 — Idempotency

Future Terra execution must handle:

## Tag
- absent → create.
- correct tag exists at correct commit → no-op/read-back.
- exists at wrong commit → STOP.

## GitHub Release
- absent → create if required.
- matching Release exists → no-op/read-back.
- conflicting Release exists → STOP.

## Milestone
- Open → close at exact policy point.
- already Closed with expected counts → no-op/read-back.
- inconsistent state → STOP.

Never duplicate lifecycle objects.

---

# Policy decision 18 — Future release convention

This adoption should establish a reusable default for future stable releases unless superseded.

Recommended reusable policy:

- release version: `MAJOR.MINOR.PATCH`;
- canonical Git tag: `vMAJOR.MINOR.PATCH`;
- annotated;
- unsigned by default until dedicated Git-signing policy exists;
- tag canonical merged default-branch commit;
- GitHub Release required for stable releases;
- milestone closes after successful tag + Release publication.

State explicitly that future release-specific authorities may override this.

---

# Policy decision 19 — Final expected Release 1.9 state

If policy execution later succeeds:

- origin/main remains canonical merge commit.
- canonical tag exists at exact commit.
- GitHub Release exists for exact tag.
- milestone #58 Closed.
- #233–#237 remain Closed/Done.
- PR #238 remains Merged.
- no issue/Project metadata changes.
- no repository file changes from publication.
- no custom binary assets.

---

# Acceptance matrix

Create a table:

| ID | Policy |
|---|---|
| VERSION | exact version |
| REQUIRED | tag required |
| NAME | exact tag |
| TARGET | exact commit |
| TYPE | annotated/lightweight |
| SIGN | signed/unsigned |
| MESSAGE | tag message |
| PUSH | push rule |
| GHREL | GitHub Release rule |
| NOTES | release notes |
| DISC | simulated-data disclosure |
| MS | milestone closure |
| ORDER | exact sequence |
| FRESH | freshness |
| SEC | security |
| IDEMP | idempotency |
| FUTURE | reusable convention |
| FINAL | final expected state |

Every row must have one explicit adopted policy.

No `UNRESOLVED`, `DEFER`, or `TBD` may remain if this authority completes.

---

# Stop conditions

BLOCK policy adoption if:

- project/package version metadata makes `1.9.0` materially false and no safe alternative can be adopted;
- tag-triggered automation would cause ungoverned side effects;
- GitHub Release publication conflicts with repository policy;
- milestone closure ordering conflicts with canonical project governance;
- adopting a tag/signing policy would require secrets/private keys not available.

If blocked, do not partially adopt contradictory policy.

---

# This Luna pass mutation boundary

Allowed:

Create exactly:
`docs/roadmap/release-1.9/RELEASE_1.9_VERSION_TAG_PUBLICATION_POLICY_ADOPTION_AUTHORITY.md`

Forbidden:

- all other repository edits;
- staging;
- commit;
- branch;
- push;
- tag;
- GitHub Release;
- milestone mutation;
- issue/Project mutation;
- PR mutation.

---

# Required completion report

## Artifact
Exact path.

## Adopted release version
Exact.

## Tag policy
Required/not-required; name; target; type; signing; message; push.

## GitHub Release policy
Required/not-required; metadata and notes.

## Milestone policy
Exact closure timing.

## Ordering
Exact sequence.

## Freshness/security
Exact rule.

## Idempotency
Exact rule.

## Future convention
Summary.

## Mutation statement
`RELEASE 1.9 VERSION/TAG PUBLICATION POLICY ADOPTION MUTATIONS: ZERO Git/GitHub mutations; one authorized policy artifact created`

## Next authority
If adoption succeeds:
`RELEASE 1.9 PUBLICATION POLICY ADOPTED — FRESH GPT-5.6 TERRA PUBLICATION EXECUTION AUTHORITY REQUIRED`

Terminal:
`RELEASE 1.9 VERSION / TAG PUBLICATION POLICY ADOPTION AUTHORITY COMPLETE`

---

# Required blocked report

State exact policy conflict and minimum decision/reconciliation needed.

Mutation marker:
`RELEASE 1.9 VERSION/TAG PUBLICATION POLICY ADOPTION MUTATIONS: ZERO Git/GitHub mutations`

Terminal:
`RELEASE 1.9 VERSION / TAG PUBLICATION POLICY ADOPTION AUTHORITY BLOCKED`

Do not emit COMPLETE unless exact tag, publication, milestone, ordering, freshness, security, and idempotency policy are all adopted with no remaining unresolved fields.

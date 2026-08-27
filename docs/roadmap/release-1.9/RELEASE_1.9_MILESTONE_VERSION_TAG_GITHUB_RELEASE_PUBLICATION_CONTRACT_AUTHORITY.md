# Release 1.9 — Milestone / Version-Tag / GitHub-Release Publication Contract Authority

**Status:** Contract definition complete; no publication mutation authorized by this document.
**Canonical merged commit:** `e4958721c9a581efbb2552134c00bc146c73f047`
**Canonical PR:** #238, merged from `6b7c2cac8c20e6033666e1dfaf160f629fb7894b`, frozen payload 286/286 paths.

## Decisions from canonical evidence

| Concern | Decision | Evidence and boundary |
|---|---|---|
| Milestone #58 | **M-DEFER** | #58 is the Release 1.9 milestone and is Open with 0 open / 13 closed. The finalization contract expressly leaves closure to a separate publication authority; the count alone does not authorize closure. |
| Git tag | **T-UNRESOLVED** | The repository has zero remote tags and no exact Release 1.9 tag identity, target/version mapping, tag type, message, or signing policy. Changelog SemVer guidance and the phrase “corresponding Git tag” do not select `1.9`, `1.9.0`, `v1.9`, or `v1.9.0`. |
| GitHub Release | **R-DEFER** | There are zero GitHub Releases and no canonical title, notes source, draft/prerelease, assets, latest-release, or tag policy. Release publication must follow a later narrow authority after tag identity is resolved. |

The decisions are independent. M-DEFER does not imply T-CREATE, and T-UNRESOLVED does not imply R-PUBLISH.

## Canonical evidence and preserved boundary

The accepted Release 1.9 definition, execution plan, file manifest, WP08–WP12 authorities, aligned documentation, contribution guidance, and finalization contract establish a simulated/replay-data visualization release that reuses the existing pipeline. They do not define a Release 1.9 version identifier or publication metadata. The accepted technical evidence is inherited only while `origin/main` remains exactly the canonical merge commit: build 0/0, .NET 339/339, Python 17/17, Streamlit 1.61.1, clean `pip check`, schema v4, security clean, and zero owned residue.

`docs/project/CHANGELOG.md` provides general Keep a Changelog and SemVer guidance, historical version examples, an Unreleased Release 1.9 plan entry, and a general release philosophy. It does not establish the exact Release 1.9 tag or GitHub Release identity. `docs/project/CONTRIBUTING.md` establishes descriptive branches, Conventional Commits, tests/build/docs/security review, and pull requests, but no tag or Release publication procedure. Historical PRs establish no sufficiently consistent tag or Release convention. Current repository workflow evidence contains no canonical tag/release automation that resolves the gap.

## Milestone contract

No milestone mutation is authorized under M-DEFER. A future execution authority may close only milestone #58 after independently proving: exact identity/title, 0 open / 13 closed, #233–#237 Closed/Done, PR #238 merged, `e4958721c9a581efbb2552134c00bc146c73f047` present on `origin/main`, and no required Release 1.9 issue outside the milestone. It must read back Closed and unchanged issue counts, with zero issue/Project mutations. If the later authority establishes that closure follows another event, it must state that event and ordering explicitly; it must not infer closure from the count.

## Version and tag resolution contract

Because T-UNRESOLVED is required, no tag may be created or pushed. A separate narrow tag/version reconciliation authority must first establish, from canonical repository evidence, the exact Release 1.9 version identity and tag name, then define:

- target commit, which must be the verified merged Release 1.9 commit unless canonical policy proves otherwise;
- annotated versus lightweight form;
- signed versus unsigned policy and tagger identity requirements;
- tag message and local creation/push permissions; and
- exact existing-tag behavior.

That authority must read all remote tags before mutation. An absent exact tag is creatable only after identity is fixed; an existing exact tag is reusable only when its target and metadata match; any conflicting target or identity is a hard stop. Moving or force-updating a tag is forbidden.

## GitHub Release contract

R-DEFER means no Release may be created or edited here. After T is resolved, a separate Release publication authority must define the exact tag, title, notes source, draft state, prerelease state, latest-release behavior, discussion/category behavior, and assets. It must decide whether accepted documentation is sufficient as notes and may not silently create a changelog or notes file. If assets are not explicitly required, publish no assets; never attach binaries, test outputs, certificates, signing artifacts, or generated archives. The notes must state factually that Release 1.9 visualization uses deterministic simulated/replay data where applicable and must not imply live brokerage/provider connectivity.

## Required future ordering

No ordering is executed by this contract. The smallest safe continuation is:

1. read back merged `origin/main`, all 13 milestone issues, Project metadata, tags, Releases, and the finalization contract;
2. run the tag/version reconciliation authority and resolve T before any tag action;
3. obtain a canonical decision on M and, if authorized, close #58 with immediate read-back;
4. create and push the exact tag only under the resolved tag authority;
5. publish or reconcile exactly one GitHub Release only under the resolved Release authority; and
6. read back final Git/GitHub state, preserving #233–#237 Closed/Done, Project metadata, PR #238 Merged, canonical `origin/main`, and the simulated/replay disclosure.

The later authorities may choose a different sequence only with canonical evidence; aesthetic ordering or common GitHub practice is insufficient. If milestone, tag, or Release decisions remain separate, each must report its own mutation accounting.

## Freshness, security, idempotency, and stop rules

Before any future publication mutation, require `origin/main == e4958721c9a581efbb2552134c00bc146c73f047`, no post-merge executable/documentation drift, and fresh or validly inherited acceptance evidence. If `origin/main` changes, stop for release-candidate revalidation. Require an approved Git-aware security scan with no findings, no secrets/private keys/certificate exports, no local signing configuration or signed binaries, and no machine-specific paths or local artifacts in published content. Local Smart App Control signing remains opt-in and local-only.

Matching existing state is a read-back/no-op: an already Closed #58, an exact tag at the expected commit, or one Release with exact metadata may be reused. A mismatched milestone count/state, tag target/name/type/signing metadata, duplicate Release, conflicting Release metadata, reopened issue, unsupported live-data claim, missing notes authority, security finding, residue, or any unrelated issue/Project mutation is a hard stop. Never duplicate, overwrite, force-push, move a tag, or repair implementation under a publication authority.

## Acceptance matrix

| ID | Gate | Evidence / pass condition | Stop condition |
|---|---|---|---|
| MAIN | Canonical merge | `origin/main` is exactly `e4958721c9a581efbb2552134c00bc146c73f047` | Any divergence or stale local evidence |
| ISSUES | Release issues | #233–#237 are Closed/Done and unchanged | Reopened, missing, or unexpected required issue |
| MS | Milestone | #58 is the Release 1.9 milestone, 0/13, state read back | Identity/count/state mismatch |
| MDEC | Milestone decision | M-DEFER is respected until a separate event/authority is proven | Closure inferred from count or tag/Release decision |
| VERS | Version identity | Exact identifier is established by canonical evidence | SemVer example substituted for evidence |
| TDEC | Tag decision | T-UNRESOLVED remains until reconciliation authority resolves it | Invented or implicit tag decision |
| TAG | Tag safety | Resolved exact name/target/type/signing and remote collision read-back | Wrong target, collision, or force update |
| RDEC | Release decision | R-DEFER remains until exact Release authority exists | Release inferred from tag or milestone |
| NOTES | Notes source | Existing canonical source or separately authorized notes artifact | Silent changelog/notes creation |
| DISC | Safety disclosure | Simulated/replay-only wording is factual and present | Claim of unsupported live/provider capability |
| SEC | Publication security | Scan clean; no secrets, keys, local signing material, or binaries/assets | Finding or private/local material |
| FRESH | Technical freshness | Canonical main unchanged and accepted evidence valid | Main drift or unvalidated executable/doc drift |
| ORDER | Sequencing | Separate decisions and read-back after each authorized mutation | Bundled or inferred action |
| IDEMP | Idempotency | Matching state is reused; conflict stops | Duplicate, overwrite, move, or force push |
| FINAL | Final state | Issues, Project, PR, main, milestone, tag, and Release exactly match the resolved authority | Any unexplained state or unrelated mutation |

## This Luna pass accounting

Only this file is created:
`docs/roadmap/release-1.9/RELEASE_1.9_MILESTONE_VERSION_TAG_GITHUB_RELEASE_PUBLICATION_CONTRACT_AUTHORITY.md`

No source, test, package, schema, Python, documentation outside this artifact, staging, commit, branch, push, PR, merge, issue, Project, milestone, tag, or GitHub Release mutation is authorized or performed. The artifact is governance-only and is excluded from the frozen 286-path PR payload.

`RELEASE 1.9 PUBLICATION CONTRACT AUTHORITY MUTATIONS: ZERO Git/GitHub mutations; one authorized contract artifact created`

`RELEASE 1.9 PUBLICATION CONTRACT DEFINED — FRESH GPT-5.6 TERRA PUBLICATION EXECUTION AUTHORITY REQUIRED`

RELEASE 1.9 MILESTONE / VERSION-TAG / GITHUB-RELEASE PUBLICATION CONTRACT AUTHORITY COMPLETE

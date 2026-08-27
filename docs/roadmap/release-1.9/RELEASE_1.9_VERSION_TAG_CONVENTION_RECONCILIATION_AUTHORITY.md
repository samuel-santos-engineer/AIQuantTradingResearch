# Release 1.9 — Version / Tag Convention Reconciliation Authority

**Model:** GPT-5.6 Luna  
**Scope:** Documentation-only reconciliation; no Git or GitHub lifecycle mutation.

## Frozen evidence

- PR #238 is merged to `main` at `e4958721c9a581efbb2552134c00bc146c73f047`; its frozen payload is 286/286 paths.
- #233–#237 are Closed/Done.
- Milestone #58 is Open with 0 open / 13 closed.
- The remote has zero tags and GitHub has zero Releases.
- Accepted publication decisions are M-DEFER, T-UNRESOLVED, and R-DEFER.
- Accepted technical evidence remains build 0/0, .NET 339/339, Python 17/17, Streamlit 1.61.1, clean `pip check`, schema v4, security clean, and zero owned residue.

## Binding source review

The Release 1.9 definition, execution plan, file manifest, roadmap, README, finalization contract, publication contract, contribution guidance, changelog, project metadata, historical release branches/PRs, tags, Releases, and tag/release workflow evidence were inspected. `docs/project/CHANGELOG.md` supplies general Keep a Changelog/SemVer guidance and says releases have a corresponding Git tag, but it does not select a Release 1.9 version identity or tag syntax. The milestone and branch supply the release label `1.9` and branch shorthand only. No project/package metadata establishes a 1.9 release version, and no historical tag, GitHub Release, or tag-triggered publication workflow establishes prefix, component count, tag type, signing, or message.

## Decision

**TAG-UNRESOLVED.** A tag may be part of the broad release philosophy, but the exact Release 1.9 identity and mechanics remain insufficiently canonical. The evidence does not distinguish `1.9`, `1.9.0`, `v1.9`, or `v1.9.0`; therefore this authority must not choose among them. No tag is required or permitted to be created by this authority.

Because the tag decision is unresolved, the minimum next step is a narrow human/GPT-5.6 Terra tag-convention decision authority that explicitly selects whether a tag is required or optional and, if so, fixes every field below from canonical evidence. A separate GitHub Release authority remains required unless its exact semantics are resolved in that same authority. Milestone closure remains **MILESTONE-STILL-DEFERRED**; no tag decision changes its timing.

## Required future tag contract if resolved

The next authority must lock the exact version identity and tag name; target commit (expected candidate: `e4958721c9a581efbb2552134c00bc146c73f047`, subject to a fresh SHA gate); annotated or lightweight form; signed or unsigned policy; exact message; tagger identity; local creation and remote push mechanism; workflow side effects; and relationship to GitHub Release and milestone #58. The Authenticode certificate used for local Smart App Control development is unrelated to Git signing unless explicit canonical evidence connects them.

Before creation, read all remote tags. An absent exact tag is creatable only after identity is resolved; an exact tag at the expected commit is an idempotent read-back/no-op; any wrong target or similar-but-different tag is a hard stop. Force-moving, deleting, renaming, or overwriting tags is forbidden. If `origin/main` advances from the canonical merge SHA, stop and require release-candidate revalidation.

## GitHub Release and milestone boundaries

The current decisions remain **RELEASE-STILL-DEFERRED** and **MILESTONE-STILL-DEFERRED**. No GitHub Release may be created or inferred from a future tag. A later authority must define exact title, tag, notes source, draft/prerelease/latest behavior, discussion/category, and assets. Notes must preserve the factual boundary that visualization uses deterministic simulated/replay data where applicable and does not provide live brokerage/provider connectivity. No local binaries, tests, certificates, signing artifacts, or generated archives may be uploaded unless explicitly authorized.

Milestone #58 must remain Open until a later authority explicitly selects closure timing and proves its identity, 0/13 counts, #233–#237 Closed/Done, merged PR, canonical `origin/main`, and no required issue outside the milestone. The count alone does not authorize closure.

## Decision matrix

| ID | Decision | Evidence / result | Stop condition |
|---|---|---|---|
| REQUIRED | TAG-UNRESOLVED | General changelog philosophy says corresponding tags, but no exact Release 1.9 requirement | Treating general guidance as exact authorization |
| VERSION | UNRESOLVED | Release label is 1.9; no package/project release identity | Selecting a SemVer example by convention |
| NAME | UNRESOLVED | Zero tags and no naming policy | Invented prefix or component count |
| TARGET | UNRESOLVED pending tag decision | Expected merged SHA is `e4958721...`, not yet an authorized tag target | Main SHA drift or pre-merge target |
| TYPE | UNRESOLVED | No historical tags/workflow evidence | Assuming annotated or lightweight |
| SIGN | UNRESOLVED | No Git-signing policy; Authenticode is separate | Reusing local certificate or inventing signing |
| MESSAGE | UNRESOLVED | No tag-message convention | Invented message |
| PUSH | UNRESOLVED | No tag-push mechanism documented | Force push or implicit mechanism |
| AUTO | NONE EVIDENCED | No tag-triggered publication workflow found | Unreviewed external side effect |
| RELEASE | RELEASE-STILL-DEFERRED | Zero Releases and no metadata policy | Inferring Release from tag |
| MILESTONE | MILESTONE-STILL-DEFERRED | #58 Open 0/13; prior contract defers closure | Closing from count or tag assumption |
| FRESH | REQUIRED | Main must remain exact canonical merge SHA with accepted evidence | Any post-merge drift |
| IDEMP | REQUIRED | Matching state read-back/no-op; conflicts stop | Duplicate, overwrite, move, or cleanup |

## Mutation boundary and accounting

This Luna pass creates exactly this governance artifact and performs no staging, commit, branch, push, tag, milestone, Release, PR, issue, or Project mutation. It does not alter source, tests, packages, schema, Python, or other documentation.

`RELEASE 1.9 VERSION/TAG RECONCILIATION MUTATIONS: ZERO Git/GitHub mutations; one authorized reconciliation artifact created`

The minimum next authority is a fresh GPT-5.6 Terra tag-convention decision/publication authority after the unresolved tag decision is explicitly resolved.

RELEASE 1.9 VERSION / TAG CONVENTION RECONCILIATION BLOCKED

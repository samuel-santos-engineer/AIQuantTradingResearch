# Release 1.9 — Version / Tag Publication Policy Adoption Authority

**Model:** GPT-5.6 Luna  
**Scope:** Forward policy definition only. No Git or GitHub lifecycle mutation is authorized here.

## Adopted policy

This authority explicitly resolves the prior `TAG-UNRESOLVED` result using the requested prospective policy. The accepted Release 1.9 human label remains **Release 1.9**; its canonical stable version identity is **`1.9.0`**. A Git tag is required: **`TAG-REQUIRED`**, with exactly one canonical tag **`v1.9.0`**.

The tag must be an **annotated, unsigned** tag targeting exactly the canonical merged `origin/main` commit:
`e4958721c9a581efbb2552134c00bc146c73f047`.

Its exact annotation message is:
`Release 1.9 — Real-Time Financial Data Visualization`

The local Smart App Control Authenticode certificate is unrelated to Git tag signing and must never be reused as Git signing material. Unsigned tags are the adopted policy until a separately approved Git-signing policy supersedes it.

The exact push policy is: create the tag locally, read it back, push only `v1.9.0` normally to `origin`, and read back the remote tag. Force-push, moving, deleting, or replacing a release tag is forbidden.

GitHub Release publication is required: **`GITHUB-RELEASE-REQUIRED`**. Create exactly one non-draft, non-prerelease Release for `v1.9.0` with title `Release 1.9 — Real-Time Financial Data Visualization`, GitHub’s normal latest-stable behavior, no discussion/category override, and no custom assets. GitHub-generated source archives are sufficient; never upload binaries, certificates, signing artifacts, test outputs, databases, or generated files.

Release notes must be concise and manually derived from accepted Release 1.9 evidence. They must cover the visualization summary, simulated/replay pipeline flow, Worker → canonical JSON → Python/Streamlit path, WP08 lifecycle/restart/cancellation, WP09 permanent integration/no-bypass coverage, schema v4 preservation, the bounded Windows atomic-replacement robustness fix, developer/security alignment, and the accepted 339/339 .NET, 17/17 Python, Streamlit 1.61.1, and clean `pip check` evidence. They must state plainly: **Release 1.9 visualization and demonstration flows use deterministic simulated/replay data where applicable; this release does not provide live market-data or brokerage/provider connectivity.** No new changelog or release-notes repository file is authorized by this policy.

Milestone closure is **`MILESTONE-AFTER-RELEASE`**: close milestone #58 only after the tag has been pushed and the GitHub Release has been created and read back successfully. The final milestone mutation is only `#58 Open → Closed`; issue counts must remain 0 open / 13 closed. #233–#237 remain Closed/Done and Project metadata remains unchanged.

## Exact execution order

1. Read back PR #238 as merged and verify `origin/main` is exactly `e4958721c9a581efbb2552134c00bc146c73f047`.
2. Verify #233–#237 are Closed/Done, #58 is the Release 1.9 milestone and Open at 0/13, and no unrelated required issue exists.
3. Verify the accepted technical/security/residue evidence remains valid.
4. Read all remote tags and Releases; require no conflicting `v1.9.0` tag or Release.
5. Create the exact annotated unsigned local `v1.9.0` tag at the canonical merge SHA and read it back.
6. Push exactly `v1.9.0` normally to `origin`; force-push is forbidden.
7. Read back the remote tag and exact target SHA.
8. Create the exact GitHub Release from `v1.9.0`, with the metadata and notes above.
9. Read back the Release and verify its tag, title, draft/prerelease flags, notes, and zero custom assets.
10. Close milestone #58 and read back Closed with unchanged counts.
11. Perform final read-back of main, tag, Release, milestone, issues, Project metadata, and PR #238.

No step may be reordered without a superseding authority.

## Freshness and security

Before tag creation and again before Release/milestone mutation, `origin/main` must equal the canonical SHA. If it advances, stop and require release-candidate revalidation. Inherited technical evidence is valid only while no executable repository content changed after the accepted PR merge: build 0/0, .NET 339/339, Python 17/17, Streamlit 1.61.1, clean `pip check`, schema v4, security clean, and zero owned residue.

Require a Git-aware secret scan with no findings, no private signing material, no Authenticode certificate/configuration, no local machine paths, no signed local binaries, and no uploaded local assets. The publication process must not alter source, tests, packages, schema, Python, or local signing configuration.

## Idempotency and stop conditions

- Existing `v1.9.0` at the exact canonical SHA and matching annotated/unsigned metadata is a read-back/no-op; any wrong target, wrong type/signing, or conflicting message stops.
- A similar tag such as `1.9.0`, `v1.9`, or `v1.9.0` is not an alias and must not be deleted or renamed automatically; report it as ambiguity and stop if it conflicts with publication.
- An existing Release for `v1.9.0` is a read-back/no-op only when title, tag, draft/prerelease state, notes disclosure, and assets match exactly; conflicting metadata or duplicate Releases stops execution.
- An already Closed #58 with expected counts is a read-back/no-op. An Open milestone may be closed only at step 10. Any identity/count/state mismatch stops.
- Any main SHA drift, reopened issue, security finding, unsupported live-data claim, residue, missing required note, failed publication, or unrelated mutation is a hard stop. No cleanup, retry that could duplicate publication, force update, or implementation repair is permitted.

## Reusable stable-release convention

Unless superseded by later governance, stable releases use `MAJOR.MINOR.PATCH`, one annotated unsigned tag named `vMAJOR.MINOR.PATCH` on the canonical merged default-branch commit, a normal push, one non-draft/non-prerelease GitHub Release with no custom assets, factual release notes, and milestone closure after successful Release publication. Release-specific authorities may override this prospectively, but may not create aliases or move existing stable tags.

## Decision matrix

| ID | Adopted policy | Evidence / gate |
|---|---|---|
| VERSION | `1.9.0` | Explicit policy adoption; compatible with changelog SemVer guidance and no contradictory Release 1.9 package version |
| REQUIRED | `TAG-REQUIRED` | Immutable stable release identity on merged main |
| NAME | `v1.9.0` | One canonical `v` + three-component stable format |
| TARGET | `e4958721c9a581efbb2552134c00bc146c73f047` | Canonical PR #238 merge commit |
| TYPE | Annotated | Exact adopted stable-release policy |
| SIGN | Unsigned | No Git-signing policy; Authenticode explicitly excluded |
| MESSAGE | `Release 1.9 — Real-Time Financial Data Visualization` | Exact adopted annotation |
| PUSH | Normal push of exact tag to `origin` | No force/move/delete |
| AUTO | No tag-triggered publication automation evidenced | Read workflows before execution; stop on newly discovered unsafe side effect |
| GHREL | `GITHUB-RELEASE-REQUIRED` | Exactly one Release from `v1.9.0` |
| NOTES | Manual concise evidence-derived notes | No silent repository changelog mutation |
| DISC | Mandatory simulated/replay-only disclosure | No live-provider implication |
| MS | `MILESTONE-AFTER-RELEASE` | Close #58 only after successful Release read-back |
| FRESH | SHA and evidence gates above | Any drift stops |
| IDEMP | Matching state no-op; conflict stops | No duplicates or overwrite |
| FUTURE | `vMAJOR.MINOR.PATCH`, annotated unsigned stable tag | Reusable unless superseded |
| FINAL | Tag, Release, and #58 Closed; main/issues/Project/PR unchanged | Final read-back required |

## Luna mutation boundary and accounting

This pass creates exactly this policy artifact. It performs no staging, commit, branch, push, tag, GitHub Release, milestone, issue, Project, or PR mutation. It leaves #58 Open and #233–#237 Closed/Done.

`RELEASE 1.9 VERSION/TAG PUBLICATION POLICY ADOPTION MUTATIONS: ZERO Git/GitHub mutations; one authorized policy artifact created`

`RELEASE 1.9 PUBLICATION POLICY ADOPTED — FRESH GPT-5.6 TERRA PUBLICATION EXECUTION AUTHORITY REQUIRED`

RELEASE 1.9 VERSION / TAG PUBLICATION POLICY ADOPTION AUTHORITY COMPLETE

# Release 1.9 — Finalization / PR / Git / Milestone / Tag / Release Contract

## Authority and role

This is the binding post-WP12 finalization contract. It is documentation-only
in the Luna pass that creates it. A fresh GPT-5.6 Terra execution authority is
required for any mutation below.

The least-permissive model is **F-SPLIT**:

1. one Terra authority may finalize the accepted 286-path Release 1.9 change
   set through a reviewed pull request and merge it into `main`; and
2. a separate narrow publication authority is required for milestone closure,
   tag creation, and GitHub Release publication because canonical evidence does
   not define a Release 1.9 tag or Release convention.

No authority may combine deferred publication actions by inference.

## Frozen entry state

The accepted predecessor is `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`.
Expected entry state is `main == origin/main`, ahead/behind `0/0`, staged paths
`0`, and the WP12 inventory of 286 non-ignored R1 paths (28 tracked and 258
untracked), with R2 absent and R5 absent. The current Luna contract file is
**governance-only and excluded** from the 286-path Release payload; its creation
must not silently expand R1.

GitHub entry state is #233–#237 Closed/Done, #237 Release 1.9 / P1 /
Engineering, milestone #58 Open with 0 open / 13 closed, and no Project item
creation or deletion authorized.

The technical predecessor remains build 0/0, .NET 339/339 (11/125/182/21),
Python 17/17, Streamlit 1.61.1, clean `pip check`, schema v4, WP08 18/18,
WP09 integration 4/4, WP09 architecture 8/8, security clean, and zero owned
residue.

## Canonical evidence and non-inference rules

`docs/project/CONTRIBUTING.md` establishes feature branches, focused commits,
Conventional Commits, pull requests, successful builds/tests, documentation,
and no-secret review. Historical PRs establish descriptive release-branch
practice, but do not establish a Release 1.9 tag, merge method, reviewer,
required check name, or GitHub Release format. The repository currently has no
authoritative Release 1.9 tag or GitHub Release convention.

Where this contract is silent, the Terra execution authority must stop and
request a narrow amendment; it must not substitute common GitHub practice.

## R1 change-set freeze

The exact R1 include set is the WP12 286-path inventory, regenerated immediately
before staging with:

```powershell
git diff --name-only
git ls-files --others --exclude-standard
```

The execution authority must materialize the resulting exact path list and
verify each path against its originating WP authority and manifest action:

```text
path → originating WP/authority → tracked or untracked → include
```

The list must contain exactly the accepted R1 paths and no path created by this
Luna contract. No wildcard staging is permitted. R1 drift, an unexplained new
non-ignored path, or a mixed file is a hard stop.

The execution authority must use explicit path arguments equivalent to
`git add -- <exact-path-list>`. `git add .` and `git add -A` are expressly
prohibited because they can capture R2/R3/R4 content. If an unintended path is
staged, unstage only that exact path with `git restore --staged -- <path>` and
preserve its working-tree contents.

Always exclude: `Directory.Build.local.props`; `.venv/`; `bin/`; `obj/`; TRX,
logs, caches, handoff/runtime files; temporary `aiq-*` roots; SQLite WAL/SHM/
journal files; PFX/P12/PEM/private-key material; machine signing settings;
generated binaries; and every R2, R3, R4, or R5 path.

## Finalization sequence

The Terra execution authority shall stop at the first failed gate.

1. Re-read the R1 inventory, all manifests/authorities, current Git state,
   #233–#237, milestone #58, tags, releases, and the finalization contract.
2. Require current `main` at the accepted predecessor, `origin/main` equal,
   ahead/behind `0/0`, and staged paths `0`. Preserve the dirty worktree.
3. Rerun freshness-gated build, full .NET, Python, Streamlit, `pip check`,
   schema-v4, security, documentation, and owned-residue checks. Any code,
   test, dependency, schema, or documentation drift since WP12 requires a new
   revalidation authority.
4. Create exactly the dedicated branch
   `release/1.9-real-time-financial-data-visualization` from the verified
   `main` predecessor. If it exists, it must point exactly at that predecessor
   or execution stops.
5. Stage only the exact regenerated R1 list. Inspect staged paths and the full
   staged diff; prove all exclusions remain absent. Accidental staging may be
   reversed with `git restore --staged -- <exact-path>` only, never by
   discarding working-tree content.
6. Create one focused Conventional Commit on that branch:
   `feat: finalize Release 1.9 real-time financial visualization (#233-#237)`.
   Do not amend, rebase, squash locally, or add unrelated fixes.
7. Run staged-content security checks where supported, then push normally to
   `origin` with upstream set to the exact branch. Force-push is forbidden.
8. Create one ready-for-review PR from the exact branch into `main` with title
   `Release 1.9 — Real-Time Financial Data Visualization`.
9. The PR body must enumerate WP01–WP12 completion, the 286-path manifest,
   .NET 339/339, Python 17/17, schema v4, the Windows atomic-replacement fix,
   simulated/replay-only warning, architecture/no-bypass boundary, security,
   residue, and documentation evidence. It must not claim real market data or
   use closing syntax for already-closed issues.
10. Require successful repository-configured checks, required maintainer
    review, and no unresolved review conversation. If the repository exposes no
    required check or review policy, stop for a narrow workflow amendment.
11. Merge only after the exact base/head, checks, approval, and diff are read
    back. The merge method is **not canonicalized by this contract**; stop and
    request a narrow merge-method authority rather than choosing merge, squash,
    or rebase.
12. After an authorized merge, fetch and fast-forward local `main`; verify
    `main == origin/main` and the merged commit contains exactly the R1 set.
13. Do not close milestone #58, create a tag, push a tag, or publish a Release
    under this contract. Those actions require the separate publication
    authority described below.

## Milestone, tag, and Release boundary

Milestone #58 must remain Open during F-SPLIT repository/PR finalization. Its
0-open/13-closed count does not authorize closure. A separate publication
authority may, only after merged-main read-back, verify all 13 issues and then
decide whether to close #58.

No canonical Release 1.9 tag string, annotated/lightweight policy, signing
policy, tag message, tag push rule, GitHub Release title, notes source,
draft/prerelease state, or assets policy was found. Therefore all are
**DEFERRED / NOT AUTHORIZED** here. A publication authority must define them
from authoritative evidence before mutation. It must stop on an existing tag or
Release whose identity or target conflicts with its contract.

## Validation and security gates

Fresh pre-commit and post-merge gates require build 0 warnings/0 errors, full
.NET 339/339 with the 11/125/182/21 distribution, Python 17/17, Streamlit
1.61.1, clean `pip check`, schema v4, WP08 18/18, WP09 integration 4/4,
architecture 8/8, truthful documentation/link/command checks, and the approved
Git-aware Gitleaks scan with no findings. Any changed R1 implementation/test
path invalidates inherited evidence and blocks until separately revalidated.

The security gate excludes ignored `.venv` third-party content from repository
scope, while scanning all tracked/non-ignored intended content. No secrets,
credentials, private keys, certificate exports, local signing props, signed
local binaries, or machine paths may enter the staged set. Local signing stays
opt-in development behavior and is never an App Control bypass.

After every phase, require zero owned Worker, testhost, Python, Streamlit,
probe/helper, listener, handoff sibling, temporary database/sidecar, and
harness-runtime residue. Never broad-kill or broadly clean unrelated resources.

## Idempotency and failure handling

Every existing branch, commit, push, PR, merge, milestone, tag, or Release must
be read back before reuse. An exact matching object is reusable; a differing
base, head, content, status, target, or metadata is a hard stop. Never create a
duplicate, force-push, overwrite a worktree, or repair implementation behavior
under finalization. Any conflict, failed check, review objection, merge
conflict, R1 drift, security finding, residue, or required new path stops the
authority and names the minimum follow-up authority.

## Expected states

Before Terra finalization: dirty R1 worktree preserved, `main` unchanged,
#233–#237 Closed/Done, #58 Open, no tag/Release mutation.

After the repository/PR split completes: the dedicated branch is merged into
`main`, local and remote `main` are synchronized, the R1 payload is present in
the merge commit, #233–#237 remain Closed/Done, and #58 remains Open. Tag and
GitHub Release state remain unchanged and undefined for Release 1.9.

## Acceptance matrix

| Gate | Required proof | Stop condition |
|---|---|---|
| R1 | Exact 286-path manifest and provenance | Drift, missing authority, mixed file |
| EXCL | All local/generated/secret exclusions absent | Any excluded path staged |
| FRESH | Fresh baseline and no post-readiness drift | Count or semantic drift |
| SEC | Approved scan and signing audit clean | Finding or private material |
| RES | Zero owned residue | Any owned process/listener/artifact |
| BRANCH | Exact branch/base and no conflict | Existing mismatch |
| STAGE | Exact-path staged set only | Unexpected staged path |
| COMMIT | One exact Conventional Commit | Extra/amended/unrelated commit |
| PUSH | Normal push to `origin` | Divergence or force-push need |
| PR | Exact base/head/title/body and one PR | Duplicate or mismatch |
| CHECKS | All configured checks and maintainer review | Missing/failing/unresolved |
| MERGE | Canonical merge method supplied separately | Method ambiguity/conflict |
| SYNC | Merged `main == origin/main` | Divergence |
| MILESTONE | Separate authority only; #58 stays open here | Premature closure |
| TAG | Deferred publication authority | Invented tag format |
| RELEASE | Deferred publication authority | Invented Release metadata |
| FINAL | Exact Git/GitHub read-back | Any unexplained state |

## This Luna pass accounting

Only this file is created. No staging, commit, branch, push, PR, merge, issue,
Project, milestone, tag, or Release mutation is authorized or performed. This
file is governance-only and excluded from the 286-path Release 1.9 payload.

`RELEASE 1.9 FINALIZATION CONTRACT AUTHORITY MUTATIONS: ZERO Git/GitHub mutations; one authorized contract artifact created`

`RELEASE 1.9 FINALIZATION CONTRACT DEFINED — FRESH GPT-5.6 TERRA FINALIZATION EXECUTION AUTHORITY REQUIRED`

RELEASE 1.9 FINALIZATION / PR-GIT / MILESTONE-TAG-RELEASE CONTRACT AUTHORITY COMPLETE

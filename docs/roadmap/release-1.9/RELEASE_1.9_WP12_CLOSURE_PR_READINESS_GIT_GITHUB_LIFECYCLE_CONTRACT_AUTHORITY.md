# Release 1.9 WP12 — Closure / PR-Readiness / Git-GitHub Lifecycle Contract

## Authority and role

This is the binding documentation-only contract for WP12 / issue #237. WP12
is **A — PR-READY-ONLY**: it inventories and proves that the accepted Release
1.9 change set is ready for a later, separately authorized Git/PR workflow.
WP12 does not stage, commit, create a branch, push, create or update a PR,
merge, tag, publish a Release, close milestone #58, or perform WP13+ work.
The later Terra execution authority may perform only the actions explicitly
authorized by a subsequent, separate lifecycle authority.

The canonical predecessor is `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`, with
`main == origin/main` and ahead/behind `0/0`. WP11 remains frozen at build
0/0, .NET 339/339, Python 17/17, Streamlit 1.61.1, clean `pip check`,
persistence schema v4, and zero WP11 repository mutation.

## Binding sources and entry state

The contract is derived from the Release 1.9 definition, execution plan, file
manifest, accepted WP08–WP11 authorities, issue #237, `docs/project/CONTRIBUTING.md`,
`docs/project/ROADMAP.md`, `README.md`, `.gitignore`, and the local Smart App
Control signing documentation. Issue #237 is Open/Backlog, has exactly one
Project #2 item `PVTI_lAHOCAzBgs4BfsiAzg33jmA`, and is Release 1.9 / P1 /
Engineering. Issues #233–#236 are Closed/Done. Milestone #58 is Open with
1 open and 12 closed issues. These facts must be read back before any later
authority acts.

## Dirty-worktree classification

The live entry inventory is exactly 269 entries: 29 tracked modifications and 240
untracked paths. Every path must be classified by the following evidence,
never by filename similarity alone:

| Class | Meaning | Later disposition |
|---|---|---|
| R1 Intended | Directly attributable to an accepted Release 1.9 WP01–WP12 authority, manifest path, and diff/new-file evidence | Candidate only; no staging under this authority |
| R2 Unrelated | Pre-existing user work outside accepted Release 1.9 scope | Preserve and exclude |
| R3 Local-only | `Directory.Build.local.props`, machine paths, certificates, private keys, passwords, and local signing configuration | Preserve locally; never commit |
| R4 Generated/evidence | `bin/`, `obj/`, TRX/results, handoff/runtime files, SQLite sidecars, logs, caches, and temporary roots | Exclude; remove only if a later authority proves ownership |
| R5 Ambiguous/mixed | Origin or hunk ownership cannot be proven, including a file containing unrelated hunks | Preserve and block whole-file staging |

R1 attribution requires an accepted authority, originating WP, manifest/path
ownership, and diff evidence. A later readiness report must name every R1
file and every R5 file. Clear generated descendants may be grouped only by an
explicit governed directory pattern; included source/documentation files and
all ambiguous files must be named exactly.

## Observed change-set inventory

The 29 tracked modified paths at entry are:

```text
.gitignore
README.md
docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
docs/project/ROADMAP.md
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Application/Datasets/DatasetSnapshotCandidate.cs
src/AIQuantTradingResearch.Application/Datasets/IMaterializeDatasetUseCase.cs
src/AIQuantTradingResearch.Application/Datasets/MaterializeDatasetUseCase.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionEvidence.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionResult.cs
src/AIQuantTradingResearch.Application/Pipelines/PipelineExecutionUseCase.cs
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetSchema.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteExperimentResultSchema.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs
src/AIQuantTradingResearch.Worker/PipelineExecution.cs
src/AIQuantTradingResearch.Worker/Program.cs
tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj
tests/AIQuantTradingResearch.Application.Tests/PipelineApplicationTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentCompositionTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentDiscoveryTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentPersistenceTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/FeatureCompositionTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/PipelineCompositionTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
```

The 240 untracked paths include 223 current files under
`docs/roadmap/release-1.9/` (the accepted Release 1.9 definitions, contracts,
authorities, and prompt pairs), plus accepted Release 1.9 implementation and
evidence paths under `docs/development/`, `eng/sec/`, `python/presentation/`,
`src/AIQuantTradingResearch.Application/`,
`src/AIQuantTradingResearch.Infrastructure/`,
`src/AIQuantTradingResearch.Worker/`, and `tests/`. The later Terra inventory
must expand those directory results with `git ls-files -co --exclude-standard`
and name each included R1 file exactly. No wildcard staging is permitted.

The following non-R1 classes are explicit exclusions: `.venv/`, `bin/`,
`obj/`, test-result and TRX directories, temporary `aiq-*` roots, handoff
siblings, SQLite `-wal`/`-shm`/journal files, SDK caches, all PFX/P12/PEM or
private-key material, `Directory.Build.local.props`, machine-specific signing
settings, and any path not proven by an accepted Release 1.9 authority.

## Mixed-file and signing rules

Do not stage an R5 file. Hunk-level staging is not authorized by this
readiness contract; a mixed file requires a separate reconciliation authority
that proves exact hunk ownership. Never reset, restore, stash, clean, overwrite,
or discard unrelated work.

`Directory.Build.local.props` is ignored local-only configuration and must
remain uncommitted. Local-development Authenticode signing for Windows Smart
App Control compatibility is opt-in development behavior, not a bypass. No
certificate export containing a private key, password, token, or machine-local
thumbprint may enter the change set. The accepted signing script and factual
documentation are candidates only when independently classified R1.

## Security, technical, and residue gates

Before any future authority stages or publishes anything, it must run the
existing repository security tooling over tracked/non-ignored content, inspect
the complete intended diff, prove all local signing exclusions, and scan the
staged content if supported. A secret, private key, generated binary, or
unexplained finding blocks all mutation.

Readiness inherits the accepted WP11 technical evidence unless a later
execution authority explicitly requires freshness: build 0 warnings/0 errors;
Domain 11/11; Application 125/125; Infrastructure 182/182; Architecture
21/21; total 339/339; Python 17/17; Streamlit 1.61.1; clean `pip check`;
schema v4; WP08 18/18; WP09 permanent scenarios and architecture gates; and
WP10 documentation gates. No WP12 test delta is authorized.

Before readiness is declared, inspect only owned resources and require zero
Worker, testhost, Python, Streamlit, listener, harness runtime, handoff temp,
test database, WAL/SHM/journal, and forbidden build/test residue. Standard
ignored result artifacts may remain only when repository rules permit them.

## Git and GitHub lifecycle boundaries

This authority is **STAGING-NOT-AUTHORIZED**, **COMMIT-NOT-AUTHORIZED**,
**BRANCH-CREATION-NOT-AUTHORIZED**, **PUSH-NOT-AUTHORIZED**, and
**PR-CREATE/UPDATE/MERGE-NOT-AUTHORIZED**. It defines readiness evidence only;
it does not invent a branch name, commit message, base/head, PR title/body,
reviewer, labels, merge method, or release convention. A later dedicated
authority must define those values before any such action.

The readiness report may propose an exact PR title/body and exact R1 path list,
but must not create a PR. It must leave `main` unchanged, preserve the dirty
worktree, and create no commit, branch, push, tag, GitHub Release, release
notes, changelog, roadmap edit, or Project item.

Issue #237 completion model is **L1 readiness complete**, but this Luna
authority performs no lifecycle mutation. A later explicit lifecycle authority
may set the unique #237 Project item to Done and close #237 only after all
readiness gates and any separately authorized Git/PR steps pass, preserving
Release 1.9 / P1 / Engineering. Milestone #58 is **M0**: closure is not
authorized; it remains Open even when #237 is complete. Tags and GitHub
Releases are not authorized. WP13 and all later work remain untouched.

## Required later readiness sequence

1. Re-read #237, the binding Release 1.9 artifacts, WP11, WP10, WP09, signing
   documentation, `.gitignore`, and contribution/release guidance.
2. Capture Git state and classify all 269 dirty entries using the R1–R5 rules.
3. Produce the exact R1 include list and explicit exclusions; block on R5.
4. Run security, technical, documentation, schema-v4, and residue gates.
5. Re-read Git status and prove repository mutation by WP12 is zero.
6. Report PR readiness only; do not stage or alter GitHub.

## Acceptance matrix

| ID | Required proof | Pass condition | Block condition |
|---|---|---|---|
| CSET | 269-entry path inventory and authority attribution | Every path classified; exact R1/R5 list | Unknown or mixed ownership |
| EXCL | Local/generated/secret exclusion audit | All exclusions preserved | Excluded content would be staged |
| SEC | Existing secret scan and signing audit | No secrets/private keys | Any finding not authoritatively cleared |
| TECH | WP11 baseline read-back or authorized rerun | 339/339, 17/17, build 0/0, schema v4 | Count drift or regression |
| RES | Owned process/listener/temp/database audit | Zero forbidden owned residue | Any owned residue |
| STAGE | Authority check | STAGING-NOT-AUTHORIZED recorded | Any staging attempt |
| COMMIT | Authority check | COMMIT-NOT-AUTHORIZED recorded | Any commit attempt |
| BRANCH/PUSH | Workflow check | No branch or push performed | Invented or unauthorized lifecycle |
| PR | Readiness report | Evidence-only, no PR mutation | PR create/update/merge |
| LIFE | #237 timing | Deferred to separate explicit lifecycle authority | Premature Done/Closed |
| MILESTONE | Milestone read-back | #58 Open; no mutation | Closure without M1 authority |
| RELEASE | Tag/release audit | No tag/Release mutation | Invented publication convention |
| PRESERVE | Git/GitHub final read-back | Dirty work and #233–#236 preserved; #237 Open/Backlog | Cleanup or unrelated mutation |

## Required mutation accounting

`WP12 CLOSURE/PR-READINESS CONTRACT AUTHORITY MUTATIONS: ZERO repository/Git/GitHub mutations; one authorized contract artifact created`

`WP12 CLOSURE/PR-READINESS CONTRACT DEFINED — FRESH GPT-5.6 TERRA EXECUTION/COMPLETION AUTHORITY REQUIRED`

## Terminal marker

RELEASE 1.9 WP12 CLOSURE / PR-READINESS / GIT-GITHUB LIFECYCLE CONTRACT AUTHORITY COMPLETE

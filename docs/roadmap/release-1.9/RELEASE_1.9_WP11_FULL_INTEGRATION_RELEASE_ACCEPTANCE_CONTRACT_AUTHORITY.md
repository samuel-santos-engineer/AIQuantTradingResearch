# Release 1.9 WP11 — Full Integration & Release Acceptance Contract

## Authority and role decision

This is the binding implementation-ready contract for WP11 / issue #236. The
canonical evidence supports **validation-only WP11**: WP09 already provides
permanent four-state integration and eight-rule architecture coverage, while
WP08 provides the accepted lifecycle proof. WP11 independently re-executes
those surfaces and performs release-level audits; it adds no production code,
tests, Python files, packages, schema changes, or migrations.

Exact test delta: **+0 .NET / +0 Python / +0 total**.
Expected post-WP11 totals remain **339/339 .NET** (Domain 11, Application 125,
Infrastructure 182, Architecture 21) and **17/17 Python**.

The canonical persistence schema is **v4**. WP11 preserves it and performs no
migration. JSON/read-model contract versions are separate from SQLite schema
version.

## Binding sources and entry boundary

This contract derives from the reconciled Release 1.9 definition, execution
plan, and file manifest; issue #236 and its unique Project #2 item
`PVTI_lAHOCAzBgs4BfsiAzg33jXQ`; the accepted WP03 schema-v4 authority; WP08
lifecycle authorities/tests; the accepted WP09 permanent integration and
architecture contract/tests; the accepted WP10 documentation contract/aligned
docs; current schema/bootstrap tests; and #237/WP12's closure/PR-readiness
boundary.

The entry state is #233/#234/#235 Closed/Done, #236 Open/Backlog with Release
1.9/P1/Testing, #237 Open/Backlog, and milestone #58 Open. The prior repository
boundary is `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`, with `main` and
`origin/main` synchronized at ahead/behind 0/0. These facts must be read back
before execution.

## Exact scenario catalog and source ownership

The release acceptance set is exactly the four permanent WP09 scenarios. WP11
re-executes them; it does not duplicate or alter them.

| Scenario | Canonical source owner | Required proof |
| --- | --- | --- |
| FI-READY | Replay `SimulatedLiveVisualizationExecution.Execute` through the existing Application pipeline, read-model use case, Infrastructure publisher, WP05 parser, WP06 frame, WP07 projection | Two deterministic ordered Replay observations produce Ready; correlate revision/source/target, points, latest/count/window, feature and factual statuses through the produced envelope to presentation inputs; prove no provider/SQLite bypass. |
| FI-WARMUP | Same real Replay/Application/publisher chain | One deterministic observation produces WarmUp; prove required-count metadata, no fabricated feature value, same-publication identity, and the governed downstream projections. |
| FI-EMPTY | Existing historical composition: `PipelineExecution.Execute` → `IPipelineExecutionUseCase.Execute` → `VisualizationReadModelUseCase.PublishHistorical` → existing publisher | Successful empty `HistoricalPresentationInputs` produces Empty with zero-window invariants and unavailable optional facts. Do not force Replay to publish Empty. |
| FI-FAILED | Existing historical composition and failed `PipelineExecutionResult` path above | Existing safe failure category/status produces Failed; prove semantic failure remains distinct from transport warning. Do not force replay-source failure through publication. |

For every scenario, accept only the existing permanent test proof:
`VisualizationPermanentIntegrationTests.cs` methods
`PiReadyRealReplayPipelinePublishesCanonicalReadyEnvelope`,
`PiWarmUpRealReplayPipelinePublishesCanonicalWarmUpEnvelope`,
`PiEmptyHistoricalCanonicalCompositionPublishesEmptyEnvelope`, and
`PiFailedHistoricalCanonicalCompositionPublishesSafeFailedEnvelope`.
`FI-STALE` is not a new scenario; stale retention remains covered by existing
WP06/WP07 behavior without inventing a wall-clock threshold.

## WP08 lifecycle gate

Re-run `WP08LifecycleDemonstrationTests` at **18/18**, including the accepted
Worker publication, independent Streamlit listener, governed probe/read-model
chain, bounded refresh, genuine changed publication, targeted CTRL_BREAK,
Worker exit 0, Worker A→B restart, stale-handoff rejection, and owned
process/listener/handoff/database cleanup. The existing Windows process-group
helper and Worker cancellation/liveness semantics are frozen. No WP08 test or
production file may change under WP11.

## Schema-v4 acceptance

Re-execute the existing schema/bootstrap evidence, especially
`SqlitePersistenceTests.VersionThreeDatabaseMigratesToVersionFourPreservingHistoricalSnapshotAndExperimentEvidence`,
`SqlitePersistenceTests.OpenConnectionWhenUnsupportedVersionExistsRejectsWithoutReplacingState`,
and the complete `SqlitePersistenceTests` and `SqliteDatasetTests` suites.
Prove `SqliteSchemaBootstrapper.CurrentVersion` and `PRAGMA user_version` remain
4, accepted v3→v4 migration remains behaviorally intact, unsupported versions
remain rejected, and no table/index/constraint/schema file changes occur. Do
not confuse this persistence boundary with JSON/read-model versions.

## Architecture and security gates

Re-run `VisualizationBoundaryRulesTests` at **8/8** and static/read-only
checks proving: presentation does not access SQLite or providers; Application
does not reference Infrastructure; Infrastructure owns the atomic publisher;
Worker does not launch or supervise Streamlit; Streamlit remains a read-only
consumer; the canonical JSON file is the Release 1.9 handoff; the Release 1.8
JSON-over-stdio endpoint remains separate; and WP08 helper/probe remains
acceptance-only. Verify local signing remains opt-in Debug/local-development
Authenticode only, with no secrets, private keys, PFX files, passwords, policy
disablement, or bypass guidance.

## Documentation/setup gates

WP10 owns documentation edits; WP11 has no documentation write paths. Perform
read-only validation of `README.md`,
`docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`,
`docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`,
`docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`, and
`docs/project/ROADMAP.md`: simulated/replay warning, v4 architecture truth,
CPython 3.13.15/.venv/pins, signing terminology, lifecycle/security claims,
branch→acceptance→PR→merge workflow, roadmap state, relative links, and
documented commands. Any inconsistency is a predecessor regression requiring
separate authority; WP11 must not edit these files.

## Exact path/action allowlist

Repository writable paths: **none**. Allowed actions are read-only inspection,
existing build/test execution, static architecture/security checks, schema
inspection, documentation/link/command validation, and owned residue
inspection. Standard test-result artifacts may be produced by normal tooling.
No source, test, Python, package, schema, migration, signing, or WP12+ path may
be created or modified.

After technical acceptance, GitHub actions are limited to the existing #236
Project item Status → Done and issue #236 → Closed, each with immediate
read-back. Project item creation/deletion and all other issue, milestone,
release, tag, branch, PR, and merge mutations are forbidden.

## Focused and full acceptance commands

From the repository root, run the existing solution with no restore or source
mutation:

```powershell
dotnet build AIQuantTradingResearch.slnx --configuration Debug --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --configuration Debug --no-restore --no-build --nologo --filter FullyQualifiedName~WP08LifecycleDemonstrationTests
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --configuration Debug --no-restore --no-build --nologo --filter FullyQualifiedName~VisualizationPermanentIntegrationTests
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --configuration Debug --no-restore --no-build --nologo --filter FullyQualifiedName~VisualizationBoundaryRulesTests
Push-Location .\python\presentation
..\..\.venv\Scripts\python.exe -m unittest test_realtime_financial_visualization.py
Pop-Location
```

The focused expected results are WP08 18/18, WP09 integration 4/4, WP09
architecture 8/8, and the permanent WP09 Python suite 4/4. Re-run the schema
tests named above and the existing WP05 3/3, WP06 6/6, and WP07 2/2 focused
suites. No new WP11 test count exists.

## Regression contract

- Build: 0 warnings / 0 errors.
- .NET: Domain 11/11, Application 125/125, Infrastructure 182/182,
  Architecture 21/21, aggregate 339/339.
- Python: 17/17 total, including WP05/WP06/WP07/WP09 governed suites.
- Streamlit: 1.61.1.
- `pip check`: clean.
- No unexplained skips or count drift.

## Residue matrix

After each focused run and the final regression, inspect only resources owned
by the run. Expected final state is zero owned Worker, testhost, Python probe,
Streamlit, helper, or harness processes; zero owned listeners; no stale or
temporary handoff siblings; no WP11-owned canonical handoff residue; no
temporary SQLite database, WAL, SHM, or journal sidecars; and no forbidden
WP11-owned `%TEMP%\aiq-*` roots. Standard test-result files may remain. Never
broad-kill processes or delete unrelated runtime files.

## Exclusions and lifecycle boundary

WP11 does not change production behavior, Replay semantics, WP05–WP10
semantics, schema/migrations, packages, dependencies, live provider/network
behavior, signing implementation, or documentation. It does not create tests,
PRs, branches, commits, merges, tags, or Releases; it does not close milestone
#58; and it does not implement WP12.

WP11 owns only #236 Project Status → Done and #236 issue → Closed after every
acceptance row passes. #237 remains Open/Backlog. WP12/#237 owns closure/PR
readiness and any later release workflow. Milestone #58 remains Open unless a
separate explicit release-closure authority authorizes its closure.

## Acceptance matrix

| ID | Exact gate | Expected result / blocker |
| --- | --- | --- |
| FI-READY | Existing WP09 Ready integration test | PASS; any source/path mismatch blocks. |
| FI-WARMUP | Existing WP09 WarmUp integration test | PASS; fabricated feature/value blocks. |
| FI-EMPTY | Existing historical Empty integration test | PASS; Replay-forced Empty blocks. |
| FI-FAILED | Existing historical Failed integration test | PASS; failure/transport conflation blocks. |
| FI-LIFECYCLE | WP08 focused lifecycle suite | 18/18, exit 0, zero owned residue; any failure blocks. |
| FI-SCHEMA | Bootstrap/migration suites and v4 inspection | v4 preserved, no migration/change; mismatch blocks. |
| FI-ARCH | `VisualizationBoundaryRulesTests` and static checks | 8/8, no bypass; any unauthorized dependency blocks. |
| FI-SECURITY | Signing/secrets/process-boundary audit | Dev-only signing, no secrets or policy weakening; violation blocks. |
| FI-DOCS | Read-only WP10 documentation/link/command audit | All claims/links/commands truthful; inconsistency blocks. |
| FI-BUILD | Full build | 0 warnings / 0 errors. |
| FI-DOTNET | Full solution regression | Exact 339/339 with stated distribution. |
| FI-PYTHON | Governed Python suites, Streamlit, pip check | 17/17, Streamlit 1.61.1, clean pip check. |
| FI-RESIDUE | Process/listener/handoff/database/runtime matrix | Zero owned residue; any leak blocks. |
| FI-SCOPE | Git diff/status and mutation audit | Repository mutation zero; any unauthorized path or GitHub action blocks. |

Every row must be recorded as PASS before #236 lifecycle mutation. A failed
row is not repaired by WP11; it requires the appropriate narrow authority.

## Required completion accounting

## Canonical persistence-schema proof

WP11 treats Release 1.9 SQLite persistence schema v4 as binding. Acceptance
must read the existing bootstrapper's `CurrentVersion = 4` and
`PRAGMA user_version = 4`, exercise the existing v3-to-v4 migration and
unsupported-version rejection tests, and prove that the accepted v4 schema
remains unchanged. WP11 performs no migration, bootstrap edit, or schema
mutation; JSON/read-model contract versions are separate from the SQLite
persistence schema version.

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT AUTHORITY MUTATIONS: ZERO production/test/Python/GitHub mutations; one authorized contract artifact created`

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT DEFINED — FRESH GPT-5.6 TERRA EXECUTION/COMPLETION AUTHORITY REQUIRED`

RELEASE 1.9 WP11 FULL-INTEGRATION / RELEASE-ACCEPTANCE CONTRACT AUTHORITY COMPLETE

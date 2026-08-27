# Release 1.9 — WP09 Permanent Integration and Architecture Test Contract + Manifest/Path Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only semantic + manifest/path authority** for Release 1.9 WP09, canonical issue **#234**.

WP09 implementation is currently blocked because the accepted Release 1.9 definition, execution plan, manifest, and issue #234 define only high-level permanent integration/architecture testing goals.

This authority exists to remove those ambiguities before any implementation begins.

No production mutation.
No test mutation.
No Python mutation.
No package mutation.
No schema mutation.
No GitHub mutation.
No WP10+ work.

---

# Verified predecessor state

Treat as binding unless current repository read-back disproves it:

- #233 Closed / Done.
- #234 Open / Backlog.
- #234 Project item:
  `PVTI_lAHOCAzBgs4BfsiAzg33XcY`.
- #234 metadata:
  - Release 1.9
  - Priority P1
  - Area Testing.
- Milestone #58 Open.
- `main` = `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`.
- ahead/behind 0/0.
- WP08 technical/lifecycle acceptance complete.
- WP09 has not started.

Do not mutate GitHub under this authority.

---

# Existing high-level WP09 requirements

The accepted Release 1.9 plan/issue require permanent coverage for:

1. deterministic replay → pipeline → read model → Streamlit;
2. architecture proof that UI does not bypass governed Application/Infrastructure boundaries;
3. no direct provider/SQLite access from presentation/UI;
4. deterministic Streamlit behavior;
5. permanent shared integration/architecture coverage;
6. preservation of all accepted WP02–WP08 behavior.

The current gap is not intent; it is missing exact acceptance semantics and path authority.

---

# Objective

Create one binding documentation artifact that defines:

- exact deterministic end-to-end integration scenarios;
- exact source/inputs and expected assertions;
- exact architecture dependency/bypass rules;
- exact Streamlit functional states and cleanup;
- exact permanent test ownership;
- exact shared predecessor test modification permissions;
- exact dedicated WP09 test paths;
- exact expected test-count delta;
- exact regression/residue gates;
- exact WP09/WP08/WP10 boundaries.

The output must be implementation-ready without inventing new production semantics.

---

# Canonical output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

No other documentation artifact is required unless repository governance explicitly demands a single cross-reference.

---

# Binding predecessor contracts

Read and preserve all relevant accepted artifacts, including:

- Release 1.9 definition;
- Release 1.9 execution plan;
- Release 1.9 manifest/path authority;
- WP04 visualization read-model contract;
- WP05 handoff/parser contract;
- WP06 visualization-frame contract;
- WP07 canonical semantic and presentation contracts;
- WP08 lifecycle/bounded-demonstration/residue contracts;
- WP08 final completion evidence;
- issue #234.

Do not redefine predecessor behavior.

If predecessor contracts conflict materially, STOP.

---

# Section 1 — WP09 scope boundary

Define WP09 as **permanent integration + architecture verification only**.

WP09 must not:
- add new production behavior;
- expand schema;
- add package dependencies;
- alter persistence semantics;
- alter Worker lifecycle;
- alter Replay;
- alter WP05 parser semantics;
- alter WP06 frame semantics;
- alter WP07 presentation semantics;
- alter WP08 lifecycle harness semantics.

Tests may exercise existing production behavior only.

Any production change discovered necessary during WP09 implementation requires a fresh separate authority.

---

# Section 2 — Deterministic end-to-end scenario set

Define the exact permanent scenarios.

At minimum include:

## Scenario E2E-READY
Purpose:
Prove a deterministic successful Replay observation sequence flows through the real governed chain:

Replay
→ pipeline/application execution
→ visualization read model
→ atomic JSON handoff
→ WP05 parser
→ WP06 frame
→ WP07 presentation projection
→ Streamlit-facing functional output.

Requirements:
- use deterministic existing Replay data;
- no live provider/network;
- use harness-owned temporary runtime/handoff/database;
- use existing production Worker/read-model path where feasible;
- exact publication identity correlation across chain;
- exact Ready-state assertions;
- exact latest/count/window assertions;
- exact snapshot/version/status metadata;
- exact WP07 five-section structure/order;
- no browser automation required unless existing accepted tests already require it.

## Scenario E2E-WARMUP
Purpose:
Prove deterministic warm-up/incomplete-window behavior through the same governed projection chain.

Define exact input observation count needed to produce WarmUp according to existing WP04/WP06 semantics.

Assert:
- state;
- count/window;
- latest;
- factual metadata;
- presentation sections;
- no fabricated error.

## Scenario E2E-EMPTY
Purpose:
Prove deterministic Empty/no-observation projection using existing contract.

Assert exact:
- state;
- zero count;
- unavailable/latest semantics;
- presentation rows;
- no provider/SQLite bypass.

## Scenario E2E-FAILED
Purpose:
Prove an existing governed failure representation flows through read-model/parser/frame/presentation without bypassing contracts.

Use only an already-existing deterministic failure seam/fixture.
Do not create new production failure semantics.

Assert:
- Failed state;
- pipeline status;
- semantic status values as already defined;
- transport warning remains distinct.

## Scenario E2E-STALE
Include only if accepted WP04/WP05/WP06/WP07 contracts expose deterministic Stale behavior suitable for permanent tests.

If included:
- define exact stale predecessor/new publication relationship;
- assert retained factual projection and stale marker;
- do not invent clock-based nondeterminism.

If existing semantics do not permit deterministic Stale integration without new production seams, explicitly exclude Stale from WP09 permanent E2E and preserve existing focused coverage.

---

# Section 3 — Scenario identity/correlation contract

Define the exact existing identifiers used to prove same-publication flow.

Use only accepted existing fields such as:
- revision;
- snapshot identity;
- snapshot version;
- existing replay logical tick if exposed.

Do not introduce a new correlation ID.

Permanent tests must prove:
`source publication == parser source == frame source == presentation source`.

---

# Section 4 — Deterministic Streamlit functional contract

Authorize permanent Python test coverage in:

`python/presentation/test_realtime_financial_visualization.py`

This path is WP09-owned permanent integration coverage.

Define exact test responsibilities:

- call existing parser/frame/presentation functions;
- exercise the existing Streamlit-facing projection/render orchestration without launching a long-lived interactive server unless absolutely required;
- use deterministic in-memory/temp handoff inputs;
- no browser automation;
- no network;
- no provider access;
- no SQLite access;
- no arbitrary sleeps;
- no persistent files.

Define exact expected states tested:
- Ready;
- WarmUp;
- Empty;
- Failed;
- Stale only if deterministic predecessor contract supports it.

For each state specify:
- input shape;
- expected frame state;
- exact latest/count/window;
- exact semantic metadata;
- exact WP07 section labels/order;
- exact transport-warning behavior;
- cleanup.

---

# Section 5 — Architecture boundary rules

Define exact prohibited dependencies and assertions for:

`tests/AIQuantTradingResearch.Architecture.Tests/VisualizationBoundaryRulesTests.cs`

or the canonical architecture-test path discovered in the repository.

Permanent rules must prove:

## Rule A — Presentation/UI must not reference Infrastructure persistence/provider implementation
Presentation layer must not directly depend on:
- SQLite implementation types;
- provider adapters;
- persistence stores;
- concrete Infrastructure repositories.

## Rule B — Presentation/UI must not reference Domain/application persistence internals beyond accepted contracts
Only accepted read-model/presentation contract symbols may cross the boundary.

## Rule C — Streamlit/Python presentation must not access SQLite/provider directly
Architecture evidence may use:
- static file/module inspection;
- import graph;
- deterministic source-rule assertions.

Do not add runtime monkeypatch tricks if static assertions are sufficient.

## Rule D — Worker remains producer, Streamlit remains consumer
No production process-supervision dependency:
- Worker must not launch Streamlit;
- Streamlit must not launch Worker.

## Rule E — JSON handoff remains the governed cross-process boundary
No alternate direct DB/provider path from Streamlit.

## Rule F — Release 1.8 JSON-over-stdio endpoint remains unrelated
WP09 must not expand or route presentation through the Release 1.8 endpoint.

Define exact assertion surfaces:
- namespace/reference rules for .NET;
- import/source rules for Python;
- file/module patterns.

Avoid vague “should not depend” language.

---

# Section 6 — Dedicated WP09 test paths

Authorize creation/modification only at exact permanent WP09 paths.

At minimum:

## Python permanent presentation/integration
`python/presentation/test_realtime_financial_visualization.py`

Ownership:
- WP09 permanent integration tests.

## .NET permanent integration
Create one dedicated WP09 test file in the most appropriate existing test project, exact path to be selected from repository conventions, e.g.:

`tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationPermanentIntegrationTests.cs`

This file owns deterministic end-to-end replay → read-model → handoff evidence that is not architecture-only.

## Architecture
Use/create exact architecture path:

`tests/AIQuantTradingResearch.Architecture.Tests/VisualizationBoundaryRulesTests.cs`

If this path already exists, WP09 may modify it.
If canonical repository path differs, use that exact path and document it.

No other new test files unless this authority explicitly names them.

---

# Section 7 — Shared predecessor test-path authority

Define shared predecessor paths as **read-only by default**.

WP09 may not modify:
- WP02 focused tests;
- WP03 focused tests;
- WP04 focused tests;
- WP05 exclusive tests;
- WP06 exclusive tests;
- WP07 semantic/presentation tests;
- WP08 lifecycle/diagnostic tests.

Exception:
A shared predecessor test may be modified only if:
1. it is the canonical permanent architecture/integration surface already identified by Release 1.9 manifest; and
2. this artifact names the exact file and exact allowed additive assertions.

Otherwise WP09 must create/use its dedicated paths.

No migration/deletion of predecessor tests.

---

# Section 8 — Exact .NET integration scenario contract

For the dedicated .NET permanent integration test, define:

- harness-owned temp runtime;
- deterministic Replay source;
- real production pipeline/application execution;
- real visualization read-model/publisher path;
- real canonical handoff JSON;
- exact identity fields;
- no Streamlit server launch required in .NET;
- Python presentation-chain invocation only if already governed by accepted WP08 seam and suitable for permanent use.

Important:
The WP08 Python probe was acceptance/demo-only.
Do not automatically repurpose it for WP09 permanent integration unless predecessor authority explicitly permits permanent reuse.

If permanent .NET→Python invocation lacks authority, define the .NET permanent test to stop at real handoff/read-model and let the Python permanent test own parser/frame/presentation coverage.

This split is preferred unless a permanent cross-language seam is already accepted.

---

# Section 9 — Cross-language permanent coverage split

Define the permanent chain as two complementary governed tests unless existing authority supports a single E2E process test:

## .NET permanent integration
Replay
→ Application/pipeline
→ read model
→ file publisher
→ canonical JSON handoff.

## Python permanent integration
canonical governed handoff fixture/input
→ WP05 parse
→ WP06 frame
→ WP07 sections
→ Streamlit-facing functional projection.

Require the fixture/input to be generated from or structurally identical to the governed canonical handoff contract, not a duplicate hand-authored schema with divergent semantics.

If a checked-in fixture is proposed, exact fixture path and ownership must be defined here.
Prefer temp-generated deterministic JSON in test code using existing contract helpers if possible.

---

# Section 10 — Streamlit cleanup contract

Permanent Python tests must prove:
- no Streamlit server process is left running;
- no listener is created unless a specific server-launch test is explicitly defined;
- temp handoff files removed;
- temp runtime dirs removed;
- no DB/provider access;
- no cache residue beyond process-local test state.

If using Streamlit testing APIs rather than server process, define that no listener/process assertion is needed.

---

# Section 11 — Expected test-count delta

Define exact planned permanent WP09 additions.

The artifact must state a concrete expected count, for example:

- .NET Infrastructure permanent integration: N tests;
- Architecture permanent rules: N tests;
- Python Streamlit permanent integration: N tests.

Choose exact counts based on the final scenario/rule set.

Do not leave “TBD”.

The implementation authority must explain any deviation.

---

# Section 12 — Regression gates

After WP09 implementation, require:

## Focused WP09
- all dedicated WP09 .NET tests;
- all permanent architecture tests;
- all WP09 Python tests.

## Predecessor preservation
- WP08 focused 18/18;
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2.

## Full .NET
Current accepted pre-WP09 baseline:
- Domain 11;
- Application 125;
- Infrastructure 178;
- Architecture 13;
- aggregate 327.

Expected post-WP09 aggregate:
`327 + exact authorized WP09 .NET delta`.

Require 0 failures.

## Build
0 warnings / 0 errors.

## Python
- exact predecessor counts above;
- exact WP09 permanent Python count;
- Streamlit 1.61.1;
- `pip check`.

---

# Section 13 — Residue gates

Permanent tests must leave:
- no Worker process;
- no Streamlit process;
- no Python probe process;
- no testhost residue attributable to WP09;
- no listener;
- no temp handoff;
- no temp DB/sidecars;
- no temp runtime root.

Standard test-result artifacts are allowed only in normal runner result directories.

---

# Section 14 — GitHub completion boundary

This artifact does not mutate GitHub.

Future implementation/completion authority may, only after all technical gates:

- set #234 Project Status → Done;
- close #234;
- read back;
- verify successor work package remains untouched.

No Project item creation/deletion.

---

# Section 15 — WP10+/release boundary

WP09 may not:
- implement later work packages;
- close milestone #58 unless canonical release plan explicitly makes WP09 the final required item and all other open Release 1.9 items are resolved.

At completion, report next eligible canonical WP from manifest.

---

# Section 16 — Required authority artifact contents

The created artifact must contain:

1. purpose/scope;
2. binding predecessor references;
3. exact scenario catalog;
4. exact assertions per scenario;
5. architecture rules;
6. exact test paths;
7. shared-path ownership table;
8. test-count delta;
9. regression matrix;
10. residue matrix;
11. GitHub boundary;
12. stop conditions.

---

# Section 17 — Stop conditions

Stop documentation definition if:

- issue #234 requirements materially conflict with accepted Release 1.9 manifest;
- deterministic scenario requires new production semantics;
- exact test path ownership cannot be resolved from repository conventions;
- architecture rule would prohibit an already-accepted predecessor dependency;
- permanent cross-language test requires repurposing a WP08 acceptance-only seam without authority.

If blocked, create no speculative contract.

---

# Mutation boundary

Allowed mutation:
- exactly one documentation artifact:
  `docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

All production/test/Python/GitHub mutations:
`ZERO`

---

# Required completion report

## Artifact
Exact path.

## Scenario catalog
Exact E2E scenarios.

## Architecture rules
Exact prohibitions/assertion surfaces.

## Test paths
Exact dedicated/shared paths and ownership.

## Test-count delta
Exact .NET/Python additions.

## Regression/residue
Exact gates.

## Boundary
WP08 frozen; WP10+ untouched.

## Mutation statement

`WP09 PERMANENT INTEGRATION/ARCHITECTURE CONTRACT AUTHORITY MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

## Next step

On success:

`WP09 PERMANENT INTEGRATION/ARCHITECTURE TEST CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH TERRA AUTHORITY`

---

# Terminal markers

Success:

`RELEASE 1.9 WP09 PERMANENT INTEGRATION AND ARCHITECTURE TEST CONTRACT + MANIFEST/PATH AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.9 WP09 PERMANENT INTEGRATION AND ARCHITECTURE TEST CONTRACT + MANIFEST/PATH AUTHORITY BLOCKED`

Do not emit COMPLETE unless the artifact fixes the exact scenarios, assertions, architecture rules, paths, counts, regression, and residue gates without inventing new production semantics.

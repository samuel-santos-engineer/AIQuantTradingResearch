# Release 1.9 — WP11 Full-Integration / Release-Acceptance Contract Authority

## Model
Use **GPT-5.6 Luna**.

## Purpose
Create the binding, implementation-ready acceptance contract for:

`WP11 — Full Integration and Acceptance — GitHub issue #236`

This is a **documentation-authority pass only**.

It follows the completed schema reconciliation:

`SCHEMA-V4-CANONICAL`

WP11 must preserve canonical SQLite persistence **schema v4**. WP11 does not migrate or change the schema.

No production, tests, Python, packages, migrations, signing, GitHub, or WP12+ mutation is authorized by this pass.

---

# Accepted entry boundary

Treat as binding unless current read-back disproves it:

- #233 Closed / Done.
- #234 Closed / Done.
- #235 Closed / Done.
- #236 Open / Backlog.
- #236 Project #2 item:
  `PVTI_lAHOCAzBgs4BfsiAzg33jXQ`
- #236: Release 1.9 / P1 / Testing.
- #237 Open / Backlog.
- milestone #58 Open, latest accepted state 2 open / 11 closed.
- `main == origin/main == 3a02f035a253e4e16f479e1866c9a5195f5cfbdb`
  at the previously verified boundary, ahead/behind 0/0.

Technical predecessor baseline:

- build: 0 warnings / 0 errors
- Domain: 11/11
- Application: 125/125
- Infrastructure: 182/182
- Architecture: 21/21
- total .NET: **339/339**
- governed Python: **17/17**
- Streamlit: **1.61.1**
- `pip check`: clean
- WP08 focused lifecycle: **18/18**
- WP09 permanent integration and no-bypass acceptance: complete
- WP10 documentation alignment: complete.

Schema boundary:

- Release 1.8 historical endpoint: schema v3.
- accepted WP03: v3 → v4 evolution.
- Release 1.9 current/canonical persistence schema: **v4**.
- WP09 preserves v4.
- WP11 must preserve v4 without migration.

---

# Canonical output

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_WP11_FULL_INTEGRATION_RELEASE_ACCEPTANCE_CONTRACT_AUTHORITY.md`

Do not create implementation files.

---

# Sources that must be read completely

1. reconciled `RELEASE_1.9_DEFINITION.md`
2. reconciled `RELEASE_1.9_EXECUTION_PLAN.md`
3. reconciled `RELEASE_1.9_FILE_MANIFEST.md`
4. issue #236
5. #236 Project item
6. accepted WP03 schema-evolution authority
7. accepted WP08 lifecycle/bounded-demonstration authorities and current focused tests
8. accepted WP09 permanent integration/architecture contract and current tests
9. accepted WP10 documentation contract and aligned documentation
10. current schema/bootstrap definitions/tests
11. #237 / WP12 scope sufficient to preserve the lifecycle boundary.

Do not infer acceptance requirements from conversation summaries where repository authority exists.

---

# Contract decision 1 — WP11 role

Determine from canonical evidence whether WP11 is:

## A. Validation-only
WP11 adds no production code and no executable tests. It independently re-executes permanent acceptance surfaces created by predecessor WPs and performs release-level audits.

If supported, lock:

- writable repository implementation paths: **none**
- .NET delta: **+0**
- Python delta: **+0**
- post-WP11 .NET: **339/339**
- post-WP11 Python: **17/17**

## B. Test-adding
Only choose this if #236 or another canonical source explicitly requires dedicated WP11 tests beyond permanent WP08/WP09 coverage.

If B:
- list every exact writable test path;
- exact test names/scenarios;
- exact .NET/Python deltas;
- exact post-WP11 totals;
- why predecessor permanent coverage cannot satisfy #236.

Do not add tests merely to create “independent” proof. Independent proof may be re-execution of permanent tests.

If neither A nor B is supportable, BLOCK.

---

# Contract decision 2 — Deterministic scenario matrix

Define the exact release-level scenario set.

Unless canonical #236 says otherwise, evaluate these four already-accepted state families:

## FI-READY
Source ownership:
- real accepted Replay-origin path;
- `SimulatedLiveVisualizationExecution.Execute` or exact current equivalent;
- Application/pipeline;
- visualization read model;
- canonical file publisher/handoff;
- WP05;
- WP06;
- WP07;
- Streamlit-facing projection.

Acceptance must prove:
- deterministic input;
- canonical Ready state;
- same-publication correlation through the governed presentation chain;
- no bypass.

## FI-WARMUP
Same Replay-origin ownership.

Acceptance:
- deterministic WarmUp state;
- canonical projection;
- same-publication correlation;
- no bypass.

## FI-EMPTY
Source ownership:
- accepted historical-composition path from WP09;
- `PipelineExecution.Execute`
  → `IPipelineExecutionUseCase.Execute`
  → `VisualizationReadModelUseCase.PublishHistorical`
  or exact current symbols.

Do not require Replay to publish Empty if frozen production does not do so.

## FI-FAILED
Use the accepted historical-composition failure path.

Do not require replay-source failure to publish Failed if current production returns before publication.

For every scenario, specify exact existing permanent test(s) that prove it.

If #236 requires fewer/more scenarios, state the exact canonical reason.

---

# Contract decision 3 — Full process/lifecycle acceptance

Define the exact WP11 lifecycle gate.

Use WP08 accepted semantics rather than inventing a new harness.

At minimum decide whether WP11 must re-run the complete current WP08 focused suite (**18/18**).

Release-level lifecycle evidence should cover, where already permanent:

- real Worker publication;
- independent Streamlit listener ownership;
- governed presentation chain;
- bounded refresh;
- genuine changed/new publication readiness;
- graceful targeted CTRL_BREAK;
- Worker exit 0;
- Worker A → Worker B restart;
- stale prior handoff not accepted as Worker B readiness;
- no forced kill on passing path;
- cleanup/residue.

If 18/18 is the required gate, state it exactly.

Do not authorize changes to WP08 tests.

---

# Contract decision 4 — Schema-v4 acceptance

Lock the following unless current source evidence requires more precise wording:

1. SQLite persistence schema version is **4**.
2. bootstrap current version remains 4.
3. `PRAGMA user_version` remains 4 after governed initialization/migration.
4. existing v3→v4 migration semantics remain accepted.
5. WP11 performs no migration change.
6. no schema definition/table/index mutation is authorized.
7. existing schema/bootstrap/migration tests are re-executed as acceptance evidence.
8. schema-v4 preservation is part of the full .NET regression.

Name exact existing tests/suites that prove these assertions.

Do not confuse persistence schema v4 with JSON/read-model envelope versions.

---

# Contract decision 5 — Architecture/security acceptance

Reuse WP09 permanent no-bypass rules.

The WP11 acceptance matrix must prove:

- Python/Streamlit does not access SQLite directly;
- Python/Streamlit does not access provider adapters directly;
- no unauthorized presentation → Infrastructure dependency;
- Worker/.NET remains producer;
- Streamlit/Python remains consumer;
- canonical JSON file handoff remains the Release 1.9 presentation cross-process boundary;
- Release 1.8 JSON-over-stdio endpoint remains separate;
- WP08 presentation-chain probe remains acceptance/test-only;
- local Smart App Control signing remains opt-in, Debug/local-development only;
- no secrets/private key material committed.

Name exact permanent architecture tests/static checks.

No new security package/tool.

---

# Contract decision 6 — Documentation/setup acceptance

WP10 owns documentation implementation. WP11 must not casually reopen it.

Determine whether #236 requires independent read-only release acceptance of:

- README simulated/replay warning;
- interoperability architecture;
- Python developer environment;
- Smart App Control local signing guidance;
- branch/PR workflow;
- roadmap;
- relative links;
- documented commands.

Preferred model if supported:
- WP11 validates these read-only;
- WP11 writable documentation paths: none;
- any discovered inconsistency is a predecessor regression requiring separate authority.

State exact checks.

---

# Contract decision 7 — Focused acceptance suite

Define exact focused WP11 execution before full regressions.

Prefer existing permanent surfaces.

The contract must name exact test files/classes/filters/commands for:

1. WP08 lifecycle
2. WP09 permanent integration
3. WP09 architecture/no-bypass
4. WP09 Python presentation
5. schema-v4 bootstrap/migration
6. any additional existing release acceptance gate required by #236.

State expected counts for each focused gate.

Do not write “run relevant tests.”

---

# Contract decision 8 — Test-count arithmetic

Lock exact counts.

Pre-WP11:

## .NET
- Domain 11
- Application 125
- Infrastructure 182
- Architecture 21
- total 339

## Python
- total 17

If validation-only:
- .NET delta +0
- Python delta +0
- post-WP11:
  - Domain 11
  - Application 125
  - Infrastructure 182
  - Architecture 21
  - total 339
  - Python 17.

If test-adding:
- provide exact new per-project counts and aggregate totals.

No ranges/TBD.

---

# Contract decision 9 — Full regression gates

Define:

## Build
- 0 warnings
- 0 errors

## .NET
Exact post-WP11 counts from the test-count contract.

## Python
Exact post-WP11 count.

## Environment
- Streamlit 1.61.1
- `pip check` clean.

## No unexplained skips
Any count deviation blocks completion until reconciled.

---

# Contract decision 10 — Residue matrix

Define exact post-acceptance residue proof.

At minimum inspect harness-owned:

## Processes
- Worker
- testhost
- Python
- Streamlit
- presentation probe/helper where applicable.

Expected:
- zero owned residue.

## Listeners
- zero owned Streamlit/listener residue.

## Runtime roots
- zero forbidden harness-owned `%TEMP%\aiq-*` roots attributable to acceptance runs.

## Handoff
Define exact accepted final state:
- remove test-owned handoff/runtime artifacts when the harness owns them;
- no temp atomic-publication siblings;
- no stale acceptance-owned handoff residue.

## SQLite
Define exact accepted final state for test-owned DBs:
- no forbidden DB;
- no WAL/SHM/journal residue after harness cleanup;
or the exact current permanent-test cleanup contract.

## Evidence
Standard TRX/test-result files may remain.

No broad deletion or process termination.

---

# Contract decision 11 — Exclusions

WP11 must explicitly exclude unless #236 says otherwise:

- production behavior changes;
- schema migration/version changes;
- package upgrades;
- live provider/network acceptance;
- release tagging;
- release publication;
- PR creation;
- PR merge;
- branch cleanup;
- milestone closure;
- WP12 implementation.

---

# Contract decision 12 — WP11/WP12 lifecycle boundary

Read #237 and the manifest.

The expected boundary from current evidence is:

## WP11 / #236
Owns:
- full integration;
- release acceptance evidence;
- #236 Project Status → Done;
- #236 issue → Closed.

## WP12 / #237
Owns:
- closure/PR readiness and any final release workflow assigned by canonical artifacts.

The contract must explicitly state:

- #237 remains Open / Backlog after WP11.
- milestone #58 remains Open after WP11 unless a separate explicit release-closure authority says otherwise.
- WP11 must not create/merge a PR, tag/publish a release, or close the milestone merely because acceptance passes.

---

# Exact path/action authority

The contract must state one exact allowlist.

If validation-only:

## Repository mutations
`ZERO`

## Allowed read/execute actions
- run existing build/tests;
- run existing Python suites;
- inspect schema;
- inspect architecture references;
- validate docs/links/commands;
- inspect processes/listeners/files/DB residue;
- collect standard test evidence.

## GitHub after technical acceptance
Only:
- #236 Project Status → Done
- #236 issue → Closed

If test-adding, replace repository mutation ZERO with exact test paths only.

---

# Required acceptance matrix

The artifact must contain a concrete table with every row defined.

Minimum rows:

| ID | Acceptance area |
|---|---|
| FI-READY | deterministic Ready |
| FI-WARMUP | deterministic WarmUp |
| FI-EMPTY | deterministic Empty |
| FI-FAILED | deterministic Failed |
| FI-LIFECYCLE | WP08 lifecycle/restart |
| FI-SCHEMA | SQLite schema v4 |
| FI-ARCH | architecture/no-bypass |
| FI-SECURITY | signing/secrets/boundary security |
| FI-DOCS | WP10 documentation/setup |
| FI-BUILD | build 0/0 |
| FI-DOTNET | exact .NET regression |
| FI-PYTHON | exact Python regression |
| FI-RESIDUE | process/listener/file/DB cleanup |
| FI-SCOPE | exclusions/path boundary |

For each row specify:
- exact source;
- exact executable/read-only proof;
- expected result;
- blocker condition.

---

# Authority precedence

For WP11 contract semantics use:

1. reconciled Release 1.9 definition/plan/manifest;
2. accepted WP03 schema-v4 evolution authority for persistence schema;
3. accepted WP08 lifecycle authority/tests for lifecycle;
4. accepted WP09 permanent integration/architecture contract for state/source/no-bypass ownership;
5. accepted WP10 documentation contract for docs/setup;
6. issue #236;
7. WP12/#237 boundary for post-WP11 lifecycle ownership.

If these conflict materially after schema reconciliation:
- BLOCK;
- name exact contradiction.

---

# Stop conditions

STOP and create no artifact if:

- v4 reconciliation is not present in the canonical planning docs;
- #236 requires new product semantics;
- exact scenario ownership conflicts with WP09;
- exact lifecycle ownership conflicts with WP08;
- exact test-count role cannot be decided;
- a production/test path is required but absent from canonical authority;
- WP11/WP12 closure ownership remains contradictory.

Do not invent a release policy.

---

# Mutation boundary for this Luna pass

Allowed:
- create exactly:
  `docs/roadmap/release-1.9/RELEASE_1.9_WP11_FULL_INTEGRATION_RELEASE_ACCEPTANCE_CONTRACT_AUTHORITY.md`

Everything else:
- production mutations: ZERO
- test mutations: ZERO
- Python mutations: ZERO
- package/schema/migration mutations: ZERO
- GitHub mutations: ZERO
- WP12+ mutations: ZERO.

---

# Required completion report

## Artifact
Exact path.

## Role decision
Validation-only or test-adding.

## Scenario matrix
Exact Ready/WarmUp/Empty/Failed ownership and proof.

## Lifecycle
Exact WP08 gate/count.

## Schema
Canonical v4 assertions and existing proof.

## Architecture/security
Exact permanent gates.

## Docs/setup
Exact read-only acceptance.

## Path/action allowlist
Exact.

## Test-count
Exact delta and post-WP11 totals.

## Focused suite
Exact files/classes/commands/counts.

## Full regression
Exact expected totals.

## Residue
Exact matrix.

## WP11/WP12 boundary
#236-only lifecycle; #237/milestone untouched.

## Mutation statement

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT AUTHORITY MUTATIONS: ZERO production/test/Python/GitHub mutations; one authorized contract artifact created`

## Next step

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT DEFINED — FRESH GPT-5.6 TERRA EXECUTION/COMPLETION AUTHORITY REQUIRED`

---

# Terminal markers

Success:

`RELEASE 1.9 WP11 FULL-INTEGRATION / RELEASE-ACCEPTANCE CONTRACT AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.9 WP11 FULL-INTEGRATION / RELEASE-ACCEPTANCE CONTRACT AUTHORITY BLOCKED`

Do not emit COMPLETE unless WP11 role, exact scenarios, schema-v4 acceptance, lifecycle, security/docs gates, path/actions, exact counts, regressions, residue, and #236-only lifecycle are all unambiguous.

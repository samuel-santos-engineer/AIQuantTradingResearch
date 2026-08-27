# Release 1.9 — WP11 Full-Integration / Release-Acceptance Scenario + Manifest/Path + Test-Count Contract Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only semantic/acceptance/path authority** for Release 1.9 WP11, canonical issue **#236**.

WP11 execution is currently blocked because the canonical Release 1.9 definition, execution plan, manifest, and issue #236 specify high-level acceptance intent but do not define an executable acceptance contract.

This authority exists solely to make WP11 implementation/validation unambiguous.

No production mutation.
No test mutation.
No Python mutation.
No documentation implementation mutation outside the single authority artifact.
No package/schema/signing mutation.
No GitHub mutation.
No WP12+ work.

---

# Verified predecessor state

Treat as binding unless current read-back disproves it:

- #233 Closed / Done.
- #234 Closed / Done.
- #235 Closed / Done.
- #236 Open / Backlog.
- #236 unique Project #2 item:
  `PVTI_lAHOCAzBgs4BfsiAzg33jXQ`.
- #236 metadata:
  - Release 1.9
  - Priority P1
  - Area Testing.
- #237 Open / Backlog.
- milestone #58 Open.
- latest accepted milestone count: 2 open / 11 closed.
- local `main` and `origin/main` match at:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`
- ahead/behind 0/0.

Accepted technical baseline entering WP11:

## .NET
- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- aggregate **339/339**

## Python
- governed aggregate **17/17**

## Other
- build 0 warnings / 0 errors
- Streamlit 1.61.1
- `pip check` clean
- WP08 focused 18/18
- WP09 permanent integration/no-bypass accepted
- WP10 documentation alignment accepted.

Do not change these baselines under this authority.

---

# Canonical WP11 high-level intent

Read and reconcile:
- `RELEASE_1.9_DEFINITION.md`
- `RELEASE_1.9_EXECUTION_PLAN.md`
- `RELEASE_1.9_FILE_MANIFEST.md`
- issue #236
- WP09 permanent integration contract
- WP10 documentation contract
- relevant WP08 lifecycle acceptance contracts
- any Release 1.9 release-acceptance / definition-of-done artifact
- WP12/#237 boundary.

The current high-level WP11 intent includes independent proof of:
- deterministic end-to-end behavior;
- regressions;
- schema v3 boundary;
- security/no-bypass;
- residue;
- exclusions/non-goals.

But exact executable semantics are missing.

---

# Objective

Create one binding WP11 authority that defines:

1. whether WP11 is validation-only or adds new tests;
2. exact full-integration acceptance scenarios;
3. exact scenario source ownership;
4. exact path/action allowlist;
5. exact schema-v3 acceptance assertions;
6. exact architecture/security assertions;
7. exact lifecycle/restart/cancellation acceptance;
8. exact documentation/setup acceptance;
9. exact residue matrix;
10. exact test-count delta and expected post-WP11 totals;
11. exact focused and full-regression gates;
12. exact #236-only GitHub lifecycle boundary;
13. explicit WP12/#237 ownership of closure/PR readiness.

The artifact must be implementation-ready without inventing new product semantics.

---

# Canonical output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_WP11_FULL_INTEGRATION_RELEASE_ACCEPTANCE_SCENARIO_MANIFEST_PATH_TEST_COUNT_CONTRACT_AUTHORITY.md`

No other artifact unless repository governance explicitly requires one cross-reference.

---

# Section 1 — WP11 role decision

The authority MUST decide exactly one:

## Option A — Validation-only WP11
WP11 adds **no new production/tests** and independently executes the already-permanent WP08/WP09 acceptance surfaces plus release-level checks.

If this is supported by canonical #236/plan/manifest, define:
- .NET delta `+0`
- Python delta `+0`
- post-WP11 .NET `339/339`
- post-WP11 Python `17/17`.

## Option B — Dedicated acceptance-test WP11
WP11 adds new permanent/acceptance tests.

If this is required by canonical sources, define:
- exact writable test paths;
- exact .NET delta;
- exact Python delta;
- exact post-WP11 totals;
- exact reason existing WP08/WP09 tests are insufficient.

Do not choose Option B merely for redundancy.

If canonical evidence does not support either decision, STOP.

---

# Section 2 — Full-integration scenario catalog

Define the exact WP11 scenario set.

Prefer reuse of already-accepted permanent coverage rather than duplication.

At minimum decide whether the release-level acceptance matrix requires these state paths:

## FI-READY
Prove deterministic Ready path using the existing accepted source boundary.

Likely source:
Replay-origin path already accepted by WP09.

Must assert as applicable:
- deterministic source;
- Application/pipeline result;
- read-model Ready;
- canonical handoff;
- schema v3-compatible transport boundary where relevant;
- WP05 parse;
- WP06 frame;
- WP07 sections;
- Streamlit-facing Ready state;
- same-publication identity;
- no bypass.

## FI-WARMUP
Same principles for WarmUp.

## FI-EMPTY
Use existing canonical historical-composition ownership accepted by WP09.

Do not require Replay to publish Empty.

## FI-FAILED
Use existing canonical historical-composition ownership accepted by WP09.

Do not require replay-source failure to traverse a path frozen production does not expose.

The authority may choose a subset only if #236 does not require all four. If all four are required, state that explicitly.

Do not invent Stale unless #236/release acceptance requires it.

---

# Section 3 — Scenario source ownership

For every scenario, name exact existing symbols/boundaries after repository inspection.

Expected accepted ownership from WP09:

- Ready/WarmUp:
  `SimulatedLiveVisualizationExecution.Execute`
  through existing Replay/Application/read-model/publisher chain.

- Empty/Failed:
  existing historical composition path:
  `PipelineExecution.Execute`
  → `IPipelineExecutionUseCase.Execute`
  → `VisualizationReadModelUseCase.PublishHistorical`
  or exact current equivalent.

Confirm exact repository symbols before writing.

No new source seam.

---

# Section 4 — Schema v3 acceptance contract

Define exactly what WP11 must prove about schema v3.

Use the repository's canonical schema-v3 contract and current transport implementation.

At minimum specify:
- exact schema/version field/value;
- backward/forward assumptions if already defined;
- canonical JSON envelope shape acceptance;
- no schema mutation under WP11;
- no acceptance of malformed/mismatched schema where current parser already rejects it;
- no duplicate hand-authored schema semantics.

If schema v3 is already permanently tested, WP11 may reference/re-execute those tests rather than add new ones.

Do not invent version behavior.

---

# Section 5 — Architecture/security acceptance contract

Define exact release-level security assertions by reusing permanent WP09 rules.

Require as applicable:

- presentation/UI does not access SQLite directly;
- presentation/UI does not access provider adapters directly;
- no unauthorized Infrastructure dependency;
- Worker remains producer;
- Streamlit remains consumer;
- canonical JSON handoff remains the presentation cross-process boundary;
- Release 1.8 JSON-over-stdio endpoint remains separate;
- WP08 acceptance-only helper/probe remains non-production;
- local-development signing remains dev-only.

If WP11 is validation-only, these are re-executed acceptance gates, not new test rules.

---

# Section 6 — Lifecycle/restart/cancellation acceptance

Define exact WP11 release-level lifecycle proof by reusing WP08 accepted semantics.

Require as applicable:
- independent Worker/Streamlit ownership;
- Worker startup canonical-handoff cleanup;
- bounded refresh;
- genuine changed/new publication readiness;
- graceful CTRL_BREAK cancellation;
- Worker A → Worker B restart;
- Worker B exit 0;
- no forced kill on passing path;
- listener/process cleanup;
- no stale prior-session handoff accepted as new readiness.

Specify whether WP11 re-runs the full WP08 focused 18/18 or a narrower canonical subset plus inherited evidence.

Prefer full 18/18 if release-level acceptance requires independent proof.

Do not modify WP08 tests.

---

# Section 7 — Documentation/setup acceptance

Define whether WP11 must independently validate WP10 documentation.

If yes, require read-only checks for:

- README simulated/replay warning;
- .NET/Python interoperability boundary;
- Python developer setup;
- Smart App Control local-signing cross-reference;
- branch/PR workflow accuracy;
- roadmap state;
- links and documented commands.

No documentation mutation under WP11 unless #236 explicitly authorizes it.

If documentation alignment is owned entirely by WP10 and WP11 only needs inherited evidence, state that precisely.

---

# Section 8 — Path/action allowlist

The authority MUST define exact writable paths/actions.

If WP11 is validation-only:
- repository writable paths: **none**
- allowed actions:
  - run existing tests;
  - run static/security checks;
  - inspect docs/links;
  - inspect schema/transport;
  - inspect residue;
  - perform #236 lifecycle at the end.

If WP11 adds tests:
- name each exact file path;
- state create/modify permission;
- state exact test contribution count.

No production path should be writable unless canonical #236 explicitly requires implementation changes.

If production changes appear necessary, STOP and request a separate semantic/path authority.

---

# Section 9 — Focused WP11 acceptance matrix

Define exact focused gates before full regression.

If validation-only, this may be a named selection of existing suites, e.g.:

- WP08 lifecycle 18/18;
- WP09 permanent integration 4/4;
- WP09 architecture 8/8;
- WP09 Python 4/4;
- schema-v3 focused tests;
- documentation/link/security checks.

The authority MUST list exact commands/suites/test files or exact symbol groups.

No vague “run integration tests.”

---

# Section 10 — Test-count contract

Fix exact arithmetic.

Pre-WP11:
- .NET 339
- Python 17

If validation-only:
- .NET delta +0
- Python delta +0
- post-WP11 .NET 339
- post-WP11 Python 17.

If test-adding:
- state exact deltas and totals.

No TBD.
No range.
No “approximately.”

Also state expected per-project .NET counts if additions occur.

---

# Section 11 — Full regression contract

Require:

## Build
0 warnings / 0 errors.

## .NET
Exact expected post-WP11 counts.

Predecessor reference:
- Domain 11
- Application 125
- Infrastructure 182
- Architecture 21
- aggregate 339.

## Python
Exact expected post-WP11 count.

Predecessor reference:
- 17.

## Environment
- Streamlit 1.61.1
- `pip check` clean.

No unexplained skipped tests.

---

# Section 12 — Release-level residue matrix

Define exact final residue checks.

At minimum:

## Processes
Zero owned:
- Worker;
- testhost;
- Python probe;
- Streamlit;
- other WP11 harness process.

## Listeners
Zero owned listener residue.

## Handoff
- canonical final state according to accepted contract;
- zero forbidden temp siblings;
- no stale harness-owned handoff.

## Database
- exact temporary DB final state;
- WAL/SHM/journal sidecars according to accepted contract.

## Runtime
- zero forbidden `%TEMP%\aiq-*`/harness roots owned by WP11;
- only standard test-result artifacts may remain.

No global cleanup.

---

# Section 13 — Exclusions / non-goals

WP11 must explicitly exclude:

- new production semantics;
- live provider/network testing unless already canonical;
- schema migration;
- package upgrades;
- release tagging;
- milestone closure;
- branch merge;
- PR readiness;
- release publication;
- WP12 implementation.

WP12/#237 owns closure/PR readiness per current manifest unless repository evidence says otherwise.

---

# Section 14 — GitHub lifecycle boundary

This authority performs no GitHub mutation.

Future Terra WP11 execution may, only after every technical acceptance row passes:

- set #236 Project Status → Done;
- close #236;
- read back;
- preserve #233–#235.

Forbidden under WP11 unless explicit canonical authority exists:
- close milestone #58;
- mutate #237;
- create/delete Project items;
- create PR;
- merge branch;
- tag release;
- publish release.

---

# Section 15 — Milestone boundary

State explicitly:

`Milestone #58 closure is NOT authorized by WP11 unless a separate canonical release-closure authority explicitly says otherwise.`

Current manifest evidence says WP12—not WP11—owns closure/PR readiness.

Therefore expected successful WP11 end state should normally be:
- #236 Closed / Done;
- #237 Open / Backlog;
- milestone #58 still Open.

If canonical sources contradict this, resolve before completing artifact.

---

# Section 16 — Release acceptance matrix structure

The binding artifact must define a table with exact rows such as:

| Acceptance area | Exact gate | Source/evidence |
|---|---|---|
| deterministic Ready | ... | ... |
| deterministic WarmUp | ... | ... |
| deterministic Empty | ... | ... |
| deterministic Failed | ... | ... |
| schema v3 | ... | ... |
| architecture/no-bypass | ... | ... |
| lifecycle/restart | ... | ... |
| docs/setup | ... | ... |
| .NET regression | ... | ... |
| Python regression | ... | ... |
| residue | ... | ... |
| exclusions | ... | ... |

Every future Terra completion report must mark every row PASS.

---

# Section 17 — Security acceptance details

Define exact security evidence acceptable for WP11:

- re-run WP09 Architecture rules if permanent;
- static import/reference inspection;
- read-only inspection of local signing docs/config where required;
- no secrets;
- no recommendation to disable security controls;
- no direct DB/provider bypass.

Do not introduce new security tools/packages.

---

# Section 18 — Documentation/link acceptance details

If docs are in WP11 acceptance:

- validate relative links;
- validate commands/paths exist;
- confirm simulated-data warning is still present;
- confirm branch/PR workflow reflects actual repository state.

No content edits unless WP11 path authority explicitly includes docs.

If a doc inconsistency is discovered:
- STOP;
- report as predecessor regression requiring separate authority.

---

# Section 19 — Exact scenario independence rule

WP11 is “independent proof” only in the sense of independently re-running/verifying accepted production behavior.

It does NOT require duplicating permanent tests if existing WP08/WP09 tests already provide deterministic executable proof.

The authority must distinguish:
- re-execution of permanent tests;
- additional WP11-owned tests.

This distinction determines +0 vs positive test delta.

---

# Section 20 — Stop conditions for definition

STOP with zero mutation if:

- canonical #236 requires a production change not yet authorized;
- exact final scenarios cannot be derived from accepted WP08/WP09 semantics;
- schema-v3 acceptance semantics are ambiguous;
- test-count decision cannot be justified;
- WP11/WP12 lifecycle boundary conflicts;
- milestone closure ownership conflicts;
- path ownership cannot be resolved.

Do not invent release policy.

---

# Mutation boundary

Allowed mutation:
- exactly one authority artifact:
  `docs/roadmap/release-1.9/RELEASE_1.9_WP11_FULL_INTEGRATION_RELEASE_ACCEPTANCE_SCENARIO_MANIFEST_PATH_TEST_COUNT_CONTRACT_AUTHORITY.md`

All production/test/Python/GitHub mutations:
`ZERO`

---

# Required completion report

## Artifact
Exact path.

## WP11 role
Validation-only or test-adding, with rationale.

## Scenario catalog
Exact scenarios and source ownership.

## Path/action allowlist
Exact writable paths/actions.

## Schema v3
Exact acceptance assertions.

## Security/lifecycle/docs
Exact acceptance gates.

## Test-count
Exact .NET/Python delta and post-WP11 totals.

## Regression
Exact focused/full suite requirements.

## Residue
Exact matrix.

## Lifecycle boundary
#236-only; #237 untouched; milestone closure prohibited unless separate authority.

## Mutation statement

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT AUTHORITY MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

## Next step

On success:

`WP11 FULL-INTEGRATION/RELEASE-ACCEPTANCE CONTRACT DEFINED — FRESH TERRA EXECUTION AUTHORITY REQUIRED`

---

# Terminal markers

Success:

`RELEASE 1.9 WP11 FULL-INTEGRATION / RELEASE-ACCEPTANCE SCENARIO + MANIFEST/PATH + TEST-COUNT CONTRACT AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.9 WP11 FULL-INTEGRATION / RELEASE-ACCEPTANCE SCENARIO + MANIFEST/PATH + TEST-COUNT CONTRACT AUTHORITY BLOCKED`

Do not emit COMPLETE unless the artifact fixes WP11 role, scenarios, paths/actions, schema/security/lifecycle/docs gates, exact counts, residue, and strict #236-only lifecycle semantics without implementation mutation.

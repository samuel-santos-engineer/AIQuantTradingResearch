# Release 1.9 — WP09 Consolidated Implementation + Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is the **fresh consolidated implementation, validation, and lifecycle-completion authority** for Release 1.9 WP09, canonical issue **#234**.

The newly created artifact:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

is the **binding WP09 semantic, scenario, assertion, ownership, manifest, and path authority**.

All WP09 implementation decisions must conform to that artifact.

WP08 / #233 is complete and frozen.
WP10+ must remain unstarted.

---

# Binding predecessor state

Treat as accepted unless current read-back contradicts it.

## WP08
- #233 Closed / Done.
- technical acceptance complete.
- no reopening of WP08 implementation.
- focused WP08 predecessor: 18/18.
- .NET pre-WP09 aggregate baseline:
  - Domain 11
  - Application 125
  - Infrastructure 178
  - Architecture 13
  - total 327
- Python predecessor:
  - WP05 3/3
  - WP06 6/6
  - WP07 semantic 2/2
  - WP07 presentation 2/2
- Streamlit 1.61.1.
- `pip check` clean.

## WP09
- #234 Open / Backlog.
- exactly one canonical Project #2 item expected.
- Release 1.9.
- Priority P1.
- Area Testing.
- milestone #58 Open.

## Contract-defined test delta
Exact authorized permanent WP09 additions:
- **+12 .NET**
- **+4 Python**
- **+16 total**

No unexplained deviation is permitted.

---

# Authority precedence

For WP09:

1. `RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`
2. canonical Release 1.9 definition/plan/manifest
3. accepted WP04–WP08 predecessor contracts
4. issue #234

If a conflict exists:
- STOP before mutation;
- report exact conflict.

Do not improvise.

---

# Objective

Implement exactly the permanent WP09 integration and architecture coverage defined by the binding artifact.

Then prove:
1. all focused WP09 tests pass;
2. WP08 and all predecessors remain green;
3. .NET total increases by exactly +12;
4. Python total increases by exactly +4;
5. build remains 0 warnings / 0 errors;
6. architecture/security/no-bypass rules pass;
7. residue is clean;
8. #234 is transitioned to Done/Closed only after every gate passes.

---

# Mutation scope

Only the exact test paths authorized by the binding WP09 artifact may change/create.

Do not modify production unless the binding artifact explicitly authorizes none—and it should be treated as test-only unless current artifact says otherwise.

Expected categories:
- dedicated .NET permanent integration test path;
- dedicated/permanent architecture rules test path;
- permanent Python Streamlit/presentation integration test path.

Shared predecessor tests:
- read-only unless the WP09 artifact explicitly names an exact file and exact additive assertion authority.

No package/schema/signing/Replay/WP08 mutation.

---

# Phase 0 — Read-only entry verification

Verify:
- branch;
- HEAD;
- `origin/main`;
- ahead/behind;
- staged/unstaged/untracked state;
- #233 Closed / Done;
- #234 Open / Backlog;
- exactly one #234 Project #2 item;
- milestone #58 state/counts.

Read the complete binding WP09 artifact and extract:
- four deterministic integration scenarios;
- exact architecture rules;
- exact paths;
- exact assertion surfaces;
- exact test counts;
- regression gates;
- residue gates;
- GitHub lifecycle boundary.

No mutation yet.

---

# Phase 1 — Predecessor baseline

Before WP09 mutation, run the entry gates required by the binding artifact.

At minimum:
- build;
- WP08 focused 18/18;
- relevant .NET predecessor suites if required;
- Python WP05/WP06/WP07 predecessor suites.

If predecessor gate fails:
- STOP before WP09 mutation unless the failure is clearly an environment issue and the artifact explicitly allows remediation.

Do not “fix” predecessor tests under WP09.

---

# Phase 2 — Create exact WP09 test surfaces

Create/modify only the exact paths named in the binding artifact.

Do not add helper paths unless explicitly authorized.

Every new test must map to one of:
- deterministic integration scenario;
- architecture no-bypass rule;
- permanent Streamlit/presentation functional scenario.

No generic testing framework.

---

# Phase 3 — Implement four deterministic integration scenarios

Implement the exact four scenarios from the binding artifact.

For each scenario:
- use deterministic governed input;
- preserve existing production semantics;
- prove exact expected state;
- prove exact publication/source identity where required;
- prove no provider/SQLite bypass;
- use harness-owned temp resources;
- clean all owned resources.

Do not add fifth scenario unless binding artifact explicitly authorizes it.

---

# Phase 4 — .NET permanent integration tests

Implement the exact .NET permanent integration coverage defined by the artifact.

Required behavior:
- real governed Replay/application/read-model/handoff chain as specified;
- no production shortcuts;
- no mock replacement where real integration is mandated;
- no network/live provider;
- deterministic assertions.

Do not repurpose WP08 acceptance-only Python probe unless the binding artifact explicitly authorizes it.

---

# Phase 5 — Architecture rules

Implement the exact architecture assertions defined by the binding artifact.

Must cover all specified no-bypass boundaries, including as applicable:
- Presentation/UI → no direct Infrastructure persistence/provider implementation;
- no direct SQLite/provider access;
- Worker remains producer;
- Streamlit remains consumer;
- JSON handoff remains governed cross-process boundary;
- Release 1.8 JSON-over-stdio endpoint remains unrelated;
- no process-supervision inversion.

Use static namespace/reference/import/source assertions exactly as defined.

No vague rule implementation.

---

# Phase 6 — Python permanent integration tests

Implement the exact +4 Python permanent tests at the authorized path.

Use existing WP05 parser, WP06 frame projection, WP07 presentation projection, and Streamlit-facing functional surface as defined.

No:
- network;
- provider;
- SQLite;
- browser automation unless explicitly required;
- long-lived Streamlit server unless binding artifact mandates it;
- arbitrary sleeps;
- persistent evidence files.

Assert exact states and cleanup.

---

# Phase 7 — Exact count gate before execution

Before running tests, statically count newly added tests.

Require:
- .NET additions = **12**
- Python additions = **4**

If not exact:
- STOP;
- reconcile only within authorized WP09 test scope.

Do not proceed with unexplained count delta.

---

# Phase 8 — Focused WP09 validation

Run only WP09-focused tests first.

Require:
- all +12 .NET pass;
- all +4 Python pass;
- architecture rules pass;
- build 0 warnings / 0 errors.

If any focused test fails:
- fix only within authorized WP09 test scope;
- if failure implies missing production semantics, STOP for new authority.

---

# Phase 9 — WP08 preservation

Run WP08 focused suite.

Require:
- **18/18 passed**.

No test deletion/skipping.

If WP08 regresses:
- STOP;
- do not patch WP08 under WP09 authority.

---

# Phase 10 — .NET regression

Run:
- Domain;
- Application;
- Infrastructure;
- Architecture;
- full solution.

Expected post-WP09 totals must reflect exact authorized +12 .NET delta from baseline 327.

The binding artifact should determine distribution across projects.

Require:
- total = **339/339** if all 12 are additive to current baseline and no pre-existing count changed;
- 0 failures;
- no unexplained skipped tests;
- build 0 warnings / 0 errors.

If per-project distribution differs from plan, explain exact authorized allocation.

---

# Phase 11 — Python regression

Run:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- WP09 permanent Python **4/4**;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

Require all pass.

---

# Phase 12 — Security/no-bypass audit

Run all architecture/security tests and static checks required by WP09.

Prove:
- zero direct SQLite access from presentation;
- zero direct provider access from presentation;
- zero unauthorized Infrastructure references;
- Worker/Streamlit ownership preserved;
- no alternate transport/bypass;
- no Release 1.8 endpoint reuse for WP09 presentation.

---

# Phase 13 — Residue

After focused and regression runs, verify zero owned:
- Worker process;
- Streamlit process;
- Python probe/process;
- testhost residue attributable to WP09;
- listener;
- temp handoff;
- temp DB/sidecars;
- temp runtime root.

Allow only standard test-result artifacts in governed result directories.

No broad process kills.

---

# Phase 14 — Scope audit

Produce:

`changed path → WP09 authority section → purpose → test count contribution`

Require:
- only authorized WP09 test paths changed;
- no production mutation;
- no package;
- no schema;
- no signing;
- no WP08;
- no WP10+.

If pre-existing worktree changes exist, preserve them and distinguish them from WP09 mutations.

---

# Phase 15 — #234 acceptance matrix

Before GitHub mutation, map every issue #234 requirement to:
- implementation;
- focused test;
- regression evidence;
- architecture/security evidence;
- residue evidence.

Every row must PASS.

If any row incomplete:
- #234 remains Open / Backlog;
- GitHub mutations zero.

---

# Phase 16 — Project item identity

Identify exactly one canonical Project #2 item linked to #234.

Record:
- item node ID;
- current Status;
- Release;
- Priority;
- Area.

Expected:
- Backlog;
- Release 1.9;
- Priority P1;
- Area Testing.

Resolve exact Status field ID and Done option ID.

If ambiguous:
- BLOCK;
- GitHub mutations zero.

---

# Phase 17 — GitHub lifecycle completion

Only after all technical gates pass:

1. set #234 Project Status → Done;
2. read back;
3. verify Release/Priority/Area unchanged;
4. close #234;
5. read back;
6. verify #233 remains Closed / Done;
7. verify milestone #58 state/counts;
8. identify next eligible canonical Release 1.9 work package;
9. do not start it.

No item creation/deletion.

---

# Phase 18 — Final read-back

Verify:
- #234 Closed / Done;
- #233 still Closed / Done;
- milestone #58 consistent;
- next WP untouched;
- repository/Git state expected;
- no owned residue.

---

# Completion gate

WP09 completes only if:

1. exact four deterministic scenarios implemented;
2. exact architecture rule set implemented;
3. +12 .NET tests exist and pass;
4. +4 Python tests exist and pass;
5. WP08 remains 18/18;
6. full .NET is exactly baseline +12 with 0 failures;
7. Python predecessors + WP09 pass;
8. build 0/0;
9. architecture/security no-bypass gates pass;
10. residue clean;
11. scope audit clean;
12. #234 Project Done;
13. #234 Closed;
14. #233 preserved;
15. WP10+ unstarted.

---

# Required completion report

## Binding authority
Name/path of WP09 contract artifact.

## Entry state
Git/GitHub/milestone/predecessor.

## Implementation
Exact changed paths.

## Test delta
- +12 .NET
- +4 Python
- +16 total.

## Scenario results
All four deterministic scenarios.

## Architecture/security
Exact rules and outcomes.

## WP08 preservation
18/18.

## .NET regression
Exact per-project and aggregate counts.

## Python regression
Exact results.

## Residue
Processes/listeners/files/DB/runtime.

## Scope audit
Authorized paths only.

## #234 acceptance matrix
All rows.

## GitHub
Project item ID, Done, Closed, read-back.

## Milestone
State/counts.

## Next eligible WP
Name/issue only.

---

# Mutation statements

Before lifecycle completion:

`WP09 GITHUB MUTATIONS: ZERO`

On success:

`WP09 LIFECYCLE GITHUB MUTATIONS: #234 PROJECT STATUS → DONE; #234 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Report repository mutations exactly by path.

---

# Stop conditions

STOP before mutation if:
- binding WP09 artifact is missing/contradictory;
- exact test paths/counts cannot be mapped;
- predecessor baseline is invalid.

STOP during implementation if:
- production change is required;
- package/schema/transport change is required;
- WP08 must be altered;
- permanent cross-language coverage requires an unauthorized seam.

STOP before GitHub mutation if:
- any focused/regression/security/residue gate fails;
- exact +12/+4 delta is not achieved;
- Project item identity ambiguous.

---

# Terminal markers

Success:

`RELEASE 1.9 WP09 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP09 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless all permanent WP09 tests are implemented, all acceptance/regression/security/residue gates pass, and #234 is authoritatively Closed / Done.

# Release 1.9 — WP08 Consolidated Implementation + Completion — Final Fresh Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This prompt is the fresh consolidated implementation/completion authority for Release 1.9 **WP08 — canonical issue #233**.

WP08 may be closed only if every implementation, Windows process-signal, finite-demonstration, residue, regression, preservation, scope, and GitHub lifecycle gate passes.

WP09 (#234) must remain Open / Backlog and unstarted.

---

# Binding authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_LIFECYCLE_LIVENESS_TEST_SEAM_CANCELLATION_ADAPTER_CONTRACT_PATH_AUTHORITY_DEFINITION.md`
3. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WINDOWS_ISOLATED_PROCESS_GROUP_CONSOLE_SIGNAL_TEST_HELPER_AUTHORITY.md`
4. Accepted Release 1.9 release definition/manifest.
5. Accepted WP05 runtime lifecycle, atomic handoff, refresh/retry, manifest/path authority, completed implementation, and deterministic test-isolation fix.
6. Accepted WP06 visualization-frame contract/path amendment and implementation.
7. Accepted WP07 semantic/presentation contracts and completed implementation/lifecycle.
8. Any accepted local-repository reconciliation/preservation rule still applicable to shared dirty paths.

Authority precedence:

- Artifact 1 controls finite lifecycle/demo/residue timing and acceptance semantics.
- Artifact 2 controls production cancellation adapter + `--wp08-test-liveness` seam.
- Artifact 3 controls Windows-only process-group launch and targeted `CTRL_BREAK_EVENT` test helper.
- Existing predecessor contracts control everything not explicitly amended.

If any binding authorities conflict, STOP.

---

# Preserved partial WP08 production work

The following partial authorized implementation already exists and must be preserved unless it conflicts with binding artifacts:

- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Known accepted behavior from the partial implementation:

- Worker builds.
- `--wp08-test-liveness` keeps Worker alive.
- a real canonical handoff is published.
- handoff contract validates.
- owned-process cleanup works.
- graceful process-level signal proof is not yet implemented because the old launcher lacked isolated process-group creation.

Do not discard or rewrite valid partial work gratuitously.

---

# Entry state

Expected:

- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor reference: **309/309**.
- build: 0 warnings / 0 errors.
- WP05 Python: 3/3.
- WP06 Python: 6/6.
- WP07 semantic: 2/2.
- WP07 presentation: 2/2.
- Streamlit 1.61.1.
- `pip check`: clean.
- no WP08 focused lifecycle test path yet unless created by a later authorized pass.
- no WP09 implementation.

Verify current reality.

Do not require a clean worktree. Preserve accepted predecessor and unrelated local changes.

---

# Objective

Complete WP08 by:

1. preserving/finalizing the already-authorized Worker cancellation/liveness implementation;
2. implementing the Windows-only isolated process-group helper defined by artifact 3;
3. implementing the exact WP08 lifecycle demonstration tests/harness;
4. executing the exact finite WP08 demonstration;
5. proving graceful `CTRL_BREAK` cancellation, restart, bounded refresh, independent Streamlit lifecycle, and all residue rules;
6. preserving predecessor behavior;
7. closing #233 / setting Done only after all gates pass.

---

# Phase 0 — Extract exact contracts

Before mutation, extract exact values/symbols from all three WP08 artifacts.

## From lifecycle/demo/residue artifact
Extract:
- total demonstration max;
- Worker startup/readiness timeout;
- Streamlit readiness timeout;
- observation timeout;
- bounded refresh window;
- graceful cancellation timeout;
- force fallback timeout;
- Streamlit shutdown timeout;
- restart timeout;
- harness polling interval;
- exact readiness conditions;
- success-observation condition;
- shutdown order;
- canonical handoff final-state rule;
- temp handoff rule;
- temp DB/sidecar rule;
- evidence/log rule;
- exact authorized paths.

## From cancellation/liveness artifact
Extract:
- `Console.CancelKeyPress` behavior;
- Worker CTS ownership/disposal;
- token propagation;
- intentional cancellation exit code 0;
- no pipeline-failure translation;
- `--wp08-test-liveness` exact activation;
- post-publication gate semantics;
- race behavior;
- authorized production/helper/test symbols.

## From Windows helper artifact
Extract:
- inherited-console/new-process-group model;
- `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- exact command-line quoting rules;
- environment propagation;
- stdout/stderr behavior;
- `GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, processGroupId)`;
- exact runner-protection argument;
- safe-handle/disposal rules;
- cleanup fallback;
- no Job Object;
- exact helper/test path;
- Windows-only platform behavior.

Build a checklist.

If any exact value/path remains unresolved, STOP.

---

# Phase 1 — Repository/GitHub verification

Read-only verify:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked paths;
- current content/diff of the three partial Worker files;
- #233 issue body/state/Project state;
- #232 Closed / Done;
- #234 Open / Backlog;
- milestone #58;
- exact authorized WP08 paths;
- no conflicting partial WP08 test/helper work;
- no WP09 implementation.

If current dirty state introduces ambiguity not covered by accepted reconciliation, STOP before mutation.

---

# Phase 2 — Predecessor gate

Before new WP08 mutation:

## .NET
- repository-standard build;
- full solution regression.

Reference: **309/309**.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

## Residue
Verify no harness-owned:
- Worker;
- Streamlit;
- listener;
- temp handoff;
- temp DB/sidecar

exists before tests begin.

Do not globally kill unrelated processes.

If predecessor gate fails, STOP.

---

# Phase 3 — Path/symbol hard gate

Every mutation must map to one binding artifact.

Expected authorized new paths:

- exact Windows process-group helper path from artifact 3;
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`.

Production mutations are limited to already-authorized partial Worker paths if final conformance adjustments are actually needed.

Do not create extra helpers.
Do not consume WP09 paths.
Do not repurpose predecessor-exclusive tests.

---

# Phase 4 — Reconcile partial Worker implementation

Inspect:

- `WorkerLifecycleCancellation.cs`
- `Program.cs`
- `SimulatedLiveVisualizationExecution.cs`

For each classify:

- already compliant;
- needs narrow conformance adjustment;
- conflicting.

Preserve compliant content.

Allowed final behavior:

`Console.CancelKeyPress`
→ `e.Cancel = true`
→ Worker-owned CTS
→ existing execution cancellation token
→ intentional cancellation exit 0
→ no pipeline failure.

`--wp08-test-liveness`
→ internal post-publication cancellation-aware gate
→ production default no-op
→ no Replay semantic change.

Any broader production change = STOP.

---

# Phase 5 — Implement Windows process-group helper

Create only the exact test-only helper path authorized by artifact 3.

Implement exact model:

- Windows-only;
- real Worker executable;
- `CreateProcessW`;
- inherited console;
- `CREATE_NEW_PROCESS_GROUP`;
- dedicated non-zero process-group ID;
- exact command line/environment;
- exact stdio behavior;
- safe native handles;
- deterministic disposal.

No `CREATE_NEW_CONSOLE` unless artifact 3 explicitly selected it.

No Job Object.

No new package.

---

# Phase 6 — Command-line/environment correctness

Implement the exact governed quoting/escaping rules.

Prove support for:
- executable/working paths with spaces;
- `--wp08-test-liveness`;
- existing Worker arguments;
- harness-owned environment configuration.

Do not invent new liveness env vars.

Add focused helper assertions only in authorized WP08 test surface.

---

# Phase 7 — Implement targeted CTRL_BREAK

Implement exact helper operation equivalent to:

`RequestCtrlBreak()`

Requirements:

- `GenerateConsoleCtrlEvent`;
- event = `CTRL_BREAK_EVENT`;
- group = owned Worker process-group ID;
- never group 0;
- no broadcast;
- API success/failure returned;
- repeated request safe.

Signal API success is not acceptance; Worker graceful exit is.

---

# Phase 8 — Test-runner safety

Prove by construction and focused assertions:

- Worker has its own non-zero process group;
- test runner remains in another group;
- no broad Ctrl+C;
- no image-name kill;
- no broadcast signal;
- no persistent global console-handler mutation.

If temporary handler manipulation is explicitly required by artifact 3, scope/dispose it exactly.

Otherwise do not add it.

---

# Phase 9 — Cleanup fallback

If graceful exit fails:

- acceptance test fails;
- helper may terminate only the owned Worker for cleanup;
- bounded wait;
- handle disposal;
- fallback is never counted as graceful cancellation success.

No global `taskkill`.

No unrelated process mutation.

---

# Phase 10 — Create WP08 lifecycle test harness

Create only:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

or exact binding path.

The harness may:

- launch real Worker via Windows helper;
- launch real Streamlit;
- allocate isolated runtime dir;
- allocate isolated temporary DB;
- allocate isolated loopback port;
- track exact PIDs/process handles;
- observe readiness;
- send real `CTRL_BREAK`;
- restart Worker;
- terminate Streamlit;
- audit residue.

Not production supervision.

---

# Phase 11 — Worker readiness

Use exact binding readiness:

- process alive;
- valid Worker publication;
- valid `aiq-visualization-read-model-v1`;
- accepted post-publication liveness condition.

No sleep-only proof.

Timeout => cleanup + test failure.

---

# Phase 12 — Streamlit readiness

Launch Streamlit independently.

Use exact loopback/port strategy.

Prove:
- listener belongs to Streamlit;
- readiness within timeout;
- Worker owns no new listener;
- no port collision.

No browser automation unless binding artifact explicitly requires it.

---

# Phase 13 — Real chain observation

Prove actual:

`Worker → atomic JSON → WP05 parser → WP06 frame → WP07 presentation projection`

Use Worker-produced handoff.

No fabricated fixture as sole evidence.

If artifact 1 separates live Streamlit readiness from deterministic parser/projection observation, follow it exactly.

---

# Phase 14 — Bounded refresh

Execute exact accepted sequential-observation protocol.

Prove:
- initial valid observation;
- required newer revision/publication;
- observed within configured bound;
- unchanged/equivalent does not fabricate transition;
- no retry/cadence redesign.

---

# Phase 15 — Graceful cancellation

Success path must be exactly:

1. launch Worker with `--wp08-test-liveness`;
2. observe real valid publication;
3. confirm Worker remains alive in liveness seam;
4. call `RequestCtrlBreak()`;
5. Windows API succeeds;
6. Worker `Console.CancelKeyPress` cancels Worker CTS;
7. liveness seam/execution observes token;
8. Worker exits within graceful timeout;
9. exit code = 0;
10. no pipeline-failure translation;
11. no forced cleanup was required;
12. no Worker process residue.

If forced kill is used, graceful-cancellation acceptance fails.

---

# Phase 16 — Cancellation races

Test exact binding race cases where authorized:

- cancellation before execution;
- during liveness seam/execution;
- repeated cancellation;
- natural completion before request does not count as graceful proof.

Do not modify Replay to manufacture timing.

---

# Phase 17 — Restart

Execute exact sequence:

- session A publication;
- graceful `CTRL_BREAK`;
- allowed post-A handoff state;
- session B starts at same isolated runtime path;
- B startup cleanup removes prior canonical file before new session publication;
- B publishes valid new envelope;
- Historical revision/session rule proven if required;
- Replay restart only if binding contract requires it.

---

# Phase 18 — Streamlit shutdown

Use exact binding method/order.

- least-forceful allowed termination first;
- bounded wait;
- forced fallback only if needed/allowed;
- listener released;
- no owned child residue.

No mutual production shutdown signaling.

---

# Phase 19 — Final shutdown order

Follow binding order exactly.

Record bounded evidence for:
- final observation;
- Worker graceful exit;
- Streamlit exit;
- cleanup.

No discretionary reordering.

---

# Phase 20 — Process/listener residue

After each focused lifecycle scenario and final demo:

Require:
- zero harness-owned Workers;
- zero harness-owned Streamlit processes;
- zero owned child processes;
- zero harness-owned listeners/ports.

Do not kill unrelated processes.

---

# Phase 21 — Handoff residue

Assert exact final state for:
- canonical handoff;
- temp siblings matching accepted pattern.

Any intermediate abrupt artifact must be cleaned by next Worker startup if contract requires it.

Streamlit never deletes handoff.

---

# Phase 22 — DB residue

Use harness-owned isolated DB.

Verify exact final state for:
- DB;
- WAL;
- SHM;
- journal/other governed sidecars.

Do not touch developer DB.

No schema change.

---

# Phase 23 — Evidence/log residue

Follow artifact 1 exactly.

If test output only:
- no persistent evidence dir.

If one evidence path is authorized:
- use only that path;
- keep bounded.

---

# Phase 24 — Focused WP08 acceptance

Run all authorized WP08 tests.

Report:
- exact count;
- duration;
- per-scenario result.

Map each #233 criterion to an assertion/evidence item.

No WP09 coverage counted.

---

# Phase 25 — Finite local demonstration

Run complete WP08 demonstration under exact total wall-clock maximum.

Record:
- start/end;
- total duration;
- Worker readiness;
- Streamlit readiness;
- real chain observation;
- bounded refresh;
- graceful CTRL_BREAK cancellation;
- restart;
- shutdown;
- residue.

Exceeding max duration = failure.

---

# Phase 26 — Python predecessor regression

Run:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

Verify no process/listener residue after suite.

---

# Phase 27 — .NET governed suites

Run:
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Reference predecessor: **309/309**.

Calculate exact expected delta from newly authorized WP08 .NET tests.

Require:
- 0 failed;
- 0 skipped unless existing governance permits;
- build 0 warnings / 0 errors.

---

# Phase 28 — Static scope audit

Prove zero unauthorized:
- production process supervisor;
- Worker↔Streamlit mutual control;
- custom IPC;
- named pipe;
- control file;
- socket/HTTP/WebSocket;
- Worker listener;
- PowerShell as canonical signal mechanism;
- broad taskkill;
- Job Object;
- Replay changes;
- schema/persistence/provider changes;
- refresh/retry redesign;
- WP06/WP07 semantic changes;
- package additions;
- WP09 implementation/tests.

List every changed path and authority.

---

# Phase 29 — Preservation audit

For partial Worker files:
- show pre-existing valid content preserved;
- identify only final conforming additions if any.

For shared predecessor files:
- prove accepted behavior unchanged.

Unrelated local state untouched.

---

# Phase 30 — #233 acceptance matrix

Before GitHub mutation:

Create table:

`#233 requirement → binding authority → implementation/test → demo evidence → residue evidence`

Every row PASS.

Also require:
- finite duration PASS;
- Python PASS;
- .NET PASS;
- scope PASS;
- preservation PASS.

Otherwise leave #233 Open / Backlog.

---

# Phase 31 — GitHub Project item identification

Only after technical acceptance:

Identify exactly one Project #2 item for #233.

Use robust typed API/GraphQL lookup and exhaustive pagination.

Resolve:
- item node ID;
- Status field ID;
- Done option ID;
- Release;
- Priority;
- Area/category.

If ambiguous:
- GitHub mutations zero;
- BLOCK.

No item create/delete.

---

# Phase 32 — GitHub lifecycle completion

After identity proof:

1. set exact #233 Project item Status → Done;
2. read back Done and unchanged metadata;
3. close #233;
4. read back Closed;
5. verify #234 Open / Backlog;
6. verify milestone state/counts.

Do not mutate #234.
Do not start WP09.

Do not close milestone unless accepted release governance independently requires it and no later Release 1.9 work remains.

---

# Phase 33 — Final residue/read-back

After lifecycle completion, no code changes.

Verify:
- no Worker residue;
- no Streamlit residue;
- no listener;
- handoff final state;
- DB final state;
- #233 Closed / Done;
- #234 Open / Backlog.

---

# Completion gate

WP08 completes only if:

1. partial Worker cancellation/liveness implementation conforms exactly;
2. Windows isolated process-group helper works safely;
3. targeted CTRL_BREAK gracefully exits Worker with code 0;
4. no forced kill is needed in passing graceful-cancellation path;
5. Replay unchanged;
6. focused WP08 tests pass;
7. finite demo passes within bound;
8. bounded refresh/restart/shutdown pass;
9. process/listener/handoff/DB residue passes;
10. predecessor Python passes;
11. .NET/build pass;
12. scope/preservation pass;
13. #233 Project = Done;
14. #233 Closed;
15. #234 Open / Backlog;
16. WP09 unstarted.

---

# Required completion report

## Binding authorities
List all three WP08 artifacts.

## Entry state
Repository/GitHub/baseline.

## Preserved partial implementation
Status of three Worker files.

## Windows helper
Exact model/APIs/flags/helper path.

## Graceful cancellation
Publication → liveness → CTRL_BREAK → CTS → exit 0.

## Demonstration
Exact bounded flow and duration.

## Refresh/restart/shutdown
Observed proof.

## Residue
Processes/listeners/handoff/temp/DB/logs.

## Tests
WP08 focused count.

## Python
WP05/WP06/WP07/compile/Streamlit/pip.

## .NET
Application/Infrastructure/Domain/Architecture/build/full total.

## Scope/preservation
Forbidden categories zero.

## #233 mapping
Requirement → proof.

## GitHub
#233 Closed / Done; metadata preserved; #234 unchanged.

## Milestone
Raw/canonical counts and state.

## Next eligible work
State exact canonical next WP.

---

# Mutation statements

If lifecycle completion succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Report repository mutations exactly by path.

---

# Stop conditions

Stop if:
- binding artifacts conflict;
- helper cannot target Worker group safely;
- test runner can be signaled;
- partial Worker work conflicts with binding definitions;
- required production change is outside authority;
- forced kill is required for acceptance;
- Replay changes are needed;
- predecessor regression fails;
- residue cannot be cleaned safely;
- WP09 ownership would be crossed;
- scope/preservation fails;
- Project identity ambiguous.

Preserve valid authorized partial WP08 work and keep #233 Open / Backlog.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless #233 is authoritatively Closed / Done and all Windows signal, graceful-cancellation, finite-demonstration, residue, regression, preservation, and scope gates pass.

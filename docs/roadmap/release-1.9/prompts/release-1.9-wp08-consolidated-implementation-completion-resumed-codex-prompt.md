# Release 1.9 — WP08 Consolidated Implementation + Completion — Resumed Fresh Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This prompt is the fresh consolidated implementation/completion authority for Release 1.9 **WP08 — canonical issue #233**.

WP08 may be closed only if every implementation, lifecycle, finite-demonstration, residue, regression, preservation, scope, and GitHub lifecycle gate passes.

WP09 remains unstarted.

---

# Binding authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_LIFECYCLE_LIVENESS_TEST_SEAM_CANCELLATION_ADAPTER_CONTRACT_PATH_AUTHORITY_DEFINITION.md`
3. Accepted Release 1.9 definition/manifest.
4. Accepted WP05 runtime-location/lifecycle, atomic handoff, refresh/retry, manifest amendment, completed implementation, and deterministic test-isolation fix.
5. Accepted WP06 visualization-frame contract and completed implementation.
6. Accepted WP07 semantic/presentation contracts and completed implementation/lifecycle.
7. Any accepted local-repository reconciliation/preservation rule still applicable to shared dirty paths.

The first WP08 artifact controls:
- clock bounds;
- readiness/success observation;
- bounded refresh;
- shutdown ordering;
- restart;
- residue;
- evidence ownership.

The second WP08 artifact controls:
- `Console.CancelKeyPress → Worker CTS → existing execution token`;
- intentional cancellation exit semantics;
- post-publication test-only liveness seam;
- `--wp08-test-liveness`;
- exact `Program.cs`, `SimulatedLiveVisualizationExecution.cs`, helper, and WP08 test-path authority.

If binding authorities conflict, STOP.

---

# Entry state

Expected:

- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor: **309/309**.
- build: 0 warnings / 0 errors.
- WP05 Python: 3/3.
- WP06 Python: 6/6.
- WP07 semantic: 2/2.
- WP07 presentation: 2/2.
- Streamlit 1.61.1.
- `pip check`: clean.
- no WP08 test path yet unless a later authorized partial attempt exists.
- no WP09 work.

Verify current reality.

Do not require a clean worktree. Preserve accepted predecessor and unrelated local work.

---

# Objective

Implement only the exact WP08 production adapter + liveness seam + focused harness/test surface authorized by the two WP08 definitions.

Then execute the exact finite WP08 demonstration proving:

- Worker readiness;
- Streamlit readiness;
- live Worker → atomic handoff → WP05 parser → WP06 frame → WP07 presentation observation;
- bounded refresh;
- graceful Worker cancellation;
- Worker restart;
- prior-session cleanup;
- independent Streamlit shutdown;
- process/listener cleanup;
- canonical handoff final state;
- temp handoff cleanup;
- temp database/sidecar cleanup;
- total demonstration duration within the accepted bound.

Only after all gates pass may #233 become Closed / Done.

---

# Phase 0 — Contract extraction

Before mutation, extract exact values from both binding WP08 artifacts:

## Lifecycle/demonstration
- total demo max duration;
- Worker startup timeout;
- Worker readiness timeout;
- Streamlit readiness timeout;
- observation timeout;
- refresh observation bound;
- graceful Worker cancellation timeout;
- forced fallback timeout;
- Streamlit graceful/forced shutdown timeout;
- restart timeout;
- harness polling interval;
- exact readiness conditions;
- exact success observation condition;
- exact shutdown order;
- exact canonical handoff final state;
- exact temp handoff rule;
- exact temp DB/sidecar rule;
- exact evidence/log rule.

## Cancellation/liveness
- exact `Console.CancelKeyPress` handling;
- exact CTS ownership/disposal;
- exact token propagation;
- exact intentional cancellation exit code = `0`;
- no pipeline-failure translation;
- exact `--wp08-test-liveness` semantics;
- exact post-publication gate behavior;
- exact race semantics;
- exact process-level proof;
- exact authorized paths/symbols.

Build a checklist from these exact values.

If any material value remains undefined, STOP.

---

# Phase 1 — Repository/GitHub verification

Read-only verify:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked paths;
- #233 body/state/Project state;
- #232 Closed / Done;
- #234 Open / Backlog;
- milestone #58;
- exact authorized WP08 production/test/helper paths;
- no incompatible pre-existing WP08 implementation;
- no WP09 implementation.

If shared dirty state introduces ambiguity not covered by accepted reconciliation, STOP before mutation.

---

# Phase 2 — Predecessor validation

Before WP08 mutation:

## .NET
- build;
- full regression.

Reference: **309/309**.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

## Residue
Verify no harness-owned residue before starting:
- Worker process;
- Streamlit process;
- listener;
- temp handoff;
- temp DB/sidecars.

Do not globally kill unrelated processes.

If predecessor gate fails, STOP.

---

# Phase 3 — Path/symbol hard gate

For every proposed mutation:

- exact path appears in one of the WP08 binding definitions;
- exact symbol/concern is authorized;
- shared predecessor ownership is preserved.

Do not invent a helper path.

Do not reuse WP05/WP06/WP07-exclusive tests.

Do not use WP09 paths.

If the cancellation definition authorizes one exact helper, use only that helper.

---

# Phase 4 — Implement production cancellation adapter

Implement exactly the accepted adapter:

`Console.CancelKeyPress → Worker-owned CancellationTokenSource → existing execution token`

Requirements:

- `e.Cancel = true`;
- CTS owned/disposed by Worker composition;
- token propagated to `SimulatedLiveVisualizationExecution.Execute`;
- repeated cancellation safe;
- cancellation before execution is preserved;
- intentional cancellation exits `0`;
- intentional cancellation does not become pipeline failure;
- no raw cancellation stack trace;
- natural finite completion unchanged.

No new package.
No new IPC.
No listener.
No Streamlit coupling.

---

# Phase 5 — Implement post-publication liveness seam

Implement exactly the accepted test/demo-only seam.

Fixed semantics:

- activated only by `--wp08-test-liveness`;
- internal/test-focused;
- post-publication;
- cancellation-aware;
- production default no-op;
- no user config key;
- no env-var contract;
- no production delay;
- no infinite loop;
- no Replay semantic change;
- no extra observation/tick/persistence.

The seam must let a real Worker process remain alive after a valid publication so the harness can send the real production cancellation signal.

Use only the exact helper/symbol visibility fixed by the binding definition.

---

# Phase 6 — Preserve Replay semantics

Hard gate.

Prove zero change to:

- three-observation Replay fixture;
- logical ticks;
- requested count;
- end-of-replay;
- duplicate behavior;
- source authority;
- replay cancellation semantics;
- pipeline stage behavior.

The seam must sit outside Replay semantics.

---

# Phase 7 — WP08 lifecycle test/harness implementation

Create only the exact WP08 test path authorized by the binding definitions.

Expected:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Use actual binding value if more precise.

The harness may:

- launch real Worker;
- launch real Streamlit;
- allocate isolated runtime directory;
- allocate isolated temp database;
- allocate harness-owned loopback port;
- track exact PIDs/process handles;
- observe readiness;
- send real cancellation signal;
- restart Worker;
- terminate Streamlit;
- audit residue.

It is not production supervision.

---

# Phase 8 — Isolation

Use only harness-owned resources:

- handoff runtime;
- DB;
- port;
- temp dirs;
- evidence files if authorized.

Never mutate/delete developer canonical runtime/database.

Track ownership explicitly.

---

# Phase 9 — Worker readiness

Use the exact binding readiness condition.

For the cancellation test, readiness must include the accepted post-publication/liveness condition.

No arbitrary sleep as proof.

On timeout:
- cleanup owned resources;
- fail.

---

# Phase 10 — Streamlit readiness

Launch Streamlit independently.

Use the exact loopback/port strategy.

Prove:
- Streamlit owns listener;
- readiness occurs within bound;
- Worker owns no new listener;
- no fixed shared-port conflict.

No browser automation unless binding artifact explicitly requires it.

---

# Phase 11 — Real success observation

Prove live:

`Worker → atomic JSON → WP05 parser → WP06 frame → WP07 presentation projection`

Use real Worker-produced handoff.

No fabricated JSON as sole proof.

If the binding definition separates live Streamlit readiness from deterministic parser/projection evidence, follow that exact split.

---

# Phase 12 — Bounded refresh

Execute the exact accepted sequential-observation protocol.

Prove:
- initial valid observation;
- required newer publication(s);
- within max window;
- no fabricated transition;
- no refresh/retry semantic changes.

---

# Phase 13 — Graceful cancellation

Execute exact flow:

1. Worker launched with `--wp08-test-liveness`;
2. valid publication observed;
3. liveness seam confirmed;
4. harness sends real production cancellation signal;
5. `Console.CancelKeyPress` requests CTS cancellation;
6. seam/execution observes token;
7. Worker exits within graceful timeout;
8. exit code = `0`;
9. no pipeline-failure translation;
10. process residue = zero.

Forced kill, if used, is cleanup fallback only and does **not** satisfy graceful cancellation acceptance.

---

# Phase 14 — Cancellation races

Add focused assertions for binding race semantics:

- cancellation before execution;
- cancellation during seam/execution;
- repeated cancellation;
- natural completion before request does not count as graceful-cancellation proof.

Do not broaden production behavior.

---

# Phase 15 — Restart

Execute accepted restart sequence:

- session A publishes;
- graceful cancellation/termination;
- allowed post-A canonical state;
- session B launches at same isolated runtime location;
- B startup removes prior canonical handoff before new-session publication;
- B publishes valid new envelope;
- Historical revision reset/no cross-session comparison if required;
- Replay restart only if binding definition requires.

---

# Phase 16 — Streamlit shutdown

Use exact accepted shutdown method/order.

- least-forceful allowed termination first;
- wait exact timeout;
- forced fallback only if allowed;
- listener released;
- owned child processes gone.

No Worker↔Streamlit shutdown signaling.

---

# Phase 17 — Final shutdown order

Execute the fixed sequence exactly.

Capture bounded evidence.

No timing discretion.

---

# Phase 18 — Process/listener residue audit

At each focused lifecycle case and final demo, verify exact final state:

- zero harness-owned Worker processes;
- zero harness-owned Streamlit processes;
- zero owned child processes;
- zero owned listeners/ports.

Do not kill unrelated processes by name.

---

# Phase 19 — Handoff residue audit

Assert the exact binding final state for:

- canonical handoff file;
- `.visualization-read-model.json.<suffix>.tmp` siblings;
- any intermediate crash/test artifact cleanup.

Streamlit must never delete handoff.

---

# Phase 20 — Database residue audit

Use isolated harness-owned DB.

Verify exact final state for:

- main DB;
- WAL;
- SHM;
- journal/other governed sidecars.

No persistence/schema changes.

---

# Phase 21 — Evidence/log rule

Follow binding artifact exactly.

If test output only, create no evidence directory.

If one evidence path is authorized, use only that exact bounded path.

---

# Phase 22 — Focused WP08 tests

Run all WP08-focused lifecycle tests.

Report exact count/duration.

Map each #233 criterion to an assertion.

No WP09 test counted.

---

# Phase 23 — Finite local demonstration

Run the full demonstration under the exact total wall-clock max.

Record:
- start/end;
- total duration;
- Worker readiness;
- Streamlit readiness;
- real success observation;
- bounded refresh;
- graceful cancellation;
- restart;
- shutdown;
- residue.

Exceeding max duration = failure.

---

# Phase 24 — Python predecessor regression

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

# Phase 25 — .NET governed suites

Run:
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Reference: **309/309**.

Explain authorized WP08 test delta exactly.

Require:
- 0 failed;
- 0 skipped unless already governed;
- build 0 warnings / 0 errors.

---

# Phase 26 — Static scope audit

Prove zero unauthorized:
- production supervisor;
- Worker starts/stops Streamlit;
- Streamlit starts/stops Worker;
- HTTP/WebSocket/queue/shared-memory/named-pipe/control-file IPC;
- Worker listener;
- schema/persistence/provider change;
- Replay change;
- refresh/retry redesign;
- WP06/WP07 semantic change;
- package addition;
- WP09 work.

List every changed path and authority.

---

# Phase 27 — Preservation audit

For shared predecessor files touched:
- identify preserved predecessor content;
- identify exact WP08 addition;
- prove no accepted behavior removed.

Unrelated dirty state untouched.

---

# Phase 28 — #233 acceptance mapping

Before GitHub mutation produce:

`#233 requirement → binding contract → implementation/test → demonstration evidence → residue evidence`

Every row PASS.

Also require:
- predecessor regressions green;
- finite demo within bound;
- scope/preservation green.

Otherwise leave #233 Open / Backlog.

---

# Phase 29 — GitHub Project item identification

Only after repository acceptance:

Identify exactly one Project #2 item for #233.

Use robust typed GitHub/API/GraphQL lookup and exhaustive pagination.

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

No item creation/deletion.

---

# Phase 30 — GitHub lifecycle completion

After identity proof:

1. set exact #233 Project item Status → Done;
2. read back Done + unchanged metadata;
3. close #233;
4. read back Closed;
5. verify #234 remains Open / Backlog;
6. verify milestone state/counts.

Do not mutate #234.

Do not start WP09.

Do not close milestone unless accepted release governance independently requires it and no later Release 1.9 work remains.

---

# Phase 31 — Final read-back/residue

Verify:
- #233 Closed / Done;
- #234 Open / Backlog;
- no harness-owned Worker;
- no harness-owned Streamlit;
- no listener;
- handoff residue correct;
- DB residue correct;
- no source mutation after lifecycle completion.

---

# Completion gate

WP08 completes only if:

1. cancellation adapter implemented exactly;
2. liveness seam implemented exactly;
3. Replay unchanged;
4. focused lifecycle tests pass;
5. finite demonstration passes;
6. refresh/cancellation/restart pass;
7. process/listener/handoff/DB residue pass;
8. Python predecessors pass;
9. .NET regression/build pass;
10. scope/preservation pass;
11. #233 Project = Done;
12. #233 Closed;
13. #234 Open / Backlog;
14. WP09 unstarted.

---

# Required completion report

## Binding authorities
Name both WP08 artifacts.

## Entry state
Repository/GitHub/predecessor baseline.

## Implementation
Exact paths/symbols.

## Cancellation adapter
Signal → CTS → execution token, exit semantics.

## Liveness seam
Activation/readiness/default no-op.

## Demonstration
Exact bounded protocol and duration.

## Refresh/cancellation/restart
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

If GitHub lifecycle succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Report repository mutations exactly by changed paths.

---

# Stop conditions

Stop if:
- binding artifacts conflict;
- liveness seam cannot be implemented without Replay change;
- required signal cannot be delivered in governed environment;
- unauthorized path required;
- predecessor regression fails;
- process/residue cannot be cleaned safely;
- WP09 ownership would be crossed;
- scope/preservation audit fails;
- Project item identity is ambiguous.

Preserve valid authorized partial WP08 work if safe, but do not close #233.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless #233 is authoritatively Closed / Done and all cancellation/liveness, finite-demonstration, residue, regression, preservation, and scope gates pass.

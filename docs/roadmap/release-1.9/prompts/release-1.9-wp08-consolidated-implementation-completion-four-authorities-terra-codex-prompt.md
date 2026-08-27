# Release 1.9 — WP08 Consolidated Implementation + Completion — Four-Authority Fresh Execution

## Model
Use **GPT-5.6 Terra**.

## Sole authority
Execute Release 1.9 **WP08 — canonical issue #233** to completion under this fresh consolidated authority.

WP08 may be closed only if every implementation, bounded-refresh, Windows graceful-cancellation, finite-demonstration, restart, residue, regression, preservation, scope, and GitHub lifecycle gate passes.

**WP09 / #234 must remain Open / Backlog and unstarted.**

---

# Four binding WP08 authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_LIFECYCLE_LIVENESS_TEST_SEAM_CANCELLATION_ADAPTER_CONTRACT_PATH_AUTHORITY_DEFINITION.md`
3. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WINDOWS_ISOLATED_PROCESS_GROUP_CONSOLE_SIGNAL_TEST_HELPER_AUTHORITY.md`
4. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIVENESS_SEAM_BOUNDED_REFRESH_RECONCILIATION_CONTRACT_AMENDMENT.md`

Also preserve:
- accepted Release 1.9 definition/manifest;
- accepted WP05 runtime/handoff/test-isolation contracts;
- accepted WP06 visualization-frame contract/implementation;
- accepted WP07 semantic/presentation contracts/implementation;
- accepted local-repository reconciliation/preservation rules.

Precedence:
- Authority 4 supersedes only the contradictory liveness-seam progression portion of authority 2.
- Authority 1 continues to control finite-demo timing, bounded refresh, restart, shutdown, and residue.
- Authority 2 continues to control production cancellation adapter, intentional cancellation, and test-liveness boundaries except where amended by authority 4.
- Authority 3 controls Windows process-group creation and targeted `CTRL_BREAK_EVENT`.
- Existing predecessor contracts govern all unaffected behavior.

If any remaining material conflict exists, STOP.

---

# Accepted partial WP08 implementation

Preserve valid existing WP08 work:

Production:
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Tests:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Already validated:
- focused WP08 **4/4 passed**;
- Windows `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- real Worker with `--wp08-test-liveness`;
- real atomic handoff publication;
- targeted `CTRL_BREAK_EVENT`;
- graceful Worker exit code `0`;
- deterministic Windows quoting;
- build 0 warnings / 0 errors.

The prior blocker was only:
- P1 immediately entered terminal cancellation hold;
- therefore P2 could not occur within the fixed 8-second refresh window.

Authority 4 resolves this with:
**P1 pass-through → normal genuine P2 → post-P2 cancellation hold.**

Do not reopen already-proven Windows signal work.

---

# Entry state

Expected:
- #232 Closed / Done.
- #233 Open / Backlog.
- #234 Open / Backlog.
- milestone #58 Open.
- full .NET predecessor reference before WP08 test delta: **309/309**.
- WP05 Python 3/3.
- WP06 Python 6/6.
- WP07 semantic 2/2.
- WP07 presentation 2/2.
- Streamlit 1.61.1.
- `pip check` clean.
- no GitHub mutation from blocked WP08 passes.

Verify current reality.

Do not require a clean worktree.
Preserve accepted predecessor, partial WP08, and unrelated local work.

---

# Objective

Complete WP08 by:

1. applying only the seam reconciliation authorized by authority 4;
2. preserving the existing cancellation adapter and Windows helper;
3. updating the focused WP08 tests only as authorized;
4. proving real P1 and genuine newer P2 within 8 seconds;
5. proving Worker remains alive after P2;
6. sending targeted `CTRL_BREAK_EVENT`;
7. proving graceful exit code 0 without forced kill;
8. completing Streamlit, refresh, restart, shutdown, process/listener/handoff/DB residue;
9. rerunning all predecessor regressions;
10. completing #233 lifecycle only after every technical gate passes.

---

# Phase 0 — Exact contract extraction

Before mutation extract exact values from all four authorities.

## Authority 1
Extract:
- total demonstration max duration;
- Worker startup/readiness timeout;
- Streamlit readiness timeout;
- observation timeout;
- exact 8-second bounded-refresh semantics/timer origin;
- graceful cancellation timeout;
- forced cleanup timeout;
- Streamlit shutdown timeout;
- restart timeout;
- polling interval;
- initial readiness;
- success observation;
- shutdown ordering;
- canonical handoff final state;
- temp handoff residue;
- temp DB/sidecar residue;
- evidence/log rules.

## Authority 2
Extract:
- `Console.CancelKeyPress`;
- `e.Cancel = true`;
- Worker CTS ownership/disposal;
- token propagation;
- intentional cancellation exit 0;
- no pipeline-failure translation;
- `--wp08-test-liveness`;
- exact authorized production symbols.

## Authority 3
Extract:
- inherited-console/new-process-group model;
- `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- exact quoting/environment/stdio;
- safe handles;
- `GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, processGroupId)`;
- runner protection;
- cleanup fallback;
- no Job Object;
- exact helper/test paths.

## Authority 4
Extract:
- exact P1 definition;
- exact P1 pass-through behavior;
- exact existing newer-publication comparison;
- exact P2 definition;
- exact post-P2 hold;
- per-process seam state;
- race behavior;
- cancellation readiness;
- exact production/test symbols authorized for amendment.

If any material value remains undefined, STOP.

---

# Phase 1 — Read-only repository/GitHub verification

Verify:
- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked paths;
- diffs of all five partial WP08 paths;
- no conflicting later WP08 changes;
- #233 state and Project item;
- #232 Closed / Done;
- #234 Open / Backlog;
- milestone #58;
- no WP09 implementation.

Classify current WP08 changes as accepted partial work vs conflicting.

Do not mutate until classification is safe.

---

# Phase 2 — Pre-mutation predecessor gate

Run:

## .NET
- build;
- full regression.

Reference pre-WP08 baseline: **309/309**.
Existing authorized WP08 tests may increase current total; explain exact delta.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

## Residue
Ensure no harness-owned Worker/Streamlit/listener/temp handoff/temp DB residue exists.

Do not kill unrelated processes.

If predecessor behavior is broken, STOP.

---

# Phase 3 — Path/symbol hard gate

New mutation is limited to exact symbols authorized by authority 4.

Expected primary production path:
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Expected test path:
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Do not modify:
- Windows helper unless authority 4 explicitly requires it (expected: unchanged);
- `WorkerLifecycleCancellation.cs` unless exact conformance is required by prior binding authority;
- `Program.cs` unless exact conformance is required;
- Replay source;
- WP05/WP06/WP07 production;
- WP09.

No new helper path.

---

# Phase 4 — Implement reconciled seam

Implement exactly:

## Without `--wp08-test-liveness`
- complete no-op;
- production execution unchanged.

## With `--wp08-test-liveness`

### P1
- produced by normal existing execution;
- real canonical atomic handoff;
- initial readiness;
- seam records/processes first invocation;
- **returns immediately**;
- no terminal hold.

### P2
- produced by next normal existing qualifying publication;
- must satisfy existing governed newer-publication rule;
- no fabricated data/tick/revision;
- after P2 publication seam enters cancellation-aware hold.

### Hold
- waits only on existing Worker cancellation token;
- blocks further replay progression if authority 4 requires exact P2 hold;
- no timeout inside production seam;
- no IPC release.

### Cancellation
- targeted CTRL_BREAK;
- existing `Console.CancelKeyPress`;
- Worker CTS;
- hold releases;
- graceful exit 0.

Use per-process/execution-local state exactly as authority 4 defines.

No static cross-session state.

---

# Phase 5 — Replay preservation hard gate

Prove zero change to:
- three-observation fixture;
- logical ticks;
- observation values;
- source ordering;
- end-of-replay;
- duplicate behavior;
- cancellation semantics;
- pipeline stages;
- publication semantics.

P2 must arise solely because P1 no longer blocks normal existing execution.

If normal execution cannot produce P2, STOP.

---

# Phase 6 — Focused test update

Preserve existing Windows/helper tests.

Add/update only authorized WP08 assertions:

1. P1 is real/valid.
2. P1 does not terminally block.
3. P2 is real and genuinely newer under existing rule.
4. P2 observed within **8 seconds** using existing timer origin.
5. Worker remains alive after P2.
6. targeted `CTRL_BREAK_EVENT` succeeds.
7. Worker exits code 0 within graceful timeout.
8. forced cleanup not used in passing path.
9. production run without flag unchanged.
10. Replay semantics unchanged.

Report exact focused count after changes.

---

# Phase 7 — Windows helper preservation

Re-run its accepted proof but do not redesign it.

Require:
- dedicated non-zero process group;
- group-targeted CTRL_BREAK;
- runner unaffected;
- safe handles;
- quoting tests;
- owned cleanup fallback only.

No Job Object.
No broad taskkill.
No PowerShell signal mechanism.

---

# Phase 8 — Initial readiness and bounded refresh

Run real Worker with test-liveness.

Observe P1 using exact readiness condition.

Start/continue the refresh timer exactly as authority 1 defines.

Observe P2:
- real Worker-produced;
- qualifying newer publication;
- within 8 seconds.

Do not reset timer for convenience.

If P2 misses bound, fail.

---

# Phase 9 — Cancellation readiness

Only after P2:
- verify Worker still alive;
- verify post-P2 hold is active through observable process behavior;
- then send CTRL_BREAK.

Cancellation after only P1 does not satisfy acceptance.

---

# Phase 10 — Graceful cancellation

Success path:

1. P1 observed.
2. P2 observed within 8 seconds.
3. Worker alive.
4. `RequestCtrlBreak()`.
5. `GenerateConsoleCtrlEvent` succeeds.
6. Worker cancellation handler cancels CTS.
7. hold observes token.
8. Worker exits within timeout.
9. exit code 0.
10. no pipeline-failure translation.
11. no forced kill.
12. process residue zero.

If fallback termination is needed, test fails graceful acceptance.

---

# Phase 11 — Streamlit readiness

Launch Streamlit independently according to authority 1.

Use isolated harness-owned loopback port.

Prove:
- Streamlit readiness within bound;
- listener ownership;
- Worker has no listener;
- no production mutual supervision.

Follow authority 1 ordering relative to P1/P2 exactly.

---

# Phase 12 — Real chain observation

Prove:

`Worker → atomic JSON → WP05 parser → WP06 frame → WP07 presentation`

Use real Worker-produced P1/P2 handoff.

No fabricated JSON as sole evidence.

Follow authority 1 exact split between Streamlit readiness and deterministic projection evidence.

---

# Phase 13 — Restart

Execute authority 1 restart sequence exactly.

Session A:
- P1;
- P2;
- graceful CTRL_BREAK;
- allowed post-session handoff state.

Session B:
- same isolated runtime path;
- startup cleanup semantics;
- fresh P1/P2 if required by contract;
- valid new envelope;
- revision/session semantics preserved.

No seam state carried between processes.

---

# Phase 14 — Streamlit shutdown

Use exact authority 1 method/order.

- least-forceful allowed termination;
- bounded wait;
- forced fallback only if permitted;
- listener released;
- owned children gone.

Streamlit never controls Worker cancellation.

---

# Phase 15 — Final shutdown order

Execute exact fixed order from authority 1.

No discretionary changes.

Capture bounded evidence.

---

# Phase 16 — Process/listener residue

Require:
- zero harness-owned Worker processes;
- zero harness-owned Streamlit processes;
- zero owned child processes;
- zero owned listeners/ports.

At focused-test end and full-demo end.

No global process-name cleanup.

---

# Phase 17 — Handoff residue

Assert exact authority 1 final state for:
- canonical handoff;
- temp siblings.

Preserve Worker ownership/startup cleanup.

Streamlit never deletes handoff.

---

# Phase 18 — DB residue

Use isolated harness-owned DB.

Verify exact final state for:
- DB;
- WAL;
- SHM;
- journal/other governed sidecars.

No persistence/schema change.

---

# Phase 19 — Evidence/log rule

Follow authority 1 exactly.

Do not invent persistent evidence artifacts.

---

# Phase 20 — Full finite demonstration

Run complete WP08 demonstration within exact total max duration.

Record:
- start/end/duration;
- P1;
- P2 + refresh elapsed;
- Streamlit readiness;
- real chain observation;
- cancellation readiness;
- CTRL_BREAK;
- exit 0;
- restart;
- shutdown;
- residue.

Exceeding max duration = failure.

---

# Phase 21 — Python regression

Run:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

No process/listener residue afterward.

---

# Phase 22 — .NET governed regression

Run:
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Reference predecessor: 309/309.

Explain exact authorized WP08 test delta.

Require:
- 0 failed;
- 0 skipped unless already governed;
- build 0 warnings / 0 errors.

---

# Phase 23 — Static scope audit

Prove zero unauthorized:
- harness release IPC;
- new config/env seam activation;
- production delays;
- infinite loop;
- Replay change;
- extra observations/ticks;
- publication semantic change;
- Windows helper redesign;
- Job Object;
- broad taskkill;
- HTTP/socket/named pipe/control file;
- Worker listener;
- Worker↔Streamlit supervision;
- schema/persistence/provider change;
- package addition;
- WP09.

List every changed path and binding authority.

---

# Phase 24 — Preservation audit

For all partial WP08 paths:
- identify accepted predecessor content;
- identify exact new reconciliation hunk;
- prove no valid Windows/cancellation behavior removed.

For unrelated local state:
- untouched.

---

# Phase 25 — #233 acceptance matrix

Before GitHub mutation produce:

`#233 requirement → authority → implementation/test → demo evidence → residue evidence`

Every row PASS.

Also require:
- P2 within 8 sec;
- graceful CTRL_BREAK PASS;
- no forced kill;
- finite demo PASS;
- Python PASS;
- .NET PASS;
- scope PASS;
- preservation PASS.

Otherwise keep #233 Open / Backlog.

---

# Phase 26 — GitHub Project item identification

Only after technical acceptance.

Identify exactly one Project #2 item for #233 with exhaustive/typed lookup.

Resolve:
- item node ID;
- Status field ID;
- Done option ID;
- Release;
- Priority;
- Area.

If ambiguous:
- GitHub mutations zero;
- BLOCK.

No item creation/deletion.

---

# Phase 27 — GitHub lifecycle completion

After identity proof:

1. set exact #233 Project Status → Done;
2. read back Done + unchanged metadata;
3. close #233;
4. read back Closed;
5. verify #234 Open / Backlog;
6. verify milestone state/counts.

Do not mutate #234.
Do not start WP09.

---

# Phase 28 — Final read-back/residue

Verify:
- #233 Closed / Done;
- #234 Open / Backlog;
- no Worker;
- no Streamlit;
- no listener;
- correct handoff final state;
- correct DB final state.

No code mutation after lifecycle completion.

---

# Completion gate

WP08 completes only if:

1. authority-4 seam reconciliation implemented exactly;
2. P1 pass-through proven;
3. genuine P2 within unchanged 8-second bound;
4. post-P2 Worker liveness proven;
5. targeted CTRL_BREAK graceful exit 0;
6. no forced kill in success path;
7. Windows helper preserved;
8. Replay unchanged;
9. Streamlit/chain evidence passes;
10. restart passes;
11. process/listener/handoff/DB residue passes;
12. Python predecessors pass;
13. .NET/build pass;
14. scope/preservation pass;
15. #233 Project Done;
16. #233 Closed;
17. #234 Open / Backlog;
18. WP09 unstarted.

---

# Required completion report

## Four binding authorities
Name all four.

## Entry state
Repository/GitHub/baselines.

## Preserved partial WP08
Status of five existing paths.

## Reconciliation implementation
Exact P1/P2/hold state and changed symbols.

## Bounded refresh
Exact newer rule and elapsed time.

## Graceful cancellation
P2 → hold → targeted CTRL_BREAK → CTS → exit 0.

## Windows helper
Preservation/read-back.

## Streamlit/chain
Readiness and real projection evidence.

## Restart/shutdown
Exact evidence.

## Residue
Processes/listeners/handoff/temp/DB/logs.

## Tests
Focused WP08 count.

## Python
WP05/WP06/WP07/Streamlit/pip.

## .NET
Application/Infrastructure/Domain/Architecture/build/full.

## Scope/preservation
Forbidden categories zero.

## #233 mapping
Requirement → proof.

## GitHub
#233 Closed / Done; #234 unchanged.

## Milestone
State and raw/canonical counts.

## Next eligible work
Exact next canonical WP.

---

# Mutation statement

If lifecycle completion succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Report repository mutations exactly.

---

# Stop conditions

Stop if:
- P2 cannot arise naturally;
- 8-second bound cannot be met;
- seam requires harness release IPC;
- Replay semantics must change;
- Windows helper must be redesigned;
- forced kill is required for graceful acceptance;
- predecessor regression fails;
- residue unsafe;
- unauthorized path required;
- WP09 boundary crossed;
- Project item identity ambiguous.

Preserve valid partial WP08 work and leave #233 Open / Backlog.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless #233 is authoritatively Closed / Done and every bounded-refresh, Windows graceful-cancellation, finite-demonstration, restart, residue, regression, preservation, and scope gate passes.

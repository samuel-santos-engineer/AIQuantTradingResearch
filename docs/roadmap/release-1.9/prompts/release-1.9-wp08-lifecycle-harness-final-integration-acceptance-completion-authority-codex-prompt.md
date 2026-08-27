# Release 1.9 — WP08 Lifecycle-Harness Final Integration + Acceptance Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is the **narrow final integration and acceptance-completion authority** for Release 1.9 WP08, canonical issue **#233**.

All production lifecycle/cancellation semantics, Windows process-group signal semantics, bounded-refresh reconciliation, and Python presentation-chain invocation semantics are already fixed and partially implemented.

This authority may modify only the existing WP08 lifecycle demonstration test surface required to finish acceptance, plus perform the final GitHub lifecycle transition if every technical gate passes.

WP09 / #234 must remain Open / Backlog and unstarted.

---

# Binding WP08 authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WORKER_LIFECYCLE_LIVENESS_TEST_SEAM_CANCELLATION_ADAPTER_CONTRACT_PATH_AUTHORITY_DEFINITION.md`
3. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_WINDOWS_ISOLATED_PROCESS_GROUP_CONSOLE_SIGNAL_TEST_HELPER_AUTHORITY.md`
4. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIVENESS_SEAM_BOUNDED_REFRESH_RECONCILIATION_CONTRACT_AMENDMENT.md`
5. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_GOVERNED_PYTHON_PRESENTATION_CHAIN_INVOCATION_SEAM_PATH_AUTHORITY_AMENDMENT.md`

Also preserve:
- accepted Release 1.9 manifest;
- accepted WP05/WP06/WP07 contracts and implementations;
- all previously valid WP08 partial implementation;
- accepted local-repository reconciliation/preservation rules.

Do not redefine any semantics.

---

# Accepted partial implementation

Treat as valid predecessor work:

## Worker production
- `src/AIQuantTradingResearch.Worker/WorkerLifecycleCancellation.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs`

Accepted behavior:
- `Console.CancelKeyPress → Worker CTS → existing execution token`;
- intentional cancellation exits 0;
- no pipeline-failure translation;
- `--wp08-test-liveness`;
- P1 pass-through;
- genuine P2 from normal Replay;
- P2 within fixed 8-second bound;
- post-P2 cancellation hold.

## Windows test helper
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WindowsIsolatedProcessGroup.cs`

Accepted behavior:
- `CreateProcessW`;
- `CREATE_NEW_PROCESS_GROUP`;
- targeted `CTRL_BREAK_EVENT`;
- safe command-line quoting;
- safe-handle cleanup;
- no broad taskkill;
- no Job Object.

## Existing WP08 lifecycle test
- `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Accepted focused evidence:
- **4/4 passed**.

## Governed Python probe
- `python/presentation/wp08_presentation_chain_probe.py`

Accepted behavior:
- only `--handoff <absolute-path>`;
- delegates to existing WP05 parser;
- delegates to existing WP06 frame projection;
- delegates to existing WP07 presentation projection;
- one JSON document on stdout;
- stderr diagnostics only;
- invalid path verified with exit code 2;
- no generic bridge;
- no production transport.

Do not discard or redesign any of this.

---

# Accepted regression baseline

Treat as the accepted predecessor baseline entering this authority:

## .NET
- Application: **125**
- Infrastructure: **164**
- Domain: **11**
- Architecture: **13**
- full: **313/313**
- build: 0 warnings / 0 errors

## Python
- WP05: **3/3**
- WP06: **6/6**
- WP07 semantic: **2/2**
- WP07 presentation: **2/2**
- Streamlit: **1.61.1**
- `pip check`: clean

Any new .NET count delta must come only from authorized additions inside the existing WP08 lifecycle test path.

---

# Lifecycle state

Expected:
- #232 Closed / Done
- #233 Open / Backlog
- #234 Open / Backlog
- milestone #58 Open
- no GitHub mutation from prior blocked passes
- WP09 unstarted

Verify read-only.

---

# Mutation scope

Authorized repository mutation is limited to:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Use the existing `wp08_presentation_chain_probe.py` and `WindowsIsolatedProcessGroup.cs` as read-only dependencies unless a concrete defect is proven.

If either dependency requires modification:
- STOP;
- report exact defect and missing authority.

No production mutation.
No new helper path.
No new Python path.
No WP09 path.

---

# Objective

Complete the missing WP08 acceptance proof inside the existing lifecycle harness:

1. independently launch and verify Streamlit readiness;
2. prove listener ownership belongs to Streamlit and not Worker;
3. invoke the governed Python probe against the real Worker P2 handoff;
4. prove real WP05 → WP06 → WP07 chain output and same-publication correlation;
5. execute Worker A → Worker B restart;
6. prove final process/listener/handoff/database residue matrix;
7. prove finite total demonstration duration;
8. rerun all predecessor regressions;
9. close #233 / set Done only if every gate passes.

---

# Phase 0 — Exact contract extraction

Before mutation, extract exact values from the binding authorities:

- total demonstration maximum;
- Streamlit startup timeout;
- Streamlit readiness condition;
- loopback host rule;
- free-port allocation rule;
- listener ownership proof rule;
- Worker/Streamlit launch ordering;
- P1/P2 timing rule;
- real-chain observation ordering;
- 2-second Python probe timeout;
- Worker cancellation timeout;
- restart timeout;
- Worker B required behavior;
- Streamlit shutdown timeout/method;
- final shutdown ordering;
- canonical handoff final-state rule;
- temp handoff pattern;
- DB/WAL/SHM/journal final-state rules;
- evidence/log rule.

If any value remains undefined, STOP.

---

# Phase 1 — Read-only entry verification

Verify:

- branch/HEAD/origin/ahead-behind;
- staged/unstaged/untracked state;
- current content/diff of the WP08 lifecycle test;
- existing Python probe path;
- existing Windows helper path;
- accepted Worker partial files;
- #233 state/Project item;
- #234 state/Project item;
- milestone #58;
- no WP09 implementation.

Do not require a clean worktree.

Preserve unrelated local work.

---

# Phase 2 — Pre-mutation baseline

Run:

- focused WP08 **4/4**;
- build;
- full .NET **313/313**;
- WP05 Python 3/3;
- WP06 Python 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- Streamlit 1.61.1;
- `pip check`.

Also verify no harness-owned process/listener/temp runtime residue exists before tests.

If baseline fails, STOP.

---

# Phase 3 — Extend only the WP08 lifecycle test

Implement all missing orchestration/assertions inside:

`WP08LifecycleDemonstrationTests.cs`

No extraction into a new helper file unless already explicitly authorized by prior accepted authority.

Reuse existing local helper methods inside the same test file if needed.

---

# Phase 4 — Harness-owned resource model

Create deterministic ownership for:

- isolated runtime directory;
- canonical handoff path;
- isolated temporary DB;
- Worker A;
- Worker B;
- Streamlit process;
- Streamlit loopback port;
- Python probe process.

Track all exact PIDs/process handles/paths.

Never touch developer/default runtime or database.

---

# Phase 5 — Streamlit independent launch

Launch the real existing Streamlit entry point as an independent peer.

Requirements:
- Worker does not launch Streamlit;
- Streamlit does not launch Worker;
- loopback-only host according to binding authority;
- harness-owned free port;
- existing Python executable/environment conventions;
- no browser automation unless explicitly required.

No production changes.

---

# Phase 6 — Streamlit readiness

Prove the exact accepted readiness condition.

Must include:
- process alive;
- expected listener active;
- readiness within fixed timeout;
- no sleep-only success criterion.

If readiness fails:
- cleanup only owned resources;
- test fails.

---

# Phase 7 — Listener ownership

Prove:

- exact Streamlit port;
- listener owner belongs to harness-owned Streamlit process/tree;
- Worker PID is not listener owner;
- Worker exposes no new listener.

Use repository/OS tooling already authorized/available from the test environment.

Do not globally enumerate/kill unrelated listeners.

---

# Phase 8 — Worker A launch

Launch Worker A using the accepted Windows isolated-process-group helper.

Use:
`--wp08-test-liveness`

and isolated harness configuration.

Preserve:
- P1 valid;
- P1 pass-through;
- genuine P2;
- P2 within 8 seconds;
- post-P2 hold.

No modification to seam/helper.

---

# Phase 9 — Select real P2 handoff

Identify the qualifying P2 canonical handoff using the exact accepted newer-publication rule.

Record the exact existing correlation fields needed by the probe/harness:
- revision;
- snapshot identity/version;
- Replay tick if applicable.

Do not invent correlation data.

---

# Phase 10 — Invoke governed Python probe

Launch the existing:

`python/presentation/wp08_presentation_chain_probe.py`

with:
`--handoff <absolute-P2-handoff-path>`

Requirements:
- exact Python interpreter convention;
- exact working directory;
- stdout/stderr capture;
- **2-second timeout**;
- process disposed;
- no shell=True;
- no generic bridge.

Require exit 0.

---

# Phase 11 — Parse probe result

Parse the single deterministic JSON document.

Assert exact schema fixed by the probe authority.

No tolerant fallback parsing.

No prose expected on stdout.

If invalid JSON/output shape:
- fail.

---

# Phase 12 — WP05 real-chain proof

Assert probe evidence proves actual existing WP05 parser executed against the real P2 handoff.

Validate only governed fields.

No duplicated parser semantics in .NET.

---

# Phase 13 — WP06 real-chain proof

Assert actual WP06 frame projection evidence from the same P2 publication.

Validate only governed frame facts.

No duplicate projection implementation in .NET.

---

# Phase 14 — WP07 real-chain proof

Assert actual WP07 presentation projection evidence:
- exact five sections/order;
- required factual statuses;
- same underlying publication;
- transport-warning separation.

No duplicate formatter.

---

# Phase 15 — End-to-end identity correlation

Prove:
`Worker P2 == WP05 parsed source == WP06 frame source == WP07 presentation source`

using only accepted existing identifiers.

If any identity mismatch or ambiguity exists:
- FAIL.

---

# Phase 16 — Streamlit coexistence during chain proof

While Worker A and Streamlit are alive:

- Streamlit listener remains ready;
- canonical handoff remains available/readable;
- Streamlit does not delete handoff;
- Worker owns no listener;
- no mutual process control exists.

---

# Phase 17 — Worker A graceful cancellation

After P2 + probe + chain evidence:

- call accepted targeted `CTRL_BREAK_EVENT`;
- require signal API success;
- Worker exits within timeout;
- exit code 0;
- no forced kill;
- no pipeline-failure translation;
- Worker A process residue zero.

Do not modify helper.

---

# Phase 18 — Session A residue snapshot

Before restart cleanup, capture:

- canonical handoff existence/content identity;
- temp handoff siblings;
- temp DB + sidecars;
- Streamlit process/listener still alive if contract requires.

Apply exact accepted intermediate-state rules.

---

# Phase 19 — Worker B restart

Launch Worker B using the same isolated runtime/database ownership boundary.

Prove:
- restart within timeout;
- prior-session handoff cleanup behavior exactly as defined;
- seam state reset;
- valid new publication;
- accepted session/revision behavior;
- no cross-session ordering invented.

No Replay change.

---

# Phase 20 — Restart-specific assertions

Assert exact authority requirements for:
- prior canonical file cleanup;
- stale temp artifact cleanup;
- new-session publication;
- Historical/revision reset behavior if applicable;
- Streamlit independence throughout restart if required.

Do not add new restart semantics.

---

# Phase 21 — Worker B completion

End Worker B using the exact accepted WP08 rule.

If graceful cancellation required:
- targeted CTRL_BREAK.

If natural completion accepted:
- follow exact rule.

No broad kill.

---

# Phase 22 — Streamlit shutdown

Perform shutdown in the exact accepted order.

Requirements:
- least-forceful allowed method first;
- wait fixed timeout;
- forced fallback only if binding contract permits;
- listener released;
- owned child processes gone.

If fallback is disallowed and needed:
- FAIL.

---

# Phase 23 — Final process residue matrix

Prove final state:

- Worker A absent;
- Worker B absent;
- Streamlit absent;
- Python probe absent;
- owned child processes absent;
- unrelated processes untouched.

Use owned PIDs/handles.

---

# Phase 24 — Final listener residue matrix

Prove:

- harness Streamlit port no longer listening;
- no Worker listener exists;
- no other harness-owned listener remains;
- unrelated listeners untouched.

---

# Phase 25 — Final handoff residue matrix

Apply exact contract to:

- canonical handoff;
- temp siblings matching exact pattern;
- any intermediate artifact expected to be removed by Worker B startup.

Streamlit never deletes handoff.

---

# Phase 26 — Final DB residue matrix

Apply exact contract to:

- main DB;
- `-wal`;
- `-shm`;
- journal;
- any governed sidecars.

Only harness-owned DB artifacts may be cleaned.

No schema/persistence changes.

---

# Phase 27 — Evidence/log residue

Follow exact accepted rule.

If evidence is stdout/test output only:
- persistent evidence artifacts = zero.

No new evidence dir.

---

# Phase 28 — Finite total duration

Measure the full demonstration using the exact accepted start/end boundaries.

Report:
- actual elapsed;
- configured maximum.

The Python probe 2-second timeout counts toward the full budget.

If total exceeds maximum:
- FAIL.

---

# Phase 29 — Focused WP08 suite

Run the full WP08 lifecycle test suite.

Requirements:
- previous 4/4 behaviors remain green;
- report final exact test count;
- report only authorized additions in existing test path;
- report duration;
- zero failures.

Explain any count delta exactly.

---

# Phase 30 — Python regression

Run:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

No extra Python probe test file is authorized.

No residue afterward.

---

# Phase 31 — .NET governed regression

Run:
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution.

Accepted baseline:
**313/313**.

If new tests were added inside the existing WP08 lifecycle file, calculate the exact authorized delta.

Require:
- 0 failed;
- build 0 warnings / 0 errors.

---

# Phase 32 — Static scope audit

Prove zero mutation to:

- Worker cancellation adapter;
- Worker P1/P2 seam;
- Windows process helper;
- Python probe semantics beyond already-created accepted implementation;
- Replay;
- WP05 parser;
- WP06 frame;
- WP07 presentation;
- Release 1.8 endpoint;
- schema/persistence/provider;
- package pins;
- production process control;
- custom IPC;
- WP09.

Expected repository mutation under this authority:
- only `WP08LifecycleDemonstrationTests.cs`.

Any other path requires STOP unless it was pre-existing accepted work untouched by this authority.

---

# Phase 33 — Preservation audit

Prove:
- focused predecessor 4/4 behavior preserved;
- P2 still within 8 seconds;
- graceful CTRL_BREAK still exits 0;
- Windows helper unchanged;
- Python probe unchanged from accepted partial work;
- Worker files unchanged;
- unrelated local work untouched.

---

# Phase 34 — #233 acceptance matrix

Before GitHub mutation create:

`#233 requirement → binding authority → implementation/test evidence → residue evidence`

Must include:
- bounded refresh;
- graceful cancellation;
- restart;
- independent Streamlit;
- listener ownership;
- process ownership;
- real WP05 observation;
- real WP06 observation;
- real WP07 observation;
- same-publication identity;
- temp handoff residue;
- DB residue;
- finite duration.

Every row PASS.

Otherwise:
- #233 remains Open / Backlog;
- GitHub mutations zero.

---

# Phase 35 — GitHub Project item identification

Only after all technical gates pass.

Identify exactly one existing Project #2 item for #233 using robust typed API/GraphQL lookup and exhaustive pagination.

Resolve:
- item node ID;
- Status field ID;
- Done option ID;
- Release;
- Priority;
- Area/category.

If ambiguous:
- BLOCK;
- GitHub mutations zero.

No item creation/deletion.

---

# Phase 36 — GitHub lifecycle completion

After identity proof:

1. set exact #233 Project item Status → Done;
2. read back Done;
3. verify Release/Priority/Area unchanged;
4. close #233;
5. read back Closed;
6. verify #234 Open / Backlog;
7. verify milestone #58 state/counts.

Do not mutate #234.
Do not start WP09.

---

# Phase 37 — Final read-back/residue

After GitHub lifecycle mutation, no repository changes.

Verify:
- #233 Closed / Done;
- #234 Open / Backlog;
- Worker A absent;
- Worker B absent;
- Streamlit absent;
- Python probe absent;
- listener absent;
- handoff final state correct;
- DB final state correct.

---

# Completion gate

WP08 completes only if:

1. Streamlit independent readiness proven;
2. listener ownership proven;
3. Worker owns no listener;
4. real WP05 probe evidence proven;
5. real WP06 probe evidence proven;
6. real WP07 probe evidence proven;
7. same P2 publication correlated end-to-end;
8. P1/P2/8-second proof remains green;
9. graceful CTRL_BREAK exit 0 remains green;
10. restart passes;
11. process residue passes;
12. listener residue passes;
13. handoff residue passes;
14. DB residue passes;
15. finite total duration passes;
16. Python regressions pass;
17. .NET/build pass from 313 baseline plus only authorized delta;
18. scope/preservation pass;
19. #233 Project Done;
20. #233 Closed;
21. #234 Open / Backlog;
22. WP09 unstarted.

---

# Required completion report

## Binding authorities
List all five WP08 artifacts.

## Accepted predecessor
4/4 focused WP08, 313/313 .NET, Python baselines.

## Changed paths
Exact list.

## Streamlit
Launch/readiness/listener ownership.

## Real chain
P2 handoff → probe → WP05 → WP06 → WP07 identity proof.

## Bounded refresh/cancellation
Preserved results.

## Restart
Worker A → Worker B proof.

## Shutdown
Worker/Streamlit results.

## Residue
Processes/listeners/handoff/temp/DB/evidence.

## Duration
Actual vs maximum.

## Focused WP08
Exact final count.

## Python regression
Exact results.

## .NET regression
Exact suite totals/build.

## Scope/preservation
Forbidden mutations zero.

## #233 mapping
Requirement → proof.

## GitHub
#233 Closed / Done; #234 unchanged.

## Milestone
State and raw/canonical counts.

## Next eligible work package
Exact canonical next WP.

---

# Mutation statement

If lifecycle completion succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Report repository mutations exactly.

---

# Stop conditions

Stop if:
- Streamlit readiness/listener ownership cannot be proven using existing authorized mechanisms;
- the accepted Python probe cannot prove same-publication identity;
- restart requires production changes;
- Worker/helper/probe must be modified;
- residue semantics cannot be satisfied;
- finite duration cannot be met;
- regression fails;
- package/new transport/helper path is required;
- WP09 boundary would be crossed;
- Project item identity ambiguous.

Preserve all valid partial WP08 work and leave #233 Open / Backlog.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 LIFECYCLE-HARNESS FINAL INTEGRATION AND ACCEPTANCE COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 LIFECYCLE-HARNESS FINAL INTEGRATION AND ACCEPTANCE COMPLETION BLOCKED`

Do not emit COMPLETE unless all remaining finite-demonstration gates, all regressions, and #233 Closed / Done are authoritatively proven.

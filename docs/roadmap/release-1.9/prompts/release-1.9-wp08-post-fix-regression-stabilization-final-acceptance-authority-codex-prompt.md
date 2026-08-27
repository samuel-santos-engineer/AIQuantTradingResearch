# Release 1.9 — WP08 Post-Fix Regression Stabilization + Final Acceptance Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is a **narrow verification/final-acceptance authority** for Release 1.9 WP08, canonical issue **#233**.

The reused-runtime defect has already been diagnosed and fixed in the lifecycle harness:

- classification: `RR-HANDOFF`;
- canonical handoff cleanup remains Worker-startup-owned;
- prior failure occurred because the harness accepted stale Worker A handoff content as Worker B readiness;
- fix: Worker B readiness now requires a **changed handoff payload** before acceptance;
- canonical shared-runtime restart test passed after this change;
- build passed with 0 warnings / 0 errors.

This authority does **not** reopen that fix.

Its purpose is to stabilize the regression environment, complete every remaining acceptance gate, and perform the final #233 lifecycle transition only if all evidence is green.

WP09 / #234 must remain Open / Backlog and unstarted.

---

# Accepted predecessor state

Treat as binding:

## Reused-runtime fix
Changed only:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

Accepted behavior:
- stale Worker A handoff no longer satisfies Worker B readiness;
- Worker B readiness requires changed canonical handoff payload;
- shared runtime/handoff/database restart remains canonical;
- no fresh-runtime escape hatch;
- canonical shared-runtime restart test passed.

## Diagnostics preserved
- standalone CTRL_BREAK exit 0;
- A/B/C/D topology matrix 4/4;
- restart matrix R0–R4 + R1F;
- R-RUNTIME and RR-HANDOFF evidence;
- Smart App Control/local signing remediation documented and working.

## Latest validation
- build: 0 warnings / 0 errors;
- Domain: 11/11;
- Application: 125/125;
- Architecture: 13/13;
- Infrastructure did not produce a terminal summary because an owned/stale `testhost` held a build-output lock;
- stale owned `testhost` was terminated;
- subsequent `--no-build` Infrastructure run still did not emit terminal summary;
- full focused WP08, 3 restart repetitions, and Python regressions not yet fully proven after final fix.

Lifecycle:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- no GitHub lifecycle mutation.

---

# Authority posture

This is verification-first.

No new implementation change is expected.

Repository mutation is **not authorized by default**.

If a regression exposes a genuine defect:
- STOP;
- report exact failing gate and path;
- do not patch under this authority unless the defect is a purely test-environment cleanup issue already covered below.

---

# Allowed environment stabilization

This authority explicitly permits cleaning **owned/stale test-runner processes only** when they are proven to block the governed test run.

Allowed:
- identify exact `testhost` PID(s) owned by the current repository/test invocation or left stale by prior runs;
- terminate those owned stale processes;
- verify associated file locks are released;
- clean only build/test artifacts already owned by this repository if required by accepted test workflow.

Forbidden:
- global `taskkill /IM testhost.exe /F`;
- killing unrelated IDE/test processes;
- broad process-name cleanup;
- changing Windows security policy;
- disabling Smart App Control;
- changing signing setup.

Every terminated process must be justified as harness/test-owned.

---

# Objective

Complete all remaining acceptance evidence:

1. clean restart test repeatability;
2. full focused WP08 suite;
3. Infrastructure terminal summary;
4. full .NET regression;
5. Python predecessor regressions;
6. Streamlit/pip checks;
7. final residue matrix;
8. scope/preservation audit;
9. #233 Project Done + issue Closed;
10. #234 remains Open / Backlog.

---

# Phase 0 — Entry verification

Read-only verify:
- branch/HEAD/origin/ahead-behind;
- staged/unstaged/untracked state;
- exact current diff of `WP08LifecycleDemonstrationTests.cs`;
- no unexpected changes to helper/Worker/Python/signing;
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- local signing remains development-only/opt-in.

Do not require clean worktree.

---

# Phase 1 — Owned testhost audit

Before running acceptance:

1. enumerate active `testhost` processes;
2. identify which are owned by:
   - current repository path;
   - current dotnet/vstest invocation lineage;
   - prior stale WP08 test runs;
3. leave unrelated processes untouched.

If a stale owned process exists:
- terminate only that PID;
- verify exit;
- verify build-output lock released.

Record:
- PID;
- parent if available;
- command line/path evidence if available;
- reason for ownership classification.

If ownership cannot be proven, do not terminate.

---

# Phase 2 — Build gate

Run build.

Require:
- 0 warnings;
- 0 errors.

If build output lock recurs:
- perform owned-process audit again;
- do not modify repository logic.

---

# Phase 3 — Canonical restart repetition

Run the canonical shared-runtime restart-specific acceptance test **3 consecutive times**.

Each repetition must prove:
- Worker A reaches governed state;
- Worker A CTRL_BREAK exits 0;
- Worker A disposed;
- shared runtime/handoff/database preserved;
- Worker B readiness waits for changed handoff payload;
- Worker B reaches governed state;
- Worker B CTRL_BREAK exits 0;
- no forced kill;
- no process/listener residue.

Require 3/3.

If one fails:
- capture exact evidence;
- STOP unless failure is solely stale owned testhost/file-lock environment issue.

---

# Phase 4 — Focused WP08 suite

Run the complete focused WP08 lifecycle test surface.

Include:
- standalone CTRL_BREAK;
- A/B/C/D;
- R0–R4;
- R1F;
- one-factor reuse diagnostics if still present;
- canonical restart acceptance.

Require:
- all pass;
- exact count reported;
- zero unexplained skips.

Diagnostic tests may remain if governed; do not delete them under this authority.

---

# Phase 5 — Infrastructure stabilization

Run Infrastructure suite with a command that must produce a terminal summary.

Preferred order:

1. standard governed invocation;
2. if file-lock/testhost issue recurs:
   - identify/terminate only owned stale testhost;
   - rerun once;
3. if standard build step is already proven and only test execution is needed:
   - `--no-build` may be used, but terminal summary is still mandatory.

Require:
- exact passed/failed/skipped;
- 0 failed;
- terminal summary present.

If test process hangs:
- capture owned PID/process state;
- terminate only owned process;
- STOP with evidence if a second clean attempt still hangs.

Do not loop indefinitely.

---

# Phase 6 — Other .NET suites

Run:
- Application;
- Domain;
- Architecture.

Expected reference:
- Application 125;
- Domain 11;
- Architecture 13.

Report exact current counts.

---

# Phase 7 — Full .NET regression

Run the full governed solution regression.

Require:
- terminal summary;
- 0 failed;
- 0 unexpected skipped;
- exact final total.

Use current test count including authorized WP08 diagnostic additions.

Do not compare blindly to 313; explain exact delta from WP08 test additions.

---

# Phase 8 — Python regression

Run:
- WP05: 3/3;
- WP06: 6/6;
- WP07 semantic: 2/2;
- WP07 presentation: 2/2;
- Python compile/import checks;
- Streamlit version = 1.61.1;
- `pip check`.

Require all pass/clean.

No Python mutation.

---

# Phase 9 — Streamlit/process residue

After all tests:
- no harness-owned Worker;
- no harness-owned Streamlit;
- no probe;
- no owned testhost residue;
- no owned listener residue;
- no helper drain task/process residue.

Do not kill unrelated processes.

---

# Phase 10 — Handoff/database residue

Verify the final accepted WP08 residue contract:

- canonical handoff final state correct;
- zero forbidden temp handoff siblings;
- DB final state correct;
- WAL/SHM/journal/sidecars correct;
- runtime directory contains only allowed artifacts.

No cleanup outside harness-owned paths.

---

# Phase 11 — Signing/App Control sanity

Read-only verify that the environment remediation remains valid enough for test execution:

- first-party test assembly has valid Authenticode signature after build/sign workflow;
- no new Event 3077 block affects the governed run.

Do not change signing config under this authority.

This is environment evidence only, not a WP08 acceptance requirement if tests already executed successfully.

---

# Phase 12 — Static scope/preservation audit

Prove no new mutation under this authority to:

- `WindowsIsolatedProcessGroup.cs`;
- Worker production;
- Python probe;
- Replay;
- WP05/WP06/WP07;
- signing scripts/project settings;
- docs;
- packages;
- WP09.

Expected repository mutation under this authority:
- **zero**.

The previously accepted `WP08LifecycleDemonstrationTests.cs` fix remains unchanged.

---

# Phase 13 — #233 acceptance matrix

Before GitHub mutation produce:

`#233 requirement → implementation/evidence → final test result → residue result`

Include:
- bounded refresh;
- graceful cancellation;
- restart;
- changed-payload Worker B readiness;
- independent Streamlit;
- listener ownership;
- real WP05→WP06→WP07 chain;
- process ownership;
- handoff residue;
- DB residue;
- finite demonstration;
- regression stability.

Every row PASS.

Otherwise:
- #233 remains Open / Backlog;
- GitHub mutations zero.

---

# Phase 14 — GitHub Project item identification

Only after all technical acceptance.

Identify exactly one Project #2 item for #233 using robust typed/exhaustive lookup.

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

# Phase 15 — GitHub lifecycle completion

After identity proof:

1. set #233 Project Status → Done;
2. read back Done;
3. verify Release/Priority/Area unchanged;
4. close #233;
5. read back Closed;
6. verify #234 Open / Backlog;
7. verify milestone #58 state/counts.

Do not mutate #234.
Do not start WP09.

---

# Phase 16 — Final read-back

After GitHub mutation:
- no repository changes;
- #233 Closed / Done;
- #234 Open / Backlog;
- milestone #58 correct;
- no owned Worker/Streamlit/probe/testhost;
- listener residue zero;
- handoff/database residue correct.

---

# Completion gate

WP08 completes only if:

1. canonical restart test passes 3/3;
2. full focused WP08 passes;
3. Infrastructure produces terminal PASS summary;
4. Application/Domain/Architecture pass;
5. full .NET produces terminal PASS summary;
6. build 0 warnings / 0 errors;
7. Python WP05/WP06/WP07 pass;
8. Streamlit/pip checks pass;
9. no owned process/listener residue;
10. handoff/DB residue pass;
11. scope/preservation pass;
12. #233 Project Done;
13. #233 Closed;
14. #234 Open / Backlog;
15. WP09 unstarted.

---

# Required completion report

## Accepted fix
RR-HANDOFF and changed-payload readiness.

## Environment stabilization
Owned testhost findings/actions.

## Restart repeatability
3/3 results.

## Focused WP08
Exact count/result.

## .NET
Infrastructure/Application/Domain/Architecture/full/build exact results.

## Python
WP05/WP06/WP07/Streamlit/pip.

## Residue
Processes/listeners/handoff/DB.

## Signing/App Control sanity
Read-only result.

## Scope/preservation
Zero new repository mutation.

## #233 acceptance matrix
Requirement → proof.

## GitHub
#233 Closed / Done; #234 unchanged.

## Milestone
State and raw/canonical counts.

## Next eligible work
Exact next canonical WP.

---

# Mutation statements

If repository untouched:

`WP08 POST-FIX REGRESSION STABILIZATION REPOSITORY MUTATIONS: ZERO`

If lifecycle completion succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

---

# Stop conditions

Stop if:
- canonical restart fails after the accepted changed-payload fix;
- owned testhost cannot be identified safely;
- Infrastructure still cannot produce a terminal result after one clean retry;
- any .NET/Python regression fails;
- residue unsafe;
- Project item identity ambiguous;
- WP09 boundary crossed.

Do not patch implementation under this authority.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 POST-FIX REGRESSION STABILIZATION AND FINAL ACCEPTANCE COMPLETE`

Blocked:

`RELEASE 1.9 WP08 POST-FIX REGRESSION STABILIZATION AND FINAL ACCEPTANCE BLOCKED`

Do not emit COMPLETE unless every technical gate passes and #233 is authoritatively Closed / Done.

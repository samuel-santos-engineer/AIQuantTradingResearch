# Release 1.9 — Post-Fix Predecessor Revalidation Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is a **narrow revalidation-only authority** for Release 1.9 after a late Windows atomic-replacement race fix was applied following WP11 technical acceptance and before WP12 PR-readiness completion.

Accepted candidate fix paths:

1. `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`

Candidate behavior:

- publisher retries transient `UnauthorizedAccessException` / `IOException` replacement failures for up to 200 ms;
- publisher test reader opens with `FileShare.ReadWrite | FileShare.Delete` so an old read handle can coexist with atomic replacement.

Previously observed validation:

- build 0 warnings / 0 errors;
- failing atomic test passed 3 consecutive runs;
- publisher suite 4/4;
- Infrastructure 182/182;
- owned processes/listeners 0.

This authority must determine whether the late fix is fully compatible with all frozen predecessor contracts and restore a trustworthy predecessor baseline for WP12.

No further implementation mutation is authorized by default.
No Git mutation.
No GitHub mutation.
No WP12 lifecycle mutation.

---

# 1. Predecessor context

Before the late fix, accepted WP11 boundary was:

- build 0 warnings / 0 errors;
- Domain 11/11;
- Application 125/125;
- Infrastructure 182/182;
- Architecture 21/21;
- .NET aggregate 339/339;
- Python 17/17;
- Streamlit 1.61.1;
- `pip check` clean;
- WP08 lifecycle 18/18;
- WP09 permanent integration and architecture accepted;
- schema v4 preserved;
- WP11 validation-only complete;
- #233–#236 Closed / Done;
- #237 Open / Backlog.

The late race fix invalidates the assumption that the predecessor repository surface remained unchanged after WP11 acceptance.

Therefore this authority must revalidate all materially affected predecessor gates.

---

# 2. Entry-state snapshot

Read-only verify:

## Git
- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked paths.

Do not reset/stash/clean/revert.

Identify exact diffs in the two candidate-fix files.

## GitHub
Read:
- #233–#236;
- #237;
- milestone #58.

Require no lifecycle mutation from this authority.

---

# 3. Fix-scope audit

Inspect exact changes in:

`VisualizationReadModelFilePublisher.cs`

`VisualizationReadModelFilePublisherTests.cs`

Confirm the fix is limited to:

## Production
- bounded retry around transient Windows atomic replacement failure;
- retry timeout maximum 200 ms;
- no change to JSON schema;
- no change to file naming;
- no change to canonical handoff path;
- no change to atomic replacement semantics;
- no silent swallowing of persistent failures;
- no infinite retry;
- no new package/configuration.

## Test
- read sharing changed only to permit old-handle coexistence with replacement;
- semantic assertions unchanged;
- no weakening of atomicity/finality assertions.

If the diff contains broader changes:
BLOCK.

---

# 4. Contract-compatibility audit

Read the accepted WP05/WP08/WP09 contracts governing publication/handoff semantics.

Confirm the fix preserves:

- file publisher ownership;
- atomic publication;
- canonical handoff path;
- prior-session cleanup;
- consumer independence;
- no direct provider/SQLite presentation bypass;
- no alternate transport;
- no stale handoff acceptance;
- no custom IPC.

Explicitly answer:

`Does a bounded retry on transient Windows replacement errors alter observable contract semantics?`

Required answer for acceptance:
No, except that transient OS-level contention may now succeed within the bounded retry window instead of surfacing immediately.

Persistent failure must still surface after the bound.

---

# 5. Focused atomic replacement gate

Run the exact formerly failing atomic replacement test:

- 3 consecutive runs minimum.

Require:
- 3/3 pass.

Then run full publisher-focused suite.

Expected current reference:
- 4/4.

No unexplained skip.

---

# 6. Infrastructure regression

Run the full Infrastructure suite.

Require:
- **182/182**
- 0 failed
- 0 unexplained skipped

If count differs:
BLOCK until reconciled.

---

# 7. WP08 lifecycle preservation

Run the full WP08 focused lifecycle suite.

Require:
- **18/18**

Specifically preserve:
- atomic handoff publication;
- P1/P2 readiness;
- graceful CTRL_BREAK;
- Worker A → Worker B restart;
- stale handoff rejection;
- no forced kill on passing path;
- residue cleanup.

This is mandatory because the publisher behavior participates directly in WP08 handoff/restart acceptance.

---

# 8. WP09 permanent integration preservation

Run the exact permanent WP09 integration scenarios.

Require:
- Ready;
- WarmUp;
- Empty;
- Failed;

all pass at the expected permanent test count.

Also run WP09 architecture/no-bypass suite:
- expected **8/8**.

Confirm the retry did not introduce:
- UI/provider/SQLite bypass;
- alternate handoff mechanism;
- changed publication identity semantics.

---

# 9. Full .NET regression

Run:

- Domain
- Application
- Infrastructure
- Architecture
- full solution

Required totals:

- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- aggregate **339/339**

Build:
- **0 warnings**
- **0 errors**

No test-count delta is authorized by this fix.

---

# 10. Python regression

Run full governed Python predecessor suite.

Require:
- **17/17**

Also:
- Streamlit **1.61.1**
- `pip check` clean

No Python mutation.

---

# 11. Schema/security preservation

Read-only confirm:

## Schema
- SQLite persistence schema remains v4.
- no migration/table/index/version change in fix diff.

## Security/no-bypass
Run/inspect permanent architecture/security gates as required.

Confirm:
- presentation does not access SQLite/provider directly;
- canonical JSON handoff remains the cross-process boundary;
- local Smart App Control signing semantics unchanged;
- no new secret/private-key material.

---

# 12. Residue

After all tests:

Require zero owned:
- Worker;
- testhost;
- Python;
- Streamlit;
- listeners;
- handoff temp siblings;
- test-owned SQLite/WAL/SHM/journal residue;
- harness runtime roots attributable to revalidation.

Only clean factually owned residue.

No global process kill.

---

# 13. Scope audit

Expected repository changes attributable to the accepted candidate fix are exactly:

1. `src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs`
2. `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationReadModelFilePublisherTests.cs`

This authority itself must add:
- zero repository mutations;
- zero Git mutations;
- zero GitHub mutations.

If other late changes are discovered:
BLOCK and report them separately.

---

# 14. Revalidation decision

Declare exactly one:

## PREDECESSOR-REVALIDATED
Only if every gate passes.

Then state:

`WP12 PR READINESS MAY RESUME AGAINST THE UPDATED TWO-FILE PREDECESSOR FIX`

## PREDECESSOR-NOT-REVALIDATED
If any gate fails.

Do not modify implementation to repair failures under this authority.

---

# 15. Git/GitHub boundary

Do not:
- stage;
- commit;
- branch;
- push;
- PR;
- close #237;
- mutate Project;
- close milestone #58;
- tag/release.

Expected final lifecycle:
- #233–#236 unchanged;
- #237 Open / Backlog;
- milestone #58 Open.

---

# Required success report

## Candidate fix
Exact two files and semantic change.

## Contract compatibility
Why bounded retry/test sharing preserve accepted handoff semantics.

## Focused publisher
- failing test 3/3
- suite 4/4

## WP08
18/18

## WP09
permanent integration scenarios + architecture 8/8

## .NET
11/11, 125/125, 182/182, 21/21, aggregate 339/339

## Python
17/17
Streamlit 1.61.1
pip check clean

## Build
0 warnings / 0 errors

## Schema/security
v4 and no-bypass preserved

## Residue
zero owned residue

## Scope
exactly two candidate-fix files; this revalidation authority added no repository mutation

## Lifecycle
#237 still Open/Backlog; milestone open

Required markers:

`POST-FIX PREDECESSOR REVALIDATION REPOSITORY MUTATIONS: ZERO`

`POST-FIX PREDECESSOR REVALIDATION GIT MUTATIONS: ZERO`

`POST-FIX PREDECESSOR REVALIDATION GITHUB MUTATIONS: ZERO`

`POST-FIX PREDECESSOR REVALIDATED — WP12 PR READINESS MAY RESUME`

Terminal:

`RELEASE 1.9 POST-FIX PREDECESSOR REVALIDATION COMPLETE`

---

# Required blocked report

Include:
- gates passed;
- exact failing gate;
- exact evidence;
- whether failure appears implementation, environment, or contract-related;
- zero mutation accounting.

Markers:

`POST-FIX PREDECESSOR REVALIDATION REPOSITORY MUTATIONS: ZERO`

`POST-FIX PREDECESSOR REVALIDATION GIT MUTATIONS: ZERO`

`POST-FIX PREDECESSOR REVALIDATION GITHUB MUTATIONS: ZERO`

Terminal:

`RELEASE 1.9 POST-FIX PREDECESSOR REVALIDATION BLOCKED`

Never emit COMPLETE unless the two-file fix preserves the full accepted Release 1.9 predecessor surface.

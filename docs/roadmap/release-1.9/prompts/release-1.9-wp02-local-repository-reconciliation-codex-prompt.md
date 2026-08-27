# Release 1.9 — WP02 Local Repository Reconciliation — Codex Authority

## Authority

This document grants **narrow local-repository reconciliation authority** for Release 1.9 WP02 after an interrupted cleanup patch left the local implementation state uncertain.

Current proven situation:

- WP02 canonical issue: **#227**
- WP02 remains blocked
- a repository cleanup patch was attempted
- the patch operation stalled
- the stalled process was terminated to avoid uncertain partial deletion
- the cleanup did **not** complete
- no GitHub mutation was made
- WP03 has not started

This authority exists only to determine the exact local repository state and reconcile it safely.

This is **not** authority to continue WP02 implementation.

This is **not** authority to perform GitHub lifecycle changes.

This is **not** authority to start WP03.

---

# Objective

Establish a fully proven local repository state after the interrupted cleanup attempt.

The reconciliation must answer, with evidence:

1. What files currently differ from the accepted pre-WP02 baseline?
2. Which differences were created by the blocked WP02 implementation attempt?
3. Which differences pre-existed WP02 and must not be touched?
4. Did the stalled cleanup partially delete, truncate, rename, or otherwise corrupt any path?
5. Can the repository be restored safely to the clean pre-WP02 baseline?
6. If not, can the exact partial WP02 state be proven sufficiently to support a later, separately authorized resume?

The preferred end state is the **clean pre-WP02 baseline**.

If clean restoration cannot be proven safe, stop with an exact classified inventory instead of guessing.

---

# Accepted Baseline Context

Before WP02 implementation began, the accepted Release 1.9 execution baseline included:

- branch: `main`
- predecessor baseline commit:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`
- local `HEAD` aligned with `origin/main`
- ahead/behind `0/0`
- zero tracked changes
- zero staged changes
- known Release 1.9 authority/control files preserved
- WP01 #226 complete
- WP02 #227 open
- WP03+ untouched

Use this as baseline context, but freshly prove current state.

Do not assume the interrupted cleanup changed only files you expect.

---

# Core Safety Rule

> Never delete, restore, reset, checkout, clean, or overwrite a path until its ownership and intended baseline state are proven.

A stalled cleanup creates uncertainty.

Treat every changed/deleted/untracked path as potentially valuable until classified.

---

# Explicitly Forbidden

Do not:

- run `git reset --hard`
- run `git clean -fd`, `git clean -fdx`, or equivalents
- run broad `git checkout -- .`
- run broad `git restore .`
- delete directories recursively without path-by-path proof
- remove untracked files merely because they appear WP02-related
- discard pre-existing user work
- continue WP02 implementation
- apply new feature patches
- modify GitHub
- change Project #2 state
- close #227
- start WP03
- change Python/package/schema versions
- change Release 1.9 planning authority
- stage or commit reconciliation work unless a later explicit authority permits it

---

# Phase 0 — Process and Repository Safety

Before touching files:

1. confirm no stalled patch/edit process is still running;
2. identify repository root;
3. record active branch;
4. record local `HEAD`;
5. record `origin/main`;
6. record ahead/behind;
7. record staged state;
8. record tracked worktree state;
9. record untracked files;
10. record deleted/renamed/type-changed paths if any.

Do not mutate anything in this phase.

If another process still has the repository open for mutation, stop.

---

# Phase 1 — Full State Inventory

Collect a fresh, exhaustive inventory using Git-native evidence.

At minimum, capture:

- `git status --short`
- `git status --porcelain=v2`
- staged diff name/status
- unstaged diff name/status
- staged full diff
- unstaged full diff
- untracked path list
- deleted paths
- renamed paths
- file type changes
- submodule state if applicable

For every changed path, classify current Git state exactly.

Do not rely on memory from the failed patch.

---

# Phase 2 — Baseline Reconstruction

For every tracked changed/deleted path:

1. obtain the baseline version from the accepted pre-WP02 commit or current `origin/main` if still identical;
2. compare current path to baseline;
3. determine whether the path existed before WP02;
4. determine whether the WP02 blocked run modified it;
5. determine whether the stalled cleanup partially reverted it.

For every untracked path:

1. determine creation time/context where safely observable;
2. inspect content;
3. compare against known WP02 authority/implementation scope;
4. determine whether it is:
   - pre-existing Release 1.9 authority/control artifact
   - WP02 implementation artifact
   - WP02 test artifact
   - temporary patch artifact
   - unrelated pre-existing file
   - unknown

Do not classify by filename alone when content/history evidence is available.

---

# Phase 3 — Ownership Classification

Assign every divergent path to exactly one category:

## A. Pre-existing protected state

Examples:

- Release 1.9 authority/definition/control files that existed before WP02
- unrelated user work
- unrelated repository state

Action: **preserve**

## B. Proven WP02-owned implementation change

A path may enter this category only when evidence shows it was created or modified by the blocked WP02 attempt.

Action: eligible for narrow restoration to pre-WP02 baseline.

## C. Proven WP02-owned temporary/cleanup artifact

Examples:

- patch temp files
- generated intermediate files
- partial replacement files

Action: eligible for removal only after identity is proven.

## D. Uncertain ownership

Any path whose origin cannot be established safely.

Action: **do not mutate**

A single material Category D path may block full cleanup.

---

# Phase 4 — Partial-Deletion / Corruption Audit

Specifically check for consequences of the stalled cleanup:

- tracked files missing unexpectedly
- zero-byte files
- truncated files
- malformed files
- half-applied textual hunks
- duplicate replacement files
- temporary suffix files
- conflict markers
- broken directory structure
- renamed files without source/destination consistency

Use baseline comparison rather than heuristics alone.

If any corruption is detected, identify whether restoration from the baseline is safe and path-local.

---

# Phase 5 — Reconciliation Decision Gate

Choose exactly one of these outcomes.

## Outcome A — Safe clean restoration is fully proven

Use this only when every WP02-owned divergent path is known and no protected/uncertain path would be affected.

Then restore **only those proven WP02-owned paths** to the accepted pre-WP02 baseline.

For tracked paths, prefer path-specific Git restoration from the proven baseline.

For untracked WP02-owned artifacts, remove only individually proven paths.

Do not use broad cleanup commands.

## Outcome B — Exact partial WP02 state is safe and coherent

Use this only if:

- full clean restoration is unnecessary or unsafe;
- every remaining divergence is classified;
- no corruption remains;
- the partial implementation state is internally coherent;
- later continuation could begin from this exact state under separate authority.

Do not continue implementation under this authority.

## Outcome C — State remains uncertain

If any material path cannot be classified or safely reconciled, stop.

Do not force a cleanup.

---

# Phase 6 — Path-by-Path Reconciliation

If Outcome A is selected:

For each proven WP02-owned tracked path:

1. record current hash/state;
2. record baseline hash/state;
3. restore only that path;
4. immediately verify it matches baseline.

For each proven WP02-owned untracked artifact:

1. record exact path and reason it is WP02-owned;
2. remove only that path;
3. verify no adjacent file was touched.

After every small batch, rerun `git status --short`.

Do not mutate protected or uncertain paths.

---

# Phase 7 — Final Repository Proof

After reconciliation, freshly prove:

- branch
- local `HEAD`
- `origin/main`
- ahead/behind
- staged paths
- tracked changes
- untracked inventory

If Outcome A was achieved, expected final repository state is:

- `main`
- local `HEAD` at the accepted baseline unless it had legitimately moved before WP02
- `origin/main` relation explicitly reported
- staged changes = 0
- tracked changes = 0
- only protected/pre-existing untracked files remain
- no WP02 implementation artifacts remain
- no corruption remains

If Outcome B was achieved, provide an exact remaining diff inventory and prove every path is classified.

---

# GitHub Guard

Perform only enough read-only GitHub verification to prove:

- #227 remains open
- #227 has not been transitioned to Done/closed
- WP03 has not started if that state can be observed safely

Do not mutate GitHub.

---

# Stop Conditions

Stop immediately if:

- another mutation process is active
- repository identity is unexpected
- current branch/history cannot be understood
- a changed path has uncertain ownership and cleanup would risk data loss
- baseline content cannot be reconstructed reliably
- a path appears partially corrupted and safe restoration cannot be proven
- broad cleanup would be required
- Git state changes unexpectedly during reconciliation
- any operation outcome is uncertain
- cleanup would touch WP03+ work or unrelated user work

On stop:

- make no further mutations
- preserve evidence
- report exact repository state
- list all classified and unclassified paths
- identify the precise blocker

---

# Success Criteria

Reconciliation succeeds only if one of these is proven:

## Success A — Clean pre-WP02 baseline restored

- all WP02-owned local implementation changes removed
- all WP02 temporary/cleanup artifacts removed
- no protected file changed
- no uncertain path remains
- tracked changes = 0
- staged changes = 0
- protected pre-existing untracked files preserved
- no corruption remains
- #227 remains open
- no GitHub mutation occurred
- WP03 not started

## Success B — Exact resumable partial state established

- every remaining divergent path is classified
- no corruption remains
- no uncertain ownership remains
- exact diff is documented
- no GitHub mutation occurred
- WP03 not started
- further implementation requires separate authority

Prefer Success A when safely possible.

---

# Required Completion Report

Return:

## Repository state before reconciliation

- branch
- HEAD
- origin/main
- ahead/behind
- staged summary
- tracked summary
- untracked summary

## Path classification

For every divergent path:

- path
- Git state
- category A/B/C/D
- ownership evidence
- action taken or preserved

## Corruption audit

State whether any partial deletion/truncation/half-applied patch artifact was found.

## Reconciliation outcome

State exactly one:

- `OUTCOME A — CLEAN PRE-WP02 BASELINE RESTORED`
- `OUTCOME B — EXACT PARTIAL WP02 STATE ESTABLISHED`
- `OUTCOME C — RECONCILIATION BLOCKED`

## Final repository proof

Report final:

- branch
- HEAD
- origin/main
- ahead/behind
- staged count
- tracked change count
- remaining untracked files

## GitHub guard

Confirm:

- #227 remains open
- no GitHub mutation was made
- WP03 not started

## Next step

If Outcome A:

`WP02 MAY BE RETRIED UNDER A FRESH EXECUTION AUTHORITY`

If Outcome B:

`WP02 MAY RESUME ONLY UNDER A NEW AUTHORITY THAT ACCEPTS THIS EXACT PARTIAL STATE`

Do not execute WP02 here.

---

# Terminal Markers

On Success A, end with exactly:

`RELEASE 1.9 WP02 LOCAL REPOSITORY RECONCILIATION COMPLETE — CLEAN BASELINE`

On Success B, end with exactly:

`RELEASE 1.9 WP02 LOCAL REPOSITORY RECONCILIATION COMPLETE — PARTIAL STATE PROVEN`

On blocker, end with exactly:

`RELEASE 1.9 WP02 LOCAL REPOSITORY RECONCILIATION BLOCKED`

Do not emit a completion marker unless the corresponding success criteria are freshly proven.

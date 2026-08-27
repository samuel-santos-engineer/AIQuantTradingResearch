# Release 1.9 — WP01 Release & Repository Preflight — Codex Execution Authority

## Authority

This document grants authority to execute **Release 1.9 WP01 — Release & Repository Preflight** against canonical GitHub issue **#226**.

This authority begins from the proven completed Release 1.9 planning state:

- milestone **#58** open;
- canonical WP set **#226–#237**;
- exactly 12 canonical Release 1.9 work packages;
- exactly 12 canonical Project #2 items;
- all canonical Project items at:
  - `Status = Backlog`
  - `Priority = P1`
  - `Release = 1.9`
  - authoritative Area;
- exact 11-edge linear dependency chain:
  `WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12`;
- historical duplicate **#225** closed, documented, removed from Project #2, and excluded from the canonical set;
- Release taxonomy contains exactly one `1.9` option;
- protected milestones **#59/#60/#50/#51/#61** unchanged;
- repository state unchanged;
- zero tracked/staged changes;
- nine pre-existing untracked Release 1.9 planning/definition authority files preserved, plus the two WP01 execution-control artifacts listed in Phase 2;
- Release 1.9 implementation not started;
- idempotent planning audit **GP1–GP20 = PASS**.

The terminal planning state is:

`RELEASE 1.9 GITHUB PLANNING COMPLETE`

and:

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

This authority is **only for WP01**.

It is not authority to execute WP02 or any later work package.

It is not authority to implement Release 1.9 product functionality unless the accepted WP01 definition explicitly requires a repository-level preflight artifact whose creation is itself the WP01 deliverable.

---

# Objective

Execute WP01 exactly as defined by the accepted Release 1.9 planning authority and issue #226.

The purpose of WP01 is to establish a fresh, evidence-backed, reproducible Release 1.9 execution baseline before any implementation work begins.

The preflight must prove the current repository, Git, Python, package, schema, test, release-definition, and GitHub planning state required by the Release 1.9 execution plan.

The output of WP01 must make it possible to answer:

> Is the repository and Release 1.9 planning state safe, coherent, reproducible, and ready for WP02 under the accepted Release 1.9 definition?

Do not infer readiness. Prove it.

---

# Authoritative Inputs

Use the following as authoritative inputs, in priority order:

1. canonical GitHub issue **#226**;
2. the accepted Release 1.9 authority/definition files already present in the repository;
3. the canonical Release 1.9 GitHub planning state;
4. repository-local project configuration and executable evidence;
5. predecessor release evidence where explicitly referenced by the Release 1.9 definition.

The nine pre-existing untracked Release 1.9 planning/definition authority files and the two WP01 execution-control artifacts listed in Phase 2 are read-only authority unless issue #226 explicitly authorizes creation or modification of a specific WP01-owned artifact.

Do not rewrite planning authority merely to restate observed facts.

If issue #226 and the accepted Release 1.9 authority files materially conflict, stop and report the conflict.

---

# Proven Predecessor Boundary

The accepted Release 1.9 planning baseline established:

- predecessor PR **#224** merged;
- predecessor boundary commit:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- local `main` matched `origin/main` at planning time;
- Python baseline: **3.13.15**;
- package baseline: **exact four pins**;
- schema baseline: **v3**;
- process boundary: **one-shot JSON-over-stdio**;
- Streamlit baseline: **1.61.1**;
- test baseline: **281/281 passing**.

These are historical accepted planning facts.

WP01 must freshly verify the current state rather than merely repeating them.

If current reality differs, classify the difference before any mutation.

---

# Scope

## Permitted activities

WP01 may perform read-only inspection and validation of:

- Git repository identity;
- active branch;
- local `HEAD`;
- `origin/main`;
- ahead/behind status;
- tracked changes;
- staged changes;
- untracked files;
- predecessor boundary commit;
- relevant tags/releases if required by the accepted WP01 definition;
- Python runtime/version;
- environment/interpreter identity where relevant;
- package manifest/lock/pin state;
- installed package state where required;
- schema version;
- schema files and invariants;
- one-shot JSON-over-stdio boundary;
- Streamlit version and configuration;
- current automated test suite;
- existing Release 1.9 authority/definition files;
- GitHub milestone #58;
- canonical issues #226–#237;
- Project #2 Release 1.9 field state;
- dependency chain;
- historical duplicate #225;
- protected milestones #59/#60/#50/#51/#61;
- any WP01-specific acceptance criteria contained in issue #226.

WP01 may create or update **only a WP01-owned preflight evidence artifact** if and only if the accepted WP01 definition requires such an artifact.

WP01 may update GitHub issue #226 / Project state only if the accepted execution governance explicitly requires it as part of WP completion.

## Explicitly forbidden

Do not:

- execute WP02 or later work;
- implement Release 1.9 product features;
- refactor unrelated code;
- change dependencies or package pins merely to make preflight pass;
- change Python version;
- change schema;
- change the stdio protocol boundary;
- change Streamlit version;
- fix failing tests unless issue #226 explicitly defines such a fix as WP01 work;
- alter the Release 1.9 work-package plan;
- recreate or reconcile planning objects already proven complete;
- modify #225 other than read-only verification;
- modify protected milestones #59/#60/#50/#51/#61;
- create new Release 1.9 work packages;
- change the dependency chain;
- stage or commit unrelated files;
- normalize or rewrite the nine pre-existing Release 1.9 planning/definition authority files or either WP01 execution-control artifact;
- silently repair divergence discovered during preflight.

A preflight failure is evidence, not implicit authorization to repair.

---

# Execution Protocol

## Phase 0 — Load WP01 authority

Before any mutation:

1. Read canonical issue **#226** completely.
2. Read the accepted Release 1.9 definition/authority files relevant to WP01.
3. Extract the exact WP01:
   - objective;
   - scope;
   - deliverables;
   - acceptance criteria;
   - required evidence;
   - permitted files, if any;
   - expected GitHub completion semantics, if any.
4. Build an in-memory checklist from those authoritative criteria.

Do not rely on this prompt to replace more specific WP01 criteria found in #226.

If the issue cannot be read or WP01 criteria are ambiguous, stop.

---

## Phase 1 — Repository and Git identity proof

Prove:

- repository root;
- repository remote identity;
- active branch;
- exact local `HEAD`;
- exact `origin/main`;
- ahead/behind counts;
- whether local `main` equals `origin/main`;
- staged path count;
- tracked working-tree modification count;
- untracked path inventory.

The accepted planning baseline expected:

- `main`;
- local `main == origin/main`;
- staged paths = 0;
- no tracked modifications;
- nine pre-existing untracked Release 1.9 planning/definition authority files plus the two WP01 execution-control artifacts listed in Phase 2.

Freshly verify all of it.

### Predecessor boundary proof

Prove that commit:

`3a02f035a253e4e16f479e1866c9a5195f5cfbdb`

is present in repository history and determine its exact relationship to current `HEAD`.

If the accepted WP01 definition requires proof of PR #224 merge ancestry, verify that too.

Do not reset, checkout, pull, merge, rebase, or otherwise mutate Git merely to force alignment.

### Hard stop

If local repository state is unexpectedly dirty or branch/history state differs materially from the accepted execution baseline, stop before mutation unless #226 explicitly authorizes a specific corrective preflight action.

---

## Phase 2 — Release 1.9 authority-file integrity

Enumerate the typed WP01 execution inventory:

- nine pre-existing untracked Release 1.9 planning/definition authority files; and
- two known WP01 execution-control artifacts:
  - `release-1.9-wp01-release-repository-preflight-codex-prompt.md`;
  - `release-1.9-wp01-release-repository-preflight-codex-prompt-chat.md`.

Prove:

- exact typed inventory = 9 preserved baseline files + 2 WP01 control artifacts = 11 files;
- expected paths;
- readable content;
- no accidental staging;
- no tracked replacement/collision;
- no unexplained additional Release 1.9 authority/execution file;
- no mutation during WP01 unless explicitly authorized.

If practical, record stable content hashes before execution and compare them at completion.

If the typed inventory, path set, or integrity differs from the accepted planning baseline, stop and report the exact divergence.

---

## Phase 3 — Python/runtime baseline

Freshly prove:

- Python executable used by the project;
- Python version;
- expected version from project configuration;
- any virtual environment identity needed for reproducibility.

Accepted planning baseline:

`Python 3.13.15`

Do not install or change Python as part of preflight unless issue #226 explicitly authorizes it.

If runtime and project expectation differ, record the mismatch and stop if it invalidates WP01 acceptance.

---

## Phase 4 — Package/dependency baseline

Determine the authoritative package/dependency definition used by the repository.

Prove the accepted baseline of **exact four pins**, including:

- authoritative file(s);
- exact package names;
- exact versions;
- absence of unintended extra direct pins if that is part of the accepted invariant.

Where WP01 requires environment validation, compare installed versions to the authoritative pins.

Accepted planning evidence includes:

- Streamlit **1.61.1**.

Do not install, upgrade, downgrade, or rewrite package files merely to make the evidence conform.

If installed state is not required by #226, do not broaden scope unnecessarily.

---

## Phase 5 — Schema baseline

Freshly prove:

- authoritative schema location;
- schema version;
- expected invariants relevant to Release 1.9.

Accepted planning baseline:

`schema v3`

Do not migrate or edit schema in WP01 unless #226 explicitly requires a WP01-owned metadata artifact rather than schema behavior.

Any schema divergence must be reported as a blocker rather than silently repaired.

---

## Phase 6 — Process/protocol boundary proof

Prove the accepted application/process boundary:

`one-shot JSON-over-stdio`

Use repository code, tests, specifications, or executable validation as appropriate.

The proof should establish, at minimum where applicable:

- process invocation model;
- JSON input boundary;
- stdout output boundary;
- one-shot lifecycle;
- absence of an unintended persistent/server protocol replacing the accepted boundary.

Do not redesign or refactor the boundary in WP01.

---

## Phase 7 — Streamlit baseline

Prove:

- authoritative Streamlit pin/configuration;
- expected version;
- installed/runtime version if WP01 requires executable environment validation.

Accepted planning baseline:

`Streamlit 1.61.1`

Do not change the pin or runtime.

---

## Phase 8 — Automated test baseline

Run the authoritative automated test suite exactly as the repository defines it.

Capture:

- exact command;
- exit status;
- passed count;
- failed count;
- skipped/xfailed/etc. if applicable;
- any warnings material to Release 1.9 readiness.

Accepted planning baseline:

`281/281 passing`

WP01 must report the current observed count.

Do not suppress failures.

Do not patch tests or implementation merely to restore 281/281.

If the suite now contains a different number of tests, determine whether the change is expected from repository history. If not provable within WP01 authority, treat it as a blocker.

---

## Phase 9 — GitHub planning-state audit

Perform a read-only audit of the canonical Release 1.9 planning state.

Prove:

### Canonical issues

Exactly:

- WP01 #226
- WP02 #227
- WP03 #228
- WP04 #229
- WP05 #230
- WP06 #231
- WP07 #232
- WP08 #233
- WP09 #234
- WP10 #235
- WP11 #236
- WP12 #237

For each canonical WP prove:

- open;
- assigned as required by the planning baseline;
- milestone #58;
- exactly one matching Project #2 item;
- Status = Backlog;
- Priority = P1;
- Release = 1.9;
- authoritative Area.

### Historical duplicate

Prove:

- #225 remains closed;
- #225 remains documented as historical duplicate;
- #225 has no Project #2 item;
- #225 is excluded from the canonical set.

### Milestone

Prove:

- milestone #58 open;
- canonical count = 12 open / 0 closed;
- raw GitHub counter may be 12 open / 1 closed because #225 remains historically attached.

Do not misclassify the raw closed counter as a planning failure.

### Release taxonomy

Prove exactly one Project Release option corresponding to `1.9`.

### Protected milestones

Read-only prove existence/state of:

- #59
- #60
- #50
- #51
- #61

Do not mutate them.

---

## Phase 10 — Dependency-chain proof

Freshly verify all 11 canonical edges:

1. WP01 → WP02
2. WP02 → WP03
3. WP03 → WP04
4. WP04 → WP05
5. WP05 → WP06
6. WP06 → WP07
7. WP07 → WP08
8. WP08 → WP09
9. WP09 → WP10
10. WP10 → WP11
11. WP11 → WP12

Prove:

- exactly 11 canonical Release 1.9 edges;
- no missing edge;
- no extra canonical edge;
- direction semantics match the accepted plan.

This phase is read-only.

Do not reconcile or repair dependencies under WP01.

---

## Phase 11 — WP01-specific artifact, only if required

If issue #226 explicitly requires a repository preflight artifact:

1. identify the exact authorized path;
2. ensure it is WP01-owned;
3. create/update only that artifact;
4. include evidence sufficient to reproduce the preflight;
5. avoid copying volatile data unnecessarily if the accepted format expects commands plus results;
6. do not modify unrelated authority files.

If no artifact is required, do not invent one.

---

## Phase 12 — Full completion proof

Before declaring WP01 complete, freshly verify:

- repository identity unchanged;
- active branch unchanged;
- local `HEAD` relationship unchanged except for an explicitly authorized WP01 commit, if any;
- staged paths = 0 unless governance explicitly requires a staged WP01 artifact;
- no unrelated tracked changes;
- nine pre-existing Release 1.9 planning/definition authority files and both WP01 execution-control artifacts preserved;
- Python state unchanged;
- package state unchanged;
- schema unchanged;
- process/protocol boundary unchanged;
- Streamlit baseline unchanged;
- test suite result recorded;
- GitHub planning state still canonical;
- dependency chain still 11/11;
- protected milestones unchanged;
- no WP02+ implementation started.

If a WP01 artifact was created, prove it is the only intended repository mutation attributable to this authority.

---

# Mutation Discipline

WP01 is fundamentally a preflight and evidence work package.

The default rule is:

> Read, prove, record — do not repair.

For any permitted mutation:

1. prove that #226 explicitly authorizes it;
2. read current state;
3. mutate the minimum necessary object;
4. immediately read back;
5. ensure no adjacent state changed;
6. stop on unexpected divergence.

Do not use broad formatting, cleanup, dependency refresh, or repository normalization operations.

---

# Stop Conditions

Stop immediately and do not broaden scope if any of the following occurs:

- issue #226 cannot be read;
- accepted WP01 criteria are ambiguous;
- issue #226 conflicts materially with accepted Release 1.9 authority;
- repository identity is unexpected;
- branch is unexpected;
- local/remote history is unexpectedly divergent;
- tracked/staged changes exist unexpectedly;
- any pre-existing baseline authority/definition file or either WP01 execution-control artifact is missing, altered, staged, or replaced;
- an unexplained additional Release 1.9 authority/execution file exists;
- Python baseline differs materially;
- package pin invariant differs;
- schema version differs;
- one-shot JSON-over-stdio boundary cannot be proven;
- Streamlit baseline differs;
- automated tests fail;
- automated test count diverges without an authoritative explanation;
- canonical GitHub planning state differs;
- Project item count/fields differ;
- dependency chain differs;
- protected milestones appear changed;
- completing WP01 would require implementing WP02 or broader Release 1.9 functionality;
- any operation would require authority broader than this document.

On stop:

- perform no speculative repair;
- preserve evidence;
- report exact observed state;
- identify last proven invariant;
- identify the precise blocker.

---

# Success Criteria

WP01 succeeds only when all authoritative issue #226 acceptance criteria and all applicable conditions below are freshly proven:

- canonical issue #226 governs WP01;
- repository identity correct;
- expected branch state proven;
- predecessor boundary commit present and correctly related to current `HEAD`;
- zero unexpected tracked changes;
- zero staged paths at completion unless explicitly authorized otherwise;
- nine pre-existing Release 1.9 planning/definition authority files and both WP01 execution-control artifacts preserved;
- Python baseline proven;
- exact four package pins proven;
- Streamlit 1.61.1 baseline proven;
- schema v3 proven;
- one-shot JSON-over-stdio boundary proven;
- automated test suite passes at the current authoritative count;
- canonical WP map #226–#237 unchanged;
- 12/12 Project #2 Release 1.9 items valid;
- historical duplicate #225 remains excluded;
- milestone #58 canonical state valid;
- exactly one Release `1.9` Project taxonomy option;
- 11/11 dependency edges verified;
- #59/#60/#50/#51/#61 unchanged;
- no WP02+ implementation begun;
- any WP01-specific required evidence artifact completed and verified;
- no unauthorized mutation occurred.

---

# Required Completion Report

Return an evidence-based completion report containing:

## WP01 authority

- issue #226 title;
- extracted WP01 objective;
- extracted acceptance criteria.

## Repository/Git proof

- repository;
- branch;
- local `HEAD`;
- `origin/main`;
- ahead/behind;
- predecessor boundary relationship;
- tracked/staged/untracked summary.

## Authority-file proof

- typed inventory: 9 preserved baseline files + 2 WP01 execution-control artifacts = 11;
- paths or concise inventory;
- integrity result.

## Runtime/package/schema proof

- Python version;
- four exact package pins;
- Streamlit version;
- schema version;
- process/protocol boundary.

## Test proof

- exact command;
- exit status;
- exact result counts.

## GitHub planning proof

- canonical WP map #226–#237;
- 12/12 Project items;
- milestone #58 canonical counts;
- #225 historical duplicate state;
- Release taxonomy proof;
- protected milestone proof.

## Dependency proof

List all final 11 edges.

## Mutation proof

State exactly what, if anything, changed under WP01.

If nothing changed, explicitly state:

`WP01 PREFLIGHT MUTATIONS: ZERO`

## Readiness conclusion

State whether WP01 acceptance is satisfied and whether WP02 is eligible for separate authorization.

Do not authorize WP02 yourself.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP01 RELEASE & REPOSITORY PREFLIGHT COMPLETE`

On a safe stop/blocker, end with exactly:

`RELEASE 1.9 WP01 RELEASE & REPOSITORY PREFLIGHT BLOCKED`

Do not emit the success marker unless every applicable WP01 acceptance criterion has been freshly proven.

---

# After Success

Do not begin WP02 automatically.

Do not change Project status for WP02.

Do not infer authorization for any later work package.

The only permissible post-WP01 statement is that WP02 is **eligible for separate authorization** if the accepted dependency/governance model permits it.

# Release 1.9 — WP09-Specific Execution Authority

## Recommended model
Use **GPT-5.6 Terra**.

Rationale:
- WP09 begins after a long, fully accepted WP01→WP08 dependency chain.
- Execution requires disciplined repository/GitHub state verification, implementation, testing, residue/scope audits, and lifecycle completion.
- Terra is the preferred execution model for this governed implementation/completion pass.
- Use Luna later for a narrow contract/authority amendment if execution exposes an ambiguity requiring definition rather than implementation.
- Sol is not preferred for this primary governed WP09 implementation pass.

---

# Sole authority

This is the **fresh WP09-specific implementation, validation, and completion authority** for Release 1.9.

Canonical work package:

`WP09 — GitHub issue #234`

WP08 / #233 is a completed predecessor and must remain frozen.

This authority must first discover and read the repository's canonical Release 1.9 planning artifacts, WP09 issue, manifest/path authority, accepted predecessor contracts, and current GitHub Project state before deciding what WP09 implementation is authorized.

Do not infer WP09 scope from conversation summaries alone.

---

# Accepted predecessor boundary

Treat as binding unless current repository/GitHub read-back contradicts it:

## WP08
- #233 Closed / Done.
- Exactly one canonical Project #2 item.
- Release 1.9.
- Priority P1.
- Area Infrastructure.
- technical acceptance complete.
- focused WP08: 18/18.
- .NET aggregate: 327/327.
- Python predecessor suites green.
- WP08 implementation frozen.

## Successor
- #234 Open / Backlog.
- WP09 unstarted.
- milestone #58 Open.
- latest accepted milestone count: 4 open / 9 closed.

## Repository boundary
Latest reported:
- `main` at `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- ahead/behind 0/0.

Verify these facts rather than blindly assuming them.

---

# Primary objective

Implement exactly the canonical WP09 scope defined by:
1. issue #234;
2. Release 1.9 definition/plan;
3. Release 1.9 work-package manifest;
4. any WP09-specific contract/path-authority artifact;
5. accepted predecessor contracts.

Then:
- run focused validation;
- run required regressions;
- prove scope/residue;
- transition #234 to Done/Closed only if every acceptance gate passes.

If those authorities do not provide enough semantic/path authority for implementation, STOP before mutation and report the minimum narrow follow-up authority required.

---

# Phase 0 — Read-only entry verification

Before any mutation:

## Git
Verify:
- current branch;
- HEAD;
- `origin/main`;
- ahead/behind;
- staged;
- unstaged;
- untracked.

Do not discard pre-existing work.

## Build/runtime baseline
Read repository configuration and record:
- .NET SDK/runtime;
- Python version;
- exact governed Python package pins;
- Streamlit version;
- schema/version boundary where relevant.

## GitHub
Read:
- #233;
- #234;
- milestone #58;
- Project #2 item for #234.

Require:
- #233 Closed / Done;
- #234 Open / Backlog;
- exactly one canonical Project item for #234.

Record #234:
- title;
- body/acceptance criteria;
- milestone;
- Release;
- Priority;
- Area;
- dependencies.

No mutation yet.

---

# Phase 1 — Canonical WP09 authority discovery

Search/read completely all relevant Release 1.9 artifacts, including as applicable:

- Release 1.9 definition;
- implementation plan;
- work-package manifest;
- dependency map;
- path authority;
- WP09-specific definition/contract;
- WP08 completion/evidence docs needed as predecessor;
- issue #234.

Produce an internal binding summary:

`WP09 requirement → authority source → authorized paths → acceptance evidence`

Do not mutate until this mapping is complete.

---

# Phase 2 — Scope sufficiency gate

Before implementation answer:

1. What exactly must WP09 deliver?
2. Which production paths may change?
3. Which test paths may change/create?
4. Which documentation/evidence paths may change?
5. Which paths are forbidden?
6. What are the exact acceptance gates?
7. What predecessor behavior must remain unchanged?
8. What GitHub lifecycle mutations are allowed at completion?

If any material answer is undefined or contradictory:

STOP.

Required report:

`WP09 IMPLEMENTATION BLOCKED BEFORE MUTATION — NARROW <missing-contract> AUTHORITY REQUIRED`

No speculative implementation.

---

# Phase 3 — Predecessor baseline

Run only the predecessor gates required by the canonical WP09 authority.

At minimum preserve the latest accepted Release 1.9 baseline relevant to WP09.

Do not rerun large suites unnecessarily if the canonical plan defines a smaller entry gate, but record inherited WP08 evidence.

If an entry gate fails:
- distinguish environment failure from implementation regression;
- do not mutate WP09 until the predecessor boundary is trustworthy.

---

# Phase 4 — Minimal implementation

Implement exactly WP09.

Rules:
- smallest coherent change;
- no opportunistic refactor;
- no package upgrades unless explicitly authorized;
- no schema expansion unless explicitly authorized;
- no generic framework introduced for a one-off WP09 requirement;
- preserve WP08 lifecycle/cancellation/handoff behavior;
- preserve WP05→WP07 semantics unless WP09 explicitly extends them.

Every changed path must map to explicit authority.

---

# Phase 5 — Focused tests

Create/modify only authorized WP09 tests.

Tests must prove the issue #234 acceptance criteria directly.

Prefer:
- deterministic tests;
- bounded time;
- no network dependency unless explicitly required;
- harness-owned temp resources;
- explicit cleanup;
- exact semantic assertions rather than presentation-only snapshots.

Report exact focused test count and delta.

---

# Phase 6 — Focused execution

Run WP09 focused tests first.

Require:
- all pass;
- build 0 warnings / 0 errors where applicable.

If focused tests fail:
- fix only within authorized WP09 scope;
- if failure reveals missing authority, STOP rather than broadening scope.

---

# Phase 7 — Predecessor preservation

Run the exact predecessor suites affected by WP09.

Preserve:
- WP08 focused lifecycle behavior;
- graceful cancellation;
- shared-runtime restart;
- Windows helper semantics;
- WP05/WP06/WP07 chain;
- Smart App Control local-development signing boundary.

No regression may be waived.

---

# Phase 8 — Full regression

After focused acceptance:

## .NET
Run governed:
- Domain;
- Application;
- Infrastructure;
- Architecture;
- full solution/build.

Use current baseline, not historical 313.

Latest accepted pre-WP09 aggregate:
- Domain 11;
- Application 125;
- Infrastructure 178;
- Architecture 13;
- total 327.

Explain the exact WP09 test-count delta.

## Python
Run the canonical Release 1.9 predecessor Python gates:
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- any WP09 Python tests if authorized;
- Streamlit 1.61.1;
- `pip check`.

---

# Phase 9 — Residue

Prove zero forbidden residue created by WP09.

As relevant:
- processes;
- listeners;
- handoff files;
- temp siblings;
- database/WAL/SHM/journal;
- runtime roots;
- generated evidence;
- caches.

Clean only harness-owned temporary resources.

Never use broad/global process termination.

---

# Phase 10 — Scope audit

Produce:

`changed path → reason → authority source → acceptance test`

Verify zero unauthorized:
- package;
- schema;
- Replay;
- WP08;
- signing;
- unrelated docs;
- WP10+;
- GitHub item creation/deletion.

If unauthorized mutation occurred:
- revert only your own WP09 mutation if safe;
- preserve pre-existing worktree state;
- report accurately.

---

# Phase 11 — WP09 acceptance matrix

Before GitHub mutation produce:

`#234 acceptance requirement → implementation → focused proof → regression proof → residue proof`

Every row must PASS.

If any row is incomplete:
- keep #234 Open / Backlog;
- GitHub lifecycle mutations zero.

---

# Phase 12 — Project identity

Only after technical acceptance:

Identify exactly one Project #2 item linked to canonical #234.

Record:
- item node ID;
- current Status;
- Release;
- Priority;
- Area.

Resolve exact Status field and Done option IDs.

If ambiguous:
- BLOCK;
- no lifecycle mutation.

---

# Phase 13 — GitHub lifecycle completion

Only after all technical gates pass:

1. set #234 Project Status → Done;
2. read back;
3. verify Release/Priority/Area unchanged;
4. close #234;
5. read back;
6. verify milestone #58 counts/state;
7. identify the next canonical Release 1.9 work package from the accepted manifest;
8. do not start it.

No Project item creation/deletion.

---

# Phase 14 — Final read-back

Verify:
- #233 remains Closed / Done;
- #234 Closed / Done;
- milestone #58 consistent;
- next WP remains Open / Backlog if already represented;
- repository state expected;
- no owned residue.

---

# GitHub mutation boundary

Before technical acceptance:

`WP09 GITHUB MUTATIONS: ZERO`

On successful completion, allowed only:

`#234 PROJECT STATUS → DONE`
`#234 ISSUE → CLOSED`

All other GitHub mutations zero unless the canonical WP09 authority explicitly requires another lifecycle action.

---

# Required completion report

## Entry
Git/repository/GitHub predecessor state.

## Binding WP09 scope
Exact issue requirements and authority sources.

## Implementation
Changed paths and behavior.

## Focused validation
Exact results.

## Preservation
WP08 and earlier predecessor results.

## Full regression
.NET/Python exact counts.

## Residue
Exact cleanup/final state.

## Scope audit
Authorized paths only.

## #234 acceptance matrix
All rows.

## GitHub
Project identity, mutations, read-back.

## Milestone
State/counts.

## Next eligible WP
Name/issue only; do not start it.

---

# Stop conditions

STOP before mutation if:
- canonical WP09 scope is missing;
- issue #234 conflicts with manifest/plan;
- path authority is insufficient;
- predecessor #233 is not Closed / Done;
- multiple/zero #234 Project items create lifecycle ambiguity.

STOP during implementation if:
- required change crosses unauthorized production/test paths;
- package/schema/transport expansion is needed but not authorized;
- WP08 behavior must be altered without explicit authority.

STOP before GitHub lifecycle mutation if:
- any acceptance/regression/residue gate fails.

---

# Terminal markers

Successful implementation and lifecycle completion:

`RELEASE 1.9 WP09 IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP09 IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless WP09 is technically accepted and #234 is authoritatively Closed / Done.

# Release 1.9 — WP10-Specific Execution Authority

## Recommended model
Use **GPT-5.6 Terra**.

Use Luna only if execution discovers a genuine missing semantic/contract/path authority. Do not use a definition pass preemptively if the canonical WP10 artifacts are already sufficient.

---

# Sole authority

This is the **fresh WP10-specific implementation, validation, and completion authority** for Release 1.9.

Canonical work package:

`WP10 — GitHub issue #235`

WP09 / #234 is completed and frozen.

This authority must first discover and read the canonical WP10 scope from the repository and GitHub. Do not infer WP10 implementation from conversation summaries or issue numbering alone.

---

# Accepted predecessor boundary

Treat as binding unless current read-back contradicts it.

## WP09
- #234 Closed / Done.
- Project item: `PVTI_lAHOCAzBgs4BfsiAzg33XcY`.
- Release 1.9 / P1 / Testing preserved.
- permanent WP09 additions:
  - 4 Infrastructure integration tests;
  - 8 Architecture no-bypass tests;
  - 4 Python presentation tests.
- WP09 focused: 12 .NET + 4 Python passed.
- WP08 preservation: 18/18.
- full .NET:
  - Domain 11/11
  - Application 125/125
  - Infrastructure 182/182
  - Architecture 21/21
  - aggregate **339/339**
- governed Python WP05–WP09: **17/17**.
- Streamlit 1.61.1.
- `pip check` clean.
- build 0 warnings / 0 errors.
- residue clean.

## WP10 entry
- #235 expected Open / Backlog.
- milestone #58 expected Open.
- latest accepted milestone count: 3 open / 10 closed.
- WP10 unstarted.

Verify rather than blindly assume.

---

# Primary objective

Read and reconcile:

1. issue #235;
2. Release 1.9 definition;
3. Release 1.9 execution plan;
4. work-package manifest;
5. dependency map;
6. path/ownership authority;
7. any WP10-specific contract;
8. accepted WP09 completion boundary.

Then either:

### A. Authority is sufficient
Implement exactly WP10, validate it, preserve predecessors, run required regressions/residue audits, and complete #235 lifecycle.

### B. Authority is materially incomplete
STOP **before implementation mutation** and identify the minimum narrow Luna contract/path authority required.

Do not invent material WP10 semantics.

---

# Phase 0 — Read-only repository/GitHub entry

Verify:

## Git
- branch;
- HEAD;
- `origin/main`;
- ahead/behind;
- staged;
- unstaged;
- untracked.

Preserve unrelated pre-existing changes.

## GitHub
Read:
- #233;
- #234;
- #235;
- milestone #58;
- Project #2 item linked to #235.

Require:
- #233 Closed / Done;
- #234 Closed / Done;
- #235 Open / Backlog;
- exactly one canonical Project item for #235.

Record #235:
- exact title;
- body;
- acceptance criteria;
- milestone;
- Release;
- Priority;
- Area;
- dependencies;
- Project item node ID.

No mutation yet.

---

# Phase 1 — Canonical WP10 authority discovery

Read completely all relevant Release 1.9 artifacts.

Search explicitly for:
- WP10;
- #235;
- work-package sequence;
- authorized paths;
- acceptance gates;
- predecessor dependencies;
- expected test-count delta;
- GitHub lifecycle requirements.

Do not treat a high-level roadmap sentence as implementation authority if exact semantics are required.

---

# Phase 2 — Binding authority map

Before mutation, construct:

`WP10 requirement → source artifact → authorized path(s) → acceptance proof`

Answer exactly:

1. What does WP10 deliver?
2. What production paths may change?
3. What test paths may change/create?
4. What Python/documentation paths may change?
5. Are packages/schema/IPC/persistence changes authorized?
6. What exact behavior/assertions define acceptance?
7. What exact test-count delta is expected?
8. Which predecessor suites must remain unchanged?
9. What residue/security gates apply?
10. What lifecycle mutations are permitted?

If any material answer is missing or contradictory:
- STOP before mutation.

Required blocker wording:

`WP10 IMPLEMENTATION BLOCKED BEFORE MUTATION — NARROW <exact missing authority> REQUIRED`

---

# Phase 3 — Entry/predecessor validation

Run only the entry gates required by canonical WP10 authority.

Use accepted baseline:
- .NET 339/339;
- Python 17/17;
- WP08 18/18 where relevant;
- build 0/0.

If canonical WP10 authority specifies narrower entry gates, follow it.

Do not modify predecessors to make the entry gate pass.

---

# Phase 4 — Minimal WP10 implementation

If authority is sufficient:

- implement exactly #235;
- use only authorized paths;
- smallest coherent change;
- no opportunistic refactor;
- no package upgrade unless explicitly authorized;
- no schema/transport expansion unless explicitly authorized;
- no WP09 semantic changes;
- no WP11+ work.

Every changed path must map to a binding authority row.

---

# Phase 5 — Focused WP10 tests

Create/modify only authorized WP10 tests.

Tests must directly prove #235 acceptance.

Requirements:
- deterministic;
- bounded;
- no live network unless explicitly required;
- harness-owned resources;
- explicit cleanup;
- semantic assertions;
- no arbitrary sleeps where deterministic readiness is available.

Record exact new test delta.

If the canonical authority fixes an exact delta, enforce it before regression.

---

# Phase 6 — Focused execution

Run WP10-focused validation first.

Require:
- all focused WP10 tests pass;
- build 0 warnings / 0 errors where applicable.

If failure reveals a production semantic requirement outside authority:
- STOP;
- request narrow Luna authority;
- do not broaden implementation.

---

# Phase 7 — Predecessor preservation

Run all predecessor suites materially touched by WP10.

At minimum preserve as applicable:
- WP09 permanent integration and architecture coverage;
- WP08 lifecycle 18/18;
- WP05–WP09 Python 17/17;
- no-bypass architecture rules;
- Worker/handoff/lifecycle semantics;
- local-development signing remains opt-in and development-only.

No predecessor regression may be waived.

---

# Phase 8 — Full .NET regression

Current accepted pre-WP10 baseline:

- Domain 11
- Application 125
- Infrastructure 182
- Architecture 21
- total **339**

Expected post-WP10 total:

`339 + exact authorized WP10 .NET test delta`

Require:
- exact expected total;
- 0 failures;
- no unexplained skipped tests;
- build 0 warnings / 0 errors.

Do not reuse stale 309/313/327 baselines.

---

# Phase 9 — Python regression

Current accepted pre-WP10 governed Python baseline:

**17/17**

Run:
- all canonical predecessor Python suites;
- WP10 Python tests if authorized;
- Streamlit 1.61.1;
- `pip check`;
- any compile/import gates defined by WP10.

Expected post-WP10:

`17 + exact authorized WP10 Python delta`

No unexplained deviation.

---

# Phase 10 — Architecture/security

Run the architecture/security gates required by #235 and the Release 1.9 plan.

Preserve WP09 no-bypass guarantees:
- no presentation direct SQLite;
- no presentation direct provider;
- no unauthorized Infrastructure dependency;
- Worker producer / Streamlit consumer ownership;
- canonical JSON handoff boundary;
- no unauthorized Release 1.8 endpoint expansion.

Add only WP10-specific rules explicitly authorized.

---

# Phase 11 — Residue

Prove zero forbidden WP10-owned residue.

As applicable:
- Worker;
- testhost;
- Python;
- Streamlit;
- listeners;
- handoff files;
- temp siblings;
- databases/sidecars;
- runtime roots;
- caches/evidence files.

Clean only resources factually owned by the WP10 harness.

Never broad-kill unrelated processes.

---

# Phase 12 — Scope audit

Produce:

`changed path → reason → authority source → acceptance proof`

Require zero unauthorized:
- production paths;
- tests;
- packages;
- schema;
- signing;
- WP08/WP09;
- WP11+;
- GitHub item creation/deletion.

Preserve unrelated pre-existing worktree changes.

---

# Phase 13 — WP10 acceptance matrix

Before GitHub mutation:

`#235 acceptance criterion → implementation → focused proof → regression proof → security/residue proof`

Every row must PASS.

If any row is incomplete:
- keep #235 Open / Backlog;
- GitHub lifecycle mutations zero.

---

# Phase 14 — Project identity

Only after technical acceptance:

Identify exactly one Project #2 item linked to canonical #235.

Record:
- item node ID;
- Status;
- Release;
- Priority;
- Area.

Resolve exact Status field and Done option IDs.

Do not identify solely by title when linked issue identity is available.

If zero/multiple items:
- BLOCK;
- no lifecycle mutation.

---

# Phase 15 — GitHub lifecycle completion

Only after every technical gate passes:

1. set #235 Project Status → Done;
2. read back;
3. verify Release/Priority/Area unchanged;
4. close #235;
5. read back;
6. verify #234 remains Closed / Done;
7. verify #233 remains Closed / Done;
8. read milestone #58 state/counts;
9. identify the next canonical Release 1.9 work package;
10. do not start it.

No Project item creation/deletion.

Do not close milestone #58 unless a separate canonical release-completion authority explicitly permits it.

---

# Phase 16 — Final read-back

Verify:
- #235 Closed / Done;
- #234 Closed / Done;
- #233 Closed / Done;
- milestone #58 consistent;
- next WP untouched;
- repository/Git state expected;
- no WP10-owned residue.

---

# Completion gate

WP10 is complete only if:

1. canonical #235 scope was fully identified;
2. implementation stayed inside exact authority;
3. all focused WP10 acceptance tests pass;
4. exact test-count delta is proven;
5. predecessor suites remain green;
6. full .NET matches exact post-WP10 expected count;
7. Python matches exact post-WP10 expected count;
8. build 0/0;
9. architecture/security gates pass;
10. residue clean;
11. scope audit clean;
12. #235 Project item = Done;
13. #235 issue = Closed;
14. #233/#234 preserved;
15. WP11+ unstarted.

---

# GitHub mutation boundary

Before technical acceptance:

`WP10 GITHUB MUTATIONS: ZERO`

Successful lifecycle may perform only:

`#235 PROJECT STATUS → DONE`
`#235 ISSUE → CLOSED`

unless canonical WP10 authority explicitly requires another WP10 lifecycle mutation.

All other GitHub mutations zero.

---

# Required completion report

## Entry state
Git, predecessor issues, #235, Project item, milestone.

## Binding WP10 scope
Exact requirements and sources.

## Implementation
Exact changed paths.

## Test delta
Exact .NET/Python additions.

## Focused validation
Exact results.

## Predecessor preservation
WP09/WP08/Python results.

## Full regression
Exact .NET/Python totals.

## Architecture/security
Exact results.

## Residue
Exact final state.

## Scope audit
Authorized paths only.

## #235 acceptance matrix
Every row.

## GitHub
Project item ID, Done, Closed, read-back.

## Milestone
State/counts.

## Next eligible WP
Name/issue only; do not start it.

---

# Stop conditions

STOP before mutation if:
- #235 scope is ambiguous;
- canonical artifacts conflict;
- exact path authority is insufficient;
- required acceptance semantics are absent;
- test-count expectations conflict materially;
- predecessor #234 is not Closed / Done.

STOP during implementation if:
- production/package/schema/transport changes exceed authority;
- WP09/WP08 must be altered;
- a new seam/helper is required but unauthorized.

STOP before lifecycle mutation if:
- any acceptance/regression/security/residue gate fails;
- Project item identity is ambiguous.

---

# Terminal markers

Success:

`RELEASE 1.9 WP10 IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP10 IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless WP10 is technically accepted and #235 is authoritatively Closed / Done.

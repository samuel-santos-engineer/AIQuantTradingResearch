# Release 1.9 — WP11-Specific Execution Authority

## Recommended model
Use **GPT-5.6 Terra**.

Use GPT-5.6 Luna only if this execution pass proves that a material WP11 semantic, scenario, path, count, release-acceptance, or lifecycle contract is missing. Do not preemptively invent one.

---

# Sole authority

This is the **fresh WP11-specific execution, validation, and completion authority** for Release 1.9.

Canonical work package:

`WP11 — Full Integration and Acceptance — GitHub issue #236`

WP10 / #235 is completed and frozen.
WP12+ and any release-finalization package must remain unstarted unless canonical artifacts explicitly identify WP11 itself as the final release package and authorize those actions.

This authority begins with read-only discovery. It MUST NOT infer WP11 acceptance semantics merely from the title “Full Integration and Acceptance.”

---

# Accepted predecessor boundary

Treat as accepted unless current repository/GitHub read-back contradicts it.

## Lifecycle
- #233 / WP08: Closed / Done.
- #234 / WP09: Closed / Done.
- #235 / WP10: Closed / Done.
- #236 / WP11: expected Open / Backlog.
- milestone #58: expected Open.
- latest accepted milestone state: 2 open / 11 closed.

## Technical baseline entering WP11
- build: 0 warnings / 0 errors.
- .NET:
  - Domain 11/11
  - Application 125/125
  - Infrastructure 182/182
  - Architecture 21/21
  - aggregate **339/339**
- governed Python: **17/17**
- Streamlit: **1.61.1**
- `pip check`: clean.
- WP08 lifecycle predecessor: 18/18.
- WP09 permanent integration/no-bypass coverage accepted.
- WP10 executable-test delta: +0 .NET / +0 Python.
- WP10 documentation alignment accepted.

These are predecessor facts, not permission to assume WP11 adds zero tests.

---

# Primary objective

Discover the exact canonical WP11 contract from:

1. issue #236;
2. Release 1.9 definition;
3. Release 1.9 execution plan;
4. work-package manifest;
5. dependency/path authority;
6. any WP11-specific artifact;
7. release-level acceptance/definition-of-done artifacts;
8. accepted WP08–WP10 predecessor boundaries.

Then choose exactly one path:

## Path A — authority sufficient
Implement/execute exactly the WP11 full-integration and acceptance package, run every required release-level gate, preserve predecessors, and complete #236 lifecycle only after acceptance.

## Path B — authority insufficient
STOP before implementation mutation and identify the minimum narrow **GPT-5.6 Luna** authority required.

Do not invent final-integration scenarios, release gates, expected counts, or release closure semantics.

---

# Phase 0 — Read-only Git/repository state

Verify:

- current branch;
- HEAD;
- `origin/main`;
- ahead/behind;
- staged files;
- unstaged files;
- untracked files.

Classify current worktree changes as:
- accepted predecessor work;
- unrelated user work;
- potential WP11 work.

Do not clean/reset/stash/revert unrelated work.

Record the exact accepted predecessor commit boundary if canonical artifacts define one.

---

# Phase 1 — Read-only GitHub state

Read:

- #233;
- #234;
- #235;
- #236;
- all remaining Release 1.9 issues if needed to understand sequence;
- milestone #58;
- Project #2 item linked to #236.

Require before WP11 mutation:
- #233 Closed / Done;
- #234 Closed / Done;
- #235 Closed / Done;
- #236 Open / Backlog;
- exactly one canonical Project item for #236.

Record #236:
- exact title;
- full body;
- acceptance criteria;
- milestone;
- Release;
- Priority;
- Area;
- dependencies;
- Project item node ID;
- any linked issue/PR relationships.

No GitHub mutation yet.

---

# Phase 2 — Canonical WP11 artifact discovery

Search/read completely all Release 1.9 artifacts relevant to:

- WP11;
- #236;
- “Full Integration and Acceptance”;
- release acceptance;
- final integration;
- end-to-end validation;
- architecture/security acceptance;
- documentation acceptance;
- residue;
- test-count delta;
- release lifecycle;
- milestone closure;
- next work package.

Read predecessor contracts only where needed to establish inherited gates.

Do not substitute conversation history for repository authority.

---

# Phase 3 — Build the WP11 authority matrix

Before mutation, produce internally:

`WP11 requirement → canonical source → authorized path/action → acceptance evidence`

The matrix MUST answer:

1. Is WP11 implementation work, validation-only work, or both?
2. What exact end-to-end/full-integration scenarios are required?
3. Which production paths, if any, may change?
4. Which .NET test paths may change/create?
5. Which Python test paths may change/create?
6. Which documentation paths may change?
7. Are packages/schema/IPC/persistence changes authorized?
8. What exact architecture/security gates apply?
9. What exact lifecycle/restart/cancellation gates apply?
10. What exact documentation/setup gates apply?
11. What exact residue matrix applies?
12. What exact .NET test delta is expected?
13. What exact Python test delta is expected?
14. What exact post-WP11 totals are expected?
15. Does WP11 authorize closing only #236, or any release/milestone lifecycle?
16. What work package/issue follows WP11?
17. Is milestone #58 closure explicitly authorized here?

If any material answer is absent or contradictory:
- STOP before implementation mutation.

Required blocker form:

`WP11 IMPLEMENTATION BLOCKED BEFORE MUTATION — NARROW <EXACT MISSING CONTRACT> AUTHORITY REQUIRED`

---

# Phase 4 — Authority sufficiency rules

The following are material and MUST NOT be invented:

- final integration scenario topology;
- Ready/WarmUp/Empty/Failed source ownership;
- Worker/Streamlit process topology;
- restart/cancellation sequence;
- publication/readiness semantics;
- exact presentation expectations;
- security/no-bypass assertions;
- executable test counts;
- post-WP11 aggregate counts;
- release/milestone closure semantics.

If the canonical plan says only “run full integration and acceptance” without fixing these, request a Luna contract authority.

---

# Phase 5 — Entry baseline validation

If authority is sufficient, run the exact predecessor entry gates it requires.

Default accepted reference baseline:

## .NET
- Domain 11
- Application 125
- Infrastructure 182
- Architecture 21
- total 339

## Python
- total 17

## Other
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check` clean.

Run WP08/WP09 focused suites where WP11 contract requires them.

If predecessor failure occurs:
- classify factual environment vs repository regression;
- do not modify frozen predecessor semantics under WP11 unless explicitly authorized.

---

# Phase 6 — Exact mutation boundary

If WP11 authorizes implementation changes:

- modify only exact paths named by canonical WP11 authority;
- smallest coherent change;
- no opportunistic refactoring;
- no dependency upgrades unless authorized;
- no schema/transport expansion unless authorized;
- no WP08–WP10 semantic rewrite;
- no WP12+ work.

Every changed path must map to the authority matrix.

If WP11 is validation-only, repository implementation mutations must remain zero.

---

# Phase 7 — Full-integration scenarios

Execute/implement exactly the scenarios defined by canonical WP11 authority.

For every scenario prove as applicable:

- deterministic governed source;
- application/pipeline execution;
- visualization read-model state;
- publication identity;
- canonical handoff;
- WP05 parse;
- WP06 frame;
- WP07 presentation projection;
- Streamlit-facing state;
- Worker lifecycle;
- restart/cancellation;
- cleanup;
- no provider/SQLite bypass.

Do not add scenarios not required by the contract.

Do not weaken permanent WP09 scenario ownership.

---

# Phase 8 — Executable test-count gate

Before regression, determine exact WP11 additions.

Current predecessor counts:

- .NET: 339
- Python: 17

Require canonical authority to state exact WP11 delta or unambiguously state validation-only/+0.

Calculate:

`expected .NET post-WP11 = 339 + authorized .NET delta`

`expected Python post-WP11 = 17 + authorized Python delta`

If count delta is ambiguous:
- STOP;
- request Luna reconciliation authority.

No unexplained count drift.

---

# Phase 9 — Focused WP11 validation

Run focused WP11 gates first.

Require:
- every WP11 scenario passes;
- exact expected focused test count;
- no unexplained skips;
- build 0/0 if build is relevant.

If focused failure reveals missing production semantics outside authority:
- STOP;
- do not broaden scope.

---

# Phase 10 — WP10 preservation

Verify WP10 documentation remains truthful after WP11 work.

At minimum ensure:
- simulated/replay warning remains accurate;
- JSON-handoff architecture remains accurate;
- lifecycle/security guidance remains accurate;
- signing guidance remains dev-only/opt-in;
- branch/PR workflow remains accurate.

Do not edit WP10 docs unless WP11 explicitly authorizes them.

---

# Phase 11 — WP09 preservation

Run/preserve permanent WP09 integration and architecture/no-bypass gates.

Require:
- Ready/WarmUp Replay-origin semantics preserved;
- Empty/Failed historical-composition semantics preserved;
- no presentation provider/SQLite bypass;
- Worker producer / Streamlit consumer boundary preserved;
- Release 1.8 JSON-over-stdio boundary remains separate.

No WP09 test weakening.

---

# Phase 12 — WP08 preservation

Run WP08 focused lifecycle suite when required.

Accepted reference:
- **18/18**

Preserve:
- graceful CTRL_BREAK cancellation;
- restart/readiness semantics;
- bounded lifecycle behavior;
- process/listener cleanup.

Do not reopen WP08 implementation.

---

# Phase 13 — Full .NET regression

Run all governed .NET suites.

Require exact post-WP11 counts derived from authorized delta.

Predecessor distribution:
- Domain 11
- Application 125
- Infrastructure 182
- Architecture 21

Report exact post-WP11 per-project and aggregate counts.

Require:
- 0 failures;
- no unexplained skipped tests;
- build 0 warnings / 0 errors.

---

# Phase 14 — Full Python regression

Run all governed Python suites.

Predecessor aggregate:
- **17/17**

Add only authorized WP11 Python delta.

Also require:
- compile/import gates if canonical;
- Streamlit 1.61.1;
- `pip check` clean.

No network/provider dependency unless explicitly authorized.

---

# Phase 15 — Architecture/security acceptance

Run all canonical release-level architecture/security gates.

At minimum preserve accepted no-bypass guarantees:

- no presentation direct provider access;
- no presentation direct SQLite access;
- no unauthorized Infrastructure dependency;
- canonical JSON handoff;
- Worker producer;
- Streamlit consumer;
- no Release 1.8 endpoint expansion;
- no acceptance-only helper promoted to production transport.

Add WP11-specific security gates only if explicitly defined.

---

# Phase 16 — Documentation/setup acceptance

If canonical WP11 includes release-level documentation acceptance, verify:

- README current;
- interoperability doc current;
- Python developer guide current;
- roadmap current;
- simulated-data warning present;
- local signing guidance accurate;
- documented links resolve;
- documented commands remain valid.

Do not mutate docs unless authorized.

---

# Phase 17 — Full residue matrix

After all focused/full runs, verify zero forbidden owned residue as required by canonical WP11 contract.

At minimum inspect as applicable:

- Worker processes;
- testhost processes;
- Python processes;
- Streamlit processes;
- owned listeners;
- canonical/test handoff files;
- temp siblings;
- SQLite DBs and sidecars;
- harness runtime roots;
- probe evidence;
- stale test/demo resources.

Only clean resources factually owned by WP11/test harness.

No broad process kills.

Report retained standard test-result artifacts separately.

---

# Phase 18 — Release-level acceptance matrix

Before GitHub mutation, produce:

`Release 1.9 / WP11 acceptance criterion → implementation/evidence → focused result → regression result → security/docs/residue result`

Every row must PASS.

If WP11 is the release-level acceptance package, this matrix must include all inherited gates explicitly required by canonical authority.

No “implicitly covered” rows.

---

# Phase 19 — Scope audit

Produce:

`changed path/action → WP11 authority source → purpose → proof`

Require:
- only authorized paths/actions;
- no hidden package/schema/IPC changes;
- no WP08–WP10 mutation unless explicitly authorized;
- no WP12+ work;
- no unrelated cleanup.

Preserve user/pre-existing work.

---

# Phase 20 — #236 Project item identity

Only after technical acceptance:

Identify exactly one canonical Project #2 item linked to #236.

Record:
- item node ID;
- Status;
- Release;
- Priority;
- Area.

Expected pre-lifecycle:
- Backlog;
- Release 1.9;
- metadata per #236.

Resolve exact Status field ID and Done option ID.

If ambiguous:
- BLOCK;
- GitHub mutations zero.

---

# Phase 21 — WP11 GitHub lifecycle

Only after every WP11 acceptance gate passes:

1. set #236 Project Status → Done;
2. read back;
3. verify Release/Priority/Area unchanged;
4. close #236;
5. read back;
6. verify #235 remains Closed / Done;
7. verify #234 remains Closed / Done;
8. verify #233 remains Closed / Done;
9. read milestone #58 state/counts;
10. identify the next canonical Release 1.9 work package/issue;
11. do not start it.

No Project item creation/deletion.

---

# Milestone/release closure prohibition

**Do not close milestone #58 merely because WP11 succeeds.**

Milestone closure, release tagging, release publication, branch merge, or final Release 1.9 declaration is authorized only if the canonical WP11/release artifacts explicitly make those actions part of #236 and provide exact acceptance/lifecycle authority.

If they do not:
- leave milestone open;
- report next eligible release work.

---

# Phase 22 — Final read-back

Verify:

- #236 Closed / Done;
- #235 Closed / Done;
- #234 Closed / Done;
- #233 Closed / Done;
- milestone #58 state/counts;
- next package untouched;
- repository/Git state expected;
- no WP11-owned residue.

---

# Completion gate

WP11 completes only if:

1. exact #236 scope is known;
2. canonical authority is sufficient;
3. all authorized WP11 implementation/validation is complete;
4. exact focused gates pass;
5. exact test delta is proven;
6. full .NET matches exact expected post-WP11 total;
7. full Python matches exact expected post-WP11 total;
8. build 0/0;
9. WP08–WP10 required preservation gates pass;
10. architecture/security passes;
11. documentation/setup acceptance passes where required;
12. residue clean;
13. scope audit clean;
14. release/WP11 acceptance matrix fully passes;
15. #236 Project Status = Done;
16. #236 issue = Closed;
17. #233–#235 preserved;
18. WP12+ unstarted.

Milestone #58 closure is NOT a WP11 completion requirement unless explicitly authorized by canonical release authority.

---

# GitHub mutation boundary

Before technical acceptance:

`WP11 GITHUB MUTATIONS: ZERO`

Successful WP11 lifecycle may perform only:

`#236 PROJECT STATUS → DONE`
`#236 ISSUE → CLOSED`

unless canonical #236 explicitly authorizes additional release-level lifecycle changes.

All other GitHub mutations zero.

---

# Required blocker report

If blocked before mutation, report:

## Verified entry state
Git/GitHub/predecessors.

## Canonical WP11 sources read
Exact artifacts.

## Authority matrix gaps
Exact missing semantics/paths/counts/gates.

## Minimum next authority
Name one narrow Luna authority.

## Mutation statements
`WP11 REPOSITORY MUTATIONS: ZERO`
`WP11 GITHUB MUTATIONS: ZERO`

Then:

`RELEASE 1.9 WP11 IMPLEMENTATION AND COMPLETION BLOCKED`

---

# Required completion report

## Binding WP11 authority
Exact sources.

## Entry state
Git/GitHub/milestone/predecessor.

## WP11 scope
Exact acceptance package.

## Implementation
Exact changed paths, or zero if validation-only.

## Test delta
Exact .NET/Python additions.

## Focused acceptance
Exact results.

## Predecessor preservation
WP08/WP09/WP10.

## Full regression
Exact .NET/Python totals.

## Architecture/security
Exact results.

## Documentation/setup
Exact results.

## Residue
Exact final matrix.

## Scope audit
Authorized changes only.

## Release/WP11 acceptance matrix
Every row.

## GitHub
#236 item ID, Done, Closed, read-back.

## Milestone
State/counts and whether closure is separately authorized.

## Next eligible work
Name/issue only; do not start.

---

# Stop conditions

STOP before mutation if:
- #236 or canonical WP11 scope is materially ambiguous;
- final integration scenarios are undefined;
- path authority is insufficient;
- exact executable-test delta is missing where additions are expected;
- release-level acceptance criteria are incomplete;
- milestone/release closure semantics are ambiguous.

STOP during implementation if:
- production/package/schema/transport change exceeds authority;
- frozen predecessor semantics must change;
- a new helper/seam is required without authority.

STOP before GitHub mutation if:
- any focused/regression/security/docs/residue gate fails;
- exact counts cannot be proven;
- Project item identity is ambiguous.

Do not broaden scope.

---

# Terminal markers

Success:

`RELEASE 1.9 WP11 IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP11 IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless #236 is technically accepted and authoritatively Closed / Done.

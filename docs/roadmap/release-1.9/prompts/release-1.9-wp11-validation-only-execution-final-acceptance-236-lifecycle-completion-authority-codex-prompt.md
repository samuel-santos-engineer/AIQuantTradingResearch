# Release 1.9 — WP11 Validation-Only Execution + Final Acceptance + #236 Lifecycle Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
Execute and complete:

`WP11 — Full Integration and Acceptance — #236`

using the binding contract:

`docs/roadmap/release-1.9/RELEASE_1.9_WP11_FULL_INTEGRATION_RELEASE_ACCEPTANCE_CONTRACT_AUTHORITY.md`

WP11 is **validation-only**.

Repository implementation mutation is forbidden.
Executable test delta is fixed at:
- .NET: **+0**
- Python: **+0**

Required final executable totals remain:
- .NET: **339/339**
- Python: **17/17**

Canonical SQLite persistence schema is **v4** and must remain unchanged.

Successful GitHub lifecycle authority is limited to:
1. #236 Project Status → Done
2. #236 issue → Closed

#237 and milestone #58 must remain untouched.

---

# 1. Precedence

Use, in order:

1. binding WP11 full-integration/release-acceptance contract;
2. reconciled Release 1.9 definition;
3. reconciled execution plan;
4. reconciled file manifest;
5. accepted WP03 schema-v4 authority;
6. accepted WP08 lifecycle contracts/tests;
7. accepted WP09 permanent integration/architecture contract/tests;
8. accepted WP10 documentation contract/current aligned docs;
9. issue #236;
10. #237/WP12 boundary.

Do not re-open settled semantics.

---

# 2. Entry-state read-back

Before execution, verify read-only:

## Git
- current branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged/unstaged/untracked paths.

Preserve every pre-existing user/predecessor change.
Do not reset/stash/clean/revert.

## GitHub
Verify:
- #233 Closed / Done;
- #234 Closed / Done;
- #235 Closed / Done;
- #236 Open / Backlog;
- exactly one canonical Project #2 item for #236;
- expected item from prior evidence:
  `PVTI_lAHOCAzBgs4BfsiAzg33jXQ`;
- Release 1.9 / P1 / Testing preserved;
- #237 Open / Backlog;
- milestone #58 Open.

Record milestone counts, but do not mutate milestone.

If #236 item identity is ambiguous, STOP.

---

# 3. Repository mutation prohibition

WP11 is validation-only.

Allowed:
- build;
- run existing tests;
- run existing Python suites;
- read/inspect files;
- validate docs/links/commands;
- inspect process/listener/file/database residue;
- create normal test-result evidence outside source mutation where existing tooling does so.

Forbidden:
- source edits;
- test edits;
- Python edits;
- documentation edits;
- project/package edits;
- schema/migration edits;
- signing edits;
- generated source committed to repository;
- WP12 work.

If a failing acceptance gate requires repository change:
STOP and classify the predecessor regression.
Do not fix it under this authority.

---

# 4. Binding acceptance matrix

Read the WP11 contract completely and execute every row exactly.

At minimum this includes:

- FI-READY
- FI-WARMUP
- FI-EMPTY
- FI-FAILED
- FI-LIFECYCLE
- FI-SCHEMA
- FI-ARCH
- FI-SECURITY
- FI-DOCS
- FI-BUILD
- FI-DOTNET
- FI-PYTHON
- FI-RESIDUE
- FI-SCOPE

Use the exact permanent tests/checks/counts named by the contract.

Do not substitute a different test simply because it is convenient.

---

# 5. Deterministic integration scenarios

Execute the contract's existing permanent proof for all four states.

## READY
Preserve accepted Replay-origin ownership.

Require:
- deterministic source;
- canonical Ready state;
- governed read-model/publisher/handoff chain;
- WP05 parse;
- WP06 frame;
- WP07 projection;
- same-publication identity where contract requires it;
- no bypass.

## WARMUP
Same accepted Replay-origin ownership and proof.

## EMPTY
Preserve WP09 historical-composition ownership.

Do not require Replay to publish Empty.

## FAILED
Preserve WP09 historical-composition ownership.

Do not require replay-source failure to publish Failed.

All exact permanent scenario tests required by the contract must pass.

---

# 6. WP08 lifecycle acceptance

Run the exact WP08 lifecycle gate named by the contract.

Expected accepted focused baseline:
**18/18**, if that is the binding contract count.

Require preservation of:
- Worker/Streamlit ownership;
- genuine readiness;
- targeted CTRL_BREAK;
- exit 0;
- Worker A → Worker B restart;
- stale handoff rejection;
- bounded cleanup;
- no forced kill on passing path.

Do not modify WP08.

---

# 7. Schema-v4 acceptance

Execute exact schema-v4 proof from the contract.

Require:
- `CurrentVersion = 4`;
- governed `PRAGMA user_version = 4`;
- accepted v3→v4 migration behavior preserved;
- schema/bootstrap tests pass;
- no schema/table/index/version mutation.

Do not confuse persistence schema version with JSON envelope/version semantics.

---

# 8. Architecture/security acceptance

Execute exact permanent WP09/static gates.

Require:
- no Python/Streamlit direct SQLite access;
- no Python/Streamlit direct provider access;
- no unauthorized presentation → Infrastructure dependency;
- Worker/.NET producer;
- Python/Streamlit consumer;
- canonical JSON file handoff;
- Release 1.8 JSON-over-stdio boundary remains separate;
- WP08 helper/probe remains test-only;
- Smart App Control local signing remains opt-in and development-only;
- no committed private key/secret material.

No new tooling/packages.

---

# 9. WP10 documentation/setup acceptance

Perform only the read-only checks required by the binding contract.

Validate as applicable:
- README simulated/replay warning;
- interoperability architecture;
- Python developer setup;
- Smart App Control signing guidance;
- branch/PR workflow;
- roadmap;
- relative links;
- documented commands.

No doc mutation.

Any material inconsistency is a predecessor regression and blocks WP11.

---

# 10. Focused acceptance

Run every focused command/suite specified by the contract.

Capture:
- command/filter;
- expected count;
- actual count;
- pass/fail;
- skips;
- terminal evidence or governed result artifact.

Every focused gate must PASS before full regression.

No unexplained count deviation.

---

# 11. Build gate

Run the contract's governed build.

Require:
- **0 warnings**
- **0 errors**

If environment policy interferes, diagnose factually.
Do not change repository/security policy under WP11.

---

# 12. Full .NET regression

Run the exact governed full .NET regression.

Required final counts:

- Domain: **11/11**
- Application: **125/125**
- Infrastructure: **182/182**
- Architecture: **21/21**
- Aggregate: **339/339**
- failures: 0
- unexplained skips: 0

WP11 delta:
**+0 .NET**

If runner console output is incomplete but authoritative TRX/result evidence exists, apply only the already accepted evidence methodology; do not invent counts.

---

# 13. Full Python regression

Run the exact governed Python regression.

Required:
- **17/17**
- failures: 0
- unexplained skips: 0
- Streamlit **1.61.1**
- `pip check`: clean

WP11 delta:
**+0 Python**

---

# 14. Residue acceptance

After focused and full runs, execute the exact residue matrix.

At minimum inspect contract-owned/harness-owned:

## Processes
Zero owned:
- Worker;
- testhost;
- Python;
- Streamlit;
- probe/helper processes.

## Listeners
Zero owned listener residue.

## Runtime/temp
Zero forbidden WP11/test-harness runtime roots.

## Handoff
No forbidden acceptance-owned handoff or atomic temp sibling residue.

## SQLite
No forbidden test-owned DB/WAL/SHM/journal residue according to the permanent cleanup contract.

Only remove residue after proving factual ownership.
No broad process kills or global temp cleanup.

Standard TRX/test-result evidence may remain.

---

# 15. Scope audit

Before GitHub mutation, prove:

`repository mutation by WP11 = ZERO`

Audit:
- Git status before vs after;
- any generated files;
- any timestamps/content changes attributable to validation;
- no test/source/docs/package/schema changes;
- no WP12 work.

Pre-existing dirty worktree content must remain preserved.

---

# 16. Final acceptance matrix

Produce a complete table:

`acceptance row → required proof → actual result → PASS/BLOCK`

Every binding row must be PASS:
- Ready
- WarmUp
- Empty
- Failed
- lifecycle
- schema v4
- architecture
- security
- docs/setup
- build
- .NET
- Python
- residue
- scope

No GitHub lifecycle mutation until all rows PASS.

---

# 17. #236 Project identity

After technical acceptance only:

Read Project #2.

Require exactly one #236 item.

Confirm:
- node ID;
- Status = Backlog before transition;
- Release = 1.9;
- Priority = P1;
- Area = Testing.

Resolve exact Status field and Done option.

If any identity/metadata ambiguity exists:
STOP with GitHub mutations zero.

---

# 18. GitHub lifecycle completion

Only after all technical acceptance passes:

## Mutation 1
Set #236 Project Status → Done.

Immediately read back and require:
- Status Done;
- Release 1.9 unchanged;
- P1 unchanged;
- Testing unchanged;
- same item node ID.

## Mutation 2
Close #236.

If already closed as an automatic side effect of Project automation:
- treat explicit close as idempotent/no-op;
- do not create extra mutation.

Read back and require:
- #236 Closed;
- Project item Done.

---

# 19. Frozen lifecycle boundaries

After #236 completion verify:

- #233 remains Closed / Done;
- #234 remains Closed / Done;
- #235 remains Closed / Done;
- #237 remains Open / Backlog;
- milestone #58 remains Open.

Do NOT:
- change #237;
- close milestone #58;
- create/delete Project items;
- create PR;
- merge PR;
- tag release;
- publish release;
- perform branch cleanup.

WP12 owns closure/PR readiness.

---

# 20. Final Git/read-back

Record:
- final Git status;
- branch/HEAD;
- ahead/behind;
- repository mutations attributable to WP11: zero;
- final #236 issue/project state;
- final #237 state;
- milestone #58 state/counts;
- final residue.

Do not require a clean worktree if it was dirty on entry.
Require preservation, not cleanup.

---

# Stop conditions

STOP before GitHub mutation if any of these occur:

- any binding acceptance row fails;
- focused test count differs;
- .NET is not exactly 339/339;
- Python is not exactly 17/17;
- build is not 0 warnings / 0 errors;
- schema-v4 proof fails;
- architecture/security gate fails;
- docs/setup gate fails;
- forbidden residue remains;
- WP11 caused repository mutation;
- #236 Project item is ambiguous.

STOP immediately if a fix would require modifying:
- production;
- tests;
- Python;
- docs;
- schema;
- migrations;
- packages;
- signing configuration.

Report the narrow follow-up authority required.

---

# Required blocked report

Include:

## Entry state
Git/GitHub.

## Acceptance completed
Exact passing rows.

## Blocker
Exact failing row/evidence.

## Mutation audit
Repository mutations zero.
GitHub mutations zero.

Then:

`WP11 VALIDATION-ONLY GITHUB MUTATIONS: ZERO`

`RELEASE 1.9 WP11 VALIDATION-ONLY EXECUTION AND FINAL ACCEPTANCE BLOCKED`

---

# Required success report

Include:

## Entry state
Git/GitHub.

## Contract
Exact binding WP11 contract.

## Focused acceptance
Exact commands/counts.

## Four scenarios
Ready/WarmUp/Empty/Failed proof.

## Lifecycle
Exact WP08 result.

## Schema
Exact v4 proof.

## Architecture/security
Exact results.

## Docs/setup
Exact results.

## Build
0 warnings / 0 errors.

## Full .NET
11/11, 125/125, 182/182, 21/21, total 339/339.

## Python
17/17; Streamlit 1.61.1; pip check clean.

## Test delta
+0 .NET / +0 Python.

## Residue
Exact final matrix.

## Scope
Repository mutation zero.

## GitHub
#236 item ID; Done; #236 Closed.

## Preservation
#233/#234/#235 unchanged Closed/Done.
#237 Open/Backlog.
milestone #58 Open.

## Next eligible work
WP12 / #237 only; do not start.

Required mutation markers:

`WP11 VALIDATION-ONLY REPOSITORY MUTATIONS: ZERO`

`WP11 LIFECYCLE GITHUB MUTATIONS: #236 PROJECT STATUS → DONE; #236 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

---

# Terminal marker

Success:

`RELEASE 1.9 WP11 VALIDATION-ONLY EXECUTION, FINAL ACCEPTANCE, AND #236 LIFECYCLE COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP11 VALIDATION-ONLY EXECUTION AND FINAL ACCEPTANCE BLOCKED`

Never emit COMPLETE unless every acceptance row passes and #236 is authoritatively Done / Closed.

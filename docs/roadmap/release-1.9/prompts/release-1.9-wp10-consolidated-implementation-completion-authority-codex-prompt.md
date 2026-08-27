# Release 1.9 — WP10 Consolidated Implementation + Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is the **fresh consolidated implementation, validation, and lifecycle-completion authority** for Release 1.9 WP10, canonical issue **#235**.

The binding WP10 contract is:

`docs/roadmap/release-1.9/RELEASE_1.9_WP10_ARCHITECTURE_DOCUMENTATION_DEVELOPER_ALIGNMENT_CONTRACT_PATH_ACCEPTANCE_TEST_COUNT_AUTHORITY.md`

That artifact is the controlling semantic/path/acceptance authority for WP10.

WP09 / #234 is complete and frozen.
WP11+ must remain unstarted.

---

# Accepted predecessor state

Treat as binding unless current read-back contradicts it.

## WP09
- #234 Closed / Done.
- full .NET baseline: **339/339**
  - Domain 11
  - Application 125
  - Infrastructure 182
  - Architecture 21
- governed Python baseline: **17/17**
- WP08 focused: 18/18.
- build: 0 warnings / 0 errors.
- Streamlit: 1.61.1.
- `pip check`: clean.
- WP09 architecture/no-bypass and permanent integration accepted.

## WP10
- #235 Open / Backlog.
- unique Project #2 item expected:
  `PVTI_lAHOCAzBgs4BfsiAzg33Xh8`.
- Release 1.9.
- Priority P1.
- Area Documentation.
- milestone #58 Open.

## WP10 executable-test contract
- .NET delta: **+0**
- Python delta: **+0**
- total test delta: **+0**
- expected post-WP10 .NET: **339/339**
- expected post-WP10 Python: **17/17**

Any new executable test is out of scope.

---

# Authority precedence

For WP10:

1. `RELEASE_1.9_WP10_ARCHITECTURE_DOCUMENTATION_DEVELOPER_ALIGNMENT_CONTRACT_PATH_ACCEPTANCE_TEST_COUNT_AUTHORITY.md`
2. canonical Release 1.9 definition/plan/manifest
3. accepted WP05–WP09 predecessor contracts/evidence
4. issue #235

If there is a material conflict:
- STOP before mutation;
- report exact contradiction.

Do not improvise documentation semantics.

---

# Exact writable paths

WP10 may modify only:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
3. `README.md`
4. `docs/project/ROADMAP.md`

No other repository path may change under this authority.

Existing pre-existing modifications in these paths must be reconciled carefully and preserved if they are unrelated/accepted.

No source/test/Python/package/schema/signing changes.

---

# Primary objective

Implement exactly the documentation alignment defined by the binding WP10 contract:

- architecture;
- developer setup;
- simulated-data warning;
- lifecycle;
- security;
- troubleshooting;
- branch/PR workflow;
- roadmap alignment.

Then prove:
1. documentation content is truthful and consistent;
2. all added/changed links resolve;
3. commands/paths are valid;
4. security guidance is safe and accurate;
5. simulated-data warning is present and unambiguous;
6. +0/+0 executable-test contract preserved;
7. 339/339 .NET and 17/17 Python remain green;
8. no WP10 residue;
9. #235 becomes Done/Closed only after all acceptance gates pass.

---

# Phase 0 — Read-only entry verification

Verify:

## Git
- branch;
- HEAD;
- `origin/main`;
- ahead/behind;
- staged;
- unstaged;
- untracked.

Preserve unrelated pre-existing worktree changes.

## GitHub
Read:
- #233;
- #234;
- #235;
- Project #2 item for #235;
- milestone #58.

Require:
- #233 Closed / Done;
- #234 Closed / Done;
- #235 Open / Backlog;
- exactly one canonical #235 item.

Record:
- item ID;
- Release;
- Priority;
- Area;
- milestone state/counts.

No mutation yet.

---

# Phase 1 — Read binding contract + current docs

Read completely:

- WP10 binding contract;
- all four authorized docs;
- Smart App Control local-signing guide;
- current Release 1.9 roadmap/planning docs referenced by WP10;
- current branch/PR workflow docs if any.

Before mutation produce an internal map:

`document → required edits → forbidden edits → factual source`

Do not rely on conversation summary where repository evidence is available.

---

# Phase 2 — Pre-existing-diff reconciliation

Because existing README/Python-guide modifications were reported as preserved during the Luna definition pass:

- inspect current diffs in all four writable paths;
- classify each hunk as:
  - accepted predecessor/pre-existing work;
  - WP10-required alignment;
  - unrelated/ambiguous.

Preserve accepted predecessor hunks exactly.

Do not overwrite unrelated current work.

If an authorized doc contains ambiguous intersecting changes that cannot be safely reconciled:
- STOP before mutation;
- request a narrow local-diff reconciliation authority.

Do not require a globally clean worktree.

---

# Phase 3 — Update interoperability architecture doc

Modify:

`docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`

Implement exactly the binding contract.

Required current-state content includes:

## Architecture
- .NET Worker/Application side is producer.
- Python/Streamlit side is consumer/presentation.
- canonical JSON handoff/file boundary.
- no direct provider/SQLite access from presentation.
- no production Worker↔Streamlit supervision relationship.
- Release 1.8 JSON-over-stdio endpoint remains separate.

## Data flow
Document accepted flow:

Replay / historical composition
→ Application/pipeline
→ visualization read model
→ file publisher / canonical JSON handoff
→ WP05 parser
→ WP06 frame
→ WP07 sections
→ Streamlit.

Accurately distinguish Replay-origin Ready/WarmUp and canonical historical-composition-origin Empty/Failed if relevant.

## Lifecycle
- independent launch;
- Worker startup canonical-handoff cleanup;
- atomic publication;
- Streamlit refresh/consumer ownership;
- graceful cancellation/restart;
- stale prior payload does not satisfy new readiness.

## Security
Cross-reference WP09 permanent no-bypass rules.

Forbidden:
- acceptance-only WP08 probe as production interface;
- direct provider/database UI claims;
- JSON-over-stdio as Release 1.9 presentation boundary.

---

# Phase 4 — Update Python developer environment guide

Modify:

`docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`

Align exact current setup.

Required:
- governed Python version from repository;
- current package/pin source of truth;
- Streamlit 1.61.1;
- environment verification;
- `pip check`;
- current Python test commands/suites;
- Smart App Control local-signing cross-reference;
- safe troubleshooting.

Security terminology:
use:
`local-development Authenticode signing for Windows Smart App Control compatibility`

Do not use:
`Smart App Control bypass`

Do not duplicate the full signing guide.

Do not change package pins.

---

# Phase 5 — Update root README

Modify:

`README.md`

Keep concise.

Required:

## Current architecture summary
- .NET processing/Worker;
- canonical JSON handoff;
- Python/Streamlit presentation;
- no direct DB/provider bypass.

## Prominent simulated-data warning
Must clearly state:
- current Release 1.9 visualization/demo flows use deterministic simulated/replay data;
- not live market/provider data;
- intended for local testing/demo;
- no implication of live trading suitability.

Place near relevant usage/demo content.

## Developer setup links
Link to:
- interoperability doc;
- Python developer environment;
- Smart App Control local signing guide where appropriate.

## Lifecycle/demo usage
Only commands/behaviors that actually exist.

## Branch/PR workflow
Document truthfully from current repository conventions or link to the canonical workflow doc.

Do not invent CI/branch-protection policy.

---

# Phase 6 — Update ROADMAP

Modify:

`docs/project/ROADMAP.md`

Align Release 1.9 with accepted lifecycle.

At implementation stage:
- WP08 complete;
- WP09 complete;
- WP10 implementation in progress until lifecycle close;
- successor packages untouched.

Document WP10 scope accurately:
- documentation/developer alignment only;
- +0 executable tests.

Do not:
- close milestone #58;
- mark WP11+ started/completed;
- claim #235 closed before lifecycle mutation if document timing makes that false.

If established roadmap style supports post-implementation/pre-lifecycle wording, use it.

---

# Phase 7 — Cross-link validation

Validate every added/changed relative link.

At minimum:
- README → interoperability doc;
- README → Python developer guide;
- Python guide → Smart App Control guide;
- any ROADMAP/planning links added.

No broken links.

Use repository-relative path resolution.

If links cannot be validated, fix documentation only within authorized paths.

---

# Phase 8 — Command/path validation

For every command or path added/changed:

- verify target file/tool/path exists;
- verify command syntax is current and safe;
- do not run destructive commands;
- do not rely on removed tools.

Examples to verify as applicable:
- Python test commands;
- Streamlit version command;
- `pip check`;
- Windows SDK/signing guide path;
- build/test commands.

Do not modify code to make docs true.

If an expected command is false, correct the docs.

---

# Phase 9 — Content truthfulness audit

Cross-check changed claims against:
- current implementation;
- WP05–WP09 accepted contracts;
- current GitHub lifecycle;
- repository files.

Audit:
- architecture;
- lifecycle;
- security;
- simulated data;
- setup;
- troubleshooting;
- branch/PR workflow;
- roadmap state.

No historical claim may be presented as current state without qualification.

---

# Phase 10 — Security audit

Require:
- no private keys;
- no PFX;
- no passwords;
- no machine-specific secret material;
- no unsafe recommendation to disable Smart App Control as default;
- no production-trust claim for local self-signed cert;
- no provider/SQLite bypass guidance.

If such text exists in changed content, correct it.

---

# Phase 11 — Simulated-data warning gate

Verify README contains a clear warning semantically equivalent to:

“Current Release 1.9 visualization/demo flows use deterministic simulated/replay data for local demonstration and testing. They are not a live market-data feed.”

Do not require exact wording, but all semantic components must be present.

No live-data ambiguity.

---

# Phase 12 — Branch/PR workflow gate

Verify documentation reflects actual repository workflow only.

Check:
- current branch conventions;
- PR expectations;
- validation expectations;
- whether direct main work is documented/allowed/discouraged.

Do not invent:
- protected branch rules;
- required checks not present;
- CI automation not present.

Prefer concise cross-reference if canonical workflow already exists.

---

# Phase 13 — Executable-test count gate

Before running regressions, verify repository test count delta from WP10 is exactly:

- .NET: +0
- Python: +0

No new test files.
No modified test behavior.

If any executable test was added/changed under WP10:
- STOP;
- authority violation.

---

# Phase 14 — Build gate

Run build.

Require:
- 0 warnings;
- 0 errors.

WP10 is documentation-only; build must remain unchanged.

---

# Phase 15 — .NET regression

Run full governed .NET regression.

Expected:
- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- aggregate **339/339**

Require 0 failures.

No unexplained count change.

---

# Phase 16 — Python regression

Run full governed Python regression.

Expected:
- **17/17**

Also require:
- Streamlit 1.61.1
- `pip check` clean.

No new Python tests.

---

# Phase 17 — Documentation acceptance matrix

Produce:

`WP10 requirement → document/path → implemented text/change → factual source → validation result`

Include:
- architecture;
- setup;
- simulated-data warning;
- lifecycle;
- security;
- troubleshooting;
- branch/PR workflow;
- roadmap;
- links;
- commands;
- +0/+0 test contract.

Every row PASS.

---

# Phase 18 — Residue

Verify WP10 documentation work leaves no owned runtime residue.

No owned:
- Worker;
- Streamlit;
- Python;
- testhost;
- listener;
- handoff;
- temp DB/sidecars;
- runtime root.

Standard test-result artifacts may remain in normal result directories.

Do not broad-kill processes.

---

# Phase 19 — Scope audit

Changed paths must be a subset of exactly:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
3. `README.md`
4. `docs/project/ROADMAP.md`

No other WP10 implementation mutations.

Distinguish:
- pre-existing hunks;
- WP10 new hunks.

Prove zero:
- source;
- tests;
- Python code;
- package;
- schema;
- signing implementation;
- WP08/WP09;
- WP11+.

---

# Phase 20 — #235 Project identity

Only after all documentation/regression gates pass:

Identify exactly one canonical Project #2 item linked to #235.

Expected:
- item ID `PVTI_lAHOCAzBgs4BfsiAzg33Xh8` unless read-back changed;
- Backlog;
- Release 1.9;
- Priority P1;
- Area Documentation.

Resolve Status field and Done option.

If ambiguous:
- BLOCK;
- GitHub mutations zero.

---

# Phase 21 — GitHub lifecycle completion

Only after all acceptance gates pass:

1. set #235 Project Status → Done;
2. read back;
3. verify Release/Priority/Area unchanged;
4. close #235;
5. read back;
6. verify #234 remains Closed / Done;
7. verify #233 remains Closed / Done;
8. read milestone #58 state/counts;
9. identify next eligible Release 1.9 work package;
10. do not start it.

No item creation/deletion.

Do not close milestone #58 without separate release-completion authority.

---

# Phase 22 — Final read-back

Verify:
- #235 Closed / Done;
- #234 Closed / Done;
- #233 Closed / Done;
- milestone consistent;
- next WP untouched;
- repository state expected;
- no WP10-owned residue.

---

# Completion gate

WP10 completes only if:

1. only authorized documentation paths changed;
2. all required content implemented;
3. all forbidden claims absent;
4. links valid;
5. commands/paths valid;
6. simulated-data warning passes;
7. architecture/lifecycle/security consistency passes;
8. branch/PR workflow is truthful;
9. test delta = +0/+0;
10. build 0/0;
11. .NET 339/339;
12. Python 17/17;
13. Streamlit 1.61.1;
14. `pip check` clean;
15. residue clean;
16. #235 Project Done;
17. #235 Closed;
18. #233/#234 preserved;
19. WP11+ unstarted.

---

# GitHub mutation boundary

Before lifecycle completion:

`WP10 GITHUB MUTATIONS: ZERO`

Successful lifecycle may perform only:

`#235 PROJECT STATUS → DONE`
`#235 ISSUE → CLOSED`

All other GitHub mutations zero.

---

# Required completion report

## Binding authority
Exact contract path.

## Entry state
Git/GitHub/milestone/predecessor.

## Reconciliation
Pre-existing doc hunks vs WP10 hunks.

## Documentation changes
Exact paths and sections.

## Simulated-data warning
Final semantic wording/location.

## Architecture/lifecycle/security
Final aligned claims.

## Setup/troubleshooting
Final aligned guidance.

## Branch/PR workflow
Truthful source/summary.

## Link/command validation
Results.

## Test-count
+0 .NET / +0 Python.

## Regression
339/339 .NET; 17/17 Python; build 0/0; Streamlit/pip.

## Residue
Final state.

## Scope audit
Only four authorized docs.

## #235 acceptance matrix
All rows.

## GitHub
Project item ID, Done, Closed, read-back.

## Milestone
State/counts.

## Next eligible WP
Name/issue only.

---

# Stop conditions

STOP before mutation if:
- binding contract is missing/contradictory;
- intersecting pre-existing doc diffs cannot be safely reconciled.

STOP during implementation if:
- a required current-state claim is false and fixing it needs code changes;
- another document path is required;
- executable tests appear necessary;
- test delta would not remain +0/+0.

STOP before GitHub mutation if:
- any doc acceptance/link/security/regression/residue gate fails;
- Project item identity ambiguous.

Do not broaden scope.

---

# Terminal markers

Success:

`RELEASE 1.9 WP10 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP10 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless documentation acceptance, unchanged technical baselines, and #235 Closed / Done are all authoritatively proven.

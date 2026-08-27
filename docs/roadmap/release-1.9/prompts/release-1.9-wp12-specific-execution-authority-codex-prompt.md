# Release 1.9 — WP12-Specific Execution Authority

## Recommended model
Use **GPT-5.6 Terra**.

Use **GPT-5.6 Luna** only if read-only discovery proves that a material WP12 closure/PR-readiness contract is absent, contradictory, or insufficient. Do not invent release lifecycle semantics.

---

# Sole authority

This is the fresh execution authority for:

`WP12 — GitHub issue #237`

WP11/#236 is complete and frozen as the final technical-acceptance predecessor.

WP12 owns only the closure / PR-readiness / final Release 1.9 lifecycle work actually assigned to it by canonical repository and GitHub authority.

This prompt does **not** itself authorize:
- committing;
- pushing;
- creating a branch;
- creating/updating/merging a PR;
- closing milestone #58;
- tagging a release;
- publishing a release;
- deleting branches;
- changing Project metadata;
- modifying repository files.

Those actions become authorized only if canonical WP12/#237 artifacts explicitly define them and all required gates pass.

---

# Accepted predecessor boundary

Treat as binding unless current read-back contradicts it.

## Technical
- build: 0 warnings / 0 errors.
- Domain: 11/11.
- Application: 125/125.
- Infrastructure: 182/182.
- Architecture: 21/21.
- aggregate .NET: **339/339**.
- governed Python: **17/17**.
- Streamlit: **1.61.1**.
- `pip check`: clean.
- WP08 lifecycle: **18/18**.
- WP09 permanent Ready/WarmUp/Empty/Failed integration: accepted.
- WP11 schema-v4 acceptance: accepted.
- WP11 architecture/security/docs/residue acceptance: accepted.
- WP11 repository mutations: zero.

## GitHub
- #233 Closed / Done.
- #234 Closed / Done.
- #235 Closed / Done.
- #236 Closed / Done.
- #237 expected Open / Backlog.
- milestone #58 expected Open.
- latest accepted milestone count: **1 open / 12 closed**.

## Git
Previously verified:
- branch `main`.
- `main == origin/main`.
- predecessor commit:
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`
- ahead/behind 0/0.
- worktree contains pre-existing accepted predecessor and/or unrelated user changes.
- staged files previously 0.

Do not assume the dirty worktree should be cleaned.

---

# Primary objective

Read canonical WP12/#237 authority and determine exactly what “closure / PR readiness” means for Release 1.9.

Then choose one path.

## Path A — sufficient authority
Execute exactly the authorized WP12 closure/readiness workflow and final lifecycle actions.

## Path B — insufficient authority
STOP before mutation and request the minimum narrow **GPT-5.6 Luna** WP12 contract authority.

No speculative Git/GitHub mutation.

---

# Phase 0 — Git safety snapshot

Before any mutation, record:

- current branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged paths;
- unstaged paths;
- untracked paths;
- ignored local configuration relevant to development signing if necessary.

Classify each dirty path:

1. accepted Release 1.9 implementation/documentation work intended for eventual PR;
2. unrelated user work;
3. local-only/ignored development configuration;
4. generated/test evidence;
5. unknown.

Do not stage, commit, discard, stash, clean, or rewrite anything yet.

If path ownership is ambiguous, preserve it and block any operation that could capture/delete it.

---

# Phase 1 — GitHub read-back

Read:

- #237 completely;
- #236 and its Project item;
- milestone #58;
- Project #2 item for #237;
- remaining open Release 1.9 issues, if any;
- existing Release 1.9 PRs;
- existing branches/PR relationships if canonical tooling exposes them.

Require:
- #233–#236 Closed / Done;
- #237 Open / Backlog;
- exactly one canonical #237 Project item.

Record #237:
- title;
- full body;
- acceptance criteria;
- dependencies;
- milestone;
- Release;
- Priority;
- Area;
- Project item node ID;
- linked PR/branch references;
- closure automation semantics if observable.

No mutation.

---

# Phase 2 — Canonical WP12 artifact discovery

Read completely:

1. `RELEASE_1.9_DEFINITION.md`
2. `RELEASE_1.9_EXECUTION_PLAN.md`
3. `RELEASE_1.9_FILE_MANIFEST.md`
4. WP11 full-integration/release-acceptance contract
5. #237
6. any WP12-specific artifact
7. repository contribution/branch/PR guidance
8. release/closure/definition-of-done guidance
9. roadmap/release workflow documentation
10. any canonical instructions governing milestone closure, PR creation, commit structure, branch naming, or release tagging.

Search for:
- WP12;
- #237;
- closure;
- PR readiness;
- pull request;
- branch;
- commit;
- milestone;
- release;
- tag;
- merge;
- final acceptance;
- release notes;
- changelog;
- definition of done.

Do not infer from common GitHub practice.

---

# Phase 3 — WP12 authority matrix

Before mutation, construct:

`requirement → canonical source → authorized action/path → required evidence`

It MUST answer:

1. Is WP12 validation-only, Git/GitHub lifecycle-only, documentation-changing, or mixed?
2. Does WP12 authorize repository file changes?
3. Does it authorize staging?
4. Does it authorize a commit?
5. Does it require one commit or multiple commits?
6. Does it authorize branch creation? Exact branch name?
7. Does it authorize pushing?
8. Does it authorize PR creation? Exact base/head/title/body?
9. Does it authorize updating an existing PR?
10. Does it authorize PR merge?
11. Does it authorize #237 Project Status → Done?
12. Does it authorize closing #237?
13. Does it authorize milestone #58 closure?
14. Does it authorize a Git tag?
15. Does it authorize a GitHub Release?
16. Does it authorize release notes/changelog mutation?
17. What final technical regression must be re-run?
18. What security/secrets check must pass before staging/push?
19. What residue state is required?
20. What dirty-worktree content is in-scope for the Release 1.9 PR?
21. How must unrelated/local-only work be excluded?
22. What is the expected final state of `main`, feature branch, PR, milestone, and Project?
23. Is PR “readiness” evidence only, or actual PR creation?
24. Does #237 completion precede PR merge, follow PR creation, or follow merge?

If any material answer is missing or contradictory:
STOP before mutation.

Required blocker:

`WP12 IMPLEMENTATION BLOCKED BEFORE MUTATION — NARROW WP12 CLOSURE / PR-READINESS / GIT-GITHUB LIFECYCLE CONTRACT AUTHORITY REQUIRED`

---

# Phase 4 — Frozen technical baseline

WP12 must not reopen accepted WP08–WP11 semantics.

Reference baseline:
- build 0/0;
- .NET 339/339;
- Python 17/17;
- Streamlit 1.61.1;
- `pip check` clean;
- schema v4;
- WP08 18/18;
- WP11 acceptance complete.

Any required re-validation is evidence collection, not permission to change implementation.

If a regression fails and a source fix is required:
STOP and request separate authority.

---

# Phase 5 — Release 1.9 change-set inventory

If canonical WP12 requires PR readiness, inventory the full intended Release 1.9 change set.

Use Git history/status/diff and canonical work-package artifacts.

Produce:

`path → originating WP → intended Release 1.9? → safe to include? → evidence`

Distinguish:
- tracked modified files;
- new untracked files;
- ignored local settings;
- test outputs;
- unrelated user work.

Never include:
- private keys;
- certificates containing private keys;
- local signing secrets;
- local-only props/settings;
- build outputs;
- test outputs;
- temp runtime artifacts;
- unrelated user work.

Do not stage until the inventory is complete and authority explicitly permits staging.

---

# Phase 6 — Security/secrets preflight

Before any staging/commit/push/PR action, run the exact canonical security gate.

At minimum, where existing tooling supports it:

- tracked/non-ignored secret scan;
- inspect signing-related paths;
- verify no private key material;
- verify local Smart App Control configuration remains ignored/local;
- inspect staged diff before commit;
- verify no generated binary/build output is included.

Do not add new security dependencies.

Any secret/private-key finding blocks mutation.

---

# Phase 7 — Final technical readiness

Run exactly the final technical gates required by WP12.

If canonical WP12 inherits WP11 final acceptance without mandatory rerun, state that.

If it requires rerun, expected baseline is:

## Build
0 warnings / 0 errors.

## .NET
- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- aggregate 339/339.

## Python
17/17.

## Environment
- Streamlit 1.61.1
- `pip check` clean.

## Schema
v4 preserved.

No unexplained count drift.

---

# Phase 8 — Final residue gate

Before Git/GitHub lifecycle mutation verify zero forbidden owned residue:

- Worker;
- testhost;
- Python;
- Streamlit;
- listeners;
- harness temp roots;
- handoff temp siblings;
- test-owned SQLite/WAL/SHM/journal residue;
- generated build/test artifacts if canonical PR rules prohibit them.

Only remove factually owned generated residue when explicitly safe.

Do not clean unrelated files.

---

# Phase 9 — Documentation/release-readiness audit

If canonical WP12 requires it, verify:

- README;
- interoperability architecture;
- Python setup;
- roadmap;
- release-specific documentation;
- branch/PR workflow;
- simulated/replay warning;
- schema-v4 wording;
- Smart App Control local-signing guidance;
- links/commands.

If a documentation edit is required but WP12 path authority does not authorize it:
STOP.

---

# Phase 10 — Staging authority

Do not stage anything unless canonical WP12 explicitly authorizes staging.

If authorized:

- stage only Release 1.9 intended paths proven by the inventory;
- exclude unrelated/local-only/generated content;
- inspect staged diff completely;
- re-run secrets check over staged content;
- record exact staged paths.

If a mixed file contains unrelated user changes that cannot safely be separated:
STOP.
Do not stage the whole file.

---

# Phase 11 — Commit authority

Do not commit unless explicitly authorized.

If authorized, canonical authority must define or permit:
- commit scope;
- message convention;
- one vs multiple commits.

Before commit:
- staged diff audited;
- security clean;
- required tests pass;
- no unrelated content.

After commit:
- record commit SHA;
- verify exact files included;
- verify no unintended files.

Do not amend/rebase/squash unless explicitly authorized.

---

# Phase 12 — Branch/push authority

Do not create/switch/push a branch unless explicitly authorized.

If required:
- use exact canonical branch name/pattern;
- preserve local unrelated work safely;
- never force-push unless explicitly authorized;
- record remote tracking state.

If branch creation cannot be done without risking unrelated work:
STOP.

---

# Phase 13 — PR readiness vs PR creation

The contract must distinguish:

## Readiness-only
Produce evidence that the change set is ready for PR, but do not create one.

## PR creation
Create/update the exact authorized PR.

If PR creation is authorized, require:
- exact base;
- exact head;
- title;
- body/summary;
- test evidence;
- issue linkage;
- no unsupported claims.

Do not merge unless merge is separately explicit.

---

# Phase 14 — #237 lifecycle

Only after every WP12 technical/security/readiness gate required by canonical authority passes:

If authorized:
1. #237 Project Status → Done;
2. read back metadata;
3. close #237;
4. read back.

If Project automation auto-closes the issue:
- explicit close is idempotent/no-op.

Preserve:
- Release;
- Priority;
- Area;
- item identity.

No item creation/deletion.

---

# Phase 15 — Milestone #58

Milestone closure is a distinct mutation.

Do NOT close milestone #58 unless canonical WP12/#237/release authority explicitly requires it and all prerequisite issues are closed.

If authorized:
- verify #237 is Closed;
- verify milestone open issue count = 0;
- verify all Release 1.9 milestone issues are in expected final state;
- close milestone;
- read back.

If authority says WP12 is only PR readiness and milestone closure occurs later:
leave milestone open.

Never infer milestone closure solely from 0 open issues.

---

# Phase 16 — PR merge / tag / release publication

Treat each as separately privileged.

Do not:
- merge PR;
- tag;
- publish GitHub Release;
- delete branch

unless canonical WP12 authority explicitly requires that exact action.

If absent:
report them as out-of-scope next actions.

---

# Phase 17 — Final acceptance matrix

Before any final lifecycle mutation, produce:

`WP12 criterion → required evidence → actual result → PASS/BLOCK`

Include every canonical row, plus:
- predecessor frozen;
- change-set inventory;
- security;
- tests;
- residue;
- docs/readiness;
- Git safety;
- PR readiness;
- lifecycle.

Every row must pass.

---

# Phase 18 — Final read-back

Record:

## Git
- branch;
- HEAD;
- origin relation;
- staged/unstaged/untracked state;
- commits created, if any;
- pushes, if any.

## GitHub
- #233–#236 preserved;
- #237 final state;
- #237 Project item final state;
- milestone #58 final state/counts;
- PR state, if any;
- release/tag state, if any.

## Repository
- exact mutations attributable to WP12;
- unrelated work preserved;
- no secret/local-only content included.

## Residue
- final zero-owned-residue evidence.

---

# Mutation accounting

The final report must enumerate every mutation separately.

Examples:

`WP12 REPOSITORY MUTATIONS: ZERO`

or exact authorized paths/commit.

`WP12 GIT MUTATIONS: <exact staging/commit/branch/push actions>`

`WP12 GITHUB MUTATIONS: <exact #237/PR/milestone actions>`

Never summarize broad mutation classes inaccurately.

---

# Blocker rules

STOP before mutation if:
- WP12 scope is high-level only;
- exact Git/PR lifecycle is unspecified;
- path inclusion authority is missing;
- dirty-worktree ownership is ambiguous;
- commit/branch/PR/milestone semantics are unspecified;
- release/tag/merge semantics are unspecified but appear required.

STOP before staging/commit if:
- secrets scan fails;
- unrelated changes cannot be separated;
- tests/residue gates fail.

STOP before #237 Done/Closed if:
- required PR/readiness gate has not passed.

STOP before milestone closure if:
- explicit authority is absent;
- any milestone issue remains open;
- release acceptance is incomplete.

Do not broaden scope.

---

# Required blocker report

## Verified entry state
Git/GitHub.

## Canonical WP12 sources
Exact artifacts/issues read.

## Authority matrix
Known vs missing.

## Minimum next authority
Name the narrow Luna authority required.

## Mutations
Repository/Git/GitHub all zero.

Required markers:

`WP12 REPOSITORY MUTATIONS: ZERO`

`WP12 GIT MUTATIONS: ZERO`

`WP12 GITHUB MUTATIONS: ZERO`

`RELEASE 1.9 WP12 IMPLEMENTATION AND COMPLETION BLOCKED`

---

# Required completion report

## Binding WP12 authority
Exact sources.

## Entry state
Git/GitHub.

## Frozen predecessor
WP11 technical acceptance.

## WP12 role
Exact closure/readiness responsibilities.

## Change-set inventory
Exact Release 1.9 paths vs excluded local/unrelated content.

## Security
Exact result.

## Technical readiness
Exact tests/counts.

## Residue
Exact result.

## Git actions
Exact staging/commit/branch/push, or zero.

## PR
Readiness/creation/update/merge state exactly.

## #237
Project item ID; Done/Closed if authorized.

## Milestone #58
Exact state/counts and whether mutated.

## Release/tag
Exact state; no mutation unless authorized.

## Preservation
#233–#236 unchanged; unrelated work preserved.

## Mutation statements
Exact.

## Next action
Only what canonical workflow says; do not perform unrequested/out-of-scope actions.

---

# Terminal markers

Success:

`RELEASE 1.9 WP12 EXECUTION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP12 IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE merely because #237 closes. COMPLETE requires every action required by the binding WP12 contract and no unauthorized mutation.

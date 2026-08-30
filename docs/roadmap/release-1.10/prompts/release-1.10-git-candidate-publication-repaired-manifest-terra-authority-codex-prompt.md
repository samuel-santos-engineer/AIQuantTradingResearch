# Release 1.10 — Git Candidate Publication & Pull Request Authority — Repaired-Manifest Terra Execution

## Model assignment
- **GPT-5.6 Luna** — contract/policy/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — PRIMARY publication execution, validation, approved Git/GitHub mutations.
- **GPT-5.6 Sol** — supporting analysis only.

**Selected execution model: GPT-5.6 Terra.**

# Binding reconciled state

Canonical base and required candidate parent:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Authoritative staging source:

`docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Authoritative procedure/validation source:

`docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`

Latest Luna repair froze:

- raw inventory: **109 = 21 tracked + 88 untracked**
- canonical publication candidate: **103 = 21 tracked + 82 untracked**
- candidate prompt artifacts: **70**
- candidate presence: **103/103**
- duplicate candidate entries: **0**
- non-path entries: **0**
- excluded execution-control artifacts: **6 files**
- staging at handoff: empty
- HEAD and `origin/main`: `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- #242–#249 Closed/Done
- milestone #59 Open, 0 open / 8 closed.

The repaired manifest's literal 103-path list is the sole staging allowlist. Do not reconstruct it from filenames, prior prompts, globbing, or memory.

The six excluded execution-control files are the exact paths persisted by the repaired manifest, comprising:
- remote-base reconciliation pair;
- Terra publication-resumption pair;
- publication-manifest repair-authority pair.

Do not stage any of them.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST TERRA PUBLICATION ENTRY: PASS`

# Scope

Authorized:
1. verify repaired manifest integrity;
2. run frozen re-anchor/full publication validation;
3. create/reuse governed Release 1.10 publication branch from canonical base;
4. stage exactly the literal 103-path manifest allowlist;
5. create one governed candidate commit;
6. push only the governed branch;
7. create/reuse exactly one Release 1.10 PR against `main`;
8. post-verify commit/remote/PR/GitHub lifecycle;
9. produce downstream merge/release handoff.

Forbidden:
- candidate content edits;
- planning edits;
- staging excluded controls;
- unrelated cleanup;
- force-push;
- merge;
- milestone #59 closure;
- tag/version mutation;
- GitHub Release publication;
- WP issue/Project mutation unless correcting an authority-caused inconsistency.

# Phase 1 — Fresh-state and manifest verification

Before mutation verify:
- HEAD = canonical base;
- `origin/main` = canonical base;
- no staged files;
- no merge/rebase/cherry-pick in progress;
- raw inventory arithmetic matches repaired governance or any difference is fully explained by execution-control creation only;
- manifest literal candidate list has exactly 103 unique repository paths;
- all 103 exist/present as classified;
- none of the six exclusions appears in candidate list;
- no warning/prose line appears in candidate list.

If base changed or candidate list is malformed again, BLOCK.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST INTEGRITY: PASS`

# Phase 2 — Full reconciled validation

Run the exact full validation policy frozen in the execution plan after re-anchor.

At minimum report actual results for:
- WP06 permanent focused suites;
- full Application tests;
- full Infrastructure tests;
- full Architecture tests;
- full Domain tests;
- total .NET;
- full Python;
- canonical build;
- Python version/environment as governed;
- Streamlit version;
- `pip check`;
- Gitleaks 8.30.1 using governed command;
- docs links/diff checks;
- schema v4 preservation;
- package/project/schema diff;
- process/listener/UI residue.

Expected carried-forward baselines:
- .NET 365/365
- Python 25/25
- build 0 errors
- Streamlit 1.61.1
- `pip check` clean
- Gitleaks clean
- schema v4
- zero package/project/schema mutation.

The two documented local certificate-selector warnings may remain only if unchanged and environment-only.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST FULL PUBLICATION VALIDATION: PASS`

# Phase 3 — Branch strategy

Use repository convention. Do not commit to `main`.

If no stronger governed name exists, use:
`release/1.10`

Create it from exactly:
`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

If an existing local/remote Release 1.10 branch exists, inspect before reuse. Never overwrite incompatible work.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST BRANCH STRATEGY: PASS`

# Phase 4 — Exact manifest-driven staging

Stage paths by consuming the repaired manifest literal allowlist.

Do not use `git add .`, `git add -A`, directory-wide adds, or globbing if they could include exclusions.

After staging prove:
- staged paths = exactly 103;
- staged tracked subset = 21;
- staged previously-untracked subset = 82;
- candidate prompt artifacts = 70;
- all six excluded controls remain unstaged;
- unrelated paths remain unstaged;
- no candidate path remains unstaged;
- `git diff --cached --check` passes.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST EXACT STAGING: PASS`

# Phase 5 — Pre-commit freeze

Compare staged path set byte-for-byte/path-for-path against the manifest literal allowlist.

Required:
- missing = 0
- extra = 0
- duplicates = 0
- excluded staged = 0

Confirm candidate parent will be canonical base.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST PRE-COMMIT FREEZE: PASS`

# Phase 6 — Candidate commit

Create exactly one governed candidate commit.

If repository convention does not mandate another subject, use:

`Release 1.10: governed observability and system health`

Record:
- full commit SHA;
- parent SHA;
- commit subject;
- committed path count.

Require:
- parent SHA = `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- committed path set = exact manifest 103 paths.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST CANDIDATE COMMIT: PASS`

# Phase 7 — Local post-commit verification

Verify:
- commit contains exactly 103 paths;
- no excluded control artifact entered commit;
- no Release 1.10 candidate path remains dirty;
- excluded controls remain local/uncommitted as governed;
- no unrelated content entered commit.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST POST-COMMIT VERIFY: PASS`

# Phase 8 — Push

Push only the governed Release 1.10 branch.
- no force push;
- no tag push;
- no `main` push.

Verify remote branch head equals candidate commit SHA.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST REMOTE PUBLICATION: PASS`

# Phase 9 — Pull request

Search first for an existing PR from the governed branch.

Create or reuse exactly one Release 1.10 PR against `main`.

Preferred title absent stronger convention:

`Release 1.10 — Governed Observability and System Health`

PR body must truthfully summarize:
- BCL-governed pipeline/boundary observability;
- no external exporter;
- Worker/interop lifecycle isolation;
- canonical .NET System Health via existing visualization read model;
- Streamlit presentation-only;
- permanent observability/no-bypass/security tests;
- developer setup/runbook;
- preserved schema v4;
- no live provider/trading/ML/backtesting/parallel pipeline;
- actual validation results from this execution;
- #242–#249 Closed/Done;
- milestone #59 Open, 0/8;
- merge/tag/release publication deferred.

Do not enable auto-merge.

Record PR number, URL, base, head branch, head SHA, state.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST PULL REQUEST: PASS`

# Phase 10 — Post-publication verification

Verify:
- remote branch head = candidate SHA;
- PR head SHA = candidate SHA;
- PR base = `main`;
- PR Open;
- no merge;
- #242–#249 remain Closed/Done;
- milestone #59 remains Open, 0 open / 8 closed;
- no Project mutation;
- no tag/version;
- no GitHub Release.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST PUBLICATION POST-VERIFY: PASS`

# Phase 11 — Mutation ledger

Report exact explicit mutations.

Repository content:
- expected ZERO content edits.

Git:
- branch create/reuse;
- exact staging;
- one commit;
- push/upstream.

GitHub:
- PR create/update only.

No issue/Project/milestone/tag/release mutation expected.

Do not count automation/read operations.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST PUBLICATION MUTATION ACCOUNTING: PASS`

# Phase 12 — Publication acceptance and downstream handoff

Require:
- canonical 103-path commit published;
- parent = `5cc2...`;
- validation PASS;
- one correct open PR;
- no forbidden mutation.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST PUBLICATION ACCEPTANCE: PASS`

Handoff must include:
- base SHA;
- candidate commit SHA;
- branch;
- PR number/URL;
- PR base/head;
- 103-path count;
- validation results;
- issue/milestone state;
- remaining authority needs: final PR acceptance/merge, post-merge verification, milestone closure, version/tag, GitHub Release publication, final idempotent verification as governed.

Emit:
`RELEASE 1.10 REPAIRED-MANIFEST → MERGE/RELEASE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Required success terminal

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY COMPLETE`

# Block conditions

BLOCK if:
- base changes;
- manifest literal list ceases to be exactly 103 valid unique paths;
- any candidate path is missing;
- an excluded control appears in staging;
- staging cannot exactly match manifest;
- validation fails and repair would require content mutation;
- branch/remote contains incompatible work;
- PR identity is ambiguous;
- authentication/permissions prevent safe publication.

On block, preserve valid work and report exact mutations already performed. Never merge/tag/close milestone/publish release.

Exact blocked terminal:

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY BLOCKED`

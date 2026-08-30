# Release 1.10 — Git Candidate Publication & Pull Request Authority — Repaired Manifest Final Terra Execution

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY execution authority for final validation, exact staging, governed Git publication, and pull-request creation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Purpose

Publish the reconciled Release 1.10 candidate using the repaired manifest as the sole authoritative staging boundary.

This authority begins only after:

- publication-manifest literal-list repair completed;
- Infrastructure runner/hang reconciliation completed;
- OpenTelemetry Markdown content-hygiene repair completed.

This is the final candidate-publication / PR-creation authority. It does NOT authorize merge, milestone closure, tagging/versioning, or GitHub Release publication.

---

# Binding entry state

Treat as accepted unless direct inspection contradicts it:

Canonical base / required candidate parent:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Authoritative staging source:

`docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Authoritative procedure/validation source:

`docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`

Canonical publication candidate:

- exactly **103 paths**
- **21 tracked**
- **82 untracked before staging**
- **70 prompt artifacts**
- candidate presence **103/103**
- duplicates **0**
- non-path entries **0**

Explicit publication-control exclusions remain exactly as frozen by the repaired manifest.

Known content-hygiene repair:

- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
- exactly three trailing-whitespace defects removed
- trailing-whitespace lines: 3 → 0
- line count remains 188
- no semantic Markdown change
- file remains part of canonical 103-path candidate
- staging remains empty at handoff

Known terminal validation evidence:

- Infrastructure full suite: **191/191 PASS**
- WP08 lifecycle: **18/18 PASS**
- Architecture: **27/27 PASS**
- Application: **136/136 PASS**
- Domain: **11/11 PASS**
- Python: **25/25 PASS**
- build: **0 errors**
- two known local certificate-selector warnings remain environment-only
- Python **3.13.15**
- Streamlit **1.61.1**
- `pip check` clean
- Gitleaks **8.30.1**, no leaks across **112 commits**
- owned stale runner processes cleaned; no unrelated process touched

Issue/milestone state:

- #242–#249 Closed/Done
- milestone #59 Open, 0 open / 8 closed

No candidate commit, push, PR, tag, merge, milestone close, or GitHub Release has yet occurred.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION ENTRY: PASS`

---

# Authority precedence

The repaired manifest is the sole path-level publication authority.

If any prior publication/resumption prompt contains stale literals regarding:

- base SHA;
- candidate count;
- tracked/untracked counts;
- prompt count;
- exclusion count;
- staging allowlist;
- whitespace blocker;

those stale literals are superseded by:

1. the repaired manifest;
2. the reconciled execution plan;
3. this final Terra authority.

All other safety constraints from prior publication authorities remain binding.

---

# Authorized mutations

Allowed Git mutations:

- create or safely reuse one governed Release 1.10 publication branch;
- stage exactly the manifest's literal 103-path candidate;
- create exactly one candidate commit;
- push only the governed publication branch.

Allowed GitHub mutations:

- create or safely reuse exactly one Release 1.10 pull request against `main`;
- update that PR only if required to make title/body/head/base truthful.

Forbidden:

- content edits;
- planning edits;
- test/source/doc edits;
- package/project/schema/signing-config edits;
- force push;
- push to `main`;
- merge;
- auto-merge;
- milestone #59 closure;
- issue/Project lifecycle mutation;
- tag/version creation;
- GitHub Release publication.

---

# Phase 1 — Fresh-state verification

Verify before any mutation:

1. `HEAD` is based on or exactly at canonical base:
   `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
2. `origin/main` equals canonical base.
3. staging is empty.
4. no merge/rebase/cherry-pick in progress.
5. no incompatible publication branch exists.
6. repaired manifest literal candidate list has exactly 103 unique valid paths.
7. all 103 candidate paths are present.
8. no excluded publication-control artifact appears in the literal candidate list.
9. `OPEN_TELEMETRY_SELECTION.md` is included in candidate and has zero trailing-whitespace defects.
10. no ungoverned local path would be captured by publication.

If any candidate/base/path invariant fails, BLOCK.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION PRECONDITIONS: PASS`

---

# Phase 2 — Final publication validation

Run the exact validation still required by the reconciled execution plan before publication.

At minimum:

- verify prior full terminal suite results remain applicable;
- rerun any final publication gate explicitly required after the content-hygiene repair;
- verify documentation/link checks;
- verify `git diff --check` on current candidate state;
- verify schema v4 preservation;
- verify package/project/schema diff remains zero;
- verify Gitleaks is clean if required by final publication gate;
- verify no owned Worker/testhost/Python/Streamlit/listener residue.

The exact staged diff gate must also be rerun after staging in Phase 4.

Do not rerun expensive suites unless execution plan requires it; if required, run and report actual results.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION VALIDATION: PASS`

---

# Phase 3 — Governed branch

Use repository convention.

If no stronger governed name exists, use:

`release/1.10`

Requirements:

- branch point = canonical base;
- do not commit to `main`;
- do not overwrite incompatible local/remote work;
- no force-reset of unrelated branch history.

Record:

- local branch name;
- branch start SHA;
- existing/new classification.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION BRANCH: PASS`

---

# Phase 4 — Exact manifest-driven staging

Stage exactly the 103 literal paths from:

`docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Do not derive paths from:

- wildcards;
- directory scans;
- filenames from prior prompts;
- `git add .`;
- `git add -A`;
- broad directory staging.

After staging prove:

- staged path count = **103**
- staged tracked subset = **21**
- staged previously-untracked subset = **82**
- prompt artifact count = **70**
- missing candidate paths = **0**
- extra staged paths = **0**
- excluded control paths staged = **0**
- candidate paths left unstaged = **0**

Emit:

`RELEASE 1.10 FINAL TERRA EXACT STAGING: PASS`

---

# Phase 5 — Mandatory staged diff gate

Run:

- `git diff --cached --check`
- staged path-list comparison against manifest
- staged diff inspection sufficient to prove no unauthorized content/path drift

Required:

- whitespace errors = 0
- missing = 0
- extra = 0
- duplicates = 0
- excluded staged = 0

Confirm the prior three trailing-space defects are no longer present in staged content.

If the staged diff gate fails, unstaging is allowed; content repair is NOT authorized here. BLOCK and report exact defect.

Emit:

`RELEASE 1.10 FINAL TERRA STAGED DIFF GATE: PASS`

---

# Phase 6 — Candidate commit freeze

Before commit verify:

- staged set equals manifest exactly;
- candidate parent will be:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- no excluded/unrelated local artifact is staged;
- no candidate content edit occurred in this authority.

Preferred commit subject unless repository convention requires another:

`Release 1.10: governed observability and system health`

Emit:

`RELEASE 1.10 FINAL TERRA PRE-COMMIT FREEZE: PASS`

---

# Phase 7 — Candidate commit

Create exactly one governed Release 1.10 candidate commit.

Record:

- commit SHA;
- parent SHA;
- subject;
- committed path count.

Require:

- parent SHA = `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- committed paths = exactly 103
- no excluded path committed

Emit:

`RELEASE 1.10 FINAL TERRA CANDIDATE COMMIT: PASS`

---

# Phase 8 — Local post-commit verification

Verify:

- candidate commit contains exactly 103 paths;
- committed set equals manifest literal list;
- no excluded publication-control artifact entered commit;
- no candidate path remains dirty due to missed staging;
- excluded local control artifacts remain uncommitted as governed;
- no unrelated content entered commit.

Emit:

`RELEASE 1.10 FINAL TERRA LOCAL POST-COMMIT VERIFY: PASS`

---

# Phase 9 — Push

Push only the governed Release 1.10 publication branch.

Forbidden:

- force push;
- tag push;
- `main` push.

Verify remote branch head equals candidate commit SHA.

Emit:

`RELEASE 1.10 FINAL TERRA REMOTE PUBLICATION: PASS`

---

# Phase 10 — Pull request

Search first for an existing open/closed PR for the governed head branch.

Create or reuse exactly one Release 1.10 PR against `main`.

Preferred title unless repository convention requires another:

`Release 1.10 — Governed Observability and System Health`

PR body must truthfully summarize:

- governed BCL/OpenTelemetry-style observability contract;
- no external exporter in Release 1.10;
- application/infrastructure/Worker/interop boundary instrumentation;
- canonical .NET-owned System Health read model;
- Streamlit presentation-only role;
- schema v4 preserved;
- permanent no-bypass/observability tests;
- operational/developer docs;
- actual validation results;
- content-hygiene repair completed;
- #242–#249 Closed/Done;
- milestone #59 Open, 0 open / 8 closed;
- merge/tag/release publication deferred.

Do not enable auto-merge.

Record:

- PR number;
- URL;
- state;
- base;
- head branch;
- head SHA.

Emit:

`RELEASE 1.10 FINAL TERRA PULL REQUEST: PASS`

---

# Phase 11 — Post-publication verification

Verify:

- remote branch head = candidate SHA;
- PR head SHA = candidate SHA;
- PR base = `main`;
- PR state = Open;
- no merge occurred;
- no auto-merge enabled;
- #242–#249 remain Closed/Done;
- milestone #59 remains Open, 0 open / 8 closed;
- no Project mutation;
- no tag/version;
- no GitHub Release.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION POST-VERIFY: PASS`

---

# Phase 12 — Mutation ledger

Report exact explicit mutations.

Repository content:
- expected ZERO content edits.

Git:
- branch create/reuse;
- exact staging;
- one commit;
- push/upstream.

GitHub:
- one PR create/reuse/update as needed.

Expected issue/Project/milestone/tag/release mutations:
- ZERO.

Do not count read-only commands or automation side effects you did not explicitly invoke.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION MUTATION AUDIT: PASS`

---

# Phase 13 — Acceptance and downstream handoff

Require all:

- validation PASS;
- exact staged diff PASS;
- exact 103-path commit;
- parent = canonical base;
- remote branch published;
- exactly one correct open PR;
- no forbidden mutation.

Emit:

`RELEASE 1.10 FINAL TERRA PUBLICATION ACCEPTANCE: PASS`

Produce downstream handoff with:

- canonical base SHA;
- candidate commit SHA;
- branch;
- PR number/URL;
- PR base/head;
- committed path count = 103;
- validation results;
- #242–#249 Closed/Done;
- milestone #59 Open 0/8;
- remaining authority needs:
  - PR acceptance/merge;
  - post-merge verification;
  - milestone closure at release completion;
  - version/tag;
  - GitHub Release publication;
  - final idempotent verification.

Emit:

`RELEASE 1.10 FINAL TERRA → MERGE/RELEASE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success terminal

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY COMPLETE`

---

# Block conditions

BLOCK if:

- canonical base changes;
- repaired manifest is no longer exactly 103 valid unique paths;
- any candidate path is missing;
- any excluded path is staged;
- staged diff check fails;
- validation required by execution plan fails;
- incompatible branch/remote state exists;
- candidate commit parent would differ from canonical base;
- PR identity is ambiguous;
- authentication/permissions prevent safe publication.

On BLOCK:

- preserve any valid work already performed;
- if staging occurred but commit did not, unstage unless doing so would destroy evidence;
- do not edit content;
- do not merge;
- do not close milestone;
- do not tag/version;
- do not publish GitHub Release;
- report exact mutations already made.

# Exact blocked terminal

`RELEASE 1.10 — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY BLOCKED`

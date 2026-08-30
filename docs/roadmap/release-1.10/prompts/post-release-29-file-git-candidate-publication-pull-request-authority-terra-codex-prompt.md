# Post-Release 29-File Git Candidate Publication & Pull Request Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY execution authority for exact candidate validation, approved Git mutations, push, and PR creation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Purpose

Publish the currently intended **29-file local candidate** as a dedicated Git branch and GitHub pull request after Release 1.10 publication.

This authority may:

1. inventory and freeze the exact 29-file candidate;
2. validate that candidate;
3. stage exactly those 29 files;
4. create one candidate commit;
5. push one dedicated branch;
6. open one pull request against `main`;
7. verify exact PR payload equivalence.

This authority does **not** authorize merge, milestone mutation, tag/version changes, or GitHub Release changes.

---

# Binding repository baseline

Release 1.10 is lifecycle-complete and published.

Canonical release state:

- version: `1.10.0`
- tag: `v1.10.0`
- PR #250 merge/release commit:
  `eb9601596d9a9dd68f1f8a7c963906a76e5a2833`
- milestone #59: Closed, 0 open / 8 closed
- #242–#249: Closed/Done
- Project #2: Done / Release 1.10
- GitHub Release `v1.10.0`: published, non-draft, non-prerelease, zero assets
- release disclosure preserved:
  `POST-MERGE VALIDATION EXECUTED WITH WINDOWS SMART APP CONTROL OFF`

Most recent local-work inventory reported:

- staging: 0
- tracked local changes: 2
- untracked local/control files: 25
- total intended merge candidate: **29 files**

The user has explicitly stated that these **29 files should be merged**.

Emit:

`POST-RELEASE 29-FILE PR AUTHORITY ENTRY: PASS`

---

# Critical candidate rule

The current 29-file local state is the sole candidate source.

Before any staging, enumerate every path using Git status and freeze a literal sorted 29-path manifest.

The candidate must satisfy:

`29 = tracked modified/deleted/renamed paths + untracked paths`

No path may be inferred, silently added, silently dropped, or substituted.

If the observed candidate is not exactly 29 paths, BLOCK before staging.

Emit:

`POST-RELEASE 29-FILE CANDIDATE INVENTORY: FROZEN — 29 PATHS`

---

# Content classification

For each of the 29 paths, classify:

- tracked modification;
- tracked deletion;
- tracked rename;
- untracked file.

Also classify functionally, where applicable:

- signing/developer-environment artifact;
- documentation;
- governance/authority artifact;
- source;
- test;
- configuration;
- other.

The user has authorized these 29 files as the intended PR candidate. Do not exclude a path merely because it is a control/authority artifact if it is one of the frozen 29.

However, BLOCK if any candidate path contains:

- private keys;
- passwords/tokens/secrets;
- machine-specific credentials;
- certificate private-key material;
- prohibited binary signing secrets;
- unrelated personal data;
- obvious temporary build outputs (`bin/`, `obj/`, `.pytest_cache/`, etc.) unless already tracked and intentionally governed.

Run Gitleaks or equivalent repository secret scan before staging.

Emit:

`POST-RELEASE 29-FILE CONTENT CLASSIFICATION: PASS`

---

# Mutation boundary

## Authorized

Only after all preconditions pass:

- create a dedicated branch from authoritative `main`;
- stage exactly the frozen 29 paths;
- create exactly one candidate commit unless repository hooks require a non-content metadata action;
- push exactly the dedicated branch;
- open exactly one GitHub PR against `main`;
- set a concise PR title/body grounded in the actual 29-file diff.

## Forbidden

- editing candidate content during publication;
- adding/removing candidate paths after freeze;
- staging any path outside the 29;
- committing unrelated files;
- force-push;
- rewriting `main`;
- merging the PR;
- milestone mutation;
- issue/Project lifecycle mutation;
- version/tag mutation;
- GitHub Release mutation;
- deleting local files to make counts match.

If candidate content requires repair, BLOCK and request a separate narrow repair/reconciliation authority.

---

# Phase 1 — Authoritative base verification

Fetch/read remote state and verify:

- `origin/main` authoritative SHA;
- local `main` relationship to `origin/main`;
- Release 1.10 merge commit remains an ancestor of current `main`;
- no unreviewed local commit is mixed into the candidate.

Expected baseline, absent later legitimate commits:

`eb9601596d9a9dd68f1f8a7c963906a76e5a2833`

If `main` has legitimately advanced since Release 1.10 publication, do not automatically BLOCK. Instead:

1. record the new authoritative `origin/main`;
2. prove Release 1.10 merge commit remains an ancestor;
3. prove the 29-file candidate can be safely based on current `main`;
4. ensure no candidate path conflicts with intervening remote changes.

If overlap/conflict exists, BLOCK for Luna reconciliation.

Emit:

`POST-RELEASE 29-FILE AUTHORITATIVE BASE: VERIFIED`

---

# Phase 2 — Freeze exact 29-path manifest

Capture a literal sorted list of all 29 intended paths.

Verify:

- count = 29;
- duplicates = 0;
- every listed path exists in the observed local candidate state;
- no observed candidate path is omitted;
- staging is empty before publication.

Compute and report:

- tracked candidate count;
- untracked candidate count;
- total = 29.

Emit:

`POST-RELEASE 29-FILE MANIFEST INTEGRITY: PASS — 29/29`

---

# Phase 3 — Pre-publication validation

Run validation appropriate to the actual diff.

Minimum required gates:

1. `git diff --check` or equivalent whitespace/error check;
2. Gitleaks/security scan;
3. repository build if any source/project/configuration file changes;
4. affected test suites if source/test/configuration behavior changes;
5. documentation/link checks if docs/Markdown files change;
6. package/schema/project invariant checks if relevant.

If the 29 paths are purely governance/documentation/developer-environment artifacts, do not manufacture irrelevant product test requirements, but still run:

- diff/whitespace validation;
- secret scan;
- applicable documentation checks;
- repository status/integrity checks.

If tracked signing-related files affect executable project configuration, run the corresponding build/tests before publication.

All required validation must produce terminal results.

Emit:

`POST-RELEASE 29-FILE PRE-PUBLICATION VALIDATION: PASS`

---

# Phase 4 — Branch creation

Create a dedicated publication branch from the verified authoritative base.

Preferred branch name:

`post-release/1.10-local-followups`

If that branch already exists remotely or locally with unrelated history, choose a deterministic non-conflicting alternative such as:

`post-release/1.10-local-followups-29`

Record the exact branch name.

Do not create the branch from a dirty non-authoritative commit. The worktree may contain the frozen 29-file candidate while branch creation occurs, but the branch base must be the verified `main` commit.

Emit:

`POST-RELEASE 29-FILE BRANCH: CREATED`

---

# Phase 5 — Exact staging

Stage only the literal 29-path manifest.

Immediately verify:

- staged path count = 29;
- staged path set equals frozen manifest exactly;
- missing = 0;
- extra = 0;
- no non-candidate path staged.

Then run the exact staged-diff integrity checks again, including:

- `git diff --cached --check`;
- secret scan over the staged/candidate state as appropriate.

If any check fails, unstage only the candidate paths if safe, preserve worktree content, and BLOCK.

Emit:

`POST-RELEASE 29-FILE STAGING: PASS — EXACT 29/29`

---

# Phase 6 — Candidate commit

Create exactly one candidate commit containing the 29 paths.

Preferred commit subject:

`chore: publish post-release 1.10 local follow-ups`

If the actual content is better represented by another conventional subject, choose a concise truthful subject without altering scope.

After commit, verify:

- parent = verified authoritative base;
- commit path set = exactly 29;
- no missing/extra paths;
- worktree contains no newly introduced authority mutation beyond expected pre-existing state;
- staging is empty.

Record the candidate commit SHA.

Emit:

`POST-RELEASE 29-FILE CANDIDATE COMMIT: CREATED`

`POST-RELEASE 29-FILE COMMIT PAYLOAD: PASS — 29/29`

---

# Phase 7 — Push

Push only the dedicated branch.

Do not push `main`.

Do not push tags.

Do not force-push.

Verify the remote branch resolves to the candidate commit SHA.

Emit:

`POST-RELEASE 29-FILE BRANCH PUSH: PASS`

---

# Phase 8 — Pull request creation

Create exactly one PR:

- base: `main`
- head: dedicated candidate branch

Preferred title:

`Post-release 1.10 local follow-ups`

PR body must include:

- authoritative base SHA;
- candidate commit SHA;
- exact 29-path payload statement;
- high-level classification of the files;
- validation results;
- explicit statement that this PR does not modify Release 1.10 tag/release history;
- explicit statement that merge is not authorized by this authority.

If signing/developer-environment changes are included, describe them truthfully without exposing secrets.

Do not add unrelated milestone/issue linkage unless already required by existing governance.

Emit:

`POST-RELEASE 29-FILE PULL REQUEST: CREATED`

---

# Phase 9 — PR payload equivalence

Read back the created PR and verify:

- state = Open;
- base = `main`;
- head = expected branch;
- PR commit includes exact 29-file candidate;
- changed-file set = exactly the frozen manifest;
- missing = 0;
- extra = 0;
- no unexpected GitHub lifecycle mutation occurred.

Emit:

`POST-RELEASE 29-FILE PR PAYLOAD: PASS — 29/29`

---

# Phase 10 — Mutation accounting

Report exact mutations.

Expected Git mutations:

- one branch creation;
- one candidate commit;
- one branch push.

Expected GitHub mutations:

- one PR creation.

Expected repository-content edits by this authority:

- ZERO.

The authority publishes existing local content; it does not edit that content.

Not authorized / expected zero:

- PR merge;
- milestone mutation;
- issue/Project mutation;
- tag/version mutation;
- GitHub Release mutation;
- force-push;
- `main` push.

Emit:

`POST-RELEASE 29-FILE PR MUTATION AUDIT: PASS`

---

# Phase 11 — Handoff

On success, leave the PR Open.

Do not merge it.

The next authority, if requested, must separately govern:

- PR review/acceptance;
- final validation;
- merge;
- post-merge verification.

Emit:

`POST-RELEASE 29-FILE PR → REVIEW/MERGE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`POST-RELEASE 29-FILE PR AUTHORITY ENTRY: PASS`

`POST-RELEASE 29-FILE CANDIDATE INVENTORY: FROZEN — 29 PATHS`

`POST-RELEASE 29-FILE CONTENT CLASSIFICATION: PASS`

`POST-RELEASE 29-FILE AUTHORITATIVE BASE: VERIFIED`

`POST-RELEASE 29-FILE MANIFEST INTEGRITY: PASS — 29/29`

`POST-RELEASE 29-FILE PRE-PUBLICATION VALIDATION: PASS`

`POST-RELEASE 29-FILE BRANCH: CREATED`

`POST-RELEASE 29-FILE STAGING: PASS — EXACT 29/29`

`POST-RELEASE 29-FILE CANDIDATE COMMIT: CREATED`

`POST-RELEASE 29-FILE COMMIT PAYLOAD: PASS — 29/29`

`POST-RELEASE 29-FILE BRANCH PUSH: PASS`

`POST-RELEASE 29-FILE PULL REQUEST: CREATED`

`POST-RELEASE 29-FILE PR PAYLOAD: PASS — 29/29`

`POST-RELEASE 29-FILE PR MUTATION AUDIT: PASS`

`POST-RELEASE 29-FILE PR → REVIEW/MERGE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`POST-RELEASE 29-FILE — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY COMPLETE`

---

# Block conditions

BLOCK without broadening scope if:

- observed candidate count is not exactly 29;
- exact path manifest cannot be frozen;
- candidate contains secrets/private-key material or obvious unintended build/temp output;
- authoritative base cannot be proven;
- remote changes overlap/conflict with candidate paths;
- required validation fails;
- staged set differs from 29-path manifest;
- commit payload differs from 29-path manifest;
- remote branch does not resolve to the created commit;
- PR changed-file set differs from exact 29 paths;
- candidate content requires repair/editing;
- any unauthorized release/lifecycle mutation occurs.

On BLOCK:

- do not merge;
- do not mutate milestone/issues/Project/tag/Release;
- do not force-push;
- preserve candidate content and evidence;
- report the narrow next authority required.

# Exact blocked terminal

`POST-RELEASE 29-FILE — GIT CANDIDATE PUBLICATION & PULL REQUEST AUTHORITY BLOCKED`

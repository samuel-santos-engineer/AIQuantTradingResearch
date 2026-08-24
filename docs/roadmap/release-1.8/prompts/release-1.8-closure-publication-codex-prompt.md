# Release 1.8 — Closure & Publication — Codex Authority

## 1. Mission

Perform the **narrow closure and publication workflow** for accepted Release 1.8 of:

`samuel-santos-engineer/AIQuantTradingResearch`

Release:

`1.8 — Python & AI Engineering Foundation`

Accepted milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

This authority exists only to convert the already accepted Release 1.8 repository state into a durable, committed, published release boundary.

Release 1.8 implementation and acceptance are already complete.

This authority MUST NOT perform new product implementation, architectural redesign, Release 1.9 planning, or opportunistic cleanup.

---

## 2. Accepted Release 1.8 Evidence

The authoritative accepted state entering this workflow is:

- WP01–WP13: CLOSED / Done;
- milestone #56: CLOSED;
- milestone #56: 13 closed / 0 open;
- Project #2: exactly 13 Release 1.8 items;
- all 13: Done / P1;
- authoritative Areas preserved;
- dependency chain: exactly 12 edges;
- ACC1–ACC20: PASS;
- schema: v3;
- WP08 scientific validation: 4/4 × 3;
- WP11 interoperability subset: 11/11 × 3;
- full .NET verification: 281/281;
- skipped: 0;
- build: 0 warnings / 0 errors;
- Gitleaks: PASS;
- `pip check`: PASS;
- exact governed direct Python dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- machine Python: PSF CPython 3.13.15 AMD64;
- `.venv`: isolated, ignored, untracked;
- Release 1.9 milestone #50: untouched;
- Release 2.0 milestone #51: untouched.

WP13 permitted only narrow documentation corrections and reported no production/test/dependency/schema/Git-history mutation.

---

## 3. Critical Baseline Reconciliation

The WP13 accepted pre-publication baseline reported:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

However, Release 1.8 work may currently exist as legitimate uncommitted repository changes because prior execution authorities intentionally prohibited commits.

Therefore:

**DO NOT require current HEAD to contain the Release 1.8 work before the closure commit.**

Instead prove:

1. branch is `main`;
2. local HEAD equals `origin/main`;
3. ahead/behind is `0/0`;
4. current HEAD is the accepted pre-publication baseline unless a separately accepted later baseline is provable;
5. all tracked/untracked Release 1.8 changes are explainable by the governed WP01–WP13 work and closure-authority inputs;
6. no unrelated change is mixed into the candidate release commit.

If HEAD differs from the accepted pre-publication baseline without explicit accepted authority, stop.

---

## 4. Mandatory Starting-State Gate

Before mutation verify:

### Git

- repository identity is correct;
- remote points to the expected GitHub repository;
- branch: `main`;
- local HEAD = `origin/main`;
- divergence: `0/0`;
- no merge/rebase/cherry-pick/revert in progress;
- staged paths are fully accounted for;
- unstaged tracked paths are fully accounted for;
- untracked paths are fully accounted for;
- no conflict markers.

### GitHub

Read back:

- #211–#223: CLOSED;
- Project Status: Done;
- milestone #56: CLOSED, 13/13;
- Project #2: exactly 13 Release 1.8 items;
- dependency chain: 12 edges;
- historical milestone #49 unchanged;
- Release 1.9 milestone #50 unchanged;
- Release 2.0 milestone #51 unchanged.

If API rate limiting prevents required authoritative read-back, stop.

### Accepted validation

Confirm the accepted WP13 report/evidence remains represented by repository state.

Do not rerun destructive setup.

---

## 5. Closure Scope

This authority may perform only:

1. final release-state documentation reconciliation;
2. inclusion of all governed Release 1.8 Markdown documents in Git;
3. inclusion of all accepted Release 1.8 implementation/test/configuration changes in Git;
4. final validation of the exact commit candidate;
5. one Release 1.8 closure commit on `main`;
6. push of that commit to `origin/main`;
7. optional annotated Git tag only as explicitly governed below;
8. optional GitHub Release publication only as explicitly governed below;
9. final read-back and immutable SHA reporting.

No other mutation is authorized.

---

## 6. Mandatory Markdown Inclusion Rule

This is a hard release-closure requirement:

> **All Markdown documents created or modified as part of the governed Release 1.8 work must be committed.**

This includes, where present:

- Release 1.8 definition;
- execution plan;
- file manifest;
- planning acceptance/reconciliation authorities that are intended as durable repository governance;
- Python runtime selection/compatibility records;
- Python dependency governance;
- NumPy selection record;
- pandas selection record;
- scikit-learn selection record;
- Streamlit selection record;
- VS Code Python extension selection record;
- .NET↔Python interoperability record;
- architecture/documentation alignment changes;
- portable Python environment/developer guide;
- README changes;
- testing/developer/architecture documentation changed by WP12/WP13;
- any other `.md` file created or modified by accepted WP01–WP13 repository work.

No accepted Release 1.8 `.md` document may remain untracked, unstaged, or uncommitted after closure.

---

## 7. Execution Prompt Exception

Codex execution-only prompt files require explicit classification.

Inspect all Release 1.8 files matching patterns such as:

- `*-codex-prompt.md`;
- `*-codex-prompt-chat.md`;
- corrective/reconciliation authority prompts;
- execution authority pairs.

Because the user explicitly requires **all Markdown documents to be committed**, the default for this closure is:

**COMMIT THEM if they are currently located inside the repository and were used as governed Release 1.8 authority/evidence.**

Do not delete or exclude them merely because earlier work packages treated them as untracked execution inputs.

If any such prompt contains:

- secrets;
- credentials;
- tokens;
- machine-private data;
- accidental temporary content;
- content clearly not intended for repository history;

stop rather than silently omit or sanitize it.

If an authority pair exists only outside the repository, do not copy it into the repository unless the Release 1.8 manifest or established repository convention requires it.

The closure authority pair itself is committed only if it is placed inside the repository as part of the governed closure workflow.

---

## 8. Complete Markdown Inventory

Before staging, generate a read-only inventory of:

- tracked modified `.md`;
- untracked `.md`;
- staged `.md`;
- ignored `.md`, if any relevant Release 1.8 artifact is unexpectedly ignored.

For every candidate Markdown file classify:

- RELEASE_1_8_GOVERNED — must commit;
- PREEXISTING_UNRELATED — must not commit;
- SENSITIVE/BLOCKED — stop;
- AMBIGUOUS — stop.

Report the complete governed Markdown list before commit.

Do not use broad `git add .` until classification is complete.

---

## 9. Non-Markdown Release 1.8 Inventory

Likewise classify all non-Markdown candidate changes.

Expected accepted categories may include:

- `.gitignore`;
- `requirements.txt`;
- Python validation scripts;
- Python production integration endpoint;
- Application contracts;
- Infrastructure integration implementation;
- permanent Application tests;
- permanent Infrastructure tests;
- test-only Python fixtures;
- configuration/DI changes explicitly delivered by WP10;
- other manifest-authorized Release 1.8 files.

For each path classify:

- RELEASE_1_8_GOVERNED;
- PREEXISTING_UNRELATED;
- BLOCKED;
- AMBIGUOUS.

Only RELEASE_1_8_GOVERNED paths may enter the release commit.

---

## 10. Forbidden Commit Content

Do not commit:

- `.venv/`;
- Python caches;
- `__pycache__/`;
- `.pyc`;
- temporary test output;
- coverage output;
- build artifacts;
- `bin/`;
- `obj/`;
- IDE caches;
- user-specific settings unless explicitly governed;
- credentials;
- API keys;
- OAuth material;
- GitHub tokens;
- local machine paths accidentally generated into temporary files;
- unrelated working-tree changes;
- Release 1.9 implementation/planning changes;
- Release 2.0 changes.

If any are staged, unstage them before proceeding.

---

## 11. Release-State Documentation Reconciliation

Before commit, inspect current-state documentation for lifecycle wording.

A narrow documentation-only correction is authorized if required to truthfully state:

- Release 1.8 is accepted;
- WP01–WP13 are complete;
- milestone #56 is closed;
- permanent test baseline is 281/281;
- Python foundation versions are governed as accepted;
- schema remains v3;
- Release 1.9 has not begun under this authority.

Do not rewrite architecture or add new design content.

Do not create release notes yet unless the repository has an established release-note convention or Section 19 authorizes GitHub Release notes.

---

## 12. Changelog

If the repository has an existing `CHANGELOG.md`, reconcile Release 1.8 using the existing format.

The entry must be factual and concise.

It may summarize accepted outcomes such as:

- Python 3.13 foundation;
- `.venv` and dependency governance;
- NumPy/pandas/scikit-learn/Streamlit foundation;
- VS Code Python tooling governance;
- scientific validation;
- .NET↔Python JSON-over-stdio boundary;
- infrastructure adapter;
- permanent interoperability tests;
- developer/documentation alignment;
- 281/281 accepted test baseline.

Do not claim Release 1.9 ML capabilities.

If no changelog convention supports a Release 1.8 entry, do not invent a new format without necessity.

---

## 13. Version/Tag Discovery

Before creating any tag, inspect existing Git tags and release conventions.

Determine:

- current tag naming pattern;
- whether prior releases use tags;
- whether GitHub Releases exist;
- whether semantic versions include `v` prefix;
- whether repository documentation defines publication rules.

Do not assume `v1.8.0`, `1.8`, or another tag format.

If no unambiguous existing convention exists, **stop before tag creation** and complete only the commit/push boundary unless the user has separately authorized a specific tag convention.

Commit success must not depend on tag ambiguity.

---

## 14. GitHub Release Discovery

Before publishing a GitHub Release, inspect prior repository GitHub Releases.

If an unambiguous convention exists, publication may follow it.

If no prior convention exists, stop before GitHub Release creation and report that the committed Release 1.8 boundary is complete but publication naming requires explicit authority.

Do not invent release title/tag conventions.

---

## 15. Pre-Staging Validation

Before staging:

- `git status`;
- candidate path inventory;
- Markdown inventory;
- secret scan;
- whitespace/conflict scan;
- ensure `.venv` ignored;
- ensure no generated residue;
- verify exact Python pins;
- verify schema v3.

If candidate content is not fully explainable, stop.

---

## 16. Staging Discipline

Stage only explicitly classified Release 1.8 governed paths.

Prefer path-specific staging.

After staging verify:

- staged file list;
- staged `.md` list;
- unstaged file list;
- untracked file list;
- `git diff --cached --stat`;
- `git diff --cached --name-status`;
- `git diff --cached --check`.

Hard gate:

**Every Release 1.8-governed Markdown file must be staged.**

Hard gate:

**No unrelated file may be staged.**

---

## 17. Staged Secret Scan

Run Gitleaks or the repository's canonical secret scan against the candidate state.

Require:

- no leaks;
- no credentials;
- no tokens.

If a leak is detected, stop.

Do not commit and then repair.

---

## 18. Final Candidate Validation

Run validation against the exact staged/candidate repository state.

Require:

### Python

- Python 3.13.15;
- exact four direct pins;
- `pip check`: PASS;
- global direct-package cleanliness: PASS;
- WP08 validation: 4/4;
- `.venv` ignored/untracked.

### Interoperability

- WP11 interoperability subset: 11/11;
- zero skipped;
- zero owned-process residue.

### .NET

- Domain: 11/11;
- Application: 121/121;
- Infrastructure: 136/136;
- Architecture: 13/13;
- total: 281/281;
- skipped: 0;
- build: 0 warnings / 0 errors.

### Engineering

- restore: PASS;
- format: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- terminal newline: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- `git diff --cached --check`: PASS;
- schema: v3;
- dependency graph: valid/acyclic.

If legitimate live counts differ, stop unless the difference is fully explained by an accepted pre-closure correction.

---

## 19. Release Commit

After every candidate gate passes, create exactly **one closure commit** containing the accepted Release 1.8 repository changes.

Use a concise repository-consistent commit message.

Preferred message if consistent with repository convention:

`release: complete 1.8 Python & AI engineering foundation`

Do not create multiple cleanup commits.

Do not amend historical commits.

After commit capture:

- commit SHA;
- commit subject;
- file count;
- Markdown file count;
- clean/still-untracked state.

The new commit SHA becomes the immutable Release 1.8 repository boundary.

---

## 20. Post-Commit Completeness Gate

Immediately after commit verify:

- no Release 1.8-governed tracked change remains;
- no Release 1.8-governed untracked file remains;
- specifically no governed `.md` remains untracked/uncommitted;
- `.venv` remains ignored;
- no staged paths;
- no unrelated path was committed.

Run a repository search/status audit sufficient to prove Markdown completeness.

If a governed Markdown file was missed, **do not push**.

Correct the candidate before publication. Because Section 19 requires exactly one closure commit, if the omission is discovered before push, amend that closure commit only after revalidation.

Do not amend after push without new authority.

---

## 21. Push Authority

After post-commit completeness passes:

Push:

`main` → `origin/main`

Then verify:

- local HEAD = `origin/main`;
- ahead/behind `0/0`;
- remote contains the closure commit.

Do not force-push.

Do not push another branch.

If push is rejected due to remote movement, stop and reconcile. Do not pull/rebase/merge automatically.

---

## 22. Tag Authority

A tag may be created **only if** Section 13 finds an unambiguous existing repository tag convention.

If authorized by convention:

- create exactly one annotated Release 1.8 tag at the closure commit;
- use repository-consistent naming;
- annotation should identify Release 1.8 — Python & AI Engineering Foundation;
- push only that tag;
- verify remote tag target equals closure commit.

Do not retag or overwrite an existing tag.

If a Release 1.8 tag already exists unexpectedly, stop.

If tag convention is ambiguous, skip tagging and report the boundary requiring human choice.

---

## 23. GitHub Release Authority

A GitHub Release may be published **only if**:

- tag creation was unambiguous and successful; and
- Section 14 found an established GitHub Release convention.

Use the exact closure tag.

Release notes must summarize only accepted Release 1.8 outcomes.

Include concise evidence:

- Python 3.13.15;
- governed scientific/ML/UI foundation;
- `.venv` isolation/dependency governance;
- .NET↔Python JSON-over-stdio boundary;
- permanent interoperability tests;
- 281/281, 0 skipped;
- schema v3 unchanged.

Do not describe Release 1.9 plans as delivered work.

Do not mark prerelease unless repository convention requires it.

Do not publish if convention is ambiguous.

---

## 24. GitHub Planning Preservation

Do not mutate:

- #211–#223 lifecycle;
- Project #2 fields;
- dependency chain;
- milestone #56 except read-back;
- milestone #49;
- milestone #50;
- milestone #51;
- Release 1.9 issues/planning;
- Release 2.0 issues/planning.

The closure commit/publication does not require reopening or editing accepted WP issues.

---

## 25. Release 1.9 Firewall

This authority explicitly forbids:

- Release 1.9 definition;
- Release 1.9 execution plan;
- Release 1.9 file manifest;
- Release 1.9 issues;
- Release 1.9 Project mutations;
- ML implementation;
- model training;
- feature engineering;
- inference;
- model persistence;
- Release 1.9 documentation creation.

Do not combine closure with next-release planning.

---

## 26. Mutation Accounting

Report exact deltas for:

- Markdown files committed;
- non-Markdown files committed;
- documentation-only closure corrections;
- production code;
- tests;
- Python files;
- dependencies;
- `.venv`;
- schema;
- packages/projects/references;
- Git commit;
- push;
- tag;
- GitHub Release;
- GitHub planning objects;
- Release 1.9/2.0.

For each Markdown file committed, include its repository-relative path in the final evidence or provide a complete counted inventory with the full path list in a clearly identified section.

---

## 27. Closure Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- PUB1 — repository/remote/branch baseline reconciled;
- PUB2 — GitHub accepted lifecycle reconciled;
- PUB3 — complete Release 1.8 Markdown inventory classified;
- PUB4 — all governed Release 1.8 Markdown documents included;
- PUB5 — non-Markdown candidate inventory classified;
- PUB6 — no forbidden/local/generated/sensitive content staged;
- PUB7 — lifecycle/changelog documentation truthful;
- PUB8 — exact Python runtime/dependency state preserved;
- PUB9 — WP08 validation passes;
- PUB10 — WP11 interoperability subset passes;
- PUB11 — full .NET 281/281, 0 skipped;
- PUB12 — build/format/Gitleaks/docs/diff gates pass;
- PUB13 — schema v3 and dependency graph preserved;
- PUB14 — staged candidate contains only governed Release 1.8 content;
- PUB15 — exactly one closure commit created;
- PUB16 — post-commit audit proves no governed Markdown omitted;
- PUB17 — push succeeds without force and local/remote converge;
- PUB18 — tag convention reconciled; tag published if unambiguous, otherwise explicitly deferred;
- PUB19 — GitHub Release convention reconciled; Release published if unambiguous, otherwise explicitly deferred;
- PUB20 — Release 1.9/2.0 and accepted GitHub planning state remain untouched.

PUB1–PUB17 and PUB20 must PASS for closure success.

PUB18/PUB19 may be NOT-APPLICABLE/DEFERRED only because repository publication convention is genuinely ambiguous.

---

## 28. Stop Conditions

Stop with:

`RELEASE 1.8 CLOSURE & PUBLICATION BLOCKED`

if:

- baseline cannot be reconciled;
- working-tree content includes unexplained changes;
- a governed Release 1.8 Markdown file cannot be safely committed;
- a Markdown authority contains sensitive material;
- candidate staging contains unrelated content;
- Gitleaks fails;
- Python/WP08/WP11/.NET validation fails;
- schema/dependency drift appears;
- post-commit audit finds omitted governed Markdown before push;
- remote `main` moves before push;
- push would require force;
- GitHub API read-back is unavailable for mandatory planning verification;
- publication would require inventing a tag/Release convention.

If only tag/GitHub Release convention is ambiguous **after commit and push have succeeded**, do not classify the repository closure itself as failed. Report publication as deferred and stop before the ambiguous mutation.

---

## 29. Required Execution Report

Report:

### Pre-Publication Baseline
- repository;
- branch;
- local HEAD;
- origin HEAD;
- divergence;
- accepted baseline reconciliation.

### GitHub Accepted State
- #211–#223;
- milestone #56;
- Project #2;
- historical/#50/#51 preservation.

### Markdown Inventory
For every governed `.md`:
- path;
- tracked/untracked starting state;
- classification;
- committed: yes/no.

### Non-Markdown Inventory
- governed candidate paths;
- excluded paths;
- reason.

### Candidate Validation
- Python;
- pins;
- WP08;
- WP11;
- .NET counts;
- build;
- format;
- Gitleaks;
- links/diff;
- schema/graph.

### Commit
- exact commit SHA;
- subject;
- total files;
- Markdown files;
- post-commit completeness.

### Push
- result;
- local/remote convergence.

### Tag
- discovered convention;
- tag name/target if created;
- otherwise deferred reason.

### GitHub Release
- discovered convention;
- publication result if created;
- otherwise deferred reason.

### PUB1–PUB20
Report each gate.

### Final Mutation Accounting
Report every repository/Git/GitHub mutation.

---

## 30. Success States

### A. Full closure + publication

Use when commit, push, tag, and GitHub Release all succeed under established conventions.

End exactly:

`RELEASE 1.8 CLOSURE & PUBLICATION COMPLETE`

`RELEASE 1.8 REPOSITORY BOUNDARY: <closure commit SHA>`

`ALL GOVERNED RELEASE 1.8 MARKDOWN DOCUMENTS: COMMITTED`

`RELEASE 1.8 TAG: <tag>`

`RELEASE 1.8 GITHUB RELEASE: PUBLISHED`

`NEXT AUTHORIZED ACTION: Begin Release 1.9 planning only under explicit new authority.`

### B. Repository closure complete; publication convention deferred

Use only when commit and push succeed, all governed Markdown is committed, but tag and/or GitHub Release convention is genuinely ambiguous.

End exactly:

`RELEASE 1.8 REPOSITORY CLOSURE COMPLETE`

`RELEASE 1.8 REPOSITORY BOUNDARY: <closure commit SHA>`

`ALL GOVERNED RELEASE 1.8 MARKDOWN DOCUMENTS: COMMITTED`

`RELEASE 1.8 TAG/GITHUB RELEASE: DEFERRED — EXPLICIT PUBLICATION CONVENTION AUTHORITY REQUIRED`

`NEXT AUTHORIZED ACTION: Define publication convention or begin Release 1.9 planning only under explicit new authority.`

### C. Blocked

End exactly:

`RELEASE 1.8 CLOSURE & PUBLICATION BLOCKED`

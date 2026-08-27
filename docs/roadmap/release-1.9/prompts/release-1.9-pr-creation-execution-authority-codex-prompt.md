# Release 1.9 — PR Creation Execution Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
Execute the PR-creation slice of the binding Release 1.9 finalization contract:

`docs/roadmap/release-1.9/RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md`

The accepted finalization model is:

`F-SPLIT`

This authority covers only:
- final R1 verification;
- branch preparation;
- exact-path staging;
- commit;
- push;
- PR creation;
- PR read-back.

It does **not** authorize:
- PR merge;
- milestone #58 closure;
- Git tag creation;
- GitHub Release publication;
- issue/Project mutation;
- release publication;
- branch deletion;
- unrelated repository changes.

---

# Frozen predecessor state

Treat as binding unless current read-back contradicts it:

- #233–#237 Closed / Done.
- milestone #58 Open, 0 open / 13 closed.
- WP12 PR readiness: PASS.
- exact hypothetical R1 set: 286 non-ignored paths.
- R5: none.
- `Directory.Build.local.props` ignored/local-only and excluded.
- signing secrets/configuration excluded.
- generated/runtime/test artifacts excluded.
- build: 0 warnings / 0 errors.
- .NET: 339/339.
- Python: 17/17.
- Streamlit 1.61.1.
- `pip check` clean.
- schema v4 preserved.
- security/Gitleaks clean.
- zero owned residue.
- `main == origin/main` at accepted predecessor boundary.
- staged paths 0 before finalization.

Late accepted atomic-replacement fix is part of Release 1.9 R1.

---

# Binding sources

Read completely before mutation:

1. finalization contract artifact;
2. WP12 closure/PR-readiness contract;
3. WP12 PR-readiness evidence;
4. current Git status and exact R1 manifest;
5. repository branch/PR workflow guidance;
6. current GitHub PR state;
7. milestone #58 read-back.

Do not infer workflow from generic GitHub practice.

---

# 1 — Pre-mutation Git safety snapshot

Record:

- current branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged paths;
- unstaged tracked paths;
- untracked paths;
- ignored local configuration relevant to signing.

Require:
- no unexpected staged content;
- no unexplained R1 drift;
- R5 remains empty.

If staged paths are nonzero unexpectedly:
STOP and preserve them.

---

# 2 — Final R1 verification

Reconcile current worktree against the binding finalization contract's exact R1 include manifest.

Require:

- every R1 path still present as intended;
- no new unexplained non-ignored path;
- no R2/R3/R4/R5 path enters R1;
- `Directory.Build.local.props` excluded;
- no secrets/private key material;
- no generated binaries/test/runtime artifacts.

If the finalization contract authority artifact itself is governed as R1, include it exactly as the contract specifies.

Do not broaden the path set.

---

# 3 — Freshness gate before staging

Apply the finalization contract's freshness rule.

If no post-readiness code/test changes occurred since accepted revalidation, inherited evidence may be sufficient.

If current diff includes any new post-readiness implementation/test change not already revalidated:
STOP and require revalidation.

Do not silently rerun/accept around new scope drift.

---

# 4 — Security preflight

Before staging:

- run approved Git-aware secret scan;
- inspect all untracked R1 files;
- verify no PFX/P12/PEM/private-key material;
- verify local signing configuration excluded;
- verify no signed build binaries;
- verify no credentials/tokens.

Any finding blocks PR creation.

---

# 5 — Branch authority

Follow the exact branch rule in the binding finalization contract.

If a branch name/pattern is defined, use it exactly.

If the contract permits a fresh Release 1.9 branch but does not define a literal name, use the canonical repository naming convention proven by repository guidance.

Do not invent a branch style.

Before branch creation/switch:
- ensure unrelated dirty/local-only state will remain preserved;
- no destructive cleanup;
- no reset/stash unless explicitly authorized.

If safe branch creation cannot be guaranteed:
STOP.

---

# 6 — Exact-path staging

Stage only the exact R1 manifest.

Forbidden:
- `git add .`
- `git add -A`
- broad wildcard staging.

After staging:

1. list staged paths;
2. compare exact staged set to R1;
3. inspect full staged diff;
4. verify no excluded path;
5. rerun staged-content security check if supported.

If any unintended path is staged:
- unstage only that unintended path if safe;
- do not modify working content;
- re-audit.

If exact R1 staging cannot be achieved:
STOP.

---

# 7 — Commit authority

Follow exact commit structure from the binding finalization contract.

Require:
- exact commit count/grouping;
- exact message or allowed pattern;
- no amend/rebase/squash unless contract explicitly permits.

Before commit:
- staged set exact;
- security clean;
- freshness gate satisfied.

After commit:
- record SHA;
- verify committed paths exactly match staged R1 set;
- verify no excluded content.

---

# 8 — Push authority

Push only if explicitly authorized by the finalization contract.

Use:
- exact remote;
- exact branch;
- normal push;
- no force push.

Record:
- remote branch;
- upstream;
- resulting ahead/behind relationship.

If push rejected due to divergence:
STOP.
Do not force/rebase without separate authority.

---

# 9 — PR creation

Create exactly one Release 1.9 PR if the contract authorizes PR creation.

Use exact contract rules for:

## Base
Canonical base branch.

## Head
The authorized Release 1.9 branch.

## Title
Exact title or pattern from contract.

## Body
Must include required evidence, including as applicable:

- Release 1.9 summary;
- WP01–WP12 completion;
- 339/339 .NET;
- 17/17 Python;
- schema v4;
- atomic-replacement race fix;
- simulated/replay non-live warning;
- architecture/no-bypass;
- security/Gitleaks;
- residue clean;
- documentation alignment;
- #233–#237 references.

Do not use misleading auto-closing syntax for already-closed issues unless canonical workflow requires references only.

## State
Draft vs ready-for-review exactly as contract specifies.

## Labels/reviewers
Only if contract explicitly authorizes them.

If an equivalent existing PR already exists:
- do not duplicate;
- follow idempotency rule from contract.

---

# 10 — PR read-back

After creation, verify:

- exactly one intended Release 1.9 PR;
- correct base;
- correct head;
- correct title;
- correct state;
- correct commit SHA;
- expected changed-file set;
- no excluded/local/generated content.

If the PR changed-file set differs materially from R1:
STOP and report.
Do not merge.

---

# 11 — Post-creation preservation

Verify:

- #233–#237 unchanged Closed/Done;
- milestone #58 remains Open;
- no tag;
- no GitHub Release;
- no PR merge;
- no branch deletion.

Read Git state:
- branch;
- HEAD;
- upstream;
- staged paths;
- remaining unstaged/untracked excluded content.

Preserve ignored/local-only configuration.

---

# 12 — Mutation boundary

Authorized Git mutations only:

- branch creation/switch if contract requires;
- exact-path staging;
- commit;
- push.

Authorized GitHub mutation only:

- one PR creation, or idempotent read-back of an already-existing canonical PR;
- only PR metadata explicitly required by contract.

Forbidden GitHub mutations:

- merge;
- milestone closure;
- issue/Project changes;
- tag;
- Release;
- branch deletion.

---

# Stop conditions

STOP before staging if:

- R1 drift;
- R5 appears;
- security failure;
- unclassified path;
- freshness invalid;
- branch rule ambiguous.

STOP before commit if:

- staged set != exact R1;
- staged security check fails;
- commit structure ambiguous.

STOP before push if:

- commit content mismatch;
- remote divergence;
- push authority ambiguous.

STOP before PR creation if:

- base/head/title/body semantics ambiguous;
- branch not pushed as required;
- equivalent PR state conflicts.

Do not repair implementation under this authority.

---

# Required success report

## Binding contract
Exact path.

## Entry Git state
Branch/HEAD/origin/ahead-behind.

## R1
Exact final path count and any accepted delta explanation.

## Security
Exact result.

## Branch
Exact branch name and base.

## Staging
Exact staged path count; confirm exact R1 only.

## Commit
SHA and message.

## Push
Remote branch/upstream result.

## PR
Number/URL, title, base/head, state, commit, changed-path count.

## Preservation
#233–#237 unchanged; milestone #58 Open; no merge/tag/Release.

## Mutation markers

`RELEASE 1.9 PR-CREATION REPOSITORY MUTATIONS: ZERO beyond pre-existing R1 content`

`RELEASE 1.9 PR-CREATION GIT MUTATIONS: <exact branch/stage/commit/push actions>`

`RELEASE 1.9 PR-CREATION GITHUB MUTATIONS: ONE PR CREATED; ALL OTHER GITHUB MUTATIONS ZERO`

## Next authority

`RELEASE 1.9 PR CREATED — SEPARATE PR REVIEW/CHECKS/MERGE AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 PR CREATION EXECUTION COMPLETE`

---

# Required blocked report

Include:

- exact completed gates;
- exact blocker;
- exact mutation state so far;
- safest next authority.

Mutation accounting must be exact.

Terminal:

`RELEASE 1.9 PR CREATION EXECUTION BLOCKED`

Do not emit COMPLETE unless the canonical PR exists with exact intended Release 1.9 change-set and no forbidden lifecycle action occurred.

# Release 1.10 — Remote Base & Publication-Authority Artifact Reconciliation Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations, publication; NOT selected here.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

# Purpose

Reconcile exactly two blockers discovered by the Release 1.10 Git Candidate Publication & Pull Request Authority:

1. WP08 froze candidate base `35ec644576275570aee522872c770e6c06e7879d`, while authoritative remote `main` is now `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`.
2. WP08 froze 101 paths (21 tracked + 80 untracked; 68 prompt artifacts), while current candidate is 103 paths (21 tracked + 82 untracked; 70 prompt artifacts), with the two added paths expected to be the Git-publication authority prompt pair.

This authority must eliminate all Terra choice about:
- canonical publication base/ancestry;
- whether/how the candidate is re-anchored;
- canonical candidate path count;
- exact classification of the two added authority artifacts;
- exact resumed staging/publication boundary.

# Entry state

Accepted evidence:
- #242–#249 Closed.
- Project items Done.
- milestone #59 Open, 0 open / 8 closed.
- publication attempt made ZERO Git/GitHub mutations.
- no staging, branch creation, commit, push, PR, milestone, tag, or GitHub Release mutation occurred.
- WP08 validation acceptance remains the latest accepted validation baseline unless reconciliation proves it invalidated.

Emit:
`RELEASE 1.10 REMOTE-BASE/PUBLICATION-ARTIFACT RECONCILIATION ENTRY: PASS`

# Mutation boundary

This is a Luna reconciliation authority.

Allowed repository mutations:
- Release 1.10 planning/governance artifacts only, and only when required to persist the reconciliation:
  - `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
  - `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
  - `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md` ONLY for a direct contradiction that cannot be resolved elsewhere; strongly avoid.

Forbidden:
- production changes;
- test changes;
- WP07 docs changes;
- package/project/schema/signing changes;
- staging;
- branch creation;
- commits;
- push;
- PR create/update;
- issue/Project/milestone mutations;
- tags;
- GitHub Release publication;
- history rewrite/rebase/merge/cherry-pick.

Git mutations: ZERO.
GitHub mutations: ZERO.

# Phase 1 — Read authoritative state

Read:
1. Release 1.10 definition/plan/manifest.
2. WP08 freeze/handoff evidence.
3. blocked Git/PR publication authority and result.
4. `git status --short`
5. `git status --branch`
6. `git rev-parse HEAD`
7. `git rev-parse main`
8. `git rev-parse origin/main`
9. `git log --graph --decorate --oneline --all` sufficient to establish ancestry.
10. merge-base between `35ec644576275570aee522872c770e6c06e7879d` and `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`.
11. exact commits in `35ec..5cc2` and their changed paths.
12. complete current tracked/untracked candidate inventory.
13. exact two added publication-authority artifact paths.
14. GitHub state for #242–#249, milestone #59, Project #2, and any Release 1.10 PR/branch if present.

Emit:
`RELEASE 1.10 REMOTE-BASE ANCESTRY INVENTORY: COMPLETE`

# Phase 2 — Canonical remote-base relationship

Prove the exact relationship between:
- WP08 frozen base `35ec644576275570aee522872c770e6c06e7879d`
- authoritative remote main `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Classify exactly one:
A. `35ec` is ancestor of `5cc2`.
B. `5cc2` is ancestor of `35ec`.
C. divergent histories.
D. another provable relationship requiring explicit explanation.

For commits after `35ec` through `5cc2`, classify every changed path as:
- unrelated predecessor/governance work already canonical on main;
- overlapping Release 1.10 path;
- conflicting Release 1.10 path;
- other.

Do not assume that a clean ancestry relationship automatically authorizes rebasing.

Emit:
`RELEASE 1.10 REMOTE-BASE RELATIONSHIP: FROZEN`

# Phase 3 — Publication base policy decision

Freeze one deterministic publication policy.

Preferred policy when evidence permits:
- authoritative PR base is current `origin/main` at `5cc2...`;
- Release 1.10 candidate must be materialized on top of that canonical base without publishing a commit whose parent is stale `35ec...`;
- Terra must use a clean deterministic mechanism that preserves candidate content and does not overwrite canonical main work.

But Luna must choose based on evidence.

Specify literally:
- canonical base SHA;
- required candidate parent SHA;
- whether Terra may create a branch from current `origin/main`;
- whether existing dirty Release 1.10 working-tree changes may be carried onto that branch directly;
- whether any rebase/merge/cherry-pick is required, allowed, or forbidden;
- exact conflict behavior;
- whether full validation must be rerun after re-anchoring and which gates;
- whether WP08 acceptance remains valid as semantic evidence.

No implementation choice may remain.

Emit:
`RELEASE 1.10 PUBLICATION BASE POLICY: FROZEN`

# Phase 4 — Two added authority artifacts

Identify the exact two paths created after WP08 freeze by the Git-publication authority.

Expected identities:
- `release-1.10-git-candidate-publication-pull-request-authority-codex-prompt.md`
- `release-1.10-git-candidate-publication-pull-request-authority-chat-bootstrap.md`

Use actual repository paths, not guessed paths.

For each, freeze:
- exact path;
- created-after-WP08 status;
- governance purpose;
- whether it belongs in the Release 1.10 published candidate;
- whether it is staging-authorized;
- whether it changes semantic release implementation;
- ownership category.

Choose exactly one canonical policy:
A. Include both, making canonical candidate 103 paths / 70 prompt artifacts.
B. Exclude both from publication while preserving them locally, keeping canonical published candidate 101 paths / 68 prompt artifacts.
C. Another exact policy justified by repository governance.

The pair must not be split unless repository evidence proves distinct ownership.

Emit:
`RELEASE 1.10 PUBLICATION-AUTHORITY ARTIFACT CLASSIFICATION: FROZEN`

# Phase 5 — Canonical candidate reconciliation

Freeze the canonical publication candidate after applying Phase 3 and Phase 4.

State literally:
- canonical base SHA;
- canonical candidate path count;
- tracked changed count;
- untracked/addition count before staging;
- authority-prompt artifact count;
- whether the two publication authority files are IN or OUT;
- exact expected candidate commit parent;
- exact excluded-local path set, if any;
- exact rule for pre-existing unrelated local work.

If the base change alters tracked/untracked representation without altering semantic paths, document both:
- pre-publication working-tree representation;
- canonical staged/commit path set.

Produce an exhaustive literal path list or bind to an existing manifest section that contains an exhaustive literal list. Wildcards/generic phrases are forbidden.

Emit:
`RELEASE 1.10 CANONICAL PUBLICATION CANDIDATE: FROZEN`

# Phase 6 — Overlap/conflict simulation

Simulate Terra materialization against canonical `origin/main`.

For every Release 1.10 tracked changed path, determine whether moving from `35ec` to `5cc2` causes:
- no overlap;
- identical upstream change;
- compatible overlap;
- conflict.

If any semantic conflict requires design judgment, BLOCK for a narrower Luna reconciliation.

Question:
“Can Terra construct the exact canonical candidate on the reconciled base without choosing content policy?”

Required answer: YES.

Emit:
`RELEASE 1.10 RE-ANCHOR MATERIALIZATION SIMULATION: PASS — TERRA-READY`

# Phase 7 — Validation consequence

Freeze exact validation required after candidate re-anchoring.

At minimum decide whether Terra must rerun:
- focused WP06 permanent suites;
- full .NET;
- full Python;
- build;
- Streamlit/pip check;
- Gitleaks;
- docs/diff check;
- schema/package audit;
- residue.

If base movement changes executable or governed files relevant to Release 1.10, require full validation.

State whether counts are expected baselines or exact required counts.

Emit:
`RELEASE 1.10 RECONCILED PUBLICATION VALIDATION POLICY: FROZEN`

# Phase 8 — Planning artifact reconciliation

Persist the decision into the minimum allowed planning artifacts.

The manifest must contain:
- canonical base SHA;
- canonical candidate path boundary/count;
- classification of the two publication-authority artifacts;
- exact Terra staging boundary.

The execution plan must contain:
- re-anchor/materialization procedure;
- validation consequence;
- publication resumption handoff.

Do not mutate Definition unless unavoidable.

Emit:
`RELEASE 1.10 PUBLICATION PLANNING RECONCILIATION: PASS`

# Phase 9 — Terra resumption contract

Produce an exact handoff to the existing:

**Release 1.10 — Git Candidate Publication & Pull Request Authority — GPT-5.6 Terra**

The handoff must state:
- selected model Terra;
- canonical base SHA;
- exact branch starting point;
- exact candidate path count/set;
- publication-authority prompt pair IN/OUT;
- exact staging rule;
- exact commit parent;
- exact validation required;
- PR base;
- no merge;
- no milestone closure;
- no tag/version;
- no GitHub Release.

If the existing authority contains stale literals (101 paths or `35ec`) that conflict with the reconciliation, state explicitly that the reconciled manifest/plan and this Luna handoff supersede only those named stale literals. All other Terra authority constraints remain binding.

Emit:
`RELEASE 1.10 RECONCILIATION → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`

# Phase 10 — Mutation audit

Report exact mutations:
- planning paths changed;
- Definition changed or not;
- production/test/docs/package/project/schema/signing: ZERO;
- Git: ZERO;
- GitHub: ZERO.

Verify #242–#249 remain Closed/Done and milestone #59 remains Open 0/8.

Emit:
`RELEASE 1.10 REMOTE-BASE/PUBLICATION-ARTIFACT RECONCILIATION MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Required success markers

`RELEASE 1.10 REMOTE-BASE/PUBLICATION-ARTIFACT RECONCILIATION ENTRY: PASS`
`RELEASE 1.10 REMOTE-BASE ANCESTRY INVENTORY: COMPLETE`
`RELEASE 1.10 REMOTE-BASE RELATIONSHIP: FROZEN`
`RELEASE 1.10 PUBLICATION BASE POLICY: FROZEN`
`RELEASE 1.10 PUBLICATION-AUTHORITY ARTIFACT CLASSIFICATION: FROZEN`
`RELEASE 1.10 CANONICAL PUBLICATION CANDIDATE: FROZEN`
`RELEASE 1.10 RE-ANCHOR MATERIALIZATION SIMULATION: PASS — TERRA-READY`
`RELEASE 1.10 RECONCILED PUBLICATION VALIDATION POLICY: FROZEN`
`RELEASE 1.10 PUBLICATION PLANNING RECONCILIATION: PASS`
`RELEASE 1.10 RECONCILIATION → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`
`RELEASE 1.10 REMOTE-BASE/PUBLICATION-ARTIFACT RECONCILIATION MUTATION AUDIT: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 — REMOTE BASE & PUBLICATION-AUTHORITY ARTIFACT RECONCILIATION AUTHORITY COMPLETE`

# Blocked outcome

BLOCK if:
- ancestry cannot be established;
- remote state is moving/ambiguous;
- the two added files cannot be identified exactly;
- canonical inclusion/exclusion cannot be determined from governance;
- re-anchoring creates a semantic conflict requiring broader design;
- candidate path ownership remains non-deterministic;
- a planning mutation beyond the allowed boundary is required.

On BLOCK:
- no Git/GitHub mutation;
- preserve all local candidate work;
- report the minimum unresolved governance choice.

Exact blocked terminal:

`RELEASE 1.10 — REMOTE BASE & PUBLICATION-AUTHORITY ARTIFACT RECONCILIATION AUTHORITY BLOCKED`

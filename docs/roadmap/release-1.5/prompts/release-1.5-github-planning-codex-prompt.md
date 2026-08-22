# Release 1.5 GitHub Planning Authority

## Phase 4 — Release 1.5: Deterministic Research Experiment Foundation

## 1. Authority

This prompt is the authoritative GitHub-planning instruction for Release 1.5 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 has already completed and passed the planning-definition step. The following post-definition artifacts are therefore expected, separately authorized, and MUST NOT be treated as premature or contradictory:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `prompts/release-1.5-github-planning-codex-prompt.md`
- `prompts/release-1.5-github-planning-codex-prompt-chat.md`

The earlier `release-1.5-planning-definition-codex-prompt.md` and its companion are historical execution inputs only. Their original pre-definition starting-state constraints are superseded for this GitHub-planning step and MUST NOT be reapplied.

The accepted Release 1.5 definition is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected capability:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

Read the definition, execution plan, and file manifest completely before any GitHub mutation. Reconcile this authority against them. If this prompt conflicts materially with those accepted artifacts, stop and report the conflict rather than inventing a resolution.

---

## 2. Objective

Establish the exact GitHub lifecycle structure required to execute Release 1.5 WP01–WP13.

The intended final planning state is:

- existing milestone #46 represents Release 1.5;
- exactly 13 Release 1.5 work-package issues exist;
- every WP belongs to milestone #46;
- every WP is present in GitHub Project #2;
- established Priority, Release, Area, and Status conventions are applied;
- all WPs begin Open / Backlog;
- dependencies and sequencing match the accepted execution plan;
- WP01 implementation does not begin;
- no Release 1.6 work begins;
- no repository content is staged, committed, pushed, or otherwise transported.

---

## 3. Accepted Temporal State

This authority executes AFTER:

1. Release 1.4 formal closure;
2. legacy milestone #44 reconciliation;
3. Release 1.5 planning and definition;
4. creation of `RELEASE_1.5_EXECUTION_PLAN.md`;
5. creation of `RELEASE_1.5_FILE_MANIFEST.md`;
6. creation of this GitHub-planning authority pair.

Therefore, the presence of those accepted Release 1.5 planning/governance artifacts is required/expected and is not a blocker.

Do not rerun or enforce the historical planning-definition authority's rule that the execution plan, manifest, or GitHub-planning authority must not yet exist.

---

## 4. Starting Baseline to Reconcile

Expected repository baseline:

- branch: `main`;
- accepted HEAD: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- `HEAD == origin/main`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- accepted Release 1.5 planning/governance artifacts may be untracked;
- no Release 1.5 production or test implementation exists;
- SQLite schema: version 2;
- permanent test baseline: 214;
- Architecture.Tests baseline: 13.

Expected predecessor lifecycle:

- Release 1.3 PR #152: MERGED;
- Release 1.4 PR #167: MERGED;
- milestone #44: CLOSED;
- milestone #45: CLOSED;
- milestone #54: CLOSED;
- issues #138–#151: Closed/Done;
- issues #153–#166: Closed/Done.

Expected Release 1.5 starting lifecycle:

- milestone #46: OPEN and empty;
- Release 1.5 issues: `0`;
- Release 1.5 implementation: none;
- Release 1.5 integration branch/PR: none.

Read these facts back from Git and GitHub. Do not assume them.

If the actual state materially conflicts with this accepted temporal baseline, stop before mutation and report the smallest corrective authority required.

---

## 5. Accepted Working-Tree Classification

Before GitHub mutation, classify the working tree.

The following Release 1.5 artifacts are accepted planning/governance inputs and must not be classified as premature implementation merely because they are untracked:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `prompts/release-1.5-github-planning-codex-prompt.md`
- `prompts/release-1.5-github-planning-codex-prompt-chat.md`

If the planning-definition authority pair is also present locally, classify it according to the accepted Release 1.5 manifest/governance rules. Do not silently add it to the governed candidate or delete it unless an accepted authority explicitly permits that action.

Unexpected production code, tests, schema changes, package/reference changes, Worker behavior, or other Release 1.5 implementation remain blockers.

This authority does not authorize repository-file cleanup.

---

## 6. Authentication and Repository Protection

Before mutation:

1. Verify authenticated GitHub account.
2. Verify target repository exactly:
   `samuel-santos-engineer/AIQuantTradingResearch`.
3. Verify default branch is `main`.
4. Verify local `main` synchronization.
5. Verify there is no active Release 1.5 implementation branch or PR.
6. Verify milestone #46 is empty unless exact partial output from this authority already exists.
7. Verify no Release 1.6 implementation/planning lifecycle has begun unexpectedly.

Do not mutate any other repository.

---

## 7. Milestone #46 Reconciliation

Reuse milestone #46. Do not create a replacement milestone.

Reconcile its title to:

`Phase 4 - Release 1.5: Deterministic Research Experiment Foundation`

Use the repository's established punctuation/casing convention if an equivalent form is already used.

Reconcile its description to:

`Establish one deterministic offline research experiment over accepted simple-return feature evidence, producing immutable count, arithmetic mean, minimum, and maximum evidence with canonical experiment identity and provenance.`

Required state after planning:

`OPEN`

Do not invent a due date.

If #46 already has a due date, preserve it unless the accepted Release 1.5 artifacts explicitly require otherwise.

Do not modify closed milestones #44, #45, or #54.

---

## 8. Exact Work-Package Set

Create or reconcile exactly these 13 issues, in this order.

### WP01 — Release & Repository Preflight

**Title:** `Release 1.5 WP01 — Release & Repository Preflight`

Purpose: verify the closed Release 1.4 baseline, exact Release 1.5 planning state, repository synchronization, schema v2, dependency graph, absence of premature implementation, and canonical technical baseline.

Dependency: accepted Release 1.5 planning and closed Release 1.4.

Recommended model: Luna.

### WP02 — Experiment Semantic Discovery

**Title:** `Release 1.5 WP02 — Experiment Semantic Discovery`

Purpose: freeze exact `simple-return-descriptive-summary-v1` semantics, including Feature Set input, count, arithmetic mean, minimum, maximum, empty-result behavior, decimal arithmetic, determinism, equivalence, evidence validity, and exclusions.

Dependency: WP01.

Recommended model: Sol.

### WP03 — Experiment Identity, Provenance & Evidence

**Title:** `Release 1.5 WP03 — Experiment Identity, Provenance & Evidence`

Purpose: freeze `aiq-experiment-identity-v1`, distinct Experiment Definition and Experiment Result identities, canonical SHA-256 encoding, exact Feature Set binding, empty-result identity, provenance, acyclic lineage, equivalence, integrity contradiction, and evidence-established-only rules.

Dependency: WP02.

Recommended model: Sol.

### WP04 — Experiment Model & Contracts

**Title:** `Release 1.5 WP04 — Experiment Model & Contracts`

Purpose: implement the minimum immutable Application-owned experiment model and contract surface, including the minimum canonical `aiq-experiment-identity-v1` computation required to construct valid experiment evidence.

Dependency: WP03.

Recommended model: Terra.

### WP05 — Deterministic Summary Computation

**Title:** `Release 1.5 WP05 — Deterministic Summary Computation`

Purpose: implement deterministic decimal-only count, arithmetic mean, minimum, and maximum computation for `simple-return-descriptive-summary-v1`, including successful empty results and canonical result construction using WP04 identity machinery.

Dependency: WP04.

Recommended model: Terra.

### WP06 — Experiment Validation & Failure Semantics

**Title:** `Release 1.5 WP06 — Experiment Validation & Failure Semantics`

Purpose: implement deterministic Application-owned validation, first-failure precedence, bounded failure distinctions, fail-stop behavior, no fabricated downstream evidence, and unknown-defect propagation.

Dependency: WP05.

Recommended model: Sol.

### WP07 — Feature-to-Experiment Integration

**Title:** `Release 1.5 WP07 — Feature-to-Experiment Integration`

Purpose: integrate exact experiment requests with the existing Release 1.4 feature-generation boundary, validate returned Feature Set evidence, execute exactly one summary computation, and return immutable experiment evidence without provider or persistence coupling.

Dependency: WP06.

Recommended model: Terra.

### WP08 — Dependency Registration & Configuration

**Title:** `Release 1.5 WP08 — Dependency Registration & Configuration`

Purpose: register Release 1.5 Application services and establish minimal explicit experiment execution configuration while preserving side-effect-free composition and the existing dependency graph.

Dependency: WP07.

Recommended model: Terra.

### WP09 — One-Shot Worker Experiment Execution

**Title:** `Release 1.5 WP09 — One-Shot Worker Experiment Execution`

Purpose: expose one explicit bounded Worker experiment mode that invokes the experiment use case exactly once, presents structured semantic evidence, preserves existing pipeline/feature modes, and terminates deterministically.

Dependency: WP08.

Recommended model: Terra.

### WP10 — Application Experiment Tests

**Title:** `Release 1.5 WP10 — Application Experiment Tests`

Purpose: add permanent deterministic offline Application coverage for experiment identities, canonical fingerprints, summary computation, empty/non-empty behavior, provenance, equivalence, validation, failures, integration, immutability, and unknown-exception propagation.

Dependency: WP09.

Recommended model: Luna.

### WP11 — Composition & Worker Validation

**Title:** `Release 1.5 WP11 — Composition & Worker Validation`

Purpose: add permanent offline composition and black-box Worker validation covering DI, lifetimes, side-effect-free resolution, successful experiment processes, equivalent recomputation, empty behavior, bounded failures, provider isolation, absence of experiment persistence, and cleanup.

Dependency: WP10.

Recommended model: Terra.

### WP12 — Architecture & Documentation Alignment

**Title:** `Release 1.5 WP12 — Architecture & Documentation Alignment`

Purpose: reconcile Release 1.5 with stable architecture rules and current-state documentation, preferring zero Architecture.Tests delta when existing rules already enforce all stable structural boundaries.

Dependency: WP11.

Recommended model: Terra.

### WP13 — Full Validation, Integration & Acceptance

**Title:** `Release 1.5 WP13 — Full Validation, Integration & Acceptance`

Purpose: reconcile the exact Release 1.5 candidate, run complete semantic/technical/regression/security acceptance, create one validated integration commit, prove fresh-checkout reproducibility, push normally, and create one review-ready PR without merging it or closing milestone #46.

Dependency: WP12.

Recommended model: Sol.

---

## 9. Issue Body Contract

Each issue body must include, concisely:

- Release 1.5 name;
- WP number/title;
- purpose;
- immediate predecessor/dependency;
- governing Release 1.5 definition, execution plan, and file manifest;
- scope boundary;
- major exclusions;
- completion expectation;
- recommended model.

Do not duplicate the full planning artifacts inside issue bodies.

Do not invent implementation semantics beyond the accepted authorities.

---

## 10. Labels and Existing Taxonomy

Inspect existing labels before mutation.

Reuse the established conventions from recent releases, including:

- Release classification;
- P1 priority;
- Area classification.

Do not create duplicate/equivalent labels.

If prior releases use release-specific labels and `Release 1.5` does not yet exist, creating the minimal matching Release 1.5 label is authorized.

Do not rename unrelated labels.

Repository truth takes precedence over guessed label names.

---

## 11. GitHub Project #2

Add all WP01–WP13 issues to:

`AIQuantTradingResearch Engineering Roadmap`

Project #2.

For every WP, reconcile established fields to:

- Status: `Backlog`;
- Priority: `P1`;
- Release: `Release 1.5`, when this field exists;
- Area: closest established value matching ownership.

Do not move WP01 to In Progress.

Do not modify unrelated Project items.

Suggested ownership mapping, subject to existing Project taxonomy:

| WP | Ownership |
|---|---|
| WP01 | Governance / Engineering |
| WP02 | Data / Architecture |
| WP03 | Data / Architecture |
| WP04 | Application |
| WP05 | Application |
| WP06 | Application |
| WP07 | Application |
| WP08 | Application / composition |
| WP09 | Worker |
| WP10 | Testing |
| WP11 | Testing / Infrastructure |
| WP12 | Architecture / Documentation |
| WP13 | Governance / Engineering |

Use existing values rather than creating a parallel taxonomy.

---

## 12. Dependency Order

The exact execution sequence is:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13`

Represent the immediate predecessor in each issue body using established repository convention.

Do not invent GitHub-native dependency relationships if the repository does not already use them.

---

## 13. Release Boundary Protection

Planning must preserve:

- exactly one built-in experiment: `simple-return-descriptive-summary-v1`;
- input from accepted `simple-return-lag-1-v1` Feature Set evidence;
- immutable count/mean/min/max result evidence;
- `aiq-experiment-identity-v1`;
- Application ownership of experiment semantics;
- no experiment persistence;
- SQLite schema v2;
- no provider/network dependency;
- no generalized experiment engine;
- no feature persistence expansion;
- no notebooks/workspace;
- no strategies/signals/backtesting;
- no portfolio/risk;
- no AI/ML/explainability/MLOps;
- no scheduling/retries/recovery/checkpoints;
- no plugins/generalized DAGs;
- no Release 1.6 work.

Do not create deferred-capability issues.

---

## 14. Repository Mutation Protection

This authority permits GitHub planning mutations only.

Do not:

- edit repository content;
- edit the definition, plan, or manifest;
- edit production code;
- edit tests;
- edit documentation;
- create new repository artifacts;
- delete accepted planning artifacts;
- stage;
- commit;
- branch;
- push;
- create a PR;
- merge;
- tag;
- create a GitHub Release.

Existing accepted Release 1.5 planning/governance files must remain unchanged and unstaged.

---

## 15. Authorized GitHub Mutation Budget

Authorized mutations are limited to:

1. reconcile milestone #46 title/description;
2. create/reconcile exactly 13 WP issues;
3. associate all 13 with milestone #46;
4. apply established labels;
5. add all 13 to Project #2;
6. set established Project fields;
7. leave all 13 Open / Backlog.

No issue closure is authorized.

No issue In Progress transition is authorized.

No completion comments are authorized because no WP has executed.

---

## 16. Idempotency and Partial-Execution Rule

Search before creating.

If exact Release 1.5 planning objects already exist because this authority was partially executed:

- reuse them;
- reconcile missing metadata;
- do not duplicate issues;
- preserve existing issue numbers;
- report reused objects.

If unexpected Release 1.5 implementation work exists, stop unless it is clearly only partial GitHub-planning output from this authority.

Do not use destructive cleanup to manufacture the expected starting state.

---

## 17. Post-Mutation Validation

Read back all final GitHub state.

Required:

- milestone #46: OPEN;
- correct Release 1.5 title;
- open issues: 13;
- closed issues: 0;
- WP01–WP13: exactly once each;
- all 13 issues: OPEN;
- Project #2 membership: 13/13;
- Status Backlog: 13/13;
- P1: 13/13 under established convention;
- Release 1.5 classification: 13/13 under established convention;
- Area: 13/13;
- dependency order: exact;
- Release 1.6 issues/implementation: 0;
- predecessor milestones #44/#45/#54: CLOSED and unchanged;
- repository-content mutations: 0;
- staged paths: 0;
- commits/branches/pushes/PRs: 0.

Report the assigned issue number for every WP.

---

## 18. Technical Baseline Verification

After GitHub planning, run the canonical Release verification if available.

Expected baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- build warnings/errors: `0/0`;
- formatting: PASS;
- Gitleaks: PASS.

Also validate:

- `git diff --check`;
- `git diff --cached --check`;
- direct trailing-whitespace inspection of relevant untracked Release 1.5 planning/governance files;
- database/generated residue: 0.

No provider execution, live market-data access, or real credentials are required.

GitHub API/network activity strictly for governance is permitted.

---

## 19. Stop Conditions

Stop without further mutation if:

- authenticated repository is wrong;
- Release 1.4 closure cannot be reconciled;
- milestone #46 contains unrelated work;
- unexpected Release 1.5 implementation exists;
- an active Release 1.5 implementation branch/PR exists unexpectedly;
- accepted definition/plan/manifest materially disagree;
- required Project fields cannot be identified without guessing;
- duplicates cannot be safely avoided;
- predecessor release lifecycle would need mutation;
- repository content would need mutation;
- Release 1.6 work would need to begin.

Do NOT stop merely because the accepted execution plan, file manifest, or GitHub-planning authority pair exists. Their presence is explicitly authorized by this authority.

If blocked, state the smallest corrective authority required.

---

## 20. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. accepted temporal-state reconciliation;
4. authenticated repository/account;
5. Git baseline and working-tree classification;
6. predecessor release closure;
7. milestone #46 before/after;
8. WP01–WP13 issue-number mapping;
9. labels and Areas;
10. Project #2 membership/fields;
11. dependency reconciliation;
12. Release 1.5 scope/exclusion audit;
13. technical baseline;
14. repository mutation accounting;
15. GitHub mutation accounting;
16. findings/blockers;
17. final lifecycle state;
18. next authorized work package.

Explicitly distinguish accepted post-definition governance artifacts from implementation.

---

## 21. Required Terminal Marker

On success, end exactly with:

`RELEASE 1.5 GITHUB PLANNING COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP01 — Release & Repository Preflight`

Do not begin WP01.

If blocked, end with:

`RELEASE 1.5 GITHUB PLANNING BLOCKED`

and identify the smallest corrective authority required.

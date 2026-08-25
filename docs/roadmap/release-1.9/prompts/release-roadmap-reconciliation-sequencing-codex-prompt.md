# AIQuantTradingResearch — Roadmap Reconciliation & Release Sequencing — Codex Authority

## Mission
Reconcile the post–Release 1.8 roadmap for `samuel-santos-engineer/AIQuantTradingResearch`. This is planning/governance authority only; no product implementation is authorized.

## Immutable predecessor
Release 1.8 repository boundary: `0bffb508d1e5a716214ff3a92a8f8c1da4a44be0`.
Preserve milestone #56, issues #211–#223, Project #2 Release 1.8 state, schema v3, and all historical release state.

## Accepted canonical sequence
1. **1.9 — Real-Time Financial Data Visualization**
   - deterministic simulated/live-mock provider ticker;
   - incremental use of the existing pipeline;
   - Streamlit presentation adapter;
   - evolving financial charts;
   - existing feature outputs such as `simple-return-lag-1-v1`;
   - dataset snapshot/data-quality states;
   - no ML training and no broad observability platform.

2. **1.10 — OpenTelemetry & Pipeline Observability**
   - governed OpenTelemetry introduction;
   - pipeline/stage timing, throughput, provider behavior, persistence latency, failures and appropriate Python-boundary telemetry;
   - Streamlit `System Health` view;
   - no ML training.

3. **2.0 — Lightweight Machine Learning Evaluation**
   - one narrow deterministic ML hypothesis;
   - Logistic Regression via governed scikit-learn is the preferred initial candidate unless later definition finds a blocking reason;
   - temporal, not random, evaluation split;
   - baseline comparison, reproducible experiment identity and metrics;
   - experiment visualization;
   - no broad reusable ML platform and no strategy backtesting.

4. **2.1 — Machine Learning**
   - preserve the existing broader Machine Learning milestone/scope;
   - defer and renumber it; do not redesign it here.

5. **2.2 — Explainable AI**
   - preserve the existing Explainable AI milestone/scope;
   - defer and renumber it; do not redesign it here.

6. **2.3 — Backtesting**
   - new future milestone after Explainable AI;
   - high-level purpose only: evaluate decision policies/research outputs historically with explicit temporal integrity and trading assumptions;
   - do not create detailed WPs.

Canonical capability narrative:
`Acquire → Persist → Validate → Transform → Stream → Visualize → Observe → Learn → Explain → Backtest`

## Mandatory release-integration invariant
Establish prospectively beginning with Release 1.9:

> Release implementation must occur on a dedicated release/working branch. Completion and acceptance do not authorize direct integration into `main`. After acceptance, all governed release artifacts—including documentation—must be committed to the release branch, a PR must be opened against `main`, required verification must pass on the PR candidate, and only then may the PR be merged. The resulting `main` merge SHA becomes the immutable release repository boundary.

Direct push of release implementation/closure changes to `main` is prohibited except under separately authorized emergency/hotfix governance. Do not rewrite Release 1.8 history.

## Foundational-technology invariant
Preserve:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

OpenTelemetry therefore requires a governed selection record before Release 1.10 implementation.

## Mandatory starting-state gate
Before mutation verify:
- correct repository and remote;
- branch `main`;
- local HEAD and `origin/main`;
- Release 1.8 boundary reachable/reconciled;
- ahead/behind `0/0`;
- no merge/rebase/cherry-pick/revert;
- staged paths 0;
- no unexplained tracked/untracked changes.

Read authoritative GitHub state for:
- milestones #49, #50, #51, #56 and all open milestones;
- every issue assigned to #50/#51;
- Project #2;
- Project #2 Release field/options;
- existing future items/dependencies referencing 1.9, 2.0, Machine Learning or Explainable AI.

Stop if required GitHub read-back is blocked or rate-limited.

## Preservation snapshot
Fully inventory milestone #50 and #51 before mutation:
- exact title/description/state/due date;
- issue counts and memberships;
- issue labels/assignees;
- Project items and Release/Status/Priority/Area assignments;
- dependency relationships;
- repository references.

Expected semantic identities must be verified live:
- #50 = Machine Learning;
- #51 = Explainable AI.

Hard rules:
- never delete #50/#51;
- never recreate them merely to renumber;
- preserve issue numbers, history, labels, assignees, milestone membership and dependency semantics;
- stop if their live scope materially conflicts with the accepted resequencing.

## Target milestone mapping
Preserve identities:
- existing #50 → **Release 2.1: Machine Learning**;
- existing #51 → **Release 2.2: Explainable AI**.

Create exactly one new milestone for each:
- **Release 1.9: Real-Time Financial Data Visualization**;
- **Release 1.10: OpenTelemetry & Pipeline Observability**;
- **Release 2.0: Lightweight Machine Learning Evaluation**;
- **Release 2.3: Backtesting**.

Use established repository milestone-title conventions. Do not create detailed issues/WPs for these new milestones.

## Project #2 Release taxonomy
Required future Release options:
`1.9`, `1.10`, `2.0`, `2.1`, `2.2`, `2.3`.

Preserve historical options, IDs/assignments where possible, and completed historical items.

If existing `1.9`/`2.0` options exist, reuse them structurally rather than duplicating them. Add only missing options.

For proven future Machine Learning items:
- milestone remains #50;
- Project Release becomes `2.1`.

For proven future Explainable AI items:
- milestone remains #51;
- Project Release becomes `2.2`.

Preserve Status, Priority, Area, issue identity and dependency semantics. Do not create Project items for the new empty milestones.

If taxonomy extension cannot be safely performed with verified GitHub mechanisms, stop for narrow corrective authority.

## Roadmap documentation
Read all repository roadmap/navigation documents that express future release order. Update only what is necessary to make the canonical sequence truthful.

Clearly distinguish COMPLETE, NEXT, PLANNED and RESEQUENCED states.

Preserve unaffected long-term roadmap content such as Cloud/SRE, MLOps, Production, Risk/Portfolio and other governed future phases.

Do not create Release 1.9 definition, execution plan, file manifest, WP issues or implementation.

## Planning boundaries
This authority does not freeze:
- OpenTelemetry package/version/exporter/backend choices;
- Release 2.0 target variable, exact dataset/split windows/metrics/hyperparameters/schema;
- Backtesting APIs, costs, slippage, portfolio rules, metrics, persistence or UI.

Those require later release-specific definition authorities.

## Git/PR governance
After starting-state verification:
1. create a dedicated roadmap-reconciliation working branch using repository convention;
2. make only roadmap/governance documentation changes there;
3. perform authorized GitHub milestone/Project mutations conservatively and idempotently;
4. commit all governed reconciliation artifacts/Markdown;
5. push the branch;
6. open a PR against `main`;
7. run required PR-candidate verification;
8. merge only if every gate passes and the repository merge convention is unambiguous;
9. capture the resulting `main` merge SHA.

Direct push of reconciliation content to `main` is forbidden.

If this authority pair is repository-resident, both files must be committed:
- `release-roadmap-reconciliation-sequencing-codex-prompt.md`
- `release-roadmap-reconciliation-sequencing-codex-prompt-chat.md`

All governed Markdown created/modified by this reconciliation must be committed.

## Conservative GitHub mutation order
1. preservation snapshot;
2. add only missing Project Release taxonomy options;
3. reconcile #50/#51 titles/descriptions/version references;
4. reassign proven #50/#51 Project items to 2.1/2.2;
5. create 1.9 milestone;
6. create 1.10 milestone;
7. create 2.0 milestone;
8. create 2.3 milestone;
9. reconcile repository roadmap docs;
10. full read-back;
11. commit branch;
12. push;
13. PR;
14. PR validation;
15. merge;
16. final `main`/GitHub read-back.

Before every create, search for an existing matching object. Never duplicate milestones, Release options, items or documents. Stop on ambiguous partial state.

## Validation
Require:
- canonical sequence appears exactly once as current future ordering;
- #50 preserved and mapped to 2.1;
- #51 preserved and mapped to 2.2;
- new 1.9/1.10/2.0/2.3 milestones each exist exactly once;
- no detailed issues/WPs created for new milestones;
- Project Release taxonomy contains 1.9–2.3 as required;
- proven #50/#51 items are 2.1/2.2;
- historical assignments unchanged;
- only roadmap/governance repository content changed;
- production/test/package/dependency/schema/Python environment deltas are zero.

Run canonical build, full tests, format, Gitleaks, Markdown links, whitespace/conflict and diff checks. Expected accepted permanent baseline remains 281/281 unless independently changed by accepted later work.

## RR1–RR20
Report PASS/FAIL/NOT-APPLICABLE:
- RR1 Release 1.8 immutable predecessor reconciled;
- RR2 repository clean/synchronized starting state;
- RR3 #50 preservation snapshot;
- RR4 #51 preservation snapshot;
- RR5 future issues/items/dependencies inventoried;
- RR6 canonical target sequence reconciled without historical rewrite;
- RR7 Project Release taxonomy reconciled through 2.3;
- RR8 #50 preserved as Release 2.1 Machine Learning;
- RR9 #51 preserved as Release 2.2 Explainable AI;
- RR10 Release 1.9 milestone exactly once;
- RR11 Release 1.10 milestone exactly once;
- RR12 Release 2.0 milestone exactly once;
- RR13 Release 2.3 Backtesting milestone exactly once;
- RR14 no detailed WPs/issues for new milestones;
- RR15 repository roadmap/docs canonical;
- RR16 technology/ML/explainability/backtesting boundaries preserved;
- RR17 dedicated branch/PR integration invariant established and followed;
- RR18 all governed reconciliation Markdown committed;
- RR19 canonical engineering/document verification passes;
- RR20 PR merged only after validation and resulting `main` merge SHA captured.

All applicable gates must PASS.

## PR requirements
PR must explain:
- why visible platform capabilities are inserted before broad ML expansion;
- sequence: 1.9 Visualization → 1.10 Observability → 2.0 Lightweight ML Evaluation → 2.1 Machine Learning → 2.2 Explainable AI → 2.3 Backtesting;
- #50/#51 scope/history preserved;
- historical releases unchanged;
- no implementation;
- future release branch → acceptance → PR → verification → merge invariant.

## Merge gate
Merge only if:
- branch is synchronized with target;
- required checks and canonical verification pass;
- GitHub roadmap read-back matches authority;
- PR diff is governance/roadmap only;
- all governed Markdown is committed;
- no Release 1.9 implementation exists;
- no historical release state changed unexpectedly.

Use established merge method. If merge convention is ambiguous, stop for narrow merge authority. Never bypass checks or force-push `main`.

## Final read-back
After merge:
- verify remote `main` contains PR result;
- capture resulting `main` merge SHA;
- verify GitHub milestone/Project state;
- verify #49/#56 unchanged;
- verify no Release 1.9 WP issues exist.

The merge SHA is the immutable Roadmap Reconciliation / Release Sequencing boundary, not a Release 1.9 implementation boundary.

## Stop conditions
Stop with `ROADMAP RECONCILIATION & RELEASE SEQUENCING BLOCKED` on baseline ambiguity, #50/#51 semantic conflict, unsafe history loss, unsafe taxonomy mutation, rate limit/read-back failure, duplicate ambiguity, implementation requirement, new foundational decision, validation failure, non-governance PR content, or merge requiring bypass/invented convention.

Report exact blocker, partial mutations, preserved state and smallest corrective authority.

## Required report
Report:
- starting repository/branch/HEAD/origin/status;
- #50/#51 preservation snapshots;
- canonical sequence `1.9 → 1.10 → 2.0 → 2.1 → 2.2 → 2.3`;
- all milestone/Project mutations;
- all changed/created Markdown paths;
- zero implementation/package/schema mutation;
- build/tests/format/Gitleaks/links/diff results;
- RR1–RR20;
- branch/commit/PR/checks/merge;
- final `main` merge SHA and milestone map.

## Success markers
On success end exactly:

`ROADMAP RECONCILIATION & RELEASE SEQUENCING COMPLETE`

`CANONICAL NEXT RELEASE: 1.9 — REAL-TIME FINANCIAL DATA VISUALIZATION`

`FUTURE SEQUENCE: 1.9 → 1.10 → 2.0 → 2.1 MACHINE LEARNING → 2.2 EXPLAINABLE AI → 2.3 BACKTESTING`

`ROADMAP RECONCILIATION MAIN BOUNDARY: <merge SHA>`

`RELEASE BRANCH → ACCEPTANCE → PR → VERIFICATION → MERGE INVARIANT: ESTABLISHED`

`NEXT AUTHORIZED ACTION: Define Release 1.9 under a separate planning/definition authority.`

Do not begin Release 1.9 automatically.

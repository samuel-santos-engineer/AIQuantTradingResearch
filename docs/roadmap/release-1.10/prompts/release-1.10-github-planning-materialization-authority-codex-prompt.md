# Release 1.10 — GitHub Planning Materialization Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, architecture, scope, acceptance, governance, reconciliation, and read-only audit authority.
- **GPT-5.6 Terra** — implementation/execution authority, including approved Git/GitHub mutations and validation execution.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra for assigned authorities.

**Selected execution model: GPT-5.6 Terra.**

---

# Purpose

Materialize the **accepted Release 1.10 planning baseline** into GitHub planning objects.

Canonical Release 1.10 capability:

> Governed OpenTelemetry-based pipeline/boundary observability plus a truthful Streamlit System Health view.

This authority may create/update GitHub planning metadata only.

It MUST NOT implement Release 1.10.

---

# Canonical planning sources

Read these local accepted artifacts first:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

These are the authoritative source for:

- eight work packages;
- exact WP titles;
- objectives;
- scope/non-scope;
- dependencies;
- acceptance criteria;
- validation requirements;
- security requirements;
- path ownership;
- Luna/Terra/Sol model assignments;
- release exclusions.

Do NOT invent alternative work packages.

If the three artifacts disagree materially:
BLOCK before GitHub mutation.

---

# Accepted Release 1.10 boundaries

Preserve:

- .NET pipeline/business ownership;
- canonical governed JSON handoff;
- SQLite schema v4 unless the accepted plan explicitly says otherwise;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- existing no-bypass architecture.

Release 1.10 excludes:

- live providers;
- broker/exchange connectivity;
- trading/execution;
- ML;
- backtesting;
- parallel pipeline architectures;
- direct Streamlit/UI SQLite access;
- direct Python provider access;
- schema migration unless explicitly re-authorized;
- unselected telemetry dependencies;
- unrelated dependency modernization.

GitHub issue wording must not overclaim these exclusions.

---

# Predecessor state

Expected current remote `main` at accepted definition time:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Read current remote state authoritatively.

If `main` legitimately advanced, record it. Do not reset or mutate Git.

Preserve Release 1.9:

- `v1.9.0` -> `e4958721c9a581efbb2552134c00bc146c73f047`
- GitHub Release remains published
- milestone #58 remains Closed 0/13
- #233–#237 remain Closed / Done
- PR #240 remains Merged
- PR #241 remains Merged

---

# Absolute mutation boundary

## Repository content mutations

ZERO.

Do not edit the three accepted planning artifacts.

Do not edit source, tests, schema, packages, config, prompts, README, or roadmap files.

## Git mutations

ZERO.

Do not:

- fetch if the execution environment treats fetch as a prohibited Git mutation;
- checkout/switch;
- branch;
- stage;
- commit;
- amend;
- merge;
- rebase;
- push;
- tag;
- delete refs.

Use GitHub/API reads for remote truth where necessary.

## GitHub mutations

Only the minimum planning mutations explicitly authorized below:

1. update milestone #59 description only if required to materialize the accepted definition;
2. create exactly the eight Release 1.10 WP issues if they do not already exist;
3. assign those eight issues to milestone #59;
4. add/configure those eight issues in the canonical repository Project;
5. set existing canonical Project fields required by the accepted plan, including Release = `1.10` and appropriate planning Status if those fields already exist;
6. encode dependency relationships using the repository's established mechanism if supported and already conventional.

No PR creation.
No merges.
No Release/tag mutation.
No issue closure.
No milestone closure.
No implementation.

---

# Phase 0 — Read-only entry audit

Before mutation, inspect and record:

- repository identity;
- default branch;
- current remote `main` SHA;
- milestone #59;
- milestone #59 current title/description/state/counts;
- all existing issues currently assigned to #59;
- canonical Project identity;
- Project fields;
- Release taxonomy options;
- Status options;
- existing Release 1.10 Project items;
- existing issues whose titles/bodies could duplicate the eight accepted WPs.

State:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION ENTRY AUDIT: COMPLETE`

Do not mutate until duplicate/idempotence analysis is complete.

---

# Phase 1 — Reconcile milestone #59

Expected accepted milestone identity:

- number: `#59`
- state: Open
- current planning baseline at definition: 0 open / 0 closed
- theme: OpenTelemetry / Pipeline Observability

Read its exact title and description.

Compare with `RELEASE_1.10_DEFINITION.md`.

If the title is already canonically correct, preserve it.

Update the description only if needed to express the accepted Release 1.10 definition faithfully.

The milestone description should concisely capture:

- governed OpenTelemetry-based pipeline/boundary observability;
- truthful Streamlit System Health;
- preservation of existing architecture/provenance boundaries;
- key exclusions;
- pointer to the eight-WP plan conceptually.

Do not close milestone #59.

---

# Phase 2 — Extract exact eight-WP manifest

From the accepted execution plan, extract all eight work packages.

Before creating anything, print a deterministic table containing for each WP:

- WP identifier;
- exact issue title;
- objective;
- dependencies;
- selected GPT-5.6 execution model;
- expected path ownership;
- acceptance summary.

Require exactly:

`8`

State:

`RELEASE 1.10 WORK-PACKAGE MANIFEST: 8/8 EXTRACTED FROM ACCEPTED PLAN`

If count != 8:
BLOCK.

Do not invent a ninth WP.

---

# Phase 3 — Duplicate/idempotence gate

Search GitHub for issues matching each accepted WP by:

- exact title;
- Release 1.10 identifier;
- semantic equivalent.

Classify each WP as:

- `ABSENT — CREATE`
- `EXACT EXISTING — REUSE`
- `CONFLICTING EXISTING — BLOCK`

If an exact existing issue already represents a WP:
reuse it rather than create a duplicate, but verify its body/milestone/Project metadata against the accepted plan.

If a conflicting issue exists:
BLOCK before creating duplicates.

Report the planned mutation count before execution.

---

# Phase 4 — Issue contract template

Each Release 1.10 WP issue must faithfully encode the accepted plan.

Use repository issue conventions where present.

Each body must contain, at minimum:

## Release
`Release 1.10`

## Work package
WP identifier and title.

## Model assignment
Explicitly define:

- GPT-5.6 Luna — contract/planning/reconciliation role;
- GPT-5.6 Terra — implementation/execution/mutation role;
- GPT-5.6 Sol — supporting analysis/review role;
- selected execution model for this WP.

## Objective
Exact accepted objective.

## Scope
Accepted in-scope work only.

## Non-scope
Explicit exclusions relevant to the WP.

## Architecture contract
Applicable ownership/boundary/no-bypass rules.

## Data provenance / truthfulness
Applicable provenance restrictions and truthful UI/telemetry claims.

## Dependencies
Exact predecessor WP(s), or `None` if foundational.

## Expected repository areas
Use the accepted file/path ownership plan.

This is planning guidance, not permission to mutate those files now.

## Acceptance criteria
Measurable accepted criteria.

## Validation
Expected later validation commands/categories from the accepted plan.

## Security
Relevant security constraints.

## Completion boundary
State that implementation requires a separate WP execution authority.

Do not add speculative implementation details absent from the accepted planning artifacts.

---

# Phase 5 — Create/reconcile eight issues

Create only WPs classified `ABSENT — CREATE`.

For reused exact issues, modify only if necessary to reconcile them exactly with the accepted planning baseline and only within the authorized planning scope.

Assign every WP issue to milestone #59.

Do not close any WP issue.

Expected final milestone issue state immediately after materialization:

- 8 open WP issues;
- 0 closed WP issues,

unless pre-existing exact issues have a legitimate different state that requires Luna reconciliation. If so, BLOCK rather than silently changing lifecycle state.

---

# Phase 6 — Project materialization

Use the canonical repository Project already used by prior releases.

Do not create a new Project.

For all eight WP issues:

- add to Project if absent;
- preserve existing item identity if already present;
- set Release field to canonical option `1.10`;
- set Status to the repository's planning/not-started state consistent with prior releases;
- set other fields only when the accepted plan or established repository convention clearly requires them.

Do not create new Project fields/options unless the accepted planning baseline explicitly requires it.

Expected Release taxonomy already contains `1.10`.

If `1.10` is absent:
BLOCK and require a separate taxonomy reconciliation authority unless the accepted plan explicitly authorized taxonomy creation.

---

# Phase 7 — Dependency topology

Materialize or document the exact WP dependency graph from `RELEASE_1.10_EXECUTION_PLAN.md`.

Prefer the repository's established dependency mechanism.

If GitHub native issue dependencies/sub-issues are used by the repository, use them only if consistent with existing practice.

Otherwise encode dependencies explicitly in issue bodies.

Do not invent dependencies.

Verify:

- no cycle;
- all dependency references resolve to one of the eight WPs or an explicitly accepted predecessor;
- implementation order matches the accepted execution plan.

State:

`RELEASE 1.10 WORK-PACKAGE DEPENDENCY TOPOLOGY: PASS`

---

# Phase 8 — Post-mutation authoritative read-back

After all authorized mutations, read back every object.

## Milestone #59

Verify:

- Open;
- exactly 8 open / 0 closed Release 1.10 WP issues;
- description matches accepted scope.

## Issues

For all eight verify:

- number;
- title;
- state = Open;
- milestone = #59;
- body contract present;
- model assignment present;
- dependencies correct.

## Project

For all eight verify:

- item exists exactly once;
- Release = `1.10`;
- Status = correct planning state;
- no duplicate Project items;
- no unintended field changes.

## Topology

Verify exact dependency graph.

---

# Phase 9 — Release 1.9 integrity read-back

Reconfirm:

- `v1.9.0` still targets `e4958721c9a581efbb2552134c00bc146c73f047`;
- GitHub Release remains published;
- milestone #58 remains Closed 0/13;
- #233–#237 remain Closed/Done;
- PR #240 remains Merged;
- PR #241 remains Merged.

No Release 1.9 object may be mutated.

---

# Phase 10 — Mutation accounting

Enumerate every GitHub mutation exactly.

Expected categories:

- milestone #59 description: zero or one update;
- WP issues: up to eight creates, or fewer if exact issues reused;
- milestone assignments: eight final assignments;
- Project additions: up to eight;
- Project field updates: only those required for Release/Status/planning convention;
- dependency metadata: only accepted topology.

Repository mutations:

`ZERO`

Git mutations:

`ZERO`

GitHub mutations must equal the declared planning materialization set and nothing else.

---

# Phase 11 — Next authority

If materialization passes, the next authority is NOT implementation automatically.

First identify the first WP from the accepted execution plan.

Then state the exact next authority as:

**Release 1.10 <WP-ID> implementation authority — GPT-5.6 Terra**

unless the accepted plan assigns that WP primarily to Luna.

That next authority must again include the explicit Luna/Terra/Sol model map.

Do not execute it here.

---

# Acceptance criteria

PASS only if:

1. three accepted planning artifacts were read and reconciled;
2. milestone #59 remains Open;
3. exact eight-WP manifest extracted;
4. duplicate/idempotence gate passes;
5. exactly eight canonical WP issues exist after materialization;
6. all eight are Open and assigned to #59;
7. issue bodies match accepted contracts;
8. every issue explicitly defines Luna/Terra/Sol and selected model;
9. all eight are represented exactly once in canonical Project;
10. Release = `1.10`;
11. Status fields match planning convention;
12. dependency topology matches accepted execution plan and is acyclic;
13. no repository or Git mutation occurs;
14. no implementation occurs;
15. Release 1.9 lifecycle remains unchanged;
16. exact next WP authority is identified.

---

# Required success report

## Model

`GPT-5.6 Terra`

and explicit Luna/Terra/Sol role map.

## Baseline

- remote `main`
- milestone #59 pre-state
- Project identity
- Release taxonomy verification

## Eight-WP manifest

Print all eight:

- WP ID
- issue number
- title
- selected model
- dependencies
- state
- milestone
- Project Release
- Project Status

Emit:

`RELEASE 1.10 WORK-PACKAGE MANIFEST: PASS — 8/8 MATERIALIZED`

## Milestone

Report final #59 title/state/counts.

`RELEASE 1.10 MILESTONE #59 MATERIALIZATION: PASS — 8 OPEN / 0 CLOSED`

## Project

`RELEASE 1.10 PROJECT MATERIALIZATION: PASS — 8/8 ITEMS`

## Dependency topology

`RELEASE 1.10 WORK-PACKAGE DEPENDENCY TOPOLOGY: PASS`

## Release integrity

Report Release 1.9 tag/Release/milestone/issues/PRs.

## Mutation accounting

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION REPOSITORY MUTATIONS: ZERO`

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION GIT MUTATIONS: ZERO`

Report exact GitHub mutation count and objects, then:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION GITHUB MUTATIONS: ACCEPTED PLANNING OBJECTS ONLY`

## Next authority

Print exact first WP implementation authority and model.

## Completion

`RELEASE 1.10 GITHUB PLANNING BASELINE: MATERIALIZED AND ACCEPTED`

Terminal:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION AUTHORITY COMPLETE`

---

# Blocked outcome

If any prerequisite or reconciliation gate fails:

- stop before unsafe/duplicate mutation;
- report exact phase;
- report planning artifact conflict if any;
- report milestone/Project/taxonomy mismatch;
- report duplicate/conflicting issues;
- report mutations already performed;
- identify smallest next Luna/Terra reconciliation authority.

Terminal:

`RELEASE 1.10 GITHUB PLANNING MATERIALIZATION AUTHORITY BLOCKED`

Never emit COMPLETE unless the authoritative post-mutation read-back passes.

# Phase 5 Documentation Amendment Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY: documentation governance, current-vs-historical classification, amendment contract, validation and acceptance.
- **GPT-5.6 Terra** — implementation/execution mutations only under separately authorized implementation scopes; not selected here.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

# Mission
Reconcile repository documentation with the newly accepted GitHub governance baseline in which open numbered product Release milestones use the **Phase 5** organizational prefix, while preserving genuine historical Phase 4 evidence and preserving the Phase 4 non-release Initiative-1.11 milestone.

This authority permits **documentation-only repository edits**.

It does NOT authorize any GitHub milestone, issue, Project, release-taxonomy, Azure, application, test, package, schema, or production mutation.

# Canonical governance baseline
Treat the following as binding current state:

- `main` and `origin/main` baseline at reconciliation:
  `fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`
  unless legitimate advancement is detected before execution.
- Milestone #50:
  `Phase 5- Release 2.1: Machine Learning`
  — Open.
- Milestone #51:
  `Phase 5 - Release 2.2: Explainable AI`
  — Open.
- Milestone #60:
  `Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`
  — Open.
- Milestone #61:
  `Phase 5 - Release 2.3: Backtesting`
  — Open.
- Milestone #62:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`
  — Open, 5 open / 1 closed.
- #252 Closed/Done.
- #253–#257 Open/Todo.
- Initiative Project Release fields unset.
- no Project Release `1.11` option exists.
- product sequence:
  `1.10 → 2.0 → 2.1 → 2.2 → 2.3`.
- `Initiative-1.11 ≠ Product Release 1.11`.
- Product Release 1.11 remains abandoned.

Required marker:
`PHASE 5 DOCUMENTATION BASELINE: ACCEPTED`

# Critical classification rule
Every Phase 4 reference encountered must be classified before editing.

Use exactly these classes:

1. `CURRENT-STATE STALE`
   - text currently claims an open numbered product Release milestone is Phase 4;
   - update to the verified Phase 5 current title/phase.

2. `HISTORICAL — PRESERVE`
   - records what was true at an earlier time;
   - appears in completed authority evidence, historical reconciliation narrative, audit log, prior terminal output, immutable release history, or a statement explicitly describing the former state;
   - do not rewrite.

3. `CURRENT — PRESERVE`
   - correctly refers to milestone #62 / Initiative-1.11 as Phase 4;
   - or otherwise remains correct.

4. `AMBIGUOUS — DO NOT EDIT`
   - cannot be safely classified without changing meaning;
   - report for follow-up.

Never perform global search-and-replace from `Phase 4` to `Phase 5`.

Required marker:
`PHASE 4/PHASE 5 REFERENCE CLASSIFICATION: COMPLETE`

# Scope discovery
Search at minimum:
- `README*`;
- `docs/roadmap/**`;
- initiative planning artifacts under:
  `docs/roadmap/initiatives/azure-f1-public-reference-deployment/**`;
- governance/planning documents that describe current milestone identities;
- tracked prompt/authority artifacts if they exist in the repository;
- other tracked Markdown/text governance files containing:
  - `Phase 4`;
  - `Phase 5`;
  - `Release 2.0`;
  - milestone `#60`;
  - `Lightweight Machine Learning Evaluation`;
  - `Initiative-1.11`;
  - milestone `#62`.

Do not edit generated files outside the repository working tree.

# Required amendment
Update only `CURRENT-STATE STALE` documentation references.

In particular, any current-state reference to milestone #60's former title such as:

`Phase 4 - Release 2.0: Lightweight Machine Learning Evaluation`

must become:

`Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation`

where and only where the reference describes current governance state.

If current-state documentation lists #51 or #61 with Phase 4, reconcile those to their verified Phase 5 titles.

For milestone #50, preserve GitHub's verified exact current title if a document is explicitly quoting GitHub:

`Phase 5- Release 2.1: Machine Learning`

However, if a document uses a normalized human-readable convention rather than claiming an exact GitHub-title quote, use:

`Phase 5 - Release 2.1: Machine Learning`

and clearly avoid claiming that normalized spacing is the exact GitHub title.

This authority does **not** authorize renaming milestone #50 on GitHub.

# Initiative preservation
Do not convert Initiative-1.11 documentation to Phase 5.

Current Initiative-1.11 references must preserve:

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Preserve:
`Initiative-1.11 ≠ Product Release 1.11`

Do not introduce:
- Product Release 1.11;
- Release option 1.11;
- `v1.11.0`;
- implication that Initiative-1.11 belongs to Phase 5 merely because numbered releases do.

Required marker:
`PHASE 4 INITIATIVE-1.11 DOCUMENTATION: PRESERVED`

# Historical evidence preservation
Do not rewrite historical records solely to make them resemble today's GitHub titles.

Examples to preserve when historical:
- earlier statements that milestone #60 was Phase 4;
- prior governance prompt text whose purpose was to reconcile Phase 4 → Phase 5;
- completed authority terminal output;
- evidence showing the previous title;
- immutable release-history narratives.

Where a historical document also contains an explicit "current state" section that is intended to stay live, only that live section may be amended if classification is unambiguous.

Required marker:
`HISTORICAL PHASE 4 EVIDENCE: PRESERVED`

# Repository mutation boundary
Allowed:
- edit the minimum number of tracked documentation/governance text files necessary to remove stale current-state references;
- whitespace/newline changes only when incidental and minimal.

Forbidden:
- source code;
- tests;
- solution/project files;
- package/dependency files;
- schemas;
- migrations;
- Docker/application deployment files;
- CI workflows unless the only content is clearly documentation and separately justified;
- binaries;
- generated artifacts;
- GitHub milestone/issue/Project mutations;
- Azure/registry/Twelve Data mutations.

Prefer the smallest possible diff.

# Git handling
This authority permits working-tree documentation edits but does **not** authorize publication by default.

Unless an existing higher authority explicitly authorizes Git publication:
- do not commit;
- do not push;
- do not create a PR;
- do not merge.

Staging is not required and should remain unchanged unless the execution environment has an established validation-only staging convention. Conservative default: do not stage.

Report the resulting working-tree diff exactly.

# Validation
After edits verify:

## Content
- no stale current-state claim remains that milestone #60 is Phase 4;
- current-state references to affected numbered open Release milestones reflect Phase 5;
- milestone #62 remains Phase 4 wherever current-state Initiative-1.11 is referenced;
- product sequence remains `1.10 → 2.0 → 2.1 → 2.2 → 2.3`;
- Product Release 1.11 is not introduced;
- Initiative-1.11 remains non-release;
- historical Phase 4 evidence is preserved.

## Diff
- only allowed documentation files changed;
- no production/source/test/config/package/schema files changed;
- no accidental mass replacement;
- no trailing whitespace introduced;
- Markdown links remain valid syntactically;
- no secrets or credentials introduced.

## Git/GitHub
- no commit;
- no push;
- no PR;
- no GitHub mutation.

Emit:
`PHASE 5 DOCUMENTATION AMENDMENT VALIDATION: PASS`

# Documentation drift disposition
After amendment, report:
- files changed;
- exact stale references corrected;
- historical references deliberately preserved;
- ambiguous references left unchanged.

Choose exactly one:

`PHASE 5 DOCUMENTATION DRIFT: RECONCILED`

or

`PHASE 5 DOCUMENTATION DRIFT: PARTIAL — AMBIGUOUS REFERENCES REQUIRE FOLLOW-UP`

A PARTIAL disposition may still require BLOCK if canonical current-state documentation remains materially misleading.

# Mutation audit
Report exact counts:
- repository documentation files edited;
- source files edited;
- test files edited;
- staging mutations;
- commits;
- pushes;
- PRs;
- merges;
- milestone mutations;
- issue mutations;
- Project mutations;
- Project Release mutations;
- Azure mutations;
- registry mutations;
- Twelve Data requests.

Expected:
- documentation files edited: minimum necessary;
- every other category: 0.

Emit:
`PHASE 5 DOCUMENTATION AMENDMENT MUTATION AUDIT: PASS`

# WP02 protection
This documentation amendment must not execute or materially change WP02 #253.

Verify:
- milestone #62 remains the governing milestone conceptually;
- WP02 remains pending empirical execution;
- Azure CLI/authentication/strict-$0 preflight remain prerequisites;
- no feasibility PASS is claimed.

Emit:
`AZURE F1 WP02 #253 EXECUTION BOUNDARY: PRESERVED`

# Required success markers
`PHASE 5 DOCUMENTATION BASELINE: ACCEPTED`
`PHASE 4/PHASE 5 REFERENCE CLASSIFICATION: COMPLETE`
`PHASE 4 INITIATIVE-1.11 DOCUMENTATION: PRESERVED`
`HISTORICAL PHASE 4 EVIDENCE: PRESERVED`
`PHASE 5 DOCUMENTATION AMENDMENT VALIDATION: PASS`
`PHASE 5 DOCUMENTATION DRIFT: RECONCILED`
`PHASE 5 DOCUMENTATION AMENDMENT MUTATION AUDIT: PASS`
`AZURE F1 WP02 #253 EXECUTION BOUNDARY: PRESERVED`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Success terminal
`PHASE 5 DOCUMENTATION AMENDMENT AUTHORITY COMPLETE`

# Block conditions
BLOCK if:
- current GitHub baseline materially differs from the accepted reconciliation and cannot be explained;
- Phase 4 references cannot be safely classified;
- required corrections would touch production/source/test/package/schema/application behavior;
- correcting documentation would require changing release identity rather than presentation metadata;
- Product Release 1.11 appears to have been introduced;
- a broad/global replacement would be necessary;
- the working tree contains conflicting pre-existing edits to the same required documentation sections and safe preservation cannot be guaranteed.

If blocked:
- make no speculative edits;
- report exact files/references;
- report any edits already made before the blocker;
- provide minimum next authority needed.

# Blocked terminal
`PHASE 5 DOCUMENTATION AMENDMENT AUTHORITY BLOCKED`

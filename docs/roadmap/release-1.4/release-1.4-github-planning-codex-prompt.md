# Release 1.4 GitHub Planning — Codex Authority

## Mission

Establish the GitHub planning objects for **Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation** from the accepted Release 1.4 definition, execution plan, and file manifest.

This authority is GitHub-planning only. It authorizes no production implementation, test implementation, documentation alignment, Git integration, release integration, or Release 1.5 work.

## Governing Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2. `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4. Release 1.3 post-merge closure evidence and current GitHub/repository state.

If repository or GitHub truth materially contradicts these authorities, stop without mutation and report the contradiction.

## Starting-State Gates

Before changing GitHub:

- Authenticate against `samuel-santos-engineer/AIQuantTradingResearch`.
- Verify default branch is `main`.
- Verify Release 1.3 PR #152 is merged.
- Verify Release 1.3 milestone #54 is closed.
- Verify issues #138–#151 are Closed/Done.
- Verify local `main` and `origin/main` are synchronized unless repository truth has legitimately advanced after closure.
- Verify no Release 1.4 implementation work has started.
- Inspect milestone #45, `Phase 4 - Release 1.4: Feature Engineering`.
- Inspect Project #2 fields/options, existing labels, priorities, Areas, owner conventions, and issue conventions.
- Search for existing Release 1.4 planning objects before creating anything.
- Reject duplicate milestone, issue, Release option, or equivalent WP objects.

## Milestone Decision

The accepted Release 1.4 title is:

`Phase 4 - Release 1.4: Deterministic Feature Engineering Foundation`

Legacy milestone #45 is open and empty according to the accepted definition.

Prefer reconciling milestone #45 in place rather than creating a duplicate **only if current GitHub truth confirms it remains open, empty, unused, and clearly represents the historical Release 1.4 placeholder**.

If those conditions hold, update milestone #45 to the accepted Release 1.4 title and a concise description reflecting:

- one built-in deterministic `simple-return-lag-1-v1` feature;
- accepted immutable snapshot input;
- immutable in-memory feature-set output;
- `aiq-feature-identity-v1`;
- SQLite schema remains version 2;
- no feature persistence, acquisition, scheduling, retries, plugins, backtesting, ML, or Release 1.5 work.

If milestone #45 is no longer empty/unused or reconciliation would destroy meaningful history, stop and request human authority rather than creating a competing milestone.

## Project #2 Planning

Inspect Project #2 before mutation.

If `Release = 1.4` does not exist, add exactly one `1.4` option to the existing Release field.

Do not create new Project fields.

Reuse existing values for:

- Status: `Backlog`
- Priority: `P1`
- Release: `1.4`
- Area: choose the closest existing Area justified by each WP.

Do not invent a new Area merely to say "Features". Reuse the closest established Application/Data/Architecture/Documentation area based on current Project truth.

Reuse established owner conventions. Do not assign a new owner unless existing conventions make the correct owner unambiguous.

## Exact Work-Package Set

Create exactly fourteen issues, WP01–WP14. No WP15+, closure issue, lifecycle-gate issue, or Release 1.5 issue.

### WP01 — Release & Repository Preflight

Objective: Reconcile Release 1.3 closure, GitHub planning, repository state, schema-v2, architecture graph, security baseline, and the 197-test baseline before implementation.

Depends on: Release 1.3 CLOSED.

Expected area: governance/engineering or closest existing equivalent.

### WP02 — Feature Engineering Semantic Discovery

Objective: Freeze feature vocabulary, exact lag-1 formula, ordering, timestamp association, decimal fidelity, empty/single-input semantics, invalid numeric evidence, and exclusions.

Depends on: WP01.

Expected area: data/architecture or closest existing equivalent.

### WP03 — Feature Identity, Provenance & Evidence Semantics

Objective: Freeze `aiq-feature-identity-v1`, canonical Feature Definition and Feature Set identities, provenance, lineage, equivalence, and evidence-establishment semantics.

Depends on: WP02.

Expected area: data/architecture or closest existing equivalent.

### WP04 — Feature Domain/Application Model

Objective: Establish the minimum immutable provider/storage-independent feature model, preferring Domain delta zero and Application ownership.

Depends on: WP03.

Expected area: application/architecture or closest existing equivalent.

### WP05 — Feature Generation Contracts

Objective: Define request, result, failure, evidence, and use-case seams for bounded feature generation.

Depends on: WP04.

Expected area: application or closest existing equivalent.

### WP06 — Deterministic Simple-Return Computation

Objective: Implement exact `simple-return-lag-1-v1` computation using decimal arithmetic and timestamp/offset fidelity.

Depends on: WP05.

Expected area: application or closest existing equivalent.

### WP07 — Feature Validation & Failure Mapping

Objective: Enforce request/evidence invariants, expected failure distinctions, fail-stop behavior, and unknown-exception propagation.

Depends on: WP03, WP06.

Expected area: application or closest existing equivalent.

### WP08 — Feature Generation Integration

Objective: Compose exact snapshot lookup, validation, deterministic computation, feature identity/provenance, and structured result evidence without persistence.

Depends on: WP05, WP06, WP07.

Expected area: application or closest existing equivalent.

### WP09 — Dependency Registration & Configuration

Objective: Register the bounded feature graph and explicit request factory with culture-invariant configuration and side-effect-free resolution.

Depends on: WP08.

Expected area: application/worker or closest existing equivalent.

### WP10 — One-Shot Worker Feature Execution

Objective: Execute one explicit feature request, emit safe structured evidence, return deterministic exit status, and terminate.

Depends on: WP09.

Expected area: worker/application or closest existing equivalent.

### WP11 — Domain & Application Feature Tests

Objective: Add permanent deterministic offline coverage for feature identities, formula, fidelity, determinism, provenance, validation, failures, and integration semantics.

Depends on: WP03, WP04, WP06, WP07, WP08.

Expected area: testing/application or closest existing equivalent.

### WP12 — Composition & Worker Validation

Objective: Prove real DI, configuration, one-shot Worker behavior, cleanup, and zero provider calls with permanent offline tests.

Depends on: WP09, WP10.

Expected area: testing/infrastructure or closest existing equivalent.

### WP13 — Architecture & Documentation Alignment

Objective: Preserve stable architecture boundaries and align only current-state documentation made stale by Release 1.4.

Depends on: WP11, WP12.

Expected area: architecture/documentation or closest existing equivalent.

### WP14 — Full Validation, Integration & Acceptance

Objective: Reconcile the exact candidate, run full/fresh-checkout validation, create one integration commit/branch/push and one review-ready PR, while leaving the PR unmerged and milestone open.

Depends on: WP11, WP12, WP13.

Expected area: engineering/release or closest existing equivalent.

## Issue Body Requirements

Every WP issue must include:

- release name;
- objective;
- dependency/dependencies;
- concise in-scope boundary;
- explicit exclusions relevant to that WP;
- expected repository area;
- expected model from the accepted plan;
- completion condition;
- statement that the next WP must not begin automatically.

Use the model recommendations:

- WP01: Luna
- WP02: Sol
- WP03: Sol
- WP04: Terra
- WP05: Terra
- WP06: Terra
- WP07: Sol
- WP08: Terra
- WP09: Terra
- WP10: Terra
- WP11: Luna
- WP12: Terra
- WP13: Terra
- WP14: Sol

Model names are execution guidance only.

## Initial Lifecycle

For all fourteen issues:

- milestone: authoritative Release 1.4 milestone;
- Project #2: included;
- Status: `Backlog`;
- Priority: `P1`;
- Release: `1.4`;
- Area: closest existing justified value;
- issue state: Open.

Do not start WP01. WP01 remains Open/Backlog after planning.

Do not close any Release 1.4 issue during this planning action.

## Dependency Encoding

Use the repository's established dependency representation if one exists.

Dependencies must exactly match:

```text
Release 1.3 CLOSED → WP01 → WP02 → WP03 → WP04 → WP05 → WP06
WP03 + WP06 → WP07
WP05 + WP06 + WP07 → WP08 → WP09 → WP10
WP03 + WP04 + WP06 + WP07 + WP08 → WP11
WP09 + WP10 → WP12
WP11 + WP12 → WP13
WP11 + WP12 + WP13 → WP14
```

Do not add convenience dependencies that change this graph.

## Repository Mutation Protection

This authority permits no repository-content mutation.

Do not:

- create/edit planning Markdown files;
- create WP prompts;
- stage or commit;
- create branches;
- push;
- create PRs;
- alter code/tests/docs;
- change schema/packages/references.

The only authorized mutations are the GitHub planning mutations described above.

## Validation

After planning:

- authoritative Release 1.4 milestone exists exactly once;
- milestone is Open;
- exactly fourteen Release 1.4 WP issues exist;
- WP01–WP14 titles match this authority;
- all fourteen are Open;
- all fourteen are Backlog;
- all fourteen are P1;
- all fourteen use Release `1.4`;
- all fourteen belong to the authoritative milestone;
- all fourteen are in Project #2;
- dependency graph matches exactly;
- no WP15+ exists;
- no closure/lifecycle issue exists;
- Release 1.5 planning/issues were not created;
- repository content and Git history are unchanged.

## Required Final Report

Report:

1. authenticated repository;
2. starting Release 1.3 lifecycle proof;
3. milestone #45 starting state;
4. milestone action taken and final milestone number/title/state;
5. Project #2 Release-field action;
6. issue numbers/titles for WP01–WP14;
7. Project field values for each WP;
8. dependency reconciliation;
9. duplicate/unexpected Release 1.4 objects;
10. repository/Git mutation count;
11. final WP01 state;
12. final milestone state;
13. blockers/findings.

Terminal marker:

`RELEASE 1.4 GITHUB PLANNING COMPLETE`

Then:

`NEXT AUTHORIZED ACTION: Human review of Release 1.4 GitHub planning, then WP01 — Release & Repository Preflight.`

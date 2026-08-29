# Release 1.10 WP07 — Documentation Path Allowlist & Content Ownership Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY authority for contract, policy, architecture, documentation-path ownership, content allocation, reconciliation, acceptance criteria, governance, and planning-only mutations.
- **GPT-5.6 Terra** — downstream WP07 documentation implementation, validation execution, approved repository mutations, and GitHub lifecycle completion only after this authority is TERRA-READY.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10**

Work package:

**WP07 — Documentation, Developer Setup & Operational Runbook**

Issue: **#248**

Milestone: **#59**

Project: **#2**

Predecessor:

**WP06 #247 — Closed / Done**

Dependency:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This is a narrow reconciliation authority. It repairs only WP07 documentation path/content ownership determinism.

---

# Accepted blocker

The first WP07 Terra authority correctly blocked before mutation because the binding manifest says:

> “exact existing architecture/developer/operations Markdown allowlist”

but names no writable documentation files.

The WP07 Terra authority explicitly forbids inventing a path.

Accepted unchanged state:

- #242–#247 Closed/Done.
- #248 Open/Backlog.
- #249 Open/Backlog.
- milestone #59 Open: **2 open / 6 closed**.
- repository mutations from blocked WP07: ZERO.
- Git mutations: ZERO.
- GitHub mutations: ZERO.

Emit:

`RELEASE 1.10 WP07 PATH-ALLOWLIST RECONCILIATION ENTRY: PASS`

---

# Purpose

Freeze an exact, exhaustive documentation path allowlist and content-ownership contract so Terra can execute WP07 without deciding:

- which existing Markdown file to edit;
- whether a new Markdown file is permitted;
- where architecture content belongs;
- where developer setup belongs;
- where operational procedures belong;
- where permanent-test/validation references belong;
- which predecessor documentation is read-only;
- which adjacent documentation is forbidden;
- where the WP08 handoff belongs.

Do not implement WP07 documentation.

---

# Frozen runtime/test contracts

Do not reopen WP02–WP06 semantics.

## WP02
Application owns BCL `System.Diagnostics` pipeline observability, root `pipeline.execute`, five governed stage activities, truthful retrieval/materialization boundaries, and parent topology.

## WP03
Instrumentation owners:
- `SqliteHistoricalObservationStore.Retrieve(string target)`
- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Non-owners:
- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

Infrastructure ActivitySource/Meter, `provider.operation`, `persistence.operation`, bounded instruments/failure categories are frozen.

## WP04
- Worker owns lifecycle.
- no external exporter for Release 1.10.
- no exporter package/project/configuration/lifecycle.
- Python capability invocation bounded.
- Streamlit independent.
- canonical handoff preserved.

## WP05
- .NET canonical System Health.
- nested optional `systemHealth` in `aiq-visualization-read-model-v1`.
- schema v4.
- health states exactly `ready`, `warmup`, `empty`, `failed`, `stale`, `unavailable`.
- `degraded` excluded.
- predicates, precedence, finite reasons, freshness semantics, malformed/absent behavior, deterministic Streamlit mapping frozen.

## WP06
- four dedicated permanent test paths are frozen and implemented.
- predecessor tests are read-only under WP06 ownership.
- permanent no-bypass/architecture/security enforcement is frozen.
- accepted regression baseline:
  - Application 136/136
  - Infrastructure 191/191
  - Architecture 27/27
  - Domain 11/11
  - .NET 365/365
  - Python 25/25
  - Streamlit 1.61.1
  - `pip check` clean
  - Gitleaks 8.30.1 clean
  - build 0 errors
  - pre-existing duplicate `AIQuantTradingDev` selector warnings are environment-only.

---

# Mutation boundary

Allowed repository mutations are planning/architecture documentation needed to repair WP07 determinism, expected among:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` only if content-ownership references genuinely belong there.

Modify `RELEASE_1.10_DEFINITION.md` only if a direct definition-level contradiction exists; strongly avoid.

Forbidden:

- WP07 target documentation implementation;
- production code;
- tests;
- project/package files;
- schema/migrations;
- signing configuration;
- Git mutations;
- GitHub mutations;
- #248 closure;
- Project Status mutation;
- WP08 implementation.

#248 remains Open/Backlog.

---

# Phase 0 — Documentation topology inventory

Read:

1. Release 1.10 definition.
2. Release 1.10 execution plan.
3. Release 1.10 file manifest.
4. `OPEN_TELEMETRY_SELECTION.md`.
5. WP06→WP07 handoff.
6. #248 read-only.
7. #249 read-only.
8. all Markdown files under relevant architecture/developer/operations/documentation directories.
9. root/project README files.
10. existing setup/developer guidance.
11. existing runbooks/operations guidance.
12. existing validation/security documentation.
13. existing release documentation conventions.
14. WP02–WP06 production/test paths read-only where needed to validate documentation ownership.
15. current worktree status/diff.

Enumerate actual candidate Markdown paths before deciding ownership.

For every candidate record:

| Path | Exists | Current purpose | Audience | Release-specific/general | Candidate WP07 role |
| --- | --- | --- | --- | --- | --- |

Do not assume a README or create a new runbook merely because it seems natural.

Emit:

`RELEASE 1.10 WP07 DOCUMENTATION TOPOLOGY INVENTORY: COMPLETE`

---

# Phase 1 — Freeze writable path strategy

For every WP07 documentation responsibility decide exactly one of:

A. extend an existing Markdown path;
B. create an explicitly authorized new Markdown path;
C. document through multiple exact paths with non-overlapping ownership.

For every writable path freeze:

- exact repository-relative path;
- EXISTING or NEW;
- canonical audience;
- exact WP07 content families allowed;
- exact content families forbidden;
- whether Release 1.10-specific or durable cross-release documentation.

Do not leave generic terms such as:

- architecture docs;
- developer docs;
- operations docs;
- README;
- runbook.

Emit:

`RELEASE 1.10 WP07 WRITABLE DOCUMENTATION PATH SET: FROZEN`

---

# Phase 2 — Architecture content ownership

Freeze exact writable path ownership for:

- WP02 pipeline observability topology;
- WP03 Infrastructure instrumentation ownership;
- WP04 Worker lifecycle/exporter isolation;
- WP05 System Health architecture;
- canonical data flow;
- Python/Streamlit no-bypass;
- schema v4 preservation;
- `aiq-visualization-read-model-v1`;
- Release 1.8 JSON-over-stdio separation.

For each content family specify:

- exact path;
- exact section heading to add/update;
- source-of-truth implementation/test paths;
- whether existing content is replaced, amended, or referenced;
- duplication policy.

Prefer one canonical architecture owner plus links/references elsewhere rather than copying the same contract into multiple files.

Emit:

`RELEASE 1.10 WP07 ARCHITECTURE CONTENT OWNERSHIP: FROZEN`

---

# Phase 3 — Developer setup ownership

Freeze exact writable path and section ownership for:

- .NET prerequisites;
- Python prerequisites;
- dependency installation;
- `pip check`;
- Streamlit 1.61.1;
- Worker build/run;
- Streamlit launch;
- canonical handoff prerequisites;
- Windows dev signing;
- duplicate `AIQuantTradingDev` selector-warning interpretation.

For every command family freeze where it is documented.

Do not authorize weakening OS security policy.

Do not authorize tracked signing configuration changes.

Emit:

`RELEASE 1.10 WP07 DEVELOPER SETUP CONTENT OWNERSHIP: FROZEN`

---

# Phase 4 — Operational runbook ownership

Freeze exact writable path for operational procedures.

If an existing runbook exists, determine whether WP07 extends it.

If no suitable runbook exists and planning intent requires a dedicated runbook, explicitly authorize one exact NEW path.

Freeze exact section ownership for:

- normal verification;
- System Health interpretation;
- visualization-state/System-Health distinction;
- absent health;
- malformed health;
- stale handoff;
- no-exporter expectation;
- Worker/handoff troubleshooting;
- Streamlit troubleshooting;
- Python dependency troubleshooting;
- dev-signing/Application Control troubleshooting;
- duplicate dev certificate handling;
- process/listener/UI residue checks.

Emit:

`RELEASE 1.10 WP07 OPERATIONAL RUNBOOK CONTENT OWNERSHIP: FROZEN`

---

# Phase 5 — Permanent test and validation-map ownership

Consume the exact WP06→WP07 handoff.

Freeze the exact WP07 documentation path/section that will name:

- all four dedicated WP06 permanent test paths;
- each test path's invariant family;
- focused WP06 commands;
- full .NET commands;
- Python commands;
- Streamlit version check;
- `pip check`;
- build;
- Gitleaks 8.30.1 command;
- residue checks.

Predecessor tests that WP06 classified read-only must not be mislabeled as WP06-owned.

Emit:

`RELEASE 1.10 WP07 PERMANENT TEST/VALIDATION CONTENT OWNERSHIP: FROZEN`

---

# Phase 6 — Security/cardinality content ownership

Freeze exact path/section ownership for:

- bounded telemetry attributes;
- finite failure categories;
- finite System Health states/reasons;
- no raw exception messages in governed reason vocabulary;
- no secrets/credentials/endpoints in telemetry;
- no uncontrolled high-cardinality identifiers;
- no external exporter configuration;
- Gitleaks security gate.

Distinguish:

- architecture/security design documentation;
- operator validation procedure.

Avoid duplicate canonical ownership.

Emit:

`RELEASE 1.10 WP07 SECURITY/CARDINALITY CONTENT OWNERSHIP: FROZEN`

---

# Phase 7 — Provenance/capability boundary ownership

Freeze exact path/section ownership for truthful capability statements:

- deterministic/replay/simulated provenance;
- no live provider capability;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no external telemetry backend;
- System Health limited to authoritative existing source facts.

Ensure developer/runbook docs link to the canonical boundary rather than inventing broader claims.

Emit:

`RELEASE 1.10 WP07 PROVENANCE/CAPABILITY CONTENT OWNERSHIP: FROZEN`

---

# Phase 8 — WP08 handoff ownership

Freeze exactly where WP07 records or exposes the information WP08 must consume:

- Release 1.10 documentation path set;
- implemented path inventory references;
- validation commands;
- latest test counts;
- build warnings/errors;
- Gitleaks result;
- environment facts;
- schema v4;
- canonical v1 handoff;
- no-exporter fact;
- known environment-only caveats.

Do not perform WP08 validation or acceptance.

If handoff belongs only in the execution plan rather than durable product docs, say so explicitly.

Emit:

`RELEASE 1.10 WP07 → WP08 HANDOFF CONTENT OWNERSHIP: FROZEN`

---

# Phase 9 — Read-only predecessor documentation set

Enumerate exact existing documentation paths Terra may read/reference but must not mutate under WP07.

Include:

- planning artifacts not allocated as WP07 writable;
- predecessor architecture docs;
- historical release docs;
- README/setup/runbook files that are reference-only;
- generated or policy-controlled docs if present.

For each path state why it is read-only.

Emit:

`RELEASE 1.10 WP07 READ-ONLY PREDECESSOR DOCUMENTATION SET: FROZEN`

---

# Phase 10 — Forbidden adjacent documentation paths

Enumerate exact tempting but forbidden Markdown paths/directories.

Examples only if actually present:

- Release 1.9 docs;
- unrelated architecture decisions;
- contribution/governance docs;
- security policy;
- generated documentation;
- release notes reserved for WP08/final publication.

Do not use broad directory bans that accidentally conflict with the exact writable allowlist.

Emit:

`RELEASE 1.10 WP07 FORBIDDEN ADJACENT DOCUMENTATION TARGETS: FROZEN`

---

# Phase 11 — Content allocation matrix

Produce an exhaustive canonical matrix:

| Content ID | Required content | Exact writable path | Exact section | Source evidence | Canonical/Reference | Duplication rule |
| --- | --- | --- | --- | --- | --- | --- |

Required content families must include:

- OBS-ARCH
- INFRA-OBS
- LIFECYCLE
- NO-EXPORTER
- HEALTH-CONTRACT
- DATA-FLOW
- NO-BYPASS
- SCHEMA/HANDOFF
- DEV-SETUP
- DEV-SIGNING
- RUNBOOK-HEALTH
- RUNBOOK-TROUBLESHOOT
- PERMANENT-TESTS
- VALIDATION
- SECURITY/CARDINALITY
- PROVENANCE
- CAPABILITY-BOUNDARIES
- WP08-HANDOFF.

Every content family must have one canonical writable owner.

Emit:

`RELEASE 1.10 WP07 COMPLETE CONTENT/PATH OWNERSHIP MATRIX: FROZEN`

---

# Phase 12 — Exact WP07 mutation path set

Produce a literal complete list:

## WP07 Terra writable paths

For each:

`<path> — EXISTING|NEW — <bounded purpose>`

No wildcard.

No “existing architecture/developer/operations Markdown allowlist”.

This list is the binding WP07 implementation allowlist.

If a path is not in this list, Terra cannot mutate it under WP07.

Emit:

`RELEASE 1.10 WP07 EXACT MUTATION PATH SET: FROZEN`

---

# Phase 13 — Documentation reference/link strategy

Freeze how Terra should link/reference canonical material.

For every writable path specify:

- internal relative links required;
- source sections referenced;
- whether code symbols should be rendered literally;
- whether commands are canonical or illustrative.

Require repository-relative links to resolve.

Do not create duplicated full contract copies where a canonical reference suffices.

Emit:

`RELEASE 1.10 WP07 DOCUMENTATION REFERENCE STRATEGY: FROZEN`

---

# Phase 14 — Validation ownership

Freeze exact validation procedures for WP07:

## Focused documentation validation
- `git diff --check`
- path existence
- relative link/reference verification
- symbol/command/version consistency
- existing repository Markdown/doc checks, if any.

## Full regression
Freeze exact existing commands for:
- Application
- Infrastructure
- Architecture
- Domain
- Python
- build
- Streamlit version
- `pip check`
- Gitleaks
- residue checks.

Do not introduce a new documentation linter/package.

Do not hard-code future counts as pass conditions; Terra reports actual counts.

Emit:

`RELEASE 1.10 WP07 VALIDATION OWNERSHIP: FROZEN`

---

# Phase 15 — Planning artifact reconciliation

Update only minimum planning/architecture artifacts.

At minimum the file manifest must replace the non-deterministic WP07 phrase with the literal writable path set.

The execution plan must identify:

- content → path ownership;
- read-only predecessor docs;
- forbidden adjacent docs;
- validation ownership;
- WP08 handoff ownership.

Only update `OPEN_TELEMETRY_SELECTION.md` if necessary to express canonical documentation ownership, not to rewrite runtime architecture.

Emit:

`RELEASE 1.10 WP07 PATH-ALLOWLIST PLANNING RECONCILIATION: PASS`

---

# Phase 16 — Cross-contract consistency

Re-read all modified planning/architecture artifacts.

Prove:

- every EXISTING writable path exists;
- every NEW writable path is explicitly authorized;
- no writable path is simultaneously read-only/forbidden;
- every content family has one canonical owner;
- no runtime/test path is writable;
- no package/schema/signing path is writable;
- WP06 test references are exact;
- WP08 handoff ownership is deterministic.

Emit:

`RELEASE 1.10 WP07 PATH/CONTENT CROSS-CONTRACT CONSISTENCY: PASS`

---

# Phase 17 — Terra materialization simulation

Simulate WP07 implementation path-by-path.

For every writable path answer:

1. exact file;
2. existing/new;
3. exact sections to create/update;
4. exact content IDs owned;
5. exact source evidence;
6. exact links/references;
7. exact commands documented;
8. exact validation that proves the edit;
9. exact content that must NOT be placed there.

Ask:

**Could two competent Terra implementers choose different documentation files or different canonical content owners while both claiming compliance?**

If YES, reconciliation is incomplete.

Only if NO emit:

`RELEASE 1.10 WP07 DOCUMENTATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 18 — Mutation audit

Require:

`RELEASE 1.10 WP07 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP07 RECONCILIATION WP07 TARGET DOCUMENTATION MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION PRODUCTION/TEST/PROJECT/PACKAGE/SCHEMA/SIGNING MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION GITHUB MUTATIONS: ZERO`

#248 remains Open/Backlog.

#249 remains Open/Backlog.

Milestone #59 remains Open.

---

# Phase 19 — Terra resumption handoff

After TERRA-READY, authorize resumption of:

**Release 1.10 WP07 — Documentation, Developer Setup & Operational Runbook Authority — GPT-5.6 Terra**

Terra must consume the reconciled manifest/execution plan as binding.

Terra must not reopen:

- writable path choice;
- content ownership;
- read-only/forbidden path classification;
- validation ownership;
- WP08 handoff location.

Emit:

`RELEASE 1.10 WP07 PATH-ALLOWLIST/CONTENT-OWNERSHIP RECONCILIATION: PASS`

`RELEASE 1.10 WP07 → TERRA RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WP07 PATH-ALLOWLIST RECONCILIATION ENTRY: PASS`

`RELEASE 1.10 WP07 DOCUMENTATION TOPOLOGY INVENTORY: COMPLETE`

`RELEASE 1.10 WP07 WRITABLE DOCUMENTATION PATH SET: FROZEN`

`RELEASE 1.10 WP07 ARCHITECTURE CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 DEVELOPER SETUP CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 OPERATIONAL RUNBOOK CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 PERMANENT TEST/VALIDATION CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 SECURITY/CARDINALITY CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 PROVENANCE/CAPABILITY CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 → WP08 HANDOFF CONTENT OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 READ-ONLY PREDECESSOR DOCUMENTATION SET: FROZEN`

`RELEASE 1.10 WP07 FORBIDDEN ADJACENT DOCUMENTATION TARGETS: FROZEN`

`RELEASE 1.10 WP07 COMPLETE CONTENT/PATH OWNERSHIP MATRIX: FROZEN`

`RELEASE 1.10 WP07 EXACT MUTATION PATH SET: FROZEN`

`RELEASE 1.10 WP07 DOCUMENTATION REFERENCE STRATEGY: FROZEN`

`RELEASE 1.10 WP07 VALIDATION OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 PATH-ALLOWLIST PLANNING RECONCILIATION: PASS`

`RELEASE 1.10 WP07 PATH/CONTENT CROSS-CONTRACT CONSISTENCY: PASS`

`RELEASE 1.10 WP07 DOCUMENTATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP07 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP07 RECONCILIATION WP07 TARGET DOCUMENTATION MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION PRODUCTION/TEST/PROJECT/PACKAGE/SCHEMA/SIGNING MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP07 RECONCILIATION GITHUB MUTATIONS: ZERO`

`RELEASE 1.10 WP07 PATH-ALLOWLIST/CONTENT-OWNERSHIP RECONCILIATION: PASS`

`RELEASE 1.10 WP07 → TERRA RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 WP07 — DOCUMENTATION PATH ALLOWLIST & CONTENT OWNERSHIP RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if Luna cannot deterministically allocate all WP07 content to exact documentation paths without reopening runtime/test architecture or exceeding Release 1.10 scope.

If blocked:

- preserve valid planning analysis;
- WP07 target documentation mutations ZERO;
- production/test/project/package/schema/signing mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO;
- #248 remains Open/Backlog;
- #249 remains Open/Backlog;
- milestone #59 remains Open;
- WP08 does not start;
- report the minimum unresolved path/content governance decision.

Exact blocked terminal:

`RELEASE 1.10 WP07 — DOCUMENTATION PATH ALLOWLIST & CONTENT OWNERSHIP RECONCILIATION AUTHORITY BLOCKED`

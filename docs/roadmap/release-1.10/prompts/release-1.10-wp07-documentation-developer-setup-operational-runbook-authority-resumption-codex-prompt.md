# Release 1.10 WP07 — Documentation, Developer Setup & Operational Runbook Authority — Terra Resumption

## Model assignment
- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY implementation, validation execution, approved repository/GitHub mutations, lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

## Resumption status

The prior WP07 Terra run BLOCKED before mutation because writable documentation paths were not deterministic.

The Luna reconciliation is now COMPLETE and TERRA-READY.

Binding exact WP07 writable allowlist:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
3. `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`

All three paths are EXISTING.

No new WP07 documentation path is authorized.

The reconciled:
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`

are binding read-only inputs for this Terra run and contain the exact content/section ownership allocation.

## Entry state

- #242–#247 Closed/Done.
- #248 Open/Backlog.
- #249 Open/Backlog.
- milestone #59 Open, 2 open / 6 closed.
- Luna reconciliation changed planning artifacts only.
- WP07 target documentation mutations so far: ZERO.
- production/test/package/schema/Git/GitHub mutations from blocked/reconciliation stages: ZERO.

Emit:

`RELEASE 1.10 WP07 TERRA RESUMPTION ENTRY: PASS`

## Non-negotiable resumption rule

Do not reopen path ownership or content ownership.

Do not mutate either reconciliation planning artifact under this Terra authority.

If implementation requires any path outside the three-path allowlist, BLOCK.

Then execute the complete existing WP07 authority below, interpreting its path-ownership gate as already satisfied by the reconciled manifest/plan.

---

# Release 1.10 WP07 — Documentation, Developer Setup & Operational Runbook Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, and read-only/planning authority.
- **GPT-5.6 Terra** — PRIMARY documentation implementation, validation execution, approved repository mutations, and mandatory GitHub WP lifecycle completion authority.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

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

WP07 documents the implemented and permanently tested Release 1.10 capability. It does not redesign runtime behavior.

---

# Accepted entry state

Treat as accepted unless current authoritative inspection directly contradicts it:

- #242–#247 Closed/Done.
- #248 Open/Backlog.
- #249 Open/Backlog.
- milestone #59 Open.
- milestone state after WP06: **2 open / 6 closed**.
- pre-existing dirty WP01–WP06 work must be preserved.
- Git mutations from WP06: ZERO.
- explicit WP06 GitHub mutation: close #247; Project Done was automated.

Accepted validation baseline after WP06:

- Application: **136/136**
- Infrastructure: **191/191**
- Architecture: **27/27**
- Domain: **11/11**
- .NET total: **365/365**
- Python: **25/25**
- Streamlit: **1.61.1**
- `pip check`: clean
- Gitleaks 8.30.1: no leaks
- build: 0 errors
- only pre-existing local `AIQuantTradingDev` selector warnings.

Emit:

`RELEASE 1.10 WP07 ENTRY: PASS`

---

# Binding source hierarchy

Before mutation read:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. #248
6. WP02 implementation/tests
7. WP03 implementation/tests
8. WP04 implementation/tests
9. WP05 implementation/tests
10. the four WP06 permanent test paths
11. WP06→WP07 handoff
12. existing developer/setup/runbook documentation conventions
13. current worktree status/diff.

Runtime implementation and permanent tests are evidence. Planning documents are scope/governance. Documentation must match both.

Emit:

`RELEASE 1.10 WP07 CONTRACT/HANDOFF CONSUMPTION: PASS`

---

# Exact path ownership gate

The Release 1.10 file manifest and WP06→WP07 handoff must identify exact WP07 documentation paths.

Before editing, produce a matrix:

| Path | Existing/New | Purpose | Source facts | Allowed mutation |
| --- | --- | --- | --- | --- |

If exact WP07 documentation paths are not deterministically frozen, or if Terra would need to invent a documentation location/name, BLOCK before mutation and request narrow GPT-5.6 Luna path reconciliation.

Do not create a plausible README/runbook path by judgment.

Emit:

`RELEASE 1.10 WP07 PATH OWNERSHIP: FROZEN`

---

# Documentation truthfulness principles

All WP07 documentation must be:

- evidence-based;
- operationally executable;
- bounded to Release 1.10;
- explicit about simulated/replay/deterministic provenance;
- explicit that no live provider capability is added;
- explicit that no external telemetry exporter is selected for Release 1.10;
- explicit that Streamlit does not own telemetry/Worker/provider/persistence;
- explicit that System Health is a canonical .NET read-model projection, not a second runtime health service;
- explicit that schema remains v4;
- explicit that canonical visualization handoff remains v1.

Do not document aspirational or future capability as current.

---

# Phase 0 — Baseline and documentation inventory

Verify entry state and inspect existing documentation structure.

Identify:

- canonical architecture docs;
- developer setup docs;
- validation docs;
- operational troubleshooting/runbook conventions;
- commands already used by the repository;
- current Python/.NET version requirements;
- dev-signing guidance;
- canonical Streamlit launch workflow;
- Worker launch workflow;
- canonical handoff location/configuration from actual code/docs.

Do not infer commands when exact repository commands exist.

Emit:

`RELEASE 1.10 WP07 DOCUMENTATION INVENTORY: COMPLETE`

---

# Phase 1 — Document Release 1.10 observability architecture

Document the implemented WP02–WP04 architecture accurately.

## WP02 pipeline topology

Document:

- Application owns pipeline observability;
- BCL `System.Diagnostics`;
- root activity `pipeline.execute`;
- five governed stage activities using exact implemented names;
- historical retrieval boundary;
- dataset materialization starts after retrieval;
- Infrastructure activity nesting through `Activity.Current`;
- no duplicate opaque interval.

## WP03 Infrastructure observability

Document:

- exact three governed instrumentation owners:
  - `SqliteHistoricalObservationStore.Retrieve(string target)`
  - `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
  - `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`
- non-owners:
  - `SqliteDatasetCatalog`
  - `SqliteHistoricalObservationStore.Persist(...)`
- ActivitySource/Meter ownership;
- exact activity names:
  - `provider.operation`
  - `persistence.operation`
- bounded metrics/failure categories;
- sanitized/cardinality rules.

## WP04 lifecycle/exporter isolation

Document:

- Worker owns observability lifecycle;
- Python capability invocation remains bounded;
- Streamlit remains independent;
- no external exporter in Release 1.10;
- no exporter package/configuration/lifecycle;
- canonical handoff remains the cross-runtime presentation boundary.

Emit:

`RELEASE 1.10 WP07 OBSERVABILITY ARCHITECTURE DOCUMENTATION: PASS`

---

# Phase 2 — Document System Health contract

Document WP05 exactly.

Include:

- System Health is nested in `aiq-visualization-read-model-v1`;
- optional `systemHealth` preserves pre-WP05 v1 compatibility;
- visualization state remains distinct from System Health;
- visualization states remain:
  - `Ready`
  - `WarmUp`
  - `Empty`
  - `Stale`
  - `Failed`
- System Health states remain exactly:
  - `ready`
  - `warmup`
  - `empty`
  - `failed`
  - `stale`
  - `unavailable`
- `degraded` is intentionally excluded because Release 1.10 has no authoritative WP03/WP04 source predicate;
- exact frozen precedence;
- exact finite reason-token vocabulary and meaning;
- exact timestamp/freshness semantics;
- no independent health freshness threshold;
- absent pre-WP05 behavior;
- malformed-health behavior;
- deterministic Streamlit mapping.

Use exact current implementation/test facts; do not simplify semantics into misleading prose.

Emit:

`RELEASE 1.10 WP07 SYSTEM HEALTH CONTRACT DOCUMENTATION: PASS`

---

# Phase 3 — Document canonical data-flow and no-bypass boundaries

Document the end-to-end ownership/data flow:

`.NET pipeline → canonical visualization read model → atomic JSON handoff → Python parser/frame → Streamlit presentation`

Document explicit forbidden bypasses:

- Streamlit → SQLite: forbidden
- Streamlit → provider: forbidden
- Streamlit → Worker supervision: forbidden
- Python → second System Health authority: forbidden
- Python → provider/persistence parallel path: forbidden
- second System Health channel: forbidden.

Document Release 1.8 JSON-over-stdio as a separate boundary.

Document schema v4 preservation and absence of Release 1.10 migration.

Emit:

`RELEASE 1.10 WP07 DATA-FLOW/NO-BYPASS DOCUMENTATION: PASS`

---

# Phase 4 — Developer setup

Produce/update exact developer setup instructions using repository-supported commands.

Cover only actual prerequisites and workflows:

- required .NET SDK/runtime from repository configuration;
- Python version from repository configuration;
- Python dependency installation using the repository's exact mechanism;
- `pip check`;
- Streamlit 1.61.1 expectation;
- Worker build/run;
- Streamlit launch;
- canonical handoff prerequisites/configuration;
- local development signing workflow where Windows Application Control requires it;
- how to distinguish local duplicate `AIQuantTradingDev` certificate selector warnings from repository signing defects.

Do not add tracked signing configuration.

Do not recommend weakening execution policy or OS security controls.

Emit:

`RELEASE 1.10 WP07 DEVELOPER SETUP: PASS`

---

# Phase 5 — Operational runbook

Create/update the exact frozen operational runbook path.

The runbook must be executable and bounded.

At minimum cover:

## Normal verification
- build;
- .NET test suites;
- Python tests;
- Streamlit version;
- `pip check`;
- Gitleaks;
- process/listener/residue verification.

## System Health interpretation
For every frozen health state document:

- meaning;
- source condition;
- reason token(s);
- what an operator should verify;
- what it does NOT imply.

## Visualization state vs System Health
Provide a concise matrix showing they are separate dimensions and may coexist according to the frozen truth table.

## Absent/malformed health
Document expected pre-WP05 compatible absence and malformed extension behavior.

## No exporter
State clearly that Release 1.10 does not export telemetry to an external backend and therefore no exporter endpoint/dashboard should be expected.

## Troubleshooting
Bounded troubleshooting for:
- Worker not running/available according to actual contract;
- canonical handoff absent/stale/malformed;
- Streamlit presentation issue;
- Python dependency issue;
- dev-signing/Application Control issue;
- duplicate local development certificates;
- test/listener/process residue.

Do not invent operational remediation that changes architecture.

Emit:

`RELEASE 1.10 WP07 OPERATIONAL RUNBOOK: PASS`

---

# Phase 6 — Permanent-test map

Document the four exact WP06 permanent test paths and what each permanently enforces.

Include exact focused commands frozen by WP06 reconciliation.

Document the full validation commands used by the project.

Do not claim predecessor tests are WP06-owned if they were frozen read-only.

Emit:

`RELEASE 1.10 WP07 PERMANENT TEST/RUNBOOK MAP: PASS`

---

# Phase 7 — Security/cardinality documentation

Document:

- bounded telemetry attributes;
- finite failure categories;
- finite System Health states/reasons;
- no raw exception text in governed reason vocabulary;
- no secrets/credentials/endpoints in telemetry;
- no uncontrolled high-cardinality identifiers;
- Gitleaks command and expected clean gate;
- no external exporter configuration.

Canonical scanner:

**Gitleaks 8.30.1**

Command:

`gitleaks git . --redact --verbose`

Emit:

`RELEASE 1.10 WP07 SECURITY/CARDINALITY DOCUMENTATION: PASS`

---

# Phase 8 — Provenance and capability boundaries

Document Release 1.10 capability truthfully:

- observability is for the governed existing pipeline/boundaries;
- System Health is bounded to available authoritative source facts;
- deterministic/replay/simulated provenance remains visible;
- no live-provider claim;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no external telemetry backend.

Emit:

`RELEASE 1.10 WP07 PROVENANCE/CAPABILITY BOUNDARY DOCUMENTATION: PASS`

---

# Phase 9 — Documentation link/reference audit

Verify every repository-relative path, symbol, command, schema/version, state token, reason token, and test location named in WP07 documentation.

No broken internal documentation references.

No stale 1.9 semantics presented as 1.10.

No future WP08 acceptance state claimed.

Emit:

`RELEASE 1.10 WP07 DOCUMENTATION REFERENCE AUDIT: PASS`

---

# Phase 10 — Focused documentation validation

Run documentation-specific checks available in the repository.

At minimum:

- `git diff --check`;
- exact path existence checks;
- command/path/symbol verification;
- any existing markdown/doc validation explicitly used by the repository.

Do not install a new linter/package solely for WP07.

Emit:

`RELEASE 1.10 WP07 FOCUSED DOCUMENTATION VALIDATION: PASS`

---

# Phase 11 — Full regression validation

Documentation changes must not alter runtime behavior.

Run the complete affected release validation baseline and report actual counts:

- Application;
- Infrastructure;
- Architecture;
- Domain;
- total .NET;
- Python;
- Streamlit version;
- `pip check`;
- relevant build warnings/errors.

Expected carried-forward baseline is 365/365 .NET and 25/25 Python, but report actual results rather than forcing expected counts.

If documentation-only changes cause no runtime diff but an unrelated pre-existing validation issue appears, classify it accurately rather than modifying runtime code under WP07.

Emit:

`RELEASE 1.10 WP07 FULL REGRESSION VALIDATION: PASS`

---

# Phase 12 — Gitleaks

Run:

`gitleaks git . --redact --verbose`

Use Gitleaks 8.30.1.

Require no unresolved leaks.

Do not weaken execution policy.

Emit:

`RELEASE 1.10 WP07 GITLEAKS SECURITY GATE: PASS`

---

# Phase 13 — Residue audit

Verify no WP07-owned:

- Worker;
- testhost;
- Python child;
- Streamlit server;
- listener;
- temp server;
- validation residue;
- locked handoff artifact.

Emit:

`RELEASE 1.10 WP07 PROCESS/LISTENER/UI RESIDUE: CLEAN`

---

# Phase 14 — Exact mutation audit

Prove:

- only frozen WP07 documentation paths changed;
- production mutations ZERO;
- test mutations ZERO;
- project/package mutations ZERO;
- schema/migration mutations ZERO;
- signing configuration mutations ZERO;
- no WP08 implementation;
- no unrelated formatting/refactor.

Emit:

`RELEASE 1.10 WP07 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP07 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 15 — WP08 handoff

WP08 is:

**Full Validation, Acceptance & PR Readiness**

Prepare a factual handoff containing:

- exact Release 1.10 changed-path inventory through WP07;
- current validation commands;
- current test counts;
- build warnings/errors;
- Gitleaks result;
- Streamlit/Python environment facts;
- schema v4;
- canonical v1 handoff;
- no-exporter fact;
- WP01–WP07 GitHub lifecycle state;
- known environment-only caveats;
- release acceptance criteria still to be executed by WP08.

Do not perform WP08 acceptance or PR/Git mutations.

Emit:

`RELEASE 1.10 WP07 DOWNSTREAM HANDOFF: PASS — WP08 READY`

---

# Phase 16 — Final acceptance

Evaluate every WP07 criterion from:

- #248;
- Release 1.10 definition;
- execution plan;
- file manifest;
- architecture selection;
- WP06→WP07 handoff;
- this authority.

Map each criterion to documentation evidence and validation evidence.

Only when all pass emit:

`RELEASE 1.10 WP07 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

---

# Phase 17 — Mandatory GitHub work-package completion

Immediately after acceptance, re-read #248 and its unique Project #2 item.

Require:

- issue #248;
- Release=1.10;
- milestone #59;
- unique Project item.

Then:

1. close #248 if Open;
2. set Project Status to `Done` only if not already Done.

If issue-close automation sets Status to Done, do not make a redundant Status mutation.

Count only explicit mutations actually performed.

Do NOT:

- close milestone #59;
- modify #249;
- change Release;
- perform WP08 implementation;
- create commits/branches/tags/PRs.

Emit:

`RELEASE 1.10 WP07 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 18 — GitHub post-verification

Re-read GitHub.

Require:

- #242–#248 Closed/Done;
- #248 Release=1.10;
- #248 milestone #59;
- milestone #59 remains Open;
- #249 remains Open/Backlog unless independently changed by another valid authority.

Expected milestone state if no independent changes occurred:

- **1 open / 7 closed**

Report actual state.

Emit:

`RELEASE 1.10 WP07 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 19 — Exact mutation accounting

Report exact final mutation ledger.

Repository:
- frozen WP07 documentation paths only.

Production:
- ZERO.

Tests:
- ZERO.

Project/package/schema/signing:
- ZERO.

Git:
- ZERO.

GitHub:
- only explicit WP07 completion mutations actually performed.

Emit:

`RELEASE 1.10 WP07 REPOSITORY MUTATIONS: ACCEPTED WP07 DOCUMENTATION PATHS ONLY`

`RELEASE 1.10 WP07 PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP07 PROJECT/PACKAGE/SCHEMA/SIGNING MUTATIONS: ZERO`

`RELEASE 1.10 WP07 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP07 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WP07 ENTRY: PASS`

`RELEASE 1.10 WP07 CONTRACT/HANDOFF CONSUMPTION: PASS`

`RELEASE 1.10 WP07 PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP07 DOCUMENTATION INVENTORY: COMPLETE`

`RELEASE 1.10 WP07 OBSERVABILITY ARCHITECTURE DOCUMENTATION: PASS`

`RELEASE 1.10 WP07 SYSTEM HEALTH CONTRACT DOCUMENTATION: PASS`

`RELEASE 1.10 WP07 DATA-FLOW/NO-BYPASS DOCUMENTATION: PASS`

`RELEASE 1.10 WP07 DEVELOPER SETUP: PASS`

`RELEASE 1.10 WP07 OPERATIONAL RUNBOOK: PASS`

`RELEASE 1.10 WP07 PERMANENT TEST/RUNBOOK MAP: PASS`

`RELEASE 1.10 WP07 SECURITY/CARDINALITY DOCUMENTATION: PASS`

`RELEASE 1.10 WP07 PROVENANCE/CAPABILITY BOUNDARY DOCUMENTATION: PASS`

`RELEASE 1.10 WP07 DOCUMENTATION REFERENCE AUDIT: PASS`

`RELEASE 1.10 WP07 FOCUSED DOCUMENTATION VALIDATION: PASS`

`RELEASE 1.10 WP07 FULL REGRESSION VALIDATION: PASS`

`RELEASE 1.10 WP07 GITLEAKS SECURITY GATE: PASS`

`RELEASE 1.10 WP07 PROCESS/LISTENER/UI RESIDUE: CLEAN`

`RELEASE 1.10 WP07 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP07 FORBIDDEN TARGET AUDIT: PASS`

`RELEASE 1.10 WP07 DOWNSTREAM HANDOFF: PASS — WP08 READY`

`RELEASE 1.10 WP07 ACCEPTANCE: PASS`

`RELEASE 1.10 WP07 GITHUB WORK-PACKAGE COMPLETION: PASS`

`RELEASE 1.10 WP07 GITHUB COMPLETION POST-VERIFY: PASS`

`RELEASE 1.10 WP07 REPOSITORY MUTATIONS: ACCEPTED WP07 DOCUMENTATION PATHS ONLY`

`RELEASE 1.10 WP07 PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP07 PROJECT/PACKAGE/SCHEMA/SIGNING MUTATIONS: ZERO`

`RELEASE 1.10 WP07 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP07 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 WP07 — DOCUMENTATION, DEVELOPER SETUP & OPERATIONAL RUNBOOK AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- exact WP07 documentation paths are not frozen;
- a documentation claim cannot be supported by current implementation/tests;
- developer/runbook instructions require inventing unsupported commands;
- completing documentation would require production/test/package/schema/signing mutation;
- a required validation gate cannot be completed under existing environment authority.

If blocked before acceptance:

- #248 remains Open/Backlog;
- GitHub mutations ZERO;
- Git mutations ZERO;
- preserve valid in-scope documentation work;
- WP08 does not start.

If blocked after acceptance during GitHub completion, report exact partial lifecycle state and make no unrelated repair mutations.

Exact blocked terminal:

`RELEASE 1.10 WP07 — DOCUMENTATION, DEVELOPER SETUP & OPERATIONAL RUNBOOK AUTHORITY BLOCKED`


---

# Resumption-specific final audit

Before acceptance prove the repository mutation set is a subset of exactly:

- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
- `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`

No other repository path may be mutated by this Terra run.

After exact:

`RELEASE 1.10 WP07 ACCEPTANCE: PASS`

complete GitHub lifecycle:

- close #248;
- ensure its unique Project #2 item is Done;
- do not redundantly mutate Status if issue-close automation already sets Done;
- keep milestone #59 Open;
- keep #249 Open/Backlog.

Expected milestone state after successful completion, absent independent changes:

**1 open / 7 closed**

Git mutations remain ZERO.

Exact success terminal remains:

`RELEASE 1.10 WP07 — DOCUMENTATION, DEVELOPER SETUP & OPERATIONAL RUNBOOK AUTHORITY COMPLETE`

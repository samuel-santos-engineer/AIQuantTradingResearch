# Release 1.10 WP05 — System Health Read Model and Streamlit Presentation Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, and read-only/planning authority.
- **GPT-5.6 Terra** — PRIMARY implementation, validation execution, approved repository mutations, and mandatory WP lifecycle completion authority for WP05.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; Sol does not silently replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

## Authority identity

Release: **1.10**

WP: **WP05 — System Health Read Model and Streamlit Presentation**

Issue: **#246**

Milestone: **#59**

Project: **#2**

Predecessor: **WP04 #245 — Closed / Done**

Dependency:
`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This authority may implement WP05 only if the authoritative Release 1.10 artifacts deterministically define the System Health read model, handoff representation, Streamlit presentation ownership, status vocabulary, exact production/test paths, and acceptance criteria.

If any required contract remains ambiguous, BLOCK BEFORE WP05 IMPLEMENTATION MUTATION and request the minimum GPT-5.6 Luna reconciliation.

---

# Accepted predecessor state

Carry forward unless current inspection contradicts it:

## WP03
- #244 Closed/Done.
- Infrastructure ActivitySource/Meter `AIQuantTradingResearch.Infrastructure`.
- activities `provider.operation`, `persistence.operation`.
- BCL-only Infrastructure observability.

## WP04
- #245 Closed/Done.
- no external exporter for Release 1.10.
- zero exporter/package/project-file mutations.
- BCL-only Worker/interop observations.
- Worker owns observability lifecycle.
- Streamlit remains independent.
- canonical JSON handoff unchanged.
- schema v4 unchanged.
- focused WP04 14/14 PASS.
- Infrastructure 186/186 PASS.
- Application 131/131 PASS.
- Architecture 21/21 PASS.
- relevant build 0 warnings / 0 errors.
- Gitleaks 8.30.1: 112 commits scanned, no leaks.
- no Worker/testhost/listener residue.
- Git mutations zero.

Accepted milestone entry state:
- milestone #59 Open;
- 4 open / 4 closed;
- #246–#249 Open/Backlog.

Emit:
`RELEASE 1.10 WP05 ENTRY: PASS`

---

# Release architecture to preserve

WP05 MUST preserve:

- .NET canonical pipeline ownership;
- canonical visualization JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- Streamlit does not read SQLite;
- Streamlit does not call providers;
- Streamlit does not supervise Worker;
- Streamlit does not own telemetry lifecycle;
- no external exporter;
- Release 1.8 JSON-over-stdio boundary remains separate;
- no live providers;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no schema migration;
- no direct UI bypass.

System Health must be a truthful read model/presentation of .NET-owned state, not a second observability subsystem.

---

# Mandatory pre-mutation determinism gate

Before changing any repository file, read:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. issue #246
6. WP04 lifecycle implementation
7. WP04 frozen downstream handoff
8. canonical visualization read-model/handoff implementation
9. Python parser/frame/presentation/Streamlit implementation
10. existing tests for those paths
11. current worktree status/diff.

Freeze from existing authority only:

- exact .NET System Health source of truth;
- exact .NET read-model path/symbol;
- exact canonical handoff path/symbol;
- exact representation/schema shape;
- whether health is embedded in an existing JSON document or another already-frozen artifact;
- exact bounded state vocabulary;
- exact enabled/disabled/available/failure/unknown/stale semantics as applicable;
- timestamp/age semantics, if any;
- provenance semantics;
- exact Python parser/frame/presentation paths;
- exact Streamlit path/component ownership;
- exact test paths;
- exact empty/missing/malformed/backward-compatible behavior;
- exact no-bypass rules;
- exact WP06 test handoff.

Do NOT invent a new JSON schema version.

Do NOT add a second handoff merely because it seems convenient.

Do NOT make Streamlit inspect Worker processes, telemetry listeners, SQLite, providers, or exporter internals.

Do NOT invent status vocabulary or freshness thresholds.

Emit only if deterministic:

`RELEASE 1.10 WP05 IMPLEMENTATION CONTRACT: DETERMINISTIC`

`RELEASE 1.10 WP05 .NET READ-MODEL OWNERSHIP: FROZEN`

`RELEASE 1.10 WP05 HANDOFF REPRESENTATION: FROZEN`

`RELEASE 1.10 WP05 STATUS VOCABULARY/SEMANTICS: FROZEN`

`RELEASE 1.10 WP05 PYTHON/STREAMLIT PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP05 TEST PATH OWNERSHIP: FROZEN`

If any required item is ambiguous:
- WP05 repository mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO;
- #246 remains Open/Backlog;
- WP06 does not start;
- request:
  **Release 1.10 WP05 — System Health Read Model and Streamlit Presentation Contract & Path Reconciliation Authority — GPT-5.6 Luna**

Then terminate BLOCKED.

---

# Worktree preservation

Preserve accepted Release 1.10 residue.

Do not reset, clean, checkout over, revert, stash, discard, or normalize unrelated changes.

Classify current changes into:
1. accepted planning/WP01–WP04 residue;
2. environment-only local validation state;
3. WP05 mutations.

WP05 may mutate only frozen paths/hunks.

---

# Phase 0 — Entry audit

Verify:
- repository/branch/HEAD;
- worktree attribution;
- #245 Closed/Done;
- #246 Open/Backlog;
- milestone #59 Open;
- #247–#249 unchanged;
- WP04 handoff present;
- schema v4 remains canonical;
- no external exporter.

Emit:
`RELEASE 1.10 WP05 ENTRY AUDIT: PASS`

---

# Phase 1 — Baseline validation

Run the exact focused baseline frozen for WP05, including applicable:
- .NET read-model/handoff tests;
- Python parser/frame/presentation tests;
- Streamlit tests;
- architecture/no-bypass tests;
- relevant builds.

Use the documented local Worker dev-signing flow only for generated artifacts if Windows Application Control requires it.

Approved security scanner:
Gitleaks 8.30.1.

Canonical invocation:
`gitleaks git . --redact --verbose`

Do not weaken PowerShell/Windows policy.

Emit:
`RELEASE 1.10 WP05 BASELINE: PASS`

---

# Phase 2 — Implement .NET System Health source/read model

Implement only the frozen .NET read-model contract.

Require:
- .NET remains source of truth;
- health derives only from frozen WP03/WP04 state;
- no synthetic claims unsupported by observed state;
- bounded status vocabulary;
- deterministic field semantics;
- no high-cardinality dimensions;
- no secrets/raw exception messages;
- no direct UI concerns in .NET domain/application layers;
- no schema migration.

If health includes time/freshness, use only the frozen timestamp/age semantics.

Emit:
`RELEASE 1.10 WP05 .NET SYSTEM HEALTH READ MODEL: PASS`

---

# Phase 3 — Canonical handoff integration

Integrate System Health only through the exact frozen handoff representation.

Require:
- existing canonical atomic handoff behavior preserved;
- schema v4 preserved;
- no second parallel health file/channel unless explicitly frozen;
- no direct Python call into Worker;
- no direct Python/Streamlit SQLite read;
- malformed/incomplete handoff behavior is deterministic;
- backward compatibility follows frozen semantics;
- provenance remains truthful.

Emit:
`RELEASE 1.10 WP05 SYSTEM HEALTH HANDOFF: PASS`

---

# Phase 4 — Python parsing and frame model

Implement only frozen Python parser/frame paths.

Require:
- parser validates exact health shape;
- missing/malformed fields follow frozen behavior;
- no guessing of health from unrelated fields;
- no process inspection;
- no SQLite access;
- no provider calls;
- no telemetry listener/exporter access;
- frame uses bounded deterministic values;
- presentation layer receives a stable typed/structured health model.

Emit:
`RELEASE 1.10 WP05 PYTHON HEALTH PARSER/FRAME: PASS`

---

# Phase 5 — Streamlit System Health presentation

Implement the frozen Streamlit System Health view.

Require:
- presentation only consumes the parsed canonical handoff/read model;
- status wording matches frozen vocabulary;
- disabled/unknown/stale/failure distinctions remain truthful;
- no claim of live external telemetry/exporter availability;
- no Worker supervision/control;
- no refresh loop outside existing governed behavior;
- no SQLite/provider access;
- no hidden network dependency;
- deterministic/replay/simulated disclosure remains visible/truthful where applicable;
- inaccessible/missing health state is presented according to frozen semantics.

Do not add operational controls unless explicitly frozen.

Emit:
`RELEASE 1.10 WP05 STREAMLIT SYSTEM HEALTH PRESENTATION: PASS`

---

# Phase 6 — Truthfulness/state matrix

Create or execute deterministic tests proving every frozen System Health state.

For each state verify:
- .NET source condition;
- handoff representation;
- Python parsed/frame representation;
- Streamlit label/message;
- provenance/freshness behavior;
- no contradictory state.

Cover all frozen states, including applicable:
- healthy/ready;
- disabled;
- unavailable;
- degraded/failure;
- unknown;
- stale;
- missing;
- malformed.

Do not test states not present in the frozen vocabulary as though they were canonical.

Emit:
`RELEASE 1.10 WP05 SYSTEM HEALTH TRUTHFULNESS MATRIX: PASS`

---

# Phase 7 — Focused permanent tests

Add/modify only frozen WP05 test paths.

Require deterministic coverage for applicable:
- .NET read model;
- JSON serialization/handoff;
- parser;
- frame;
- presentation;
- missing/malformed input;
- status vocabulary;
- freshness semantics;
- provenance;
- no-bypass;
- schema v4 preservation.

No external network service.

Emit:
`RELEASE 1.10 WP05 FOCUSED TESTS: PASS`

---

# Phase 8 — Backward compatibility and schema preservation

Prove:
- SQLite schema remains v4;
- no migration;
- canonical handoff compatibility matches frozen contract;
- older/missing health payload behavior is deterministic if supported;
- Release 1.9 visualization fields remain semantically unchanged;
- Release 1.8 JSON-over-stdio remains separate.

Emit:
`RELEASE 1.10 WP05 SCHEMA/BACKWARD COMPATIBILITY: PASS`

---

# Phase 9 — No-bypass architecture proof

Require:
- Streamlit reads only the canonical presentation input;
- Streamlit does not read SQLite;
- Streamlit does not call providers;
- Streamlit does not inspect Worker process state;
- Streamlit does not own telemetry lifecycle;
- Python does not create a parallel health authority;
- .NET remains canonical health source;
- no external exporter added.

Emit:
`RELEASE 1.10 WP05 ARCHITECTURE/NO-BYPASS: PASS`

---

# Phase 10 — Security/cardinality

Run Gitleaks 8.30.1 with:
`gitleaks git . --redact --verbose`

Require no unresolved leaks.

Audit health payload/presentation for:
- secrets;
- tokens;
- connection strings;
- raw paths;
- SQL;
- raw exception messages;
- arbitrary identifiers;
- uncontrolled symbols/targets;
- excessive internal implementation detail.

Emit:
`RELEASE 1.10 WP05 SECURITY/CARDINALITY: PASS`

`RELEASE 1.10 WP05 GITLEAKS SECURITY GATE: PASS`

---

# Phase 11 — Full affected validation

Run every suite/build frozen for WP05.

Report exact:
- focused WP05 tests;
- Infrastructure tests if affected;
- Application tests if affected;
- Worker tests if affected;
- Python tests;
- Streamlit/presentation tests;
- Architecture tests;
- relevant builds;
- warnings/errors;
- Gitleaks result.

Repair only WP05-attributable failures within frozen scope.

Emit:
`RELEASE 1.10 WP05 FULL AFFECTED VALIDATION: PASS`

---

# Phase 12 — Functional preservation

Prove no unintended change to:
- pipeline business behavior;
- provider retrieval;
- snapshot persistence/retrieval;
- WP03 observability;
- WP04 lifecycle;
- JSON handoff atomicity;
- deterministic/replay/simulated provenance;
- schema v4;
- Worker/Streamlit independence.

Emit:
`RELEASE 1.10 WP05 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 13 — Residue audit

Verify no WP05-owned:
- Worker process;
- testhost;
- Streamlit server;
- Python child process;
- ActivityListener;
- MeterListener;
- temporary health server;
- temporary results directory;
- locked handoff artifact.

Clean only WP05-owned temporary residue.

Emit:
`RELEASE 1.10 WP05 PROCESS/LISTENER/UI RESIDUE: CLEAN`

---

# Phase 14 — Exact path/hunk audit

Separate:
1. pre-existing Release 1.10 residue;
2. Luna WP04 planning residue;
3. WP04 implementation;
4. WP05 implementation/tests;
5. environment-only state.

Require WP05 mutations only in exact frozen paths/hunks.

Forbidden:
- project/package changes unless explicitly frozen;
- schema/migration changes;
- external exporter;
- direct Streamlit SQLite/provider/Worker inspection;
- WP06 implementation.

Emit:
`RELEASE 1.10 WP05 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP05 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 15 — WP06 handoff

WP06 is:
**Permanent Observability and No-Bypass Tests**

Freeze the exact implemented WP05 contracts WP06 must enforce, including:
- .NET health source;
- handoff shape;
- status vocabulary;
- parser/frame/presentation behavior;
- missing/malformed/freshness semantics;
- no-bypass constraints;
- schema v4;
- Worker/Streamlit independence.

Do not execute WP06.

Emit:
`RELEASE 1.10 WP05 DOWNSTREAM HANDOFF: PASS — WP06 READY`

---

# Phase 16 — Final acceptance matrix

Evaluate every WP05 criterion from:
- #246;
- Release 1.10 definition;
- execution plan;
- file manifest;
- `OPEN_TELEMETRY_SELECTION.md`;
- WP04 frozen handoff;
- this authority.

Every criterion must map to concrete evidence.

Only if all pass emit exactly:
`RELEASE 1.10 WP05 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

---

# Phase 17 — Mandatory GitHub completion

Immediately after acceptance:
1. re-read #246;
2. verify milestone #59 and Release=1.10;
3. verify unique Project #2 item;
4. close #246 if Open;
5. set Project Status to `Done` only if not already Done.

If issue-close automation transitions the Project item to Done, do not issue a redundant status mutation.

Count only explicit mutations actually performed.

Do NOT:
- close milestone #59;
- change Release;
- modify #247–#249;
- start WP06.

Emit:
`RELEASE 1.10 WP05 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 18 — GitHub post-verification

Require:
- #242–#246 Closed/Done;
- #246 Release=1.10;
- #246 milestone #59;
- milestone #59 remains Open;
- #247–#249 remain Open/Backlog unless independently changed.

Report actual milestone open/closed counts.

Emit:
`RELEASE 1.10 WP05 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 19 — Mutation accounting

Report exact ledger.

Repository:
- exact frozen WP05 paths/hunks only.

Project/package/schema:
- only if explicitly frozen; otherwise ZERO.

Git:
- ZERO.

GitHub:
- only explicit #246 completion mutations actually performed.

Emit:
`RELEASE 1.10 WP05 REPOSITORY MUTATIONS: ACCEPTED WP05 PATHS ONLY`

`RELEASE 1.10 WP05 PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`

`RELEASE 1.10 WP05 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP05 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success terminal

`RELEASE 1.10 WP05 — SYSTEM HEALTH READ MODEL AND STREAMLIT PRESENTATION AUTHORITY COMPLETE`

# Blocked outcome

BLOCK before implementation mutation if any required .NET read-model path, handoff representation, status vocabulary/semantics, Python/Streamlit ownership, test path, compatibility rule, or WP06 handoff remains ambiguous.

Request:
**Release 1.10 WP05 — System Health Read Model and Streamlit Presentation Contract & Path Reconciliation Authority — GPT-5.6 Luna**

If blocked before acceptance:
- #246 remains Open/Backlog;
- repository implementation mutations must remain zero if blocked at determinism gate;
- Git mutations zero;
- GitHub mutations zero;
- WP06 does not start.

If blocked after implementation begins for a genuine validation issue, preserve valid in-scope work and report exact state.

Exact blocked terminal:
`RELEASE 1.10 WP05 — SYSTEM HEALTH READ MODEL AND STREAMLIT PRESENTATION AUTHORITY BLOCKED`

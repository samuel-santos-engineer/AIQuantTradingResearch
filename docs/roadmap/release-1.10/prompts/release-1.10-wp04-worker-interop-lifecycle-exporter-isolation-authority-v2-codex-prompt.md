# Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority V2

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, and read-only/planning authority.
- **GPT-5.6 Terra** — PRIMARY implementation, validation execution, approved repository mutations, and mandatory WP lifecycle completion authority for this WP04 continuation.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; Sol does not silently replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10**

Work package:

**WP04 — Worker/Interop Lifecycle and Exporter Isolation**

Issue: **#245**

Milestone: **#59**

Project: **#2**

Predecessor:

**WP03 #244 — Closed / Done**

Dependency:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This is the resumed implementation authority after the successful Luna reconciliation:

**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Contract & Path Reconciliation Authority**

Do not reopen architectural choices that Luna froze.

---

# Accepted Luna reconciliation

Carry forward these frozen decisions unless current repository inspection proves the authoritative planning artifacts differ:

- **No external exporter for Release 1.10.**
- **Zero exporter/package/project-file mutations.**
- **BCL-only Worker/interop observations.**
- Exact Worker production paths are frozen in the reconciled planning artifacts.
- Exact `Program` path/symbol ownership is frozen.
- Exact Python invoker/interop path/symbol ownership is frozen.
- Exact focused lifecycle-test paths are frozen.
- Worker owns observability lifecycle.
- Streamlit remains independent.
- Canonical JSON handoff remains unchanged.
- SQLite schema v4 remains unchanged.
- WP05 handoff contract is frozen.
- #245 remains Open / Backlog.
- milestone #59 remains Open.
- Production, test, package, Git, GitHub mutations from Luna reconciliation: ZERO.
- Planning/architecture docs were updated only in the three authorized artifacts.

Accepted Luna markers include:

`RELEASE 1.10 WP04 EXPORTER SELECTION: FROZEN`

`RELEASE 1.10 WP04 PACKAGE/PROJECT CONTRACT: FROZEN`

`RELEASE 1.10 WP04 PRODUCTION PATH/SYMBOL OWNERSHIP: FROZEN`

`RELEASE 1.10 WP04 LIFECYCLE CONTRACT: FROZEN`

`RELEASE 1.10 WP04 CONFIGURATION CONTRACT: FROZEN`

`RELEASE 1.10 WP04 EXPORTER FAILURE-ISOLATION CONTRACT: FROZEN`

`RELEASE 1.10 WP04 RESOURCE/CARDINALITY/SECURITY CONTRACT: FROZEN`

`RELEASE 1.10 WP04 INTEROP/NO-BYPASS CONTRACT: FROZEN`

`RELEASE 1.10 WP04 TEST PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP04 → WP05 HANDOFF CONTRACT: FROZEN`

`RELEASE 1.10 WP04 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP04 → TERRA IMPLEMENTATION HANDOFF: PASS`

Emit:

`RELEASE 1.10 WP04 V2 TERRA ENTRY: PASS`

---

# Canonical predecessor evidence

Treat WP03 as accepted unless current inspection directly contradicts it:

- #244 Closed / Done;
- focused WP03 listener tests 25/25 PASS;
- Infrastructure 184/184 PASS;
- Application 131/131 PASS;
- Architecture 21/21 PASS;
- Infrastructure build 0 warnings / 0 errors;
- Gitleaks 8.30.1 clean over 112 commits;
- ActivitySource `AIQuantTradingResearch.Infrastructure`;
- Meter `AIQuantTradingResearch.Infrastructure`;
- activities `provider.operation`, `persistence.operation`;
- WP03 BCL-only instrumentation;
- ambient topology preserved.

Do not re-architect WP03.

---

# Release architecture to preserve

WP04 MUST preserve:

- .NET ownership of the canonical pipeline;
- canonical Release 1.9 visualization JSON handoff;
- SQLite schema v4;
- deterministic/replay/simulated provenance;
- Worker/Streamlit independence;
- Streamlit does not read SQLite;
- Streamlit does not call providers;
- Streamlit does not supervise Worker;
- Release 1.8 JSON-over-stdio boundary remains separate;
- no live providers;
- no trading;
- no ML;
- no backtesting;
- no parallel pipeline;
- no schema migration;
- no direct UI bypass;
- no external exporter;
- no new OpenTelemetry packages.

WP04 must not implement the WP05 System Health presentation.

---

# Mutation boundary

Use the exact path/symbol ownership frozen in:

1. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
3. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`

Do not guess paths from this authority if the reconciled artifacts are more specific.

Allowed:

- only frozen WP04 production paths/symbols;
- only frozen WP04 test paths/symbols.

Forbidden:

- `.csproj` mutation;
- package mutation;
- schema/migration mutation;
- external exporter;
- new telemetry package;
- unrelated Worker refactor;
- unrelated Python/Streamlit refactor;
- WP05 implementation;
- Release 1.8 boundary change;
- WP03 rework outside an exact compile/test repair caused by WP04 and within frozen ownership.

---

# Worktree preservation rule

The worktree may contain accepted Release 1.10 planning and WP01–WP03 implementation residue.

Do not:

- reset;
- clean;
- checkout over;
- revert;
- stash;
- discard;
- normalize unrelated changes.

Before editing, classify current changes into:

1. pre-existing accepted Release 1.10 residue;
2. Luna WP04 reconciliation planning residue;
3. WP04 Terra implementation.

WP04 may mutate only category 3 paths/hunks authorized by the reconciled manifest.

---

# Phase 0 — Entry audit and contract load

Read:

1. Release 1.10 definition;
2. reconciled execution plan;
3. reconciled file manifest;
4. `OPEN_TELEMETRY_SELECTION.md`;
5. #245;
6. #244 status;
7. milestone #59;
8. current Worker/Program/interop code;
9. frozen focused tests;
10. current `git status --short`;
11. current diff;
12. relevant existing configuration patterns.

Verify:

- #244 Closed/Done;
- #245 Open/Backlog;
- milestone #59 Open;
- #246–#249 unchanged;
- no external exporter is selected;
- zero package/project mutation is frozen;
- exact path/symbol ownership is deterministic.

Emit:

`RELEASE 1.10 WP04 CONTRACT CONSUMPTION: PASS`

`RELEASE 1.10 WP04 PATH/SYMBOL OWNERSHIP: FROZEN`

`RELEASE 1.10 WP04 DEPENDENCY CONTRACT: ZERO PACKAGE/PROJECT MUTATIONS`

---

# Phase 1 — Baseline validation

Run the focused baseline frozen by Luna.

At minimum execute the exact relevant:

- Worker tests;
- Worker/interop lifecycle tests;
- affected Infrastructure/Application tests;
- architecture/no-bypass tests;
- builds.

Use the existing documented local development signing flow only if Windows Application Control blocks generated Worker artifacts.

Do not add tracked signing configuration.

For security validation, approved scanner:

**Gitleaks 8.30.1**

Canonical command:

`gitleaks git . --redact --verbose`

If a PowerShell wrapper remains blocked by script-execution policy, do not weaken policy; use the exact approved underlying invocation.

Emit:

`RELEASE 1.10 WP04 BASELINE: PASS`

---

# Phase 2 — Implement Worker lifecycle ownership

Implement the exact frozen lifecycle contract.

Require:

- Worker owns the lifecycle;
- initialization occurs at the exact frozen Worker boundary;
- initialization is exactly once per owning process/lifecycle;
- no per-pipeline recreation;
- pipeline use cases do not own lifecycle;
- Streamlit does not own lifecycle;
- Python interop does not own lifecycle;
- shutdown/disposal follows frozen order;
- cancellation behavior matches frozen semantics;
- any explicit flush behavior matches Luna's frozen contract;
- no background listener/task/process residue.

Because Release 1.10 has no external exporter, do not introduce exporter-specific runtime infrastructure.

Emit:

`RELEASE 1.10 WP04 WORKER LIFECYCLE IMPLEMENTATION: PASS`

---

# Phase 3 — Implement BCL-only observation boundary

Implement only the BCL-only Worker/interop observations frozen by Luna.

Use only BCL APIs already authorized by the reconciled contract.

Do not add:

- `OpenTelemetry.*` package references;
- OTLP;
- console exporter;
- Prometheus exporter;
- Azure Monitor exporter;
- custom network exporter;
- external telemetry sink dependency.

Ensure observations remain bounded, sanitized, low-cardinality, and truthful.

Emit:

`RELEASE 1.10 WP04 BCL-ONLY OBSERVATION BOUNDARY: PASS`

---

# Phase 4 — Disabled/no-exporter behavior

Release 1.10's canonical state has no external exporter.

Implement and prove the exact frozen disabled/no-exporter semantics:

- Worker remains functional;
- core pipeline correctness is independent of telemetry export;
- persistence correctness unaffected;
- provider retrieval unaffected;
- JSON handoff unaffected;
- Streamlit unaffected;
- no retries toward a nonexistent exporter;
- no hidden external endpoint dependency;
- lifecycle state remains truthful for WP05 handoff.

Emit:

`RELEASE 1.10 WP04 NO-EXTERNAL-EXPORTER ISOLATION: PASS`

---

# Phase 5 — Configuration semantics

Implement only the frozen WP04 configuration surface.

Require:

- exact keys/types/defaults from Luna;
- deterministic default behavior;
- no secret settings;
- no endpoint configuration for an exporter that does not exist;
- invalid values handled exactly as frozen;
- no environment-variable sprawl;
- no high-cardinality metadata;
- no direct Streamlit ownership/configuration of Worker lifecycle.

Emit:

`RELEASE 1.10 WP04 CONFIGURATION IMPLEMENTATION: PASS`

---

# Phase 6 — Program/Worker/interop integration

Apply only the exact frozen integration points in:

- Worker;
- Program;
- Python invoker/interop path;

as recorded by Luna.

Prove:

- lifecycle initialization ownership is singular;
- Program wiring does not create duplicate lifecycle instances;
- Python invocation observes the frozen boundary only;
- no direct Python ownership of .NET telemetry infrastructure;
- interop exception/cancellation behavior remains truthful;
- canonical JSON payload semantics remain unchanged.

Emit:

`RELEASE 1.10 WP04 PROGRAM/WORKER/INTEROP INTEGRATION: PASS`

---

# Phase 7 — Focused deterministic tests

Add/modify only the exact frozen lifecycle test paths.

Prove every Luna-frozen case, including applicable:

- exactly-once initialization;
- correct Worker ownership;
- no per-operation recreation;
- no Streamlit ownership;
- no Python ownership;
- no external exporter;
- disabled/no-exporter path;
- lifecycle shutdown/disposal;
- cancellation;
- failure isolation;
- invalid configuration;
- interop behavior;
- bounded status/failure categories;
- no process/listener/task residue.

Use deterministic local test mechanisms only.

No external network dependency.

Emit:

`RELEASE 1.10 WP04 FOCUSED LIFECYCLE TESTS: PASS`

---

# Phase 8 — Failure-isolation proof

Exercise every deterministic failure mode frozen by Luna.

Even without an external exporter, prove the frozen lifecycle/observation failure semantics.

Require:

- core pipeline result/exception contract preserved;
- no corrupted persistence;
- no corrupted provider retrieval;
- no corrupted JSON handoff;
- no false readiness/health state;
- no unbounded retry;
- no process hang;
- no leaked listener/task/thread;
- bounded sanitized failure evidence.

Emit:

`RELEASE 1.10 WP04 FAILURE ISOLATION: PASS`

---

# Phase 9 — Interop/no-bypass verification

Prove:

- canonical JSON handoff unchanged;
- schema v4 unchanged;
- Release 1.8 JSON-over-stdio unchanged;
- Streamlit does not supervise Worker;
- Streamlit does not read SQLite;
- Streamlit does not call providers;
- Streamlit does not own lifecycle;
- no parallel pipeline;
- no external exporter dependency.

Emit:

`RELEASE 1.10 WP04 INTEROP/NO-BYPASS: PASS`

---

# Phase 10 — Security/cardinality validation

Run the approved Gitleaks gate.

Canonical:

`gitleaks git . --redact --verbose`

using **Gitleaks 8.30.1**.

Require no unresolved leaks.

Manually inspect WP04 observations/configuration for forbidden values:

- credentials;
- API tokens;
- connection strings;
- raw filesystem paths;
- SQL;
- payloads;
- arbitrary exception messages;
- symbols/targets as uncontrolled dimensions;
- GUID/request IDs;
- timestamps as cardinality dimensions.

Emit:

`RELEASE 1.10 WP04 SECURITY/CARDINALITY: PASS`

`RELEASE 1.10 WP04 GITLEAKS SECURITY GATE: PASS`

---

# Phase 11 — Architecture validation

Run the frozen architecture/no-bypass suite.

Require preservation of:

- .NET canonical ownership;
- Worker/Streamlit independence;
- schema v4;
- JSON handoff;
- Release 1.8 boundary separation;
- WP03 telemetry contract;
- zero external exporter;
- zero package/project mutation.

Emit:

`RELEASE 1.10 WP04 ARCHITECTURE CONTRACT: PASS`

---

# Phase 12 — Full affected validation

Run all suites/builds frozen for WP04.

Report exact results.

At minimum reconcile:

- focused WP04 lifecycle tests;
- Worker tests;
- Infrastructure tests if affected;
- Application tests if affected;
- architecture tests;
- relevant builds;
- Gitleaks.

If Windows Application Control requires the documented local Worker signing flow, reapply it only to generated artifacts and use `--no-build` where necessary.

Do not convert the signing workaround into tracked repository configuration.

Emit:

`RELEASE 1.10 WP04 FULL AFFECTED VALIDATION: PASS`

---

# Phase 13 — Functional preservation

Prove no unintended change to:

- pipeline business behavior;
- historical retrieval;
- snapshot persistence/retrieval;
- deterministic/replay/simulated provenance;
- canonical JSON handoff;
- schema v4;
- Streamlit independence;
- Worker cancellation/exception semantics beyond the frozen lifecycle contract.

Emit:

`RELEASE 1.10 WP04 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

---

# Phase 14 — Residue audit

After validation, verify no WP04-owned residue:

- Worker process;
- testhost process;
- ActivityListener;
- MeterListener;
- lifecycle background task/thread;
- temporary IPC/listener;
- locked telemetry resource.

Emit:

`RELEASE 1.10 WP04 PROCESS/LISTENER RESIDUE: CLEAN`

---

# Phase 15 — Exact path/hunk audit

Audit the complete WP04 Terra delta.

Separate:

1. pre-existing Release 1.10 residue;
2. Luna reconciliation documentation;
3. WP04 implementation/test changes;
4. local environment-only signing/tool state.

Require:

- implementation/test changes only in exact Luna-frozen paths;
- zero `.csproj` mutation;
- zero package mutation;
- zero schema/migration mutation;
- zero WP05 implementation;
- zero unrelated Program/Worker/Python refactor.

Emit:

`RELEASE 1.10 WP04 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP04 FORBIDDEN TARGET AUDIT: PASS`

---

# Phase 16 — WP05 handoff verification

Reconcile implementation with Luna's frozen:

`RELEASE 1.10 WP04 → WP05 HANDOFF CONTRACT: FROZEN`

Report the exact downstream facts WP05 may consume.

Require:

- source of truth remains .NET-owned;
- bounded lifecycle/status vocabulary;
- no direct Streamlit inspection of Worker internals;
- no direct exporter access because Release 1.10 has no external exporter;
- no Worker supervision from Streamlit;
- no WP05 presentation implementation in WP04.

Emit:

`RELEASE 1.10 WP04 DOWNSTREAM HANDOFF: PASS — WP05 READY`

Do not execute WP05.

---

# Phase 17 — Final acceptance matrix

Evaluate every WP04 criterion from:

- #245;
- Release 1.10 definition;
- reconciled execution plan;
- reconciled file manifest;
- reconciled `OPEN_TELEMETRY_SELECTION.md`;
- WP03 inherited contract;
- Luna WP04 reconciliation;
- this Terra authority.

Every criterion must map to concrete evidence.

Only if every criterion passes emit exactly:

`RELEASE 1.10 WP04 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

---

# Phase 18 — Mandatory GitHub WP completion

Immediately after:

`RELEASE 1.10 WP04 ACCEPTANCE: PASS`

re-read #245 and its unique Project #2 item.

Require:

- issue identity #245;
- milestone #59;
- Release=1.10;
- unique Project #2 item.

Then:

1. close #245 if still Open;
2. set Project #2 Status to `Done` only if it is not already Done.

If issue-close automation transitions the Project item automatically, do not issue a redundant Status mutation.

Count only explicit mutations actually performed.

Do NOT:

- close milestone #59;
- change Release;
- modify #246–#249;
- start WP05.

Emit:

`RELEASE 1.10 WP04 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 19 — GitHub post-verification

Re-read GitHub and require:

- #242 Closed/Done;
- #243 Closed/Done;
- #244 Closed/Done;
- #245 Closed/Done;
- #245 Release=1.10;
- #245 milestone #59;
- milestone #59 remains Open;
- #246–#249 remain Open/Backlog unless independently changed by another valid authority.

Report actual milestone open/closed counts.

Emit:

`RELEASE 1.10 WP04 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 20 — Exact mutation accounting

Report final ledger.

## Repository

Only exact Luna-frozen WP04 production/test paths.

## Project/package

ZERO.

## Git

ZERO.

## GitHub

Only explicit WP04 lifecycle completion mutations actually performed.

Emit:

`RELEASE 1.10 WP04 REPOSITORY MUTATIONS: ACCEPTED WP04 PATHS ONLY`

`RELEASE 1.10 WP04 PROJECT/PACKAGE MUTATIONS: ZERO`

`RELEASE 1.10 WP04 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP04 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WP04 V2 TERRA ENTRY: PASS`

`RELEASE 1.10 WP04 CONTRACT CONSUMPTION: PASS`

`RELEASE 1.10 WP04 PATH/SYMBOL OWNERSHIP: FROZEN`

`RELEASE 1.10 WP04 DEPENDENCY CONTRACT: ZERO PACKAGE/PROJECT MUTATIONS`

`RELEASE 1.10 WP04 BASELINE: PASS`

`RELEASE 1.10 WP04 WORKER LIFECYCLE IMPLEMENTATION: PASS`

`RELEASE 1.10 WP04 BCL-ONLY OBSERVATION BOUNDARY: PASS`

`RELEASE 1.10 WP04 NO-EXTERNAL-EXPORTER ISOLATION: PASS`

`RELEASE 1.10 WP04 CONFIGURATION IMPLEMENTATION: PASS`

`RELEASE 1.10 WP04 PROGRAM/WORKER/INTEROP INTEGRATION: PASS`

`RELEASE 1.10 WP04 FOCUSED LIFECYCLE TESTS: PASS`

`RELEASE 1.10 WP04 FAILURE ISOLATION: PASS`

`RELEASE 1.10 WP04 INTEROP/NO-BYPASS: PASS`

`RELEASE 1.10 WP04 SECURITY/CARDINALITY: PASS`

`RELEASE 1.10 WP04 GITLEAKS SECURITY GATE: PASS`

`RELEASE 1.10 WP04 ARCHITECTURE CONTRACT: PASS`

`RELEASE 1.10 WP04 FULL AFFECTED VALIDATION: PASS`

`RELEASE 1.10 WP04 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.10 WP04 PROCESS/LISTENER RESIDUE: CLEAN`

`RELEASE 1.10 WP04 PATH OWNERSHIP: PASS`

`RELEASE 1.10 WP04 FORBIDDEN TARGET AUDIT: PASS`

`RELEASE 1.10 WP04 DOWNSTREAM HANDOFF: PASS — WP05 READY`

`RELEASE 1.10 WP04 ACCEPTANCE: PASS`

`RELEASE 1.10 WP04 GITHUB WORK-PACKAGE COMPLETION: PASS`

`RELEASE 1.10 WP04 GITHUB COMPLETION POST-VERIFY: PASS`

`RELEASE 1.10 WP04 REPOSITORY MUTATIONS: ACCEPTED WP04 PATHS ONLY`

`RELEASE 1.10 WP04 PROJECT/PACKAGE MUTATIONS: ZERO`

`RELEASE 1.10 WP04 GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP04 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Exact success terminal:

`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION AUTHORITY V2 COMPLETE`

---

# Blocked outcome

BLOCK only for a genuine implementation/validation/lifecycle blocker inside this now-frozen contract.

Do NOT block for exporter/package ambiguity: Luna resolved those choices.

Do NOT invent an external exporter or package as a repair.

If blocked before:

`RELEASE 1.10 WP04 ACCEPTANCE: PASS`

then:

- #245 remains Open/Backlog;
- GitHub mutations ZERO;
- Git mutations ZERO;
- WP05 remains blocked;
- preserve valid WP04 work.

If blocked after acceptance during lifecycle completion, report exact partial GitHub state and make no unrelated repair mutations.

Exact blocked terminal:

`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION AUTHORITY V2 BLOCKED`

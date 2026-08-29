# Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, and read-only/planning authority.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations, merge/publication, and PRIMARY execution authority for WP04.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

## Authority identity

Release: **1.10**
WP: **WP04 — Worker/Interop Lifecycle and Exporter Isolation**
Issue: **#245**
Milestone: **#59**
Project: **#2**
Predecessor: **WP03 #244 — Closed / Done**

Dependency: `WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This authority may implement WP04 only if the frozen Release 1.10 planning artifacts deterministically define Worker/interop ownership, exporter selection/dependency authority, lifecycle behavior, isolation semantics, exact production/test paths, and acceptance contract.

If not deterministic, BLOCK BEFORE WP04 IMPLEMENTATION MUTATION and request the minimum GPT-5.6 Luna reconciliation.

## Accepted predecessor evidence

Treat as accepted unless current inspection contradicts it:

- #244 Closed/Done and WP03 acceptance PASS.
- WP03 focused listener tests 25/25.
- Infrastructure 184/184.
- Application 131/131.
- Architecture 21/21.
- Infrastructure build 0 warnings / 0 errors.
- Gitleaks 8.30.1 clean over 112 commits.
- Infrastructure ActivitySource/Meter: `AIQuantTradingResearch.Infrastructure`.
- Activities: `provider.operation`, `persistence.operation`.
- BCL-only WP03 instrumentation and ambient topology.
- Git mutations zero.
- Milestone #59 Open, 5 open / 3 closed.
- #245–#249 Open/Backlog.

## Architecture to preserve

Preserve .NET canonical pipeline ownership, canonical visualization JSON handoff, SQLite schema v4, deterministic/replay/simulated provenance, Worker/Streamlit independence, no Streamlit SQLite/provider/Worker-supervision access, Release 1.8 JSON-over-stdio separation, and no live provider/trading/ML/backtesting/parallel pipeline/schema migration/direct UI bypass.

Do not pre-implement WP05 System Health presentation.

## Mandatory pre-mutation determinism gate

Before changing any repository file, read:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. issue #245
6. WP03 implementation/contracts
7. Worker/interop production code
8. Worker/interop tests
9. relevant Worker project/package files
10. current worktree status/diff.

Freeze from existing authority only:

- exact production path(s);
- exact interop path(s), if owned;
- exact test path(s);
- exact exporter technology;
- exact exporter packages/versions, if any;
- exact project-file authority, if any;
- telemetry provider ownership;
- initialization/start lifecycle;
- flush/shutdown/disposal lifecycle;
- failure isolation;
- disabled/unavailable/failing exporter behavior;
- configuration surface/defaults;
- bounded attributes/resource metadata;
- no-bypass constraints;
- WP05 handoff.

Do NOT infer OTLP, console, Prometheus, Azure Monitor, or another exporter. Do NOT add `OpenTelemetry.*` packages unless exact package authority is frozen.

Emit only if deterministic:

`RELEASE 1.10 WP04 IMPLEMENTATION CONTRACT: DETERMINISTIC`
`RELEASE 1.10 WP04 PRODUCTION PATH OWNERSHIP: FROZEN`
`RELEASE 1.10 WP04 TEST PATH OWNERSHIP: FROZEN`
`RELEASE 1.10 WP04 EXPORTER/DEPENDENCY CONTRACT: FROZEN`
`RELEASE 1.10 WP04 LIFECYCLE/ISOLATION CONTRACT: FROZEN`

If any required item is ambiguous: WP04 repository/Git/GitHub mutations ZERO, #245 stays Open/Backlog, WP05 does not start, and request:
**Release 1.10 WP04 — Worker/Interop Lifecycle and Exporter Isolation Contract & Path Reconciliation Authority — GPT-5.6 Luna**.

## Worktree preservation

Preserve accepted uncommitted Release 1.10 residue. Do not reset, clean, checkout over, revert, stash, discard, or normalize unrelated changes. Classify existing changes before editing. WP04 may mutate only frozen paths/hunks.

## Phase 0 — Entry audit

Verify repository/branch/HEAD, worktree attribution, #244 Closed/Done, #245 Open/Backlog, milestone #59 Open, #246–#249 untouched, and predecessor contract present.

For Windows Application Control, only the already-approved documented local development signing flow may be used for generated Worker artifacts; it must not become a tracked signing configuration change.

Emit:
`RELEASE 1.10 WP04 ENTRY AUDIT: PASS`

## Phase 1 — Baseline

Run the focused baseline frozen for WP04: relevant Worker/Infrastructure/interop tests, Application tests where applicable, architecture/no-bypass tests, and relevant builds.

If Windows Application Control blocks generated Worker artifacts, use only the documented local dev signing mechanism and accepted `--no-build` validation pattern where applicable.

For Gitleaks, approved tool is 8.30.1 and canonical command is `gitleaks git . --redact --verbose`; do not weaken script/security policy.

Emit:
`RELEASE 1.10 WP04 BASELINE: PASS`

## Phase 2 — Worker observability lifecycle

Implement only the frozen lifecycle behavior. Prove, as applicable:

- initialization exactly once per owning Worker/process lifecycle;
- correct initialization boundary;
- Streamlit does not own provider/exporter lifecycle;
- pipeline use cases do not own/recreate global telemetry infrastructure;
- deterministic shutdown/disposal;
- bounded truthful flush behavior;
- correct cancellation/shutdown semantics;
- no process/listener leak.

Emit:
`RELEASE 1.10 WP04 WORKER OBSERVABILITY LIFECYCLE: PASS`

## Phase 3 — Exporter isolation

Implement only the exact frozen exporter/isolation contract. Exporter behavior must not become a correctness dependency of pipeline execution, persistence, provider retrieval, JSON handoff, Worker core operation, or Streamlit availability.

Prove frozen disabled, successful, unavailable/failing, initialization-failure, export-failure, and shutdown/flush-failure behavior.

Emit:
`RELEASE 1.10 WP04 EXPORTER ISOLATION: PASS`

## Phase 4 — Configuration

Implement only frozen configuration. Require deterministic defaults, no committed secrets, no unauthorized hard-coded endpoints, no environment-variable sprawl, bounded metadata, truthful disabled/default behavior, and frozen invalid-config semantics.

Emit:
`RELEASE 1.10 WP04 CONFIGURATION CONTRACT: PASS`

## Phase 5 — Interop preservation

Prove canonical JSON handoff remains atomic/truthful, schema v4 unchanged, Release 1.8 boundary separate, Streamlit does not supervise Worker or own exporter/provider lifecycle, and no telemetry UI bypass exists.

Emit:
`RELEASE 1.10 WP04 INTEROP BOUNDARY: PASS`

## Phase 6 — Focused deterministic tests

Use only frozen test paths. Prove applicable one-time initialization, ownership, disabled/success/failure exporter behavior, initialization failure, flush/shutdown/disposal, cancellation, no duplicate creation, no per-pipeline recreation, no Streamlit ownership, no-listener/no-exporter behavior, bounded configuration/resource attributes, and unchanged core result/exception semantics.

No external network dependency in permanent tests unless explicitly frozen.

Emit:
`RELEASE 1.10 WP04 FOCUSED TESTS: PASS`

## Phase 7 — Dependency audit

If package/project mutations are frozen, verify exact package IDs, versions, project ownership, and no unselected exporter family/drift. If zero dependency mutations are authorized, require zero.

Emit:
`RELEASE 1.10 WP04 DEPENDENCY CONTRACT: PASS`

## Phase 8 — Failure isolation

Exercise deterministic failures. Require core pipeline/persistence/provider/JSON correctness, no false health state, no unbounded retry, no hang, no leaked task/thread/listener, and bounded sanitized failure evidence.

Emit:
`RELEASE 1.10 WP04 FAILURE ISOLATION: PASS`

## Phase 9 — Security/cardinality

Run Gitleaks over WP04-mutated tracked paths/repository as required. Manually inspect exporter/config/resource attributes for secrets, credentials, tokens, connection strings, uncontrolled endpoints, raw payloads/messages, and high-cardinality IDs.

Emit:
`RELEASE 1.10 WP04 SECURITY/CARDINALITY: PASS`

## Phase 10 — Architecture/no-bypass

Run frozen architecture/no-bypass validation and preserve .NET ownership, Worker/Streamlit independence, no direct UI SQLite/provider access, no parallel pipeline, schema v4, canonical JSON handoff, Release 1.8 separation, and WP03 telemetry contract.

Emit:
`RELEASE 1.10 WP04 ARCHITECTURE/NO-BYPASS: PASS`

## Phase 11 — Full affected validation

Run all frozen WP04 builds/suites and report exact counts/warnings/errors/failures/skips. Use local dev signing only for generated artifacts if required. Repair only WP04-attributable failures inside frozen scope.

Emit:
`RELEASE 1.10 WP04 FULL AFFECTED VALIDATION: PASS`

## Phase 12 — Functional preservation

Prove no change to pipeline business behavior, historical retrieval, snapshot persistence/retrieval, provenance, JSON handoff, schema v4, Streamlit independence, or exception/cancellation semantics except frozen WP04 lifecycle ownership.

Emit:
`RELEASE 1.10 WP04 FUNCTIONAL BEHAVIOR PRESERVATION: PASS`

## Phase 13 — Residue

Prove no Worker/testhost/exporter worker/thread/task/ActivityListener/MeterListener/temporary endpoint or locked telemetry resource remains.

Emit:
`RELEASE 1.10 WP04 PROCESS/EXPORTER/LISTENER RESIDUE: CLEAN`

## Phase 14 — Path/hunk audit

Separate pre-existing Release 1.10 residue, environment-only signing/tooling state, and WP04 mutations. Require only frozen WP04 paths/hunks and no WP05 implementation.

Emit:
`RELEASE 1.10 WP04 PATH OWNERSHIP: PASS`

## Phase 15 — WP05 handoff

Freeze only WP04-established downstream facts: lifecycle state, exporter enabled/disabled/available/failure semantics, bounded status vocabulary, source of truth, and no direct exporter/Worker supervision from Streamlit. Do not implement WP05.

Emit:
`RELEASE 1.10 WP04 DOWNSTREAM HANDOFF: PASS — WP05 READY`

## Phase 16 — Acceptance

Evaluate every criterion from #245, release definition, execution plan, manifest, selection doc, WP03 inheritance, and frozen WP04 contract.

Only if every criterion passes emit:
`RELEASE 1.10 WP04 ACCEPTANCE: PASS`

No GitHub lifecycle mutation before this marker.

## Phase 17 — Mandatory GitHub completion

After acceptance, re-read #245 and its unique Project #2 item. Close #245 and set Status Done if needed. If issue-close automation already transitions it to Done, do not make a redundant Status mutation.

Count only explicit mutations actually performed. Keep milestone #59 Open, Release=1.10 unchanged, #246–#249 unchanged, and do not start WP05.

Emit:
`RELEASE 1.10 WP04 GITHUB WORK-PACKAGE COMPLETION: PASS`

## Phase 18 — Post-verification

Require #242–#245 Closed/Done, #245 Release=1.10 and milestone #59, milestone #59 Open, #246–#249 Open/Backlog unless independently changed. Report actual milestone counts.

Emit:
`RELEASE 1.10 WP04 GITHUB COMPLETION POST-VERIFY: PASS`

## Phase 19 — Mutation accounting

Repository: frozen WP04 paths/hunks only.
Git: ZERO.
GitHub: accepted completion mutations actually required/performed.

Emit:
`RELEASE 1.10 WP04 REPOSITORY MUTATIONS: ACCEPTED WP04 PATHS ONLY`
`RELEASE 1.10 WP04 GIT MUTATIONS: ZERO`
`RELEASE 1.10 WP04 GITHUB MUTATIONS: ACCEPTED COMPLETION MUTATIONS ONLY`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

## Required terminal

Success:
`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION AUTHORITY COMPLETE`

Blocked:
`RELEASE 1.10 WP04 — WORKER/INTEROP LIFECYCLE AND EXPORTER ISOLATION AUTHORITY BLOCKED`

BLOCK before implementation mutation for any unresolved production/test path, exporter, package/version/project, lifecycle, isolation, configuration, or WP05-handoff ambiguity. Preserve valid work, keep #245 Open/Backlog, Git/GitHub mutations zero, and do not start WP05.

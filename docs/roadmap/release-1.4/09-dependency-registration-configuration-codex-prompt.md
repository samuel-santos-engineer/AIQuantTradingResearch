# Release 1.4 --- WP09 Dependency Registration & Configuration --- Codex Authority

## Mission

Execute **Release 1.4 --- WP09: Dependency Registration & Configuration
--- GitHub issue #161**.

WP09 wires the already accepted Release 1.4 feature-generation graph
into composition and establishes the minimum deterministic
request/configuration boundary required for later bounded Worker
execution. It must not execute feature generation.

Recommended model: **GPT-5.6 Terra**.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Before any mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
6.  WP04 --- Feature Domain/Application Model authority/result
7.  WP05 --- Feature Generation Contracts authority/result
8.  WP06 --- Deterministic Feature Computation authority/result
9.  WP06 identity clarification authority/result
10. WP07 --- Feature Validation & Failure Mapping authority/result
11. WP08 --- Feature Generation Integration authority/result
12. Current Application and Infrastructure dependency-registration code
13. Current Worker configuration/composition code, including Release 1.3
    accepted patterns
14. Existing Release 1.2 snapshot-store registrations and configuration
15. Current tests and architecture rules
16. Current GitHub state for #161 and successor #162

Repository truth and accepted authorities govern. Do not redesign
accepted WP04--WP08 semantics merely to simplify DI.

------------------------------------------------------------------------

## 2. Starting-State Gates

Before implementation verify and report:

-   branch `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   cumulative Release 1.4 paths are expected and classified;
-   #153--#160 are Closed/Done;
-   #161 is OPEN / Backlog;
-   #162 is OPEN / Backlog and untouched;
-   milestone #45 is OPEN;
-   no Release 1.5 implementation has started;
-   SQLite schema remains exactly version `2`;
-   permanent baseline remains 197 tests unless repository truth shows
    an accepted predecessor delta;
-   production dependency graph remains:
    -   Domain → none
    -   Application → Domain
    -   Infrastructure → Application
    -   Worker → Application, Infrastructure.

Run `eng/verify.ps1 -Configuration Release` before mutation. If the
baseline fails for an unrelated reason, stop.

Only after all starting gates pass may #161 move Backlog → In Progress.

------------------------------------------------------------------------

## 3. WP09 Objective

Register the accepted feature-generation services and establish
deterministic construction of the feature-generation request needed by
WP10.

The intended composition boundary is:

``` text
configuration/request inputs
    → deterministic FeatureGenerationRequest construction
    → resolvable IFeatureGenerationUseCase graph
```

WP09 proves composition. It does **not** invoke the feature-generation
use case.

------------------------------------------------------------------------

## 4. Accepted Graph to Register

Reconcile actual WP04--WP08 types and register only what is required to
resolve the accepted graph.

Expected concepts include:

-   `IFeatureGenerationUseCase` → WP08 implementation;
-   `IFeatureComputer` → WP06 deterministic implementation;
-   any accepted stateless validator/identity component only if it is an
    injectable service in repository truth;
-   existing `IDatasetSnapshotStore` registration from Release 1.2,
    reused rather than duplicated.

Do not invent interfaces solely for DI aesthetics.

Do not create a second snapshot-store registration or parallel
persistence abstraction.

------------------------------------------------------------------------

## 5. Lifetime Decisions

Choose the narrowest lifetimes consistent with existing repository
conventions and service state.

Requirements:

-   no mutable cross-execution semantic state;
-   no hidden caching;
-   no service lifetime that changes identity or evidence semantics;
-   no eager execution during registration or resolution;
-   no eager SQLite connection creation solely due to feature-service
    resolution.

Prefer consistency with Release 1.3 Application registration patterns.

Document each new lifetime decision in the execution report.

------------------------------------------------------------------------

## 6. Configuration Boundary

WP09 may add only the minimum configuration/request-construction surface
explicitly required by the accepted Release 1.4 plan and manifest.

The feature request must deterministically identify:

-   the sole accepted feature definition `simple-return-lag-1-v1`;
-   exact `DatasetSnapshotIdentity`;
-   exact `DatasetVersion`.

Do not add semantic knobs for:

-   formula;
-   lag;
-   rounding;
-   arithmetic mode;
-   topology;
-   identity scheme;
-   retries;
-   scheduling;
-   persistence.

Those semantics are code-owned authorities, not runtime configuration.

------------------------------------------------------------------------

## 7. Request Construction

Provide a narrow request factory/configuration helper only if required
by the accepted plan/manifest.

It must:

-   construct the existing WP05 `FeatureGenerationRequest`;
-   use the built-in accepted feature definition;
-   parse/validate snapshot identity and version deterministically;
-   reject missing or malformed required inputs before feature
    execution;
-   remain culture-independent;
-   avoid current time, machine state, random values, paths, or provider
    state.

Do not compute feature identities in Worker configuration code if
accepted Application components already own identity semantics.

------------------------------------------------------------------------

## 8. Configuration Failure Behavior

Invalid configuration must fail before execution.

At minimum reconcile and prove behavior for applicable cases:

-   missing snapshot identity;
-   malformed snapshot fingerprint;
-   missing/invalid dataset version;
-   unsupported or user-configurable feature definition must not be
    introduced;
-   malformed values must not silently default to another
    snapshot/version.

Use existing repository configuration conventions where possible.

Do not turn configuration errors into feature semantic failures after
execution begins.

------------------------------------------------------------------------

## 9. Resolution Must Be Side-Effect Free

Building and resolving the service graph must not:

-   call `IFeatureGenerationUseCase`;
-   perform snapshot lookup;
-   create feature evidence;
-   compute feature values;
-   create or mutate SQLite database files;
-   invoke provider/network code;
-   persist anything;
-   mutate catalogs;
-   start a host loop.

Prove this explicitly.

If the existing SQLite connection factory is configuration-only until
use, preserve that behavior.

------------------------------------------------------------------------

## 10. Provider and Network Isolation

Feature generation begins from persisted Release 1.2 snapshot evidence.

WP09 must not register or invoke a new provider path for features.

No Twelve Data call, HTTP request, credential validation, or acquisition
orchestration is authorized.

Existing provider registrations may remain as predecessor composition if
already part of the Worker graph, but WP09 must not cause them to
execute.

------------------------------------------------------------------------

## 11. Release 1.3 Pipeline Protection

Do not alter the Release 1.3 pipeline registrations, semantics,
configuration, or one-shot behavior except for a manifest-authorized
composition-root edit strictly necessary to add the separate feature
graph.

Feature generation is not a sixth pipeline stage.

Do not automatically chain feature generation after pipeline execution.

WP10 owns the later bounded Worker trigger.

------------------------------------------------------------------------

## 12. Persistence and Schema Protection

SQLite remains schema version `2`.

Do not add:

-   feature tables;
-   feature catalog;
-   feature cache;
-   run history;
-   migrations;
-   schema v3;
-   eager database initialization for feature resolution.

Reuse the accepted snapshot-store abstraction only.

------------------------------------------------------------------------

## 13. WP10 Protection

WP10 --- One-Shot Worker Feature Execution --- owns actual invocation
and presentation.

WP09 must not:

-   execute feature generation from `Program.cs`;
-   print feature evidence;
-   define feature success/failure exit codes;
-   add loops/timers;
-   run the feature use case after DI resolution;
-   alter Worker control flow beyond minimum configuration/composition
    support authorized by the manifest.

If a Worker file is touched, it must remain
configuration/composition-only.

------------------------------------------------------------------------

## 14. Expected File Surface

Use the Release 1.4 file manifest as hard authority.

Prefer minimal changes to existing composition/configuration files
rather than creating parallel registration systems.

Likely categories:

-   Application dependency registration;
-   narrowly scoped Worker feature request/configuration helper;
-   Infrastructure registration only if repository truth proves an
    accepted existing component is not currently exposed and the
    manifest authorizes it.

Do not expand beyond manifest-authorized paths.

If the manifest and actual composition needs conflict materially, stop.

------------------------------------------------------------------------

## 15. Permanent Test Boundary

Do not add permanent tests in WP09.

WP11 and WP12 own permanent semantic/composition coverage.

A removable offline DI/configuration probe is authorized when useful.

The probe must prove real service resolution using production
registrations without invoking feature generation. Remove it before
final validation.

Permanent test delta must remain `0`.

------------------------------------------------------------------------

## 16. Required WP09 Acceptance Matrix

Prove all applicable cases:

1.  exactly one effective `IFeatureGenerationUseCase` registration
    exists;
2.  it resolves to the accepted WP08 implementation;
3.  exactly one effective `IFeatureComputer` registration exists;
4.  it resolves to the accepted WP06 implementation;
5.  existing `IDatasetSnapshotStore` registration is reused;
6.  service lifetimes match accepted design and repository conventions;
7.  the complete feature-generation graph resolves successfully;
8.  graph resolution does not execute feature generation;
9.  graph resolution does not perform snapshot lookup;
10. graph resolution does not create feature evidence;
11. graph resolution does not create/mutate a database;
12. graph resolution does not invoke provider/network code;
13. valid configuration constructs the exact accepted request;
14. snapshot identity is preserved exactly;
15. dataset version is preserved exactly;
16. built-in definition is `simple-return-lag-1-v1`;
17. configuration parsing is culture-independent;
18. missing snapshot identity fails before execution;
19. malformed snapshot identity fails before execution;
20. missing/invalid dataset version fails before execution;
21. no user-configurable formula/lag/rounding semantics exist;
22. SQLite remains schema v2;
23. Release 1.3 pipeline behavior remains unchanged;
24. WP10 execution has not started.

Do not claim a case without evidence.

------------------------------------------------------------------------

## 17. Architecture and Dependency Protection

Preferred deltas:

-   Domain: `0`
-   Application: minimal registration delta
-   Infrastructure: `0` preferred
-   Worker: minimal configuration/composition delta
-   packages: `0`
-   project references: `0`
-   schema: `0`
-   permanent tests: `0`

The production graph must remain unchanged and acyclic.

Application must not reference Infrastructure or Worker.

------------------------------------------------------------------------

## 18. Validation Requirements

After implementation:

1.  run targeted build and removable DI/configuration probe if useful;
2.  remove all temporary probes;
3.  run `git diff --check`;
4.  run `git diff --cached --check`;
5.  directly inspect trailing whitespace in untracked governed files;
6.  run `eng/verify.ps1 -Configuration Release`;
7.  confirm:
    -   build warnings `0`;
    -   build errors `0`;
    -   all permanent tests pass;
    -   Architecture.Tests pass;
    -   Gitleaks passes;
    -   permanent test delta `0`;
    -   package/reference/schema delta `0/0/0`;
    -   database/WAL/SHM/journal residue `0`;
    -   production graph unchanged and acyclic;
    -   provider/network calls `0`;
    -   real credentials `0`;
    -   feature use-case invocations during WP09 validation `0`.

Formatting/analyzer corrections within authorized WP09 files are already
within this authority.

------------------------------------------------------------------------

## 19. Regression Requirements

Confirm no regression to:

### Release 1.1

Historical persistence/retrieval and provider isolation.

### Release 1.2

Snapshot identity/version, exact lookup, immutable persistence, catalog,
schema v2, and existing DI.

### Release 1.3

Pipeline registrations, deterministic pipeline semantics, configuration,
structured evidence, and one-shot Worker behavior.

### Release 1.4 WP02--WP08

Feature semantics, identity, model, contracts, computation, validation,
failure mapping, and exact snapshot integration.

------------------------------------------------------------------------

## 20. Git Protection

Do not:

-   stage;
-   commit;
-   create a branch;
-   push;
-   create a PR;
-   merge;
-   tag;
-   create a release;
-   rewrite history.

Preserve cumulative accepted Release 1.4 work.

------------------------------------------------------------------------

## 21. GitHub Lifecycle

Only issue #161 may receive lifecycle mutation.

After starting gates pass:

1.  move #161 Backlog → In Progress;
2.  implement and validate;
3.  post completion evidence only after all gates pass;
4.  close #161;
5.  set Project #2 Status to Done;
6.  read back #161 as CLOSED / Done;
7.  verify #162 remains OPEN / Backlog and unchanged;
8.  verify milestone #45 remains OPEN.

If the intended #161 lifecycle transition fails to persist, reconcile
only #161 under this authority.

------------------------------------------------------------------------

## 22. Stop Conditions

Stop with:

`RELEASE 1.4 WP09 BLOCKED`

if:

-   accepted WP06--WP08 services cannot be registered without semantic
    redesign;
-   required request construction cannot be expressed deterministically;
-   Application → Infrastructure coupling would be required;
-   package/project-reference/schema changes become necessary;
-   actual feature execution is required to prove composition;
-   provider/network activity becomes necessary;
-   feature persistence is required;
-   Release 1.3 pipeline semantics must change;
-   WP10 behavior must be implemented;
-   the file manifest does not authorize a required path;
-   canonical verification cannot be restored within WP09 scope.

Report the smallest corrective authority required. Do not guess.

------------------------------------------------------------------------

## 23. Required Execution Report

The final report must include at least:

1.  executive summary;
2.  authorities reviewed;
3.  initial Git/repository state;
4.  working-tree classification;
5.  predecessor/lifecycle gates;
6.  initial canonical baseline;
7.  current composition inventory;
8.  WP06--WP08 service reconciliation;
9.  existing snapshot-store registration reconciliation;
10. Application registration design;
11. Infrastructure registration decision;
12. service registrations;
13. lifetime decisions;
14. configuration inventory;
15. request-construction design;
16. configuration keys/inputs;
17. parsing/validation behavior;
18. culture independence;
19. invalid-configuration behavior;
20. service graph resolution proof;
21. no-execution proof;
22. no-snapshot-lookup proof;
23. database side-effect proof;
24. provider/network isolation;
25. schema preservation;
26. files added/modified;
27. layer deltas;
28. package/reference/schema delta;
29. permanent-test delta;
30. temporary probe evidence/removal;
31. WP10 protection;
32. Release 1.3 protection;
33. Release 1.5 protection;
34. security evidence;
35. whitespace/diff evidence;
36. restore/build evidence;
37. permanent test counts;
38. canonical verification;
39. architecture validation;
40. predecessor regressions;
41. WP09 acceptance matrix;
42. mutation accounting;
43. Git/GitHub protection;
44. findings/blockers;
45. final GitHub state;
46. WP10 handoff;
47. final decision.

On success end exactly with:

`RELEASE 1.4 WP09 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — One-Shot Worker Feature Execution — GitHub issue #162`

Do not start WP10.

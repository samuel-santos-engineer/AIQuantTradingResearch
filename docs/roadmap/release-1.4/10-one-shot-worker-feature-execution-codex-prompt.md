# Release 1.4 --- WP10 One-Shot Worker Feature Execution --- Codex Authority

## Mission

Execute **Release 1.4 --- WP10: One-Shot Worker Feature Execution ---
GitHub issue #162**.

WP10 activates the accepted WP04--WP09 feature-generation capability at
the Worker boundary exactly once per process. The Worker must construct
the deterministic WP09 request, resolve the accepted Application use
case, execute it once, present bounded semantic feature evidence, return
deterministic process status, and terminate.

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
8.  WP06 --- Deterministic Feature Computation authority/result and
    identity clarification
9.  WP07 --- Feature Validation & Failure Mapping authority/result
10. WP08 --- Feature Generation Integration authority/result
11. WP09 --- Dependency Registration & Configuration authority/result
12. Current Worker `Program.cs`, Release 1.3 pipeline execution
    boundary, and WP09 `FeatureExecutionConfiguration`
13. Current Application feature contracts/results
14. Existing Release 1.2 snapshot persistence/configuration behavior
15. Current tests and architecture rules
16. Current GitHub lifecycle state for #162 and successor #163

Repository truth and accepted authorities govern. Do not redesign
feature semantics, composition, or predecessor behavior.

------------------------------------------------------------------------

## 2. Starting-State Gates

Before implementation verify and report:

-   branch `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   cumulative Release 1.4 paths are expected and classified;
-   #153--#161 are Closed/Done;
-   #162 is OPEN / Backlog;
-   #163 is OPEN / Backlog and untouched;
-   milestone #45 is OPEN;
-   SQLite schema remains exactly version `2`;
-   permanent test baseline remains `197/197` unless accepted repository
    truth shows otherwise;
-   no Release 1.5 implementation has started;
-   production graph remains:
    -   Domain → none
    -   Application → Domain
    -   Infrastructure → Application
    -   Worker → Application, Infrastructure.

Run `eng/verify.ps1 -Configuration Release` before mutation. Stop on
unrelated baseline failure.

Only after all gates pass may #162 move Backlog → In Progress.

------------------------------------------------------------------------

## 3. WP10 Objective

Implement the minimum bounded Worker execution path:

``` text
WP09 feature configuration
    → FeatureGenerationRequest
    → resolve IFeatureGenerationUseCase
    → Execute exactly once
    → present bounded semantic result/evidence
    → deterministic exit code
    → process terminates
```

No loop, recurrence, retry, scheduler, daemon behavior, or durable
execution history is authorized.

------------------------------------------------------------------------

## 4. Exactly-Once Process Boundary

For one feature-execution process path, there must be exactly one
invocation of `IFeatureGenerationUseCase`.

Do not:

-   loop;
-   retry;
-   poll;
-   schedule;
-   recursively invoke;
-   automatically recompute;
-   execute once for validation and again for output.

A single process execution produces one terminal feature-generation
result.

------------------------------------------------------------------------

## 5. Request Construction

Use the accepted WP09 configuration/request boundary.

Inputs remain exactly the accepted configuration needed to identify:

-   `Feature:SnapshotIdentity`;
-   `Feature:SnapshotVersion`;
-   built-in `simple-return-lag-1-v1`.

Do not introduce configurable formula, lag, rounding, arithmetic,
identity scheme, retry, scheduling, or persistence semantics.

Invalid configuration must terminate before invoking
`IFeatureGenerationUseCase`.

------------------------------------------------------------------------

## 6. Feature Execution

Resolve the accepted Application `IFeatureGenerationUseCase` from the
production DI graph and execute the constructed request exactly once.

Do not bypass WP08 orchestration by:

-   directly reading SQLite in Worker;
-   invoking `IFeatureComputer` from Worker;
-   reconstructing snapshot evidence in Worker;
-   computing identities in Worker;
-   duplicating validation in Worker.

Worker owns triggering and presentation, not feature semantics.

------------------------------------------------------------------------

## 7. Success Evidence

For successful feature generation, present bounded deterministic
semantic evidence sufficient to demonstrate the accepted result.

At minimum, reconcile the actual contracts and safely expose applicable
facts such as:

-   feature definition identity;
-   feature set identity;
-   exact dataset snapshot identity;
-   exact dataset version;
-   feature definition name (`simple-return-lag-1-v1`);
-   feature value count;
-   ordered feature values with their accepted timestamp/offset evidence
    where appropriate.

Do not invent a second feature evidence model merely for Worker output.

Do not include secrets, connection strings, machine identity, temporary
paths, or other operational data in semantic evidence.

------------------------------------------------------------------------

## 8. Empty Success

Both accepted empty-success cases must remain successful:

-   existing snapshot with zero observations;
-   existing snapshot with one observation.

They must:

-   return a successful feature result;
-   retain deterministic snapshot-bound feature-set identity;
-   expose feature count `0`;
-   return process exit code `0`.

Do not reinterpret successful empty evidence as `NotFound` or failure.

------------------------------------------------------------------------

## 9. Failure Presentation

Present only accepted bounded feature failures from WP05--WP08.

Reconcile exact repository vocabulary, including applicable categories
such as:

-   invalid request/configuration boundary;
-   unsupported definition;
-   snapshot not found;
-   dependency unavailable;
-   invalid snapshot evidence;
-   invalid numeric input;
-   integrity conflict.

For a bounded semantic failure:

-   present the accepted failure category safely;
-   present only evidence already established;
-   do not fabricate FeatureSet or downstream identity;
-   terminate with non-zero exit status.

Do not expose sensitive exception internals unnecessarily.

------------------------------------------------------------------------

## 10. Unknown Exceptions

Preserve WP07/WP08 unknown-defect propagation.

Do not add a broad `catch (Exception)` that converts unknown defects
into a normal feature failure.

Unknown defects must remain visibly exceptional to the process/runtime
rather than being semantically normalized.

If the existing Worker top-level mechanics inherently produce a non-zero
process status for an unhandled exception, preserve that behavior.

------------------------------------------------------------------------

## 11. Exit-Code Policy

Use the smallest deterministic policy consistent with existing Worker
conventions:

-   accepted feature success, including non-empty and empty success →
    `0`;
-   accepted configuration failure → non-zero;
-   accepted bounded feature failure → non-zero;
-   unknown defect → unhandled/runtime non-zero behavior.

Prefer `1` for bounded/configuration failures if that matches existing
Release 1.3 Worker conventions.

Do not introduce a complex exit-code taxonomy unless already governed.

------------------------------------------------------------------------

## 12. Release 1.3 Worker Protection

The existing Release 1.3 one-shot research pipeline behavior must remain
intact.

Do not:

-   make feature generation a sixth pipeline stage;
-   silently execute features after every research pipeline run;
-   change pipeline identity/evidence;
-   change pipeline exit semantics;
-   introduce coupling that makes pipeline execution depend on feature
    generation.

If `Program.cs` must select a bounded execution path, preserve existing
behavior and use the minimum explicit mechanism authorized by the
Release 1.4 manifest.

Do not create recurrence between pipeline and feature execution.

------------------------------------------------------------------------

## 13. Provider/Network Isolation

Feature execution starts from an exact persisted snapshot.

The WP10 feature path must make zero provider/network calls.

Do not invoke:

-   Twelve Data;
-   HTTP;
-   market-data acquisition;
-   provider credentials;
-   live refresh.

A dummy/non-production key may be supplied only if the existing
production composition root requires configuration to build, but the
feature path must not execute the provider.

------------------------------------------------------------------------

## 14. Persistence and Schema Protection

Feature output remains in memory.

Do not add:

-   feature tables;
-   feature persistence;
-   feature catalog/cache;
-   execution history;
-   checkpoints;
-   schema migration;
-   schema v3.

SQLite remains schema version `2`.

Existing snapshot reads are allowed through the accepted WP08
Application boundary.

------------------------------------------------------------------------

## 15. Output Safety and Determinism

Worker output should be stable enough for later WP12 process tests
without becoming a new semantic authority.

Prefer explicit labels and invariant formatting for:

-   identities;
-   versions;
-   counts;
-   decimals;
-   timestamps/offsets.

Use culture-independent formatting.

Do not print:

-   API keys;
-   full connection strings;
-   credentials;
-   environment dumps;
-   sensitive filesystem information.

Operational diagnostics must not alter semantic identity.

------------------------------------------------------------------------

## 16. Expected File Surface

Use the Release 1.4 file manifest as hard authority.

Prefer the minimum Worker-only delta necessary to add the bounded
feature trigger/presentation while reusing WP09 configuration.

Likely categories include:

-   Worker `Program.cs` composition/control-flow refinement if
    authorized;
-   a narrowly scoped feature execution/presentation class under the
    manifest-authorized Worker path.

Application, Infrastructure, Domain, packages, project references,
schema, and tests should remain unchanged.

If required paths are not authorized by the manifest, stop.

------------------------------------------------------------------------

## 17. Permanent Test Boundary

Do not add permanent tests in WP10.

WP11 owns permanent feature semantic tests. WP12 owns permanent
composition/Worker validation.

Use removable offline execution/probes as needed.

Temporary execution must:

-   use isolated disposable SQLite state;
-   seed only synthetic persisted snapshot evidence through accepted
    repository mechanisms;
-   use dummy credentials only if composition requires them;
-   make zero provider/network calls;
-   clean database/WAL/SHM/journal/output residue afterward.

Permanent test delta remains `0`.

------------------------------------------------------------------------

## 18. Required WP10 Acceptance Matrix

Prove all applicable cases:

1.  valid feature configuration constructs the accepted request;
2.  feature use case is resolved from production DI;
3.  feature use case executes exactly once per process;
4.  successful non-empty snapshot returns exit `0`;
5.  success presents definition identity;
6.  success presents feature-set identity;
7.  success presents exact snapshot identity/version;
8.  success presents deterministic feature count/evidence;
9.  empty-snapshot success returns exit `0` with count `0`;
10. single-observation success returns exit `0` with count `0`;
11. equivalent executions over identical accepted evidence preserve
    Feature Set Identity;
12. missing/malformed configuration fails before feature execution;
13. snapshot NotFound returns non-zero;
14. dependency unavailable returns non-zero;
15. invalid snapshot evidence returns non-zero where constructible;
16. invalid numeric evidence returns non-zero where constructible;
17. bounded failure does not present fabricated FeatureSet identity;
18. unknown exceptions are not broadly normalized;
19. no loop/retry/scheduling exists;
20. no feature persistence occurs;
21. no provider/network call occurs;
22. SQLite remains schema v2;
23. Release 1.3 pipeline behavior remains unchanged;
24. process terminates after the single bounded execution.

Do not fabricate scenarios that accepted immutable boundaries make
impossible. Explicitly identify unconstructable cases and the invariant
that prevents them.

------------------------------------------------------------------------

## 19. Architecture and Dependency Protection

Preferred deltas:

-   Domain: `0`
-   Application: `0`
-   Infrastructure: `0`
-   Worker: minimum authorized delta
-   packages: `0`
-   project references: `0`
-   schema: `0`
-   permanent tests: `0`

Production graph must remain unchanged and acyclic.

------------------------------------------------------------------------

## 20. Validation Requirements

After implementation:

1.  run targeted Worker build;
2.  execute bounded offline Worker/process proofs as needed;
3.  remove all temporary state/probes;
4.  run `git diff --check`;
5.  run `git diff --cached --check`;
6.  directly inspect whitespace in untracked governed files;
7.  run `eng/verify.ps1 -Configuration Release`;
8.  confirm:
    -   build warnings/errors `0/0`;
    -   all permanent tests pass;
    -   Architecture.Tests pass;
    -   Gitleaks PASS;
    -   permanent-test delta `0`;
    -   package/reference/schema delta `0/0/0`;
    -   database/WAL/SHM/journal residue `0`;
    -   provider/network calls `0`;
    -   real credentials `0`;
    -   production graph unchanged.

Formatting/analyzer corrections within authorized WP10 files are within
this authority.

------------------------------------------------------------------------

## 21. Regression Requirements

Confirm no regression to:

### Release 1.1

Historical observation persistence/retrieval and provider isolation.

### Release 1.2

Immutable snapshot identity/version, exact lookup, empty snapshot
distinction, schema v2.

### Release 1.3

Fixed five-stage pipeline, structured evidence, DI/configuration,
one-shot Worker execution, exit behavior.

### Release 1.4 WP02--WP09

Feature semantics, identities, model, contracts, computation,
validation, exact snapshot integration, and DI/configuration.

------------------------------------------------------------------------

## 22. Release 1.5+ Protection

Do not add:

-   multiple feature definitions;
-   configurable formulas/lags;
-   feature plugins;
-   feature persistence/catalog;
-   DAGs;
-   scheduling;
-   retries;
-   durable histories;
-   notebooks/workspaces;
-   strategies;
-   backtesting;
-   ML/MLOps.

------------------------------------------------------------------------

## 23. Git Protection

Do not:

-   stage;
-   commit;
-   create a branch;
-   push;
-   create a PR;
-   merge;
-   tag;
-   create a GitHub Release;
-   rewrite history.

Preserve cumulative accepted Release 1.4 work.

------------------------------------------------------------------------

## 24. GitHub Lifecycle

Only issue #162 may receive lifecycle mutation.

After starting gates pass:

1.  move #162 Backlog → In Progress;
2.  implement and validate;
3.  post completion evidence only after all gates pass;
4.  close #162;
5.  set Project #2 Status to Done;
6.  read back #162 as CLOSED / Done;
7.  verify #163 remains OPEN / Backlog and unchanged;
8.  verify milestone #45 remains OPEN.

If #162's intended lifecycle state fails to persist, reconcile only #162
under this authority.

------------------------------------------------------------------------

## 25. Stop Conditions

Stop with:

`RELEASE 1.4 WP10 BLOCKED`

if:

-   actual feature execution requires semantic changes to WP04--WP09;
-   the Worker cannot trigger features without modifying the Release 1.3
    pipeline semantics;
-   provider/network acquisition becomes necessary;
-   feature persistence/schema evolution becomes necessary;
-   packages/project references become necessary;
-   permanent tests must be added to implement WP10;
-   the manifest does not authorize a required path;
-   correct behavior requires loops/retries/scheduling;
-   unknown exceptions would need broad normalization;
-   canonical verification cannot be restored within WP10 scope.

Report the smallest corrective authority required. Do not guess.

------------------------------------------------------------------------

## 26. Required Execution Report

The final report must include at least:

1.  executive summary;
2.  authorities reviewed;
3.  initial Git/repository state;
4.  working-tree classification;
5.  predecessor/lifecycle gates;
6.  initial canonical baseline;
7.  existing Worker inventory;
8.  WP09 configuration/composition reconciliation;
9.  Worker execution design;
10. request construction;
11. service resolution;
12. exactly-once invocation proof;
13. success evidence presentation;
14. non-empty success proof;
15. empty-snapshot success proof;
16. single-observation success proof;
17. equivalent-execution identity proof;
18. invalid-configuration behavior;
19. NotFound behavior;
20. dependency-unavailable behavior;
21. invalid-evidence/numeric behavior where constructible;
22. unknown-exception propagation;
23. exit-code policy;
24. first-failure/evidence-established-only behavior;
25. output determinism/safety;
26. provider/network isolation;
27. persistence/schema protection;
28. Release 1.3 pipeline protection;
29. files added/modified;
30. layer deltas;
31. package/reference/schema delta;
32. permanent-test delta;
33. temporary process/probe evidence and cleanup;
34. Release 1.5 protection;
35. whitespace/diff evidence;
36. restore/build evidence;
37. permanent test counts;
38. canonical verification;
39. architecture validation;
40. predecessor regressions;
41. WP10 acceptance matrix;
42. mutation accounting;
43. Git/GitHub protection;
44. findings/blockers;
45. final GitHub state;
46. WP11 handoff;
47. final decision.

On success end exactly with:

`RELEASE 1.4 WP10 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Feature Semantic Tests — GitHub issue #163`

Do not start WP11.

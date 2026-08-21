# Release 1.4 --- WP07 Feature Validation & Failure Mapping --- Codex Authority

## Mission

Execute **Release 1.4 --- WP07: Feature Validation & Failure Mapping ---
GitHub issue #159**.

WP07 hardens the Application-owned feature-generation boundary created
by WP04--WP06. It must make validation order, bounded failure
classification, first-failure behavior, evidence-established-only
behavior, and unknown-defect propagation explicit and deterministic
without expanding Release 1.4 scope.

Recommended model: **GPT-5.6 Sol**.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Before any mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
6.  WP04 authority/result --- Feature Domain/Application Model
7.  WP05 authority/result --- Feature Generation Contracts
8.  WP06 original authority/result --- Deterministic Feature Computation
9.  `06-deterministic-feature-computation-identity-clarification-codex-prompt.md`
10. Current Release 1.4 Application feature implementation
11. Current Release 1.2 dataset snapshot/catalog contracts and failure
    vocabulary
12. Current Release 1.3 pipeline contracts only as predecessor behavior
    that must remain unchanged
13. Current permanent tests and architecture tests
14. Current GitHub state for milestone #45, issue #159, and successor
    issue #160

Do not infer semantics from filenames alone. Repository truth and
accepted semantic authorities govern.

------------------------------------------------------------------------

## 2. Starting-State Gates

Before implementation, verify and report:

-   branch is `main`;
-   local `HEAD == origin/main`;
-   ahead/behind is `0/0`;
-   staged paths are `0`;
-   cumulative Release 1.4 work is classified and contains no unexpected
    residue;
-   WP01--WP06 issues #153--#158 are Closed/Done;
-   #159 is OPEN / Backlog;
-   #160 is OPEN / Backlog and untouched;
-   milestone #45 is OPEN;
-   no Release 1.5 implementation has started;
-   SQLite schema is still exactly version `2`;
-   production dependency graph remains:
    -   Domain → none
    -   Application → Domain
    -   Infrastructure → Application
    -   Worker → Application, Infrastructure.

Run the canonical Release verification before mutation. If the starting
baseline fails for a reason unrelated to already-authorized cumulative
Release 1.4 work, stop.

Only after the starting gates pass may #159 move Backlog → In Progress.

------------------------------------------------------------------------

## 3. WP07 Objective

Establish the minimum deterministic validation and failure-mapping
boundary required for Release 1.4 feature generation.

WP07 must preserve:

-   exactly one built-in feature: `simple-return-lag-1-v1`;
-   decimal-only lag-1 computation;
-   `aiq-feature-identity-v1`;
-   WP03 canonical identity encoding;
-   WP04 immutable model;
-   WP05 contracts;
-   WP06 deterministic computation;
-   snapshot-bound feature evidence;
-   empty and single-observation successful empty results;
-   provider/storage independence;
-   SQLite schema v2;
-   Release 1.3's fixed five-stage pipeline unchanged.

WP07 is not a redesign work package.

------------------------------------------------------------------------

## 4. Validation Ownership

Validation must remain Application-owned.

Introduce or refine only the smallest Application feature validation
surface justified by repository conventions and the Release 1.4 file
manifest.

Validation must be:

-   deterministic;
-   synchronous;
-   culture-independent;
-   timezone-independent except for explicitly preserved semantic
    offsets;
-   free from I/O;
-   free from provider/network access;
-   free from persistence;
-   free from machine/process/time/random state.

Do not add validation to Domain, Infrastructure, or Worker merely to
satisfy WP07.

------------------------------------------------------------------------

## 5. Required Validation Order

Define and enforce a deterministic validation order so the same invalid
semantic input always produces the same bounded result.

The boundary must distinguish, in the order justified by accepted
contracts:

1.  invalid request shape or incoherent request evidence;
2.  unsupported feature definition;
3.  snapshot lookup outcome where lookup belongs to the accepted
    use-case boundary;
4.  unavailable dependency;
5.  invalid snapshot/evidence;
6.  invalid numeric evidence;
7.  integrity contradiction;
8.  successful empty or non-empty result.

Do not manufacture a failure precedence that contradicts the existing
WP05 contracts or Release 1.2 lookup semantics.

If current repository truth assigns snapshot lookup to WP08 rather than
WP07, do **not** implement lookup in WP07. In that case freeze and
implement only the validation/failure mapping that can occur on the
existing WP04--WP06 boundary, and preserve the remaining categories for
WP08 orchestration. Report that boundary explicitly.

------------------------------------------------------------------------

## 6. Invalid Request

Invalid request behavior must be bounded and deterministic.

Reject malformed or incoherent request evidence before invoking
downstream computation when the existing seam permits this.

Examples include only conditions supported by accepted contracts, such
as:

-   missing required semantic input;
-   definition/request mismatch;
-   incoherent snapshot identity/version binding;
-   malformed typed identity that is not already constructor-rejected;
-   request content that contradicts immutable model invariants.

Do not duplicate constructor validation unnecessarily.

Do not convert programmer defects into ordinary invalid-request results.

------------------------------------------------------------------------

## 7. Unsupported Definition

Release 1.4 supports exactly:

`simple-return-lag-1-v1`

Any otherwise valid request for an unsupported feature definition must
remain distinguishable as the WP05 bounded unsupported-definition
failure.

Do not silently fall back to the built-in definition.

Do not introduce aliases, configurable formulas, multiple lags, plugins,
or generalized dispatch infrastructure.

------------------------------------------------------------------------

## 8. Snapshot NotFound

Preserve the semantic distinction between:

-   exact snapshot lookup returning `NotFound`; and
-   an existing snapshot whose accepted observations produce an empty
    feature set.

`NotFound` is a failure.

An existing empty snapshot is a successful empty result.

An existing single-observation snapshot is also a successful empty
result.

Do not collapse these cases.

If lookup implementation is owned by WP08, do not implement it here;
preserve the failure contract and validate any already-established
snapshot evidence supplied to WP07/WP06.

------------------------------------------------------------------------

## 9. DependencyUnavailable

Dependency unavailability must remain distinct from invalid semantic
evidence.

Only failures already classified by accepted predecessor boundaries as
unavailable dependencies may map to `DependencyUnavailable`.

Do not catch arbitrary exceptions and reclassify them as dependency
failures.

Do not add retries, fallback, recovery, or circuit-breaker behavior.

------------------------------------------------------------------------

## 10. Invalid Snapshot Evidence

Accepted feature computation must operate only on coherent immutable
snapshot evidence.

Validate only semantic conditions required by WP02/WP03 and existing
dataset contracts, including where applicable:

-   exact snapshot identity/version coherence;
-   deterministic accepted ordering;
-   feature provenance binding to the supplied snapshot;
-   evidence required for identity computation;
-   cardinality/evidence consistency;
-   no fabricated downstream evidence.

Do not reinterpret Release 1.2 dataset semantics.

------------------------------------------------------------------------

## 11. Invalid Numeric Evidence

Preserve WP02 numeric rules exactly.

For `simple-return-lag-1-v1`:

`r[i] = (p[i] / p[i-1]) - 1`

using `decimal` only.

A zero predecessor is invalid numeric evidence.

The entire generation attempt must fail atomically for invalid numeric
evidence.

Do not:

-   produce NaN;
-   produce infinity;
-   use a sentinel;
-   skip the invalid pair;
-   return a partial feature set;
-   convert through `double` or `float`;
-   round for convenience.

If other impossible numeric states are already prevented by predecessor
contracts, do not invent new failure cases.

------------------------------------------------------------------------

## 12. Integrity Contradiction

Preserve the accepted integrity semantics.

Examples include contradictions such as equal asserted semantic identity
with different canonical semantic content where such evidence can be
observed at this boundary.

Integrity contradictions must remain distinct from:

-   invalid request;
-   unsupported definition;
-   NotFound;
-   unavailable dependency;
-   invalid snapshot evidence;
-   invalid numeric evidence.

Do not repair, overwrite, compensate, or silently normalize
contradictory evidence.

------------------------------------------------------------------------

## 13. Unknown Defects

Unknown or unrelated exceptions must propagate.

WP07 must not introduce:

-   `catch (Exception)` normalization;
-   broad fallback mapping;
-   "unknown failure" result categories merely to prevent exceptions;
-   logging-and-success behavior.

A bounded feature-generation failure is only one explicitly justified by
accepted Release 1.4 semantics.

------------------------------------------------------------------------

## 14. First-Failure and Fail-Stop Behavior

The first established failure terminates the feature-generation attempt.

After failure:

-   do not compute later semantic evidence;
-   do not fabricate Feature Definition Identity if the definition was
    not established;
-   do not fabricate Feature Set Identity;
-   do not return partial feature values;
-   do not continue to later dependencies;
-   do not persist anything;
-   do not compensate or retry.

Evidence must represent only what was semantically established before
the first failure.

------------------------------------------------------------------------

## 15. Identity Protection

WP07 must preserve WP03/WP06 identity behavior exactly.

Do not change:

-   `aiq-feature-identity-v1`;
-   canonical SHA-256 encoding;
-   Feature Definition Identity semantics;
-   Feature Set Identity semantics;
-   decimal canonicalization;
-   timestamp/offset canonicalization;
-   empty-set identity behavior;
-   snapshot identity/version participation;
-   ordered evidence participation.

Validation may reject contradictory evidence, but must not create an
alternative identity algorithm.

Equivalent recomputation must remain identity-stable.

------------------------------------------------------------------------

## 16. Successful Results

Preserve all successful forms:

### Non-empty success

For valid `N >= 2` input, produce exactly `N-1` ordered values.

### Empty snapshot success

An accepted empty snapshot produces a successful empty FeatureSet.

### Single-observation success

An accepted one-observation snapshot produces a successful empty
FeatureSet.

Empty success must still have deterministic snapshot-bound Feature Set
Identity and valid provenance.

Do not introduce a special failure for insufficient history when the
accepted semantics define empty success.

------------------------------------------------------------------------

## 17. Atomicity and Immutability

Feature generation is an in-memory deterministic transformation.

WP07 must preserve:

-   immutable input evidence;
-   immutable output evidence;
-   no partial-success object after failure;
-   no mutation of dataset snapshots;
-   no persistence side effects;
-   no feature cache;
-   no run history.

------------------------------------------------------------------------

## 18. Provider and Storage Independence

No feature validation rule may depend on:

-   Twelve Data;
-   HTTP;
-   provider-specific symbols or payloads;
-   SQLite implementation details;
-   SQL;
-   connection strings;
-   filesystem paths;
-   credentials.

Feature semantics depend on accepted snapshot evidence, not its
acquisition or storage mechanism.

------------------------------------------------------------------------

## 19. Release 1.3 Protection

Feature generation remains separate from the Release 1.3 Research
Pipeline.

Do not:

-   add a sixth pipeline stage;
-   modify pipeline identity;
-   modify pipeline evidence;
-   modify pipeline failure taxonomy;
-   alter one-shot pipeline Worker behavior.

------------------------------------------------------------------------

## 20. Persistence and Schema Protection

SQLite remains exactly schema version `2`.

WP07 must not introduce:

-   feature tables;
-   feature catalog;
-   feature cache;
-   feature history;
-   feature-run history;
-   scheduler/checkpoint tables;
-   schema migration;
-   persistence disposition.

Feature persistence remains Release 1.5+.

------------------------------------------------------------------------

## 21. Architecture and Package Protection

Expected WP07 production delta is Application-only.

Preferred deltas:

-   Domain: `0`
-   Application: minimum necessary validation/failure-mapping changes
-   Infrastructure: `0`
-   Worker: `0`
-   package delta: `0`
-   project-reference delta: `0`
-   schema delta: `0`

The production dependency graph must remain unchanged and acyclic.

------------------------------------------------------------------------

## 22. Permanent Test Boundary

WP07 must **not** add permanent tests.

Permanent semantic feature tests are assigned to WP11.

Use existing tests plus, only if necessary, a temporary deterministic
offline probe to prove the WP07 acceptance matrix.

Any temporary probe must:

-   use no provider/network;
-   use no real credential;
-   use no repository database;
-   leave no generated residue;
-   be removed before final validation.

Permanent test count must remain the accepted pre-WP07 baseline.

------------------------------------------------------------------------

## 23. Required WP07 Acceptance Matrix

Prove all applicable cases without broadening scope:

1.  valid non-empty computation remains successful;
2.  valid empty snapshot remains successful;
3.  valid single-observation snapshot remains successful empty;
4.  equivalent recomputation remains identity-equivalent;
5.  invalid request fails before downstream computation where the seam
    permits;
6.  unsupported definition remains distinct;
7.  snapshot `NotFound` remains distinct where the current boundary can
    exercise it;
8.  dependency unavailable remains distinct where the current boundary
    can exercise it;
9.  invalid snapshot evidence remains distinct;
10. zero predecessor maps to invalid numeric evidence;
11. invalid numeric evidence produces no partial FeatureSet;
12. integrity contradiction remains distinct where observable;
13. first failure stops later work;
14. no downstream identity is fabricated after failure;
15. unknown exceptions propagate;
16. culture does not change classification;
17. machine timezone does not change classification;
18. no provider/network access occurs;
19. no persistence/schema mutation occurs;
20. Release 1.3 behavior remains unchanged.

If a row belongs operationally to WP08 because lookup/integration is not
yet implemented, mark it **deferred to WP08 by accepted boundary**, not
falsely passed.

------------------------------------------------------------------------

## 24. Implementation Discipline

Prefer the smallest coherent change.

Do not create abstractions "for future extensibility."

Do not add:

-   generalized validation framework;
-   validation package;
-   rule engine;
-   plugin model;
-   feature registry;
-   dynamic dispatch system;
-   retry policy;
-   observability backend.

Follow existing C#/.NET repository conventions, nullable rules, analyzer
rules, formatting, and naming.

------------------------------------------------------------------------

## 25. Required Validation

After implementation:

1.  run targeted build/validation appropriate to changed Application
    code;
2.  run any authorized temporary offline probe;
3.  remove temporary probes;
4.  run `git diff --check`;
5.  run `git diff --cached --check`;
6.  directly inspect trailing whitespace in untracked Release 1.4 files
    where Git diff cannot see it;
7.  run canonical: `eng/verify.ps1 -Configuration Release`
8.  confirm:
    -   build warnings `0`;
    -   build errors `0`;
    -   all permanent tests pass;
    -   Architecture.Tests pass;
    -   Gitleaks passes;
    -   permanent test delta `0`;
    -   package/reference/schema delta `0/0/0`;
    -   database/WAL/SHM/journal residue `0`;
    -   production graph unchanged;
    -   no provider/network activity;
    -   no real credentials.

If canonical verification fails because of WP07 code, correct only
within WP07 authority and rerun validation. A formatting/analyzer
failure alone is not a reason to request new authority.

------------------------------------------------------------------------

## 26. Git and Repository Protection

WP07 is a cumulative working-tree work package.

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

Preserve all accepted cumulative Release 1.4 work.

Do not modify unrelated files.

------------------------------------------------------------------------

## 27. GitHub Lifecycle

Only issue #159 may receive lifecycle mutation.

After starting gates pass:

1.  move #159 Backlog → In Progress;
2.  implement and validate WP07;
3.  post bounded completion evidence only after all WP07 gates pass;
4.  close #159;
5.  set Project #2 Status to Done;
6.  read back and verify #159 CLOSED / Done;
7.  verify #160 remains OPEN / Backlog and unchanged;
8.  verify milestone #45 remains OPEN.

If a lifecycle write does not persist, reconcile only that exact
lifecycle state under this authority; do not request semantic authority
for a pure GitHub state mismatch.

------------------------------------------------------------------------

## 28. Stop Conditions

Stop and report:

`RELEASE 1.4 WP07 BLOCKED`

if:

-   accepted WP02/WP03 semantics materially conflict;
-   WP04/WP05/WP06 contracts cannot support the required validation
    without redesign;
-   required behavior demands snapshot orchestration owned by WP08;
-   correct behavior requires persistence or schema evolution;
-   correct behavior requires package/project-reference changes;
-   correct behavior requires modifying Release 1.3 pipeline semantics;
-   correct behavior requires starting WP08 or WP11;
-   unknown failures cannot remain propagating;
-   canonical verification cannot be restored within WP07 scope.

Do not guess.

For a blocker, identify the smallest corrective authority required.

------------------------------------------------------------------------

## 29. Required Execution Report

The final report must include at least:

1.  executive summary;
2.  authorities reviewed;
3.  initial Git/repository state;
4.  working-tree classification;
5.  predecessor and lifecycle gates;
6.  initial canonical baseline;
7.  WP02 semantic reconciliation;
8.  WP03 identity reconciliation;
9.  WP04 model reconciliation;
10. WP05 contract reconciliation;
11. WP06 computation/identity reconciliation;
12. validation inventory before change;
13. failure-taxonomy inventory before change;
14. validation-order decision;
15. invalid-request behavior;
16. unsupported-definition behavior;
17. NotFound boundary;
18. dependency-unavailable boundary;
19. invalid-snapshot-evidence behavior;
20. invalid-numeric behavior;
21. integrity-conflict behavior;
22. unknown-exception propagation;
23. first-failure/fail-stop evidence;
24. evidence-established-only behavior;
25. empty/single-observation success;
26. identity preservation;
27. files added/modified;
28. layer deltas;
29. package/reference/schema delta;
30. permanent-test delta;
31. temporary-probe evidence and removal, if used;
32. WP08 protection;
33. WP11 protection;
34. Release 1.5 protection;
35. security/offline evidence;
36. whitespace/diff evidence;
37. restore/build evidence;
38. permanent test counts;
39. canonical verification;
40. architecture validation;
41. Release 1.1--1.3 regression;
42. WP06 regression;
43. WP07 acceptance matrix;
44. mutation accounting;
45. Git/GitHub protection;
46. findings/blockers;
47. final GitHub state;
48. WP08 handoff;
49. final decision.

On success end exactly with:

`RELEASE 1.4 WP07 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Feature Generation Integration — GitHub issue #160`

Do not start WP08.

# Release 1.4 --- WP08 Feature Generation Integration --- Codex Authority

## Mission

Execute **Release 1.4 --- WP08: Feature Generation Integration ---
GitHub issue #160**.

WP08 connects exact immutable dataset snapshot lookup to the accepted
WP04--WP07 feature-generation boundary. Implement the minimum
Application-owned orchestration required to resolve the exact Release
1.2 snapshot, distinguish `NotFound` and dependency failures, validate
established evidence, invoke deterministic feature computation exactly
once, and return the accepted bounded result.

Recommended model: **GPT-5.6 Terra**.

## Mandatory authorities

Before mutation, read completely and reconcile the Release 1.4
definition, execution plan, file manifest,
`FEATURE_ENGINEERING_SEMANTICS.md`,
`FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`, WP04--WP07
authorities/results, the WP06 identity clarification, current
`Application/Features/**`, Release 1.2 snapshot identity/version/store
and failure contracts, current tests/architecture rules, and GitHub
state for #160/#161.

Repository truth and accepted authorities govern. Do not invent missing
APIs or semantics.

## Starting-state gates

Verify before implementation:

-   `main`; `HEAD == origin/main`; ahead/behind `0/0`; staged paths `0`.
-   Cumulative Release 1.4 paths are expected and classified.
-   #153--#159 are Closed/Done.
-   #160 is OPEN / Backlog.
-   #161 is OPEN / Backlog and untouched.
-   Milestone #45 is OPEN.
-   SQLite schema is exactly v2.
-   No Release 1.5 implementation exists.
-   Production graph remains Domain → none; Application → Domain;
    Infrastructure → Application; Worker → Application, Infrastructure.

Run canonical `eng/verify.ps1 -Configuration Release` before mutation.
Only after the gates pass may #160 move Backlog → In Progress.

## Integration boundary

Implement only:

`FeatureGenerationRequest → exact immutable snapshot lookup → lookup/failure classification → snapshot evidence validation → deterministic feature computation → immutable FeatureSet/result`

The use case is explicit, synchronous, deterministic, bounded, and
one-shot.

Application owns orchestration and must reuse the existing Release 1.2
Application snapshot abstraction. Do not introduce Application →
Infrastructure coupling.

## Exact snapshot lookup

Use the request's exact `DatasetSnapshotIdentity` and `DatasetVersion`.

Do not select latest, fall back to another version, rematerialize, call
a provider, or substitute an equivalent but differently identified
snapshot.

### NotFound

If the exact snapshot/version does not exist:

-   return the accepted snapshot-not-found failure;
-   do not invoke feature computation;
-   do not create feature-set identity;
-   do not return an empty FeatureSet.

### Existing empty snapshot

An existing zero-observation snapshot is valid and produces successful
empty feature evidence with deterministic snapshot-bound identity.

### Existing single-observation snapshot

An existing one-observation snapshot is valid and produces successful
empty feature evidence.

Never collapse these three states.

## DependencyUnavailable

Only an existing Release 1.2 storage/lookup failure already classified
as unavailable may map to the WP05 `DependencyUnavailable` failure.

Do not map NotFound, invalid evidence, integrity contradiction, or
unknown exceptions to unavailable. No retry, fallback, recovery, or
compensation is authorized.

## Validation and computation

After successful lookup, use the accepted WP07 validation boundary.
Invalid/contradictory snapshot evidence must fail before computation and
must not fabricate downstream identity.

For valid evidence invoke the WP06 deterministic computer exactly once.
Preserve:

-   only `simple-return-lag-1-v1`;
-   decimal-only arithmetic;
-   accepted snapshot order;
-   current-observation timestamp/offset ownership;
-   empty/single-observation success;
-   `aiq-feature-identity-v1`;
-   WP03 canonical identities;
-   immutable evidence.

Do not duplicate the formula in orchestration if WP06 already owns it.

## Failure precedence

Preserve deterministic first-failure semantics, conceptually:

1.  request validation;
2.  supported-definition validation;
3.  exact snapshot lookup;
4.  NotFound/dependency classification;
5.  returned snapshot validation;
6.  deterministic computation;
7.  integrity validation where observable;
8.  success.

Unknown defects propagate. Do not add catch-all normalization or an
`Unknown` bounded category.

At every failure expose only evidence already established. No partial
FeatureSet, fabricated identity, repair, overwrite, persistence, retry,
or compensation.

## Scope protections

WP08 must not:

-   add feature persistence/catalog/cache/history;
-   change SQLite schema v2;
-   add DI registrations or configuration (WP09);
-   modify Worker behavior (WP10);
-   add permanent tests (WP11/WP12 own later coverage);
-   invoke Twelve Data, HTTP, providers, or acquisition;
-   modify the Release 1.3 five-stage pipeline or make feature
    generation a sixth stage;
-   add packages or project references;
-   start Release 1.5 work.

Preferred production delta: Application only. Infrastructure, Domain,
Worker, package, reference, and schema deltas should be zero.

## Permanent-test boundary

Permanent test delta must be zero.

A removable deterministic offline probe is allowed if needed. Prefer
hand-written in-memory doubles. It must use no provider/network, real
credentials, repository database, or permanent dependency changes, and
must be removed before final validation.

## Required acceptance matrix

Prove all applicable cases:

1.  exact requested snapshot/version is looked up;
2.  valid non-empty snapshot invokes computation exactly once;
3.  success preserves requested snapshot identity/version;
4.  existing empty snapshot succeeds empty;
5.  existing single-observation snapshot succeeds empty;
6.  NotFound is distinct;
7.  NotFound does not invoke computation;
8.  unavailable lookup maps to `DependencyUnavailable`;
9.  unavailable lookup does not invoke computation;
10. invalid returned snapshot evidence maps to accepted validation
    failure;
11. invalid snapshot evidence stops computation;
12. invalid numeric evidence remains distinct;
13. invalid numeric failure returns no partial FeatureSet;
14. equivalent recomputation remains identity-equivalent;
15. unknown lookup exceptions propagate;
16. unknown computation exceptions propagate;
17. first failure prevents later work;
18. no downstream identity is fabricated after failure;
19. provider/network calls are zero;
20. feature persistence is zero;
21. schema remains v2;
22. Release 1.3 pipeline remains unchanged.

Do not claim a case passed without evidence.

## Validation

After implementation:

-   run useful targeted Application validation/probes;
-   remove temporary probes;
-   run `git diff --check`;
-   run `git diff --cached --check`;
-   directly inspect whitespace in untracked governed files;
-   run `eng/verify.ps1 -Configuration Release`.

Require:

-   build warnings/errors `0/0`;
-   all permanent tests pass;
-   Architecture.Tests pass;
-   Gitleaks PASS;
-   permanent-test delta `0`;
-   package/reference/schema delta `0/0/0`;
-   SQLite/WAL/SHM/journal residue `0`;
-   production graph unchanged and acyclic;
-   provider/network calls `0`;
-   real credentials `0`.

Formatting/analyzer corrections to authorized WP08 files are within WP08
authority.

Confirm Release 1.1 historical-observation, Release 1.2
dataset/snapshot/catalog, Release 1.3 pipeline, and Release 1.4
WP02--WP07 regressions remain intact.

## Git protection

Do not stage, commit, branch, push, create a PR, merge, tag, release, or
rewrite history. Preserve cumulative accepted Release 1.4 work.

## GitHub lifecycle

Only #160 may change.

After all gates pass:

1.  #160 Backlog → In Progress;
2.  implement/validate;
3.  post bounded completion evidence;
4.  close #160;
5.  set Project #2 Status = Done;
6.  read back #160 CLOSED / Done;
7.  verify #161 remains OPEN / Backlog unchanged;
8.  verify milestone #45 remains OPEN.

If #160's intended lifecycle write fails to persist, reconcile only that
exact #160 state under this authority.

## Stop conditions

Stop with `RELEASE 1.4 WP08 BLOCKED` if exact lookup cannot be
implemented through the accepted Application seam; Application →
Infrastructure coupling becomes necessary; WP04--WP07 require semantic
redesign; NotFound/unavailable/invalid-evidence distinctions cannot be
preserved; persistence/schema, DI, Worker, provider acquisition,
pipeline changes, packages/references, or later-WP work become
necessary; or canonical verification cannot be restored within scope.

Report the smallest corrective authority required. Do not guess.

## Required execution report

Report: authorities; starting Git/lifecycle state; baseline
verification; Release 1.2 lookup inventory; WP05--WP07 reconciliation;
integration design; exact lookup; NotFound/empty/single-observation
behavior; unavailable mapping; invalid snapshot/numeric behavior;
unknown exception propagation; fail-stop/evidence-established-only
proof; identity/provenance preservation; files/layer deltas;
package/reference/schema and permanent-test deltas; probe cleanup;
provider isolation; persistence/DI/Worker/pipeline protections;
whitespace/build/tests/canonical/architecture evidence; predecessor
regressions; acceptance matrix; mutation accounting; final GitHub state;
findings; and WP09 handoff.

On success end exactly:

`RELEASE 1.4 WP08 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP09 — Dependency Registration & Configuration — GitHub issue #161`

Do not start WP09.

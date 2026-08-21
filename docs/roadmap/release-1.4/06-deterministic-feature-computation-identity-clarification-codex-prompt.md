# Release 1.4 --- WP06 Deterministic Feature Computation --- Identity Clarification Authority

## Purpose

This is a **narrow corrective authority** for the previously blocked:

**Release 1.4 --- WP06: Deterministic Feature Computation --- GitHub
issue #158**

It supplements, and does not replace, the existing authoritative WP06
prompt:

`06-deterministic-feature-computation-codex-prompt.md`

The previous WP06 execution correctly stopped before mutation because
ownership of canonical `aiq-feature-identity-v1` computation was
materially ambiguous.

This clarification resolves only that ambiguity.

All other Release 1.4 definition, execution-plan, file-manifest,
WP02/WP03 semantic, WP04 model, WP05 contract, lifecycle, validation,
scope, and protection requirements remain fully authoritative.

Recommended model: **GPT-5.6 Terra**.

------------------------------------------------------------------------

## 1. Corrective Decision

**WP06 explicitly owns the minimum Application-level canonical
identity-computation implementation required to construct a valid
immutable `FeatureSet`.**

WP06 is authorized to implement:

1.  canonical `FeatureDefinitionIdentity` computation; and
2.  canonical `FeatureSetIdentity` computation.

These computations must implement the already-frozen WP03 semantics
exactly.

This authority does **not** permit WP06 to redesign, reinterpret,
broaden, or version feature identity semantics.

------------------------------------------------------------------------

## 2. Semantic Authority

The authoritative semantic specification remains:

`docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`

The implementation must conform to that document.

In particular preserve:

-   scheme: `aiq-feature-identity-v1`;
-   SHA-256;
-   32 digest bytes;
-   exactly 64 lowercase hexadecimal fingerprint characters;
-   deterministic BOM-free UTF-8 canonical representation;
-   ordinal semantics;
-   explicit identity domains;
-   fixed field order;
-   invariant field counts;
-   byte-length-delimited components;
-   acyclic derivation;
-   operational metadata exclusion.

If the current WP03 document and this clarification appear to conflict,
WP03 semantics win and execution must stop rather than invent a
reconciliation.

------------------------------------------------------------------------

## 3. Feature Definition Identity Ownership

WP06 must compute the canonical identity of the sole Release 1.4
built-in definition:

`simple-return-lag-1-v1`

The Feature Definition Identity represents only the frozen semantic
definition.

It must not include:

-   dataset identity;
-   snapshot identity;
-   dataset version;
-   feature values;
-   invocation data;
-   persistence disposition;
-   timestamps of execution;
-   correlation identifiers;
-   paths;
-   machine/process information;
-   configuration unrelated to semantic definition.

Equivalent representations of the same frozen built-in definition must
produce the same identity.

No second feature definition is authorized.

------------------------------------------------------------------------

## 4. Feature Set Identity Ownership

WP06 must compute the canonical Feature Set Identity required by the
accepted WP04 `FeatureSet`.

The Feature Set Identity must bind exactly the semantic evidence frozen
by WP03, including:

-   Feature Definition Identity;
-   exact `DatasetSnapshotIdentity`;
-   exact `DatasetVersion`;
-   feature-set cardinality;
-   ordered feature-value evidence;
-   each feature value's preserved timestamp/offset evidence;
-   each feature value's exact decimal semantic evidence.

The implementation must preserve ordered evidence. Do not sort,
deduplicate, or enumerate semantic values through an unordered
collection.

------------------------------------------------------------------------

## 5. Decimal Canonicalization

Feature Set Identity computation must follow WP03 decimal semantics.

Canonical decimal representation must be:

-   deterministic;
-   culture-independent;
-   based on decimal semantic value;
-   free from binary floating-point conversion;
-   normalized so redundant trailing-zero representations do not create
    different semantic identities.

Do not use `double` or `float`.

Do not use locale-dependent formatting.

Do not introduce presentation rounding.

If the WP03 authority specifies sign/coefficient/scale encoding details,
implement those exact details.

------------------------------------------------------------------------

## 6. Timestamp and Offset Canonicalization

Feature Set Identity must preserve the WP03 timestamp identity
semantics.

Canonical timestamp evidence must include the semantic instant and
preserved original offset as frozen by WP03, including the specified
UTC-tick and offset-minute representation where applicable.

Do not replace the original offset with the machine-local offset or an
execution-time offset.

Do not include wall-clock execution time.

------------------------------------------------------------------------

## 7. Empty Feature Set Identity

WP06 must support deterministic identities for valid empty FeatureSets.

An empty FeatureSet is **not** represented by a global empty sentinel.

Its identity remains bound to:

-   the exact Feature Definition Identity;
-   exact source `DatasetSnapshotIdentity`;
-   exact `DatasetVersion`;
-   cardinality `0`;
-   empty ordered feature evidence.

Therefore two empty results derived from semantically distinct snapshots
remain identity-distinct where required by WP03.

Equivalent recomputation over the same accepted semantic evidence must
reproduce the same Feature Set Identity.

------------------------------------------------------------------------

## 8. Identity Integrity

Identity computation must remain a pure deterministic function of
semantic evidence.

Do not introduce:

-   identity persistence;
-   identity lookup;
-   mutable identity state;
-   caches whose state changes semantic output;
-   random salt;
-   timestamps;
-   environment data;
-   provider data;
-   filesystem data.

Equal canonical content must produce equal identities.

Contradictory canonical content under an asserted equal identity remains
an integrity contradiction under existing semantics; WP06 does not
redefine the failure taxonomy for that case.

------------------------------------------------------------------------

## 9. Relationship to WP06 Computation

The original WP06 computation authority remains unchanged.

WP06 may now compose the two responsibilities necessary to return the
already-accepted contract-valid `FeatureSet`:

``` text
accepted ordered snapshot evidence
    → deterministic simple-return-lag-1-v1 computation
    → canonical Feature Definition Identity
    → canonical Feature Set Identity
    → immutable FeatureSet
```

This does not authorize snapshot lookup or feature-generation
orchestration.

The computation still begins from already accepted snapshot evidence
supplied through the Application computation seam.

------------------------------------------------------------------------

## 10. Contract Preservation

Do not change the WP05 seam merely to avoid constructing a `FeatureSet`.

Specifically, this clarification rejects the alternative of introducing
a non-`FeatureSet` intermediate solely because identity implementation
had not previously been assigned.

Preserve the accepted WP04/WP05 model and contracts unless an
independent material defect is discovered.

If such a defect exists, stop and report it.

------------------------------------------------------------------------

## 11. Implementation Location

The canonical identity computer is Application-owned.

Use only paths authorized by the Release 1.4 file manifest under the
existing Application feature area, expected under:

`src/AIQuantTradingResearch.Application/Features/`

A small dedicated identity-computation type/file is acceptable if
consistent with repository conventions and manifest authority.

Do not place feature identity computation in:

-   Domain;
-   Infrastructure;
-   Worker;
-   tests;
-   persistence code.

Do not add project references or packages.

------------------------------------------------------------------------

## 12. WP07 Ownership Preserved

WP07 --- Feature Validation & Failure Mapping --- remains separately
governed.

This clarification does not authorize WP06 to implement WP07 broadly.

WP06 may perform only validation intrinsically necessary to safely
compute canonical identities and produce a valid FeatureSet under
existing WP03--WP05 invariants.

WP07 retains ownership of generalized validation/failure-mapping
hardening.

Do not start issue #159.

------------------------------------------------------------------------

## 13. Persistence and Schema Protection

No feature identity is persisted in WP06.

Do not add or modify:

-   SQLite tables;
-   migrations;
-   schema version;
-   feature catalog;
-   feature cache;
-   feature run history;
-   identity registry.

SQLite remains exactly schema version `2`.

Schema v3 remains deferred.

------------------------------------------------------------------------

## 14. Pipeline Protection

Release 1.3 remains unchanged.

Feature computation remains a separate one-shot feature use case and is
not a sixth pipeline stage.

Do not modify:

-   Release 1.3 pipeline topology;
-   `aiq-pipeline-identity-v1`;
-   dataset identity semantics;
-   snapshot/catalog behavior;
-   pipeline evidence.

------------------------------------------------------------------------

## 15. Testing Boundary

This clarification does not authorize permanent test additions.

Permanent feature semantic tests remain assigned to the later Release
1.4 testing work package.

WP06 may use removable deterministic offline probes if the original WP06
authority permits them.

Useful probe evidence may include:

-   same definition → same Feature Definition Identity;
-   equivalent feature evidence → same Feature Set Identity;
-   same numeric values from different snapshot identities → distinct
    Feature Set identities;
-   empty equivalent recomputation → same identity;
-   empty results from distinct snapshots → appropriately distinct
    identities;
-   culture changes do not affect identities;
-   equivalent decimal values with redundant trailing zeros do not alter
    semantic identity;
-   offset evidence participates exactly as WP03 specifies.

All temporary probes must be removed before final validation.

Permanent test count must remain unchanged.

------------------------------------------------------------------------

## 16. Restart Procedure

Because the prior WP06 attempt performed no repository or lifecycle
mutation, restart WP06 from its original starting-state gates.

Before mutation verify again:

-   branch `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   #157 Closed/Done;
-   #158 Open/Backlog;
-   #159 Open/Backlog and untouched;
-   milestone #45 Open;
-   no unexpected residue;
-   canonical baseline passes.

Then read together:

1.  original WP06 full authority;
2.  this clarification;
3.  Release 1.4 definition;
4.  execution plan;
5.  file manifest;
6.  WP02 feature semantics;
7.  WP03 identity/provenance/evidence semantics;
8.  accepted WP04 model;
9.  accepted WP05 contracts.

Only then may #158 move to In Progress and implementation begin.

------------------------------------------------------------------------

## 17. Required Validation Additions

In addition to every validation gate in the original WP06 authority,
explicitly demonstrate that:

-   Feature Definition Identity computation conforms to WP03;
-   Feature Set Identity computation conforms to WP03;
-   SHA-256 fingerprints are 64 lowercase hexadecimal characters;
-   equivalent semantic recomputation is identity-stable;
-   feature disposition/operational metadata does not affect identity;
-   empty FeatureSets have deterministic snapshot-bound identities;
-   snapshot identity/version participate correctly;
-   ordered feature evidence participates correctly;
-   decimal canonicalization is culture-independent;
-   timestamp/offset canonicalization is deterministic;
-   no binary floating point participates;
-   no persistence or I/O participates in identity computation;
-   no permanent-test delta occurred;
-   no package/reference/schema delta occurred.

------------------------------------------------------------------------

## 18. Mutation Boundary

The only newly clarified production authority is the minimum canonical
feature identity computation needed by WP06.

Expected overall WP06 layer delta remains:

-   Domain: `0`
-   Application: bounded feature-computation/identity implementation
    only
-   Infrastructure: `0`
-   Worker: `0`

No unrelated documentation or governance file should be semantically
modified by execution.

Do not modify this clarification authority during execution.

------------------------------------------------------------------------

## 19. Lifecycle Authority

Issue #158 remains the only issue whose lifecycle may change.

After all original WP06 gates plus this clarification's identity gates
pass:

1.  post completion evidence to #158;
2.  close #158;
3.  set Project #2 status to Done;
4.  verify #159 remains Open/Backlog and untouched.

No commit, push, branch, PR, tag, release, or integration action is
authorized.

------------------------------------------------------------------------

## 20. Stop Conditions

Stop with:

`RELEASE 1.4 WP06 BLOCKED`

if:

-   WP03 canonical identity semantics cannot be implemented without
    reinterpretation;
-   required identity evidence is absent from the accepted WP04/WP05
    model;
-   manifest authority does not permit the minimal Application identity
    implementation;
-   implementation requires persistence/schema evolution;
-   implementation requires a package/project-reference change;
-   implementation requires changing snapshot/dataset/pipeline
    identities;
-   WP07 must be implemented to make the identity computation work;
-   correct implementation requires a generalized feature engine;
-   canonical validation cannot pass within the clarified WP06 scope.

Do not guess.

------------------------------------------------------------------------

## 21. Final Reporting Requirement

The restarted WP06 execution report must explicitly include:

-   acknowledgement that the prior WP06 attempt was blocked before
    mutation;
-   this clarification as the authority resolving identity ownership;
-   exact identity-computation files added/modified;
-   Feature Definition Identity canonicalization evidence;
-   Feature Set Identity canonicalization evidence;
-   decimal canonicalization evidence;
-   timestamp/offset canonicalization evidence;
-   empty FeatureSet identity evidence;
-   equivalent recomputation evidence;
-   different-snapshot identity distinction;
-   proof that operational metadata is excluded;
-   proof that no persistence/DI/Worker/WP07 work was introduced;
-   all original WP06 reporting requirements.

On successful completion, use the original terminal marker:

`RELEASE 1.4 WP06 COMPLETE`

followed by:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Feature Validation & Failure Mapping — GitHub issue #159`

Do not start WP07.

# Release 1.5 WP03 — Experiment Identity, Provenance & Evidence

## GitHub Issue
`#170 — Release 1.5 WP03 — Experiment Identity, Provenance & Evidence`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP03 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme to freeze in WP03:

`aiq-experiment-identity-v1`

WP03 owns the exact semantic identity, provenance, lineage, canonical representation, and evidence rules deliberately deferred by WP02. It is a semantic-definition work package only.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- Release 1.4 feature identity/provenance/evidence semantics
- Release 1.4 feature engineering semantics
- Release 1.3 pipeline identity/provenance/evidence semantics
- Release 1.2 dataset identity/provenance semantics
- relevant public-contract, lifecycle, architecture, configuration, and observability authorities
- WP01 and WP02 completion evidence
- this WP03 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If accepted authorities materially conflict, stop rather than silently inventing a reconciliation.

---

## 2. Objective

Create exactly the manifest-authorized WP03 semantic artifact that freezes `aiq-experiment-identity-v1`.

The artifact must define:

- Experiment Definition Identity;
- Experiment Result Identity;
- canonical representation;
- hashing algorithm and external fingerprint form;
- domain separation;
- field ordering and component framing;
- experiment-definition binding;
- exact Feature Set binding;
- count and aggregate evidence encoding;
- successful-empty-result encoding;
- decimal canonicalization;
- provenance and lineage;
- equivalence and distinctness;
- evidence-established-only rules;
- integrity contradiction semantics;
- exclusion of operational metadata;
- provider/storage independence;
- acyclic relationship to predecessor identities.

WP03 must not implement production identity code. Concrete model/contracts begin in WP04.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Expected lifecycle:

- #168 WP01: CLOSED / Done;
- #169 WP02: CLOSED / Done;
- #170 WP03: OPEN / Backlog;
- #171 WP04: OPEN / Backlog;
- #172–#180: OPEN / Backlog;
- milestone #46: OPEN with 11 open / 2 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected permanent technical baseline:

- Domain.Tests: 11;
- Application.Tests: 86;
- Infrastructure.Tests: 104;
- Architecture.Tests: 13;
- total: 214.

Accepted untracked Release 1.5 governance/semantic artifacts and manifest-defined out-of-band execution inputs are not blockers. Reconcile exact paths from repository truth.

If #169 is not Closed/Done or #171 has started, stop before mutation.

---

## 4. WP03 Lifecycle Start

Only after starting-state reconciliation passes:

- move #170 Project #2 Status from Backlog to In Progress.

Read back the state.

If #170 is already In Progress solely because this exact WP03 execution partially started, continue idempotently if no unauthorized mutation occurred.

#171 must remain OPEN / Backlog throughout WP03.

---

## 5. Predecessor Identity Reconciliation

Before defining experiment identities, inspect and preserve predecessor identity schemes.

### Release 1.2 — Dataset

Preserve the accepted `aiq-dataset-identity-v1` principles, including:

- SHA-256 semantic fingerprints;
- lowercase hexadecimal external form;
- deterministic canonical representation;
- immutable dataset/snapshot/version identity;
- provenance and lineage;
- semantic equivalence;
- integrity contradiction behavior.

Do not alter dataset identities.

### Release 1.3 — Pipeline

Preserve `aiq-pipeline-identity-v1`, including:

- distinct definition and execution identities;
- semantic rerun equivalence;
- first-failure evidence limits;
- operational metadata exclusion;
- acyclic identity derivation.

Do not alter pipeline identities or the fixed five-stage pipeline.

### Release 1.4 — Feature

Preserve `aiq-feature-identity-v1`, including:

- distinct Feature Definition and Feature Set identities;
- SHA-256 with 32 digest bytes / 64 lowercase hexadecimal characters;
- deterministic BOM-free UTF-8 canonical representation;
- ordinal semantics;
- explicit identity domains;
- fixed field order;
- invariant field counts;
- byte-length-delimited components;
- invariant decimal canonicalization;
- exact Feature Set binding to snapshot/version evidence;
- equivalent recomputation identity;
- successful empty Feature Set identity;
- acyclic provenance/lineage;
- operational metadata exclusion.

Experiment identity semantics should reuse established canonicalization principles where semantically appropriate, without aliasing experiment identities to feature identities.

---

## 6. Identity Scheme

Freeze the scheme identifier exactly as:

`aiq-experiment-identity-v1`

All Release 1.5 experiment semantic identities governed by WP03 must use this scheme.

Fingerprint requirements:

- SHA-256;
- exactly 32 digest bytes;
- exactly 64 lowercase hexadecimal characters externally;
- no uppercase normalization variants;
- no culture-dependent representation;
- no platform-dependent representation.

Do not define multiple identity scheme versions in Release 1.5.

---

## 7. Identity Types

Freeze exactly two distinct semantic identity categories:

1. **Experiment Definition Identity**
2. **Experiment Result Identity**

They must not be interchangeable.

The identity type/domain must participate in canonical representation so that identical payload bytes in different semantic identity categories cannot intentionally collapse into the same semantic domain.

Do not introduce experiment-run, invocation, persistence-record, registry-entry, workspace, or scheduler identities.

---

## 8. Experiment Definition Identity

The Experiment Definition Identity represents only the governed semantics of:

`simple-return-descriptive-summary-v1`

It must bind the semantic definition required to distinguish this experiment from any future experiment definition.

It must exclude:

- Feature Set identity;
- Feature Set values;
- snapshot identity/version;
- computed count/mean/min/max;
- invocation time;
- execution duration;
- process/machine identity;
- correlation IDs;
- paths;
- credentials;
- logging;
- persistence disposition.

The semantic artifact must freeze the exact canonical fields required for the Release 1.5 definition identity.

Do not create a generic configurable statistics identity model.

Any future change to the governed experiment semantics requires a separately governed semantic definition/identity.

---

## 9. Experiment Result Identity

The Experiment Result Identity represents the exact successful immutable experiment result evidence.

It must be distinct from the Experiment Definition Identity.

At minimum it must bind:

- exact Experiment Definition Identity;
- exact accepted Feature Set Identity;
- exact result cardinality/count;
- aggregate-presence state;
- arithmetic mean when present;
- minimum when present;
- maximum when present.

The result identity must remain tied to the exact Feature Set identity even when another Feature Set produces numerically identical aggregates.

Do not use only summary values as result identity input.

Do not include persistence disposition or execution metadata.

---

## 10. Exact Feature Set Binding

Freeze that Experiment Result Identity is downstream of and bound to the exact accepted Release 1.4 Feature Set Identity.

Different Feature Set identities must produce identity-distinct Experiment Results even when:

- count is equal;
- mean is equal;
- minimum is equal;
- maximum is equal.

The experiment must not reconstruct or substitute a Feature Set identity from raw values.

Feature identity remains owned by Release 1.4.

No experiment identity may feed back into Feature Set identity.

---

## 11. Canonical Representation Requirements

Define one deterministic canonical byte representation for each identity type.

Use established repository principles:

- BOM-free UTF-8;
- ordinal semantics;
- explicit identity domain;
- explicit scheme/version;
- fixed field order;
- invariant field count;
- unambiguous component framing;
- byte-length-delimited variable components;
- no locale-sensitive formatting;
- no platform newline dependency;
- no JSON serializer behavior as an implicit identity contract unless an accepted predecessor authority explicitly requires it.

The semantic document must specify the exact field order and framing sufficiently for WP04/WP05 implementation and tests to produce one canonical result.

Do not leave canonical field order ambiguous.

---

## 12. Domain Separation

Freeze explicit canonical domains for:

- Experiment Definition Identity;
- Experiment Result Identity.

Domain strings must be stable, ordinal, and included in the hashed canonical representation.

Choose names consistent with accepted predecessor identity conventions after inspecting repository truth.

Do not guess predecessor domain syntax without inspection.

The document must make clear that definition/result identities cannot collide semantically merely because their remaining fields are equal.

---

## 13. Canonical Component Framing

Preserve the accepted predecessor principle of unambiguous framing.

Each variable-length canonical component must have deterministic framing sufficient to prevent concatenation ambiguity.

If the accepted Release 1.4 identity semantics define a specific byte-length-delimited convention, reuse that convention unless Release 1.5 semantics require an explicit difference.

Record the exact convention in the WP03 artifact.

Do not rely on delimiters that can occur unescaped in values unless the predecessor authority explicitly proves the encoding unambiguous.

---

## 14. Count Encoding

Freeze count as the exact non-negative cardinality established by WP02.

Canonical count encoding must be:

- invariant;
- deterministic;
- unambiguous;
- independent of culture;
- independent of leading-zero textual variants.

Choose and document the exact canonical encoding after reconciling predecessor numeric/integer encoding conventions.

For an empty result:

`count = 0`

For a non-empty result:

`count >= 1`

Count must agree with aggregate-presence semantics.

A contradiction is invalid evidence, not a second valid representation.

---

## 15. Aggregate Presence Encoding

WP02 established:

- count 0 → mean/min/max absent;
- count >= 1 → mean/min/max present.

WP03 must make aggregate presence canonical and unambiguous.

The identity representation must distinguish absence from any decimal value, including zero.

Do not encode an absent aggregate as decimal zero.

Do not use NaN, infinity, sentinel decimal values, empty locale-dependent strings, or omitted fields whose interpretation is ambiguous.

Freeze one deterministic presence representation.

---

## 16. Decimal Canonicalization

For mean, minimum, and maximum, preserve the accepted repository decimal-identity principles.

Canonical decimal representation must be:

- exact;
- invariant;
- independent of culture;
- independent of display formatting;
- independent of binary floating point;
- normalized so semantically equal decimal values do not acquire different identities solely from redundant representation.

Reconcile and reuse Release 1.4's accepted sign/coefficient/scale semantics and trailing-zero normalization where applicable.

The WP03 artifact must define the exact canonical decimal rule used for experiment aggregates.

Do not introduce convenience rounding.

---

## 17. Successful Empty Result Identity

An empty Feature Set produces valid successful experiment evidence:

- count = 0;
- mean absent;
- minimum absent;
- maximum absent.

Freeze that this successful empty result receives a deterministic Experiment Result Identity.

There must be no global empty-result sentinel identity.

Its identity must remain bound to:

- the exact Experiment Definition Identity;
- the exact Feature Set Identity;
- count zero;
- canonical aggregate-absence evidence.

Therefore, empty results from different Feature Set identities remain identity-distinct.

---

## 18. Successful Non-Empty Result Identity

For count >= 1, the Experiment Result Identity must bind all four semantic result components:

- count;
- mean;
- minimum;
- maximum;

in addition to the exact definition and Feature Set identities.

The identity must be computed only after valid complete summary evidence exists.

Do not generate a result identity from partial aggregate evidence.

---

## 19. Equivalence

Freeze:

- same governed Experiment Definition semantics → same Experiment Definition Identity;
- same Experiment Definition Identity + same exact Feature Set Identity + equivalent canonical result evidence → same Experiment Result Identity;
- equivalent recomputation in another process/time/machine → same semantic result identity;
- persistence or presentation differences do not alter semantic identity.

Equivalent identity is semantic evidence, not proof that two separate executions were the same operational event.

Release 1.5 does not create operational run identity.

---

## 20. Distinctness

Freeze identity distinctness where semantic evidence differs.

At minimum:

- different experiment definitions → distinct definition identities;
- different Feature Set identities → distinct result identities;
- different valid count/aggregate evidence → distinct result identities;
- empty results bound to different Feature Sets → distinct result identities.

Do not require different result identities merely because:

- invocation time differs;
- process differs;
- machine differs;
- logging differs;
- database path differs;
- configuration source differs;
- Worker output formatting differs.

---

## 21. Provenance

Experiment result provenance must reference established predecessor evidence rather than redefine it.

At minimum preserve traceability to:

- Experiment Definition Identity;
- exact Feature Set Identity;
- the Feature Set's accepted definition;
- exact dataset snapshot/version evidence already bound by Release 1.4;
- predecessor dataset/source-state provenance.

Do not copy provider-specific or storage-specific operational details into semantic experiment identity merely to demonstrate traceability.

Provenance is semantic evidence; implementation storage remains deferred.

---

## 22. Lineage

Freeze acyclic conceptual lineage:

`source state → dataset definition/research dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

No downstream experiment identity may become input to:

- dataset identity;
- snapshot identity;
- pipeline identity;
- feature identity.

Do not introduce cycles or generalized lineage graphs beyond the bounded Release 1.5 need.

---

## 23. Evidence-Established-Only Rule

Freeze that semantic identities may exist only when their required evidence has been established.

Examples:

- invalid request → no Experiment Result Identity;
- unsupported definition → no valid downstream result identity;
- Feature Set lookup/generation failure → no result identity;
- invalid Feature Set evidence → no result identity;
- numeric summary failure → no result identity;
- integrity contradiction → no newly fabricated result identity.

An Experiment Definition Identity may exist independently because it represents the governed definition, but a successful Experiment Result Identity requires complete valid result evidence.

Do not fabricate placeholder hashes for failures.

---

## 24. Integrity Contradictions

Freeze that equal semantic identities with contradictory canonical semantic content represent an integrity contradiction.

Do not normalize such contradictions into equivalence.

Examples include an asserted identical Experiment Result Identity paired with different:

- Feature Set identity;
- count;
- aggregate presence;
- mean;
- minimum;
- maximum.

Exact failure mapping/type naming remains WP06-owned unless already governed, but the semantic contradiction must be explicit here.

---

## 25. Failure Evidence Boundary

WP03 must preserve WP02 fail-stop semantics.

Failure evidence must not claim downstream semantic identity that was never established.

First governed failure prevents construction of later successful result evidence.

Unknown defects remain unknown defects and must not be converted into synthetic identity-bearing failure success.

Do not define durable failure-history identity in Release 1.5.

---

## 26. Operational Metadata Exclusion

Explicitly exclude from Experiment Definition and Experiment Result identities:

- invocation/start/end timestamps;
- duration;
- current wall-clock time;
- process ID;
- machine/host identity;
- thread identity;
- correlation/request IDs;
- filesystem paths;
- database paths;
- connection strings;
- credentials/API keys;
- logging levels/messages;
- metrics/traces;
- retry counts;
- scheduling state;
- persistence disposition;
- Worker exit code;
- Git SHA/build number unless already part of the governed semantic definition, which Release 1.5 does not require.

Operational evidence may be useful for diagnostics but is not semantic experiment identity.

---

## 27. Provider and Storage Independence

`aiq-experiment-identity-v1` must not depend on:

- Twelve Data;
- HTTP;
- provider payload shape;
- provider credentials;
- SQLite APIs;
- SQL;
- table row IDs;
- database paths;
- filesystem paths.

The experiment consumes accepted Feature Set semantic evidence.

SQLite remains schema v2.

No experiment identity persistence, registry, cache, or history is introduced.

---

## 28. Relationship to Release 1.3 Pipeline Identity

Experiment identities are not pipeline identities.

Do not:

- append experiment identity to the Release 1.3 pipeline identity;
- create a sixth pipeline stage;
- redefine pipeline execution identity;
- make pipeline identity depend on experiment output.

The Release 1.3 fixed five-stage pipeline remains unchanged.

---

## 29. Relationship to Release 1.4 Feature Identity

Experiment identities are downstream of Feature Set identity.

Preserve:

- `aiq-feature-identity-v1`;
- Feature Definition Identity;
- Feature Set Identity;
- Feature Set equivalence;
- exact snapshot/version binding;
- feature provenance/lineage.

Do not recompute or reinterpret feature identity semantics under `aiq-experiment-identity-v1`.

---

## 30. Immutability

Identity-bearing experiment semantic evidence must be immutable.

Once established, the semantic content associated with an Experiment Definition Identity or Experiment Result Identity cannot be mutated while retaining the same identity.

WP03 defines this requirement; WP04 owns the concrete immutable model/contracts.

---

## 31. Explicit Deferrals

Keep outside WP03 and Release 1.5 unless separately governed:

- experiment persistence;
- experiment registry/history;
- experiment run identity;
- workspace/notebooks;
- additional experiments/statistics;
- configurable formulas;
- strategies/signals/backtesting;
- portfolio/risk;
- AI/ML/MLOps;
- live acquisition;
- scheduling/retries/recovery/checkpoints;
- generalized plugins/DAGs;
- distributed execution;
- durable telemetry;
- identity migration/version 2;
- cross-release generic identity framework refactoring.

Do not create placeholders for these capabilities.

---

## 32. Authorized Repository Mutation

WP03 may create exactly the semantic artifact assigned to WP03 by:

`docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`

Use the exact manifest path and filename.

If the manifest does not unambiguously assign one WP03 semantic artifact, stop.

Expected WP03 deltas:

- semantic documentation: exactly 1 manifest-authorized file;
- production: 0;
- tests: 0;
- Domain: 0;
- Infrastructure: 0;
- Worker: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not modify `EXPERIMENT_SEMANTICS.md` unless the accepted manifest explicitly assigns such a change to WP03. A contradiction with WP02 requires a stop, not silent predecessor editing.

Do not stage or commit.

---

## 33. Artifact Quality Gate

The WP03 semantic artifact must be sufficiently exact that later implementation can derive deterministic test vectors without guessing.

It must explicitly specify:

- scheme;
- identity types;
- domains;
- canonical field order;
- component framing;
- hash/external form;
- definition binding;
- Feature Set binding;
- count encoding;
- aggregate presence;
- decimal canonicalization;
- empty result;
- non-empty result;
- equivalence/distinctness;
- provenance/lineage;
- evidence-established-only rule;
- integrity contradictions;
- operational exclusions.

Avoid implementation-language-specific APIs unless needed to remove semantic ambiguity.

---

## 34. Validation

Run canonical Release verification:

`eng/verify.ps1 -Configuration Release`

Expected:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of the new untracked WP03 artifact and relevant governance companions.

Require:

- trailing whitespace: 0;
- database/WAL/SHM/journal/generated residue: 0;
- provider/network calls: 0;
- real credentials: 0.

No permanent tests are added in WP03.

---

## 35. Semantic Reconciliation Gate

Before closure, prove WP03 is consistent with:

- Release 1.5 definition;
- Release 1.5 execution plan;
- Release 1.5 file manifest;
- `EXPERIMENT_SEMANTICS.md`;
- Release 1.4 feature identity/provenance/evidence semantics;
- Release 1.3 pipeline identity and fixed pipeline;
- Release 1.2 dataset identity/provenance;
- architecture boundaries;
- SQLite schema v2.

Do not resolve contradictions by modifying predecessor authorities.

---

## 36. Repository and Git Protection

Do not:

- modify production code;
- modify tests;
- modify schema;
- modify packages/projects/references;
- implement WP04+;
- stage;
- commit;
- create/switch integration branches;
- push;
- create/merge PRs;
- tag;
- release;
- delete accepted governance artifacts.

Git transport mutation budget:

`0`

Repository mutation budget:

exactly one manifest-authorized WP03 semantic artifact.

---

## 37. Authorized GitHub Mutation Budget

At WP03 start, after gates pass:

1. #170 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP03 completion-evidence comment to #170;
3. close #170 as completed;
4. set #170 Project Status to Done.

Do not mutate #171.

Milestone #46 remains OPEN.

---

## 38. Completion Gate

WP03 may close only if:

- #168 and #169 are Closed/Done;
- #170 was In Progress during execution;
- #171 remains Open/Backlog;
- exactly one manifest-authorized WP03 semantic artifact exists;
- `aiq-experiment-identity-v1` is frozen without ambiguity;
- definition/result identities are distinct;
- canonical representation is exact;
- Feature Set/result binding is exact;
- empty/non-empty identity semantics are exact;
- decimal canonicalization is exact;
- provenance/lineage are acyclic;
- evidence-established-only and integrity rules are explicit;
- operational metadata is excluded;
- production/test/package/project/reference/schema deltas are zero;
- canonical verification passes;
- tests remain 214/214;
- Architecture.Tests remain 13/13;
- warnings/errors 0/0;
- Gitleaks/format/whitespace pass;
- residue 0;
- provider/network execution 0;
- Release 1.6 work 0.

If any gate fails, do not close #170 or mark Done.

---

## 39. Completion Evidence Comment

On success, post concise evidence to #170 covering:

- semantic artifact path;
- `aiq-experiment-identity-v1`;
- SHA-256 / 64 lowercase hexadecimal form;
- distinct Experiment Definition/Result identities;
- canonical domains/framing/field ordering;
- exact Feature Set binding;
- count and aggregate presence encoding;
- decimal canonicalization;
- successful empty-result identity;
- equivalence/distinctness;
- provenance/acyclic lineage;
- evidence-established-only and integrity contradiction rules;
- operational metadata exclusion;
- schema v2 / no persistence;
- zero production/test/package/reference/schema delta;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #171 preserved Open/Backlog.

---

## 40. Final Read-Back

After successful closure verify:

- #170: CLOSED / Done;
- #171: OPEN / Backlog;
- #172–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 10 open / 3 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted untracked Release 1.5 artifacts accurately.

---

## 41. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #169 is not Closed/Done;
- #171+ started unexpectedly;
- WP03 manifest ownership is ambiguous;
- WP02 and accepted identity authorities materially conflict;
- canonical representation cannot be specified without guessing;
- premature WP04+ implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- satisfying WP03 requires production/test/schema/package/reference mutation.

Report the smallest corrective authority required.

---

## 42. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. predecessor identity schemes preserved;
6. semantic artifact created;
7. experiment identity scheme;
8. definition identity;
9. result identity;
10. canonical domains/representation/framing;
11. count/presence/decimal encoding;
12. Feature Set binding;
13. empty/non-empty identity semantics;
14. equivalence/distinctness;
15. provenance/lineage;
16. evidence-established-only rule;
17. integrity contradictions;
18. operational exclusions;
19. provider/storage/pipeline protection;
20. deferrals;
21. repository delta;
22. canonical validation/test counts;
23. security/whitespace/residue;
24. GitHub lifecycle mutations;
25. final #170/#171/milestone state;
26. findings/blockers;
27. next authorized WP.

---

## 43. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP03 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP04 — Experiment Model & Contracts — GitHub issue #171`

Do not begin WP04.

If blocked, end:

`RELEASE 1.5 WP03 BLOCKED`

and identify the smallest corrective authority required.

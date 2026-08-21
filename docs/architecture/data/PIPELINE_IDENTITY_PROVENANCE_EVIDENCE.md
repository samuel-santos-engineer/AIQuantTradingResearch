# Pipeline Identity, Provenance, and Evidence Semantics

## 1. Purpose

This document freezes the Release 1.3 identity, provenance, lineage,
equivalence, canonical-representation, and semantic-evidence model for the
fixed Research Pipeline. It builds on `RESEARCH_PIPELINE_SEMANTICS.md` and
preserves the accepted Release 1.2 dataset identity model without defining
Application contracts or implementation.

## 2. Frozen pipeline boundary

The identity model applies only to the fixed ordered topology:

```text
historical observation retrieval
  -> deterministic dataset materialization
  -> immutable snapshot persistence
  -> catalog registration
  -> structured pipeline result/evidence
```

The source is accepted persisted Release 1.1 historical observations. Live
acquisition, dynamic stages, scheduling, retries, DAGs, and durable run history
are outside the identity domain.

## 3. Distinct identity concepts

The following concepts must remain separate:

- **Pipeline Definition Identity:** immutable identity of the semantic pipeline
  definition used for a dataset definition.
- **Semantic Pipeline Execution Identity:** immutable identity of one semantic
  terminal execution result over relevant accepted source state.
- **Dataset Definition Identity:** the existing Release 1.2 identity of the
  dataset semantics requested by the pipeline.
- **Dataset Snapshot Identity / Dataset Version:** the existing Release 1.2
  identity of materialized dataset content.
- **Relevant Source State Identity:** the existing deterministic evidence for
  the selected persisted observations.
- **Operational Invocation Identifier:** non-semantic correlation evidence for
  one process invocation.

None is an alias for another. In particular, an operational invocation cannot
create or modify a semantic identity.

## 4. Identity scheme and domains

Release 1.3 pipeline semantic identities use the scheme:

`aiq-pipeline-identity-v1`

This scheme is owned by the Application pipeline semantic boundary and is
distinct from `aiq-dataset-identity-v1`. Each canonical representation includes
an explicit identity-type domain marker:

- `pipeline-definition`;
- `pipeline-execution-success`; or
- `pipeline-execution-failure`.

The scheme and domain marker participate in canonical content and prevent
cross-type or cross-scheme interpretation.

## 5. Pipeline Definition Identity

Pipeline Definition Identity is determined by:

1. identity scheme and `pipeline-definition` domain;
2. pipeline semantic-model version;
3. the fixed ordered stage identifiers and their semantic versions;
4. Dataset Definition Identity; and
5. any explicit semantic parameter capable of changing semantic output.

The Dataset Definition Identity is a dependency, not a replacement for the
Pipeline Definition Identity. It identifies dataset meaning; the pipeline
identity additionally identifies the ordered orchestration semantics that
produce and register that dataset.

Excluded facts include invocation time, duration, host/process/thread identity,
filesystem path, connection string, environment name that does not change
semantic input, logging configuration, random values, credentials, correlation
identifiers, database row identifiers, and natural database ordering.

## 6. Semantic Pipeline Execution Identity

Semantic Pipeline Execution Identity represents the terminal semantic result of
one execution. Its derivation is acyclic:

```text
Dataset Definition Identity
  + fixed pipeline semantic model
  -> Pipeline Definition Identity

Pipeline Definition Identity
  + validated request semantics
  + Relevant Source State Identity
  + terminal semantic outcome
  + established output identity references
  -> Semantic Pipeline Execution Identity
```

For success, established output references include the Dataset Snapshot
Identity / Version and catalog evidence identity or equivalent immutable
reference. For failure, they include only identities established before the
first failing stage. The execution identity never depends on an operational
identifier or on itself.

## 7. Success identity semantics

A newly accepted success, equivalent-existing success, and valid empty-dataset
success each have a Semantic Pipeline Execution Identity.

`NewlyAccepted` and `EquivalentExisting` are persistence dispositions, not
identity-bearing semantic differences. Equivalent reruns over equivalent source
state therefore produce the same semantic execution identity even when the
first invocation accepts new durable evidence and the second recognizes it.

An empty success is identity-bearing in the normal way: the deterministic empty
Dataset Snapshot Identity / Version participates as the established output.

## 8. Failure identity semantics

A terminal failure has a Semantic Pipeline Execution Identity so that repeated
equivalent failures can be distinguished from different semantic failures. Its
canonical content contains:

- Pipeline Definition Identity;
- validated request semantics available before execution;
- Relevant Source State Identity when established;
- the first failing stage ordinal and semantic identifier;
- bounded semantic failure classification; and
- identities of upstream evidence established before failure.

It excludes downstream dataset, snapshot, or catalog identities that were not
established. Unknown failures are not coerced into a false category. A change
in failing stage or bounded semantic failure classification creates
distinguishable failure identity evidence.

## 9. Operational invocation identity

An Operational Invocation Identifier may be unique, random, and different for
each process invocation. It exists only for runtime correlation. It is not part
of pipeline or dataset semantic identity, does not affect equivalence, cannot
create a semantic version, and need not be durably persisted. Its concrete type
is reserved for later contracts and hosting work.

## 10. Pipeline version semantics

Release 1.3 does not introduce a separate mutable Pipeline Version. Pipeline
Definition Identity already provides immutable, content-derived version
semantics. Terms such as `latest pipeline version`, numeric revision counters,
and mutable version pointers are rejected because they add no semantic value
and would weaken reproducibility.

## 11. Canonical representation

Pipeline identities use deterministic length-delimited canonicalization
consistent with Release 1.2 principles. The conceptual byte representation is
an ordered sequence of fields, each encoded as:

```text
utf8-byte-length ":" utf8-bytes
```

Nested ordered collections encode their element count followed by each element
in semantic order. Optional fields encode an explicit presence marker before
their value. No delimiter is interpreted inside a length-delimited value.

The representation is:

- UTF-8 without serializer-default dependence;
- culture and local-time-zone independent;
- explicit about scheme, domain, and semantic-model version;
- stable in field and collection ordering; and
- independent of `GetHashCode`, runtime object identity, database natural row
  order, filesystem location, or operational metadata.

## 12. Digest and external identity form

The canonical representation is fingerprinted with SHA-256 and encoded as
exactly 64 lowercase hexadecimal characters. The conceptual external form is:

```text
<identity-type>:aiq-pipeline-identity-v1:sha256:<64-lowercase-hex>
```

The fingerprint is compact deterministic identity evidence. It is not
authentication, authorization, a digital signature, or proof that collisions
are impossible.

## 13. Canonical definition fields

Pipeline Definition Identity canonical fields, in order, are:

1. scheme: `aiq-pipeline-identity-v1`;
2. domain: `pipeline-definition`;
3. pipeline semantic-model version;
4. ordered stage count;
5. for each stage: ordinal, semantic identifier, and semantic version;
6. Dataset Definition Identity including its own scheme-qualified value; and
7. ordered semantic parameter names, types, and canonical values.

Semantic parameters are ordered by ordinal field definition, never by runtime
map enumeration.

## 14. Canonical success-execution fields

Success execution canonical fields, in order, are:

1. scheme and `pipeline-execution-success` domain;
2. Pipeline Definition Identity;
3. canonical validated request semantics, including target and `[from,to)`;
4. Relevant Source State Identity;
5. terminal outcome `success`;
6. Dataset Snapshot Identity / Version;
7. immutable catalog evidence identity/reference; and
8. ordered semantic stage outcomes excluding persistence disposition.

The empty-dataset case uses the ordinary deterministic empty snapshot identity;
it introduces no sentinel observation or mutable special version.

## 15. Canonical failure-execution fields

Failure execution canonical fields, in order, are:

1. scheme and `pipeline-execution-failure` domain;
2. Pipeline Definition Identity;
3. canonical validated request semantics;
4. explicit presence and value of Relevant Source State Identity;
5. terminal outcome `failure`;
6. failing stage ordinal and semantic identifier;
7. bounded semantic failure classification; and
8. ordered, scheme-qualified upstream identity references established before
   failure.

Unavailable downstream fields are omitted through explicit absence markers,
not fabricated placeholders.

## 16. Equivalence and distinguishability

Equivalent definition, request, relevant accepted source state, terminal
semantic result, and established output evidence produce equivalent execution
identity and provenance. A later operational invocation or a different
new/existing persistence disposition does not create a new semantic identity.

The following relevant changes produce distinguishable identity or evidence:

- target or `[from,to)` change;
- dataset semantic parameter or Dataset Definition Identity change;
- pipeline semantic-model, topology, stage meaning, or semantic parameter
  change;
- selected source membership, observation instant/offset, or decimal value
  change;
- resulting Dataset Snapshot Identity / Version change;
- terminal success/failure change; or
- failing stage or bounded semantic failure classification change.

Logging, timing, process, correlation, path, connection, and equivalent
persistence-disposition changes are excluded.

## 17. Provenance semantics

Pipeline provenance is the immutable semantic explanation of how pipeline
evidence was derived. Where established, it makes knowable:

- pipeline identity scheme and Pipeline Definition Identity;
- Semantic Pipeline Execution Identity;
- Dataset Definition Identity;
- Relevant Source State Identity;
- Dataset Snapshot Identity / Version and catalog evidence;
- the fixed ordered stage model and semantic outcomes;
- terminal success or failure; and
- the first failing stage and bounded failure category.

Release 1.2 dataset provenance is referenced by its accepted identities rather
than duplicated or reinterpreted. Release 1.1 observation semantics remain the
source foundation.

## 18. Pipeline lineage

Pipeline lineage is the narrow acyclic relationship:

```text
Pipeline Definition Identity
  + Relevant Source State Identity
  -> Semantic Pipeline Execution Identity
  -> Dataset Snapshot / catalog evidence when established
```

For failure, lineage ends at the last established upstream evidence. It does
not claim downstream completion or form a general DAG lineage engine.

## 19. Pipeline evidence

Immutable Pipeline Evidence explains the definition used, relevant source
state, ordered stages, semantic stage outcomes, terminal result, dataset
evidence when produced, failure evidence when applicable, and their identity
and provenance relationships. Semantic evidence is independent from logging
format and operational telemetry.

## 20. Stage evidence

Each fixed stage contributes, when reached:

- fixed ordinal and semantic identifier;
- semantic outcome: newly accepted, equivalent existing, or failed;
- established input identity references;
- established output identity references; and
- bounded failure classification when failed.

Start/end timestamps, duration, machine/process/thread identity, severity,
retry counters, trace/span identifiers, and correlation identifiers are not
semantic stage evidence.

## 21. Failure and empty evidence

Failure evidence identifies the first failing stage, preserves established
upstream identities, and never implies downstream completion or rollback of
already accepted immutable evidence.

A valid empty success records successful completion, the deterministic empty
Dataset Snapshot Identity / Version, and explicit empty-output provenance. It
uses no sentinel observation or special mutable version.

## 22. Immutability and collision handling

Semantic identities and their accepted canonical evidence are immutable. A
fingerprint cannot be reassigned to contradictory semantic content. Equal
scheme/fingerprint with contradictory canonical content is an integrity
conflict: do not overwrite, alias, or generate a timestamp/random replacement.
No collision-recovery storage mechanism is introduced.

## 23. Scheme evolution

Existing identities retain their original scheme. A future scheme cannot
reinterpret an old fingerprint. Any semantic-model change that changes
canonical meaning requires explicit authority and a new scheme or model
version. Accepted identities are not automatically migrated or re-hashed.

## 24. Evidence persistence boundary

Release 1.3 requires no persisted operational pipeline-run history and no new
SQLite tables or files. SQLite remains schema version 2. Application may return
structured semantic evidence and Worker/logging may surface it, but persistence
of run history, checkpoints, schedules, or operational telemetry is prohibited.

## 25. Architecture ownership

- **Domain:** unchanged.
- **Application:** owns pipeline semantic identities, canonical semantics,
  provenance, lineage, evidence contracts, and later fixed orchestration.
- **Infrastructure:** continues to own observation, snapshot, and catalog
  persistence mechanics; it does not define pipeline semantic identity.
- **Worker:** owns operational invocation and process-level correlation only;
  it cannot redefine semantic identity.

The accepted production dependency graph remains unchanged.

## 26. WP04 handoff

WP04 may express minimal provider- and storage-independent Application
contracts for these concepts. It must preserve the fixed WP02 topology, keep
dataset and pipeline identities distinct, expose no operational metadata as
semantic identity, and introduce no implementation or persistence mechanism.

## 27. Later-work boundaries

WP05 owns orchestration; WP06 owns detailed validation and failure mapping;
WP07 owns structured execution-evidence representation; WP08 owns DI and
configuration; WP09 owns one-shot Worker execution; and WP10+ own tests,
architecture, documentation, and integration. Live acquisition, scheduling,
retries, DAGs, durable run history, schema evolution, and Release 1.4 behavior
remain excluded.

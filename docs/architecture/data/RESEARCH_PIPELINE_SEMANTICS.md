# Research Pipeline Semantics

## 1. Purpose

This document freezes the Release 1.3 semantic model for a fixed,
deterministic, one-shot Research Pipeline. It composes capabilities already
accepted in Releases 1.1 and 1.2 without defining implementation contracts,
identity encoding, hosting mechanics, or future workflow-engine behavior.

## 2. Release boundary

The Research Pipeline begins with an explicit request over persisted historical
observations and ends with a structured terminal result and semantic evidence.
Its topology is fixed for Release 1.3. It is neither a general workflow engine
nor a live market-data acquisition pipeline.

## 3. Vocabulary

- **Research Pipeline:** the fixed semantic process that transforms accepted
  persisted observations into an immutable, cataloged research dataset.
- **Pipeline Definition:** the semantic description of the fixed pipeline,
  including its ordered stages and the rules governing their execution.
- **Pipeline Request:** the validated semantic inputs selecting the target,
  observation range, and dataset definition for one execution.
- **Pipeline Stage:** one required semantic step in the fixed topology.
- **Pipeline Execution:** one evaluation of the definition for one request
  against the relevant accepted persisted source state.
- **Pipeline Result:** the terminal success or failure returned by an execution.
- **Pipeline Success:** completion of every required stage with mutually
  consistent dataset, snapshot, catalog, and pipeline evidence.
- **Pipeline Failure:** terminal fail-stop completion when a required stage
  cannot produce an accepted outcome.
- **Pipeline Stage Outcome:** a stage's completed-new, completed-equivalent, or
  failed semantic outcome.
- **Pipeline Input:** the request plus the relevant accepted persisted source
  state; operational host metadata is not input.
- **Pipeline Output:** the terminal result and bounded semantic evidence; logs,
  duration, process identity, and similar operational data are not output.

## 4. Pipeline topology

The only Release 1.3 topology is:

```text
explicit pipeline request
  -> historical observation retrieval
  -> deterministic dataset materialization
  -> immutable snapshot persistence
  -> catalog registration
  -> structured pipeline result/evidence
```

Every stage is required and executes sequentially. Stages cannot be omitted,
reordered, repeated independently, inserted dynamically, or run in parallel.

## 5. Stage semantics

1. **Historical observation retrieval** selects the persisted Release 1.1
   observations described by the validated request and returns them in the
   accepted deterministic order.
2. **Dataset materialization** applies the Release 1.2 dataset definition and
   canonical semantics to the retrieved observations.
3. **Immutable snapshot persistence** accepts a new snapshot or recognizes an
   equivalent existing snapshot under Release 1.2 rules. A conflict is failure.
4. **Catalog registration** records or recognizes equivalent dataset metadata
   without changing snapshot identity or provenance.
5. **Result/evidence production** reports the terminal outcome and the bounded
   semantic evidence knowable from the completed or failed execution.

No stage is a hook or extension point in Release 1.3.

## 6. Source and acquisition boundary

The pipeline consumes historical observations already persisted through the
Release 1.1 storage boundary. Provider selection, credentials, HTTP transport,
normalization, live acquisition, and provider failure handling occur outside
the pipeline. An explicit pipeline request cannot cause network acquisition or
silently refresh its source data.

## 7. Dataset relationship

The pipeline reuses the Release 1.2 dataset definition, deterministic
materialization, immutable snapshot, catalog, identity, version, provenance,
lineage, ordering, equivalence, conflict, and empty-result semantics. The
pipeline adds orchestration context only; it does not redefine dataset identity
or alter accepted observation and snapshot meaning.

## 8. Definition and request semantics

The Release 1.3 Pipeline Definition denotes the fixed topology and its settled
semantic rules. It contains no runtime service instance, connection string,
filesystem path, provider setting, scheduler setting, or logging configuration.

A Pipeline Request identifies the target and inclusive historical range needed
by the accepted dataset definition. It must be explicit, complete, and valid
before retrieval begins. It does not contain a current-time default, provider
instruction, retry policy, execution schedule, arbitrary stage list, or
operational correlation value.

## 9. Execution semantics

One explicit trigger creates exactly one Pipeline Execution. The stages run
once, synchronously and sequentially, until all complete or the first required
stage fails. An execution produces exactly one terminal Pipeline Result. It
does not remain active, poll, schedule itself, or initiate another execution.

## 10. Re-execution and equivalence

Re-execution is a new evaluation of the same semantic request against the
relevant accepted persisted source state. When that state and all semantic
inputs are equivalent, re-execution must produce equivalent dataset and
terminal semantic evidence. Existing equivalent snapshot or catalog evidence
is a successful idempotent outcome, not a conflict or a newly accepted object.

A changed semantic input or changed relevant persisted source state may produce
a distinguishable result. Operational differences alone cannot do so. WP03
owns the precise identity, provenance, and evidence encoding rules.

## 11. Empty dataset semantics

An empty retrieved observation set is not inherently a pipeline failure. It is
materialized using the accepted Release 1.2 empty-dataset semantics. If every
subsequent stage completes, the pipeline succeeds with valid empty snapshot and
catalog evidence. Any stage-specific integrity or storage failure still causes
fail-stop completion.

## 12. Success semantics

Success requires every stage to complete in order and the final dataset,
snapshot, catalog, and pipeline evidence to agree semantically. A stage may
complete by accepting new evidence or recognizing equivalent existing evidence.
Success never implies that every durable object was newly created.

## 13. Fail-stop semantics

The first failed required stage terminates the execution. Later stages do not
run, and success evidence is not fabricated. The result identifies the failing
stage and a bounded semantic failure category while preserving the accepted
lower-layer failure meaning. Release 1.3 adds no automatic retry, fallback,
partial resume, compensation workflow, or broad exception swallowing.

## 14. Stage outcomes

A stage has one terminal semantic outcome:

- **completed with newly accepted evidence**;
- **completed with equivalent existing evidence**; or
- **failed**, classified as validation, semantic/integrity,
  unavailable dependency/storage, or invalid evidence/input as supported by
  that stage's accepted boundary.

Stages need not expose a shared lower-level result representation. Pipeline
semantics normalize only the bounded outcome required for orchestration.

## 15. Determinism and reproducibility

Pipeline behavior depends only on the semantic request, the fixed definition,
and the relevant accepted persisted source state. It must not depend on current
time, machine or process identity, culture, local time zone, database natural
row order, provider ordering, filesystem path, connection string, random state,
or mutable logging metadata. WP03 will define identity encoding; this work
package does not prescribe byte-level canonicalization.

## 16. Evidence boundary

After execution, semantic evidence must make knowable:

- the Pipeline Definition semantics;
- the semantic run-identity concept, without fixing its encoding;
- the target and dataset definition;
- the produced dataset snapshot/version identity when available;
- the ordered stage sequence and each completed outcome;
- the terminal success or failure;
- the failing stage and bounded failure category when failed; and
- the provenance relation to persisted observations and dataset evidence.

Wall-clock timestamps, duration, process ID, host identity, and log correlation
IDs are operational evidence. They cannot affect semantic identity or
equivalence. No durable pipeline-run history is introduced.

## 17. Architecture ownership

- **Domain:** unchanged; continues to own provider-independent observation
  value semantics.
- **Application:** owns pipeline semantics, future contracts, fixed
  orchestration, semantic validation/failure behavior, and semantic evidence
  contracts.
- **Infrastructure:** owns historical observation storage, dataset snapshot and
  catalog persistence, provider transport, and other storage mechanics.
- **Worker:** owns composition, explicit configuration, one-shot triggering,
  and process-level handling of the terminal result.

The production dependency graph remains Domain -> none, Application -> Domain,
Infrastructure -> Application, and Worker -> Application plus Infrastructure.

## 18. Schema decision

Release 1.3 requires no schema evolution. SQLite remains at schema version 2.
There are no pipeline-definition, run-history, checkpoint, scheduler, retry, or
operational-evidence tables.

## 19. Release 1.4 and later deferrals

Deferred capabilities include live acquisition inside the pipeline,
scheduling/cron, recurring refresh, configurable DAGs, plugin workflow engines,
parallel/streaming/distributed execution, automatic retries, circuit breakers,
provider fallback, durable checkpoints, partial-run resume, persisted
operational run history, metrics or distributed tracing backends,
enrichment/feature generation, model training/evaluation, and MLOps. Release
1.3 introduces no preparatory abstraction solely for these capabilities.

## 20. WP03 handoff

WP03 may define pipeline definition identity, semantic run identity,
provenance, lineage, evidence equivalence, and distinguishability using this
fixed topology and boundary. It must not change stage order, add acquisition,
introduce durable run history, alter dataset identity semantics, or broaden the
Release 1.3 execution model.

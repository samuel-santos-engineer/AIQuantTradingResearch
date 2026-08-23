# Release 1.6 WP10 Composition-Scope Reconciliation Authority

## 1. Purpose

This prompt is the sole corrective authority for the blocked execution of:

**Release 1.6 WP10 — Dependency Registration & Configuration — GitHub issue #191**

The accepted WP10 authority required both:

- Application/Infrastructure durable-service registrations; and
- a Worker-owned Durable Experiment configuration model,

while also prohibiting `Program.cs` routing/execution changes.

Repository truth shows that current Worker configuration types are wired directly through `Program.cs`. Creating an unused Durable Experiment configuration type would not establish a real composition boundary, while wiring it through `Program.cs` would cross into WP11 routing/execution ownership.

This corrective authority therefore narrows WP10 to **Application/Infrastructure dependency registration only** and explicitly defers all Durable Experiment Worker configuration binding and intent/routing integration to WP11.

This is a scope reconciliation, not a semantic redesign.

Issue #191 must remain OPEN / In Progress during this corrective run until the narrowed WP10 validation completes under the resumed WP10 authority.

---

## 2. Superseded WP10 Requirement

The following original WP10 requirement is superseded for WP10 only:

> establish a Worker-owned Durable Experiment configuration model required by WP11.

WP10 no longer owns:

- Durable Experiment Worker configuration type creation;
- Durable Experiment configuration binding;
- partial-intent configuration representation;
- Worker configuration parsing/validation;
- `Program.cs` configuration hook.

Those responsibilities are transferred intact to WP11.

All other WP10 requirements remain authoritative.

---

## 3. WP11 Ownership After Reconciliation

WP11 must own, in one coherent Worker-boundary change:

- Durable Experiment configuration type/model;
- exact configuration key names;
- configuration binding;
- missing/malformed/incoherent/partial-intent validation;
- culture-independent parsing;
- Durable Experiment explicit-intent detection;
- mode precedence;
- `Program.cs` routing;
- one-shot invocation;
- output;
- exit codes;
- no-fallback behavior.

This avoids an unused WP10 configuration artifact and keeps Worker composition with Worker execution ownership.

---

## 4. Starting State

Verify:

- repository `samuel-santos-engineer/AIQuantTradingResearch`;
- cumulative Release 1.6 work remains uncommitted/un-staged;
- #182–#190 CLOSED / Done;
- #191 OPEN / In Progress;
- #192–#195 OPEN / Backlog;
- milestone #47 OPEN;
- schema v3;
- WP09 complete;
- no WP11 implementation has started;
- no `Program.cs` change attributable to WP10;
- no unused Durable Experiment configuration type was created;
- no Release 1.7 work.

If this materially differs, stop.

---

## 5. Narrowed WP10 Scope

WP10 is now limited to:

1. register the existing Application durable Experiment use case exactly once;
2. register the existing Infrastructure durable Experiment evidence store exactly once;
3. verify correct lifetimes;
4. verify predecessor registrations remain singular;
5. prove real DI graph construction/resolution is side-effect-free;
6. preserve all existing Worker configuration/routing unchanged.

No Worker-owned file is required or authorized by WP10 after this reconciliation.

---

## 6. Application Registration

Register the existing durable Experiment Application seam exactly once.

Use actual repository types implemented by WP04/WP05.

Do not introduce aliases, decorators, factories, or duplicate descriptors.

Use the established lifetime convention for stateless Application use cases.

---

## 7. Infrastructure Registration

Register:

`IDurableExperimentEvidenceStore`

to the existing WP07/WP08/WP09 SQLite implementation exactly once.

Reuse the existing database/path configuration already used by Infrastructure.

Do not introduce a separate durable Experiment database or new storage options.

---

## 8. Lifetime and Cardinality

For each WP10-owned registration require:

- exactly one service descriptor;
- exactly one implementation;
- no duplicate registration;
- no hidden service locator;
- no mutable SQLite connection singleton.

Report exact lifetimes.

Predecessor Release 1.3/1.4/1.5 registrations must remain unchanged and singular.

---

## 9. Side-Effect-Free DI Resolution

A real production DI graph build/resolution must not:

- create a database merely because services are resolved;
- migrate schema;
- persist Experiment evidence;
- retrieve Experiment evidence;
- execute Feature generation;
- execute Experiment generation;
- call providers;
- perform network I/O.

Use direct graph validation with a dummy/temporary path if existing options require one.

Any temporary file/path must leave zero residue.

---

## 10. Worker Preservation

WP10 must leave all Worker files unchanged.

Required WP10 Worker delta:

`0`

Do not modify:

- `Program.cs`;
- existing Feature configuration;
- existing Experiment configuration;
- existing pipeline configuration;
- Worker routing;
- output;
- exit codes.

Do not create a Durable Experiment configuration type in WP10.

---

## 11. Manifest Reconciliation

For WP10, interpret the Release 1.6 file manifest as follows:

- Application DI surface: authorized if required;
- Infrastructure DI surface: authorized if required;
- previously anticipated WP10 Worker durable-configuration artifact: **deferred to WP11 and excluded from WP10**.

Do not modify the manifest document itself under this corrective authority.

WP14 must later reconcile the actual accepted WP10/WP11 path ownership against this corrective authority.

---

## 12. Permanent Test Boundary

WP10 still adds no permanent test suite.

Expected permanent total remains:

`238`

Use a removable offline DI probe only.

Do not update Worker tests in WP10.

---

## 13. Temporary DI Probe

Prove:

- durable Application use-case registration exists once;
- durable evidence store registration exists once;
- exact lifetimes;
- predecessor registrations remain singular;
- `BuildServiceProvider` validation succeeds;
- durable Application use case resolves;
- durable store resolves;
- graph resolution performs no database creation/migration;
- no accept/retrieve call occurs;
- no provider/network activity;
- no real credentials;
- no Worker execution;
- zero residue after probe removal.

Remove probe completely.

---

## 14. Canonical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Require:

- Domain 11/11;
- Application 102/102;
- Infrastructure 112/112;
- Architecture 13/13;
- total 238/238;
- skipped 0;
- build warnings/errors 0/0;
- formatting PASS;
- Gitleaks PASS;
- `git diff --check` PASS;
- `git diff --cached --check` PASS;
- direct whitespace/final-newline checks PASS;
- staged paths 0;
- schema v3;
- package/project/reference delta 0/0/0;
- graph unchanged;
- database/WAL/SHM/journal/probe residue 0;
- provider/network activity 0.

---

## 15. GitHub Mutation Budget

This corrective authority itself authorizes no GitHub lifecycle mutation.

Preserve:

- #191 OPEN / In Progress;
- #192–#195 OPEN / Backlog;
- milestone #47 OPEN.

After this reconciliation succeeds, resume the existing WP10 authority from its implementation/validation gate using the narrowed scope established here.

The resumed WP10 authority may then post completion evidence, close #191, and set it Done.

---

## 16. Repository Mutation Budget

Authorized:

- only the Application/Infrastructure DI registration paths already permitted by WP10 and the file manifest.

Not authorized:

- Worker files;
- configuration files/models;
- `Program.cs`;
- schema/storage behavior;
- tests;
- packages/projects/references;
- staging/commit/branch/push/PR;
- Release 1.7 work.

---

## 17. Stop Conditions

Stop if:

- DI registration requires a Worker configuration artifact;
- DI resolution requires `Program.cs` mutation;
- a service constructor causes storage side effects;
- a new package/reference is required;
- Worker behavior must change;
- schema/storage semantics must change;
- permanent tests must change;
- canonical verification fails;
- provider/network activity occurs;
- Release 1.7 work exists.

Report the smallest further corrective authority required.

---

## 18. Completion Report

Report:

1. starting state;
2. scope reconciliation applied;
3. superseded Worker-configuration requirement;
4. WP11 deferred ownership;
5. exact DI paths changed;
6. Application registration;
7. Infrastructure registration;
8. cardinality/lifetimes;
9. predecessor registrations;
10. side-effect-free DI proof;
11. Worker delta = 0;
12. schema/storage preservation;
13. permanent test preservation;
14. canonical 238/238;
15. residue/provider isolation;
16. repository mutation accounting;
17. GitHub mutation accounting;
18. blockers;
19. WP10 resume state.

---

## 19. Completion Marker

On success end exactly:

`RELEASE 1.6 WP10 COMPOSITION-SCOPE RECONCILIATION COMPLETE`

Then:

`NEXT AUTHORIZED ACTION: Resume WP10 — Dependency Registration & Configuration under the narrowed Application/Infrastructure-only scope. Issue #191 remains OPEN / In Progress.`

If blocked end:

`RELEASE 1.6 WP10 COMPOSITION-SCOPE RECONCILIATION BLOCKED`

and identify the smallest corrective authority required.

# Release 1.9 — WP08 Consolidated Implementation + Completion — Fresh Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This prompt is the fresh consolidated implementation/completion authority for Release 1.9 **WP08 — canonical issue #233**.

WP08 may be closed only if every implementation, finite-demonstration, residue, regression, scope, and GitHub lifecycle gate below passes.

WP09 must remain unstarted.

---

# Binding authorities

Read completely and treat as binding:

1. `docs/roadmap/release-1.9/RELEASE_1.9_WP08_LIFECYCLE_BOUNDED_DEMONSTRATION_PROCESS_RESIDUE_CONTRACT_MANIFEST_PATH_AUTHORITY_DEFINITION.md`
2. Accepted Release 1.9 release definition and manifest.
3. Accepted WP05:
   - atomic JSON handoff;
   - runtime-location/lifecycle contract;
   - refresh-cadence/retry contract;
   - manifest/path amendment;
   - completed implementation;
   - deterministic Worker handoff test-isolation fix.
4. Accepted WP06 visualization-frame contract/path amendment and completed implementation.
5. Accepted WP07:
   - canonical idempotency/data-quality semantics;
   - semantic-exposure path amendment and completed exposure implementation;
   - feature/data-quality presentation contract/path amendment;
   - completed presentation implementation;
   - completed lifecycle state (#232 Closed / Done).
6. Any accepted local-repository reconciliation/preservation rule still applicable to shared dirty paths.

The new WP08 definition controls all lifecycle/demonstration/residue values and path ownership.

Do not substitute alternative timeout, readiness, shutdown, residue, or evidence rules.

---

# Entry state

Expected accepted state:

- #232: Closed / Done.
- #233: Open / Backlog.
- WP09 canonical issue: Open / Backlog and unstarted.
- milestone #58: Open.
- full .NET predecessor: **309/309**.
- build: 0 warnings / 0 errors.
- WP05 Python: **3/3**.
- WP06 Python: **6/6**.
- WP07 semantic exposure: **2/2**.
- WP07 presentation: **2/2**.
- Streamlit: **1.61.1**.
- `pip check`: clean.

Verify all current facts.

Do not require a clean worktree. Preserve accepted predecessor and unrelated local changes.

---

# Objective

Implement only the WP08 surface authorized by the binding WP08 lifecycle/demonstration/residue definition.

Then execute and prove the exact finite acceptance protocol for #233, including the binding subset of:

- Worker readiness;
- Streamlit readiness/listener ownership;
- real Worker → handoff → Python projection/presentation observation;
- bounded refresh;
- Worker cancellation;
- Worker restart;
- prior-session cleanup;
- session/revision behavior;
- independent Streamlit shutdown;
- process/listener cleanup;
- canonical handoff final state;
- temporary handoff cleanup;
- temporary database/sidecar cleanup;
- finite demonstration duration.

Do not implement WP09.

---

# Phase 0 — Contract extraction

Before mutation, extract verbatim/normatively from the WP08 definition:

- exact total demonstration bound;
- every startup/readiness/observation/shutdown/restart timeout;
- harness polling interval;
- Worker readiness condition;
- Streamlit readiness condition;
- listener/port allocation rule;
- success-observation condition;
- number of sequential refresh observations;
- cancellation method;
- cancellation success condition;
- forced fallback rule;
- restart sequence;
- shutdown ordering;
- canonical handoff final-state rule;
- temp handoff final-state rule;
- temporary database/sidecar rule;
- evidence/log rule;
- exact authorized production paths/symbols;
- exact WP08 test/harness paths;
- exact optional helper/evidence paths;
- WP08/WP09 ownership boundary.

Create a checklist. If any of these remain undefined in the binding artifact, STOP.

---

# Phase 1 — Repository/GitHub verification

Read-only verify:

- branch/HEAD/origin/ahead-behind;
- staged/unstaged/untracked paths;
- #233 body/state/Project state;
- #232 Closed / Done;
- next WP issue state;
- milestone #58;
- exact authorized WP08 paths;
- no incompatible pre-existing WP08 implementation;
- no WP09 implementation.

If current shared dirty state introduces ambiguity not covered by accepted reconciliation, STOP before mutation.

---

# Phase 2 — Predecessor gate

Before WP08 mutation:

## .NET
- build;
- full regression.

Reference: **309/309**.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic exposure 2/2;
- WP07 presentation 2/2;
- compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

## Residue
Before and after predecessor gate, verify no test-owned:

- Worker process;
- Streamlit process;
- listener;
- handoff temp file;
- temporary database artifact

remains, according to the binding WP08 ownership rules.

Do not globally kill unrelated processes.

If predecessor gate fails, STOP.

---

# Phase 3 — Manifest/path hard gate

For every planned mutation:

- exact path must be authorized by the WP08 definition;
- exact symbol/concern must be authorized;
- shared predecessor ownership must be preserved.

Do not invent a helper path.

Do not reuse WP05/WP06/WP07-exclusive test paths.

Do not consume WP09 paths.

If the binding definition says **no production changes are required**, production mutations must be zero.

---

# Phase 4 — Implement WP08 harness/test surface

Implement the smallest exact acceptance harness/tests defined by the binding artifact.

The harness may orchestrate production processes for finite acceptance only.

It is **not** a production supervisor.

It may:

- launch Worker;
- launch Streamlit;
- track exact owned PIDs/process handles;
- allocate a harness-owned loopback port according to the binding rule;
- observe readiness;
- observe handoff/projection/presentation;
- request governed cancellation;
- restart Worker;
- terminate Streamlit according to the binding rule;
- clean only harness-owned artifacts.

It must not change production Worker/Streamlit mutual independence.

---

# Phase 5 — Isolation

Use only harness-owned isolated resources:

- handoff path/runtime directory;
- temporary database;
- port/listener;
- evidence files if authorized.

Never use/delete:

- developer canonical runtime artifacts;
- developer database;
- unrelated processes;
- unrelated temp directories.

Track resource ownership explicitly.

---

# Phase 6 — Worker readiness test

Implement/prove the exact binding Worker readiness condition.

Require all fixed signals and no additional invented signals.

Use real Worker execution.

Do not replace readiness with an arbitrary sleep.

On timeout:

- clean owned processes/resources;
- fail focused acceptance.

---

# Phase 7 — Streamlit readiness/listener test

Launch Streamlit independently through the harness.

Use the exact binding loopback/port strategy.

Prove:

- Streamlit owns the listener;
- readiness condition is met within timeout;
- Worker does not own a listener introduced by WP08;
- no fixed shared-port collision behavior is introduced.

No browser automation unless explicitly required by the binding definition.

---

# Phase 8 — Real success observation

Prove the exact binding live chain:

`Worker → atomic JSON → WP05 parser/refresh → WP06 frame → WP07 presentation`

Use a Worker-produced handoff.

No fabricated JSON fixture for this demonstration.

If the binding contract separates live Streamlit readiness from deterministic parser/projection observation, implement exactly that split.

Record only bounded evidence.

---

# Phase 9 — Bounded refresh

Execute the exact sequential observation protocol.

Prove:

- initial valid observation;
- required newer publication(s);
- observation occurs within the fixed maximum window;
- accepted revision comparison is respected;
- unchanged/equivalent state does not fabricate a transition;
- no cadence/retry semantic change was made.

Do not assert sub-second precision unless binding authority requires it.

---

# Phase 10 — Worker cancellation

Use the exact existing production cancellation mechanism selected by the WP08 contract.

Prove:

- request issued by harness;
- process exits within graceful timeout;
- expected exit behavior;
- fallback used only if permitted and only after timeout;
- resulting handoff state matches contract.

Do not add production shutdown IPC.

---

# Phase 11 — Worker restart

Execute the exact binding restart sequence.

Prove:

- session A publishes valid envelope;
- A shuts down according to contract;
- allowed post-A canonical state;
- session B uses the same isolated canonical runtime location;
- B startup removes prior-session canonical handoff before new publication;
- B publishes a valid new-session envelope;
- Historical revision reset/no cross-session comparison is proven if required;
- Replay behavior is demonstrated only if binding definition requires it.

Do not infer cross-session ordering.

---

# Phase 12 — Streamlit shutdown

Terminate the harness-owned Streamlit process in the exact binding order/method.

Prove:

- least-forceful allowed termination attempted first;
- exact graceful timeout;
- forced fallback only if allowed/needed;
- listener is released;
- child processes owned by the launch are not left behind.

Do not signal Worker through Streamlit.

---

# Phase 13 — Final shutdown ordering

Execute the exact fixed ordering.

No implementation discretion.

Capture bounded timestamps/evidence sufficient to prove:

- required final observation;
- Worker cancellation/termination;
- Streamlit termination;
- cleanup.

Do not turn timing logs into a new persistent logging subsystem.

---

# Phase 14 — Process/listener residue gate

After each focused lifecycle case and final demonstration:

Require exactly the binding final state.

At minimum verify as applicable:

- zero harness-owned Worker processes;
- zero harness-owned Streamlit processes;
- zero owned child processes;
- zero owned listeners/bound ports.

Never kill by broad process name.

Use tracked PIDs/handles/ports.

Unexpected owned residue = acceptance failure.

---

# Phase 15 — Handoff residue gate

Verify exact binding rules for:

## Canonical file
Assert the exact allowed final state.

## Temp siblings
Assert zero final:
`.visualization-read-model.json.<owned-random-suffix>.tmp`
for the isolated runtime, unless the binding definition names a different exact pattern.

If an intermediate crash artifact is intentionally created, prove next Worker startup cleans it as required.

Streamlit must never perform handoff deletion.

---

# Phase 16 — Database residue gate

Use the exact temporary database ownership/cleanup contract.

At final acceptance verify the required state for:

- main database file;
- `-wal`;
- `-shm`;
- journal/other explicitly governed sidecars.

Only delete harness-owned artifacts.

No schema/persistence behavior change.

---

# Phase 17 — Evidence/log gate

Follow the exact binding evidence rule.

If command/test output only:
- create no persistent evidence directory.

If one persistent evidence path is authorized:
- use only that exact path/format;
- keep it bounded;
- do not create extra logs.

---

# Phase 18 — Focused WP08 acceptance

Run every WP08-owned focused test/harness command fixed by the binding definition.

Report exact test count and duration.

Map every #233 criterion to a focused assertion/evidence item.

No WP09 test may be counted.

---

# Phase 19 — Finite local demonstration

Run the complete real local demonstration under the exact total wall-clock bound.

Record:

- start/end;
- total duration;
- readiness results;
- success observation;
- refresh observation;
- cancellation result;
- restart result;
- shutdown result;
- final residue.

If total duration exceeds the binding maximum, WP08 is not complete even if individual tests passed.

---

# Phase 20 — Python predecessor regression

After WP08 implementation/demonstration run:

- WP05: 3/3;
- WP06: 6/6;
- WP07 semantic exposure: 2/2;
- WP07 presentation: 2/2;
- WP08 focused tests;
- compile/import;
- Streamlit 1.61.1;
- `pip check`.

No new package.

No process/listener residue after suite.

---

# Phase 21 — .NET governed suites

Run definitive:

- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full solution regression.

Predecessor reference: **309/309**.

If the WP08 definition authorizes new .NET tests, calculate and explain the exact expected delta.

Require:

- 0 failed;
- 0 skipped unless already governed;
- build 0 warnings / 0 errors.

---

# Phase 22 — Static scope audit

Search/diff prove zero unauthorized:

- production process supervisor;
- Worker starts/stops Streamlit;
- Streamlit starts/stops Worker;
- HTTP/WebSocket/queue/shared-memory IPC;
- Worker listener;
- schema change;
- persistence redesign;
- provider change;
- feature recomputation;
- revision change;
- refresh/retry redesign;
- WP06 semantic change;
- WP07 semantic/presentation change;
- new package;
- WP09 implementation/tests.

List every changed path and binding authority.

---

# Phase 23 — Preservation audit

For shared predecessor files touched:

- identify pre-existing accepted content;
- identify exact WP08 addition;
- prove predecessor behavior preserved.

Verify unrelated dirty state remains untouched.

If binding WP08 definition requires no production change, prove production diff = zero.

---

# Phase 24 — #233 acceptance gate

Before GitHub mutation, create a table:

`#233 requirement → binding contract → implementation/test → demonstration evidence → residue evidence`

Every row must pass.

Also require:

- predecessor regressions pass;
- finite duration passes;
- scope audit passes;
- final residue passes.

Otherwise leave #233 Open / Backlog.

---

# Phase 25 — GitHub Project identification

Only after repository acceptance:

Identify exactly one Project #2 item for canonical issue #233.

Use robust read-only identification:

- authoritative issue content;
- correct GraphQL/API types;
- exhaustive pagination where needed;
- no login-string-as-numeric-variable mistake.

Resolve:

- Project item node ID;
- Status field ID;
- Done option ID;
- current Release;
- Priority;
- Area/category.

If identity is ambiguous, make zero GitHub lifecycle mutations and BLOCK.

Do not create/delete items.

---

# Phase 26 — GitHub lifecycle completion

After identity proof:

1. set exact #233 Project item Status → Done;
2. read back Done and unchanged governed metadata;
3. close #233;
4. read back Closed;
5. verify next canonical WP issue remains Open / Backlog;
6. verify milestone state/counts.

Do not mutate next WP.

Do not close milestone unless accepted release governance independently requires it and no later Release 1.9 work remains. Otherwise keep it Open.

---

# Phase 27 — Final residue/read-back

After GitHub completion, no source changes.

Perform final read-only checks:

- no harness-owned Worker;
- no harness-owned Streamlit;
- no owned listener;
- handoff final state correct;
- temp handoff residue correct;
- temp database residue correct;
- #233 Closed / Done;
- next WP Open / Backlog.

---

# Completion gate

WP08 completes only if all are true:

1. exact binding lifecycle contract implemented;
2. focused WP08 acceptance passes;
3. finite demonstration passes within bound;
4. cancellation/restart/refresh proof passes;
5. process/listener residue passes;
6. handoff/database residue passes;
7. predecessor Python suites pass;
8. .NET regression passes;
9. build 0/0;
10. scope/preservation audit passes;
11. #233 Project item = Done;
12. #233 = Closed;
13. WP09 remains unstarted.

---

# Blocked behavior

If blocked before mutation:
- mutations zero;
- #233 remains Open / Backlog.

If valid partial WP08 implementation exists but a later gate fails:
- preserve authorized partial work;
- do not close #233;
- report exact failed gate and evidence.

If Project Done succeeds but issue close fails:
- report exact partial lifecycle state;
- do not perform unrelated compensation.

---

# Required completion report

## Binding authorities
List WP08 definition and predecessor authorities.

## Entry state
Repository/GitHub/predecessor baseline.

## Implementation
Exact changed paths/symbols and production-vs-harness boundary.

## Clock/lifecycle evidence
All exact bounds and observed results.

## Demonstration
Real chain and total duration.

## Cancellation/restart/refresh
Exact proof.

## Residue
Processes, listeners, canonical/temp handoff, database/sidecars, logs.

## Focused tests
Exact WP08 count.

## Python regression
WP05/WP06/WP07/WP08/compile/Streamlit/pip.

## .NET regression
Application/Infrastructure/Domain/Architecture/build/full total.

## Scope/preservation
All forbidden categories zero.

## #233 mapping
Requirement → proof.

## GitHub read-back
#233 Closed / Done; metadata preserved; next WP unchanged.

## Milestone
Open/closed raw and canonical counts and state.

## Next eligible work
State exact next canonical work package from Release 1.9 planning.

---

# Mutation statements

Report exact repository mutations.

If GitHub completion succeeds:

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

If no GitHub mutation occurs, report that accurately.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit COMPLETE unless #233 is authoritatively Closed / Done and every finite-demonstration, residue, regression, preservation, and scope gate passes.

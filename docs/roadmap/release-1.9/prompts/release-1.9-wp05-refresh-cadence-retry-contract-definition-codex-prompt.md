# Release 1.9 — WP05 Refresh-Cadence / Retry Contract Definition — Codex Authority

## Authority

This document grants a **very narrow, definition-only authority** for Release 1.9 WP05, canonical GitHub issue **#230**.

The WP05 cross-process architecture is now fixed except for one remaining material consumer contract: Streamlit refresh cadence and retry behavior.

Already-fixed inputs:

- transport = local atomic JSON file;
- base path = `Environment.SpecialFolder.LocalApplicationData`;
- default directory:
  `<LocalApplicationData>\AIQuantTradingResearch\Release1.9\runtime`;
- canonical file:
  `visualization-read-model.json`;
- override key:
  `Visualization:HandoffPath`;
- override must be an absolute full file path;
- Worker and Streamlit are independently launched;
- Worker owns directory creation and all writes;
- Streamlit is read-only;
- Worker startup removes prior-session canonical file;
- graceful Worker shutdown may leave last valid envelope;
- abrupt termination may leave stale file;
- next Worker startup removes prior-session file;
- missing file maps to consumer-local `ProducerUnavailable`, never WP04 `Empty`;
- Streamlit owns bounded polling/read retries;
- Worker never signals Streamlit.

The only unresolved semantics are:

- automatic refresh cadence;
- manual refresh availability;
- retry count;
- retry timing/backoff;
- behavior when file revision is unchanged;
- behavior on transient open/read/parse races;
- behavior after repeated transport read failures;
- cache/last-good envelope use during transient failures.

This authority exists only to define those refresh/retry semantics.

It does **not** authorize implementation.

It does **not** authorize Streamlit code.

It does **not** authorize Worker code.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Produce one explicit, bounded Streamlit refresh/retry contract that a later WP05 implementation authority can implement without inventing timing policy.

The definition must specify exactly:

1. whether automatic refresh is enabled;
2. whether manual refresh is also available;
3. default polling interval;
4. minimum polling interval;
5. maximum polling interval, if configurable;
6. whether cadence is configurable;
7. exact configuration key, if any;
8. retry count per refresh cycle;
9. retry delay strategy;
10. whether exponential backoff is used;
11. behavior when file is missing;
12. behavior when revision is unchanged;
13. behavior when a newer valid revision is read;
14. behavior on transient open/read failures;
15. behavior on parse/contract-version failure;
16. behavior after retry exhaustion;
17. last-good envelope retention semantics;
18. bounds on refresh-related resource use;
19. required future tests;
20. explicit non-goals.

The result must be specific enough for a later Terra implementation/completion authority.

---

# Fixed Consumer Semantics

Do not reopen these.

## Missing file

Consumer-local:

`ProducerUnavailable`

This is transport-local and distinct from WP04:

- Empty
- WarmUp
- Stale
- Failed
- Ready

## Read-only consumer

Streamlit may:

- read canonical file;
- parse/validate envelope;
- cache at most one last-good parsed envelope if this contract allows it;
- compare revisions within valid mode/context;
- render state.

Streamlit must not:

- write handoff file;
- acknowledge revisions;
- mutate Worker state;
- access SQLite;
- call providers;
- recompute features;
- synthesize revisions.

## Atomic file contract

Canonical destination is replaced atomically.

Transient read/open failures may still occur due to platform/file-system behavior, but partial canonical content is not an accepted normal state.

---

# Core Design Principles

## Bounded refresh

Polling must have an explicit lower bound to prevent busy-looping.

## Low latency without churn

Cadence should be responsive enough for simulated-live visualization but not cause excessive reruns or filesystem churn.

## Bounded retry

A single refresh cycle must not retry indefinitely.

## No state fabrication

Transport failures must not be mapped to backend presentation states.

## Last-good safety

A transient transport problem may retain the last-good parsed envelope if explicitly defined, but UI must distinguish transport trouble from fresh backend success.

## Revision-driven update

Unchanged revision should not be treated as new backend state.

---

# Permitted Scope

This authority may read:

- #230;
- Release 1.9 WP05 definition/manifest;
- Streamlit 1.61.1 refresh/rerun patterns already used in repository;
- existing polling/sleep/configuration conventions;
- tests involving file polling or bounded retry;
- Streamlit execution model constraints relevant to polling.

It may define one normative refresh/retry contract.

If governance permits one WP05-owned definition artifact, create only that artifact.

Otherwise return the full contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify code;
- modify tests;
- modify configuration files;
- change transport/path/lifecycle contract;
- change WP04 envelope;
- add Worker signaling;
- add WebSockets/HTTP/sockets/queues;
- add background threads unless later implementation authority explicitly permits them;
- add async event infrastructure;
- modify GitHub;
- close #230;
- start WP06.

This is definition-only authority.

---

# Phase 0 — Read Existing Streamlit Refresh Conventions

Before defining timing:

1. Read #230 completely.
2. Read WP05 manifest/definition.
3. Read current Streamlit entry/application structure.
4. Identify existing use of:
   - `st.rerun`
   - fragments/auto-refresh helpers
   - timers
   - loops
   - sleeps
   - session state
5. Identify whether external dependencies for auto-refresh already exist.
6. Confirm no new dependency is required for the chosen approach if avoidable.
7. Identify any existing configuration key naming conventions for UI timing.

Do not mutate anything.

---

# Phase 1 — Choose Refresh Model

Evaluate:

## Model A — Automatic periodic refresh + manual refresh

Streamlit periodically checks the handoff and also offers an explicit user-triggered refresh.

Assess:
- usability;
- bounded load;
- responsiveness;
- implementation simplicity.

## Model B — Automatic periodic refresh only

Assess whether manual control is unnecessary.

## Model C — Manual refresh only

Use only if #230 does not actually require bounded automatic refresh.

### Preferred direction

If #230 says bounded refresh, prefer Model A or B.

Choose one exact model.

Do not leave automatic/manual behavior ambiguous.

---

# Phase 2 — Define Default Polling Interval

Choose one exact default interval.

Candidate range to evaluate:

- 1 second
- 2 seconds
- 5 seconds
- another repository-supported value

Selection criteria:

- simulated-live responsiveness;
- Streamlit rerun cost;
- local file polling overhead;
- no need for sub-second updates unless #230 explicitly requires them.

Do not choose sub-second polling without explicit evidence.

Every timing choice must include rationale.

---

# Phase 3 — Define Configurability and Bounds

Decide whether polling interval is configurable.

If configurable, define:

- exact configuration key;
- environment-variable mapping;
- type/unit;
- default;
- minimum;
- maximum;
- invalid-value behavior.

Potential semantic key shape:

`Visualization:RefreshIntervalSeconds`

Use only if consistent with repository naming.

Prefer integer seconds unless finer granularity is required.

Example bound shape to evaluate, not pre-authorized:

- min = 1s
- default = 2s or 5s
- max = 60s

Do not define arbitrary huge ranges.

If not configurable, state exact fixed cadence.

---

# Phase 4 — Define Refresh Cycle Semantics

One refresh cycle must perform:

1. resolve canonical handoff path;
2. check file existence;
3. if missing:
   - return `ProducerUnavailable`;
   - no retry storm;
4. if present:
   - attempt read;
   - parse JSON;
   - validate contract version;
   - validate revision;
5. if valid:
   - compare with last-good/current revision;
   - if newer:
     - replace consumer cache;
   - if equivalent:
     - retain current cache;
   - if older:
     - ignore/reject according to fixed revision semantics;
   - if conflict:
     - surface consumer integrity error according to WP04 semantics.

Define exact behavior for each branch.

---

# Phase 5 — Define Retry Count

Choose exact maximum retry attempts within one refresh cycle for transient file access/read failures.

Potential models:

## Model A — No retry

Rely entirely on next polling cycle.

## Model B — One immediate bounded retry

Useful if open/read races can occur.

## Model C — Small bounded retries

Example 2–3 attempts with tiny bounded delay.

### Preferred direction

Prefer the smallest retry count justified by local atomic-replace behavior.

Do not turn each refresh into a long blocking loop.

---

# Phase 6 — Define Retry Delay / Backoff

If retries are used, define exact delay.

Options:

- immediate retry;
- fixed short delay;
- bounded linear/exponential backoff.

Avoid exponential backoff unless repeated retries are actually necessary.

For a local file with atomic replacement, a tiny fixed delay may be sufficient.

Do not invent random jitter unless multiple clients/contention make it necessary.

---

# Phase 7 — Distinguish Failure Classes

Define separate behavior for:

## Missing file
`ProducerUnavailable`.
Normally no within-cycle retry unless startup race semantics justify one.

## File open/share violation / transient IO error
Eligible for bounded retry.

## File disappears between existence check and open
Treat as transient/missing according to exact contract.

## Corrupt/unparseable JSON
Because atomic canonical publication should prevent partial JSON, treat as:
- transport/integrity failure;
- optionally one bounded retry;
- never map to backend Failed.

## Unknown contract version
Non-retryable for that file revision unless file may be replaced on next poll.
Surface safe consumer version error; preserve last-good known-version envelope if allowed.

## Revision integrity conflict
Non-retryable semantic conflict for that read.
Do not overwrite last-good.

---

# Phase 8 — Define Last-Good Envelope Retention

Decide exactly when the consumer retains last-good envelope.

Potential rule:

- on transient transport failure, keep last-good payload visible with a consumer-local transport warning;
- on missing file, choose either:
  - show ProducerUnavailable only; or
  - retain last-good payload with ProducerUnavailable overlay.

This must not be confused with WP04 Stale.

Define whether last-good retention is allowed for:
- missing file;
- transient IO failure;
- corrupt JSON;
- unknown version;
- integrity conflict.

Prefer conservative semantics:
- retain last-good parsed envelope;
- explicitly mark transport/version/integrity issue at consumer layer;
- do not mutate the underlying WP04 state.

If this creates too many new UI states beyond #230, choose a simpler rule.

---

# Phase 9 — Define Unchanged Revision Behavior

When valid file revision is equivalent to currently cached envelope:

- do not replace cache unnecessarily;
- do not create new UI/backend state;
- automatic polling may still rerun UI if Streamlit mechanism requires it, but consumer semantics remain unchanged.

Define whether rendering should short-circuit expensive transformations if revision unchanged.

Do not fabricate Stale solely because revision is unchanged across a few polls.

WP04 Stale remains producer-defined.

---

# Phase 10 — Define Manual Refresh

If manual refresh is included:

- it triggers one immediate refresh cycle;
- it does not alter automatic cadence permanently;
- it follows the same retry/error semantics;
- it does not bypass revision/version validation;
- it does not mutate Worker.

Do not let manual refresh become a command channel.

---

# Phase 11 — Define Retry Exhaustion Behavior

After bounded retry attempts fail:

- stop retrying within that cycle;
- retain or clear last-good according to fixed contract;
- surface consumer-local transport/read error;
- wait until next scheduled/manual refresh.

Do not block indefinitely.

Do not escalate to backend Failed.

---

# Phase 12 — Define Resource Bounds

Specify exact bounds:

- one canonical file read per normal cycle;
- max N retry reads per cycle;
- one cached last-good envelope maximum;
- no historical file accumulation;
- no unbounded error log accumulation in UI state;
- no background retry thread unless later implementation authority explicitly authorizes one.

---

# Phase 13 — Required Future Tests

Define implementation tests.

At minimum:

## Cadence/config
- default interval exact;
- min/max validation if configurable;
- invalid values fail/fallback exactly.

## Missing file
- ProducerUnavailable;
- no false backend Empty;
- bounded retry behavior.

## Unchanged revision
- no cache replacement;
- no false state transition.

## Newer revision
- cache updates exactly once;
- new envelope rendered.

## Older revision
- ignored/rejected.

## Conflict
- integrity error;
- last-good preserved.

## Transient IO
- retries exact count;
- delay strategy exact;
- exhaustion behavior.

## Corrupt JSON
- safe consumer error;
- no backend Failed mapping;
- last-good behavior exact.

## Unknown version
- reject;
- no reinterpretation;
- last-good behavior exact.

## Manual refresh
- immediate one-cycle refresh;
- same validation/retry semantics.

## Resource bounds
- no unbounded retries;
- no extra cache/history growth.

Do not implement tests here.

---

# Non-Goals

This definition must not authorize:

- WebSockets;
- HTTP polling;
- Worker push notifications;
- filesystem watcher/event subscriptions unless separately authorized;
- background retry thread;
- adaptive polling;
- exponential backoff unless selected explicitly;
- user-configurable sub-second refresh;
- schema changes;
- WP04 semantic changes;
- WP06 work.

---

# Stop Conditions

Stop if:

- #230 does not clarify whether automatic refresh is required;
- Streamlit's supported execution model makes periodic refresh materially architecture-dependent;
- selecting a cadence would require adding a third-party dependency;
- repository policy forbids timing configuration;
- multiple materially different refresh models remain equally valid.

On stop:

- make zero production/config/test/GitHub changes;
- report exact unresolved refresh choice;
- identify minimum additional governance authority required.

---

# Success Criteria

This definition authority succeeds only when one complete bounded refresh/retry contract is established that specifies:

- automatic/manual model;
- default cadence;
- configurability;
- min/max bounds if configurable;
- exact configuration key if any;
- per-cycle steps;
- retry count;
- retry delay/backoff;
- missing-file behavior;
- transient IO behavior;
- corrupt JSON behavior;
- unknown-version behavior;
- revision unchanged/newer/older/conflict behavior;
- last-good retention;
- retry exhaustion;
- resource bounds;
- required future tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

WP06 remains unstarted.

---

# Required Completion Report

Return:

## Refresh model
- automatic/manual behavior;
- rationale.

## Cadence
- default;
- min/max;
- configuration key/environment mapping if applicable.

## Refresh cycle
Exact branch behavior.

## Retry
- attempts;
- delay/backoff;
- eligible failure classes;
- exhaustion behavior.

## Revision behavior
- unchanged;
- newer;
- older;
- conflict.

## Last-good retention
Exact semantics by failure class.

## Missing/corrupt/unknown-version
Exact consumer-local behavior.

## Resource bounds
Exact limits.

## Required future tests
List exact scenarios.

## Non-goals
List exclusions.

## Mutation proof

Expected:

`WP05 REFRESH-CADENCE/RETRY DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP05 REFRESH-CADENCE/RETRY CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH CONSOLIDATED AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 REFRESH-CADENCE/RETRY DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 REFRESH-CADENCE/RETRY DEFINITION BLOCKED`

Emit success only if the refresh/retry contract is fully unambiguous.

# Release 1.9 — WP08-Specific Execution Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This prompt is the sole execution authority for Release 1.9 **WP08 — canonical issue #233**.

Begin read-only. Implement only if the repository, issue, accepted Release 1.9 definitions, manifest ownership, and predecessor contracts already provide enough authority to do so without inventing a material contract.

If a material lifecycle/demonstration/residue choice is undefined, stop before mutation and name the minimum narrow follow-up definition/path authority required.

---

# Entry boundary

Accepted predecessor lifecycle:

- WP01–WP07 complete.
- #232 Closed / Done.
- #233 Open / Backlog.
- WP09 and later work packages remain open/unstarted.
- Milestone #58 remains Open.
- Project #2 item for #232 is Done.
- WP08 has not started.

Accepted predecessor validation:

- full .NET regression: **309/309**;
- build: **0 warnings / 0 errors**;
- WP05 Python: **3/3**;
- WP06 Python: **6/6**;
- WP07 semantic-exposure Python: **2/2**;
- WP07 presentation Python: **2/2**;
- Streamlit: **1.61.1**;
- `pip check`: clean.

Verify current reality.

Do not assume a clean worktree is required. Preserve accepted predecessor local work and unrelated local changes.

---

# Binding predecessor surface

Read the accepted Release 1.9 artifacts governing at least:

- release definition/manifest;
- WP04 read-model/revision behavior;
- WP05 local atomic JSON handoff, runtime location/lifecycle, refresh/retry, and manifest amendment;
- WP06 visualization-frame contract/path amendment;
- WP07 canonical idempotency/data-quality semantics;
- WP07 semantic-exposure path amendment and completed exposure implementation;
- WP07 feature/data-quality presentation contract/path amendment;
- any accepted reconciliation/preservation authority still relevant to shared dirty paths.

Treat current implementation as evidence only where it conforms to accepted definitions.

Do not silently redesign predecessor contracts.

---

# Objective

Execute WP08 exactly as defined by issue #233 and accepted Release 1.9 planning.

WP08 is expected to own the Release 1.9 **runtime lifecycle / bounded demonstration / process-residue** acceptance surface that was explicitly excluded from WP06/WP07.

You must derive the exact WP08 requirements from #233 and accepted artifacts before mutation.

Do not infer a demonstration protocol merely from this prompt.

---

# Phase 0 — Read-only GitHub and repository verification

Verify:

1. #233 exact title/body/acceptance criteria/state.
2. #233 Project #2 status and governed metadata.
3. #232 Closed / Done.
4. WP09 canonical issue remains Open / Backlog and unstarted.
5. Milestone #58 remains Open.
6. current branch/HEAD/origin/ahead-behind.
7. staged, unstaged, and untracked paths.
8. accepted predecessor test counts.
9. no existing WP08 implementation already satisfies or partially satisfies #233.

No mutation.

---

# Phase 1 — WP08 requirement extraction

Produce a normative checklist directly from #233 and accepted planning.

Classify each requirement into:

- runtime startup;
- runtime shutdown;
- Worker ownership;
- Streamlit ownership;
- independent process behavior;
- handoff cleanup;
- temporary-file cleanup;
- stale prior-session cleanup;
- bounded refresh/demonstration;
- bounded execution duration;
- process residue;
- file residue;
- evidence capture;
- any other explicitly required WP08 concern.

For every requirement identify:

- existing production behavior that already satisfies it;
- missing implementation, if any;
- required test/evidence surface;
- owning path according to the accepted manifest.

If #233 does not require one of these categories, do not add it.

---

# Phase 2 — Ownership / manifest hard gate

Before mutation, map every required change to an exact authorized path.

Inspect existing Release 1.9 manifest ownership for WP08.

Do not borrow:

- WP05-exclusive test paths;
- WP06-exclusive test path;
- WP07-exclusive test paths;
- WP09 permanent integration/architecture test paths.

Shared production files may be modified only if WP08 already has an explicit symbol/path exception.

If required WP08 production/test/evidence paths are absent from governance, STOP.

Minimum blocker report must name:

- exact missing path;
- exact required symbol/concern;
- why existing ownership cannot be reused;
- minimum narrow manifest/path amendment needed.

Do not invent paths.

---

# Phase 3 — Lifecycle contract sufficiency gate

Determine whether accepted contracts already fix all material WP08 lifecycle choices.

At minimum inspect whether governance fixes:

- who launches Worker;
- who launches Streamlit;
- whether they remain independently launched;
- who terminates each process during demonstration;
- maximum demonstration duration or other boundedness rule;
- readiness condition;
- success observation condition;
- shutdown ordering if any;
- graceful versus forced termination rules;
- cleanup responsibility;
- expected canonical handoff state after demonstration;
- expected temp-file state;
- acceptable final canonical handoff file;
- acceptable process residue;
- failure/timeout behavior.

Preserve the fixed WP05 rule that Worker and Streamlit do not start/stop one another unless a later accepted authority explicitly supersedes it.

If any material choice needed by #233 is undefined, STOP before mutation and request a narrow **WP08 lifecycle/demonstration contract definition authority**.

Do not invent timing values, process supervisors, readiness probes, or cleanup semantics.

---

# Phase 4 — Predecessor regression gate

Before any WP08 mutation, run the governed predecessor checks where safe:

## .NET
- build;
- full solution regression.

Expected reference: **309/309**.

## Python
- WP05: **3/3**;
- WP06: **6/6**;
- WP07 semantic exposure: **2/2**;
- WP07 presentation: **2/2**;
- compilation/import;
- Streamlit 1.61.1 smoke;
- `pip check`.

Verify no pre-existing Streamlit/Worker process residue attributable to the test run.

If predecessor validation fails, stop. Do not repair unrelated predecessor defects under WP08.

---

# Phase 5 — Minimal implementation

Only if Phases 2 and 3 prove sufficient authority:

Implement the smallest WP08 surface required by #233.

Hard constraints:

- no transport redesign;
- no HTTP/WebSocket/queue/shared-memory IPC;
- no Worker↔Streamlit mutual process control unless explicitly authorized;
- no schema change;
- no persistence/provider change;
- no pipeline/feature change;
- no WP04 state/revision change;
- no WP05 cache/retry semantic change;
- no WP06 frame semantic change;
- no WP07 presentation semantic change;
- no package additions;
- no WP09 work.

Reuse the accepted Worker → atomic JSON → Streamlit path.

---

# Phase 6 — Bounded demonstration

Execute only the demonstration protocol actually fixed by accepted WP08 authority.

The demonstration must be bounded and reproducible.

Evidence should prove the #233-required subset of:

- Worker starts successfully;
- Streamlit starts successfully;
- Streamlit consumes the real Worker handoff;
- accepted Historical and/or Replay mode is demonstrated exactly as required by #233;
- refresh occurs within the governed bounded policy;
- displayed/frame data is real canonical data, not a fabricated fixture;
- processes are terminated according to the governed lifecycle;
- no process is left running;
- temp artifacts are cleaned as governed;
- canonical handoff residue matches the governed lifecycle contract.

Do not expand demonstration scope beyond #233.

---

# Phase 7 — Residue audit

After every lifecycle/demonstration test and at final completion, inspect:

- Worker processes;
- Streamlit processes;
- Python child processes if relevant;
- canonical handoff file;
- sibling `.tmp` handoff files;
- test-specific handoff files/directories;
- logs/evidence artifacts if governed.

Classify each remaining artifact as:

- required/allowed;
- test-owned and cleaned;
- unexpected residue.

Unexpected process/file residue blocks completion unless #233 explicitly permits it.

Do not delete unrelated user/repository files.

---

# Phase 8 — WP08-focused acceptance

Run every focused WP08 test/evidence check authorized by the manifest.

Prove each #233 acceptance criterion directly.

Do not count WP09-owned permanent integration/architecture coverage as WP08 completion evidence.

If #233 requires evidence but no WP08-owned test/evidence path exists, stop and request path authority rather than placing tests in WP09.

---

# Phase 9 — Predecessor regression after implementation

Run:

- WP05 Python 3/3;
- WP06 Python 6/6;
- WP07 semantic exposure 2/2;
- WP07 presentation 2/2;
- any new WP08 tests;
- Python compile/import;
- Streamlit 1.61.1 smoke;
- `pip check`;
- Application;
- Infrastructure;
- Domain;
- Architecture;
- build;
- full .NET regression.

Reference predecessor .NET total is **309/309**. Explain any authorized test-count delta.

Require zero failed and build 0 warnings / 0 errors.

---

# Phase 10 — Static scope audit

Diff/search prove zero unauthorized:

- WP09 implementation/tests;
- schema changes;
- persistence/provider changes;
- feature recomputation;
- semantic status changes;
- revision changes;
- transport redesign;
- adaptive/background retry;
- new process supervisor;
- Worker-starts-Streamlit or Streamlit-starts-Worker behavior unless explicitly governed;
- package changes;
- UI redesign.

List every changed path and its WP08 authority.

---

# Phase 11 — Completion gate

Before GitHub mutation, map every #233 acceptance criterion to:

- implementation/evidence;
- focused test/demonstration;
- regression result;
- residue result.

If any criterion is unsupported, leave #233 Open / Backlog.

---

# Phase 12 — GitHub lifecycle completion

Only after all gates pass:

1. identify exactly one existing Project #2 item for #233;
2. verify its current governed Release/Priority/Area metadata;
3. resolve the Status field and Done option authoritatively;
4. set that exact item Status → Done;
5. read back Done;
6. close #233;
7. read back Closed;
8. preserve all other metadata.

Use the robust item-identification discipline established during WP07. Do not pass login strings into numeric GraphQL variables.

Do not create/delete Project items.

Do not mutate the next work package.

---

# Phase 13 — Milestone / next-work read-back

Read:

- #233 final state;
- next canonical issue state;
- Project #2 states;
- milestone #58 counts.

Do not close milestone unless #233 is actually the final Release 1.9 work package and accepted release governance independently requires closure. If later WPs remain, milestone remains Open.

State the next eligible work package exactly from canonical planning.

---

# Stop conditions

Stop before mutation if:

- #233 semantics are materially undefined;
- lifecycle/demonstration timing/readiness/shutdown/residue choices are required but undefined;
- required paths are not authorized;
- required test/evidence ownership is missing;
- current dirty state creates new ambiguity;
- predecessor regression fails;
- implementation would cross into WP09;
- new packages/schema/persistence/transport redesign are required.

If implementation has begun and a later acceptance gate fails, preserve valid authorized partial work, leave #233 Open / Backlog, and report the exact blocker.

---

# Required blocked report

When blocked, include:

- exact accepted predecessor state;
- exact #233 requirement that cannot be executed;
- missing semantic/path authority;
- mutations made (normally zero if blocked before mutation);
- #233 Open / Backlog;
- next minimum authority required.

Terminal marker:

`RELEASE 1.9 WP08 BLOCKED`

---

# Required completion report

On success include:

## Entry state
#232 Done/Closed, #233 initial state, repository baseline.

## WP08 implementation
Exact paths/symbols and why each is authorized.

## Lifecycle/demonstration
Exact bounded protocol and observed evidence.

## Residue
Worker/Streamlit/temp/canonical-file final state.

## Validation
WP08 focused evidence, all Python predecessor suites, .NET suites, build, full regression.

## Scope audit
All forbidden categories zero.

## GitHub read-back
#233 Closed / Done, governed metadata preserved.

## Milestone
Current raw and canonical counts; milestone state.

## Next eligible work
Exact canonical next WP.

Terminal marker:

`RELEASE 1.9 WP08 CONSOLIDATED IMPLEMENTATION AND COMPLETION COMPLETE`

Do not emit COMPLETE unless #233 is Closed / Done and every implementation, demonstration, residue, regression, scope, and lifecycle gate passes.

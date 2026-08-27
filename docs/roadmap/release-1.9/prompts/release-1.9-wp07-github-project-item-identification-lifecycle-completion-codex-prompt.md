# Release 1.9 — WP07 GitHub Project-Item Identification + Lifecycle Completion Authority

## Authority

Use **GPT-5.6 Luna**.

This is a **narrow GitHub-only identification and lifecycle-completion authority** for Release 1.9 WP07, canonical issue **#232**.

Repository implementation and acceptance are already complete.

This authority must make **zero repository/source/test/document/package mutations**.

Its only purpose is to:

1. identify the existing GitHub Project #2 item corresponding to issue #232 without ambiguity;
2. verify its current governed metadata;
3. transition that exact Project item to **Done**;
4. close issue #232;
5. authoritatively read back the final lifecycle state;
6. leave #233 Open / Backlog and untouched.

If the Project item cannot be unambiguously identified, make **zero GitHub mutations**.

---

# Binding Predecessor Evidence

Treat the immediately preceding WP07 consolidated implementation result as binding repository evidence.

All repository acceptance checks passed:

- WP07 presentation tests: **2/2**;
- WP07 semantic-exposure tests: **2/2**;
- WP05 Python: **3/3**;
- WP06 Python: **6/6**;
- build: **0 warnings / 0 errors**;
- full .NET: **309/309**;
- Streamlit: **1.61.1**;
- `pip check`: clean.

The only failed gate was GitHub Project item identification.

No GitHub mutation occurred in that blocked pass.

Do not rerun implementation.
Do not modify code.
Do not create new tests.

---

# Proven Lifecycle Entry State

Expected from the blocked pass:

- #232: Open / Backlog.
- #233: Open / Backlog.
- Project #2 contains the existing item for #232, but its item ID was not authoritatively resolved.
- The attempted read-only GraphQL lookup failed twice because GitHub CLI parsed owner/login `samuel-santos-engineer` as a malformed numeric value.
- No partial close/Done mutation occurred.

Verify current state before mutation.

---

# Objective

Complete WP07 lifecycle atomically at the governance level:

- identify exactly one existing Project #2 item whose content is issue #232;
- verify it is the canonical WP07 item;
- preserve accepted metadata;
- set its Project Status to **Done**;
- close #232;
- read back both transitions;
- leave #233 Open / Backlog;
- leave WP08 unstarted.

Do not create a duplicate Project item.

---

# Absolute Repository Mutation Prohibition

Do not:

- edit any repository file;
- stage;
- commit;
- reset;
- restore;
- checkout;
- stash;
- clean;
- create documentation;
- run formatters;
- update packages;
- change tests;
- alter generated files.

Repository mutations under this authority must be:

`ZERO`

Read-only repository inspection is allowed only if needed to confirm issue/repository identity.

---

# Phase 0 — GitHub Authentication / Repository Identity

Read-only verify:

- authenticated GitHub account;
- repository owner/name;
- issue #232 URL/title/state;
- issue #233 URL/title/state;
- Project #2 identity/title/owner.

Do not assume that a human login string is a numeric GraphQL ID.

Record the exact repository and Project identities.

---

# Phase 1 — Avoid the Known GraphQL Failure Mode

Do **not** repeat the malformed query pattern that passes:

`samuel-santos-engineer`

into a GraphQL variable declared as an integer/numeric ID.

Before issuing a GraphQL call:

1. inspect the query variable declarations;
2. distinguish:
   - login/string;
   - databaseId/int;
   - node ID/ID!;
   - project number/int;
3. bind each value to the correct GraphQL type.

Prefer stable node IDs for mutation once discovered.

Do not guess node IDs.

---

# Phase 2 — Preferred Read-Only Identification Strategy

Use the safest available GitHub CLI/API route.

Preferred order:

## Strategy A — Project item listing/filtering

Read Project #2 items with sufficient pagination and inspect item content.

Identify entries whose content is issue #232 by authoritative content fields such as:

- repository;
- issue number;
- issue URL;
- content node ID.

Do not identify solely by title text.

## Strategy B — Issue → project items

If supported reliably, read issue #232's project-item/project membership connection and select Project #2.

## Strategy C — Correctly typed GraphQL

Use GraphQL only with verified variable types.

Resolve:

- owner node if needed;
- ProjectV2 node;
- issue #232 node;
- ProjectV2Item node.

Do not pass owner login where a numeric database ID is expected.

---

# Phase 3 — Pagination Requirement

Project item lookup must be exhaustive enough to avoid a false “not found”.

If the API/CLI paginates:

- inspect all pages required to locate #232;
- record page/item count or pagination evidence;
- do not assume the first page contains the issue.

Do not mutate while pagination/identification remains incomplete.

---

# Phase 4 — Identity Proof

Before any mutation, establish all of the following for exactly one item:

- Project: #2;
- content repository: canonical AIQuantTradingResearch repository;
- content issue number: #232;
- issue content node ID matches #232;
- Project item node ID is known;
- current Project status is known;
- current Release value is known;
- current Priority value is known;
- current Category/Architecture value is known if governed;
- no second active Project #2 item also represents #232.

Produce an internal identity tuple equivalent to:

`Project #2 + ProjectV2Item node ID + repository + issue #232 node ID`

If zero matches: BLOCK.

If more than one plausible match: BLOCK.

Do not deduplicate or delete items under this authority.

---

# Phase 5 — Verify Status Field and Done Option

Read Project #2 field definitions.

Resolve exactly:

- Status field node ID;
- `Done` option ID.

Do not infer option IDs from display order.

Verify the existing item is not already Done.

If already Done while #232 is still open, classify this as a recoverable partial lifecycle state and proceed only with the missing issue close after all identity checks pass.

---

# Phase 6 — Preserve Governed Metadata

Before mutation, read and record the #232 Project item's governed metadata.

Expected accepted values from Release 1.9 governance should be verified from actual Project state, not invented.

At minimum preserve:

- Release;
- Priority;
- Category/Architecture classification where present;
- any other field the existing lifecycle convention requires to remain unchanged.

This authority may change **Status only**.

If required metadata is missing or conflicts with accepted WP07 planning state, stop rather than repairing unrelated fields.

---

# Phase 7 — Pre-Mutation Gate

Mutation is authorized only if all are true:

1. exactly one #232 Project #2 item identified;
2. item content authoritatively matches #232;
3. Status field ID known;
4. Done option ID known;
5. governed metadata verified;
6. #232 currently Open unless already partially closed;
7. #233 verified Open / Backlog;
8. repository acceptance evidence remains binding;
9. no prior mutation in this authority created ambiguity.

If any fail:

`GITHUB MUTATIONS: ZERO`

and block.

---

# Phase 8 — Lifecycle Mutation Order

Use a controlled sequence that minimizes ambiguous partial state.

Preferred:

1. set the exact #232 Project #2 item Status → **Done**;
2. immediately read back Project item Status;
3. only after Done is confirmed, close issue #232;
4. immediately read back issue #232 state.

Reason: the prior blocker specifically prevented closing the issue without authoritative Project transition.

If Project status mutation fails, do not close #232.

If Project status succeeds but issue close fails, report the exact partial lifecycle state. Do not revert Done automatically unless an accepted governance rule explicitly requires rollback.

---

# Phase 9 — Project Status Mutation

Mutate only:

- Project #2;
- exact identified #232 item;
- Status field;
- Done option.

Do not:

- create a new item;
- delete an item;
- alter Release;
- alter Priority;
- alter Category;
- alter #233;
- alter milestone;
- reorder Project items.

Use node IDs and option IDs obtained read-only in this run.

---

# Phase 10 — Immediate Project Read-Back

After mutation, authoritatively verify:

- same Project item node ID;
- same issue #232 content;
- Status = Done;
- Release unchanged;
- Priority unchanged;
- Category unchanged.

If Status is not Done, do not close #232.

---

# Phase 11 — Close Issue #232

After confirmed Project Done:

- close issue #232 using the repository's normal lifecycle method;
- add a concise completion comment only if the accepted project convention requires one.

If a comment is optional, prefer no extra mutation.

Do not edit issue title/body/labels/milestone unless separately required by established lifecycle convention.

---

# Phase 12 — Immediate Issue Read-Back

Verify:

- #232 state = Closed;
- issue identity remains canonical;
- milestone assignment unchanged;
- no accidental issue metadata mutation.

---

# Phase 13 — #233 Guard

Read back #233 and prove:

- state = Open;
- Project status = Backlog;
- no field changed;
- no comment/lifecycle mutation was made;
- WP08 remains unstarted.

If #233 changed unexpectedly, report it; do not “fix” it unless this authority caused the mutation and a safe direct rollback is clearly governed.

---

# Phase 14 — Milestone / Canonical Count Read-Back

Read milestone #58 after completion.

Report:

- milestone remains Open;
- current raw open/closed counts;
- canonical Release 1.9 open/closed counts if historical duplicate #225 still affects raw counts;
- #233 and later work packages remain open.

Do not close milestone #58 merely because WP07 completed.

No milestone mutation is authorized.

---

# Phase 15 — Final Project Read-Back

Perform a final read-only verification of Project #2:

For #232:

- exact item ID;
- Status = Done;
- accepted Priority unchanged;
- Release = 1.9 or exact governed value unchanged;
- Category/Architecture unchanged.

For #233:

- Status = Backlog;
- existing metadata unchanged.

This final read-back is mandatory before success.

---

# Phase 16 — Mutation Audit

List every GitHub mutation actually made.

Success should contain only:

1. #232 Project item Status → Done;
2. #232 issue → Closed;
3. optional completion comment only if required by established governance.

Everything else must be zero.

Explicitly report:

- repository mutations: zero;
- Project item creation: zero;
- Project item deletion: zero;
- #233 mutations: zero;
- milestone mutations: zero;
- metadata changes other than #232 Status: zero.

---

# Recovery / Partial-State Rules

## If identification fails before mutation

Make zero mutations and BLOCK.

## If Project Done mutation fails

Do not close #232. BLOCK.

## If Project Done succeeds but read-back fails

Do not close #232 until authoritative confirmation is obtained. BLOCK with exact known state.

## If Project Done is confirmed but issue close fails

Report:

- #232 Project = Done;
- #232 issue = Open;
- exact close failure.

Do not make unrelated compensating changes.

## If #232 was already Closed when authority begins

Identify the Project item first.

If Project is Backlog and issue Closed, this is a pre-existing partial lifecycle state. This authority may complete only the missing Project Status → Done after all identity/metadata checks.

## If #232 Project is already Done and issue Open

After identity/metadata verification, close the issue.

## If both already complete

Make zero mutations and perform final read-back; success may be reported as already satisfied.

---

# Non-Goals

This authority does not authorize:

- repository implementation;
- tests;
- docs;
- commits;
- releases;
- tags;
- milestone closure;
- WP08 work;
- WP09 work;
- #233 mutation;
- Project item creation/deletion;
- metadata repair;
- duplicate cleanup;
- issue relabeling;
- issue body edits.

---

# Required Completion Report

## Repository acceptance evidence

Restate the binding passed evidence without rerunning implementation.

## GitHub identity

Report:

- repository;
- Project #2 identity;
- #232 issue node/URL identity;
- #232 Project item node ID;
- Status field ID;
- Done option ID.

Do not expose credentials/tokens.

## Identification method

State which read-only strategy succeeded and how pagination/identity was proven.

## Pre-mutation metadata

Report Status, Release, Priority, Category for #232.

## Mutations

Exact GitHub mutations performed.

## Read-back

Report:

- #232 Closed / Done;
- governed metadata unchanged;
- #233 Open / Backlog;
- milestone #58 Open;
- raw/canonical milestone counts.

## Mutation audit

State:

`WP07 LIFECYCLE REPOSITORY MUTATIONS: ZERO`

and, on normal successful transition:

`WP07 LIFECYCLE GITHUB MUTATIONS: #232 PROJECT STATUS → DONE; #232 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

If already complete and no mutation was needed, report that accurately instead.

## Next eligible work

On success:

`NEXT ELIGIBLE WORK PACKAGE: WP08 — #233`

---

# Success Gate

Success requires authoritative read-back proving:

- exactly one canonical #232 Project #2 item;
- Project Status = Done;
- #232 = Closed;
- accepted Project metadata unchanged;
- #233 = Open / Backlog;
- WP08 unstarted;
- milestone remains correctly open;
- repository mutations = zero.

---

# Terminal Markers

Success:

`RELEASE 1.9 WP07 GITHUB PROJECT-ITEM IDENTIFICATION AND LIFECYCLE COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP07 GITHUB PROJECT-ITEM IDENTIFICATION AND LIFECYCLE COMPLETION BLOCKED`

Do not emit COMPLETE if the #232 Project item identity or final lifecycle read-back is ambiguous.

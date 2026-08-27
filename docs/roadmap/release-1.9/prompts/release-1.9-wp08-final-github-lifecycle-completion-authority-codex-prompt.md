# Release 1.9 — WP08 Final GitHub Lifecycle Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is a **very narrow GitHub lifecycle-only authority** for Release 1.9 WP08, canonical issue **#233**.

WP08 technical acceptance is complete and frozen.

This authority performs **no implementation, documentation, test, package, schema, signing, environment, or repository mutation**.

Its sole purpose is to:
1. identify the exact existing GitHub Project #2 item for canonical issue #233;
2. verify its current metadata;
3. set only its Project Status to `Done`;
4. close only issue #233;
5. read back and prove final lifecycle state;
6. verify #234 and WP09 remain untouched;
7. verify milestone #58 remains consistent.

No WP09 work is authorized.

---

# Frozen technical acceptance

Treat as binding predecessor evidence. Do not rerun tests.

## Focused WP08
- root cause: `RR-HANDOFF`;
- shared-runtime readiness fix accepted;
- canonical R1: 3/3 passed;
- RF-HANDOFF passed;
- focused WP08: 18/18 passed;
- build: 0 warnings / 0 errors.

## .NET
- Infrastructure: 178/178 passed;
- Domain: 11/11 passed;
- Application: 125/125 passed;
- Architecture: 13/13 passed;
- aggregate: 327/327, 0 failures;
- runner classification: `IR-OUTPUT`;
- authoritative standalone Infrastructure TRX: Completed, 178 executed / 178 passed.

## Python
- WP05: 3/3;
- WP06: 6/6;
- WP07 semantic: 2/2;
- WP07 presentation: 2/2;
- Streamlit: 1.61.1;
- `pip check`: clean.

## Residue/environment
- no owned Worker;
- no owned testhost;
- no owned Python;
- no owned Streamlit;
- verified stale harness-owned temp roots cleaned;
- no related Code Integrity 3077 event in reported window.

## Repository
- repository mutations from regression-evidence authority: zero;
- GitHub mutations from regression-evidence authority: zero;
- `main` = `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- ahead/behind 0/0.

## Lifecycle before this authority
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- WP09 unstarted.

---

# Hard scope boundary

Allowed GitHub mutations:

1. exact Project #2 item for #233:
   - `Status` → `Done`;
2. exact canonical issue #233:
   - Open → Closed.

All other GitHub mutations: ZERO.

Forbidden:
- Project item creation;
- Project item deletion;
- Project item duplication;
- Release change;
- Priority change;
- Area change;
- milestone mutation;
- issue body/title/labels/assignees change;
- #234 mutation;
- WP09 mutation;
- repository mutation;
- test rerun.

---

# Phase 0 — Repository read-only sanity

Read-only verify:
- local HEAD remains expected;
- local `main` and `origin/main` remain 0/0;
- no mutation is required for lifecycle completion.

Do not modify repository even if unrelated pre-existing worktree changes exist.

---

# Phase 1 — Canonical issue verification

Read #233.

Require:
- issue number exactly `233`;
- issue remains Open before mutation;
- milestone exactly Release 1.9 milestone #58;
- issue identity/title matches canonical WP08;
- no ambiguity with similarly named issues.

Read #234 and record:
- Open;
- Backlog;
- untouched successor WP09.

If #233 is already Closed, do not blindly mutate. Inspect Project state and report idempotent lifecycle state.

---

# Phase 2 — Exhaustive Project #2 item identification

Identify Project #2 and exhaustively locate items linked to issue #233.

Require:
- exactly one Project item linked to canonical repository issue #233.

Do not identify only by title text if linked issue identity is available.

Record:
- Project node ID;
- item node ID;
- linked issue number/repository;
- current Status;
- current Release;
- current Priority;
- current Area.

Expected before mutation:
- Status: Backlog;
- Release: 1.9;
- Priority: P1;
- Area: Infrastructure.

If zero or more than one canonical linked item exists:
- BLOCK;
- GitHub mutations zero.

Do not create/delete/deduplicate items.

---

# Phase 3 — Field/option identity proof

Resolve Project #2 field IDs and option IDs for:

- Status field;
- `Done` option.

Also resolve/read:
- Release field/value;
- Priority field/value;
- Area field/value.

Do not mutate Release/Priority/Area.

Before mutation record exact raw IDs needed for the Status update.

If field identity is ambiguous:
- BLOCK.

---

# Phase 4 — Pre-mutation lifecycle snapshot

Produce:

| Entity | State |
|---|---|
| #233 issue | Open |
| #233 Project item | Backlog |
| Release | 1.9 |
| Priority | P1 |
| Area | Infrastructure |
| #234 issue | Open |
| #234 Project item | Backlog |
| Milestone #58 | Open |

Also record milestone #58 open/closed issue counts from authoritative GitHub data.

---

# Phase 5 — Single Project mutation

Mutate only the identified #233 Project item:

`Status: Backlog → Done`

No other field update.

Immediately read back the same item.

Require:
- Status = Done;
- Release still 1.9;
- Priority still P1;
- Area still Infrastructure;
- linked issue still #233.

If read-back fails:
- STOP;
- do not close #233.

---

# Phase 6 — Single issue mutation

Only after Project Done read-back succeeds:

Close canonical issue #233.

Do not add comments unless closing mechanism requires none; prefer direct close only.

Immediately read back #233.

Require:
- state = Closed;
- milestone remains #58;
- metadata otherwise unchanged.

If issue was already Closed by the time this phase runs, treat close as idempotent only after verifying identity and Project Done.

---

# Phase 7 — Successor protection

Read #234 after #233 closure.

Require:
- #234 = Open;
- Project Status = Backlog;
- no metadata mutation;
- no WP09 implementation started by this authority.

No mutation permitted even if #234 metadata appears imperfect; report only.

---

# Phase 8 — Milestone read-back

Read milestone #58 after #233 closure.

Require:
- milestone remains Open unless pre-existing canonical lifecycle rules independently say otherwise;
- no milestone mutation was made;
- closed/open issue counts reflect #233 closure exactly.

Report:
- milestone state;
- open count;
- closed count.

Do not close milestone.

---

# Phase 9 — Project item cardinality read-back

Re-query Project #2.

Require:
- exactly one item linked to #233;
- item node ID unchanged;
- no item creation/deletion;
- Status Done;
- Release 1.9;
- Priority P1;
- Area Infrastructure.

Also verify #234 item remains unchanged.

---

# Phase 10 — Repository mutation proof

Verify lifecycle operations caused:
- zero repository file changes;
- zero commits;
- zero pushes.

Do not attempt to clean pre-existing worktree changes.

---

# Completion gate

WP08 lifecycle completion succeeds only if all are true:

1. exactly one canonical Project #2 item for #233;
2. #233 item Status = Done;
3. Release = 1.9 unchanged;
4. Priority = P1 unchanged;
5. Area = Infrastructure unchanged;
6. #233 issue = Closed;
7. #233 milestone remains #58;
8. #234 = Open / Backlog;
9. milestone #58 state/counts read back consistently;
10. Project item creation/deletion = zero;
11. repository mutations = zero;
12. WP09 work = zero.

---

# Required completion report

## Technical acceptance
State that technical acceptance was inherited/frozen and not rerun.

## Project identity
- Project #2;
- exact #233 item node ID;
- canonical linked issue proof.

## Pre-state
- #233 Open / Backlog;
- Release/Priority/Area;
- #234 Open / Backlog;
- milestone #58 counts.

## Mutations
Exactly:
- #233 Project Status → Done;
- #233 issue → Closed.

## Read-back
- #233 Closed / Done;
- Release 1.9;
- Priority P1;
- Area Infrastructure;
- #234 Open / Backlog;
- milestone #58 state/counts.

## Cardinality
Exactly one #233 Project item; no item creation/deletion.

## Repository
Zero mutation.

## Next eligible work package
Report:

`NEXT ELIGIBLE WORK PACKAGE: WP09 — #234`

Do not start it.

---

# Exact mutation statements

`WP08 LIFECYCLE REPOSITORY MUTATIONS: ZERO`

`WP08 LIFECYCLE GITHUB MUTATIONS: #233 PROJECT STATUS → DONE; #233 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

---

# Stop conditions

STOP before mutation if:
- #233 identity is ambiguous;
- Project #2 identity is ambiguous;
- zero/multiple canonical #233 items exist;
- Status/Done field identity is ambiguous;
- Release/Priority/Area cannot be safely read;
- #233 is linked to an unexpected milestone and canonical evidence cannot resolve it.

STOP after Project mutation but before issue closure if:
- Done read-back fails;
- Release/Priority/Area changed unexpectedly;
- linked issue identity changed.

Do not repair unrelated GitHub metadata under this authority.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 GITHUB PROJECT-ITEM IDENTIFICATION AND LIFECYCLE COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 FINAL GITHUB LIFECYCLE COMPLETION BLOCKED`

Do not emit COMPLETE unless #233 is authoritatively Closed / Done and every preservation/read-back condition passes.

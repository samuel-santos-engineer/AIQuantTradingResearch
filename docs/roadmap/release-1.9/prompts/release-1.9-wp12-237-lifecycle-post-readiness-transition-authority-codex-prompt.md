# Release 1.9 — WP12 #237 Lifecycle / Post-Readiness Transition Authority

## Model
Use **GPT-5.6 Terra**.

## Purpose
This is a very narrow GitHub lifecycle authority for WP12 / #237 after Role-A PR readiness has passed.

Inherited and frozen:
- WP12 PR READINESS: PASS.
- Exact hypothetical R1 set: 286 non-ignored paths; R5 none.
- Build 0 warnings / 0 errors.
- .NET 339/339; Python 17/17.
- Streamlit 1.61.1; `pip check` clean.
- WP08/WP09 focused preservation 22/22; architecture 8/8.
- SQLite schema v4 preserved.
- Git-aware Gitleaks clean; residue zero.
- Git `main == origin/main == 3a02f035...`, ahead/behind 0/0, staged 0.
- #233–#236 Closed/Done.
- #237 Open/Backlog with exactly one Project #2 item `PVTI_lAHOCAzBgs4BfsiAzg33jmA`, Release 1.9 / P1 / Engineering.
- milestone #58 Open, 1 open / 12 closed.

Do not reopen technical readiness unless current state contradicts it.

## Binding sources
Read completely:
1. `docs/roadmap/release-1.9/RELEASE_1.9_WP12_CLOSURE_PR_READINESS_GIT_GITHUB_LIFECYCLE_CONTRACT_AUTHORITY.md`
2. #237 and its Project #2 item.
3. Release 1.9 definition, execution plan, and file manifest.
4. completed WP12 PR-readiness evidence if stored.
5. any canonical Release 1.9 source explicitly defining WP12/#237 completion semantics.

Historical conventions are evidence only, not authority.

## Decision before mutation
Classify the canonical completion event exactly:

- `L1 — READINESS COMPLETION`: #237 completes when Role-A PR readiness is proven.
- `L2 — PR CREATION REQUIRED`.
- `L3 — PR MERGE REQUIRED`.
- `L-UNRESOLVED`.

The WP12 contract deliberately deferred #237 timing. Do not infer L1 simply because readiness passed.

## Allowed mutation boundary
Only if canonical evidence proves L1, authorize exactly:
1. #237 Project #2 Status `Backlog → Done`.
2. Read back the same unique item and verify Done, Release 1.9, P1, Engineering.
3. Close #237.
4. If Project automation already closed it, treat explicit close as idempotent.
5. Read back #237 Closed / Done.
6. Read back milestone #58 counts.

All other mutations are forbidden.

If classification is L2, L3, or L-UNRESOLVED: perform zero mutation and report the minimum next authority.

## Explicitly forbidden
Regardless of classification:
- repository edits;
- staging/commit/amend;
- branch creation/switch;
- push;
- PR creation/update/merge;
- milestone #58 closure;
- tag;
- GitHub Release;
- Project item creation/deletion;
- Release/Priority/Area changes;
- changes to #233–#236;
- WP13+ work.

Closing #237 is never implicit authority to close milestone #58.

## Pre-mutation read-back for L1
Verify:
- #237 Open;
- exactly one Project #2 item with ID `PVTI_lAHOCAzBgs4BfsiAzg33jmA`;
- Backlog / Release 1.9 / P1 / Engineering;
- #233–#236 remain Closed/Done;
- milestone #58 Open;
- Git staged count 0;
- inherited report contains `WP12 PR READINESS: PASS` and zero repository/Git/GitHub mutation.

If any material identity/state differs: STOP.

Do not rerun the full technical matrix solely for lifecycle completion.

## Milestone boundary
After #237 closes, milestone counts may naturally become 0 open / 13 closed. That is an effect of closing #237, not authority to close the milestone.

Milestone #58 MUST remain Open.

If automation unexpectedly closes the milestone, report it and do not make broader corrective mutations without separate authority.

## Expected L1 final state
- #233–#237 Closed.
- #237 Project item Done; ID unchanged.
- Release 1.9 / P1 / Engineering preserved.
- milestone #58 Open, expected 0 open / 13 closed.
- no PR created.
- repository/Git mutations zero.
- no tag/GitHub Release.

## Required L1 report
State:
`WP12 #237 LIFECYCLE CLASSIFICATION: L1 — READINESS COMPLETION`

Report inherited readiness, pre-state, exact mutations, and final read-back.

Required markers:

`WP12 #237 LIFECYCLE REPOSITORY MUTATIONS: ZERO`

`WP12 #237 LIFECYCLE GIT MUTATIONS: ZERO`

`WP12 #237 LIFECYCLE GITHUB MUTATIONS: #237 PROJECT STATUS → DONE; #237 ISSUE → CLOSED; ALL OTHER GITHUB MUTATIONS ZERO`

Then:

`RELEASE 1.9 WORK PACKAGES COMPLETE — SEPARATE RELEASE FINALIZATION / PR-GIT LIFECYCLE AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 WP12 #237 LIFECYCLE / POST-READINESS TRANSITION COMPLETE`

## Required blocked report
For L2:
`WP12 #237 LIFECYCLE CLASSIFICATION: L2 — PR CREATION REQUIRED`

For L3:
`WP12 #237 LIFECYCLE CLASSIFICATION: L3 — PR MERGE REQUIRED`

Otherwise:
`WP12 #237 LIFECYCLE CLASSIFICATION: L-UNRESOLVED`

Provide canonical evidence, exact missing authority, and:

`WP12 #237 LIFECYCLE REPOSITORY MUTATIONS: ZERO`
`WP12 #237 LIFECYCLE GIT MUTATIONS: ZERO`
`WP12 #237 LIFECYCLE GITHUB MUTATIONS: ZERO`

Terminal:

`RELEASE 1.9 WP12 #237 LIFECYCLE / POST-READINESS TRANSITION BLOCKED`

Never manufacture L1 authority from #237 being the last open milestone issue.

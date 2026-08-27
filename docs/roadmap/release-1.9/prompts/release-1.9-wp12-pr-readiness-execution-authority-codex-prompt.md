# Release 1.9 — WP12 PR-Readiness Execution Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
Execute the binding WP12 PR-readiness contract:

`docs/roadmap/release-1.9/RELEASE_1.9_WP12_CLOSURE_PR_READINESS_GIT_GITHUB_LIFECYCLE_CONTRACT_AUTHORITY.md`

WP12 role is fixed:

`A — PR-READY-ONLY`

This authority proves Release 1.9 PR readiness. It does **not** create Git or GitHub lifecycle mutations.

---

# Hard mutation boundary

## Repository
No implementation/documentation edits are authorized.

## Git
Forbidden:
- staging;
- unstaging except if needed to reverse an accidental mutation made by this pass;
- commit;
- amend;
- branch create/delete/switch for workflow purposes;
- rebase;
- squash;
- cherry-pick;
- merge;
- push;
- force-push;
- reset;
- stash;
- clean.

## GitHub
Forbidden:
- PR create/update/merge;
- issue edit/close;
- Project field mutation;
- milestone mutation;
- tag;
- GitHub Release;
- label/reviewer mutation.

#237 remains Open / Backlog.
Milestone #58 remains Open.

---

# Accepted predecessor boundary

Unless current read-back disproves it:

## Git
- branch `main`;
- `main == origin/main`;
- predecessor HEAD `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`;
- ahead/behind 0/0;
- staged paths 0.

The Luna contract observed:
- 269 dirty entries;
- 29 tracked modifications;
- 240 untracked paths.

Do not fail merely because counts differ from an earlier 25/244 snapshot. Explain any current delta factually.

## GitHub
- #233–#236 Closed / Done;
- #237 Open / Backlog;
- unique #237 Project item:
  `PVTI_lAHOCAzBgs4BfsiAzg33jmA`;
- Release 1.9 / P1 / Engineering;
- milestone #58 Open;
- latest accepted count 1 open / 12 closed.

## Technical
WP11 accepted:
- build 0 warnings / 0 errors;
- .NET 339/339;
- Python 17/17;
- Streamlit 1.61.1;
- `pip check` clean;
- SQLite persistence schema v4;
- security/docs/residue accepted.

---

# Phase 1 — Read binding contract completely

Read the WP12 contract completely before evaluating readiness.

Extract verbatim/precisely:
- R1 include set;
- R2 unrelated exclusions;
- R3 local-only exclusions;
- R4 generated/runtime exclusions;
- R5 ambiguous/mixed paths;
- mixed-file policy;
- security gate;
- technical gate;
- residue gate;
- documentation/readiness gate;
- expected final Git state;
- expected final GitHub state.

Do not re-author the contract.

If the contract lacks an exact executable rule required for readiness:
STOP before any mutation and name the missing authority.

---

# Phase 2 — Current Git safety snapshot

Record read-only:

- current branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged path count/list;
- tracked modifications;
- deletions;
- untracked paths;
- ignored relevant paths.

Require:
- staged paths remain 0 unless they were already staged before this pass; if unexpectedly nonzero, STOP and preserve them;
- no automatic cleanup.

Compare current inventory to the contract snapshot.

Classify changes since contract creation as:
- expected contract artifact addition;
- accepted predecessor work;
- unrelated user work;
- generated evidence;
- unexplained.

Any unexplained mutation affecting R1/R5 classification blocks readiness.

---

# Phase 3 — R1–R5 inventory verification

Re-verify every contract-classified path.

## R1
Every intended Release 1.9 path must:
- exist in expected state;
- have provenance to accepted WP work;
- be permitted by the binding contract;
- contain no unrelated/local-only material.

## R2
Unrelated user work:
- preserved;
- excluded.

## R3
Local-only:
- preserved locally;
- excluded;
- ignored where contract requires it.

Explicitly verify:
`Directory.Build.local.props`

is not tracked/staged/intended for PR.

## R4
Generated/runtime/test evidence:
- excluded from PR;
- ignored or otherwise outside the intended include set;
- cleaned only when factually harness-owned and contract permits cleanup.

Because repository mutation is forbidden by this execution authority, if cleanup would itself modify repository-visible state, report the residue and BLOCK rather than deleting it unless the binding contract explicitly classifies cleanup as non-repository safe evidence handling.

## R5
Every ambiguous/mixed path must satisfy the contract's policy.

If any R5 path still requires hunk separation, editing, or judgment not already resolved by the contract:
BLOCK PR readiness.

Produce final table:

`path/pattern → R-class → include/exclude → current verification → PASS/BLOCK`

Every R1 path must be exact.
Grouped R4 generated descendants are allowed only as the contract permits.

---

# Phase 4 — PR inclusion simulation

Without staging, compute the exact hypothetical PR include set from R1.

Perform a read-only diff/inventory equivalent to:

`what would be staged if only the exact R1 manifest were selected?`

Verify:
- no R2/R3/R4/R5 path enters the hypothetical set;
- no private/local signing material;
- no build output;
- no test result/runtime artifact;
- no unrelated user work.

Record:
- exact intended tracked modifications;
- exact intended new files;
- exact intended deletions, if any;
- total intended path count.

Do NOT run `git add`.

---

# Phase 5 — Diff audit

Read the full diff/content for every R1 path.

Verify:
- changes match accepted Release 1.9 work;
- no debug instrumentation accidentally retained unless explicitly accepted;
- no temporary diagnostic behavior outside accepted permanent tests;
- no machine-specific paths/secrets;
- no accidental generated content;
- no unexplained scope outside WP01–WP12 accepted work;
- documentation claims match accepted implementation.

For large new documentation/prompt trees, inspect enough to establish provenance and absence of generated/private content as required by the contract.

Any material mismatch:
BLOCK.

---

# Phase 6 — Security preflight

Run exactly the existing approved security process named by the contract.

At minimum verify:

- tracked/non-ignored intended content secret scan passes;
- no private keys;
- no certificate exports containing private keys;
- no credentials/tokens;
- no local signing configuration in R1;
- no signed build binaries in R1;
- `Directory.Build.local.props` excluded.

If the contract uses Gitleaks, use the existing approved installation/configuration only.
Do not install/upgrade security tooling.

Record exact command/tool/version if available and result.

Any finding:
BLOCK.

---

# Phase 7 — Signing hygiene audit

Read-only verify the accepted local Smart App Control development-signing boundary:

- opt-in;
- development-only;
- local configuration excluded;
- no private key committed;
- committed documentation/script/project behavior matches accepted WP10/predecessor state;
- terminology does not misrepresent the mechanism as a production trust solution.

Do not change signing files.

---

# Phase 8 — Technical readiness

Execute exactly the technical readiness required by the binding contract.

Unless the contract explicitly allows inheritance without rerun, use:

## Build
Require:
- 0 warnings
- 0 errors.

## .NET
Require:
- Domain 11/11
- Application 125/125
- Infrastructure 182/182
- Architecture 21/21
- total 339/339
- failures 0
- unexplained skips 0.

## Python
Require:
- 17/17
- failures 0
- unexplained skips 0.

## Environment
Require:
- Streamlit 1.61.1
- `pip check` clean.

## Schema
Read-only confirm:
- canonical SQLite persistence schema v4;
- no R1 diff changes the accepted schema version/migration contract unexpectedly.

No test-count delta.

If Windows Application Control interferes, use only the already-established local development configuration; do not weaken system policy under this authority.

---

# Phase 9 — Focused predecessor preservation

If the binding contract requires focused proof, execute exactly those gates.

At minimum, if required:
- WP08 lifecycle 18/18;
- WP09 permanent integration Ready/WarmUp/Empty/Failed 4/4;
- WP09 architecture/no-bypass 8/8;
- schema-v4 focused proof.

Do not invent additional tests.

---

# Phase 10 — Documentation/readiness audit

Read-only verify all contract-required Release 1.9 documentation:

- README;
- interoperability boundary;
- Python developer environment;
- roadmap;
- Release 1.9 planning/authority artifacts required for PR provenance;
- simulated/replay warning;
- schema-v4 wording;
- security/lifecycle wording;
- branch/PR guidance;
- local signing guidance;
- relative links and documented commands where required.

No documentation edit.

A material stale/broken claim blocks PR readiness.

---

# Phase 11 — Residue audit

After tests, verify exact contract residue conditions.

Require zero factually owned residue for:
- Worker;
- testhost;
- Python;
- Streamlit;
- probe/helper processes;
- owned listeners;
- forbidden harness temp/runtime roots;
- atomic handoff siblings;
- forbidden test-owned SQLite/WAL/SHM/journal files.

Do not terminate unrelated processes.

Do not broadly clean `%TEMP%`.

If contract-authorized harness cleanup is required and can be done without repository/Git mutation, perform only factually owned cleanup. Record every deletion/termination as operational cleanup, not repository mutation.

If ownership is uncertain:
do not clean; BLOCK.

---

# Phase 12 — Final Git preservation audit

Re-read:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- staged files;
- dirty inventory.

Require:
- no staging by this pass;
- no commit;
- no branch/push;
- no R2/R3 changes caused by this pass;
- no implementation/doc mutation by this pass;
- expected test-generated artifacts either ignored/allowed or safely cleaned under residue rules.

Compare with entry snapshot.

Repository mutation attributable to WP12 PR-readiness execution must be ZERO.

---

# Phase 13 — Final GitHub preservation audit

Read back:

- #233;
- #234;
- #235;
- #236;
- #237;
- #237 Project item;
- milestone #58;
- relevant PR state if needed for evidence.

Require:
- #233–#236 unchanged Closed/Done;
- #237 remains Open/Backlog;
- same Project item ID;
- Release 1.9 / P1 / Engineering unchanged;
- milestone #58 remains Open;
- no PR created/updated/merged;
- no tag/Release mutation.

GitHub mutations must be ZERO.

---

# Phase 14 — PR-readiness package

If every gate passes, produce a final evidence package in the terminal report only; do not create a repository file unless the binding contract explicitly requires one.

Include:

## Exact hypothetical PR include set
Every R1 path.

## Exact exclusions
R2/R3/R4/R5.

## Proposed staging command plan
Informational only.
Use exact-path staging commands or equivalent based on the R1 manifest.
DO NOT EXECUTE THEM.

## Proposed branch
Only if the contract names one.
Otherwise state branch creation remains unauthorized.

## Proposed commit
Only if contract defines it.
Otherwise state commit remains unauthorized.

## Proposed PR
Only if contract provides title/body semantics.
Otherwise state PR creation remains unauthorized.

This package is evidence for the next separate authority.

---

# Phase 15 — Readiness decision

Declare exactly one:

## PR-READY
Only if:
- all R1 paths verified;
- all exclusions verified;
- no unresolved R5;
- security clean;
- technical gates pass;
- docs/readiness pass;
- residue pass;
- Git preservation pass;
- GitHub preservation pass.

## NOT-PR-READY
If any gate fails.

Do not “mostly pass.”

---

# Phase 16 — Lifecycle boundary

Even when PR-READY:

DO NOT:
- set #237 Done;
- close #237;
- close milestone #58;
- stage;
- commit;
- branch;
- push;
- create/update/merge PR;
- tag;
- publish Release.

The binding contract defers #237 lifecycle to a separate explicit authority.

Required next authority after PR-READY:

`NARROW WP12 #237 LIFECYCLE / POST-READINESS TRANSITION AUTHORITY`

That authority must decide whether #237 closes at readiness, PR creation, or another canonical event without reopening technical readiness.

---

# Stop conditions

STOP and report NOT-PR-READY if:

- R1 set no longer matches current worktree;
- an R1 path contains unrelated changes;
- unresolved R5 exists;
- local signing/private material could enter the PR;
- security scan fails;
- build is not 0/0;
- .NET is not exactly 339/339 where rerun required;
- Python is not exactly 17/17 where rerun required;
- schema v4 is not preserved;
- required focused suite fails;
- docs/readiness gate fails;
- forbidden residue remains;
- execution would require repository edit;
- execution would require staging/Git/GitHub mutation.

Do not fix under this authority.

---

# Required success report

## Binding contract
Exact path.

## Entry state
Git/GitHub.

## Inventory
Observed counts and explanation of any difference from 269 / 29 / 240.

## R1 include manifest
Exact paths.

## Exclusions
R2/R3/R4/R5.

## Mixed-file status
Exact.

## Hypothetical PR
Exact path count and scope.

## Security
Exact result.

## Signing hygiene
Exact result.

## Technical
- build 0/0
- .NET 339/339
- Python 17/17
- Streamlit 1.61.1
- pip check clean
- schema v4.

## Focused preservation
Exact required results.

## Docs/readiness
Exact result.

## Residue
Exact result.

## Git preservation
No staging/commit/branch/push; repository mutation zero.

## GitHub preservation
#237 Open/Backlog; milestone open; mutations zero.

## Decision
`WP12 PR READINESS: PASS`

## Mutation markers

`WP12 PR-READINESS REPOSITORY MUTATIONS: ZERO`

`WP12 PR-READINESS GIT MUTATIONS: ZERO`

`WP12 PR-READINESS GITHUB MUTATIONS: ZERO`

## Next authority
`WP12 PR READINESS PROVEN — NARROW #237 LIFECYCLE / POST-READINESS TRANSITION AUTHORITY REQUIRED`

# Terminal marker

`RELEASE 1.9 WP12 PR-READINESS EXECUTION COMPLETE`

---

# Required blocked report

Include:
- entry state;
- gates passed;
- exact blocker;
- affected R-class/path/test;
- minimum follow-up authority;
- zero mutation accounting.

Required markers:

`WP12 PR-READINESS REPOSITORY MUTATIONS: ZERO`

`WP12 PR-READINESS GIT MUTATIONS: ZERO`

`WP12 PR-READINESS GITHUB MUTATIONS: ZERO`

`WP12 PR READINESS: BLOCKED`

# Terminal marker

`RELEASE 1.9 WP12 PR-READINESS EXECUTION BLOCKED`

Never emit COMPLETE unless the binding role-A contract is satisfied in full with zero repository/Git/GitHub mutation.

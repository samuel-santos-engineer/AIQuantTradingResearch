# Release 1.10 — Publication Manifest Literal-List Repair & Resumption-Artifact Classification Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations, publication; NOT selected for this authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Purpose

Repair the Release 1.10 publication manifest so the publication candidate boundary is once again literal, count-consistent, path-only, and Terra-executable.

This authority reconciles exactly two blockers from the latest Terra publication resumption attempt:

1. The manifest claims a canonical **103-path literal list**, but the persisted list contains **110 entries**, where entries 104–110 are Git CRLF warning text rather than repository paths.
2. The current worktree is now **107 paths = 21 tracked + 86 untracked**, because two newly created Terra publication-resumption authority prompt files are not classified by the binding manifest.

This is a narrow planning/governance repair authority.

---

# Accepted entry state

Treat as accepted unless authoritative inspection directly contradicts it:

- publication branch is already safely re-anchored at:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- `origin/main` matches:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- no staging occurred;
- no commit occurred;
- no push occurred;
- no PR creation/update occurred;
- #242–#249 remain Closed/Done;
- milestone #59 remains Open, 0 open / 8 closed;
- current raw worktree inventory is:
  **107 paths = 21 tracked + 86 untracked**
- prior reconciled canonical publication candidate was intended as:
  **103 paths = 21 tracked + 82 untracked**
- the prior Git-publication authority pair was INCLUDED;
- the prior remote-base reconciliation authority pair was EXCLUDED;
- two newly created Terra resumption-authority prompt files remain unclassified.

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST REPAIR ENTRY: PASS`

---

# Mutation boundary

Allowed repository mutations:

- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md` only if required to persist corrected resumption semantics.

Avoid changing:
- `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`

Forbidden:
- production changes;
- test changes;
- WP07 documentation changes;
- authority prompt content changes outside planning classification;
- package/project/schema/signing changes;
- staging;
- commits;
- branch mutation;
- push;
- PR creation/update;
- issue/Project/milestone mutation;
- tag/version/GitHub Release mutation.

Git mutations: ZERO.
GitHub mutations: ZERO.

---

# Phase 1 — Authoritative inventory

Read and inspect:

1. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. prior Luna remote-base/publication-artifact reconciliation result.
4. prior Terra Git/PR publication authority.
5. Terra Git/PR publication resumption authority.
6. latest BLOCKED result.
7. `git status --short`
8. exact tracked changed paths.
9. exact untracked paths.
10. exact raw worktree path count.
11. exact manifest publication-list entries with ordinal numbers.
12. exact CRLF warning lines currently embedded as fake entries.
13. exact two unclassified resumption-authority prompt paths.
14. exact two already-excluded remote-base reconciliation authority paths.
15. exact two already-included Git-publication authority paths.

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST INVENTORY: COMPLETE`

---

# Phase 2 — Repair malformed literal list

Identify every non-path entry currently present in the manifest's canonical publication literal list.

Expected defect:

- entries 104–110 are Git CRLF warning text.

Do not assume this expectation if actual manifest differs; report actual ordinals/text.

Repair requirements:

- the canonical publication list must contain repository paths only;
- one path per entry;
- no command output;
- no warnings;
- no prose;
- no duplicated paths;
- no wildcard;
- no ellipsis;
- no generic directory ownership phrase;
- no path-range shorthand.

After repair, prove:

`declared candidate count == actual literal path entry count`

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST PATH-ONLY LITERAL LIST: REPAIRED`

---

# Phase 3 — Classify the two new Terra resumption artifacts

Identify the exact repository paths of the two newly created Terra resumption authority files.

Expected filenames:

- `release-1.10-git-candidate-publication-pull-request-authority-resumption-codex-prompt.md`
- `release-1.10-git-candidate-publication-pull-request-authority-resumption-chat-bootstrap.md`

Use actual repository paths.

For each file freeze:

- exact path;
- creation timing relative to prior reconciliation;
- governance purpose;
- whether it is an execution-control input;
- whether it belongs in the published Release 1.10 candidate;
- whether it is staging-authorized;
- whether it changes release semantics;
- ownership category.

Choose exactly one deterministic policy for the pair:

A. INCLUDE both in canonical publication candidate.
B. EXCLUDE both while preserving locally as execution-control artifacts.
C. another exact policy, justified by repository governance.

Do not split the pair unless repository evidence proves different ownership.

Emit:

`RELEASE 1.10 PUBLICATION-RESUMPTION ARTIFACT CLASSIFICATION: FROZEN`

---

# Phase 4 — Recompute canonical raw and publication sets

Starting from the actual 107-path raw worktree, classify every path into exactly one set:

1. canonical publication candidate;
2. explicitly excluded execution-control artifact;
3. unrelated local work;
4. invalid/unexpected path requiring BLOCK.

The manifest must explicitly enumerate the publication candidate and all excluded execution-control paths.

Freeze:

- raw worktree path count;
- tracked count;
- untracked count;
- canonical publication candidate count;
- candidate tracked count;
- candidate untracked count;
- prompt-artifact count;
- excluded execution-control count;
- exact excluded execution-control path list.

The arithmetic must reconcile exactly.

Example structure:

`raw = publication + excluded execution-control + unrelated`

No hidden remainder is allowed.

Emit:

`RELEASE 1.10 CANONICAL PUBLICATION SET ARITHMETIC: PASS`

---

# Phase 5 — Canonical base preservation

Verify:

- current branch HEAD/base state remains anchored to:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- `origin/main` remains the same.
- no publication Git mutation occurred since re-anchor.

This authority does NOT re-anchor again.

Freeze:

- canonical PR base SHA;
- required candidate commit parent SHA.

Expected:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Emit:

`RELEASE 1.10 PUBLICATION BASE PRESERVATION: PASS`

---

# Phase 6 — Exact Terra staging boundary

The corrected manifest must provide Terra with one exhaustive literal staging allowlist.

Requirements:

- exact path list only;
- exact declared count;
- exact included publication authority/resumption authority classification;
- exact excluded-control list;
- no stale 101-path or 103-path assertion unless still literally true after reconciliation;
- no stale 21+82 or 70-prompt count unless still literally true;
- no CRLF warning text;
- no implicit "all Release 1.10 files" wording.

Question:

> Could two competent Terra implementers stage different path sets while both claiming compliance?

Required answer:

**NO**

Emit:

`RELEASE 1.10 TERRA STAGING BOUNDARY: FROZEN`

---

# Phase 7 — Validation consequence

Determine whether this reconciliation changes semantic release content.

If only execution-control artifact classification/counts change:

- preserve previously frozen full re-anchor validation policy;
- do not invent extra implementation validation;
- Terra must still perform the full validation required before publication if it has not yet been completed successfully after the current re-anchor.

If canonical candidate content changes beyond execution-control prompt inclusion/exclusion, specify exact revalidation impact and BLOCK if semantic policy judgment is required.

Emit:

`RELEASE 1.10 PUBLICATION VALIDATION CONSEQUENCE: FROZEN`

---

# Phase 8 — Planning artifact repair

Update the minimum planning artifacts.

## Manifest must contain

- canonical base SHA;
- raw worktree count;
- exact canonical publication count;
- exact tracked/untracked candidate counts;
- exact prompt-artifact count;
- literal publication candidate path list;
- exact exclusions list;
- classification of:
  - Git-publication authority pair;
  - remote-base reconciliation pair;
  - Terra resumption authority pair;
- explicit statement that all list entries are repository paths only.

## Execution plan must contain, if needed

- exact Terra resumption staging count;
- exact exclusion handling;
- preserved base/parent;
- preserved validation requirement;
- statement that prior stale count literals are superseded.

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST PLANNING REPAIR: PASS`

---

# Phase 9 — Literal-list verification

Programmatically or mechanically verify:

- every manifest candidate entry resolves syntactically as a repository path;
- no entry begins with known Git warning text;
- no entry contains warning prose;
- no duplicate entry;
- actual candidate-entry count equals declared candidate count;
- exact tracked/untracked decomposition matches current inventory/classification;
- excluded paths are not duplicated in candidate list.

Produce a compact verification table:

| Check | Expected | Actual | Result |
|---|---:|---:|---|
| Raw paths | frozen | actual | PASS |
| Candidate paths | frozen | actual | PASS |
| Candidate tracked | frozen | actual | PASS |
| Candidate untracked | frozen | actual | PASS |
| Prompt artifacts | frozen | actual | PASS |
| Excluded execution-control | frozen | actual | PASS |
| Non-path entries | 0 | actual | PASS |
| Duplicates | 0 | actual | PASS |

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST LITERAL VERIFICATION: PASS`

---

# Phase 10 — Terra materialization simulation

Simulate resumed Terra publication with ZERO Git mutation.

Prove Terra can:

1. start from the already re-anchored base;
2. read the exact literal allowlist;
3. stage exactly the canonical candidate;
4. leave all excluded execution-control files unstaged;
5. produce the required candidate parent;
6. run the required validation;
7. commit/push/create PR under the existing publication authority without making any content-policy choice.

Required question:

> Does any staging, inclusion/exclusion, count, base, parent, or validation choice remain for Terra?

Required answer:

**NO**

Emit:

`RELEASE 1.10 PUBLICATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 11 — Terra resumption handoff

Produce an exact handoff to:

**Release 1.10 — Git Candidate Publication & Pull Request Authority — GPT-5.6 Terra**

State literally:

- selected model: GPT-5.6 Terra;
- current/canonical base SHA;
- required candidate parent SHA;
- raw worktree count;
- canonical publication candidate count;
- candidate tracked/untracked counts;
- prompt-artifact count;
- exact excluded execution-control paths;
- exact classification of the Terra resumption prompt pair;
- exact manifest section containing the literal staging list;
- required validation policy;
- no merge;
- no milestone close;
- no tag/version;
- no GitHub Release.

If prior Terra authority/resumption prompt contains stale count literals, explicitly supersede only those stale literals with this repaired manifest/handoff. Keep every other safety constraint binding.

Emit:

`RELEASE 1.10 MANIFEST REPAIR → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`

---

# Phase 12 — Mutation audit

Report exact mutations.

Expected:

- manifest: changed;
- execution plan: changed only if necessary;
- definition: unchanged;
- production/tests/docs/packages/project/schema/signing: ZERO;
- Git: ZERO;
- GitHub: ZERO.

Reverify:

- #242–#249 Closed/Done;
- milestone #59 Open, 0 open / 8 closed;
- no staging/commit/push/PR/tag/release mutation.

Emit:

`RELEASE 1.10 PUBLICATION-MANIFEST REPAIR MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 PUBLICATION-MANIFEST REPAIR ENTRY: PASS`

`RELEASE 1.10 PUBLICATION-MANIFEST INVENTORY: COMPLETE`

`RELEASE 1.10 PUBLICATION-MANIFEST PATH-ONLY LITERAL LIST: REPAIRED`

`RELEASE 1.10 PUBLICATION-RESUMPTION ARTIFACT CLASSIFICATION: FROZEN`

`RELEASE 1.10 CANONICAL PUBLICATION SET ARITHMETIC: PASS`

`RELEASE 1.10 PUBLICATION BASE PRESERVATION: PASS`

`RELEASE 1.10 TERRA STAGING BOUNDARY: FROZEN`

`RELEASE 1.10 PUBLICATION VALIDATION CONSEQUENCE: FROZEN`

`RELEASE 1.10 PUBLICATION-MANIFEST PLANNING REPAIR: PASS`

`RELEASE 1.10 PUBLICATION-MANIFEST LITERAL VERIFICATION: PASS`

`RELEASE 1.10 PUBLICATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 MANIFEST REPAIR → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`

`RELEASE 1.10 PUBLICATION-MANIFEST REPAIR MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 — PUBLICATION MANIFEST LITERAL-LIST REPAIR & RESUMPTION-ARTIFACT CLASSIFICATION RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- actual raw worktree inventory cannot be reconciled exactly;
- non-path manifest corruption extends beyond deterministically removable warning text;
- the two Terra resumption files cannot be identified exactly;
- inclusion/exclusion policy cannot be determined from governance;
- unrelated local work is mixed into the claimed candidate;
- candidate arithmetic leaves an unexplained remainder;
- a semantic content decision beyond narrow publication governance is required;
- base/parent has changed again;
- a mutation outside the allowed planning boundary is required.

On BLOCK:

- Git/GitHub mutations remain ZERO;
- preserve the already-safe re-anchor;
- do not stage;
- do not commit;
- do not push;
- do not create/update PR;
- report the minimum unresolved governance choice.

# Exact blocked terminal

`RELEASE 1.10 — PUBLICATION MANIFEST LITERAL-LIST REPAIR & RESUMPTION-ARTIFACT CLASSIFICATION RECONCILIATION AUTHORITY BLOCKED`

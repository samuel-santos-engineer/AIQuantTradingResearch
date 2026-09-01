# GPT-5.6 Luna — Unmerged Repository Artifacts Reconciliation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: read-only repository-state reconciliation, artifact classification, publication-set definition, governance boundary decisions, acceptance criteria.
- **GPT-5.6 Terra** — implementation/publication/merge/lifecycle mutations only under a later explicit authority.
- **GPT-5.6 Sol** — supporting analysis and synthesis only; never silently replaces Luna or Terra.

## 1. Mission

Reconcile the current local repository state after Release 1.12 WP02 completion and determine exactly which unmerged repository artifacts should be published, excluded, split, deleted, or left local.

This authority is read-only.

It MUST NOT stage, commit, restore, delete, create, move, rename, publish, merge, or otherwise mutate repository state.

The output must freeze one or more literal publication path sets suitable for a later GPT-5.6 Terra Git/PR publication authority.

## 2. Canonical starting point

Expected canonical `main` anchor:

`0ffca425f485fdea23e4a2f88ee2c5968b6046f0`

Expected Git synchronization:
- local `main` = `origin/main`
- ahead/behind `0/0`

Expected known local state from completed WP02 authority:
- 73 pre-existing tracked deletions were preserved and unstaged;
- unrelated untracked controls/deployment artifacts were preserved and unstaged;
- `prompters/` remained untracked/preserved;
- none of these local changes overlapped the exact WP02 3/3 payload;
- #261 is Closed/Done;
- #262 is Open/Todo;
- milestone #63 is Open with 6 open / 2 closed.

Fresh empirical evidence controls if counts or paths differ.

## 3. Purpose of this reconciliation

Do not assume:
- every tracked deletion is intended;
- every untracked file should be committed;
- all artifacts belong in one PR;
- filenames imply ownership;
- historical feasibility evidence should automatically become Release 1.12 implementation content;
- deployment/control artifacts should automatically attach to WP03.

Instead, classify every local path by actual content, provenance, relationship to current governance, and publication necessity.

## 4. Required read-only inventory

Collect and reconcile, without mutation:

### Git state
- current branch;
- `HEAD`;
- `origin/main`;
- ahead/behind;
- `git status --short`;
- staged paths;
- unstaged tracked modifications/deletions;
- untracked paths;
- ignored paths where materially relevant.

### Tracked deletions
For every tracked deletion:
- literal repository-relative path;
- whether it exists on canonical `main`;
- last commit introducing/modifying it;
- high-level purpose;
- whether deletion appears intentional, obsolete, generated, duplicated, superseded, or accidental;
- whether any current tracked file references it;
- whether deletion belongs to a coherent publication set.

### Untracked files/directories
For every untracked path:
- expand directories to literal files where practical;
- literal path;
- file type;
- high-level purpose;
- whether it contains secrets, credentials, tokens, auth caches, machine-local state, generated output, binaries, logs, or other non-publishable content;
- relationship to Initiative-1.11, Release 1.12, WP03+, tooling, controls, prompts, or unrelated local workflow;
- whether it should be published, ignored, retained local, or separately governed.

Do not use directory-only authorization in the final publishable path set.

## 5. Required source inspection

Read enough content from candidate artifacts to classify them correctly.

For scripts/config/deployment artifacts, determine:
- whether they are evidence-only;
- whether they are reusable implementation tooling;
- whether they encode Azure/App Service/GHCR assumptions;
- whether they mutate resources;
- whether they contain environment-specific identifiers;
- whether they are safe for public repository publication;
- whether they belong to Release 1.12 WP03 or later;
- whether publishing them now creates hidden architecture coupling.

For `prompters/`:
- inspect representative/full content as needed;
- determine whether it is project-governance source, generated working material, local-only operator content, or publishable documentation/tooling;
- do not assume it belongs in the repository merely because it is useful.

## 6. Secret and sensitive-content screening

Perform read-only screening of all candidate untracked files and relevant deleted/replacement artifacts.

At minimum look for:
- Azure subscription/resource identifiers where publication is unnecessary;
- tokens;
- API keys;
- auth headers;
- cookies;
- local account/user paths;
- credentials;
- Docker auth;
- GitHub auth;
- provider secrets;
- private endpoints;
- transient execution logs containing sensitive material.

A candidate path with unresolved sensitive content MUST NOT enter a publishable path set.

Do not modify or sanitize files under this authority.

## 7. Classification taxonomy

Every local path must receive exactly one primary classification:

- `PUBLISH_NOW`
- `PUBLISH_SEPARATELY`
- `KEEP_LOCAL`
- `RESTORE_TRACKED_PATH`
- `DELETE_LOCAL_ONLY`
- `BLOCKED_REVIEW`
- `IGNORED_GENERATED`
- `ALREADY_GOVERNED_HISTORICAL`

For tracked deletions:
- `PUBLISH_NOW` means the deletion itself is intended and should be staged as a deletion.
- `RESTORE_TRACKED_PATH` means the local deletion should not be published and a later authorized cleanup authority may restore it.
- `PUBLISH_SEPARATELY` means the deletion belongs to another coherent publication package.

Do not perform the restore/delete/publication here.

## 8. Governance ownership mapping

For each candidate path, identify ownership where applicable:

- Initiative-1.11 historical evidence/governance;
- Release 1.12 cross-cutting governance;
- Release 1.12 WP03 — GHCR Publication & Azure F1 Deployment Automation;
- WP04 — Persistent SQLite Initialization, Data Update & Recovery;
- WP05 — Twelve Data Runtime Configuration, Secrets & Bounded Automation;
- WP06 — Public Streamlit/System Health Deployment & Truthful Diagnostics;
- WP07 — Stability, Recovery, Cost & No-Bypass Validation;
- WP08 — Documentation, Operational Runbook & Release Acceptance;
- repository tooling/engineering hygiene;
- local-only operator workflow;
- unrelated/unknown.

A path may have secondary relevance, but one primary ownership decision is required.

## 9. Publication grouping

If publishable artifacts are found, Luna must decide whether they form:

- one coherent publication PR;
- multiple coherent PRs;
- no publication set yet.

Do not group unrelated artifacts merely to clear `git status`.

Each proposed publication group must have:
- exact purpose;
- governance owner;
- exact literal path set;
- exact tracked-deletion set;
- exact create/add set;
- rationale for grouping;
- whether it must be merged before WP03 begins;
- whether it may be published independently/later.

## 10. Literal path-set closure

For every publication group, output an exact sorted block:

`PUBLICATION_SET_<N>`

Every path must be repository-relative and literal.

No:
- wildcards;
- globs;
- directory-only entries;
- `...`;
- “related files”;
- “all scripts under”;
- future unspecified files.

For each set report:
- `TOTAL_PATH_COUNT`
- `TRACKED_DELETION_COUNT`
- `UNTRACKED_CREATE_COUNT`
- `OTHER_MODIFICATION_COUNT`
- `SCRIPT_COUNT`
- `DOC_COUNT`
- `CONFIG_COUNT`
- `EVIDENCE_COUNT`
- `GOVERNANCE_COUNT`

## 11. Mandatory decision on the 73 tracked deletions

The authority must explicitly reconcile the empirically observed tracked deletions.

Output:

`TRACKED_DELETION_RECONCILIATION`

with:
- observed count;
- intended publishable deletions;
- restore-later paths;
- separately governed deletions;
- blocked/unresolved deletions;
- exact count equality check.

The classifications must account for every observed tracked deletion exactly once.

If observed count is not 73, state the fresh count and explain the discrepancy from prior WP02 evidence.

## 12. Mandatory decision on untracked artifacts

Output:

`UNTRACKED_ARTIFACT_RECONCILIATION`

with every literal untracked file classified.

Directories may be summarized in prose only after the contained literal files have been accounted for.

Explicitly identify:
- controls artifacts;
- deployment artifacts;
- feasibility/probe evidence;
- prompts/prompters;
- temporary/generated files;
- anything sensitive or machine-local.

## 13. WP03 dependency decision

Luna must decide exactly one:

`WP03 PUBLICATION PREREQUISITE: REQUIRED`

or

`WP03 PUBLICATION PREREQUISITE: NOT REQUIRED`

If REQUIRED:
- identify the exact publication set(s) that must merge before WP03 implementation;
- explain the dependency.

If NOT REQUIRED:
- explain why WP03 may proceed independently without relying on unpublished local artifacts.

Do not leave this implicit.

## 14. Historical-governance protection

Preserve:
- Release 1.10 immutable historical anchor;
- Initiative-1.11 historical qualification;
- Release 1.12 WP01/WP02 completed lifecycle;
- Product Release 1.11 abandoned/nonexistent status;
- Release 1.12 sequence and milestone;
- frozen Release 1.12 planning artifacts unless a separate governance amendment is justified.

Do not retroactively rewrite prior authority outputs.

## 15. Read-only mutation prohibition

Authorized:
- file reads;
- Git reads;
- GitHub reads;
- hashes/counts/diffs;
- secret scanning in read-only mode;
- repository-reference searches.

Not authorized:
- file creation inside repository;
- file deletion;
- file restore;
- rename/move;
- formatting;
- staging;
- commit;
- branch;
- push;
- PR creation/merge;
- issue/Project/milestone mutation;
- Docker;
- Azure;
- GHCR;
- provider calls;
- package changes;
- schema changes.

If temporary analysis files are unavoidable, create them outside the repository, delete them before completion, and report them.

## 16. Required output structure

The final report must include:

1. canonical base reconciliation;
2. exact current Git state;
3. `TRACKED_DELETION_RECONCILIATION`;
4. `UNTRACKED_ARTIFACT_RECONCILIATION`;
5. secret/sensitive screening result;
6. ownership mapping;
7. publication grouping decision;
8. one or more exact `PUBLICATION_SET_<N>` blocks if applicable;
9. exact excluded/keep-local/restore-later sets;
10. `WP03 PUBLICATION PREREQUISITE` decision;
11. mutation audit;
12. next Terra authority recommendation.

## 17. Acceptance gates

### Gate A — base reconciliation
Canonical `main` state is proven.

### Gate B — total local-state closure
Every tracked deletion and untracked file is accounted for.

### Gate C — content/provenance classification
Classification is based on actual inspection, not filenames alone.

### Gate D — sensitive-content safety
No publishable set contains unresolved secrets/machine-local sensitive content.

### Gate E — publication-set coherence
Each proposed PR group has one coherent purpose and literal path closure.

### Gate F — deletion accounting
All observed tracked deletions are classified exactly once.

### Gate G — WP03 dependency
Publication prerequisite is explicit.

### Gate H — read-only audit
No repository/Git/GitHub/runtime mutation occurred.

## 18. Required markers

`UNMERGED ARTIFACTS — CANONICAL BASE RECONCILIATION: PASS`

`UNMERGED ARTIFACTS — GIT STATE INVENTORY: PASS`

`UNMERGED ARTIFACTS — TRACKED DELETION RECONCILIATION: PASS`

`UNMERGED ARTIFACTS — UNTRACKED ARTIFACT RECONCILIATION: PASS`

`UNMERGED ARTIFACTS — SENSITIVE CONTENT SCREENING: PASS`

`UNMERGED ARTIFACTS — GOVERNANCE OWNERSHIP MAPPING: PASS`

`UNMERGED ARTIFACTS — PUBLICATION GROUPING: PASS`

`UNMERGED ARTIFACTS — LITERAL PUBLICATION SET CLOSURE: PASS`

`UNMERGED ARTIFACTS — WP03 DEPENDENCY DECISION: PASS`

`UNMERGED ARTIFACTS — READ-ONLY MUTATION AUDIT: PASS`

Acceptance:

`UNMERGED ARTIFACTS — REPOSITORY RECONCILIATION: PASS`

If a Terra publication authority may be created:

`UNMERGED ARTIFACTS — RECONCILED TERRA PUBLICATION AUTHORITY: READY TO CREATE`

Terminal:

`UNMERGED ARTIFACTS — RECONCILIATION AUTHORITY COMPLETE`

If unresolved:

`UNMERGED ARTIFACTS — RECONCILIATION AUTHORITY BLOCKED`

State exact unresolved paths/classifications and do not authorize publication.

## 19. Completion boundary

This authority completes only when the entire current local unmerged state is accounted for, publishable artifacts are frozen into literal coherent set(s), non-publishable artifacts are explicitly classified, and the WP03 dependency decision is explicit.

It performs no mutation and does not itself create or merge any publication PR.

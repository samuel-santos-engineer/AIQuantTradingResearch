# Release Roadmap & Version-Sequencing Policy Reconciliation Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY: release/version policy, roadmap contract, architecture/governance reconciliation, acceptance criteria, and authoritative decision.
- **GPT-5.6 Terra** — implementation and approved Git/GitHub mutations only under explicit later authority.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory review; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

# Mission
Resolve the canonical release-sequencing conflict created by the proposed Azure public-deployment work.

The repository currently establishes:

`Release 1.10 → Release 2.0`

while a later proposal attempted to introduce:

`Release 1.10 → Release 1.11 → Release 2.0`

This authority must decide whether Release 1.11 is intentionally abandoned in favor of the canonical 2.0 sequence, or formally introduced through an explicit version-policy/roadmap amendment.

This is a narrow governance authority. Default behavior is read-only.

# Binding baseline
Verify independently before decision:
- local `main` = `origin/main` = `fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`, unless main legitimately advanced;
- staging/worktree state;
- Release 1.10 remains historically anchored to `v1.10.0`;
- prior Release 1.11 Phase A definition authority blocked before mutation;
- prior Release 1.11 milestone/taxonomy reconciliation blocked before mutation;
- no Release 1.11 planning artifacts, milestone, Project option, WP issues, Azure resources, commits, or GitHub mutations were created by those blocked authorities.

If main advanced, record the new baseline and prove `fe74af1d...` remains an ancestor.

Emit:
`RELEASE ROADMAP/VERSION POLICY RECONCILIATION ENTRY: PASS`

# Known canonical conflict
Treat these as evidence to re-verify, not assumptions to overwrite:
- all GitHub milestones were previously enumerated;
- milestone #60 is explicitly `Phase 4 - Release 2.0: Lightweight Machine Learning Evaluation`;
- milestone #61 is explicitly Release 2.3 Backtesting;
- no milestone represents Release 1.11;
- Project #2 Release taxonomy contains 2.0, 2.1, 2.2, 2.3 but no 1.11;
- roadmap and Release 1.9 governance artifacts reserve the next planned scope as Release 2.0.

Preserve milestone #60 and all later milestones unless a separate proven governance defect exists.

# Required evidence review
Read and reconcile all authoritative sources that can define version sequencing, including:
- `docs/roadmap/` indexes and release directories;
- Release 1.9 definition/execution/governance artifacts;
- Release 1.10 definition/execution/governance artifacts;
- any roadmap/versioning policy documents;
- all milestones from the late 1.x through 2.x sequence;
- Project #2 Release field/options and order;
- relevant GitHub issue/milestone descriptions;
- repository version/tag history where useful.

Produce a literal evidence matrix with:
- source;
- exact sequencing claim;
- authority level;
- current/stale status;
- implications for the Azure feasibility initiative.

Emit:
`RELEASE ROADMAP/VERSION POLICY EVIDENCE MATRIX: COMPLETE`

# Decision principles
The decision must prioritize:
1. historical consistency;
2. semantic meaning of version boundaries;
3. existing roadmap commitments;
4. minimal governance churn;
5. clear recruiter/public-deployment value;
6. provider independence;
7. ability to run Azure feasibility before production architecture changes;
8. preservation of Release 2.0/2.1/2.2/2.3 identities.

Do not introduce 1.11 merely because earlier conversational planning used that label.

Do not force Azure work into 2.0 merely because #60 is next.

The scope and version identity must both be justified.

# Analyze the Azure initiative independently of version number
Freeze the initiative conceptually as:

**Public Reference Deployment / Azure App Service F1 Feasibility Qualification**

Frozen deployment direction:
- Hugging Face Docker Spaces = ABANDONED.
- Azure Container Apps + Azure Files = DEFERRED.
- Azure App Service Linux F1 + custom Docker + persistent `/home` + SQLite = sole feasibility candidate.
- strict recurring infrastructure cost = `$0.00`.
- no production architecture changes until empirical feasibility passes.
- Azure remains deployment-only, never Domain/Application dependency.

This authority must determine where this initiative belongs in canonical roadmap governance.

# Required outcome analysis
Evaluate at least these three policies.

## Policy A — Preserve 1.10 → 2.0; no Release 1.11
Determine whether the Azure feasibility work should be:
- a non-release pre-2.0 feasibility initiative;
- a roadmap/governance spike;
- a bounded prerequisite attached to 2.0 without changing 2.0 capability identity;
- or deferred to a later already-planned release.

If selected, explicitly abandon the Release 1.11 identity.

## Policy B — Formally introduce Release 1.11
This is valid only if evidence supports an intentional new 1.x release boundary.

If selected, define the required canonical amendment:
`1.10 → 1.11 → 2.0`

Preserve #60 as Release 2.0.

Specify, but do not automatically execute:
- new Project #2 Release option `1.11`;
- new GitHub milestone for 1.11;
- exact roadmap documents requiring amendment;
- relationship between Phase A feasibility and any Phase B deployment;
- impact on future release sequencing.

## Policy C — Assign the initiative to another existing future release
If the Azure initiative semantically belongs to an already-defined release other than 2.0, identify it and prove why.

Do not distort an existing release definition merely to avoid adding a version.

# Mandatory authoritative decision
Select exactly one canonical policy and emit exactly one of:

`RELEASE VERSION-SEQUENCING POLICY: PRESERVE 1.10 → 2.0; RELEASE 1.11 ABANDONED`

`RELEASE VERSION-SEQUENCING POLICY: FORMALLY INTRODUCE 1.11 BETWEEN 1.10 AND 2.0`

`RELEASE VERSION-SEQUENCING POLICY: AZURE INITIATIVE ASSIGNED TO EXISTING RELEASE <VERSION>`

Do not emit multiple outcomes.

# Milestone preservation contract
Regardless of outcome, unless separately proven corrupt:
- milestone #60 remains Release 2.0;
- milestone #61 and all later milestone identities remain unchanged;
- no existing milestone is renumbered;
- no existing milestone is deleted/recreated to alter numbering;
- Release 1.10 history remains immutable.

Emit:
`EXISTING 2.X MILESTONE IDENTITIES: PRESERVED`

# Project #2 taxonomy contract
Determine the exact Project #2 Release-field consequences of the selected policy.

If 1.11 is abandoned:
- do not add `1.11`.

If 1.11 is formally introduced:
- specify the exact new `1.11` option required;
- preserve all existing option IDs/order as much as the platform allows;
- do not delete/recreate existing options.

If assigned elsewhere:
- use the existing canonical Release option and prove it matches.

Emit:
`PROJECT #2 RELEASE TAXONOMY DECISION: FROZEN`

# Roadmap amendment specification
For the selected policy, identify the exact repository documentation that would need amendment, if any.

Do not edit it under this authority unless the repository's established governance explicitly permits Luna to publish a narrow policy reconciliation in the same authority.

Preferred behavior: produce an exact amendment plan and stop before mutation.

The amendment specification must include:
- file path;
- current canonical statement;
- required replacement/addition;
- reason;
- historical impact;
- whether it changes capability sequencing or only version identity.

Emit:
`ROADMAP AMENDMENT SPECIFICATION: COMPLETE`

# Azure feasibility handoff
Define the exact next authority after this policy decision.

Examples:

If 1.11 is abandoned:
- create a non-release Azure F1 Feasibility Initiative authority, or
- attach a clearly bounded feasibility prerequisite to the selected canonical release.

If 1.11 is introduced:
- authorize a narrow GitHub taxonomy mutation authority first;
- then resume the Release 1.11 Phase A definition authority.

If assigned to another release:
- create/reconcile that release's feasibility planning authority.

The handoff must retain:
- Hugging Face abandoned;
- Container Apps/Azure Files deferred;
- Azure F1 sole feasibility candidate;
- strict-$0;
- provider independence;
- empirical SQLite locking/journal validation;
- no production architecture change before feasibility PASS.

Emit:
`AZURE F1 FEASIBILITY GOVERNANCE HANDOFF: COMPLETE`

# Mutation boundary
## Default allowed
Read-only:
- Git/GitHub inspection;
- roadmap/taxonomy analysis;
- policy decision;
- exact amendment/mutation plan.

## Forbidden by default
- repository-content edits;
- commits/pushes;
- milestone creation/edit/closure;
- Project Release-option creation/edit/deletion;
- WP issue creation;
- Project item mutation;
- Azure resource creation;
- Docker deployment;
- production architecture changes;
- tag/release publication.

If established governance unambiguously permits a Luna policy document mutation here, do not exercise it unless necessary to resolve the policy. Prefer a later narrow mutation authority.

# Mutation audit
Report exact counts:
- repository-content edits;
- commits;
- pushes;
- milestone creations/edits/closures;
- issue mutations;
- Project mutations;
- Release-option mutations;
- Azure mutations.

Expected: all zero.

Emit:
`RELEASE ROADMAP/VERSION POLICY MUTATION AUDIT: PASS`

# Required success markers
`RELEASE ROADMAP/VERSION POLICY RECONCILIATION ENTRY: PASS`
`RELEASE ROADMAP/VERSION POLICY EVIDENCE MATRIX: COMPLETE`
`EXISTING 2.X MILESTONE IDENTITIES: PRESERVED`
`PROJECT #2 RELEASE TAXONOMY DECISION: FROZEN`
`ROADMAP AMENDMENT SPECIFICATION: COMPLETE`
`AZURE F1 FEASIBILITY GOVERNANCE HANDOFF: COMPLETE`
`RELEASE ROADMAP/VERSION POLICY MUTATION AUDIT: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Plus exactly one mandatory version-sequencing policy marker.

# Exact success terminal
`RELEASE ROADMAP & VERSION-SEQUENCING POLICY RECONCILIATION AUTHORITY COMPLETE`

# Block conditions
BLOCK if:
- repository/GitHub baseline cannot be reconciled;
- authoritative roadmap sources materially contradict each other;
- Project #2 taxonomy cannot be inspected;
- no policy can be selected without rewriting historical releases;
- the only apparent resolution requires renumbering/repurposing #60 or later milestones;
- Azure feasibility scope cannot be separated from provider-specific product architecture;
- a mutation is required merely to understand the canonical policy.

On BLOCK, make no mutation and identify the minimum higher-order governance decision required.

# Exact blocked terminal
`RELEASE ROADMAP & VERSION-SEQUENCING POLICY RECONCILIATION AUTHORITY BLOCKED`

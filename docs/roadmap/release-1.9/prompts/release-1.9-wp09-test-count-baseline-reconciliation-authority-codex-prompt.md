# Release 1.9 — WP09 Test-Count/Baseline Reconciliation Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only reconciliation authority** for Release 1.9 WP09, canonical issue **#234**.

Its sole purpose is to reconcile a contradiction in the binding WP09 contract:

- the WP09 contract currently states predecessor baseline `313/313` and post-WP09 total `329/329`;
- accepted post-WP08 technical evidence is `327/327`;
- the same WP09 contract defines an exact WP09 .NET test delta of `+12`;
- therefore the correct post-WP09 aggregate is `339/339`.

The consolidated Terra implementation authority correctly uses:

`327/327 → +12 .NET → 339/339`

This authority must update only the binding WP09 documentation so that all count/baseline language is internally consistent.

No implementation mutation.
No test mutation.
No Python mutation.
No package/schema mutation.
No GitHub mutation.
No WP10+ work.

---

# Binding accepted evidence

Treat the following as authoritative predecessor evidence:

## Post-WP08 .NET baseline
- Domain: 11/11
- Application: 125/125
- Infrastructure: 178/178
- Architecture: 13/13
- Aggregate: **327/327**

Arithmetic:

`11 + 125 + 178 + 13 = 327`

## WP09 authorized .NET delta
The binding WP09 contract already defines:

`+12 .NET`

Therefore expected post-WP09 aggregate:

`327 + 12 = 339`

Expected:

**339/339**

## Python
WP09 contract separately defines:
- +4 Python
- +16 total across .NET + Python additions.

Do not change those counts unless a direct contradiction is found.

---

# Source of contradiction

Binding artifact:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

contains stale count language inherited from an earlier pre-WP08 baseline:

- `313/313`
- `329/329`

Those values are no longer valid because WP08 permanently added 14 .NET tests between the earlier 313 baseline and the accepted 327 baseline.

This authority must correct those stale references.

---

# Objective

Amend the binding WP09 contract so that it consistently states:

## Pre-WP09 .NET baseline
- Domain 11
- Application 125
- Infrastructure 178
- Architecture 13
- aggregate **327**

## Authorized WP09 .NET delta
- `+12`

## Post-WP09 expected .NET total
- **339**

## Python delta
- `+4`

## Total new tests
- `+16`

No other semantic/path/ownership rule changes.

---

# Exact mutation scope

Modify exactly one file:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

No other repository path may change.

Do not create a replacement contract unless repository conventions require an amendment artifact instead of editing the binding contract.

Preferred outcome:
- correct the binding artifact directly;
- add a concise reconciliation note inside it if useful.

If governance forbids direct amendment of an accepted artifact, STOP and create no mutation; report that a separate superseding amendment artifact is required.

---

# Phase 0 — Read-only audit

Read the complete binding WP09 contract.

Find every occurrence or implication of:

- 313
- 329
- 327
- 339
- +12 .NET
- +4 Python
- +16 total
- predecessor baseline
- post-WP09 aggregate

Also inspect the accepted WP08 technical completion evidence/documentation necessary to confirm 327.

Do not mutate yet.

---

# Phase 1 — Reconciliation rule

Apply this exact rule:

`Accepted post-WP08 baseline supersedes stale historical pre-WP08 test totals for WP09 acceptance-count purposes.`

This does **not** supersede any semantic, path, scenario, architecture, ownership, regression, or residue rule.

Only count/baseline language changes.

---

# Phase 2 — Exact count correction

Replace every stale WP09 acceptance count reference so the contract consistently uses:

### Pre-WP09
`327/327`

with project distribution:

- Domain 11
- Application 125
- Infrastructure 178
- Architecture 13

### WP09 delta
`+12 .NET`

### Post-WP09
`339/339`

No unexplained deviation permitted.

If the contract allocates the +12 across projects, preserve that exact allocation.

If it does not, do not invent a distribution.

---

# Phase 3 — Historical-baseline clarification

If the artifact mentions `313/313` as historical context, it may remain only if clearly labeled:

`historical pre-WP08 baseline; not the WP09 predecessor acceptance baseline`

Do not leave any ambiguous statement implying WP09 begins from 313.

Likewise `329/329` may remain only as an obsolete historical arithmetic example if explicitly marked invalid; preferred approach is removal.

---

# Phase 4 — Regression-gate correction

Ensure the WP09 regression section states:

- current accepted pre-WP09 aggregate: **327**
- expected exact post-WP09 aggregate after +12 .NET: **339**
- 0 failures
- no unexplained skipped tests
- exact +12 delta mandatory.

Do not change Python regression requirements.

---

# Phase 5 — Completion-gate correction

Ensure every completion/acceptance gate that references .NET totals uses:

`327 → 339`

not:

`313 → 329`

No other completion criterion changes.

---

# Phase 6 — Terra-authority compatibility

Verify the corrected binding artifact is now consistent with the existing consolidated Terra implementation authority:

`release-1.9-wp09-consolidated-implementation-completion-authority-codex-prompt.md`

Specifically:
- predecessor 327
- +12 .NET
- post-WP09 339
- +4 Python
- +16 total

Do not modify the Terra prompt under this authority.

---

# Phase 7 — Scope audit

Changed path must be exactly the binding WP09 contract.

Prove zero:
- production;
- test;
- Python;
- package;
- schema;
- GitHub;
- WP10+.

No scenario/path/architecture semantic changes.

---

# Required completion report

## Contradiction found
Exact stale references.

## Accepted baseline
327/327 with per-project counts.

## Corrected expected total
339/339 after +12 .NET.

## Python/total delta
+4 Python / +16 total preserved.

## Artifact changed
Exact path.

## Semantic preservation
Confirm all non-count WP09 contract semantics/path authority unchanged.

## Mutation statement

`WP09 TEST-COUNT/BASELINE RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; one binding documentation artifact corrected`

## Next step

On success:

`WP09 BASELINE/COUNT CONTRACT RECONCILED — CONSOLIDATED TERRA IMPLEMENTATION MAY RESUME`

---

# Stop conditions

Stop if:
- accepted WP08 evidence does not support 327;
- +12 .NET delta is itself ambiguous;
- direct modification of the binding contract is prohibited by repository governance;
- correcting counts would require changing scenario/test ownership semantics.

Do not broaden scope.

---

# Terminal markers

Success:

`RELEASE 1.9 WP09 TEST-COUNT AND BASELINE RECONCILIATION COMPLETE`

Blocked:

`RELEASE 1.9 WP09 TEST-COUNT AND BASELINE RECONCILIATION BLOCKED`

Do not emit COMPLETE unless every stale WP09 baseline/aggregate reference is reconciled to the accepted 327 → 339 count model.

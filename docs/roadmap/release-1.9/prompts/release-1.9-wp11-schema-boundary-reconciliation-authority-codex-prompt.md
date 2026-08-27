# Release 1.9 — WP11 Schema-Boundary Reconciliation Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **very narrow documentation-only schema-boundary reconciliation authority** for Release 1.9 WP11 / issue #236.

Its sole purpose is to resolve an authoritative contradiction between:

- stale Release 1.9 planning references to schema v3; and
- later accepted schema-evolution authority plus current implementation and accepted WP09 contract using schema v4.

No production mutation.
No migration mutation.
No persistence behavior change.
No test mutation.
No Python mutation.
No package mutation.
No GitHub mutation.
No WP12+ work.

---

# Verified contradiction

Current canonical sources conflict:

## Stale v3 references
The following Release 1.9 planning artifacts currently require/preserve schema v3:

- `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md`
- `docs/roadmap/release-1.9/RELEASE_1.9_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.9/RELEASE_1.9_FILE_MANIFEST.md`

## Accepted v4 evolution
The accepted WP03 schema-evolution authority explicitly defines schema v4.

The authority must locate and read the exact WP03 schema-evolution artifact and treat its accepted semantics as candidate later authority.

## Current implementation
Current repository implementation uses schema v4:

- `SqliteSchemaBootstrapper.cs`
  - `CurrentVersion = 4`
  - `PRAGMA user_version = 4`

Read the exact current file and confirm paths/symbols before amendment.

## Later accepted WP09 contract
The accepted WP09 contract requires schema-v4 preservation.

Read:
`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

Confirm exact schema-version language.

---

# Objective

Determine the authoritative Release 1.9 schema boundary by applying repository-governance precedence.

The likely intended result is:

`Release 1.9 canonical persistence schema boundary = v4`

but this authority MUST verify that conclusion rather than assume it.

The reconciliation must then update only the stale documentation language necessary so WP11 can define schema acceptance unambiguously.

---

# Precedence rule to verify

Use this candidate precedence rule only if repository governance supports it:

1. accepted explicit schema-evolution authority;
2. later accepted work-package contracts that depend on that evolution;
3. current implementation consistent with those accepted authorities;
4. earlier high-level planning text.

If repository governance defines a different precedence rule, use it and explain.

Do not use “current code wins” by itself.

---

# Canonical output decision

The artifact must conclude exactly one:

## SCHEMA-V4-CANONICAL
Release 1.9 acceptance must preserve schema v4.

or

## SCHEMA-V3-CANONICAL
Release 1.9 acceptance must preserve schema v3, implying current implementation/later contracts are inconsistent.

If SCHEMA-V3-CANONICAL is selected:
- STOP before modifying implementation;
- report that a separate implementation rollback/reconciliation authority is required.

Do not change code here.

---

# Exact mutation scope

Preferred if SCHEMA-V4-CANONICAL:

Modify only stale schema-version language in these existing Release 1.9 planning artifacts:

1. `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md`
2. `docs/roadmap/release-1.9/RELEASE_1.9_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.9/RELEASE_1.9_FILE_MANIFEST.md`

Do not alter unrelated planning semantics.

If repository governance forbids direct edits to accepted planning artifacts:
- STOP with zero mutation;
- create no substitute artifact unless governance explicitly requires a separate amendment path;
- report the exact required superseding amendment authority.

No other file may change.

---

# Phase 0 — Read-only source audit

Read completely:

- the three stale v3 planning artifacts;
- accepted WP03 schema-evolution authority/artifact;
- current `SqliteSchemaBootstrapper.cs`;
- any migration/bootstrap tests that prove v4 only as read-only evidence;
- accepted WP09 contract;
- any later WP10/WP11 planning references to schema version.

Find every occurrence of:
- schema v3;
- schema v4;
- `CurrentVersion`;
- `user_version`;
- migration/evolution language;
- persistence schema boundary.

No mutation yet.

---

# Phase 1 — Historical timeline

Construct a factual timeline:

1. Release 1.9 initial plan states v3.
2. WP03 accepted schema-evolution authority changes/defines v4.
3. implementation moved to v4.
4. later accepted WP09 contract preserves v4.
5. WP11 planning still inherits stale v3 wording.

Use exact artifact names/dates/order if available.

This timeline is essential to prove whether the planning references are stale rather than contradictory current intent.

---

# Phase 2 — Semantic scope check

Verify what “schema version” means in each source.

Confirm that:
- the v3/v4 references refer to the same persistence schema/version boundary;
- they are not different schema domains (for example JSON envelope version vs SQLite persistence version).

If different domains are involved:
- STOP;
- report that apparent contradiction is a terminology collision.

Do not reconcile unlike versions.

---

# Phase 3 — Canonical version decision

Choose SCHEMA-V4-CANONICAL only if all are true:

1. WP03 explicitly and validly authorizes evolution to v4;
2. current implementation is intentionally v4, not accidental drift;
3. accepted later WP09 contract explicitly preserves v4;
4. no later accepted authority supersedes v4 back to v3;
5. v3 references in definition/plan/manifest predate WP03 or otherwise fail later-authority precedence.

If any condition fails:
- do not force v4;
- report the unresolved governance conflict.

---

# Phase 4 — Exact documentation reconciliation

If SCHEMA-V4-CANONICAL:

Update only schema-version references needed to make Release 1.9 planning consistent.

Required semantic correction:

- “schema v3” → “schema v4” where the text refers to the canonical persistence schema boundary after WP03 evolution.
- Preserve historical wording only if clearly marked historical/pre-WP03.

Do not globally replace every numeral “3”.

Do not modify:
- JSON/read-model versions;
- Release 1.8 version references;
- unrelated schema terminology.

---

# Phase 5 — Manifest correction

In `RELEASE_1.9_FILE_MANIFEST.md`, ensure any:
- acceptance baseline;
- persistence/schema version;
- WP11 release gate;
- test expectation

uses canonical v4 where it refers to the persistence schema.

Do not alter path ownership or file allowlists except where schema-version text itself is stale.

---

# Phase 6 — Definition correction

In `RELEASE_1.9_DEFINITION.md`, ensure Release 1.9’s canonical persistence boundary states v4.

If useful, add one concise clarification:

“Schema v4 reflects the accepted WP03 schema-evolution authority and supersedes the initial v3 planning baseline.”

Do not rewrite release scope.

---

# Phase 7 — Execution-plan correction

In `RELEASE_1.9_EXECUTION_PLAN.md`, update only:
- predecessor baseline;
- preservation gate;
- WP11 acceptance references

that still state v3 but mean current persistence schema.

Do not change execution ordering or WP ownership.

---

# Phase 8 — Compatibility audit

After amendment, verify consistency with:

- WP03 schema-evolution authority;
- current `SqliteSchemaBootstrapper`;
- migration/bootstrap tests;
- WP09 contract;
- WP10 docs;
- prospective WP11 schema acceptance.

Required final statement if v4 canonical:

`WP11 schema acceptance must prove preservation of canonical persistence schema v4; no schema migration/change is part of WP11.`

---

# Phase 9 — Test-count and implementation preservation

This reconciliation changes no executable behavior.

Therefore preserve:
- .NET baseline 339/339;
- Python baseline 17/17;
- build baseline 0 warnings / 0 errors.

No test-count change.

Do not run or add tests unless repository governance requires read-only verification.

---

# Phase 10 — Scope audit

Changed paths must be exactly the stale planning docs authorized above.

Prove zero:
- production;
- migrations;
- tests;
- Python;
- packages;
- schema implementation;
- GitHub;
- WP12+.

---

# GitHub boundary

Keep:
- #236 Open / Backlog;
- #237 Open / Backlog;
- milestone #58 Open.

GitHub mutations:
`ZERO`

---

# Required completion report

## Sources read
Exact planning/WP03/WP09/implementation sources.

## Version-domain check
Prove v3 and v4 refer to the same persistence schema boundary.

## Timeline
Initial v3 → accepted WP03 v4 → current v4 → later WP09 v4.

## Canonical decision
`SCHEMA-V4-CANONICAL` or `SCHEMA-V3-CANONICAL`.

## Files changed
Exact paths.

## Semantic preservation
Confirm only stale schema-boundary wording changed.

## Baselines
339/339 .NET, 17/17 Python unchanged.

## Mutation statement

If v4 reconciliation succeeds:

`WP11 SCHEMA-BOUNDARY RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; stale Release 1.9 planning schema references reconciled to canonical v4`

## Resume marker

On successful v4 reconciliation:

`WP11 SCHEMA BOUNDARY RECONCILED TO V4 — WP11 FULL-INTEGRATION CONTRACT AUTHORITY MAY RESUME`

---

# Stop conditions

STOP with zero mutation if:

- v3 and v4 refer to different schema domains;
- WP03 evolution authority is not actually accepted/binding;
- later authority supersedes v4 back to v3;
- current v4 implementation appears unauthorized drift;
- repository governance forbids direct correction of the stale planning docs;
- canonical version cannot be resolved without changing implementation.

Do not modify code.

---

# Terminal markers

Success:

`RELEASE 1.9 WP11 SCHEMA-BOUNDARY RECONCILIATION COMPLETE`

Blocked:

`RELEASE 1.9 WP11 SCHEMA-BOUNDARY RECONCILIATION BLOCKED`

Do not emit COMPLETE unless the canonical persistence schema version is authoritatively resolved and WP11 can consume one unambiguous schema boundary.

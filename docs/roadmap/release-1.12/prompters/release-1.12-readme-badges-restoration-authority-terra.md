# GPT-5.6 Terra — Release 1.12 README Badges Restoration Authority

**Selected execution model: GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: bounded README badge restoration, validation, Git/GitHub publication.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Mission

Repair only the top README badge section after merged PR #273.

Restore the prior badge visual language — including the **same badge colors used by the canonical pre-PR #273 README** — while updating badge labels/values to the current accepted project state.

The desired badge progression is exactly:

1. Release
2. Tests
3. Architecture Tests
4. .NET / C#
5. Python 3.13
6. Docker / Containerized
7. License / MIT

This is a badge-only front-door correction. Do not rewrite or remove any other README information.

## 2. Canonical objects

Current merged main anchor expected:

`3d34a0576e47f9fe7ae7ec123771885eb239ab33`

Historical badge-style source:

`6f8b19634301d03ce48dfc9f3c9ef1cfbc9a5b3d:README.md`

Current README source:

`3d34a0576e47f9fe7ae7ec123771885eb239ab33:README.md`

Fresh Git evidence controls.

If `main` advanced, reconcile first. Proceed only when the newer state does not conflict with this badge-only contract.

## 3. Exact repository mutation

Authorized path:

```text
README.md
```

Operation:

`MODIFY`

Exactly one repository path may change.

Do not change any other README section except the badge block and unavoidable immediately adjacent whitespace required for valid Markdown.

## 4. Historical color preservation

Before editing, inspect the exact badge markup from:

`6f8b19634301d03ce48dfc9f3c9ef1cfbc9a5b3d:README.md`

Extract and record:
- badge order;
- badge style;
- shields endpoint/markup convention;
- colors;
- logo/logoColor parameters where present;
- spacing/layout.

The restored badge block must reuse the **same prior visual color scheme** for corresponding badge roles wherever possible.

Do not arbitrarily redesign badge colors.

For newly added badges that had no historical equivalent:
- choose a color consistent with the existing historical badge palette;
- prefer standard technology-brand/logo semantics only when this does not visually break the historical badge family;
- record the selected value.

Required:

`README BADGES — HISTORICAL COLOR RECONCILIATION: PASS`

## 5. Badge contract

The resulting top badge row/block must contain these seven badges in this order.

### 5.1 Release badge

Visible label/value should express only the numeric release progression.

Use:

`1.12`

Do **not** use:
- `Release 1.12`
- `v1.12.0`
- `1.12.0`
- semantic tag claims
- `IN PROGRESS` inside the badge

The surrounding README already conveys that Release 1.12 is in progress.

The badge must therefore communicate simply the current release progression number:

`1.12`

Required:

`README BADGES — RELEASE PROGRESSION 1.12: PASS`

### 5.2 Tests badge

Badge label:

`tests`

Value:
- must be derived from fresh canonical validation evidence;
- must not reuse stale historical count merely for visual continuity;
- must not invent a number.

Prefer a concise passing count format if a current total can be derived exactly.

If there are multiple suites and no single canonical aggregate is governable, use a truthful nonnumeric value such as:

`passing`

Do not silently sum heterogeneous test suites unless the repository's accepted validation model treats that aggregate as legitimate.

Required:

`README BADGES — TESTS CURRENT/TRUTHFUL: PASS`

### 5.3 Architecture Tests badge

Badge label should remain recognizable as:

`architecture tests`

Value:
- derive current exact Architecture test count from fresh canonical test execution/evidence;
- if exact count is unavailable, use truthful `passing`;
- do not retain obsolete `13` or another stale count.

Known prior accepted Release 1.12 evidence may be used only if fresh repository execution confirms it remains current.

Required:

`README BADGES — ARCHITECTURE TESTS CURRENT/TRUTHFUL: PASS`

### 5.4 .NET / C# badge

Display technology foundation clearly.

Preferred visible wording:

`.NET / C#`

Do not imply a future-only technology.

Preserve the historical badge visual style/color family.

Required:

`README BADGES — DOTNET CSHARP: PASS`

### 5.5 Python 3.13 badge

Display:

`Python 3.13`

Python interoperability is implemented.

Do not describe Python as planned.

Required:

`README BADGES — PYTHON 3.13: PASS`

### 5.6 Docker badge

Display Docker as implemented containerization.

Preferred visible wording:

`Docker | containerized`

or an equivalent Shields badge rendering whose visible meaning is:

`Docker — containerized`

Do not describe Docker as planned.

Required:

`README BADGES — DOCKER CONTAINERIZED: PASS`

### 5.7 License badge

Display:

`license | MIT`

or equivalent Shields rendering with visible `MIT`.

Preserve the prior MIT badge color/style where present.

Required:

`README BADGES — LICENSE MIT: PASS`

## 6. Layout

Keep the badges immediately under the project title/description area in the same compact front-door style used historically.

Preferred:
- one visual row when Markdown rendering permits;
- no badge subsection heading unless one already existed historically;
- no prose inserted between individual badges;
- maintain consistent Shields style.

Do not add unrelated badges.

Final badge count:

`7`

Required:

`README BADGES — EXACT BADGE COUNT 7/7: PASS`

## 7. Current project truth

The badge restoration must remain consistent with current README truth:

- Current closed milestone:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification — FEASIBLE`
- Current accepted milestone:
  `Phase 4 - Release 1.12: Public Reference Deployment Implementation & Stabilization — IN PROGRESS`
- WP01–WP03 accepted;
- WP04 next;
- Product Release 1.11 does not exist;
- Release 1.12 is not complete;
- no `v1.12.0` tag claim.

## 8. Validation commands/evidence

Before publication verify:

- current canonical base;
- only `README.md` modified;
- only badge block materially changed;
- `git diff --check`;
- Markdown badge syntax;
- each badge image URL is syntactically valid;
- link targets are valid where badges are linked;
- no stale Release 1.5 badge;
- no stale Release 1.7 current-state badge;
- no obsolete test counts;
- release badge shows `1.12` only;
- Python badge shows `3.13`;
- Docker badge communicates `containerized`;
- MIT badge present;
- seven badges exactly;
- historical colors restored/reused;
- Gitleaks.

Required markers:

`README BADGES — CANONICAL BASE RECONCILIATION: PASS`

`README BADGES — HISTORICAL BADGE BLOCK RECONCILIATION: PASS`

`README BADGES — HISTORICAL COLOR RECONCILIATION: PASS`

`README BADGES — RELEASE PROGRESSION 1.12: PASS`

`README BADGES — TESTS CURRENT/TRUTHFUL: PASS`

`README BADGES — ARCHITECTURE TESTS CURRENT/TRUTHFUL: PASS`

`README BADGES — DOTNET CSHARP: PASS`

`README BADGES — PYTHON 3.13: PASS`

`README BADGES — DOCKER CONTAINERIZED: PASS`

`README BADGES — LICENSE MIT: PASS`

`README BADGES — EXACT BADGE COUNT 7/7: PASS`

`README BADGES — README-ONLY MUTATION 1/1: PASS`

`README BADGES — VALIDATION: PASS`

Acceptance:

`RELEASE 1.12 — FRONT-DOOR README BADGES RESTORATION: PASS`

## 9. Publication

After acceptance:
- create a dedicated docs branch from current canonical main;
- stage only `README.md`;
- commit once;
- push;
- create one non-draft PR to `main`;
- verify PR payload exactly 1/1.

Preferred branch:

`docs/release-1.12-readme-badges`

Preferred commit:

`Restore current README badges`

Preferred PR title:

`Docs: restore current project badges`

Do not create a duplicate PR if fresh evidence shows an existing open PR already carries this exact badge-only correction.

## 10. Merge boundary

This authority does **not** authorize PR merge.

Do not mutate:
- issue #263;
- Project #2;
- milestone #63;
- tags/releases;
- Azure;
- Docker runtime/resources;
- GHCR;
- Twelve Data/provider;
- packages;
- schema.

Required:

`README BADGES — COMMIT: PASS`

`README BADGES — PUSH: PASS`

`README BADGES — PR PAYLOAD 1/1: PASS`

`README BADGES — PR MERGE: NOT AUTHORIZED BY THIS AUTHORITY`

## 11. Mutation audit

Expected:
- repository modified paths: 1 (`README.md`)
- created/deleted/renamed repository paths: 0
- branches: 1
- commits: 1
- pushes: 1
- PRs: 1
- merges: 0
- issue mutations: 0
- Project mutations: 0
- milestone mutations: 0
- Azure/Docker/GHCR/provider mutations: 0
- package/schema mutations: 0

Report actual counts.

Terminal:

`RELEASE 1.12 — README BADGES RESTORATION AUTHORITY COMPLETE`

If blocked:

`RELEASE 1.12 — README BADGES RESTORATION AUTHORITY BLOCKED`

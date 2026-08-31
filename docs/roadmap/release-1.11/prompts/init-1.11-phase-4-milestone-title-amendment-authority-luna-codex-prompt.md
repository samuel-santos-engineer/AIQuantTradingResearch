# INIT-1.11 — Phase 4 Milestone-Title Amendment Authority

## Model assignment
- **GPT-5.6 Luna** — PRIMARY: governance reconciliation, naming contract, mutation boundary, acceptance verification.
- **GPT-5.6 Terra** — implementation/execution mutations only when separately authorized; not selected here.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Luna.**

# Mission
Perform exactly one substantive GitHub mutation:

Rename GitHub milestone **#62** from its current INIT-1.11 title to exactly:

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Then verify issues **#252–#257** remain intact, attached to milestone #62, and retain their existing lifecycle state.

This is a presentation/naming amendment only.

# Binding identity
The rename MUST NOT change initiative/release semantics.

Binding:
- `Initiative-1.11` is a non-release initiative identifier.
- `Initiative-1.11 ≠ Product Release 1.11`.
- Product Release 1.11 remains abandoned.
- Product release sequence remains `1.10 → 2.0`.
- milestone #60 remains Release 2.0.
- milestone #62 remains the Azure F1 non-release initiative milestone.
- no Project Release option `1.11` may be created.
- no WP may be assigned Project Release `1.11` or `2.0`.
- no tag, GitHub Release, branch, package version, or product-version metadata is authorized.

Required marker:
`INIT-1.11 MILESTONE IDENTITY: NON-RELEASE — UNCHANGED`

# Pre-mutation read-only verification
Before mutation, verify:
1. milestone #62 exists;
2. it is Open;
3. it represents the Azure F1 initiative;
4. issues #252–#257 exist;
5. all six are assigned to milestone #62;
6. #252 is Closed;
7. #253–#257 are Open;
8. milestone counts are 5 open / 1 closed;
9. Project #2 has all six items if accessible;
10. #252 Status = Done and #253–#257 Status = Todo if accessible;
11. Release field is unset for all six if accessible;
12. milestone #60 remains Release 2.0;
13. no Product Release 1.11 governance object has been introduced.

If any material mismatch exists, BLOCK before mutation rather than broadening scope.

Emit:
`INIT-1.11 MILESTONE TITLE AMENDMENT BASELINE: VERIFIED`

# Authorized mutation
Rename **milestone #62 only** to exactly:

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Important:
- GitHub milestone title is plain text.
- Do not insert Markdown link syntax into the GitHub milestone title.
- Do not change milestone number.
- Do not close milestone.
- Do not change milestone description.
- Do not change due date.
- Do not edit issue titles/bodies.
- Do not alter issue state.
- Do not alter issue milestone assignments.
- Do not alter Project fields.
- Do not alter repository content.

Emit:
`MILESTONE #62 TITLE AMENDMENT: PASS`

# Required post-mutation verification
Read milestone #62 back from GitHub and verify its title is exactly:

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Then independently verify:

## Milestone
- number = #62;
- state = Open;
- open issues = 5;
- closed issues = 1;
- description unchanged;
- due date unchanged.

## Issues
- #252 exists, remains Closed, remains assigned to #62;
- #253 exists, remains Open, remains assigned to #62;
- #254 exists, remains Open, remains assigned to #62;
- #255 exists, remains Open, remains assigned to #62;
- #256 exists, remains Open, remains assigned to #62;
- #257 exists, remains Open, remains assigned to #62.

Do not edit these issues merely to verify them.

Emit:
`INIT-1.11 ISSUES #252-#257 INTEGRITY: PASS`

## Project #2
If Project #2 can be read:
- all six items remain attached;
- #252 remains Done;
- #253–#257 remain Todo;
- Release remains unset for all six.

No Project mutation is authorized.

Emit:
`INIT-1.11 PROJECT #2 INTEGRITY: PASS`

If Project #2 cannot be read because of transient network/API limitations but milestone/issue integrity is independently verified, report the limitation truthfully. Do not mutate or infer Project state.

# Release-governance verification
Verify after rename:
- milestone #60 remains unchanged;
- Release 2.0 scope is untouched;
- Product Release 1.11 remains absent/abandoned;
- no Project Release taxonomy mutation occurred;
- milestone #62 is still explicitly governed as a non-release initiative despite the `Phase 4` prefix.

The `Phase 4` prefix is organizational presentation only and MUST NOT imply that milestone #62 is Release 2.0 or a numbered product release.

Emit:
`PHASE 4 PREFIX SEMANTICS: PRESENTATION ONLY — RELEASE GOVERNANCE UNCHANGED`

# Repository boundary
Repository mutation is forbidden.

Required:
- repository edits = 0;
- commits = 0;
- pushes = 0;
- staging mutations = 0.

No documentation amendment is authorized here.

# Other forbidden mutations
Must remain zero:
- issue creations;
- issue edits;
- issue closures/reopens;
- issue milestone reassignment;
- Project item additions/removals;
- Project Status mutations;
- Project Release mutations;
- label mutations;
- milestone creation;
- milestone closure;
- Azure mutations;
- registry mutations;
- Twelve Data requests;
- tags;
- GitHub Releases;
- PRs/merges.

# Mutation audit
Report exact mutation counts.

Expected:
- milestones renamed/edited: 1;
- all other GitHub mutations: 0;
- repository/Git mutations: 0;
- Azure/registry/Twelve Data mutations: 0.

If an API operation represents the rename as one milestone edit, count it once.

Emit:
`INIT-1.11 MILESTONE TITLE AMENDMENT MUTATION AUDIT: PASS`

# Required success markers
`INIT-1.11 MILESTONE TITLE AMENDMENT BASELINE: VERIFIED`
`INIT-1.11 MILESTONE IDENTITY: NON-RELEASE — UNCHANGED`
`MILESTONE #62 TITLE AMENDMENT: PASS`
`INIT-1.11 ISSUES #252-#257 INTEGRITY: PASS`
`PHASE 4 PREFIX SEMANTICS: PRESENTATION ONLY — RELEASE GOVERNANCE UNCHANGED`
`INIT-1.11 MILESTONE TITLE AMENDMENT MUTATION AUDIT: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Also emit `INIT-1.11 PROJECT #2 INTEGRITY: PASS` when Project #2 read-back succeeds.

# Exact success terminal
`INIT-1.11 — PHASE 4 MILESTONE-TITLE AMENDMENT AUTHORITY COMPLETE`

# Block conditions
BLOCK before mutation if:
- milestone #62 cannot be uniquely verified;
- #62 no longer represents this initiative;
- any of #252–#257 is missing or assigned to another milestone;
- issue lifecycle differs materially from #252 Closed / #253–#257 Open;
- milestone counts differ materially from 5 open / 1 closed without a known legitimate reason;
- requested rename would require any broader governance mutation;
- GitHub permissions do not permit the single milestone rename.

If the rename succeeds but post-mutation verification encounters a transient read failure:
- do not retry by making another mutation;
- report the successful mutation if proven;
- report the verification limitation;
- do not claim COMPLETE unless required integrity can be established.

# Exact blocked terminal
`INIT-1.11 — PHASE 4 MILESTONE-TITLE AMENDMENT AUTHORITY BLOCKED`

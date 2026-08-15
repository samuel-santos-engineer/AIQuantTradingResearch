# Release 1.0 WP16 — Full Validation, Integration & Acceptance — Authoritative Codex Prompt

## 0. Prompt Identity

**Release:** 1.0 — Market Data Foundation
**Work package:** WP16 — Full Validation, Integration & Acceptance
**GitHub issue:** #101
**Role:** Final technical acceptance gate for the cumulative Release 1.0 candidate
**Execution mode:** Evidence-first, repository-truth-first, no speculative repair, no Git/GitHub integration
**Success terminal:** `RELEASE 1.0 ACCEPTED`
**Failure terminal:** `RELEASE 1.0 WP16 BLOCKED`

This prompt is the authoritative execution contract for WP16. It validates the complete cumulative Release 1.0 candidate produced by WP01–WP15. It does **not** commit, push, create a PR, merge, close planning objects, tag a release, create a GitHub Release, or begin Release 1.1.

---

## 1. Mission

Prove that the cumulative Release 1.0 Market Data Foundation candidate is internally coherent, reproducible, correctly bounded, fully validated, and ready for a separately authorized Git/GitHub integration step.

WP16 must:

1. reconcile repository state against Release 1.0 authorities and all accepted work-package outcomes;
2. prove the candidate contains only authorized cumulative Release 1.0 changes;
3. validate the production dependency graph and architectural ownership;
4. validate the complete provider-backed historical market-data vertical slice;
5. validate provider-independent failure behavior;
6. validate configuration, dependency injection, Worker execution, and security properties;
7. run the full permanent test suite and canonical verification;
8. validate documentation alignment and repository-relative links;
9. prove formatting, whitespace, and working-tree integrity;
10. produce a final acceptance decision based on evidence;
11. stop before Git/GitHub integration or Release 1.0 closure.

WP16 is primarily a **validation and acceptance package**. Do not make implementation changes merely to obtain a passing result. If a material defect is discovered, stop and report the blocker with the minimum required corrective scope.

---

## 2. Governing Authorities

Read completely before mutation or validation conclusions:

1. `docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md`
2. `docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md`
3. all Release 1.0 authoritative work-package prompts and prompt-chat companions under:
   `docs/roadmap/release-1.0/prompts/`
4. all Release 1.0 unblock/reconciliation/authorization prompts under that directory;
5. WP02 provider assessment and provider decision artifacts;
6. the accepted cumulative production, test, architecture, and documentation state;
7. GitHub issue #101, read-only;
8. repository build/package/project/solution governance relevant to validation.

Where a prompt references an accepted predecessor result that is available in the current execution context, reconcile it against repository truth rather than blindly trusting prose.

### Authority precedence

Use this order when authorities appear to conflict:

1. explicit later human authorization or unblock authority;
2. Release 1.0 execution plan;
3. Release 1.0 file manifest;
4. current work-package prompt;
5. earlier work-package prompts;
6. current repository truth;
7. GitHub planning metadata.

Do not silently reconcile a material conflict. Record it and stop if it changes scope, semantics, or acceptance.

---

## 3. Expected Accepted Predecessor State

WP15 must already be complete.

Expected evidence from the accepted WP15 state includes:

- Release identity: **1.0 — Market Data Foundation**
- WP15 delta: documentation only
- WP15 changed exactly nine current-state documents
- Architecture.Tests: **13/13**
- Total permanent tests: **105/105**
- Production graph:
  - Domain → none
  - Application → Domain
  - Infrastructure → Application
  - Worker → Application, Infrastructure
- Production cycles: **0**
- canonical verification: PASS
- `git diff --check`: PASS
- `git diff --cached --check`: PASS
- staged files: 0
- unexpected files: 0
- WP16 not started
- Release 1.1 not started

Do not assume these counts or states if repository truth differs. Recalculate and report actual evidence.

---

## 4. Hard Scope Boundaries

### WP16 MAY

- read all repository files necessary for validation;
- read GitHub issue #101 and relevant Release 1.0 planning state without mutation;
- execute restore, build, format verification, tests, architecture tests, Worker execution, repository scans, link checks, diff checks, and security scans;
- create temporary local validation artifacts only when necessary;
- remove every temporary artifact before final assessment;
- use deterministic local HTTP test mechanisms already present in the repository;
- inspect Git metadata and working-tree state;
- report blockers and the minimum corrective package required.

### WP16 MUST NOT

- change production behavior;
- add or modify permanent tests;
- change architecture rules;
- modify documentation to repair WP15;
- change packages, projects, solution membership, build policy, scripts, workflows, or `.editorconfig`;
- change Git configuration;
- stage files;
- commit;
- create a branch;
- push;
- create or edit a PR;
- merge;
- close or edit issues/milestones;
- mutate Project fields/items;
- create tags or GitHub Releases;
- use real credentials;
- perform a live provider call unless an authority explicitly requires it;
- begin Release 1.0 Git/GitHub integration;
- begin Release 1.0 closure;
- begin Release 1.1.

If validation itself changes tracked files, restore only execution-generated changes and prove that no accepted candidate change was lost.

---

## 5. Initial Repository Preflight

Record:

- repository root;
- current branch;
- `HEAD`;
- `origin/main`;
- ahead/behind;
- staged files;
- tracked modifications;
- untracked files;
- ignored/generated artifacts relevant to validation;
- configured upstream;
- GitHub authentication identity, without exposing credentials.

Expected branch is `main` unless a later authority explicitly says otherwise.

Classify every visible working-tree item into:

- `EXPECTED GOVERNANCE`
- `WP02 AUTHORIZED`
- `WP03 AUTHORIZED`
- ...
- `WP15 AUTHORIZED`
- `UNBLOCK / RECONCILIATION AUTHORIZED`
- `EXPECTED GENERATED/IGNORED`
- `UNEXPECTED`

**Gate:** unexpected mutations, staged candidate files, an unrecognized branch/base, or material divergence from the accepted cumulative candidate are blockers unless an authority explicitly explains them.

Do not clean, stash, reset, stage, or commit the accepted cumulative working tree.

---

## 6. Authority / Manifest Reconciliation

Build a Release 1.0 candidate reconciliation matrix.

At minimum identify:

- governance artifacts;
- provider assessment/decision artifacts;
- Domain changes;
- Application changes;
- Infrastructure transport/client/normalization/source changes;
- DI/configuration changes;
- Worker changes;
- Domain/Application tests;
- Infrastructure/provider tests;
- Architecture tests;
- WP15 documentation changes;
- test-only package/project changes authorized by the WP13 DI unblock.

For every changed/untracked candidate file, answer:

1. Which WP or explicit unblock authority owns it?
2. Is its path authorized by the manifest or later authority?
3. Is it required for the accepted candidate?
4. Is it duplicated elsewhere?
5. Does it introduce later-release scope?

Required result:

- authorized candidate files: fully accounted for;
- unexpected files: 0;
- duplicate governed artifacts: 0;
- Release 1.1 implementation artifacts: 0.

Do not hardcode a candidate-file total unless repository reconciliation proves it.

---

## 7. Production Dependency Graph Validation

Prove the production graph from project references and executable architecture tests:

```text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Validate:

- four production projects;
- no forbidden reverse edges;
- no direct Worker → Domain project reference;
- no cycles;
- no provider-specific reference from Domain or Application;
- no HTTP transport reference from Domain or Application;
- Application ownership of acquisition contracts/results/failures;
- Infrastructure confinement of Twelve Data implementation types.

Architecture.Tests must execute successfully. The accepted WP14 baseline is 13 tests, but report actual repository truth.

---

## 8. Release 1.0 Capability Validation

Prove the implemented vertical slice is exactly the bounded Release 1.0 capability:

```text
Worker
→ IResearchUseCase
→ ResearchUseCase
→ IObservationSource
→ TwelveDataObservationSource
→ TwelveDataClient
→ Twelve Data /time_series transport boundary
→ TwelveDataTimeSeriesNormalizer
→ PriceObservation / ObservationSeries / MeanPrice
→ provider-independent outcome
→ Worker presentation
```

Validate that the implementation provides:

- one selected historical-data provider: Twelve Data;
- daily historical observations;
- provider-independent Domain and Application boundaries;
- Infrastructure-owned HTTP, provider DTOs, authentication, normalization, validation, and failure mapping;
- one-shot Worker execution.

Prove that the candidate does **not** implement or claim as current:

- storage/database persistence;
- caching;
- streaming/live feeds;
- runtime provider selection;
- provider fallback;
- trading/order execution;
- portfolio behavior;
- plugins;
- AI/ML;
- cloud/production deployment;
- Release 1.1 scope.

---

## 9. Provider Request Contract Validation

Inspect the accepted Twelve Data request construction.

Prove the bounded request contract remains:

- endpoint: `/time_series`;
- `symbol` present and safely encoded;
- `interval=1day`;
- requested output size represented through the accepted parameter;
- `adjust=splits`;
- authentication remains header-based;
- API key is absent from URI/query;
- no unauthorized query parameters;
- no credential logging.

Do not perform a live provider request merely to validate this contract.

---

## 10. Normalization Semantics Validation

Prove the accepted deterministic normalization semantics:

- canonical price field: `close`;
- date parsing: exact `yyyy-MM-dd`;
- culture: invariant;
- timezone source: `meta.exchange_timezone`;
- local anchor: `00:00:00`;
- exchange offset resolved for that local date;
- no UTC conversion before `PriceObservation` construction;
- observations sorted by absolute instant ascending;
- duplicate instants rejected;
- missing/malformed/non-positive close rejected;
- invalid/unresolvable/ambiguous time evidence handled deterministically;
- no fallback to open/high/low/volume or machine-local timezone;
- empty normalized values remain governed by the accepted downstream validation policy.

Use permanent tests as primary evidence. Temporary probes are allowed only for a material uncovered acceptance question and must be removed.

---

## 11. Failure Model Validation

Prove the provider-independent source failure vocabulary remains:

- `UnsupportedTarget`
- `InsufficientObservations`
- `SourceUnavailable`
- `AccessDenied`
- `UsageLimitReached`
- `InvalidSourceResponse`

Prove `ResearchFailure` additionally includes:

- `InvalidRequest`

Validate:

- transport/provider mechanics are mapped inside Infrastructure;
- `ResearchUseCase` propagates the six source failures one-to-one;
- invalid requests remain Application-owned;
- raw HTTP codes/provider DTOs do not leak into Application or Domain;
- defensive unknown-state behavior has not been weakened.

Use existing permanent tests and source inspection.

---

## 12. Configuration and DI Validation

Validate the accepted composition model:

### Application

- `IResearchUseCase → ResearchUseCase`
- transient lifetime

### Infrastructure

- explicit `TwelveDataConfiguration`;
- required configuration key: `TwelveData:ApiKey`;
- deterministic failure when required configuration is absent;
- singleton `HttpClient`;
- base address: `https://api.twelvedata.com/`;
- singleton `TwelveDataClient`;
- singleton `IObservationSource → TwelveDataObservationSource`;
- no active deterministic observation source;
- no provider call during construction/resolution.

### Worker

- composes through `AddApplication` and `AddInfrastructure`;
- resolves only `IResearchUseCase` as its business entry point;
- does not manually construct provider implementation types.

Use the real Microsoft DI container tests established by WP13. Do not add another DI mechanism.

---

## 13. Worker Acceptance Validation

Validate the one-shot Worker behavior without real provider credentials or a live network dependency.

Use the deterministic/offline mechanism already authorized by the accepted test architecture where possible.

Prove:

- configuration is read through the accepted composition boundary;
- one research request is executed;
- successful result presentation is deterministic for the supplied controlled input;
- process completes rather than entering a hosted/background loop;
- no provider-specific DTO or HTTP mechanic leaks into Worker business flow.

If the current Worker can only run against a real credential in its default executable path, do **not** invent or expose a credential. Use existing tests/source evidence and report the limitation honestly.

---

## 14. Security / Credential Validation

Perform targeted scans and evidence review for:

- committed API keys;
- real credential literals;
- secrets in URI/query;
- secrets in logs/output;
- secrets in documentation examples;
- `.env`/secret artifacts accidentally included in candidate state;
- authenticated URLs exposed in reports;
- live-provider test dependencies.

Required outcome:

- real credentials: 0;
- committed credentials: 0;
- credential-bearing request URIs: 0;
- credential logging: 0;
- permanent tests requiring a real provider call: 0.

Placeholders are permitted when clearly non-secret.

Do not print secret values even if discovered. Report path/category only and stop if a real secret is found.

---

## 15. Permanent Test Suite Validation

Run all four permanent test projects directly.

Expected accepted baseline before WP16:

| Suite | Accepted predecessor count |
|---|---:|
| Domain.Tests | 11 |
| Application.Tests | 16 |
| Infrastructure.Tests | 65 |
| Architecture.Tests | 13 |
| **Total** | **105** |

These are predecessor expectations, not permission to fake or force counts.

Record actual:

- passed;
- failed;
- skipped;
- total;
- warnings/errors affecting execution.

Acceptance requires:

- all permanent tests pass;
- failed = 0;
- unexpected skipped tests = 0;
- no temporary tests remain.

---

## 16. Restore and Build Validation

Run from repository root:

```powershell
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
```

Record:

- exit status;
- warnings;
- errors;
- project failures if any.

Acceptance requires zero build errors.

Warnings must be classified. Do not silently ignore a new warning that indicates candidate drift or correctness risk.

---

## 17. Canonical Verification

Run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Record each phase actually reported by the script, including:

- restore;
- format verification;
- build;
- test execution;
- final exit status.

Acceptance requires canonical verification PASS.

Do not modify `eng/verify.ps1` to make the candidate pass.

---

## 18. Documentation Acceptance Validation

Revalidate the WP15 current-state documentation against implementation truth.

At minimum verify:

- Release identity is 1.0 — Market Data Foundation;
- project graph is correct;
- Twelve Data is Infrastructure-owned;
- Application contracts remain provider-independent;
- DI composition is accurate;
- Worker lifecycle is one-shot;
- normalization semantics are accurate;
- failure vocabulary is accurate;
- current test responsibilities/counts are not stale;
- all 13 executable architecture rules are represented without inventing incidental enforcement;
- future capabilities remain future-facing.

Run a targeted stale-reference scan for material current-state contradictions.

Do not edit documentation in WP16. Any material documentation defect is a blocker or a narrowly scoped follow-up requiring separate authority.

---

## 19. Repository-Relative Link Validation

Validate repository-relative Markdown links in:

- the nine WP15-changed current-state documents;
- Release 1.0 execution plan and file manifest;
- Release 1.0 governed prompt set where practical and deterministic.

Record:

- files scanned;
- links checked;
- broken local links.

Acceptance requires no material broken repository-relative link in the Release 1.0 acceptance surface.

Do not fail on external-link reachability unless an authority explicitly requires network validation.

---

## 20. Diff / Whitespace / Formatting Validation

Run:

```powershell
git diff --check
git diff --cached --check
```

Also verify:

- staged files remain 0;
- WP16 created no tracked delta;
- no temporary validation artifact remains;
- no broad line-ending normalization occurred;
- accepted cumulative candidate files remain present.

Informational checkout line-ending notices may be recorded, but a real whitespace error is a blocker.

---

## 21. Git / GitHub Read-Only Validation

Read-only inspect:

- issue #101 exists and represents WP16;
- milestone #41 remains the Release 1.0 milestone;
- Release 1.0 WP01–WP16 planning remains coherent;
- no Release 1.1 implementation/planning was newly created by WP16;
- no PR/tag/GitHub Release was created by WP16.

Do not close issue #101 or milestone #41.

WP16 technical acceptance is distinct from Git/GitHub integration and post-merge closure.

---

## 22. Integration Readiness Assessment

If every validation gate passes, declare the cumulative working tree an **accepted Release 1.0 candidate** suitable for a separately authorized Git/GitHub integration prompt.

The acceptance decision must explicitly state:

- candidate technically accepted: YES/NO;
- candidate scope reconciled: YES/NO;
- permanent tests: actual count;
- architecture tests: actual count;
- canonical verification: PASS/FAIL;
- unexpected files: count;
- staged files: count;
- production dependency drift: count;
- Release 1.1 started: YES/NO;
- Git integration performed: NO;
- GitHub integration performed: NO.

WP16 must not itself transport the candidate into Git history.

---

## 23. Blocker Policy

Stop and return `RELEASE 1.0 WP16 BLOCKED` when any of these occurs:

- authority conflict affecting candidate semantics or scope;
- unexpected candidate file;
- unauthorized package/project/reference/build change;
- production dependency graph drift;
- provider leakage into Domain/Application;
- material Release 1.0 behavior mismatch;
- failed permanent test;
- unexpected skipped test;
- build error;
- canonical verification failure;
- material documentation contradiction;
- broken acceptance-critical repository link;
- secret/credential exposure;
- staged candidate state that violates the acceptance contract;
- temporary validation artifact cannot be removed;
- evidence requires unauthorized implementation change.

Do not repair a material defect unless this prompt explicitly authorizes the exact repair. Instead report:

1. blocker ID;
2. observed evidence;
3. affected authority;
4. minimum corrective scope;
5. whether a new unblock prompt is required.

---

## 24. Required Execution Report

Produce a report titled:

```text
# Release 1.0 WP16 — Full Validation, Integration & Acceptance Execution Report
```

Use these sections:

1. Executive Summary
2. Authorities Reviewed
3. Initial Repository State
4. WP15 Predecessor Gate
5. Candidate / Manifest Reconciliation
6. Cumulative Release 1.0 Change Classification
7. Production Dependency Graph Evidence
8. Architecture Enforcement Evidence
9. Market Data Vertical-Slice Evidence
10. Provider Request Contract Evidence
11. Normalization Semantics Evidence
12. Failure Model Evidence
13. Configuration / DI Evidence
14. Worker Acceptance Evidence
15. Security / Credential Evidence
16. Restore Evidence
17. Build Evidence
18. Permanent Test Evidence
19. Canonical Verification
20. Documentation Acceptance Evidence
21. Link Validation
22. Diff / Whitespace / Formatting Validation
23. Git / GitHub Protection
24. Scope Protection
25. Findings / Observations
26. Acceptance Matrix
27. Final Repository State
28. Final Decision
29. Next Authorized Action

Use concrete counts, commands, hashes/commit IDs where relevant, and actual repository truth.

---

## 25. Acceptance Matrix

The final report must explicitly assess at least:

| Requirement | Result |
|---|---|
| WP15 predecessor gate | PASS/FAIL |
| Candidate fully reconciled | PASS/FAIL |
| Unexpected files | count |
| Production graph | PASS/FAIL |
| Production cycles | count |
| Provider leakage | count |
| Provider request contract | PASS/FAIL |
| Normalization semantics | PASS/FAIL |
| Failure mapping | PASS/FAIL |
| Configuration / DI | PASS/FAIL |
| Worker acceptance | PASS/FAIL |
| Real credentials | count |
| Restore | PASS/FAIL |
| Build errors | count |
| Domain.Tests | x/x |
| Application.Tests | x/x |
| Infrastructure.Tests | x/x |
| Architecture.Tests | x/x |
| Total permanent tests | x/x |
| Canonical verification | PASS/FAIL |
| Material stale documentation claims | count |
| Broken acceptance-surface local links | count |
| `git diff --check` | PASS/FAIL |
| `git diff --cached --check` | PASS/FAIL |
| Staged files | count |
| Temporary artifacts | count |
| Git/GitHub integration performed | NO |
| Release 1.1 started | NO |

---

## 26. Success Criteria

WP16 succeeds only when all of the following are true:

- all Release 1.0 candidate files are authorized and reconciled;
- unexpected mutations = 0;
- production graph matches the accepted architecture;
- cycles = 0;
- provider mechanics remain confined to Infrastructure;
- historical market-data request semantics match accepted authority;
- normalization semantics match accepted authority;
- provider-independent failures are complete and correctly propagated;
- DI/configuration behavior matches accepted authority;
- Worker remains correctly bounded;
- security/credential checks pass;
- restore succeeds;
- build errors = 0;
- all permanent tests pass;
- architecture tests pass;
- canonical verification passes;
- WP15 documentation remains materially aligned;
- acceptance-surface repository links are valid;
- both diff checks pass;
- staged files = 0;
- temporary artifacts = 0;
- no Git/GitHub integration action occurred;
- Release 1.1 was not started.

If successful, the exact final terminal line must be:

```text
RELEASE 1.0 ACCEPTED
```

Do not append qualifiers to that terminal.

---

## 27. Failure Criteria

If any mandatory criterion fails, the final decision must not claim partial acceptance.

The exact final terminal line must be:

```text
RELEASE 1.0 WP16 BLOCKED
```

Before that terminal, identify the minimum separately authorized correction needed.

---

## 28. Next Authorized Action After Success

A successful WP16 **does not close Release 1.0**.

It authorizes only the creation/execution of a separately governed **Release 1.0 Git/GitHub integration step** that transports the exact accepted candidate into:

- an integration branch;
- a single reconciled commit unless later authority says otherwise;
- a pushed remote branch;
- an open PR for human review.

That later integration step must preserve the exact WP16-accepted candidate and must not merge without explicit human authorization.

After the integration PR is merged, a separate **Release 1.0 closure gate** must validate the merged `main`, reconcile planning/release governance, and only then may it emit:

```text
RELEASE 1.0 CLOSED
```

The Release 1.0 closure gate—not WP16 and not the Git/GitHub integration step—is the only Release 1.0 step permitted to explicitly authorize commencement of Release 1.1 after all closure criteria pass.

Do not create Release 1.1 planning or implementation in WP16.

---

## 29. Execution Discipline

Use evidence over assumption.

Do not:

- infer success from prior reports when the repository can be checked;
- hide warnings or observations;
- widen scope to make validation easier;
- alter accepted semantics;
- mutate Git/GitHub state;
- claim live-provider proof without actually performing an authorized live call;
- claim closure when only technical acceptance has been achieved.

The desired outcome is not merely a green command. The desired outcome is a defensible, reproducible acceptance record for the exact Release 1.0 candidate.

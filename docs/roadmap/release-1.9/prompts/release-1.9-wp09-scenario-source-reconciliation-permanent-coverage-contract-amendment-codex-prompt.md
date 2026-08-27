# Release 1.9 — WP09 Scenario-Source Reconciliation / Permanent-Coverage Contract Amendment

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **very narrow documentation-only contract amendment** for Release 1.9 WP09 / issue #234.

It reconciles one proven mismatch between the binding WP09 permanent-integration contract and frozen production behavior:

- `PI-EMPTY` cannot be produced by the existing Replay producer because terminal replay with zero observations returns without publishing an Empty envelope.
- replay-source failure returns before `PipelineExecutionUseCase` / `VisualizationReadModelUseCase`, so the existing Replay producer cannot publish the contract's replay-path `PI-FAILED` envelope.

WP09 is a permanent test/architecture package. This amendment MUST NOT introduce production behavior merely to manufacture test states.

No production, test, Python, package, schema, Replay, Worker, GitHub, or WP10+ mutation is authorized.

---

# Binding predecessor evidence

Preserve:

- #233 Closed / Done.
- #234 Open / Backlog.
- #234 Project item `PVTI_lAHOCAzBgs4BfsiAzg33XcY`.
- Release 1.9 / P1 / Testing.
- milestone #58 Open.
- build 0 warnings / 0 errors.
- WP08 focused 18/18.
- Python WP05–WP07 predecessors 13/13.
- accepted pre-WP09 .NET baseline 327/327.
- exact WP09 delta +12 .NET, +4 Python, +16 total.
- expected post-WP09 .NET total 339/339.

Do not change these.

---

# Binding artifact to amend

Read completely:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

Also read the exact accepted predecessor contracts that define:
- Replay producer behavior;
- pipeline execution/composition;
- visualization read-model composition;
- canonical Empty state;
- canonical Failed state;
- WP05 parser;
- WP06 frame;
- WP07 presentation;
- WP08 frozen boundary.

Do not infer symbol/path names where repository evidence can establish them.

---

# Objective

Preserve **four permanent WP09 scenarios** while assigning each state to the nearest existing governed production boundary that can factually produce it.

The reconciled scenario ownership must be:

## PI-READY
Source:
**real existing Replay → pipeline/application → visualization read model → canonical handoff chain**

No semantic change.

## PI-WARMUP
Source:
**real existing Replay → pipeline/application → visualization read model → canonical handoff chain**

No semantic change.

## PI-EMPTY
Source:
**existing canonical historical-composition / pipeline/read-model boundary that already owns deterministic Empty composition**

It MUST NOT require the Replay producer to publish an Empty envelope if production does not do so.

## PI-FAILED
Source:
**existing canonical historical-composition / pipeline/read-model boundary that already owns deterministic Failed composition**

It MUST NOT require replay-source failure to proceed through a pipeline path production currently short-circuits.

The artifact must name the exact existing production boundary/symbols for PI-EMPTY and PI-FAILED after repository inspection.

If no existing governed production boundary can factually construct one of these states without new semantics, STOP rather than inventing one.

---

# Coverage-strength rule

This amendment is a **source-boundary correction, not an acceptance reduction**.

All four states remain permanent integration coverage.

For PI-EMPTY and PI-FAILED, require the deepest existing governed composition path that owns those canonical states, followed by the same downstream contract surfaces authorized by WP09 where feasible:

canonical composition/read-model
→ canonical handoff/envelope representation as existing contracts permit
→ WP05 parser
→ WP06 frame
→ WP07 presentation/Streamlit-facing projection.

Do not mock the semantic state if an existing real composer/use case can produce it.

Do not require an upstream Replay step that cannot produce it.

---

# Exact scenario assertions

Preserve all existing state assertions from the binding WP09 contract unless they specifically assert the now-invalid Replay source.

## PI-EMPTY must still prove
- canonical Empty state;
- zero observation/count semantics;
- exact window/latest unavailable semantics;
- canonical metadata;
- governed handoff/envelope validity where applicable;
- WP05 parse;
- WP06 Empty projection;
- WP07 exact section structure/order;
- Streamlit-facing Empty state;
- no provider/SQLite bypass.

## PI-FAILED must still prove
- canonical Failed state;
- exact existing failure/status semantics;
- factual metadata;
- distinction between semantic failure and transport warning;
- governed handoff/envelope validity where applicable;
- WP05 parse;
- WP06 Failed projection;
- WP07 exact section structure/order;
- Streamlit-facing Failed state;
- no provider/SQLite bypass.

Do not invent a new failure kind, exception, status, field, or transport behavior.

---

# Replay-chain terminology correction

Amend every statement that currently says or implies **all four** scenarios must originate from Replay.

Replace it with a precise two-tier permanent-integration model:

### Replay-origin permanent scenarios
- PI-READY
- PI-WARMUP

### Canonical-composition-origin permanent scenarios
- PI-EMPTY
- PI-FAILED

The overall permanent coverage remains four scenarios.

---

# Cross-language coverage

Preserve the existing WP09 cross-language split.

Do not authorize a new .NET→Python bridge.

Do not repurpose the WP08 acceptance-only probe unless already independently authorized.

PI-EMPTY/PI-FAILED Python coverage may consume deterministic canonical handoff/envelope input produced or represented exactly as allowed by the binding WP09 artifact.

No duplicated schema semantics.

---

# Path authority

Do not broaden path authority.

Preserve the exact WP09 test paths already authorized by the binding artifact.

Shared WP02–WP08 tests remain frozen/read-only except where the binding artifact already explicitly authorizes additive changes.

No production path becomes writable through this amendment.

---

# Test-count preservation

Preserve exactly:

- +12 .NET
- +4 Python
- +16 total
- pre-WP09 .NET 327/327
- expected post-WP09 .NET 339/339.

If scenario redistribution requires moving tests between the already-authorized WP09 .NET test files/projects, permit only a redistribution that:
1. remains inside existing WP09 path authority;
2. preserves +12 .NET total;
3. is explicitly documented.

Do not add tests beyond the fixed totals.

---

# Architecture/security preservation

All existing WP09 no-bypass rules remain binding:

- no presentation/UI direct SQLite access;
- no presentation/UI direct provider access;
- no unauthorized Infrastructure dependency;
- Worker producer / Streamlit consumer ownership preserved;
- canonical JSON handoff boundary preserved;
- no Release 1.8 endpoint expansion;
- no alternate transport.

This amendment changes no architecture rule.

---

# Regression/residue preservation

Preserve all existing WP09 gates, including:
- focused WP09;
- WP08 18/18;
- full .NET expected 339/339;
- Python predecessor suites;
- WP09 Python 4/4;
- build 0 warnings / 0 errors;
- Streamlit 1.61.1;
- `pip check`;
- security/no-bypass audit;
- full owned-process/listener/handoff/database/runtime residue audit.

---

# Exact documentation mutation

Preferred approach: modify exactly the binding WP09 contract:

`docs/roadmap/release-1.9/RELEASE_1.9_WP09_PERMANENT_INTEGRATION_ARCHITECTURE_TEST_CONTRACT_MANIFEST_PATH_AUTHORITY.md`

Add a clearly identified **Scenario-Source Reconciliation Amendment** section and correct the affected scenario/source language throughout so there is no contradiction.

No second artifact is needed unless repository governance forbids direct amendment. If direct amendment is forbidden, STOP and report the exact required amendment path rather than inventing it.

---

# Required factual explanation

The amended contract must explicitly record:

1. why Replay cannot publish PI-EMPTY under frozen behavior;
2. why replay-source failure cannot publish PI-FAILED under frozen behavior;
3. why changing production for these tests is outside WP09;
4. which exact existing governed boundary owns Empty;
5. which exact existing governed boundary owns Failed;
6. why the reassignment preserves permanent coverage strength.

---

# Stop conditions

STOP with zero mutation if:
- no existing canonical composition boundary owns Empty;
- no existing canonical composition boundary owns Failed;
- producing either state requires new production semantics;
- the required downstream projection would require a new transport/bridge;
- direct amendment of the binding artifact is forbidden;
- reconciliation would require changing architecture rules or total test counts.

Do not weaken assertions to force completion.

---

# Mutation boundary

Allowed:
- one binding documentation artifact correction/amendment.

Forbidden:
- production;
- tests;
- Python;
- Replay;
- Worker;
- packages;
- schema;
- GitHub;
- Project;
- WP10+.

---

# Required completion report

Report:

## Reconciled ownership
- PI-READY → exact Replay-origin boundary.
- PI-WARMUP → exact Replay-origin boundary.
- PI-EMPTY → exact canonical composition boundary/symbol.
- PI-FAILED → exact canonical composition boundary/symbol.

## Coverage preservation
Confirm four permanent scenarios remain and downstream semantic/presentation assertions are preserved.

## Counts
327 baseline; +12 .NET; +4 Python; +16 total; 339 expected .NET.

## Artifact
Exact changed path.

## Mutations
`WP09 SCENARIO-SOURCE RECONCILIATION MUTATIONS: ZERO production/test/GitHub mutations; one binding documentation artifact amended`

## Resume marker
`WP09 SCENARIO-SOURCE CONTRACT RECONCILED — CONSOLIDATED TERRA IMPLEMENTATION MAY RESUME`

---

# Terminal markers

Success:

`RELEASE 1.9 WP09 SCENARIO-SOURCE RECONCILIATION AND PERMANENT-COVERAGE CONTRACT AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP09 SCENARIO-SOURCE RECONCILIATION AND PERMANENT-COVERAGE CONTRACT AMENDMENT BLOCKED`

Do not emit COMPLETE unless exact existing Empty/Failed ownership is identified and the four-scenario contract is implementation-ready without production semantic changes.

# Release 1.9 WP09 — Permanent Integration and Architecture Test Contract

Status: normative, documentation-only authority for WP09 issue #234. This
document authorizes no implementation, test execution, GitHub mutation, issue
closure, WP08 change, or WP10+ work. A fresh GPT-5.6 Terra implementation
authority is required after this contract is accepted.

## Binding predecessor and objective

WP09 consumes the accepted WP04 read model, WP05 local atomic-JSON handoff,
WP06 `VisualizationFrame`, WP07 factual presentation projection, and the
accepted Release 1.9 definition, execution plan, and file manifest. Its
permanent proof uses the existing governed source boundary for each state,
followed by one deterministic downstream path:

`governed source/composition → existing canonical five-stage pipeline → WP04
read model → WP05 atomic JSON → WP05 parser → WP06 frame → WP07
sections/render inputs`.

The suite proves the path and its ownership boundaries; it does not add a
parallel pipeline, a production supervisor, a new transport, a provider, a
database UI, a browser harness, or Release 1.9 feature scope beyond the
accepted visualization surface.

## Scenario-Source Reconciliation Amendment

Frozen Replay behavior cannot publish every presentation state: in
`AIQuantTradingResearch.Worker.SimulatedLiveVisualizationExecution.Execute`,
terminal replay with zero observations returns before publication, and a replay
source failure returns before `IPipelineExecutionUseCase` or
`VisualizationReadModelUseCase` is invoked. Changing either behavior solely to
manufacture WP09 test states is outside this test-only work package.

The existing canonical historical-composition boundary already owns both
states. `AIQuantTradingResearch.Worker.PipelineExecution.Execute`
(`PipelineExecutionConfiguration`) invokes the real
`IPipelineExecutionUseCase.Execute(PipelineRequest)` and then
`VisualizationReadModelUseCase.PublishHistorical(string, PipelineExecutionResult)`.
That use case maps successful empty `HistoricalPresentationInputs` to
`VisualizationPresentationState.Empty` and a failed `PipelineExecutionResult`
to `VisualizationPresentationState.Failed`, retaining the existing safe
failure category/message policy. The existing
`VisualizationReadModelFilePublishingStore` and
`VisualizationReadModelFilePublisher` remain the governed atomic handoff for
either historical model.

This is a source-boundary correction, not a coverage reduction. Ready and
WarmUp retain their real Replay origin. Empty and Failed use the deepest
existing historical pipeline/composition/read-model path that factually owns
those states, then use the same existing handoff, parser, frame, and WP07
projection boundaries. No new bridge, transport, semantic state, or production
behavior is authorized.

## Permanent integration scenarios

The authoritative scenario set is exactly four deterministic scenarios. Each
scenario uses fixed governed synthetic input, the existing canonical execution
composition, an isolated temporary handoff path, and a bounded cleanup scope.
`PI-READY` and `PI-WARMUP` use an explicit Replay seed; `PI-EMPTY` and
`PI-FAILED` use the existing historical-composition source boundary described
above. No scenario may invoke the WP08 lifecycle argument, Windows
process-group helper, CTRL_BREAK helper, Streamlit process launcher, or WP08
Python probe.

| ID | Canonical input/state | Required path and assertions |
| --- | --- | --- |
| `PI-READY` | Two ordered Replay observations; accepted `Ready` envelope | Use `SimulatedLiveVisualizationExecution.Execute` with the real Replay source and canonical pipeline/read-model/handoff chain; read the produced `aiq-visualization-read-model-v1` JSON through the existing WP05 parser, project the existing WP06 frame, and assert exact Replay revision identity, source mode/authority/target, ordered points, latest, counts/window capacity, feature metadata, factual statuses, and five WP07 sections. |
| `PI-WARMUP` | One ordered Replay observation; accepted `WarmUp` envelope | Use the same real Replay chain and prove no fabricated feature value, exact observed/required values, `Unavailable` optional facts where absent, and exact `WarmUp` state. |
| `PI-EMPTY` | Existing successful historical pipeline result with empty `HistoricalPresentationInputs` | Use `PipelineExecution.Execute` → real `IPipelineExecutionUseCase.Execute` → `VisualizationReadModelUseCase.PublishHistorical(target, result)` → existing publishing store/file publisher. Prove canonical `Empty`, no latest or fabricated point, exact zero/window invariants, `Unavailable` optional facts, governed envelope/parser/frame/sections, and no provider/SQLite presentation bypass. |
| `PI-FAILED` | Existing failed historical `PipelineExecutionResult` from canonical materialization/pipeline composition | Use `PipelineExecution.Execute` → real `IPipelineExecutionUseCase.Execute` → `VisualizationReadModelUseCase.PublishHistorical(target, result)` → existing publishing store/file publisher. Prove canonical `Failed`, the existing safe failure category/status, semantic-failure versus transport-warning separation, governed envelope/parser/frame/sections, and no provider/SQLite presentation bypass. |

`PI-STALE` is not a fifth end-to-end scenario: the accepted contracts do not
define a new wall-clock threshold. Stale retention remains covered by the
existing WP06/WP07 focused tests and is asserted only where the permanent
scenario consumes an already-authoritative retained frame. No timing-based
stale behavior may be invented.

Each scenario has one success assertion surface and one cleanup assertion
surface. The test must assert that the temporary handoff directory, atomic
temporary siblings, isolated database/WAL/SHM/journal sidecars, and owned
process/listener resources are absent after disposal. No persistent evidence
file is created.

## Exact layer and ownership rules

1. Domain remains unaware of visualization, JSON, Streamlit, files, processes,
   providers, and test orchestration.
2. Application owns technology-neutral pipeline/read-model contracts and
   orchestration. It does not reference Python, Streamlit, filesystem paths,
   SQLite, process APIs, or presentation rendering.
3. Infrastructure owns persistence-backed read-model composition and the
   atomic JSON publisher. The publisher is the only permanent handoff writer;
   atomic replacement, revision acceptance, and existing failure behavior are
   unchanged.
4. Worker composition may invoke the existing canonical execution and publish
   through the existing Infrastructure service. It does not launch or control
   Streamlit and does not expose a new listener or transport.
5. Python presentation reads only the governed handoff through
   `visualization_read_model.py`, then delegates to the existing frame and
   WP07 section projections. It must not import provider/SQLite access, invoke
   the Worker, launch a process, or reconstruct the read model.
6. Streamlit remains a read-only peer. The permanent Python test does not
   require a browser, a fixed port, a live server, or the WP08 acceptance
   harness.
7. `python/validation/`, the Release 1.8 capability endpoint, the WP08 probe,
   WP08 lifecycle tests, and Windows signal helpers are not permanent WP09
   integration surfaces.
8. No test may prove success with fabricated UI output, a copied database, a
   hand-authored production envelope in place of the canonical producer, or a
   second parser/frame/pipeline implementation.

## Permanent test paths and ownership

Only these new dedicated paths are authorized:

| Exact path | Owner | Authorized concern |
| --- | --- | --- |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationPermanentIntegrationTests.cs` | WP09 | The four `PI-*` end-to-end scenarios, canonical producer-to-handoff proof, parser/frame/section correlation, bounded cleanup, and no bypass assertions. |
| `tests/AIQuantTradingResearch.Architecture.Tests/VisualizationBoundaryRulesTests.cs` | WP09 | Static architecture rules 1–8 above, using repository source/project inspection without executing production processes. |
| `python/presentation/test_realtime_financial_visualization.py` | WP09 | Deterministic Python parser-to-frame-to-WP07 presentation-input tests for the four states and exact functional values; no pytest or external framework. |

The shared predecessor paths remain owned by their original work packages and
are read-only for WP09: `VisualizationReadModelContracts.cs`,
`VisualizationReadModelUseCase.cs`, `VisualizationReadModelFilePublisher.cs`,
`visualization_read_model.py`, `realtime_financial_visualization.py`, and the
WP05/WP06/WP07 focused tests. WP09 may consume their public behavior and add
no shared-file symbols unless a later authority explicitly amends the path
allowlist. `test_visualization_read_model.py`, the WP06/WP07 dedicated Python
tests, `SimulatedLiveVisualizationExecutionTests.cs`, and all WP08 paths are
not repurposed.

The Python test uses `unittest`, fixed in-memory contract-shaped inputs, and
temporary files only when parser behavior requires a file. The input builder
must mirror the accepted envelope field names and values exactly; it is not a
second production schema and cannot be used as the .NET end-to-end proof.

## Exact assertion and test-count contract

The implementation must add exactly four .NET integration tests, eight
architecture-rule tests (one for each rule above), and four Python tests (one
for each `PI-*` state). The WP09 delta is therefore exactly:

`+12 .NET tests, +4 Python tests, +16 total tests`.

The expected accepted pre-WP09 predecessor baseline is 327/327 .NET, consisting
of Domain 11/11, Application 125/125, Infrastructure 178/178, and Architecture
13/13. The WP09 implementation target is 339/339 .NET, consisting of the 327
predecessor tests plus four integration and eight architecture tests. The Python target is
the predecessor WP05/WP06/WP07 suites plus exactly four new WP09 tests. A
different discovered count is a reconciliation blocker, not permission to
adjust the contract or weaken assertions.

The four integration tests must assert the same revision identity and source
facts at the produced envelope, parsed envelope, frame, and section-input
boundaries. They must assert ordered oldest-to-newest observations, decimal
text/value fidelity, state, feature availability, pipeline status, data
quality, idempotency, and the exact five WP07 section/row labels where the
state permits them. Transport warnings remain separate and never become a
backend state or data-quality result.

## Regression, security, and residue gates

The later Terra implementation authority must run, without changing pins or
adding tools:

- the four new integration tests and eight architecture tests;
- the new four-test Python suite with the existing no-pytest convention;
- WP05 Python 3/3, WP06 Python 6/6, WP07 semantic 2/2, and WP07 presentation
  2/2;
- Application, Domain, Infrastructure, and Architecture suites;
- full .NET regression from the accepted 327/327 baseline, expected 339/339
  after exactly the authorized +12 .NET test delta;
- build 0/0, Streamlit 1.61.1 smoke, exact dependency pins, and `pip check`;
- schema-v4 preservation, selection-record coverage, documentation/link/
  format/security/diff checks, and repository scope audit;
- zero owned Worker/Streamlit processes or listeners, zero temporary handoff
  and database sidecars, and no persistent WP09 evidence output.

Any predecessor regression, altered production semantic, package/schema change,
unowned process termination, residue, or count mismatch blocks completion.

## GitHub completion boundary and exclusions

WP09 implementation completion may transition only issue #234’s already
identified Project #2 item to the established completed status and close issue
#234 after all gates and read-back pass. Milestone #58 remains open until its
own later authority. Issue #235 and all later work packages remain Open/Backlog.
No Project item is created or deleted, no taxonomy is changed, and no other
issue, milestone, Release, tag, PR, or Release 1.10+ state is touched.

This documentation authority itself performs no GitHub mutation. It creates
only this Markdown artifact; it does not modify production code, tests,
packages, Python state, schema, Git state, or the manifest.

`WP09 PERMANENT INTEGRATION/ARCHITECTURE CONTRACT AUTHORITY MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP09 PERMANENT INTEGRATION/ARCHITECTURE TEST CONTRACT AND PATH AUTHORITY DEFINED — IMPLEMENTATION REQUIRES FRESH TERRA AUTHORITY`

`RELEASE 1.9 WP09 PERMANENT INTEGRATION AND ARCHITECTURE TEST CONTRACT + MANIFEST/PATH AUTHORITY COMPLETE`

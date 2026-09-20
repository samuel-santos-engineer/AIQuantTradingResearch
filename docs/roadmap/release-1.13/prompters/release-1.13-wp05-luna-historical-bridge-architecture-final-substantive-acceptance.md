# Release 1.13 WP05 --- Historical Bridge Architecture Final Substantive Acceptance

## Selected model

**Execution model: GPT-5.6 Luna**

-   **GPT-5.6 Luna** owns final substantive acceptance of the WP05
    historical bridge architecture.
-   **GPT-5.6 Terra** owns later implementation and lifecycle execution
    only under separate authority.
-   **GPT-5.6 Sol** may provide supporting analysis only and may not
    override Luna's governance decision.

## Acceptance target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Pull request:

`#301`

Expected branch:

`docs/release-1.13-wp05-bridge-architecture`

Expected canonical predecessor / PR base lineage:

`cc96c638b3c255499412bd79aca3953cb2973902`

Expected candidate head:

`2387b8d466d723dd5213ecb9bb9aa20a871d3ab0`

Expected issue:

`#292` --- remains Open / Project Backlog during substantive acceptance.

Expected milestone:

`#64` --- remains Open.

The implementation of WP05 remains unauthorized under this acceptance
authority.

## Prior architecture result

The architecture candidate reported:

`RELEASE 1.13 WP05 EXTENDED F1 HISTORICAL BRIDGE ARCHITECTURE: PASS`

The selected mechanism is:

**additive bounded one-shot local Worker/stdio historical-query bridge**

The intended chain is:

`Streamlit` `-> bounded safe local invocation`
`-> Worker/host historical-query mode`
`-> HistoricalMarketDataReadService` `-> WP04 cache`
`-> IHistoricalMarketDataProvider`
`-> Vike only when governed acquisition is required`
`-> presentation-safe response` `-> Streamlit`

The existing legacy Worker/read-model path must remain valid and
independently usable.

## Exact candidate documentation scope

The candidate must contain exactly these three changed documentation
paths:

1.  `docs/roadmap/release-1.13/RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`
2.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
3.  `docs/roadmap/release-1.13/prompters/release-1.13-wp05-luna-extended-f1-historical-request-read-model-bridge-architecture-authority.md`

No production code, test code, dependency manifest, Docker/deployment
file, or root README change belongs in this candidate.

## Purpose

Perform a fresh, adversarial substantive review of the architecture
before it becomes canonical.

Do not accept the ADR merely because the prior architecture-authority
run reported PASS.

Inspect the actual repository, actual PR diff, actual ADR, actual
runtime topology, and relevant existing contracts.

The objective is to decide whether the selected bridge is sufficiently
complete, safe, bounded, implementable, and compatible with Azure Linux
F1 to serve as the frozen implementation architecture for WP05.

## Mandatory live-state verification

Before substantive review verify:

-   PR #301 exists;
-   PR is Open;
-   PR is not merged;
-   PR base is the expected canonical lineage;
-   PR head is exactly `2387b8d466d723dd5213ecb9bb9aa20a871d3ab0`;
-   branch is `docs/release-1.13-wp05-bridge-architecture`;
-   exactly three changed paths exist;
-   all three are authorized documentation paths;
-   root `README.md` is absent from the diff;
-   issue #292 remains Open / non-Done;
-   Project Release remains `1.13`;
-   milestone #64 remains Open;
-   WP06-WP08 remain unstarted/open;
-   no Release 1.13 tag or GitHub Release was created as part of this
    architecture candidate.

If the candidate head changed, inspect the change before proceeding. Any
substantive change after this acceptance begins must be included in the
final accepted head and revalidated.

## Mandatory repository inspection

Read the candidate ADR and compare it against live implementation
constraints.

At minimum inspect:

1.  `RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`;
2.  `RELEASE_1.13_FILE_MANIFEST.md`;
3.  the architecture authority record;
4.  merged WP01 contract;
5.  merged WP02 historical market-data contracts;
6.  WP04 cache contracts;
7.  WP04 `HistoricalMarketDataReadService`;
8.  WP04 atomic file cache;
9.  Vike provider;
10. current Worker entry point/composition root;
11. Worker command-line/stdin/stdout behavior;
12. existing one-shot JSON-over-stdio contract;
13. legacy visualization read-model publication;
14. `python/presentation/visualization_read_model.py`;
15. `python/presentation/realtime_financial_visualization.py`;
16. relevant Python integration code;
17. Dockerfile/container startup;
18. Python requirements;
19. .NET project/package declarations relevant to the proposed bridge;
20. architecture/no-bypass tests;
21. Release 1.12 F1 `/home` and runtime assumptions where still
    applicable.

Record the important evidence in the acceptance report.

## Acceptance Gate 1 --- Existing-model preservation

Prove the selected architecture is genuinely additive.

It must not require wholesale replacement of:

-   current Worker responsibilities;
-   legacy one-shot behavior;
-   existing legacy visualization publication;
-   existing Streamlit process;
-   existing System Health behavior;
-   WP04 Application ownership;
-   Infrastructure provider/cache ownership.

The new historical-query mode must be separable from existing Worker
modes/responsibilities.

The architecture must define enough mode discrimination that existing
invocation behavior cannot accidentally become historical-query
behavior.

## Acceptance Gate 2 --- WP04 remains authoritative

The architecture must preserve:

`historical query -> HistoricalMarketDataReadService -> cache -> provider`

It must not authorize:

-   direct Vike calls from Python;
-   direct Twelve Data calls from Python;
-   browser-to-provider calls;
-   a second freshness algorithm;
-   a second market-data cache authority;
-   provider-specific logic in Streamlit;
-   bypass of `HistoricalMarketDataReadService`.

The bridge is orchestration/serialization, not a competing market-data
subsystem.

## Acceptance Gate 3 --- Request contract

Verify the ADR freezes a sufficiently explicit provider-independent
request contract.

It must support exactly:

Symbols: - BTC/USD - ETH/USD

Intervals: - 1h - 4h - 1d

Ranges: - 1D - 7D - 30D - 90D

Review:

-   contract version;
-   canonical selection encoding;
-   bounded request size;
-   timeout/deadline semantics;
-   correlation identity only if justified;
-   malformed/unknown value rejection;
-   no provider URL/key/symbol;
-   no arbitrary filesystem path;
-   no arbitrary command fragments;
-   no arbitrary query language.

The future implementation must be able to map this contract
deterministically into the accepted WP02 request.

## Acceptance Gate 4 --- Response contract

Verify the ADR freezes enough presentation-safe output for WP05/WP06
without leaking internals.

Review:

-   contract version;
-   canonical requested selection;
-   availability/read state;
-   freshness/stale state;
-   canonical candle timestamps;
-   open/high/low/close/volume;
-   provider display identity;
-   last acquisition/validation/update UTC metadata;
-   empty-success distinction;
-   controlled failure categories;
-   response size bounds.

The response must exclude:

-   secrets;
-   authorization headers;
-   provider request URLs;
-   raw Vike payloads;
-   raw exception text;
-   stack traces;
-   internal filesystem paths;
-   database paths.

## Acceptance Gate 5 --- JSON / stdio discipline

For the one-shot stdio design, verify the ADR makes the process boundary
deterministic.

Require:

-   one bounded request;
-   one bounded machine-readable response;
-   explicit contract version;
-   UTF-8;
-   unambiguous UTC;
-   adequate decimal precision;
-   no NaN/Infinity;
-   stdout reserved for protocol output;
-   diagnostic logging separated from protocol output, normally stderr;
-   deterministic exit behavior;
-   malformed request cannot cause mixed logs/protocol output;
-   partial/truncated output is detected by Streamlit as bridge failure.

Ensure the architecture does not depend on fragile scraping of console
text.

## Acceptance Gate 6 --- Process invocation security

This is a critical gate.

Verify the future Streamlit implementation can invoke the local .NET
executable without shell interpolation.

Require architecture compatible with:

-   fixed executable identity/path under repository/container control;
-   argument-array or direct subprocess APIs;
-   canonical enumerated user selections only;
-   no `shell=True`-style command construction;
-   bounded stdin/request payload;
-   bounded stdout response;
-   bounded stderr capture/log handling;
-   timeout;
-   process termination/reaping on timeout;
-   no arbitrary path or executable selection from browser input.

If the ADR leaves command injection or process selection ambiguous,
correct the ADR before acceptance.

## Acceptance Gate 7 --- Timeout and cancellation

Verify a complete timeout chain exists conceptually:

`Streamlit invocation timeout` `-> host/bridge cancellation`
`-> HistoricalMarketDataRequest deadline/cancellation`
`-> provider cancellation`

Review what happens when:

-   caller disconnects;
-   Streamlit reruns;
-   subprocess exceeds timeout;
-   provider stalls;
-   cache I/O stalls;
-   process is terminated.

The design must avoid orphaned unbounded subprocesses.

It need not promise cancellation behavior that the current runtime
cannot actually guarantee; limitations must be stated truthfully.

## Acceptance Gate 8 --- Cross-process concurrency and duplicate acquisition

This is the most important architecture review gate.

WP04's `SemaphoreSlim` coalescing is process-local.

A one-shot design can create multiple Worker processes for identical
cache misses.

The ADR must explicitly resolve whether two or more simultaneous
processes can independently observe a miss/stale snapshot and call Vike.

Do not accept vague statements that "the cache handles concurrency."

Inspect the actual atomic cache and WP04 service.

The architecture must choose and justify one bounded policy, for
example:

-   demonstrate that limited duplicate acquisition is an explicitly
    accepted bounded property under F1/request constraints; or
-   specify a minimal local cross-process coordination primitive; or
-   specify another repository-native serialization mechanism.

Any coordination design must be:

-   local;
-   schema-free;
-   dependency-free where possible;
-   bounded;
-   recoverable after process death;
-   safe on Linux F1;
-   compatible with `/home`;
-   unable to deadlock indefinitely.

If file locking is selected, define lock ownership, acquisition timeout,
stale/dead-owner behavior, scope/keying, and atomic cache recheck after
lock acquisition.

Do not silently introduce Redis, broker, database lock table, or managed
service.

If this gate is unresolved, substantive acceptance is BLOCKED.

## Acceptance Gate 9 --- Atomic cache interaction

Verify the bridge does not weaken WP04 atomic cache semantics.

A query process must:

-   construct/use the governed service;
-   read the shared persistent cache location expected by deployment;
-   preserve cache-contract versioning;
-   preserve corrupt-cache handling;
-   preserve stale fallback;
-   preserve atomic replacement;
-   preserve bounded retention.

If multiple processes can write the same cache key, the architecture
must explain why the selected coordination/atomicity behavior remains
safe.

## Acceptance Gate 10 --- Streamlit rerun semantics

Verify the ADR distinguishes:

Bridge invocation may occur for: - initial Market Research request; -
BTC/USD vs ETH/USD change; - 1h/4h/1d change; - 1D/7D/30D/90D change; -
explicit governed refresh if retained.

Bridge invocation must not be caused by: - Plotly hover; - Plotly
zoom; - Plotly pan; - other client-only chart interactions.

Streamlit session state/memoization may preserve a last presentation
result, but it cannot become the market-data freshness authority.

Review how ordinary Streamlit reruns avoid pathological subprocess
spawning.

## Acceptance Gate 11 --- F1 resource compatibility

Review the architecture against:

-   Azure App Service Linux F1;
-   shared capacity;
-   approximately 60 CPU minutes/day;
-   1 GB storage;
-   cold starts/throttling;
-   persistent `/home`;
-   no SLA;
-   `$0.00` recurring infrastructure.

The ADR must reason credibly about:

-   .NET process startup per governed query;
-   likely request frequency;
-   Streamlit reruns;
-   concurrent sessions;
-   memory lifetime;
-   cache suppression of provider calls;
-   temporary artifacts;
-   no polling/watch loop;
-   no always-on companion API.

Do not require unsupported performance claims.

If the architecture cannot plausibly fit the F1 model, BLOCK rather than
assuming later optimization.

## Acceptance Gate 12 --- Docker/runtime availability

Inspect the current Dockerfile and deployed topology.

Verify whether the .NET Worker executable/artifacts required for
one-shot invocation are already present in the same container/runtime
where Streamlit executes.

Determine:

-   executable location can be fixed/configured server-side;
-   Python can invoke it locally;
-   required .NET runtime is present;
-   shared persistent cache path is accessible;
-   process permissions are plausible;
-   startup entrypoint does not preclude local child-process invocation.

If the selected architecture necessarily requires Docker/startup
mutation, the ADR must state that exact dependency so later
implementation/deployment authorities can govern it.

Do not accept an ADR that assumes artifacts exist when the current image
proves otherwise.

## Acceptance Gate 13 --- Legacy compatibility

The historical-query extension must not silently alter the default
behavior of existing Worker invocations.

Require future tests for:

-   legacy invocation remains unchanged;
-   historical-query mode is explicit;
-   malformed historical request cannot fall through to legacy mode;
-   legacy output is not emitted into historical-query stdout;
-   historical-query execution does not publish/overwrite the legacy
    visualization envelope unless explicitly architected and justified.

## Acceptance Gate 14 --- Failure semantics

Verify the architecture defines bounded presentation-safe mapping for:

-   invalid selection;
-   malformed request;
-   executable unavailable;
-   process launch failure;
-   timeout;
-   cancellation;
-   cache failure;
-   provider unavailable;
-   provider rate limit;
-   provider auth/config failure;
-   malformed provider response;
-   empty successful dataset;
-   stale fallback;
-   malformed/truncated bridge response.

Raw implementation failures must not become public UI text.

## Acceptance Gate 15 --- Implementation mutation forecast

The ADR must be concrete enough for the next Terra authority to freeze
an exact literal allowlist.

It should identify expected categories such as:

-   minimum Worker/host mode/composition path;
-   bridge request/response contracts;
-   focused .NET tests;
-   Python bridge consumer;
-   Streamlit Market Research UI;
-   Python tests;
-   `requirements.txt` only if Plotly is separately approved;
-   Release 1.13 manifest;
-   Terra implementation authority record.

This acceptance does not itself authorize those paths.

If the ADR is too vague to derive a bounded implementation authority,
correct it.

## Acceptance Gate 16 --- Dependency/schema/infrastructure boundary

Confirm selected architecture requires no:

-   FastAPI;
-   Flask;
-   new persistent ASP.NET service;
-   Redis;
-   Celery;
-   broker;
-   managed queue;
-   managed cache;
-   new database;
-   schema migration;
-   paid service.

Plotly is not part of bridge architecture acceptance and remains a
separate WP05 implementation dependency decision.

## Acceptance Gate 17 --- Documentation quality and model declaration

Verify every new Release 1.13 Markdown file created by the candidate
states its GPT-5.6 model role.

Verify prompter naming preserves the `release-1.13-` prefix convention.

The ADR must distinguish frozen requirements from implementation
examples.

Avoid accidentally freezing speculative filenames/API names that
repository inspection did not justify.

## Correction authority

This acceptance authority may correct substantive architecture-document
defects **only inside the same three-path documentation scope**.

Allowed corrections include:

-   clarifying request/response contract;
-   fixing concurrency policy;
-   defining cross-process coordination;
-   correcting Docker/runtime assumptions;
-   tightening security;
-   clarifying timeout/cancellation;
-   fixing failure mappings;
-   making implementation forecast sufficiently bounded;
-   correcting F1 reasoning;
-   formatting/documentation defects.

If corrections are made:

1.  remain on the same PR/branch;
2.  mutate only the three authorized documentation paths;
3.  commit/push corrections;
4.  record the new PR head;
5.  rerun the complete substantive acceptance against that new head;
6.  report the **final Luna-accepted head SHA**.

Report:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

when an architecture defect is corrected within these boundaries.

## Governance stop conditions

Stop and report:

`GOVERNANCE RESTRICTION — STOPPED`

if acceptance would require:

-   production/test code mutation;
-   a fourth changed path;
-   changing WP01/WP02/WP04 accepted semantics;
-   changing Vike or Twelve Data behavior;
-   new package/dependency;
-   schema migration;
-   paid/managed infrastructure;
-   Docker/deployment mutation;
-   root README;
-   Release 1.14/2.0 scope;
-   secret exposure;
-   implementation of the bridge;
-   Plotly/UI implementation;
-   merge/lifecycle execution.

State the exact evidence and required next authority.

## Retry-until-governance-boundary rule

Do not stop merely because inspection, validation, documentation checks,
Git operations, or an architecture detail initially fails.

Diagnose it, correct defects permitted by the three-path acceptance
scope, rerun the relevant checks, and continue until the architecture
passes or a true governance restriction is reached.

Ordinary correctable architecture/document defects are not a reason to
return BLOCKED.

## Validation before PASS

At minimum:

-   live PR/base/head/state verified;
-   exact three changed paths verified;
-   README unchanged;
-   no implementation files changed;
-   `git diff --check` passes;
-   secret scan of candidate docs passes;
-   all acceptance gates above reviewed;
-   cross-process concurrency explicitly resolved;
-   Docker/runtime assumption verified against repository;
-   request/response contract frozen;
-   stdout/stderr discipline frozen;
-   process security frozen;
-   timeout/cancellation bounded;
-   F1 compatibility supported;
-   legacy behavior preserved;
-   no dependency/schema/managed service introduced;
-   issue #292 remains Open / non-Done;
-   milestone #64 remains Open;
-   PR remains unmerged;
-   no WP05 implementation begun.

## Lifecycle boundary

A substantive acceptance PASS does **not** authorize merge.

After PASS, the next action is a separate **GPT-5.6 Terra PR #301
merge/post-merge lifecycle authority** bound to the final Luna-accepted
head.

Only after that lifecycle completes and the ADR exists on canonical
`main` may a revised Terra WP05 bridge/UI implementation authority be
issued.

## Required final report

Return:

-   canonical predecessor inspected;
-   PR #301 state/base/branch;
-   initial candidate head;
-   final accepted head;
-   exact changed paths;
-   files inspected;
-   issue #292 / Project state;
-   milestone #64 state;
-   existing-model preservation finding;
-   WP04 no-bypass finding;
-   request-contract finding;
-   response-contract finding;
-   stdio/JSON finding;
-   subprocess-security finding;
-   timeout/cancellation finding;
-   cross-process concurrency decision and evidence;
-   cache/atomicity finding;
-   Streamlit rerun finding;
-   F1 resource finding;
-   Docker/runtime compatibility finding;
-   legacy compatibility finding;
-   failure-semantics finding;
-   future implementation mutation forecast;
-   dependency/schema/infrastructure finding;
-   documentation/model-role finding;
-   corrections made;
-   validation results;
-   secret/whitespace results;
-   README proof;
-   confirmation PR remains unmerged;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP05 HISTORICAL BRIDGE ARCHITECTURE FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP05 HISTORICAL BRIDGE ARCHITECTURE FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS accepts architecture only. It does not authorize merge, bridge
implementation, Plotly/UI implementation, WP06, deployment, tag, or
release.

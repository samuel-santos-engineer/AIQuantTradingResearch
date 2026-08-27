# Release 1.9 — WP02 Contract-Boundary Amendment — Codex Authority

## Authority

This document grants a **narrow corrective amendment** to the existing Release 1.9 WP02 execution authority for canonical GitHub issue **#227**.

WP02 is currently blocked before implementation because its required replay semantics cannot be represented safely by the current Application contract.

Proven blocker:

- `IObservationSource` currently exposes only:
  `ObservationSourceResult GetObservations(ResearchRequest request)`
- `ResearchRequest` currently carries only:
  - target
  - requested observation count
- WP02 requires semantics for:
  - replay identity
  - logical ticks
  - restart
  - duplicate behavior
  - cancellation
  - finite replay
- the existing WP02 manifest authorizes Infrastructure replay/configuration/DI paths but does not authorize the Application contract evolution required to express those semantics correctly;
- implementing hidden state behind the current interface would either omit acceptance semantics or create an ungoverned contract change;
- no repository or GitHub mutation was made during the blocked WP02 attempt;
- WP03 has not started.

Terminal blocked state:

`RELEASE 1.9 WP02 BLOCKED`

This amendment exists only to authorize the **minimum Application-layer contract change necessary to make WP02 implementable as already defined**.

It does not redefine WP02.

It does not authorize WP03.

It does not authorize broad Application refactoring.

---

# Objective

Read canonical issue **#227**, the accepted Release 1.9 definition, and the current Application/Infrastructure boundaries.

Then determine and implement the **smallest coherent contract extension** required to represent all WP02 acceptance semantics safely and explicitly.

The amended contract must be sufficient for the already-authorized WP02 Infrastructure replay implementation to support:

- replay identity;
- deterministic logical ticks;
- restart/resume semantics;
- duplicate semantics;
- cancellation;
- finite replay/end-of-stream behavior.

Do not preselect a design merely because it is convenient.

Derive the contract from #227 acceptance requirements and existing architecture.

---

# Core Principle

The Application contract must make required semantics **explicit enough to test and reason about**.

Do not hide required WP02 semantics in:

- static state;
- global mutable state;
- implicit adapter internals;
- thread-local state;
- process-global caches;
- undocumented side channels;
- magic sentinel values;
- exception-only control flow where a normal result contract is required;
- ungoverned lifecycle assumptions.

The goal is not to maximize abstraction.

The goal is to expose only the minimum state/inputs/results necessary for WP02 correctness.

---

# Binding WP02 Replay Semantics

The prior amendment left several API-shaping replay behaviors implicit. This
section resolves only those ambiguities; it does not add product scope.

## Replay identity and fixture

- The Infrastructure adapter owns one fixed, repository-owned fixture with a
  stable replay identity of `simulated-live-replay-v1`.
- Its target, UTC instants, decimal prices, and ordering are immutable for the
  lifetime of WP02. No provider, wall clock, randomness, or network input is
  permitted.

## Logical tick and restart

- A logical tick is the zero-based index of a fixture observation.
- A replay request explicitly carries its requested starting tick.
- Starting tick `0` is a deterministic restart: it returns the same fixture
  sequence on every fresh request.
- Starting tick `n` resumes deterministically at fixture index `n`; it does
  not depend on hidden adapter/session state.

## Bounded read and finite completion

- A request asks for a bounded positive count beginning at its starting tick.
- A successful response returns only the contiguous fixture observations in
  that bounded range, in ascending tick order, and reports the first/next
  logical tick explicitly.
- A request whose starting tick equals the fixture length returns an explicit
  successful end-of-replay result, distinct from failure and distinct from an
  empty successful observation list.
- A request past the fixture end, or a non-positive requested count, is an
  explicit invalid/insufficient replay request according to the existing
  failure/result convention; it must not wrap, sleep, or synthesize data.

## Duplicate semantics

- Reissuing the same replay identity, starting tick, and bounded count is a
  duplicate replay request.
- The source returns the same deterministic successful result, including the
  same observations and tick metadata. It performs no hidden de-duplication,
  persistence, or suppression.
- Downstream persistence/idempotency remains the owner of accepting an
  equivalent observation set. WP02 exposes the duplicate deterministically;
  it does not implement WP03 pipeline or persistence behavior.

## Cancellation

- Cancellation is represented by an additive `CancellationToken` public
  contract parameter; the existing source remains synchronous.
- If the token is already cancelled before work begins, the source throws
  `OperationCanceledException` with that token and returns no replay result.
- The bounded in-memory fixture enumeration checks the token before each
  observation. No background work, thread, timer, task, process, or later
  callback is created by WP02.

## Minimum contract shape

The minimum coherent Application extension is an additive replay request/result
shape used by a new replay-specific member on `IObservationSource`; preserve
the existing `GetObservations(ResearchRequest)` member and its existing
implementations/callers unchanged. The new request carries replay identity,
target, starting tick, bounded count, and cancellation token. The new result
explicitly distinguishes observations available from end-of-replay and exposes
tick metadata. Do not add resume/session persistence, async conversion, or a
general scheduler.

This binding is authoritative for Phase 1 onward. It removes the restart,
duplicate, cancellation, and finite-completion ambiguity identified by the
blocked run.

---

# Permitted Scope Expansion

This amendment permits WP02 to modify the minimum necessary Application-layer contract surface required for #227.

Potentially permitted, when justified by acceptance criteria:

- `IObservationSource`;
- `ResearchRequest`;
- `ObservationSourceResult`;
- directly related Application-layer replay contract types;
- cancellation parameter propagation;
- deterministic replay/session identity representation;
- logical position/tick representation;
- finite/end-of-replay result representation;
- restart/resume request semantics;
- duplicate semantics representation;
- tests for those contracts.

This amendment also preserves the original WP02 authority for:

- Infrastructure replay implementation;
- Infrastructure configuration;
- Dependency Injection wiring;
- focused tests;
- full regression validation;
- WP02 GitHub lifecycle finalization after acceptance.

---

# Explicitly Forbidden

Do not:

- redesign unrelated Application services;
- perform a general interface cleanup;
- introduce a new architectural layer unless #227 cannot be satisfied without it;
- change schema version unless #227 explicitly requires persistence semantics that are impossible otherwise;
- change Python version;
- change package pins;
- change Streamlit version;
- change the governed one-shot JSON-over-stdio boundary unless #227 explicitly requires a compatible representation adjustment and the Release 1.9 authority allows it;
- implement WP03 or later work;
- alter Release 1.9 planning;
- change dependency edges;
- create replacement issues;
- close #227 before WP02 acceptance passes;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- add broad concurrency, persistence, networking, or scheduling infrastructure merely to support replay;
- use hidden state as a substitute for an explicit contract.

---

# Phase 0 — Read and Extract

Before changing code:

1. Read #227 completely.
2. Read the Release 1.9 authority/definition sections governing WP02.
3. Read the current definitions and usages of:
   - `IObservationSource`
   - `ResearchRequest`
   - `ObservationSourceResult`
4. Read existing implementations of `IObservationSource`.
5. Read existing call sites.
6. Read tests covering observation-source behavior.
7. Read the original WP02 manifest/scope restrictions.
8. Build a requirement-to-contract matrix for:
   - replay identity
   - logical ticks
   - restart
   - duplicates
   - cancellation
   - finite replay

Do not mutate code until each acceptance semantic has an identified representation need.

---

# Phase 1 — Contract Design Gate

Design the minimum contract change.

For every proposed contract element, document:

- which #227 acceptance requirement requires it;
- why the current contract cannot safely express that requirement;
- why the proposed element is the minimum necessary;
- which existing callers/implementations are affected;
- how backward compatibility is handled, if applicable;
- how it will be tested.

Prefer additive/minimal changes where they preserve clarity.

Do not preserve the old shape merely to avoid touching Application code if doing so would make semantics implicit.

Do not expand the contract with speculative future fields.

### Hard stop

If more than one materially different architecture appears equally valid and the accepted Release 1.9 authority does not resolve the choice, stop and report the design ambiguity instead of guessing.

---

# Phase 2 — Contract Implementation

Implement only the approved minimum contract extension.

Requirements:

- semantics must be explicit;
- naming must align with existing repository conventions;
- deterministic replay identity must be stable and testable;
- logical ticks/positions must be unambiguous;
- restart/resume behavior must have defined input semantics;
- duplicate handling must have defined observable behavior;
- cancellation must propagate through the public contract;
- finite replay must expose completion/end state explicitly enough for callers/tests to distinguish it from error or empty data.

If cancellation is represented through `.NET CancellationToken`, use the repository's normal asynchronous/synchronous conventions rather than inventing a parallel cancellation mechanism.

Do not convert the entire subsystem to async unless #227 requires it.

---

# Phase 3 — Compatibility Audit

After changing the Application contract:

1. enumerate all implementations of `IObservationSource`;
2. enumerate all call sites;
3. update only what is required to compile and preserve existing behavior;
4. verify no unrelated behavior changes;
5. prove the one-shot JSON-over-stdio boundary still behaves as governed;
6. prove any existing non-replay observation source retains its prior semantics unless #227 explicitly changes them.

Avoid compatibility shims that obscure the new semantics.

If a shim is required, keep it minimal and tested.

---

# Phase 4 — WP02 Replay Implementation

Once the contract is proven sufficient, continue the already-authorized WP02 work.

Implement the Infrastructure replay/configuration/DI behavior required by #227.

The implementation must exercise the amended contract rather than bypass it.

Do not add hidden state that contradicts the explicit contract.

---

# Phase 5 — Focused Acceptance Tests

Add or update focused tests proving each required semantic independently.

At minimum, tests must cover:

## Replay identity
- distinct replay identities remain distinguishable;
- the same replay identity behaves deterministically where #227 requires determinism.

## Logical ticks
- ticks/positions advance according to the accepted semantics;
- ordering is deterministic.

## Restart
- restart/resume begins from the explicitly requested replay state;
- behavior is stable and reproducible.

## Duplicate behavior
- duplicates are handled exactly as #227 specifies;
- duplicate semantics are observable and testable.

## Cancellation
- cancellation reaches the source through the public contract;
- cancellation produces the expected result/exception behavior under repository convention;
- no hidden continued replay work occurs after cancellation where that would violate #227.

## Finite replay
- end-of-replay is explicitly observable;
- finite completion is not confused with failure;
- over-read/request-past-end behavior matches #227.

Also test compatibility for existing observation-source behavior affected by the contract change.

---

# Phase 6 — Full Regression

Run the authoritative suite:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Capture:

- exact exit status;
- passed;
- failed;
- skipped;
- relevant warnings.

Historical predecessor baseline was 281/281.

A larger count is expected if WP02 adds tests.

Do not weaken or remove tests merely to restore the old count.

---

# Phase 7 — Diff and Boundary Audit

Before declaring WP02 technically complete, inspect the final diff.

Classify every changed file as one of:

- Application contract change required by this amendment;
- compatibility update caused directly by that contract;
- Infrastructure replay/configuration/DI implementation already authorized by WP02;
- WP02-focused test;
- WP02-required documentation.

Anything else is unauthorized unless separately justified by #227.

Prove:

- no WP03+ work;
- no unrelated refactor;
- no package/Python/schema/version change unless explicitly required and authorized;
- no planning authority changes;
- no hidden-state workaround remains.

---

# Phase 8 — WP02 Lifecycle Completion

Only after all #227 acceptance criteria pass:

1. follow the established WP completion convention;
2. add required concise evidence comment;
3. transition #227 Project Status to the authoritative completed state;
4. preserve Priority = P1, Release = 1.9, authoritative Area;
5. close #227;
6. keep milestone #58 open;
7. verify #228–#237 remain untouched.

Do not begin WP03.

---

# Stop Conditions

Stop immediately if:

- #227 cannot be read;
- contract requirements remain ambiguous;
- replay identity semantics cannot be derived from accepted authority;
- restart semantics cannot be derived safely;
- duplicate semantics are unspecified and materially affect API design;
- cancellation semantics require a broad async/concurrency redesign not authorized here;
- finite replay semantics require schema/protocol changes not explicitly authorized;
- implementation would require WP03+ scope;
- the contract change expands beyond the minimum necessary Application surface;
- compatibility updates reveal a broader architecture conflict;
- focused tests fail for reasons outside WP02;
- full regression fails for reasons not owned by WP02;
- GitHub lifecycle mutation cannot be proven.

On stop:

- do not broaden scope;
- preserve evidence;
- report the exact unresolved contract boundary;
- identify the minimum additional authority that would be required.

---

# Success Criteria

The amendment and WP02 succeed only when:

- #227 acceptance requirements are mapped to explicit contract semantics;
- the minimum Application contract change is implemented;
- replay identity is explicit and tested;
- logical ticks are explicit and tested;
- restart semantics are explicit and tested;
- duplicate semantics are explicit and tested;
- cancellation is represented through the public contract and tested;
- finite replay completion is explicit and tested;
- Infrastructure replay/configuration/DI implementation uses the amended contract;
- existing affected callers/implementations preserve intended behavior;
- focused WP02 tests pass;
- full authoritative regression passes;
- final diff remains inside amended WP02 scope;
- #227 is finalized under normal lifecycle governance;
- milestone #58 remains open;
- #228–#237 remain open and untouched;
- WP03 has not started.

---

# Required Completion Report

Return:

## Contract boundary
- original contract limitation;
- exact amended contract shape;
- requirement-to-contract mapping;
- compatibility impact.

## WP02 implementation
- Infrastructure replay/configuration/DI work completed;
- files changed;
- tests added/changed.

## Acceptance semantics
Report pass/fail evidence for:
- replay identity;
- logical ticks;
- restart;
- duplicates;
- cancellation;
- finite replay.

## Regression
- focused test results;
- full `dotnet test` result and exact counts.

## Scope proof
- final diff classification;
- confirmation that no WP03+ work occurred;
- confirmation that no unauthorized foundation changes occurred.

## GitHub lifecycle
- #227 before/after;
- Project Status before/after;
- milestone #58 state;
- confirmation #228–#237 untouched.

## Next eligibility
State:

`NEXT ELIGIBLE WORK PACKAGE: WP03 — #228`

Do not authorize or execute WP03.

---

# Terminal Markers

On success, end with exactly:

`RELEASE 1.9 WP02 CONTRACT AMENDMENT AND EXECUTION COMPLETE`

On safe stop/blocker, end with exactly:

`RELEASE 1.9 WP02 CONTRACT AMENDMENT BLOCKED`

Do not emit the success marker unless all amended WP02 acceptance and lifecycle requirements are proven.

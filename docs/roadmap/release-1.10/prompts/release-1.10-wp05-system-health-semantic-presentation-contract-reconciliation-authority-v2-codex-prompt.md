# Release 1.10 WP05 — System Health Semantic & Presentation Contract Reconciliation Authority V2

## Model assignment

- **GPT-5.6 Luna** — PRIMARY authority for contract, policy, architecture, semantics, presentation contract, reconciliation, acceptance criteria, governance, and read-only/planning changes.
- **GPT-5.6 Terra** — implementation, validation execution, approved repository/Git/GitHub mutations, and WP lifecycle completion only after Luna freezes a deterministic implementation contract.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and exploratory/non-authoritative review; Sol never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10**

Work package:

**WP05 — System Health Read Model and Streamlit Presentation**

Issue: **#246**

Milestone: **#59**

Project: **#2**

Predecessor:

**WP04 #245 — Closed / Done**

Dependency:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This is a narrow **semantic and presentation reconciliation V2 authority**.

It exists because:

1. the first WP05 Terra authority correctly blocked before mutation;
2. the first Luna WP05 contract/path reconciliation froze the broad architecture;
3. WP05 Terra V2 correctly blocked because material implementation semantics remained unresolved.

This authority MUST NOT reopen already-frozen architecture.

It MUST close only the remaining gaps that caused Terra V2 to block.

---

# Accepted frozen WP05 architecture

Treat the following as immutable unless an actual contradiction in the authoritative artifacts is discovered:

- existing visualization states remain:
  - `Ready`
  - `WarmUp`
  - `Empty`
  - `Stale`
  - `Failed`
- visualization state and System Health remain separate concepts;
- `aiq-visualization-read-model-v1` remains canonical;
- System Health is an **optional nested `systemHealth`** extension;
- no second health channel;
- no SQLite schema migration;
- no independent System Health freshness threshold;
- exact production paths already exist and are frozen;
- exact test paths already exist and are frozen;
- exact WP06 handoff structure is already frozen;
- no external exporter;
- WP03/WP04 BCL-only observability/lifecycle architecture remains valid;
- Streamlit remains presentation-only;
- .NET remains canonical System Health source;
- Python does not become a second health authority;
- Release 1.8 JSON-over-stdio remains separate.

Do not redesign these.

---

# Known unresolved gaps

Terra V2 identified these exact remaining implementation choices:

1. **No exact source condition for `degraded`.**
2. **No exact source condition for `unavailable`.**
3. **No finite exhaustive `reason` token set.**
4. **No exact state/reason mapping.**
5. **No exact Streamlit placement.**
6. **No exact user-visible Streamlit labels/messages.**
7. **No exact malformed-health presentation behavior.**

These are the ONLY intended reconciliation targets unless inspection reveals a directly coupled ambiguity required to freeze them.

Emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION V2 ENTRY: PASS`

---

# Mutation boundary

This is planning/reconciliation only.

Allowed repository mutations are restricted to the minimum already-authorized Release 1.10 planning/architecture artifacts needed to encode the missing semantics, expected among:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`

Modify `RELEASE_1.10_DEFINITION.md` only if a direct definition-level contradiction makes it unavoidable.

Forbidden:

- production code;
- tests;
- `.csproj`;
- package files;
- package installation;
- schema/migration files;
- generated files;
- Git mutations;
- GitHub mutations;
- issue closure;
- Project status mutation;
- WP06 implementation.

#246 remains Open/Backlog.

---

# Phase 0 — Load frozen contract and actual implementation surfaces

Read:

1. Release 1.10 definition;
2. reconciled execution plan;
3. reconciled file manifest;
4. reconciled `OPEN_TELEMETRY_SELECTION.md`;
5. #246 read-only;
6. #247 read-only;
7. current WP03 observability implementation;
8. current WP04 lifecycle implementation;
9. exact .NET WP05 source/read-model paths frozen previously;
10. exact canonical handoff/serialization paths;
11. exact Python parser/frame/presentation paths;
12. exact Streamlit path/component;
13. exact WP05 test allowlist;
14. current worktree status/diff.

The purpose of reading production code is to identify actual available source facts, not to mutate code.

Emit:

`RELEASE 1.10 WP05 AVAILABLE HEALTH SOURCE FACTS: ENUMERATED`

---

# Phase 1 — Freeze exact health-state predicates

Freeze the exact predicate for every canonical System Health state already selected by the prior reconciliation.

At minimum explicitly define the predicates for:

- normal/available state;
- `degraded`;
- `unavailable`;
- any other canonical health state already frozen.

For each state specify:

- exact source fact(s);
- exact Boolean predicate;
- precedence relative to other states;
- whether the state is mutually exclusive;
- whether it may coexist with visualization `Ready/WarmUp/Empty/Stale/Failed`;
- exact behavior when multiple source conditions are true.

No vague phrases such as:

- "when observability is degraded";
- "when unavailable";
- "when an error occurs";
- "when something is unhealthy".

Use concrete source conditions from actual WP03/WP04 state.

If no actual source fact can truthfully support a state, remove that state from the canonical health vocabulary rather than inventing telemetry.

Emit:

`RELEASE 1.10 WP05 HEALTH STATE PREDICATES: FROZEN`

---

# Phase 2 — Freeze precedence/order of evaluation

Define deterministic health-state evaluation order.

Provide a total precedence rule such as:

1. `<highest-priority state>`
2. `<next>`
3. `<next>`
4. `<default>`

The exact order must explain all overlaps.

For every pair of potentially overlapping conditions, state which wins.

Emit:

`RELEASE 1.10 WP05 HEALTH STATE PRECEDENCE: FROZEN`

---

# Phase 3 — Freeze finite reason-token vocabulary

Define the complete finite serialized `reason` token set.

Requirements:

- exhaustive for every canonical health outcome;
- bounded;
- sanitized;
- machine-stable;
- lowercase or other exact casing frozen;
- no arbitrary exception text;
- no raw provider/target/symbol/path values;
- no GUID/request IDs;
- no free-form string escape hatch.

For every token define:

- exact token;
- exact triggering condition;
- allowed health state(s);
- exact semantic meaning;
- whether shown directly to the user or mapped to presentation text.

If "no reason" is valid, freeze whether:
- field is omitted;
- field is null;
- a specific token is used.

Do not leave this to Terra.

Emit:

`RELEASE 1.10 WP05 HEALTH REASON TOKEN SET: FROZEN`

---

# Phase 4 — Freeze state → reason mapping

Provide an exhaustive mapping table:

| Source predicate | Health state | Reason token | Notes |
| --- | --- | --- | --- |

Every possible frozen source condition must land in exactly one canonical row.

No ambiguous many-to-many mapping unless precedence fully resolves it.

Emit:

`RELEASE 1.10 WP05 STATE/REASON MAPPING: FROZEN`

---

# Phase 5 — Freeze serialized System Health examples

Using the previously frozen nested `systemHealth` shape, provide synthetic canonical JSON examples for every health state/reason combination.

Do not alter the broad handoff schema.

Examples must show:

- exact property names;
- exact state token;
- exact reason token;
- timestamp fields if already frozen;
- absence/null behavior if already frozen.

Emit:

`RELEASE 1.10 WP05 SYSTEM HEALTH SERIALIZATION EXAMPLES: FROZEN`

---

# Phase 6 — Freeze exact Streamlit placement

Inspect the current Streamlit application structure and freeze the precise System Health placement.

Specify:

- exact file;
- exact function/component;
- exact relative location in the existing page;
- whether it appears before/after the current visualization status/metadata;
- whether it is always visible or conditional;
- whether it uses an existing container/section/column;
- exact heading text if any.

Do not merely say "show System Health in Streamlit."

Do not introduce a new page unless the existing frozen path explicitly requires it.

Emit:

`RELEASE 1.10 WP05 STREAMLIT PLACEMENT: FROZEN`

---

# Phase 7 — Freeze exact presentation text

For every canonical health state and reason token, freeze exact user-visible wording.

Provide a complete table:

| Health state | Reason token | Heading/label | Primary text | Optional detail |
| --- | --- | --- | --- | --- |

Requirements:

- concise;
- truthful;
- no implication of external exporter availability;
- no implication of live provider connectivity unless actually observed;
- no conflation with visualization lifecycle;
- no raw internal token shown unless intentionally frozen;
- deterministic.

If icon/status component type matters (`st.info`, `st.warning`, etc.), freeze exact component type only if repository UI conventions make that part of the contract.

Emit:

`RELEASE 1.10 WP05 STREAMLIT STATE/REASON TEXT MAPPING: FROZEN`

---

# Phase 8 — Freeze malformed-health behavior

Define exact behavior for each malformed case already contemplated by the broader compatibility contract.

At minimum:

1. `systemHealth` absent on pre-WP05 compatible v1 payload;
2. `systemHealth` present but not an object;
3. missing required state field;
4. unknown state token;
5. missing required reason field if reason is required;
6. unknown reason token;
7. malformed timestamp;
8. incompatible field type;
9. malformed entire canonical document.

For each case specify:

- .NET producer expectation if applicable;
- Python parser outcome;
- whether base visualization remains renderable;
- System Health frame/result;
- exact Streamlit presentation;
- whether this becomes `unknown`, `unavailable`, parser error, or whole-document failure.

Do not collapse all malformed cases into one vague "error" unless the existing parser contract requires that.

Emit:

`RELEASE 1.10 WP05 MALFORMED HEALTH BEHAVIOR: FROZEN`

---

# Phase 9 — Freeze absent/pre-WP05 behavior

Because `systemHealth` is optional for v1 compatibility, explicitly define behavior for a valid pre-WP05 payload.

Freeze:

- parser representation;
- frame representation;
- canonical health state or absence representation;
- exact user-visible Streamlit text;
- whether this is "not reported", "unavailable", or another exact frozen label;
- whether base visualization remains fully usable.

Do not portray absence as healthy.

Emit:

`RELEASE 1.10 WP05 PRE-WP05 ABSENT HEALTH BEHAVIOR: FROZEN`

---

# Phase 10 — Freeze presentation truth table

Produce a single end-to-end executable truth table with columns:

| .NET source condition | Health state | Reason token | Serialized `systemHealth` | Python parsed result | Frame result | Streamlit placement | Streamlit exact text | Visualization state interaction |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |

Include:

- every canonical normal health case;
- every degraded case;
- every unavailable case;
- every additional canonical state;
- absent pre-WP05 payload;
- malformed health cases.

Terra must be able to implement directly from this table without judgment.

Emit:

`RELEASE 1.10 WP05 END-TO-END HEALTH TRUTH TABLE: FROZEN`

---

# Phase 11 — Reconcile exact test assertions

Using the already-frozen test paths, specify the exact assertions required for each row of the truth table.

For each relevant test path define:

- input/source condition;
- expected health state;
- expected reason token;
- expected serialized representation;
- expected parser result;
- expected Streamlit text/component/placement;
- expected malformed behavior.

No new test path unless absolutely necessary and consistent with the existing allowlist policy.

Emit:

`RELEASE 1.10 WP05 TEST ASSERTION MATRIX: FROZEN`

---

# Phase 12 — WP06 handoff refinement

Refine, without broadening WP06 scope, the exact permanent assertions WP06 must enforce for the newly frozen semantics:

- health-state predicates;
- state precedence;
- finite reason-token set;
- state/reason mapping;
- absent-health compatibility;
- malformed-health behavior;
- exact presentation mapping;
- no-bypass relationship.

Do not implement WP06.

Emit:

`RELEASE 1.10 WP05 → WP06 SEMANTIC/PRESENTATION HANDOFF: FROZEN`

---

# Phase 13 — Planning artifact reconciliation

Update only the minimum authorized planning/architecture artifacts.

Require the final documents to contain enough detail that Terra does not need to infer:

- degraded predicate;
- unavailable predicate;
- state precedence;
- reason vocabulary;
- state/reason mapping;
- exact Streamlit location;
- exact Streamlit text;
- malformed behavior;
- test assertions.

Emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PLANNING RECONCILIATION: PASS`

---

# Phase 14 — Cross-document consistency

Re-read all three reconciled artifacts.

Prove:

- broad WP05 architecture remains unchanged;
- `aiq-visualization-read-model-v1` remains canonical;
- optional nested `systemHealth` remains intact;
- no second channel;
- schema v4 unchanged;
- no independent freshness threshold;
- exact paths remain unchanged unless this authority explicitly and validly tightens path detail;
- all state/reason/presentation semantics match across documents;
- WP06 handoff is consistent.

Emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION CROSS-CONTRACT CONSISTENCY: PASS`

---

# Phase 15 — Strong Terra materialization simulation

Perform a strict simulated implementation walkthrough.

For every row in the end-to-end truth table answer:

1. exact .NET source symbol read;
2. exact predicate evaluated;
3. exact state selected;
4. exact reason selected;
5. exact serialized fields written;
6. exact Python parser branch;
7. exact frame value;
8. exact Streamlit component/location;
9. exact user-visible text;
10. exact test assertion.

Then ask:

**Could two competent Terra implementers make different reasonable choices while still claiming compliance?**

If YES, reconciliation is incomplete.

Only if NO emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 16 — Mutation audit

Report exact mutations.

Require:

- planning/architecture documentation only;
- production mutations ZERO;
- test mutations ZERO;
- project/package mutations ZERO;
- schema/runtime mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO.

#246 remains Open/Backlog.

Milestone #59 remains Open.

WP06 does not start.

Emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION GITHUB MUTATIONS: ZERO`

---

# Phase 17 — Terra V2 resumption handoff

Only after the stronger materialization simulation passes, authorize resumption of:

**Release 1.10 WP05 — System Health Read Model and Streamlit Presentation Authority V2 — GPT-5.6 Terra**

Terra V2 must consume this semantic/presentation reconciliation and must not reopen:

- health-state predicates;
- state precedence;
- reason tokens;
- state/reason mapping;
- Streamlit placement;
- user-visible labels;
- malformed behavior;
- pre-WP05 absent-health behavior.

Emit:

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP05 → TERRA V2 RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION V2 ENTRY: PASS`

`RELEASE 1.10 WP05 AVAILABLE HEALTH SOURCE FACTS: ENUMERATED`

`RELEASE 1.10 WP05 HEALTH STATE PREDICATES: FROZEN`

`RELEASE 1.10 WP05 HEALTH STATE PRECEDENCE: FROZEN`

`RELEASE 1.10 WP05 HEALTH REASON TOKEN SET: FROZEN`

`RELEASE 1.10 WP05 STATE/REASON MAPPING: FROZEN`

`RELEASE 1.10 WP05 SYSTEM HEALTH SERIALIZATION EXAMPLES: FROZEN`

`RELEASE 1.10 WP05 STREAMLIT PLACEMENT: FROZEN`

`RELEASE 1.10 WP05 STREAMLIT STATE/REASON TEXT MAPPING: FROZEN`

`RELEASE 1.10 WP05 MALFORMED HEALTH BEHAVIOR: FROZEN`

`RELEASE 1.10 WP05 PRE-WP05 ABSENT HEALTH BEHAVIOR: FROZEN`

`RELEASE 1.10 WP05 END-TO-END HEALTH TRUTH TABLE: FROZEN`

`RELEASE 1.10 WP05 TEST ASSERTION MATRIX: FROZEN`

`RELEASE 1.10 WP05 → WP06 SEMANTIC/PRESENTATION HANDOFF: FROZEN`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PLANNING RECONCILIATION: PASS`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION CROSS-CONTRACT CONSISTENCY: PASS`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION REPOSITORY MUTATIONS: PLANNING/ARCHITECTURE ONLY`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PRODUCTION/TEST MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION PROJECT/PACKAGE/SCHEMA MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION GITHUB MUTATIONS: ZERO`

`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION CONTRACT RECONCILIATION: PASS`

`RELEASE 1.10 WP05 → TERRA V2 RESUMPTION HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 WP05 — SYSTEM HEALTH SEMANTIC & PRESENTATION CONTRACT RECONCILIATION AUTHORITY V2 COMPLETE`

---

# Blocked outcome

BLOCK if Luna cannot truthfully derive deterministic semantics from available WP03/WP04 facts and Release 1.10 intent.

If a canonical state such as `degraded` or `unavailable` lacks a real source predicate, do not fabricate one. Either remove/redefine it within the existing WP05 contract if governance permits, or BLOCK with the exact missing governance decision.

If blocked:

- preserve valid planning analysis;
- production/test/project/package/schema mutations ZERO;
- Git mutations ZERO;
- GitHub mutations ZERO;
- #246 remains Open/Backlog;
- milestone #59 remains Open;
- WP06 does not start.

Exact blocked terminal:

`RELEASE 1.10 WP05 — SYSTEM HEALTH SEMANTIC & PRESENTATION CONTRACT RECONCILIATION AUTHORITY V2 BLOCKED`

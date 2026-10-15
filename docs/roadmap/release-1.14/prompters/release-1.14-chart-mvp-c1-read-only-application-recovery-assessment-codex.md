# AIQuantTradingResearch — Chart MVP C1 Codex Authority

## Authority and objective

Owner authorizes **Chart MVP C1: Read-Only Application Recovery and Reuse Assessment** only. The priority is a working, publicly viewable, **read-only historical candlestick interface for BTC/USD and ETH/USD**, using existing project components where feasible. This is an investigation and plan, **not implementation or deployment authority**.

The advanced administrative lifecycle (Release 1.14 WP07 P3L onward, native supervisor/guardian, privileged bootstrap and public administrative diagnostics) is **ON HOLD**. Preserve its artifacts and security boundaries; do not resume or simplify them by implication. `PUBLIC_RUNTIME_VERIFIED=False`; production administrative endpoints and administrative runtime activation remain **NO-GO**.

## Repository safety / immutable baseline

- Expected protected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Last reported HEAD: `67893f51931ee183f71050a83b81127e6e648b87` (verify actual checkout; STOP and report discrepancies rather than assuming freshness).
- The checkout is intentionally dirty/untracked. **Do not clean, stash, reset, checkout, switch, fetch, pull, merge, rebase, commit, push, create PRs, or edit files.**
- **Mutation allowlist: empty.** No repository files, documentation, configs, generated artifacts, environment, cloud resources, or Git state may be changed.
- Capture at start and finish: branch, HEAD, full `git status --porcelain=v1 -uall`, relevant local and remote-tracking refs, and evidence that inspected files did not change. Distinguish committed/merged evidence from dirty/untracked local alternatives and cached refs from fresh remote state. No network calls.
- If preserving the checkout cannot be assured, **STOP**.

## Authorized read-only inspection

Use only bounded read-only filesystem and Git inspection. Do not execute application code, import project modules, run tests, install dependencies, build images, run containers, contact APIs, probe Azure, or read/expose secrets. Avoid commands that create caches or side effects. Inspect source as text, and redact any sensitive values from reports.

### C1-A — Trace current public application startup

1. Locate the actual entrypoint(s): Dockerfile stages and `CMD`/`ENTRYPOINT`, Python/Streamlit startup, port and host binding, environment variable names, Azure App Service container expectations, health-check routes and logging.
2. Identify the minimal publicly reachable **non-admin** app path and what prevents a visible page from loading. Separate **source-backed defects** from **hypotheses requiring later runtime evidence**.
3. Map dependencies between the public UI, legacy administrative diagnostics, lifecycle gates and runtime supervision. Explicitly identify any forbidden coupling or accidental activation risk.
4. Do not infer Azure configuration or runtime state from repository text alone; list specific Azure evidence that a later separately authorized check would require.

### C1-B — Inventory chart/UI reuse

1. Locate existing Streamlit UI pages, charting libraries, market-data abstractions, candle schemas, caches, fixtures, tests and sample datasets.
2. Evaluate whether the current UI can present OHLCV candlesticks with instrument selector (BTC/USD, ETH/USD), interval selector and date range without introducing a new frontend stack.
3. Identify smallest safe implementation touchpoints and tests, **without changing anything**. Clearly separate already-present functionality from proposed additions.
4. Exclude order execution, trading credentials, administrative controls, privileged operations and arbitrary user-supplied code execution from the MVP.

### C1-C — Historical data provenance and display permissions

1. Inventory current providers, API clients, cached datasets, source attribution, license/terms references and redistribution/public-display assumptions.
2. Treat **free API access, public endpoints, and login/password gating as insufficient evidence of display/redistribution rights**.
3. Do not call any provider or acquire external documentation under C1. If first-party licensing is not already demonstrably established in inspected sources, record **LICENSING GAP** and require separate verification before public candle display.
4. Assess only conceptual cache design: source provenance, update cadence, rate limiting, attribution and no hidden secrets in browser responses. Do not populate caches.

### C1-D — Recovery and implementation plan

Produce a practical, ordered plan with independent authorization gates:

- **C2: Provider and public-display rights verification** (first-party evidence; separately authorized external research if needed).
- **C3: Minimal read-only BTC/USD and ETH/USD chart implementation**, after licensing and startup decisions; exact file/test scope proposed but not authorized.
- **C4: Azure deployment and public smoke validation**, with separately approved credentials, cloud operations and rollback criteria.

For each stage identify inputs, dependencies, explicit acceptance criteria, tests/observations needed, risks, and a STOP/GO recommendation. Prioritize recovering a visible, non-admin app without broad architectural redesign. Recommend whether startup diagnosis must precede data integration and what can be parallelized safely.

## Required deliverable (chat report only)

Do not create or edit a repository report. Return:

1. **Executive finding**: shortest plausible path to a working public chart, with evidence confidence.
2. **Source-backed architecture map**: actual startup path, app UI, market data, container/Azure assumptions, and admin separation; include repository-relative file:line citations.
3. **Defect and gap register**: `CONFIRMED`, `LIKELY`, or `UNVERIFIED`; distinguish missing licensing evidence from technical defects.
4. **Reuse matrix**: existing components versus proposed work, with minimal candidate file touchpoints.
5. **C2–C4 staged plan**: ordered tasks, verification and acceptance gates; no execution authority.
6. **Preservation report**: initial/final branch, HEAD, complete status, relevant refs and non-mutation evidence; disclose limits of inspection.
7. **Final disposition**, exactly one of:
   - `CHART-MVP-C1 READ-ONLY ASSESSMENT COMPLETE — OWNER DECISION REQUIRED`
   - `CHART-MVP-C1 BLOCKED — OWNER REVIEW REQUIRED`

## Mandatory STOP conditions

STOP on any requirement to edit files, install/build/run code, access secrets, call network APIs, contact Azure, change Git state, enable public administrative diagnostics, weaken frozen WP07 obligations, or assert provider public-display rights without first-party evidence. Report the blocked step and ask for distinct owner authority. **No implementation, public exposure, deployment or GitHub mutation is authorized by this file.**

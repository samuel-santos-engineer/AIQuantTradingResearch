# Codex Prompt — Release 1.15 WP21: Fresh Private Controller and Independent Recovery Qualification

## Context and authority

Work in `samuel-santos-engineer/AIQuantTradingResearch` from current `origin/main`. WP19 was merged as PR #409 (`914c7bb8937c0d7c92fa62f590cfeb570981a9b9`). WP20's actual operator-session offline preflight passed: four scripts parsed, `liveControllerInvoked=false`, and **no Azure deployment or live health probe occurred**. The remaining blocking results are `privateControllerQualified=false` and `liveAttemptReady=false`.

**WP21 is implementation and OFFLINE qualification only.** Do not mutate Azure, deploy images, issue Azure management or public HTTP probes, call Twelve Data or other providers, import data, change production/UI, modify PowerShell policy, or trigger a live controller. Do not merge the PR. Preserve all owner files and local modifications. No new licensing, terminal-attestation, or infrastructure-cost gates. Keep `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False`.

## Objective

Implement and offline-qualify a **fresh, single-use, private, research-only Azure F1 deployment controller** and a **separately invocable, independent recovery procedure**, integrated with the existing WP18/WP19/WP20 operator PowerShell handoff. Remove the two specific WP20 blockers with evidence, rather than adding another planning-only or generic diagnostic layer.

## Inspect before editing

1. Inspect the actual current `main`, Release 1.15 governance and runbooks, WP16 health collector, WP18 mandatory real-script offline gate, WP19 human handoff, and all WP20 scripts and reports. Confirm actual filenames and interfaces; do not assume them from this prompt.
2. Identify the already published, reviewed immutable research image digest and its approved registry reference from repository evidence. Do not silently change images or create new images. If identity cannot be verified offline, report the exact missing input; never invent one.
3. Identify the **exact existing** Azure F1 research app, resource group and plan from repository evidence: research app `aiqr115researchstagingwus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2, existing F1 plan. UI app `aiqr112wp05wus27f5eabb5` is out of scope.
4. Inspect the previous WP17 consumed attempt record and rollback evidence. **Never reuse, reset, overwrite or delete its single-attempt record**. The WP21 controller needs its own fresh immutable attempt identity and fail-closed replay protection.
5. Establish the exact operator-session script flow, expected report schema, and any existing private-controller path or private-evidence handling. Do not put secrets, settings values or private controller evidence into the PR.

## Implementation requirements

### A. Fresh controller

Implement a real, executable Python controller (and only necessary supporting code) for a **single separately authorized future attempt**, not a stub or TODO. Keep it private if it would contain operational secrets; repository may contain a sanitized template/generator or an operator-created local ignored artifact. Never commit credentials, connection strings, private settings values or deployment evidence.

The controller must fail closed unless all conditions hold:

- Exact allowlisted research app/resource group/plan, no UI/production/slot/resource drift; no paid plan changes.
- Explicit, fresh operator authorization for **one specific attempt**, with unique attempt ID, approved immutable research image digest, and reviewed controller/recovery identities. Approval must not be inferred from a passed offline preflight or a generic `--execute` switch.
- Complete private baseline captured **before any write** and independently compared to the current state immediately before mutation: image, startup command, app settings and slot-setting flags, connection-string metadata as applicable, logging, HTTPS-only, slot inventory, state, and any other mutated configuration. Avoid exposing setting values. If a field cannot be safely captured and restored, stop before mutation.
- Exact pre-existing storage/UID/role, authentication, TLS/transport and SQLite safety guards preserved. No automatic database creation or provider import.
- At most one approved research-only candidate application. No implicit retry, no fallback image, no additional resource operations.
- Start the existing WP16 health observer immediately following Azure Start, using its three bounded attempts / 180-second total and 190-second parent bound. Accept only exact successful HTTPS `GET /health` response (`200`, `Alive`), TLS verified, no redirects; do not mistake Azure Start acknowledgement for process health.
- Only after valid health, permit the previously defined bounded **unauthenticated** snapshot behavior checks for BTC/USD and ETH/USD, with correct `503 DATASET_NOT_READY` handling when no dataset exists. Do not use provider credentials, generate candles or claim authentic chart acceptance.
- Always perform exact baseline restoration and independent post-restore comparison after the attempt, **including apparent success**. A rollback/recovery failure must be prominently surfaced, not masked by a successful health check.
- Ensure timeouts, interrupts, subprocess errors and partial mutation cannot skip recovery. Treat hard process termination as a reason for independent recovery, not something `finally` can solve.

### B. Independent recovery

Provide a **separate operator-executable recovery script**, not merely a `finally` block or controller flag. It must work when the deployment controller crashes or is unavailable, consume the immutable private baseline and attempt identity, refuse mismatched targets, and restore/independently compare exact original state. Make repeated recovery safe and auditable; no unsafe overwrite of a different baseline. Keep recovery callable from the owner's PowerShell session even when Codex is sandbox-restricted.

### C. Operator handoff

Integrate with WP19/WP20 without weakening WP18's mandatory PowerShell 5.1 `-File` offline gate. The **default path is offline-only**. Provide copy/paste-ready Windows PowerShell 5.1 commands and scripts for the human operator, including:

1. offline controller/recovery qualification, without Azure access;
2. sanitized results with explicit `privateControllerQualified` and `liveAttemptReady` booleans and failure reasons;
3. a **separate** future live command, clearly marked **NOT AUTHORIZED BY WP21**;
4. a separate independent recovery command that can be invoked even if the controller fails;
5. instructions to return only sanitized evidence, never credentials or raw private baseline.

If Codex cannot run PowerShell commands due to sandbox/tool restrictions, **create and test the scripts as far as possible, then hand them to the owner to execute in their own session**. Do not change execution policy, use `-ExecutionPolicy Bypass`, invoke alternative identities or interpreters to circumvent restrictions, or repeatedly retry blocked commands. An operator-session success may be incorporated as evidence only when actually supplied; do not fabricate it.

### D. Offline tests and evidence

Add focused tests with mocked Azure CLI and subprocess boundaries covering:

- exact target/image/digest/authorization/attempt ID validation;
- complete baseline capture and independent pre-write comparison;
- refusal on incomplete baseline, drift, missing recovery, wrong image or reused attempt;
- single candidate application; bounded health and correct `Alive` acceptance;
- health timeout, malformed response, snapshot failures and crash paths;
- rollback after both success and failure, and independently verified restoration;
- standalone recovery after controller interruption, including idempotency and mismatch refusal;
- no UI/provider/import/production/paid-resource action;
- no secret leakage into logs or shareable reports;
- PowerShell 5.1 syntax and real-script offline invocation where the operator host permits it.

Do not claim offline mocks prove Azure health or live recovery. Preserve the existing WP16/WP18/WP19/WP20 behavior and run their relevant regression tests. Keep UTF-8 without BOM and LF, check diffs and owner-file preservation.

## Deliverables

- Implementation files for fresh private controller qualification and independent recovery.
- Focused automated tests and updated operator runbook with exact offline and future separately authorized live/recovery commands.
- Sanitized offline qualification evidence that distinguishes `privateControllerQualified` (controller/recovery behavior demonstrated offline) from `liveAttemptReady` (all non-live prerequisites and required operator evidence satisfied). Never mark `liveAttemptReady=true` solely on mocks or unverified operator prerequisites.
- A PR against `main` for owner review, **OPEN and UNMERGED**. Report PR URL/head SHA, files changed, tests run, any unrun host tests, preserved owner files and exact blockers.

## Stop conditions and reporting

If a qualified controller or recovery procedure cannot be established without live Azure access or missing private baseline inputs, implement what is safely possible, report precise remaining inputs and stop. Do not silently downgrade the qualification criteria. The output must distinguish:

- `AZURE_DEPLOYMENT_NOT_RUN` (WP21)
- `privateControllerQualified=<true|false>` with basis
- `liveAttemptReady=<true|false>` with basis
- `RESEARCH_PROCESS_ALIVE=UNVERIFIED`
- `AUTHENTIC_DATA_NOT_IMPORTED`
- `PUBLIC_RUNTIME_VERIFIED=False`

**No live Azure attempt is authorized in this WP21 prompt.** After PR review/merge and any operator-only offline qualification, ask for separate explicit approval of one named live research-only attempt.

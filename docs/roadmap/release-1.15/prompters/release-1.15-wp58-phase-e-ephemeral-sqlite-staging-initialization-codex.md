# Codex execution prompt — Release 1.15 / WP58 Phase E

## Mission

Advance the existing **two-application Azure F1 staging** deployment toward functional equivalence with the accepted WP57 local BTC/USD and ETH/USD historical chart. Keep **Python/Streamlit UI + C#/.NET research API + SQLite on the existing ephemeral research-container filesystem**. No third layer, new database, persistent storage, paid resources, new provider, or licensing approval gate.

**Primary objective:** design the smallest secure, auditable initialization/reinitialization procedure that can populate the Azure research container's existing ephemeral SQLite state with the **same validated authentic BTC/ETH observations** accepted locally, without putting private data or credentials in images, the browser, GitHub, logs, or public endpoints. Then specify the precise staged image-promotion and acceptance sequence. Do not assume that deploying images transports the SQLite database.

## Starting evidence and authority

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR #445: `https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/445`, last reported OPEN/UNMERGED at `64aac2d88793a7a9be28c76f617270a899d2ad00`. **Recheck before edits**; do not overwrite concurrent updates or merge.
- Accepted local WP57 chart: BTC→ETH→BTC, 1,000 authentic candles per symbol, SMA20 981 non-null values with 19 warmup nulls where contiguous, unavailable volume represented as null/no bars, provenance and educational disclosures. Historical local chart verified; public chart not verified.
- Azure research app last configured to old WP35/WP37 digest `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`; configured reference is not running-image evidence or proof of HTTP 403 origin.
- Azure staging UI: `aiqr112wp05wus27f5eabb5`; research API: `aiqr115researchstagingwus27f5eabb5`; shared existing Linux F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. Production `aiqr112wp035ec325382770` excluded.
- The current research `serve` bootstrap does **not** seed missing SQLite state. Current storage guard accepts the staging ephemeral `/tmp/aiq-research` overlay; container replacement can erase it. Image/config rollback cannot restore lost observations.
- Prior anonymous research `/health` returned HTTP 403 once and a subsequent headers-only attempt timed out. Origin UNKNOWN; do not claim image promotion fixes it.
- Current status: `AUTHENTIC_DATA_NOT_IMPORTED=False` (historical local), `LOCAL_CHART_VERIFIED=True` (historical), `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.

## Authority boundary — this run is OFFLINE DESIGN / DOCUMENTATION ONLY

Allowed: inspect repository code, tests, existing documentation and GitHub PR state; reason about the minimum change; edit **only** the two already approved WP58 documentation files; run offline textual/diff validation; commit/push documentation to the existing PR branch if clean, current and authorized by repository workflow.

**Not authorized:** Azure/ARM/SCM/Kudu/HTTP requests, health probes, Docker build/run/pull/push/inspection, registry calls, provider API requests, SQLite/private dataset reads or exports, secret inspection, upload/import/migration, local runtime mutation, deployments, restarts, settings changes, paid resource creation, production access, browser operations, PR merge, or implementation code edits. Any such future action must be proposed with exact scope and obtain separate explicit owner approval. Do not treat the user's architecture agreement as operational authorization.

## Investigation tasks

1. **Verify exact source flow.** Inspect dedicated `container/release_1_15/research-api.Dockerfile`, `research-chart.Dockerfile`, `research-bootstrap.sh`, research state initialization/storage guards, `wp39_local_history.py` prepare path, authenticated snapshot endpoint, existing deployment helpers and tests. Identify the validated local observations' **logical provenance and format** without opening or copying private data. Distinguish local image IDs from OCI manifest digests and configured from running images.
2. **Compare initialization alternatives** within the existing two-app architecture: (a) existing operator-stdin/private export initialization mechanism adapted to Azure staging; (b) a narrowly scoped one-shot container-local initializer using an owner-approved private delivery channel; (c) other genuinely smaller supported mechanism. Evaluate exact private channel, transfer limit, data integrity, origin verification, idempotency, failure atomicity, SQLite locking, cleanup, secret exposure, and repeatability after container replacement. Avoid a public upload endpoint or permanent third service. Do not assume Kudu/SSH/exec is available, safe or authorized; verify support offline or mark UNKNOWN.
3. **Select one minimal recommended procedure** and explicitly describe the necessary *policy amendment* to the previous no-upload/no-import/no-migration WP58 boundary. Scope it only to validated BTC/USD and ETH/USD staging observations, one controlled initialization/reinitialization, no provider reacquisition. If no secure feasible channel can be established, mark the path BLOCKED and give the smallest next evidence request instead of inventing a transfer method.
4. **Specify integrity and privacy safeguards:** dataset identity and schema/version checks; complete 1,000-per-symbol coverage and timestamp ordering/gap rules; validation of source/provenance and transform; SQLite transaction/atomic activation, permissions and ephemeral directory guard; no raw observations or credentials in public logs, PR, CLI arguments, Docker layers, registry, UI/browser or screenshots; bounded byte/request budgets and failure behavior. Clearly separate private source data from transformed chart responses.
5. **Lifecycle:** cold-start with no DB, successful initialization, repeated initialization, interrupted initialization, container replacement, lost DB, invalid/corrupt DB, failed API startup, and rollback. No automatic provider calls, silent synthetic fallback, restart loop, or claim of persistence. Define truthful unavailable-state UI and an owner-triggered reinitialization procedure.
6. **Promotion sequence:** identify exact accepted UI/API source candidates, source-only image build and compatible immutable digest qualification, safe settings/auth/HTTPS and port configuration, baseline capture and rollback, research API first, initialize and verify private data, then UI, then real BTC→ETH→BTC browser acceptance. Explain that if API image replacement erases state, initialization must occur **after** replacement. Distinguish successful HTTP health from authenticated dataset readiness and public chart verification. Keep actual cloud actions outside this run.
7. **Design tests and acceptance:** offline mock tests for initializer validation, wrong identity, missing data, partial transfer, corruption, no leakage, repeated init and rollback; future separately authorized bounded two-symbol authenticated snapshot checks; real staging UI interaction with 1,000 candles and SMA20 981 non-null values, null volume, no JS errors, no secret exposure; bounded lifecycle/reinitialization acceptance. Record which checks were actually run versus proposed.
8. **Owner approval proposal:** finish with **one** minimal, specific next authorization request (or a precise missing-evidence decision), including exact resources, operations, files/paths, maximum calls/bytes/time, private data handling, stop conditions, and rollback. Do not silently request broad permission to deploy or transfer data.

## Documentation deliverables

Update only:

- `docs/roadmap/release-1.15/RELEASE_1.15_WP58_PUBLIC_RUNTIME_QUALIFICATION_PLAN.md` — append **Phase E — Ephemeral SQLite Staging Initialization / Reinitialization Design** with source evidence, alternatives, chosen procedure, lifecycle matrix, explicit narrow policy amendment proposal, security constraints, sequence, tests, unresolved blockers and single next owner decision.
- `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` — short append-only Phase E summary linking to the detailed plan.

Keep PR #445 OPEN/UNMERGED; no other files or code changes. Preserve existing architecture and the user's decision to accept ephemeral SQLite. Do not introduce persistent-storage requirements or a third tier. Do not resurrect provider licensing gates.

## Validation and final response

Before any documentation commit: verify exact branch/PR head, no concurrent drift, changed-file allowlist, UTF-8 without BOM, LF, and `git diff --check`. Do not claim tests ran if only docs changed. Report actual head SHA and PR URL after publication, what was observed versus merely proposed, precise missing approvals, and status flags:

`LOCAL_CHART_VERIFIED=True` (historical)
`PUBLIC_RUNTIME_VERIFIED=False`
`liveAttemptReady=false`

**Stop** on identity mismatch, dirty unrelated working tree, missing safe private channel, unverified data source, unsupported Azure capability, or any action requiring fresh owner authorization. No deployment or data transfer in Phase E.

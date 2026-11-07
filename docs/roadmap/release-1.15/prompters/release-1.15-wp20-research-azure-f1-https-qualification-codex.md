# Codex prompt — Release 1.15 WP20: research-only Azure F1 HTTPS qualification

## Context and fixed evidence

Repository: `samuel-santos-engineer/AIQuantTradingResearch`, branch `main`. PR #409 (WP19 operator handoff) was squash-merged as `914c7bb8937c0d7c92fa62f590cfeb570981a9b9`; PR #408 (WP18 execution-policy gate) was merged as `87fae322f3b0ab8f7f0ac31d7e27d34bf2efa5d4`. The operator has independently completed the WP19 real-script and Python-helper **offline** handoff under effective `RemoteSigned`, with no parse errors and `liveControllerInvoked=false`. This proves local readiness only. The WP17 research candidate was deployed once, but its collector was blocked locally before any HTTP request; exact baseline rollback was independently verified. That attempt is consumed. Never reuse its one-attempt record or infer a successful research start.

State remains `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False`.

## Objective

Prepare a narrowly scoped **new** research-only staging attempt to establish whether the existing .NET research service responds over public Azure-terminated HTTPS. Use the merged WP18/WP19 operator workflow, a fresh one-attempt record and the existing previously approved immutable Linux/amd64 research image. Prefer actual implementation and a working HTTPS probe to further diagnosis or governance changes. Keep the existing UI unchanged.

## Authorization — critical

This prompt authorizes repository edits, local inspection, offline tests, a PR, and preparation of operator-run scripts. **It does not authorize live Azure deployment, mutation, Start/Stop, configuration changes, public HTTPS requests, snapshot requests, data acquisition, provider calls or imports.** Stop after preparing and testing the handoff and ask the owner for **separate explicit approval** of the exact research-only target, immutable image digest, new single attempt and rollback plan. Do not infer live approval from this prompt, PR merge, a preflight success or an approval-reference argument. If Codex cannot execute PowerShell due to sandbox/tool policy, create the exact reviewed `.ps1` scripts and a short command sequence for the owner to run in their own authorized PowerShell session; do not bypass restrictions or change policy. The owner may return sanitized results for analysis.

## Exact boundaries

- Research staging App Service: `aiqr115researchstagingwus27f5eabb5` in `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2, existing F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`.
- Public research host: `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net`; intended health `GET /health`, exact success HTTP 200 body `Alive` (valid TLS, no redirect). Research endpoint `POST /v1/research/snapshot` is gated on health success.
- UI app `aiqr112wp05wus27f5eabb5` is **out of scope**: no changes, deployment or acceptance claims.
- Existing approved image digest from merged WP19: `sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f`. Privately verify the exact registry/repository image reference, digest, Linux/amd64 and available artifact before requesting approval; never rebuild, retag or push.
- Preserve current UID, storage, auth, TLS/HTTPS, ports, non-durable staging and dataset guards. No synthetic candles, auto-seeding, provider requests, credentials in source/logs, new resources or paid tier.
- Preserve all 37 owner files and existing unrelated files. Do not alter existing policy or create licensing prerequisites.

## Work to perform before requesting live approval

1. Inspect `eng/azure-cli/r1.15-qualification/wp18_research_launcher.py`, `wp19_operator_handoff.py`, `eng/docker/release-1.15/Test-Wp19OperatorPreflight.ps1`, `Invoke-Wp19ResearchHandoff.ps1`, `Test-Wp07StagingChartReadiness.ps1`, `wp16_research_readiness.py`, and the WP19 runbook. Verify paths and exact CLI contract from current `main` rather than guessing.
2. Identify the original private WP17 controller's responsibilities but **do not reuse its consumed attempt record**. If it is not accessible to Codex, write an explicit operator handoff specifying the required fresh, privately reviewed controller and recovery procedure; do not invent or claim qualification. If a repository-side controller is appropriate, implement a fresh one only after careful offline review of the existing guards, with strict one-attempt state and no automatic execution. Avoid duplicating Azure control logic or creating a broad orchestrator.
3. Create a short, reproducible **offline-only** operator script/runbook that checks the merged WP19 preflight, policy, script provenance/parse status, interpreter resolution, and verifies that the actual collector path runs. A failure must prevent any deployment. Never use `-ExecutionPolicy Bypass`, `Set-ExecutionPolicy`, `Invoke-Expression`, profile changes, or alternate identities to circumvent a restriction.
4. Specify the exact live procedure **for later separately approved owner execution**, not for immediate execution: privately capture and compare the full current baseline (image/startup, app settings, logging, HTTPS-only, slots, Running state), validate immutable artifact and exact target, create a fresh one-attempt record, invoke the guarded handoff, and independently restore/compare the original baseline after the attempt even if the candidate is healthy. If any baseline query fails, stop before mutation.
5. After Start, launch the existing bounded WP16 collector immediately: up to 3 HTTPS `GET /health` attempts, each at most 60 seconds within a 180-second overall window; 190-second parent bound. Distinguish process alive from dataset availability. Do not attribute collector failures to Azure startup without HTTP evidence.
6. Only after exact valid health: optionally one **unauthenticated** `POST /v1/research/snapshot` expecting 401; then at most one authenticated snapshot per BTC/USD and ETH/USD **only if** a secure private credential channel is already available and separately authorized. Never print or persist credentials or response candle payloads. If unavailable, skip and report `AUTHENTIC_DATA_NOT_IMPORTED`; a 503 `DATASET_NOT_READY` is an expected guarded response when data is missing, not a service crash. Do not call Twelve Data or import datasets.
7. Implement focused offline tests for local preflight refusal, no live calls without approval, fresh-attempt exclusivity, exact baseline/rollback sequencing, bounded health invocation, no snapshots before valid health, sanitization and safe failure. Keep UTF-8 without BOM and LF, and run diff checks.
8. Create a focused PR **for owner review**, leave it open/unmerged. Report exact files, tests, immutable image verification status, missing private controller/credential dependencies, and a concise operator command sequence. If no code change is necessary, say so rather than create an artificial PR.

## Acceptance and truthful reporting

- A passing offline preflight is `LOCAL_PREFLIGHT_PASS`, not `RESEARCH_PROCESS_ALIVE`.
- Only an observed valid HTTPS 200 `Alive` during a separately approved live attempt may establish `RESEARCH_PROCESS_ALIVE=True` for that attempt.
- Authenticated 503 `DATASET_NOT_READY` means process/API route is reachable but authentic historical data is absent; never substitute synthetic data.
- `PUBLIC_RUNTIME_VERIFIED=False` until authentic historical BTC/USD and ETH/USD candles and indicators are visibly accepted in the public browser UI. Research-only HTTPS health cannot qualify public chart acceptance.
- On any live failure after approval, stop additional probes, restore and independently compare exact original baseline; record only sanitized outcomes. If recovery cannot be verified, stop and escalate; never claim rollback success without independent evidence.

## Deliverables

A concise WP20 operator-run script/runbook and focused offline tests, plus an open implementation PR only if changes are needed. End with a **separate approval request** stating exact artifact, research app, single-attempt scope, rollback requirements and what will be observed. Do not execute the live attempt under this prompt.

# Codex prompt — Release 1.15 WP18: PowerShell execution-policy preflight

## Context and authority

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Work from current `main` (PR #407 merged at `509e1f37d7414cfbd5fe6d1309110e625f4d7114`) and inspect current repository state before editing. Release 1.15 uses Streamlit -> authenticated HTTPS/JSON -> .NET 10 research API on existing Azure F1 staging. The WP17 research-only deployment applied the image and configuration, but the PowerShell execution policy blocked the health collector **before any HTTP request**. The original research configuration, image, startup, settings, logging, HTTPS-only, slots and Running state were independently restored. No Azure startup failure has been established. The single deployment attempt is consumed. No authentic data import or public chart acceptance has occurred.

**Authorization: repository-only implementation and offline validation.** Do not mutate Azure, deploy or publish images, run live application probes, fetch provider data, import candles, change the UI, merge PRs, or modify execution policy at machine/user/process scope as a workaround. Preserve all owner dirty/untracked files and avoid changing unrelated files.

## Objective

Make the research-only deployment/readiness launcher fail *before any Azure mutation* if its PowerShell execution prerequisites are not satisfied, and provide reliable, narrowly scoped offline validation that the intended readiness invocation is actually executable under the applicable policy. The preflight must not rely merely on a reported `Get-ExecutionPolicy` value when an effective policy or script-launch restriction can still prevent execution.

## Tasks

1. Inspect the current WP17 deployment/runbook workflow, the PowerShell 5.1 readiness entry point (`eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1`), its Python helper (`eng/azure-cli/r1.15-qualification/wp16_research_readiness.py`), and any deployment orchestration scripts. Locate the exact command that failed and identify whether policy applied to the parent script, child script, or both. Do not infer the mechanism without evidence.
2. Implement the smallest safe preflight at the earliest applicable boundary, **before any Azure write**. Check the effective policy and relevant precedence (MachinePolicy/UserPolicy/Process/CurrentUser/LocalMachine), then prove the actual approved script-launch route works using a no-network/no-Azure/no-secret dry-run. Prefer invoking the real readiness script without `-ProbeResearch`, or an equivalent harmless entry path, rather than a fabricated success flag. Never use `-ExecutionPolicy Bypass`, `Set-ExecutionPolicy`, registry edits, profile modifications, or an alternative interpreter to defeat a policy restriction. If the organization's policy disallows the script, stop and provide a policy-compliant manual remediation path for the owner/administrator.
3. Ensure the preflight is unconditionally enforced in the documented deployment launcher and cannot be skipped by calling a lower-level deployment step through the supported workflow. If no single deployment launcher exists, add a minimal explicit, fail-closed pre-deployment gate and update the operator sequence; do not claim an unenforceable documentation check is a technical guard.
4. Keep all existing deployment safety boundaries: exact research App Service only; privately captured and independently compared rollback baseline; unchanged storage, authentication and transport guards; already-published immutable research image; no UI change; up to three anonymous `/health` requests within 180 seconds **only in a separately authorized future deployment**; snapshots only after health success; exact independent rollback verification. No new live authority is granted by this work package.
5. Add focused offline tests, including PowerShell 5.1 parsing, policy precedence, restrictive effective policies, blocked child invocation, missing Python/interpreter, safe dry-run, no network/Azure calls, no secrets in output, and proof that any failed preflight prevents the deployment step. Test the realistic launcher path under available supported environments. Do not relax policy or skip tests to get a green result.
6. Update the existing Release 1.15 runbook concisely with the root cause (local execution-policy block), operator preflight, expected safe failure, exact manual recovery guidance, and separation between a successful local preflight and unverified Azure process health. Include exact commands only if they are genuinely safe and supported.
7. Open a narrowly scoped PR for owner review; leave it **OPEN/UNMERGED**. Report changed files, test commands/results, current head, whether any Azure operations occurred, and any unresolved limitations. Preserve and report owner working-tree state.

## Acceptance criteria

- A supported deployment path refuses to apply any Azure change unless the real PowerShell readiness invocation is locally executable under effective policy.
- No automatic execution-policy changes, bypass flags, Azure mutations, live HTTP probes, image publications, provider requests, or imports.
- Restricted-policy and child-script-block scenarios fail closed with a sanitized, actionable error.
- Existing 180-second readiness and rollback semantics are unchanged.
- Focused offline tests pass, with UTF-8 (no BOM), LF, PowerShell 5.1 compatibility and `git diff --check` verified.
- PR is open for review, not merged.

## Reporting flags

`AZURE_DEPLOYMENT_NOT_RUN` (WP18); `RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.

After the owner reviews and merges the WP18 correction, request **separate explicit authorization** before any further research-only Azure deployment.

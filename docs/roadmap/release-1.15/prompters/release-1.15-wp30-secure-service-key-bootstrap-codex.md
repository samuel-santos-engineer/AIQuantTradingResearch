# Release 1.15 — WP30 Codex Prompt: Secure Service-Key Bootstrap (Planning and Offline Implementation)

## Starting point

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `origin/main` containing merged PR #417, squash commit `2d5169de33c295dacce7db576f4ed3497abb8621`. Confirm the actual HEAD before changes. Do not rewrite historical attempts, approvals, observations, or baseline evidence.

The owner has **never created** `AIQ_RESEARCH_API_KEY`. A successful operator read-only qualification for attempt `2683eeb5-903c-4434-b262-70452fb3040e` confirmed matching captures and valid baseline; `serviceKeyChannelChecked=true`, `serviceKeyChannelAvailable=false`. This attempt is historical, disabled, and must never be reused or armed. `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`. No live controller, API health probe, or browser acceptance has occurred.

## Objective

Prepare a **reviewable, offline-tested, secure bootstrap procedure** for a single new research API shared secret, using `AIQ_RESEARCH_API_KEY` in the Azure research API App Service and `AIQ_WP21_SERVICE_KEY` in the authorized Windows operator session. These are two names for the **same secret**, not two independently generated secrets. Avoid introducing a new secret-management service, paid infrastructure, or new governance/licensing gates.

**This work package authorizes code, tests, documentation, and a PR for review only. It does not authorize changing Azure configuration, running Azure commands, generating or receiving the owner's real secret, deploying, invoking the live controller, acquiring provider data, or starting a live attempt.** Do not request the owner to paste key material into Codex, ChatGPT, GitHub, files, or logs.

## Investigation before coding

1. Read the merged WP21/WP22/WP29 implementation and runbooks, especially `eng/azure-cli/r1.15-qualification/wp21_runtime.py`, `eng/azure-cli/r1.15-qualification/wp22_preparation.py`, `eng/docker/release-1.15/Invoke-Wp29ReadOnlyQualification.ps1`, and the research API authentication code/configuration.
2. Verify precisely how the research API reads `AIQ_RESEARCH_API_KEY`, whether it is startup-only, how missing/invalid keys are handled, and whether any operator workflow currently reads `AIQ_WP21_SERVICE_KEY`. Do not assume a key-presence check proves that the deployed API accepts the key.
3. Determine whether the research App Service's `AIQ_RESEARCH_API_KEY` setting exists (only from existing sanitized evidence/source; **do not query Azure**). Preserve all unrelated app settings, slot flags, image/startup values, HTTPS-only, plan, and UI App Service.
4. Check existing secret-safe PowerShell patterns and tests. Avoid external dependencies or requiring Azure CLI authentication in offline tests.

## Deliverables

Create the smallest coherent change set, preferably an operator runbook and offline-verifiable PowerShell helper(s), with regression tests. The procedure should:

- Generate a **single** cryptographically secure 32-byte or stronger key **locally in the owner's authorized Windows session**, without writing it to source, disk, PowerShell history, transcripts, process arguments, command output, exception messages, or PR artifacts. Prefer secure input/secret-handling practices; explicitly document unavoidable process/environment exposure and its lifetime.
- Prepare `AIQ_WP21_SERVICE_KEY` for only the required operator process/session, avoiding permanent user/machine environment changes. Do not print, hash-and-print, echo, or log secret material. Do not embed a key in command-line arguments, shell history, a script file, or Azure CLI `--settings KEY=VALUE` arguments.
- Document a **separately owner-approved** Azure App Service configuration step for `AIQ_RESEARCH_API_KEY` that avoids exposing the value through shell arguments or logs, retains unrelated settings and slot behavior, and includes safe rollback/recovery of the setting. Do not silently assume `az webapp config appsettings set` is secret-safe: assess its argument and telemetry exposure; if there is no demonstrably safe automated path, document an owner-only secure portal entry workflow instead. Do not use an invented portal feature or claim the value is hidden without evidence.
- Explain that adding an App Service setting is a **live Azure configuration mutation** and may restart the App Service, invalidating an earlier read-only baseline. Require a **fresh** read-only qualification after an approved change, with a new disabled attempt and no historical attempt reuse.
- Distinguish **key presence** from **authenticated API success**. A future authenticated API probe requires separate explicit authorization, and must not disclose the key or response bodies. Do not claim readiness from presence alone.
- Preserve strict fail-closed behavior, exact existing safety fields, historical records, controller/recovery implementation, and all unrelated Azure resources. No live approval or deployment authorization.
- Provide concise owner-facing instructions for generating and retaining the secret in a secure, recoverable manner (without instructing the owner to paste it into chat). Include a plan for accidental exposure/rotation.

## Offline validation

Run relevant Python tests and PowerShell parser/static checks available in the Codex environment. Add tests that establish: secure generation strength and non-disclosure; missing-key fail-closed; no secret in stdout/stderr/JSON/errors; no persistence to repository files; no Azure calls in offline mode; no mutation or approval without explicit operator action; no changes to existing controller/recovery semantics. Do not fake a live Azure success. If Windows PowerShell 5.1 or installed Azure CLI is unavailable, report the corresponding test as not run, rather than passed.

Avoid unneeded new key-handling code if a carefully documented, secure operator-only workflow is safer. Never include an actual key or example that resembles a real credential in tests or documentation.

## PR and report

Open **one focused PR**, leave it **unmerged**, and include: exact base/head SHAs, changed files, tests passed/skipped, security tradeoffs, proposed operator instructions, and precise future owner approval boundary. If a safe approach cannot be demonstrated, open a documentation-only PR explaining the constraints and recommended secure manual workflow. Do not create or arm a private attempt as part of WP30.

Return only sanitized status. Keep `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`. The prior read-only qualification passed; service-key availability remains **unavailable until an authorized future operator check actually establishes it**.

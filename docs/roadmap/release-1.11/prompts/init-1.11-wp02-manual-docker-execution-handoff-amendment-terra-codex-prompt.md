# INIT-1.11 WP02 — Manual Docker Execution Handoff Amendment

## Model assignment
- **GPT-5.6 Luna** — contract, policy, governance, acceptance criteria.
- **GPT-5.6 Terra** — PRIMARY: orchestration, command generation, evidence evaluation, validation, and authorized execution.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

## Purpose
Amend the pending WP02 tooling/runtime process for the verified VS Code Codex sandbox limitation. Codex runs as `sam-laptop-hp\codexsandboxoffline`, which cannot access Docker Desktop/WSL, while Docker works from the interactive Windows `sabsf` PowerShell session.

This amendment applies to the pending Interactive Tooling Runtime Verification Authority and later Docker/WSL operations in the existing WP02 Execution Authority Amendment.

## Governed execution model
**Terra/Codex = orchestrator, command author, evidence evaluator, acceptance authority.**

**Interactive `sabsf` PowerShell = execution transport for Docker/WSL commands blocked by the Codex sandbox.**

Required marker:
`AZURE F1 WP02 — MANUAL DOCKER EXECUTION HANDOFF: AUTHORIZED`

## Mandatory handoff protocol
For each handed-off operation Terra must:
1. state why handoff is required;
2. provide an exact copy/paste-ready PowerShell command block;
3. resolve known values rather than using avoidable placeholders;
4. state whether commands are read-only or create temporary artifacts;
5. include labeled exit-code capture where practical;
6. state exactly what stdout/stderr/evidence the user must return;
7. STOP before dependent work and wait;
8. independently evaluate returned evidence;
9. request only missing evidence when incomplete;
10. never infer PASS merely because commands were issued or reportedly executed.

User-executed commands count as real mutations in the audit.

## First handoff
Request this from interactive `sabsf` PowerShell:

```powershell
whoami

az version
Write-Host "AZ_EXIT_CODE=$LASTEXITCODE"

docker version
Write-Host "DOCKER_VERSION_EXIT_CODE=$LASTEXITCODE"

docker info
Write-Host "DOCKER_INFO_EXIT_CODE=$LASTEXITCODE"

wsl --status
Write-Host "WSL_STATUS_EXIT_CODE=$LASTEXITCODE"

wsl -l -v
Write-Host "WSL_LIST_EXIT_CODE=$LASTEXITCODE"
```

Evaluate:
- identity is `sabsf`;
- Azure CLI works;
- Docker client/server reachable;
- Linux-container capability present;
- WSL does not show the prior access-denied failure.

Then STOP until evidence is returned.

## Second handoff
Only after the first evidence passes, generate an exact deterministic PowerShell block that, outside the repository:
- creates a unique temp directory;
- writes minimal non-secret Dockerfile/test content;
- builds exactly one temporary Linux image;
- runs one temporary container;
- proves deterministic behavior;
- proves Linux runtime;
- captures labeled exit codes;
- removes container;
- removes image;
- removes temporary directory;
- verifies cleanup.

Use a trusted minimal base-image path. No registry push.

STOP until the user returns complete stdout/stderr and exit-code evidence.

## Azure boundary
Azure CLI commands should run directly in Codex whenever possible.

This amendment does NOT authorize manual Azure resource mutation. If Azure commands later cannot run inside Codex, require a separate explicit manual Azure-command handoff decision.

Do not run `az login` during tooling verification.

## Security boundary
Never instruct the user to weaken named-pipe ACLs, WSL security, Defender, firewall, Smart App Control, Docker security, BIOS/UEFI, or expose Docker over insecure TCP.

## Repository/GitHub/cloud boundary
During tooling verification:
- repository/Git mutations = 0;
- GitHub mutations = 0;
- Azure mutations = 0;
- registry pushes = 0;
- Twelve Data requests = 0;
- #253 remains Open/Todo;
- milestone #62 remains Open.

Strict recurring infrastructure cost remains `$0.00`.

## Acceptance
When returned evidence proves interactive `sabsf`, Azure CLI, Docker server, Linux-container build/run, and complete temporary cleanup, emit:

`AZURE F1 WP02 TOOLING — INTERACTIVE USER CONTEXT: PASS`
`AZURE F1 WP02 TOOLING — EXISTING INSTALLATIONS: PASS`
`AZURE F1 WP02 TOOLING — LINUX CONTAINER BUILD/RUN: PASS`
`AZURE F1 WP02 TOOLING — TEMPORARY ARTIFACT CLEANUP: PASS`
`AZURE F1 WP02 TOOLING — INTERACTIVE VERIFICATION MUTATION AUDIT: PASS`
`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITES: PASS`
`AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`

Then complete the pending tooling authority. Resume the existing WP02 Execution Authority Amendment afterward; do not create a new WP02 scope contract.

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

## Exact completion terminal
`AZURE F1 WP02 — MANUAL DOCKER EXECUTION HANDOFF AMENDMENT COMPLETE`

## Block
Block the affected step if the user terminal is not `sabsf`, Docker remains unavailable there, evidence is insufficient, security weakening would be required, or the requested operation exceeds Docker/WSL scope.

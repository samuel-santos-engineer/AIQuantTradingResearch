# INIT-1.11 WP02 — Execution Authority Amendment
## Minimal Docker + Azure App Service F1 Execution Probe

### Model authority
- **GPT-5.6 Luna** — contract, policy, architecture, governance, reconciliation, acceptance criteria.
- **GPT-5.6 Terra** — PRIMARY selected execution model: implementation, validation execution, approved Git/GitHub/Azure mutations, evidence evaluation, lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, exploratory/non-authoritative review; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

---

## 1. Mission

Execute **WP02 — Minimal Docker + App Service F1 Execution Probe** for:

**Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification**

Prove empirically that the frozen candidate:

**Azure App Service Linux F1 + custom Docker + persistent `/home`**

can satisfy the WP02 execution requirements without changing production architecture and without violating the strict recurring infrastructure cost constraint:

**`$0.00`**

This authority supersedes conflicting operational bindings in older WP02 execution prompts, while preserving the frozen Initiative-1.11 feasibility contract and WP01 governance.

---

## 2. Canonical baseline

Repository/publication baseline:
- Initiative governance PR #258 is merged.
- Merge/main anchor: `62a7e36eb3064982f3dbfd16b065f3cb8b75c524`.
- Published governance package:
  - `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_EXECUTION_PLAN.md`
  - `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_CONTRACT.md`
  - `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`
  - `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FILE_MANIFEST.md`

GitHub governance:
- milestone #62:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`
- #252 — WP01 — Closed/Done.
- #253 — WP02 — Open/Todo, Release unset.
- #254 — WP03 — Open/Todo.
- #255 — WP04 — Open/Todo.
- #256 — WP05 — Open/Todo.
- #257 — WP06 — Open/Todo.
- Project #2 governs lifecycle.
- `Initiative-1.11 ≠ Product Release 1.11`.
- No Project Release `1.11` is authorized.
- Product Release 2.0 scope remains ML.

Dependency:
`WP01 → WP02 → WP03 → WP04 → WP05 → WP06`

No Phase B/public-reference implementation is authorized before WP06 returns FEASIBLE.

---

## 3. Prerequisite status

The local tooling prerequisite is accepted:

`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITES: PASS`

Verified evidence includes:
- interactive Windows identity `sabsf`;
- Azure CLI `2.89.1`;
- Docker Desktop Linux engine reachable from interactive `sabsf`;
- WSL 2 with Linux/Docker runtime available;
- one temporary Linux image successfully built;
- one temporary container successfully emitted:
  `AIQ_WP02_LINUX_CONTAINER_OK`;
- temporary image/container/files cleaned;
- repository/Git/GitHub/Azure mutations zero during tooling proof.

Therefore:

`AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`

---

# 4. Binding Docker/WSL execution boundary

The VS Code Codex execution sandbox runs under:

`sam-laptop-hp\codexsandboxoffline`

and cannot access Docker Desktop's named pipe or WSL runtime.

Therefore the following execution model is **binding**:

**Terra/Codex = orchestrator, exact-command author, evidence evaluator, acceptance authority.**

**`sabsf` PowerShell executes Docker/WSL commands.**

Terra MUST NOT attempt to treat the inaccessible Codex Docker runtime as the authoritative Docker execution environment.

## Mandatory manual handoff protocol

Whenever WP02 requires Docker or WSL execution, Terra MUST:

1. state the purpose of the operation;
2. provide the exact copy/paste-ready PowerShell command block for the interactive `sabsf` terminal;
3. resolve all already-known values and avoid unnecessary placeholders;
4. label commands that create, change, or delete resources;
5. capture and label stdout/stderr and exit codes where practical;
6. state exactly what evidence the user must return;
7. **STOP and wait for the user's results**;
8. evaluate the returned stdout/stderr, exit codes, identifiers, and observable results;
9. request only missing evidence if evidence is incomplete;
10. proceed only after the evidence proves the current gate;
11. **never infer success** because a command was supplied, reportedly run, or expected to work.

User-executed Docker/WSL commands are real execution and must be counted in the final mutation audit.

Do not ask the user to weaken named-pipe ACLs, WSL permissions, Defender, firewall, Smart App Control, Docker security, BIOS/UEFI, or expose Docker through insecure TCP.

### Azure boundary

Azure CLI commands should be executed directly by Terra/Codex when the available environment permits them.

The Docker/WSL handoff does **not** silently authorize manual Azure mutation.

If an Azure CLI mutation cannot execute from Terra's environment, Terra must stop and explicitly identify the blocked Azure command and required evidence. Do not silently transform the Docker handoff into a general cloud-mutation delegation.

Interactive Azure authentication may require user participation. Never request, print, or persist credentials/tokens.

---

# 5. WP02 scope

WP02 proves only the minimal App Service F1 execution substrate.

Required empirical probes:

1. **F1 availability**
2. **custom Docker execution**
3. **public HTTPS reachability**
4. **persistent `/home` write/read**
5. **persistence across App Service restart**
6. **persistence across container recycle/restart**
7. **persistence across image redeployment**
8. **complete Azure resource inventory**

WP02 does **not** prove:
- SQLite correctness;
- SQLite locking behavior;
- WAL suitability;
- Twelve Data connectivity;
- production workload capacity;
- ML;
- trading;
- backtesting;
- production SLA;
- Azure SQL;
- paid scaling.

Those belong to later WPs or initiatives.

---

# 6. Phase A — governance and repository preflight

Before Azure mutation, Terra must verify read-only:

- current repository state;
- `main` / `origin/main` relationship;
- Initiative-1.11 governance artifacts exist on `origin/main`;
- WP01 acceptance remains valid;
- #252 Closed/Done;
- #253 Open/Todo and Release unset;
- #254–#257 remain Open/Todo;
- milestone #62 remains Open;
- no product Release 1.11 assignment exists;
- staging is empty;
- pre-existing untracked control artifacts are preserved and not staged.

Do not modify production source code for WP02.

Required marker:

`AZURE F1 WP02 — GOVERNANCE & REPOSITORY PREFLIGHT: PASS`

---

# 7. Phase B — official Azure documentation refresh

Before creating resources, refresh the relevant current Microsoft documentation and record evidence sufficient to support:

- App Service custom Linux containers;
- Free F1 App Service plan availability/constraints;
- persistent `/home` behavior for Linux custom containers;
- `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`;
- relevant F1 CPU/storage/resource constraints;
- custom-container deployment/redeployment mechanics.

Prefer Microsoft Learn/Azure official sources.

Do not substitute assumptions from earlier planning for current documentation.

Required marker:

`AZURE F1 WP02 — AZURE DOCUMENTATION REFRESH: PASS`

---

# 8. Phase C — Azure authentication, subscription, and strict-$0 gate

Verify:
- Azure CLI authentication status;
- intended subscription;
- subscription is enabled/usable;
- target region candidates;
- F1 App Service availability in the selected region;
- no paid App Service plan/SKU is selected;
- no Azure Files;
- no Azure Container Apps;
- no mandatory ACR;
- no paid registry dependency;
- no unrelated resource creation.

If authentication requires interactive user action, provide the exact safe command/instruction, STOP, and wait for sanitized confirmation.

Do not expose access tokens, subscription secrets, or credentials in output.

Before any Azure resource creation, emit:

`AZURE F1 WP02 — STRICT-ZERO-COST PRE-MUTATION GATE: PASS`

If the probe cannot be performed without recurring infrastructure cost greater than `$0.00`, stop with NOT FEASIBLE/BLOCKED evidence as appropriate.

---

# 9. Phase D — minimal probe image

Use a minimal purpose-built probe image, not the production application architecture.

The probe must be capable of:
- serving an HTTP response suitable for App Service;
- reading/writing a marker under `/home`;
- reporting deterministic non-secret probe state sufficient to verify persistence;
- exposing no credentials;
- requiring no database/provider/Twelve Data access.

Docker/WSL build and local verification MUST use the `sabsf` PowerShell handoff protocol.

Terra must generate exact commands and STOP for returned evidence.

No Docker success may be inferred from the earlier tooling proof; the actual WP02 probe image must receive its own evidence.

Required marker:

`AZURE F1 WP02 — MINIMAL PROBE IMAGE: PASS`

---

# 10. Phase E — image distribution

Use a distribution mechanism compatible with strict `$0.00`.

Do not make Azure Container Registry mandatory.

Prefer an already-authorized/free registry path such as GHCR when required by App Service and compatible with the governance constraints.

Do not expose registry credentials.

If Docker login/tag/push must execute through `sabsf`, Terra must:
- provide exact commands;
- identify any credential-sensitive command separately;
- avoid asking the user to paste secrets back;
- STOP for sanitized evidence;
- validate the pushed image reference/digest without receiving secret material.

Any user-executed image/tag/push mutations must be counted.

Required marker:

`AZURE F1 WP02 — PROBE IMAGE DISTRIBUTION: PASS`

---

# 11. Phase F — Azure F1 deployment

Create only the minimum resources authorized by the WP01 resource plan and required for the WP02 probe.

The App Service configuration must establish:
- Linux custom-container execution;
- F1/free App Service plan;
- public HTTPS endpoint;
- persistent App Service storage enabled for `/home`;
- no Azure Files;
- no Container Apps;
- no paid scaling;
- no unrelated services.

Record exact created resource names/types/region/SKU and relevant non-secret configuration.

Required marker:

`AZURE F1 WP02 — APP SERVICE F1 DEPLOYMENT: PASS`

---

# 12. Phase G — eight empirical probes

Terra must execute and retain evidence for each probe.

## Probe 1 — F1 availability
Prove the deployed App Service plan is actually F1/free.

Marker:
`AZURE F1 WP02 PROBE 1 — F1 AVAILABILITY: PASS`

## Probe 2 — custom Docker
Prove App Service is running the intended custom container/image.

Marker:
`AZURE F1 WP02 PROBE 2 — CUSTOM DOCKER: PASS`

## Probe 3 — public HTTPS
Prove the public HTTPS endpoint returns the expected deterministic probe response.

Marker:
`AZURE F1 WP02 PROBE 3 — PUBLIC HTTPS: PASS`

## Probe 4 — persistent `/home`
Write a unique non-secret marker under `/home`, then prove it can be read back.

Marker:
`AZURE F1 WP02 PROBE 4 — PERSISTENT HOME: PASS`

## Probe 5 — App Service restart persistence
Perform an App Service restart through an authorized Azure operation, wait for healthy return, then prove the same `/home` marker persists.

Marker:
`AZURE F1 WP02 PROBE 5 — APP RESTART PERSISTENCE: PASS`

## Probe 6 — container recycle persistence
Cause or observe a container recycle/restart in a controlled authorized way distinguishable from the prior check, then prove the `/home` marker persists.

Marker:
`AZURE F1 WP02 PROBE 6 — CONTAINER RECYCLE PERSISTENCE: PASS`

## Probe 7 — image redeployment persistence
Redeploy a new probe image revision/tag/digest sufficient to demonstrate image redeployment, then prove the pre-existing `/home` marker remains.

Docker build/tag/push operations must use `sabsf` PowerShell when required by the sandbox boundary. Terra supplies exact commands, STOPS, and validates returned evidence.

Marker:
`AZURE F1 WP02 PROBE 7 — IMAGE REDEPLOYMENT PERSISTENCE: PASS`

## Probe 8 — resource inventory
Enumerate all Azure resources created or used specifically for WP02 and verify there are no unauthorized dependencies.

Marker:
`AZURE F1 WP02 PROBE 8 — RESOURCE INVENTORY: PASS`

---

# 13. Resource disposition and cleanup

Default disposition is cleanup.

Retention for WP03 is permitted only if:
- the published WP01 resource plan permits it;
- retaining the exact resources is useful for WP03;
- recurring infrastructure cost remains exactly `$0.00`;
- the retained inventory is explicit;
- no resource outside the authorized feasibility footprint is retained.

Otherwise delete WP02-created Azure resources and verify deletion.

Docker/local temporary artifacts not needed for evidence must be cleaned.

Report:
- Azure resources created;
- Azure resources updated;
- Azure resources deleted;
- Azure resources retained for WP03;
- Docker images built/tagged/pushed/deleted;
- containers created/run/deleted;
- temp files/directories created/deleted;
- registry artifacts retained/deleted.

Required marker:

`AZURE F1 WP02 — RESOURCE DISPOSITION: PASS`

---

# 14. Strict-$0 evidence

WP02 must report the resource/SKU configuration and available evidence supporting the strict recurring infrastructure constraint.

Do not claim final initiative-wide cost qualification; WP05 owns the complete resource-envelope and strict-$0 qualification.

For WP02, no known recurring infrastructure charge may be introduced.

Required marker:

`AZURE F1 WP02 — ZERO-COST CONSTRAINT PRESERVED: PASS`

---

# 15. Mutation audit

Report exact mutations actually performed, including operations manually executed by the user under Terra's command handoff.

At minimum account for:
- repository file mutations;
- Git staging/commits/branches/pushes;
- GitHub issue mutations;
- Project mutations;
- milestone mutations;
- Azure resources created/updated/deleted/retained;
- Docker images built/tagged/pushed/deleted;
- Docker containers created/run/deleted;
- registry artifacts;
- temporary local files/directories;
- Twelve Data requests;
- tags/releases.

Do not report user-executed Docker operations as zero.

Required marker:

`AZURE F1 WP02 — MUTATION AUDIT: PASS`

---

# 16. Acceptance gate

WP02 may PASS only when all eight probes pass and the resource disposition, cost constraint, evidence, and mutation audit are complete.

Exact WP02 acceptance marker:

`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

Do not emit this marker early.

A genuine inability of Azure App Service Linux F1 to satisfy the frozen feasibility contract is a valid evidence-bearing outcome. Distinguish environmental/tooling/authentication BLOCKED from architectural/platform NOT FEASIBLE.

---

# 17. Mandatory GitHub lifecycle completion after PASS

Only after the exact acceptance marker has been emitted:

1. close GitHub issue **#253**;
2. ensure its Project #2 Status is **Done**;
3. if GitHub/Project automation already changed Status to Done as a consequence of closing the issue, do not perform a redundant Project Status mutation;
4. count only explicit mutations actually performed;
5. verify #253 is Closed/Done;
6. leave milestone #62 Open;
7. leave #254 Open/Todo;
8. do not advance WP03 execution before WP02 lifecycle completion is verified.

Required marker:

`AZURE F1 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`

---

# 18. WP03 handoff

After WP02 PASS and #253 Closed/Done, the next work package is:

**WP03 — Persistent SQLite Filesystem, Locking & Journal Qualification**

GitHub issue: **#254**

Do not execute WP03 under this authority.

Required handoff marker:

`AZURE F1 WP03 — EXECUTION AUTHORITY: READY`

---

# 19. Required final model marker

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# 20. Terminal outcomes

## Success
After WP02 PASS and GitHub lifecycle completion:

`AZURE F1 WP02 — EXECUTION AUTHORITY AMENDMENT COMPLETE`

## Blocked
Use when execution cannot continue because of authentication, permissions, tooling, missing evidence, transient service limitation, or another remediable execution dependency:

`AZURE F1 WP02 — EXECUTION AUTHORITY AMENDMENT BLOCKED`

## Not feasible
Use when empirical evidence establishes that the frozen Azure App Service Linux F1 candidate cannot satisfy a required WP02 feasibility condition without violating the frozen constraints:

`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: NOT FEASIBLE`

Do not close #253 as successfully completed unless the exact PASS marker was reached.

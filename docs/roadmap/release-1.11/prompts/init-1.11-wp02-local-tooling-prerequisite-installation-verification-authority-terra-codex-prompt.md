# INIT-1.11 WP02 — Local Tooling Prerequisite Installation & Verification Authority

## Model assignment
- **GPT-5.6 Luna** — contract/policy/governance/acceptance owner.
- **GPT-5.6 Terra** — PRIMARY: approved local tooling installation, configuration needed for tool execution, and verification.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

# Mission
Remove the narrow environmental blocker preventing WP02 execution by installing and verifying the minimum local tooling required for:

**WP02 — Minimal Docker + App Service F1 Execution Probe**

Required tools:
1. Azure CLI (`az`);
2. Docker tooling sufficient to build/run/tag a minimal Linux container image and later interact with an approved registry path.

This authority is **local-tooling-only**.

It does not execute WP02 and does not authorize Azure resource mutations.

# Canonical baseline
Published Initiative-1.11 governance baseline:
- PR #258 merged;
- merge commit:
  `62a7e36eb3064982f3dbfd16b065f3cb8b75c524`;
- four governance artifacts present on `origin/main`.

Lifecycle:
- #252 WP01 Closed/Done;
- #253 WP02 Open/Todo, Release unset;
- #254–#257 Open/Todo;
- milestone #62 remains:
  `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`.

Binding:
`Initiative-1.11 ≠ Product Release 1.11`

# Triggering blocker
The WP02 Execution Authority Amendment blocked before Azure mutation because:
- Azure CLI (`az`) unavailable;
- Docker unavailable.

No Azure/GitHub/repository mutations occurred.

This authority addresses only that blocker.

# Phase A — host/tooling preflight
Before installation, determine and report:
- OS edition/version/build;
- architecture;
- shell;
- privilege/elevation availability;
- package managers/install mechanisms available;
- whether `az` exists anywhere but is absent from PATH;
- whether Docker CLI/engine/Desktop exists anywhere but is absent from PATH;
- virtualization/container prerequisites relevant to the host;
- disk-space sanity sufficient for the minimum install;
- whether corporate/device policy blocks installation.

Do not expose private machine identifiers unnecessarily.

Do not change BIOS/firmware/security policy.

Required marker:
`AZURE F1 WP02 TOOLING — HOST PREFLIGHT: PASS`

# Phase B — authoritative installation source verification
Use current official vendor documentation before installation.

Azure CLI:
- Microsoft official installation documentation/packages only.

Docker:
- Docker official documentation/distribution or an already-approved native Windows package source resolving to the official Docker distribution.

Do not use:
- unofficial binaries;
- random mirrors;
- curl-pipe-shell from unverified sources;
- repackaged installers of uncertain provenance.

Record:
- official source;
- selected install method;
- expected package/product identity.

Required marker:
`AZURE F1 WP02 TOOLING — INSTALL SOURCE VERIFICATION: PASS`

# Phase C — Azure CLI installation
Install the minimum current supported Azure CLI using the official Windows installation path appropriate to the host.

Allowed local mutations:
- Azure CLI package installation;
- PATH/environment registration performed by the official installer/package manager;
- normal installer metadata/cache;
- minimum dependencies installed by the official package.

Do not authenticate to Azure under this phase.

After installation verify from a fresh shell/process where necessary:
- `az` resolves;
- `az version` succeeds;
- executable path is expected;
- version is reported;
- no secret/account context is printed.

Required marker:
`AZURE F1 WP02 TOOLING — AZURE CLI INSTALLED: PASS`

# Phase D — Docker installation
Install the minimum supported Docker tooling suitable for later WP02 custom-Linux-container work.

On Windows, if Docker Desktop is the appropriate official supported path, installation is authorized.

Allowed local mutations:
- Docker official package/application installation;
- required normal PATH integration;
- required local Docker service/components;
- minimum Windows/WSL/container prerequisites only when explicitly required by the official Docker installation path.

## Critical Windows prerequisite boundary
If Docker requires enabling/installing a Windows optional feature or WSL component:
- first identify the exact required component and why;
- use only the minimum official prerequisite;
- report whether reboot/logout is required.

Do NOT:
- alter BIOS/UEFI settings;
- disable security controls;
- disable Smart App Control;
- disable Defender/firewall;
- weaken execution policy globally;
- install Kubernetes;
- enable paid Docker services/features;
- sign into Docker Hub unless separately needed later;
- create cloud resources.

If a reboot is required, complete all safe pre-reboot installation work, report the pending reboot, and classify the authority as BLOCKED/PENDING REBOOT rather than pretending runtime verification passed.

Required marker after successful installation:
`AZURE F1 WP02 TOOLING — DOCKER INSTALLED: PASS`

# Phase E — Docker runtime verification
Verify locally only.

Required:
- Docker CLI resolves;
- Docker engine/daemon is reachable;
- client/server versions can be obtained;
- Linux-container capability required for WP02 is available;
- build a minimal local test image from non-secret temporary content;
- run it locally;
- verify expected output/HTTP behavior;
- stop/remove the test container;
- remove the temporary test image if no longer needed.

No registry push.
No Azure deployment.

If Docker Desktop requires a first-run UI action or license acceptance that cannot be completed non-interactively, report the exact blocker without bypassing it.

Required marker:
`AZURE F1 WP02 TOOLING — DOCKER RUNTIME: PASS`

# Phase F — Azure CLI local verification
Perform non-mutating/local CLI checks only:
- command resolution;
- version;
- help/config command sanity.

Do not execute Azure login if it would initiate authentication. Authentication belongs to the WP02 execution gate unless explicitly authorized below.

Permitted read-only account check:
- if an authenticated Azure session already exists, `az account show` may be used read-only to detect it;
- do not expose subscription/tenant identifiers in terminal summaries beyond sanitized evidence.

Do not change subscription context.

Required marker:
`AZURE F1 WP02 TOOLING — AZ CLI LOCAL VERIFICATION: PASS`

# Authentication boundary
**New Azure authentication is NOT required for completion of this tooling authority.**

Default:
- do not run `az login`;
- do not create service principals;
- do not generate credentials;
- do not store cloud secrets.

If a valid pre-existing Azure CLI login exists, report only:
`PRE-EXISTING AZURE AUTH SESSION: PRESENT`
or
`PRE-EXISTING AZURE AUTH SESSION: ABSENT`

The subsequent WP02 Execution Authority Amendment owns authentication/subscription verification.

# Repository boundary
Do not modify the AIQuantTradingResearch repository.

Temporary Docker verification files must be created outside the repository, preferably in an OS temp directory.

Forbidden:
- repository edits;
- staging;
- commits;
- pushes;
- branches;
- PRs.

# GitHub lifecycle boundary
Do not mutate:
- #253;
- #252/#254–#257;
- milestone #62;
- Project #2;
- Release fields.

WP02 remains Open/Todo.

# Cloud/network mutation boundary
Absolutely no:
- Azure resource create/update/delete;
- registry repository/package creation;
- registry push;
- Azure App Service operations;
- Azure plan creation;
- Azure Files;
- ACR creation;
- Container Apps;
- Twelve Data requests.

Normal network downloads required to obtain official installers/packages are authorized.

# Cost boundary
Do not purchase:
- Docker subscriptions;
- Azure products;
- paid package-manager products;
- paid support;
- any recurring service.

Local tooling must not introduce recurring infrastructure cost.

# Cleanup
Remove:
- temporary Docker test container;
- temporary Docker test image when safe;
- temporary Dockerfile/test content;
- unnecessary downloaded standalone installer artifacts if appropriate.

Do not uninstall Azure CLI or Docker after successful verification.

# Mutation audit
Report exact local mutations as practically observable:
- packages/apps installed;
- optional Windows features/WSL components enabled/installed;
- PATH/environment mutations;
- services/components installed;
- temporary files created/deleted;
- Docker images created/deleted;
- Docker containers created/deleted;
- reboots performed/requested.

Also report exact zero counts for:
- repository files modified;
- Git commits/pushes/PRs;
- GitHub issue/milestone/Project mutations;
- Azure resource mutations;
- registry pushes;
- Twelve Data requests;
- tags/releases.

Required marker:
`AZURE F1 WP02 TOOLING — MUTATION AUDIT: PASS`

# Acceptance gate
PASS only if:
- Azure CLI is installed and executable;
- Docker CLI is installed and executable;
- Docker engine is reachable;
- required Linux-container runtime works;
- minimal local image build succeeds;
- minimal local container run succeeds;
- temporary runtime artifacts are cleaned;
- repository remains untouched;
- no cloud/resource/GitHub mutation occurred.

Exact acceptance marker:

`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITES: PASS`

After PASS, hand back to the existing WP02 Execution Authority Amendment. Do not execute WP02 in this authority.

Required handoff:
`AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`

# Required PASS markers
`AZURE F1 WP02 TOOLING — HOST PREFLIGHT: PASS`
`AZURE F1 WP02 TOOLING — INSTALL SOURCE VERIFICATION: PASS`
`AZURE F1 WP02 TOOLING — AZURE CLI INSTALLED: PASS`
`AZURE F1 WP02 TOOLING — DOCKER INSTALLED: PASS`
`AZURE F1 WP02 TOOLING — DOCKER RUNTIME: PASS`
`AZURE F1 WP02 TOOLING — AZ CLI LOCAL VERIFICATION: PASS`
`AZURE F1 WP02 TOOLING — MUTATION AUDIT: PASS`
`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITES: PASS`
`AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal
`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITE INSTALLATION & VERIFICATION AUTHORITY COMPLETE`

# Block conditions
BLOCK if:
- official install sources cannot be established;
- required privileges unavailable;
- device policy prohibits installation;
- unsupported OS/architecture;
- Docker prerequisite requires prohibited security/firmware mutation;
- reboot is required before Docker runtime can be verified;
- Docker daemon/Linux-container runtime cannot operate;
- installation would require paid tooling/service;
- repository/cloud/GitHub mutation would be necessary.

If reboot is the only blocker, explicitly report:
`AZURE F1 WP02 TOOLING — REBOOT REQUIRED`

After reboot, rerun this authority from verification/preflight; do not reinstall blindly.

# Exact blocked terminal
`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITE INSTALLATION & VERIFICATION AUTHORITY BLOCKED`

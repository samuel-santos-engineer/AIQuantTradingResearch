Execute `init-1.11-wp02-local-tooling-prerequisite-installation-verification-authority-terra-codex-prompt.md`.

Use **GPT-5.6 Terra**.

The only blocker being addressed is missing local tooling:
- Azure CLI (`az`)
- Docker

Install them from official vendor sources and verify them locally.

Required successful proof:
- `az` resolves and version/help works;
- Docker CLI resolves;
- Docker daemon is reachable;
- Linux-container runtime works;
- build and run a minimal temporary local image/container;
- clean temporary Docker verification artifacts.

Do not:
- run WP02 Azure probes;
- create/update/delete Azure resources;
- push a registry image;
- call Twelve Data;
- modify the repository;
- mutate GitHub;
- close #253;
- change Project #2;
- create commits/PRs/tags/releases;
- purchase any service;
- disable security controls.

New Azure authentication is not part of this authority. If an existing session exists, detect it read-only; otherwise leave authentication to the WP02 Execution Authority Amendment.

If Docker requires a reboot before runtime verification, report the reboot requirement and BLOCK rather than claiming PASS.

On success emit:
`AZURE F1 WP02 — LOCAL TOOLING PREREQUISITES: PASS`
`AZURE F1 WP02 — EXECUTION AUTHORITY RERUN: READY`

Then end with the exact COMPLETE terminal.

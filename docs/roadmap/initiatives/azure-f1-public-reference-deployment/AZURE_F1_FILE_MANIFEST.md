# Azure F1 Feasibility File Manifest

This initiative is non-release planning and feasibility evidence. It is not Release 1.11 and does not modify Release 2.0.

## Planning and governance paths

The four planning artifacts are:

- `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_DEFINITION.md`
- `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FEASIBILITY_CONTRACT.md`
- `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_EXECUTION_PLAN.md`
- `docs/roadmap/initiatives/azure-f1-public-reference-deployment/AZURE_F1_FILE_MANIFEST.md`

Only these planning paths are writable under the current Luna authority.

## Isolated probe paths

Future Terra execution requires a separately authorized, isolated probe directory beneath this initiative. Exact filenames are not authorized here. The probe must not modify `src/`, product `.csproj` files, production Docker/deployment files, schema/migrations, Python/Streamlit production paths, or canonical handoff code.

## Evidence paths

Future evidence belongs beneath an explicitly excluded-from-commit structure:

```text
evidence/
  availability/
  inventory/
  startup/
  https/
  home-storage/
  sqlite-crud/
  sqlite-transactions/
  sqlite-concurrency/
  sqlite-journal/
  sqlite-integrity/
  restart/
  redeployment/
  twelve-data/
  secrets/
  resources/
  cost/
  cleanup/
  acceptance/
```

Evidence is temporary unless a later authority explicitly authorizes retention. Raw secrets, tokens, keys, subscription credentials, Docker credentials, unrestricted environment dumps, and unredacted provider responses are forbidden.

## Generated and temporary exclusions

Never stage or commit Azure CLI profiles, Terraform/state files, Docker credentials, `.env` files, private keys/certificates, raw logs, process dumps, image layers, container databases, SQLite `-wal`/`-shm`/journal files, caches, `bin/`, `obj/`, `.pytest_cache/`, or temporary resource exports.

## Forbidden production paths/actions

- Domain/Application Azure SDKs or Azure-specific contracts.
- Production persistence replacement, schema migration, or canonical-database synchronization redesign.
- Production Dockerfile, App Service deployment, CI/CD, registry publication, or public cutover.
- Release 2.0 capability edits or fake Release 1.11 taxonomy.
- Production Worker/Streamlit supervision or direct UI/SQLite bypass.
- Twelve Data provider-abstraction redesign.
- Any Phase B implementation before WP06 decision.

## Path and mutation gate

Every future Terra change must enumerate exact paths before mutation, prove it is isolated feasibility work, and separate repository/Git/GitHub/Azure mutation accounting. No Azure resource or live provider request is authorized by this manifest alone.

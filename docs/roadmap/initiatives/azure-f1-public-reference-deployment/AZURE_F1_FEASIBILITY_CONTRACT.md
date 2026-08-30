# Azure F1 Feasibility Contract

## Contract invariants

1. Recurring infrastructure cost must be exactly `$0.00`; estimates are not proof.
2. Azure-specific concerns remain outside Domain/Application and product contracts.
3. The feasibility database is isolated from the production database and is never the canonical product store.
4. Secrets are supplied only through approved secret injection and never written to committed evidence, logs, images, command output, or URLs.
5. Every temporary resource has an owner, cost record, cleanup authority, and final disposition.
6. WAL is not presumed safe. Rollback-journal and WAL behavior must both be tested and the selected mode must follow evidence.
7. No Phase B or production architecture change occurs before the exact feasibility decision.

## Evidence contract

Evidence is stored only under the initiative evidence structure defined by `AZURE_F1_FILE_MANIFEST.md`. Each record contains UTC timestamp, test identifier, resource identity at the minimum necessary sensitivity, command/procedure, observed result, artifact hash where useful, and PASS/FAIL classification.

Redact API keys, tokens, passwords, connection strings, private keys, subscription identifiers when not required for reproducibility, host dumps, cookies, and personal data. Preserve only bounded facts needed to reproduce the decision. Do not commit raw environment dumps, Azure CLI profiles, Docker credentials, or provider responses containing secrets.

## Mandatory feasibility matrix

| ID | Required proof | Owner | Evidence |
|---|---|---|---|
| F01 | F1 available in the actual subscription and region | Terra | `availability/` |
| F02 | Custom Linux Docker runs on actual F1 | Terra | `startup/` |
| F03 | Persistent `/home` is explicitly enabled and writable | Terra | `home-storage/` |
| F04 | SQLite is created beneath `/home` | Terra | `sqlite-crud/` |
| F05 | INSERT/UPDATE/SELECT succeed | Terra | `sqlite-crud/` |
| F06 | Commit/rollback transactions behave correctly | Terra | `sqlite-transactions/` |
| F07 | Bounded concurrent readers/writer are characterized | Terra | `sqlite-concurrency/` |
| F08 | Lock/busy behavior and recovery are characterized | Terra | `sqlite-concurrency/` |
| F09 | Data survives app restart | Terra | `restart/` |
| F10 | Data survives container restart/recycle | Terra | `restart/` |
| F11 | Data survives image redeployment | Terra | `redeployment/` |
| F12 | `PRAGMA integrity_check` passes | Terra | `sqlite-integrity/` |
| F13 | `PRAGMA quick_check` passes | Terra | `sqlite-integrity/` |
| F14 | Journal and locking mode are qualified | Terra | `sqlite-journal/` |
| F15 | WAL and rollback-journal are explicitly compared | Terra | `sqlite-journal/` |
| F16 | Twelve Data DNS/TLS/HTTPS succeeds | Terra | `twelve-data/` |
| F17 | Secret injection occurs without leakage | Terra | `secrets/` |
| F18 | Provider timeout/error isolation is proven | Terra | `twelve-data/` |
| F19 | Public HTTPS behavior is acceptable, if available at zero cost | Terra | `https/` |
| F20 | CPU/memory/CPU-minute envelope fits F1 with headroom | Terra | `resources/` |
| F21 | Storage/image/log envelope fits with headroom | Terra | `resources/` |
| F22 | Every created resource is inventoried | Terra | `inventory/` |
| F23 | Actual recurring cost is `$0.00` | Terra | `cost/` |
| F24 | No production architecture change is required | Luna | `acceptance/` |
| F25 | Cleanup or explicit retention disposition is proven | Luna/Terra | `cleanup/` |

Every row is mandatory. A documentation claim may be recorded as context but cannot replace an empirical row.

## Resource lifecycle

Resources are temporary by default. Before creation, record purpose, type, region, SKU, owner, authority, expected cost, and cleanup command. After execution, record creation time, actual inventory, observed cost, and deletion/read-back. Retention requires a later explicit authority. Failure paths must attempt bounded cleanup and must report anything not deleted.

## Security and failure rules

Use least privilege, no persistent developer credentials, no public secret exposure, and no broad firewall or monitoring exceptions. A provider outage, unsupported F1 capability, non-writable `/home`, unsafe locking mode, cost ambiguity, or cleanup failure is a bounded FAIL/NOT FEASIBLE result—not a reason to weaken the contract.

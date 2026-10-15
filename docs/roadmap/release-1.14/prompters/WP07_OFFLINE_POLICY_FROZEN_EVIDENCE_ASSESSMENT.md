# WP07 — Frozen Offline Policy Evidence Assessment

Date: 2026-10-10. Status: **Frozen for owner review; partial implementation; public invocation disabled.**

Repository HEAD: `67893f51931ee183f71050a83b81127e6e648b87`. This is a working-tree review snapshot, not a commit or release. No further implementation or containment development is undertaken under this freeze. Source/test files are preserved; this assessment is the only new artifact of the freeze operation. No staging, commit, push, PR update, deployment or acceptance is implied. Future resumption requires owner direction consistent with the security contract and proof gates.

## Exact source/test inventory

The offline implementation created these ten files:

1. `python/public_runtime/__init__.py`
2. `python/public_runtime/bootstrap.py`
3. `python/public_runtime/broker.py`
4. `python/public_runtime/ipc.py`
5. `python/public_runtime/journal.py`
6. `python/public_runtime/quota.py`
7. `python/public_runtime/supervisor.py`
8. `python/presentation/test_public_broker.py`
9. `python/presentation/test_public_quota_evidence.py`
10. `python/presentation/test_public_supervisor.py`

Updated files:

11. `python/presentation/test_public_runtime_isolation.py`: the admission-lock holder waits for observer release with a bounded fallback, avoiding a startup timing race.
12. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs`: separately authorized by the owner's infrastructure-failure correction request. The private-root startup-rejection case closes stdin without racing an unnecessary request write. Other raw-input cases and all response, exit, bounds, stderr and leak assertions remain.

These are the exact twelve changed source/test paths in the current snapshot. Runtime .NET code, generator/analytics contracts, provider adapters, Dockerfile, entrypoint, bridge and UI were not changed in this offline implementation. The bridge still declares `PUBLIC_RUNTIME_VERIFIED = False`.

## Other working-tree documentation

Four previously updated policy/release documents remain part of the worktree inventory:

- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`

Six untracked review/planning documents, including this assessment, are preserved under `docs/roadmap/release-1.14/prompters/`:

- `WP07_Azure_F1_Containment_Feasibility_Codex_Authority.md`
- `WP07_BROKER_SUPERVISOR_IMPLEMENTATION_PREFLIGHT.md`
- `WP07_OFFLINE_POLICY_IMPLEMENTATION_REVIEW.md`
- `WP07_OPTION_B_CONSOLIDATED_PLANNING_AND_QUALIFICATION.md`
- `WP07_OPTION_B_CONSOLIDATED_SECURITY_CONTRACT_AMENDMENT.md`
- `WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md`

The original implementation review records chronological failures and their later correction. This assessment consolidates the final evidence without erasing that history. The inventory is 22 paths: twelve source/test paths and ten documentation paths. Ignored build outputs are not source changes or a release artifact.

## Verification evidence

These are recorded completed runs, not new runs during the freeze. Test totals are separate suite results and must not be summed as distinct coverage.

| Verification | Result and limitation |
|---|---|
| Windows quota tests | 6 passed |
| Windows broker tests | 9 passed, 1 Linux credential API skip |
| Windows supervisor tests | 12 passed, 3 Linux filesystem skips |
| Full Windows Python presentation discovery | 92 total: 77 passed, 15 skipped; run preceded the Linux-only lock-fixture correction, whose affected case is skipped on Windows |
| Final WSL public test discovery | 45 total: 44 passed, 1 real delegated-cgroup skip |
| Linux focused supervisor tests | 15 passed, including local-file reopen, permission and link negatives |
| Full Application suite | 215 passed, using existing assemblies before the later Infrastructure fixture rebuild |
| Rebuilt focused public Worker acceptance cases | 4 passed, 0 failed, 0 skipped |
| Final full Infrastructure suite | 317 passed, 0 failed, 0 skipped, using the rebuilt assemblies |
| Python AST checks and whitespace validation | Passed; `git diff --check` also rerun for the freeze artifact |

Commands:

```text
.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p test_public_quota_evidence.py
.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p test_public_broker.py
.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p test_public_supervisor.py
.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p 'test_*.py'
wsl.exe -- python3 -B -m unittest discover -s /mnt/c/projects/github/AIQuantTradingResearch/python/presentation -p 'test_public_*.py'
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --no-restore --verbosity minimal
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -p:AutoSignTestBinaries=false --filter FullyQualifiedName~ActualPublicWorkerIsSecretFreeAndUsesBoundedRawStdin --verbosity minimal
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --no-restore --verbosity minimal
git diff --check
```

Earlier Infrastructure runs had 315/317 and 316/317 passes. The failures were an early-exit stdin race and a localhost HTTP 0.5-second timeout; the five-case filtered retry passed. The targeted stdin fixture fix was then rebuilt and the complete suite passed. No runtime or test timeout was increased. One final passing suite is compatibility evidence, not a proof that unrelated timing sensitivity can never recur.

## Signing exception

The initial rebuild failed because optional local Authenticode signing could not find the configured certificate. Verification used the command-line property `-p:AutoSignTestBinaries=false`. No signing configuration or certificate store was changed. This establishes test behavior with locally rebuilt binaries; it does not establish Authenticode signing, Windows App Control qualification or production artifact trust. Any release procedure requiring signed artifacts must restore a valid certificate and independently verify those artifacts.

## What the offline evidence establishes

Focused negative tests cover canonical bounded request validation, malformed UTF-8/JSON, duplicate and extra fields, private identity rejection, framing limits, deadlines, peer credential policy, journal bounds and ownership, reservation-before-launch ordering, rolling counts and full CPU reservations, uncertain-state occupation, wrong-token/boundary handshakes and quota staleness/replay/rollback/headroom rejection.

The journal requires explicit existing protected state; reservation persists before broker preparation, and occupation clears only after an independent cleanup verifier. Default quota acquisition, broker operations and cleanup verification deny live use. Bootstrap and supervisor entrypoints do not launch live services. Fake broker/quota/verifier fixtures prove policy ordering and negative behavior, not authenticated telemetry, kernel process cleanup or containment.

## Remaining security obligations and activation blockers

1. **Process-tree ownership and launch:** prove fixed-role UID/GID separation, credential drop, bounded startup handshake, parent-death handling, descendant ownership and cleanup. The proposed broker/Worker shared identity requires proof that Worker code cannot control or impersonate the broker. No live bootstrap, guardian or broker launch is qualified.
2. **Durable admission and recovery:** select and qualify the storage backend for ownership, concurrent access, atomic replacement, file/directory persistence and crash recovery across actual target restarts. Local Linux file tests do not qualify Azure CIFS. Uncertain state must remain occupied; no public reset/recovery commands or lease-based reopening.
3. **Private IPC:** implement and verify actual endpoints, peer credentials, permissions, bounded framing and routing. Policy-level credential checks alone do not prove the production transport boundary.
4. **Quota acquisition:** establish operational authentication without owner credentials, real observation timestamps, units, scope and replay/freshness guarantees. A reset timestamp is not an observation timestamp. Unavailable or stale evidence must deny generation; no current operational adapter is qualified.
5. **Abuse and headroom:** implement mandatory durable global admission/client controls, cached-read limits, health protection and quota-headroom gating before public activation. Offline reservation rules are not complete global abuse enforcement.
6. **Resource evidence:** reconcile Worker and supervisor CPU accounting and orphan costs; verify the approved reservation policy, measured CPU overshoot, daily quota share, memory and latency criteria on the actual target. Reservation limits do not provide kernel CPU ceilings. Azure F1 writable cgroup delegation was unavailable; no hard process-tree CPU bound is claimed.
7. **Isolation:** prove private filesystem, credentials, environment, network and provider separation, including Worker-to-broker access and descendants. No end-to-end public isolation or production request-size enforcement through a broker is established.
8. **Target qualification and lifecycle:** satisfy the remaining G-B proof gates and obtain owner acceptance before startup integration/public activation. Real delegated-cgroup coverage remains skipped. No privileged deployment, paid resource, new service, managed identity/RBAC change or deployment is authorized by this freeze.

The preferred candidate remains external supervision under the conditionally approved architecture. Its feasibility is unresolved. Passing offline tests do not close WP07, activate dependent release work or establish WP08 release acceptance.

## Snapshot integrity

The SHA-256 table below records the twelve source/test files as frozen on this date. These hashes identify the review contents; they are not qualification fingerprints or a cryptographic signature. This document is intentionally excluded from its own hash table.

| Path | SHA-256 |
|---|---|
| `python/public_runtime/__init__.py` | `7bc3e016c188aace04fea13fdf2a600433e1b0d89a17081bf6a22288c17dcf03` |
| `python/public_runtime/bootstrap.py` | `55cd898f0c422b72072712e048464d40af8426132f25eedde158afa70892359e` |
| `python/public_runtime/broker.py` | `225c4f9aa5b396c352eb4727e2bfd05e3144d5dcc7379f3d47320a9201822d6d` |
| `python/public_runtime/ipc.py` | `b3098ae64e1aafcaf17861211af89865eac71d2b67dec47d2bcfe335c9d11bfc` |
| `python/public_runtime/journal.py` | `8a171c74db07591c4fa7777d97e148e5943d373d720ea80ecdeb84569fcf8b2a` |
| `python/public_runtime/quota.py` | `1ed8b75f0c054e2d7d0985d74a4dbbe8d84315f8622018b32c5eca8bdc4287ce` |
| `python/public_runtime/supervisor.py` | `1bea4b3b82b6ddfb8b8b49e503c3dad9eb3b0e883f50b9f2d86976067e546ff1` |
| `python/presentation/test_public_broker.py` | `8a9b9c08ea97e79b928185e317a795ba9eae3f1bef49f6f57dc95f538d8db157` |
| `python/presentation/test_public_quota_evidence.py` | `fa49519c75c6439887d7a426253a7540c21c35eb0def85c320370c52995b7611` |
| `python/presentation/test_public_supervisor.py` | `cceb4f1cc8fcb71579a0258eda22ac8700a1497bd0a359958a2b273c992addd2` |
| `python/presentation/test_public_runtime_isolation.py` | `7cd7742a26e153e2c675bbbf54c61525a5f98021e31398aec712302a840f30da` |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs` | `5657d531f499517952f2f174ee041fb301064d489e5ad568ee777671ab55afbb` |

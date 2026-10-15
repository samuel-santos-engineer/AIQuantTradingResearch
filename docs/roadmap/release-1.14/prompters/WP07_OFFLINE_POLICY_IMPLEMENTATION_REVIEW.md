# WP07 — Offline Policy Implementation Review

**Date:**2026-10-10 (America/Sao_Paulo). **Status:**PARTIAL IMPLEMENTATION; PUBLIC INVOCATION DISABLED.  
**Authority:**Owner authorized implementation against the17path broker/supervisor preflight. This report is recorded under the owner's standing instruction to save future WP07 documents in this prompters directory. No commit, PR update, issue closure, deployment, Azure role/configuration change or activation.

## Outcome and exact implementation inventory

Implemented the offline admission/journal, strict framed IPC, quota-evidence validation, handshake/identity policy and focused negative tests. The supervisor controller uses trusted composition seams for tests; default quota, broker and independent-cleanup verifier deny live use. Bootstrap/supervisor entrypoints exit unavailable. They are not functional live process-launch services. Imports create no processes, sockets, namespaces, directories or background tasks.

New files:
1. `python/public_runtime/__init__.py`
2. `python/public_runtime/bootstrap.py`
3. `python/public_runtime/broker.py`
4. `python/public_runtime/journal.py`
5. `python/public_runtime/ipc.py`
6. `python/public_runtime/quota.py`
7. `python/public_runtime/supervisor.py`
8. `python/presentation/test_public_broker.py`
9. `python/presentation/test_public_supervisor.py`
10. `python/presentation/test_public_quota_evidence.py`

Updated implementation/test file:
11. `python/presentation/test_public_runtime_isolation.py`

This review Markdown is an additional separately authorized documentation artifact, not a source/test allowlist expansion. The four already-modified policy/release documents and existing untracked prompters were preserved. Dockerfile, entrypoint, bridge, UI, requirements, all .NET files and private adapters were not changed in this implementation turn. Six authorized existing paths were deliberately not wired before prerequisite proof.

## Implemented negative-security behavior

- Strict4096byte canonical v2 request validation: malformed UTF8/JSON, noncanonical characters, duplicate/extra fields, private identity/source, invalid ranges/deadlines, booleans as integer deadlines and public recovery/reset commands rejected before expensive work.
- Length-prefixed private IPC framing checks header limits before body buffering;2MiB maximum response,8192byte chunks, partial send/read handling and monotonic deadlines. Kernel credential check rejects unauthorized UID/GID; live endpoint/server routing is not implemented.
- Explicitly provisioned protected file journal only: never auto-initialize missing state. Existing lock/journal required; secure relative opens, ownership/mode/type/hardlink checks, bounded32768byte read/write, duplicate-field rejection, flock, atomic replacement and file/directory fsync. These mechanisms do not qualify Azure CIFS.
- Occupied state persists across final-holder death, time passage, different scope, broker failure, uncertain START or failed settlement. Rolling900/86400second3/15counts and16second full charges,60/240reservation caps, owner-token checks, monotonic history and no lease expiry reopening.
- Reservation write precedes PREPARE; registered and potential-running states commit before START. Write failure prevents later launch step. Independent verifier required before verified/settled clearing; default verifier never supplies fake proof. Numeric metadata alone is insufficient.
- Quota acquisition defaults unavailable. Trusted source must explicitly authenticate before validation; observations require scope, actual timestamp, sequence, daily+short counters, units as typed CPU-seconds, reset and>20%headroom. Future/stale/rollback/replayed/conflicting observations, missing counter classes and invalid types deny. No credentials, HTTP acquisition or fabricated timestamp.
- Fixed-role plan and real/effective/saved UID/GID/group verification contract; broker READY requires actual expected role/localPID1 and exact registered boundary; duplicate START, wrong owner/boundary, EOF and wrong-role events cannot authorize start. No live UID switching or Worker launch exists.

## Verification and commands

Commands used the qualified repository Python interpreter with-B to suppress bytecode; .NET used existing binaries because no .NET code changed.

| Command | Result |
|---|---|
| `.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p test_public_quota_evidence.py` |6passed |
| same command with`test_public_broker.py` |9passed,1LinuxcredentialAPI skip on Windows |
| same command with`test_public_supervisor.py` |12passed,3Linuxfilesystem skips on Windows |
| `.venv/Scripts/python.exe -B -m unittest discover -s python/presentation -p 'test_*.py'` |92total,77passed,15skipped; no failures |
| `wsl.exe -- python3 -B -m unittest discover -s /mnt/c/projects/github/AIQuantTradingResearch/python/presentation -p 'test_public_*.py'` |Final45total,44passed,1real-delegated-cgroup skip |
| Linux focused`test_public_supervisor.py` |15passed, including ordinary temporary-file journal reopen/permissions/link negatives |
| `dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --no-restore --verbosity minimal` |215passed |
| analogous full Infrastructure command, initial run |315passed,2failed of317:private-root stdin pipe close; localhost qualification HTTP0.5second timeout |
| Infrastructure filtered retry covering`ActualPublicWorkerIsSecretFreeAndUsesBoundedRawStdin` and`QualificationHttpEvidencePreservesEndpointStatusesAndEmitsSanitizedLifecycleDiagnostics` |5passed |
| Full Infrastructure rerun, sequential |316passed,1failed of317:private-root stdin pipe close remained |
| Python AST checks and`git diff --check` |Passed |

Initial Linux broad run exposed an existing admission test timing assumption: the1.4second holder could exit during contender import/startup, causing “admitted” rather than “busy”. The authorized fixture now keeps the lock until observer release using stdin, bounded by4seconds, without increasing admission wait or claiming process containment. Final Linux run passes. Python Windows full run preceded this Linux-only fixture adjustment; its affected test is skipped on Windows.

Remaining Infrastructure failure is in unchanged `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs`, line50. The private-root Worker can reject configuration and close stdin before the test writes. Isolated retry passes, full-run failure recurs. This file is outside the17path implementation allowlist; no test/runtime timeout relaxation or unauthorized edit was made. Full Infrastructure is NOT reported as passed. Separate literal-path authority is needed if fixing that regression fixture is desired.

## Compatibility and limits

Existing C# generation/analytics/provider contracts were not modified; Application regressions passed and isolated public Worker composition cases passed. Windows full Python regressions pass. Full Infrastructure has the disclosed failure, so compatibility evidence is not universally green. Linux temp-file/fsync/flock results are not Azure durability evidence. Offline fake journal/broker/verifier/quota fixtures establish policy ordering only, not kernel lifecycle, authenticated acquisition or real containment. No privileged role/namespace probe or Azure diagnostic was run.

## Stop boundary and outstanding work

The preflight requires credible proof of distinct-identity launch/cleanup, shared broker/Worker isolation, persistent backend ownership/durability and real quota timestamp/authentication before startup integration. These remain unresolved. The17path authority does not grant managed identity/RBAC, active target tests or new security dependencies. No additional architecture choice was silently adopted.

Not implemented/qualified:live fixed-role root bootstrap and credential drop, namespace/guardian execution, descendant kill/reap verification, IPC listener/client routing, durable global cached-read/client-abuse state and health admission integration, operational quota acquisition, per-job20CPU-second measurement, daily10%allocation validation, target F1 startup/memory/latency/isolation/network/abuse gates. Existing bridge gate remains `PUBLIC_RUNTIME_VERIFIED=False`. No end-to-end public isolation, request-size enforcement via production broker, or WP07 acceptance is claimed.

Next review should decide whether to authorize the required exact bounded proof work and the out-of-allowlist Infrastructure fixture correction, then resume integration only after evidence supports it. Public generation remains unavailable if quota freshness or cleanup cannot be independently established. No commit/PR/deployment is proposed by this report.

## Infrastructure failure correction — 2026-10-10

The owner's subsequent request, “fix infrastructure fail”, authorized the targeted fixture correction in `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs`. The private-root case now closes stdin without writing a request: invalid cache configuration is rejected during startup before input is read, so the former write raced the expected process exit. Valid, malformed UTF-8 and oversized-input cases still write their original bytes. Exit code, response contract/state, output bounds, stderr and secret-leak assertions are preserved. No runtime code or timeout changed.

The initial rebuild encountered an unavailable local Authenticode certificate. Optional local signing was disabled through a command-line property for verification, without changing signing configuration.

- `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -p:AutoSignTestBinaries=false --filter FullyQualifiedName~ActualPublicWorkerIsSecretFreeAndUsesBoundedRawStdin --verbosity minimal`: 4 passed, 0 failed, 0 skipped; rebuilt the test assembly.
- `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --no-restore --verbosity minimal`: 317 passed, 0 failed, 0 skipped.

This supersedes the outstanding fixture failure and Infrastructure compatibility limitation above; historical failed runs remain recorded. The exact paths changed for this correction are the acceptance test and this review document. WP07 production qualification blockers and the disabled public invocation gate remain unchanged. No commit, push, PR update or deployment was performed.

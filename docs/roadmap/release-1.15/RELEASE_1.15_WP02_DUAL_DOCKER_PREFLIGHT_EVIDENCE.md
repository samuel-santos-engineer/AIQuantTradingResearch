# Release 1.15 WP02 - Dual Docker preflight evidence

Date: 2026-10-23. **PLANNING DELIVERABLE; SOURCE IMPLEMENTATION BLOCKED pending merged governance and literal-path approval.** UTF-8/no BOM/LF/final newline applies. `PUBLIC_RUNTIME_VERIFIED=False`.

## Repository and PR observations

- Original checkout branch feat/release-1.15-wp02-runtime-isolation at6f76697707e2363cc52820e0c805172815b92159. Dirty manifest plus six untracked WP02/Azure/prompt artifacts inventoried and hash-snapshotted. None overwritten, staged, reset, stashed or silently imported into implementation. Separately named new plan supersedes the old supervisor proposal only as intended D1-B planning, not an edit of that owner file.
- PR #381 verified MERGED at968ed80302feb33e32ad65e191971691537362bf; live main matches. #368 OPEN and revised to D1-B/API-key direction. PR #382 OPEN/CONFLICTING at17436b937580c8a8b4c8e9d177399bb498822b9f; its seven governance/roadmap changes were inspected, not treated as merged policy.
- Separate isolated worktree merged origin/main into the #382 branch using a normal merge commit. Conflict only in RELEASE_1.15_FILE_MANIFEST.md. Resolution preserves main's complete Azure evidence/authority index and #382's complete dual-container section; its reference to open #381 corrected to merged. Merge commitdee115556d450a0717a110e6131bafd58b830254 pushed without force to existing PR #382. No PR merge performed.
- Second clean worktree from current main publishes this four-file planning PR. It does not contain #382 governance changes; adoption is a dependency, not silently bundled authorization. Executed prompt preserved as an artifact and indexed.

## Exact scope and result ledger

[Literal preflight plan](RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md) proposes eleven future Docker/fixture/test/runner paths and exactly four current documentation paths. Two direct-entrypoint non-root images; no C/native/Bash dual-process supervisor. Production D4/auth integration is deferred to WP05; fixture key format/300s overlap are proposals for review, not accepted new contract values.

| Category | Observed result |
| --- | --- |
| Main and PR381/382/issue live state | Read via GitHub CLI and fetched refs |
| PR382 merge conflict | Resolved and normal merge commit pushed; PR still OPEN/unmerged |
| Azure evidence retention | Main manifest prefix retained; merged Azure evidence/prompts unchanged |
| Source implementation | NOT STARTED; governing PR unmerged |
| Product tests/builds/Docker/TLS/security fixtures | NOT RUN; zero test passes/digests/resource benchmarks claimed |
| API-key/TLS threat model | Documented proposal; no executed transport/security evidence |
| Azure/app settings/secrets/provider/registry/deployment | NOT CALLED / no mutation |
| Original dirty work | Preserved by SHA-256/status verification |

One diagnostic console print hit Windows cp1252 encoding on existing Unicode; repository bytes were not changed by that read failure. Subsequent validation uses UTF-8 file reads/byte scans. A guessed existing public_preview_shell.py path was absent and was not added; actual run-only test paths were resolved from the repository.

## Outstanding obligations

PR #382 must be owner-reviewed/merged before D1-B source implementation. Scoped path/native-free design approval remains required by existing WP02 preflight. WP03 must prove writer exclusivity/durable recovery across deployments; WP05 owns production API-key/HTTPS/schema/rotation integration; WP07 actual F1 ingress/TLS/cold start/resource/secret-rotation and paired-image verification. No Azure key creation/read/rotation/display, no provider/HMAC secret transfer to UI, no readiness/acceptance claim. `PUBLIC_RUNTIME_VERIFIED=False`.

Documentation validation: four exact changed paths, 20 relative Markdown targets, all five run-only paths, UTF-8/no BOM/LF/final newline and git diff --check PASS. Governance manifest retains the full merged-main prefix plus D1-B section; merged Azure artifacts byte-preserved. Original dirty checkout status/hashes unchanged. Staged blobs separately checked before commit; no executable tests run.

## Owner continuation and reconciliation - 2026-10-24

PR #382 merged as49966b38e1228d4eb336cb1676d4ac3e41c716a4; D1-B governance is now effective. Owner explicitly authorizes merge of this documentation plan and its eleven exact implementation paths in a separate implementation PR. Earlier unmerged/approval-pending labels are historical preflight observations, superseded by this note. Manifest reconciliation preserves both merged governance/Azure entries and this plan's index. Review confirms fixture-only scope, unchanged D3/D4/D5 semantics, no new dependency or production API authority. No tests/images/security behaviors are claimed by plan acceptance. Rotation/key-format candidates remain isolated fixture choices for WP05 review. `PUBLIC_RUNTIME_VERIFIED=False`; no deployment/merge of the future implementation PR is authorized.

## Authorized fixture implementation and qualification - 2026-10-24

**WP02 Docker/security fixture foundation implemented; OWNER REVIEW REQUIRED, not whole-WP acceptance.** PR #382 merged at49966b38e1228d4eb336cb1676d4ac3e41c716a4; reconciled documentation PR #383 merged at9e2efc2dff0d604b50ae1c912e3261b2e05148e6. Owner then expressly authorized its eleven literal source/test/build paths and a separate implementation PR. Earlier planning-blocked labels above are historical; no governing PR was assumed effective before merge. Original dirty checkout/owner files remain untouched; implementation is in a separate current-main worktree/branch.

### Exact implementation and provenance

Two explicit Dockerfiles, direct non-root entrypoints, no C/Bash supervisor: research UID/GID12001 dotnet; UI UID/GID12002 Streamlit. Filesystem separation is across containers, not a shared UID-switching boundary. Both are labeled OFFLINE FIXTURES, not production ingestion/research/chart. Existing default Dockerfile, application sources, project/package/lockfiles, CI, historical feasibility and Azure settings unchanged. D3/D4/D5 production meanings are not implemented or altered. The dummy C# endpoint deliberately returns503 unavailable instead of inventing D4 data.

Qualification used base commit9e2efc2 with the following uncommitted implementation bytes. Their SHA-256 values were recorded by the runner and independently matched again before publication; the PR commit contains these same source bytes. Documentation was appended afterward and is not an image input. This distinction avoids falsely claiming images were built from a later commit.

| Exact approved implementation path | Qualified SHA-256 |
| --- | --- |
| `container/release_1_15/research.Dockerfile` | `d8b0b108baac41da76345e78ba993e5e3d3ec4a6d05b5758acc4f05e29d67570` |
| `container/release_1_15/ui.Dockerfile` | `07caf8833706eaa58274b0a7aaee4660652b3be8a01e0c3e0a388755719b46b9` |
| `eng/docker/release-1.15/Invoke-Wp02DualDockerQualification.ps1` | `9e877ff605351530c539811779516663ed5199100f6cfa2b73654ffefa64c546` |
| `tests/runtime/release_1_15/dual_docker/Program.cs` | `7b4fbe85eef0f0fe6f192d04d0ffffa665bb0008fa81bb458299897a5415d19a` |
| `tests/runtime/release_1_15/dual_docker/ResearchFixture.csproj` | `337d54b78362116c0f8ba4f3396231d959709fe0819e4e4bbfbeea4e8d72bf7d` |
| `tests/runtime/release_1_15/dual_docker/api_key_client_fixture.py` | `8eacdc35a78e230d0925cd986feb86f0f1c93f0a98b1487e62b9c03682b3d80a` |
| `tests/runtime/release_1_15/dual_docker/fixture_contract.md` | `5e72398a3574a35e6bc3550d36947524e11f4eb0f7717e223d51c5942476d32a` |
| `tests/runtime/release_1_15/dual_docker/public_app_fixture.py` | `7a513ec7175e0896dc131a6400aed352e9fe6d4e8f43e47f9c547e5bb586ed41` |
| `tests/runtime/release_1_15/dual_docker/test_api_key_client_fixture.py` | `b7d0afd27db148853ead080b1ad0c3afaad2a213da70a6c7ad9f3fcbe86e328a` |
| `tests/runtime/release_1_15/dual_docker/test_pair_boundary.py` | `abb7e7511e1a154e7fad1065acae9d93f58a7f0fa6aef101981ce4b1be3e352f` |
| `tests/runtime/release_1_15/dual_docker/tls_fixture.py` | `1b3e41642135f932562783e96d99bc1c46229a29033eb50537bd9bf1120764d9` |

Additional changed paths are only this existing evidence document, the existing preflight plan (dated implementation note), and RELEASE_1.15_FILE_MANIFEST.md (append-only index). Executed prompt remains the merged, unmodified prompt from #383. No extra source/generated artifacts/dependencies introduced.

| Qualified local Linux/amd64 image | Immutable local ID/digest | Reported image size |
| --- | --- | --- |
| research fixture | `sha256:7bf502edf0e16fddddb0216541fe51e1b7784b7e68185c1268559b641c83769b` | 95993303 bytes |
| ui fixture | `sha256:f51fd24c3300af380f4d89df8542aea6c909f616a82d64b55bcc46b6b2ff3e60` | 249458400 bytes |

These are locally loaded Docker image identities, not evidence of registry publication. Exact paired tags use run ID47cac38365ea4d16aaa36f08f75370ae; both retained for review, never pushed/deployed. Bases are pinned in the Dockerfiles. UI copies only /opt/venv from the existing qualified local dependency asset with full IDsha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed; it does not inherit its application/market assets or dotnet runtime. Runner verifies the full ID before named-context use; a fresh machine must supply that exact asset. No pip install or new NuGet package. Restore/publish RUN steps and builds use network=none; base-image metadata resolution is not a claim of an air-gapped builder. Per-run labels/attestation metadata change image digests across runs; bit-for-bit reproducible image builds on fresh builders remain unproven.

Observed versions: Docker29.7.2, Linux x86_64 kernel6.18.33.2-microsoft-standard-WSL2, ASP.NET/Core runtime10.0.13, Python3.13.16, Streamlit1.61.1, Plotly7.1.0. Runner uses PowerShell5.1 syntax and was executed on5.1.26100.9711; exact requested patch5.1.26100.9444 is not installed here, so no patch-specific execution claim. No persistent execution-policy change: initial script execution was blocked by local policy; the permitted RemoteSigned setting was limited to the invoking process.

### Reproducible commands and actual test outcomes

From this PR's source checkout with the pinned existing local assets:

```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
.\eng\docker\release-1.15\Invoke-Wp02DualDockerQualification.ps1 -KeepImages
```

The runner executes the exact source-hashed Dockerfiles with --pull=false --network=none --platform=linux/amd64 and verifies dependency image identity. It then runs these commands from tests/runtime/release_1_15/dual_docker:

```text
python -B -m unittest test_api_key_client_fixture -v
python -B -m unittest test_pair_boundary -v
```

Final run: **PASS, exit0;12 deterministic client tests in0.292s;9 paired-image tests in119.205s; overall runner124.152s.** These are21 test methods, not21 independent runtime products or production release gates. No existing broad Application/Infrastructure suite run: production sources unchanged. No actual browser was used; UI evidence is Streamlit AppTest inside the Linux UI image. No performance/browser/F1 acceptance is inferred.

| Negative/functional area | Executed evidence |
| --- | --- |
| Client transport/config | Reject HTTP, userinfo/query/fragment/path/bad port, malformed keys, invalid bounds; default TLS chain+hostname verification asserted; service key only in server request header |
| Client response safety | Reject redirects/non200/content type, declared/streamed oversize, malformed UTF-8/JSON/duplicate/extra fields, truncated length and fixture=1 boolean coercion; preserve complete last-known object and mark disconnected |
| Client deadline/errors | Total-time boundary and watchdog start/cancel unit checks, sanitized network errors; actual slow-drip schedule/fault-injected watchdog cleanup not exercised |
| Direct research access | Missing/wrong/different dummy key and duplicate header401; authenticated unknown/raw/admin/control paths404; anonymous non-liveness paths401; generic liveness200 only |
| Actual TLS | Trusted local fixture cert works; untrusted chain and wrong hostname fail; plaintext origin refused and raw HTTP to TLS listener cannot obtain200. Explicit expired-certificate connection case NOT RUN; standard TLS verification retained |
| Bounded request body | Empty-object fixture POST503; unknown request fields/malformed UTF-8 rejected;4097-byte body413; unauthenticated oversized body401 before application body handling |
| Configuration/key lifecycle | Missing/malformed active key, previous without expiry and beyond300s expiry cause expected exit64. Active/previous overlap accepted before a recorded30s deadline; previous401 after expiry, active still200; old key rejected with no previous configured. No actual secrets or real rotation |
| Image/process/files | Independent proc1 UID/GID identity and zero CapEff/CapBnd/no_new_privs; research-only fake plaintext sidecar/canary files0700 parent/0600 owner12001; UI has no /private, private certificate mount, provider/HMAC env or dotnet runtime; image metadata/history do not contain dummy service key/private canary |
| Lifecycle/UI | In-image AppTest connected/disconnected warnings; internal TLS client works; research stop leaves UI Running, then UI stop leaves a fresh research fixture healthy; no in-container role supervisor/restart |

Fake raw.sqlite/WAL/checkpoint/watermark/config/key files are plaintext canary fixtures, **not SQLite recovery evidence**. Process inspection checks the service process, not exhaustive every-thread attestation. Diagnostic docker exec/AppTest processes are temporary test observers, not extra production principal services. No rendered real browser/Plotly payload or arbitrary request-reordering proof is claimed.

### Resource observations and cleanup

Each test container is independently limited to1CPU,512MiB and64PIDs; read-only root, cap-drop ALL, no-new-privileges, bounded tmpfs and loopback-only host-published ports. This is not one shared F1 allocation: concurrent fixture limits can total more than one F1 budget. An owned local bridge permits paired DNS/TLS; it is not an enforced egress-denial policy. Clients only address owned localhost/research fixture endpoints. No live external API/provider/Azure call performed.

Independent intermediate cgroup sample of the main pair (rotation helpers excluded):

| Role | memory.current | memory.peak at sample | pids.current | cumulative CPU usage_usec |
| --- | --- | --- | --- | --- |
| Research | 30662656 | 30756864 |12 |1474327 |
| UI |71282688 |79011840 |5 |2157961 |

These are sampled cgroup bytes/cumulative CPU, not RSS, whole-run final peaks or throughput. Later Docker working-set sample after UI render: research26.33MiB/0.28%CPU, UI63.71MiB/0.57%CPU; do not sum working set and raw cgroup counters. Samples include diagnostic-process cost and do not qualify1/3/5 browser sessions, quota headroom or real ingestion load.

Final cleanup independently recorded four normal service instances exit0 and four intentionally invalid configurations exit64; no OOM. All run-owned containers and network removed, temporary private test certs removed; runner also has ownership-checked timeout cleanup backstop. Earlier completed attempts' six superseded image tags removed only after matching labels/IDs; no global prune, unrelated process/resource deletion. Final image pair retained locally for review.

### Failures retained and corrections

1. First research build failed on ambiguous BadHttpRequestException; qualified the ASP.NET type within approved Program.cs. No security test passed from that build.
2. First paired setup ran zero tests because the internal Docker network did not publish the requested host port; owned research exited0 and cleanup completed. Switched only local fixture network to a bridge with all host ports still127.0.0.1; no LAN/public service exposure or Azure change.
3. A subsequent12-client/8-pair baseline passed. Review then found Python True/1 equality acceptance; strict boolean identity and regression mutation added. Added explicit config negatives, private file modes, full input hashes and cleanup backstop.
4. Expanded9-pair run had8 passes/1 failure: a5s previous-key window elapsed during container setup before the first acceptance observation. Test now records a30s absolute deadline, proves acceptance first, waits until that deadline and proves401 afterward. The verifier300s cap was not increased or disabled. Final12+9 run passed.

### Remaining obligations and recommendation

Review recommendation: accept this **isolated WP02 fixture foundation only**, not WP02 completion or public/runtime activation. Full production image composition and D4 client/server integration remain outside these fixtures. WP03 must implement exclusive writer/storage/lineage/recovery across replacements; WP05 must review candidate key format/deadlines and implement actual bounded authenticated D4 transport/rotation config, especially clock/restart/config-rollback handling. The memory-only fixture cannot prove durable anti-rollback or secure real key provisioning. WP07 must qualify Azure frontend TLS termination/proxy trust, HTTPS-only/direct unauthenticated ingress, real secret rotation, independent deployment/restart policy, paired digests/browser/resource contention and per-site F1 behavior. Local TLS and non-root containers do not establish private networking or production confidentiality.

No Azure CLI/app-setting/credential read/write, provider call, registry push, deployment, new infrastructure or #368 closure during implementation. Default Release1.14 remains unchanged. Owner review of separate implementation PR required; no merge authorized. `PUBLIC_RUNTIME_VERIFIED=False`.

Implementation publication validation: exact14-file scope (11 approved implementation paths plus3 existing documentation paths), all11 qualified input SHA-256 matches,23 relative Markdown targets/anchors, append-only manifest, independent UTF-8/no-BOM/LF/final-newline and git diff --check PASS. Original checkout's dirty status and every snapshotted owner-file SHA-256 remain identical. Staged blobs are independently verified before commit. No additional product tests required after documentation-only additions; qualified source bytes unchanged.

## PR #384 disposition and next-phase read-only review - 2026-10-24

Owner explicitly approved fixture-only PR #384 at eeae40a2af7c6c75a4e457f53cde1d34abe9dce1. Live GitHub preflight: OPEN, exact approved head, MERGEABLE/CLEAN,14 approved paths, no inline comments/reviews or reported checks. No checks is **not successful CI**; owner acceptance was in this conversation, not fabricated as a GitHub review. Full patch inventory/diff check and11 source hashes matched retained local qualification artifacts; historical client log12/12 and paired log9/9 re-read, not tests rerun. PR #384 squash-merged using normal gh pr merge --squash --match-head-commit, no admin bypass, resulting main d237e268fc0cd61cad9c8a32053f57095351f78c. WP02 #368 remains OPEN.

A clean worktree based on that main contains every merged fixture file. Original owner's dirty manifest and untracked prompt/plan/evidence bytes/status were snapshotted and preserved. Merged governance/roadmap/WP01 D1-B and source fixture code were read. No source/test/Docker/runner file changes, builds, tests, Docker runtime probes, Azure CLI/settings/secrets/provider/registry/deployment operations executed in this continuation.

Observed code facts, not new runtime failures: client watchdog starts after connection.request(), leaving DNS/connect/TLS/write outside active watchdog control. Previous-key cutoff reconstructed from separate wall samples can drift from parsed E; prior30s expiry test does not establish exact-boundary/restart/rollback behavior. Native DNS cancellation and independent current-auth-policy trust are unresolved design obligations. [Next-phase matrix](RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md#next-bounded-phase-absolute-deadline-and-rotation-qualification---2026-10-24) proposes seven exact existing paths and bounded proofs. Proposed tests were NOT RUN; no claim that a mocked resolver proves native cancellation or memory-only state proves configuration rollback resistance.

Only this evidence, the existing plan, append-only manifest index and [new executed continuation archive](prompters/release-1.15-wp02-pr384-merge-and-next-preflight-codex.md) change. The archive's exact path was named before writing and is justified as executed authority/provenance; no new next-phase plan file was needed. Candidate implementation requires separate owner authorization; no change to D3/D4/D5, production integration or WP02 acceptance. `PUBLIC_RUNTIME_VERIFIED=False`.

Continuation documentation validation: four exact paths; 29 relative Markdown targets/anchors; seven proposed source paths unchanged;14 merged paths match approved fixture head; manifest append-only; owner dirty status/hashes preserved; UTF-8/no BOM/LF/final newline and git diff --check PASS. Staged blobs checked separately before commit. No tests rerun;21 historical local fixture test methods verified from retained logs/hash correspondence, not CI/Azure evidence.

First staged whitespace check flagged three existing Markdown hard-break lines copied from the owner prompt. Removed trailing whitespace only in the new archive; wording unchanged and original owner file byte-preserved. Final working/staged whitespace checks pass; this is documentation normalization, not a product test result.

## Simplified lab hardening qualification - 2026-10-24

**Implemented six explicitly owner-approved paths; OWNER REVIEW REQUIRED.** Baseline main4578f8645c9626f4daa7ec76923953e8892c93d6, merged #385. Owner's simplified lab handoff and explicit subsequent six-path approval supersede the complex strict DNS cancellation/rotation proposal for this phase. WP02 #368 remains OPEN, not fully accepted. Work isolated from the original dirty checkout; owner file/status SHA-256 preservation checked.

Executed handoff filename release-1.15-wp02-simplified-lab-hardening-codex.md remains owner-untracked and byte-preserved in the original checkout (SHA-256F28DD004399D5649C1822C6E8FFB4BF9E85038B3320C1C800EA2C0B2D01608B0). No new archive path added: authorization permits only the six existing source paths and three existing documentation paths. Its decision and explicit source authorization are recorded here without silently expanding the file list.

### Exact source scope and correspondence

Qualification built from baseline4578f864 plus the changed source bytes below. The runner recorded all11 image/test inputs, including unchanged Dockerfiles/project/TLS helper; all hashes independently matched after testing. Publication commits these same six modified source bytes; documentation appended afterward is not an image input. No claim images were built from a later commit.

| Changed exact path | Qualified SHA-256 |
| --- | --- |
| `tests/runtime/release_1_15/dual_docker/api_key_client_fixture.py` | `2306d10f84cfe2b535eac0c82a7b8653b1a45e46a5709634444a678a6422f23e` |
| `tests/runtime/release_1_15/dual_docker/test_api_key_client_fixture.py` | `dd46af9a24786f595f2b942a09d4439259ebb4498a6c14760ca12735f9f610bf` |
| `tests/runtime/release_1_15/dual_docker/Program.cs` | `b205920cbfcd2cf50c67cd2b100d99ad5fadb65bc79225a890bc33d11a270984` |
| `tests/runtime/release_1_15/dual_docker/test_pair_boundary.py` | `776fc1b798defaeb607367dafee676ed7514eb16f41479fdda6871cdf678b8bc` |
| `tests/runtime/release_1_15/dual_docker/fixture_contract.md` | `674fa08538cc4f43dffe4853127f7494e377923495f04606ca2a80f70d33dcd8` |
| `eng/docker/release-1.15/Invoke-Wp02DualDockerQualification.ps1` | `f324fd01c005ddab67a7e149469280f7d5946c1610d07f22fe18a9b02287c971` |

Additional edits are only existing RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md, this evidence and RELEASE_1.15_FILE_MANIFEST.md. Unchanged: both Dockerfiles, tls_fixture.py, project/package/lockfiles, CI, production Worker/UI/API/schema and historical Release1.14 evidence. No dependency installation or new source files.

### Resulting fixture behavior

- Standard synchronous HTTPSConnection with explicit connect and remaining monotonic budget checks before request/write, reads and publication. Default timeout4s retained. Existing small post-request timer remains for header/body stalls, not DNS. No DNS workers/helper processes/custom transport or watchdog supervisor. Native DNS and compound standard-library connect/TLS work may overrun the nominal budget; no hard end-to-end claim. Configuration/CA loading is before refresh.
- TLS chain/hostname checks, server-side X-API-Key, no redirect/proxy/plaintext fallback, generic errors, bounded response parsing and last-known atomic publication retained. Response and connection explicitly closed on success/failure. Elapsed budget exhaustion after connect prevents sending the key; late validation cannot publish a result after budget exhaustion.
- Exactly one active dummy service key. Constant-time fixed-size digest comparison retained. Previous-key/expiry configuration is rejected rather than creating overlap. No authentication clock/revision witness, grace period, persistence, automatic rotation or trusted-clock mechanism. Certificate generation still uses ordinary validity dates only.
- Added bounded, startup-only OFFLINE delay knobs for the existing fixture-status route:0..1000ms, headers/body. No timing-control HTTP endpoint and no production setting changes. The intentionally unavailable D4 fixture remains503 with no real snapshots, private research/storage/calculations or chart integration.

### Executed commands and tests

Same existing PowerShell5.1 runner, with process-local RemoteSigned only:

```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process
.\eng\docker\release-1.15\Invoke-Wp02DualDockerQualification.ps1 -KeepImages
```

From tests/runtime/release_1_15/dual_docker, runner executes:

```text
python -B -m unittest test_api_key_client_fixture -v
python -B -m unittest test_pair_boundary -v
```

**Final run PASS, exit0:14 client tests in0.301s,10 paired tests in120.465s; total runner137.961s.** These24 methods replace the earlier phase's12+9 count, not24 additional release gates. A separate initial14-client run also passed; it is the same14 methods, not28 unique tests. Prior historical results remain unchanged above. All preserved auth/TLS/body/response/identity/lifecycle intentions reran; obsolete overlap/expiry test transparently replaced by the owner's single-key replacement test. No broad production suites or actual browser/CI/Azure checks executed.

| Focus | Actual proof |
| --- | --- |
| Client budget/recovery | Deterministic connect-exhaustion prevents header write, accumulated budget not reset, TimeoutError closes connection/preserves state, normal next request reconnects |
| Local delayed headers | Server delay800ms; client timeout0.2s; observed failure0.2207779s, last-known preserved/disconnected; subsequent4s request succeeds |
| Local delayed body | Server delay800ms after headers; client timeout0.2s; observed failure0.2090009s; same preservation/recovery proof |
| Single key/restart | Former dummy key accepted before stop, replacement key denied there; old service observed stopped, new instance with only new active key accepts it and rejects former key401; no overlap grace |
| Bad configuration | Missing/malformed active key and either legacy previous/expiry setting cause expected exit64; no sensitive error text |
| Existing security negatives | HTTP/userinfo/query/redirect refusal, chain/wrong-host rejection, missing/wrong/duplicate/different dummy keys401, authenticated raw/admin/control routes404, malformed/oversized UTF-8/JSON/type/length responses and request limits |
| Isolation/lifecycle | Two non-root principal services, zero effective/bounding caps/no_new_privs; fake private canary file modes/separate mounts/env; Linux AppTest connected/disconnected; independent stop/peer health; no image/log key/canary leakage |

No claim of native DNS cancellation, exact timer/OS resolver-worker reclamation, key-clock expiry, configuration rollback-proof revocation, or actual Azure key replacement. No such machinery was added. Both local delay scenarios ran without new tooling/dependencies or changes to tls_fixture.py. No full production D4/SQL/algorithm/real-browser proof.

### Images, limits and cleanup

Separate Linux/amd64 local fixture pair (never pushed/deployed):

| Role | Qualified local image ID/digest |
| --- | --- |
| research | `sha256:09e5b6c68a6ca4e6dfbc239eebf3349a9b49bd68cc27e20eae7deeb4d96ab652` |
| ui | `sha256:b1cd35f6bee8ca7f25de16d849488c8d64d28b10b1ec31e41ce7ac2c2aab986d` |

Pinned existing base/dependency assets and runner ownership limits unchanged: each container1CPU/512MiB/64PIDs, read-only root, no capabilities, no-new-privileges, loopback host ports, owned local bridge and temporary certs. Last Docker working-set samples: research29.91MiB/1.91%CPU; UI91.27MiB/0.55%CPU. Instant samples are not peak/RSS/throughput/F1 quota proof; per-container limits do not simulate a single shared F1 allocation. Existing fresh-builder/local-asset and bitwise-reproducibility limitations remain. PowerShell5.1 parser and actual runner execution passed; exact patch9444 is not installed here (host5.1.26100.9711).

Cleanup:11 run-owned containers removed, seven normal service exits0 and four intended invalid-config exits64; no OOM or cleanup failures. Owned network and private test certificates removed; final pair retained locally for review. No unrelated resource cleanup/prune or Azure mutation.

### Scope boundary and handoff

Only fixture-level changes. Production/staging service keys must be distinct and separate from Twelve Data/HMAC material, kept in App Service settings only under future deployment authority. No actual key creation/read/rotation, App Service settings operation, provider call, registry push, Azure write/read, deployment, paid service or issue closure in this task. Operational replacement can use coordinated settings updates/restarts and a planned interruption; zero downtime/instant cross-instance revocation is not promised. API keys authenticate callers, not private networking.

WP05 owns production D4 authenticated HTTPS integration, timeout policy and coordinated key replacement semantics; WP07 actual Azure TLS termination/ingress/proxy trust/settings/deployment/shared F1 qualification; WP03 real storage/writer recovery. Older stricter DNS/rotation proposals are superseded for this phase, not lingering requirements or new approval/licensing gates. Separate implementation PR remains OPEN/UNMERGED for owner review; `PUBLIC_RUNTIME_VERIFIED=False`.

Publication validation: nine exact existing paths (six authorized source files plus three allowed docs); all11 qualification-input hashes match; unchanged Dockerfiles/TLS helper; 32 relative links/anchors; append-only manifest; independent UTF-8/no BOM/LF/final-newline and git diff --check PASS. Original owner dirty status/hashes unchanged. Staged bytes independently verified before commit. No tests repeated after documentation-only additions.

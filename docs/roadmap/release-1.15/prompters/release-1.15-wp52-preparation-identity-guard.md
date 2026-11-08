# Release 1.15 WP52 - automatic preparation identity guard

Date: 2026-11-08. Baseline main/#438 squash `f888613ca2be8cacc3d6ab90d77a05769b024660`, verified merged. Isolated clean worktree used; owner checkout/untracked files, private artifacts and Docker resources preserved. All new/modified text is UTF-8/LF/no BOM.

## Implemented boundary

Twelve Data remains the sole provider for BTC/USD and ETH/USD. Existing Prepare/Acquire CLI flags are unchanged. Prepare now writes an exclusive, versioned, nonsecret `preparation-identity.json` inside the owned preparation **only after successful publication/context copying/image preparation and existing local.json creation**. No actual Prepare/Docker operation ran in this work package.

The manifest contains exactly version=1, workflowSha256 and publication (relative filename -> lowercase SHA-256). It binds the current executing workflow source to the complete actual `publish/` tree used by worker(), and requires byte-for-byte identity with the copied `context/research/` tree. Required files are Worker.dll, Worker.deps.json and Worker.runtimeconfig.json; all additional managed/native/runtime files are included, not just the entry assembly. No Git SHA, absolute path, credentials, TLS configuration, bundle or private history is included or read. Import-loaded workflow bytes and current on-disk workflow hash must agree, preventing an edited source file from blessing an older loaded implementation.

After existing approval and volume-confirmation checks, Acquire validates the guard **before accessing `AIQ_WP39_PROVIDER_KEY` or copying the process environment**, and checks it again immediately before dispatch to the existing `dotnet <directory>/publish/AIQuantTradingResearch.Worker.dll --research-acquire-local` command. It does not invoke the Worker on rejection. The CLI preserves the unresolved Acquire path until validation, so resolving a symlink cannot erase evidence of the redirect. Other action dispatch/path behavior is unchanged.

Old preparations without this manifest fail closed; there is no upgrade, re-sign, trust override or discovery from old private data. Operator instruction: **prepare fresh from one reviewed checkout under separately authorized resource preparation; do not edit or fabricate a manifest to reuse an old preparation.** A failed Prepare may leave its owned incomplete directory for review; it cannot pass Acquire with absent/invalid identity.

## Untrusted input and bounded validation

- Manifest: maximum128 KiB, strict UTF-8 JSON, duplicate/extra/missing fields rejected, integer version1 (bool rejected), fixed 64-character lowercase hex digests and bounded nonempty publication mapping.
- Manifest paths are never used to open files. Compare its exact mapping against a freshly enumerated actual publication; traversal, absolute paths, backslashes, aliases, extras and missing entries cannot select an alternate executable.
- Actual directories/files and every ancestor are checked with lstat for symlinks and Windows reparse points/junctions. Nonregular files and multiply linked files are rejected. Unreadable tree enumeration fails rather than silently omitting entries.
- Portable relative paths use forward slashes; each component accepts ASCII letters/digits/underscore/dot/hyphen, excluding dot/dotdot. Case-only collisions are rejected to avoid divergent Windows/Linux identity. Maximum512 file/directory entries, directory depth8,64 MiB per publication file and256 MiB aggregate; workflow hash input <=1 MiB. SHA-256 reads use64 KiB chunks.
- Hashing compares file device/inode/size/mtime before opening, on the open handle and after closing, with exact byte count. Windows creation/change time is deliberately excluded: synthetic probes showed stat/fstat can disagree for an unchanged file. File identity, modification time, size and content hash remain checked.
- Exclusive manifest creation prevents accidental overwriting. Successful creation first verifies both publication trees agree. Any validation failure yields only the fixed sanitized parent diagnostic below; no local path/value/exception text forwarded.

## Failure and compatibility

`PREPARATION_IDENTITY_INVALID` is a **parent-only** code at preflight, not a provider/Worker diagnostic. It is added to Python PARENT_CODES only; C# and CHILD_CODES remain unchanged because the child does not produce it. Version1 envelope still has exactly ten fields:

```json
{"version":1,"event":"WP39_ACQUIRE_FAILED","failedStage":"preflight","instrument":null,"httpStatus":null,"requestsStarted":0,"responsesReceived":0,"outcomeUnknown":false,"failureCode":"PREPARATION_IDENTITY_INVALID","bundlePublished":false}
```

No provider response or billing fact is invented. Legacy INVALID_VOLUME and six WP50 subtypes remain accepted exactly as before by the strict child parser. Wrong/unknown/malformed/extra-field/oversized child envelopes still fail. Missing approval or volume confirmation still stops earlier without credential reads. Non-acquisition actions do not require the new manifest; authentic-only bundle verification remains unchanged.

If the initial preparation is invalid, tests prove zero credential/presence reads, zero environment copy and zero child launch/provider sends. A separate synthetic race test mutates a file **after** the initial valid check, when fake credential access occurs; the dispatch recheck blocks the child with zero sends and restores the existing transient cleanup behavior. That case does not claim zero fake credential reads: it explicitly asserts the one read after a valid first boundary.

No accepted OHLCV, volume representation, bounds (1000/24h/15s), UTC/ASC, base-asset units, provenance, two-symbol sequencing, no-retry behavior or atomic publication changed. No dependencies, schemas, provider abstractions, analytics/UI or infrastructure changes.

## Residual threat and qualification limits

This deterministic binding detects accidental stale/mixed/tampered artifacts relative to a recorded preparation; it is not a signature, build attestation or cryptographic proof of compiler inputs. An attacker able to rewrite both publication trees, workflow and manifest can forge the set. Current threat model relies on the existing owner-only preparation ACL and trusted local tools/dotnet host; the guard does not attest the installed dotnet runtime or authorize any operational step.

Pre-read and pre-dispatch checks plus per-file stability checks narrow TOCTOU, but do not create an immutable execution snapshot or prevent a concurrent owner/attacker replacing files after the last check. No hostile-write resistance claimed. Retained context copy is required; deleting it invalidates acquisition identity. Whitespace/line-ending changes to workflow bytes intentionally require fresh preparation. Bounds fail closed if a future publication exceeds them; no silent exception/override.

Windows execution covered fixture directories, nested Linux-style native filenames, canonical forward-slash manifest names and a mocked Windows reparse attribute. **Actual symlink escape test was skipped**: this Windows host disallows unprivileged symlink creation. Real Linux/Docker execution and real Windows junction traversal are not qualified here. The separate reparse-attribute rejection test ran successfully on the host; this is not a substitute for native Linux symlink qualification. No Docker operation was used to bypass that limitation.

## Offline tests and observed results

Fresh results: **262 tests run:261 passed,1 skipped**. Breakdown:163 C# passed;38 Python workflow tests run (37 passed,1 skipped);61 chart/client passed. All suite commands exited0. No unavailable prerequisite downloaded.

Fourteen new workflow test cases use fake files/Worker bytes/process boundaries to cover deterministic matching identity reaching only the missing-private-channel boundary; loaded/current workflow mismatch; stale/swapped/missing actual and copied publication/native/dependency files; absent/old manifest; malformed/duplicate/version/bool/size/hash/path fields; added/case aliases; bounded tree/bytes; existing destination preservation; exclusive write; mismatched copy preventing manifest creation; Prepare orchestration success/failure ordering with all external commands mocked; and late-change dispatch rejection. Symlink scenario skipped as above. Existing channel tests explicitly mock the new guard to retain their independent secret-channel assertions; the new guard tests do not bypass it. Existing real C# -> Python parent-reader rehearsal and synthetic-only provenance checks remain intact.

Initial run exposed Windows stat/fstat creation-time inconsistency; resolved as documented above. A mocked Prepare fixture initially omitted directory creation normally performed by private_directory; corrected the mock to create only its disposable directory. No production weakening or operational command was used to make tests pass.

Commands from the isolated checkout:

```powershell
$feed = Join-Path $env:TEMP 'aiq-wp52-empty-feed'
New-Item -ItemType Directory -Force -Path $feed | Out-Null
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source $feed -p:NuGetAudit=false
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter 'FullyQualifiedName~ResearchLocalHistoryTests|FullyQualifiedName~TwelveDataMinuteCandleSourceTests|FullyQualifiedName~ResearchStateInitializationTests' --verbosity quiet
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
$env:PYTHONPATH = Join-Path (Get-Location) 'python/presentation'
& '<existing-venv-python.exe>' -B -m unittest test_research_chart test_research_snapshot_client -q
```

Restore used cached packages and an empty local feed, NuGetAudit=false. Existing installed owner-checkout .venv interpreter used for chart/client, no dependency installation. Existing analyzer CA1711/SYSLIB1045/CA1861/CA2219 and bare-mode Streamlit ScriptRunContext warnings remain. git diff --check and separate UTF-8/LF/no-BOM checks performed before commit/PR. No new PowerShell script; shown commands are Windows PowerShell5.1 compatible.

## Exact changed paths / operator next step

1. `eng/docker/release-1.15/wp39_local_history.py` - bounded identity creation/validation and parent-only rejection before credential access.
2. `tests/runtime/release_1_15/test_wp39_local_history.py` - fake identity tests and explicit guard isolation in preexisting channel tests.
3. `docs/roadmap/release-1.15/prompters/release-1.15-wp52-preparation-identity-guard.md` - this runbook.
4. `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` - append-only WP52 result.

Review implementation PR OPEN/UNMERGED. Later resource preparation must produce a fresh manifest from the matching checkout; existing preparations are deliberately incompatible for Acquire. No fresh real preparation or acquired data exists from WP52. A future actual Twelve Data invocation still requires separate explicit single-attempt authority and fresh preparation/identity/TLS/ACL/destination checks, first-failure stop/no retry. The guard does not grant approval or prove minute volume availability/units. WP47's historical subtype and charged credits remain unknown; its approval is consumed.

No real Acquire/Import/Start/Verify/browser, credential/presence/private response/bundle inspection, provider/DNS/HEAD/account/credit/support, Docker/Azure/registry/deployment or destructive cleanup operations. Twelve Data-only direction preserved. `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

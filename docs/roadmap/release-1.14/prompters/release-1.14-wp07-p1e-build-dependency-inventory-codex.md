# Codex Authority — Release 1.14 WP07-P1E: Build Dependency Inventory

## Authority and outcome
OWNER AUTHORIZES **READ-ONLY INVESTIGATION ONLY** to establish the smallest independently reproducible build/test dependency closure for the corrected four-file offline protocol PR against current `main`. This is **not** authority to edit, copy, stage, commit, push, open a PR, merge, deploy, or integrate runtime behavior. STOP for owner review after reporting.

Required final verdict: `WP07-P1E INVENTORY COMPLETE — OWNER REVIEW REQUIRED` or `WP07-P1E BLOCKED — OWNER REVIEW REQUIRED`.

## Context to verify, not assume
- Protected dirty checkout branch: `draft/release-1.14-wp07-partial-public-disabled`; protected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Previous investigation reported `67893f5` is an ancestor of `main` at `b080367506e0715669b74246767926dd30c7dc26`. Verify current local and remote refs, record any drift; do not update them or checkout.
- Four corrected protocol source/test files exist in protected checkout, but the two .NET project files required for offline validation are reportedly absent from `main` and untracked locally.
- Reported last passing focused checks: Python 24, .NET 50, build 0 warnings/errors. These are historical claims, **not evidence that clean `main` can build**.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API, ingress TLS, Azure passive qualification, and public Worker activation remain NO-GO.

## Read-only preservation rules
1. Record full initial and final `git status --porcelain=v1 -uall`, branch/HEAD, `origin/main` and local `main` object IDs, merge-base/ancestry evidence, and any relevant Git configuration. Do not fetch if fetching would change repository state; use already available refs and clearly report freshness limits. GitHub API read-only inspection is permitted if available.
2. Preserve every tracked, untracked, ignored, and dirty local file. No reset, clean, stash, checkout, worktree creation, copy into repository, restore, stage, commit, push, PR, merge, package restore, or build/test execution. Do not alter Git refs, index, config, or working tree.
3. Read-only file inspection and local Git object/history queries are permitted. No Docker, Azure, network endpoint probing, deployment, credential creation, UID/GID allocation, runtime integration, or production enablement.
4. If any step would mutate files, Git metadata, or the checkout, STOP and explain. Do not treat a status-only comparison as proof of unrelated file-content preservation.

## Specific investigation
### A. Identify the missing project definitions
Inspect, without modifying, these paths as present in the protected checkout and/or Git history:
- `src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj`
- `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj`
Determine whether each is tracked, untracked, ignored, or present in another commit/branch. Verify absence/presence in `main` using Git tree evidence. Record their exact current SHA-256 hashes if readable, and distinguish on-disk files from Git-tracked versions.

### B. Resolve transitive build/test dependency closure
Read each project's references, imports, targets, SDK requirements, test adapters/framework, NuGet package versions, source includes, generated files, project references, `Directory.Build.*`, `Directory.Packages.props`, `global.json`, `NuGet.Config`, lock/assets files, signing rules, assembly information, conditional properties, and solution/test-discovery assumptions. For every required path, establish whether it already exists in clean `main`, is present only in the dirty checkout, is generated, or is externally restored. Trace transitive references until closure, without assuming the two `.csproj` files alone suffice.

Inspect how the corrected standalone C# protocol module and tests would be included in compilation, whether existing projects already provide an alternative viable host, and whether source linkage or project inclusion could introduce unrelated implementation/runtime code. Identify any collision with existing names, APIs, package pins, target frameworks, or test discovery.

### C. Compare candidate packaging strategies (proposal only)
Evaluate, with precise file lists and risk assessment:
1. A separate minimal prerequisite build/test-project PR followed by the existing four-file protocol PR.
2. One explicitly expanded protocol PR containing the minimal dependency closure.
3. An already-present `main` build/test host if demonstrably suitable without changing the frozen contract.

Prefer the smallest auditable scope and least risk to disabled Option A. Do not infer owner approval for any strategy. Explain whether build validation can genuinely be performed from clean `main` without temporary command-line properties that hide missing dependencies.

### D. Preserve protocol and security boundaries
No changes to `aiq-admin-lifecycle-v1` 64-byte wire format, canonical vectors, T9–T11, C1 bootstrap-only `A_EXEC`/`A_EXEC_ACK` launch gate, or the six P1C corrections and subsequent input-parity correction. A validated report and injected observation remain offline models, not D1/strict D2 kernel-wait evidence. A1–A3, D1–D3, T1–T11, C1, four-second shutdown deadline, five-second Docker grace, five-second privileged bootstrap deadline, 30-second total startup deadline, and disabled Option A remain binding.

## Required report
Provide:
1. **Baseline and preservation:** exact refs, ancestry, initial/final full Git status, read-only commands used, and explicit preservation limitations.
2. **Project-file provenance:** Git tracked/untracked/ignored status, main-tree presence, hashes, and historical provenance of each missing `.csproj`.
3. **Dependency closure table:** path/package/project; why needed; main vs local-only vs generated/external; evidence location; whether prerequisite for clean offline build/test.
4. **Proposed minimal file allowlist(s):** exact relative paths and any unavoidable unknowns; separate necessary build artifacts from unrelated runtime code.
5. **Strategy comparison and recommendation:** prerequisite PR vs expanded PR vs existing host, with sequencing and required follow-up validation.
6. **Blockers and owner decisions:** precise choices needing explicit authorization; do not create a candidate or PR.
7. **Security/release status:** `PUBLIC_RUNTIME_VERIFIED=False`, all production gates NO-GO.

If file contents or provenance cannot be established, mark UNKNOWN and STOP rather than inventing a dependency or broadening scope.

**STOP — OWNER REVIEW REQUIRED.**

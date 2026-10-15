# Codex Authority — Release 1.14 WP07-P1F
## Offline Protocol Packaging, Clean Validation, Conditional PR and Merge

**OWNER AUTHORIZATION:** Execute this bounded six-file packaging workflow, including a PR and merge **only if every gate passes**. Stop on any ambiguity or unmet condition. This authority does not permit runtime integration or production qualification.

## 1. Baseline and preservation

- Existing protected dirty checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Do not reset, clean, stash, checkout, commit, or modify it.
- Last observed GitHub `main`: `b080367506e0715669b74246767926dd30c7dc26`; verify current remote `main` anew, and verify `67893f5` is an ancestor. Local `main`/`origin/main` may be stale: do not infer the remote tip from them.
- Record original branch/HEAD, complete Git status and original source hashes. Use an isolated worktree/clone or separate temporary checkout that cannot mutate the protected checkout; avoid changing its refs, index or files. Confirm preservation after all actions.
- Do not copy the two original untracked `.csproj` files verbatim. They pull in unrelated web runtime dependencies.

## 2. Exactly six permitted PR paths

1. `container/admin_lifecycle_protocol.py`
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs`
3. `python/presentation/test_admin_lifecycle_protocol.py`
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs`
5. `src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj`
6. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj`

The first four must match the latest P1D-corrected, reviewed local snapshot byte-for-byte; calculate and report fresh SHA-256 values before and after copying, since the post-input-parity hashes were not provided here. No edits to those four unless separately authorized. The two project files may be **new, narrowly authored** project definitions in the isolated candidate only. Do not alter any other tracked file, solution, build props/targets, package props, notice, Dockerfile or runtime source. Generated build assets are allowed only in isolated temporary paths and must not appear in the PR.

## 3. Packaging requirements

- Base the isolated feature branch on freshly verified current remote `main`.
- Source project: `Microsoft.NET.Sdk` class library; disable default compile globs and explicitly compile only `AdminLifecycleProtocol.cs`; use inherited repo settings for `net10.0`, nullable, implicit usings and signing where applicable. No YARP, hashing, web host, `Program.cs`, notices or runtime sources.
- Test project: `Microsoft.NET.Sdk` test project; disable default compile globs and explicitly compile only `AdminLifecycleProtocolTests.cs`; reference the source project via `ProjectReference`; use existing centrally pinned versions of `Microsoft.NET.Test.Sdk` 17.14.1, `xunit` 2.9.3, `xunit.runner.visualstudio` 3.1.4 as applicable. No extra packages or solution membership edits.
- Use .NET SDK `10.0.103` as required by `global.json`; if unavailable, stop. Honor existing repository build policies without disabling safety checks to force a pass.
- No reliance on `BuildProjectReferences=false`, stale `obj/project.assets.json`, reused assemblies, or prior local outputs. Explicitly demonstrate that project references are built from this candidate.

## 4. Gate A — clean validation and security invariants

- Inspect six-path candidate diff against verified remote `main` and verify only those paths are present.
- Perform a bounded `dotnet restore` for the candidate projects with fresh, isolated NuGet assets/output and an explicitly recorded source list. Network package restore is permitted **only** for the declared existing dependencies; do not add feeds, credentials, package versions or dependencies. If restore requires unexpected sources, files or access, stop.
- Run Python focused tests from candidate: `python -B -m unittest python.presentation.test_admin_lifecycle_protocol -v`; expected at least **24 passes**, zero failures.
- Build source project and run focused .NET tests using explicit project paths, freshly restored assets and normal project-reference compilation; expected at least **50 passes**, zero failures, zero build warnings/errors. Record exact commands and versions.
- Independently recheck P1C/P1D security invariants: no generic `AGENT_RESULT` bypass; validated bound zero-result plus separate injected positive observation; sticky FAIL for rejected mutations; valid distinct PIDs; channel/state-specific one-time EOF; strict Python/.NET numeric parity; frozen 64-byte T9–T11 wire layout and canonical fixtures; C1 launch-gate exception only, no bootstrap senders on runtime channels.
- These offline observations are **not** real kernel waits, D1/strict D2 proof, confinement or production readiness.
- Stop on any test failure, protocol discrepancy, unexpected candidate dependency, unrelated diff or unapproved remediation. Do not modify the four protocol files to make tests pass.

## 5. Gate B — conditional GitHub PR

Only after all Gate A conditions pass:
- Commit exactly six authorized files on the isolated feature branch. Report changed paths, commit hash, base SHA and hashes.
- Push the isolated branch and open a PR against the verified `main` base. Describe offline-only scope, test evidence, security boundaries and `PUBLIC_RUNTIME_VERIFIED=False`.
- Verify GitHub PR file list is **exactly** the six paths and there are no unexpected commits, files, generated artifacts or unrelated base changes. If `main` advances, re-evaluate diff and validation on the new base before proceeding; stop if this cannot be done within scope.

## 6. Gate C — conditional automatic merge

Only merge if all required CI/status checks have completed successfully, all required reviews are satisfied, branch protections/rulesets allow merge, PR remains exactly six paths, and no unresolved security or validation findings exist. Do not bypass protections, force merge, self-approve to evade policy, alter settings, or infer missing checks are green. If all conditions are met, merge using a repository-permitted method and report PR URL, merge commit, final `main` SHA and verification. If any condition is unmet, leave PR unmerged and stop for owner review. Do not delete protected or unrelated branches.

## 7. Hard boundaries and verdict

- No changes to production administration, router/guardian/supervisor/agent integration, UID/GID allocation, credentials, Docker, Azure, deployment, public listeners, or active runtime qualification.
- Preserve A1–A3, D1/strict D2/D3, T1–T11 and C1, four-second shutdown deadline, five-second Docker grace, five-second privileged bootstrap startup and 30-second total startup deadlines, disabled Option A.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API, ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO** regardless of PR result.
- Final report: baseline/ancestry evidence; initial/final preservation; exact six-path diff and hashes; restore/build/test commands/results; Gate A/B/C decisions; PR/merge URLs and hashes if applicable; remaining limitations.
- End with exactly one verdict: `WP07-P1F MERGED — OWNER REVIEW REQUIRED`, `WP07-P1F PR OPEN — MERGE BLOCKED — OWNER REVIEW REQUIRED`, or `WP07-P1F BLOCKED — OWNER REVIEW REQUIRED`.

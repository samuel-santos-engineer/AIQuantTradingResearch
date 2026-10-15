# Codex Authority — Release 1.14 WP07-P1D: Independent P1C Verification and Conditional PR/Merge

## Owner authorization and decision rule

The owner authorizes **a read-only independent source/security review first**. **Only if every gate below passes**, Codex is additionally authorized to create a narrowly scoped pull request containing the four P1/P1C offline protocol files, wait for required checks, and merge that PR. This is a conditional authorization, not permission to merge despite uncertainty, failures, missing checks, or scope expansion. **Any failed, unverified, or ambiguous gate means STOP, report, and do not create/merge a PR.** Do not silently repair code in this authority.

## Protected starting state

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- The local working tree is intentionally dirty. Preserve every unrelated tracked/untracked/ignored file and all user changes. Never reset, clean, stash, discard, or overwrite them.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification, and public Worker activation remain **NO-GO**. Disabled Option A remains unchanged.
- No runtime integration, account/UID allocation, credentials, Docker build/run, deployment, Azure operations, active network qualification, or production enablement.

## Exact four-file content allowlist

1. `container/admin_lifecycle_protocol.py` — expected SHA-256 `2305859E045B3FE441277DCB1D27F20FD2B7D2FDD86C3727C9F74E68454E92EF`
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs` — `E64D7F410C0582A4F5087597B2105DAE66AA7CDF1073C70293A446B8027420C3`
3. `python/presentation/test_admin_lifecycle_protocol.py` — `C2E8F82D4EEB1E5F861D958A0AAD656233F34775EFBCADB7DB5D1124B59F7041`
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs` — `889456FF194F1141A4E029AE9B6B61F9F32A9F0B757766FDEC75D25F2758A63A`

No other repository content may be added, modified, deleted, staged, committed, or included in the PR. These four files must be the only PR diff. No edits to these four files are authorized during the review: a discrepancy blocks this run.

## Phase 1 — Independent read-only review (mandatory before GitHub mutations)

1. Record full initial `git status`, branch, HEAD, the four file hashes, and an inventory of unrelated tracked and untracked state. Verify expected branch/HEAD and all hashes. Inspect the full four-file source and tests, not merely the previous Codex report.
2. Review all six P1C corrections in **both** languages and their tests: generic `AGENT_RESULT` bypass closed; positive, in-range, distinct bound PIDs; rejected mutating operations irreversibly sticky-FAIL (including overflow and malformed report); only one timely EOF on agent→router after `UI_STOPPED`; non-bypassable report binding of channel/role/launch/PIDs/sequence/deadlines/type/outcome/state; strict Python integer/bool/timestamp/observation handling with matching .NET boundaries.
3. Prove from source that **validated report != actual positive kernel wait**. A staged report plus injected observation is only offline modeling, not D1/D2 runtime evidence. No generic event, alternate method, replay, EOF, internal-proof spoof, or missing observation may yield a clean shutdown verdict.
4. Verify exact 64-byte big-endian `aiq-admin-lifecycle-v1` wire contract, role/type/outcome catalog, reserved zeros, per-direction sequences, metadata ordering, monotonic deadline arithmetic, no `.NET Stopwatch` epoch substitution, canonical vectors and C1 bootstrap-only `A_EXEC`/`A_EXEC_ACK` launch gate. Bootstrap roles remain prohibited on runtime channels; ACK does not prove exec/readiness.
5. Verify A1–A3, D1/strict D2/D3, T1–T11 and C1 are not weakened; four-second shutdown, five-second Docker stop grace, five-second privileged startup, and 30-second total startup remain distinct. Actual kernel waits, identity confinement, FDs, session isolation, lineage, and cleanup remain **unqualified**.
6. Independently inspect tests for missing edge cases and whether any passing test relies on a bypass. If there is a material gap, ambiguity, or security concern, STOP with actionable findings; do not fix or create PR.
7. Only after the source review passes, run focused offline validation without altering project files: `python -B -m unittest python.presentation.test_admin_lifecycle_protocol -v`; .NET module build and filtered `AdminLifecycleProtocolTests` using `--no-restore` and temporary output/intermediate properties as previously demonstrated. Avoid generating tracked/ignored repository artifacts; if that cannot be ensured, STOP. Require **all tests pass, zero build warnings/errors, canonical parity**, and no unapproved file mutations. Capture commands and exact counts.
8. Recheck all four SHA-256 hashes and full Git status. Independently verify unrelated preservation to the extent possible (at minimum before/after hashes for Git-listed non-allowlisted files and unchanged untracked path inventory). Any discrepancy blocks PR creation.

**Gate A:** All checks above must be PASS with concrete evidence, no unresolved high/medium findings, no skipped/unknown required validations, no content/hash drift. If not: `WP07-P1D REVIEW BLOCKED — NO PR CREATED — OWNER REVIEW REQUIRED`.

## Phase 2 — Conditional isolated PR creation (only after Gate A PASS)

1. Verify GitHub repository/remote identity, authentication, branch protections, required checks, and merge permissions. If unclear, STOP. Do not relax protections, bypass required reviews/checks, or use admin override.
2. Preserve the dirty original checkout. Prefer a **separate temporary Git worktree or isolated clone** rooted at the verified protected HEAD; do not checkout or commit on the user's dirty draft branch. Create a uniquely named `codex/release-1.14-wp07-p1d-offline-protocol`-style feature branch from the verified intended base. If isolation cannot be done safely, STOP.
3. Copy **only** the four exact verified P1C files into the isolated branch. Inspect `git diff --name-status`, `git diff --check`, and the staged diff. Ensure no changes to any other path, no secret/credential material, and exact file hashes match. Commit only these four files with a descriptive P1C offline-only message. Do not amend or rewrite existing history.
4. Push only the new feature branch, open a PR against the repository's intended protected integration target (normally `main`, but confirm it is correct). PR title/body must describe offline-only P1/P1C code, the six corrections, focused test evidence, frozen contracts, explicit runtime non-qualification and production NO-GO. Include the four paths and hashes. Do not change release flags or GitHub project taxonomy.
5. Validate the **actual PR diff** still contains only the four authorized files and expected content. Check for unexpected base divergence/conflicts. If PR requires unrelated changes or a new interpretation of the base, STOP and leave the PR unmerged, reporting the URL.

**Gate B:** PR scope exactly four files; no unauthorized changes; target correct; no conflicts; repository policies and required reviewers/checks known and satisfied. Otherwise STOP and leave PR unmerged.

## Phase 3 — Conditional merge (only after Gates A and B PASS)

1. Wait for **all applicable required CI/status checks and required reviews** to complete successfully on the PR's current head SHA. Do not treat pending, missing, skipped, cancelled, neutral, or failing required checks as success. If a required check cannot run, STOP. Verify no new commits or review changes invalidated evidence.
2. Confirm GitHub reports the PR mergeable without bypass, no branch-protection override, and no unapproved file changes. Recheck PR diff, hashes, target, head SHA, and the production NO-GO boundary.
3. Merge via the repository's permitted merge method **only when all conditions are satisfied**. Do not force-push or bypass protections. Do not delete the protected draft branch; do not alter the original checkout. Feature-branch deletion is optional only if safely permitted by repository policy.
4. Record PR number/URL, merge commit SHA, actual merged file list, final check conclusions, and confirmation that the original dirty checkout remained unchanged. If GitHub cannot confirm the merge, report uncertainty rather than claiming success.

## Mandatory stop conditions

Stop immediately on baseline/hash mismatch, missing source or tests, failed/ambiguous review, validation failure, unexpected filesystem change, unsupported .NET test setup, unavailable permissions, unknown base/target, scope drift, required-check/review failure, merge conflict, GitHub protection restrictions, or any pressure to weaken D1/D2/C1/T9–T11. Never use `--force`, protection bypass, or broad staging (`git add .`). Do not autonomously repair and retry outside this authority.

## Final report and verdict

Report: initial/final branch, HEAD, status, four hashes, preservation inventory; finding-by-finding independent review; commands/test counts; Gate A/B/merge checks; exact PR diff; PR URL and merge SHA if applicable; remaining unqualified runtime evidence; confirmation `PUBLIC_RUNTIME_VERIFIED=False` and all production gates **NO-GO**.

Use exactly one applicable verdict:
- `WP07-P1D REVIEW BLOCKED — NO PR CREATED — OWNER REVIEW REQUIRED`
- `WP07-P1D REVIEW PASSED — PR BLOCKED — OWNER REVIEW REQUIRED`
- `WP07-P1D PR CREATED — MERGE BLOCKED — OWNER REVIEW REQUIRED`
- `WP07-P1D OFFLINE PROTOCOL PR MERGED — OWNER REVIEW REQUIRED`

**No runtime integration authority is granted by a successful PR merge.**

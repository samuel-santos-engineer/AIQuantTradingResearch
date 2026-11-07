# Codex execution prompt — Release 1.15 WP07 Azure staging blockers

## Baseline and objective

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from merged `main` containing PR #396, squash commit `8bbb8790fff96c895b2a3b4ec9d0200f1ac51d74`. Confirm the exact baseline before making changes. Work only in an isolated worktree/branch; inventory and preserve the original owner's dirty/untracked checkout, HEAD, status, and every owner file byte-for-byte. Do not merge the resulting PR.

Implement the smallest cohesive **code and local qualification** increment that removes or precisely narrows the three factual blockers identified in the merged Azure readiness runbook:
1. App Service's frontend HTTPS termination / HTTP-to-container routing versus the research service's current HTTPS-only port 52731.
2. Durable, private SQLite/witness/D5 storage and exclusive writer semantics across App Service restarts and overlapping replacements.
3. Secure, non-test operational initialization of D5 state and an honest initial research-data acquisition/startup path.

Read the actual repository code, governance, existing Dockerfiles, WP03/WP04/WP05 contracts, PR #395 evidence, and merged `docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md` first. Do not treat the runbook's suggested mechanisms as already implemented. Respect Docker-first and dual-process/two-image governance; do not import unmerged PR #386/#387. Preserve existing package versions, schema, D4/D5 behavior, strict Python HTTPS client, no raw-price browser exposure, and the local Linux qualification contract.

## Engineering decisions and implementation scope

**Transport:** Investigate actual App Service Linux custom-container routing and implement a narrowly scoped, explicit **platform-only HTTP backend listener** if necessary, with HTTPS-only at the Azure public frontend and unchanged authenticated snapshot behavior. The existing local Docker research HTTPS listener must remain the default and pass its existing tests. Fail closed on unknown modes or misconfiguration. Never expose plaintext as a general-purpose public API, silently relax the Python client's HTTPS/TLS/hostname verification, bypass `X-API-Key`, or infer that `httpsOnly` encrypts the backend hop. Document the trust boundary and App Service ingress limitations. Add tests for allowed/forbidden modes, request auth and bounded routing. If platform limitations make this unacceptable under governance, stop that subtask and report the exact incompatible contract and minimal alternative; do not invent a proxy or paid service.

**Storage:** Evaluate the actual F1 Linux App Service filesystem/mount capabilities and SQLite WAL requirements before choosing a backend. Do not claim Azure `/home`, an SMB/Azure Files mount, or local ephemeral disk is automatically suitable for durable WAL plus cross-instance writer exclusion. Do not substitute an untested network filesystem, turn off durability, discard the external watermark, reset generation/epochs, or invent a distributed lease. Prefer a practical mechanism supported by the **existing free staging resources**; if no mechanism satisfies durability and overlap, produce a concrete blocker and evidence instead of a fake PASS. Implement only changes that can be validated locally and are useful even if Azure storage remains blocked. Test lock/permission, crash/restart, witness integrity, and negative overlap paths; clearly distinguish local from Azure evidence.

**Initialization:** Provide a production-code, operator-invoked, idempotent-or-fail-closed path to create/activate private research state and D5 configuration, with separate API and HMAC secrets, explicit generation/key epoch/transform epoch, 0700/0600 permissions, and recoverable witness. Never log or commit secret values, place them in CLI arguments/image layers, or use the WP07 test fixture exporter as an Azure initializer. Define exactly where initial **real completed BTC/USD and ETH/USD 1-minute observations** come from and how they enter the existing WP03 pipeline; do not fabricate candles or claim readiness at fewer than 401 contiguous observations. Provider activation is **not authorized** by this prompt; implement a safe import/offline path where appropriate, and mark actual acquisition unverified. Preserve 1000-row retention, correction replay, gaps and D3/D4/D5 semantics. Avoid introducing an extra licensing approval gate.

Make the three areas separable and testable. Do not widen into architecture redesign, new managed services, paid tier, new packages, deployment framework, unrelated refactoring or feature additions. If one area is factually blocked, continue the independently safe parts and record its precise remaining prerequisite. Avoid unnecessarily delaying implementation for another planning-only PR.

## Execution and evidence

- Begin with read-only inspection and a short exact-path implementation inventory in your PR description. Edit only paths genuinely required for these three blockers and focused tests/evidence; do not modify owner prompts or unrelated files.
- Run the existing focused .NET/Python suites and, when inputs exist, the **actual Linux/amd64 two-container runner** with the exact pinned bases. Add tests for any new transport or initialization mode. Report actual commands, exit codes, hashes, and baseline-known legacy hover failure separately.
- Verify private DB and witness survival, writer exclusion and failure behavior under restart/abrupt termination locally; do not mislabel quiescent SIGKILL as power-loss durability.
- Recheck UTF-8 without BOM, LF, final newline, `git diff --check`, changed-file inventory and owner checkout preservation.
- Update the existing WP07 evidence **append-only**, and revise the staging runbook only where implementation changes its factual status. Explicitly classify each area as IMPLEMENTED_LOCAL, BLOCKED, or NOT_RUN; keep Azure-specific acceptance unverified without real execution.
- Open **one focused PR**, leave it OPEN and UNMERGED for owner review. Provide head SHA, file list, tests, failures, exact remaining Azure prerequisites, and a recommendation for the next step.

## Hard operational boundaries

**No Azure mutations, container registry pushes, deployment, new resource, plan/tier changes, production operations, live provider calls, credential retrieval, issue closures, or PR merges.** Existing staging apps and shared F1 plan may be inspected read-only if credentials already exist, but never display secret setting values. Do not assume availability or feasibility from CLI inventory alone. Keep `PUBLIC_RUNTIME_VERIFIED=False` until separately authorized and actually verified in Azure. Preserve the two distinct staging app identities and the server-side research HTTPS client contract wherever it applies.

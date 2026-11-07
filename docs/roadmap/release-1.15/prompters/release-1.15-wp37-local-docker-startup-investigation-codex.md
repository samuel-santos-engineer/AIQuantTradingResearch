# Codex task — Release 1.15 WP37: local-only Docker startup investigation

## Authority and current evidence

Work in `samuel-santos-engineer/AIQuantTradingResearch` on the **existing open PR #425 branch** (currently reported head `8d31b55`; fetch and verify actual head before editing). Do not create a competing PR. PR #424 was merged as `9196ba26893a80f79111318a88f3d7ad509f3ca1`.

The WP37 Azure Apply has already been attempted once: immutable image assignment and HTTPS-only were accepted, but public health failed. Later read-only Azure evidence showed recurring `WPStopRequests` quota exhaustion (17/15, reset 0/15, then 26/15), `QuotaExceeded/Exceeded`, and HTTP 403 site-disabled response. **Root cause of startup/stop requests is unproven.** WP35 had already demonstrated a local 200 `Alive`, so merely repeating a happy-path health test is insufficient.

The owner approves **local-only** Docker investigation and, if evidence warrants, a minimal local source/bootstrap/Dockerfile fix with tests on PR #425. The owner does **not** approve Azure access of any kind, new detector queries, Apply, restarts, settings operations, image publication, or a new deployment. Leave PR #425 OPEN/UNMERGED.

## Hard boundaries

- **Zero Azure requests** (including CLI inspect, resource queries, health checks, portal, logs, detectors, or authentication), even read-only. Do not invoke the WP36/WP37 Azure helper in a networked mode.
- Do not rebuild/push/retag any registry artifact as part of deployment. A **local-only** image build with a clearly local tag is permitted for reproducing and testing a source fix. No GHCR push, Azure image change, or deployment.
- Avoid registry requests if the exact image is already cached locally. If not cached, an anonymous GHCR pull of the exact immutable image is allowed only if needed for local reproduction; never use credentials or disclose tokens. If network or Docker is unavailable, record limitation and proceed with static analysis and fake-only tests.
- Never read, print, change, generate, or pass the real `AIQ_RESEARCH_API_KEY`. Use a syntactically valid **fake local test key** if an endpoint requires it; never put it in committed logs. Do not access Azure settings, connection strings, or private datasets.
- No changes to UI, Azure plan, governance, licensing, provider acquisition, authentic data, or unrelated functionality. Do not fabricate Azure platform markers to claim Azure equivalence or bypass existing security/storage guards.
- No repeated Azure stop requests or speculative Azure fixes. Do not claim that local success proves Azure F1 compatibility or fixes `WPStopRequests`.

## Local investigation plan

1. Inspect PR #425 diff and relevant existing files, particularly `container/release_1_15/research-api.Dockerfile`, `research-bootstrap.sh`, `src/AIQuantTradingResearch.Worker/Program.cs`, research endpoint/storage guard code, and WP35/WP36/WP37 tests and runbooks. Identify the exact published OCI image:
   `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`
   Linux/amd64 runtime manifest `sha256:fb9aab67753be84ca3300876fa4ed4d669ab0d897beb49f3fb8f2eae58b9b012`.
2. Establish a reproducible **local-only** harness with bounded runs and cleanup. Match container entrypoint `/bin/sh /app/research-bootstrap.sh`, API argument `--research-snapshot-api`, port `52731`, nonroot UID/GID `1654`, and the ten approved nonsecret environment settings where applicable. Bind the container port only to loopback (`127.0.0.1`) and do not publish it to the network. Do not invent Azure-specific host/instance/role identity values; document any guard that cannot be exercised faithfully locally.
3. Test beyond WP35's happy path: repeated cold starts, startup and shutdown timing, readiness versus process lifetime, `docker inspect` exit/OOM/restart state, bounded CPU/memory observation, writable `/tmp/aiq-research` under nonroot identity, expected mount-path handling, signal/termination behavior, and local HTTP `/health` (HTTP 200, exact five-byte `Alive`, anonymous). Compare baseline with constrained local memory/CPU; distinguish synthetic stress from real F1 measurements. Do not loop indefinitely or trigger external services.
4. Analyze app startup path and bootstrap for concrete defects (wrong mode, bind address, port, environment parsing, startup exceptions, permissions, initialization loops, missing files, background worker unexpectedly starting, process signal handling). Preserve exact failure evidence without sensitive values. If no reproducible defect appears, **do not invent a fix**; document negative evidence, remaining Azure-only hypotheses, and what a future separately authorized Azure diagnostic would need to distinguish.
5. If a defect is reproduced, implement the **smallest targeted fix** in the existing branch, with a regression test that fails before and passes after. Build and test a local image under a distinct local-only tag. Do not alter security, authentication, non-durable staging semantics, or real production secrets to make a test pass.
6. Run relevant existing and new offline tests; report counts, exact commands, observed exit codes, Docker availability, startup metrics and resource measurements where obtainable. Verify no changes to Azure, GHCR, UI, secret values or plan. Keep evidence sanitized.

## Deliverables

- Update the existing WP37 runbook `docs/roadmap/release-1.15/prompters/release-1.15-wp37-image-deployment-preflight.md` with a clearly separated **local Docker investigation** section: baseline, test environment, commands, observed outcomes, root-cause status, fix and verification (if any), remaining unknowns, and safe next step.
- Add only necessary local harness/test files and minimal application/bootstrap/Dockerfile changes justified by evidence. Avoid a new framework, unrelated refactors or complex rollback system.
- Commit to the existing PR #425 branch and update the PR description with the actual findings and validation. **Do not merge**.
- Stop if local reproduction is impossible or the evidence is inconclusive; request owner review rather than proceeding to Azure.

## Acceptance criteria

- No Azure call or mutation; no image push or deployment; no real API key access.
- Local tests are bounded and repeatable; health and process stability are independently checked.
- Any claimed fix has a demonstrated local before/after regression; otherwise root cause remains explicitly `UNRESOLVED`.
- PR #425 remains open/unmerged; status remains `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`.

Return a concise summary: changed paths, observed local evidence, tests, whether a reproducible defect/fix exists, PR head, and any blockers. Do not report Azure health as verified.

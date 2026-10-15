# Release 1.14 WP07 — Administrative REST Startup Failure and Disabled-Route Contract: Read-Only Diagnosis Authority

## Authority type and objective

**OWNER AUTHORIZATION: TARGETED READ-ONLY DIAGNOSIS ONLY.** Investigate the two failures from the first bounded local-container qualification of the administrative diagnostic REST API. Produce a source-indexed diagnosis and minimal repair proposal for owner review. **Do not implement repairs or repeat container qualification.**

This authority supplements the prior planning and offline-implementation record but does not expand implementation, deployment, or execution permissions.

## Frozen context and evidence

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Recorded baseline HEAD: `67893f51931ee183f71050a83b81127e6e648b87`. Confirm actual HEAD and status; STOP if the expected branch/baseline is unexpectedly different, and report rather than changing it.
- Existing working tree contains authorized but uncommitted implementation and pre-existing unrelated modifications/untracked files. Preserve all of them exactly.
- Offline candidate: 33 .NET tests + 6 bootstrap Python tests + 6 lifecycle Python tests passed; this does not prove container behavior.
- Local Docker qualification: one successful build, two starts, cleanup completed. Disabled UI/health returned 200; disabled `/admin/...` returned 200 with UI-like content rather than expected 404. Enabled startup exited **64** after **4.410 seconds**, logging only `AIQ_UI_OUTCOME=Unavailable` and `AIQ_ADMIN_OUTCOME=Unavailable`. Enabled identity, descriptor, lifecycle, HTTP/WebSocket and TLS/API were not qualified. Root causes remain unknown.
- Candidate admin identity `1100:1100`; existing presentation `1000:1000`; frozen public role reservations `1001–1003`.
- `PUBLIC_RUNTIME_VERIFIED=False`; public Worker activation **NO-GO**. Administrative API availability and Azure qualification **NO-GO**.

## Strict permissions

Permitted:

1. Read local Git metadata, tracked and untracked source files, Dockerfile, entrypoint, bootstrap, lifecycle agent, admin .NET project, existing tests and previously captured qualification evidence available in the repository or supplied context.
2. Inspect relevant static configuration and trace startup/route control flow manually; reason about shell/Python/.NET behavior from source.
3. Read authoritative public documentation **only if needed** to resolve a specific documented behavior; distinguish documentation from repository evidence and runtime observation.
4. Produce the diagnosis **in the Codex response only**; no output file is authorized.

Prohibited:

- **No file writes**, patches, generated files, formatting, staging, commits, branch changes, cleanup, or resets.
- No `dotnet build/test/restore/publish`, Python execution/tests, Docker build/run/exec/inspect, application startup, local HTTP requests, Worker execution, database or SQLite access, Azure commands/API/portal, GitHub mutations, secret retrieval, network probing, or diagnostic collection.
- Do not modify permissions, identities, environment settings, TLS policy, public gate, provider adapters, or any other runtime behavior.
- Do not read or print real credentials or environment-variable values; never include synthetic key values in the report.
- Do not assume that a hypothesized cause is established without source or runtime evidence.

If static analysis cannot establish a cause, label it **UNKNOWN**, state the exact missing evidence and propose the smallest *separately authorized* discriminating test. STOP if investigation requires any prohibited action.

## Investigation A — enabled startup exit 64

Trace the **actual implemented** control flow (not merely the previously approved design) from `Dockerfile` `ENTRYPOINT` and `container/entrypoint.sh` through `container/admin_diagnostics_bootstrap.py`, `container/presentation_lifecycle_agent.py`, and the administrative .NET startup, configuration, authentication, routing and lifecycle classes.

For each startup stage, identify:

- Which process and UID/GID executes it; which environment variables and file descriptors are expected (without reading values).
- The exact conditions that can yield exit code 64, `AIQ_UI_OUTCOME=Unavailable`, or `AIQ_ADMIN_OUTCOME=Unavailable`; which layer emits each event.
- Whether enablement parsing, canonical base64url decoding, generated synthetic-key delivery, pipe ordering/EOF, descriptor mapping, privilege drop, executable paths, ownership/modes, PID 1 handoff, agent messages, readiness polling or backend startup could cause the observed sequence.
- Whether the enabled launch incorrectly depends on provider secrets, persistence configuration, Worker state or private paths.
- Whether the logs are deliberately non-specific and what evidence they actually establish.

Provide a stage-by-stage failure tree and rank candidate causes as **PROVEN / STRONGLY INDICATED / POSSIBLE / UNKNOWN / EXCLUDED**, with literal `path:line` citations. Do not claim a root cause if it cannot be proven statically.

## Investigation B — disabled-route contract conflict

Trace the disabled launch and request routing for `GET /admin/diagnostics/v1/environment`:

- Verify whether the disabled path directly executes Streamlit on port 8501 and therefore bypasses all ASP.NET `/admin` route handling.
- Determine whether Streamlit's fallback behavior is consistent with the observed 200; distinguish source-proven control flow from framework behavior needing qualification.
- Reconcile the conflicting requirements: **(i)** disabled mode has no router/admin process and preserves direct Streamlit on 8501; **(ii)** disabled administrative URLs return uniform HTTP 404 with no collection.
- Compare at least these options: (A) retain direct Streamlit and explicitly accept/document a non-collecting UI fallback response; (B) retain a minimal front router in disabled mode to enforce 404; (C) another supported minimal design if demonstrably superior. Explain security, process/F1 cost, WebSocket compatibility, operational complexity and regression implications.
- Distinguish **no administrative collector exposure** from the stronger requirement of **404 status**. Do not silently redefine the existing contract or choose a repair without owner approval.

## Required deliverable (response only)

1. **Baseline and action ledger:** branch, HEAD, status preservation, exact categories of read-only operations; confirm zero mutations and executions.
2. **Evidence table:** precise repository `path:line` references for each finding; clearly identify observations taken from the owner's qualification report versus source analysis.
3. **Enabled-startup failure tree:** stages, exit/status emitters, candidate causes and confidence; smallest missing evidence if unresolved.
4. **Disabled-route contract assessment:** prove routing ownership and compare options with a clear recommendation and explicit owner decision.
5. **Minimal repair proposal:** exact proposed literal file paths, conceptual changes and targeted offline regression tests; **proposal only, no edits**. Separate any needed bounded local runtime probe into a future authority request.
6. **Security/non-regression checklist:** default-off, no key leakage, immutable trusted code, distinct UIDs, FD confinement, no private DB/Worker, WebSocket/UI behavior, TLS policy unchanged, fail-closed semantics.
7. **GO/NO-GO decision:** whether there is enough evidence to request narrow repair authority, what remains unknown, and STOP for owner approval.

## Hard gates

- No automatic repair or broad redesign.
- No weakening TLS, trusting unverified forwarded headers, logging secrets, bypassing authentication, introducing new storage, or activating the public Worker.
- Multiple-instance/revision-overlap analysis remains deferred for this diagnostic API phase.
- Existing SQLite-only storage constraint remains unchanged; the administrative API must not access SQLite or sidecars.
- Local-container and Azure qualification require distinct later authorities.

**STOP after reporting. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability NO-GO; Azure passive qualification NO-GO; public Worker activation NO-GO.**

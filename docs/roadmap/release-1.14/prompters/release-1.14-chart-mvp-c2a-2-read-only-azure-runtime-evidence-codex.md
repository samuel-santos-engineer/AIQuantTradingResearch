# Codex Authority — Chart MVP C2A-2: Read-Only Azure Runtime Evidence

## Owner authorization and objective
Owner authorized **C2A-2 only** following the completed C2A source-level diagnosis. Collect the smallest bounded, sanitized **read-only** operational evidence needed to identify why the existing public Streamlit application at `https://aiqr112wp035ec325382770.azurewebsites.net/` is not functioning. Do not remediate, deploy, restart, execute inside the container, or modify state. Advanced WP07 administrative lifecycle remains **ON HOLD**.

## Protected baseline
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Protected local branch: `draft/release-1.14-wp07-partial-public-disabled`; baseline HEAD `67893f51931ee183f71050a83b81127e6e648b87` (verify rather than assume).
- Checkout is intentionally dirty/untracked. Preserve every tracked, untracked and ignored artifact. No checkout, reset, clean, stash, fetch, pull, commit, push, PR or merge.
- `PUBLIC_RUNTIME_VERIFIED=False` must remain unchanged. Administrative diagnostics must remain disabled. Do not infer Azure production qualification from a healthy page.
- Expected public startup: `Dockerfile` → `container/entrypoint.sh` → non-admin Streamlit `0.0.0.0:8501`; checked-in guidance expects `WEBSITES_PORT=8501` and `/_stcore/health`. These are source expectations, **not observed deployment facts**.

## Exact authorization boundary
**Mutation allowlist: EMPTY.** Read-only source/Git inspection and Azure control-plane **GET/list/show** operations for the single named web app and its directly associated deployment/container diagnostics are allowed, only if an already-authenticated, authorized Azure CLI/session is available. Read-only retrieval of a small bounded existing log excerpt is allowed. No authentication initiation or changes, role elevation, key retrieval, credential inspection, secret-bearing environment dump, connection strings, application settings bulk dump, publishing profiles, SSH/Kudu command execution, filesystem browsing, deployment, restart, scale, configuration update, slot swap, Docker operation, test/build/import, or public/admin endpoint probing. Do not use commands with ambiguous side effects. Do not access other apps/resources unless separately authorized.

If the Azure session is unavailable, access is denied, the app cannot be uniquely identified, or a command would reveal secrets or mutate state: **STOP that collection path**, record the missing evidence and exact minimal access request; do not improvise. Never print tokens, subscription IDs, tenant IDs, principal identifiers, credentials, or private URLs. Prefer explicit allowlisted fields and redact incidental sensitive data before reporting.

## Execution sequence
1. **Preservation preflight:** Record complete `git status --porcelain=v1 -uall`, current branch, HEAD, local refs and a reproducible tracked-file hash inventory or aggregate. No Git network operations. Record current UTC timestamp. If baseline differs from expectation, note it; do not repair.
2. **Scope the Azure target:** Identify the exact existing App Service by its hostname/name using only safe, nonsecret read-only metadata. Do not guess resource group/subscription. If ambiguity remains, STOP and request owner-provided identifiers. Do not disclose identifiers unnecessarily.
3. **Read-only configuration evidence:** Obtain, where supported without secret exposure: running/stopped state and timestamps; configured container image **reference** and available immutable digest (mark digest GAP if absent); startup command override (redact if sensitive); nonsecret `WEBSITES_PORT` value; health-check path; startup timeout; App Service storage enabled/disabled; container runtime/image pull status; platform quota/availability indicators. For `AIQ_RUNTIME_MODE` and admin diagnostics flag, obtain only an explicitly allowlisted **nonsecret value** if a safe field-level query is possible; otherwise report UNVERIFIED rather than dumping settings. Never request values for other keys.
4. **Existing bounded logs:** If safe and authorized, retrieve a small timestamped excerpt of existing image pull, container start, and Streamlit application logs (e.g., last 100 lines per relevant stream, with a bounded time window). Do not enable logging, stream indefinitely, or request unrestricted historical archives. Sanitize before inclusion; do not include environment dumps, secrets, private paths or cache contents. Distinguish platform-generated errors from application errors.
5. **Evidence classification:** Map observations to this sequence: deployed image exists → container starts/survives filesystem guards → Streamlit listens on 8501 → Azure routes to 8501 → page renders → candle bridge intentionally unavailable. Separate OBSERVED, SOURCE-CONFIRMED, HYPOTHESIS and UNVERIFIED. A healthy `/_stcore/health` does not prove browser rendering. Do not treat `PUBLIC_RUNTIME_VERIFIED=False` bridge refusal as a startup defect.
6. **Preservation postflight:** Repeat branch, HEAD, complete Git status, local refs and hash inventory. Compare exactly. If a difference is detected, STOP and report; do not restore automatically.

## Required final report
- **Target and access:** Which app was identified, read-only access availability, observation timestamps, and commands/operations used (sanitized; do not echo secret-bearing output).
- **Evidence table:** Observed value or `UNVERIFIED`, source and timestamp, confidence, and implications for image, container, Streamlit, ingress, page and data gate. Explicitly state any evidence not retrievable safely.
- **Failure localization:** Earliest **observed** failing stage, if any; otherwise say root cause remains unverified. Rank next diagnostic actions by minimal risk, clearly distinguishing a separately authorized local smoke check from a code fix.
- **Security:** Confirm no admin activation, no private data access and no security-gate changes; if the flags could not be safely observed, say they remain unverified.
- **Preservation:** Before/after branch, HEAD, full status, refs, tracked hash results; acknowledge ignored/internal Git metadata not exhaustively covered unless actually checked.
- **Recommendation:** A tightly scoped next owner decision, with no implied authority to remediate or deploy.

## STOP / GO
STOP immediately on ambiguous app identity, unexpected permission boundary, suspected secret exposure, mutation requirement, unbounded logging, or checkout drift. No Azure changes and no repository changes are permitted. Do not conflate source expectations, cached refs or previous reports with live Azure evidence.

Final disposition must be exactly one of:

`CHART-MVP-C2A-2 READ-ONLY AZURE EVIDENCE COMPLETE — OWNER DECISION REQUIRED`

`CHART-MVP-C2A-2 BLOCKED — OWNER REVIEW REQUIRED`

**Production/admin activation remains NO-GO; advanced WP07 remains ON HOLD.**

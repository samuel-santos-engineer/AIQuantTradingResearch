# GPT-5.6 Terra — Release 1.12 WP04 Read-Only Azure Request-Path Investigation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: bounded read-only Azure request-path investigation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra authority.

## Governed state
Repository: `C:\projects\github\AIQuantTradingResearch`  
PowerShell: `Windows PowerShell 5.1.26100.9444`  
Source: `2532f6abd4677edfb205c26c083a534783038979`  
Image: `sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

Current Azure state must remain:
- six qualification settings absent;
- logging disabled;
- `WEBSITES_PORT=8501`;
- SCM/FTP basic auth false;
- registry credentials absent.

Actual diagnostic RunId:
`initialize-53207c5eb11a4ee8ad68019f8a228a64`

Procedure metadata RunId:
`initialize-c704973bd0194938ba9e0751e6e91a57`

Exactly one Azure qualification run occurred. Both IDs are permanently forbidden from reuse.

Retained evidence:
`C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57`

Expected archive SHA-256:
`4BA7E1E4D0D7ED12DD49EA44C2DF2EB77FD10E0BB3344CB513DB0ED1351A04F9`

## Binding prior findings
- `B6 — LISTENER_STARTED`: proven for actual RunId.
- `C4 — REQUEST_PATH_FAILURE_PROVEN`.
- HTTP 503 origin: Azure frontend/platform.
- request construction: proven correct.
- diagnostic D3 payload: attributable and valid, but **no acceptance credit**.
- RunId binding model: `RB2` — wrapper owns RunId; future metadata follows actual wrapper RunId.
- source remediation: no.
- helper remediation: no.
- new image: no.
- new diagnostic run: not authorized.

## Mission
Using only retained evidence plus read-only Azure/source inspection, determine the narrowest supported explanation for:

`listener started on 8501 → correct external HTTPS request → Azure/platform 503 → timeout → no REQUEST_ARRIVED/HANDLER_ENTERED`.

Focus on Azure frontend/platform routing between public HTTPS and the proven listener. Do not reopen SQLite/Worker/listener implementation without contradictory evidence.

## Mandatory read-only investigation

### 1. Evidence integrity
Verify retained archive SHA-256 before use. Do not delete, rename, rewrite, regenerate, or clean retained evidence.

### 2. UTC timeline
Reconstruct, where proven:
- qualification settings write/restart accepted;
- container creation/start;
- warm-up/startup-probe events;
- `QUALIFICATION_ENTERED`;
- `SQLITE_QUALIFICATION_STARTED`;
- `ARTIFACT_WRITE_SUCCEEDED`;
- `LISTENER_STARTING`;
- `LISTENER_STARTED`;
- first helper poll;
- HTTP 503;
- transport timeout;
- `LISTENER_STOPPING`, `LISTENER_STOPPED`, `WORKER_EXITING`, container exit if present;
- restoration start.

Calculate supported intervals:
- listener-started → first poll;
- listener-started → 503;
- listener-started → timeout;
- listener-started → stop/exit if available.

### 3. Listener lifetime
Search retained evidence for stop/exit/termination events. Return:
- `L1` listener proven alive throughout polling;
- `L2` listener/container proven to stop during polling;
- `L3` lifetime after listener start not proven.

### 4. Azure platform readiness
Inspect retained platform/container records for warm-up requests/results, startup probe success/failure, container ready, startup failure, recycle/termination, and routing/readiness evidence. Return:
- `P1` public-routing readiness proven before polling;
- `P2` startup/warm-up not complete when polling began;
- `P3` startup/warm-up failure proven;
- `P4` readiness not proven.

Do not equate application listener start with Azure public-routing readiness.

### 5. Container/routing identity
If instance/container/worker IDs exist, determine whether the listener-emitting instance was the active routed instance. Return `I1`, `I2`, or `I3` (not proven).

### 6. Port and startup contracts
Preserve existing `WEBSITES_PORT=8501` and listener-on-8501 proof. Read-only inspect additional applicable port configuration (`PORT`, `SERVER_PORT`, container metadata/siteConfig) and effective startup command.

Return:
- `PORT CONTRACT = PROVEN_MATCH | CONFLICT_PROVEN | NOT_PROVEN`
- `STARTUP COMMAND CONTRACT = PROVEN_MATCH | CONFLICT_PROVEN | NOT_PROVEN`

### 7. Health/startup/warm-up configuration
Read-only inspect applicable settings/fields including:
`healthCheckPath`,
`WEBSITE_HEALTHCHECK_MAXPINGFAILURES`,
`WEBSITES_CONTAINER_START_TIME_LIMIT`,
`WEBSITE_WARMUP_PATH`,
`WEBSITE_WARMUP_STATUSES`,
`WEBSITE_SWAP_WARMUP_PING_PATH`,
`WEBSITE_SWAP_WARMUP_PING_STATUSES`.

Classify each as `ABSENT`, `PRESENT_SAFE_VALUE`, `PRESENT_RELEVANT_VALUE`, or `NOT_PROVEN`. Never print secrets.

### 8. Qualification listener vs Azure warm-up
Determine whether Azure warm-up/startup probes target `/` or another path while qualification mode serves only `/internal/wp04/persistence-qualification`.

Return:
- `W1` compatible;
- `W2` path/readiness incompatibility proven;
- `W3` compatibility not proven.

### 9. 503 platform class
Using retained run evidence and official Microsoft documentation only as interpretive support, classify:
`503 PLATFORM CLASS = <specific proven class | CONSISTENT_BUT_NOT_PROVEN | UNKNOWN>`.

Distinguish documentation-based possibility from run-specific proof.

### 10. Activity Log limits
Return separately:
- config write accepted: proven/not proven;
- restart accepted: proven/not proven;
- restart completion/public routing: proven/not proven.

Never treat accepted control-plane operations as runtime readiness.

## Hypotheses
Evaluate:
- `R1` frontend routing activation lag;
- `R2` startup/warm-up probe incompatibility;
- `R3` container exited before routing/poll completion;
- `R4` stale/other instance routing;
- `R5` port/startup-command conflict;
- `R6` helper request defect (already disfavored);
- `R7` application listener defect (already disfavored);
- `R8` another proven platform cause;
- `R9` exact platform sub-cause remains unproven.

## Outcome
Select exactly one:
- `O1 — FRONTEND_ROUTING_ACTIVATION_LAG_PROVEN`
- `O2 — STARTUP_OR_WARMUP_PROBE_INCOMPATIBILITY_PROVEN`
- `O3 — CONTAINER_OR_LISTENER_LIFETIME_FAILURE_PROVEN`
- `O4 — STALE_INSTANCE_ROUTING_PROVEN`
- `O5 — PORT_OR_STARTUP_CONTRACT_CONFLICT_PROVEN`
- `O6 — OTHER_PLATFORM_CAUSE_PROVEN`
- `O7 — REQUEST_PATH_FAILURE_PROVEN; PLATFORM_SUBCAUSE_NOT_PROVEN`

If O7, identify the narrowest remaining evidence gap; do not mutate blindly.

## Boundaries
Allowed: retained files, current source, Azure Activity Log, `az webapp show`, `az webapp config appsettings list`, ARM/resource GETs, other read-only Azure metadata, official Microsoft documentation.

Forbidden:
- all Azure writes/restarts/logging changes;
- qualification settings/new RunId/new diagnostic attempt;
- Twelve Data secret configuration/readout;
- Kudu `/home`;
- direct SQLite/Python database inspection;
- repository/helper/source edits;
- Git/Docker/GHCR/GitHub/lifecycle mutations.

Preserve:
`INITIALIZE D3 ACCEPTANCE=NOT_PROVEN`, `REOPEN=NOT_AUTHORIZED`, WP04 incomplete, #263 open, WP05 not started.

## Required terminal markers
`RELEASE 1.12 WP04 — READ-ONLY AZURE REQUEST-PATH INVESTIGATION: PASS`
`RELEASE 1.12 WP04 — ACTUAL AZURE QUALIFICATION RUN ID: initialize-53207c5eb11a4ee8ad68019f8a228a64`
`RELEASE 1.12 WP04 — ACTUAL RUN ID REUSE: FORBIDDEN`
`RELEASE 1.12 WP04 — RETAINED ARCHIVE HASH VERIFICATION: <PASS|FAIL>`
`RELEASE 1.12 WP04 — B6 LISTENER STARTED: PROVEN`
`RELEASE 1.12 WP04 — C4 REQUEST-PATH FAILURE: PROVEN`
`RELEASE 1.12 WP04 — LISTENER LIFETIME: <L1|L2|L3>`
`RELEASE 1.12 WP04 — AZURE PLATFORM READINESS: <P1|P2|P3|P4>`
`RELEASE 1.12 WP04 — CONTAINER/ROUTING IDENTITY: <I1|I2|I3>`
`RELEASE 1.12 WP04 — PORT CONTRACT: <PROVEN_MATCH|CONFLICT_PROVEN|NOT_PROVEN>`
`RELEASE 1.12 WP04 — STARTUP COMMAND CONTRACT: <PROVEN_MATCH|CONFLICT_PROVEN|NOT_PROVEN>`
`RELEASE 1.12 WP04 — QUALIFICATION/WARMUP COMPATIBILITY: <W1|W2|W3>`
`RELEASE 1.12 WP04 — HTTP 503 ORIGIN: AZURE_FRONTEND_OR_PLATFORM`
`RELEASE 1.12 WP04 — 503 PLATFORM CLASS: <value>`
`RELEASE 1.12 WP04 — REQUEST CONSTRUCTION: PROVEN_CORRECT`
`RELEASE 1.12 WP04 — REQUEST-PATH PLATFORM HYPOTHESIS: <R1|R2|R3|R4|R5|R8|R9>`
`RELEASE 1.12 WP04 — REQUEST-PATH INVESTIGATION OUTCOME: <O1|O2|O3|O4|O5|O6|O7>`
`RELEASE 1.12 WP04 — RUNID BINDING MODEL: RB2`
`RELEASE 1.12 WP04 — SOURCE REMEDIATION REQUIRED: <YES|NO|NOT_PROVEN>`
`RELEASE 1.12 WP04 — HELPER REMEDIATION REQUIRED: <YES|NO|NOT_PROVEN>`
`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO|NOT_PROVEN>`
`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`
`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — CURRENT QUALIFICATION SETTINGS STATE: ABSENT`
`RELEASE 1.12 WP04 — LOGGING CURRENT STATE: DISABLED`
`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`
`RELEASE 1.12 WP04 — READ-ONLY AZURE REQUEST-PATH INVESTIGATION MUTATION AUDIT: PASS`

Then exactly one:
`RELEASE 1.12 WP04 — LUNA REQUEST-PATH REMEDIATION RECONCILIATION AUTHORITY: READY`
or
`RELEASE 1.12 WP04 — LUNA REQUEST-PATH EVIDENCE-GAP RECONCILIATION AUTHORITY: READY`

Final:
`RELEASE 1.12 WP04 — TERRA READ-ONLY AZURE REQUEST-PATH INVESTIGATION COMPLETE`

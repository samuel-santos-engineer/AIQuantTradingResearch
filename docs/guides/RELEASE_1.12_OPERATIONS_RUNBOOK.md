# Release 1.12 Operations Runbook

## Purpose and boundary

This runbook describes the accepted Release 1.12 public reference/demo deployment. It is not production hosting, an SLA, a high-availability design, or a trading system. Do not use it to infer paid fallback, automatic scaling, automatic secret copying, or unbounded resource creation.

The current accepted target is Azure App Service in **West US 2** on a **Linux F1/Free** plan. West Central US remains accepted historical feasibility evidence. The governed constrained-capacity order is:

```text
West Central US → West US 2 → Central US → South Central India
```

Use the next region only after recording capacity evidence for the current region. A regional recovery is a bounded reference-deployment recovery, not production failover or WP07 continuity evidence by itself.

## Accepted runtime identity

```text
Resource group: rg-aiq-r112-wp05-wus2-7f5eabb5
Web app: aiqr112wp05wus27f5eabb5
Plan: Linux F1/Free
Image: ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
Database path: /home/data/aiquant.db
SQLite schema: 4
SQLite journal mode: delete
```

The image is custom Docker from public/free GHCR. Use the immutable digest, never a floating tag, for a governed redeploy or rollback. No image rebuild is required for ordinary recovery.

## Prerequisites and configuration

Use Windows PowerShell 5.1.26100.9444-compatible syntax and an authenticated Azure CLI session. The application uses public/default App Service HTTPS/DNS and `WEBSITES_PORT=8501`.

Required configuration includes the tracked application settings appropriate to the deployed image, including `WEBSITES_PORT`, `Persistence__DatabasePath`, and `TwelveData__ApiKey`. Configure `TwelveData__ApiKey` directly on each target. Never print, copy, commit, log, or transfer its value between regional targets. Its presence may be checked by name only:

```powershell
az webapp config appsettings list `
  --resource-group rg-aiq-r112-wp05-wus2-7f5eabb5 `
  --name aiqr112wp05wus27f5eabb5 `
  --query "[?name=='TwelveData__ApiKey'].name" `
  --output tsv
```

Twelve Data behavior is bounded: the implementation uses one request attempt with a bounded provider deadline. Provider failure is isolated; it is not a reason to expose a secret or bypass the .NET-owned pipeline.

## Startup and public verification

On startup, the application uses persistent `/home` storage and maintains SQLite schema v4 with DELETE journal mode. The public root is the Streamlit presentation surface. Verify only public, non-secret endpoints:

```powershell
$base = 'https://aiqr112wp05wus27f5eabb5.azurewebsites.net'
Invoke-WebRequest -UseBasicParsing -Uri "$base/" -TimeoutSec 30
Invoke-WebRequest -UseBasicParsing -Uri "$base/_stcore/health" -TimeoutSec 30
```

HTTP 200 proves reachability, not an SLA. System Health is a truthful presentation of governed .NET-produced health/provenance data. Streamlit remains read-only: it does not own provider access, SQLite access, or Worker supervision.

## Persistence and recovery verification

Use the accepted WP04 verifier for governed persistence qualification with a fresh RunId. It verifies `/home` continuity, schema v4, DELETE journal mode, `integrity_check=ok`, `quick_check=ok`, and accepted evidence continuity. The verifier applies temporary qualification settings and restores them afterward; do not persist its capability token or output it.

```powershell
$runId = 'wp07-verify-' + [guid]::NewGuid().ToString('N')
.\eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1 `
  -ResourceGroup 'rg-aiq-r112-wp05-wus2-7f5eabb5' `
  -WebAppName 'aiqr112wp05wus27f5eabb5' `
  -Phase initialize `
  -RunId $runId `
  -LifecycleAction Restart
```

For a governed recovery scenario, capture the pre-state, use the applicable authorized restart, bounded recycle equivalent, or same-digest redeploy procedure, then rerun the verifier with a new RunId. A same-digest redeploy must retain the accepted digest shown above. Do not treat an ordinary restart as a substitute for a documented recovery scenario.

## Failure diagnosis and escalation

During App Service configuration or restart handoff, the public front door can temporarily serve Streamlit HTML while the qualification listener becomes available. The verifier treats that specific response as transient only inside its bounded poll window; accepted JSON evidence still receives full RunId, schema, journal, integrity, quick-check, and continuity validation. Malformed JSON, wrong RunId, or invalid evidence remains a failure.

For 503, timeout, or capacity symptoms, first distinguish App Service state, availability, configured immutable image, public root/health response, and F1 regional capacity. Do not attribute a platform or quota problem to the application without contemporaneous evidence. Escalate if recovery would require a paid tier, new paid service, non-governed region, secret retrieval/copying, architecture substitution, schema migration, or source/image change.

## Cost, rollback, and limits

Verify the target remains Linux F1/Free and inventory the resource group before and after a governed action. The `$0.00` statement is an accepted recurring-infrastructure architecture target, not a universal billing guarantee. No automatic paid upgrade is authorized.

Rollback or recovery uses the accepted immutable GHCR artifact and the same bounded F1 architecture. Do not introduce Azure SQL, Azure Files, Container Apps, mandatory ACR, Key Vault dependency, Front Door, Traffic Manager, load balancing, or a production HA claim.

## Evidence and lifecycle

Keep sanitized evidence for configuration identity, recovery timing, public checks, persistence verification, resource inventory, and cleanup. Do not record secrets, tokens, or credentials. This runbook does not close issues, set Project status, close the milestone, or publish Release 1.12.

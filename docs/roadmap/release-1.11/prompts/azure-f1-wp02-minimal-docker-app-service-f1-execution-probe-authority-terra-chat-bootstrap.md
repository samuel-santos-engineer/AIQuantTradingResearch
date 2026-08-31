Execute `azure-f1-wp02-minimal-docker-app-service-f1-execution-probe-authority-terra-codex-prompt.md`.

Use **GPT-5.6 Terra**.

This is WP02 of the non-release initiative:
`Public Reference Deployment / Azure App Service F1 Feasibility Qualification`.

Before any mutation, prove:
`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

Preserve:
- canonical release sequence `1.10 → 2.0`;
- Release 1.11 abandoned;
- milestone #60 untouched;
- strict recurring infrastructure cost `$0.00`;
- Azure as deployment-only;
- production architecture freeze.

Execute only the minimal isolated hosting probe needed to prove:
1. F1 availability in the actual subscription/region;
2. custom Linux Docker execution;
3. public HTTPS;
4. `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`;
5. writable persistent `/home`;
6. persistence across app restart;
7. persistence across container recycle;
8. persistence across image redeployment;
9. complete resource inventory with no paid supporting resource.

Do NOT:
- perform WP03 SQLite locking/journal qualification;
- call Twelve Data;
- modify production architecture;
- create Release 1.11/Release 2.0 governance objects;
- use paid fallback;
- authorize Phase B.

Use a minimal probe image/application.
Prefer a free registry path such as GHCR if available; do not make ACR mandatory.

Capture sanitized empirical evidence.
Default to cleanup after evidence unless WP01 explicitly authorizes minimum zero-cost resource retention for WP03.

A true platform failure may end as NOT FEASIBLE.
An access/credential/external blocker must end BLOCKED, not NOT FEASIBLE.

After PASS, complete WP02 lifecycle if a GitHub issue exists, then hand off to:
`GPT-5.6 Terra — Azure F1 WP03 Persistent SQLite Filesystem, Locking & Journal Qualification Authority`

End only with the exact COMPLETE, NOT FEASIBLE, or BLOCKED terminal.

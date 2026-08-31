Execute `init-1.11-wp02-execution-authority-amendment-terra-west-central-us.md` using **GPT-5.6 Terra**.

Resume the existing WP02 execution; do not create a new WP02 contract.

Binding target:
`WP02 AZURE TARGET REGION: West Central US`

Binding evidence:
- Brazil South F1 = 1/1 saturated and rejected.
- West Central US F1 = 0/30 available.
- Existing personal Azure resources must remain untouched.

Binding execution handoffs:
- **`sabsf` PowerShell executes Docker/WSL commands.**
- **`sabsf` PowerShell executes Azure CLI commands requiring the authenticated profile.**

For every handed-off batch:
- provide exact copy/paste-ready commands;
- state READ-ONLY/CREATE/UPDATE/RESTART/REDEPLOY/DELETE classification;
- state expected mutations;
- include labeled exit codes;
- tell me exactly what sanitized stdout/stderr/evidence to return;
- STOP and wait;
- independently evaluate results;
- never infer success.

Resume at the West Central US read-only/pre-mutation verification, then strict-$0 gate. Continue one evidence-bearing batch at a time through minimal deployment, all eight probes, resource disposition, zero-cost evidence, and mutation audit.

Only after:
`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

close #253 and ensure Project #2 Status is Done, avoiding redundant Project mutation if automation already did it. Leave milestone #62 Open and #254 Open/Todo.

Then emit:
`AZURE F1 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`
`AZURE F1 WP03 — EXECUTION AUTHORITY: READY`

End with the authority's exact COMPLETE, BLOCKED, or NOT FEASIBLE terminal.

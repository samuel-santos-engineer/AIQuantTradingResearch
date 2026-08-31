Execute `init-1.11-wp02-execution-authority-amendment-terra-manual-docker-handoff.md` using **GPT-5.6 Terra**.

The local tooling prerequisite has passed.

Binding execution rule:

**`sabsf` PowerShell executes Docker/WSL commands.**

For every Docker/WSL operation:
- give me the exact copy/paste-ready PowerShell commands;
- identify mutations and required evidence;
- include labeled exit codes where practical;
- STOP and wait for my stdout/stderr/results;
- independently evaluate the returned evidence;
- never infer success.

Run Azure CLI directly from Terra/Codex where possible. Do not silently extend the Docker handoff to Azure mutations.

Execute WP02 in order:
1. governance/repository preflight;
2. official Microsoft documentation refresh;
3. Azure auth/subscription/F1/strict-$0 pre-mutation gate;
4. minimal probe image;
5. zero-cost-compatible image distribution;
6. minimal Azure App Service Linux F1 deployment with persistent `/home`;
7. all eight empirical probes;
8. resource disposition/cleanup;
9. strict-$0 evidence;
10. mutation audit;
11. exact WP02 acceptance.

Only after:
`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

close GitHub issue #253 and ensure Project #2 Status is Done. Do not redundantly mutate Project Status if automation already did so. Leave milestone #62 Open and #254 Open/Todo.

Then emit:
`AZURE F1 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`
`AZURE F1 WP03 — EXECUTION AUTHORITY: READY`

End with the exact COMPLETE, BLOCKED, or NOT FEASIBLE terminal defined by the authority.

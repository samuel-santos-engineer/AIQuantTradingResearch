Execute `release-1.10-wp04-worker-interop-lifecycle-exporter-isolation-authority-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — contract/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — PRIMARY WP04 implementation/validation authority.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

WP03 is complete and #244 is Closed/Done. WP04 is #245.

Before ANY WP04 implementation mutation, freeze from the Release 1.10 definition, execution plan, file manifest, `OPEN_TELEMETRY_SELECTION.md`, #245, and current code/tests:
- exact production/test paths;
- exporter selection;
- package/version/project-file authority;
- lifecycle owner and init/disposal behavior;
- failure isolation;
- configuration;
- WP05 handoff.

Do NOT infer an exporter or add OpenTelemetry packages merely because OpenTelemetry was selected.

If any item is ambiguous: ZERO WP04 repository/Git/GitHub mutations, keep #245 Open/Backlog, and BLOCK for the minimum GPT-5.6 Luna WP04 contract/path reconciliation.

If deterministic, implement only WP04 while preserving WP03, schema v4, canonical JSON handoff, and Worker/Streamlit independence.

Windows validation may reuse only the documented local dev-signing flow for generated artifacts. Gitleaks approved tool: 8.30.1; canonical command: `gitleaks git . --redact --verbose`. Do not weaken security policy.

After exact `RELEASE 1.10 WP04 ACCEPTANCE: PASS`, close #245 and ensure its unique Project #2 item is Done. Avoid a redundant Status mutation if Project automation does it. Keep milestone #59 Open, #246–#249 unchanged, Git mutations ZERO, and do not start WP05.

End only with exact WP04 COMPLETE or BLOCKED terminal.

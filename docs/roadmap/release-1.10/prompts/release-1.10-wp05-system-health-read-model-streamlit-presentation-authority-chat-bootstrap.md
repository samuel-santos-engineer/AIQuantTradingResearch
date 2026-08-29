Execute `release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — contract/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — PRIMARY WP05 implementation, validation, and lifecycle completion authority.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

WP04 is complete and #245 is Closed/Done. WP05 is #246.

Before ANY implementation mutation, freeze from the Release 1.10 definition, execution plan, file manifest, `OPEN_TELEMETRY_SELECTION.md`, #246, WP04 handoff, and current .NET/Python/Streamlit code/tests:
- exact .NET System Health source/read-model path;
- exact canonical handoff representation;
- exact bounded status vocabulary and semantics;
- freshness/timestamp behavior if any;
- Python parser/frame/presentation paths;
- Streamlit component ownership;
- missing/malformed/backward-compatible behavior;
- exact tests;
- WP06 handoff.

Do NOT invent a new schema version, second health channel, status vocabulary, or freshness threshold. Do NOT let Streamlit inspect SQLite, providers, Worker processes, listeners, or exporter internals.

If any required item is ambiguous: ZERO WP05 implementation/Git/GitHub mutations, keep #246 Open/Backlog, and BLOCK for the minimum GPT-5.6 Luna WP05 reconciliation.

If deterministic, implement only WP05 and preserve schema v4, canonical JSON handoff, no external exporter, and Worker/Streamlit independence.

Use Gitleaks 8.30.1 with canonical `gitleaks git . --redact --verbose`; do not weaken security policy.

After exact:
`RELEASE 1.10 WP05 ACCEPTANCE: PASS`

close #246 and ensure its unique Project #2 item is Done. Avoid a redundant Status mutation if Project automation handles it.

Keep milestone #59 Open. Leave #247–#249 unchanged. Git mutations ZERO. Do not start WP06.

End only with exact WP05 COMPLETE or BLOCKED terminal.

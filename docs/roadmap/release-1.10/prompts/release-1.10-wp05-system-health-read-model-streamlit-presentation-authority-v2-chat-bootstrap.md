Resume WP05 using `release-1.10-wp05-system-health-read-model-streamlit-presentation-authority-v2-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — frozen WP05 contract/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — PRIMARY implementation, validation, and #246 lifecycle-completion authority.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

Luna WP05 reconciliation is COMPLETE.

Canonical frozen decisions:
- existing `Ready`, `WarmUp`, `Empty`, `Stale`, `Failed` visualization states remain separate from System Health;
- `aiq-visualization-read-model-v1` remains canonical;
- optional nested `systemHealth` compatibly extends v1;
- no second health channel;
- SQLite schema v4 unchanged;
- no independent System Health freshness threshold;
- exact .NET/Python/Streamlit production paths exist and are frozen;
- exact test allowlist is frozen;
- exact WP06 handoff is frozen.

Read the reconciled execution plan, file manifest, and `OPEN_TELEMETRY_SELECTION.md` for exact vocabulary, JSON shape, symbols, compatibility matrix, presentation mapping, and tests. Do not reopen those decisions.

Implement only the frozen WP05 paths. Project/package/schema changes are expected ZERO. No external exporter. Preserve Worker/Streamlit independence and Release 1.8 boundary.

Use Gitleaks 8.30.1:
`gitleaks git . --redact --verbose`
without weakening Windows/PowerShell policy.

After exact:
`RELEASE 1.10 WP05 ACCEPTANCE: PASS`

close #246 and ensure its unique Project #2 item is Done. If Project automation sets Done when the issue closes, do not issue a redundant Status mutation.

Keep milestone #59 Open. Leave #247–#249 unchanged. Git mutations ZERO. Do not start WP06.

End only with exact WP05 V2 COMPLETE or BLOCKED terminal.

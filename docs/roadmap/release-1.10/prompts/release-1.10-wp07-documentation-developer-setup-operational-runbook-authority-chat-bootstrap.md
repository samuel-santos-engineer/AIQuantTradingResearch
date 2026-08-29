Execute `release-1.10-wp07-documentation-developer-setup-operational-runbook-authority-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — frozen contract/architecture/governance.
- **GPT-5.6 Terra** — PRIMARY WP07 documentation implementation, validation, and lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

Entry:
- WP01–WP06 Closed/Done.
- #248 Open/Backlog.
- #249 Open/Backlog.
- milestone #59 Open, expected 2 open / 6 closed.
- accepted baseline: Application 136/136, Infrastructure 191/191, Architecture 27/27, Domain 11/11 = 365/365 .NET; Python 25/25; Streamlit 1.61.1; pip check clean; Gitleaks 8.30.1 clean; build 0 errors.
- pre-existing local `AIQuantTradingDev` selector warnings remain environment-only.

First consume the exact WP07 documentation path allowlist from the reconciled Release 1.10 manifest and WP06→WP07 handoff. If Terra would need to invent a documentation path/name, BLOCK before mutation for narrow Luna reconciliation.

Document only implemented facts:
- WP02 pipeline observability topology.
- WP03 Infrastructure provider/persistence observability.
- WP04 Worker lifecycle and no-external-exporter design.
- WP05 bounded System Health and deterministic Streamlit presentation.
- WP06 permanent-test locations and validation commands.
- canonical `aiq-visualization-read-model-v1` handoff.
- schema v4.
- deterministic/replay/simulated provenance.
- Python/Streamlit no-bypass.
- developer setup and bounded operational troubleshooting.

Do not claim live providers, exporter backend, trading, ML, backtesting, or parallel pipelines.

Repository mutations: exact WP07 documentation paths only. Production/test/project/package/schema/signing/Git mutations ZERO. Do not implement WP08.

Run full regression and:
`gitleaks git . --redact --verbose`

After exact:
`RELEASE 1.10 WP07 ACCEPTANCE: PASS`

close #248 and ensure its unique Project #2 item is Done. Do not redundantly set Done if issue-close automation already did it. Keep milestone #59 Open and #249 Open/Backlog.

Expected post-completion milestone state: 1 open / 7 closed.

End only with exact WP07 COMPLETE or BLOCKED terminal.

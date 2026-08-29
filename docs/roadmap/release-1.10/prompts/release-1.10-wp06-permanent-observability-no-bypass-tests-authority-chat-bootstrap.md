Execute `release-1.10-wp06-permanent-observability-no-bypass-tests-authority-codex-prompt.md`.

Use **GPT-5.6 Terra**.

Model map:
- GPT-5.6 Luna = contract/architecture/governance.
- GPT-5.6 Terra = WP06 implementation, validation, lifecycle completion.
- GPT-5.6 Sol = supporting analysis only.

Entry: WP01–WP05 Closed/Done; #247 Open/Backlog; milestone #59 Open at expected 3 open / 5 closed; #248–#249 Open/Backlog.

Consume the exact WP06 file allowlist and WP05→WP06 handoff from the reconciled Release 1.10 artifacts. Do not invent paths or redesign production.

Permanently enforce WP02 pipeline observability, WP03 Infrastructure observability, WP04 Worker lifecycle/no-exporter, WP05 bounded System Health (`ready`, `warmup`, `empty`, `failed`, `stale`, `unavailable`; no `degraded`), canonical v1 handoff, schema v4, provenance, Release 1.8 separation, and Python/Streamlit no-bypass boundaries.

Production/project/package/schema/Git mutations are expected ZERO. Do not implement WP07.

Baseline carried forward: 350/350 .NET; Python presentation 21/21; Streamlit 1.61.1; pip check clean; Gitleaks 8.30.1 clean over 112 commits; build 0 errors. Report actual new counts after WP06.

Run:
`gitleaks git . --redact --verbose`

Do not change signing/project configuration to address duplicate local `AIQuantTradingDev` selector warnings.

After exact:
`RELEASE 1.10 WP06 ACCEPTANCE: PASS`

close #247 and ensure its unique Project #2 item is Done. Do not redundantly set Done if issue-close automation already did it. Keep milestone #59 Open and #248–#249 Open/Backlog.

End only with exact WP06 COMPLETE or BLOCKED terminal.

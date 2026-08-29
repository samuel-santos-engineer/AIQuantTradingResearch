Execute `release-1.10-github-planning-materialization-authority-v2-codex-prompt.md` as the resumed Release 1.10 GitHub planning materialization authority.

## Model assignment
- **GPT-5.6 Luna** — contract/planning/architecture/reconciliation.
- **GPT-5.6 Terra** — execute now; approved GitHub planning mutations and authoritative read-back.
- **GPT-5.6 Sol** — supporting analysis/synthesis/non-authoritative review.

Use **GPT-5.6 Terra**.

The previous Terra run correctly blocked because per-WP contracts were incomplete. GPT-5.6 Luna has now completed them.

Reread:
- `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`

Require 8/8 deterministic materialization readiness and rerun the duplicate/idempotence gate from scratch.

Expected frozen WPs:
WP01 Observability Selection, Vocabulary & Scope
WP02 Application Pipeline Observability Contract
WP03 Infrastructure Provider, Persistence & Failure Instrumentation
WP04 Worker/Interop Lifecycle and Exporter Isolation
WP05 System Health Read Model and Streamlit Presentation
WP06 Permanent Observability and No-Bypass Tests
WP07 Documentation, Developer Setup & Operational Runbook
WP08 Full Validation, Acceptance & PR Readiness

Expected direct chain:
`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

If GitHub baseline remains compatible, materialize exactly eight Open issues into milestone #59 and Project #2, with Release=`1.10`, Status=`Backlog`, deterministic issue contracts, and accepted dependencies.

Repository mutations: ZERO.
Git mutations: ZERO.
GitHub mutations: accepted Release 1.10 planning objects only.

Do not implement WP01. After authoritative 8/8 read-back, identify the exact WP01 authority and selected model from the accepted plan.

End only with the exact COMPLETE or BLOCKED terminal marker.

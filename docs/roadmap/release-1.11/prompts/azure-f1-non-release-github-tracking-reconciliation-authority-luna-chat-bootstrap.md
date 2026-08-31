Execute `azure-f1-non-release-github-tracking-reconciliation-authority-luna-codex-prompt.md`.

Use **GPT-5.6 Luna**.

Create/reconcile the exact GitHub milestone:

`INIT-1.11 — Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Binding identity:
- `INIT-1.11` is an initiative identifier only.
- `INIT-1.11 ≠ Release 1.11`.
- Product Release 1.11 remains abandoned.
- Product release sequence remains `1.10 → 2.0`.
- milestone #60 remains Release 2.0.
- do not create Project Release option `1.11`.
- do not assign these WPs to Release `2.0`.

Create/reconcile six issues:
- Azure F1 WP01 — Feasibility Contract, Evidence Matrix & Resource Plan
- Azure F1 WP02 — Minimal Docker + App Service F1 Execution Probe
- Azure F1 WP03 — Persistent SQLite Filesystem, Locking & Journal Qualification
- Azure F1 WP04 — Twelve Data Outbound Connectivity, Secrets & Failure Isolation
- Azure F1 WP05 — F1 Resource Envelope & Strict-$0 Qualification
- Azure F1 WP06 — Feasibility Acceptance, Cleanup & Architecture Decision

WP01 already passed:
`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

Create its tracking issue retrospectively, document that fact, then close it and mark Project Status Done if applicable.

WP02–WP06 remain Open.

Use Project #2 only if the Release field can remain unset.

Expected clean milestone state:
- 5 open
- 1 closed

Do not execute Azure.
Do not install tooling.
Do not run the Docker probe.
Do not call Twelve Data.
Do not modify repository content.
Do not authorize Phase B.

After completion, report the actual milestone number and WP issue numbers for the Terra WP02 handoff.

End only with the exact COMPLETE or BLOCKED terminal.

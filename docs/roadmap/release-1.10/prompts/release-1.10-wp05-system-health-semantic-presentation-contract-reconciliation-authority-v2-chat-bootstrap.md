Execute `release-1.10-wp05-system-health-semantic-presentation-contract-reconciliation-authority-v2-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — PRIMARY semantic/presentation reconciliation authority.
- **GPT-5.6 Terra** — resumes WP05 implementation only after deterministic TERRA-READY.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Luna**.

Do not reopen the already-frozen WP05 architecture:
- visualization states remain separate from System Health;
- `aiq-visualization-read-model-v1` remains canonical;
- optional nested `systemHealth`;
- no second health channel;
- schema v4 unchanged;
- no independent System Health freshness threshold;
- exact production/test paths already frozen;
- no external exporter.

Terra V2 blocked only because the contract still lacks:
1. exact source predicate for `degraded`;
2. exact source predicate for `unavailable`;
3. exhaustive finite `reason` tokens;
4. deterministic state/reason mapping;
5. exact Streamlit placement;
6. exact Streamlit labels/messages;
7. exact malformed-health presentation behavior.

Freeze those from actual WP03/WP04 source facts and current Streamlit structure.

Required output must include an executable end-to-end truth table:
`.NET source condition → health state → reason token → serialized systemHealth → Python parsed/frame result → exact Streamlit placement/text → test assertion`

Do not fabricate telemetry. If a state cannot be truthfully sourced, reconcile/remove it if allowed or BLOCK.

Update only the minimum authorized Release 1.10 planning/architecture docs. Production/test/project/package/schema/runtime, Git, and GitHub mutations ZERO. #246 remains Open/Backlog. Milestone #59 remains Open.

Before completion prove:
`RELEASE 1.10 WP05 SEMANTIC/PRESENTATION MATERIALIZATION SIMULATION: PASS — TERRA-READY`

Then hand back to the existing WP05 Terra V2 authority.

End only with exact semantic/presentation reconciliation V2 COMPLETE or BLOCKED terminal.

# Release 1.15 WP08 — Codex acceptance runbook handoff prompt

## Governing context
Read `AGENTS.md`, `docs/governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md`, `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, and Release 1.15 definition/execution plan/file manifest/acceptance. Read the linked WP issue and accepted predecessor evidence before doing any implementation.

## Task
Reconcile R115-01–R115-12 against evidence, write operator recovery/rollback runbook and owner decision ledger. Never claim a gate passed without proof; deployment/activation remains separate.

## Required preflight
1. Inspect `main`, working tree, existing repository paths, actual tests and predecessor acceptance.
2. Propose the exact literal paths to change and run-only paths; obtain scoped implementation authority before writing code. This planning prompt alone does not authorize source changes, provider activation, image promotion, deployment or merge.
3. Preserve Release 1.14 history, existing security gates, non-trading scope and no licensing-specific engineering gate.

## Required delivery
Produce a narrow PR with diff summary, deterministic tests (including negative cases), commands/results, evidence, limits and any unresolved questions. No invented benchmarks or approvals. Stop when permissions, dependencies or paths are ambiguous.

## Mandatory project-wide line endings
All new or modified text files MUST be UTF-8 with LF (`\n`) only, including PowerShell. Never introduce CRLF (`\r\n`) or mixed endings. Follow root `.gitattributes`; independently scan changed text files for CRLF, run `git diff --check`, and avoid unrelated mass normalization.

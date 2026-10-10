# Release 1.14 WP01 — Codex Governance Amendment and Provider Strategy Reconciliation

## Mission and authority

This file **supersedes the earlier Vike public-source qualification mission under the same historical filename**. The owner approved a new direction: **independent synthetic public OHLCV + Twelve Data primary private research + Vike retained for a future qualified private backup**. The prior Vike qualification report remains historical evidence, not authorization for public activation.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`

**Controlling document:** `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`, including the October 2026 Release 1.14 amendment.

## Required preflight

Inspect current `main`, branch, worktree and controlling documents; reconcile current repo truth, not assumed SHAs. Inspect Release 1.13/1.14 planning, existing Twelve Data and Vike adapters, relevant provider-independent contracts, and public UI navigation. Do not overwrite pre-existing changes. Do not disclose or use provider credentials.

## Architecture decisions

1. Public preview uses **independently generated, deterministic synthetic OHLCV**, not copied, perturbed or model-generated derivatives of privately licensed data. Public label: **BTC-like Market Simulation — Synthetic Historical OHLCV**.
2. Twelve Data remains the primary **private** external provider. No Twelve Data data, caches, features, model outputs or credentials may enter public surfaces.
3. Preserve Vike as a **retained secondary provider** for a future release. Do not delete its adapter, activate it publicly, or implement automatic fallback. A later backup must establish instrument equivalence, license rights, data quality and provenance.
4. Canonical OHLCV contracts and shared SMA, EMA, RSI, MACD and Bollinger algorithms remain provider-independent. Preserve provenance, source context, UTC, instrument type and volume units.
5. Keep the zero-recurring-cost Azure F1 target.

## Public architecture diagram

The amended governance document contains the normative Mermaid diagram. The future public **Architecture & Data Governance** page must render that architecture as a visible, responsive graphic and provide an accessible text equivalent. Show synthetic public data, private Twelve Data, retained future Vike backup, canonical contracts, shared analytics and the publication gate. Distinguish implemented behavior from proposed future capabilities.

## Acceptance and governance

Preserve AC107–AC122 and G-1–G-4 with the revised meanings documented in the amendment. Verify synthetic-data labeling, reproducibility, isolation, no provider calls in anonymous public flows, and no cross-context fallback. Prior Vike technical qualification is conditional and must not be described as public-rights clearance.

## Mutation boundary

This document and the controlling amendment are **planning/governance artifacts only**. No application code, package manifests, schemas, tests, deployment settings, secrets, root README, Azure resources, or public feed activation are authorized. Do not create new issues, branches, commits, or PRs from this instruction without separate owner authority.

## Codex deliverable

Review the amended governance against repository truth, identify conflicting historical requirements requiring future reconciliation, and report a bounded implementation-ready **plan**, acceptance traceability, proposed future public-page integration, risks, and any governance blockers. Stop before implementation. Never claim the public architecture page is already deployed merely because its requirement and diagram are documented.

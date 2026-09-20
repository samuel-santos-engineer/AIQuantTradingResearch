# Release 1.13 Acceptance — Historical Market Visualization & Provider Abstraction

## Authority

Selected model: **GPT-5.6 Luna**.

Documentary correction model: **GPT-5.6 Terra**.

This document is the WP08 final reconciliation artifact. It records
documentary reconciliation only; it does not authorize implementation,
deployment, merge, issue closure, milestone closure, tagging, or a
GitHub Release.

## Canonical identity

- Canonical `main`: `fb946d77d0e8acb34d1bd91865bb62b2aad24160`
- Accepted WP07 candidate: `6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`
- WP07 merge commit: `128bbb4e9462990f6edd1a0e068828db95be1da8`
- PR #305 region-evidence merge commit: `fb946d77d0e8acb34d1bd91865bb62b2aad24160`
- Accepted deployed image digest: `sha256:18f60a172d51ff3d4c2b1bdf1b8089ba989cb5ef149adc3e5cf9488c9482f829`

`main` contains the accepted WP07 head. Its Release 1.13 change set is
the accepted nine-path set recorded by the WP07 merge authority. Root
`README.md`, dependencies, and schema are unchanged.

## Reconciled implementation and evidence

WP01–WP07 implementation remains consistent with the provider-independent
C# contract, the bounded Worker/stdio bridge, cache-first historical
reads, server-side Vike access, the public Streamlit presentation
boundary, controlled failure states, and the Twelve Data
private/internal boundary.

The accepted WP07 evidence records successful local and container BTC/USD
and ETH/USD acquisition, public rendering, alternate controls, restored
defaults, Vike/UTC provenance, System Health, public information safety,
and the Linux F1/Free zero-recurring-cost boundary. The accepted Vike
adapter correction parses the observed named candle fields
`ts`, `open`, `high`, `low`, `close`, and `volume` while retaining strict
validation and compatibility coverage.

The canonical validation evidence includes:

- Release build: 0 warnings, 0 errors;
- Domain tests: 11/11;
- Application tests: 168/168;
- Architecture tests: 27/27;
- Infrastructure tests: 238/238;
- focused Vike tests: 24/24;
- Python presentation tests: 36/36;
- Python compilation, dependency, whitespace, parser, and secret checks:
  pass.

## WP07 Azure-region reconciliation

The initial WP08 reconciliation correctly identified that the prior
`WP07_AZURE_F1_VALIDATION.md` statement attributed the accepted WP07 App
Service to **West US 2** while authoritative evidence identified the
specific existing Linux F1/Free App Service
`aiqr112wp035ec325382770` in **West Central US**, with plan
`asp-aiq-r112-wp03-wcus-5ec325382770`.

PR #305 was substantively reviewed and merged into canonical `main`.
It corrected that WP07 evidence statement to **West Central US**. The
correction is resource-specific: it describes that accepted WP07 App
Service only. It does not make a project-wide Azure-region assertion.

Separately valid **West US 2** evidence remains preserved for Release
1.12 accepted-target documentation, the constrained regional recovery
order, the operations-runbook context, and legitimate historical or
alternate deployment contexts supported by repository evidence. Those
contexts are not conflated with the accepted WP07 App Service.

Chronology: the initial Luna reconciliation identified the documentary
discrepancy; PR #305 was reviewed and merged; canonical WP07 evidence
now records the resource-specific West Central US fact; the resumed Luna
reconciliation identified this untracked artifact as stale; this Terra
documentary correction updates the artifact. Final acceptance remains
pending a fresh Luna reconciliation.

## Lifecycle and release boundary

- WP07 issue #294: closed; existing Project item: Done.
- WP08 issue #295: Open and not started.
- Release 1.13 milestone #64: Open.
- Release 1.13 tag: absent.
- Release 1.13 GitHub Release: absent.
- Release 1.14 and Release 2.0 implementation: not begun by this
  reconciliation.
- Root README: protected and unchanged.

## Conclusion

`WP08SubstantiveAcceptanceResult=PASS`.

The prior Luna BLOCKED conclusion was valid before the canonical region
correction and before this artifact was refreshed. The identified
documentary blocker is now corrected, and GPT-5.6 Luna's fresh final
reconciliation has accepted Release 1.13:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: PASS`

This accepted result authorizes only the separately governed publication
and lifecycle actions. It does not alter the implementation, Azure,
deployment, or README evidence recorded above.

Required next authority: GPT-5.6 Terra final governance publication and
Release 1.13 lifecycle completion.

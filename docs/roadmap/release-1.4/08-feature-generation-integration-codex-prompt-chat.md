Execute Release 1.4 WP08/#160 — Feature Generation Integration — under the full WP08 authority and accepted WP02–WP07 semantics plus Release 1.2 snapshot contracts.
Implement only Application-owned exact snapshot lookup → validation → deterministic feature computation, preserving NotFound, dependency-unavailable, invalid-evidence/numeric, and unknown-defect behavior.
Preserve empty/single success, `aiq-feature-identity-v1`, exact snapshot/version binding, schema v2, and the Release 1.3 five-stage pipeline.
Do not add persistence, DI, configuration, Worker, provider/network, packages/references, or permanent tests; use only removable offline probes when necessary.
Close #160 only after the WP08 matrix and canonical verification pass, leave #161 Open/Backlog, and end with `RELEASE 1.4 WP08 COMPLETE`.

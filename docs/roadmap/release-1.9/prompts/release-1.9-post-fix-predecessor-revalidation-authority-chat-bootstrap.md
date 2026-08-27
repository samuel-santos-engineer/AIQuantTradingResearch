Execute `release-1.9-post-fix-predecessor-revalidation-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Treat the late Windows atomic-replacement race fix as an accepted candidate change in exactly two files:
- `VisualizationReadModelFilePublisher.cs`
- `VisualizationReadModelFilePublisherTests.cs`

Do not modify anything further. First audit the diff against accepted WP05/WP08/WP09 handoff contracts and confirm the production change is only a bounded <=200 ms retry for transient UnauthorizedAccessException/IOException replacement contention, while persistent failures still surface; confirm the test change only adds FileShare.Delete compatibility without weakening assertions.

Then revalidate: atomic failing test 3/3, publisher suite 4/4, Infrastructure 182/182, WP08 lifecycle 18/18, WP09 permanent integration plus architecture 8/8, build 0/0, full .NET 339/339, Python 17/17, Streamlit 1.61.1, clean pip check, schema v4/no-bypass preservation, and zero owned residue.

Make zero repository, Git, and GitHub mutations. Leave #237 Open/Backlog and milestone #58 Open. On full success state that WP12 PR-readiness may resume against the updated two-file predecessor fix. End with the authority's exact terminal marker.

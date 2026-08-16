Execute Release 1.1 WP14 using `14-infrastructure-persistence-tests-codex-prompt.md` as the authoritative execution contract.
Read the complete authority plus Release 1.1 and accepted WP01–WP13 evidence, inventory the existing 65 Infrastructure tests, and map real WP06–WP12 coverage gaps before mutation.
Add only permanent Infrastructure/persistence tests using isolated offline SQLite; do not duplicate WP13 Domain/Application coverage, change production behavior, or add packages/references without explicit authority.
Run targeted Infrastructure.Tests plus the full canonical suite, prove database cleanup and zero network calls, manage only issue #116 lifecycle, and leave WP15 untouched.
Do not stage, commit, push, create a PR, or begin WP15; emit the prescribed WP14 terminal only if every mandatory gate passes.

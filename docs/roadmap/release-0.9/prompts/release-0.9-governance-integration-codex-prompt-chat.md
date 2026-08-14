# Codex Prompt Chat --- Release 0.9 Governance Integration

Execute the authoritative Release 0.9 governance integration prompt
located at:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-governance-integration-codex-prompt.md
```

Before taking any action:

1.  Read that authoritative prompt completely.
2.  Read these Release 0.9 governance authorities completely:

``` text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

3.  Inspect and classify every existing file under:

``` text
docs/roadmap/release-0.9/
```

4.  Preserve the repository convention that every authoritative:

``` text
*-codex-prompt.md
```

may have an intentional companion:

``` text
*-codex-prompt-chat.md
```

The `-chat` companion is a deliberate repository artifact retained for
clarity, repeatability, future reference, and AI-assisted engineering
traceability. Do not classify a valid companion as accidental or
unrelated solely because it has the `-chat` suffix.

5.  Verify the current Git/GitHub state, GitHub authentication,
    milestone #40, issues #69--#82, and the accepted technical baseline
    exactly as required by the authoritative prompt.

Then execute the authoritative prompt exactly as written.

This run is **Release 0.9 governance integration only**.

Do not:

-   redesign Release 0.9;
-   re-plan GitHub unless a material discrepancy is discovered;
-   modify Release 0.8 history;
-   modify source or test code;
-   modify build/configuration assets;
-   modify engineering scripts;
-   create CI/workflows;
-   stage unrelated, generated, protected, or ambiguous files;
-   expose credentials or tokens;
-   commit directly to `main`;
-   force-push;
-   bypass required review;
-   start WP01;
-   change WP01 to `In Progress`;
-   implement any Release 0.9 work package.

Use a dedicated forward-only integration branch from synchronized
`main`.

Validate the technical baseline before staging.

Stage only the approved Release 0.9 governance artifacts under:

``` text
docs/roadmap/release-0.9/**
```

Inspect the exact staged delta before committing.

Commit and push according to repository conventions, then create the
governed pull request targeting `main`.

If human review or merge remains required, stop at that boundary and
report:

``` text
COMPLETE WITH ACTIONS
```

Do not bypass the review/merge gate.

If the governance integration is actually merged during the authorized
flow, synchronize `main`, revalidate the repository baseline, and
report:

``` text
COMPLETE
```

If safe execution is blocked by a material conflict, report:

``` text
BLOCKED
```

At completion, return the full:

``` text
Release 0.9 Governance Integration Execution Report
```

using the exact output contract defined by the authoritative prompt.

Finish with exactly one:

``` text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

When governance authority is fully integrated into `main`, identify:

``` text
WP01 — Repository & Release Preflight
```

as the next authorized work package.

**Do not execute WP01.**

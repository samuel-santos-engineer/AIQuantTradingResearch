Execute the authoritative Release 0.9 GitHub planning prompt located at:

```
docs/roadmap/release-0.9/prompts/release-0.9-github-planning-codex-prompt.md
```

Before taking any action:

1. Read that prompt completely.
2. Read these two authoritative Release 0.9 governance artifacts
   completely:

```
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

3. Inspect the repository's existing GitHub governance, labels,
   milestones, issues, project/roadmap conventions, and current remote
   state as required by the authoritative prompt.
4. Verify GitHub authentication and repository identity without
   exposing credentials.

Then execute the authoritative prompt exactly as written.

The purpose of this run is  **GitHub planning only** .

Create or reuse:

```
1 authoritative Release 0.9 milestone
14 authoritative Release 0.9 work-package issues (WP01–WP14)
```

Derive the milestone and issue content strictly from the Release 0.9
execution plan and file manifest.

Do **not** redesign Release 0.9.

Do  **not** :

* invent, remove, merge, split, or rename work packages;
* create WP15;
* create a release-closure issue unless separately authorized later;
* start WP01;
* implement Release 0.9 source or tests;
* modify engineering/build assets;
* create CI or workflows;
* create duplicate milestones or issues;
* invent project fields, labels, priorities, areas, or due dates when
  repository authority does not support them;
* expose GitHub credentials or tokens.

Reuse existing repository conventions and existing GitHub planning
objects whenever appropriate.

If a material conflict prevents safe execution, stop rather than
guessing and report the blocker according to the authoritative prompt.

At completion, return the full:

```
Release 0.9 GitHub Planning Execution Report
```

using the exact output contract defined by the authoritative prompt.

Finish with exactly one planning decision:

```
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If the result is `<span>COMPLETE</span>`, identify:

```
WP01 — Repository & Release Preflight
```

as the next authorized work package, but  **do not execute WP01** .

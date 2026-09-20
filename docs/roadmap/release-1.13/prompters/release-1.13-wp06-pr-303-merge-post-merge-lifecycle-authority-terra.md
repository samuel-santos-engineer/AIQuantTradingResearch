# Release 1.13 WP06 --- PR #303 Merge and Post-Merge Lifecycle Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Luna completed substantive acceptance. Terra owns only this
bounded merge and post-merge lifecycle operation.

## Target

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#303 — Release 1.13 WP06: public research messaging`

Expected branch: `feature/release-1.13-wp06-public-research-messaging`

Required pre-merge canonical main:

`79486313f3e01a5004e0d983e95b57ce8020926a`

Initial candidate: `e3a5b2bc773f1fba64976dd960507e0b1886f001`

**Final Luna-accepted head:**

`f034eef0ad88f23e02953e11f6b10b08b0051d38`

Luna acceptance marker:

`RELEASE 1.13 WP06 PUBLIC RESEARCH MESSAGING AND INFORMATIONAL SURFACES FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Accepted corrections include non-UTC provenance rejection and behavioral
tests replacing source-text inspection.

## Exact accepted scope

Exactly these four paths may land:

1.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
2.  `docs/roadmap/release-1.13/prompters/release-1.13-wp06-terra-public-research-messaging-informational-surfaces-controlled-states-implementation.md`
3.  `python/presentation/realtime_financial_visualization.py`
4.  `python/presentation/test_market_research_ui.py`

No fifth path is authorized. `README.md` remains byte-for-byte
protected.

## Pre-merge gate

Verify live PR #303 is Open, unmerged, non-draft, mergeable, based on
`main`, and its head is exactly the final Luna-accepted SHA.

Verify the diff is exactly the four paths above; dependencies are
unchanged; README is absent; and there is no Worker, bridge, provider,
cache, schema, Docker, Azure, GHCR, deployment, tag, or release
mutation.

Verify issue #293 remains Open/non-Done, milestone #64 Open, issues
#294/#295 Open, and no Release 1.13 tag/GitHub Release exists.

If the PR head differs from the accepted SHA, stop. Substantive drift
requires renewed Luna acceptance.

## Accepted behavior preservation

Confirm the accepted head retains:

-   Market Research default;
-   truthful Vike provenance with validated UTC timestamp;
-   rejection of non-UTC provenance timestamps;
-   accepted Twelve Data public/private research-boundary wording;
-   informational ML & Automation Studies roadmap/status;
-   explicit no-trade statement;
-   research/demo no-trade footer;
-   controlled fresh/stale/empty/unavailable states;
-   safe System Health failure rendering;
-   WP05 chart, controls, bridge, and retry behavior;
-   no provider acquisition from Python/browser;
-   no dependency changes.

## Pre-merge validation

Run/report:

-   exact path proof;
-   `git diff --check`;
-   secret/public-information scan;
-   provider no-bypass scan;
-   dependency unchanged proof;
-   README unchanged;
-   repository cleanliness/staging accounting.

Acceptance baseline:

-   focused WP06/legacy tests: 20/20;
-   full presentation suite: 35/35;
-   Python compile: pass;
-   `pip check`: pass.

Rerun relevant checks before merge where repository lifecycle precedent
requires it.

## Merge

Use the normal merge-commit method established for Release 1.13.

Do not direct-push main, force push, bypass protection, amend the
accepted head, or merge another PR.

Capture the actual merge timestamp and merge commit.

## Post-merge verification

Fetch canonical `origin/main` and:

-   record its new SHA;
-   prove `f034eef0ad88f23e02953e11f6b10b08b0051d38` is reachable;
-   prove exactly four accepted paths landed relative to pre-merge main;
-   prove README unchanged;
-   run whitespace, secret/public-information, and no-bypass checks;
-   prove dependencies unchanged;
-   run relevant presentation tests and Python compile checks;
-   confirm no
    Worker/provider/cache/bridge/schema/Docker/Azure/GHCR/deployment/tag/release
    mutation.

## WP06 lifecycle

Only after successful post-merge verification:

-   close issue #293;
-   verify Project #2 WP06 transitions to Done using established
    issue-close automation/equivalent precedent;
-   verify Project Release remains `1.13`;
-   keep milestone #64 Open;
-   keep issues #294 and #295 Open.

Do not close #294/#295.

## Next boundary

WP07 (#294) becomes eligible for a separate authority only after PR #303
is canonical, issue #293 is Closed, and Project WP06 is Done.

Do not perform WP07 implementation, Azure/Docker/GHCR/deployment work,
WP08, tag creation, GitHub Release creation, milestone closure, or
README changes.

## Retry-until-governance-boundary rule

Do not stop for ordinary fetch, mergeability, test, compile, whitespace,
scan, merge, issue-close automation, or lifecycle tooling failures.
Diagnose and retry actions permitted by this authority.

Do not mutate accepted implementation.

Stop only if the next corrective action crosses governance, including
head drift requiring Luna review, a fifth path,
implementation/dependency change, README,
Worker/provider/cache/bridge/schema mutation, deployment mutation,
branch-protection bypass, direct/force push, WP07 implementation,
closing later issues, milestone closure, tag, or GitHub Release.

When blocked report:

`GOVERNANCE RESTRICTION — STOPPED`

## Final PASS requirements

Confirm:

-   PR #303 merged normally;
-   actual merge commit and canonical main captured;
-   accepted head reachable;
-   exactly four paths landed;
-   README unchanged;
-   dependencies unchanged;
-   whitespace/secret/no-bypass clean;
-   relevant tests/compile pass;
-   issue #293 Closed;
-   Project WP06 Done;
-   Project Release `1.13`;
-   milestone #64 Open;
-   #294/#295 Open;
-   no Release 1.13 tag/GitHub Release;
-   no deployment or WP07 work.

## Required final report

Return pre-merge main, PR state/base/branch, accepted head, exact paths,
validation, merge method/timestamp, merge commit, new canonical main,
ancestry proof, landed paths, post-merge tests/scans, dependency/README
proof, issue #293 and Project WP06 state, Project Release field,
milestone/#294/#295 state, tag/release state, technical lifecycle
failures corrected, governance restrictions, and next separately
authorized action.

End with exactly one of:

`RELEASE 1.13 WP06 PR #303 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP06 PR #303 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS completes WP06 lifecycle only and does not authorize WP07
implementation, deployment, tag, milestone closure, or release.

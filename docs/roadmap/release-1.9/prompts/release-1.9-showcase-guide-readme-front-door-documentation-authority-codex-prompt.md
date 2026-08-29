# Release 1.9 — Showcase Guide + README Front-Door Documentation Authority

## Model
Use **GPT-5.6 Terra**.

## Purpose
Add the prepared Release 1.9 showcase/local-run guide and one concise front-door reference from the root `README.md`.

This is documentation-only. It must not reopen or mutate the published Release 1.9 lifecycle.

## Published state to preserve
- PR #238 merged.
- PR #239 governance follow-up merged.
- tag `v1.9.0` remains anchored to `e4958721c9a581efbb2552134c00bc146c73f047`.
- GitHub Release `v1.9.0` remains published, non-draft, non-prerelease.
- milestone #58 remains Closed, 0 open / 13 closed.
- #233–#237 remain Closed / Done.
- accepted Release 1.9 baseline: .NET 339/339, Python 17/17, schema v4, Streamlit 1.61.1, `pip check` clean.

## Source and destination
Use the prepared artifact:
`RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Destination:
`docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

If the artifact is not directly mounted, use its supplied content as canonical. Do not invent a materially different guide.

## Exact writable paths
Only:
1. `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`
2. `README.md`

No other path may change.

## Entry-state gate
Read current branch, HEAD, `origin/main`, `git status`, staged paths, current README, `docs/guides/`, and the canonical interoperability/Python/signing documentation.

Preserve all unrelated dirty work. Do not reset, clean, stash, or discard it.

If either writable path contains unrelated user edits that cannot be safely preserved, STOP.

## Guide requirements
The guide must substantially explain:
- why Release 1.9 matters;
- .NET → atomic canonical JSON handoff → Python/Streamlit architecture;
- deterministic simulated/replay-data disclosure;
- prerequisites and environment setup;
- build/test verification;
- Worker producer and Streamlit consumer startup;
- Ready/WarmUp/Empty/Failed semantics;
- refresh/restart/lifecycle behavior;
- Windows atomic-replacement robustness;
- Smart App Control local-signing boundary;
- troubleshooting;
- accepted Release 1.9 quality baseline;
- a useful technical showcase/demo narrative;
- explicit non-claims: no live broker/provider connectivity.

Preserve:
- no Streamlit → SQLite bypass;
- no Streamlit → market-provider bypass;
- no Streamlit supervision of Worker;
- governed JSON handoff ownership.

## Command/path truthfulness audit
Before acceptance, verify every runnable command and referenced repository path against the current checkout, especially:
- `eng` scripts;
- Worker `.csproj`;
- Python presentation entry point;
- Python test invocation;
- Streamlit command;
- interoperability guide;
- Python developer guide;
- Smart App Control signing guide.

If the prepared guide is stale, correct the guide only. Never modify implementation to make documentation true.

Do not present illustrative commands as canonical commands unless verified.

## README front door
Modify root `README.md` minimally.

Add one discoverable link in the most appropriate existing navigation/getting-started/documentation/release section.

Preferred anchor text:
`Release 1.9 showcase and local run guide`

Target:
`docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

A concise description may say it covers running the .NET → JSON handoff → Streamlit demonstration, architecture, and troubleshooting.

Avoid a redundant large README section.

## Link and consistency validation
Verify:
- README link resolves;
- new guide local links resolve;
- referenced repository files exist;
- repository navigation uses relative links;
- schema language is v4;
- Streamlit 1.61.1 is correct where stated;
- 339/339 and 17/17 are described as Release 1.9 acceptance evidence;
- simulated/replay disclosure is explicit;
- no live connectivity overclaim exists.

Cross-check with:
- `README.md`
- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
- `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`
- `docs/project/ROADMAP.md`

## Validation
At minimum:
1. inspect diff for both authorized paths;
2. validate Markdown links;
3. validate commands/paths;
4. verify no secrets/local signing material;
5. verify only the two authorized paths changed by this authority.

Run an existing cheap docs/lint check if available.

This is documentation-only; do not rerun the full 339/339 + 17/17 matrix unless repository policy explicitly requires it.

## Git/GitHub boundary
Repository documentation edits are authorized.

Not authorized:
- staging;
- commit;
- branch;
- push;
- PR;
- merge;
- tag;
- GitHub Release;
- milestone;
- issue/Project mutation.

A separate PR authority is required.

## Forbidden repository paths
Do not modify:
- `src/`
- `tests/`
- `python/`
- project/package/dependency files;
- schema/migrations;
- signing scripts/config;
- `Directory.Build.local.props`;
- roadmap authority artifacts;
- workflows;
- generated/runtime/test outputs.

## Acceptance criteria
PASS only if:
1. guide exists at exact destination;
2. commands/paths are truthful;
3. architecture matches Release 1.9;
4. simulated/replay disclosure is explicit;
5. README has one useful front-door reference;
6. README link resolves;
7. guide links resolve;
8. no executable/config behavior changed;
9. only two authorized documentation paths were changed;
10. published Release 1.9 lifecycle state was not mutated.

## Required success report
Report exact files changed, command/path audit, link audit, architecture/disclosure consistency, README section used, and:

`RELEASE 1.9 SHOWCASE GUIDE AUTHORITY REPOSITORY MUTATIONS: TWO DOCUMENTATION PATHS ONLY — GUIDE + README`

`RELEASE 1.9 SHOWCASE GUIDE AUTHORITY GIT MUTATIONS: ZERO`

`RELEASE 1.9 SHOWCASE GUIDE AUTHORITY GITHUB MUTATIONS: ZERO`

`RELEASE 1.9 SHOWCASE GUIDE ACCEPTED — SEPARATE NARROW DOCUMENTATION PR CREATION AUTHORITY REQUIRED`

Terminal:

`RELEASE 1.9 SHOWCASE GUIDE + README FRONT-DOOR DOCUMENTATION AUTHORITY COMPLETE`

## Required blocked report
State the exact conflicting path/content, unavailable source, command/link inconsistency, mutations already made, and minimum reconciliation needed.

Terminal:

`RELEASE 1.9 SHOWCASE GUIDE + README FRONT-DOOR DOCUMENTATION AUTHORITY BLOCKED`

Do not emit COMPLETE unless both guide and README reference are validated.

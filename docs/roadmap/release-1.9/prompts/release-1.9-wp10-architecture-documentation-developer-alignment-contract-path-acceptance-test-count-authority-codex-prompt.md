# Release 1.9 — WP10 Architecture/Documentation/Developer-Alignment Contract + Path + Acceptance + Test-Count Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only semantic/path/acceptance authority** for Release 1.9 WP10, canonical issue **#235**.

WP10 implementation is currently blocked because the canonical Release 1.9 artifacts identify only high-level documentation objectives and four writable documentation locations, but do not define exact truthful content, forbidden edits, validation rules, or test-count expectations.

This authority exists solely to make WP10 implementation-ready.

No production mutation.
No test mutation.
No Python mutation.
No package/schema/signing mutation.
No GitHub mutation.
No WP11+ work.

---

# Verified predecessor state

Treat as binding unless current read-back disproves it:

- #233 Closed / Done.
- #234 Closed / Done.
- #235 Open / Backlog.
- #235 Project item:
  `PVTI_lAHOCAzBgs4BfsiAzg33Xh8`.
- #235 metadata:
  - Release 1.9
  - Priority P1
  - Area Documentation.
- milestone #58 Open.
- latest accepted pre-WP10 technical baseline:
  - .NET 339/339
  - Python 17/17
  - build 0 warnings / 0 errors
  - WP08 18/18
  - WP09 permanent integration/architecture accepted.
- WP10 unstarted.

Do not mutate GitHub under this authority.

---

# Canonical WP10 high-level objective

The accepted Release 1.9 artifacts require documentation alignment covering:

- architecture;
- developer setup;
- simulated-data warning;
- lifecycle;
- security;
- troubleshooting;
- branch/PR workflow.

Current authorized writable documentation locations:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
3. `README.md`
4. `docs/project/ROADMAP.md`

This authority must define exact content and acceptance boundaries for those four paths.

---

# Objective

Create one binding WP10 documentation contract that defines:

1. exact required content per document;
2. exact forbidden/misleading claims;
3. exact cross-link requirements;
4. exact current-state architecture/lifecycle/security claims;
5. exact simulated-data warning language/content requirements;
6. exact developer-environment setup expectations;
7. exact troubleshooting coverage;
8. exact branch/PR workflow guidance;
9. exact acceptance validation;
10. exact test-count delta and expected post-WP10 technical totals;
11. exact GitHub lifecycle boundary.

The output must be implementation-ready without requiring Terra to invent documentation semantics.

---

# Canonical output artifact

Create exactly:

`docs/roadmap/release-1.9/RELEASE_1.9_WP10_ARCHITECTURE_DOCUMENTATION_DEVELOPER_ALIGNMENT_CONTRACT_PATH_ACCEPTANCE_TEST_COUNT_AUTHORITY.md`

No other artifact is required unless repository governance mandates a single cross-reference.

---

# Binding predecessor sources

Read completely before drafting:

- Release 1.9 definition;
- Release 1.9 execution plan;
- Release 1.9 manifest/path authority;
- issue #235;
- accepted WP05–WP09 contracts/evidence relevant to architecture/setup/lifecycle/security;
- current contents of all four writable docs;
- Smart App Control local-signing documentation;
- current branch/PR workflow docs if they exist elsewhere;
- current ROADMAP status/sequence.

Do not document historical assumptions as current truth without verification.

---

# Section 1 — WP10 scope boundary

Define WP10 as **documentation alignment only**.

WP10 must not:
- change production behavior;
- change tests;
- change Python code;
- change packages;
- change schema;
- change signing implementation;
- change lifecycle implementation;
- change GitHub workflow configuration;
- create new runtime behavior.

WP10 documents accepted current state only.

Any discovered implementation/documentation mismatch requiring code changes must be reported, not fixed under WP10.

---

# Section 2 — Test-count contract

Determine whether issue #235/canonical plan requires any new executable tests.

Preferred rule if no test work is explicitly required:

- .NET delta: **+0**
- Python delta: **+0**
- total test delta: **+0**
- pre-WP10 .NET baseline: **339/339**
- expected post-WP10 .NET total: **339/339**
- pre-WP10 Python baseline: **17/17**
- expected post-WP10 Python total: **17/17**

This authority MUST verify that +0/+0 is correct from issue #235 and the Release 1.9 plan.

If canonical WP10 requires executable test additions, STOP and report the contradiction rather than inventing counts.

---

# Section 3 — `DOTNET_PYTHON_INTEROPERABILITY.md` contract

Define exact required updates.

Must truthfully document:

## Architecture boundary
- .NET Worker/application side remains producer.
- Python/Streamlit remains consumer/presentation.
- governed cross-process boundary is the canonical JSON handoff/file path defined by accepted contracts.
- no direct provider/SQLite access from presentation.
- no Worker↔Streamlit production supervision relationship.
- Release 1.8 JSON-over-stdio capability endpoint remains separate and is not the Release 1.9 presentation transport.

## Data flow
Document current accepted flow:

Replay / historical composition
→ Application/pipeline
→ visualization read model
→ file publisher / canonical JSON handoff
→ WP05 parser
→ WP06 visualization frame
→ WP07 presentation sections
→ Streamlit.

If different source paths exist for Ready/WarmUp versus Empty/Failed, document them accurately and concisely.

## Lifecycle
Document:
- independent processes;
- Worker startup prior canonical-handoff cleanup ownership;
- atomic publication semantics;
- Streamlit read/refresh ownership;
- WP08 graceful cancellation/restart acceptance;
- no stale prior-session handoff accepted as new readiness.

## Security/no-bypass
Reference permanent WP09 architecture rules.

Forbidden:
- implying Python invokes .NET provider/database directly;
- implying Streamlit owns Worker lifecycle;
- implying the WP08 probe is a production interface;
- implying JSON-over-stdio is the Release 1.9 presentation boundary.

---

# Section 4 — `PYTHON_DEVELOPER_ENVIRONMENT.md` contract

Must include or accurately cross-reference:

## Python environment
- supported Python version currently used by Release 1.9;
- exact governed package pins if the guide already owns them;
- Streamlit 1.61.1;
- environment setup/verification commands;
- `pip check`.

## Windows local signing
Cross-reference the canonical Smart App Control guide:
`docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`

Do not duplicate the entire signing guide.

Explain:
- local-development-only Authenticode signing;
- opt-in Debug-only activation;
- not production/public trust;
- not a “bypass”.

## Running presentation tests
Document current governed Python test commands/suites as appropriate:
- WP05;
- WP06;
- WP07 semantic/presentation;
- WP09 permanent presentation tests.

Do not claim WP10 adds tests if test delta is +0.

## Troubleshooting
Cover:
- `ModuleNotFoundError`/path issues if current guide already owns them;
- Streamlit version mismatch;
- `pip check` failures;
- Smart App Control assembly-load symptom and cross-reference;
- stale handoff/readiness confusion only at high level, without duplicating WP08 internals.

Forbidden:
- telling developers to disable security controls as the primary setup path;
- implying self-signed local trust is portable production signing.

---

# Section 5 — root `README.md` contract

README must remain concise.

Required content:

## Architecture summary
One current-state paragraph covering:
- .NET processing/Worker side;
- JSON handoff;
- Python/Streamlit presentation side;
- no-bypass boundary.

## Simulated-data warning
A clear, prominent warning that current demonstration/replay data is simulated/deterministic and is not live market/provider data.

Required qualities:
- visible near usage/demo instructions;
- not buried in troubleshooting;
- explicitly distinguishes simulated/demo data from production/live feeds;
- avoids implying trading suitability.

Suggested semantic content:
“Current Release 1.9 visualization/demo flows use deterministic simulated/replay data for local demonstration and testing. They do not represent a live market-data feed.”

Do not overstate beyond accepted repository behavior.

## Developer setup links
Link to:
- Python developer environment guide;
- Smart App Control local signing guide where appropriate;
- architecture interoperability doc.

## Lifecycle/demo usage
Only document commands/behavior that currently exist and are accepted.

## Branch/PR workflow
README may contain a concise pointer to the canonical workflow section/doc rather than duplicate full policy.

Forbidden:
- stale architecture diagrams/claims;
- live-data implication;
- direct DB/provider UI claims;
- outdated WP sequence/status.

---

# Section 6 — `ROADMAP.md` contract

Must align Release 1.9 status with actual accepted lifecycle.

At time of WP10 implementation start:

- WP08 complete;
- WP09 complete;
- WP10 in progress until closure;
- #233 Closed / Done;
- #234 Closed / Done;
- #235 Open / Backlog pre-completion.

The roadmap update should document:
- Release 1.9 objective/status;
- completed WP01–WP09 as appropriate;
- WP10 documentation alignment scope;
- successor packages remain untouched.

After WP10 implementation but before GitHub close, roadmap content may state “WP10 implementation complete, lifecycle pending” only if that is the repository's established style.

Avoid hardcoding ephemeral Project node IDs unless roadmap convention already does so.

Forbidden:
- marking WP10 closed before lifecycle completion if that would be false at commit time;
- starting/claiming WP11+ work;
- closing milestone #58.

---

# Section 7 — Simulated-data warning contract

The warning must appear in at least:

- `README.md`

and may be cross-referenced from:
- Python developer guide;
- interoperability doc if user-facing demo semantics are discussed.

Required assertions:
- deterministic/simulated/replay;
- not live provider data;
- intended for testing/demo;
- no guarantee of real-time/live trading suitability.

Avoid legal boilerplate unless repository already uses it.

---

# Section 8 — Lifecycle documentation contract

Across the four docs, ensure the accepted lifecycle story is consistent:

- Worker and Streamlit launched independently.
- Worker owns publication/start-session cleanup.
- Streamlit consumes the handoff.
- cancellation is graceful where governed.
- restart requires new publication/readiness, not stale payload acceptance.
- final harness/demo cleanup owns only temporary test/demo resources.
- no custom IPC introduced.

Do not expose WP08 test-only command-line seams as ordinary production usage unless already documented as test-only.

---

# Section 9 — Security documentation contract

Required current-state claims:

- presentation does not access provider/SQLite directly;
- architecture tests permanently enforce no-bypass rules;
- Windows local signing is dev-only;
- no secrets/private keys committed;
- no recommendation to disable Smart App Control as normal workflow;
- local self-signed setup is machine-local development trust.

If README security content is too detailed, cross-reference guides instead.

---

# Section 10 — Troubleshooting contract

Required issues to cover across the docs without duplication:

- Python env/version/package mismatch;
- Streamlit version;
- `pip check`;
- App Control / `0x800711C7` cross-reference;
- stale/old handoff confusion;
- process/listener residue troubleshooting at a high level;
- test-runner output anomalies only if currently documented/valuable.

Do not document transient one-off debugging internals as permanent user guidance unless reusable.

---

# Section 11 — Branch/PR workflow contract

Read the repository's actual current workflow conventions.

The documentation must truthfully state:
- expected branch usage;
- PR workflow;
- whether direct work on `main` is allowed or discouraged;
- required validation before PR/merge;
- no assumptions about CI checks not present in repository.

Prefer a concise canonical section in `README.md` or a cross-reference if a workflow doc already exists.

Do not invent branch protection or CI policy.

---

# Section 12 — Link/cross-reference contract

All new/updated relative links must resolve within the repository.

Required cross-links should include:

- README → interoperability doc;
- README → Python developer guide;
- Python developer guide → Smart App Control guide;
- interoperability doc → relevant developer/setup guide if helpful;
- ROADMAP → Release 1.9 planning/status docs if repository convention supports it.

Do not create circular duplicated content unnecessarily.

---

# Section 13 — Per-document forbidden edits

## DOTNET_PYTHON_INTEROPERABILITY.md
Do not:
- change production semantics by documentation;
- document acceptance-only probe as permanent transport;
- imply direct DB/provider UI access.

## PYTHON_DEVELOPER_ENVIRONMENT.md
Do not:
- embed private certificate material;
- prescribe global security disablement;
- change package pins unless existing source of truth already changed.

## README.md
Do not:
- become a full implementation manual;
- expose ephemeral test internals;
- claim live market data.

## ROADMAP.md
Do not:
- mark future WPs complete;
- close milestone;
- rewrite historical release sequence incorrectly.

---

# Section 14 — Documentation acceptance rules

WP10 implementation acceptance must include:

## Content truthfulness
Every changed factual claim must be supported by:
- current repository implementation;
- accepted WP05–WP09 contracts/evidence;
- current GitHub lifecycle where relevant.

## Link validation
Every added/changed relative link resolves to an existing repository path.

## Command validation
Commands included must:
- exist;
- use current paths;
- not rely on removed tools;
- be safe/default-local where possible.

## Security validation
No:
- private keys;
- passwords;
- machine-specific secrets;
- unsafe “disable security” default guidance.

## Terminology validation
Use:
- “local-development Authenticode signing for Windows Smart App Control compatibility”
not:
- “Smart App Control bypass”.

## Simulated-data validation
README warning present and unambiguous.

## Lifecycle consistency
No contradictions across the four docs.

---

# Section 15 — Test/validation contract

If +0/+0 is confirmed, WP10 does not add executable tests.

Implementation validation must still run:

- build: 0 warnings / 0 errors;
- full existing .NET regression: **339/339**;
- full governed Python regression: **17/17**;
- Streamlit 1.61.1;
- `pip check`;
- documentation link validation;
- documentation content/security audit.

No new test-count delta.

If any executable test is added during WP10 implementation, that is an authority violation unless a fresh amendment is created first.

---

# Section 16 — Residue contract

WP10 documentation work should create no runtime residue.

Acceptance must verify:
- no WP10-owned Worker;
- no Streamlit;
- no Python/testhost process launched solely by documentation validation remains;
- no temp runtime roots;
- no handoff/database residue.

Standard test-result artifacts may remain in normal result directories if regression commands create them.

---

# Section 17 — GitHub lifecycle boundary

This authority does not mutate GitHub.

Future Terra implementation/completion authority may, only after documentation and regression acceptance:

- set #235 Project Status → Done;
- close #235;
- read back;
- preserve #233/#234;
- leave WP11+ untouched.

No Project item creation/deletion.

---

# Section 18 — Exact path authority

Future WP10 implementation may modify only:

1. `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
2. `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
3. `README.md`
4. `docs/project/ROADMAP.md`

No other docs unless this artifact explicitly adds a narrow exception.

The contract artifact itself is created by this Luna authority.

---

# Section 19 — Expected mutation/test model

Documentation implementation:
- up to 4 documentation files modified;
- +0 .NET tests;
- +0 Python tests;
- 339/339 remains expected;
- 17/17 remains expected.

If repository inspection disproves +0/+0, STOP.

---

# Section 20 — Stop conditions

Stop documentation definition if:

- issue #235 requires executable tests;
- one of the four docs is not actually the canonical owner of the required content;
- branch/PR workflow cannot be determined truthfully;
- current implementation contradicts a required high-level WP10 claim;
- path authority needs another document not listed;
- +0/+0 cannot be justified.

Do not invent policy.

---

# Mutation boundary

Allowed:
- create exactly one contract artifact:
  `docs/roadmap/release-1.9/RELEASE_1.9_WP10_ARCHITECTURE_DOCUMENTATION_DEVELOPER_ALIGNMENT_CONTRACT_PATH_ACCEPTANCE_TEST_COUNT_AUTHORITY.md`

All other repository/GitHub mutations:
`ZERO`

---

# Required completion report

## Artifact
Exact path.

## Per-document contract
Required/forbidden edits for all four docs.

## Simulated-data warning
Exact semantic requirement.

## Architecture/lifecycle/security
Exact current-state claims.

## Branch/PR workflow
Exact truthful source/contract.

## Acceptance
Link/command/security/content rules.

## Test-count
Confirm:
- +0 .NET
- +0 Python
- 339/339 unchanged
- 17/17 unchanged.

## Residue/GitHub boundary
Exact gates.

## Mutation statement

`WP10 ARCHITECTURE/DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT AUTHORITY MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

## Next step

On success:

`WP10 DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT DEFINED — FRESH TERRA IMPLEMENTATION AUTHORITY REQUIRED`

---

# Terminal markers

Success:

`RELEASE 1.9 WP10 ARCHITECTURE/DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT + PATH + ACCEPTANCE + TEST-COUNT AUTHORITY COMPLETE`

Blocked:

`RELEASE 1.9 WP10 ARCHITECTURE/DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT + PATH + ACCEPTANCE + TEST-COUNT AUTHORITY BLOCKED`

Do not emit COMPLETE unless the artifact fixes exact content, path, acceptance, security, lifecycle, workflow, and test-count semantics without implementation mutation.

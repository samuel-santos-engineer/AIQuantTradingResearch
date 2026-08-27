# Release 1.9 WP10 — Architecture, Documentation & Developer Alignment

## Authority status

This is the binding documentation-only contract and path authority for WP10
(issue #235). It authorizes one later GPT-5.6 Terra implementation authority;
it does not itself edit the four governed documents, execute tests, or change
GitHub. WP08 and WP09 are accepted predecessor work. WP10 remains open until
its implementation authority completes and performs its GitHub closure gate.

## Verified starting state

- Milestone #58 is open. Issues #233 and #234 are closed/Done; #235 is
  open/Backlog with Release 1.9, P1, and Area Documentation.
- Project #2 contains the unique #235 item
  `PVTI_lAHOCAzBgs4BfsiAzg33Xh8`; its Release, Priority, and Area values are
  preserved.
- The synchronized repository predecessor is
  `3a02f035a253e4e16f479e1866c9a5195f5cfbdb`, with local `main` equal to
  `origin/main` before this authority's mutation.
- Python is CPython 3.13.15 x64; the project environment is the disposable
  repository `.venv`; direct pins are NumPy 2.5.1, pandas 3.0.5,
  scikit-learn 1.9.0, and Streamlit 1.61.1.
- Accepted predecessor evidence is WP08 18/18, WP09 +12 .NET/+4 Python,
  .NET 339/339 after WP09, Python 17/17, and build 0/0.

## Exact writable surface

The Terra implementation authority may modify only these existing Markdown
files, and only for the concerns stated below:

| Path | Required alignment | Forbidden change |
| --- | --- | --- |
| `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md` | State the delivered local one-shot `.venv` process boundary, versioned JSON-over-stdio, ownership, lifecycle, timeout/cancellation, bounded diagnostics, security, and WP08/WP09 relationship truthfully. | New transport, service, package, schema, ML behavior, provider/SQLite/UI bypass, or change to Domain/Application ownership. |
| `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md` | State CPython 3.13.15, `.venv` ownership, exact existing pins, qualified commands, Streamlit validation, troubleshooting, test commands, signing cross-link, and portable interpreter policy. | Installation of new dependencies, global project packages, user-specific paths, policy weakening, or production ML claims. |
| `README.md` | Add a concise current architecture/foundation summary, prominent simulated/replay-data warning, and links to the guide, interoperability document, signing guide, and roadmap. | Rewriting historical release claims, claiming Release 1.9 product behavior, adding new commands/tools, or defining 1.10/2.0. |
| `docs/project/ROADMAP.md` | Record the truthful current Release 1.9 lifecycle: WP08/WP09 complete, WP10 in progress, #235 open until closure, and the successor sequence untouched. | Closing milestone #58, defining WP11+, implementing 1.9 behavior, or changing future milestone scope. |

No other path is writable. In particular, source, tests, Python scripts,
`requirements.txt`, project files, schema, packages, signing scripts,
`.venv`, authority files, GitHub objects, and Git integration are verify-only.

## Binding architecture and lifecycle claims

The current flow is deterministic simulated/replay evidence through the
existing pipeline and canonical read model, then a Worker-owned local atomic
JSON handoff consumed read-only by Streamlit. The presentation layer does not
read SQLite, call providers, reconstruct features, or create a parallel
pipeline. Worker and Streamlit are independent processes; WP08's finite
acceptance harness may orchestrate them for proof but is not a production
supervisor. Worker cancellation, restart, owned-process cleanup, and bounded
refresh semantics must be described as accepted contracts, not expanded.

The .NET boundary is the separate Release 1.8 one-shot Python capability
endpoint using versioned JSON-over-stdio. It is not the Release 1.9
Worker-to-Streamlit handoff and must not be presented as a generic bridge.
Domain remains free of Python, Streamlit, process, JSON, filesystem, provider,
and persistence concerns; Application owns technology-neutral contracts;
Infrastructure owns concrete process, file, and serialization mechanics.

Documentation must say prominently and unambiguously that current charts and
validation use deterministic simulated/replay data, not a live market feed or
investment advice. It must not imply that Release 1.9 ML, model training,
remote execution, OpenTelemetry, or Backtesting has begun.

## Developer, security, and workflow rules

The guide must use interpreter-qualified commands through `.venv`, retain the
exact four direct pins, keep transitive packages as resolver output, and
explain disposable environment recreation plus `pip check`. It may link to
`WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`; it must preserve that document's
local-only Authenticode/App Control setup, uncommitted local props, and
no-bypass/security-hygiene language. No private key, PFX, password, secret,
machine-specific absolute path, or policy-disable instruction may be added.

The four documents must link only to existing, correctly relative or canonical
repository/GitHub targets. Commands must be current, bounded, and safe for a
fresh checkout; bare global `pip`, arbitrary Python-module execution, and
commands that delete broad directories are forbidden. The documentation must
retain the repository workflow: dedicated branch, acceptance/verification,
pull request to `main`, review, and merge; direct release integration to
`main` is forbidden. This authority does not create a branch, commit, PR, or
GitHub mutation.

## Acceptance contract for the Terra implementation

The implementation authority must prove, without inventing executable tests:

1. Each required claim above is traceable to the accepted Release 1.9
   definition/plan/manifest and WP05–WP09 contracts/evidence.
2. All four files contain only the permitted alignment, with no stale claim
   that contradicts delivered WP08/WP09 behavior or the unstarted Release 1.9
   product boundary.
3. Relative links resolve, Markdown formatting is valid, documented commands
   use governed paths, and the documentation/security scan finds no secrets,
   private certificate material, policy bypass, or unsafe absolute path.
4. The exact test-count contract is unchanged: WP10 is **+0 .NET, +0 Python,
   +0 total**; pre- and post-WP10 evidence remains **339/339 .NET** and
   **17/17 Python**. If any executable test or additional path is required to
   satisfy WP10, stop and require a new narrow authority rather than inventing
   one.
5. Repository cleanliness/diff accounting proves only the four authorized
   Markdown files changed in the Terra implementation; no GitHub objects,
   Project fields/items, milestones, packages, Python environment, processes,
   or runtime residue were changed or left by the documentation work.

The later Terra implementation authority may perform the documented GitHub
closure transition only after every row passes: identify exactly one #235
Project #2 item, set its established completed Status, read back, close #235,
read back, preserve #233/#234 Closed/Done, preserve milestone #58 open, and
leave WP11+ untouched. It must not begin the next work package.

## Mutation accounting and handoff

`WP10 ARCHITECTURE/DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT AUTHORITY MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP10 DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT DEFINED — FRESH TERRA IMPLEMENTATION AUTHORITY REQUIRED`

RELEASE 1.9 WP10 ARCHITECTURE/DOCUMENTATION/DEVELOPER-ALIGNMENT CONTRACT + PATH + ACCEPTANCE + TEST-COUNT AUTHORITY COMPLETE

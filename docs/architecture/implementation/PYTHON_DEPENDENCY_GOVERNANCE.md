# Python Dependency Governance

## Scope and ownership

Machine scope owns only the governed CPython 3.13 runtime and normal bootstrap
tooling. The repository owns dependency declarations and governance. Installed
project packages belong only in the ignored repository-root `.venv`; VS Code
extensions are developer tooling, not Python application dependencies. Python
packages do not alter the .NET package, project, or reference graph.

## Declaration and installation

`requirements.txt` is the single authoritative direct-dependency declaration.
WP07 now pins exactly NumPy 2.5.1, pandas 3.0.5, scikit-learn 1.9.0, and
Streamlit 1.61.1. No `pyproject.toml`, lock
file, constraints file, or alternative dependency manager is introduced.

All dependency mutation must use the proven repository interpreter:

```powershell
.venv\Scripts\python.exe -m pip install -r requirements.txt
```

Before mutation, verify `.venv\Scripts\python.exe --version` and
`sys.prefix != sys.base_prefix`. Bare `pip`, machine `python -m pip`,
`py -m pip` targeting the machine runtime, `--user`, elevation, and global
installation are prohibited for project dependencies.

## Direct, transitive, and version policy

Direct dependencies are explicitly selected capabilities used by repository
code or governed tooling and must appear in `requirements.txt`. Transitive
dependencies are implementation details unless separately selected; their
presence never authorizes direct use. A later governed reproducibility
artifact may capture resolved transitives, but `pip freeze` is evidence and
not an automatic design record.

Future direct entries must use exact `==` versions chosen from authoritative
Python 3.13 compatibility/security evidence. Changes are intentional: review
the selection record, upstream release/security information, compatibility,
and the direct declaration; then recreate/validate `.venv` and inspect the
diff. No uncontrolled `pip install --upgrade` is permitted. Exact versions
The four WP07 direct versions are exact; resolved transitives remain pip/upstream
resolution evidence rather than a second lock mechanism.

## Reproducibility, integrity, and security

The reproducible lifecycle is: verify machine Python; create `.venv`; verify
its provenance; install the direct declaration with the qualified interpreter;
run `python -m pip check`, `pip list`, and `pip freeze`; then run governed
Python and .NET validation. WP07 captures the resolved transitive set as
validation evidence without promoting transitives to direct design.

Use PyPI or separately governed trusted sources only. Do not embed credentials
or private-index tokens, disable TLS, use arbitrary mirrors, or introduce
ungoverned dependency tools. Gitleaks remains mandatory. New foundational
libraries require selection records.

## VS Code, recreation, upgrade, and removal

Machine Python is the bootstrap interpreter; `.venv` is the preferred project
interpreter once present. VS Code terminal installation commands must still
target `.venv`; never commit absolute interpreter paths.

To remove a dependency, remove its direct declaration, reconstruct the
environment rather than trusting residue, validate integrity, and update its
selection/governance record when the architectural capability changes.

To upgrade one, assess compatibility and security, revisit its selection
record triggers, update the direct declaration and governed resolution if
introduced, recreate/validate `.venv`, run Python/.NET checks, and document
material changes.

# Release 1.8 Python Runtime Compatibility Decision

Status: authoritative WP02 decision for Release 1.8.

Research date: 2026-08-23 (America/Sao_Paulo). Sources were reviewed from
official upstream documentation only; no Python or package installation was
performed.

## Decision

Release 1.8 selects **Python 3.13** as its single machine-runtime target.
Later installation work shall use the latest secure patch release in the
3.13 minor line. An exact patch pin is deferred to the installation and
dependency-governance work packages.

Python 3.13 is the preferred intersection because it has normal Windows
x64 releases, current scientific-stack wheels, Streamlit support, and a
more mature ecosystem than the newly released 3.14 line. Python 3.14 is a
viable alternative, but is rejected for this baseline because novelty is not
the goal and broader third-party maturity is preferable for a machine-wide
foundation. Python 3.12 remains viable and is used by Streamlit Community
Cloud by default, but 3.13 provides a newer supported baseline without
requiring the 3.14 adoption risk.

## Compatibility matrix

| Component | Current version/release considered | Minimum Python | Maximum/upper support evidence | Windows x64 evidence | Notes |
|---|---|---|---|---|---|
| CPython | 3.13.14 current 3.13 release observed | 3.13 target | 3.13 minor line; patch governed later | Official 64-bit Windows installer and embeddable package published | Python 3.13 supports Windows 8.1 and newer, therefore Windows 11 |
| NumPy | Current stable documentation, 2.5 manual / 2.4 release notes | Package metadata is authoritative at restore time; no unsupported upper bound inferred | No documentation-based upper bound asserted | Official NumPy documentation describes Windows x86/x86-64 wheels and recommends binaries where available | Scientific binary wheels are required; exact version belongs to later dependency governance |
| pandas | 3.0.5 documentation | Current installation page lists NumPy >=1.26.0 as a required dependency; Python support policy is linked upstream rather than duplicated | No unsupported upper bound inferred | Official installation documentation supports Windows and recommends a virtual environment; PyPI distribution is official | pandas 3.0 introduces behavior changes; exact release/constraints belong to WP06 |
| scikit-learn | 1.9.0 documentation and release/PyPI metadata | 1.7 requires Python 3.10+; current 1.9 publishes CPython 3.12/3.13/3.14 wheels | Current docs do not require a narrower upper bound for 3.13 | PyPI lists `win_amd64` wheels for CPython 3.13; official install docs cover Windows 64-bit Python 3 | NumPy/SciPy dependencies remain project-local and are governed later |
| Streamlit | Current installation/deployment documentation | Official installation page supports Python 3.10 through 3.14 | Community Cloud supports released Python versions still receiving security updates; no narrower Streamlit upper bound asserted | Local installation guidance covers Windows; normal CPython 3.13 is within the supported range | Community Cloud defaults to 3.12, but permits selecting a supported version; no deployment performed |

### Official sources

- Python Windows releases: <https://www.python.org/downloads/windows/>
- Python 3.13 Windows documentation: <https://docs.python.org/3.13/using/windows.html>
- NumPy installation: <https://numpy.org/install>
- NumPy Windows wheel/build evidence: <https://numpy.org/doc/stable/dev/releasing.html>
- pandas installation and support guidance: <https://pandas.pydata.org/pandas-docs/stable/getting_started/install.html>
- pandas 3.0 release notes: <https://pandas.pydata.org/pandas-docs/stable/whatsnew/v3.0.0.html>
- scikit-learn installation and Python history: <https://scikit-learn.org/stable/install.html>
- scikit-learn 1.8/1.9 release documentation: <https://scikit-learn.org/stable/whats_new/v1.8.html>
- Streamlit installation support: <https://docs.streamlit.io/get-started/installation/command-line>
- Streamlit Community Cloud Python policy: <https://docs.streamlit.io/deploy/streamlit-community-cloud/deploy-your-app/deploy>

## Machine versus project ownership

The Windows machine owns the selected CPython 3.13 runtime, any official
launcher supplied by the installation method, and base runtime tooling.
Later WP03/WP04 work must verify machine installation, PowerShell, and VS
Code resolution.

The repository owns its Python dependencies through a project-local isolated
virtual environment and a reproducible dependency declaration introduced by
later work packages. NumPy, pandas, SciPy where governed, scikit-learn, and
Streamlit are project dependencies, never authoritative machine-global
application dependencies. Global `pip install` is not an accepted project
dependency workflow. `.venv`, caches, and generated local environment state
must remain untracked.

Streamlit remains a project dependency even though it supplies a CLI.
scikit-learn is a project dependency for Release 1.9 readiness. No ML.NET
dependency is introduced by this decision.

## Deferred boundaries

WP02 does not install Python, create a venv or dependency manifest, choose a
package manager, configure VS Code, validate the machine runtime, select or
implement `.NET`/Python transport, create Streamlit UI, implement ML, or
begin Release 1.9. Future interoperability must preserve the existing .NET
architecture, keep Python out of Domain contracts, retain deterministic
evidence/provenance expectations, specify failure/process boundaries, and
keep Python replaceable at the integration boundary.

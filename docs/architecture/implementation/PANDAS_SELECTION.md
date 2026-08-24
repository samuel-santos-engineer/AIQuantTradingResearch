# pandas Selection

## Decision

Select pandas for Python-side tabular data manipulation, DataFrame transformation, analytical preparation, and readable inspection of governed research evidence.

Exact selection: `pandas==3.0.5` (observed 2026-08-24). PyPI publishes it as a stable release with a CPython 3.13 Windows x86-64 wheel and Python 3.13 support metadata. It resolves with the selected NumPy 2.5.1 and scikit-learn 1.9.0.

pandas integrates naturally with NumPy and scikit-learn and provides a productive, expressive tabular API. Alternatives considered include direct NumPy structures, standard-library data handling, Polars, and distributed dataframe systems. They were not selected now because the initial scope benefits from pandas ecosystem maturity and direct stack compatibility.

Accepted trade-offs include memory overhead, mutable-transformation risks, behavioral/API evolution, and limited suitability for larger-than-memory or distributed workloads. pandas does not replace authoritative durable persistence, schema, or evidence identity.

## Ownership and boundaries

pandas is a project-local `.venv` dependency declared directly in `requirements.txt`. DataFrames and pandas operations remain Python implementation/evidence-preparation concerns and must not become Domain contracts or bypass governed persistence and platform data boundaries.

## Validation and reconsideration

Later work must validate isolated restoration, deterministic DataFrame construction/transformation, compatibility with NumPy and scikit-learn, and absence of unintended durable state. Version changes follow intentional WP06 upgrade and recreation governance. Reconsider if memory/distribution requirements, compatibility, or evidence semantics require another table engine. Compatibility evidence remains in `PYTHON_RUNTIME_COMPATIBILITY.md`.

This record supports Release 1.8 readiness and does not implement Release 1.9.

Evidence and policy: https://pypi.org/project/pandas/3.0.5/ and https://pypi.org/pypi/pandas/3.0.5/json. Install only in `.venv`; revisit on upstream security/support changes, Python compatibility changes, or a governed resolver conflict.

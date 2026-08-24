# NumPy Selection

## Decision

Select NumPy as the foundational Python numerical-array library for quantitative and ML-readiness work. It provides efficient n-dimensional arrays, vectorized operations, and the interoperability base expected by pandas, SciPy, and scikit-learn.

Exact selection: `numpy==2.5.1` (observed 2026-08-24). PyPI marks it Production/Stable, declares Python 3.12–3.14 classifiers, and publishes a CPython 3.13 Windows x86-64 wheel. It is compatible with the selected pandas 3.0.5 and scikit-learn 1.9.0 resolution.

## Alternatives and trade-offs

Built-in Python sequences, pandas-only computation, array-specific alternatives, and larger numerical frameworks were considered. They do not provide the same broadly shared scientific-stack foundation. Accepted trade-offs include native/binary dependency surface, platform wheel concerns, memory usage, and layout/dtype complexity.

## Ownership and boundaries

NumPy is a project-local `.venv` dependency declared directly in `requirements.txt`, never an authoritative machine-global application dependency. Arrays, dtypes, and NumPy representations are Python implementation concerns and must not become Domain contracts. Any future capability must consume governed platform evidence and preserve deterministic, reproducible operations.

## Validation and reconsideration

Later dependency and library work must validate Windows x64 restoration, binary availability, deterministic numerical behavior, and isolated environment ownership. Version changes follow intentional WP06 upgrade and recreation governance. Reconsider if the required stack no longer supports the selected runtime, binary distribution becomes operationally unsuitable, or a governed workload needs a different numerical foundation. Compatibility evidence remains in `PYTHON_RUNTIME_COMPATIBILITY.md`.

NumPy is Release 1.8 foundation technology; no Release 1.9 ML capability is implemented here.

Evidence and policy: https://pypi.org/project/numpy/2.5.1/ and https://pypi.org/pypi/numpy/2.5.1/json. Install only in `.venv`; retain the exact pin until an intentional WP06-governed upgrade, security issue, Python support change, or resolver incompatibility triggers reconsideration.

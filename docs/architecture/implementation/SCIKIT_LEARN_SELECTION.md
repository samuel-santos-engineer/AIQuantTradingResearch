# scikit-learn Selection

## Decision

Select scikit-learn as the initial conventional machine-learning library for Release 1.9 readiness. It supplies mature supervised and unsupervised learning, preprocessing, model-selection, and evaluation capabilities that integrate with NumPy and pandas.

Exact selection: `scikit-learn==1.9.0` (observed 2026-08-24). PyPI publishes it as a stable release with a CPython 3.13 Windows x86-64 wheel and Python 3.13 support metadata. It resolves with NumPy 2.5.1 and pandas 3.0.5. No algorithm is selected here.

ML.NET was considered but is not selected as the primary ML foundation because the planned scientific stack and future Python-side capabilities are centered on the established Python ecosystem. Heavier deep-learning and GPU-centric frameworks were also considered but exceed the current bounded classical-ML readiness scope.

Accepted trade-offs include Python/.NET interoperability complexity, native binary dependencies, and limited suitability for deep-learning or GPU-centric workloads. ML must consume governed platform data/evidence and must not bypass established boundaries, create durable model state, or introduce production inference through this decision.

## Ownership and validation

scikit-learn is a project-local `.venv` dependency declared directly in `requirements.txt`. Later work must validate isolated restoration, deterministic disposable operations, reproducibility controls, and explicit separation of capability tests from product ML. Python-specific types must remain outside Domain contracts.

Version changes follow intentional WP06 upgrade and recreation governance. Reconsider if governed workloads require deep learning, GPU acceleration, different deployment topology, unacceptable interoperability cost, or a changed supported-runtime intersection. Compatibility evidence remains in `PYTHON_RUNTIME_COMPATIBILITY.md`.

This is Release 1.8 foundation governance for Release 1.9 readiness, not ML implementation.

Evidence and policy: https://pypi.org/project/scikit-learn/1.9.0/ and https://pypi.org/pypi/scikit-learn/1.9.0/json. Install only in `.venv`; revisit on security/support changes, Python compatibility changes, or a governed stack conflict.

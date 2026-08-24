# Python Runtime Selection

## Decision

Select CPython as the machine-wide Python runtime for the Python-side quantitative, ML-readiness, and interface capabilities planned by Release 1.8. The selected minor line is **Python 3.13**. Track the latest secure patch within 3.13 unless later governed evidence requires reconsideration.

Python complements the existing .NET platform; it does not replace it. Python is appropriate for the intended scientific, data, classical-ML, and Streamlit ecosystem, while .NET remains authoritative for existing platform architecture and contracts.

## Rationale and alternatives

WP02 compatibility research established 3.13 as the broad, mature Windows 11 x64 intersection. Python 3.14 was considered but not selected because the newest line carries greater adoption risk. Python 3.12 remains viable, but 3.13 offers a newer supported baseline with normal Windows distributions and current ecosystem support.

Accepted trade-offs are dual-runtime operational complexity, separate tooling and diagnostics, and an additional environment boundary.

## Ownership and boundaries

The machine owns CPython, its appropriate launcher, and base runtime tools. Project dependencies belong in an isolated project-local virtual environment; global package installation is not authoritative. Python mechanics must not leak into Domain contracts. Future integration must preserve replaceability, deterministic evidence, explicit failure semantics, and existing .NET boundaries.

## Validation and reconsideration

Later work must validate Windows, PowerShell, VS Code, venv isolation, and reproducible dependency restoration. Reconsider if the 3.13 line loses security support, the required library intersection changes, Windows support or binary distribution becomes inadequate, or governed interop requirements make another runtime necessary. WP02 compatibility evidence is preserved in `PYTHON_RUNTIME_COMPATIBILITY.md`.

Release 1.8 establishes the runtime foundation. Release 1.9 may consume it for governed ML work but is not implemented by this record.

# Changelog

**Status:** Active
**Version:** 1.0
**Last Updated:** 2026-08-24
**Maintainers:** AIQuantTradingResearch Team

---

All notable changes to AIQuantTradingResearch will be documented in this file.

The format follows the principles of **Keep a Changelog** and adheres to **Semantic Versioning (SemVer)**.

- Keep a Changelog: https://keepachangelog.com/
- Semantic Versioning: https://semver.org/

---

## [Unreleased]

### Planned

- Release 1.9 — Real-Time Financial Data Visualization.
- Release 1.10 — OpenTelemetry & Pipeline Observability.
- Release 2.0 — Lightweight Machine Learning Evaluation.
- Release 2.1 — broader Machine Learning (resequenced; scope preserved).
- Release 2.2 — Explainable AI (resequenced; scope preserved).
- Release 2.3 — Backtesting.

---

## [1.8.0] - 2026-08-24

### Added

- Governed PSF CPython 3.13 foundation with an isolated, disposable project `.venv`.
- Exact NumPy 2.5.1, pandas 3.0.5, scikit-learn 1.9.0, and Streamlit 1.61.1 dependency foundation.
- Deterministic offline scientific-stack and Streamlit validation.
- One-shot local `.NET ↔ Python` interoperability through versioned JSON-over-stdio.
- Permanent Application and Infrastructure interoperability tests.
- Portable Python developer-environment guidance and foundational selection records.

### Changed

- Permanent verification baseline increased to 281 passing tests with zero skipped.
- Current architecture and testing documentation aligned with the Release 1.8 foundation.

### Security

- Project dependencies remain isolated from machine Python, integration execution remains local and bounded, and schema v3 is unchanged.

Release 1.8 provides Python and interoperability readiness only. Release 1.9 real-time visualization behavior has not begun; lightweight ML evaluation is planned separately for Release 2.0.

---

## [0.1.0] - Foundation

### Added

- Initial GitHub repository
- MIT License
- Solution structure
- Documentation framework
- Engineering Workbook
- Project Constitution
- Engineering Guidelines
- Architecture Overview
- Roadmap
- Documentation index
- Initial folder structure

### Changed

- N/A

### Deprecated

- None

### Removed

- None

### Fixed

- None

### Security

- Initial repository configuration

---

## Versioning Strategy

The project follows Semantic Versioning:

MAJOR.MINOR.PATCH

Examples:

- 1.0.0 — First production-ready release
- 1.1.0 — New feature
- 1.1.1 — Bug fix

Major releases indicate breaking changes.

Minor releases introduce new capabilities while preserving compatibility.

Patch releases include bug fixes, documentation improvements, dependency updates, and minor refactoring.

---

## Release Philosophy

Each release should:

- Be fully documented.
- Include updated documentation.
- Build successfully.
- Pass automated tests.
- Be reproducible.
- Have a corresponding Git tag.

No release should contain undocumented functionality.

---

## Milestone Mapping

| Version | Milestone              |
| ------- | ---------------------- |
| 0.1.0   | Foundation             |
| 0.2.0   | Market Data Service    |
| 0.3.0   | Feature Engineering    |
| 0.4.0   | Backtesting Engine     |
| 0.5.0   | Machine Learning       |
| 0.6.0   | Explainable AI         |
| 0.7.0   | REST API               |
| 0.8.0   | Dashboard              |
| 0.9.0   | DevOps & Observability |
| 1.0.0   | Production Release     |

---

## Maintenance Guidelines

The changelog should be updated as part of every completed milestone.

Entries should focus on user-visible changes rather than implementation details.

Changes should be grouped under the following categories when applicable:

- Added
- Changed
- Deprecated
- Removed
- Fixed
- Security

The `Unreleased` section should always describe the next planned work and be reviewed before each release.
